﻿using HarshPoint.Diagnostics;
using HarshPoint.Provisioning;
using HarshPoint.Provisioning.Implementation;
using HarshPoint.Provisioning.Records;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace HarshPoint.Tests
{
    public abstract class SharePointClientTest :
        SeriloggedTest
    {
        private readonly HarshDisposableBag _disposables = new HarshDisposableBag();

        public SharePointClientTest(ITestOutputHelper output)
            : base(output)
        {
            ManualResolver = new ClientObjectManualResolver(CreateResolveContext);

            var progressBuffer = new ProgressBuffer<HarshProvisionerRecord>();
            Output = progressBuffer.Reports;

            ClientContext = CreateClientContext();
            Context = new HarshProvisionerContext(ClientContext)
                .WithProgress(
                    new ProgressComposite<HarshProvisionerRecord>(
                        new ProgressSerilog<HarshProvisionerRecord>(
                            HarshLog.ForContext("ProvisionerOutput", true)
                        ),
                        progressBuffer
                    )
                );
        }

        public void AddDisposable(IDisposable disposable)
            => _disposables.Add(disposable);

        public void AddDisposable(Action action)
            => _disposables.Add(action);

        public HarshProvisionerContext Context { get; }
        public ClientContext ClientContext { get; }
        public IReadOnlyCollection<HarshProvisionerRecord> Output { get; }
        public Site Site => ClientContext.Site;
        public TaxonomySession TaxonomySession => Context.TaxonomySession;
        public Web Web => ClientContext.Web;
        public ClientObjectManualResolver ManualResolver { get; }


        protected virtual ClientObjectResolveContext CreateResolveContext()
            => new ClientObjectResolveContext(Context);

        public override void Dispose()
        {
            try
            {
                _disposables.Dispose();
                ClientContext.ExecuteQueryAsync().Wait();
            }
            catch (AggregateException exc)
            {
                foreach (var inner in exc.InnerExceptions)
                {
                    Logger.Error.Write(inner);
                }
            }

            ClientContext.Dispose();
            base.Dispose();
        }

        protected async Task<Field> CreateField(params Expression<Func<Field, Object>>[] retrievals)
        {
            var guid = Guid.NewGuid();

            Web.Fields.AddFieldAsXml(
                $"<Field ID='{guid}' Name='{guid:n}' Type='Text' />",
                addToDefaultView: false,
                options: AddFieldOptions.DefaultValue
            );

            var field = Web.Fields.GetById(guid);

            ClientContext.Load(field, retrievals);
            await ClientContext.ExecuteQueryAsync();

            RegisterForDeletion(field);
            return field;
        }

        protected async Task<List> CreateList(params Expression<Func<List, Object>>[] retrievals)
        {
            var guid = Guid.NewGuid();
            var name = guid.ToStringInvariant("n");

            var list = Web.Lists.Add(new ListCreationInformation()
            {
                TemplateType = (Int32)ListTemplateType.GenericList,
                Title = name,
                Url = $"Lists/{name}",
            });

            if (retrievals.Any())
            {
                ClientContext.Load(list, retrievals);
            }

            ClientContext.Load(list, l => l.Id);

            await ClientContext.ExecuteQueryAsync();

            RegisterForDeletion(list);
            return list;
        }


        protected void RegisterForDeletion(ContentType ct)
        {
            if (ct == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(ct));
            }

            AddDisposable(ct.DeleteObject);
        }
        protected void RegisterForDeletion(Field f)
        {
            if (f == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(f));
            }

            AddDisposable(f.DeleteObject);
        }
        protected void RegisterForDeletion(List list)
        {
            if (list == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(list));
            }

            AddDisposable(list.DeleteObject);
        }

        protected IdentifiedRecord LastIdentifiedOutput()
            => Output.OfType<IdentifiedRecord>().Last();

        protected IdentifiedObjectRecord<T> LastObjectOutput<T>()
            => Output.OfType<IdentifiedObjectRecord<T>>().Last();

        private static ClientContext CreateClientContext()
        {
            var url = Environment.GetEnvironmentVariable("HarshPointTestUrl");

            if (String.IsNullOrWhiteSpace(url))
            {
                return new SeriloggedClientContext($"http://{Environment.MachineName}");
            }

            var clientContext = new SeriloggedClientContext(url);

            var username = Environment.GetEnvironmentVariable("HarshPointTestUser");
            var password = Environment.GetEnvironmentVariable("HarshPointTestPassword");
            var authType = Environment.GetEnvironmentVariable("HarshPointTestAuth");

            if (StringComparer.OrdinalIgnoreCase.Equals(authType, "Windows"))
            {
                clientContext.Credentials = new NetworkCredential(
                    username,
                    password
                );
            }
            else if (StringComparer.OrdinalIgnoreCase.Equals(authType, "SharePointOnline"))
            {
                clientContext.Credentials = new SharePointOnlineCredentials(
                    username,
                    password
                );
            }

            return clientContext;
        }

        private static readonly HarshLogger Logger = HarshLog.ForContext<SharePointClientTest>();
    }
}
