using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs.wwpbaseobjects {
   public class saveuserkeyvalue : GXProcedure
   {
      public saveuserkeyvalue( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public saveuserkeyvalue( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_UserCustomizationsKey ,
                           string aP1_UserCustomizationsValue )
      {
         this.AV16UserCustomizationsKey = aP0_UserCustomizationsKey;
         this.AV17UserCustomizationsValue = aP1_UserCustomizationsValue;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( string aP0_UserCustomizationsKey ,
                                 string aP1_UserCustomizationsValue )
      {
         this.AV16UserCustomizationsKey = aP0_UserCustomizationsKey;
         this.AV17UserCustomizationsValue = aP1_UserCustomizationsValue;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV17UserCustomizationsValue)) )
         {
            AV15Session.Remove(AV16UserCustomizationsKey);
         }
         else
         {
            AV15Session.Set(AV16UserCustomizationsKey, AV17UserCustomizationsValue);
         }
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         AV15Session = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV17UserCustomizationsValue ;
      private string AV16UserCustomizationsKey ;
      private IGxSession AV15Session ;
   }

}
