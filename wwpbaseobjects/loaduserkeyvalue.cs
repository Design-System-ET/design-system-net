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
   public class loaduserkeyvalue : GXProcedure
   {
      public loaduserkeyvalue( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public loaduserkeyvalue( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_UserCustomizationsKey ,
                           out string aP1_UserCustomizationsValue )
      {
         this.AV17UserCustomizationsKey = aP0_UserCustomizationsKey;
         this.AV18UserCustomizationsValue = "" ;
         initialize();
         ExecuteImpl();
         aP1_UserCustomizationsValue=this.AV18UserCustomizationsValue;
      }

      public string executeUdp( string aP0_UserCustomizationsKey )
      {
         execute(aP0_UserCustomizationsKey, out aP1_UserCustomizationsValue);
         return AV18UserCustomizationsValue ;
      }

      public void executeSubmit( string aP0_UserCustomizationsKey ,
                                 out string aP1_UserCustomizationsValue )
      {
         this.AV17UserCustomizationsKey = aP0_UserCustomizationsKey;
         this.AV18UserCustomizationsValue = "" ;
         SubmitImpl();
         aP1_UserCustomizationsValue=this.AV18UserCustomizationsValue;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV18UserCustomizationsValue = AV16Session.Get(AV17UserCustomizationsKey);
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
         AV18UserCustomizationsValue = "";
         AV16Session = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV18UserCustomizationsValue ;
      private string AV17UserCustomizationsKey ;
      private IGxSession AV16Session ;
      private string aP1_UserCustomizationsValue ;
   }

}
