using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs.wwpbaseobjects {
   public class gxdomaindvmessagestyling
   {
      private static Hashtable domain = new Hashtable();
      private static Hashtable domainMap;
      static gxdomaindvmessagestyling ()
      {
         domain["N"] = "No Specify";
         domain["bootstrap2"] = "bootstrap2";
         domain["boostrap3"] = "boostrap3";
         domain["fontawesome"] = "fontawesome";
         domain["brighttheme"] = "brighttheme";
      }

      public static string getDescription( IGxContext context ,
                                           string key )
      {
         string rtkey;
         string value;
         rtkey = ((key==null) ? "" : StringUtil.Trim( (string)(key)));
         value = (string)(domain[rtkey]==null?"":domain[rtkey]);
         return context.GetMessage( value, "") ;
      }

      public static GxSimpleCollection<string> getValues( )
      {
         GxSimpleCollection<string> value = new GxSimpleCollection<string>();
         ArrayList aKeys = new ArrayList(domain.Keys);
         aKeys.Sort();
         foreach (string key in aKeys)
         {
            value.Add(key);
         }
         return value;
      }

      public static string getValue( string key )
      {
         if(domainMap == null)
         {
            domainMap = new Hashtable();
            domainMap["NoSpecify"] = "N";
            domainMap["bootstrap2"] = "bootstrap2";
            domainMap["boostrap3"] = "boostrap3";
            domainMap["fontawesome"] = "fontawesome";
            domainMap["brighttheme"] = "brighttheme";
         }
         return (string)domainMap[key] ;
      }

   }

}
