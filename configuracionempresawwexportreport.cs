using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.Printer;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   public class configuracionempresawwexportreport : GXWebProcedure
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

      public configuracionempresawwexportreport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public configuracionempresawwexportreport( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
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
         M_top = 0;
         M_bot = 6;
         P_lines = (int)(66-M_bot);
         getPrinter().GxClearAttris() ;
         add_metrics( ) ;
         lineHeight = 15;
         PrtOffset = 0;
         gxXPage = 100;
         gxYPage = 100;
         setOutputFileName("ConfiguracionEmpresaWWExportReport");
         setOutputType("PDF");
         try
         {
            Gx_out = "FIL" ;
            if (!initPrinter (Gx_out, gxXPage, gxYPage, "GXPRN.INI", "", "", 2, 1, 1, 15840, 12240, 0, 1, 1, 0, 1, 1) )
            {
               cleanup();
               return;
            }
            getPrinter().setModal(false) ;
            P_lines = (int)(gxYPage-(lineHeight*6));
            Gx_line = (int)(P_lines+1);
            getPrinter().setPageLines(P_lines);
            getPrinter().setLineHeight(lineHeight);
            getPrinter().setM_top(M_top);
            getPrinter().setM_bot(M_bot);
            GXt_boolean1 = AV8IsAuthorized;
            new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "configuracionempresaww_Execute", out  GXt_boolean1) ;
            AV8IsAuthorized = GXt_boolean1;
            if ( AV8IsAuthorized )
            {
               new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
               /* Execute user subroutine: 'LOADGRIDSTATE' */
               S151 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV40Title = context.GetMessage( "Lista de Configuracion Empresa", "");
               GXt_char2 = AV64Companyname;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Name", out  GXt_char2) ;
               AV64Companyname = GXt_char2;
               GXt_char2 = AV39Phone;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Phone", out  GXt_char2) ;
               AV39Phone = GXt_char2;
               GXt_char2 = AV37Mail;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Mail", out  GXt_char2) ;
               AV37Mail = GXt_char2;
               GXt_char2 = AV41Website;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Website", out  GXt_char2) ;
               AV41Website = GXt_char2;
               GXt_char2 = AV30AddressLine1;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Address", out  GXt_char2) ;
               AV30AddressLine1 = GXt_char2;
               GXt_char2 = AV31AddressLine2;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Neighbour", out  GXt_char2) ;
               AV31AddressLine2 = GXt_char2;
               GXt_char2 = AV32AddressLine3;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_CityAndCountry", out  GXt_char2) ;
               AV32AddressLine3 = GXt_char2;
               /* Execute user subroutine: 'PRINTFILTERS' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               /* Execute user subroutine: 'PRINTCOLUMNTITLES' */
               S121 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               /* Execute user subroutine: 'PRINTDATA' */
               S131 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               /* Execute user subroutine: 'PRINTFOOTER' */
               S171 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
            }
            /* Print footer for last page */
            ToSkip = (int)(P_lines+1);
            H3D0( true, 0) ;
         }
         catch ( GeneXus.Printer.ProcessInterruptedException  )
         {
         }
         finally
         {
            /* Close printer file */
            try
            {
               getPrinter().GxEndPage() ;
               getPrinter().GxEndDocument() ;
            }
            catch ( GeneXus.Printer.ProcessInterruptedException  )
            {
            }
            endPrinter();
         }
         if ( context.WillRedirect( ) )
         {
            context.Redirect( context.wjLoc );
            context.wjLoc = "";
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'PRINTFILTERS' Routine */
         returnInSub = false;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13FilterFullText)) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "WWP_FullTextFilterDescription", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV13FilterFullText, "")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (0==AV18TFConfiguracionEmpresaId) && (0==AV19TFConfiguracionEmpresaId_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Id", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV18TFConfiguracionEmpresaId), "ZZZ9")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV26TFConfiguracionEmpresaId_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Id", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV26TFConfiguracionEmpresaId_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV19TFConfiguracionEmpresaId_To), "ZZZ9")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV21TFConfiguracionEmpresaTelefono_Sel)) )
         {
            AV29TempBoolean = (bool)(((StringUtil.StrCmp(AV21TFConfiguracionEmpresaTelefono_Sel, "<#Empty#>")==0)));
            AV21TFConfiguracionEmpresaTelefono_Sel = (AV29TempBoolean ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV21TFConfiguracionEmpresaTelefono_Sel);
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Telefono", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV21TFConfiguracionEmpresaTelefono_Sel, "")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV21TFConfiguracionEmpresaTelefono_Sel = (AV29TempBoolean ? "<#Empty#>" : AV21TFConfiguracionEmpresaTelefono_Sel);
         }
         else
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV20TFConfiguracionEmpresaTelefono)) )
            {
               H3D0( false, 19) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(context.GetMessage( "Telefono", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV20TFConfiguracionEmpresaTelefono, "")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
               Gx_OldLine = Gx_line;
               Gx_line = (int)(Gx_line+19);
            }
         }
         if ( ! ( (Convert.ToDecimal(0)==AV22TFConfiguracionEmpresaCostoPlanBasico) && (Convert.ToDecimal(0)==AV23TFConfiguracionEmpresaCostoPlanBasico_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Plan Basico", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV22TFConfiguracionEmpresaCostoPlanBasico, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV27TFConfiguracionEmpresaCostoPlanBasico_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Plan Basico", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV27TFConfiguracionEmpresaCostoPlanBasico_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV23TFConfiguracionEmpresaCostoPlanBasico_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV24TFConfiguracionEmpresaCuotaPlanBasico) && (Convert.ToDecimal(0)==AV25TFConfiguracionEmpresaCuotaPlanBasico_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Plan Basico", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV24TFConfiguracionEmpresaCuotaPlanBasico, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV28TFConfiguracionEmpresaCuotaPlanBasico_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Plan Basico", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV28TFConfiguracionEmpresaCuotaPlanBasico_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV25TFConfiguracionEmpresaCuotaPlanBasico_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV42TFConfiguracionEmpresaCostoPlanSuperior) && (Convert.ToDecimal(0)==AV43TFConfiguracionEmpresaCostoPlanSuperior_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Plan Superior", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV42TFConfiguracionEmpresaCostoPlanSuperior, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV50TFConfiguracionEmpresaCostoPlanSuperior_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Plan Superior", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV50TFConfiguracionEmpresaCostoPlanSuperior_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV43TFConfiguracionEmpresaCostoPlanSuperior_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV44TFConfiguracionEmpresaCuotaPlanSuperior) && (Convert.ToDecimal(0)==AV45TFConfiguracionEmpresaCuotaPlanSuperior_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Plan Superior", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV44TFConfiguracionEmpresaCuotaPlanSuperior, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV51TFConfiguracionEmpresaCuotaPlanSuperior_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Plan Superior", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV51TFConfiguracionEmpresaCuotaPlanSuperior_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV45TFConfiguracionEmpresaCuotaPlanSuperior_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV46TFConfiguracionEmpresaCostoPlanNegocios) && (Convert.ToDecimal(0)==AV47TFConfiguracionEmpresaCostoPlanNegocios_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Plan Negocios", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV46TFConfiguracionEmpresaCostoPlanNegocios, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV52TFConfiguracionEmpresaCostoPlanNegocios_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Plan Negocios", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV52TFConfiguracionEmpresaCostoPlanNegocios_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV47TFConfiguracionEmpresaCostoPlanNegocios_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV48TFConfiguracionEmpresaCuotaPlanNegocios) && (Convert.ToDecimal(0)==AV49TFConfiguracionEmpresaCuotaPlanNegocios_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Plan Negocios", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV48TFConfiguracionEmpresaCuotaPlanNegocios, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV53TFConfiguracionEmpresaCuotaPlanNegocios_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Plan Negocios", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV53TFConfiguracionEmpresaCuotaPlanNegocios_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV49TFConfiguracionEmpresaCuotaPlanNegocios_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV54TFConfiguracionEmpresaCostoLandingPage) && (Convert.ToDecimal(0)==AV55TFConfiguracionEmpresaCostoLandingPage_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Landing Page", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV54TFConfiguracionEmpresaCostoLandingPage, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV58TFConfiguracionEmpresaCostoLandingPage_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Landing Page", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV58TFConfiguracionEmpresaCostoLandingPage_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV55TFConfiguracionEmpresaCostoLandingPage_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV56TFConfiguracionEmpresaCuotaLandingPage) && (Convert.ToDecimal(0)==AV57TFConfiguracionEmpresaCuotaLandingPage_To) ) )
         {
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Landing Page", ""), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV56TFConfiguracionEmpresaCuotaLandingPage, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV59TFConfiguracionEmpresaCuotaLandingPage_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Landing Page", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H3D0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV59TFConfiguracionEmpresaCuotaLandingPage_To_Description, "")), 25, Gx_line+0, 145, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( AV57TFConfiguracionEmpresaCuotaLandingPage_To, "ZZZZZZZZ9.99")), 145, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
      }

      protected void S121( )
      {
         /* 'PRINTCOLUMNTITLES' Routine */
         returnInSub = false;
         H3D0( false, 22) ;
         getPrinter().GxDrawLine(25, Gx_line+21, 789, Gx_line+21, 2, 0, 0, 255, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+22);
         H3D0( false, 36) ;
         getPrinter().GxAttris("Microsoft Sans Serif", 9, false, false, false, false, 0, 0, 0, 255, 0, 255, 255, 255) ;
         getPrinter().GxDrawText(context.GetMessage( "Id", ""), 30, Gx_line+10, 95, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Telefono", ""), 99, Gx_line+10, 229, Gx_line+26, 0, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Plan Basico", ""), 233, Gx_line+10, 298, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Plan Basico", ""), 302, Gx_line+10, 367, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Plan Superior", ""), 371, Gx_line+10, 437, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Plan Superior", ""), 441, Gx_line+10, 507, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Plan Negocios", ""), 511, Gx_line+10, 577, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Plan Negocios", ""), 581, Gx_line+10, 647, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Landing Page", ""), 651, Gx_line+10, 717, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Landing Page", ""), 721, Gx_line+10, 787, Gx_line+26, 2, 0, 0, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+36);
      }

      protected void S131( )
      {
         /* 'PRINTDATA' Routine */
         returnInSub = false;
         AV68Configuracionempresawwds_1_filterfulltext = AV13FilterFullText;
         AV69Configuracionempresawwds_2_tfconfiguracionempresaid = AV18TFConfiguracionEmpresaId;
         AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV19TFConfiguracionEmpresaId_To;
         AV71Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV20TFConfiguracionEmpresaTelefono;
         AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV21TFConfiguracionEmpresaTelefono_Sel;
         AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV22TFConfiguracionEmpresaCostoPlanBasico;
         AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV23TFConfiguracionEmpresaCostoPlanBasico_To;
         AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV24TFConfiguracionEmpresaCuotaPlanBasico;
         AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV25TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV42TFConfiguracionEmpresaCostoPlanSuperior;
         AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV43TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV44TFConfiguracionEmpresaCuotaPlanSuperior;
         AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV45TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV46TFConfiguracionEmpresaCostoPlanNegocios;
         AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV47TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV48TFConfiguracionEmpresaCuotaPlanNegocios;
         AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV49TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV54TFConfiguracionEmpresaCostoLandingPage;
         AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV55TFConfiguracionEmpresaCostoLandingPage_To;
         AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV56TFConfiguracionEmpresaCuotaLandingPage;
         AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV57TFConfiguracionEmpresaCuotaLandingPage_To;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV68Configuracionempresawwds_1_filterfulltext ,
                                              AV69Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                              AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                              AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                              AV71Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                              AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                              AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                              AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                              AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                              AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                              AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                              AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                              AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                              AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                              AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                              AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                              AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                              AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                              AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                              AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                              AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
                                              A44ConfiguracionEmpresaId ,
                                              A45ConfiguracionEmpresaTelefono ,
                                              A46ConfiguracionEmpresaCostoPlanB ,
                                              A47ConfiguracionEmpresaCuotaPlanB ,
                                              A48ConfiguracionEmpresaCostoPlanS ,
                                              A49ConfiguracionEmpresaCuotaPlanS ,
                                              A50ConfiguracionEmpresaCostoPlanN ,
                                              A51ConfiguracionEmpresaCuotaPlanN ,
                                              A54ConfiguracionEmpresaCostoLandi ,
                                              A55ConfiguracionEmpresaCuotaLandi ,
                                              AV11OrderedBy ,
                                              AV12OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV68Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext), "%", "");
         lV71Configuracionempresawwds_4_tfconfiguracionempresatelefono = StringUtil.PadR( StringUtil.RTrim( AV71Configuracionempresawwds_4_tfconfiguracionempresatelefono), 20, "%");
         /* Using cursor P003D2 */
         pr_default.execute(0, new Object[] {lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, lV68Configuracionempresawwds_1_filterfulltext, AV69Configuracionempresawwds_2_tfconfiguracionempresaid, AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to, lV71Configuracionempresawwds_4_tfconfiguracionempresatelefono, AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico, AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to, AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico, AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to, AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior, AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to, AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior, AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to, AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios, AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to, AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios, AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to, AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage, AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to, AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage, AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A55ConfiguracionEmpresaCuotaLandi = P003D2_A55ConfiguracionEmpresaCuotaLandi[0];
            A54ConfiguracionEmpresaCostoLandi = P003D2_A54ConfiguracionEmpresaCostoLandi[0];
            A51ConfiguracionEmpresaCuotaPlanN = P003D2_A51ConfiguracionEmpresaCuotaPlanN[0];
            A50ConfiguracionEmpresaCostoPlanN = P003D2_A50ConfiguracionEmpresaCostoPlanN[0];
            A49ConfiguracionEmpresaCuotaPlanS = P003D2_A49ConfiguracionEmpresaCuotaPlanS[0];
            A48ConfiguracionEmpresaCostoPlanS = P003D2_A48ConfiguracionEmpresaCostoPlanS[0];
            A47ConfiguracionEmpresaCuotaPlanB = P003D2_A47ConfiguracionEmpresaCuotaPlanB[0];
            A46ConfiguracionEmpresaCostoPlanB = P003D2_A46ConfiguracionEmpresaCostoPlanB[0];
            A44ConfiguracionEmpresaId = P003D2_A44ConfiguracionEmpresaId[0];
            A45ConfiguracionEmpresaTelefono = P003D2_A45ConfiguracionEmpresaTelefono[0];
            /* Execute user subroutine: 'BEFOREPRINTLINE' */
            S144 ();
            if ( returnInSub )
            {
               pr_default.close(0);
               returnInSub = true;
               if (true) return;
            }
            H3D0( false, 35) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9")), 30, Gx_line+10, 95, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( A45ConfiguracionEmpresaTelefono, "")), 99, Gx_line+10, 229, Gx_line+24, 0+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A46ConfiguracionEmpresaCostoPlanB, "ZZZZZZZZ9.99")), 233, Gx_line+10, 298, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A47ConfiguracionEmpresaCuotaPlanB, "ZZZZZZZZ9.99")), 302, Gx_line+10, 367, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A48ConfiguracionEmpresaCostoPlanS, "ZZZZZZZZ9.99")), 371, Gx_line+10, 437, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A49ConfiguracionEmpresaCuotaPlanS, "ZZZZZZZZ9.99")), 441, Gx_line+10, 507, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A50ConfiguracionEmpresaCostoPlanN, "ZZZZZZZZ9.99")), 511, Gx_line+10, 577, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A51ConfiguracionEmpresaCuotaPlanN, "ZZZZZZZZ9.99")), 581, Gx_line+10, 647, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A54ConfiguracionEmpresaCostoLandi, "ZZZZZZZZ9.99")), 651, Gx_line+10, 717, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( A55ConfiguracionEmpresaCuotaLandi, "ZZZZZZZZ9.99")), 721, Gx_line+10, 787, Gx_line+24, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawLine(28, Gx_line+34, 789, Gx_line+34, 1, 220, 220, 220, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+35);
            /* Execute user subroutine: 'AFTERPRINTLINE' */
            S161 ();
            if ( returnInSub )
            {
               pr_default.close(0);
               returnInSub = true;
               if (true) return;
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
      }

      protected void S151( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV14Session.Get("ConfiguracionEmpresaWWGridState"), "") == 0 )
         {
            AV16GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "ConfiguracionEmpresaWWGridState"), null, "", "");
         }
         else
         {
            AV16GridState.FromXml(AV14Session.Get("ConfiguracionEmpresaWWGridState"), null, "", "");
         }
         AV11OrderedBy = AV16GridState.gxTpr_Orderedby;
         AV12OrderedDsc = AV16GridState.gxTpr_Ordereddsc;
         AV89GXV1 = 1;
         while ( AV89GXV1 <= AV16GridState.gxTpr_Filtervalues.Count )
         {
            AV17GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV16GridState.gxTpr_Filtervalues.Item(AV89GXV1));
            if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV13FilterFullText = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESAID") == 0 )
            {
               AV18TFConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV19TFConfiguracionEmpresaId_To = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO") == 0 )
            {
               AV20TFConfiguracionEmpresaTelefono = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO_SEL") == 0 )
            {
               AV21TFConfiguracionEmpresaTelefono_Sel = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANBASICO") == 0 )
            {
               AV22TFConfiguracionEmpresaCostoPlanBasico = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV23TFConfiguracionEmpresaCostoPlanBasico_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANBASICO") == 0 )
            {
               AV24TFConfiguracionEmpresaCuotaPlanBasico = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV25TFConfiguracionEmpresaCuotaPlanBasico_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR") == 0 )
            {
               AV42TFConfiguracionEmpresaCostoPlanSuperior = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV43TFConfiguracionEmpresaCostoPlanSuperior_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR") == 0 )
            {
               AV44TFConfiguracionEmpresaCuotaPlanSuperior = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV45TFConfiguracionEmpresaCuotaPlanSuperior_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS") == 0 )
            {
               AV46TFConfiguracionEmpresaCostoPlanNegocios = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV47TFConfiguracionEmpresaCostoPlanNegocios_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS") == 0 )
            {
               AV48TFConfiguracionEmpresaCuotaPlanNegocios = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV49TFConfiguracionEmpresaCuotaPlanNegocios_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOLANDINGPAGE") == 0 )
            {
               AV54TFConfiguracionEmpresaCostoLandingPage = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV55TFConfiguracionEmpresaCostoLandingPage_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTALANDINGPAGE") == 0 )
            {
               AV56TFConfiguracionEmpresaCuotaLandingPage = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, ".");
               AV57TFConfiguracionEmpresaCuotaLandingPage_To = NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, ".");
            }
            AV89GXV1 = (int)(AV89GXV1+1);
         }
      }

      protected void S144( )
      {
         /* 'BEFOREPRINTLINE' Routine */
         returnInSub = false;
      }

      protected void S161( )
      {
         /* 'AFTERPRINTLINE' Routine */
         returnInSub = false;
      }

      protected void S171( )
      {
         /* 'PRINTFOOTER' Routine */
         returnInSub = false;
      }

      protected void H3D0( bool bFoot ,
                           int Inc )
      {
         /* Skip the required number of lines */
         while ( ( ToSkip > 0 ) || ( Gx_line + Inc > P_lines ) )
         {
            if ( Gx_line + Inc >= P_lines )
            {
               if ( Gx_page > 0 )
               {
                  /* Print footers */
                  Gx_line = P_lines;
                  AV38PageInfo = context.GetMessage( "Page: ", "") + StringUtil.Trim( StringUtil.Str( (decimal)(Gx_page), 6, 0));
                  AV35DateInfo = context.GetMessage( "Date: ", "") + context.localUtil.Format( Gx_date, "99/99/99");
                  getPrinter().GxDrawRect(0, Gx_line+5, 819, Gx_line+39, 1, 0, 0, 0, 1, 0, 0, 255, 1, 1, 1, 1, 0, 0, 0, 0) ;
                  getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
                  getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV38PageInfo, "")), 30, Gx_line+15, 409, Gx_line+29, 0, 0, 0, 0) ;
                  getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV35DateInfo, "")), 409, Gx_line+15, 789, Gx_line+29, 2, 0, 0, 0) ;
                  Gx_OldLine = Gx_line;
                  Gx_line = (int)(Gx_line+39);
                  getPrinter().GxEndPage() ;
                  if ( bFoot )
                  {
                     return  ;
                  }
               }
               ToSkip = 0;
               Gx_line = 0;
               Gx_page = (int)(Gx_page+1);
               /* Skip Margin Top Lines */
               Gx_line = (int)(Gx_line+(M_top*lineHeight));
               /* Print headers */
               getPrinter().GxStartPage() ;
               getPrinter().GxDrawRect(0, Gx_line+0, 819, Gx_line+107, 1, 0, 0, 0, 1, 0, 0, 255, 1, 1, 1, 1, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV33AppName, "")), 30, Gx_line+30, 283, Gx_line+44, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 20, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV40Title, "")), 30, Gx_line+44, 283, Gx_line+77, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV39Phone, "")), 283, Gx_line+30, 536, Gx_line+45, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV37Mail, "")), 283, Gx_line+45, 536, Gx_line+60, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV41Website, "")), 283, Gx_line+60, 536, Gx_line+77, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV30AddressLine1, "")), 536, Gx_line+30, 789, Gx_line+45, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV31AddressLine2, "")), 536, Gx_line+45, 789, Gx_line+60, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV32AddressLine3, "")), 536, Gx_line+60, 789, Gx_line+77, 2, 0, 0, 0) ;
               Gx_OldLine = Gx_line;
               Gx_line = (int)(Gx_line+127);
               if (true) break;
            }
            else
            {
               PrtOffset = 0;
               Gx_line = (int)(Gx_line+1);
            }
            ToSkip = (int)(ToSkip-1);
         }
         getPrinter().setPage(Gx_page);
      }

      protected void add_metrics( )
      {
         add_metrics0( ) ;
      }

      protected void add_metrics0( )
      {
         getPrinter().setMetrics("Microsoft Sans Serif", false, false, 58, 14, 72, 171,  new int[] {48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 18, 20, 23, 36, 36, 57, 43, 12, 21, 21, 25, 37, 18, 21, 18, 18, 36, 36, 36, 36, 36, 36, 36, 36, 36, 36, 18, 18, 37, 37, 37, 36, 65, 43, 43, 46, 46, 43, 39, 50, 46, 18, 32, 43, 36, 53, 46, 50, 43, 50, 46, 43, 40, 46, 43, 64, 41, 42, 39, 18, 18, 18, 27, 36, 21, 36, 36, 32, 36, 36, 18, 36, 36, 14, 15, 33, 14, 55, 36, 36, 36, 36, 21, 32, 18, 36, 33, 47, 31, 31, 31, 21, 17, 21, 37, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 48, 18, 20, 36, 36, 36, 36, 17, 36, 21, 47, 24, 36, 37, 21, 47, 35, 26, 35, 21, 21, 21, 37, 34, 21, 21, 21, 23, 36, 53, 53, 53, 39, 43, 43, 43, 43, 43, 43, 64, 46, 43, 43, 43, 43, 18, 18, 18, 18, 46, 46, 50, 50, 50, 50, 50, 37, 50, 46, 46, 46, 46, 43, 43, 39, 36, 36, 36, 36, 36, 36, 57, 32, 36, 36, 36, 36, 18, 18, 18, 18, 36, 36, 36, 36, 36, 36, 36, 35, 39, 36, 36, 36, 36, 32, 36, 32}) ;
      }

      public override int getOutputType( )
      {
         return GxReportUtils.OUTPUT_PDF ;
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
         AV9WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV40Title = "";
         AV64Companyname = "";
         AV39Phone = "";
         AV37Mail = "";
         AV41Website = "";
         AV30AddressLine1 = "";
         AV31AddressLine2 = "";
         AV32AddressLine3 = "";
         GXt_char2 = "";
         AV13FilterFullText = "";
         AV18TFConfiguracionEmpresaId = 0;
         AV19TFConfiguracionEmpresaId_To = 0;
         AV26TFConfiguracionEmpresaId_To_Description = "";
         AV21TFConfiguracionEmpresaTelefono_Sel = "";
         AV20TFConfiguracionEmpresaTelefono = "";
         AV27TFConfiguracionEmpresaCostoPlanBasico_To_Description = "";
         AV28TFConfiguracionEmpresaCuotaPlanBasico_To_Description = "";
         AV50TFConfiguracionEmpresaCostoPlanSuperior_To_Description = "";
         AV51TFConfiguracionEmpresaCuotaPlanSuperior_To_Description = "";
         AV52TFConfiguracionEmpresaCostoPlanNegocios_To_Description = "";
         AV53TFConfiguracionEmpresaCuotaPlanNegocios_To_Description = "";
         AV58TFConfiguracionEmpresaCostoLandingPage_To_Description = "";
         AV59TFConfiguracionEmpresaCuotaLandingPage_To_Description = "";
         AV68Configuracionempresawwds_1_filterfulltext = "";
         AV71Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = "";
         lV68Configuracionempresawwds_1_filterfulltext = "";
         lV71Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         A45ConfiguracionEmpresaTelefono = "";
         P003D2_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         P003D2_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         P003D2_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         P003D2_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         P003D2_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         P003D2_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         P003D2_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         P003D2_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         P003D2_A44ConfiguracionEmpresaId = new short[1] ;
         P003D2_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         AV14Session = context.GetSession();
         AV16GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV17GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV38PageInfo = "";
         AV35DateInfo = "";
         Gx_date = DateTime.MinValue;
         AV33AppName = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresawwexportreport__default(),
            new Object[][] {
                new Object[] {
               P003D2_A55ConfiguracionEmpresaCuotaLandi, P003D2_A54ConfiguracionEmpresaCostoLandi, P003D2_A51ConfiguracionEmpresaCuotaPlanN, P003D2_A50ConfiguracionEmpresaCostoPlanN, P003D2_A49ConfiguracionEmpresaCuotaPlanS, P003D2_A48ConfiguracionEmpresaCostoPlanS, P003D2_A47ConfiguracionEmpresaCuotaPlanB, P003D2_A46ConfiguracionEmpresaCostoPlanB, P003D2_A44ConfiguracionEmpresaId, P003D2_A45ConfiguracionEmpresaTelefono
               }
            }
         );
         Gx_date = DateTimeUtil.Today( context);
         /* GeneXus formulas. */
         Gx_line = 0;
         Gx_date = DateTimeUtil.Today( context);
      }

      private short gxcookieaux ;
      private short nGotPars ;
      private short GxWebError ;
      private short AV18TFConfiguracionEmpresaId ;
      private short AV19TFConfiguracionEmpresaId_To ;
      private short AV69Configuracionempresawwds_2_tfconfiguracionempresaid ;
      private short AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to ;
      private short A44ConfiguracionEmpresaId ;
      private short AV11OrderedBy ;
      private int M_top ;
      private int M_bot ;
      private int Line ;
      private int ToSkip ;
      private int PrtOffset ;
      private int Gx_OldLine ;
      private int AV89GXV1 ;
      private decimal AV22TFConfiguracionEmpresaCostoPlanBasico ;
      private decimal AV23TFConfiguracionEmpresaCostoPlanBasico_To ;
      private decimal AV24TFConfiguracionEmpresaCuotaPlanBasico ;
      private decimal AV25TFConfiguracionEmpresaCuotaPlanBasico_To ;
      private decimal AV42TFConfiguracionEmpresaCostoPlanSuperior ;
      private decimal AV43TFConfiguracionEmpresaCostoPlanSuperior_To ;
      private decimal AV44TFConfiguracionEmpresaCuotaPlanSuperior ;
      private decimal AV45TFConfiguracionEmpresaCuotaPlanSuperior_To ;
      private decimal AV46TFConfiguracionEmpresaCostoPlanNegocios ;
      private decimal AV47TFConfiguracionEmpresaCostoPlanNegocios_To ;
      private decimal AV48TFConfiguracionEmpresaCuotaPlanNegocios ;
      private decimal AV49TFConfiguracionEmpresaCuotaPlanNegocios_To ;
      private decimal AV54TFConfiguracionEmpresaCostoLandingPage ;
      private decimal AV55TFConfiguracionEmpresaCostoLandingPage_To ;
      private decimal AV56TFConfiguracionEmpresaCuotaLandingPage ;
      private decimal AV57TFConfiguracionEmpresaCuotaLandingPage_To ;
      private decimal AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ;
      private decimal AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ;
      private decimal AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ;
      private decimal AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ;
      private decimal AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ;
      private decimal AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ;
      private decimal AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ;
      private decimal AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ;
      private decimal AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ;
      private decimal AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ;
      private decimal AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ;
      private decimal AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ;
      private decimal AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ;
      private decimal AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ;
      private decimal AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ;
      private decimal AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ;
      private decimal A46ConfiguracionEmpresaCostoPlanB ;
      private decimal A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal A48ConfiguracionEmpresaCostoPlanS ;
      private decimal A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal A50ConfiguracionEmpresaCostoPlanN ;
      private decimal A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal A54ConfiguracionEmpresaCostoLandi ;
      private decimal A55ConfiguracionEmpresaCuotaLandi ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private string GXt_char2 ;
      private string AV21TFConfiguracionEmpresaTelefono_Sel ;
      private string AV20TFConfiguracionEmpresaTelefono ;
      private string AV71Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ;
      private string lV71Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string A45ConfiguracionEmpresaTelefono ;
      private DateTime Gx_date ;
      private bool entryPointCalled ;
      private bool AV8IsAuthorized ;
      private bool GXt_boolean1 ;
      private bool returnInSub ;
      private bool AV29TempBoolean ;
      private bool AV12OrderedDsc ;
      private string AV64Companyname ;
      private string AV40Title ;
      private string AV39Phone ;
      private string AV37Mail ;
      private string AV41Website ;
      private string AV30AddressLine1 ;
      private string AV31AddressLine2 ;
      private string AV32AddressLine3 ;
      private string AV13FilterFullText ;
      private string AV26TFConfiguracionEmpresaId_To_Description ;
      private string AV27TFConfiguracionEmpresaCostoPlanBasico_To_Description ;
      private string AV28TFConfiguracionEmpresaCuotaPlanBasico_To_Description ;
      private string AV50TFConfiguracionEmpresaCostoPlanSuperior_To_Description ;
      private string AV51TFConfiguracionEmpresaCuotaPlanSuperior_To_Description ;
      private string AV52TFConfiguracionEmpresaCostoPlanNegocios_To_Description ;
      private string AV53TFConfiguracionEmpresaCuotaPlanNegocios_To_Description ;
      private string AV58TFConfiguracionEmpresaCostoLandingPage_To_Description ;
      private string AV59TFConfiguracionEmpresaCuotaLandingPage_To_Description ;
      private string AV68Configuracionempresawwds_1_filterfulltext ;
      private string lV68Configuracionempresawwds_1_filterfulltext ;
      private string AV38PageInfo ;
      private string AV35DateInfo ;
      private string AV33AppName ;
      private IGxSession AV14Session ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private IDataStoreProvider pr_default ;
      private decimal[] P003D2_A55ConfiguracionEmpresaCuotaLandi ;
      private decimal[] P003D2_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] P003D2_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] P003D2_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] P003D2_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] P003D2_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] P003D2_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] P003D2_A46ConfiguracionEmpresaCostoPlanB ;
      private short[] P003D2_A44ConfiguracionEmpresaId ;
      private string[] P003D2_A45ConfiguracionEmpresaTelefono ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV16GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV17GridStateFilterValue ;
   }

   public class configuracionempresawwexportreport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P003D2( IGxContext context ,
                                             string AV68Configuracionempresawwds_1_filterfulltext ,
                                             short AV69Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                             short AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                             string AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                             string AV71Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                             decimal AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                             decimal AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                             decimal AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                             decimal AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                             decimal AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                             decimal AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                             decimal AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                             decimal AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                             decimal AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                             decimal AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                             decimal AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                             decimal AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                             decimal AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                             decimal AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                             decimal AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                             decimal AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
                                             short A44ConfiguracionEmpresaId ,
                                             string A45ConfiguracionEmpresaTelefono ,
                                             decimal A46ConfiguracionEmpresaCostoPlanB ,
                                             decimal A47ConfiguracionEmpresaCuotaPlanB ,
                                             decimal A48ConfiguracionEmpresaCostoPlanS ,
                                             decimal A49ConfiguracionEmpresaCuotaPlanS ,
                                             decimal A50ConfiguracionEmpresaCostoPlanN ,
                                             decimal A51ConfiguracionEmpresaCuotaPlanN ,
                                             decimal A54ConfiguracionEmpresaCostoLandi ,
                                             decimal A55ConfiguracionEmpresaCuotaLandi ,
                                             short AV11OrderedBy ,
                                             bool AV12OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[30];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `ConfiguracionEmpresaCuotaLandi`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaId`, `ConfiguracionEmpresaTelefono` FROM `ConfiguracionEmpresa`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV68Configuracionempresawwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( `ConfiguracionEmpresaTelefono` like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV68Configuracionempresawwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
            GXv_int3[4] = 1;
            GXv_int3[5] = 1;
            GXv_int3[6] = 1;
            GXv_int3[7] = 1;
            GXv_int3[8] = 1;
            GXv_int3[9] = 1;
         }
         if ( ! (0==AV69Configuracionempresawwds_2_tfconfiguracionempresaid) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` >= @AV69Configuracionempresawwds_2_tfconfiguracionempresaid)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` <= @AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV71Configuracionempresawwds_4_tfconfiguracionempresatelefono)) ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` like @lV71Configuracionempresawwds_4_tfconfiguracionempresatelefono)");
         }
         else
         {
            GXv_int3[12] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ! ( StringUtil.StrCmp(AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` = @AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_se)");
         }
         else
         {
            GXv_int3[13] = 1;
         }
         if ( StringUtil.StrCmp(AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`ConfiguracionEmpresaTelefono`))=0))");
         }
         if ( ! (Convert.ToDecimal(0)==AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` >= @AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int3[14] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` <= @AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int3[15] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` >= @AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int3[16] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` <= @AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int3[17] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` >= @AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int3[18] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` <= @AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int3[19] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` >= @AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int3[20] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` <= @AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int3[21] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` >= @AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int3[22] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` <= @AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int3[23] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` >= @AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int3[24] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` <= @AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int3[25] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` >= @AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int3[26] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` <= @AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int3[27] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` >= @AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int3[28] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` <= @AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int3[29] = 1;
         }
         scmdbuf += sWhereString;
         if ( ( AV11OrderedBy == 1 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaTelefono`";
         }
         else if ( ( AV11OrderedBy == 1 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaTelefono` DESC";
         }
         else if ( ( AV11OrderedBy == 2 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaId`";
         }
         else if ( ( AV11OrderedBy == 2 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaId` DESC";
         }
         else if ( ( AV11OrderedBy == 3 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanB`";
         }
         else if ( ( AV11OrderedBy == 3 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanB` DESC";
         }
         else if ( ( AV11OrderedBy == 4 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanB`";
         }
         else if ( ( AV11OrderedBy == 4 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanB` DESC";
         }
         else if ( ( AV11OrderedBy == 5 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanS`";
         }
         else if ( ( AV11OrderedBy == 5 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanS` DESC";
         }
         else if ( ( AV11OrderedBy == 6 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanS`";
         }
         else if ( ( AV11OrderedBy == 6 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanS` DESC";
         }
         else if ( ( AV11OrderedBy == 7 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanN`";
         }
         else if ( ( AV11OrderedBy == 7 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanN` DESC";
         }
         else if ( ( AV11OrderedBy == 8 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanN`";
         }
         else if ( ( AV11OrderedBy == 8 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanN` DESC";
         }
         else if ( ( AV11OrderedBy == 9 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoLandi`";
         }
         else if ( ( AV11OrderedBy == 9 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoLandi` DESC";
         }
         else if ( ( AV11OrderedBy == 10 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaLandi`";
         }
         else if ( ( AV11OrderedBy == 10 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaLandi` DESC";
         }
         GXv_Object4[0] = scmdbuf;
         GXv_Object4[1] = GXv_int3;
         return GXv_Object4 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_P003D2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (decimal)dynConstraints[5] , (decimal)dynConstraints[6] , (decimal)dynConstraints[7] , (decimal)dynConstraints[8] , (decimal)dynConstraints[9] , (decimal)dynConstraints[10] , (decimal)dynConstraints[11] , (decimal)dynConstraints[12] , (decimal)dynConstraints[13] , (decimal)dynConstraints[14] , (decimal)dynConstraints[15] , (decimal)dynConstraints[16] , (decimal)dynConstraints[17] , (decimal)dynConstraints[18] , (decimal)dynConstraints[19] , (decimal)dynConstraints[20] , (short)dynConstraints[21] , (string)dynConstraints[22] , (decimal)dynConstraints[23] , (decimal)dynConstraints[24] , (decimal)dynConstraints[25] , (decimal)dynConstraints[26] , (decimal)dynConstraints[27] , (decimal)dynConstraints[28] , (decimal)dynConstraints[29] , (decimal)dynConstraints[30] , (short)dynConstraints[31] , (bool)dynConstraints[32] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP003D2;
          prmP003D2 = new Object[] {
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV68Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV69Configuracionempresawwds_2_tfconfiguracionempresaid",GXType.Int16,4,0) ,
          new ParDef("@AV70Configuracionempresawwds_3_tfconfiguracionempresaid_to",GXType.Int16,4,0) ,
          new ParDef("@lV71Configuracionempresawwds_4_tfconfiguracionempresatelefono",GXType.Char,20,0) ,
          new ParDef("@AV72Configuracionempresawwds_5_tfconfiguracionempresatelefono_se",GXType.Char,20,0) ,
          new ParDef("@AV73Configuracionempresawwds_6_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV74Configuracionempresawwds_7_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV75Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV76Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV77Configuracionempresawwds_10_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV78Configuracionempresawwds_11_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV79Configuracionempresawwds_12_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV80Configuracionempresawwds_13_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV81Configuracionempresawwds_14_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV82Configuracionempresawwds_15_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV83Configuracionempresawwds_16_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV84Configuracionempresawwds_17_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV85Configuracionempresawwds_18_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV86Configuracionempresawwds_19_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV87Configuracionempresawwds_20_tfconfiguracionempresacuotalandi",GXType.Number,12,2) ,
          new ParDef("@AV88Configuracionempresawwds_21_tfconfiguracionempresacuotalandi",GXType.Number,12,2)
          };
          def= new CursorDef[] {
              new CursorDef("P003D2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP003D2,100, GxCacheFrequency.OFF ,true,false )
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
       switch ( cursor )
       {
             case 0 :
                ((decimal[]) buf[0])[0] = rslt.getDecimal(1);
                ((decimal[]) buf[1])[0] = rslt.getDecimal(2);
                ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
                ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
                ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
                ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
                ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
                ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
                ((short[]) buf[8])[0] = rslt.getShort(9);
                ((string[]) buf[9])[0] = rslt.getString(10, 20);
                return;
       }
    }

 }

}
