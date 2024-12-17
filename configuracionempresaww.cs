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
   public class configuracionempresaww : GXDataArea
   {
      public configuracionempresaww( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public configuracionempresaww( IGxContext context )
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
         AV27TFConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( GetPar( "TFConfiguracionEmpresaId"), "."), 18, MidpointRounding.ToEven));
         AV28TFConfiguracionEmpresaId_To = (short)(Math.Round(NumberUtil.Val( GetPar( "TFConfiguracionEmpresaId_To"), "."), 18, MidpointRounding.ToEven));
         AV26ManageFiltersExecutionStep = (short)(Math.Round(NumberUtil.Val( GetPar( "ManageFiltersExecutionStep"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV21ColumnsSelector);
         AV61Pgmname = GetPar( "Pgmname");
         AV29TFConfiguracionEmpresaTelefono = GetPar( "TFConfiguracionEmpresaTelefono");
         AV30TFConfiguracionEmpresaTelefono_Sel = GetPar( "TFConfiguracionEmpresaTelefono_Sel");
         AV31TFConfiguracionEmpresaCostoPlanBasico = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoPlanBasico"), ".");
         AV32TFConfiguracionEmpresaCostoPlanBasico_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoPlanBasico_To"), ".");
         AV33TFConfiguracionEmpresaCuotaPlanBasico = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaPlanBasico"), ".");
         AV34TFConfiguracionEmpresaCuotaPlanBasico_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaPlanBasico_To"), ".");
         AV45TFConfiguracionEmpresaCostoPlanSuperior = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoPlanSuperior"), ".");
         AV46TFConfiguracionEmpresaCostoPlanSuperior_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoPlanSuperior_To"), ".");
         AV47TFConfiguracionEmpresaCuotaPlanSuperior = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaPlanSuperior"), ".");
         AV48TFConfiguracionEmpresaCuotaPlanSuperior_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaPlanSuperior_To"), ".");
         AV49TFConfiguracionEmpresaCostoPlanNegocios = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoPlanNegocios"), ".");
         AV50TFConfiguracionEmpresaCostoPlanNegocios_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoPlanNegocios_To"), ".");
         AV51TFConfiguracionEmpresaCuotaPlanNegocios = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaPlanNegocios"), ".");
         AV52TFConfiguracionEmpresaCuotaPlanNegocios_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaPlanNegocios_To"), ".");
         AV57TFConfiguracionEmpresaCostoLandingPage = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoLandingPage"), ".");
         AV58TFConfiguracionEmpresaCostoLandingPage_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCostoLandingPage_To"), ".");
         AV59TFConfiguracionEmpresaCuotaLandingPage = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaLandingPage"), ".");
         AV60TFConfiguracionEmpresaCuotaLandingPage_To = NumberUtil.Val( GetPar( "TFConfiguracionEmpresaCuotaLandingPage_To"), ".");
         AV53IsAuthorized_Display = StringUtil.StrToBool( GetPar( "IsAuthorized_Display"));
         AV54IsAuthorized_Update = StringUtil.StrToBool( GetPar( "IsAuthorized_Update"));
         AV55IsAuthorized_Delete = StringUtil.StrToBool( GetPar( "IsAuthorized_Delete"));
         AV56IsAuthorized_Insert = StringUtil.StrToBool( GetPar( "IsAuthorized_Insert"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV27TFConfiguracionEmpresaId, AV28TFConfiguracionEmpresaId_To, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV61Pgmname, AV29TFConfiguracionEmpresaTelefono, AV30TFConfiguracionEmpresaTelefono_Sel, AV31TFConfiguracionEmpresaCostoPlanBasico, AV32TFConfiguracionEmpresaCostoPlanBasico_To, AV33TFConfiguracionEmpresaCuotaPlanBasico, AV34TFConfiguracionEmpresaCuotaPlanBasico_To, AV45TFConfiguracionEmpresaCostoPlanSuperior, AV46TFConfiguracionEmpresaCostoPlanSuperior_To, AV47TFConfiguracionEmpresaCuotaPlanSuperior, AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, AV49TFConfiguracionEmpresaCostoPlanNegocios, AV50TFConfiguracionEmpresaCostoPlanNegocios_To, AV51TFConfiguracionEmpresaCuotaPlanNegocios, AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, AV57TFConfiguracionEmpresaCostoLandingPage, AV58TFConfiguracionEmpresaCostoLandingPage_To, AV59TFConfiguracionEmpresaCuotaLandingPage, AV60TFConfiguracionEmpresaCuotaLandingPage_To, AV53IsAuthorized_Display, AV54IsAuthorized_Update, AV55IsAuthorized_Delete, AV56IsAuthorized_Insert) ;
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
            return "configuracionempresaww_Execute" ;
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
         PA3R2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3R2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("configuracionempresaww.aspx") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV61Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV61Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV53IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV53IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV54IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV54IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV55IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV55IsAuthorized_Delete, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV56IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV56IsAuthorized_Insert, context));
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vAGEXPORTDATA", AV43AGExportData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vAGEXPORTDATA", AV43AGExportData);
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
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV61Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV61Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESAID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV27TFConfiguracionEmpresaId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESAID_TO", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV28TFConfiguracionEmpresaId_To), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESATELEFONO", StringUtil.RTrim( AV29TFConfiguracionEmpresaTelefono));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESATELEFONO_SEL", StringUtil.RTrim( AV30TFConfiguracionEmpresaTelefono_Sel));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOPLANBASICO", StringUtil.LTrim( StringUtil.NToC( AV31TFConfiguracionEmpresaCostoPlanBasico, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO", StringUtil.LTrim( StringUtil.NToC( AV32TFConfiguracionEmpresaCostoPlanBasico_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTAPLANBASICO", StringUtil.LTrim( StringUtil.NToC( AV33TFConfiguracionEmpresaCuotaPlanBasico, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO", StringUtil.LTrim( StringUtil.NToC( AV34TFConfiguracionEmpresaCuotaPlanBasico_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR", StringUtil.LTrim( StringUtil.NToC( AV45TFConfiguracionEmpresaCostoPlanSuperior, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO", StringUtil.LTrim( StringUtil.NToC( AV46TFConfiguracionEmpresaCostoPlanSuperior_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR", StringUtil.LTrim( StringUtil.NToC( AV47TFConfiguracionEmpresaCuotaPlanSuperior, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO", StringUtil.LTrim( StringUtil.NToC( AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS", StringUtil.LTrim( StringUtil.NToC( AV49TFConfiguracionEmpresaCostoPlanNegocios, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO", StringUtil.LTrim( StringUtil.NToC( AV50TFConfiguracionEmpresaCostoPlanNegocios_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS", StringUtil.LTrim( StringUtil.NToC( AV51TFConfiguracionEmpresaCuotaPlanNegocios, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO", StringUtil.LTrim( StringUtil.NToC( AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE", StringUtil.LTrim( StringUtil.NToC( AV57TFConfiguracionEmpresaCostoLandingPage, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO", StringUtil.LTrim( StringUtil.NToC( AV58TFConfiguracionEmpresaCostoLandingPage_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE", StringUtil.LTrim( StringUtil.NToC( AV59TFConfiguracionEmpresaCuotaLandingPage, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO", StringUtil.LTrim( StringUtil.NToC( AV60TFConfiguracionEmpresaCuotaLandingPage_To, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vORDEREDBY", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13OrderedBy), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, "vORDEREDDSC", AV14OrderedDsc);
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV53IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV53IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV54IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV54IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV55IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV55IsAuthorized_Delete, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGRIDSTATE", AV11GridState);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGRIDSTATE", AV11GridState);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV56IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV56IsAuthorized_Insert, context));
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
            WE3R2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3R2( ) ;
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
         return formatLink("configuracionempresaww.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "ConfiguracionEmpresaWW" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( " Configuracion Empresa", "") ;
      }

      protected void WB3R0( )
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
            ClassString = "ErrorViewer";
            StyleString = "";
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, StyleString, ClassString, "", "false");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtninsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "GXM_insert", ""), bttBtninsert_Jsonclick, 5, context.GetMessage( "GXM_insert", ""), "", StyleString, ClassString, bttBtninsert_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOINSERT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_ConfiguracionEmpresaWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 27,'',false,'',0)\"";
            ClassString = "ColumnsSelector";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnagexport_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "WWP_ExportData", ""), bttBtnagexport_Jsonclick, 0, context.GetMessage( "WWP_ExportData", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_ConfiguracionEmpresaWW.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'',false,'',0)\"";
            ClassString = "hidden-xs";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtneditcolumns_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(44), 2, 0)+","+"null"+");", context.GetMessage( "WWP_EditColumnsCaption", ""), bttBtneditcolumns_Jsonclick, 0, context.GetMessage( "WWP_EditColumnsTooltip", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_ConfiguracionEmpresaWW.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'" + sGXsfl_44_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFilterfulltext_Internalname, AV16FilterFullText, StringUtil.RTrim( context.localUtil.Format( AV16FilterFullText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "WWP_Search", ""), edtavFilterfulltext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFilterfulltext_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "WWPFullTextFilter", "start", true, "", "HLP_ConfiguracionEmpresaWW.htm");
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
            ucDdo_agexport.SetProperty("DropDownOptionsData", AV43AGExportData);
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

      protected void START3R2( )
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
         Form.Meta.addItem("description", context.GetMessage( " Configuracion Empresa", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3R0( ) ;
      }

      protected void WS3R2( )
      {
         START3R2( ) ;
         EVT3R2( ) ;
      }

      protected void EVT3R2( )
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
                              E113R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changepage */
                              E123R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changerowsperpage */
                              E133R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_AGEXPORT.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_agexport.Onoptionclicked */
                              E143R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRID.ONOPTIONCLICKED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_grid.Onoptionclicked */
                              E153R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_gridcolumnsselector.Oncolumnschanged */
                              E163R2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOINSERT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoInsert' */
                              E173R2 ();
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
                              A44ConfiguracionEmpresaId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              A45ConfiguracionEmpresaTelefono = cgiGet( edtConfiguracionEmpresaTelefono_Internalname);
                              A46ConfiguracionEmpresaCostoPlanB = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              A47ConfiguracionEmpresaCuotaPlanB = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              A48ConfiguracionEmpresaCostoPlanS = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              A49ConfiguracionEmpresaCuotaPlanS = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              A50ConfiguracionEmpresaCostoPlanN = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              A51ConfiguracionEmpresaCuotaPlanN = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              A54ConfiguracionEmpresaCostoLandi = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              A55ConfiguracionEmpresaCuotaLandi = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E183R2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E193R2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid.Load */
                                    E203R2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VGRIDACTIONS.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E213R2 ();
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

      protected void WE3R2( )
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

      protected void PA3R2( )
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
                                       short AV27TFConfiguracionEmpresaId ,
                                       short AV28TFConfiguracionEmpresaId_To ,
                                       short AV26ManageFiltersExecutionStep ,
                                       DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV21ColumnsSelector ,
                                       string AV61Pgmname ,
                                       string AV29TFConfiguracionEmpresaTelefono ,
                                       string AV30TFConfiguracionEmpresaTelefono_Sel ,
                                       decimal AV31TFConfiguracionEmpresaCostoPlanBasico ,
                                       decimal AV32TFConfiguracionEmpresaCostoPlanBasico_To ,
                                       decimal AV33TFConfiguracionEmpresaCuotaPlanBasico ,
                                       decimal AV34TFConfiguracionEmpresaCuotaPlanBasico_To ,
                                       decimal AV45TFConfiguracionEmpresaCostoPlanSuperior ,
                                       decimal AV46TFConfiguracionEmpresaCostoPlanSuperior_To ,
                                       decimal AV47TFConfiguracionEmpresaCuotaPlanSuperior ,
                                       decimal AV48TFConfiguracionEmpresaCuotaPlanSuperior_To ,
                                       decimal AV49TFConfiguracionEmpresaCostoPlanNegocios ,
                                       decimal AV50TFConfiguracionEmpresaCostoPlanNegocios_To ,
                                       decimal AV51TFConfiguracionEmpresaCuotaPlanNegocios ,
                                       decimal AV52TFConfiguracionEmpresaCuotaPlanNegocios_To ,
                                       decimal AV57TFConfiguracionEmpresaCostoLandingPage ,
                                       decimal AV58TFConfiguracionEmpresaCostoLandingPage_To ,
                                       decimal AV59TFConfiguracionEmpresaCuotaLandingPage ,
                                       decimal AV60TFConfiguracionEmpresaCuotaLandingPage_To ,
                                       bool AV53IsAuthorized_Display ,
                                       bool AV54IsAuthorized_Update ,
                                       bool AV55IsAuthorized_Delete ,
                                       bool AV56IsAuthorized_Insert )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RF3R2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_CONFIGURACIONEMPRESAID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "CONFIGURACIONEMPRESAID", StringUtil.LTrim( StringUtil.NToC( (decimal)(A44ConfiguracionEmpresaId), 4, 0, ".", "")));
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
         RF3R2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV61Pgmname = "ConfiguracionEmpresaWW";
      }

      protected void RF3R2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 44;
         /* Execute user event: Refresh */
         E193R2 ();
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
                                                 AV62Configuracionempresawwds_1_filterfulltext ,
                                                 AV63Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                                 AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                                 AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                                 AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                                 AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                                 AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                                 AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                                 AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                                 AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                                 AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                                 AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                                 AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                                 AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                                 AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                                 AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                                 AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                                 AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                                 AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                                 AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                                 AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
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
                                                 AV13OrderedBy ,
                                                 AV14OrderedDsc } ,
                                                 new int[]{
                                                 TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL,
                                                 TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.DECIMAL,
                                                 TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                                 }
            });
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
            lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = StringUtil.PadR( StringUtil.RTrim( AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono), 20, "%");
            /* Using cursor H003R2 */
            pr_default.execute(0, new Object[] {lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, AV63Configuracionempresawwds_2_tfconfiguracionempresaid, AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to, lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono, AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico, AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to, AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico, AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to, AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior, AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to, AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior, AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to, AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios, AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to, AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios, AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to, AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage, AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to, AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage, AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to, GXPagingFrom2, GXPagingTo2});
            nGXsfl_44_idx = 1;
            sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
            SubsflControlProps_442( ) ;
            while ( ( (pr_default.getStatus(0) != 101) ) && ( ( ( subGrid_Rows == 0 ) || ( GRID_nCurrentRecord < subGrid_fnc_Recordsperpage( ) ) ) ) )
            {
               A55ConfiguracionEmpresaCuotaLandi = H003R2_A55ConfiguracionEmpresaCuotaLandi[0];
               A54ConfiguracionEmpresaCostoLandi = H003R2_A54ConfiguracionEmpresaCostoLandi[0];
               A51ConfiguracionEmpresaCuotaPlanN = H003R2_A51ConfiguracionEmpresaCuotaPlanN[0];
               A50ConfiguracionEmpresaCostoPlanN = H003R2_A50ConfiguracionEmpresaCostoPlanN[0];
               A49ConfiguracionEmpresaCuotaPlanS = H003R2_A49ConfiguracionEmpresaCuotaPlanS[0];
               A48ConfiguracionEmpresaCostoPlanS = H003R2_A48ConfiguracionEmpresaCostoPlanS[0];
               A47ConfiguracionEmpresaCuotaPlanB = H003R2_A47ConfiguracionEmpresaCuotaPlanB[0];
               A46ConfiguracionEmpresaCostoPlanB = H003R2_A46ConfiguracionEmpresaCostoPlanB[0];
               A45ConfiguracionEmpresaTelefono = H003R2_A45ConfiguracionEmpresaTelefono[0];
               A44ConfiguracionEmpresaId = H003R2_A44ConfiguracionEmpresaId[0];
               /* Execute user event: Grid.Load */
               E203R2 ();
               pr_default.readNext(0);
            }
            GRID_nEOF = (short)(((pr_default.getStatus(0) == 101) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            pr_default.close(0);
            wbEnd = 44;
            WB3R0( ) ;
         }
         bGXsfl_44_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes3R2( )
      {
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV61Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV61Pgmname, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV53IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV53IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV54IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV54IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DELETE", AV55IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV55IsAuthorized_Delete, context));
         GxWebStd.gx_hidden_field( context, "gxhash_CONFIGURACIONEMPRESAID"+"_"+sGXsfl_44_idx, GetSecureSignedToken( sGXsfl_44_idx, context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV56IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV56IsAuthorized_Insert, context));
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
         AV62Configuracionempresawwds_1_filterfulltext = AV16FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV27TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV28TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV29TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV30TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV31TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV32TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV33TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV34TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV45TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV46TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV47TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV48TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV49TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV50TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV51TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV52TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV57TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV58TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV59TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV60TFConfiguracionEmpresaCuotaLandingPage_To;
         pr_default.dynParam(1, new Object[]{ new Object[]{
                                              AV62Configuracionempresawwds_1_filterfulltext ,
                                              AV63Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                              AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                              AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                              AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                              AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                              AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                              AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                              AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                              AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                              AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                              AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                              AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                              AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                              AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                              AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                              AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                              AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                              AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                              AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                              AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
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
                                              AV13OrderedBy ,
                                              AV14OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = StringUtil.PadR( StringUtil.RTrim( AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono), 20, "%");
         /* Using cursor H003R3 */
         pr_default.execute(1, new Object[] {lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, AV63Configuracionempresawwds_2_tfconfiguracionempresaid, AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to, lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono, AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico, AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to, AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico, AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to, AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior, AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to, AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior, AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to, AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios, AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to, AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios, AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to, AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage, AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to, AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage, AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to});
         GRID_nRecordCount = H003R3_AGRID_nRecordCount[0];
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
         AV62Configuracionempresawwds_1_filterfulltext = AV16FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV27TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV28TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV29TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV30TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV31TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV32TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV33TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV34TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV45TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV46TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV47TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV48TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV49TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV50TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV51TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV52TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV57TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV58TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV59TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV60TFConfiguracionEmpresaCuotaLandingPage_To;
         GRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV27TFConfiguracionEmpresaId, AV28TFConfiguracionEmpresaId_To, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV61Pgmname, AV29TFConfiguracionEmpresaTelefono, AV30TFConfiguracionEmpresaTelefono_Sel, AV31TFConfiguracionEmpresaCostoPlanBasico, AV32TFConfiguracionEmpresaCostoPlanBasico_To, AV33TFConfiguracionEmpresaCuotaPlanBasico, AV34TFConfiguracionEmpresaCuotaPlanBasico_To, AV45TFConfiguracionEmpresaCostoPlanSuperior, AV46TFConfiguracionEmpresaCostoPlanSuperior_To, AV47TFConfiguracionEmpresaCuotaPlanSuperior, AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, AV49TFConfiguracionEmpresaCostoPlanNegocios, AV50TFConfiguracionEmpresaCostoPlanNegocios_To, AV51TFConfiguracionEmpresaCuotaPlanNegocios, AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, AV57TFConfiguracionEmpresaCostoLandingPage, AV58TFConfiguracionEmpresaCostoLandingPage_To, AV59TFConfiguracionEmpresaCuotaLandingPage, AV60TFConfiguracionEmpresaCuotaLandingPage_To, AV53IsAuthorized_Display, AV54IsAuthorized_Update, AV55IsAuthorized_Delete, AV56IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_nextpage( )
      {
         AV62Configuracionempresawwds_1_filterfulltext = AV16FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV27TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV28TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV29TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV30TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV31TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV32TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV33TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV34TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV45TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV46TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV47TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV48TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV49TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV50TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV51TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV52TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV57TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV58TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV59TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV60TFConfiguracionEmpresaCuotaLandingPage_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV27TFConfiguracionEmpresaId, AV28TFConfiguracionEmpresaId_To, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV61Pgmname, AV29TFConfiguracionEmpresaTelefono, AV30TFConfiguracionEmpresaTelefono_Sel, AV31TFConfiguracionEmpresaCostoPlanBasico, AV32TFConfiguracionEmpresaCostoPlanBasico_To, AV33TFConfiguracionEmpresaCuotaPlanBasico, AV34TFConfiguracionEmpresaCuotaPlanBasico_To, AV45TFConfiguracionEmpresaCostoPlanSuperior, AV46TFConfiguracionEmpresaCostoPlanSuperior_To, AV47TFConfiguracionEmpresaCuotaPlanSuperior, AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, AV49TFConfiguracionEmpresaCostoPlanNegocios, AV50TFConfiguracionEmpresaCostoPlanNegocios_To, AV51TFConfiguracionEmpresaCuotaPlanNegocios, AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, AV57TFConfiguracionEmpresaCostoLandingPage, AV58TFConfiguracionEmpresaCostoLandingPage_To, AV59TFConfiguracionEmpresaCuotaLandingPage, AV60TFConfiguracionEmpresaCuotaLandingPage_To, AV53IsAuthorized_Display, AV54IsAuthorized_Update, AV55IsAuthorized_Delete, AV56IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgrid_previouspage( )
      {
         AV62Configuracionempresawwds_1_filterfulltext = AV16FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV27TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV28TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV29TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV30TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV31TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV32TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV33TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV34TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV45TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV46TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV47TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV48TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV49TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV50TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV51TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV52TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV57TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV58TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV59TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV60TFConfiguracionEmpresaCuotaLandingPage_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV27TFConfiguracionEmpresaId, AV28TFConfiguracionEmpresaId_To, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV61Pgmname, AV29TFConfiguracionEmpresaTelefono, AV30TFConfiguracionEmpresaTelefono_Sel, AV31TFConfiguracionEmpresaCostoPlanBasico, AV32TFConfiguracionEmpresaCostoPlanBasico_To, AV33TFConfiguracionEmpresaCuotaPlanBasico, AV34TFConfiguracionEmpresaCuotaPlanBasico_To, AV45TFConfiguracionEmpresaCostoPlanSuperior, AV46TFConfiguracionEmpresaCostoPlanSuperior_To, AV47TFConfiguracionEmpresaCuotaPlanSuperior, AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, AV49TFConfiguracionEmpresaCostoPlanNegocios, AV50TFConfiguracionEmpresaCostoPlanNegocios_To, AV51TFConfiguracionEmpresaCuotaPlanNegocios, AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, AV57TFConfiguracionEmpresaCostoLandingPage, AV58TFConfiguracionEmpresaCostoLandingPage_To, AV59TFConfiguracionEmpresaCuotaLandingPage, AV60TFConfiguracionEmpresaCuotaLandingPage_To, AV53IsAuthorized_Display, AV54IsAuthorized_Update, AV55IsAuthorized_Delete, AV56IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         AV62Configuracionempresawwds_1_filterfulltext = AV16FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV27TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV28TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV29TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV30TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV31TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV32TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV33TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV34TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV45TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV46TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV47TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV48TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV49TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV50TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV51TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV52TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV57TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV58TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV59TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV60TFConfiguracionEmpresaCuotaLandingPage_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV27TFConfiguracionEmpresaId, AV28TFConfiguracionEmpresaId_To, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV61Pgmname, AV29TFConfiguracionEmpresaTelefono, AV30TFConfiguracionEmpresaTelefono_Sel, AV31TFConfiguracionEmpresaCostoPlanBasico, AV32TFConfiguracionEmpresaCostoPlanBasico_To, AV33TFConfiguracionEmpresaCuotaPlanBasico, AV34TFConfiguracionEmpresaCuotaPlanBasico_To, AV45TFConfiguracionEmpresaCostoPlanSuperior, AV46TFConfiguracionEmpresaCostoPlanSuperior_To, AV47TFConfiguracionEmpresaCuotaPlanSuperior, AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, AV49TFConfiguracionEmpresaCostoPlanNegocios, AV50TFConfiguracionEmpresaCostoPlanNegocios_To, AV51TFConfiguracionEmpresaCuotaPlanNegocios, AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, AV57TFConfiguracionEmpresaCostoLandingPage, AV58TFConfiguracionEmpresaCostoLandingPage_To, AV59TFConfiguracionEmpresaCuotaLandingPage, AV60TFConfiguracionEmpresaCuotaLandingPage_To, AV53IsAuthorized_Display, AV54IsAuthorized_Update, AV55IsAuthorized_Delete, AV56IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgrid_gotopage( int nPageNo )
      {
         AV62Configuracionempresawwds_1_filterfulltext = AV16FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV27TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV28TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV29TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV30TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV31TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV32TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV33TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV34TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV45TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV46TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV47TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV48TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV49TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV50TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV51TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV52TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV57TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV58TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV59TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV60TFConfiguracionEmpresaCuotaLandingPage_To;
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
            gxgrGrid_refresh( subGrid_Rows, AV13OrderedBy, AV14OrderedDsc, AV16FilterFullText, AV27TFConfiguracionEmpresaId, AV28TFConfiguracionEmpresaId_To, AV26ManageFiltersExecutionStep, AV21ColumnsSelector, AV61Pgmname, AV29TFConfiguracionEmpresaTelefono, AV30TFConfiguracionEmpresaTelefono_Sel, AV31TFConfiguracionEmpresaCostoPlanBasico, AV32TFConfiguracionEmpresaCostoPlanBasico_To, AV33TFConfiguracionEmpresaCuotaPlanBasico, AV34TFConfiguracionEmpresaCuotaPlanBasico_To, AV45TFConfiguracionEmpresaCostoPlanSuperior, AV46TFConfiguracionEmpresaCostoPlanSuperior_To, AV47TFConfiguracionEmpresaCuotaPlanSuperior, AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, AV49TFConfiguracionEmpresaCostoPlanNegocios, AV50TFConfiguracionEmpresaCostoPlanNegocios_To, AV51TFConfiguracionEmpresaCuotaPlanNegocios, AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, AV57TFConfiguracionEmpresaCostoLandingPage, AV58TFConfiguracionEmpresaCostoLandingPage_To, AV59TFConfiguracionEmpresaCuotaLandingPage, AV60TFConfiguracionEmpresaCuotaLandingPage_To, AV53IsAuthorized_Display, AV54IsAuthorized_Update, AV55IsAuthorized_Delete, AV56IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV61Pgmname = "ConfiguracionEmpresaWW";
         edtConfiguracionEmpresaId_Enabled = 0;
         edtConfiguracionEmpresaTelefono_Enabled = 0;
         edtConfiguracionEmpresaCostoPlanB_Enabled = 0;
         edtConfiguracionEmpresaCuotaPlanB_Enabled = 0;
         edtConfiguracionEmpresaCostoPlanS_Enabled = 0;
         edtConfiguracionEmpresaCuotaPlanS_Enabled = 0;
         edtConfiguracionEmpresaCostoPlanN_Enabled = 0;
         edtConfiguracionEmpresaCuotaPlanN_Enabled = 0;
         edtConfiguracionEmpresaCostoLandi_Enabled = 0;
         edtConfiguracionEmpresaCuotaLandi_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3R0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E183R2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vMANAGEFILTERSDATA"), AV24ManageFiltersData);
            ajax_req_read_hidden_sdt(cgiGet( "vAGEXPORTDATA"), AV43AGExportData);
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
         E183R2 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
      }

      protected void E183R2( )
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
         AV43AGExportData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV44AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV44AGExportDataItem.gxTpr_Title = context.GetMessage( "WWP_ExportCaption", "");
         AV44AGExportDataItem.gxTpr_Icon = context.convertURL( (string)(context.GetImagePath( "da69a816-fd11-445b-8aaf-1a2f7f1acc93", "", context.GetTheme( ))));
         AV44AGExportDataItem.gxTpr_Eventkey = "Export";
         AV44AGExportDataItem.gxTpr_Isdivider = false;
         AV43AGExportData.Add(AV44AGExportDataItem, 0);
         AV44AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV44AGExportDataItem.gxTpr_Title = context.GetMessage( "WWP_ExportReportCaption", "");
         AV44AGExportDataItem.gxTpr_Icon = context.convertURL( (string)(context.GetImagePath( "776fb79c-a0a1-4302-b5e5-d773dbe1a297", "", context.GetTheme( ))));
         AV44AGExportDataItem.gxTpr_Eventkey = "ExportReport";
         AV44AGExportDataItem.gxTpr_Isdivider = false;
         AV43AGExportData.Add(AV44AGExportDataItem, 0);
         AV36GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV37GAMErrors);
         Ddo_grid_Gridinternalname = subGrid_Internalname;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GridInternalName", Ddo_grid_Gridinternalname);
         Ddo_grid_Gamoauthtoken = AV36GAMSession.gxTpr_Token;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GAMOAuthToken", Ddo_grid_Gamoauthtoken);
         Form.Caption = context.GetMessage( " Configuracion Empresa", "");
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

      protected void E193R2( )
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
         if ( StringUtil.StrCmp(AV23Session.Get("ConfiguracionEmpresaWWColumnsSelector"), "") != 0 )
         {
            AV19ColumnsSelectorXML = AV23Session.Get("ConfiguracionEmpresaWWColumnsSelector");
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
         edtConfiguracionEmpresaId_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(1)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaId_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaTelefono_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(2)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaTelefono_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaTelefono_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCostoPlanB_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(3)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCostoPlanB_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoPlanB_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCuotaPlanB_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(4)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCuotaPlanB_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaPlanB_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCostoPlanS_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(5)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCostoPlanS_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoPlanS_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCuotaPlanS_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(6)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCuotaPlanS_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaPlanS_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCostoPlanN_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(7)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCostoPlanN_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoPlanN_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCuotaPlanN_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(8)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCuotaPlanN_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaPlanN_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCostoLandi_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(9)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCostoLandi_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoLandi_Visible), 5, 0), !bGXsfl_44_Refreshing);
         edtConfiguracionEmpresaCuotaLandi_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV21ColumnsSelector.gxTpr_Columns.Item(10)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtConfiguracionEmpresaCuotaLandi_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaLandi_Visible), 5, 0), !bGXsfl_44_Refreshing);
         AV39GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri("", false, "AV39GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV39GridCurrentPage), 10, 0));
         AV40GridPageCount = subGrid_fnc_Pagecount( );
         AssignAttri("", false, "AV40GridPageCount", StringUtil.LTrimStr( (decimal)(AV40GridPageCount), 10, 0));
         GXt_char2 = AV41GridAppliedFilters;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getappliedfiltersdescription(context ).execute(  AV61Pgmname, out  GXt_char2) ;
         AV41GridAppliedFilters = GXt_char2;
         AssignAttri("", false, "AV41GridAppliedFilters", AV41GridAppliedFilters);
         AV62Configuracionempresawwds_1_filterfulltext = AV16FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV27TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV28TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV29TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV30TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV31TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV32TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV33TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV34TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV45TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV46TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV47TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV48TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV49TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV50TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV51TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV52TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV57TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV58TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV59TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV60TFConfiguracionEmpresaCuotaLandingPage_To;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E123R2( )
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

      protected void E133R2( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      protected void E153R2( )
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
            if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaId") == 0 )
            {
               AV27TFConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtext_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV27TFConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(AV27TFConfiguracionEmpresaId), 4, 0));
               AV28TFConfiguracionEmpresaId_To = (short)(Math.Round(NumberUtil.Val( Ddo_grid_Filteredtextto_get, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV28TFConfiguracionEmpresaId_To", StringUtil.LTrimStr( (decimal)(AV28TFConfiguracionEmpresaId_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaTelefono") == 0 )
            {
               AV29TFConfiguracionEmpresaTelefono = Ddo_grid_Filteredtext_get;
               AssignAttri("", false, "AV29TFConfiguracionEmpresaTelefono", AV29TFConfiguracionEmpresaTelefono);
               AV30TFConfiguracionEmpresaTelefono_Sel = Ddo_grid_Selectedvalue_get;
               AssignAttri("", false, "AV30TFConfiguracionEmpresaTelefono_Sel", AV30TFConfiguracionEmpresaTelefono_Sel);
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCostoPlanBasico") == 0 )
            {
               AV31TFConfiguracionEmpresaCostoPlanBasico = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV31TFConfiguracionEmpresaCostoPlanBasico", StringUtil.LTrimStr( AV31TFConfiguracionEmpresaCostoPlanBasico, 12, 2));
               AV32TFConfiguracionEmpresaCostoPlanBasico_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV32TFConfiguracionEmpresaCostoPlanBasico_To", StringUtil.LTrimStr( AV32TFConfiguracionEmpresaCostoPlanBasico_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCuotaPlanBasico") == 0 )
            {
               AV33TFConfiguracionEmpresaCuotaPlanBasico = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV33TFConfiguracionEmpresaCuotaPlanBasico", StringUtil.LTrimStr( AV33TFConfiguracionEmpresaCuotaPlanBasico, 12, 2));
               AV34TFConfiguracionEmpresaCuotaPlanBasico_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV34TFConfiguracionEmpresaCuotaPlanBasico_To", StringUtil.LTrimStr( AV34TFConfiguracionEmpresaCuotaPlanBasico_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCostoPlanSuperior") == 0 )
            {
               AV45TFConfiguracionEmpresaCostoPlanSuperior = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV45TFConfiguracionEmpresaCostoPlanSuperior", StringUtil.LTrimStr( AV45TFConfiguracionEmpresaCostoPlanSuperior, 12, 2));
               AV46TFConfiguracionEmpresaCostoPlanSuperior_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV46TFConfiguracionEmpresaCostoPlanSuperior_To", StringUtil.LTrimStr( AV46TFConfiguracionEmpresaCostoPlanSuperior_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCuotaPlanSuperior") == 0 )
            {
               AV47TFConfiguracionEmpresaCuotaPlanSuperior = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV47TFConfiguracionEmpresaCuotaPlanSuperior", StringUtil.LTrimStr( AV47TFConfiguracionEmpresaCuotaPlanSuperior, 12, 2));
               AV48TFConfiguracionEmpresaCuotaPlanSuperior_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV48TFConfiguracionEmpresaCuotaPlanSuperior_To", StringUtil.LTrimStr( AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCostoPlanNegocios") == 0 )
            {
               AV49TFConfiguracionEmpresaCostoPlanNegocios = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV49TFConfiguracionEmpresaCostoPlanNegocios", StringUtil.LTrimStr( AV49TFConfiguracionEmpresaCostoPlanNegocios, 12, 2));
               AV50TFConfiguracionEmpresaCostoPlanNegocios_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV50TFConfiguracionEmpresaCostoPlanNegocios_To", StringUtil.LTrimStr( AV50TFConfiguracionEmpresaCostoPlanNegocios_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCuotaPlanNegocios") == 0 )
            {
               AV51TFConfiguracionEmpresaCuotaPlanNegocios = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV51TFConfiguracionEmpresaCuotaPlanNegocios", StringUtil.LTrimStr( AV51TFConfiguracionEmpresaCuotaPlanNegocios, 12, 2));
               AV52TFConfiguracionEmpresaCuotaPlanNegocios_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV52TFConfiguracionEmpresaCuotaPlanNegocios_To", StringUtil.LTrimStr( AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCostoLandingPage") == 0 )
            {
               AV57TFConfiguracionEmpresaCostoLandingPage = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV57TFConfiguracionEmpresaCostoLandingPage", StringUtil.LTrimStr( AV57TFConfiguracionEmpresaCostoLandingPage, 12, 2));
               AV58TFConfiguracionEmpresaCostoLandingPage_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV58TFConfiguracionEmpresaCostoLandingPage_To", StringUtil.LTrimStr( AV58TFConfiguracionEmpresaCostoLandingPage_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(Ddo_grid_Selectedcolumn, "ConfiguracionEmpresaCuotaLandingPage") == 0 )
            {
               AV59TFConfiguracionEmpresaCuotaLandingPage = NumberUtil.Val( Ddo_grid_Filteredtext_get, ".");
               AssignAttri("", false, "AV59TFConfiguracionEmpresaCuotaLandingPage", StringUtil.LTrimStr( AV59TFConfiguracionEmpresaCuotaLandingPage, 12, 2));
               AV60TFConfiguracionEmpresaCuotaLandingPage_To = NumberUtil.Val( Ddo_grid_Filteredtextto_get, ".");
               AssignAttri("", false, "AV60TFConfiguracionEmpresaCuotaLandingPage_To", StringUtil.LTrimStr( AV60TFConfiguracionEmpresaCuotaLandingPage_To, 12, 2));
            }
            subgrid_firstpage( ) ;
         }
         /*  Sending Event outputs  */
      }

      private void E203R2( )
      {
         /* Grid_Load Routine */
         returnInSub = false;
         cmbavGridactions.removeAllItems();
         cmbavGridactions.addItem("0", ";fa fa-bars", 0);
         if ( AV53IsAuthorized_Display )
         {
            cmbavGridactions.addItem("1", StringUtil.Format( "%1;%2", context.GetMessage( "GXM_display", ""), "fa fa-search", "", "", "", "", "", "", ""), 0);
         }
         if ( AV54IsAuthorized_Update )
         {
            cmbavGridactions.addItem("2", StringUtil.Format( "%1;%2", context.GetMessage( "GXM_update", ""), "fa fa-pen", "", "", "", "", "", "", ""), 0);
         }
         if ( AV55IsAuthorized_Delete )
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

      protected void E163R2( )
      {
         /* Ddo_gridcolumnsselector_Oncolumnschanged Routine */
         returnInSub = false;
         AV19ColumnsSelectorXML = Ddo_gridcolumnsselector_Columnsselectorvalues;
         AV21ColumnsSelector.FromJSonString(AV19ColumnsSelectorXML, null);
         new DesignSystem.Programs.wwpbaseobjects.savecolumnsselectorstate(context ).execute(  "ConfiguracionEmpresaWWColumnsSelector",  (String.IsNullOrEmpty(StringUtil.RTrim( AV19ColumnsSelectorXML)) ? "" : AV21ColumnsSelector.ToXml(false, true, "", ""))) ;
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV21ColumnsSelector", AV21ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV24ManageFiltersData", AV24ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV11GridState", AV11GridState);
      }

      protected void E113R2( )
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
            GXEncryptionTmp = "wwpbaseobjects.savefilteras.aspx"+UrlEncode(StringUtil.RTrim("ConfiguracionEmpresaWWFilters")) + "," + UrlEncode(StringUtil.RTrim(AV61Pgmname+"GridState"));
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
            GXEncryptionTmp = "wwpbaseobjects.managefilters.aspx"+UrlEncode(StringUtil.RTrim("ConfiguracionEmpresaWWFilters"));
            context.PopUp(formatLink("wwpbaseobjects.managefilters.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV26ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV26ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV26ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else
         {
            GXt_char2 = AV25ManageFiltersXml;
            new DesignSystem.Programs.wwpbaseobjects.getfilterbyname(context ).execute(  "ConfiguracionEmpresaWWFilters",  Ddo_managefilters_Activeeventkey, out  GXt_char2) ;
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
               new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV61Pgmname+"GridState",  AV25ManageFiltersXml) ;
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

      protected void E213R2( )
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

      protected void E173R2( )
      {
         /* 'DoInsert' Routine */
         returnInSub = false;
         if ( AV56IsAuthorized_Insert )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "configuracionempresa.aspx"+UrlEncode(StringUtil.RTrim("INS")) + "," + UrlEncode(StringUtil.LTrimStr(0,1,0));
            CallWebObject(formatLink("configuracionempresa.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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

      protected void E143R2( )
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
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaId",  "",  "Id",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaTelefono",  "",  "Telefono",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCostoPlanBasico",  "",  "Plan Basico",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCuotaPlanBasico",  "",  "Plan Basico",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCostoPlanSuperior",  "",  "Plan Superior",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCuotaPlanSuperior",  "",  "Plan Superior",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCostoPlanNegocios",  "",  "Plan Negocios",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCuotaPlanNegocios",  "",  "Plan Negocios",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCostoLandingPage",  "",  "Landing Page",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV21ColumnsSelector,  "ConfiguracionEmpresaCuotaLandingPage",  "",  "Landing Page",  true,  "") ;
         GXt_char2 = AV20UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "ConfiguracionEmpresaWWColumnsSelector", out  GXt_char2) ;
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
         GXt_boolean3 = AV53IsAuthorized_Display;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "configuracionempresa_Execute", out  GXt_boolean3) ;
         AV53IsAuthorized_Display = GXt_boolean3;
         AssignAttri("", false, "AV53IsAuthorized_Display", AV53IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV53IsAuthorized_Display, context));
         GXt_boolean3 = AV54IsAuthorized_Update;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "configuracionempresa_Update", out  GXt_boolean3) ;
         AV54IsAuthorized_Update = GXt_boolean3;
         AssignAttri("", false, "AV54IsAuthorized_Update", AV54IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV54IsAuthorized_Update, context));
         GXt_boolean3 = AV55IsAuthorized_Delete;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "configuracionempresa_Delete", out  GXt_boolean3) ;
         AV55IsAuthorized_Delete = GXt_boolean3;
         AssignAttri("", false, "AV55IsAuthorized_Delete", AV55IsAuthorized_Delete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DELETE", GetSecureSignedToken( "", AV55IsAuthorized_Delete, context));
         GXt_boolean3 = AV56IsAuthorized_Insert;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "configuracionempresa_Insert", out  GXt_boolean3) ;
         AV56IsAuthorized_Insert = GXt_boolean3;
         AssignAttri("", false, "AV56IsAuthorized_Insert", AV56IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV56IsAuthorized_Insert, context));
         if ( ! ( AV56IsAuthorized_Insert ) )
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
         new DesignSystem.Programs.wwpbaseobjects.wwp_managefiltersloadsavedfilters(context ).execute(  "ConfiguracionEmpresaWWFilters",  "",  "",  false, out  GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4) ;
         AV24ManageFiltersData = GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4;
      }

      protected void S182( )
      {
         /* 'CLEANFILTERS' Routine */
         returnInSub = false;
         AV16FilterFullText = "";
         AssignAttri("", false, "AV16FilterFullText", AV16FilterFullText);
         AV27TFConfiguracionEmpresaId = 0;
         AssignAttri("", false, "AV27TFConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(AV27TFConfiguracionEmpresaId), 4, 0));
         AV28TFConfiguracionEmpresaId_To = 0;
         AssignAttri("", false, "AV28TFConfiguracionEmpresaId_To", StringUtil.LTrimStr( (decimal)(AV28TFConfiguracionEmpresaId_To), 4, 0));
         AV29TFConfiguracionEmpresaTelefono = "";
         AssignAttri("", false, "AV29TFConfiguracionEmpresaTelefono", AV29TFConfiguracionEmpresaTelefono);
         AV30TFConfiguracionEmpresaTelefono_Sel = "";
         AssignAttri("", false, "AV30TFConfiguracionEmpresaTelefono_Sel", AV30TFConfiguracionEmpresaTelefono_Sel);
         AV31TFConfiguracionEmpresaCostoPlanBasico = 0;
         AssignAttri("", false, "AV31TFConfiguracionEmpresaCostoPlanBasico", StringUtil.LTrimStr( AV31TFConfiguracionEmpresaCostoPlanBasico, 12, 2));
         AV32TFConfiguracionEmpresaCostoPlanBasico_To = 0;
         AssignAttri("", false, "AV32TFConfiguracionEmpresaCostoPlanBasico_To", StringUtil.LTrimStr( AV32TFConfiguracionEmpresaCostoPlanBasico_To, 12, 2));
         AV33TFConfiguracionEmpresaCuotaPlanBasico = 0;
         AssignAttri("", false, "AV33TFConfiguracionEmpresaCuotaPlanBasico", StringUtil.LTrimStr( AV33TFConfiguracionEmpresaCuotaPlanBasico, 12, 2));
         AV34TFConfiguracionEmpresaCuotaPlanBasico_To = 0;
         AssignAttri("", false, "AV34TFConfiguracionEmpresaCuotaPlanBasico_To", StringUtil.LTrimStr( AV34TFConfiguracionEmpresaCuotaPlanBasico_To, 12, 2));
         AV45TFConfiguracionEmpresaCostoPlanSuperior = 0;
         AssignAttri("", false, "AV45TFConfiguracionEmpresaCostoPlanSuperior", StringUtil.LTrimStr( AV45TFConfiguracionEmpresaCostoPlanSuperior, 12, 2));
         AV46TFConfiguracionEmpresaCostoPlanSuperior_To = 0;
         AssignAttri("", false, "AV46TFConfiguracionEmpresaCostoPlanSuperior_To", StringUtil.LTrimStr( AV46TFConfiguracionEmpresaCostoPlanSuperior_To, 12, 2));
         AV47TFConfiguracionEmpresaCuotaPlanSuperior = 0;
         AssignAttri("", false, "AV47TFConfiguracionEmpresaCuotaPlanSuperior", StringUtil.LTrimStr( AV47TFConfiguracionEmpresaCuotaPlanSuperior, 12, 2));
         AV48TFConfiguracionEmpresaCuotaPlanSuperior_To = 0;
         AssignAttri("", false, "AV48TFConfiguracionEmpresaCuotaPlanSuperior_To", StringUtil.LTrimStr( AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, 12, 2));
         AV49TFConfiguracionEmpresaCostoPlanNegocios = 0;
         AssignAttri("", false, "AV49TFConfiguracionEmpresaCostoPlanNegocios", StringUtil.LTrimStr( AV49TFConfiguracionEmpresaCostoPlanNegocios, 12, 2));
         AV50TFConfiguracionEmpresaCostoPlanNegocios_To = 0;
         AssignAttri("", false, "AV50TFConfiguracionEmpresaCostoPlanNegocios_To", StringUtil.LTrimStr( AV50TFConfiguracionEmpresaCostoPlanNegocios_To, 12, 2));
         AV51TFConfiguracionEmpresaCuotaPlanNegocios = 0;
         AssignAttri("", false, "AV51TFConfiguracionEmpresaCuotaPlanNegocios", StringUtil.LTrimStr( AV51TFConfiguracionEmpresaCuotaPlanNegocios, 12, 2));
         AV52TFConfiguracionEmpresaCuotaPlanNegocios_To = 0;
         AssignAttri("", false, "AV52TFConfiguracionEmpresaCuotaPlanNegocios_To", StringUtil.LTrimStr( AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, 12, 2));
         AV57TFConfiguracionEmpresaCostoLandingPage = 0;
         AssignAttri("", false, "AV57TFConfiguracionEmpresaCostoLandingPage", StringUtil.LTrimStr( AV57TFConfiguracionEmpresaCostoLandingPage, 12, 2));
         AV58TFConfiguracionEmpresaCostoLandingPage_To = 0;
         AssignAttri("", false, "AV58TFConfiguracionEmpresaCostoLandingPage_To", StringUtil.LTrimStr( AV58TFConfiguracionEmpresaCostoLandingPage_To, 12, 2));
         AV59TFConfiguracionEmpresaCuotaLandingPage = 0;
         AssignAttri("", false, "AV59TFConfiguracionEmpresaCuotaLandingPage", StringUtil.LTrimStr( AV59TFConfiguracionEmpresaCuotaLandingPage, 12, 2));
         AV60TFConfiguracionEmpresaCuotaLandingPage_To = 0;
         AssignAttri("", false, "AV60TFConfiguracionEmpresaCuotaLandingPage_To", StringUtil.LTrimStr( AV60TFConfiguracionEmpresaCuotaLandingPage_To, 12, 2));
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
         if ( AV53IsAuthorized_Display )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "configuracionempresa.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.LTrimStr(A44ConfiguracionEmpresaId,4,0));
            CallWebObject(formatLink("configuracionempresa.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         if ( AV54IsAuthorized_Update )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "configuracionempresa.aspx"+UrlEncode(StringUtil.RTrim("UPD")) + "," + UrlEncode(StringUtil.LTrimStr(A44ConfiguracionEmpresaId,4,0));
            CallWebObject(formatLink("configuracionempresa.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         if ( AV55IsAuthorized_Delete )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "configuracionempresa.aspx"+UrlEncode(StringUtil.RTrim("DLT")) + "," + UrlEncode(StringUtil.LTrimStr(A44ConfiguracionEmpresaId,4,0));
            CallWebObject(formatLink("configuracionempresa.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         if ( StringUtil.StrCmp(AV23Session.Get(AV61Pgmname+"GridState"), "") == 0 )
         {
            AV11GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  AV61Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV11GridState.FromXml(AV23Session.Get(AV61Pgmname+"GridState"), null, "", "");
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
         AV83GXV1 = 1;
         while ( AV83GXV1 <= AV11GridState.gxTpr_Filtervalues.Count )
         {
            AV12GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV11GridState.gxTpr_Filtervalues.Item(AV83GXV1));
            if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV16FilterFullText = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV16FilterFullText", AV16FilterFullText);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESAID") == 0 )
            {
               AV27TFConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV27TFConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(AV27TFConfiguracionEmpresaId), 4, 0));
               AV28TFConfiguracionEmpresaId_To = (short)(Math.Round(NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV28TFConfiguracionEmpresaId_To", StringUtil.LTrimStr( (decimal)(AV28TFConfiguracionEmpresaId_To), 4, 0));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO") == 0 )
            {
               AV29TFConfiguracionEmpresaTelefono = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV29TFConfiguracionEmpresaTelefono", AV29TFConfiguracionEmpresaTelefono);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO_SEL") == 0 )
            {
               AV30TFConfiguracionEmpresaTelefono_Sel = AV12GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV30TFConfiguracionEmpresaTelefono_Sel", AV30TFConfiguracionEmpresaTelefono_Sel);
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANBASICO") == 0 )
            {
               AV31TFConfiguracionEmpresaCostoPlanBasico = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV31TFConfiguracionEmpresaCostoPlanBasico", StringUtil.LTrimStr( AV31TFConfiguracionEmpresaCostoPlanBasico, 12, 2));
               AV32TFConfiguracionEmpresaCostoPlanBasico_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV32TFConfiguracionEmpresaCostoPlanBasico_To", StringUtil.LTrimStr( AV32TFConfiguracionEmpresaCostoPlanBasico_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANBASICO") == 0 )
            {
               AV33TFConfiguracionEmpresaCuotaPlanBasico = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV33TFConfiguracionEmpresaCuotaPlanBasico", StringUtil.LTrimStr( AV33TFConfiguracionEmpresaCuotaPlanBasico, 12, 2));
               AV34TFConfiguracionEmpresaCuotaPlanBasico_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV34TFConfiguracionEmpresaCuotaPlanBasico_To", StringUtil.LTrimStr( AV34TFConfiguracionEmpresaCuotaPlanBasico_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR") == 0 )
            {
               AV45TFConfiguracionEmpresaCostoPlanSuperior = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV45TFConfiguracionEmpresaCostoPlanSuperior", StringUtil.LTrimStr( AV45TFConfiguracionEmpresaCostoPlanSuperior, 12, 2));
               AV46TFConfiguracionEmpresaCostoPlanSuperior_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV46TFConfiguracionEmpresaCostoPlanSuperior_To", StringUtil.LTrimStr( AV46TFConfiguracionEmpresaCostoPlanSuperior_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR") == 0 )
            {
               AV47TFConfiguracionEmpresaCuotaPlanSuperior = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV47TFConfiguracionEmpresaCuotaPlanSuperior", StringUtil.LTrimStr( AV47TFConfiguracionEmpresaCuotaPlanSuperior, 12, 2));
               AV48TFConfiguracionEmpresaCuotaPlanSuperior_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV48TFConfiguracionEmpresaCuotaPlanSuperior_To", StringUtil.LTrimStr( AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS") == 0 )
            {
               AV49TFConfiguracionEmpresaCostoPlanNegocios = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV49TFConfiguracionEmpresaCostoPlanNegocios", StringUtil.LTrimStr( AV49TFConfiguracionEmpresaCostoPlanNegocios, 12, 2));
               AV50TFConfiguracionEmpresaCostoPlanNegocios_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV50TFConfiguracionEmpresaCostoPlanNegocios_To", StringUtil.LTrimStr( AV50TFConfiguracionEmpresaCostoPlanNegocios_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS") == 0 )
            {
               AV51TFConfiguracionEmpresaCuotaPlanNegocios = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV51TFConfiguracionEmpresaCuotaPlanNegocios", StringUtil.LTrimStr( AV51TFConfiguracionEmpresaCuotaPlanNegocios, 12, 2));
               AV52TFConfiguracionEmpresaCuotaPlanNegocios_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV52TFConfiguracionEmpresaCuotaPlanNegocios_To", StringUtil.LTrimStr( AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOLANDINGPAGE") == 0 )
            {
               AV57TFConfiguracionEmpresaCostoLandingPage = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV57TFConfiguracionEmpresaCostoLandingPage", StringUtil.LTrimStr( AV57TFConfiguracionEmpresaCostoLandingPage, 12, 2));
               AV58TFConfiguracionEmpresaCostoLandingPage_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV58TFConfiguracionEmpresaCostoLandingPage_To", StringUtil.LTrimStr( AV58TFConfiguracionEmpresaCostoLandingPage_To, 12, 2));
            }
            else if ( StringUtil.StrCmp(AV12GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTALANDINGPAGE") == 0 )
            {
               AV59TFConfiguracionEmpresaCuotaLandingPage = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Value, ".");
               AssignAttri("", false, "AV59TFConfiguracionEmpresaCuotaLandingPage", StringUtil.LTrimStr( AV59TFConfiguracionEmpresaCuotaLandingPage, 12, 2));
               AV60TFConfiguracionEmpresaCuotaLandingPage_To = NumberUtil.Val( AV12GridStateFilterValue.gxTpr_Valueto, ".");
               AssignAttri("", false, "AV60TFConfiguracionEmpresaCuotaLandingPage_To", StringUtil.LTrimStr( AV60TFConfiguracionEmpresaCuotaLandingPage_To, 12, 2));
            }
            AV83GXV1 = (int)(AV83GXV1+1);
         }
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV30TFConfiguracionEmpresaTelefono_Sel)),  AV30TFConfiguracionEmpresaTelefono_Sel, out  GXt_char2) ;
         Ddo_grid_Selectedvalue_set = "|"+GXt_char2+"||||||||";
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "SelectedValue_set", Ddo_grid_Selectedvalue_set);
         GXt_char2 = "";
         new DesignSystem.Programs.wwpbaseobjects.wwp_getfilterval(context ).execute(  String.IsNullOrEmpty(StringUtil.RTrim( AV29TFConfiguracionEmpresaTelefono)),  AV29TFConfiguracionEmpresaTelefono, out  GXt_char2) ;
         Ddo_grid_Filteredtext_set = ((0==AV27TFConfiguracionEmpresaId) ? "" : StringUtil.Str( (decimal)(AV27TFConfiguracionEmpresaId), 4, 0))+"|"+GXt_char2+"|"+((Convert.ToDecimal(0)==AV31TFConfiguracionEmpresaCostoPlanBasico) ? "" : StringUtil.Str( AV31TFConfiguracionEmpresaCostoPlanBasico, 12, 2))+"|"+((Convert.ToDecimal(0)==AV33TFConfiguracionEmpresaCuotaPlanBasico) ? "" : StringUtil.Str( AV33TFConfiguracionEmpresaCuotaPlanBasico, 12, 2))+"|"+((Convert.ToDecimal(0)==AV45TFConfiguracionEmpresaCostoPlanSuperior) ? "" : StringUtil.Str( AV45TFConfiguracionEmpresaCostoPlanSuperior, 12, 2))+"|"+((Convert.ToDecimal(0)==AV47TFConfiguracionEmpresaCuotaPlanSuperior) ? "" : StringUtil.Str( AV47TFConfiguracionEmpresaCuotaPlanSuperior, 12, 2))+"|"+((Convert.ToDecimal(0)==AV49TFConfiguracionEmpresaCostoPlanNegocios) ? "" : StringUtil.Str( AV49TFConfiguracionEmpresaCostoPlanNegocios, 12, 2))+"|"+((Convert.ToDecimal(0)==AV51TFConfiguracionEmpresaCuotaPlanNegocios) ? "" : StringUtil.Str( AV51TFConfiguracionEmpresaCuotaPlanNegocios, 12, 2))+"|"+((Convert.ToDecimal(0)==AV57TFConfiguracionEmpresaCostoLandingPage) ? "" : StringUtil.Str( AV57TFConfiguracionEmpresaCostoLandingPage, 12, 2))+"|"+((Convert.ToDecimal(0)==AV59TFConfiguracionEmpresaCuotaLandingPage) ? "" : StringUtil.Str( AV59TFConfiguracionEmpresaCuotaLandingPage, 12, 2));
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredText_set", Ddo_grid_Filteredtext_set);
         Ddo_grid_Filteredtextto_set = ((0==AV28TFConfiguracionEmpresaId_To) ? "" : StringUtil.Str( (decimal)(AV28TFConfiguracionEmpresaId_To), 4, 0))+"||"+((Convert.ToDecimal(0)==AV32TFConfiguracionEmpresaCostoPlanBasico_To) ? "" : StringUtil.Str( AV32TFConfiguracionEmpresaCostoPlanBasico_To, 12, 2))+"|"+((Convert.ToDecimal(0)==AV34TFConfiguracionEmpresaCuotaPlanBasico_To) ? "" : StringUtil.Str( AV34TFConfiguracionEmpresaCuotaPlanBasico_To, 12, 2))+"|"+((Convert.ToDecimal(0)==AV46TFConfiguracionEmpresaCostoPlanSuperior_To) ? "" : StringUtil.Str( AV46TFConfiguracionEmpresaCostoPlanSuperior_To, 12, 2))+"|"+((Convert.ToDecimal(0)==AV48TFConfiguracionEmpresaCuotaPlanSuperior_To) ? "" : StringUtil.Str( AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, 12, 2))+"|"+((Convert.ToDecimal(0)==AV50TFConfiguracionEmpresaCostoPlanNegocios_To) ? "" : StringUtil.Str( AV50TFConfiguracionEmpresaCostoPlanNegocios_To, 12, 2))+"|"+((Convert.ToDecimal(0)==AV52TFConfiguracionEmpresaCuotaPlanNegocios_To) ? "" : StringUtil.Str( AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, 12, 2))+"|"+((Convert.ToDecimal(0)==AV58TFConfiguracionEmpresaCostoLandingPage_To) ? "" : StringUtil.Str( AV58TFConfiguracionEmpresaCostoLandingPage_To, 12, 2))+"|"+((Convert.ToDecimal(0)==AV60TFConfiguracionEmpresaCuotaLandingPage_To) ? "" : StringUtil.Str( AV60TFConfiguracionEmpresaCuotaLandingPage_To, 12, 2));
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "FilteredTextTo_set", Ddo_grid_Filteredtextto_set);
      }

      protected void S162( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV11GridState.FromXml(AV23Session.Get(AV61Pgmname+"GridState"), null, "", "");
         AV11GridState.gxTpr_Orderedby = AV13OrderedBy;
         AV11GridState.gxTpr_Ordereddsc = AV14OrderedDsc;
         AV11GridState.gxTpr_Filtervalues.Clear();
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "FILTERFULLTEXT",  context.GetMessage( "WWP_FullTextFilterDescription", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV16FilterFullText)),  0,  AV16FilterFullText,  AV16FilterFullText,  false,  "",  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESAID",  context.GetMessage( "Id", ""),  !((0==AV27TFConfiguracionEmpresaId)&&(0==AV28TFConfiguracionEmpresaId_To)),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV27TFConfiguracionEmpresaId), 4, 0)),  ((0==AV27TFConfiguracionEmpresaId) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV27TFConfiguracionEmpresaId), "ZZZ9"))),  true,  StringUtil.Trim( StringUtil.Str( (decimal)(AV28TFConfiguracionEmpresaId_To), 4, 0)),  ((0==AV28TFConfiguracionEmpresaId_To) ? "" : StringUtil.Trim( context.localUtil.Format( (decimal)(AV28TFConfiguracionEmpresaId_To), "ZZZ9")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalueandsel(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESATELEFONO",  context.GetMessage( "Telefono", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV29TFConfiguracionEmpresaTelefono)),  0,  AV29TFConfiguracionEmpresaTelefono,  AV29TFConfiguracionEmpresaTelefono,  false,  "",  "",  !String.IsNullOrEmpty(StringUtil.RTrim( AV30TFConfiguracionEmpresaTelefono_Sel)),  AV30TFConfiguracionEmpresaTelefono_Sel,  AV30TFConfiguracionEmpresaTelefono_Sel) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACOSTOPLANBASICO",  context.GetMessage( "Plan Basico", ""),  !((Convert.ToDecimal(0)==AV31TFConfiguracionEmpresaCostoPlanBasico)&&(Convert.ToDecimal(0)==AV32TFConfiguracionEmpresaCostoPlanBasico_To)),  0,  StringUtil.Trim( StringUtil.Str( AV31TFConfiguracionEmpresaCostoPlanBasico, 12, 2)),  ((Convert.ToDecimal(0)==AV31TFConfiguracionEmpresaCostoPlanBasico) ? "" : StringUtil.Trim( context.localUtil.Format( AV31TFConfiguracionEmpresaCostoPlanBasico, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV32TFConfiguracionEmpresaCostoPlanBasico_To, 12, 2)),  ((Convert.ToDecimal(0)==AV32TFConfiguracionEmpresaCostoPlanBasico_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV32TFConfiguracionEmpresaCostoPlanBasico_To, "ZZZZZZZZ9.99")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACUOTAPLANBASICO",  context.GetMessage( "Plan Basico", ""),  !((Convert.ToDecimal(0)==AV33TFConfiguracionEmpresaCuotaPlanBasico)&&(Convert.ToDecimal(0)==AV34TFConfiguracionEmpresaCuotaPlanBasico_To)),  0,  StringUtil.Trim( StringUtil.Str( AV33TFConfiguracionEmpresaCuotaPlanBasico, 12, 2)),  ((Convert.ToDecimal(0)==AV33TFConfiguracionEmpresaCuotaPlanBasico) ? "" : StringUtil.Trim( context.localUtil.Format( AV33TFConfiguracionEmpresaCuotaPlanBasico, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV34TFConfiguracionEmpresaCuotaPlanBasico_To, 12, 2)),  ((Convert.ToDecimal(0)==AV34TFConfiguracionEmpresaCuotaPlanBasico_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV34TFConfiguracionEmpresaCuotaPlanBasico_To, "ZZZZZZZZ9.99")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR",  context.GetMessage( "Plan Superior", ""),  !((Convert.ToDecimal(0)==AV45TFConfiguracionEmpresaCostoPlanSuperior)&&(Convert.ToDecimal(0)==AV46TFConfiguracionEmpresaCostoPlanSuperior_To)),  0,  StringUtil.Trim( StringUtil.Str( AV45TFConfiguracionEmpresaCostoPlanSuperior, 12, 2)),  ((Convert.ToDecimal(0)==AV45TFConfiguracionEmpresaCostoPlanSuperior) ? "" : StringUtil.Trim( context.localUtil.Format( AV45TFConfiguracionEmpresaCostoPlanSuperior, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV46TFConfiguracionEmpresaCostoPlanSuperior_To, 12, 2)),  ((Convert.ToDecimal(0)==AV46TFConfiguracionEmpresaCostoPlanSuperior_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV46TFConfiguracionEmpresaCostoPlanSuperior_To, "ZZZZZZZZ9.99")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR",  context.GetMessage( "Plan Superior", ""),  !((Convert.ToDecimal(0)==AV47TFConfiguracionEmpresaCuotaPlanSuperior)&&(Convert.ToDecimal(0)==AV48TFConfiguracionEmpresaCuotaPlanSuperior_To)),  0,  StringUtil.Trim( StringUtil.Str( AV47TFConfiguracionEmpresaCuotaPlanSuperior, 12, 2)),  ((Convert.ToDecimal(0)==AV47TFConfiguracionEmpresaCuotaPlanSuperior) ? "" : StringUtil.Trim( context.localUtil.Format( AV47TFConfiguracionEmpresaCuotaPlanSuperior, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, 12, 2)),  ((Convert.ToDecimal(0)==AV48TFConfiguracionEmpresaCuotaPlanSuperior_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV48TFConfiguracionEmpresaCuotaPlanSuperior_To, "ZZZZZZZZ9.99")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS",  context.GetMessage( "Plan Negocios", ""),  !((Convert.ToDecimal(0)==AV49TFConfiguracionEmpresaCostoPlanNegocios)&&(Convert.ToDecimal(0)==AV50TFConfiguracionEmpresaCostoPlanNegocios_To)),  0,  StringUtil.Trim( StringUtil.Str( AV49TFConfiguracionEmpresaCostoPlanNegocios, 12, 2)),  ((Convert.ToDecimal(0)==AV49TFConfiguracionEmpresaCostoPlanNegocios) ? "" : StringUtil.Trim( context.localUtil.Format( AV49TFConfiguracionEmpresaCostoPlanNegocios, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV50TFConfiguracionEmpresaCostoPlanNegocios_To, 12, 2)),  ((Convert.ToDecimal(0)==AV50TFConfiguracionEmpresaCostoPlanNegocios_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV50TFConfiguracionEmpresaCostoPlanNegocios_To, "ZZZZZZZZ9.99")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS",  context.GetMessage( "Plan Negocios", ""),  !((Convert.ToDecimal(0)==AV51TFConfiguracionEmpresaCuotaPlanNegocios)&&(Convert.ToDecimal(0)==AV52TFConfiguracionEmpresaCuotaPlanNegocios_To)),  0,  StringUtil.Trim( StringUtil.Str( AV51TFConfiguracionEmpresaCuotaPlanNegocios, 12, 2)),  ((Convert.ToDecimal(0)==AV51TFConfiguracionEmpresaCuotaPlanNegocios) ? "" : StringUtil.Trim( context.localUtil.Format( AV51TFConfiguracionEmpresaCuotaPlanNegocios, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, 12, 2)),  ((Convert.ToDecimal(0)==AV52TFConfiguracionEmpresaCuotaPlanNegocios_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV52TFConfiguracionEmpresaCuotaPlanNegocios_To, "ZZZZZZZZ9.99")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACOSTOLANDINGPAGE",  context.GetMessage( "Landing Page", ""),  !((Convert.ToDecimal(0)==AV57TFConfiguracionEmpresaCostoLandingPage)&&(Convert.ToDecimal(0)==AV58TFConfiguracionEmpresaCostoLandingPage_To)),  0,  StringUtil.Trim( StringUtil.Str( AV57TFConfiguracionEmpresaCostoLandingPage, 12, 2)),  ((Convert.ToDecimal(0)==AV57TFConfiguracionEmpresaCostoLandingPage) ? "" : StringUtil.Trim( context.localUtil.Format( AV57TFConfiguracionEmpresaCostoLandingPage, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV58TFConfiguracionEmpresaCostoLandingPage_To, 12, 2)),  ((Convert.ToDecimal(0)==AV58TFConfiguracionEmpresaCostoLandingPage_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV58TFConfiguracionEmpresaCostoLandingPage_To, "ZZZZZZZZ9.99")))) ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV11GridState,  "TFCONFIGURACIONEMPRESACUOTALANDINGPAGE",  context.GetMessage( "Landing Page", ""),  !((Convert.ToDecimal(0)==AV59TFConfiguracionEmpresaCuotaLandingPage)&&(Convert.ToDecimal(0)==AV60TFConfiguracionEmpresaCuotaLandingPage_To)),  0,  StringUtil.Trim( StringUtil.Str( AV59TFConfiguracionEmpresaCuotaLandingPage, 12, 2)),  ((Convert.ToDecimal(0)==AV59TFConfiguracionEmpresaCuotaLandingPage) ? "" : StringUtil.Trim( context.localUtil.Format( AV59TFConfiguracionEmpresaCuotaLandingPage, "ZZZZZZZZ9.99"))),  true,  StringUtil.Trim( StringUtil.Str( AV60TFConfiguracionEmpresaCuotaLandingPage_To, 12, 2)),  ((Convert.ToDecimal(0)==AV60TFConfiguracionEmpresaCuotaLandingPage_To) ? "" : StringUtil.Trim( context.localUtil.Format( AV60TFConfiguracionEmpresaCuotaLandingPage_To, "ZZZZZZZZ9.99")))) ;
         AV11GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         AV11GridState.gxTpr_Currentpage = (short)(subGrid_fnc_Currentpage( ));
         new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV61Pgmname+"GridState",  AV11GridState.ToXml(false, true, "", "")) ;
      }

      protected void S122( )
      {
         /* 'PREPARETRANSACTION' Routine */
         returnInSub = false;
         AV9TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV9TrnContext.gxTpr_Callerobject = AV61Pgmname;
         AV9TrnContext.gxTpr_Callerondelete = true;
         AV9TrnContext.gxTpr_Callerurl = AV8HTTPRequest.ScriptName+"?"+AV8HTTPRequest.QueryString;
         AV9TrnContext.gxTpr_Transactionname = "ConfiguracionEmpresa";
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
         new configuracionempresawwexport(context ).execute( out  AV17ExcelFilename, out  AV18ErrorMessage) ;
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
         Innewwindow1_Target = formatLink("configuracionempresawwexportreport.aspx") ;
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
         PA3R2( ) ;
         WS3R2( ) ;
         WE3R2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217093870", true, true);
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
         context.AddJavascriptSource("configuracionempresaww.js", "?20241217093871", false, true);
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
         edtConfiguracionEmpresaId_Internalname = "CONFIGURACIONEMPRESAID_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaTelefono_Internalname = "CONFIGURACIONEMPRESATELEFONO_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCostoPlanB_Internalname = "CONFIGURACIONEMPRESACOSTOPLANB_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCuotaPlanB_Internalname = "CONFIGURACIONEMPRESACUOTAPLANB_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCostoPlanS_Internalname = "CONFIGURACIONEMPRESACOSTOPLANS_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCuotaPlanS_Internalname = "CONFIGURACIONEMPRESACUOTAPLANS_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCostoPlanN_Internalname = "CONFIGURACIONEMPRESACOSTOPLANN_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCuotaPlanN_Internalname = "CONFIGURACIONEMPRESACUOTAPLANN_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCostoLandi_Internalname = "CONFIGURACIONEMPRESACOSTOLANDI_"+sGXsfl_44_idx;
         edtConfiguracionEmpresaCuotaLandi_Internalname = "CONFIGURACIONEMPRESACUOTALANDI_"+sGXsfl_44_idx;
      }

      protected void SubsflControlProps_fel_442( )
      {
         cmbavGridactions_Internalname = "vGRIDACTIONS_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaId_Internalname = "CONFIGURACIONEMPRESAID_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaTelefono_Internalname = "CONFIGURACIONEMPRESATELEFONO_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCostoPlanB_Internalname = "CONFIGURACIONEMPRESACOSTOPLANB_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCuotaPlanB_Internalname = "CONFIGURACIONEMPRESACUOTAPLANB_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCostoPlanS_Internalname = "CONFIGURACIONEMPRESACOSTOPLANS_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCuotaPlanS_Internalname = "CONFIGURACIONEMPRESACUOTAPLANS_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCostoPlanN_Internalname = "CONFIGURACIONEMPRESACOSTOPLANN_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCuotaPlanN_Internalname = "CONFIGURACIONEMPRESACUOTAPLANN_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCostoLandi_Internalname = "CONFIGURACIONEMPRESACOSTOLANDI_"+sGXsfl_44_fel_idx;
         edtConfiguracionEmpresaCuotaLandi_Internalname = "CONFIGURACIONEMPRESACUOTALANDI_"+sGXsfl_44_fel_idx;
      }

      protected void sendrow_442( )
      {
         sGXsfl_44_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_44_idx), 4, 0), 4, "0");
         SubsflControlProps_442( ) ;
         WB3R0( ) ;
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
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaId_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaId_Internalname,StringUtil.LTrim( StringUtil.NToC( (decimal)(A44ConfiguracionEmpresaId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9")),(string)" dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaId_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtConfiguracionEmpresaId_Visible,(short)0,(short)0,(string)"text",(string)"1",(short)0,(string)"px",(short)17,(string)"px",(short)4,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Id",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtConfiguracionEmpresaTelefono_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            if ( context.isSmartDevice( ) )
            {
               gxphoneLink = "tel:" + StringUtil.RTrim( A45ConfiguracionEmpresaTelefono);
            }
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaTelefono_Internalname,StringUtil.RTrim( A45ConfiguracionEmpresaTelefono),(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)gxphoneLink,(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaTelefono_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtConfiguracionEmpresaTelefono_Visible,(short)0,(short)0,(string)"tel",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"GeneXus\\Phone",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCostoPlanB_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCostoPlanB_Internalname,StringUtil.LTrim( StringUtil.NToC( A46ConfiguracionEmpresaCostoPlanB, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A46ConfiguracionEmpresaCostoPlanB, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCostoPlanB_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtConfiguracionEmpresaCostoPlanB_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCuotaPlanB_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCuotaPlanB_Internalname,StringUtil.LTrim( StringUtil.NToC( A47ConfiguracionEmpresaCuotaPlanB, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A47ConfiguracionEmpresaCuotaPlanB, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCuotaPlanB_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtConfiguracionEmpresaCuotaPlanB_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCostoPlanS_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCostoPlanS_Internalname,StringUtil.LTrim( StringUtil.NToC( A48ConfiguracionEmpresaCostoPlanS, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A48ConfiguracionEmpresaCostoPlanS, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCostoPlanS_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtConfiguracionEmpresaCostoPlanS_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCuotaPlanS_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCuotaPlanS_Internalname,StringUtil.LTrim( StringUtil.NToC( A49ConfiguracionEmpresaCuotaPlanS, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A49ConfiguracionEmpresaCuotaPlanS, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCuotaPlanS_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtConfiguracionEmpresaCuotaPlanS_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCostoPlanN_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCostoPlanN_Internalname,StringUtil.LTrim( StringUtil.NToC( A50ConfiguracionEmpresaCostoPlanN, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A50ConfiguracionEmpresaCostoPlanN, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCostoPlanN_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtConfiguracionEmpresaCostoPlanN_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCuotaPlanN_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCuotaPlanN_Internalname,StringUtil.LTrim( StringUtil.NToC( A51ConfiguracionEmpresaCuotaPlanN, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A51ConfiguracionEmpresaCuotaPlanN, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCuotaPlanN_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtConfiguracionEmpresaCuotaPlanN_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCostoLandi_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCostoLandi_Internalname,StringUtil.LTrim( StringUtil.NToC( A54ConfiguracionEmpresaCostoLandi, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A54ConfiguracionEmpresaCostoLandi, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCostoLandi_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtConfiguracionEmpresaCostoLandi_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+((edtConfiguracionEmpresaCuotaLandi_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtConfiguracionEmpresaCuotaLandi_Internalname,StringUtil.LTrim( StringUtil.NToC( A55ConfiguracionEmpresaCuotaLandi, 12, 2, context.GetLanguageProperty( "decimal_point"), "")),StringUtil.LTrim( context.localUtil.Format( A55ConfiguracionEmpresaCuotaLandi, "ZZZZZZZZ9.99")),(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtConfiguracionEmpresaCuotaLandi_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtConfiguracionEmpresaCuotaLandi_Visible,(short)0,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)12,(short)0,(short)0,(short)44,(short)0,(short)-1,(short)0,(bool)true,(string)"Precio",(string)"end",(bool)false,(string)""});
            send_integrity_lvl_hashes3R2( ) ;
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
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaId_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Id", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaTelefono_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Telefono", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCostoPlanB_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Plan Basico", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCuotaPlanB_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Plan Basico", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCostoPlanS_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Plan Superior", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCuotaPlanS_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Plan Superior", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCostoPlanN_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Plan Negocios", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCuotaPlanN_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Plan Negocios", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCostoLandi_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Landing Page", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtConfiguracionEmpresaCuotaLandi_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Landing Page", "")) ;
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
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(A44ConfiguracionEmpresaId), 4, 0, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaId_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( A45ConfiguracionEmpresaTelefono)));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaTelefono_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A46ConfiguracionEmpresaCostoPlanB, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCostoPlanB_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A47ConfiguracionEmpresaCuotaPlanB, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCuotaPlanB_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A48ConfiguracionEmpresaCostoPlanS, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCostoPlanS_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A49ConfiguracionEmpresaCuotaPlanS, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCuotaPlanS_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A50ConfiguracionEmpresaCostoPlanN, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCostoPlanN_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A51ConfiguracionEmpresaCuotaPlanN, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCuotaPlanN_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A54ConfiguracionEmpresaCostoLandi, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCostoLandi_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( A55ConfiguracionEmpresaCuotaLandi, 12, 2, ".", ""))));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtConfiguracionEmpresaCuotaLandi_Visible), 5, 0, ".", "")));
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
         edtConfiguracionEmpresaId_Internalname = "CONFIGURACIONEMPRESAID";
         edtConfiguracionEmpresaTelefono_Internalname = "CONFIGURACIONEMPRESATELEFONO";
         edtConfiguracionEmpresaCostoPlanB_Internalname = "CONFIGURACIONEMPRESACOSTOPLANB";
         edtConfiguracionEmpresaCuotaPlanB_Internalname = "CONFIGURACIONEMPRESACUOTAPLANB";
         edtConfiguracionEmpresaCostoPlanS_Internalname = "CONFIGURACIONEMPRESACOSTOPLANS";
         edtConfiguracionEmpresaCuotaPlanS_Internalname = "CONFIGURACIONEMPRESACUOTAPLANS";
         edtConfiguracionEmpresaCostoPlanN_Internalname = "CONFIGURACIONEMPRESACOSTOPLANN";
         edtConfiguracionEmpresaCuotaPlanN_Internalname = "CONFIGURACIONEMPRESACUOTAPLANN";
         edtConfiguracionEmpresaCostoLandi_Internalname = "CONFIGURACIONEMPRESACOSTOLANDI";
         edtConfiguracionEmpresaCuotaLandi_Internalname = "CONFIGURACIONEMPRESACUOTALANDI";
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
         edtConfiguracionEmpresaCuotaLandi_Jsonclick = "";
         edtConfiguracionEmpresaCostoLandi_Jsonclick = "";
         edtConfiguracionEmpresaCuotaPlanN_Jsonclick = "";
         edtConfiguracionEmpresaCostoPlanN_Jsonclick = "";
         edtConfiguracionEmpresaCuotaPlanS_Jsonclick = "";
         edtConfiguracionEmpresaCostoPlanS_Jsonclick = "";
         edtConfiguracionEmpresaCuotaPlanB_Jsonclick = "";
         edtConfiguracionEmpresaCostoPlanB_Jsonclick = "";
         edtConfiguracionEmpresaTelefono_Jsonclick = "";
         edtConfiguracionEmpresaId_Jsonclick = "";
         cmbavGridactions_Jsonclick = "";
         cmbavGridactions_Class = "ConvertToDDO";
         subGrid_Class = "GridWithPaginationBar GridNoBorder WorkWith";
         subGrid_Backcolorstyle = 0;
         edtConfiguracionEmpresaCuotaLandi_Visible = -1;
         edtConfiguracionEmpresaCostoLandi_Visible = -1;
         edtConfiguracionEmpresaCuotaPlanN_Visible = -1;
         edtConfiguracionEmpresaCostoPlanN_Visible = -1;
         edtConfiguracionEmpresaCuotaPlanS_Visible = -1;
         edtConfiguracionEmpresaCostoPlanS_Visible = -1;
         edtConfiguracionEmpresaCuotaPlanB_Visible = -1;
         edtConfiguracionEmpresaCostoPlanB_Visible = -1;
         edtConfiguracionEmpresaTelefono_Visible = -1;
         edtConfiguracionEmpresaId_Visible = -1;
         edtConfiguracionEmpresaCuotaLandi_Enabled = 0;
         edtConfiguracionEmpresaCostoLandi_Enabled = 0;
         edtConfiguracionEmpresaCuotaPlanN_Enabled = 0;
         edtConfiguracionEmpresaCostoPlanN_Enabled = 0;
         edtConfiguracionEmpresaCuotaPlanS_Enabled = 0;
         edtConfiguracionEmpresaCostoPlanS_Enabled = 0;
         edtConfiguracionEmpresaCuotaPlanB_Enabled = 0;
         edtConfiguracionEmpresaCostoPlanB_Enabled = 0;
         edtConfiguracionEmpresaTelefono_Enabled = 0;
         edtConfiguracionEmpresaId_Enabled = 0;
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
         Ddo_grid_Format = "4.0||12.2|12.2|12.2|12.2|12.2|12.2|12.2|12.2";
         Ddo_grid_Datalistproc = "ConfiguracionEmpresaWWGetFilterData";
         Ddo_grid_Datalisttype = "|Dynamic||||||||";
         Ddo_grid_Includedatalist = "|T||||||||";
         Ddo_grid_Filterisrange = "T||T|T|T|T|T|T|T|T";
         Ddo_grid_Filtertype = "Numeric|Character|Numeric|Numeric|Numeric|Numeric|Numeric|Numeric|Numeric|Numeric";
         Ddo_grid_Includefilter = "T";
         Ddo_grid_Fixable = "T";
         Ddo_grid_Includesortasc = "T";
         Ddo_grid_Columnssortvalues = "2|1|3|4|5|6|7|8|9|10";
         Ddo_grid_Columnids = "1:ConfiguracionEmpresaId|2:ConfiguracionEmpresaTelefono|3:ConfiguracionEmpresaCostoPlanBasico|4:ConfiguracionEmpresaCuotaPlanBasico|5:ConfiguracionEmpresaCostoPlanSuperior|6:ConfiguracionEmpresaCuotaPlanSuperior|7:ConfiguracionEmpresaCostoPlanNegocios|8:ConfiguracionEmpresaCuotaPlanNegocios|9:ConfiguracionEmpresaCostoLandingPage|10:ConfiguracionEmpresaCuotaLandingPage";
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
         Dvpanel_unnamedtable1_Title = context.GetMessage( "Configuración de Empresa", "");
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
         Form.Caption = context.GetMessage( " Configuracion Empresa", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtConfiguracionEmpresaId_Visible","ctrl":"CONFIGURACIONEMPRESAID","prop":"Visible"},{"av":"edtConfiguracionEmpresaTelefono_Visible","ctrl":"CONFIGURACIONEMPRESATELEFONO","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoLandi_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOLANDI","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaLandi_Visible","ctrl":"CONFIGURACIONEMPRESACUOTALANDI","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E123R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E133R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED","""{"handler":"E153R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_grid_Activeeventkey","ctrl":"DDO_GRID","prop":"ActiveEventKey"},{"av":"Ddo_grid_Selectedvalue_get","ctrl":"DDO_GRID","prop":"SelectedValue_get"},{"av":"Ddo_grid_Filteredtextto_get","ctrl":"DDO_GRID","prop":"FilteredTextTo_get"},{"av":"Ddo_grid_Filteredtext_get","ctrl":"DDO_GRID","prop":"FilteredText_get"},{"av":"Ddo_grid_Selectedcolumn","ctrl":"DDO_GRID","prop":"SelectedColumn"}]""");
         setEventMetadata("DDO_GRID.ONOPTIONCLICKED",""","oparms":[{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E203R2","iparms":[{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"}]}""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED","""{"handler":"E163R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_gridcolumnsselector_Columnsselectorvalues","ctrl":"DDO_GRIDCOLUMNSSELECTOR","prop":"ColumnsSelectorValues"}]""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED",""","oparms":[{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"edtConfiguracionEmpresaId_Visible","ctrl":"CONFIGURACIONEMPRESAID","prop":"Visible"},{"av":"edtConfiguracionEmpresaTelefono_Visible","ctrl":"CONFIGURACIONEMPRESATELEFONO","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoLandi_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOLANDI","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaLandi_Visible","ctrl":"CONFIGURACIONEMPRESACUOTALANDI","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED","""{"handler":"E113R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_managefilters_Activeeventkey","ctrl":"DDO_MANAGEFILTERS","prop":"ActiveEventKey"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtConfiguracionEmpresaId_Visible","ctrl":"CONFIGURACIONEMPRESAID","prop":"Visible"},{"av":"edtConfiguracionEmpresaTelefono_Visible","ctrl":"CONFIGURACIONEMPRESATELEFONO","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoLandi_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOLANDI","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaLandi_Visible","ctrl":"CONFIGURACIONEMPRESACUOTALANDI","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"}]}""");
         setEventMetadata("VGRIDACTIONS.CLICK","""{"handler":"E213R2","iparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A44ConfiguracionEmpresaId","fld":"CONFIGURACIONEMPRESAID","pic":"ZZZ9","hsh":true}]""");
         setEventMetadata("VGRIDACTIONS.CLICK",""","oparms":[{"av":"cmbavGridactions"},{"av":"AV42GridActions","fld":"vGRIDACTIONS","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtConfiguracionEmpresaId_Visible","ctrl":"CONFIGURACIONEMPRESAID","prop":"Visible"},{"av":"edtConfiguracionEmpresaTelefono_Visible","ctrl":"CONFIGURACIONEMPRESATELEFONO","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoLandi_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOLANDI","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaLandi_Visible","ctrl":"CONFIGURACIONEMPRESACUOTALANDI","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("'DOINSERT'","""{"handler":"E173R2","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"A44ConfiguracionEmpresaId","fld":"CONFIGURACIONEMPRESAID","pic":"ZZZ9","hsh":true}]""");
         setEventMetadata("'DOINSERT'",""","oparms":[{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtConfiguracionEmpresaId_Visible","ctrl":"CONFIGURACIONEMPRESAID","prop":"Visible"},{"av":"edtConfiguracionEmpresaTelefono_Visible","ctrl":"CONFIGURACIONEMPRESATELEFONO","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanB_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANB","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanS_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANS","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaPlanN_Visible","ctrl":"CONFIGURACIONEMPRESACUOTAPLANN","prop":"Visible"},{"av":"edtConfiguracionEmpresaCostoLandi_Visible","ctrl":"CONFIGURACIONEMPRESACOSTOLANDI","prop":"Visible"},{"av":"edtConfiguracionEmpresaCuotaLandi_Visible","ctrl":"CONFIGURACIONEMPRESACUOTALANDI","prop":"Visible"},{"av":"AV39GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV41GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV24ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV11GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_AGEXPORT.ONOPTIONCLICKED","""{"handler":"E143R2","iparms":[{"av":"Ddo_agexport_Activeeventkey","ctrl":"DDO_AGEXPORT","prop":"ActiveEventKey"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"}]""");
         setEventMetadata("DDO_AGEXPORT.ONOPTIONCLICKED",""","oparms":[{"av":"Innewwindow1_Target","ctrl":"INNEWWINDOW1","prop":"Target"},{"av":"Innewwindow1_Height","ctrl":"INNEWWINDOW1","prop":"Height"},{"av":"Innewwindow1_Width","ctrl":"INNEWWINDOW1","prop":"Width"},{"av":"AV11GridState","fld":"vGRIDSTATE"},{"av":"AV13OrderedBy","fld":"vORDEREDBY","pic":"ZZZ9"},{"av":"AV14OrderedDsc","fld":"vORDEREDDSC"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"AV16FilterFullText","fld":"vFILTERFULLTEXT"},{"av":"AV27TFConfiguracionEmpresaId","fld":"vTFCONFIGURACIONEMPRESAID","pic":"ZZZ9"},{"av":"AV28TFConfiguracionEmpresaId_To","fld":"vTFCONFIGURACIONEMPRESAID_TO","pic":"ZZZ9"},{"av":"AV26ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV21ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV61Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV29TFConfiguracionEmpresaTelefono","fld":"vTFCONFIGURACIONEMPRESATELEFONO"},{"av":"AV30TFConfiguracionEmpresaTelefono_Sel","fld":"vTFCONFIGURACIONEMPRESATELEFONO_SEL"},{"av":"AV31TFConfiguracionEmpresaCostoPlanBasico","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV32TFConfiguracionEmpresaCostoPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV33TFConfiguracionEmpresaCuotaPlanBasico","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV34TFConfiguracionEmpresaCuotaPlanBasico_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANBASICO_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV45TFConfiguracionEmpresaCostoPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV46TFConfiguracionEmpresaCostoPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV47TFConfiguracionEmpresaCuotaPlanSuperior","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV48TFConfiguracionEmpresaCuotaPlanSuperior_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV49TFConfiguracionEmpresaCostoPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV50TFConfiguracionEmpresaCostoPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV51TFConfiguracionEmpresaCuotaPlanNegocios","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV52TFConfiguracionEmpresaCuotaPlanNegocios_To","fld":"vTFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV57TFConfiguracionEmpresaCostoLandingPage","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV58TFConfiguracionEmpresaCostoLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACOSTOLANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV59TFConfiguracionEmpresaCuotaLandingPage","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE","pic":"ZZZZZZZZ9.99"},{"av":"AV60TFConfiguracionEmpresaCuotaLandingPage_To","fld":"vTFCONFIGURACIONEMPRESACUOTALANDINGPAGE_TO","pic":"ZZZZZZZZ9.99"},{"av":"AV53IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV54IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV55IsAuthorized_Delete","fld":"vISAUTHORIZED_DELETE","hsh":true},{"av":"AV56IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_grid_Sortedstatus","ctrl":"DDO_GRID","prop":"SortedStatus"},{"av":"Ddo_grid_Selectedvalue_set","ctrl":"DDO_GRID","prop":"SelectedValue_set"},{"av":"Ddo_grid_Filteredtext_set","ctrl":"DDO_GRID","prop":"FilteredText_set"},{"av":"Ddo_grid_Filteredtextto_set","ctrl":"DDO_GRID","prop":"FilteredTextTo_set"}]}""");
         setEventMetadata("NULL","""{"handler":"Valid_Configuracionempresacuotalandi","iparms":[]}""");
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
         AV27TFConfiguracionEmpresaId = 0;
         AV28TFConfiguracionEmpresaId_To = 0;
         AV21ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV61Pgmname = "";
         AV29TFConfiguracionEmpresaTelefono = "";
         AV30TFConfiguracionEmpresaTelefono_Sel = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV24ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV41GridAppliedFilters = "";
         AV43AGExportData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
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
         ClassString = "";
         StyleString = "";
         ucDvpanel_unnamedtable1 = new GXUserControl();
         TempTags = "";
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
         A45ConfiguracionEmpresaTelefono = "";
         lV62Configuracionempresawwds_1_filterfulltext = "";
         lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         AV62Configuracionempresawwds_1_filterfulltext = "";
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = "";
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         H003R2_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         H003R2_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         H003R2_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         H003R2_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         H003R2_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         H003R2_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         H003R2_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         H003R2_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         H003R2_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         H003R2_A44ConfiguracionEmpresaId = new short[1] ;
         H003R3_AGRID_nRecordCount = new long[1] ;
         AV8HTTPRequest = new GxHttpRequest( context);
         AV44AGExportDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
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
         GXt_char2 = "";
         AV9TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV17ExcelFilename = "";
         AV18ErrorMessage = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid_Linesclass = "";
         GXCCtl = "";
         ROClassString = "";
         gxphoneLink = "";
         GridColumn = new GXWebColumn();
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresaww__default(),
            new Object[][] {
                new Object[] {
               H003R2_A55ConfiguracionEmpresaCuotaLandi, H003R2_A54ConfiguracionEmpresaCostoLandi, H003R2_A51ConfiguracionEmpresaCuotaPlanN, H003R2_A50ConfiguracionEmpresaCostoPlanN, H003R2_A49ConfiguracionEmpresaCuotaPlanS, H003R2_A48ConfiguracionEmpresaCostoPlanS, H003R2_A47ConfiguracionEmpresaCuotaPlanB, H003R2_A46ConfiguracionEmpresaCostoPlanB, H003R2_A45ConfiguracionEmpresaTelefono, H003R2_A44ConfiguracionEmpresaId
               }
               , new Object[] {
               H003R3_AGRID_nRecordCount
               }
            }
         );
         AV61Pgmname = "ConfiguracionEmpresaWW";
         /* GeneXus formulas. */
         AV61Pgmname = "ConfiguracionEmpresaWW";
      }

      private short GRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short AV13OrderedBy ;
      private short AV27TFConfiguracionEmpresaId ;
      private short AV28TFConfiguracionEmpresaId_To ;
      private short AV26ManageFiltersExecutionStep ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV42GridActions ;
      private short A44ConfiguracionEmpresaId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGrid_Backcolorstyle ;
      private short subGrid_Sortable ;
      private short AV63Configuracionempresawwds_2_tfconfiguracionempresaid ;
      private short AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ;
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
      private int edtConfiguracionEmpresaId_Enabled ;
      private int edtConfiguracionEmpresaTelefono_Enabled ;
      private int edtConfiguracionEmpresaCostoPlanB_Enabled ;
      private int edtConfiguracionEmpresaCuotaPlanB_Enabled ;
      private int edtConfiguracionEmpresaCostoPlanS_Enabled ;
      private int edtConfiguracionEmpresaCuotaPlanS_Enabled ;
      private int edtConfiguracionEmpresaCostoPlanN_Enabled ;
      private int edtConfiguracionEmpresaCuotaPlanN_Enabled ;
      private int edtConfiguracionEmpresaCostoLandi_Enabled ;
      private int edtConfiguracionEmpresaCuotaLandi_Enabled ;
      private int edtConfiguracionEmpresaId_Visible ;
      private int edtConfiguracionEmpresaTelefono_Visible ;
      private int edtConfiguracionEmpresaCostoPlanB_Visible ;
      private int edtConfiguracionEmpresaCuotaPlanB_Visible ;
      private int edtConfiguracionEmpresaCostoPlanS_Visible ;
      private int edtConfiguracionEmpresaCuotaPlanS_Visible ;
      private int edtConfiguracionEmpresaCostoPlanN_Visible ;
      private int edtConfiguracionEmpresaCuotaPlanN_Visible ;
      private int edtConfiguracionEmpresaCostoLandi_Visible ;
      private int edtConfiguracionEmpresaCuotaLandi_Visible ;
      private int AV38PageToGo ;
      private int AV83GXV1 ;
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
      private decimal AV31TFConfiguracionEmpresaCostoPlanBasico ;
      private decimal AV32TFConfiguracionEmpresaCostoPlanBasico_To ;
      private decimal AV33TFConfiguracionEmpresaCuotaPlanBasico ;
      private decimal AV34TFConfiguracionEmpresaCuotaPlanBasico_To ;
      private decimal AV45TFConfiguracionEmpresaCostoPlanSuperior ;
      private decimal AV46TFConfiguracionEmpresaCostoPlanSuperior_To ;
      private decimal AV47TFConfiguracionEmpresaCuotaPlanSuperior ;
      private decimal AV48TFConfiguracionEmpresaCuotaPlanSuperior_To ;
      private decimal AV49TFConfiguracionEmpresaCostoPlanNegocios ;
      private decimal AV50TFConfiguracionEmpresaCostoPlanNegocios_To ;
      private decimal AV51TFConfiguracionEmpresaCuotaPlanNegocios ;
      private decimal AV52TFConfiguracionEmpresaCuotaPlanNegocios_To ;
      private decimal AV57TFConfiguracionEmpresaCostoLandingPage ;
      private decimal AV58TFConfiguracionEmpresaCostoLandingPage_To ;
      private decimal AV59TFConfiguracionEmpresaCuotaLandingPage ;
      private decimal AV60TFConfiguracionEmpresaCuotaLandingPage_To ;
      private decimal A46ConfiguracionEmpresaCostoPlanB ;
      private decimal A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal A48ConfiguracionEmpresaCostoPlanS ;
      private decimal A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal A50ConfiguracionEmpresaCostoPlanN ;
      private decimal A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal A54ConfiguracionEmpresaCostoLandi ;
      private decimal A55ConfiguracionEmpresaCuotaLandi ;
      private decimal AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ;
      private decimal AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ;
      private decimal AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ;
      private decimal AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ;
      private decimal AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ;
      private decimal AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ;
      private decimal AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ;
      private decimal AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ;
      private decimal AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ;
      private decimal AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ;
      private decimal AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ;
      private decimal AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ;
      private decimal AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ;
      private decimal AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ;
      private decimal AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ;
      private decimal AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ;
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
      private string AV61Pgmname ;
      private string AV29TFConfiguracionEmpresaTelefono ;
      private string AV30TFConfiguracionEmpresaTelefono_Sel ;
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
      private string ClassString ;
      private string StyleString ;
      private string Dvpanel_unnamedtable1_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string divTableheader_Internalname ;
      private string divTableheadercontent_Internalname ;
      private string divTableactions_Internalname ;
      private string TempTags ;
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
      private string edtConfiguracionEmpresaId_Internalname ;
      private string A45ConfiguracionEmpresaTelefono ;
      private string edtConfiguracionEmpresaTelefono_Internalname ;
      private string edtConfiguracionEmpresaCostoPlanB_Internalname ;
      private string edtConfiguracionEmpresaCuotaPlanB_Internalname ;
      private string edtConfiguracionEmpresaCostoPlanS_Internalname ;
      private string edtConfiguracionEmpresaCuotaPlanS_Internalname ;
      private string edtConfiguracionEmpresaCostoPlanN_Internalname ;
      private string edtConfiguracionEmpresaCuotaPlanN_Internalname ;
      private string edtConfiguracionEmpresaCostoLandi_Internalname ;
      private string edtConfiguracionEmpresaCuotaLandi_Internalname ;
      private string lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ;
      private string AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string cmbavGridactions_Class ;
      private string GXEncryptionTmp ;
      private string GXt_char2 ;
      private string sGXsfl_44_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string GXCCtl ;
      private string cmbavGridactions_Jsonclick ;
      private string ROClassString ;
      private string edtConfiguracionEmpresaId_Jsonclick ;
      private string gxphoneLink ;
      private string edtConfiguracionEmpresaTelefono_Jsonclick ;
      private string edtConfiguracionEmpresaCostoPlanB_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaPlanB_Jsonclick ;
      private string edtConfiguracionEmpresaCostoPlanS_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaPlanS_Jsonclick ;
      private string edtConfiguracionEmpresaCostoPlanN_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaPlanN_Jsonclick ;
      private string edtConfiguracionEmpresaCostoLandi_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaLandi_Jsonclick ;
      private string subGrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV14OrderedDsc ;
      private bool AV53IsAuthorized_Display ;
      private bool AV54IsAuthorized_Update ;
      private bool AV55IsAuthorized_Delete ;
      private bool AV56IsAuthorized_Insert ;
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
      private string AV19ColumnsSelectorXML ;
      private string AV25ManageFiltersXml ;
      private string AV20UserCustomValue ;
      private string AV16FilterFullText ;
      private string AV41GridAppliedFilters ;
      private string lV62Configuracionempresawwds_1_filterfulltext ;
      private string AV62Configuracionempresawwds_1_filterfulltext ;
      private string AV17ExcelFilename ;
      private string AV18ErrorMessage ;
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
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV43AGExportData ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV35DDO_TitleSettingsIcons ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV11GridState ;
      private IDataStoreProvider pr_default ;
      private decimal[] H003R2_A55ConfiguracionEmpresaCuotaLandi ;
      private decimal[] H003R2_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] H003R2_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] H003R2_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] H003R2_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] H003R2_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] H003R2_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] H003R2_A46ConfiguracionEmpresaCostoPlanB ;
      private string[] H003R2_A45ConfiguracionEmpresaTelefono ;
      private short[] H003R2_A44ConfiguracionEmpresaId ;
      private long[] H003R3_AGRID_nRecordCount ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item AV44AGExportDataItem ;
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

   public class configuracionempresaww__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_H003R2( IGxContext context ,
                                             string AV62Configuracionempresawwds_1_filterfulltext ,
                                             short AV63Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                             short AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                             string AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                             string AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                             decimal AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                             decimal AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                             decimal AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                             decimal AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                             decimal AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                             decimal AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                             decimal AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                             decimal AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                             decimal AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                             decimal AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                             decimal AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                             decimal AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                             decimal AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                             decimal AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                             decimal AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                             decimal AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
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
                                             short AV13OrderedBy ,
                                             bool AV14OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int5 = new short[32];
         Object[] GXv_Object6 = new Object[2];
         string sSelectString;
         string sFromString;
         string sOrderString;
         sSelectString = " `ConfiguracionEmpresaCuotaLandi`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaId`";
         sFromString = " FROM `ConfiguracionEmpresa`";
         sOrderString = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( `ConfiguracionEmpresaTelefono` like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int5[0] = 1;
            GXv_int5[1] = 1;
            GXv_int5[2] = 1;
            GXv_int5[3] = 1;
            GXv_int5[4] = 1;
            GXv_int5[5] = 1;
            GXv_int5[6] = 1;
            GXv_int5[7] = 1;
            GXv_int5[8] = 1;
            GXv_int5[9] = 1;
         }
         if ( ! (0==AV63Configuracionempresawwds_2_tfconfiguracionempresaid) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` >= @AV63Configuracionempresawwds_2_tfconfiguracionempresaid)");
         }
         else
         {
            GXv_int5[10] = 1;
         }
         if ( ! (0==AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` <= @AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to)");
         }
         else
         {
            GXv_int5[11] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono)) ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` like @lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono)");
         }
         else
         {
            GXv_int5[12] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ! ( StringUtil.StrCmp(AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` = @AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_se)");
         }
         else
         {
            GXv_int5[13] = 1;
         }
         if ( StringUtil.StrCmp(AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`ConfiguracionEmpresaTelefono`))=0))");
         }
         if ( ! (Convert.ToDecimal(0)==AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` >= @AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int5[14] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` <= @AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int5[15] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` >= @AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int5[16] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` <= @AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int5[17] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` >= @AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int5[18] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` <= @AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int5[19] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` >= @AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int5[20] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` <= @AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int5[21] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` >= @AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int5[22] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` <= @AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int5[23] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` >= @AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int5[24] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` <= @AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int5[25] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` >= @AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int5[26] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` <= @AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int5[27] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` >= @AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int5[28] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` <= @AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int5[29] = 1;
         }
         if ( ( AV13OrderedBy == 1 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaTelefono`";
         }
         else if ( ( AV13OrderedBy == 1 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaTelefono` DESC";
         }
         else if ( ( AV13OrderedBy == 2 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaId`";
         }
         else if ( ( AV13OrderedBy == 2 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaId` DESC";
         }
         else if ( ( AV13OrderedBy == 3 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoPlanB`";
         }
         else if ( ( AV13OrderedBy == 3 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoPlanB` DESC";
         }
         else if ( ( AV13OrderedBy == 4 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaPlanB`";
         }
         else if ( ( AV13OrderedBy == 4 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaPlanB` DESC";
         }
         else if ( ( AV13OrderedBy == 5 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoPlanS`";
         }
         else if ( ( AV13OrderedBy == 5 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoPlanS` DESC";
         }
         else if ( ( AV13OrderedBy == 6 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaPlanS`";
         }
         else if ( ( AV13OrderedBy == 6 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaPlanS` DESC";
         }
         else if ( ( AV13OrderedBy == 7 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoPlanN`";
         }
         else if ( ( AV13OrderedBy == 7 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoPlanN` DESC";
         }
         else if ( ( AV13OrderedBy == 8 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaPlanN`";
         }
         else if ( ( AV13OrderedBy == 8 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaPlanN` DESC";
         }
         else if ( ( AV13OrderedBy == 9 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoLandi`";
         }
         else if ( ( AV13OrderedBy == 9 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCostoLandi` DESC";
         }
         else if ( ( AV13OrderedBy == 10 ) && ! AV14OrderedDsc )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaLandi`";
         }
         else if ( ( AV13OrderedBy == 10 ) && ( AV14OrderedDsc ) )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaCuotaLandi` DESC";
         }
         else if ( true )
         {
            sOrderString += " ORDER BY `ConfiguracionEmpresaId`";
         }
         scmdbuf = "SELECT " + sSelectString + sFromString + sWhereString + sOrderString + "" + " LIMIT " + "@GXPagingFrom2" + ", " + "@GXPagingTo2";
         GXv_Object6[0] = scmdbuf;
         GXv_Object6[1] = GXv_int5;
         return GXv_Object6 ;
      }

      protected Object[] conditional_H003R3( IGxContext context ,
                                             string AV62Configuracionempresawwds_1_filterfulltext ,
                                             short AV63Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                             short AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                             string AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                             string AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                             decimal AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                             decimal AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                             decimal AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                             decimal AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                             decimal AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                             decimal AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                             decimal AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                             decimal AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                             decimal AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                             decimal AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                             decimal AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                             decimal AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                             decimal AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                             decimal AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                             decimal AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                             decimal AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
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
                                             short AV13OrderedBy ,
                                             bool AV14OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int7 = new short[30];
         Object[] GXv_Object8 = new Object[2];
         scmdbuf = "SELECT COUNT(*) FROM `ConfiguracionEmpresa`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( `ConfiguracionEmpresaTelefono` like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int7[0] = 1;
            GXv_int7[1] = 1;
            GXv_int7[2] = 1;
            GXv_int7[3] = 1;
            GXv_int7[4] = 1;
            GXv_int7[5] = 1;
            GXv_int7[6] = 1;
            GXv_int7[7] = 1;
            GXv_int7[8] = 1;
            GXv_int7[9] = 1;
         }
         if ( ! (0==AV63Configuracionempresawwds_2_tfconfiguracionempresaid) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` >= @AV63Configuracionempresawwds_2_tfconfiguracionempresaid)");
         }
         else
         {
            GXv_int7[10] = 1;
         }
         if ( ! (0==AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` <= @AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to)");
         }
         else
         {
            GXv_int7[11] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono)) ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` like @lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono)");
         }
         else
         {
            GXv_int7[12] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ! ( StringUtil.StrCmp(AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, "<#Empty#>") == 0 ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` = @AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_se)");
         }
         else
         {
            GXv_int7[13] = 1;
         }
         if ( StringUtil.StrCmp(AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, "<#Empty#>") == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`ConfiguracionEmpresaTelefono`))=0))");
         }
         if ( ! (Convert.ToDecimal(0)==AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` >= @AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int7[14] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` <= @AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int7[15] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` >= @AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int7[16] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` <= @AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int7[17] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` >= @AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int7[18] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` <= @AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int7[19] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` >= @AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int7[20] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` <= @AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int7[21] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` >= @AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int7[22] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` <= @AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int7[23] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` >= @AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int7[24] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` <= @AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int7[25] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` >= @AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int7[26] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` <= @AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int7[27] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` >= @AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int7[28] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` <= @AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int7[29] = 1;
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
         else if ( ( AV13OrderedBy == 7 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 7 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 8 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 8 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 9 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 9 ) && ( AV14OrderedDsc ) )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 10 ) && ! AV14OrderedDsc )
         {
            scmdbuf += "";
         }
         else if ( ( AV13OrderedBy == 10 ) && ( AV14OrderedDsc ) )
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
                     return conditional_H003R2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (decimal)dynConstraints[5] , (decimal)dynConstraints[6] , (decimal)dynConstraints[7] , (decimal)dynConstraints[8] , (decimal)dynConstraints[9] , (decimal)dynConstraints[10] , (decimal)dynConstraints[11] , (decimal)dynConstraints[12] , (decimal)dynConstraints[13] , (decimal)dynConstraints[14] , (decimal)dynConstraints[15] , (decimal)dynConstraints[16] , (decimal)dynConstraints[17] , (decimal)dynConstraints[18] , (decimal)dynConstraints[19] , (decimal)dynConstraints[20] , (short)dynConstraints[21] , (string)dynConstraints[22] , (decimal)dynConstraints[23] , (decimal)dynConstraints[24] , (decimal)dynConstraints[25] , (decimal)dynConstraints[26] , (decimal)dynConstraints[27] , (decimal)dynConstraints[28] , (decimal)dynConstraints[29] , (decimal)dynConstraints[30] , (short)dynConstraints[31] , (bool)dynConstraints[32] );
               case 1 :
                     return conditional_H003R3(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (decimal)dynConstraints[5] , (decimal)dynConstraints[6] , (decimal)dynConstraints[7] , (decimal)dynConstraints[8] , (decimal)dynConstraints[9] , (decimal)dynConstraints[10] , (decimal)dynConstraints[11] , (decimal)dynConstraints[12] , (decimal)dynConstraints[13] , (decimal)dynConstraints[14] , (decimal)dynConstraints[15] , (decimal)dynConstraints[16] , (decimal)dynConstraints[17] , (decimal)dynConstraints[18] , (decimal)dynConstraints[19] , (decimal)dynConstraints[20] , (short)dynConstraints[21] , (string)dynConstraints[22] , (decimal)dynConstraints[23] , (decimal)dynConstraints[24] , (decimal)dynConstraints[25] , (decimal)dynConstraints[26] , (decimal)dynConstraints[27] , (decimal)dynConstraints[28] , (decimal)dynConstraints[29] , (decimal)dynConstraints[30] , (short)dynConstraints[31] , (bool)dynConstraints[32] );
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
          Object[] prmH003R2;
          prmH003R2 = new Object[] {
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV63Configuracionempresawwds_2_tfconfiguracionempresaid",GXType.Int16,4,0) ,
          new ParDef("@AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to",GXType.Int16,4,0) ,
          new ParDef("@lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono",GXType.Char,20,0) ,
          new ParDef("@AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_se",GXType.Char,20,0) ,
          new ParDef("@AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandi",GXType.Number,12,2) ,
          new ParDef("@AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandi",GXType.Number,12,2) ,
          new ParDef("@GXPagingFrom2",GXType.Int32,9,0) ,
          new ParDef("@GXPagingTo2",GXType.Int32,9,0)
          };
          Object[] prmH003R3;
          prmH003R3 = new Object[] {
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV63Configuracionempresawwds_2_tfconfiguracionempresaid",GXType.Int16,4,0) ,
          new ParDef("@AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to",GXType.Int16,4,0) ,
          new ParDef("@lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono",GXType.Char,20,0) ,
          new ParDef("@AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_se",GXType.Char,20,0) ,
          new ParDef("@AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandi",GXType.Number,12,2) ,
          new ParDef("@AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandi",GXType.Number,12,2)
          };
          def= new CursorDef[] {
              new CursorDef("H003R2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH003R2,11, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("H003R3", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmH003R3,1, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[8])[0] = rslt.getString(9, 20);
                ((short[]) buf[9])[0] = rslt.getShort(10);
                return;
             case 1 :
                ((long[]) buf[0])[0] = rslt.getLong(1);
                return;
       }
    }

 }

}
