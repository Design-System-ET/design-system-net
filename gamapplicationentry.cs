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
   public class gamapplicationentry : GXDataArea
   {
      public gamapplicationentry( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamapplicationentry( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref string aP0_Gx_mode ,
                           ref long aP1_Id )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV71Id = aP1_Id;
         ExecuteImpl();
         aP0_Gx_mode=this.Gx_mode;
         aP1_Id=this.AV71Id;
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         chkavReturnmenuoptionswithoutpermission = new GXCheckbox();
         cmbavMainmenu = new GXCombobox();
         chkavUseabsoluteurlbyenvironment = new GXCheckbox();
         cmbavClientaccessstatus = new GXCombobox();
         chkavClientauthrequestmustincludeuserscopes = new GXCheckbox();
         chkavClientdonotshareuserids = new GXCheckbox();
         chkavClientallowremoteauth = new GXCheckbox();
         chkavClientallowgetuserdata = new GXCheckbox();
         chkavClientallowgetuseradddata = new GXCheckbox();
         chkavClientallowgetuserroles = new GXCheckbox();
         chkavClientallowgetsessioniniprop = new GXCheckbox();
         chkavClientallowgetsessionappdata = new GXCheckbox();
         chkavClientcallbackurliscustom = new GXCheckbox();
         chkavClientallowremoterestauth = new GXCheckbox();
         chkavClientallowgetuserdatarest = new GXCheckbox();
         chkavClientallowgetuseradddatarest = new GXCheckbox();
         chkavClientallowgetuserrolesrest = new GXCheckbox();
         chkavClientallowgetsessioniniproprest = new GXCheckbox();
         chkavClientallowgetsessionappdatarest = new GXCheckbox();
         chkavClientaccessuniquebyuser = new GXCheckbox();
         chkavAccessrequirespermission = new GXCheckbox();
         chkavIsauthorizationdelegated = new GXCheckbox();
         cmbavDelegateauthorizationversion = new GXCombobox();
         chkavSsorestenable = new GXCheckbox();
         cmbavSsorestmode = new GXCombobox();
         chkavSsorestserverurl_iscustom = new GXCheckbox();
         chkavStsprotocolenable = new GXCheckbox();
         cmbavStsmode = new GXCombobox();
         chkavMiniappenable = new GXCheckbox();
         cmbavMiniappmode = new GXCombobox();
         chkavMiniappclienturl_iscustom = new GXCheckbox();
         cmbavMiniappuserauthenticationtypename = new GXCombobox();
         chkavMiniappserverurl_iscustom = new GXCheckbox();
         chkavApikeyenable = new GXCheckbox();
         cmbavApikeyallowonlyauthenticationtypename = new GXCombobox();
         chkavApikeyallowscopecustomization = new GXCheckbox();
         chkavEnvironmentsecureprotocol = new GXCheckbox();
         chkavOnline = new GXCheckbox();
         chkavAutoregisteranomymoususer = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "Mode");
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
               gxfirstwebparm = GetFirstPar( "Mode");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "Mode");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridlanguages") == 0 )
            {
               gxnrGridlanguages_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridlanguages") == 0 )
            {
               gxgrGridlanguages_refresh_invoke( ) ;
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

      protected void gxnrGridlanguages_newrow_invoke( )
      {
         nRC_GXsfl_563 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_563"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_563_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_563_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_563_idx = GetPar( "sGXsfl_563_idx");
         chkavOnline.Enabled = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, chkavOnline_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavOnline.Enabled), 5, 0), !bGXsfl_563_Refreshing);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridlanguages_newrow( ) ;
         /* End function gxnrGridlanguages_newrow_invoke */
      }

      protected void gxgrGridlanguages_refresh_invoke( )
      {
         subGridlanguages_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGridlanguages_Rows"), "."), 18, MidpointRounding.ToEven));
         chkavOnline.Enabled = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, chkavOnline_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavOnline.Enabled), 5, 0), !bGXsfl_563_Refreshing);
         Gx_mode = GetPar( "Mode");
         AV92ReturnMenuOptionsWithoutPermission = StringUtil.StrToBool( GetPar( "ReturnMenuOptionsWithoutPermission"));
         AV109UseAbsoluteUrlByEnvironment = StringUtil.StrToBool( GetPar( "UseAbsoluteUrlByEnvironment"));
         AV31ClientAuthRequestMustIncludeUserScopes = StringUtil.StrToBool( GetPar( "ClientAuthRequestMustIncludeUserScopes"));
         AV35ClientDoNotShareUserIDs = StringUtil.StrToBool( GetPar( "ClientDoNotShareUserIDs"));
         AV29ClientAllowRemoteAuth = StringUtil.StrToBool( GetPar( "ClientAllowRemoteAuth"));
         AV25ClientAllowGetUserData = StringUtil.StrToBool( GetPar( "ClientAllowGetUserData"));
         AV23ClientAllowGetUserAddData = StringUtil.StrToBool( GetPar( "ClientAllowGetUserAddData"));
         AV27ClientAllowGetUserRoles = StringUtil.StrToBool( GetPar( "ClientAllowGetUserRoles"));
         AV21ClientAllowGetSessionIniProp = StringUtil.StrToBool( GetPar( "ClientAllowGetSessionIniProp"));
         AV19ClientAllowGetSessionAppData = StringUtil.StrToBool( GetPar( "ClientAllowGetSessionAppData"));
         AV33ClientCallbackURLisCustom = StringUtil.StrToBool( GetPar( "ClientCallbackURLisCustom"));
         AV30ClientAllowRemoteRestAuth = StringUtil.StrToBool( GetPar( "ClientAllowRemoteRestAuth"));
         AV26ClientAllowGetUserDataREST = StringUtil.StrToBool( GetPar( "ClientAllowGetUserDataREST"));
         AV24ClientAllowGetUserAddDataRest = StringUtil.StrToBool( GetPar( "ClientAllowGetUserAddDataRest"));
         AV28ClientAllowGetUserRolesRest = StringUtil.StrToBool( GetPar( "ClientAllowGetUserRolesRest"));
         AV22ClientAllowGetSessionIniPropRest = StringUtil.StrToBool( GetPar( "ClientAllowGetSessionIniPropRest"));
         AV20ClientAllowGetSessionAppDataREST = StringUtil.StrToBool( GetPar( "ClientAllowGetSessionAppDataREST"));
         AV16ClientAccessUniqueByUser = StringUtil.StrToBool( GetPar( "ClientAccessUniqueByUser"));
         AV7AccessRequiresPermission = StringUtil.StrToBool( GetPar( "AccessRequiresPermission"));
         AV72IsAuthorizationDelegated = StringUtil.StrToBool( GetPar( "IsAuthorizationDelegated"));
         AV94SSORestEnable = StringUtil.StrToBool( GetPar( "SSORestEnable"));
         AV99SSORestServerURL_isCustom = StringUtil.StrToBool( GetPar( "SSORestServerURL_isCustom"));
         AV105STSProtocolEnable = StringUtil.StrToBool( GetPar( "STSProtocolEnable"));
         AV83MiniAppEnable = StringUtil.StrToBool( GetPar( "MiniAppEnable"));
         AV82MiniAppClientURL_isCustom = StringUtil.StrToBool( GetPar( "MiniAppClientURL_isCustom"));
         AV87MiniAppServerURL_isCustom = StringUtil.StrToBool( GetPar( "MiniAppServerURL_isCustom"));
         AV11APIKeyEnable = StringUtil.StrToBool( GetPar( "APIKeyEnable"));
         AV10APIKeyAllowScopeCustomization = StringUtil.StrToBool( GetPar( "APIKeyAllowScopeCustomization"));
         AV56EnvironmentSecureProtocol = StringUtil.StrToBool( GetPar( "EnvironmentSecureProtocol"));
         AV14AutoRegisterAnomymousUser = StringUtil.StrToBool( GetPar( "AutoRegisterAnomymousUser"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridlanguages_refresh( subGridlanguages_Rows, Gx_mode, AV92ReturnMenuOptionsWithoutPermission, AV109UseAbsoluteUrlByEnvironment, AV31ClientAuthRequestMustIncludeUserScopes, AV35ClientDoNotShareUserIDs, AV29ClientAllowRemoteAuth, AV25ClientAllowGetUserData, AV23ClientAllowGetUserAddData, AV27ClientAllowGetUserRoles, AV21ClientAllowGetSessionIniProp, AV19ClientAllowGetSessionAppData, AV33ClientCallbackURLisCustom, AV30ClientAllowRemoteRestAuth, AV26ClientAllowGetUserDataREST, AV24ClientAllowGetUserAddDataRest, AV28ClientAllowGetUserRolesRest, AV22ClientAllowGetSessionIniPropRest, AV20ClientAllowGetSessionAppDataREST, AV16ClientAccessUniqueByUser, AV7AccessRequiresPermission, AV72IsAuthorizationDelegated, AV94SSORestEnable, AV99SSORestServerURL_isCustom, AV105STSProtocolEnable, AV83MiniAppEnable, AV82MiniAppClientURL_isCustom, AV87MiniAppServerURL_isCustom, AV11APIKeyEnable, AV10APIKeyAllowScopeCustomization, AV56EnvironmentSecureProtocol, AV14AutoRegisterAnomymousUser) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridlanguages_refresh_invoke */
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
            return "gamapplicationentry_Execute" ;
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
         PA142( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START142( ) ;
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
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("calendar-"+StringUtil.Substring( context.GetLanguageProperty( "culture"), 1, 2)+".js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/TabRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"true\" data-Skiponenter=\"false\"";
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamapplicationentry.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV71Id,12,0));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamapplicationentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_563", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_563), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDLANGUAGESPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV67GridLanguagesPageCount), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDLANGUAGESAPPLIEDFILTERS", AV115GridLanguagesAppliedFilters);
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "subGridlanguages_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Width", StringUtil.RTrim( Dvpanel_unnamedtable6_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable6_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable6_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Cls", StringUtil.RTrim( Dvpanel_unnamedtable6_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Title", StringUtil.RTrim( Dvpanel_unnamedtable6_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable6_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable6_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable6_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable6_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE6_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable6_Autoscroll));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Width", StringUtil.RTrim( Dvpanel_unnamedtable7_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Cls", StringUtil.RTrim( Dvpanel_unnamedtable7_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Title", StringUtil.RTrim( Dvpanel_unnamedtable7_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable7_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE7_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Autoscroll));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Width", StringUtil.RTrim( Dvpanel_unnamedtable8_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Cls", StringUtil.RTrim( Dvpanel_unnamedtable8_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Title", StringUtil.RTrim( Dvpanel_unnamedtable8_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable8_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE8_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Autoscroll));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Class", StringUtil.RTrim( Gridlanguagespaginationbar_Class));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Showfirst", StringUtil.BoolToStr( Gridlanguagespaginationbar_Showfirst));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Showprevious", StringUtil.BoolToStr( Gridlanguagespaginationbar_Showprevious));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Shownext", StringUtil.BoolToStr( Gridlanguagespaginationbar_Shownext));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Showlast", StringUtil.BoolToStr( Gridlanguagespaginationbar_Showlast));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Pagestoshow", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridlanguagespaginationbar_Pagestoshow), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Pagingbuttonsposition", StringUtil.RTrim( Gridlanguagespaginationbar_Pagingbuttonsposition));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Pagingcaptionposition", StringUtil.RTrim( Gridlanguagespaginationbar_Pagingcaptionposition));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Emptygridclass", StringUtil.RTrim( Gridlanguagespaginationbar_Emptygridclass));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageselector", StringUtil.BoolToStr( Gridlanguagespaginationbar_Rowsperpageselector));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridlanguagespaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageoptions", StringUtil.RTrim( Gridlanguagespaginationbar_Rowsperpageoptions));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Previous", StringUtil.RTrim( Gridlanguagespaginationbar_Previous));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Next", StringUtil.RTrim( Gridlanguagespaginationbar_Next));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Caption", StringUtil.RTrim( Gridlanguagespaginationbar_Caption));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Emptygridcaption", StringUtil.RTrim( Gridlanguagespaginationbar_Emptygridcaption));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Rowsperpagecaption", StringUtil.RTrim( Gridlanguagespaginationbar_Rowsperpagecaption));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS_Pagecount", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gxuitabspanel_tabs_Pagecount), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS_Class", StringUtil.RTrim( Gxuitabspanel_tabs_Class));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS_Historymanagement", StringUtil.BoolToStr( Gxuitabspanel_tabs_Historymanagement));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_EMPOWERER_Gridinternalname", StringUtil.RTrim( Gridlanguages_empowerer_Gridinternalname));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridlanguagespaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridlanguagespaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vONLINE_Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkavOnline.Enabled), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridlanguagespaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridlanguagespaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
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
            WE142( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT142( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return true ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamapplicationentry.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV71Id,12,0));
         return formatLink("gamapplicationentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "GAMApplicationEntry" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Application", "") ;
      }

      protected void WB140( )
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
            GxWebStd.gx_div_start( context, divLayoutmaintable_Internalname, 1, 0, "px", 0, "px", divLayoutmaintable_Class, "start", "top", "", "", "div");
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
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablecontent_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucGxuitabspanel_tabs.SetProperty("PageCount", Gxuitabspanel_tabs_Pagecount);
            ucGxuitabspanel_tabs.SetProperty("Class", Gxuitabspanel_tabs_Class);
            ucGxuitabspanel_tabs.SetProperty("HistoryManagement", Gxuitabspanel_tabs_Historymanagement);
            ucGxuitabspanel_tabs.Render(context, "tab", Gxuitabspanel_tabs_Internalname, "GXUITABSPANEL_TABSContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title1"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblGeneral_title_Internalname, context.GetMessage( "WWP_GAM_General", ""), "", "", lblGeneral_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "General") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel1"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable9_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavId_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavId_Internalname, context.GetMessage( "WWP_GAM_Id", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavId_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV71Id), 12, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavId_Enabled!=0) ? context.localUtil.Format( (decimal)(AV71Id), "ZZZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV71Id), "ZZZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,25);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavId_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavId_Enabled, 0, "text", "1", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMKeyNumLong", "end", false, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGuid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGuid_Internalname, context.GetMessage( "WWP_GAM_GUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGuid_Internalname, StringUtil.RTrim( AV68GUID), StringUtil.RTrim( context.localUtil.Format( AV68GUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,30);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGuid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavGuid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavName_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavName_Internalname, context.GetMessage( "WWP_GAM_Name", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavName_Internalname, StringUtil.RTrim( AV89Name), StringUtil.RTrim( context.localUtil.Format( AV89Name, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,35);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavName_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavName_Enabled, 1, "text", "", 80, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDsc_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDsc_Internalname, context.GetMessage( "WWP_GAM_Description", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDsc_Internalname, StringUtil.RTrim( AV50Dsc), StringUtil.RTrim( context.localUtil.Format( AV50Dsc, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,40);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDsc_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDsc_Enabled, 1, "text", "", 80, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavVersion_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavVersion_Internalname, context.GetMessage( "WWP_GAM_Version", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 45,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavVersion_Internalname, StringUtil.RTrim( AV111Version), StringUtil.RTrim( context.localUtil.Format( AV111Version, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,45);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavVersion_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavVersion_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCompany_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCompany_Internalname, context.GetMessage( "WWP_GAM_Company", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 50,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCompany_Internalname, StringUtil.RTrim( AV43Company), StringUtil.RTrim( context.localUtil.Format( AV43Company, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,50);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCompany_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCompany_Enabled, 1, "text", "", 80, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCopyright_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCopyright_Internalname, context.GetMessage( "WWP_GAM_Copyright", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 55,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCopyright_Internalname, StringUtil.RTrim( AV44Copyright), StringUtil.RTrim( context.localUtil.Format( AV44Copyright, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,55);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCopyright_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCopyright_Enabled, 1, "text", "", 80, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavReturnmenuoptionswithoutpermission_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavReturnmenuoptionswithoutpermission_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavReturnmenuoptionswithoutpermission_Internalname, StringUtil.BoolToStr( AV92ReturnMenuOptionsWithoutPermission), "", " ", 1, chkavReturnmenuoptionswithoutpermission.Enabled, "true", context.GetMessage( "WWP_GAM_Returnmenuoptionswithoutpermission", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(60, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,60);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavMainmenu_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavMainmenu_Internalname, context.GetMessage( "WWP_GAM_MainMenu", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 65,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavMainmenu, cmbavMainmenu_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV77MainMenu), 12, 0)), 1, cmbavMainmenu_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavMainmenu.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,65);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavMainmenu.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV77MainMenu), 12, 0));
            AssignProp("", false, cmbavMainmenu_Internalname, "Values", (string)(cmbavMainmenu.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavUseabsoluteurlbyenvironment_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavUseabsoluteurlbyenvironment_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 70,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavUseabsoluteurlbyenvironment_Internalname, StringUtil.BoolToStr( AV109UseAbsoluteUrlByEnvironment), "", " ", 1, chkavUseabsoluteurlbyenvironment.Enabled, "true", context.GetMessage( "WWP_GAM_UseAbsoluteURLByEnvironment", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(70, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,70);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavHomeobject_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavHomeobject_Internalname, context.GetMessage( "WWP_GAM_HomeObject", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 75,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavHomeobject_Internalname, AV69HomeObject, StringUtil.RTrim( context.localUtil.Format( AV69HomeObject, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,75);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavHomeobject_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavHomeobject_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAccountactivationobject_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAccountactivationobject_Internalname, context.GetMessage( "WWP_GAM_AccountActivationObject", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 80,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAccountactivationobject_Internalname, AV8AccountActivationObject, StringUtil.RTrim( context.localUtil.Format( AV8AccountActivationObject, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,80);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAccountactivationobject_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAccountactivationobject_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedlogoutobject_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblocklogoutobject_Internalname, context.GetMessage( "WWP_GAM_LocalLogoutObject", ""), "", "", lblTextblocklogoutobject_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_88_142( true) ;
         }
         else
         {
            wb_table1_88_142( false) ;
         }
         return  ;
      }

      protected void wb_table1_88_142e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title2"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblRemoteauthentication_title_Internalname, context.GetMessage( "WWP_GAM_OAuthAuthentication", ""), "", "", lblRemoteauthentication_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "RemoteAuthentication") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel2"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable5_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavClientaccessstatus_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavClientaccessstatus_Internalname, context.GetMessage( "WWP_GAM_Status", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 104,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavClientaccessstatus, cmbavClientaccessstatus_Internalname, StringUtil.RTrim( AV15ClientAccessStatus), 1, cmbavClientaccessstatus_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavClientaccessstatus.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,104);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavClientaccessstatus.CurrentValue = StringUtil.RTrim( AV15ClientAccessStatus);
            AssignProp("", false, cmbavClientaccessstatus_Internalname, "Values", (string)(cmbavClientaccessstatus.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavClientrevoked_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientrevoked_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientrevoked_Internalname, context.GetMessage( "WWP_GAM_Revoked", ""), " AttributeDateTimeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 109,'',false,'" + sGXsfl_563_idx + "',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavClientrevoked_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavClientrevoked_Internalname, context.localUtil.TToC( AV41ClientRevoked, 10, 8, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "), context.localUtil.Format( AV41ClientRevoked, "99/99/9999 99:99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onblur(this,109);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientrevoked_Jsonclick, 0, "AttributeDateTime", "", "", "", "", edtavClientrevoked_Visible, edtavClientrevoked_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMDateTime", "end", false, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_bitmap( context, edtavClientrevoked_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((edtavClientrevoked_Visible==0)||(edtavClientrevoked_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_GAMApplicationEntry.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientid_Internalname, context.GetMessage( "WWP_GAM_ClientId", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 114,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientid_Internalname, StringUtil.RTrim( AV37ClientId), StringUtil.RTrim( context.localUtil.Format( AV37ClientId, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,114);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMClientApplicationId", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedclientsecret_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockclientsecret_Internalname, context.GetMessage( "WWP_GAM_ClientSecret", ""), "", "", lblTextblockclientsecret_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table2_122_142( true) ;
         }
         else
         {
            wb_table2_122_142( false) ;
         }
         return  ;
      }

      protected void wb_table2_122_142e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientauthrequestmustincludeuserscopes_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientauthrequestmustincludeuserscopes_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 133,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientauthrequestmustincludeuserscopes_Internalname, StringUtil.BoolToStr( AV31ClientAuthRequestMustIncludeUserScopes), "", " ", 1, chkavClientauthrequestmustincludeuserscopes.Enabled, "true", context.GetMessage( "WWP_GAM_Authenticationrequestmustincludeuserscopes", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(133, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,133);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientdonotshareuserids_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientdonotshareuserids_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 138,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientdonotshareuserids_Internalname, StringUtil.BoolToStr( AV35ClientDoNotShareUserIDs), "", " ", 1, chkavClientdonotshareuserids.Enabled, "true", context.GetMessage( "WWP_GAM_DonotshareuserIDs", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(138, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,138);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable6.SetProperty("Width", Dvpanel_unnamedtable6_Width);
            ucDvpanel_unnamedtable6.SetProperty("AutoWidth", Dvpanel_unnamedtable6_Autowidth);
            ucDvpanel_unnamedtable6.SetProperty("AutoHeight", Dvpanel_unnamedtable6_Autoheight);
            ucDvpanel_unnamedtable6.SetProperty("Cls", Dvpanel_unnamedtable6_Cls);
            ucDvpanel_unnamedtable6.SetProperty("Title", Dvpanel_unnamedtable6_Title);
            ucDvpanel_unnamedtable6.SetProperty("Collapsible", Dvpanel_unnamedtable6_Collapsible);
            ucDvpanel_unnamedtable6.SetProperty("Collapsed", Dvpanel_unnamedtable6_Collapsed);
            ucDvpanel_unnamedtable6.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable6_Showcollapseicon);
            ucDvpanel_unnamedtable6.SetProperty("IconPosition", Dvpanel_unnamedtable6_Iconposition);
            ucDvpanel_unnamedtable6.SetProperty("AutoScroll", Dvpanel_unnamedtable6_Autoscroll);
            ucDvpanel_unnamedtable6.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable6_Internalname, "DVPANEL_UNNAMEDTABLE6Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_UNNAMEDTABLE6Container"+"UnnamedTable6"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable6_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowremoteauth_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowremoteauth_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 148,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowremoteauth_Internalname, StringUtil.BoolToStr( AV29ClientAllowRemoteAuth), "", " ", 1, chkavClientallowremoteauth.Enabled, "true", context.GetMessage( "WWP_GAM_AllowRemoteAuthentication", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,148);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblwebauth_Internalname, divTblwebauth_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetuserdata_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuserdata_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 156,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuserdata_Internalname, StringUtil.BoolToStr( AV25ClientAllowGetUserData), "", " ", 1, chkavClientallowgetuserdata.Enabled, "true", context.GetMessage( "WWP_GAM_Userdata", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(156, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,156);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetuseradddata_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuseradddata_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 161,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuseradddata_Internalname, StringUtil.BoolToStr( AV23ClientAllowGetUserAddData), "", " ", 1, chkavClientallowgetuseradddata.Enabled, "true", context.GetMessage( "WWP_GAM_CanGetUserAdditionalData", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(161, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,161);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetuserroles_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuserroles_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 166,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuserroles_Internalname, StringUtil.BoolToStr( AV27ClientAllowGetUserRoles), "", " ", 1, chkavClientallowgetuserroles.Enabled, "true", context.GetMessage( "WWP_GAM_CanGetUserRoles", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(166, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,166);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetsessioniniprop_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetsessioniniprop_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 171,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetsessioniniprop_Internalname, StringUtil.BoolToStr( AV21ClientAllowGetSessionIniProp), "", " ", 1, chkavClientallowgetsessioniniprop.Enabled, "true", context.GetMessage( "WWP_GAM_CanGetSessionInitialProperties", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(171, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,171);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetsessionappdata_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetsessionappdata_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 176,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetsessionappdata_Internalname, StringUtil.BoolToStr( AV19ClientAllowGetSessionAppData), "", " ", 1, chkavClientallowgetsessionappdata.Enabled, "true", context.GetMessage( "WWP_GAM_Sessionapplicationdata", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(176, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,176);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientallowadditionalscope_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientallowadditionalscope_Internalname, context.GetMessage( "WWP_GAM_Additionaluserscopes", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 181,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientallowadditionalscope_Internalname, AV17ClientAllowAdditionalScope, StringUtil.RTrim( context.localUtil.Format( AV17ClientAllowAdditionalScope, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,181);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientallowadditionalscope_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientallowadditionalscope_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientimageurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientimageurl_Internalname, context.GetMessage( "WWP_GAM_ImageUrl", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 186,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientimageurl_Internalname, AV38ClientImageURL, StringUtil.RTrim( context.localUtil.Format( AV38ClientImageURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,186);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientimageurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientimageurl_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientlocalloginurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientlocalloginurl_Internalname, context.GetMessage( "WWP_GAM_LocalLoginURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 191,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientlocalloginurl_Internalname, AV39ClientLocalLoginURL, StringUtil.RTrim( context.localUtil.Format( AV39ClientLocalLoginURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,191);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientlocalloginurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientlocalloginurl_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientcallbackurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientcallbackurl_Internalname, context.GetMessage( "WWP_GAM_CallbackURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 196,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientcallbackurl_Internalname, AV32ClientCallbackURL, StringUtil.RTrim( context.localUtil.Format( AV32ClientCallbackURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,196);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientcallbackurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientcallbackurl_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientcallbackurliscustom_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientcallbackurliscustom_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 201,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientcallbackurliscustom_Internalname, StringUtil.BoolToStr( AV33ClientCallbackURLisCustom), "", " ", 1, chkavClientcallbackurliscustom.Enabled, "true", context.GetMessage( "WWP_GAM_CallbackURLIsCustom", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(201, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,201);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientcallbackurlstatename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientcallbackurlstatename_Internalname, context.GetMessage( "WWP_GAM_CallbackURLState", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 206,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientcallbackurlstatename_Internalname, StringUtil.RTrim( AV34ClientCallbackURLStateName), StringUtil.RTrim( context.localUtil.Format( AV34ClientCallbackURLStateName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,206);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientcallbackurlstatename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientcallbackurlstatename_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable7.SetProperty("Width", Dvpanel_unnamedtable7_Width);
            ucDvpanel_unnamedtable7.SetProperty("AutoWidth", Dvpanel_unnamedtable7_Autowidth);
            ucDvpanel_unnamedtable7.SetProperty("AutoHeight", Dvpanel_unnamedtable7_Autoheight);
            ucDvpanel_unnamedtable7.SetProperty("Cls", Dvpanel_unnamedtable7_Cls);
            ucDvpanel_unnamedtable7.SetProperty("Title", Dvpanel_unnamedtable7_Title);
            ucDvpanel_unnamedtable7.SetProperty("Collapsible", Dvpanel_unnamedtable7_Collapsible);
            ucDvpanel_unnamedtable7.SetProperty("Collapsed", Dvpanel_unnamedtable7_Collapsed);
            ucDvpanel_unnamedtable7.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable7_Showcollapseicon);
            ucDvpanel_unnamedtable7.SetProperty("IconPosition", Dvpanel_unnamedtable7_Iconposition);
            ucDvpanel_unnamedtable7.SetProperty("AutoScroll", Dvpanel_unnamedtable7_Autoscroll);
            ucDvpanel_unnamedtable7.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable7_Internalname, "DVPANEL_UNNAMEDTABLE7Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_UNNAMEDTABLE7Container"+"UnnamedTable7"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable7_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowremoterestauth_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowremoterestauth_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 216,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowremoterestauth_Internalname, StringUtil.BoolToStr( AV30ClientAllowRemoteRestAuth), "", " ", 1, chkavClientallowremoterestauth.Enabled, "true", context.GetMessage( "WWP_GAM_AllowAuthenticationV20", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,216);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblrestauth_Internalname, divTblrestauth_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetuserdatarest_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuserdatarest_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 224,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuserdatarest_Internalname, StringUtil.BoolToStr( AV26ClientAllowGetUserDataREST), "", " ", 1, chkavClientallowgetuserdatarest.Enabled, "true", context.GetMessage( "WWP_GAM_Userdata", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(224, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,224);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetuseradddatarest_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuseradddatarest_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 229,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuseradddatarest_Internalname, StringUtil.BoolToStr( AV24ClientAllowGetUserAddDataRest), "", " ", 1, chkavClientallowgetuseradddatarest.Enabled, "true", context.GetMessage( "WWP_GAM_CanGetUserAdditionalData", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(229, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,229);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetuserrolesrest_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuserrolesrest_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 234,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuserrolesrest_Internalname, StringUtil.BoolToStr( AV28ClientAllowGetUserRolesRest), "", " ", 1, chkavClientallowgetuserrolesrest.Enabled, "true", context.GetMessage( "WWP_GAM_CanGetUserRoles", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(234, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,234);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetsessioniniproprest_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetsessioniniproprest_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 239,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetsessioniniproprest_Internalname, StringUtil.BoolToStr( AV22ClientAllowGetSessionIniPropRest), "", " ", 1, chkavClientallowgetsessioniniproprest.Enabled, "true", context.GetMessage( "WWP_GAM_CanGetSessionInitialProperties", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(239, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,239);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientallowgetsessionappdatarest_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetsessionappdatarest_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 244,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetsessionappdatarest_Internalname, StringUtil.BoolToStr( AV20ClientAllowGetSessionAppDataREST), "", " ", 1, chkavClientallowgetsessionappdatarest.Enabled, "true", context.GetMessage( "WWP_GAM_Sessionapplicationdata", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(244, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,244);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientallowadditionalscoperest_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientallowadditionalscoperest_Internalname, context.GetMessage( "WWP_GAM_Additionaluserscopes", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 249,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientallowadditionalscoperest_Internalname, AV18ClientAllowAdditionalScopeREST, StringUtil.RTrim( context.localUtil.Format( AV18ClientAllowAdditionalScopeREST, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,249);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientallowadditionalscoperest_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientallowadditionalscoperest_Enabled, 1, "text", "", 100, "%", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblgeneralauth_Internalname, divTblgeneralauth_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable8.SetProperty("Width", Dvpanel_unnamedtable8_Width);
            ucDvpanel_unnamedtable8.SetProperty("AutoWidth", Dvpanel_unnamedtable8_Autowidth);
            ucDvpanel_unnamedtable8.SetProperty("AutoHeight", Dvpanel_unnamedtable8_Autoheight);
            ucDvpanel_unnamedtable8.SetProperty("Cls", Dvpanel_unnamedtable8_Cls);
            ucDvpanel_unnamedtable8.SetProperty("Title", Dvpanel_unnamedtable8_Title);
            ucDvpanel_unnamedtable8.SetProperty("Collapsible", Dvpanel_unnamedtable8_Collapsible);
            ucDvpanel_unnamedtable8.SetProperty("Collapsed", Dvpanel_unnamedtable8_Collapsed);
            ucDvpanel_unnamedtable8.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable8_Showcollapseicon);
            ucDvpanel_unnamedtable8.SetProperty("IconPosition", Dvpanel_unnamedtable8_Iconposition);
            ucDvpanel_unnamedtable8.SetProperty("AutoScroll", Dvpanel_unnamedtable8_Autoscroll);
            ucDvpanel_unnamedtable8.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable8_Internalname, "DVPANEL_UNNAMEDTABLE8Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_UNNAMEDTABLE8Container"+"UnnamedTable8"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable8_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavClientaccessuniquebyuser_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientaccessuniquebyuser_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 262,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientaccessuniquebyuser_Internalname, StringUtil.BoolToStr( AV16ClientAccessUniqueByUser), "", " ", 1, chkavClientaccessuniquebyuser.Enabled, "true", context.GetMessage( "WWP_GAM_SingleUserAccess", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(262, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,262);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedclientencryptionkey_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockclientencryptionkey_Internalname, context.GetMessage( "WWP_GAM_PrivateEncryptionKey", ""), "", "", lblTextblockclientencryptionkey_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table3_270_142( true) ;
         }
         else
         {
            wb_table3_270_142( false) ;
         }
         return  ;
      }

      protected void wb_table3_270_142e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientrepositoryguid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientrepositoryguid_Internalname, context.GetMessage( "WWP_GAM_RepositoryGUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 281,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientrepositoryguid_Internalname, StringUtil.RTrim( AV40ClientRepositoryGUID), StringUtil.RTrim( context.localUtil.Format( AV40ClientRepositoryGUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,281);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientrepositoryguid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientrepositoryguid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title3"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblAuthorization_title_Internalname, context.GetMessage( "WWP_GAM_Authorization", ""), "", "", lblAuthorization_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Authorization") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel3"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable4_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAccessrequirespermission_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAccessrequirespermission_Internalname, context.GetMessage( "WWP_GAM_EnableAuthorization", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 291,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAccessrequirespermission_Internalname, StringUtil.BoolToStr( AV7AccessRequiresPermission), "", context.GetMessage( "WWP_GAM_EnableAuthorization", ""), 1, chkavAccessrequirespermission.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(291, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,291);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTbldelegateauthorization_Internalname, divTbldelegateauthorization_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavIsauthorizationdelegated_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavIsauthorizationdelegated_Internalname, context.GetMessage( "WWP_GAM_Delegateauthorizationcheckingtoanexternalprogram", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 299,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavIsauthorizationdelegated_Internalname, StringUtil.BoolToStr( AV72IsAuthorizationDelegated), "", context.GetMessage( "WWP_GAM_Delegateauthorizationcheckingtoanexternalprogram", ""), 1, chkavIsauthorizationdelegated.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(299, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,299);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTbldelegateauthorizationprop_Internalname, divTbldelegateauthorizationprop_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavDelegateauthorizationversion_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavDelegateauthorizationversion_Internalname, context.GetMessage( "WWP_GAM_Version", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 307,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavDelegateauthorizationversion, cmbavDelegateauthorizationversion_Internalname, StringUtil.RTrim( AV49DelegateAuthorizationVersion), 1, cmbavDelegateauthorizationversion_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavDelegateauthorizationversion.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,307);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavDelegateauthorizationversion.CurrentValue = StringUtil.RTrim( AV49DelegateAuthorizationVersion);
            AssignProp("", false, cmbavDelegateauthorizationversion_Internalname, "Values", (string)(cmbavDelegateauthorizationversion.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDelegateauthorizationfilename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDelegateauthorizationfilename_Internalname, context.GetMessage( "WWP_GAM_FileName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 312,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDelegateauthorizationfilename_Internalname, StringUtil.RTrim( AV46DelegateAuthorizationFileName), StringUtil.RTrim( context.localUtil.Format( AV46DelegateAuthorizationFileName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,312);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDelegateauthorizationfilename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDelegateauthorizationfilename_Enabled, 1, "text", "", 60, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDelegateauthorizationpackage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDelegateauthorizationpackage_Internalname, context.GetMessage( "WWP_GAM_PackageName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 317,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDelegateauthorizationpackage_Internalname, StringUtil.RTrim( AV48DelegateAuthorizationPackage), StringUtil.RTrim( context.localUtil.Format( AV48DelegateAuthorizationPackage, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,317);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDelegateauthorizationpackage_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDelegateauthorizationpackage_Enabled, 1, "text", "", 60, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDelegateauthorizationclassname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDelegateauthorizationclassname_Internalname, context.GetMessage( "WWP_GAM_ClassName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 322,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDelegateauthorizationclassname_Internalname, StringUtil.RTrim( AV45DelegateAuthorizationClassName), StringUtil.RTrim( context.localUtil.Format( AV45DelegateAuthorizationClassName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,322);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDelegateauthorizationclassname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDelegateauthorizationclassname_Enabled, 1, "text", "", 60, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDelegateauthorizationmethod_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDelegateauthorizationmethod_Internalname, context.GetMessage( "WWP_GAM_MethodName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 327,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDelegateauthorizationmethod_Internalname, StringUtil.RTrim( AV47DelegateAuthorizationMethod), StringUtil.RTrim( context.localUtil.Format( AV47DelegateAuthorizationMethod, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,327);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDelegateauthorizationmethod_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDelegateauthorizationmethod_Enabled, 1, "text", "", 60, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title4"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblSsorest_title_Internalname, context.GetMessage( "WWP_GAM_SSORest", ""), "", "", lblSsorest_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "SSORest") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel4"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSsorestenable_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSsorestenable_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 337,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSsorestenable_Internalname, StringUtil.BoolToStr( AV94SSORestEnable), "", " ", 1, chkavSsorestenable.Enabled, "true", context.GetMessage( "WWP_GAM_EnableSSORestServices", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,337);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablessorest_Internalname, divTablessorest_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavSsorestmode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavSsorestmode_Internalname, context.GetMessage( "WWP_GAM_ModeSSORest", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 345,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavSsorestmode, cmbavSsorestmode_Internalname, StringUtil.RTrim( AV95SSORestMode), 1, cmbavSsorestmode_Jsonclick, 5, "'"+""+"'"+",false,"+"'"+"EVSSORESTMODE.CLICK."+"'", "char", "", 1, cmbavSsorestmode.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,345);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavSsorestmode.CurrentValue = StringUtil.RTrim( AV95SSORestMode);
            AssignProp("", false, cmbavSsorestmode_Internalname, "Values", (string)(cmbavSsorestmode.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblssorestmodeclient_Internalname, divTblssorestmodeclient_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSsorestuserauthtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSsorestuserauthtypename_Internalname, context.GetMessage( "WWP_GAM_UserAuthenticationTypeNameInServer", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 353,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSsorestuserauthtypename_Internalname, StringUtil.RTrim( AV101SSORestUserAuthTypeName), StringUtil.RTrim( context.localUtil.Format( AV101SSORestUserAuthTypeName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,353);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSsorestuserauthtypename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSsorestuserauthtypename_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMAuthenticationTypeName", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSsorestserverurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSsorestserverurl_Internalname, context.GetMessage( "WWP_GAM_ServerURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 358,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSsorestserverurl_Internalname, AV98SSORestServerURL, StringUtil.RTrim( context.localUtil.Format( AV98SSORestServerURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,358);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSsorestserverurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSsorestserverurl_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSsorestserverurl_iscustom_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSsorestserverurl_iscustom_Internalname, context.GetMessage( "WWP_GAM_CustomserverURLSSO", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 363,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSsorestserverurl_iscustom_Internalname, StringUtil.BoolToStr( AV99SSORestServerURL_isCustom), "", context.GetMessage( "WWP_GAM_CustomserverURLSSO", ""), 1, chkavSsorestserverurl_iscustom.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(363, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,363);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSsorestserverurl_slo_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSsorestserverurl_slo_Internalname, context.GetMessage( "WWP_GAM_ServerURLSLO", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 368,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSsorestserverurl_slo_Internalname, AV100SSORestServerURL_SLO, StringUtil.RTrim( context.localUtil.Format( AV100SSORestServerURL_SLO, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,368);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSsorestserverurl_slo_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSsorestserverurl_slo_Enabled, 1, "text", "", 60, "chr", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSsorestserverrepositoryguid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSsorestserverrepositoryguid_Internalname, context.GetMessage( "WWP_GAM_ServerrepositoryGUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 373,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSsorestserverrepositoryguid_Internalname, StringUtil.RTrim( AV97SSORestServerRepositoryGUID), StringUtil.RTrim( context.localUtil.Format( AV97SSORestServerRepositoryGUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,373);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSsorestserverrepositoryguid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSsorestserverrepositoryguid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSsorestserverkey_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSsorestserverkey_Internalname, context.GetMessage( "WWP_GAM_Serverencryptionkey", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 378,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSsorestserverkey_Internalname, StringUtil.RTrim( AV96SSORestServerKey), StringUtil.RTrim( context.localUtil.Format( AV96SSORestServerKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,378);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSsorestserverkey_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSsorestserverkey_Enabled, 1, "text", "", 32, "chr", 1, "row", 32, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMEncryptionKey", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title5"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblSts_title_Internalname, context.GetMessage( "WWP_GAM_STS", ""), "", "", lblSts_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "STS") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel5"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavStsprotocolenable_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavStsprotocolenable_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 388,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavStsprotocolenable_Internalname, StringUtil.BoolToStr( AV105STSProtocolEnable), "", " ", 1, chkavStsprotocolenable.Enabled, "true", context.GetMessage( "WWP_GAM_EnableSTSProtocol", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,388);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablests_Internalname, divTablests_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavStsmode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavStsmode_Internalname, context.GetMessage( "WWP_GAM_STSMode", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 396,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavStsmode, cmbavStsmode_Internalname, StringUtil.RTrim( AV104STSMode), 1, cmbavStsmode_Jsonclick, 7, "'"+""+"'"+",false,"+"'"+"e11141_client"+"'", "char", "", 1, cmbavStsmode.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,396);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavStsmode.CurrentValue = StringUtil.RTrim( AV104STSMode);
            AssignProp("", false, cmbavStsmode_Internalname, "Values", (string)(cmbavStsmode.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablestsserverchecktoken_Internalname, divTablestsserverchecktoken_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavStsauthorizationusername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavStsauthorizationusername_Internalname, context.GetMessage( "WWP_GAM_UserName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 404,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavStsauthorizationusername_Internalname, AV103STSAuthorizationUserName, StringUtil.RTrim( context.localUtil.Format( AV103STSAuthorizationUserName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,404);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavStsauthorizationusername_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavStsauthorizationusername_Enabled, 1, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            GxWebStd.gx_div_start( context, divTablestsclientgettoken_Internalname, divTablestsclientgettoken_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divStsserverclientpassword_cell_Internalname, 1, 0, "px", 0, "px", divStsserverclientpassword_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavStsserverclientpassword_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavStsserverclientpassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavStsserverclientpassword_Internalname, context.GetMessage( "WWP_GAM_ClientPassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 412,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavStsserverclientpassword_Internalname, StringUtil.RTrim( AV106STSServerClientPassword), StringUtil.RTrim( context.localUtil.Format( AV106STSServerClientPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,412);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavStsserverclientpassword_Jsonclick, 0, "Attribute", "", "", "", "", edtavStsserverclientpassword_Visible, edtavStsserverclientpassword_Enabled, 1, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            GxWebStd.gx_div_start( context, divTablestsclient_Internalname, divTablestsclient_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavStsserverurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavStsserverurl_Internalname, context.GetMessage( "WWP_GAM_ServerURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 420,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavStsserverurl_Internalname, AV108STSServerURL, StringUtil.RTrim( context.localUtil.Format( AV108STSServerURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,420);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavStsserverurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavStsserverurl_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavStsserverrepositoryguid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavStsserverrepositoryguid_Internalname, context.GetMessage( "WWP_GAM_RepositoryGUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 425,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavStsserverrepositoryguid_Internalname, StringUtil.RTrim( AV107STSServerRepositoryGUID), StringUtil.RTrim( context.localUtil.Format( AV107STSServerRepositoryGUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,425);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavStsserverrepositoryguid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavStsserverrepositoryguid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title6"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblMiniapp_title_Internalname, context.GetMessage( "WWP_GAM_MiniApp", ""), "", "", lblMiniapp_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "MiniApp") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel6"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divMiniapptable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavMiniappenable_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavMiniappenable_Internalname, context.GetMessage( "WWP_GAM_EnableworkasMiniApp", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 435,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavMiniappenable_Internalname, StringUtil.BoolToStr( AV83MiniAppEnable), "", context.GetMessage( "WWP_GAM_EnableworkasMiniApp", ""), 1, chkavMiniappenable.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(435, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,435);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblminiapp_Internalname, divTblminiapp_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavMiniappmode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavMiniappmode_Internalname, context.GetMessage( "WWP_GAM_Mode", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 443,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavMiniappmode, cmbavMiniappmode_Internalname, StringUtil.RTrim( AV84MiniAppMode), 1, cmbavMiniappmode_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavMiniappmode.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,443);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavMiniappmode.CurrentValue = StringUtil.RTrim( AV84MiniAppMode);
            AssignProp("", false, cmbavMiniappmode_Internalname, "Values", (string)(cmbavMiniappmode.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblminiappserver_Internalname, divTblminiappserver_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavMiniappclienturl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMiniappclienturl_Internalname, context.GetMessage( "WWP_GAM_MiniAppclientURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 451,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMiniappclienturl_Internalname, AV81MiniAppClientURL, StringUtil.RTrim( context.localUtil.Format( AV81MiniAppClientURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,451);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMiniappclienturl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavMiniappclienturl_Enabled, 1, "text", "", 100, "%", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavMiniappclienturl_iscustom_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavMiniappclienturl_iscustom_Internalname, context.GetMessage( "WWP_GAM_CustomMiniAppclientURL", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 456,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavMiniappclienturl_iscustom_Internalname, StringUtil.BoolToStr( AV82MiniAppClientURL_isCustom), "", context.GetMessage( "WWP_GAM_CustomMiniAppclientURL", ""), 1, chkavMiniappclienturl_iscustom.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(456, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,456);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavMiniappclientrepositoryguid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMiniappclientrepositoryguid_Internalname, context.GetMessage( "WWP_GAM_MiniAppclientrepositoryGUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 461,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMiniappclientrepositoryguid_Internalname, StringUtil.RTrim( AV80MiniAppClientRepositoryGUID), StringUtil.RTrim( context.localUtil.Format( AV80MiniAppClientRepositoryGUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,461);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMiniappclientrepositoryguid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavMiniappclientrepositoryguid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            GxWebStd.gx_div_start( context, divTblminiappclient_Internalname, divTblminiappclient_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavMiniappuserauthenticationtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavMiniappuserauthenticationtypename_Internalname, context.GetMessage( "WWP_GAM_Userauthenticationtypenameinthisclient", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 469,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavMiniappuserauthenticationtypename, cmbavMiniappuserauthenticationtypename_Internalname, StringUtil.RTrim( AV88MiniAppUserAuthenticationTypeName), 1, cmbavMiniappuserauthenticationtypename_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavMiniappuserauthenticationtypename.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,469);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavMiniappuserauthenticationtypename.CurrentValue = StringUtil.RTrim( AV88MiniAppUserAuthenticationTypeName);
            AssignProp("", false, cmbavMiniappuserauthenticationtypename_Internalname, "Values", (string)(cmbavMiniappuserauthenticationtypename.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavMiniappserverurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMiniappserverurl_Internalname, context.GetMessage( "WWP_GAM_SuperAppserverURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 474,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMiniappserverurl_Internalname, AV86MiniAppServerURL, StringUtil.RTrim( context.localUtil.Format( AV86MiniAppServerURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,474);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMiniappserverurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavMiniappserverurl_Enabled, 1, "text", "", 100, "%", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavMiniappserverurl_iscustom_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavMiniappserverurl_iscustom_Internalname, context.GetMessage( "WWP_GAM_CustomSuperAppserverURL", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 479,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavMiniappserverurl_iscustom_Internalname, StringUtil.BoolToStr( AV87MiniAppServerURL_isCustom), "", context.GetMessage( "WWP_GAM_CustomSuperAppserverURL", ""), 1, chkavMiniappserverurl_iscustom.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(479, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,479);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavMiniappserverrepositoryguid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMiniappserverrepositoryguid_Internalname, context.GetMessage( "WWP_GAM_SuperApprepositoryGUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 484,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMiniappserverrepositoryguid_Internalname, StringUtil.RTrim( AV85MiniAppServerRepositoryGUID), StringUtil.RTrim( context.localUtil.Format( AV85MiniAppServerRepositoryGUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,484);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMiniappserverrepositoryguid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavMiniappserverrepositoryguid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMApplicationEntry.htm");
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
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title7"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblApikey_title_Internalname, context.GetMessage( "WWP_GAM_APIkey", ""), "", "", lblApikey_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "APIkey") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel7"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divApikeytable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavApikeyenable_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavApikeyenable_Internalname, context.GetMessage( "WWP_GAM_EnableworkwithAPIkeys", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 494,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavApikeyenable_Internalname, StringUtil.BoolToStr( AV11APIKeyEnable), "", context.GetMessage( "WWP_GAM_EnableworkwithAPIkeys", ""), 1, chkavApikeyenable.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(494, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,494);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblapikey_Internalname, divTblapikey_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavApikeytimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavApikeytimeout_Internalname, context.GetMessage( "WWP_GAM_APIkeytimeoutinhours", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 502,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavApikeytimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV12APIKeyTimeout), 9, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV12APIKeyTimeout), "ZZZZZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,502);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavApikeytimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavApikeytimeout_Enabled, 1, "text", "1", 9, "chr", 1, "row", 9, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMKeyNumShort", "end", false, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavApikeyallowonlyauthenticationtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavApikeyallowonlyauthenticationtypename_Internalname, context.GetMessage( "WWP_GAM_Allowonlythisauthenticationtypename", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 507,'',false,'" + sGXsfl_563_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavApikeyallowonlyauthenticationtypename, cmbavApikeyallowonlyauthenticationtypename_Internalname, StringUtil.RTrim( AV9APIKeyAllowOnlyAuthenticationTypeName), 1, cmbavApikeyallowonlyauthenticationtypename_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavApikeyallowonlyauthenticationtypename.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,507);\"", "", true, 0, "HLP_GAMApplicationEntry.htm");
            cmbavApikeyallowonlyauthenticationtypename.CurrentValue = StringUtil.RTrim( AV9APIKeyAllowOnlyAuthenticationTypeName);
            AssignProp("", false, cmbavApikeyallowonlyauthenticationtypename_Internalname, "Values", (string)(cmbavApikeyallowonlyauthenticationtypename.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavApikeyallowscopecustomization_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavApikeyallowscopecustomization_Internalname, context.GetMessage( "WWP_GAM_APIKeyAllowScopeCustomization", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 512,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavApikeyallowscopecustomization_Internalname, StringUtil.BoolToStr( AV10APIKeyAllowScopeCustomization), "", context.GetMessage( "WWP_GAM_APIKeyAllowScopeCustomization", ""), 1, chkavApikeyallowscopecustomization.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(512, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,512);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title8"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblEnvironmentsettings_title_Internalname, context.GetMessage( "WWP_GAM_EnvironmentSettings", ""), "", "", lblEnvironmentsettings_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "EnvironmentSettings") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel8"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEnvironmentname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEnvironmentname_Internalname, context.GetMessage( "WWP_GAM_Name", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 522,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEnvironmentname_Internalname, StringUtil.RTrim( AV52EnvironmentName), StringUtil.RTrim( context.localUtil.Format( AV52EnvironmentName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,522);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEnvironmentname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEnvironmentname_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEnvironmentsecureprotocol_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEnvironmentsecureprotocol_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 527,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEnvironmentsecureprotocol_Internalname, StringUtil.BoolToStr( AV56EnvironmentSecureProtocol), "", " ", 1, chkavEnvironmentsecureprotocol.Enabled, "true", context.GetMessage( "WWP_GAM_IsHttps", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(527, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,527);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEnvironmenthost_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEnvironmenthost_Internalname, context.GetMessage( "WWP_GAM_Host", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 532,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEnvironmenthost_Internalname, StringUtil.RTrim( AV51EnvironmentHost), StringUtil.RTrim( context.localUtil.Format( AV51EnvironmentHost, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,532);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEnvironmenthost_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEnvironmenthost_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEnvironmentport_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEnvironmentport_Internalname, context.GetMessage( "WWP_GAM_Port", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 537,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEnvironmentport_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV53EnvironmentPort), 5, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV53EnvironmentPort), "ZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,537);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEnvironmentport_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEnvironmentport_Enabled, 1, "text", "1", 5, "chr", 1, "row", 5, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEnvironmentvirtualdirectory_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEnvironmentvirtualdirectory_Internalname, context.GetMessage( "WWP_GAM_VirtualDirectory", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 542,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEnvironmentvirtualdirectory_Internalname, StringUtil.RTrim( AV57EnvironmentVirtualDirectory), StringUtil.RTrim( context.localUtil.Format( AV57EnvironmentVirtualDirectory, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,542);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEnvironmentvirtualdirectory_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEnvironmentvirtualdirectory_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEnvironmentprogrampackage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEnvironmentprogrampackage_Internalname, context.GetMessage( "WWP_GAM_Package", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 547,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEnvironmentprogrampackage_Internalname, StringUtil.RTrim( AV55EnvironmentProgramPackage), StringUtil.RTrim( context.localUtil.Format( AV55EnvironmentProgramPackage, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,547);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEnvironmentprogrampackage_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEnvironmentprogrampackage_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEnvironmentprogramextension_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEnvironmentprogramextension_Internalname, context.GetMessage( "WWP_GAM_Extension", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 552,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEnvironmentprogramextension_Internalname, StringUtil.RTrim( AV54EnvironmentProgramExtension), StringUtil.RTrim( context.localUtil.Format( AV54EnvironmentProgramExtension, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,552);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEnvironmentprogramextension_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEnvironmentprogramextension_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title9"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblLanguages_title_Internalname, context.GetMessage( "WWP_GAM_Languages", ""), "", "", lblLanguages_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMApplicationEntry.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Languages") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel9"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divLanguagestable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 HasGridEmpowerer", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGridlanguagestablewithpaginationbar_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridlanguagesContainer.SetWrapped(nGXWrapped);
            StartGridControl563( ) ;
         }
         if ( wbEnd == 563 )
         {
            wbEnd = 0;
            nRC_GXsfl_563 = (int)(nGXsfl_563_idx-1);
            if ( GridlanguagesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridlanguagesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridlanguages", GridlanguagesContainer, subGridlanguages_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridlanguagesContainerData", GridlanguagesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridlanguagesContainerData"+"V", GridlanguagesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridlanguagesContainerData"+"V"+"\" value='"+GridlanguagesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucGridlanguagespaginationbar.SetProperty("Class", Gridlanguagespaginationbar_Class);
            ucGridlanguagespaginationbar.SetProperty("ShowFirst", Gridlanguagespaginationbar_Showfirst);
            ucGridlanguagespaginationbar.SetProperty("ShowPrevious", Gridlanguagespaginationbar_Showprevious);
            ucGridlanguagespaginationbar.SetProperty("ShowNext", Gridlanguagespaginationbar_Shownext);
            ucGridlanguagespaginationbar.SetProperty("ShowLast", Gridlanguagespaginationbar_Showlast);
            ucGridlanguagespaginationbar.SetProperty("PagesToShow", Gridlanguagespaginationbar_Pagestoshow);
            ucGridlanguagespaginationbar.SetProperty("PagingButtonsPosition", Gridlanguagespaginationbar_Pagingbuttonsposition);
            ucGridlanguagespaginationbar.SetProperty("PagingCaptionPosition", Gridlanguagespaginationbar_Pagingcaptionposition);
            ucGridlanguagespaginationbar.SetProperty("EmptyGridClass", Gridlanguagespaginationbar_Emptygridclass);
            ucGridlanguagespaginationbar.SetProperty("RowsPerPageSelector", Gridlanguagespaginationbar_Rowsperpageselector);
            ucGridlanguagespaginationbar.SetProperty("RowsPerPageOptions", Gridlanguagespaginationbar_Rowsperpageoptions);
            ucGridlanguagespaginationbar.SetProperty("Previous", Gridlanguagespaginationbar_Previous);
            ucGridlanguagespaginationbar.SetProperty("Next", Gridlanguagespaginationbar_Next);
            ucGridlanguagespaginationbar.SetProperty("Caption", Gridlanguagespaginationbar_Caption);
            ucGridlanguagespaginationbar.SetProperty("EmptyGridCaption", Gridlanguagespaginationbar_Emptygridcaption);
            ucGridlanguagespaginationbar.SetProperty("RowsPerPageCaption", Gridlanguagespaginationbar_Rowsperpagecaption);
            ucGridlanguagespaginationbar.SetProperty("CurrentPage", AV66GridLanguagesCurrentPage);
            ucGridlanguagespaginationbar.SetProperty("PageCount", AV67GridLanguagesPageCount);
            ucGridlanguagespaginationbar.SetProperty("AppliedFilters", AV115GridLanguagesAppliedFilters);
            ucGridlanguagespaginationbar.Render(context, "dvelop.dvpaginationbar", Gridlanguagespaginationbar_Internalname, "GRIDLANGUAGESPAGINATIONBARContainer");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 573,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(563), 3, 0)+","+"null"+");", bttBtnenter_Caption, bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtnenter_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 575,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(563), 3, 0)+","+"null"+");", context.GetMessage( "GX_BtnCancel", ""), bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMApplicationEntry.htm");
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
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 579,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGridlanguagescurrentpage_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV66GridLanguagesCurrentPage), 10, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV66GridLanguagesCurrentPage), "ZZZZZZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,579);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGridlanguagescurrentpage_Jsonclick, 0, "Attribute", "", "", "", "", edtavGridlanguagescurrentpage_Visible, 1, 0, "text", "1", 10, "chr", 1, "row", 10, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMApplicationEntry.htm");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 580,'',false,'" + sGXsfl_563_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAutoregisteranomymoususer_Internalname, StringUtil.BoolToStr( AV14AutoRegisterAnomymousUser), "", "", chkavAutoregisteranomymoususer.Visible, 1, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(580, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,580);\"");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 581,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavStsauthorizationuserguid_Internalname, StringUtil.RTrim( AV102STSAuthorizationUserGUID), StringUtil.RTrim( context.localUtil.Format( AV102STSAuthorizationUserGUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,581);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavStsauthorizationuserguid_Jsonclick, 0, "Attribute", "", "", "", "", edtavStsauthorizationuserguid_Visible, 1, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMApplicationEntry.htm");
            /* User Defined Control */
            ucGridlanguages_empowerer.Render(context, "wwp.gridempowerer", Gridlanguages_empowerer_Internalname, "GRIDLANGUAGES_EMPOWERERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 563 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridlanguagesContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridlanguagesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridlanguages", GridlanguagesContainer, subGridlanguages_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridlanguagesContainerData", GridlanguagesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridlanguagesContainerData"+"V", GridlanguagesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridlanguagesContainerData"+"V"+"\" value='"+GridlanguagesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START142( )
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
         Form.Meta.addItem("description", context.GetMessage( "Application", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP140( ) ;
      }

      protected void WS142( )
      {
         START142( ) ;
         EVT142( ) ;
      }

      protected void EVT142( )
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
                           else if ( StringUtil.StrCmp(sEvt, "GRIDLANGUAGESPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridlanguagespaginationbar.Changepage */
                              E12142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDLANGUAGESPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridlanguagespaginationbar.Changerowsperpage */
                              E13142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOCHANGECLIENTSECRET'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoChangeClientSecret' */
                              E14142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOGENERATEKEYGAMREMOTE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoGenerateKeyGAMRemote' */
                              E15142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOREVOKEALLOW'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoRevokeAllow' */
                              E16142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ! wbErr )
                              {
                                 Rfr0gs = false;
                                 if ( ! Rfr0gs )
                                 {
                                    /* Execute user event: Enter */
                                    E17142 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VSSORESTENABLE.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E18142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VSSORESTMODE.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E19142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VMINIAPPENABLE.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E20142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VMINIAPPMODE.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E21142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VAPIKEYENABLE.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E22142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VSSORESTMODE.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E23142 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VSSORESTENABLE.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E24142 ();
                              /* No code required for Cancel button. It is implemented as the Reset button. */
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "GRIDLANGUAGES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 22), "'GENERATEKEYGAMREMOTE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "'REVOKE-AUTHORIZE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'TRANSLATIONS'") == 0 ) )
                           {
                              nGXsfl_563_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_563_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_563_idx), 4, 0), 4, "0");
                              SubsflControlProps_5632( ) ;
                              AV90Online = StringUtil.StrToBool( cgiGet( chkavOnline_Internalname));
                              AssignAttri("", false, chkavOnline_Internalname, AV90Online);
                              AV75Language = cgiGet( edtavLanguage_Internalname);
                              AssignAttri("", false, edtavLanguage_Internalname, AV75Language);
                              GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE"+"_"+sGXsfl_563_idx, GetSecureSignedToken( sGXsfl_563_idx, StringUtil.RTrim( context.localUtil.Format( AV75Language, "")), context));
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E25142 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E26142 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDLANGUAGES.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridlanguages.Load */
                                    E27142 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'GENERATEKEYGAMREMOTE'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'GenerateKeyGAMRemote' */
                                    E28142 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'REVOKE-AUTHORIZE'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Revoke-Authorize' */
                                    E29142 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'TRANSLATIONS'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Translations' */
                                    E30142 ();
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

      protected void WE142( )
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

      protected void PA142( )
      {
         if ( nDonePA == 0 )
         {
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               GxWebError = 1;
               context.HttpContext.Response.StatusCode = 403;
               context.WriteHtmlText( "<title>403 Forbidden</title>") ;
               context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
               context.WriteHtmlText( "<p /><hr />") ;
               GXUtil.WriteLog("send_http_error_code " + 403.ToString());
            }
            if ( ( StringUtil.StrCmp(context.GetRequestQueryString( ), "") != 0 ) && ( GxWebError == 0 ) && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               GXDecQS = UriDecrypt64( context.GetRequestQueryString( ), GXKey);
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamapplicationentry.aspx")), "gamapplicationentry.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamapplicationentry.aspx")))) ;
               }
               else
               {
                  GxWebError = 1;
                  context.HttpContext.Response.StatusCode = 403;
                  context.WriteHtmlText( "<title>403 Forbidden</title>") ;
                  context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
                  context.WriteHtmlText( "<p /><hr />") ;
                  GXUtil.WriteLog("send_http_error_code " + 403.ToString());
               }
            }
            if ( ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               if ( nGotPars == 0 )
               {
                  entryPointCalled = false;
                  gxfirstwebparm = GetFirstPar( "Mode");
                  toggleJsOutput = isJsOutputEnabled( );
                  if ( context.isSpaRequest( ) )
                  {
                     disableJsOutput();
                  }
                  if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
                  {
                     Gx_mode = gxfirstwebparm;
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                     if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") != 0 )
                     {
                        AV71Id = (long)(Math.Round(NumberUtil.Val( GetPar( "Id"), "."), 18, MidpointRounding.ToEven));
                        AssignAttri("", false, "AV71Id", StringUtil.LTrimStr( (decimal)(AV71Id), 12, 0));
                     }
                  }
                  if ( toggleJsOutput )
                  {
                     if ( context.isSpaRequest( ) )
                     {
                        enableJsOutput();
                     }
                  }
               }
            }
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
               GX_FocusControl = edtavGuid_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridlanguages_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_5632( ) ;
         while ( nGXsfl_563_idx <= nRC_GXsfl_563 )
         {
            sendrow_5632( ) ;
            nGXsfl_563_idx = ((subGridlanguages_Islastpage==1)&&(nGXsfl_563_idx+1>subGridlanguages_fnc_Recordsperpage( )) ? 1 : nGXsfl_563_idx+1);
            sGXsfl_563_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_563_idx), 4, 0), 4, "0");
            SubsflControlProps_5632( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridlanguagesContainer)) ;
         /* End function gxnrGridlanguages_newrow */
      }

      protected void gxgrGridlanguages_refresh( int subGridlanguages_Rows ,
                                                string Gx_mode ,
                                                bool AV92ReturnMenuOptionsWithoutPermission ,
                                                bool AV109UseAbsoluteUrlByEnvironment ,
                                                bool AV31ClientAuthRequestMustIncludeUserScopes ,
                                                bool AV35ClientDoNotShareUserIDs ,
                                                bool AV29ClientAllowRemoteAuth ,
                                                bool AV25ClientAllowGetUserData ,
                                                bool AV23ClientAllowGetUserAddData ,
                                                bool AV27ClientAllowGetUserRoles ,
                                                bool AV21ClientAllowGetSessionIniProp ,
                                                bool AV19ClientAllowGetSessionAppData ,
                                                bool AV33ClientCallbackURLisCustom ,
                                                bool AV30ClientAllowRemoteRestAuth ,
                                                bool AV26ClientAllowGetUserDataREST ,
                                                bool AV24ClientAllowGetUserAddDataRest ,
                                                bool AV28ClientAllowGetUserRolesRest ,
                                                bool AV22ClientAllowGetSessionIniPropRest ,
                                                bool AV20ClientAllowGetSessionAppDataREST ,
                                                bool AV16ClientAccessUniqueByUser ,
                                                bool AV7AccessRequiresPermission ,
                                                bool AV72IsAuthorizationDelegated ,
                                                bool AV94SSORestEnable ,
                                                bool AV99SSORestServerURL_isCustom ,
                                                bool AV105STSProtocolEnable ,
                                                bool AV83MiniAppEnable ,
                                                bool AV82MiniAppClientURL_isCustom ,
                                                bool AV87MiniAppServerURL_isCustom ,
                                                bool AV11APIKeyEnable ,
                                                bool AV10APIKeyAllowScopeCustomization ,
                                                bool AV56EnvironmentSecureProtocol ,
                                                bool AV14AutoRegisterAnomymousUser )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDLANGUAGES_nCurrentRecord = 0;
         RF142( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridlanguages_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV75Language, "")), context));
         GxWebStd.gx_hidden_field( context, "vLANGUAGE", AV75Language);
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
         AV92ReturnMenuOptionsWithoutPermission = StringUtil.StrToBool( StringUtil.BoolToStr( AV92ReturnMenuOptionsWithoutPermission));
         AssignAttri("", false, "AV92ReturnMenuOptionsWithoutPermission", AV92ReturnMenuOptionsWithoutPermission);
         if ( cmbavMainmenu.ItemCount > 0 )
         {
            AV77MainMenu = (long)(Math.Round(NumberUtil.Val( cmbavMainmenu.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV77MainMenu), 12, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV77MainMenu", StringUtil.LTrimStr( (decimal)(AV77MainMenu), 12, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavMainmenu.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV77MainMenu), 12, 0));
            AssignProp("", false, cmbavMainmenu_Internalname, "Values", cmbavMainmenu.ToJavascriptSource(), true);
         }
         AV109UseAbsoluteUrlByEnvironment = StringUtil.StrToBool( StringUtil.BoolToStr( AV109UseAbsoluteUrlByEnvironment));
         AssignAttri("", false, "AV109UseAbsoluteUrlByEnvironment", AV109UseAbsoluteUrlByEnvironment);
         if ( cmbavClientaccessstatus.ItemCount > 0 )
         {
            AV15ClientAccessStatus = cmbavClientaccessstatus.getValidValue(AV15ClientAccessStatus);
            AssignAttri("", false, "AV15ClientAccessStatus", AV15ClientAccessStatus);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavClientaccessstatus.CurrentValue = StringUtil.RTrim( AV15ClientAccessStatus);
            AssignProp("", false, cmbavClientaccessstatus_Internalname, "Values", cmbavClientaccessstatus.ToJavascriptSource(), true);
         }
         AV31ClientAuthRequestMustIncludeUserScopes = StringUtil.StrToBool( StringUtil.BoolToStr( AV31ClientAuthRequestMustIncludeUserScopes));
         AssignAttri("", false, "AV31ClientAuthRequestMustIncludeUserScopes", AV31ClientAuthRequestMustIncludeUserScopes);
         AV35ClientDoNotShareUserIDs = StringUtil.StrToBool( StringUtil.BoolToStr( AV35ClientDoNotShareUserIDs));
         AssignAttri("", false, "AV35ClientDoNotShareUserIDs", AV35ClientDoNotShareUserIDs);
         AV29ClientAllowRemoteAuth = StringUtil.StrToBool( StringUtil.BoolToStr( AV29ClientAllowRemoteAuth));
         AssignAttri("", false, "AV29ClientAllowRemoteAuth", AV29ClientAllowRemoteAuth);
         AV25ClientAllowGetUserData = StringUtil.StrToBool( StringUtil.BoolToStr( AV25ClientAllowGetUserData));
         AssignAttri("", false, "AV25ClientAllowGetUserData", AV25ClientAllowGetUserData);
         AV23ClientAllowGetUserAddData = StringUtil.StrToBool( StringUtil.BoolToStr( AV23ClientAllowGetUserAddData));
         AssignAttri("", false, "AV23ClientAllowGetUserAddData", AV23ClientAllowGetUserAddData);
         AV27ClientAllowGetUserRoles = StringUtil.StrToBool( StringUtil.BoolToStr( AV27ClientAllowGetUserRoles));
         AssignAttri("", false, "AV27ClientAllowGetUserRoles", AV27ClientAllowGetUserRoles);
         AV21ClientAllowGetSessionIniProp = StringUtil.StrToBool( StringUtil.BoolToStr( AV21ClientAllowGetSessionIniProp));
         AssignAttri("", false, "AV21ClientAllowGetSessionIniProp", AV21ClientAllowGetSessionIniProp);
         AV19ClientAllowGetSessionAppData = StringUtil.StrToBool( StringUtil.BoolToStr( AV19ClientAllowGetSessionAppData));
         AssignAttri("", false, "AV19ClientAllowGetSessionAppData", AV19ClientAllowGetSessionAppData);
         AV33ClientCallbackURLisCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV33ClientCallbackURLisCustom));
         AssignAttri("", false, "AV33ClientCallbackURLisCustom", AV33ClientCallbackURLisCustom);
         AV30ClientAllowRemoteRestAuth = StringUtil.StrToBool( StringUtil.BoolToStr( AV30ClientAllowRemoteRestAuth));
         AssignAttri("", false, "AV30ClientAllowRemoteRestAuth", AV30ClientAllowRemoteRestAuth);
         AV26ClientAllowGetUserDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV26ClientAllowGetUserDataREST));
         AssignAttri("", false, "AV26ClientAllowGetUserDataREST", AV26ClientAllowGetUserDataREST);
         AV24ClientAllowGetUserAddDataRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV24ClientAllowGetUserAddDataRest));
         AssignAttri("", false, "AV24ClientAllowGetUserAddDataRest", AV24ClientAllowGetUserAddDataRest);
         AV28ClientAllowGetUserRolesRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV28ClientAllowGetUserRolesRest));
         AssignAttri("", false, "AV28ClientAllowGetUserRolesRest", AV28ClientAllowGetUserRolesRest);
         AV22ClientAllowGetSessionIniPropRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV22ClientAllowGetSessionIniPropRest));
         AssignAttri("", false, "AV22ClientAllowGetSessionIniPropRest", AV22ClientAllowGetSessionIniPropRest);
         AV20ClientAllowGetSessionAppDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV20ClientAllowGetSessionAppDataREST));
         AssignAttri("", false, "AV20ClientAllowGetSessionAppDataREST", AV20ClientAllowGetSessionAppDataREST);
         AV16ClientAccessUniqueByUser = StringUtil.StrToBool( StringUtil.BoolToStr( AV16ClientAccessUniqueByUser));
         AssignAttri("", false, "AV16ClientAccessUniqueByUser", AV16ClientAccessUniqueByUser);
         AV7AccessRequiresPermission = StringUtil.StrToBool( StringUtil.BoolToStr( AV7AccessRequiresPermission));
         AssignAttri("", false, "AV7AccessRequiresPermission", AV7AccessRequiresPermission);
         AV72IsAuthorizationDelegated = StringUtil.StrToBool( StringUtil.BoolToStr( AV72IsAuthorizationDelegated));
         AssignAttri("", false, "AV72IsAuthorizationDelegated", AV72IsAuthorizationDelegated);
         if ( cmbavDelegateauthorizationversion.ItemCount > 0 )
         {
            AV49DelegateAuthorizationVersion = cmbavDelegateauthorizationversion.getValidValue(AV49DelegateAuthorizationVersion);
            AssignAttri("", false, "AV49DelegateAuthorizationVersion", AV49DelegateAuthorizationVersion);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavDelegateauthorizationversion.CurrentValue = StringUtil.RTrim( AV49DelegateAuthorizationVersion);
            AssignProp("", false, cmbavDelegateauthorizationversion_Internalname, "Values", cmbavDelegateauthorizationversion.ToJavascriptSource(), true);
         }
         AV94SSORestEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV94SSORestEnable));
         AssignAttri("", false, "AV94SSORestEnable", AV94SSORestEnable);
         if ( cmbavSsorestmode.ItemCount > 0 )
         {
            AV95SSORestMode = cmbavSsorestmode.getValidValue(AV95SSORestMode);
            AssignAttri("", false, "AV95SSORestMode", AV95SSORestMode);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavSsorestmode.CurrentValue = StringUtil.RTrim( AV95SSORestMode);
            AssignProp("", false, cmbavSsorestmode_Internalname, "Values", cmbavSsorestmode.ToJavascriptSource(), true);
         }
         AV99SSORestServerURL_isCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV99SSORestServerURL_isCustom));
         AssignAttri("", false, "AV99SSORestServerURL_isCustom", AV99SSORestServerURL_isCustom);
         AV105STSProtocolEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV105STSProtocolEnable));
         AssignAttri("", false, "AV105STSProtocolEnable", AV105STSProtocolEnable);
         if ( cmbavStsmode.ItemCount > 0 )
         {
            AV104STSMode = cmbavStsmode.getValidValue(AV104STSMode);
            AssignAttri("", false, "AV104STSMode", AV104STSMode);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavStsmode.CurrentValue = StringUtil.RTrim( AV104STSMode);
            AssignProp("", false, cmbavStsmode_Internalname, "Values", cmbavStsmode.ToJavascriptSource(), true);
         }
         AV83MiniAppEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV83MiniAppEnable));
         AssignAttri("", false, "AV83MiniAppEnable", AV83MiniAppEnable);
         if ( cmbavMiniappmode.ItemCount > 0 )
         {
            AV84MiniAppMode = cmbavMiniappmode.getValidValue(AV84MiniAppMode);
            AssignAttri("", false, "AV84MiniAppMode", AV84MiniAppMode);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavMiniappmode.CurrentValue = StringUtil.RTrim( AV84MiniAppMode);
            AssignProp("", false, cmbavMiniappmode_Internalname, "Values", cmbavMiniappmode.ToJavascriptSource(), true);
         }
         AV82MiniAppClientURL_isCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV82MiniAppClientURL_isCustom));
         AssignAttri("", false, "AV82MiniAppClientURL_isCustom", AV82MiniAppClientURL_isCustom);
         if ( cmbavMiniappuserauthenticationtypename.ItemCount > 0 )
         {
            AV88MiniAppUserAuthenticationTypeName = cmbavMiniappuserauthenticationtypename.getValidValue(AV88MiniAppUserAuthenticationTypeName);
            AssignAttri("", false, "AV88MiniAppUserAuthenticationTypeName", AV88MiniAppUserAuthenticationTypeName);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavMiniappuserauthenticationtypename.CurrentValue = StringUtil.RTrim( AV88MiniAppUserAuthenticationTypeName);
            AssignProp("", false, cmbavMiniappuserauthenticationtypename_Internalname, "Values", cmbavMiniappuserauthenticationtypename.ToJavascriptSource(), true);
         }
         AV87MiniAppServerURL_isCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV87MiniAppServerURL_isCustom));
         AssignAttri("", false, "AV87MiniAppServerURL_isCustom", AV87MiniAppServerURL_isCustom);
         AV11APIKeyEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV11APIKeyEnable));
         AssignAttri("", false, "AV11APIKeyEnable", AV11APIKeyEnable);
         if ( cmbavApikeyallowonlyauthenticationtypename.ItemCount > 0 )
         {
            AV9APIKeyAllowOnlyAuthenticationTypeName = cmbavApikeyallowonlyauthenticationtypename.getValidValue(AV9APIKeyAllowOnlyAuthenticationTypeName);
            AssignAttri("", false, "AV9APIKeyAllowOnlyAuthenticationTypeName", AV9APIKeyAllowOnlyAuthenticationTypeName);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavApikeyallowonlyauthenticationtypename.CurrentValue = StringUtil.RTrim( AV9APIKeyAllowOnlyAuthenticationTypeName);
            AssignProp("", false, cmbavApikeyallowonlyauthenticationtypename_Internalname, "Values", cmbavApikeyallowonlyauthenticationtypename.ToJavascriptSource(), true);
         }
         AV10APIKeyAllowScopeCustomization = StringUtil.StrToBool( StringUtil.BoolToStr( AV10APIKeyAllowScopeCustomization));
         AssignAttri("", false, "AV10APIKeyAllowScopeCustomization", AV10APIKeyAllowScopeCustomization);
         AV56EnvironmentSecureProtocol = StringUtil.StrToBool( StringUtil.BoolToStr( AV56EnvironmentSecureProtocol));
         AssignAttri("", false, "AV56EnvironmentSecureProtocol", AV56EnvironmentSecureProtocol);
         AV14AutoRegisterAnomymousUser = StringUtil.StrToBool( StringUtil.BoolToStr( AV14AutoRegisterAnomymousUser));
         AssignAttri("", false, "AV14AutoRegisterAnomymousUser", AV14AutoRegisterAnomymousUser);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF142( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavId_Enabled = 0;
         AssignProp("", false, edtavId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavId_Enabled), 5, 0), true);
         edtavGuid_Enabled = 0;
         AssignProp("", false, edtavGuid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGuid_Enabled), 5, 0), true);
         edtavClientrevoked_Enabled = 0;
         AssignProp("", false, edtavClientrevoked_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientrevoked_Enabled), 5, 0), true);
         chkavOnline.Enabled = 0;
         edtavLanguage_Enabled = 0;
      }

      protected void RF142( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridlanguagesContainer.ClearRows();
         }
         wbStart = 563;
         /* Execute user event: Refresh */
         E26142 ();
         nGXsfl_563_idx = 1;
         sGXsfl_563_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_563_idx), 4, 0), 4, "0");
         SubsflControlProps_5632( ) ;
         bGXsfl_563_Refreshing = true;
         GridlanguagesContainer.AddObjectProperty("GridName", "Gridlanguages");
         GridlanguagesContainer.AddObjectProperty("CmpContext", "");
         GridlanguagesContainer.AddObjectProperty("InMasterPage", "false");
         GridlanguagesContainer.AddObjectProperty("Class", "GridWithPaginationBar GridNoBorder WorkWith");
         GridlanguagesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridlanguagesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridlanguagesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Backcolorstyle), 1, 0, ".", "")));
         GridlanguagesContainer.PageSize = subGridlanguages_fnc_Recordsperpage( );
         if ( subGridlanguages_Islastpage != 0 )
         {
            GRIDLANGUAGES_nFirstRecordOnPage = (long)(subGridlanguages_fnc_Recordcount( )-subGridlanguages_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nFirstRecordOnPage), 15, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("GRIDLANGUAGES_nFirstRecordOnPage", GRIDLANGUAGES_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_5632( ) ;
            /* Execute user event: Gridlanguages.Load */
            E27142 ();
            if ( ( subGridlanguages_Islastpage == 0 ) && ( GRIDLANGUAGES_nCurrentRecord > 0 ) && ( GRIDLANGUAGES_nGridOutOfScope == 0 ) && ( nGXsfl_563_idx == 1 ) )
            {
               GRIDLANGUAGES_nCurrentRecord = 0;
               GRIDLANGUAGES_nGridOutOfScope = 1;
               subgridlanguages_firstpage( ) ;
               /* Execute user event: Gridlanguages.Load */
               E27142 ();
            }
            wbEnd = 563;
            WB140( ) ;
         }
         bGXsfl_563_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes142( )
      {
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE"+"_"+sGXsfl_563_idx, GetSecureSignedToken( sGXsfl_563_idx, StringUtil.RTrim( context.localUtil.Format( AV75Language, "")), context));
      }

      protected int subGridlanguages_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridlanguages_fnc_Recordcount( )
      {
         return (int)(((subGridlanguages_Recordcount==0) ? GRIDLANGUAGES_nFirstRecordOnPage+1 : subGridlanguages_Recordcount)) ;
      }

      protected int subGridlanguages_fnc_Recordsperpage( )
      {
         if ( subGridlanguages_Rows > 0 )
         {
            return subGridlanguages_Rows*1 ;
         }
         else
         {
            return (int)(-1) ;
         }
      }

      protected int subGridlanguages_fnc_Currentpage( )
      {
         return (int)(((subGridlanguages_Islastpage==1) ? NumberUtil.Int( (long)(Math.Round(subGridlanguages_fnc_Recordcount( )/ (decimal)(subGridlanguages_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+((((int)((subGridlanguages_fnc_Recordcount( )) % (subGridlanguages_fnc_Recordsperpage( ))))==0) ? 0 : 1) : NumberUtil.Int( (long)(Math.Round(GRIDLANGUAGES_nFirstRecordOnPage/ (decimal)(subGridlanguages_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1)) ;
      }

      protected short subgridlanguages_firstpage( )
      {
         GRIDLANGUAGES_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridlanguages_refresh( subGridlanguages_Rows, Gx_mode, AV92ReturnMenuOptionsWithoutPermission, AV109UseAbsoluteUrlByEnvironment, AV31ClientAuthRequestMustIncludeUserScopes, AV35ClientDoNotShareUserIDs, AV29ClientAllowRemoteAuth, AV25ClientAllowGetUserData, AV23ClientAllowGetUserAddData, AV27ClientAllowGetUserRoles, AV21ClientAllowGetSessionIniProp, AV19ClientAllowGetSessionAppData, AV33ClientCallbackURLisCustom, AV30ClientAllowRemoteRestAuth, AV26ClientAllowGetUserDataREST, AV24ClientAllowGetUserAddDataRest, AV28ClientAllowGetUserRolesRest, AV22ClientAllowGetSessionIniPropRest, AV20ClientAllowGetSessionAppDataREST, AV16ClientAccessUniqueByUser, AV7AccessRequiresPermission, AV72IsAuthorizationDelegated, AV94SSORestEnable, AV99SSORestServerURL_isCustom, AV105STSProtocolEnable, AV83MiniAppEnable, AV82MiniAppClientURL_isCustom, AV87MiniAppServerURL_isCustom, AV11APIKeyEnable, AV10APIKeyAllowScopeCustomization, AV56EnvironmentSecureProtocol, AV14AutoRegisterAnomymousUser) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridlanguages_nextpage( )
      {
         if ( GRIDLANGUAGES_nEOF == 0 )
         {
            GRIDLANGUAGES_nFirstRecordOnPage = (long)(GRIDLANGUAGES_nFirstRecordOnPage+subGridlanguages_fnc_Recordsperpage( ));
         }
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nFirstRecordOnPage), 15, 0, ".", "")));
         GridlanguagesContainer.AddObjectProperty("GRIDLANGUAGES_nFirstRecordOnPage", GRIDLANGUAGES_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGridlanguages_refresh( subGridlanguages_Rows, Gx_mode, AV92ReturnMenuOptionsWithoutPermission, AV109UseAbsoluteUrlByEnvironment, AV31ClientAuthRequestMustIncludeUserScopes, AV35ClientDoNotShareUserIDs, AV29ClientAllowRemoteAuth, AV25ClientAllowGetUserData, AV23ClientAllowGetUserAddData, AV27ClientAllowGetUserRoles, AV21ClientAllowGetSessionIniProp, AV19ClientAllowGetSessionAppData, AV33ClientCallbackURLisCustom, AV30ClientAllowRemoteRestAuth, AV26ClientAllowGetUserDataREST, AV24ClientAllowGetUserAddDataRest, AV28ClientAllowGetUserRolesRest, AV22ClientAllowGetSessionIniPropRest, AV20ClientAllowGetSessionAppDataREST, AV16ClientAccessUniqueByUser, AV7AccessRequiresPermission, AV72IsAuthorizationDelegated, AV94SSORestEnable, AV99SSORestServerURL_isCustom, AV105STSProtocolEnable, AV83MiniAppEnable, AV82MiniAppClientURL_isCustom, AV87MiniAppServerURL_isCustom, AV11APIKeyEnable, AV10APIKeyAllowScopeCustomization, AV56EnvironmentSecureProtocol, AV14AutoRegisterAnomymousUser) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRIDLANGUAGES_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgridlanguages_previouspage( )
      {
         if ( GRIDLANGUAGES_nFirstRecordOnPage >= subGridlanguages_fnc_Recordsperpage( ) )
         {
            GRIDLANGUAGES_nFirstRecordOnPage = (long)(GRIDLANGUAGES_nFirstRecordOnPage-subGridlanguages_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridlanguages_refresh( subGridlanguages_Rows, Gx_mode, AV92ReturnMenuOptionsWithoutPermission, AV109UseAbsoluteUrlByEnvironment, AV31ClientAuthRequestMustIncludeUserScopes, AV35ClientDoNotShareUserIDs, AV29ClientAllowRemoteAuth, AV25ClientAllowGetUserData, AV23ClientAllowGetUserAddData, AV27ClientAllowGetUserRoles, AV21ClientAllowGetSessionIniProp, AV19ClientAllowGetSessionAppData, AV33ClientCallbackURLisCustom, AV30ClientAllowRemoteRestAuth, AV26ClientAllowGetUserDataREST, AV24ClientAllowGetUserAddDataRest, AV28ClientAllowGetUserRolesRest, AV22ClientAllowGetSessionIniPropRest, AV20ClientAllowGetSessionAppDataREST, AV16ClientAccessUniqueByUser, AV7AccessRequiresPermission, AV72IsAuthorizationDelegated, AV94SSORestEnable, AV99SSORestServerURL_isCustom, AV105STSProtocolEnable, AV83MiniAppEnable, AV82MiniAppClientURL_isCustom, AV87MiniAppServerURL_isCustom, AV11APIKeyEnable, AV10APIKeyAllowScopeCustomization, AV56EnvironmentSecureProtocol, AV14AutoRegisterAnomymousUser) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridlanguages_lastpage( )
      {
         subGridlanguages_Islastpage = 1;
         if ( isFullAjaxMode( ) )
         {
            gxgrGridlanguages_refresh( subGridlanguages_Rows, Gx_mode, AV92ReturnMenuOptionsWithoutPermission, AV109UseAbsoluteUrlByEnvironment, AV31ClientAuthRequestMustIncludeUserScopes, AV35ClientDoNotShareUserIDs, AV29ClientAllowRemoteAuth, AV25ClientAllowGetUserData, AV23ClientAllowGetUserAddData, AV27ClientAllowGetUserRoles, AV21ClientAllowGetSessionIniProp, AV19ClientAllowGetSessionAppData, AV33ClientCallbackURLisCustom, AV30ClientAllowRemoteRestAuth, AV26ClientAllowGetUserDataREST, AV24ClientAllowGetUserAddDataRest, AV28ClientAllowGetUserRolesRest, AV22ClientAllowGetSessionIniPropRest, AV20ClientAllowGetSessionAppDataREST, AV16ClientAccessUniqueByUser, AV7AccessRequiresPermission, AV72IsAuthorizationDelegated, AV94SSORestEnable, AV99SSORestServerURL_isCustom, AV105STSProtocolEnable, AV83MiniAppEnable, AV82MiniAppClientURL_isCustom, AV87MiniAppServerURL_isCustom, AV11APIKeyEnable, AV10APIKeyAllowScopeCustomization, AV56EnvironmentSecureProtocol, AV14AutoRegisterAnomymousUser) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgridlanguages_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRIDLANGUAGES_nFirstRecordOnPage = (long)(subGridlanguages_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRIDLANGUAGES_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridlanguages_refresh( subGridlanguages_Rows, Gx_mode, AV92ReturnMenuOptionsWithoutPermission, AV109UseAbsoluteUrlByEnvironment, AV31ClientAuthRequestMustIncludeUserScopes, AV35ClientDoNotShareUserIDs, AV29ClientAllowRemoteAuth, AV25ClientAllowGetUserData, AV23ClientAllowGetUserAddData, AV27ClientAllowGetUserRoles, AV21ClientAllowGetSessionIniProp, AV19ClientAllowGetSessionAppData, AV33ClientCallbackURLisCustom, AV30ClientAllowRemoteRestAuth, AV26ClientAllowGetUserDataREST, AV24ClientAllowGetUserAddDataRest, AV28ClientAllowGetUserRolesRest, AV22ClientAllowGetSessionIniPropRest, AV20ClientAllowGetSessionAppDataREST, AV16ClientAccessUniqueByUser, AV7AccessRequiresPermission, AV72IsAuthorizationDelegated, AV94SSORestEnable, AV99SSORestServerURL_isCustom, AV105STSProtocolEnable, AV83MiniAppEnable, AV82MiniAppClientURL_isCustom, AV87MiniAppServerURL_isCustom, AV11APIKeyEnable, AV10APIKeyAllowScopeCustomization, AV56EnvironmentSecureProtocol, AV14AutoRegisterAnomymousUser) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavId_Enabled = 0;
         AssignProp("", false, edtavId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavId_Enabled), 5, 0), true);
         edtavGuid_Enabled = 0;
         AssignProp("", false, edtavGuid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGuid_Enabled), 5, 0), true);
         edtavClientrevoked_Enabled = 0;
         AssignProp("", false, edtavClientrevoked_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientrevoked_Enabled), 5, 0), true);
         chkavOnline.Enabled = 0;
         edtavLanguage_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP140( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E25142 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            nRC_GXsfl_563 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_563"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV67GridLanguagesPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDLANGUAGESPAGECOUNT"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV115GridLanguagesAppliedFilters = cgiGet( "vGRIDLANGUAGESAPPLIEDFILTERS");
            Gx_mode = cgiGet( "vMODE");
            GRIDLANGUAGES_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDLANGUAGES_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRIDLANGUAGES_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDLANGUAGES_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGridlanguages_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "subGridlanguages_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGridlanguages_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDLANGUAGES_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Rows), 6, 0, ".", "")));
            Dvpanel_unnamedtable6_Width = cgiGet( "DVPANEL_UNNAMEDTABLE6_Width");
            Dvpanel_unnamedtable6_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE6_Autowidth"));
            Dvpanel_unnamedtable6_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE6_Autoheight"));
            Dvpanel_unnamedtable6_Cls = cgiGet( "DVPANEL_UNNAMEDTABLE6_Cls");
            Dvpanel_unnamedtable6_Title = cgiGet( "DVPANEL_UNNAMEDTABLE6_Title");
            Dvpanel_unnamedtable6_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE6_Collapsible"));
            Dvpanel_unnamedtable6_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE6_Collapsed"));
            Dvpanel_unnamedtable6_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE6_Showcollapseicon"));
            Dvpanel_unnamedtable6_Iconposition = cgiGet( "DVPANEL_UNNAMEDTABLE6_Iconposition");
            Dvpanel_unnamedtable6_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE6_Autoscroll"));
            Dvpanel_unnamedtable7_Width = cgiGet( "DVPANEL_UNNAMEDTABLE7_Width");
            Dvpanel_unnamedtable7_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE7_Autowidth"));
            Dvpanel_unnamedtable7_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE7_Autoheight"));
            Dvpanel_unnamedtable7_Cls = cgiGet( "DVPANEL_UNNAMEDTABLE7_Cls");
            Dvpanel_unnamedtable7_Title = cgiGet( "DVPANEL_UNNAMEDTABLE7_Title");
            Dvpanel_unnamedtable7_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE7_Collapsible"));
            Dvpanel_unnamedtable7_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE7_Collapsed"));
            Dvpanel_unnamedtable7_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE7_Showcollapseicon"));
            Dvpanel_unnamedtable7_Iconposition = cgiGet( "DVPANEL_UNNAMEDTABLE7_Iconposition");
            Dvpanel_unnamedtable7_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE7_Autoscroll"));
            Dvpanel_unnamedtable8_Width = cgiGet( "DVPANEL_UNNAMEDTABLE8_Width");
            Dvpanel_unnamedtable8_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE8_Autowidth"));
            Dvpanel_unnamedtable8_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE8_Autoheight"));
            Dvpanel_unnamedtable8_Cls = cgiGet( "DVPANEL_UNNAMEDTABLE8_Cls");
            Dvpanel_unnamedtable8_Title = cgiGet( "DVPANEL_UNNAMEDTABLE8_Title");
            Dvpanel_unnamedtable8_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE8_Collapsible"));
            Dvpanel_unnamedtable8_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE8_Collapsed"));
            Dvpanel_unnamedtable8_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE8_Showcollapseicon"));
            Dvpanel_unnamedtable8_Iconposition = cgiGet( "DVPANEL_UNNAMEDTABLE8_Iconposition");
            Dvpanel_unnamedtable8_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE8_Autoscroll"));
            Gridlanguagespaginationbar_Class = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Class");
            Gridlanguagespaginationbar_Showfirst = StringUtil.StrToBool( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Showfirst"));
            Gridlanguagespaginationbar_Showprevious = StringUtil.StrToBool( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Showprevious"));
            Gridlanguagespaginationbar_Shownext = StringUtil.StrToBool( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Shownext"));
            Gridlanguagespaginationbar_Showlast = StringUtil.StrToBool( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Showlast"));
            Gridlanguagespaginationbar_Pagestoshow = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Pagestoshow"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridlanguagespaginationbar_Pagingbuttonsposition = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Pagingbuttonsposition");
            Gridlanguagespaginationbar_Pagingcaptionposition = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Pagingcaptionposition");
            Gridlanguagespaginationbar_Emptygridclass = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Emptygridclass");
            Gridlanguagespaginationbar_Rowsperpageselector = StringUtil.StrToBool( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageselector"));
            Gridlanguagespaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridlanguagespaginationbar_Rowsperpageoptions = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageoptions");
            Gridlanguagespaginationbar_Previous = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Previous");
            Gridlanguagespaginationbar_Next = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Next");
            Gridlanguagespaginationbar_Caption = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Caption");
            Gridlanguagespaginationbar_Emptygridcaption = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Emptygridcaption");
            Gridlanguagespaginationbar_Rowsperpagecaption = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Rowsperpagecaption");
            Gxuitabspanel_tabs_Pagecount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GXUITABSPANEL_TABS_Pagecount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gxuitabspanel_tabs_Class = cgiGet( "GXUITABSPANEL_TABS_Class");
            Gxuitabspanel_tabs_Historymanagement = StringUtil.StrToBool( cgiGet( "GXUITABSPANEL_TABS_Historymanagement"));
            Gridlanguages_empowerer_Gridinternalname = cgiGet( "GRIDLANGUAGES_EMPOWERER_Gridinternalname");
            Gridlanguagespaginationbar_Selectedpage = cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Selectedpage");
            Gridlanguagespaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDLANGUAGESPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            /* Read variables values. */
            AV71Id = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV71Id", StringUtil.LTrimStr( (decimal)(AV71Id), 12, 0));
            AV68GUID = cgiGet( edtavGuid_Internalname);
            AssignAttri("", false, "AV68GUID", AV68GUID);
            AV89Name = cgiGet( edtavName_Internalname);
            AssignAttri("", false, "AV89Name", AV89Name);
            AV50Dsc = cgiGet( edtavDsc_Internalname);
            AssignAttri("", false, "AV50Dsc", AV50Dsc);
            AV111Version = cgiGet( edtavVersion_Internalname);
            AssignAttri("", false, "AV111Version", AV111Version);
            AV43Company = cgiGet( edtavCompany_Internalname);
            AssignAttri("", false, "AV43Company", AV43Company);
            AV44Copyright = cgiGet( edtavCopyright_Internalname);
            AssignAttri("", false, "AV44Copyright", AV44Copyright);
            AV92ReturnMenuOptionsWithoutPermission = StringUtil.StrToBool( cgiGet( chkavReturnmenuoptionswithoutpermission_Internalname));
            AssignAttri("", false, "AV92ReturnMenuOptionsWithoutPermission", AV92ReturnMenuOptionsWithoutPermission);
            cmbavMainmenu.Name = cmbavMainmenu_Internalname;
            cmbavMainmenu.CurrentValue = cgiGet( cmbavMainmenu_Internalname);
            AV77MainMenu = (long)(Math.Round(NumberUtil.Val( cgiGet( cmbavMainmenu_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV77MainMenu", StringUtil.LTrimStr( (decimal)(AV77MainMenu), 12, 0));
            AV109UseAbsoluteUrlByEnvironment = StringUtil.StrToBool( cgiGet( chkavUseabsoluteurlbyenvironment_Internalname));
            AssignAttri("", false, "AV109UseAbsoluteUrlByEnvironment", AV109UseAbsoluteUrlByEnvironment);
            AV69HomeObject = cgiGet( edtavHomeobject_Internalname);
            AssignAttri("", false, "AV69HomeObject", AV69HomeObject);
            AV8AccountActivationObject = cgiGet( edtavAccountactivationobject_Internalname);
            AssignAttri("", false, "AV8AccountActivationObject", AV8AccountActivationObject);
            AV76LogoutObject = cgiGet( edtavLogoutobject_Internalname);
            AssignAttri("", false, "AV76LogoutObject", AV76LogoutObject);
            cmbavClientaccessstatus.Name = cmbavClientaccessstatus_Internalname;
            cmbavClientaccessstatus.CurrentValue = cgiGet( cmbavClientaccessstatus_Internalname);
            AV15ClientAccessStatus = cgiGet( cmbavClientaccessstatus_Internalname);
            AssignAttri("", false, "AV15ClientAccessStatus", AV15ClientAccessStatus);
            if ( context.localUtil.VCDateTime( cgiGet( edtavClientrevoked_Internalname), (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt"))), (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0))) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_baddatetime", new   object[]  {context.GetMessage( "Client Revoked", "")}), 1, "vCLIENTREVOKED");
               GX_FocusControl = edtavClientrevoked_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV41ClientRevoked = (DateTime)(DateTime.MinValue);
               AssignAttri("", false, "AV41ClientRevoked", context.localUtil.TToC( AV41ClientRevoked, 10, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            else
            {
               AV41ClientRevoked = context.localUtil.CToT( cgiGet( edtavClientrevoked_Internalname));
               AssignAttri("", false, "AV41ClientRevoked", context.localUtil.TToC( AV41ClientRevoked, 10, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            AV37ClientId = cgiGet( edtavClientid_Internalname);
            AssignAttri("", false, "AV37ClientId", AV37ClientId);
            AV42ClientSecret = cgiGet( edtavClientsecret_Internalname);
            AssignAttri("", false, "AV42ClientSecret", AV42ClientSecret);
            AV31ClientAuthRequestMustIncludeUserScopes = StringUtil.StrToBool( cgiGet( chkavClientauthrequestmustincludeuserscopes_Internalname));
            AssignAttri("", false, "AV31ClientAuthRequestMustIncludeUserScopes", AV31ClientAuthRequestMustIncludeUserScopes);
            AV35ClientDoNotShareUserIDs = StringUtil.StrToBool( cgiGet( chkavClientdonotshareuserids_Internalname));
            AssignAttri("", false, "AV35ClientDoNotShareUserIDs", AV35ClientDoNotShareUserIDs);
            AV29ClientAllowRemoteAuth = StringUtil.StrToBool( cgiGet( chkavClientallowremoteauth_Internalname));
            AssignAttri("", false, "AV29ClientAllowRemoteAuth", AV29ClientAllowRemoteAuth);
            AV25ClientAllowGetUserData = StringUtil.StrToBool( cgiGet( chkavClientallowgetuserdata_Internalname));
            AssignAttri("", false, "AV25ClientAllowGetUserData", AV25ClientAllowGetUserData);
            AV23ClientAllowGetUserAddData = StringUtil.StrToBool( cgiGet( chkavClientallowgetuseradddata_Internalname));
            AssignAttri("", false, "AV23ClientAllowGetUserAddData", AV23ClientAllowGetUserAddData);
            AV27ClientAllowGetUserRoles = StringUtil.StrToBool( cgiGet( chkavClientallowgetuserroles_Internalname));
            AssignAttri("", false, "AV27ClientAllowGetUserRoles", AV27ClientAllowGetUserRoles);
            AV21ClientAllowGetSessionIniProp = StringUtil.StrToBool( cgiGet( chkavClientallowgetsessioniniprop_Internalname));
            AssignAttri("", false, "AV21ClientAllowGetSessionIniProp", AV21ClientAllowGetSessionIniProp);
            AV19ClientAllowGetSessionAppData = StringUtil.StrToBool( cgiGet( chkavClientallowgetsessionappdata_Internalname));
            AssignAttri("", false, "AV19ClientAllowGetSessionAppData", AV19ClientAllowGetSessionAppData);
            AV17ClientAllowAdditionalScope = cgiGet( edtavClientallowadditionalscope_Internalname);
            AssignAttri("", false, "AV17ClientAllowAdditionalScope", AV17ClientAllowAdditionalScope);
            AV38ClientImageURL = cgiGet( edtavClientimageurl_Internalname);
            AssignAttri("", false, "AV38ClientImageURL", AV38ClientImageURL);
            AV39ClientLocalLoginURL = cgiGet( edtavClientlocalloginurl_Internalname);
            AssignAttri("", false, "AV39ClientLocalLoginURL", AV39ClientLocalLoginURL);
            AV32ClientCallbackURL = cgiGet( edtavClientcallbackurl_Internalname);
            AssignAttri("", false, "AV32ClientCallbackURL", AV32ClientCallbackURL);
            AV33ClientCallbackURLisCustom = StringUtil.StrToBool( cgiGet( chkavClientcallbackurliscustom_Internalname));
            AssignAttri("", false, "AV33ClientCallbackURLisCustom", AV33ClientCallbackURLisCustom);
            AV34ClientCallbackURLStateName = cgiGet( edtavClientcallbackurlstatename_Internalname);
            AssignAttri("", false, "AV34ClientCallbackURLStateName", AV34ClientCallbackURLStateName);
            AV30ClientAllowRemoteRestAuth = StringUtil.StrToBool( cgiGet( chkavClientallowremoterestauth_Internalname));
            AssignAttri("", false, "AV30ClientAllowRemoteRestAuth", AV30ClientAllowRemoteRestAuth);
            AV26ClientAllowGetUserDataREST = StringUtil.StrToBool( cgiGet( chkavClientallowgetuserdatarest_Internalname));
            AssignAttri("", false, "AV26ClientAllowGetUserDataREST", AV26ClientAllowGetUserDataREST);
            AV24ClientAllowGetUserAddDataRest = StringUtil.StrToBool( cgiGet( chkavClientallowgetuseradddatarest_Internalname));
            AssignAttri("", false, "AV24ClientAllowGetUserAddDataRest", AV24ClientAllowGetUserAddDataRest);
            AV28ClientAllowGetUserRolesRest = StringUtil.StrToBool( cgiGet( chkavClientallowgetuserrolesrest_Internalname));
            AssignAttri("", false, "AV28ClientAllowGetUserRolesRest", AV28ClientAllowGetUserRolesRest);
            AV22ClientAllowGetSessionIniPropRest = StringUtil.StrToBool( cgiGet( chkavClientallowgetsessioniniproprest_Internalname));
            AssignAttri("", false, "AV22ClientAllowGetSessionIniPropRest", AV22ClientAllowGetSessionIniPropRest);
            AV20ClientAllowGetSessionAppDataREST = StringUtil.StrToBool( cgiGet( chkavClientallowgetsessionappdatarest_Internalname));
            AssignAttri("", false, "AV20ClientAllowGetSessionAppDataREST", AV20ClientAllowGetSessionAppDataREST);
            AV18ClientAllowAdditionalScopeREST = cgiGet( edtavClientallowadditionalscoperest_Internalname);
            AssignAttri("", false, "AV18ClientAllowAdditionalScopeREST", AV18ClientAllowAdditionalScopeREST);
            AV16ClientAccessUniqueByUser = StringUtil.StrToBool( cgiGet( chkavClientaccessuniquebyuser_Internalname));
            AssignAttri("", false, "AV16ClientAccessUniqueByUser", AV16ClientAccessUniqueByUser);
            AV36ClientEncryptionKey = cgiGet( edtavClientencryptionkey_Internalname);
            AssignAttri("", false, "AV36ClientEncryptionKey", AV36ClientEncryptionKey);
            AV40ClientRepositoryGUID = cgiGet( edtavClientrepositoryguid_Internalname);
            AssignAttri("", false, "AV40ClientRepositoryGUID", AV40ClientRepositoryGUID);
            AV7AccessRequiresPermission = StringUtil.StrToBool( cgiGet( chkavAccessrequirespermission_Internalname));
            AssignAttri("", false, "AV7AccessRequiresPermission", AV7AccessRequiresPermission);
            AV72IsAuthorizationDelegated = StringUtil.StrToBool( cgiGet( chkavIsauthorizationdelegated_Internalname));
            AssignAttri("", false, "AV72IsAuthorizationDelegated", AV72IsAuthorizationDelegated);
            cmbavDelegateauthorizationversion.Name = cmbavDelegateauthorizationversion_Internalname;
            cmbavDelegateauthorizationversion.CurrentValue = cgiGet( cmbavDelegateauthorizationversion_Internalname);
            AV49DelegateAuthorizationVersion = cgiGet( cmbavDelegateauthorizationversion_Internalname);
            AssignAttri("", false, "AV49DelegateAuthorizationVersion", AV49DelegateAuthorizationVersion);
            AV46DelegateAuthorizationFileName = cgiGet( edtavDelegateauthorizationfilename_Internalname);
            AssignAttri("", false, "AV46DelegateAuthorizationFileName", AV46DelegateAuthorizationFileName);
            AV48DelegateAuthorizationPackage = cgiGet( edtavDelegateauthorizationpackage_Internalname);
            AssignAttri("", false, "AV48DelegateAuthorizationPackage", AV48DelegateAuthorizationPackage);
            AV45DelegateAuthorizationClassName = cgiGet( edtavDelegateauthorizationclassname_Internalname);
            AssignAttri("", false, "AV45DelegateAuthorizationClassName", AV45DelegateAuthorizationClassName);
            AV47DelegateAuthorizationMethod = cgiGet( edtavDelegateauthorizationmethod_Internalname);
            AssignAttri("", false, "AV47DelegateAuthorizationMethod", AV47DelegateAuthorizationMethod);
            AV94SSORestEnable = StringUtil.StrToBool( cgiGet( chkavSsorestenable_Internalname));
            AssignAttri("", false, "AV94SSORestEnable", AV94SSORestEnable);
            cmbavSsorestmode.Name = cmbavSsorestmode_Internalname;
            cmbavSsorestmode.CurrentValue = cgiGet( cmbavSsorestmode_Internalname);
            AV95SSORestMode = cgiGet( cmbavSsorestmode_Internalname);
            AssignAttri("", false, "AV95SSORestMode", AV95SSORestMode);
            AV101SSORestUserAuthTypeName = cgiGet( edtavSsorestuserauthtypename_Internalname);
            AssignAttri("", false, "AV101SSORestUserAuthTypeName", AV101SSORestUserAuthTypeName);
            AV98SSORestServerURL = cgiGet( edtavSsorestserverurl_Internalname);
            AssignAttri("", false, "AV98SSORestServerURL", AV98SSORestServerURL);
            AV99SSORestServerURL_isCustom = StringUtil.StrToBool( cgiGet( chkavSsorestserverurl_iscustom_Internalname));
            AssignAttri("", false, "AV99SSORestServerURL_isCustom", AV99SSORestServerURL_isCustom);
            AV100SSORestServerURL_SLO = cgiGet( edtavSsorestserverurl_slo_Internalname);
            AssignAttri("", false, "AV100SSORestServerURL_SLO", AV100SSORestServerURL_SLO);
            AV97SSORestServerRepositoryGUID = cgiGet( edtavSsorestserverrepositoryguid_Internalname);
            AssignAttri("", false, "AV97SSORestServerRepositoryGUID", AV97SSORestServerRepositoryGUID);
            AV96SSORestServerKey = cgiGet( edtavSsorestserverkey_Internalname);
            AssignAttri("", false, "AV96SSORestServerKey", AV96SSORestServerKey);
            AV105STSProtocolEnable = StringUtil.StrToBool( cgiGet( chkavStsprotocolenable_Internalname));
            AssignAttri("", false, "AV105STSProtocolEnable", AV105STSProtocolEnable);
            cmbavStsmode.Name = cmbavStsmode_Internalname;
            cmbavStsmode.CurrentValue = cgiGet( cmbavStsmode_Internalname);
            AV104STSMode = cgiGet( cmbavStsmode_Internalname);
            AssignAttri("", false, "AV104STSMode", AV104STSMode);
            AV103STSAuthorizationUserName = cgiGet( edtavStsauthorizationusername_Internalname);
            AssignAttri("", false, "AV103STSAuthorizationUserName", AV103STSAuthorizationUserName);
            AV106STSServerClientPassword = cgiGet( edtavStsserverclientpassword_Internalname);
            AssignAttri("", false, "AV106STSServerClientPassword", AV106STSServerClientPassword);
            AV108STSServerURL = cgiGet( edtavStsserverurl_Internalname);
            AssignAttri("", false, "AV108STSServerURL", AV108STSServerURL);
            AV107STSServerRepositoryGUID = cgiGet( edtavStsserverrepositoryguid_Internalname);
            AssignAttri("", false, "AV107STSServerRepositoryGUID", AV107STSServerRepositoryGUID);
            AV83MiniAppEnable = StringUtil.StrToBool( cgiGet( chkavMiniappenable_Internalname));
            AssignAttri("", false, "AV83MiniAppEnable", AV83MiniAppEnable);
            cmbavMiniappmode.Name = cmbavMiniappmode_Internalname;
            cmbavMiniappmode.CurrentValue = cgiGet( cmbavMiniappmode_Internalname);
            AV84MiniAppMode = cgiGet( cmbavMiniappmode_Internalname);
            AssignAttri("", false, "AV84MiniAppMode", AV84MiniAppMode);
            AV81MiniAppClientURL = cgiGet( edtavMiniappclienturl_Internalname);
            AssignAttri("", false, "AV81MiniAppClientURL", AV81MiniAppClientURL);
            AV82MiniAppClientURL_isCustom = StringUtil.StrToBool( cgiGet( chkavMiniappclienturl_iscustom_Internalname));
            AssignAttri("", false, "AV82MiniAppClientURL_isCustom", AV82MiniAppClientURL_isCustom);
            AV80MiniAppClientRepositoryGUID = cgiGet( edtavMiniappclientrepositoryguid_Internalname);
            AssignAttri("", false, "AV80MiniAppClientRepositoryGUID", AV80MiniAppClientRepositoryGUID);
            cmbavMiniappuserauthenticationtypename.Name = cmbavMiniappuserauthenticationtypename_Internalname;
            cmbavMiniappuserauthenticationtypename.CurrentValue = cgiGet( cmbavMiniappuserauthenticationtypename_Internalname);
            AV88MiniAppUserAuthenticationTypeName = cgiGet( cmbavMiniappuserauthenticationtypename_Internalname);
            AssignAttri("", false, "AV88MiniAppUserAuthenticationTypeName", AV88MiniAppUserAuthenticationTypeName);
            AV86MiniAppServerURL = cgiGet( edtavMiniappserverurl_Internalname);
            AssignAttri("", false, "AV86MiniAppServerURL", AV86MiniAppServerURL);
            AV87MiniAppServerURL_isCustom = StringUtil.StrToBool( cgiGet( chkavMiniappserverurl_iscustom_Internalname));
            AssignAttri("", false, "AV87MiniAppServerURL_isCustom", AV87MiniAppServerURL_isCustom);
            AV85MiniAppServerRepositoryGUID = cgiGet( edtavMiniappserverrepositoryguid_Internalname);
            AssignAttri("", false, "AV85MiniAppServerRepositoryGUID", AV85MiniAppServerRepositoryGUID);
            AV11APIKeyEnable = StringUtil.StrToBool( cgiGet( chkavApikeyenable_Internalname));
            AssignAttri("", false, "AV11APIKeyEnable", AV11APIKeyEnable);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavApikeytimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavApikeytimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vAPIKEYTIMEOUT");
               GX_FocusControl = edtavApikeytimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV12APIKeyTimeout = 0;
               AssignAttri("", false, "AV12APIKeyTimeout", StringUtil.LTrimStr( (decimal)(AV12APIKeyTimeout), 9, 0));
            }
            else
            {
               AV12APIKeyTimeout = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavApikeytimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV12APIKeyTimeout", StringUtil.LTrimStr( (decimal)(AV12APIKeyTimeout), 9, 0));
            }
            cmbavApikeyallowonlyauthenticationtypename.Name = cmbavApikeyallowonlyauthenticationtypename_Internalname;
            cmbavApikeyallowonlyauthenticationtypename.CurrentValue = cgiGet( cmbavApikeyallowonlyauthenticationtypename_Internalname);
            AV9APIKeyAllowOnlyAuthenticationTypeName = cgiGet( cmbavApikeyallowonlyauthenticationtypename_Internalname);
            AssignAttri("", false, "AV9APIKeyAllowOnlyAuthenticationTypeName", AV9APIKeyAllowOnlyAuthenticationTypeName);
            AV10APIKeyAllowScopeCustomization = StringUtil.StrToBool( cgiGet( chkavApikeyallowscopecustomization_Internalname));
            AssignAttri("", false, "AV10APIKeyAllowScopeCustomization", AV10APIKeyAllowScopeCustomization);
            AV52EnvironmentName = cgiGet( edtavEnvironmentname_Internalname);
            AssignAttri("", false, "AV52EnvironmentName", AV52EnvironmentName);
            AV56EnvironmentSecureProtocol = StringUtil.StrToBool( cgiGet( chkavEnvironmentsecureprotocol_Internalname));
            AssignAttri("", false, "AV56EnvironmentSecureProtocol", AV56EnvironmentSecureProtocol);
            AV51EnvironmentHost = cgiGet( edtavEnvironmenthost_Internalname);
            AssignAttri("", false, "AV51EnvironmentHost", AV51EnvironmentHost);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavEnvironmentport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavEnvironmentport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 99999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vENVIRONMENTPORT");
               GX_FocusControl = edtavEnvironmentport_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV53EnvironmentPort = 0;
               AssignAttri("", false, "AV53EnvironmentPort", StringUtil.LTrimStr( (decimal)(AV53EnvironmentPort), 5, 0));
            }
            else
            {
               AV53EnvironmentPort = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavEnvironmentport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV53EnvironmentPort", StringUtil.LTrimStr( (decimal)(AV53EnvironmentPort), 5, 0));
            }
            AV57EnvironmentVirtualDirectory = cgiGet( edtavEnvironmentvirtualdirectory_Internalname);
            AssignAttri("", false, "AV57EnvironmentVirtualDirectory", AV57EnvironmentVirtualDirectory);
            AV55EnvironmentProgramPackage = cgiGet( edtavEnvironmentprogrampackage_Internalname);
            AssignAttri("", false, "AV55EnvironmentProgramPackage", AV55EnvironmentProgramPackage);
            AV54EnvironmentProgramExtension = cgiGet( edtavEnvironmentprogramextension_Internalname);
            AssignAttri("", false, "AV54EnvironmentProgramExtension", AV54EnvironmentProgramExtension);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavGridlanguagescurrentpage_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavGridlanguagescurrentpage_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vGRIDLANGUAGESCURRENTPAGE");
               GX_FocusControl = edtavGridlanguagescurrentpage_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV66GridLanguagesCurrentPage = 0;
               AssignAttri("", false, "AV66GridLanguagesCurrentPage", StringUtil.LTrimStr( (decimal)(AV66GridLanguagesCurrentPage), 10, 0));
            }
            else
            {
               AV66GridLanguagesCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavGridlanguagescurrentpage_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV66GridLanguagesCurrentPage", StringUtil.LTrimStr( (decimal)(AV66GridLanguagesCurrentPage), 10, 0));
            }
            AV14AutoRegisterAnomymousUser = StringUtil.StrToBool( cgiGet( chkavAutoregisteranomymoususer_Internalname));
            AssignAttri("", false, "AV14AutoRegisterAnomymousUser", AV14AutoRegisterAnomymousUser);
            AV102STSAuthorizationUserGUID = cgiGet( edtavStsauthorizationuserguid_Internalname);
            AssignAttri("", false, "AV102STSAuthorizationUserGUID", AV102STSAuthorizationUserGUID);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E25142 ();
         if (returnInSub) return;
      }

      protected void E25142( )
      {
         /* Start Routine */
         returnInSub = false;
         edtavStsauthorizationuserguid_Visible = 0;
         AssignProp("", false, edtavStsauthorizationuserguid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavStsauthorizationuserguid_Visible), 5, 0), true);
         chkavOnline.Enabled = (((StringUtil.StrCmp(Gx_mode, "DSP")==0) ? false : true) ? 1 : 0);
         AssignProp("", false, chkavOnline_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavOnline.Enabled), 5, 0), !bGXsfl_563_Refreshing);
         cmbavMainmenu.addItem("0", context.GetMessage( "GAM_none", ""), 0);
         bttBtnchangeclientsecret_Visible = 0;
         AssignProp("", false, bttBtnchangeclientsecret_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnchangeclientsecret_Visible), 5, 0), true);
         if ( StringUtil.StrCmp(Gx_mode, "INS") != 0 )
         {
            if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) )
            {
               bttBtnchangeclientsecret_Visible = 1;
               AssignProp("", false, bttBtnchangeclientsecret_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnchangeclientsecret_Visible), 5, 0), true);
            }
            edtavClientsecret_Enabled = 0;
            AssignProp("", false, edtavClientsecret_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientsecret_Enabled), 5, 0), true);
            AV60GAMApplication.load( AV71Id);
            AV71Id = AV60GAMApplication.gxTpr_Id;
            AssignAttri("", false, "AV71Id", StringUtil.LTrimStr( (decimal)(AV71Id), 12, 0));
            AV68GUID = AV60GAMApplication.gxTpr_Guid;
            AssignAttri("", false, "AV68GUID", AV68GUID);
            AV89Name = AV60GAMApplication.gxTpr_Name;
            AssignAttri("", false, "AV89Name", AV89Name);
            AV50Dsc = AV60GAMApplication.gxTpr_Description;
            AssignAttri("", false, "AV50Dsc", AV50Dsc);
            AV111Version = AV60GAMApplication.gxTpr_Version;
            AssignAttri("", false, "AV111Version", AV111Version);
            AV44Copyright = AV60GAMApplication.gxTpr_Copyright;
            AssignAttri("", false, "AV44Copyright", AV44Copyright);
            AV43Company = AV60GAMApplication.gxTpr_Companyname;
            AssignAttri("", false, "AV43Company", AV43Company);
            AV92ReturnMenuOptionsWithoutPermission = AV60GAMApplication.gxTpr_Returnmenuoptionswithoutpermission;
            AssignAttri("", false, "AV92ReturnMenuOptionsWithoutPermission", AV92ReturnMenuOptionsWithoutPermission);
            AV77MainMenu = AV60GAMApplication.gxTpr_Mainmenuid;
            AssignAttri("", false, "AV77MainMenu", StringUtil.LTrimStr( (decimal)(AV77MainMenu), 12, 0));
            AV109UseAbsoluteUrlByEnvironment = AV60GAMApplication.gxTpr_Useabsoluteurlbyenvironment;
            AssignAttri("", false, "AV109UseAbsoluteUrlByEnvironment", AV109UseAbsoluteUrlByEnvironment);
            AV69HomeObject = AV60GAMApplication.gxTpr_Homeobject;
            AssignAttri("", false, "AV69HomeObject", AV69HomeObject);
            AV8AccountActivationObject = AV60GAMApplication.gxTpr_Accountactivationobject;
            AssignAttri("", false, "AV8AccountActivationObject", AV8AccountActivationObject);
            AV76LogoutObject = AV60GAMApplication.gxTpr_Logoutobject;
            AssignAttri("", false, "AV76LogoutObject", AV76LogoutObject);
            /* Execute user subroutine: 'SHOWAPPLICATIONSTATUS' */
            S112 ();
            if (returnInSub) return;
            AV37ClientId = AV60GAMApplication.gxTpr_Clientid;
            AssignAttri("", false, "AV37ClientId", AV37ClientId);
            AV42ClientSecret = AV60GAMApplication.gxTpr_Clientsecret;
            AssignAttri("", false, "AV42ClientSecret", AV42ClientSecret);
            AV31ClientAuthRequestMustIncludeUserScopes = AV60GAMApplication.gxTpr_Clientauthenticationrequestmustincludeuserscopes;
            AssignAttri("", false, "AV31ClientAuthRequestMustIncludeUserScopes", AV31ClientAuthRequestMustIncludeUserScopes);
            AV35ClientDoNotShareUserIDs = AV60GAMApplication.gxTpr_Clientdonotshareuserids;
            AssignAttri("", false, "AV35ClientDoNotShareUserIDs", AV35ClientDoNotShareUserIDs);
            AV29ClientAllowRemoteAuth = AV60GAMApplication.gxTpr_Clientallowremoteauthentication;
            AssignAttri("", false, "AV29ClientAllowRemoteAuth", AV29ClientAllowRemoteAuth);
            AV25ClientAllowGetUserData = AV60GAMApplication.gxTpr_Clientallowgetuserdata;
            AssignAttri("", false, "AV25ClientAllowGetUserData", AV25ClientAllowGetUserData);
            AV23ClientAllowGetUserAddData = AV60GAMApplication.gxTpr_Clientallowgetuseradditionaldata;
            AssignAttri("", false, "AV23ClientAllowGetUserAddData", AV23ClientAllowGetUserAddData);
            AV27ClientAllowGetUserRoles = AV60GAMApplication.gxTpr_Clientallowgetuserroles;
            AssignAttri("", false, "AV27ClientAllowGetUserRoles", AV27ClientAllowGetUserRoles);
            AV21ClientAllowGetSessionIniProp = AV60GAMApplication.gxTpr_Clientallowgetsessioninitialproperties;
            AssignAttri("", false, "AV21ClientAllowGetSessionIniProp", AV21ClientAllowGetSessionIniProp);
            AV19ClientAllowGetSessionAppData = AV60GAMApplication.gxTpr_Clientallowgetsessionapplicationdata;
            AssignAttri("", false, "AV19ClientAllowGetSessionAppData", AV19ClientAllowGetSessionAppData);
            AV17ClientAllowAdditionalScope = AV60GAMApplication.gxTpr_Clientallowadditionalscope;
            AssignAttri("", false, "AV17ClientAllowAdditionalScope", AV17ClientAllowAdditionalScope);
            AV38ClientImageURL = AV60GAMApplication.gxTpr_Clientimageurl;
            AssignAttri("", false, "AV38ClientImageURL", AV38ClientImageURL);
            AV39ClientLocalLoginURL = AV60GAMApplication.gxTpr_Clientlocalloginurl;
            AssignAttri("", false, "AV39ClientLocalLoginURL", AV39ClientLocalLoginURL);
            AV32ClientCallbackURL = AV60GAMApplication.gxTpr_Clientcallbackurl;
            AssignAttri("", false, "AV32ClientCallbackURL", AV32ClientCallbackURL);
            AV33ClientCallbackURLisCustom = AV60GAMApplication.gxTpr_Clientcallbackurliscustom;
            AssignAttri("", false, "AV33ClientCallbackURLisCustom", AV33ClientCallbackURLisCustom);
            AV34ClientCallbackURLStateName = AV60GAMApplication.gxTpr_Clientcallbackurlstatename;
            AssignAttri("", false, "AV34ClientCallbackURLStateName", AV34ClientCallbackURLStateName);
            AV30ClientAllowRemoteRestAuth = AV60GAMApplication.gxTpr_Clientallowremoterestauthentication;
            AssignAttri("", false, "AV30ClientAllowRemoteRestAuth", AV30ClientAllowRemoteRestAuth);
            AV26ClientAllowGetUserDataREST = AV60GAMApplication.gxTpr_Clientallowgetuserdatarest;
            AssignAttri("", false, "AV26ClientAllowGetUserDataREST", AV26ClientAllowGetUserDataREST);
            AV24ClientAllowGetUserAddDataRest = AV60GAMApplication.gxTpr_Clientallowgetuseradditionaldatarest;
            AssignAttri("", false, "AV24ClientAllowGetUserAddDataRest", AV24ClientAllowGetUserAddDataRest);
            AV28ClientAllowGetUserRolesRest = AV60GAMApplication.gxTpr_Clientallowgetuserrolesrest;
            AssignAttri("", false, "AV28ClientAllowGetUserRolesRest", AV28ClientAllowGetUserRolesRest);
            AV22ClientAllowGetSessionIniPropRest = AV60GAMApplication.gxTpr_Clientallowgetsessioninitialpropertiesrest;
            AssignAttri("", false, "AV22ClientAllowGetSessionIniPropRest", AV22ClientAllowGetSessionIniPropRest);
            AV20ClientAllowGetSessionAppDataREST = AV60GAMApplication.gxTpr_Clientallowgetsessionapplicationdatarest;
            AssignAttri("", false, "AV20ClientAllowGetSessionAppDataREST", AV20ClientAllowGetSessionAppDataREST);
            AV18ClientAllowAdditionalScopeREST = AV60GAMApplication.gxTpr_Clientallowadditionalscoperest;
            AssignAttri("", false, "AV18ClientAllowAdditionalScopeREST", AV18ClientAllowAdditionalScopeREST);
            AV16ClientAccessUniqueByUser = AV60GAMApplication.gxTpr_Clientaccessuniquebyuser;
            AssignAttri("", false, "AV16ClientAccessUniqueByUser", AV16ClientAccessUniqueByUser);
            AV36ClientEncryptionKey = AV60GAMApplication.gxTpr_Clientencryptionkey;
            AssignAttri("", false, "AV36ClientEncryptionKey", AV36ClientEncryptionKey);
            AV40ClientRepositoryGUID = AV60GAMApplication.gxTpr_Clientrepositoryguid;
            AssignAttri("", false, "AV40ClientRepositoryGUID", AV40ClientRepositoryGUID);
            AV7AccessRequiresPermission = AV60GAMApplication.gxTpr_Accessrequirespermission;
            AssignAttri("", false, "AV7AccessRequiresPermission", AV7AccessRequiresPermission);
            AV72IsAuthorizationDelegated = AV60GAMApplication.gxTpr_Isauthorizationdelegated;
            AssignAttri("", false, "AV72IsAuthorizationDelegated", AV72IsAuthorizationDelegated);
            AV49DelegateAuthorizationVersion = AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Version;
            AssignAttri("", false, "AV49DelegateAuthorizationVersion", AV49DelegateAuthorizationVersion);
            AV46DelegateAuthorizationFileName = AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Filename;
            AssignAttri("", false, "AV46DelegateAuthorizationFileName", AV46DelegateAuthorizationFileName);
            AV48DelegateAuthorizationPackage = AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Package;
            AssignAttri("", false, "AV48DelegateAuthorizationPackage", AV48DelegateAuthorizationPackage);
            AV45DelegateAuthorizationClassName = AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Classname;
            AssignAttri("", false, "AV45DelegateAuthorizationClassName", AV45DelegateAuthorizationClassName);
            AV47DelegateAuthorizationMethod = AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Method;
            AssignAttri("", false, "AV47DelegateAuthorizationMethod", AV47DelegateAuthorizationMethod);
            AV94SSORestEnable = AV60GAMApplication.gxTpr_Ssorestenable;
            AssignAttri("", false, "AV94SSORestEnable", AV94SSORestEnable);
            AV95SSORestMode = AV60GAMApplication.gxTpr_Ssorestmode;
            AssignAttri("", false, "AV95SSORestMode", AV95SSORestMode);
            AV101SSORestUserAuthTypeName = AV60GAMApplication.gxTpr_Ssorestuserauthenticationtypename;
            AssignAttri("", false, "AV101SSORestUserAuthTypeName", AV101SSORestUserAuthTypeName);
            AV98SSORestServerURL = AV60GAMApplication.gxTpr_Ssorestserverurl;
            AssignAttri("", false, "AV98SSORestServerURL", AV98SSORestServerURL);
            AV99SSORestServerURL_isCustom = AV60GAMApplication.gxTpr_Ssorestserverurl_iscustom;
            AssignAttri("", false, "AV99SSORestServerURL_isCustom", AV99SSORestServerURL_isCustom);
            AV100SSORestServerURL_SLO = AV60GAMApplication.gxTpr_Ssorestserverurl_slo;
            AssignAttri("", false, "AV100SSORestServerURL_SLO", AV100SSORestServerURL_SLO);
            AV97SSORestServerRepositoryGUID = AV60GAMApplication.gxTpr_Ssorestserverrepositoryguid;
            AssignAttri("", false, "AV97SSORestServerRepositoryGUID", AV97SSORestServerRepositoryGUID);
            AV96SSORestServerKey = AV60GAMApplication.gxTpr_Ssorestserverkey;
            AssignAttri("", false, "AV96SSORestServerKey", AV96SSORestServerKey);
            AV105STSProtocolEnable = AV60GAMApplication.gxTpr_Stsprotocolenable;
            AssignAttri("", false, "AV105STSProtocolEnable", AV105STSProtocolEnable);
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60GAMApplication.gxTpr_Stsauthorizationuserguid)) )
            {
               AV64GAMUser.load( AV60GAMApplication.gxTpr_Stsauthorizationuserguid);
               AV103STSAuthorizationUserName = AV64GAMUser.gxTpr_Name;
               AssignAttri("", false, "AV103STSAuthorizationUserName", AV103STSAuthorizationUserName);
            }
            AV104STSMode = AV60GAMApplication.gxTpr_Stsmode;
            AssignAttri("", false, "AV104STSMode", AV104STSMode);
            AV108STSServerURL = AV60GAMApplication.gxTpr_Stsserverurl;
            AssignAttri("", false, "AV108STSServerURL", AV108STSServerURL);
            AV106STSServerClientPassword = AV60GAMApplication.gxTpr_Stsserverclientpassword;
            AssignAttri("", false, "AV106STSServerClientPassword", AV106STSServerClientPassword);
            AV107STSServerRepositoryGUID = AV60GAMApplication.gxTpr_Stsserverrepositoryguid;
            AssignAttri("", false, "AV107STSServerRepositoryGUID", AV107STSServerRepositoryGUID);
            AV83MiniAppEnable = AV60GAMApplication.gxTpr_Miniappenable;
            AssignAttri("", false, "AV83MiniAppEnable", AV83MiniAppEnable);
            AV84MiniAppMode = AV60GAMApplication.gxTpr_Miniappmode;
            AssignAttri("", false, "AV84MiniAppMode", AV84MiniAppMode);
            AV81MiniAppClientURL = AV60GAMApplication.gxTpr_Miniappclienturl;
            AssignAttri("", false, "AV81MiniAppClientURL", AV81MiniAppClientURL);
            AV82MiniAppClientURL_isCustom = AV60GAMApplication.gxTpr_Miniappclienturl_iscustom;
            AssignAttri("", false, "AV82MiniAppClientURL_isCustom", AV82MiniAppClientURL_isCustom);
            AV80MiniAppClientRepositoryGUID = AV60GAMApplication.gxTpr_Miniappclientrepositoryguid;
            AssignAttri("", false, "AV80MiniAppClientRepositoryGUID", AV80MiniAppClientRepositoryGUID);
            AV88MiniAppUserAuthenticationTypeName = AV60GAMApplication.gxTpr_Miniappuserauthenticationtypename;
            AssignAttri("", false, "AV88MiniAppUserAuthenticationTypeName", AV88MiniAppUserAuthenticationTypeName);
            AV86MiniAppServerURL = AV60GAMApplication.gxTpr_Miniappserverurl;
            AssignAttri("", false, "AV86MiniAppServerURL", AV86MiniAppServerURL);
            AV87MiniAppServerURL_isCustom = AV60GAMApplication.gxTpr_Miniappserverurl_iscustom;
            AssignAttri("", false, "AV87MiniAppServerURL_isCustom", AV87MiniAppServerURL_isCustom);
            AV85MiniAppServerRepositoryGUID = AV60GAMApplication.gxTpr_Miniappserverrepositoryguid;
            AssignAttri("", false, "AV85MiniAppServerRepositoryGUID", AV85MiniAppServerRepositoryGUID);
            AV11APIKeyEnable = AV60GAMApplication.gxTpr_Apikeyenable;
            AssignAttri("", false, "AV11APIKeyEnable", AV11APIKeyEnable);
            AV12APIKeyTimeout = AV60GAMApplication.gxTpr_Apikeytimeout;
            AssignAttri("", false, "AV12APIKeyTimeout", StringUtil.LTrimStr( (decimal)(AV12APIKeyTimeout), 9, 0));
            AV9APIKeyAllowOnlyAuthenticationTypeName = AV60GAMApplication.gxTpr_Apikeyallowonlyauthenticationtypename;
            AssignAttri("", false, "AV9APIKeyAllowOnlyAuthenticationTypeName", AV9APIKeyAllowOnlyAuthenticationTypeName);
            AV10APIKeyAllowScopeCustomization = AV60GAMApplication.gxTpr_Apikeyallowscopecustomization;
            AssignAttri("", false, "AV10APIKeyAllowScopeCustomization", AV10APIKeyAllowScopeCustomization);
            AV52EnvironmentName = AV60GAMApplication.gxTpr_Environment.gxTpr_Name;
            AssignAttri("", false, "AV52EnvironmentName", AV52EnvironmentName);
            AV56EnvironmentSecureProtocol = AV60GAMApplication.gxTpr_Environment.gxTpr_Secureprotocol;
            AssignAttri("", false, "AV56EnvironmentSecureProtocol", AV56EnvironmentSecureProtocol);
            AV51EnvironmentHost = AV60GAMApplication.gxTpr_Environment.gxTpr_Host;
            AssignAttri("", false, "AV51EnvironmentHost", AV51EnvironmentHost);
            AV53EnvironmentPort = AV60GAMApplication.gxTpr_Environment.gxTpr_Port;
            AssignAttri("", false, "AV53EnvironmentPort", StringUtil.LTrimStr( (decimal)(AV53EnvironmentPort), 5, 0));
            AV57EnvironmentVirtualDirectory = AV60GAMApplication.gxTpr_Environment.gxTpr_Virtualdirectory;
            AssignAttri("", false, "AV57EnvironmentVirtualDirectory", AV57EnvironmentVirtualDirectory);
            AV55EnvironmentProgramPackage = AV60GAMApplication.gxTpr_Environment.gxTpr_Programpackage;
            AssignAttri("", false, "AV55EnvironmentProgramPackage", AV55EnvironmentProgramPackage);
            AV54EnvironmentProgramExtension = AV60GAMApplication.gxTpr_Environment.gxTpr_Programextension;
            AssignAttri("", false, "AV54EnvironmentProgramExtension", AV54EnvironmentProgramExtension);
            AV118GXV2 = 1;
            AV117GXV1 = AV60GAMApplication.getmenus(AV79MenuFilter, out  AV5GAMErrorCollection);
            while ( AV118GXV2 <= AV117GXV1.Count )
            {
               AV78Menu = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu)AV117GXV1.Item(AV118GXV2));
               cmbavMainmenu.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(AV78Menu.gxTpr_Id), 12, 0)), AV78Menu.gxTpr_Name, 0);
               AV118GXV2 = (int)(AV118GXV2+1);
            }
         }
         else
         {
            edtavClientsecret_Enabled = 1;
            AssignProp("", false, edtavClientsecret_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientsecret_Enabled), 5, 0), true);
            AV31ClientAuthRequestMustIncludeUserScopes = true;
            AssignAttri("", false, "AV31ClientAuthRequestMustIncludeUserScopes", AV31ClientAuthRequestMustIncludeUserScopes);
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            edtavGuid_Enabled = 0;
            AssignProp("", false, edtavGuid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGuid_Enabled), 5, 0), true);
            edtavName_Enabled = 0;
            AssignProp("", false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
            edtavDsc_Enabled = 0;
            AssignProp("", false, edtavDsc_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDsc_Enabled), 5, 0), true);
            edtavVersion_Enabled = 0;
            AssignProp("", false, edtavVersion_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavVersion_Enabled), 5, 0), true);
            edtavCopyright_Enabled = 0;
            AssignProp("", false, edtavCopyright_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCopyright_Enabled), 5, 0), true);
            edtavCompany_Enabled = 0;
            AssignProp("", false, edtavCompany_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCompany_Enabled), 5, 0), true);
            chkavReturnmenuoptionswithoutpermission.Enabled = 0;
            AssignProp("", false, chkavReturnmenuoptionswithoutpermission_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavReturnmenuoptionswithoutpermission.Enabled), 5, 0), true);
            cmbavMainmenu.Enabled = 0;
            AssignProp("", false, cmbavMainmenu_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavMainmenu.Enabled), 5, 0), true);
            chkavUseabsoluteurlbyenvironment.Enabled = 0;
            AssignProp("", false, chkavUseabsoluteurlbyenvironment_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavUseabsoluteurlbyenvironment.Enabled), 5, 0), true);
            edtavHomeobject_Enabled = 0;
            AssignProp("", false, edtavHomeobject_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavHomeobject_Enabled), 5, 0), true);
            edtavAccountactivationobject_Enabled = 0;
            AssignProp("", false, edtavAccountactivationobject_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavAccountactivationobject_Enabled), 5, 0), true);
            edtavLogoutobject_Enabled = 0;
            AssignProp("", false, edtavLogoutobject_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavLogoutobject_Enabled), 5, 0), true);
            edtavClientid_Enabled = 0;
            AssignProp("", false, edtavClientid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientid_Enabled), 5, 0), true);
            edtavClientsecret_Enabled = 0;
            AssignProp("", false, edtavClientsecret_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientsecret_Enabled), 5, 0), true);
            chkavClientauthrequestmustincludeuserscopes.Enabled = 0;
            AssignProp("", false, chkavClientauthrequestmustincludeuserscopes_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientauthrequestmustincludeuserscopes.Enabled), 5, 0), true);
            chkavClientdonotshareuserids.Enabled = 0;
            AssignProp("", false, chkavClientdonotshareuserids_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientdonotshareuserids.Enabled), 5, 0), true);
            chkavClientallowremoteauth.Enabled = 0;
            AssignProp("", false, chkavClientallowremoteauth_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowremoteauth.Enabled), 5, 0), true);
            chkavClientallowgetuserdata.Enabled = 0;
            AssignProp("", false, chkavClientallowgetuserdata_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuserdata.Enabled), 5, 0), true);
            chkavClientallowgetuseradddata.Enabled = 0;
            AssignProp("", false, chkavClientallowgetuseradddata_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuseradddata.Enabled), 5, 0), true);
            chkavClientallowgetuserroles.Enabled = 0;
            AssignProp("", false, chkavClientallowgetuserroles_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuserroles.Enabled), 5, 0), true);
            chkavClientallowgetsessioniniprop.Enabled = 0;
            AssignProp("", false, chkavClientallowgetsessioniniprop_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetsessioniniprop.Enabled), 5, 0), true);
            chkavClientallowgetsessionappdata.Enabled = 0;
            AssignProp("", false, chkavClientallowgetsessionappdata_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetsessionappdata.Enabled), 5, 0), true);
            edtavClientallowadditionalscope_Enabled = 0;
            AssignProp("", false, edtavClientallowadditionalscope_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientallowadditionalscope_Enabled), 5, 0), true);
            edtavClientimageurl_Enabled = 0;
            AssignProp("", false, edtavClientimageurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientimageurl_Enabled), 5, 0), true);
            edtavClientlocalloginurl_Enabled = 0;
            AssignProp("", false, edtavClientlocalloginurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientlocalloginurl_Enabled), 5, 0), true);
            edtavClientcallbackurl_Enabled = 0;
            AssignProp("", false, edtavClientcallbackurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientcallbackurl_Enabled), 5, 0), true);
            chkavClientcallbackurliscustom.Enabled = 0;
            AssignProp("", false, chkavClientcallbackurliscustom_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientcallbackurliscustom.Enabled), 5, 0), true);
            edtavClientcallbackurlstatename_Enabled = 0;
            AssignProp("", false, edtavClientcallbackurlstatename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientcallbackurlstatename_Enabled), 5, 0), true);
            chkavClientallowremoterestauth.Enabled = 0;
            AssignProp("", false, chkavClientallowremoterestauth_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowremoterestauth.Enabled), 5, 0), true);
            chkavClientallowgetuserdatarest.Enabled = 0;
            AssignProp("", false, chkavClientallowgetuserdatarest_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuserdatarest.Enabled), 5, 0), true);
            chkavClientallowgetuseradddatarest.Enabled = 0;
            AssignProp("", false, chkavClientallowgetuseradddatarest_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuseradddatarest.Enabled), 5, 0), true);
            chkavClientallowgetuserrolesrest.Enabled = 0;
            AssignProp("", false, chkavClientallowgetuserrolesrest_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuserrolesrest.Enabled), 5, 0), true);
            chkavClientallowgetsessioniniproprest.Enabled = 0;
            AssignProp("", false, chkavClientallowgetsessioniniproprest_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetsessioniniproprest.Enabled), 5, 0), true);
            chkavClientallowgetsessionappdatarest.Enabled = 0;
            AssignProp("", false, chkavClientallowgetsessionappdatarest_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientallowgetsessionappdatarest.Enabled), 5, 0), true);
            edtavClientallowadditionalscoperest_Enabled = 0;
            AssignProp("", false, edtavClientallowadditionalscoperest_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientallowadditionalscoperest_Enabled), 5, 0), true);
            chkavClientaccessuniquebyuser.Enabled = 0;
            AssignProp("", false, chkavClientaccessuniquebyuser_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavClientaccessuniquebyuser.Enabled), 5, 0), true);
            edtavClientencryptionkey_Enabled = 0;
            AssignProp("", false, edtavClientencryptionkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientencryptionkey_Enabled), 5, 0), true);
            edtavClientrepositoryguid_Enabled = 0;
            AssignProp("", false, edtavClientrepositoryguid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientrepositoryguid_Enabled), 5, 0), true);
            chkavAccessrequirespermission.Enabled = 0;
            AssignProp("", false, chkavAccessrequirespermission_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavAccessrequirespermission.Enabled), 5, 0), true);
            chkavIsauthorizationdelegated.Enabled = 0;
            AssignProp("", false, chkavIsauthorizationdelegated_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavIsauthorizationdelegated.Enabled), 5, 0), true);
            cmbavDelegateauthorizationversion.Enabled = 0;
            AssignProp("", false, cmbavDelegateauthorizationversion_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavDelegateauthorizationversion.Enabled), 5, 0), true);
            edtavDelegateauthorizationfilename_Enabled = 0;
            AssignProp("", false, edtavDelegateauthorizationfilename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDelegateauthorizationfilename_Enabled), 5, 0), true);
            edtavDelegateauthorizationpackage_Enabled = 0;
            AssignProp("", false, edtavDelegateauthorizationpackage_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDelegateauthorizationpackage_Enabled), 5, 0), true);
            edtavDelegateauthorizationclassname_Enabled = 0;
            AssignProp("", false, edtavDelegateauthorizationclassname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDelegateauthorizationclassname_Enabled), 5, 0), true);
            edtavDelegateauthorizationmethod_Enabled = 0;
            AssignProp("", false, edtavDelegateauthorizationmethod_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDelegateauthorizationmethod_Enabled), 5, 0), true);
            chkavSsorestenable.Enabled = 0;
            AssignProp("", false, chkavSsorestenable_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSsorestenable.Enabled), 5, 0), true);
            cmbavSsorestmode.Enabled = 0;
            AssignProp("", false, cmbavSsorestmode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavSsorestmode.Enabled), 5, 0), true);
            edtavSsorestuserauthtypename_Enabled = 0;
            AssignProp("", false, edtavSsorestuserauthtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSsorestuserauthtypename_Enabled), 5, 0), true);
            edtavSsorestserverurl_Enabled = 0;
            AssignProp("", false, edtavSsorestserverurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSsorestserverurl_Enabled), 5, 0), true);
            chkavSsorestserverurl_iscustom.Enabled = 0;
            AssignProp("", false, chkavSsorestserverurl_iscustom_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSsorestserverurl_iscustom.Enabled), 5, 0), true);
            edtavSsorestserverurl_slo_Enabled = 0;
            AssignProp("", false, edtavSsorestserverurl_slo_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSsorestserverurl_slo_Enabled), 5, 0), true);
            edtavSsorestserverrepositoryguid_Enabled = 0;
            AssignProp("", false, edtavSsorestserverrepositoryguid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSsorestserverrepositoryguid_Enabled), 5, 0), true);
            edtavSsorestserverkey_Enabled = 0;
            AssignProp("", false, edtavSsorestserverkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSsorestserverkey_Enabled), 5, 0), true);
            chkavStsprotocolenable.Enabled = 0;
            AssignProp("", false, chkavStsprotocolenable_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavStsprotocolenable.Enabled), 5, 0), true);
            edtavStsauthorizationusername_Enabled = 0;
            AssignProp("", false, edtavStsauthorizationusername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavStsauthorizationusername_Enabled), 5, 0), true);
            cmbavStsmode.Enabled = 0;
            AssignProp("", false, cmbavStsmode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavStsmode.Enabled), 5, 0), true);
            edtavStsserverurl_Enabled = 0;
            AssignProp("", false, edtavStsserverurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavStsserverurl_Enabled), 5, 0), true);
            edtavStsserverclientpassword_Enabled = 0;
            AssignProp("", false, edtavStsserverclientpassword_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavStsserverclientpassword_Enabled), 5, 0), true);
            edtavStsserverrepositoryguid_Enabled = 0;
            AssignProp("", false, edtavStsserverrepositoryguid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavStsserverrepositoryguid_Enabled), 5, 0), true);
            chkavMiniappenable.Enabled = 0;
            AssignProp("", false, chkavMiniappenable_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavMiniappenable.Enabled), 5, 0), true);
            cmbavMiniappmode.Enabled = 0;
            AssignProp("", false, cmbavMiniappmode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavMiniappmode.Enabled), 5, 0), true);
            edtavMiniappclienturl_Enabled = 0;
            AssignProp("", false, edtavMiniappclienturl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMiniappclienturl_Enabled), 5, 0), true);
            chkavMiniappclienturl_iscustom.Enabled = 0;
            AssignProp("", false, chkavMiniappclienturl_iscustom_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavMiniappclienturl_iscustom.Enabled), 5, 0), true);
            edtavMiniappclientrepositoryguid_Enabled = 0;
            AssignProp("", false, edtavMiniappclientrepositoryguid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMiniappclientrepositoryguid_Enabled), 5, 0), true);
            cmbavMiniappuserauthenticationtypename.Enabled = 0;
            AssignProp("", false, cmbavMiniappuserauthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavMiniappuserauthenticationtypename.Enabled), 5, 0), true);
            edtavMiniappserverurl_Enabled = 0;
            AssignProp("", false, edtavMiniappserverurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMiniappserverurl_Enabled), 5, 0), true);
            chkavMiniappserverurl_iscustom.Enabled = 0;
            AssignProp("", false, chkavMiniappserverurl_iscustom_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavMiniappserverurl_iscustom.Enabled), 5, 0), true);
            edtavMiniappserverrepositoryguid_Enabled = 0;
            AssignProp("", false, edtavMiniappserverrepositoryguid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMiniappserverrepositoryguid_Enabled), 5, 0), true);
            chkavApikeyenable.Enabled = 0;
            AssignProp("", false, chkavApikeyenable_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavApikeyenable.Enabled), 5, 0), true);
            edtavApikeytimeout_Enabled = 0;
            AssignProp("", false, edtavApikeytimeout_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavApikeytimeout_Enabled), 5, 0), true);
            cmbavApikeyallowonlyauthenticationtypename.Enabled = 0;
            AssignProp("", false, cmbavApikeyallowonlyauthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavApikeyallowonlyauthenticationtypename.Enabled), 5, 0), true);
            chkavApikeyallowscopecustomization.Enabled = 0;
            AssignProp("", false, chkavApikeyallowscopecustomization_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavApikeyallowscopecustomization.Enabled), 5, 0), true);
            edtavEnvironmentname_Enabled = 0;
            AssignProp("", false, edtavEnvironmentname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEnvironmentname_Enabled), 5, 0), true);
            chkavEnvironmentsecureprotocol.Enabled = 0;
            AssignProp("", false, chkavEnvironmentsecureprotocol_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavEnvironmentsecureprotocol.Enabled), 5, 0), true);
            edtavEnvironmenthost_Enabled = 0;
            AssignProp("", false, edtavEnvironmenthost_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEnvironmenthost_Enabled), 5, 0), true);
            edtavEnvironmentport_Enabled = 0;
            AssignProp("", false, edtavEnvironmentport_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEnvironmentport_Enabled), 5, 0), true);
            edtavEnvironmentvirtualdirectory_Enabled = 0;
            AssignProp("", false, edtavEnvironmentvirtualdirectory_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEnvironmentvirtualdirectory_Enabled), 5, 0), true);
            edtavEnvironmentprogrampackage_Enabled = 0;
            AssignProp("", false, edtavEnvironmentprogrampackage_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEnvironmentprogrampackage_Enabled), 5, 0), true);
            edtavEnvironmentprogramextension_Enabled = 0;
            AssignProp("", false, edtavEnvironmentprogramextension_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEnvironmentprogramextension_Enabled), 5, 0), true);
            bttBtngeneratekeygamremote_Visible = 0;
            AssignProp("", false, bttBtngeneratekeygamremote_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtngeneratekeygamremote_Visible), 5, 0), true);
            if ( (DateTime.MinValue==AV60GAMApplication.gxTpr_Clientrevoked) )
            {
               bttBtnrevokeallow_Caption = context.GetMessage( "WWP_GAM_Revoke", "");
               AssignProp("", false, bttBtnrevokeallow_Internalname, "Caption", bttBtnrevokeallow_Caption, true);
            }
            else
            {
               bttBtnrevokeallow_Caption = context.GetMessage( "WWP_GAM_Authorize", "");
               AssignProp("", false, bttBtnrevokeallow_Internalname, "Caption", bttBtnrevokeallow_Caption, true);
            }
            if ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 )
            {
               bttBtnenter_Caption = context.GetMessage( "GAM_Delete", "");
               AssignProp("", false, bttBtnenter_Internalname, "Caption", bttBtnenter_Caption, true);
            }
         }
         /* Execute user subroutine: 'UI_REMOTEAUTHENTICATIONWEB' */
         S122 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'UI_REMOTEAUTHENTICATIONREST' */
         S132 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'UI_DELEGATEAUTHORIZATION' */
         S142 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'UI_SSOREST' */
         S152 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'UI_STSPROTOCOL' */
         S162 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'UI_MINIAPP' */
         S172 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'UI_APIKEY' */
         S182 ();
         if (returnInSub) return;
         if ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 )
         {
            bttBtnenter_Visible = 0;
            AssignProp("", false, bttBtnenter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnenter_Visible), 5, 0), true);
         }
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S192 ();
         if (returnInSub) return;
         chkavAutoregisteranomymoususer.Visible = 0;
         AssignProp("", false, chkavAutoregisteranomymoususer_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavAutoregisteranomymoususer.Visible), 5, 0), true);
         edtavStsauthorizationuserguid_Visible = 0;
         AssignProp("", false, edtavStsauthorizationuserguid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavStsauthorizationuserguid_Visible), 5, 0), true);
         Gridlanguages_empowerer_Gridinternalname = subGridlanguages_Internalname;
         ucGridlanguages_empowerer.SendProperty(context, "", false, Gridlanguages_empowerer_Internalname, "GridInternalName", Gridlanguages_empowerer_Gridinternalname);
         subGridlanguages_Rows = 10;
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Rows), 6, 0, ".", "")));
         AV66GridLanguagesCurrentPage = 1;
         AssignAttri("", false, "AV66GridLanguagesCurrentPage", StringUtil.LTrimStr( (decimal)(AV66GridLanguagesCurrentPage), 10, 0));
         edtavGridlanguagescurrentpage_Visible = 0;
         AssignProp("", false, edtavGridlanguagescurrentpage_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGridlanguagescurrentpage_Visible), 5, 0), true);
         AV67GridLanguagesPageCount = -1;
         AssignAttri("", false, "AV67GridLanguagesPageCount", StringUtil.LTrimStr( (decimal)(AV67GridLanguagesPageCount), 10, 0));
         Gridlanguagespaginationbar_Rowsperpageselectedvalue = subGridlanguages_Rows;
         ucGridlanguagespaginationbar.SendProperty(context, "", false, Gridlanguagespaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridlanguagespaginationbar_Rowsperpageselectedvalue), 9, 0));
      }

      protected void E26142( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
      }

      private void E27142( )
      {
         /* Gridlanguages_Load Routine */
         returnInSub = false;
         /* Execute user subroutine: 'LOADLANGUAGES' */
         S202 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E12142( )
      {
         /* Gridlanguagespaginationbar_Changepage Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Gridlanguagespaginationbar_Selectedpage, "Previous") == 0 )
         {
            AV66GridLanguagesCurrentPage = (long)(AV66GridLanguagesCurrentPage-1);
            AssignAttri("", false, "AV66GridLanguagesCurrentPage", StringUtil.LTrimStr( (decimal)(AV66GridLanguagesCurrentPage), 10, 0));
            subgridlanguages_previouspage( ) ;
         }
         else if ( StringUtil.StrCmp(Gridlanguagespaginationbar_Selectedpage, "Next") == 0 )
         {
            AV66GridLanguagesCurrentPage = (long)(AV66GridLanguagesCurrentPage+1);
            AssignAttri("", false, "AV66GridLanguagesCurrentPage", StringUtil.LTrimStr( (decimal)(AV66GridLanguagesCurrentPage), 10, 0));
            subgridlanguages_nextpage( ) ;
         }
         else
         {
            AV91PageToGo = (int)(Math.Round(NumberUtil.Val( Gridlanguagespaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            AV66GridLanguagesCurrentPage = AV91PageToGo;
            AssignAttri("", false, "AV66GridLanguagesCurrentPage", StringUtil.LTrimStr( (decimal)(AV66GridLanguagesCurrentPage), 10, 0));
            subgridlanguages_gotopage( AV91PageToGo) ;
         }
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
      }

      protected void E13142( )
      {
         /* Gridlanguagespaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGridlanguages_Rows = Gridlanguagespaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Rows), 6, 0, ".", "")));
         AV66GridLanguagesCurrentPage = 1;
         AssignAttri("", false, "AV66GridLanguagesCurrentPage", StringUtil.LTrimStr( (decimal)(AV66GridLanguagesCurrentPage), 10, 0));
         subgridlanguages_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      protected void E14142( )
      {
         /* 'DoChangeClientSecret' Routine */
         returnInSub = false;
         /* Window Datatype Object Property */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamapplicationclientsecret.aspx"+UrlEncode(StringUtil.RTrim(AV37ClientId));
         AV6Window.Url = formatLink("gamapplicationclientsecret.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         AV6Window.SetReturnParms(new Object[] {});
         context.NewWindow(AV6Window);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamapplicationentry.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.LTrimStr(AV71Id,12,0));
         CallWebObject(formatLink("gamapplicationentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void E15142( )
      {
         /* 'DoGenerateKeyGAMRemote' Routine */
         returnInSub = false;
         AV36ClientEncryptionKey = Crypto.GetEncryptionKey( );
         AssignAttri("", false, "AV36ClientEncryptionKey", AV36ClientEncryptionKey);
         /*  Sending Event outputs  */
      }

      protected void E16142( )
      {
         /* 'DoRevokeAllow' Routine */
         returnInSub = false;
         AV60GAMApplication.load( AV71Id);
         if ( (DateTime.MinValue==AV60GAMApplication.gxTpr_Clientrevoked) )
         {
            AV74isOk = AV60GAMApplication.revokeclient(out  AV59Errors);
         }
         else
         {
            AV74isOk = AV60GAMApplication.authorizeclient(out  AV59Errors);
         }
         if ( AV74isOk )
         {
            if ( (DateTime.MinValue==AV60GAMApplication.gxTpr_Clientrevoked) )
            {
               bttBtnrevokeallow_Caption = context.GetMessage( "WWP_GAM_Revoke", "");
               AssignProp("", false, bttBtnrevokeallow_Internalname, "Caption", bttBtnrevokeallow_Caption, true);
            }
            else
            {
               bttBtnrevokeallow_Caption = context.GetMessage( "WWP_GAM_Authorize", "");
               AssignProp("", false, bttBtnrevokeallow_Internalname, "Caption", bttBtnrevokeallow_Caption, true);
            }
            context.CommitDataStores("gamapplicationentry",pr_default);
            context.DoAjaxRefresh();
         }
         else
         {
            /* Execute user subroutine: 'ERRORS' */
            S212 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV60GAMApplication", AV60GAMApplication);
      }

      protected void S192( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
         if ( ! ( ( StringUtil.StrCmp(AV104STSMode, "fulltoken") == 0 ) || ( StringUtil.StrCmp(AV104STSMode, "gettoken") == 0 ) ) )
         {
            edtavStsserverclientpassword_Visible = 0;
            AssignProp("", false, edtavStsserverclientpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavStsserverclientpassword_Visible), 5, 0), true);
            divStsserverclientpassword_cell_Class = "Invisible";
            AssignProp("", false, divStsserverclientpassword_cell_Internalname, "Class", divStsserverclientpassword_cell_Class, true);
         }
         else
         {
            edtavStsserverclientpassword_Visible = 1;
            AssignProp("", false, edtavStsserverclientpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavStsserverclientpassword_Visible), 5, 0), true);
            divStsserverclientpassword_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divStsserverclientpassword_cell_Internalname, "Class", divStsserverclientpassword_cell_Class, true);
         }
         if ( edtavStsserverclientpassword_Visible == 0 )
         {
            divTablestsclientgettoken_Visible = 0;
            AssignProp("", false, divTablestsclientgettoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclientgettoken_Visible), 5, 0), true);
         }
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E17142 ();
         if (returnInSub) return;
      }

      protected void E17142( )
      {
         /* Enter Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Gx_mode, "INS") != 0 )
         {
            AV60GAMApplication.load( AV71Id);
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) )
         {
            AV60GAMApplication.gxTpr_Name = AV89Name;
            AV60GAMApplication.gxTpr_Description = AV50Dsc;
            AV60GAMApplication.gxTpr_Version = AV111Version;
            AV60GAMApplication.gxTpr_Copyright = AV44Copyright;
            AV60GAMApplication.gxTpr_Companyname = AV43Company;
            AV60GAMApplication.gxTpr_Returnmenuoptionswithoutpermission = AV92ReturnMenuOptionsWithoutPermission;
            AV60GAMApplication.gxTpr_Mainmenuid = AV77MainMenu;
            AV60GAMApplication.gxTpr_Useabsoluteurlbyenvironment = AV109UseAbsoluteUrlByEnvironment;
            AV60GAMApplication.gxTpr_Homeobject = AV69HomeObject;
            AV60GAMApplication.gxTpr_Accountactivationobject = AV8AccountActivationObject;
            AV60GAMApplication.gxTpr_Logoutobject = AV76LogoutObject;
            AV60GAMApplication.gxTpr_Clientid = AV37ClientId;
            AV60GAMApplication.gxTpr_Clientsecret = AV42ClientSecret;
            AV60GAMApplication.gxTpr_Clientauthenticationrequestmustincludeuserscopes = AV31ClientAuthRequestMustIncludeUserScopes;
            AV60GAMApplication.gxTpr_Clientdonotshareuserids = AV35ClientDoNotShareUserIDs;
            AV60GAMApplication.gxTpr_Clientaccessuniquebyuser = AV16ClientAccessUniqueByUser;
            AV60GAMApplication.gxTpr_Clientallowremoteauthentication = AV29ClientAllowRemoteAuth;
            AV60GAMApplication.gxTpr_Clientallowgetuserdata = AV25ClientAllowGetUserData;
            AV60GAMApplication.gxTpr_Clientallowgetuseradditionaldata = AV23ClientAllowGetUserAddData;
            AV60GAMApplication.gxTpr_Clientallowgetuserroles = AV27ClientAllowGetUserRoles;
            AV60GAMApplication.gxTpr_Clientallowgetsessioninitialproperties = AV21ClientAllowGetSessionIniProp;
            AV60GAMApplication.gxTpr_Clientallowgetsessionapplicationdata = AV19ClientAllowGetSessionAppData;
            AV60GAMApplication.gxTpr_Clientallowadditionalscope = AV17ClientAllowAdditionalScope;
            AV60GAMApplication.gxTpr_Clientlocalloginurl = AV39ClientLocalLoginURL;
            AV60GAMApplication.gxTpr_Clientcallbackurl = AV32ClientCallbackURL;
            AV60GAMApplication.gxTpr_Clientcallbackurliscustom = AV33ClientCallbackURLisCustom;
            AV60GAMApplication.gxTpr_Clientcallbackurlstatename = AV34ClientCallbackURLStateName;
            AV60GAMApplication.gxTpr_Clientimageurl = AV38ClientImageURL;
            AV60GAMApplication.gxTpr_Clientallowremoterestauthentication = AV30ClientAllowRemoteRestAuth;
            AV60GAMApplication.gxTpr_Clientallowgetuserdatarest = AV26ClientAllowGetUserDataREST;
            AV60GAMApplication.gxTpr_Clientallowgetuseradditionaldatarest = AV24ClientAllowGetUserAddDataRest;
            AV60GAMApplication.gxTpr_Clientallowgetuserrolesrest = AV28ClientAllowGetUserRolesRest;
            AV60GAMApplication.gxTpr_Clientallowgetsessioninitialpropertiesrest = AV22ClientAllowGetSessionIniPropRest;
            AV60GAMApplication.gxTpr_Clientallowgetsessionapplicationdatarest = AV20ClientAllowGetSessionAppDataREST;
            AV60GAMApplication.gxTpr_Clientallowadditionalscoperest = AV18ClientAllowAdditionalScopeREST;
            AV60GAMApplication.gxTpr_Clientencryptionkey = AV36ClientEncryptionKey;
            AV60GAMApplication.gxTpr_Clientrepositoryguid = AV40ClientRepositoryGUID;
            AV60GAMApplication.gxTpr_Accessrequirespermission = AV7AccessRequiresPermission;
            AV60GAMApplication.gxTpr_Isauthorizationdelegated = AV72IsAuthorizationDelegated;
            AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Version = AV49DelegateAuthorizationVersion;
            AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Filename = AV46DelegateAuthorizationFileName;
            AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Package = AV48DelegateAuthorizationPackage;
            AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Classname = AV45DelegateAuthorizationClassName;
            AV60GAMApplication.gxTpr_Delegateauthorization.gxTpr_Method = AV47DelegateAuthorizationMethod;
            AV60GAMApplication.gxTpr_Ssorestenable = AV94SSORestEnable;
            AV60GAMApplication.gxTpr_Ssorestmode = AV95SSORestMode;
            AV60GAMApplication.gxTpr_Ssorestuserauthenticationtypename = AV101SSORestUserAuthTypeName;
            AV60GAMApplication.gxTpr_Ssorestserverurl = AV98SSORestServerURL;
            AV60GAMApplication.gxTpr_Ssorestserverurl_iscustom = AV99SSORestServerURL_isCustom;
            AV60GAMApplication.gxTpr_Ssorestserverurl_slo = AV100SSORestServerURL_SLO;
            AV60GAMApplication.gxTpr_Ssorestserverrepositoryguid = AV97SSORestServerRepositoryGUID;
            AV60GAMApplication.gxTpr_Ssorestserverkey = AV96SSORestServerKey;
            AV60GAMApplication.gxTpr_Stsprotocolenable = AV105STSProtocolEnable;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV103STSAuthorizationUserName)) )
            {
               AV64GAMUser = AV64GAMUser.getbylogin("local", AV103STSAuthorizationUserName, out  AV110UserErrors);
               AV102STSAuthorizationUserGUID = AV64GAMUser.gxTpr_Guid;
               AssignAttri("", false, "AV102STSAuthorizationUserGUID", AV102STSAuthorizationUserGUID);
            }
            AV60GAMApplication.gxTpr_Stsauthorizationuserguid = AV102STSAuthorizationUserGUID;
            AV60GAMApplication.gxTpr_Stsmode = AV104STSMode;
            AV60GAMApplication.gxTpr_Stsserverurl = AV108STSServerURL;
            AV60GAMApplication.gxTpr_Stsserverclientpassword = AV106STSServerClientPassword;
            AV60GAMApplication.gxTpr_Stsserverrepositoryguid = AV107STSServerRepositoryGUID;
            AV60GAMApplication.gxTpr_Miniappenable = AV83MiniAppEnable;
            AV60GAMApplication.gxTpr_Miniappmode = AV84MiniAppMode;
            AV60GAMApplication.gxTpr_Miniappclienturl = AV81MiniAppClientURL;
            AV60GAMApplication.gxTpr_Miniappclienturl_iscustom = AV82MiniAppClientURL_isCustom;
            AV60GAMApplication.gxTpr_Miniappclientrepositoryguid = AV80MiniAppClientRepositoryGUID;
            AV60GAMApplication.gxTpr_Miniappuserauthenticationtypename = AV88MiniAppUserAuthenticationTypeName;
            AV60GAMApplication.gxTpr_Miniappserverurl = AV86MiniAppServerURL;
            AV60GAMApplication.gxTpr_Miniappserverurl_iscustom = AV87MiniAppServerURL_isCustom;
            AV60GAMApplication.gxTpr_Miniappserverrepositoryguid = AV85MiniAppServerRepositoryGUID;
            AV60GAMApplication.gxTpr_Apikeyenable = AV11APIKeyEnable;
            AV60GAMApplication.gxTpr_Apikeytimeout = AV12APIKeyTimeout;
            AV60GAMApplication.gxTpr_Apikeyallowonlyauthenticationtypename = AV9APIKeyAllowOnlyAuthenticationTypeName;
            AV60GAMApplication.gxTpr_Apikeyallowscopecustomization = AV10APIKeyAllowScopeCustomization;
            AV60GAMApplication.gxTpr_Environment.gxTpr_Name = AV52EnvironmentName;
            AV60GAMApplication.gxTpr_Environment.gxTpr_Secureprotocol = AV56EnvironmentSecureProtocol;
            AV60GAMApplication.gxTpr_Environment.gxTpr_Host = AV51EnvironmentHost;
            AV60GAMApplication.gxTpr_Environment.gxTpr_Port = AV53EnvironmentPort;
            AV60GAMApplication.gxTpr_Environment.gxTpr_Virtualdirectory = AV57EnvironmentVirtualDirectory;
            AV60GAMApplication.gxTpr_Environment.gxTpr_Programpackage = AV55EnvironmentProgramPackage;
            AV60GAMApplication.gxTpr_Environment.gxTpr_Programextension = AV54EnvironmentProgramExtension;
            /* Execute user subroutine: 'SAVELANGUAGES' */
            S222 ();
            if (returnInSub) return;
            AV60GAMApplication.save();
         }
         else if ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 )
         {
            AV60GAMApplication.delete();
         }
         if ( AV60GAMApplication.success() && ( AV110UserErrors.Count == 0 ) )
         {
            context.CommitDataStores("gamapplicationentry",pr_default);
            CallWebObject(formatLink("gamwwapplications.aspx") );
            context.wjLocDisableFrm = 1;
         }
         else
         {
            AV59Errors = AV60GAMApplication.geterrors();
            /* Execute user subroutine: 'ERRORS' */
            S212 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV60GAMApplication", AV60GAMApplication);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV64GAMUser", AV64GAMUser);
      }

      protected void E28142( )
      {
         /* 'GenerateKeyGAMRemote' Routine */
         returnInSub = false;
         AV36ClientEncryptionKey = Crypto.GetEncryptionKey( );
         AssignAttri("", false, "AV36ClientEncryptionKey", AV36ClientEncryptionKey);
         /*  Sending Event outputs  */
      }

      protected void E29142( )
      {
         /* 'Revoke-Authorize' Routine */
         returnInSub = false;
         AV60GAMApplication.load( AV71Id);
         if ( ! (DateTime.MinValue==AV60GAMApplication.gxTpr_Clientrevoked) )
         {
            AV74isOk = AV60GAMApplication.revokeclient(out  AV5GAMErrorCollection);
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "WWP_GAM_Theapplication1wasrevoked", ""), AV60GAMApplication.gxTpr_Name, "", "", "", "", "", "", "", ""));
         }
         else
         {
            AV74isOk = AV60GAMApplication.authorizeclient(out  AV5GAMErrorCollection);
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "WWP_GAM_Theapplication1wasactivated", ""), AV60GAMApplication.gxTpr_Name, "", "", "", "", "", "", "", ""));
         }
         if ( AV74isOk )
         {
            context.CommitDataStores("gamapplicationentry",pr_default);
            /* Execute user subroutine: 'SHOWAPPLICATIONSTATUS' */
            S112 ();
            if (returnInSub) return;
         }
         AV59Errors = AV5GAMErrorCollection;
         /* Execute user subroutine: 'ERRORS' */
         S212 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV60GAMApplication", AV60GAMApplication);
         cmbavClientaccessstatus.CurrentValue = StringUtil.RTrim( AV15ClientAccessStatus);
         AssignProp("", false, cmbavClientaccessstatus_Internalname, "Values", cmbavClientaccessstatus.ToJavascriptSource(), true);
      }

      protected void E30142( )
      {
         /* 'Translations' Routine */
         returnInSub = false;
         /* Window Datatype Object Property */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamtranslations.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.RTrim("Application")) + "," + UrlEncode(StringUtil.RTrim(AV89Name)) + "," + UrlEncode(StringUtil.LTrimStr(AV71Id,12,0)) + "," + UrlEncode(StringUtil.LTrimStr(0,1,0)) + "," + UrlEncode(StringUtil.LTrimStr(0,1,0));
         AV6Window.Url = formatLink("gamtranslations.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         AV6Window.SetReturnParms(new Object[] {});
         context.NewWindow(AV6Window);
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
      }

      protected void E18142( )
      {
         /* Ssorestenable_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'UI_SSOREST' */
         S152 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E19142( )
      {
         /* Ssorestmode_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'UI_SSOREST' */
         S152 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E20142( )
      {
         /* Miniappenable_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'UI_MINIAPP' */
         S172 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         cmbavMiniappuserauthenticationtypename.CurrentValue = StringUtil.RTrim( AV88MiniAppUserAuthenticationTypeName);
         AssignProp("", false, cmbavMiniappuserauthenticationtypename_Internalname, "Values", cmbavMiniappuserauthenticationtypename.ToJavascriptSource(), true);
      }

      protected void E21142( )
      {
         /* Miniappmode_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'UI_MINIAPP' */
         S172 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         cmbavMiniappuserauthenticationtypename.CurrentValue = StringUtil.RTrim( AV88MiniAppUserAuthenticationTypeName);
         AssignProp("", false, cmbavMiniappuserauthenticationtypename_Internalname, "Values", cmbavMiniappuserauthenticationtypename.ToJavascriptSource(), true);
      }

      protected void E22142( )
      {
         /* Apikeyenable_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'UI_APIKEY' */
         S182 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         cmbavApikeyallowonlyauthenticationtypename.CurrentValue = StringUtil.RTrim( AV9APIKeyAllowOnlyAuthenticationTypeName);
         AssignProp("", false, cmbavApikeyallowonlyauthenticationtypename_Internalname, "Values", cmbavApikeyallowonlyauthenticationtypename.ToJavascriptSource(), true);
      }

      protected void E23142( )
      {
         /* Ssorestmode_Click Routine */
         returnInSub = false;
         /* Execute user subroutine: 'UI_SSOREST' */
         S152 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E24142( )
      {
         /* Ssorestenable_Click Routine */
         returnInSub = false;
         /* Execute user subroutine: 'UI_SSOREST' */
         S152 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void S212( )
      {
         /* 'ERRORS' Routine */
         returnInSub = false;
         if ( AV59Errors.Count > 0 )
         {
            AV119GXV3 = 1;
            while ( AV119GXV3 <= AV59Errors.Count )
            {
               AV58Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV59Errors.Item(AV119GXV3));
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV58Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV58Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
               AV119GXV3 = (int)(AV119GXV3+1);
            }
         }
         if ( AV110UserErrors.Count > 0 )
         {
            AV120GXV4 = 1;
            while ( AV120GXV4 <= AV110UserErrors.Count )
            {
               AV58Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV110UserErrors.Item(AV120GXV4));
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV58Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV58Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
               AV120GXV4 = (int)(AV120GXV4+1);
            }
         }
      }

      protected void S112( )
      {
         /* 'SHOWAPPLICATIONSTATUS' Routine */
         returnInSub = false;
         if ( (DateTime.MinValue==AV60GAMApplication.gxTpr_Clientrevoked) )
         {
            bttBtnrevokeallow_Caption = context.GetMessage( "GAM_Revoke", "");
            AssignProp("", false, bttBtnrevokeallow_Internalname, "Caption", bttBtnrevokeallow_Caption, true);
            AV15ClientAccessStatus = "on";
            AssignAttri("", false, "AV15ClientAccessStatus", AV15ClientAccessStatus);
            edtavClientrevoked_Visible = 0;
            AssignProp("", false, edtavClientrevoked_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavClientrevoked_Visible), 5, 0), true);
         }
         else
         {
            bttBtnrevokeallow_Caption = context.GetMessage( "GAM_Activate", "");
            AssignProp("", false, bttBtnrevokeallow_Internalname, "Caption", bttBtnrevokeallow_Caption, true);
            AV15ClientAccessStatus = "off";
            AssignAttri("", false, "AV15ClientAccessStatus", AV15ClientAccessStatus);
            edtavClientrevoked_Visible = 1;
            AssignProp("", false, edtavClientrevoked_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavClientrevoked_Visible), 5, 0), true);
            AV41ClientRevoked = AV60GAMApplication.gxTpr_Clientrevoked;
            AssignAttri("", false, "AV41ClientRevoked", context.localUtil.TToC( AV41ClientRevoked, 10, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
         }
      }

      protected void S122( )
      {
         /* 'UI_REMOTEAUTHENTICATIONWEB' Routine */
         returnInSub = false;
         if ( AV29ClientAllowRemoteAuth )
         {
            divTblwebauth_Visible = 1;
            AssignProp("", false, divTblwebauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblwebauth_Visible), 5, 0), true);
            divTblgeneralauth_Visible = 1;
            AssignProp("", false, divTblgeneralauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblgeneralauth_Visible), 5, 0), true);
         }
         else
         {
            divTblwebauth_Visible = 0;
            AssignProp("", false, divTblwebauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblwebauth_Visible), 5, 0), true);
            if ( ! AV30ClientAllowRemoteRestAuth )
            {
               divTblgeneralauth_Visible = 0;
               AssignProp("", false, divTblgeneralauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblgeneralauth_Visible), 5, 0), true);
            }
         }
      }

      protected void S132( )
      {
         /* 'UI_REMOTEAUTHENTICATIONREST' Routine */
         returnInSub = false;
         if ( AV30ClientAllowRemoteRestAuth )
         {
            divTblrestauth_Visible = 1;
            AssignProp("", false, divTblrestauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblrestauth_Visible), 5, 0), true);
            divTblgeneralauth_Visible = 1;
            AssignProp("", false, divTblgeneralauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblgeneralauth_Visible), 5, 0), true);
         }
         else
         {
            divTblrestauth_Visible = 0;
            AssignProp("", false, divTblrestauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblrestauth_Visible), 5, 0), true);
            if ( ! AV29ClientAllowRemoteAuth )
            {
               divTblgeneralauth_Visible = 0;
               AssignProp("", false, divTblgeneralauth_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblgeneralauth_Visible), 5, 0), true);
            }
         }
      }

      protected void S142( )
      {
         /* 'UI_DELEGATEAUTHORIZATION' Routine */
         returnInSub = false;
         if ( AV7AccessRequiresPermission )
         {
            divTbldelegateauthorization_Visible = 1;
            AssignProp("", false, divTbldelegateauthorization_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTbldelegateauthorization_Visible), 5, 0), true);
            if ( AV72IsAuthorizationDelegated )
            {
               divTbldelegateauthorizationprop_Visible = 1;
               AssignProp("", false, divTbldelegateauthorizationprop_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTbldelegateauthorizationprop_Visible), 5, 0), true);
            }
            else
            {
               divTbldelegateauthorizationprop_Visible = 0;
               AssignProp("", false, divTbldelegateauthorizationprop_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTbldelegateauthorizationprop_Visible), 5, 0), true);
            }
         }
         else
         {
            divTbldelegateauthorization_Visible = 0;
            AssignProp("", false, divTbldelegateauthorization_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTbldelegateauthorization_Visible), 5, 0), true);
         }
      }

      protected void S152( )
      {
         /* 'UI_SSOREST' Routine */
         returnInSub = false;
         if ( AV94SSORestEnable )
         {
            /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPESSOREST' */
            S232 ();
            if (returnInSub) return;
            divTablessorest_Visible = 1;
            AssignProp("", false, divTablessorest_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablessorest_Visible), 5, 0), true);
            divTblssorestmodeclient_Visible = 0;
            AssignProp("", false, divTblssorestmodeclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblssorestmodeclient_Visible), 5, 0), true);
            if ( StringUtil.StrCmp(AV95SSORestMode, "server") == 0 )
            {
               divTblssorestmodeclient_Visible = 0;
               AssignProp("", false, divTblssorestmodeclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblssorestmodeclient_Visible), 5, 0), true);
            }
            else if ( StringUtil.StrCmp(AV95SSORestMode, "client") == 0 )
            {
               divTblssorestmodeclient_Visible = 1;
               AssignProp("", false, divTblssorestmodeclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblssorestmodeclient_Visible), 5, 0), true);
            }
         }
         else
         {
            divTablessorest_Visible = 0;
            AssignProp("", false, divTablessorest_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablessorest_Visible), 5, 0), true);
         }
      }

      protected void S162( )
      {
         /* 'UI_STSPROTOCOL' Routine */
         returnInSub = false;
         if ( AV105STSProtocolEnable )
         {
            divTablests_Visible = 1;
            AssignProp("", false, divTablests_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablests_Visible), 5, 0), true);
            if ( StringUtil.StrCmp(AV104STSMode, "server") == 0 )
            {
               divTablestsserverchecktoken_Visible = 1;
               AssignProp("", false, divTablestsserverchecktoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsserverchecktoken_Visible), 5, 0), true);
               divTablestsclientgettoken_Visible = 0;
               AssignProp("", false, divTablestsclientgettoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclientgettoken_Visible), 5, 0), true);
               divTablestsclient_Visible = 0;
               AssignProp("", false, divTablestsclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclient_Visible), 5, 0), true);
            }
            else if ( StringUtil.StrCmp(AV104STSMode, "gettoken") == 0 )
            {
               divTablestsserverchecktoken_Visible = 0;
               AssignProp("", false, divTablestsserverchecktoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsserverchecktoken_Visible), 5, 0), true);
               divTablestsclientgettoken_Visible = 1;
               AssignProp("", false, divTablestsclientgettoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclientgettoken_Visible), 5, 0), true);
               divTablestsclient_Visible = 1;
               AssignProp("", false, divTablestsclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclient_Visible), 5, 0), true);
            }
            else if ( StringUtil.StrCmp(AV104STSMode, "checktoken") == 0 )
            {
               divTablestsserverchecktoken_Visible = 1;
               AssignProp("", false, divTablestsserverchecktoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsserverchecktoken_Visible), 5, 0), true);
               divTablestsclientgettoken_Visible = 0;
               AssignProp("", false, divTablestsclientgettoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclientgettoken_Visible), 5, 0), true);
               divTablestsclient_Visible = 1;
               AssignProp("", false, divTablestsclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclient_Visible), 5, 0), true);
            }
            else if ( StringUtil.StrCmp(AV104STSMode, "fulltoken") == 0 )
            {
               divTablestsserverchecktoken_Visible = 1;
               AssignProp("", false, divTablestsserverchecktoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsserverchecktoken_Visible), 5, 0), true);
               divTablestsclientgettoken_Visible = 1;
               AssignProp("", false, divTablestsclientgettoken_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclientgettoken_Visible), 5, 0), true);
               divTablestsclient_Visible = 1;
               AssignProp("", false, divTablestsclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestsclient_Visible), 5, 0), true);
            }
         }
         else
         {
            divTablests_Visible = 0;
            AssignProp("", false, divTablests_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablests_Visible), 5, 0), true);
         }
      }

      protected void S172( )
      {
         /* 'UI_MINIAPP' Routine */
         returnInSub = false;
         if ( AV83MiniAppEnable )
         {
            divTblminiapp_Visible = 1;
            AssignProp("", false, divTblminiapp_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblminiapp_Visible), 5, 0), true);
            /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEMINIAPP' */
            S242 ();
            if (returnInSub) return;
            divTblminiappserver_Visible = 0;
            AssignProp("", false, divTblminiappserver_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblminiappserver_Visible), 5, 0), true);
            divTblminiappclient_Visible = 0;
            AssignProp("", false, divTblminiappclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblminiappclient_Visible), 5, 0), true);
            if ( StringUtil.StrCmp(AV84MiniAppMode, "server") == 0 )
            {
               divTblminiappserver_Visible = 1;
               AssignProp("", false, divTblminiappserver_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblminiappserver_Visible), 5, 0), true);
            }
            else if ( StringUtil.StrCmp(AV84MiniAppMode, "client") == 0 )
            {
               divTblminiappclient_Visible = 1;
               AssignProp("", false, divTblminiappclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblminiappclient_Visible), 5, 0), true);
            }
         }
         else
         {
            divTblminiapp_Visible = 0;
            AssignProp("", false, divTblminiapp_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblminiapp_Visible), 5, 0), true);
         }
      }

      protected void S182( )
      {
         /* 'UI_APIKEY' Routine */
         returnInSub = false;
         if ( AV11APIKeyEnable )
         {
            divTblapikey_Visible = 1;
            AssignProp("", false, divTblapikey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblapikey_Visible), 5, 0), true);
            /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEAPIKEY' */
            S252 ();
            if (returnInSub) return;
         }
         else
         {
            divTblapikey_Visible = 0;
            AssignProp("", false, divTblapikey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblapikey_Visible), 5, 0), true);
         }
      }

      protected void S232( )
      {
         /* 'GETLISTAUTHENTICATIONTYPESSOREST' Routine */
         returnInSub = false;
         AV61GAMAuthenticationTypeFilter = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter(context);
         AV61GAMAuthenticationTypeFilter.gxTpr_Canusersbedefined = "T";
         AV122GXV6 = 1;
         AV121GXV5 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getauthenticationtypes(AV61GAMAuthenticationTypeFilter, out  AV5GAMErrorCollection);
         while ( AV122GXV6 <= AV121GXV5.Count )
         {
            AV13AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType)AV121GXV5.Item(AV122GXV6));
            AV122GXV6 = (int)(AV122GXV6+1);
         }
      }

      protected void S242( )
      {
         /* 'GETLISTAUTHENTICATIONTYPEMINIAPP' Routine */
         returnInSub = false;
         AV61GAMAuthenticationTypeFilter = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter(context);
         AV61GAMAuthenticationTypeFilter.gxTpr_Canusersbedefined = "T";
         AV124GXV8 = 1;
         AV123GXV7 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getauthenticationtypes(AV61GAMAuthenticationTypeFilter, out  AV5GAMErrorCollection);
         while ( AV124GXV8 <= AV123GXV7.Count )
         {
            AV13AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType)AV123GXV7.Item(AV124GXV8));
            cmbavMiniappuserauthenticationtypename.addItem(AV13AuthenticationType.gxTpr_Name, AV13AuthenticationType.gxTpr_Name, 0);
            AV124GXV8 = (int)(AV124GXV8+1);
         }
      }

      protected void S252( )
      {
         /* 'GETLISTAUTHENTICATIONTYPEAPIKEY' Routine */
         returnInSub = false;
         AV61GAMAuthenticationTypeFilter = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter(context);
         AV61GAMAuthenticationTypeFilter.gxTpr_Type = "APIkey";
         AV126GXV10 = 1;
         AV125GXV9 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getauthenticationtypes(AV61GAMAuthenticationTypeFilter, out  AV5GAMErrorCollection);
         while ( AV126GXV10 <= AV125GXV9.Count )
         {
            AV13AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType)AV125GXV9.Item(AV126GXV10));
            cmbavApikeyallowonlyauthenticationtypename.addItem(AV13AuthenticationType.gxTpr_Name, AV13AuthenticationType.gxTpr_Name, 0);
            AV126GXV10 = (int)(AV126GXV10+1);
         }
      }

      protected void S202( )
      {
         /* 'LOADLANGUAGES' Routine */
         returnInSub = false;
         AV63GAMLanguages = new GeneXus.Programs.genexussecurity.SdtGAM(context).getlanguages(AV60GAMApplication.gxTpr_Languages);
         AV127GXV11 = 1;
         while ( AV127GXV11 <= AV63GAMLanguages.Count )
         {
            AV62GAMLanguage = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage)AV63GAMLanguages.Item(AV127GXV11));
            AV90Online = AV62GAMLanguage.gxTpr_Online;
            AssignAttri("", false, chkavOnline_Internalname, AV90Online);
            AV75Language = AV62GAMLanguage.gxTpr_Name;
            AssignAttri("", false, edtavLanguage_Internalname, AV75Language);
            GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE"+"_"+sGXsfl_563_idx, GetSecureSignedToken( sGXsfl_563_idx, StringUtil.RTrim( context.localUtil.Format( AV75Language, "")), context));
            if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) )
            {
               /* Load Method */
               if ( wbStart != -1 )
               {
                  wbStart = 563;
               }
               if ( ( subGridlanguages_Islastpage == 1 ) || ( subGridlanguages_Rows == 0 ) || ( ( GRIDLANGUAGES_nCurrentRecord >= GRIDLANGUAGES_nFirstRecordOnPage ) && ( GRIDLANGUAGES_nCurrentRecord < GRIDLANGUAGES_nFirstRecordOnPage + subGridlanguages_fnc_Recordsperpage( ) ) ) )
               {
                  sendrow_5632( ) ;
               }
               GRIDLANGUAGES_nEOF = (short)(((GRIDLANGUAGES_nCurrentRecord<GRIDLANGUAGES_nFirstRecordOnPage+subGridlanguages_fnc_Recordsperpage( )) ? 1 : 0));
               GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nEOF), 1, 0, ".", "")));
               GRIDLANGUAGES_nCurrentRecord = (long)(GRIDLANGUAGES_nCurrentRecord+1);
               subGridlanguages_Recordcount = (int)(GRIDLANGUAGES_nCurrentRecord);
               if ( isFullAjaxMode( ) && ! bGXsfl_563_Refreshing )
               {
                  DoAjaxLoad(563, GridlanguagesRow);
               }
            }
            else
            {
               if ( AV90Online )
               {
                  /* Load Method */
                  if ( wbStart != -1 )
                  {
                     wbStart = 563;
                  }
                  if ( ( subGridlanguages_Islastpage == 1 ) || ( subGridlanguages_Rows == 0 ) || ( ( GRIDLANGUAGES_nCurrentRecord >= GRIDLANGUAGES_nFirstRecordOnPage ) && ( GRIDLANGUAGES_nCurrentRecord < GRIDLANGUAGES_nFirstRecordOnPage + subGridlanguages_fnc_Recordsperpage( ) ) ) )
                  {
                     sendrow_5632( ) ;
                  }
                  GRIDLANGUAGES_nEOF = (short)(((GRIDLANGUAGES_nCurrentRecord<GRIDLANGUAGES_nFirstRecordOnPage+subGridlanguages_fnc_Recordsperpage( )) ? 1 : 0));
                  GxWebStd.gx_hidden_field( context, "GRIDLANGUAGES_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDLANGUAGES_nEOF), 1, 0, ".", "")));
                  GRIDLANGUAGES_nCurrentRecord = (long)(GRIDLANGUAGES_nCurrentRecord+1);
                  subGridlanguages_Recordcount = (int)(GRIDLANGUAGES_nCurrentRecord);
                  if ( isFullAjaxMode( ) && ! bGXsfl_563_Refreshing )
                  {
                     DoAjaxLoad(563, GridlanguagesRow);
                  }
               }
            }
            AV127GXV11 = (int)(AV127GXV11+1);
         }
      }

      protected void S222( )
      {
         /* 'SAVELANGUAGES' Routine */
         returnInSub = false;
         AV63GAMLanguages = new GeneXus.Programs.genexussecurity.SdtGAM(context).getlanguages(AV60GAMApplication.gxTpr_Languages);
         /* Start For Each Line */
         nRC_GXsfl_563 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_563"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         nGXsfl_563_fel_idx = 0;
         while ( nGXsfl_563_fel_idx < nRC_GXsfl_563 )
         {
            nGXsfl_563_fel_idx = ((subGridlanguages_Islastpage==1)&&(nGXsfl_563_fel_idx+1>subGridlanguages_fnc_Recordsperpage( )) ? 1 : nGXsfl_563_fel_idx+1);
            sGXsfl_563_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_563_fel_idx), 4, 0), 4, "0");
            SubsflControlProps_fel_5632( ) ;
            AV90Online = StringUtil.StrToBool( cgiGet( chkavOnline_Internalname));
            AV75Language = cgiGet( edtavLanguage_Internalname);
            AV129GXV12 = 1;
            while ( AV129GXV12 <= AV63GAMLanguages.Count )
            {
               AV62GAMLanguage = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage)AV63GAMLanguages.Item(AV129GXV12));
               if ( StringUtil.StrCmp(AV62GAMLanguage.gxTpr_Name, AV75Language) == 0 )
               {
                  AV62GAMLanguage.gxTpr_Online = AV90Online;
               }
               AV129GXV12 = (int)(AV129GXV12+1);
            }
            /* End For Each Line */
         }
         if ( nGXsfl_563_fel_idx == 0 )
         {
            nGXsfl_563_idx = 1;
            sGXsfl_563_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_563_idx), 4, 0), 4, "0");
            SubsflControlProps_5632( ) ;
         }
         nGXsfl_563_fel_idx = 1;
         AV60GAMApplication.gxTpr_Languages = AV63GAMLanguages;
      }

      protected void wb_table3_270_142( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablemergedclientencryptionkey_Internalname, tblTablemergedclientencryptionkey_Internalname, "", "TableMerged", 0, "", "", 0, 0, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td class='MergeDataCell'>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientencryptionkey_Internalname, context.GetMessage( "Client Encryption Key", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 274,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientencryptionkey_Internalname, StringUtil.RTrim( AV36ClientEncryptionKey), StringUtil.RTrim( context.localUtil.Format( AV36ClientEncryptionKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,274);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientencryptionkey_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientencryptionkey_Enabled, 1, "text", "", 32, "chr", 1, "row", 32, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMEncryptionKey", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 276,'',false,'',0)\"";
            ClassString = "Button ButtonMaterialGAM";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtngeneratekeygamremote_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(563), 3, 0)+","+"null"+");", context.GetMessage( "WWP_GAM_GenerateKeyGAMRemote", ""), bttBtngeneratekeygamremote_Jsonclick, 5, context.GetMessage( "WWP_GAM_GenerateKeyGAMRemote", ""), "", StyleString, ClassString, bttBtngeneratekeygamremote_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOGENERATEKEYGAMREMOTE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMApplicationEntry.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table3_270_142e( true) ;
         }
         else
         {
            wb_table3_270_142e( false) ;
         }
      }

      protected void wb_table2_122_142( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablemergedclientsecret_Internalname, tblTablemergedclientsecret_Internalname, "", "TableMerged", 0, "", "", 0, 0, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td class='MergeDataCell'>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientsecret_Internalname, context.GetMessage( "Client Secret", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 126,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientsecret_Internalname, StringUtil.RTrim( AV42ClientSecret), StringUtil.RTrim( context.localUtil.Format( AV42ClientSecret, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,126);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientsecret_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientsecret_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMClientApplicationSecret", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 128,'',false,'',0)\"";
            ClassString = "Button ButtonMaterialGAM";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnchangeclientsecret_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(563), 3, 0)+","+"null"+");", context.GetMessage( "WWP_GAM_Change", ""), bttBtnchangeclientsecret_Jsonclick, 5, context.GetMessage( "WWP_GAM_Change", ""), "", StyleString, ClassString, bttBtnchangeclientsecret_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOCHANGECLIENTSECRET\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMApplicationEntry.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table2_122_142e( true) ;
         }
         else
         {
            wb_table2_122_142e( false) ;
         }
      }

      protected void wb_table1_88_142( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablemergedlogoutobject_Internalname, tblTablemergedlogoutobject_Internalname, "", "TableMerged", 0, "", "", 0, 0, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td class='MergeDataCell'>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavLogoutobject_Internalname, context.GetMessage( "Logout Object", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 92,'',false,'" + sGXsfl_563_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavLogoutobject_Internalname, AV76LogoutObject, StringUtil.RTrim( context.localUtil.Format( AV76LogoutObject, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,92);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavLogoutobject_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavLogoutobject_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMApplicationEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 94,'',false,'',0)\"";
            ClassString = "Button ButtonMaterialGAM";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnrevokeallow_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(563), 3, 0)+","+"null"+");", bttBtnrevokeallow_Caption, bttBtnrevokeallow_Jsonclick, 5, context.GetMessage( "WWP_GAM_Revoke", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOREVOKEALLOW\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMApplicationEntry.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_88_142e( true) ;
         }
         else
         {
            wb_table1_88_142e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         Gx_mode = (string)getParm(obj,0);
         AssignAttri("", false, "Gx_mode", Gx_mode);
         AV71Id = Convert.ToInt64(getParm(obj,1));
         AssignAttri("", false, "AV71Id", StringUtil.LTrimStr( (decimal)(AV71Id), 12, 0));
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
         PA142( ) ;
         WS142( ) ;
         WE142( ) ;
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
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202412162354189", true, true);
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
         context.AddJavascriptSource("gamapplicationentry.js", "?202412162354193", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/TabRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_5632( )
      {
         chkavOnline_Internalname = "vONLINE_"+sGXsfl_563_idx;
         edtavLanguage_Internalname = "vLANGUAGE_"+sGXsfl_563_idx;
      }

      protected void SubsflControlProps_fel_5632( )
      {
         chkavOnline_Internalname = "vONLINE_"+sGXsfl_563_fel_idx;
         edtavLanguage_Internalname = "vLANGUAGE_"+sGXsfl_563_fel_idx;
      }

      protected void sendrow_5632( )
      {
         sGXsfl_563_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_563_idx), 4, 0), 4, "0");
         SubsflControlProps_5632( ) ;
         WB140( ) ;
         if ( ( subGridlanguages_Rows * 1 == 0 ) || ( nGXsfl_563_idx <= subGridlanguages_fnc_Recordsperpage( ) * 1 ) )
         {
            GridlanguagesRow = GXWebRow.GetNew(context,GridlanguagesContainer);
            if ( subGridlanguages_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGridlanguages_Backstyle = 0;
               if ( StringUtil.StrCmp(subGridlanguages_Class, "") != 0 )
               {
                  subGridlanguages_Linesclass = subGridlanguages_Class+"Odd";
               }
            }
            else if ( subGridlanguages_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGridlanguages_Backstyle = 0;
               subGridlanguages_Backcolor = subGridlanguages_Allbackcolor;
               if ( StringUtil.StrCmp(subGridlanguages_Class, "") != 0 )
               {
                  subGridlanguages_Linesclass = subGridlanguages_Class+"Uniform";
               }
            }
            else if ( subGridlanguages_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGridlanguages_Backstyle = 1;
               if ( StringUtil.StrCmp(subGridlanguages_Class, "") != 0 )
               {
                  subGridlanguages_Linesclass = subGridlanguages_Class+"Odd";
               }
               subGridlanguages_Backcolor = (int)(0x0);
            }
            else if ( subGridlanguages_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGridlanguages_Backstyle = 1;
               if ( ((int)((nGXsfl_563_idx) % (2))) == 0 )
               {
                  subGridlanguages_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridlanguages_Class, "") != 0 )
                  {
                     subGridlanguages_Linesclass = subGridlanguages_Class+"Even";
                  }
               }
               else
               {
                  subGridlanguages_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridlanguages_Class, "") != 0 )
                  {
                     subGridlanguages_Linesclass = subGridlanguages_Class+"Odd";
                  }
               }
            }
            if ( GridlanguagesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"GridWithPaginationBar GridNoBorder WorkWith"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_563_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridlanguagesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
            }
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 564,'',false,'" + sGXsfl_563_idx + "',563)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GXCCtl = "vONLINE_" + sGXsfl_563_idx;
            chkavOnline.Name = GXCCtl;
            chkavOnline.WebTags = "";
            chkavOnline.Caption = "";
            AssignProp("", false, chkavOnline_Internalname, "TitleCaption", chkavOnline.Caption, !bGXsfl_563_Refreshing);
            chkavOnline.CheckedValue = "false";
            AV90Online = StringUtil.StrToBool( StringUtil.BoolToStr( AV90Online));
            AssignAttri("", false, chkavOnline_Internalname, AV90Online);
            GridlanguagesRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkavOnline_Internalname,StringUtil.BoolToStr( AV90Online),(string)"",(string)"",(short)-1,chkavOnline.Enabled,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"WWColumn",(string)"",TempTags+" onclick="+"\"gx.fn.checkboxClick(564, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,564);\""});
            /* Subfile cell */
            if ( GridlanguagesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 565,'',false,'" + sGXsfl_563_idx + "',563)\"";
            ROClassString = "Attribute";
            GridlanguagesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavLanguage_Internalname,(string)AV75Language,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,565);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavLanguage_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)-1,(int)edtavLanguage_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)563,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes142( ) ;
            GridlanguagesContainer.AddRow(GridlanguagesRow);
            nGXsfl_563_idx = ((subGridlanguages_Islastpage==1)&&(nGXsfl_563_idx+1>subGridlanguages_fnc_Recordsperpage( )) ? 1 : nGXsfl_563_idx+1);
            sGXsfl_563_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_563_idx), 4, 0), 4, "0");
            SubsflControlProps_5632( ) ;
         }
         /* End function sendrow_5632 */
      }

      protected void init_web_controls( )
      {
         chkavReturnmenuoptionswithoutpermission.Name = "vRETURNMENUOPTIONSWITHOUTPERMISSION";
         chkavReturnmenuoptionswithoutpermission.WebTags = "";
         chkavReturnmenuoptionswithoutpermission.Caption = " ";
         AssignProp("", false, chkavReturnmenuoptionswithoutpermission_Internalname, "TitleCaption", chkavReturnmenuoptionswithoutpermission.Caption, true);
         chkavReturnmenuoptionswithoutpermission.CheckedValue = "false";
         AV92ReturnMenuOptionsWithoutPermission = StringUtil.StrToBool( StringUtil.BoolToStr( AV92ReturnMenuOptionsWithoutPermission));
         AssignAttri("", false, "AV92ReturnMenuOptionsWithoutPermission", AV92ReturnMenuOptionsWithoutPermission);
         cmbavMainmenu.Name = "vMAINMENU";
         cmbavMainmenu.WebTags = "";
         if ( cmbavMainmenu.ItemCount > 0 )
         {
            AV77MainMenu = (long)(Math.Round(NumberUtil.Val( cmbavMainmenu.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV77MainMenu), 12, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV77MainMenu", StringUtil.LTrimStr( (decimal)(AV77MainMenu), 12, 0));
         }
         chkavUseabsoluteurlbyenvironment.Name = "vUSEABSOLUTEURLBYENVIRONMENT";
         chkavUseabsoluteurlbyenvironment.WebTags = "";
         chkavUseabsoluteurlbyenvironment.Caption = " ";
         AssignProp("", false, chkavUseabsoluteurlbyenvironment_Internalname, "TitleCaption", chkavUseabsoluteurlbyenvironment.Caption, true);
         chkavUseabsoluteurlbyenvironment.CheckedValue = "false";
         AV109UseAbsoluteUrlByEnvironment = StringUtil.StrToBool( StringUtil.BoolToStr( AV109UseAbsoluteUrlByEnvironment));
         AssignAttri("", false, "AV109UseAbsoluteUrlByEnvironment", AV109UseAbsoluteUrlByEnvironment);
         cmbavClientaccessstatus.Name = "vCLIENTACCESSSTATUS";
         cmbavClientaccessstatus.WebTags = "";
         cmbavClientaccessstatus.addItem("on", context.GetMessage( "Online", ""), 0);
         cmbavClientaccessstatus.addItem("off", context.GetMessage( "Offline", ""), 0);
         if ( cmbavClientaccessstatus.ItemCount > 0 )
         {
            AV15ClientAccessStatus = cmbavClientaccessstatus.getValidValue(AV15ClientAccessStatus);
            AssignAttri("", false, "AV15ClientAccessStatus", AV15ClientAccessStatus);
         }
         chkavClientauthrequestmustincludeuserscopes.Name = "vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES";
         chkavClientauthrequestmustincludeuserscopes.WebTags = "";
         chkavClientauthrequestmustincludeuserscopes.Caption = " ";
         AssignProp("", false, chkavClientauthrequestmustincludeuserscopes_Internalname, "TitleCaption", chkavClientauthrequestmustincludeuserscopes.Caption, true);
         chkavClientauthrequestmustincludeuserscopes.CheckedValue = "false";
         AV31ClientAuthRequestMustIncludeUserScopes = StringUtil.StrToBool( StringUtil.BoolToStr( AV31ClientAuthRequestMustIncludeUserScopes));
         AssignAttri("", false, "AV31ClientAuthRequestMustIncludeUserScopes", AV31ClientAuthRequestMustIncludeUserScopes);
         chkavClientdonotshareuserids.Name = "vCLIENTDONOTSHAREUSERIDS";
         chkavClientdonotshareuserids.WebTags = "";
         chkavClientdonotshareuserids.Caption = " ";
         AssignProp("", false, chkavClientdonotshareuserids_Internalname, "TitleCaption", chkavClientdonotshareuserids.Caption, true);
         chkavClientdonotshareuserids.CheckedValue = "false";
         AV35ClientDoNotShareUserIDs = StringUtil.StrToBool( StringUtil.BoolToStr( AV35ClientDoNotShareUserIDs));
         AssignAttri("", false, "AV35ClientDoNotShareUserIDs", AV35ClientDoNotShareUserIDs);
         chkavClientallowremoteauth.Name = "vCLIENTALLOWREMOTEAUTH";
         chkavClientallowremoteauth.WebTags = "";
         chkavClientallowremoteauth.Caption = " ";
         AssignProp("", false, chkavClientallowremoteauth_Internalname, "TitleCaption", chkavClientallowremoteauth.Caption, true);
         chkavClientallowremoteauth.CheckedValue = "false";
         AV29ClientAllowRemoteAuth = StringUtil.StrToBool( StringUtil.BoolToStr( AV29ClientAllowRemoteAuth));
         AssignAttri("", false, "AV29ClientAllowRemoteAuth", AV29ClientAllowRemoteAuth);
         chkavClientallowgetuserdata.Name = "vCLIENTALLOWGETUSERDATA";
         chkavClientallowgetuserdata.WebTags = "";
         chkavClientallowgetuserdata.Caption = " ";
         AssignProp("", false, chkavClientallowgetuserdata_Internalname, "TitleCaption", chkavClientallowgetuserdata.Caption, true);
         chkavClientallowgetuserdata.CheckedValue = "false";
         AV25ClientAllowGetUserData = StringUtil.StrToBool( StringUtil.BoolToStr( AV25ClientAllowGetUserData));
         AssignAttri("", false, "AV25ClientAllowGetUserData", AV25ClientAllowGetUserData);
         chkavClientallowgetuseradddata.Name = "vCLIENTALLOWGETUSERADDDATA";
         chkavClientallowgetuseradddata.WebTags = "";
         chkavClientallowgetuseradddata.Caption = " ";
         AssignProp("", false, chkavClientallowgetuseradddata_Internalname, "TitleCaption", chkavClientallowgetuseradddata.Caption, true);
         chkavClientallowgetuseradddata.CheckedValue = "false";
         AV23ClientAllowGetUserAddData = StringUtil.StrToBool( StringUtil.BoolToStr( AV23ClientAllowGetUserAddData));
         AssignAttri("", false, "AV23ClientAllowGetUserAddData", AV23ClientAllowGetUserAddData);
         chkavClientallowgetuserroles.Name = "vCLIENTALLOWGETUSERROLES";
         chkavClientallowgetuserroles.WebTags = "";
         chkavClientallowgetuserroles.Caption = " ";
         AssignProp("", false, chkavClientallowgetuserroles_Internalname, "TitleCaption", chkavClientallowgetuserroles.Caption, true);
         chkavClientallowgetuserroles.CheckedValue = "false";
         AV27ClientAllowGetUserRoles = StringUtil.StrToBool( StringUtil.BoolToStr( AV27ClientAllowGetUserRoles));
         AssignAttri("", false, "AV27ClientAllowGetUserRoles", AV27ClientAllowGetUserRoles);
         chkavClientallowgetsessioniniprop.Name = "vCLIENTALLOWGETSESSIONINIPROP";
         chkavClientallowgetsessioniniprop.WebTags = "";
         chkavClientallowgetsessioniniprop.Caption = " ";
         AssignProp("", false, chkavClientallowgetsessioniniprop_Internalname, "TitleCaption", chkavClientallowgetsessioniniprop.Caption, true);
         chkavClientallowgetsessioniniprop.CheckedValue = "false";
         AV21ClientAllowGetSessionIniProp = StringUtil.StrToBool( StringUtil.BoolToStr( AV21ClientAllowGetSessionIniProp));
         AssignAttri("", false, "AV21ClientAllowGetSessionIniProp", AV21ClientAllowGetSessionIniProp);
         chkavClientallowgetsessionappdata.Name = "vCLIENTALLOWGETSESSIONAPPDATA";
         chkavClientallowgetsessionappdata.WebTags = "";
         chkavClientallowgetsessionappdata.Caption = " ";
         AssignProp("", false, chkavClientallowgetsessionappdata_Internalname, "TitleCaption", chkavClientallowgetsessionappdata.Caption, true);
         chkavClientallowgetsessionappdata.CheckedValue = "false";
         AV19ClientAllowGetSessionAppData = StringUtil.StrToBool( StringUtil.BoolToStr( AV19ClientAllowGetSessionAppData));
         AssignAttri("", false, "AV19ClientAllowGetSessionAppData", AV19ClientAllowGetSessionAppData);
         chkavClientcallbackurliscustom.Name = "vCLIENTCALLBACKURLISCUSTOM";
         chkavClientcallbackurliscustom.WebTags = "";
         chkavClientcallbackurliscustom.Caption = " ";
         AssignProp("", false, chkavClientcallbackurliscustom_Internalname, "TitleCaption", chkavClientcallbackurliscustom.Caption, true);
         chkavClientcallbackurliscustom.CheckedValue = "false";
         AV33ClientCallbackURLisCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV33ClientCallbackURLisCustom));
         AssignAttri("", false, "AV33ClientCallbackURLisCustom", AV33ClientCallbackURLisCustom);
         chkavClientallowremoterestauth.Name = "vCLIENTALLOWREMOTERESTAUTH";
         chkavClientallowremoterestauth.WebTags = "";
         chkavClientallowremoterestauth.Caption = " ";
         AssignProp("", false, chkavClientallowremoterestauth_Internalname, "TitleCaption", chkavClientallowremoterestauth.Caption, true);
         chkavClientallowremoterestauth.CheckedValue = "false";
         AV30ClientAllowRemoteRestAuth = StringUtil.StrToBool( StringUtil.BoolToStr( AV30ClientAllowRemoteRestAuth));
         AssignAttri("", false, "AV30ClientAllowRemoteRestAuth", AV30ClientAllowRemoteRestAuth);
         chkavClientallowgetuserdatarest.Name = "vCLIENTALLOWGETUSERDATAREST";
         chkavClientallowgetuserdatarest.WebTags = "";
         chkavClientallowgetuserdatarest.Caption = " ";
         AssignProp("", false, chkavClientallowgetuserdatarest_Internalname, "TitleCaption", chkavClientallowgetuserdatarest.Caption, true);
         chkavClientallowgetuserdatarest.CheckedValue = "false";
         AV26ClientAllowGetUserDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV26ClientAllowGetUserDataREST));
         AssignAttri("", false, "AV26ClientAllowGetUserDataREST", AV26ClientAllowGetUserDataREST);
         chkavClientallowgetuseradddatarest.Name = "vCLIENTALLOWGETUSERADDDATAREST";
         chkavClientallowgetuseradddatarest.WebTags = "";
         chkavClientallowgetuseradddatarest.Caption = " ";
         AssignProp("", false, chkavClientallowgetuseradddatarest_Internalname, "TitleCaption", chkavClientallowgetuseradddatarest.Caption, true);
         chkavClientallowgetuseradddatarest.CheckedValue = "false";
         AV24ClientAllowGetUserAddDataRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV24ClientAllowGetUserAddDataRest));
         AssignAttri("", false, "AV24ClientAllowGetUserAddDataRest", AV24ClientAllowGetUserAddDataRest);
         chkavClientallowgetuserrolesrest.Name = "vCLIENTALLOWGETUSERROLESREST";
         chkavClientallowgetuserrolesrest.WebTags = "";
         chkavClientallowgetuserrolesrest.Caption = " ";
         AssignProp("", false, chkavClientallowgetuserrolesrest_Internalname, "TitleCaption", chkavClientallowgetuserrolesrest.Caption, true);
         chkavClientallowgetuserrolesrest.CheckedValue = "false";
         AV28ClientAllowGetUserRolesRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV28ClientAllowGetUserRolesRest));
         AssignAttri("", false, "AV28ClientAllowGetUserRolesRest", AV28ClientAllowGetUserRolesRest);
         chkavClientallowgetsessioniniproprest.Name = "vCLIENTALLOWGETSESSIONINIPROPREST";
         chkavClientallowgetsessioniniproprest.WebTags = "";
         chkavClientallowgetsessioniniproprest.Caption = " ";
         AssignProp("", false, chkavClientallowgetsessioniniproprest_Internalname, "TitleCaption", chkavClientallowgetsessioniniproprest.Caption, true);
         chkavClientallowgetsessioniniproprest.CheckedValue = "false";
         AV22ClientAllowGetSessionIniPropRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV22ClientAllowGetSessionIniPropRest));
         AssignAttri("", false, "AV22ClientAllowGetSessionIniPropRest", AV22ClientAllowGetSessionIniPropRest);
         chkavClientallowgetsessionappdatarest.Name = "vCLIENTALLOWGETSESSIONAPPDATAREST";
         chkavClientallowgetsessionappdatarest.WebTags = "";
         chkavClientallowgetsessionappdatarest.Caption = " ";
         AssignProp("", false, chkavClientallowgetsessionappdatarest_Internalname, "TitleCaption", chkavClientallowgetsessionappdatarest.Caption, true);
         chkavClientallowgetsessionappdatarest.CheckedValue = "false";
         AV20ClientAllowGetSessionAppDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV20ClientAllowGetSessionAppDataREST));
         AssignAttri("", false, "AV20ClientAllowGetSessionAppDataREST", AV20ClientAllowGetSessionAppDataREST);
         chkavClientaccessuniquebyuser.Name = "vCLIENTACCESSUNIQUEBYUSER";
         chkavClientaccessuniquebyuser.WebTags = "";
         chkavClientaccessuniquebyuser.Caption = " ";
         AssignProp("", false, chkavClientaccessuniquebyuser_Internalname, "TitleCaption", chkavClientaccessuniquebyuser.Caption, true);
         chkavClientaccessuniquebyuser.CheckedValue = "false";
         AV16ClientAccessUniqueByUser = StringUtil.StrToBool( StringUtil.BoolToStr( AV16ClientAccessUniqueByUser));
         AssignAttri("", false, "AV16ClientAccessUniqueByUser", AV16ClientAccessUniqueByUser);
         chkavAccessrequirespermission.Name = "vACCESSREQUIRESPERMISSION";
         chkavAccessrequirespermission.WebTags = "";
         chkavAccessrequirespermission.Caption = context.GetMessage( "WWP_GAM_EnableAuthorization", "");
         AssignProp("", false, chkavAccessrequirespermission_Internalname, "TitleCaption", chkavAccessrequirespermission.Caption, true);
         chkavAccessrequirespermission.CheckedValue = "false";
         AV7AccessRequiresPermission = StringUtil.StrToBool( StringUtil.BoolToStr( AV7AccessRequiresPermission));
         AssignAttri("", false, "AV7AccessRequiresPermission", AV7AccessRequiresPermission);
         chkavIsauthorizationdelegated.Name = "vISAUTHORIZATIONDELEGATED";
         chkavIsauthorizationdelegated.WebTags = "";
         chkavIsauthorizationdelegated.Caption = context.GetMessage( "WWP_GAM_Delegateauthorizationcheckingtoanexternalprogram", "");
         AssignProp("", false, chkavIsauthorizationdelegated_Internalname, "TitleCaption", chkavIsauthorizationdelegated.Caption, true);
         chkavIsauthorizationdelegated.CheckedValue = "false";
         AV72IsAuthorizationDelegated = StringUtil.StrToBool( StringUtil.BoolToStr( AV72IsAuthorizationDelegated));
         AssignAttri("", false, "AV72IsAuthorizationDelegated", AV72IsAuthorizationDelegated);
         cmbavDelegateauthorizationversion.Name = "vDELEGATEAUTHORIZATIONVERSION";
         cmbavDelegateauthorizationversion.WebTags = "";
         cmbavDelegateauthorizationversion.addItem("10", context.GetMessage( "GAM_Version10", ""), 0);
         if ( cmbavDelegateauthorizationversion.ItemCount > 0 )
         {
            AV49DelegateAuthorizationVersion = cmbavDelegateauthorizationversion.getValidValue(AV49DelegateAuthorizationVersion);
            AssignAttri("", false, "AV49DelegateAuthorizationVersion", AV49DelegateAuthorizationVersion);
         }
         chkavSsorestenable.Name = "vSSORESTENABLE";
         chkavSsorestenable.WebTags = "";
         chkavSsorestenable.Caption = " ";
         AssignProp("", false, chkavSsorestenable_Internalname, "TitleCaption", chkavSsorestenable.Caption, true);
         chkavSsorestenable.CheckedValue = "false";
         AV94SSORestEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV94SSORestEnable));
         AssignAttri("", false, "AV94SSORestEnable", AV94SSORestEnable);
         cmbavSsorestmode.Name = "vSSORESTMODE";
         cmbavSsorestmode.WebTags = "";
         cmbavSsorestmode.addItem("server", context.GetMessage( "GAM_Server", ""), 0);
         cmbavSsorestmode.addItem("client", context.GetMessage( "GAM_Client", ""), 0);
         if ( cmbavSsorestmode.ItemCount > 0 )
         {
            AV95SSORestMode = cmbavSsorestmode.getValidValue(AV95SSORestMode);
            AssignAttri("", false, "AV95SSORestMode", AV95SSORestMode);
         }
         chkavSsorestserverurl_iscustom.Name = "vSSORESTSERVERURL_ISCUSTOM";
         chkavSsorestserverurl_iscustom.WebTags = "";
         chkavSsorestserverurl_iscustom.Caption = context.GetMessage( "WWP_GAM_CustomserverURLSSO", "");
         AssignProp("", false, chkavSsorestserverurl_iscustom_Internalname, "TitleCaption", chkavSsorestserverurl_iscustom.Caption, true);
         chkavSsorestserverurl_iscustom.CheckedValue = "false";
         AV99SSORestServerURL_isCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV99SSORestServerURL_isCustom));
         AssignAttri("", false, "AV99SSORestServerURL_isCustom", AV99SSORestServerURL_isCustom);
         chkavStsprotocolenable.Name = "vSTSPROTOCOLENABLE";
         chkavStsprotocolenable.WebTags = "";
         chkavStsprotocolenable.Caption = " ";
         AssignProp("", false, chkavStsprotocolenable_Internalname, "TitleCaption", chkavStsprotocolenable.Caption, true);
         chkavStsprotocolenable.CheckedValue = "false";
         AV105STSProtocolEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV105STSProtocolEnable));
         AssignAttri("", false, "AV105STSProtocolEnable", AV105STSProtocolEnable);
         cmbavStsmode.Name = "vSTSMODE";
         cmbavStsmode.WebTags = "";
         cmbavStsmode.addItem("server", context.GetMessage( "GAM_Server", ""), 0);
         cmbavStsmode.addItem("gettoken", context.GetMessage( "GAM_Gettoken", ""), 0);
         cmbavStsmode.addItem("checktoken", context.GetMessage( "GAM_Checktoken", ""), 0);
         cmbavStsmode.addItem("fulltoken", context.GetMessage( "GAM_Getandchecktoken", ""), 0);
         if ( cmbavStsmode.ItemCount > 0 )
         {
            AV104STSMode = cmbavStsmode.getValidValue(AV104STSMode);
            AssignAttri("", false, "AV104STSMode", AV104STSMode);
         }
         chkavMiniappenable.Name = "vMINIAPPENABLE";
         chkavMiniappenable.WebTags = "";
         chkavMiniappenable.Caption = context.GetMessage( "WWP_GAM_EnableworkasMiniApp", "");
         AssignProp("", false, chkavMiniappenable_Internalname, "TitleCaption", chkavMiniappenable.Caption, true);
         chkavMiniappenable.CheckedValue = "false";
         AV83MiniAppEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV83MiniAppEnable));
         AssignAttri("", false, "AV83MiniAppEnable", AV83MiniAppEnable);
         cmbavMiniappmode.Name = "vMINIAPPMODE";
         cmbavMiniappmode.WebTags = "";
         cmbavMiniappmode.addItem("server", context.GetMessage( "GAM_Server", ""), 0);
         cmbavMiniappmode.addItem("client", context.GetMessage( "GAM_Client", ""), 0);
         if ( cmbavMiniappmode.ItemCount > 0 )
         {
            AV84MiniAppMode = cmbavMiniappmode.getValidValue(AV84MiniAppMode);
            AssignAttri("", false, "AV84MiniAppMode", AV84MiniAppMode);
         }
         chkavMiniappclienturl_iscustom.Name = "vMINIAPPCLIENTURL_ISCUSTOM";
         chkavMiniappclienturl_iscustom.WebTags = "";
         chkavMiniappclienturl_iscustom.Caption = context.GetMessage( "WWP_GAM_CustomMiniAppclientURL", "");
         AssignProp("", false, chkavMiniappclienturl_iscustom_Internalname, "TitleCaption", chkavMiniappclienturl_iscustom.Caption, true);
         chkavMiniappclienturl_iscustom.CheckedValue = "false";
         AV82MiniAppClientURL_isCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV82MiniAppClientURL_isCustom));
         AssignAttri("", false, "AV82MiniAppClientURL_isCustom", AV82MiniAppClientURL_isCustom);
         cmbavMiniappuserauthenticationtypename.Name = "vMINIAPPUSERAUTHENTICATIONTYPENAME";
         cmbavMiniappuserauthenticationtypename.WebTags = "";
         cmbavMiniappuserauthenticationtypename.addItem("", context.GetMessage( "(Select)", ""), 0);
         if ( cmbavMiniappuserauthenticationtypename.ItemCount > 0 )
         {
            AV88MiniAppUserAuthenticationTypeName = cmbavMiniappuserauthenticationtypename.getValidValue(AV88MiniAppUserAuthenticationTypeName);
            AssignAttri("", false, "AV88MiniAppUserAuthenticationTypeName", AV88MiniAppUserAuthenticationTypeName);
         }
         chkavMiniappserverurl_iscustom.Name = "vMINIAPPSERVERURL_ISCUSTOM";
         chkavMiniappserverurl_iscustom.WebTags = "";
         chkavMiniappserverurl_iscustom.Caption = context.GetMessage( "WWP_GAM_CustomSuperAppserverURL", "");
         AssignProp("", false, chkavMiniappserverurl_iscustom_Internalname, "TitleCaption", chkavMiniappserverurl_iscustom.Caption, true);
         chkavMiniappserverurl_iscustom.CheckedValue = "false";
         AV87MiniAppServerURL_isCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV87MiniAppServerURL_isCustom));
         AssignAttri("", false, "AV87MiniAppServerURL_isCustom", AV87MiniAppServerURL_isCustom);
         chkavApikeyenable.Name = "vAPIKEYENABLE";
         chkavApikeyenable.WebTags = "";
         chkavApikeyenable.Caption = context.GetMessage( "WWP_GAM_EnableworkwithAPIkeys", "");
         AssignProp("", false, chkavApikeyenable_Internalname, "TitleCaption", chkavApikeyenable.Caption, true);
         chkavApikeyenable.CheckedValue = "false";
         AV11APIKeyEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV11APIKeyEnable));
         AssignAttri("", false, "AV11APIKeyEnable", AV11APIKeyEnable);
         cmbavApikeyallowonlyauthenticationtypename.Name = "vAPIKEYALLOWONLYAUTHENTICATIONTYPENAME";
         cmbavApikeyallowonlyauthenticationtypename.WebTags = "";
         cmbavApikeyallowonlyauthenticationtypename.addItem("", context.GetMessage( "(All)", ""), 0);
         if ( cmbavApikeyallowonlyauthenticationtypename.ItemCount > 0 )
         {
            AV9APIKeyAllowOnlyAuthenticationTypeName = cmbavApikeyallowonlyauthenticationtypename.getValidValue(AV9APIKeyAllowOnlyAuthenticationTypeName);
            AssignAttri("", false, "AV9APIKeyAllowOnlyAuthenticationTypeName", AV9APIKeyAllowOnlyAuthenticationTypeName);
         }
         chkavApikeyallowscopecustomization.Name = "vAPIKEYALLOWSCOPECUSTOMIZATION";
         chkavApikeyallowscopecustomization.WebTags = "";
         chkavApikeyallowscopecustomization.Caption = context.GetMessage( "WWP_GAM_APIKeyAllowScopeCustomization", "");
         AssignProp("", false, chkavApikeyallowscopecustomization_Internalname, "TitleCaption", chkavApikeyallowscopecustomization.Caption, true);
         chkavApikeyallowscopecustomization.CheckedValue = "false";
         AV10APIKeyAllowScopeCustomization = StringUtil.StrToBool( StringUtil.BoolToStr( AV10APIKeyAllowScopeCustomization));
         AssignAttri("", false, "AV10APIKeyAllowScopeCustomization", AV10APIKeyAllowScopeCustomization);
         chkavEnvironmentsecureprotocol.Name = "vENVIRONMENTSECUREPROTOCOL";
         chkavEnvironmentsecureprotocol.WebTags = "";
         chkavEnvironmentsecureprotocol.Caption = " ";
         AssignProp("", false, chkavEnvironmentsecureprotocol_Internalname, "TitleCaption", chkavEnvironmentsecureprotocol.Caption, true);
         chkavEnvironmentsecureprotocol.CheckedValue = "false";
         AV56EnvironmentSecureProtocol = StringUtil.StrToBool( StringUtil.BoolToStr( AV56EnvironmentSecureProtocol));
         AssignAttri("", false, "AV56EnvironmentSecureProtocol", AV56EnvironmentSecureProtocol);
         GXCCtl = "vONLINE_" + sGXsfl_563_idx;
         chkavOnline.Name = GXCCtl;
         chkavOnline.WebTags = "";
         chkavOnline.Caption = "";
         AssignProp("", false, chkavOnline_Internalname, "TitleCaption", chkavOnline.Caption, !bGXsfl_563_Refreshing);
         chkavOnline.CheckedValue = "false";
         AV90Online = StringUtil.StrToBool( StringUtil.BoolToStr( AV90Online));
         AssignAttri("", false, chkavOnline_Internalname, AV90Online);
         chkavAutoregisteranomymoususer.Name = "vAUTOREGISTERANOMYMOUSUSER";
         chkavAutoregisteranomymoususer.WebTags = "";
         chkavAutoregisteranomymoususer.Caption = "";
         AssignProp("", false, chkavAutoregisteranomymoususer_Internalname, "TitleCaption", chkavAutoregisteranomymoususer.Caption, true);
         chkavAutoregisteranomymoususer.CheckedValue = "false";
         AV14AutoRegisterAnomymousUser = StringUtil.StrToBool( StringUtil.BoolToStr( AV14AutoRegisterAnomymousUser));
         AssignAttri("", false, "AV14AutoRegisterAnomymousUser", AV14AutoRegisterAnomymousUser);
         /* End function init_web_controls */
      }

      protected void StartGridControl563( )
      {
         if ( GridlanguagesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridlanguagesContainer"+"DivS\" data-gxgridid=\"563\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridlanguages_Internalname, subGridlanguages_Internalname, "", "GridWithPaginationBar GridNoBorder WorkWith", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridlanguages_Backcolorstyle == 0 )
            {
               subGridlanguages_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridlanguages_Class) > 0 )
               {
                  subGridlanguages_Linesclass = subGridlanguages_Class+"Title";
               }
            }
            else
            {
               subGridlanguages_Titlebackstyle = 1;
               if ( subGridlanguages_Backcolorstyle == 1 )
               {
                  subGridlanguages_Titlebackcolor = subGridlanguages_Allbackcolor;
                  if ( StringUtil.Len( subGridlanguages_Class) > 0 )
                  {
                     subGridlanguages_Linesclass = subGridlanguages_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridlanguages_Class) > 0 )
                  {
                     subGridlanguages_Linesclass = subGridlanguages_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"AttributeCheckBox"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Online", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Languages", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridlanguagesContainer.AddObjectProperty("GridName", "Gridlanguages");
         }
         else
         {
            GridlanguagesContainer.AddObjectProperty("GridName", "Gridlanguages");
            GridlanguagesContainer.AddObjectProperty("Header", subGridlanguages_Header);
            GridlanguagesContainer.AddObjectProperty("Class", "GridWithPaginationBar GridNoBorder WorkWith");
            GridlanguagesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Backcolorstyle), 1, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("CmpContext", "");
            GridlanguagesContainer.AddObjectProperty("InMasterPage", "false");
            GridlanguagesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridlanguagesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.BoolToStr( AV90Online)));
            GridlanguagesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkavOnline.Enabled), 5, 0, ".", "")));
            GridlanguagesContainer.AddColumnProperties(GridlanguagesColumn);
            GridlanguagesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridlanguagesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV75Language));
            GridlanguagesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavLanguage_Enabled), 5, 0, ".", "")));
            GridlanguagesContainer.AddColumnProperties(GridlanguagesColumn);
            GridlanguagesContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Selectedindex), 4, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Allowselection), 1, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Selectioncolor), 9, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Allowhovering), 1, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Hoveringcolor), 9, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Allowcollapsing), 1, 0, ".", "")));
            GridlanguagesContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridlanguages_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblGeneral_title_Internalname = "GENERAL_TITLE";
         edtavId_Internalname = "vID";
         edtavGuid_Internalname = "vGUID";
         edtavName_Internalname = "vNAME";
         edtavDsc_Internalname = "vDSC";
         edtavVersion_Internalname = "vVERSION";
         edtavCompany_Internalname = "vCOMPANY";
         edtavCopyright_Internalname = "vCOPYRIGHT";
         chkavReturnmenuoptionswithoutpermission_Internalname = "vRETURNMENUOPTIONSWITHOUTPERMISSION";
         cmbavMainmenu_Internalname = "vMAINMENU";
         chkavUseabsoluteurlbyenvironment_Internalname = "vUSEABSOLUTEURLBYENVIRONMENT";
         edtavHomeobject_Internalname = "vHOMEOBJECT";
         edtavAccountactivationobject_Internalname = "vACCOUNTACTIVATIONOBJECT";
         lblTextblocklogoutobject_Internalname = "TEXTBLOCKLOGOUTOBJECT";
         edtavLogoutobject_Internalname = "vLOGOUTOBJECT";
         bttBtnrevokeallow_Internalname = "BTNREVOKEALLOW";
         tblTablemergedlogoutobject_Internalname = "TABLEMERGEDLOGOUTOBJECT";
         divTablesplittedlogoutobject_Internalname = "TABLESPLITTEDLOGOUTOBJECT";
         divUnnamedtable9_Internalname = "UNNAMEDTABLE9";
         lblRemoteauthentication_title_Internalname = "REMOTEAUTHENTICATION_TITLE";
         cmbavClientaccessstatus_Internalname = "vCLIENTACCESSSTATUS";
         edtavClientrevoked_Internalname = "vCLIENTREVOKED";
         edtavClientid_Internalname = "vCLIENTID";
         lblTextblockclientsecret_Internalname = "TEXTBLOCKCLIENTSECRET";
         edtavClientsecret_Internalname = "vCLIENTSECRET";
         bttBtnchangeclientsecret_Internalname = "BTNCHANGECLIENTSECRET";
         tblTablemergedclientsecret_Internalname = "TABLEMERGEDCLIENTSECRET";
         divTablesplittedclientsecret_Internalname = "TABLESPLITTEDCLIENTSECRET";
         chkavClientauthrequestmustincludeuserscopes_Internalname = "vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES";
         chkavClientdonotshareuserids_Internalname = "vCLIENTDONOTSHAREUSERIDS";
         chkavClientallowremoteauth_Internalname = "vCLIENTALLOWREMOTEAUTH";
         chkavClientallowgetuserdata_Internalname = "vCLIENTALLOWGETUSERDATA";
         chkavClientallowgetuseradddata_Internalname = "vCLIENTALLOWGETUSERADDDATA";
         chkavClientallowgetuserroles_Internalname = "vCLIENTALLOWGETUSERROLES";
         chkavClientallowgetsessioniniprop_Internalname = "vCLIENTALLOWGETSESSIONINIPROP";
         chkavClientallowgetsessionappdata_Internalname = "vCLIENTALLOWGETSESSIONAPPDATA";
         edtavClientallowadditionalscope_Internalname = "vCLIENTALLOWADDITIONALSCOPE";
         edtavClientimageurl_Internalname = "vCLIENTIMAGEURL";
         edtavClientlocalloginurl_Internalname = "vCLIENTLOCALLOGINURL";
         edtavClientcallbackurl_Internalname = "vCLIENTCALLBACKURL";
         chkavClientcallbackurliscustom_Internalname = "vCLIENTCALLBACKURLISCUSTOM";
         edtavClientcallbackurlstatename_Internalname = "vCLIENTCALLBACKURLSTATENAME";
         divTblwebauth_Internalname = "TBLWEBAUTH";
         divUnnamedtable6_Internalname = "UNNAMEDTABLE6";
         Dvpanel_unnamedtable6_Internalname = "DVPANEL_UNNAMEDTABLE6";
         chkavClientallowremoterestauth_Internalname = "vCLIENTALLOWREMOTERESTAUTH";
         chkavClientallowgetuserdatarest_Internalname = "vCLIENTALLOWGETUSERDATAREST";
         chkavClientallowgetuseradddatarest_Internalname = "vCLIENTALLOWGETUSERADDDATAREST";
         chkavClientallowgetuserrolesrest_Internalname = "vCLIENTALLOWGETUSERROLESREST";
         chkavClientallowgetsessioniniproprest_Internalname = "vCLIENTALLOWGETSESSIONINIPROPREST";
         chkavClientallowgetsessionappdatarest_Internalname = "vCLIENTALLOWGETSESSIONAPPDATAREST";
         edtavClientallowadditionalscoperest_Internalname = "vCLIENTALLOWADDITIONALSCOPEREST";
         divTblrestauth_Internalname = "TBLRESTAUTH";
         divUnnamedtable7_Internalname = "UNNAMEDTABLE7";
         Dvpanel_unnamedtable7_Internalname = "DVPANEL_UNNAMEDTABLE7";
         chkavClientaccessuniquebyuser_Internalname = "vCLIENTACCESSUNIQUEBYUSER";
         lblTextblockclientencryptionkey_Internalname = "TEXTBLOCKCLIENTENCRYPTIONKEY";
         edtavClientencryptionkey_Internalname = "vCLIENTENCRYPTIONKEY";
         bttBtngeneratekeygamremote_Internalname = "BTNGENERATEKEYGAMREMOTE";
         tblTablemergedclientencryptionkey_Internalname = "TABLEMERGEDCLIENTENCRYPTIONKEY";
         divTablesplittedclientencryptionkey_Internalname = "TABLESPLITTEDCLIENTENCRYPTIONKEY";
         edtavClientrepositoryguid_Internalname = "vCLIENTREPOSITORYGUID";
         divUnnamedtable8_Internalname = "UNNAMEDTABLE8";
         Dvpanel_unnamedtable8_Internalname = "DVPANEL_UNNAMEDTABLE8";
         divTblgeneralauth_Internalname = "TBLGENERALAUTH";
         divUnnamedtable5_Internalname = "UNNAMEDTABLE5";
         lblAuthorization_title_Internalname = "AUTHORIZATION_TITLE";
         chkavAccessrequirespermission_Internalname = "vACCESSREQUIRESPERMISSION";
         chkavIsauthorizationdelegated_Internalname = "vISAUTHORIZATIONDELEGATED";
         cmbavDelegateauthorizationversion_Internalname = "vDELEGATEAUTHORIZATIONVERSION";
         edtavDelegateauthorizationfilename_Internalname = "vDELEGATEAUTHORIZATIONFILENAME";
         edtavDelegateauthorizationpackage_Internalname = "vDELEGATEAUTHORIZATIONPACKAGE";
         edtavDelegateauthorizationclassname_Internalname = "vDELEGATEAUTHORIZATIONCLASSNAME";
         edtavDelegateauthorizationmethod_Internalname = "vDELEGATEAUTHORIZATIONMETHOD";
         divTbldelegateauthorizationprop_Internalname = "TBLDELEGATEAUTHORIZATIONPROP";
         divTbldelegateauthorization_Internalname = "TBLDELEGATEAUTHORIZATION";
         divUnnamedtable4_Internalname = "UNNAMEDTABLE4";
         lblSsorest_title_Internalname = "SSOREST_TITLE";
         chkavSsorestenable_Internalname = "vSSORESTENABLE";
         cmbavSsorestmode_Internalname = "vSSORESTMODE";
         edtavSsorestuserauthtypename_Internalname = "vSSORESTUSERAUTHTYPENAME";
         edtavSsorestserverurl_Internalname = "vSSORESTSERVERURL";
         chkavSsorestserverurl_iscustom_Internalname = "vSSORESTSERVERURL_ISCUSTOM";
         edtavSsorestserverurl_slo_Internalname = "vSSORESTSERVERURL_SLO";
         edtavSsorestserverrepositoryguid_Internalname = "vSSORESTSERVERREPOSITORYGUID";
         edtavSsorestserverkey_Internalname = "vSSORESTSERVERKEY";
         divTblssorestmodeclient_Internalname = "TBLSSORESTMODECLIENT";
         divTablessorest_Internalname = "TABLESSOREST";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         lblSts_title_Internalname = "STS_TITLE";
         chkavStsprotocolenable_Internalname = "vSTSPROTOCOLENABLE";
         cmbavStsmode_Internalname = "vSTSMODE";
         edtavStsauthorizationusername_Internalname = "vSTSAUTHORIZATIONUSERNAME";
         divTablestsserverchecktoken_Internalname = "TABLESTSSERVERCHECKTOKEN";
         edtavStsserverclientpassword_Internalname = "vSTSSERVERCLIENTPASSWORD";
         divStsserverclientpassword_cell_Internalname = "STSSERVERCLIENTPASSWORD_CELL";
         divTablestsclientgettoken_Internalname = "TABLESTSCLIENTGETTOKEN";
         edtavStsserverurl_Internalname = "vSTSSERVERURL";
         edtavStsserverrepositoryguid_Internalname = "vSTSSERVERREPOSITORYGUID";
         divTablestsclient_Internalname = "TABLESTSCLIENT";
         divTablests_Internalname = "TABLESTS";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         lblMiniapp_title_Internalname = "MINIAPP_TITLE";
         chkavMiniappenable_Internalname = "vMINIAPPENABLE";
         cmbavMiniappmode_Internalname = "vMINIAPPMODE";
         edtavMiniappclienturl_Internalname = "vMINIAPPCLIENTURL";
         chkavMiniappclienturl_iscustom_Internalname = "vMINIAPPCLIENTURL_ISCUSTOM";
         edtavMiniappclientrepositoryguid_Internalname = "vMINIAPPCLIENTREPOSITORYGUID";
         divTblminiappserver_Internalname = "TBLMINIAPPSERVER";
         cmbavMiniappuserauthenticationtypename_Internalname = "vMINIAPPUSERAUTHENTICATIONTYPENAME";
         edtavMiniappserverurl_Internalname = "vMINIAPPSERVERURL";
         chkavMiniappserverurl_iscustom_Internalname = "vMINIAPPSERVERURL_ISCUSTOM";
         edtavMiniappserverrepositoryguid_Internalname = "vMINIAPPSERVERREPOSITORYGUID";
         divTblminiappclient_Internalname = "TBLMINIAPPCLIENT";
         divTblminiapp_Internalname = "TBLMINIAPP";
         divMiniapptable1_Internalname = "MINIAPPTABLE1";
         lblApikey_title_Internalname = "APIKEY_TITLE";
         chkavApikeyenable_Internalname = "vAPIKEYENABLE";
         edtavApikeytimeout_Internalname = "vAPIKEYTIMEOUT";
         cmbavApikeyallowonlyauthenticationtypename_Internalname = "vAPIKEYALLOWONLYAUTHENTICATIONTYPENAME";
         chkavApikeyallowscopecustomization_Internalname = "vAPIKEYALLOWSCOPECUSTOMIZATION";
         divTblapikey_Internalname = "TBLAPIKEY";
         divApikeytable1_Internalname = "APIKEYTABLE1";
         lblEnvironmentsettings_title_Internalname = "ENVIRONMENTSETTINGS_TITLE";
         edtavEnvironmentname_Internalname = "vENVIRONMENTNAME";
         chkavEnvironmentsecureprotocol_Internalname = "vENVIRONMENTSECUREPROTOCOL";
         edtavEnvironmenthost_Internalname = "vENVIRONMENTHOST";
         edtavEnvironmentport_Internalname = "vENVIRONMENTPORT";
         edtavEnvironmentvirtualdirectory_Internalname = "vENVIRONMENTVIRTUALDIRECTORY";
         edtavEnvironmentprogrampackage_Internalname = "vENVIRONMENTPROGRAMPACKAGE";
         edtavEnvironmentprogramextension_Internalname = "vENVIRONMENTPROGRAMEXTENSION";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         lblLanguages_title_Internalname = "LANGUAGES_TITLE";
         chkavOnline_Internalname = "vONLINE";
         edtavLanguage_Internalname = "vLANGUAGE";
         Gridlanguagespaginationbar_Internalname = "GRIDLANGUAGESPAGINATIONBAR";
         divGridlanguagestablewithpaginationbar_Internalname = "GRIDLANGUAGESTABLEWITHPAGINATIONBAR";
         divLanguagestable1_Internalname = "LANGUAGESTABLE1";
         Gxuitabspanel_tabs_Internalname = "GXUITABSPANEL_TABS";
         divTablecontent_Internalname = "TABLECONTENT";
         bttBtnenter_Internalname = "BTNENTER";
         bttBtncancel_Internalname = "BTNCANCEL";
         divTablemain_Internalname = "TABLEMAIN";
         edtavGridlanguagescurrentpage_Internalname = "vGRIDLANGUAGESCURRENTPAGE";
         chkavAutoregisteranomymoususer_Internalname = "vAUTOREGISTERANOMYMOUSUSER";
         edtavStsauthorizationuserguid_Internalname = "vSTSAUTHORIZATIONUSERGUID";
         Gridlanguages_empowerer_Internalname = "GRIDLANGUAGES_EMPOWERER";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subGridlanguages_Internalname = "GRIDLANGUAGES";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridlanguages_Allowcollapsing = 0;
         subGridlanguages_Allowselection = 0;
         subGridlanguages_Header = "";
         chkavAutoregisteranomymoususer.Caption = "";
         chkavEnvironmentsecureprotocol.Caption = " ";
         chkavApikeyallowscopecustomization.Caption = context.GetMessage( "WWP_GAM_APIKeyAllowScopeCustomization", "");
         chkavApikeyenable.Caption = context.GetMessage( "WWP_GAM_EnableworkwithAPIkeys", "");
         chkavMiniappserverurl_iscustom.Caption = context.GetMessage( "WWP_GAM_CustomSuperAppserverURL", "");
         chkavMiniappclienturl_iscustom.Caption = context.GetMessage( "WWP_GAM_CustomMiniAppclientURL", "");
         chkavMiniappenable.Caption = context.GetMessage( "WWP_GAM_EnableworkasMiniApp", "");
         chkavStsprotocolenable.Caption = " ";
         chkavSsorestserverurl_iscustom.Caption = context.GetMessage( "WWP_GAM_CustomserverURLSSO", "");
         chkavSsorestenable.Caption = " ";
         chkavIsauthorizationdelegated.Caption = context.GetMessage( "WWP_GAM_Delegateauthorizationcheckingtoanexternalprogram", "");
         chkavAccessrequirespermission.Caption = context.GetMessage( "WWP_GAM_EnableAuthorization", "");
         chkavClientaccessuniquebyuser.Caption = " ";
         chkavClientallowgetsessionappdatarest.Caption = " ";
         chkavClientallowgetsessioniniproprest.Caption = " ";
         chkavClientallowgetuserrolesrest.Caption = " ";
         chkavClientallowgetuseradddatarest.Caption = " ";
         chkavClientallowgetuserdatarest.Caption = " ";
         chkavClientallowremoterestauth.Caption = " ";
         chkavClientcallbackurliscustom.Caption = " ";
         chkavClientallowgetsessionappdata.Caption = " ";
         chkavClientallowgetsessioniniprop.Caption = " ";
         chkavClientallowgetuserroles.Caption = " ";
         chkavClientallowgetuseradddata.Caption = " ";
         chkavClientallowgetuserdata.Caption = " ";
         chkavClientallowremoteauth.Caption = " ";
         chkavClientdonotshareuserids.Caption = " ";
         chkavClientauthrequestmustincludeuserscopes.Caption = " ";
         chkavUseabsoluteurlbyenvironment.Caption = " ";
         chkavReturnmenuoptionswithoutpermission.Caption = " ";
         edtavLanguage_Jsonclick = "";
         edtavLanguage_Enabled = 1;
         chkavOnline.Caption = "";
         subGridlanguages_Class = "GridWithPaginationBar GridNoBorder WorkWith";
         subGridlanguages_Backcolorstyle = 0;
         edtavLogoutobject_Jsonclick = "";
         bttBtnchangeclientsecret_Visible = 1;
         edtavClientsecret_Jsonclick = "";
         bttBtngeneratekeygamremote_Visible = 1;
         edtavClientencryptionkey_Jsonclick = "";
         bttBtnrevokeallow_Caption = context.GetMessage( "WWP_GAM_Revoke", "");
         edtavClientencryptionkey_Enabled = 1;
         edtavLogoutobject_Enabled = 1;
         edtavClientsecret_Enabled = 1;
         edtavStsauthorizationuserguid_Jsonclick = "";
         edtavStsauthorizationuserguid_Visible = 1;
         chkavAutoregisteranomymoususer.Visible = 1;
         edtavGridlanguagescurrentpage_Jsonclick = "";
         edtavGridlanguagescurrentpage_Visible = 1;
         bttBtnenter_Caption = context.GetMessage( "GX_BtnEnter", "");
         bttBtnenter_Visible = 1;
         edtavEnvironmentprogramextension_Jsonclick = "";
         edtavEnvironmentprogramextension_Enabled = 1;
         edtavEnvironmentprogrampackage_Jsonclick = "";
         edtavEnvironmentprogrampackage_Enabled = 1;
         edtavEnvironmentvirtualdirectory_Jsonclick = "";
         edtavEnvironmentvirtualdirectory_Enabled = 1;
         edtavEnvironmentport_Jsonclick = "";
         edtavEnvironmentport_Enabled = 1;
         edtavEnvironmenthost_Jsonclick = "";
         edtavEnvironmenthost_Enabled = 1;
         chkavEnvironmentsecureprotocol.Enabled = 1;
         edtavEnvironmentname_Jsonclick = "";
         edtavEnvironmentname_Enabled = 1;
         chkavApikeyallowscopecustomization.Enabled = 1;
         cmbavApikeyallowonlyauthenticationtypename_Jsonclick = "";
         cmbavApikeyallowonlyauthenticationtypename.Enabled = 1;
         edtavApikeytimeout_Jsonclick = "";
         edtavApikeytimeout_Enabled = 1;
         divTblapikey_Visible = 1;
         chkavApikeyenable.Enabled = 1;
         edtavMiniappserverrepositoryguid_Jsonclick = "";
         edtavMiniappserverrepositoryguid_Enabled = 1;
         chkavMiniappserverurl_iscustom.Enabled = 1;
         edtavMiniappserverurl_Jsonclick = "";
         edtavMiniappserverurl_Enabled = 1;
         cmbavMiniappuserauthenticationtypename_Jsonclick = "";
         cmbavMiniappuserauthenticationtypename.Enabled = 1;
         divTblminiappclient_Visible = 1;
         edtavMiniappclientrepositoryguid_Jsonclick = "";
         edtavMiniappclientrepositoryguid_Enabled = 1;
         chkavMiniappclienturl_iscustom.Enabled = 1;
         edtavMiniappclienturl_Jsonclick = "";
         edtavMiniappclienturl_Enabled = 1;
         divTblminiappserver_Visible = 1;
         cmbavMiniappmode_Jsonclick = "";
         cmbavMiniappmode.Enabled = 1;
         divTblminiapp_Visible = 1;
         chkavMiniappenable.Enabled = 1;
         edtavStsserverrepositoryguid_Jsonclick = "";
         edtavStsserverrepositoryguid_Enabled = 1;
         edtavStsserverurl_Jsonclick = "";
         edtavStsserverurl_Enabled = 1;
         divTablestsclient_Visible = 1;
         edtavStsserverclientpassword_Jsonclick = "";
         edtavStsserverclientpassword_Enabled = 1;
         edtavStsserverclientpassword_Visible = 1;
         divStsserverclientpassword_cell_Class = "col-xs-12";
         divTablestsclientgettoken_Visible = 1;
         edtavStsauthorizationusername_Jsonclick = "";
         edtavStsauthorizationusername_Enabled = 1;
         divTablestsserverchecktoken_Visible = 1;
         cmbavStsmode_Jsonclick = "";
         cmbavStsmode.Enabled = 1;
         divTablests_Visible = 1;
         chkavStsprotocolenable.Enabled = 1;
         edtavSsorestserverkey_Jsonclick = "";
         edtavSsorestserverkey_Enabled = 1;
         edtavSsorestserverrepositoryguid_Jsonclick = "";
         edtavSsorestserverrepositoryguid_Enabled = 1;
         edtavSsorestserverurl_slo_Jsonclick = "";
         edtavSsorestserverurl_slo_Enabled = 1;
         chkavSsorestserverurl_iscustom.Enabled = 1;
         edtavSsorestserverurl_Jsonclick = "";
         edtavSsorestserverurl_Enabled = 1;
         edtavSsorestuserauthtypename_Jsonclick = "";
         edtavSsorestuserauthtypename_Enabled = 1;
         divTblssorestmodeclient_Visible = 1;
         cmbavSsorestmode_Jsonclick = "";
         cmbavSsorestmode.Enabled = 1;
         divTablessorest_Visible = 1;
         chkavSsorestenable.Enabled = 1;
         edtavDelegateauthorizationmethod_Jsonclick = "";
         edtavDelegateauthorizationmethod_Enabled = 1;
         edtavDelegateauthorizationclassname_Jsonclick = "";
         edtavDelegateauthorizationclassname_Enabled = 1;
         edtavDelegateauthorizationpackage_Jsonclick = "";
         edtavDelegateauthorizationpackage_Enabled = 1;
         edtavDelegateauthorizationfilename_Jsonclick = "";
         edtavDelegateauthorizationfilename_Enabled = 1;
         cmbavDelegateauthorizationversion_Jsonclick = "";
         cmbavDelegateauthorizationversion.Enabled = 1;
         divTbldelegateauthorizationprop_Visible = 1;
         chkavIsauthorizationdelegated.Enabled = 1;
         divTbldelegateauthorization_Visible = 1;
         chkavAccessrequirespermission.Enabled = 1;
         edtavClientrepositoryguid_Jsonclick = "";
         edtavClientrepositoryguid_Enabled = 1;
         chkavClientaccessuniquebyuser.Enabled = 1;
         divTblgeneralauth_Visible = 1;
         edtavClientallowadditionalscoperest_Jsonclick = "";
         edtavClientallowadditionalscoperest_Enabled = 1;
         chkavClientallowgetsessionappdatarest.Enabled = 1;
         chkavClientallowgetsessioniniproprest.Enabled = 1;
         chkavClientallowgetuserrolesrest.Enabled = 1;
         chkavClientallowgetuseradddatarest.Enabled = 1;
         chkavClientallowgetuserdatarest.Enabled = 1;
         divTblrestauth_Visible = 1;
         chkavClientallowremoterestauth.Enabled = 1;
         edtavClientcallbackurlstatename_Jsonclick = "";
         edtavClientcallbackurlstatename_Enabled = 1;
         chkavClientcallbackurliscustom.Enabled = 1;
         edtavClientcallbackurl_Jsonclick = "";
         edtavClientcallbackurl_Enabled = 1;
         edtavClientlocalloginurl_Jsonclick = "";
         edtavClientlocalloginurl_Enabled = 1;
         edtavClientimageurl_Jsonclick = "";
         edtavClientimageurl_Enabled = 1;
         edtavClientallowadditionalscope_Jsonclick = "";
         edtavClientallowadditionalscope_Enabled = 1;
         chkavClientallowgetsessionappdata.Enabled = 1;
         chkavClientallowgetsessioniniprop.Enabled = 1;
         chkavClientallowgetuserroles.Enabled = 1;
         chkavClientallowgetuseradddata.Enabled = 1;
         chkavClientallowgetuserdata.Enabled = 1;
         divTblwebauth_Visible = 1;
         chkavClientallowremoteauth.Enabled = 1;
         chkavClientdonotshareuserids.Enabled = 1;
         chkavClientauthrequestmustincludeuserscopes.Enabled = 1;
         edtavClientid_Jsonclick = "";
         edtavClientid_Enabled = 1;
         edtavClientrevoked_Jsonclick = "";
         edtavClientrevoked_Enabled = 1;
         edtavClientrevoked_Visible = 1;
         cmbavClientaccessstatus_Jsonclick = "";
         cmbavClientaccessstatus.Enabled = 1;
         edtavAccountactivationobject_Jsonclick = "";
         edtavAccountactivationobject_Enabled = 1;
         edtavHomeobject_Jsonclick = "";
         edtavHomeobject_Enabled = 1;
         chkavUseabsoluteurlbyenvironment.Enabled = 1;
         cmbavMainmenu_Jsonclick = "";
         cmbavMainmenu.Enabled = 1;
         chkavReturnmenuoptionswithoutpermission.Enabled = 1;
         edtavCopyright_Jsonclick = "";
         edtavCopyright_Enabled = 1;
         edtavCompany_Jsonclick = "";
         edtavCompany_Enabled = 1;
         edtavVersion_Jsonclick = "";
         edtavVersion_Enabled = 1;
         edtavDsc_Jsonclick = "";
         edtavDsc_Enabled = 1;
         edtavName_Jsonclick = "";
         edtavName_Enabled = 1;
         edtavGuid_Jsonclick = "";
         edtavGuid_Enabled = 1;
         edtavId_Jsonclick = "";
         edtavId_Enabled = 0;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         Gxuitabspanel_tabs_Historymanagement = Convert.ToBoolean( 0);
         Gxuitabspanel_tabs_Class = "Tab";
         Gxuitabspanel_tabs_Pagecount = 9;
         Gridlanguagespaginationbar_Rowsperpagecaption = "WWP_PagingRowsPerPage";
         Gridlanguagespaginationbar_Emptygridcaption = "WWP_PagingEmptyGridCaption";
         Gridlanguagespaginationbar_Caption = context.GetMessage( "WWP_PagingCaption", "");
         Gridlanguagespaginationbar_Next = "WWP_PagingNextCaption";
         Gridlanguagespaginationbar_Previous = "WWP_PagingPreviousCaption";
         Gridlanguagespaginationbar_Rowsperpageoptions = "5:WWP_Rows5,10:WWP_Rows10,20:WWP_Rows20,50:WWP_Rows50";
         Gridlanguagespaginationbar_Rowsperpageselectedvalue = 10;
         Gridlanguagespaginationbar_Rowsperpageselector = Convert.ToBoolean( -1);
         Gridlanguagespaginationbar_Emptygridclass = "PaginationBarEmptyGrid";
         Gridlanguagespaginationbar_Pagingcaptionposition = "Left";
         Gridlanguagespaginationbar_Pagingbuttonsposition = "Right";
         Gridlanguagespaginationbar_Pagestoshow = 5;
         Gridlanguagespaginationbar_Showlast = Convert.ToBoolean( 0);
         Gridlanguagespaginationbar_Shownext = Convert.ToBoolean( -1);
         Gridlanguagespaginationbar_Showprevious = Convert.ToBoolean( -1);
         Gridlanguagespaginationbar_Showfirst = Convert.ToBoolean( 0);
         Gridlanguagespaginationbar_Class = "PaginationBar";
         Dvpanel_unnamedtable8_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Iconposition = "Right";
         Dvpanel_unnamedtable8_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable8_Title = context.GetMessage( "WWP_GAM_General", "");
         Dvpanel_unnamedtable8_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable8_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable8_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Width = "100%";
         Dvpanel_unnamedtable7_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Iconposition = "Right";
         Dvpanel_unnamedtable7_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable7_Title = context.GetMessage( "WWP_GAM_REST_GAMRemoteREST_SSOREST_MiniApp_APIkey", "");
         Dvpanel_unnamedtable7_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable7_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable7_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Width = "100%";
         Dvpanel_unnamedtable6_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable6_Iconposition = "Right";
         Dvpanel_unnamedtable6_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable6_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable6_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable6_Title = context.GetMessage( "WWP_GAM_WEB_GAMRemote_IDPusingSSO", "");
         Dvpanel_unnamedtable6_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable6_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable6_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable6_Width = "100%";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Application", "");
         chkavOnline.Enabled = 1;
         subGridlanguages_Rows = 0;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDLANGUAGES_nFirstRecordOnPage"},{"av":"GRIDLANGUAGES_nEOF"},{"av":"subGridlanguages_Rows","ctrl":"GRIDLANGUAGES","prop":"Rows"},{"av":"chkavOnline.Enabled","ctrl":"vONLINE","prop":"Enabled"},{"av":"AV92ReturnMenuOptionsWithoutPermission","fld":"vRETURNMENUOPTIONSWITHOUTPERMISSION"},{"av":"AV109UseAbsoluteUrlByEnvironment","fld":"vUSEABSOLUTEURLBYENVIRONMENT"},{"av":"AV31ClientAuthRequestMustIncludeUserScopes","fld":"vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES"},{"av":"AV35ClientDoNotShareUserIDs","fld":"vCLIENTDONOTSHAREUSERIDS"},{"av":"AV29ClientAllowRemoteAuth","fld":"vCLIENTALLOWREMOTEAUTH"},{"av":"AV25ClientAllowGetUserData","fld":"vCLIENTALLOWGETUSERDATA"},{"av":"AV23ClientAllowGetUserAddData","fld":"vCLIENTALLOWGETUSERADDDATA"},{"av":"AV27ClientAllowGetUserRoles","fld":"vCLIENTALLOWGETUSERROLES"},{"av":"AV21ClientAllowGetSessionIniProp","fld":"vCLIENTALLOWGETSESSIONINIPROP"},{"av":"AV19ClientAllowGetSessionAppData","fld":"vCLIENTALLOWGETSESSIONAPPDATA"},{"av":"AV33ClientCallbackURLisCustom","fld":"vCLIENTCALLBACKURLISCUSTOM"},{"av":"AV30ClientAllowRemoteRestAuth","fld":"vCLIENTALLOWREMOTERESTAUTH"},{"av":"AV26ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV24ClientAllowGetUserAddDataRest","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV28ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV22ClientAllowGetSessionIniPropRest","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV20ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV16ClientAccessUniqueByUser","fld":"vCLIENTACCESSUNIQUEBYUSER"},{"av":"AV7AccessRequiresPermission","fld":"vACCESSREQUIRESPERMISSION"},{"av":"AV72IsAuthorizationDelegated","fld":"vISAUTHORIZATIONDELEGATED"},{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"AV99SSORestServerURL_isCustom","fld":"vSSORESTSERVERURL_ISCUSTOM"},{"av":"AV105STSProtocolEnable","fld":"vSTSPROTOCOLENABLE"},{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"AV82MiniAppClientURL_isCustom","fld":"vMINIAPPCLIENTURL_ISCUSTOM"},{"av":"AV87MiniAppServerURL_isCustom","fld":"vMINIAPPSERVERURL_ISCUSTOM"},{"av":"AV11APIKeyEnable","fld":"vAPIKEYENABLE"},{"av":"AV10APIKeyAllowScopeCustomization","fld":"vAPIKEYALLOWSCOPECUSTOMIZATION"},{"av":"AV56EnvironmentSecureProtocol","fld":"vENVIRONMENTSECUREPROTOCOL"},{"av":"AV14AutoRegisterAnomymousUser","fld":"vAUTOREGISTERANOMYMOUSUSER"},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true}]}""");
         setEventMetadata("GRIDLANGUAGES.LOAD","""{"handler":"E27142","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true}]""");
         setEventMetadata("GRIDLANGUAGES.LOAD",""","oparms":[{"av":"AV90Online","fld":"vONLINE"},{"av":"AV75Language","fld":"vLANGUAGE","hsh":true}]}""");
         setEventMetadata("GRIDLANGUAGESPAGINATIONBAR.CHANGEPAGE","""{"handler":"E12142","iparms":[{"av":"GRIDLANGUAGES_nFirstRecordOnPage"},{"av":"GRIDLANGUAGES_nEOF"},{"av":"subGridlanguages_Rows","ctrl":"GRIDLANGUAGES","prop":"Rows"},{"av":"chkavOnline.Enabled","ctrl":"vONLINE","prop":"Enabled"},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV92ReturnMenuOptionsWithoutPermission","fld":"vRETURNMENUOPTIONSWITHOUTPERMISSION"},{"av":"AV109UseAbsoluteUrlByEnvironment","fld":"vUSEABSOLUTEURLBYENVIRONMENT"},{"av":"AV31ClientAuthRequestMustIncludeUserScopes","fld":"vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES"},{"av":"AV35ClientDoNotShareUserIDs","fld":"vCLIENTDONOTSHAREUSERIDS"},{"av":"AV29ClientAllowRemoteAuth","fld":"vCLIENTALLOWREMOTEAUTH"},{"av":"AV25ClientAllowGetUserData","fld":"vCLIENTALLOWGETUSERDATA"},{"av":"AV23ClientAllowGetUserAddData","fld":"vCLIENTALLOWGETUSERADDDATA"},{"av":"AV27ClientAllowGetUserRoles","fld":"vCLIENTALLOWGETUSERROLES"},{"av":"AV21ClientAllowGetSessionIniProp","fld":"vCLIENTALLOWGETSESSIONINIPROP"},{"av":"AV19ClientAllowGetSessionAppData","fld":"vCLIENTALLOWGETSESSIONAPPDATA"},{"av":"AV33ClientCallbackURLisCustom","fld":"vCLIENTCALLBACKURLISCUSTOM"},{"av":"AV30ClientAllowRemoteRestAuth","fld":"vCLIENTALLOWREMOTERESTAUTH"},{"av":"AV26ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV24ClientAllowGetUserAddDataRest","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV28ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV22ClientAllowGetSessionIniPropRest","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV20ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV16ClientAccessUniqueByUser","fld":"vCLIENTACCESSUNIQUEBYUSER"},{"av":"AV7AccessRequiresPermission","fld":"vACCESSREQUIRESPERMISSION"},{"av":"AV72IsAuthorizationDelegated","fld":"vISAUTHORIZATIONDELEGATED"},{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"AV99SSORestServerURL_isCustom","fld":"vSSORESTSERVERURL_ISCUSTOM"},{"av":"AV105STSProtocolEnable","fld":"vSTSPROTOCOLENABLE"},{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"AV82MiniAppClientURL_isCustom","fld":"vMINIAPPCLIENTURL_ISCUSTOM"},{"av":"AV87MiniAppServerURL_isCustom","fld":"vMINIAPPSERVERURL_ISCUSTOM"},{"av":"AV11APIKeyEnable","fld":"vAPIKEYENABLE"},{"av":"AV10APIKeyAllowScopeCustomization","fld":"vAPIKEYALLOWSCOPECUSTOMIZATION"},{"av":"AV56EnvironmentSecureProtocol","fld":"vENVIRONMENTSECUREPROTOCOL"},{"av":"AV14AutoRegisterAnomymousUser","fld":"vAUTOREGISTERANOMYMOUSUSER"},{"av":"Gridlanguagespaginationbar_Selectedpage","ctrl":"GRIDLANGUAGESPAGINATIONBAR","prop":"SelectedPage"},{"av":"AV66GridLanguagesCurrentPage","fld":"vGRIDLANGUAGESCURRENTPAGE","pic":"ZZZZZZZZZ9"}]""");
         setEventMetadata("GRIDLANGUAGESPAGINATIONBAR.CHANGEPAGE",""","oparms":[{"av":"AV66GridLanguagesCurrentPage","fld":"vGRIDLANGUAGESCURRENTPAGE","pic":"ZZZZZZZZZ9"}]}""");
         setEventMetadata("GRIDLANGUAGESPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E13142","iparms":[{"av":"GRIDLANGUAGES_nFirstRecordOnPage"},{"av":"GRIDLANGUAGES_nEOF"},{"av":"subGridlanguages_Rows","ctrl":"GRIDLANGUAGES","prop":"Rows"},{"av":"chkavOnline.Enabled","ctrl":"vONLINE","prop":"Enabled"},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV92ReturnMenuOptionsWithoutPermission","fld":"vRETURNMENUOPTIONSWITHOUTPERMISSION"},{"av":"AV109UseAbsoluteUrlByEnvironment","fld":"vUSEABSOLUTEURLBYENVIRONMENT"},{"av":"AV31ClientAuthRequestMustIncludeUserScopes","fld":"vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES"},{"av":"AV35ClientDoNotShareUserIDs","fld":"vCLIENTDONOTSHAREUSERIDS"},{"av":"AV29ClientAllowRemoteAuth","fld":"vCLIENTALLOWREMOTEAUTH"},{"av":"AV25ClientAllowGetUserData","fld":"vCLIENTALLOWGETUSERDATA"},{"av":"AV23ClientAllowGetUserAddData","fld":"vCLIENTALLOWGETUSERADDDATA"},{"av":"AV27ClientAllowGetUserRoles","fld":"vCLIENTALLOWGETUSERROLES"},{"av":"AV21ClientAllowGetSessionIniProp","fld":"vCLIENTALLOWGETSESSIONINIPROP"},{"av":"AV19ClientAllowGetSessionAppData","fld":"vCLIENTALLOWGETSESSIONAPPDATA"},{"av":"AV33ClientCallbackURLisCustom","fld":"vCLIENTCALLBACKURLISCUSTOM"},{"av":"AV30ClientAllowRemoteRestAuth","fld":"vCLIENTALLOWREMOTERESTAUTH"},{"av":"AV26ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV24ClientAllowGetUserAddDataRest","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV28ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV22ClientAllowGetSessionIniPropRest","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV20ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV16ClientAccessUniqueByUser","fld":"vCLIENTACCESSUNIQUEBYUSER"},{"av":"AV7AccessRequiresPermission","fld":"vACCESSREQUIRESPERMISSION"},{"av":"AV72IsAuthorizationDelegated","fld":"vISAUTHORIZATIONDELEGATED"},{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"AV99SSORestServerURL_isCustom","fld":"vSSORESTSERVERURL_ISCUSTOM"},{"av":"AV105STSProtocolEnable","fld":"vSTSPROTOCOLENABLE"},{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"AV82MiniAppClientURL_isCustom","fld":"vMINIAPPCLIENTURL_ISCUSTOM"},{"av":"AV87MiniAppServerURL_isCustom","fld":"vMINIAPPSERVERURL_ISCUSTOM"},{"av":"AV11APIKeyEnable","fld":"vAPIKEYENABLE"},{"av":"AV10APIKeyAllowScopeCustomization","fld":"vAPIKEYALLOWSCOPECUSTOMIZATION"},{"av":"AV56EnvironmentSecureProtocol","fld":"vENVIRONMENTSECUREPROTOCOL"},{"av":"AV14AutoRegisterAnomymousUser","fld":"vAUTOREGISTERANOMYMOUSUSER"},{"av":"Gridlanguagespaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDLANGUAGESPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDLANGUAGESPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGridlanguages_Rows","ctrl":"GRIDLANGUAGES","prop":"Rows"},{"av":"AV66GridLanguagesCurrentPage","fld":"vGRIDLANGUAGESCURRENTPAGE","pic":"ZZZZZZZZZ9"}]}""");
         setEventMetadata("'DOCHANGECLIENTSECRET'","""{"handler":"E14142","iparms":[{"av":"AV37ClientId","fld":"vCLIENTID"},{"av":"AV71Id","fld":"vID","pic":"ZZZZZZZZZZZ9"}]""");
         setEventMetadata("'DOCHANGECLIENTSECRET'",""","oparms":[{"av":"AV71Id","fld":"vID","pic":"ZZZZZZZZZZZ9"}]}""");
         setEventMetadata("'DOGENERATEKEYGAMREMOTE'","""{"handler":"E15142","iparms":[]""");
         setEventMetadata("'DOGENERATEKEYGAMREMOTE'",""","oparms":[{"av":"AV36ClientEncryptionKey","fld":"vCLIENTENCRYPTIONKEY"}]}""");
         setEventMetadata("'DOREVOKEALLOW'","""{"handler":"E16142","iparms":[{"av":"GRIDLANGUAGES_nFirstRecordOnPage"},{"av":"GRIDLANGUAGES_nEOF"},{"av":"subGridlanguages_Rows","ctrl":"GRIDLANGUAGES","prop":"Rows"},{"av":"chkavOnline.Enabled","ctrl":"vONLINE","prop":"Enabled"},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV92ReturnMenuOptionsWithoutPermission","fld":"vRETURNMENUOPTIONSWITHOUTPERMISSION"},{"av":"AV109UseAbsoluteUrlByEnvironment","fld":"vUSEABSOLUTEURLBYENVIRONMENT"},{"av":"AV31ClientAuthRequestMustIncludeUserScopes","fld":"vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES"},{"av":"AV35ClientDoNotShareUserIDs","fld":"vCLIENTDONOTSHAREUSERIDS"},{"av":"AV29ClientAllowRemoteAuth","fld":"vCLIENTALLOWREMOTEAUTH"},{"av":"AV25ClientAllowGetUserData","fld":"vCLIENTALLOWGETUSERDATA"},{"av":"AV23ClientAllowGetUserAddData","fld":"vCLIENTALLOWGETUSERADDDATA"},{"av":"AV27ClientAllowGetUserRoles","fld":"vCLIENTALLOWGETUSERROLES"},{"av":"AV21ClientAllowGetSessionIniProp","fld":"vCLIENTALLOWGETSESSIONINIPROP"},{"av":"AV19ClientAllowGetSessionAppData","fld":"vCLIENTALLOWGETSESSIONAPPDATA"},{"av":"AV33ClientCallbackURLisCustom","fld":"vCLIENTCALLBACKURLISCUSTOM"},{"av":"AV30ClientAllowRemoteRestAuth","fld":"vCLIENTALLOWREMOTERESTAUTH"},{"av":"AV26ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV24ClientAllowGetUserAddDataRest","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV28ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV22ClientAllowGetSessionIniPropRest","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV20ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV16ClientAccessUniqueByUser","fld":"vCLIENTACCESSUNIQUEBYUSER"},{"av":"AV7AccessRequiresPermission","fld":"vACCESSREQUIRESPERMISSION"},{"av":"AV72IsAuthorizationDelegated","fld":"vISAUTHORIZATIONDELEGATED"},{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"AV99SSORestServerURL_isCustom","fld":"vSSORESTSERVERURL_ISCUSTOM"},{"av":"AV105STSProtocolEnable","fld":"vSTSPROTOCOLENABLE"},{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"AV82MiniAppClientURL_isCustom","fld":"vMINIAPPCLIENTURL_ISCUSTOM"},{"av":"AV87MiniAppServerURL_isCustom","fld":"vMINIAPPSERVERURL_ISCUSTOM"},{"av":"AV11APIKeyEnable","fld":"vAPIKEYENABLE"},{"av":"AV10APIKeyAllowScopeCustomization","fld":"vAPIKEYALLOWSCOPECUSTOMIZATION"},{"av":"AV56EnvironmentSecureProtocol","fld":"vENVIRONMENTSECUREPROTOCOL"},{"av":"AV14AutoRegisterAnomymousUser","fld":"vAUTOREGISTERANOMYMOUSUSER"},{"av":"AV71Id","fld":"vID","pic":"ZZZZZZZZZZZ9"}]""");
         setEventMetadata("'DOREVOKEALLOW'",""","oparms":[{"ctrl":"BTNREVOKEALLOW","prop":"Caption"}]}""");
         setEventMetadata("ENTER","""{"handler":"E17142","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV71Id","fld":"vID","pic":"ZZZZZZZZZZZ9"},{"av":"AV89Name","fld":"vNAME"},{"av":"AV50Dsc","fld":"vDSC"},{"av":"AV111Version","fld":"vVERSION"},{"av":"AV44Copyright","fld":"vCOPYRIGHT"},{"av":"AV43Company","fld":"vCOMPANY"},{"av":"AV92ReturnMenuOptionsWithoutPermission","fld":"vRETURNMENUOPTIONSWITHOUTPERMISSION"},{"av":"cmbavMainmenu"},{"av":"AV77MainMenu","fld":"vMAINMENU","pic":"ZZZZZZZZZZZ9"},{"av":"AV109UseAbsoluteUrlByEnvironment","fld":"vUSEABSOLUTEURLBYENVIRONMENT"},{"av":"AV69HomeObject","fld":"vHOMEOBJECT"},{"av":"AV8AccountActivationObject","fld":"vACCOUNTACTIVATIONOBJECT"},{"av":"AV76LogoutObject","fld":"vLOGOUTOBJECT"},{"av":"AV37ClientId","fld":"vCLIENTID"},{"av":"AV42ClientSecret","fld":"vCLIENTSECRET"},{"av":"AV31ClientAuthRequestMustIncludeUserScopes","fld":"vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES"},{"av":"AV35ClientDoNotShareUserIDs","fld":"vCLIENTDONOTSHAREUSERIDS"},{"av":"AV16ClientAccessUniqueByUser","fld":"vCLIENTACCESSUNIQUEBYUSER"},{"av":"AV29ClientAllowRemoteAuth","fld":"vCLIENTALLOWREMOTEAUTH"},{"av":"AV25ClientAllowGetUserData","fld":"vCLIENTALLOWGETUSERDATA"},{"av":"AV23ClientAllowGetUserAddData","fld":"vCLIENTALLOWGETUSERADDDATA"},{"av":"AV27ClientAllowGetUserRoles","fld":"vCLIENTALLOWGETUSERROLES"},{"av":"AV21ClientAllowGetSessionIniProp","fld":"vCLIENTALLOWGETSESSIONINIPROP"},{"av":"AV19ClientAllowGetSessionAppData","fld":"vCLIENTALLOWGETSESSIONAPPDATA"},{"av":"AV17ClientAllowAdditionalScope","fld":"vCLIENTALLOWADDITIONALSCOPE"},{"av":"AV39ClientLocalLoginURL","fld":"vCLIENTLOCALLOGINURL"},{"av":"AV32ClientCallbackURL","fld":"vCLIENTCALLBACKURL"},{"av":"AV33ClientCallbackURLisCustom","fld":"vCLIENTCALLBACKURLISCUSTOM"},{"av":"AV34ClientCallbackURLStateName","fld":"vCLIENTCALLBACKURLSTATENAME"},{"av":"AV38ClientImageURL","fld":"vCLIENTIMAGEURL"},{"av":"AV30ClientAllowRemoteRestAuth","fld":"vCLIENTALLOWREMOTERESTAUTH"},{"av":"AV26ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV24ClientAllowGetUserAddDataRest","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV28ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV22ClientAllowGetSessionIniPropRest","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV20ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV18ClientAllowAdditionalScopeREST","fld":"vCLIENTALLOWADDITIONALSCOPEREST"},{"av":"AV36ClientEncryptionKey","fld":"vCLIENTENCRYPTIONKEY"},{"av":"AV40ClientRepositoryGUID","fld":"vCLIENTREPOSITORYGUID"},{"av":"AV7AccessRequiresPermission","fld":"vACCESSREQUIRESPERMISSION"},{"av":"AV72IsAuthorizationDelegated","fld":"vISAUTHORIZATIONDELEGATED"},{"av":"cmbavDelegateauthorizationversion"},{"av":"AV49DelegateAuthorizationVersion","fld":"vDELEGATEAUTHORIZATIONVERSION"},{"av":"AV46DelegateAuthorizationFileName","fld":"vDELEGATEAUTHORIZATIONFILENAME"},{"av":"AV48DelegateAuthorizationPackage","fld":"vDELEGATEAUTHORIZATIONPACKAGE"},{"av":"AV45DelegateAuthorizationClassName","fld":"vDELEGATEAUTHORIZATIONCLASSNAME"},{"av":"AV47DelegateAuthorizationMethod","fld":"vDELEGATEAUTHORIZATIONMETHOD"},{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"cmbavSsorestmode"},{"av":"AV95SSORestMode","fld":"vSSORESTMODE"},{"av":"AV101SSORestUserAuthTypeName","fld":"vSSORESTUSERAUTHTYPENAME"},{"av":"AV98SSORestServerURL","fld":"vSSORESTSERVERURL"},{"av":"AV99SSORestServerURL_isCustom","fld":"vSSORESTSERVERURL_ISCUSTOM"},{"av":"AV100SSORestServerURL_SLO","fld":"vSSORESTSERVERURL_SLO"},{"av":"AV97SSORestServerRepositoryGUID","fld":"vSSORESTSERVERREPOSITORYGUID"},{"av":"AV96SSORestServerKey","fld":"vSSORESTSERVERKEY"},{"av":"AV105STSProtocolEnable","fld":"vSTSPROTOCOLENABLE"},{"av":"AV103STSAuthorizationUserName","fld":"vSTSAUTHORIZATIONUSERNAME"},{"av":"AV102STSAuthorizationUserGUID","fld":"vSTSAUTHORIZATIONUSERGUID"},{"av":"cmbavStsmode"},{"av":"AV104STSMode","fld":"vSTSMODE"},{"av":"AV108STSServerURL","fld":"vSTSSERVERURL"},{"av":"AV106STSServerClientPassword","fld":"vSTSSERVERCLIENTPASSWORD"},{"av":"AV107STSServerRepositoryGUID","fld":"vSTSSERVERREPOSITORYGUID"},{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"cmbavMiniappmode"},{"av":"AV84MiniAppMode","fld":"vMINIAPPMODE"},{"av":"AV81MiniAppClientURL","fld":"vMINIAPPCLIENTURL"},{"av":"AV82MiniAppClientURL_isCustom","fld":"vMINIAPPCLIENTURL_ISCUSTOM"},{"av":"AV80MiniAppClientRepositoryGUID","fld":"vMINIAPPCLIENTREPOSITORYGUID"},{"av":"cmbavMiniappuserauthenticationtypename"},{"av":"AV88MiniAppUserAuthenticationTypeName","fld":"vMINIAPPUSERAUTHENTICATIONTYPENAME"},{"av":"AV86MiniAppServerURL","fld":"vMINIAPPSERVERURL"},{"av":"AV87MiniAppServerURL_isCustom","fld":"vMINIAPPSERVERURL_ISCUSTOM"},{"av":"AV85MiniAppServerRepositoryGUID","fld":"vMINIAPPSERVERREPOSITORYGUID"},{"av":"AV11APIKeyEnable","fld":"vAPIKEYENABLE"},{"av":"AV12APIKeyTimeout","fld":"vAPIKEYTIMEOUT","pic":"ZZZZZZZZ9"},{"av":"cmbavApikeyallowonlyauthenticationtypename"},{"av":"AV9APIKeyAllowOnlyAuthenticationTypeName","fld":"vAPIKEYALLOWONLYAUTHENTICATIONTYPENAME"},{"av":"AV10APIKeyAllowScopeCustomization","fld":"vAPIKEYALLOWSCOPECUSTOMIZATION"},{"av":"AV52EnvironmentName","fld":"vENVIRONMENTNAME"},{"av":"AV56EnvironmentSecureProtocol","fld":"vENVIRONMENTSECUREPROTOCOL"},{"av":"AV51EnvironmentHost","fld":"vENVIRONMENTHOST"},{"av":"AV53EnvironmentPort","fld":"vENVIRONMENTPORT","pic":"ZZZZ9"},{"av":"AV57EnvironmentVirtualDirectory","fld":"vENVIRONMENTVIRTUALDIRECTORY"},{"av":"AV55EnvironmentProgramPackage","fld":"vENVIRONMENTPROGRAMPACKAGE"},{"av":"AV54EnvironmentProgramExtension","fld":"vENVIRONMENTPROGRAMEXTENSION"},{"av":"AV75Language","fld":"vLANGUAGE","grid":563,"hsh":true},{"av":"GRIDLANGUAGES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_563","ctrl":"GRIDLANGUAGES","grid":563,"prop":"GridRC","grid":563},{"av":"AV90Online","fld":"vONLINE","grid":563}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV102STSAuthorizationUserGUID","fld":"vSTSAUTHORIZATIONUSERGUID"}]}""");
         setEventMetadata("'GENERATEKEYGAMREMOTE'","""{"handler":"E28142","iparms":[]""");
         setEventMetadata("'GENERATEKEYGAMREMOTE'",""","oparms":[{"av":"AV36ClientEncryptionKey","fld":"vCLIENTENCRYPTIONKEY"}]}""");
         setEventMetadata("'REVOKE-AUTHORIZE'","""{"handler":"E29142","iparms":[{"av":"AV71Id","fld":"vID","pic":"ZZZZZZZZZZZ9"}]""");
         setEventMetadata("'REVOKE-AUTHORIZE'",""","oparms":[{"av":"AV41ClientRevoked","fld":"vCLIENTREVOKED","pic":"99/99/9999 99:99"},{"ctrl":"BTNREVOKEALLOW","prop":"Caption"},{"av":"cmbavClientaccessstatus"},{"av":"AV15ClientAccessStatus","fld":"vCLIENTACCESSSTATUS"},{"av":"edtavClientrevoked_Visible","ctrl":"vCLIENTREVOKED","prop":"Visible"}]}""");
         setEventMetadata("'TRANSLATIONS'","""{"handler":"E30142","iparms":[{"av":"GRIDLANGUAGES_nFirstRecordOnPage"},{"av":"GRIDLANGUAGES_nEOF"},{"av":"subGridlanguages_Rows","ctrl":"GRIDLANGUAGES","prop":"Rows"},{"av":"chkavOnline.Enabled","ctrl":"vONLINE","prop":"Enabled"},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV92ReturnMenuOptionsWithoutPermission","fld":"vRETURNMENUOPTIONSWITHOUTPERMISSION"},{"av":"AV109UseAbsoluteUrlByEnvironment","fld":"vUSEABSOLUTEURLBYENVIRONMENT"},{"av":"AV31ClientAuthRequestMustIncludeUserScopes","fld":"vCLIENTAUTHREQUESTMUSTINCLUDEUSERSCOPES"},{"av":"AV35ClientDoNotShareUserIDs","fld":"vCLIENTDONOTSHAREUSERIDS"},{"av":"AV29ClientAllowRemoteAuth","fld":"vCLIENTALLOWREMOTEAUTH"},{"av":"AV25ClientAllowGetUserData","fld":"vCLIENTALLOWGETUSERDATA"},{"av":"AV23ClientAllowGetUserAddData","fld":"vCLIENTALLOWGETUSERADDDATA"},{"av":"AV27ClientAllowGetUserRoles","fld":"vCLIENTALLOWGETUSERROLES"},{"av":"AV21ClientAllowGetSessionIniProp","fld":"vCLIENTALLOWGETSESSIONINIPROP"},{"av":"AV19ClientAllowGetSessionAppData","fld":"vCLIENTALLOWGETSESSIONAPPDATA"},{"av":"AV33ClientCallbackURLisCustom","fld":"vCLIENTCALLBACKURLISCUSTOM"},{"av":"AV30ClientAllowRemoteRestAuth","fld":"vCLIENTALLOWREMOTERESTAUTH"},{"av":"AV26ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV24ClientAllowGetUserAddDataRest","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV28ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV22ClientAllowGetSessionIniPropRest","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV20ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV16ClientAccessUniqueByUser","fld":"vCLIENTACCESSUNIQUEBYUSER"},{"av":"AV7AccessRequiresPermission","fld":"vACCESSREQUIRESPERMISSION"},{"av":"AV72IsAuthorizationDelegated","fld":"vISAUTHORIZATIONDELEGATED"},{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"AV99SSORestServerURL_isCustom","fld":"vSSORESTSERVERURL_ISCUSTOM"},{"av":"AV105STSProtocolEnable","fld":"vSTSPROTOCOLENABLE"},{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"AV82MiniAppClientURL_isCustom","fld":"vMINIAPPCLIENTURL_ISCUSTOM"},{"av":"AV87MiniAppServerURL_isCustom","fld":"vMINIAPPSERVERURL_ISCUSTOM"},{"av":"AV11APIKeyEnable","fld":"vAPIKEYENABLE"},{"av":"AV10APIKeyAllowScopeCustomization","fld":"vAPIKEYALLOWSCOPECUSTOMIZATION"},{"av":"AV56EnvironmentSecureProtocol","fld":"vENVIRONMENTSECUREPROTOCOL"},{"av":"AV14AutoRegisterAnomymousUser","fld":"vAUTOREGISTERANOMYMOUSUSER"},{"av":"AV89Name","fld":"vNAME"},{"av":"AV71Id","fld":"vID","pic":"ZZZZZZZZZZZ9"}]}""");
         setEventMetadata("VSSORESTENABLE.CONTROLVALUECHANGED","""{"handler":"E18142","iparms":[{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"cmbavSsorestmode"},{"av":"AV95SSORestMode","fld":"vSSORESTMODE"}]""");
         setEventMetadata("VSSORESTENABLE.CONTROLVALUECHANGED",""","oparms":[{"av":"divTblssorestmodeclient_Visible","ctrl":"TBLSSORESTMODECLIENT","prop":"Visible"},{"av":"divTablessorest_Visible","ctrl":"TABLESSOREST","prop":"Visible"}]}""");
         setEventMetadata("VSSORESTMODE.CONTROLVALUECHANGED","""{"handler":"E19142","iparms":[{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"cmbavSsorestmode"},{"av":"AV95SSORestMode","fld":"vSSORESTMODE"}]""");
         setEventMetadata("VSSORESTMODE.CONTROLVALUECHANGED",""","oparms":[{"av":"divTblssorestmodeclient_Visible","ctrl":"TBLSSORESTMODECLIENT","prop":"Visible"},{"av":"divTablessorest_Visible","ctrl":"TABLESSOREST","prop":"Visible"}]}""");
         setEventMetadata("VMINIAPPENABLE.CONTROLVALUECHANGED","""{"handler":"E20142","iparms":[{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"cmbavMiniappmode"},{"av":"AV84MiniAppMode","fld":"vMINIAPPMODE"},{"av":"cmbavMiniappuserauthenticationtypename"},{"av":"AV88MiniAppUserAuthenticationTypeName","fld":"vMINIAPPUSERAUTHENTICATIONTYPENAME"}]""");
         setEventMetadata("VMINIAPPENABLE.CONTROLVALUECHANGED",""","oparms":[{"av":"divTblminiappserver_Visible","ctrl":"TBLMINIAPPSERVER","prop":"Visible"},{"av":"divTblminiappclient_Visible","ctrl":"TBLMINIAPPCLIENT","prop":"Visible"},{"av":"divTblminiapp_Visible","ctrl":"TBLMINIAPP","prop":"Visible"},{"av":"cmbavMiniappuserauthenticationtypename"},{"av":"AV88MiniAppUserAuthenticationTypeName","fld":"vMINIAPPUSERAUTHENTICATIONTYPENAME"}]}""");
         setEventMetadata("VMINIAPPMODE.CONTROLVALUECHANGED","""{"handler":"E21142","iparms":[{"av":"AV83MiniAppEnable","fld":"vMINIAPPENABLE"},{"av":"cmbavMiniappmode"},{"av":"AV84MiniAppMode","fld":"vMINIAPPMODE"},{"av":"cmbavMiniappuserauthenticationtypename"},{"av":"AV88MiniAppUserAuthenticationTypeName","fld":"vMINIAPPUSERAUTHENTICATIONTYPENAME"}]""");
         setEventMetadata("VMINIAPPMODE.CONTROLVALUECHANGED",""","oparms":[{"av":"divTblminiappserver_Visible","ctrl":"TBLMINIAPPSERVER","prop":"Visible"},{"av":"divTblminiappclient_Visible","ctrl":"TBLMINIAPPCLIENT","prop":"Visible"},{"av":"divTblminiapp_Visible","ctrl":"TBLMINIAPP","prop":"Visible"},{"av":"cmbavMiniappuserauthenticationtypename"},{"av":"AV88MiniAppUserAuthenticationTypeName","fld":"vMINIAPPUSERAUTHENTICATIONTYPENAME"}]}""");
         setEventMetadata("VAPIKEYENABLE.CONTROLVALUECHANGED","""{"handler":"E22142","iparms":[{"av":"AV11APIKeyEnable","fld":"vAPIKEYENABLE"},{"av":"cmbavApikeyallowonlyauthenticationtypename"},{"av":"AV9APIKeyAllowOnlyAuthenticationTypeName","fld":"vAPIKEYALLOWONLYAUTHENTICATIONTYPENAME"}]""");
         setEventMetadata("VAPIKEYENABLE.CONTROLVALUECHANGED",""","oparms":[{"av":"divTblapikey_Visible","ctrl":"TBLAPIKEY","prop":"Visible"},{"av":"cmbavApikeyallowonlyauthenticationtypename"},{"av":"AV9APIKeyAllowOnlyAuthenticationTypeName","fld":"vAPIKEYALLOWONLYAUTHENTICATIONTYPENAME"}]}""");
         setEventMetadata("VSSORESTMODE.CLICK","""{"handler":"E23142","iparms":[{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"cmbavSsorestmode"},{"av":"AV95SSORestMode","fld":"vSSORESTMODE"}]""");
         setEventMetadata("VSSORESTMODE.CLICK",""","oparms":[{"av":"divTblssorestmodeclient_Visible","ctrl":"TBLSSORESTMODECLIENT","prop":"Visible"},{"av":"divTablessorest_Visible","ctrl":"TABLESSOREST","prop":"Visible"}]}""");
         setEventMetadata("VSTSMODE.CLICK","""{"handler":"E11141","iparms":[{"av":"cmbavStsmode"},{"av":"AV104STSMode","fld":"vSTSMODE"}]""");
         setEventMetadata("VSTSMODE.CLICK",""","oparms":[{"av":"divTablestsserverchecktoken_Visible","ctrl":"TABLESTSSERVERCHECKTOKEN","prop":"Visible"},{"av":"divTablestsclient_Visible","ctrl":"TABLESTSCLIENT","prop":"Visible"},{"av":"edtavStsserverclientpassword_Visible","ctrl":"vSTSSERVERCLIENTPASSWORD","prop":"Visible"},{"av":"divStsserverclientpassword_cell_Class","ctrl":"STSSERVERCLIENTPASSWORD_CELL","prop":"Class"},{"av":"divTablestsclientgettoken_Visible","ctrl":"TABLESTSCLIENTGETTOKEN","prop":"Visible"}]}""");
         setEventMetadata("VSSORESTENABLE.CLICK","""{"handler":"E24142","iparms":[{"av":"AV94SSORestEnable","fld":"vSSORESTENABLE"},{"av":"cmbavSsorestmode"},{"av":"AV95SSORestMode","fld":"vSSORESTMODE"}]""");
         setEventMetadata("VSSORESTENABLE.CLICK",""","oparms":[{"av":"divTblssorestmodeclient_Visible","ctrl":"TBLSSORESTMODECLIENT","prop":"Visible"},{"av":"divTablessorest_Visible","ctrl":"TABLESSOREST","prop":"Visible"}]}""");
         setEventMetadata("VALIDV_CLIENTACCESSSTATUS","""{"handler":"Validv_Clientaccessstatus","iparms":[]}""");
         setEventMetadata("VALIDV_CLIENTREVOKED","""{"handler":"Validv_Clientrevoked","iparms":[]}""");
         setEventMetadata("VALIDV_DELEGATEAUTHORIZATIONVERSION","""{"handler":"Validv_Delegateauthorizationversion","iparms":[]}""");
         setEventMetadata("VALIDV_SSORESTMODE","""{"handler":"Validv_Ssorestmode","iparms":[]}""");
         setEventMetadata("VALIDV_STSMODE","""{"handler":"Validv_Stsmode","iparms":[]}""");
         setEventMetadata("VALIDV_MINIAPPMODE","""{"handler":"Validv_Miniappmode","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Language","iparms":[]}""");
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
         wcpOGx_mode = "";
         Gridlanguagespaginationbar_Selectedpage = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV115GridLanguagesAppliedFilters = "";
         Gridlanguages_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         StyleString = "";
         ucGxuitabspanel_tabs = new GXUserControl();
         lblGeneral_title_Jsonclick = "";
         TempTags = "";
         AV68GUID = "";
         AV89Name = "";
         AV50Dsc = "";
         AV111Version = "";
         AV43Company = "";
         AV44Copyright = "";
         AV69HomeObject = "";
         AV8AccountActivationObject = "";
         lblTextblocklogoutobject_Jsonclick = "";
         lblRemoteauthentication_title_Jsonclick = "";
         AV15ClientAccessStatus = "";
         AV41ClientRevoked = (DateTime)(DateTime.MinValue);
         AV37ClientId = "";
         lblTextblockclientsecret_Jsonclick = "";
         ucDvpanel_unnamedtable6 = new GXUserControl();
         AV17ClientAllowAdditionalScope = "";
         AV38ClientImageURL = "";
         AV39ClientLocalLoginURL = "";
         AV32ClientCallbackURL = "";
         AV34ClientCallbackURLStateName = "";
         ucDvpanel_unnamedtable7 = new GXUserControl();
         AV18ClientAllowAdditionalScopeREST = "";
         ucDvpanel_unnamedtable8 = new GXUserControl();
         lblTextblockclientencryptionkey_Jsonclick = "";
         AV40ClientRepositoryGUID = "";
         lblAuthorization_title_Jsonclick = "";
         AV49DelegateAuthorizationVersion = "";
         AV46DelegateAuthorizationFileName = "";
         AV48DelegateAuthorizationPackage = "";
         AV45DelegateAuthorizationClassName = "";
         AV47DelegateAuthorizationMethod = "";
         lblSsorest_title_Jsonclick = "";
         AV95SSORestMode = "";
         AV101SSORestUserAuthTypeName = "";
         AV98SSORestServerURL = "";
         AV100SSORestServerURL_SLO = "";
         AV97SSORestServerRepositoryGUID = "";
         AV96SSORestServerKey = "";
         lblSts_title_Jsonclick = "";
         AV104STSMode = "";
         AV103STSAuthorizationUserName = "";
         AV106STSServerClientPassword = "";
         AV108STSServerURL = "";
         AV107STSServerRepositoryGUID = "";
         lblMiniapp_title_Jsonclick = "";
         AV84MiniAppMode = "";
         AV81MiniAppClientURL = "";
         AV80MiniAppClientRepositoryGUID = "";
         AV88MiniAppUserAuthenticationTypeName = "";
         AV86MiniAppServerURL = "";
         AV85MiniAppServerRepositoryGUID = "";
         lblApikey_title_Jsonclick = "";
         AV9APIKeyAllowOnlyAuthenticationTypeName = "";
         lblEnvironmentsettings_title_Jsonclick = "";
         AV52EnvironmentName = "";
         AV51EnvironmentHost = "";
         AV57EnvironmentVirtualDirectory = "";
         AV55EnvironmentProgramPackage = "";
         AV54EnvironmentProgramExtension = "";
         lblLanguages_title_Jsonclick = "";
         GridlanguagesContainer = new GXWebGrid( context);
         sStyleString = "";
         ucGridlanguagespaginationbar = new GXUserControl();
         bttBtnenter_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         AV102STSAuthorizationUserGUID = "";
         ucGridlanguages_empowerer = new GXUserControl();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV75Language = "";
         GXDecQS = "";
         AV76LogoutObject = "";
         AV42ClientSecret = "";
         AV36ClientEncryptionKey = "";
         AV60GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV64GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV117GXV1 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu>( context, "GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu", "DesignSystem.Programs");
         AV79MenuFilter = new GeneXus.Programs.genexussecurity.SdtGAMApplicationMenuFilter(context);
         AV5GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV78Menu = new GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu(context);
         AV6Window = new GXWindow();
         AV59Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV110UserErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV58Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         AV61GAMAuthenticationTypeFilter = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter(context);
         AV121GXV5 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType", "DesignSystem.Programs");
         AV13AuthenticationType = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType(context);
         AV123GXV7 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType", "DesignSystem.Programs");
         AV125GXV9 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType", "DesignSystem.Programs");
         AV63GAMLanguages = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage>( context, "GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage", "DesignSystem.Programs");
         AV62GAMLanguage = new GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage(context);
         GridlanguagesRow = new GXWebRow();
         bttBtngeneratekeygamremote_Jsonclick = "";
         bttBtnchangeclientsecret_Jsonclick = "";
         bttBtnrevokeallow_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridlanguages_Linesclass = "";
         GXCCtl = "";
         ROClassString = "";
         GridlanguagesColumn = new GXWebColumn();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamapplicationentry__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamapplicationentry__default(),
            new Object[][] {
            }
         );
         /* GeneXus formulas. */
         edtavId_Enabled = 0;
         edtavGuid_Enabled = 0;
         edtavClientrevoked_Enabled = 0;
         chkavOnline.Enabled = 0;
         edtavLanguage_Enabled = 0;
      }

      private short GRIDLANGUAGES_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short subGridlanguages_Backcolorstyle ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private short subGridlanguages_Backstyle ;
      private short subGridlanguages_Titlebackstyle ;
      private short subGridlanguages_Allowselection ;
      private short subGridlanguages_Allowhovering ;
      private short subGridlanguages_Allowcollapsing ;
      private short subGridlanguages_Collapsed ;
      private int Gridlanguagespaginationbar_Rowsperpageselectedvalue ;
      private int nRC_GXsfl_563 ;
      private int subGridlanguages_Recordcount ;
      private int subGridlanguages_Rows ;
      private int nGXsfl_563_idx=1 ;
      private int Gridlanguagespaginationbar_Pagestoshow ;
      private int Gxuitabspanel_tabs_Pagecount ;
      private int edtavId_Enabled ;
      private int edtavGuid_Enabled ;
      private int edtavName_Enabled ;
      private int edtavDsc_Enabled ;
      private int edtavVersion_Enabled ;
      private int edtavCompany_Enabled ;
      private int edtavCopyright_Enabled ;
      private int edtavHomeobject_Enabled ;
      private int edtavAccountactivationobject_Enabled ;
      private int edtavClientrevoked_Visible ;
      private int edtavClientrevoked_Enabled ;
      private int edtavClientid_Enabled ;
      private int divTblwebauth_Visible ;
      private int edtavClientallowadditionalscope_Enabled ;
      private int edtavClientimageurl_Enabled ;
      private int edtavClientlocalloginurl_Enabled ;
      private int edtavClientcallbackurl_Enabled ;
      private int edtavClientcallbackurlstatename_Enabled ;
      private int divTblrestauth_Visible ;
      private int edtavClientallowadditionalscoperest_Enabled ;
      private int divTblgeneralauth_Visible ;
      private int edtavClientrepositoryguid_Enabled ;
      private int divTbldelegateauthorization_Visible ;
      private int divTbldelegateauthorizationprop_Visible ;
      private int edtavDelegateauthorizationfilename_Enabled ;
      private int edtavDelegateauthorizationpackage_Enabled ;
      private int edtavDelegateauthorizationclassname_Enabled ;
      private int edtavDelegateauthorizationmethod_Enabled ;
      private int divTablessorest_Visible ;
      private int divTblssorestmodeclient_Visible ;
      private int edtavSsorestuserauthtypename_Enabled ;
      private int edtavSsorestserverurl_Enabled ;
      private int edtavSsorestserverurl_slo_Enabled ;
      private int edtavSsorestserverrepositoryguid_Enabled ;
      private int edtavSsorestserverkey_Enabled ;
      private int divTablests_Visible ;
      private int divTablestsserverchecktoken_Visible ;
      private int edtavStsauthorizationusername_Enabled ;
      private int divTablestsclientgettoken_Visible ;
      private int edtavStsserverclientpassword_Visible ;
      private int edtavStsserverclientpassword_Enabled ;
      private int divTablestsclient_Visible ;
      private int edtavStsserverurl_Enabled ;
      private int edtavStsserverrepositoryguid_Enabled ;
      private int divTblminiapp_Visible ;
      private int divTblminiappserver_Visible ;
      private int edtavMiniappclienturl_Enabled ;
      private int edtavMiniappclientrepositoryguid_Enabled ;
      private int divTblminiappclient_Visible ;
      private int edtavMiniappserverurl_Enabled ;
      private int edtavMiniappserverrepositoryguid_Enabled ;
      private int divTblapikey_Visible ;
      private int AV12APIKeyTimeout ;
      private int edtavApikeytimeout_Enabled ;
      private int edtavEnvironmentname_Enabled ;
      private int edtavEnvironmenthost_Enabled ;
      private int AV53EnvironmentPort ;
      private int edtavEnvironmentport_Enabled ;
      private int edtavEnvironmentvirtualdirectory_Enabled ;
      private int edtavEnvironmentprogrampackage_Enabled ;
      private int edtavEnvironmentprogramextension_Enabled ;
      private int bttBtnenter_Visible ;
      private int edtavGridlanguagescurrentpage_Visible ;
      private int edtavStsauthorizationuserguid_Visible ;
      private int subGridlanguages_Islastpage ;
      private int edtavLanguage_Enabled ;
      private int GRIDLANGUAGES_nGridOutOfScope ;
      private int bttBtnchangeclientsecret_Visible ;
      private int edtavClientsecret_Enabled ;
      private int AV118GXV2 ;
      private int edtavLogoutobject_Enabled ;
      private int edtavClientencryptionkey_Enabled ;
      private int bttBtngeneratekeygamremote_Visible ;
      private int AV91PageToGo ;
      private int AV119GXV3 ;
      private int AV120GXV4 ;
      private int AV122GXV6 ;
      private int AV124GXV8 ;
      private int AV126GXV10 ;
      private int AV127GXV11 ;
      private int nGXsfl_563_fel_idx=1 ;
      private int AV129GXV12 ;
      private int idxLst ;
      private int subGridlanguages_Backcolor ;
      private int subGridlanguages_Allbackcolor ;
      private int subGridlanguages_Titlebackcolor ;
      private int subGridlanguages_Selectedindex ;
      private int subGridlanguages_Selectioncolor ;
      private int subGridlanguages_Hoveringcolor ;
      private long AV71Id ;
      private long wcpOAV71Id ;
      private long GRIDLANGUAGES_nFirstRecordOnPage ;
      private long AV67GridLanguagesPageCount ;
      private long AV77MainMenu ;
      private long AV66GridLanguagesCurrentPage ;
      private long GRIDLANGUAGES_nCurrentRecord ;
      private string Gx_mode ;
      private string wcpOGx_mode ;
      private string Gridlanguagespaginationbar_Selectedpage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_563_idx="0001" ;
      private string chkavOnline_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string Dvpanel_unnamedtable6_Width ;
      private string Dvpanel_unnamedtable6_Cls ;
      private string Dvpanel_unnamedtable6_Title ;
      private string Dvpanel_unnamedtable6_Iconposition ;
      private string Dvpanel_unnamedtable7_Width ;
      private string Dvpanel_unnamedtable7_Cls ;
      private string Dvpanel_unnamedtable7_Title ;
      private string Dvpanel_unnamedtable7_Iconposition ;
      private string Dvpanel_unnamedtable8_Width ;
      private string Dvpanel_unnamedtable8_Cls ;
      private string Dvpanel_unnamedtable8_Title ;
      private string Dvpanel_unnamedtable8_Iconposition ;
      private string Gridlanguagespaginationbar_Class ;
      private string Gridlanguagespaginationbar_Pagingbuttonsposition ;
      private string Gridlanguagespaginationbar_Pagingcaptionposition ;
      private string Gridlanguagespaginationbar_Emptygridclass ;
      private string Gridlanguagespaginationbar_Rowsperpageoptions ;
      private string Gridlanguagespaginationbar_Previous ;
      private string Gridlanguagespaginationbar_Next ;
      private string Gridlanguagespaginationbar_Caption ;
      private string Gridlanguagespaginationbar_Emptygridcaption ;
      private string Gridlanguagespaginationbar_Rowsperpagecaption ;
      private string Gxuitabspanel_tabs_Class ;
      private string Gridlanguages_empowerer_Gridinternalname ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string divTablecontent_Internalname ;
      private string Gxuitabspanel_tabs_Internalname ;
      private string lblGeneral_title_Internalname ;
      private string lblGeneral_title_Jsonclick ;
      private string divUnnamedtable9_Internalname ;
      private string edtavId_Internalname ;
      private string TempTags ;
      private string edtavId_Jsonclick ;
      private string edtavGuid_Internalname ;
      private string AV68GUID ;
      private string edtavGuid_Jsonclick ;
      private string edtavName_Internalname ;
      private string AV89Name ;
      private string edtavName_Jsonclick ;
      private string edtavDsc_Internalname ;
      private string AV50Dsc ;
      private string edtavDsc_Jsonclick ;
      private string edtavVersion_Internalname ;
      private string AV111Version ;
      private string edtavVersion_Jsonclick ;
      private string edtavCompany_Internalname ;
      private string AV43Company ;
      private string edtavCompany_Jsonclick ;
      private string edtavCopyright_Internalname ;
      private string AV44Copyright ;
      private string edtavCopyright_Jsonclick ;
      private string chkavReturnmenuoptionswithoutpermission_Internalname ;
      private string cmbavMainmenu_Internalname ;
      private string cmbavMainmenu_Jsonclick ;
      private string chkavUseabsoluteurlbyenvironment_Internalname ;
      private string edtavHomeobject_Internalname ;
      private string edtavHomeobject_Jsonclick ;
      private string edtavAccountactivationobject_Internalname ;
      private string edtavAccountactivationobject_Jsonclick ;
      private string divTablesplittedlogoutobject_Internalname ;
      private string lblTextblocklogoutobject_Internalname ;
      private string lblTextblocklogoutobject_Jsonclick ;
      private string lblRemoteauthentication_title_Internalname ;
      private string lblRemoteauthentication_title_Jsonclick ;
      private string divUnnamedtable5_Internalname ;
      private string cmbavClientaccessstatus_Internalname ;
      private string AV15ClientAccessStatus ;
      private string cmbavClientaccessstatus_Jsonclick ;
      private string edtavClientrevoked_Internalname ;
      private string edtavClientrevoked_Jsonclick ;
      private string edtavClientid_Internalname ;
      private string AV37ClientId ;
      private string edtavClientid_Jsonclick ;
      private string divTablesplittedclientsecret_Internalname ;
      private string lblTextblockclientsecret_Internalname ;
      private string lblTextblockclientsecret_Jsonclick ;
      private string chkavClientauthrequestmustincludeuserscopes_Internalname ;
      private string chkavClientdonotshareuserids_Internalname ;
      private string Dvpanel_unnamedtable6_Internalname ;
      private string divUnnamedtable6_Internalname ;
      private string chkavClientallowremoteauth_Internalname ;
      private string divTblwebauth_Internalname ;
      private string chkavClientallowgetuserdata_Internalname ;
      private string chkavClientallowgetuseradddata_Internalname ;
      private string chkavClientallowgetuserroles_Internalname ;
      private string chkavClientallowgetsessioniniprop_Internalname ;
      private string chkavClientallowgetsessionappdata_Internalname ;
      private string edtavClientallowadditionalscope_Internalname ;
      private string edtavClientallowadditionalscope_Jsonclick ;
      private string edtavClientimageurl_Internalname ;
      private string edtavClientimageurl_Jsonclick ;
      private string edtavClientlocalloginurl_Internalname ;
      private string edtavClientlocalloginurl_Jsonclick ;
      private string edtavClientcallbackurl_Internalname ;
      private string edtavClientcallbackurl_Jsonclick ;
      private string chkavClientcallbackurliscustom_Internalname ;
      private string edtavClientcallbackurlstatename_Internalname ;
      private string AV34ClientCallbackURLStateName ;
      private string edtavClientcallbackurlstatename_Jsonclick ;
      private string Dvpanel_unnamedtable7_Internalname ;
      private string divUnnamedtable7_Internalname ;
      private string chkavClientallowremoterestauth_Internalname ;
      private string divTblrestauth_Internalname ;
      private string chkavClientallowgetuserdatarest_Internalname ;
      private string chkavClientallowgetuseradddatarest_Internalname ;
      private string chkavClientallowgetuserrolesrest_Internalname ;
      private string chkavClientallowgetsessioniniproprest_Internalname ;
      private string chkavClientallowgetsessionappdatarest_Internalname ;
      private string edtavClientallowadditionalscoperest_Internalname ;
      private string edtavClientallowadditionalscoperest_Jsonclick ;
      private string divTblgeneralauth_Internalname ;
      private string Dvpanel_unnamedtable8_Internalname ;
      private string divUnnamedtable8_Internalname ;
      private string chkavClientaccessuniquebyuser_Internalname ;
      private string divTablesplittedclientencryptionkey_Internalname ;
      private string lblTextblockclientencryptionkey_Internalname ;
      private string lblTextblockclientencryptionkey_Jsonclick ;
      private string edtavClientrepositoryguid_Internalname ;
      private string AV40ClientRepositoryGUID ;
      private string edtavClientrepositoryguid_Jsonclick ;
      private string lblAuthorization_title_Internalname ;
      private string lblAuthorization_title_Jsonclick ;
      private string divUnnamedtable4_Internalname ;
      private string chkavAccessrequirespermission_Internalname ;
      private string divTbldelegateauthorization_Internalname ;
      private string chkavIsauthorizationdelegated_Internalname ;
      private string divTbldelegateauthorizationprop_Internalname ;
      private string cmbavDelegateauthorizationversion_Internalname ;
      private string AV49DelegateAuthorizationVersion ;
      private string cmbavDelegateauthorizationversion_Jsonclick ;
      private string edtavDelegateauthorizationfilename_Internalname ;
      private string AV46DelegateAuthorizationFileName ;
      private string edtavDelegateauthorizationfilename_Jsonclick ;
      private string edtavDelegateauthorizationpackage_Internalname ;
      private string AV48DelegateAuthorizationPackage ;
      private string edtavDelegateauthorizationpackage_Jsonclick ;
      private string edtavDelegateauthorizationclassname_Internalname ;
      private string AV45DelegateAuthorizationClassName ;
      private string edtavDelegateauthorizationclassname_Jsonclick ;
      private string edtavDelegateauthorizationmethod_Internalname ;
      private string AV47DelegateAuthorizationMethod ;
      private string edtavDelegateauthorizationmethod_Jsonclick ;
      private string lblSsorest_title_Internalname ;
      private string lblSsorest_title_Jsonclick ;
      private string divUnnamedtable3_Internalname ;
      private string chkavSsorestenable_Internalname ;
      private string divTablessorest_Internalname ;
      private string cmbavSsorestmode_Internalname ;
      private string AV95SSORestMode ;
      private string cmbavSsorestmode_Jsonclick ;
      private string divTblssorestmodeclient_Internalname ;
      private string edtavSsorestuserauthtypename_Internalname ;
      private string AV101SSORestUserAuthTypeName ;
      private string edtavSsorestuserauthtypename_Jsonclick ;
      private string edtavSsorestserverurl_Internalname ;
      private string edtavSsorestserverurl_Jsonclick ;
      private string chkavSsorestserverurl_iscustom_Internalname ;
      private string edtavSsorestserverurl_slo_Internalname ;
      private string edtavSsorestserverurl_slo_Jsonclick ;
      private string edtavSsorestserverrepositoryguid_Internalname ;
      private string AV97SSORestServerRepositoryGUID ;
      private string edtavSsorestserverrepositoryguid_Jsonclick ;
      private string edtavSsorestserverkey_Internalname ;
      private string AV96SSORestServerKey ;
      private string edtavSsorestserverkey_Jsonclick ;
      private string lblSts_title_Internalname ;
      private string lblSts_title_Jsonclick ;
      private string divUnnamedtable2_Internalname ;
      private string chkavStsprotocolenable_Internalname ;
      private string divTablests_Internalname ;
      private string cmbavStsmode_Internalname ;
      private string AV104STSMode ;
      private string cmbavStsmode_Jsonclick ;
      private string divTablestsserverchecktoken_Internalname ;
      private string edtavStsauthorizationusername_Internalname ;
      private string edtavStsauthorizationusername_Jsonclick ;
      private string divTablestsclientgettoken_Internalname ;
      private string divStsserverclientpassword_cell_Internalname ;
      private string divStsserverclientpassword_cell_Class ;
      private string edtavStsserverclientpassword_Internalname ;
      private string AV106STSServerClientPassword ;
      private string edtavStsserverclientpassword_Jsonclick ;
      private string divTablestsclient_Internalname ;
      private string edtavStsserverurl_Internalname ;
      private string edtavStsserverurl_Jsonclick ;
      private string edtavStsserverrepositoryguid_Internalname ;
      private string AV107STSServerRepositoryGUID ;
      private string edtavStsserverrepositoryguid_Jsonclick ;
      private string lblMiniapp_title_Internalname ;
      private string lblMiniapp_title_Jsonclick ;
      private string divMiniapptable1_Internalname ;
      private string chkavMiniappenable_Internalname ;
      private string divTblminiapp_Internalname ;
      private string cmbavMiniappmode_Internalname ;
      private string AV84MiniAppMode ;
      private string cmbavMiniappmode_Jsonclick ;
      private string divTblminiappserver_Internalname ;
      private string edtavMiniappclienturl_Internalname ;
      private string edtavMiniappclienturl_Jsonclick ;
      private string chkavMiniappclienturl_iscustom_Internalname ;
      private string edtavMiniappclientrepositoryguid_Internalname ;
      private string AV80MiniAppClientRepositoryGUID ;
      private string edtavMiniappclientrepositoryguid_Jsonclick ;
      private string divTblminiappclient_Internalname ;
      private string cmbavMiniappuserauthenticationtypename_Internalname ;
      private string AV88MiniAppUserAuthenticationTypeName ;
      private string cmbavMiniappuserauthenticationtypename_Jsonclick ;
      private string edtavMiniappserverurl_Internalname ;
      private string edtavMiniappserverurl_Jsonclick ;
      private string chkavMiniappserverurl_iscustom_Internalname ;
      private string edtavMiniappserverrepositoryguid_Internalname ;
      private string AV85MiniAppServerRepositoryGUID ;
      private string edtavMiniappserverrepositoryguid_Jsonclick ;
      private string lblApikey_title_Internalname ;
      private string lblApikey_title_Jsonclick ;
      private string divApikeytable1_Internalname ;
      private string chkavApikeyenable_Internalname ;
      private string divTblapikey_Internalname ;
      private string edtavApikeytimeout_Internalname ;
      private string edtavApikeytimeout_Jsonclick ;
      private string cmbavApikeyallowonlyauthenticationtypename_Internalname ;
      private string AV9APIKeyAllowOnlyAuthenticationTypeName ;
      private string cmbavApikeyallowonlyauthenticationtypename_Jsonclick ;
      private string chkavApikeyallowscopecustomization_Internalname ;
      private string lblEnvironmentsettings_title_Internalname ;
      private string lblEnvironmentsettings_title_Jsonclick ;
      private string divUnnamedtable1_Internalname ;
      private string edtavEnvironmentname_Internalname ;
      private string AV52EnvironmentName ;
      private string edtavEnvironmentname_Jsonclick ;
      private string chkavEnvironmentsecureprotocol_Internalname ;
      private string edtavEnvironmenthost_Internalname ;
      private string AV51EnvironmentHost ;
      private string edtavEnvironmenthost_Jsonclick ;
      private string edtavEnvironmentport_Internalname ;
      private string edtavEnvironmentport_Jsonclick ;
      private string edtavEnvironmentvirtualdirectory_Internalname ;
      private string AV57EnvironmentVirtualDirectory ;
      private string edtavEnvironmentvirtualdirectory_Jsonclick ;
      private string edtavEnvironmentprogrampackage_Internalname ;
      private string AV55EnvironmentProgramPackage ;
      private string edtavEnvironmentprogrampackage_Jsonclick ;
      private string edtavEnvironmentprogramextension_Internalname ;
      private string AV54EnvironmentProgramExtension ;
      private string edtavEnvironmentprogramextension_Jsonclick ;
      private string lblLanguages_title_Internalname ;
      private string lblLanguages_title_Jsonclick ;
      private string divLanguagestable1_Internalname ;
      private string divGridlanguagestablewithpaginationbar_Internalname ;
      private string sStyleString ;
      private string subGridlanguages_Internalname ;
      private string Gridlanguagespaginationbar_Internalname ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Caption ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Jsonclick ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string edtavGridlanguagescurrentpage_Internalname ;
      private string edtavGridlanguagescurrentpage_Jsonclick ;
      private string chkavAutoregisteranomymoususer_Internalname ;
      private string edtavStsauthorizationuserguid_Internalname ;
      private string AV102STSAuthorizationUserGUID ;
      private string edtavStsauthorizationuserguid_Jsonclick ;
      private string Gridlanguages_empowerer_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavLanguage_Internalname ;
      private string GXDecQS ;
      private string edtavLogoutobject_Internalname ;
      private string AV42ClientSecret ;
      private string edtavClientsecret_Internalname ;
      private string AV36ClientEncryptionKey ;
      private string edtavClientencryptionkey_Internalname ;
      private string bttBtnchangeclientsecret_Internalname ;
      private string bttBtngeneratekeygamremote_Internalname ;
      private string bttBtnrevokeallow_Caption ;
      private string bttBtnrevokeallow_Internalname ;
      private string sGXsfl_563_fel_idx="0001" ;
      private string tblTablemergedclientencryptionkey_Internalname ;
      private string edtavClientencryptionkey_Jsonclick ;
      private string bttBtngeneratekeygamremote_Jsonclick ;
      private string tblTablemergedclientsecret_Internalname ;
      private string edtavClientsecret_Jsonclick ;
      private string bttBtnchangeclientsecret_Jsonclick ;
      private string tblTablemergedlogoutobject_Internalname ;
      private string edtavLogoutobject_Jsonclick ;
      private string bttBtnrevokeallow_Jsonclick ;
      private string subGridlanguages_Class ;
      private string subGridlanguages_Linesclass ;
      private string GXCCtl ;
      private string ROClassString ;
      private string edtavLanguage_Jsonclick ;
      private string subGridlanguages_Header ;
      private DateTime AV41ClientRevoked ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_563_Refreshing=false ;
      private bool AV92ReturnMenuOptionsWithoutPermission ;
      private bool AV109UseAbsoluteUrlByEnvironment ;
      private bool AV31ClientAuthRequestMustIncludeUserScopes ;
      private bool AV35ClientDoNotShareUserIDs ;
      private bool AV29ClientAllowRemoteAuth ;
      private bool AV25ClientAllowGetUserData ;
      private bool AV23ClientAllowGetUserAddData ;
      private bool AV27ClientAllowGetUserRoles ;
      private bool AV21ClientAllowGetSessionIniProp ;
      private bool AV19ClientAllowGetSessionAppData ;
      private bool AV33ClientCallbackURLisCustom ;
      private bool AV30ClientAllowRemoteRestAuth ;
      private bool AV26ClientAllowGetUserDataREST ;
      private bool AV24ClientAllowGetUserAddDataRest ;
      private bool AV28ClientAllowGetUserRolesRest ;
      private bool AV22ClientAllowGetSessionIniPropRest ;
      private bool AV20ClientAllowGetSessionAppDataREST ;
      private bool AV16ClientAccessUniqueByUser ;
      private bool AV7AccessRequiresPermission ;
      private bool AV72IsAuthorizationDelegated ;
      private bool AV94SSORestEnable ;
      private bool AV99SSORestServerURL_isCustom ;
      private bool AV105STSProtocolEnable ;
      private bool AV83MiniAppEnable ;
      private bool AV82MiniAppClientURL_isCustom ;
      private bool AV87MiniAppServerURL_isCustom ;
      private bool AV11APIKeyEnable ;
      private bool AV10APIKeyAllowScopeCustomization ;
      private bool AV56EnvironmentSecureProtocol ;
      private bool AV14AutoRegisterAnomymousUser ;
      private bool Dvpanel_unnamedtable6_Autowidth ;
      private bool Dvpanel_unnamedtable6_Autoheight ;
      private bool Dvpanel_unnamedtable6_Collapsible ;
      private bool Dvpanel_unnamedtable6_Collapsed ;
      private bool Dvpanel_unnamedtable6_Showcollapseicon ;
      private bool Dvpanel_unnamedtable6_Autoscroll ;
      private bool Dvpanel_unnamedtable7_Autowidth ;
      private bool Dvpanel_unnamedtable7_Autoheight ;
      private bool Dvpanel_unnamedtable7_Collapsible ;
      private bool Dvpanel_unnamedtable7_Collapsed ;
      private bool Dvpanel_unnamedtable7_Showcollapseicon ;
      private bool Dvpanel_unnamedtable7_Autoscroll ;
      private bool Dvpanel_unnamedtable8_Autowidth ;
      private bool Dvpanel_unnamedtable8_Autoheight ;
      private bool Dvpanel_unnamedtable8_Collapsible ;
      private bool Dvpanel_unnamedtable8_Collapsed ;
      private bool Dvpanel_unnamedtable8_Showcollapseicon ;
      private bool Dvpanel_unnamedtable8_Autoscroll ;
      private bool Gridlanguagespaginationbar_Showfirst ;
      private bool Gridlanguagespaginationbar_Showprevious ;
      private bool Gridlanguagespaginationbar_Shownext ;
      private bool Gridlanguagespaginationbar_Showlast ;
      private bool Gridlanguagespaginationbar_Rowsperpageselector ;
      private bool Gxuitabspanel_tabs_Historymanagement ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool AV90Online ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool AV74isOk ;
      private string AV115GridLanguagesAppliedFilters ;
      private string AV69HomeObject ;
      private string AV8AccountActivationObject ;
      private string AV17ClientAllowAdditionalScope ;
      private string AV38ClientImageURL ;
      private string AV39ClientLocalLoginURL ;
      private string AV32ClientCallbackURL ;
      private string AV18ClientAllowAdditionalScopeREST ;
      private string AV98SSORestServerURL ;
      private string AV100SSORestServerURL_SLO ;
      private string AV103STSAuthorizationUserName ;
      private string AV108STSServerURL ;
      private string AV81MiniAppClientURL ;
      private string AV86MiniAppServerURL ;
      private string AV75Language ;
      private string AV76LogoutObject ;
      private GXWebGrid GridlanguagesContainer ;
      private GXWebRow GridlanguagesRow ;
      private GXWebColumn GridlanguagesColumn ;
      private GXUserControl ucGxuitabspanel_tabs ;
      private GXUserControl ucDvpanel_unnamedtable6 ;
      private GXUserControl ucDvpanel_unnamedtable7 ;
      private GXUserControl ucDvpanel_unnamedtable8 ;
      private GXUserControl ucGridlanguagespaginationbar ;
      private GXUserControl ucGridlanguages_empowerer ;
      private GXWebForm Form ;
      private GXWindow AV6Window ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_Gx_mode ;
      private long aP1_Id ;
      private GXCheckbox chkavReturnmenuoptionswithoutpermission ;
      private GXCombobox cmbavMainmenu ;
      private GXCheckbox chkavUseabsoluteurlbyenvironment ;
      private GXCombobox cmbavClientaccessstatus ;
      private GXCheckbox chkavClientauthrequestmustincludeuserscopes ;
      private GXCheckbox chkavClientdonotshareuserids ;
      private GXCheckbox chkavClientallowremoteauth ;
      private GXCheckbox chkavClientallowgetuserdata ;
      private GXCheckbox chkavClientallowgetuseradddata ;
      private GXCheckbox chkavClientallowgetuserroles ;
      private GXCheckbox chkavClientallowgetsessioniniprop ;
      private GXCheckbox chkavClientallowgetsessionappdata ;
      private GXCheckbox chkavClientcallbackurliscustom ;
      private GXCheckbox chkavClientallowremoterestauth ;
      private GXCheckbox chkavClientallowgetuserdatarest ;
      private GXCheckbox chkavClientallowgetuseradddatarest ;
      private GXCheckbox chkavClientallowgetuserrolesrest ;
      private GXCheckbox chkavClientallowgetsessioniniproprest ;
      private GXCheckbox chkavClientallowgetsessionappdatarest ;
      private GXCheckbox chkavClientaccessuniquebyuser ;
      private GXCheckbox chkavAccessrequirespermission ;
      private GXCheckbox chkavIsauthorizationdelegated ;
      private GXCombobox cmbavDelegateauthorizationversion ;
      private GXCheckbox chkavSsorestenable ;
      private GXCombobox cmbavSsorestmode ;
      private GXCheckbox chkavSsorestserverurl_iscustom ;
      private GXCheckbox chkavStsprotocolenable ;
      private GXCombobox cmbavStsmode ;
      private GXCheckbox chkavMiniappenable ;
      private GXCombobox cmbavMiniappmode ;
      private GXCheckbox chkavMiniappclienturl_iscustom ;
      private GXCombobox cmbavMiniappuserauthenticationtypename ;
      private GXCheckbox chkavMiniappserverurl_iscustom ;
      private GXCheckbox chkavApikeyenable ;
      private GXCombobox cmbavApikeyallowonlyauthenticationtypename ;
      private GXCheckbox chkavApikeyallowscopecustomization ;
      private GXCheckbox chkavEnvironmentsecureprotocol ;
      private GXCheckbox chkavOnline ;
      private GXCheckbox chkavAutoregisteranomymoususer ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV60GAMApplication ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV64GAMUser ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu> AV117GXV1 ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationMenuFilter AV79MenuFilter ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV5GAMErrorCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu AV78Menu ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV59Errors ;
      private IDataStoreProvider pr_default ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV110UserErrors ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV58Error ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter AV61GAMAuthenticationTypeFilter ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType> AV121GXV5 ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType AV13AuthenticationType ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType> AV123GXV7 ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType> AV125GXV9 ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage> AV63GAMLanguages ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage AV62GAMLanguage ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamapplicationentry__gam : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          def= new CursorDef[] {
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
    }

    public override string getDataStoreName( )
    {
       return "GAM";
    }

 }

 public class gamapplicationentry__default : DataStoreHelperBase, IDataStoreHelper
 {
    public ICursor[] getCursors( )
    {
       cursorDefinitions();
       return new Cursor[] {
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        def= new CursorDef[] {
        };
     }
  }

  public void getResults( int cursor ,
                          IFieldGetter rslt ,
                          Object[] buf )
  {
  }

}

}
