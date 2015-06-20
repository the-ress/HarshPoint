﻿using Microsoft.SharePoint.Client;
using System.Threading.Tasks;

namespace HarshPoint.Provisioning
{
    public class HarshDateTimeField : HarshFieldProvisioner<FieldDateTime>
    {
        public DateTimeFieldFormatType? DisplayFormat
        {
            get;
            set;
        }

        public DateTimeFieldFriendlyFormatType? FriendlyDisplayFormat
        {
            get;
            set;
        }

        protected override async Task<HarshProvisionerResult> OnProvisioningAsync()
        {
            foreach (var field in FieldsResolved)
            {
                if (DisplayFormat.HasValue)
                {
                    field.DisplayFormat = DisplayFormat.Value;
                }

                if (FriendlyDisplayFormat.HasValue)
                {
                    field.FriendlyDisplayFormat = FriendlyDisplayFormat.Value;
                }

                UpdateField(field);
            }

            await ClientContext.ExecuteQueryAsync();

            return await base.OnProvisioningAsync();
        }
    }
}