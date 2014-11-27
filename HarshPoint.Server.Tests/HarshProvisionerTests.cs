﻿using HarshPoint.Server.Provisioning;
using Microsoft.SharePoint;
using Moq;
using Moq.Protected;
using System;
using Xunit;

namespace HarshPoint.Server.Tests.UnitTests
{
    public class HarshProvisionerTests : IDisposable, IUseFixture<SharePointFixture>
    {
        public SharePointFixture SPFixture
        {
            get;
            set;
        }

        public void SetFixture(SharePointFixture data)
        {
            SPFixture = data;
        }

        public void Dispose()
        {
            SPFixture.Dispose();
            SPFixture = null;
        }

        [Fact]
        public void Provision_calls_Initialize()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("Initialize");
            mock.Object.Provision();
            mock.Verify();
        }
    
        [Fact]
        public void Provision_calls_OnProvisioning()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("OnProvisioning");
            mock.Object.Provision();
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
                mock.Object.Provision();
            });

            mock.Verify();
        }

        [Fact]
        public void Unprovision_calls_Initialize()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("Initialize");
            mock.Object.Unprovision();
            mock.Verify();
        }

        [Fact]
        public void Unprovision_calls_OnUnprovisioning()
        {
            var mock = new Mock<HarshServerProvisioner>();

            mock.Protected().Setup("OnUnprovisioning");
            mock.Object.Unprovision();
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
                mock.Object.Unprovision();
            });

            mock.Verify();
        }

        [Fact]
        public void Set_Web_sets_Site()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.Web = SPFixture.Web;
            Assert.Equal(SPFixture.Site, p.Site);
        }

        [Fact]
        public void Set_Web_sets_WebApplication()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.Web = SPFixture.Web;
            Assert.Equal(SPFixture.WebApplication, p.WebApplication);
        }

        [Fact]
        public void Set_Web_sets_Farm()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.Web = SPFixture.Web;
            Assert.Equal(SPFixture.Farm, p.Farm);
        }

        [Fact]
        public void Set_Site_clears_Web()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.Site = SPFixture.Site;
            Assert.Equal(null, p.Web);
        }

        [Fact]
        public void Set_Site_sets_WebApplication()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.Site = SPFixture.Site;
            Assert.Equal(SPFixture.WebApplication, p.WebApplication);
        }

        [Fact]
        public void Set_Site_sets_Farm()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.Site = SPFixture.Site;
            Assert.Equal(SPFixture.Farm, p.Farm);
        }

        [Fact]
        public void Set_WebApplication_clears_Web()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.WebApplication = SPFixture.WebApplication;
            Assert.Equal(null, p.Web);
        }

        [Fact]
        public void Set_WebApplication_clears_Site()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.WebApplication = SPFixture.WebApplication;
            Assert.Equal(null, p.Site);
        }

        [Fact]
        public void Set_WebApplication_sets_Farm()
        {
            var p = Mock.Of<HarshServerProvisioner>();
            p.WebApplication = SPFixture.WebApplication;
            Assert.Equal(SPFixture.Farm, p.Farm);
        }
    }
}
