﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HarshPoint.Provisioning.Implementation
{
    public abstract class IdentifierResolveBuilder<TResult, TContext, TIdentifier> :
        ResolveBuilder<TResult, TContext>
        where TContext : class, IResolveContext
    {
        private static readonly HarshLogger Logger = HarshLog.ForContext(typeof(IdentifierResolveBuilder<,,>));

        protected IdentifierResolveBuilder(
            IResolveBuilder<TResult, TContext> parent,
            IEnumerable<TIdentifier> identifiers
        )
            : this(parent, identifiers, null)
        {
        }

        protected IdentifierResolveBuilder(
            IResolveBuilder<TResult, TContext> parent,
            IEnumerable<TIdentifier> identifiers,
            IEqualityComparer<TIdentifier> identifierComparer
        )
        {
            if (parent == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(parent));
            }

            if (identifiers == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(identifiers));
            }

            if (identifierComparer == null)
            {
                identifierComparer = EqualityComparer<TIdentifier>.Default;
            }

            Parent = parent;

            Identifiers = new HashSet<TIdentifier>(
                identifiers, identifierComparer
            );
        }

        public IResolveBuilder<TResult, TContext> Parent { get; private set; }

        public HashSet<TIdentifier> Identifiers { get; private set; }

        public IEqualityComparer<TIdentifier> IdentifierComparer => Identifiers.Comparer;


        protected sealed override void InitializeContext(TContext context)
        {
            InitializeContextBeforeParent(context);
            Parent.InitializeContext(context);
        }

        protected virtual void InitializeContextBeforeParent(TContext context)
        {
        }

        protected override Object Initialize(TContext context)
            => Parent.Initialize(context);

        protected abstract TIdentifier GetIdentifier(TResult result);

        protected override IEnumerable ToEnumerable(Object state, TContext context)
        {
            var items = Parent.ToEnumerable(state, context);

            var byId = items.ToImmutableDictionaryFirstWins(
                item => GetIdentifier(NestedResolveResult.Unpack<TResult>(item)),
                item => item,
                IdentifierComparer
            );

            foreach (var id in Identifiers)
            {
                Object value;

                if (byId.TryGetValue(id, out value))
                {
                    yield return value;
                }
                else
                {
                    context.AddFailure(this, id);
                }
            }
        }
    }
}
