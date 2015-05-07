﻿using HarshPoint.Server.Provisioning;
using Microsoft.SharePoint;
using Moq;
using Moq.Protected;
using System;
using Xunit;

namespace HarshPoint.Server.Tests.UnitTests
{
    public class HarshServerProvisionerTests : IUseFixture<SharePointServerFixture>
    {
        public SharePointServerFixture SPFixture
        {
            get;
            set;
        }

        public void SetFixture(SharePointServerFixture data)
        {
            SPFixture = data;
        }

        [Fact]
        public void Provision_calls_Initialize()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("Initialize");
            mock.Object.Provision(SPFixture.WebContext);
            mock.Verify();
        }
    
        [Fact]
        public void Provision_calls_OnProvisioning()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("OnProvisioning");
            mock.Object.Provision(SPFixture.WebContext);
            mock.Verify();
        }

        [Fact]
        public void Provision_always_calls_Complete()
        {
            var mock = new Mock<HarshServerProvisioner>();
            
            mock.Protected().Setup("OnProvisioning").Throws<Exception>();
            mock.Protected().Setup("Complete");

            Assert.Throws<Exception>(delegate
            {
                mock.Object.Provision(SPFixture.WebContext);
            });

            mock.Verify();
        }

        [Fact]
        public void Unprovision_calls_Initialize()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("Initialize");
            mock.Object.Unprovision(SPFixture.WebContext);
            mock.Verify();
        }

        [Fact]
        public void Unprovision_calls_OnUnprovisioning()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("OnUnprovisioning");
            mock.Object.Unprovision(SPFixture.WebContext);
            mock.Verify();
        }

        [Fact]
        public void Unprovision_always_calls_Complete()
        {
            var mock = new Mock<HarshServerProvisioner>();
            
            mock.Protected().Setup("OnUnprovisioning").Throws<Exception>();
            mock.Protected().Setup("Complete");

            Assert.Throws<Exception>(delegate
            {
                mock.Object.Unprovision(SPFixture.WebContext);
            });

            mock.Verify();
        }

    }
}
