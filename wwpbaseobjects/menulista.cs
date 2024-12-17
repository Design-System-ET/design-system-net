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
   public class menulista : GXProcedure
   {
      public menulista( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public menulista( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName> aP0_ProgramNames )
      {
         this.AV9ProgramNames = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName>( context, "ProgramName", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP0_ProgramNames=this.AV9ProgramNames;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName> executeUdp( )
      {
         execute(out aP0_ProgramNames);
         return AV9ProgramNames ;
      }

      public void executeSubmit( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName> aP0_ProgramNames )
      {
         this.AV9ProgramNames = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName>( context, "ProgramName", "DesignSystem") ;
         SubmitImpl();
         aP0_ProgramNames=this.AV9ProgramNames;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV9ProgramNames = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName>( context, "ProgramName", "DesignSystem");
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV16WWPContext) ;
         AV13name = "CategoriasWW";
         AV14description = context.GetMessage( "Categorias", "");
         AV15link = formatLink("categoriasww.aspx") ;
         /* Execute user subroutine: 'ADDPROGRAM' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV13name = "Nuevo Blog";
         AV14description = context.GetMessage( "Nuevo Blog", "");
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "newblog.aspx"+GXUtil.UrlEncode(StringUtil.RTrim("INS"));
         AV15link = formatLink("newblog.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         /* Execute user subroutine: 'ADDPROGRAM' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV13name = "NewBlogWW";
         AV14description = context.GetMessage( "Listado del Blog", "");
         AV15link = formatLink("newblogww.aspx") ;
         /* Execute user subroutine: 'ADDPROGRAM' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV13name = "Nueva Solución";
         AV14description = context.GetMessage( "Nueva Solución", "");
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "newproductos.aspx"+GXUtil.UrlEncode(StringUtil.RTrim("INS"));
         AV15link = formatLink("newproductos.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         /* Execute user subroutine: 'ADDPROGRAM' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV13name = "Listado Soluciones";
         AV14description = context.GetMessage( "Listado Soluciones", "");
         AV15link = formatLink("newproductosww.aspx") ;
         /* Execute user subroutine: 'ADDPROGRAM' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV13name = "Configuración Empresa";
         AV14description = context.GetMessage( "Configuración Empresa", "");
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "configuracionempresa.aspx"+GXUtil.UrlEncode(StringUtil.RTrim("INS"));
         AV15link = formatLink("configuracionempresa.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         /* Execute user subroutine: 'ADDPROGRAM' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'ADDPROGRAM' Routine */
         returnInSub = false;
         AV8IsAuthorized = true;
         if ( AV8IsAuthorized )
         {
            AV10ProgramName = new DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName(context);
            AV10ProgramName.gxTpr_Name = AV13name;
            AV10ProgramName.gxTpr_Description = AV14description;
            AV10ProgramName.gxTpr_Link = AV15link;
            AV9ProgramNames.Add(AV10ProgramName, 0);
         }
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
         AV9ProgramNames = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName>( context, "ProgramName", "DesignSystem");
         AV16WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV13name = "";
         AV14description = "";
         AV15link = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV10ProgramName = new DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName(context);
         /* GeneXus formulas. */
      }

      private short gxcookieaux ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private bool returnInSub ;
      private bool AV8IsAuthorized ;
      private string AV13name ;
      private string AV14description ;
      private string AV15link ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName> AV9ProgramNames ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV16WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName AV10ProgramName ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName> aP0_ProgramNames ;
   }

}
