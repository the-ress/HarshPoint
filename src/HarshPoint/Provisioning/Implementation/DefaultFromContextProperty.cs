﻿using HarshPoint.ObjectModel;
using HarshPoint.Reflection;
using System;
using System.Reflection;

namespace HarshPoint.Provisioning.Implementation
{
    internal sealed class DefaultFromContextProperty
    {
        public DefaultFromContextProperty(
            PropertyAccessor accessor,
            DefaultFromContextAttribute attribute
        )
        {
            if (accessor == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(accessor));
            }

            if (attribute == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(attribute));
            }

            Accessor = accessor;
            Attribute = attribute;
            ResolvedPropertyInfo = ResolvedPropertyTypeInfo.TryParse(PropertyTypeInfo);

            ValidateIsNullable();
            ValidateTagTypeIsIDefaultFromContextTag();
            ValidateWithTagNotResolvable();
        }

        public PropertyAccessor Accessor { get; }
        public DefaultFromContextAttribute Attribute { get; }
        public String Name => PropertyInfo.Name;
        public PropertyInfo PropertyInfo => Accessor.PropertyInfo;
        public Type PropertyType => PropertyInfo.PropertyType;
        public TypeInfo PropertyTypeInfo => PropertyType.GetTypeInfo();
        public ResolvedPropertyTypeInfo ResolvedPropertyInfo { get; }
        public Type TagType => Attribute.TagType;

        private void ValidateIsNullable()
        {
            if (!PropertyTypeInfo.IsNullable())
            {
                throw Logger.Fatal.ObjectMetadata(
                    SR.HarshProvisionerMetadata_NoValueTypeDefaultFromContext,
                    PropertyInfo.DeclaringType,
                    PropertyInfo.Name,
                    PropertyInfo.PropertyType
                );
            }
        }

        private void ValidateWithTagNotResolvable()
        {
            if ((TagType != null) && (ResolvedPropertyInfo != null))
            {
                throw Logger.Fatal.ObjectMetadata(
                    SR.HarshProvisionerMetadata_NoTagTypesOnResolvers,
                    PropertyInfo.DeclaringType,
                    PropertyInfo.Name
                );
            }
        }

        private void ValidateTagTypeIsIDefaultFromContextTag()
        {
            if (TagType == null)
            {
                return;
            }

            if (!IDefaultFromContextTagTypeInfo.IsAssignableFrom(TagType.GetTypeInfo()))
            {
                throw Logger.Fatal.ObjectMetadata(
                    SR.HarshProvisionerMetadata_TagTypeNotAssignableFromIDefaultFromContextTag,
                    TagType,
                    PropertyInfo.DeclaringType,
                    PropertyInfo.Name
                );
            }
        }

        private static readonly TypeInfo IDefaultFromContextTagTypeInfo
            = typeof(IDefaultFromContextTag).GetTypeInfo();

        private static readonly HarshLogger Logger = HarshLog.ForContext<DefaultFromContextProperty>();
    }
}
