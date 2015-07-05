﻿using Xunit;
using Xunit.Abstractions;

namespace HarshPoint.Tests
{
    public abstract class SharePointClientTest :
        SeriloggedTest, 
        IClassFixture<SharePointClientFixture>
     //   IClassFixture<SystemNetLoggingFixture>
    {
        public SharePointClientTest(SharePointClientFixture fixture, ITestOutputHelper output) 
            : base(output)
        {
            Fixture = fixture;
        }

        public SharePointClientFixture Fixture { get; private set; }
    }
}
