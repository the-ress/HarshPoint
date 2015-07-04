﻿using HarshPoint.Provisioning;
using HarshPoint.Provisioning.Implementation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Threading.Tasks;

namespace HarshPoint.Tests
{
    public class SharePointClientFixture : IDisposable
    {
        public const String TestListUrl = "HarshPoint Tests";
        public const String TestListTitle = "HarshPoint Tests";

        public SharePointClientFixture()
        {
            var url = ""; //Environment.GetEnvironmentVariable("HarshPointTestUrl");

            if (String.IsNullOrWhiteSpace(url))
            {
                ClientContext = new ClientContext("http://" + Environment.MachineName);
            }
            else
            {
                ClientContext = new ClientContext(url);
                ClientContext.Credentials = new SharePointOnlineCredentials(
                    Environment.GetEnvironmentVariable("HarshPointTestUser"),
                    Environment.GetEnvironmentVariable("HarshPointTestPassword")
                );
            }

            Context = new HarshProvisionerContext(ClientContext);
        }

        public async Task EnsureTestList()
        {
            var list = Web.Lists.GetByTitle(TestListTitle);
            ClientContext.Load(list);

            try
            {
                await ClientContext.ExecuteQueryAsync();
            }
            catch (ServerException)
            {
                list = Web.Lists.Add(new ListCreationInformation()
                {
                    Url = TestListUrl,
                    Title = TestListTitle,
                    TemplateType = (Int32)ListTemplateType.DocumentLibrary,
                    DocumentTemplateType = (Int32)DocumentTemplateType.Word,
                });

                await ClientContext.ExecuteQueryAsync();
            }
        }

        public HarshProvisionerContext Context
        {
            get;
            private set;
        }

        public ResolveContext<HarshProvisionerContext> ResolveContext
            => new ClientObjectResolveContext() { ProvisionerContext = Context };

        public void Dispose()
        {
            Context = null;
            ClientContext?.Dispose();
        }

        public ClientContext ClientContext
        {
            get;
            set;
        }

        public Site Site
        {
            get { return ClientContext?.Site; }
        }

        public Web Web
        {
            get { return ClientContext?.Web; }
        }

        public TaxonomySession TaxonomySession
            => Context?.TaxonomySession;
    }
}