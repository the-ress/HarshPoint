﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HarshPoint.Provisioning.Implementation
{
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class NestedResolvable<T1, T2, TContext, TSelf> :
        ResolvableChain,
        
        IResolvableChainElement<IGrouping<T1, T2>>,
        IResolveSingle<IGrouping<T1, T2>>,
        IResolve<IGrouping<T1, T2>>,    
        
        IResolvableChainElement<T2>,
        IResolveSingle<T2>,
        IResolve<T2>

        where TContext : HarshProvisionerContextBase
        where TSelf : NestedResolvable<T1, T2, TContext, TSelf>
    {
        protected NestedResolvable(IResolve<T1> parent)
        {
            if (parent == null)
            {
                throw Error.ArgumentNull(nameof(parent));
            }

            Parents = parent;
        }

        public IResolve<T1> Parents
        {
            get;
            private set;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public TSelf And(IResolvableChainElement<IGrouping<T1, T2>> other)
        {
            if (other == null)
            {
                throw Error.ArgumentNull(nameof(other));
            }

            return (TSelf)And((ResolvableChain)(other));
        }
        
        public TSelf And(IResolvableChainElement<T2> other)
        {
            if (other == null)
            {
                throw Error.ArgumentNull(nameof(other));
            }

            return (TSelf)And((ResolvableChain)(other));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected abstract Task<IEnumerable<T2>> ResolveChainElement(TContext context, T1 parent);

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        async Task<IEnumerable<T2>> IResolvableChainElement<T2>.ResolveChainElement(HarshProvisionerContextBase context)
            => (await ResolveChainElement(context)).SelectMany(g => g);

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        async Task<IEnumerable<IGrouping<T1, T2>>> IResolvableChainElement<IGrouping<T1, T2>>.ResolveChainElement(HarshProvisionerContextBase context)
            => await ResolveChainElement(context);

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        Task<IEnumerable<T2>> IResolve<T2>.ResolveAsync(HarshProvisionerContextBase context)
            => ResolveChain<T2>(context);

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        Task<IEnumerable<IGrouping<T1, T2>>> IResolve<IGrouping<T1, T2>>.ResolveAsync(HarshProvisionerContextBase context)
            => ResolveChain<IGrouping<T1, T2>>(context);

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        Task<T2> IResolveSingle<T2>.ResolveSingleAsync(HarshProvisionerContextBase context)
            => ResolveChainSingle<T2>(context);

        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        Task<IGrouping<T1, T2>> IResolveSingle<IGrouping<T1, T2>>.ResolveSingleAsync(HarshProvisionerContextBase context)
            => ResolveChainSingle<IGrouping<T1, T2>>(context);
            
        private async Task<IEnumerable<IGrouping<T1, T2>>> ResolveChainElement(HarshProvisionerContextBase context)
        {
            var typedContext = ValidateContext<TContext>(context);

            var tasks = from parent in await Parents.ResolveAsync(context)
                        select ResolveChildren(typedContext, parent);

            return await Task.WhenAll(tasks);
        }

        private async Task<IGrouping<T1, T2>> ResolveChildren(TContext context, T1 parent)
        {
            return ResolvedGrouping.Create(
                parent,
                await ResolveChainElement(context, parent)
            );
        }
    }
}