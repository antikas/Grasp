﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Grasp.Knowledge {
    using System;
    
    
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Grasp.Knowledge.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to {0} ({1}).
        /// </summary>
        internal static string Field {
            get {
                return ResourceManager.GetString("Field", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Get.
        /// </summary>
        internal static string GetPrefix {
            get {
                return ResourceManager.GetString("GetPrefix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Getter method &quot;{0}&quot; does not start with &quot;Get&quot;.
        /// </summary>
        internal static string GetterMethodNameDoesNotStartWithGet {
            get {
                return ResourceManager.GetString("GetterMethodNameDoesNotStartWithGet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Getter method &quot;{0}&quot; is not declared on owning type {1}.
        /// </summary>
        internal static string GetterMethodNotDeclaredOnOwningType {
            get {
                return ResourceManager.GetString("GetterMethodNotDeclaredOnOwningType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Getter method &quot;{0}&quot; is not static.
        /// </summary>
        internal static string GetterMethodNotStatic {
            get {
                return ResourceManager.GetString("GetterMethodNotStatic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Member &quot;{0}&quot; is not a property.
        /// </summary>
        internal static string MemberIsNotProperty {
            get {
                return ResourceManager.GetString("MemberIsNotProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Member &quot;{0}&quot; is not declared on owning type {1}.
        /// </summary>
        internal static string MemberNotDeclaredOnOwningType {
            get {
                return ResourceManager.GetString("MemberNotDeclaredOnOwningType", resourceCulture);
            }
        }
    }
}
