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
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   public class newblogww : GXDataArea
   {
      public newblogww( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newblogww( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         cmbavGridactions = new GXCombobox();
         chkNewBlogDestacado = new GXCheckbox();
         chkNewBlogBorrador = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetNextPar( );
            gxfirstwebparm_bkp = gxfirstwebparm;
            gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
            {
               setAjaxCallMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               dyncall( GetNextPar( )) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
            {
               setAjaxEventMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Grid") == 0 )
            {
               gxnrGrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Grid") == 0 )
            {
               gxgrGrid_refresh_invoke( ) ;
               return  ;
            }
            else
            {
               if ( ! IsValidAjaxCall( false) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = gxfirstwebparm_bkp;
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrGrid_newrow_invoke( )
      {
         nRC_GXsfl_44 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_44"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_44_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_44_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_44_idx = GetPar( "sGXsfl_44_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGrid_newrow( ) ;
         /* End function gxnrGrid_newrow_invoke */
      }

      protected void gxgrGrid_refresh_invoke( )
      {
         subGrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV13OrderedBy = (short)(Math.Round(NumberUtil.Val( GetPar( "OrderedBy"), "."), 18, MidpointRounding.ToEven));
         AV14OrderedDsc = StringUtil.StrToBool( GetPar( "OrderedDsc"));
         AV16FilterFullText = GetPar( "FilterFullText");
         AV26ManageFiltersExecutionStep = (short)(Math.Round(NumberUtil.Val( GetPar( "ManageFiltersExecutionStep"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV21ColumnsSelector);
         AV55Pgmname = GetPar( "Pgmname");
         AV27TFNewBlogId = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewBlogId"), "."), 18, MidpointRounding.ToEven));
         AV28TFNewBlogId_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewBlogId_To"), "."), 18, MidpointRounding.ToEven));
         AV29TFNewBlogTitulo = GetPar( "TFNewBlogTitulo");
         AV30TFNewBlogTitulo_Sel = GetPar( "TFNewBlogTitulo_Sel");
         AV31TFNewBlogSubTitulo = GetPar( "TFNewBlogSubTitulo");
         AV32TFNewBlogSubTitulo_Sel = GetPar( "TFNewBlogSubTitulo_Sel");
         AV51TFNewBlogDestacado_Sel = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewBlogDestacado_Sel"), "."), 18, MidpointRounding.ToEven));
         AV52TFNewBlogVisitas = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewBlogVisitas"), "."), 18, MidpointRounding.ToEven));
         AV53TFNewBlogVisitas_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewBlogVisitas_To"), "."), 18, MidpointRounding.ToEven));
         AV54TFNewBlogBorrador_Sel = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewBlogBorrador_Sel"), "."), 18, MidpointRounding.ToEven));
         AV43IsAuthorized_Display = StringUtil.StrToBool( GetPar( "IsAuthorized_Display"));
         AV44IsAuthorized_Update = StringUtil.StrToBool( GetPar( "IsAuthorized_Update"));
         AV45IsAuthorized_Delete = StringUtil.StrToBool( GetPar( "IsAuthorized_Delete"));
         AV48IsAuthorized_Insert = StringUtil.StrToBool( GetPar( "IsAuthorized_Insert"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV55Pgmname, AV27TFNewBlogId, AV28TFNewBlogId_To, AV29TFNewBlogTitulo, AV30TFNewBlogTitulo_Sel, AV31TFNewBlogSubTitulo, AV32TFNewBlogSubTitulo_Sel, AV51TFNewBlogDestacado_Sel, AV52TFNewBlogVisitas, AV53TFNewBlogVisitas_To, AV54TFNewBlogBorrador_Sel, AV43IsAuthorized_Display, AV44IsAuthorized_Update, AV45IsAuthorized_Delete, AV48IsAuthorized_Insert) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGrid_refresh_invoke */
      }

      protected override bool IntegratedSecurityEnabled
      {
         get {
            return true ;
         }

      }

      protected override GAMSecurityLevel IntegratedSecurityLevel
      {
         get {
            return GAMSecurityLevel.SecurityHigh ;
         }

      }

      protected override string ExecutePermissionPrefix
      {
         get {
            return "newblogww_Execute" ;
         }

      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("wwpbaseobjects.workwithplusmasterpage", "DesignSystem.Programs.wwpbaseobjects.workwithplusmasterpage", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA2R2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START2R2( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1318140), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("Window/InNewWindowRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("newblogww.aspx") +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV55Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV55Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV43IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV43IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV44IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV44IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV45IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV45IsAuthorized_Delete, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV48IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV48IsAuthorized_Insert, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         GxWebStd.gx_hidden_field( context, "GXH_vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GXH_vORDEREDDSC", StringUtil.BoolToStr( AV14OrderedDsc));
         GxWebStd.gx_hidden_field( context, "GXH_vFILTERFULLTEXT", AV16FilterFullText);
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_44", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_44), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vMANAGEFILTERSDATA", AV24ManageFiltersData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vMANAGEFILTERSDATA", AV24ManageFiltersData);
         }
         GxWebStd.gx_hidden_field( context, "vGRIDCURRENTPAGE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV39GridCurrentPage), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV40GridPageCount), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDAPPLIEDFILTERS", AV41GridAppliedFilters);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vAGEXPORTDATA", AV46AGExportData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vAGEXPORTDATA", AV46AGExportData);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV35DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV35DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCOLUMNSSELECTOR", AV21ColumnsSelector);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCOLUMNSSELECTOR", AV21ColumnsSelector);
         }
         GxWebStd.gx_hidden_field( context, "vMANAGEFILTERSEXECUTIONSTEP", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV26ManageFiltersExecutionStep), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV55Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV55Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV27TFNewBlogId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGID_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV28TFNewBlogId_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGTITULO", AV29TFNewBlogTitulo);
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGTITULO_SEL", AV30TFNewBlogTitulo_Sel);
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGSUBTITULO", AV31TFNewBlogSubTitulo);
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGSUBTITULO_SEL", AV32TFNewBlogSubTitulo_Sel);
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGDESTACADO_SEL", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV51TFNewBlogDestacado_Sel), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGVISITAS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV52TFNewBlogVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGVISITAS_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV53TFNewBlogVisitas_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWBLOGBORRADOR_SEL", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV54TFNewBlogBorrador_Sel), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, "vORDEREDDSC", AV14OrderedDsc);
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV43IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV43IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV44IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV44IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV45IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV45IsAuthorized_Delete, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGRIDSTATE", AV11GridState);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGRIDSTATE", AV11GridState);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV48IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV48IsAuthorized_Insert, context));
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Icontype", StringUtil.RTrim( Ddo_managefilters_Icontype));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Icon", StringUtil.RTrim( Ddo_managefilters_Icon));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Tooltip", StringUtil.RTrim( Ddo_managefilters_Tooltip));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Cls", StringUtil.RTrim( Ddo_managefilters_Cls));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Class", StringUtil.RTrim( Gridpaginationbar_Class));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Showfirst", StringUtil.BoolToStr( Gridpaginationbar_Showfirst));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Showprevious", StringUtil.BoolToStr( Gridpaginationbar_Showprevious));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Shownext", StringUtil.BoolToStr( Gridpaginationbar_Shownext));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Showlast", StringUtil.BoolToStr( Gridpaginationbar_Showlast));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Pagestoshow", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Pagestoshow), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Pagingbuttonsposition", StringUtil.RTrim( Gridpaginationbar_Pagingbuttonsposition));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Pagingcaptionposition", StringUtil.RTrim( Gridpaginationbar_Pagingcaptionposition));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Emptygridclass", StringUtil.RTrim( Gridpaginationbar_Emptygridclass));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselector", StringUtil.BoolToStr( Gridpaginationbar_Rowsperpageselector));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageoptions", StringUtil.RTrim( Gridpaginationbar_Rowsperpageoptions));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Previous", StringUtil.RTrim( Gridpaginationbar_Previous));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Next", StringUtil.RTrim( Gridpaginationbar_Next));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Caption", StringUtil.RTrim( Gridpaginationbar_Caption));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Emptygridcaption", StringUtil.RTrim( Gridpaginationbar_Emptygridcaption));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpagecaption", StringUtil.RTrim( Gridpaginationbar_Rowsperpagecaption));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Width", StringUtil.RTrim( Dvpanel_unnamedtable1_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable1_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable1_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Cls", StringUtil.RTrim( Dvpanel_unnamedtable1_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Title", StringUtil.RTrim( Dvpanel_unnamedtable1_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable1_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable1_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable1_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable1_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE1_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable1_Autoscroll));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Icontype", StringUtil.RTrim( Ddo_agexport_Icontype));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Icon", StringUtil.RTrim( Ddo_agexport_Icon));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Caption", StringUtil.RTrim( Ddo_agexport_Caption));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Cls", StringUtil.RTrim( Ddo_agexport_Cls));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Titlecontrolidtoreplace", StringUtil.RTrim( Ddo_agexport_Titlecontrolidtoreplace));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Caption", StringUtil.RTrim( Ddo_grid_Caption));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_set", StringUtil.RTrim( Ddo_grid_Filteredtext_set));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtextto_set", StringUtil.RTrim( Ddo_grid_Filteredtextto_set));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedvalue_set", StringUtil.RTrim( Ddo_grid_Selectedvalue_set));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Gamoauthtoken", StringUtil.RTrim( Ddo_grid_Gamoauthtoken));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Gridinternalname", StringUtil.RTrim( Ddo_grid_Gridinternalname));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Columnids", StringUtil.RTrim( Ddo_grid_Columnids));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Columnssortvalues", StringUtil.RTrim( Ddo_grid_Columnssortvalues));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Includesortasc", StringUtil.RTrim( Ddo_grid_Includesortasc));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Fixable", StringUtil.RTrim( Ddo_grid_Fixable));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Sortedstatus", StringUtil.RTrim( Ddo_grid_Sortedstatus));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Includefilter", StringUtil.RTrim( Ddo_grid_Includefilter));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filtertype", StringUtil.RTrim( Ddo_grid_Filtertype));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filterisrange", StringUtil.RTrim( Ddo_grid_Filterisrange));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Includedatalist", StringUtil.RTrim( Ddo_grid_Includedatalist));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Datalisttype", StringUtil.RTrim( Ddo_grid_Datalisttype));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Datalistfixedvalues", StringUtil.RTrim( Ddo_grid_Datalistfixedvalues));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Datalistproc", StringUtil.RTrim( Ddo_grid_Datalistproc));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Format", StringUtil.RTrim( Ddo_grid_Format));
         GxWebStd.gx_hidden_field( context, "INNEWWINDOW1_Width", StringUtil.RTrim( Innewwindow1_Width));
         GxWebStd.gx_hidden_field( context, "INNEWWINDOW1_Height", StringUtil.RTrim( Innewwindow1_Height));
         GxWebStd.gx_hidden_field( context, "INNEWWINDOW1_Target", StringUtil.RTrim( Innewwindow1_Target));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Icontype", StringUtil.RTrim( Ddo_gridcolumnsselector_Icontype));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Icon", StringUtil.RTrim( Ddo_gridcolumnsselector_Icon));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Caption", StringUtil.RTrim( Ddo_gridcolumnsselector_Caption));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Tooltip", StringUtil.RTrim( Ddo_gridcolumnsselector_Tooltip));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Cls", StringUtil.RTrim( Ddo_gridcolumnsselector_Cls));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Dropdownoptionstype", StringUtil.RTrim( Ddo_gridcolumnsselector_Dropdownoptionstype));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Gridinternalname", StringUtil.RTrim( Ddo_gridcolumnsselector_Gridinternalname));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Titlecontrolidtoreplace", StringUtil.RTrim( Ddo_gridcolumnsselector_Titlecontrolidtoreplace));
         GxWebStd.gx_hidden_field( context, "GRID_EMPOWERER_Gridinternalname", StringUtil.RTrim( Grid_empowerer_Gridinternalname));
         GxWebStd.gx_hidden_field( context, "GRID_EMPOWERER_Hastitlesettings", StringUtil.BoolToStr( Grid_empowerer_Hastitlesettings));
         GxWebStd.gx_hidden_field( context, "GRID_EMPOWERER_Hascolumnsselector", StringUtil.BoolToStr( Grid_empowerer_Hascolumnsselector));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Activeeventkey", StringUtil.RTrim( Ddo_grid_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedvalue_get", StringUtil.RTrim( Ddo_grid_Selectedvalue_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtextto_get", StringUtil.RTrim( Ddo_grid_Filteredtextto_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Activeeventkey", StringUtil.RTrim( Ddo_agexport_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Activeeventkey", StringUtil.RTrim( Ddo_grid_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedvalue_get", StringUtil.RTrim( Ddo_grid_Selectedvalue_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtextto_get", StringUtil.RTrim( Ddo_grid_Filteredtextto_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Activeeventkey", StringUtil.RTrim( Ddo_agexport_Activeeventkey));
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
         context.WriteHtmlText( "<script type=\"text/javascript\">") ;
         context.WriteHtmlText( "gx.setLanguageCode(\""+context.GetLanguageProperty( "code")+"\");") ;
         if ( ! context.isSpaRequest( ) )
         {
            context.WriteHtmlText( "gx.setDateFormat(\""+context.GetLanguageProperty( "date_fmt")+"\");") ;
            context.WriteHtmlText( "gx.setTimeFormat("+context.GetLanguageProperty( "time_fmt")+");") ;
            context.WriteHtmlText( "gx.setCenturyFirstYear("+40+");") ;
            context.WriteHtmlText( "gx.setDecimalPoint(\""+context.GetLanguageProperty( "decimal_point")+"\");") ;
            context.WriteHtmlText( "gx.setThousandSeparator(\""+context.GetLanguageProperty( "thousand_sep")+"\");") ;
            context.WriteHtmlText( "gx.StorageTimeZone = "+2+";") ;
         }
         context.WriteHtmlText( "</script>") ;
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE2R2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT2R2( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("newblogww.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "NewBlogWW" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( " New Blog", "") ;
      }

      protected void WB2R0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"bootstrapv3\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divLayoutmaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMain", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable1.SetProperty("Width", Dvpanel_unnamedtable1_Width);
            ucDvpanel_unnamedtable1.SetProperty("AutoWidth", Dvpanel_unnamedtable1_Autowidth);
            ucDvpanel_unnamedtable1.SetProperty("AutoHeight", Dvpanel_unnamedtable1_Autoheight);
            ucDvpanel_unnamedtable1.SetProperty("Cls", Dvpanel_unnamedtable1_Cls);
            ucDvpanel_unnamedtable1.SetProperty("Title", Dvpanel_unnamedtable1_Title);
            ucDvpanel_unnamedtable1.SetProperty("Collapsible", Dvpanel_unnamedtable1_Collapsible);
            ucDvpanel_unnamedtable1.SetProperty("Collapsed", Dvpanel_unnamedtable1_Collapsed);
            ucDvpanel_unnamedtable1.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable1_Showcollapseicon);
            ucDvpanel_unnamedtable1.SetProperty("IconPosition", Dvpanel_unnamedtable1_Iconposition);
            ucDvpanel_unnamedtable1.SetProperty("AutoScroll", Dvpanel_unnamedtable1_Autoscroll);
            ucDvpanel_unnamedtable1.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable1_Internalname, "DVPANEL_UNNAMEDTABLE1Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_UNNAMEDTABLE1Container"+"UnnamedTable1"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingBottom", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheader_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-direction:column;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheadercontent_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-wrap:wrap;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableactions_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "align-self:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroupColorFilledActions", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtninsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "GXM_insert", ""), bttBtninsert_Jsonclick, 5, context.GetMessage( "GXM_insert", ""), "", StyleString, ClassString, bttBtninsert_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOINSERT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewBlogWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'',false,'',0)\"";
            ClassString = "ColumnsSelector";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnagexport_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "WWP_ExportData", ""), bttBtnagexport_Jsonclick, 0, context.GetMessage( "WWP_ExportData", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_NewBlogWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
            ClassString = "hidden-xs";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtneditcolumns_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "WWP_EditColumnsCaption", ""), bttBtneditcolumns_Jsonclick, 0, context.GetMessage( "WWP_EditColumnsTooltip", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_NewBlogWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "align-self:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablerightheader_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* User Defined Control */
            ucDdo_managefilters.SetProperty("IconType", Ddo_managefilters_Icontype);
            ucDdo_managefilters.SetProperty("Icon", Ddo_managefilters_Icon);
            ucDdo_managefilters.SetProperty("Caption", Ddo_managefilters_Caption);
            ucDdo_managefilters.SetProperty("Tooltip", Ddo_managefilters_Tooltip);
            ucDdo_managefilters.SetProperty("Cls", Ddo_managefilters_Cls);
            ucDdo_managefilters.SetProperty("DropDownOptionsData", AV24ManageFiltersData);
            ucDdo_managefilters.Render(context, "dvelop.gxbootstrap.ddoregular", Ddo_managefilters_Internalname, "DDO_MANAGEFILTERSContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;align-self:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablefilters_Internalname, 1, 0, "px", 0, "px", "TableFilters", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavFilterfulltext_Internalname, context.GetMessage( "Filter Full Text", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'" + sGXsfl_44_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFilterfulltext_Internalname, AV16FilterFullText, StringUtil.RTrim( context.localUtil.Format( AV16FilterFullText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,35);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "WWP_Search", ""), edtavFilterfulltext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFilterfulltext_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "WWPFullTextFilter", "start", true, "", "HLP_NewBlogWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            ClassString = "ErrorViewer";
            StyleString = "";
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, StyleString, ClassString, "", "false");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SectionGrid GridNoBorderCell HasGridEmpowerer", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGridtablewithpaginationbar_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridContainer.SetWrapped(nGXWrapped);
            StartGridControl44( ) ;
         }
         if ( wbEnd == 44 )
         {
            wbEnd = 0;
            nRC_GXsfl_44 = (int)(nGXsfl_44_idx-1);
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid", GridContainer, subGrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridContainerData", GridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridContainerData"+"V", GridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucGridpaginationbar.SetProperty("Class", Gridpaginationbar_Class);
            ucGridpaginationbar.SetProperty("ShowFirst", Gridpaginationbar_Showfirst);
            ucGridpaginationbar.SetProperty("ShowPrevious", Gridpaginationbar_Showprevious);
            ucGridpaginationbar.SetProperty("ShowNext", Gridpaginationbar_Shownext);
            ucGridpaginationbar.SetProperty("ShowLast", Gridpaginationbar_Showlast);
            ucGridpaginationbar.SetProperty("PagesToShow", Gridpaginationbar_Pagestoshow);
            ucGridpaginationbar.SetProperty("PagingButtonsPosition", Gridpaginationbar_Pagingbuttonsposition);
            ucGridpaginationbar.SetProperty("PagingCaptionPosition", Gridpaginationbar_Pagingcaptionposition);
            ucGridpaginationbar.SetProperty("EmptyGridClass", Gridpaginationbar_Emptygridclass);
            ucGridpaginationbar.SetProperty("RowsPerPageSelector", Gridpaginationbar_Rowsperpageselector);
            ucGridpaginationbar.SetProperty("RowsPerPageOptions", Gridpaginationbar_Rowsperpageoptions);
            ucGridpaginationbar.SetProperty("Previous", Gridpaginationbar_Previous);
            ucGridpaginationbar.SetProperty("Next", Gridpaginationbar_Next);
            ucGridpaginationbar.SetProperty("Caption", Gridpaginationbar_Caption);
            ucGridpaginationbar.SetProperty("EmptyGridCaption", Gridpaginationbar_Emptygridcaption);
            ucGridpaginationbar.SetProperty("RowsPerPageCaption", Gridpaginationbar_Rowsperpagecaption);
            ucGridpaginationbar.SetProperty("CurrentPage", AV39GridCurrentPage);
            ucGridpaginationbar.SetProperty("PageCount", AV40GridPageCount);
            ucGridpaginationbar.SetProperty("AppliedFilters", AV41GridAppliedFilters);
            ucGridpaginationbar.Render(context, "dvelop.dvpaginationbar", Gridpaginationbar_Internalname, "GRIDPAGINATIONBARContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divHtml_bottomauxiliarcontrols_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDdo_agexport.SetProperty("IconType", Ddo_agexport_Icontype);
            ucDdo_agexport.SetProperty("Icon", Ddo_agexport_Icon);
            ucDdo_agexport.SetProperty("Caption", Ddo_agexport_Caption);
            ucDdo_agexport.SetProperty("Cls", Ddo_agexport_Cls);
            ucDdo_agexport.SetProperty("DropDownOptionsData", AV46AGExportData);
            ucDdo_agexport.Render(context, "dvelop.gxbootstrap.ddoregular", Ddo_agexport_Internalname, "DDO_AGEXPORTContainer");
            /* User Defined Control */
            ucDdo_grid.SetProperty("Caption", Ddo_grid_Caption);
            ucDdo_grid.SetProperty("ColumnIds", Ddo_grid_Columnids);
            ucDdo_grid.SetProperty("ColumnsSortValues", Ddo_grid_Columnssortvalues);
            ucDdo_grid.SetProperty("IncludeSortASC", Ddo_grid_Includesortasc);
            ucDdo_grid.SetProperty("Fixable", Ddo_grid_Fixable);
            ucDdo_grid.SetProperty("IncludeFilter", Ddo_grid_Includefilter);
            ucDdo_grid.SetProperty("FilterType", Ddo_grid_Filtertype);
            ucDdo_grid.SetProperty("FilterIsRange", Ddo_grid_Filterisrange);
            ucDdo_grid.SetProperty("IncludeDataList", Ddo_grid_Includedatalist);
            ucDdo_grid.SetProperty("DataListType", Ddo_grid_Datalisttype);
            ucDdo_grid.SetProperty("DataListFixedValues", Ddo_grid_Datalistfixedvalues);
            ucDdo_grid.SetProperty("DataListProc", Ddo_grid_Datalistproc);
            ucDdo_grid.SetProperty("Format", Ddo_grid_Format);
            ucDdo_grid.SetProperty("DropDownOptionsTitleSettingsIcons", AV35DDO_TitleSettingsIcons);
            ucDdo_grid.Render(context, "dvelop.gxbootstrap.ddogridtitlesettingsm", Ddo_grid_Internalname, "DDO_GRIDContainer");
            /* User Defined Control */
            ucInnewwindow1.Render(context, "innewwindow", Innewwindow1_Internalname, "INNEWWINDOW1Container");
            /* User Defined Control */
            ucDdo_gridcolumnsselector.SetProperty("IconType", Ddo_gridcolumnsselector_Icontype);
            ucDdo_gridcolumnsselector.SetProperty("Icon", Ddo_gridcolumnsselector_Icon);
            ucDdo_gridcolumnsselector.SetProperty("Caption", Ddo_gridcolumnsselector_Caption);
            ucDdo_gridcolumnsselector.SetProperty("Tooltip", Ddo_gridcolumnsselector_Tooltip);
            ucDdo_gridcolumnsselector.SetProperty("Cls", Ddo_gridcolumnsselector_Cls);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsType", Ddo_gridcolumnsselector_Dropdownoptionstype);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsTitleSettingsIcons", AV35DDO_TitleSettingsIcons);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsData", AV21ColumnsSelector);
            ucDdo_gridcolumnsselector.Render(context, "dvelop.gxbootstrap.ddogridcolumnsselector", Ddo_gridcolumnsselector_Internalname, "DDO_GRIDCOLUMNSSELECTORContainer");
            /* User Defined Control */
            ucGrid_empowerer.SetProperty("HasTitleSettings", Grid_empowerer_Hastitlesettings);
            ucGrid_empowerer.SetProperty("HasColumnsSelector", Grid_empowerer_Hascolumnsselector);
            ucGrid_empowerer.Render(context, "wwp.gridempowerer", Grid_empowerer_Internalname, "GRID_EMPOWERERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 44 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid", GridContainer, subGrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridContainerData", GridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridContainerData"+"V", GridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2R2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
            }
         }
         Form.Meta.addItem("description", context.GetMessage( " New Blog", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2R0( ) ;
      }

      protected void WS2R2( )
      {
         START2R2( ) ;
         EVT2R2( ) ;
      }

      protected void EVT2R2( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
               if ( StringUtil.Len( sEvt) > 0 )
               {
                  sEvtType = StringUtil.Left( sEvt, 1);
                  sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_MANAGEFILTERS.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_managefilters.Onoptionclicked */
                              E112R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changepage */
                              E122R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changerowsperpage */
                              E132R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_AGEXPORT.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_agexport.Onoptionclicked */
                              E142R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRID.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_grid.Onoptionclicked */
                              E152R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_gridcolumnsselector.Oncolumnschanged */
                              E162R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOINSERT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoInsert' */
                              E172R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "GRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "VGRIDACTIONS.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "VGRIDACTIONS.CLICK") == 0 ) )
                           {
                              nGXsfl_44_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
                              SubsflControlProps_442( ) ;
                              cmbavGridactions.Name = cmbavGridactions_Internalname;
                              cmbavGridactions.CurrentValue = cgiGet( cmbavGridactions_Internalname);
                              AV42GridActions = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavGridactions_Internalname), "."), 18, MidpointRounding.ToEven));
                              AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV42GridActions), 4, 0));
                              A12NewBlogId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewBlogId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A13NewBlogImagen = cgiGet( edtNewBlogImagen_Internalname);
                              AssignProp("", false, edtNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), !bGXsfl_44_Refreshing);
                              AssignProp("", false, edtNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
                              A14NewBlogTitulo = cgiGet( edtNewBlogTitulo_Internalname);
                              A15NewBlogSubTitulo = cgiGet( edtNewBlogSubTitulo_Internalname);
                              A19NewBlogDestacado = StringUtil.StrToBool( cgiGet( chkNewBlogDestacado_Internalname));
                              A18NewBlogVisitas = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewBlogVisitas_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A25NewBlogBorrador = StringUtil.StrToBool( cgiGet( chkNewBlogBorrador_Internalname));
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E182R2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E192R2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid.Load */
                                    E202R2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VGRIDACTIONS.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E212R2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       /* Set Refresh If Orderedby Changed */
                                       if ( ( context.localUtil.CToN( cgiGet( "GXH_vORDEREDBY"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) != Convert.ToDecimal( AV13OrderedBy )) )
                                       {
                                          Rfr0gs = true;
                                       }
                                       /* Set Refresh If Ordereddsc Changed */
                                       if ( StringUtil.StrToBool( cgiGet( "GXH_vORDEREDDSC")) != AV14OrderedDsc )
                                       {
                                          Rfr0gs = true;
                                       }
                                       /* Set Refresh If Filterfulltext Changed */
                                       if ( StringUtil.StrCmp(cgiGet( "GXH_vFILTERFULLTEXT"), AV16FilterFullText) != 0 )
                                       {
                                          Rfr0gs = true;
                                       }
                                       if ( ! Rfr0gs )
                                       {
                                       }
                                       dynload_actions( ) ;
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE2R2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA2R2( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = edtavFilterfulltext_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_442( ) ;
         while ( nGXsfl_44_idx <= nRC_GXsfl_44 )
         {
            sendrow_442( ) ;
            nGXsfl_44_idx = ((subGrid_Islastpage==1)&&(nGXsfl_44_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_44_idx+1);
            sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
            SubsflControlProps_442( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridContainer)) ;
         /* End function gxnrGrid_newrow */
      }

      protected void gxgrGrid_refresh( int subGrid_Rows ,
                                       short AV13OrderedBy ,
                                       bool AV14OrderedDsc ,
                                       string AV16FilterFullText ,
                                       short AV26ManageFiltersExecutionStep ,
                                       DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV21ColumnsSelector ,
                                       string AV55Pgmname ,
                                       short AV27TFNewBlogId ,
                                       short AV28TFNewBlogId_To ,
                                       string AV29TFNewBlogTitulo ,
                                       string AV30TFNewBlogTitulo_Sel ,
                                       string AV31TFNewBlogSubTitulo ,
                                       string AV32TFNewBlogSubTitulo_Sel ,
                                       short AV51TFNewBlogDestacado_Sel ,
                                       short AV52TFNewBlogVisitas ,
                                       short AV53TFNewBlogVisitas_To ,
                                       short AV54TFNewBlogBorrador_Sel ,
                                       bool AV43IsAuthorized_Display ,
                                       bool AV44IsAuthorized_Update ,
                                       bool AV45IsAuthorized_Delete ,
                                       bool AV48IsAuthorized_Insert )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RF2R2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_NEWBLOGID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "NEWBLOGID", StringUtil.LTrim( StringUtil.NToC( (decimal)(A12NewBlogId), 4, 0, ".", "")));
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF2R2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV55Pgmname = "NewBlogWW";
      }

      protected void RF2R2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 44;
         /* Execute user event: Refresh */
         E192R2 ();
         nGXsfl_44_idx = 1;
         sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
         SubsflControlProps_442( ) ;
         bGXsfl_44_Refreshing = true;
         GridContainer.AddObjectProperty("GridName", "Grid");
         GridContainer.AddObjectProperty("CmpContext", "");
         GridContainer.AddObjectProperty("InMasterPage", "false");
         GridContainer.AddObjectProperty("Class", "GridWithPaginationBar GridNoBorder WorkWith");
         GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
         GridContainer.AddObjectProperty("Sortable", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Sortable), 1, 0, ".", "")));
         GridContainer.PageSize = subGrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_442( ) ;
            GXPagingFrom2 = (int)(((subGrid_Rows==0) ? 0 : GRID_nFirstRecordOnPage));
            GXPagingTo2 = ((subGrid_Rows==0) ? 10000 : subGrid_fnc_Recordsperpage( )+1);
            pr_default.dynParam(0, new Object[]{ new Object[]{
                                                 AV56Newblogwwds_1_filterfulltext ,
                                                 AV57Newblogwwds_2_tfnewblogid ,
                                                 AV58Newblogwwds_3_tfnewblogid_to ,
                                                 AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                                 AV59Newblogwwds_4_tfnewblogtitulo ,
                                                 AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                                 AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                                 AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                                 AV64Newblogwwds_9_tfnewblogvisitas ,
                                                 AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                                 AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                                 A12NewBlogId ,
                                                 A14NewBlogTitulo ,
                                                 A15NewBlogSubTitulo ,
                                                 A18NewBlogVisitas ,
                                                 A19NewBlogDestacado ,
                                                 A25NewBlogBorrador ,
                                                 AV13OrderedBy ,
                                                 AV14OrderedDsc } ,
                                                 new int[]{
                                                 TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.BOOLEAN,
                                                 TypeConstants.SHORT, TypeConstants.BOOLEAN
                                                 }
            });
            lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
            lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
            lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
            lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
            lV59Newblogwwds_4_tfnewblogtitulo = StringUtil.Concat( StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo), "%", "");
            lV61Newblogwwds_6_tfnewblogsubtitulo = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo), "%", "");
            /* Using cursor H002R2 */
            pr_default.execute(0, new Object[] {lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, AV57Newblogwwds_2_tfnewblogid, AV58Newblogwwds_3_tfnewblogid_to, lV59Newblogwwds_4_tfnewblogtitulo, AV60Newblogwwds_5_tfnewblogtitulo_sel, lV61Newblogwwds_6_tfnewblogsubtitulo, AV62Newblogwwds_7_tfnewblogsubtitulo_sel, AV64Newblogwwds_9_tfnewblogvisitas, AV65Newblogwwds_10_tfnewblogvisitas_to, GXPagingFrom2, GXPagingTo2});
            nGXsfl_44_idx = 1;
            sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
            SubsflControlProps_442( ) ;
            while ( ( (pr_default.getStatus(0) != 101) ) && ( ( ( subGrid_Rows == 0 ) || ( GRID_nCurrentRecord < subGrid_fnc_Recordsperpage( ) ) ) ) )
            {
               A25NewBlogBorrador = H002R2_A25NewBlogBorrador[0];
               A18NewBlogVisitas = H002R2_A18NewBlogVisitas[0];
               A19NewBlogDestacado = H002R2_A19NewBlogDestacado[0];
               A15NewBlogSubTitulo = H002R2_A15NewBlogSubTitulo[0];
               A14NewBlogTitulo = H002R2_A14NewBlogTitulo[0];
               A40000NewBlogImagen_GXI = H002R2_A40000NewBlogImagen_GXI[0];
               AssignProp("", false, edtNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), !bGXsfl_44_Refreshing);
               AssignProp("", false, edtNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
               A12NewBlogId = H002R2_A12NewBlogId[0];
               A13NewBlogImagen = H002R2_A13NewBlogImagen[0];
               AssignProp("", false, edtNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), !bGXsfl_44_Refreshing);
               AssignProp("", false, edtNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
               /* Execute user event: Grid.Load */
               E202R2 ();
               pr_default.readNext(0);
            }
            GRID_nEOF = (short)(((pr_default.getStatus(0) == 101) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            pr_default.close(0);
            wbEnd = 44;
            WB2R0( ) ;
         }
         bGXsfl_44_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2R2( )
      {
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV55Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV55Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV43IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV43IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV44IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV44IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV45IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV45IsAuthorized_Delete, context));
         GxWebStd.gx_hidden_field( context, "gxhash_NEWBLOGID"+"_"+sGXsfl_44_idx, GetSecureSignedToken( sGXsfl_44_idx, context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV48IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV48IsAuthorized_Insert, context));
      }

      protected int subGrid_fnc_Pagecount( )
      {
         GRID_nRecordCount = subGrid_fnc_Recordcount( );
         if ( ((int)((GRID_nRecordCount) % (subGrid_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(GRID_nRecordCount/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(GRID_nRecordCount/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subGrid_fnc_Recordcount( )
      {
         AV56Newblogwwds_1_filterfulltext = AV16FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV27TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV28TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV29TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV30TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV31TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV32TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV51TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV52TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV53TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV54TFNewBlogBorrador_Sel;
         pr_default.dynParam(1, new Object[]{ new Object[]{
                                              AV56Newblogwwds_1_filterfulltext ,
                                              AV57Newblogwwds_2_tfnewblogid ,
                                              AV58Newblogwwds_3_tfnewblogid_to ,
                                              AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                              AV59Newblogwwds_4_tfnewblogtitulo ,
                                              AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                              AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                              AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                              AV64Newblogwwds_9_tfnewblogvisitas ,
                                              AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                              AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                              A12NewBlogId ,
                                              A14NewBlogTitulo ,
                                              A15NewBlogSubTitulo ,
                                              A18NewBlogVisitas ,
                                              A19NewBlogDestacado ,
                                              A25NewBlogBorrador ,
                                              AV13OrderedBy ,
                                              AV14OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.BOOLEAN,
                                              TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV59Newblogwwds_4_tfnewblogtitulo = StringUtil.Concat( StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo), "%", "");
         lV61Newblogwwds_6_tfnewblogsubtitulo = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo), "%", "");
         /* Using cursor H002R3 */
         pr_default.execute(1, new Object[] {lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, AV57Newblogwwds_2_tfnewblogid, AV58Newblogwwds_3_tfnewblogid_to, lV59Newblogwwds_4_tfnewblogtitulo, AV60Newblogwwds_5_tfnewblogtitulo_sel, lV61Newblogwwds_6_tfnewblogsubtitulo, AV62Newblogwwds_7_tfnewblogsubtitulo_sel, AV64Newblogwwds_9_tfnewblogvisitas, AV65Newblogwwds_10_tfnewblogvisitas_to});
         GRID_nRecordCount = H002R3_AGRID_nRecordCount[0];
         pr_default.close(1);
         return (int)(GRID_nRecordCount) ;
      }

      protected int subGrid_fnc_Recordsperpage( )
      {
         if ( subGrid_Rows > 0 )
         {
            return subGrid_Rows*1 ;
         }
         else
         {
            return (int)(-1) ;
         }
      }

      protected int subGrid_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(GRID_nFirstRecordOnPage/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subgrid_firstpage( )
      {
         AV56Newblogwwds_1_filterfulltext = AV16FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV27TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV28TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV29TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV30TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV31TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV32TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV51TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV52TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV53TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV54TFNewBlogBorrador_Sel;
         GRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV55Pgmname, AV27TFNewBlogId, AV28TFNewBlogId_To, AV29TFNewBlogTitulo, AV30TFNewBlogTitulo_Sel, AV31TFNewBlogSubTitulo, AV32TFNewBlogSubTitulo_Sel, AV51TFNewBlogDestacado_Sel, AV52TFNewBlogVisitas, AV53TFNewBlogVisitas_To, AV54TFNewBlogBorrador_Sel, AV43IsAuthorized_Display, AV44IsAuthorized_Update, AV45IsAuthorized_Delete, AV48IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_nextpage( )
      {
         AV56Newblogwwds_1_filterfulltext = AV16FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV27TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV28TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV29TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV30TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV31TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV32TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV51TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV52TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV53TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV54TFNewBlogBorrador_Sel;
         GRID_nRecordCount = subGrid_fnc_Recordcount( );
         if ( ( GRID_nRecordCount >= subGrid_fnc_Recordsperpage( ) ) && ( GRID_nEOF == 0 ) )
         {
            GRID_nFirstRecordOnPage = (long)(GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV55Pgmname, AV27TFNewBlogId, AV28TFNewBlogId_To, AV29TFNewBlogTitulo, AV30TFNewBlogTitulo_Sel, AV31TFNewBlogSubTitulo, AV32TFNewBlogSubTitulo_Sel, AV51TFNewBlogDestacado_Sel, AV52TFNewBlogVisitas, AV53TFNewBlogVisitas_To, AV54TFNewBlogBorrador_Sel, AV43IsAuthorized_Display, AV44IsAuthorized_Update, AV45IsAuthorized_Delete, AV48IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgrid_previouspage( )
      {
         AV56Newblogwwds_1_filterfulltext = AV16FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV27TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV28TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV29TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV30TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV31TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV32TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV51TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV52TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV53TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV54TFNewBlogBorrador_Sel;
         if ( GRID_nFirstRecordOnPage >= subGrid_fnc_Recordsperpage( ) )
         {
            GRID_nFirstRecordOnPage = (long)(GRID_nFirstRecordOnPage-subGrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV55Pgmname, AV27TFNewBlogId, AV28TFNewBlogId_To, AV29TFNewBlogTitulo, AV30TFNewBlogTitulo_Sel, AV31TFNewBlogSubTitulo, AV32TFNewBlogSubTitulo_Sel, AV51TFNewBlogDestacado_Sel, AV52TFNewBlogVisitas, AV53TFNewBlogVisitas_To, AV54TFNewBlogBorrador_Sel, AV43IsAuthorized_Display, AV44IsAuthorized_Update, AV45IsAuthorized_Delete, AV48IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         AV56Newblogwwds_1_filterfulltext = AV16FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV27TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV28TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV29TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV30TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV31TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV32TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV51TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV52TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV53TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV54TFNewBlogBorrador_Sel;
         GRID_nRecordCount = subGrid_fnc_Recordcount( );
         if ( GRID_nRecordCount > subGrid_fnc_Recordsperpage( ) )
         {
            if ( ((int)((GRID_nRecordCount) % (subGrid_fnc_Recordsperpage( )))) == 0 )
            {
               GRID_nFirstRecordOnPage = (long)(GRID_nRecordCount-subGrid_fnc_Recordsperpage( ));
            }
            else
            {
               GRID_nFirstRecordOnPage = (long)(GRID_nRecordCount-((int)((GRID_nRecordCount) % (subGrid_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            GRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV55Pgmname, AV27TFNewBlogId, AV28TFNewBlogId_To, AV29TFNewBlogTitulo, AV30TFNewBlogTitulo_Sel, AV31TFNewBlogSubTitulo, AV32TFNewBlogSubTitulo_Sel, AV51TFNewBlogDestacado_Sel, AV52TFNewBlogVisitas, AV53TFNewBlogVisitas_To, AV54TFNewBlogBorrador_Sel, AV43IsAuthorized_Display, AV44IsAuthorized_Update, AV45IsAuthorized_Delete, AV48IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgrid_gotopage( int nPageNo )
      {
         AV56Newblogwwds_1_filterfulltext = AV16FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV27TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV28TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV29TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV30TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV31TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV32TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV51TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV52TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV53TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV54TFNewBlogBorrador_Sel;
         if ( nPageNo > 0 )
         {
            GRID_nFirstRecordOnPage = (long)(subGrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV55Pgmname, AV27TFNewBlogId, AV28TFNewBlogId_To, AV29TFNewBlogTitulo, AV30TFNewBlogTitulo_Sel, AV31TFNewBlogSubTitulo, AV32TFNewBlogSubTitulo_Sel, AV51TFNewBlogDestacado_Sel, AV52TFNewBlogVisitas, AV53TFNewBlogVisitas_To, AV54TFNewBlogBorrador_Sel, AV43IsAuthorized_Display, AV44IsAuthorized_Update, AV45IsAuthorized_Delete, AV48IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV55Pgmname = "NewBlogWW";
         edtNewBlogId_Enabled = 0;
         edtNewBlogImagen_Enabled = 0;
         edtNewBlogTitulo_Enabled = 0;
         edtNewBlogSubTitulo_Enabled = 0;
         chkNewBlogDestacado.Enabled = 0;
         edtNewBlogVisitas_Enabled = 0;
         chkNewBlogBorrador.Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2R0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E182R2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vMANAGEFILTERSDATA"), AV24ManageFiltersData);
            ajax_req_read_hidden_sdt(cgiGet( "vAGEXPORTDATA"), AV46AGExportData);
            ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV35DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( "vCOLUMNSSELECTOR"), AV21ColumnsSelector);
            /* Read saved values. */
            nRC_GXsfl_44 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_44"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV39GridCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDCURRENTPAGE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV40GridPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDPAGECOUNT"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV41GridAppliedFilters = cgiGet( "vGRIDAPPLIEDFILTERS");
            GRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Ddo_managefilters_Icontype = cgiGet( "DDO_MANAGEFILTERS_Icontype");
            Ddo_managefilters_Icon = cgiGet( "DDO_MANAGEFILTERS_Icon");
            Ddo_managefilters_Tooltip = cgiGet( "DDO_MANAGEFILTERS_Tooltip");
            Ddo_managefilters_Cls = cgiGet( "DDO_MANAGEFILTERS_Cls");
            Gridpaginationbar_Class = cgiGet( "GRIDPAGINATIONBAR_Class");
            Gridpaginationbar_Showfirst = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Showfirst"));
            Gridpaginationbar_Showprevious = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Showprevious"));
            Gridpaginationbar_Shownext = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Shownext"));
            Gridpaginationbar_Showlast = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Showlast"));
            Gridpaginationbar_Pagestoshow = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDPAGINATIONBAR_Pagestoshow"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Pagingbuttonsposition = cgiGet( "GRIDPAGINATIONBAR_Pagingbuttonsposition");
            Gridpaginationbar_Pagingcaptionposition = cgiGet( "GRIDPAGINATIONBAR_Pagingcaptionposition");
            Gridpaginationbar_Emptygridclass = cgiGet( "GRIDPAGINATIONBAR_Emptygridclass");
            Gridpaginationbar_Rowsperpageselector = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Rowsperpageselector"));
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Rowsperpageoptions = cgiGet( "GRIDPAGINATIONBAR_Rowsperpageoptions");
            Gridpaginationbar_Previous = cgiGet( "GRIDPAGINATIONBAR_Previous");
            Gridpaginationbar_Next = cgiGet( "GRIDPAGINATIONBAR_Next");
            Gridpaginationbar_Caption = cgiGet( "GRIDPAGINATIONBAR_Caption");
            Gridpaginationbar_Emptygridcaption = cgiGet( "GRIDPAGINATIONBAR_Emptygridcaption");
            Gridpaginationbar_Rowsperpagecaption = cgiGet( "GRIDPAGINATIONBAR_Rowsperpagecaption");
            Dvpanel_unnamedtable1_Width = cgiGet( "DVPANEL_UNNAMEDTABLE1_Width");
            Dvpanel_unnamedtable1_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE1_Autowidth"));
            Dvpanel_unnamedtable1_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE1_Autoheight"));
            Dvpanel_unnamedtable1_Cls = cgiGet( "DVPANEL_UNNAMEDTABLE1_Cls");
            Dvpanel_unnamedtable1_Title = cgiGet( "DVPANEL_UNNAMEDTABLE1_Title");
            Dvpanel_unnamedtable1_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE1_Collapsible"));
            Dvpanel_unnamedtable1_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE1_Collapsed"));
            Dvpanel_unnamedtable1_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE1_Showcollapseicon"));
            Dvpanel_unnamedtable1_Iconposition = cgiGet( "DVPANEL_UNNAMEDTABLE1_Iconposition");
            Dvpanel_unnamedtable1_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE1_Autoscroll"));
            Ddo_agexport_Icontype = cgiGet( "DDO_AGEXPORT_Icontype");
            Ddo_agexport_Icon = cgiGet( "DDO_AGEXPORT_Icon");
            Ddo_agexport_Caption = cgiGet( "DDO_AGEXPORT_Caption");
            Ddo_agexport_Cls = cgiGet( "DDO_AGEXPORT_Cls");
            Ddo_agexport_Titlecontrolidtoreplace = cgiGet( "DDO_AGEXPORT_Titlecontrolidtoreplace");
            Ddo_grid_Caption = cgiGet( "DDO_GRID_Caption");
            Ddo_grid_Filteredtext_set = cgiGet( "DDO_GRID_Filteredtext_set");
            Ddo_grid_Filteredtextto_set = cgiGet( "DDO_GRID_Filteredtextto_set");
            Ddo_grid_Selectedvalue_set = cgiGet( "DDO_GRID_Selectedvalue_set");
            Ddo_grid_Gamoauthtoken = cgiGet( "DDO_GRID_Gamoauthtoken");
            Ddo_grid_Gridinternalname = cgiGet( "DDO_GRID_Gridinternalname");
            Ddo_grid_Columnids = cgiGet( "DDO_GRID_Columnids");
            Ddo_grid_Columnssortvalues = cgiGet( "DDO_GRID_Columnssortvalues");
            Ddo_grid_Includesortasc = cgiGet( "DDO_GRID_Includesortasc");
            Ddo_grid_Fixable = cgiGet( "DDO_GRID_Fixable");
            Ddo_grid_Sortedstatus = cgiGet( "DDO_GRID_Sortedstatus");
            Ddo_grid_Includefilter = cgiGet( "DDO_GRID_Includefilter");
            Ddo_grid_Filtertype = cgiGet( "DDO_GRID_Filtertype");
            Ddo_grid_Filterisrange = cgiGet( "DDO_GRID_Filterisrange");
            Ddo_grid_Includedatalist = cgiGet( "DDO_GRID_Includedatalist");
            Ddo_grid_Datalisttype = cgiGet( "DDO_GRID_Datalisttype");
            Ddo_grid_Datalistfixedvalues = cgiGet( "DDO_GRID_Datalistfixedvalues");
            Ddo_grid_Datalistproc = cgiGet( "DDO_GRID_Datalistproc");
            Ddo_grid_Format = cgiGet( "DDO_GRID_Format");
            Innewwindow1_Width = cgiGet( "INNEWWINDOW1_Width");
            Innewwindow1_Height = cgiGet( "INNEWWINDOW1_Height");
            Innewwindow1_Target = cgiGet( "INNEWWINDOW1_Target");
            Ddo_gridcolumnsselector_Icontype = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Icontype");
            Ddo_gridcolumnsselector_Icon = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Icon");
            Ddo_gridcolumnsselector_Caption = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Caption");
            Ddo_gridcolumnsselector_Tooltip = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Tooltip");
            Ddo_gridcolumnsselector_Cls = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Cls");
            Ddo_gridcolumnsselector_Dropdownoptionstype = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Dropdownoptionstype");
            Ddo_gridcolumnsselector_Gridinternalname = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Gridinternalname");
            Ddo_gridcolumnsselector_Titlecontrolidtoreplace = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Titlecontrolidtoreplace");
            Grid_empowerer_Gridinternalname = cgiGet( "GRID_EMPOWERER_Gridinternalname");
            Grid_empowerer_Hastitlesettings = StringUtil.StrToBool( cgiGet( "GRID_EMPOWERER_Hastitlesettings"));
            Grid_empowerer_Hascolumnsselector = StringUtil.StrToBool( cgiGet( "GRID_EMPOWERER_Hascolumnsselector"));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Gridpaginationbar_Selectedpage = cgiGet( "GRIDPAGINATIONBAR_Selectedpage");
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Ddo_grid_Activeeventkey = cgiGet( "DDO_GRID_Activeeventkey");
            Ddo_grid_Selectedvalue_get = cgiGet( "DDO_GRID_Selectedvalue_get");
            Ddo_grid_Filteredtextto_get = cgiGet( "DDO_GRID_Filteredtextto_get");
            Ddo_grid_Filteredtext_get = cgiGet( "DDO_GRID_Filteredtext_get");
            Ddo_grid_Selectedcolumn = cgiGet( "DDO_GRID_Selectedcolumn");
            Ddo_gridcolumnsselector_Columnsselectorvalues = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues");
            Ddo_managefilters_Activeeventkey = cgiGet( "DDO_MANAGEFILTERS_Activeeventkey");
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Ddo_agexport_Activeeventkey = cgiGet( "DDO_AGEXPORT_Activeeventkey");
            /* Read variables values. */
            AV16FilterFullText = cgiGet( edtavFilterfulltext_Internalname);
            AssignAttri("", false, "AV16FilterFullText", AV16FilterFullText);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
            if ( ( context.localUtil.CToN( cgiGet( "GXH_vORDEREDBY"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) != Convert.ToDecimal( AV13OrderedBy )) )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrToBool( cgiGet( "GXH_vORDEREDDSC")) != AV14OrderedDsc )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrCmp(cgiGet( "GXH_vFILTERFULLTEXT"), AV16FilterFullText) != 0 )
            {
               GRID_nFirstRecordOnPage = 0;
            }
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E182R2 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
      }

      protected void E182R2( )
      {
         /* Start Routine */
         returnInSub = false;
         subGrid_Rows = 10;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         Grid_empowerer_Gridinternalname = subGrid_Internalname;
         ucGrid_empowerer.SendProperty(context, "", false, Grid_empowerer_Internalname, "GridInternalName", Grid_empowerer_Gridinternalname);
         Ddo_gridcolumnsselector_Gridinternalname = subGrid_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, "", false, Ddo_gridcolumnsselector_Internalname, "GridInternalName", Ddo_gridcolumnsselector_Gridinternalname);
         if ( StringUtil.StrCmp(AV8HTTPRequest.Method, "GET") == 0 )
         {
            /* Execute user subroutine: 'LOADSAVEDFILTERS' */
            S112 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         Ddo_agexport_Titlecontrolidtoreplace = bttBtnagexport_Internalname;
         ucDdo_agexport.SendProperty(context, "", false, Ddo_agexport_Internalname, "TitleControlIdToReplace", Ddo_agexport_Titlecontrolidtoreplace);
         AV46AGExportData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV47AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV47AGExportDataItem.gxTpr_Title = context.GetMessage( "WWP_ExportCaption", "");
         AV47AGExportDataItem.gxTpr_Icon = context.convertURL( (string)(context.GetImagePath( "da69a816-fd11-445b-8aaf-1a2f7f1acc93", "", context.GetTheme( ))));
         AV47AGExportDataItem.gxTpr_Eventkey = "Export";
         AV47AGExportDataItem.gxTpr_Isdivider = false;
         AV46AGExportData.Add(AV47AGExportDataItem, 0);
         AV47AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV47AGExportDataItem.gxTpr_Title = context.GetMessage( "WWP_ExportReportCaption", "");
         AV47AGExportDataItem.gxTpr_Icon = context.convertURL( (string)(context.GetImagePath( "776fb79c-a0a1-4302-b5e5-d773dbe1a297", "", context.GetTheme( ))));
         AV47AGExportDataItem.gxTpr_Eventkey = "ExportReport";
         AV47AGExportDataItem.gxTpr_Isdivider = false;
         AV46AGExportData.Add(AV47AGExportDataItem, 0);
         AV36GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV37GAMErrors);
         Ddo_grid_Gridinternalname = subGrid_Internalname;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GridInternalName", Ddo_grid_Gridinternalname);
         Ddo_grid_Gamoauthtoken = AV36GAMSession.gxTpr_Token;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GAMOAuthToken", Ddo_grid_Gamoauthtoken);
         Form.Caption = context.GetMessage( " New Blog", "");
         AssignProp("", false, "FORM", "Caption", Form.Caption, true);
         /* Execute user subroutine: 'PREPARETRANSACTION' */
         S122 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S132 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( AV13OrderedBy < 1 )
         {
            AV13OrderedBy = 1;
            AssignAttri("", false, "AV13OrderedBy", StringUtil.LTrimStr( (decimal)(AV13OrderedBy), 4, 0));
            /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
            S142 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = AV35DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1) ;
         AV35DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1;
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = bttBtneditcolumns_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, "", false, Ddo_gridcolumnsselector_Internalname, "TitleControlIdToReplace", Ddo_gridcolumnsselector_Titlecontrolidtoreplace);
         Gridpaginationbar_Rowsperpageselectedvalue = subGrid_Rows;
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0));
      }

      protected void E192R2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV6WWPContext) ;
         /* Execute user subroutine: 'CHECKSECURITYFORACTIONS' */
         S152 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( AV26ManageFiltersExecutionStep == 1 )
         {
            AV26ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV26ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV26ManageFiltersExecutionStep), 1, 0));
         }
         else if ( AV26ManageFiltersExecutionStep == 2 )
         {
            AV26ManageFiltersExecutionStep = 0;
            AssignAttri("", false, "AV26ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV26ManageFiltersExecutionStep), 1, 0));
            /* Execute user subroutine: 'LOADSAVEDFILTERS' */
            S112 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         /* Execute user subroutine: 'SAVEGRIDSTATE' */
         S162 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( StringUtil.StrCmp(AV23Session.Get("NewBlogWWColumnsSelector"), "") != 0 )
         {
            AV19ColumnsSelectorXML = AV23Session.Get("NewBlogWWColumnsSelector");
            AV21ColumnsSelector.FromXml(AV19ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S172 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         edtNewBlogId_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(1)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewBlogId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewBlogId_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewBlogImagen_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(2)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewBlogImagen_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewBlogImagen_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewBlogTitulo_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(3)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewBlogTitulo_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewBlogTitulo_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewBlogSubTitulo_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(4)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewBlogSubTitulo_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewBlogSubTitulo_Visible), 5, 0), !bGXsfl_44_Refreshing);
         chkNewBlogDestacado.Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(5)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, chkNewBlogDestacado_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkNewBlogDestacado.Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewBlogVisitas_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(6)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewBlogVisitas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewBlogVisitas_Visible), 5, 0), !bGXsfl_44_Refreshing);
         chkNewBlogBorrador.Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(7)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, chkNewBlogBorrador_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkNewBlogBorrador.Visible), 5, 0), !bGXsfl_44_Refreshing);
         AV39GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri("", false, "AV39GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV39GridCurrentPage), 10, 0));
         AV40GridPageCount = subGrid_fnc_Pagecount( );
         AssignAttri("", false, "AV40GridPageCount", StringUtil.LTrimStr( (decimal)(AV40GridPageCount), 10, 0));
         GXt_char2 = AV41GridAppliedFilters;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getappliedfiltersdescription(context ).execute(  AV55Pgmname, out  GXt_char2) ;
         AV41GridAppliedFilters = GXt_char2;
         AssignAttri("", false, "AV41GridAppliedFilters", AV41GridAppliedFilters);
         AV56Newblogwwds_1_filterfulltext = AV16FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV27TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV28TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV29TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV30TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV31TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV32TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV51TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV52TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV53TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV54TFNewBlogBorrador_Sel;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E122R2( )
      {
         /* Gridpaginationbar_Changepage Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Gridpaginationbar_Selectedpage, "Previous") == 0 )
         {
            subgrid_previouspage( ) ;
         }
         else if ( StringUtil.StrCmp(Gridpaginationbar_Selectedpage, "Next") == 0 )
         {
            subgrid_nextpage( ) ;
         }
         else
         {
            AV38PageToGo = (int)(Math.Round(NumberUtil.Val( Gridpaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            subgrid_gotopage( AV38PageToGo) ;
         }
      }

      protected void E132R2( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      protected void E152R2( )
      {
         /* Ddo_grid_Onoptionclicked Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderASC#>") == 0 ) || ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>") == 0 ) )
         {
            AV13OrderedBy = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Selectedvalue_get, "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV13OrderedBy", StringUtil.LTrimStr( (decimal)(AV13OrderedBy), 4, 0));
            AV14OrderedDsc = ((StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>")==0) ? true : false);
            AssignAttri("", false, "AV14OrderedDsc", AV14OrderedDsc);
            /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
            S142 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
            subgrid_firstpage( ) ;
         }
         else if ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#Filter#>") == 0 )
         {
            if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewBlogId") == 0 )
            {
               AV27TFNewBlogId = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV27TFNewBlogId", StringUtil.LTrimStr( (decimal)(AV27TFNewBlogId), 4, 0));
               AV28TFNewBlogId_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV28TFNewBlogId_To", StringUtil.LTrimStr( (decimal)(AV28TFNewBlogId_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewBlogTitulo") == 0 )
            {
               AV29TFNewBlogTitulo = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV29TFNewBlogTitulo", AV29TFNewBlogTitulo);
               AV30TFNewBlogTitulo_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV30TFNewBlogTitulo_Sel", AV30TFNewBlogTitulo_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewBlogSubTitulo") == 0 )
            {
               AV31TFNewBlogSubTitulo = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV31TFNewBlogSubTitulo", AV31TFNewBlogSubTitulo);
               AV32TFNewBlogSubTitulo_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV32TFNewBlogSubTitulo_Sel", AV32TFNewBlogSubTitulo_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewBlogDestacado") == 0 )
            {
               AV51TFNewBlogDestacado_Sel = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Selectedvalue_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV51TFNewBlogDestacado_Sel", StringUtil.Str( (decimal)(AV51TFNewBlogDestacado_Sel), 1, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewBlogVisitas") == 0 )
            {
               AV52TFNewBlogVisitas = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV52TFNewBlogVisitas", StringUtil.LTrimStr( (decimal)(AV52TFNewBlogVisitas), 4, 0));
               AV53TFNewBlogVisitas_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV53TFNewBlogVisitas_To", StringUtil.LTrimStr( (decimal)(AV53TFNewBlogVisitas_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewBlogBorrador") == 0 )
            {
               AV54TFNewBlogBorrador_Sel = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Selectedvalue_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV54TFNewBlogBorrador_Sel", StringUtil.Str( (decimal)(AV54TFNewBlogBorrador_Sel), 1, 0));
            }
            subgrid_firstpage( ) ;
         }
         /*  Sending Event outputs  */
      }

      private void E202R2( )
      {
         /* Grid_Load Routine */
         returnInSub = false;
         cmbavGridactions.removeAllItems();
         cmbavGridactions.addItem("0", ";fa fa-bars", 0);
         if ( AV43IsAuthorized_Display )
         {
            cmbavGridactions.addItem("1", StringUtil.Format( "%1;%2", context.GetMessage( "GXM_display", ""), "fa fa-search", "", "", "", "", "", "", ""), 0);
         }
         if ( AV44IsAuthorized_Update )
         {
            cmbavGridactions.addItem("2", StringUtil.Format( "%1;%2", context.GetMessage( "GXM_update", ""), "fa fa-pen", "", "", "", "", "", "", ""), 0);
         }
         if ( AV45IsAuthorized_Delete )
         {
            cmbavGridactions.addItem("3", StringUtil.Format( "%1;%2", context.GetMessage( "GX_BtnDelete", ""), "fa fa-times", "", "", "", "", "", "", ""), 0);
         }
         if ( cmbavGridactions.ItemCount == 1 )
         {
            cmbavGridactions_Class = "Invisible";
         }
         else
         {
            cmbavGridactions_Class = "ConvertToDDO";
         }
         /* Load Method */
         if ( wbStart != -1 )
         {
            wbStart = 44;
         }
         sendrow_442( ) ;
         GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
         if ( isFullAjaxMode( ) && ! bGXsfl_44_Refreshing )
         {
            DoAjaxLoad(44, GridRow);
         }
         /*  Sending Event outputs  */
         cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0));
      }

      protected void E162R2( )
      {
         /* Ddo_gridcolumnsselector_Oncolumnschanged Routine */
         returnInSub = false;
         AV19ColumnsSelectorXML = Ddo_gridcolumnsselector_Columnsselectorvalues;
         AV21ColumnsSelector.FromJSonString(AV19ColumnsSelectorXML, null);
         new DesignSystem.Programs.wwpbaseobjects.savecolumnsselectorstate(context ).execute(  "NewBlogWWColumnsSelector",  (String.IsNullOrEmpty(StringUtil.RTrim( AV19ColumnsSelectorXML)) ? "" : AV21ColumnsSelector.ToXml(false, true, "", ""))) ;
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E112R2( )
      {
         /* Ddo_managefilters_Onoptionclicked Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Clean#>") == 0 )
         {
            /* Execute user subroutine: 'CLEANFILTERS' */
            S182 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
            subgrid_firstpage( ) ;
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Save#>") == 0 )
         {
            /* Execute user subroutine: 'SAVEGRIDSTATE' */
            S162 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "wwpbaseobjects.savefilteras.aspx"+UrlEncode(StringUtil.RTrim("NewBlogWWFilters")) + "," + UrlEncode(StringUtil.RTrim(AV55Pgmname+"GridState"));
            context.PopUp(formatLink("wwpbaseobjects.savefilteras.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV26ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV26ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV26ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Manage#>") == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "wwpbaseobjects.managefilters.aspx"+UrlEncode(StringUtil.RTrim("NewBlogWWFilters"));
            context.PopUp(formatLink("wwpbaseobjects.managefilters.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV26ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV26ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV26ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else
         {
            GXt_char2 = AV25ManageFiltersXml;
            new DesignSystem.Programs.wwpbaseobjects.getfilterbyname(context ).execute(  "NewBlogWWFilters",  Ddo_managefilters_Activeeventkey, out  GXt_char2) ;
            AV25ManageFiltersXml = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV25ManageFiltersXml)) )
            {
               GX_msglist.addItem(context.GetMessage( "WWP_FilterNotExist", ""));
            }
            else
            {
               /* Execute user subroutine: 'CLEANFILTERS' */
               S182 ();
               if ( returnInSub )
               {
                  returnInSub = true;
                  if (true) return;
               }
               new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV55Pgmname+"GridState",  AV25ManageFiltersXml) ;
               AV11GridState.FromXml(AV25ManageFiltersXml, null, "", "");
               AV13OrderedBy = AV11GridState.gxTpr_Orderedby;
               AssignAttri("", false, "AV13OrderedBy", StringUtil.LTrimStr( (decimal)(AV13OrderedBy), 4, 0));
               AV14OrderedDsc = AV11GridState.gxTpr_Ordereddsc;
               AssignAttri("", false, "AV14OrderedDsc", AV14OrderedDsc);
               /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
               S142 ();
               if ( returnInSub )
               {
                  returnInSub = true;
                  if (true) return;
               }
               /* Execute user subroutine: 'LOADREGFILTERSSTATE' */
               S192 ();
               if ( returnInSub )
               {
                  returnInSub = true;
                  if (true) return;
               }
               subgrid_firstpage( ) ;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
      }

      protected void E212R2( )
      {
         /* Gridactions_Click Routine */
         returnInSub = false;
         if ( AV42GridActions == 1 )
         {
            /* Execute user subroutine: 'DO DISPLAY' */
            S202 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV42GridActions == 2 )
         {
            /* Execute user subroutine: 'DO UPDATE' */
            S212 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV42GridActions == 3 )
         {
            /* Execute user subroutine: 'DO DELETE' */
            S222 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         AV42GridActions = 0;
         AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV42GridActions), 4, 0));
         /*  Sending Event outputs  */
         cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0));
         AssignProp("", false, cmbavGridactions_Internalname, "Values", cmbavGridactions.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E172R2( )
      {
         /* 'DoInsert' Routine */
         returnInSub = false;
         if ( AV48IsAuthorized_Insert )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "newblog.aspx"+UrlEncode(StringUtil.RTrim("INS")) + "," + UrlEncode(StringUtil.LTrimStr(0,1,0));
            CallWebObject(formatLink("newblog.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E142R2( )
      {
         /* Ddo_agexport_Onoptionclicked Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Ddo_agexport_Activeeventkey, "Export") == 0 )
         {
            /* Execute user subroutine: 'DOEXPORT' */
            S232 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(Ddo_agexport_Activeeventkey, "ExportReport") == 0 )
         {
            /* Execute user subroutine: 'DOEXPORTREPORT' */
            S242 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void S142( )
      {
         /* 'SETDDOSORTEDSTATUS' Routine */
         returnInSub = false;
         Ddo_grid_Sortedstatus = StringUtil.Trim( StringUtil.Str( (decimal)(AV13OrderedBy), 4, 0))+":"+(AV14OrderedDsc ? "DSC" : "ASC");
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SortedStatus", Ddo_grid_Sortedstatus);
      }

      protected void S172( )
      {
         /* 'INITIALIZECOLUMNSSELECTOR' Routine */
         returnInSub = false;
         AV21ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewBlogId",  "",  "Id",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewBlogImagen",  "",  "Imagen",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewBlogTitulo",  "",  "Titulo",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewBlogSubTitulo",  "",  "SubTitulo",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewBlogDestacado",  "",  "Destacado",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewBlogVisitas",  "",  "Visitas",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewBlogBorrador",  "",  "Borrador",  true,  "") ;
         GXt_char2 = AV20UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "NewBlogWWColumnsSelector", out  GXt_char2) ;
         AV20UserCustomValue = GXt_char2;
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV20UserCustomValue)) ) )
         {
            AV22ColumnsSelectorAux.FromXml(AV20UserCustomValue, null, "", "");
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnselector_updatecolumns(context ).execute( ref  AV22ColumnsSelectorAux, ref  AV21ColumnsSelector) ;
         }
      }

      protected void S152( )
      {
         /* 'CHECKSECURITYFORACTIONS' Routine */
         returnInSub = false;
         GXt_boolean3 = AV43IsAuthorized_Display;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newblog_Execute", out  GXt_boolean3) ;
         AV43IsAuthorized_Display = GXt_boolean3;
         AssignAttri("", false, "AV43IsAuthorized_Display", AV43IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV43IsAuthorized_Display, context));
         GXt_boolean3 = AV44IsAuthorized_Update;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newblog_Update", out  GXt_boolean3) ;
         AV44IsAuthorized_Update = GXt_boolean3;
         AssignAttri("", false, "AV44IsAuthorized_Update", AV44IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV44IsAuthorized_Update, context));
         GXt_boolean3 = AV45IsAuthorized_Delete;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newblog_Delete", out  GXt_boolean3) ;
         AV45IsAuthorized_Delete = GXt_boolean3;
         AssignAttri("", false, "AV45IsAuthorized_Delete", AV45IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV45IsAuthorized_Delete, context));
         GXt_boolean3 = AV48IsAuthorized_Insert;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newblog_Insert", out  GXt_boolean3) ;
         AV48IsAuthorized_Insert = GXt_boolean3;
         AssignAttri("", false, "AV48IsAuthorized_Insert", AV48IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV48IsAuthorized_Insert, context));
         if ( ! ( AV48IsAuthorized_Insert ) )
         {
            bttBtninsert_Visible = 0;
            AssignProp("", false, bttBtninsert_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtninsert_Visible), 5, 0), true);
         }
      }

      protected void S112( )
      {
         /* 'LOADSAVEDFILTERS' Routine */
         returnInSub = false;
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 = AV24ManageFiltersData;
         new DesignSystem.Programs.wwpbaseobjects.wwp_managefiltersloadsavedfilters(context ).execute(  "NewBlogWWFilters",  "",  "",  false, out  GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4) ;
         AV24ManageFiltersData = GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4;
      }

      protected void S182( )
      {
         /* 'CLEANFILTERS' Routine */
         returnInSub = false;
         AV16FilterFullText = "";
         AssignAttri("", false, "AV16FilterFullText", AV16FilterFullText);
         AV27TFNewBlogId = 0;
         AssignAttri("", false, "AV27TFNewBlogId", StringUtil.LTrimStr( (decimal)(AV27TFNewBlogId), 4, 0));
         AV28TFNewBlogId_To = 0;
         AssignAttri("", false, "AV28TFNewBlogId_To", StringUtil.LTrimStr( (decimal)(AV28TFNewBlogId_To), 4, 0));
         AV29TFNewBlogTitulo = "";
         AssignAttri("", false, "AV29TFNewBlogTitulo", AV29TFNewBlogTitulo);
         AV30TFNewBlogTitulo_Sel = "";
         AssignAttri("", false, "AV30TFNewBlogTitulo_Sel", AV30TFNewBlogTitulo_Sel);
         AV31TFNewBlogSubTitulo = "";
         AssignAttri("", false, "AV31TFNewBlogSubTitulo", AV31TFNewBlogSubTitulo);
         AV32TFNewBlogSubTitulo_Sel = "";
         AssignAttri("", false, "AV32TFNewBlogSubTitulo_Sel", AV32TFNewBlogSubTitulo_Sel);
         AV51TFNewBlogDestacado_Sel = 0;
         AssignAttri("", false, "AV51TFNewBlogDestacado_Sel", StringUtil.Str( (decimal)(AV51TFNewBlogDestacado_Sel), 1, 0));
         AV52TFNewBlogVisitas = 0;
         AssignAttri("", false, "AV52TFNewBlogVisitas", StringUtil.LTrimStr( (decimal)(AV52TFNewBlogVisitas), 4, 0));
         AV53TFNewBlogVisitas_To = 0;
         AssignAttri("", false, "AV53TFNewBlogVisitas_To", StringUtil.LTrimStr( (decimal)(AV53TFNewBlogVisitas_To), 4, 0));
         AV54TFNewBlogBorrador_Sel = 0;
         AssignAttri("", false, "AV54TFNewBlogBorrador_Sel", StringUtil.Str( (decimal)(AV54TFNewBlogBorrador_Sel), 1, 0));
         Ddo_grid_Selectedvalue_set = "";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         Ddo_grid_Filteredtext_set = "";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = "";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S202( )
      {
         /* 'DO DISPLAY' Routine */
         returnInSub = false;
         if ( AV43IsAuthorized_Display )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "newblog.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.LTrimStr(A12NewBlogId,4,0));
            CallWebObject(formatLink("newblog.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
      }

      protected void S212( )
      {
         /* 'DO UPDATE' Routine */
         returnInSub = false;
         if ( AV44IsAuthorized_Update )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "newblog.aspx"+UrlEncode(StringUtil.RTrim("UPD")) + "," + UrlEncode(StringUtil.LTrimStr(A12NewBlogId,4,0));
            CallWebObject(formatLink("newblog.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
      }

      protected void S222( )
      {
         /* 'DO DELETE' Routine */
         returnInSub = false;
         if ( AV45IsAuthorized_Delete )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "newblog.aspx"+UrlEncode(StringUtil.RTrim("DLT")) + "," + UrlEncode(StringUtil.LTrimStr(A12NewBlogId,4,0));
            CallWebObject(formatLink("newblog.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
      }

      protected void S132( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV23Session.Get(AV55Pgmname+"GridState"), "") == 0 )
         {
            AV11GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  AV55Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV11GridState.FromXml(AV23Session.Get(AV55Pgmname+"GridState"), null, "", "");
         }
         AV13OrderedBy = AV11GridState.gxTpr_Orderedby;
         AssignAttri("", false, "AV13OrderedBy", StringUtil.LTrimStr( (decimal)(AV13OrderedBy), 4, 0));
         AV14OrderedDsc = AV11GridState.gxTpr_Ordereddsc;
         AssignAttri("", false, "AV14OrderedDsc", AV14OrderedDsc);
         /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
         S142 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         /* Execute user subroutine: 'LOADREGFILTERSSTATE' */
         S192 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV11GridState.gxTpr_Pagesize))) )
         {
            subGrid_Rows = (int)(Math.Round(NumberUtil.Val( AV11GridState.gxTpr_Pagesize, "."), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         }
         subgrid_gotopage( AV11GridState.gxTpr_Currentpage) ;
      }

      protected void S192( )
      {
         /* 'LOADREGFILTERSSTATE' Routine */
         returnInSub = false;
         AV67GXV1 = 1;
         while ( AV67GXV1 <= AV11GridState.gxTpr_Filtervalues.Count )
         {
            AV12GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV11GridState.gxTpr_Filtervalues.Item(AV67GXV1));
            if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV16FilterFullText = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV16FilterFullText", AV16FilterFullText);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGID") == 0 )
            {
               AV27TFNewBlogId = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV27TFNewBlogId", StringUtil.LTrimStr( (decimal)(AV27TFNewBlogId), 4, 0));
               AV28TFNewBlogId_To = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV28TFNewBlogId_To", StringUtil.LTrimStr( (decimal)(AV28TFNewBlogId_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO") == 0 )
            {
               AV29TFNewBlogTitulo = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV29TFNewBlogTitulo", AV29TFNewBlogTitulo);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO_SEL") == 0 )
            {
               AV30TFNewBlogTitulo_Sel = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV30TFNewBlogTitulo_Sel", AV30TFNewBlogTitulo_Sel);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO") == 0 )
            {
               AV31TFNewBlogSubTitulo = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV31TFNewBlogSubTitulo", AV31TFNewBlogSubTitulo);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO_SEL") == 0 )
            {
               AV32TFNewBlogSubTitulo_Sel = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV32TFNewBlogSubTitulo_Sel", AV32TFNewBlogSubTitulo_Sel);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGDESTACADO_SEL") == 0 )
            {
               AV51TFNewBlogDestacado_Sel = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV51TFNewBlogDestacado_Sel", StringUtil.Str( (decimal)(AV51TFNewBlogDestacado_Sel), 1, 0));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGVISITAS") == 0 )
            {
               AV52TFNewBlogVisitas = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV52TFNewBlogVisitas", StringUtil.LTrimStr( (decimal)(AV52TFNewBlogVisitas), 4, 0));
               AV53TFNewBlogVisitas_To = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV53TFNewBlogVisitas_To", StringUtil.LTrimStr( (decimal)(AV53TFNewBlogVisitas_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWBLOGBORRADOR_SEL") == 0 )
            {
               AV54TFNewBlogBorrador_Sel = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV54TFNewBlogBorrador_Sel", StringUtil.Str( (decimal)(AV54TFNewBlogBorrador_Sel), 1, 0));
            }
            AV67GXV1 = (int)(AV67GXV1+1);
         }
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV30TFNewBlogTitulo_Sel)),  AV30TFNewBlogTitulo_Sel, out  GXt_char2) ;
         GXt_char5 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV32TFNewBlogSubTitulo_Sel)),  AV32TFNewBlogSubTitulo_Sel, out  GXt_char5) ;
         Ddo_grid_Selectedvalue_set = "||"+GXt_char2+"|"+GXt_char5+"|"+((0==AV51TFNewBlogDestacado_Sel) ? "" : StringUtil.Str( (decimal)(AV51TFNewBlogDestacado_Sel), 1, 0))+"||"+((0==AV54TFNewBlogBorrador_Sel) ? "" : StringUtil.Str( (decimal)(AV54TFNewBlogBorrador_Sel), 1, 0));
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         GXt_char5 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV29TFNewBlogTitulo)),  AV29TFNewBlogTitulo, out  GXt_char5) ;
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV31TFNewBlogSubTitulo)),  AV31TFNewBlogSubTitulo, out  GXt_char2) ;
         Ddo_grid_Filteredtext_set = ((0==AV27TFNewBlogId) ? "" : StringUtil.Str( (decimal)(AV27TFNewBlogId), 4, 0))+"||"+GXt_char5+"|"+GXt_char2+"||"+((0==AV52TFNewBlogVisitas) ? "" : StringUtil.Str( (decimal)(AV52TFNewBlogVisitas), 4, 0))+"|";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = ((0==AV28TFNewBlogId_To) ? "" : StringUtil.Str( (decimal)(AV28TFNewBlogId_To), 4, 0))+"|||||"+((0==AV53TFNewBlogVisitas_To) ? "" : StringUtil.Str( (decimal)(AV53TFNewBlogVisitas_To), 4, 0))+"|";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S162( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV11GridState.FromXml(AV23Session.Get(AV55Pgmname+"GridState"), null, "", "");
         AV11GridState.gxTpr_Orderedby = AV13OrderedBy;
         AV11GridState.gxTpr_Ordereddsc = AV14OrderedDsc;
         AV11GridState.gxTpr_Filtervalues.Clear();
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "FILTERFULLTEXT",  context.GetMessage( "WWP_FullTextFilterDescription", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV16FilterFullText)),  0,  AV16FilterFullText,  AV16FilterFullText,  false,  "",  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWBLOGID",  context.GetMessage( "Id", ""),  !((0==AV27TFNewBlogId)&&(0==AV28TFNewBlogId_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV27TFNewBlogId), 4, 0)),  ((0==AV27TFNewBlogId) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV27TFNewBlogId), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV28TFNewBlogId_To), 4, 0)),  ((0==AV28TFNewBlogId_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV28TFNewBlogId_To), "ZZZ9")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV11GridState,  "TFNEWBLOGTITULO",  context.GetMessage( "Titulo", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV29TFNewBlogTitulo)),  0,  AV29TFNewBlogTitulo,  AV29TFNewBlogTitulo,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV30TFNewBlogTitulo_Sel)),  AV30TFNewBlogTitulo_Sel,  AV30TFNewBlogTitulo_Sel) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV11GridState,  "TFNEWBLOGSUBTITULO",  context.GetMessage( "SubTitulo", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV31TFNewBlogSubTitulo)),  0,  AV31TFNewBlogSubTitulo,  AV31TFNewBlogSubTitulo,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV32TFNewBlogSubTitulo_Sel)),  AV32TFNewBlogSubTitulo_Sel,  AV32TFNewBlogSubTitulo_Sel) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWBLOGDESTACADO_SEL",  context.GetMessage( "Destacado", ""),  !(0==AV51TFNewBlogDestacado_Sel),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV51TFNewBlogDestacado_Sel), 1, 0)),  ((AV51TFNewBlogDestacado_Sel==1) ? context.GetMessage( "WWP_TSChecked", "") : context.GetMessage( "WWP_TSUnChecked", "")),  false,  "",  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWBLOGVISITAS",  context.GetMessage( "Visitas", ""),  !((0==AV52TFNewBlogVisitas)&&(0==AV53TFNewBlogVisitas_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV52TFNewBlogVisitas), 4, 0)),  ((0==AV52TFNewBlogVisitas) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV52TFNewBlogVisitas), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV53TFNewBlogVisitas_To), 4, 0)),  ((0==AV53TFNewBlogVisitas_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV53TFNewBlogVisitas_To), "ZZZ9")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWBLOGBORRADOR_SEL",  context.GetMessage( "Borrador", ""),  !(0==AV54TFNewBlogBorrador_Sel),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV54TFNewBlogBorrador_Sel), 1, 0)),  ((AV54TFNewBlogBorrador_Sel==1) ? context.GetMessage( "WWP_TSChecked", "") : context.GetMessage( "WWP_TSUnChecked", "")),  false,  "",  "") ;
         AV11GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         AV11GridState.gxTpr_Currentpage = (short)(subGrid_fnc_Currentpage( ));
         new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV55Pgmname+"GridState",  AV11GridState.ToXml(false, true, "", "")) ;
      }

      protected void S122( )
      {
         /* 'PREPARETRANSACTION' Routine */
         returnInSub = false;
         AV9TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV9TrnContext.gxTpr_Callerobject = AV55Pgmname;
         AV9TrnContext.gxTpr_Callerondelete = true;
         AV9TrnContext.gxTpr_Callerurl = AV8HTTPRequest.ScriptName+"?"+AV8HTTPRequest.QueryString;
         AV9TrnContext.gxTpr_Transactionname = "NewBlog";
         AV23Session.Set("TrnContext", AV9TrnContext.ToXml(false, true, "", ""));
      }

      protected void S232( )
      {
         /* 'DOEXPORT' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S132 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         new newblogwwexport(context ).execute( out  AV17ExcelFilename, out  AV18ErrorMessage) ;
         if ( StringUtil.StrCmp(AV17ExcelFilename, "") != 0 )
         {
            CallWebObject(formatLink(AV17ExcelFilename) );
            context.wjLocDisableFrm = 0;
         }
         else
         {
            GX_msglist.addItem(AV18ErrorMessage);
         }
      }

      protected void S242( )
      {
         /* 'DOEXPORTREPORT' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S132 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         Innewwindow1_Target = formatLink("newblogwwexportreport.aspx") ;
         ucInnewwindow1.SendProperty(context, "", false, Innewwindow1_Internalname, "Target", Innewwindow1_Target);
         Innewwindow1_Height = "600";
         ucInnewwindow1.SendProperty(context, "", false, Innewwindow1_Internalname, "Height", Innewwindow1_Height);
         Innewwindow1_Width = "800";
         ucInnewwindow1.SendProperty(context, "", false, Innewwindow1_Internalname, "Width", Innewwindow1_Width);
         this.executeUsercontrolMethod("", false, "INNEWWINDOW1Container", "OpenWindow", "", new Object[] {});
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA2R2( ) ;
         WS2R2( ) ;
         WE2R2( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("DVelop/DVPaginationBar/DVPaginationBar.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217061199", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages."+StringUtil.Lower( context.GetLanguageProperty( "code"))+".js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("newblogww.js", "?2024121706123", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("Window/InNewWindowRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_442( )
      {
         cmbavGridactions_Internalname = "vGRIDACTIONS_"+sGXsfl_44_idx;
         edtNewBlogId_Internalname = "NEWBLOGID_"+sGXsfl_44_idx;
         edtNewBlogImagen_Internalname = "NEWBLOGIMAGEN_"+sGXsfl_44_idx;
         edtNewBlogTitulo_Internalname = "NEWBLOGTITULO_"+sGXsfl_44_idx;
         edtNewBlogSubTitulo_Internalname = "NEWBLOGSUBTITULO_"+sGXsfl_44_idx;
         chkNewBlogDestacado_Internalname = "NEWBLOGDESTACADO_"+sGXsfl_44_idx;
         edtNewBlogVisitas_Internalname = "NEWBLOGVISITAS_"+sGXsfl_44_idx;
         chkNewBlogBorrador_Internalname = "NEWBLOGBORRADOR_"+sGXsfl_44_idx;
      }

      protected void SubsflControlProps_fel_442( )
      {
         cmbavGridactions_Internalname = "vGRIDACTIONS_"+sGXsfl_44_fel_idx;
         edtNewBlogId_Internalname = "NEWBLOGID_"+sGXsfl_44_fel_idx;
         edtNewBlogImagen_Internalname = "NEWBLOGIMAGEN_"+sGXsfl_44_fel_idx;
         edtNewBlogTitulo_Internalname = "NEWBLOGTITULO_"+sGXsfl_44_fel_idx;
         edtNewBlogSubTitulo_Internalname = "NEWBLOGSUBTITULO_"+sGXsfl_44_fel_idx;
         chkNewBlogDestacado_Internalname = "NEWBLOGDESTACADO_"+sGXsfl_44_fel_idx;
         edtNewBlogVisitas_Internalname = "NEWBLOGVISITAS_"+sGXsfl_44_fel_idx;
         chkNewBlogBorrador_Internalname = "NEWBLOGBORRADOR_"+sGXsfl_44_fel_idx;
      }

      protected void sendrow_442( )
      {
         sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
         SubsflControlProps_442( ) ;
         WB2R0( ) ;
         if ( ( subGrid_Rows * 1 == 0 ) || ( nGXsfl_44_idx <= subGrid_fnc_Recordsperpage( ) * 1 ) )
         {
            GridRow = GXWebRow.GetNew(context,GridContainer);
            if ( subGrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Odd";
               }
            }
            else if ( subGrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGrid_Backstyle = 0;
               subGrid_Backcolor = subGrid_Allbackcolor;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Uniform";
               }
            }
            else if ( subGrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Odd";
               }
               subGrid_Backcolor = (int)(0x0);
            }
            else if ( subGrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGrid_Backstyle = 1;
               if ( ((int)((nGXsfl_44_idx) % (2))) == 0 )
               {
                  subGrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Even";
                  }
               }
               else
               {
                  subGrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Odd";
                  }
               }
            }
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"GridWithPaginationBar GridNoBorder WorkWith"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_44_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 45,'',false,'" + sGXsfl_44_idx + "',44)\"";
            if ( ( cmbavGridactions.ItemCount == 0 ) && isAjaxCallMode( ) )
            {
               GXCCtl = "vGRIDACTIONS_" + sGXsfl_44_idx;
               cmbavGridactions.Name = GXCCtl;
               cmbavGridactions.WebTags = "";
               if ( cmbavGridactions.ItemCount > 0 )
               {
                  AV42GridActions = (short)(Math.Round(NumberUtil.Val( cmbavGridactions.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0))), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV42GridActions), 4, 0));
               }
            }
            /* ComboBox */
            GridRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavGridactions,(string)cmbavGridactions_Internalname,StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0)),(short)1,(string)cmbavGridactions_Jsonclick,(short)5,"'"+""+"'"+",false,"+"'"+"EVGRIDACTIONS.CLICK."+sGXsfl_44_idx+"'",(string)"int",(string)"",(short)-1,(short)1,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)cmbavGridactions_Class,(string)"WWActionGroupColumn",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,45);\"",(string)"",(bool)true,(short)0});
            cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0));
            AssignProp("", false, cmbavGridactions_Internalname, "Values", (string)(cmbavGridactions.ToJavascriptSource()), !bGXsfl_44_Refreshing);
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtNewBlogId_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewBlogId_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A12NewBlogId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewBlogId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewBlogId_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Id",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtNewBlogImagen_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Static Bitmap Variable */
            ClassString = "Attribute";
            StyleString = "";
            A13NewBlogImagen_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen))&&String.IsNullOrEmpty(StringUtil.RTrim( A40000NewBlogImagen_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)));
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.PathToRelativeUrl( A13NewBlogImagen));
            GridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtNewBlogImagen_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtNewBlogImagen_Visible,(short)0,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)0,(string)"",(string)"",(string)StyleString,(string)ClassString,(string)"WWColumn",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(bool)A13NewBlogImagen_IsBlob,(bool)true,context.GetImageSrcSet( sImgUrl)});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtNewBlogTitulo_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewBlogTitulo_Internalname,(string)A14NewBlogTitulo,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewBlogTitulo_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewBlogTitulo_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusUnanimo\\Description",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtNewBlogSubTitulo_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewBlogSubTitulo_Internalname,(string)A15NewBlogSubTitulo,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewBlogSubTitulo_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewBlogSubTitulo_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusUnanimo\\Description",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((chkNewBlogDestacado.Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Check box */
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GXCCtl = "NEWBLOGDESTACADO_" + sGXsfl_44_idx;
            chkNewBlogDestacado.Name = GXCCtl;
            chkNewBlogDestacado.WebTags = "";
            chkNewBlogDestacado.Caption = "";
            AssignProp("", false, chkNewBlogDestacado_Internalname, "TitleCaption", chkNewBlogDestacado.Caption, !bGXsfl_44_Refreshing);
            chkNewBlogDestacado.CheckedValue = "false";
            A19NewBlogDestacado = StringUtil.StrToBool( StringUtil.BoolToStr( A19NewBlogDestacado));
            GridRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkNewBlogDestacado_Internalname,StringUtil.BoolToStr( A19NewBlogDestacado),(string)"",(string)"",chkNewBlogDestacado.Visible,(short)0,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"WWColumn",(string)"",(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtNewBlogVisitas_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewBlogVisitas_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A18NewBlogVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A18NewBlogVisitas), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewBlogVisitas_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewBlogVisitas_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Contador",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((chkNewBlogBorrador.Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Check box */
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GXCCtl = "NEWBLOGBORRADOR_" + sGXsfl_44_idx;
            chkNewBlogBorrador.Name = GXCCtl;
            chkNewBlogBorrador.WebTags = "";
            chkNewBlogBorrador.Caption = "";
            AssignProp("", false, chkNewBlogBorrador_Internalname, "TitleCaption", chkNewBlogBorrador.Caption, !bGXsfl_44_Refreshing);
            chkNewBlogBorrador.CheckedValue = "false";
            A25NewBlogBorrador = StringUtil.StrToBool( StringUtil.BoolToStr( A25NewBlogBorrador));
            GridRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkNewBlogBorrador_Internalname,StringUtil.BoolToStr( A25NewBlogBorrador),(string)"",(string)"",chkNewBlogBorrador.Visible,(short)0,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"WWColumn",(string)"",(string)""});
            send_integrity_lvl_hashes2R2( ) ;
            GridContainer.AddRow(GridRow);
            nGXsfl_44_idx = ((subGrid_Islastpage==1)&&(nGXsfl_44_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_44_idx+1);
            sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
            SubsflControlProps_442( ) ;
         }
         /* End function sendrow_442 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "vGRIDACTIONS_" + sGXsfl_44_idx;
         cmbavGridactions.Name = GXCCtl;
         cmbavGridactions.WebTags = "";
         if ( cmbavGridactions.ItemCount > 0 )
         {
            AV42GridActions = (short)(Math.Round(NumberUtil.Val( cmbavGridactions.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV42GridActions), 4, 0));
         }
         GXCCtl = "NEWBLOGDESTACADO_" + sGXsfl_44_idx;
         chkNewBlogDestacado.Name = GXCCtl;
         chkNewBlogDestacado.WebTags = "";
         chkNewBlogDestacado.Caption = "";
         AssignProp("", false, chkNewBlogDestacado_Internalname, "TitleCaption", chkNewBlogDestacado.Caption, !bGXsfl_44_Refreshing);
         chkNewBlogDestacado.CheckedValue = "false";
         A19NewBlogDestacado = StringUtil.StrToBool( StringUtil.BoolToStr( A19NewBlogDestacado));
         GXCCtl = "NEWBLOGBORRADOR_" + sGXsfl_44_idx;
         chkNewBlogBorrador.Name = GXCCtl;
         chkNewBlogBorrador.WebTags = "";
         chkNewBlogBorrador.Caption = "";
         AssignProp("", false, chkNewBlogBorrador_Internalname, "TitleCaption", chkNewBlogBorrador.Caption, !bGXsfl_44_Refreshing);
         chkNewBlogBorrador.CheckedValue = "false";
         A25NewBlogBorrador = StringUtil.StrToBool( StringUtil.BoolToStr( A25NewBlogBorrador));
         /* End function init_web_controls */
      }

      protected void StartGridControl44( )
      {
         if ( GridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridContainer"+"DivS\" data-gxgridid=\"44\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGrid_Internalname, subGrid_Internalname, "", "GridWithPaginationBar GridNoBorder WorkWith", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGrid_Backcolorstyle == 0 )
            {
               subGrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subGrid_Class) > 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Title";
               }
            }
            else
            {
               subGrid_Titlebackstyle = 1;
               if ( subGrid_Backcolorstyle == 1 )
               {
                  subGrid_Titlebackcolor = subGrid_Allbackcolor;
                  if ( StringUtil.Len( subGrid_Class) > 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGrid_Class) > 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+cmbavGridactions_Class+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewBlogId_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Id", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewBlogImagen_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Imagen", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewBlogTitulo_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Titulo", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewBlogSubTitulo_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "SubTitulo", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"AttributeCheckBox"+"\" "+" style=\""+((chkNewBlogDestacado.Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Destacado", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewBlogVisitas_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Visitas", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"AttributeCheckBox"+"\" "+" style=\""+((chkNewBlogBorrador.Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Borrador", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridContainer.AddObjectProperty("GridName", "Grid");
         }
         else
         {
            if ( isAjaxCallMode( ) )
            {
               GridContainer = new GXWebGrid( context);
            }
            else
            {
               GridContainer.Clear();
            }
            GridContainer.SetWrapped(nGXWrapped);
            GridContainer.AddObjectProperty("GridName", "Grid");
            GridContainer.AddObjectProperty("Header", subGrid_Header);
            GridContainer.AddObjectProperty("Class", "GridWithPaginationBar GridNoBorder WorkWith");
            GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Sortable", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Sortable), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("CmpContext", "");
            GridContainer.AddObjectProperty("InMasterPage", "false");
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(AV42GridActions), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( cmbavGridactions_Class));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A12NewBlogId), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewBlogId_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", context.convertURL( A13NewBlogImagen));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewBlogImagen_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A14NewBlogTitulo));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewBlogTitulo_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A15NewBlogSubTitulo));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewBlogSubTitulo_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.BoolToStr( A19NewBlogDestacado)));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkNewBlogDestacado.Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A18NewBlogVisitas), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewBlogVisitas_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.BoolToStr( A25NewBlogBorrador)));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkNewBlogBorrador.Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Selectedindex), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowselection), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Selectioncolor), 9, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowhovering), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Hoveringcolor), 9, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowcollapsing), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttBtninsert_Internalname = "BTNINSERT";
         bttBtnagexport_Internalname = "BTNAGEXPORT";
         bttBtneditcolumns_Internalname = "BTNEDITCOLUMNS";
         divTableactions_Internalname = "TABLEACTIONS";
         Ddo_managefilters_Internalname = "DDO_MANAGEFILTERS";
         edtavFilterfulltext_Internalname = "vFILTERFULLTEXT";
         divTablefilters_Internalname = "TABLEFILTERS";
         divTablerightheader_Internalname = "TABLERIGHTHEADER";
         divTableheadercontent_Internalname = "TABLEHEADERCONTENT";
         divTableheader_Internalname = "TABLEHEADER";
         cmbavGridactions_Internalname = "vGRIDACTIONS";
         edtNewBlogId_Internalname = "NEWBLOGID";
         edtNewBlogImagen_Internalname = "NEWBLOGIMAGEN";
         edtNewBlogTitulo_Internalname = "NEWBLOGTITULO";
         edtNewBlogSubTitulo_Internalname = "NEWBLOGSUBTITULO";
         chkNewBlogDestacado_Internalname = "NEWBLOGDESTACADO";
         edtNewBlogVisitas_Internalname = "NEWBLOGVISITAS";
         chkNewBlogBorrador_Internalname = "NEWBLOGBORRADOR";
         Gridpaginationbar_Internalname = "GRIDPAGINATIONBAR";
         divGridtablewithpaginationbar_Internalname = "GRIDTABLEWITHPAGINATIONBAR";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         Dvpanel_unnamedtable1_Internalname = "DVPANEL_UNNAMEDTABLE1";
         divTablemain_Internalname = "TABLEMAIN";
         Ddo_agexport_Internalname = "DDO_AGEXPORT";
         Ddo_grid_Internalname = "DDO_GRID";
         Innewwindow1_Internalname = "INNEWWINDOW1";
         Ddo_gridcolumnsselector_Internalname = "DDO_GRIDCOLUMNSSELECTOR";
         Grid_empowerer_Internalname = "GRID_EMPOWERER";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subGrid_Internalname = "GRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGrid_Allowcollapsing = 0;
         subGrid_Allowselection = 0;
         subGrid_Header = "";
         chkNewBlogBorrador.Caption = "";
         edtNewBlogVisitas_Jsonclick = "";
         chkNewBlogDestacado.Caption = "";
         edtNewBlogSubTitulo_Jsonclick = "";
         edtNewBlogTitulo_Jsonclick = "";
         edtNewBlogId_Jsonclick = "";
         cmbavGridactions_Jsonclick = "";
         cmbavGridactions_Class = "ConvertToDDO";
         subGrid_Class = "GridWithPaginationBar GridNoBorder WorkWith";
         subGrid_Backcolorstyle = 0;
         chkNewBlogBorrador.Visible = -1;
         edtNewBlogVisitas_Visible = -1;
         chkNewBlogDestacado.Visible = -1;
         edtNewBlogSubTitulo_Visible = -1;
         edtNewBlogTitulo_Visible = -1;
         edtNewBlogImagen_Visible = -1;
         edtNewBlogId_Visible = -1;
         chkNewBlogBorrador.Enabled = 0;
         edtNewBlogVisitas_Enabled = 0;
         chkNewBlogDestacado.Enabled = 0;
         edtNewBlogSubTitulo_Enabled = 0;
         edtNewBlogTitulo_Enabled = 0;
         edtNewBlogImagen_Enabled = 0;
         edtNewBlogId_Enabled = 0;
         subGrid_Sortable = 0;
         edtavFilterfulltext_Jsonclick = "";
         edtavFilterfulltext_Enabled = 1;
         bttBtninsert_Visible = 1;
         Grid_empowerer_Hascolumnsselector = Convert.ToBoolean( -1);
         Grid_empowerer_Hastitlesettings = Convert.ToBoolean( -1);
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = "";
         Ddo_gridcolumnsselector_Dropdownoptionstype = "GridColumnsSelector";
         Ddo_gridcolumnsselector_Cls = "ColumnsSelector hidden-xs";
         Ddo_gridcolumnsselector_Tooltip = "WWP_EditColumnsTooltip";
         Ddo_gridcolumnsselector_Caption = context.GetMessage( "WWP_EditColumnsCaption", "");
         Ddo_gridcolumnsselector_Icon = "fas fa-cog";
         Ddo_gridcolumnsselector_Icontype = "FontIcon";
         Innewwindow1_Target = "";
         Innewwindow1_Height = "50";
         Innewwindow1_Width = "50";
         Ddo_grid_Format = "4.0|||||4.0|";
         Ddo_grid_Datalistproc = "NewBlogWWGetFilterData";
         Ddo_grid_Datalistfixedvalues = "||||1:WWP_TSChecked,2:WWP_TSUnChecked||1:WWP_TSChecked,2:WWP_TSUnChecked";
         Ddo_grid_Datalisttype = "||Dynamic|Dynamic|FixedValues||FixedValues";
         Ddo_grid_Includedatalist = "||T|T|T||T";
         Ddo_grid_Filterisrange = "T|||||T|";
         Ddo_grid_Filtertype = "Numeric||Character|Character||Numeric|";
         Ddo_grid_Includefilter = "T||T|T||T|";
         Ddo_grid_Fixable = "T";
         Ddo_grid_Includesortasc = "T||T|T|T|T|T";
         Ddo_grid_Columnssortvalues = "2||1|3|4|5|6";
         Ddo_grid_Columnids = "1:NewBlogId|2:NewBlogImagen|3:NewBlogTitulo|4:NewBlogSubTitulo|5:NewBlogDestacado|6:NewBlogVisitas|7:NewBlogBorrador";
         Ddo_grid_Gridinternalname = "";
         Ddo_agexport_Titlecontrolidtoreplace = "";
         Ddo_agexport_Cls = "ColumnsSelector";
         Ddo_agexport_Icon = "fas fa-download";
         Ddo_agexport_Icontype = "FontIcon";
         Dvpanel_unnamedtable1_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Iconposition = "Right";
         Dvpanel_unnamedtable1_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Title = context.GetMessage( "Listado de Entradas en Blog", "");
         Dvpanel_unnamedtable1_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable1_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Width = "100%";
         Gridpaginationbar_Rowsperpagecaption = "WWP_PagingRowsPerPage";
         Gridpaginationbar_Emptygridcaption = "WWP_PagingEmptyGridCaption";
         Gridpaginationbar_Caption = context.GetMessage( "WWP_PagingCaption", "");
         Gridpaginationbar_Next = "WWP_PagingNextCaption";
         Gridpaginationbar_Previous = "WWP_PagingPreviousCaption";
         Gridpaginationbar_Rowsperpageoptions = "5:WWP_Rows5,10:WWP_Rows10,20:WWP_Rows20,50:WWP_Rows50";
         Gridpaginationbar_Rowsperpageselectedvalue = 10;
         Gridpaginationbar_Rowsperpageselector = Convert.ToBoolean( -1);
         Gridpaginationbar_Emptygridclass = "PaginationBarEmptyGrid";
         Gridpaginationbar_Pagingcaptionposition = "Left";
         Gridpaginationbar_Pagingbuttonsposition = "Right";
         Gridpaginationbar_Pagestoshow = 5;
         Gridpaginationbar_Showlast = Convert.ToBoolean( 0);
         Gridpaginationbar_Shownext = Convert.ToBoolean( -1);
         Gridpaginationbar_Showprevious = Convert.ToBoolean( -1);
         Gridpaginationbar_Showfirst = Convert.ToBoolean( 0);
         Gridpaginationbar_Class = "PaginationBar";
         Ddo_managefilters_Cls = "ManageFilters";
         Ddo_managefilters_Tooltip = "WWP_ManageFiltersTooltip";
         Ddo_managefilters_Icon = "fas fa-filter";
         Ddo_managefilters_Icontype = "FontIcon";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( " New Blog", "");
         subGrid_Rows = 0;
         context.GX_msglist.DisplayMode = 1;
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override string AjaxOnSessionTimeout( )
      {
         return "Warn" ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewBlogId_Visible","ctrl":"NEWBLOGID","prop":"Visible"},{"av":"edtNewBlogImagen_Visible","ctrl":"NEWBLOGIMAGEN","prop":"Visible"},{"av":"edtNewBlogTitulo_Visible","ctrl":"NEWBLOGTITULO","prop":"Visible"},{"av":"edtNewBlogSubTitulo_Visible","ctrl":"NEWBLOGSUBTITULO","prop":"Visible"},{"av":"chkNewBlogDestacado.Visible","ctrl":"NEWBLOGDESTACADO","prop":"Visible"},{"av":"edtNewBlogVisitas_Visible","ctrl":"NEWBLOGVISITAS","prop":"Visible"},{"av":"chkNewBlogBorrador.Visible","ctrl":"NEWBLOGBORRADOR","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E122R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E132R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED","""{"handler":"E152R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_grid_Activeeventkey","ctrl":"DDO_GRID","prop":"ActiveEventKey"},{"av":"Ddo_grid_Selectedvalue_get","ctrl":"DDO_GRID","prop":"SelectedValue_get"},{"av":"Ddo_grid_Filteredtextto_get","ctrl":"DDO_GRID","prop":"FilteredTextTo_get"},{"av":"Ddo_grid_Filteredtext_get","ctrl":"DDO_GRID","prop":"FilteredText_get"},{"av":"Ddo_grid_Selectedcolumn","ctrl":"DDO_GRID","prop":"SelectedColumn"}]""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED",""","oparms":[{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E202R2","iparms":[{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"}]}""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED","""{"handler":"E162R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_gridcolumnsselector_Columnsselectorvalues","ctrl":"DDO_GRIDCOLUMNSSELECTOR","prop":"ColumnsSelectorValues"}]""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED",""","oparms":[{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"edtNewBlogId_Visible","ctrl":"NEWBLOGID","prop":"Visible"},{"av":"edtNewBlogImagen_Visible","ctrl":"NEWBLOGIMAGEN","prop":"Visible"},{"av":"edtNewBlogTitulo_Visible","ctrl":"NEWBLOGTITULO","prop":"Visible"},{"av":"edtNewBlogSubTitulo_Visible","ctrl":"NEWBLOGSUBTITULO","prop":"Visible"},{"av":"chkNewBlogDestacado.Visible","ctrl":"NEWBLOGDESTACADO","prop":"Visible"},{"av":"edtNewBlogVisitas_Visible","ctrl":"NEWBLOGVISITAS","prop":"Visible"},{"av":"chkNewBlogBorrador.Visible","ctrl":"NEWBLOGBORRADOR","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED","""{"handler":"E112R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_managefilters_Activeeventkey","ctrl":"DDO_MANAGEFILTERS","prop":"ActiveEventKey"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewBlogId_Visible","ctrl":"NEWBLOGID","prop":"Visible"},{"av":"edtNewBlogImagen_Visible","ctrl":"NEWBLOGIMAGEN","prop":"Visible"},{"av":"edtNewBlogTitulo_Visible","ctrl":"NEWBLOGTITULO","prop":"Visible"},{"av":"edtNewBlogSubTitulo_Visible","ctrl":"NEWBLOGSUBTITULO","prop":"Visible"},{"av":"chkNewBlogDestacado.Visible","ctrl":"NEWBLOGDESTACADO","prop":"Visible"},{"av":"edtNewBlogVisitas_Visible","ctrl":"NEWBLOGVISITAS","prop":"Visible"},{"av":"chkNewBlogBorrador.Visible","ctrl":"NEWBLOGBORRADOR","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"}]}""");
         setEventMetadata("VGRIDACTIONS.CLICK","""{"handler":"E212R2","iparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A12NewBlogId","fld":"NEWBLOGID","pic":"ZZZ9","hsh":true}]""");
         setEventMetadata("VGRIDACTIONS.CLICK",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewBlogId_Visible","ctrl":"NEWBLOGID","prop":"Visible"},{"av":"edtNewBlogImagen_Visible","ctrl":"NEWBLOGIMAGEN","prop":"Visible"},{"av":"edtNewBlogTitulo_Visible","ctrl":"NEWBLOGTITULO","prop":"Visible"},{"av":"edtNewBlogSubTitulo_Visible","ctrl":"NEWBLOGSUBTITULO","prop":"Visible"},{"av":"chkNewBlogDestacado.Visible","ctrl":"NEWBLOGDESTACADO","prop":"Visible"},{"av":"edtNewBlogVisitas_Visible","ctrl":"NEWBLOGVISITAS","prop":"Visible"},{"av":"chkNewBlogBorrador.Visible","ctrl":"NEWBLOGBORRADOR","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("'DOINSERT'","""{"handler":"E172R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A12NewBlogId","fld":"NEWBLOGID","pic":"ZZZ9","hsh":true}]""");
         setEventMetadata("'DOINSERT'",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewBlogId_Visible","ctrl":"NEWBLOGID","prop":"Visible"},{"av":"edtNewBlogImagen_Visible","ctrl":"NEWBLOGIMAGEN","prop":"Visible"},{"av":"edtNewBlogTitulo_Visible","ctrl":"NEWBLOGTITULO","prop":"Visible"},{"av":"edtNewBlogSubTitulo_Visible","ctrl":"NEWBLOGSUBTITULO","prop":"Visible"},{"av":"chkNewBlogDestacado.Visible","ctrl":"NEWBLOGDESTACADO","prop":"Visible"},{"av":"edtNewBlogVisitas_Visible","ctrl":"NEWBLOGVISITAS","prop":"Visible"},{"av":"chkNewBlogBorrador.Visible","ctrl":"NEWBLOGBORRADOR","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_AGEXPORT.ONOPTIONCLICKED","""{"handler":"E142R2","iparms":[{"av":"Ddo_agexport_Activeeventkey","ctrl":"DDO_AGEXPORT","prop":"ActiveEventKey"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"}]""");
         setEventMetadata("DDO_AGEXPORT.ONOPTIONCLICKED",""","oparms":[{"av":"Innewwindow1_Target","ctrl":"INNEWWINDOW1","prop":"Target"},{"av":"Innewwindow1_Height","ctrl":"INNEWWINDOW1","prop":"Height"},{"av":"Innewwindow1_Width","ctrl":"INNEWWINDOW1","prop":"Width"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV27TFNewBlogId","fld":"vTFNEWBLOGID","pic":"ZZZ9"},{"av":"AV28TFNewBlogId_To","fld":"vTFNEWBLOGID_TO","pic":"ZZZ9"},{"av":"AV29TFNewBlogTitulo","fld":"vTFNEWBLOGTITULO"},{"av":"AV30TFNewBlogTitulo_Sel","fld":"vTFNEWBLOGTITULO_SEL"},{"av":"AV31TFNewBlogSubTitulo","fld":"vTFNEWBLOGSUBTITULO"},{"av":"AV32TFNewBlogSubTitulo_Sel","fld":"vTFNEWBLOGSUBTITULO_SEL"},{"av":"AV51TFNewBlogDestacado_Sel","fld":"vTFNEWBLOGDESTACADO_SEL","pic":"9"},{"av":"AV52TFNewBlogVisitas","fld":"vTFNEWBLOGVISITAS","pic":"ZZZ9"},{"av":"AV53TFNewBlogVisitas_To","fld":"vTFNEWBLOGVISITAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewBlogBorrador_Sel","fld":"vTFNEWBLOGBORRADOR_SEL","pic":"9"},{"av":"AV43IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV44IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV45IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV48IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"}]}""");
         setEventMetadata("NULL","""{"handler":"Valid_Newblogborrador","iparms":[]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         Gridpaginationbar_Selectedpage = "";
         Ddo_grid_Activeeventkey = "";
         Ddo_grid_Selectedvalue_get = "";
         Ddo_grid_Filteredtextto_get = "";
         Ddo_grid_Filteredtext_get = "";
         Ddo_grid_Selectedcolumn = "";
         Ddo_gridcolumnsselector_Columnsselectorvalues = "";
         Ddo_managefilters_Activeeventkey = "";
         Ddo_agexport_Activeeventkey = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV16FilterFullText = "";
         AV21ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV55Pgmname = "";
         AV29TFNewBlogTitulo = "";
         AV30TFNewBlogTitulo_Sel = "";
         AV31TFNewBlogSubTitulo = "";
         AV32TFNewBlogSubTitulo_Sel = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV24ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV41GridAppliedFilters = "";
         AV46AGExportData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV35DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV11GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         Ddo_agexport_Caption = "";
         Ddo_grid_Caption = "";
         Ddo_grid_Filteredtext_set = "";
         Ddo_grid_Filteredtextto_set = "";
         Ddo_grid_Selectedvalue_set = "";
         Ddo_grid_Gamoauthtoken = "";
         Ddo_grid_Sortedstatus = "";
         Ddo_gridcolumnsselector_Gridinternalname = "";
         Grid_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ucDvpanel_unnamedtable1 = new GXUserControl();
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttBtninsert_Jsonclick = "";
         bttBtnagexport_Jsonclick = "";
         bttBtneditcolumns_Jsonclick = "";
         ucDdo_managefilters = new GXUserControl();
         Ddo_managefilters_Caption = "";
         GridContainer = new GXWebGrid( context);
         sStyleString = "";
         ucGridpaginationbar = new GXUserControl();
         ucDdo_agexport = new GXUserControl();
         ucDdo_grid = new GXUserControl();
         ucInnewwindow1 = new GXUserControl();
         ucDdo_gridcolumnsselector = new GXUserControl();
         ucGrid_empowerer = new GXUserControl();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         A13NewBlogImagen = "";
         A40000NewBlogImagen_GXI = "";
         A14NewBlogTitulo = "";
         A15NewBlogSubTitulo = "";
         lV56Newblogwwds_1_filterfulltext = "";
         lV59Newblogwwds_4_tfnewblogtitulo = "";
         lV61Newblogwwds_6_tfnewblogsubtitulo = "";
         AV56Newblogwwds_1_filterfulltext = "";
         AV60Newblogwwds_5_tfnewblogtitulo_sel = "";
         AV59Newblogwwds_4_tfnewblogtitulo = "";
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = "";
         AV61Newblogwwds_6_tfnewblogsubtitulo = "";
         H002R2_A25NewBlogBorrador = new bool[] {false} ;
         H002R2_A18NewBlogVisitas = new short[1] ;
         H002R2_A19NewBlogDestacado = new bool[] {false} ;
         H002R2_A15NewBlogSubTitulo = new string[] {""} ;
         H002R2_A14NewBlogTitulo = new string[] {""} ;
         H002R2_A40000NewBlogImagen_GXI = new string[] {""} ;
         H002R2_A12NewBlogId = new short[1] ;
         H002R2_A13NewBlogImagen = new string[] {""} ;
         H002R3_AGRID_nRecordCount = new long[1] ;
         AV8HTTPRequest = new GxHttpRequest( context);
         AV47AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV36GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV37GAMErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV6WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV23Session = context.GetSession();
         AV19ColumnsSelectorXML = "";
         GridRow = new GXWebRow();
         GXEncryptionTmp = "";
         AV25ManageFiltersXml = "";
         AV20UserCustomValue = "";
         AV22ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV12GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         GXt_char5 = "";
         GXt_char2 = "";
         AV9TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV17ExcelFilename = "";
         AV18ErrorMessage = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid_Linesclass = "";
         GXCCtl = "";
         ROClassString = "";
         sImgUrl = "";
         GridColumn = new GXWebColumn();
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newblogww__default(),
            new Object[][] {
                new Object[] {
               H002R2_A25NewBlogBorrador, H002R2_A18NewBlogVisitas, H002R2_A19NewBlogDestacado, H002R2_A15NewBlogSubTitulo, H002R2_A14NewBlogTitulo, H002R2_A40000NewBlogImagen_GXI, H002R2_A12NewBlogId, H002R2_A13NewBlogImagen
               }
               , new Object[] {
               H002R3_AGRID_nRecordCount
               }
            }
         );
         AV55Pgmname = "NewBlogWW";
         /* GeneXus formulas. */
         AV55Pgmname = "NewBlogWW";
      }

      private short GRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short AV13OrderedBy ;
      private short AV26ManageFiltersExecutionStep ;
      private short AV27TFNewBlogId ;
      private short AV28TFNewBlogId_To ;
      private short AV51TFNewBlogDestacado_Sel ;
      private short AV52TFNewBlogVisitas ;
      private short AV53TFNewBlogVisitas_To ;
      private short AV54TFNewBlogBorrador_Sel ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV42GridActions ;
      private short A12NewBlogId ;
      private short A18NewBlogVisitas ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid_Backcolorstyle ;
      private short subGrid_Sortable ;
      private short AV57Newblogwwds_2_tfnewblogid ;
      private short AV58Newblogwwds_3_tfnewblogid_to ;
      private short AV63Newblogwwds_8_tfnewblogdestacado_sel ;
      private short AV64Newblogwwds_9_tfnewblogvisitas ;
      private short AV65Newblogwwds_10_tfnewblogvisitas_to ;
      private short AV66Newblogwwds_11_tfnewblogborrador_sel ;
      private short nGXWrapped ;
      private short subGrid_Backstyle ;
      private short subGrid_Titlebackstyle ;
      private short subGrid_Allowselection ;
      private short subGrid_Allowhovering ;
      private short subGrid_Allowcollapsing ;
      private short subGrid_Collapsed ;
      private int subGrid_Rows ;
      private int Gridpaginationbar_Rowsperpageselectedvalue ;
      private int nRC_GXsfl_44 ;
      private int nGXsfl_44_idx=1 ;
      private int Gridpaginationbar_Pagestoshow ;
      private int bttBtninsert_Visible ;
      private int edtavFilterfulltext_Enabled ;
      private int subGrid_Islastpage ;
      private int GXPagingFrom2 ;
      private int GXPagingTo2 ;
      private int edtNewBlogId_Enabled ;
      private int edtNewBlogImagen_Enabled ;
      private int edtNewBlogTitulo_Enabled ;
      private int edtNewBlogSubTitulo_Enabled ;
      private int edtNewBlogVisitas_Enabled ;
      private int edtNewBlogId_Visible ;
      private int edtNewBlogImagen_Visible ;
      private int edtNewBlogTitulo_Visible ;
      private int edtNewBlogSubTitulo_Visible ;
      private int edtNewBlogVisitas_Visible ;
      private int AV38PageToGo ;
      private int AV67GXV1 ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long GRID_nFirstRecordOnPage ;
      private long AV39GridCurrentPage ;
      private long AV40GridPageCount ;
      private long GRID_nCurrentRecord ;
      private long GRID_nRecordCount ;
      private string Gridpaginationbar_Selectedpage ;
      private string Ddo_grid_Activeeventkey ;
      private string Ddo_grid_Selectedvalue_get ;
      private string Ddo_grid_Filteredtextto_get ;
      private string Ddo_grid_Filteredtext_get ;
      private string Ddo_grid_Selectedcolumn ;
      private string Ddo_gridcolumnsselector_Columnsselectorvalues ;
      private string Ddo_managefilters_Activeeventkey ;
      private string Ddo_agexport_Activeeventkey ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_44_idx="0001" ;
      private string AV55Pgmname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string Ddo_managefilters_Icontype ;
      private string Ddo_managefilters_Icon ;
      private string Ddo_managefilters_Tooltip ;
      private string Ddo_managefilters_Cls ;
      private string Gridpaginationbar_Class ;
      private string Gridpaginationbar_Pagingbuttonsposition ;
      private string Gridpaginationbar_Pagingcaptionposition ;
      private string Gridpaginationbar_Emptygridclass ;
      private string Gridpaginationbar_Rowsperpageoptions ;
      private string Gridpaginationbar_Previous ;
      private string Gridpaginationbar_Next ;
      private string Gridpaginationbar_Caption ;
      private string Gridpaginationbar_Emptygridcaption ;
      private string Gridpaginationbar_Rowsperpagecaption ;
      private string Dvpanel_unnamedtable1_Width ;
      private string Dvpanel_unnamedtable1_Cls ;
      private string Dvpanel_unnamedtable1_Title ;
      private string Dvpanel_unnamedtable1_Iconposition ;
      private string Ddo_agexport_Icontype ;
      private string Ddo_agexport_Icon ;
      private string Ddo_agexport_Caption ;
      private string Ddo_agexport_Cls ;
      private string Ddo_agexport_Titlecontrolidtoreplace ;
      private string Ddo_grid_Caption ;
      private string Ddo_grid_Filteredtext_set ;
      private string Ddo_grid_Filteredtextto_set ;
      private string Ddo_grid_Selectedvalue_set ;
      private string Ddo_grid_Gamoauthtoken ;
      private string Ddo_grid_Gridinternalname ;
      private string Ddo_grid_Columnids ;
      private string Ddo_grid_Columnssortvalues ;
      private string Ddo_grid_Includesortasc ;
      private string Ddo_grid_Fixable ;
      private string Ddo_grid_Sortedstatus ;
      private string Ddo_grid_Includefilter ;
      private string Ddo_grid_Filtertype ;
      private string Ddo_grid_Filterisrange ;
      private string Ddo_grid_Includedatalist ;
      private string Ddo_grid_Datalisttype ;
      private string Ddo_grid_Datalistfixedvalues ;
      private string Ddo_grid_Datalistproc ;
      private string Ddo_grid_Format ;
      private string Innewwindow1_Width ;
      private string Innewwindow1_Height ;
      private string Innewwindow1_Target ;
      private string Ddo_gridcolumnsselector_Icontype ;
      private string Ddo_gridcolumnsselector_Icon ;
      private string Ddo_gridcolumnsselector_Caption ;
      private string Ddo_gridcolumnsselector_Tooltip ;
      private string Ddo_gridcolumnsselector_Cls ;
      private string Ddo_gridcolumnsselector_Dropdownoptionstype ;
      private string Ddo_gridcolumnsselector_Gridinternalname ;
      private string Ddo_gridcolumnsselector_Titlecontrolidtoreplace ;
      private string Grid_empowerer_Gridinternalname ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divTablemain_Internalname ;
      private string Dvpanel_unnamedtable1_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string divTableheader_Internalname ;
      private string divTableheadercontent_Internalname ;
      private string divTableactions_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtninsert_Internalname ;
      private string bttBtninsert_Jsonclick ;
      private string bttBtnagexport_Internalname ;
      private string bttBtnagexport_Jsonclick ;
      private string bttBtneditcolumns_Internalname ;
      private string bttBtneditcolumns_Jsonclick ;
      private string divTablerightheader_Internalname ;
      private string Ddo_managefilters_Caption ;
      private string Ddo_managefilters_Internalname ;
      private string divTablefilters_Internalname ;
      private string edtavFilterfulltext_Internalname ;
      private string edtavFilterfulltext_Jsonclick ;
      private string divGridtablewithpaginationbar_Internalname ;
      private string sStyleString ;
      private string subGrid_Internalname ;
      private string Gridpaginationbar_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string Ddo_agexport_Internalname ;
      private string Ddo_grid_Internalname ;
      private string Innewwindow1_Internalname ;
      private string Ddo_gridcolumnsselector_Internalname ;
      private string Grid_empowerer_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string cmbavGridactions_Internalname ;
      private string edtNewBlogId_Internalname ;
      private string edtNewBlogImagen_Internalname ;
      private string edtNewBlogTitulo_Internalname ;
      private string edtNewBlogSubTitulo_Internalname ;
      private string chkNewBlogDestacado_Internalname ;
      private string edtNewBlogVisitas_Internalname ;
      private string chkNewBlogBorrador_Internalname ;
      private string cmbavGridactions_Class ;
      private string GXEncryptionTmp ;
      private string GXt_char5 ;
      private string GXt_char2 ;
      private string sGXsfl_44_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string GXCCtl ;
      private string cmbavGridactions_Jsonclick ;
      private string ROClassString ;
      private string edtNewBlogId_Jsonclick ;
      private string sImgUrl ;
      private string edtNewBlogTitulo_Jsonclick ;
      private string edtNewBlogSubTitulo_Jsonclick ;
      private string edtNewBlogVisitas_Jsonclick ;
      private string subGrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV14OrderedDsc ;
      private bool AV43IsAuthorized_Display ;
      private bool AV44IsAuthorized_Update ;
      private bool AV45IsAuthorized_Delete ;
      private bool AV48IsAuthorized_Insert ;
      private bool Gridpaginationbar_Showfirst ;
      private bool Gridpaginationbar_Showprevious ;
      private bool Gridpaginationbar_Shownext ;
      private bool Gridpaginationbar_Showlast ;
      private bool Gridpaginationbar_Rowsperpageselector ;
      private bool Dvpanel_unnamedtable1_Autowidth ;
      private bool Dvpanel_unnamedtable1_Autoheight ;
      private bool Dvpanel_unnamedtable1_Collapsible ;
      private bool Dvpanel_unnamedtable1_Collapsed ;
      private bool Dvpanel_unnamedtable1_Showcollapseicon ;
      private bool Dvpanel_unnamedtable1_Autoscroll ;
      private bool Grid_empowerer_Hastitlesettings ;
      private bool Grid_empowerer_Hascolumnsselector ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_44_Refreshing=false ;
      private bool A19NewBlogDestacado ;
      private bool A25NewBlogBorrador ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool GXt_boolean3 ;
      private bool A13NewBlogImagen_IsBlob ;
      private string AV19ColumnsSelectorXML ;
      private string AV25ManageFiltersXml ;
      private string AV20UserCustomValue ;
      private string AV16FilterFullText ;
      private string AV29TFNewBlogTitulo ;
      private string AV30TFNewBlogTitulo_Sel ;
      private string AV31TFNewBlogSubTitulo ;
      private string AV32TFNewBlogSubTitulo_Sel ;
      private string AV41GridAppliedFilters ;
      private string A40000NewBlogImagen_GXI ;
      private string A14NewBlogTitulo ;
      private string A15NewBlogSubTitulo ;
      private string lV56Newblogwwds_1_filterfulltext ;
      private string lV59Newblogwwds_4_tfnewblogtitulo ;
      private string lV61Newblogwwds_6_tfnewblogsubtitulo ;
      private string AV56Newblogwwds_1_filterfulltext ;
      private string AV60Newblogwwds_5_tfnewblogtitulo_sel ;
      private string AV59Newblogwwds_4_tfnewblogtitulo ;
      private string AV62Newblogwwds_7_tfnewblogsubtitulo_sel ;
      private string AV61Newblogwwds_6_tfnewblogsubtitulo ;
      private string AV17ExcelFilename ;
      private string AV18ErrorMessage ;
      private string A13NewBlogImagen ;
      private IGxSession AV23Session ;
      private GXWebGrid GridContainer ;
      private GXWebRow GridRow ;
      private GXWebColumn GridColumn ;
      private GXUserControl ucDvpanel_unnamedtable1 ;
      private GXUserControl ucDdo_managefilters ;
      private GXUserControl ucGridpaginationbar ;
      private GXUserControl ucDdo_agexport ;
      private GXUserControl ucDdo_grid ;
      private GXUserControl ucInnewwindow1 ;
      private GXUserControl ucDdo_gridcolumnsselector ;
      private GXUserControl ucGrid_empowerer ;
      private GxHttpRequest AV8HTTPRequest ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavGridactions ;
      private GXCheckbox chkNewBlogDestacado ;
      private GXCheckbox chkNewBlogBorrador ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV21ColumnsSelector ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV24ManageFiltersData ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV46AGExportData ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV35DDO_TitleSettingsIcons ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV11GridState ;
      private IDataStoreProvider pr_default ;
      private bool[] H002R2_A25NewBlogBorrador ;
      private short[] H002R2_A18NewBlogVisitas ;
      private bool[] H002R2_A19NewBlogDestacado ;
      private string[] H002R2_A15NewBlogSubTitulo ;
      private string[] H002R2_A14NewBlogTitulo ;
      private string[] H002R2_A40000NewBlogImagen_GXI ;
      private short[] H002R2_A12NewBlogId ;
      private string[] H002R2_A13NewBlogImagen ;
      private long[] H002R3_AGRID_nRecordCount ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item AV47AGExportDataItem ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV36GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV37GAMErrors ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV6WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV22ColumnsSelectorAux ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV12GridStateFilterValue ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV9TrnContext ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

   public class newblogww__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_H002R2( IGxContext context ,
                                             string AV56Newblogwwds_1_filterfulltext ,
                                             short AV57Newblogwwds_2_tfnewblogid ,
                                             short AV58Newblogwwds_3_tfnewblogid_to ,
                                             string AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                             string AV59Newblogwwds_4_tfnewblogtitulo ,
                                             string AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                             string AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                             short AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                             short AV64Newblogwwds_9_tfnewblogvisitas ,
                                             short AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                             short AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                             short A12NewBlogId ,
                                             string A14NewBlogTitulo ,
                                             string A15NewBlogSubTitulo ,
                                             short A18NewBlogVisitas ,
                                             bool A19NewBlogDestacado ,
                                             bool A25NewBlogBorrador ,
                                             short AV13OrderedBy ,
                                             bool AV14OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int6 = new short[14];
         Object[] GXv_Object7 = new Object[2];
         string sSelectString;
         string sFromString;
         string sOrderString;
         sSelectString = " `NewBlogBorrador`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogSubTitulo`, `NewBlogTitulo`, `NewBlogImagen_GXI`, `NewBlogId`, `NewBlogImagen`";
         sFromString = " FROM `NewBlog`";
         sOrderString = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewBlogId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogSubTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewBlogVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int6[0] = 1;
            GXv_int6[1] = 1;
            GXv_int6[2] = 1;
            GXv_int6[3] = 1;
         }
         if ( ! (0==AV57Newblogwwds_2_tfnewblogid) )
         {
            AddWhere(sWhereString, "(`NewBlogId` >= @AV57Newblogwwds_2_tfnewblogid)");
         }
         else
         {
            GXv_int6[4] = 1;
         }
         if ( ! (0==AV58Newblogwwds_3_tfnewblogid_to) )
         {
            AddWhere(sWhereString, "(`NewBlogId` <= @AV58Newblogwwds_3_tfnewblogid_to)");
         }
         else
         {
            GXv_int6[5] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` like @lV59Newblogwwds_4_tfnewblogtitulo)");
         }
         else
         {
            GXv_int6[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ! ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` = @AV60Newblogwwds_5_tfnewblogtitulo_sel)");
         }
         else
         {
            GXv_int6[7] = 1;
         }
         if ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogTitulo`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` like @lV61Newblogwwds_6_tfnewblogsubtitulo)");
         }
         else
         {
            GXv_int6[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ! ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` = @AV62Newblogwwds_7_tfnewblogsubtitulo_sel)");
         }
         else
         {
            GXv_int6[9] = 1;
         }
         if ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogSubTitulo`))=0))");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 1)");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 0)");
         }
         if ( ! (0==AV64Newblogwwds_9_tfnewblogvisitas) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` >= @AV64Newblogwwds_9_tfnewblogvisitas)");
         }
         else
         {
            GXv_int6[10] = 1;
         }
         if ( ! (0==AV65Newblogwwds_10_tfnewblogvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` <= @AV65Newblogwwds_10_tfnewblogvisitas_to)");
         }
         else
         {
            GXv_int6[11] = 1;
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 1)");
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 0)");
         }
         if ( ( AV13OrderedBy == 1 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewBlogTitulo`";
         }
         else if ( ( AV13OrderedBy == 1 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewBlogTitulo` DESC";
         }
         else if ( ( AV13OrderedBy == 2 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewBlogId`";
         }
         else if ( ( AV13OrderedBy == 2 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewBlogId` DESC";
         }
         else if ( ( AV13OrderedBy == 3 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewBlogSubTitulo`";
         }
         else if ( ( AV13OrderedBy == 3 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewBlogSubTitulo` DESC";
         }
         else if ( ( AV13OrderedBy == 4 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewBlogDestacado`";
         }
         else if ( ( AV13OrderedBy == 4 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewBlogDestacado` DESC";
         }
         else if ( ( AV13OrderedBy == 5 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewBlogVisitas`";
         }
         else if ( ( AV13OrderedBy == 5 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewBlogVisitas` DESC";
         }
         else if ( ( AV13OrderedBy == 6 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewBlogBorrador`";
         }
         else if ( ( AV13OrderedBy == 6 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewBlogBorrador` DESC";
         }
         else if ( true )
         {
            sOrderString += " ORDER BY `NewBlogId`";
         }
         scmdbuf = "SELECT " + sSelectString + sFromString + sWhereString + sOrderString + "" + " LIMIT " + "@GXPagingFrom2" + ", " + "@GXPagingTo2";
         GXv_Object7[0] = scmdbuf;
         GXv_Object7[1] = GXv_int6;
         return GXv_Object7 ;
      }

      protected Object[] conditional_H002R3( IGxContext context ,
                                             string AV56Newblogwwds_1_filterfulltext ,
                                             short AV57Newblogwwds_2_tfnewblogid ,
                                             short AV58Newblogwwds_3_tfnewblogid_to ,
                                             string AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                             string AV59Newblogwwds_4_tfnewblogtitulo ,
                                             string AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                             string AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                             short AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                             short AV64Newblogwwds_9_tfnewblogvisitas ,
                                             short AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                             short AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                             short A12NewBlogId ,
                                             string A14NewBlogTitulo ,
                                             string A15NewBlogSubTitulo ,
                                             short A18NewBlogVisitas ,
                                             bool A19NewBlogDestacado ,
                                             bool A25NewBlogBorrador ,
                                             short AV13OrderedBy ,
                                             bool AV14OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int8 = new short[12];
         Object[] GXv_Object9 = new Object[2];
         scmdbuf = "SELECT COUNT(*) FROM `NewBlog`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewBlogId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogSubTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewBlogVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int8[0] = 1;
            GXv_int8[1] = 1;
            GXv_int8[2] = 1;
            GXv_int8[3] = 1;
         }
         if ( ! (0==AV57Newblogwwds_2_tfnewblogid) )
         {
            AddWhere(sWhereString, "(`NewBlogId` >= @AV57Newblogwwds_2_tfnewblogid)");
         }
         else
         {
            GXv_int8[4] = 1;
         }
         if ( ! (0==AV58Newblogwwds_3_tfnewblogid_to) )
         {
            AddWhere(sWhereString, "(`NewBlogId` <= @AV58Newblogwwds_3_tfnewblogid_to)");
         }
         else
         {
            GXv_int8[5] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` like @lV59Newblogwwds_4_tfnewblogtitulo)");
         }
         else
         {
            GXv_int8[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ! ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` = @AV60Newblogwwds_5_tfnewblogtitulo_sel)");
         }
         else
         {
            GXv_int8[7] = 1;
         }
         if ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogTitulo`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` like @lV61Newblogwwds_6_tfnewblogsubtitulo)");
         }
         else
         {
            GXv_int8[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ! ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` = @AV62Newblogwwds_7_tfnewblogsubtitulo_sel)");
         }
         else
         {
            GXv_int8[9] = 1;
         }
         if ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogSubTitulo`))=0))");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 1)");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 0)");
         }
         if ( ! (0==AV64Newblogwwds_9_tfnewblogvisitas) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` >= @AV64Newblogwwds_9_tfnewblogvisitas)");
         }
         else
         {
            GXv_int8[10] = 1;
         }
         if ( ! (0==AV65Newblogwwds_10_tfnewblogvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` <= @AV65Newblogwwds_10_tfnewblogvisitas_to)");
         }
         else
         {
            GXv_int8[11] = 1;
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 1)");
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 0)");
         }
         scmdbuf += sWhereString;
         if ( ( AV13OrderedBy == 1 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 1 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 2 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 2 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 3 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 3 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 4 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 4 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 5 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 5 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 6 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 6 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( true )
         {
            scmdbuf += "";
         }
         GXv_Object9[0] = scmdbuf;
         GXv_Object9[1] = GXv_int8;
         return GXv_Object9 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_H002R2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (bool)dynConstraints[16] , (short)dynConstraints[17] , (bool)dynConstraints[18] );
               case 1 :
                     return conditional_H002R3(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (bool)dynConstraints[16] , (short)dynConstraints[17] , (bool)dynConstraints[18] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmH002R2;
          prmH002R2 = new Object[] {
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV57Newblogwwds_2_tfnewblogid",GXType.Int16,4,0) ,
          new ParDef("@AV58Newblogwwds_3_tfnewblogid_to",GXType.Int16,4,0) ,
          new ParDef("@lV59Newblogwwds_4_tfnewblogtitulo",GXType.Char,200,0) ,
          new ParDef("@AV60Newblogwwds_5_tfnewblogtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@lV61Newblogwwds_6_tfnewblogsubtitulo",GXType.Char,200,0) ,
          new ParDef("@AV62Newblogwwds_7_tfnewblogsubtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@AV64Newblogwwds_9_tfnewblogvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV65Newblogwwds_10_tfnewblogvisitas_to",GXType.Int16,4,0) ,
          new ParDef("@GXPagingFrom2",GXType.Int32,9,0) ,
          new ParDef("@GXPagingTo2",GXType.Int32,9,0)
          };
          Object[] prmH002R3;
          prmH002R3 = new Object[] {
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV57Newblogwwds_2_tfnewblogid",GXType.Int16,4,0) ,
          new ParDef("@AV58Newblogwwds_3_tfnewblogid_to",GXType.Int16,4,0) ,
          new ParDef("@lV59Newblogwwds_4_tfnewblogtitulo",GXType.Char,200,0) ,
          new ParDef("@AV60Newblogwwds_5_tfnewblogtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@lV61Newblogwwds_6_tfnewblogsubtitulo",GXType.Char,200,0) ,
          new ParDef("@AV62Newblogwwds_7_tfnewblogsubtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@AV64Newblogwwds_9_tfnewblogvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV65Newblogwwds_10_tfnewblogvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("H002R2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH002R2,11, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("H002R3", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH002R3,1, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((string[]) buf[4])[0] = rslt.getVarchar(5);
                ((string[]) buf[5])[0] = rslt.getMultimediaUri(6);
                ((short[]) buf[6])[0] = rslt.getShort(7);
                ((string[]) buf[7])[0] = rslt.getMultimediaFile(8, rslt.getVarchar(6));
                return;
             case 1 :
                ((long[]) buf[0])[0] = rslt.getLong(1);
                return;
       }
    }

 }

}
