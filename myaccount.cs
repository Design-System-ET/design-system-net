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
   public class myaccount : GXDataArea
   {
      public myaccount( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public myaccount( IGxContext context )
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
         PA3J2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3J2( ) ;
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
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV25Name, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")), context));
         GxWebStd.gx_hidden_field( context, "vURLPROFILE", AV30URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV30URLProfile, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"MyAccount");
         forbiddenHiddens.Add("AuthenticationTypeName", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")));
         forbiddenHiddens.Add("Name", StringUtil.RTrim( context.localUtil.Format( AV25Name, "")));
         forbiddenHiddens.Add("EMail", StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("myaccount:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "vURLPROFILE", AV30URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV30URLProfile, "")), context));
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
            WE3J2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3J2( ) ;
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
         GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode));
         return formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "MyAccount" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "My Account", "") ;
      }

      protected void WB3J0( )
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
            GxWebStd.gx_div_start( context, divTblheader_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-7 col-sm-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTxtuser_Internalname, lblTxtuser_Caption, "", "", lblTxtuser_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Title", 0, "", 1, 1, 0, 0, "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-5 col-sm-6", "end", "top", "", "", "div");
            context.WriteHtmlText( "<nav class=\"navbar navbar-default gx-navbar  action-group\" data-gx-actiongroup-type=\"menu\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "container-fluid", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "navbar-header", "start", "top", "", "", "div");
            context.WriteHtmlText( "<button type=\"button\" class=\"navbar-toggle collapsed gx-navbar-toggle\" data-toggle=\"collapse\" aria-expanded=\"false\">") ;
            context.WriteHtmlText( "<span class=\"icon-bar\"></span>") ;
            context.WriteHtmlText( "<span class=\"icon-bar\"></span>") ;
            context.WriteHtmlText( "<span class=\"icon-bar\"></span>") ;
            context.WriteHtmlText( "</button>") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lbllbl13_Internalname, "", "", "", lbllbl13_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "navbar-brand", 0, "", 1, 1, 0, 0, "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divToolbar_inner_Internalname, 1, 0, "px", 0, "px", "collapse navbar-collapse gx-navbar-inner", "start", "top", "", "", "div");
            context.WriteHtmlText( "<ul class=\"nav navbar-nav\">") ;
            context.WriteHtmlText( "<li>") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblBtnedit_Internalname, context.GetMessage( "GAM_Edit", ""), "", "", lblBtnedit_Jsonclick, "'"+""+"'"+",false,"+"'"+"E\\'EDIT\\'."+"'", "", "gx-navbar-textblock action-group-option Button Primary", 5, "", lblBtnedit_Visible, 1, 0, 0, "HLP_MyAccount.htm");
            context.WriteHtmlText( "</li>") ;
            context.WriteHtmlText( "<li>") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblBtntotpauthenticator_Internalname, lblBtntotpauthenticator_Caption, "", "", lblBtntotpauthenticator_Jsonclick, "'"+""+"'"+",false,"+"'"+"E\\'AUTHENTICATORAPP\\'."+"'", "", "gx-navbar-textblock action-group-option Button Tertiary", 5, "", lblBtntotpauthenticator_Visible, 1, 0, 0, "HLP_MyAccount.htm");
            context.WriteHtmlText( "</li>") ;
            context.WriteHtmlText( "</ul>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</nav>") ;
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-5", "start", "top", "", "", "div");
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
            GxWebStd.gx_label_ctrl( context, lblGam_datacard_tbtitlegeneral_Internalname, context.GetMessage( "GAM_Generalinformation", ""), "", "", lblGam_datacard_tbtitlegeneral_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "", 0, "", 1, 1, 0, 0, "HLP_MyAccount.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, "", context.GetMessage( "GAM_ExternalImage", ""), "col-sm-3 ImageLabel", 0, true, "");
            /* Static Bitmap Variable */
            ClassString = "Image" + " " + ((StringUtil.StrCmp(imgavImage_gximage, "")==0) ? "" : "GX_Image_"+imgavImage_gximage+"_Class");
            StyleString = "";
            AV22Image_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV22Image))&&String.IsNullOrEmpty(StringUtil.RTrim( AV44Image_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV22Image)));
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV22Image)) ? AV44Image_GXI : context.PathToRelativeUrl( AV22Image));
            GxWebStd.gx_bitmap( context, imgavImage_Internalname, sImgUrl, "", "", "", context.GetTheme( ), imgavImage_Visible, 0, "", "", 0, -1, 0, "", 0, "", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", "", "", "", 1, AV22Image_IsBlob, false, context.GetImageSrcSet( sImgUrl), "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavAuthenticationtypename.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavAuthenticationtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavAuthenticationtypename_Internalname, context.GetMessage( "GAM_AuthenticationType", ""), "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavAuthenticationtypename, cmbavAuthenticationtypename_Internalname, StringUtil.RTrim( AV7AuthenticationTypeName), 1, cmbavAuthenticationtypename_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", cmbavAuthenticationtypename.Visible, cmbavAuthenticationtypename.Enabled, 0, 0, 40, "%", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"", "", true, 0, "HLP_MyAccount.htm");
            cmbavAuthenticationtypename.CurrentValue = StringUtil.RTrim( AV7AuthenticationTypeName);
            AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Values", (string)(cmbavAuthenticationtypename.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-md-8", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavName_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavName_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavName_Internalname, edtavName_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavName_Internalname, AV25Name, StringUtil.RTrim( context.localUtil.Format( AV25Name, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,42);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavName_Jsonclick, 0, "Attribute", "", "", "", "", edtavName_Visible, edtavName_Enabled, 0, "text", "", 100, "%", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-md-4", "start", "Bottom", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'',0)\"";
            ClassString = "Button Secondary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnchangename_Internalname, "", context.GetMessage( "GAM_Changename", ""), bttBtnchangename_Jsonclick, 5, context.GetMessage( "GAM_Changenickname", ""), "", StyleString, ClassString, bttBtnchangename_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CHANGENAME\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "Bottom", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-md-8", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmail_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmail_Internalname, edtavEmail_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmail_Internalname, AV14EMail, StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,49);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmail_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEmail_Enabled, 0, "text", "", 100, "%", 1, "row", 100, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMEMail", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-md-4", "start", "Bottom", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 51,'',false,'',0)\"";
            ClassString = "Button Secondary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnchangeemail_Internalname, "", context.GetMessage( "GAM_Changeemail", ""), bttBtnchangeemail_Jsonclick, 5, context.GetMessage( "GAM_Changeemail", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CHANGEEMAIL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "Bottom", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavFirstname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavFirstname_Internalname, edtavFirstname_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFirstname_Internalname, StringUtil.RTrim( AV18FirstName), StringUtil.RTrim( context.localUtil.Format( AV18FirstName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,56);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavFirstname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavFirstname_Enabled, 1, "text", "", 70, "%", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavLastname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavLastname_Internalname, edtavLastname_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 61,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavLastname_Internalname, StringUtil.RTrim( AV24LastName), StringUtil.RTrim( context.localUtil.Format( AV24LastName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,61);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavLastname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavLastname_Enabled, 1, "text", "", 70, "%", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavPhone_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavPhone_Internalname, edtavPhone_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 66,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavPhone_Internalname, StringUtil.RTrim( AV26Phone), StringUtil.RTrim( context.localUtil.Format( AV26Phone, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,66);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavPhone_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavPhone_Enabled, 1, "text", "", 70, "%", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavDontreceiveinformation_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavDontreceiveinformation_Internalname, context.GetMessage( "GAM_Dontwanttoreceiveinformation", ""), "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 71,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavDontreceiveinformation_Internalname, StringUtil.BoolToStr( AV13DontReceiveInformation), "", context.GetMessage( "GAM_Dontwanttoreceiveinformation", ""), 1, chkavDontreceiveinformation.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(71, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,71);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavEnabletwofactorauthentication.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEnabletwofactorauthentication_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEnabletwofactorauthentication_Internalname, context.GetMessage( "GAM_EnableTwoFactorAuthentication", ""), "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 76,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEnabletwofactorauthentication_Internalname, StringUtil.BoolToStr( AV15EnableTwoFactorAuthentication), "", context.GetMessage( "GAM_EnableTwoFactorAuthentication", ""), chkavEnabletwofactorauthentication.Visible, chkavEnabletwofactorauthentication.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(76, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,76);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divLastauthcell_Internalname, 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDatelastauthentication_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDatelastauthentication_Internalname, context.GetMessage( "GAM_Lastauthentication", ""), "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 81,'',false,'',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavDatelastauthentication_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavDatelastauthentication_Internalname, context.localUtil.TToC( AV12DateLastAuthentication, 10, 8, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "), context.localUtil.Format( AV12DateLastAuthentication, "99/99/99 99:99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'"+context.GetLanguageProperty( "date_fmt")+"',5,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onblur(this,81);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDatelastauthentication_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDatelastauthentication_Enabled, 0, "text", "", 14, "chr", 1, "row", 14, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_MyAccount.htm");
            GxWebStd.gx_bitmap( context, edtavDatelastauthentication_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(edtavDatelastauthentication_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_MyAccount.htm");
            context.WriteHtmlTextNl( "</div>") ;
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-7 stack-bottom-xxxxl", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divRighttable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divStencil1_Internalname, 1, 0, "px", 0, "px", "Card bg-white", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divStencil1_tablegeneraltitle_Internalname, 1, 0, "px", 0, "px", "card-heading", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblStencil1_tbtitlegeneral_Internalname, context.GetMessage( "GAM_Advancedinformation", ""), "", "", lblStencil1_tbtitlegeneral_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "", 0, "", 1, 1, 0, 0, "HLP_MyAccount.htm");
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
            GxWebStd.gx_div_start( context, divStencil1_tabledatageneral_Internalname, 1, 0, "px", 0, "px", "card-body", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavLanguage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavLanguage_Internalname, cmbavLanguage.Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 100,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavLanguage, cmbavLanguage_Internalname, StringUtil.RTrim( AV23Language), 1, cmbavLanguage_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "svchar", "", 1, cmbavLanguage.Enabled, 1, 0, 30, "%", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,100);\"", "", true, 0, "HLP_MyAccount.htm");
            cmbavLanguage.CurrentValue = StringUtil.RTrim( AV23Language);
            AssignProp("", false, cmbavLanguage_Internalname, "Values", (string)(cmbavLanguage.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavTheme_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavTheme_Internalname, context.GetMessage( "GAM_Theme", ""), "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 105,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavTheme, cmbavTheme_Internalname, StringUtil.RTrim( AV32Theme), 1, cmbavTheme_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "svchar", "", 1, cmbavTheme.Enabled, 1, 0, 30, "%", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,105);\"", "", true, 0, "HLP_MyAccount.htm");
            cmbavTheme.CurrentValue = StringUtil.RTrim( AV32Theme);
            AssignProp("", false, cmbavTheme_Internalname, "Values", (string)(cmbavTheme.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavBirthday_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavBirthday_Internalname, edtavBirthday_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 110,'',false,'',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavBirthday_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavBirthday_Internalname, context.localUtil.Format(AV10Birthday, "99/99/9999"), context.localUtil.Format( AV10Birthday, "99/99/9999"), TempTags+" onchange=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',0,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 10,'"+context.GetLanguageProperty( "date_fmt")+"',0,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onblur(this,110);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavBirthday_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavBirthday_Enabled, 1, "text", "", 20, "%", 1, "row", 10, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMDate", "end", false, "", "HLP_MyAccount.htm");
            GxWebStd.gx_bitmap( context, edtavBirthday_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(edtavBirthday_Enabled==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_MyAccount.htm");
            context.WriteHtmlTextNl( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavGender_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavGender_Internalname, cmbavGender.Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 115,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavGender, cmbavGender_Internalname, StringUtil.RTrim( AV21Gender), 1, cmbavGender_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavGender.Enabled, 1, 0, 20, "%", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,115);\"", "", true, 0, "HLP_MyAccount.htm");
            cmbavGender.CurrentValue = StringUtil.RTrim( AV21Gender);
            AssignProp("", false, cmbavGender_Internalname, "Values", (string)(cmbavGender.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAddress_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAddress_Internalname, edtavAddress_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 120,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAddress_Internalname, StringUtil.RTrim( AV5Address), StringUtil.RTrim( context.localUtil.Format( AV5Address, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,120);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAddress_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAddress_Enabled, 1, "text", "", 70, "%", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAddress2_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAddress2_Internalname, context.GetMessage( "GAM_Address2", ""), "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 125,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAddress2_Internalname, StringUtil.RTrim( AV6Address2), StringUtil.RTrim( context.localUtil.Format( AV6Address2, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,125);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAddress2_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAddress2_Enabled, 1, "text", "", 70, "%", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCity_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCity_Internalname, edtavCity_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 130,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCity_Internalname, StringUtil.RTrim( AV11City), StringUtil.RTrim( context.localUtil.Format( AV11City, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,130);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCity_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCity_Enabled, 1, "text", "", 70, "%", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavState_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavState_Internalname, edtavState_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 135,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavState_Internalname, StringUtil.RTrim( AV28State), StringUtil.RTrim( context.localUtil.Format( AV28State, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,135);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavState_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavState_Enabled, 1, "text", "", 70, "%", 1, "row", 254, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMAddress", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavPostcode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavPostcode_Internalname, edtavPostcode_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 140,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavPostcode_Internalname, StringUtil.RTrim( AV38PostCode), StringUtil.RTrim( context.localUtil.Format( AV38PostCode, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,140);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavPostcode_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavPostcode_Enabled, 1, "text", "", 70, "%", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTimezone_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTimezone_Internalname, edtavTimezone_Caption, "col-xs-12 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 145,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTimezone_Internalname, StringUtil.RTrim( AV29Timezone), StringUtil.RTrim( context.localUtil.Format( AV29Timezone, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,145);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTimezone_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTimezone_Enabled, 1, "text", "", 20, "%", 1, "row", 60, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMTimeZone", "start", true, "", "HLP_MyAccount.htm");
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
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "W0148"+"", StringUtil.RTrim( WebComp_Wcmessages_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0148"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldWcmessages), StringUtil.Lower( WebComp_Wcmessages_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0148"+"");
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
            GxWebStd.gx_div_start( context, divGam_footerentry_Internalname, divGam_footerentry_Visible, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGam_footerentry_tablebuttons_Internalname, 1, 0, "px", 0, "px", "", "start", "top", " "+"data-gx-flex"+" ", "justify-content:flex-end;align-items:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "end", "top", "", "min-height:30px;", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 156,'',false,'',0)\"";
            ClassString = "Button button-tertiary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttGam_footerentry_btncancel_Internalname, "", context.GetMessage( "GAM_Cancel", ""), bttGam_footerentry_btncancel_Jsonclick, 1, context.GetMessage( "GAM_Cancel", ""), "", StyleString, ClassString, bttGam_footerentry_btncancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_MyAccount.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 30, "px", "inline-left-l inline-right-xl", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 158,'',false,'',0)\"";
            ClassString = "Button button-primary";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttGam_footerentry_btnconfirm_Internalname, "", context.GetMessage( "GAM_Confirm", ""), bttGam_footerentry_btnconfirm_Jsonclick, 5, context.GetMessage( "GAM_Confirm", ""), "", StyleString, ClassString, bttGam_footerentry_btnconfirm_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CONFIRM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_MyAccount.htm");
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

      protected void START3J2( )
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
         Form.Meta.addItem("description", context.GetMessage( "My Account", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3J0( ) ;
      }

      protected void WS3J2( )
      {
         START3J2( ) ;
         EVT3J2( ) ;
      }

      protected void EVT3J2( )
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
                              E113J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CONFIRM'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Confirm' */
                              E123J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'EDIT'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Edit' */
                              E133J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CHANGENAME'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'ChangeName' */
                              E143J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'CHANGEEMAIL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'ChangeEmail' */
                              E153J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'AUTHENTICATORAPP'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'AuthenticatorApp' */
                              E163J2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E173J2 ();
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
                        if ( nCmpId == 148 )
                        {
                           OldWcmessages = cgiGet( "W0148");
                           if ( ( StringUtil.Len( OldWcmessages) == 0 ) || ( StringUtil.StrCmp(OldWcmessages, WebComp_Wcmessages_Component) != 0 ) )
                           {
                              WebComp_Wcmessages = getWebComponent(GetType(), "DesignSystem.Programs", OldWcmessages, new Object[] {context} );
                              WebComp_Wcmessages.ComponentInit();
                              WebComp_Wcmessages.Name = "OldWcmessages";
                              WebComp_Wcmessages_Component = OldWcmessages;
                           }
                           if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
                           {
                              WebComp_Wcmessages.componentprocess("W0148", "", sEvt);
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

      protected void WE3J2( )
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

      protected void PA3J2( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "myaccount.aspx")), "myaccount.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "myaccount.aspx")))) ;
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
               GX_FocusControl = cmbavAuthenticationtypename_Internalname;
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
            AV7AuthenticationTypeName = cmbavAuthenticationtypename.getValidValue(AV7AuthenticationTypeName);
            AssignAttri("", false, "AV7AuthenticationTypeName", AV7AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")), context));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavAuthenticationtypename.CurrentValue = StringUtil.RTrim( AV7AuthenticationTypeName);
            AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Values", cmbavAuthenticationtypename.ToJavascriptSource(), true);
         }
         AV13DontReceiveInformation = StringUtil.StrToBool( StringUtil.BoolToStr( AV13DontReceiveInformation));
         AssignAttri("", false, "AV13DontReceiveInformation", AV13DontReceiveInformation);
         AV15EnableTwoFactorAuthentication = StringUtil.StrToBool( StringUtil.BoolToStr( AV15EnableTwoFactorAuthentication));
         AssignAttri("", false, "AV15EnableTwoFactorAuthentication", AV15EnableTwoFactorAuthentication);
         if ( cmbavLanguage.ItemCount > 0 )
         {
            AV23Language = cmbavLanguage.getValidValue(AV23Language);
            AssignAttri("", false, "AV23Language", AV23Language);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavLanguage.CurrentValue = StringUtil.RTrim( AV23Language);
            AssignProp("", false, cmbavLanguage_Internalname, "Values", cmbavLanguage.ToJavascriptSource(), true);
         }
         if ( cmbavTheme.ItemCount > 0 )
         {
            AV32Theme = cmbavTheme.getValidValue(AV32Theme);
            AssignAttri("", false, "AV32Theme", AV32Theme);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavTheme.CurrentValue = StringUtil.RTrim( AV32Theme);
            AssignProp("", false, cmbavTheme_Internalname, "Values", cmbavTheme.ToJavascriptSource(), true);
         }
         if ( cmbavGender.ItemCount > 0 )
         {
            AV21Gender = cmbavGender.getValidValue(AV21Gender);
            AssignAttri("", false, "AV21Gender", AV21Gender);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavGender.CurrentValue = StringUtil.RTrim( AV21Gender);
            AssignProp("", false, cmbavGender_Internalname, "Values", cmbavGender.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF3J2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         cmbavAuthenticationtypename.Enabled = 0;
         AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavAuthenticationtypename.Enabled), 5, 0), true);
         edtavName_Enabled = 0;
         AssignProp("", false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
         edtavEmail_Enabled = 0;
         AssignProp("", false, edtavEmail_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEmail_Enabled), 5, 0), true);
         edtavDatelastauthentication_Enabled = 0;
         AssignProp("", false, edtavDatelastauthentication_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDatelastauthentication_Enabled), 5, 0), true);
      }

      protected void RF3J2( )
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
            E173J2 ();
            WB3J0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3J2( )
      {
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV25Name, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")), context));
         GxWebStd.gx_hidden_field( context, "vURLPROFILE", AV30URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV30URLProfile, "")), context));
      }

      protected void before_start_formulas( )
      {
         cmbavAuthenticationtypename.Enabled = 0;
         AssignProp("", false, cmbavAuthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavAuthenticationtypename.Enabled), 5, 0), true);
         edtavName_Enabled = 0;
         AssignProp("", false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
         edtavEmail_Enabled = 0;
         AssignProp("", false, edtavEmail_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavEmail_Enabled), 5, 0), true);
         edtavDatelastauthentication_Enabled = 0;
         AssignProp("", false, edtavDatelastauthentication_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDatelastauthentication_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3J0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E113J2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            AV22Image = cgiGet( imgavImage_Internalname);
            cmbavAuthenticationtypename.CurrentValue = cgiGet( cmbavAuthenticationtypename_Internalname);
            AV7AuthenticationTypeName = cgiGet( cmbavAuthenticationtypename_Internalname);
            AssignAttri("", false, "AV7AuthenticationTypeName", AV7AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")), context));
            AV25Name = cgiGet( edtavName_Internalname);
            AssignAttri("", false, "AV25Name", AV25Name);
            GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV25Name, "")), context));
            AV14EMail = cgiGet( edtavEmail_Internalname);
            AssignAttri("", false, "AV14EMail", AV14EMail);
            GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")), context));
            AV18FirstName = cgiGet( edtavFirstname_Internalname);
            AssignAttri("", false, "AV18FirstName", AV18FirstName);
            AV24LastName = cgiGet( edtavLastname_Internalname);
            AssignAttri("", false, "AV24LastName", AV24LastName);
            AV26Phone = cgiGet( edtavPhone_Internalname);
            AssignAttri("", false, "AV26Phone", AV26Phone);
            AV13DontReceiveInformation = StringUtil.StrToBool( cgiGet( chkavDontreceiveinformation_Internalname));
            AssignAttri("", false, "AV13DontReceiveInformation", AV13DontReceiveInformation);
            AV15EnableTwoFactorAuthentication = StringUtil.StrToBool( cgiGet( chkavEnabletwofactorauthentication_Internalname));
            AssignAttri("", false, "AV15EnableTwoFactorAuthentication", AV15EnableTwoFactorAuthentication);
            if ( context.localUtil.VCDateTime( cgiGet( edtavDatelastauthentication_Internalname), (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt"))), (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0))) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_baddatetime", new   object[]  {context.GetMessage( "Date Last Authentication", "")}), 1, "vDATELASTAUTHENTICATION");
               GX_FocusControl = edtavDatelastauthentication_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV12DateLastAuthentication = (DateTime)(DateTime.MinValue);
               AssignAttri("", false, "AV12DateLastAuthentication", context.localUtil.TToC( AV12DateLastAuthentication, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            else
            {
               AV12DateLastAuthentication = context.localUtil.CToT( cgiGet( edtavDatelastauthentication_Internalname));
               AssignAttri("", false, "AV12DateLastAuthentication", context.localUtil.TToC( AV12DateLastAuthentication, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
            }
            cmbavLanguage.CurrentValue = cgiGet( cmbavLanguage_Internalname);
            AV23Language = cgiGet( cmbavLanguage_Internalname);
            AssignAttri("", false, "AV23Language", AV23Language);
            cmbavTheme.CurrentValue = cgiGet( cmbavTheme_Internalname);
            AV32Theme = cgiGet( cmbavTheme_Internalname);
            AssignAttri("", false, "AV32Theme", AV32Theme);
            if ( context.localUtil.VCDate( cgiGet( edtavBirthday_Internalname), (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")))) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_faildate", new   object[]  {context.GetMessage( "Birthday", "")}), 1, "vBIRTHDAY");
               GX_FocusControl = edtavBirthday_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV10Birthday = DateTime.MinValue;
               AssignAttri("", false, "AV10Birthday", context.localUtil.Format(AV10Birthday, "99/99/9999"));
            }
            else
            {
               AV10Birthday = context.localUtil.CToD( cgiGet( edtavBirthday_Internalname), DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")));
               AssignAttri("", false, "AV10Birthday", context.localUtil.Format(AV10Birthday, "99/99/9999"));
            }
            cmbavGender.CurrentValue = cgiGet( cmbavGender_Internalname);
            AV21Gender = cgiGet( cmbavGender_Internalname);
            AssignAttri("", false, "AV21Gender", AV21Gender);
            AV5Address = cgiGet( edtavAddress_Internalname);
            AssignAttri("", false, "AV5Address", AV5Address);
            AV6Address2 = cgiGet( edtavAddress2_Internalname);
            AssignAttri("", false, "AV6Address2", AV6Address2);
            AV11City = cgiGet( edtavCity_Internalname);
            AssignAttri("", false, "AV11City", AV11City);
            AV28State = cgiGet( edtavState_Internalname);
            AssignAttri("", false, "AV28State", AV28State);
            AV38PostCode = cgiGet( edtavPostcode_Internalname);
            AssignAttri("", false, "AV38PostCode", AV38PostCode);
            AV29Timezone = cgiGet( edtavTimezone_Internalname);
            AssignAttri("", false, "AV29Timezone", AV29Timezone);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"MyAccount");
            AV7AuthenticationTypeName = cgiGet( cmbavAuthenticationtypename_Internalname);
            AssignAttri("", false, "AV7AuthenticationTypeName", AV7AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")), context));
            forbiddenHiddens.Add("AuthenticationTypeName", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")));
            AV25Name = cgiGet( edtavName_Internalname);
            AssignAttri("", false, "AV25Name", AV25Name);
            GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV25Name, "")), context));
            forbiddenHiddens.Add("Name", StringUtil.RTrim( context.localUtil.Format( AV25Name, "")));
            AV14EMail = cgiGet( edtavEmail_Internalname);
            AssignAttri("", false, "AV14EMail", AV14EMail);
            GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")), context));
            forbiddenHiddens.Add("EMail", StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("myaccount:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
         E113J2 ();
         if (returnInSub) return;
      }

      protected void E113J2( )
      {
         /* Start Routine */
         returnInSub = false;
         lblBtnedit_Visible = 0;
         AssignProp("", false, lblBtnedit_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblBtnedit_Visible), 5, 0), true);
         lblBtntotpauthenticator_Visible = 0;
         AssignProp("", false, lblBtntotpauthenticator_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblBtntotpauthenticator_Visible), 5, 0), true);
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

      protected void E123J2( )
      {
         /* 'Confirm' Routine */
         returnInSub = false;
         AV20gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         AV20gamUser.load( AV20gamUser.gxTpr_Guid);
         if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
         {
            divGam_footerentry_Visible = 1;
            AssignProp("", false, divGam_footerentry_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divGam_footerentry_Visible), 5, 0), true);
            AV20gamUser.gxTpr_Authenticationtypename = AV7AuthenticationTypeName;
            AV20gamUser.gxTpr_Name = AV25Name;
            AV20gamUser.gxTpr_Email = AV14EMail;
            AV20gamUser.gxTpr_Firstname = AV18FirstName;
            AV20gamUser.gxTpr_Lastname = AV24LastName;
            AV20gamUser.gxTpr_Birthday = AV10Birthday;
            AV20gamUser.gxTpr_Gender = AV21Gender;
            AV20gamUser.gxTpr_Phone = AV26Phone;
            AV20gamUser.gxTpr_Address = AV5Address;
            AV20gamUser.gxTpr_Address2 = AV6Address2;
            AV20gamUser.gxTpr_City = AV11City;
            AV20gamUser.gxTpr_State = AV28State;
            AV20gamUser.gxTpr_Theme = AV32Theme;
            AV20gamUser.gxTpr_Timezone = AV29Timezone;
            AV20gamUser.gxTpr_Urlprofile = AV30URLProfile;
            AV20gamUser.gxTpr_Dontreceiveinformation = AV13DontReceiveInformation;
            AV20gamUser.gxTpr_Enabletwofactorauthentication = AV15EnableTwoFactorAuthentication;
            AV35GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
            AV34GAMLanguages = AV35GAMApplication.gxTpr_Languages;
            AV41GXV1 = 1;
            while ( AV41GXV1 <= AV34GAMLanguages.Count )
            {
               AV33GAMLanguage = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage)AV34GAMLanguages.Item(AV41GXV1));
               if ( StringUtil.StrCmp(AV33GAMLanguage.gxTpr_Culture, AV23Language) == 0 )
               {
                  AV20gamUser.gxTpr_Language = AV33GAMLanguage.gxTpr_Culture;
                  if (true) break;
               }
               AV41GXV1 = (int)(AV41GXV1+1);
            }
            AV20gamUser.save();
            if ( AV20gamUser.success() )
            {
               context.CommitDataStores("myaccount",pr_default);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
               CallWebObject(formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
               context.wjLocDisableFrm = 1;
            }
            else
            {
               AV17GAMErrorCollection = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrors();
               AV42GXV2 = 1;
               while ( AV42GXV2 <= AV17GAMErrorCollection.Count )
               {
                  AV16GAMError = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV17GAMErrorCollection.Item(AV42GXV2));
                  GX_msglist.addItem(AV16GAMError.gxTpr_Message);
                  AV42GXV2 = (int)(AV42GXV2+1);
               }
            }
         }
         /*  Sending Event outputs  */
      }

      protected void E133J2( )
      {
         /* 'Edit' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim("UPD"));
         CallWebObject(formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
      }

      protected void E143J2( )
      {
         /* 'ChangeName' Routine */
         returnInSub = false;
         /* Window Datatype Object Property */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamexampleuserchangeidentification.aspx"+UrlEncode(StringUtil.RTrim("Name"));
         AV31Window.Url = formatLink("gamexampleuserchangeidentification.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         AV31Window.SetReturnParms(new Object[] {"",});
         context.NewWindow(AV31Window);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
         CallWebObject(formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void E153J2( )
      {
         /* 'ChangeEmail' Routine */
         returnInSub = false;
         /* Window Datatype Object Property */
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamexampleuserchangeidentification.aspx"+UrlEncode(StringUtil.RTrim("Email"));
         AV31Window.Url = formatLink("gamexampleuserchangeidentification.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         AV31Window.SetReturnParms(new Object[] {"",});
         context.NewWindow(AV31Window);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
         CallWebObject(formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void E163J2( )
      {
         /* 'AuthenticatorApp' Routine */
         returnInSub = false;
         AV20gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         if ( AV20gamUser.gxTpr_Totpenable )
         {
            /* Window Datatype Object Property */
            AV31Window.Url = formatLink("gamexampleauthenticatordisable.aspx") ;
            AV31Window.SetReturnParms(new Object[] {});
            context.NewWindow(AV31Window);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "myaccount.aspx"+UrlEncode(StringUtil.RTrim("DSP"));
            CallWebObject(formatLink("myaccount.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            CallWebObject(formatLink("gamexampleauthenticatorenable.aspx") );
            context.wjLocDisableFrm = 1;
         }
         /*  Sending Event outputs  */
      }

      protected void S122( )
      {
         /* 'INITFORM' Routine */
         returnInSub = false;
         AV19GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
         AV20gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         if ( AV19GAMRepository.istotpauthenticatorenabled() )
         {
            lblBtntotpauthenticator_Visible = 1;
            AssignProp("", false, lblBtntotpauthenticator_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblBtntotpauthenticator_Visible), 5, 0), true);
            if ( AV20gamUser.gxTpr_Totpenable )
            {
               lblBtntotpauthenticator_Caption = context.GetMessage( "GAM_Disableauthenticator", "");
               AssignProp("", false, lblBtntotpauthenticator_Internalname, "Caption", lblBtntotpauthenticator_Caption, true);
            }
            else
            {
               lblBtntotpauthenticator_Caption = context.GetMessage( "GAM_Enableauthenticator", "");
               AssignProp("", false, lblBtntotpauthenticator_Internalname, "Caption", lblBtntotpauthenticator_Caption, true);
            }
         }
         /* Execute user subroutine: 'MARKREQUIEREDUSERDATA' */
         S132 ();
         if (returnInSub) return;
         if ( (0==AV19GAMRepository.gxTpr_Authenticationmasterrepositoryid) )
         {
            cmbavAuthenticationtypename.removeAllItems();
            AV8AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV23Language, out  AV17GAMErrorCollection);
            AV43GXV3 = 1;
            while ( AV43GXV3 <= AV8AuthenticationTypes.Count )
            {
               AV9AuthenticationTypesIns = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV8AuthenticationTypes.Item(AV43GXV3));
               cmbavAuthenticationtypename.addItem(AV9AuthenticationTypesIns.gxTpr_Name, AV9AuthenticationTypesIns.gxTpr_Description, 0);
               AV43GXV3 = (int)(AV43GXV3+1);
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
            bttGam_footerentry_btncancel_Visible = 0;
            AssignProp("", false, bttGam_footerentry_btncancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttGam_footerentry_btncancel_Visible), 5, 0), true);
         }
         AV7AuthenticationTypeName = AV20gamUser.gxTpr_Authenticationtypename;
         AssignAttri("", false, "AV7AuthenticationTypeName", AV7AuthenticationTypeName);
         GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")), context));
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV20gamUser.gxTpr_Firstname)) && String.IsNullOrEmpty(StringUtil.RTrim( AV20gamUser.gxTpr_Lastname)) )
         {
            lblTxtuser_Caption = AV20gamUser.gxTpr_Name;
            AssignProp("", false, lblTxtuser_Internalname, "Caption", lblTxtuser_Caption, true);
         }
         else
         {
            lblTxtuser_Caption = StringUtil.Format( "%1 %2", AV20gamUser.gxTpr_Firstname, AV20gamUser.gxTpr_Lastname, "", "", "", "", "", "", "");
            AssignProp("", false, lblTxtuser_Internalname, "Caption", lblTxtuser_Caption, true);
         }
         /* Execute user subroutine: 'LOADLANGUAGES' */
         S142 ();
         if (returnInSub) return;
         if ( StringUtil.StrCmp(AV19GAMRepository.gxTpr_Useridentification, "email") == 0 )
         {
            edtavName_Visible = 0;
            AssignProp("", false, edtavName_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavName_Visible), 5, 0), true);
            bttBtnchangename_Visible = 0;
            AssignProp("", false, bttBtnchangename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnchangename_Visible), 5, 0), true);
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV20gamUser.gxTpr_Urlimage)) )
         {
            AV22Image = GXUtil.UrlEncode( AV20gamUser.gxTpr_Urlimage);
            AssignProp("", false, imgavImage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV22Image)) ? AV44Image_GXI : context.convertURL( context.PathToRelativeUrl( AV22Image))), true);
            AssignProp("", false, imgavImage_Internalname, "SrcSet", context.GetImageSrcSet( AV22Image), true);
            AV44Image_GXI = GXDbFile.PathToUrl( GXUtil.UrlEncode( AV20gamUser.gxTpr_Urlimage), context);
            AssignProp("", false, imgavImage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV22Image)) ? AV44Image_GXI : context.convertURL( context.PathToRelativeUrl( AV22Image))), true);
            AssignProp("", false, imgavImage_Internalname, "SrcSet", context.GetImageSrcSet( AV22Image), true);
         }
         else
         {
            imgavImage_Visible = 0;
            AssignProp("", false, imgavImage_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavImage_Visible), 5, 0), true);
         }
         AV25Name = AV20gamUser.gxTpr_Name;
         AssignAttri("", false, "AV25Name", AV25Name);
         GxWebStd.gx_hidden_field( context, "gxhash_vNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV25Name, "")), context));
         AV14EMail = AV20gamUser.gxTpr_Email;
         AssignAttri("", false, "AV14EMail", AV14EMail);
         GxWebStd.gx_hidden_field( context, "gxhash_vEMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14EMail, "")), context));
         AV18FirstName = AV20gamUser.gxTpr_Firstname;
         AssignAttri("", false, "AV18FirstName", AV18FirstName);
         AV24LastName = AV20gamUser.gxTpr_Lastname;
         AssignAttri("", false, "AV24LastName", AV24LastName);
         AV10Birthday = AV20gamUser.gxTpr_Birthday;
         AssignAttri("", false, "AV10Birthday", context.localUtil.Format(AV10Birthday, "99/99/9999"));
         AV21Gender = AV20gamUser.gxTpr_Gender;
         AssignAttri("", false, "AV21Gender", AV21Gender);
         AV26Phone = AV20gamUser.gxTpr_Phone;
         AssignAttri("", false, "AV26Phone", AV26Phone);
         AV5Address = AV20gamUser.gxTpr_Address;
         AssignAttri("", false, "AV5Address", AV5Address);
         AV6Address2 = AV20gamUser.gxTpr_Address2;
         AssignAttri("", false, "AV6Address2", AV6Address2);
         AV11City = AV20gamUser.gxTpr_City;
         AssignAttri("", false, "AV11City", AV11City);
         AV28State = AV20gamUser.gxTpr_State;
         AssignAttri("", false, "AV28State", AV28State);
         AV38PostCode = AV20gamUser.gxTpr_Postcode;
         AssignAttri("", false, "AV38PostCode", AV38PostCode);
         AV23Language = AV20gamUser.gxTpr_Language;
         AssignAttri("", false, "AV23Language", AV23Language);
         AV32Theme = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).gettheme();
         AssignAttri("", false, "AV32Theme", AV32Theme);
         AV29Timezone = AV20gamUser.gxTpr_Timezone;
         AssignAttri("", false, "AV29Timezone", AV29Timezone);
         AV30URLProfile = AV20gamUser.gxTpr_Urlprofile;
         AssignAttri("", false, "AV30URLProfile", AV30URLProfile);
         GxWebStd.gx_hidden_field( context, "gxhash_vURLPROFILE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV30URLProfile, "")), context));
         AV13DontReceiveInformation = AV20gamUser.gxTpr_Dontreceiveinformation;
         AssignAttri("", false, "AV13DontReceiveInformation", AV13DontReceiveInformation);
         AV12DateLastAuthentication = AV20gamUser.gxTpr_Datelastauthentication;
         AssignAttri("", false, "AV12DateLastAuthentication", context.localUtil.TToC( AV12DateLastAuthentication, 8, 5, (short)(((StringUtil.StrCmp(context.GetLanguageProperty( "time_fmt"), "12")==0) ? 1 : 0)), (short)(DateTimeUtil.MapDateTimeFormat( context.GetLanguageProperty( "date_fmt"))), "/", ":", " "));
         AV15EnableTwoFactorAuthentication = AV20gamUser.gxTpr_Enabletwofactorauthentication;
         AssignAttri("", false, "AV15EnableTwoFactorAuthentication", AV15EnableTwoFactorAuthentication);
         if ( AV19GAMRepository.istwofactorauthenticationenabled() )
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
            bttGam_footerentry_btnconfirm_Visible = 0;
            AssignProp("", false, bttGam_footerentry_btnconfirm_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttGam_footerentry_btnconfirm_Visible), 5, 0), true);
            lblBtnedit_Visible = 1;
            AssignProp("", false, lblBtnedit_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblBtnedit_Visible), 5, 0), true);
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
         if ( ( StringUtil.StrCmp(AV19GAMRepository.gxTpr_Useridentification, "email") == 0 ) || ( StringUtil.StrCmp(AV19GAMRepository.gxTpr_Useridentification, "namema") == 0 ) )
         {
            if ( AV19GAMRepository.gxTpr_Requiredemail )
            {
               edtavEmail_Caption = edtavEmail_Caption+"  *";
               AssignProp("", false, edtavEmail_Internalname, "Caption", edtavEmail_Caption, true);
            }
         }
         if ( AV19GAMRepository.gxTpr_Requiredfirstname )
         {
            edtavFirstname_Caption = edtavFirstname_Caption+"  *";
            AssignProp("", false, edtavFirstname_Internalname, "Caption", edtavFirstname_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredlastname )
         {
            edtavLastname_Caption = edtavLastname_Caption+"  *";
            AssignProp("", false, edtavLastname_Internalname, "Caption", edtavLastname_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredphone )
         {
            edtavPhone_Caption = edtavPhone_Caption+"  *";
            AssignProp("", false, edtavPhone_Internalname, "Caption", edtavPhone_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredbirthday )
         {
            edtavBirthday_Caption = edtavBirthday_Caption+"  *";
            AssignProp("", false, edtavBirthday_Internalname, "Caption", edtavBirthday_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredgender )
         {
            cmbavGender.Caption = cmbavGender.Caption+"  *";
            AssignProp("", false, cmbavGender_Internalname, "Caption", cmbavGender.Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredaddress )
         {
            edtavAddress_Caption = edtavAddress_Caption+"  *";
            AssignProp("", false, edtavAddress_Internalname, "Caption", edtavAddress_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredcity )
         {
            edtavCity_Caption = edtavCity_Caption+"  *";
            AssignProp("", false, edtavCity_Internalname, "Caption", edtavCity_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredstate )
         {
            edtavState_Caption = edtavState_Caption+"  *";
            AssignProp("", false, edtavState_Internalname, "Caption", edtavState_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredpostcode )
         {
            edtavPostcode_Caption = edtavPostcode_Caption+"  *";
            AssignProp("", false, edtavPostcode_Internalname, "Caption", edtavPostcode_Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredlanguage )
         {
            cmbavLanguage.Caption = cmbavLanguage.Caption+"  *";
            AssignProp("", false, cmbavLanguage_Internalname, "Caption", cmbavLanguage.Caption, true);
         }
         if ( AV19GAMRepository.gxTpr_Requiredtimezone )
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
            WebComp_Wcmessages.componentprepare(new Object[] {(string)"W0148",(string)""});
            WebComp_Wcmessages.componentbind(new Object[] {});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Wcmessages )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0148"+"");
            WebComp_Wcmessages.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
      }

      protected void S142( )
      {
         /* 'LOADLANGUAGES' Routine */
         returnInSub = false;
         AV35GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
         AV34GAMLanguages = AV35GAMApplication.gxTpr_Languages;
         AV45GXV4 = 1;
         while ( AV45GXV4 <= AV34GAMLanguages.Count )
         {
            AV33GAMLanguage = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage)AV34GAMLanguages.Item(AV45GXV4));
            if ( AV33GAMLanguage.gxTpr_Online )
            {
               cmbavLanguage.addItem(AV33GAMLanguage.gxTpr_Culture, AV33GAMLanguage.gxTpr_Description, 0);
            }
            AV45GXV4 = (int)(AV45GXV4+1);
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E173J2( )
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
         PA3J2( ) ;
         WS3J2( ) ;
         WE3J2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217085769", true, true);
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
         context.AddJavascriptSource("myaccount.js", "?20241217085772", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavAuthenticationtypename.Name = "vAUTHENTICATIONTYPENAME";
         cmbavAuthenticationtypename.WebTags = "";
         if ( cmbavAuthenticationtypename.ItemCount > 0 )
         {
            AV7AuthenticationTypeName = cmbavAuthenticationtypename.getValidValue(AV7AuthenticationTypeName);
            AssignAttri("", false, "AV7AuthenticationTypeName", AV7AuthenticationTypeName);
            GxWebStd.gx_hidden_field( context, "gxhash_vAUTHENTICATIONTYPENAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7AuthenticationTypeName, "")), context));
         }
         chkavDontreceiveinformation.Name = "vDONTRECEIVEINFORMATION";
         chkavDontreceiveinformation.WebTags = "";
         chkavDontreceiveinformation.Caption = context.GetMessage( "GAM_Dontwanttoreceiveinformation", "");
         AssignProp("", false, chkavDontreceiveinformation_Internalname, "TitleCaption", chkavDontreceiveinformation.Caption, true);
         chkavDontreceiveinformation.CheckedValue = "false";
         AV13DontReceiveInformation = StringUtil.StrToBool( StringUtil.BoolToStr( AV13DontReceiveInformation));
         AssignAttri("", false, "AV13DontReceiveInformation", AV13DontReceiveInformation);
         chkavEnabletwofactorauthentication.Name = "vENABLETWOFACTORAUTHENTICATION";
         chkavEnabletwofactorauthentication.WebTags = "";
         chkavEnabletwofactorauthentication.Caption = context.GetMessage( "GAM_EnableTwoFactorAuthentication", "");
         AssignProp("", false, chkavEnabletwofactorauthentication_Internalname, "TitleCaption", chkavEnabletwofactorauthentication.Caption, true);
         chkavEnabletwofactorauthentication.CheckedValue = "false";
         AV15EnableTwoFactorAuthentication = StringUtil.StrToBool( StringUtil.BoolToStr( AV15EnableTwoFactorAuthentication));
         AssignAttri("", false, "AV15EnableTwoFactorAuthentication", AV15EnableTwoFactorAuthentication);
         cmbavLanguage.Name = "vLANGUAGE";
         cmbavLanguage.WebTags = "";
         cmbavLanguage.addItem("", context.GetMessage( "GAM_None", ""), 0);
         if ( cmbavLanguage.ItemCount > 0 )
         {
            AV23Language = cmbavLanguage.getValidValue(AV23Language);
            AssignAttri("", false, "AV23Language", AV23Language);
         }
         cmbavTheme.Name = "vTHEME";
         cmbavTheme.WebTags = "";
         cmbavTheme.addItem("light", context.GetMessage( "GAM_Light", ""), 0);
         cmbavTheme.addItem("dark", context.GetMessage( "GAM_Dark", ""), 0);
         if ( cmbavTheme.ItemCount > 0 )
         {
            AV32Theme = cmbavTheme.getValidValue(AV32Theme);
            AssignAttri("", false, "AV32Theme", AV32Theme);
         }
         cmbavGender.Name = "vGENDER";
         cmbavGender.WebTags = "";
         cmbavGender.addItem("N", context.GetMessage( "GAM_NotSpecified", ""), 0);
         cmbavGender.addItem("F", context.GetMessage( "GAM_Female", ""), 0);
         cmbavGender.addItem("M", context.GetMessage( "GAM_Male", ""), 0);
         if ( cmbavGender.ItemCount > 0 )
         {
            AV21Gender = cmbavGender.getValidValue(AV21Gender);
            AssignAttri("", false, "AV21Gender", AV21Gender);
         }
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblTxtuser_Internalname = "TXTUSER";
         lbllbl13_Internalname = "LBL13";
         lblBtnedit_Internalname = "BTNEDIT";
         lblBtntotpauthenticator_Internalname = "BTNTOTPAUTHENTICATOR";
         divToolbar_inner_Internalname = "TOOLBAR_INNER";
         divTblheader_Internalname = "TBLHEADER";
         lblGam_datacard_tbtitlegeneral_Internalname = "GAM_DATACARD_TBTITLEGENERAL";
         divGam_datacard_tablegeneraltitle_Internalname = "GAM_DATACARD_TABLEGENERALTITLE";
         imgavImage_Internalname = "vIMAGE";
         cmbavAuthenticationtypename_Internalname = "vAUTHENTICATIONTYPENAME";
         edtavName_Internalname = "vNAME";
         bttBtnchangename_Internalname = "BTNCHANGENAME";
         edtavEmail_Internalname = "vEMAIL";
         bttBtnchangeemail_Internalname = "BTNCHANGEEMAIL";
         edtavFirstname_Internalname = "vFIRSTNAME";
         edtavLastname_Internalname = "vLASTNAME";
         edtavPhone_Internalname = "vPHONE";
         chkavDontreceiveinformation_Internalname = "vDONTRECEIVEINFORMATION";
         chkavEnabletwofactorauthentication_Internalname = "vENABLETWOFACTORAUTHENTICATION";
         edtavDatelastauthentication_Internalname = "vDATELASTAUTHENTICATION";
         divLastauthcell_Internalname = "LASTAUTHCELL";
         divGam_datacard_tabledatageneral_Internalname = "GAM_DATACARD_TABLEDATAGENERAL";
         divGam_datacard_Internalname = "GAM_DATACARD";
         lblStencil1_tbtitlegeneral_Internalname = "STENCIL1_TBTITLEGENERAL";
         divStencil1_tablegeneraltitle_Internalname = "STENCIL1_TABLEGENERALTITLE";
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
         divStencil1_Internalname = "STENCIL1";
         divRighttable_Internalname = "RIGHTTABLE";
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
         chkavEnabletwofactorauthentication.Caption = context.GetMessage( "GAM_EnableTwoFactorAuthentication", "");
         chkavDontreceiveinformation.Caption = context.GetMessage( "GAM_Dontwanttoreceiveinformation", "");
         bttGam_footerentry_btnconfirm_Visible = 1;
         bttGam_footerentry_btncancel_Visible = 1;
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
         edtavDatelastauthentication_Jsonclick = "";
         edtavDatelastauthentication_Enabled = 1;
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
         edtavEmail_Jsonclick = "";
         edtavEmail_Enabled = 1;
         edtavEmail_Caption = context.GetMessage( "GAM_Email", "");
         bttBtnchangename_Visible = 1;
         edtavName_Jsonclick = "";
         edtavName_Enabled = 1;
         edtavName_Caption = context.GetMessage( "GAM_UserName", "");
         edtavName_Visible = 1;
         cmbavAuthenticationtypename_Jsonclick = "";
         cmbavAuthenticationtypename.Enabled = 1;
         cmbavAuthenticationtypename.Visible = 1;
         imgavImage_gximage = "";
         imgavImage_Visible = 1;
         lblBtntotpauthenticator_Caption = context.GetMessage( "GAM_Enableauthenticator", "");
         lblBtntotpauthenticator_Visible = 1;
         lblBtnedit_Visible = 1;
         lblTxtuser_Caption = context.GetMessage( "GAM_User", "");
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "My Account", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV13DontReceiveInformation","fld":"vDONTRECEIVEINFORMATION"},{"av":"AV15EnableTwoFactorAuthentication","fld":"vENABLETWOFACTORAUTHENTICATION"},{"av":"AV30URLProfile","fld":"vURLPROFILE","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"cmbavAuthenticationtypename"},{"av":"AV7AuthenticationTypeName","fld":"vAUTHENTICATIONTYPENAME","hsh":true},{"av":"AV25Name","fld":"vNAME","hsh":true},{"av":"AV14EMail","fld":"vEMAIL","hsh":true}]}""");
         setEventMetadata("'CONFIRM'","""{"handler":"E123J2","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"cmbavAuthenticationtypename"},{"av":"AV7AuthenticationTypeName","fld":"vAUTHENTICATIONTYPENAME","hsh":true},{"av":"AV25Name","fld":"vNAME","hsh":true},{"av":"AV14EMail","fld":"vEMAIL","hsh":true},{"av":"AV18FirstName","fld":"vFIRSTNAME"},{"av":"AV24LastName","fld":"vLASTNAME"},{"av":"AV10Birthday","fld":"vBIRTHDAY"},{"av":"cmbavGender"},{"av":"AV21Gender","fld":"vGENDER"},{"av":"AV26Phone","fld":"vPHONE"},{"av":"AV5Address","fld":"vADDRESS"},{"av":"AV6Address2","fld":"vADDRESS2"},{"av":"AV11City","fld":"vCITY"},{"av":"AV28State","fld":"vSTATE"},{"av":"cmbavTheme"},{"av":"AV32Theme","fld":"vTHEME"},{"av":"AV29Timezone","fld":"vTIMEZONE"},{"av":"AV30URLProfile","fld":"vURLPROFILE","hsh":true},{"av":"AV13DontReceiveInformation","fld":"vDONTRECEIVEINFORMATION"},{"av":"AV15EnableTwoFactorAuthentication","fld":"vENABLETWOFACTORAUTHENTICATION"},{"av":"cmbavLanguage"},{"av":"AV23Language","fld":"vLANGUAGE"}]""");
         setEventMetadata("'CONFIRM'",""","oparms":[{"av":"divGam_footerentry_Visible","ctrl":"GAM_FOOTERENTRY","prop":"Visible"}]}""");
         setEventMetadata("'EDIT'","""{"handler":"E133J2","iparms":[]}""");
         setEventMetadata("'CHANGENAME'","""{"handler":"E143J2","iparms":[]}""");
         setEventMetadata("'CHANGEEMAIL'","""{"handler":"E153J2","iparms":[]}""");
         setEventMetadata("'AUTHENTICATORAPP'","""{"handler":"E163J2","iparms":[]}""");
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
         AV7AuthenticationTypeName = "";
         AV25Name = "";
         AV14EMail = "";
         AV30URLProfile = "";
         forbiddenHiddens = new GXProperties();
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblTxtuser_Jsonclick = "";
         lbllbl13_Jsonclick = "";
         lblBtnedit_Jsonclick = "";
         lblBtntotpauthenticator_Jsonclick = "";
         lblGam_datacard_tbtitlegeneral_Jsonclick = "";
         ClassString = "";
         StyleString = "";
         AV22Image = "";
         AV44Image_GXI = "";
         sImgUrl = "";
         TempTags = "";
         bttBtnchangename_Jsonclick = "";
         bttBtnchangeemail_Jsonclick = "";
         AV18FirstName = "";
         AV24LastName = "";
         AV26Phone = "";
         AV12DateLastAuthentication = (DateTime)(DateTime.MinValue);
         lblStencil1_tbtitlegeneral_Jsonclick = "";
         AV23Language = "";
         AV32Theme = "";
         AV10Birthday = DateTime.MinValue;
         AV21Gender = "";
         AV5Address = "";
         AV6Address2 = "";
         AV11City = "";
         AV28State = "";
         AV38PostCode = "";
         AV29Timezone = "";
         WebComp_Wcmessages_Component = "";
         OldWcmessages = "";
         bttGam_footerentry_btncancel_Jsonclick = "";
         bttGam_footerentry_btnconfirm_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         hsh = "";
         AV20gamUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV35GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV34GAMLanguages = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage>( context, "GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage", "DesignSystem.Programs");
         AV33GAMLanguage = new GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage(context);
         AV17GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV16GAMError = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         AV31Window = new GXWindow();
         AV19GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV8AuthenticationTypes = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple", "DesignSystem.Programs");
         AV9AuthenticationTypesIns = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.myaccount__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.myaccount__default(),
            new Object[][] {
            }
         );
         WebComp_Wcmessages = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         cmbavAuthenticationtypename.Enabled = 0;
         edtavName_Enabled = 0;
         edtavEmail_Enabled = 0;
         edtavDatelastauthentication_Enabled = 0;
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
      private int lblBtnedit_Visible ;
      private int lblBtntotpauthenticator_Visible ;
      private int imgavImage_Visible ;
      private int edtavName_Visible ;
      private int edtavName_Enabled ;
      private int bttBtnchangename_Visible ;
      private int edtavEmail_Enabled ;
      private int edtavFirstname_Enabled ;
      private int edtavLastname_Enabled ;
      private int edtavPhone_Enabled ;
      private int edtavDatelastauthentication_Enabled ;
      private int edtavBirthday_Enabled ;
      private int edtavAddress_Enabled ;
      private int edtavAddress2_Enabled ;
      private int edtavCity_Enabled ;
      private int edtavState_Enabled ;
      private int edtavPostcode_Enabled ;
      private int edtavTimezone_Enabled ;
      private int divGam_footerentry_Visible ;
      private int bttGam_footerentry_btncancel_Visible ;
      private int bttGam_footerentry_btnconfirm_Visible ;
      private int AV41GXV1 ;
      private int AV42GXV2 ;
      private int AV43GXV3 ;
      private int AV45GXV4 ;
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
      private string AV7AuthenticationTypeName ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string divTblheader_Internalname ;
      private string lblTxtuser_Internalname ;
      private string lblTxtuser_Caption ;
      private string lblTxtuser_Jsonclick ;
      private string lbllbl13_Internalname ;
      private string lbllbl13_Jsonclick ;
      private string divToolbar_inner_Internalname ;
      private string lblBtnedit_Internalname ;
      private string lblBtnedit_Jsonclick ;
      private string lblBtntotpauthenticator_Internalname ;
      private string lblBtntotpauthenticator_Caption ;
      private string lblBtntotpauthenticator_Jsonclick ;
      private string divGam_datacard_Internalname ;
      private string divGam_datacard_tablegeneraltitle_Internalname ;
      private string lblGam_datacard_tbtitlegeneral_Internalname ;
      private string lblGam_datacard_tbtitlegeneral_Jsonclick ;
      private string divGam_datacard_tabledatageneral_Internalname ;
      private string ClassString ;
      private string imgavImage_gximage ;
      private string StyleString ;
      private string sImgUrl ;
      private string imgavImage_Internalname ;
      private string cmbavAuthenticationtypename_Internalname ;
      private string TempTags ;
      private string cmbavAuthenticationtypename_Jsonclick ;
      private string edtavName_Internalname ;
      private string edtavName_Caption ;
      private string edtavName_Jsonclick ;
      private string bttBtnchangename_Internalname ;
      private string bttBtnchangename_Jsonclick ;
      private string edtavEmail_Internalname ;
      private string edtavEmail_Caption ;
      private string edtavEmail_Jsonclick ;
      private string bttBtnchangeemail_Internalname ;
      private string bttBtnchangeemail_Jsonclick ;
      private string edtavFirstname_Internalname ;
      private string edtavFirstname_Caption ;
      private string AV18FirstName ;
      private string edtavFirstname_Jsonclick ;
      private string edtavLastname_Internalname ;
      private string edtavLastname_Caption ;
      private string AV24LastName ;
      private string edtavLastname_Jsonclick ;
      private string edtavPhone_Internalname ;
      private string edtavPhone_Caption ;
      private string AV26Phone ;
      private string edtavPhone_Jsonclick ;
      private string chkavDontreceiveinformation_Internalname ;
      private string chkavEnabletwofactorauthentication_Internalname ;
      private string divLastauthcell_Internalname ;
      private string edtavDatelastauthentication_Internalname ;
      private string edtavDatelastauthentication_Jsonclick ;
      private string divRighttable_Internalname ;
      private string divStencil1_Internalname ;
      private string divStencil1_tablegeneraltitle_Internalname ;
      private string lblStencil1_tbtitlegeneral_Internalname ;
      private string lblStencil1_tbtitlegeneral_Jsonclick ;
      private string divStencil1_tabledatageneral_Internalname ;
      private string cmbavLanguage_Internalname ;
      private string cmbavLanguage_Jsonclick ;
      private string cmbavTheme_Internalname ;
      private string cmbavTheme_Jsonclick ;
      private string edtavBirthday_Internalname ;
      private string edtavBirthday_Caption ;
      private string edtavBirthday_Jsonclick ;
      private string cmbavGender_Internalname ;
      private string AV21Gender ;
      private string cmbavGender_Jsonclick ;
      private string edtavAddress_Internalname ;
      private string edtavAddress_Caption ;
      private string AV5Address ;
      private string edtavAddress_Jsonclick ;
      private string edtavAddress2_Internalname ;
      private string AV6Address2 ;
      private string edtavAddress2_Jsonclick ;
      private string edtavCity_Internalname ;
      private string edtavCity_Caption ;
      private string AV11City ;
      private string edtavCity_Jsonclick ;
      private string edtavState_Internalname ;
      private string edtavState_Caption ;
      private string AV28State ;
      private string edtavState_Jsonclick ;
      private string edtavPostcode_Internalname ;
      private string edtavPostcode_Caption ;
      private string AV38PostCode ;
      private string edtavPostcode_Jsonclick ;
      private string edtavTimezone_Internalname ;
      private string edtavTimezone_Caption ;
      private string AV29Timezone ;
      private string edtavTimezone_Jsonclick ;
      private string WebComp_Wcmessages_Component ;
      private string OldWcmessages ;
      private string divGam_footerentry_Internalname ;
      private string divGam_footerentry_tablebuttons_Internalname ;
      private string bttGam_footerentry_btncancel_Internalname ;
      private string bttGam_footerentry_btncancel_Jsonclick ;
      private string bttGam_footerentry_btnconfirm_Internalname ;
      private string bttGam_footerentry_btnconfirm_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string hsh ;
      private DateTime AV12DateLastAuthentication ;
      private DateTime AV10Birthday ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool AV22Image_IsBlob ;
      private bool AV13DontReceiveInformation ;
      private bool AV15EnableTwoFactorAuthentication ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool bDynCreated_Wcmessages ;
      private string AV25Name ;
      private string AV14EMail ;
      private string AV30URLProfile ;
      private string AV44Image_GXI ;
      private string AV23Language ;
      private string AV32Theme ;
      private string AV22Image ;
      private GXWebComponent WebComp_Wcmessages ;
      private GXProperties forbiddenHiddens ;
      private GXWebForm Form ;
      private GXWindow AV31Window ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_Gx_mode ;
      private GXCombobox cmbavAuthenticationtypename ;
      private GXCheckbox chkavDontreceiveinformation ;
      private GXCheckbox chkavEnabletwofactorauthentication ;
      private GXCombobox cmbavLanguage ;
      private GXCombobox cmbavTheme ;
      private GXCombobox cmbavGender ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV20gamUser ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV35GAMApplication ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage> AV34GAMLanguages ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage AV33GAMLanguage ;
      private IDataStoreProvider pr_default ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV17GAMErrorCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV16GAMError ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV19GAMRepository ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple> AV8AuthenticationTypes ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple AV9AuthenticationTypesIns ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class myaccount__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class myaccount__default : DataStoreHelperBase, IDataStoreHelper
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
