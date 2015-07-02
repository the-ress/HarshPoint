﻿using HarshPoint.Reflection;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace HarshPoint.Provisioning.Implementation
{
    internal sealed class HarshProvisionerMetadata : HarshObjectMetadata
    {
        public HarshProvisionerMetadata(Type type)
            : base(type)
        {
            if (!HarshProvisionerBaseTypeInfo.IsAssignableFrom(ObjectTypeInfo))
            {
                throw Error.ArgumentOutOfRange_TypeNotAssignableFrom(
                    nameof(type),
                    HarshProvisionerBaseTypeInfo,
                    ObjectTypeInfo
                );
            }

            DefaultFromContextProperties =
                GetPropertiesWith<DefaultFromContextAttribute>(inherit: true)
                .Select(tuple => new DefaultFromContextPropertyInfo(tuple.Item1, tuple.Item2))
                .ToImmutableHashSet();

            UnprovisionDeletesUserData = GetDeletesUserData("OnUnprovisioningAsync");
        }

        public IImmutableSet<DefaultFromContextPropertyInfo> DefaultFromContextProperties
        {
            get;
            private set;
        }

        public Boolean UnprovisionDeletesUserData
        {
            get;
            private set;
        }

        private Boolean GetDeletesUserData(String methodName)
        {
            var method = ObjectType
                .GetRuntimeMethods()
                .Instance()
                .NonPublic()
                .Single(m =>
                    StringComparer.Ordinal.Equals(m.Name, methodName) &&
                    !m.GetParameters().Any()
                );

            return method
                .GetRuntimeBaseMethodChain()
                .Any(
                    m => !m.IsDefined(
                        typeof(NeverDeletesUserDataAttribute),
                        inherit: false
                    )
                );
        }

        private static readonly TypeInfo HarshProvisionerBaseTypeInfo = typeof(HarshProvisionerBase).GetTypeInfo();
    }
}
