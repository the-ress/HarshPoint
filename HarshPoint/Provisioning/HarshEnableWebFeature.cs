﻿using Microsoft.SharePoint.Client;
using System;
using System.Threading.Tasks;

namespace HarshPoint.Provisioning
{
    public sealed class HarshEnableWebFeature : HarshProvisioner
    {
        public Guid FeatureId
        {
            get;
            set;
        }

        public FeatureDefinitionScope FeatureDefinitionScope
        {
            get;
            set;
        }

        public Boolean Force
        {
            get;
            set;
        }

        protected override async Task OnProvisioningAsync()
        {
            Web.Features.Add(FeatureId, Force, FeatureDefinitionScope);

            await ClientContext.ExecuteQueryAsync();
            await base.OnProvisioningAsync();
        }
    }
}
