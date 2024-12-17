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
   public class categoriaswwexportreport : GXWebProcedure
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

      public categoriaswwexportreport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public categoriaswwexportreport( IGxContext context )
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
         setOutputFileName("CategoriasWWExportReport");
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
            new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "categoriasww_Execute", out  GXt_boolean1) ;
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
               AV34Title = context.GetMessage( "Lista de Gestión de Categorias", "");
               GXt_char2 = AV40Companyname;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Name", out  GXt_char2) ;
               AV40Companyname = GXt_char2;
               GXt_char2 = AV33Phone;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Phone", out  GXt_char2) ;
               AV33Phone = GXt_char2;
               GXt_char2 = AV31Mail;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Mail", out  GXt_char2) ;
               AV31Mail = GXt_char2;
               GXt_char2 = AV35Website;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Website", out  GXt_char2) ;
               AV35Website = GXt_char2;
               GXt_char2 = AV24AddressLine1;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Address", out  GXt_char2) ;
               AV24AddressLine1 = GXt_char2;
               GXt_char2 = AV25AddressLine2;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Neighbour", out  GXt_char2) ;
               AV25AddressLine2 = GXt_char2;
               GXt_char2 = AV26AddressLine3;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_CityAndCountry", out  GXt_char2) ;
               AV26AddressLine3 = GXt_char2;
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
            H2M0( true, 0) ;
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
            H2M0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "WWP_FullTextFilterDescription", ""), 25, Gx_line+0, 101, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV13FilterFullText, "")), 101, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV21TFCategoriasCategoria_Sel)) )
         {
            AV23TempBoolean = (bool)(((StringUtil.StrCmp(AV21TFCategoriasCategoria_Sel, "<#Empty#>")==0)));
            AV21TFCategoriasCategoria_Sel = (AV23TempBoolean ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV21TFCategoriasCategoria_Sel);
            H2M0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Categoria", ""), 25, Gx_line+0, 101, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV21TFCategoriasCategoria_Sel, "")), 101, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV21TFCategoriasCategoria_Sel = (AV23TempBoolean ? "<#Empty#>" : AV21TFCategoriasCategoria_Sel);
         }
         else
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV20TFCategoriasCategoria)) )
            {
               H2M0( false, 19) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(context.GetMessage( "Categoria", ""), 25, Gx_line+0, 101, Gx_line+14, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV20TFCategoriasCategoria, "")), 101, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
               Gx_OldLine = Gx_line;
               Gx_line = (int)(Gx_line+19);
            }
         }
      }

      protected void S121( )
      {
         /* 'PRINTCOLUMNTITLES' Routine */
         returnInSub = false;
         H2M0( false, 22) ;
         getPrinter().GxDrawLine(25, Gx_line+21, 789, Gx_line+21, 2, 0, 0, 255, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+22);
         H2M0( false, 36) ;
         getPrinter().GxAttris("Microsoft Sans Serif", 9, false, false, false, false, 0, 0, 0, 255, 0, 255, 255, 255) ;
         getPrinter().GxDrawText(context.GetMessage( "Categoria", ""), 30, Gx_line+10, 787, Gx_line+26, 0, 0, 0, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+36);
      }

      protected void S131( )
      {
         /* 'PRINTDATA' Routine */
         returnInSub = false;
         AV44Categoriaswwds_1_filterfulltext = AV13FilterFullText;
         AV45Categoriaswwds_2_tfcategoriascategoria = AV20TFCategoriasCategoria;
         AV46Categoriaswwds_3_tfcategoriascategoria_sel = AV21TFCategoriasCategoria_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV44Categoriaswwds_1_filterfulltext ,
                                              AV46Categoriaswwds_3_tfcategoriascategoria_sel ,
                                              AV45Categoriaswwds_2_tfcategoriascategoria ,
                                              A21CategoriasCategoria ,
                                              AV12OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         lV44Categoriaswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV44Categoriaswwds_1_filterfulltext), "%", "");
         lV45Categoriaswwds_2_tfcategoriascategoria = StringUtil.PadR( StringUtil.RTrim( AV45Categoriaswwds_2_tfcategoriascategoria), 100, "%");
         /* Using cursor P002M2 */
         pr_default.execute(0, new Object[] {lV44Categoriaswwds_1_filterfulltext, lV45Categoriaswwds_2_tfcategoriascategoria, AV46Categoriaswwds_3_tfcategoriascategoria_sel});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A21CategoriasCategoria = P002M2_A21CategoriasCategoria[0];
            A20CategoriasId = P002M2_A20CategoriasId[0];
            /* Execute user subroutine: 'BEFOREPRINTLINE' */
            S144 ();
            if ( returnInSub )
            {
               pr_default.close(0);
               returnInSub = true;
               if (true) return;
            }
            H2M0( false, 35) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( A21CategoriasCategoria, "")), 30, Gx_line+10, 787, Gx_line+24, 0+16, 0, 0, 0) ;
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
         if ( StringUtil.StrCmp(AV14Session.Get("CategoriasWWGridState"), "") == 0 )
         {
            AV16GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "CategoriasWWGridState"), null, "", "");
         }
         else
         {
            AV16GridState.FromXml(AV14Session.Get("CategoriasWWGridState"), null, "", "");
         }
         AV12OrderedDsc = AV16GridState.gxTpr_Ordereddsc;
         AV47GXV1 = 1;
         while ( AV47GXV1 <= AV16GridState.gxTpr_Filtervalues.Count )
         {
            AV17GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV16GridState.gxTpr_Filtervalues.Item(AV47GXV1));
            if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV13FilterFullText = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCATEGORIASCATEGORIA") == 0 )
            {
               AV20TFCategoriasCategoria = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFCATEGORIASCATEGORIA_SEL") == 0 )
            {
               AV21TFCategoriasCategoria_Sel = AV17GridStateFilterValue.gxTpr_Value;
            }
            AV47GXV1 = (int)(AV47GXV1+1);
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

      protected void H2M0( bool bFoot ,
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
                  AV32PageInfo = context.GetMessage( "Page: ", "") + StringUtil.Trim( StringUtil.Str( (decimal)(Gx_page), 6, 0));
                  AV29DateInfo = context.GetMessage( "Date: ", "") + context.localUtil.Format( Gx_date, "99/99/99");
                  getPrinter().GxDrawRect(0, Gx_line+5, 819, Gx_line+39, 1, 0, 0, 0, 1, 0, 0, 255, 1, 1, 1, 1, 0, 0, 0, 0) ;
                  getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
                  getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV32PageInfo, "")), 30, Gx_line+15, 409, Gx_line+29, 0, 0, 0, 0) ;
                  getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV29DateInfo, "")), 409, Gx_line+15, 789, Gx_line+29, 2, 0, 0, 0) ;
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
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV27AppName, "")), 30, Gx_line+30, 283, Gx_line+44, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 20, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV34Title, "")), 30, Gx_line+44, 283, Gx_line+77, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV33Phone, "")), 283, Gx_line+30, 536, Gx_line+45, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV31Mail, "")), 283, Gx_line+45, 536, Gx_line+60, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV35Website, "")), 283, Gx_line+60, 536, Gx_line+77, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV24AddressLine1, "")), 536, Gx_line+30, 789, Gx_line+45, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV25AddressLine2, "")), 536, Gx_line+45, 789, Gx_line+60, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV26AddressLine3, "")), 536, Gx_line+60, 789, Gx_line+77, 2, 0, 0, 0) ;
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
         AV34Title = "";
         AV40Companyname = "";
         AV33Phone = "";
         AV31Mail = "";
         AV35Website = "";
         AV24AddressLine1 = "";
         AV25AddressLine2 = "";
         AV26AddressLine3 = "";
         GXt_char2 = "";
         AV13FilterFullText = "";
         AV21TFCategoriasCategoria_Sel = "";
         AV20TFCategoriasCategoria = "";
         AV44Categoriaswwds_1_filterfulltext = "";
         AV45Categoriaswwds_2_tfcategoriascategoria = "";
         AV46Categoriaswwds_3_tfcategoriascategoria_sel = "";
         lV44Categoriaswwds_1_filterfulltext = "";
         lV45Categoriaswwds_2_tfcategoriascategoria = "";
         A21CategoriasCategoria = "";
         P002M2_A21CategoriasCategoria = new string[] {""} ;
         P002M2_A20CategoriasId = new short[1] ;
         AV14Session = context.GetSession();
         AV16GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV17GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV32PageInfo = "";
         AV29DateInfo = "";
         Gx_date = DateTime.MinValue;
         AV27AppName = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.categoriaswwexportreport__default(),
            new Object[][] {
                new Object[] {
               P002M2_A21CategoriasCategoria, P002M2_A20CategoriasId
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
      private short A20CategoriasId ;
      private int M_top ;
      private int M_bot ;
      private int Line ;
      private int ToSkip ;
      private int PrtOffset ;
      private int Gx_OldLine ;
      private int AV47GXV1 ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private string GXt_char2 ;
      private string AV21TFCategoriasCategoria_Sel ;
      private string AV20TFCategoriasCategoria ;
      private string AV45Categoriaswwds_2_tfcategoriascategoria ;
      private string AV46Categoriaswwds_3_tfcategoriascategoria_sel ;
      private string lV45Categoriaswwds_2_tfcategoriascategoria ;
      private string A21CategoriasCategoria ;
      private DateTime Gx_date ;
      private bool entryPointCalled ;
      private bool AV8IsAuthorized ;
      private bool GXt_boolean1 ;
      private bool returnInSub ;
      private bool AV23TempBoolean ;
      private bool AV12OrderedDsc ;
      private string AV40Companyname ;
      private string AV34Title ;
      private string AV33Phone ;
      private string AV31Mail ;
      private string AV35Website ;
      private string AV24AddressLine1 ;
      private string AV25AddressLine2 ;
      private string AV26AddressLine3 ;
      private string AV13FilterFullText ;
      private string AV44Categoriaswwds_1_filterfulltext ;
      private string lV44Categoriaswwds_1_filterfulltext ;
      private string AV32PageInfo ;
      private string AV29DateInfo ;
      private string AV27AppName ;
      private IGxSession AV14Session ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private IDataStoreProvider pr_default ;
      private string[] P002M2_A21CategoriasCategoria ;
      private short[] P002M2_A20CategoriasId ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV16GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV17GridStateFilterValue ;
   }

   public class categoriaswwexportreport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002M2( IGxContext context ,
                                             string AV44Categoriaswwds_1_filterfulltext ,
                                             string AV46Categoriaswwds_3_tfcategoriascategoria_sel ,
                                             string AV45Categoriaswwds_2_tfcategoriascategoria ,
                                             string A21CategoriasCategoria ,
                                             bool AV12OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[3];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `CategoriasCategoria`, `CategoriasId` FROM `Categorias`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV44Categoriaswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( `CategoriasCategoria` like CONCAT('%', @lV44Categoriaswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV46Categoriaswwds_3_tfcategoriascategoria_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV45Categoriaswwds_2_tfcategoriascategoria)) ) )
         {
            AddWhere(sWhereString, "(`CategoriasCategoria` like @lV45Categoriaswwds_2_tfcategoriascategoria)");
         }
         else
         {
            GXv_int3[1] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV46Categoriaswwds_3_tfcategoriascategoria_sel)) && ! ( StringUtil.StrCmp(AV46Categoriaswwds_3_tfcategoriascategoria_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`CategoriasCategoria` = @AV46Categoriaswwds_3_tfcategoriascategoria_sel)");
         }
         else
         {
            GXv_int3[2] = 1;
         }
         if ( StringUtil.StrCmp(AV46Categoriaswwds_3_tfcategoriascategoria_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`CategoriasCategoria`))=0))");
         }
         scmdbuf += sWhereString;
         if ( ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `CategoriasCategoria`";
         }
         else if ( AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `CategoriasCategoria` DESC";
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
                     return conditional_P002M2(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (bool)dynConstraints[4] );
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
          Object[] prmP002M2;
          prmP002M2 = new Object[] {
          new ParDef("@lV44Categoriaswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV45Categoriaswwds_2_tfcategoriascategoria",GXType.Char,100,0) ,
          new ParDef("@AV46Categoriaswwds_3_tfcategoriascategoria_sel",GXType.Char,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002M2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002M2,100, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[0])[0] = rslt.getString(1, 100);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                return;
       }
    }

 }

}
