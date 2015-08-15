﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarshPoint.ObjectModel
{
    internal sealed class ParameterSetBuilder
    {
        private static readonly HarshLogger Logger = HarshLog.ForContext<ParameterSetBuilder>();

        public ParameterSetBuilder(Type type)
            : this(type, null)
        {
        }

        public ParameterSetBuilder(Type type, IDefaultValuePolicy defaultValuePolicy)
            : this(new HarshObjectMetadata(type), defaultValuePolicy)
        {
        }

        public ParameterSetBuilder(HarshObjectMetadata metadata, IDefaultValuePolicy defaultValuePolicy)
        {
            if (metadata == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(metadata));
            }

            Metadata = metadata;

            DefaultParameterSetName = metadata
                .ObjectTypeInfo
                .GetCustomAttribute<DefaultParameterSetAttribute>(inherit: true)?
                .DefaultParameterSetName;

            DefaultValuePolicy = defaultValuePolicy ?? new NullDefaultValuePolicy();

            ValidateNoInvalidParameters();
        }

        private void ValidateNoInvalidParameters()
        {
            var parameterProperties = Metadata.ObjectType
                .GetRuntimeProperties()
                .Where(p => p.IsDefined(typeof(ParameterAttribute)));

            foreach (var property in parameterProperties)
            {
                var valid =
                    property.CanRead &&
                    property.CanWrite &&
                    !property.GetIndexParameters().Any() &&

                    property.GetMethod.IsPublic &&
                    property.SetMethod.IsPublic &&

                    !property.GetMethod.IsStatic &&
                    !property.SetMethod.IsStatic &&

                    !property.GetMethod.IsAbstract &&
                    !property.SetMethod.IsAbstract;

                if (!valid)
                {
                    throw Logger.Fatal.ObjectMetadata(
                        SR.HarshProvisionerMetadata_InvalidParameterProperty,
                        property.PropertyType,
                        property.DeclaringType,
                        property.Name
                    );
                }
            }
        }

        public String DefaultParameterSetName { get; }
        public IDefaultValuePolicy DefaultValuePolicy { get; }
        public HarshObjectMetadata Metadata { get; }

        public IEnumerable<ParameterSet> Build()
        {
            Logger.Debug(
                "Building parameter set metadata for {ProcessedType}",
                Metadata
            );

            Logger.Debug(
                "{ProcessedType}: Default parameter set name: {DefaultParameterSetName}",
                Metadata,
                DefaultParameterSetName
            );

            var parameters = BuildParameterMetadata().ToArray();

            Logger.Debug(
                "{ProcessedType}: All parameters: {@Parameters}",
                Metadata,
                parameters
            );

            var commonParameters = parameters
                .Where(p => p.IsCommonParameter)
                .ToArray();

            Logger.Debug(
                "{ProcessedType}: Common parameters: {@CommonParameters}",
                Metadata,
                commonParameters
            );

            var parameterSets = parameters
                .Where(p => !p.IsCommonParameter)
                .GroupBy(p => p.ParameterSetName, ParameterSet.NameComparer)
                .Select(
                    (set, index) => new ParameterSet(
                        set.Key,
                        set.Concat(commonParameters),
                        IsDefaultParameterSet(set.Key, index)
                    )
                )
                .ToArray();

            if ((DefaultParameterSetName != null) &&
                !parameterSets.Any(set => set.IsDefault))
            {
                throw Logger.Fatal.ObjectMetadata(
                    SR.HarshProvisionerMetadata_DefaultParameterSetNotFound,
                    DefaultParameterSetName,
                    Metadata
                );
            }

            if (parameterSets.Any())
            {
                Logger.Debug(
                    "{ProcessedType}: Parameter sets: {@ParameterSets}",
                    Metadata,
                    parameterSets
                );

                return parameterSets;
            }

            var implicitParameterSet = new ParameterSet(
                ParameterSet.ImplicitParameterSetName,
                commonParameters,
                isDefault: true
            );

            Logger.Debug(
                "{ProcessedType}: Implicit parameter set: {@ImplicitParameterSet}",
                Metadata,
                implicitParameterSet
            );

            return new[] { implicitParameterSet };
        }

        private Boolean IsDefaultParameterSet(String name, Int32 index)
        {
            if (DefaultParameterSetName != null)
            {
                return ParameterSet.NameComparer.Equals(
                    DefaultParameterSetName,
                    name
                );
            }

            return (index == 0);
        }

        private IEnumerable<Parameter> BuildParameterMetadata()
            => from paramAttrs in 
                   Metadata.ReadableWritableInstancePropertiesWith<ParameterAttribute>(inherit: true)

               let property = paramAttrs.Key
               where
                  !HasNonUniqueParameterSetNames(property, paramAttrs) &&
                  !IsBothCommonParameterAndInParameterSet(property, paramAttrs) &&
                  !IsMandatoryAndUnsupportedByDefaultValuePolicy(property, paramAttrs)

               from attr in paramAttrs
               select new Parameter(property, attr, DefaultValuePolicy);

        private Boolean IsMandatoryAndUnsupportedByDefaultValuePolicy(
            PropertyAccessor property, 
            IEnumerable<ParameterAttribute> attributes
        )
        {
            if (!DefaultValuePolicy.SupportsType(property.PropertyTypeInfo) &&
                attributes.Any(a => a.Mandatory))
            {
                throw Logger.Fatal.ObjectMetadata(
                    SR.HarshProvisionerMetadata_MandatoryTypeNotSupportedByDefaultValuePolicy,
                    property.DeclaringType,
                    property.Name,
                    property.PropertyType,
                    DefaultValuePolicy.GetType()
                );
            }

            return false;
        }

        private static Boolean IsBothCommonParameterAndInParameterSet(
            PropertyAccessor property, 
            IEnumerable<ParameterAttribute> attributes
        )
        {
            if (attributes.Any(a => a.ParameterSetName == null))
            {
                if (attributes.Count() > 1)
                {
                    throw Logger.Fatal.ObjectMetadata(
                        SR.HarshProvisionerMetadata_ParameterBothCommonAndInSet,
                        property.DeclaringType,
                        property.Name
                    );
                }
            }

            return false;
        }

        private static Boolean HasNonUniqueParameterSetNames(
            PropertyAccessor property,
            IEnumerable<ParameterAttribute> attributes
        )
        {
            var nonUniqueParameterSetNames = attributes
                .GroupBy(p => p.ParameterSetName, ParameterSet.NameComparer)
                .Where(set => set.Count() > 1)
                .Select(set => set.Key);

            if (nonUniqueParameterSetNames.Any())
            {
                throw Logger.Fatal.ObjectMetadata(
                    SR.HarshProvisionerMetadata_MoreParametersWithSameSet,
                    property.DeclaringType,
                    property.Name,
                    String.Join(
                        ", ",
                        nonUniqueParameterSetNames.Select(
                            set => '"' + set + '"'
                        )
                    )
                );
            }

            return false;
        }
    }
}