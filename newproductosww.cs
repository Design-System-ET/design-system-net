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
   public class newproductosww : GXDataArea
   {
      public newproductosww( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newproductosww( IGxContext context )
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
         AV57Pgmname = GetPar( "Pgmname");
         AV55TFNewProductosId = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosId"), "."), 18, MidpointRounding.ToEven));
         AV56TFNewProductosId_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosId_To"), "."), 18, MidpointRounding.ToEven));
         AV27TFNewProductosNombre = GetPar( "TFNewProductosNombre");
         AV28TFNewProductosNombre_Sel = GetPar( "TFNewProductosNombre_Sel");
         AV31TFNewProductosNumeroDescargas = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosNumeroDescargas"), "."), 18, MidpointRounding.ToEven));
         AV32TFNewProductosNumeroDescargas_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosNumeroDescargas_To"), "."), 18, MidpointRounding.ToEven));
         AV51TFNewProductosNumeroVentas = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosNumeroVentas"), "."), 18, MidpointRounding.ToEven));
         AV52TFNewProductosNumeroVentas_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosNumeroVentas_To"), "."), 18, MidpointRounding.ToEven));
         AV53TFNewProductosVisitas = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosVisitas"), "."), 18, MidpointRounding.ToEven));
         AV54TFNewProductosVisitas_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFNewProductosVisitas_To"), "."), 18, MidpointRounding.ToEven));
         AV45IsAuthorized_Display = StringUtil.StrToBool( GetPar( "IsAuthorized_Display"));
         AV46IsAuthorized_Update = StringUtil.StrToBool( GetPar( "IsAuthorized_Update"));
         AV47IsAuthorized_Delete = StringUtil.StrToBool( GetPar( "IsAuthorized_Delete"));
         AV50IsAuthorized_Insert = StringUtil.StrToBool( GetPar( "IsAuthorized_Insert"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV57Pgmname, AV55TFNewProductosId, AV56TFNewProductosId_To, AV27TFNewProductosNombre, AV28TFNewProductosNombre_Sel, AV31TFNewProductosNumeroDescargas, AV32TFNewProductosNumeroDescargas_To, AV51TFNewProductosNumeroVentas, AV52TFNewProductosNumeroVentas_To, AV53TFNewProductosVisitas, AV54TFNewProductosVisitas_To, AV45IsAuthorized_Display, AV46IsAuthorized_Update, AV47IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
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
            return "newproductosww_Execute" ;
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
         PA2W2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START2W2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("newproductosww.aspx") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV57Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV57Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV45IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV45IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV46IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV46IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV47IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV47IsAuthorized_Delete, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV50IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV50IsAuthorized_Insert, context));
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
         GxWebStd.gx_hidden_field( context, "vGRIDCURRENTPAGE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV41GridCurrentPage), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV42GridPageCount), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDAPPLIEDFILTERS", AV43GridAppliedFilters);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vAGEXPORTDATA", AV48AGExportData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vAGEXPORTDATA", AV48AGExportData);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV37DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV37DDO_TitleSettingsIcons);
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
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV57Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV57Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV55TFNewProductosId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSID_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV56TFNewProductosId_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSNOMBRE", AV27TFNewProductosNombre);
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSNOMBRE_SEL", AV28TFNewProductosNombre_Sel);
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSNUMERODESCARGAS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV31TFNewProductosNumeroDescargas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSNUMERODESCARGAS_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV32TFNewProductosNumeroDescargas_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSNUMEROVENTAS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV51TFNewProductosNumeroVentas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSNUMEROVENTAS_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV52TFNewProductosNumeroVentas_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSVISITAS", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV53TFNewProductosVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFNEWPRODUCTOSVISITAS_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV54TFNewProductosVisitas_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, "vORDEREDDSC", AV14OrderedDsc);
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV45IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV45IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV46IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV46IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV47IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV47IsAuthorized_Delete, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGRIDSTATE", AV11GridState);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGRIDSTATE", AV11GridState);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV50IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV50IsAuthorized_Insert, context));
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
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtextto_get", StringUtil.RTrim( Ddo_grid_Filteredtextto_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "DDO_AGEXPORT_Activeeventkey", StringUtil.RTrim( Ddo_agexport_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Activeeventkey", StringUtil.RTrim( Ddo_grid_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedvalue_get", StringUtil.RTrim( Ddo_grid_Selectedvalue_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Selectedcolumn", StringUtil.RTrim( Ddo_grid_Selectedcolumn));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtext_get", StringUtil.RTrim( Ddo_grid_Filteredtext_get));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Filteredtextto_get", StringUtil.RTrim( Ddo_grid_Filteredtextto_get));
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
            WE2W2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT2W2( ) ;
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
         return formatLink("newproductosww.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "NewProductosWW" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( " New Productos", "") ;
      }

      protected void WB2W0( )
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
            GxWebStd.gx_button_ctrl( context, bttBtninsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "GXM_insert", ""), bttBtninsert_Jsonclick, 5, context.GetMessage( "GXM_insert", ""), "", StyleString, ClassString, bttBtninsert_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOINSERT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewProductosWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'',false,'',0)\"";
            ClassString = "ColumnsSelector";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnagexport_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "WWP_ExportData", ""), bttBtnagexport_Jsonclick, 0, context.GetMessage( "WWP_ExportData", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_NewProductosWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
            ClassString = "hidden-xs";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtneditcolumns_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "WWP_EditColumnsCaption", ""), bttBtneditcolumns_Jsonclick, 0, context.GetMessage( "WWP_EditColumnsTooltip", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_NewProductosWW.htm");
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
            GxWebStd.gx_single_line_edit( context, edtavFilterfulltext_Internalname, AV16FilterFullText, StringUtil.RTrim( context.localUtil.Format( AV16FilterFullText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,35);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "WWP_Search", ""), edtavFilterfulltext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFilterfulltext_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "WWPFullTextFilter", "start", true, "", "HLP_NewProductosWW.htm");
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
            ucGridpaginationbar.SetProperty("CurrentPage", AV41GridCurrentPage);
            ucGridpaginationbar.SetProperty("PageCount", AV42GridPageCount);
            ucGridpaginationbar.SetProperty("AppliedFilters", AV43GridAppliedFilters);
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
            ucDdo_agexport.SetProperty("DropDownOptionsData", AV48AGExportData);
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
            ucDdo_grid.SetProperty("DataListProc", Ddo_grid_Datalistproc);
            ucDdo_grid.SetProperty("Format", Ddo_grid_Format);
            ucDdo_grid.SetProperty("DropDownOptionsTitleSettingsIcons", AV37DDO_TitleSettingsIcons);
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
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsTitleSettingsIcons", AV37DDO_TitleSettingsIcons);
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

      protected void START2W2( )
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
         Form.Meta.addItem("description", context.GetMessage( " New Productos", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2W0( ) ;
      }

      protected void WS2W2( )
      {
         START2W2( ) ;
         EVT2W2( ) ;
      }

      protected void EVT2W2( )
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
                              E112W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changepage */
                              E122W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changerowsperpage */
                              E132W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_AGEXPORT.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_agexport.Onoptionclicked */
                              E142W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRID.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_grid.Onoptionclicked */
                              E152W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_gridcolumnsselector.Oncolumnschanged */
                              E162W2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOINSERT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoInsert' */
                              E172W2 ();
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
                              AV44GridActions = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavGridactions_Internalname), "."), 18, MidpointRounding.ToEven));
                              AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV44GridActions), 4, 0));
                              A34NewProductosId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewProductosId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A35NewProductosImagen = cgiGet( edtNewProductosImagen_Internalname);
                              AssignProp("", false, edtNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), !bGXsfl_44_Refreshing);
                              AssignProp("", false, edtNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
                              A36NewProductosNombre = cgiGet( edtNewProductosNombre_Internalname);
                              A39NewProductosNumeroDescargas = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewProductosNumeroDescargas_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A42NewProductosNumeroVentas = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewProductosNumeroVentas_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A43NewProductosVisitas = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewProductosVisitas_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E182W2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E192W2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid.Load */
                                    E202W2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VGRIDACTIONS.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E212W2 ();
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

      protected void WE2W2( )
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

      protected void PA2W2( )
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
                                       string AV57Pgmname ,
                                       short AV55TFNewProductosId ,
                                       short AV56TFNewProductosId_To ,
                                       string AV27TFNewProductosNombre ,
                                       string AV28TFNewProductosNombre_Sel ,
                                       short AV31TFNewProductosNumeroDescargas ,
                                       short AV32TFNewProductosNumeroDescargas_To ,
                                       short AV51TFNewProductosNumeroVentas ,
                                       short AV52TFNewProductosNumeroVentas_To ,
                                       short AV53TFNewProductosVisitas ,
                                       short AV54TFNewProductosVisitas_To ,
                                       bool AV45IsAuthorized_Display ,
                                       bool AV46IsAuthorized_Update ,
                                       bool AV47IsAuthorized_Delete ,
                                       bool AV50IsAuthorized_Insert )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RF2W2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_NEWPRODUCTOSID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSID", StringUtil.LTrim( StringUtil.NToC( (decimal)(A34NewProductosId), 4, 0, ".", "")));
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
         RF2W2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV57Pgmname = "NewProductosWW";
      }

      protected void RF2W2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 44;
         /* Execute user event: Refresh */
         E192W2 ();
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
                                                 AV58Newproductoswwds_1_filterfulltext ,
                                                 AV59Newproductoswwds_2_tfnewproductosid ,
                                                 AV60Newproductoswwds_3_tfnewproductosid_to ,
                                                 AV62Newproductoswwds_5_tfnewproductosnombre_sel ,
                                                 AV61Newproductoswwds_4_tfnewproductosnombre ,
                                                 AV63Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                                 AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                                 AV65Newproductoswwds_8_tfnewproductosnumeroventas ,
                                                 AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                                 AV67Newproductoswwds_10_tfnewproductosvisitas ,
                                                 AV68Newproductoswwds_11_tfnewproductosvisitas_to ,
                                                 A34NewProductosId ,
                                                 A36NewProductosNombre ,
                                                 A39NewProductosNumeroDescargas ,
                                                 A42NewProductosNumeroVentas ,
                                                 A43NewProductosVisitas ,
                                                 AV13OrderedBy ,
                                                 AV14OrderedDsc } ,
                                                 new int[]{
                                                 TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT,
                                                 TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                                 }
            });
            lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
            lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
            lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
            lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
            lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
            lV61Newproductoswwds_4_tfnewproductosnombre = StringUtil.Concat( StringUtil.RTrim( AV61Newproductoswwds_4_tfnewproductosnombre), "%", "");
            /* Using cursor H002W2 */
            pr_default.execute(0, new Object[] {lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, AV59Newproductoswwds_2_tfnewproductosid, AV60Newproductoswwds_3_tfnewproductosid_to, lV61Newproductoswwds_4_tfnewproductosnombre, AV62Newproductoswwds_5_tfnewproductosnombre_sel, AV63Newproductoswwds_6_tfnewproductosnumerodescargas, AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to, AV65Newproductoswwds_8_tfnewproductosnumeroventas, AV66Newproductoswwds_9_tfnewproductosnumeroventas_to, AV67Newproductoswwds_10_tfnewproductosvisitas, AV68Newproductoswwds_11_tfnewproductosvisitas_to, GXPagingFrom2, GXPagingTo2});
            nGXsfl_44_idx = 1;
            sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
            SubsflControlProps_442( ) ;
            while ( ( (pr_default.getStatus(0) != 101) ) && ( ( ( subGrid_Rows == 0 ) || ( GRID_nCurrentRecord < subGrid_fnc_Recordsperpage( ) ) ) ) )
            {
               A43NewProductosVisitas = H002W2_A43NewProductosVisitas[0];
               A42NewProductosNumeroVentas = H002W2_A42NewProductosNumeroVentas[0];
               A39NewProductosNumeroDescargas = H002W2_A39NewProductosNumeroDescargas[0];
               A36NewProductosNombre = H002W2_A36NewProductosNombre[0];
               A40000NewProductosImagen_GXI = H002W2_A40000NewProductosImagen_GXI[0];
               AssignProp("", false, edtNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), !bGXsfl_44_Refreshing);
               AssignProp("", false, edtNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
               A34NewProductosId = H002W2_A34NewProductosId[0];
               A35NewProductosImagen = H002W2_A35NewProductosImagen[0];
               AssignProp("", false, edtNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), !bGXsfl_44_Refreshing);
               AssignProp("", false, edtNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
               /* Execute user event: Grid.Load */
               E202W2 ();
               pr_default.readNext(0);
            }
            GRID_nEOF = (short)(((pr_default.getStatus(0) == 101) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            pr_default.close(0);
            wbEnd = 44;
            WB2W0( ) ;
         }
         bGXsfl_44_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2W2( )
      {
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV57Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV57Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV45IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV45IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV46IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV46IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV47IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV47IsAuthorized_Delete, context));
         GxWebStd.gx_hidden_field( context, "gxhash_NEWPRODUCTOSID"+"_"+sGXsfl_44_idx, GetSecureSignedToken( sGXsfl_44_idx, context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9"), context));
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
         AV58Newproductoswwds_1_filterfulltext = AV16FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV55TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV56TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV27TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV28TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV31TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV32TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV51TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV52TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV53TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV54TFNewProductosVisitas_To;
         pr_default.dynParam(1, new Object[]{ new Object[]{
                                              AV58Newproductoswwds_1_filterfulltext ,
                                              AV59Newproductoswwds_2_tfnewproductosid ,
                                              AV60Newproductoswwds_3_tfnewproductosid_to ,
                                              AV62Newproductoswwds_5_tfnewproductosnombre_sel ,
                                              AV61Newproductoswwds_4_tfnewproductosnombre ,
                                              AV63Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                              AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                              AV65Newproductoswwds_8_tfnewproductosnumeroventas ,
                                              AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                              AV67Newproductoswwds_10_tfnewproductosvisitas ,
                                              AV68Newproductoswwds_11_tfnewproductosvisitas_to ,
                                              A34NewProductosId ,
                                              A36NewProductosNombre ,
                                              A39NewProductosNumeroDescargas ,
                                              A42NewProductosNumeroVentas ,
                                              A43NewProductosVisitas ,
                                              AV13OrderedBy ,
                                              AV14OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT,
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV61Newproductoswwds_4_tfnewproductosnombre = StringUtil.Concat( StringUtil.RTrim( AV61Newproductoswwds_4_tfnewproductosnombre), "%", "");
         /* Using cursor H002W3 */
         pr_default.execute(1, new Object[] {lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, AV59Newproductoswwds_2_tfnewproductosid, AV60Newproductoswwds_3_tfnewproductosid_to, lV61Newproductoswwds_4_tfnewproductosnombre, AV62Newproductoswwds_5_tfnewproductosnombre_sel, AV63Newproductoswwds_6_tfnewproductosnumerodescargas, AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to, AV65Newproductoswwds_8_tfnewproductosnumeroventas, AV66Newproductoswwds_9_tfnewproductosnumeroventas_to, AV67Newproductoswwds_10_tfnewproductosvisitas, AV68Newproductoswwds_11_tfnewproductosvisitas_to});
         GRID_nRecordCount = H002W3_AGRID_nRecordCount[0];
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
         AV58Newproductoswwds_1_filterfulltext = AV16FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV55TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV56TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV27TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV28TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV31TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV32TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV51TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV52TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV53TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV54TFNewProductosVisitas_To;
         GRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV57Pgmname, AV55TFNewProductosId, AV56TFNewProductosId_To, AV27TFNewProductosNombre, AV28TFNewProductosNombre_Sel, AV31TFNewProductosNumeroDescargas, AV32TFNewProductosNumeroDescargas_To, AV51TFNewProductosNumeroVentas, AV52TFNewProductosNumeroVentas_To, AV53TFNewProductosVisitas, AV54TFNewProductosVisitas_To, AV45IsAuthorized_Display, AV46IsAuthorized_Update, AV47IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_nextpage( )
      {
         AV58Newproductoswwds_1_filterfulltext = AV16FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV55TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV56TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV27TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV28TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV31TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV32TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV51TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV52TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV53TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV54TFNewProductosVisitas_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV57Pgmname, AV55TFNewProductosId, AV56TFNewProductosId_To, AV27TFNewProductosNombre, AV28TFNewProductosNombre_Sel, AV31TFNewProductosNumeroDescargas, AV32TFNewProductosNumeroDescargas_To, AV51TFNewProductosNumeroVentas, AV52TFNewProductosNumeroVentas_To, AV53TFNewProductosVisitas, AV54TFNewProductosVisitas_To, AV45IsAuthorized_Display, AV46IsAuthorized_Update, AV47IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgrid_previouspage( )
      {
         AV58Newproductoswwds_1_filterfulltext = AV16FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV55TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV56TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV27TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV28TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV31TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV32TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV51TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV52TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV53TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV54TFNewProductosVisitas_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV57Pgmname, AV55TFNewProductosId, AV56TFNewProductosId_To, AV27TFNewProductosNombre, AV28TFNewProductosNombre_Sel, AV31TFNewProductosNumeroDescargas, AV32TFNewProductosNumeroDescargas_To, AV51TFNewProductosNumeroVentas, AV52TFNewProductosNumeroVentas_To, AV53TFNewProductosVisitas, AV54TFNewProductosVisitas_To, AV45IsAuthorized_Display, AV46IsAuthorized_Update, AV47IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         AV58Newproductoswwds_1_filterfulltext = AV16FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV55TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV56TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV27TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV28TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV31TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV32TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV51TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV52TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV53TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV54TFNewProductosVisitas_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV57Pgmname, AV55TFNewProductosId, AV56TFNewProductosId_To, AV27TFNewProductosNombre, AV28TFNewProductosNombre_Sel, AV31TFNewProductosNumeroDescargas, AV32TFNewProductosNumeroDescargas_To, AV51TFNewProductosNumeroVentas, AV52TFNewProductosNumeroVentas_To, AV53TFNewProductosVisitas, AV54TFNewProductosVisitas_To, AV45IsAuthorized_Display, AV46IsAuthorized_Update, AV47IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgrid_gotopage( int nPageNo )
      {
         AV58Newproductoswwds_1_filterfulltext = AV16FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV55TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV56TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV27TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV28TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV31TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV32TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV51TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV52TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV53TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV54TFNewProductosVisitas_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV57Pgmname, AV55TFNewProductosId, AV56TFNewProductosId_To, AV27TFNewProductosNombre, AV28TFNewProductosNombre_Sel, AV31TFNewProductosNumeroDescargas, AV32TFNewProductosNumeroDescargas_To, AV51TFNewProductosNumeroVentas, AV52TFNewProductosNumeroVentas_To, AV53TFNewProductosVisitas, AV54TFNewProductosVisitas_To, AV45IsAuthorized_Display, AV46IsAuthorized_Update, AV47IsAuthorized_Delete, AV50IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV57Pgmname = "NewProductosWW";
         edtNewProductosId_Enabled = 0;
         edtNewProductosImagen_Enabled = 0;
         edtNewProductosNombre_Enabled = 0;
         edtNewProductosNumeroDescargas_Enabled = 0;
         edtNewProductosNumeroVentas_Enabled = 0;
         edtNewProductosVisitas_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2W0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E182W2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vMANAGEFILTERSDATA"), AV24ManageFiltersData);
            ajax_req_read_hidden_sdt(cgiGet( "vAGEXPORTDATA"), AV48AGExportData);
            ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV37DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( "vCOLUMNSSELECTOR"), AV21ColumnsSelector);
            /* Read saved values. */
            nRC_GXsfl_44 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_44"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV41GridCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDCURRENTPAGE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV42GridPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDPAGECOUNT"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV43GridAppliedFilters = cgiGet( "vGRIDAPPLIEDFILTERS");
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
            Ddo_grid_Selectedcolumn = cgiGet( "DDO_GRID_Selectedcolumn");
            Ddo_grid_Filteredtext_get = cgiGet( "DDO_GRID_Filteredtext_get");
            Ddo_grid_Filteredtextto_get = cgiGet( "DDO_GRID_Filteredtextto_get");
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
         E182W2 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
      }

      protected void E182W2( )
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
         AV48AGExportData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV49AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV49AGExportDataItem.gxTpr_Title = context.GetMessage( "WWP_ExportCaption", "");
         AV49AGExportDataItem.gxTpr_Icon = context.convertURL( (string)(context.GetImagePath( "da69a816-fd11-445b-8aaf-1a2f7f1acc93", "", context.GetTheme( ))));
         AV49AGExportDataItem.gxTpr_Eventkey = "Export";
         AV49AGExportDataItem.gxTpr_Isdivider = false;
         AV48AGExportData.Add(AV49AGExportDataItem, 0);
         AV49AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV49AGExportDataItem.gxTpr_Title = context.GetMessage( "WWP_ExportReportCaption", "");
         AV49AGExportDataItem.gxTpr_Icon = context.convertURL( (string)(context.GetImagePath( "776fb79c-a0a1-4302-b5e5-d773dbe1a297", "", context.GetTheme( ))));
         AV49AGExportDataItem.gxTpr_Eventkey = "ExportReport";
         AV49AGExportDataItem.gxTpr_Isdivider = false;
         AV48AGExportData.Add(AV49AGExportDataItem, 0);
         AV38GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV39GAMErrors);
         Ddo_grid_Gridinternalname = subGrid_Internalname;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GridInternalName", Ddo_grid_Gridinternalname);
         Ddo_grid_Gamoauthtoken = AV38GAMSession.gxTpr_Token;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GAMOAuthToken", Ddo_grid_Gamoauthtoken);
         Form.Caption = context.GetMessage( " New Productos", "");
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
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = AV37DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1) ;
         AV37DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1;
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = bttBtneditcolumns_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, "", false, Ddo_gridcolumnsselector_Internalname, "TitleControlIdToReplace", Ddo_gridcolumnsselector_Titlecontrolidtoreplace);
         Gridpaginationbar_Rowsperpageselectedvalue = subGrid_Rows;
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0));
      }

      protected void E192W2( )
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
         if ( StringUtil.StrCmp(AV23Session.Get("NewProductosWWColumnsSelector"), "") != 0 )
         {
            AV19ColumnsSelectorXML = AV23Session.Get("NewProductosWWColumnsSelector");
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
         edtNewProductosId_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(1)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewProductosId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewProductosId_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewProductosImagen_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(2)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewProductosImagen_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewProductosImagen_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewProductosNombre_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(3)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewProductosNombre_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewProductosNombre_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewProductosNumeroDescargas_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(4)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewProductosNumeroDescargas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewProductosNumeroDescargas_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewProductosNumeroVentas_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(5)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewProductosNumeroVentas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewProductosNumeroVentas_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtNewProductosVisitas_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(6)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtNewProductosVisitas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewProductosVisitas_Visible), 5, 0), !bGXsfl_44_Refreshing);
         AV41GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri("", false, "AV41GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV41GridCurrentPage), 10, 0));
         AV42GridPageCount = subGrid_fnc_Pagecount( );
         AssignAttri("", false, "AV42GridPageCount", StringUtil.LTrimStr( (decimal)(AV42GridPageCount), 10, 0));
         GXt_char2 = AV43GridAppliedFilters;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getappliedfiltersdescription(context ).execute(  AV57Pgmname, out  GXt_char2) ;
         AV43GridAppliedFilters = GXt_char2;
         AssignAttri("", false, "AV43GridAppliedFilters", AV43GridAppliedFilters);
         AV58Newproductoswwds_1_filterfulltext = AV16FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV55TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV56TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV27TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV28TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV31TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV32TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV51TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV52TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV53TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV54TFNewProductosVisitas_To;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E122W2( )
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
            AV40PageToGo = (int)(Math.Round(NumberUtil.Val( Gridpaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            subgrid_gotopage( AV40PageToGo) ;
         }
      }

      protected void E132W2( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      protected void E152W2( )
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
            if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewProductosId") == 0 )
            {
               AV55TFNewProductosId = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV55TFNewProductosId", StringUtil.LTrimStr( (decimal)(AV55TFNewProductosId), 4, 0));
               AV56TFNewProductosId_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV56TFNewProductosId_To", StringUtil.LTrimStr( (decimal)(AV56TFNewProductosId_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewProductosNombre") == 0 )
            {
               AV27TFNewProductosNombre = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV27TFNewProductosNombre", AV27TFNewProductosNombre);
               AV28TFNewProductosNombre_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV28TFNewProductosNombre_Sel", AV28TFNewProductosNombre_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewProductosNumeroDescargas") == 0 )
            {
               AV31TFNewProductosNumeroDescargas = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV31TFNewProductosNumeroDescargas", StringUtil.LTrimStr( (decimal)(AV31TFNewProductosNumeroDescargas), 4, 0));
               AV32TFNewProductosNumeroDescargas_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV32TFNewProductosNumeroDescargas_To", StringUtil.LTrimStr( (decimal)(AV32TFNewProductosNumeroDescargas_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewProductosNumeroVentas") == 0 )
            {
               AV51TFNewProductosNumeroVentas = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV51TFNewProductosNumeroVentas", StringUtil.LTrimStr( (decimal)(AV51TFNewProductosNumeroVentas), 4, 0));
               AV52TFNewProductosNumeroVentas_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV52TFNewProductosNumeroVentas_To", StringUtil.LTrimStr( (decimal)(AV52TFNewProductosNumeroVentas_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "NewProductosVisitas") == 0 )
            {
               AV53TFNewProductosVisitas = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV53TFNewProductosVisitas", StringUtil.LTrimStr( (decimal)(AV53TFNewProductosVisitas), 4, 0));
               AV54TFNewProductosVisitas_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV54TFNewProductosVisitas_To", StringUtil.LTrimStr( (decimal)(AV54TFNewProductosVisitas_To), 4, 0));
            }
            subgrid_firstpage( ) ;
         }
         /*  Sending Event outputs  */
      }

      private void E202W2( )
      {
         /* Grid_Load Routine */
         returnInSub = false;
         cmbavGridactions.removeAllItems();
         cmbavGridactions.addItem("0", ";fa fa-bars", 0);
         if ( AV45IsAuthorized_Display )
         {
            cmbavGridactions.addItem("1", StringUtil.Format( "%1;%2", context.GetMessage( "GXM_display", ""), "fa fa-search", "", "", "", "", "", "", ""), 0);
         }
         if ( AV46IsAuthorized_Update )
         {
            cmbavGridactions.addItem("2", StringUtil.Format( "%1;%2", context.GetMessage( "GXM_update", ""), "fa fa-pen", "", "", "", "", "", "", ""), 0);
         }
         if ( AV47IsAuthorized_Delete )
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
         cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV44GridActions), 4, 0));
      }

      protected void E162W2( )
      {
         /* Ddo_gridcolumnsselector_Oncolumnschanged Routine */
         returnInSub = false;
         AV19ColumnsSelectorXML = Ddo_gridcolumnsselector_Columnsselectorvalues;
         AV21ColumnsSelector.FromJSonString(AV19ColumnsSelectorXML, null);
         new DesignSystem.Programs.wwpbaseobjects.savecolumnsselectorstate(context ).execute(  "NewProductosWWColumnsSelector",  (String.IsNullOrEmpty(StringUtil.RTrim( AV19ColumnsSelectorXML)) ? "" : AV21ColumnsSelector.ToXml(false, true, "", ""))) ;
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E112W2( )
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
            GXEncryptionTmp = "wwpbaseobjects.savefilteras.aspx"+UrlEncode(StringUtil.RTrim("NewProductosWWFilters")) + "," + UrlEncode(StringUtil.RTrim(AV57Pgmname+"GridState"));
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
            GXEncryptionTmp = "wwpbaseobjects.managefilters.aspx"+UrlEncode(StringUtil.RTrim("NewProductosWWFilters"));
            context.PopUp(formatLink("wwpbaseobjects.managefilters.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV26ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV26ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV26ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else
         {
            GXt_char2 = AV25ManageFiltersXml;
            new DesignSystem.Programs.wwpbaseobjects.getfilterbyname(context ).execute(  "NewProductosWWFilters",  Ddo_managefilters_Activeeventkey, out  GXt_char2) ;
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
               new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV57Pgmname+"GridState",  AV25ManageFiltersXml) ;
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

      protected void E212W2( )
      {
         /* Gridactions_Click Routine */
         returnInSub = false;
         if ( AV44GridActions == 1 )
         {
            /* Execute user subroutine: 'DO DISPLAY' */
            S202 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV44GridActions == 2 )
         {
            /* Execute user subroutine: 'DO UPDATE' */
            S212 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         else if ( AV44GridActions == 3 )
         {
            /* Execute user subroutine: 'DO DELETE' */
            S222 ();
            if ( returnInSub )
            {
               returnInSub = true;
               if (true) return;
            }
         }
         AV44GridActions = 0;
         AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV44GridActions), 4, 0));
         /*  Sending Event outputs  */
         cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV44GridActions), 4, 0));
         AssignProp("", false, cmbavGridactions_Internalname, "Values", cmbavGridactions.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E172W2( )
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
            GXEncryptionTmp = "newproductos.aspx"+UrlEncode(StringUtil.RTrim("INS")) + "," + UrlEncode(StringUtil.LTrimStr(0,1,0));
            CallWebObject(formatLink("newproductos.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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

      protected void E142W2( )
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
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewProductosId",  "",  "Id",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewProductosImagen",  "",  "Imagen",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewProductosNombre",  "",  "Nombre",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewProductosNumeroDescargas",  "",  "Descargas",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewProductosNumeroVentas",  "",  "Ventas",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "NewProductosVisitas",  "",  "Visitas",  true,  "") ;
         GXt_char2 = AV20UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "NewProductosWWColumnsSelector", out  GXt_char2) ;
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
         GXt_boolean3 = AV45IsAuthorized_Display;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newproductos_Execute", out  GXt_boolean3) ;
         AV45IsAuthorized_Display = GXt_boolean3;
         AssignAttri("", false, "AV45IsAuthorized_Display", AV45IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV45IsAuthorized_Display, context));
         GXt_boolean3 = AV46IsAuthorized_Update;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newproductos_Update", out  GXt_boolean3) ;
         AV46IsAuthorized_Update = GXt_boolean3;
         AssignAttri("", false, "AV46IsAuthorized_Update", AV46IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV46IsAuthorized_Update, context));
         GXt_boolean3 = AV47IsAuthorized_Delete;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newproductos_Delete", out  GXt_boolean3) ;
         AV47IsAuthorized_Delete = GXt_boolean3;
         AssignAttri("", false, "AV47IsAuthorized_Delete", AV47IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV47IsAuthorized_Delete, context));
         GXt_boolean3 = AV50IsAuthorized_Insert;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "newproductos_Insert", out  GXt_boolean3) ;
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
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 = AV24ManageFiltersData;
         new DesignSystem.Programs.wwpbaseobjects.wwp_managefiltersloadsavedfilters(context ).execute(  "NewProductosWWFilters",  "",  "",  false, out  GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4) ;
         AV24ManageFiltersData = GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4;
      }

      protected void S182( )
      {
         /* 'CLEANFILTERS' Routine */
         returnInSub = false;
         AV16FilterFullText = "";
         AssignAttri("", false, "AV16FilterFullText", AV16FilterFullText);
         AV55TFNewProductosId = 0;
         AssignAttri("", false, "AV55TFNewProductosId", StringUtil.LTrimStr( (decimal)(AV55TFNewProductosId), 4, 0));
         AV56TFNewProductosId_To = 0;
         AssignAttri("", false, "AV56TFNewProductosId_To", StringUtil.LTrimStr( (decimal)(AV56TFNewProductosId_To), 4, 0));
         AV27TFNewProductosNombre = "";
         AssignAttri("", false, "AV27TFNewProductosNombre", AV27TFNewProductosNombre);
         AV28TFNewProductosNombre_Sel = "";
         AssignAttri("", false, "AV28TFNewProductosNombre_Sel", AV28TFNewProductosNombre_Sel);
         AV31TFNewProductosNumeroDescargas = 0;
         AssignAttri("", false, "AV31TFNewProductosNumeroDescargas", StringUtil.LTrimStr( (decimal)(AV31TFNewProductosNumeroDescargas), 4, 0));
         AV32TFNewProductosNumeroDescargas_To = 0;
         AssignAttri("", false, "AV32TFNewProductosNumeroDescargas_To", StringUtil.LTrimStr( (decimal)(AV32TFNewProductosNumeroDescargas_To), 4, 0));
         AV51TFNewProductosNumeroVentas = 0;
         AssignAttri("", false, "AV51TFNewProductosNumeroVentas", StringUtil.LTrimStr( (decimal)(AV51TFNewProductosNumeroVentas), 4, 0));
         AV52TFNewProductosNumeroVentas_To = 0;
         AssignAttri("", false, "AV52TFNewProductosNumeroVentas_To", StringUtil.LTrimStr( (decimal)(AV52TFNewProductosNumeroVentas_To), 4, 0));
         AV53TFNewProductosVisitas = 0;
         AssignAttri("", false, "AV53TFNewProductosVisitas", StringUtil.LTrimStr( (decimal)(AV53TFNewProductosVisitas), 4, 0));
         AV54TFNewProductosVisitas_To = 0;
         AssignAttri("", false, "AV54TFNewProductosVisitas_To", StringUtil.LTrimStr( (decimal)(AV54TFNewProductosVisitas_To), 4, 0));
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
         if ( AV45IsAuthorized_Display )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "newproductos.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.LTrimStr(A34NewProductosId,4,0));
            CallWebObject(formatLink("newproductos.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         if ( AV46IsAuthorized_Update )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "newproductos.aspx"+UrlEncode(StringUtil.RTrim("UPD")) + "," + UrlEncode(StringUtil.LTrimStr(A34NewProductosId,4,0));
            CallWebObject(formatLink("newproductos.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         if ( AV47IsAuthorized_Delete )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "newproductos.aspx"+UrlEncode(StringUtil.RTrim("DLT")) + "," + UrlEncode(StringUtil.LTrimStr(A34NewProductosId,4,0));
            CallWebObject(formatLink("newproductos.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         if ( StringUtil.StrCmp(AV23Session.Get(AV57Pgmname+"GridState"), "") == 0 )
         {
            AV11GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  AV57Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV11GridState.FromXml(AV23Session.Get(AV57Pgmname+"GridState"), null, "", "");
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
         AV69GXV1 = 1;
         while ( AV69GXV1 <= AV11GridState.gxTpr_Filtervalues.Count )
         {
            AV12GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV11GridState.gxTpr_Filtervalues.Item(AV69GXV1));
            if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV16FilterFullText = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV16FilterFullText", AV16FilterFullText);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSID") == 0 )
            {
               AV55TFNewProductosId = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV55TFNewProductosId", StringUtil.LTrimStr( (decimal)(AV55TFNewProductosId), 4, 0));
               AV56TFNewProductosId_To = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV56TFNewProductosId_To", StringUtil.LTrimStr( (decimal)(AV56TFNewProductosId_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE") == 0 )
            {
               AV27TFNewProductosNombre = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV27TFNewProductosNombre", AV27TFNewProductosNombre);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE_SEL") == 0 )
            {
               AV28TFNewProductosNombre_Sel = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV28TFNewProductosNombre_Sel", AV28TFNewProductosNombre_Sel);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMERODESCARGAS") == 0 )
            {
               AV31TFNewProductosNumeroDescargas = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV31TFNewProductosNumeroDescargas", StringUtil.LTrimStr( (decimal)(AV31TFNewProductosNumeroDescargas), 4, 0));
               AV32TFNewProductosNumeroDescargas_To = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV32TFNewProductosNumeroDescargas_To", StringUtil.LTrimStr( (decimal)(AV32TFNewProductosNumeroDescargas_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMEROVENTAS") == 0 )
            {
               AV51TFNewProductosNumeroVentas = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV51TFNewProductosNumeroVentas", StringUtil.LTrimStr( (decimal)(AV51TFNewProductosNumeroVentas), 4, 0));
               AV52TFNewProductosNumeroVentas_To = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV52TFNewProductosNumeroVentas_To", StringUtil.LTrimStr( (decimal)(AV52TFNewProductosNumeroVentas_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSVISITAS") == 0 )
            {
               AV53TFNewProductosVisitas = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV53TFNewProductosVisitas", StringUtil.LTrimStr( (decimal)(AV53TFNewProductosVisitas), 4, 0));
               AV54TFNewProductosVisitas_To = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV54TFNewProductosVisitas_To", StringUtil.LTrimStr( (decimal)(AV54TFNewProductosVisitas_To), 4, 0));
            }
            AV69GXV1 = (int)(AV69GXV1+1);
         }
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV28TFNewProductosNombre_Sel)),  AV28TFNewProductosNombre_Sel, out  GXt_char2) ;
         Ddo_grid_Selectedvalue_set = "||"+GXt_char2+"|||";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV27TFNewProductosNombre)),  AV27TFNewProductosNombre, out  GXt_char2) ;
         Ddo_grid_Filteredtext_set = ((0==AV55TFNewProductosId) ? "" : StringUtil.Str( (decimal)(AV55TFNewProductosId), 4, 0))+"||"+GXt_char2+"|"+((0==AV31TFNewProductosNumeroDescargas) ? "" : StringUtil.Str( (decimal)(AV31TFNewProductosNumeroDescargas), 4, 0))+"|"+((0==AV51TFNewProductosNumeroVentas) ? "" : StringUtil.Str( (decimal)(AV51TFNewProductosNumeroVentas), 4, 0))+"|"+((0==AV53TFNewProductosVisitas) ? "" : StringUtil.Str( (decimal)(AV53TFNewProductosVisitas), 4, 0));
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = ((0==AV56TFNewProductosId_To) ? "" : StringUtil.Str( (decimal)(AV56TFNewProductosId_To), 4, 0))+"|||"+((0==AV32TFNewProductosNumeroDescargas_To) ? "" : StringUtil.Str( (decimal)(AV32TFNewProductosNumeroDescargas_To), 4, 0))+"|"+((0==AV52TFNewProductosNumeroVentas_To) ? "" : StringUtil.Str( (decimal)(AV52TFNewProductosNumeroVentas_To), 4, 0))+"|"+((0==AV54TFNewProductosVisitas_To) ? "" : StringUtil.Str( (decimal)(AV54TFNewProductosVisitas_To), 4, 0));
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S162( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV11GridState.FromXml(AV23Session.Get(AV57Pgmname+"GridState"), null, "", "");
         AV11GridState.gxTpr_Orderedby = AV13OrderedBy;
         AV11GridState.gxTpr_Ordereddsc = AV14OrderedDsc;
         AV11GridState.gxTpr_Filtervalues.Clear();
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "FILTERFULLTEXT",  context.GetMessage( "WWP_FullTextFilterDescription", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV16FilterFullText)),  0,  AV16FilterFullText,  AV16FilterFullText,  false,  "",  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWPRODUCTOSID",  context.GetMessage( "Id", ""),  !((0==AV55TFNewProductosId)&&(0==AV56TFNewProductosId_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV55TFNewProductosId), 4, 0)),  ((0==AV55TFNewProductosId) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV55TFNewProductosId), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV56TFNewProductosId_To), 4, 0)),  ((0==AV56TFNewProductosId_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV56TFNewProductosId_To), "ZZZ9")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV11GridState,  "TFNEWPRODUCTOSNOMBRE",  context.GetMessage( "Nombre", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV27TFNewProductosNombre)),  0,  AV27TFNewProductosNombre,  AV27TFNewProductosNombre,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV28TFNewProductosNombre_Sel)),  AV28TFNewProductosNombre_Sel,  AV28TFNewProductosNombre_Sel) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWPRODUCTOSNUMERODESCARGAS",  context.GetMessage( "Descargas", ""),  !((0==AV31TFNewProductosNumeroDescargas)&&(0==AV32TFNewProductosNumeroDescargas_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV31TFNewProductosNumeroDescargas), 4, 0)),  ((0==AV31TFNewProductosNumeroDescargas) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV31TFNewProductosNumeroDescargas), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV32TFNewProductosNumeroDescargas_To), 4, 0)),  ((0==AV32TFNewProductosNumeroDescargas_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV32TFNewProductosNumeroDescargas_To), "ZZZ9")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWPRODUCTOSNUMEROVENTAS",  context.GetMessage( "Ventas", ""),  !((0==AV51TFNewProductosNumeroVentas)&&(0==AV52TFNewProductosNumeroVentas_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV51TFNewProductosNumeroVentas), 4, 0)),  ((0==AV51TFNewProductosNumeroVentas) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV51TFNewProductosNumeroVentas), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV52TFNewProductosNumeroVentas_To), 4, 0)),  ((0==AV52TFNewProductosNumeroVentas_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV52TFNewProductosNumeroVentas_To), "ZZZ9")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFNEWPRODUCTOSVISITAS",  context.GetMessage( "Visitas", ""),  !((0==AV53TFNewProductosVisitas)&&(0==AV54TFNewProductosVisitas_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV53TFNewProductosVisitas), 4, 0)),  ((0==AV53TFNewProductosVisitas) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV53TFNewProductosVisitas), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV54TFNewProductosVisitas_To), 4, 0)),  ((0==AV54TFNewProductosVisitas_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV54TFNewProductosVisitas_To), "ZZZ9")))) ;
         AV11GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         AV11GridState.gxTpr_Currentpage = (short)(subGrid_fnc_Currentpage( ));
         new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV57Pgmname+"GridState",  AV11GridState.ToXml(false, true, "", "")) ;
      }

      protected void S122( )
      {
         /* 'PREPARETRANSACTION' Routine */
         returnInSub = false;
         AV9TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV9TrnContext.gxTpr_Callerobject = AV57Pgmname;
         AV9TrnContext.gxTpr_Callerondelete = true;
         AV9TrnContext.gxTpr_Callerurl = AV8HTTPRequest.ScriptName+"?"+AV8HTTPRequest.QueryString;
         AV9TrnContext.gxTpr_Transactionname = "NewProductos";
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
         new newproductoswwexport(context ).execute( out  AV17ExcelFilename, out  AV18ErrorMessage) ;
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
         Innewwindow1_Target = formatLink("newproductoswwexportreport.aspx") ;
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
         PA2W2( ) ;
         WS2W2( ) ;
         WE2W2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217072690", true, true);
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
         context.AddJavascriptSource("newproductosww.js", "?20241217072694", false, true);
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
         edtNewProductosId_Internalname = "NEWPRODUCTOSID_"+sGXsfl_44_idx;
         edtNewProductosImagen_Internalname = "NEWPRODUCTOSIMAGEN_"+sGXsfl_44_idx;
         edtNewProductosNombre_Internalname = "NEWPRODUCTOSNOMBRE_"+sGXsfl_44_idx;
         edtNewProductosNumeroDescargas_Internalname = "NEWPRODUCTOSNUMERODESCARGAS_"+sGXsfl_44_idx;
         edtNewProductosNumeroVentas_Internalname = "NEWPRODUCTOSNUMEROVENTAS_"+sGXsfl_44_idx;
         edtNewProductosVisitas_Internalname = "NEWPRODUCTOSVISITAS_"+sGXsfl_44_idx;
      }

      protected void SubsflControlProps_fel_442( )
      {
         cmbavGridactions_Internalname = "vGRIDACTIONS_"+sGXsfl_44_fel_idx;
         edtNewProductosId_Internalname = "NEWPRODUCTOSID_"+sGXsfl_44_fel_idx;
         edtNewProductosImagen_Internalname = "NEWPRODUCTOSIMAGEN_"+sGXsfl_44_fel_idx;
         edtNewProductosNombre_Internalname = "NEWPRODUCTOSNOMBRE_"+sGXsfl_44_fel_idx;
         edtNewProductosNumeroDescargas_Internalname = "NEWPRODUCTOSNUMERODESCARGAS_"+sGXsfl_44_fel_idx;
         edtNewProductosNumeroVentas_Internalname = "NEWPRODUCTOSNUMEROVENTAS_"+sGXsfl_44_fel_idx;
         edtNewProductosVisitas_Internalname = "NEWPRODUCTOSVISITAS_"+sGXsfl_44_fel_idx;
      }

      protected void sendrow_442( )
      {
         sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
         SubsflControlProps_442( ) ;
         WB2W0( ) ;
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
                  AV44GridActions = (short)(Math.Round(NumberUtil.Val( cmbavGridactions.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV44GridActions), 4, 0))), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV44GridActions), 4, 0));
               }
            }
            /* ComboBox */
            GridRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavGridactions,(string)cmbavGridactions_Internalname,StringUtil.Trim( StringUtil.Str( (decimal)(AV44GridActions), 4, 0)),(short)1,(string)cmbavGridactions_Jsonclick,(short)5,"'"+""+"'"+",false,"+"'"+"EVGRIDACTIONS.CLICK."+sGXsfl_44_idx+"'",(string)"int",(string)"",(short)-1,(short)1,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)cmbavGridactions_Class,(string)"WWActionGroupColumn",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,45);\"",(string)"",(bool)true,(short)0});
            cmbavGridactions.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV44GridActions), 4, 0));
            AssignProp("", false, cmbavGridactions_Internalname, "Values", (string)(cmbavGridactions.ToJavascriptSource()), !bGXsfl_44_Refreshing);
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtNewProductosId_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewProductosId_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A34NewProductosId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewProductosId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewProductosId_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Id",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtNewProductosImagen_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Static Bitmap Variable */
            ClassString = "Attribute";
            StyleString = "";
            A35NewProductosImagen_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen))&&String.IsNullOrEmpty(StringUtil.RTrim( A40000NewProductosImagen_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)));
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.PathToRelativeUrl( A35NewProductosImagen));
            GridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtNewProductosImagen_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtNewProductosImagen_Visible,(short)0,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)0,(string)"",(string)"",(string)StyleString,(string)ClassString,(string)"WWColumn",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(bool)A35NewProductosImagen_IsBlob,(bool)true,context.GetImageSrcSet( sImgUrl)});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtNewProductosNombre_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewProductosNombre_Internalname,(string)A36NewProductosNombre,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewProductosNombre_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewProductosNombre_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)200,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusUnanimo\\Description",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtNewProductosNumeroDescargas_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewProductosNumeroDescargas_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A39NewProductosNumeroDescargas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A39NewProductosNumeroDescargas), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewProductosNumeroDescargas_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewProductosNumeroDescargas_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtNewProductosNumeroVentas_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewProductosNumeroVentas_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A42NewProductosNumeroVentas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A42NewProductosNumeroVentas), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewProductosNumeroVentas_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewProductosNumeroVentas_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtNewProductosVisitas_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtNewProductosVisitas_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A43NewProductosVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A43NewProductosVisitas), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtNewProductosVisitas_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtNewProductosVisitas_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"",(string)"end",(bool)false,(string)""});
            send_integrity_lvl_hashes2W2( ) ;
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
            AV44GridActions = (short)(Math.Round(NumberUtil.Val( cmbavGridactions.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV44GridActions), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, cmbavGridactions_Internalname, StringUtil.LTrimStr( (decimal)(AV44GridActions), 4, 0));
         }
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
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewProductosId_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Id", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewProductosImagen_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Imagen", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewProductosNombre_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Nombre", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewProductosNumeroDescargas_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Descargas", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewProductosNumeroVentas_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Ventas", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtNewProductosVisitas_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Visitas", "")) ;
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
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(AV44GridActions), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( cmbavGridactions_Class));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A34NewProductosId), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewProductosId_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", context.convertURL( A35NewProductosImagen));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewProductosImagen_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( A36NewProductosNombre));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewProductosNombre_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A39NewProductosNumeroDescargas), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewProductosNumeroDescargas_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A42NewProductosNumeroVentas), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewProductosNumeroVentas_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A43NewProductosVisitas), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtNewProductosVisitas_Visible), 5, 0, ".", "")));
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
         edtNewProductosId_Internalname = "NEWPRODUCTOSID";
         edtNewProductosImagen_Internalname = "NEWPRODUCTOSIMAGEN";
         edtNewProductosNombre_Internalname = "NEWPRODUCTOSNOMBRE";
         edtNewProductosNumeroDescargas_Internalname = "NEWPRODUCTOSNUMERODESCARGAS";
         edtNewProductosNumeroVentas_Internalname = "NEWPRODUCTOSNUMEROVENTAS";
         edtNewProductosVisitas_Internalname = "NEWPRODUCTOSVISITAS";
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
         edtNewProductosVisitas_Jsonclick = "";
         edtNewProductosNumeroVentas_Jsonclick = "";
         edtNewProductosNumeroDescargas_Jsonclick = "";
         edtNewProductosNombre_Jsonclick = "";
         edtNewProductosId_Jsonclick = "";
         cmbavGridactions_Jsonclick = "";
         cmbavGridactions_Class = "ConvertToDDO";
         subGrid_Class = "GridWithPaginationBar GridNoBorder WorkWith";
         subGrid_Backcolorstyle = 0;
         edtNewProductosVisitas_Visible = -1;
         edtNewProductosNumeroVentas_Visible = -1;
         edtNewProductosNumeroDescargas_Visible = -1;
         edtNewProductosNombre_Visible = -1;
         edtNewProductosImagen_Visible = -1;
         edtNewProductosId_Visible = -1;
         edtNewProductosVisitas_Enabled = 0;
         edtNewProductosNumeroVentas_Enabled = 0;
         edtNewProductosNumeroDescargas_Enabled = 0;
         edtNewProductosNombre_Enabled = 0;
         edtNewProductosImagen_Enabled = 0;
         edtNewProductosId_Enabled = 0;
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
         Ddo_grid_Format = "4.0|||4.0|4.0|4.0";
         Ddo_grid_Datalistproc = "NewProductosWWGetFilterData";
         Ddo_grid_Datalisttype = "||Dynamic|||";
         Ddo_grid_Includedatalist = "||T|||";
         Ddo_grid_Filterisrange = "T|||T|T|T";
         Ddo_grid_Filtertype = "Numeric||Character|Numeric|Numeric|Numeric";
         Ddo_grid_Includefilter = "T||T|T|T|T";
         Ddo_grid_Fixable = "T";
         Ddo_grid_Includesortasc = "T||T|T|T|T";
         Ddo_grid_Columnssortvalues = "2||1|3|4|5";
         Ddo_grid_Columnids = "1:NewProductosId|2:NewProductosImagen|3:NewProductosNombre|4:NewProductosNumeroDescargas|5:NewProductosNumeroVentas|6:NewProductosVisitas";
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
         Dvpanel_unnamedtable1_Title = context.GetMessage( "Listado de Soluciones", "");
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
         Form.Caption = context.GetMessage( " New Productos", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewProductosId_Visible","ctrl":"NEWPRODUCTOSID","prop":"Visible"},{"av":"edtNewProductosImagen_Visible","ctrl":"NEWPRODUCTOSIMAGEN","prop":"Visible"},{"av":"edtNewProductosNombre_Visible","ctrl":"NEWPRODUCTOSNOMBRE","prop":"Visible"},{"av":"edtNewProductosNumeroDescargas_Visible","ctrl":"NEWPRODUCTOSNUMERODESCARGAS","prop":"Visible"},{"av":"edtNewProductosNumeroVentas_Visible","ctrl":"NEWPRODUCTOSNUMEROVENTAS","prop":"Visible"},{"av":"edtNewProductosVisitas_Visible","ctrl":"NEWPRODUCTOSVISITAS","prop":"Visible"},{"av":"AV41GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV42GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV43GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E122W2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E132W2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED","""{"handler":"E152W2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_grid_Activeeventkey","ctrl":"DDO_GRID","prop":"ActiveEventKey"},{"av":"Ddo_grid_Selectedvalue_get","ctrl":"DDO_GRID","prop":"SelectedValue_get"},{"av":"Ddo_grid_Selectedcolumn","ctrl":"DDO_GRID","prop":"SelectedColumn"},{"av":"Ddo_grid_Filteredtext_get","ctrl":"DDO_GRID","prop":"FilteredText_get"},{"av":"Ddo_grid_Filteredtextto_get","ctrl":"DDO_GRID","prop":"FilteredTextTo_get"}]""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED",""","oparms":[{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E202W2","iparms":[{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV44GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"}]}""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED","""{"handler":"E162W2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_gridcolumnsselector_Columnsselectorvalues","ctrl":"DDO_GRIDCOLUMNSSELECTOR","prop":"ColumnsSelectorValues"}]""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED",""","oparms":[{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"edtNewProductosId_Visible","ctrl":"NEWPRODUCTOSID","prop":"Visible"},{"av":"edtNewProductosImagen_Visible","ctrl":"NEWPRODUCTOSIMAGEN","prop":"Visible"},{"av":"edtNewProductosNombre_Visible","ctrl":"NEWPRODUCTOSNOMBRE","prop":"Visible"},{"av":"edtNewProductosNumeroDescargas_Visible","ctrl":"NEWPRODUCTOSNUMERODESCARGAS","prop":"Visible"},{"av":"edtNewProductosNumeroVentas_Visible","ctrl":"NEWPRODUCTOSNUMEROVENTAS","prop":"Visible"},{"av":"edtNewProductosVisitas_Visible","ctrl":"NEWPRODUCTOSVISITAS","prop":"Visible"},{"av":"AV41GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV42GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV43GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED","""{"handler":"E112W2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_managefilters_Activeeventkey","ctrl":"DDO_MANAGEFILTERS","prop":"ActiveEventKey"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewProductosId_Visible","ctrl":"NEWPRODUCTOSID","prop":"Visible"},{"av":"edtNewProductosImagen_Visible","ctrl":"NEWPRODUCTOSIMAGEN","prop":"Visible"},{"av":"edtNewProductosNombre_Visible","ctrl":"NEWPRODUCTOSNOMBRE","prop":"Visible"},{"av":"edtNewProductosNumeroDescargas_Visible","ctrl":"NEWPRODUCTOSNUMERODESCARGAS","prop":"Visible"},{"av":"edtNewProductosNumeroVentas_Visible","ctrl":"NEWPRODUCTOSNUMEROVENTAS","prop":"Visible"},{"av":"edtNewProductosVisitas_Visible","ctrl":"NEWPRODUCTOSVISITAS","prop":"Visible"},{"av":"AV41GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV42GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV43GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"}]}""");
         setEventMetadata("VGRIDACTIONS.CLICK","""{"handler":"E212W2","iparms":[{"av":"cmbavGridactions"},{"av":"AV44GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A34NewProductosId","fld":"NEWPRODUCTOSID","pic":"ZZZ9","hsh":true}]""");
         setEventMetadata("VGRIDACTIONS.CLICK",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV44GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewProductosId_Visible","ctrl":"NEWPRODUCTOSID","prop":"Visible"},{"av":"edtNewProductosImagen_Visible","ctrl":"NEWPRODUCTOSIMAGEN","prop":"Visible"},{"av":"edtNewProductosNombre_Visible","ctrl":"NEWPRODUCTOSNOMBRE","prop":"Visible"},{"av":"edtNewProductosNumeroDescargas_Visible","ctrl":"NEWPRODUCTOSNUMERODESCARGAS","prop":"Visible"},{"av":"edtNewProductosNumeroVentas_Visible","ctrl":"NEWPRODUCTOSNUMEROVENTAS","prop":"Visible"},{"av":"edtNewProductosVisitas_Visible","ctrl":"NEWPRODUCTOSVISITAS","prop":"Visible"},{"av":"AV41GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV42GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV43GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("'DOINSERT'","""{"handler":"E172W2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A34NewProductosId","fld":"NEWPRODUCTOSID","pic":"ZZZ9","hsh":true}]""");
         setEventMetadata("'DOINSERT'",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtNewProductosId_Visible","ctrl":"NEWPRODUCTOSID","prop":"Visible"},{"av":"edtNewProductosImagen_Visible","ctrl":"NEWPRODUCTOSIMAGEN","prop":"Visible"},{"av":"edtNewProductosNombre_Visible","ctrl":"NEWPRODUCTOSNOMBRE","prop":"Visible"},{"av":"edtNewProductosNumeroDescargas_Visible","ctrl":"NEWPRODUCTOSNUMERODESCARGAS","prop":"Visible"},{"av":"edtNewProductosNumeroVentas_Visible","ctrl":"NEWPRODUCTOSNUMEROVENTAS","prop":"Visible"},{"av":"edtNewProductosVisitas_Visible","ctrl":"NEWPRODUCTOSVISITAS","prop":"Visible"},{"av":"AV41GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV42GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV43GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_AGEXPORT.ONOPTIONCLICKED","""{"handler":"E142W2","iparms":[{"av":"Ddo_agexport_Activeeventkey","ctrl":"DDO_AGEXPORT","prop":"ActiveEventKey"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"}]""");
         setEventMetadata("DDO_AGEXPORT.ONOPTIONCLICKED",""","oparms":[{"av":"Innewwindow1_Target","ctrl":"INNEWWINDOW1","prop":"Target"},{"av":"Innewwindow1_Height","ctrl":"INNEWWINDOW1","prop":"Height"},{"av":"Innewwindow1_Width","ctrl":"INNEWWINDOW1","prop":"Width"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV57Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV55TFNewProductosId","fld":"vTFNEWPRODUCTOSID","pic":"ZZZ9"},{"av":"AV56TFNewProductosId_To","fld":"vTFNEWPRODUCTOSID_TO","pic":"ZZZ9"},{"av":"AV27TFNewProductosNombre","fld":"vTFNEWPRODUCTOSNOMBRE"},{"av":"AV28TFNewProductosNombre_Sel","fld":"vTFNEWPRODUCTOSNOMBRE_SEL"},{"av":"AV31TFNewProductosNumeroDescargas","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"AV32TFNewProductosNumeroDescargas_To","fld":"vTFNEWPRODUCTOSNUMERODESCARGAS_TO","pic":"ZZZ9"},{"av":"AV51TFNewProductosNumeroVentas","fld":"vTFNEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"AV52TFNewProductosNumeroVentas_To","fld":"vTFNEWPRODUCTOSNUMEROVENTAS_TO","pic":"ZZZ9"},{"av":"AV53TFNewProductosVisitas","fld":"vTFNEWPRODUCTOSVISITAS","pic":"ZZZ9"},{"av":"AV54TFNewProductosVisitas_To","fld":"vTFNEWPRODUCTOSVISITAS_TO","pic":"ZZZ9"},{"av":"AV45IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV46IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV47IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV50IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"}]}""");
         setEventMetadata("NULL","""{"handler":"Valid_Newproductosvisitas","iparms":[]}""");
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
         Ddo_grid_Filteredtextto_get = "";
         Ddo_gridcolumnsselector_Columnsselectorvalues = "";
         Ddo_managefilters_Activeeventkey = "";
         Ddo_agexport_Activeeventkey = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV16FilterFullText = "";
         AV21ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV57Pgmname = "";
         AV27TFNewProductosNombre = "";
         AV28TFNewProductosNombre_Sel = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV24ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV43GridAppliedFilters = "";
         AV48AGExportData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV37DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
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
         A35NewProductosImagen = "";
         A40000NewProductosImagen_GXI = "";
         A36NewProductosNombre = "";
         lV58Newproductoswwds_1_filterfulltext = "";
         lV61Newproductoswwds_4_tfnewproductosnombre = "";
         AV58Newproductoswwds_1_filterfulltext = "";
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = "";
         AV61Newproductoswwds_4_tfnewproductosnombre = "";
         H002W2_A43NewProductosVisitas = new short[1] ;
         H002W2_A42NewProductosNumeroVentas = new short[1] ;
         H002W2_A39NewProductosNumeroDescargas = new short[1] ;
         H002W2_A36NewProductosNombre = new string[] {""} ;
         H002W2_A40000NewProductosImagen_GXI = new string[] {""} ;
         H002W2_A34NewProductosId = new short[1] ;
         H002W2_A35NewProductosImagen = new string[] {""} ;
         H002W3_AGRID_nRecordCount = new long[1] ;
         AV8HTTPRequest = new GxHttpRequest( context);
         AV49AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV38GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV39GAMErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
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
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newproductosww__default(),
            new Object[][] {
                new Object[] {
               H002W2_A43NewProductosVisitas, H002W2_A42NewProductosNumeroVentas, H002W2_A39NewProductosNumeroDescargas, H002W2_A36NewProductosNombre, H002W2_A40000NewProductosImagen_GXI, H002W2_A34NewProductosId, H002W2_A35NewProductosImagen
               }
               , new Object[] {
               H002W3_AGRID_nRecordCount
               }
            }
         );
         AV57Pgmname = "NewProductosWW";
         /* GeneXus formulas. */
         AV57Pgmname = "NewProductosWW";
      }

      private short GRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short AV13OrderedBy ;
      private short AV26ManageFiltersExecutionStep ;
      private short AV55TFNewProductosId ;
      private short AV56TFNewProductosId_To ;
      private short AV31TFNewProductosNumeroDescargas ;
      private short AV32TFNewProductosNumeroDescargas_To ;
      private short AV51TFNewProductosNumeroVentas ;
      private short AV52TFNewProductosNumeroVentas_To ;
      private short AV53TFNewProductosVisitas ;
      private short AV54TFNewProductosVisitas_To ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV44GridActions ;
      private short A34NewProductosId ;
      private short A39NewProductosNumeroDescargas ;
      private short A42NewProductosNumeroVentas ;
      private short A43NewProductosVisitas ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid_Backcolorstyle ;
      private short subGrid_Sortable ;
      private short AV59Newproductoswwds_2_tfnewproductosid ;
      private short AV60Newproductoswwds_3_tfnewproductosid_to ;
      private short AV63Newproductoswwds_6_tfnewproductosnumerodescargas ;
      private short AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ;
      private short AV65Newproductoswwds_8_tfnewproductosnumeroventas ;
      private short AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ;
      private short AV67Newproductoswwds_10_tfnewproductosvisitas ;
      private short AV68Newproductoswwds_11_tfnewproductosvisitas_to ;
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
      private int edtNewProductosId_Enabled ;
      private int edtNewProductosImagen_Enabled ;
      private int edtNewProductosNombre_Enabled ;
      private int edtNewProductosNumeroDescargas_Enabled ;
      private int edtNewProductosNumeroVentas_Enabled ;
      private int edtNewProductosVisitas_Enabled ;
      private int edtNewProductosId_Visible ;
      private int edtNewProductosImagen_Visible ;
      private int edtNewProductosNombre_Visible ;
      private int edtNewProductosNumeroDescargas_Visible ;
      private int edtNewProductosNumeroVentas_Visible ;
      private int edtNewProductosVisitas_Visible ;
      private int AV40PageToGo ;
      private int AV69GXV1 ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long GRID_nFirstRecordOnPage ;
      private long AV41GridCurrentPage ;
      private long AV42GridPageCount ;
      private long GRID_nCurrentRecord ;
      private long GRID_nRecordCount ;
      private string Gridpaginationbar_Selectedpage ;
      private string Ddo_grid_Activeeventkey ;
      private string Ddo_grid_Selectedvalue_get ;
      private string Ddo_grid_Selectedcolumn ;
      private string Ddo_grid_Filteredtext_get ;
      private string Ddo_grid_Filteredtextto_get ;
      private string Ddo_gridcolumnsselector_Columnsselectorvalues ;
      private string Ddo_managefilters_Activeeventkey ;
      private string Ddo_agexport_Activeeventkey ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_44_idx="0001" ;
      private string AV57Pgmname ;
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
      private string edtNewProductosId_Internalname ;
      private string edtNewProductosImagen_Internalname ;
      private string edtNewProductosNombre_Internalname ;
      private string edtNewProductosNumeroDescargas_Internalname ;
      private string edtNewProductosNumeroVentas_Internalname ;
      private string edtNewProductosVisitas_Internalname ;
      private string cmbavGridactions_Class ;
      private string GXEncryptionTmp ;
      private string GXt_char2 ;
      private string sGXsfl_44_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string GXCCtl ;
      private string cmbavGridactions_Jsonclick ;
      private string ROClassString ;
      private string edtNewProductosId_Jsonclick ;
      private string sImgUrl ;
      private string edtNewProductosNombre_Jsonclick ;
      private string edtNewProductosNumeroDescargas_Jsonclick ;
      private string edtNewProductosNumeroVentas_Jsonclick ;
      private string edtNewProductosVisitas_Jsonclick ;
      private string subGrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV14OrderedDsc ;
      private bool AV45IsAuthorized_Display ;
      private bool AV46IsAuthorized_Update ;
      private bool AV47IsAuthorized_Delete ;
      private bool AV50IsAuthorized_Insert ;
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
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool GXt_boolean3 ;
      private bool A35NewProductosImagen_IsBlob ;
      private string AV19ColumnsSelectorXML ;
      private string AV25ManageFiltersXml ;
      private string AV20UserCustomValue ;
      private string AV16FilterFullText ;
      private string AV27TFNewProductosNombre ;
      private string AV28TFNewProductosNombre_Sel ;
      private string AV43GridAppliedFilters ;
      private string A40000NewProductosImagen_GXI ;
      private string A36NewProductosNombre ;
      private string lV58Newproductoswwds_1_filterfulltext ;
      private string lV61Newproductoswwds_4_tfnewproductosnombre ;
      private string AV58Newproductoswwds_1_filterfulltext ;
      private string AV62Newproductoswwds_5_tfnewproductosnombre_sel ;
      private string AV61Newproductoswwds_4_tfnewproductosnombre ;
      private string AV17ExcelFilename ;
      private string AV18ErrorMessage ;
      private string A35NewProductosImagen ;
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
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV21ColumnsSelector ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV24ManageFiltersData ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV48AGExportData ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV37DDO_TitleSettingsIcons ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV11GridState ;
      private IDataStoreProvider pr_default ;
      private short[] H002W2_A43NewProductosVisitas ;
      private short[] H002W2_A42NewProductosNumeroVentas ;
      private short[] H002W2_A39NewProductosNumeroDescargas ;
      private string[] H002W2_A36NewProductosNombre ;
      private string[] H002W2_A40000NewProductosImagen_GXI ;
      private short[] H002W2_A34NewProductosId ;
      private string[] H002W2_A35NewProductosImagen ;
      private long[] H002W3_AGRID_nRecordCount ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item AV49AGExportDataItem ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV38GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV39GAMErrors ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV6WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV22ColumnsSelectorAux ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV12GridStateFilterValue ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV9TrnContext ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

   public class newproductosww__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_H002W2( IGxContext context ,
                                             string AV58Newproductoswwds_1_filterfulltext ,
                                             short AV59Newproductoswwds_2_tfnewproductosid ,
                                             short AV60Newproductoswwds_3_tfnewproductosid_to ,
                                             string AV62Newproductoswwds_5_tfnewproductosnombre_sel ,
                                             string AV61Newproductoswwds_4_tfnewproductosnombre ,
                                             short AV63Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                             short AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                             short AV65Newproductoswwds_8_tfnewproductosnumeroventas ,
                                             short AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                             short AV67Newproductoswwds_10_tfnewproductosvisitas ,
                                             short AV68Newproductoswwds_11_tfnewproductosvisitas_to ,
                                             short A34NewProductosId ,
                                             string A36NewProductosNombre ,
                                             short A39NewProductosNumeroDescargas ,
                                             short A42NewProductosNumeroVentas ,
                                             short A43NewProductosVisitas ,
                                             short AV13OrderedBy ,
                                             bool AV14OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int5 = new short[17];
         Object[] GXv_Object6 = new Object[2];
         string sSelectString;
         string sFromString;
         string sOrderString;
         sSelectString = " `NewProductosVisitas`, `NewProductosNumeroVentas`, `NewProductosNumeroDescargas`, `NewProductosNombre`, `NewProductosImagen_GXI`, `NewProductosId`, `NewProductosImagen`";
         sFromString = " FROM `NewProductos`";
         sOrderString = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewProductosId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( `NewProductosNombre` like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroDescargas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroVentas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int5[0] = 1;
            GXv_int5[1] = 1;
            GXv_int5[2] = 1;
            GXv_int5[3] = 1;
            GXv_int5[4] = 1;
         }
         if ( ! (0==AV59Newproductoswwds_2_tfnewproductosid) )
         {
            AddWhere(sWhereString, "(`NewProductosId` >= @AV59Newproductoswwds_2_tfnewproductosid)");
         }
         else
         {
            GXv_int5[5] = 1;
         }
         if ( ! (0==AV60Newproductoswwds_3_tfnewproductosid_to) )
         {
            AddWhere(sWhereString, "(`NewProductosId` <= @AV60Newproductoswwds_3_tfnewproductosid_to)");
         }
         else
         {
            GXv_int5[6] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Newproductoswwds_5_tfnewproductosnombre_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newproductoswwds_4_tfnewproductosnombre)) ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` like @lV61Newproductoswwds_4_tfnewproductosnombre)");
         }
         else
         {
            GXv_int5[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newproductoswwds_5_tfnewproductosnombre_sel)) && ! ( StringUtil.StrCmp(AV62Newproductoswwds_5_tfnewproductosnombre_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` = @AV62Newproductoswwds_5_tfnewproductosnombre_sel)");
         }
         else
         {
            GXv_int5[8] = 1;
         }
         if ( StringUtil.StrCmp(AV62Newproductoswwds_5_tfnewproductosnombre_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewProductosNombre`))=0))");
         }
         if ( ! (0==AV63Newproductoswwds_6_tfnewproductosnumerodescargas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` >= @AV63Newproductoswwds_6_tfnewproductosnumerodescargas)");
         }
         else
         {
            GXv_int5[9] = 1;
         }
         if ( ! (0==AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` <= @AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to)");
         }
         else
         {
            GXv_int5[10] = 1;
         }
         if ( ! (0==AV65Newproductoswwds_8_tfnewproductosnumeroventas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` >= @AV65Newproductoswwds_8_tfnewproductosnumeroventas)");
         }
         else
         {
            GXv_int5[11] = 1;
         }
         if ( ! (0==AV66Newproductoswwds_9_tfnewproductosnumeroventas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` <= @AV66Newproductoswwds_9_tfnewproductosnumeroventas_to)");
         }
         else
         {
            GXv_int5[12] = 1;
         }
         if ( ! (0==AV67Newproductoswwds_10_tfnewproductosvisitas) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` >= @AV67Newproductoswwds_10_tfnewproductosvisitas)");
         }
         else
         {
            GXv_int5[13] = 1;
         }
         if ( ! (0==AV68Newproductoswwds_11_tfnewproductosvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` <= @AV68Newproductoswwds_11_tfnewproductosvisitas_to)");
         }
         else
         {
            GXv_int5[14] = 1;
         }
         if ( ( AV13OrderedBy == 1 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewProductosNombre`";
         }
         else if ( ( AV13OrderedBy == 1 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewProductosNombre` DESC";
         }
         else if ( ( AV13OrderedBy == 2 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewProductosId`";
         }
         else if ( ( AV13OrderedBy == 2 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewProductosId` DESC";
         }
         else if ( ( AV13OrderedBy == 3 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewProductosNumeroDescargas`";
         }
         else if ( ( AV13OrderedBy == 3 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewProductosNumeroDescargas` DESC";
         }
         else if ( ( AV13OrderedBy == 4 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewProductosNumeroVentas`";
         }
         else if ( ( AV13OrderedBy == 4 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewProductosNumeroVentas` DESC";
         }
         else if ( ( AV13OrderedBy == 5 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `NewProductosVisitas`";
         }
         else if ( ( AV13OrderedBy == 5 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `NewProductosVisitas` DESC";
         }
         else if ( true )
         {
            sOrderString += " ORDER BY `NewProductosId`";
         }
         scmdbuf = "SELECT " + sSelectString + sFromString + sWhereString + sOrderString + "" + " LIMIT " + "@GXPagingFrom2" + ", " + "@GXPagingTo2";
         GXv_Object6[0] = scmdbuf;
         GXv_Object6[1] = GXv_int5;
         return GXv_Object6 ;
      }

      protected Object[] conditional_H002W3( IGxContext context ,
                                             string AV58Newproductoswwds_1_filterfulltext ,
                                             short AV59Newproductoswwds_2_tfnewproductosid ,
                                             short AV60Newproductoswwds_3_tfnewproductosid_to ,
                                             string AV62Newproductoswwds_5_tfnewproductosnombre_sel ,
                                             string AV61Newproductoswwds_4_tfnewproductosnombre ,
                                             short AV63Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                             short AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                             short AV65Newproductoswwds_8_tfnewproductosnumeroventas ,
                                             short AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                             short AV67Newproductoswwds_10_tfnewproductosvisitas ,
                                             short AV68Newproductoswwds_11_tfnewproductosvisitas_to ,
                                             short A34NewProductosId ,
                                             string A36NewProductosNombre ,
                                             short A39NewProductosNumeroDescargas ,
                                             short A42NewProductosNumeroVentas ,
                                             short A43NewProductosVisitas ,
                                             short AV13OrderedBy ,
                                             bool AV14OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int7 = new short[15];
         Object[] GXv_Object8 = new Object[2];
         scmdbuf = "SELECT COUNT(*) FROM `NewProductos`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewProductosId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( `NewProductosNombre` like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroDescargas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroVentas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int7[0] = 1;
            GXv_int7[1] = 1;
            GXv_int7[2] = 1;
            GXv_int7[3] = 1;
            GXv_int7[4] = 1;
         }
         if ( ! (0==AV59Newproductoswwds_2_tfnewproductosid) )
         {
            AddWhere(sWhereString, "(`NewProductosId` >= @AV59Newproductoswwds_2_tfnewproductosid)");
         }
         else
         {
            GXv_int7[5] = 1;
         }
         if ( ! (0==AV60Newproductoswwds_3_tfnewproductosid_to) )
         {
            AddWhere(sWhereString, "(`NewProductosId` <= @AV60Newproductoswwds_3_tfnewproductosid_to)");
         }
         else
         {
            GXv_int7[6] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Newproductoswwds_5_tfnewproductosnombre_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newproductoswwds_4_tfnewproductosnombre)) ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` like @lV61Newproductoswwds_4_tfnewproductosnombre)");
         }
         else
         {
            GXv_int7[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newproductoswwds_5_tfnewproductosnombre_sel)) && ! ( StringUtil.StrCmp(AV62Newproductoswwds_5_tfnewproductosnombre_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` = @AV62Newproductoswwds_5_tfnewproductosnombre_sel)");
         }
         else
         {
            GXv_int7[8] = 1;
         }
         if ( StringUtil.StrCmp(AV62Newproductoswwds_5_tfnewproductosnombre_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewProductosNombre`))=0))");
         }
         if ( ! (0==AV63Newproductoswwds_6_tfnewproductosnumerodescargas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` >= @AV63Newproductoswwds_6_tfnewproductosnumerodescargas)");
         }
         else
         {
            GXv_int7[9] = 1;
         }
         if ( ! (0==AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` <= @AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to)");
         }
         else
         {
            GXv_int7[10] = 1;
         }
         if ( ! (0==AV65Newproductoswwds_8_tfnewproductosnumeroventas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` >= @AV65Newproductoswwds_8_tfnewproductosnumeroventas)");
         }
         else
         {
            GXv_int7[11] = 1;
         }
         if ( ! (0==AV66Newproductoswwds_9_tfnewproductosnumeroventas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` <= @AV66Newproductoswwds_9_tfnewproductosnumeroventas_to)");
         }
         else
         {
            GXv_int7[12] = 1;
         }
         if ( ! (0==AV67Newproductoswwds_10_tfnewproductosvisitas) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` >= @AV67Newproductoswwds_10_tfnewproductosvisitas)");
         }
         else
         {
            GXv_int7[13] = 1;
         }
         if ( ! (0==AV68Newproductoswwds_11_tfnewproductosvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` <= @AV68Newproductoswwds_11_tfnewproductosvisitas_to)");
         }
         else
         {
            GXv_int7[14] = 1;
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
         else if ( true )
         {
            scmdbuf += "";
         }
         GXv_Object8[0] = scmdbuf;
         GXv_Object8[1] = GXv_int7;
         return GXv_Object8 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_H002W2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (short)dynConstraints[5] , (short)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (short)dynConstraints[13] , (short)dynConstraints[14] , (short)dynConstraints[15] , (short)dynConstraints[16] , (bool)dynConstraints[17] );
               case 1 :
                     return conditional_H002W3(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (short)dynConstraints[5] , (short)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (short)dynConstraints[13] , (short)dynConstraints[14] , (short)dynConstraints[15] , (short)dynConstraints[16] , (bool)dynConstraints[17] );
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
          Object[] prmH002W2;
          prmH002W2 = new Object[] {
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV59Newproductoswwds_2_tfnewproductosid",GXType.Int16,4,0) ,
          new ParDef("@AV60Newproductoswwds_3_tfnewproductosid_to",GXType.Int16,4,0) ,
          new ParDef("@lV61Newproductoswwds_4_tfnewproductosnombre",GXType.Char,200,0) ,
          new ParDef("@AV62Newproductoswwds_5_tfnewproductosnombre_sel",GXType.Char,200,0) ,
          new ParDef("@AV63Newproductoswwds_6_tfnewproductosnumerodescargas",GXType.Int16,4,0) ,
          new ParDef("@AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to",GXType.Int16,4,0) ,
          new ParDef("@AV65Newproductoswwds_8_tfnewproductosnumeroventas",GXType.Int16,4,0) ,
          new ParDef("@AV66Newproductoswwds_9_tfnewproductosnumeroventas_to",GXType.Int16,4,0) ,
          new ParDef("@AV67Newproductoswwds_10_tfnewproductosvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV68Newproductoswwds_11_tfnewproductosvisitas_to",GXType.Int16,4,0) ,
          new ParDef("@GXPagingFrom2",GXType.Int32,9,0) ,
          new ParDef("@GXPagingTo2",GXType.Int32,9,0)
          };
          Object[] prmH002W3;
          prmH002W3 = new Object[] {
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV59Newproductoswwds_2_tfnewproductosid",GXType.Int16,4,0) ,
          new ParDef("@AV60Newproductoswwds_3_tfnewproductosid_to",GXType.Int16,4,0) ,
          new ParDef("@lV61Newproductoswwds_4_tfnewproductosnombre",GXType.Char,200,0) ,
          new ParDef("@AV62Newproductoswwds_5_tfnewproductosnombre_sel",GXType.Char,200,0) ,
          new ParDef("@AV63Newproductoswwds_6_tfnewproductosnumerodescargas",GXType.Int16,4,0) ,
          new ParDef("@AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to",GXType.Int16,4,0) ,
          new ParDef("@AV65Newproductoswwds_8_tfnewproductosnumeroventas",GXType.Int16,4,0) ,
          new ParDef("@AV66Newproductoswwds_9_tfnewproductosnumeroventas_to",GXType.Int16,4,0) ,
          new ParDef("@AV67Newproductoswwds_10_tfnewproductosvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV68Newproductoswwds_11_tfnewproductosvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("H002W2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH002W2,11, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("H002W3", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH002W3,1, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                ((string[]) buf[4])[0] = rslt.getMultimediaUri(5);
                ((short[]) buf[5])[0] = rslt.getShort(6);
                ((string[]) buf[6])[0] = rslt.getMultimediaFile(7, rslt.getVarchar(5));
                return;
             case 1 :
                ((long[]) buf[0])[0] = rslt.getLong(1);
                return;
       }
    }

 }

}
