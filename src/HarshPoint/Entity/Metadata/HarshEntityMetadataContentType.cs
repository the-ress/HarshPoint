﻿using System;
using System.Reflection;

namespace HarshPoint.Entity.Metadata
{
    internal sealed class HarshEntityMetadataContentType : HarshEntityMetadata
    {
        internal HarshEntityMetadataContentType(HarshEntityMetadataRepository repository, TypeInfo entityTypeInfo)
            : base(repository, entityTypeInfo)
        {
            ContentTypeAttribute = ObjectTypeInfo.GetCustomAttribute<ContentTypeAttribute>();

            if (ContentTypeAttribute == null)
            {
                throw Error.ArgumentOutOfRangeFormat(
                    nameof(entityTypeInfo),
                    SR.HarshEntityMetadataContentType_NoContentTypeAttribute,
                    entityTypeInfo.FullName
                );
            }

            ContentTypeId = new ContentTypeIdBuilder(ObjectTypeInfo).ToString();
        }

        public ContentTypeAttribute ContentTypeAttribute
        {
            get;
            private set;
        }

        public String ContentTypeId
        {
            get;
            private set;
        }
    }
}