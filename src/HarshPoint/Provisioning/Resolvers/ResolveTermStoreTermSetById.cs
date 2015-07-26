﻿using HarshPoint.Provisioning.Implementation;
using Microsoft.SharePoint.Client.Taxonomy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarshPoint.Provisioning.Resolvers
{
    public sealed class ResolveTermStoreTermSetById
        : Implementation.OldNestedResolvable<TermStore, TermSet, Guid, HarshProvisionerContext, ResolveTermStoreTermSetById>
    {
        public ResolveTermStoreTermSetById(IResolveOld<TermStore> termStore, IEnumerable<Guid> ids)
            : base(termStore, ids)
        {
        }

        protected override Task<IEnumerable<TermSet>> ResolveChainElement(ResolveContext<HarshProvisionerContext> context, TermStore parent)
        {
            if (context == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(context));
            }

            if (parent == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(parent));
            }

            return this.ResolveQuery(
                ClientObjectResolveQuery.TermStoreTermSetById,
                context,
                parent
            );
        }

        private static readonly HarshLogger Logger = HarshLog.ForContext<ResolveTermStoreTermSetById>();
    }
}
