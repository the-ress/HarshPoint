﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HarshPoint {
    using System;
    using System.Reflection;
    
    
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
    internal class SR {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SR() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HarshPoint.SR", typeof(SR).GetTypeInfo().Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The content type ID could not be determined because no parent of the specified type &apos;{0}&apos; defines an absolute content type ID..
        /// </summary>
        internal static string ContentTypeIdBuilder_NoAbsoluteIDInHierarchy {
            get {
                return ResourceManager.GetString("ContentTypeIdBuilder_NoAbsoluteIDInHierarchy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The content type ID could not be determined because the type &apos;{0}&apos; has no ContentTypeAttribute..
        /// </summary>
        internal static string ContentTypeIdBuilder_NoContentTypeAttribute {
            get {
                return ResourceManager.GetString("ContentTypeIdBuilder_NoContentTypeAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The content type ID cannot be determined because the type &apos;{0}&apos;, from which the specified type &apos;{1}&apos; inherits, has no ContentTypeAttribute..
        /// </summary>
        internal static string ContentTypeIdBuilder_NoContentTypeAttributeBaseClass {
            get {
                return ResourceManager.GetString("ContentTypeIdBuilder_NoContentTypeAttributeBaseClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value cannot be null or whitespace-only string..
        /// </summary>
        internal static string Error_ArgumentNullOrWhitespace {
            get {
                return ResourceManager.GetString("Error_ArgumentNullOrWhitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified object &apos;{0}&apos; is not a subtype of &apos;{1}&apos;..
        /// </summary>
        internal static string Error_ObjectNotAssignableTo {
            get {
                return ResourceManager.GetString("Error_ObjectNotAssignableTo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified type &apos;{0}&apos; is not assignable from &apos;{1}&apos;..
        /// </summary>
        internal static string Error_TypeNotAssignableFrom {
            get {
                return ResourceManager.GetString("Error_TypeNotAssignableFrom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A MemberExpression could not be found in the specified Expression..
        /// </summary>
        internal static string ExpressionExtensions_MemberExpressionNotFound {
            get {
                return ResourceManager.GetString("ExpressionExtensions_MemberExpressionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field ID cannot be an empty Guid..
        /// </summary>
        internal static string FieldAttribute_EmptyGuid {
            get {
                return ResourceManager.GetString("FieldAttribute_EmptyGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified string is not a valid Guid..
        /// </summary>
        internal static string FieldAttribute_InvalidGuid {
            get {
                return ResourceManager.GetString("FieldAttribute_InvalidGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified sequence cannot be empty..
        /// </summary>
        internal static string HarshCompositeProvisioner_SequenceEmpty {
            get {
                return ResourceManager.GetString("HarshCompositeProvisioner_SequenceEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified content type ID &apos;{0}&apos; cannot be appended to &apos;{1}&apos;, because it is already an absolute content type ID...
        /// </summary>
        internal static string HarshContentTypeId_CannotAppendAbsoluteCTId {
            get {
                return ResourceManager.GetString("HarshContentTypeId_CannotAppendAbsoluteCTId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified string &apos;{0}&apos; is not a valid content type ID. Expected a 32-character ID after the &quot;00&quot; separator..
        /// </summary>
        internal static string HarshContentTypeId_Expected_32chars_ID_after_00 {
            get {
                return ResourceManager.GetString("HarshContentTypeId_Expected_32chars_ID_after_00", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified string &apos;{0}&apos; contains characters illegal in a content type ID..
        /// </summary>
        internal static string HarshContentTypeId_IllegalCharacters {
            get {
                return ResourceManager.GetString("HarshContentTypeId_IllegalCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified string &apos;{0}&apos; is not a vaild content type ID, its length isn&apos;t an even number..
        /// </summary>
        internal static string HarshContentTypeId_NotEven {
            get {
                return ResourceManager.GetString("HarshContentTypeId_NotEven", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified string &quot;00&quot; is used to separate content type ID parts and therefore cannot be used as a valid relative content type ID..
        /// </summary>
        internal static string HarshContentTypeId_RelCTId_00_OutOfRange {
            get {
                return ResourceManager.GetString("HarshContentTypeId_RelCTId_00_OutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified relative content type ID &apos;{0}&apos; is invalid. A relative content type ID is either a 2 or 32 character long hexadecimal string..
        /// </summary>
        internal static string HarshContentTypeId_RelCTId_OutOfRange {
            get {
                return ResourceManager.GetString("HarshContentTypeId_RelCTId_OutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This HarshEntityMetadata instance doesn&apos;t belong to a HarshEntityMetadataRepository..
        /// </summary>
        internal static string HarshEntityMetadata_DoesntBelongToARepo {
            get {
                return ResourceManager.GetString("HarshEntityMetadata_DoesntBelongToARepo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified type &apos;{0}&apos; is not a subclass of HarshEntity..
        /// </summary>
        internal static string HarshEntityMetadata_TypeNotAnEntity {
            get {
                return ResourceManager.GetString("HarshEntityMetadata_TypeNotAnEntity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified type &apos;{0}&apos; has no ContentTypeAttribute..
        /// </summary>
        internal static string HarshEntityMetadataContentType_NoContentTypeAttribute {
            get {
                return ResourceManager.GetString("HarshEntityMetadataContentType_NoContentTypeAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field cannot be added because FieldSchemaXml is null..
        /// </summary>
        internal static string HarshFieldProvisioner_SchemaXmlNotSet {
            get {
                return ResourceManager.GetString("HarshFieldProvisioner_SchemaXmlNotSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HarshFieldSchemaXmlTransformer.Transform cannot be null..
        /// </summary>
        internal static string HarshFieldProvisioner_TransformNull {
            get {
                return ResourceManager.GetString("HarshFieldProvisioner_TransformNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The FieldId cannot be an empty Guid..
        /// </summary>
        internal static string HarshFieldProvisionerBase_FieldIdEmpty {
            get {
                return ResourceManager.GetString("HarshFieldProvisionerBase_FieldIdEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} cannot be an empty Guid..
        /// </summary>
        internal static string HarshFieldSchemaXmlProvisioner_PropertyEmptyGuid {
            get {
                return ResourceManager.GetString("HarshFieldSchemaXmlProvisioner_PropertyEmptyGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} cannot be null or a whitespace-only string..
        /// </summary>
        internal static string HarshFieldSchemaXmlProvisioner_PropertyWhiteSpace {
            get {
                return ResourceManager.GetString("HarshFieldSchemaXmlProvisioner_PropertyWhiteSpace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} resolved in more than one result..
        /// </summary>
        internal static string ResolvableChain_ManyResults {
            get {
                return ResourceManager.GetString("ResolvableChain_ManyResults", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} resolved in no results..
        /// </summary>
        internal static string ResolvableChain_NoResult {
            get {
                return ResourceManager.GetString("ResolvableChain_NoResult", resourceCulture);
            }
        }
    }
}
