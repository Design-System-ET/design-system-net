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
using GeneXus.Http.Server;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs.wwpbaseobjects {
   public class wwp_downloadreport : GXWebProcedure
   {
      public override void webExecute( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         initialize();
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetNextPar( );
         }
         if ( GxWebError == 0 )
         {
            ExecutePrivate();
         }
         cleanup();
      }

      public wwp_downloadreport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_downloadreport( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( )
      {
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9fileName = AV12webSession.Get("WWPExportFilePath");
         AV11name = AV12webSession.Get("WWPExportFileName");
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV9fileName)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV11name)) )
         {
            AV12webSession.Remove("WWPExportFilePath");
            AV12webSession.Remove("WWPExportFileName");
            if ( ! context.isAjaxRequest( ) )
            {
               AV10httpResponse.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            if ( ! context.isAjaxRequest( ) )
            {
               AV10httpResponse.AppendHeader("X-Frame-Options", "deny");
            }
            if ( ! context.isAjaxRequest( ) )
            {
               AV10httpResponse.AppendHeader("Type-Options", "nosniff");
            }
            if ( ! context.isAjaxRequest( ) )
            {
               AV10httpResponse.AppendHeader("Content-Disposition", "attachment;filename="+AV11name);
            }
            AV10httpResponse.AddFile(AV9fileName);
         }
         else
         {
            AV10httpResponse.AddString("Not found");
         }
         if ( context.WillRedirect( ) )
         {
            context.Redirect( context.wjLoc );
            context.wjLoc = "";
         }
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         base.cleanup();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         GXKey = "";
         gxfirstwebparm = "";
         AV9fileName = "";
         AV12webSession = context.GetSession();
         AV11name = "";
         AV10httpResponse = new GxHttpResponse( context);
         /* GeneXus formulas. */
      }

      private short gxcookieaux ;
      private short nGotPars ;
      private short GxWebError ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private bool entryPointCalled ;
      private string AV9fileName ;
      private string AV11name ;
      private IGxSession AV12webSession ;
      private GxHttpResponse AV10httpResponse ;
   }

}
