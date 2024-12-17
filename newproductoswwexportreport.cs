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
   public class newproductoswwexportreport : GXWebProcedure
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

      public newproductoswwexportreport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newproductoswwexportreport( IGxContext context )
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
         setOutputFileName("NewProductosWWExportReport");
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
            new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newproductosww_Execute", out  GXt_boolean1) ;
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
               AV40Title = context.GetMessage( "Lista de New Productos", "");
               GXt_char2 = AV55Companyname;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Name", out  GXt_char2) ;
               AV55Companyname = GXt_char2;
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
            H2U0( true, 0) ;
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
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "WWP_FullTextFilterDescription", ""), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV13FilterFullText, "")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (0==AV48TFNewProductosId) && (0==AV49TFNewProductosId_To) ) )
         {
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Id", ""), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV48TFNewProductosId), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV50TFNewProductosId_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Id", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV50TFNewProductosId_To_Description, "")), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV49TFNewProductosId_To), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19TFNewProductosNombre_Sel)) )
         {
            AV29TempBoolean = (bool)(((StringUtil.StrCmp(AV19TFNewProductosNombre_Sel, "<#Empty#>")==0)));
            AV19TFNewProductosNombre_Sel = (AV29TempBoolean ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV19TFNewProductosNombre_Sel);
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Nombre", ""), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV19TFNewProductosNombre_Sel, "")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV19TFNewProductosNombre_Sel = (AV29TempBoolean ? "<#Empty#>" : AV19TFNewProductosNombre_Sel);
         }
         else
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV18TFNewProductosNombre)) )
            {
               H2U0( false, 19) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(context.GetMessage( "Nombre", ""), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV18TFNewProductosNombre, "")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
               Gx_OldLine = Gx_line;
               Gx_line = (int)(Gx_line+19);
            }
         }
         if ( ! ( (0==AV22TFNewProductosNumeroDescargas) && (0==AV23TFNewProductosNumeroDescargas_To) ) )
         {
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Descargas", ""), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV22TFNewProductosNumeroDescargas), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV28TFNewProductosNumeroDescargas_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Descargas", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV28TFNewProductosNumeroDescargas_To_Description, "")), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV23TFNewProductosNumeroDescargas_To), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (0==AV42TFNewProductosNumeroVentas) && (0==AV43TFNewProductosNumeroVentas_To) ) )
         {
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Ventas", ""), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV42TFNewProductosNumeroVentas), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV44TFNewProductosNumeroVentas_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Ventas", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV44TFNewProductosNumeroVentas_To_Description, "")), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV43TFNewProductosNumeroVentas_To), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (0==AV45TFNewProductosVisitas) && (0==AV46TFNewProductosVisitas_To) ) )
         {
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Visitas", ""), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV45TFNewProductosVisitas), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV47TFNewProductosVisitas_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Visitas", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H2U0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV47TFNewProductosVisitas_To_Description, "")), 25, Gx_line+0, 127, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV46TFNewProductosVisitas_To), "ZZZ9")), 127, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
      }

      protected void S121( )
      {
         /* 'PRINTCOLUMNTITLES' Routine */
         returnInSub = false;
         H2U0( false, 22) ;
         getPrinter().GxDrawLine(25, Gx_line+21, 789, Gx_line+21, 2, 0, 0, 255, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+22);
         H2U0( false, 36) ;
         getPrinter().GxAttris("Microsoft Sans Serif", 9, false, false, false, false, 0, 0, 0, 255, 0, 255, 255, 255) ;
         getPrinter().GxDrawText(context.GetMessage( "Id", ""), 30, Gx_line+10, 135, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Imagen", ""), 139, Gx_line+10, 244, Gx_line+26, 0, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Nombre", ""), 248, Gx_line+10, 458, Gx_line+26, 0, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Descargas", ""), 462, Gx_line+10, 567, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Ventas", ""), 571, Gx_line+10, 677, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Visitas", ""), 681, Gx_line+10, 787, Gx_line+26, 2, 0, 0, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+36);
      }

      protected void S131( )
      {
         /* 'PRINTDATA' Routine */
         returnInSub = false;
         AV59Newproductoswwds_1_filterfulltext = AV13FilterFullText;
         AV60Newproductoswwds_2_tfnewproductosid = AV48TFNewProductosId;
         AV61Newproductoswwds_3_tfnewproductosid_to = AV49TFNewProductosId_To;
         AV62Newproductoswwds_4_tfnewproductosnombre = AV18TFNewProductosNombre;
         AV63Newproductoswwds_5_tfnewproductosnombre_sel = AV19TFNewProductosNombre_Sel;
         AV64Newproductoswwds_6_tfnewproductosnumerodescargas = AV22TFNewProductosNumeroDescargas;
         AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV23TFNewProductosNumeroDescargas_To;
         AV66Newproductoswwds_8_tfnewproductosnumeroventas = AV42TFNewProductosNumeroVentas;
         AV67Newproductoswwds_9_tfnewproductosnumeroventas_to = AV43TFNewProductosNumeroVentas_To;
         AV68Newproductoswwds_10_tfnewproductosvisitas = AV45TFNewProductosVisitas;
         AV69Newproductoswwds_11_tfnewproductosvisitas_to = AV46TFNewProductosVisitas_To;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV59Newproductoswwds_1_filterfulltext ,
                                              AV60Newproductoswwds_2_tfnewproductosid ,
                                              AV61Newproductoswwds_3_tfnewproductosid_to ,
                                              AV63Newproductoswwds_5_tfnewproductosnombre_sel ,
                                              AV62Newproductoswwds_4_tfnewproductosnombre ,
                                              AV64Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                              AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                              AV66Newproductoswwds_8_tfnewproductosnumeroventas ,
                                              AV67Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                              AV68Newproductoswwds_10_tfnewproductosvisitas ,
                                              AV69Newproductoswwds_11_tfnewproductosvisitas_to ,
                                              A34NewProductosId ,
                                              A36NewProductosNombre ,
                                              A39NewProductosNumeroDescargas ,
                                              A42NewProductosNumeroVentas ,
                                              A43NewProductosVisitas ,
                                              AV11OrderedBy ,
                                              AV12OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT,
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV59Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV59Newproductoswwds_1_filterfulltext), "%", "");
         lV59Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV59Newproductoswwds_1_filterfulltext), "%", "");
         lV59Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV59Newproductoswwds_1_filterfulltext), "%", "");
         lV59Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV59Newproductoswwds_1_filterfulltext), "%", "");
         lV59Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV59Newproductoswwds_1_filterfulltext), "%", "");
         lV62Newproductoswwds_4_tfnewproductosnombre = StringUtil.Concat( StringUtil.RTrim( AV62Newproductoswwds_4_tfnewproductosnombre), "%", "");
         /* Using cursor P002U2 */
         pr_default.execute(0, new Object[] {lV59Newproductoswwds_1_filterfulltext, lV59Newproductoswwds_1_filterfulltext, lV59Newproductoswwds_1_filterfulltext, lV59Newproductoswwds_1_filterfulltext, lV59Newproductoswwds_1_filterfulltext, AV60Newproductoswwds_2_tfnewproductosid, AV61Newproductoswwds_3_tfnewproductosid_to, lV62Newproductoswwds_4_tfnewproductosnombre, AV63Newproductoswwds_5_tfnewproductosnombre_sel, AV64Newproductoswwds_6_tfnewproductosnumerodescargas, AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to, AV66Newproductoswwds_8_tfnewproductosnumeroventas, AV67Newproductoswwds_9_tfnewproductosnumeroventas_to, AV68Newproductoswwds_10_tfnewproductosvisitas, AV69Newproductoswwds_11_tfnewproductosvisitas_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A43NewProductosVisitas = P002U2_A43NewProductosVisitas[0];
            A42NewProductosNumeroVentas = P002U2_A42NewProductosNumeroVentas[0];
            A39NewProductosNumeroDescargas = P002U2_A39NewProductosNumeroDescargas[0];
            A34NewProductosId = P002U2_A34NewProductosId[0];
            A36NewProductosNombre = P002U2_A36NewProductosNombre[0];
            A40000NewProductosImagen_GXI = P002U2_A40000NewProductosImagen_GXI[0];
            A35NewProductosImagen = P002U2_A35NewProductosImagen[0];
            /* Execute user subroutine: 'BEFOREPRINTLINE' */
            S144 ();
            if ( returnInSub )
            {
               pr_default.close(0);
               returnInSub = true;
               if (true) return;
            }
            H2U0( false, 63) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9")), 30, Gx_line+10, 135, Gx_line+52, 2+16, 0, 0, 0) ;
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : A35NewProductosImagen);
            getPrinter().GxDrawBitMap(sImgUrl, 139, Gx_line+10, 244, Gx_line+52) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( A36NewProductosNombre, "")), 248, Gx_line+10, 458, Gx_line+52, 0+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(A39NewProductosNumeroDescargas), "ZZZ9")), 462, Gx_line+10, 567, Gx_line+52, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(A42NewProductosNumeroVentas), "ZZZ9")), 571, Gx_line+10, 677, Gx_line+52, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(A43NewProductosVisitas), "ZZZ9")), 681, Gx_line+10, 787, Gx_line+52, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawLine(28, Gx_line+62, 789, Gx_line+62, 1, 220, 220, 220, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+63);
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
         if ( StringUtil.StrCmp(AV14Session.Get("NewProductosWWGridState"), "") == 0 )
         {
            AV16GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "NewProductosWWGridState"), null, "", "");
         }
         else
         {
            AV16GridState.FromXml(AV14Session.Get("NewProductosWWGridState"), null, "", "");
         }
         AV11OrderedBy = AV16GridState.gxTpr_Orderedby;
         AV12OrderedDsc = AV16GridState.gxTpr_Ordereddsc;
         AV70GXV1 = 1;
         while ( AV70GXV1 <= AV16GridState.gxTpr_Filtervalues.Count )
         {
            AV17GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV16GridState.gxTpr_Filtervalues.Item(AV70GXV1));
            if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV13FilterFullText = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSID") == 0 )
            {
               AV48TFNewProductosId = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV49TFNewProductosId_To = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE") == 0 )
            {
               AV18TFNewProductosNombre = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE_SEL") == 0 )
            {
               AV19TFNewProductosNombre_Sel = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMERODESCARGAS") == 0 )
            {
               AV22TFNewProductosNumeroDescargas = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV23TFNewProductosNumeroDescargas_To = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMEROVENTAS") == 0 )
            {
               AV42TFNewProductosNumeroVentas = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV43TFNewProductosNumeroVentas_To = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSVISITAS") == 0 )
            {
               AV45TFNewProductosVisitas = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV46TFNewProductosVisitas_To = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            AV70GXV1 = (int)(AV70GXV1+1);
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

      protected void H2U0( bool bFoot ,
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
         AV55Companyname = "";
         AV39Phone = "";
         AV37Mail = "";
         AV41Website = "";
         AV30AddressLine1 = "";
         AV31AddressLine2 = "";
         AV32AddressLine3 = "";
         GXt_char2 = "";
         AV13FilterFullText = "";
         AV50TFNewProductosId_To_Description = "";
         AV19TFNewProductosNombre_Sel = "";
         AV18TFNewProductosNombre = "";
         AV28TFNewProductosNumeroDescargas_To_Description = "";
         AV44TFNewProductosNumeroVentas_To_Description = "";
         AV47TFNewProductosVisitas_To_Description = "";
         AV59Newproductoswwds_1_filterfulltext = "";
         AV62Newproductoswwds_4_tfnewproductosnombre = "";
         AV63Newproductoswwds_5_tfnewproductosnombre_sel = "";
         lV59Newproductoswwds_1_filterfulltext = "";
         lV62Newproductoswwds_4_tfnewproductosnombre = "";
         A36NewProductosNombre = "";
         P002U2_A43NewProductosVisitas = new short[1] ;
         P002U2_A42NewProductosNumeroVentas = new short[1] ;
         P002U2_A39NewProductosNumeroDescargas = new short[1] ;
         P002U2_A34NewProductosId = new short[1] ;
         P002U2_A36NewProductosNombre = new string[] {""} ;
         P002U2_A40000NewProductosImagen_GXI = new string[] {""} ;
         P002U2_A35NewProductosImagen = new string[] {""} ;
         A40000NewProductosImagen_GXI = "";
         A35NewProductosImagen = "";
         sImgUrl = "";
         AV14Session = context.GetSession();
         AV16GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV17GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV38PageInfo = "";
         AV35DateInfo = "";
         Gx_date = DateTime.MinValue;
         AV33AppName = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newproductoswwexportreport__default(),
            new Object[][] {
                new Object[] {
               P002U2_A43NewProductosVisitas, P002U2_A42NewProductosNumeroVentas, P002U2_A39NewProductosNumeroDescargas, P002U2_A34NewProductosId, P002U2_A36NewProductosNombre, P002U2_A40000NewProductosImagen_GXI, P002U2_A35NewProductosImagen
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
      private short AV48TFNewProductosId ;
      private short AV49TFNewProductosId_To ;
      private short AV22TFNewProductosNumeroDescargas ;
      private short AV23TFNewProductosNumeroDescargas_To ;
      private short AV42TFNewProductosNumeroVentas ;
      private short AV43TFNewProductosNumeroVentas_To ;
      private short AV45TFNewProductosVisitas ;
      private short AV46TFNewProductosVisitas_To ;
      private short AV60Newproductoswwds_2_tfnewproductosid ;
      private short AV61Newproductoswwds_3_tfnewproductosid_to ;
      private short AV64Newproductoswwds_6_tfnewproductosnumerodescargas ;
      private short AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to ;
      private short AV66Newproductoswwds_8_tfnewproductosnumeroventas ;
      private short AV67Newproductoswwds_9_tfnewproductosnumeroventas_to ;
      private short AV68Newproductoswwds_10_tfnewproductosvisitas ;
      private short AV69Newproductoswwds_11_tfnewproductosvisitas_to ;
      private short A34NewProductosId ;
      private short A39NewProductosNumeroDescargas ;
      private short A42NewProductosNumeroVentas ;
      private short A43NewProductosVisitas ;
      private short AV11OrderedBy ;
      private int M_top ;
      private int M_bot ;
      private int Line ;
      private int ToSkip ;
      private int PrtOffset ;
      private int Gx_OldLine ;
      private int AV70GXV1 ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private string GXt_char2 ;
      private string sImgUrl ;
      private DateTime Gx_date ;
      private bool entryPointCalled ;
      private bool AV8IsAuthorized ;
      private bool GXt_boolean1 ;
      private bool returnInSub ;
      private bool AV29TempBoolean ;
      private bool AV12OrderedDsc ;
      private string AV55Companyname ;
      private string AV40Title ;
      private string AV39Phone ;
      private string AV37Mail ;
      private string AV41Website ;
      private string AV30AddressLine1 ;
      private string AV31AddressLine2 ;
      private string AV32AddressLine3 ;
      private string AV13FilterFullText ;
      private string AV50TFNewProductosId_To_Description ;
      private string AV19TFNewProductosNombre_Sel ;
      private string AV18TFNewProductosNombre ;
      private string AV28TFNewProductosNumeroDescargas_To_Description ;
      private string AV44TFNewProductosNumeroVentas_To_Description ;
      private string AV47TFNewProductosVisitas_To_Description ;
      private string AV59Newproductoswwds_1_filterfulltext ;
      private string AV62Newproductoswwds_4_tfnewproductosnombre ;
      private string AV63Newproductoswwds_5_tfnewproductosnombre_sel ;
      private string lV59Newproductoswwds_1_filterfulltext ;
      private string lV62Newproductoswwds_4_tfnewproductosnombre ;
      private string A36NewProductosNombre ;
      private string A40000NewProductosImagen_GXI ;
      private string AV38PageInfo ;
      private string AV35DateInfo ;
      private string AV33AppName ;
      private string A35NewProductosImagen ;
      private IGxSession AV14Session ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private IDataStoreProvider pr_default ;
      private short[] P002U2_A43NewProductosVisitas ;
      private short[] P002U2_A42NewProductosNumeroVentas ;
      private short[] P002U2_A39NewProductosNumeroDescargas ;
      private short[] P002U2_A34NewProductosId ;
      private string[] P002U2_A36NewProductosNombre ;
      private string[] P002U2_A40000NewProductosImagen_GXI ;
      private string[] P002U2_A35NewProductosImagen ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV16GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV17GridStateFilterValue ;
   }

   public class newproductoswwexportreport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002U2( IGxContext context ,
                                             string AV59Newproductoswwds_1_filterfulltext ,
                                             short AV60Newproductoswwds_2_tfnewproductosid ,
                                             short AV61Newproductoswwds_3_tfnewproductosid_to ,
                                             string AV63Newproductoswwds_5_tfnewproductosnombre_sel ,
                                             string AV62Newproductoswwds_4_tfnewproductosnombre ,
                                             short AV64Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                             short AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                             short AV66Newproductoswwds_8_tfnewproductosnumeroventas ,
                                             short AV67Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                             short AV68Newproductoswwds_10_tfnewproductosvisitas ,
                                             short AV69Newproductoswwds_11_tfnewproductosvisitas_to ,
                                             short A34NewProductosId ,
                                             string A36NewProductosNombre ,
                                             short A39NewProductosNumeroDescargas ,
                                             short A42NewProductosNumeroVentas ,
                                             short A43NewProductosVisitas ,
                                             short AV11OrderedBy ,
                                             bool AV12OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[15];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `NewProductosVisitas`, `NewProductosNumeroVentas`, `NewProductosNumeroDescargas`, `NewProductosId`, `NewProductosNombre`, `NewProductosImagen_GXI`, `NewProductosImagen` FROM `NewProductos`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Newproductoswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewProductosId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV59Newproductoswwds_1_filterfulltext)) or ( `NewProductosNombre` like CONCAT('%', @lV59Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroDescargas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV59Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroVentas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV59Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV59Newproductoswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
            GXv_int3[4] = 1;
         }
         if ( ! (0==AV60Newproductoswwds_2_tfnewproductosid) )
         {
            AddWhere(sWhereString, "(`NewProductosId` >= @AV60Newproductoswwds_2_tfnewproductosid)");
         }
         else
         {
            GXv_int3[5] = 1;
         }
         if ( ! (0==AV61Newproductoswwds_3_tfnewproductosid_to) )
         {
            AddWhere(sWhereString, "(`NewProductosId` <= @AV61Newproductoswwds_3_tfnewproductosid_to)");
         }
         else
         {
            GXv_int3[6] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV63Newproductoswwds_5_tfnewproductosnombre_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newproductoswwds_4_tfnewproductosnombre)) ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` like @lV62Newproductoswwds_4_tfnewproductosnombre)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Newproductoswwds_5_tfnewproductosnombre_sel)) && ! ( StringUtil.StrCmp(AV63Newproductoswwds_5_tfnewproductosnombre_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` = @AV63Newproductoswwds_5_tfnewproductosnombre_sel)");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( StringUtil.StrCmp(AV63Newproductoswwds_5_tfnewproductosnombre_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewProductosNombre`))=0))");
         }
         if ( ! (0==AV64Newproductoswwds_6_tfnewproductosnumerodescargas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` >= @AV64Newproductoswwds_6_tfnewproductosnumerodescargas)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( ! (0==AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` <= @AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV66Newproductoswwds_8_tfnewproductosnumeroventas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` >= @AV66Newproductoswwds_8_tfnewproductosnumeroventas)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( ! (0==AV67Newproductoswwds_9_tfnewproductosnumeroventas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` <= @AV67Newproductoswwds_9_tfnewproductosnumeroventas_to)");
         }
         else
         {
            GXv_int3[12] = 1;
         }
         if ( ! (0==AV68Newproductoswwds_10_tfnewproductosvisitas) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` >= @AV68Newproductoswwds_10_tfnewproductosvisitas)");
         }
         else
         {
            GXv_int3[13] = 1;
         }
         if ( ! (0==AV69Newproductoswwds_11_tfnewproductosvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` <= @AV69Newproductoswwds_11_tfnewproductosvisitas_to)");
         }
         else
         {
            GXv_int3[14] = 1;
         }
         scmdbuf += sWhereString;
         if ( ( AV11OrderedBy == 1 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosNombre`";
         }
         else if ( ( AV11OrderedBy == 1 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosNombre` DESC";
         }
         else if ( ( AV11OrderedBy == 2 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosId`";
         }
         else if ( ( AV11OrderedBy == 2 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosId` DESC";
         }
         else if ( ( AV11OrderedBy == 3 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroDescargas`";
         }
         else if ( ( AV11OrderedBy == 3 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroDescargas` DESC";
         }
         else if ( ( AV11OrderedBy == 4 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroVentas`";
         }
         else if ( ( AV11OrderedBy == 4 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroVentas` DESC";
         }
         else if ( ( AV11OrderedBy == 5 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosVisitas`";
         }
         else if ( ( AV11OrderedBy == 5 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosVisitas` DESC";
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
                     return conditional_P002U2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (short)dynConstraints[5] , (short)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (short)dynConstraints[13] , (short)dynConstraints[14] , (short)dynConstraints[15] , (short)dynConstraints[16] , (bool)dynConstraints[17] );
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
          Object[] prmP002U2;
          prmP002U2 = new Object[] {
          new ParDef("@lV59Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV59Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV59Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV59Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV59Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV60Newproductoswwds_2_tfnewproductosid",GXType.Int16,4,0) ,
          new ParDef("@AV61Newproductoswwds_3_tfnewproductosid_to",GXType.Int16,4,0) ,
          new ParDef("@lV62Newproductoswwds_4_tfnewproductosnombre",GXType.Char,200,0) ,
          new ParDef("@AV63Newproductoswwds_5_tfnewproductosnombre_sel",GXType.Char,200,0) ,
          new ParDef("@AV64Newproductoswwds_6_tfnewproductosnumerodescargas",GXType.Int16,4,0) ,
          new ParDef("@AV65Newproductoswwds_7_tfnewproductosnumerodescargas_to",GXType.Int16,4,0) ,
          new ParDef("@AV66Newproductoswwds_8_tfnewproductosnumeroventas",GXType.Int16,4,0) ,
          new ParDef("@AV67Newproductoswwds_9_tfnewproductosnumeroventas_to",GXType.Int16,4,0) ,
          new ParDef("@AV68Newproductoswwds_10_tfnewproductosvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV69Newproductoswwds_11_tfnewproductosvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002U2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002U2,100, GxCacheFrequency.OFF ,true,false )
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
                ((short[]) buf[0])[0] = rslt.getShort(1);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                ((short[]) buf[3])[0] = rslt.getShort(4);
                ((string[]) buf[4])[0] = rslt.getVarchar(5);
                ((string[]) buf[5])[0] = rslt.getMultimediaUri(6);
                ((string[]) buf[6])[0] = rslt.getMultimediaFile(7, rslt.getVarchar(6));
                return;
       }
    }

 }

}
