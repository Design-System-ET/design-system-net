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
namespace DesignSystem.Programs.workwithplus {
   public class wwp_parameterww : GXDataArea
   {
      public wwp_parameterww( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_parameterww( IGxContext context )
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
         nRC_GXsfl_37 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_37"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_37_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_37_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_37_idx = GetPar( "sGXsfl_37_idx");
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
         AV16OrderedBy = (short)(Math.Round(NumberUtil.Val( GetPar( "OrderedBy"), "."), 18, MidpointRounding.ToEven));
         AV17OrderedDsc = StringUtil.StrToBool( GetPar( "OrderedDsc"));
         AV19FilterFullText = GetPar( "FilterFullText");
         AV29ManageFiltersExecutionStep = (short)(Math.Round(NumberUtil.Val( GetPar( "ManageFiltersExecutionStep"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV24ColumnsSelector);
         AV55Pgmname = GetPar( "Pgmname");
         AV30TFWWPParameterKey = GetPar( "TFWWPParameterKey");
         AV31TFWWPParameterKey_Sel = GetPar( "TFWWPParameterKey_Sel");
         AV32TFWWPParameterCategory = GetPar( "TFWWPParameterCategory");
         AV33TFWWPParameterCategory_Sel = GetPar( "TFWWPParameterCategory_Sel");
         AV34TFWWPParameterDescription = GetPar( "TFWWPParameterDescription");
         AV35TFWWPParameterDescription_Sel = GetPar( "TFWWPParameterDescription_Sel");
         AV36TFWWPParameterValueTrimmed = GetPar( "TFWWPParameterValueTrimmed");
         AV37TFWWPParameterValueTrimmed_Sel = GetPar( "TFWWPParameterValueTrimmed_Sel");
         AV48IsAuthorized_Update = StringUtil.StrToBool( GetPar( "IsAuthorized_Update"));
         AV49IsAuthorized_Delete = StringUtil.StrToBool( GetPar( "IsAuthorized_Delete"));
         AV50IsAuthorized_Insert = StringUtil.StrToBool( GetPar( "IsAuthorized_Insert"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV16OrderedBy, AV17OrderedDsc, AV19FilterFullText, AV29ManageFiltersExecutionStep, AV24ColumnsSelector, AV55Pgmname, AV30TFWWPParameterKey, AV31TFWWPParameterKey_Sel, AV32TFWWPParameterCategory, AV33TFWWPParameterCategory_Sel, AV34TFWWPParameterDescription, AV35TFWWPParameterDescription_Sel, AV36TFWWPParameterValueTrimmed, AV37TFWWPParameterValueTrimmed_Sel, AV48IsAuthorized_Update, AV49IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
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
            return "wwp_parameterww_Execute" ;
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
         PA122( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START122( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("workwithplus.wwp_parameterww.aspx") +"\">") ;
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
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV48IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV48IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV49IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV49IsAuthorized_Delete, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV50IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV50IsAuthorized_Insert, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         GxWebStd.gx_hidden_field( context, "GXH_vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV16OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GXH_vORDEREDDSC", StringUtil.BoolToStr( AV17OrderedDsc));
         GxWebStd.gx_hidden_field( context, "GXH_vFILTERFULLTEXT", AV19FilterFullText);
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_37", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_37), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vMANAGEFILTERSDATA", AV27ManageFiltersData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vMANAGEFILTERSDATA", AV27ManageFiltersData);
         }
         GxWebStd.gx_hidden_field( context, "vGRIDCURRENTPAGE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV40GridCurrentPage), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV41GridPageCount), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDAPPLIEDFILTERS", AV8GridAppliedFilters);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV38DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV38DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCOLUMNSSELECTOR", AV24ColumnsSelector);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCOLUMNSSELECTOR", AV24ColumnsSelector);
         }
         GxWebStd.gx_hidden_field( context, "vMANAGEFILTERSEXECUTIONSTEP", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV29ManageFiltersExecutionStep), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV55Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV55Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERKEY", AV30TFWWPParameterKey);
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERKEY_SEL", AV31TFWWPParameterKey_Sel);
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERCATEGORY", AV32TFWWPParameterCategory);
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERCATEGORY_SEL", AV33TFWWPParameterCategory_Sel);
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERDESCRIPTION", AV34TFWWPParameterDescription);
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERDESCRIPTION_SEL", AV35TFWWPParameterDescription_Sel);
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERVALUETRIMMED", AV36TFWWPParameterValueTrimmed);
         GxWebStd.gx_hidden_field( context, "vTFWWPPARAMETERVALUETRIMMED_SEL", AV37TFWWPParameterValueTrimmed_Sel);
         GxWebStd.gx_hidden_field( context, "vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV16OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, "vORDEREDDSC", AV17OrderedDsc);
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV48IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV48IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV49IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV49IsAuthorized_Delete, context));
         GxWebStd.gx_boolean_hidden_field( context, "WWPPARAMETERDISABLEDELETE", A5WWPParameterDisableDelete);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGRIDSTATE", AV14GridState);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGRIDSTATE", AV14GridState);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV50IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV50IsAuthorized_Insert, context));
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "WWPPARAMETERVALUE", A2WWPParameterValue);
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
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Caption", StringUtil.RTrim( Ddo_grid_Caption));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_set", StringUtil.RTrim( Ddo_grid_Filteredtext_set));
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
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Includedatalist", StringUtil.RTrim( Ddo_grid_Includedatalist));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Datalisttype", StringUtil.RTrim( Ddo_grid_Datalisttype));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Datalistproc", StringUtil.RTrim( Ddo_grid_Datalistproc));
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
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Activeeventkey", StringUtil.RTrim( Ddo_grid_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedvalue_get", StringUtil.RTrim( Ddo_grid_Selectedvalue_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
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
            WE122( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT122( ) ;
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
         return formatLink("workwithplus.wwp_parameterww.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "WorkWithPlus.WWP_ParameterWW" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "WWP_Parameter_Transaction_Description", "") ;
      }

      protected void WB120( )
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtninsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(37), 2, 0)+","+"null"+");", context.GetMessage( "GXM_insert", ""), bttBtninsert_Jsonclick, 5, context.GetMessage( "GXM_insert", ""), "", StyleString, ClassString, bttBtninsert_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOINSERT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_WorkWithPlus/WWP_ParameterWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'',0)\"";
            ClassString = "hidden-xs";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtneditcolumns_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(37), 2, 0)+","+"null"+");", context.GetMessage( "WWP_EditColumnsCaption", ""), bttBtneditcolumns_Jsonclick, 0, context.GetMessage( "WWP_EditColumnsTooltip", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_WorkWithPlus/WWP_ParameterWW.htm");
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
            ucDdo_managefilters.SetProperty("DropDownOptionsData", AV27ManageFiltersData);
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'',false,'" + sGXsfl_37_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFilterfulltext_Internalname, AV19FilterFullText, StringUtil.RTrim( context.localUtil.Format( AV19FilterFullText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,28);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "WWP_Search", ""), edtavFilterfulltext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFilterfulltext_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "WWPFullTextFilter", "start", true, "", "HLP_WorkWithPlus/WWP_ParameterWW.htm");
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
            StartGridControl37( ) ;
         }
         if ( wbEnd == 37 )
         {
            wbEnd = 0;
            nRC_GXsfl_37 = (int)(nGXsfl_37_idx-1);
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
            ucGridpaginationbar.SetProperty("CurrentPage", AV40GridCurrentPage);
            ucGridpaginationbar.SetProperty("PageCount", AV41GridPageCount);
            ucGridpaginationbar.SetProperty("AppliedFilters", AV8GridAppliedFilters);
            ucGridpaginationbar.Render(context, "dvelop.dvpaginationbar", Gridpaginationbar_Internalname, "GRIDPAGINATIONBARContainer");
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
            /* Div Control */
            GxWebStd.gx_div_start( context, divHtml_bottomauxiliarcontrols_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDdo_grid.SetProperty("Caption", Ddo_grid_Caption);
            ucDdo_grid.SetProperty("ColumnIds", Ddo_grid_Columnids);
            ucDdo_grid.SetProperty("ColumnsSortValues", Ddo_grid_Columnssortvalues);
            ucDdo_grid.SetProperty("IncludeSortASC", Ddo_grid_Includesortasc);
            ucDdo_grid.SetProperty("Fixable", Ddo_grid_Fixable);
            ucDdo_grid.SetProperty("IncludeFilter", Ddo_grid_Includefilter);
            ucDdo_grid.SetProperty("FilterType", Ddo_grid_Filtertype);
            ucDdo_grid.SetProperty("IncludeDataList", Ddo_grid_Includedatalist);
            ucDdo_grid.SetProperty("DataListType", Ddo_grid_Datalisttype);
            ucDdo_grid.SetProperty("DataListProc", Ddo_grid_Datalistproc);
            ucDdo_grid.SetProperty("DropDownOptionsTitleSettingsIcons", AV38DDO_TitleSettingsIcons);
            ucDdo_grid.Render(context, "dvelop.gxbootstrap.ddogridtitlesettingsm", Ddo_grid_Internalname, "DDO_GRIDContainer");
            /* User Defined Control */
            ucDdo_gridcolumnsselector.SetProperty("IconType", Ddo_gridcolumnsselector_Icontype);
            ucDdo_gridcolumnsselector.SetProperty("Icon", Ddo_gridcolumnsselector_Icon);
            ucDdo_gridcolumnsselector.SetProperty("Caption", Ddo_gridcolumnsselector_Caption);
            ucDdo_gridcolumnsselector.SetProperty("Tooltip", Ddo_gridcolumnsselector_Tooltip);
            ucDdo_gridcolumnsselector.SetProperty("Cls", Ddo_gridcolumnsselector_Cls);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsType", Ddo_gridcolumnsselector_Dropdownoptionstype);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsTitleSettingsIcons", AV38DDO_TitleSettingsIcons);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsData", AV24ColumnsSelector);
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
         if ( wbEnd == 37 )
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

      protected void START122( )
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
         Form.Meta.addItem("description", context.GetMessage( "WWP_Parameter_Transaction_Description", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP120( ) ;
      }

      protected void WS122( )
      {
         START122( ) ;
         EVT122( ) ;
      }

      protected void EVT122( )
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
                              E11122 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changepage */
                              E12122 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changerowsperpage */
                              E13122 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRID.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_grid.Onoptionclicked */
                              E14122 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_gridcolumnsselector.Oncolumnschanged */
                              E15122 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOINSERT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoInsert' */
                              E16122 ();
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
                              nGXsfl_37_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_37_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_37_idx), 4, 0), 4, "0");
                              SubsflControlProps_372( ) ;
                              cmbavGridactions.Name = cmbavGridactions_Internalname;
                              cmbavGridactions.CurrentValue = cgiGet( cmbavGridactions_Internalname);
                              AV42GridActions = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavGridactions_Internalname), "."), 18, MidpointRounding.ToEven));
                              AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV42GridActions), 4, 0));
                              A1WWPParameterKey = cgiGet( edtWWPParameterKey_Internalname);
                              A3WWPParameterCategory = cgiGet( edtWWPParameterCategory_Internalname);
                              A4WWPParameterDescription = cgiGet( edtWWPParameterDescription_Internalname);
                              A6WWPParameterValueTrimmed = cgiGet( edtWWPParameterValueTrimmed_Internalname);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E17122 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E18122 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid.Load */
                                    E19122 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VGRIDACTIONS.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E20122 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       /* Set Refresh If Orderedby Changed */
                                       if ( ( context.localUtil.CToN( cgiGet( "GXH_vORDEREDBY"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) != Convert.ToDecimal( AV16OrderedBy )) )
                                       {
                                          Rfr0gs = true;
                                       }
                                       /* Set Refresh If Ordereddsc Changed */
                                       if ( StringUtil.StrToBool( cgiGet( "GXH_vORDEREDDSC")) != AV17OrderedDsc )
                                       {
                                          Rfr0gs = true;
                                       }
                                       /* Set Refresh If Filterfulltext Changed */
                                       if ( StringUtil.StrCmp(cgiGet( "GXH_vFILTERFULLTEXT"), AV19FilterFullText) != 0 )
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

      protected void WE122( )
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

      protected void PA122( )
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
         SubsflControlProps_372( ) ;
         while ( nGXsfl_37_idx <= nRC_GXsfl_37 )
         {
            sendrow_372( ) ;
            nGXsfl_37_idx = ((subGrid_Islastpage==1)&&(nGXsfl_37_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_37_idx+1);
            sGXsfl_37_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_37_idx), 4, 0), 4, "0");
            SubsflControlProps_372( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridContainer)) ;
         /* End function gxnrGrid_newrow */
      }

      protected void gxgrGrid_refresh( int subGrid_Rows ,
                                       short AV16OrderedBy ,
                                       bool AV17OrderedDsc ,
                                       string AV19FilterFullText ,
                                       short AV29ManageFiltersExecutionStep ,
                                       DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV24ColumnsSelector ,
                                       string AV55Pgmname ,
                                       string AV30TFWWPParameterKey ,
                                       string AV31TFWWPParameterKey_Sel ,
                                       string AV32TFWWPParameterCategory ,
                                       string AV33TFWWPParameterCategory_Sel ,
                                       string AV34TFWWPParameterDescription ,
                                       string AV35TFWWPParameterDescription_Sel ,
                                       string AV36TFWWPParameterValueTrimmed ,
                                       string AV37TFWWPParameterValueTrimmed_Sel ,
                                       bool AV48IsAuthorized_Update ,
                                       bool AV49IsAuthorized_Delete ,
                                       bool AV50IsAuthorized_Insert )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RF122( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_WWPPARAMETERKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( A1WWPParameterKey, "")), context));
         GxWebStd.gx_hidden_field( context, "WWPPARAMETERKEY", A1WWPParameterKey);
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
         RF122( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV55Pgmname = "WorkWithPlus.WWP_ParameterWW";
      }

      protected void RF122( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 37;
         /* Execute user event: Refresh */
         E18122 ();
         nGXsfl_37_idx = 1;
         sGXsfl_37_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_37_idx), 4, 0), 4, "0");
         SubsflControlProps_372( ) ;
         bGXsfl_37_Refreshing = true;
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
            SubsflControlProps_372( ) ;
            GXPagingFrom2 = (int)(((subGrid_Rows==0) ? 0 : GRID_nFirstRecordOnPage));
            GXPagingTo2 = ((subGrid_Rows==0) ? 10000 : subGrid_fnc_Recordsperpage( )+1);
            pr_default.dynParam(0, new Object[]{ new Object[]{
                                                 AV56Workwithplus_wwp_parameterwwds_1_filterfulltext ,
                                                 AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel ,
                                                 AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey ,
                                                 AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel ,
                                                 AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory ,
                                                 AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel ,
                                                 AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription ,
                                                 AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel ,
                                                 AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed ,
                                                 A1WWPParameterKey ,
                                                 A3WWPParameterCategory ,
                                                 A4WWPParameterDescription ,
                                                 A2WWPParameterValue ,
                                                 AV16OrderedBy ,
                                                 AV17OrderedDsc } ,
                                                 new int[]{
                                                 TypeConstants.SHORT, TypeConstants.BOOLEAN
                                                 }
            });
            lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
            lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
            lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
            lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
            lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = StringUtil.Concat( StringUtil.RTrim( AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey), "%", "");
            lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = StringUtil.Concat( StringUtil.RTrim( AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory), "%", "");
            lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = StringUtil.Concat( StringUtil.RTrim( AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription), "%", "");
            lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = StringUtil.Concat( StringUtil.RTrim( AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed), "%", "");
            /* Using cursor H00122 */
            pr_default.execute(0, new Object[] {lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey, AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel, lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory, AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel, lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription, AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel, lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed, AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel, GXPagingFrom2, GXPagingTo2});
            nGXsfl_37_idx = 1;
            sGXsfl_37_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_37_idx), 4, 0), 4, "0");
            SubsflControlProps_372( ) ;
            while ( ( (pr_default.getStatus(0) != 101) ) && ( ( ( subGrid_Rows == 0 ) || ( GRID_nCurrentRecord < subGrid_fnc_Recordsperpage( ) ) ) ) )
            {
               A5WWPParameterDisableDelete = H00122_A5WWPParameterDisableDelete[0];
               A4WWPParameterDescription = H00122_A4WWPParameterDescription[0];
               A3WWPParameterCategory = H00122_A3WWPParameterCategory[0];
               A1WWPParameterKey = H00122_A1WWPParameterKey[0];
               A2WWPParameterValue = H00122_A2WWPParameterValue[0];
               if ( StringUtil.Len( A2WWPParameterValue) <= 30 )
               {
                  A6WWPParameterValueTrimmed = A2WWPParameterValue;
               }
               else
               {
                  A6WWPParameterValueTrimmed = StringUtil.Trim( StringUtil.Substring( A2WWPParameterValue, 1, 27)) + "...";
               }
               /* Execute user event: Grid.Load */
               E19122 ();
               pr_default.readNext(0);
            }
            GRID_nEOF = (short)(((pr_default.getStatus(0) == 101) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            pr_default.close(0);
            wbEnd = 37;
            WB120( ) ;
         }
         bGXsfl_37_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes122( )
      {
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV55Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV55Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV48IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV48IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV49IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV49IsAuthorized_Delete, context));
         GxWebStd.gx_hidden_field( context, "gxhash_WWPPARAMETERKEY"+"_"+sGXsfl_37_idx, GetSecureSignedToken( sGXsfl_37_idx, StringUtil.RTrim( context.localUtil.Format( A1WWPParameterKey, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV50IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV50IsAuthorized_Insert, context));
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
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = AV19FilterFullText;
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = AV30TFWWPParameterKey;
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = AV31TFWWPParameterKey_Sel;
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = AV32TFWWPParameterCategory;
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = AV33TFWWPParameterCategory_Sel;
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = AV34TFWWPParameterDescription;
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = AV35TFWWPParameterDescription_Sel;
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = AV36TFWWPParameterValueTrimmed;
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = AV37TFWWPParameterValueTrimmed_Sel;
         pr_default.dynParam(1, new Object[]{ new Object[]{
                                              AV56Workwithplus_wwp_parameterwwds_1_filterfulltext ,
                                              AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel ,
                                              AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey ,
                                              AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel ,
                                              AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory ,
                                              AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel ,
                                              AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription ,
                                              AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel ,
                                              AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed ,
                                              A1WWPParameterKey ,
                                              A3WWPParameterCategory ,
                                              A4WWPParameterDescription ,
                                              A2WWPParameterValue ,
                                              AV16OrderedBy ,
                                              AV17OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
         lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
         lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
         lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext), "%", "");
         lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = StringUtil.Concat( StringUtil.RTrim( AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey), "%", "");
         lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = StringUtil.Concat( StringUtil.RTrim( AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory), "%", "");
         lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = StringUtil.Concat( StringUtil.RTrim( AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription), "%", "");
         lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = StringUtil.Concat( StringUtil.RTrim( AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed), "%", "");
         /* Using cursor H00123 */
         pr_default.execute(1, new Object[] {lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV56Workwithplus_wwp_parameterwwds_1_filterfulltext, lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey, AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel, lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory, AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel, lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription, AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel, lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed, AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel});
         GRID_nRecordCount = H00123_AGRID_nRecordCount[0];
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
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = AV19FilterFullText;
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = AV30TFWWPParameterKey;
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = AV31TFWWPParameterKey_Sel;
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = AV32TFWWPParameterCategory;
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = AV33TFWWPParameterCategory_Sel;
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = AV34TFWWPParameterDescription;
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = AV35TFWWPParameterDescription_Sel;
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = AV36TFWWPParameterValueTrimmed;
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = AV37TFWWPParameterValueTrimmed_Sel;
         GRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV16OrderedBy, AV17OrderedDsc, AV19FilterFullText, AV29ManageFiltersExecutionStep, AV24ColumnsSelector, AV55Pgmname, AV30TFWWPParameterKey, AV31TFWWPParameterKey_Sel, AV32TFWWPParameterCategory, AV33TFWWPParameterCategory_Sel, AV34TFWWPParameterDescription, AV35TFWWPParameterDescription_Sel, AV36TFWWPParameterValueTrimmed, AV37TFWWPParameterValueTrimmed_Sel, AV48IsAuthorized_Update, AV49IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_nextpage( )
      {
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = AV19FilterFullText;
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = AV30TFWWPParameterKey;
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = AV31TFWWPParameterKey_Sel;
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = AV32TFWWPParameterCategory;
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = AV33TFWWPParameterCategory_Sel;
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = AV34TFWWPParameterDescription;
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = AV35TFWWPParameterDescription_Sel;
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = AV36TFWWPParameterValueTrimmed;
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = AV37TFWWPParameterValueTrimmed_Sel;
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
            gxgrGrid_refresh( subGrid_Rows, AV16OrderedBy, AV17OrderedDsc, AV19FilterFullText, AV29ManageFiltersExecutionStep, AV24ColumnsSelector, AV55Pgmname, AV30TFWWPParameterKey, AV31TFWWPParameterKey_Sel, AV32TFWWPParameterCategory, AV33TFWWPParameterCategory_Sel, AV34TFWWPParameterDescription, AV35TFWWPParameterDescription_Sel, AV36TFWWPParameterValueTrimmed, AV37TFWWPParameterValueTrimmed_Sel, AV48IsAuthorized_Update, AV49IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgrid_previouspage( )
      {
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = AV19FilterFullText;
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = AV30TFWWPParameterKey;
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = AV31TFWWPParameterKey_Sel;
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = AV32TFWWPParameterCategory;
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = AV33TFWWPParameterCategory_Sel;
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = AV34TFWWPParameterDescription;
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = AV35TFWWPParameterDescription_Sel;
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = AV36TFWWPParameterValueTrimmed;
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = AV37TFWWPParameterValueTrimmed_Sel;
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
            gxgrGrid_refresh( subGrid_Rows, AV16OrderedBy, AV17OrderedDsc, AV19FilterFullText, AV29ManageFiltersExecutionStep, AV24ColumnsSelector, AV55Pgmname, AV30TFWWPParameterKey, AV31TFWWPParameterKey_Sel, AV32TFWWPParameterCategory, AV33TFWWPParameterCategory_Sel, AV34TFWWPParameterDescription, AV35TFWWPParameterDescription_Sel, AV36TFWWPParameterValueTrimmed, AV37TFWWPParameterValueTrimmed_Sel, AV48IsAuthorized_Update, AV49IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = AV19FilterFullText;
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = AV30TFWWPParameterKey;
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = AV31TFWWPParameterKey_Sel;
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = AV32TFWWPParameterCategory;
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = AV33TFWWPParameterCategory_Sel;
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = AV34TFWWPParameterDescription;
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = AV35TFWWPParameterDescription_Sel;
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = AV36TFWWPParameterValueTrimmed;
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = AV37TFWWPParameterValueTrimmed_Sel;
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
            gxgrGrid_refresh( subGrid_Rows, AV16OrderedBy, AV17OrderedDsc, AV19FilterFullText, AV29ManageFiltersExecutionStep, AV24ColumnsSelector, AV55Pgmname, AV30TFWWPParameterKey, AV31TFWWPParameterKey_Sel, AV32TFWWPParameterCategory, AV33TFWWPParameterCategory_Sel, AV34TFWWPParameterDescription, AV35TFWWPParameterDescription_Sel, AV36TFWWPParameterValueTrimmed, AV37TFWWPParameterValueTrimmed_Sel, AV48IsAuthorized_Update, AV49IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgrid_gotopage( int nPageNo )
      {
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = AV19FilterFullText;
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = AV30TFWWPParameterKey;
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = AV31TFWWPParameterKey_Sel;
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = AV32TFWWPParameterCategory;
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = AV33TFWWPParameterCategory_Sel;
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = AV34TFWWPParameterDescription;
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = AV35TFWWPParameterDescription_Sel;
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = AV36TFWWPParameterValueTrimmed;
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = AV37TFWWPParameterValueTrimmed_Sel;
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
            gxgrGrid_refresh( subGrid_Rows, AV16OrderedBy, AV17OrderedDsc, AV19FilterFullText, AV29ManageFiltersExecutionStep, AV24ColumnsSelector, AV55Pgmname, AV30TFWWPParameterKey, AV31TFWWPParameterKey_Sel, AV32TFWWPParameterCategory, AV33TFWWPParameterCategory_Sel, AV34TFWWPParameterDescription, AV35TFWWPParameterDescription_Sel, AV36TFWWPParameterValueTrimmed, AV37TFWWPParameterValueTrimmed_Sel, AV48IsAuthorized_Update, AV49IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV55Pgmname = "WorkWithPlus.WWP_ParameterWW";
         edtWWPParameterKey_Enabled = 0;
         edtWWPParameterCategory_Enabled = 0;
         edtWWPParameterDescription_Enabled = 0;
         edtWWPParameterValueTrimmed_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP120( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E17122 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vMANAGEFILTERSDATA"), AV27ManageFiltersData);
            ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV38DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( "vCOLUMNSSELECTOR"), AV24ColumnsSelector);
            /* Read saved values. */
            nRC_GXsfl_37 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_37"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV40GridCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDCURRENTPAGE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV41GridPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDPAGECOUNT"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV8GridAppliedFilters = cgiGet( "vGRIDAPPLIEDFILTERS");
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
            Ddo_grid_Caption = cgiGet( "DDO_GRID_Caption");
            Ddo_grid_Filteredtext_set = cgiGet( "DDO_GRID_Filteredtext_set");
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
            Ddo_grid_Includedatalist = cgiGet( "DDO_GRID_Includedatalist");
            Ddo_grid_Datalisttype = cgiGet( "DDO_GRID_Datalisttype");
            Ddo_grid_Datalistproc = cgiGet( "DDO_GRID_Datalistproc");
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
            Ddo_grid_Selectedcolumn = cgiGet( "DDO_GRID_Selectedcolumn");
            Ddo_grid_Filteredtext_get = cgiGet( "DDO_GRID_Filteredtext_get");
            Ddo_gridcolumnsselector_Columnsselectorvalues = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues");
            Ddo_managefilters_Activeeventkey = cgiGet( "DDO_MANAGEFILTERS_Activeeventkey");
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            /* Read variables values. */
            AV19FilterFullText = cgiGet( edtavFilterfulltext_Internalname);
            AssignAttri("", false, "AV19FilterFullText", AV19FilterFullText);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
            if ( ( context.localUtil.CToN( cgiGet( "GXH_vORDEREDBY"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) != Convert.ToDecimal( AV16OrderedBy )) )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrToBool( cgiGet( "GXH_vORDEREDDSC")) != AV17OrderedDsc )
            {
               GRID_nFirstRecordOnPage = 0;
            }
            if ( StringUtil.StrCmp(cgiGet( "GXH_vFILTERFULLTEXT"), AV19FilterFullText) != 0 )
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
         E17122 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
      }

      protected void E17122( )
      {
         /* Start Routine */
         returnInSub = false;
         subGrid_Rows = 10;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         Grid_empowerer_Gridinternalname = subGrid_Internalname;
         ucGrid_empowerer.SendProperty(context, "", false, Grid_empowerer_Internalname, "GridInternalName", Grid_empowerer_Gridinternalname);
         Ddo_gridcolumnsselector_Gridinternalname = subGrid_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, "", false, Ddo_gridcolumnsselector_Internalname, "GridInternalName", Ddo_gridcolumnsselector_Gridinternalname);
         if ( StringUtil.StrCmp(AV11HTTPRequest.Method, "GET") == 0 )
         {
            /* Execute user subroutine: 'LOADSAVEDFILTERS' */
            S112 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         AV46GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV47GAMErrors);
         Ddo_grid_Gridinternalname = subGrid_Internalname;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GridInternalName", Ddo_grid_Gridinternalname);
         Ddo_grid_Gamoauthtoken = AV46GAMSession.gxTpr_Token;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GAMOAuthToken", Ddo_grid_Gamoauthtoken);
         Form.Caption = context.GetMessage( "WWP_Parameter_Transaction_Description", "");
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
         if ( AV16OrderedBy < 1 )
         {
            AV16OrderedBy = 1;
            AssignAttri("", false, "AV16OrderedBy", StringUtil.LTrimStr( (decimal)(AV16OrderedBy), 4, 0));
            /* Execute user subroutine: 'SETDDOSORTEDSTATUS' */
            S142 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = AV38DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1) ;
         AV38DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1;
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = bttBtneditcolumns_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, "", false, Ddo_gridcolumnsselector_Internalname, "TitleControlIdToReplace", Ddo_gridcolumnsselector_Titlecontrolidtoreplace);
         Gridpaginationbar_Rowsperpageselectedvalue = subGrid_Rows;
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0));
      }

      protected void E18122( )
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
         if ( AV29ManageFiltersExecutionStep == 1 )
         {
            AV29ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV29ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV29ManageFiltersExecutionStep), 1, 0));
         }
         else if ( AV29ManageFiltersExecutionStep == 2 )
         {
            AV29ManageFiltersExecutionStep = 0;
            AssignAttri("", false, "AV29ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV29ManageFiltersExecutionStep), 1, 0));
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
         if ( StringUtil.StrCmp(AV26Session.Get("WorkWithPlus.WWP_ParameterWWColumnsSelector"), "") != 0 )
         {
            AV22ColumnsSelectorXML = AV26Session.Get("WorkWithPlus.WWP_ParameterWWColumnsSelector");
            AV24ColumnsSelector.FromXml(AV22ColumnsSelectorXML, null, "", "");
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
         edtWWPParameterKey_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(1)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtWWPParameterKey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPParameterKey_Visible), 5, 0), !bGXsfl_37_Refreshing);
         edtWWPParameterCategory_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(2)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtWWPParameterCategory_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPParameterCategory_Visible), 5, 0), !bGXsfl_37_Refreshing);
         edtWWPParameterDescription_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(3)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtWWPParameterDescription_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPParameterDescription_Visible), 5, 0), !bGXsfl_37_Refreshing);
         edtWWPParameterValueTrimmed_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(4)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtWWPParameterValueTrimmed_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtWWPParameterValueTrimmed_Visible), 5, 0), !bGXsfl_37_Refreshing);
         AV40GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri("", false, "AV40GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV40GridCurrentPage), 10, 0));
         AV41GridPageCount = subGrid_fnc_Pagecount( );
         AssignAttri("", false, "AV41GridPageCount", StringUtil.LTrimStr( (decimal)(AV41GridPageCount), 10, 0));
         GXt_char2 = AV8GridAppliedFilters;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getappliedfiltersdescription(context ).execute(  AV55Pgmname, out  GXt_char2) ;
         AV8GridAppliedFilters = GXt_char2;
         AssignAttri("", false, "AV8GridAppliedFilters", AV8GridAppliedFilters);
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = AV19FilterFullText;
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = AV30TFWWPParameterKey;
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = AV31TFWWPParameterKey_Sel;
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = AV32TFWWPParameterCategory;
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = AV33TFWWPParameterCategory_Sel;
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = AV34TFWWPParameterDescription;
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = AV35TFWWPParameterDescription_Sel;
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = AV36TFWWPParameterValueTrimmed;
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = AV37TFWWPParameterValueTrimmed_Sel;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ColumnsSelector", AV24ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV27ManageFiltersData", AV27ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV14GridState", AV14GridState);
      }

      protected void E12122( )
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
            AV39PageToGo = (int)(Math.Round(NumberUtil.Val( Gridpaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            subgrid_gotopage( AV39PageToGo) ;
         }
      }

      protected void E13122( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      protected void E14122( )
      {
         /* Ddo_grid_Onoptionclicked Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderASC#>") == 0 ) || ( StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>") == 0 ) )
         {
            AV16OrderedBy = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Selectedvalue_get, "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV16OrderedBy", StringUtil.LTrimStr( (decimal)(AV16OrderedBy), 4, 0));
            AV17OrderedDsc = ((StringUtil.StrCmp(Ddo_grid_Activeeventkey, "<#OrderDSC#>")==0) ? true : false);
            AssignAttri("", false, "AV17OrderedDsc", AV17OrderedDsc);
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
            if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPParameterKey") == 0 )
            {
               AV30TFWWPParameterKey = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV30TFWWPParameterKey", AV30TFWWPParameterKey);
               AV31TFWWPParameterKey_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV31TFWWPParameterKey_Sel", AV31TFWWPParameterKey_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPParameterCategory") == 0 )
            {
               AV32TFWWPParameterCategory = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV32TFWWPParameterCategory", AV32TFWWPParameterCategory);
               AV33TFWWPParameterCategory_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV33TFWWPParameterCategory_Sel", AV33TFWWPParameterCategory_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPParameterDescription") == 0 )
            {
               AV34TFWWPParameterDescription = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV34TFWWPParameterDescription", AV34TFWWPParameterDescription);
               AV35TFWWPParameterDescription_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV35TFWWPParameterDescription_Sel", AV35TFWWPParameterDescription_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "WWPParameterValueTrimmed") == 0 )
            {
               AV36TFWWPParameterValueTrimmed = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV36TFWWPParameterValueTrimmed", AV36TFWWPParameterValueTrimmed);
               AV37TFWWPParameterValueTrimmed_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV37TFWWPParameterValueTrimmed_Sel", AV37TFWWPParameterValueTrimmed_Sel);
            }
            subgrid_firstpage( ) ;
         }
         /*  Sending Event outputs  */
      }

      private void E19122( )
      {
         /* Grid_Load Routine */
         returnInSub = false;
         cmbavGridactions.removeAllItems();
         cmbavGridactions.addItem("0", ";fa fa-bars", 0);
         if ( AV48IsAuthorized_Update )
         {
            cmbavGridactions.addItem("1", StringUtil.Format( "%1;%2", context.GetMessage( "GXM_update", ""), "fa fa-pen", "", "", "", "", "", "", ""), 0);
         }
         if ( AV49IsAuthorized_Delete )
         {
            if ( ! A5WWPParameterDisableDelete )
            {
               cmbavGridactions.addItem("2", StringUtil.Format( "%1;%2", context.GetMessage( "GX_BtnDelete", ""), "fa fa-times", "", "", "", "", "", "", ""), 0);
            }
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
            wbStart = 37;
         }
         sendrow_372( ) ;
         GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
         if ( isFullAjaxMode( ) && ! bGXsfl_37_Refreshing )
         {
            DoAjaxLoad(37, GridRow);
         }
         /*  Sending Event outputs  */
         cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0));
      }

      protected void E15122( )
      {
         /* Ddo_gridcolumnsselector_Oncolumnschanged Routine */
         returnInSub = false;
         AV22ColumnsSelectorXML = Ddo_gridcolumnsselector_Columnsselectorvalues;
         AV24ColumnsSelector.FromJSonString(AV22ColumnsSelectorXML, null);
         new DesignSystem.Programs.wwpbaseobjects.savecolumnsselectorstate(context ).execute(  "WorkWithPlus.WWP_ParameterWWColumnsSelector",  (String.IsNullOrEmpty(StringUtil.RTrim( AV22ColumnsSelectorXML)) ? "" : AV24ColumnsSelector.ToXml(false, true, "", ""))) ;
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ColumnsSelector", AV24ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV27ManageFiltersData", AV27ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV14GridState", AV14GridState);
      }

      protected void E11122( )
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
            GXEncryptionTmp = "wwpbaseobjects.savefilteras.aspx"+UrlEncode(StringUtil.RTrim("WorkWithPlus.WWP_ParameterWWFilters")) + "," + UrlEncode(StringUtil.RTrim(AV55Pgmname+"GridState"));
            context.PopUp(formatLink("wwpbaseobjects.savefilteras.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV29ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV29ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV29ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Manage#>") == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "wwpbaseobjects.managefilters.aspx"+UrlEncode(StringUtil.RTrim("WorkWithPlus.WWP_ParameterWWFilters"));
            context.PopUp(formatLink("wwpbaseobjects.managefilters.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV29ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV29ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV29ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else
         {
            GXt_char2 = AV28ManageFiltersXml;
            new DesignSystem.Programs.wwpbaseobjects.getfilterbyname(context ).execute(  "WorkWithPlus.WWP_ParameterWWFilters",  Ddo_managefilters_Activeeventkey, out  GXt_char2) ;
            AV28ManageFiltersXml = GXt_char2;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV28ManageFiltersXml)) )
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
               new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV55Pgmname+"GridState",  AV28ManageFiltersXml) ;
               AV14GridState.FromXml(AV28ManageFiltersXml, null, "", "");
               AV16OrderedBy = AV14GridState.gxTpr_Orderedby;
               AssignAttri("", false, "AV16OrderedBy", StringUtil.LTrimStr( (decimal)(AV16OrderedBy), 4, 0));
               AV17OrderedDsc = AV14GridState.gxTpr_Ordereddsc;
               AssignAttri("", false, "AV17OrderedDsc", AV17OrderedDsc);
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
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV14GridState", AV14GridState);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ColumnsSelector", AV24ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV27ManageFiltersData", AV27ManageFiltersData);
      }

      protected void E20122( )
      {
         /* Gridactions_Click Routine */
         returnInSub = false;
         if ( AV42GridActions == 1 )
         {
            /* Execute user subroutine: 'DO UPDATE' */
            S202 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV42GridActions == 2 )
         {
            /* Execute user subroutine: 'DO DELETE' */
            S212 ();
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
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ColumnsSelector", AV24ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV27ManageFiltersData", AV27ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV14GridState", AV14GridState);
      }

      protected void E16122( )
      {
         /* 'DoInsert' Routine */
         returnInSub = false;
         if ( AV50IsAuthorized_Insert )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "workwithplus.wwp_parameter.aspx"+UrlEncode(StringUtil.RTrim("INS")) + "," + UrlEncode(StringUtil.RTrim(""));
            CallWebObject(formatLink("workwithplus.wwp_parameter.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ColumnsSelector", AV24ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV27ManageFiltersData", AV27ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV14GridState", AV14GridState);
      }

      protected void S142( )
      {
         /* 'SETDDOSORTEDSTATUS' Routine */
         returnInSub = false;
         Ddo_grid_Sortedstatus = StringUtil.Trim( StringUtil.Str( (decimal)(AV16OrderedBy), 4, 0))+":"+(AV17OrderedDsc ? "DSC" : "ASC");
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SortedStatus", Ddo_grid_Sortedstatus);
      }

      protected void S172( )
      {
         /* 'INITIALIZECOLUMNSSELECTOR' Routine */
         returnInSub = false;
         AV24ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "WWPParameterKey",  "",  "WWP_ParameterKey_Attribute_ContextualTitle",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "WWPParameterCategory",  "",  "WWP_ParameterCategory_Attribute_ContextualTitle",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "WWPParameterDescription",  "",  "WWP_ParameterDescription_Attribute_ContextualTitle",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "WWPParameterValueTrimmed",  "",  "WWP_ParameterValue_Attribute_ContextualTitle",  true,  "") ;
         GXt_char2 = AV23UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "WorkWithPlus.WWP_ParameterWWColumnsSelector", out  GXt_char2) ;
         AV23UserCustomValue = GXt_char2;
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV23UserCustomValue)) ) )
         {
            AV25ColumnsSelectorAux.FromXml(AV23UserCustomValue, null, "", "");
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnselector_updatecolumns(context ).execute( ref  AV25ColumnsSelectorAux, ref  AV24ColumnsSelector) ;
         }
      }

      protected void S152( )
      {
         /* 'CHECKSECURITYFORACTIONS' Routine */
         returnInSub = false;
         GXt_boolean3 = AV48IsAuthorized_Update;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "wwp_parameter_Update", out  GXt_boolean3) ;
         AV48IsAuthorized_Update = GXt_boolean3;
         AssignAttri("", false, "AV48IsAuthorized_Update", AV48IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV48IsAuthorized_Update, context));
         GXt_boolean3 = AV49IsAuthorized_Delete;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "wwp_parameter_Delete", out  GXt_boolean3) ;
         AV49IsAuthorized_Delete = GXt_boolean3;
         AssignAttri("", false, "AV49IsAuthorized_Delete", AV49IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV49IsAuthorized_Delete, context));
         GXt_boolean3 = AV50IsAuthorized_Insert;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "wwp_parameter_Insert", out  GXt_boolean3) ;
         AV50IsAuthorized_Insert = GXt_boolean3;
         AssignAttri("", false, "AV50IsAuthorized_Insert", AV50IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV50IsAuthorized_Insert, context));
         if ( ! ( AV50IsAuthorized_Insert ) )
         {
            bttBtninsert_Visible = 0;
            AssignProp("", false, bttBtninsert_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtninsert_Visible), 5, 0), true);
         }
      }

      protected void S112( )
      {
         /* 'LOADSAVEDFILTERS' Routine */
         returnInSub = false;
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 = AV27ManageFiltersData;
         new DesignSystem.Programs.wwpbaseobjects.wwp_managefiltersloadsavedfilters(context ).execute(  "WorkWithPlus.WWP_ParameterWWFilters",  "",  "",  false, out  GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4) ;
         AV27ManageFiltersData = GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4;
      }

      protected void S182( )
      {
         /* 'CLEANFILTERS' Routine */
         returnInSub = false;
         AV19FilterFullText = "";
         AssignAttri("", false, "AV19FilterFullText", AV19FilterFullText);
         AV30TFWWPParameterKey = "";
         AssignAttri("", false, "AV30TFWWPParameterKey", AV30TFWWPParameterKey);
         AV31TFWWPParameterKey_Sel = "";
         AssignAttri("", false, "AV31TFWWPParameterKey_Sel", AV31TFWWPParameterKey_Sel);
         AV32TFWWPParameterCategory = "";
         AssignAttri("", false, "AV32TFWWPParameterCategory", AV32TFWWPParameterCategory);
         AV33TFWWPParameterCategory_Sel = "";
         AssignAttri("", false, "AV33TFWWPParameterCategory_Sel", AV33TFWWPParameterCategory_Sel);
         AV34TFWWPParameterDescription = "";
         AssignAttri("", false, "AV34TFWWPParameterDescription", AV34TFWWPParameterDescription);
         AV35TFWWPParameterDescription_Sel = "";
         AssignAttri("", false, "AV35TFWWPParameterDescription_Sel", AV35TFWWPParameterDescription_Sel);
         AV36TFWWPParameterValueTrimmed = "";
         AssignAttri("", false, "AV36TFWWPParameterValueTrimmed", AV36TFWWPParameterValueTrimmed);
         AV37TFWWPParameterValueTrimmed_Sel = "";
         AssignAttri("", false, "AV37TFWWPParameterValueTrimmed_Sel", AV37TFWWPParameterValueTrimmed_Sel);
         Ddo_grid_Selectedvalue_set = "";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         Ddo_grid_Filteredtext_set = "";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
      }

      protected void S202( )
      {
         /* 'DO UPDATE' Routine */
         returnInSub = false;
         if ( AV48IsAuthorized_Update )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "workwithplus.wwp_parameter.aspx"+UrlEncode(StringUtil.RTrim("UPD")) + "," + UrlEncode(StringUtil.RTrim(A1WWPParameterKey));
            CallWebObject(formatLink("workwithplus.wwp_parameter.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         /* 'DO DELETE' Routine */
         returnInSub = false;
         if ( AV49IsAuthorized_Delete )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "workwithplus.wwp_parameter.aspx"+UrlEncode(StringUtil.RTrim("DLT")) + "," + UrlEncode(StringUtil.RTrim(A1WWPParameterKey));
            CallWebObject(formatLink("workwithplus.wwp_parameter.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         if ( StringUtil.StrCmp(AV26Session.Get(AV55Pgmname+"GridState"), "") == 0 )
         {
            AV14GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  AV55Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV14GridState.FromXml(AV26Session.Get(AV55Pgmname+"GridState"), null, "", "");
         }
         AV16OrderedBy = AV14GridState.gxTpr_Orderedby;
         AssignAttri("", false, "AV16OrderedBy", StringUtil.LTrimStr( (decimal)(AV16OrderedBy), 4, 0));
         AV17OrderedDsc = AV14GridState.gxTpr_Ordereddsc;
         AssignAttri("", false, "AV17OrderedDsc", AV17OrderedDsc);
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
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV14GridState.gxTpr_Pagesize))) )
         {
            subGrid_Rows = (int)(Math.Round(NumberUtil.Val( AV14GridState.gxTpr_Pagesize, "."), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         }
         subgrid_gotopage( AV14GridState.gxTpr_Currentpage) ;
      }

      protected void S192( )
      {
         /* 'LOADREGFILTERSSTATE' Routine */
         returnInSub = false;
         AV65GXV1 = 1;
         while ( AV65GXV1 <= AV14GridState.gxTpr_Filtervalues.Count )
         {
            AV15GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV14GridState.gxTpr_Filtervalues.Item(AV65GXV1));
            if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV19FilterFullText = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV19FilterFullText", AV19FilterFullText);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERKEY") == 0 )
            {
               AV30TFWWPParameterKey = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV30TFWWPParameterKey", AV30TFWWPParameterKey);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERKEY_SEL") == 0 )
            {
               AV31TFWWPParameterKey_Sel = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV31TFWWPParameterKey_Sel", AV31TFWWPParameterKey_Sel);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERCATEGORY") == 0 )
            {
               AV32TFWWPParameterCategory = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV32TFWWPParameterCategory", AV32TFWWPParameterCategory);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERCATEGORY_SEL") == 0 )
            {
               AV33TFWWPParameterCategory_Sel = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV33TFWWPParameterCategory_Sel", AV33TFWWPParameterCategory_Sel);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERDESCRIPTION") == 0 )
            {
               AV34TFWWPParameterDescription = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV34TFWWPParameterDescription", AV34TFWWPParameterDescription);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERDESCRIPTION_SEL") == 0 )
            {
               AV35TFWWPParameterDescription_Sel = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV35TFWWPParameterDescription_Sel", AV35TFWWPParameterDescription_Sel);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERVALUETRIMMED") == 0 )
            {
               AV36TFWWPParameterValueTrimmed = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV36TFWWPParameterValueTrimmed", AV36TFWWPParameterValueTrimmed);
            }
            else if ( StringUtil.StrCmp(AV15GridStateFilterValue.gxTpr_Name, "TFWWPPARAMETERVALUETRIMMED_SEL") == 0 )
            {
               AV37TFWWPParameterValueTrimmed_Sel = AV15GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV37TFWWPParameterValueTrimmed_Sel", AV37TFWWPParameterValueTrimmed_Sel);
            }
            AV65GXV1 = (int)(AV65GXV1+1);
         }
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV31TFWWPParameterKey_Sel)),  AV31TFWWPParameterKey_Sel, out  GXt_char2) ;
         GXt_char5 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV33TFWWPParameterCategory_Sel)),  AV33TFWWPParameterCategory_Sel, out  GXt_char5) ;
         GXt_char6 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV35TFWWPParameterDescription_Sel)),  AV35TFWWPParameterDescription_Sel, out  GXt_char6) ;
         GXt_char7 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV37TFWWPParameterValueTrimmed_Sel)),  AV37TFWWPParameterValueTrimmed_Sel, out  GXt_char7) ;
         Ddo_grid_Selectedvalue_set = GXt_char2+"|"+GXt_char5+"|"+GXt_char6+"|"+GXt_char7;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         GXt_char7 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV30TFWWPParameterKey)),  AV30TFWWPParameterKey, out  GXt_char7) ;
         GXt_char6 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV32TFWWPParameterCategory)),  AV32TFWWPParameterCategory, out  GXt_char6) ;
         GXt_char5 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV34TFWWPParameterDescription)),  AV34TFWWPParameterDescription, out  GXt_char5) ;
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV36TFWWPParameterValueTrimmed)),  AV36TFWWPParameterValueTrimmed, out  GXt_char2) ;
         Ddo_grid_Filteredtext_set = GXt_char7+"|"+GXt_char6+"|"+GXt_char5+"|"+GXt_char2;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
      }

      protected void S162( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV14GridState.FromXml(AV26Session.Get(AV55Pgmname+"GridState"), null, "", "");
         AV14GridState.gxTpr_Orderedby = AV16OrderedBy;
         AV14GridState.gxTpr_Ordereddsc = AV17OrderedDsc;
         AV14GridState.gxTpr_Filtervalues.Clear();
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV14GridState,  "FILTERFULLTEXT",  context.GetMessage( "WWP_FullTextFilterDescription", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV19FilterFullText)),  0,  AV19FilterFullText,  AV19FilterFullText,  false,  "",  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV14GridState,  "TFWWPPARAMETERKEY",  context.GetMessage( "WWP_ParameterKey_Attribute_ContextualTitle", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV30TFWWPParameterKey)),  0,  AV30TFWWPParameterKey,  AV30TFWWPParameterKey,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV31TFWWPParameterKey_Sel)),  AV31TFWWPParameterKey_Sel,  AV31TFWWPParameterKey_Sel) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV14GridState,  "TFWWPPARAMETERCATEGORY",  context.GetMessage( "WWP_ParameterCategory_Attribute_ContextualTitle", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV32TFWWPParameterCategory)),  0,  AV32TFWWPParameterCategory,  AV32TFWWPParameterCategory,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV33TFWWPParameterCategory_Sel)),  AV33TFWWPParameterCategory_Sel,  AV33TFWWPParameterCategory_Sel) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV14GridState,  "TFWWPPARAMETERDESCRIPTION",  context.GetMessage( "WWP_ParameterDescription_Attribute_ContextualTitle", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV34TFWWPParameterDescription)),  0,  AV34TFWWPParameterDescription,  AV34TFWWPParameterDescription,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV35TFWWPParameterDescription_Sel)),  AV35TFWWPParameterDescription_Sel,  AV35TFWWPParameterDescription_Sel) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV14GridState,  "TFWWPPARAMETERVALUETRIMMED",  context.GetMessage( "WWP_ParameterValue_Attribute_ContextualTitle", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV36TFWWPParameterValueTrimmed)),  0,  AV36TFWWPParameterValueTrimmed,  AV36TFWWPParameterValueTrimmed,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV37TFWWPParameterValueTrimmed_Sel)),  AV37TFWWPParameterValueTrimmed_Sel,  AV37TFWWPParameterValueTrimmed_Sel) ;
         AV14GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         AV14GridState.gxTpr_Currentpage = (short)(subGrid_fnc_Currentpage( ));
         new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV55Pgmname+"GridState",  AV14GridState.ToXml(false, true, "", "")) ;
      }

      protected void S122( )
      {
         /* 'PREPARETRANSACTION' Routine */
         returnInSub = false;
         AV12TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV12TrnContext.gxTpr_Callerobject = AV55Pgmname;
         AV12TrnContext.gxTpr_Callerondelete = true;
         AV12TrnContext.gxTpr_Callerurl = AV11HTTPRequest.ScriptName+"?"+AV11HTTPRequest.QueryString;
         AV12TrnContext.gxTpr_Transactionname = "WorkWithPlus.WWP_Parameter";
         AV26Session.Set("TrnContext", AV12TrnContext.ToXml(false, true, "", ""));
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
         PA122( ) ;
         WS122( ) ;
         WE122( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623494788", true, true);
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
         context.AddJavascriptSource("workwithplus/wwp_parameterww.js", "?2024121623494791", false, true);
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
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_372( )
      {
         cmbavGridactions_Internalname = "vGRIDACTIONS_"+sGXsfl_37_idx;
         edtWWPParameterKey_Internalname = "WWPPARAMETERKEY_"+sGXsfl_37_idx;
         edtWWPParameterCategory_Internalname = "WWPPARAMETERCATEGORY_"+sGXsfl_37_idx;
         edtWWPParameterDescription_Internalname = "WWPPARAMETERDESCRIPTION_"+sGXsfl_37_idx;
         edtWWPParameterValueTrimmed_Internalname = "WWPPARAMETERVALUETRIMMED_"+sGXsfl_37_idx;
      }

      protected void SubsflControlProps_fel_372( )
      {
         cmbavGridactions_Internalname = "vGRIDACTIONS_"+sGXsfl_37_fel_idx;
         edtWWPParameterKey_Internalname = "WWPPARAMETERKEY_"+sGXsfl_37_fel_idx;
         edtWWPParameterCategory_Internalname = "WWPPARAMETERCATEGORY_"+sGXsfl_37_fel_idx;
         edtWWPParameterDescription_Internalname = "WWPPARAMETERDESCRIPTION_"+sGXsfl_37_fel_idx;
         edtWWPParameterValueTrimmed_Internalname = "WWPPARAMETERVALUETRIMMED_"+sGXsfl_37_fel_idx;
      }

      protected void sendrow_372( )
      {
         sGXsfl_37_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_37_idx), 4, 0), 4, "0");
         SubsflControlProps_372( ) ;
         WB120( ) ;
         if ( ( subGrid_Rows * 1 == 0 ) || ( nGXsfl_37_idx <= subGrid_fnc_Recordsperpage( ) * 1 ) )
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
               if ( ((int)((nGXsfl_37_idx) % (2))) == 0 )
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
               context.WriteHtmlText( " gxrow=\""+sGXsfl_37_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'" + sGXsfl_37_idx + "',37)\"";
            if ( ( cmbavGridactions.ItemCount == 0 ) && isAjaxCallMode( ) )
            {
               GXCCtl = "vGRIDACTIONS_" + sGXsfl_37_idx;
               cmbavGridactions.Name = GXCCtl;
               cmbavGridactions.WebTags = "";
               if ( cmbavGridactions.ItemCount > 0 )
               {
                  AV42GridActions = (short)(Math.Round(NumberUtil.Val( cmbavGridactions.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0))), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV42GridActions), 4, 0));
               }
            }
            /* ComboBox */
            GridRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavGridactions,(string)cmbavGridactions_Internalname,StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0)),(short)1,(string)cmbavGridactions_Jsonclick,(short)5,"'"+""+"'"+",false,"+"'"+"EVGRIDACTIONS.CLICK."+sGXsfl_37_idx+"'",(string)"int",(string)"",(short)-1,(short)1,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)cmbavGridactions_Class,(string)"WWActionGroupColumn",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"",(string)"",(bool)true,(short)0});
            cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0));
            AssignProp("", false, cmbavGridactions_Internalname, "Values", (string)(cmbavGridactions.ToJavascriptSource()), !bGXsfl_37_Refreshing);
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPParameterKey_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPParameterKey_Internalname,(string)A1WWPParameterKey,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPParameterKey_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPParameterKey_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)37,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPParameterCategory_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPParameterCategory_Internalname,(string)A3WWPParameterCategory,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPParameterCategory_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPParameterCategory_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)37,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPParameterDescription_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPParameterDescription_Internalname,(string)A4WWPParameterDescription,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPParameterDescription_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPParameterDescription_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)37,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtWWPParameterValueTrimmed_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtWWPParameterValueTrimmed_Internalname,(string)A6WWPParameterValueTrimmed,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtWWPParameterValueTrimmed_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtWWPParameterValueTrimmed_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)30,(short)0,(short)0,(short)37,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes122( ) ;
            GridContainer.AddRow(GridRow);
            nGXsfl_37_idx = ((subGrid_Islastpage==1)&&(nGXsfl_37_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_37_idx+1);
            sGXsfl_37_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_37_idx), 4, 0), 4, "0");
            SubsflControlProps_372( ) ;
         }
         /* End function sendrow_372 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "vGRIDACTIONS_" + sGXsfl_37_idx;
         cmbavGridactions.Name = GXCCtl;
         cmbavGridactions.WebTags = "";
         if ( cmbavGridactions.ItemCount > 0 )
         {
            AV42GridActions = (short)(Math.Round(NumberUtil.Val( cmbavGridactions.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV42GridActions), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV42GridActions), 4, 0));
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl37( )
      {
         if ( GridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridContainer"+"DivS\" data-gxgridid=\"37\">") ;
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
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPParameterKey_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_ParameterKey_Attribute_ContextualTitle", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPParameterCategory_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_ParameterCategory_Attribute_ContextualTitle", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPParameterDescription_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_ParameterDescription_Attribute_ContextualTitle", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtWWPParameterValueTrimmed_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_ParameterValue_Attribute_ContextualTitle", "")) ;
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
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A1WWPParameterKey));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPParameterKey_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A3WWPParameterCategory));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPParameterCategory_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A4WWPParameterDescription));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPParameterDescription_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A6WWPParameterValueTrimmed));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtWWPParameterValueTrimmed_Visible), 5, 0, ".", "")));
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
         bttBtneditcolumns_Internalname = "BTNEDITCOLUMNS";
         divTableactions_Internalname = "TABLEACTIONS";
         Ddo_managefilters_Internalname = "DDO_MANAGEFILTERS";
         edtavFilterfulltext_Internalname = "vFILTERFULLTEXT";
         divTablefilters_Internalname = "TABLEFILTERS";
         divTablerightheader_Internalname = "TABLERIGHTHEADER";
         divTableheadercontent_Internalname = "TABLEHEADERCONTENT";
         divTableheader_Internalname = "TABLEHEADER";
         cmbavGridactions_Internalname = "vGRIDACTIONS";
         edtWWPParameterKey_Internalname = "WWPPARAMETERKEY";
         edtWWPParameterCategory_Internalname = "WWPPARAMETERCATEGORY";
         edtWWPParameterDescription_Internalname = "WWPPARAMETERDESCRIPTION";
         edtWWPParameterValueTrimmed_Internalname = "WWPPARAMETERVALUETRIMMED";
         Gridpaginationbar_Internalname = "GRIDPAGINATIONBAR";
         divGridtablewithpaginationbar_Internalname = "GRIDTABLEWITHPAGINATIONBAR";
         divTablemain_Internalname = "TABLEMAIN";
         Ddo_grid_Internalname = "DDO_GRID";
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
         edtWWPParameterValueTrimmed_Jsonclick = "";
         edtWWPParameterDescription_Jsonclick = "";
         edtWWPParameterCategory_Jsonclick = "";
         edtWWPParameterKey_Jsonclick = "";
         cmbavGridactions_Jsonclick = "";
         cmbavGridactions_Class = "ConvertToDDO";
         subGrid_Class = "GridWithPaginationBar GridNoBorder WorkWith";
         subGrid_Backcolorstyle = 0;
         edtWWPParameterValueTrimmed_Visible = -1;
         edtWWPParameterDescription_Visible = -1;
         edtWWPParameterCategory_Visible = -1;
         edtWWPParameterKey_Visible = -1;
         edtWWPParameterValueTrimmed_Enabled = 0;
         edtWWPParameterDescription_Enabled = 0;
         edtWWPParameterCategory_Enabled = 0;
         edtWWPParameterKey_Enabled = 0;
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
         Ddo_grid_Datalistproc = "WorkWithPlus.WWP_ParameterWWGetFilterData";
         Ddo_grid_Datalisttype = "Dynamic|Dynamic|Dynamic|Dynamic";
         Ddo_grid_Includedatalist = "T";
         Ddo_grid_Filtertype = "Character|Character|Character|Character";
         Ddo_grid_Includefilter = "T";
         Ddo_grid_Fixable = "T";
         Ddo_grid_Includesortasc = "T|T|T|";
         Ddo_grid_Columnssortvalues = "1|2|3|";
         Ddo_grid_Columnids = "1:WWPParameterKey|2:WWPParameterCategory|3:WWPParameterDescription|4:WWPParameterValueTrimmed";
         Ddo_grid_Gridinternalname = "";
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
         Form.Caption = context.GetMessage( "WWP_Parameter_Transaction_Description", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPParameterKey_Visible","ctrl":"WWPPARAMETERKEY","prop":"Visible"},{"av":"edtWWPParameterCategory_Visible","ctrl":"WWPPARAMETERCATEGORY","prop":"Visible"},{"av":"edtWWPParameterDescription_Visible","ctrl":"WWPPARAMETERDESCRIPTION","prop":"Visible"},{"av":"edtWWPParameterValueTrimmed_Visible","ctrl":"WWPPARAMETERVALUETRIMMED","prop":"Visible"},{"av":"AV40GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV41GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV8GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV27ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV14GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E12122","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E13122","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED","""{"handler":"E14122","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_grid_Activeeventkey","ctrl":"DDO_GRID","prop":"ActiveEventKey"},{"av":"Ddo_grid_Selectedvalue_get","ctrl":"DDO_GRID","prop":"SelectedValue_get"},{"av":"Ddo_grid_Selectedcolumn","ctrl":"DDO_GRID","prop":"SelectedColumn"},{"av":"Ddo_grid_Filteredtext_get","ctrl":"DDO_GRID","prop":"FilteredText_get"}]""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED",""","oparms":[{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E19122","iparms":[{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"A5WWPParameterDisableDelete","fld":"WWPPARAMETERDISABLEDELETE"}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"}]}""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED","""{"handler":"E15122","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_gridcolumnsselector_Columnsselectorvalues","ctrl":"DDO_GRIDCOLUMNSSELECTOR","prop":"ColumnsSelectorValues"}]""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED",""","oparms":[{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"edtWWPParameterKey_Visible","ctrl":"WWPPARAMETERKEY","prop":"Visible"},{"av":"edtWWPParameterCategory_Visible","ctrl":"WWPPARAMETERCATEGORY","prop":"Visible"},{"av":"edtWWPParameterDescription_Visible","ctrl":"WWPPARAMETERDESCRIPTION","prop":"Visible"},{"av":"edtWWPParameterValueTrimmed_Visible","ctrl":"WWPPARAMETERVALUETRIMMED","prop":"Visible"},{"av":"AV40GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV41GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV8GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV27ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV14GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED","""{"handler":"E11122","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_managefilters_Activeeventkey","ctrl":"DDO_MANAGEFILTERS","prop":"ActiveEventKey"},{"av":"AV14GridState","fld":"vGRIDSTATE"}]""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED",""","oparms":[{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV14GridState","fld":"vGRIDSTATE"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPParameterKey_Visible","ctrl":"WWPPARAMETERKEY","prop":"Visible"},{"av":"edtWWPParameterCategory_Visible","ctrl":"WWPPARAMETERCATEGORY","prop":"Visible"},{"av":"edtWWPParameterDescription_Visible","ctrl":"WWPPARAMETERDESCRIPTION","prop":"Visible"},{"av":"edtWWPParameterValueTrimmed_Visible","ctrl":"WWPPARAMETERVALUETRIMMED","prop":"Visible"},{"av":"AV40GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV41GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV8GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV27ManageFiltersData","fld":"vMANAGEFILTERSDATA"}]}""");
         setEventMetadata("VGRIDACTIONS.CLICK","""{"handler":"E20122","iparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A1WWPParameterKey","fld":"WWPPARAMETERKEY","hsh":true}]""");
         setEventMetadata("VGRIDACTIONS.CLICK",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPParameterKey_Visible","ctrl":"WWPPARAMETERKEY","prop":"Visible"},{"av":"edtWWPParameterCategory_Visible","ctrl":"WWPPARAMETERCATEGORY","prop":"Visible"},{"av":"edtWWPParameterDescription_Visible","ctrl":"WWPPARAMETERDESCRIPTION","prop":"Visible"},{"av":"edtWWPParameterValueTrimmed_Visible","ctrl":"WWPPARAMETERVALUETRIMMED","prop":"Visible"},{"av":"AV40GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV41GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV8GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV27ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV14GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("'DOINSERT'","""{"handler":"E16122","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV16OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV17OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV19FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV55Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV30TFWWPParameterKey","fld":"vTFWWPPARAMETERKEY"},{"av":"AV31TFWWPParameterKey_Sel","fld":"vTFWWPPARAMETERKEY_SEL"},{"av":"AV32TFWWPParameterCategory","fld":"vTFWWPPARAMETERCATEGORY"},{"av":"AV33TFWWPParameterCategory_Sel","fld":"vTFWWPPARAMETERCATEGORY_SEL"},{"av":"AV34TFWWPParameterDescription","fld":"vTFWWPPARAMETERDESCRIPTION"},{"av":"AV35TFWWPParameterDescription_Sel","fld":"vTFWWPPARAMETERDESCRIPTION_SEL"},{"av":"AV36TFWWPParameterValueTrimmed","fld":"vTFWWPPARAMETERVALUETRIMMED"},{"av":"AV37TFWWPParameterValueTrimmed_Sel","fld":"vTFWWPPARAMETERVALUETRIMMED_SEL"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A1WWPParameterKey","fld":"WWPPARAMETERKEY","hsh":true}]""");
         setEventMetadata("'DOINSERT'",""","oparms":[{"av":"AV29ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV24ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtWWPParameterKey_Visible","ctrl":"WWPPARAMETERKEY","prop":"Visible"},{"av":"edtWWPParameterCategory_Visible","ctrl":"WWPPARAMETERCATEGORY","prop":"Visible"},{"av":"edtWWPParameterDescription_Visible","ctrl":"WWPPARAMETERDESCRIPTION","prop":"Visible"},{"av":"edtWWPParameterValueTrimmed_Visible","ctrl":"WWPPARAMETERVALUETRIMMED","prop":"Visible"},{"av":"AV40GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV41GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV8GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV48IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV49IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV27ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV14GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("NULL","""{"handler":"Valid_Wwpparametervaluetrimmed","iparms":[]}""");
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
         Ddo_grid_Selectedcolumn = "";
         Ddo_grid_Filteredtext_get = "";
         Ddo_gridcolumnsselector_Columnsselectorvalues = "";
         Ddo_managefilters_Activeeventkey = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV19FilterFullText = "";
         AV24ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV55Pgmname = "";
         AV30TFWWPParameterKey = "";
         AV31TFWWPParameterKey_Sel = "";
         AV32TFWWPParameterCategory = "";
         AV33TFWWPParameterCategory_Sel = "";
         AV34TFWWPParameterDescription = "";
         AV35TFWWPParameterDescription_Sel = "";
         AV36TFWWPParameterValueTrimmed = "";
         AV37TFWWPParameterValueTrimmed_Sel = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV27ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV8GridAppliedFilters = "";
         AV38DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV14GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         A2WWPParameterValue = "";
         Ddo_grid_Caption = "";
         Ddo_grid_Filteredtext_set = "";
         Ddo_grid_Selectedvalue_set = "";
         Ddo_grid_Gamoauthtoken = "";
         Ddo_grid_Sortedstatus = "";
         Ddo_gridcolumnsselector_Gridinternalname = "";
         Grid_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttBtninsert_Jsonclick = "";
         bttBtneditcolumns_Jsonclick = "";
         ucDdo_managefilters = new GXUserControl();
         Ddo_managefilters_Caption = "";
         GridContainer = new GXWebGrid( context);
         sStyleString = "";
         ucGridpaginationbar = new GXUserControl();
         ucDdo_grid = new GXUserControl();
         ucDdo_gridcolumnsselector = new GXUserControl();
         ucGrid_empowerer = new GXUserControl();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         A1WWPParameterKey = "";
         A3WWPParameterCategory = "";
         A4WWPParameterDescription = "";
         A6WWPParameterValueTrimmed = "";
         lV56Workwithplus_wwp_parameterwwds_1_filterfulltext = "";
         lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = "";
         lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = "";
         lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = "";
         lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = "";
         AV56Workwithplus_wwp_parameterwwds_1_filterfulltext = "";
         AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel = "";
         AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey = "";
         AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel = "";
         AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory = "";
         AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel = "";
         AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription = "";
         AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel = "";
         AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed = "";
         H00122_A5WWPParameterDisableDelete = new bool[] {false} ;
         H00122_A4WWPParameterDescription = new string[] {""} ;
         H00122_A3WWPParameterCategory = new string[] {""} ;
         H00122_A1WWPParameterKey = new string[] {""} ;
         H00122_A2WWPParameterValue = new string[] {""} ;
         H00123_AGRID_nRecordCount = new long[1] ;
         AV11HTTPRequest = new GxHttpRequest( context);
         AV46GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV47GAMErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV6WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV26Session = context.GetSession();
         AV22ColumnsSelectorXML = "";
         GridRow = new GXWebRow();
         GXEncryptionTmp = "";
         AV28ManageFiltersXml = "";
         AV23UserCustomValue = "";
         AV25ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV15GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         GXt_char7 = "";
         GXt_char6 = "";
         GXt_char5 = "";
         GXt_char2 = "";
         AV12TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid_Linesclass = "";
         GXCCtl = "";
         ROClassString = "";
         GridColumn = new GXWebColumn();
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.workwithplus.wwp_parameterww__default(),
            new Object[][] {
                new Object[] {
               H00122_A5WWPParameterDisableDelete, H00122_A4WWPParameterDescription, H00122_A3WWPParameterCategory, H00122_A1WWPParameterKey, H00122_A2WWPParameterValue
               }
               , new Object[] {
               H00123_AGRID_nRecordCount
               }
            }
         );
         AV55Pgmname = "WorkWithPlus.WWP_ParameterWW";
         /* GeneXus formulas. */
         AV55Pgmname = "WorkWithPlus.WWP_ParameterWW";
      }

      private short GRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short AV16OrderedBy ;
      private short AV29ManageFiltersExecutionStep ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV42GridActions ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid_Backcolorstyle ;
      private short subGrid_Sortable ;
      private short nGXWrapped ;
      private short subGrid_Backstyle ;
      private short subGrid_Titlebackstyle ;
      private short subGrid_Allowselection ;
      private short subGrid_Allowhovering ;
      private short subGrid_Allowcollapsing ;
      private short subGrid_Collapsed ;
      private int subGrid_Rows ;
      private int Gridpaginationbar_Rowsperpageselectedvalue ;
      private int nRC_GXsfl_37 ;
      private int nGXsfl_37_idx=1 ;
      private int Gridpaginationbar_Pagestoshow ;
      private int bttBtninsert_Visible ;
      private int edtavFilterfulltext_Enabled ;
      private int subGrid_Islastpage ;
      private int GXPagingFrom2 ;
      private int GXPagingTo2 ;
      private int edtWWPParameterKey_Enabled ;
      private int edtWWPParameterCategory_Enabled ;
      private int edtWWPParameterDescription_Enabled ;
      private int edtWWPParameterValueTrimmed_Enabled ;
      private int edtWWPParameterKey_Visible ;
      private int edtWWPParameterCategory_Visible ;
      private int edtWWPParameterDescription_Visible ;
      private int edtWWPParameterValueTrimmed_Visible ;
      private int AV39PageToGo ;
      private int AV65GXV1 ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long GRID_nFirstRecordOnPage ;
      private long AV40GridCurrentPage ;
      private long AV41GridPageCount ;
      private long GRID_nCurrentRecord ;
      private long GRID_nRecordCount ;
      private string Gridpaginationbar_Selectedpage ;
      private string Ddo_grid_Activeeventkey ;
      private string Ddo_grid_Selectedvalue_get ;
      private string Ddo_grid_Selectedcolumn ;
      private string Ddo_grid_Filteredtext_get ;
      private string Ddo_gridcolumnsselector_Columnsselectorvalues ;
      private string Ddo_managefilters_Activeeventkey ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_37_idx="0001" ;
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
      private string Ddo_grid_Caption ;
      private string Ddo_grid_Filteredtext_set ;
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
      private string Ddo_grid_Includedatalist ;
      private string Ddo_grid_Datalisttype ;
      private string Ddo_grid_Datalistproc ;
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
      private string divTableheader_Internalname ;
      private string divTableheadercontent_Internalname ;
      private string divTableactions_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtninsert_Internalname ;
      private string bttBtninsert_Jsonclick ;
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
      private string Ddo_grid_Internalname ;
      private string Ddo_gridcolumnsselector_Internalname ;
      private string Grid_empowerer_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string cmbavGridactions_Internalname ;
      private string edtWWPParameterKey_Internalname ;
      private string edtWWPParameterCategory_Internalname ;
      private string edtWWPParameterDescription_Internalname ;
      private string edtWWPParameterValueTrimmed_Internalname ;
      private string cmbavGridactions_Class ;
      private string GXEncryptionTmp ;
      private string GXt_char7 ;
      private string GXt_char6 ;
      private string GXt_char5 ;
      private string GXt_char2 ;
      private string sGXsfl_37_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string GXCCtl ;
      private string cmbavGridactions_Jsonclick ;
      private string ROClassString ;
      private string edtWWPParameterKey_Jsonclick ;
      private string edtWWPParameterCategory_Jsonclick ;
      private string edtWWPParameterDescription_Jsonclick ;
      private string edtWWPParameterValueTrimmed_Jsonclick ;
      private string subGrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV17OrderedDsc ;
      private bool AV48IsAuthorized_Update ;
      private bool AV49IsAuthorized_Delete ;
      private bool AV50IsAuthorized_Insert ;
      private bool A5WWPParameterDisableDelete ;
      private bool Gridpaginationbar_Showfirst ;
      private bool Gridpaginationbar_Showprevious ;
      private bool Gridpaginationbar_Shownext ;
      private bool Gridpaginationbar_Showlast ;
      private bool Gridpaginationbar_Rowsperpageselector ;
      private bool Grid_empowerer_Hastitlesettings ;
      private bool Grid_empowerer_Hascolumnsselector ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_37_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool GXt_boolean3 ;
      private string A2WWPParameterValue ;
      private string AV22ColumnsSelectorXML ;
      private string AV28ManageFiltersXml ;
      private string AV23UserCustomValue ;
      private string AV19FilterFullText ;
      private string AV30TFWWPParameterKey ;
      private string AV31TFWWPParameterKey_Sel ;
      private string AV32TFWWPParameterCategory ;
      private string AV33TFWWPParameterCategory_Sel ;
      private string AV34TFWWPParameterDescription ;
      private string AV35TFWWPParameterDescription_Sel ;
      private string AV36TFWWPParameterValueTrimmed ;
      private string AV37TFWWPParameterValueTrimmed_Sel ;
      private string AV8GridAppliedFilters ;
      private string A1WWPParameterKey ;
      private string A3WWPParameterCategory ;
      private string A4WWPParameterDescription ;
      private string A6WWPParameterValueTrimmed ;
      private string lV56Workwithplus_wwp_parameterwwds_1_filterfulltext ;
      private string lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey ;
      private string lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory ;
      private string lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription ;
      private string lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed ;
      private string AV56Workwithplus_wwp_parameterwwds_1_filterfulltext ;
      private string AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel ;
      private string AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey ;
      private string AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel ;
      private string AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory ;
      private string AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel ;
      private string AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription ;
      private string AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel ;
      private string AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed ;
      private IGxSession AV26Session ;
      private GXWebGrid GridContainer ;
      private GXWebRow GridRow ;
      private GXWebColumn GridColumn ;
      private GXUserControl ucDdo_managefilters ;
      private GXUserControl ucGridpaginationbar ;
      private GXUserControl ucDdo_grid ;
      private GXUserControl ucDdo_gridcolumnsselector ;
      private GXUserControl ucGrid_empowerer ;
      private GxHttpRequest AV11HTTPRequest ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavGridactions ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV24ColumnsSelector ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV27ManageFiltersData ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV38DDO_TitleSettingsIcons ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV14GridState ;
      private IDataStoreProvider pr_default ;
      private bool[] H00122_A5WWPParameterDisableDelete ;
      private string[] H00122_A4WWPParameterDescription ;
      private string[] H00122_A3WWPParameterCategory ;
      private string[] H00122_A1WWPParameterKey ;
      private string[] H00122_A2WWPParameterValue ;
      private long[] H00123_AGRID_nRecordCount ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV46GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV47GAMErrors ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV6WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV25ColumnsSelectorAux ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV15GridStateFilterValue ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV12TrnContext ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

   public class wwp_parameterww__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_H00122( IGxContext context ,
                                             string AV56Workwithplus_wwp_parameterwwds_1_filterfulltext ,
                                             string AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel ,
                                             string AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey ,
                                             string AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel ,
                                             string AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory ,
                                             string AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel ,
                                             string AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription ,
                                             string AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel ,
                                             string AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed ,
                                             string A1WWPParameterKey ,
                                             string A3WWPParameterCategory ,
                                             string A4WWPParameterDescription ,
                                             string A2WWPParameterValue ,
                                             short AV16OrderedBy ,
                                             bool AV17OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int8 = new short[14];
         Object[] GXv_Object9 = new Object[2];
         string sSelectString;
         string sFromString;
         string sOrderString;
         sSelectString = " `WWPParameterDisableDelete`, `WWPParameterDescription`, `WWPParameterCategory`, `WWPParameterKey`, `WWPParameterValue`";
         sFromString = " FROM `WWP_Parameter`";
         sOrderString = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( `WWPParameterKey` like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) or ( `WWPParameterCategory` like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) or ( `WWPParameterDescription` like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) or ( ( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE CONCAT(RTRIM(LTRIM(SUBSTRING(`WWPParameterValue`, 1, 27))), '...') END) like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int8[0] = 1;
            GXv_int8[1] = 1;
            GXv_int8[2] = 1;
            GXv_int8[3] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey)) ) )
         {
            AddWhere(sWhereString, "(`WWPParameterKey` like @lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey)");
         }
         else
         {
            GXv_int8[4] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel)) && ! ( StringUtil.StrCmp(AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`WWPParameterKey` = @AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel)");
         }
         else
         {
            GXv_int8[5] = 1;
         }
         if ( StringUtil.StrCmp(AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`WWPParameterKey`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory)) ) )
         {
            AddWhere(sWhereString, "(`WWPParameterCategory` like @lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory)");
         }
         else
         {
            GXv_int8[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel)) && ! ( StringUtil.StrCmp(AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`WWPParameterCategory` = @AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel)");
         }
         else
         {
            GXv_int8[7] = 1;
         }
         if ( StringUtil.StrCmp(AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`WWPParameterCategory`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription)) ) )
         {
            AddWhere(sWhereString, "(`WWPParameterDescription` like @lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription)");
         }
         else
         {
            GXv_int8[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel)) && ! ( StringUtil.StrCmp(AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`WWPParameterDescription` = @AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_s)");
         }
         else
         {
            GXv_int8[9] = 1;
         }
         if ( StringUtil.StrCmp(AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`WWPParameterDescription`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed)) ) )
         {
            AddWhere(sWhereString, "(( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE CONCAT(RTRIM(LTRIM(SUBSTRING(`WWPParameterValue`, 1, 27))), '...') END) like @lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed)");
         }
         else
         {
            GXv_int8[10] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel)) && ! ( StringUtil.StrCmp(AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE CONCAT(RTRIM(LTRIM(SUBSTRING(`WWPParameterValue`, 1, 27))), '...') END) = @AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_)");
         }
         else
         {
            GXv_int8[11] = 1;
         }
         if ( StringUtil.StrCmp(AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE RTRIM(LTRIM(SUBSTR(`WWPParameterValue`, 1, 27))) || '...' END)))=0))");
         }
         if ( ( AV16OrderedBy == 1 ) && ! AV17OrderedDsc )
         {
            sOrderString += " ORDER BY `WWPParameterKey`";
         }
         else if ( ( AV16OrderedBy == 1 ) && ( AV17OrderedDsc ) )
         {
            sOrderString += " ORDER BY `WWPParameterKey` DESC";
         }
         else if ( ( AV16OrderedBy == 2 ) && ! AV17OrderedDsc )
         {
            sOrderString += " ORDER BY `WWPParameterCategory`";
         }
         else if ( ( AV16OrderedBy == 2 ) && ( AV17OrderedDsc ) )
         {
            sOrderString += " ORDER BY `WWPParameterCategory` DESC";
         }
         else if ( ( AV16OrderedBy == 3 ) && ! AV17OrderedDsc )
         {
            sOrderString += " ORDER BY `WWPParameterDescription`";
         }
         else if ( ( AV16OrderedBy == 3 ) && ( AV17OrderedDsc ) )
         {
            sOrderString += " ORDER BY `WWPParameterDescription` DESC";
         }
         else if ( true )
         {
            sOrderString += " ORDER BY `WWPParameterKey`";
         }
         scmdbuf = "SELECT " + sSelectString + sFromString + sWhereString + sOrderString + "" + " LIMIT " + "@GXPagingFrom2" + ", " + "@GXPagingTo2";
         GXv_Object9[0] = scmdbuf;
         GXv_Object9[1] = GXv_int8;
         return GXv_Object9 ;
      }

      protected Object[] conditional_H00123( IGxContext context ,
                                             string AV56Workwithplus_wwp_parameterwwds_1_filterfulltext ,
                                             string AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel ,
                                             string AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey ,
                                             string AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel ,
                                             string AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory ,
                                             string AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel ,
                                             string AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription ,
                                             string AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel ,
                                             string AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed ,
                                             string A1WWPParameterKey ,
                                             string A3WWPParameterCategory ,
                                             string A4WWPParameterDescription ,
                                             string A2WWPParameterValue ,
                                             short AV16OrderedBy ,
                                             bool AV17OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int10 = new short[12];
         Object[] GXv_Object11 = new Object[2];
         scmdbuf = "SELECT COUNT(*) FROM `WWP_Parameter`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( `WWPParameterKey` like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) or ( `WWPParameterCategory` like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) or ( `WWPParameterDescription` like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)) or ( ( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE CONCAT(RTRIM(LTRIM(SUBSTRING(`WWPParameterValue`, 1, 27))), '...') END) like CONCAT('%', @lV56Workwithplus_wwp_parameterwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int10[0] = 1;
            GXv_int10[1] = 1;
            GXv_int10[2] = 1;
            GXv_int10[3] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey)) ) )
         {
            AddWhere(sWhereString, "(`WWPParameterKey` like @lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey)");
         }
         else
         {
            GXv_int10[4] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel)) && ! ( StringUtil.StrCmp(AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`WWPParameterKey` = @AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel)");
         }
         else
         {
            GXv_int10[5] = 1;
         }
         if ( StringUtil.StrCmp(AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`WWPParameterKey`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory)) ) )
         {
            AddWhere(sWhereString, "(`WWPParameterCategory` like @lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory)");
         }
         else
         {
            GXv_int10[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel)) && ! ( StringUtil.StrCmp(AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`WWPParameterCategory` = @AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel)");
         }
         else
         {
            GXv_int10[7] = 1;
         }
         if ( StringUtil.StrCmp(AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`WWPParameterCategory`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription)) ) )
         {
            AddWhere(sWhereString, "(`WWPParameterDescription` like @lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription)");
         }
         else
         {
            GXv_int10[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel)) && ! ( StringUtil.StrCmp(AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`WWPParameterDescription` = @AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_s)");
         }
         else
         {
            GXv_int10[9] = 1;
         }
         if ( StringUtil.StrCmp(AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`WWPParameterDescription`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed)) ) )
         {
            AddWhere(sWhereString, "(( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE CONCAT(RTRIM(LTRIM(SUBSTRING(`WWPParameterValue`, 1, 27))), '...') END) like @lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed)");
         }
         else
         {
            GXv_int10[10] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel)) && ! ( StringUtil.StrCmp(AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE CONCAT(RTRIM(LTRIM(SUBSTRING(`WWPParameterValue`, 1, 27))), '...') END) = @AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_)");
         }
         else
         {
            GXv_int10[11] = 1;
         }
         if ( StringUtil.StrCmp(AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(( CASE  WHEN LENGTH(RTRIM(`WWPParameterValue`)) <= 30 THEN `WWPParameterValue` ELSE RTRIM(LTRIM(SUBSTR(`WWPParameterValue`, 1, 27))) || '...' END)))=0))");
         }
         scmdbuf += sWhereString;
         if ( ( AV16OrderedBy == 1 ) && ! AV17OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV16OrderedBy == 1 ) && ( AV17OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV16OrderedBy == 2 ) && ! AV17OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV16OrderedBy == 2 ) && ( AV17OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV16OrderedBy == 3 ) && ! AV17OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV16OrderedBy == 3 ) && ( AV17OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( true )
         {
            scmdbuf += "";
         }
         GXv_Object11[0] = scmdbuf;
         GXv_Object11[1] = GXv_int10;
         return GXv_Object11 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_H00122(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (short)dynConstraints[13] , (bool)dynConstraints[14] );
               case 1 :
                     return conditional_H00123(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (string)dynConstraints[7] , (string)dynConstraints[8] , (string)dynConstraints[9] , (string)dynConstraints[10] , (string)dynConstraints[11] , (string)dynConstraints[12] , (short)dynConstraints[13] , (bool)dynConstraints[14] );
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
          Object[] prmH00122;
          prmH00122 = new Object[] {
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey",GXType.Char,200,0) ,
          new ParDef("@AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel",GXType.Char,200,0) ,
          new ParDef("@lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory",GXType.Char,200,0) ,
          new ParDef("@AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel",GXType.Char,200,0) ,
          new ParDef("@lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription",GXType.Char,200,0) ,
          new ParDef("@AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_s",GXType.Char,200,0) ,
          new ParDef("@lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed",GXType.Char,30,0) ,
          new ParDef("@AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_",GXType.Char,30,0) ,
          new ParDef("@GXPagingFrom2",GXType.Int32,9,0) ,
          new ParDef("@GXPagingTo2",GXType.Int32,9,0)
          };
          Object[] prmH00123;
          prmH00123 = new Object[] {
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Workwithplus_wwp_parameterwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV57Workwithplus_wwp_parameterwwds_2_tfwwpparameterkey",GXType.Char,200,0) ,
          new ParDef("@AV58Workwithplus_wwp_parameterwwds_3_tfwwpparameterkey_sel",GXType.Char,200,0) ,
          new ParDef("@lV59Workwithplus_wwp_parameterwwds_4_tfwwpparametercategory",GXType.Char,200,0) ,
          new ParDef("@AV60Workwithplus_wwp_parameterwwds_5_tfwwpparametercategory_sel",GXType.Char,200,0) ,
          new ParDef("@lV61Workwithplus_wwp_parameterwwds_6_tfwwpparameterdescription",GXType.Char,200,0) ,
          new ParDef("@AV62Workwithplus_wwp_parameterwwds_7_tfwwpparameterdescription_s",GXType.Char,200,0) ,
          new ParDef("@lV63Workwithplus_wwp_parameterwwds_8_tfwwpparametervaluetrimmed",GXType.Char,30,0) ,
          new ParDef("@AV64Workwithplus_wwp_parameterwwds_9_tfwwpparametervaluetrimmed_",GXType.Char,30,0)
          };
          def= new CursorDef[] {
              new CursorDef("H00122", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00122,11, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("H00123", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH00123,1, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[1])[0] = rslt.getVarchar(2);
                ((string[]) buf[2])[0] = rslt.getVarchar(3);
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
                return;
             case 1 :
                ((long[]) buf[0])[0] = rslt.getLong(1);
                return;
       }
    }

 }

}
