﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HarshPoint.Server
{


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SR
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SR()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HarshPoint.Server.SR", typeof(SR).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The web &apos;{0}&apos; is not a publishing web..
        /// </summary>
        internal static string HarshProvisionerPublishing_NotAPublishingWeb
        {
            get
            {
                return ResourceManager.GetString("HarshProvisionerPublishing_NotAPublishingWeb", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The Feature property of the SPFeatureReceiverProperties argument cannot be null..
        /// </summary>
        internal static string HarshServerProvisionerContext_PropertiesFeatureNull
        {
            get
            {
                return ResourceManager.GetString("HarshServerProvisionerContext_PropertiesFeatureNull", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Don&apos;t know how to convert an instance of type &apos;{0}&apos; to a HarshServerProvisioner..
        /// </summary>
        internal static string HarshServerProvisionerConverter_CannotConvert
        {
            get
            {
                return ResourceManager.GetString("HarshServerProvisionerConverter_CannotConvert", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to HarshServerProvisioner has to specify an SPWeb or an SPSite in order to forward to a client-side provisioner implementation..
        /// </summary>
        internal static string HarshServerProvisionerConverter_OnlyWebAndSiteSupported
        {
            get
            {
                return ResourceManager.GetString("HarshServerProvisionerConverter_OnlyWebAndSiteSupported", resourceCulture);
            }
        }
    }
}
