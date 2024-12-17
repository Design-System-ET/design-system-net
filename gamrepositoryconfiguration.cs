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
   public class gamrepositoryconfiguration : GXDataArea
   {
      public gamrepositoryconfiguration( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamrepositoryconfiguration( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( long aP0_Id )
      {
         this.AV45Id = aP0_Id;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         cmbavEnabletracing = new GXCombobox();
         cmbavDefaultauthtypename = new GXCombobox();
         cmbavDefaultroleid = new GXCombobox();
         cmbavDefaultsecuritypolicyid = new GXCombobox();
         chkavAllowoauthaccess = new GXCheckbox();
         cmbavLogoutbehavior = new GXCombobox();
         chkavEnableworkingasgammanagerrepo = new GXCheckbox();
         cmbavUseridentification = new GXCombobox();
         chkavUseremailisunique = new GXCheckbox();
         cmbavUseractivationmethod = new GXCombobox();
         cmbavUserremembermetype = new GXCombobox();
         chkavRequiredemail = new GXCheckbox();
         chkavRequiredpassword = new GXCheckbox();
         chkavRequiredfirstname = new GXCheckbox();
         chkavRequiredlastname = new GXCheckbox();
         chkavRequiredbirthday = new GXCheckbox();
         chkavRequiredgender = new GXCheckbox();
         cmbavGeneratesessionstatistics = new GXCombobox();
         chkavGiveanonymoussession = new GXCheckbox();
         chkavSessionexpiresonipchange = new GXCheckbox();
         chkavIntsecbydomainenable = new GXCheckbox();
         cmbavIntsecbydomainmode = new GXCombobox();
         chkavEmailserversecure = new GXCheckbox();
         chkavEmailserverusesauthentication = new GXCheckbox();
         chkavEmailserver_sendemailwhenuseractivateaccount = new GXCheckbox();
         chkavEmailserver_sendemailwhenuserchangepassword = new GXCheckbox();
         chkavEmailserver_sendemailwhenuserchangeemail = new GXCheckbox();
         chkavEmailserver_sendemailforrecoverypassword = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "Id");
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
               gxfirstwebparm = GetFirstPar( "Id");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "Id");
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
            return "gamrepositoryconfiguration_Execute" ;
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
         PA1X2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START1X2( ) ;
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
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/TabRender.js", "", false, true);
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
         GXEncryptionTmp = "gamrepositoryconfiguration.aspx"+UrlEncode(StringUtil.LTrimStr(AV45Id,12,0));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamrepositoryconfiguration.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "gxhash_vREPOID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vSECURITYADMINISTRATOREMAIL", AV71SecurityAdministratorEmail);
         GxWebStd.gx_hidden_field( context, "gxhash_vSECURITYADMINISTRATOREMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV71SecurityAdministratorEmail, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vCANREGISTERUSERS", AV9CanRegisterUsers);
         GxWebStd.gx_hidden_field( context, "gxhash_vCANREGISTERUSERS", GetSecureSignedToken( "", AV9CanRegisterUsers, context));
         GxWebStd.gx_hidden_field( context, "vID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV45Id), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45Id), "ZZZZZZZZZZZ9"), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"GAMRepositoryConfiguration");
         forbiddenHiddens.Add("RepoId", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("gamrepositoryconfiguration:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vSECURITYADMINISTRATOREMAIL", AV71SecurityAdministratorEmail);
         GxWebStd.gx_hidden_field( context, "gxhash_vSECURITYADMINISTRATOREMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV71SecurityAdministratorEmail, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vCANREGISTERUSERS", AV9CanRegisterUsers);
         GxWebStd.gx_hidden_field( context, "gxhash_vCANREGISTERUSERS", GetSecureSignedToken( "", AV9CanRegisterUsers, context));
         GxWebStd.gx_hidden_field( context, "vID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV45Id), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45Id), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS_Pagecount", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gxuitabspanel_tabs_Pagecount), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS_Class", StringUtil.RTrim( Gxuitabspanel_tabs_Class));
         GxWebStd.gx_hidden_field( context, "GXUITABSPANEL_TABS_Historymanagement", StringUtil.BoolToStr( Gxuitabspanel_tabs_Historymanagement));
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
            WE1X2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT1X2( ) ;
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
         GXEncryptionTmp = "gamrepositoryconfiguration.aspx"+UrlEncode(StringUtil.LTrimStr(AV45Id,12,0));
         return formatLink("gamrepositoryconfiguration.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "GAMRepositoryConfiguration" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Repository configuration ", "") ;
      }

      protected void WB1X0( )
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
            /* User Defined Control */
            ucGxuitabspanel_tabs.SetProperty("PageCount", Gxuitabspanel_tabs_Pagecount);
            ucGxuitabspanel_tabs.SetProperty("Class", Gxuitabspanel_tabs_Class);
            ucGxuitabspanel_tabs.SetProperty("HistoryManagement", Gxuitabspanel_tabs_Historymanagement);
            ucGxuitabspanel_tabs.Render(context, "tab", Gxuitabspanel_tabs_Internalname, "GXUITABSPANEL_TABSContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title1"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblGeneral_title_Internalname, context.GetMessage( "WWP_GAM_General", ""), "", "", lblGeneral_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMRepositoryConfiguration.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "General") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel1"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRepoid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRepoid_Internalname, context.GetMessage( "WWP_GAM_Id", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavRepoid_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV59RepoId), 12, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavRepoid_Enabled!=0) ? context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,22);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRepoid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavRepoid_Enabled, 0, "text", "1", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMKeyNumLong", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGuid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGuid_Internalname, context.GetMessage( "WWP_GAM_GUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGuid_Internalname, StringUtil.RTrim( AV44GUID), StringUtil.RTrim( context.localUtil.Format( AV44GUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,26);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGuid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavGuid_Enabled, 0, "text", "", 32, "chr", 1, "row", 32, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavNamespace_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavNamespace_Internalname, context.GetMessage( "WWP_GAM_Namespace", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNamespace_Internalname, StringUtil.RTrim( AV58NameSpace), StringUtil.RTrim( context.localUtil.Format( AV58NameSpace, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,31);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNamespace_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNamespace_Enabled, 0, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMRepositoryNameSpace", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavName_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavName_Internalname, context.GetMessage( "WWP_GAM_Name", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavName_Internalname, StringUtil.RTrim( AV57Name), StringUtil.RTrim( context.localUtil.Format( AV57Name, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,35);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavName_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavName_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDsc_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDsc_Internalname, context.GetMessage( "WWP_GAM_Description", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDsc_Internalname, StringUtil.RTrim( AV13Dsc), StringUtil.RTrim( context.localUtil.Format( AV13Dsc, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,40);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDsc_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDsc_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavEnabletracing_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavEnabletracing_Internalname, context.GetMessage( "WWP_GAM_EnableTracing", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavEnabletracing, cmbavEnabletracing_Internalname, StringUtil.RTrim( AV33EnableTracing), 1, cmbavEnabletracing_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavEnabletracing.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,44);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavEnabletracing.CurrentValue = StringUtil.RTrim( AV33EnableTracing);
            AssignProp("", false, cmbavEnabletracing_Internalname, "Values", (string)(cmbavEnabletracing.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divAuthenticationmasterrepository_cell_Internalname, 1, 0, "px", 0, "px", divAuthenticationmasterrepository_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavAuthenticationmasterrepository_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAuthenticationmasterrepository_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAuthenticationmasterrepository_Internalname, context.GetMessage( "WWP_GAM_AuthenticationMasterRepository", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAuthenticationmasterrepository_Internalname, StringUtil.RTrim( AV6AuthenticationMasterRepository), StringUtil.RTrim( context.localUtil.Format( AV6AuthenticationMasterRepository, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,49);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAuthenticationmasterrepository_Jsonclick, 0, "Attribute", "", "", "", "", edtavAuthenticationmasterrepository_Visible, edtavAuthenticationmasterrepository_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divDefaultauthtypename_cell_Internalname, 1, 0, "px", 0, "px", divDefaultauthtypename_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavDefaultauthtypename.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavDefaultauthtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavDefaultauthtypename_Internalname, context.GetMessage( "WWP_GAM_DefaultAuthenticationtype", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 53,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavDefaultauthtypename, cmbavDefaultauthtypename_Internalname, StringUtil.RTrim( AV10DefaultAuthTypeName), 1, cmbavDefaultauthtypename_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", cmbavDefaultauthtypename.Visible, cmbavDefaultauthtypename.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,53);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavDefaultauthtypename.CurrentValue = StringUtil.RTrim( AV10DefaultAuthTypeName);
            AssignProp("", false, cmbavDefaultauthtypename_Internalname, "Values", (string)(cmbavDefaultauthtypename.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divDefaultroleid_cell_Internalname, 1, 0, "px", 0, "px", divDefaultroleid_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavDefaultroleid.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavDefaultroleid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavDefaultroleid_Internalname, context.GetMessage( "WWP_GAM_Defaultrole", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 58,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavDefaultroleid, cmbavDefaultroleid_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV11DefaultRoleId), 12, 0)), 1, cmbavDefaultroleid_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", cmbavDefaultroleid.Visible, cmbavDefaultroleid.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,58);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavDefaultroleid.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV11DefaultRoleId), 12, 0));
            AssignProp("", false, cmbavDefaultroleid_Internalname, "Values", (string)(cmbavDefaultroleid.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divDefaultsecuritypolicyid_cell_Internalname, 1, 0, "px", 0, "px", divDefaultsecuritypolicyid_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavDefaultsecuritypolicyid.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavDefaultsecuritypolicyid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavDefaultsecuritypolicyid_Internalname, context.GetMessage( "WWP_GAM_DefaultSecurityPolicy", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 62,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavDefaultsecuritypolicyid, cmbavDefaultsecuritypolicyid_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV12DefaultSecurityPolicyId), 9, 0)), 1, cmbavDefaultsecuritypolicyid_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "int", "", cmbavDefaultsecuritypolicyid.Visible, cmbavDefaultsecuritypolicyid.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,62);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavDefaultsecuritypolicyid.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV12DefaultSecurityPolicyId), 9, 0));
            AssignProp("", false, cmbavDefaultsecuritypolicyid_Internalname, "Values", (string)(cmbavDefaultsecuritypolicyid.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAllowoauthaccess_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAllowoauthaccess_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 67,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAllowoauthaccess_Internalname, StringUtil.BoolToStr( AV5AllowOauthAccess), "", " ", 1, chkavAllowoauthaccess.Enabled, "true", context.GetMessage( "WWP_GAM_RepositoryAllowOauthAcess", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(67, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,67);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavLogoutbehavior_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavLogoutbehavior_Internalname, context.GetMessage( "WWP_GAM_GAMRemoteLogoutbehavior", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 71,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavLogoutbehavior, cmbavLogoutbehavior_Internalname, StringUtil.RTrim( AV55LogoutBehavior), 1, cmbavLogoutbehavior_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavLogoutbehavior.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,71);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavLogoutbehavior.CurrentValue = StringUtil.RTrim( AV55LogoutBehavior);
            AssignProp("", false, cmbavLogoutbehavior_Internalname, "Values", (string)(cmbavLogoutbehavior.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEnableworkingasgammanagerrepo_cell_Internalname, 1, 0, "px", 0, "px", divEnableworkingasgammanagerrepo_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavEnableworkingasgammanagerrepo.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEnableworkingasgammanagerrepo_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEnableworkingasgammanagerrepo_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 76,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEnableworkingasgammanagerrepo_Internalname, StringUtil.BoolToStr( AV35EnableWorkingAsGAMManagerRepo), "", " ", chkavEnableworkingasgammanagerrepo.Visible, chkavEnableworkingasgammanagerrepo.Enabled, "true", context.GetMessage( "WWP_GAM_EnableWorkingAsGAMManagerRepo", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(76, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,76);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title2"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblUsers_title_Internalname, context.GetMessage( "WWP_GAM_Users", ""), "", "", lblUsers_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMRepositoryConfiguration.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Users") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel2"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUseridentification_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavUseridentification_Internalname, context.GetMessage( "WWP_GAM_UserIdentification", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 86,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUseridentification, cmbavUseridentification_Internalname, StringUtil.RTrim( AV80UserIdentification), 1, cmbavUseridentification_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavUseridentification.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,86);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavUseridentification.CurrentValue = StringUtil.RTrim( AV80UserIdentification);
            AssignProp("", false, cmbavUseridentification_Internalname, "Values", (string)(cmbavUseridentification.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUseremailisunique_cell_Internalname, 1, 0, "px", 0, "px", divUseremailisunique_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavUseremailisunique.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavUseremailisunique_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavUseremailisunique_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 90,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavUseremailisunique_Internalname, StringUtil.BoolToStr( AV79UserEmailisUnique), "", " ", chkavUseremailisunique.Visible, chkavUseremailisunique.Enabled, "true", context.GetMessage( "WWP_GAM_UserEmailIsUnique", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(90, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,90);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUseractivationmethod_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavUseractivationmethod_Internalname, context.GetMessage( "WWP_GAM_Useractivationmethod", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 95,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUseractivationmethod, cmbavUseractivationmethod_Internalname, StringUtil.RTrim( AV77UserActivationMethod), 1, cmbavUseractivationmethod_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavUseractivationmethod.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,95);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavUseractivationmethod.CurrentValue = StringUtil.RTrim( AV77UserActivationMethod);
            AssignProp("", false, cmbavUseractivationmethod_Internalname, "Values", (string)(cmbavUseractivationmethod.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserautomaticactivationtimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserautomaticactivationtimeout_Internalname, context.GetMessage( "WWP_GAM_Userautomaticactivationtimeouthours", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 99,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserautomaticactivationtimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV78UserAutomaticActivationTimeout), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavUserautomaticactivationtimeout_Enabled!=0) ? context.localUtil.Format( (decimal)(AV78UserAutomaticActivationTimeout), "ZZZ9") : context.localUtil.Format( (decimal)(AV78UserAutomaticActivationTimeout), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,99);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserautomaticactivationtimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserautomaticactivationtimeout_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserrecoverypasswordkeytimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserrecoverypasswordkeytimeout_Internalname, context.GetMessage( "WWP_GAM_UserRecoveryPasswordTimeout", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 104,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserrecoverypasswordkeytimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV81UserRecoveryPasswordKeyTimeOut), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavUserrecoverypasswordkeytimeout_Enabled!=0) ? context.localUtil.Format( (decimal)(AV81UserRecoveryPasswordKeyTimeOut), "ZZZ9") : context.localUtil.Format( (decimal)(AV81UserRecoveryPasswordKeyTimeOut), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,104);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserrecoverypasswordkeytimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserrecoverypasswordkeytimeout_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserrecoverypasswordkeydailymaximum_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserrecoverypasswordkeydailymaximum_Internalname, context.GetMessage( "WWP_GAM_UserRecoveryPasswordKeyDailyMaximum", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 108,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserrecoverypasswordkeydailymaximum_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV82UserRecoveryPasswordKeyDailyMaximum), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavUserrecoverypasswordkeydailymaximum_Enabled!=0) ? context.localUtil.Format( (decimal)(AV82UserRecoveryPasswordKeyDailyMaximum), "ZZZ9") : context.localUtil.Format( (decimal)(AV82UserRecoveryPasswordKeyDailyMaximum), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,108);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserrecoverypasswordkeydailymaximum_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserrecoverypasswordkeydailymaximum_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserrecoverypasswordkeymonthlymaximum_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserrecoverypasswordkeymonthlymaximum_Internalname, context.GetMessage( "WWP_GAM_UserRecoveryPasswordKeyMonthlyMaximum", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 113,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserrecoverypasswordkeymonthlymaximum_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV83UserRecoveryPasswordKeyMonthlyMaximum), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavUserrecoverypasswordkeymonthlymaximum_Enabled!=0) ? context.localUtil.Format( (decimal)(AV83UserRecoveryPasswordKeyMonthlyMaximum), "ZZZ9") : context.localUtil.Format( (decimal)(AV83UserRecoveryPasswordKeyMonthlyMaximum), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,113);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserrecoverypasswordkeymonthlymaximum_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserrecoverypasswordkeymonthlymaximum_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavLoginattemptstolockuser_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavLoginattemptstolockuser_Internalname, context.GetMessage( "WWP_GAM_LoginRetriesLockUser", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 117,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavLoginattemptstolockuser_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV54LoginAttemptsToLockUser), 2, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavLoginattemptstolockuser_Enabled!=0) ? context.localUtil.Format( (decimal)(AV54LoginAttemptsToLockUser), "Z9") : context.localUtil.Format( (decimal)(AV54LoginAttemptsToLockUser), "Z9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,117);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavLoginattemptstolockuser_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavLoginattemptstolockuser_Enabled, 0, "text", "1", 2, "chr", 1, "row", 2, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGamunblockusertimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGamunblockusertimeout_Internalname, context.GetMessage( "WWP_GAM_UnblockUserTimeout", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 122,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGamunblockusertimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV41GAMUnblockUserTimeout), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavGamunblockusertimeout_Enabled!=0) ? context.localUtil.Format( (decimal)(AV41GAMUnblockUserTimeout), "ZZZ9") : context.localUtil.Format( (decimal)(AV41GAMUnblockUserTimeout), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,122);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGamunblockusertimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavGamunblockusertimeout_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavUserremembermetype_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavUserremembermetype_Internalname, context.GetMessage( "WWP_GAM_UserRememberMeType", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 126,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavUserremembermetype, cmbavUserremembermetype_Internalname, StringUtil.RTrim( AV85UserRememberMeType), 1, cmbavUserremembermetype_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavUserremembermetype.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,126);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavUserremembermetype.CurrentValue = StringUtil.RTrim( AV85UserRememberMeType);
            AssignProp("", false, cmbavUserremembermetype_Internalname, "Values", (string)(cmbavUserremembermetype.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserremembermetimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserremembermetimeout_Internalname, context.GetMessage( "WWP_GAM_UserRememberMeTimeoutDays", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 131,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserremembermetimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV84UserRememberMeTimeOut), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavUserremembermetimeout_Enabled!=0) ? context.localUtil.Format( (decimal)(AV84UserRememberMeTimeOut), "ZZZ9") : context.localUtil.Format( (decimal)(AV84UserRememberMeTimeOut), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,131);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserremembermetimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserremembermetimeout_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTotpsecretkeylength_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTotpsecretkeylength_Internalname, context.GetMessage( "WWP_GAM_TOTPSecretKeyLength", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 135,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTotpsecretkeylength_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV50TOTPSecretKeyLength), 12, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavTotpsecretkeylength_Enabled!=0) ? context.localUtil.Format( (decimal)(AV50TOTPSecretKeyLength), "ZZZZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV50TOTPSecretKeyLength), "ZZZZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,135);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTotpsecretkeylength_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavTotpsecretkeylength_Enabled, 0, "text", "1", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMKeyNumLong", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divRequiredemail_cell_Internalname, 1, 0, "px", 0, "px", divRequiredemail_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavRequiredemail.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavRequiredemail_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRequiredemail_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 140,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRequiredemail_Internalname, StringUtil.BoolToStr( AV64RequiredEmail), "", " ", chkavRequiredemail.Visible, chkavRequiredemail.Enabled, "true", context.GetMessage( "WWP_GAM_Requiredemail", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(140, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,140);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavRequiredpassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRequiredpassword_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 144,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRequiredpassword_Internalname, StringUtil.BoolToStr( AV68RequiredPassword), "", " ", 1, chkavRequiredpassword.Enabled, "true", context.GetMessage( "WWP_GAM_Requiredpassword", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(144, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,144);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavRequiredfirstname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRequiredfirstname_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 149,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRequiredfirstname_Internalname, StringUtil.BoolToStr( AV65RequiredFirstName), "", " ", 1, chkavRequiredfirstname.Enabled, "true", context.GetMessage( "WWP_GAM_Requiredfirstname", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(149, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,149);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavRequiredlastname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRequiredlastname_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 153,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRequiredlastname_Internalname, StringUtil.BoolToStr( AV67RequiredLastName), "", " ", 1, chkavRequiredlastname.Enabled, "true", context.GetMessage( "WWP_GAM_Requiredlastname", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(153, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,153);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavRequiredbirthday_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRequiredbirthday_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 158,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRequiredbirthday_Internalname, StringUtil.BoolToStr( AV63RequiredBirthday), "", " ", 1, chkavRequiredbirthday.Enabled, "true", context.GetMessage( "WWP_GAM_RequiredBirthday", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(158, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,158);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavRequiredgender_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRequiredgender_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 162,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRequiredgender_Internalname, StringUtil.BoolToStr( AV66RequiredGender), "", " ", 1, chkavRequiredgender.Enabled, "true", context.GetMessage( "WWP_GAM_RequiredGender", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(162, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,162);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"title3"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblSession_title_Internalname, context.GetMessage( "WWP_GAM_Session", ""), "", "", lblSession_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMRepositoryConfiguration.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Session") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel3"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavGeneratesessionstatistics_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavGeneratesessionstatistics_Internalname, context.GetMessage( "WWP_GAM_Generatesessionstatistics", ""), " AttributeRealWidthLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 172,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavGeneratesessionstatistics, cmbavGeneratesessionstatistics_Internalname, StringUtil.RTrim( AV42GenerateSessionStatistics), 1, cmbavGeneratesessionstatistics_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavGeneratesessionstatistics.Enabled, 0, 0, 0, "em", 0, "", "", "AttributeRealWidth", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,172);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavGeneratesessionstatistics.CurrentValue = StringUtil.RTrim( AV42GenerateSessionStatistics);
            AssignProp("", false, cmbavGeneratesessionstatistics_Internalname, "Values", (string)(cmbavGeneratesessionstatistics.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUsersessioncachetimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUsersessioncachetimeout_Internalname, context.GetMessage( "WWP_GAM_UserSessionCacheTimeoutSeconds", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 177,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUsersessioncachetimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV86UserSessionCacheTimeout), 9, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavUsersessioncachetimeout_Enabled!=0) ? context.localUtil.Format( (decimal)(AV86UserSessionCacheTimeout), "ZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV86UserSessionCacheTimeout), "ZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,177);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUsersessioncachetimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUsersessioncachetimeout_Enabled, 0, "text", "1", 9, "chr", 1, "row", 9, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavGiveanonymoussession_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavGiveanonymoussession_Internalname, "  ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 182,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavGiveanonymoussession_Internalname, StringUtil.BoolToStr( AV43GiveAnonymousSession), "", "  ", 1, chkavGiveanonymoussession.Enabled, "true", context.GetMessage( "WWP_GAM_GiveWebAnonymousSession", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(182, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,182);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSessionexpiresonipchange_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSessionexpiresonipchange_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 187,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSessionexpiresonipchange_Internalname, StringUtil.BoolToStr( AV76SessionExpiresOnIPChange), "", " ", 1, chkavSessionexpiresonipchange.Enabled, "true", context.GetMessage( "WWP_GAM_GAMsessionexpiresonIPchange", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(187, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,187);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavLoginattemptstolocksession_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavLoginattemptstolocksession_Internalname, context.GetMessage( "WWP_GAM_Loginretriestolocksession", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 192,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavLoginattemptstolocksession_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV53LoginAttemptsToLockSession), 2, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavLoginattemptstolocksession_Enabled!=0) ? context.localUtil.Format( (decimal)(AV53LoginAttemptsToLockSession), "Z9") : context.localUtil.Format( (decimal)(AV53LoginAttemptsToLockSession), "Z9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,192);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavLoginattemptstolocksession_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavLoginattemptstolocksession_Enabled, 0, "text", "1", 2, "chr", 1, "row", 2, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavMinimumamountcharactersinlogin_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMinimumamountcharactersinlogin_Internalname, context.GetMessage( "WWP_GAM_Minimumamountofcharactersinlogin", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 197,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMinimumamountcharactersinlogin_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV56MinimumAmountCharactersInLogin), 2, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavMinimumamountcharactersinlogin_Enabled!=0) ? context.localUtil.Format( (decimal)(AV56MinimumAmountCharactersInLogin), "Z9") : context.localUtil.Format( (decimal)(AV56MinimumAmountCharactersInLogin), "Z9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,197);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMinimumamountcharactersinlogin_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavMinimumamountcharactersinlogin_Enabled, 0, "text", "1", 2, "chr", 1, "row", 2, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavRepositorycachetimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavRepositorycachetimeout_Internalname, context.GetMessage( "WWP_GAM_CacheTimeoutMinutes", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 202,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavRepositorycachetimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV61RepositoryCacheTimeout), 9, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavRepositorycachetimeout_Enabled!=0) ? context.localUtil.Format( (decimal)(AV61RepositoryCacheTimeout), "ZZZZZZZZ9") : context.localUtil.Format( (decimal)(AV61RepositoryCacheTimeout), "ZZZZZZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,202);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavRepositorycachetimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavRepositorycachetimeout_Enabled, 0, "text", "1", 9, "chr", 1, "row", 9, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavIntsecbydomainenable_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavIntsecbydomainenable_Internalname, context.GetMessage( "WWP_GAM_EnableIntegratedSecurityByDomain", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 207,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavIntsecbydomainenable_Internalname, StringUtil.BoolToStr( AV51IntSecByDomainEnable), "", context.GetMessage( "WWP_GAM_EnableIntegratedSecurityByDomain", ""), 1, chkavIntsecbydomainenable.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,207);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblintsecbydomain_Internalname, divTblintsecbydomain_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavIntsecbydomainmode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavIntsecbydomainmode_Internalname, context.GetMessage( "WWP_GAM_IntegratedSecurityByDomainMode", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 215,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavIntsecbydomainmode, cmbavIntsecbydomainmode_Internalname, StringUtil.RTrim( AV49IntSecByDomainMode), 1, cmbavIntsecbydomainmode_Jsonclick, 0, "'"+""+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavIntsecbydomainmode.Enabled, 0, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,215);\"", "", true, 0, "HLP_GAMRepositoryConfiguration.htm");
            cmbavIntsecbydomainmode.CurrentValue = StringUtil.RTrim( AV49IntSecByDomainMode);
            AssignProp("", false, cmbavIntsecbydomainmode_Internalname, "Values", (string)(cmbavIntsecbydomainmode.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavIntsecbydomainjwtsecret_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavIntsecbydomainjwtsecret_Internalname, context.GetMessage( "WWP_GAM_IntegratedSecurityByDomainSecret", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 220,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavIntsecbydomainjwtsecret_Internalname, AV48IntSecByDomainJWTSecret, StringUtil.RTrim( context.localUtil.Format( AV48IntSecByDomainJWTSecret, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,220);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavIntsecbydomainjwtsecret_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavIntsecbydomainjwtsecret_Enabled, 0, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavIntsecbydomainencryptionkey_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavIntsecbydomainencryptionkey_Internalname, context.GetMessage( "WWP_GAM_IntegratedSecurityByDomainEncriptionKey", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 225,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavIntsecbydomainencryptionkey_Internalname, AV47IntSecByDomainEncryptionKey, StringUtil.RTrim( context.localUtil.Format( AV47IntSecByDomainEncryptionKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,225);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavIntsecbydomainencryptionkey_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavIntsecbydomainencryptionkey_Enabled, 0, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
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
            GxWebStd.gx_label_ctrl( context, lblTabemail_title_Internalname, context.GetMessage( "WWP_GAM_Email", ""), "", "", lblTabemail_title_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMRepositoryConfiguration.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "tabEmail") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"GXUITABSPANEL_TABSContainer"+"panel4"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableattributes_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserverhost_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserverhost_Internalname, context.GetMessage( "WWP_GAM_ServerHost", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 235,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserverhost_Internalname, StringUtil.RTrim( AV28EmailServerHost), StringUtil.RTrim( context.localUtil.Format( AV28EmailServerHost, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,235);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserverhost_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEmailserverhost_Enabled, 0, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserverport_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserverport_Internalname, context.GetMessage( "WWP_GAM_Serverport", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 239,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserverport_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV29EmailServerPort), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavEmailserverport_Enabled!=0) ? context.localUtil.Format( (decimal)(AV29EmailServerPort), "ZZZ9") : context.localUtil.Format( (decimal)(AV29EmailServerPort), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,239);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserverport_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEmailserverport_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailservertimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailservertimeout_Internalname, context.GetMessage( "WWP_GAM_TimeoutSeconds", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 244,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailservertimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV31EmailServerTimeout), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavEmailservertimeout_Enabled!=0) ? context.localUtil.Format( (decimal)(AV31EmailServerTimeout), "ZZZ9") : context.localUtil.Format( (decimal)(AV31EmailServerTimeout), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,244);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailservertimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavEmailservertimeout_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEmailserversecure_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEmailserversecure_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 248,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEmailserversecure_Internalname, StringUtil.BoolToStr( AV30EmailServerSecure), "", " ", 1, chkavEmailserversecure.Enabled, "true", context.GetMessage( "WWP_GAM_Secure", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(248, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,248);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavServersenderaddress_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavServersenderaddress_Internalname, context.GetMessage( "WWP_GAM_SenderEmailAddress", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 253,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavServersenderaddress_Internalname, AV74ServerSenderAddress, StringUtil.RTrim( context.localUtil.Format( AV74ServerSenderAddress, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,253);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavServersenderaddress_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavServersenderaddress_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMEMail", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavServersendername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavServersendername_Internalname, context.GetMessage( "WWP_GAM_SenderName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 257,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavServersendername_Internalname, StringUtil.RTrim( AV75ServerSenderName), StringUtil.RTrim( context.localUtil.Format( AV75ServerSenderName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,257);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavServersendername_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavServersendername_Enabled, 0, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEmailserverusesauthentication_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEmailserverusesauthentication_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 262,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEmailserverusesauthentication_Internalname, StringUtil.BoolToStr( AV32EmailServerUsesAuthentication), "", " ", 1, chkavEmailserverusesauthentication.Enabled, "true", context.GetMessage( "WWP_GAM_ServerRequireAudthentication", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,262);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserverauthenticationusername_cell_Internalname, 1, 0, "px", 0, "px", divEmailserverauthenticationusername_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserverauthenticationusername_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserverauthenticationusername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserverauthenticationusername_Internalname, context.GetMessage( "WWP_GAM_UserName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 267,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserverauthenticationusername_Internalname, StringUtil.RTrim( AV26EmailServerAuthenticationUsername), StringUtil.RTrim( context.localUtil.Format( AV26EmailServerAuthenticationUsername, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,267);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserverauthenticationusername_Jsonclick, 0, "Attribute", "", "", "", "", edtavEmailserverauthenticationusername_Visible, edtavEmailserverauthenticationusername_Enabled, 0, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserverauthenticationuserpassword_cell_Internalname, 1, 0, "px", 0, "px", divEmailserverauthenticationuserpassword_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserverauthenticationuserpassword_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserverauthenticationuserpassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserverauthenticationuserpassword_Internalname, context.GetMessage( "WWP_GAM_Password", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 271,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserverauthenticationuserpassword_Internalname, StringUtil.RTrim( AV27EmailServerAuthenticationUserPassword), StringUtil.RTrim( context.localUtil.Format( AV27EmailServerAuthenticationUserPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,271);\""+" "+"data-gx-password-reveal"+" "+"idenableshowpasswordhint=\"True\""+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserverauthenticationuserpassword_Jsonclick, 0, "Attribute", "", "", "", "", edtavEmailserverauthenticationuserpassword_Visible, edtavEmailserverauthenticationuserpassword_Enabled, 0, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEmailserver_sendemailwhenuseractivateaccount_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEmailserver_sendemailwhenuseractivateaccount_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 276,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEmailserver_sendemailwhenuseractivateaccount_Internalname, StringUtil.BoolToStr( AV23EmailServer_SendEmailWhenUserActivateAccount), "", " ", 1, chkavEmailserver_sendemailwhenuseractivateaccount.Enabled, "true", context.GetMessage( "WWP_GAM_SendEmailUserActivateAccount", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,276);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailsubjectwhenuseractivateaccount_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname, context.GetMessage( "WWP_GAM_EmailSubjectActivateUserAccount", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 280,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname, StringUtil.RTrim( AV19EmailServer_EmailSubjectWhenUserActivateAccount), StringUtil.RTrim( context.localUtil.Format( AV19EmailServer_EmailSubjectWhenUserActivateAccount, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,280);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserver_emailsubjectwhenuseractivateaccount_Jsonclick, 0, "Attribute", "", "", "", "", edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible, edtavEmailserver_emailsubjectwhenuseractivateaccount_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailbodywhenuseractivateaccount_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailbodywhenuseractivateaccount_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailbodywhenuseractivateaccount_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailbodywhenuseractivateaccount_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailbodywhenuseractivateaccount_Internalname, context.GetMessage( "WWP_GAM_EmailBodyActivateUserAccount", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 285,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavEmailserver_emailbodywhenuseractivateaccount_Internalname, AV15EmailServer_EmailBodyWhenUserActivateAccount, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,285);\"", 0, edtavEmailserver_emailbodywhenuseractivateaccount_Visible, edtavEmailserver_emailbodywhenuseractivateaccount_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "800", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEmailserver_sendemailwhenuserchangepassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEmailserver_sendemailwhenuserchangepassword_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 289,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEmailserver_sendemailwhenuserchangepassword_Internalname, StringUtil.BoolToStr( AV25EmailServer_SendEmailWhenUserChangePassword), "", " ", 1, chkavEmailserver_sendemailwhenuserchangepassword.Enabled, "true", context.GetMessage( "WWP_GAM_SendEmailUserChangePassword", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,289);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailsubjectwhenuserchangepassword_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailsubjectwhenuserchangepassword_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailsubjectwhenuserchangepassword_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname, context.GetMessage( "WWP_GAM_EmailSubjectChangeUserPassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 294,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname, StringUtil.RTrim( AV21EmailServer_EmailSubjectWhenUserChangePassword), StringUtil.RTrim( context.localUtil.Format( AV21EmailServer_EmailSubjectWhenUserChangePassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,294);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserver_emailsubjectwhenuserchangepassword_Jsonclick, 0, "Attribute", "", "", "", "", edtavEmailserver_emailsubjectwhenuserchangepassword_Visible, edtavEmailserver_emailsubjectwhenuserchangepassword_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailbodywhenuserchangepassword_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailbodywhenuserchangepassword_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailbodywhenuserchangepassword_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailbodywhenuserchangepassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailbodywhenuserchangepassword_Internalname, context.GetMessage( "WWP_GAM_EmailBodyChangeUserPassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 298,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavEmailserver_emailbodywhenuserchangepassword_Internalname, AV17EmailServer_EmailBodyWhenUserChangePassword, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,298);\"", 0, edtavEmailserver_emailbodywhenuserchangepassword_Visible, edtavEmailserver_emailbodywhenuserchangepassword_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "800", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEmailserver_sendemailwhenuserchangeemail_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEmailserver_sendemailwhenuserchangeemail_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 303,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEmailserver_sendemailwhenuserchangeemail_Internalname, StringUtil.BoolToStr( AV24EmailServer_SendEmailWhenUserChangeEmail), "", " ", 1, chkavEmailserver_sendemailwhenuserchangeemail.Enabled, "true", context.GetMessage( "WWP_GAM_SendEmailUserChangeEmailUsername", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,303);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailsubjectwhenuserchangeemail_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailsubjectwhenuserchangeemail_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailsubjectwhenuserchangeemail_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname, context.GetMessage( "WWP_GAM_EmailSubjectChangeEmailUsername", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 307,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname, StringUtil.RTrim( AV20EmailServer_EmailSubjectWhenUserChangeEmail), StringUtil.RTrim( context.localUtil.Format( AV20EmailServer_EmailSubjectWhenUserChangeEmail, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,307);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserver_emailsubjectwhenuserchangeemail_Jsonclick, 0, "Attribute", "", "", "", "", edtavEmailserver_emailsubjectwhenuserchangeemail_Visible, edtavEmailserver_emailsubjectwhenuserchangeemail_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailbodywhenuserchangeemail_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailbodywhenuserchangeemail_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailbodywhenuserchangeemail_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailbodywhenuserchangeemail_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailbodywhenuserchangeemail_Internalname, context.GetMessage( "WWP_GAM_EmailBodyChangeEmailUsername", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 312,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavEmailserver_emailbodywhenuserchangeemail_Internalname, AV16EmailServer_EmailBodyWhenUserChangeEmail, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,312);\"", 0, edtavEmailserver_emailbodywhenuserchangeemail_Visible, edtavEmailserver_emailbodywhenuserchangeemail_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "800", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavEmailserver_sendemailforrecoverypassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavEmailserver_sendemailforrecoverypassword_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 316,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavEmailserver_sendemailforrecoverypassword_Internalname, StringUtil.BoolToStr( AV22EmailServer_SendEmailForRecoveryPassword), "", " ", 1, chkavEmailserver_sendemailforrecoverypassword.Enabled, "true", context.GetMessage( "WWP_GAM_SendEmailRecoveryPassword", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,316);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailsubjectforrecoverypassword_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailsubjectforrecoverypassword_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailsubjectforrecoverypassword_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailsubjectforrecoverypassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailsubjectforrecoverypassword_Internalname, context.GetMessage( "WWP_GAM_EmailSubjectRecoveryPassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 321,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEmailserver_emailsubjectforrecoverypassword_Internalname, StringUtil.RTrim( AV18EmailServer_EmailSubjectForRecoveryPassword), StringUtil.RTrim( context.localUtil.Format( AV18EmailServer_EmailSubjectForRecoveryPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,321);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEmailserver_emailsubjectforrecoverypassword_Jsonclick, 0, "Attribute", "", "", "", "", edtavEmailserver_emailsubjectforrecoverypassword_Visible, edtavEmailserver_emailsubjectforrecoverypassword_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divEmailserver_emailbodyforrecoverypassword_cell_Internalname, 1, 0, "px", 0, "px", divEmailserver_emailbodyforrecoverypassword_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavEmailserver_emailbodyforrecoverypassword_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavEmailserver_emailbodyforrecoverypassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEmailserver_emailbodyforrecoverypassword_Internalname, context.GetMessage( "WWP_GAM_EmailBodyRecoveryPassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 325,'',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavEmailserver_emailbodyforrecoverypassword_Internalname, AV14EmailServer_EmailBodyForRecoveryPassword, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,325);\"", 0, edtavEmailserver_emailbodyforrecoverypassword_Visible, edtavEmailserver_emailbodyforrecoverypassword_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "800", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_GAMRepositoryConfiguration.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 330,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "", context.GetMessage( "GX_BtnEnter", ""), bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMRepositoryConfiguration.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 332,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMRepositoryConfiguration.htm");
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

      protected void START1X2( )
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
         Form.Meta.addItem("description", context.GetMessage( "Repository configuration ", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP1X0( ) ;
      }

      protected void WS1X2( )
      {
         START1X2( ) ;
         EVT1X2( ) ;
      }

      protected void EVT1X2( )
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
                              E111X2 ();
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
                                    E121X2 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VUSERIDENTIFICATION.CONTROLVALUECHANGED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E131X2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VEMAILSERVERUSESAUTHENTICATION.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E141X2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E151X2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E161X2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E171X2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E181X2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E191X2 ();
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

      protected void WE1X2( )
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

      protected void PA1X2( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamrepositoryconfiguration.aspx")), "gamrepositoryconfiguration.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamrepositoryconfiguration.aspx")))) ;
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
                  gxfirstwebparm = GetFirstPar( "Id");
                  toggleJsOutput = isJsOutputEnabled( );
                  if ( context.isSpaRequest( ) )
                  {
                     disableJsOutput();
                  }
                  if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
                  {
                     AV45Id = (long)(Math.Round(NumberUtil.Val( gxfirstwebparm, "."), 18, MidpointRounding.ToEven));
                     AssignAttri("", false, "AV45Id", StringUtil.LTrimStr( (decimal)(AV45Id), 12, 0));
                     GxWebStd.gx_hidden_field( context, "gxhash_vID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45Id), "ZZZZZZZZZZZ9"), context));
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
               GX_FocusControl = edtavRepoid_Internalname;
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
         if ( cmbavEnabletracing.ItemCount > 0 )
         {
            AV33EnableTracing = cmbavEnabletracing.getValidValue(AV33EnableTracing);
            AssignAttri("", false, "AV33EnableTracing", AV33EnableTracing);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavEnabletracing.CurrentValue = StringUtil.RTrim( AV33EnableTracing);
            AssignProp("", false, cmbavEnabletracing_Internalname, "Values", cmbavEnabletracing.ToJavascriptSource(), true);
         }
         if ( cmbavDefaultauthtypename.ItemCount > 0 )
         {
            AV10DefaultAuthTypeName = cmbavDefaultauthtypename.getValidValue(AV10DefaultAuthTypeName);
            AssignAttri("", false, "AV10DefaultAuthTypeName", AV10DefaultAuthTypeName);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavDefaultauthtypename.CurrentValue = StringUtil.RTrim( AV10DefaultAuthTypeName);
            AssignProp("", false, cmbavDefaultauthtypename_Internalname, "Values", cmbavDefaultauthtypename.ToJavascriptSource(), true);
         }
         if ( cmbavDefaultroleid.ItemCount > 0 )
         {
            AV11DefaultRoleId = (long)(Math.Round(NumberUtil.Val( cmbavDefaultroleid.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV11DefaultRoleId), 12, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV11DefaultRoleId", StringUtil.LTrimStr( (decimal)(AV11DefaultRoleId), 12, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavDefaultroleid.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV11DefaultRoleId), 12, 0));
            AssignProp("", false, cmbavDefaultroleid_Internalname, "Values", cmbavDefaultroleid.ToJavascriptSource(), true);
         }
         if ( cmbavDefaultsecuritypolicyid.ItemCount > 0 )
         {
            AV12DefaultSecurityPolicyId = (int)(Math.Round(NumberUtil.Val( cmbavDefaultsecuritypolicyid.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV12DefaultSecurityPolicyId), 9, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV12DefaultSecurityPolicyId", StringUtil.LTrimStr( (decimal)(AV12DefaultSecurityPolicyId), 9, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavDefaultsecuritypolicyid.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV12DefaultSecurityPolicyId), 9, 0));
            AssignProp("", false, cmbavDefaultsecuritypolicyid_Internalname, "Values", cmbavDefaultsecuritypolicyid.ToJavascriptSource(), true);
         }
         AV5AllowOauthAccess = StringUtil.StrToBool( StringUtil.BoolToStr( AV5AllowOauthAccess));
         AssignAttri("", false, "AV5AllowOauthAccess", AV5AllowOauthAccess);
         if ( cmbavLogoutbehavior.ItemCount > 0 )
         {
            AV55LogoutBehavior = cmbavLogoutbehavior.getValidValue(AV55LogoutBehavior);
            AssignAttri("", false, "AV55LogoutBehavior", AV55LogoutBehavior);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavLogoutbehavior.CurrentValue = StringUtil.RTrim( AV55LogoutBehavior);
            AssignProp("", false, cmbavLogoutbehavior_Internalname, "Values", cmbavLogoutbehavior.ToJavascriptSource(), true);
         }
         AV35EnableWorkingAsGAMManagerRepo = StringUtil.StrToBool( StringUtil.BoolToStr( AV35EnableWorkingAsGAMManagerRepo));
         AssignAttri("", false, "AV35EnableWorkingAsGAMManagerRepo", AV35EnableWorkingAsGAMManagerRepo);
         if ( cmbavUseridentification.ItemCount > 0 )
         {
            AV80UserIdentification = cmbavUseridentification.getValidValue(AV80UserIdentification);
            AssignAttri("", false, "AV80UserIdentification", AV80UserIdentification);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUseridentification.CurrentValue = StringUtil.RTrim( AV80UserIdentification);
            AssignProp("", false, cmbavUseridentification_Internalname, "Values", cmbavUseridentification.ToJavascriptSource(), true);
         }
         AV79UserEmailisUnique = StringUtil.StrToBool( StringUtil.BoolToStr( AV79UserEmailisUnique));
         AssignAttri("", false, "AV79UserEmailisUnique", AV79UserEmailisUnique);
         if ( cmbavUseractivationmethod.ItemCount > 0 )
         {
            AV77UserActivationMethod = cmbavUseractivationmethod.getValidValue(AV77UserActivationMethod);
            AssignAttri("", false, "AV77UserActivationMethod", AV77UserActivationMethod);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUseractivationmethod.CurrentValue = StringUtil.RTrim( AV77UserActivationMethod);
            AssignProp("", false, cmbavUseractivationmethod_Internalname, "Values", cmbavUseractivationmethod.ToJavascriptSource(), true);
         }
         if ( cmbavUserremembermetype.ItemCount > 0 )
         {
            AV85UserRememberMeType = cmbavUserremembermetype.getValidValue(AV85UserRememberMeType);
            AssignAttri("", false, "AV85UserRememberMeType", AV85UserRememberMeType);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavUserremembermetype.CurrentValue = StringUtil.RTrim( AV85UserRememberMeType);
            AssignProp("", false, cmbavUserremembermetype_Internalname, "Values", cmbavUserremembermetype.ToJavascriptSource(), true);
         }
         AV64RequiredEmail = StringUtil.StrToBool( StringUtil.BoolToStr( AV64RequiredEmail));
         AssignAttri("", false, "AV64RequiredEmail", AV64RequiredEmail);
         AV68RequiredPassword = StringUtil.StrToBool( StringUtil.BoolToStr( AV68RequiredPassword));
         AssignAttri("", false, "AV68RequiredPassword", AV68RequiredPassword);
         AV65RequiredFirstName = StringUtil.StrToBool( StringUtil.BoolToStr( AV65RequiredFirstName));
         AssignAttri("", false, "AV65RequiredFirstName", AV65RequiredFirstName);
         AV67RequiredLastName = StringUtil.StrToBool( StringUtil.BoolToStr( AV67RequiredLastName));
         AssignAttri("", false, "AV67RequiredLastName", AV67RequiredLastName);
         AV63RequiredBirthday = StringUtil.StrToBool( StringUtil.BoolToStr( AV63RequiredBirthday));
         AssignAttri("", false, "AV63RequiredBirthday", AV63RequiredBirthday);
         AV66RequiredGender = StringUtil.StrToBool( StringUtil.BoolToStr( AV66RequiredGender));
         AssignAttri("", false, "AV66RequiredGender", AV66RequiredGender);
         if ( cmbavGeneratesessionstatistics.ItemCount > 0 )
         {
            AV42GenerateSessionStatistics = cmbavGeneratesessionstatistics.getValidValue(AV42GenerateSessionStatistics);
            AssignAttri("", false, "AV42GenerateSessionStatistics", AV42GenerateSessionStatistics);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavGeneratesessionstatistics.CurrentValue = StringUtil.RTrim( AV42GenerateSessionStatistics);
            AssignProp("", false, cmbavGeneratesessionstatistics_Internalname, "Values", cmbavGeneratesessionstatistics.ToJavascriptSource(), true);
         }
         AV43GiveAnonymousSession = StringUtil.StrToBool( StringUtil.BoolToStr( AV43GiveAnonymousSession));
         AssignAttri("", false, "AV43GiveAnonymousSession", AV43GiveAnonymousSession);
         AV76SessionExpiresOnIPChange = StringUtil.StrToBool( StringUtil.BoolToStr( AV76SessionExpiresOnIPChange));
         AssignAttri("", false, "AV76SessionExpiresOnIPChange", AV76SessionExpiresOnIPChange);
         AV51IntSecByDomainEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV51IntSecByDomainEnable));
         AssignAttri("", false, "AV51IntSecByDomainEnable", AV51IntSecByDomainEnable);
         if ( cmbavIntsecbydomainmode.ItemCount > 0 )
         {
            AV49IntSecByDomainMode = cmbavIntsecbydomainmode.getValidValue(AV49IntSecByDomainMode);
            AssignAttri("", false, "AV49IntSecByDomainMode", AV49IntSecByDomainMode);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavIntsecbydomainmode.CurrentValue = StringUtil.RTrim( AV49IntSecByDomainMode);
            AssignProp("", false, cmbavIntsecbydomainmode_Internalname, "Values", cmbavIntsecbydomainmode.ToJavascriptSource(), true);
         }
         AV30EmailServerSecure = StringUtil.StrToBool( StringUtil.BoolToStr( AV30EmailServerSecure));
         AssignAttri("", false, "AV30EmailServerSecure", AV30EmailServerSecure);
         AV32EmailServerUsesAuthentication = StringUtil.StrToBool( StringUtil.BoolToStr( AV32EmailServerUsesAuthentication));
         AssignAttri("", false, "AV32EmailServerUsesAuthentication", AV32EmailServerUsesAuthentication);
         AV23EmailServer_SendEmailWhenUserActivateAccount = StringUtil.StrToBool( StringUtil.BoolToStr( AV23EmailServer_SendEmailWhenUserActivateAccount));
         AssignAttri("", false, "AV23EmailServer_SendEmailWhenUserActivateAccount", AV23EmailServer_SendEmailWhenUserActivateAccount);
         AV25EmailServer_SendEmailWhenUserChangePassword = StringUtil.StrToBool( StringUtil.BoolToStr( AV25EmailServer_SendEmailWhenUserChangePassword));
         AssignAttri("", false, "AV25EmailServer_SendEmailWhenUserChangePassword", AV25EmailServer_SendEmailWhenUserChangePassword);
         AV24EmailServer_SendEmailWhenUserChangeEmail = StringUtil.StrToBool( StringUtil.BoolToStr( AV24EmailServer_SendEmailWhenUserChangeEmail));
         AssignAttri("", false, "AV24EmailServer_SendEmailWhenUserChangeEmail", AV24EmailServer_SendEmailWhenUserChangeEmail);
         AV22EmailServer_SendEmailForRecoveryPassword = StringUtil.StrToBool( StringUtil.BoolToStr( AV22EmailServer_SendEmailForRecoveryPassword));
         AssignAttri("", false, "AV22EmailServer_SendEmailForRecoveryPassword", AV22EmailServer_SendEmailForRecoveryPassword);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF1X2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavRepoid_Enabled = 0;
         AssignProp("", false, edtavRepoid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRepoid_Enabled), 5, 0), true);
         edtavGuid_Enabled = 0;
         AssignProp("", false, edtavGuid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGuid_Enabled), 5, 0), true);
         edtavNamespace_Enabled = 0;
         AssignProp("", false, edtavNamespace_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavNamespace_Enabled), 5, 0), true);
      }

      protected void RF1X2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E191X2 ();
            WB1X0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes1X2( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_vREPOID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vSECURITYADMINISTRATOREMAIL", AV71SecurityAdministratorEmail);
         GxWebStd.gx_hidden_field( context, "gxhash_vSECURITYADMINISTRATOREMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV71SecurityAdministratorEmail, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vCANREGISTERUSERS", AV9CanRegisterUsers);
         GxWebStd.gx_hidden_field( context, "gxhash_vCANREGISTERUSERS", GetSecureSignedToken( "", AV9CanRegisterUsers, context));
      }

      protected void before_start_formulas( )
      {
         edtavRepoid_Enabled = 0;
         AssignProp("", false, edtavRepoid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavRepoid_Enabled), 5, 0), true);
         edtavGuid_Enabled = 0;
         AssignProp("", false, edtavGuid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGuid_Enabled), 5, 0), true);
         edtavNamespace_Enabled = 0;
         AssignProp("", false, edtavNamespace_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavNamespace_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1X0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E111X2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            Gxuitabspanel_tabs_Pagecount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GXUITABSPANEL_TABS_Pagecount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gxuitabspanel_tabs_Class = cgiGet( "GXUITABSPANEL_TABS_Class");
            Gxuitabspanel_tabs_Historymanagement = StringUtil.StrToBool( cgiGet( "GXUITABSPANEL_TABS_Historymanagement"));
            /* Read variables values. */
            if ( ( ( context.localUtil.CToN( cgiGet( edtavRepoid_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavRepoid_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vREPOID");
               GX_FocusControl = edtavRepoid_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV59RepoId = 0;
               AssignAttri("", false, "AV59RepoId", StringUtil.LTrimStr( (decimal)(AV59RepoId), 12, 0));
               GxWebStd.gx_hidden_field( context, "gxhash_vREPOID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"), context));
            }
            else
            {
               AV59RepoId = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavRepoid_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV59RepoId", StringUtil.LTrimStr( (decimal)(AV59RepoId), 12, 0));
               GxWebStd.gx_hidden_field( context, "gxhash_vREPOID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"), context));
            }
            AV44GUID = cgiGet( edtavGuid_Internalname);
            AssignAttri("", false, "AV44GUID", AV44GUID);
            AV58NameSpace = cgiGet( edtavNamespace_Internalname);
            AssignAttri("", false, "AV58NameSpace", AV58NameSpace);
            AV57Name = cgiGet( edtavName_Internalname);
            AssignAttri("", false, "AV57Name", AV57Name);
            AV13Dsc = cgiGet( edtavDsc_Internalname);
            AssignAttri("", false, "AV13Dsc", AV13Dsc);
            cmbavEnabletracing.CurrentValue = cgiGet( cmbavEnabletracing_Internalname);
            AV33EnableTracing = cgiGet( cmbavEnabletracing_Internalname);
            AssignAttri("", false, "AV33EnableTracing", AV33EnableTracing);
            AV6AuthenticationMasterRepository = cgiGet( edtavAuthenticationmasterrepository_Internalname);
            AssignAttri("", false, "AV6AuthenticationMasterRepository", AV6AuthenticationMasterRepository);
            cmbavDefaultauthtypename.CurrentValue = cgiGet( cmbavDefaultauthtypename_Internalname);
            AV10DefaultAuthTypeName = cgiGet( cmbavDefaultauthtypename_Internalname);
            AssignAttri("", false, "AV10DefaultAuthTypeName", AV10DefaultAuthTypeName);
            cmbavDefaultroleid.CurrentValue = cgiGet( cmbavDefaultroleid_Internalname);
            AV11DefaultRoleId = (long)(Math.Round(NumberUtil.Val( cgiGet( cmbavDefaultroleid_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV11DefaultRoleId", StringUtil.LTrimStr( (decimal)(AV11DefaultRoleId), 12, 0));
            cmbavDefaultsecuritypolicyid.CurrentValue = cgiGet( cmbavDefaultsecuritypolicyid_Internalname);
            AV12DefaultSecurityPolicyId = (int)(Math.Round(NumberUtil.Val( cgiGet( cmbavDefaultsecuritypolicyid_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV12DefaultSecurityPolicyId", StringUtil.LTrimStr( (decimal)(AV12DefaultSecurityPolicyId), 9, 0));
            AV5AllowOauthAccess = StringUtil.StrToBool( cgiGet( chkavAllowoauthaccess_Internalname));
            AssignAttri("", false, "AV5AllowOauthAccess", AV5AllowOauthAccess);
            cmbavLogoutbehavior.CurrentValue = cgiGet( cmbavLogoutbehavior_Internalname);
            AV55LogoutBehavior = cgiGet( cmbavLogoutbehavior_Internalname);
            AssignAttri("", false, "AV55LogoutBehavior", AV55LogoutBehavior);
            AV35EnableWorkingAsGAMManagerRepo = StringUtil.StrToBool( cgiGet( chkavEnableworkingasgammanagerrepo_Internalname));
            AssignAttri("", false, "AV35EnableWorkingAsGAMManagerRepo", AV35EnableWorkingAsGAMManagerRepo);
            cmbavUseridentification.CurrentValue = cgiGet( cmbavUseridentification_Internalname);
            AV80UserIdentification = cgiGet( cmbavUseridentification_Internalname);
            AssignAttri("", false, "AV80UserIdentification", AV80UserIdentification);
            AV79UserEmailisUnique = StringUtil.StrToBool( cgiGet( chkavUseremailisunique_Internalname));
            AssignAttri("", false, "AV79UserEmailisUnique", AV79UserEmailisUnique);
            cmbavUseractivationmethod.CurrentValue = cgiGet( cmbavUseractivationmethod_Internalname);
            AV77UserActivationMethod = cgiGet( cmbavUseractivationmethod_Internalname);
            AssignAttri("", false, "AV77UserActivationMethod", AV77UserActivationMethod);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavUserautomaticactivationtimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavUserautomaticactivationtimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vUSERAUTOMATICACTIVATIONTIMEOUT");
               GX_FocusControl = edtavUserautomaticactivationtimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV78UserAutomaticActivationTimeout = 0;
               AssignAttri("", false, "AV78UserAutomaticActivationTimeout", StringUtil.LTrimStr( (decimal)(AV78UserAutomaticActivationTimeout), 4, 0));
            }
            else
            {
               AV78UserAutomaticActivationTimeout = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavUserautomaticactivationtimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV78UserAutomaticActivationTimeout", StringUtil.LTrimStr( (decimal)(AV78UserAutomaticActivationTimeout), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeytimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeytimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vUSERRECOVERYPASSWORDKEYTIMEOUT");
               GX_FocusControl = edtavUserrecoverypasswordkeytimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV81UserRecoveryPasswordKeyTimeOut = 0;
               AssignAttri("", false, "AV81UserRecoveryPasswordKeyTimeOut", StringUtil.LTrimStr( (decimal)(AV81UserRecoveryPasswordKeyTimeOut), 4, 0));
            }
            else
            {
               AV81UserRecoveryPasswordKeyTimeOut = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeytimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV81UserRecoveryPasswordKeyTimeOut", StringUtil.LTrimStr( (decimal)(AV81UserRecoveryPasswordKeyTimeOut), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeydailymaximum_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeydailymaximum_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vUSERRECOVERYPASSWORDKEYDAILYMAXIMUM");
               GX_FocusControl = edtavUserrecoverypasswordkeydailymaximum_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV82UserRecoveryPasswordKeyDailyMaximum = 0;
               AssignAttri("", false, "AV82UserRecoveryPasswordKeyDailyMaximum", StringUtil.LTrimStr( (decimal)(AV82UserRecoveryPasswordKeyDailyMaximum), 4, 0));
            }
            else
            {
               AV82UserRecoveryPasswordKeyDailyMaximum = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeydailymaximum_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV82UserRecoveryPasswordKeyDailyMaximum", StringUtil.LTrimStr( (decimal)(AV82UserRecoveryPasswordKeyDailyMaximum), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeymonthlymaximum_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeymonthlymaximum_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vUSERRECOVERYPASSWORDKEYMONTHLYMAXIMUM");
               GX_FocusControl = edtavUserrecoverypasswordkeymonthlymaximum_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV83UserRecoveryPasswordKeyMonthlyMaximum = 0;
               AssignAttri("", false, "AV83UserRecoveryPasswordKeyMonthlyMaximum", StringUtil.LTrimStr( (decimal)(AV83UserRecoveryPasswordKeyMonthlyMaximum), 4, 0));
            }
            else
            {
               AV83UserRecoveryPasswordKeyMonthlyMaximum = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavUserrecoverypasswordkeymonthlymaximum_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV83UserRecoveryPasswordKeyMonthlyMaximum", StringUtil.LTrimStr( (decimal)(AV83UserRecoveryPasswordKeyMonthlyMaximum), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavLoginattemptstolockuser_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavLoginattemptstolockuser_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 99 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vLOGINATTEMPTSTOLOCKUSER");
               GX_FocusControl = edtavLoginattemptstolockuser_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV54LoginAttemptsToLockUser = 0;
               AssignAttri("", false, "AV54LoginAttemptsToLockUser", StringUtil.LTrimStr( (decimal)(AV54LoginAttemptsToLockUser), 2, 0));
            }
            else
            {
               AV54LoginAttemptsToLockUser = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavLoginattemptstolockuser_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV54LoginAttemptsToLockUser", StringUtil.LTrimStr( (decimal)(AV54LoginAttemptsToLockUser), 2, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavGamunblockusertimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavGamunblockusertimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vGAMUNBLOCKUSERTIMEOUT");
               GX_FocusControl = edtavGamunblockusertimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV41GAMUnblockUserTimeout = 0;
               AssignAttri("", false, "AV41GAMUnblockUserTimeout", StringUtil.LTrimStr( (decimal)(AV41GAMUnblockUserTimeout), 4, 0));
            }
            else
            {
               AV41GAMUnblockUserTimeout = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavGamunblockusertimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV41GAMUnblockUserTimeout", StringUtil.LTrimStr( (decimal)(AV41GAMUnblockUserTimeout), 4, 0));
            }
            cmbavUserremembermetype.CurrentValue = cgiGet( cmbavUserremembermetype_Internalname);
            AV85UserRememberMeType = cgiGet( cmbavUserremembermetype_Internalname);
            AssignAttri("", false, "AV85UserRememberMeType", AV85UserRememberMeType);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavUserremembermetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavUserremembermetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vUSERREMEMBERMETIMEOUT");
               GX_FocusControl = edtavUserremembermetimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV84UserRememberMeTimeOut = 0;
               AssignAttri("", false, "AV84UserRememberMeTimeOut", StringUtil.LTrimStr( (decimal)(AV84UserRememberMeTimeOut), 4, 0));
            }
            else
            {
               AV84UserRememberMeTimeOut = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavUserremembermetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV84UserRememberMeTimeOut", StringUtil.LTrimStr( (decimal)(AV84UserRememberMeTimeOut), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTotpsecretkeylength_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTotpsecretkeylength_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999999L )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTOTPSECRETKEYLENGTH");
               GX_FocusControl = edtavTotpsecretkeylength_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV50TOTPSecretKeyLength = 0;
               AssignAttri("", false, "AV50TOTPSecretKeyLength", StringUtil.LTrimStr( (decimal)(AV50TOTPSecretKeyLength), 12, 0));
            }
            else
            {
               AV50TOTPSecretKeyLength = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavTotpsecretkeylength_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV50TOTPSecretKeyLength", StringUtil.LTrimStr( (decimal)(AV50TOTPSecretKeyLength), 12, 0));
            }
            AV64RequiredEmail = StringUtil.StrToBool( cgiGet( chkavRequiredemail_Internalname));
            AssignAttri("", false, "AV64RequiredEmail", AV64RequiredEmail);
            AV68RequiredPassword = StringUtil.StrToBool( cgiGet( chkavRequiredpassword_Internalname));
            AssignAttri("", false, "AV68RequiredPassword", AV68RequiredPassword);
            AV65RequiredFirstName = StringUtil.StrToBool( cgiGet( chkavRequiredfirstname_Internalname));
            AssignAttri("", false, "AV65RequiredFirstName", AV65RequiredFirstName);
            AV67RequiredLastName = StringUtil.StrToBool( cgiGet( chkavRequiredlastname_Internalname));
            AssignAttri("", false, "AV67RequiredLastName", AV67RequiredLastName);
            AV63RequiredBirthday = StringUtil.StrToBool( cgiGet( chkavRequiredbirthday_Internalname));
            AssignAttri("", false, "AV63RequiredBirthday", AV63RequiredBirthday);
            AV66RequiredGender = StringUtil.StrToBool( cgiGet( chkavRequiredgender_Internalname));
            AssignAttri("", false, "AV66RequiredGender", AV66RequiredGender);
            cmbavGeneratesessionstatistics.CurrentValue = cgiGet( cmbavGeneratesessionstatistics_Internalname);
            AV42GenerateSessionStatistics = cgiGet( cmbavGeneratesessionstatistics_Internalname);
            AssignAttri("", false, "AV42GenerateSessionStatistics", AV42GenerateSessionStatistics);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavUsersessioncachetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavUsersessioncachetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vUSERSESSIONCACHETIMEOUT");
               GX_FocusControl = edtavUsersessioncachetimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV86UserSessionCacheTimeout = 0;
               AssignAttri("", false, "AV86UserSessionCacheTimeout", StringUtil.LTrimStr( (decimal)(AV86UserSessionCacheTimeout), 9, 0));
            }
            else
            {
               AV86UserSessionCacheTimeout = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavUsersessioncachetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV86UserSessionCacheTimeout", StringUtil.LTrimStr( (decimal)(AV86UserSessionCacheTimeout), 9, 0));
            }
            AV43GiveAnonymousSession = StringUtil.StrToBool( cgiGet( chkavGiveanonymoussession_Internalname));
            AssignAttri("", false, "AV43GiveAnonymousSession", AV43GiveAnonymousSession);
            AV76SessionExpiresOnIPChange = StringUtil.StrToBool( cgiGet( chkavSessionexpiresonipchange_Internalname));
            AssignAttri("", false, "AV76SessionExpiresOnIPChange", AV76SessionExpiresOnIPChange);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavLoginattemptstolocksession_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavLoginattemptstolocksession_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 99 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vLOGINATTEMPTSTOLOCKSESSION");
               GX_FocusControl = edtavLoginattemptstolocksession_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV53LoginAttemptsToLockSession = 0;
               AssignAttri("", false, "AV53LoginAttemptsToLockSession", StringUtil.LTrimStr( (decimal)(AV53LoginAttemptsToLockSession), 2, 0));
            }
            else
            {
               AV53LoginAttemptsToLockSession = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavLoginattemptstolocksession_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV53LoginAttemptsToLockSession", StringUtil.LTrimStr( (decimal)(AV53LoginAttemptsToLockSession), 2, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavMinimumamountcharactersinlogin_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavMinimumamountcharactersinlogin_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 99 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vMINIMUMAMOUNTCHARACTERSINLOGIN");
               GX_FocusControl = edtavMinimumamountcharactersinlogin_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV56MinimumAmountCharactersInLogin = 0;
               AssignAttri("", false, "AV56MinimumAmountCharactersInLogin", StringUtil.LTrimStr( (decimal)(AV56MinimumAmountCharactersInLogin), 2, 0));
            }
            else
            {
               AV56MinimumAmountCharactersInLogin = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavMinimumamountcharactersinlogin_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV56MinimumAmountCharactersInLogin", StringUtil.LTrimStr( (decimal)(AV56MinimumAmountCharactersInLogin), 2, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavRepositorycachetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavRepositorycachetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vREPOSITORYCACHETIMEOUT");
               GX_FocusControl = edtavRepositorycachetimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV61RepositoryCacheTimeout = 0;
               AssignAttri("", false, "AV61RepositoryCacheTimeout", StringUtil.LTrimStr( (decimal)(AV61RepositoryCacheTimeout), 9, 0));
            }
            else
            {
               AV61RepositoryCacheTimeout = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavRepositorycachetimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV61RepositoryCacheTimeout", StringUtil.LTrimStr( (decimal)(AV61RepositoryCacheTimeout), 9, 0));
            }
            AV51IntSecByDomainEnable = StringUtil.StrToBool( cgiGet( chkavIntsecbydomainenable_Internalname));
            AssignAttri("", false, "AV51IntSecByDomainEnable", AV51IntSecByDomainEnable);
            cmbavIntsecbydomainmode.CurrentValue = cgiGet( cmbavIntsecbydomainmode_Internalname);
            AV49IntSecByDomainMode = cgiGet( cmbavIntsecbydomainmode_Internalname);
            AssignAttri("", false, "AV49IntSecByDomainMode", AV49IntSecByDomainMode);
            AV48IntSecByDomainJWTSecret = cgiGet( edtavIntsecbydomainjwtsecret_Internalname);
            AssignAttri("", false, "AV48IntSecByDomainJWTSecret", AV48IntSecByDomainJWTSecret);
            AV47IntSecByDomainEncryptionKey = cgiGet( edtavIntsecbydomainencryptionkey_Internalname);
            AssignAttri("", false, "AV47IntSecByDomainEncryptionKey", AV47IntSecByDomainEncryptionKey);
            AV28EmailServerHost = cgiGet( edtavEmailserverhost_Internalname);
            AssignAttri("", false, "AV28EmailServerHost", AV28EmailServerHost);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavEmailserverport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavEmailserverport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vEMAILSERVERPORT");
               GX_FocusControl = edtavEmailserverport_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV29EmailServerPort = 0;
               AssignAttri("", false, "AV29EmailServerPort", StringUtil.LTrimStr( (decimal)(AV29EmailServerPort), 4, 0));
            }
            else
            {
               AV29EmailServerPort = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavEmailserverport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV29EmailServerPort", StringUtil.LTrimStr( (decimal)(AV29EmailServerPort), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavEmailservertimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavEmailservertimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vEMAILSERVERTIMEOUT");
               GX_FocusControl = edtavEmailservertimeout_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV31EmailServerTimeout = 0;
               AssignAttri("", false, "AV31EmailServerTimeout", StringUtil.LTrimStr( (decimal)(AV31EmailServerTimeout), 4, 0));
            }
            else
            {
               AV31EmailServerTimeout = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavEmailservertimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV31EmailServerTimeout", StringUtil.LTrimStr( (decimal)(AV31EmailServerTimeout), 4, 0));
            }
            AV30EmailServerSecure = StringUtil.StrToBool( cgiGet( chkavEmailserversecure_Internalname));
            AssignAttri("", false, "AV30EmailServerSecure", AV30EmailServerSecure);
            AV74ServerSenderAddress = cgiGet( edtavServersenderaddress_Internalname);
            AssignAttri("", false, "AV74ServerSenderAddress", AV74ServerSenderAddress);
            AV75ServerSenderName = cgiGet( edtavServersendername_Internalname);
            AssignAttri("", false, "AV75ServerSenderName", AV75ServerSenderName);
            AV32EmailServerUsesAuthentication = StringUtil.StrToBool( cgiGet( chkavEmailserverusesauthentication_Internalname));
            AssignAttri("", false, "AV32EmailServerUsesAuthentication", AV32EmailServerUsesAuthentication);
            AV26EmailServerAuthenticationUsername = cgiGet( edtavEmailserverauthenticationusername_Internalname);
            AssignAttri("", false, "AV26EmailServerAuthenticationUsername", AV26EmailServerAuthenticationUsername);
            AV27EmailServerAuthenticationUserPassword = cgiGet( edtavEmailserverauthenticationuserpassword_Internalname);
            AssignAttri("", false, "AV27EmailServerAuthenticationUserPassword", AV27EmailServerAuthenticationUserPassword);
            AV23EmailServer_SendEmailWhenUserActivateAccount = StringUtil.StrToBool( cgiGet( chkavEmailserver_sendemailwhenuseractivateaccount_Internalname));
            AssignAttri("", false, "AV23EmailServer_SendEmailWhenUserActivateAccount", AV23EmailServer_SendEmailWhenUserActivateAccount);
            AV19EmailServer_EmailSubjectWhenUserActivateAccount = cgiGet( edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname);
            AssignAttri("", false, "AV19EmailServer_EmailSubjectWhenUserActivateAccount", AV19EmailServer_EmailSubjectWhenUserActivateAccount);
            AV15EmailServer_EmailBodyWhenUserActivateAccount = cgiGet( edtavEmailserver_emailbodywhenuseractivateaccount_Internalname);
            AssignAttri("", false, "AV15EmailServer_EmailBodyWhenUserActivateAccount", AV15EmailServer_EmailBodyWhenUserActivateAccount);
            AV25EmailServer_SendEmailWhenUserChangePassword = StringUtil.StrToBool( cgiGet( chkavEmailserver_sendemailwhenuserchangepassword_Internalname));
            AssignAttri("", false, "AV25EmailServer_SendEmailWhenUserChangePassword", AV25EmailServer_SendEmailWhenUserChangePassword);
            AV21EmailServer_EmailSubjectWhenUserChangePassword = cgiGet( edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname);
            AssignAttri("", false, "AV21EmailServer_EmailSubjectWhenUserChangePassword", AV21EmailServer_EmailSubjectWhenUserChangePassword);
            AV17EmailServer_EmailBodyWhenUserChangePassword = cgiGet( edtavEmailserver_emailbodywhenuserchangepassword_Internalname);
            AssignAttri("", false, "AV17EmailServer_EmailBodyWhenUserChangePassword", AV17EmailServer_EmailBodyWhenUserChangePassword);
            AV24EmailServer_SendEmailWhenUserChangeEmail = StringUtil.StrToBool( cgiGet( chkavEmailserver_sendemailwhenuserchangeemail_Internalname));
            AssignAttri("", false, "AV24EmailServer_SendEmailWhenUserChangeEmail", AV24EmailServer_SendEmailWhenUserChangeEmail);
            AV20EmailServer_EmailSubjectWhenUserChangeEmail = cgiGet( edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname);
            AssignAttri("", false, "AV20EmailServer_EmailSubjectWhenUserChangeEmail", AV20EmailServer_EmailSubjectWhenUserChangeEmail);
            AV16EmailServer_EmailBodyWhenUserChangeEmail = cgiGet( edtavEmailserver_emailbodywhenuserchangeemail_Internalname);
            AssignAttri("", false, "AV16EmailServer_EmailBodyWhenUserChangeEmail", AV16EmailServer_EmailBodyWhenUserChangeEmail);
            AV22EmailServer_SendEmailForRecoveryPassword = StringUtil.StrToBool( cgiGet( chkavEmailserver_sendemailforrecoverypassword_Internalname));
            AssignAttri("", false, "AV22EmailServer_SendEmailForRecoveryPassword", AV22EmailServer_SendEmailForRecoveryPassword);
            AV18EmailServer_EmailSubjectForRecoveryPassword = cgiGet( edtavEmailserver_emailsubjectforrecoverypassword_Internalname);
            AssignAttri("", false, "AV18EmailServer_EmailSubjectForRecoveryPassword", AV18EmailServer_EmailSubjectForRecoveryPassword);
            AV14EmailServer_EmailBodyForRecoveryPassword = cgiGet( edtavEmailserver_emailbodyforrecoverypassword_Internalname);
            AssignAttri("", false, "AV14EmailServer_EmailBodyForRecoveryPassword", AV14EmailServer_EmailBodyForRecoveryPassword);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"GAMRepositoryConfiguration");
            AV59RepoId = (long)(Math.Round(context.localUtil.CToN( cgiGet( edtavRepoid_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV59RepoId", StringUtil.LTrimStr( (decimal)(AV59RepoId), 12, 0));
            GxWebStd.gx_hidden_field( context, "gxhash_vREPOID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"), context));
            forbiddenHiddens.Add("RepoId", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("gamrepositoryconfiguration:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
         E111X2 ();
         if (returnInSub) return;
      }

      protected void E111X2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV8AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV52Language, out  AV37Errors);
         AV90GXV1 = 1;
         while ( AV90GXV1 <= AV8AuthenticationTypes.Count )
         {
            AV7AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV8AuthenticationTypes.Item(AV90GXV1));
            cmbavDefaultauthtypename.addItem(AV7AuthenticationType.gxTpr_Name, AV7AuthenticationType.gxTpr_Description, 0);
            AV90GXV1 = (int)(AV90GXV1+1);
         }
         AV72SecurityPolicies = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getsecuritypolicies(AV40FilterSecPol, out  AV37Errors);
         AV91GXV2 = 1;
         while ( AV91GXV2 <= AV72SecurityPolicies.Count )
         {
            AV73SecurityPolicy = ((GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy)AV72SecurityPolicies.Item(AV91GXV2));
            cmbavDefaultsecuritypolicyid.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(AV73SecurityPolicy.gxTpr_Id), 9, 0)), AV73SecurityPolicy.gxTpr_Name, 0);
            AV91GXV2 = (int)(AV91GXV2+1);
         }
         AV70Roles = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getroles(AV39FilterRole, out  AV37Errors);
         AV92GXV3 = 1;
         while ( AV92GXV3 <= AV70Roles.Count )
         {
            AV69Role = ((GeneXus.Programs.genexussecurity.SdtGAMRole)AV70Roles.Item(AV92GXV3));
            cmbavDefaultroleid.addItem(StringUtil.Trim( StringUtil.Str( (decimal)(AV69Role.gxTpr_Id), 12, 0)), AV69Role.gxTpr_Name, 0);
            AV92GXV3 = (int)(AV92GXV3+1);
         }
         if ( (0==AV45Id) )
         {
            AV59RepoId = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getid();
            AssignAttri("", false, "AV59RepoId", StringUtil.LTrimStr( (decimal)(AV59RepoId), 12, 0));
            GxWebStd.gx_hidden_field( context, "gxhash_vREPOID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"), context));
         }
         else
         {
            AV59RepoId = AV45Id;
            AssignAttri("", false, "AV59RepoId", StringUtil.LTrimStr( (decimal)(AV59RepoId), 12, 0));
            GxWebStd.gx_hidden_field( context, "gxhash_vREPOID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV59RepoId), "ZZZZZZZZZZZ9"), context));
         }
         AV60Repository.load( (int)(AV59RepoId));
         AV44GUID = AV60Repository.gxTpr_Guid;
         AssignAttri("", false, "AV44GUID", AV44GUID);
         AV58NameSpace = AV60Repository.gxTpr_Namespace;
         AssignAttri("", false, "AV58NameSpace", AV58NameSpace);
         AV57Name = AV60Repository.gxTpr_Name;
         AssignAttri("", false, "AV57Name", AV57Name);
         AV13Dsc = AV60Repository.gxTpr_Description;
         AssignAttri("", false, "AV13Dsc", AV13Dsc);
         AV10DefaultAuthTypeName = AV60Repository.gxTpr_Defaultauthenticationtypename;
         AssignAttri("", false, "AV10DefaultAuthTypeName", AV10DefaultAuthTypeName);
         AV80UserIdentification = AV60Repository.gxTpr_Useridentification;
         AssignAttri("", false, "AV80UserIdentification", AV80UserIdentification);
         AV42GenerateSessionStatistics = AV60Repository.gxTpr_Generatesessionstatistics;
         AssignAttri("", false, "AV42GenerateSessionStatistics", AV42GenerateSessionStatistics);
         AV77UserActivationMethod = AV60Repository.gxTpr_Useractivationmethod;
         AssignAttri("", false, "AV77UserActivationMethod", AV77UserActivationMethod);
         AV78UserAutomaticActivationTimeout = (short)(AV60Repository.gxTpr_Userautomaticactivationtimeout);
         AssignAttri("", false, "AV78UserAutomaticActivationTimeout", StringUtil.LTrimStr( (decimal)(AV78UserAutomaticActivationTimeout), 4, 0));
         AV85UserRememberMeType = AV60Repository.gxTpr_Userremembermetype;
         AssignAttri("", false, "AV85UserRememberMeType", AV85UserRememberMeType);
         AV84UserRememberMeTimeOut = AV60Repository.gxTpr_Userremembermetimeout;
         AssignAttri("", false, "AV84UserRememberMeTimeOut", StringUtil.LTrimStr( (decimal)(AV84UserRememberMeTimeOut), 4, 0));
         AV81UserRecoveryPasswordKeyTimeOut = (short)(AV60Repository.gxTpr_Userrecoverypasswordkeytimeout);
         AssignAttri("", false, "AV81UserRecoveryPasswordKeyTimeOut", StringUtil.LTrimStr( (decimal)(AV81UserRecoveryPasswordKeyTimeOut), 4, 0));
         AV82UserRecoveryPasswordKeyDailyMaximum = AV60Repository.gxTpr_Userrecoverypasswordkeydailymaximum;
         AssignAttri("", false, "AV82UserRecoveryPasswordKeyDailyMaximum", StringUtil.LTrimStr( (decimal)(AV82UserRecoveryPasswordKeyDailyMaximum), 4, 0));
         AV83UserRecoveryPasswordKeyMonthlyMaximum = AV60Repository.gxTpr_Userrecoverypasswordkeymonthlymaximum;
         AssignAttri("", false, "AV83UserRecoveryPasswordKeyMonthlyMaximum", StringUtil.LTrimStr( (decimal)(AV83UserRecoveryPasswordKeyMonthlyMaximum), 4, 0));
         AV56MinimumAmountCharactersInLogin = AV60Repository.gxTpr_Minimumamountcharactersinlogin;
         AssignAttri("", false, "AV56MinimumAmountCharactersInLogin", StringUtil.LTrimStr( (decimal)(AV56MinimumAmountCharactersInLogin), 2, 0));
         AV54LoginAttemptsToLockUser = AV60Repository.gxTpr_Loginattemptstolockuser;
         AssignAttri("", false, "AV54LoginAttemptsToLockUser", StringUtil.LTrimStr( (decimal)(AV54LoginAttemptsToLockUser), 2, 0));
         AV41GAMUnblockUserTimeout = (short)(AV60Repository.gxTpr_Gamunblockusertimeout);
         AssignAttri("", false, "AV41GAMUnblockUserTimeout", StringUtil.LTrimStr( (decimal)(AV41GAMUnblockUserTimeout), 4, 0));
         AV53LoginAttemptsToLockSession = AV60Repository.gxTpr_Loginattemptstolocksession;
         AssignAttri("", false, "AV53LoginAttemptsToLockSession", StringUtil.LTrimStr( (decimal)(AV53LoginAttemptsToLockSession), 2, 0));
         AV86UserSessionCacheTimeout = AV60Repository.gxTpr_Usersessioncachetimeout;
         AssignAttri("", false, "AV86UserSessionCacheTimeout", StringUtil.LTrimStr( (decimal)(AV86UserSessionCacheTimeout), 9, 0));
         AV51IntSecByDomainEnable = AV60Repository.gxTpr_Integratedsecuritybydomainenable;
         AssignAttri("", false, "AV51IntSecByDomainEnable", AV51IntSecByDomainEnable);
         AV49IntSecByDomainMode = AV60Repository.gxTpr_Integratedsecuritybydomainmode;
         AssignAttri("", false, "AV49IntSecByDomainMode", AV49IntSecByDomainMode);
         AV48IntSecByDomainJWTSecret = AV60Repository.gxTpr_Integratedsecuritybydomainjwtsecret;
         AssignAttri("", false, "AV48IntSecByDomainJWTSecret", AV48IntSecByDomainJWTSecret);
         AV47IntSecByDomainEncryptionKey = AV60Repository.gxTpr_Integratedsecuritybydomainencryptionkey;
         AssignAttri("", false, "AV47IntSecByDomainEncryptionKey", AV47IntSecByDomainEncryptionKey);
         AV61RepositoryCacheTimeout = AV60Repository.gxTpr_Cachetimeout;
         AssignAttri("", false, "AV61RepositoryCacheTimeout", StringUtil.LTrimStr( (decimal)(AV61RepositoryCacheTimeout), 9, 0));
         AV55LogoutBehavior = StringUtil.Trim( AV60Repository.gxTpr_Gamremotelogoutbehavior);
         AssignAttri("", false, "AV55LogoutBehavior", AV55LogoutBehavior);
         AV71SecurityAdministratorEmail = AV60Repository.gxTpr_Securityadministratoremail;
         AssignAttri("", false, "AV71SecurityAdministratorEmail", AV71SecurityAdministratorEmail);
         GxWebStd.gx_hidden_field( context, "gxhash_vSECURITYADMINISTRATOREMAIL", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV71SecurityAdministratorEmail, "")), context));
         AV43GiveAnonymousSession = AV60Repository.gxTpr_Giveanonymoussession;
         AssignAttri("", false, "AV43GiveAnonymousSession", AV43GiveAnonymousSession);
         AV9CanRegisterUsers = AV60Repository.gxTpr_Canregisterusers;
         AssignAttri("", false, "AV9CanRegisterUsers", AV9CanRegisterUsers);
         GxWebStd.gx_hidden_field( context, "gxhash_vCANREGISTERUSERS", GetSecureSignedToken( "", AV9CanRegisterUsers, context));
         AV79UserEmailisUnique = AV60Repository.gxTpr_Useremailisunique;
         AssignAttri("", false, "AV79UserEmailisUnique", AV79UserEmailisUnique);
         AV11DefaultRoleId = AV60Repository.gxTpr_Defaultroleid;
         AssignAttri("", false, "AV11DefaultRoleId", StringUtil.LTrimStr( (decimal)(AV11DefaultRoleId), 12, 0));
         AV12DefaultSecurityPolicyId = AV60Repository.gxTpr_Defaultsecuritypolicyid;
         AssignAttri("", false, "AV12DefaultSecurityPolicyId", StringUtil.LTrimStr( (decimal)(AV12DefaultSecurityPolicyId), 9, 0));
         AV35EnableWorkingAsGAMManagerRepo = AV60Repository.gxTpr_Enableworkingasgammanagerrepository;
         AssignAttri("", false, "AV35EnableWorkingAsGAMManagerRepo", AV35EnableWorkingAsGAMManagerRepo);
         AV34EnableTracingNumeric = AV60Repository.gxTpr_Enabletracing;
         AV33EnableTracing = StringUtil.Str( (decimal)(AV34EnableTracingNumeric), 4, 0);
         AssignAttri("", false, "AV33EnableTracing", AV33EnableTracing);
         AV5AllowOauthAccess = AV60Repository.gxTpr_Allowoauthaccess;
         AssignAttri("", false, "AV5AllowOauthAccess", AV5AllowOauthAccess);
         AV76SessionExpiresOnIPChange = AV60Repository.gxTpr_Sessionexpiresonipchange;
         AssignAttri("", false, "AV76SessionExpiresOnIPChange", AV76SessionExpiresOnIPChange);
         AV50TOTPSecretKeyLength = AV60Repository.gxTpr_Totpsecretkeylength;
         AssignAttri("", false, "AV50TOTPSecretKeyLength", StringUtil.LTrimStr( (decimal)(AV50TOTPSecretKeyLength), 12, 0));
         AV68RequiredPassword = AV60Repository.gxTpr_Requiredpassword;
         AssignAttri("", false, "AV68RequiredPassword", AV68RequiredPassword);
         AV64RequiredEmail = AV60Repository.gxTpr_Requiredemail;
         AssignAttri("", false, "AV64RequiredEmail", AV64RequiredEmail);
         AV65RequiredFirstName = AV60Repository.gxTpr_Requiredfirstname;
         AssignAttri("", false, "AV65RequiredFirstName", AV65RequiredFirstName);
         AV67RequiredLastName = AV60Repository.gxTpr_Requiredlastname;
         AssignAttri("", false, "AV67RequiredLastName", AV67RequiredLastName);
         AV63RequiredBirthday = AV60Repository.gxTpr_Requiredbirthday;
         AssignAttri("", false, "AV63RequiredBirthday", AV63RequiredBirthday);
         AV66RequiredGender = AV60Repository.gxTpr_Requiredgender;
         AssignAttri("", false, "AV66RequiredGender", AV66RequiredGender);
         if ( ! (0==AV60Repository.gxTpr_Authenticationmasterrepositoryid) )
         {
            AV62RepositoryCollection = new GeneXus.Programs.genexussecurity.SdtGAM(context).getallrepositories(AV38Filter, out  AV37Errors);
            if ( AV62RepositoryCollection.Count > 1 )
            {
               AV93GXV4 = 1;
               while ( AV93GXV4 <= AV62RepositoryCollection.Count )
               {
                  AV87GAMRepositoryItem = ((GeneXus.Programs.genexussecurity.SdtGAMRepository)AV62RepositoryCollection.Item(AV93GXV4));
                  if ( AV87GAMRepositoryItem.gxTpr_Id == AV60Repository.gxTpr_Authenticationmasterrepositoryid )
                  {
                     AV6AuthenticationMasterRepository = StringUtil.Trim( AV87GAMRepositoryItem.gxTpr_Guid) + " - " + StringUtil.Trim( AV87GAMRepositoryItem.gxTpr_Name);
                     AssignAttri("", false, "AV6AuthenticationMasterRepository", AV6AuthenticationMasterRepository);
                     if (true) break;
                  }
                  AV93GXV4 = (int)(AV93GXV4+1);
               }
            }
         }
         divTblintsecbydomain_Visible = 0;
         AssignProp("", false, divTblintsecbydomain_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblintsecbydomain_Visible), 5, 0), true);
         if ( AV51IntSecByDomainEnable )
         {
            divTblintsecbydomain_Visible = 1;
            AssignProp("", false, divTblintsecbydomain_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblintsecbydomain_Visible), 5, 0), true);
         }
         AV28EmailServerHost = AV60Repository.gxTpr_Email.gxTpr_Serverhost;
         AssignAttri("", false, "AV28EmailServerHost", AV28EmailServerHost);
         AV29EmailServerPort = AV60Repository.gxTpr_Email.gxTpr_Serverport;
         AssignAttri("", false, "AV29EmailServerPort", StringUtil.LTrimStr( (decimal)(AV29EmailServerPort), 4, 0));
         AV30EmailServerSecure = AV60Repository.gxTpr_Email.gxTpr_Serversecure;
         AssignAttri("", false, "AV30EmailServerSecure", AV30EmailServerSecure);
         AV31EmailServerTimeout = AV60Repository.gxTpr_Email.gxTpr_Servertimeout;
         AssignAttri("", false, "AV31EmailServerTimeout", StringUtil.LTrimStr( (decimal)(AV31EmailServerTimeout), 4, 0));
         AV32EmailServerUsesAuthentication = AV60Repository.gxTpr_Email.gxTpr_Serverusesauthentication;
         AssignAttri("", false, "AV32EmailServerUsesAuthentication", AV32EmailServerUsesAuthentication);
         AV74ServerSenderAddress = AV60Repository.gxTpr_Email.gxTpr_Serversenderaddress;
         AssignAttri("", false, "AV74ServerSenderAddress", AV74ServerSenderAddress);
         AV75ServerSenderName = AV60Repository.gxTpr_Email.gxTpr_Serversendername;
         AssignAttri("", false, "AV75ServerSenderName", AV75ServerSenderName);
         if ( AV32EmailServerUsesAuthentication )
         {
            AV26EmailServerAuthenticationUsername = AV60Repository.gxTpr_Email.gxTpr_Serverauthenticationusername;
            AssignAttri("", false, "AV26EmailServerAuthenticationUsername", AV26EmailServerAuthenticationUsername);
            AV27EmailServerAuthenticationUserPassword = AV60Repository.gxTpr_Email.gxTpr_Serverauthenticationuserpassword;
            AssignAttri("", false, "AV27EmailServerAuthenticationUserPassword", AV27EmailServerAuthenticationUserPassword);
         }
         AV23EmailServer_SendEmailWhenUserActivateAccount = AV60Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount;
         AssignAttri("", false, "AV23EmailServer_SendEmailWhenUserActivateAccount", AV23EmailServer_SendEmailWhenUserActivateAccount);
         if ( AV23EmailServer_SendEmailWhenUserActivateAccount )
         {
            AV19EmailServer_EmailSubjectWhenUserActivateAccount = AV60Repository.gxTpr_Email.gxTpr_Subjectwhenuseractivateaccount;
            AssignAttri("", false, "AV19EmailServer_EmailSubjectWhenUserActivateAccount", AV19EmailServer_EmailSubjectWhenUserActivateAccount);
            AV15EmailServer_EmailBodyWhenUserActivateAccount = AV60Repository.gxTpr_Email.gxTpr_Bodywhenuseractivateaccount;
            AssignAttri("", false, "AV15EmailServer_EmailBodyWhenUserActivateAccount", AV15EmailServer_EmailBodyWhenUserActivateAccount);
         }
         AV25EmailServer_SendEmailWhenUserChangePassword = AV60Repository.gxTpr_Email.gxTpr_Sendemailwhenuserchangepassword;
         AssignAttri("", false, "AV25EmailServer_SendEmailWhenUserChangePassword", AV25EmailServer_SendEmailWhenUserChangePassword);
         if ( AV25EmailServer_SendEmailWhenUserChangePassword )
         {
            AV21EmailServer_EmailSubjectWhenUserChangePassword = AV60Repository.gxTpr_Email.gxTpr_Subjectwhenuserchangepassword;
            AssignAttri("", false, "AV21EmailServer_EmailSubjectWhenUserChangePassword", AV21EmailServer_EmailSubjectWhenUserChangePassword);
            AV17EmailServer_EmailBodyWhenUserChangePassword = AV60Repository.gxTpr_Email.gxTpr_Bodywhenuserchangepassword;
            AssignAttri("", false, "AV17EmailServer_EmailBodyWhenUserChangePassword", AV17EmailServer_EmailBodyWhenUserChangePassword);
         }
         AV24EmailServer_SendEmailWhenUserChangeEmail = AV60Repository.gxTpr_Email.gxTpr_Sendemailwhenuserchangeemail;
         AssignAttri("", false, "AV24EmailServer_SendEmailWhenUserChangeEmail", AV24EmailServer_SendEmailWhenUserChangeEmail);
         if ( AV24EmailServer_SendEmailWhenUserChangeEmail )
         {
            AV20EmailServer_EmailSubjectWhenUserChangeEmail = AV60Repository.gxTpr_Email.gxTpr_Subjectwhenuserchangeemail;
            AssignAttri("", false, "AV20EmailServer_EmailSubjectWhenUserChangeEmail", AV20EmailServer_EmailSubjectWhenUserChangeEmail);
            AV16EmailServer_EmailBodyWhenUserChangeEmail = AV60Repository.gxTpr_Email.gxTpr_Bodywhenuserchangeemail;
            AssignAttri("", false, "AV16EmailServer_EmailBodyWhenUserChangeEmail", AV16EmailServer_EmailBodyWhenUserChangeEmail);
         }
         AV22EmailServer_SendEmailForRecoveryPassword = AV60Repository.gxTpr_Email.gxTpr_Sendemailtorecoveruserpassword;
         AssignAttri("", false, "AV22EmailServer_SendEmailForRecoveryPassword", AV22EmailServer_SendEmailForRecoveryPassword);
         if ( AV22EmailServer_SendEmailForRecoveryPassword )
         {
            AV18EmailServer_EmailSubjectForRecoveryPassword = AV60Repository.gxTpr_Email.gxTpr_Subjecttorecoveruserpassword;
            AssignAttri("", false, "AV18EmailServer_EmailSubjectForRecoveryPassword", AV18EmailServer_EmailSubjectForRecoveryPassword);
            AV14EmailServer_EmailBodyForRecoveryPassword = AV60Repository.gxTpr_Email.gxTpr_Bodytorecoveruserpassword;
            AssignAttri("", false, "AV14EmailServer_EmailBodyForRecoveryPassword", AV14EmailServer_EmailBodyForRecoveryPassword);
         }
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
      }

      protected void S112( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
         if ( ! ( AV32EmailServerUsesAuthentication ) )
         {
            edtavEmailserverauthenticationusername_Visible = 0;
            AssignProp("", false, edtavEmailserverauthenticationusername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserverauthenticationusername_Visible), 5, 0), true);
            divEmailserverauthenticationusername_cell_Class = "Invisible";
            AssignProp("", false, divEmailserverauthenticationusername_cell_Internalname, "Class", divEmailserverauthenticationusername_cell_Class, true);
         }
         else
         {
            edtavEmailserverauthenticationusername_Visible = 1;
            AssignProp("", false, edtavEmailserverauthenticationusername_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserverauthenticationusername_Visible), 5, 0), true);
            divEmailserverauthenticationusername_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divEmailserverauthenticationusername_cell_Internalname, "Class", divEmailserverauthenticationusername_cell_Class, true);
         }
         if ( ! ( AV32EmailServerUsesAuthentication ) )
         {
            edtavEmailserverauthenticationuserpassword_Visible = 0;
            AssignProp("", false, edtavEmailserverauthenticationuserpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserverauthenticationuserpassword_Visible), 5, 0), true);
            divEmailserverauthenticationuserpassword_cell_Class = "Invisible";
            AssignProp("", false, divEmailserverauthenticationuserpassword_cell_Internalname, "Class", divEmailserverauthenticationuserpassword_cell_Class, true);
         }
         else
         {
            edtavEmailserverauthenticationuserpassword_Visible = 1;
            AssignProp("", false, edtavEmailserverauthenticationuserpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserverauthenticationuserpassword_Visible), 5, 0), true);
            divEmailserverauthenticationuserpassword_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divEmailserverauthenticationuserpassword_cell_Internalname, "Class", divEmailserverauthenticationuserpassword_cell_Class, true);
         }
         if ( ! ( AV23EmailServer_SendEmailWhenUserActivateAccount ) )
         {
            edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible), 5, 0), true);
            divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailsubjectwhenuseractivateaccount_cell_Internalname, "Class", divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible), 5, 0), true);
            divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailsubjectwhenuseractivateaccount_cell_Internalname, "Class", divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class, true);
         }
         if ( ! ( AV23EmailServer_SendEmailWhenUserActivateAccount ) )
         {
            edtavEmailserver_emailbodywhenuseractivateaccount_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailbodywhenuseractivateaccount_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodywhenuseractivateaccount_Visible), 5, 0), true);
            divEmailserver_emailbodywhenuseractivateaccount_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailbodywhenuseractivateaccount_cell_Internalname, "Class", divEmailserver_emailbodywhenuseractivateaccount_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailbodywhenuseractivateaccount_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailbodywhenuseractivateaccount_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodywhenuseractivateaccount_Visible), 5, 0), true);
            divEmailserver_emailbodywhenuseractivateaccount_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailbodywhenuseractivateaccount_cell_Internalname, "Class", divEmailserver_emailbodywhenuseractivateaccount_cell_Class, true);
         }
         if ( ! ( AV25EmailServer_SendEmailWhenUserChangePassword ) )
         {
            edtavEmailserver_emailsubjectwhenuserchangepassword_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectwhenuserchangepassword_Visible), 5, 0), true);
            divEmailserver_emailsubjectwhenuserchangepassword_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailsubjectwhenuserchangepassword_cell_Internalname, "Class", divEmailserver_emailsubjectwhenuserchangepassword_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailsubjectwhenuserchangepassword_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectwhenuserchangepassword_Visible), 5, 0), true);
            divEmailserver_emailsubjectwhenuserchangepassword_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailsubjectwhenuserchangepassword_cell_Internalname, "Class", divEmailserver_emailsubjectwhenuserchangepassword_cell_Class, true);
         }
         if ( ! ( AV25EmailServer_SendEmailWhenUserChangePassword ) )
         {
            edtavEmailserver_emailbodywhenuserchangepassword_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailbodywhenuserchangepassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodywhenuserchangepassword_Visible), 5, 0), true);
            divEmailserver_emailbodywhenuserchangepassword_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailbodywhenuserchangepassword_cell_Internalname, "Class", divEmailserver_emailbodywhenuserchangepassword_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailbodywhenuserchangepassword_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailbodywhenuserchangepassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodywhenuserchangepassword_Visible), 5, 0), true);
            divEmailserver_emailbodywhenuserchangepassword_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailbodywhenuserchangepassword_cell_Internalname, "Class", divEmailserver_emailbodywhenuserchangepassword_cell_Class, true);
         }
         if ( ! ( AV24EmailServer_SendEmailWhenUserChangeEmail ) )
         {
            edtavEmailserver_emailsubjectwhenuserchangeemail_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectwhenuserchangeemail_Visible), 5, 0), true);
            divEmailserver_emailsubjectwhenuserchangeemail_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailsubjectwhenuserchangeemail_cell_Internalname, "Class", divEmailserver_emailsubjectwhenuserchangeemail_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailsubjectwhenuserchangeemail_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectwhenuserchangeemail_Visible), 5, 0), true);
            divEmailserver_emailsubjectwhenuserchangeemail_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailsubjectwhenuserchangeemail_cell_Internalname, "Class", divEmailserver_emailsubjectwhenuserchangeemail_cell_Class, true);
         }
         if ( ! ( AV24EmailServer_SendEmailWhenUserChangeEmail ) )
         {
            edtavEmailserver_emailbodywhenuserchangeemail_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailbodywhenuserchangeemail_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodywhenuserchangeemail_Visible), 5, 0), true);
            divEmailserver_emailbodywhenuserchangeemail_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailbodywhenuserchangeemail_cell_Internalname, "Class", divEmailserver_emailbodywhenuserchangeemail_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailbodywhenuserchangeemail_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailbodywhenuserchangeemail_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodywhenuserchangeemail_Visible), 5, 0), true);
            divEmailserver_emailbodywhenuserchangeemail_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailbodywhenuserchangeemail_cell_Internalname, "Class", divEmailserver_emailbodywhenuserchangeemail_cell_Class, true);
         }
         if ( ! ( AV22EmailServer_SendEmailForRecoveryPassword ) )
         {
            edtavEmailserver_emailsubjectforrecoverypassword_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailsubjectforrecoverypassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectforrecoverypassword_Visible), 5, 0), true);
            divEmailserver_emailsubjectforrecoverypassword_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailsubjectforrecoverypassword_cell_Internalname, "Class", divEmailserver_emailsubjectforrecoverypassword_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailsubjectforrecoverypassword_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailsubjectforrecoverypassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailsubjectforrecoverypassword_Visible), 5, 0), true);
            divEmailserver_emailsubjectforrecoverypassword_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailsubjectforrecoverypassword_cell_Internalname, "Class", divEmailserver_emailsubjectforrecoverypassword_cell_Class, true);
         }
         if ( ! ( AV22EmailServer_SendEmailForRecoveryPassword ) )
         {
            edtavEmailserver_emailbodyforrecoverypassword_Visible = 0;
            AssignProp("", false, edtavEmailserver_emailbodyforrecoverypassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodyforrecoverypassword_Visible), 5, 0), true);
            divEmailserver_emailbodyforrecoverypassword_cell_Class = "Invisible";
            AssignProp("", false, divEmailserver_emailbodyforrecoverypassword_cell_Internalname, "Class", divEmailserver_emailbodyforrecoverypassword_cell_Class, true);
         }
         else
         {
            edtavEmailserver_emailbodyforrecoverypassword_Visible = 1;
            AssignProp("", false, edtavEmailserver_emailbodyforrecoverypassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEmailserver_emailbodyforrecoverypassword_Visible), 5, 0), true);
            divEmailserver_emailbodyforrecoverypassword_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divEmailserver_emailbodyforrecoverypassword_cell_Internalname, "Class", divEmailserver_emailbodyforrecoverypassword_cell_Class, true);
         }
         if ( ! ( ! ( ( StringUtil.StrCmp(AV80UserIdentification, "email") == 0 ) || ( StringUtil.StrCmp(AV80UserIdentification, "namema") == 0 ) ) ) )
         {
            chkavUseremailisunique.Visible = 0;
            AssignProp("", false, chkavUseremailisunique_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavUseremailisunique.Visible), 5, 0), true);
            divUseremailisunique_cell_Class = "Invisible";
            AssignProp("", false, divUseremailisunique_cell_Internalname, "Class", divUseremailisunique_cell_Class, true);
         }
         else
         {
            chkavUseremailisunique.Visible = 1;
            AssignProp("", false, chkavUseremailisunique_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavUseremailisunique.Visible), 5, 0), true);
            divUseremailisunique_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divUseremailisunique_cell_Internalname, "Class", divUseremailisunique_cell_Class, true);
         }
         if ( ! ( ! ( ( StringUtil.StrCmp(AV80UserIdentification, "email") == 0 ) || ( StringUtil.StrCmp(AV80UserIdentification, "namema") == 0 ) ) ) )
         {
            chkavRequiredemail.Visible = 0;
            AssignProp("", false, chkavRequiredemail_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRequiredemail.Visible), 5, 0), true);
            divRequiredemail_cell_Class = "Invisible";
            AssignProp("", false, divRequiredemail_cell_Internalname, "Class", divRequiredemail_cell_Class, true);
         }
         else
         {
            chkavRequiredemail.Visible = 1;
            AssignProp("", false, chkavRequiredemail_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRequiredemail.Visible), 5, 0), true);
            divRequiredemail_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divRequiredemail_cell_Internalname, "Class", divRequiredemail_cell_Class, true);
         }
         if ( ! ( ! (0==AV60Repository.gxTpr_Authenticationmasterrepositoryid) && ( AV62RepositoryCollection.Count > 1 ) ) )
         {
            edtavAuthenticationmasterrepository_Visible = 0;
            AssignProp("", false, edtavAuthenticationmasterrepository_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavAuthenticationmasterrepository_Visible), 5, 0), true);
            divAuthenticationmasterrepository_cell_Class = "Invisible";
            AssignProp("", false, divAuthenticationmasterrepository_cell_Internalname, "Class", divAuthenticationmasterrepository_cell_Class, true);
         }
         else
         {
            edtavAuthenticationmasterrepository_Visible = 1;
            AssignProp("", false, edtavAuthenticationmasterrepository_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavAuthenticationmasterrepository_Visible), 5, 0), true);
            divAuthenticationmasterrepository_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divAuthenticationmasterrepository_cell_Internalname, "Class", divAuthenticationmasterrepository_cell_Class, true);
         }
         if ( ! ( (0==AV60Repository.gxTpr_Authenticationmasterrepositoryid) ) )
         {
            cmbavDefaultauthtypename.Visible = 0;
            AssignProp("", false, cmbavDefaultauthtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavDefaultauthtypename.Visible), 5, 0), true);
            divDefaultauthtypename_cell_Class = "Invisible";
            AssignProp("", false, divDefaultauthtypename_cell_Internalname, "Class", divDefaultauthtypename_cell_Class, true);
         }
         else
         {
            cmbavDefaultauthtypename.Visible = 1;
            AssignProp("", false, cmbavDefaultauthtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavDefaultauthtypename.Visible), 5, 0), true);
            divDefaultauthtypename_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divDefaultauthtypename_cell_Internalname, "Class", divDefaultauthtypename_cell_Class, true);
         }
         if ( ! ( (0==AV60Repository.gxTpr_Authenticationmasterrepositoryid) ) )
         {
            cmbavDefaultroleid.Visible = 0;
            AssignProp("", false, cmbavDefaultroleid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavDefaultroleid.Visible), 5, 0), true);
            divDefaultroleid_cell_Class = "Invisible";
            AssignProp("", false, divDefaultroleid_cell_Internalname, "Class", divDefaultroleid_cell_Class, true);
         }
         else
         {
            cmbavDefaultroleid.Visible = 1;
            AssignProp("", false, cmbavDefaultroleid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavDefaultroleid.Visible), 5, 0), true);
            divDefaultroleid_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divDefaultroleid_cell_Internalname, "Class", divDefaultroleid_cell_Class, true);
         }
         if ( ! ( (0==AV60Repository.gxTpr_Authenticationmasterrepositoryid) ) )
         {
            cmbavDefaultsecuritypolicyid.Visible = 0;
            AssignProp("", false, cmbavDefaultsecuritypolicyid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavDefaultsecuritypolicyid.Visible), 5, 0), true);
            divDefaultsecuritypolicyid_cell_Class = "Invisible";
            AssignProp("", false, divDefaultsecuritypolicyid_cell_Internalname, "Class", divDefaultsecuritypolicyid_cell_Class, true);
         }
         else
         {
            cmbavDefaultsecuritypolicyid.Visible = 1;
            AssignProp("", false, cmbavDefaultsecuritypolicyid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavDefaultsecuritypolicyid.Visible), 5, 0), true);
            divDefaultsecuritypolicyid_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divDefaultsecuritypolicyid_cell_Internalname, "Class", divDefaultsecuritypolicyid_cell_Class, true);
         }
         if ( ! ( (0==AV60Repository.gxTpr_Authenticationmasterrepositoryid) ) )
         {
            chkavEnableworkingasgammanagerrepo.Visible = 0;
            AssignProp("", false, chkavEnableworkingasgammanagerrepo_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavEnableworkingasgammanagerrepo.Visible), 5, 0), true);
            divEnableworkingasgammanagerrepo_cell_Class = "Invisible";
            AssignProp("", false, divEnableworkingasgammanagerrepo_cell_Internalname, "Class", divEnableworkingasgammanagerrepo_cell_Class, true);
         }
         else
         {
            chkavEnableworkingasgammanagerrepo.Visible = 1;
            AssignProp("", false, chkavEnableworkingasgammanagerrepo_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavEnableworkingasgammanagerrepo.Visible), 5, 0), true);
            divEnableworkingasgammanagerrepo_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp("", false, divEnableworkingasgammanagerrepo_cell_Internalname, "Class", divEnableworkingasgammanagerrepo_cell_Class, true);
         }
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E121X2 ();
         if (returnInSub) return;
      }

      protected void E121X2( )
      {
         /* Enter Routine */
         returnInSub = false;
         AV60Repository.load( (int)(AV59RepoId));
         AV60Repository.gxTpr_Name = AV57Name;
         AV60Repository.gxTpr_Description = AV13Dsc;
         AV60Repository.gxTpr_Defaultauthenticationtypename = AV10DefaultAuthTypeName;
         AV60Repository.gxTpr_Useridentification = AV80UserIdentification;
         AV60Repository.gxTpr_Generatesessionstatistics = AV42GenerateSessionStatistics;
         AV60Repository.gxTpr_Useractivationmethod = AV77UserActivationMethod;
         AV60Repository.gxTpr_Userautomaticactivationtimeout = AV78UserAutomaticActivationTimeout;
         AV60Repository.gxTpr_Gamunblockusertimeout = AV41GAMUnblockUserTimeout;
         AV60Repository.gxTpr_Userremembermetype = AV85UserRememberMeType;
         AV60Repository.gxTpr_Userremembermetimeout = AV84UserRememberMeTimeOut;
         AV60Repository.gxTpr_Userrecoverypasswordkeytimeout = AV81UserRecoveryPasswordKeyTimeOut;
         AV60Repository.gxTpr_Userrecoverypasswordkeydailymaximum = AV82UserRecoveryPasswordKeyDailyMaximum;
         AV60Repository.gxTpr_Userrecoverypasswordkeymonthlymaximum = AV83UserRecoveryPasswordKeyMonthlyMaximum;
         AV60Repository.gxTpr_Gamremotelogoutbehavior = AV55LogoutBehavior;
         AV60Repository.gxTpr_Minimumamountcharactersinlogin = AV56MinimumAmountCharactersInLogin;
         AV60Repository.gxTpr_Loginattemptstolockuser = AV54LoginAttemptsToLockUser;
         AV60Repository.gxTpr_Loginattemptstolocksession = AV53LoginAttemptsToLockSession;
         AV60Repository.gxTpr_Usersessioncachetimeout = AV86UserSessionCacheTimeout;
         AV60Repository.gxTpr_Cachetimeout = AV61RepositoryCacheTimeout;
         AV60Repository.gxTpr_Securityadministratoremail = AV71SecurityAdministratorEmail;
         AV60Repository.gxTpr_Giveanonymoussession = AV43GiveAnonymousSession;
         AV60Repository.gxTpr_Canregisterusers = AV9CanRegisterUsers;
         AV60Repository.gxTpr_Useremailisunique = AV79UserEmailisUnique;
         AV60Repository.gxTpr_Defaultroleid = AV11DefaultRoleId;
         AV60Repository.gxTpr_Defaultsecuritypolicyid = AV12DefaultSecurityPolicyId;
         AV60Repository.gxTpr_Enableworkingasgammanagerrepository = AV35EnableWorkingAsGAMManagerRepo;
         AV60Repository.gxTpr_Enabletracing = (short)(Math.Round(NumberUtil.Val( AV33EnableTracing, "."), 18, MidpointRounding.ToEven));
         AV60Repository.gxTpr_Allowoauthaccess = AV5AllowOauthAccess;
         AV60Repository.gxTpr_Sessionexpiresonipchange = AV76SessionExpiresOnIPChange;
         AV60Repository.gxTpr_Integratedsecuritybydomainenable = AV51IntSecByDomainEnable;
         AV60Repository.gxTpr_Integratedsecuritybydomainmode = AV49IntSecByDomainMode;
         AV60Repository.gxTpr_Integratedsecuritybydomainjwtsecret = AV48IntSecByDomainJWTSecret;
         AV60Repository.gxTpr_Integratedsecuritybydomainencryptionkey = AV47IntSecByDomainEncryptionKey;
         AV60Repository.gxTpr_Totpsecretkeylength = AV50TOTPSecretKeyLength;
         AV60Repository.gxTpr_Requiredpassword = AV68RequiredPassword;
         AV60Repository.gxTpr_Requiredemail = AV64RequiredEmail;
         AV60Repository.gxTpr_Requiredfirstname = AV65RequiredFirstName;
         AV60Repository.gxTpr_Requiredlastname = AV67RequiredLastName;
         AV60Repository.gxTpr_Requiredbirthday = AV63RequiredBirthday;
         AV60Repository.gxTpr_Requiredgender = AV66RequiredGender;
         AV60Repository.gxTpr_Email.gxTpr_Serverhost = AV28EmailServerHost;
         AV60Repository.gxTpr_Email.gxTpr_Serverport = AV29EmailServerPort;
         AV60Repository.gxTpr_Email.gxTpr_Serversecure = AV30EmailServerSecure;
         AV60Repository.gxTpr_Email.gxTpr_Servertimeout = AV31EmailServerTimeout;
         AV60Repository.gxTpr_Email.gxTpr_Serverusesauthentication = AV32EmailServerUsesAuthentication;
         AV60Repository.gxTpr_Email.gxTpr_Serversenderaddress = AV74ServerSenderAddress;
         AV60Repository.gxTpr_Email.gxTpr_Serversendername = AV75ServerSenderName;
         if ( AV32EmailServerUsesAuthentication )
         {
            AV60Repository.gxTpr_Email.gxTpr_Serverauthenticationusername = AV26EmailServerAuthenticationUsername;
            AV60Repository.gxTpr_Email.gxTpr_Serverauthenticationuserpassword = AV27EmailServerAuthenticationUserPassword;
         }
         AV60Repository.gxTpr_Email.gxTpr_Sendemailwhenuseractivateaccount = AV23EmailServer_SendEmailWhenUserActivateAccount;
         if ( AV23EmailServer_SendEmailWhenUserActivateAccount )
         {
            AV60Repository.gxTpr_Email.gxTpr_Subjectwhenuseractivateaccount = AV19EmailServer_EmailSubjectWhenUserActivateAccount;
            AV60Repository.gxTpr_Email.gxTpr_Bodywhenuseractivateaccount = AV15EmailServer_EmailBodyWhenUserActivateAccount;
         }
         AV60Repository.gxTpr_Email.gxTpr_Sendemailwhenuserchangepassword = AV25EmailServer_SendEmailWhenUserChangePassword;
         if ( AV25EmailServer_SendEmailWhenUserChangePassword )
         {
            AV60Repository.gxTpr_Email.gxTpr_Subjectwhenuserchangepassword = AV21EmailServer_EmailSubjectWhenUserChangePassword;
            AV60Repository.gxTpr_Email.gxTpr_Bodywhenuserchangepassword = AV17EmailServer_EmailBodyWhenUserChangePassword;
         }
         AV60Repository.gxTpr_Email.gxTpr_Sendemailwhenuserchangeemail = AV24EmailServer_SendEmailWhenUserChangeEmail;
         if ( AV24EmailServer_SendEmailWhenUserChangeEmail )
         {
            AV60Repository.gxTpr_Email.gxTpr_Subjectwhenuserchangeemail = AV20EmailServer_EmailSubjectWhenUserChangeEmail;
            AV60Repository.gxTpr_Email.gxTpr_Bodywhenuserchangeemail = AV16EmailServer_EmailBodyWhenUserChangeEmail;
         }
         AV60Repository.gxTpr_Email.gxTpr_Sendemailtorecoveruserpassword = AV22EmailServer_SendEmailForRecoveryPassword;
         if ( AV22EmailServer_SendEmailForRecoveryPassword )
         {
            AV60Repository.gxTpr_Email.gxTpr_Subjecttorecoveruserpassword = AV18EmailServer_EmailSubjectForRecoveryPassword;
            AV60Repository.gxTpr_Email.gxTpr_Bodytorecoveruserpassword = AV14EmailServer_EmailBodyForRecoveryPassword;
         }
         AV60Repository.save();
         if ( AV60Repository.success() )
         {
            context.CommitDataStores("gamrepositoryconfiguration",pr_default);
            GX_msglist.addItem(context.GetMessage( context.GetMessage( "GXM_sucupdated", ""), ""));
         }
         else
         {
            AV37Errors = AV60Repository.geterrors();
            AV94GXV5 = 1;
            while ( AV94GXV5 <= AV37Errors.Count )
            {
               AV36Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV37Errors.Item(AV94GXV5));
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV36Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV36Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
               AV94GXV5 = (int)(AV94GXV5+1);
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV60Repository", AV60Repository);
      }

      protected void E131X2( )
      {
         /* Useridentification_Controlvaluechanged Routine */
         returnInSub = false;
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E141X2( )
      {
         /* Emailserverusesauthentication_Click Routine */
         returnInSub = false;
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E151X2( )
      {
         /* Emailserver_sendemailwhenuseractivateaccount_Click Routine */
         returnInSub = false;
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E161X2( )
      {
         /* Emailserver_sendemailwhenuserchangepassword_Click Routine */
         returnInSub = false;
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E171X2( )
      {
         /* Emailserver_sendemailwhenuserchangeemail_Click Routine */
         returnInSub = false;
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void E181X2( )
      {
         /* Emailserver_sendemailforrecoverypassword_Click Routine */
         returnInSub = false;
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void nextLoad( )
      {
      }

      protected void E191X2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV45Id = Convert.ToInt64(getParm(obj,0));
         AssignAttri("", false, "AV45Id", StringUtil.LTrimStr( (decimal)(AV45Id), 12, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV45Id), "ZZZZZZZZZZZ9"), context));
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
         PA1X2( ) ;
         WS1X2( ) ;
         WE1X2( ) ;
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
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217023820", true, true);
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
         context.AddJavascriptSource("gamrepositoryconfiguration.js", "?20241217023825", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManager.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/json2005.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/rsh/rsh.js", "", false, true);
         context.AddJavascriptSource("shared/HistoryManager/HistoryManagerCreate.js", "", false, true);
         context.AddJavascriptSource("Tab/TabRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavEnabletracing.Name = "vENABLETRACING";
         cmbavEnabletracing.WebTags = "";
         cmbavEnabletracing.addItem("0", context.GetMessage( "WWP_GAM_Tracing0", ""), 0);
         cmbavEnabletracing.addItem("1", context.GetMessage( "WWP_GAM_Tracing1", ""), 0);
         if ( cmbavEnabletracing.ItemCount > 0 )
         {
            AV33EnableTracing = cmbavEnabletracing.getValidValue(AV33EnableTracing);
            AssignAttri("", false, "AV33EnableTracing", AV33EnableTracing);
         }
         cmbavDefaultauthtypename.Name = "vDEFAULTAUTHTYPENAME";
         cmbavDefaultauthtypename.WebTags = "";
         if ( cmbavDefaultauthtypename.ItemCount > 0 )
         {
            AV10DefaultAuthTypeName = cmbavDefaultauthtypename.getValidValue(AV10DefaultAuthTypeName);
            AssignAttri("", false, "AV10DefaultAuthTypeName", AV10DefaultAuthTypeName);
         }
         cmbavDefaultroleid.Name = "vDEFAULTROLEID";
         cmbavDefaultroleid.WebTags = "";
         if ( cmbavDefaultroleid.ItemCount > 0 )
         {
            AV11DefaultRoleId = (long)(Math.Round(NumberUtil.Val( cmbavDefaultroleid.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV11DefaultRoleId), 12, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV11DefaultRoleId", StringUtil.LTrimStr( (decimal)(AV11DefaultRoleId), 12, 0));
         }
         cmbavDefaultsecuritypolicyid.Name = "vDEFAULTSECURITYPOLICYID";
         cmbavDefaultsecuritypolicyid.WebTags = "";
         if ( cmbavDefaultsecuritypolicyid.ItemCount > 0 )
         {
            AV12DefaultSecurityPolicyId = (int)(Math.Round(NumberUtil.Val( cmbavDefaultsecuritypolicyid.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV12DefaultSecurityPolicyId), 9, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "AV12DefaultSecurityPolicyId", StringUtil.LTrimStr( (decimal)(AV12DefaultSecurityPolicyId), 9, 0));
         }
         chkavAllowoauthaccess.Name = "vALLOWOAUTHACCESS";
         chkavAllowoauthaccess.WebTags = "";
         chkavAllowoauthaccess.Caption = " ";
         AssignProp("", false, chkavAllowoauthaccess_Internalname, "TitleCaption", chkavAllowoauthaccess.Caption, true);
         chkavAllowoauthaccess.CheckedValue = "false";
         AV5AllowOauthAccess = StringUtil.StrToBool( StringUtil.BoolToStr( AV5AllowOauthAccess));
         AssignAttri("", false, "AV5AllowOauthAccess", AV5AllowOauthAccess);
         cmbavLogoutbehavior.Name = "vLOGOUTBEHAVIOR";
         cmbavLogoutbehavior.WebTags = "";
         cmbavLogoutbehavior.addItem("clionl", context.GetMessage( "WWP_GAM_ClientOnly", ""), 0);
         cmbavLogoutbehavior.addItem("cliip", context.GetMessage( "WWP_GAM_IdentityProviderAndClient", ""), 0);
         cmbavLogoutbehavior.addItem("all", context.GetMessage( "WWP_GAM_IdentityProviderAndAllClients", ""), 0);
         if ( cmbavLogoutbehavior.ItemCount > 0 )
         {
            AV55LogoutBehavior = cmbavLogoutbehavior.getValidValue(AV55LogoutBehavior);
            AssignAttri("", false, "AV55LogoutBehavior", AV55LogoutBehavior);
         }
         chkavEnableworkingasgammanagerrepo.Name = "vENABLEWORKINGASGAMMANAGERREPO";
         chkavEnableworkingasgammanagerrepo.WebTags = "";
         chkavEnableworkingasgammanagerrepo.Caption = " ";
         AssignProp("", false, chkavEnableworkingasgammanagerrepo_Internalname, "TitleCaption", chkavEnableworkingasgammanagerrepo.Caption, true);
         chkavEnableworkingasgammanagerrepo.CheckedValue = "false";
         AV35EnableWorkingAsGAMManagerRepo = StringUtil.StrToBool( StringUtil.BoolToStr( AV35EnableWorkingAsGAMManagerRepo));
         AssignAttri("", false, "AV35EnableWorkingAsGAMManagerRepo", AV35EnableWorkingAsGAMManagerRepo);
         cmbavUseridentification.Name = "vUSERIDENTIFICATION";
         cmbavUseridentification.WebTags = "";
         cmbavUseridentification.addItem("name", context.GetMessage( "WWP_GAM_Name", ""), 0);
         cmbavUseridentification.addItem("email", context.GetMessage( "WWP_GAM_Email", ""), 0);
         cmbavUseridentification.addItem("namema", context.GetMessage( "WWP_GAM_NameEmail", ""), 0);
         if ( cmbavUseridentification.ItemCount > 0 )
         {
            AV80UserIdentification = cmbavUseridentification.getValidValue(AV80UserIdentification);
            AssignAttri("", false, "AV80UserIdentification", AV80UserIdentification);
         }
         chkavUseremailisunique.Name = "vUSEREMAILISUNIQUE";
         chkavUseremailisunique.WebTags = "";
         chkavUseremailisunique.Caption = " ";
         AssignProp("", false, chkavUseremailisunique_Internalname, "TitleCaption", chkavUseremailisunique.Caption, true);
         chkavUseremailisunique.CheckedValue = "false";
         AV79UserEmailisUnique = StringUtil.StrToBool( StringUtil.BoolToStr( AV79UserEmailisUnique));
         AssignAttri("", false, "AV79UserEmailisUnique", AV79UserEmailisUnique);
         cmbavUseractivationmethod.Name = "vUSERACTIVATIONMETHOD";
         cmbavUseractivationmethod.WebTags = "";
         cmbavUseractivationmethod.addItem("A", context.GetMessage( "WWP_GAM_Automatic", ""), 0);
         cmbavUseractivationmethod.addItem("U", context.GetMessage( "WWP_GAM_User", ""), 0);
         cmbavUseractivationmethod.addItem("D", context.GetMessage( "WWP_GAM_Administrator", ""), 0);
         if ( cmbavUseractivationmethod.ItemCount > 0 )
         {
            AV77UserActivationMethod = cmbavUseractivationmethod.getValidValue(AV77UserActivationMethod);
            AssignAttri("", false, "AV77UserActivationMethod", AV77UserActivationMethod);
         }
         cmbavUserremembermetype.Name = "vUSERREMEMBERMETYPE";
         cmbavUserremembermetype.WebTags = "";
         cmbavUserremembermetype.addItem("None", context.GetMessage( "WWP_GAM_None", ""), 0);
         cmbavUserremembermetype.addItem("Login", context.GetMessage( "WWP_GAM_Login", ""), 0);
         cmbavUserremembermetype.addItem("Auth", context.GetMessage( "WWP_GAM_Authentication", ""), 0);
         cmbavUserremembermetype.addItem("Both", context.GetMessage( "WWP_GAM_Both", ""), 0);
         if ( cmbavUserremembermetype.ItemCount > 0 )
         {
            AV85UserRememberMeType = cmbavUserremembermetype.getValidValue(AV85UserRememberMeType);
            AssignAttri("", false, "AV85UserRememberMeType", AV85UserRememberMeType);
         }
         chkavRequiredemail.Name = "vREQUIREDEMAIL";
         chkavRequiredemail.WebTags = "";
         chkavRequiredemail.Caption = " ";
         AssignProp("", false, chkavRequiredemail_Internalname, "TitleCaption", chkavRequiredemail.Caption, true);
         chkavRequiredemail.CheckedValue = "false";
         AV64RequiredEmail = StringUtil.StrToBool( StringUtil.BoolToStr( AV64RequiredEmail));
         AssignAttri("", false, "AV64RequiredEmail", AV64RequiredEmail);
         chkavRequiredpassword.Name = "vREQUIREDPASSWORD";
         chkavRequiredpassword.WebTags = "";
         chkavRequiredpassword.Caption = " ";
         AssignProp("", false, chkavRequiredpassword_Internalname, "TitleCaption", chkavRequiredpassword.Caption, true);
         chkavRequiredpassword.CheckedValue = "false";
         AV68RequiredPassword = StringUtil.StrToBool( StringUtil.BoolToStr( AV68RequiredPassword));
         AssignAttri("", false, "AV68RequiredPassword", AV68RequiredPassword);
         chkavRequiredfirstname.Name = "vREQUIREDFIRSTNAME";
         chkavRequiredfirstname.WebTags = "";
         chkavRequiredfirstname.Caption = " ";
         AssignProp("", false, chkavRequiredfirstname_Internalname, "TitleCaption", chkavRequiredfirstname.Caption, true);
         chkavRequiredfirstname.CheckedValue = "false";
         AV65RequiredFirstName = StringUtil.StrToBool( StringUtil.BoolToStr( AV65RequiredFirstName));
         AssignAttri("", false, "AV65RequiredFirstName", AV65RequiredFirstName);
         chkavRequiredlastname.Name = "vREQUIREDLASTNAME";
         chkavRequiredlastname.WebTags = "";
         chkavRequiredlastname.Caption = " ";
         AssignProp("", false, chkavRequiredlastname_Internalname, "TitleCaption", chkavRequiredlastname.Caption, true);
         chkavRequiredlastname.CheckedValue = "false";
         AV67RequiredLastName = StringUtil.StrToBool( StringUtil.BoolToStr( AV67RequiredLastName));
         AssignAttri("", false, "AV67RequiredLastName", AV67RequiredLastName);
         chkavRequiredbirthday.Name = "vREQUIREDBIRTHDAY";
         chkavRequiredbirthday.WebTags = "";
         chkavRequiredbirthday.Caption = " ";
         AssignProp("", false, chkavRequiredbirthday_Internalname, "TitleCaption", chkavRequiredbirthday.Caption, true);
         chkavRequiredbirthday.CheckedValue = "false";
         AV63RequiredBirthday = StringUtil.StrToBool( StringUtil.BoolToStr( AV63RequiredBirthday));
         AssignAttri("", false, "AV63RequiredBirthday", AV63RequiredBirthday);
         chkavRequiredgender.Name = "vREQUIREDGENDER";
         chkavRequiredgender.WebTags = "";
         chkavRequiredgender.Caption = " ";
         AssignProp("", false, chkavRequiredgender_Internalname, "TitleCaption", chkavRequiredgender.Caption, true);
         chkavRequiredgender.CheckedValue = "false";
         AV66RequiredGender = StringUtil.StrToBool( StringUtil.BoolToStr( AV66RequiredGender));
         AssignAttri("", false, "AV66RequiredGender", AV66RequiredGender);
         cmbavGeneratesessionstatistics.Name = "vGENERATESESSIONSTATISTICS";
         cmbavGeneratesessionstatistics.WebTags = "";
         cmbavGeneratesessionstatistics.addItem("None", context.GetMessage( "WWP_GAM_None", ""), 0);
         cmbavGeneratesessionstatistics.addItem("Minimum", context.GetMessage( "WWP_GAM_MinimumOnlyAuthUsers", ""), 0);
         cmbavGeneratesessionstatistics.addItem("Detail", context.GetMessage( "WWP_GAM_DetailAuthAnonUsers", ""), 0);
         cmbavGeneratesessionstatistics.addItem("Full", context.GetMessage( "WWP_GAM_FullLogAuthAndAnonUsers", ""), 0);
         if ( cmbavGeneratesessionstatistics.ItemCount > 0 )
         {
            AV42GenerateSessionStatistics = cmbavGeneratesessionstatistics.getValidValue(AV42GenerateSessionStatistics);
            AssignAttri("", false, "AV42GenerateSessionStatistics", AV42GenerateSessionStatistics);
         }
         chkavGiveanonymoussession.Name = "vGIVEANONYMOUSSESSION";
         chkavGiveanonymoussession.WebTags = "";
         chkavGiveanonymoussession.Caption = "  ";
         AssignProp("", false, chkavGiveanonymoussession_Internalname, "TitleCaption", chkavGiveanonymoussession.Caption, true);
         chkavGiveanonymoussession.CheckedValue = "false";
         AV43GiveAnonymousSession = StringUtil.StrToBool( StringUtil.BoolToStr( AV43GiveAnonymousSession));
         AssignAttri("", false, "AV43GiveAnonymousSession", AV43GiveAnonymousSession);
         chkavSessionexpiresonipchange.Name = "vSESSIONEXPIRESONIPCHANGE";
         chkavSessionexpiresonipchange.WebTags = "";
         chkavSessionexpiresonipchange.Caption = " ";
         AssignProp("", false, chkavSessionexpiresonipchange_Internalname, "TitleCaption", chkavSessionexpiresonipchange.Caption, true);
         chkavSessionexpiresonipchange.CheckedValue = "false";
         AV76SessionExpiresOnIPChange = StringUtil.StrToBool( StringUtil.BoolToStr( AV76SessionExpiresOnIPChange));
         AssignAttri("", false, "AV76SessionExpiresOnIPChange", AV76SessionExpiresOnIPChange);
         chkavIntsecbydomainenable.Name = "vINTSECBYDOMAINENABLE";
         chkavIntsecbydomainenable.WebTags = "";
         chkavIntsecbydomainenable.Caption = context.GetMessage( "WWP_GAM_EnableIntegratedSecurityByDomain", "");
         AssignProp("", false, chkavIntsecbydomainenable_Internalname, "TitleCaption", chkavIntsecbydomainenable.Caption, true);
         chkavIntsecbydomainenable.CheckedValue = "false";
         AV51IntSecByDomainEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV51IntSecByDomainEnable));
         AssignAttri("", false, "AV51IntSecByDomainEnable", AV51IntSecByDomainEnable);
         cmbavIntsecbydomainmode.Name = "vINTSECBYDOMAINMODE";
         cmbavIntsecbydomainmode.WebTags = "";
         cmbavIntsecbydomainmode.addItem("server", context.GetMessage( "WWP_GAM_Server", ""), 0);
         cmbavIntsecbydomainmode.addItem("client", context.GetMessage( "WWP_GAM_Client", ""), 0);
         if ( cmbavIntsecbydomainmode.ItemCount > 0 )
         {
            AV49IntSecByDomainMode = cmbavIntsecbydomainmode.getValidValue(AV49IntSecByDomainMode);
            AssignAttri("", false, "AV49IntSecByDomainMode", AV49IntSecByDomainMode);
         }
         chkavEmailserversecure.Name = "vEMAILSERVERSECURE";
         chkavEmailserversecure.WebTags = "";
         chkavEmailserversecure.Caption = " ";
         AssignProp("", false, chkavEmailserversecure_Internalname, "TitleCaption", chkavEmailserversecure.Caption, true);
         chkavEmailserversecure.CheckedValue = "false";
         AV30EmailServerSecure = StringUtil.StrToBool( StringUtil.BoolToStr( AV30EmailServerSecure));
         AssignAttri("", false, "AV30EmailServerSecure", AV30EmailServerSecure);
         chkavEmailserverusesauthentication.Name = "vEMAILSERVERUSESAUTHENTICATION";
         chkavEmailserverusesauthentication.WebTags = "";
         chkavEmailserverusesauthentication.Caption = " ";
         AssignProp("", false, chkavEmailserverusesauthentication_Internalname, "TitleCaption", chkavEmailserverusesauthentication.Caption, true);
         chkavEmailserverusesauthentication.CheckedValue = "false";
         AV32EmailServerUsesAuthentication = StringUtil.StrToBool( StringUtil.BoolToStr( AV32EmailServerUsesAuthentication));
         AssignAttri("", false, "AV32EmailServerUsesAuthentication", AV32EmailServerUsesAuthentication);
         chkavEmailserver_sendemailwhenuseractivateaccount.Name = "vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT";
         chkavEmailserver_sendemailwhenuseractivateaccount.WebTags = "";
         chkavEmailserver_sendemailwhenuseractivateaccount.Caption = " ";
         AssignProp("", false, chkavEmailserver_sendemailwhenuseractivateaccount_Internalname, "TitleCaption", chkavEmailserver_sendemailwhenuseractivateaccount.Caption, true);
         chkavEmailserver_sendemailwhenuseractivateaccount.CheckedValue = "false";
         AV23EmailServer_SendEmailWhenUserActivateAccount = StringUtil.StrToBool( StringUtil.BoolToStr( AV23EmailServer_SendEmailWhenUserActivateAccount));
         AssignAttri("", false, "AV23EmailServer_SendEmailWhenUserActivateAccount", AV23EmailServer_SendEmailWhenUserActivateAccount);
         chkavEmailserver_sendemailwhenuserchangepassword.Name = "vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD";
         chkavEmailserver_sendemailwhenuserchangepassword.WebTags = "";
         chkavEmailserver_sendemailwhenuserchangepassword.Caption = " ";
         AssignProp("", false, chkavEmailserver_sendemailwhenuserchangepassword_Internalname, "TitleCaption", chkavEmailserver_sendemailwhenuserchangepassword.Caption, true);
         chkavEmailserver_sendemailwhenuserchangepassword.CheckedValue = "false";
         AV25EmailServer_SendEmailWhenUserChangePassword = StringUtil.StrToBool( StringUtil.BoolToStr( AV25EmailServer_SendEmailWhenUserChangePassword));
         AssignAttri("", false, "AV25EmailServer_SendEmailWhenUserChangePassword", AV25EmailServer_SendEmailWhenUserChangePassword);
         chkavEmailserver_sendemailwhenuserchangeemail.Name = "vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL";
         chkavEmailserver_sendemailwhenuserchangeemail.WebTags = "";
         chkavEmailserver_sendemailwhenuserchangeemail.Caption = " ";
         AssignProp("", false, chkavEmailserver_sendemailwhenuserchangeemail_Internalname, "TitleCaption", chkavEmailserver_sendemailwhenuserchangeemail.Caption, true);
         chkavEmailserver_sendemailwhenuserchangeemail.CheckedValue = "false";
         AV24EmailServer_SendEmailWhenUserChangeEmail = StringUtil.StrToBool( StringUtil.BoolToStr( AV24EmailServer_SendEmailWhenUserChangeEmail));
         AssignAttri("", false, "AV24EmailServer_SendEmailWhenUserChangeEmail", AV24EmailServer_SendEmailWhenUserChangeEmail);
         chkavEmailserver_sendemailforrecoverypassword.Name = "vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD";
         chkavEmailserver_sendemailforrecoverypassword.WebTags = "";
         chkavEmailserver_sendemailforrecoverypassword.Caption = " ";
         AssignProp("", false, chkavEmailserver_sendemailforrecoverypassword_Internalname, "TitleCaption", chkavEmailserver_sendemailforrecoverypassword.Caption, true);
         chkavEmailserver_sendemailforrecoverypassword.CheckedValue = "false";
         AV22EmailServer_SendEmailForRecoveryPassword = StringUtil.StrToBool( StringUtil.BoolToStr( AV22EmailServer_SendEmailForRecoveryPassword));
         AssignAttri("", false, "AV22EmailServer_SendEmailForRecoveryPassword", AV22EmailServer_SendEmailForRecoveryPassword);
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblGeneral_title_Internalname = "GENERAL_TITLE";
         edtavRepoid_Internalname = "vREPOID";
         edtavGuid_Internalname = "vGUID";
         edtavNamespace_Internalname = "vNAMESPACE";
         edtavName_Internalname = "vNAME";
         edtavDsc_Internalname = "vDSC";
         cmbavEnabletracing_Internalname = "vENABLETRACING";
         edtavAuthenticationmasterrepository_Internalname = "vAUTHENTICATIONMASTERREPOSITORY";
         divAuthenticationmasterrepository_cell_Internalname = "AUTHENTICATIONMASTERREPOSITORY_CELL";
         cmbavDefaultauthtypename_Internalname = "vDEFAULTAUTHTYPENAME";
         divDefaultauthtypename_cell_Internalname = "DEFAULTAUTHTYPENAME_CELL";
         cmbavDefaultroleid_Internalname = "vDEFAULTROLEID";
         divDefaultroleid_cell_Internalname = "DEFAULTROLEID_CELL";
         cmbavDefaultsecuritypolicyid_Internalname = "vDEFAULTSECURITYPOLICYID";
         divDefaultsecuritypolicyid_cell_Internalname = "DEFAULTSECURITYPOLICYID_CELL";
         chkavAllowoauthaccess_Internalname = "vALLOWOAUTHACCESS";
         cmbavLogoutbehavior_Internalname = "vLOGOUTBEHAVIOR";
         chkavEnableworkingasgammanagerrepo_Internalname = "vENABLEWORKINGASGAMMANAGERREPO";
         divEnableworkingasgammanagerrepo_cell_Internalname = "ENABLEWORKINGASGAMMANAGERREPO_CELL";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         lblUsers_title_Internalname = "USERS_TITLE";
         cmbavUseridentification_Internalname = "vUSERIDENTIFICATION";
         chkavUseremailisunique_Internalname = "vUSEREMAILISUNIQUE";
         divUseremailisunique_cell_Internalname = "USEREMAILISUNIQUE_CELL";
         cmbavUseractivationmethod_Internalname = "vUSERACTIVATIONMETHOD";
         edtavUserautomaticactivationtimeout_Internalname = "vUSERAUTOMATICACTIVATIONTIMEOUT";
         edtavUserrecoverypasswordkeytimeout_Internalname = "vUSERRECOVERYPASSWORDKEYTIMEOUT";
         edtavUserrecoverypasswordkeydailymaximum_Internalname = "vUSERRECOVERYPASSWORDKEYDAILYMAXIMUM";
         edtavUserrecoverypasswordkeymonthlymaximum_Internalname = "vUSERRECOVERYPASSWORDKEYMONTHLYMAXIMUM";
         edtavLoginattemptstolockuser_Internalname = "vLOGINATTEMPTSTOLOCKUSER";
         edtavGamunblockusertimeout_Internalname = "vGAMUNBLOCKUSERTIMEOUT";
         cmbavUserremembermetype_Internalname = "vUSERREMEMBERMETYPE";
         edtavUserremembermetimeout_Internalname = "vUSERREMEMBERMETIMEOUT";
         edtavTotpsecretkeylength_Internalname = "vTOTPSECRETKEYLENGTH";
         chkavRequiredemail_Internalname = "vREQUIREDEMAIL";
         divRequiredemail_cell_Internalname = "REQUIREDEMAIL_CELL";
         chkavRequiredpassword_Internalname = "vREQUIREDPASSWORD";
         chkavRequiredfirstname_Internalname = "vREQUIREDFIRSTNAME";
         chkavRequiredlastname_Internalname = "vREQUIREDLASTNAME";
         chkavRequiredbirthday_Internalname = "vREQUIREDBIRTHDAY";
         chkavRequiredgender_Internalname = "vREQUIREDGENDER";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         lblSession_title_Internalname = "SESSION_TITLE";
         cmbavGeneratesessionstatistics_Internalname = "vGENERATESESSIONSTATISTICS";
         edtavUsersessioncachetimeout_Internalname = "vUSERSESSIONCACHETIMEOUT";
         chkavGiveanonymoussession_Internalname = "vGIVEANONYMOUSSESSION";
         chkavSessionexpiresonipchange_Internalname = "vSESSIONEXPIRESONIPCHANGE";
         edtavLoginattemptstolocksession_Internalname = "vLOGINATTEMPTSTOLOCKSESSION";
         edtavMinimumamountcharactersinlogin_Internalname = "vMINIMUMAMOUNTCHARACTERSINLOGIN";
         edtavRepositorycachetimeout_Internalname = "vREPOSITORYCACHETIMEOUT";
         chkavIntsecbydomainenable_Internalname = "vINTSECBYDOMAINENABLE";
         cmbavIntsecbydomainmode_Internalname = "vINTSECBYDOMAINMODE";
         edtavIntsecbydomainjwtsecret_Internalname = "vINTSECBYDOMAINJWTSECRET";
         edtavIntsecbydomainencryptionkey_Internalname = "vINTSECBYDOMAINENCRYPTIONKEY";
         divTblintsecbydomain_Internalname = "TBLINTSECBYDOMAIN";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         lblTabemail_title_Internalname = "TABEMAIL_TITLE";
         edtavEmailserverhost_Internalname = "vEMAILSERVERHOST";
         edtavEmailserverport_Internalname = "vEMAILSERVERPORT";
         edtavEmailservertimeout_Internalname = "vEMAILSERVERTIMEOUT";
         chkavEmailserversecure_Internalname = "vEMAILSERVERSECURE";
         edtavServersenderaddress_Internalname = "vSERVERSENDERADDRESS";
         edtavServersendername_Internalname = "vSERVERSENDERNAME";
         chkavEmailserverusesauthentication_Internalname = "vEMAILSERVERUSESAUTHENTICATION";
         edtavEmailserverauthenticationusername_Internalname = "vEMAILSERVERAUTHENTICATIONUSERNAME";
         divEmailserverauthenticationusername_cell_Internalname = "EMAILSERVERAUTHENTICATIONUSERNAME_CELL";
         edtavEmailserverauthenticationuserpassword_Internalname = "vEMAILSERVERAUTHENTICATIONUSERPASSWORD";
         divEmailserverauthenticationuserpassword_cell_Internalname = "EMAILSERVERAUTHENTICATIONUSERPASSWORD_CELL";
         chkavEmailserver_sendemailwhenuseractivateaccount_Internalname = "vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT";
         edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname = "vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT";
         divEmailserver_emailsubjectwhenuseractivateaccount_cell_Internalname = "EMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT_CELL";
         edtavEmailserver_emailbodywhenuseractivateaccount_Internalname = "vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT";
         divEmailserver_emailbodywhenuseractivateaccount_cell_Internalname = "EMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT_CELL";
         chkavEmailserver_sendemailwhenuserchangepassword_Internalname = "vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD";
         edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname = "vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD";
         divEmailserver_emailsubjectwhenuserchangepassword_cell_Internalname = "EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD_CELL";
         edtavEmailserver_emailbodywhenuserchangepassword_Internalname = "vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD";
         divEmailserver_emailbodywhenuserchangepassword_cell_Internalname = "EMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD_CELL";
         chkavEmailserver_sendemailwhenuserchangeemail_Internalname = "vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL";
         edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname = "vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL";
         divEmailserver_emailsubjectwhenuserchangeemail_cell_Internalname = "EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL_CELL";
         edtavEmailserver_emailbodywhenuserchangeemail_Internalname = "vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL";
         divEmailserver_emailbodywhenuserchangeemail_cell_Internalname = "EMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL_CELL";
         chkavEmailserver_sendemailforrecoverypassword_Internalname = "vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD";
         edtavEmailserver_emailsubjectforrecoverypassword_Internalname = "vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD";
         divEmailserver_emailsubjectforrecoverypassword_cell_Internalname = "EMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD_CELL";
         edtavEmailserver_emailbodyforrecoverypassword_Internalname = "vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD";
         divEmailserver_emailbodyforrecoverypassword_cell_Internalname = "EMAILSERVER_EMAILBODYFORRECOVERYPASSWORD_CELL";
         divTableattributes_Internalname = "TABLEATTRIBUTES";
         Gxuitabspanel_tabs_Internalname = "GXUITABSPANEL_TABS";
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
         chkavEmailserver_sendemailforrecoverypassword.Caption = " ";
         chkavEmailserver_sendemailwhenuserchangeemail.Caption = " ";
         chkavEmailserver_sendemailwhenuserchangepassword.Caption = " ";
         chkavEmailserver_sendemailwhenuseractivateaccount.Caption = " ";
         chkavEmailserverusesauthentication.Caption = " ";
         chkavEmailserversecure.Caption = " ";
         chkavIntsecbydomainenable.Caption = context.GetMessage( "WWP_GAM_EnableIntegratedSecurityByDomain", "");
         chkavSessionexpiresonipchange.Caption = " ";
         chkavGiveanonymoussession.Caption = "  ";
         chkavRequiredgender.Caption = " ";
         chkavRequiredbirthday.Caption = " ";
         chkavRequiredlastname.Caption = " ";
         chkavRequiredfirstname.Caption = " ";
         chkavRequiredpassword.Caption = " ";
         chkavRequiredemail.Caption = " ";
         chkavUseremailisunique.Caption = " ";
         chkavEnableworkingasgammanagerrepo.Caption = " ";
         chkavAllowoauthaccess.Caption = " ";
         edtavEmailserver_emailbodyforrecoverypassword_Enabled = 1;
         edtavEmailserver_emailbodyforrecoverypassword_Visible = 1;
         divEmailserver_emailbodyforrecoverypassword_cell_Class = "col-xs-12";
         edtavEmailserver_emailsubjectforrecoverypassword_Jsonclick = "";
         edtavEmailserver_emailsubjectforrecoverypassword_Enabled = 1;
         edtavEmailserver_emailsubjectforrecoverypassword_Visible = 1;
         divEmailserver_emailsubjectforrecoverypassword_cell_Class = "col-xs-12";
         chkavEmailserver_sendemailforrecoverypassword.Enabled = 1;
         edtavEmailserver_emailbodywhenuserchangeemail_Enabled = 1;
         edtavEmailserver_emailbodywhenuserchangeemail_Visible = 1;
         divEmailserver_emailbodywhenuserchangeemail_cell_Class = "col-xs-12";
         edtavEmailserver_emailsubjectwhenuserchangeemail_Jsonclick = "";
         edtavEmailserver_emailsubjectwhenuserchangeemail_Enabled = 1;
         edtavEmailserver_emailsubjectwhenuserchangeemail_Visible = 1;
         divEmailserver_emailsubjectwhenuserchangeemail_cell_Class = "col-xs-12";
         chkavEmailserver_sendemailwhenuserchangeemail.Enabled = 1;
         edtavEmailserver_emailbodywhenuserchangepassword_Enabled = 1;
         edtavEmailserver_emailbodywhenuserchangepassword_Visible = 1;
         divEmailserver_emailbodywhenuserchangepassword_cell_Class = "col-xs-12";
         edtavEmailserver_emailsubjectwhenuserchangepassword_Jsonclick = "";
         edtavEmailserver_emailsubjectwhenuserchangepassword_Enabled = 1;
         edtavEmailserver_emailsubjectwhenuserchangepassword_Visible = 1;
         divEmailserver_emailsubjectwhenuserchangepassword_cell_Class = "col-xs-12";
         chkavEmailserver_sendemailwhenuserchangepassword.Enabled = 1;
         edtavEmailserver_emailbodywhenuseractivateaccount_Enabled = 1;
         edtavEmailserver_emailbodywhenuseractivateaccount_Visible = 1;
         divEmailserver_emailbodywhenuseractivateaccount_cell_Class = "col-xs-12";
         edtavEmailserver_emailsubjectwhenuseractivateaccount_Jsonclick = "";
         edtavEmailserver_emailsubjectwhenuseractivateaccount_Enabled = 1;
         edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible = 1;
         divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class = "col-xs-12";
         chkavEmailserver_sendemailwhenuseractivateaccount.Enabled = 1;
         edtavEmailserverauthenticationuserpassword_Jsonclick = "";
         edtavEmailserverauthenticationuserpassword_Enabled = 1;
         edtavEmailserverauthenticationuserpassword_Visible = 1;
         divEmailserverauthenticationuserpassword_cell_Class = "col-xs-12 col-sm-6";
         edtavEmailserverauthenticationusername_Jsonclick = "";
         edtavEmailserverauthenticationusername_Enabled = 1;
         edtavEmailserverauthenticationusername_Visible = 1;
         divEmailserverauthenticationusername_cell_Class = "col-xs-12 col-sm-6";
         chkavEmailserverusesauthentication.Enabled = 1;
         edtavServersendername_Jsonclick = "";
         edtavServersendername_Enabled = 1;
         edtavServersenderaddress_Jsonclick = "";
         edtavServersenderaddress_Enabled = 1;
         chkavEmailserversecure.Enabled = 1;
         edtavEmailservertimeout_Jsonclick = "";
         edtavEmailservertimeout_Enabled = 1;
         edtavEmailserverport_Jsonclick = "";
         edtavEmailserverport_Enabled = 1;
         edtavEmailserverhost_Jsonclick = "";
         edtavEmailserverhost_Enabled = 1;
         edtavIntsecbydomainencryptionkey_Jsonclick = "";
         edtavIntsecbydomainencryptionkey_Enabled = 1;
         edtavIntsecbydomainjwtsecret_Jsonclick = "";
         edtavIntsecbydomainjwtsecret_Enabled = 1;
         cmbavIntsecbydomainmode_Jsonclick = "";
         cmbavIntsecbydomainmode.Enabled = 1;
         divTblintsecbydomain_Visible = 1;
         chkavIntsecbydomainenable.Enabled = 1;
         edtavRepositorycachetimeout_Jsonclick = "";
         edtavRepositorycachetimeout_Enabled = 1;
         edtavMinimumamountcharactersinlogin_Jsonclick = "";
         edtavMinimumamountcharactersinlogin_Enabled = 1;
         edtavLoginattemptstolocksession_Jsonclick = "";
         edtavLoginattemptstolocksession_Enabled = 1;
         chkavSessionexpiresonipchange.Enabled = 1;
         chkavGiveanonymoussession.Enabled = 1;
         edtavUsersessioncachetimeout_Jsonclick = "";
         edtavUsersessioncachetimeout_Enabled = 1;
         cmbavGeneratesessionstatistics_Jsonclick = "";
         cmbavGeneratesessionstatistics.Enabled = 1;
         chkavRequiredgender.Enabled = 1;
         chkavRequiredbirthday.Enabled = 1;
         chkavRequiredlastname.Enabled = 1;
         chkavRequiredfirstname.Enabled = 1;
         chkavRequiredpassword.Enabled = 1;
         chkavRequiredemail.Enabled = 1;
         chkavRequiredemail.Visible = 1;
         divRequiredemail_cell_Class = "col-xs-12 col-sm-6";
         edtavTotpsecretkeylength_Jsonclick = "";
         edtavTotpsecretkeylength_Enabled = 1;
         edtavUserremembermetimeout_Jsonclick = "";
         edtavUserremembermetimeout_Enabled = 1;
         cmbavUserremembermetype_Jsonclick = "";
         cmbavUserremembermetype.Enabled = 1;
         edtavGamunblockusertimeout_Jsonclick = "";
         edtavGamunblockusertimeout_Enabled = 1;
         edtavLoginattemptstolockuser_Jsonclick = "";
         edtavLoginattemptstolockuser_Enabled = 1;
         edtavUserrecoverypasswordkeymonthlymaximum_Jsonclick = "";
         edtavUserrecoverypasswordkeymonthlymaximum_Enabled = 1;
         edtavUserrecoverypasswordkeydailymaximum_Jsonclick = "";
         edtavUserrecoverypasswordkeydailymaximum_Enabled = 1;
         edtavUserrecoverypasswordkeytimeout_Jsonclick = "";
         edtavUserrecoverypasswordkeytimeout_Enabled = 1;
         edtavUserautomaticactivationtimeout_Jsonclick = "";
         edtavUserautomaticactivationtimeout_Enabled = 1;
         cmbavUseractivationmethod_Jsonclick = "";
         cmbavUseractivationmethod.Enabled = 1;
         chkavUseremailisunique.Enabled = 1;
         chkavUseremailisunique.Visible = 1;
         divUseremailisunique_cell_Class = "col-xs-12 col-sm-6";
         cmbavUseridentification_Jsonclick = "";
         cmbavUseridentification.Enabled = 1;
         chkavEnableworkingasgammanagerrepo.Enabled = 1;
         chkavEnableworkingasgammanagerrepo.Visible = 1;
         divEnableworkingasgammanagerrepo_cell_Class = "col-xs-12 col-sm-6";
         cmbavLogoutbehavior_Jsonclick = "";
         cmbavLogoutbehavior.Enabled = 1;
         chkavAllowoauthaccess.Enabled = 1;
         cmbavDefaultsecuritypolicyid_Jsonclick = "";
         cmbavDefaultsecuritypolicyid.Enabled = 1;
         cmbavDefaultsecuritypolicyid.Visible = 1;
         divDefaultsecuritypolicyid_cell_Class = "col-xs-12 col-sm-6";
         cmbavDefaultroleid_Jsonclick = "";
         cmbavDefaultroleid.Enabled = 1;
         cmbavDefaultroleid.Visible = 1;
         divDefaultroleid_cell_Class = "col-xs-12 col-sm-6";
         cmbavDefaultauthtypename_Jsonclick = "";
         cmbavDefaultauthtypename.Enabled = 1;
         cmbavDefaultauthtypename.Visible = 1;
         divDefaultauthtypename_cell_Class = "col-xs-12 col-sm-6";
         edtavAuthenticationmasterrepository_Jsonclick = "";
         edtavAuthenticationmasterrepository_Enabled = 1;
         edtavAuthenticationmasterrepository_Visible = 1;
         divAuthenticationmasterrepository_cell_Class = "col-xs-12 col-sm-6";
         cmbavEnabletracing_Jsonclick = "";
         cmbavEnabletracing.Enabled = 1;
         edtavDsc_Jsonclick = "";
         edtavDsc_Enabled = 1;
         edtavName_Jsonclick = "";
         edtavName_Enabled = 1;
         edtavNamespace_Jsonclick = "";
         edtavNamespace_Enabled = 1;
         edtavGuid_Jsonclick = "";
         edtavGuid_Enabled = 1;
         edtavRepoid_Jsonclick = "";
         edtavRepoid_Enabled = 1;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         Gxuitabspanel_tabs_Historymanagement = Convert.ToBoolean( 0);
         Gxuitabspanel_tabs_Class = "Tab";
         Gxuitabspanel_tabs_Pagecount = 4;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Repository configuration ", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV5AllowOauthAccess","fld":"vALLOWOAUTHACCESS"},{"av":"AV35EnableWorkingAsGAMManagerRepo","fld":"vENABLEWORKINGASGAMMANAGERREPO"},{"av":"AV79UserEmailisUnique","fld":"vUSEREMAILISUNIQUE"},{"av":"AV64RequiredEmail","fld":"vREQUIREDEMAIL"},{"av":"AV68RequiredPassword","fld":"vREQUIREDPASSWORD"},{"av":"AV65RequiredFirstName","fld":"vREQUIREDFIRSTNAME"},{"av":"AV67RequiredLastName","fld":"vREQUIREDLASTNAME"},{"av":"AV63RequiredBirthday","fld":"vREQUIREDBIRTHDAY"},{"av":"AV66RequiredGender","fld":"vREQUIREDGENDER"},{"av":"AV43GiveAnonymousSession","fld":"vGIVEANONYMOUSSESSION"},{"av":"AV76SessionExpiresOnIPChange","fld":"vSESSIONEXPIRESONIPCHANGE"},{"av":"AV51IntSecByDomainEnable","fld":"vINTSECBYDOMAINENABLE"},{"av":"AV30EmailServerSecure","fld":"vEMAILSERVERSECURE"},{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"AV71SecurityAdministratorEmail","fld":"vSECURITYADMINISTRATOREMAIL","hsh":true},{"av":"AV9CanRegisterUsers","fld":"vCANREGISTERUSERS","hsh":true},{"av":"AV45Id","fld":"vID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV59RepoId","fld":"vREPOID","pic":"ZZZZZZZZZZZ9","hsh":true}]}""");
         setEventMetadata("ENTER","""{"handler":"E121X2","iparms":[{"av":"AV59RepoId","fld":"vREPOID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV57Name","fld":"vNAME"},{"av":"AV13Dsc","fld":"vDSC"},{"av":"cmbavDefaultauthtypename"},{"av":"AV10DefaultAuthTypeName","fld":"vDEFAULTAUTHTYPENAME"},{"av":"cmbavUseridentification"},{"av":"AV80UserIdentification","fld":"vUSERIDENTIFICATION"},{"av":"cmbavGeneratesessionstatistics"},{"av":"AV42GenerateSessionStatistics","fld":"vGENERATESESSIONSTATISTICS"},{"av":"cmbavUseractivationmethod"},{"av":"AV77UserActivationMethod","fld":"vUSERACTIVATIONMETHOD"},{"av":"AV78UserAutomaticActivationTimeout","fld":"vUSERAUTOMATICACTIVATIONTIMEOUT","pic":"ZZZ9"},{"av":"AV41GAMUnblockUserTimeout","fld":"vGAMUNBLOCKUSERTIMEOUT","pic":"ZZZ9"},{"av":"cmbavUserremembermetype"},{"av":"AV85UserRememberMeType","fld":"vUSERREMEMBERMETYPE"},{"av":"AV84UserRememberMeTimeOut","fld":"vUSERREMEMBERMETIMEOUT","pic":"ZZZ9"},{"av":"AV81UserRecoveryPasswordKeyTimeOut","fld":"vUSERRECOVERYPASSWORDKEYTIMEOUT","pic":"ZZZ9"},{"av":"AV82UserRecoveryPasswordKeyDailyMaximum","fld":"vUSERRECOVERYPASSWORDKEYDAILYMAXIMUM","pic":"ZZZ9"},{"av":"AV83UserRecoveryPasswordKeyMonthlyMaximum","fld":"vUSERRECOVERYPASSWORDKEYMONTHLYMAXIMUM","pic":"ZZZ9"},{"av":"cmbavLogoutbehavior"},{"av":"AV55LogoutBehavior","fld":"vLOGOUTBEHAVIOR"},{"av":"AV56MinimumAmountCharactersInLogin","fld":"vMINIMUMAMOUNTCHARACTERSINLOGIN","pic":"Z9"},{"av":"AV54LoginAttemptsToLockUser","fld":"vLOGINATTEMPTSTOLOCKUSER","pic":"Z9"},{"av":"AV53LoginAttemptsToLockSession","fld":"vLOGINATTEMPTSTOLOCKSESSION","pic":"Z9"},{"av":"AV86UserSessionCacheTimeout","fld":"vUSERSESSIONCACHETIMEOUT","pic":"ZZZZZZZZ9"},{"av":"AV61RepositoryCacheTimeout","fld":"vREPOSITORYCACHETIMEOUT","pic":"ZZZZZZZZ9"},{"av":"AV71SecurityAdministratorEmail","fld":"vSECURITYADMINISTRATOREMAIL","hsh":true},{"av":"AV43GiveAnonymousSession","fld":"vGIVEANONYMOUSSESSION"},{"av":"AV9CanRegisterUsers","fld":"vCANREGISTERUSERS","hsh":true},{"av":"AV79UserEmailisUnique","fld":"vUSEREMAILISUNIQUE"},{"av":"cmbavDefaultroleid"},{"av":"AV11DefaultRoleId","fld":"vDEFAULTROLEID","pic":"ZZZZZZZZZZZ9"},{"av":"cmbavDefaultsecuritypolicyid"},{"av":"AV12DefaultSecurityPolicyId","fld":"vDEFAULTSECURITYPOLICYID","pic":"ZZZZZZZZ9"},{"av":"AV35EnableWorkingAsGAMManagerRepo","fld":"vENABLEWORKINGASGAMMANAGERREPO"},{"av":"cmbavEnabletracing"},{"av":"AV33EnableTracing","fld":"vENABLETRACING"},{"av":"AV5AllowOauthAccess","fld":"vALLOWOAUTHACCESS"},{"av":"AV76SessionExpiresOnIPChange","fld":"vSESSIONEXPIRESONIPCHANGE"},{"av":"AV51IntSecByDomainEnable","fld":"vINTSECBYDOMAINENABLE"},{"av":"cmbavIntsecbydomainmode"},{"av":"AV49IntSecByDomainMode","fld":"vINTSECBYDOMAINMODE"},{"av":"AV48IntSecByDomainJWTSecret","fld":"vINTSECBYDOMAINJWTSECRET"},{"av":"AV47IntSecByDomainEncryptionKey","fld":"vINTSECBYDOMAINENCRYPTIONKEY"},{"av":"AV50TOTPSecretKeyLength","fld":"vTOTPSECRETKEYLENGTH","pic":"ZZZZZZZZZZZ9"},{"av":"AV68RequiredPassword","fld":"vREQUIREDPASSWORD"},{"av":"AV64RequiredEmail","fld":"vREQUIREDEMAIL"},{"av":"AV65RequiredFirstName","fld":"vREQUIREDFIRSTNAME"},{"av":"AV67RequiredLastName","fld":"vREQUIREDLASTNAME"},{"av":"AV63RequiredBirthday","fld":"vREQUIREDBIRTHDAY"},{"av":"AV66RequiredGender","fld":"vREQUIREDGENDER"},{"av":"AV28EmailServerHost","fld":"vEMAILSERVERHOST"},{"av":"AV29EmailServerPort","fld":"vEMAILSERVERPORT","pic":"ZZZ9"},{"av":"AV30EmailServerSecure","fld":"vEMAILSERVERSECURE"},{"av":"AV31EmailServerTimeout","fld":"vEMAILSERVERTIMEOUT","pic":"ZZZ9"},{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV74ServerSenderAddress","fld":"vSERVERSENDERADDRESS"},{"av":"AV75ServerSenderName","fld":"vSERVERSENDERNAME"},{"av":"AV26EmailServerAuthenticationUsername","fld":"vEMAILSERVERAUTHENTICATIONUSERNAME"},{"av":"AV27EmailServerAuthenticationUserPassword","fld":"vEMAILSERVERAUTHENTICATIONUSERPASSWORD"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV19EmailServer_EmailSubjectWhenUserActivateAccount","fld":"vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT"},{"av":"AV15EmailServer_EmailBodyWhenUserActivateAccount","fld":"vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV21EmailServer_EmailSubjectWhenUserChangePassword","fld":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD"},{"av":"AV17EmailServer_EmailBodyWhenUserChangePassword","fld":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV20EmailServer_EmailSubjectWhenUserChangeEmail","fld":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL"},{"av":"AV16EmailServer_EmailBodyWhenUserChangeEmail","fld":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"AV18EmailServer_EmailSubjectForRecoveryPassword","fld":"vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD"},{"av":"AV14EmailServer_EmailBodyForRecoveryPassword","fld":"vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD"}]}""");
         setEventMetadata("VUSERIDENTIFICATION.CONTROLVALUECHANGED","""{"handler":"E131X2","iparms":[{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"cmbavUseridentification"},{"av":"AV80UserIdentification","fld":"vUSERIDENTIFICATION"}]""");
         setEventMetadata("VUSERIDENTIFICATION.CONTROLVALUECHANGED",""","oparms":[{"av":"edtavEmailserverauthenticationusername_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERNAME","prop":"Visible"},{"av":"divEmailserverauthenticationusername_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERNAME_CELL","prop":"Class"},{"av":"edtavEmailserverauthenticationuserpassword_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERPASSWORD","prop":"Visible"},{"av":"divEmailserverauthenticationuserpassword_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodyforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodyforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"chkavUseremailisunique.Visible","ctrl":"vUSEREMAILISUNIQUE","prop":"Visible"},{"av":"divUseremailisunique_cell_Class","ctrl":"USEREMAILISUNIQUE_CELL","prop":"Class"},{"av":"chkavRequiredemail.Visible","ctrl":"vREQUIREDEMAIL","prop":"Visible"},{"av":"divRequiredemail_cell_Class","ctrl":"REQUIREDEMAIL_CELL","prop":"Class"},{"av":"edtavAuthenticationmasterrepository_Visible","ctrl":"vAUTHENTICATIONMASTERREPOSITORY","prop":"Visible"},{"av":"divAuthenticationmasterrepository_cell_Class","ctrl":"AUTHENTICATIONMASTERREPOSITORY_CELL","prop":"Class"},{"av":"cmbavDefaultauthtypename"},{"av":"divDefaultauthtypename_cell_Class","ctrl":"DEFAULTAUTHTYPENAME_CELL","prop":"Class"},{"av":"cmbavDefaultroleid"},{"av":"divDefaultroleid_cell_Class","ctrl":"DEFAULTROLEID_CELL","prop":"Class"},{"av":"cmbavDefaultsecuritypolicyid"},{"av":"divDefaultsecuritypolicyid_cell_Class","ctrl":"DEFAULTSECURITYPOLICYID_CELL","prop":"Class"},{"av":"chkavEnableworkingasgammanagerrepo.Visible","ctrl":"vENABLEWORKINGASGAMMANAGERREPO","prop":"Visible"},{"av":"divEnableworkingasgammanagerrepo_cell_Class","ctrl":"ENABLEWORKINGASGAMMANAGERREPO_CELL","prop":"Class"}]}""");
         setEventMetadata("VEMAILSERVERUSESAUTHENTICATION.CLICK","""{"handler":"E141X2","iparms":[{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"cmbavUseridentification"},{"av":"AV80UserIdentification","fld":"vUSERIDENTIFICATION"}]""");
         setEventMetadata("VEMAILSERVERUSESAUTHENTICATION.CLICK",""","oparms":[{"av":"edtavEmailserverauthenticationusername_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERNAME","prop":"Visible"},{"av":"divEmailserverauthenticationusername_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERNAME_CELL","prop":"Class"},{"av":"edtavEmailserverauthenticationuserpassword_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERPASSWORD","prop":"Visible"},{"av":"divEmailserverauthenticationuserpassword_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodyforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodyforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"chkavUseremailisunique.Visible","ctrl":"vUSEREMAILISUNIQUE","prop":"Visible"},{"av":"divUseremailisunique_cell_Class","ctrl":"USEREMAILISUNIQUE_CELL","prop":"Class"},{"av":"chkavRequiredemail.Visible","ctrl":"vREQUIREDEMAIL","prop":"Visible"},{"av":"divRequiredemail_cell_Class","ctrl":"REQUIREDEMAIL_CELL","prop":"Class"},{"av":"edtavAuthenticationmasterrepository_Visible","ctrl":"vAUTHENTICATIONMASTERREPOSITORY","prop":"Visible"},{"av":"divAuthenticationmasterrepository_cell_Class","ctrl":"AUTHENTICATIONMASTERREPOSITORY_CELL","prop":"Class"},{"av":"cmbavDefaultauthtypename"},{"av":"divDefaultauthtypename_cell_Class","ctrl":"DEFAULTAUTHTYPENAME_CELL","prop":"Class"},{"av":"cmbavDefaultroleid"},{"av":"divDefaultroleid_cell_Class","ctrl":"DEFAULTROLEID_CELL","prop":"Class"},{"av":"cmbavDefaultsecuritypolicyid"},{"av":"divDefaultsecuritypolicyid_cell_Class","ctrl":"DEFAULTSECURITYPOLICYID_CELL","prop":"Class"},{"av":"chkavEnableworkingasgammanagerrepo.Visible","ctrl":"vENABLEWORKINGASGAMMANAGERREPO","prop":"Visible"},{"av":"divEnableworkingasgammanagerrepo_cell_Class","ctrl":"ENABLEWORKINGASGAMMANAGERREPO_CELL","prop":"Class"}]}""");
         setEventMetadata("VEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT.CLICK","""{"handler":"E151X2","iparms":[{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"cmbavUseridentification"},{"av":"AV80UserIdentification","fld":"vUSERIDENTIFICATION"}]""");
         setEventMetadata("VEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT.CLICK",""","oparms":[{"av":"edtavEmailserverauthenticationusername_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERNAME","prop":"Visible"},{"av":"divEmailserverauthenticationusername_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERNAME_CELL","prop":"Class"},{"av":"edtavEmailserverauthenticationuserpassword_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERPASSWORD","prop":"Visible"},{"av":"divEmailserverauthenticationuserpassword_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodyforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodyforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"chkavUseremailisunique.Visible","ctrl":"vUSEREMAILISUNIQUE","prop":"Visible"},{"av":"divUseremailisunique_cell_Class","ctrl":"USEREMAILISUNIQUE_CELL","prop":"Class"},{"av":"chkavRequiredemail.Visible","ctrl":"vREQUIREDEMAIL","prop":"Visible"},{"av":"divRequiredemail_cell_Class","ctrl":"REQUIREDEMAIL_CELL","prop":"Class"},{"av":"edtavAuthenticationmasterrepository_Visible","ctrl":"vAUTHENTICATIONMASTERREPOSITORY","prop":"Visible"},{"av":"divAuthenticationmasterrepository_cell_Class","ctrl":"AUTHENTICATIONMASTERREPOSITORY_CELL","prop":"Class"},{"av":"cmbavDefaultauthtypename"},{"av":"divDefaultauthtypename_cell_Class","ctrl":"DEFAULTAUTHTYPENAME_CELL","prop":"Class"},{"av":"cmbavDefaultroleid"},{"av":"divDefaultroleid_cell_Class","ctrl":"DEFAULTROLEID_CELL","prop":"Class"},{"av":"cmbavDefaultsecuritypolicyid"},{"av":"divDefaultsecuritypolicyid_cell_Class","ctrl":"DEFAULTSECURITYPOLICYID_CELL","prop":"Class"},{"av":"chkavEnableworkingasgammanagerrepo.Visible","ctrl":"vENABLEWORKINGASGAMMANAGERREPO","prop":"Visible"},{"av":"divEnableworkingasgammanagerrepo_cell_Class","ctrl":"ENABLEWORKINGASGAMMANAGERREPO_CELL","prop":"Class"}]}""");
         setEventMetadata("VEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD.CLICK","""{"handler":"E161X2","iparms":[{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"cmbavUseridentification"},{"av":"AV80UserIdentification","fld":"vUSERIDENTIFICATION"}]""");
         setEventMetadata("VEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD.CLICK",""","oparms":[{"av":"edtavEmailserverauthenticationusername_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERNAME","prop":"Visible"},{"av":"divEmailserverauthenticationusername_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERNAME_CELL","prop":"Class"},{"av":"edtavEmailserverauthenticationuserpassword_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERPASSWORD","prop":"Visible"},{"av":"divEmailserverauthenticationuserpassword_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodyforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodyforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"chkavUseremailisunique.Visible","ctrl":"vUSEREMAILISUNIQUE","prop":"Visible"},{"av":"divUseremailisunique_cell_Class","ctrl":"USEREMAILISUNIQUE_CELL","prop":"Class"},{"av":"chkavRequiredemail.Visible","ctrl":"vREQUIREDEMAIL","prop":"Visible"},{"av":"divRequiredemail_cell_Class","ctrl":"REQUIREDEMAIL_CELL","prop":"Class"},{"av":"edtavAuthenticationmasterrepository_Visible","ctrl":"vAUTHENTICATIONMASTERREPOSITORY","prop":"Visible"},{"av":"divAuthenticationmasterrepository_cell_Class","ctrl":"AUTHENTICATIONMASTERREPOSITORY_CELL","prop":"Class"},{"av":"cmbavDefaultauthtypename"},{"av":"divDefaultauthtypename_cell_Class","ctrl":"DEFAULTAUTHTYPENAME_CELL","prop":"Class"},{"av":"cmbavDefaultroleid"},{"av":"divDefaultroleid_cell_Class","ctrl":"DEFAULTROLEID_CELL","prop":"Class"},{"av":"cmbavDefaultsecuritypolicyid"},{"av":"divDefaultsecuritypolicyid_cell_Class","ctrl":"DEFAULTSECURITYPOLICYID_CELL","prop":"Class"},{"av":"chkavEnableworkingasgammanagerrepo.Visible","ctrl":"vENABLEWORKINGASGAMMANAGERREPO","prop":"Visible"},{"av":"divEnableworkingasgammanagerrepo_cell_Class","ctrl":"ENABLEWORKINGASGAMMANAGERREPO_CELL","prop":"Class"}]}""");
         setEventMetadata("VEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL.CLICK","""{"handler":"E171X2","iparms":[{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"cmbavUseridentification"},{"av":"AV80UserIdentification","fld":"vUSERIDENTIFICATION"}]""");
         setEventMetadata("VEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL.CLICK",""","oparms":[{"av":"edtavEmailserverauthenticationusername_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERNAME","prop":"Visible"},{"av":"divEmailserverauthenticationusername_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERNAME_CELL","prop":"Class"},{"av":"edtavEmailserverauthenticationuserpassword_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERPASSWORD","prop":"Visible"},{"av":"divEmailserverauthenticationuserpassword_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodyforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodyforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"chkavUseremailisunique.Visible","ctrl":"vUSEREMAILISUNIQUE","prop":"Visible"},{"av":"divUseremailisunique_cell_Class","ctrl":"USEREMAILISUNIQUE_CELL","prop":"Class"},{"av":"chkavRequiredemail.Visible","ctrl":"vREQUIREDEMAIL","prop":"Visible"},{"av":"divRequiredemail_cell_Class","ctrl":"REQUIREDEMAIL_CELL","prop":"Class"},{"av":"edtavAuthenticationmasterrepository_Visible","ctrl":"vAUTHENTICATIONMASTERREPOSITORY","prop":"Visible"},{"av":"divAuthenticationmasterrepository_cell_Class","ctrl":"AUTHENTICATIONMASTERREPOSITORY_CELL","prop":"Class"},{"av":"cmbavDefaultauthtypename"},{"av":"divDefaultauthtypename_cell_Class","ctrl":"DEFAULTAUTHTYPENAME_CELL","prop":"Class"},{"av":"cmbavDefaultroleid"},{"av":"divDefaultroleid_cell_Class","ctrl":"DEFAULTROLEID_CELL","prop":"Class"},{"av":"cmbavDefaultsecuritypolicyid"},{"av":"divDefaultsecuritypolicyid_cell_Class","ctrl":"DEFAULTSECURITYPOLICYID_CELL","prop":"Class"},{"av":"chkavEnableworkingasgammanagerrepo.Visible","ctrl":"vENABLEWORKINGASGAMMANAGERREPO","prop":"Visible"},{"av":"divEnableworkingasgammanagerrepo_cell_Class","ctrl":"ENABLEWORKINGASGAMMANAGERREPO_CELL","prop":"Class"}]}""");
         setEventMetadata("VEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD.CLICK","""{"handler":"E181X2","iparms":[{"av":"AV32EmailServerUsesAuthentication","fld":"vEMAILSERVERUSESAUTHENTICATION"},{"av":"AV23EmailServer_SendEmailWhenUserActivateAccount","fld":"vEMAILSERVER_SENDEMAILWHENUSERACTIVATEACCOUNT"},{"av":"AV25EmailServer_SendEmailWhenUserChangePassword","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEPASSWORD"},{"av":"AV24EmailServer_SendEmailWhenUserChangeEmail","fld":"vEMAILSERVER_SENDEMAILWHENUSERCHANGEEMAIL"},{"av":"AV22EmailServer_SendEmailForRecoveryPassword","fld":"vEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD"},{"av":"cmbavUseridentification"},{"av":"AV80UserIdentification","fld":"vUSERIDENTIFICATION"}]""");
         setEventMetadata("VEMAILSERVER_SENDEMAILFORRECOVERYPASSWORD.CLICK",""","oparms":[{"av":"edtavEmailserverauthenticationusername_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERNAME","prop":"Visible"},{"av":"divEmailserverauthenticationusername_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERNAME_CELL","prop":"Class"},{"av":"edtavEmailserverauthenticationuserpassword_Visible","ctrl":"vEMAILSERVERAUTHENTICATIONUSERPASSWORD","prop":"Visible"},{"av":"divEmailserverauthenticationuserpassword_cell_Class","ctrl":"EMAILSERVERAUTHENTICATIONUSERPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuseractivateaccount_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuseractivateaccount_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERACTIVATEACCOUNT_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangepassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangepassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectwhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailsubjectwhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodywhenuserchangeemail_Visible","ctrl":"vEMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL","prop":"Visible"},{"av":"divEmailserver_emailbodywhenuserchangeemail_cell_Class","ctrl":"EMAILSERVER_EMAILBODYWHENUSERCHANGEEMAIL_CELL","prop":"Class"},{"av":"edtavEmailserver_emailsubjectforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailsubjectforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILSUBJECTFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"edtavEmailserver_emailbodyforrecoverypassword_Visible","ctrl":"vEMAILSERVER_EMAILBODYFORRECOVERYPASSWORD","prop":"Visible"},{"av":"divEmailserver_emailbodyforrecoverypassword_cell_Class","ctrl":"EMAILSERVER_EMAILBODYFORRECOVERYPASSWORD_CELL","prop":"Class"},{"av":"chkavUseremailisunique.Visible","ctrl":"vUSEREMAILISUNIQUE","prop":"Visible"},{"av":"divUseremailisunique_cell_Class","ctrl":"USEREMAILISUNIQUE_CELL","prop":"Class"},{"av":"chkavRequiredemail.Visible","ctrl":"vREQUIREDEMAIL","prop":"Visible"},{"av":"divRequiredemail_cell_Class","ctrl":"REQUIREDEMAIL_CELL","prop":"Class"},{"av":"edtavAuthenticationmasterrepository_Visible","ctrl":"vAUTHENTICATIONMASTERREPOSITORY","prop":"Visible"},{"av":"divAuthenticationmasterrepository_cell_Class","ctrl":"AUTHENTICATIONMASTERREPOSITORY_CELL","prop":"Class"},{"av":"cmbavDefaultauthtypename"},{"av":"divDefaultauthtypename_cell_Class","ctrl":"DEFAULTAUTHTYPENAME_CELL","prop":"Class"},{"av":"cmbavDefaultroleid"},{"av":"divDefaultroleid_cell_Class","ctrl":"DEFAULTROLEID_CELL","prop":"Class"},{"av":"cmbavDefaultsecuritypolicyid"},{"av":"divDefaultsecuritypolicyid_cell_Class","ctrl":"DEFAULTSECURITYPOLICYID_CELL","prop":"Class"},{"av":"chkavEnableworkingasgammanagerrepo.Visible","ctrl":"vENABLEWORKINGASGAMMANAGERREPO","prop":"Visible"},{"av":"divEnableworkingasgammanagerrepo_cell_Class","ctrl":"ENABLEWORKINGASGAMMANAGERREPO_CELL","prop":"Class"}]}""");
         setEventMetadata("VALIDV_LOGOUTBEHAVIOR","""{"handler":"Validv_Logoutbehavior","iparms":[]}""");
         setEventMetadata("VALIDV_USERIDENTIFICATION","""{"handler":"Validv_Useridentification","iparms":[]}""");
         setEventMetadata("VALIDV_USERACTIVATIONMETHOD","""{"handler":"Validv_Useractivationmethod","iparms":[]}""");
         setEventMetadata("VALIDV_USERREMEMBERMETYPE","""{"handler":"Validv_Userremembermetype","iparms":[]}""");
         setEventMetadata("VALIDV_GENERATESESSIONSTATISTICS","""{"handler":"Validv_Generatesessionstatistics","iparms":[]}""");
         setEventMetadata("VALIDV_INTSECBYDOMAINMODE","""{"handler":"Validv_Intsecbydomainmode","iparms":[]}""");
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
         GXEncryptionTmp = "";
         AV71SecurityAdministratorEmail = "";
         forbiddenHiddens = new GXProperties();
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         StyleString = "";
         ucGxuitabspanel_tabs = new GXUserControl();
         lblGeneral_title_Jsonclick = "";
         TempTags = "";
         AV44GUID = "";
         AV58NameSpace = "";
         AV57Name = "";
         AV13Dsc = "";
         AV33EnableTracing = "";
         AV6AuthenticationMasterRepository = "";
         AV10DefaultAuthTypeName = "";
         AV55LogoutBehavior = "";
         lblUsers_title_Jsonclick = "";
         AV80UserIdentification = "";
         AV77UserActivationMethod = "";
         AV85UserRememberMeType = "";
         lblSession_title_Jsonclick = "";
         AV42GenerateSessionStatistics = "";
         AV49IntSecByDomainMode = "";
         AV48IntSecByDomainJWTSecret = "";
         AV47IntSecByDomainEncryptionKey = "";
         lblTabemail_title_Jsonclick = "";
         AV28EmailServerHost = "";
         AV74ServerSenderAddress = "";
         AV75ServerSenderName = "";
         AV26EmailServerAuthenticationUsername = "";
         AV27EmailServerAuthenticationUserPassword = "";
         AV19EmailServer_EmailSubjectWhenUserActivateAccount = "";
         AV15EmailServer_EmailBodyWhenUserActivateAccount = "";
         AV21EmailServer_EmailSubjectWhenUserChangePassword = "";
         AV17EmailServer_EmailBodyWhenUserChangePassword = "";
         AV20EmailServer_EmailSubjectWhenUserChangeEmail = "";
         AV16EmailServer_EmailBodyWhenUserChangeEmail = "";
         AV18EmailServer_EmailSubjectForRecoveryPassword = "";
         AV14EmailServer_EmailBodyForRecoveryPassword = "";
         bttBtnenter_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         hsh = "";
         AV8AuthenticationTypes = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple", "DesignSystem.Programs");
         AV52Language = "";
         AV37Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV7AuthenticationType = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple(context);
         AV72SecurityPolicies = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy>( context, "GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy", "DesignSystem.Programs");
         AV40FilterSecPol = new GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicyFilter(context);
         AV73SecurityPolicy = new GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy(context);
         AV70Roles = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRole>( context, "GeneXus.Programs.genexussecurity.SdtGAMRole", "DesignSystem.Programs");
         AV39FilterRole = new GeneXus.Programs.genexussecurity.SdtGAMRoleFilter(context);
         AV69Role = new GeneXus.Programs.genexussecurity.SdtGAMRole(context);
         AV60Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV62RepositoryCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRepository>( context, "GeneXus.Programs.genexussecurity.SdtGAMRepository", "DesignSystem.Programs");
         AV38Filter = new GeneXus.Programs.genexussecurity.SdtGAMRepositoryFilter(context);
         AV87GAMRepositoryItem = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV36Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamrepositoryconfiguration__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamrepositoryconfiguration__default(),
            new Object[][] {
            }
         );
         /* GeneXus formulas. */
         edtavRepoid_Enabled = 0;
         edtavGuid_Enabled = 0;
         edtavNamespace_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short AV78UserAutomaticActivationTimeout ;
      private short AV81UserRecoveryPasswordKeyTimeOut ;
      private short AV82UserRecoveryPasswordKeyDailyMaximum ;
      private short AV83UserRecoveryPasswordKeyMonthlyMaximum ;
      private short AV54LoginAttemptsToLockUser ;
      private short AV41GAMUnblockUserTimeout ;
      private short AV84UserRememberMeTimeOut ;
      private short AV53LoginAttemptsToLockSession ;
      private short AV56MinimumAmountCharactersInLogin ;
      private short AV29EmailServerPort ;
      private short AV31EmailServerTimeout ;
      private short nDonePA ;
      private short AV34EnableTracingNumeric ;
      private short nGXWrapped ;
      private int Gxuitabspanel_tabs_Pagecount ;
      private int edtavRepoid_Enabled ;
      private int edtavGuid_Enabled ;
      private int edtavNamespace_Enabled ;
      private int edtavName_Enabled ;
      private int edtavDsc_Enabled ;
      private int edtavAuthenticationmasterrepository_Visible ;
      private int edtavAuthenticationmasterrepository_Enabled ;
      private int AV12DefaultSecurityPolicyId ;
      private int edtavUserautomaticactivationtimeout_Enabled ;
      private int edtavUserrecoverypasswordkeytimeout_Enabled ;
      private int edtavUserrecoverypasswordkeydailymaximum_Enabled ;
      private int edtavUserrecoverypasswordkeymonthlymaximum_Enabled ;
      private int edtavLoginattemptstolockuser_Enabled ;
      private int edtavGamunblockusertimeout_Enabled ;
      private int edtavUserremembermetimeout_Enabled ;
      private int edtavTotpsecretkeylength_Enabled ;
      private int AV86UserSessionCacheTimeout ;
      private int edtavUsersessioncachetimeout_Enabled ;
      private int edtavLoginattemptstolocksession_Enabled ;
      private int edtavMinimumamountcharactersinlogin_Enabled ;
      private int AV61RepositoryCacheTimeout ;
      private int edtavRepositorycachetimeout_Enabled ;
      private int divTblintsecbydomain_Visible ;
      private int edtavIntsecbydomainjwtsecret_Enabled ;
      private int edtavIntsecbydomainencryptionkey_Enabled ;
      private int edtavEmailserverhost_Enabled ;
      private int edtavEmailserverport_Enabled ;
      private int edtavEmailservertimeout_Enabled ;
      private int edtavServersenderaddress_Enabled ;
      private int edtavServersendername_Enabled ;
      private int edtavEmailserverauthenticationusername_Visible ;
      private int edtavEmailserverauthenticationusername_Enabled ;
      private int edtavEmailserverauthenticationuserpassword_Visible ;
      private int edtavEmailserverauthenticationuserpassword_Enabled ;
      private int edtavEmailserver_emailsubjectwhenuseractivateaccount_Visible ;
      private int edtavEmailserver_emailsubjectwhenuseractivateaccount_Enabled ;
      private int edtavEmailserver_emailbodywhenuseractivateaccount_Visible ;
      private int edtavEmailserver_emailbodywhenuseractivateaccount_Enabled ;
      private int edtavEmailserver_emailsubjectwhenuserchangepassword_Visible ;
      private int edtavEmailserver_emailsubjectwhenuserchangepassword_Enabled ;
      private int edtavEmailserver_emailbodywhenuserchangepassword_Visible ;
      private int edtavEmailserver_emailbodywhenuserchangepassword_Enabled ;
      private int edtavEmailserver_emailsubjectwhenuserchangeemail_Visible ;
      private int edtavEmailserver_emailsubjectwhenuserchangeemail_Enabled ;
      private int edtavEmailserver_emailbodywhenuserchangeemail_Visible ;
      private int edtavEmailserver_emailbodywhenuserchangeemail_Enabled ;
      private int edtavEmailserver_emailsubjectforrecoverypassword_Visible ;
      private int edtavEmailserver_emailsubjectforrecoverypassword_Enabled ;
      private int edtavEmailserver_emailbodyforrecoverypassword_Visible ;
      private int edtavEmailserver_emailbodyforrecoverypassword_Enabled ;
      private int AV90GXV1 ;
      private int AV91GXV2 ;
      private int AV92GXV3 ;
      private int AV93GXV4 ;
      private int AV94GXV5 ;
      private int idxLst ;
      private long AV45Id ;
      private long wcpOAV45Id ;
      private long AV59RepoId ;
      private long AV11DefaultRoleId ;
      private long AV50TOTPSecretKeyLength ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string Gxuitabspanel_tabs_Class ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string Gxuitabspanel_tabs_Internalname ;
      private string lblGeneral_title_Internalname ;
      private string lblGeneral_title_Jsonclick ;
      private string divUnnamedtable3_Internalname ;
      private string edtavRepoid_Internalname ;
      private string TempTags ;
      private string edtavRepoid_Jsonclick ;
      private string edtavGuid_Internalname ;
      private string AV44GUID ;
      private string edtavGuid_Jsonclick ;
      private string edtavNamespace_Internalname ;
      private string AV58NameSpace ;
      private string edtavNamespace_Jsonclick ;
      private string edtavName_Internalname ;
      private string AV57Name ;
      private string edtavName_Jsonclick ;
      private string edtavDsc_Internalname ;
      private string AV13Dsc ;
      private string edtavDsc_Jsonclick ;
      private string cmbavEnabletracing_Internalname ;
      private string AV33EnableTracing ;
      private string cmbavEnabletracing_Jsonclick ;
      private string divAuthenticationmasterrepository_cell_Internalname ;
      private string divAuthenticationmasterrepository_cell_Class ;
      private string edtavAuthenticationmasterrepository_Internalname ;
      private string AV6AuthenticationMasterRepository ;
      private string edtavAuthenticationmasterrepository_Jsonclick ;
      private string divDefaultauthtypename_cell_Internalname ;
      private string divDefaultauthtypename_cell_Class ;
      private string cmbavDefaultauthtypename_Internalname ;
      private string AV10DefaultAuthTypeName ;
      private string cmbavDefaultauthtypename_Jsonclick ;
      private string divDefaultroleid_cell_Internalname ;
      private string divDefaultroleid_cell_Class ;
      private string cmbavDefaultroleid_Internalname ;
      private string cmbavDefaultroleid_Jsonclick ;
      private string divDefaultsecuritypolicyid_cell_Internalname ;
      private string divDefaultsecuritypolicyid_cell_Class ;
      private string cmbavDefaultsecuritypolicyid_Internalname ;
      private string cmbavDefaultsecuritypolicyid_Jsonclick ;
      private string chkavAllowoauthaccess_Internalname ;
      private string cmbavLogoutbehavior_Internalname ;
      private string AV55LogoutBehavior ;
      private string cmbavLogoutbehavior_Jsonclick ;
      private string divEnableworkingasgammanagerrepo_cell_Internalname ;
      private string divEnableworkingasgammanagerrepo_cell_Class ;
      private string chkavEnableworkingasgammanagerrepo_Internalname ;
      private string lblUsers_title_Internalname ;
      private string lblUsers_title_Jsonclick ;
      private string divUnnamedtable2_Internalname ;
      private string cmbavUseridentification_Internalname ;
      private string AV80UserIdentification ;
      private string cmbavUseridentification_Jsonclick ;
      private string divUseremailisunique_cell_Internalname ;
      private string divUseremailisunique_cell_Class ;
      private string chkavUseremailisunique_Internalname ;
      private string cmbavUseractivationmethod_Internalname ;
      private string AV77UserActivationMethod ;
      private string cmbavUseractivationmethod_Jsonclick ;
      private string edtavUserautomaticactivationtimeout_Internalname ;
      private string edtavUserautomaticactivationtimeout_Jsonclick ;
      private string edtavUserrecoverypasswordkeytimeout_Internalname ;
      private string edtavUserrecoverypasswordkeytimeout_Jsonclick ;
      private string edtavUserrecoverypasswordkeydailymaximum_Internalname ;
      private string edtavUserrecoverypasswordkeydailymaximum_Jsonclick ;
      private string edtavUserrecoverypasswordkeymonthlymaximum_Internalname ;
      private string edtavUserrecoverypasswordkeymonthlymaximum_Jsonclick ;
      private string edtavLoginattemptstolockuser_Internalname ;
      private string edtavLoginattemptstolockuser_Jsonclick ;
      private string edtavGamunblockusertimeout_Internalname ;
      private string edtavGamunblockusertimeout_Jsonclick ;
      private string cmbavUserremembermetype_Internalname ;
      private string AV85UserRememberMeType ;
      private string cmbavUserremembermetype_Jsonclick ;
      private string edtavUserremembermetimeout_Internalname ;
      private string edtavUserremembermetimeout_Jsonclick ;
      private string edtavTotpsecretkeylength_Internalname ;
      private string edtavTotpsecretkeylength_Jsonclick ;
      private string divRequiredemail_cell_Internalname ;
      private string divRequiredemail_cell_Class ;
      private string chkavRequiredemail_Internalname ;
      private string chkavRequiredpassword_Internalname ;
      private string chkavRequiredfirstname_Internalname ;
      private string chkavRequiredlastname_Internalname ;
      private string chkavRequiredbirthday_Internalname ;
      private string chkavRequiredgender_Internalname ;
      private string lblSession_title_Internalname ;
      private string lblSession_title_Jsonclick ;
      private string divUnnamedtable1_Internalname ;
      private string cmbavGeneratesessionstatistics_Internalname ;
      private string AV42GenerateSessionStatistics ;
      private string cmbavGeneratesessionstatistics_Jsonclick ;
      private string edtavUsersessioncachetimeout_Internalname ;
      private string edtavUsersessioncachetimeout_Jsonclick ;
      private string chkavGiveanonymoussession_Internalname ;
      private string chkavSessionexpiresonipchange_Internalname ;
      private string edtavLoginattemptstolocksession_Internalname ;
      private string edtavLoginattemptstolocksession_Jsonclick ;
      private string edtavMinimumamountcharactersinlogin_Internalname ;
      private string edtavMinimumamountcharactersinlogin_Jsonclick ;
      private string edtavRepositorycachetimeout_Internalname ;
      private string edtavRepositorycachetimeout_Jsonclick ;
      private string chkavIntsecbydomainenable_Internalname ;
      private string divTblintsecbydomain_Internalname ;
      private string cmbavIntsecbydomainmode_Internalname ;
      private string AV49IntSecByDomainMode ;
      private string cmbavIntsecbydomainmode_Jsonclick ;
      private string edtavIntsecbydomainjwtsecret_Internalname ;
      private string edtavIntsecbydomainjwtsecret_Jsonclick ;
      private string edtavIntsecbydomainencryptionkey_Internalname ;
      private string edtavIntsecbydomainencryptionkey_Jsonclick ;
      private string lblTabemail_title_Internalname ;
      private string lblTabemail_title_Jsonclick ;
      private string divTableattributes_Internalname ;
      private string edtavEmailserverhost_Internalname ;
      private string AV28EmailServerHost ;
      private string edtavEmailserverhost_Jsonclick ;
      private string edtavEmailserverport_Internalname ;
      private string edtavEmailserverport_Jsonclick ;
      private string edtavEmailservertimeout_Internalname ;
      private string edtavEmailservertimeout_Jsonclick ;
      private string chkavEmailserversecure_Internalname ;
      private string edtavServersenderaddress_Internalname ;
      private string edtavServersenderaddress_Jsonclick ;
      private string edtavServersendername_Internalname ;
      private string AV75ServerSenderName ;
      private string edtavServersendername_Jsonclick ;
      private string chkavEmailserverusesauthentication_Internalname ;
      private string divEmailserverauthenticationusername_cell_Internalname ;
      private string divEmailserverauthenticationusername_cell_Class ;
      private string edtavEmailserverauthenticationusername_Internalname ;
      private string AV26EmailServerAuthenticationUsername ;
      private string edtavEmailserverauthenticationusername_Jsonclick ;
      private string divEmailserverauthenticationuserpassword_cell_Internalname ;
      private string divEmailserverauthenticationuserpassword_cell_Class ;
      private string edtavEmailserverauthenticationuserpassword_Internalname ;
      private string AV27EmailServerAuthenticationUserPassword ;
      private string edtavEmailserverauthenticationuserpassword_Jsonclick ;
      private string chkavEmailserver_sendemailwhenuseractivateaccount_Internalname ;
      private string divEmailserver_emailsubjectwhenuseractivateaccount_cell_Internalname ;
      private string divEmailserver_emailsubjectwhenuseractivateaccount_cell_Class ;
      private string edtavEmailserver_emailsubjectwhenuseractivateaccount_Internalname ;
      private string AV19EmailServer_EmailSubjectWhenUserActivateAccount ;
      private string edtavEmailserver_emailsubjectwhenuseractivateaccount_Jsonclick ;
      private string divEmailserver_emailbodywhenuseractivateaccount_cell_Internalname ;
      private string divEmailserver_emailbodywhenuseractivateaccount_cell_Class ;
      private string edtavEmailserver_emailbodywhenuseractivateaccount_Internalname ;
      private string chkavEmailserver_sendemailwhenuserchangepassword_Internalname ;
      private string divEmailserver_emailsubjectwhenuserchangepassword_cell_Internalname ;
      private string divEmailserver_emailsubjectwhenuserchangepassword_cell_Class ;
      private string edtavEmailserver_emailsubjectwhenuserchangepassword_Internalname ;
      private string AV21EmailServer_EmailSubjectWhenUserChangePassword ;
      private string edtavEmailserver_emailsubjectwhenuserchangepassword_Jsonclick ;
      private string divEmailserver_emailbodywhenuserchangepassword_cell_Internalname ;
      private string divEmailserver_emailbodywhenuserchangepassword_cell_Class ;
      private string edtavEmailserver_emailbodywhenuserchangepassword_Internalname ;
      private string chkavEmailserver_sendemailwhenuserchangeemail_Internalname ;
      private string divEmailserver_emailsubjectwhenuserchangeemail_cell_Internalname ;
      private string divEmailserver_emailsubjectwhenuserchangeemail_cell_Class ;
      private string edtavEmailserver_emailsubjectwhenuserchangeemail_Internalname ;
      private string AV20EmailServer_EmailSubjectWhenUserChangeEmail ;
      private string edtavEmailserver_emailsubjectwhenuserchangeemail_Jsonclick ;
      private string divEmailserver_emailbodywhenuserchangeemail_cell_Internalname ;
      private string divEmailserver_emailbodywhenuserchangeemail_cell_Class ;
      private string edtavEmailserver_emailbodywhenuserchangeemail_Internalname ;
      private string chkavEmailserver_sendemailforrecoverypassword_Internalname ;
      private string divEmailserver_emailsubjectforrecoverypassword_cell_Internalname ;
      private string divEmailserver_emailsubjectforrecoverypassword_cell_Class ;
      private string edtavEmailserver_emailsubjectforrecoverypassword_Internalname ;
      private string AV18EmailServer_EmailSubjectForRecoveryPassword ;
      private string edtavEmailserver_emailsubjectforrecoverypassword_Jsonclick ;
      private string divEmailserver_emailbodyforrecoverypassword_cell_Internalname ;
      private string divEmailserver_emailbodyforrecoverypassword_cell_Class ;
      private string edtavEmailserver_emailbodyforrecoverypassword_Internalname ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string hsh ;
      private string AV52Language ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV9CanRegisterUsers ;
      private bool Gxuitabspanel_tabs_Historymanagement ;
      private bool wbLoad ;
      private bool AV5AllowOauthAccess ;
      private bool AV35EnableWorkingAsGAMManagerRepo ;
      private bool AV79UserEmailisUnique ;
      private bool AV64RequiredEmail ;
      private bool AV68RequiredPassword ;
      private bool AV65RequiredFirstName ;
      private bool AV67RequiredLastName ;
      private bool AV63RequiredBirthday ;
      private bool AV66RequiredGender ;
      private bool AV43GiveAnonymousSession ;
      private bool AV76SessionExpiresOnIPChange ;
      private bool AV51IntSecByDomainEnable ;
      private bool AV30EmailServerSecure ;
      private bool AV32EmailServerUsesAuthentication ;
      private bool AV23EmailServer_SendEmailWhenUserActivateAccount ;
      private bool AV25EmailServer_SendEmailWhenUserChangePassword ;
      private bool AV24EmailServer_SendEmailWhenUserChangeEmail ;
      private bool AV22EmailServer_SendEmailForRecoveryPassword ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private string AV71SecurityAdministratorEmail ;
      private string AV48IntSecByDomainJWTSecret ;
      private string AV47IntSecByDomainEncryptionKey ;
      private string AV74ServerSenderAddress ;
      private string AV15EmailServer_EmailBodyWhenUserActivateAccount ;
      private string AV17EmailServer_EmailBodyWhenUserChangePassword ;
      private string AV16EmailServer_EmailBodyWhenUserChangeEmail ;
      private string AV14EmailServer_EmailBodyForRecoveryPassword ;
      private GXProperties forbiddenHiddens ;
      private GXUserControl ucGxuitabspanel_tabs ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavEnabletracing ;
      private GXCombobox cmbavDefaultauthtypename ;
      private GXCombobox cmbavDefaultroleid ;
      private GXCombobox cmbavDefaultsecuritypolicyid ;
      private GXCheckbox chkavAllowoauthaccess ;
      private GXCombobox cmbavLogoutbehavior ;
      private GXCheckbox chkavEnableworkingasgammanagerrepo ;
      private GXCombobox cmbavUseridentification ;
      private GXCheckbox chkavUseremailisunique ;
      private GXCombobox cmbavUseractivationmethod ;
      private GXCombobox cmbavUserremembermetype ;
      private GXCheckbox chkavRequiredemail ;
      private GXCheckbox chkavRequiredpassword ;
      private GXCheckbox chkavRequiredfirstname ;
      private GXCheckbox chkavRequiredlastname ;
      private GXCheckbox chkavRequiredbirthday ;
      private GXCheckbox chkavRequiredgender ;
      private GXCombobox cmbavGeneratesessionstatistics ;
      private GXCheckbox chkavGiveanonymoussession ;
      private GXCheckbox chkavSessionexpiresonipchange ;
      private GXCheckbox chkavIntsecbydomainenable ;
      private GXCombobox cmbavIntsecbydomainmode ;
      private GXCheckbox chkavEmailserversecure ;
      private GXCheckbox chkavEmailserverusesauthentication ;
      private GXCheckbox chkavEmailserver_sendemailwhenuseractivateaccount ;
      private GXCheckbox chkavEmailserver_sendemailwhenuserchangepassword ;
      private GXCheckbox chkavEmailserver_sendemailwhenuserchangeemail ;
      private GXCheckbox chkavEmailserver_sendemailforrecoverypassword ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple> AV8AuthenticationTypes ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV37Errors ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple AV7AuthenticationType ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy> AV72SecurityPolicies ;
      private GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicyFilter AV40FilterSecPol ;
      private GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy AV73SecurityPolicy ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRole> AV70Roles ;
      private GeneXus.Programs.genexussecurity.SdtGAMRoleFilter AV39FilterRole ;
      private GeneXus.Programs.genexussecurity.SdtGAMRole AV69Role ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV60Repository ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRepository> AV62RepositoryCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepositoryFilter AV38Filter ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV87GAMRepositoryItem ;
      private IDataStoreProvider pr_default ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV36Error ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamrepositoryconfiguration__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamrepositoryconfiguration__default : DataStoreHelperBase, IDataStoreHelper
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
