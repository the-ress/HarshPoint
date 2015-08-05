﻿using HarshPoint.Provisioning;
using HarshPoint.Provisioning.Output;
using Microsoft.SharePoint.Client;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace HarshPoint.Tests.Provisioning
{
    public class ListProvisioning : SharePointClientTest
    {
        public ListProvisioning(SharePointClientFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [Fact]
        public async Task Existing_list_is_not_added()
        {
            await Fixture.EnsureTestList();

            var prov = new HarshList()
            {
                Title = SharePointClientFixture.TestListTitle,
                Url = SharePointClientFixture.TestListUrl,
                TemplateType = ListTemplateType.DocumentLibrary
            };

            await prov.ProvisionAsync(Fixture.Context);

            var output = Assert.Single(Output);
            var alreadyExists = Assert.IsType<ObjectAlreadyExists<List>>(output);

            var list = alreadyExists.Object;
            Assert.NotNull(list);

            Fixture.ClientContext.Load(
                list,
                l => l.Title,
                l => l.BaseTemplate
            );

            await Fixture.ClientContext.ExecuteQueryAsync();


            Assert.Equal(SharePointClientFixture.TestListTitle, list.Title);
            Assert.Equal((Int32)ListTemplateType.DocumentLibrary, list.BaseTemplate);
        }

        [Fact]
        public async Task Random_list_is_added()
        {
            var name = Guid.NewGuid().ToString("n");

            var prov = new HarshList()
            {
                Title = name,
                Url = "Lists/" + name,
            };

            List list = null;

            try
            {
                await prov.ProvisionAsync(Fixture.Context);

                var objectCreated = FindOutput<List>();
                Assert.IsType<ObjectCreated<List>>(objectCreated);

                list = objectCreated.Object;
                Assert.NotNull(list);

                Fixture.ClientContext.Load(
                    list,
                    l => l.Title,
                    l => l.BaseTemplate
                );

                await Fixture.ClientContext.ExecuteQueryAsync();

                Assert.NotNull(list);
                Assert.Equal(name, list.Title);
                Assert.Equal((Int32)ListTemplateType.GenericList, list.BaseTemplate);
            }
            finally
            {
                list?.DeleteObject();
                await Fixture.ClientContext.ExecuteQueryAsync();
            }
        }
    }
}
