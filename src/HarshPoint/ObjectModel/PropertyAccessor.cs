﻿using HarshPoint.Reflection;
using System;
using System.Reflection;

namespace HarshPoint.ObjectModel
{
    public sealed class PropertyAccessor
    {
        public PropertyAccessor(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw Logger.Fatal.ArgumentNull(nameof(propertyInfo));
            }

            PropertyInfo = propertyInfo;
            Getter = propertyInfo.MakeGetter();
            Setter = propertyInfo.MakeSetter();
        }

        public PropertyInfo PropertyInfo { get; }
        public Action<Object, Object> Setter { get; }
        public Func<Object, Object> Getter { get; }

        public Type DeclaringType => PropertyInfo.DeclaringType;
        public String Name => PropertyInfo.Name;
        public Type PropertyType => PropertyInfo.PropertyType;
        public TypeInfo PropertyTypeInfo => PropertyType.GetTypeInfo();

        public override String ToString() => PropertyInfo.ToString();

        private static readonly HarshLogger Logger = HarshLog.ForContext<PropertyAccessor>();
    }
}