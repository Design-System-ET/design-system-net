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
   public class gamwwusers : GXDataArea
   {
      public gamwwusers( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamwwusers( IGxContext context )
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
         cmbavFilusergender = new GXCombobox();
         cmbavFilauttype = new GXCombobox();
         cmbavFilrol = new GXCombobox();
         cmbavGridactiongroup1 = new GXCombobox();
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
         nRC_GXsfl_60 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_60"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_60_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_60_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_60_idx = GetPar( "sGXsfl_60_idx");
         edtavBtnrole_Title = GetNextPar( );
         AssignProp("", false, edtavBtnrole_Internalname, "Title", edtavBtnrole_Title, !bGXsfl_60_Refreshing);
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
         AV79ManageFiltersExecutionStep = (short)(Math.Round(NumberUtil.Val( GetPar( "ManageFiltersExecutionStep"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV17ColumnsSelector);
         AV123Pgmname = GetPar( "Pgmname");
         cmbavFilusergender.FromJSonString( GetNextPar( ));
         AV40FilUserGender = GetPar( "FilUserGender");
         cmbavFilauttype.FromJSonString( GetNextPar( ));
         AV35FilAutType = GetPar( "FilAutType");
         AV91Search = GetPar( "Search");
         cmbavFilrol.FromJSonString( GetNextPar( ));
         AV36FilRol = (long)(Math.Round(NumberUtil.Val( GetPar( "FilRol"), "."), 18, MidpointRounding.ToEven));
         edtavBtnrole_Title = GetNextPar( );
         AssignProp("", false, edtavBtnrole_Internalname, "Title", edtavBtnrole_Title, !bGXsfl_60_Refreshing);
         AV41FirstIndex = (short)(Math.Round(NumberUtil.Val( GetPar( "FirstIndex"), "."), 18, MidpointRounding.ToEven));
         AV111IsAuthorized_Display = StringUtil.StrToBool( GetPar( "IsAuthorized_Display"));
         AV112IsAuthorized_Update = StringUtil.StrToBool( GetPar( "IsAuthorized_Update"));
         AV113IsAuthorized_BtnRole = StringUtil.StrToBool( GetPar( "IsAuthorized_BtnRole"));
         AV114IsAuthorized_BtnPermissions = StringUtil.StrToBool( GetPar( "IsAuthorized_BtnPermissions"));
         AV115IsAuthorized_BtnSetPwd = StringUtil.StrToBool( GetPar( "IsAuthorized_BtnSetPwd"));
         AV62IsAuthorized_Authenticator = StringUtil.StrToBool( GetPar( "IsAuthorized_Authenticator"));
         AV116IsAuthorized_UDelete = StringUtil.StrToBool( GetPar( "IsAuthorized_UDelete"));
         AV117IsAuthorized_ApplicationAPIKeys = StringUtil.StrToBool( GetPar( "IsAuthorized_ApplicationAPIKeys"));
         AV118IsAuthorized_Name = StringUtil.StrToBool( GetPar( "IsAuthorized_Name"));
         AV119IsAuthorized_Email = StringUtil.StrToBool( GetPar( "IsAuthorized_Email"));
         AV120IsAuthorized_Insert = StringUtil.StrToBool( GetPar( "IsAuthorized_Insert"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
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
            return "gamwwusers_Execute" ;
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
         PA192( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START192( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamwwusers.aspx") +"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV123Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV123Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vFIRSTINDEX", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV41FirstIndex), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vFIRSTINDEX", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV41FirstIndex), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV111IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV111IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV112IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV112IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNROLE", AV113IsAuthorized_BtnRole);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNROLE", GetSecureSignedToken( "", AV113IsAuthorized_BtnRole, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNPERMISSIONS", AV114IsAuthorized_BtnPermissions);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNPERMISSIONS", GetSecureSignedToken( "", AV114IsAuthorized_BtnPermissions, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNSETPWD", AV115IsAuthorized_BtnSetPwd);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNSETPWD", GetSecureSignedToken( "", AV115IsAuthorized_BtnSetPwd, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_AUTHENTICATOR", AV62IsAuthorized_Authenticator);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_AUTHENTICATOR", GetSecureSignedToken( "", AV62IsAuthorized_Authenticator, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UDELETE", AV116IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( "", AV116IsAuthorized_UDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_APPLICATIONAPIKEYS", AV117IsAuthorized_ApplicationAPIKeys);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_APPLICATIONAPIKEYS", GetSecureSignedToken( "", AV117IsAuthorized_ApplicationAPIKeys, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_NAME", AV118IsAuthorized_Name);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_NAME", GetSecureSignedToken( "", AV118IsAuthorized_Name, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_EMAIL", AV119IsAuthorized_Email);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_EMAIL", GetSecureSignedToken( "", AV119IsAuthorized_Email, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV120IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV120IsAuthorized_Insert, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_60", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_60), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vMANAGEFILTERSDATA", AV77ManageFiltersData);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vMANAGEFILTERSDATA", AV77ManageFiltersData);
         }
         GxWebStd.gx_hidden_field( context, "vGRIDCURRENTPAGE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV51GridCurrentPage), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV53GridPageCount), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDAPPLIEDFILTERS", AV50GridAppliedFilters);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV27DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV27DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCOLUMNSSELECTOR", AV17ColumnsSelector);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCOLUMNSSELECTOR", AV17ColumnsSelector);
         }
         GxWebStd.gx_hidden_field( context, "vMANAGEFILTERSEXECUTIONSTEP", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV79ManageFiltersExecutionStep), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV123Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV123Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vFIRSTINDEX", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV41FirstIndex), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vFIRSTINDEX", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV41FirstIndex), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV111IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV111IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV112IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV112IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNROLE", AV113IsAuthorized_BtnRole);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNROLE", GetSecureSignedToken( "", AV113IsAuthorized_BtnRole, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNPERMISSIONS", AV114IsAuthorized_BtnPermissions);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNPERMISSIONS", GetSecureSignedToken( "", AV114IsAuthorized_BtnPermissions, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNSETPWD", AV115IsAuthorized_BtnSetPwd);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNSETPWD", GetSecureSignedToken( "", AV115IsAuthorized_BtnSetPwd, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_AUTHENTICATOR", AV62IsAuthorized_Authenticator);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_AUTHENTICATOR", GetSecureSignedToken( "", AV62IsAuthorized_Authenticator, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UDELETE", AV116IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( "", AV116IsAuthorized_UDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_APPLICATIONAPIKEYS", AV117IsAuthorized_ApplicationAPIKeys);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_APPLICATIONAPIKEYS", GetSecureSignedToken( "", AV117IsAuthorized_ApplicationAPIKeys, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_NAME", AV118IsAuthorized_Name);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_NAME", GetSecureSignedToken( "", AV118IsAuthorized_Name, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_EMAIL", AV119IsAuthorized_Email);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_EMAIL", GetSecureSignedToken( "", AV119IsAuthorized_Email, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGRIDSTATE", AV56GridState);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGRIDSTATE", AV56GridState);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV120IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV120IsAuthorized_Insert, context));
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "subGrid_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
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
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Caption", StringUtil.RTrim( Ddo_grid_Caption));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Gridinternalname", StringUtil.RTrim( Ddo_grid_Gridinternalname));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Columnids", StringUtil.RTrim( Ddo_grid_Columnids));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Columnssortvalues", StringUtil.RTrim( Ddo_grid_Columnssortvalues));
         GxWebStd.gx_hidden_field( context, "DDO_GRID_Fixable", StringUtil.RTrim( Ddo_grid_Fixable));
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
         GxWebStd.gx_hidden_field( context, "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues", StringUtil.RTrim( Ddo_gridcolumnsselector_Columnsselectorvalues));
         GxWebStd.gx_hidden_field( context, "DDO_MANAGEFILTERS_Activeeventkey", StringUtil.RTrim( Ddo_managefilters_Activeeventkey));
         GxWebStd.gx_hidden_field( context, "vBTNROLE_Title", StringUtil.RTrim( edtavBtnrole_Title));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
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
            WE192( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT192( ) ;
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
         return formatLink("gamwwusers.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "GAMWWUsers" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "WWP_GAM_Users", "") ;
      }

      protected void WB190( )
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMainWithShadow", "start", "top", "", "", "div");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            ClassString = "ErrorViewer";
            StyleString = "";
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, StyleString, ClassString, "", "false");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingBottom", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheader_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableactions_Internalname, 1, 0, "px", 0, "px", "TableCellsWidthAuto", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroupColorFilledActions", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtninsert_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(60), 2, 0)+","+"null"+");", context.GetMessage( "GXM_insert", ""), bttBtninsert_Jsonclick, 5, context.GetMessage( "GXM_insert", ""), "", StyleString, ClassString, bttBtninsert_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOINSERT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWWUsers.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 27,'',false,'',0)\"";
            ClassString = "hidden-xs";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtneditcolumns_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(60), 2, 0)+","+"null"+");", context.GetMessage( "WWP_EditColumnsCaption", ""), bttBtneditcolumns_Jsonclick, 0, context.GetMessage( "WWP_EditColumnsTooltip", ""), "", StyleString, ClassString, 1, 0, "standard", "'"+""+"'"+",false,"+"'"+""+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWWUsers.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-8", "end", "top", "", "", "div");
            wb_table1_29_192( true) ;
         }
         else
         {
            wb_table1_29_192( false) ;
         }
         return  ;
      }

      protected void wb_table1_29_192e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SectionGrid GridNoBorderCell CellMarginTop HasGridEmpowerer", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGridtablewithpaginationbar_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridContainer.SetWrapped(nGXWrapped);
            StartGridControl60( ) ;
         }
         if ( wbEnd == 60 )
         {
            wbEnd = 0;
            nRC_GXsfl_60 = (int)(nGXsfl_60_idx-1);
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
            ucGridpaginationbar.SetProperty("CurrentPage", AV51GridCurrentPage);
            ucGridpaginationbar.SetProperty("PageCount", AV53GridPageCount);
            ucGridpaginationbar.SetProperty("AppliedFilters", AV50GridAppliedFilters);
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
            ucDdo_grid.SetProperty("Caption", Ddo_grid_Caption);
            ucDdo_grid.SetProperty("ColumnIds", Ddo_grid_Columnids);
            ucDdo_grid.SetProperty("ColumnsSortValues", Ddo_grid_Columnssortvalues);
            ucDdo_grid.SetProperty("Fixable", Ddo_grid_Fixable);
            ucDdo_grid.SetProperty("DropDownOptionsTitleSettingsIcons", AV27DDO_TitleSettingsIcons);
            ucDdo_grid.Render(context, "dvelop.gxbootstrap.ddogridtitlesettingsm", Ddo_grid_Internalname, "DDO_GRIDContainer");
            /* User Defined Control */
            ucDdo_gridcolumnsselector.SetProperty("IconType", Ddo_gridcolumnsselector_Icontype);
            ucDdo_gridcolumnsselector.SetProperty("Icon", Ddo_gridcolumnsselector_Icon);
            ucDdo_gridcolumnsselector.SetProperty("Caption", Ddo_gridcolumnsselector_Caption);
            ucDdo_gridcolumnsselector.SetProperty("Tooltip", Ddo_gridcolumnsselector_Tooltip);
            ucDdo_gridcolumnsselector.SetProperty("Cls", Ddo_gridcolumnsselector_Cls);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsType", Ddo_gridcolumnsselector_Dropdownoptionstype);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsTitleSettingsIcons", AV27DDO_TitleSettingsIcons);
            ucDdo_gridcolumnsselector.SetProperty("DropDownOptionsData", AV17ColumnsSelector);
            ucDdo_gridcolumnsselector.Render(context, "dvelop.gxbootstrap.ddogridcolumnsselector", Ddo_gridcolumnsselector_Internalname, "DDO_GRIDCOLUMNSSELECTORContainer");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 80,'',false,'" + sGXsfl_60_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGamuserscount_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV47GAMUsersCount), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV47GAMUsersCount), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,80);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGamuserscount_Jsonclick, 0, "Attribute", "", "", "", "", edtavGamuserscount_Visible, 1, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWWUsers.htm");
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
         if ( wbEnd == 60 )
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

      protected void START192( )
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
         Form.Meta.addItem("description", context.GetMessage( "WWP_GAM_Users", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP190( ) ;
      }

      protected void WS192( )
      {
         START192( ) ;
         EVT192( ) ;
      }

      protected void EVT192( )
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
                              E11192 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changepage */
                              E12192 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changerowsperpage */
                              E13192 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Ddo_gridcolumnsselector.Oncolumnschanged */
                              E14192 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOINSERT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoInsert' */
                              E15192 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "GRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "VGRIDACTIONGROUP1.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "VGRIDACTIONGROUP1.CLICK") == 0 ) )
                           {
                              nGXsfl_60_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_60_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_60_idx), 4, 0), 4, "0");
                              SubsflControlProps_602( ) ;
                              AV29Display = cgiGet( edtavDisplay_Internalname);
                              AssignAttri("", false, edtavDisplay_Internalname, AV29Display);
                              AV99Update = cgiGet( edtavUpdate_Internalname);
                              AssignAttri("", false, edtavUpdate_Internalname, AV99Update);
                              AV12BtnRole = cgiGet( edtavBtnrole_Internalname);
                              AssignAttri("", false, edtavBtnrole_Internalname, AV12BtnRole);
                              cmbavGridactiongroup1.Name = cmbavGridactiongroup1_Internalname;
                              cmbavGridactiongroup1.CurrentValue = cgiGet( cmbavGridactiongroup1_Internalname);
                              AV48GridActionGroup1 = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavGridactiongroup1_Internalname), "."), 18, MidpointRounding.ToEven));
                              AssignAttri("", false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV48GridActionGroup1), 4, 0));
                              AV83Name = cgiGet( edtavName_Internalname);
                              AssignAttri("", false, edtavName_Internalname, AV83Name);
                              AV30Email = cgiGet( edtavEmail_Internalname);
                              AssignAttri("", false, edtavEmail_Internalname, AV30Email);
                              AV42FirstName = cgiGet( edtavFirstname_Internalname);
                              AssignAttri("", false, edtavFirstname_Internalname, AV42FirstName);
                              AV75LastName = cgiGet( edtavLastname_Internalname);
                              AssignAttri("", false, edtavLastname_Internalname, AV75LastName);
                              AV8AuthenticationTypeName = cgiGet( edtavAuthenticationtypename_Internalname);
                              AssignAttri("", false, edtavAuthenticationtypename_Internalname, AV8AuthenticationTypeName);
                              AV58GUID = cgiGet( edtavGuid_Internalname);
                              AssignAttri("", false, edtavGuid_Internalname, AV58GUID);
                              AV94Status = cgiGet( edtavStatus_Internalname);
                              AssignAttri("", false, edtavStatus_Internalname, AV94Status);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E16192 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E17192 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid.Load */
                                    E18192 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VGRIDACTIONGROUP1.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E19192 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
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

      protected void WE192( )
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

      protected void PA192( )
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
               GX_FocusControl = edtavSearch_Internalname;
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
         SubsflControlProps_602( ) ;
         while ( nGXsfl_60_idx <= nRC_GXsfl_60 )
         {
            sendrow_602( ) ;
            nGXsfl_60_idx = ((subGrid_Islastpage==1)&&(nGXsfl_60_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_60_idx+1);
            sGXsfl_60_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_60_idx), 4, 0), 4, "0");
            SubsflControlProps_602( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridContainer)) ;
         /* End function gxnrGrid_newrow */
      }

      protected void gxgrGrid_refresh( int subGrid_Rows ,
                                       short AV79ManageFiltersExecutionStep ,
                                       DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV17ColumnsSelector ,
                                       string AV123Pgmname ,
                                       string AV40FilUserGender ,
                                       string AV35FilAutType ,
                                       string AV91Search ,
                                       long AV36FilRol ,
                                       short AV41FirstIndex ,
                                       bool AV111IsAuthorized_Display ,
                                       bool AV112IsAuthorized_Update ,
                                       bool AV113IsAuthorized_BtnRole ,
                                       bool AV114IsAuthorized_BtnPermissions ,
                                       bool AV115IsAuthorized_BtnSetPwd ,
                                       bool AV62IsAuthorized_Authenticator ,
                                       bool AV116IsAuthorized_UDelete ,
                                       bool AV117IsAuthorized_ApplicationAPIKeys ,
                                       bool AV118IsAuthorized_Name ,
                                       bool AV119IsAuthorized_Email ,
                                       bool AV120IsAuthorized_Insert )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RF192( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
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
         if ( cmbavFilusergender.ItemCount > 0 )
         {
            AV40FilUserGender = cmbavFilusergender.getValidValue(AV40FilUserGender);
            AssignAttri("", false, "AV40FilUserGender", AV40FilUserGender);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavFilusergender.CurrentValue = StringUtil.RTrim( AV40FilUserGender);
            AssignProp("", false, cmbavFilusergender_Internalname, "Values", cmbavFilusergender.ToJavascriptSource(), true);
         }
         if ( cmbavFilauttype.ItemCount > 0 )
         {
            AV35FilAutType = cmbavFilauttype.getValidValue(AV35FilAutType);
            AssignAttri("", false, "AV35FilAutType", AV35FilAutType);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavFilauttype.CurrentValue = StringUtil.RTrim( AV35FilAutType);
            AssignProp("", false, cmbavFilauttype_Internalname, "Values", cmbavFilauttype.ToJavascriptSource(), true);
         }
         if ( cmbavFilrol.ItemCount > 0 )
         {
            AV36FilRol = (long)(Math.Round(NumberUtil.Val( cmbavFilrol.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV36FilRol), 12, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV36FilRol", StringUtil.LTrimStr( (decimal)(AV36FilRol), 12, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavFilrol.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV36FilRol), 12, 0));
            AssignProp("", false, cmbavFilrol_Internalname, "Values", cmbavFilrol.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF192( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV123Pgmname = "GAMWWUsers";
         edtavDisplay_Enabled = 0;
         edtavUpdate_Enabled = 0;
         edtavBtnrole_Enabled = 0;
         edtavName_Enabled = 0;
         edtavEmail_Enabled = 0;
         edtavFirstname_Enabled = 0;
         edtavLastname_Enabled = 0;
         edtavAuthenticationtypename_Enabled = 0;
         edtavGuid_Enabled = 0;
         edtavStatus_Enabled = 0;
      }

      protected void RF192( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 60;
         /* Execute user event: Refresh */
         E17192 ();
         nGXsfl_60_idx = 1;
         sGXsfl_60_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_60_idx), 4, 0), 4, "0");
         SubsflControlProps_602( ) ;
         bGXsfl_60_Refreshing = true;
         GridContainer.AddObjectProperty("GridName", "Grid");
         GridContainer.AddObjectProperty("CmpContext", "");
         GridContainer.AddObjectProperty("InMasterPage", "false");
         GridContainer.AddObjectProperty("Class", "GridWithPaginationBar GridNoBorder WorkWith");
         GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
         GridContainer.AddObjectProperty("Sortable", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Sortable), 1, 0, ".", "")));
         GridContainer.PageSize = subGrid_fnc_Recordsperpage( );
         if ( subGrid_Islastpage != 0 )
         {
            GRID_nFirstRecordOnPage = (long)(subGrid_fnc_Recordcount( )-subGrid_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
            GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_602( ) ;
            /* Execute user event: Grid.Load */
            E18192 ();
            if ( ( subGrid_Islastpage == 0 ) && ( GRID_nCurrentRecord > 0 ) && ( GRID_nGridOutOfScope == 0 ) && ( nGXsfl_60_idx == 1 ) )
            {
               GRID_nCurrentRecord = 0;
               GRID_nGridOutOfScope = 1;
               subgrid_firstpage( ) ;
               /* Execute user event: Grid.Load */
               E18192 ();
            }
            wbEnd = 60;
            WB190( ) ;
         }
         bGXsfl_60_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes192( )
      {
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV123Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV123Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vFIRSTINDEX", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV41FirstIndex), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vFIRSTINDEX", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV41FirstIndex), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_DISPLAY", AV111IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV111IsAuthorized_Display, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UPDATE", AV112IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV112IsAuthorized_Update, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNROLE", AV113IsAuthorized_BtnRole);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNROLE", GetSecureSignedToken( "", AV113IsAuthorized_BtnRole, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNPERMISSIONS", AV114IsAuthorized_BtnPermissions);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNPERMISSIONS", GetSecureSignedToken( "", AV114IsAuthorized_BtnPermissions, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_BTNSETPWD", AV115IsAuthorized_BtnSetPwd);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNSETPWD", GetSecureSignedToken( "", AV115IsAuthorized_BtnSetPwd, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_AUTHENTICATOR", AV62IsAuthorized_Authenticator);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_AUTHENTICATOR", GetSecureSignedToken( "", AV62IsAuthorized_Authenticator, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_UDELETE", AV116IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( "", AV116IsAuthorized_UDelete, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_APPLICATIONAPIKEYS", AV117IsAuthorized_ApplicationAPIKeys);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_APPLICATIONAPIKEYS", GetSecureSignedToken( "", AV117IsAuthorized_ApplicationAPIKeys, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_NAME", AV118IsAuthorized_Name);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_NAME", GetSecureSignedToken( "", AV118IsAuthorized_Name, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_EMAIL", AV119IsAuthorized_Email);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_EMAIL", GetSecureSignedToken( "", AV119IsAuthorized_Email, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_INSERT", AV120IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV120IsAuthorized_Insert, context));
      }

      protected int subGrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid_fnc_Recordcount( )
      {
         return (int)(((subGrid_Recordcount==0) ? GRID_nFirstRecordOnPage+1 : subGrid_Recordcount)) ;
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
         return (int)(((subGrid_Islastpage==1) ? NumberUtil.Int( (long)(Math.Round(subGrid_fnc_Recordcount( )/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+((((int)((subGrid_fnc_Recordcount( )) % (subGrid_fnc_Recordsperpage( ))))==0) ? 0 : 1) : NumberUtil.Int( (long)(Math.Round(GRID_nFirstRecordOnPage/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1)) ;
      }

      protected short subgrid_firstpage( )
      {
         GRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_nextpage( )
      {
         if ( GRID_nEOF == 0 )
         {
            GRID_nFirstRecordOnPage = (long)(GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( ));
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgrid_previouspage( )
      {
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
            gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         subGrid_Islastpage = 1;
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgrid_gotopage( int nPageNo )
      {
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
            gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV123Pgmname = "GAMWWUsers";
         edtavDisplay_Enabled = 0;
         edtavUpdate_Enabled = 0;
         edtavBtnrole_Enabled = 0;
         edtavName_Enabled = 0;
         edtavEmail_Enabled = 0;
         edtavFirstname_Enabled = 0;
         edtavLastname_Enabled = 0;
         edtavAuthenticationtypename_Enabled = 0;
         edtavGuid_Enabled = 0;
         edtavStatus_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP190( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E16192 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vMANAGEFILTERSDATA"), AV77ManageFiltersData);
            ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV27DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( "vCOLUMNSSELECTOR"), AV17ColumnsSelector);
            /* Read saved values. */
            nRC_GXsfl_60 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_60"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV51GridCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDCURRENTPAGE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV53GridPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDPAGECOUNT"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV50GridAppliedFilters = cgiGet( "vGRIDAPPLIEDFILTERS");
            GRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGrid_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "subGrid_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
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
            Ddo_grid_Caption = cgiGet( "DDO_GRID_Caption");
            Ddo_grid_Gridinternalname = cgiGet( "DDO_GRID_Gridinternalname");
            Ddo_grid_Columnids = cgiGet( "DDO_GRID_Columnids");
            Ddo_grid_Columnssortvalues = cgiGet( "DDO_GRID_Columnssortvalues");
            Ddo_grid_Fixable = cgiGet( "DDO_GRID_Fixable");
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
            Ddo_gridcolumnsselector_Columnsselectorvalues = cgiGet( "DDO_GRIDCOLUMNSSELECTOR_Columnsselectorvalues");
            Ddo_managefilters_Activeeventkey = cgiGet( "DDO_MANAGEFILTERS_Activeeventkey");
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            /* Read variables values. */
            AV91Search = cgiGet( edtavSearch_Internalname);
            AssignAttri("", false, "AV91Search", AV91Search);
            cmbavFilusergender.Name = cmbavFilusergender_Internalname;
            cmbavFilusergender.CurrentValue = cgiGet( cmbavFilusergender_Internalname);
            AV40FilUserGender = cgiGet( cmbavFilusergender_Internalname);
            AssignAttri("", false, "AV40FilUserGender", AV40FilUserGender);
            cmbavFilauttype.Name = cmbavFilauttype_Internalname;
            cmbavFilauttype.CurrentValue = cgiGet( cmbavFilauttype_Internalname);
            AV35FilAutType = cgiGet( cmbavFilauttype_Internalname);
            AssignAttri("", false, "AV35FilAutType", AV35FilAutType);
            cmbavFilrol.Name = cmbavFilrol_Internalname;
            cmbavFilrol.CurrentValue = cgiGet( cmbavFilrol_Internalname);
            AV36FilRol = (long)(Math.Round(NumberUtil.Val( cgiGet( cmbavFilrol_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV36FilRol", StringUtil.LTrimStr( (decimal)(AV36FilRol), 12, 0));
            if ( ( ( context.localUtil.CToN( cgiGet( edtavGamuserscount_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavGamuserscount_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vGAMUSERSCOUNT");
               GX_FocusControl = edtavGamuserscount_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV47GAMUsersCount = 0;
               AssignAttri("", false, "AV47GAMUsersCount", StringUtil.LTrimStr( (decimal)(AV47GAMUsersCount), 4, 0));
            }
            else
            {
               AV47GAMUsersCount = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavGamuserscount_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV47GAMUsersCount", StringUtil.LTrimStr( (decimal)(AV47GAMUsersCount), 4, 0));
            }
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
         E16192 ();
         if (returnInSub) return;
      }

      protected void E16192( )
      {
         /* Start Routine */
         returnInSub = false;
         Gridpaginationbar_Caption = context.GetMessage( "WWP_GAM_WWUsers_PagingCaption", "");
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "Caption", Gridpaginationbar_Caption);
         Gridpaginationbar_Pagestoshow = 0;
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "PagesToShow", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Pagestoshow), 9, 0));
         Gridpaginationbar_Showlast = false;
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "ShowLast", StringUtil.BoolToStr( Gridpaginationbar_Showlast));
         edtavGamuserscount_Visible = 0;
         AssignProp("", false, edtavGamuserscount_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGamuserscount_Visible), 5, 0), true);
         subGrid_Rows = 10;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         Grid_empowerer_Gridinternalname = subGrid_Internalname;
         ucGrid_empowerer.SendProperty(context, "", false, Grid_empowerer_Internalname, "GridInternalName", Grid_empowerer_Gridinternalname);
         Ddo_gridcolumnsselector_Gridinternalname = subGrid_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, "", false, Ddo_gridcolumnsselector_Internalname, "GridInternalName", Ddo_gridcolumnsselector_Gridinternalname);
         if ( StringUtil.StrCmp(AV59HTTPRequest.Method, "GET") == 0 )
         {
            /* Execute user subroutine: 'LOADSAVEDFILTERS' */
            S112 ();
            if (returnInSub) return;
         }
         GXt_boolean1 = AV118IsAuthorized_Name;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamuserentry_Execute", out  GXt_boolean1) ;
         AV118IsAuthorized_Name = GXt_boolean1;
         AssignAttri("", false, "AV118IsAuthorized_Name", AV118IsAuthorized_Name);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_NAME", GetSecureSignedToken( "", AV118IsAuthorized_Name, context));
         GXt_boolean1 = AV119IsAuthorized_Email;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamuserentry_Execute", out  GXt_boolean1) ;
         AV119IsAuthorized_Email = GXt_boolean1;
         AssignAttri("", false, "AV119IsAuthorized_Email", AV119IsAuthorized_Email);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_EMAIL", GetSecureSignedToken( "", AV119IsAuthorized_Email, context));
         Ddo_grid_Gridinternalname = subGrid_Internalname;
         ucDdo_grid.SendProperty(context, "", false, Ddo_grid_Internalname, "GridInternalName", Ddo_grid_Gridinternalname);
         Form.Caption = context.GetMessage( "WWP_GAM_Users", "");
         AssignProp("", false, "FORM", "Caption", Form.Caption, true);
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S122 ();
         if (returnInSub) return;
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2 = AV27DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2) ;
         AV27DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2;
         Ddo_gridcolumnsselector_Titlecontrolidtoreplace = bttBtneditcolumns_Internalname;
         ucDdo_gridcolumnsselector.SendProperty(context, "", false, Ddo_gridcolumnsselector_Internalname, "TitleControlIdToReplace", Ddo_gridcolumnsselector_Titlecontrolidtoreplace);
         Gridpaginationbar_Rowsperpageselectedvalue = subGrid_Rows;
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0));
         cmbavFilauttype.removeAllItems();
         cmbavFilauttype.addItem("", context.GetMessage( "WWP_GAM_All", ""), 0);
         AV9AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getauthenticationtypes(AV38FilterAutType, out  AV33Errors);
         AV121GXV1 = 1;
         while ( AV121GXV1 <= AV9AuthenticationTypes.Count )
         {
            AV7AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType)AV9AuthenticationTypes.Item(AV121GXV1));
            cmbavFilauttype.addItem(AV7AuthenticationType.gxTpr_Name, AV7AuthenticationType.gxTpr_Description, 0);
            AV121GXV1 = (int)(AV121GXV1+1);
         }
         cmbavFilrol.removeAllItems();
         cmbavFilrol.addItem("0", context.GetMessage( "WWP_GAM_All", ""), 0);
         cmbavFilrol.addItem("-1", context.GetMessage( "WWP_GAM_NoRole", ""), 0);
         AV90Roles = AV88Repository.getroles(AV39FilterRoles, out  AV33Errors);
         AV122GXV2 = 1;
         while ( AV122GXV2 <= AV90Roles.Count )
         {
            AV89Role = ((GeneXus.Programs.genexussecurity.SdtGAMRole)AV90Roles.Item(AV122GXV2));
            cmbavFilrol.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(AV89Role.gxTpr_Id), 12, 0)), AV89Role.gxTpr_Name, 0);
            AV122GXV2 = (int)(AV122GXV2+1);
         }
         edtavBtnrole_Title = context.GetMessage( "WWP_GAM_Roles", "");
         AssignProp("", false, edtavBtnrole_Internalname, "Title", edtavBtnrole_Title, !bGXsfl_60_Refreshing);
         AV88Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
      }

      protected void E17192( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV103WWPContext) ;
         /* Execute user subroutine: 'CHECKSECURITYFORACTIONS' */
         S132 ();
         if (returnInSub) return;
         if ( AV79ManageFiltersExecutionStep == 1 )
         {
            AV79ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV79ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV79ManageFiltersExecutionStep), 1, 0));
         }
         else if ( AV79ManageFiltersExecutionStep == 2 )
         {
            AV79ManageFiltersExecutionStep = 0;
            AssignAttri("", false, "AV79ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV79ManageFiltersExecutionStep), 1, 0));
            /* Execute user subroutine: 'LOADSAVEDFILTERS' */
            S112 ();
            if (returnInSub) return;
         }
         /* Execute user subroutine: 'SAVEGRIDSTATE' */
         S142 ();
         if (returnInSub) return;
         if ( StringUtil.StrCmp(AV93Session.Get("GAMWWUsersColumnsSelector"), "") != 0 )
         {
            AV19ColumnsSelectorXML = AV93Session.Get("GAMWWUsersColumnsSelector");
            AV17ColumnsSelector.FromXml(AV19ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S152 ();
            if (returnInSub) return;
         }
         edtavName_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV17ColumnsSelector.gxTpr_Columns.Item(1)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtavName_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavName_Visible), 5, 0), !bGXsfl_60_Refreshing);
         edtavEmail_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV17ColumnsSelector.gxTpr_Columns.Item(2)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtavEmail_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmail_Visible), 5, 0), !bGXsfl_60_Refreshing);
         edtavFirstname_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV17ColumnsSelector.gxTpr_Columns.Item(3)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtavFirstname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavFirstname_Visible), 5, 0), !bGXsfl_60_Refreshing);
         edtavLastname_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV17ColumnsSelector.gxTpr_Columns.Item(4)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtavLastname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavLastname_Visible), 5, 0), !bGXsfl_60_Refreshing);
         edtavAuthenticationtypename_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV17ColumnsSelector.gxTpr_Columns.Item(5)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtavAuthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavAuthenticationtypename_Visible), 5, 0), !bGXsfl_60_Refreshing);
         edtavStatus_Visible = (((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV17ColumnsSelector.gxTpr_Columns.Item(6)).gxTpr_Isvisible ? 1 : 0);
         AssignProp("", false, edtavStatus_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavStatus_Visible), 5, 0), !bGXsfl_60_Refreshing);
         AV51GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri("", false, "AV51GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV51GridCurrentPage), 10, 0));
         GXt_char3 = AV50GridAppliedFilters;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getappliedfiltersdescription(context ).execute(  AV123Pgmname, out  GXt_char3) ;
         AV50GridAppliedFilters = GXt_char3;
         AssignAttri("", false, "AV50GridAppliedFilters", AV50GridAppliedFilters);
         edtavStatus_Columnheaderclass = "WWColumn";
         AssignProp("", false, edtavStatus_Internalname, "Columnheaderclass", edtavStatus_Columnheaderclass, !bGXsfl_60_Refreshing);
         AV51GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri("", false, "AV51GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV51GridCurrentPage), 10, 0));
         AV54GridPageSize = subGrid_Rows;
         AV37Filter.gxTpr_Gender = AV40FilUserGender;
         AV37Filter.gxTpr_Authenticationtypename = AV35FilAutType;
         AV37Filter.gxTpr_Loadcustomattributes = false;
         AV37Filter.gxTpr_Returnanonymoususer = false;
         AV37Filter.gxTpr_Limit = (int)(AV54GridPageSize+1);
         AV41FirstIndex = (short)((AV51GridCurrentPage-1)*AV54GridPageSize+1);
         AssignAttri("", false, "AV41FirstIndex", StringUtil.LTrimStr( (decimal)(AV41FirstIndex), 4, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vFIRSTINDEX", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV41FirstIndex), "ZZZ9"), context));
         AV37Filter.gxTpr_Start = AV41FirstIndex-1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV91Search)) )
         {
            AV37Filter.gxTpr_Searchable = "%"+AV91Search;
         }
         if ( AV36FilRol == -1 )
         {
            AV37Filter.gxTpr_Withoutroles = true;
         }
         else
         {
            AV37Filter.gxTpr_Roleid = AV36FilRol;
         }
         AV46GAMUsers = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getusersorderby(ref  AV37Filter, 0, out  AV33Errors);
         if ( cmbavFilauttype.ItemCount == 2 )
         {
            edtavAuthenticationtypename_Visible = 0;
            AssignProp("", false, edtavAuthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavAuthenticationtypename_Visible), 5, 0), !bGXsfl_60_Refreshing);
         }
         if ( AV46GAMUsers.Count == AV37Filter.gxTpr_Limit )
         {
            AV55GridRecordCount = (long)(AV51GridCurrentPage*AV54GridPageSize+1);
            AV53GridPageCount = (long)(AV51GridCurrentPage+1);
            AssignAttri("", false, "AV53GridPageCount", StringUtil.LTrimStr( (decimal)(AV53GridPageCount), 10, 0));
         }
         else
         {
            AV55GridRecordCount = (long)((AV51GridCurrentPage-1)*AV54GridPageSize+AV46GAMUsers.Count);
            AV53GridPageCount = AV51GridCurrentPage;
            AssignAttri("", false, "AV53GridPageCount", StringUtil.LTrimStr( (decimal)(AV53GridPageCount), 10, 0));
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17ColumnsSelector", AV17ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV37Filter", AV37Filter);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV77ManageFiltersData", AV77ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV56GridState", AV56GridState);
      }

      protected void E12192( )
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
            AV85PageToGo = (int)(Math.Round(NumberUtil.Val( Gridpaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            subgrid_gotopage( AV85PageToGo) ;
         }
      }

      protected void E13192( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      private void E18192( )
      {
         /* Grid_Load Routine */
         returnInSub = false;
         AV60i = 1;
         while ( AV60i <= AV41FirstIndex - 1 )
         {
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 60;
            }
            if ( ( subGrid_Islastpage == 1 ) || ( subGrid_Rows == 0 ) || ( ( GRID_nCurrentRecord >= GRID_nFirstRecordOnPage ) && ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_602( ) ;
            }
            GRID_nEOF = (short)(((GRID_nCurrentRecord<GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
            subGrid_Recordcount = (int)(GRID_nCurrentRecord);
            if ( isFullAjaxMode( ) && ! bGXsfl_60_Refreshing )
            {
               DoAjaxLoad(60, GridRow);
            }
            AV60i = (long)(AV60i+1);
         }
         AV124GXV3 = 1;
         while ( AV124GXV3 <= AV46GAMUsers.Count )
         {
            AV100User = ((GeneXus.Programs.genexussecurity.SdtGAMUser)AV46GAMUsers.Item(AV124GXV3));
            AV8AuthenticationTypeName = AV100User.gxTpr_Authenticationtypename;
            AssignAttri("", false, edtavAuthenticationtypename_Internalname, AV8AuthenticationTypeName);
            AV58GUID = AV100User.gxTpr_Guid;
            AssignAttri("", false, edtavGuid_Internalname, AV58GUID);
            AV83Name = AV100User.gxTpr_Name;
            AssignAttri("", false, edtavName_Internalname, AV83Name);
            AV42FirstName = AV100User.gxTpr_Firstname;
            AssignAttri("", false, edtavFirstname_Internalname, AV42FirstName);
            AV75LastName = AV100User.gxTpr_Lastname;
            AssignAttri("", false, edtavLastname_Internalname, AV75LastName);
            AV94Status = AV100User.gxTpr_Status;
            AssignAttri("", false, edtavStatus_Internalname, AV94Status);
            AV30Email = AV100User.gxTpr_Email;
            AssignAttri("", false, edtavEmail_Internalname, AV30Email);
            if ( AV100User.gxTpr_Isenabledinrepository )
            {
               edtavName_Class = "Attribute";
               edtavFirstname_Class = "Attribute";
               edtavLastname_Class = "Attribute";
               edtavAuthenticationtypename_Class = "Attribute";
               edtavEmail_Class = "Attribute";
            }
            else
            {
               edtavName_Class = "AttributeInactive";
               edtavFirstname_Class = "AttributeInactive";
               edtavLastname_Class = "AttributeInactive";
               edtavAuthenticationtypename_Class = "AttributeInactive";
               edtavEmail_Class = "AttributeInactive";
            }
            AV104GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).getbyguid(AV100User.gxTpr_Guid, out  AV5GAMErrorCollection);
            AV29Display = "<i class=\"fa fa-search\"></i>";
            AssignAttri("", false, edtavDisplay_Internalname, AV29Display);
            if ( AV111IsAuthorized_Display )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "gamuserentry.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
               edtavDisplay_Link = formatLink("gamuserentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
            }
            AV99Update = "<i class=\"fa fa-pen\"></i>";
            AssignAttri("", false, edtavUpdate_Internalname, AV99Update);
            if ( AV112IsAuthorized_Update )
            {
               if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV100User.gxTpr_Guid), StringUtil.Trim( AV88Repository.gxTpr_Anonymoususerguid)) == 0 ) && ! AV100User.gxTpr_Isautoregistereduser )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamuserentry.aspx"+UrlEncode(StringUtil.RTrim("UPD")) + "," + UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
                  edtavUpdate_Link = formatLink("gamuserentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
                  edtavUpdate_Class = "Attribute";
               }
               else
               {
                  edtavUpdate_Link = "";
                  edtavUpdate_Class = "Invisible";
               }
            }
            AV12BtnRole = "<i class=\"fa fa-cog\"></i>";
            AssignAttri("", false, edtavBtnrole_Internalname, AV12BtnRole);
            if ( AV113IsAuthorized_BtnRole )
            {
               if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV100User.gxTpr_Guid), StringUtil.Trim( AV88Repository.gxTpr_Anonymoususerguid)) == 0 ) && ! AV100User.gxTpr_Isautoregistereduser )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamwwuserroles.aspx"+UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
                  edtavBtnrole_Link = formatLink("gamwwuserroles.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
                  edtavBtnrole_Class = "Attribute";
               }
               else
               {
                  edtavBtnrole_Link = "";
                  edtavBtnrole_Class = "Invisible";
               }
            }
            cmbavGridactiongroup1.removeAllItems();
            cmbavGridactiongroup1.addItem("0", ";fas fa-ellipsis", 0);
            if ( AV114IsAuthorized_BtnPermissions )
            {
               if ( ! ( StringUtil.StrCmp(StringUtil.Trim( AV100User.gxTpr_Guid), StringUtil.Trim( AV88Repository.gxTpr_Anonymoususerguid)) == 0 ) && ! AV100User.gxTpr_Isautoregistereduser )
               {
                  cmbavGridactiongroup1.addItem("1", context.GetMessage( "WWP_GAM_EditPermissions", ""), 0);
               }
            }
            if ( ! AV104GAMUser.gxTpr_Isactive && AV88Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount )
            {
               cmbavGridactiongroup1.addItem("2", context.GetMessage( "WWP_GAM_SendActivationEmail", ""), 0);
            }
            if ( AV115IsAuthorized_BtnSetPwd )
            {
               if ( AV100User.gxTpr_Isactive || ! AV88Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount )
               {
                  cmbavGridactiongroup1.addItem("3", context.GetMessage( "WWP_GAM_ChangePassword", ""), 0);
               }
            }
            if ( AV62IsAuthorized_Authenticator )
            {
               if ( AV88Repository.istotpauthenticatorenabled() && AV104GAMUser.gxTpr_Enabletwofactorauthentication && ( AV104GAMUser.gxTpr_Isactive || ! AV88Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount ) )
               {
                  cmbavGridactiongroup1.addItem("4", context.GetMessage( "WWP_GAM_EnableAuthenticator", ""), 0);
               }
            }
            if ( ( AV100User.gxTpr_Isactive || ! AV88Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount ) && AV88Repository.isonetimepasswordenabled() )
            {
               cmbavGridactiongroup1.addItem("5", context.GetMessage( "WWP_GAM_UnblockOTPCodes", ""), 0);
            }
            if ( AV100User.gxTpr_Isactive || ! AV88Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount )
            {
               cmbavGridactiongroup1.addItem("6", context.GetMessage( "WWP_GAM_BlockUser", ""), 0);
            }
            if ( AV116IsAuthorized_UDelete )
            {
               cmbavGridactiongroup1.addItem("7", context.GetMessage( "WWP_GAM_DeleteUser", ""), 0);
            }
            cmbavGridactiongroup1.addItem("8", context.GetMessage( "WWP_GAM_EnableInRepository", ""), 0);
            cmbavGridactiongroup1.addItem("9", context.GetMessage( "WWP_GAM_KillSessions", ""), 0);
            if ( AV117IsAuthorized_ApplicationAPIKeys )
            {
               cmbavGridactiongroup1.addItem("10", context.GetMessage( "WWP_GAM_ApplicationAPIKeys", ""), 0);
            }
            if ( AV118IsAuthorized_Name )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "gamuserentry.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
               edtavName_Link = formatLink("gamuserentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
            }
            if ( AV119IsAuthorized_Email )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "gamuserentry.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
               edtavEmail_Link = formatLink("gamuserentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
            }
            if ( StringUtil.StrCmp(AV94Status, "Deleted") == 0 )
            {
               edtavStatus_Columnclass = "WWColumn WWColumnTag WWColumnTagInfoLight WWColumnTagInfoLightSingleCell";
            }
            else if ( StringUtil.StrCmp(AV94Status, "Blocked") == 0 )
            {
               edtavStatus_Columnclass = "WWColumn WWColumnTag WWColumnTagDanger WWColumnTagDangerSingleCell";
            }
            else if ( StringUtil.StrCmp(AV94Status, "Inactive") == 0 )
            {
               edtavStatus_Columnclass = "WWColumn WWColumnTag WWColumnTagWarning WWColumnTagWarningSingleCell";
            }
            else if ( StringUtil.StrCmp(AV94Status, "Disabled") == 0 )
            {
               edtavStatus_Columnclass = "WWColumn WWColumnTag WWColumnTagInfo WWColumnTagInfoSingleCell";
            }
            else if ( StringUtil.StrCmp(AV94Status, "Active") == 0 )
            {
               edtavStatus_Columnclass = "WWColumn WWColumnTag WWColumnTagSuccess WWColumnTagSuccessSingleCell";
            }
            else
            {
               edtavStatus_Columnclass = "WWColumn";
            }
            if ( AV62IsAuthorized_Authenticator )
            {
               if ( ( AV88Repository.istotpauthenticatorenabled() && AV100User.gxTpr_Enabletwofactorauthentication && ( AV100User.gxTpr_Isactive || ! AV88Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount ) ) && AV100User.gxTpr_Totpenable )
               {
                  cmbavGridactiongroup1.removeItem(StringUtil.Str( (decimal)(4), 1, 0));
                  cmbavGridactiongroup1.addItem("4", context.GetMessage( "WWP_GAM_DisableAuthenticator", ""), 0);
               }
            }
            if ( AV100User.gxTpr_Isblocked )
            {
               cmbavGridactiongroup1.removeItem(StringUtil.Str( (decimal)(6), 1, 0));
               cmbavGridactiongroup1.addItem("6", context.GetMessage( "WWP_GAM_UnblockUser", ""), 0);
            }
            if ( AV100User.gxTpr_Isenabledinrepository )
            {
               cmbavGridactiongroup1.removeItem(StringUtil.Str( (decimal)(8), 1, 0));
               cmbavGridactiongroup1.addItem("8", context.GetMessage( "WWP_GAM_DisableInRepository", ""), 0);
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 60;
            }
            if ( ( subGrid_Islastpage == 1 ) || ( subGrid_Rows == 0 ) || ( ( GRID_nCurrentRecord >= GRID_nFirstRecordOnPage ) && ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_602( ) ;
            }
            GRID_nEOF = (short)(((GRID_nCurrentRecord<GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
            subGrid_Recordcount = (int)(GRID_nCurrentRecord);
            if ( isFullAjaxMode( ) && ! bGXsfl_60_Refreshing )
            {
               DoAjaxLoad(60, GridRow);
            }
            AV124GXV3 = (int)(AV124GXV3+1);
         }
         /*  Sending Event outputs  */
         cmbavGridactiongroup1.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV48GridActionGroup1), 4, 0));
      }

      protected void E14192( )
      {
         /* Ddo_gridcolumnsselector_Oncolumnschanged Routine */
         returnInSub = false;
         AV19ColumnsSelectorXML = Ddo_gridcolumnsselector_Columnsselectorvalues;
         AV17ColumnsSelector.FromJSonString(AV19ColumnsSelectorXML, null);
         new DesignSystem.Programs.wwpbaseobjects.savecolumnsselectorstate(context ).execute(  "GAMWWUsersColumnsSelector",  (String.IsNullOrEmpty(StringUtil.RTrim( AV19ColumnsSelectorXML)) ? "" : AV17ColumnsSelector.ToXml(false, true, "", ""))) ;
         context.DoAjaxRefresh();
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17ColumnsSelector", AV17ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV37Filter", AV37Filter);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV77ManageFiltersData", AV77ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV56GridState", AV56GridState);
      }

      protected void E11192( )
      {
         /* Ddo_managefilters_Onoptionclicked Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Clean#>") == 0 )
         {
            /* Execute user subroutine: 'CLEANFILTERS' */
            S162 ();
            if (returnInSub) return;
            subgrid_firstpage( ) ;
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Save#>") == 0 )
         {
            /* Execute user subroutine: 'SAVEGRIDSTATE' */
            S142 ();
            if (returnInSub) return;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "wwpbaseobjects.savefilteras.aspx"+UrlEncode(StringUtil.RTrim("GAMWWUsersFilters")) + "," + UrlEncode(StringUtil.RTrim(AV123Pgmname+"GridState"));
            context.PopUp(formatLink("wwpbaseobjects.savefilteras.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV79ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV79ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV79ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else if ( StringUtil.StrCmp(Ddo_managefilters_Activeeventkey, "<#Manage#>") == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "wwpbaseobjects.managefilters.aspx"+UrlEncode(StringUtil.RTrim("GAMWWUsersFilters"));
            context.PopUp(formatLink("wwpbaseobjects.managefilters.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
            AV79ManageFiltersExecutionStep = 2;
            AssignAttri("", false, "AV79ManageFiltersExecutionStep", StringUtil.Str( (decimal)(AV79ManageFiltersExecutionStep), 1, 0));
            context.DoAjaxRefresh();
         }
         else
         {
            GXt_char3 = AV82ManageFiltersXml;
            new DesignSystem.Programs.wwpbaseobjects.getfilterbyname(context ).execute(  "GAMWWUsersFilters",  Ddo_managefilters_Activeeventkey, out  GXt_char3) ;
            AV82ManageFiltersXml = GXt_char3;
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV82ManageFiltersXml)) )
            {
               GX_msglist.addItem(context.GetMessage( "WWP_FilterNotExist", ""));
            }
            else
            {
               /* Execute user subroutine: 'CLEANFILTERS' */
               S162 ();
               if (returnInSub) return;
               new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV123Pgmname+"GridState",  AV82ManageFiltersXml) ;
               AV56GridState.FromXml(AV82ManageFiltersXml, null, "", "");
               /* Execute user subroutine: 'LOADREGFILTERSSTATE' */
               S172 ();
               if (returnInSub) return;
               subgrid_firstpage( ) ;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV56GridState", AV56GridState);
         cmbavFilusergender.CurrentValue = StringUtil.RTrim( AV40FilUserGender);
         AssignProp("", false, cmbavFilusergender_Internalname, "Values", cmbavFilusergender.ToJavascriptSource(), true);
         cmbavFilauttype.CurrentValue = StringUtil.RTrim( AV35FilAutType);
         AssignProp("", false, cmbavFilauttype_Internalname, "Values", cmbavFilauttype.ToJavascriptSource(), true);
         cmbavFilrol.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV36FilRol), 12, 0));
         AssignProp("", false, cmbavFilrol_Internalname, "Values", cmbavFilrol.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17ColumnsSelector", AV17ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV37Filter", AV37Filter);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV77ManageFiltersData", AV77ManageFiltersData);
      }

      protected void E19192( )
      {
         /* Gridactiongroup1_Click Routine */
         returnInSub = false;
         if ( AV48GridActionGroup1 == 1 )
         {
            /* Execute user subroutine: 'DO BTNPERMISSIONS' */
            S182 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 2 )
         {
            /* Execute user subroutine: 'DO SENDACTIVATIONMAIL' */
            S192 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 3 )
         {
            /* Execute user subroutine: 'DO BTNSETPWD' */
            S202 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 4 )
         {
            /* Execute user subroutine: 'DO AUTHENTICATOR' */
            S212 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 5 )
         {
            /* Execute user subroutine: 'DO BTNUNBLOCKOTPCODES' */
            S222 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 6 )
         {
            /* Execute user subroutine: 'DO BLOCKUSER' */
            S232 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 7 )
         {
            /* Execute user subroutine: 'DO UDELETE' */
            S242 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 8 )
         {
            /* Execute user subroutine: 'DO ENABLEDISABLEUSER' */
            S252 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 9 )
         {
            /* Execute user subroutine: 'DO KILLSESSIONS' */
            S262 ();
            if (returnInSub) return;
         }
         else if ( AV48GridActionGroup1 == 10 )
         {
            /* Execute user subroutine: 'DO APPLICATIONAPIKEYS' */
            S272 ();
            if (returnInSub) return;
         }
         AV48GridActionGroup1 = 0;
         AssignAttri("", false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV48GridActionGroup1), 4, 0));
         /*  Sending Event outputs  */
         cmbavGridactiongroup1.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV48GridActionGroup1), 4, 0));
         AssignProp("", false, cmbavGridactiongroup1_Internalname, "Values", cmbavGridactiongroup1.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17ColumnsSelector", AV17ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV37Filter", AV37Filter);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV77ManageFiltersData", AV77ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV56GridState", AV56GridState);
      }

      protected void E15192( )
      {
         /* 'DoInsert' Routine */
         returnInSub = false;
         if ( AV120IsAuthorized_Insert )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamuserentry.aspx"+UrlEncode(StringUtil.RTrim("INS")) + "," + UrlEncode(StringUtil.RTrim(""));
            CallWebObject(formatLink("gamuserentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV17ColumnsSelector", AV17ColumnsSelector);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV37Filter", AV37Filter);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV77ManageFiltersData", AV77ManageFiltersData);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV56GridState", AV56GridState);
      }

      protected void S152( )
      {
         /* 'INITIALIZECOLUMNSSELECTOR' Routine */
         returnInSub = false;
         AV17ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         if ( StringUtil.StrCmp(AV88Repository.gxTpr_Useridentification, "email") != 0 )
         {
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "&Name",  "",  "WWP_GAM_Name",  true,  "") ;
         }
         else
         {
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "",  "",  "",  false,  "") ;
         }
         if ( StringUtil.StrCmp(AV88Repository.gxTpr_Useridentification, "name") != 0 )
         {
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "&Email",  "",  "WWP_GAM_Email",  true,  "") ;
         }
         else
         {
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "",  "",  "",  false,  "") ;
         }
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "&FirstName",  "",  "WWP_GAM_FirstName",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "&LastName",  "",  "WWP_GAM_LastName",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "&AuthenticationTypeName",  "",  "WWP_GAM_Authentication",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV17ColumnsSelector,  "&Status",  "",  "Status",  true,  "") ;
         GXt_char3 = AV101UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "GAMWWUsersColumnsSelector", out  GXt_char3) ;
         AV101UserCustomValue = GXt_char3;
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV101UserCustomValue)) ) )
         {
            AV18ColumnsSelectorAux.FromXml(AV101UserCustomValue, null, "", "");
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnselector_updatecolumns(context ).execute( ref  AV18ColumnsSelectorAux, ref  AV17ColumnsSelector) ;
         }
      }

      protected void S132( )
      {
         /* 'CHECKSECURITYFORACTIONS' Routine */
         returnInSub = false;
         GXt_boolean1 = AV111IsAuthorized_Display;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamuserentry_Execute", out  GXt_boolean1) ;
         AV111IsAuthorized_Display = GXt_boolean1;
         AssignAttri("", false, "AV111IsAuthorized_Display", AV111IsAuthorized_Display);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_DISPLAY", GetSecureSignedToken( "", AV111IsAuthorized_Display, context));
         if ( ! ( AV111IsAuthorized_Display ) )
         {
            edtavDisplay_Visible = 0;
            AssignProp("", false, edtavDisplay_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDisplay_Visible), 5, 0), !bGXsfl_60_Refreshing);
         }
         GXt_boolean1 = AV112IsAuthorized_Update;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamuserentry_Execute", out  GXt_boolean1) ;
         AV112IsAuthorized_Update = GXt_boolean1;
         AssignAttri("", false, "AV112IsAuthorized_Update", AV112IsAuthorized_Update);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UPDATE", GetSecureSignedToken( "", AV112IsAuthorized_Update, context));
         if ( ! ( AV112IsAuthorized_Update ) )
         {
            edtavUpdate_Visible = 0;
            AssignProp("", false, edtavUpdate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUpdate_Visible), 5, 0), !bGXsfl_60_Refreshing);
         }
         GXt_boolean1 = AV113IsAuthorized_BtnRole;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamwwuserroles_Execute", out  GXt_boolean1) ;
         AV113IsAuthorized_BtnRole = GXt_boolean1;
         AssignAttri("", false, "AV113IsAuthorized_BtnRole", AV113IsAuthorized_BtnRole);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNROLE", GetSecureSignedToken( "", AV113IsAuthorized_BtnRole, context));
         if ( ! ( AV113IsAuthorized_BtnRole ) )
         {
            edtavBtnrole_Visible = 0;
            AssignProp("", false, edtavBtnrole_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavBtnrole_Visible), 5, 0), !bGXsfl_60_Refreshing);
         }
         GXt_boolean1 = AV114IsAuthorized_BtnPermissions;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamwwuserpermissions_Execute", out  GXt_boolean1) ;
         AV114IsAuthorized_BtnPermissions = GXt_boolean1;
         AssignAttri("", false, "AV114IsAuthorized_BtnPermissions", AV114IsAuthorized_BtnPermissions);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNPERMISSIONS", GetSecureSignedToken( "", AV114IsAuthorized_BtnPermissions, context));
         GXt_boolean1 = AV115IsAuthorized_BtnSetPwd;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamsetpassword_Execute", out  GXt_boolean1) ;
         AV115IsAuthorized_BtnSetPwd = GXt_boolean1;
         AssignAttri("", false, "AV115IsAuthorized_BtnSetPwd", AV115IsAuthorized_BtnSetPwd);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_BTNSETPWD", GetSecureSignedToken( "", AV115IsAuthorized_BtnSetPwd, context));
         GXt_boolean1 = AV62IsAuthorized_Authenticator;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamusertotpactivation_Execute", out  GXt_boolean1) ;
         AV62IsAuthorized_Authenticator = GXt_boolean1;
         AssignAttri("", false, "AV62IsAuthorized_Authenticator", AV62IsAuthorized_Authenticator);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_AUTHENTICATOR", GetSecureSignedToken( "", AV62IsAuthorized_Authenticator, context));
         GXt_boolean1 = AV116IsAuthorized_UDelete;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamuserentry_Execute", out  GXt_boolean1) ;
         AV116IsAuthorized_UDelete = GXt_boolean1;
         AssignAttri("", false, "AV116IsAuthorized_UDelete", AV116IsAuthorized_UDelete);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_UDELETE", GetSecureSignedToken( "", AV116IsAuthorized_UDelete, context));
         GXt_boolean1 = AV117IsAuthorized_ApplicationAPIKeys;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamexampleuserentry_Execute", out  GXt_boolean1) ;
         AV117IsAuthorized_ApplicationAPIKeys = GXt_boolean1;
         AssignAttri("", false, "AV117IsAuthorized_ApplicationAPIKeys", AV117IsAuthorized_ApplicationAPIKeys);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_APPLICATIONAPIKEYS", GetSecureSignedToken( "", AV117IsAuthorized_ApplicationAPIKeys, context));
         GXt_boolean1 = AV120IsAuthorized_Insert;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "gamuserentry_Execute", out  GXt_boolean1) ;
         AV120IsAuthorized_Insert = GXt_boolean1;
         AssignAttri("", false, "AV120IsAuthorized_Insert", AV120IsAuthorized_Insert);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_INSERT", GetSecureSignedToken( "", AV120IsAuthorized_Insert, context));
         if ( ! ( AV120IsAuthorized_Insert && ( (0==AV88Repository.gxTpr_Authenticationmasterrepositoryid) ) ) )
         {
            bttBtninsert_Visible = 0;
            AssignProp("", false, bttBtninsert_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtninsert_Visible), 5, 0), true);
         }
      }

      protected void S112( )
      {
         /* 'LOADSAVEDFILTERS' Routine */
         returnInSub = false;
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 = AV77ManageFiltersData;
         new DesignSystem.Programs.wwpbaseobjects.wwp_managefiltersloadsavedfilters(context ).execute(  "GAMWWUsersFilters",  "",  "",  false, out  GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4) ;
         AV77ManageFiltersData = GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4;
      }

      protected void S162( )
      {
         /* 'CLEANFILTERS' Routine */
         returnInSub = false;
         AV91Search = "";
         AssignAttri("", false, "AV91Search", AV91Search);
         AV40FilUserGender = "";
         AssignAttri("", false, "AV40FilUserGender", AV40FilUserGender);
         AV35FilAutType = "";
         AssignAttri("", false, "AV35FilAutType", AV35FilAutType);
         AV36FilRol = 0;
         AssignAttri("", false, "AV36FilRol", StringUtil.LTrimStr( (decimal)(AV36FilRol), 12, 0));
      }

      protected void S182( )
      {
         /* 'DO BTNPERMISSIONS' Routine */
         returnInSub = false;
         if ( AV114IsAuthorized_BtnPermissions )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamwwuserpermissions.aspx"+UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID))) + "," + UrlEncode(StringUtil.LTrimStr(0,1,0));
            CallWebObject(formatLink("gamwwuserpermissions.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
      }

      protected void S192( )
      {
         /* 'DO SENDACTIVATIONMAIL' Routine */
         returnInSub = false;
      }

      protected void S202( )
      {
         /* 'DO BTNSETPWD' Routine */
         returnInSub = false;
         if ( AV115IsAuthorized_BtnSetPwd )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamsetpassword.aspx"+UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
            CallWebObject(formatLink("gamsetpassword.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         /* 'DO AUTHENTICATOR' Routine */
         returnInSub = false;
         if ( AV62IsAuthorized_Authenticator )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamusertotpactivation.aspx"+UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
            CallWebObject(formatLink("gamusertotpactivation.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
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
         /* 'DO BTNUNBLOCKOTPCODES' Routine */
         returnInSub = false;
         AV100User.load( AV58GUID);
         if ( AV100User.unblockotpcodes(out  AV33Errors) )
         {
            context.CommitDataStores("gamwwusers",pr_default);
            GX_msglist.addItem(context.GetMessage( "WWP_GAM_UserSuccessfullyUnlockOTPCodes", ""));
         }
         else
         {
            AV125GXV4 = 1;
            while ( AV125GXV4 <= AV33Errors.Count )
            {
               AV31Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV33Errors.Item(AV125GXV4));
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV31Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV31Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
               AV125GXV4 = (int)(AV125GXV4+1);
            }
         }
         gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
      }

      protected void S232( )
      {
         /* 'DO BLOCKUSER' Routine */
         returnInSub = false;
         AV100User.load( AV58GUID);
         if ( AV100User.gxTpr_Isblocked )
         {
            AV100User.gxTpr_Isblocked = false;
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  context.GetMessage( "User succesfully unblocked!", ""),  "success",  "",  "true",  ""));
         }
         else
         {
            AV100User.gxTpr_Isblocked = true;
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  context.GetMessage( "User succesfully blocked!", ""),  "success",  "",  "true",  ""));
         }
         AV100User.save();
         if ( AV100User.success() )
         {
            context.CommitDataStores("gamwwusers",pr_default);
         }
         gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
      }

      protected void S242( )
      {
         /* 'DO UDELETE' Routine */
         returnInSub = false;
         if ( AV116IsAuthorized_UDelete )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamuserentry.aspx"+UrlEncode(StringUtil.RTrim("DLT")) + "," + UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
            CallWebObject(formatLink("gamuserentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
      }

      protected void S252( )
      {
         /* 'DO ENABLEDISABLEUSER' Routine */
         returnInSub = false;
         AV100User.load( AV58GUID);
         if ( AV100User.gxTpr_Isenabledinrepository )
         {
            AV6isOK = AV100User.repositorydisable(out  AV5GAMErrorCollection);
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  context.GetMessage( "User successfully disabled in repository!", ""),  "success",  "",  "true",  ""));
         }
         else
         {
            AV6isOK = AV100User.repositoryenable(out  AV5GAMErrorCollection);
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  context.GetMessage( "User successfully enabled in repository!", ""),  "success",  "",  "true",  ""));
         }
         if ( AV6isOK )
         {
            context.CommitDataStores("gamwwusers",pr_default);
         }
         gxgrGrid_refresh( subGrid_Rows, AV79ManageFiltersExecutionStep, AV17ColumnsSelector, AV123Pgmname, AV40FilUserGender, AV35FilAutType, AV91Search, AV36FilRol, AV41FirstIndex, AV111IsAuthorized_Display, AV112IsAuthorized_Update, AV113IsAuthorized_BtnRole, AV114IsAuthorized_BtnPermissions, AV115IsAuthorized_BtnSetPwd, AV62IsAuthorized_Authenticator, AV116IsAuthorized_UDelete, AV117IsAuthorized_ApplicationAPIKeys, AV118IsAuthorized_Name, AV119IsAuthorized_Email, AV120IsAuthorized_Insert) ;
      }

      protected void S262( )
      {
         /* 'DO KILLSESSIONS' Routine */
         returnInSub = false;
         AV104GAMUser.load( AV58GUID);
         if ( AV104GAMUser.success() )
         {
            AV6isOK = AV104GAMUser.killsessions(out  AV5GAMErrorCollection);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamuserentry.aspx"+UrlEncode(StringUtil.RTrim("DSP")) + "," + UrlEncode(StringUtil.RTrim(AV58GUID));
            CallWebObject(formatLink("gamuserentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
      }

      protected void S272( )
      {
         /* 'DO APPLICATIONAPIKEYS' Routine */
         returnInSub = false;
         if ( AV117IsAuthorized_ApplicationAPIKeys )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamwwuserapplications.aspx"+UrlEncode(StringUtil.RTrim(StringUtil.Trim( AV58GUID)));
            CallWebObject(formatLink("gamwwuserapplications.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
            context.DoAjaxRefresh();
         }
      }

      protected void S122( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV93Session.Get(AV123Pgmname+"GridState"), "") == 0 )
         {
            AV56GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  AV123Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV56GridState.FromXml(AV93Session.Get(AV123Pgmname+"GridState"), null, "", "");
         }
         /* Execute user subroutine: 'LOADREGFILTERSSTATE' */
         S172 ();
         if (returnInSub) return;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV56GridState.gxTpr_Pagesize))) )
         {
            subGrid_Rows = (int)(Math.Round(NumberUtil.Val( AV56GridState.gxTpr_Pagesize, "."), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         }
         subgrid_gotopage( AV56GridState.gxTpr_Currentpage) ;
      }

      protected void S172( )
      {
         /* 'LOADREGFILTERSSTATE' Routine */
         returnInSub = false;
         AV126GXV5 = 1;
         while ( AV126GXV5 <= AV56GridState.gxTpr_Filtervalues.Count )
         {
            AV57GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV56GridState.gxTpr_Filtervalues.Item(AV126GXV5));
            if ( StringUtil.StrCmp(AV57GridStateFilterValue.gxTpr_Name, "SEARCH") == 0 )
            {
               AV91Search = AV57GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV91Search", AV91Search);
            }
            else if ( StringUtil.StrCmp(AV57GridStateFilterValue.gxTpr_Name, "FILUSERGENDER") == 0 )
            {
               AV40FilUserGender = AV57GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV40FilUserGender", AV40FilUserGender);
            }
            else if ( StringUtil.StrCmp(AV57GridStateFilterValue.gxTpr_Name, "FILAUTTYPE") == 0 )
            {
               AV35FilAutType = AV57GridStateFilterValue.gxTpr_Value;
               AssignAttri("", false, "AV35FilAutType", AV35FilAutType);
            }
            else if ( StringUtil.StrCmp(AV57GridStateFilterValue.gxTpr_Name, "FILROL") == 0 )
            {
               AV36FilRol = (long)(Math.Round(NumberUtil.Val( AV57GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV36FilRol", StringUtil.LTrimStr( (decimal)(AV36FilRol), 12, 0));
            }
            AV126GXV5 = (int)(AV126GXV5+1);
         }
      }

      protected void S142( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV56GridState.FromXml(AV93Session.Get(AV123Pgmname+"GridState"), null, "", "");
         AV56GridState.gxTpr_Filtervalues.Clear();
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV56GridState,  "SEARCH",  context.GetMessage( "WWP_GAM_Search", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV91Search)),  0,  AV91Search,  AV91Search,  false,  "",  "") ;
         AV106AuxText = "[" + AV40FilUserGender + "]";
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV56GridState,  "FILUSERGENDER",  context.GetMessage( "WWP_GAM_Gender", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV40FilUserGender)),  0,  AV40FilUserGender,  StringUtil.StringReplace( StringUtil.StringReplace( StringUtil.StringReplace( AV106AuxText, "[N]", context.GetMessage( "WWP_GAM_NotSpecified", "")), "[F]", context.GetMessage( "WWP_GAM_Female", "")), "[M]", context.GetMessage( "WWP_GAM_Male", "")),  false,  "",  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV56GridState,  "FILAUTTYPE",  context.GetMessage( "WWP_GAM_Authentication", ""),  !String.IsNullOrEmpty(StringUtil.RTrim( AV35FilAutType)),  0,  AV35FilAutType,  AV35FilAutType,  false,  "",  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_gridstateaddfiltervalue(context ).execute( ref  AV56GridState,  "FILROL",  context.GetMessage( "WWP_GAM_Role", ""),  !(0==AV36FilRol),  0,  StringUtil.Trim( StringUtil.Str( (decimal)(AV36FilRol), 12, 0)),  StringUtil.Trim( context.localUtil.Format( (decimal)(AV36FilRol), "ZZZZZZZZZZZ9")),  false,  "",  "") ;
         AV56GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         AV56GridState.gxTpr_Currentpage = (short)(subGrid_fnc_Currentpage( ));
         new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV123Pgmname+"GridState",  AV56GridState.ToXml(false, true, "", "")) ;
      }

      protected void wb_table1_29_192( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablerightheader_Internalname, tblTablerightheader_Internalname, "", "Table100x100", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablefilters_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2 CellAlignTopPaddingTop2", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDdo_managefilters.SetProperty("IconType", Ddo_managefilters_Icontype);
            ucDdo_managefilters.SetProperty("Icon", Ddo_managefilters_Icon);
            ucDdo_managefilters.SetProperty("Caption", Ddo_managefilters_Caption);
            ucDdo_managefilters.SetProperty("Tooltip", Ddo_managefilters_Tooltip);
            ucDdo_managefilters.SetProperty("Cls", Ddo_managefilters_Cls);
            ucDdo_managefilters.SetProperty("DropDownOptionsData", AV77ManageFiltersData);
            ucDdo_managefilters.Render(context, "dvelop.gxbootstrap.ddoregular", Ddo_managefilters_Internalname, "DDO_MANAGEFILTERSContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2 CellFormGroupMarginBottom5 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSearch_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSearch_Internalname, context.GetMessage( "WWP_GAM_Search", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'',false,'" + sGXsfl_60_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSearch_Internalname, AV91Search, StringUtil.RTrim( context.localUtil.Format( AV91Search, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,42);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "Login/Name/Email", ""), edtavSearch_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSearch_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_GAMWWUsers.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2 CellFormGroupMarginBottom5 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavFilusergender_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavFilusergender_Internalname, context.GetMessage( "WWP_GAM_Gender", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 46,'',false,'" + sGXsfl_60_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavFilusergender, cmbavFilusergender_Internalname, StringUtil.RTrim( AV40FilUserGender), 1, cmbavFilusergender_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavFilusergender.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,46);\"", "", true, 0, "HLP_GAMWWUsers.htm");
            cmbavFilusergender.CurrentValue = StringUtil.RTrim( AV40FilUserGender);
            AssignProp("", false, cmbavFilusergender_Internalname, "Values", (string)(cmbavFilusergender.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2 CellFormGroupMarginBottom5 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavFilauttype_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavFilauttype_Internalname, context.GetMessage( "WWP_GAM_Authentication", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 50,'',false,'" + sGXsfl_60_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavFilauttype, cmbavFilauttype_Internalname, StringUtil.RTrim( AV35FilAutType), 1, cmbavFilauttype_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavFilauttype.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,50);\"", "", true, 0, "HLP_GAMWWUsers.htm");
            cmbavFilauttype.CurrentValue = StringUtil.RTrim( AV35FilAutType);
            AssignProp("", false, cmbavFilauttype_Internalname, "Values", (string)(cmbavFilauttype.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2 CellFormGroupMarginBottom5 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavFilrol_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavFilrol_Internalname, context.GetMessage( "WWP_GAM_Role", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 54,'',false,'" + sGXsfl_60_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavFilrol, cmbavFilrol_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV36FilRol), 12, 0)), 1, cmbavFilrol_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavFilrol.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,54);\"", "", true, 0, "HLP_GAMWWUsers.htm");
            cmbavFilrol.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV36FilRol), 12, 0));
            AssignProp("", false, cmbavFilrol_Internalname, "Values", (string)(cmbavFilrol.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_29_192e( true) ;
         }
         else
         {
            wb_table1_29_192e( false) ;
         }
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
         PA192( ) ;
         WS192( ) ;
         WE192( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623542676", true, true);
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
         context.AddJavascriptSource("gamwwusers.js", "?2024121623542689", false, true);
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
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_602( )
      {
         edtavDisplay_Internalname = "vDISPLAY_"+sGXsfl_60_idx;
         edtavUpdate_Internalname = "vUPDATE_"+sGXsfl_60_idx;
         edtavBtnrole_Internalname = "vBTNROLE_"+sGXsfl_60_idx;
         cmbavGridactiongroup1_Internalname = "vGRIDACTIONGROUP1_"+sGXsfl_60_idx;
         edtavName_Internalname = "vNAME_"+sGXsfl_60_idx;
         edtavEmail_Internalname = "vEMAIL_"+sGXsfl_60_idx;
         edtavFirstname_Internalname = "vFIRSTNAME_"+sGXsfl_60_idx;
         edtavLastname_Internalname = "vLASTNAME_"+sGXsfl_60_idx;
         edtavAuthenticationtypename_Internalname = "vAUTHENTICATIONTYPENAME_"+sGXsfl_60_idx;
         edtavGuid_Internalname = "vGUID_"+sGXsfl_60_idx;
         edtavStatus_Internalname = "vSTATUS_"+sGXsfl_60_idx;
      }

      protected void SubsflControlProps_fel_602( )
      {
         edtavDisplay_Internalname = "vDISPLAY_"+sGXsfl_60_fel_idx;
         edtavUpdate_Internalname = "vUPDATE_"+sGXsfl_60_fel_idx;
         edtavBtnrole_Internalname = "vBTNROLE_"+sGXsfl_60_fel_idx;
         cmbavGridactiongroup1_Internalname = "vGRIDACTIONGROUP1_"+sGXsfl_60_fel_idx;
         edtavName_Internalname = "vNAME_"+sGXsfl_60_fel_idx;
         edtavEmail_Internalname = "vEMAIL_"+sGXsfl_60_fel_idx;
         edtavFirstname_Internalname = "vFIRSTNAME_"+sGXsfl_60_fel_idx;
         edtavLastname_Internalname = "vLASTNAME_"+sGXsfl_60_fel_idx;
         edtavAuthenticationtypename_Internalname = "vAUTHENTICATIONTYPENAME_"+sGXsfl_60_fel_idx;
         edtavGuid_Internalname = "vGUID_"+sGXsfl_60_fel_idx;
         edtavStatus_Internalname = "vSTATUS_"+sGXsfl_60_fel_idx;
      }

      protected void sendrow_602( )
      {
         sGXsfl_60_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_60_idx), 4, 0), 4, "0");
         SubsflControlProps_602( ) ;
         WB190( ) ;
         if ( ( subGrid_Rows * 1 == 0 ) || ( nGXsfl_60_idx <= subGrid_fnc_Recordsperpage( ) * 1 ) )
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
               if ( ((int)((nGXsfl_60_idx) % (2))) == 0 )
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
               context.WriteHtmlText( " gxrow=\""+sGXsfl_60_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavDisplay_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 61,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplay_Internalname,StringUtil.RTrim( AV29Display),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,61);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)edtavDisplay_Link,(string)"",context.GetMessage( "GXM_display", ""),(string)"",(string)edtavDisplay_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(int)edtavDisplay_Visible,(int)edtavDisplay_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)60,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavUpdate_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 62,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = edtavUpdate_Class;
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavUpdate_Internalname,StringUtil.RTrim( AV99Update),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,62);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)edtavUpdate_Link,(string)"",context.GetMessage( "GXM_update", ""),(string)"",(string)edtavUpdate_Jsonclick,(short)0,(string)edtavUpdate_Class,(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(int)edtavUpdate_Visible,(int)edtavUpdate_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)60,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavBtnrole_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 63,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = edtavBtnrole_Class;
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavBtnrole_Internalname,StringUtil.RTrim( AV12BtnRole),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,63);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)edtavBtnrole_Link,(string)"",context.GetMessage( "WWP_GAM_Roles", ""),(string)"",(string)edtavBtnrole_Jsonclick,(short)0,(string)edtavBtnrole_Class,(string)"",(string)ROClassString,(string)"WWIconActionColumn hidden-xs",(string)"",(int)edtavBtnrole_Visible,(int)edtavBtnrole_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)60,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
            }
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 64,'',false,'" + sGXsfl_60_idx + "',60)\"";
            if ( ( cmbavGridactiongroup1.ItemCount == 0 ) && isAjaxCallMode( ) )
            {
               GXCCtl = "vGRIDACTIONGROUP1_" + sGXsfl_60_idx;
               cmbavGridactiongroup1.Name = GXCCtl;
               cmbavGridactiongroup1.WebTags = "";
               if ( cmbavGridactiongroup1.ItemCount > 0 )
               {
                  AV48GridActionGroup1 = (short)(Math.Round(NumberUtil.Val( cmbavGridactiongroup1.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV48GridActionGroup1), 4, 0))), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV48GridActionGroup1), 4, 0));
               }
            }
            /* ComboBox */
            GridRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavGridactiongroup1,(string)cmbavGridactiongroup1_Internalname,StringUtil.Trim( StringUtil.Str( (decimal)(AV48GridActionGroup1), 4, 0)),(short)1,(string)cmbavGridactiongroup1_Jsonclick,(short)5,"'"+""+"'"+",false,"+"'"+"EVGRIDACTIONGROUP1.CLICK."+sGXsfl_60_idx+"'",(string)"int",(string)"",(short)-1,(short)1,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"ConvertToDDO",(string)"WWActionGroupColumn",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,64);\"",(string)"",(bool)true,(short)0});
            cmbavGridactiongroup1.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV48GridActionGroup1), 4, 0));
            AssignProp("", false, cmbavGridactiongroup1_Internalname, "Values", (string)(cmbavGridactiongroup1.ToJavascriptSource()), !bGXsfl_60_Refreshing);
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavName_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 65,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = edtavName_Class;
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavName_Internalname,(string)AV83Name,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,65);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)edtavName_Link,(string)"",(string)"",(string)"",(string)edtavName_Jsonclick,(short)0,(string)edtavName_Class,(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtavName_Visible,(int)edtavName_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)60,(short)0,(short)0,(short)0,(bool)true,(string)"GeneXusSecurityCommon\\GAMUserIdentification",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavEmail_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 66,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = edtavEmail_Class;
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavEmail_Internalname,(string)AV30Email,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,66);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)edtavEmail_Link,(string)"",(string)"",(string)"",(string)edtavEmail_Jsonclick,(short)0,(string)edtavEmail_Class,(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtavEmail_Visible,(int)edtavEmail_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)60,(short)0,(short)-1,(short)0,(bool)true,(string)"GeneXusSecurityCommon\\GAMEMail",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavFirstname_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 67,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = edtavFirstname_Class;
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavFirstname_Internalname,StringUtil.RTrim( AV42FirstName),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,67);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavFirstname_Jsonclick,(short)0,(string)edtavFirstname_Class,(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtavFirstname_Visible,(int)edtavFirstname_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)60,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusSecurityCommon\\GAMDescriptionShort",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavLastname_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 68,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = edtavLastname_Class;
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavLastname_Internalname,StringUtil.RTrim( AV75LastName),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,68);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavLastname_Jsonclick,(short)0,(string)edtavLastname_Class,(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtavLastname_Visible,(int)edtavLastname_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)60,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusSecurityCommon\\GAMDescriptionShort",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavAuthenticationtypename_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 69,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = edtavAuthenticationtypename_Class;
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavAuthenticationtypename_Internalname,StringUtil.RTrim( AV8AuthenticationTypeName),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,69);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavAuthenticationtypename_Jsonclick,(short)0,(string)edtavAuthenticationtypename_Class,(string)"",(string)ROClassString,(string)"WWColumn hidden-xs",(string)"",(int)edtavAuthenticationtypename_Visible,(int)edtavAuthenticationtypename_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)60,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusSecurityCommon\\GAMAuthenticationTypeName",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGuid_Internalname,StringUtil.RTrim( AV58GUID),(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavGuid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)0,(int)edtavGuid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)60,(short)0,(short)-1,(short)0,(bool)true,(string)"GeneXusSecurityCommon\\GAMGUID",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavStatus_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 71,'',false,'" + sGXsfl_60_idx + "',60)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavStatus_Internalname,(string)AV94Status,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,71);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavStatus_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)edtavStatus_Columnclass,(string)edtavStatus_Columnheaderclass,(int)edtavStatus_Visible,(int)edtavStatus_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)60,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes192( ) ;
            GridContainer.AddRow(GridRow);
            nGXsfl_60_idx = ((subGrid_Islastpage==1)&&(nGXsfl_60_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_60_idx+1);
            sGXsfl_60_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_60_idx), 4, 0), 4, "0");
            SubsflControlProps_602( ) ;
         }
         /* End function sendrow_602 */
      }

      protected void init_web_controls( )
      {
         cmbavFilusergender.Name = "vFILUSERGENDER";
         cmbavFilusergender.WebTags = "";
         cmbavFilusergender.addItem("", context.GetMessage( "All", ""), 0);
         cmbavFilusergender.addItem("N", context.GetMessage( "WWP_GAM_NotSpecified", ""), 0);
         cmbavFilusergender.addItem("F", context.GetMessage( "WWP_GAM_Female", ""), 0);
         cmbavFilusergender.addItem("M", context.GetMessage( "WWP_GAM_Male", ""), 0);
         if ( cmbavFilusergender.ItemCount > 0 )
         {
            AV40FilUserGender = cmbavFilusergender.getValidValue(AV40FilUserGender);
            AssignAttri("", false, "AV40FilUserGender", AV40FilUserGender);
         }
         cmbavFilauttype.Name = "vFILAUTTYPE";
         cmbavFilauttype.WebTags = "";
         if ( cmbavFilauttype.ItemCount > 0 )
         {
            AV35FilAutType = cmbavFilauttype.getValidValue(AV35FilAutType);
            AssignAttri("", false, "AV35FilAutType", AV35FilAutType);
         }
         cmbavFilrol.Name = "vFILROL";
         cmbavFilrol.WebTags = "";
         if ( cmbavFilrol.ItemCount > 0 )
         {
            AV36FilRol = (long)(Math.Round(NumberUtil.Val( cmbavFilrol.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV36FilRol), 12, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV36FilRol", StringUtil.LTrimStr( (decimal)(AV36FilRol), 12, 0));
         }
         GXCCtl = "vGRIDACTIONGROUP1_" + sGXsfl_60_idx;
         cmbavGridactiongroup1.Name = GXCCtl;
         cmbavGridactiongroup1.WebTags = "";
         if ( cmbavGridactiongroup1.ItemCount > 0 )
         {
            AV48GridActionGroup1 = (short)(Math.Round(NumberUtil.Val( cmbavGridactiongroup1.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV48GridActionGroup1), 4, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, cmbavGridactiongroup1_Internalname, StringUtil.LTrimStr( (decimal)(AV48GridActionGroup1), 4, 0));
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl60( )
      {
         if ( GridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridContainer"+"DivS\" data-gxgridid=\"60\">") ;
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
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavDisplay_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavUpdate_Class+"\" "+" style=\""+((edtavUpdate_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavBtnrole_Class+"\" "+" style=\""+((edtavBtnrole_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( edtavBtnrole_Title) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"ConvertToDDO"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavName_Class+"\" "+" style=\""+((edtavName_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Name", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavEmail_Class+"\" "+" style=\""+((edtavEmail_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Email", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavFirstname_Class+"\" "+" style=\""+((edtavFirstname_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_FirstName", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavLastname_Class+"\" "+" style=\""+((edtavLastname_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_LastName", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavAuthenticationtypename_Class+"\" "+" style=\""+((edtavAuthenticationtypename_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Authentication", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavStatus_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "Status", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridContainer.AddObjectProperty("GridName", "Grid");
         }
         else
         {
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
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV29Display)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplay_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavDisplay_Link));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplay_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV99Update)));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavUpdate_Class));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavUpdate_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavUpdate_Link));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavUpdate_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV12BtnRole)));
            GridColumn.AddObjectProperty("Title", StringUtil.RTrim( edtavBtnrole_Title));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavBtnrole_Class));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavBtnrole_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavBtnrole_Link));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavBtnrole_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.LTrim( StringUtil.NToC( (decimal)(AV48GridActionGroup1), 4, 0, ".", ""))));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV83Name));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavName_Class));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavName_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavName_Link));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavName_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV30Email));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavEmail_Class));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavEmail_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavEmail_Link));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavEmail_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV42FirstName)));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavFirstname_Class));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavFirstname_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavFirstname_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV75LastName)));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavLastname_Class));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavLastname_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavLastname_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV8AuthenticationTypeName)));
            GridColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavAuthenticationtypename_Class));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavAuthenticationtypename_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavAuthenticationtypename_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV58GUID)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGuid_Enabled), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV94Status));
            GridColumn.AddObjectProperty("Columnclass", StringUtil.RTrim( edtavStatus_Columnclass));
            GridColumn.AddObjectProperty("Columnheaderclass", StringUtil.RTrim( edtavStatus_Columnheaderclass));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavStatus_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavStatus_Visible), 5, 0, ".", "")));
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
         edtavSearch_Internalname = "vSEARCH";
         cmbavFilusergender_Internalname = "vFILUSERGENDER";
         cmbavFilauttype_Internalname = "vFILAUTTYPE";
         cmbavFilrol_Internalname = "vFILROL";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         divTablefilters_Internalname = "TABLEFILTERS";
         tblTablerightheader_Internalname = "TABLERIGHTHEADER";
         divTableheader_Internalname = "TABLEHEADER";
         edtavDisplay_Internalname = "vDISPLAY";
         edtavUpdate_Internalname = "vUPDATE";
         edtavBtnrole_Internalname = "vBTNROLE";
         cmbavGridactiongroup1_Internalname = "vGRIDACTIONGROUP1";
         edtavName_Internalname = "vNAME";
         edtavEmail_Internalname = "vEMAIL";
         edtavFirstname_Internalname = "vFIRSTNAME";
         edtavLastname_Internalname = "vLASTNAME";
         edtavAuthenticationtypename_Internalname = "vAUTHENTICATIONTYPENAME";
         edtavGuid_Internalname = "vGUID";
         edtavStatus_Internalname = "vSTATUS";
         Gridpaginationbar_Internalname = "GRIDPAGINATIONBAR";
         divGridtablewithpaginationbar_Internalname = "GRIDTABLEWITHPAGINATIONBAR";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         Dvpanel_unnamedtable1_Internalname = "DVPANEL_UNNAMEDTABLE1";
         divTablemain_Internalname = "TABLEMAIN";
         Ddo_grid_Internalname = "DDO_GRID";
         Ddo_gridcolumnsselector_Internalname = "DDO_GRIDCOLUMNSSELECTOR";
         edtavGamuserscount_Internalname = "vGAMUSERSCOUNT";
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
         edtavStatus_Jsonclick = "";
         edtavStatus_Columnclass = "WWColumn";
         edtavStatus_Enabled = 1;
         edtavGuid_Jsonclick = "";
         edtavGuid_Enabled = 1;
         edtavAuthenticationtypename_Jsonclick = "";
         edtavAuthenticationtypename_Class = "Attribute";
         edtavAuthenticationtypename_Enabled = 1;
         edtavLastname_Jsonclick = "";
         edtavLastname_Class = "Attribute";
         edtavLastname_Enabled = 1;
         edtavFirstname_Jsonclick = "";
         edtavFirstname_Class = "Attribute";
         edtavFirstname_Enabled = 1;
         edtavEmail_Jsonclick = "";
         edtavEmail_Class = "Attribute";
         edtavEmail_Link = "";
         edtavEmail_Enabled = 1;
         edtavName_Jsonclick = "";
         edtavName_Class = "Attribute";
         edtavName_Link = "";
         edtavName_Enabled = 1;
         cmbavGridactiongroup1_Jsonclick = "";
         edtavBtnrole_Jsonclick = "";
         edtavBtnrole_Class = "Attribute";
         edtavBtnrole_Link = "";
         edtavBtnrole_Enabled = 1;
         edtavUpdate_Jsonclick = "";
         edtavUpdate_Class = "Attribute";
         edtavUpdate_Link = "";
         edtavUpdate_Enabled = 1;
         edtavDisplay_Jsonclick = "";
         edtavDisplay_Link = "";
         edtavDisplay_Enabled = 1;
         subGrid_Class = "GridWithPaginationBar GridNoBorder WorkWith";
         subGrid_Backcolorstyle = 0;
         cmbavFilrol_Jsonclick = "";
         cmbavFilrol.Enabled = 1;
         cmbavFilauttype_Jsonclick = "";
         cmbavFilauttype.Enabled = 1;
         cmbavFilusergender_Jsonclick = "";
         cmbavFilusergender.Enabled = 1;
         edtavSearch_Jsonclick = "";
         edtavSearch_Enabled = 1;
         edtavBtnrole_Visible = -1;
         edtavUpdate_Visible = -1;
         edtavDisplay_Visible = -1;
         edtavStatus_Columnheaderclass = "";
         edtavStatus_Visible = -1;
         edtavAuthenticationtypename_Visible = -1;
         edtavLastname_Visible = -1;
         edtavFirstname_Visible = -1;
         edtavEmail_Visible = -1;
         edtavName_Visible = -1;
         subGrid_Sortable = 0;
         edtavGamuserscount_Jsonclick = "";
         edtavGamuserscount_Visible = 1;
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
         Ddo_grid_Fixable = "T";
         Ddo_grid_Columnssortvalues = "|||||";
         Ddo_grid_Columnids = "4:Name|5:Email|6:FirstName|7:LastName|8:AuthenticationTypeName|10:Status";
         Ddo_grid_Gridinternalname = "";
         Dvpanel_unnamedtable1_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Iconposition = "Right";
         Dvpanel_unnamedtable1_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Title = context.GetMessage( "Gestión de Usuarios", "");
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
         Form.Caption = context.GetMessage( "WWP_GAM_Users", "");
         edtavBtnrole_Title = "";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"edtavBtnrole_Title","ctrl":"vBTNROLE","prop":"Title"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV123Pgmname","fld":"vPGMNAME","hsh":true},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtavName_Visible","ctrl":"vNAME","prop":"Visible"},{"av":"edtavEmail_Visible","ctrl":"vEMAIL","prop":"Visible"},{"av":"edtavFirstname_Visible","ctrl":"vFIRSTNAME","prop":"Visible"},{"av":"edtavLastname_Visible","ctrl":"vLASTNAME","prop":"Visible"},{"av":"edtavAuthenticationtypename_Visible","ctrl":"vAUTHENTICATIONTYPENAME","prop":"Visible"},{"av":"edtavStatus_Visible","ctrl":"vSTATUS","prop":"Visible"},{"av":"AV51GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV50GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"edtavStatus_Columnheaderclass","ctrl":"vSTATUS","prop":"Columnheaderclass"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV53GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"edtavDisplay_Visible","ctrl":"vDISPLAY","prop":"Visible"},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"edtavUpdate_Visible","ctrl":"vUPDATE","prop":"Visible"},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"edtavBtnrole_Visible","ctrl":"vBTNROLE","prop":"Visible"},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV77ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV56GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E12192","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV123Pgmname","fld":"vPGMNAME","hsh":true},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"edtavBtnrole_Title","ctrl":"vBTNROLE","prop":"Title"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E13192","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV123Pgmname","fld":"vPGMNAME","hsh":true},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"edtavBtnrole_Title","ctrl":"vBTNROLE","prop":"Title"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E18192","iparms":[{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"AV8AuthenticationTypeName","fld":"vAUTHENTICATIONTYPENAME"},{"av":"AV58GUID","fld":"vGUID"},{"av":"AV83Name","fld":"vNAME"},{"av":"AV42FirstName","fld":"vFIRSTNAME"},{"av":"AV75LastName","fld":"vLASTNAME"},{"av":"AV94Status","fld":"vSTATUS"},{"av":"AV30Email","fld":"vEMAIL"},{"av":"edtavName_Class","ctrl":"vNAME","prop":"Class"},{"av":"edtavFirstname_Class","ctrl":"vFIRSTNAME","prop":"Class"},{"av":"edtavLastname_Class","ctrl":"vLASTNAME","prop":"Class"},{"av":"edtavAuthenticationtypename_Class","ctrl":"vAUTHENTICATIONTYPENAME","prop":"Class"},{"av":"edtavEmail_Class","ctrl":"vEMAIL","prop":"Class"},{"av":"AV29Display","fld":"vDISPLAY"},{"av":"edtavDisplay_Link","ctrl":"vDISPLAY","prop":"Link"},{"av":"AV99Update","fld":"vUPDATE"},{"av":"edtavUpdate_Link","ctrl":"vUPDATE","prop":"Link"},{"av":"edtavUpdate_Class","ctrl":"vUPDATE","prop":"Class"},{"av":"AV12BtnRole","fld":"vBTNROLE"},{"av":"edtavBtnrole_Link","ctrl":"vBTNROLE","prop":"Link"},{"av":"edtavBtnrole_Class","ctrl":"vBTNROLE","prop":"Class"},{"av":"cmbavGridactiongroup1"},{"av":"AV48GridActionGroup1","fld":"vGRIDACTIONGROUP1","pic":"ZZZ9"},{"av":"edtavName_Link","ctrl":"vNAME","prop":"Link"},{"av":"edtavEmail_Link","ctrl":"vEMAIL","prop":"Link"},{"av":"edtavStatus_Columnclass","ctrl":"vSTATUS","prop":"Columnclass"}]}""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED","""{"handler":"E14192","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV123Pgmname","fld":"vPGMNAME","hsh":true},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"edtavBtnrole_Title","ctrl":"vBTNROLE","prop":"Title"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_gridcolumnsselector_Columnsselectorvalues","ctrl":"DDO_GRIDCOLUMNSSELECTOR","prop":"ColumnsSelectorValues"}]""");
         setEventMetadata("DDO_GRIDCOLUMNSSELECTOR.ONCOLUMNSCHANGED",""","oparms":[{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"edtavName_Visible","ctrl":"vNAME","prop":"Visible"},{"av":"edtavEmail_Visible","ctrl":"vEMAIL","prop":"Visible"},{"av":"edtavFirstname_Visible","ctrl":"vFIRSTNAME","prop":"Visible"},{"av":"edtavLastname_Visible","ctrl":"vLASTNAME","prop":"Visible"},{"av":"edtavAuthenticationtypename_Visible","ctrl":"vAUTHENTICATIONTYPENAME","prop":"Visible"},{"av":"edtavStatus_Visible","ctrl":"vSTATUS","prop":"Visible"},{"av":"AV51GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV50GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"edtavStatus_Columnheaderclass","ctrl":"vSTATUS","prop":"Columnheaderclass"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV53GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"edtavDisplay_Visible","ctrl":"vDISPLAY","prop":"Visible"},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"edtavUpdate_Visible","ctrl":"vUPDATE","prop":"Visible"},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"edtavBtnrole_Visible","ctrl":"vBTNROLE","prop":"Visible"},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV77ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV56GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED","""{"handler":"E11192","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV123Pgmname","fld":"vPGMNAME","hsh":true},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"edtavBtnrole_Title","ctrl":"vBTNROLE","prop":"Title"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"Ddo_managefilters_Activeeventkey","ctrl":"DDO_MANAGEFILTERS","prop":"ActiveEventKey"},{"av":"AV56GridState","fld":"vGRIDSTATE"}]""");
         setEventMetadata("DDO_MANAGEFILTERS.ONOPTIONCLICKED",""","oparms":[{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV56GridState","fld":"vGRIDSTATE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtavName_Visible","ctrl":"vNAME","prop":"Visible"},{"av":"edtavEmail_Visible","ctrl":"vEMAIL","prop":"Visible"},{"av":"edtavFirstname_Visible","ctrl":"vFIRSTNAME","prop":"Visible"},{"av":"edtavLastname_Visible","ctrl":"vLASTNAME","prop":"Visible"},{"av":"edtavAuthenticationtypename_Visible","ctrl":"vAUTHENTICATIONTYPENAME","prop":"Visible"},{"av":"edtavStatus_Visible","ctrl":"vSTATUS","prop":"Visible"},{"av":"AV51GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV50GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"edtavStatus_Columnheaderclass","ctrl":"vSTATUS","prop":"Columnheaderclass"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV53GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"edtavDisplay_Visible","ctrl":"vDISPLAY","prop":"Visible"},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"edtavUpdate_Visible","ctrl":"vUPDATE","prop":"Visible"},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"edtavBtnrole_Visible","ctrl":"vBTNROLE","prop":"Visible"},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV77ManageFiltersData","fld":"vMANAGEFILTERSDATA"}]}""");
         setEventMetadata("VGRIDACTIONGROUP1.CLICK","""{"handler":"E19192","iparms":[{"av":"cmbavGridactiongroup1"},{"av":"AV48GridActionGroup1","fld":"vGRIDACTIONGROUP1","pic":"ZZZ9"},{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV123Pgmname","fld":"vPGMNAME","hsh":true},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"edtavBtnrole_Title","ctrl":"vBTNROLE","prop":"Title"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"av":"AV58GUID","fld":"vGUID"}]""");
         setEventMetadata("VGRIDACTIONGROUP1.CLICK",""","oparms":[{"av":"cmbavGridactiongroup1"},{"av":"AV48GridActionGroup1","fld":"vGRIDACTIONGROUP1","pic":"ZZZ9"},{"av":"AV58GUID","fld":"vGUID"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtavName_Visible","ctrl":"vNAME","prop":"Visible"},{"av":"edtavEmail_Visible","ctrl":"vEMAIL","prop":"Visible"},{"av":"edtavFirstname_Visible","ctrl":"vFIRSTNAME","prop":"Visible"},{"av":"edtavLastname_Visible","ctrl":"vLASTNAME","prop":"Visible"},{"av":"edtavAuthenticationtypename_Visible","ctrl":"vAUTHENTICATIONTYPENAME","prop":"Visible"},{"av":"edtavStatus_Visible","ctrl":"vSTATUS","prop":"Visible"},{"av":"AV51GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV50GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"edtavStatus_Columnheaderclass","ctrl":"vSTATUS","prop":"Columnheaderclass"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV53GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"edtavDisplay_Visible","ctrl":"vDISPLAY","prop":"Visible"},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"edtavUpdate_Visible","ctrl":"vUPDATE","prop":"Visible"},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"edtavBtnrole_Visible","ctrl":"vBTNROLE","prop":"Visible"},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV77ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV56GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("'DOINSERT'","""{"handler":"E15192","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"AV123Pgmname","fld":"vPGMNAME","hsh":true},{"av":"cmbavFilusergender"},{"av":"AV40FilUserGender","fld":"vFILUSERGENDER"},{"av":"cmbavFilauttype"},{"av":"AV35FilAutType","fld":"vFILAUTTYPE"},{"av":"AV91Search","fld":"vSEARCH"},{"av":"cmbavFilrol"},{"av":"AV36FilRol","fld":"vFILROL","pic":"ZZZZZZZZZZZ9"},{"av":"edtavBtnrole_Title","ctrl":"vBTNROLE","prop":"Title"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV118IsAuthorized_Name","fld":"vISAUTHORIZED_NAME","hsh":true},{"av":"AV119IsAuthorized_Email","fld":"vISAUTHORIZED_EMAIL","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true}]""");
         setEventMetadata("'DOINSERT'",""","oparms":[{"av":"AV79ManageFiltersExecutionStep","fld":"vMANAGEFILTERSEXECUTIONSTEP","pic":"9"},{"av":"AV17ColumnsSelector","fld":"vCOLUMNSSELECTOR"},{"av":"edtavName_Visible","ctrl":"vNAME","prop":"Visible"},{"av":"edtavEmail_Visible","ctrl":"vEMAIL","prop":"Visible"},{"av":"edtavFirstname_Visible","ctrl":"vFIRSTNAME","prop":"Visible"},{"av":"edtavLastname_Visible","ctrl":"vLASTNAME","prop":"Visible"},{"av":"edtavAuthenticationtypename_Visible","ctrl":"vAUTHENTICATIONTYPENAME","prop":"Visible"},{"av":"edtavStatus_Visible","ctrl":"vSTATUS","prop":"Visible"},{"av":"AV51GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV50GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"edtavStatus_Columnheaderclass","ctrl":"vSTATUS","prop":"Columnheaderclass"},{"av":"AV41FirstIndex","fld":"vFIRSTINDEX","pic":"ZZZ9","hsh":true},{"av":"AV53GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"},{"av":"AV111IsAuthorized_Display","fld":"vISAUTHORIZED_DISPLAY","hsh":true},{"av":"edtavDisplay_Visible","ctrl":"vDISPLAY","prop":"Visible"},{"av":"AV112IsAuthorized_Update","fld":"vISAUTHORIZED_UPDATE","hsh":true},{"av":"edtavUpdate_Visible","ctrl":"vUPDATE","prop":"Visible"},{"av":"AV113IsAuthorized_BtnRole","fld":"vISAUTHORIZED_BTNROLE","hsh":true},{"av":"edtavBtnrole_Visible","ctrl":"vBTNROLE","prop":"Visible"},{"av":"AV114IsAuthorized_BtnPermissions","fld":"vISAUTHORIZED_BTNPERMISSIONS","hsh":true},{"av":"AV115IsAuthorized_BtnSetPwd","fld":"vISAUTHORIZED_BTNSETPWD","hsh":true},{"av":"AV62IsAuthorized_Authenticator","fld":"vISAUTHORIZED_AUTHENTICATOR","hsh":true},{"av":"AV116IsAuthorized_UDelete","fld":"vISAUTHORIZED_UDELETE","hsh":true},{"av":"AV117IsAuthorized_ApplicationAPIKeys","fld":"vISAUTHORIZED_APPLICATIONAPIKEYS","hsh":true},{"av":"AV120IsAuthorized_Insert","fld":"vISAUTHORIZED_INSERT","hsh":true},{"ctrl":"BTNINSERT","prop":"Visible"},{"av":"AV77ManageFiltersData","fld":"vMANAGEFILTERSDATA"},{"av":"AV56GridState","fld":"vGRIDSTATE"}]}""");
         setEventMetadata("VALIDV_FILUSERGENDER","""{"handler":"Validv_Filusergender","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Status","iparms":[]}""");
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
         Ddo_gridcolumnsselector_Columnsselectorvalues = "";
         Ddo_managefilters_Activeeventkey = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV17ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV123Pgmname = "";
         AV40FilUserGender = "";
         AV35FilAutType = "";
         AV91Search = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV77ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV50GridAppliedFilters = "";
         AV27DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV56GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         Ddo_grid_Caption = "";
         Ddo_gridcolumnsselector_Gridinternalname = "";
         Grid_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ucDvpanel_unnamedtable1 = new GXUserControl();
         ClassString = "";
         StyleString = "";
         TempTags = "";
         bttBtninsert_Jsonclick = "";
         bttBtneditcolumns_Jsonclick = "";
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
         AV29Display = "";
         AV99Update = "";
         AV12BtnRole = "";
         AV83Name = "";
         AV30Email = "";
         AV42FirstName = "";
         AV75LastName = "";
         AV8AuthenticationTypeName = "";
         AV58GUID = "";
         AV94Status = "";
         AV59HTTPRequest = new GxHttpRequest( context);
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2 = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV9AuthenticationTypes = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType", "DesignSystem.Programs");
         AV38FilterAutType = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter(context);
         AV33Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV7AuthenticationType = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType(context);
         AV90Roles = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRole>( context, "GeneXus.Programs.genexussecurity.SdtGAMRole", "DesignSystem.Programs");
         AV39FilterRoles = new GeneXus.Programs.genexussecurity.SdtGAMRoleFilter(context);
         AV88Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV89Role = new GeneXus.Programs.genexussecurity.SdtGAMRole(context);
         AV103WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV93Session = context.GetSession();
         AV19ColumnsSelectorXML = "";
         AV37Filter = new GeneXus.Programs.genexussecurity.SdtGAMUserFilter(context);
         AV46GAMUsers = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMUser>( context, "GeneXus.Programs.genexussecurity.SdtGAMUser", "DesignSystem.Programs");
         GridRow = new GXWebRow();
         AV100User = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV104GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV5GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         GXEncryptionTmp = "";
         AV82ManageFiltersXml = "";
         AV101UserCustomValue = "";
         GXt_char3 = "";
         AV18ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV31Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         AV57GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV106AuxText = "";
         ucDdo_managefilters = new GXUserControl();
         Ddo_managefilters_Caption = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid_Linesclass = "";
         ROClassString = "";
         GXCCtl = "";
         GridColumn = new GXWebColumn();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamwwusers__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamwwusers__default(),
            new Object[][] {
            }
         );
         AV123Pgmname = "GAMWWUsers";
         /* GeneXus formulas. */
         AV123Pgmname = "GAMWWUsers";
         edtavDisplay_Enabled = 0;
         edtavUpdate_Enabled = 0;
         edtavBtnrole_Enabled = 0;
         edtavName_Enabled = 0;
         edtavEmail_Enabled = 0;
         edtavFirstname_Enabled = 0;
         edtavLastname_Enabled = 0;
         edtavAuthenticationtypename_Enabled = 0;
         edtavGuid_Enabled = 0;
         edtavStatus_Enabled = 0;
      }

      private short GRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short AV79ManageFiltersExecutionStep ;
      private short AV41FirstIndex ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV47GAMUsersCount ;
      private short AV48GridActionGroup1 ;
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
      private int nRC_GXsfl_60 ;
      private int subGrid_Recordcount ;
      private int nGXsfl_60_idx=1 ;
      private int Gridpaginationbar_Pagestoshow ;
      private int bttBtninsert_Visible ;
      private int edtavGamuserscount_Visible ;
      private int subGrid_Islastpage ;
      private int edtavDisplay_Enabled ;
      private int edtavUpdate_Enabled ;
      private int edtavBtnrole_Enabled ;
      private int edtavName_Enabled ;
      private int edtavEmail_Enabled ;
      private int edtavFirstname_Enabled ;
      private int edtavLastname_Enabled ;
      private int edtavAuthenticationtypename_Enabled ;
      private int edtavGuid_Enabled ;
      private int edtavStatus_Enabled ;
      private int GRID_nGridOutOfScope ;
      private int AV121GXV1 ;
      private int AV122GXV2 ;
      private int edtavName_Visible ;
      private int edtavEmail_Visible ;
      private int edtavFirstname_Visible ;
      private int edtavLastname_Visible ;
      private int edtavAuthenticationtypename_Visible ;
      private int edtavStatus_Visible ;
      private int AV85PageToGo ;
      private int AV124GXV3 ;
      private int edtavDisplay_Visible ;
      private int edtavUpdate_Visible ;
      private int edtavBtnrole_Visible ;
      private int AV125GXV4 ;
      private int AV126GXV5 ;
      private int edtavSearch_Enabled ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long GRID_nFirstRecordOnPage ;
      private long AV36FilRol ;
      private long AV51GridCurrentPage ;
      private long AV53GridPageCount ;
      private long GRID_nCurrentRecord ;
      private long AV54GridPageSize ;
      private long AV55GridRecordCount ;
      private long AV60i ;
      private string edtavBtnrole_Title ;
      private string Gridpaginationbar_Selectedpage ;
      private string Ddo_gridcolumnsselector_Columnsselectorvalues ;
      private string Ddo_managefilters_Activeeventkey ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_60_idx="0001" ;
      private string edtavBtnrole_Internalname ;
      private string AV123Pgmname ;
      private string AV40FilUserGender ;
      private string AV35FilAutType ;
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
      private string Ddo_grid_Caption ;
      private string Ddo_grid_Gridinternalname ;
      private string Ddo_grid_Columnids ;
      private string Ddo_grid_Columnssortvalues ;
      private string Ddo_grid_Fixable ;
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
      private string ClassString ;
      private string StyleString ;
      private string divTableheader_Internalname ;
      private string divTableactions_Internalname ;
      private string TempTags ;
      private string bttBtninsert_Internalname ;
      private string bttBtninsert_Jsonclick ;
      private string bttBtneditcolumns_Internalname ;
      private string bttBtneditcolumns_Jsonclick ;
      private string divGridtablewithpaginationbar_Internalname ;
      private string sStyleString ;
      private string subGrid_Internalname ;
      private string Gridpaginationbar_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string Ddo_grid_Internalname ;
      private string Ddo_gridcolumnsselector_Internalname ;
      private string edtavGamuserscount_Internalname ;
      private string edtavGamuserscount_Jsonclick ;
      private string Grid_empowerer_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV29Display ;
      private string edtavDisplay_Internalname ;
      private string AV99Update ;
      private string edtavUpdate_Internalname ;
      private string AV12BtnRole ;
      private string cmbavGridactiongroup1_Internalname ;
      private string edtavName_Internalname ;
      private string edtavEmail_Internalname ;
      private string AV42FirstName ;
      private string edtavFirstname_Internalname ;
      private string AV75LastName ;
      private string edtavLastname_Internalname ;
      private string AV8AuthenticationTypeName ;
      private string edtavAuthenticationtypename_Internalname ;
      private string AV58GUID ;
      private string edtavGuid_Internalname ;
      private string edtavStatus_Internalname ;
      private string edtavSearch_Internalname ;
      private string cmbavFilusergender_Internalname ;
      private string cmbavFilauttype_Internalname ;
      private string cmbavFilrol_Internalname ;
      private string edtavStatus_Columnheaderclass ;
      private string edtavName_Class ;
      private string edtavFirstname_Class ;
      private string edtavLastname_Class ;
      private string edtavAuthenticationtypename_Class ;
      private string edtavEmail_Class ;
      private string edtavDisplay_Link ;
      private string GXEncryptionTmp ;
      private string edtavUpdate_Link ;
      private string edtavUpdate_Class ;
      private string edtavBtnrole_Link ;
      private string edtavBtnrole_Class ;
      private string edtavName_Link ;
      private string edtavEmail_Link ;
      private string edtavStatus_Columnclass ;
      private string GXt_char3 ;
      private string tblTablerightheader_Internalname ;
      private string divTablefilters_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string Ddo_managefilters_Caption ;
      private string Ddo_managefilters_Internalname ;
      private string edtavSearch_Jsonclick ;
      private string cmbavFilusergender_Jsonclick ;
      private string cmbavFilauttype_Jsonclick ;
      private string cmbavFilrol_Jsonclick ;
      private string sGXsfl_60_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string ROClassString ;
      private string edtavDisplay_Jsonclick ;
      private string edtavUpdate_Jsonclick ;
      private string edtavBtnrole_Jsonclick ;
      private string GXCCtl ;
      private string cmbavGridactiongroup1_Jsonclick ;
      private string edtavName_Jsonclick ;
      private string edtavEmail_Jsonclick ;
      private string edtavFirstname_Jsonclick ;
      private string edtavLastname_Jsonclick ;
      private string edtavAuthenticationtypename_Jsonclick ;
      private string edtavGuid_Jsonclick ;
      private string edtavStatus_Jsonclick ;
      private string subGrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_60_Refreshing=false ;
      private bool AV111IsAuthorized_Display ;
      private bool AV112IsAuthorized_Update ;
      private bool AV113IsAuthorized_BtnRole ;
      private bool AV114IsAuthorized_BtnPermissions ;
      private bool AV115IsAuthorized_BtnSetPwd ;
      private bool AV62IsAuthorized_Authenticator ;
      private bool AV116IsAuthorized_UDelete ;
      private bool AV117IsAuthorized_ApplicationAPIKeys ;
      private bool AV118IsAuthorized_Name ;
      private bool AV119IsAuthorized_Email ;
      private bool AV120IsAuthorized_Insert ;
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
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool GXt_boolean1 ;
      private bool AV6isOK ;
      private string AV19ColumnsSelectorXML ;
      private string AV82ManageFiltersXml ;
      private string AV101UserCustomValue ;
      private string AV91Search ;
      private string AV50GridAppliedFilters ;
      private string AV83Name ;
      private string AV30Email ;
      private string AV94Status ;
      private string AV106AuxText ;
      private IGxSession AV93Session ;
      private GXWebGrid GridContainer ;
      private GXWebRow GridRow ;
      private GXWebColumn GridColumn ;
      private GXUserControl ucDvpanel_unnamedtable1 ;
      private GXUserControl ucGridpaginationbar ;
      private GXUserControl ucDdo_grid ;
      private GXUserControl ucDdo_gridcolumnsselector ;
      private GXUserControl ucGrid_empowerer ;
      private GXUserControl ucDdo_managefilters ;
      private GxHttpRequest AV59HTTPRequest ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavFilusergender ;
      private GXCombobox cmbavFilauttype ;
      private GXCombobox cmbavFilrol ;
      private GXCombobox cmbavGridactiongroup1 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV17ColumnsSelector ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV77ManageFiltersData ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV27DDO_TitleSettingsIcons ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV56GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons2 ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType> AV9AuthenticationTypes ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter AV38FilterAutType ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV33Errors ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType AV7AuthenticationType ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRole> AV90Roles ;
      private GeneXus.Programs.genexussecurity.SdtGAMRoleFilter AV39FilterRoles ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV88Repository ;
      private GeneXus.Programs.genexussecurity.SdtGAMRole AV89Role ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV103WWPContext ;
      private GeneXus.Programs.genexussecurity.SdtGAMUserFilter AV37Filter ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMUser> AV46GAMUsers ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV100User ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV104GAMUser ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV5GAMErrorCollection ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV18ColumnsSelectorAux ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> GXt_objcol_SdtDVB_SDTDropDownOptionsData_Item4 ;
      private IDataStoreProvider pr_default ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV31Error ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV57GridStateFilterValue ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamwwusers__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamwwusers__default : DataStoreHelperBase, IDataStoreHelper
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
