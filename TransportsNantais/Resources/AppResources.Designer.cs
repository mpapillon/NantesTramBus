﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransportsNantais.Resources {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TransportsNantais.Resources.AppResources", typeof(AppResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à .
        /// </summary>
        public static string ApplicationId {
            get {
                return ResourceManager.GetString("ApplicationId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à NANTES TRAM &amp; BUS.
        /// </summary>
        public static string ApplicationTitle {
            get {
                return ResourceManager.GetString("ApplicationTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à .
        /// </summary>
        public static string AuthenticationToken {
            get {
                return ResourceManager.GetString("AuthenticationToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à LeftToRight.
        /// </summary>
        public static string ResourceFlowDirection {
            get {
                return ResourceManager.GetString("ResourceFlowDirection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à fr-FR.
        /// </summary>
        public static string ResourceLanguage {
            get {
                return ResourceManager.GetString("ResourceLanguage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à https://www.tan.fr/jsp/fiche_pagelibre.jsp?CODE=21852478&amp;LANGUE=0&amp;RH=ACCUEIL&amp;RF=1373631635261.
        /// </summary>
        public static string TanPrivacyLink {
            get {
                return ResourceManager.GetString("TanPrivacyLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Version 0.0.0.2 :
        ///    - Correction d&apos;un problème avec le bouton 
        ///	  &quot;directions&quot; des détails d&apos;une ligne.
        ///	- Correction d&apos;un problème lors de 
        ///	  l&apos;épinglage d&apos;un arrêt.
        ///	- Le rafraichissement de la position
        ///	  géographique est automatique.
        ///	- Avertissement concernant les données
        ///	  des lignes.
        ///	- Historique des versions dans &quot;a propos&quot;.
        ///
        ///Version 0.0.0.1 :
        ///    - Rechercher un arrêt.
        ///    - Voir ceux qui se trouvent proche de vous.
        ///    - Connaître les prochains départs.
        ///    - Consulter le traje [le reste de la chaîne a été tronqué]&quot;;.
        /// </summary>
        public static string VersionHistory {
            get {
                return ResourceManager.GetString("VersionHistory", resourceCulture);
            }
        }
    }
}
