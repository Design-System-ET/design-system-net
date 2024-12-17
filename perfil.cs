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
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   public class perfil : GXDataArea
   {
      public perfil( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public perfil( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref string aP0_Gx_mode )
      {
         this.Gx_mode = aP0_Gx_mode;
         ExecuteImpl();
         aP0_Gx_mode=this.Gx_mode;
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         cmbavAuthenticationtypename = new GXCombobox();
         chkavDontreceiveinformation = new GXCheckbox();
         chkavEnabletwofactorauthentication = new GXCheckbox();
         cmbavLanguage = new GXCombobox();
         cmbavTheme = new GXCombobox();
         cmbavGender = new GXCombobox();
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

      protected override bool IntegratedSecurityEnabled
      {
         get {
            return true ;
         }

      }

      protected override GAMSecurityLevel IntegratedSecurityLevel
      {
         get {
            return GAMSecurityLevel.SecurityLow ;
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
         PA3K2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3K2( ) ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "perfil.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("perfil.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7Name, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")), context));
         GxWebStd.gx_hidden_field( context, "vURLPROFILE", AV34URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV34URLProfile, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"Perfil");
         forbiddenHiddens.Add("AuthenticationTypeName", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")));
         forbiddenHiddens.Add("Name", StringUtil.RTrim( context.localUtil.Format( AV7Name, "")));
         forbiddenHiddens.Add("EMail", StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("perfil:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "vURLPROFILE", AV34URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV34URLProfile, "")), context));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Width", StringUtil.RTrim( Dvpanel_unnamedtable9_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable9_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable9_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Cls", StringUtil.RTrim( Dvpanel_unnamedtable9_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Title", StringUtil.RTrim( Dvpanel_unnamedtable9_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable9_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable9_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable9_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable9_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE9_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable9_Autoscroll));
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
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Width", StringUtil.RTrim( Dvpanel_unnamedtable4_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Cls", StringUtil.RTrim( Dvpanel_unnamedtable4_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Title", StringUtil.RTrim( Dvpanel_unnamedtable4_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable4_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE4_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Autoscroll));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS1_Pagecount", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gxuitabspanel_tabs1_Pagecount), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS1_Class", StringUtil.RTrim( Gxuitabspanel_tabs1_Class));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS1_Historymanagement", StringUtil.BoolToStr( Gxuitabspanel_tabs1_Historymanagement));
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
         if ( ! ( WebComp_Wcmessages == null ) )
         {
            WebComp_Wcmessages.componentjscripts();
         }
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
            WE3K2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3K2( ) ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "perfil.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode));
         return formatLink("perfil.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "Perfil" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Perfil", "") ;
      }

      protected void WB3K0( )
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
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"bootstrapv3\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divLayoutmaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "TableMain", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom20", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable10_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable11_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTxtuser_Internalname, lblTxtuser_Caption, "", "", lblTxtuser_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "BlogSubTitle", 0, "", 1, 1, 0, 0, "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnbtntotpauthenticator_Internalname, "", bttBtnbtntotpauthenticator_Caption, bttBtnbtntotpauthenticator_Jsonclick, 5, context.GetMessage( "GAM_Enableauthenticator", ""), "", StyleString, ClassString, bttBtnbtntotpauthenticator_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOBTNTOTPAUTHENTICATOR\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnbtnedit_Internalname, "", context.GetMessage( "GAM_Edit", ""), bttBtnbtnedit_Jsonclick, 5, context.GetMessage( "GAM_Edit", ""), "", StyleString, ClassString, bttBtnbtnedit_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOBTNEDIT\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2 DscTop", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDatelastauthentication_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDatelastauthentication_Internalname, context.GetMessage( "GAM_Lastauthentication", ""), " AttributeDateTimeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'',false,'',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavDatelastauthentication_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavDatelastauthentication_Internalname, context.localUtil.TToC( AV14DateLastAuthentication, 10, 8, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "), context.localUtil.Format( AV14DateLastAuthentication, "99/99/99 99:99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onblur(this,28);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDatelastauthentication_Jsonclick, 0, "AttributeDateTime", "", "", "", "", 1, edtavDatelastauthentication_Enabled, 0, "text", "", 14, "chr", 1, "row", 14, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_Perfil.htm");
            GxWebStd.gx_bitmap( context, edtavDatelastauthentication_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(edtavDatelastauthentication_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_Perfil.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
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
            /* User Defined Control */
            ucGxuitabspanel_tabs1.SetProperty("PageCount", Gxuitabspanel_tabs1_Pagecount);
            ucGxuitabspanel_tabs1.SetProperty("Class", Gxuitabspanel_tabs1_Class);
            ucGxuitabspanel_tabs1.SetProperty("HistoryManagement", Gxuitabspanel_tabs1_Historymanagement);
            ucGxuitabspanel_tabs1.Render(context, "tab", Gxuitabspanel_tabs1_Internalname, "GXUITABSPANEL_TABS1Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABS1Container"+"title1"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTab1_title_Internalname, context.GetMessage( "General", ""), "", "", lblTab1_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Perfil.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Tab1") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABS1Container"+"panel1"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable8_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTable2_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable9.SetProperty("Width", Dvpanel_unnamedtable9_Width);
            ucDvpanel_unnamedtable9.SetProperty("AutoWidth", Dvpanel_unnamedtable9_Autowidth);
            ucDvpanel_unnamedtable9.SetProperty("AutoHeight", Dvpanel_unnamedtable9_Autoheight);
            ucDvpanel_unnamedtable9.SetProperty("Cls", Dvpanel_unnamedtable9_Cls);
            ucDvpanel_unnamedtable9.SetProperty("Title", Dvpanel_unnamedtable9_Title);
            ucDvpanel_unnamedtable9.SetProperty("Collapsible", Dvpanel_unnamedtable9_Collapsible);
            ucDvpanel_unnamedtable9.SetProperty("Collapsed", Dvpanel_unnamedtable9_Collapsed);
            ucDvpanel_unnamedtable9.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable9_Showcollapseicon);
            ucDvpanel_unnamedtable9.SetProperty("IconPosition", Dvpanel_unnamedtable9_Iconposition);
            ucDvpanel_unnamedtable9.SetProperty("AutoScroll", Dvpanel_unnamedtable9_Autoscroll);
            ucDvpanel_unnamedtable9.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable9_Internalname, "DVPANEL_UNNAMEDTABLE9Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_UNNAMEDTABLE9Container"+"UnnamedTable9"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable9_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGam_datacard_tabledatageneral_Internalname, 1, 0, "px", 0, "px", "card-body", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, "", context.GetMessage( "Image", ""), "col-sm-3 AttributeLabel", 0, true, "");
            /* Static Bitmap Variable */
            ClassString = "Attribute" + " " + ((StringUtil.StrCmp(imgavImage_gximage, "")==0) ? "" : "GX_Image_"+imgavImage_gximage+"_Class");
            StyleString = "";
            AV5Image_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV5Image))&&String.IsNullOrEmpty(StringUtil.RTrim( AV48Image_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV5Image)));
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV5Image)) ? AV48Image_GXI : context.PathToRelativeUrl( AV5Image));
            GxWebStd.gx_bitmap( context, imgavImage_Internalname, sImgUrl, "", "", "", context.GetTheme( ), imgavImage_Visible, 0, "", "", 0, -1, 0, "", 0, "", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", "", "", "", 1, AV5Image_IsBlob, false, context.GetImageSrcSet( sImgUrl), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavAuthenticationtypename.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavAuthenticationtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavAuthenticationtypename_Internalname, context.GetMessage( "GAM_AuthenticationType", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavAuthenticationtypename, cmbavAuthenticationtypename_Internalname, StringUtil.RTrim( AV6AuthenticationTypeName), 1, cmbavAuthenticationtypename_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", cmbavAuthenticationtypename.Visible, cmbavAuthenticationtypename.Enabled, 0, 0, 40, "%", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,56);\"", "", true, 0, "HLP_Perfil.htm");
            cmbavAuthenticationtypename.CurrentValue = StringUtil.RTrim( AV6AuthenticationTypeName);
            AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Values", (string)(cmbavAuthenticationtypename.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-8 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavName_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavName_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavName_Internalname, edtavName_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 61,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavName_Internalname, AV7Name, StringUtil.RTrim( context.localUtil.Format( AV7Name, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,61);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavName_Jsonclick, 0, "Attribute", "", "", "", "", edtavName_Visible, edtavName_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 col-sm-2", "start", "top", "", "", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 64,'',false,'',0)\"";
            ClassString = "Button Secondary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnbtnchangename_Internalname, "", context.GetMessage( "GAM_Changename", ""), bttBtnbtnchangename_Jsonclick, 5, context.GetMessage( "GAM_Changenickname", ""), "", StyleString, ClassString, bttBtnbtnchangename_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOBTNCHANGENAME\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-8 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmail_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmail_Internalname, edtavEmail_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 69,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmail_Internalname, AV8EMail, StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,69);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmail_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEmail_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMEMail", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 col-sm-2", "start", "top", "", "", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 72,'',false,'',0)\"";
            ClassString = "Button Secondary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnbtnchangeemail_Internalname, "", context.GetMessage( "GAM_Changeemail", ""), bttBtnbtnchangeemail_Jsonclick, 5, context.GetMessage( "GAM_Changeemail", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOBTNCHANGEEMAIL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-8 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGampassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGampassword_Internalname, context.GetMessage( "Password", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 77,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGampassword_Internalname, StringUtil.RTrim( AV43GAMPassword), StringUtil.RTrim( context.localUtil.Format( AV43GAMPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,77);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGampassword_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavGampassword_Enabled, 0, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 col-sm-2", "start", "top", "", "", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-2", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 80,'',false,'',0)\"";
            ClassString = "Button Secondary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseraction1_Internalname, "", context.GetMessage( "Cambiar Password", ""), bttBtnuseraction1_Jsonclick, 5, context.GetMessage( "Cambiar Password", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSERACTION1\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavFirstname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavFirstname_Internalname, edtavFirstname_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 85,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFirstname_Internalname, StringUtil.RTrim( AV9FirstName), StringUtil.RTrim( context.localUtil.Format( AV9FirstName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,85);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavFirstname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFirstname_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavLastname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavLastname_Internalname, edtavLastname_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 89,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavLastname_Internalname, StringUtil.RTrim( AV10LastName), StringUtil.RTrim( context.localUtil.Format( AV10LastName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,89);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavLastname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavLastname_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavPhone_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavPhone_Internalname, edtavPhone_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 93,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavPhone_Internalname, StringUtil.RTrim( AV11Phone), StringUtil.RTrim( context.localUtil.Format( AV11Phone, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,93);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavPhone_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavPhone_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavDontreceiveinformation_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavDontreceiveinformation_Internalname, context.GetMessage( "GAM_Dontwanttoreceiveinformation", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 98,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavDontreceiveinformation_Internalname, StringUtil.BoolToStr( AV12DontReceiveInformation), "", context.GetMessage( "GAM_Dontwanttoreceiveinformation", ""), 1, chkavDontreceiveinformation.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(98, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,98);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavEnabletwofactorauthentication.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEnabletwofactorauthentication_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEnabletwofactorauthentication_Internalname, context.GetMessage( "GAM_EnableTwoFactorAuthentication", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 102,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEnabletwofactorauthentication_Internalname, StringUtil.BoolToStr( AV13EnableTwoFactorAuthentication), "", context.GetMessage( "GAM_EnableTwoFactorAuthentication", ""), chkavEnabletwofactorauthentication.Visible, chkavEnabletwofactorauthentication.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(102, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,102);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABS1Container"+"title2"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTab3_title_Internalname, context.GetMessage( "Informacion Avanzada", ""), "", "", lblTab3_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Perfil.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Tab3") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABS1Container"+"panel2"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable5_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable6_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divStencil1_tabledatageneral_Internalname, 1, 0, "px", 0, "px", "card-body", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavLanguage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavLanguage_Internalname, cmbavLanguage.Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 123,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavLanguage, cmbavLanguage_Internalname, StringUtil.RTrim( AV15Language), 1, cmbavLanguage_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "svchar", "", 1, cmbavLanguage.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,123);\"", "", true, 0, "HLP_Perfil.htm");
            cmbavLanguage.CurrentValue = StringUtil.RTrim( AV15Language);
            AssignProp("", false, cmbavLanguage_Internalname, "Values", (string)(cmbavLanguage.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavTheme_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavTheme_Internalname, context.GetMessage( "GAM_Theme", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 127,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavTheme, cmbavTheme_Internalname, StringUtil.RTrim( AV16Theme), 1, cmbavTheme_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "svchar", "", 1, cmbavTheme.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,127);\"", "", true, 0, "HLP_Perfil.htm");
            cmbavTheme.CurrentValue = StringUtil.RTrim( AV16Theme);
            AssignProp("", false, cmbavTheme_Internalname, "Values", (string)(cmbavTheme.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavBirthday_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavBirthday_Internalname, edtavBirthday_Caption, " AttributeDateLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 132,'',false,'',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavBirthday_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavBirthday_Internalname, context.localUtil.Format(AV17Birthday, "99/99/9999"), context.localUtil.Format( AV17Birthday, "99/99/9999"), TempTags+" onchange=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',0,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',0,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onblur(this,132);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavBirthday_Jsonclick, 0, "AttributeDate", "", "", "", "", 1, edtavBirthday_Enabled, 1, "text", "", 10, "chr", 1, "row", 10, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMDate", "end", false, "", "HLP_Perfil.htm");
            GxWebStd.gx_bitmap( context, edtavBirthday_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(edtavBirthday_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_Perfil.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavGender_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavGender_Internalname, cmbavGender.Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 136,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavGender, cmbavGender_Internalname, StringUtil.RTrim( AV18Gender), 1, cmbavGender_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavGender.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,136);\"", "", true, 0, "HLP_Perfil.htm");
            cmbavGender.CurrentValue = StringUtil.RTrim( AV18Gender);
            AssignProp("", false, cmbavGender_Internalname, "Values", (string)(cmbavGender.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAddress_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAddress_Internalname, edtavAddress_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 141,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAddress_Internalname, StringUtil.RTrim( AV19Address), StringUtil.RTrim( context.localUtil.Format( AV19Address, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,141);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAddress_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAddress_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAddress2_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAddress2_Internalname, context.GetMessage( "GAM_Address2", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 145,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAddress2_Internalname, StringUtil.RTrim( AV20Address2), StringUtil.RTrim( context.localUtil.Format( AV20Address2, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,145);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAddress2_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAddress2_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCity_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCity_Internalname, edtavCity_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 150,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCity_Internalname, StringUtil.RTrim( AV21City), StringUtil.RTrim( context.localUtil.Format( AV21City, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,150);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCity_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCity_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavState_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavState_Internalname, edtavState_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 154,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavState_Internalname, StringUtil.RTrim( AV22State), StringUtil.RTrim( context.localUtil.Format( AV22State, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,154);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavState_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavState_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavPostcode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavPostcode_Internalname, edtavPostcode_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 159,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavPostcode_Internalname, StringUtil.RTrim( AV23PostCode), StringUtil.RTrim( context.localUtil.Format( AV23PostCode, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,159);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavPostcode_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavPostcode_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTimezone_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTimezone_Internalname, edtavTimezone_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 163,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTimezone_Internalname, StringUtil.RTrim( AV24Timezone), StringUtil.RTrim( context.localUtil.Format( AV24Timezone, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,163);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTimezone_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTimezone_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMTimeZone", "start", true, "", "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABS1Container"+"title3"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTab2_title_Internalname, context.GetMessage( "Solicitudes de Soporte", ""), "", "", lblTab2_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_Perfil.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Tab2") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABS1Container"+"panel3"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable4.SetProperty("Width", Dvpanel_unnamedtable4_Width);
            ucDvpanel_unnamedtable4.SetProperty("AutoWidth", Dvpanel_unnamedtable4_Autowidth);
            ucDvpanel_unnamedtable4.SetProperty("AutoHeight", Dvpanel_unnamedtable4_Autoheight);
            ucDvpanel_unnamedtable4.SetProperty("Cls", Dvpanel_unnamedtable4_Cls);
            ucDvpanel_unnamedtable4.SetProperty("Title", Dvpanel_unnamedtable4_Title);
            ucDvpanel_unnamedtable4.SetProperty("Collapsible", Dvpanel_unnamedtable4_Collapsible);
            ucDvpanel_unnamedtable4.SetProperty("Collapsed", Dvpanel_unnamedtable4_Collapsed);
            ucDvpanel_unnamedtable4.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable4_Showcollapseicon);
            ucDvpanel_unnamedtable4.SetProperty("IconPosition", Dvpanel_unnamedtable4_Iconposition);
            ucDvpanel_unnamedtable4.SetProperty("AutoScroll", Dvpanel_unnamedtable4_Autoscroll);
            ucDvpanel_unnamedtable4.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable4_Internalname, "DVPANEL_UNNAMEDTABLE4Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_UNNAMEDTABLE4Container"+"UnnamedTable4"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable4_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom20", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "W0179"+"", StringUtil.RTrim( WebComp_Wcmessages_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0179"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldWcmessages), StringUtil.Lower( WebComp_Wcmessages_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0179"+"");
                  }
                  WebComp_Wcmessages.componentdraw();
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldWcmessages), StringUtil.Lower( WebComp_Wcmessages_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspEndCmp();
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGam_footerentry_Internalname, divGam_footerentry_Visible, 0, "px", 0, "px", "SectionAbout", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom20", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 187,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtngam_footerentry_btnconfirm_Internalname, "", context.GetMessage( "GAM_Confirm", ""), bttBtngam_footerentry_btnconfirm_Jsonclick, 5, context.GetMessage( "GAM_Confirm", ""), "", StyleString, ClassString, bttBtngam_footerentry_btnconfirm_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOGAM_FOOTERENTRY_BTNCONFIRM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 189,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, bttBtncancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_Perfil.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START3K2( )
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
         Form.Meta.addItem("description", context.GetMessage( "Perfil", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3K0( ) ;
      }

      protected void WS3K2( )
      {
         START3K2( ) ;
         EVT3K2( ) ;
      }

      protected void EVT3K2( )
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
                           else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Start */
                              E113K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOGAM_FOOTERENTRY_BTNCONFIRM'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoGAM_FooterEntry_BtnConfirm' */
                              E123K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOBTNCHANGENAME'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoBtnChangeName' */
                              E133K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOBTNCHANGEEMAIL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoBtnChangeEmail' */
                              E143K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOUSERACTION1'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoUserAction1' */
                              E153K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOBTNTOTPAUTHENTICATOR'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoBtnTOTPAuthenticator' */
                              E163K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOBTNEDIT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoBtnEdit' */
                              E173K2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E183K2 ();
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
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                        }
                     }
                     else if ( StringUtil.StrCmp(sEvtType, "W") == 0 )
                     {
                        sEvtType = StringUtil.Left( sEvt, 4);
                        sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-4));
                        nCmpId = (short)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                        if ( nCmpId == 179 )
                        {
                           OldWcmessages = cgiGet( "W0179");
                           if ( ( StringUtil.Len( OldWcmessages) == 0 ) || ( StringUtil.StrCmp(OldWcmessages, WebComp_Wcmessages_Component) != 0 ) )
                           {
                              WebComp_Wcmessages = getWebComponent(GetType(), "DesignSystem.Programs", OldWcmessages, new Object[] {context} );
                              WebComp_Wcmessages.ComponentInit();
                              WebComp_Wcmessages.Name = "OldWcmessages";
                              WebComp_Wcmessages_Component = OldWcmessages;
                           }
                           if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
                           {
                              WebComp_Wcmessages.componentprocess("W0179", "", sEvt);
                           }
                           WebComp_Wcmessages_Component = OldWcmessages;
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE3K2( )
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

      protected void PA3K2( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "perfil.aspx")), "perfil.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "perfil.aspx")))) ;
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
               GX_FocusControl = edtavDatelastauthentication_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
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
         if ( cmbavAuthenticationtypename.ItemCount > 0 )
         {
            AV6AuthenticationTypeName = cmbavAuthenticationtypename.getValidValue(AV6AuthenticationTypeName);
            AssignAttri("", false, "AV6AuthenticationTypeName", AV6AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")), context));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavAuthenticationtypename.CurrentValue = StringUtil.RTrim( AV6AuthenticationTypeName);
            AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Values", cmbavAuthenticationtypename.ToJavascriptSource(), true);
         }
         AV12DontReceiveInformation = StringUtil.StrToBool( StringUtil.BoolToStr( AV12DontReceiveInformation));
         AssignAttri("", false, "AV12DontReceiveInformation", AV12DontReceiveInformation);
         AV13EnableTwoFactorAuthentication = StringUtil.StrToBool( StringUtil.BoolToStr( AV13EnableTwoFactorAuthentication));
         AssignAttri("", false, "AV13EnableTwoFactorAuthentication", AV13EnableTwoFactorAuthentication);
         if ( cmbavLanguage.ItemCount > 0 )
         {
            AV15Language = cmbavLanguage.getValidValue(AV15Language);
            AssignAttri("", false, "AV15Language", AV15Language);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavLanguage.CurrentValue = StringUtil.RTrim( AV15Language);
            AssignProp("", false, cmbavLanguage_Internalname, "Values", cmbavLanguage.ToJavascriptSource(), true);
         }
         if ( cmbavTheme.ItemCount > 0 )
         {
            AV16Theme = cmbavTheme.getValidValue(AV16Theme);
            AssignAttri("", false, "AV16Theme", AV16Theme);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavTheme.CurrentValue = StringUtil.RTrim( AV16Theme);
            AssignProp("", false, cmbavTheme_Internalname, "Values", cmbavTheme.ToJavascriptSource(), true);
         }
         if ( cmbavGender.ItemCount > 0 )
         {
            AV18Gender = cmbavGender.getValidValue(AV18Gender);
            AssignAttri("", false, "AV18Gender", AV18Gender);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavGender.CurrentValue = StringUtil.RTrim( AV18Gender);
            AssignProp("", false, cmbavGender_Internalname, "Values", cmbavGender.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3K2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavDatelastauthentication_Enabled = 0;
         AssignProp("", false, edtavDatelastauthentication_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDatelastauthentication_Enabled), 5, 0), true);
         cmbavAuthenticationtypename.Enabled = 0;
         AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavAuthenticationtypename.Enabled), 5, 0), true);
         edtavName_Enabled = 0;
         AssignProp("", false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
         edtavEmail_Enabled = 0;
         AssignProp("", false, edtavEmail_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEmail_Enabled), 5, 0), true);
         edtavGampassword_Enabled = 0;
         AssignProp("", false, edtavGampassword_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGampassword_Enabled), 5, 0), true);
      }

      protected void RF3K2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( 1 != 0 )
            {
               if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
               {
                  WebComp_Wcmessages.componentstart();
               }
            }
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E183K2 ();
            WB3K0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3K2( )
      {
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7Name, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")), context));
         GxWebStd.gx_hidden_field( context, "vURLPROFILE", AV34URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV34URLProfile, "")), context));
      }

      protected void before_start_formulas( )
      {
         edtavDatelastauthentication_Enabled = 0;
         AssignProp("", false, edtavDatelastauthentication_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDatelastauthentication_Enabled), 5, 0), true);
         cmbavAuthenticationtypename.Enabled = 0;
         AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavAuthenticationtypename.Enabled), 5, 0), true);
         edtavName_Enabled = 0;
         AssignProp("", false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
         edtavEmail_Enabled = 0;
         AssignProp("", false, edtavEmail_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEmail_Enabled), 5, 0), true);
         edtavGampassword_Enabled = 0;
         AssignProp("", false, edtavGampassword_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGampassword_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3K0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E113K2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            Dvpanel_unnamedtable9_Width = cgiGet( "DVPANEL_UNNAMEDTABLE9_Width");
            Dvpanel_unnamedtable9_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE9_Autowidth"));
            Dvpanel_unnamedtable9_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE9_Autoheight"));
            Dvpanel_unnamedtable9_Cls = cgiGet( "DVPANEL_UNNAMEDTABLE9_Cls");
            Dvpanel_unnamedtable9_Title = cgiGet( "DVPANEL_UNNAMEDTABLE9_Title");
            Dvpanel_unnamedtable9_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE9_Collapsible"));
            Dvpanel_unnamedtable9_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE9_Collapsed"));
            Dvpanel_unnamedtable9_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE9_Showcollapseicon"));
            Dvpanel_unnamedtable9_Iconposition = cgiGet( "DVPANEL_UNNAMEDTABLE9_Iconposition");
            Dvpanel_unnamedtable9_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE9_Autoscroll"));
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
            Dvpanel_unnamedtable4_Width = cgiGet( "DVPANEL_UNNAMEDTABLE4_Width");
            Dvpanel_unnamedtable4_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE4_Autowidth"));
            Dvpanel_unnamedtable4_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE4_Autoheight"));
            Dvpanel_unnamedtable4_Cls = cgiGet( "DVPANEL_UNNAMEDTABLE4_Cls");
            Dvpanel_unnamedtable4_Title = cgiGet( "DVPANEL_UNNAMEDTABLE4_Title");
            Dvpanel_unnamedtable4_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE4_Collapsible"));
            Dvpanel_unnamedtable4_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE4_Collapsed"));
            Dvpanel_unnamedtable4_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE4_Showcollapseicon"));
            Dvpanel_unnamedtable4_Iconposition = cgiGet( "DVPANEL_UNNAMEDTABLE4_Iconposition");
            Dvpanel_unnamedtable4_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE4_Autoscroll"));
            Gxuitabspanel_tabs1_Pagecount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GXUITABSPANEL_TABS1_Pagecount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gxuitabspanel_tabs1_Class = cgiGet( "GXUITABSPANEL_TABS1_Class");
            Gxuitabspanel_tabs1_Historymanagement = StringUtil.StrToBool( cgiGet( "GXUITABSPANEL_TABS1_Historymanagement"));
            /* Read variables values. */
            if ( context.localUtil.VCDateTime( cgiGet( edtavDatelastauthentication_Internalname), (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt"))), (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0))) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_baddatetime", new   object[]  {context.GetMessage( "Date Last Authentication", "")}), 1, "vDATELASTAUTHENTICATION");
               GX_FocusControl = edtavDatelastauthentication_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV14DateLastAuthentication = (DateTime)(DateTime.MinValue);
               AssignAttri("", false, "AV14DateLastAuthentication", context.localUtil.TToC( AV14DateLastAuthentication, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            else
            {
               AV14DateLastAuthentication = context.localUtil.CToT( cgiGet( edtavDatelastauthentication_Internalname));
               AssignAttri("", false, "AV14DateLastAuthentication", context.localUtil.TToC( AV14DateLastAuthentication, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            AV5Image = cgiGet( imgavImage_Internalname);
            cmbavAuthenticationtypename.CurrentValue = cgiGet( cmbavAuthenticationtypename_Internalname);
            AV6AuthenticationTypeName = cgiGet( cmbavAuthenticationtypename_Internalname);
            AssignAttri("", false, "AV6AuthenticationTypeName", AV6AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")), context));
            AV7Name = cgiGet( edtavName_Internalname);
            AssignAttri("", false, "AV7Name", AV7Name);
            GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7Name, "")), context));
            AV8EMail = cgiGet( edtavEmail_Internalname);
            AssignAttri("", false, "AV8EMail", AV8EMail);
            GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")), context));
            AV43GAMPassword = cgiGet( edtavGampassword_Internalname);
            AssignAttri("", false, "AV43GAMPassword", AV43GAMPassword);
            AV9FirstName = cgiGet( edtavFirstname_Internalname);
            AssignAttri("", false, "AV9FirstName", AV9FirstName);
            AV10LastName = cgiGet( edtavLastname_Internalname);
            AssignAttri("", false, "AV10LastName", AV10LastName);
            AV11Phone = cgiGet( edtavPhone_Internalname);
            AssignAttri("", false, "AV11Phone", AV11Phone);
            AV12DontReceiveInformation = StringUtil.StrToBool( cgiGet( chkavDontreceiveinformation_Internalname));
            AssignAttri("", false, "AV12DontReceiveInformation", AV12DontReceiveInformation);
            AV13EnableTwoFactorAuthentication = StringUtil.StrToBool( cgiGet( chkavEnabletwofactorauthentication_Internalname));
            AssignAttri("", false, "AV13EnableTwoFactorAuthentication", AV13EnableTwoFactorAuthentication);
            cmbavLanguage.CurrentValue = cgiGet( cmbavLanguage_Internalname);
            AV15Language = cgiGet( cmbavLanguage_Internalname);
            AssignAttri("", false, "AV15Language", AV15Language);
            cmbavTheme.CurrentValue = cgiGet( cmbavTheme_Internalname);
            AV16Theme = cgiGet( cmbavTheme_Internalname);
            AssignAttri("", false, "AV16Theme", AV16Theme);
            if ( context.localUtil.VCDate( cgiGet( edtavBirthday_Internalname), (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")))) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_faildate", new   object[]  {context.GetMessage( "Birthday", "")}), 1, "vBIRTHDAY");
               GX_FocusControl = edtavBirthday_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV17Birthday = DateTime.MinValue;
               AssignAttri("", false, "AV17Birthday", context.localUtil.Format(AV17Birthday, "99/99/9999"));
            }
            else
            {
               AV17Birthday = context.localUtil.CToD( cgiGet( edtavBirthday_Internalname), DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")));
               AssignAttri("", false, "AV17Birthday", context.localUtil.Format(AV17Birthday, "99/99/9999"));
            }
            cmbavGender.CurrentValue = cgiGet( cmbavGender_Internalname);
            AV18Gender = cgiGet( cmbavGender_Internalname);
            AssignAttri("", false, "AV18Gender", AV18Gender);
            AV19Address = cgiGet( edtavAddress_Internalname);
            AssignAttri("", false, "AV19Address", AV19Address);
            AV20Address2 = cgiGet( edtavAddress2_Internalname);
            AssignAttri("", false, "AV20Address2", AV20Address2);
            AV21City = cgiGet( edtavCity_Internalname);
            AssignAttri("", false, "AV21City", AV21City);
            AV22State = cgiGet( edtavState_Internalname);
            AssignAttri("", false, "AV22State", AV22State);
            AV23PostCode = cgiGet( edtavPostcode_Internalname);
            AssignAttri("", false, "AV23PostCode", AV23PostCode);
            AV24Timezone = cgiGet( edtavTimezone_Internalname);
            AssignAttri("", false, "AV24Timezone", AV24Timezone);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"Perfil");
            AV6AuthenticationTypeName = cgiGet( cmbavAuthenticationtypename_Internalname);
            AssignAttri("", false, "AV6AuthenticationTypeName", AV6AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")), context));
            forbiddenHiddens.Add("AuthenticationTypeName", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")));
            AV7Name = cgiGet( edtavName_Internalname);
            AssignAttri("", false, "AV7Name", AV7Name);
            GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7Name, "")), context));
            forbiddenHiddens.Add("Name", StringUtil.RTrim( context.localUtil.Format( AV7Name, "")));
            AV8EMail = cgiGet( edtavEmail_Internalname);
            AssignAttri("", false, "AV8EMail", AV8EMail);
            GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")), context));
            forbiddenHiddens.Add("EMail", StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("perfil:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
               GxWebError = 1;
               context.HttpContext.Response.StatusCode = 403;
               context.WriteHtmlText( "<title>403 Forbidden</title>") ;
               context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
               context.WriteHtmlText( "<p /><hr />") ;
               GXUtil.WriteLog("send_http_error_code " + 403.ToString());
               return  ;
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
         E113K2 ();
         if (returnInSub) return;
      }

      protected void E113K2( )
      {
         /* Start Routine */
         returnInSub = false;
         bttBtnbtnedit_Visible = 0;
         AssignProp("", false, bttBtnbtnedit_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnbtnedit_Visible), 5, 0), true);
         bttBtnbtntotpauthenticator_Visible = 0;
         AssignProp("", false, bttBtnbtntotpauthenticator_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnbtntotpauthenticator_Visible), 5, 0), true);
         cmbavAuthenticationtypename.Visible = 0;
         AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavAuthenticationtypename.Visible), 5, 0), true);
         divGam_footerentry_Visible = 0;
         AssignProp("", false, divGam_footerentry_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divGam_footerentry_Visible), 5, 0), true);
         /* Execute user subroutine: 'SHOWMESSAGES' */
         S112 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'INITFORM' */
         S122 ();
         if (returnInSub) return;
      }

      protected void E123K2( )
      {
         /* 'DoGAM_FooterEntry_BtnConfirm' Routine */
         returnInSub = false;
         AV33gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         AV33gamUser.load( AV33gamUser.gxTpr_Guid);
         if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
         {
            divGam_footerentry_Visible = 1;
            AssignProp("", false, divGam_footerentry_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divGam_footerentry_Visible), 5, 0), true);
            AV33gamUser.gxTpr_Authenticationtypename = AV6AuthenticationTypeName;
            AV33gamUser.gxTpr_Name = AV7Name;
            AV33gamUser.gxTpr_Email = AV8EMail;
            AV33gamUser.gxTpr_Firstname = AV9FirstName;
            AV33gamUser.gxTpr_Lastname = AV10LastName;
            AV33gamUser.gxTpr_Birthday = AV17Birthday;
            AV33gamUser.gxTpr_Gender = AV18Gender;
            AV33gamUser.gxTpr_Phone = AV11Phone;
            AV33gamUser.gxTpr_Address = AV19Address;
            AV33gamUser.gxTpr_Address2 = AV20Address2;
            AV33gamUser.gxTpr_City = AV21City;
            AV33gamUser.gxTpr_State = AV22State;
            AV33gamUser.gxTpr_Theme = AV16Theme;
            AV33gamUser.gxTpr_Timezone = AV24Timezone;
            AV33gamUser.gxTpr_Urlprofile = AV34URLProfile;
            AV33gamUser.gxTpr_Dontreceiveinformation = AV12DontReceiveInformation;
            AV33gamUser.gxTpr_Enabletwofactorauthentication = AV13EnableTwoFactorAuthentication;
            AV25GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
            AV31GAMLanguages = AV25GAMApplication.gxTpr_Languages;
            AV45GXV1 = 1;
            while ( AV45GXV1 <= AV31GAMLanguages.Count )
            {
               AV30GAMLanguage = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage)AV31GAMLanguages.Item(AV45GXV1));
               if ( StringUtil.StrCmp(AV30GAMLanguage.gxTpr_Culture, AV15Language) == 0 )
               {
                  AV33gamUser.gxTpr_Language = AV30GAMLanguage.gxTpr_Culture;
                  if (true) break;
               }
               AV45GXV1 = (int)(AV45GXV1+1);
            }
            AV33gamUser.save();
            if ( AV33gamUser.success() )
            {
               context.CommitDataStores("perfil",pr_default);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "perfil.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
               CallWebObject(formatLink("perfil.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
               context.wjLocDisableFrm = 1;
            }
            else
            {
               AV29GAMErrorCollection = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrors();
               AV46GXV2 = 1;
               while ( AV46GXV2 <= AV29GAMErrorCollection.Count )
               {
                  AV28GAMError = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV29GAMErrorCollection.Item(AV46GXV2));
                  GX_msglist.addItem(AV28GAMError.gxTpr_Message);
                  AV46GXV2 = (int)(AV46GXV2+1);
               }
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E133K2( )
      {
         /* 'DoBtnChangeName' Routine */
         returnInSub = false;
         /* Window Datatype Object Property */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamexampleuserchangeidentification.aspx"+UrlEncode(StringUtil.RTrim("Name"));
         AV35Window.Url = formatLink("gamexampleuserchangeidentification.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         AV35Window.SetReturnParms(new Object[] {"",});
         context.NewWindow(AV35Window);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "perfil.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
         CallWebObject(formatLink("perfil.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void E143K2( )
      {
         /* 'DoBtnChangeEmail' Routine */
         returnInSub = false;
         /* Window Datatype Object Property */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamexampleuserchangeidentification.aspx"+UrlEncode(StringUtil.RTrim("Email"));
         AV35Window.Url = formatLink("gamexampleuserchangeidentification.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         AV35Window.SetReturnParms(new Object[] {"",});
         context.NewWindow(AV35Window);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "perfil.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
         CallWebObject(formatLink("perfil.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void E153K2( )
      {
         /* 'DoUserAction1' Routine */
         returnInSub = false;
         context.PopUp(formatLink("gamchangeyourpassword.aspx") , new Object[] {});
      }

      protected void E163K2( )
      {
         /* 'DoBtnTOTPAuthenticator' Routine */
         returnInSub = false;
         AV33gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         if ( AV33gamUser.gxTpr_Totpenable )
         {
            /* Window Datatype Object Property */
            AV35Window.Url = formatLink("gamexampleauthenticatordisable.aspx") ;
            AV35Window.SetReturnParms(new Object[] {});
            context.NewWindow(AV35Window);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "perfil.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
            CallWebObject(formatLink("perfil.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            CallWebObject(formatLink("gamexampleauthenticatorenable.aspx") );
            context.wjLocDisableFrm = 1;
         }
         /*  Sending Event outputs  */
      }

      protected void E173K2( )
      {
         /* 'DoBtnEdit' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "perfil.aspx"+UrlEncode(StringUtil.RTrim("UPD"));
         CallWebObject(formatLink("perfil.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
      }

      protected void S122( )
      {
         /* 'INITFORM' Routine */
         returnInSub = false;
         AV32GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
         AV33gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         if ( AV32GAMRepository.istotpauthenticatorenabled() )
         {
            bttBtnbtntotpauthenticator_Visible = 1;
            AssignProp("", false, bttBtnbtntotpauthenticator_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnbtntotpauthenticator_Visible), 5, 0), true);
            if ( AV33gamUser.gxTpr_Totpenable )
            {
               bttBtnbtntotpauthenticator_Caption = context.GetMessage( "GAM_Disableauthenticator", "");
               AssignProp("", false, bttBtnbtntotpauthenticator_Internalname, "Caption", bttBtnbtntotpauthenticator_Caption, true);
            }
            else
            {
               bttBtnbtntotpauthenticator_Caption = context.GetMessage( "GAM_Enableauthenticator", "");
               AssignProp("", false, bttBtnbtntotpauthenticator_Internalname, "Caption", bttBtnbtntotpauthenticator_Caption, true);
            }
         }
         /* Execute user subroutine: 'MARKREQUIEREDUSERDATA' */
         S132 ();
         if (returnInSub) return;
         if ( (0==AV32GAMRepository.gxTpr_Authenticationmasterrepositoryid) )
         {
            cmbavAuthenticationtypename.removeAllItems();
            AV26AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV15Language, out  AV29GAMErrorCollection);
            AV47GXV3 = 1;
            while ( AV47GXV3 <= AV26AuthenticationTypes.Count )
            {
               AV27AuthenticationTypesIns = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV26AuthenticationTypes.Item(AV47GXV3));
               cmbavAuthenticationtypename.addItem(AV27AuthenticationTypesIns.gxTpr_Name, AV27AuthenticationTypesIns.gxTpr_Description, 0);
               AV47GXV3 = (int)(AV47GXV3+1);
            }
         }
         else
         {
            cmbavAuthenticationtypename.Visible = 0;
            AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavAuthenticationtypename.Visible), 5, 0), true);
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "UPD") != 0 ) && ( StringUtil.StrCmp(Gx_mode, "DSP") != 0 ) )
         {
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         if ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 )
         {
            bttBtncancel_Visible = 0;
            AssignProp("", false, bttBtncancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtncancel_Visible), 5, 0), true);
         }
         AV6AuthenticationTypeName = AV33gamUser.gxTpr_Authenticationtypename;
         AssignAttri("", false, "AV6AuthenticationTypeName", AV6AuthenticationTypeName);
         GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")), context));
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV33gamUser.gxTpr_Firstname)) && String.IsNullOrEmpty(StringUtil.RTrim( AV33gamUser.gxTpr_Lastname)) )
         {
            lblTxtuser_Caption = AV33gamUser.gxTpr_Name;
            AssignProp("", false, lblTxtuser_Internalname, "Caption", lblTxtuser_Caption, true);
         }
         else
         {
            lblTxtuser_Caption = StringUtil.Format( "%1 %2", AV33gamUser.gxTpr_Firstname, AV33gamUser.gxTpr_Lastname, "", "", "", "", "", "", "");
            AssignProp("", false, lblTxtuser_Internalname, "Caption", lblTxtuser_Caption, true);
         }
         /* Execute user subroutine: 'LOADLANGUAGES' */
         S142 ();
         if (returnInSub) return;
         if ( StringUtil.StrCmp(AV32GAMRepository.gxTpr_Useridentification, "email") == 0 )
         {
            edtavName_Visible = 0;
            AssignProp("", false, edtavName_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavName_Visible), 5, 0), true);
            bttBtnbtnchangename_Visible = 0;
            AssignProp("", false, bttBtnbtnchangename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnbtnchangename_Visible), 5, 0), true);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV33gamUser.gxTpr_Urlimage)) )
         {
            AV5Image = GXUtil.UrlEncode( AV33gamUser.gxTpr_Urlimage);
            AssignProp("", false, imgavImage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV5Image)) ? AV48Image_GXI : context.convertURL( context.PathToRelativeUrl( AV5Image))), true);
            AssignProp("", false, imgavImage_Internalname, "SrcSet", context.GetImageSrcSet( AV5Image), true);
            AV48Image_GXI = GXDbFile.PathToUrl( GXUtil.UrlEncode( AV33gamUser.gxTpr_Urlimage), context);
            AssignProp("", false, imgavImage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV5Image)) ? AV48Image_GXI : context.convertURL( context.PathToRelativeUrl( AV5Image))), true);
            AssignProp("", false, imgavImage_Internalname, "SrcSet", context.GetImageSrcSet( AV5Image), true);
         }
         else
         {
            imgavImage_Visible = 0;
            AssignProp("", false, imgavImage_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavImage_Visible), 5, 0), true);
         }
         AV7Name = AV33gamUser.gxTpr_Name;
         AssignAttri("", false, "AV7Name", AV7Name);
         GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7Name, "")), context));
         AV8EMail = AV33gamUser.gxTpr_Email;
         AssignAttri("", false, "AV8EMail", AV8EMail);
         GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8EMail, "")), context));
         AV9FirstName = AV33gamUser.gxTpr_Firstname;
         AssignAttri("", false, "AV9FirstName", AV9FirstName);
         AV10LastName = AV33gamUser.gxTpr_Lastname;
         AssignAttri("", false, "AV10LastName", AV10LastName);
         AV17Birthday = AV33gamUser.gxTpr_Birthday;
         AssignAttri("", false, "AV17Birthday", context.localUtil.Format(AV17Birthday, "99/99/9999"));
         AV18Gender = AV33gamUser.gxTpr_Gender;
         AssignAttri("", false, "AV18Gender", AV18Gender);
         AV11Phone = AV33gamUser.gxTpr_Phone;
         AssignAttri("", false, "AV11Phone", AV11Phone);
         AV19Address = AV33gamUser.gxTpr_Address;
         AssignAttri("", false, "AV19Address", AV19Address);
         AV20Address2 = AV33gamUser.gxTpr_Address2;
         AssignAttri("", false, "AV20Address2", AV20Address2);
         AV21City = AV33gamUser.gxTpr_City;
         AssignAttri("", false, "AV21City", AV21City);
         AV22State = AV33gamUser.gxTpr_State;
         AssignAttri("", false, "AV22State", AV22State);
         AV23PostCode = AV33gamUser.gxTpr_Postcode;
         AssignAttri("", false, "AV23PostCode", AV23PostCode);
         AV15Language = AV33gamUser.gxTpr_Language;
         AssignAttri("", false, "AV15Language", AV15Language);
         AV16Theme = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).gettheme();
         AssignAttri("", false, "AV16Theme", AV16Theme);
         AV24Timezone = AV33gamUser.gxTpr_Timezone;
         AssignAttri("", false, "AV24Timezone", AV24Timezone);
         AV34URLProfile = AV33gamUser.gxTpr_Urlprofile;
         AssignAttri("", false, "AV34URLProfile", AV34URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV34URLProfile, "")), context));
         AV12DontReceiveInformation = AV33gamUser.gxTpr_Dontreceiveinformation;
         AssignAttri("", false, "AV12DontReceiveInformation", AV12DontReceiveInformation);
         AV14DateLastAuthentication = AV33gamUser.gxTpr_Datelastauthentication;
         AssignAttri("", false, "AV14DateLastAuthentication", context.localUtil.TToC( AV14DateLastAuthentication, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
         AV13EnableTwoFactorAuthentication = AV33gamUser.gxTpr_Enabletwofactorauthentication;
         AssignAttri("", false, "AV13EnableTwoFactorAuthentication", AV13EnableTwoFactorAuthentication);
         if ( AV32GAMRepository.istwofactorauthenticationenabled() )
         {
            chkavEnabletwofactorauthentication.Visible = 1;
            AssignProp("", false, chkavEnabletwofactorauthentication_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavEnabletwofactorauthentication.Visible), 5, 0), true);
         }
         else
         {
            chkavEnabletwofactorauthentication.Visible = 0;
            AssignProp("", false, chkavEnabletwofactorauthentication_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavEnabletwofactorauthentication.Visible), 5, 0), true);
         }
         if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
         {
            divGam_footerentry_Visible = 1;
            AssignProp("", false, divGam_footerentry_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divGam_footerentry_Visible), 5, 0), true);
         }
         if ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 )
         {
            bttBtngam_footerentry_btnconfirm_Visible = 0;
            AssignProp("", false, bttBtngam_footerentry_btnconfirm_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtngam_footerentry_btnconfirm_Visible), 5, 0), true);
            bttBtnbtnedit_Visible = 1;
            AssignProp("", false, bttBtnbtnedit_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnbtnedit_Visible), 5, 0), true);
            edtavFirstname_Enabled = 0;
            AssignProp("", false, edtavFirstname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavFirstname_Enabled), 5, 0), true);
            edtavLastname_Enabled = 0;
            AssignProp("", false, edtavLastname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavLastname_Enabled), 5, 0), true);
            edtavBirthday_Enabled = 0;
            AssignProp("", false, edtavBirthday_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavBirthday_Enabled), 5, 0), true);
            cmbavGender.Enabled = 0;
            AssignProp("", false, cmbavGender_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavGender.Enabled), 5, 0), true);
            edtavPhone_Enabled = 0;
            AssignProp("", false, edtavPhone_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavPhone_Enabled), 5, 0), true);
            edtavAddress_Enabled = 0;
            AssignProp("", false, edtavAddress_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavAddress_Enabled), 5, 0), true);
            edtavAddress2_Enabled = 0;
            AssignProp("", false, edtavAddress2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavAddress2_Enabled), 5, 0), true);
            edtavCity_Enabled = 0;
            AssignProp("", false, edtavCity_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCity_Enabled), 5, 0), true);
            edtavState_Enabled = 0;
            AssignProp("", false, edtavState_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavState_Enabled), 5, 0), true);
            edtavPostcode_Enabled = 0;
            AssignProp("", false, edtavPostcode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavPostcode_Enabled), 5, 0), true);
            cmbavLanguage.Enabled = 0;
            AssignProp("", false, cmbavLanguage_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavLanguage.Enabled), 5, 0), true);
            cmbavTheme.Enabled = 0;
            AssignProp("", false, cmbavTheme_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavTheme.Enabled), 5, 0), true);
            edtavTimezone_Enabled = 0;
            AssignProp("", false, edtavTimezone_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTimezone_Enabled), 5, 0), true);
            chkavDontreceiveinformation.Enabled = 0;
            AssignProp("", false, chkavDontreceiveinformation_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavDontreceiveinformation.Enabled), 5, 0), true);
            chkavEnabletwofactorauthentication.Enabled = 0;
            AssignProp("", false, chkavEnabletwofactorauthentication_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavEnabletwofactorauthentication.Enabled), 5, 0), true);
         }
      }

      protected void S132( )
      {
         /* 'MARKREQUIEREDUSERDATA' Routine */
         returnInSub = false;
         edtavName_Caption = edtavName_Caption+"  *";
         AssignProp("", false, edtavName_Internalname, "Caption", edtavName_Caption, true);
         if ( ( StringUtil.StrCmp(AV32GAMRepository.gxTpr_Useridentification, "email") == 0 ) || ( StringUtil.StrCmp(AV32GAMRepository.gxTpr_Useridentification, "namema") == 0 ) )
         {
            if ( AV32GAMRepository.gxTpr_Requiredemail )
            {
               edtavEmail_Caption = edtavEmail_Caption+"  *";
               AssignProp("", false, edtavEmail_Internalname, "Caption", edtavEmail_Caption, true);
            }
         }
         if ( AV32GAMRepository.gxTpr_Requiredfirstname )
         {
            edtavFirstname_Caption = edtavFirstname_Caption+"  *";
            AssignProp("", false, edtavFirstname_Internalname, "Caption", edtavFirstname_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredlastname )
         {
            edtavLastname_Caption = edtavLastname_Caption+"  *";
            AssignProp("", false, edtavLastname_Internalname, "Caption", edtavLastname_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredphone )
         {
            edtavPhone_Caption = edtavPhone_Caption+"  *";
            AssignProp("", false, edtavPhone_Internalname, "Caption", edtavPhone_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredbirthday )
         {
            edtavBirthday_Caption = edtavBirthday_Caption+"  *";
            AssignProp("", false, edtavBirthday_Internalname, "Caption", edtavBirthday_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredgender )
         {
            cmbavGender.Caption = cmbavGender.Caption+"  *";
            AssignProp("", false, cmbavGender_Internalname, "Caption", cmbavGender.Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredaddress )
         {
            edtavAddress_Caption = edtavAddress_Caption+"  *";
            AssignProp("", false, edtavAddress_Internalname, "Caption", edtavAddress_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredcity )
         {
            edtavCity_Caption = edtavCity_Caption+"  *";
            AssignProp("", false, edtavCity_Internalname, "Caption", edtavCity_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredstate )
         {
            edtavState_Caption = edtavState_Caption+"  *";
            AssignProp("", false, edtavState_Internalname, "Caption", edtavState_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredpostcode )
         {
            edtavPostcode_Caption = edtavPostcode_Caption+"  *";
            AssignProp("", false, edtavPostcode_Internalname, "Caption", edtavPostcode_Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredlanguage )
         {
            cmbavLanguage.Caption = cmbavLanguage.Caption+"  *";
            AssignProp("", false, cmbavLanguage_Internalname, "Caption", cmbavLanguage.Caption, true);
         }
         if ( AV32GAMRepository.gxTpr_Requiredtimezone )
         {
            edtavTimezone_Caption = edtavTimezone_Caption+"  *";
            AssignProp("", false, edtavTimezone_Internalname, "Caption", edtavTimezone_Caption, true);
         }
      }

      protected void S112( )
      {
         /* 'SHOWMESSAGES' Routine */
         returnInSub = false;
         /* Object Property */
         if ( true )
         {
            bDynCreated_Wcmessages = true;
         }
         if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Wcmessages_Component), StringUtil.Lower( "GAMExampleMessages")) != 0 )
         {
            WebComp_Wcmessages = getWebComponent(GetType(), "DesignSystem.Programs", "gamexamplemessages", new Object[] {context} );
            WebComp_Wcmessages.ComponentInit();
            WebComp_Wcmessages.Name = "GAMExampleMessages";
            WebComp_Wcmessages_Component = "GAMExampleMessages";
         }
         if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
         {
            WebComp_Wcmessages.setjustcreated();
            WebComp_Wcmessages.componentprepare(new Object[] {(string)"W0179",(string)""});
            WebComp_Wcmessages.componentbind(new Object[] {});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Wcmessages )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0179"+"");
            WebComp_Wcmessages.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
      }

      protected void S142( )
      {
         /* 'LOADLANGUAGES' Routine */
         returnInSub = false;
         AV25GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
         AV31GAMLanguages = AV25GAMApplication.gxTpr_Languages;
         AV49GXV4 = 1;
         while ( AV49GXV4 <= AV31GAMLanguages.Count )
         {
            AV30GAMLanguage = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage)AV31GAMLanguages.Item(AV49GXV4));
            if ( AV30GAMLanguage.gxTpr_Online )
            {
               cmbavLanguage.addItem(AV30GAMLanguage.gxTpr_Culture, AV30GAMLanguage.gxTpr_Description, 0);
            }
            AV49GXV4 = (int)(AV49GXV4+1);
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E183K2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         Gx_mode = (string)getParm(obj,0);
         AssignAttri("", false, "Gx_mode", Gx_mode);
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
         PA3K2( ) ;
         WS3K2( ) ;
         WE3K2( ) ;
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
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Wcmessages == null ) )
         {
            if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
            {
               WebComp_Wcmessages.componentthemes();
            }
         }
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217091482", true, true);
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
         context.AddJavascriptSource("perfil.js", "?20241217091487", false, true);
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
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavAuthenticationtypename.Name = "vAUTHENTICATIONTYPENAME";
         cmbavAuthenticationtypename.WebTags = "";
         if ( cmbavAuthenticationtypename.ItemCount > 0 )
         {
            AV6AuthenticationTypeName = cmbavAuthenticationtypename.getValidValue(AV6AuthenticationTypeName);
            AssignAttri("", false, "AV6AuthenticationTypeName", AV6AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationTypeName, "")), context));
         }
         chkavDontreceiveinformation.Name = "vDONTRECEIVEINFORMATION";
         chkavDontreceiveinformation.WebTags = "";
         chkavDontreceiveinformation.Caption = context.GetMessage( "GAM_Dontwanttoreceiveinformation", "");
         AssignProp("", false, chkavDontreceiveinformation_Internalname, "TitleCaption", chkavDontreceiveinformation.Caption, true);
         chkavDontreceiveinformation.CheckedValue = "false";
         AV12DontReceiveInformation = StringUtil.StrToBool( StringUtil.BoolToStr( AV12DontReceiveInformation));
         AssignAttri("", false, "AV12DontReceiveInformation", AV12DontReceiveInformation);
         chkavEnabletwofactorauthentication.Name = "vENABLETWOFACTORAUTHENTICATION";
         chkavEnabletwofactorauthentication.WebTags = "";
         chkavEnabletwofactorauthentication.Caption = context.GetMessage( "GAM_EnableTwoFactorAuthentication", "");
         AssignProp("", false, chkavEnabletwofactorauthentication_Internalname, "TitleCaption", chkavEnabletwofactorauthentication.Caption, true);
         chkavEnabletwofactorauthentication.CheckedValue = "false";
         AV13EnableTwoFactorAuthentication = StringUtil.StrToBool( StringUtil.BoolToStr( AV13EnableTwoFactorAuthentication));
         AssignAttri("", false, "AV13EnableTwoFactorAuthentication", AV13EnableTwoFactorAuthentication);
         cmbavLanguage.Name = "vLANGUAGE";
         cmbavLanguage.WebTags = "";
         cmbavLanguage.addItem("", context.GetMessage( "GAM_None", ""), 0);
         if ( cmbavLanguage.ItemCount > 0 )
         {
            AV15Language = cmbavLanguage.getValidValue(AV15Language);
            AssignAttri("", false, "AV15Language", AV15Language);
         }
         cmbavTheme.Name = "vTHEME";
         cmbavTheme.WebTags = "";
         cmbavTheme.addItem("light", context.GetMessage( "GAM_Light", ""), 0);
         cmbavTheme.addItem("dark", context.GetMessage( "GAM_Dark", ""), 0);
         if ( cmbavTheme.ItemCount > 0 )
         {
            AV16Theme = cmbavTheme.getValidValue(AV16Theme);
            AssignAttri("", false, "AV16Theme", AV16Theme);
         }
         cmbavGender.Name = "vGENDER";
         cmbavGender.WebTags = "";
         cmbavGender.addItem("N", context.GetMessage( "GAM_NotSpecified", ""), 0);
         cmbavGender.addItem("F", context.GetMessage( "GAM_Female", ""), 0);
         cmbavGender.addItem("M", context.GetMessage( "GAM_Male", ""), 0);
         if ( cmbavGender.ItemCount > 0 )
         {
            AV18Gender = cmbavGender.getValidValue(AV18Gender);
            AssignAttri("", false, "AV18Gender", AV18Gender);
         }
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblTxtuser_Internalname = "TXTUSER";
         bttBtnbtntotpauthenticator_Internalname = "BTNBTNTOTPAUTHENTICATOR";
         bttBtnbtnedit_Internalname = "BTNBTNEDIT";
         edtavDatelastauthentication_Internalname = "vDATELASTAUTHENTICATION";
         divUnnamedtable11_Internalname = "UNNAMEDTABLE11";
         divUnnamedtable10_Internalname = "UNNAMEDTABLE10";
         divTable1_Internalname = "TABLE1";
         lblTab1_title_Internalname = "TAB1_TITLE";
         imgavImage_Internalname = "vIMAGE";
         cmbavAuthenticationtypename_Internalname = "vAUTHENTICATIONTYPENAME";
         edtavName_Internalname = "vNAME";
         bttBtnbtnchangename_Internalname = "BTNBTNCHANGENAME";
         edtavEmail_Internalname = "vEMAIL";
         bttBtnbtnchangeemail_Internalname = "BTNBTNCHANGEEMAIL";
         edtavGampassword_Internalname = "vGAMPASSWORD";
         bttBtnuseraction1_Internalname = "BTNUSERACTION1";
         edtavFirstname_Internalname = "vFIRSTNAME";
         edtavLastname_Internalname = "vLASTNAME";
         edtavPhone_Internalname = "vPHONE";
         chkavDontreceiveinformation_Internalname = "vDONTRECEIVEINFORMATION";
         chkavEnabletwofactorauthentication_Internalname = "vENABLETWOFACTORAUTHENTICATION";
         divGam_datacard_tabledatageneral_Internalname = "GAM_DATACARD_TABLEDATAGENERAL";
         divUnnamedtable9_Internalname = "UNNAMEDTABLE9";
         Dvpanel_unnamedtable9_Internalname = "DVPANEL_UNNAMEDTABLE9";
         divTable2_Internalname = "TABLE2";
         divUnnamedtable8_Internalname = "UNNAMEDTABLE8";
         lblTab3_title_Internalname = "TAB3_TITLE";
         cmbavLanguage_Internalname = "vLANGUAGE";
         cmbavTheme_Internalname = "vTHEME";
         edtavBirthday_Internalname = "vBIRTHDAY";
         cmbavGender_Internalname = "vGENDER";
         edtavAddress_Internalname = "vADDRESS";
         edtavAddress2_Internalname = "vADDRESS2";
         edtavCity_Internalname = "vCITY";
         edtavState_Internalname = "vSTATE";
         edtavPostcode_Internalname = "vPOSTCODE";
         edtavTimezone_Internalname = "vTIMEZONE";
         divStencil1_tabledatageneral_Internalname = "STENCIL1_TABLEDATAGENERAL";
         divUnnamedtable7_Internalname = "UNNAMEDTABLE7";
         Dvpanel_unnamedtable7_Internalname = "DVPANEL_UNNAMEDTABLE7";
         divUnnamedtable6_Internalname = "UNNAMEDTABLE6";
         divUnnamedtable5_Internalname = "UNNAMEDTABLE5";
         lblTab2_title_Internalname = "TAB2_TITLE";
         divUnnamedtable4_Internalname = "UNNAMEDTABLE4";
         Dvpanel_unnamedtable4_Internalname = "DVPANEL_UNNAMEDTABLE4";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         Gxuitabspanel_tabs1_Internalname = "GXUITABSPANEL_TABS1";
         bttBtngam_footerentry_btnconfirm_Internalname = "BTNGAM_FOOTERENTRY_BTNCONFIRM";
         bttBtncancel_Internalname = "BTNCANCEL";
         divGam_footerentry_Internalname = "GAM_FOOTERENTRY";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         divMaintable_Internalname = "MAINTABLE";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         chkavEnabletwofactorauthentication.Caption = context.GetMessage( "GAM_EnableTwoFactorAuthentication", "");
         chkavDontreceiveinformation.Caption = context.GetMessage( "GAM_Dontwanttoreceiveinformation", "");
         bttBtncancel_Visible = 1;
         bttBtngam_footerentry_btnconfirm_Visible = 1;
         divGam_footerentry_Visible = 1;
         edtavTimezone_Jsonclick = "";
         edtavTimezone_Enabled = 1;
         edtavTimezone_Caption = context.GetMessage( "GAM_Timezone", "");
         edtavPostcode_Jsonclick = "";
         edtavPostcode_Enabled = 1;
         edtavPostcode_Caption = context.GetMessage( "GAM_PostCode", "");
         edtavState_Jsonclick = "";
         edtavState_Enabled = 1;
         edtavState_Caption = context.GetMessage( "GAM_State", "");
         edtavCity_Jsonclick = "";
         edtavCity_Enabled = 1;
         edtavCity_Caption = context.GetMessage( "GAM_City", "");
         edtavAddress2_Jsonclick = "";
         edtavAddress2_Enabled = 1;
         edtavAddress_Jsonclick = "";
         edtavAddress_Enabled = 1;
         edtavAddress_Caption = context.GetMessage( "GAM_Address", "");
         cmbavGender_Jsonclick = "";
         cmbavGender.Enabled = 1;
         cmbavGender.Caption = context.GetMessage( "GAM_Gender", "");
         edtavBirthday_Jsonclick = "";
         edtavBirthday_Enabled = 1;
         edtavBirthday_Caption = context.GetMessage( "GAM_Birthday", "");
         cmbavTheme_Jsonclick = "";
         cmbavTheme.Enabled = 1;
         cmbavLanguage_Jsonclick = "";
         cmbavLanguage.Enabled = 1;
         cmbavLanguage.Caption = context.GetMessage( "GAM_Language", "");
         chkavEnabletwofactorauthentication.Enabled = 1;
         chkavEnabletwofactorauthentication.Visible = 1;
         chkavDontreceiveinformation.Enabled = 1;
         edtavPhone_Jsonclick = "";
         edtavPhone_Enabled = 1;
         edtavPhone_Caption = context.GetMessage( "GAM_Phone", "");
         edtavLastname_Jsonclick = "";
         edtavLastname_Enabled = 1;
         edtavLastname_Caption = context.GetMessage( "GAM_LastName", "");
         edtavFirstname_Jsonclick = "";
         edtavFirstname_Enabled = 1;
         edtavFirstname_Caption = context.GetMessage( "GAM_FirstName", "");
         edtavGampassword_Jsonclick = "";
         edtavGampassword_Enabled = 1;
         edtavEmail_Jsonclick = "";
         edtavEmail_Enabled = 1;
         edtavEmail_Caption = context.GetMessage( "GAM_Email", "");
         bttBtnbtnchangename_Visible = 1;
         edtavName_Jsonclick = "";
         edtavName_Enabled = 1;
         edtavName_Caption = context.GetMessage( "GAM_UserName", "");
         edtavName_Visible = 1;
         cmbavAuthenticationtypename_Jsonclick = "";
         cmbavAuthenticationtypename.Enabled = 1;
         cmbavAuthenticationtypename.Visible = 1;
         imgavImage_gximage = "";
         imgavImage_Visible = 1;
         edtavDatelastauthentication_Jsonclick = "";
         edtavDatelastauthentication_Enabled = 1;
         bttBtnbtnedit_Visible = 1;
         bttBtnbtntotpauthenticator_Caption = context.GetMessage( "GAM_Enableauthenticator", "");
         bttBtnbtntotpauthenticator_Visible = 1;
         lblTxtuser_Caption = context.GetMessage( "GAM_User", "");
         Gxuitabspanel_tabs1_Historymanagement = Convert.ToBoolean( 0);
         Gxuitabspanel_tabs1_Class = "Tab";
         Gxuitabspanel_tabs1_Pagecount = 3;
         Dvpanel_unnamedtable4_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Iconposition = "Right";
         Dvpanel_unnamedtable4_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable4_Title = context.GetMessage( "Solicitudes de Soporte", "");
         Dvpanel_unnamedtable4_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable4_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable4_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Width = "100%";
         Dvpanel_unnamedtable7_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Iconposition = "Right";
         Dvpanel_unnamedtable7_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Collapsible = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Title = context.GetMessage( "GAM_Advancedinformation", "");
         Dvpanel_unnamedtable7_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable7_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable7_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Width = "100%";
         Dvpanel_unnamedtable9_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable9_Iconposition = "Right";
         Dvpanel_unnamedtable9_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable9_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable9_Collapsible = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable9_Title = context.GetMessage( "GAM_Generalinformation", "");
         Dvpanel_unnamedtable9_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable9_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable9_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable9_Width = "100%";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Perfil", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV12DontReceiveInformation","fld":"vDONTRECEIVEINFORMATION"},{"av":"AV13EnableTwoFactorAuthentication","fld":"vENABLETWOFACTORAUTHENTICATION"},{"av":"AV34URLProfile","fld":"vURLPROFILE","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"cmbavAuthenticationtypename"},{"av":"AV6AuthenticationTypeName","fld":"vAUTHENTICATIONTYPENAME","hsh":true},{"av":"AV7Name","fld":"vNAME","hsh":true},{"av":"AV8EMail","fld":"vEMAIL","hsh":true}]}""");
         setEventMetadata("'DOGAM_FOOTERENTRY_BTNCONFIRM'","""{"handler":"E123K2","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"cmbavAuthenticationtypename"},{"av":"AV6AuthenticationTypeName","fld":"vAUTHENTICATIONTYPENAME","hsh":true},{"av":"AV7Name","fld":"vNAME","hsh":true},{"av":"AV8EMail","fld":"vEMAIL","hsh":true},{"av":"AV9FirstName","fld":"vFIRSTNAME"},{"av":"AV10LastName","fld":"vLASTNAME"},{"av":"AV17Birthday","fld":"vBIRTHDAY"},{"av":"cmbavGender"},{"av":"AV18Gender","fld":"vGENDER"},{"av":"AV11Phone","fld":"vPHONE"},{"av":"AV19Address","fld":"vADDRESS"},{"av":"AV20Address2","fld":"vADDRESS2"},{"av":"AV21City","fld":"vCITY"},{"av":"AV22State","fld":"vSTATE"},{"av":"cmbavTheme"},{"av":"AV16Theme","fld":"vTHEME"},{"av":"AV24Timezone","fld":"vTIMEZONE"},{"av":"AV34URLProfile","fld":"vURLPROFILE","hsh":true},{"av":"AV12DontReceiveInformation","fld":"vDONTRECEIVEINFORMATION"},{"av":"AV13EnableTwoFactorAuthentication","fld":"vENABLETWOFACTORAUTHENTICATION"},{"av":"cmbavLanguage"},{"av":"AV15Language","fld":"vLANGUAGE"}]""");
         setEventMetadata("'DOGAM_FOOTERENTRY_BTNCONFIRM'",""","oparms":[{"av":"divGam_footerentry_Visible","ctrl":"GAM_FOOTERENTRY","prop":"Visible"}]}""");
         setEventMetadata("'DOBTNCHANGENAME'","""{"handler":"E133K2","iparms":[]}""");
         setEventMetadata("'DOBTNCHANGEEMAIL'","""{"handler":"E143K2","iparms":[]}""");
         setEventMetadata("'DOUSERACTION1'","""{"handler":"E153K2","iparms":[]}""");
         setEventMetadata("'DOBTNTOTPAUTHENTICATOR'","""{"handler":"E163K2","iparms":[]}""");
         setEventMetadata("'DOBTNEDIT'","""{"handler":"E173K2","iparms":[]}""");
         setEventMetadata("VALIDV_DATELASTAUTHENTICATION","""{"handler":"Validv_Datelastauthentication","iparms":[]}""");
         setEventMetadata("VALIDV_THEME","""{"handler":"Validv_Theme","iparms":[]}""");
         setEventMetadata("VALIDV_BIRTHDAY","""{"handler":"Validv_Birthday","iparms":[]}""");
         setEventMetadata("VALIDV_GENDER","""{"handler":"Validv_Gender","iparms":[]}""");
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
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV6AuthenticationTypeName = "";
         AV7Name = "";
         AV8EMail = "";
         AV34URLProfile = "";
         forbiddenHiddens = new GXProperties();
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblTxtuser_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttBtnbtntotpauthenticator_Jsonclick = "";
         bttBtnbtnedit_Jsonclick = "";
         AV14DateLastAuthentication = (DateTime)(DateTime.MinValue);
         ucGxuitabspanel_tabs1 = new GXUserControl();
         lblTab1_title_Jsonclick = "";
         ucDvpanel_unnamedtable9 = new GXUserControl();
         AV5Image = "";
         AV48Image_GXI = "";
         sImgUrl = "";
         bttBtnbtnchangename_Jsonclick = "";
         bttBtnbtnchangeemail_Jsonclick = "";
         AV43GAMPassword = "";
         bttBtnuseraction1_Jsonclick = "";
         AV9FirstName = "";
         AV10LastName = "";
         AV11Phone = "";
         lblTab3_title_Jsonclick = "";
         ucDvpanel_unnamedtable7 = new GXUserControl();
         AV15Language = "";
         AV16Theme = "";
         AV17Birthday = DateTime.MinValue;
         AV18Gender = "";
         AV19Address = "";
         AV20Address2 = "";
         AV21City = "";
         AV22State = "";
         AV23PostCode = "";
         AV24Timezone = "";
         lblTab2_title_Jsonclick = "";
         ucDvpanel_unnamedtable4 = new GXUserControl();
         WebComp_Wcmessages_Component = "";
         OldWcmessages = "";
         bttBtngam_footerentry_btnconfirm_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         hsh = "";
         AV33gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV25GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV31GAMLanguages = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage>( context, "GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage", "DesignSystem.Programs");
         AV30GAMLanguage = new GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage(context);
         AV29GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV28GAMError = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         AV35Window = new GXWindow();
         AV32GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV26AuthenticationTypes = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple", "DesignSystem.Programs");
         AV27AuthenticationTypesIns = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.perfil__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.perfil__default(),
            new Object[][] {
            }
         );
         WebComp_Wcmessages = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         edtavDatelastauthentication_Enabled = 0;
         cmbavAuthenticationtypename.Enabled = 0;
         edtavName_Enabled = 0;
         edtavEmail_Enabled = 0;
         edtavGampassword_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int Gxuitabspanel_tabs1_Pagecount ;
      private int bttBtnbtntotpauthenticator_Visible ;
      private int bttBtnbtnedit_Visible ;
      private int edtavDatelastauthentication_Enabled ;
      private int imgavImage_Visible ;
      private int edtavName_Visible ;
      private int edtavName_Enabled ;
      private int bttBtnbtnchangename_Visible ;
      private int edtavEmail_Enabled ;
      private int edtavGampassword_Enabled ;
      private int edtavFirstname_Enabled ;
      private int edtavLastname_Enabled ;
      private int edtavPhone_Enabled ;
      private int edtavBirthday_Enabled ;
      private int edtavAddress_Enabled ;
      private int edtavAddress2_Enabled ;
      private int edtavCity_Enabled ;
      private int edtavState_Enabled ;
      private int edtavPostcode_Enabled ;
      private int edtavTimezone_Enabled ;
      private int divGam_footerentry_Visible ;
      private int bttBtngam_footerentry_btnconfirm_Visible ;
      private int bttBtncancel_Visible ;
      private int AV45GXV1 ;
      private int AV46GXV2 ;
      private int AV47GXV3 ;
      private int AV49GXV4 ;
      private int idxLst ;
      private string Gx_mode ;
      private string wcpOGx_mode ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string AV6AuthenticationTypeName ;
      private string Dvpanel_unnamedtable9_Width ;
      private string Dvpanel_unnamedtable9_Cls ;
      private string Dvpanel_unnamedtable9_Title ;
      private string Dvpanel_unnamedtable9_Iconposition ;
      private string Dvpanel_unnamedtable7_Width ;
      private string Dvpanel_unnamedtable7_Cls ;
      private string Dvpanel_unnamedtable7_Title ;
      private string Dvpanel_unnamedtable7_Iconposition ;
      private string Dvpanel_unnamedtable4_Width ;
      private string Dvpanel_unnamedtable4_Cls ;
      private string Dvpanel_unnamedtable4_Title ;
      private string Dvpanel_unnamedtable4_Iconposition ;
      private string Gxuitabspanel_tabs1_Class ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divMaintable_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string divTable1_Internalname ;
      private string divUnnamedtable10_Internalname ;
      private string divUnnamedtable11_Internalname ;
      private string lblTxtuser_Internalname ;
      private string lblTxtuser_Caption ;
      private string lblTxtuser_Jsonclick ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtnbtntotpauthenticator_Internalname ;
      private string bttBtnbtntotpauthenticator_Caption ;
      private string bttBtnbtntotpauthenticator_Jsonclick ;
      private string bttBtnbtnedit_Internalname ;
      private string bttBtnbtnedit_Jsonclick ;
      private string edtavDatelastauthentication_Internalname ;
      private string edtavDatelastauthentication_Jsonclick ;
      private string Gxuitabspanel_tabs1_Internalname ;
      private string lblTab1_title_Internalname ;
      private string lblTab1_title_Jsonclick ;
      private string divUnnamedtable8_Internalname ;
      private string divTable2_Internalname ;
      private string Dvpanel_unnamedtable9_Internalname ;
      private string divUnnamedtable9_Internalname ;
      private string divGam_datacard_tabledatageneral_Internalname ;
      private string imgavImage_gximage ;
      private string sImgUrl ;
      private string imgavImage_Internalname ;
      private string cmbavAuthenticationtypename_Internalname ;
      private string cmbavAuthenticationtypename_Jsonclick ;
      private string edtavName_Internalname ;
      private string edtavName_Caption ;
      private string edtavName_Jsonclick ;
      private string bttBtnbtnchangename_Internalname ;
      private string bttBtnbtnchangename_Jsonclick ;
      private string edtavEmail_Internalname ;
      private string edtavEmail_Caption ;
      private string edtavEmail_Jsonclick ;
      private string bttBtnbtnchangeemail_Internalname ;
      private string bttBtnbtnchangeemail_Jsonclick ;
      private string edtavGampassword_Internalname ;
      private string AV43GAMPassword ;
      private string edtavGampassword_Jsonclick ;
      private string bttBtnuseraction1_Internalname ;
      private string bttBtnuseraction1_Jsonclick ;
      private string edtavFirstname_Internalname ;
      private string edtavFirstname_Caption ;
      private string AV9FirstName ;
      private string edtavFirstname_Jsonclick ;
      private string edtavLastname_Internalname ;
      private string edtavLastname_Caption ;
      private string AV10LastName ;
      private string edtavLastname_Jsonclick ;
      private string edtavPhone_Internalname ;
      private string edtavPhone_Caption ;
      private string AV11Phone ;
      private string edtavPhone_Jsonclick ;
      private string chkavDontreceiveinformation_Internalname ;
      private string chkavEnabletwofactorauthentication_Internalname ;
      private string lblTab3_title_Internalname ;
      private string lblTab3_title_Jsonclick ;
      private string divUnnamedtable5_Internalname ;
      private string divUnnamedtable6_Internalname ;
      private string Dvpanel_unnamedtable7_Internalname ;
      private string divUnnamedtable7_Internalname ;
      private string divStencil1_tabledatageneral_Internalname ;
      private string cmbavLanguage_Internalname ;
      private string cmbavLanguage_Jsonclick ;
      private string cmbavTheme_Internalname ;
      private string cmbavTheme_Jsonclick ;
      private string edtavBirthday_Internalname ;
      private string edtavBirthday_Caption ;
      private string edtavBirthday_Jsonclick ;
      private string cmbavGender_Internalname ;
      private string AV18Gender ;
      private string cmbavGender_Jsonclick ;
      private string edtavAddress_Internalname ;
      private string edtavAddress_Caption ;
      private string AV19Address ;
      private string edtavAddress_Jsonclick ;
      private string edtavAddress2_Internalname ;
      private string AV20Address2 ;
      private string edtavAddress2_Jsonclick ;
      private string edtavCity_Internalname ;
      private string edtavCity_Caption ;
      private string AV21City ;
      private string edtavCity_Jsonclick ;
      private string edtavState_Internalname ;
      private string edtavState_Caption ;
      private string AV22State ;
      private string edtavState_Jsonclick ;
      private string edtavPostcode_Internalname ;
      private string edtavPostcode_Caption ;
      private string AV23PostCode ;
      private string edtavPostcode_Jsonclick ;
      private string edtavTimezone_Internalname ;
      private string edtavTimezone_Caption ;
      private string AV24Timezone ;
      private string edtavTimezone_Jsonclick ;
      private string lblTab2_title_Internalname ;
      private string lblTab2_title_Jsonclick ;
      private string divUnnamedtable2_Internalname ;
      private string divUnnamedtable3_Internalname ;
      private string Dvpanel_unnamedtable4_Internalname ;
      private string divUnnamedtable4_Internalname ;
      private string WebComp_Wcmessages_Component ;
      private string OldWcmessages ;
      private string divGam_footerentry_Internalname ;
      private string bttBtngam_footerentry_btnconfirm_Internalname ;
      private string bttBtngam_footerentry_btnconfirm_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string hsh ;
      private DateTime AV14DateLastAuthentication ;
      private DateTime AV17Birthday ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Dvpanel_unnamedtable9_Autowidth ;
      private bool Dvpanel_unnamedtable9_Autoheight ;
      private bool Dvpanel_unnamedtable9_Collapsible ;
      private bool Dvpanel_unnamedtable9_Collapsed ;
      private bool Dvpanel_unnamedtable9_Showcollapseicon ;
      private bool Dvpanel_unnamedtable9_Autoscroll ;
      private bool Dvpanel_unnamedtable7_Autowidth ;
      private bool Dvpanel_unnamedtable7_Autoheight ;
      private bool Dvpanel_unnamedtable7_Collapsible ;
      private bool Dvpanel_unnamedtable7_Collapsed ;
      private bool Dvpanel_unnamedtable7_Showcollapseicon ;
      private bool Dvpanel_unnamedtable7_Autoscroll ;
      private bool Dvpanel_unnamedtable4_Autowidth ;
      private bool Dvpanel_unnamedtable4_Autoheight ;
      private bool Dvpanel_unnamedtable4_Collapsible ;
      private bool Dvpanel_unnamedtable4_Collapsed ;
      private bool Dvpanel_unnamedtable4_Showcollapseicon ;
      private bool Dvpanel_unnamedtable4_Autoscroll ;
      private bool Gxuitabspanel_tabs1_Historymanagement ;
      private bool wbLoad ;
      private bool AV5Image_IsBlob ;
      private bool AV12DontReceiveInformation ;
      private bool AV13EnableTwoFactorAuthentication ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool bDynCreated_Wcmessages ;
      private string AV7Name ;
      private string AV8EMail ;
      private string AV34URLProfile ;
      private string AV48Image_GXI ;
      private string AV15Language ;
      private string AV16Theme ;
      private string AV5Image ;
      private GXWebComponent WebComp_Wcmessages ;
      private GXProperties forbiddenHiddens ;
      private GXUserControl ucGxuitabspanel_tabs1 ;
      private GXUserControl ucDvpanel_unnamedtable9 ;
      private GXUserControl ucDvpanel_unnamedtable7 ;
      private GXUserControl ucDvpanel_unnamedtable4 ;
      private GXWebForm Form ;
      private GXWindow AV35Window ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_Gx_mode ;
      private GXCombobox cmbavAuthenticationtypename ;
      private GXCheckbox chkavDontreceiveinformation ;
      private GXCheckbox chkavEnabletwofactorauthentication ;
      private GXCombobox cmbavLanguage ;
      private GXCombobox cmbavTheme ;
      private GXCombobox cmbavGender ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV33gamUser ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV25GAMApplication ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage> AV31GAMLanguages ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage AV30GAMLanguage ;
      private IDataStoreProvider pr_default ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV29GAMErrorCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV28GAMError ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV32GAMRepository ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple> AV26AuthenticationTypes ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple AV27AuthenticationTypesIns ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class perfil__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class perfil__default : DataStoreHelperBase, IDataStoreHelper
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
