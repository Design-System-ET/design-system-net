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
   public class loadmanagefiltersstate : GXProcedure
   {
      public loadmanagefiltersstate( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public loadmanagefiltersstate( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_UserCustomKey ,
                           out string aP1_UserCustomValue )
      {
         this.AV8UserCustomKey = aP0_UserCustomKey;
         this.AV9UserCustomValue = "" ;
         initialize();
         ExecuteImpl();
         aP1_UserCustomValue=this.AV9UserCustomValue;
      }

      public string executeUdp( string aP0_UserCustomKey )
      {
         execute(aP0_UserCustomKey, out aP1_UserCustomValue);
         return AV9UserCustomValue ;
      }

      public void executeSubmit( string aP0_UserCustomKey ,
                                 out string aP1_UserCustomValue )
      {
         this.AV8UserCustomKey = aP0_UserCustomKey;
         this.AV9UserCustomValue = "" ;
         SubmitImpl();
         aP1_UserCustomValue=this.AV9UserCustomValue;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new DesignSystem.Programs.wwpbaseobjects.loaduserkeyvalue(context ).execute(  AV8UserCustomKey, out  AV9UserCustomValue) ;
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
         AV9UserCustomValue = "";
         /* GeneXus formulas. */
      }

      private string AV8UserCustomKey ;
      private string AV9UserCustomValue ;
      private string aP1_UserCustomValue ;
   }

}
