﻿using Microsoft.SharePoint.Client;

namespace HarshPoint.Provisioning
{
    public sealed class HarshProvisionerContext 
        : Implementation.HarshProvisionerContextBase<HarshProvisionerContext>
    {
        public HarshProvisionerContext(ClientContext clientContext)
        {
            if (clientContext == null)
            {
                throw Error.ArgumentNull(nameof(clientContext));
            }

            ClientContext = clientContext;
        }

        public ClientContext ClientContext
        {
            get;
            private set;
        }

        public Site Site => ClientContext?.Site;

        public Web Web => ClientContext?.Web; 
    }
}
