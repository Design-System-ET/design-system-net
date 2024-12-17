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
   public class gamuserapplicationapikey : GXDataArea
   {
      public gamuserapplicationapikey( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamuserapplicationapikey( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_UserGUID ,
                           string aP1_ClientID )
      {
         this.AV13UserGUID = aP0_UserGUID;
         this.AV6ClientID = aP1_ClientID;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         chkavClientallowgetuserdatarest = new GXCheckbox();
         chkavClientallowgetuseradddatarest = new GXCheckbox();
         chkavClientallowgetuserrolesrest = new GXCheckbox();
         chkavClientallowgetsessioniniproprest = new GXCheckbox();
         chkavClientallowgetsessionappdatarest = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "UserGUID");
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
               gxfirstwebparm = GetFirstPar( "UserGUID");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "UserGUID");
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
            return "gamexampleuserentry_Execute" ;
         }

      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("wwpbaseobjects.workwithplusmasterpageprompt", "DesignSystem.Programs.wwpbaseobjects.workwithplusmasterpageprompt", new Object[] {context});
            MasterPageObj.setDataArea(this,true);
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
         PA182( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START182( ) ;
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
         context.WriteHtmlText( " "+"class=\"form-horizontal FormNoBackgroundColor\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamuserapplicationapikey.aspx"+UrlEncode(StringUtil.RTrim(AV13UserGUID)) + "," + UrlEncode(StringUtil.RTrim(AV6ClientID));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal FormNoBackgroundColor\" data-gx-class=\"form-horizontal FormNoBackgroundColor\" novalidate action=\""+formatLink("gamuserapplicationapikey.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal FormNoBackgroundColor", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "vUSERGUID", StringUtil.RTrim( AV13UserGUID));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERGUID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13UserGUID, "")), context));
         GxWebStd.gx_hidden_field( context, "vDETAILEDSCOPE", AV20DetailedScope);
         GxWebStd.gx_hidden_field( context, "gxhash_vDETAILEDSCOPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV20DetailedScope, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vCLIENTID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ClientID, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERAPIKEY", AV22UserAPIkey);
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERAPIKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV22UserAPIkey, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERAPIKEYEXPIRES", context.localUtil.TToC( AV23UserAPIkeyExpires, 10, 8, 0, 0, "/", ":", " "));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERAPIKEYEXPIRES", GetSecureSignedToken( "", context.localUtil.Format( AV23UserAPIkeyExpires, "99/99/9999 99:99"), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vUSERGUID", StringUtil.RTrim( AV13UserGUID));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERGUID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13UserGUID, "")), context));
         GxWebStd.gx_hidden_field( context, "vDETAILEDSCOPE", AV20DetailedScope);
         GxWebStd.gx_hidden_field( context, "gxhash_vDETAILEDSCOPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV20DetailedScope, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERAPIKEY", AV22UserAPIkey);
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERAPIKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV22UserAPIkey, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERAPIKEYEXPIRES", context.localUtil.TToC( AV23UserAPIkeyExpires, 10, 8, 0, 0, "/", ":", " "));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERAPIKEYEXPIRES", GetSecureSignedToken( "", context.localUtil.Format( AV23UserAPIkeyExpires, "99/99/9999 99:99"), context));
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
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal FormNoBackgroundColor" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE182( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT182( ) ;
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
         GXEncryptionTmp = "gamuserapplicationapikey.aspx"+UrlEncode(StringUtil.RTrim(AV13UserGUID)) + "," + UrlEncode(StringUtil.RTrim(AV6ClientID));
         return formatLink("gamuserapplicationapikey.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "GAMUserApplicationAPIkey" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Application API key", "") ;
      }

      protected void WB180( )
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMainTransactionPopUp", "start", "top", "", "", "div");
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
            GxWebStd.gx_div_start( context, divTableattributes_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGamuseremail_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGamuseremail_Internalname, context.GetMessage( "WWP_GAM_Email", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGamuseremail_Internalname, AV12GAMUserEmail, StringUtil.RTrim( context.localUtil.Format( AV12GAMUserEmail, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGamuseremail_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavGamuseremail_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_GAMUserApplicationAPIkey.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientid_Internalname, StringUtil.RTrim( AV6ClientID), StringUtil.RTrim( context.localUtil.Format( AV6ClientID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,22);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientid_Enabled, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMClientApplicationId", "start", true, "", "HLP_GAMUserApplicationAPIkey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblrestscopes_Internalname, divTblrestscopes_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblrestpredscopes_Internalname, divTblrestpredscopes_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock1_Internalname, context.GetMessage( "GAM_Alloweduserscopes", ""), "", "", lblTextblock1_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock AttributeSizeLarge", 0, "", 1, 1, 0, 0, "HLP_GAMUserApplicationAPIkey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "DataContentCell", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuserdatarest_Internalname, context.GetMessage( "User data", ""), "gx-form-item AttributeCheckBoxLabel", 0, true, "width: 25%;");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuserdatarest_Internalname, StringUtil.BoolToStr( AV14ClientAllowGetUserDataREST), "", context.GetMessage( "User data", ""), chkavClientallowgetuserdatarest.Visible, chkavClientallowgetuserdatarest.Enabled, "true", context.GetMessage( "WWP_GAM_Userdata", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(37, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,37);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "DataContentCell", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuseradddatarest_Internalname, context.GetMessage( "User additional data", ""), "gx-form-item AttributeCheckBoxLabel", 0, true, "width: 25%;");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuseradddatarest_Internalname, StringUtil.BoolToStr( AV15ClientAllowGetUserAddDataREST), "", context.GetMessage( "User additional data", ""), chkavClientallowgetuseradddatarest.Visible, chkavClientallowgetuseradddatarest.Enabled, "true", context.GetMessage( "WWP_GAM_Useradditionaldata", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(40, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,40);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "DataContentCell", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetuserrolesrest_Internalname, context.GetMessage( "User roles", ""), "gx-form-item AttributeCheckBoxLabel", 0, true, "width: 25%;");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 43,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetuserrolesrest_Internalname, StringUtil.BoolToStr( AV16ClientAllowGetUserRolesRest), "", context.GetMessage( "User roles", ""), chkavClientallowgetuserrolesrest.Visible, chkavClientallowgetuserrolesrest.Enabled, "true", context.GetMessage( "WWP_GAM_UserRoles", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(43, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,43);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetsessioniniproprest_Internalname, context.GetMessage( "Session initial properties", ""), "col-sm-3 AttributeCheckBoxLabel", 0, true, "");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 47,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetsessioniniproprest_Internalname, StringUtil.BoolToStr( AV17ClientAllowGetSessionIniPropREST), "", context.GetMessage( "Session initial properties", ""), chkavClientallowgetsessioniniproprest.Visible, chkavClientallowgetsessioniniproprest.Enabled, "true", context.GetMessage( "WWP_GAM_Sessioninitialproperties", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(47, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,47);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavClientallowgetsessionappdatarest_Internalname, context.GetMessage( "Session application data", ""), "col-sm-3 AttributeCheckBoxLabel", 0, true, "");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 50,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavClientallowgetsessionappdatarest_Internalname, StringUtil.BoolToStr( AV18ClientAllowGetSessionAppDataREST), "", context.GetMessage( "Session application data", ""), chkavClientallowgetsessionappdatarest.Visible, chkavClientallowgetsessionappdatarest.Enabled, "true", context.GetMessage( "WWP_GAM_Sessionapplicationdata", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(50, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,50);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom15 DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientallowadditionalscoperest_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientallowadditionalscoperest_Internalname, context.GetMessage( "WWP_GAM_Additionaluserscopes", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 55,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientallowadditionalscoperest_Internalname, AV19ClientAllowAdditionalScopeREST, StringUtil.RTrim( context.localUtil.Format( AV19ClientAllowAdditionalScopeREST, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,55);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientallowadditionalscoperest_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavClientallowadditionalscoperest_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMUserApplicationAPIkey.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavExpires_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavExpires_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavExpires_Internalname, context.GetMessage( "WWP_GAM_Expires", ""), " AttributeDateTimeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'',false,'',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavExpires_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavExpires_Internalname, context.localUtil.TToC( AV8Expires, 10, 8, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "), context.localUtil.Format( AV8Expires, "99/99/9999 99:99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onblur(this,60);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavExpires_Jsonclick, 0, "AttributeDateTime", "", "", "", "", edtavExpires_Visible, edtavExpires_Enabled, 0, "text", "", 16, "chr", 1, "row", 16, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMDateTime", "end", false, "", "HLP_GAMUserApplicationAPIkey.htm");
            GxWebStd.gx_bitmap( context, edtavExpires_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((edtavExpires_Visible==0)||(edtavExpires_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_GAMUserApplicationAPIkey.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavApikey_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavApikey_Internalname, context.GetMessage( "WWP_GAM_APIkey", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 65,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavApikey_Internalname, AV5APIkey, StringUtil.RTrim( context.localUtil.Format( AV5APIkey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,65);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavApikey_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavApikey_Enabled, 0, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMUserApplicationAPIkey.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 70,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "", context.GetMessage( "WWP_GAM_Generate", ""), bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtnenter_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMUserApplicationAPIkey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 72,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "", bttBtncancel_Caption, bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMUserApplicationAPIkey.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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

      protected void START182( )
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
         Form.Meta.addItem("description", context.GetMessage( "Application API key", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP180( ) ;
      }

      protected void WS182( )
      {
         START182( ) ;
         EVT182( ) ;
      }

      protected void EVT182( )
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
                              E11182 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CONFIRM'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Confirm' */
                              E12182 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E13182 ();
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
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE182( )
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

      protected void PA182( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamuserapplicationapikey.aspx")), "gamuserapplicationapikey.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamuserapplicationapikey.aspx")))) ;
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
                  gxfirstwebparm = GetFirstPar( "UserGUID");
                  toggleJsOutput = isJsOutputEnabled( );
                  if ( context.isSpaRequest( ) )
                  {
                     disableJsOutput();
                  }
                  if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
                  {
                     AV13UserGUID = gxfirstwebparm;
                     AssignAttri("", false, "AV13UserGUID", AV13UserGUID);
                     GxWebStd.gx_hidden_field( context, "gxhash_vUSERGUID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13UserGUID, "")), context));
                     if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") != 0 )
                     {
                        AV6ClientID = GetPar( "ClientID");
                        AssignAttri("", false, "AV6ClientID", AV6ClientID);
                        GxWebStd.gx_hidden_field( context, "gxhash_vCLIENTID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ClientID, "")), context));
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
               GX_FocusControl = edtavGamuseremail_Internalname;
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
         AV14ClientAllowGetUserDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV14ClientAllowGetUserDataREST));
         AssignAttri("", false, "AV14ClientAllowGetUserDataREST", AV14ClientAllowGetUserDataREST);
         AV15ClientAllowGetUserAddDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV15ClientAllowGetUserAddDataREST));
         AssignAttri("", false, "AV15ClientAllowGetUserAddDataREST", AV15ClientAllowGetUserAddDataREST);
         AV16ClientAllowGetUserRolesRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV16ClientAllowGetUserRolesRest));
         AssignAttri("", false, "AV16ClientAllowGetUserRolesRest", AV16ClientAllowGetUserRolesRest);
         AV17ClientAllowGetSessionIniPropREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV17ClientAllowGetSessionIniPropREST));
         AssignAttri("", false, "AV17ClientAllowGetSessionIniPropREST", AV17ClientAllowGetSessionIniPropREST);
         AV18ClientAllowGetSessionAppDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV18ClientAllowGetSessionAppDataREST));
         AssignAttri("", false, "AV18ClientAllowGetSessionAppDataREST", AV18ClientAllowGetSessionAppDataREST);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF182( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavGamuseremail_Enabled = 0;
         AssignProp("", false, edtavGamuseremail_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGamuseremail_Enabled), 5, 0), true);
         edtavClientid_Enabled = 0;
         AssignProp("", false, edtavClientid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientid_Enabled), 5, 0), true);
         edtavExpires_Enabled = 0;
         AssignProp("", false, edtavExpires_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavExpires_Enabled), 5, 0), true);
         edtavApikey_Enabled = 0;
         AssignProp("", false, edtavApikey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavApikey_Enabled), 5, 0), true);
      }

      protected void RF182( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E13182 ();
            WB180( ) ;
         }
      }

      protected void send_integrity_lvl_hashes182( )
      {
         GxWebStd.gx_hidden_field( context, "vDETAILEDSCOPE", AV20DetailedScope);
         GxWebStd.gx_hidden_field( context, "gxhash_vDETAILEDSCOPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV20DetailedScope, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERAPIKEY", AV22UserAPIkey);
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERAPIKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV22UserAPIkey, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERAPIKEYEXPIRES", context.localUtil.TToC( AV23UserAPIkeyExpires, 10, 8, 0, 0, "/", ":", " "));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERAPIKEYEXPIRES", GetSecureSignedToken( "", context.localUtil.Format( AV23UserAPIkeyExpires, "99/99/9999 99:99"), context));
      }

      protected void before_start_formulas( )
      {
         edtavGamuseremail_Enabled = 0;
         AssignProp("", false, edtavGamuseremail_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGamuseremail_Enabled), 5, 0), true);
         edtavClientid_Enabled = 0;
         AssignProp("", false, edtavClientid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientid_Enabled), 5, 0), true);
         edtavExpires_Enabled = 0;
         AssignProp("", false, edtavExpires_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavExpires_Enabled), 5, 0), true);
         edtavApikey_Enabled = 0;
         AssignProp("", false, edtavApikey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavApikey_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP180( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E11182 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            AV12GAMUserEmail = cgiGet( edtavGamuseremail_Internalname);
            AssignAttri("", false, "AV12GAMUserEmail", AV12GAMUserEmail);
            AV14ClientAllowGetUserDataREST = StringUtil.StrToBool( cgiGet( chkavClientallowgetuserdatarest_Internalname));
            AssignAttri("", false, "AV14ClientAllowGetUserDataREST", AV14ClientAllowGetUserDataREST);
            AV15ClientAllowGetUserAddDataREST = StringUtil.StrToBool( cgiGet( chkavClientallowgetuseradddatarest_Internalname));
            AssignAttri("", false, "AV15ClientAllowGetUserAddDataREST", AV15ClientAllowGetUserAddDataREST);
            AV16ClientAllowGetUserRolesRest = StringUtil.StrToBool( cgiGet( chkavClientallowgetuserrolesrest_Internalname));
            AssignAttri("", false, "AV16ClientAllowGetUserRolesRest", AV16ClientAllowGetUserRolesRest);
            AV17ClientAllowGetSessionIniPropREST = StringUtil.StrToBool( cgiGet( chkavClientallowgetsessioniniproprest_Internalname));
            AssignAttri("", false, "AV17ClientAllowGetSessionIniPropREST", AV17ClientAllowGetSessionIniPropREST);
            AV18ClientAllowGetSessionAppDataREST = StringUtil.StrToBool( cgiGet( chkavClientallowgetsessionappdatarest_Internalname));
            AssignAttri("", false, "AV18ClientAllowGetSessionAppDataREST", AV18ClientAllowGetSessionAppDataREST);
            AV19ClientAllowAdditionalScopeREST = cgiGet( edtavClientallowadditionalscoperest_Internalname);
            AssignAttri("", false, "AV19ClientAllowAdditionalScopeREST", AV19ClientAllowAdditionalScopeREST);
            if ( context.localUtil.VCDateTime( cgiGet( edtavExpires_Internalname), (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt"))), (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0))) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_baddatetime", new   object[]  {context.GetMessage( "Expires", "")}), 1, "vEXPIRES");
               GX_FocusControl = edtavExpires_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV8Expires = (DateTime)(DateTime.MinValue);
               AssignAttri("", false, "AV8Expires", context.localUtil.TToC( AV8Expires, 10, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            else
            {
               AV8Expires = context.localUtil.CToT( cgiGet( edtavExpires_Internalname));
               AssignAttri("", false, "AV8Expires", context.localUtil.TToC( AV8Expires, 10, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            AV5APIkey = cgiGet( edtavApikey_Internalname);
            AssignAttri("", false, "AV5APIkey", AV5APIkey);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E11182 ();
         if (returnInSub) return;
      }

      protected void E11182( )
      {
         /* Start Routine */
         returnInSub = false;
         divTblrestscopes_Visible = 0;
         AssignProp("", false, divTblrestscopes_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblrestscopes_Visible), 5, 0), true);
         edtavExpires_Visible = 0;
         AssignProp("", false, edtavExpires_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavExpires_Visible), 5, 0), true);
         AV11GAMUser.load( AV13UserGUID);
         if ( AV11GAMUser.success() )
         {
            AV12GAMUserEmail = AV11GAMUser.gxTpr_Email;
            AssignAttri("", false, "AV12GAMUserEmail", AV12GAMUserEmail);
         }
         else
         {
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
         AV9GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).getbyclientid(AV6ClientID, out  AV10GAMErrorCollection);
         if ( AV10GAMErrorCollection.Count == 0 )
         {
            Form.Caption = StringUtil.Format( context.GetMessage( "WWP_GAM_1applicationAPIkeyfor2", ""), AV9GAMApplication.gxTpr_Name, AV11GAMUser.gxTpr_Name, "", "", "", "", "", "", "");
            AssignProp("", false, "FORM", "Caption", Form.Caption, true);
            if ( AV9GAMApplication.gxTpr_Apikeyenable && AV9GAMApplication.gxTpr_Clientallowremoterestauthentication )
            {
               if ( AV9GAMApplication.gxTpr_Apikeyallowscopecustomization )
               {
                  AV14ClientAllowGetUserDataREST = AV9GAMApplication.gxTpr_Clientallowgetuserdatarest;
                  AssignAttri("", false, "AV14ClientAllowGetUserDataREST", AV14ClientAllowGetUserDataREST);
                  AV15ClientAllowGetUserAddDataREST = AV9GAMApplication.gxTpr_Clientallowgetuseradditionaldatarest;
                  AssignAttri("", false, "AV15ClientAllowGetUserAddDataREST", AV15ClientAllowGetUserAddDataREST);
                  AV16ClientAllowGetUserRolesRest = AV9GAMApplication.gxTpr_Clientallowgetuserrolesrest;
                  AssignAttri("", false, "AV16ClientAllowGetUserRolesRest", AV16ClientAllowGetUserRolesRest);
                  AV17ClientAllowGetSessionIniPropREST = AV9GAMApplication.gxTpr_Clientallowgetsessioninitialpropertiesrest;
                  AssignAttri("", false, "AV17ClientAllowGetSessionIniPropREST", AV17ClientAllowGetSessionIniPropREST);
                  AV18ClientAllowGetSessionAppDataREST = AV9GAMApplication.gxTpr_Clientallowgetsessionapplicationdatarest;
                  AssignAttri("", false, "AV18ClientAllowGetSessionAppDataREST", AV18ClientAllowGetSessionAppDataREST);
                  AV19ClientAllowAdditionalScopeREST = AV9GAMApplication.gxTpr_Clientallowadditionalscoperest;
                  AssignAttri("", false, "AV19ClientAllowAdditionalScopeREST", AV19ClientAllowAdditionalScopeREST);
                  if ( ! AV14ClientAllowGetUserDataREST )
                  {
                     chkavClientallowgetuserdatarest.Visible = 0;
                     AssignProp("", false, chkavClientallowgetuserdatarest_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuserdatarest.Visible), 5, 0), true);
                  }
                  if ( ! AV15ClientAllowGetUserAddDataREST )
                  {
                     chkavClientallowgetuseradddatarest.Visible = 0;
                     AssignProp("", false, chkavClientallowgetuseradddatarest_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuseradddatarest.Visible), 5, 0), true);
                  }
                  if ( ! AV16ClientAllowGetUserRolesRest )
                  {
                     chkavClientallowgetuserrolesrest.Visible = 0;
                     AssignProp("", false, chkavClientallowgetuserrolesrest_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavClientallowgetuserrolesrest.Visible), 5, 0), true);
                  }
                  if ( ! AV17ClientAllowGetSessionIniPropREST )
                  {
                     chkavClientallowgetsessioniniproprest.Visible = 0;
                     AssignProp("", false, chkavClientallowgetsessioniniproprest_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavClientallowgetsessioniniproprest.Visible), 5, 0), true);
                  }
                  if ( ! AV18ClientAllowGetSessionAppDataREST )
                  {
                     chkavClientallowgetsessionappdatarest.Visible = 0;
                     AssignProp("", false, chkavClientallowgetsessionappdatarest_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavClientallowgetsessionappdatarest.Visible), 5, 0), true);
                  }
                  if ( ! AV14ClientAllowGetUserDataREST && ! AV15ClientAllowGetUserAddDataREST && ! AV16ClientAllowGetUserRolesRest && ! AV17ClientAllowGetSessionIniPropREST && ! AV18ClientAllowGetSessionAppDataREST )
                  {
                     divTblrestpredscopes_Visible = 0;
                     AssignProp("", false, divTblrestpredscopes_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblrestpredscopes_Visible), 5, 0), true);
                  }
                  else
                  {
                     divTblrestscopes_Visible = 1;
                     AssignProp("", false, divTblrestscopes_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblrestscopes_Visible), 5, 0), true);
                  }
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV19ClientAllowAdditionalScopeREST)) )
                  {
                     divTblrestscopes_Visible = 1;
                     AssignProp("", false, divTblrestscopes_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblrestscopes_Visible), 5, 0), true);
                  }
               }
            }
            else
            {
               context.setWebReturnParms(new Object[] {});
               context.setWebReturnParmsMetadata(new Object[] {});
               context.wjLocDisableFrm = 1;
               context.nUserReturn = 1;
               returnInSub = true;
               if (true) return;
            }
         }
         else
         {
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
      }

      protected void E12182( )
      {
         /* 'Confirm' Routine */
         returnInSub = false;
         AV11GAMUser.load( AV13UserGUID);
         if ( AV11GAMUser.success() )
         {
            new GeneXus.Programs.genexussecurity.SdtGAM(context).builddetailedscope(AV14ClientAllowGetUserDataREST, AV15ClientAllowGetUserAddDataREST, AV16ClientAllowGetUserRolesRest, AV17ClientAllowGetSessionIniPropREST, AV18ClientAllowGetSessionAppDataREST, AV19ClientAllowAdditionalScopeREST, out  AV20DetailedScope) ;
            AV21GenerateAPIKeyOK = AV11GAMUser.generateapplicationapikey(AV6ClientID, AV20DetailedScope, out  AV22UserAPIkey, out  AV23UserAPIkeyExpires, out  AV10GAMErrorCollection);
            if ( AV21GenerateAPIKeyOK )
            {
               context.CommitDataStores("gamuserapplicationapikey",pr_default);
               AV5APIkey = AV22UserAPIkey;
               AssignAttri("", false, "AV5APIkey", AV5APIkey);
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
               if ( ! (DateTime.MinValue==AV23UserAPIkeyExpires) )
               {
                  AV8Expires = AV23UserAPIkeyExpires;
                  AssignAttri("", false, "AV8Expires", context.localUtil.TToC( AV8Expires, 10, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
                  edtavExpires_Visible = 1;
                  AssignProp("", false, edtavExpires_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavExpires_Visible), 5, 0), true);
               }
               bttBtnenter_Visible = 0;
               AssignProp("", false, bttBtnenter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnenter_Visible), 5, 0), true);
               bttBtncancel_Caption = context.GetMessage( "WWP_GAM_Close", "");
               AssignProp("", false, bttBtncancel_Internalname, "Caption", bttBtncancel_Caption, true);
               GX_msglist.addItem(context.GetMessage( "WWP_GAM_YourAPIkeywasgeneratedsuccessfully", ""));
            }
         }
         /*  Sending Event outputs  */
      }

      protected void nextLoad( )
      {
      }

      protected void E13182( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV13UserGUID = (string)getParm(obj,0);
         AssignAttri("", false, "AV13UserGUID", AV13UserGUID);
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERGUID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV13UserGUID, "")), context));
         AV6ClientID = (string)getParm(obj,1);
         AssignAttri("", false, "AV6ClientID", AV6ClientID);
         GxWebStd.gx_hidden_field( context, "gxhash_vCLIENTID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ClientID, "")), context));
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
         PA182( ) ;
         WS182( ) ;
         WE182( ) ;
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
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202412162351069", true, true);
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
         context.AddJavascriptSource("gamuserapplicationapikey.js", "?202412162351076", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         chkavClientallowgetuserdatarest.Name = "vCLIENTALLOWGETUSERDATAREST";
         chkavClientallowgetuserdatarest.WebTags = "";
         chkavClientallowgetuserdatarest.Caption = context.GetMessage( "User data", "");
         AssignProp("", false, chkavClientallowgetuserdatarest_Internalname, "TitleCaption", chkavClientallowgetuserdatarest.Caption, true);
         chkavClientallowgetuserdatarest.CheckedValue = "false";
         AV14ClientAllowGetUserDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV14ClientAllowGetUserDataREST));
         AssignAttri("", false, "AV14ClientAllowGetUserDataREST", AV14ClientAllowGetUserDataREST);
         chkavClientallowgetuseradddatarest.Name = "vCLIENTALLOWGETUSERADDDATAREST";
         chkavClientallowgetuseradddatarest.WebTags = "";
         chkavClientallowgetuseradddatarest.Caption = context.GetMessage( "User additional data", "");
         AssignProp("", false, chkavClientallowgetuseradddatarest_Internalname, "TitleCaption", chkavClientallowgetuseradddatarest.Caption, true);
         chkavClientallowgetuseradddatarest.CheckedValue = "false";
         AV15ClientAllowGetUserAddDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV15ClientAllowGetUserAddDataREST));
         AssignAttri("", false, "AV15ClientAllowGetUserAddDataREST", AV15ClientAllowGetUserAddDataREST);
         chkavClientallowgetuserrolesrest.Name = "vCLIENTALLOWGETUSERROLESREST";
         chkavClientallowgetuserrolesrest.WebTags = "";
         chkavClientallowgetuserrolesrest.Caption = context.GetMessage( "User roles", "");
         AssignProp("", false, chkavClientallowgetuserrolesrest_Internalname, "TitleCaption", chkavClientallowgetuserrolesrest.Caption, true);
         chkavClientallowgetuserrolesrest.CheckedValue = "false";
         AV16ClientAllowGetUserRolesRest = StringUtil.StrToBool( StringUtil.BoolToStr( AV16ClientAllowGetUserRolesRest));
         AssignAttri("", false, "AV16ClientAllowGetUserRolesRest", AV16ClientAllowGetUserRolesRest);
         chkavClientallowgetsessioniniproprest.Name = "vCLIENTALLOWGETSESSIONINIPROPREST";
         chkavClientallowgetsessioniniproprest.WebTags = "";
         chkavClientallowgetsessioniniproprest.Caption = context.GetMessage( "Session initial properties", "");
         AssignProp("", false, chkavClientallowgetsessioniniproprest_Internalname, "TitleCaption", chkavClientallowgetsessioniniproprest.Caption, true);
         chkavClientallowgetsessioniniproprest.CheckedValue = "false";
         AV17ClientAllowGetSessionIniPropREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV17ClientAllowGetSessionIniPropREST));
         AssignAttri("", false, "AV17ClientAllowGetSessionIniPropREST", AV17ClientAllowGetSessionIniPropREST);
         chkavClientallowgetsessionappdatarest.Name = "vCLIENTALLOWGETSESSIONAPPDATAREST";
         chkavClientallowgetsessionappdatarest.WebTags = "";
         chkavClientallowgetsessionappdatarest.Caption = context.GetMessage( "Session application data", "");
         AssignProp("", false, chkavClientallowgetsessionappdatarest_Internalname, "TitleCaption", chkavClientallowgetsessionappdatarest.Caption, true);
         chkavClientallowgetsessionappdatarest.CheckedValue = "false";
         AV18ClientAllowGetSessionAppDataREST = StringUtil.StrToBool( StringUtil.BoolToStr( AV18ClientAllowGetSessionAppDataREST));
         AssignAttri("", false, "AV18ClientAllowGetSessionAppDataREST", AV18ClientAllowGetSessionAppDataREST);
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavGamuseremail_Internalname = "vGAMUSEREMAIL";
         edtavClientid_Internalname = "vCLIENTID";
         lblTextblock1_Internalname = "TEXTBLOCK1";
         chkavClientallowgetuserdatarest_Internalname = "vCLIENTALLOWGETUSERDATAREST";
         chkavClientallowgetuseradddatarest_Internalname = "vCLIENTALLOWGETUSERADDDATAREST";
         chkavClientallowgetuserrolesrest_Internalname = "vCLIENTALLOWGETUSERROLESREST";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         chkavClientallowgetsessioniniproprest_Internalname = "vCLIENTALLOWGETSESSIONINIPROPREST";
         chkavClientallowgetsessionappdatarest_Internalname = "vCLIENTALLOWGETSESSIONAPPDATAREST";
         divTblrestpredscopes_Internalname = "TBLRESTPREDSCOPES";
         edtavClientallowadditionalscoperest_Internalname = "vCLIENTALLOWADDITIONALSCOPEREST";
         divTblrestscopes_Internalname = "TBLRESTSCOPES";
         edtavExpires_Internalname = "vEXPIRES";
         edtavApikey_Internalname = "vAPIKEY";
         divTableattributes_Internalname = "TABLEATTRIBUTES";
         bttBtnenter_Internalname = "BTNENTER";
         bttBtncancel_Internalname = "BTNCANCEL";
         divTablemain_Internalname = "TABLEMAIN";
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
         chkavClientallowgetsessionappdatarest.Caption = context.GetMessage( "Session application data", "");
         chkavClientallowgetsessioniniproprest.Caption = context.GetMessage( "Session initial properties", "");
         chkavClientallowgetuserrolesrest.Caption = context.GetMessage( "User roles", "");
         chkavClientallowgetuseradddatarest.Caption = context.GetMessage( "User additional data", "");
         chkavClientallowgetuserdatarest.Caption = context.GetMessage( "User data", "");
         bttBtncancel_Caption = context.GetMessage( "GX_BtnCancel", "");
         bttBtnenter_Visible = 1;
         edtavApikey_Jsonclick = "";
         edtavApikey_Enabled = 1;
         edtavExpires_Jsonclick = "";
         edtavExpires_Enabled = 1;
         edtavExpires_Visible = 1;
         edtavClientallowadditionalscoperest_Jsonclick = "";
         edtavClientallowadditionalscoperest_Enabled = 1;
         chkavClientallowgetsessionappdatarest.Enabled = 1;
         chkavClientallowgetsessionappdatarest.Visible = 1;
         chkavClientallowgetsessioniniproprest.Enabled = 1;
         chkavClientallowgetsessioniniproprest.Visible = 1;
         chkavClientallowgetuserrolesrest.Enabled = 1;
         chkavClientallowgetuserrolesrest.Visible = 1;
         chkavClientallowgetuseradddatarest.Enabled = 1;
         chkavClientallowgetuseradddatarest.Visible = 1;
         chkavClientallowgetuserdatarest.Enabled = 1;
         chkavClientallowgetuserdatarest.Visible = 1;
         divTblrestpredscopes_Visible = 1;
         divTblrestscopes_Visible = 1;
         edtavClientid_Jsonclick = "";
         edtavClientid_Enabled = 0;
         edtavGamuseremail_Jsonclick = "";
         edtavGamuseremail_Enabled = 1;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Application API key", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV14ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV15ClientAllowGetUserAddDataREST","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV16ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV17ClientAllowGetSessionIniPropREST","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV18ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV13UserGUID","fld":"vUSERGUID","hsh":true},{"av":"AV20DetailedScope","fld":"vDETAILEDSCOPE","hsh":true},{"av":"AV22UserAPIkey","fld":"vUSERAPIKEY","hsh":true},{"av":"AV23UserAPIkeyExpires","fld":"vUSERAPIKEYEXPIRES","pic":"99/99/9999 99:99","hsh":true},{"av":"AV6ClientID","fld":"vCLIENTID","hsh":true}]}""");
         setEventMetadata("'CONFIRM'","""{"handler":"E12182","iparms":[{"av":"AV13UserGUID","fld":"vUSERGUID","hsh":true},{"av":"AV14ClientAllowGetUserDataREST","fld":"vCLIENTALLOWGETUSERDATAREST"},{"av":"AV15ClientAllowGetUserAddDataREST","fld":"vCLIENTALLOWGETUSERADDDATAREST"},{"av":"AV16ClientAllowGetUserRolesRest","fld":"vCLIENTALLOWGETUSERROLESREST"},{"av":"AV17ClientAllowGetSessionIniPropREST","fld":"vCLIENTALLOWGETSESSIONINIPROPREST"},{"av":"AV18ClientAllowGetSessionAppDataREST","fld":"vCLIENTALLOWGETSESSIONAPPDATAREST"},{"av":"AV19ClientAllowAdditionalScopeREST","fld":"vCLIENTALLOWADDITIONALSCOPEREST"},{"av":"AV20DetailedScope","fld":"vDETAILEDSCOPE","hsh":true},{"av":"AV6ClientID","fld":"vCLIENTID","hsh":true},{"av":"AV22UserAPIkey","fld":"vUSERAPIKEY","hsh":true},{"av":"AV23UserAPIkeyExpires","fld":"vUSERAPIKEYEXPIRES","pic":"99/99/9999 99:99","hsh":true}]""");
         setEventMetadata("'CONFIRM'",""","oparms":[{"av":"AV5APIkey","fld":"vAPIKEY"},{"av":"chkavClientallowgetuserdatarest.Enabled","ctrl":"vCLIENTALLOWGETUSERDATAREST","prop":"Enabled"},{"av":"chkavClientallowgetuseradddatarest.Enabled","ctrl":"vCLIENTALLOWGETUSERADDDATAREST","prop":"Enabled"},{"av":"chkavClientallowgetuserrolesrest.Enabled","ctrl":"vCLIENTALLOWGETUSERROLESREST","prop":"Enabled"},{"av":"chkavClientallowgetsessioniniproprest.Enabled","ctrl":"vCLIENTALLOWGETSESSIONINIPROPREST","prop":"Enabled"},{"av":"chkavClientallowgetsessionappdatarest.Enabled","ctrl":"vCLIENTALLOWGETSESSIONAPPDATAREST","prop":"Enabled"},{"av":"edtavClientallowadditionalscoperest_Enabled","ctrl":"vCLIENTALLOWADDITIONALSCOPEREST","prop":"Enabled"},{"av":"AV8Expires","fld":"vEXPIRES","pic":"99/99/9999 99:99"},{"av":"edtavExpires_Visible","ctrl":"vEXPIRES","prop":"Visible"},{"ctrl":"BTNENTER","prop":"Visible"},{"ctrl":"BTNCANCEL","prop":"Caption"}]}""");
         setEventMetadata("VALIDV_EXPIRES","""{"handler":"Validv_Expires","iparms":[]}""");
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
         wcpOAV13UserGUID = "";
         wcpOAV6ClientID = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV20DetailedScope = "";
         AV22UserAPIkey = "";
         AV23UserAPIkeyExpires = (DateTime)(DateTime.MinValue);
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         StyleString = "";
         TempTags = "";
         AV12GAMUserEmail = "";
         lblTextblock1_Jsonclick = "";
         AV19ClientAllowAdditionalScopeREST = "";
         AV8Expires = (DateTime)(DateTime.MinValue);
         AV5APIkey = "";
         bttBtnenter_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         AV11GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV9GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV10GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamuserapplicationapikey__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamuserapplicationapikey__default(),
            new Object[][] {
            }
         );
         /* GeneXus formulas. */
         edtavGamuseremail_Enabled = 0;
         edtavClientid_Enabled = 0;
         edtavExpires_Enabled = 0;
         edtavApikey_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short nGXWrapped ;
      private int edtavGamuseremail_Enabled ;
      private int edtavClientid_Enabled ;
      private int divTblrestscopes_Visible ;
      private int divTblrestpredscopes_Visible ;
      private int edtavClientallowadditionalscoperest_Enabled ;
      private int edtavExpires_Visible ;
      private int edtavExpires_Enabled ;
      private int edtavApikey_Enabled ;
      private int bttBtnenter_Visible ;
      private int idxLst ;
      private string AV13UserGUID ;
      private string AV6ClientID ;
      private string wcpOAV13UserGUID ;
      private string wcpOAV6ClientID ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string divTableattributes_Internalname ;
      private string edtavGamuseremail_Internalname ;
      private string TempTags ;
      private string edtavGamuseremail_Jsonclick ;
      private string edtavClientid_Internalname ;
      private string edtavClientid_Jsonclick ;
      private string divTblrestscopes_Internalname ;
      private string divTblrestpredscopes_Internalname ;
      private string lblTextblock1_Internalname ;
      private string lblTextblock1_Jsonclick ;
      private string divUnnamedtable1_Internalname ;
      private string chkavClientallowgetuserdatarest_Internalname ;
      private string chkavClientallowgetuseradddatarest_Internalname ;
      private string chkavClientallowgetuserrolesrest_Internalname ;
      private string chkavClientallowgetsessioniniproprest_Internalname ;
      private string chkavClientallowgetsessionappdatarest_Internalname ;
      private string edtavClientallowadditionalscoperest_Internalname ;
      private string edtavClientallowadditionalscoperest_Jsonclick ;
      private string edtavExpires_Internalname ;
      private string edtavExpires_Jsonclick ;
      private string edtavApikey_Internalname ;
      private string edtavApikey_Jsonclick ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Caption ;
      private string bttBtncancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private DateTime AV23UserAPIkeyExpires ;
      private DateTime AV8Expires ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool AV14ClientAllowGetUserDataREST ;
      private bool AV15ClientAllowGetUserAddDataREST ;
      private bool AV16ClientAllowGetUserRolesRest ;
      private bool AV17ClientAllowGetSessionIniPropREST ;
      private bool AV18ClientAllowGetSessionAppDataREST ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV21GenerateAPIKeyOK ;
      private string AV20DetailedScope ;
      private string AV22UserAPIkey ;
      private string AV12GAMUserEmail ;
      private string AV19ClientAllowAdditionalScopeREST ;
      private string AV5APIkey ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavClientallowgetuserdatarest ;
      private GXCheckbox chkavClientallowgetuseradddatarest ;
      private GXCheckbox chkavClientallowgetuserrolesrest ;
      private GXCheckbox chkavClientallowgetsessioniniproprest ;
      private GXCheckbox chkavClientallowgetsessionappdatarest ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV11GAMUser ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV9GAMApplication ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV10GAMErrorCollection ;
      private IDataStoreProvider pr_default ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamuserapplicationapikey__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamuserapplicationapikey__default : DataStoreHelperBase, IDataStoreHelper
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
