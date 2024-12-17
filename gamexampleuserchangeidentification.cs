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
   public class gamexampleuserchangeidentification : GXDataArea
   {
      public gamexampleuserchangeidentification( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamexampleuserchangeidentification( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref string aP0_ChangeType )
      {
         this.AV6ChangeType = aP0_ChangeType;
         ExecuteImpl();
         aP0_ChangeType=this.AV6ChangeType;
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
            gxfirstwebparm = GetFirstPar( "ChangeType");
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
               gxfirstwebparm = GetFirstPar( "ChangeType");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "ChangeType");
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
            return "gam_changeemail_Execute" ;
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
         PA3F2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3F2( ) ;
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
         GXEncryptionTmp = "gamexampleuserchangeidentification.aspx"+UrlEncode(StringUtil.RTrim(AV6ChangeType));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamexampleuserchangeidentification.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vCHANGETYPE", AV6ChangeType);
         GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"GAMExampleUserChangeIdentification");
         forbiddenHiddens.Add("CurrentUserIdentificaton", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("gamexampleuserchangeidentification:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vCHANGETYPE", AV6ChangeType);
         GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
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
            WE3F2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3F2( ) ;
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
         GXEncryptionTmp = "gamexampleuserchangeidentification.aspx"+UrlEncode(StringUtil.RTrim(AV6ChangeType));
         return formatLink("gamexampleuserchangeidentification.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "GAMExampleUserChangeIdentification" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "GAMExample User Change Identification", "") ;
      }

      protected void WB3F0( )
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
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "bg-white", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom20", "start", "top", "", "", "div");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCurrentuseridentificaton_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavCurrentuseridentificaton_Internalname, edtavCurrentuseridentificaton_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCurrentuseridentificaton_Internalname, AV8CurrentUserIdentificaton, StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCurrentuseridentificaton_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavCurrentuseridentificaton_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_GAMExampleUserChangeIdentification.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavNewuseridentification_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavNewuseridentification_Internalname, edtavNewuseridentification_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewuseridentification_Internalname, AV12NewUserIdentification, StringUtil.RTrim( context.localUtil.Format( AV12NewUserIdentification, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,21);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewuseridentification_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewuseridentification_Enabled, 1, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_GAMExampleUserChangeIdentification.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavConfirmuseridentification_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavConfirmuseridentification_Internalname, edtavConfirmuseridentification_Caption, " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavConfirmuseridentification_Internalname, AV7ConfirmUserIdentification, StringUtil.RTrim( context.localUtil.Format( AV7ConfirmUserIdentification, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,26);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavConfirmuseridentification_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavConfirmuseridentification_Enabled, 1, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_GAMExampleUserChangeIdentification.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserpassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserpassword_Internalname, context.GetMessage( "GAM_Password", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserpassword_Internalname, StringUtil.RTrim( AV13UserPassword), StringUtil.RTrim( context.localUtil.Format( AV13UserPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,31);\""+" "+"data-gx-password-reveal"+" "+"idenableshowpasswordhint=\"True\""+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserpassword_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserpassword_Enabled, 1, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_GAMExampleUserChangeIdentification.htm");
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
               GxWebStd.gx_hidden_field( context, "W0034"+"", StringUtil.RTrim( WebComp_Wcmessages_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpW0034"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
               {
                  if ( StringUtil.StrCmp(StringUtil.Lower( OldWcmessages), StringUtil.Lower( WebComp_Wcmessages_Component)) != 0 )
                  {
                     context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0034"+"");
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
            GxWebStd.gx_div_start( context, divGam_footerpopup_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGam_footerpopup_tablebuttons_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellMarginBottom20", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtngam_footerpopup_btnconfirm_Internalname, "", bttBtngam_footerpopup_btnconfirm_Caption, bttBtngam_footerpopup_btnconfirm_Jsonclick, 5, context.GetMessage( "Add selected permissions", ""), "", StyleString, ClassString, bttBtngam_footerpopup_btnconfirm_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOGAM_FOOTERPOPUP_BTNCONFIRM\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMExampleUserChangeIdentification.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtn_cancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtn_cancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, bttBtn_cancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMExampleUserChangeIdentification.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "end", "top", "div");
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
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START3F2( )
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
         Form.Meta.addItem("description", context.GetMessage( "GAMExample User Change Identification", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3F0( ) ;
      }

      protected void WS3F2( )
      {
         START3F2( ) ;
         EVT3F2( ) ;
      }

      protected void EVT3F2( )
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
                              E113F2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOGAM_FOOTERPOPUP_BTNCONFIRM'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoGAM_FooterPopup_BtnConfirm' */
                              E123F2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E133F2 ();
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
                        if ( nCmpId == 34 )
                        {
                           OldWcmessages = cgiGet( "W0034");
                           if ( ( StringUtil.Len( OldWcmessages) == 0 ) || ( StringUtil.StrCmp(OldWcmessages, WebComp_Wcmessages_Component) != 0 ) )
                           {
                              WebComp_Wcmessages = getWebComponent(GetType(), "DesignSystem.Programs", OldWcmessages, new Object[] {context} );
                              WebComp_Wcmessages.ComponentInit();
                              WebComp_Wcmessages.Name = "OldWcmessages";
                              WebComp_Wcmessages_Component = OldWcmessages;
                           }
                           if ( StringUtil.Len( WebComp_Wcmessages_Component) != 0 )
                           {
                              WebComp_Wcmessages.componentprocess("W0034", "", sEvt);
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

      protected void WE3F2( )
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

      protected void PA3F2( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamexampleuserchangeidentification.aspx")), "gamexampleuserchangeidentification.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamexampleuserchangeidentification.aspx")))) ;
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
                  gxfirstwebparm = GetFirstPar( "ChangeType");
                  toggleJsOutput = isJsOutputEnabled( );
                  if ( context.isSpaRequest( ) )
                  {
                     disableJsOutput();
                  }
                  if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
                  {
                     AV6ChangeType = gxfirstwebparm;
                     AssignAttri("", false, "AV6ChangeType", AV6ChangeType);
                     GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
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
               GX_FocusControl = edtavCurrentuseridentificaton_Internalname;
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
         RF3F2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCurrentuseridentificaton_Enabled = 0;
         AssignProp("", false, edtavCurrentuseridentificaton_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCurrentuseridentificaton_Enabled), 5, 0), true);
      }

      protected void RF3F2( )
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
            E133F2 ();
            WB3F0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3F2( )
      {
         GxWebStd.gx_hidden_field( context, "vCHANGETYPE", AV6ChangeType);
         GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
      }

      protected void before_start_formulas( )
      {
         edtavCurrentuseridentificaton_Enabled = 0;
         AssignProp("", false, edtavCurrentuseridentificaton_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCurrentuseridentificaton_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3F0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E113F2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
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
            /* Read variables values. */
            AV8CurrentUserIdentificaton = cgiGet( edtavCurrentuseridentificaton_Internalname);
            AssignAttri("", false, "AV8CurrentUserIdentificaton", AV8CurrentUserIdentificaton);
            GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
            AV12NewUserIdentification = cgiGet( edtavNewuseridentification_Internalname);
            AssignAttri("", false, "AV12NewUserIdentification", AV12NewUserIdentification);
            AV7ConfirmUserIdentification = cgiGet( edtavConfirmuseridentification_Internalname);
            AssignAttri("", false, "AV7ConfirmUserIdentification", AV7ConfirmUserIdentification);
            AV13UserPassword = cgiGet( edtavUserpassword_Internalname);
            AssignAttri("", false, "AV13UserPassword", AV13UserPassword);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            forbiddenHiddens = new GXProperties();
            forbiddenHiddens.Add("hshsalt", "hsh"+"GAMExampleUserChangeIdentification");
            AV8CurrentUserIdentificaton = cgiGet( edtavCurrentuseridentificaton_Internalname);
            AssignAttri("", false, "AV8CurrentUserIdentificaton", AV8CurrentUserIdentificaton);
            GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
            forbiddenHiddens.Add("CurrentUserIdentificaton", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")));
            hsh = cgiGet( "hsh");
            if ( ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
            {
               GXUtil.WriteLogError("gamexampleuserchangeidentification:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
         E113F2 ();
         if (returnInSub) return;
      }

      protected void E113F2( )
      {
         /* Start Routine */
         returnInSub = false;
         bttBtn_cancel_Visible = 0;
         AssignProp("", false, bttBtn_cancel_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtn_cancel_Visible), 5, 0), true);
         bttBtngam_footerpopup_btnconfirm_Caption = context.GetMessage( "GAM_Confirm", "");
         AssignProp("", false, bttBtngam_footerpopup_btnconfirm_Internalname, "Caption", bttBtngam_footerpopup_btnconfirm_Caption, true);
         AV11GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV11GAMUser.gxTpr_Firstname)) && String.IsNullOrEmpty(StringUtil.RTrim( AV11GAMUser.gxTpr_Lastname)) )
         {
            Form.Caption = AV11GAMUser.gxTpr_Name;
            AssignProp("", false, "FORM", "Caption", Form.Caption, true);
         }
         else
         {
            Form.Caption = StringUtil.Format( "%1 %2", AV11GAMUser.gxTpr_Firstname, AV11GAMUser.gxTpr_Lastname, "", "", "", "", "", "", "");
            AssignProp("", false, "FORM", "Caption", Form.Caption, true);
         }
         AV10GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
         if ( StringUtil.StrCmp(AV10GAMRepository.gxTpr_Useridentification, "email") == 0 )
         {
            AV6ChangeType = "Email";
            AssignAttri("", false, "AV6ChangeType", AV6ChangeType);
            GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
            AV8CurrentUserIdentificaton = AV11GAMUser.gxTpr_Email;
            AssignAttri("", false, "AV8CurrentUserIdentificaton", AV8CurrentUserIdentificaton);
            GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
         }
         else if ( StringUtil.StrCmp(AV10GAMRepository.gxTpr_Useridentification, "name") == 0 )
         {
            AV6ChangeType = "Name";
            AssignAttri("", false, "AV6ChangeType", AV6ChangeType);
            GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
            edtavCurrentuseridentificaton_Caption = context.GetMessage( "GAM_CurrentName", "");
            AssignProp("", false, edtavCurrentuseridentificaton_Internalname, "Caption", edtavCurrentuseridentificaton_Caption, true);
            AV8CurrentUserIdentificaton = AV11GAMUser.gxTpr_Name;
            AssignAttri("", false, "AV8CurrentUserIdentificaton", AV8CurrentUserIdentificaton);
            GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
            edtavNewuseridentification_Caption = context.GetMessage( "GAM_Newname", "");
            AssignProp("", false, edtavNewuseridentification_Internalname, "Caption", edtavNewuseridentification_Caption, true);
            edtavConfirmuseridentification_Caption = context.GetMessage( "GAM_ConfirmName", "");
            AssignProp("", false, edtavConfirmuseridentification_Internalname, "Caption", edtavConfirmuseridentification_Caption, true);
         }
         else
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV6ChangeType)) || ( StringUtil.StrCmp(AV6ChangeType, "Email") == 0 ) )
            {
               AV6ChangeType = "Email";
               AssignAttri("", false, "AV6ChangeType", AV6ChangeType);
               GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
               AV8CurrentUserIdentificaton = AV11GAMUser.gxTpr_Email;
               AssignAttri("", false, "AV8CurrentUserIdentificaton", AV8CurrentUserIdentificaton);
               GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
            }
            else
            {
               AV6ChangeType = "Name";
               AssignAttri("", false, "AV6ChangeType", AV6ChangeType);
               GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
               edtavCurrentuseridentificaton_Caption = context.GetMessage( "GAM_CurrentName", "");
               AssignProp("", false, edtavCurrentuseridentificaton_Internalname, "Caption", edtavCurrentuseridentificaton_Caption, true);
               AV8CurrentUserIdentificaton = AV11GAMUser.gxTpr_Name;
               AssignAttri("", false, "AV8CurrentUserIdentificaton", AV8CurrentUserIdentificaton);
               GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTUSERIDENTIFICATON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8CurrentUserIdentificaton, "")), context));
               edtavNewuseridentification_Caption = context.GetMessage( "GAM_NewName", "");
               AssignProp("", false, edtavNewuseridentification_Internalname, "Caption", edtavNewuseridentification_Caption, true);
               edtavConfirmuseridentification_Caption = context.GetMessage( "GAM_ConfirmName", "");
               AssignProp("", false, edtavConfirmuseridentification_Internalname, "Caption", edtavConfirmuseridentification_Caption, true);
            }
         }
      }

      protected void E123F2( )
      {
         /* 'DoGAM_FooterPopup_BtnConfirm' Routine */
         returnInSub = false;
         AV11GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         if ( StringUtil.StrCmp(AV6ChangeType, "Email") == 0 )
         {
            AV15UI_ChangeType = context.GetMessage( "GAM_Email", "");
         }
         else
         {
            AV15UI_ChangeType = context.GetMessage( "GAM_Name", "");
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12NewUserIdentification)) )
         {
            if ( StringUtil.StrCmp(AV12NewUserIdentification, AV7ConfirmUserIdentification) == 0 )
            {
               AV5AdditionalParameter.gxTpr_Authenticationtypename = "local";
               AV5AdditionalParameter.gxTpr_Isbatch = true;
               AV14LoginOK = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).login(AV8CurrentUserIdentificaton, AV13UserPassword, AV5AdditionalParameter, out  AV9GAMErrorCollection);
               if ( AV14LoginOK )
               {
                  AV11GAMUser.load( AV11GAMUser.gxTpr_Guid);
                  if ( StringUtil.StrCmp(AV6ChangeType, "Email") == 0 )
                  {
                     AV11GAMUser.gxTpr_Email = AV12NewUserIdentification;
                  }
                  else
                  {
                     AV11GAMUser.gxTpr_Name = AV12NewUserIdentification;
                  }
                  AV11GAMUser.save();
                  if ( AV11GAMUser.success() )
                  {
                     context.CommitDataStores("gamexampleuserchangeidentification",pr_default);
                     edtavNewuseridentification_Enabled = 0;
                     AssignProp("", false, edtavNewuseridentification_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavNewuseridentification_Enabled), 5, 0), true);
                     edtavConfirmuseridentification_Enabled = 0;
                     AssignProp("", false, edtavConfirmuseridentification_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavConfirmuseridentification_Enabled), 5, 0), true);
                     edtavUserpassword_Enabled = 0;
                     AssignProp("", false, edtavUserpassword_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserpassword_Enabled), 5, 0), true);
                     bttBtngam_footerpopup_btnconfirm_Visible = 0;
                     AssignProp("", false, bttBtngam_footerpopup_btnconfirm_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtngam_footerpopup_btnconfirm_Visible), 5, 0), true);
                     new gam_setmessage(context ).execute(  "Message",  StringUtil.Format( context.GetMessage( "GAM_Your1waschangedsuccessfully", ""), StringUtil.Lower( AV15UI_ChangeType), "", "", "", "", "", "", "", "")) ;
                  }
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S112 ();
                  if (returnInSub) return;
               }
               else
               {
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S112 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               new gam_setmessage(context ).execute(  "Error",  StringUtil.Format( context.GetMessage( "GAM_The1andconfirmation1donotmatch", ""), StringUtil.Lower( AV15UI_ChangeType), "", "", "", "", "", "", "", "")) ;
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S112 ();
               if (returnInSub) return;
            }
         }
         else
         {
            new gam_setmessage(context ).execute(  "Error",  StringUtil.Format( context.GetMessage( "GAM_Youmustcompletethe1field", ""), StringUtil.Lower( AV15UI_ChangeType), "", "", "", "", "", "", "", "")) ;
            /* Execute user subroutine: 'SHOWMESSAGES' */
            S112 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5AdditionalParameter", AV5AdditionalParameter);
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
            WebComp_Wcmessages.componentprepare(new Object[] {(string)"W0034",(string)""});
            WebComp_Wcmessages.componentbind(new Object[] {});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Wcmessages )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpW0034"+"");
            WebComp_Wcmessages.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E133F2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV6ChangeType = (string)getParm(obj,0);
         AssignAttri("", false, "AV6ChangeType", AV6ChangeType);
         GxWebStd.gx_hidden_field( context, "gxhash_vCHANGETYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV6ChangeType, "")), context));
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
         PA3F2( ) ;
         WS3F2( ) ;
         WE3F2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217074373", true, true);
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
         context.AddJavascriptSource("gamexampleuserchangeidentification.js", "?20241217074379", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavCurrentuseridentificaton_Internalname = "vCURRENTUSERIDENTIFICATON";
         edtavNewuseridentification_Internalname = "vNEWUSERIDENTIFICATION";
         edtavConfirmuseridentification_Internalname = "vCONFIRMUSERIDENTIFICATION";
         edtavUserpassword_Internalname = "vUSERPASSWORD";
         bttBtngam_footerpopup_btnconfirm_Internalname = "BTNGAM_FOOTERPOPUP_BTNCONFIRM";
         bttBtn_cancel_Internalname = "BTN_CANCEL";
         divGam_footerpopup_tablebuttons_Internalname = "GAM_FOOTERPOPUP_TABLEBUTTONS";
         divGam_footerpopup_Internalname = "GAM_FOOTERPOPUP";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         Dvpanel_unnamedtable1_Internalname = "DVPANEL_UNNAMEDTABLE1";
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
         bttBtn_cancel_Visible = 1;
         bttBtngam_footerpopup_btnconfirm_Caption = context.GetMessage( "GAM_Addselected", "");
         bttBtngam_footerpopup_btnconfirm_Visible = 1;
         edtavUserpassword_Jsonclick = "";
         edtavUserpassword_Enabled = 1;
         edtavConfirmuseridentification_Jsonclick = "";
         edtavConfirmuseridentification_Enabled = 1;
         edtavConfirmuseridentification_Caption = context.GetMessage( "GAM_ConfirmEmail", "");
         edtavNewuseridentification_Jsonclick = "";
         edtavNewuseridentification_Enabled = 1;
         edtavNewuseridentification_Caption = context.GetMessage( "GAM_NewEmail", "");
         edtavCurrentuseridentificaton_Jsonclick = "";
         edtavCurrentuseridentificaton_Enabled = 1;
         edtavCurrentuseridentificaton_Caption = context.GetMessage( "GAM_CurrentEmail", "");
         Dvpanel_unnamedtable1_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Iconposition = "Right";
         Dvpanel_unnamedtable1_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsible = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Title = context.GetMessage( "Realiza los cambios", "");
         Dvpanel_unnamedtable1_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable1_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Width = "100%";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "GAMExample User Change Identification", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV6ChangeType","fld":"vCHANGETYPE","hsh":true},{"av":"AV8CurrentUserIdentificaton","fld":"vCURRENTUSERIDENTIFICATON","hsh":true}]}""");
         setEventMetadata("'DOGAM_FOOTERPOPUP_BTNCONFIRM'","""{"handler":"E123F2","iparms":[{"av":"AV6ChangeType","fld":"vCHANGETYPE","hsh":true},{"av":"AV12NewUserIdentification","fld":"vNEWUSERIDENTIFICATION"},{"av":"AV7ConfirmUserIdentification","fld":"vCONFIRMUSERIDENTIFICATION"},{"av":"AV8CurrentUserIdentificaton","fld":"vCURRENTUSERIDENTIFICATON","hsh":true},{"av":"AV13UserPassword","fld":"vUSERPASSWORD"}]""");
         setEventMetadata("'DOGAM_FOOTERPOPUP_BTNCONFIRM'",""","oparms":[{"av":"edtavNewuseridentification_Enabled","ctrl":"vNEWUSERIDENTIFICATION","prop":"Enabled"},{"av":"edtavConfirmuseridentification_Enabled","ctrl":"vCONFIRMUSERIDENTIFICATION","prop":"Enabled"},{"av":"edtavUserpassword_Enabled","ctrl":"vUSERPASSWORD","prop":"Enabled"},{"ctrl":"BTNGAM_FOOTERPOPUP_BTNCONFIRM","prop":"Visible"},{"ctrl":"WCMESSAGES"}]}""");
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
         wcpOAV6ChangeType = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV8CurrentUserIdentificaton = "";
         forbiddenHiddens = new GXProperties();
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ucDvpanel_unnamedtable1 = new GXUserControl();
         TempTags = "";
         AV12NewUserIdentification = "";
         AV7ConfirmUserIdentification = "";
         AV13UserPassword = "";
         WebComp_Wcmessages_Component = "";
         OldWcmessages = "";
         ClassString = "";
         StyleString = "";
         bttBtngam_footerpopup_btnconfirm_Jsonclick = "";
         bttBtn_cancel_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         hsh = "";
         AV11GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV10GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV15UI_ChangeType = "";
         AV5AdditionalParameter = new GeneXus.Programs.genexussecurity.SdtGAMLoginAdditionalParameters(context);
         AV9GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamexampleuserchangeidentification__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamexampleuserchangeidentification__default(),
            new Object[][] {
            }
         );
         WebComp_Wcmessages = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         edtavCurrentuseridentificaton_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short nGXWrapped ;
      private int edtavCurrentuseridentificaton_Enabled ;
      private int edtavNewuseridentification_Enabled ;
      private int edtavConfirmuseridentification_Enabled ;
      private int edtavUserpassword_Enabled ;
      private int bttBtngam_footerpopup_btnconfirm_Visible ;
      private int bttBtn_cancel_Visible ;
      private int idxLst ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string Dvpanel_unnamedtable1_Width ;
      private string Dvpanel_unnamedtable1_Cls ;
      private string Dvpanel_unnamedtable1_Title ;
      private string Dvpanel_unnamedtable1_Iconposition ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divMaintable_Internalname ;
      private string Dvpanel_unnamedtable1_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string edtavCurrentuseridentificaton_Internalname ;
      private string edtavCurrentuseridentificaton_Caption ;
      private string TempTags ;
      private string edtavCurrentuseridentificaton_Jsonclick ;
      private string edtavNewuseridentification_Internalname ;
      private string edtavNewuseridentification_Caption ;
      private string edtavNewuseridentification_Jsonclick ;
      private string edtavConfirmuseridentification_Internalname ;
      private string edtavConfirmuseridentification_Caption ;
      private string edtavConfirmuseridentification_Jsonclick ;
      private string edtavUserpassword_Internalname ;
      private string AV13UserPassword ;
      private string edtavUserpassword_Jsonclick ;
      private string WebComp_Wcmessages_Component ;
      private string OldWcmessages ;
      private string divGam_footerpopup_Internalname ;
      private string divGam_footerpopup_tablebuttons_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtngam_footerpopup_btnconfirm_Internalname ;
      private string bttBtngam_footerpopup_btnconfirm_Caption ;
      private string bttBtngam_footerpopup_btnconfirm_Jsonclick ;
      private string bttBtn_cancel_Internalname ;
      private string bttBtn_cancel_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string hsh ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Dvpanel_unnamedtable1_Autowidth ;
      private bool Dvpanel_unnamedtable1_Autoheight ;
      private bool Dvpanel_unnamedtable1_Collapsible ;
      private bool Dvpanel_unnamedtable1_Collapsed ;
      private bool Dvpanel_unnamedtable1_Showcollapseicon ;
      private bool Dvpanel_unnamedtable1_Autoscroll ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV14LoginOK ;
      private bool bDynCreated_Wcmessages ;
      private string AV6ChangeType ;
      private string wcpOAV6ChangeType ;
      private string AV8CurrentUserIdentificaton ;
      private string AV12NewUserIdentification ;
      private string AV7ConfirmUserIdentification ;
      private string AV15UI_ChangeType ;
      private GXWebComponent WebComp_Wcmessages ;
      private GXProperties forbiddenHiddens ;
      private GXUserControl ucDvpanel_unnamedtable1 ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_ChangeType ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV11GAMUser ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV10GAMRepository ;
      private GeneXus.Programs.genexussecurity.SdtGAMLoginAdditionalParameters AV5AdditionalParameter ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV9GAMErrorCollection ;
      private IDataStoreProvider pr_default ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamexampleuserchangeidentification__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamexampleuserchangeidentification__default : DataStoreHelperBase, IDataStoreHelper
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
