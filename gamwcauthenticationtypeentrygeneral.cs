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
   public class gamwcauthenticationtypeentrygeneral : GXWebComponent
   {
      public gamwcauthenticationtypeentrygeneral( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            context.SetDefaultTheme("WorkWithPlusDS", true);
         }
      }

      public gamwcauthenticationtypeentrygeneral( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref string aP0_Gx_mode ,
                           ref string aP1_Name ,
                           ref string aP2_TypeId )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV46Name = aP1_Name;
         this.AV69TypeId = aP2_TypeId;
         ExecuteImpl();
         aP0_Gx_mode=this.Gx_mode;
         aP1_Name=this.AV46Name;
         aP2_TypeId=this.AV69TypeId;
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      public override void SetPrefix( string sPPrefix )
      {
         sPrefix = sPPrefix;
      }

      protected override void createObjects( )
      {
         cmbavFunctionid = new GXCombobox();
         chkavIsenable = new GXCheckbox();
         cmbavImpersonate = new GXCombobox();
         chkavTfaenable = new GXCheckbox();
         cmbavTfaauthenticationtypename = new GXCombobox();
         chkavTfaforceforallusers = new GXCheckbox();
         chkavOtpuseforfirstfactorauthentication = new GXCheckbox();
         cmbavOtpeventvalidateuser = new GXCombobox();
         cmbavOtpgenerationtype = new GXCombobox();
         cmbavOtpgenerationtype_customeventgeneratecode = new GXCombobox();
         chkavOtpgeneratecodeonlynumbers = new GXCheckbox();
         cmbavOtpeventsendcode = new GXCombobox();
         cmbavOtpeventvalidatecode = new GXCombobox();
         chkavAutocompletevirtualdirectory = new GXCheckbox();
         chkavSiteurlcallbackiscustom = new GXCheckbox();
         chkavCuautocompletevirtualdirectory = new GXCheckbox();
         chkavAdduseradditionaldatascope = new GXCheckbox();
         chkavAdduserdatascope = new GXCheckbox();
         chkavAddinitialpropertiesscope = new GXCheckbox();
         chkavAddapplicationdatascope = new GXCheckbox();
         chkavAutovalidateexternaltokenandrefresh = new GXCheckbox();
         cmbavWsversion = new GXCombobox();
         cmbavWsserversecureprotocol = new GXCombobox();
         cmbavCusversion = new GXCombobox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
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
               else if ( StringUtil.StrCmp(gxfirstwebparm, "dyncomponent") == 0 )
               {
                  setAjaxEventMode();
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  nDynComponent = 1;
                  sCompPrefix = GetPar( "sCompPrefix");
                  sSFPrefix = GetPar( "sSFPrefix");
                  Gx_mode = GetPar( "Mode");
                  AssignAttri(sPrefix, false, "Gx_mode", Gx_mode);
                  AV46Name = GetPar( "Name");
                  AssignAttri(sPrefix, false, "AV46Name", AV46Name);
                  AV69TypeId = GetPar( "TypeId");
                  AssignAttri(sPrefix, false, "AV69TypeId", AV69TypeId);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(string)Gx_mode,(string)AV46Name,(string)AV69TypeId});
                  componentstart();
                  context.httpAjaxContext.ajax_rspStartCmp(sPrefix);
                  componentdraw();
                  context.httpAjaxContext.ajax_rspEndCmp();
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               ValidateSpaRequest();
            }
            PA1I2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS1I2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  if ( nDynComponent == 0 )
                  {
                     throw new System.Net.WebException("WebComponent is not allowed to run") ;
                  }
               }
            }
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

      protected void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            context.WriteHtmlText( "<title>") ;
            context.SendWebValue( context.GetMessage( "Authentication Type Entry General", "")) ;
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
         }
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
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamwcauthenticationtypeentrygeneral.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.RTrim(AV46Name)) + "," + UrlEncode(StringUtil.RTrim(AV69TypeId));
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamwcauthenticationtypeentrygeneral.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
         }
         else
         {
            bool toggleHtmlOutput = isOutputEnabled( );
            if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
            }
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            if ( toggleHtmlOutput )
            {
               if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableOutput();
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         if ( StringUtil.StringSearch( sPrefix, "MP", 1) == 1 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOGx_mode", StringUtil.RTrim( wcpOGx_mode));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV46Name", StringUtil.RTrim( wcpOAV46Name));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV69TypeId", StringUtil.RTrim( wcpOAV69TypeId));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vCHECKREQUIREDFIELDSRESULT", AV83CheckRequiredFieldsResult);
         GxWebStd.gx_hidden_field( context, sPrefix+"vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTYPEID", StringUtil.RTrim( AV69TypeId));
      }

      protected void RenderHtmlCloseForm1I2( )
      {
         SendCloseFormHiddens( ) ;
         if ( ( StringUtil.Len( sPrefix) != 0 ) && ( context.isAjaxRequest( ) || context.isSpaRequest( ) ) )
         {
            componentjscripts();
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"GX_FocusControl", GX_FocusControl);
         define_styles( ) ;
         SendSecurityToken(sPrefix);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            SendAjaxEncryptionKey();
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
            context.WriteHtmlTextNl( "</body>") ;
            context.WriteHtmlTextNl( "</html>") ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         else
         {
            SendWebComponentState();
            context.WriteHtmlText( "</div>") ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
      }

      public override string GetPgmname( )
      {
         return "GAMWCAuthenticationTypeEntryGeneral" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Authentication Type Entry General", "") ;
      }

      protected void WB1I0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               RenderHtmlHeaders( ) ;
            }
            RenderHtmlOpenForm( ) ;
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "gamwcauthenticationtypeentrygeneral.aspx");
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMainTransaction", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            ClassString = "ErrorViewer";
            StyleString = "";
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, StyleString, ClassString, sPrefix, "false");
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
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableattributes_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 RequiredDataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavName_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavName_Internalname, context.GetMessage( "WWP_GAM_Name", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavName_Internalname, StringUtil.RTrim( AV46Name), StringUtil.RTrim( context.localUtil.Format( AV46Name, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,20);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavName_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavName_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavFunctionid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavFunctionid_Internalname, context.GetMessage( "WWP_GAM_Function", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavFunctionid, cmbavFunctionid_Internalname, StringUtil.RTrim( AV37FunctionId), 1, cmbavFunctionid_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavFunctionid.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,24);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavFunctionid.CurrentValue = StringUtil.RTrim( AV37FunctionId);
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Values", (string)(cmbavFunctionid.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavIsenable_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavIsenable_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavIsenable_Internalname, StringUtil.BoolToStr( AV45IsEnable), "", " ", 1, chkavIsenable.Enabled, "true", context.GetMessage( "WWP_GAM_Enabled", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(29, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,29);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDsc_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDsc_Internalname, context.GetMessage( "WWP_GAM_Description", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDsc_Internalname, StringUtil.RTrim( AV33Dsc), StringUtil.RTrim( context.localUtil.Format( AV33Dsc, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,33);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDsc_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDsc_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSmallimagename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSmallimagename_Internalname, context.GetMessage( "WWP_GAM_Smallimagename", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSmallimagename_Internalname, StringUtil.RTrim( AV64SmallImageName), StringUtil.RTrim( context.localUtil.Format( AV64SmallImageName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSmallimagename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSmallimagename_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavBigimagename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavBigimagename_Internalname, context.GetMessage( "WWP_GAM_BigImageName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavBigimagename_Internalname, StringUtil.RTrim( AV22BigImageName), StringUtil.RTrim( context.localUtil.Format( AV22BigImageName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,42);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavBigimagename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavBigimagename_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divImpersonate_cell_Internalname, 1, 0, "px", 0, "px", divImpersonate_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavImpersonate.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavImpersonate_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavImpersonate_Internalname, context.GetMessage( "WWP_GAM_Impersonate", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 47,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavImpersonate, cmbavImpersonate_Internalname, StringUtil.RTrim( AV44Impersonate), 1, cmbavImpersonate_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", cmbavImpersonate.Visible, cmbavImpersonate.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,47);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavImpersonate.CurrentValue = StringUtil.RTrim( AV44Impersonate);
            AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Values", (string)(cmbavImpersonate.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTfaenable_cell_Internalname, 1, 0, "px", 0, "px", divTfaenable_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavTfaenable.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavTfaenable_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavTfaenable_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 51,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavTfaenable_Internalname, StringUtil.BoolToStr( AV66TFAEnable), "", " ", chkavTfaenable.Visible, chkavTfaenable.Enabled, "true", context.GetMessage( "WWP_GAM_EnableTwoFactorAuthentication", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,51);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTfaauthenticationtypename_cell_Internalname, 1, 0, "px", 0, "px", divTfaauthenticationtypename_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavTfaauthenticationtypename.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavTfaauthenticationtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavTfaauthenticationtypename_Internalname, context.GetMessage( "WWP_GAM_AuthenticationTypeName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavTfaauthenticationtypename, cmbavTfaauthenticationtypename_Internalname, StringUtil.RTrim( AV65TFAAuthenticationTypeName), 1, cmbavTfaauthenticationtypename_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", cmbavTfaauthenticationtypename.Visible, cmbavTfaauthenticationtypename.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,56);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavTfaauthenticationtypename.CurrentValue = StringUtil.RTrim( AV65TFAAuthenticationTypeName);
            AssignProp(sPrefix, false, cmbavTfaauthenticationtypename_Internalname, "Values", (string)(cmbavTfaauthenticationtypename.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTfafirstfactorauthenticationexpiration_cell_Internalname, 1, 0, "px", 0, "px", divTfafirstfactorauthenticationexpiration_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavTfafirstfactorauthenticationexpiration_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavTfafirstfactorauthenticationexpiration_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavTfafirstfactorauthenticationexpiration_Internalname, context.GetMessage( "WWP_GAM_FirstFactorAuthExpiration", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavTfafirstfactorauthenticationexpiration_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), "ZZZZZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,60);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavTfafirstfactorauthenticationexpiration_Jsonclick, 0, "Attribute", "", "", "", "", edtavTfafirstfactorauthenticationexpiration_Visible, edtavTfafirstfactorauthenticationexpiration_Enabled, 1, "text", "1", 9, "chr", 1, "row", 9, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTfaforceforallusers_cell_Internalname, 1, 0, "px", 0, "px", divTfaforceforallusers_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavTfaforceforallusers.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavTfaforceforallusers_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavTfaforceforallusers_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 65,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavTfaforceforallusers_Internalname, StringUtil.BoolToStr( AV68TFAForceForAllUsers), "", " ", chkavTfaforceforallusers.Visible, chkavTfaforceforallusers.Enabled, "true", context.GetMessage( "WWP_GAM_Force2FAAllUsers", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(65, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,65);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblotpauthentication_Internalname, divTblotpauthentication_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavOtpuseforfirstfactorauthentication_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavOtpuseforfirstfactorauthentication_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 73,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavOtpuseforfirstfactorauthentication_Internalname, StringUtil.BoolToStr( AV61OTPUseForFirstFactorAuthentication), "", " ", 1, chkavOtpuseforfirstfactorauthentication.Enabled, "true", context.GetMessage( "WWP_GAM_UseForFFA", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(73, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,73);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblotpconfiguration_Internalname, divTblotpconfiguration_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavOtpeventvalidateuser_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavOtpeventvalidateuser_Internalname, context.GetMessage( "WWP_GAM_UserValidationEvent", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 81,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavOtpeventvalidateuser, cmbavOtpeventvalidateuser_Internalname, StringUtil.RTrim( AV52OTPEventValidateUser), 1, cmbavOtpeventvalidateuser_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavOtpeventvalidateuser.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,81);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavOtpeventvalidateuser.CurrentValue = StringUtil.RTrim( AV52OTPEventValidateUser);
            AssignProp(sPrefix, false, cmbavOtpeventvalidateuser_Internalname, "Values", (string)(cmbavOtpeventvalidateuser.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavOtpgenerationtype_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavOtpgenerationtype_Internalname, context.GetMessage( "WWP_GAM_CodeGenerationType", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 85,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavOtpgenerationtype, cmbavOtpgenerationtype_Internalname, StringUtil.RTrim( AV54OTPGenerationType), 1, cmbavOtpgenerationtype_Jsonclick, 5, "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"EVOTPGENERATIONTYPE.CLICK."+"'", "svchar", "", 1, cmbavOtpgenerationtype.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,85);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavOtpgenerationtype.CurrentValue = StringUtil.RTrim( AV54OTPGenerationType);
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_Internalname, "Values", (string)(cmbavOtpgenerationtype.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divOtpgenerationtype_customeventgeneratecode_cell_Internalname, 1, 0, "px", 0, "px", divOtpgenerationtype_customeventgeneratecode_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", cmbavOtpgenerationtype_customeventgeneratecode.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavOtpgenerationtype_customeventgeneratecode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, context.GetMessage( "WWP_GAM_EventToGenerateOTPCustom", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 90,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavOtpgenerationtype_customeventgeneratecode, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, StringUtil.RTrim( AV55OTPGenerationType_CustomEventGenerateCode), 1, cmbavOtpgenerationtype_customeventgeneratecode_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", cmbavOtpgenerationtype_customeventgeneratecode.Visible, cmbavOtpgenerationtype_customeventgeneratecode.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,90);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavOtpgenerationtype_customeventgeneratecode.CurrentValue = StringUtil.RTrim( AV55OTPGenerationType_CustomEventGenerateCode);
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, "Values", (string)(cmbavOtpgenerationtype_customeventgeneratecode.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divOtpautogeneratedcodelength_cell_Internalname, 1, 0, "px", 0, "px", divOtpautogeneratedcodelength_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavOtpautogeneratedcodelength_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpautogeneratedcodelength_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpautogeneratedcodelength_Internalname, context.GetMessage( "WWP_GAM_AutoheneratedOTPCodeLength", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 94,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOtpautogeneratedcodelength_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV47OTPAutogeneratedCodeLength), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV47OTPAutogeneratedCodeLength), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,94);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOtpautogeneratedcodelength_Jsonclick, 0, "Attribute", "", "", "", "", edtavOtpautogeneratedcodelength_Visible, edtavOtpautogeneratedcodelength_Enabled, 1, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divOtpgeneratecodeonlynumbers_cell_Internalname, 1, 0, "px", 0, "px", divOtpgeneratecodeonlynumbers_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavOtpgeneratecodeonlynumbers.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavOtpgeneratecodeonlynumbers_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavOtpgeneratecodeonlynumbers_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 99,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavOtpgeneratecodeonlynumbers_Internalname, StringUtil.BoolToStr( AV53OTPGenerateCodeOnlyNumbers), "", " ", chkavOtpgeneratecodeonlynumbers.Visible, chkavOtpgeneratecodeonlynumbers.Enabled, "true", context.GetMessage( "WWP_GAM_GenerateCodeOnlyWithNumbers", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(99, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,99);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpcodeexpirationtimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpcodeexpirationtimeout_Internalname, context.GetMessage( "WWP_GAM_CodeExpirationTimeout", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 103,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOtpcodeexpirationtimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV49OTPCodeExpirationTimeout), 9, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV49OTPCodeExpirationTimeout), "ZZZZZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,103);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOtpcodeexpirationtimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavOtpcodeexpirationtimeout_Enabled, 1, "text", "1", 9, "chr", 1, "row", 9, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpmaximumdailynumbercodes_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpmaximumdailynumbercodes_Internalname, context.GetMessage( "WWP_GAM_MaxDailyNumberCodes", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 108,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOtpmaximumdailynumbercodes_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV58OTPMaximumDailyNumberCodes), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV58OTPMaximumDailyNumberCodes), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,108);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOtpmaximumdailynumbercodes_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavOtpmaximumdailynumbercodes_Enabled, 1, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpnumberunsuccessfulretriestolockotp_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpnumberunsuccessfulretriestolockotp_Internalname, context.GetMessage( "WWP_GAM_NumberUnsuccessfullRetries", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 112,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOtpnumberunsuccessfulretriestolockotp_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV60OTPNumberUnsuccessfulRetriesToLockOTP), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV60OTPNumberUnsuccessfulRetriesToLockOTP), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,112);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOtpnumberunsuccessfulretriestolockotp_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavOtpnumberunsuccessfulretriestolockotp_Enabled, 1, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpautounlocktime_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpautounlocktime_Internalname, context.GetMessage( "WWP_GAM_AutomaticOTPUnlockTime", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 117,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOtpautounlocktime_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV48OTPAutoUnlockTime), 9, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV48OTPAutoUnlockTime), "ZZZZZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,117);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOtpautounlocktime_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavOtpautounlocktime_Enabled, 1, "text", "1", 9, "chr", 1, "row", 9, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname, context.GetMessage( "WWP_GAM_NumberUnsuccessfulRetriesLockOTP", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 121,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,121);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Enabled, 1, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblsendandvalidateotpcode_Internalname, divTblsendandvalidateotpcode_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavOtpeventsendcode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavOtpeventsendcode_Internalname, context.GetMessage( "WWP_GAM_SendCodeUsing", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 129,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavOtpeventsendcode, cmbavOtpeventsendcode_Internalname, StringUtil.RTrim( AV50OTPEventSendCode), 1, cmbavOtpeventsendcode_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavOtpeventsendcode.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,129);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavOtpeventsendcode.CurrentValue = StringUtil.RTrim( AV50OTPEventSendCode);
            AssignProp(sPrefix, false, cmbavOtpeventsendcode_Internalname, "Values", (string)(cmbavOtpeventsendcode.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpmailmessagesubject_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpmailmessagesubject_Internalname, context.GetMessage( "WWP_GAM_MailMessageSubject", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 134,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavOtpmailmessagesubject_Internalname, StringUtil.RTrim( AV57OTPMailMessageSubject), StringUtil.RTrim( context.localUtil.Format( AV57OTPMailMessageSubject, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,134);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavOtpmailmessagesubject_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavOtpmailmessagesubject_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavOtpmailmessagebodyhtml_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavOtpmailmessagebodyhtml_Internalname, context.GetMessage( "WWP_GAM_MailMessageHTMLText", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 139,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavOtpmailmessagebodyhtml_Internalname, AV56OTPMailMessageBodyHTML, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,139);\"", 0, 1, edtavOtpmailmessagebodyhtml_Enabled, 1, 80, "chr", 5, "row", 0, StyleString, ClassString, "", "", "400", -1, 0, "", "", -1, true, "", "'"+sPrefix+"'"+",false,"+"'"+""+"'", 0, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavOtpeventvalidatecode_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavOtpeventvalidatecode_Internalname, context.GetMessage( "WWP_GAM_ValidateCodeUsing", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 144,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavOtpeventvalidatecode, cmbavOtpeventvalidatecode_Internalname, StringUtil.RTrim( AV51OTPEventValidateCode), 1, cmbavOtpeventvalidatecode_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavOtpeventvalidatecode.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,144);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavOtpeventvalidatecode.CurrentValue = StringUtil.RTrim( AV51OTPEventValidateCode);
            AssignProp(sPrefix, false, cmbavOtpeventvalidatecode_Internalname, "Values", (string)(cmbavOtpeventvalidatecode.ToJavascriptSource()), true);
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
            GxWebStd.gx_div_start( context, divClientid_cell_Internalname, 1, 0, "px", 0, "px", divClientid_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavClientid_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientid_Internalname, context.GetMessage( "WWP_GAM_ClientId", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 149,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientid_Internalname, AV24ClientId, StringUtil.RTrim( context.localUtil.Format( AV24ClientId, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,149);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientid_Jsonclick, 0, "Attribute", "", "", "", "", edtavClientid_Visible, edtavClientid_Enabled, 1, "text", "", 80, "chr", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divClientsecret_cell_Internalname, 1, 0, "px", 0, "px", divClientsecret_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavClientsecret_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavClientsecret_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavClientsecret_Internalname, context.GetMessage( "WWP_GAM_ClientSecret", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 153,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavClientsecret_Internalname, AV25ClientSecret, StringUtil.RTrim( context.localUtil.Format( AV25ClientSecret, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,153);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavClientsecret_Jsonclick, 0, "Attribute", "", "", "", "", edtavClientsecret_Visible, edtavClientsecret_Enabled, 1, "text", "", 80, "chr", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divVersionpath_cell_Internalname, 1, 0, "px", 0, "px", divVersionpath_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavVersionpath_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavVersionpath_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavVersionpath_Internalname, context.GetMessage( "WWP_GAM_VersionPath", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 158,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavVersionpath_Internalname, StringUtil.RTrim( AV70VersionPath), StringUtil.RTrim( context.localUtil.Format( AV70VersionPath, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,158);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavVersionpath_Jsonclick, 0, "Attribute", "", "", "", "", edtavVersionpath_Visible, edtavVersionpath_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divSiteurl_cell_Internalname, 1, 0, "px", 0, "px", divSiteurl_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavSiteurl_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSiteurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSiteurl_Internalname, context.GetMessage( "WWP_GAM_LocalSiteURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 162,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSiteurl_Internalname, AV62SiteURL, StringUtil.RTrim( context.localUtil.Format( AV62SiteURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,162);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSiteurl_Jsonclick, 0, "Attribute", "", "", "", "", edtavSiteurl_Visible, edtavSiteurl_Enabled, 1, "text", "", 80, "chr", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divAutocompletevirtualdirectory_cell_Internalname, 1, 0, "px", 0, "px", divAutocompletevirtualdirectory_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavAutocompletevirtualdirectory.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAutocompletevirtualdirectory_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAutocompletevirtualdirectory_Internalname, context.GetMessage( "WWP_GAM_AutocompleteVirtualDirectory", ""), " AttributeCheckBoxLabel BootstrapTooltipRightLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 167,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox BootstrapTooltipRight";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAutocompletevirtualdirectory_Internalname, StringUtil.BoolToStr( AV85AutocompleteVirtualDirectory), chkavAutocompletevirtualdirectory.TooltipText, context.GetMessage( "WWP_GAM_AutocompleteVirtualDirectory", ""), chkavAutocompletevirtualdirectory.Visible, chkavAutocompletevirtualdirectory.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(167, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,167);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divSiteurlcallbackiscustom_cell_Internalname, 1, 0, "px", 0, "px", divSiteurlcallbackiscustom_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavSiteurlcallbackiscustom.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavSiteurlcallbackiscustom_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavSiteurlcallbackiscustom_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 171,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavSiteurlcallbackiscustom_Internalname, StringUtil.BoolToStr( AV63SiteURLCallbackIsCustom), "", " ", chkavSiteurlcallbackiscustom.Visible, chkavSiteurlcallbackiscustom.Enabled, "true", context.GetMessage( "WWP_GAM_CustomCallbackURL", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(171, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,171);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divConsumerkey_cell_Internalname, 1, 0, "px", 0, "px", divConsumerkey_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavConsumerkey_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavConsumerkey_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavConsumerkey_Internalname, context.GetMessage( "WWP_GAM_ConsumerKey", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 176,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavConsumerkey_Internalname, StringUtil.RTrim( AV26ConsumerKey), StringUtil.RTrim( context.localUtil.Format( AV26ConsumerKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,176);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavConsumerkey_Jsonclick, 0, "Attribute", "", "", "", "", edtavConsumerkey_Visible, edtavConsumerkey_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divConsumersecret_cell_Internalname, 1, 0, "px", 0, "px", divConsumersecret_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavConsumersecret_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavConsumersecret_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavConsumersecret_Internalname, context.GetMessage( "WWP_GAM_ConsumerSecret", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 180,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavConsumersecret_Internalname, StringUtil.RTrim( AV27ConsumerSecret), StringUtil.RTrim( context.localUtil.Format( AV27ConsumerSecret, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,180);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavConsumersecret_Jsonclick, 0, "Attribute", "", "", "", "", edtavConsumersecret_Visible, edtavConsumersecret_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divCallbackurl_cell_Internalname, 1, 0, "px", 0, "px", divCallbackurl_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavCallbackurl_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCallbackurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCallbackurl_Internalname, context.GetMessage( "WWP_GAM_CallbackURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 185,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCallbackurl_Internalname, AV23CallbackURL, StringUtil.RTrim( context.localUtil.Format( AV23CallbackURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,185);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCallbackurl_Jsonclick, 0, "Attribute", "", "", "", "", edtavCallbackurl_Visible, edtavCallbackurl_Enabled, 1, "text", "", 80, "chr", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divCuautocompletevirtualdirectory_cell_Internalname, 1, 0, "px", 0, "px", divCuautocompletevirtualdirectory_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", chkavCuautocompletevirtualdirectory.Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavCuautocompletevirtualdirectory_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavCuautocompletevirtualdirectory_Internalname, context.GetMessage( "WWP_GAM_AutocompletecallbackURLwithvirtualdirectory", ""), " AttributeCheckBoxLabel BootstrapTooltipRightLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 189,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox BootstrapTooltipRight";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavCuautocompletevirtualdirectory_Internalname, StringUtil.BoolToStr( AV86CUAutocompleteVirtualDirectory), chkavCuautocompletevirtualdirectory.TooltipText, context.GetMessage( "WWP_GAM_AutocompletecallbackURLwithvirtualdirectory", ""), chkavCuautocompletevirtualdirectory.Visible, chkavCuautocompletevirtualdirectory.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(189, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,189);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblscopes_Internalname, divTblscopes_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockrequestscopes_Internalname, context.GetMessage( "WWP_GAM_RequestTheseScopes", ""), "", "", lblTextblockrequestscopes_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock AttributeSizeLarge", 0, "", 1, 1, 0, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAdduseradditionaldatascope_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAdduseradditionaldatascope_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 200,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAdduseradditionaldatascope_Internalname, StringUtil.BoolToStr( AV7AddUserAdditionalDataScope), "", " ", 1, chkavAdduseradditionaldatascope.Enabled, "true", context.GetMessage( "WWP_GAM_AddGAMUserAddDataScope", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(200, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,200);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAdduserdatascope_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAdduserdatascope_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 204,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAdduserdatascope_Internalname, StringUtil.BoolToStr( AV88AddUserDataScope), "", " ", 1, chkavAdduserdatascope.Enabled, "true", context.GetMessage( "WWP_GAM_AddGAMUserDataScope", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(204, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,204);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAddinitialpropertiesscope_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAddinitialpropertiesscope_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 209,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAddinitialpropertiesscope_Internalname, StringUtil.BoolToStr( AV5AddInitialPropertiesScope), "", " ", 1, chkavAddinitialpropertiesscope.Enabled, "true", context.GetMessage( "WWP_GAM_AddGAMSessionInitialPropScope", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(209, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,209);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAddapplicationdatascope_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAddapplicationdatascope_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 213,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAddapplicationdatascope_Internalname, StringUtil.BoolToStr( AV89AddApplicationDataScope), "", " ", 1, chkavAddapplicationdatascope.Enabled, "true", context.GetMessage( "WWP_GAM_session_app_data", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(213, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,213);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divAdditionalscope_cell_Internalname, 1, 0, "px", 0, "px", divAdditionalscope_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavAdditionalscope_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAdditionalscope_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAdditionalscope_Internalname, context.GetMessage( "WWP_GAM_AdditionalScope", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 217,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAdditionalscope_Internalname, AV6AdditionalScope, StringUtil.RTrim( context.localUtil.Format( AV6AdditionalScope, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,217);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAdditionalscope_Jsonclick, 0, "Attribute", "", "", "", "", edtavAdditionalscope_Visible, edtavAdditionalscope_Enabled, 1, "text", "", 80, "chr", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGamrauthenticationtypename_cell_Internalname, 1, 0, "px", 0, "px", divGamrauthenticationtypename_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavGamrauthenticationtypename_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGamrauthenticationtypename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGamrauthenticationtypename_Internalname, context.GetMessage( "WWP_GAM_RAuthTypeName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 222,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGamrauthenticationtypename_Internalname, StringUtil.RTrim( AV40GAMRAuthenticationTypeName), StringUtil.RTrim( context.localUtil.Format( AV40GAMRAuthenticationTypeName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,222);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGamrauthenticationtypename_Jsonclick, 0, "Attribute", "", "", "", "", edtavGamrauthenticationtypename_Visible, edtavGamrauthenticationtypename_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMAuthenticationTypeName", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblserverhost_Internalname, divTblserverhost_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGamrserverurl_cell_Internalname, 1, 0, "px", 0, "px", divGamrserverurl_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGamrserverurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGamrserverurl_Internalname, context.GetMessage( "WWP_GAM_RemoteServerURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 230,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGamrserverurl_Internalname, AV43GAMRServerURL, StringUtil.RTrim( context.localUtil.Format( AV43GAMRServerURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,230);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGamrserverurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavGamrserverurl_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGamrprivateencryptkey_cell_Internalname, 1, 0, "px", 0, "px", divGamrprivateencryptkey_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavGamrprivateencryptkey_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGamrprivateencryptkey_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGamrprivateencryptkey_Internalname, context.GetMessage( "WWP_GAM_PrivateEncryptionKey", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 234,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGamrprivateencryptkey_Internalname, StringUtil.RTrim( AV41GAMRPrivateEncryptKey), StringUtil.RTrim( context.localUtil.Format( AV41GAMRPrivateEncryptKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,234);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGamrprivateencryptkey_Jsonclick, 0, "Attribute", "", "", "", "", edtavGamrprivateencryptkey_Visible, edtavGamrprivateencryptkey_Enabled, 1, "text", "", 32, "chr", 1, "row", 32, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMEncryptionKey", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGamrrepositoryguid_cell_Internalname, 1, 0, "px", 0, "px", divGamrrepositoryguid_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavGamrrepositoryguid_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavGamrrepositoryguid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavGamrrepositoryguid_Internalname, context.GetMessage( "WWP_GAM_GUID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 239,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavGamrrepositoryguid_Internalname, StringUtil.RTrim( AV42GAMRRepositoryGUID), StringUtil.RTrim( context.localUtil.Format( AV42GAMRRepositoryGUID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,239);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavGamrrepositoryguid_Jsonclick, 0, "Attribute", "", "", "", "", edtavGamrrepositoryguid_Visible, edtavGamrrepositoryguid_Enabled, 1, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavAutovalidateexternaltokenandrefresh_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavAutovalidateexternaltokenandrefresh_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 243,'" + sPrefix + "',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavAutovalidateexternaltokenandrefresh_Internalname, StringUtil.BoolToStr( AV21AutovalidateExternalTokenAndRefresh), "", " ", 1, chkavAutovalidateexternaltokenandrefresh.Enabled, "true", context.GetMessage( "WWP_GAM_ValidateExternalToken", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(243, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,243);\"");
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
            GxWebStd.gx_div_start( context, divTblwebservice_Internalname, divTblwebservice_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavWsversion_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavWsversion_Internalname, context.GetMessage( "WWP_GAM_WebServiceversion", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 251,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavWsversion, cmbavWsversion_Internalname, StringUtil.RTrim( AV80WSVersion), 1, cmbavWsversion_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavWsversion.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,251);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavWsversion.CurrentValue = StringUtil.RTrim( AV80WSVersion);
            AssignProp(sPrefix, false, cmbavWsversion_Internalname, "Values", (string)(cmbavWsversion.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedwsprivateencryptkey_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockwsprivateencryptkey_Internalname, context.GetMessage( "WWP_GAM_PrivateEncryptionKey", ""), "", "", lblTextblockwsprivateencryptkey_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_258_1I2( true) ;
         }
         else
         {
            wb_table1_258_1I2( false) ;
         }
         return  ;
      }

      protected void wb_table1_258_1I2e( bool wbgen )
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
            GxWebStd.gx_div_start( context, divWsservername_cell_Internalname, 1, 0, "px", 0, "px", divWsservername_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWsservername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWsservername_Internalname, context.GetMessage( "WWP_GAM_Servername", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 269,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWsservername_Internalname, StringUtil.RTrim( AV76WSServerName), StringUtil.RTrim( context.localUtil.Format( AV76WSServerName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,269);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWsservername_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWsservername_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divWsserverport_cell_Internalname, 1, 0, "px", 0, "px", divWsserverport_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWsserverport_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWsserverport_Internalname, context.GetMessage( "WWP_GAM_Serverport", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 273,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWsserverport_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV77WSServerPort), 5, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV77WSServerPort), "ZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,273);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWsserverport_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWsserverport_Enabled, 1, "text", "1", 5, "chr", 1, "row", 5, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWsserverbaseurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWsserverbaseurl_Internalname, context.GetMessage( "WWP_GAM_BaseURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 278,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWsserverbaseurl_Internalname, AV75WSServerBaseURL, StringUtil.RTrim( context.localUtil.Format( AV75WSServerBaseURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,278);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWsserverbaseurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWsserverbaseurl_Enabled, 1, "text", "", 80, "chr", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavWsserversecureprotocol_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavWsserversecureprotocol_Internalname, context.GetMessage( "WWP_GAM_Secureprotocol", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 282,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavWsserversecureprotocol, cmbavWsserversecureprotocol_Internalname, StringUtil.Trim( StringUtil.Str( (decimal)(AV78WSServerSecureProtocol), 1, 0)), 1, cmbavWsserversecureprotocol_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "int", "", 1, cmbavWsserversecureprotocol.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,282);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavWsserversecureprotocol.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV78WSServerSecureProtocol), 1, 0));
            AssignProp(sPrefix, false, cmbavWsserversecureprotocol_Internalname, "Values", (string)(cmbavWsserversecureprotocol.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWstimeout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWstimeout_Internalname, context.GetMessage( "WWP_GAM_Timeout", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 287,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWstimeout_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV79WSTimeout), 5, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(AV79WSTimeout), "ZZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,287);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWstimeout_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWstimeout_Enabled, 1, "text", "1", 5, "chr", 1, "row", 5, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWspackage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWspackage_Internalname, context.GetMessage( "WWP_GAM_WebServicepackage", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 291,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWspackage_Internalname, StringUtil.RTrim( AV73WSPackage), StringUtil.RTrim( context.localUtil.Format( AV73WSPackage, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,291);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWspackage_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWspackage_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divWsname_cell_Internalname, 1, 0, "px", 0, "px", divWsname_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWsname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWsname_Internalname, context.GetMessage( "WWP_GAM_WebServiceName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 296,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWsname_Internalname, StringUtil.RTrim( AV72WSName), StringUtil.RTrim( context.localUtil.Format( AV72WSName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,296);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWsname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWsname_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavWsextension_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWsextension_Internalname, context.GetMessage( "WWP_GAM_WebServiceExtension", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 300,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWsextension_Internalname, StringUtil.RTrim( AV71WSExtension), StringUtil.RTrim( context.localUtil.Format( AV71WSExtension, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,300);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWsextension_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWsextension_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblexternal_Internalname, divTblexternal_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavCusversion_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavCusversion_Internalname, context.GetMessage( "WWP_GAM_JSONversion", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 307,'" + sPrefix + "',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavCusversion, cmbavCusversion_Internalname, StringUtil.RTrim( AV32CusVersion), 1, cmbavCusversion_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavCusversion.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,307);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            cmbavCusversion.CurrentValue = StringUtil.RTrim( AV32CusVersion);
            AssignProp(sPrefix, false, cmbavCusversion_Internalname, "Values", (string)(cmbavCusversion.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedcusprivateencryptkey_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockcusprivateencryptkey_Internalname, context.GetMessage( "WWP_GAM_PrivateEncryptionKey", ""), "", "", lblTextblockcusprivateencryptkey_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table2_314_1I2( true) ;
         }
         else
         {
            wb_table2_314_1I2( false) ;
         }
         return  ;
      }

      protected void wb_table2_314_1I2e( bool wbgen )
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
            GxWebStd.gx_div_start( context, divCusfilename_cell_Internalname, 1, 0, "px", 0, "px", divCusfilename_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCusfilename_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCusfilename_Internalname, context.GetMessage( "WWP_GAM_FileName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 325,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCusfilename_Internalname, StringUtil.RTrim( AV29CusFileName), StringUtil.RTrim( context.localUtil.Format( AV29CusFileName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,325);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCusfilename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCusfilename_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCuspackage_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCuspackage_Internalname, context.GetMessage( "WWP_GAM_Package", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 329,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCuspackage_Internalname, StringUtil.RTrim( AV30CusPackage), StringUtil.RTrim( context.localUtil.Format( AV30CusPackage, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,329);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCuspackage_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCuspackage_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divCusclassname_cell_Internalname, 1, 0, "px", 0, "px", divCusclassname_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCusclassname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCusclassname_Internalname, context.GetMessage( "WWP_GAM_ClassName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 334,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCusclassname_Internalname, StringUtil.RTrim( AV28CusClassName), StringUtil.RTrim( context.localUtil.Format( AV28CusClassName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,334);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCusclassname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCusclassname_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCusmethod_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCusmethod_Internalname, context.GetMessage( "WWP_GAM_MethodName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 338,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCusmethod_Internalname, StringUtil.RTrim( AV87CusMethod), StringUtil.RTrim( context.localUtil.Format( AV87CusMethod, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,338);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCusmethod_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCusmethod_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
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
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 343,'" + sPrefix + "',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "", bttBtnenter_Caption, bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtnenter_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 345,'" + sPrefix + "',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
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

      protected void START1I2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( StringUtil.Len( sPrefix) != 0 )
         {
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.isSpaRequest( ) )
            {
               if ( context.ExposeMetadata( ) )
               {
                  Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
               }
            }
            Form.Meta.addItem("description", context.GetMessage( "Authentication Type Entry General", ""), 0) ;
            context.wjLoc = "";
            context.nUserReturn = 0;
            context.wbHandled = 0;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               sXEvt = cgiGet( "_EventName");
               if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
               {
               }
            }
         }
         wbErr = false;
         if ( ( StringUtil.Len( sPrefix) == 0 ) || ( nDraw == 1 ) )
         {
            if ( nDoneStart == 0 )
            {
               STRUP1I0( ) ;
            }
         }
      }

      protected void WS1I2( )
      {
         START1I2( ) ;
         EVT1I2( ) ;
      }

      protected void EVT1I2( )
      {
         sXEvt = cgiGet( "_EventName");
         if ( ( ( ( StringUtil.Len( sPrefix) == 0 ) ) || ( StringUtil.StringSearch( sXEvt, sPrefix, 1) > 0 ) ) && ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               if ( context.wbHandled == 0 )
               {
                  if ( StringUtil.Len( sPrefix) == 0 )
                  {
                     sEvt = cgiGet( "_EventName");
                     EvtGridId = cgiGet( "_EventGridId");
                     EvtRowId = cgiGet( "_EventRowId");
                  }
                  if ( StringUtil.Len( sEvt) > 0 )
                  {
                     sEvtType = StringUtil.Left( sEvt, 1);
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E111I2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOGENKEYCUSTOM'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoGenKeyCustom' */
                                    E121I2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOGENKEY'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoGenKey' */
                                    E131I2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       if ( ! Rfr0gs )
                                       {
                                          /* Execute user event: Enter */
                                          E141I2 ();
                                       }
                                       dynload_actions( ) ;
                                    }
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VTFAENABLE.CLICK") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E151I2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VOTPGENERATIONTYPE.CLICK") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    E161I2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Load */
                                    E171I2 ();
                                 }
                              }
                              /* No code required for Cancel button. It is implemented as the Reset button. */
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1I0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = cmbavFunctionid_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
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

      protected void WE1I2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm1I2( ) ;
            }
         }
      }

      protected void PA1I2( )
      {
         if ( nDonePA == 0 )
         {
            if ( StringUtil.Len( sPrefix) != 0 )
            {
               initialize_properties( ) ;
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            if ( StringUtil.Len( sPrefix) == 0 )
            {
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
                  if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamwcauthenticationtypeentrygeneral.aspx")), "gamwcauthenticationtypeentrygeneral.aspx") == 0 ) )
                  {
                     SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamwcauthenticationtypeentrygeneral.aspx")))) ;
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
            }
            if ( ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
            {
               if ( StringUtil.Len( sPrefix) == 0 )
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
                     if ( toggleJsOutput )
                     {
                        if ( context.isSpaRequest( ) )
                        {
                           enableJsOutput();
                        }
                     }
                  }
               }
            }
            toggleJsOutput = isJsOutputEnabled( );
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
            }
            init_web_controls( ) ;
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               if ( toggleJsOutput )
               {
                  if ( context.isSpaRequest( ) )
                  {
                     enableJsOutput();
                  }
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
               GX_FocusControl = cmbavFunctionid_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
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
         if ( cmbavFunctionid.ItemCount > 0 )
         {
            AV37FunctionId = cmbavFunctionid.getValidValue(AV37FunctionId);
            AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavFunctionid.CurrentValue = StringUtil.RTrim( AV37FunctionId);
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Values", cmbavFunctionid.ToJavascriptSource(), true);
         }
         AV45IsEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV45IsEnable));
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         if ( cmbavImpersonate.ItemCount > 0 )
         {
            AV44Impersonate = cmbavImpersonate.getValidValue(AV44Impersonate);
            AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavImpersonate.CurrentValue = StringUtil.RTrim( AV44Impersonate);
            AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Values", cmbavImpersonate.ToJavascriptSource(), true);
         }
         AV66TFAEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV66TFAEnable));
         AssignAttri(sPrefix, false, "AV66TFAEnable", AV66TFAEnable);
         if ( cmbavTfaauthenticationtypename.ItemCount > 0 )
         {
            AV65TFAAuthenticationTypeName = cmbavTfaauthenticationtypename.getValidValue(AV65TFAAuthenticationTypeName);
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavTfaauthenticationtypename.CurrentValue = StringUtil.RTrim( AV65TFAAuthenticationTypeName);
            AssignProp(sPrefix, false, cmbavTfaauthenticationtypename_Internalname, "Values", cmbavTfaauthenticationtypename.ToJavascriptSource(), true);
         }
         AV68TFAForceForAllUsers = StringUtil.StrToBool( StringUtil.BoolToStr( AV68TFAForceForAllUsers));
         AssignAttri(sPrefix, false, "AV68TFAForceForAllUsers", AV68TFAForceForAllUsers);
         AV61OTPUseForFirstFactorAuthentication = StringUtil.StrToBool( StringUtil.BoolToStr( AV61OTPUseForFirstFactorAuthentication));
         AssignAttri(sPrefix, false, "AV61OTPUseForFirstFactorAuthentication", AV61OTPUseForFirstFactorAuthentication);
         if ( cmbavOtpeventvalidateuser.ItemCount > 0 )
         {
            AV52OTPEventValidateUser = cmbavOtpeventvalidateuser.getValidValue(AV52OTPEventValidateUser);
            AssignAttri(sPrefix, false, "AV52OTPEventValidateUser", AV52OTPEventValidateUser);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavOtpeventvalidateuser.CurrentValue = StringUtil.RTrim( AV52OTPEventValidateUser);
            AssignProp(sPrefix, false, cmbavOtpeventvalidateuser_Internalname, "Values", cmbavOtpeventvalidateuser.ToJavascriptSource(), true);
         }
         if ( cmbavOtpgenerationtype.ItemCount > 0 )
         {
            AV54OTPGenerationType = cmbavOtpgenerationtype.getValidValue(AV54OTPGenerationType);
            AssignAttri(sPrefix, false, "AV54OTPGenerationType", AV54OTPGenerationType);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavOtpgenerationtype.CurrentValue = StringUtil.RTrim( AV54OTPGenerationType);
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_Internalname, "Values", cmbavOtpgenerationtype.ToJavascriptSource(), true);
         }
         if ( cmbavOtpgenerationtype_customeventgeneratecode.ItemCount > 0 )
         {
            AV55OTPGenerationType_CustomEventGenerateCode = cmbavOtpgenerationtype_customeventgeneratecode.getValidValue(AV55OTPGenerationType_CustomEventGenerateCode);
            AssignAttri(sPrefix, false, "AV55OTPGenerationType_CustomEventGenerateCode", AV55OTPGenerationType_CustomEventGenerateCode);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavOtpgenerationtype_customeventgeneratecode.CurrentValue = StringUtil.RTrim( AV55OTPGenerationType_CustomEventGenerateCode);
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, "Values", cmbavOtpgenerationtype_customeventgeneratecode.ToJavascriptSource(), true);
         }
         AV53OTPGenerateCodeOnlyNumbers = StringUtil.StrToBool( StringUtil.BoolToStr( AV53OTPGenerateCodeOnlyNumbers));
         AssignAttri(sPrefix, false, "AV53OTPGenerateCodeOnlyNumbers", AV53OTPGenerateCodeOnlyNumbers);
         if ( cmbavOtpeventsendcode.ItemCount > 0 )
         {
            AV50OTPEventSendCode = cmbavOtpeventsendcode.getValidValue(AV50OTPEventSendCode);
            AssignAttri(sPrefix, false, "AV50OTPEventSendCode", AV50OTPEventSendCode);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavOtpeventsendcode.CurrentValue = StringUtil.RTrim( AV50OTPEventSendCode);
            AssignProp(sPrefix, false, cmbavOtpeventsendcode_Internalname, "Values", cmbavOtpeventsendcode.ToJavascriptSource(), true);
         }
         if ( cmbavOtpeventvalidatecode.ItemCount > 0 )
         {
            AV51OTPEventValidateCode = cmbavOtpeventvalidatecode.getValidValue(AV51OTPEventValidateCode);
            AssignAttri(sPrefix, false, "AV51OTPEventValidateCode", AV51OTPEventValidateCode);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavOtpeventvalidatecode.CurrentValue = StringUtil.RTrim( AV51OTPEventValidateCode);
            AssignProp(sPrefix, false, cmbavOtpeventvalidatecode_Internalname, "Values", cmbavOtpeventvalidatecode.ToJavascriptSource(), true);
         }
         AV85AutocompleteVirtualDirectory = StringUtil.StrToBool( StringUtil.BoolToStr( AV85AutocompleteVirtualDirectory));
         AssignAttri(sPrefix, false, "AV85AutocompleteVirtualDirectory", AV85AutocompleteVirtualDirectory);
         AV63SiteURLCallbackIsCustom = StringUtil.StrToBool( StringUtil.BoolToStr( AV63SiteURLCallbackIsCustom));
         AssignAttri(sPrefix, false, "AV63SiteURLCallbackIsCustom", AV63SiteURLCallbackIsCustom);
         AV86CUAutocompleteVirtualDirectory = StringUtil.StrToBool( StringUtil.BoolToStr( AV86CUAutocompleteVirtualDirectory));
         AssignAttri(sPrefix, false, "AV86CUAutocompleteVirtualDirectory", AV86CUAutocompleteVirtualDirectory);
         AV7AddUserAdditionalDataScope = StringUtil.StrToBool( StringUtil.BoolToStr( AV7AddUserAdditionalDataScope));
         AssignAttri(sPrefix, false, "AV7AddUserAdditionalDataScope", AV7AddUserAdditionalDataScope);
         AV88AddUserDataScope = StringUtil.StrToBool( StringUtil.BoolToStr( AV88AddUserDataScope));
         AssignAttri(sPrefix, false, "AV88AddUserDataScope", AV88AddUserDataScope);
         AV5AddInitialPropertiesScope = StringUtil.StrToBool( StringUtil.BoolToStr( AV5AddInitialPropertiesScope));
         AssignAttri(sPrefix, false, "AV5AddInitialPropertiesScope", AV5AddInitialPropertiesScope);
         AV89AddApplicationDataScope = StringUtil.StrToBool( StringUtil.BoolToStr( AV89AddApplicationDataScope));
         AssignAttri(sPrefix, false, "AV89AddApplicationDataScope", AV89AddApplicationDataScope);
         AV21AutovalidateExternalTokenAndRefresh = StringUtil.StrToBool( StringUtil.BoolToStr( AV21AutovalidateExternalTokenAndRefresh));
         AssignAttri(sPrefix, false, "AV21AutovalidateExternalTokenAndRefresh", AV21AutovalidateExternalTokenAndRefresh);
         if ( cmbavWsversion.ItemCount > 0 )
         {
            AV80WSVersion = cmbavWsversion.getValidValue(AV80WSVersion);
            AssignAttri(sPrefix, false, "AV80WSVersion", AV80WSVersion);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavWsversion.CurrentValue = StringUtil.RTrim( AV80WSVersion);
            AssignProp(sPrefix, false, cmbavWsversion_Internalname, "Values", cmbavWsversion.ToJavascriptSource(), true);
         }
         if ( cmbavWsserversecureprotocol.ItemCount > 0 )
         {
            AV78WSServerSecureProtocol = (short)(Math.Round(NumberUtil.Val( cmbavWsserversecureprotocol.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(AV78WSServerSecureProtocol), 1, 0))), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV78WSServerSecureProtocol", StringUtil.Str( (decimal)(AV78WSServerSecureProtocol), 1, 0));
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavWsserversecureprotocol.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(AV78WSServerSecureProtocol), 1, 0));
            AssignProp(sPrefix, false, cmbavWsserversecureprotocol_Internalname, "Values", cmbavWsserversecureprotocol.ToJavascriptSource(), true);
         }
         if ( cmbavCusversion.ItemCount > 0 )
         {
            AV32CusVersion = cmbavCusversion.getValidValue(AV32CusVersion);
            AssignAttri(sPrefix, false, "AV32CusVersion", AV32CusVersion);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavCusversion.CurrentValue = StringUtil.RTrim( AV32CusVersion);
            AssignProp(sPrefix, false, cmbavCusversion_Internalname, "Values", cmbavCusversion.ToJavascriptSource(), true);
         }
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF1I2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF1I2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E171I2 ();
            WB1I0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes1I2( )
      {
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1I0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E111I2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            wcpOGx_mode = cgiGet( sPrefix+"wcpOGx_mode");
            wcpOAV46Name = cgiGet( sPrefix+"wcpOAV46Name");
            wcpOAV69TypeId = cgiGet( sPrefix+"wcpOAV69TypeId");
            /* Read variables values. */
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               AV46Name = cgiGet( edtavName_Internalname);
               AssignAttri(sPrefix, false, "AV46Name", AV46Name);
            }
            cmbavFunctionid.CurrentValue = cgiGet( cmbavFunctionid_Internalname);
            AV37FunctionId = cgiGet( cmbavFunctionid_Internalname);
            AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
            AV45IsEnable = StringUtil.StrToBool( cgiGet( chkavIsenable_Internalname));
            AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
            AV33Dsc = cgiGet( edtavDsc_Internalname);
            AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
            AV64SmallImageName = cgiGet( edtavSmallimagename_Internalname);
            AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
            AV22BigImageName = cgiGet( edtavBigimagename_Internalname);
            AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
            cmbavImpersonate.CurrentValue = cgiGet( cmbavImpersonate_Internalname);
            AV44Impersonate = cgiGet( cmbavImpersonate_Internalname);
            AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
            AV66TFAEnable = StringUtil.StrToBool( cgiGet( chkavTfaenable_Internalname));
            AssignAttri(sPrefix, false, "AV66TFAEnable", AV66TFAEnable);
            cmbavTfaauthenticationtypename.CurrentValue = cgiGet( cmbavTfaauthenticationtypename_Internalname);
            AV65TFAAuthenticationTypeName = cgiGet( cmbavTfaauthenticationtypename_Internalname);
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavTfafirstfactorauthenticationexpiration_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavTfafirstfactorauthenticationexpiration_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vTFAFIRSTFACTORAUTHENTICATIONEXPIRATION");
               GX_FocusControl = edtavTfafirstfactorauthenticationexpiration_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV67TFAFirstFactorAuthenticationExpiration = 0;
               AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            }
            else
            {
               AV67TFAFirstFactorAuthenticationExpiration = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavTfafirstfactorauthenticationexpiration_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            }
            AV68TFAForceForAllUsers = StringUtil.StrToBool( cgiGet( chkavTfaforceforallusers_Internalname));
            AssignAttri(sPrefix, false, "AV68TFAForceForAllUsers", AV68TFAForceForAllUsers);
            AV61OTPUseForFirstFactorAuthentication = StringUtil.StrToBool( cgiGet( chkavOtpuseforfirstfactorauthentication_Internalname));
            AssignAttri(sPrefix, false, "AV61OTPUseForFirstFactorAuthentication", AV61OTPUseForFirstFactorAuthentication);
            cmbavOtpeventvalidateuser.CurrentValue = cgiGet( cmbavOtpeventvalidateuser_Internalname);
            AV52OTPEventValidateUser = cgiGet( cmbavOtpeventvalidateuser_Internalname);
            AssignAttri(sPrefix, false, "AV52OTPEventValidateUser", AV52OTPEventValidateUser);
            cmbavOtpgenerationtype.CurrentValue = cgiGet( cmbavOtpgenerationtype_Internalname);
            AV54OTPGenerationType = cgiGet( cmbavOtpgenerationtype_Internalname);
            AssignAttri(sPrefix, false, "AV54OTPGenerationType", AV54OTPGenerationType);
            cmbavOtpgenerationtype_customeventgeneratecode.CurrentValue = cgiGet( cmbavOtpgenerationtype_customeventgeneratecode_Internalname);
            AV55OTPGenerationType_CustomEventGenerateCode = cgiGet( cmbavOtpgenerationtype_customeventgeneratecode_Internalname);
            AssignAttri(sPrefix, false, "AV55OTPGenerationType_CustomEventGenerateCode", AV55OTPGenerationType_CustomEventGenerateCode);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavOtpautogeneratedcodelength_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavOtpautogeneratedcodelength_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vOTPAUTOGENERATEDCODELENGTH");
               GX_FocusControl = edtavOtpautogeneratedcodelength_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV47OTPAutogeneratedCodeLength = 0;
               AssignAttri(sPrefix, false, "AV47OTPAutogeneratedCodeLength", StringUtil.LTrimStr( (decimal)(AV47OTPAutogeneratedCodeLength), 4, 0));
            }
            else
            {
               AV47OTPAutogeneratedCodeLength = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavOtpautogeneratedcodelength_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV47OTPAutogeneratedCodeLength", StringUtil.LTrimStr( (decimal)(AV47OTPAutogeneratedCodeLength), 4, 0));
            }
            AV53OTPGenerateCodeOnlyNumbers = StringUtil.StrToBool( cgiGet( chkavOtpgeneratecodeonlynumbers_Internalname));
            AssignAttri(sPrefix, false, "AV53OTPGenerateCodeOnlyNumbers", AV53OTPGenerateCodeOnlyNumbers);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavOtpcodeexpirationtimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavOtpcodeexpirationtimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vOTPCODEEXPIRATIONTIMEOUT");
               GX_FocusControl = edtavOtpcodeexpirationtimeout_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV49OTPCodeExpirationTimeout = 0;
               AssignAttri(sPrefix, false, "AV49OTPCodeExpirationTimeout", StringUtil.LTrimStr( (decimal)(AV49OTPCodeExpirationTimeout), 9, 0));
            }
            else
            {
               AV49OTPCodeExpirationTimeout = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavOtpcodeexpirationtimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV49OTPCodeExpirationTimeout", StringUtil.LTrimStr( (decimal)(AV49OTPCodeExpirationTimeout), 9, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavOtpmaximumdailynumbercodes_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavOtpmaximumdailynumbercodes_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vOTPMAXIMUMDAILYNUMBERCODES");
               GX_FocusControl = edtavOtpmaximumdailynumbercodes_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV58OTPMaximumDailyNumberCodes = 0;
               AssignAttri(sPrefix, false, "AV58OTPMaximumDailyNumberCodes", StringUtil.LTrimStr( (decimal)(AV58OTPMaximumDailyNumberCodes), 4, 0));
            }
            else
            {
               AV58OTPMaximumDailyNumberCodes = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavOtpmaximumdailynumbercodes_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV58OTPMaximumDailyNumberCodes", StringUtil.LTrimStr( (decimal)(AV58OTPMaximumDailyNumberCodes), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavOtpnumberunsuccessfulretriestolockotp_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavOtpnumberunsuccessfulretriestolockotp_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vOTPNUMBERUNSUCCESSFULRETRIESTOLOCKOTP");
               GX_FocusControl = edtavOtpnumberunsuccessfulretriestolockotp_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV60OTPNumberUnsuccessfulRetriesToLockOTP = 0;
               AssignAttri(sPrefix, false, "AV60OTPNumberUnsuccessfulRetriesToLockOTP", StringUtil.LTrimStr( (decimal)(AV60OTPNumberUnsuccessfulRetriesToLockOTP), 4, 0));
            }
            else
            {
               AV60OTPNumberUnsuccessfulRetriesToLockOTP = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavOtpnumberunsuccessfulretriestolockotp_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV60OTPNumberUnsuccessfulRetriesToLockOTP", StringUtil.LTrimStr( (decimal)(AV60OTPNumberUnsuccessfulRetriesToLockOTP), 4, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavOtpautounlocktime_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavOtpautounlocktime_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 999999999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vOTPAUTOUNLOCKTIME");
               GX_FocusControl = edtavOtpautounlocktime_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV48OTPAutoUnlockTime = 0;
               AssignAttri(sPrefix, false, "AV48OTPAutoUnlockTime", StringUtil.LTrimStr( (decimal)(AV48OTPAutoUnlockTime), 9, 0));
            }
            else
            {
               AV48OTPAutoUnlockTime = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavOtpautounlocktime_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV48OTPAutoUnlockTime", StringUtil.LTrimStr( (decimal)(AV48OTPAutoUnlockTime), 9, 0));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vOTPNUMBERUNSUCCESSFULRETRIESTOBLOCKUSERBASEDOFOTPLOCKS");
               GX_FocusControl = edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks = 0;
               AssignAttri(sPrefix, false, "AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks", StringUtil.LTrimStr( (decimal)(AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks), 4, 0));
            }
            else
            {
               AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks", StringUtil.LTrimStr( (decimal)(AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks), 4, 0));
            }
            cmbavOtpeventsendcode.CurrentValue = cgiGet( cmbavOtpeventsendcode_Internalname);
            AV50OTPEventSendCode = cgiGet( cmbavOtpeventsendcode_Internalname);
            AssignAttri(sPrefix, false, "AV50OTPEventSendCode", AV50OTPEventSendCode);
            AV57OTPMailMessageSubject = cgiGet( edtavOtpmailmessagesubject_Internalname);
            AssignAttri(sPrefix, false, "AV57OTPMailMessageSubject", AV57OTPMailMessageSubject);
            AV56OTPMailMessageBodyHTML = cgiGet( edtavOtpmailmessagebodyhtml_Internalname);
            AssignAttri(sPrefix, false, "AV56OTPMailMessageBodyHTML", AV56OTPMailMessageBodyHTML);
            cmbavOtpeventvalidatecode.CurrentValue = cgiGet( cmbavOtpeventvalidatecode_Internalname);
            AV51OTPEventValidateCode = cgiGet( cmbavOtpeventvalidatecode_Internalname);
            AssignAttri(sPrefix, false, "AV51OTPEventValidateCode", AV51OTPEventValidateCode);
            AV24ClientId = cgiGet( edtavClientid_Internalname);
            AssignAttri(sPrefix, false, "AV24ClientId", AV24ClientId);
            AV25ClientSecret = cgiGet( edtavClientsecret_Internalname);
            AssignAttri(sPrefix, false, "AV25ClientSecret", AV25ClientSecret);
            AV70VersionPath = cgiGet( edtavVersionpath_Internalname);
            AssignAttri(sPrefix, false, "AV70VersionPath", AV70VersionPath);
            AV62SiteURL = cgiGet( edtavSiteurl_Internalname);
            AssignAttri(sPrefix, false, "AV62SiteURL", AV62SiteURL);
            AV85AutocompleteVirtualDirectory = StringUtil.StrToBool( cgiGet( chkavAutocompletevirtualdirectory_Internalname));
            AssignAttri(sPrefix, false, "AV85AutocompleteVirtualDirectory", AV85AutocompleteVirtualDirectory);
            AV63SiteURLCallbackIsCustom = StringUtil.StrToBool( cgiGet( chkavSiteurlcallbackiscustom_Internalname));
            AssignAttri(sPrefix, false, "AV63SiteURLCallbackIsCustom", AV63SiteURLCallbackIsCustom);
            AV26ConsumerKey = cgiGet( edtavConsumerkey_Internalname);
            AssignAttri(sPrefix, false, "AV26ConsumerKey", AV26ConsumerKey);
            AV27ConsumerSecret = cgiGet( edtavConsumersecret_Internalname);
            AssignAttri(sPrefix, false, "AV27ConsumerSecret", AV27ConsumerSecret);
            AV23CallbackURL = cgiGet( edtavCallbackurl_Internalname);
            AssignAttri(sPrefix, false, "AV23CallbackURL", AV23CallbackURL);
            AV86CUAutocompleteVirtualDirectory = StringUtil.StrToBool( cgiGet( chkavCuautocompletevirtualdirectory_Internalname));
            AssignAttri(sPrefix, false, "AV86CUAutocompleteVirtualDirectory", AV86CUAutocompleteVirtualDirectory);
            AV7AddUserAdditionalDataScope = StringUtil.StrToBool( cgiGet( chkavAdduseradditionaldatascope_Internalname));
            AssignAttri(sPrefix, false, "AV7AddUserAdditionalDataScope", AV7AddUserAdditionalDataScope);
            AV88AddUserDataScope = StringUtil.StrToBool( cgiGet( chkavAdduserdatascope_Internalname));
            AssignAttri(sPrefix, false, "AV88AddUserDataScope", AV88AddUserDataScope);
            AV5AddInitialPropertiesScope = StringUtil.StrToBool( cgiGet( chkavAddinitialpropertiesscope_Internalname));
            AssignAttri(sPrefix, false, "AV5AddInitialPropertiesScope", AV5AddInitialPropertiesScope);
            AV89AddApplicationDataScope = StringUtil.StrToBool( cgiGet( chkavAddapplicationdatascope_Internalname));
            AssignAttri(sPrefix, false, "AV89AddApplicationDataScope", AV89AddApplicationDataScope);
            AV6AdditionalScope = cgiGet( edtavAdditionalscope_Internalname);
            AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
            AV40GAMRAuthenticationTypeName = cgiGet( edtavGamrauthenticationtypename_Internalname);
            AssignAttri(sPrefix, false, "AV40GAMRAuthenticationTypeName", AV40GAMRAuthenticationTypeName);
            AV43GAMRServerURL = cgiGet( edtavGamrserverurl_Internalname);
            AssignAttri(sPrefix, false, "AV43GAMRServerURL", AV43GAMRServerURL);
            AV41GAMRPrivateEncryptKey = cgiGet( edtavGamrprivateencryptkey_Internalname);
            AssignAttri(sPrefix, false, "AV41GAMRPrivateEncryptKey", AV41GAMRPrivateEncryptKey);
            AV42GAMRRepositoryGUID = cgiGet( edtavGamrrepositoryguid_Internalname);
            AssignAttri(sPrefix, false, "AV42GAMRRepositoryGUID", AV42GAMRRepositoryGUID);
            AV21AutovalidateExternalTokenAndRefresh = StringUtil.StrToBool( cgiGet( chkavAutovalidateexternaltokenandrefresh_Internalname));
            AssignAttri(sPrefix, false, "AV21AutovalidateExternalTokenAndRefresh", AV21AutovalidateExternalTokenAndRefresh);
            cmbavWsversion.CurrentValue = cgiGet( cmbavWsversion_Internalname);
            AV80WSVersion = cgiGet( cmbavWsversion_Internalname);
            AssignAttri(sPrefix, false, "AV80WSVersion", AV80WSVersion);
            AV74WSPrivateEncryptKey = cgiGet( edtavWsprivateencryptkey_Internalname);
            AssignAttri(sPrefix, false, "AV74WSPrivateEncryptKey", AV74WSPrivateEncryptKey);
            AV76WSServerName = cgiGet( edtavWsservername_Internalname);
            AssignAttri(sPrefix, false, "AV76WSServerName", AV76WSServerName);
            if ( ( ( context.localUtil.CToN( cgiGet( edtavWsserverport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavWsserverport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 99999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vWSSERVERPORT");
               GX_FocusControl = edtavWsserverport_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV77WSServerPort = 0;
               AssignAttri(sPrefix, false, "AV77WSServerPort", StringUtil.LTrimStr( (decimal)(AV77WSServerPort), 5, 0));
            }
            else
            {
               AV77WSServerPort = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavWsserverport_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV77WSServerPort", StringUtil.LTrimStr( (decimal)(AV77WSServerPort), 5, 0));
            }
            AV75WSServerBaseURL = cgiGet( edtavWsserverbaseurl_Internalname);
            AssignAttri(sPrefix, false, "AV75WSServerBaseURL", AV75WSServerBaseURL);
            cmbavWsserversecureprotocol.CurrentValue = cgiGet( cmbavWsserversecureprotocol_Internalname);
            AV78WSServerSecureProtocol = (short)(Math.Round(NumberUtil.Val( cgiGet( cmbavWsserversecureprotocol_Internalname), "."), 18, MidpointRounding.ToEven));
            AssignAttri(sPrefix, false, "AV78WSServerSecureProtocol", StringUtil.Str( (decimal)(AV78WSServerSecureProtocol), 1, 0));
            if ( ( ( context.localUtil.CToN( cgiGet( edtavWstimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavWstimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 99999 )) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vWSTIMEOUT");
               GX_FocusControl = edtavWstimeout_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV79WSTimeout = 0;
               AssignAttri(sPrefix, false, "AV79WSTimeout", StringUtil.LTrimStr( (decimal)(AV79WSTimeout), 5, 0));
            }
            else
            {
               AV79WSTimeout = (int)(Math.Round(context.localUtil.CToN( cgiGet( edtavWstimeout_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri(sPrefix, false, "AV79WSTimeout", StringUtil.LTrimStr( (decimal)(AV79WSTimeout), 5, 0));
            }
            AV73WSPackage = cgiGet( edtavWspackage_Internalname);
            AssignAttri(sPrefix, false, "AV73WSPackage", AV73WSPackage);
            AV72WSName = cgiGet( edtavWsname_Internalname);
            AssignAttri(sPrefix, false, "AV72WSName", AV72WSName);
            AV71WSExtension = cgiGet( edtavWsextension_Internalname);
            AssignAttri(sPrefix, false, "AV71WSExtension", AV71WSExtension);
            cmbavCusversion.CurrentValue = cgiGet( cmbavCusversion_Internalname);
            AV32CusVersion = cgiGet( cmbavCusversion_Internalname);
            AssignAttri(sPrefix, false, "AV32CusVersion", AV32CusVersion);
            AV31CusPrivateEncryptKey = cgiGet( edtavCusprivateencryptkey_Internalname);
            AssignAttri(sPrefix, false, "AV31CusPrivateEncryptKey", AV31CusPrivateEncryptKey);
            AV29CusFileName = cgiGet( edtavCusfilename_Internalname);
            AssignAttri(sPrefix, false, "AV29CusFileName", AV29CusFileName);
            AV30CusPackage = cgiGet( edtavCuspackage_Internalname);
            AssignAttri(sPrefix, false, "AV30CusPackage", AV30CusPackage);
            AV28CusClassName = cgiGet( edtavCusclassname_Internalname);
            AssignAttri(sPrefix, false, "AV28CusClassName", AV28CusClassName);
            AV87CusMethod = cgiGet( edtavCusmethod_Internalname);
            AssignAttri(sPrefix, false, "AV87CusMethod", AV87CusMethod);
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
         E111I2 ();
         if (returnInSub) return;
      }

      protected void E111I2( )
      {
         /* Start Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Gx_mode, "INS") == 0 )
         {
            edtavName_Enabled = 1;
            AssignProp(sPrefix, false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
         }
         else
         {
            edtavName_Enabled = 0;
            AssignProp(sPrefix, false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
            AV37FunctionId = "OnlyAuthentication";
            AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         }
         if ( StringUtil.StrCmp(AV69TypeId, "APIkey") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEAPIKEY' */
            S112 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEAPPLE' */
            S122 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPECUSTOM' */
            S132 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEEXTERNALWEBSERVICE' */
            S142 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEFACEBOOK' */
            S152 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEGAMLOCAL' */
            S162 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEGAMREMOTE' */
            S172 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEGAMREMOTEREST' */
            S182 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEGOOGLE' */
            S192 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEOTP' */
            S202 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPETWITTER' */
            S212 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 )
         {
            /* Execute user subroutine: 'INITAUTHENTICATIONTYPEWECHAT' */
            S222 ();
            if (returnInSub) return;
         }
         if ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 )
         {
            bttBtnenter_Visible = 0;
            AssignProp(sPrefix, false, bttBtnenter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnenter_Visible), 5, 0), true);
            cmbavFunctionid.Enabled = 1;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            bttBtngenkey_Visible = 0;
            AssignProp(sPrefix, false, bttBtngenkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtngenkey_Visible), 5, 0), true);
            bttBtngenkeycustom_Visible = 0;
            AssignProp(sPrefix, false, bttBtngenkeycustom_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtngenkeycustom_Visible), 5, 0), true);
            cmbavFunctionid.Enabled = 0;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
            chkavIsenable.Enabled = 0;
            AssignProp(sPrefix, false, chkavIsenable_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavIsenable.Enabled), 5, 0), true);
            edtavDsc_Enabled = 0;
            AssignProp(sPrefix, false, edtavDsc_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDsc_Enabled), 5, 0), true);
            edtavSmallimagename_Enabled = 0;
            AssignProp(sPrefix, false, edtavSmallimagename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSmallimagename_Enabled), 5, 0), true);
            edtavBigimagename_Enabled = 0;
            AssignProp(sPrefix, false, edtavBigimagename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavBigimagename_Enabled), 5, 0), true);
            cmbavImpersonate.Enabled = 0;
            AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavImpersonate.Enabled), 5, 0), true);
            cmbavWsversion.Enabled = 0;
            AssignProp(sPrefix, false, cmbavWsversion_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavWsversion.Enabled), 5, 0), true);
            edtavWsprivateencryptkey_Enabled = 0;
            AssignProp(sPrefix, false, edtavWsprivateencryptkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWsprivateencryptkey_Enabled), 5, 0), true);
            edtavWsservername_Enabled = 0;
            AssignProp(sPrefix, false, edtavWsservername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWsservername_Enabled), 5, 0), true);
            edtavWsserverport_Enabled = 0;
            AssignProp(sPrefix, false, edtavWsserverport_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWsserverport_Enabled), 5, 0), true);
            edtavWsserverbaseurl_Enabled = 0;
            AssignProp(sPrefix, false, edtavWsserverbaseurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWsserverbaseurl_Enabled), 5, 0), true);
            cmbavWsserversecureprotocol.Enabled = 0;
            AssignProp(sPrefix, false, cmbavWsserversecureprotocol_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavWsserversecureprotocol.Enabled), 5, 0), true);
            edtavWstimeout_Enabled = 0;
            AssignProp(sPrefix, false, edtavWstimeout_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWstimeout_Enabled), 5, 0), true);
            edtavWspackage_Enabled = 0;
            AssignProp(sPrefix, false, edtavWspackage_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWspackage_Enabled), 5, 0), true);
            edtavWsname_Enabled = 0;
            AssignProp(sPrefix, false, edtavWsname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWsname_Enabled), 5, 0), true);
            edtavWsextension_Enabled = 0;
            AssignProp(sPrefix, false, edtavWsextension_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWsextension_Enabled), 5, 0), true);
            edtavClientid_Enabled = 0;
            AssignProp(sPrefix, false, edtavClientid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientid_Enabled), 5, 0), true);
            edtavClientsecret_Enabled = 0;
            AssignProp(sPrefix, false, edtavClientsecret_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavClientsecret_Enabled), 5, 0), true);
            edtavVersionpath_Enabled = 0;
            AssignProp(sPrefix, false, edtavVersionpath_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavVersionpath_Enabled), 5, 0), true);
            edtavSiteurl_Enabled = 0;
            AssignProp(sPrefix, false, edtavSiteurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSiteurl_Enabled), 5, 0), true);
            chkavAutocompletevirtualdirectory.Enabled = 0;
            AssignProp(sPrefix, false, chkavAutocompletevirtualdirectory_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavAutocompletevirtualdirectory.Enabled), 5, 0), true);
            chkavSiteurlcallbackiscustom.Enabled = 0;
            AssignProp(sPrefix, false, chkavSiteurlcallbackiscustom_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavSiteurlcallbackiscustom.Enabled), 5, 0), true);
            edtavConsumerkey_Enabled = 0;
            AssignProp(sPrefix, false, edtavConsumerkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavConsumerkey_Enabled), 5, 0), true);
            edtavConsumersecret_Enabled = 0;
            AssignProp(sPrefix, false, edtavConsumersecret_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavConsumersecret_Enabled), 5, 0), true);
            edtavCallbackurl_Enabled = 0;
            AssignProp(sPrefix, false, edtavCallbackurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCallbackurl_Enabled), 5, 0), true);
            chkavCuautocompletevirtualdirectory.Enabled = 0;
            AssignProp(sPrefix, false, chkavCuautocompletevirtualdirectory_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavCuautocompletevirtualdirectory.Enabled), 5, 0), true);
            cmbavCusversion.Enabled = 0;
            AssignProp(sPrefix, false, cmbavCusversion_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavCusversion.Enabled), 5, 0), true);
            edtavCusprivateencryptkey_Enabled = 0;
            AssignProp(sPrefix, false, edtavCusprivateencryptkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCusprivateencryptkey_Enabled), 5, 0), true);
            edtavCusfilename_Enabled = 0;
            AssignProp(sPrefix, false, edtavCusfilename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCusfilename_Enabled), 5, 0), true);
            edtavCuspackage_Enabled = 0;
            AssignProp(sPrefix, false, edtavCuspackage_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCuspackage_Enabled), 5, 0), true);
            edtavCusclassname_Enabled = 0;
            AssignProp(sPrefix, false, edtavCusclassname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCusclassname_Enabled), 5, 0), true);
            edtavCusmethod_Enabled = 0;
            AssignProp(sPrefix, false, edtavCusmethod_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCusmethod_Enabled), 5, 0), true);
            chkavAdduserdatascope.Enabled = 0;
            AssignProp(sPrefix, false, chkavAdduserdatascope_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavAdduserdatascope.Enabled), 5, 0), true);
            chkavAdduseradditionaldatascope.Enabled = 0;
            AssignProp(sPrefix, false, chkavAdduseradditionaldatascope_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavAdduseradditionaldatascope.Enabled), 5, 0), true);
            chkavAddinitialpropertiesscope.Enabled = 0;
            AssignProp(sPrefix, false, chkavAddinitialpropertiesscope_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavAddinitialpropertiesscope.Enabled), 5, 0), true);
            chkavAddapplicationdatascope.Enabled = 0;
            AssignProp(sPrefix, false, chkavAddapplicationdatascope_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavAddapplicationdatascope.Enabled), 5, 0), true);
            edtavAdditionalscope_Enabled = 0;
            AssignProp(sPrefix, false, edtavAdditionalscope_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavAdditionalscope_Enabled), 5, 0), true);
            edtavGamrauthenticationtypename_Enabled = 0;
            AssignProp(sPrefix, false, edtavGamrauthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGamrauthenticationtypename_Enabled), 5, 0), true);
            edtavGamrserverurl_Enabled = 0;
            AssignProp(sPrefix, false, edtavGamrserverurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGamrserverurl_Enabled), 5, 0), true);
            edtavGamrprivateencryptkey_Enabled = 0;
            AssignProp(sPrefix, false, edtavGamrprivateencryptkey_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGamrprivateencryptkey_Enabled), 5, 0), true);
            edtavGamrrepositoryguid_Enabled = 0;
            AssignProp(sPrefix, false, edtavGamrrepositoryguid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavGamrrepositoryguid_Enabled), 5, 0), true);
            chkavAutovalidateexternaltokenandrefresh.Enabled = 0;
            AssignProp(sPrefix, false, chkavAutovalidateexternaltokenandrefresh_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavAutovalidateexternaltokenandrefresh.Enabled), 5, 0), true);
            chkavOtpuseforfirstfactorauthentication.Enabled = 0;
            AssignProp(sPrefix, false, chkavOtpuseforfirstfactorauthentication_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavOtpuseforfirstfactorauthentication.Enabled), 5, 0), true);
            chkavTfaenable.Enabled = 0;
            AssignProp(sPrefix, false, chkavTfaenable_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavTfaenable.Enabled), 5, 0), true);
            cmbavTfaauthenticationtypename.Enabled = 0;
            AssignProp(sPrefix, false, cmbavTfaauthenticationtypename_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavTfaauthenticationtypename.Enabled), 5, 0), true);
            edtavTfafirstfactorauthenticationexpiration_Enabled = 0;
            AssignProp(sPrefix, false, edtavTfafirstfactorauthenticationexpiration_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavTfafirstfactorauthenticationexpiration_Enabled), 5, 0), true);
            chkavTfaforceforallusers.Enabled = 0;
            AssignProp(sPrefix, false, chkavTfaforceforallusers_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavTfaforceforallusers.Enabled), 5, 0), true);
            cmbavOtpeventvalidateuser.Enabled = 0;
            AssignProp(sPrefix, false, cmbavOtpeventvalidateuser_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavOtpeventvalidateuser.Enabled), 5, 0), true);
            cmbavOtpgenerationtype.Enabled = 0;
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavOtpgenerationtype.Enabled), 5, 0), true);
            cmbavOtpgenerationtype_customeventgeneratecode.Enabled = 0;
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavOtpgenerationtype_customeventgeneratecode.Enabled), 5, 0), true);
            edtavOtpcodeexpirationtimeout_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpcodeexpirationtimeout_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpcodeexpirationtimeout_Enabled), 5, 0), true);
            edtavOtpmaximumdailynumbercodes_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpmaximumdailynumbercodes_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpmaximumdailynumbercodes_Enabled), 5, 0), true);
            edtavOtpautogeneratedcodelength_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpautogeneratedcodelength_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpautogeneratedcodelength_Enabled), 5, 0), true);
            chkavOtpgeneratecodeonlynumbers.Enabled = 0;
            AssignProp(sPrefix, false, chkavOtpgeneratecodeonlynumbers_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavOtpgeneratecodeonlynumbers.Enabled), 5, 0), true);
            edtavOtpnumberunsuccessfulretriestolockotp_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpnumberunsuccessfulretriestolockotp_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpnumberunsuccessfulretriestolockotp_Enabled), 5, 0), true);
            edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Enabled), 5, 0), true);
            edtavOtpautounlocktime_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpautounlocktime_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpautounlocktime_Enabled), 5, 0), true);
            cmbavOtpeventsendcode.Enabled = 0;
            AssignProp(sPrefix, false, cmbavOtpeventsendcode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavOtpeventsendcode.Enabled), 5, 0), true);
            edtavOtpmailmessagesubject_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpmailmessagesubject_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpmailmessagesubject_Enabled), 5, 0), true);
            edtavOtpmailmessagebodyhtml_Enabled = 0;
            AssignProp(sPrefix, false, edtavOtpmailmessagebodyhtml_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavOtpmailmessagebodyhtml_Enabled), 5, 0), true);
            cmbavOtpeventvalidatecode.Enabled = 0;
            AssignProp(sPrefix, false, cmbavOtpeventvalidatecode_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavOtpeventvalidatecode.Enabled), 5, 0), true);
            bttBtnenter_Caption = context.GetMessage( "WWP_GAM_Delete", "");
            AssignProp(sPrefix, false, bttBtnenter_Internalname, "Caption", bttBtnenter_Caption, true);
         }
         /* Execute user subroutine: 'REFRESHAUTHENTICATIONTYPE' */
         S232 ();
         if (returnInSub) return;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp(sPrefix, false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         chkavCuautocompletevirtualdirectory.TooltipText = context.GetMessage( "WWP_GAM_AutocompletecallbackURLwithvirtualdirectorywhereapplicationservicesarerunning", "");
         AssignProp(sPrefix, false, chkavCuautocompletevirtualdirectory_Internalname, "Tooltiptext", chkavCuautocompletevirtualdirectory.TooltipText, true);
         chkavAutocompletevirtualdirectory.TooltipText = context.GetMessage( "WWP_GAM_AutocompletesiteURLwithvirtualdirectorywhereapplicationservicesarerunning", "");
         AssignProp(sPrefix, false, chkavAutocompletevirtualdirectory_Internalname, "Tooltiptext", chkavAutocompletevirtualdirectory.TooltipText, true);
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S242 ();
         if (returnInSub) return;
      }

      protected void E121I2( )
      {
         /* 'DoGenKeyCustom' Routine */
         returnInSub = false;
         AV31CusPrivateEncryptKey = Crypto.GetEncryptionKey( );
         AssignAttri(sPrefix, false, "AV31CusPrivateEncryptKey", AV31CusPrivateEncryptKey);
         /*  Sending Event outputs  */
      }

      protected void E131I2( )
      {
         /* 'DoGenKey' Routine */
         returnInSub = false;
         AV74WSPrivateEncryptKey = Crypto.GetEncryptionKey( );
         AssignAttri(sPrefix, false, "AV74WSPrivateEncryptKey", AV74WSPrivateEncryptKey);
         /*  Sending Event outputs  */
      }

      protected void S252( )
      {
         /* 'CHECKREQUIREDFIELDS' Routine */
         returnInSub = false;
         AV83CheckRequiredFieldsResult = true;
         AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV46Name)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_Name", ""), "", "", "", "", "", "", "", ""),  "error",  edtavName_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV24ClientId)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_ClientId", ""), "", "", "", "", "", "", "", ""),  "error",  edtavClientid_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV25ClientSecret)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_ClientSecret", ""), "", "", "", "", "", "", "", ""),  "error",  edtavClientsecret_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV62SiteURL)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_LocalSiteURL", ""), "", "", "", "", "", "", "", ""),  "error",  edtavSiteurl_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) ) && (false==AV85AutocompleteVirtualDirectory) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_AutocompleteVirtualDirectory", ""), "", "", "", "", "", "", "", ""),  "error",  chkavAutocompletevirtualdirectory_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV43GAMRServerURL)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_RemoteServerURL", ""), "", "", "", "", "", "", "", ""),  "error",  edtavGamrserverurl_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV76WSServerName)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_Servername", ""), "", "", "", "", "", "", "", ""),  "error",  edtavWsservername_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) ) && (0==AV77WSServerPort) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_Serverport", ""), "", "", "", "", "", "", "", ""),  "error",  edtavWsserverport_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV72WSName)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_WebServiceName", ""), "", "", "", "", "", "", "", ""),  "error",  edtavWsname_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV29CusFileName)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_FileName", ""), "", "", "", "", "", "", "", ""),  "error",  edtavCusfilename_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
         if ( ( ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 ) ) && String.IsNullOrEmpty(StringUtil.RTrim( AV28CusClassName)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_ClassName", ""), "", "", "", "", "", "", "", ""),  "error",  edtavCusclassname_Internalname,  "true",  ""));
            AV83CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV83CheckRequiredFieldsResult", AV83CheckRequiredFieldsResult);
         }
      }

      protected void S242( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
         if ( ! ( ! ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 ) ) )
         {
            cmbavImpersonate.Visible = 0;
            AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavImpersonate.Visible), 5, 0), true);
            divImpersonate_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divImpersonate_cell_Internalname, "Class", divImpersonate_cell_Class, true);
         }
         else
         {
            cmbavImpersonate.Visible = 1;
            AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavImpersonate.Visible), 5, 0), true);
            divImpersonate_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divImpersonate_cell_Internalname, "Class", divImpersonate_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 ) ) )
         {
            chkavTfaenable.Visible = 0;
            AssignProp(sPrefix, false, chkavTfaenable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavTfaenable.Visible), 5, 0), true);
            divTfaenable_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divTfaenable_cell_Internalname, "Class", divTfaenable_cell_Class, true);
         }
         else
         {
            chkavTfaenable.Visible = 1;
            AssignProp(sPrefix, false, chkavTfaenable_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavTfaenable.Visible), 5, 0), true);
            divTfaenable_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divTfaenable_cell_Internalname, "Class", divTfaenable_cell_Class, true);
         }
         if ( ! ( ( ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 ) ) && AV66TFAEnable ) )
         {
            cmbavTfaauthenticationtypename.Visible = 0;
            AssignProp(sPrefix, false, cmbavTfaauthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTfaauthenticationtypename.Visible), 5, 0), true);
            divTfaauthenticationtypename_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divTfaauthenticationtypename_cell_Internalname, "Class", divTfaauthenticationtypename_cell_Class, true);
         }
         else
         {
            cmbavTfaauthenticationtypename.Visible = 1;
            AssignProp(sPrefix, false, cmbavTfaauthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTfaauthenticationtypename.Visible), 5, 0), true);
            divTfaauthenticationtypename_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divTfaauthenticationtypename_cell_Internalname, "Class", divTfaauthenticationtypename_cell_Class, true);
         }
         if ( ! ( ( ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 ) ) && AV66TFAEnable ) )
         {
            edtavTfafirstfactorauthenticationexpiration_Visible = 0;
            AssignProp(sPrefix, false, edtavTfafirstfactorauthenticationexpiration_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavTfafirstfactorauthenticationexpiration_Visible), 5, 0), true);
            divTfafirstfactorauthenticationexpiration_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divTfafirstfactorauthenticationexpiration_cell_Internalname, "Class", divTfafirstfactorauthenticationexpiration_cell_Class, true);
         }
         else
         {
            edtavTfafirstfactorauthenticationexpiration_Visible = 1;
            AssignProp(sPrefix, false, edtavTfafirstfactorauthenticationexpiration_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavTfafirstfactorauthenticationexpiration_Visible), 5, 0), true);
            divTfafirstfactorauthenticationexpiration_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divTfafirstfactorauthenticationexpiration_cell_Internalname, "Class", divTfafirstfactorauthenticationexpiration_cell_Class, true);
         }
         if ( ! ( ( ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 ) ) && AV66TFAEnable ) )
         {
            chkavTfaforceforallusers.Visible = 0;
            AssignProp(sPrefix, false, chkavTfaforceforallusers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavTfaforceforallusers.Visible), 5, 0), true);
            divTfaforceforallusers_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divTfaforceforallusers_cell_Internalname, "Class", divTfaforceforallusers_cell_Class, true);
         }
         else
         {
            chkavTfaforceforallusers.Visible = 1;
            AssignProp(sPrefix, false, chkavTfaforceforallusers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavTfaforceforallusers.Visible), 5, 0), true);
            divTfaforceforallusers_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divTfaforceforallusers_cell_Internalname, "Class", divTfaforceforallusers_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) )
         {
            edtavClientid_Visible = 0;
            AssignProp(sPrefix, false, edtavClientid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavClientid_Visible), 5, 0), true);
            divClientid_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divClientid_cell_Internalname, "Class", divClientid_cell_Class, true);
         }
         else
         {
            edtavClientid_Visible = 1;
            AssignProp(sPrefix, false, edtavClientid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavClientid_Visible), 5, 0), true);
            divClientid_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divClientid_cell_Internalname, "Class", divClientid_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) )
         {
            edtavClientsecret_Visible = 0;
            AssignProp(sPrefix, false, edtavClientsecret_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavClientsecret_Visible), 5, 0), true);
            divClientsecret_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divClientsecret_cell_Internalname, "Class", divClientsecret_cell_Class, true);
         }
         else
         {
            edtavClientsecret_Visible = 1;
            AssignProp(sPrefix, false, edtavClientsecret_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavClientsecret_Visible), 5, 0), true);
            divClientsecret_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divClientsecret_cell_Internalname, "Class", divClientsecret_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) )
         {
            edtavVersionpath_Visible = 0;
            AssignProp(sPrefix, false, edtavVersionpath_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavVersionpath_Visible), 5, 0), true);
            divVersionpath_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divVersionpath_cell_Internalname, "Class", divVersionpath_cell_Class, true);
         }
         else
         {
            edtavVersionpath_Visible = 1;
            AssignProp(sPrefix, false, edtavVersionpath_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavVersionpath_Visible), 5, 0), true);
            divVersionpath_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divVersionpath_cell_Internalname, "Class", divVersionpath_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) ) )
         {
            edtavSiteurl_Visible = 0;
            AssignProp(sPrefix, false, edtavSiteurl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavSiteurl_Visible), 5, 0), true);
            divSiteurl_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divSiteurl_cell_Internalname, "Class", divSiteurl_cell_Class, true);
         }
         else
         {
            edtavSiteurl_Visible = 1;
            AssignProp(sPrefix, false, edtavSiteurl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavSiteurl_Visible), 5, 0), true);
            divSiteurl_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divSiteurl_cell_Internalname, "Class", divSiteurl_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) ) )
         {
            chkavAutocompletevirtualdirectory.Visible = 0;
            AssignProp(sPrefix, false, chkavAutocompletevirtualdirectory_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavAutocompletevirtualdirectory.Visible), 5, 0), true);
            divAutocompletevirtualdirectory_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divAutocompletevirtualdirectory_cell_Internalname, "Class", divAutocompletevirtualdirectory_cell_Class, true);
         }
         else
         {
            chkavAutocompletevirtualdirectory.Visible = 1;
            AssignProp(sPrefix, false, chkavAutocompletevirtualdirectory_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavAutocompletevirtualdirectory.Visible), 5, 0), true);
            divAutocompletevirtualdirectory_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divAutocompletevirtualdirectory_cell_Internalname, "Class", divAutocompletevirtualdirectory_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) ) )
         {
            chkavSiteurlcallbackiscustom.Visible = 0;
            AssignProp(sPrefix, false, chkavSiteurlcallbackiscustom_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavSiteurlcallbackiscustom.Visible), 5, 0), true);
            divSiteurlcallbackiscustom_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divSiteurlcallbackiscustom_cell_Internalname, "Class", divSiteurlcallbackiscustom_cell_Class, true);
         }
         else
         {
            chkavSiteurlcallbackiscustom.Visible = 1;
            AssignProp(sPrefix, false, chkavSiteurlcallbackiscustom_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavSiteurlcallbackiscustom.Visible), 5, 0), true);
            divSiteurlcallbackiscustom_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divSiteurlcallbackiscustom_cell_Internalname, "Class", divSiteurlcallbackiscustom_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 ) ) )
         {
            edtavConsumerkey_Visible = 0;
            AssignProp(sPrefix, false, edtavConsumerkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavConsumerkey_Visible), 5, 0), true);
            divConsumerkey_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divConsumerkey_cell_Internalname, "Class", divConsumerkey_cell_Class, true);
         }
         else
         {
            edtavConsumerkey_Visible = 1;
            AssignProp(sPrefix, false, edtavConsumerkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavConsumerkey_Visible), 5, 0), true);
            divConsumerkey_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divConsumerkey_cell_Internalname, "Class", divConsumerkey_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 ) ) )
         {
            edtavConsumersecret_Visible = 0;
            AssignProp(sPrefix, false, edtavConsumersecret_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavConsumersecret_Visible), 5, 0), true);
            divConsumersecret_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divConsumersecret_cell_Internalname, "Class", divConsumersecret_cell_Class, true);
         }
         else
         {
            edtavConsumersecret_Visible = 1;
            AssignProp(sPrefix, false, edtavConsumersecret_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavConsumersecret_Visible), 5, 0), true);
            divConsumersecret_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divConsumersecret_cell_Internalname, "Class", divConsumersecret_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 ) ) )
         {
            edtavCallbackurl_Visible = 0;
            AssignProp(sPrefix, false, edtavCallbackurl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCallbackurl_Visible), 5, 0), true);
            divCallbackurl_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divCallbackurl_cell_Internalname, "Class", divCallbackurl_cell_Class, true);
         }
         else
         {
            edtavCallbackurl_Visible = 1;
            AssignProp(sPrefix, false, edtavCallbackurl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCallbackurl_Visible), 5, 0), true);
            divCallbackurl_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divCallbackurl_cell_Internalname, "Class", divCallbackurl_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 ) ) )
         {
            chkavCuautocompletevirtualdirectory.Visible = 0;
            AssignProp(sPrefix, false, chkavCuautocompletevirtualdirectory_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavCuautocompletevirtualdirectory.Visible), 5, 0), true);
            divCuautocompletevirtualdirectory_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divCuautocompletevirtualdirectory_cell_Internalname, "Class", divCuautocompletevirtualdirectory_cell_Class, true);
         }
         else
         {
            chkavCuautocompletevirtualdirectory.Visible = 1;
            AssignProp(sPrefix, false, chkavCuautocompletevirtualdirectory_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavCuautocompletevirtualdirectory.Visible), 5, 0), true);
            divCuautocompletevirtualdirectory_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divCuautocompletevirtualdirectory_cell_Internalname, "Class", divCuautocompletevirtualdirectory_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) )
         {
            edtavAdditionalscope_Visible = 0;
            AssignProp(sPrefix, false, edtavAdditionalscope_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavAdditionalscope_Visible), 5, 0), true);
            divAdditionalscope_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divAdditionalscope_cell_Internalname, "Class", divAdditionalscope_cell_Class, true);
         }
         else
         {
            edtavAdditionalscope_Visible = 1;
            AssignProp(sPrefix, false, edtavAdditionalscope_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavAdditionalscope_Visible), 5, 0), true);
            divAdditionalscope_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divAdditionalscope_cell_Internalname, "Class", divAdditionalscope_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) ) )
         {
            edtavGamrauthenticationtypename_Visible = 0;
            AssignProp(sPrefix, false, edtavGamrauthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGamrauthenticationtypename_Visible), 5, 0), true);
            divGamrauthenticationtypename_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divGamrauthenticationtypename_cell_Internalname, "Class", divGamrauthenticationtypename_cell_Class, true);
         }
         else
         {
            edtavGamrauthenticationtypename_Visible = 1;
            AssignProp(sPrefix, false, edtavGamrauthenticationtypename_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGamrauthenticationtypename_Visible), 5, 0), true);
            divGamrauthenticationtypename_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divGamrauthenticationtypename_cell_Internalname, "Class", divGamrauthenticationtypename_cell_Class, true);
         }
         if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
         {
            divCusfilename_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divCusfilename_cell_Internalname, "Class", divCusfilename_cell_Class, true);
         }
         else
         {
            divCusfilename_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divCusfilename_cell_Internalname, "Class", divCusfilename_cell_Class, true);
         }
         if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
         {
            divCusclassname_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divCusclassname_cell_Internalname, "Class", divCusclassname_cell_Class, true);
         }
         else
         {
            divCusclassname_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divCusclassname_cell_Internalname, "Class", divCusclassname_cell_Class, true);
         }
         if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
         {
            divWsservername_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divWsservername_cell_Internalname, "Class", divWsservername_cell_Class, true);
         }
         else
         {
            divWsservername_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divWsservername_cell_Internalname, "Class", divWsservername_cell_Class, true);
         }
         if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
         {
            divWsserverport_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divWsserverport_cell_Internalname, "Class", divWsserverport_cell_Class, true);
         }
         else
         {
            divWsserverport_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divWsserverport_cell_Internalname, "Class", divWsserverport_cell_Class, true);
         }
         if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
         {
            divWsname_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divWsname_cell_Internalname, "Class", divWsname_cell_Class, true);
         }
         else
         {
            divWsname_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divWsname_cell_Internalname, "Class", divWsname_cell_Class, true);
         }
         if ( ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) )
         {
            divGamrserverurl_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divGamrserverurl_cell_Internalname, "Class", divGamrserverurl_cell_Class, true);
         }
         else
         {
            divGamrserverurl_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divGamrserverurl_cell_Internalname, "Class", divGamrserverurl_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) ) )
         {
            edtavGamrprivateencryptkey_Visible = 0;
            AssignProp(sPrefix, false, edtavGamrprivateencryptkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGamrprivateencryptkey_Visible), 5, 0), true);
            divGamrprivateencryptkey_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divGamrprivateencryptkey_cell_Internalname, "Class", divGamrprivateencryptkey_cell_Class, true);
         }
         else
         {
            edtavGamrprivateencryptkey_Visible = 1;
            AssignProp(sPrefix, false, edtavGamrprivateencryptkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGamrprivateencryptkey_Visible), 5, 0), true);
            divGamrprivateencryptkey_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divGamrprivateencryptkey_cell_Internalname, "Class", divGamrprivateencryptkey_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) ) )
         {
            edtavGamrrepositoryguid_Visible = 0;
            AssignProp(sPrefix, false, edtavGamrrepositoryguid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGamrrepositoryguid_Visible), 5, 0), true);
            divGamrrepositoryguid_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divGamrrepositoryguid_cell_Internalname, "Class", divGamrrepositoryguid_cell_Class, true);
         }
         else
         {
            edtavGamrrepositoryguid_Visible = 1;
            AssignProp(sPrefix, false, edtavGamrrepositoryguid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGamrrepositoryguid_Visible), 5, 0), true);
            divGamrrepositoryguid_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divGamrrepositoryguid_cell_Internalname, "Class", divGamrrepositoryguid_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 ) && ( StringUtil.StrCmp(AV54OTPGenerationType, "custom") == 0 ) ) )
         {
            cmbavOtpgenerationtype_customeventgeneratecode.Visible = 0;
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavOtpgenerationtype_customeventgeneratecode.Visible), 5, 0), true);
            divOtpgenerationtype_customeventgeneratecode_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divOtpgenerationtype_customeventgeneratecode_cell_Internalname, "Class", divOtpgenerationtype_customeventgeneratecode_cell_Class, true);
         }
         else
         {
            cmbavOtpgenerationtype_customeventgeneratecode.Visible = 1;
            AssignProp(sPrefix, false, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavOtpgenerationtype_customeventgeneratecode.Visible), 5, 0), true);
            divOtpgenerationtype_customeventgeneratecode_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divOtpgenerationtype_customeventgeneratecode_cell_Internalname, "Class", divOtpgenerationtype_customeventgeneratecode_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 ) && ( StringUtil.StrCmp(AV54OTPGenerationType, "gam") == 0 ) ) )
         {
            edtavOtpautogeneratedcodelength_Visible = 0;
            AssignProp(sPrefix, false, edtavOtpautogeneratedcodelength_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavOtpautogeneratedcodelength_Visible), 5, 0), true);
            divOtpautogeneratedcodelength_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divOtpautogeneratedcodelength_cell_Internalname, "Class", divOtpautogeneratedcodelength_cell_Class, true);
         }
         else
         {
            edtavOtpautogeneratedcodelength_Visible = 1;
            AssignProp(sPrefix, false, edtavOtpautogeneratedcodelength_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavOtpautogeneratedcodelength_Visible), 5, 0), true);
            divOtpautogeneratedcodelength_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divOtpautogeneratedcodelength_cell_Internalname, "Class", divOtpautogeneratedcodelength_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 ) && ( StringUtil.StrCmp(AV54OTPGenerationType, "gam") == 0 ) ) )
         {
            chkavOtpgeneratecodeonlynumbers.Visible = 0;
            AssignProp(sPrefix, false, chkavOtpgeneratecodeonlynumbers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavOtpgeneratecodeonlynumbers.Visible), 5, 0), true);
            divOtpgeneratecodeonlynumbers_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divOtpgeneratecodeonlynumbers_cell_Internalname, "Class", divOtpgeneratecodeonlynumbers_cell_Class, true);
         }
         else
         {
            chkavOtpgeneratecodeonlynumbers.Visible = 1;
            AssignProp(sPrefix, false, chkavOtpgeneratecodeonlynumbers_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavOtpgeneratecodeonlynumbers.Visible), 5, 0), true);
            divOtpgeneratecodeonlynumbers_cell_Class = "col-xs-12 col-sm-6 DataContentCell DscTop";
            AssignProp(sPrefix, false, divOtpgeneratecodeonlynumbers_cell_Internalname, "Class", divOtpgeneratecodeonlynumbers_cell_Class, true);
         }
         divTblsendandvalidateotpcode_Visible = (((StringUtil.StrCmp(AV69TypeId, "OTP")==0)&&((StringUtil.StrCmp(AV54OTPGenerationType, "custom")==0)||(StringUtil.StrCmp(AV54OTPGenerationType, "gam")==0))) ? 1 : 0);
         AssignProp(sPrefix, false, divTblsendandvalidateotpcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblsendandvalidateotpcode_Visible), 5, 0), true);
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E141I2 ();
         if (returnInSub) return;
      }

      protected void E141I2( )
      {
         /* Enter Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CHECKREQUIREDFIELDS' */
         S252 ();
         if (returnInSub) return;
         if ( AV83CheckRequiredFieldsResult )
         {
            if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) )
            {
               if ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV15AuthenticationTypeLocal.load( AV46Name);
                  }
                  AV15AuthenticationTypeLocal.gxTpr_Name = AV46Name;
                  AV15AuthenticationTypeLocal.gxTpr_Functionid = AV37FunctionId;
                  AV15AuthenticationTypeLocal.gxTpr_Isenable = AV45IsEnable;
                  AV15AuthenticationTypeLocal.gxTpr_Description = AV33Dsc;
                  AV15AuthenticationTypeLocal.gxTpr_Smallimagename = AV64SmallImageName;
                  AV15AuthenticationTypeLocal.gxTpr_Bigimagename = AV22BigImageName;
                  AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Enable = AV66TFAEnable;
                  if ( AV66TFAEnable )
                  {
                     AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename = StringUtil.Trim( AV65TFAAuthenticationTypeName);
                     AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration = AV67TFAFirstFactorAuthenticationExpiration;
                     AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers = AV68TFAForceForAllUsers;
                  }
                  AV15AuthenticationTypeLocal.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "APIkey") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV84GAMAuthenticationTypeAPIkey.load( AV46Name);
                  }
                  AV84GAMAuthenticationTypeAPIkey.gxTpr_Name = AV46Name;
                  AV84GAMAuthenticationTypeAPIkey.gxTpr_Functionid = AV37FunctionId;
                  AV84GAMAuthenticationTypeAPIkey.gxTpr_Isenable = AV45IsEnable;
                  AV84GAMAuthenticationTypeAPIkey.gxTpr_Description = AV33Dsc;
                  AV84GAMAuthenticationTypeAPIkey.gxTpr_Smallimagename = AV64SmallImageName;
                  AV84GAMAuthenticationTypeAPIkey.gxTpr_Bigimagename = AV22BigImageName;
                  AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Enable = AV66TFAEnable;
                  if ( AV66TFAEnable )
                  {
                     AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename = StringUtil.Trim( AV65TFAAuthenticationTypeName);
                     AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration = AV67TFAFirstFactorAuthenticationExpiration;
                     AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers = AV68TFAForceForAllUsers;
                  }
                  AV84GAMAuthenticationTypeAPIkey.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV9AuthenticationTypeApple.load( AV46Name);
                  }
                  AV9AuthenticationTypeApple.gxTpr_Name = AV46Name;
                  AV9AuthenticationTypeApple.gxTpr_Isenable = AV45IsEnable;
                  AV9AuthenticationTypeApple.gxTpr_Description = AV33Dsc;
                  AV9AuthenticationTypeApple.gxTpr_Smallimagename = AV64SmallImageName;
                  AV9AuthenticationTypeApple.gxTpr_Bigimagename = AV22BigImageName;
                  AV9AuthenticationTypeApple.gxTpr_Impersonate = AV44Impersonate;
                  AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Clientid = AV24ClientId;
                  AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Clientsecret = AV25ClientSecret;
                  AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Siteurl = AV62SiteURL;
                  AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Siteurl_autocompletevirtualdirectory = AV85AutocompleteVirtualDirectory;
                  AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Additionalscope = AV6AdditionalScope;
                  AV9AuthenticationTypeApple.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV10AuthenticationTypeCustom.load( AV46Name);
                  }
                  AV10AuthenticationTypeCustom.gxTpr_Name = AV46Name;
                  AV10AuthenticationTypeCustom.gxTpr_Functionid = AV37FunctionId;
                  AV10AuthenticationTypeCustom.gxTpr_Isenable = AV45IsEnable;
                  AV10AuthenticationTypeCustom.gxTpr_Description = AV33Dsc;
                  AV10AuthenticationTypeCustom.gxTpr_Smallimagename = AV64SmallImageName;
                  AV10AuthenticationTypeCustom.gxTpr_Bigimagename = AV22BigImageName;
                  AV10AuthenticationTypeCustom.gxTpr_Impersonate = AV44Impersonate;
                  AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Enable = AV66TFAEnable;
                  if ( AV66TFAEnable )
                  {
                     AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename = AV65TFAAuthenticationTypeName;
                     AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration = AV67TFAFirstFactorAuthenticationExpiration;
                     AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers = AV68TFAForceForAllUsers;
                  }
                  AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Version = AV32CusVersion;
                  AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Privateencryptkey = AV31CusPrivateEncryptKey;
                  AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Filename = AV29CusFileName;
                  AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Package = AV30CusPackage;
                  AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Classname = AV28CusClassName;
                  AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Method = AV87CusMethod;
                  AV10AuthenticationTypeCustom.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV19AuthenticationTypeWebService.load( AV46Name);
                  }
                  AV19AuthenticationTypeWebService.gxTpr_Name = AV46Name;
                  AV19AuthenticationTypeWebService.gxTpr_Functionid = AV37FunctionId;
                  AV19AuthenticationTypeWebService.gxTpr_Isenable = AV45IsEnable;
                  AV19AuthenticationTypeWebService.gxTpr_Description = AV33Dsc;
                  AV19AuthenticationTypeWebService.gxTpr_Smallimagename = AV64SmallImageName;
                  AV19AuthenticationTypeWebService.gxTpr_Bigimagename = AV22BigImageName;
                  AV19AuthenticationTypeWebService.gxTpr_Impersonate = AV44Impersonate;
                  AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Enable = AV66TFAEnable;
                  if ( AV66TFAEnable )
                  {
                     AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename = AV65TFAAuthenticationTypeName;
                     AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration = AV67TFAFirstFactorAuthenticationExpiration;
                     AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers = AV68TFAForceForAllUsers;
                  }
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Version = AV80WSVersion;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Privateencryptkey = AV74WSPrivateEncryptKey;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Timeout = AV79WSTimeout;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Package = AV73WSPackage;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Name = AV72WSName;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Extension = AV71WSExtension;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Name = AV76WSServerName;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Port = AV77WSServerPort;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Baseurl = AV75WSServerBaseURL;
                  AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Secureprotocol = AV78WSServerSecureProtocol;
                  AV19AuthenticationTypeWebService.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV11AuthenticationTypeFacebook.load( AV46Name);
                  }
                  AV11AuthenticationTypeFacebook.gxTpr_Name = AV46Name;
                  AV11AuthenticationTypeFacebook.gxTpr_Isenable = AV45IsEnable;
                  AV11AuthenticationTypeFacebook.gxTpr_Description = AV33Dsc;
                  AV11AuthenticationTypeFacebook.gxTpr_Smallimagename = AV64SmallImageName;
                  AV11AuthenticationTypeFacebook.gxTpr_Bigimagename = AV22BigImageName;
                  AV11AuthenticationTypeFacebook.gxTpr_Impersonate = AV44Impersonate;
                  AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Clientid = AV24ClientId;
                  AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Clientsecret = AV25ClientSecret;
                  AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Versionpath = AV70VersionPath;
                  AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Siteurl = AV62SiteURL;
                  AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Siteurl_autocompletevirtualdirectory = AV85AutocompleteVirtualDirectory;
                  AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Additionalscope = AV6AdditionalScope;
                  AV11AuthenticationTypeFacebook.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV12AuthenticationTypeGAMRemote.load( AV46Name);
                  }
                  AV12AuthenticationTypeGAMRemote.gxTpr_Name = AV46Name;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Functionid = AV37FunctionId;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Isenable = AV45IsEnable;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Description = AV33Dsc;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Smallimagename = AV64SmallImageName;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Bigimagename = AV22BigImageName;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Impersonate = AV44Impersonate;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Clientid = AV24ClientId;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Clientsecret = AV25ClientSecret;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Siteurl = AV62SiteURL;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Siteurlcallbackiscustom = AV63SiteURLCallbackIsCustom;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Adduserdatascope = AV88AddUserDataScope;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Adduseradditionaldatascope = AV7AddUserAdditionalDataScope;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Addsessioninitialpropertiesscope = AV5AddInitialPropertiesScope;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Addsessionapplicationdatascope = AV89AddApplicationDataScope;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Additionalscope = AV6AdditionalScope;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Remoteserverurl = AV43GAMRServerURL;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Remoteserverkey = AV41GAMRPrivateEncryptKey;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Remoterepositoryguid = AV42GAMRRepositoryGUID;
                  AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Autovalidateexternaltokenandrefresh = AV21AutovalidateExternalTokenAndRefresh;
                  AV12AuthenticationTypeGAMRemote.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV13AuthenticationTypeGAMRemoteRest.load( AV46Name);
                  }
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Name = AV46Name;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Functionid = AV37FunctionId;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Isenable = AV45IsEnable;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Description = AV33Dsc;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Smallimagename = AV64SmallImageName;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Bigimagename = AV22BigImageName;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Impersonate = AV44Impersonate;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Enable = AV66TFAEnable;
                  if ( AV66TFAEnable )
                  {
                     AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename = AV65TFAAuthenticationTypeName;
                     AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration = AV67TFAFirstFactorAuthenticationExpiration;
                     AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers = AV68TFAForceForAllUsers;
                  }
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Clientid = AV24ClientId;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Clientsecret = AV25ClientSecret;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Versionpath = AV70VersionPath;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Adduserdatascope = AV88AddUserDataScope;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Adduseradditionaldatascope = AV7AddUserAdditionalDataScope;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Addsessioninitialpropertiesscope = AV89AddApplicationDataScope;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Addsessionapplicationdatascope = AV5AddInitialPropertiesScope;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Additionalscope = AV6AdditionalScope;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoteauthenticationtypename = AV40GAMRAuthenticationTypeName;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoteserverurl = AV43GAMRServerURL;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoteserverkey = AV41GAMRPrivateEncryptKey;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoterepositoryguid = AV42GAMRRepositoryGUID;
                  AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Autovalidateexternaltokenandrefresh = AV21AutovalidateExternalTokenAndRefresh;
                  AV13AuthenticationTypeGAMRemoteRest.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV14AuthenticationTypeGoogle.load( AV46Name);
                  }
                  AV14AuthenticationTypeGoogle.gxTpr_Name = AV46Name;
                  AV14AuthenticationTypeGoogle.gxTpr_Isenable = AV45IsEnable;
                  AV14AuthenticationTypeGoogle.gxTpr_Description = AV33Dsc;
                  AV14AuthenticationTypeGoogle.gxTpr_Smallimagename = AV64SmallImageName;
                  AV14AuthenticationTypeGoogle.gxTpr_Bigimagename = AV22BigImageName;
                  AV14AuthenticationTypeGoogle.gxTpr_Impersonate = AV44Impersonate;
                  AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Clientid = AV24ClientId;
                  AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Clientsecret = AV25ClientSecret;
                  AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Versionpath = AV70VersionPath;
                  AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Siteurl = AV62SiteURL;
                  AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Siteurl_autocompletevirtualdirectory = AV85AutocompleteVirtualDirectory;
                  AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Additionalscope = AV6AdditionalScope;
                  AV14AuthenticationTypeGoogle.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV17AuthenticationTypeOTP.load( AV46Name);
                  }
                  AV17AuthenticationTypeOTP.gxTpr_Name = AV46Name;
                  AV17AuthenticationTypeOTP.gxTpr_Isenable = AV45IsEnable;
                  AV17AuthenticationTypeOTP.gxTpr_Description = AV33Dsc;
                  AV17AuthenticationTypeOTP.gxTpr_Smallimagename = AV64SmallImageName;
                  AV17AuthenticationTypeOTP.gxTpr_Bigimagename = AV22BigImageName;
                  AV17AuthenticationTypeOTP.gxTpr_Impersonate = AV44Impersonate;
                  AV17AuthenticationTypeOTP.gxTpr_Useforfirstfactorauthentication = AV61OTPUseForFirstFactorAuthentication;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Eventvaliduser = AV52OTPEventValidateUser;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Generationtype = AV54OTPGenerationType;
                  if ( StringUtil.StrCmp(AV54OTPGenerationType, "custom") == 0 )
                  {
                     AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Generationtype_customeventgeneratecode = AV55OTPGenerationType_CustomEventGenerateCode;
                  }
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Codeexpirationtimeout = AV49OTPCodeExpirationTimeout;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Maximumdailynumbercodes = AV58OTPMaximumDailyNumberCodes;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Autogeneratedcodelength = AV47OTPAutogeneratedCodeLength;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Generatecodeonlynumbers = AV53OTPGenerateCodeOnlyNumbers;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Numberunsuccessfulretriestolockotp = AV60OTPNumberUnsuccessfulRetriesToLockOTP;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Numberunsuccessfulretriestoblockuserbasedofotplocks = AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Automaticotpunlocktime = AV48OTPAutoUnlockTime;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Eventsendcode = AV50OTPEventSendCode;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Mailmessagesubject = AV57OTPMailMessageSubject;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Mailmessagebodyhtml = AV56OTPMailMessageBodyHTML;
                  AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Eventvalidatecode = AV51OTPEventValidateCode;
                  AV17AuthenticationTypeOTP.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV18AuthenticationTypeTwitter.load( AV46Name);
                  }
                  AV18AuthenticationTypeTwitter.gxTpr_Name = AV46Name;
                  AV18AuthenticationTypeTwitter.gxTpr_Isenable = AV45IsEnable;
                  AV18AuthenticationTypeTwitter.gxTpr_Description = AV33Dsc;
                  AV18AuthenticationTypeTwitter.gxTpr_Smallimagename = AV64SmallImageName;
                  AV18AuthenticationTypeTwitter.gxTpr_Bigimagename = AV22BigImageName;
                  AV18AuthenticationTypeTwitter.gxTpr_Impersonate = AV44Impersonate;
                  AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Consumerkey = AV26ConsumerKey;
                  AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Consumersecret = AV27ConsumerSecret;
                  AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Callbackurl = AV23CallbackURL;
                  AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Callbackurl_autocompletevirtualdirectory = AV86CUAutocompleteVirtualDirectory;
                  AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Additionalscope = AV6AdditionalScope;
                  AV18AuthenticationTypeTwitter.save();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 )
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     AV20AuthenticationTypeWeChat.load( AV46Name);
                  }
                  AV20AuthenticationTypeWeChat.gxTpr_Name = AV46Name;
                  AV20AuthenticationTypeWeChat.gxTpr_Isenable = AV45IsEnable;
                  AV20AuthenticationTypeWeChat.gxTpr_Description = AV33Dsc;
                  AV20AuthenticationTypeWeChat.gxTpr_Smallimagename = AV64SmallImageName;
                  AV20AuthenticationTypeWeChat.gxTpr_Bigimagename = AV22BigImageName;
                  AV20AuthenticationTypeWeChat.gxTpr_Impersonate = AV44Impersonate;
                  AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Clientid = AV24ClientId;
                  AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Clientsecret = AV25ClientSecret;
                  AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Versionpath = AV70VersionPath;
                  AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Siteurl = AV62SiteURL;
                  AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Siteurl_autocompletevirtualdirectory = AV85AutocompleteVirtualDirectory;
                  AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Additionalscope = AV6AdditionalScope;
                  AV20AuthenticationTypeWeChat.save();
               }
            }
            else if ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 )
            {
               if ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 )
               {
                  AV15AuthenticationTypeLocal.load( AV46Name);
                  AV15AuthenticationTypeLocal.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "APIkey") == 0 )
               {
                  AV84GAMAuthenticationTypeAPIkey.load( AV46Name);
                  AV84GAMAuthenticationTypeAPIkey.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 )
               {
                  AV9AuthenticationTypeApple.load( AV46Name);
                  AV9AuthenticationTypeApple.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
               {
                  AV10AuthenticationTypeCustom.load( AV46Name);
                  AV10AuthenticationTypeCustom.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
               {
                  AV19AuthenticationTypeWebService.load( AV46Name);
                  AV19AuthenticationTypeWebService.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 )
               {
                  AV11AuthenticationTypeFacebook.load( AV46Name);
                  AV11AuthenticationTypeFacebook.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 )
               {
                  AV12AuthenticationTypeGAMRemote.load( AV46Name);
                  AV12AuthenticationTypeGAMRemote.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 )
               {
                  AV13AuthenticationTypeGAMRemoteRest.load( AV46Name);
                  AV13AuthenticationTypeGAMRemoteRest.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 )
               {
                  AV14AuthenticationTypeGoogle.load( AV46Name);
                  AV14AuthenticationTypeGoogle.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Oauth20") == 0 )
               {
                  AV16AuthenticationTypeOauth20.load( AV46Name);
                  AV16AuthenticationTypeOauth20.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 )
               {
                  AV17AuthenticationTypeOTP.load( AV46Name);
                  AV17AuthenticationTypeOTP.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 )
               {
                  AV18AuthenticationTypeTwitter.load( AV46Name);
                  AV18AuthenticationTypeTwitter.delete();
               }
               else if ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 )
               {
                  AV20AuthenticationTypeWeChat.load( AV46Name);
                  AV20AuthenticationTypeWeChat.delete();
               }
            }
            if ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 )
            {
               if ( AV15AuthenticationTypeLocal.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "APIkey") == 0 )
            {
               if ( AV84GAMAuthenticationTypeAPIkey.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 )
            {
               if ( AV9AuthenticationTypeApple.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
            {
               if ( AV10AuthenticationTypeCustom.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
            {
               if ( AV19AuthenticationTypeWebService.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 )
            {
               if ( AV11AuthenticationTypeFacebook.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 )
            {
               if ( AV12AuthenticationTypeGAMRemote.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 )
            {
               if ( AV13AuthenticationTypeGAMRemoteRest.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 )
            {
               if ( AV14AuthenticationTypeGoogle.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "Oauth20") == 0 )
            {
               if ( AV16AuthenticationTypeOauth20.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 )
            {
               if ( AV17AuthenticationTypeOTP.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 )
            {
               if ( AV18AuthenticationTypeTwitter.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            else if ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 )
            {
               if ( AV20AuthenticationTypeWeChat.success() )
               {
                  context.CommitDataStores("gamwcauthenticationtypeentrygeneral",pr_default);
               }
            }
            AV35Errors = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrors();
            if ( AV35Errors.Count == 0 )
            {
               CallWebObject(formatLink("gamwwauthtypes.aspx") );
               context.wjLocDisableFrm = 1;
            }
            else
            {
               AV93GXV1 = 1;
               while ( AV93GXV1 <= AV35Errors.Count )
               {
                  AV34Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV35Errors.Item(AV93GXV1));
                  GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV34Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV34Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
                  AV93GXV1 = (int)(AV93GXV1+1);
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV20AuthenticationTypeWeChat", AV20AuthenticationTypeWeChat);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV18AuthenticationTypeTwitter", AV18AuthenticationTypeTwitter);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV17AuthenticationTypeOTP", AV17AuthenticationTypeOTP);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV14AuthenticationTypeGoogle", AV14AuthenticationTypeGoogle);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV13AuthenticationTypeGAMRemoteRest", AV13AuthenticationTypeGAMRemoteRest);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV12AuthenticationTypeGAMRemote", AV12AuthenticationTypeGAMRemote);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV11AuthenticationTypeFacebook", AV11AuthenticationTypeFacebook);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV19AuthenticationTypeWebService", AV19AuthenticationTypeWebService);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV10AuthenticationTypeCustom", AV10AuthenticationTypeCustom);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV9AuthenticationTypeApple", AV9AuthenticationTypeApple);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV84GAMAuthenticationTypeAPIkey", AV84GAMAuthenticationTypeAPIkey);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV15AuthenticationTypeLocal", AV15AuthenticationTypeLocal);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV16AuthenticationTypeOauth20", AV16AuthenticationTypeOauth20);
      }

      protected void E151I2( )
      {
         /* Tfaenable_Click Routine */
         returnInSub = false;
         if ( AV66TFAEnable )
         {
            if ( (0==AV67TFAFirstFactorAuthenticationExpiration) )
            {
               AV67TFAFirstFactorAuthenticationExpiration = 900;
               AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            }
            /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEOTP' */
            S262 ();
            if (returnInSub) return;
         }
         else
         {
            AV65TFAAuthenticationTypeName = "";
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
         }
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S242 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         cmbavTfaauthenticationtypename.CurrentValue = StringUtil.RTrim( AV65TFAAuthenticationTypeName);
         AssignProp(sPrefix, false, cmbavTfaauthenticationtypename_Internalname, "Values", cmbavTfaauthenticationtypename.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV38GAMAuthenticationTypeFilter", AV38GAMAuthenticationTypeFilter);
      }

      protected void E161I2( )
      {
         /* Otpgenerationtype_Click Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV54OTPGenerationType, "custom") == 0 )
         {
            /* Execute user subroutine: 'GETEVENTLISTGENERATECODE' */
            S272 ();
            if (returnInSub) return;
         }
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S242 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV39GAMEventSubscriptionFilter", AV39GAMEventSubscriptionFilter);
         cmbavOtpgenerationtype_customeventgeneratecode.CurrentValue = StringUtil.RTrim( AV55OTPGenerationType_CustomEventGenerateCode);
         AssignProp(sPrefix, false, cmbavOtpgenerationtype_customeventgeneratecode_Internalname, "Values", cmbavOtpgenerationtype_customeventgeneratecode.ToJavascriptSource(), true);
      }

      protected void S112( )
      {
         /* 'INITAUTHENTICATIONTYPEAPIKEY' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEOTP' */
         S262 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV84GAMAuthenticationTypeAPIkey.load( AV46Name);
         AV46Name = AV84GAMAuthenticationTypeAPIkey.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV37FunctionId = AV15AuthenticationTypeLocal.gxTpr_Functionid;
         AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         AV45IsEnable = AV84GAMAuthenticationTypeAPIkey.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV84GAMAuthenticationTypeAPIkey.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV84GAMAuthenticationTypeAPIkey.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV84GAMAuthenticationTypeAPIkey.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV66TFAEnable = AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Enable;
         AssignAttri(sPrefix, false, "AV66TFAEnable", AV66TFAEnable);
         if ( AV66TFAEnable )
         {
            AV65TFAAuthenticationTypeName = AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename;
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
            AV67TFAFirstFactorAuthenticationExpiration = AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration;
            AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            AV68TFAForceForAllUsers = AV84GAMAuthenticationTypeAPIkey.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers;
            AssignAttri(sPrefix, false, "AV68TFAForceForAllUsers", AV68TFAForceForAllUsers);
         }
      }

      protected void S232( )
      {
         /* 'REFRESHAUTHENTICATIONTYPE' Routine */
         returnInSub = false;
         edtavSiteurl_Visible = 1;
         AssignProp(sPrefix, false, edtavSiteurl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavSiteurl_Visible), 5, 0), true);
         divTblserverhost_Visible = 0;
         AssignProp(sPrefix, false, divTblserverhost_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblserverhost_Visible), 5, 0), true);
         divTblwebservice_Visible = 0;
         AssignProp(sPrefix, false, divTblwebservice_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblwebservice_Visible), 5, 0), true);
         divTblexternal_Visible = 0;
         AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
         divTblotpauthentication_Visible = 0;
         AssignProp(sPrefix, false, divTblotpauthentication_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpauthentication_Visible), 5, 0), true);
         divTblotpconfiguration_Visible = 0;
         AssignProp(sPrefix, false, divTblotpconfiguration_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpconfiguration_Visible), 5, 0), true);
         if ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 ) )
         {
            AV37FunctionId = "OnlyAuthentication";
            AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
            cmbavFunctionid.Enabled = 0;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         }
         else
         {
            cmbavFunctionid.Enabled = 1;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         }
         if ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 )
         {
         }
         else if ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) )
         {
            if ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 )
            {
               edtavVersionpath_Visible = 0;
               AssignProp(sPrefix, false, edtavVersionpath_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavVersionpath_Visible), 5, 0), true);
            }
            if ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 )
            {
               edtavVersionpath_Visible = 0;
               AssignProp(sPrefix, false, edtavVersionpath_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavVersionpath_Visible), 5, 0), true);
               divTblserverhost_Visible = 1;
               AssignProp(sPrefix, false, divTblserverhost_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblserverhost_Visible), 5, 0), true);
            }
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 )
         {
            divTblserverhost_Visible = 1;
            AssignProp(sPrefix, false, divTblserverhost_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblserverhost_Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 )
         {
            divTblotpauthentication_Visible = 1;
            AssignProp(sPrefix, false, divTblotpauthentication_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpauthentication_Visible), 5, 0), true);
            divTblotpconfiguration_Visible = 1;
            AssignProp(sPrefix, false, divTblotpconfiguration_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpconfiguration_Visible), 5, 0), true);
            if ( StringUtil.StrCmp(AV54OTPGenerationType, "custom") == 0 )
            {
               /* Execute user subroutine: 'GETEVENTLISTGENERATECODE' */
               S272 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 )
         {
            AV37FunctionId = "OnlyAuthentication";
            AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
            cmbavFunctionid.Enabled = 0;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
         {
            divTblwebservice_Visible = 1;
            AssignProp(sPrefix, false, divTblwebservice_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblwebservice_Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
         {
            divTblexternal_Visible = 1;
            AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
         }
         edtavSiteurl_Visible = 1;
         AssignProp(sPrefix, false, edtavSiteurl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavSiteurl_Visible), 5, 0), true);
         chkavAutocompletevirtualdirectory.Visible = 1;
         AssignProp(sPrefix, false, chkavAutocompletevirtualdirectory_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavAutocompletevirtualdirectory.Visible), 5, 0), true);
         cmbavImpersonate.Visible = 1;
         AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavImpersonate.Visible), 5, 0), true);
         divTblscopes_Visible = 0;
         AssignProp(sPrefix, false, divTblscopes_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblscopes_Visible), 5, 0), true);
         divTblserverhost_Visible = 0;
         AssignProp(sPrefix, false, divTblserverhost_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblserverhost_Visible), 5, 0), true);
         divTblwebservice_Visible = 0;
         AssignProp(sPrefix, false, divTblwebservice_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblwebservice_Visible), 5, 0), true);
         divTblexternal_Visible = 0;
         AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
         divTblotpauthentication_Visible = 0;
         AssignProp(sPrefix, false, divTblotpauthentication_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpauthentication_Visible), 5, 0), true);
         divTblexternal_Visible = 0;
         AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
         divTblotpconfiguration_Visible = 0;
         AssignProp(sPrefix, false, divTblotpconfiguration_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpconfiguration_Visible), 5, 0), true);
         if ( ( StringUtil.StrCmp(AV69TypeId, "APIkey") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 ) )
         {
            AV37FunctionId = "OnlyAuthentication";
            AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
            cmbavFunctionid.Enabled = 0;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         }
         else
         {
            cmbavFunctionid.Enabled = 1;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         }
         if ( ( StringUtil.StrCmp(AV69TypeId, "APIkey") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 ) )
         {
         }
         if ( StringUtil.StrCmp(AV69TypeId, "APIkey") == 0 )
         {
            cmbavImpersonate.Visible = 0;
            AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavImpersonate.Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "GAMLocal") == 0 )
         {
            cmbavImpersonate.Visible = 0;
            AssignProp(sPrefix, false, cmbavImpersonate_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavImpersonate.Visible), 5, 0), true);
         }
         else if ( ( StringUtil.StrCmp(AV69TypeId, "AppleID") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Facebook") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "Google") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 ) || ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 ) )
         {
            if ( StringUtil.StrCmp(AV69TypeId, "WeChat") == 0 )
            {
               edtavVersionpath_Visible = 0;
               AssignProp(sPrefix, false, edtavVersionpath_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavVersionpath_Visible), 5, 0), true);
            }
            if ( StringUtil.StrCmp(AV69TypeId, "GAMRemote") == 0 )
            {
               edtavVersionpath_Visible = 0;
               AssignProp(sPrefix, false, edtavVersionpath_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavVersionpath_Visible), 5, 0), true);
               divTblscopes_Visible = 1;
               AssignProp(sPrefix, false, divTblscopes_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblscopes_Visible), 5, 0), true);
               divTblserverhost_Visible = 1;
               AssignProp(sPrefix, false, divTblserverhost_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblserverhost_Visible), 5, 0), true);
            }
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "GAMRemoteRest") == 0 )
         {
            divTblscopes_Visible = 1;
            AssignProp(sPrefix, false, divTblscopes_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblscopes_Visible), 5, 0), true);
            divTblserverhost_Visible = 1;
            AssignProp(sPrefix, false, divTblserverhost_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblserverhost_Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "OTP") == 0 )
         {
            divTblotpauthentication_Visible = 1;
            AssignProp(sPrefix, false, divTblotpauthentication_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpauthentication_Visible), 5, 0), true);
            divTblotpconfiguration_Visible = 1;
            AssignProp(sPrefix, false, divTblotpconfiguration_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblotpconfiguration_Visible), 5, 0), true);
            /* Execute user subroutine: 'DISPLAYBYOTPGENERATIONTYPE' */
            S282 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Twitter") == 0 )
         {
            AV37FunctionId = "OnlyAuthentication";
            AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
            cmbavFunctionid.Enabled = 0;
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "ExternalWebService") == 0 )
         {
            divTblwebservice_Visible = 1;
            AssignProp(sPrefix, false, divTblwebservice_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblwebservice_Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV69TypeId, "Custom") == 0 )
         {
            divTblexternal_Visible = 1;
            AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
         }
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S242 ();
         if (returnInSub) return;
      }

      protected void S282( )
      {
         /* 'DISPLAYBYOTPGENERATIONTYPE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV54OTPGenerationType, "gam") == 0 )
         {
            divTblexternal_Visible = 0;
            AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
            divTblsendandvalidateotpcode_Visible = 1;
            AssignProp(sPrefix, false, divTblsendandvalidateotpcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblsendandvalidateotpcode_Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV54OTPGenerationType, "custom") == 0 )
         {
            divTblexternal_Visible = 1;
            AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
            divTblsendandvalidateotpcode_Visible = 1;
            AssignProp(sPrefix, false, divTblsendandvalidateotpcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblsendandvalidateotpcode_Visible), 5, 0), true);
            /* Execute user subroutine: 'GETEVENTLISTGENERATECODE' */
            S272 ();
            if (returnInSub) return;
         }
         else if ( StringUtil.StrCmp(AV54OTPGenerationType, "totp") == 0 )
         {
            divTblexternal_Visible = 0;
            AssignProp(sPrefix, false, divTblexternal_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblexternal_Visible), 5, 0), true);
            divTblsendandvalidateotpcode_Visible = 0;
            AssignProp(sPrefix, false, divTblsendandvalidateotpcode_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblsendandvalidateotpcode_Visible), 5, 0), true);
         }
      }

      protected void S292( )
      {
         /* 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' Routine */
         returnInSub = false;
         AV95GXV3 = 1;
         AV94GXV2 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getauthenticationtypes(AV38GAMAuthenticationTypeFilter, out  AV35Errors);
         while ( AV95GXV3 <= AV94GXV2.Count )
         {
            AV8AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType)AV94GXV2.Item(AV95GXV3));
            if ( ( StringUtil.StrCmp(AV8AuthenticationType.gxTpr_Name, AV46Name) != 0 ) && ( StringUtil.StrCmp(AV8AuthenticationType.gxTpr_Typeid, "OTP") != 0 ) )
            {
               cmbavImpersonate.addItem(AV8AuthenticationType.gxTpr_Name, AV8AuthenticationType.gxTpr_Name, 0);
            }
            AV95GXV3 = (int)(AV95GXV3+1);
         }
      }

      protected void S262( )
      {
         /* 'GETLISTAUTHENTICATIONTYPEOTP' Routine */
         returnInSub = false;
         AV38GAMAuthenticationTypeFilter.gxTpr_Type = "OTP";
         AV97GXV5 = 1;
         AV96GXV4 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getauthenticationtypes(AV38GAMAuthenticationTypeFilter, out  AV35Errors);
         while ( AV97GXV5 <= AV96GXV4.Count )
         {
            AV8AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType)AV96GXV4.Item(AV97GXV5));
            cmbavTfaauthenticationtypename.addItem(AV8AuthenticationType.gxTpr_Name, AV8AuthenticationType.gxTpr_Description, 0);
            AV97GXV5 = (int)(AV97GXV5+1);
         }
      }

      protected void S302( )
      {
         /* 'GETEVENTLISTVALIDATEUSER' Routine */
         returnInSub = false;
         AV39GAMEventSubscriptionFilter.gxTpr_Event = "user-otp-validateuser";
         AV99GXV7 = 1;
         AV98GXV6 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).geteventsubscriptions(AV39GAMEventSubscriptionFilter, out  AV35Errors);
         while ( AV99GXV7 <= AV98GXV6.Count )
         {
            AV36EventSuscription = ((GeneXus.Programs.genexussecurity.SdtGAMEventSubscription)AV98GXV6.Item(AV99GXV7));
            cmbavOtpeventvalidateuser.addItem(AV36EventSuscription.gxTpr_Id, AV36EventSuscription.gxTpr_Description, 0);
            AV99GXV7 = (int)(AV99GXV7+1);
         }
      }

      protected void S272( )
      {
         /* 'GETEVENTLISTGENERATECODE' Routine */
         returnInSub = false;
         AV39GAMEventSubscriptionFilter.gxTpr_Event = "user-otp-generatecode";
         AV101GXV9 = 1;
         AV100GXV8 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).geteventsubscriptions(AV39GAMEventSubscriptionFilter, out  AV35Errors);
         while ( AV101GXV9 <= AV100GXV8.Count )
         {
            AV36EventSuscription = ((GeneXus.Programs.genexussecurity.SdtGAMEventSubscription)AV100GXV8.Item(AV101GXV9));
            cmbavOtpgenerationtype_customeventgeneratecode.addItem(AV36EventSuscription.gxTpr_Id, AV36EventSuscription.gxTpr_Description, 0);
            AV101GXV9 = (int)(AV101GXV9+1);
         }
      }

      protected void S312( )
      {
         /* 'GETEVENTLISTSENDCODE' Routine */
         returnInSub = false;
         AV39GAMEventSubscriptionFilter.gxTpr_Event = "user-otp-sendcode";
         AV103GXV11 = 1;
         AV102GXV10 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).geteventsubscriptions(AV39GAMEventSubscriptionFilter, out  AV35Errors);
         while ( AV103GXV11 <= AV102GXV10.Count )
         {
            AV36EventSuscription = ((GeneXus.Programs.genexussecurity.SdtGAMEventSubscription)AV102GXV10.Item(AV103GXV11));
            cmbavOtpeventsendcode.addItem(AV36EventSuscription.gxTpr_Id, AV36EventSuscription.gxTpr_Description, 0);
            AV103GXV11 = (int)(AV103GXV11+1);
         }
      }

      protected void S322( )
      {
         /* 'GETEVENTLISTVALIDATECODE' Routine */
         returnInSub = false;
         AV39GAMEventSubscriptionFilter.gxTpr_Event = "user-otp-validatecode";
         AV105GXV13 = 1;
         AV104GXV12 = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).geteventsubscriptions(AV39GAMEventSubscriptionFilter, out  AV35Errors);
         while ( AV105GXV13 <= AV104GXV12.Count )
         {
            AV36EventSuscription = ((GeneXus.Programs.genexussecurity.SdtGAMEventSubscription)AV104GXV12.Item(AV105GXV13));
            cmbavOtpeventvalidatecode.addItem(AV36EventSuscription.gxTpr_Id, AV36EventSuscription.gxTpr_Description, 0);
            AV105GXV13 = (int)(AV105GXV13+1);
         }
      }

      protected void S122( )
      {
         /* 'INITAUTHENTICATIONTYPEAPPLE' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV9AuthenticationTypeApple.load( AV46Name);
         AV46Name = AV9AuthenticationTypeApple.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV45IsEnable = AV9AuthenticationTypeApple.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV9AuthenticationTypeApple.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV9AuthenticationTypeApple.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV9AuthenticationTypeApple.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV9AuthenticationTypeApple.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV24ClientId = AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Clientid;
         AssignAttri(sPrefix, false, "AV24ClientId", AV24ClientId);
         AV25ClientSecret = AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Clientsecret;
         AssignAttri(sPrefix, false, "AV25ClientSecret", AV25ClientSecret);
         AV62SiteURL = AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Siteurl;
         AssignAttri(sPrefix, false, "AV62SiteURL", AV62SiteURL);
         AV6AdditionalScope = AV9AuthenticationTypeApple.gxTpr_Apple.gxTpr_Additionalscope;
         AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
      }

      protected void S132( )
      {
         /* 'INITAUTHENTICATIONTYPECUSTOM' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEOTP' */
         S262 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 1;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV10AuthenticationTypeCustom.load( AV46Name);
         AV46Name = AV10AuthenticationTypeCustom.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV37FunctionId = AV10AuthenticationTypeCustom.gxTpr_Functionid;
         AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         AV45IsEnable = AV10AuthenticationTypeCustom.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV10AuthenticationTypeCustom.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV10AuthenticationTypeCustom.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV10AuthenticationTypeCustom.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV10AuthenticationTypeCustom.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV66TFAEnable = AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Enable;
         AssignAttri(sPrefix, false, "AV66TFAEnable", AV66TFAEnable);
         if ( AV66TFAEnable )
         {
            AV65TFAAuthenticationTypeName = AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename;
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
            AV67TFAFirstFactorAuthenticationExpiration = AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration;
            AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            AV68TFAForceForAllUsers = AV10AuthenticationTypeCustom.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers;
            AssignAttri(sPrefix, false, "AV68TFAForceForAllUsers", AV68TFAForceForAllUsers);
         }
         AV32CusVersion = AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Version;
         AssignAttri(sPrefix, false, "AV32CusVersion", AV32CusVersion);
         AV31CusPrivateEncryptKey = AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Privateencryptkey;
         AssignAttri(sPrefix, false, "AV31CusPrivateEncryptKey", AV31CusPrivateEncryptKey);
         AV29CusFileName = AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Filename;
         AssignAttri(sPrefix, false, "AV29CusFileName", AV29CusFileName);
         AV30CusPackage = AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Package;
         AssignAttri(sPrefix, false, "AV30CusPackage", AV30CusPackage);
         AV28CusClassName = AV10AuthenticationTypeCustom.gxTpr_Custom.gxTpr_Classname;
         AssignAttri(sPrefix, false, "AV28CusClassName", AV28CusClassName);
      }

      protected void S142( )
      {
         /* 'INITAUTHENTICATIONTYPEEXTERNALWEBSERVICE' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEOTP' */
         S262 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 1;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV19AuthenticationTypeWebService.load( AV46Name);
         AV46Name = AV19AuthenticationTypeWebService.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV37FunctionId = AV19AuthenticationTypeWebService.gxTpr_Functionid;
         AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         AV45IsEnable = AV19AuthenticationTypeWebService.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV19AuthenticationTypeWebService.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV19AuthenticationTypeWebService.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV19AuthenticationTypeWebService.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV19AuthenticationTypeWebService.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV66TFAEnable = AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Enable;
         AssignAttri(sPrefix, false, "AV66TFAEnable", AV66TFAEnable);
         if ( AV66TFAEnable )
         {
            AV65TFAAuthenticationTypeName = AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename;
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
            AV67TFAFirstFactorAuthenticationExpiration = AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration;
            AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            AV68TFAForceForAllUsers = AV19AuthenticationTypeWebService.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers;
            AssignAttri(sPrefix, false, "AV68TFAForceForAllUsers", AV68TFAForceForAllUsers);
         }
         AV80WSVersion = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Version;
         AssignAttri(sPrefix, false, "AV80WSVersion", AV80WSVersion);
         AV74WSPrivateEncryptKey = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Privateencryptkey;
         AssignAttri(sPrefix, false, "AV74WSPrivateEncryptKey", AV74WSPrivateEncryptKey);
         AV76WSServerName = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV76WSServerName", AV76WSServerName);
         AV77WSServerPort = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Port;
         AssignAttri(sPrefix, false, "AV77WSServerPort", StringUtil.LTrimStr( (decimal)(AV77WSServerPort), 5, 0));
         AV75WSServerBaseURL = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Baseurl;
         AssignAttri(sPrefix, false, "AV75WSServerBaseURL", AV75WSServerBaseURL);
         AV78WSServerSecureProtocol = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Server.gxTpr_Secureprotocol;
         AssignAttri(sPrefix, false, "AV78WSServerSecureProtocol", StringUtil.Str( (decimal)(AV78WSServerSecureProtocol), 1, 0));
         AV79WSTimeout = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Timeout;
         AssignAttri(sPrefix, false, "AV79WSTimeout", StringUtil.LTrimStr( (decimal)(AV79WSTimeout), 5, 0));
         AV73WSPackage = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Package;
         AssignAttri(sPrefix, false, "AV73WSPackage", AV73WSPackage);
         AV72WSName = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV72WSName", AV72WSName);
         AV71WSExtension = AV19AuthenticationTypeWebService.gxTpr_Webservice.gxTpr_Extension;
         AssignAttri(sPrefix, false, "AV71WSExtension", AV71WSExtension);
      }

      protected void S152( )
      {
         /* 'INITAUTHENTICATIONTYPEFACEBOOK' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV11AuthenticationTypeFacebook.load( AV46Name);
         AV46Name = AV11AuthenticationTypeFacebook.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV45IsEnable = AV11AuthenticationTypeFacebook.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV11AuthenticationTypeFacebook.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV11AuthenticationTypeFacebook.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV11AuthenticationTypeFacebook.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV11AuthenticationTypeFacebook.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV24ClientId = AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Clientid;
         AssignAttri(sPrefix, false, "AV24ClientId", AV24ClientId);
         AV25ClientSecret = AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Clientsecret;
         AssignAttri(sPrefix, false, "AV25ClientSecret", AV25ClientSecret);
         AV70VersionPath = AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Versionpath;
         AssignAttri(sPrefix, false, "AV70VersionPath", AV70VersionPath);
         AV62SiteURL = AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Siteurl;
         AssignAttri(sPrefix, false, "AV62SiteURL", AV62SiteURL);
         AV6AdditionalScope = AV11AuthenticationTypeFacebook.gxTpr_Facebook.gxTpr_Additionalscope;
         AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
      }

      protected void S162( )
      {
         /* 'INITAUTHENTICATIONTYPEGAMLOCAL' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEOTP' */
         S262 ();
         if (returnInSub) return;
         AV15AuthenticationTypeLocal.load( AV46Name);
         AV46Name = AV15AuthenticationTypeLocal.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV37FunctionId = AV15AuthenticationTypeLocal.gxTpr_Functionid;
         AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         AV45IsEnable = AV15AuthenticationTypeLocal.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV15AuthenticationTypeLocal.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV15AuthenticationTypeLocal.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV15AuthenticationTypeLocal.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV66TFAEnable = AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Enable;
         AssignAttri(sPrefix, false, "AV66TFAEnable", AV66TFAEnable);
         if ( AV66TFAEnable )
         {
            AV65TFAAuthenticationTypeName = AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename;
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
            AV67TFAFirstFactorAuthenticationExpiration = AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration;
            AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            AV68TFAForceForAllUsers = AV15AuthenticationTypeLocal.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers;
            AssignAttri(sPrefix, false, "AV68TFAForceForAllUsers", AV68TFAForceForAllUsers);
         }
      }

      protected void S172( )
      {
         /* 'INITAUTHENTICATIONTYPEGAMREMOTE' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 1;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV12AuthenticationTypeGAMRemote.load( AV46Name);
         AV46Name = AV12AuthenticationTypeGAMRemote.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV37FunctionId = AV12AuthenticationTypeGAMRemote.gxTpr_Functionid;
         AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         AV45IsEnable = AV12AuthenticationTypeGAMRemote.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV12AuthenticationTypeGAMRemote.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV12AuthenticationTypeGAMRemote.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV12AuthenticationTypeGAMRemote.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV12AuthenticationTypeGAMRemote.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV24ClientId = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Clientid;
         AssignAttri(sPrefix, false, "AV24ClientId", AV24ClientId);
         AV25ClientSecret = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Clientsecret;
         AssignAttri(sPrefix, false, "AV25ClientSecret", AV25ClientSecret);
         AV62SiteURL = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Siteurl;
         AssignAttri(sPrefix, false, "AV62SiteURL", AV62SiteURL);
         AV63SiteURLCallbackIsCustom = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Siteurlcallbackiscustom;
         AssignAttri(sPrefix, false, "AV63SiteURLCallbackIsCustom", AV63SiteURLCallbackIsCustom);
         AV5AddInitialPropertiesScope = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Addsessioninitialpropertiesscope;
         AssignAttri(sPrefix, false, "AV5AddInitialPropertiesScope", AV5AddInitialPropertiesScope);
         AV7AddUserAdditionalDataScope = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Adduseradditionaldatascope;
         AssignAttri(sPrefix, false, "AV7AddUserAdditionalDataScope", AV7AddUserAdditionalDataScope);
         AV6AdditionalScope = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Additionalscope;
         AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
         AV43GAMRServerURL = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Remoteserverurl;
         AssignAttri(sPrefix, false, "AV43GAMRServerURL", AV43GAMRServerURL);
         AV41GAMRPrivateEncryptKey = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Remoteserverkey;
         AssignAttri(sPrefix, false, "AV41GAMRPrivateEncryptKey", AV41GAMRPrivateEncryptKey);
         AV42GAMRRepositoryGUID = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Remoterepositoryguid;
         AssignAttri(sPrefix, false, "AV42GAMRRepositoryGUID", AV42GAMRRepositoryGUID);
         AV21AutovalidateExternalTokenAndRefresh = AV12AuthenticationTypeGAMRemote.gxTpr_Gamremote.gxTpr_Autovalidateexternaltokenandrefresh;
         AssignAttri(sPrefix, false, "AV21AutovalidateExternalTokenAndRefresh", AV21AutovalidateExternalTokenAndRefresh);
      }

      protected void S182( )
      {
         /* 'INITAUTHENTICATIONTYPEGAMREMOTEREST' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEOTP' */
         S262 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 1;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV13AuthenticationTypeGAMRemoteRest.load( AV46Name);
         AV46Name = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV37FunctionId = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Functionid;
         AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         AV45IsEnable = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV66TFAEnable = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Enable;
         AssignAttri(sPrefix, false, "AV66TFAEnable", AV66TFAEnable);
         if ( AV66TFAEnable )
         {
            AV65TFAAuthenticationTypeName = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Authenticationtypename;
            AssignAttri(sPrefix, false, "AV65TFAAuthenticationTypeName", AV65TFAAuthenticationTypeName);
            AV67TFAFirstFactorAuthenticationExpiration = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Firstauthenticationfactorexpiration;
            AssignAttri(sPrefix, false, "AV67TFAFirstFactorAuthenticationExpiration", StringUtil.LTrimStr( (decimal)(AV67TFAFirstFactorAuthenticationExpiration), 9, 0));
            AV68TFAForceForAllUsers = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Twofactorauthentication.gxTpr_Forceforallusers;
            AssignAttri(sPrefix, false, "AV68TFAForceForAllUsers", AV68TFAForceForAllUsers);
         }
         AV24ClientId = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Clientid;
         AssignAttri(sPrefix, false, "AV24ClientId", AV24ClientId);
         AV25ClientSecret = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Clientsecret;
         AssignAttri(sPrefix, false, "AV25ClientSecret", AV25ClientSecret);
         AV70VersionPath = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Versionpath;
         AssignAttri(sPrefix, false, "AV70VersionPath", AV70VersionPath);
         AV5AddInitialPropertiesScope = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Addsessioninitialpropertiesscope;
         AssignAttri(sPrefix, false, "AV5AddInitialPropertiesScope", AV5AddInitialPropertiesScope);
         AV7AddUserAdditionalDataScope = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Adduseradditionaldatascope;
         AssignAttri(sPrefix, false, "AV7AddUserAdditionalDataScope", AV7AddUserAdditionalDataScope);
         AV6AdditionalScope = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Additionalscope;
         AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
         AV40GAMRAuthenticationTypeName = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoteauthenticationtypename;
         AssignAttri(sPrefix, false, "AV40GAMRAuthenticationTypeName", AV40GAMRAuthenticationTypeName);
         AV43GAMRServerURL = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoteserverurl;
         AssignAttri(sPrefix, false, "AV43GAMRServerURL", AV43GAMRServerURL);
         AV41GAMRPrivateEncryptKey = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoteserverkey;
         AssignAttri(sPrefix, false, "AV41GAMRPrivateEncryptKey", AV41GAMRPrivateEncryptKey);
         AV42GAMRRepositoryGUID = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Remoterepositoryguid;
         AssignAttri(sPrefix, false, "AV42GAMRRepositoryGUID", AV42GAMRRepositoryGUID);
         AV21AutovalidateExternalTokenAndRefresh = AV13AuthenticationTypeGAMRemoteRest.gxTpr_Gamremoterest.gxTpr_Autovalidateexternaltokenandrefresh;
         AssignAttri(sPrefix, false, "AV21AutovalidateExternalTokenAndRefresh", AV21AutovalidateExternalTokenAndRefresh);
      }

      protected void S192( )
      {
         /* 'INITAUTHENTICATIONTYPEGOOGLE' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV14AuthenticationTypeGoogle.load( AV46Name);
         AV46Name = AV14AuthenticationTypeGoogle.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV45IsEnable = AV14AuthenticationTypeGoogle.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV14AuthenticationTypeGoogle.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV14AuthenticationTypeGoogle.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV14AuthenticationTypeGoogle.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV14AuthenticationTypeGoogle.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV24ClientId = AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Clientid;
         AssignAttri(sPrefix, false, "AV24ClientId", AV24ClientId);
         AV25ClientSecret = AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Clientsecret;
         AssignAttri(sPrefix, false, "AV25ClientSecret", AV25ClientSecret);
         AV70VersionPath = AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Versionpath;
         AssignAttri(sPrefix, false, "AV70VersionPath", AV70VersionPath);
         AV62SiteURL = AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Siteurl;
         AssignAttri(sPrefix, false, "AV62SiteURL", AV62SiteURL);
         AV6AdditionalScope = AV14AuthenticationTypeGoogle.gxTpr_Google.gxTpr_Additionalscope;
         AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
      }

      protected void S202( )
      {
         /* 'INITAUTHENTICATIONTYPEOTP' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'GETEVENTLISTVALIDATEUSER' */
         S302 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'GETEVENTLISTSENDCODE' */
         S312 ();
         if (returnInSub) return;
         /* Execute user subroutine: 'GETEVENTLISTVALIDATECODE' */
         S322 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV37FunctionId = "OnlyAuthentication";
         AssignAttri(sPrefix, false, "AV37FunctionId", AV37FunctionId);
         AV17AuthenticationTypeOTP.load( AV46Name);
         AV46Name = AV17AuthenticationTypeOTP.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV45IsEnable = AV17AuthenticationTypeOTP.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV17AuthenticationTypeOTP.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV17AuthenticationTypeOTP.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV17AuthenticationTypeOTP.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV17AuthenticationTypeOTP.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV61OTPUseForFirstFactorAuthentication = AV17AuthenticationTypeOTP.gxTpr_Useforfirstfactorauthentication;
         AssignAttri(sPrefix, false, "AV61OTPUseForFirstFactorAuthentication", AV61OTPUseForFirstFactorAuthentication);
         AV52OTPEventValidateUser = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Eventvaliduser;
         AssignAttri(sPrefix, false, "AV52OTPEventValidateUser", AV52OTPEventValidateUser);
         AV54OTPGenerationType = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Generationtype;
         AssignAttri(sPrefix, false, "AV54OTPGenerationType", AV54OTPGenerationType);
         AV55OTPGenerationType_CustomEventGenerateCode = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Generationtype_customeventgeneratecode;
         AssignAttri(sPrefix, false, "AV55OTPGenerationType_CustomEventGenerateCode", AV55OTPGenerationType_CustomEventGenerateCode);
         AV49OTPCodeExpirationTimeout = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Codeexpirationtimeout;
         AssignAttri(sPrefix, false, "AV49OTPCodeExpirationTimeout", StringUtil.LTrimStr( (decimal)(AV49OTPCodeExpirationTimeout), 9, 0));
         AV58OTPMaximumDailyNumberCodes = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Maximumdailynumbercodes;
         AssignAttri(sPrefix, false, "AV58OTPMaximumDailyNumberCodes", StringUtil.LTrimStr( (decimal)(AV58OTPMaximumDailyNumberCodes), 4, 0));
         AV47OTPAutogeneratedCodeLength = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Autogeneratedcodelength;
         AssignAttri(sPrefix, false, "AV47OTPAutogeneratedCodeLength", StringUtil.LTrimStr( (decimal)(AV47OTPAutogeneratedCodeLength), 4, 0));
         AV53OTPGenerateCodeOnlyNumbers = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Generatecodeonlynumbers;
         AssignAttri(sPrefix, false, "AV53OTPGenerateCodeOnlyNumbers", AV53OTPGenerateCodeOnlyNumbers);
         AV60OTPNumberUnsuccessfulRetriesToLockOTP = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Numberunsuccessfulretriestolockotp;
         AssignAttri(sPrefix, false, "AV60OTPNumberUnsuccessfulRetriesToLockOTP", StringUtil.LTrimStr( (decimal)(AV60OTPNumberUnsuccessfulRetriesToLockOTP), 4, 0));
         AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Numberunsuccessfulretriestoblockuserbasedofotplocks;
         AssignAttri(sPrefix, false, "AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks", StringUtil.LTrimStr( (decimal)(AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks), 4, 0));
         AV48OTPAutoUnlockTime = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Automaticotpunlocktime;
         AssignAttri(sPrefix, false, "AV48OTPAutoUnlockTime", StringUtil.LTrimStr( (decimal)(AV48OTPAutoUnlockTime), 9, 0));
         AV50OTPEventSendCode = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Eventsendcode;
         AssignAttri(sPrefix, false, "AV50OTPEventSendCode", AV50OTPEventSendCode);
         AV57OTPMailMessageSubject = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Mailmessagesubject;
         AssignAttri(sPrefix, false, "AV57OTPMailMessageSubject", AV57OTPMailMessageSubject);
         AV56OTPMailMessageBodyHTML = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Mailmessagebodyhtml;
         AssignAttri(sPrefix, false, "AV56OTPMailMessageBodyHTML", AV56OTPMailMessageBodyHTML);
         AV51OTPEventValidateCode = AV17AuthenticationTypeOTP.gxTpr_Otp.gxTpr_Eventvalidatecode;
         AssignAttri(sPrefix, false, "AV51OTPEventValidateCode", AV51OTPEventValidateCode);
      }

      protected void S212( )
      {
         /* 'INITAUTHENTICATIONTYPETWITTER' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV18AuthenticationTypeTwitter.load( AV46Name);
         AV46Name = AV18AuthenticationTypeTwitter.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV45IsEnable = AV18AuthenticationTypeTwitter.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV18AuthenticationTypeTwitter.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV18AuthenticationTypeTwitter.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV18AuthenticationTypeTwitter.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV18AuthenticationTypeTwitter.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV26ConsumerKey = AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Consumerkey;
         AssignAttri(sPrefix, false, "AV26ConsumerKey", AV26ConsumerKey);
         AV27ConsumerSecret = AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Consumersecret;
         AssignAttri(sPrefix, false, "AV27ConsumerSecret", AV27ConsumerSecret);
         AV23CallbackURL = AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Callbackurl;
         AssignAttri(sPrefix, false, "AV23CallbackURL", AV23CallbackURL);
         AV6AdditionalScope = AV18AuthenticationTypeTwitter.gxTpr_Twitter.gxTpr_Additionalscope;
         AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
      }

      protected void S222( )
      {
         /* 'INITAUTHENTICATIONTYPEWECHAT' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'GETLISTAUTHENTICATIONTYPEIMPERSONATE' */
         S292 ();
         if (returnInSub) return;
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         AV20AuthenticationTypeWeChat.load( AV46Name);
         AV46Name = AV20AuthenticationTypeWeChat.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV45IsEnable = AV20AuthenticationTypeWeChat.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV45IsEnable", AV45IsEnable);
         AV33Dsc = AV20AuthenticationTypeWeChat.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV33Dsc", AV33Dsc);
         AV64SmallImageName = AV20AuthenticationTypeWeChat.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV64SmallImageName", AV64SmallImageName);
         AV22BigImageName = AV20AuthenticationTypeWeChat.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV22BigImageName", AV22BigImageName);
         AV44Impersonate = AV20AuthenticationTypeWeChat.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV44Impersonate", AV44Impersonate);
         AV24ClientId = AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Clientid;
         AssignAttri(sPrefix, false, "AV24ClientId", AV24ClientId);
         AV25ClientSecret = AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Clientsecret;
         AssignAttri(sPrefix, false, "AV25ClientSecret", AV25ClientSecret);
         AV70VersionPath = AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Versionpath;
         AssignAttri(sPrefix, false, "AV70VersionPath", AV70VersionPath);
         AV62SiteURL = AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Siteurl;
         AssignAttri(sPrefix, false, "AV62SiteURL", AV62SiteURL);
         AV6AdditionalScope = AV20AuthenticationTypeWeChat.gxTpr_Wechat.gxTpr_Additionalscope;
         AssignAttri(sPrefix, false, "AV6AdditionalScope", AV6AdditionalScope);
      }

      protected void nextLoad( )
      {
      }

      protected void E171I2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      protected void wb_table2_314_1I2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablemergedcusprivateencryptkey_Internalname, tblTablemergedcusprivateencryptkey_Internalname, "", "TableMerged", 0, "", "", 0, 0, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td class='MergeDataCell'>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCusprivateencryptkey_Internalname, context.GetMessage( "Cus Private Encrypt Key", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 318,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCusprivateencryptkey_Internalname, StringUtil.RTrim( AV31CusPrivateEncryptKey), StringUtil.RTrim( context.localUtil.Format( AV31CusPrivateEncryptKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,318);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCusprivateencryptkey_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCusprivateencryptkey_Enabled, 1, "text", "", 32, "chr", 1, "row", 32, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td class='CellMarginLeft'>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 320,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button ButtonMaterialGAM";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtngenkeycustom_Internalname, "", context.GetMessage( "WWP_GAM_GenerateKey", ""), bttBtngenkeycustom_Jsonclick, 5, context.GetMessage( "WWP_GAM_GenerateKey", ""), "", StyleString, ClassString, bttBtngenkeycustom_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOGENKEYCUSTOM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table2_314_1I2e( true) ;
         }
         else
         {
            wb_table2_314_1I2e( false) ;
         }
      }

      protected void wb_table1_258_1I2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablemergedwsprivateencryptkey_Internalname, tblTablemergedwsprivateencryptkey_Internalname, "", "TableMerged", 0, "", "", 0, 0, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td class='MergeDataCell'>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavWsprivateencryptkey_Internalname, context.GetMessage( "WSPrivate Encrypt Key", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 262,'" + sPrefix + "',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavWsprivateencryptkey_Internalname, StringUtil.RTrim( AV74WSPrivateEncryptKey), StringUtil.RTrim( context.localUtil.Format( AV74WSPrivateEncryptKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,262);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavWsprivateencryptkey_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavWsprivateencryptkey_Enabled, 1, "text", "", 32, "chr", 1, "row", 32, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td class='CellMarginLeft'>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 264,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button ButtonMaterialGAM";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtngenkey_Internalname, "", context.GetMessage( "WWP_GAM_GenerateKey", ""), bttBtngenkey_Jsonclick, 5, context.GetMessage( "WWP_GAM_GenerateKey", ""), "", StyleString, ClassString, bttBtngenkey_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOGENKEY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWCAuthenticationTypeEntryGeneral.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_258_1I2e( true) ;
         }
         else
         {
            wb_table1_258_1I2e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         Gx_mode = (string)getParm(obj,0);
         AssignAttri(sPrefix, false, "Gx_mode", Gx_mode);
         AV46Name = (string)getParm(obj,1);
         AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         AV69TypeId = (string)getParm(obj,2);
         AssignAttri(sPrefix, false, "AV69TypeId", AV69TypeId);
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
         PA1I2( ) ;
         WS1I2( ) ;
         WE1I2( ) ;
         cleanup();
         context.SetWrapped(false);
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected override EncryptionType GetEncryptionType( )
      {
         return EncryptionType.SESSION ;
      }

      public override void componentbind( Object[] obj )
      {
         if ( IsUrlCreated( ) )
         {
            return  ;
         }
         sCtrlGx_mode = (string)((string)getParm(obj,0));
         sCtrlAV46Name = (string)((string)getParm(obj,1));
         sCtrlAV69TypeId = (string)((string)getParm(obj,2));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA1I2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "gamwcauthenticationtypeentrygeneral", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA1I2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            Gx_mode = (string)getParm(obj,2);
            AssignAttri(sPrefix, false, "Gx_mode", Gx_mode);
            AV46Name = (string)getParm(obj,3);
            AssignAttri(sPrefix, false, "AV46Name", AV46Name);
            AV69TypeId = (string)getParm(obj,4);
            AssignAttri(sPrefix, false, "AV69TypeId", AV69TypeId);
         }
         wcpOGx_mode = cgiGet( sPrefix+"wcpOGx_mode");
         wcpOAV46Name = cgiGet( sPrefix+"wcpOAV46Name");
         wcpOAV69TypeId = cgiGet( sPrefix+"wcpOAV69TypeId");
         if ( ! GetJustCreated( ) && ( ( StringUtil.StrCmp(Gx_mode, wcpOGx_mode) != 0 ) || ( StringUtil.StrCmp(AV46Name, wcpOAV46Name) != 0 ) || ( StringUtil.StrCmp(AV69TypeId, wcpOAV69TypeId) != 0 ) ) )
         {
            setjustcreated();
         }
         wcpOGx_mode = Gx_mode;
         wcpOAV46Name = AV46Name;
         wcpOAV69TypeId = AV69TypeId;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlGx_mode = cgiGet( sPrefix+"Gx_mode_CTRL");
         if ( StringUtil.Len( sCtrlGx_mode) > 0 )
         {
            Gx_mode = cgiGet( sCtrlGx_mode);
            AssignAttri(sPrefix, false, "Gx_mode", Gx_mode);
         }
         else
         {
            Gx_mode = cgiGet( sPrefix+"Gx_mode_PARM");
         }
         sCtrlAV46Name = cgiGet( sPrefix+"AV46Name_CTRL");
         if ( StringUtil.Len( sCtrlAV46Name) > 0 )
         {
            AV46Name = cgiGet( sCtrlAV46Name);
            AssignAttri(sPrefix, false, "AV46Name", AV46Name);
         }
         else
         {
            AV46Name = cgiGet( sPrefix+"AV46Name_PARM");
         }
         sCtrlAV69TypeId = cgiGet( sPrefix+"AV69TypeId_CTRL");
         if ( StringUtil.Len( sCtrlAV69TypeId) > 0 )
         {
            AV69TypeId = cgiGet( sCtrlAV69TypeId);
            AssignAttri(sPrefix, false, "AV69TypeId", AV69TypeId);
         }
         else
         {
            AV69TypeId = cgiGet( sPrefix+"AV69TypeId_PARM");
         }
      }

      public override void componentprocess( string sPPrefix ,
                                             string sPSFPrefix ,
                                             string sCompEvt )
      {
         sCompPrefix = sPPrefix;
         sSFPrefix = sPSFPrefix;
         sPrefix = sCompPrefix + sSFPrefix;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         INITWEB( ) ;
         nDraw = 0;
         PA1I2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS1I2( ) ;
         if ( isFullAjaxMode( ) )
         {
            componentdraw();
         }
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override void componentstart( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
      }

      protected void WCStart( )
      {
         nDraw = 1;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WS1I2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"Gx_mode_PARM", StringUtil.RTrim( Gx_mode));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlGx_mode)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"Gx_mode_CTRL", StringUtil.RTrim( sCtrlGx_mode));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV46Name_PARM", StringUtil.RTrim( AV46Name));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV46Name)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV46Name_CTRL", StringUtil.RTrim( sCtrlAV46Name));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV69TypeId_PARM", StringUtil.RTrim( AV69TypeId));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV69TypeId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV69TypeId_CTRL", StringUtil.RTrim( sCtrlAV69TypeId));
         }
      }

      public override void componentdraw( )
      {
         if ( nDoneStart == 0 )
         {
            WCStart( ) ;
         }
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         WCParametersSet( ) ;
         WE1I2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      public override string getstring( string sGXControl )
      {
         string sCtrlName;
         if ( StringUtil.StrCmp(StringUtil.Substring( sGXControl, 1, 1), "&") == 0 )
         {
            sCtrlName = StringUtil.Substring( sGXControl, 2, StringUtil.Len( sGXControl)-1);
         }
         else
         {
            sCtrlName = sGXControl;
         }
         return cgiGet( sPrefix+"v"+StringUtil.Upper( sCtrlName)) ;
      }

      public override void componentjscripts( )
      {
         include_jscripts( ) ;
      }

      public override void componentthemes( )
      {
         define_styles( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623481062", true, true);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         CloseStyles();
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("gamwcauthenticationtypeentrygeneral.js", "?2024121623481079", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavFunctionid.Name = "vFUNCTIONID";
         cmbavFunctionid.WebTags = "";
         cmbavFunctionid.addItem("AuthenticationAndRoles", context.GetMessage( "WWP_GAM_AuthAndRoles", ""), 0);
         cmbavFunctionid.addItem("OnlyAuthentication", context.GetMessage( "WWP_GAM_OnlyAuth", ""), 0);
         if ( cmbavFunctionid.ItemCount > 0 )
         {
         }
         chkavIsenable.Name = "vISENABLE";
         chkavIsenable.WebTags = "";
         chkavIsenable.Caption = " ";
         AssignProp(sPrefix, false, chkavIsenable_Internalname, "TitleCaption", chkavIsenable.Caption, true);
         chkavIsenable.CheckedValue = "false";
         cmbavImpersonate.Name = "vIMPERSONATE";
         cmbavImpersonate.WebTags = "";
         cmbavImpersonate.addItem("0", context.GetMessage( "WWP_GAM_None", ""), 0);
         if ( cmbavImpersonate.ItemCount > 0 )
         {
         }
         chkavTfaenable.Name = "vTFAENABLE";
         chkavTfaenable.WebTags = "";
         chkavTfaenable.Caption = " ";
         AssignProp(sPrefix, false, chkavTfaenable_Internalname, "TitleCaption", chkavTfaenable.Caption, true);
         chkavTfaenable.CheckedValue = "false";
         cmbavTfaauthenticationtypename.Name = "vTFAAUTHENTICATIONTYPENAME";
         cmbavTfaauthenticationtypename.WebTags = "";
         if ( cmbavTfaauthenticationtypename.ItemCount > 0 )
         {
         }
         chkavTfaforceforallusers.Name = "vTFAFORCEFORALLUSERS";
         chkavTfaforceforallusers.WebTags = "";
         chkavTfaforceforallusers.Caption = " ";
         AssignProp(sPrefix, false, chkavTfaforceforallusers_Internalname, "TitleCaption", chkavTfaforceforallusers.Caption, true);
         chkavTfaforceforallusers.CheckedValue = "false";
         chkavOtpuseforfirstfactorauthentication.Name = "vOTPUSEFORFIRSTFACTORAUTHENTICATION";
         chkavOtpuseforfirstfactorauthentication.WebTags = "";
         chkavOtpuseforfirstfactorauthentication.Caption = " ";
         AssignProp(sPrefix, false, chkavOtpuseforfirstfactorauthentication_Internalname, "TitleCaption", chkavOtpuseforfirstfactorauthentication.Caption, true);
         chkavOtpuseforfirstfactorauthentication.CheckedValue = "false";
         cmbavOtpeventvalidateuser.Name = "vOTPEVENTVALIDATEUSER";
         cmbavOtpeventvalidateuser.WebTags = "";
         cmbavOtpeventvalidateuser.addItem("0", context.GetMessage( "GX_EmptyItemText", ""), 0);
         if ( cmbavOtpeventvalidateuser.ItemCount > 0 )
         {
         }
         cmbavOtpgenerationtype.Name = "vOTPGENERATIONTYPE";
         cmbavOtpgenerationtype.WebTags = "";
         cmbavOtpgenerationtype.addItem("gam", context.GetMessage( "GAM_OTP", ""), 0);
         cmbavOtpgenerationtype.addItem("custom", context.GetMessage( "GAM_OTPcustom", ""), 0);
         cmbavOtpgenerationtype.addItem("totp", context.GetMessage( "GAM_TOTPAuthenticator", ""), 0);
         if ( cmbavOtpgenerationtype.ItemCount > 0 )
         {
         }
         cmbavOtpgenerationtype_customeventgeneratecode.Name = "vOTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE";
         cmbavOtpgenerationtype_customeventgeneratecode.WebTags = "";
         if ( cmbavOtpgenerationtype_customeventgeneratecode.ItemCount > 0 )
         {
         }
         chkavOtpgeneratecodeonlynumbers.Name = "vOTPGENERATECODEONLYNUMBERS";
         chkavOtpgeneratecodeonlynumbers.WebTags = "";
         chkavOtpgeneratecodeonlynumbers.Caption = " ";
         AssignProp(sPrefix, false, chkavOtpgeneratecodeonlynumbers_Internalname, "TitleCaption", chkavOtpgeneratecodeonlynumbers.Caption, true);
         chkavOtpgeneratecodeonlynumbers.CheckedValue = "false";
         cmbavOtpeventsendcode.Name = "vOTPEVENTSENDCODE";
         cmbavOtpeventsendcode.WebTags = "";
         cmbavOtpeventsendcode.addItem("0", context.GetMessage( "WWP_GAM_EmailByGAM", ""), 0);
         if ( cmbavOtpeventsendcode.ItemCount > 0 )
         {
         }
         cmbavOtpeventvalidatecode.Name = "vOTPEVENTVALIDATECODE";
         cmbavOtpeventvalidatecode.WebTags = "";
         cmbavOtpeventvalidatecode.addItem("0", context.GetMessage( "GAM", ""), 0);
         if ( cmbavOtpeventvalidatecode.ItemCount > 0 )
         {
         }
         chkavAutocompletevirtualdirectory.Name = "vAUTOCOMPLETEVIRTUALDIRECTORY";
         chkavAutocompletevirtualdirectory.WebTags = "";
         chkavAutocompletevirtualdirectory.Caption = context.GetMessage( "WWP_GAM_AutocompleteVirtualDirectory", "");
         AssignProp(sPrefix, false, chkavAutocompletevirtualdirectory_Internalname, "TitleCaption", chkavAutocompletevirtualdirectory.Caption, true);
         chkavAutocompletevirtualdirectory.CheckedValue = "false";
         chkavSiteurlcallbackiscustom.Name = "vSITEURLCALLBACKISCUSTOM";
         chkavSiteurlcallbackiscustom.WebTags = "";
         chkavSiteurlcallbackiscustom.Caption = " ";
         AssignProp(sPrefix, false, chkavSiteurlcallbackiscustom_Internalname, "TitleCaption", chkavSiteurlcallbackiscustom.Caption, true);
         chkavSiteurlcallbackiscustom.CheckedValue = "false";
         chkavCuautocompletevirtualdirectory.Name = "vCUAUTOCOMPLETEVIRTUALDIRECTORY";
         chkavCuautocompletevirtualdirectory.WebTags = "";
         chkavCuautocompletevirtualdirectory.Caption = context.GetMessage( "WWP_GAM_AutocompletecallbackURLwithvirtualdirectory", "");
         AssignProp(sPrefix, false, chkavCuautocompletevirtualdirectory_Internalname, "TitleCaption", chkavCuautocompletevirtualdirectory.Caption, true);
         chkavCuautocompletevirtualdirectory.CheckedValue = "false";
         chkavAdduseradditionaldatascope.Name = "vADDUSERADDITIONALDATASCOPE";
         chkavAdduseradditionaldatascope.WebTags = "";
         chkavAdduseradditionaldatascope.Caption = " ";
         AssignProp(sPrefix, false, chkavAdduseradditionaldatascope_Internalname, "TitleCaption", chkavAdduseradditionaldatascope.Caption, true);
         chkavAdduseradditionaldatascope.CheckedValue = "false";
         chkavAdduserdatascope.Name = "vADDUSERDATASCOPE";
         chkavAdduserdatascope.WebTags = "";
         chkavAdduserdatascope.Caption = " ";
         AssignProp(sPrefix, false, chkavAdduserdatascope_Internalname, "TitleCaption", chkavAdduserdatascope.Caption, true);
         chkavAdduserdatascope.CheckedValue = "false";
         chkavAddinitialpropertiesscope.Name = "vADDINITIALPROPERTIESSCOPE";
         chkavAddinitialpropertiesscope.WebTags = "";
         chkavAddinitialpropertiesscope.Caption = " ";
         AssignProp(sPrefix, false, chkavAddinitialpropertiesscope_Internalname, "TitleCaption", chkavAddinitialpropertiesscope.Caption, true);
         chkavAddinitialpropertiesscope.CheckedValue = "false";
         chkavAddapplicationdatascope.Name = "vADDAPPLICATIONDATASCOPE";
         chkavAddapplicationdatascope.WebTags = "";
         chkavAddapplicationdatascope.Caption = " ";
         AssignProp(sPrefix, false, chkavAddapplicationdatascope_Internalname, "TitleCaption", chkavAddapplicationdatascope.Caption, true);
         chkavAddapplicationdatascope.CheckedValue = "false";
         chkavAutovalidateexternaltokenandrefresh.Name = "vAUTOVALIDATEEXTERNALTOKENANDREFRESH";
         chkavAutovalidateexternaltokenandrefresh.WebTags = "";
         chkavAutovalidateexternaltokenandrefresh.Caption = " ";
         AssignProp(sPrefix, false, chkavAutovalidateexternaltokenandrefresh_Internalname, "TitleCaption", chkavAutovalidateexternaltokenandrefresh.Caption, true);
         chkavAutovalidateexternaltokenandrefresh.CheckedValue = "false";
         cmbavWsversion.Name = "vWSVERSION";
         cmbavWsversion.WebTags = "";
         cmbavWsversion.addItem("GAM10", context.GetMessage( "WWP_GAM_Version10", ""), 0);
         cmbavWsversion.addItem("GAM20", context.GetMessage( "WWP_GAM_Version20", ""), 0);
         if ( cmbavWsversion.ItemCount > 0 )
         {
         }
         cmbavWsserversecureprotocol.Name = "vWSSERVERSECUREPROTOCOL";
         cmbavWsserversecureprotocol.WebTags = "";
         cmbavWsserversecureprotocol.addItem("0", context.GetMessage( "WWP_GAM_No", ""), 0);
         cmbavWsserversecureprotocol.addItem("1", context.GetMessage( "WWP_GAM_Yes", ""), 0);
         if ( cmbavWsserversecureprotocol.ItemCount > 0 )
         {
         }
         cmbavCusversion.Name = "vCUSVERSION";
         cmbavCusversion.WebTags = "";
         cmbavCusversion.addItem("GAM10", context.GetMessage( "WWP_GAM_Version10", ""), 0);
         cmbavCusversion.addItem("GAM20", context.GetMessage( "WWP_GAM_Version20", ""), 0);
         if ( cmbavCusversion.ItemCount > 0 )
         {
         }
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavName_Internalname = sPrefix+"vNAME";
         cmbavFunctionid_Internalname = sPrefix+"vFUNCTIONID";
         chkavIsenable_Internalname = sPrefix+"vISENABLE";
         edtavDsc_Internalname = sPrefix+"vDSC";
         edtavSmallimagename_Internalname = sPrefix+"vSMALLIMAGENAME";
         edtavBigimagename_Internalname = sPrefix+"vBIGIMAGENAME";
         cmbavImpersonate_Internalname = sPrefix+"vIMPERSONATE";
         divImpersonate_cell_Internalname = sPrefix+"IMPERSONATE_CELL";
         chkavTfaenable_Internalname = sPrefix+"vTFAENABLE";
         divTfaenable_cell_Internalname = sPrefix+"TFAENABLE_CELL";
         cmbavTfaauthenticationtypename_Internalname = sPrefix+"vTFAAUTHENTICATIONTYPENAME";
         divTfaauthenticationtypename_cell_Internalname = sPrefix+"TFAAUTHENTICATIONTYPENAME_CELL";
         edtavTfafirstfactorauthenticationexpiration_Internalname = sPrefix+"vTFAFIRSTFACTORAUTHENTICATIONEXPIRATION";
         divTfafirstfactorauthenticationexpiration_cell_Internalname = sPrefix+"TFAFIRSTFACTORAUTHENTICATIONEXPIRATION_CELL";
         chkavTfaforceforallusers_Internalname = sPrefix+"vTFAFORCEFORALLUSERS";
         divTfaforceforallusers_cell_Internalname = sPrefix+"TFAFORCEFORALLUSERS_CELL";
         chkavOtpuseforfirstfactorauthentication_Internalname = sPrefix+"vOTPUSEFORFIRSTFACTORAUTHENTICATION";
         cmbavOtpeventvalidateuser_Internalname = sPrefix+"vOTPEVENTVALIDATEUSER";
         cmbavOtpgenerationtype_Internalname = sPrefix+"vOTPGENERATIONTYPE";
         cmbavOtpgenerationtype_customeventgeneratecode_Internalname = sPrefix+"vOTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE";
         divOtpgenerationtype_customeventgeneratecode_cell_Internalname = sPrefix+"OTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE_CELL";
         edtavOtpautogeneratedcodelength_Internalname = sPrefix+"vOTPAUTOGENERATEDCODELENGTH";
         divOtpautogeneratedcodelength_cell_Internalname = sPrefix+"OTPAUTOGENERATEDCODELENGTH_CELL";
         chkavOtpgeneratecodeonlynumbers_Internalname = sPrefix+"vOTPGENERATECODEONLYNUMBERS";
         divOtpgeneratecodeonlynumbers_cell_Internalname = sPrefix+"OTPGENERATECODEONLYNUMBERS_CELL";
         edtavOtpcodeexpirationtimeout_Internalname = sPrefix+"vOTPCODEEXPIRATIONTIMEOUT";
         edtavOtpmaximumdailynumbercodes_Internalname = sPrefix+"vOTPMAXIMUMDAILYNUMBERCODES";
         edtavOtpnumberunsuccessfulretriestolockotp_Internalname = sPrefix+"vOTPNUMBERUNSUCCESSFULRETRIESTOLOCKOTP";
         edtavOtpautounlocktime_Internalname = sPrefix+"vOTPAUTOUNLOCKTIME";
         edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname = sPrefix+"vOTPNUMBERUNSUCCESSFULRETRIESTOBLOCKUSERBASEDOFOTPLOCKS";
         cmbavOtpeventsendcode_Internalname = sPrefix+"vOTPEVENTSENDCODE";
         edtavOtpmailmessagesubject_Internalname = sPrefix+"vOTPMAILMESSAGESUBJECT";
         edtavOtpmailmessagebodyhtml_Internalname = sPrefix+"vOTPMAILMESSAGEBODYHTML";
         cmbavOtpeventvalidatecode_Internalname = sPrefix+"vOTPEVENTVALIDATECODE";
         divTblsendandvalidateotpcode_Internalname = sPrefix+"TBLSENDANDVALIDATEOTPCODE";
         divTblotpconfiguration_Internalname = sPrefix+"TBLOTPCONFIGURATION";
         divTblotpauthentication_Internalname = sPrefix+"TBLOTPAUTHENTICATION";
         edtavClientid_Internalname = sPrefix+"vCLIENTID";
         divClientid_cell_Internalname = sPrefix+"CLIENTID_CELL";
         edtavClientsecret_Internalname = sPrefix+"vCLIENTSECRET";
         divClientsecret_cell_Internalname = sPrefix+"CLIENTSECRET_CELL";
         edtavVersionpath_Internalname = sPrefix+"vVERSIONPATH";
         divVersionpath_cell_Internalname = sPrefix+"VERSIONPATH_CELL";
         edtavSiteurl_Internalname = sPrefix+"vSITEURL";
         divSiteurl_cell_Internalname = sPrefix+"SITEURL_CELL";
         chkavAutocompletevirtualdirectory_Internalname = sPrefix+"vAUTOCOMPLETEVIRTUALDIRECTORY";
         divAutocompletevirtualdirectory_cell_Internalname = sPrefix+"AUTOCOMPLETEVIRTUALDIRECTORY_CELL";
         chkavSiteurlcallbackiscustom_Internalname = sPrefix+"vSITEURLCALLBACKISCUSTOM";
         divSiteurlcallbackiscustom_cell_Internalname = sPrefix+"SITEURLCALLBACKISCUSTOM_CELL";
         edtavConsumerkey_Internalname = sPrefix+"vCONSUMERKEY";
         divConsumerkey_cell_Internalname = sPrefix+"CONSUMERKEY_CELL";
         edtavConsumersecret_Internalname = sPrefix+"vCONSUMERSECRET";
         divConsumersecret_cell_Internalname = sPrefix+"CONSUMERSECRET_CELL";
         edtavCallbackurl_Internalname = sPrefix+"vCALLBACKURL";
         divCallbackurl_cell_Internalname = sPrefix+"CALLBACKURL_CELL";
         chkavCuautocompletevirtualdirectory_Internalname = sPrefix+"vCUAUTOCOMPLETEVIRTUALDIRECTORY";
         divCuautocompletevirtualdirectory_cell_Internalname = sPrefix+"CUAUTOCOMPLETEVIRTUALDIRECTORY_CELL";
         lblTextblockrequestscopes_Internalname = sPrefix+"TEXTBLOCKREQUESTSCOPES";
         chkavAdduseradditionaldatascope_Internalname = sPrefix+"vADDUSERADDITIONALDATASCOPE";
         chkavAdduserdatascope_Internalname = sPrefix+"vADDUSERDATASCOPE";
         chkavAddinitialpropertiesscope_Internalname = sPrefix+"vADDINITIALPROPERTIESSCOPE";
         chkavAddapplicationdatascope_Internalname = sPrefix+"vADDAPPLICATIONDATASCOPE";
         divTblscopes_Internalname = sPrefix+"TBLSCOPES";
         edtavAdditionalscope_Internalname = sPrefix+"vADDITIONALSCOPE";
         divAdditionalscope_cell_Internalname = sPrefix+"ADDITIONALSCOPE_CELL";
         edtavGamrauthenticationtypename_Internalname = sPrefix+"vGAMRAUTHENTICATIONTYPENAME";
         divGamrauthenticationtypename_cell_Internalname = sPrefix+"GAMRAUTHENTICATIONTYPENAME_CELL";
         edtavGamrserverurl_Internalname = sPrefix+"vGAMRSERVERURL";
         divGamrserverurl_cell_Internalname = sPrefix+"GAMRSERVERURL_CELL";
         edtavGamrprivateencryptkey_Internalname = sPrefix+"vGAMRPRIVATEENCRYPTKEY";
         divGamrprivateencryptkey_cell_Internalname = sPrefix+"GAMRPRIVATEENCRYPTKEY_CELL";
         edtavGamrrepositoryguid_Internalname = sPrefix+"vGAMRREPOSITORYGUID";
         divGamrrepositoryguid_cell_Internalname = sPrefix+"GAMRREPOSITORYGUID_CELL";
         chkavAutovalidateexternaltokenandrefresh_Internalname = sPrefix+"vAUTOVALIDATEEXTERNALTOKENANDREFRESH";
         divTblserverhost_Internalname = sPrefix+"TBLSERVERHOST";
         cmbavWsversion_Internalname = sPrefix+"vWSVERSION";
         lblTextblockwsprivateencryptkey_Internalname = sPrefix+"TEXTBLOCKWSPRIVATEENCRYPTKEY";
         edtavWsprivateencryptkey_Internalname = sPrefix+"vWSPRIVATEENCRYPTKEY";
         bttBtngenkey_Internalname = sPrefix+"BTNGENKEY";
         tblTablemergedwsprivateencryptkey_Internalname = sPrefix+"TABLEMERGEDWSPRIVATEENCRYPTKEY";
         divTablesplittedwsprivateencryptkey_Internalname = sPrefix+"TABLESPLITTEDWSPRIVATEENCRYPTKEY";
         edtavWsservername_Internalname = sPrefix+"vWSSERVERNAME";
         divWsservername_cell_Internalname = sPrefix+"WSSERVERNAME_CELL";
         edtavWsserverport_Internalname = sPrefix+"vWSSERVERPORT";
         divWsserverport_cell_Internalname = sPrefix+"WSSERVERPORT_CELL";
         edtavWsserverbaseurl_Internalname = sPrefix+"vWSSERVERBASEURL";
         cmbavWsserversecureprotocol_Internalname = sPrefix+"vWSSERVERSECUREPROTOCOL";
         edtavWstimeout_Internalname = sPrefix+"vWSTIMEOUT";
         edtavWspackage_Internalname = sPrefix+"vWSPACKAGE";
         edtavWsname_Internalname = sPrefix+"vWSNAME";
         divWsname_cell_Internalname = sPrefix+"WSNAME_CELL";
         edtavWsextension_Internalname = sPrefix+"vWSEXTENSION";
         divTblwebservice_Internalname = sPrefix+"TBLWEBSERVICE";
         cmbavCusversion_Internalname = sPrefix+"vCUSVERSION";
         lblTextblockcusprivateencryptkey_Internalname = sPrefix+"TEXTBLOCKCUSPRIVATEENCRYPTKEY";
         edtavCusprivateencryptkey_Internalname = sPrefix+"vCUSPRIVATEENCRYPTKEY";
         bttBtngenkeycustom_Internalname = sPrefix+"BTNGENKEYCUSTOM";
         tblTablemergedcusprivateencryptkey_Internalname = sPrefix+"TABLEMERGEDCUSPRIVATEENCRYPTKEY";
         divTablesplittedcusprivateencryptkey_Internalname = sPrefix+"TABLESPLITTEDCUSPRIVATEENCRYPTKEY";
         edtavCusfilename_Internalname = sPrefix+"vCUSFILENAME";
         divCusfilename_cell_Internalname = sPrefix+"CUSFILENAME_CELL";
         edtavCuspackage_Internalname = sPrefix+"vCUSPACKAGE";
         edtavCusclassname_Internalname = sPrefix+"vCUSCLASSNAME";
         divCusclassname_cell_Internalname = sPrefix+"CUSCLASSNAME_CELL";
         edtavCusmethod_Internalname = sPrefix+"vCUSMETHOD";
         divTblexternal_Internalname = sPrefix+"TBLEXTERNAL";
         divTableattributes_Internalname = sPrefix+"TABLEATTRIBUTES";
         divTablecontent_Internalname = sPrefix+"TABLECONTENT";
         bttBtnenter_Internalname = sPrefix+"BTNENTER";
         bttBtncancel_Internalname = sPrefix+"BTNCANCEL";
         divTablemain_Internalname = sPrefix+"TABLEMAIN";
         divLayoutmaintable_Internalname = sPrefix+"LAYOUTMAINTABLE";
         Form.Internalname = sPrefix+"FORM";
      }

      public override void initialize_properties( )
      {
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            context.SetDefaultTheme("WorkWithPlusDS", true);
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
         }
         init_default_properties( ) ;
         chkavAutovalidateexternaltokenandrefresh.Caption = " ";
         chkavAddapplicationdatascope.Caption = " ";
         chkavAddinitialpropertiesscope.Caption = " ";
         chkavAdduserdatascope.Caption = " ";
         chkavAdduseradditionaldatascope.Caption = " ";
         chkavCuautocompletevirtualdirectory.Caption = context.GetMessage( "WWP_GAM_AutocompletecallbackURLwithvirtualdirectory", "");
         chkavSiteurlcallbackiscustom.Caption = " ";
         chkavAutocompletevirtualdirectory.Caption = context.GetMessage( "WWP_GAM_AutocompleteVirtualDirectory", "");
         chkavOtpgeneratecodeonlynumbers.Caption = " ";
         chkavOtpuseforfirstfactorauthentication.Caption = " ";
         chkavTfaforceforallusers.Caption = " ";
         chkavTfaenable.Caption = " ";
         chkavIsenable.Caption = " ";
         bttBtngenkey_Visible = 1;
         edtavWsprivateencryptkey_Jsonclick = "";
         bttBtngenkeycustom_Visible = 1;
         edtavCusprivateencryptkey_Jsonclick = "";
         edtavCusprivateencryptkey_Enabled = 1;
         edtavWsprivateencryptkey_Enabled = 1;
         bttBtnenter_Caption = context.GetMessage( "GX_BtnEnter", "");
         bttBtnenter_Visible = 1;
         edtavCusmethod_Jsonclick = "";
         edtavCusmethod_Enabled = 1;
         edtavCusclassname_Jsonclick = "";
         edtavCusclassname_Enabled = 1;
         divCusclassname_cell_Class = "col-xs-12 col-sm-6";
         edtavCuspackage_Jsonclick = "";
         edtavCuspackage_Enabled = 1;
         edtavCusfilename_Jsonclick = "";
         edtavCusfilename_Enabled = 1;
         divCusfilename_cell_Class = "col-xs-12 col-sm-6";
         cmbavCusversion_Jsonclick = "";
         cmbavCusversion.Enabled = 1;
         divTblexternal_Visible = 1;
         edtavWsextension_Jsonclick = "";
         edtavWsextension_Enabled = 1;
         edtavWsname_Jsonclick = "";
         edtavWsname_Enabled = 1;
         divWsname_cell_Class = "col-xs-12 col-sm-6";
         edtavWspackage_Jsonclick = "";
         edtavWspackage_Enabled = 1;
         edtavWstimeout_Jsonclick = "";
         edtavWstimeout_Enabled = 1;
         cmbavWsserversecureprotocol_Jsonclick = "";
         cmbavWsserversecureprotocol.Enabled = 1;
         edtavWsserverbaseurl_Jsonclick = "";
         edtavWsserverbaseurl_Enabled = 1;
         edtavWsserverport_Jsonclick = "";
         edtavWsserverport_Enabled = 1;
         divWsserverport_cell_Class = "col-xs-12 col-sm-6";
         edtavWsservername_Jsonclick = "";
         edtavWsservername_Enabled = 1;
         divWsservername_cell_Class = "col-xs-12 col-sm-6";
         cmbavWsversion_Jsonclick = "";
         cmbavWsversion.Enabled = 1;
         divTblwebservice_Visible = 1;
         chkavAutovalidateexternaltokenandrefresh.Enabled = 1;
         edtavGamrrepositoryguid_Jsonclick = "";
         edtavGamrrepositoryguid_Enabled = 1;
         edtavGamrrepositoryguid_Visible = 1;
         divGamrrepositoryguid_cell_Class = "col-xs-12 col-sm-6";
         edtavGamrprivateencryptkey_Jsonclick = "";
         edtavGamrprivateencryptkey_Enabled = 1;
         edtavGamrprivateencryptkey_Visible = 1;
         divGamrprivateencryptkey_cell_Class = "col-xs-12 col-sm-6";
         edtavGamrserverurl_Jsonclick = "";
         edtavGamrserverurl_Enabled = 1;
         divGamrserverurl_cell_Class = "col-xs-12 col-sm-6";
         divTblserverhost_Visible = 1;
         edtavGamrauthenticationtypename_Jsonclick = "";
         edtavGamrauthenticationtypename_Enabled = 1;
         edtavGamrauthenticationtypename_Visible = 1;
         divGamrauthenticationtypename_cell_Class = "col-xs-12 col-sm-6";
         edtavAdditionalscope_Jsonclick = "";
         edtavAdditionalscope_Enabled = 1;
         edtavAdditionalscope_Visible = 1;
         divAdditionalscope_cell_Class = "col-xs-12 col-sm-6";
         chkavAddapplicationdatascope.Enabled = 1;
         chkavAddinitialpropertiesscope.Enabled = 1;
         chkavAdduserdatascope.Enabled = 1;
         chkavAdduseradditionaldatascope.Enabled = 1;
         divTblscopes_Visible = 1;
         chkavCuautocompletevirtualdirectory.TooltipText = "";
         chkavCuautocompletevirtualdirectory.Enabled = 1;
         chkavCuautocompletevirtualdirectory.Visible = 1;
         divCuautocompletevirtualdirectory_cell_Class = "col-xs-12 col-sm-6";
         edtavCallbackurl_Jsonclick = "";
         edtavCallbackurl_Enabled = 1;
         edtavCallbackurl_Visible = 1;
         divCallbackurl_cell_Class = "col-xs-12 col-sm-6";
         edtavConsumersecret_Jsonclick = "";
         edtavConsumersecret_Enabled = 1;
         edtavConsumersecret_Visible = 1;
         divConsumersecret_cell_Class = "col-xs-12 col-sm-6";
         edtavConsumerkey_Jsonclick = "";
         edtavConsumerkey_Enabled = 1;
         edtavConsumerkey_Visible = 1;
         divConsumerkey_cell_Class = "col-xs-12 col-sm-6";
         chkavSiteurlcallbackiscustom.Enabled = 1;
         chkavSiteurlcallbackiscustom.Visible = 1;
         divSiteurlcallbackiscustom_cell_Class = "col-xs-12 col-sm-6";
         chkavAutocompletevirtualdirectory.TooltipText = "";
         chkavAutocompletevirtualdirectory.Enabled = 1;
         chkavAutocompletevirtualdirectory.Visible = 1;
         divAutocompletevirtualdirectory_cell_Class = "col-xs-12 col-sm-6";
         edtavSiteurl_Jsonclick = "";
         edtavSiteurl_Enabled = 1;
         edtavSiteurl_Visible = 1;
         divSiteurl_cell_Class = "col-xs-12 col-sm-6";
         edtavVersionpath_Jsonclick = "";
         edtavVersionpath_Enabled = 1;
         edtavVersionpath_Visible = 1;
         divVersionpath_cell_Class = "col-xs-12 col-sm-6";
         edtavClientsecret_Jsonclick = "";
         edtavClientsecret_Enabled = 1;
         edtavClientsecret_Visible = 1;
         divClientsecret_cell_Class = "col-xs-12 col-sm-6";
         edtavClientid_Jsonclick = "";
         edtavClientid_Enabled = 1;
         edtavClientid_Visible = 1;
         divClientid_cell_Class = "col-xs-12 col-sm-6";
         cmbavOtpeventvalidatecode_Jsonclick = "";
         cmbavOtpeventvalidatecode.Enabled = 1;
         edtavOtpmailmessagebodyhtml_Enabled = 1;
         edtavOtpmailmessagesubject_Jsonclick = "";
         edtavOtpmailmessagesubject_Enabled = 1;
         cmbavOtpeventsendcode_Jsonclick = "";
         cmbavOtpeventsendcode.Enabled = 1;
         divTblsendandvalidateotpcode_Visible = 1;
         edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Jsonclick = "";
         edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Enabled = 1;
         edtavOtpautounlocktime_Jsonclick = "";
         edtavOtpautounlocktime_Enabled = 1;
         edtavOtpnumberunsuccessfulretriestolockotp_Jsonclick = "";
         edtavOtpnumberunsuccessfulretriestolockotp_Enabled = 1;
         edtavOtpmaximumdailynumbercodes_Jsonclick = "";
         edtavOtpmaximumdailynumbercodes_Enabled = 1;
         edtavOtpcodeexpirationtimeout_Jsonclick = "";
         edtavOtpcodeexpirationtimeout_Enabled = 1;
         chkavOtpgeneratecodeonlynumbers.Enabled = 1;
         chkavOtpgeneratecodeonlynumbers.Visible = 1;
         divOtpgeneratecodeonlynumbers_cell_Class = "col-xs-12 col-sm-6";
         edtavOtpautogeneratedcodelength_Jsonclick = "";
         edtavOtpautogeneratedcodelength_Enabled = 1;
         edtavOtpautogeneratedcodelength_Visible = 1;
         divOtpautogeneratedcodelength_cell_Class = "col-xs-12 col-sm-6";
         cmbavOtpgenerationtype_customeventgeneratecode_Jsonclick = "";
         cmbavOtpgenerationtype_customeventgeneratecode.Enabled = 1;
         cmbavOtpgenerationtype_customeventgeneratecode.Visible = 1;
         divOtpgenerationtype_customeventgeneratecode_cell_Class = "col-xs-12 col-sm-6";
         cmbavOtpgenerationtype_Jsonclick = "";
         cmbavOtpgenerationtype.Enabled = 1;
         cmbavOtpeventvalidateuser_Jsonclick = "";
         cmbavOtpeventvalidateuser.Enabled = 1;
         divTblotpconfiguration_Visible = 1;
         chkavOtpuseforfirstfactorauthentication.Enabled = 1;
         divTblotpauthentication_Visible = 1;
         chkavTfaforceforallusers.Enabled = 1;
         chkavTfaforceforallusers.Visible = 1;
         divTfaforceforallusers_cell_Class = "col-xs-12 col-sm-6";
         edtavTfafirstfactorauthenticationexpiration_Jsonclick = "";
         edtavTfafirstfactorauthenticationexpiration_Enabled = 1;
         edtavTfafirstfactorauthenticationexpiration_Visible = 1;
         divTfafirstfactorauthenticationexpiration_cell_Class = "col-xs-12 col-sm-6";
         cmbavTfaauthenticationtypename_Jsonclick = "";
         cmbavTfaauthenticationtypename.Enabled = 1;
         cmbavTfaauthenticationtypename.Visible = 1;
         divTfaauthenticationtypename_cell_Class = "col-xs-12 col-sm-6";
         chkavTfaenable.Enabled = 1;
         chkavTfaenable.Visible = 1;
         divTfaenable_cell_Class = "col-xs-12 col-sm-6";
         cmbavImpersonate_Jsonclick = "";
         cmbavImpersonate.Enabled = 1;
         cmbavImpersonate.Visible = 1;
         divImpersonate_cell_Class = "col-xs-12 col-sm-6";
         edtavBigimagename_Jsonclick = "";
         edtavBigimagename_Enabled = 1;
         edtavSmallimagename_Jsonclick = "";
         edtavSmallimagename_Enabled = 1;
         edtavDsc_Jsonclick = "";
         edtavDsc_Enabled = 1;
         chkavIsenable.Enabled = 1;
         cmbavFunctionid_Jsonclick = "";
         cmbavFunctionid.Enabled = 1;
         edtavName_Jsonclick = "";
         edtavName_Enabled = 0;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         context.GX_msglist.DisplayMode = 1;
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV45IsEnable","fld":"vISENABLE"},{"av":"AV66TFAEnable","fld":"vTFAENABLE"},{"av":"AV68TFAForceForAllUsers","fld":"vTFAFORCEFORALLUSERS"},{"av":"AV61OTPUseForFirstFactorAuthentication","fld":"vOTPUSEFORFIRSTFACTORAUTHENTICATION"},{"av":"AV53OTPGenerateCodeOnlyNumbers","fld":"vOTPGENERATECODEONLYNUMBERS"},{"av":"AV85AutocompleteVirtualDirectory","fld":"vAUTOCOMPLETEVIRTUALDIRECTORY"},{"av":"AV63SiteURLCallbackIsCustom","fld":"vSITEURLCALLBACKISCUSTOM"},{"av":"AV86CUAutocompleteVirtualDirectory","fld":"vCUAUTOCOMPLETEVIRTUALDIRECTORY"},{"av":"AV7AddUserAdditionalDataScope","fld":"vADDUSERADDITIONALDATASCOPE"},{"av":"AV88AddUserDataScope","fld":"vADDUSERDATASCOPE"},{"av":"AV5AddInitialPropertiesScope","fld":"vADDINITIALPROPERTIESSCOPE"},{"av":"AV89AddApplicationDataScope","fld":"vADDAPPLICATIONDATASCOPE"},{"av":"AV21AutovalidateExternalTokenAndRefresh","fld":"vAUTOVALIDATEEXTERNALTOKENANDREFRESH"}]}""");
         setEventMetadata("'DOGENKEYCUSTOM'","""{"handler":"E121I2","iparms":[]""");
         setEventMetadata("'DOGENKEYCUSTOM'",""","oparms":[{"av":"AV31CusPrivateEncryptKey","fld":"vCUSPRIVATEENCRYPTKEY"}]}""");
         setEventMetadata("'DOGENKEY'","""{"handler":"E131I2","iparms":[]""");
         setEventMetadata("'DOGENKEY'",""","oparms":[{"av":"AV74WSPrivateEncryptKey","fld":"vWSPRIVATEENCRYPTKEY"}]}""");
         setEventMetadata("ENTER","""{"handler":"E141I2","iparms":[{"av":"AV83CheckRequiredFieldsResult","fld":"vCHECKREQUIREDFIELDSRESULT"},{"av":"Gx_mode","fld":"vMODE","pic":"@!"},{"av":"AV87CusMethod","fld":"vCUSMETHOD"},{"av":"AV28CusClassName","fld":"vCUSCLASSNAME"},{"av":"AV30CusPackage","fld":"vCUSPACKAGE"},{"av":"AV29CusFileName","fld":"vCUSFILENAME"},{"av":"AV31CusPrivateEncryptKey","fld":"vCUSPRIVATEENCRYPTKEY"},{"av":"cmbavCusversion"},{"av":"AV32CusVersion","fld":"vCUSVERSION"},{"av":"cmbavWsserversecureprotocol"},{"av":"AV78WSServerSecureProtocol","fld":"vWSSERVERSECUREPROTOCOL","pic":"9"},{"av":"AV75WSServerBaseURL","fld":"vWSSERVERBASEURL"},{"av":"AV77WSServerPort","fld":"vWSSERVERPORT","pic":"ZZZZ9"},{"av":"AV76WSServerName","fld":"vWSSERVERNAME"},{"av":"AV71WSExtension","fld":"vWSEXTENSION"},{"av":"AV72WSName","fld":"vWSNAME"},{"av":"AV73WSPackage","fld":"vWSPACKAGE"},{"av":"AV79WSTimeout","fld":"vWSTIMEOUT","pic":"ZZZZ9"},{"av":"AV74WSPrivateEncryptKey","fld":"vWSPRIVATEENCRYPTKEY"},{"av":"cmbavWsversion"},{"av":"AV80WSVersion","fld":"vWSVERSION"},{"av":"AV63SiteURLCallbackIsCustom","fld":"vSITEURLCALLBACKISCUSTOM"},{"av":"AV21AutovalidateExternalTokenAndRefresh","fld":"vAUTOVALIDATEEXTERNALTOKENANDREFRESH"},{"av":"AV42GAMRRepositoryGUID","fld":"vGAMRREPOSITORYGUID"},{"av":"AV41GAMRPrivateEncryptKey","fld":"vGAMRPRIVATEENCRYPTKEY"},{"av":"AV43GAMRServerURL","fld":"vGAMRSERVERURL"},{"av":"AV40GAMRAuthenticationTypeName","fld":"vGAMRAUTHENTICATIONTYPENAME"},{"av":"AV5AddInitialPropertiesScope","fld":"vADDINITIALPROPERTIESSCOPE"},{"av":"AV89AddApplicationDataScope","fld":"vADDAPPLICATIONDATASCOPE"},{"av":"AV7AddUserAdditionalDataScope","fld":"vADDUSERADDITIONALDATASCOPE"},{"av":"AV88AddUserDataScope","fld":"vADDUSERDATASCOPE"},{"av":"AV68TFAForceForAllUsers","fld":"vTFAFORCEFORALLUSERS"},{"av":"AV67TFAFirstFactorAuthenticationExpiration","fld":"vTFAFIRSTFACTORAUTHENTICATIONEXPIRATION","pic":"ZZZZZZZZ9"},{"av":"cmbavTfaauthenticationtypename"},{"av":"AV65TFAAuthenticationTypeName","fld":"vTFAAUTHENTICATIONTYPENAME"},{"av":"AV66TFAEnable","fld":"vTFAENABLE"},{"av":"cmbavFunctionid"},{"av":"AV37FunctionId","fld":"vFUNCTIONID"},{"av":"cmbavOtpeventvalidatecode"},{"av":"AV51OTPEventValidateCode","fld":"vOTPEVENTVALIDATECODE"},{"av":"AV56OTPMailMessageBodyHTML","fld":"vOTPMAILMESSAGEBODYHTML"},{"av":"AV57OTPMailMessageSubject","fld":"vOTPMAILMESSAGESUBJECT"},{"av":"cmbavOtpeventsendcode"},{"av":"AV50OTPEventSendCode","fld":"vOTPEVENTSENDCODE"},{"av":"AV48OTPAutoUnlockTime","fld":"vOTPAUTOUNLOCKTIME","pic":"ZZZZZZZZ9"},{"av":"AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks","fld":"vOTPNUMBERUNSUCCESSFULRETRIESTOBLOCKUSERBASEDOFOTPLOCKS","pic":"ZZZ9"},{"av":"AV60OTPNumberUnsuccessfulRetriesToLockOTP","fld":"vOTPNUMBERUNSUCCESSFULRETRIESTOLOCKOTP","pic":"ZZZ9"},{"av":"AV53OTPGenerateCodeOnlyNumbers","fld":"vOTPGENERATECODEONLYNUMBERS"},{"av":"AV47OTPAutogeneratedCodeLength","fld":"vOTPAUTOGENERATEDCODELENGTH","pic":"ZZZ9"},{"av":"AV58OTPMaximumDailyNumberCodes","fld":"vOTPMAXIMUMDAILYNUMBERCODES","pic":"ZZZ9"},{"av":"AV49OTPCodeExpirationTimeout","fld":"vOTPCODEEXPIRATIONTIMEOUT","pic":"ZZZZZZZZ9"},{"av":"cmbavOtpgenerationtype_customeventgeneratecode"},{"av":"AV55OTPGenerationType_CustomEventGenerateCode","fld":"vOTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE"},{"av":"cmbavOtpgenerationtype"},{"av":"AV54OTPGenerationType","fld":"vOTPGENERATIONTYPE"},{"av":"cmbavOtpeventvalidateuser"},{"av":"AV52OTPEventValidateUser","fld":"vOTPEVENTVALIDATEUSER"},{"av":"AV61OTPUseForFirstFactorAuthentication","fld":"vOTPUSEFORFIRSTFACTORAUTHENTICATION"},{"av":"AV86CUAutocompleteVirtualDirectory","fld":"vCUAUTOCOMPLETEVIRTUALDIRECTORY"},{"av":"AV23CallbackURL","fld":"vCALLBACKURL"},{"av":"AV27ConsumerSecret","fld":"vCONSUMERSECRET"},{"av":"AV26ConsumerKey","fld":"vCONSUMERKEY"},{"av":"AV6AdditionalScope","fld":"vADDITIONALSCOPE"},{"av":"AV85AutocompleteVirtualDirectory","fld":"vAUTOCOMPLETEVIRTUALDIRECTORY"},{"av":"AV62SiteURL","fld":"vSITEURL"},{"av":"AV70VersionPath","fld":"vVERSIONPATH"},{"av":"AV25ClientSecret","fld":"vCLIENTSECRET"},{"av":"AV24ClientId","fld":"vCLIENTID"},{"av":"cmbavImpersonate"},{"av":"AV44Impersonate","fld":"vIMPERSONATE"},{"av":"AV22BigImageName","fld":"vBIGIMAGENAME"},{"av":"AV64SmallImageName","fld":"vSMALLIMAGENAME"},{"av":"AV33Dsc","fld":"vDSC"},{"av":"AV45IsEnable","fld":"vISENABLE"},{"av":"AV46Name","fld":"vNAME"},{"av":"AV69TypeId","fld":"vTYPEID"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV83CheckRequiredFieldsResult","fld":"vCHECKREQUIREDFIELDSRESULT"}]}""");
         setEventMetadata("VTFAENABLE.CLICK","""{"handler":"E151I2","iparms":[{"av":"AV66TFAEnable","fld":"vTFAENABLE"},{"av":"AV67TFAFirstFactorAuthenticationExpiration","fld":"vTFAFIRSTFACTORAUTHENTICATIONEXPIRATION","pic":"ZZZZZZZZ9"},{"av":"cmbavTfaauthenticationtypename"},{"av":"AV65TFAAuthenticationTypeName","fld":"vTFAAUTHENTICATIONTYPENAME"},{"av":"AV69TypeId","fld":"vTYPEID"},{"av":"cmbavOtpgenerationtype"},{"av":"AV54OTPGenerationType","fld":"vOTPGENERATIONTYPE"}]""");
         setEventMetadata("VTFAENABLE.CLICK",""","oparms":[{"av":"AV67TFAFirstFactorAuthenticationExpiration","fld":"vTFAFIRSTFACTORAUTHENTICATIONEXPIRATION","pic":"ZZZZZZZZ9"},{"av":"cmbavTfaauthenticationtypename"},{"av":"AV65TFAAuthenticationTypeName","fld":"vTFAAUTHENTICATIONTYPENAME"},{"av":"cmbavImpersonate"},{"av":"divImpersonate_cell_Class","ctrl":"IMPERSONATE_CELL","prop":"Class"},{"av":"chkavTfaenable.Visible","ctrl":"vTFAENABLE","prop":"Visible"},{"av":"divTfaenable_cell_Class","ctrl":"TFAENABLE_CELL","prop":"Class"},{"av":"divTfaauthenticationtypename_cell_Class","ctrl":"TFAAUTHENTICATIONTYPENAME_CELL","prop":"Class"},{"av":"edtavTfafirstfactorauthenticationexpiration_Visible","ctrl":"vTFAFIRSTFACTORAUTHENTICATIONEXPIRATION","prop":"Visible"},{"av":"divTfafirstfactorauthenticationexpiration_cell_Class","ctrl":"TFAFIRSTFACTORAUTHENTICATIONEXPIRATION_CELL","prop":"Class"},{"av":"chkavTfaforceforallusers.Visible","ctrl":"vTFAFORCEFORALLUSERS","prop":"Visible"},{"av":"divTfaforceforallusers_cell_Class","ctrl":"TFAFORCEFORALLUSERS_CELL","prop":"Class"},{"av":"edtavClientid_Visible","ctrl":"vCLIENTID","prop":"Visible"},{"av":"divClientid_cell_Class","ctrl":"CLIENTID_CELL","prop":"Class"},{"av":"edtavClientsecret_Visible","ctrl":"vCLIENTSECRET","prop":"Visible"},{"av":"divClientsecret_cell_Class","ctrl":"CLIENTSECRET_CELL","prop":"Class"},{"av":"edtavVersionpath_Visible","ctrl":"vVERSIONPATH","prop":"Visible"},{"av":"divVersionpath_cell_Class","ctrl":"VERSIONPATH_CELL","prop":"Class"},{"av":"edtavSiteurl_Visible","ctrl":"vSITEURL","prop":"Visible"},{"av":"divSiteurl_cell_Class","ctrl":"SITEURL_CELL","prop":"Class"},{"av":"chkavAutocompletevirtualdirectory.Visible","ctrl":"vAUTOCOMPLETEVIRTUALDIRECTORY","prop":"Visible"},{"av":"divAutocompletevirtualdirectory_cell_Class","ctrl":"AUTOCOMPLETEVIRTUALDIRECTORY_CELL","prop":"Class"},{"av":"chkavSiteurlcallbackiscustom.Visible","ctrl":"vSITEURLCALLBACKISCUSTOM","prop":"Visible"},{"av":"divSiteurlcallbackiscustom_cell_Class","ctrl":"SITEURLCALLBACKISCUSTOM_CELL","prop":"Class"},{"av":"edtavConsumerkey_Visible","ctrl":"vCONSUMERKEY","prop":"Visible"},{"av":"divConsumerkey_cell_Class","ctrl":"CONSUMERKEY_CELL","prop":"Class"},{"av":"edtavConsumersecret_Visible","ctrl":"vCONSUMERSECRET","prop":"Visible"},{"av":"divConsumersecret_cell_Class","ctrl":"CONSUMERSECRET_CELL","prop":"Class"},{"av":"edtavCallbackurl_Visible","ctrl":"vCALLBACKURL","prop":"Visible"},{"av":"divCallbackurl_cell_Class","ctrl":"CALLBACKURL_CELL","prop":"Class"},{"av":"chkavCuautocompletevirtualdirectory.Visible","ctrl":"vCUAUTOCOMPLETEVIRTUALDIRECTORY","prop":"Visible"},{"av":"divCuautocompletevirtualdirectory_cell_Class","ctrl":"CUAUTOCOMPLETEVIRTUALDIRECTORY_CELL","prop":"Class"},{"av":"edtavAdditionalscope_Visible","ctrl":"vADDITIONALSCOPE","prop":"Visible"},{"av":"divAdditionalscope_cell_Class","ctrl":"ADDITIONALSCOPE_CELL","prop":"Class"},{"av":"edtavGamrauthenticationtypename_Visible","ctrl":"vGAMRAUTHENTICATIONTYPENAME","prop":"Visible"},{"av":"divGamrauthenticationtypename_cell_Class","ctrl":"GAMRAUTHENTICATIONTYPENAME_CELL","prop":"Class"},{"av":"divCusfilename_cell_Class","ctrl":"CUSFILENAME_CELL","prop":"Class"},{"av":"divCusclassname_cell_Class","ctrl":"CUSCLASSNAME_CELL","prop":"Class"},{"av":"divWsservername_cell_Class","ctrl":"WSSERVERNAME_CELL","prop":"Class"},{"av":"divWsserverport_cell_Class","ctrl":"WSSERVERPORT_CELL","prop":"Class"},{"av":"divWsname_cell_Class","ctrl":"WSNAME_CELL","prop":"Class"},{"av":"divGamrserverurl_cell_Class","ctrl":"GAMRSERVERURL_CELL","prop":"Class"},{"av":"edtavGamrprivateencryptkey_Visible","ctrl":"vGAMRPRIVATEENCRYPTKEY","prop":"Visible"},{"av":"divGamrprivateencryptkey_cell_Class","ctrl":"GAMRPRIVATEENCRYPTKEY_CELL","prop":"Class"},{"av":"edtavGamrrepositoryguid_Visible","ctrl":"vGAMRREPOSITORYGUID","prop":"Visible"},{"av":"divGamrrepositoryguid_cell_Class","ctrl":"GAMRREPOSITORYGUID_CELL","prop":"Class"},{"av":"cmbavOtpgenerationtype_customeventgeneratecode"},{"av":"divOtpgenerationtype_customeventgeneratecode_cell_Class","ctrl":"OTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE_CELL","prop":"Class"},{"av":"edtavOtpautogeneratedcodelength_Visible","ctrl":"vOTPAUTOGENERATEDCODELENGTH","prop":"Visible"},{"av":"divOtpautogeneratedcodelength_cell_Class","ctrl":"OTPAUTOGENERATEDCODELENGTH_CELL","prop":"Class"},{"av":"chkavOtpgeneratecodeonlynumbers.Visible","ctrl":"vOTPGENERATECODEONLYNUMBERS","prop":"Visible"},{"av":"divOtpgeneratecodeonlynumbers_cell_Class","ctrl":"OTPGENERATECODEONLYNUMBERS_CELL","prop":"Class"},{"av":"divTblsendandvalidateotpcode_Visible","ctrl":"TBLSENDANDVALIDATEOTPCODE","prop":"Visible"}]}""");
         setEventMetadata("VOTPGENERATIONTYPE.CLICK","""{"handler":"E161I2","iparms":[{"av":"cmbavOtpgenerationtype"},{"av":"AV54OTPGenerationType","fld":"vOTPGENERATIONTYPE"},{"av":"cmbavOtpgenerationtype_customeventgeneratecode"},{"av":"AV55OTPGenerationType_CustomEventGenerateCode","fld":"vOTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE"},{"av":"AV69TypeId","fld":"vTYPEID"},{"av":"AV66TFAEnable","fld":"vTFAENABLE"}]""");
         setEventMetadata("VOTPGENERATIONTYPE.CLICK",""","oparms":[{"av":"cmbavOtpgenerationtype_customeventgeneratecode"},{"av":"AV55OTPGenerationType_CustomEventGenerateCode","fld":"vOTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE"},{"av":"cmbavImpersonate"},{"av":"divImpersonate_cell_Class","ctrl":"IMPERSONATE_CELL","prop":"Class"},{"av":"chkavTfaenable.Visible","ctrl":"vTFAENABLE","prop":"Visible"},{"av":"divTfaenable_cell_Class","ctrl":"TFAENABLE_CELL","prop":"Class"},{"av":"cmbavTfaauthenticationtypename"},{"av":"divTfaauthenticationtypename_cell_Class","ctrl":"TFAAUTHENTICATIONTYPENAME_CELL","prop":"Class"},{"av":"edtavTfafirstfactorauthenticationexpiration_Visible","ctrl":"vTFAFIRSTFACTORAUTHENTICATIONEXPIRATION","prop":"Visible"},{"av":"divTfafirstfactorauthenticationexpiration_cell_Class","ctrl":"TFAFIRSTFACTORAUTHENTICATIONEXPIRATION_CELL","prop":"Class"},{"av":"chkavTfaforceforallusers.Visible","ctrl":"vTFAFORCEFORALLUSERS","prop":"Visible"},{"av":"divTfaforceforallusers_cell_Class","ctrl":"TFAFORCEFORALLUSERS_CELL","prop":"Class"},{"av":"edtavClientid_Visible","ctrl":"vCLIENTID","prop":"Visible"},{"av":"divClientid_cell_Class","ctrl":"CLIENTID_CELL","prop":"Class"},{"av":"edtavClientsecret_Visible","ctrl":"vCLIENTSECRET","prop":"Visible"},{"av":"divClientsecret_cell_Class","ctrl":"CLIENTSECRET_CELL","prop":"Class"},{"av":"edtavVersionpath_Visible","ctrl":"vVERSIONPATH","prop":"Visible"},{"av":"divVersionpath_cell_Class","ctrl":"VERSIONPATH_CELL","prop":"Class"},{"av":"edtavSiteurl_Visible","ctrl":"vSITEURL","prop":"Visible"},{"av":"divSiteurl_cell_Class","ctrl":"SITEURL_CELL","prop":"Class"},{"av":"chkavAutocompletevirtualdirectory.Visible","ctrl":"vAUTOCOMPLETEVIRTUALDIRECTORY","prop":"Visible"},{"av":"divAutocompletevirtualdirectory_cell_Class","ctrl":"AUTOCOMPLETEVIRTUALDIRECTORY_CELL","prop":"Class"},{"av":"chkavSiteurlcallbackiscustom.Visible","ctrl":"vSITEURLCALLBACKISCUSTOM","prop":"Visible"},{"av":"divSiteurlcallbackiscustom_cell_Class","ctrl":"SITEURLCALLBACKISCUSTOM_CELL","prop":"Class"},{"av":"edtavConsumerkey_Visible","ctrl":"vCONSUMERKEY","prop":"Visible"},{"av":"divConsumerkey_cell_Class","ctrl":"CONSUMERKEY_CELL","prop":"Class"},{"av":"edtavConsumersecret_Visible","ctrl":"vCONSUMERSECRET","prop":"Visible"},{"av":"divConsumersecret_cell_Class","ctrl":"CONSUMERSECRET_CELL","prop":"Class"},{"av":"edtavCallbackurl_Visible","ctrl":"vCALLBACKURL","prop":"Visible"},{"av":"divCallbackurl_cell_Class","ctrl":"CALLBACKURL_CELL","prop":"Class"},{"av":"chkavCuautocompletevirtualdirectory.Visible","ctrl":"vCUAUTOCOMPLETEVIRTUALDIRECTORY","prop":"Visible"},{"av":"divCuautocompletevirtualdirectory_cell_Class","ctrl":"CUAUTOCOMPLETEVIRTUALDIRECTORY_CELL","prop":"Class"},{"av":"edtavAdditionalscope_Visible","ctrl":"vADDITIONALSCOPE","prop":"Visible"},{"av":"divAdditionalscope_cell_Class","ctrl":"ADDITIONALSCOPE_CELL","prop":"Class"},{"av":"edtavGamrauthenticationtypename_Visible","ctrl":"vGAMRAUTHENTICATIONTYPENAME","prop":"Visible"},{"av":"divGamrauthenticationtypename_cell_Class","ctrl":"GAMRAUTHENTICATIONTYPENAME_CELL","prop":"Class"},{"av":"divCusfilename_cell_Class","ctrl":"CUSFILENAME_CELL","prop":"Class"},{"av":"divCusclassname_cell_Class","ctrl":"CUSCLASSNAME_CELL","prop":"Class"},{"av":"divWsservername_cell_Class","ctrl":"WSSERVERNAME_CELL","prop":"Class"},{"av":"divWsserverport_cell_Class","ctrl":"WSSERVERPORT_CELL","prop":"Class"},{"av":"divWsname_cell_Class","ctrl":"WSNAME_CELL","prop":"Class"},{"av":"divGamrserverurl_cell_Class","ctrl":"GAMRSERVERURL_CELL","prop":"Class"},{"av":"edtavGamrprivateencryptkey_Visible","ctrl":"vGAMRPRIVATEENCRYPTKEY","prop":"Visible"},{"av":"divGamrprivateencryptkey_cell_Class","ctrl":"GAMRPRIVATEENCRYPTKEY_CELL","prop":"Class"},{"av":"edtavGamrrepositoryguid_Visible","ctrl":"vGAMRREPOSITORYGUID","prop":"Visible"},{"av":"divGamrrepositoryguid_cell_Class","ctrl":"GAMRREPOSITORYGUID_CELL","prop":"Class"},{"av":"divOtpgenerationtype_customeventgeneratecode_cell_Class","ctrl":"OTPGENERATIONTYPE_CUSTOMEVENTGENERATECODE_CELL","prop":"Class"},{"av":"edtavOtpautogeneratedcodelength_Visible","ctrl":"vOTPAUTOGENERATEDCODELENGTH","prop":"Visible"},{"av":"divOtpautogeneratedcodelength_cell_Class","ctrl":"OTPAUTOGENERATEDCODELENGTH_CELL","prop":"Class"},{"av":"chkavOtpgeneratecodeonlynumbers.Visible","ctrl":"vOTPGENERATECODEONLYNUMBERS","prop":"Visible"},{"av":"divOtpgeneratecodeonlynumbers_cell_Class","ctrl":"OTPGENERATECODEONLYNUMBERS_CELL","prop":"Class"},{"av":"divTblsendandvalidateotpcode_Visible","ctrl":"TBLSENDANDVALIDATEOTPCODE","prop":"Visible"}]}""");
         setEventMetadata("VALIDV_FUNCTIONID","""{"handler":"Validv_Functionid","iparms":[]}""");
         setEventMetadata("VALIDV_OTPGENERATIONTYPE","""{"handler":"Validv_Otpgenerationtype","iparms":[]}""");
         setEventMetadata("VALIDV_WSVERSION","""{"handler":"Validv_Wsversion","iparms":[]}""");
         setEventMetadata("VALIDV_CUSVERSION","""{"handler":"Validv_Cusversion","iparms":[]}""");
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
         wcpOAV46Name = "";
         wcpOAV69TypeId = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         GX_FocusControl = "";
         ClassString = "";
         StyleString = "";
         TempTags = "";
         AV37FunctionId = "";
         AV33Dsc = "";
         AV64SmallImageName = "";
         AV22BigImageName = "";
         AV44Impersonate = "";
         AV65TFAAuthenticationTypeName = "";
         AV52OTPEventValidateUser = "";
         AV54OTPGenerationType = "";
         AV55OTPGenerationType_CustomEventGenerateCode = "";
         AV50OTPEventSendCode = "";
         AV57OTPMailMessageSubject = "";
         AV56OTPMailMessageBodyHTML = "";
         AV51OTPEventValidateCode = "";
         AV24ClientId = "";
         AV25ClientSecret = "";
         AV70VersionPath = "";
         AV62SiteURL = "";
         AV26ConsumerKey = "";
         AV27ConsumerSecret = "";
         AV23CallbackURL = "";
         lblTextblockrequestscopes_Jsonclick = "";
         AV6AdditionalScope = "";
         AV40GAMRAuthenticationTypeName = "";
         AV43GAMRServerURL = "";
         AV41GAMRPrivateEncryptKey = "";
         AV42GAMRRepositoryGUID = "";
         AV80WSVersion = "";
         lblTextblockwsprivateencryptkey_Jsonclick = "";
         AV76WSServerName = "";
         AV75WSServerBaseURL = "";
         AV78WSServerSecureProtocol = 0;
         AV73WSPackage = "";
         AV72WSName = "";
         AV71WSExtension = "";
         AV32CusVersion = "";
         lblTextblockcusprivateencryptkey_Jsonclick = "";
         AV29CusFileName = "";
         AV30CusPackage = "";
         AV28CusClassName = "";
         AV87CusMethod = "";
         bttBtnenter_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         AV74WSPrivateEncryptKey = "";
         AV31CusPrivateEncryptKey = "";
         AV15AuthenticationTypeLocal = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeLocal(context);
         AV84GAMAuthenticationTypeAPIkey = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeAPIkey(context);
         AV9AuthenticationTypeApple = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeApple(context);
         AV10AuthenticationTypeCustom = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeCustom(context);
         AV19AuthenticationTypeWebService = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeWebService(context);
         AV11AuthenticationTypeFacebook = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFacebook(context);
         AV12AuthenticationTypeGAMRemote = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeGAMRemote(context);
         AV13AuthenticationTypeGAMRemoteRest = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeGAMRemoteRest(context);
         AV14AuthenticationTypeGoogle = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeGoogle(context);
         AV17AuthenticationTypeOTP = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeOTP(context);
         AV18AuthenticationTypeTwitter = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeTwitter(context);
         AV20AuthenticationTypeWeChat = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeWeChat(context);
         AV16AuthenticationTypeOauth20 = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeOauth20(context);
         AV35Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV34Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         AV38GAMAuthenticationTypeFilter = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter(context);
         AV39GAMEventSubscriptionFilter = new GeneXus.Programs.genexussecurity.SdtGAMEventSubscriptionFilter(context);
         AV94GXV2 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType", "DesignSystem.Programs");
         AV8AuthenticationType = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType(context);
         AV96GXV4 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType", "DesignSystem.Programs");
         AV98GXV6 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription>( context, "GeneXus.Programs.genexussecurity.SdtGAMEventSubscription", "DesignSystem.Programs");
         AV36EventSuscription = new GeneXus.Programs.genexussecurity.SdtGAMEventSubscription(context);
         AV100GXV8 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription>( context, "GeneXus.Programs.genexussecurity.SdtGAMEventSubscription", "DesignSystem.Programs");
         AV102GXV10 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription>( context, "GeneXus.Programs.genexussecurity.SdtGAMEventSubscription", "DesignSystem.Programs");
         AV104GXV12 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription>( context, "GeneXus.Programs.genexussecurity.SdtGAMEventSubscription", "DesignSystem.Programs");
         sStyleString = "";
         bttBtngenkeycustom_Jsonclick = "";
         bttBtngenkey_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlGx_mode = "";
         sCtrlAV46Name = "";
         sCtrlAV69TypeId = "";
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamwcauthenticationtypeentrygeneral__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamwcauthenticationtypeentrygeneral__default(),
            new Object[][] {
            }
         );
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short AV47OTPAutogeneratedCodeLength ;
      private short AV58OTPMaximumDailyNumberCodes ;
      private short AV60OTPNumberUnsuccessfulRetriesToLockOTP ;
      private short AV59OTPNumberUnsuccessfulRetriesToBlockUserBasedOfOTPLocks ;
      private short AV78WSServerSecureProtocol ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short nGXWrapped ;
      private int edtavName_Enabled ;
      private int edtavDsc_Enabled ;
      private int edtavSmallimagename_Enabled ;
      private int edtavBigimagename_Enabled ;
      private int edtavTfafirstfactorauthenticationexpiration_Visible ;
      private int AV67TFAFirstFactorAuthenticationExpiration ;
      private int edtavTfafirstfactorauthenticationexpiration_Enabled ;
      private int divTblotpauthentication_Visible ;
      private int divTblotpconfiguration_Visible ;
      private int edtavOtpautogeneratedcodelength_Visible ;
      private int edtavOtpautogeneratedcodelength_Enabled ;
      private int AV49OTPCodeExpirationTimeout ;
      private int edtavOtpcodeexpirationtimeout_Enabled ;
      private int edtavOtpmaximumdailynumbercodes_Enabled ;
      private int edtavOtpnumberunsuccessfulretriestolockotp_Enabled ;
      private int AV48OTPAutoUnlockTime ;
      private int edtavOtpautounlocktime_Enabled ;
      private int edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Enabled ;
      private int divTblsendandvalidateotpcode_Visible ;
      private int edtavOtpmailmessagesubject_Enabled ;
      private int edtavOtpmailmessagebodyhtml_Enabled ;
      private int edtavClientid_Visible ;
      private int edtavClientid_Enabled ;
      private int edtavClientsecret_Visible ;
      private int edtavClientsecret_Enabled ;
      private int edtavVersionpath_Visible ;
      private int edtavVersionpath_Enabled ;
      private int edtavSiteurl_Visible ;
      private int edtavSiteurl_Enabled ;
      private int edtavConsumerkey_Visible ;
      private int edtavConsumerkey_Enabled ;
      private int edtavConsumersecret_Visible ;
      private int edtavConsumersecret_Enabled ;
      private int edtavCallbackurl_Visible ;
      private int edtavCallbackurl_Enabled ;
      private int divTblscopes_Visible ;
      private int edtavAdditionalscope_Visible ;
      private int edtavAdditionalscope_Enabled ;
      private int edtavGamrauthenticationtypename_Visible ;
      private int edtavGamrauthenticationtypename_Enabled ;
      private int divTblserverhost_Visible ;
      private int edtavGamrserverurl_Enabled ;
      private int edtavGamrprivateencryptkey_Visible ;
      private int edtavGamrprivateencryptkey_Enabled ;
      private int edtavGamrrepositoryguid_Visible ;
      private int edtavGamrrepositoryguid_Enabled ;
      private int divTblwebservice_Visible ;
      private int edtavWsservername_Enabled ;
      private int AV77WSServerPort ;
      private int edtavWsserverport_Enabled ;
      private int edtavWsserverbaseurl_Enabled ;
      private int AV79WSTimeout ;
      private int edtavWstimeout_Enabled ;
      private int edtavWspackage_Enabled ;
      private int edtavWsname_Enabled ;
      private int edtavWsextension_Enabled ;
      private int divTblexternal_Visible ;
      private int edtavCusfilename_Enabled ;
      private int edtavCuspackage_Enabled ;
      private int edtavCusclassname_Enabled ;
      private int edtavCusmethod_Enabled ;
      private int bttBtnenter_Visible ;
      private int bttBtngenkey_Visible ;
      private int bttBtngenkeycustom_Visible ;
      private int edtavWsprivateencryptkey_Enabled ;
      private int edtavCusprivateencryptkey_Enabled ;
      private int AV93GXV1 ;
      private int AV95GXV3 ;
      private int AV97GXV5 ;
      private int AV99GXV7 ;
      private int AV101GXV9 ;
      private int AV103GXV11 ;
      private int AV105GXV13 ;
      private int idxLst ;
      private string Gx_mode ;
      private string AV46Name ;
      private string AV69TypeId ;
      private string wcpOGx_mode ;
      private string wcpOAV46Name ;
      private string wcpOAV69TypeId ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string GX_FocusControl ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string divTablecontent_Internalname ;
      private string divTableattributes_Internalname ;
      private string edtavName_Internalname ;
      private string TempTags ;
      private string edtavName_Jsonclick ;
      private string cmbavFunctionid_Internalname ;
      private string AV37FunctionId ;
      private string cmbavFunctionid_Jsonclick ;
      private string chkavIsenable_Internalname ;
      private string edtavDsc_Internalname ;
      private string AV33Dsc ;
      private string edtavDsc_Jsonclick ;
      private string edtavSmallimagename_Internalname ;
      private string AV64SmallImageName ;
      private string edtavSmallimagename_Jsonclick ;
      private string edtavBigimagename_Internalname ;
      private string AV22BigImageName ;
      private string edtavBigimagename_Jsonclick ;
      private string divImpersonate_cell_Internalname ;
      private string divImpersonate_cell_Class ;
      private string cmbavImpersonate_Internalname ;
      private string AV44Impersonate ;
      private string cmbavImpersonate_Jsonclick ;
      private string divTfaenable_cell_Internalname ;
      private string divTfaenable_cell_Class ;
      private string chkavTfaenable_Internalname ;
      private string divTfaauthenticationtypename_cell_Internalname ;
      private string divTfaauthenticationtypename_cell_Class ;
      private string cmbavTfaauthenticationtypename_Internalname ;
      private string AV65TFAAuthenticationTypeName ;
      private string cmbavTfaauthenticationtypename_Jsonclick ;
      private string divTfafirstfactorauthenticationexpiration_cell_Internalname ;
      private string divTfafirstfactorauthenticationexpiration_cell_Class ;
      private string edtavTfafirstfactorauthenticationexpiration_Internalname ;
      private string edtavTfafirstfactorauthenticationexpiration_Jsonclick ;
      private string divTfaforceforallusers_cell_Internalname ;
      private string divTfaforceforallusers_cell_Class ;
      private string chkavTfaforceforallusers_Internalname ;
      private string divTblotpauthentication_Internalname ;
      private string chkavOtpuseforfirstfactorauthentication_Internalname ;
      private string divTblotpconfiguration_Internalname ;
      private string cmbavOtpeventvalidateuser_Internalname ;
      private string AV52OTPEventValidateUser ;
      private string cmbavOtpeventvalidateuser_Jsonclick ;
      private string cmbavOtpgenerationtype_Internalname ;
      private string cmbavOtpgenerationtype_Jsonclick ;
      private string divOtpgenerationtype_customeventgeneratecode_cell_Internalname ;
      private string divOtpgenerationtype_customeventgeneratecode_cell_Class ;
      private string cmbavOtpgenerationtype_customeventgeneratecode_Internalname ;
      private string AV55OTPGenerationType_CustomEventGenerateCode ;
      private string cmbavOtpgenerationtype_customeventgeneratecode_Jsonclick ;
      private string divOtpautogeneratedcodelength_cell_Internalname ;
      private string divOtpautogeneratedcodelength_cell_Class ;
      private string edtavOtpautogeneratedcodelength_Internalname ;
      private string edtavOtpautogeneratedcodelength_Jsonclick ;
      private string divOtpgeneratecodeonlynumbers_cell_Internalname ;
      private string divOtpgeneratecodeonlynumbers_cell_Class ;
      private string chkavOtpgeneratecodeonlynumbers_Internalname ;
      private string edtavOtpcodeexpirationtimeout_Internalname ;
      private string edtavOtpcodeexpirationtimeout_Jsonclick ;
      private string edtavOtpmaximumdailynumbercodes_Internalname ;
      private string edtavOtpmaximumdailynumbercodes_Jsonclick ;
      private string edtavOtpnumberunsuccessfulretriestolockotp_Internalname ;
      private string edtavOtpnumberunsuccessfulretriestolockotp_Jsonclick ;
      private string edtavOtpautounlocktime_Internalname ;
      private string edtavOtpautounlocktime_Jsonclick ;
      private string edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Internalname ;
      private string edtavOtpnumberunsuccessfulretriestoblockuserbasedofotplocks_Jsonclick ;
      private string divTblsendandvalidateotpcode_Internalname ;
      private string cmbavOtpeventsendcode_Internalname ;
      private string AV50OTPEventSendCode ;
      private string cmbavOtpeventsendcode_Jsonclick ;
      private string edtavOtpmailmessagesubject_Internalname ;
      private string AV57OTPMailMessageSubject ;
      private string edtavOtpmailmessagesubject_Jsonclick ;
      private string edtavOtpmailmessagebodyhtml_Internalname ;
      private string cmbavOtpeventvalidatecode_Internalname ;
      private string AV51OTPEventValidateCode ;
      private string cmbavOtpeventvalidatecode_Jsonclick ;
      private string divClientid_cell_Internalname ;
      private string divClientid_cell_Class ;
      private string edtavClientid_Internalname ;
      private string edtavClientid_Jsonclick ;
      private string divClientsecret_cell_Internalname ;
      private string divClientsecret_cell_Class ;
      private string edtavClientsecret_Internalname ;
      private string edtavClientsecret_Jsonclick ;
      private string divVersionpath_cell_Internalname ;
      private string divVersionpath_cell_Class ;
      private string edtavVersionpath_Internalname ;
      private string AV70VersionPath ;
      private string edtavVersionpath_Jsonclick ;
      private string divSiteurl_cell_Internalname ;
      private string divSiteurl_cell_Class ;
      private string edtavSiteurl_Internalname ;
      private string edtavSiteurl_Jsonclick ;
      private string divAutocompletevirtualdirectory_cell_Internalname ;
      private string divAutocompletevirtualdirectory_cell_Class ;
      private string chkavAutocompletevirtualdirectory_Internalname ;
      private string divSiteurlcallbackiscustom_cell_Internalname ;
      private string divSiteurlcallbackiscustom_cell_Class ;
      private string chkavSiteurlcallbackiscustom_Internalname ;
      private string divConsumerkey_cell_Internalname ;
      private string divConsumerkey_cell_Class ;
      private string edtavConsumerkey_Internalname ;
      private string AV26ConsumerKey ;
      private string edtavConsumerkey_Jsonclick ;
      private string divConsumersecret_cell_Internalname ;
      private string divConsumersecret_cell_Class ;
      private string edtavConsumersecret_Internalname ;
      private string AV27ConsumerSecret ;
      private string edtavConsumersecret_Jsonclick ;
      private string divCallbackurl_cell_Internalname ;
      private string divCallbackurl_cell_Class ;
      private string edtavCallbackurl_Internalname ;
      private string edtavCallbackurl_Jsonclick ;
      private string divCuautocompletevirtualdirectory_cell_Internalname ;
      private string divCuautocompletevirtualdirectory_cell_Class ;
      private string chkavCuautocompletevirtualdirectory_Internalname ;
      private string divTblscopes_Internalname ;
      private string lblTextblockrequestscopes_Internalname ;
      private string lblTextblockrequestscopes_Jsonclick ;
      private string chkavAdduseradditionaldatascope_Internalname ;
      private string chkavAdduserdatascope_Internalname ;
      private string chkavAddinitialpropertiesscope_Internalname ;
      private string chkavAddapplicationdatascope_Internalname ;
      private string divAdditionalscope_cell_Internalname ;
      private string divAdditionalscope_cell_Class ;
      private string edtavAdditionalscope_Internalname ;
      private string edtavAdditionalscope_Jsonclick ;
      private string divGamrauthenticationtypename_cell_Internalname ;
      private string divGamrauthenticationtypename_cell_Class ;
      private string edtavGamrauthenticationtypename_Internalname ;
      private string AV40GAMRAuthenticationTypeName ;
      private string edtavGamrauthenticationtypename_Jsonclick ;
      private string divTblserverhost_Internalname ;
      private string divGamrserverurl_cell_Internalname ;
      private string divGamrserverurl_cell_Class ;
      private string edtavGamrserverurl_Internalname ;
      private string edtavGamrserverurl_Jsonclick ;
      private string divGamrprivateencryptkey_cell_Internalname ;
      private string divGamrprivateencryptkey_cell_Class ;
      private string edtavGamrprivateencryptkey_Internalname ;
      private string AV41GAMRPrivateEncryptKey ;
      private string edtavGamrprivateencryptkey_Jsonclick ;
      private string divGamrrepositoryguid_cell_Internalname ;
      private string divGamrrepositoryguid_cell_Class ;
      private string edtavGamrrepositoryguid_Internalname ;
      private string AV42GAMRRepositoryGUID ;
      private string edtavGamrrepositoryguid_Jsonclick ;
      private string chkavAutovalidateexternaltokenandrefresh_Internalname ;
      private string divTblwebservice_Internalname ;
      private string cmbavWsversion_Internalname ;
      private string AV80WSVersion ;
      private string cmbavWsversion_Jsonclick ;
      private string divTablesplittedwsprivateencryptkey_Internalname ;
      private string lblTextblockwsprivateencryptkey_Internalname ;
      private string lblTextblockwsprivateencryptkey_Jsonclick ;
      private string divWsservername_cell_Internalname ;
      private string divWsservername_cell_Class ;
      private string edtavWsservername_Internalname ;
      private string AV76WSServerName ;
      private string edtavWsservername_Jsonclick ;
      private string divWsserverport_cell_Internalname ;
      private string divWsserverport_cell_Class ;
      private string edtavWsserverport_Internalname ;
      private string edtavWsserverport_Jsonclick ;
      private string edtavWsserverbaseurl_Internalname ;
      private string edtavWsserverbaseurl_Jsonclick ;
      private string cmbavWsserversecureprotocol_Internalname ;
      private string cmbavWsserversecureprotocol_Jsonclick ;
      private string edtavWstimeout_Internalname ;
      private string edtavWstimeout_Jsonclick ;
      private string edtavWspackage_Internalname ;
      private string AV73WSPackage ;
      private string edtavWspackage_Jsonclick ;
      private string divWsname_cell_Internalname ;
      private string divWsname_cell_Class ;
      private string edtavWsname_Internalname ;
      private string AV72WSName ;
      private string edtavWsname_Jsonclick ;
      private string edtavWsextension_Internalname ;
      private string AV71WSExtension ;
      private string edtavWsextension_Jsonclick ;
      private string divTblexternal_Internalname ;
      private string cmbavCusversion_Internalname ;
      private string AV32CusVersion ;
      private string cmbavCusversion_Jsonclick ;
      private string divTablesplittedcusprivateencryptkey_Internalname ;
      private string lblTextblockcusprivateencryptkey_Internalname ;
      private string lblTextblockcusprivateencryptkey_Jsonclick ;
      private string divCusfilename_cell_Internalname ;
      private string divCusfilename_cell_Class ;
      private string edtavCusfilename_Internalname ;
      private string AV29CusFileName ;
      private string edtavCusfilename_Jsonclick ;
      private string edtavCuspackage_Internalname ;
      private string AV30CusPackage ;
      private string edtavCuspackage_Jsonclick ;
      private string divCusclassname_cell_Internalname ;
      private string divCusclassname_cell_Class ;
      private string edtavCusclassname_Internalname ;
      private string AV28CusClassName ;
      private string edtavCusclassname_Jsonclick ;
      private string edtavCusmethod_Internalname ;
      private string AV87CusMethod ;
      private string edtavCusmethod_Jsonclick ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Caption ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string AV74WSPrivateEncryptKey ;
      private string edtavWsprivateencryptkey_Internalname ;
      private string AV31CusPrivateEncryptKey ;
      private string edtavCusprivateencryptkey_Internalname ;
      private string bttBtngenkey_Internalname ;
      private string bttBtngenkeycustom_Internalname ;
      private string sStyleString ;
      private string tblTablemergedcusprivateencryptkey_Internalname ;
      private string edtavCusprivateencryptkey_Jsonclick ;
      private string bttBtngenkeycustom_Jsonclick ;
      private string tblTablemergedwsprivateencryptkey_Internalname ;
      private string edtavWsprivateencryptkey_Jsonclick ;
      private string bttBtngenkey_Jsonclick ;
      private string sCtrlGx_mode ;
      private string sCtrlAV46Name ;
      private string sCtrlAV69TypeId ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV83CheckRequiredFieldsResult ;
      private bool wbLoad ;
      private bool AV45IsEnable ;
      private bool AV66TFAEnable ;
      private bool AV68TFAForceForAllUsers ;
      private bool AV61OTPUseForFirstFactorAuthentication ;
      private bool AV53OTPGenerateCodeOnlyNumbers ;
      private bool AV85AutocompleteVirtualDirectory ;
      private bool AV63SiteURLCallbackIsCustom ;
      private bool AV86CUAutocompleteVirtualDirectory ;
      private bool AV7AddUserAdditionalDataScope ;
      private bool AV88AddUserDataScope ;
      private bool AV5AddInitialPropertiesScope ;
      private bool AV89AddApplicationDataScope ;
      private bool AV21AutovalidateExternalTokenAndRefresh ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private string AV54OTPGenerationType ;
      private string AV56OTPMailMessageBodyHTML ;
      private string AV24ClientId ;
      private string AV25ClientSecret ;
      private string AV62SiteURL ;
      private string AV23CallbackURL ;
      private string AV6AdditionalScope ;
      private string AV43GAMRServerURL ;
      private string AV75WSServerBaseURL ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_Gx_mode ;
      private string aP1_Name ;
      private string aP2_TypeId ;
      private GXCombobox cmbavFunctionid ;
      private GXCheckbox chkavIsenable ;
      private GXCombobox cmbavImpersonate ;
      private GXCheckbox chkavTfaenable ;
      private GXCombobox cmbavTfaauthenticationtypename ;
      private GXCheckbox chkavTfaforceforallusers ;
      private GXCheckbox chkavOtpuseforfirstfactorauthentication ;
      private GXCombobox cmbavOtpeventvalidateuser ;
      private GXCombobox cmbavOtpgenerationtype ;
      private GXCombobox cmbavOtpgenerationtype_customeventgeneratecode ;
      private GXCheckbox chkavOtpgeneratecodeonlynumbers ;
      private GXCombobox cmbavOtpeventsendcode ;
      private GXCombobox cmbavOtpeventvalidatecode ;
      private GXCheckbox chkavAutocompletevirtualdirectory ;
      private GXCheckbox chkavSiteurlcallbackiscustom ;
      private GXCheckbox chkavCuautocompletevirtualdirectory ;
      private GXCheckbox chkavAdduseradditionaldatascope ;
      private GXCheckbox chkavAdduserdatascope ;
      private GXCheckbox chkavAddinitialpropertiesscope ;
      private GXCheckbox chkavAddapplicationdatascope ;
      private GXCheckbox chkavAutovalidateexternaltokenandrefresh ;
      private GXCombobox cmbavWsversion ;
      private GXCombobox cmbavWsserversecureprotocol ;
      private GXCombobox cmbavCusversion ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeLocal AV15AuthenticationTypeLocal ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeAPIkey AV84GAMAuthenticationTypeAPIkey ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeApple AV9AuthenticationTypeApple ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeCustom AV10AuthenticationTypeCustom ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeWebService AV19AuthenticationTypeWebService ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFacebook AV11AuthenticationTypeFacebook ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeGAMRemote AV12AuthenticationTypeGAMRemote ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeGAMRemoteRest AV13AuthenticationTypeGAMRemoteRest ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeGoogle AV14AuthenticationTypeGoogle ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeOTP AV17AuthenticationTypeOTP ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeTwitter AV18AuthenticationTypeTwitter ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeWeChat AV20AuthenticationTypeWeChat ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeOauth20 AV16AuthenticationTypeOauth20 ;
      private IDataStoreProvider pr_default ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV35Errors ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV34Error ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeFilter AV38GAMAuthenticationTypeFilter ;
      private GeneXus.Programs.genexussecurity.SdtGAMEventSubscriptionFilter AV39GAMEventSubscriptionFilter ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType> AV94GXV2 ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType AV8AuthenticationType ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationType> AV96GXV4 ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription> AV98GXV6 ;
      private GeneXus.Programs.genexussecurity.SdtGAMEventSubscription AV36EventSuscription ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription> AV100GXV8 ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription> AV102GXV10 ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMEventSubscription> AV104GXV12 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamwcauthenticationtypeentrygeneral__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamwcauthenticationtypeentrygeneral__default : DataStoreHelperBase, IDataStoreHelper
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
