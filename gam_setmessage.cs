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
namespace DesignSystem.Programs {
   public class gam_setmessage : GXProcedure
   {
      public gam_setmessage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gam_setmessage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_GAMMessageType ,
                           string aP1_MessageText )
      {
         this.AV8GAMMessageType = aP0_GAMMessageType;
         this.AV9MessageText = aP1_MessageText;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( string aP0_GAMMessageType ,
                                 string aP1_MessageText )
      {
         this.AV8GAMMessageType = aP0_GAMMessageType;
         this.AV9MessageText = aP1_MessageText;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10WebSession.Set(AV8GAMMessageType, AV9MessageText);
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
         AV10WebSession = context.GetSession();
         /* GeneXus formulas. */
      }

      private string AV8GAMMessageType ;
      private string AV9MessageText ;
      private IGxSession AV10WebSession ;
   }

}
