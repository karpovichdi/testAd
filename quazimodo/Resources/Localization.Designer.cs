﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quazimodo.Resources {
    using System;
    using System.Reflection;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Localization {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Localization() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("quazimodo.Resources.Localization", typeof(Localization).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string NegativeHeader {
            get {
                return ResourceManager.GetString("NegativeHeader", resourceCulture);
            }
        }
        
        public static string PositiveHeader {
            get {
                return ResourceManager.GetString("PositiveHeader", resourceCulture);
            }
        }
        
        public static string NeutralHeader {
            get {
                return ResourceManager.GetString("NeutralHeader", resourceCulture);
            }
        }
        
        public static string Skip {
            get {
                return ResourceManager.GetString("Skip", resourceCulture);
            }
        }
        
        public static string Stop {
            get {
                return ResourceManager.GetString("Stop", resourceCulture);
            }
        }
        
        public static string Hide {
            get {
                return ResourceManager.GetString("Hide", resourceCulture);
            }
        }
        
        public static string Tap {
            get {
                return ResourceManager.GetString("Tap", resourceCulture);
            }
        }
        
        public static string AdMessageHeader {
            get {
                return ResourceManager.GetString("AdMessageHeader", resourceCulture);
            }
        }
        
        public static string AdMessage {
            get {
                return ResourceManager.GetString("AdMessage", resourceCulture);
            }
        }
    }
}