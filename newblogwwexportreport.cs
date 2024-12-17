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
   public class newblogwwexportreport : GXWebProcedure
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

      public newblogwwexportreport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newblogwwexportreport( IGxContext context )
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
         setOutputFileName("NewBlogWWExportReport");
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
            new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newblogww_Execute", out  GXt_boolean1) ;
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
               AV38Title = context.GetMessage( "Lista de New Blog", "");
               GXt_char2 = AV53Companyname;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Name", out  GXt_char2) ;
               AV53Companyname = GXt_char2;
               GXt_char2 = AV37Phone;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Phone", out  GXt_char2) ;
               AV37Phone = GXt_char2;
               GXt_char2 = AV35Mail;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Mail", out  GXt_char2) ;
               AV35Mail = GXt_char2;
               GXt_char2 = AV39Website;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Website", out  GXt_char2) ;
               AV39Website = GXt_char2;
               GXt_char2 = AV28AddressLine1;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Address", out  GXt_char2) ;
               AV28AddressLine1 = GXt_char2;
               GXt_char2 = AV29AddressLine2;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_Neighbour", out  GXt_char2) ;
               AV29AddressLine2 = GXt_char2;
               GXt_char2 = AV30AddressLine3;
               new DesignSystem.Programs.workwithplus.wwp_getsystemparameter(context ).execute(  "AD_Application_CityAndCountry", out  GXt_char2) ;
               AV30AddressLine3 = GXt_char2;
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
            H2J0( true, 0) ;
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
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "WWP_FullTextFilterDescription", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV13FilterFullText, "")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (0==AV18TFNewBlogId) && (0==AV19TFNewBlogId_To) ) )
         {
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Id", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV18TFNewBlogId), "ZZZ9")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV26TFNewBlogId_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Id", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV26TFNewBlogId_To_Description, "")), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV19TFNewBlogId_To), "ZZZ9")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV21TFNewBlogTitulo_Sel)) )
         {
            AV27TempBoolean = (bool)(((StringUtil.StrCmp(AV21TFNewBlogTitulo_Sel, "<#Empty#>")==0)));
            AV21TFNewBlogTitulo_Sel = (AV27TempBoolean ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV21TFNewBlogTitulo_Sel);
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Titulo", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV21TFNewBlogTitulo_Sel, "")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV21TFNewBlogTitulo_Sel = (AV27TempBoolean ? "<#Empty#>" : AV21TFNewBlogTitulo_Sel);
         }
         else
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV20TFNewBlogTitulo)) )
            {
               H2J0( false, 19) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(context.GetMessage( "Titulo", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV20TFNewBlogTitulo, "")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
               Gx_OldLine = Gx_line;
               Gx_line = (int)(Gx_line+19);
            }
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV23TFNewBlogSubTitulo_Sel)) )
         {
            AV27TempBoolean = (bool)(((StringUtil.StrCmp(AV23TFNewBlogSubTitulo_Sel, "<#Empty#>")==0)));
            AV23TFNewBlogSubTitulo_Sel = (AV27TempBoolean ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV23TFNewBlogSubTitulo_Sel);
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "SubTitulo", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV23TFNewBlogSubTitulo_Sel, "")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV23TFNewBlogSubTitulo_Sel = (AV27TempBoolean ? "<#Empty#>" : AV23TFNewBlogSubTitulo_Sel);
         }
         else
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV22TFNewBlogSubTitulo)) )
            {
               H2J0( false, 19) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(context.GetMessage( "SubTitulo", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV22TFNewBlogSubTitulo, "")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
               Gx_OldLine = Gx_line;
               Gx_line = (int)(Gx_line+19);
            }
         }
         if ( ! (0==AV42TFNewBlogDestacado_Sel) )
         {
            if ( AV42TFNewBlogDestacado_Sel == 1 )
            {
               AV43FilterTFNewBlogDestacado_SelValueDescription = context.GetMessage( "WWP_TSChecked", "");
            }
            else if ( AV42TFNewBlogDestacado_Sel == 2 )
            {
               AV43FilterTFNewBlogDestacado_SelValueDescription = context.GetMessage( "WWP_TSUnChecked", "");
            }
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Destacado", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV43FilterTFNewBlogDestacado_SelValueDescription, "")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! ( (0==AV44TFNewBlogVisitas) && (0==AV45TFNewBlogVisitas_To) ) )
         {
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Visitas", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV44TFNewBlogVisitas), "ZZZ9")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
            AV46TFNewBlogVisitas_To_Description = StringUtil.Format( "%1 (%2)", context.GetMessage( "Visitas", ""), context.GetMessage( "WWP_TSTo", ""), "", "", "", "", "", "", "");
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV46TFNewBlogVisitas_To_Description, "")), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(AV45TFNewBlogVisitas_To), "ZZZ9")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
         if ( ! (0==AV47TFNewBlogBorrador_Sel) )
         {
            if ( AV47TFNewBlogBorrador_Sel == 1 )
            {
               AV48FilterTFNewBlogBorrador_SelValueDescription = context.GetMessage( "WWP_TSChecked", "");
            }
            else if ( AV47TFNewBlogBorrador_Sel == 2 )
            {
               AV48FilterTFNewBlogBorrador_SelValueDescription = context.GetMessage( "WWP_TSUnChecked", "");
            }
            H2J0( false, 19) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 169, 169, 169, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(context.GetMessage( "Borrador", ""), 25, Gx_line+0, 105, Gx_line+14, 0, 0, 0, 0) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV48FilterTFNewBlogBorrador_SelValueDescription, "")), 105, Gx_line+0, 789, Gx_line+14, 0, 0, 0, 0) ;
            Gx_OldLine = Gx_line;
            Gx_line = (int)(Gx_line+19);
         }
      }

      protected void S121( )
      {
         /* 'PRINTCOLUMNTITLES' Routine */
         returnInSub = false;
         H2J0( false, 22) ;
         getPrinter().GxDrawLine(25, Gx_line+21, 789, Gx_line+21, 2, 0, 0, 255, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+22);
         H2J0( false, 36) ;
         getPrinter().GxAttris("Microsoft Sans Serif", 9, false, false, false, false, 0, 0, 0, 255, 0, 255, 255, 255) ;
         getPrinter().GxDrawText(context.GetMessage( "Id", ""), 30, Gx_line+10, 111, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Imagen", ""), 115, Gx_line+10, 196, Gx_line+26, 0, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Titulo", ""), 200, Gx_line+10, 362, Gx_line+26, 0, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "SubTitulo", ""), 366, Gx_line+10, 529, Gx_line+26, 0, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Destacado", ""), 533, Gx_line+10, 615, Gx_line+26, 0, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Visitas", ""), 619, Gx_line+10, 701, Gx_line+26, 2, 0, 0, 0) ;
         getPrinter().GxDrawText(context.GetMessage( "Borrador", ""), 705, Gx_line+10, 787, Gx_line+26, 0, 0, 0, 0) ;
         Gx_OldLine = Gx_line;
         Gx_line = (int)(Gx_line+36);
      }

      protected void S131( )
      {
         /* 'PRINTDATA' Routine */
         returnInSub = false;
         AV57Newblogwwds_1_filterfulltext = AV13FilterFullText;
         AV58Newblogwwds_2_tfnewblogid = AV18TFNewBlogId;
         AV59Newblogwwds_3_tfnewblogid_to = AV19TFNewBlogId_To;
         AV60Newblogwwds_4_tfnewblogtitulo = AV20TFNewBlogTitulo;
         AV61Newblogwwds_5_tfnewblogtitulo_sel = AV21TFNewBlogTitulo_Sel;
         AV62Newblogwwds_6_tfnewblogsubtitulo = AV22TFNewBlogSubTitulo;
         AV63Newblogwwds_7_tfnewblogsubtitulo_sel = AV23TFNewBlogSubTitulo_Sel;
         AV64Newblogwwds_8_tfnewblogdestacado_sel = AV42TFNewBlogDestacado_Sel;
         AV65Newblogwwds_9_tfnewblogvisitas = AV44TFNewBlogVisitas;
         AV66Newblogwwds_10_tfnewblogvisitas_to = AV45TFNewBlogVisitas_To;
         AV67Newblogwwds_11_tfnewblogborrador_sel = AV47TFNewBlogBorrador_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV57Newblogwwds_1_filterfulltext ,
                                              AV58Newblogwwds_2_tfnewblogid ,
                                              AV59Newblogwwds_3_tfnewblogid_to ,
                                              AV61Newblogwwds_5_tfnewblogtitulo_sel ,
                                              AV60Newblogwwds_4_tfnewblogtitulo ,
                                              AV63Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                              AV62Newblogwwds_6_tfnewblogsubtitulo ,
                                              AV64Newblogwwds_8_tfnewblogdestacado_sel ,
                                              AV65Newblogwwds_9_tfnewblogvisitas ,
                                              AV66Newblogwwds_10_tfnewblogvisitas_to ,
                                              AV67Newblogwwds_11_tfnewblogborrador_sel ,
                                              A12NewBlogId ,
                                              A14NewBlogTitulo ,
                                              A15NewBlogSubTitulo ,
                                              A18NewBlogVisitas ,
                                              A19NewBlogDestacado ,
                                              A25NewBlogBorrador ,
                                              AV11OrderedBy ,
                                              AV12OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.BOOLEAN,
                                              TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV57Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV57Newblogwwds_1_filterfulltext), "%", "");
         lV57Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV57Newblogwwds_1_filterfulltext), "%", "");
         lV57Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV57Newblogwwds_1_filterfulltext), "%", "");
         lV57Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV57Newblogwwds_1_filterfulltext), "%", "");
         lV60Newblogwwds_4_tfnewblogtitulo = StringUtil.Concat( StringUtil.RTrim( AV60Newblogwwds_4_tfnewblogtitulo), "%", "");
         lV62Newblogwwds_6_tfnewblogsubtitulo = StringUtil.Concat( StringUtil.RTrim( AV62Newblogwwds_6_tfnewblogsubtitulo), "%", "");
         /* Using cursor P002J2 */
         pr_default.execute(0, new Object[] {lV57Newblogwwds_1_filterfulltext, lV57Newblogwwds_1_filterfulltext, lV57Newblogwwds_1_filterfulltext, lV57Newblogwwds_1_filterfulltext, AV58Newblogwwds_2_tfnewblogid, AV59Newblogwwds_3_tfnewblogid_to, lV60Newblogwwds_4_tfnewblogtitulo, AV61Newblogwwds_5_tfnewblogtitulo_sel, lV62Newblogwwds_6_tfnewblogsubtitulo, AV63Newblogwwds_7_tfnewblogsubtitulo_sel, AV65Newblogwwds_9_tfnewblogvisitas, AV66Newblogwwds_10_tfnewblogvisitas_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A25NewBlogBorrador = P002J2_A25NewBlogBorrador[0];
            A18NewBlogVisitas = P002J2_A18NewBlogVisitas[0];
            A19NewBlogDestacado = P002J2_A19NewBlogDestacado[0];
            A12NewBlogId = P002J2_A12NewBlogId[0];
            A15NewBlogSubTitulo = P002J2_A15NewBlogSubTitulo[0];
            A14NewBlogTitulo = P002J2_A14NewBlogTitulo[0];
            A40000NewBlogImagen_GXI = P002J2_A40000NewBlogImagen_GXI[0];
            A13NewBlogImagen = P002J2_A13NewBlogImagen[0];
            /* Execute user subroutine: 'BEFOREPRINTLINE' */
            S144 ();
            if ( returnInSub )
            {
               pr_default.close(0);
               returnInSub = true;
               if (true) return;
            }
            H2J0( false, 63) ;
            getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 0, 0, 0, 0, 255, 255, 255) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9")), 30, Gx_line+10, 111, Gx_line+52, 2+16, 0, 0, 0) ;
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : A13NewBlogImagen);
            getPrinter().GxDrawBitMap(sImgUrl, 115, Gx_line+10, 196, Gx_line+52) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( A14NewBlogTitulo, "")), 200, Gx_line+10, 362, Gx_line+52, 0+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( A15NewBlogSubTitulo, "")), 366, Gx_line+10, 529, Gx_line+52, 0+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.BoolToStr( A19NewBlogDestacado), 533, Gx_line+10, 615, Gx_line+52, 0+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.LTrim( context.localUtil.Format( (decimal)(A18NewBlogVisitas), "ZZZ9")), 619, Gx_line+10, 701, Gx_line+52, 2+16, 0, 0, 0) ;
            getPrinter().GxDrawText(StringUtil.BoolToStr( A25NewBlogBorrador), 705, Gx_line+10, 787, Gx_line+52, 0+16, 0, 0, 0) ;
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
         if ( StringUtil.StrCmp(AV14Session.Get("NewBlogWWGridState"), "") == 0 )
         {
            AV16GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "NewBlogWWGridState"), null, "", "");
         }
         else
         {
            AV16GridState.FromXml(AV14Session.Get("NewBlogWWGridState"), null, "", "");
         }
         AV11OrderedBy = AV16GridState.gxTpr_Orderedby;
         AV12OrderedDsc = AV16GridState.gxTpr_Ordereddsc;
         AV68GXV1 = 1;
         while ( AV68GXV1 <= AV16GridState.gxTpr_Filtervalues.Count )
         {
            AV17GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV16GridState.gxTpr_Filtervalues.Item(AV68GXV1));
            if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV13FilterFullText = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGID") == 0 )
            {
               AV18TFNewBlogId = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV19TFNewBlogId_To = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO") == 0 )
            {
               AV20TFNewBlogTitulo = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO_SEL") == 0 )
            {
               AV21TFNewBlogTitulo_Sel = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO") == 0 )
            {
               AV22TFNewBlogSubTitulo = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO_SEL") == 0 )
            {
               AV23TFNewBlogSubTitulo_Sel = AV17GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGDESTACADO_SEL") == 0 )
            {
               AV42TFNewBlogDestacado_Sel = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGVISITAS") == 0 )
            {
               AV44TFNewBlogVisitas = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV45TFNewBlogVisitas_To = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV17GridStateFilterValue.gxTpr_Name, "TFNEWBLOGBORRADOR_SEL") == 0 )
            {
               AV47TFNewBlogBorrador_Sel = (short)(Math.Round(NumberUtil.Val( AV17GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
            }
            AV68GXV1 = (int)(AV68GXV1+1);
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

      protected void H2J0( bool bFoot ,
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
                  AV36PageInfo = context.GetMessage( "Page: ", "") + StringUtil.Trim( StringUtil.Str( (decimal)(Gx_page), 6, 0));
                  AV33DateInfo = context.GetMessage( "Date: ", "") + context.localUtil.Format( Gx_date, "99/99/99");
                  getPrinter().GxDrawRect(0, Gx_line+5, 819, Gx_line+39, 1, 0, 0, 0, 1, 0, 0, 255, 1, 1, 1, 1, 0, 0, 0, 0) ;
                  getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
                  getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV36PageInfo, "")), 30, Gx_line+15, 409, Gx_line+29, 0, 0, 0, 0) ;
                  getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV33DateInfo, "")), 409, Gx_line+15, 789, Gx_line+29, 2, 0, 0, 0) ;
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
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV31AppName, "")), 30, Gx_line+30, 283, Gx_line+44, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 20, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV38Title, "")), 30, Gx_line+44, 283, Gx_line+77, 0, 0, 0, 0) ;
               getPrinter().GxAttris("Microsoft Sans Serif", 8, false, false, false, false, 0, 255, 255, 255, 0, 255, 255, 255) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV37Phone, "")), 283, Gx_line+30, 536, Gx_line+45, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV35Mail, "")), 283, Gx_line+45, 536, Gx_line+60, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV39Website, "")), 283, Gx_line+60, 536, Gx_line+77, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV28AddressLine1, "")), 536, Gx_line+30, 789, Gx_line+45, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV29AddressLine2, "")), 536, Gx_line+45, 789, Gx_line+60, 2, 0, 0, 0) ;
               getPrinter().GxDrawText(StringUtil.RTrim( context.localUtil.Format( AV30AddressLine3, "")), 536, Gx_line+60, 789, Gx_line+77, 2, 0, 0, 0) ;
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
         AV38Title = "";
         AV53Companyname = "";
         AV37Phone = "";
         AV35Mail = "";
         AV39Website = "";
         AV28AddressLine1 = "";
         AV29AddressLine2 = "";
         AV30AddressLine3 = "";
         GXt_char2 = "";
         AV13FilterFullText = "";
         AV26TFNewBlogId_To_Description = "";
         AV21TFNewBlogTitulo_Sel = "";
         AV20TFNewBlogTitulo = "";
         AV23TFNewBlogSubTitulo_Sel = "";
         AV22TFNewBlogSubTitulo = "";
         AV43FilterTFNewBlogDestacado_SelValueDescription = "";
         AV46TFNewBlogVisitas_To_Description = "";
         AV48FilterTFNewBlogBorrador_SelValueDescription = "";
         AV57Newblogwwds_1_filterfulltext = "";
         AV60Newblogwwds_4_tfnewblogtitulo = "";
         AV61Newblogwwds_5_tfnewblogtitulo_sel = "";
         AV62Newblogwwds_6_tfnewblogsubtitulo = "";
         AV63Newblogwwds_7_tfnewblogsubtitulo_sel = "";
         lV57Newblogwwds_1_filterfulltext = "";
         lV60Newblogwwds_4_tfnewblogtitulo = "";
         lV62Newblogwwds_6_tfnewblogsubtitulo = "";
         A14NewBlogTitulo = "";
         A15NewBlogSubTitulo = "";
         P002J2_A25NewBlogBorrador = new bool[] {false} ;
         P002J2_A18NewBlogVisitas = new short[1] ;
         P002J2_A19NewBlogDestacado = new bool[] {false} ;
         P002J2_A12NewBlogId = new short[1] ;
         P002J2_A15NewBlogSubTitulo = new string[] {""} ;
         P002J2_A14NewBlogTitulo = new string[] {""} ;
         P002J2_A40000NewBlogImagen_GXI = new string[] {""} ;
         P002J2_A13NewBlogImagen = new string[] {""} ;
         A40000NewBlogImagen_GXI = "";
         A13NewBlogImagen = "";
         sImgUrl = "";
         AV14Session = context.GetSession();
         AV16GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV17GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV36PageInfo = "";
         AV33DateInfo = "";
         Gx_date = DateTime.MinValue;
         AV31AppName = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newblogwwexportreport__default(),
            new Object[][] {
                new Object[] {
               P002J2_A25NewBlogBorrador, P002J2_A18NewBlogVisitas, P002J2_A19NewBlogDestacado, P002J2_A12NewBlogId, P002J2_A15NewBlogSubTitulo, P002J2_A14NewBlogTitulo, P002J2_A40000NewBlogImagen_GXI, P002J2_A13NewBlogImagen
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
      private short AV18TFNewBlogId ;
      private short AV19TFNewBlogId_To ;
      private short AV42TFNewBlogDestacado_Sel ;
      private short AV44TFNewBlogVisitas ;
      private short AV45TFNewBlogVisitas_To ;
      private short AV47TFNewBlogBorrador_Sel ;
      private short AV58Newblogwwds_2_tfnewblogid ;
      private short AV59Newblogwwds_3_tfnewblogid_to ;
      private short AV64Newblogwwds_8_tfnewblogdestacado_sel ;
      private short AV65Newblogwwds_9_tfnewblogvisitas ;
      private short AV66Newblogwwds_10_tfnewblogvisitas_to ;
      private short AV67Newblogwwds_11_tfnewblogborrador_sel ;
      private short A12NewBlogId ;
      private short A18NewBlogVisitas ;
      private short AV11OrderedBy ;
      private int M_top ;
      private int M_bot ;
      private int Line ;
      private int ToSkip ;
      private int PrtOffset ;
      private int Gx_OldLine ;
      private int AV68GXV1 ;
      private string GXKey ;
      private string gxfirstwebparm ;
      private string GXt_char2 ;
      private string sImgUrl ;
      private DateTime Gx_date ;
      private bool entryPointCalled ;
      private bool AV8IsAuthorized ;
      private bool GXt_boolean1 ;
      private bool returnInSub ;
      private bool AV27TempBoolean ;
      private bool A19NewBlogDestacado ;
      private bool A25NewBlogBorrador ;
      private bool AV12OrderedDsc ;
      private string AV53Companyname ;
      private string AV38Title ;
      private string AV37Phone ;
      private string AV35Mail ;
      private string AV39Website ;
      private string AV28AddressLine1 ;
      private string AV29AddressLine2 ;
      private string AV30AddressLine3 ;
      private string AV13FilterFullText ;
      private string AV26TFNewBlogId_To_Description ;
      private string AV21TFNewBlogTitulo_Sel ;
      private string AV20TFNewBlogTitulo ;
      private string AV23TFNewBlogSubTitulo_Sel ;
      private string AV22TFNewBlogSubTitulo ;
      private string AV43FilterTFNewBlogDestacado_SelValueDescription ;
      private string AV46TFNewBlogVisitas_To_Description ;
      private string AV48FilterTFNewBlogBorrador_SelValueDescription ;
      private string AV57Newblogwwds_1_filterfulltext ;
      private string AV60Newblogwwds_4_tfnewblogtitulo ;
      private string AV61Newblogwwds_5_tfnewblogtitulo_sel ;
      private string AV62Newblogwwds_6_tfnewblogsubtitulo ;
      private string AV63Newblogwwds_7_tfnewblogsubtitulo_sel ;
      private string lV57Newblogwwds_1_filterfulltext ;
      private string lV60Newblogwwds_4_tfnewblogtitulo ;
      private string lV62Newblogwwds_6_tfnewblogsubtitulo ;
      private string A14NewBlogTitulo ;
      private string A15NewBlogSubTitulo ;
      private string A40000NewBlogImagen_GXI ;
      private string AV36PageInfo ;
      private string AV33DateInfo ;
      private string AV31AppName ;
      private string A13NewBlogImagen ;
      private IGxSession AV14Session ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private IDataStoreProvider pr_default ;
      private bool[] P002J2_A25NewBlogBorrador ;
      private short[] P002J2_A18NewBlogVisitas ;
      private bool[] P002J2_A19NewBlogDestacado ;
      private short[] P002J2_A12NewBlogId ;
      private string[] P002J2_A15NewBlogSubTitulo ;
      private string[] P002J2_A14NewBlogTitulo ;
      private string[] P002J2_A40000NewBlogImagen_GXI ;
      private string[] P002J2_A13NewBlogImagen ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV16GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV17GridStateFilterValue ;
   }

   public class newblogwwexportreport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002J2( IGxContext context ,
                                             string AV57Newblogwwds_1_filterfulltext ,
                                             short AV58Newblogwwds_2_tfnewblogid ,
                                             short AV59Newblogwwds_3_tfnewblogid_to ,
                                             string AV61Newblogwwds_5_tfnewblogtitulo_sel ,
                                             string AV60Newblogwwds_4_tfnewblogtitulo ,
                                             string AV63Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                             string AV62Newblogwwds_6_tfnewblogsubtitulo ,
                                             short AV64Newblogwwds_8_tfnewblogdestacado_sel ,
                                             short AV65Newblogwwds_9_tfnewblogvisitas ,
                                             short AV66Newblogwwds_10_tfnewblogvisitas_to ,
                                             short AV67Newblogwwds_11_tfnewblogborrador_sel ,
                                             short A12NewBlogId ,
                                             string A14NewBlogTitulo ,
                                             string A15NewBlogSubTitulo ,
                                             short A18NewBlogVisitas ,
                                             bool A19NewBlogDestacado ,
                                             bool A25NewBlogBorrador ,
                                             short AV11OrderedBy ,
                                             bool AV12OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[12];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `NewBlogBorrador`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogId`, `NewBlogSubTitulo`, `NewBlogTitulo`, `NewBlogImagen_GXI`, `NewBlogImagen` FROM `NewBlog`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV57Newblogwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewBlogId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV57Newblogwwds_1_filterfulltext)) or ( `NewBlogTitulo` like CONCAT('%', @lV57Newblogwwds_1_filterfulltext)) or ( `NewBlogSubTitulo` like CONCAT('%', @lV57Newblogwwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewBlogVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV57Newblogwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
         }
         if ( ! (0==AV58Newblogwwds_2_tfnewblogid) )
         {
            AddWhere(sWhereString, "(`NewBlogId` >= @AV58Newblogwwds_2_tfnewblogid)");
         }
         else
         {
            GXv_int3[4] = 1;
         }
         if ( ! (0==AV59Newblogwwds_3_tfnewblogid_to) )
         {
            AddWhere(sWhereString, "(`NewBlogId` <= @AV59Newblogwwds_3_tfnewblogid_to)");
         }
         else
         {
            GXv_int3[5] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV61Newblogwwds_5_tfnewblogtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_4_tfnewblogtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` like @lV60Newblogwwds_4_tfnewblogtitulo)");
         }
         else
         {
            GXv_int3[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newblogwwds_5_tfnewblogtitulo_sel)) && ! ( StringUtil.StrCmp(AV61Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` = @AV61Newblogwwds_5_tfnewblogtitulo_sel)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( StringUtil.StrCmp(AV61Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogTitulo`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV63Newblogwwds_7_tfnewblogsubtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_6_tfnewblogsubtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` like @lV62Newblogwwds_6_tfnewblogsubtitulo)");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Newblogwwds_7_tfnewblogsubtitulo_sel)) && ! ( StringUtil.StrCmp(AV63Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` = @AV63Newblogwwds_7_tfnewblogsubtitulo_sel)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( StringUtil.StrCmp(AV63Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogSubTitulo`))=0))");
         }
         if ( AV64Newblogwwds_8_tfnewblogdestacado_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 1)");
         }
         if ( AV64Newblogwwds_8_tfnewblogdestacado_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 0)");
         }
         if ( ! (0==AV65Newblogwwds_9_tfnewblogvisitas) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` >= @AV65Newblogwwds_9_tfnewblogvisitas)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV66Newblogwwds_10_tfnewblogvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` <= @AV66Newblogwwds_10_tfnewblogvisitas_to)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( AV67Newblogwwds_11_tfnewblogborrador_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 1)");
         }
         if ( AV67Newblogwwds_11_tfnewblogborrador_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 0)");
         }
         scmdbuf += sWhereString;
         if ( ( AV11OrderedBy == 1 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogTitulo`";
         }
         else if ( ( AV11OrderedBy == 1 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogTitulo` DESC";
         }
         else if ( ( AV11OrderedBy == 2 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogId`";
         }
         else if ( ( AV11OrderedBy == 2 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogId` DESC";
         }
         else if ( ( AV11OrderedBy == 3 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogSubTitulo`";
         }
         else if ( ( AV11OrderedBy == 3 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogSubTitulo` DESC";
         }
         else if ( ( AV11OrderedBy == 4 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogDestacado`";
         }
         else if ( ( AV11OrderedBy == 4 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogDestacado` DESC";
         }
         else if ( ( AV11OrderedBy == 5 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogVisitas`";
         }
         else if ( ( AV11OrderedBy == 5 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogVisitas` DESC";
         }
         else if ( ( AV11OrderedBy == 6 ) && ! AV12OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogBorrador`";
         }
         else if ( ( AV11OrderedBy == 6 ) && ( AV12OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogBorrador` DESC";
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
                     return conditional_P002J2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (bool)dynConstraints[16] , (short)dynConstraints[17] , (bool)dynConstraints[18] );
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
          Object[] prmP002J2;
          prmP002J2 = new Object[] {
          new ParDef("@lV57Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV57Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV57Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV57Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV58Newblogwwds_2_tfnewblogid",GXType.Int16,4,0) ,
          new ParDef("@AV59Newblogwwds_3_tfnewblogid_to",GXType.Int16,4,0) ,
          new ParDef("@lV60Newblogwwds_4_tfnewblogtitulo",GXType.Char,200,0) ,
          new ParDef("@AV61Newblogwwds_5_tfnewblogtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@lV62Newblogwwds_6_tfnewblogsubtitulo",GXType.Char,200,0) ,
          new ParDef("@AV63Newblogwwds_7_tfnewblogsubtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@AV65Newblogwwds_9_tfnewblogvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV66Newblogwwds_10_tfnewblogvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002J2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002J2,100, GxCacheFrequency.OFF ,true,false )
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
                ((bool[]) buf[0])[0] = rslt.getBool(1);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                ((bool[]) buf[2])[0] = rslt.getBool(3);
                ((short[]) buf[3])[0] = rslt.getShort(4);
                ((string[]) buf[4])[0] = rslt.getVarchar(5);
                ((string[]) buf[5])[0] = rslt.getVarchar(6);
                ((string[]) buf[6])[0] = rslt.getMultimediaUri(7);
                ((string[]) buf[7])[0] = rslt.getMultimediaFile(8, rslt.getVarchar(7));
                return;
       }
    }

 }

}
