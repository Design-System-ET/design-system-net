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
   public class gamexampleauthenticatorenable : GXDataArea
   {
      public gamexampleauthenticatorenable( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamexampleauthenticatorenable( IGxContext context )
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
            return GAMSecurityLevel.SecurityHigh ;
         }

      }

      protected override string ExecutePermissionPrefix
      {
         get {
            return "gamexampleusertotpactivation_Execute" ;
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
         PA3H2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3H2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamexampleauthenticatorenable.aspx") +"\">") ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"GAMExampleAuthenticatorEnable");
         forbiddenHiddens.Add("SecretKey", StringUtil.RTrim( context.localUtil.Format( AV14SecretKey, "")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("gamexampleauthenticatorenable:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
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
            WE3H2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3H2( ) ;
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
         return formatLink("gamexampleauthenticatorenable.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "GAMExampleAuthenticatorEnable" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "GAMExample Authenticator Enable", "") ;
      }

      protected void WB3H0( )
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
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTxtuser_Internalname, lblTxtuser_Caption, "", "", lblTxtuser_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Title", 0, "", 1, 1, 0, 0, "HLP_GAMExampleAuthenticatorEnable.htm");
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
            GxWebStd.gx_div_start( context, divGam_datacard_Internalname, 1, 0, "px", 0, "px", "Card bg-white", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGam_datacard_tablegeneraltitle_Internalname, 1, 0, "px", 0, "px", "card-heading", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblGam_datacard_tbtitlegeneral_Internalname, context.GetMessage( "GAM_EnableTOTPauthenticator", ""), "", "", lblGam_datacard_tbtitlegeneral_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "", 0, "", 1, 1, 0, 0, "HLP_GAMExampleAuthenticatorEnable.htm");
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
            GxWebStd.gx_div_start( context, divGam_datacard_tabledatageneral_Internalname, 1, 0, "px", 0, "px", "card-body", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblQrimage_Internalname, lblQrimage_Caption, "", "", lblQrimage_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_GAMExampleAuthenticatorEnable.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSecretkey_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSecretkey_Internalname, context.GetMessage( "GAM_Secretkey", ""), "col-sm-3 AttributeLabel w-100Label", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'',false,'',0)\"";
            ClassString = "Attribute w-100";
            StyleString = "";
            ClassString = "Attribute w-100";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavSecretkey_Internalname, AV14SecretKey, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"", 0, 1, edtavSecretkey_Enabled, 0, 80, "chr", 4, "row", 0, StyleString, ClassString, "", "", "256", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_GAMExampleAuthenticatorEnable.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotpcode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotpcode_Internalname, context.GetMessage( "GAM_Typeacode", ""), "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 34,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotpcode_Internalname, AV15TOTPCode, StringUtil.RTrim( context.localUtil.Format( AV15TOTPCode, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,34);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotpcode_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTotpcode_Enabled, 1, "text", "", 8, "chr", 1, "row", 8, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_GAMExampleAuthenticatorEnable.htm");
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
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "W0037"+"", StringUtil.RTrim( WebComp_Wcmessages_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0037"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldWcmessages), StringUtil.Lower( WebComp_Wcmessages_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0037"+"");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGam_footerentry_Internalname, 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGam_footerentry_tablebuttons_Internalname, 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-flex"+" ", "justify-content:flex-end;align-items:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "end", "top", "", "min-height:30px;", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 45,'',false,'',0)\"";
            ClassString = "Button button-tertiary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttGam_footerentry_btncancel_Internalname, "", bttGam_footerentry_btncancel_Caption, bttGam_footerentry_btncancel_Jsonclick, 5, context.GetMessage( "GAM_Cancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMExampleAuthenticatorEnable.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 30, "px", "inline-left-l inline-right-xl", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 47,'',false,'',0)\"";
            ClassString = "Button button-primary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttGam_footerentry_btnconfirm_Internalname, "", context.GetMessage( "GAM_Confirm", ""), bttGam_footerentry_btnconfirm_Jsonclick, 5, context.GetMessage( "GAM_Confirm", ""), "", StyleString, ClassString, bttGam_footerentry_btnconfirm_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMExampleAuthenticatorEnable.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START3H2( )
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
         Form.Meta.addItem("description", context.GetMessage( "GAMExample Authenticator Enable", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3H0( ) ;
      }

      protected void WS3H2( )
      {
         START3H2( ) ;
         EVT3H2( ) ;
      }

      protected void EVT3H2( )
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
                              E113H2 ();
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
                                    E123H2 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Cancel' */
                              E133H2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E143H2 ();
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
                        if ( nCmpId == 37 )
                        {
                           OldWcmessages = cgiGet( "W0037");
                           if ( ( StringUtil.Len( OldWcmessages) == 0 ) || ( StringUtil.StrCmp(OldWcmessages, WebComp_Wcmessages_Component) != 0 ) )
                           {
                              WebComp_Wcmessages = getWebComponent(GetType(), "DesignSystem.Programs", OldWcmessages, new Object[] {context} );
                              WebComp_Wcmessages.ComponentInit();
                              WebComp_Wcmessages.Name = "OldWcmessages";
                              WebComp_Wcmessages_Component = OldWcmessages;
                           }
                           if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
                           {
                              WebComp_Wcmessages.componentprocess("W0037", "", sEvt);
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

      protected void WE3H2( )
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

      protected void PA3H2( )
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
               GX_FocusControl = edtavSecretkey_Internalname;
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
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3H2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavSecretkey_Enabled = 0;
         AssignProp("", false, edtavSecretkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSecretkey_Enabled), 5, 0), true);
      }

      protected void RF3H2( )
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
            E143H2 ();
            WB3H0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3H2( )
      {
      }

      protected void before_start_formulas( )
      {
         edtavSecretkey_Enabled = 0;
         AssignProp("", false, edtavSecretkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSecretkey_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3H0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E113H2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            AV14SecretKey = cgiGet( edtavSecretkey_Internalname);
            AssignAttri("", false, "AV14SecretKey", AV14SecretKey);
            AV15TOTPCode = cgiGet( edtavTotpcode_Internalname);
            AssignAttri("", false, "AV15TOTPCode", AV15TOTPCode);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"GAMExampleAuthenticatorEnable");
            AV14SecretKey = cgiGet( edtavSecretkey_Internalname);
            AssignAttri("", false, "AV14SecretKey", AV14SecretKey);
            forbiddenHiddens.Add("SecretKey", StringUtil.RTrim( context.localUtil.Format( AV14SecretKey, "")));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("gamexampleauthenticatorenable:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
         E113H2 ();
         if (returnInSub) return;
      }

      protected void E113H2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV10GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         AV5AuthenticationTypeName = AV10GAMUser.gxTpr_Authenticationtypename;
         AssignAttri("", false, "AV5AuthenticationTypeName", AV5AuthenticationTypeName);
         /* Execute user subroutine: 'VALIDISLOCALAUTHENTICATION' */
         S112 ();
         if (returnInSub) return;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV10GAMUser.gxTpr_Firstname)) && String.IsNullOrEmpty(StringUtil.RTrim( AV10GAMUser.gxTpr_Lastname)) )
         {
            lblTxtuser_Caption = AV10GAMUser.gxTpr_Name;
            AssignProp("", false, lblTxtuser_Internalname, "Caption", lblTxtuser_Caption, true);
         }
         else
         {
            lblTxtuser_Caption = StringUtil.Format( "%1 %2", AV10GAMUser.gxTpr_Firstname, AV10GAMUser.gxTpr_Lastname, "", "", "", "", "", "", "");
            AssignProp("", false, lblTxtuser_Internalname, "Caption", lblTxtuser_Caption, true);
         }
         AV8GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
         if ( AV10GAMUser.generateqrdatatotp(AV8GAMApplication.gxTpr_Name, out  AV14SecretKey, out  AV13QRString, out  AV9GAMErrorCollection) )
         {
            lblQrimage_Caption = StringUtil.Format( "<img src=\"%1\" width=\"200px\"></img>", AV13QRString, "", "", "", "", "", "", "", "");
            AssignProp("", false, lblQrimage_Internalname, "Caption", lblQrimage_Caption, true);
         }
         else
         {
            /* Execute user subroutine: 'SHOWMESSAGES' */
            S122 ();
            if (returnInSub) return;
         }
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E123H2 ();
         if (returnInSub) return;
      }

      protected void E123H2( )
      {
         /* Enter Routine */
         returnInSub = false;
         AV10GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         AV10GAMUser.load( AV10GAMUser.gxTpr_Guid);
         if ( AV10GAMUser.validatetotpcode(AV15TOTPCode, out  AV9GAMErrorCollection) )
         {
            if ( AV10GAMUser.enabletotpauthenticator(out  AV9GAMErrorCollection) )
            {
               edtavTotpcode_Enabled = 0;
               AssignProp("", false, edtavTotpcode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTotpcode_Enabled), 5, 0), true);
               bttGam_footerentry_btnconfirm_Visible = 0;
               AssignProp("", false, bttGam_footerentry_btnconfirm_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttGam_footerentry_btnconfirm_Visible), 5, 0), true);
               bttGam_footerentry_btncancel_Caption = context.GetMessage( "GAM_Back", "");
               AssignProp("", false, bttGam_footerentry_btncancel_Internalname, "Caption", bttGam_footerentry_btncancel_Caption, true);
               new gam_setmessage(context ).execute(  "Message",  context.GetMessage( "GAM_TOTPauthenticatorenabled", "")) ;
            }
         }
         /* Execute user subroutine: 'SHOWMESSAGES' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E133H2( )
      {
         /* 'Cancel' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
         CallWebObject(formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
      }

      protected void S112( )
      {
         /* 'VALIDISLOCALAUTHENTICATION' Routine */
         returnInSub = false;
         AV11isLocalAuthentication = false;
         AV6AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV12Language, out  AV9GAMErrorCollection);
         if ( AV9GAMErrorCollection.Count > 0 )
         {
            /* Execute user subroutine: 'SHOWMESSAGES' */
            S122 ();
            if (returnInSub) return;
         }
         else
         {
            AV16GXV1 = 1;
            while ( AV16GXV1 <= AV6AuthenticationTypes.Count )
            {
               AV7AuthenticationTypeSimple = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV6AuthenticationTypes.Item(AV16GXV1));
               if ( StringUtil.StrCmp(AV7AuthenticationTypeSimple.gxTpr_Name, AV5AuthenticationTypeName) == 0 )
               {
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7AuthenticationTypeSimple.gxTpr_Impersonate)) )
                  {
                     if ( StringUtil.StrCmp(AV7AuthenticationTypeSimple.gxTpr_Impersonate, "local") == 0 )
                     {
                        AV11isLocalAuthentication = true;
                     }
                  }
                  else
                  {
                     if ( StringUtil.StrCmp(AV7AuthenticationTypeSimple.gxTpr_Type, "GAMLocal") == 0 )
                     {
                        AV11isLocalAuthentication = true;
                     }
                  }
                  if (true) break;
               }
               AV16GXV1 = (int)(AV16GXV1+1);
            }
         }
      }

      protected void S122( )
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
            WebComp_Wcmessages.componentprepare(new Object[] {(string)"W0037",(string)""});
            WebComp_Wcmessages.componentbind(new Object[] {});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Wcmessages )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0037"+"");
            WebComp_Wcmessages.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E143H2( )
      {
         /* Load Routine */
         returnInSub = false;
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
         PA3H2( ) ;
         WS3H2( ) ;
         WE3H2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121708233", true, true);
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
         context.AddJavascriptSource("gamexampleauthenticatorenable.js", "?2024121708238", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblTxtuser_Internalname = "TXTUSER";
         divTable1_Internalname = "TABLE1";
         lblGam_datacard_tbtitlegeneral_Internalname = "GAM_DATACARD_TBTITLEGENERAL";
         divGam_datacard_tablegeneraltitle_Internalname = "GAM_DATACARD_TABLEGENERALTITLE";
         lblQrimage_Internalname = "QRIMAGE";
         edtavSecretkey_Internalname = "vSECRETKEY";
         edtavTotpcode_Internalname = "vTOTPCODE";
         divGam_datacard_tabledatageneral_Internalname = "GAM_DATACARD_TABLEDATAGENERAL";
         divGam_datacard_Internalname = "GAM_DATACARD";
         bttGam_footerentry_btncancel_Internalname = "GAM_FOOTERENTRY_BTNCANCEL";
         bttGam_footerentry_btnconfirm_Internalname = "GAM_FOOTERENTRY_BTNCONFIRM";
         divGam_footerentry_tablebuttons_Internalname = "GAM_FOOTERENTRY_TABLEBUTTONS";
         divGam_footerentry_Internalname = "GAM_FOOTERENTRY";
         divMaintable_Internalname = "MAINTABLE";
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
         bttGam_footerentry_btnconfirm_Visible = 1;
         bttGam_footerentry_btncancel_Caption = context.GetMessage( "GAM_Cancel", "");
         edtavTotpcode_Jsonclick = "";
         edtavTotpcode_Enabled = 1;
         edtavSecretkey_Enabled = 1;
         lblQrimage_Caption = "";
         lblTxtuser_Caption = context.GetMessage( "GAM_User", "");
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "GAMExample Authenticator Enable", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV14SecretKey","fld":"vSECRETKEY"}]}""");
         setEventMetadata("ENTER","""{"handler":"E123H2","iparms":[{"av":"AV15TOTPCode","fld":"vTOTPCODE"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"edtavTotpcode_Enabled","ctrl":"vTOTPCODE","prop":"Enabled"},{"ctrl":"GAM_FOOTERENTRY_BTNCONFIRM","prop":"Visible"},{"ctrl":"GAM_FOOTERENTRY_BTNCANCEL","prop":"Caption"},{"ctrl":"WCMESSAGES"}]}""");
         setEventMetadata("'CANCEL'","""{"handler":"E133H2","iparms":[]}""");
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
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         forbiddenHiddens = new GXProperties();
         AV14SecretKey = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblTxtuser_Jsonclick = "";
         lblGam_datacard_tbtitlegeneral_Jsonclick = "";
         lblQrimage_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         AV15TOTPCode = "";
         WebComp_Wcmessages_Component = "";
         OldWcmessages = "";
         bttGam_footerentry_btncancel_Jsonclick = "";
         bttGam_footerentry_btnconfirm_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         hsh = "";
         AV10GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV5AuthenticationTypeName = "";
         AV8GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV13QRString = "";
         AV9GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         GXEncryptionTmp = "";
         AV6AuthenticationTypes = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple", "DesignSystem.Programs");
         AV12Language = "";
         AV7AuthenticationTypeSimple = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         WebComp_Wcmessages = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         edtavSecretkey_Enabled = 0;
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
      private int edtavSecretkey_Enabled ;
      private int edtavTotpcode_Enabled ;
      private int bttGam_footerentry_btnconfirm_Visible ;
      private int AV16GXV1 ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string divTable1_Internalname ;
      private string lblTxtuser_Internalname ;
      private string lblTxtuser_Caption ;
      private string lblTxtuser_Jsonclick ;
      private string divGam_datacard_Internalname ;
      private string divGam_datacard_tablegeneraltitle_Internalname ;
      private string lblGam_datacard_tbtitlegeneral_Internalname ;
      private string lblGam_datacard_tbtitlegeneral_Jsonclick ;
      private string divGam_datacard_tabledatageneral_Internalname ;
      private string lblQrimage_Internalname ;
      private string lblQrimage_Caption ;
      private string lblQrimage_Jsonclick ;
      private string edtavSecretkey_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string edtavTotpcode_Internalname ;
      private string edtavTotpcode_Jsonclick ;
      private string WebComp_Wcmessages_Component ;
      private string OldWcmessages ;
      private string divGam_footerentry_Internalname ;
      private string divGam_footerentry_tablebuttons_Internalname ;
      private string bttGam_footerentry_btncancel_Internalname ;
      private string bttGam_footerentry_btncancel_Caption ;
      private string bttGam_footerentry_btncancel_Jsonclick ;
      private string bttGam_footerentry_btnconfirm_Internalname ;
      private string bttGam_footerentry_btnconfirm_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string hsh ;
      private string AV5AuthenticationTypeName ;
      private string GXEncryptionTmp ;
      private string AV12Language ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV11isLocalAuthentication ;
      private bool bDynCreated_Wcmessages ;
      private string AV13QRString ;
      private string AV14SecretKey ;
      private string AV15TOTPCode ;
      private GXWebComponent WebComp_Wcmessages ;
      private GXProperties forbiddenHiddens ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV10GAMUser ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV8GAMApplication ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV9GAMErrorCollection ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple> AV6AuthenticationTypes ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple AV7AuthenticationTypeSimple ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
