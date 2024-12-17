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
   public class savemanagefiltersstate : GXProcedure
   {
      public savemanagefiltersstate( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public savemanagefiltersstate( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_UserCustomKey ,
                           string aP1_UserCustomValue )
      {
         this.AV9UserCustomKey = aP0_UserCustomKey;
         this.AV10UserCustomValue = aP1_UserCustomValue;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( string aP0_UserCustomKey ,
                                 string aP1_UserCustomValue )
      {
         this.AV9UserCustomKey = aP0_UserCustomKey;
         this.AV10UserCustomValue = aP1_UserCustomValue;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new DesignSystem.Programs.wwpbaseobjects.saveuserkeyvalue(context ).execute(  AV9UserCustomKey,  AV10UserCustomValue) ;
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
         /* GeneXus formulas. */
      }

      private string AV9UserCustomKey ;
      private string AV10UserCustomValue ;
   }

}
