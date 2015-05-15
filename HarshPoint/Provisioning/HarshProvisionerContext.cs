﻿using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarshPoint.Provisioning
{
    public sealed class HarshProvisionerContext : Implementation.HarshProvisionerContextBase
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

        public Site Site
        {
            get { return ClientContext?.Site; }
        }

        public Web Web
        {
            get { return ClientContext?.Web; }
        }
    }
}
