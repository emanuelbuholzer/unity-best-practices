﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BestPracticeChecker.Resources {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BestPracticeChecker.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
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
        ///   Sucht eine lokalisierte Zeichenfolge, die Correctness ähnelt.
        /// </summary>
        internal static string CategoryCorrectness {
            get {
                return ResourceManager.GetString("CategoryCorrectness", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Performance ähnelt.
        /// </summary>
        internal static string CategoryPerformance {
            get {
                return ResourceManager.GetString("CategoryPerformance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Type Safety ähnelt.
        /// </summary>
        internal static string CategoryTypeSafety {
            get {
                return ResourceManager.GetString("CategoryTypeSafety", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Use UnityEngine.Debug Statements only with a Conditional Attribute to avoid having debug output in the release build. ähnelt.
        /// </summary>
        internal static string ConditionalDebugDescription {
            get {
                return ResourceManager.GetString("ConditionalDebugDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Use UnityEngine.Debug Statements only with a Conditional Attribute. ähnelt.
        /// </summary>
        internal static string ConditionalDebugMessageFormat {
            get {
                return ResourceManager.GetString("ConditionalDebugMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die ConditionalDebug ähnelt.
        /// </summary>
        internal static string ConditionalDebugTitle {
            get {
                return ResourceManager.GetString("ConditionalDebugTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Tags sollten nur mit Konstanten referenziert werden. ähnelt.
        /// </summary>
        internal static string ConstTagsDescription {
            get {
                return ResourceManager.GetString("ConstTagsDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Tags sollten nur mit Konstanten referenziert werden. ähnelt.
        /// </summary>
        internal static string ConstTagsMessageFormat {
            get {
                return ResourceManager.GetString("ConstTagsMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die ConstTags ähnelt.
        /// </summary>
        internal static string ConstTagsTitle {
            get {
                return ResourceManager.GetString("ConstTagsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die GetComponent() und GetComponents() sollte aus Performanzgründen nie in der Update() Methode aufgerufen werden, da sie rechenintensiv sind und dies in jedem Frame ausgeführt wird. ähnelt.
        /// </summary>
        internal static string GetComponentDescription {
            get {
                return ResourceManager.GetString("GetComponentDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die GetComponent() und GetComponents() sollte aus Performanzgründen nie in der Update() Methode aufgerufen werden. ähnelt.
        /// </summary>
        internal static string GetComponentMessageFormat {
            get {
                return ResourceManager.GetString("GetComponentMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die GetComponent Aufrufe in Update() vermeiden ähnelt.
        /// </summary>
        internal static string GetComponentTitle {
            get {
                return ResourceManager.GetString("GetComponentTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Replace this methods, or at least use Ordinals  to prevent performanceproblems  ähnelt.
        /// </summary>
        internal static string IneffStringApiDescription {
            get {
                return ResourceManager.GetString("IneffStringApiDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Use String.StartsWith and String.EndsWith with custommethods ähnelt.
        /// </summary>
        internal static string IneffStringApiMessageFormat {
            get {
                return ResourceManager.GetString("IneffStringApiMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die InefficientStringAPI ähnelt.
        /// </summary>
        internal static string IneffStringApiTitle {
            get {
                return ResourceManager.GetString("IneffStringApiTitle", resourceCulture);
            }
        }
    }
}
