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
   public class gamwcauthenticationtypeentrysaml20 : GXWebComponent
   {
      public gamwcauthenticationtypeentrysaml20( )
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

      public gamwcauthenticationtypeentrysaml20( IGxContext context )
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
         this.AV32Name = aP1_Name;
         this.AV41TypeId = aP2_TypeId;
         ExecuteImpl();
         aP0_Gx_mode=this.Gx_mode;
         aP1_Name=this.AV32Name;
         aP2_TypeId=this.AV41TypeId;
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
         chkavIsredirectbinding = new GXCheckbox();
         cmbavNameidpolicyformat = new GXCombobox();
         chkavForceauthn = new GXCheckbox();
         chkavDisablesinglelogout = new GXCheckbox();
         chkavUserinforesponseuserlastnamegenauto = new GXCheckbox();
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
                  AV32Name = GetPar( "Name");
                  AssignAttri(sPrefix, false, "AV32Name", AV32Name);
                  AV41TypeId = GetPar( "TypeId");
                  AssignAttri(sPrefix, false, "AV41TypeId", AV41TypeId);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(string)Gx_mode,(string)AV32Name,(string)AV41TypeId});
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrGrid_newrow_invoke( )
      {
         nRC_GXsfl_235 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_235"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_235_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_235_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_235_idx = GetPar( "sGXsfl_235_idx");
         sPrefix = GetPar( "sPrefix");
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
         Gx_mode = GetPar( "Mode");
         AV19IsEnable = StringUtil.StrToBool( GetPar( "IsEnable"));
         AV20IsRedirectBinding = StringUtil.StrToBool( GetPar( "IsRedirectBinding"));
         AV15ForceAuthn = StringUtil.StrToBool( GetPar( "ForceAuthn"));
         AV67DisableSingleLogout = StringUtil.StrToBool( GetPar( "DisableSingleLogout"));
         AV51UserInfoResponseUserLastNameGenAuto = StringUtil.StrToBool( GetPar( "UserInfoResponseUserLastNameGenAuto"));
         AV45UserInfoResponseUserErrorDescriptionTag = GetPar( "UserInfoResponseUserErrorDescriptionTag");
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, Gx_mode, AV19IsEnable, AV20IsRedirectBinding, AV15ForceAuthn, AV67DisableSingleLogout, AV51UserInfoResponseUserLastNameGenAuto, AV45UserInfoResponseUserErrorDescriptionTag, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGrid_refresh_invoke */
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
            PA1H2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS1H2( ) ;
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
            context.SendWebValue( context.GetMessage( "GAMWCAuthentication Type Entry Saml20", "")) ;
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
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
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
            GXEncryptionTmp = "gamwcauthenticationtypeentrysaml20.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.RTrim(AV32Name)) + "," + UrlEncode(StringUtil.RTrim(AV41TypeId));
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamwcauthenticationtypeentrysaml20.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG", StringUtil.RTrim( AV45UserInfoResponseUserErrorDescriptionTag));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV45UserInfoResponseUserErrorDescriptionTag, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_235", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_235), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOGx_mode", StringUtil.RTrim( wcpOGx_mode));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV32Name", StringUtil.RTrim( wcpOAV32Name));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV41TypeId", StringUtil.RTrim( wcpOAV41TypeId));
         GxWebStd.gx_hidden_field( context, sPrefix+"vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vCHECKREQUIREDFIELDSRESULT", AV70CheckRequiredFieldsResult);
         GxWebStd.gx_hidden_field( context, sPrefix+"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG", StringUtil.RTrim( AV45UserInfoResponseUserErrorDescriptionTag));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV45UserInfoResponseUserErrorDescriptionTag, "")), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vTYPEID", StringUtil.RTrim( AV41TypeId));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"subGrid_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Width", StringUtil.RTrim( Dvpanel_unnamedtable7_Width));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Autowidth));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Autoheight));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Cls", StringUtil.RTrim( Dvpanel_unnamedtable7_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Title", StringUtil.RTrim( Dvpanel_unnamedtable7_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Collapsible));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Collapsed));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable7_Iconposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE7_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable7_Autoscroll));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Width", StringUtil.RTrim( Dvpanel_unnamedtable8_Width));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Autowidth));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Autoheight));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Cls", StringUtil.RTrim( Dvpanel_unnamedtable8_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Title", StringUtil.RTrim( Dvpanel_unnamedtable8_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Collapsible));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Collapsed));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable8_Iconposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE8_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable8_Autoscroll));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Width", StringUtil.RTrim( Dvpanel_unnamedtable4_Width));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Autowidth));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Autoheight));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Cls", StringUtil.RTrim( Dvpanel_unnamedtable4_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Title", StringUtil.RTrim( Dvpanel_unnamedtable4_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Collapsible));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Collapsed));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable4_Iconposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE4_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable4_Autoscroll));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Width", StringUtil.RTrim( Dvpanel_unnamedtable5_Width));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable5_Autowidth));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable5_Autoheight));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Cls", StringUtil.RTrim( Dvpanel_unnamedtable5_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Title", StringUtil.RTrim( Dvpanel_unnamedtable5_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable5_Collapsible));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable5_Collapsed));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable5_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable5_Iconposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE5_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable5_Autoscroll));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Width", StringUtil.RTrim( Dvpanel_unnamedtable2_Width));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable2_Autowidth));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable2_Autoheight));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Cls", StringUtil.RTrim( Dvpanel_unnamedtable2_Cls));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Title", StringUtil.RTrim( Dvpanel_unnamedtable2_Title));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable2_Collapsible));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable2_Collapsed));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable2_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable2_Iconposition));
         GxWebStd.gx_hidden_field( context, sPrefix+"DVPANEL_UNNAMEDTABLE2_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable2_Autoscroll));
         GxWebStd.gx_hidden_field( context, sPrefix+"GXUITABSPANEL_TABS_Pagecount", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gxuitabspanel_tabs_Pagecount), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GXUITABSPANEL_TABS_Class", StringUtil.RTrim( Gxuitabspanel_tabs_Class));
         GxWebStd.gx_hidden_field( context, sPrefix+"GXUITABSPANEL_TABS_Historymanagement", StringUtil.BoolToStr( Gxuitabspanel_tabs_Historymanagement));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_EMPOWERER_Gridinternalname", StringUtil.RTrim( Grid_empowerer_Gridinternalname));
      }

      protected void RenderHtmlCloseForm1H2( )
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
         return "GAMWCAuthenticationTypeEntrySaml20" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "GAMWCAuthentication Type Entry Saml20", "") ;
      }

      protected void WB1H0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "gamwcauthenticationtypeentrysaml20.aspx");
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
               context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavName_Internalname, StringUtil.RTrim( AV32Name), StringUtil.RTrim( context.localUtil.Format( AV32Name, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,20);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavName_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavName_Enabled, 1, "text", "", 80, "chr", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavFunctionid, cmbavFunctionid_Internalname, StringUtil.RTrim( AV16FunctionId), 1, cmbavFunctionid_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavFunctionid.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,24);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            cmbavFunctionid.CurrentValue = StringUtil.RTrim( AV16FunctionId);
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavDsc_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavDsc_Internalname, context.GetMessage( "WWP_GAM_Description", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavDsc_Internalname, StringUtil.RTrim( AV10Dsc), StringUtil.RTrim( context.localUtil.Format( AV10Dsc, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavDsc_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavDsc_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
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
            GxWebStd.gx_label_element( context, chkavIsenable_Internalname, context.GetMessage( "WWP_GAM_Enabled", ""), " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 34,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavIsenable_Internalname, StringUtil.BoolToStr( AV19IsEnable), "", context.GetMessage( "WWP_GAM_Enabled", ""), 1, chkavIsenable.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(34, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,34);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavImpersonate_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavImpersonate_Internalname, context.GetMessage( "WWP_GAM_Impersonate", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavImpersonate_Internalname, StringUtil.RTrim( AV18Impersonate), StringUtil.RTrim( context.localUtil.Format( AV18Impersonate, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavImpersonate_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavImpersonate_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMAuthenticationTypeName", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 43,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSmallimagename_Internalname, StringUtil.RTrim( AV40SmallImageName), StringUtil.RTrim( context.localUtil.Format( AV40SmallImageName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,43);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSmallimagename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSmallimagename_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 47,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavBigimagename_Internalname, StringUtil.RTrim( AV7BigImageName), StringUtil.RTrim( context.localUtil.Format( AV7BigImageName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,47);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavBigimagename_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavBigimagename_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* User Defined Control */
            ucGxuitabspanel_tabs.SetProperty("PageCount", Gxuitabspanel_tabs_Pagecount);
            ucGxuitabspanel_tabs.SetProperty("Class", Gxuitabspanel_tabs_Class);
            ucGxuitabspanel_tabs.SetProperty("HistoryManagement", Gxuitabspanel_tabs_Historymanagement);
            ucGxuitabspanel_tabs.Render(context, "tab", Gxuitabspanel_tabs_Internalname, sPrefix+"GXUITABSPANEL_TABSContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"GXUITABSPANEL_TABSContainer"+"title1"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblGeneral_title_Internalname, context.GetMessage( "WWP_GAM_General", ""), "", "", lblGeneral_title_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "General") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"GXUITABSPANEL_TABSContainer"+"panel1"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable6_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 RequiredDataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavLocalsiteurl_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavLocalsiteurl_Internalname, context.GetMessage( "WWP_GAM_LocalSiteURL", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavLocalsiteurl_Internalname, AV30LocalSiteURL, StringUtil.RTrim( context.localUtil.Format( AV30LocalSiteURL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,60);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavLocalsiteurl_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavLocalsiteurl_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 RequiredDataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavServiceproviderentityid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavServiceproviderentityid_Internalname, context.GetMessage( "WWP_GAM_ServiceProviderEntityID", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 64,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavServiceproviderentityid_Internalname, AV37ServiceProviderEntityId, StringUtil.RTrim( context.localUtil.Format( AV37ServiceProviderEntityId, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,64);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavServiceproviderentityid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavServiceproviderentityid_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavIdentityproviderentityid_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavIdentityproviderentityid_Internalname, context.GetMessage( "WWP_GAM_IdentityProviderEntityId", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 69,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavIdentityproviderentityid_Internalname, AV38IdentityProviderEntityID, StringUtil.RTrim( context.localUtil.Format( AV38IdentityProviderEntityID, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,69);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavIdentityproviderentityid_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavIdentityproviderentityid_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavIsredirectbinding_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavIsredirectbinding_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 73,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavIsredirectbinding_Internalname, StringUtil.BoolToStr( AV20IsRedirectBinding), "", " ", 1, chkavIsredirectbinding.Enabled, "true", context.GetMessage( "WWP_GAM_ExecuteSAMLRequestsGET", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(73, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,73);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
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
            ucDvpanel_unnamedtable7.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable7_Internalname, sPrefix+"DVPANEL_UNNAMEDTABLE7Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"DVPANEL_UNNAMEDTABLE7Container"+"UnnamedTable7"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable7_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 RequiredDataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSamlendpointlocation_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSamlendpointlocation_Internalname, context.GetMessage( "WWP_GAM_SamlEndpointLocation", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 83,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSamlendpointlocation_Internalname, AV36SamlEndpointLocation, StringUtil.RTrim( context.localUtil.Format( AV36SamlEndpointLocation, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,83);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSamlendpointlocation_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSamlendpointlocation_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+cmbavNameidpolicyformat_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavNameidpolicyformat_Internalname, context.GetMessage( "WWP_GAM_NameIDPolicyFormat", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 87,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavNameidpolicyformat, cmbavNameidpolicyformat_Internalname, StringUtil.RTrim( AV69NameIdPolicyFormat), 1, cmbavNameidpolicyformat_Jsonclick, 0, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "char", "", 1, cmbavNameidpolicyformat.Enabled, 1, 0, 0, "em", 0, "", "", "Attribute", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,87);\"", "", true, 0, "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            cmbavNameidpolicyformat.CurrentValue = StringUtil.RTrim( AV69NameIdPolicyFormat);
            AssignProp(sPrefix, false, cmbavNameidpolicyformat_Internalname, "Values", (string)(cmbavNameidpolicyformat.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavForceauthn_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavForceauthn_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 92,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavForceauthn_Internalname, StringUtil.BoolToStr( AV15ForceAuthn), "", " ", 1, chkavForceauthn.Enabled, "true", context.GetMessage( "WWP_GAM_ForceAuthentication", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(92, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,92);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAuthncontext_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAuthncontext_Internalname, context.GetMessage( "WWP_GAM_AuthenticationContext", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 96,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAuthncontext_Internalname, StringUtil.RTrim( AV6AuthnContext), StringUtil.RTrim( context.localUtil.Format( AV6AuthnContext, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,96);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAuthncontext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAuthncontext_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            ucDvpanel_unnamedtable8.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable8_Internalname, sPrefix+"DVPANEL_UNNAMEDTABLE8Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"DVPANEL_UNNAMEDTABLE8Container"+"UnnamedTable8"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable8_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavDisablesinglelogout_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavDisablesinglelogout_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 105,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavDisablesinglelogout_Internalname, StringUtil.BoolToStr( AV67DisableSingleLogout), "", " ", 1, chkavDisablesinglelogout.Enabled, "true", context.GetMessage( "WWP_GAM_DisableSingleLogout", ""), StyleString, ClassString, "", "", TempTags+" onblur=\""+""+";gx.evt.onblur(this,105);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divSinglelogoutendpoint_cell_Internalname, 1, 0, "px", 0, "px", divSinglelogoutendpoint_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavSinglelogoutendpoint_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSinglelogoutendpoint_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSinglelogoutendpoint_Internalname, context.GetMessage( "WWP_GAM_SingleLogoutEndpoint", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 109,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSinglelogoutendpoint_Internalname, AV39SingleLogoutendpoint, StringUtil.RTrim( context.localUtil.Format( AV39SingleLogoutendpoint, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,109);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSinglelogoutendpoint_Jsonclick, 0, "Attribute", "", "", "", "", edtavSinglelogoutendpoint_Visible, edtavSinglelogoutendpoint_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"GXUITABSPANEL_TABSContainer"+"title2"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblCredentials_title_Internalname, context.GetMessage( "WWP_GAM_Credentials", ""), "", "", lblCredentials_title_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "Credentials") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"GXUITABSPANEL_TABSContainer"+"panel2"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
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
            ucDvpanel_unnamedtable4.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable4_Internalname, sPrefix+"DVPANEL_UNNAMEDTABLE4Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"DVPANEL_UNNAMEDTABLE4Container"+"UnnamedTable4"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable4_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeystpathcredential_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeystpathcredential_Internalname, context.GetMessage( "WWP_GAM_KeyStorePath", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 124,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeystpathcredential_Internalname, AV28KeyStPathCredential, StringUtil.RTrim( context.localUtil.Format( AV28KeyStPathCredential, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,124);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeystpathcredential_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeystpathcredential_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeystpwdcredential_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeystpwdcredential_Internalname, context.GetMessage( "WWP_GAM_KeyStorePassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 128,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeystpwdcredential_Internalname, StringUtil.RTrim( AV29KeyStPwdCredential), StringUtil.RTrim( context.localUtil.Format( AV29KeyStPwdCredential, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,128);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeystpwdcredential_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeystpwdcredential_Enabled, 1, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeyaliascredential_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeyaliascredential_Internalname, context.GetMessage( "WWP_GAM_CertificateAlias", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 133,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeyaliascredential_Internalname, StringUtil.RTrim( AV21KeyAliasCredential), StringUtil.RTrim( context.localUtil.Format( AV21KeyAliasCredential, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,133);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeyaliascredential_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeyaliascredential_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeystorecredential_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeystorecredential_Internalname, context.GetMessage( "WWP_GAM_KeyStoreFormat", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 137,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeystorecredential_Internalname, StringUtil.RTrim( AV24KeyStoreCredential), StringUtil.RTrim( context.localUtil.Format( AV24KeyStoreCredential, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,137);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeystorecredential_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeystorecredential_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable5.SetProperty("Width", Dvpanel_unnamedtable5_Width);
            ucDvpanel_unnamedtable5.SetProperty("AutoWidth", Dvpanel_unnamedtable5_Autowidth);
            ucDvpanel_unnamedtable5.SetProperty("AutoHeight", Dvpanel_unnamedtable5_Autoheight);
            ucDvpanel_unnamedtable5.SetProperty("Cls", Dvpanel_unnamedtable5_Cls);
            ucDvpanel_unnamedtable5.SetProperty("Title", Dvpanel_unnamedtable5_Title);
            ucDvpanel_unnamedtable5.SetProperty("Collapsible", Dvpanel_unnamedtable5_Collapsible);
            ucDvpanel_unnamedtable5.SetProperty("Collapsed", Dvpanel_unnamedtable5_Collapsed);
            ucDvpanel_unnamedtable5.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable5_Showcollapseicon);
            ucDvpanel_unnamedtable5.SetProperty("IconPosition", Dvpanel_unnamedtable5_Iconposition);
            ucDvpanel_unnamedtable5.SetProperty("AutoScroll", Dvpanel_unnamedtable5_Autoscroll);
            ucDvpanel_unnamedtable5.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable5_Internalname, sPrefix+"DVPANEL_UNNAMEDTABLE5Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"DVPANEL_UNNAMEDTABLE5Container"+"UnnamedTable5"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable5_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeystorefilepathtrustcred_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeystorefilepathtrustcred_Internalname, context.GetMessage( "WWP_GAM_TrustStorePath", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 146,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeystorefilepathtrustcred_Internalname, AV25KeyStoreFilePathTrustCred, StringUtil.RTrim( context.localUtil.Format( AV25KeyStoreFilePathTrustCred, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,146);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeystorefilepathtrustcred_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeystorefilepathtrustcred_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeystorepwdtrustcred_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeystorepwdtrustcred_Internalname, context.GetMessage( "WWP_GAM_TrustStorePassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 150,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeystorepwdtrustcred_Internalname, StringUtil.RTrim( AV26KeyStorePwdTrustCred), StringUtil.RTrim( context.localUtil.Format( AV26KeyStorePwdTrustCred, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,150);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeystorepwdtrustcred_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeystorepwdtrustcred_Enabled, 1, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeyaliastrustcred_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeyaliastrustcred_Internalname, context.GetMessage( "WWP_GAM_CertificateAlias", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 155,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeyaliastrustcred_Internalname, StringUtil.RTrim( AV22KeyAliasTrustCred), StringUtil.RTrim( context.localUtil.Format( AV22KeyAliasTrustCred, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,155);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeyaliastrustcred_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeyaliastrustcred_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavKeystoretrustcred_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavKeystoretrustcred_Internalname, context.GetMessage( "WWP_GAM_TrustStoreFormat", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 159,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavKeystoretrustcred_Internalname, StringUtil.RTrim( AV27KeyStoreTrustCred), StringUtil.RTrim( context.localUtil.Format( AV27KeyStoreTrustCred, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,159);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavKeystoretrustcred_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavKeystoretrustcred_Enabled, 1, "text", "", 80, "chr", 1, "row", 120, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionMedium", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"GXUITABSPANEL_TABSContainer"+"title3"+"\" style=\"display:none;\">") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblUserinfo_title_Internalname, context.GetMessage( "WWP_GAM_UserInformationResponse", ""), "", "", lblUserinfo_title_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", "", "display:none;", "div");
            context.WriteHtmlText( "UserInfo") ;
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"GXUITABSPANEL_TABSContainer"+"panel3"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuseremailtag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuseremailtag_Internalname, context.GetMessage( "WWP_GAM_UserEmailTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 169,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuseremailtag_Internalname, StringUtil.RTrim( AV44UserInfoResponseUserEmailTag), StringUtil.RTrim( context.localUtil.Format( AV44UserInfoResponseUserEmailTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,169);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuseremailtag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuseremailtag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuserexternalidtag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuserexternalidtag_Internalname, context.GetMessage( "WWP_GAM_ExternalIdTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 173,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuserexternalidtag_Internalname, StringUtil.RTrim( AV46UserInfoResponseUserExternalIdTag), StringUtil.RTrim( context.localUtil.Format( AV46UserInfoResponseUserExternalIdTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,173);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuserexternalidtag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuserexternalidtag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseusernametag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseusernametag_Internalname, context.GetMessage( "WWP_GAM_UserNameTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 178,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseusernametag_Internalname, StringUtil.RTrim( AV53UserInfoResponseUserNameTag), StringUtil.RTrim( context.localUtil.Format( AV53UserInfoResponseUserNameTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,178);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseusernametag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseusernametag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuserfirstnametag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuserfirstnametag_Internalname, context.GetMessage( "WWP_GAM_UserFirstNameTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 182,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuserfirstnametag_Internalname, StringUtil.RTrim( AV47UserInfoResponseUserFirstNameTag), StringUtil.RTrim( context.localUtil.Format( AV47UserInfoResponseUserFirstNameTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,182);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuserfirstnametag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuserfirstnametag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkavUserinforesponseuserlastnamegenauto_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavUserinforesponseuserlastnamegenauto_Internalname, " ", " AttributeCheckBoxLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 187,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavUserinforesponseuserlastnamegenauto_Internalname, StringUtil.BoolToStr( AV51UserInfoResponseUserLastNameGenAuto), "", " ", 1, chkavUserinforesponseuserlastnamegenauto.Enabled, "true", context.GetMessage( "WWP_GAM_GenerateautomaticLastName", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(187, this, 'true', 'false',"+"'"+sPrefix+"'"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,187);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuserlastnametag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuserlastnametag_Internalname, context.GetMessage( "WWP_GAM_UserLastNameTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 191,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuserlastnametag_Internalname, StringUtil.RTrim( AV52UserInfoResponseUserLastNameTag), StringUtil.RTrim( context.localUtil.Format( AV52UserInfoResponseUserLastNameTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,191);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuserlastnametag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuserlastnametag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseusergendertag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseusergendertag_Internalname, context.GetMessage( "WWP_GAM_UserGenderTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 196,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseusergendertag_Internalname, StringUtil.RTrim( AV48UserInfoResponseUserGenderTag), StringUtil.RTrim( context.localUtil.Format( AV48UserInfoResponseUserGenderTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,196);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseusergendertag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseusergendertag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseusergendervalues_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseusergendervalues_Internalname, context.GetMessage( "WWP_GAM_UserGenderValues", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 200,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseusergendervalues_Internalname, AV49UserInfoResponseUserGenderValues, StringUtil.RTrim( context.localUtil.Format( AV49UserInfoResponseUserGenderValues, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,200);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseusergendervalues_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseusergendervalues_Enabled, 1, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMPropertyValue", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuserbirthdaytag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuserbirthdaytag_Internalname, context.GetMessage( "WWP_GAM_UserBirthdayTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 205,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuserbirthdaytag_Internalname, StringUtil.RTrim( AV43UserInfoResponseUserBirthdayTag), StringUtil.RTrim( context.localUtil.Format( AV43UserInfoResponseUserBirthdayTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,205);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuserbirthdaytag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuserbirthdaytag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuserurlimagetag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuserurlimagetag_Internalname, context.GetMessage( "WWP_GAM_UserURLImageTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 209,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuserurlimagetag_Internalname, StringUtil.RTrim( AV55UserInfoResponseUserURLImageTag), StringUtil.RTrim( context.localUtil.Format( AV55UserInfoResponseUserURLImageTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,209);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuserurlimagetag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuserurlimagetag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuserurlprofiletag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuserurlprofiletag_Internalname, context.GetMessage( "WWP_GAM_UserURLProfileTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 214,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuserurlprofiletag_Internalname, StringUtil.RTrim( AV56UserInfoResponseUserURLProfileTag), StringUtil.RTrim( context.localUtil.Format( AV56UserInfoResponseUserURLProfileTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,214);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuserurlprofiletag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuserurlprofiletag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseuserlanguagetag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseuserlanguagetag_Internalname, context.GetMessage( "WWP_GAM_UserLanguageTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 218,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseuserlanguagetag_Internalname, StringUtil.RTrim( AV50UserInfoResponseUserLanguageTag), StringUtil.RTrim( context.localUtil.Format( AV50UserInfoResponseUserLanguageTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,218);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseuserlanguagetag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseuserlanguagetag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseusertimezonetag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseusertimezonetag_Internalname, context.GetMessage( "WWP_GAM_UserTimezoneTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 223,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseusertimezonetag_Internalname, StringUtil.RTrim( AV54UserInfoResponseUserTimeZoneTag), StringUtil.RTrim( context.localUtil.Format( AV54UserInfoResponseUserTimeZoneTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,223);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseusertimezonetag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseusertimezonetag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserinforesponseerrordescriptiontag_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserinforesponseerrordescriptiontag_Internalname, context.GetMessage( "WWP_GAM_ErrorDescriptionTag", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 227,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserinforesponseerrordescriptiontag_Internalname, StringUtil.RTrim( AV42UserInfoResponseErrorDescriptionTag), StringUtil.RTrim( context.localUtil.Format( AV42UserInfoResponseErrorDescriptionTag, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,227);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserinforesponseerrordescriptiontag_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUserinforesponseerrordescriptiontag_Enabled, 1, "text", "", 60, "chr", 1, "row", 60, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionShort", "start", true, "", "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable2.SetProperty("Width", Dvpanel_unnamedtable2_Width);
            ucDvpanel_unnamedtable2.SetProperty("AutoWidth", Dvpanel_unnamedtable2_Autowidth);
            ucDvpanel_unnamedtable2.SetProperty("AutoHeight", Dvpanel_unnamedtable2_Autoheight);
            ucDvpanel_unnamedtable2.SetProperty("Cls", Dvpanel_unnamedtable2_Cls);
            ucDvpanel_unnamedtable2.SetProperty("Title", Dvpanel_unnamedtable2_Title);
            ucDvpanel_unnamedtable2.SetProperty("Collapsible", Dvpanel_unnamedtable2_Collapsible);
            ucDvpanel_unnamedtable2.SetProperty("Collapsed", Dvpanel_unnamedtable2_Collapsed);
            ucDvpanel_unnamedtable2.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable2_Showcollapseicon);
            ucDvpanel_unnamedtable2.SetProperty("IconPosition", Dvpanel_unnamedtable2_Iconposition);
            ucDvpanel_unnamedtable2.SetProperty("AutoScroll", Dvpanel_unnamedtable2_Autoscroll);
            ucDvpanel_unnamedtable2.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable2_Internalname, sPrefix+"DVPANEL_UNNAMEDTABLE2Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+sPrefix+"DVPANEL_UNNAMEDTABLE2Container"+"UnnamedTable2"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SectionGrid GridNoBorderCell CellMarginTop HasGridEmpowerer", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridContainer.SetWrapped(nGXWrapped);
            StartGridControl235( ) ;
         }
         if ( wbEnd == 235 )
         {
            wbEnd = 0;
            nRC_GXsfl_235 = (int)(nGXsfl_235_idx-1);
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               GridContainer.AddObjectProperty("GRID_nEOF", GRID_nEOF);
               GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Grid", GridContainer, subGrid_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData", GridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData"+"V", GridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 241,'" + sPrefix + "',false,'',0)\"";
            ClassString = "ButtonAddNewRow";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnadd_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(235), 3, 0)+","+"null"+");", context.GetMessage( "GXM_insert", ""), bttBtnadd_Jsonclick, 5, context.GetMessage( "GXM_insert", ""), "", StyleString, ClassString, bttBtnadd_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOADD\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</div>") ;
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 246,'" + sPrefix + "',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(235), 3, 0)+","+"null"+");", bttBtnenter_Caption, bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtnenter_Visible, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 248,'" + sPrefix + "',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(235), 3, 0)+","+"null"+");", context.GetMessage( "GX_BtnCancel", ""), bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMWCAuthenticationTypeEntrySaml20.htm");
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
            /* User Defined Control */
            ucGrid_empowerer.Render(context, "wwp.gridempowerer", Grid_empowerer_Internalname, sPrefix+"GRID_EMPOWERERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 235 )
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
                  GridContainer.AddObjectProperty("GRID_nEOF", GRID_nEOF);
                  GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Grid", GridContainer, subGrid_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData", GridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridContainerData"+"V", GridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START1H2( )
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
            Form.Meta.addItem("description", context.GetMessage( "GAMWCAuthentication Type Entry Saml20", ""), 0) ;
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
               STRUP1H0( ) ;
            }
         }
      }

      protected void WS1H2( )
      {
         START1H2( ) ;
         EVT1H2( ) ;
      }

      protected void EVT1H2( )
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
                                 STRUP1H0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'DOADD'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoAdd' */
                                    E111H2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1H0( ) ;
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
                                          E121H2 ();
                                       }
                                       dynload_actions( ) ;
                                    }
                                 }
                              }
                              /* No code required for Cancel button. It is implemented as the Reset button. */
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1H0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavDynamicpropname_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1H0( ) ;
                              }
                              sEvt = cgiGet( sPrefix+"GRIDPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subgrid_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subgrid_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subgrid_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subgrid_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "GRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 21), "VDELETEPROPERTY.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 21), "VDELETEPROPERTY.CLICK") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP1H0( ) ;
                              }
                              nGXsfl_235_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_235_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_235_idx), 4, 0), 4, "0");
                              SubsflControlProps_2352( ) ;
                              AV11DynamicPropName = cgiGet( edtavDynamicpropname_Internalname);
                              AssignAttri(sPrefix, false, edtavDynamicpropname_Internalname, AV11DynamicPropName);
                              AV12DynamicPropTag = cgiGet( edtavDynamicproptag_Internalname);
                              AssignAttri(sPrefix, false, edtavDynamicproptag_Internalname, AV12DynamicPropTag);
                              AV65DeleteProperty = cgiGet( edtavDeleteproperty_Internalname);
                              AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty)) ? AV75Deleteproperty_GXI : context.convertURL( context.PathToRelativeUrl( AV65DeleteProperty))), !bGXsfl_235_Refreshing);
                              AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "SrcSet", context.GetImageSrcSet( AV65DeleteProperty), true);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavDynamicpropname_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E131H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavDynamicpropname_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Grid.Load */
                                          E141H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavDynamicpropname_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Refresh */
                                          E151H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VDELETEPROPERTY.CLICK") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavDynamicpropname_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          E161H2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP1H0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavDynamicpropname_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
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

      protected void WE1H2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm1H2( ) ;
            }
         }
      }

      protected void PA1H2( )
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
                  if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamwcauthenticationtypeentrysaml20.aspx")), "gamwcauthenticationtypeentrysaml20.aspx") == 0 ) )
                  {
                     SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamwcauthenticationtypeentrysaml20.aspx")))) ;
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

      protected void gxnrGrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_2352( ) ;
         while ( nGXsfl_235_idx <= nRC_GXsfl_235 )
         {
            sendrow_2352( ) ;
            nGXsfl_235_idx = ((subGrid_Islastpage==1)&&(nGXsfl_235_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_235_idx+1);
            sGXsfl_235_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_235_idx), 4, 0), 4, "0");
            SubsflControlProps_2352( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridContainer)) ;
         /* End function gxnrGrid_newrow */
      }

      protected void gxgrGrid_refresh( int subGrid_Rows ,
                                       string Gx_mode ,
                                       bool AV19IsEnable ,
                                       bool AV20IsRedirectBinding ,
                                       bool AV15ForceAuthn ,
                                       bool AV67DisableSingleLogout ,
                                       bool AV51UserInfoResponseUserLastNameGenAuto ,
                                       string AV45UserInfoResponseUserErrorDescriptionTag ,
                                       string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RF1H2( ) ;
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
         if ( cmbavFunctionid.ItemCount > 0 )
         {
            AV16FunctionId = cmbavFunctionid.getValidValue(AV16FunctionId);
            AssignAttri(sPrefix, false, "AV16FunctionId", AV16FunctionId);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavFunctionid.CurrentValue = StringUtil.RTrim( AV16FunctionId);
            AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Values", cmbavFunctionid.ToJavascriptSource(), true);
         }
         AV19IsEnable = StringUtil.StrToBool( StringUtil.BoolToStr( AV19IsEnable));
         AssignAttri(sPrefix, false, "AV19IsEnable", AV19IsEnable);
         AV20IsRedirectBinding = StringUtil.StrToBool( StringUtil.BoolToStr( AV20IsRedirectBinding));
         AssignAttri(sPrefix, false, "AV20IsRedirectBinding", AV20IsRedirectBinding);
         if ( cmbavNameidpolicyformat.ItemCount > 0 )
         {
            AV69NameIdPolicyFormat = cmbavNameidpolicyformat.getValidValue(AV69NameIdPolicyFormat);
            AssignAttri(sPrefix, false, "AV69NameIdPolicyFormat", AV69NameIdPolicyFormat);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavNameidpolicyformat.CurrentValue = StringUtil.RTrim( AV69NameIdPolicyFormat);
            AssignProp(sPrefix, false, cmbavNameidpolicyformat_Internalname, "Values", cmbavNameidpolicyformat.ToJavascriptSource(), true);
         }
         AV15ForceAuthn = StringUtil.StrToBool( StringUtil.BoolToStr( AV15ForceAuthn));
         AssignAttri(sPrefix, false, "AV15ForceAuthn", AV15ForceAuthn);
         AV67DisableSingleLogout = StringUtil.StrToBool( StringUtil.BoolToStr( AV67DisableSingleLogout));
         AssignAttri(sPrefix, false, "AV67DisableSingleLogout", AV67DisableSingleLogout);
         AV51UserInfoResponseUserLastNameGenAuto = StringUtil.StrToBool( StringUtil.BoolToStr( AV51UserInfoResponseUserLastNameGenAuto));
         AssignAttri(sPrefix, false, "AV51UserInfoResponseUserLastNameGenAuto", AV51UserInfoResponseUserLastNameGenAuto);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF1H2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF1H2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 235;
         /* Execute user event: Refresh */
         E151H2 ();
         nGXsfl_235_idx = 1;
         sGXsfl_235_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_235_idx), 4, 0), 4, "0");
         SubsflControlProps_2352( ) ;
         bGXsfl_235_Refreshing = true;
         GridContainer.AddObjectProperty("GridName", "Grid");
         GridContainer.AddObjectProperty("CmpContext", sPrefix);
         GridContainer.AddObjectProperty("InMasterPage", "false");
         GridContainer.AddObjectProperty("Class", "GridNoBorder WorkWith");
         GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
         GridContainer.PageSize = subGrid_fnc_Recordsperpage( );
         if ( subGrid_Islastpage != 0 )
         {
            GRID_nFirstRecordOnPage = (long)(subGrid_fnc_Recordcount( )-subGrid_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
            GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_2352( ) ;
            /* Execute user event: Grid.Load */
            E141H2 ();
            if ( ( subGrid_Islastpage == 0 ) && ( GRID_nCurrentRecord > 0 ) && ( GRID_nGridOutOfScope == 0 ) && ( nGXsfl_235_idx == 1 ) )
            {
               GRID_nCurrentRecord = 0;
               GRID_nGridOutOfScope = 1;
               subgrid_firstpage( ) ;
               /* Execute user event: Grid.Load */
               E141H2 ();
            }
            wbEnd = 235;
            WB1H0( ) ;
         }
         bGXsfl_235_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes1H2( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG", StringUtil.RTrim( AV45UserInfoResponseUserErrorDescriptionTag));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV45UserInfoResponseUserErrorDescriptionTag, "")), context));
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
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, Gx_mode, AV19IsEnable, AV20IsRedirectBinding, AV15ForceAuthn, AV67DisableSingleLogout, AV51UserInfoResponseUserLastNameGenAuto, AV45UserInfoResponseUserErrorDescriptionTag, sPrefix) ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, Gx_mode, AV19IsEnable, AV20IsRedirectBinding, AV15ForceAuthn, AV67DisableSingleLogout, AV51UserInfoResponseUserLastNameGenAuto, AV45UserInfoResponseUserErrorDescriptionTag, sPrefix) ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, Gx_mode, AV19IsEnable, AV20IsRedirectBinding, AV15ForceAuthn, AV67DisableSingleLogout, AV51UserInfoResponseUserLastNameGenAuto, AV45UserInfoResponseUserErrorDescriptionTag, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         subGrid_Islastpage = 1;
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, Gx_mode, AV19IsEnable, AV20IsRedirectBinding, AV15ForceAuthn, AV67DisableSingleLogout, AV51UserInfoResponseUserLastNameGenAuto, AV45UserInfoResponseUserErrorDescriptionTag, sPrefix) ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, Gx_mode, AV19IsEnable, AV20IsRedirectBinding, AV15ForceAuthn, AV67DisableSingleLogout, AV51UserInfoResponseUserLastNameGenAuto, AV45UserInfoResponseUserErrorDescriptionTag, sPrefix) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1H0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E131H2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            nRC_GXsfl_235 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_235"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            wcpOGx_mode = cgiGet( sPrefix+"wcpOGx_mode");
            wcpOAV32Name = cgiGet( sPrefix+"wcpOAV32Name");
            wcpOAV41TypeId = cgiGet( sPrefix+"wcpOAV41TypeId");
            GRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGrid_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"subGrid_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Dvpanel_unnamedtable7_Width = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Width");
            Dvpanel_unnamedtable7_Autowidth = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Autowidth"));
            Dvpanel_unnamedtable7_Autoheight = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Autoheight"));
            Dvpanel_unnamedtable7_Cls = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Cls");
            Dvpanel_unnamedtable7_Title = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Title");
            Dvpanel_unnamedtable7_Collapsible = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Collapsible"));
            Dvpanel_unnamedtable7_Collapsed = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Collapsed"));
            Dvpanel_unnamedtable7_Showcollapseicon = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Showcollapseicon"));
            Dvpanel_unnamedtable7_Iconposition = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Iconposition");
            Dvpanel_unnamedtable7_Autoscroll = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE7_Autoscroll"));
            Dvpanel_unnamedtable8_Width = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Width");
            Dvpanel_unnamedtable8_Autowidth = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Autowidth"));
            Dvpanel_unnamedtable8_Autoheight = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Autoheight"));
            Dvpanel_unnamedtable8_Cls = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Cls");
            Dvpanel_unnamedtable8_Title = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Title");
            Dvpanel_unnamedtable8_Collapsible = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Collapsible"));
            Dvpanel_unnamedtable8_Collapsed = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Collapsed"));
            Dvpanel_unnamedtable8_Showcollapseicon = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Showcollapseicon"));
            Dvpanel_unnamedtable8_Iconposition = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Iconposition");
            Dvpanel_unnamedtable8_Autoscroll = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE8_Autoscroll"));
            Dvpanel_unnamedtable4_Width = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Width");
            Dvpanel_unnamedtable4_Autowidth = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Autowidth"));
            Dvpanel_unnamedtable4_Autoheight = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Autoheight"));
            Dvpanel_unnamedtable4_Cls = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Cls");
            Dvpanel_unnamedtable4_Title = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Title");
            Dvpanel_unnamedtable4_Collapsible = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Collapsible"));
            Dvpanel_unnamedtable4_Collapsed = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Collapsed"));
            Dvpanel_unnamedtable4_Showcollapseicon = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Showcollapseicon"));
            Dvpanel_unnamedtable4_Iconposition = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Iconposition");
            Dvpanel_unnamedtable4_Autoscroll = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE4_Autoscroll"));
            Dvpanel_unnamedtable5_Width = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Width");
            Dvpanel_unnamedtable5_Autowidth = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Autowidth"));
            Dvpanel_unnamedtable5_Autoheight = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Autoheight"));
            Dvpanel_unnamedtable5_Cls = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Cls");
            Dvpanel_unnamedtable5_Title = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Title");
            Dvpanel_unnamedtable5_Collapsible = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Collapsible"));
            Dvpanel_unnamedtable5_Collapsed = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Collapsed"));
            Dvpanel_unnamedtable5_Showcollapseicon = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Showcollapseicon"));
            Dvpanel_unnamedtable5_Iconposition = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Iconposition");
            Dvpanel_unnamedtable5_Autoscroll = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE5_Autoscroll"));
            Dvpanel_unnamedtable2_Width = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Width");
            Dvpanel_unnamedtable2_Autowidth = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Autowidth"));
            Dvpanel_unnamedtable2_Autoheight = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Autoheight"));
            Dvpanel_unnamedtable2_Cls = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Cls");
            Dvpanel_unnamedtable2_Title = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Title");
            Dvpanel_unnamedtable2_Collapsible = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Collapsible"));
            Dvpanel_unnamedtable2_Collapsed = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Collapsed"));
            Dvpanel_unnamedtable2_Showcollapseicon = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Showcollapseicon"));
            Dvpanel_unnamedtable2_Iconposition = cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Iconposition");
            Dvpanel_unnamedtable2_Autoscroll = StringUtil.StrToBool( cgiGet( sPrefix+"DVPANEL_UNNAMEDTABLE2_Autoscroll"));
            Gxuitabspanel_tabs_Pagecount = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"GXUITABSPANEL_TABS_Pagecount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gxuitabspanel_tabs_Class = cgiGet( sPrefix+"GXUITABSPANEL_TABS_Class");
            Gxuitabspanel_tabs_Historymanagement = StringUtil.StrToBool( cgiGet( sPrefix+"GXUITABSPANEL_TABS_Historymanagement"));
            Grid_empowerer_Gridinternalname = cgiGet( sPrefix+"GRID_EMPOWERER_Gridinternalname");
            /* Read variables values. */
            if ( StringUtil.Len( sPrefix) == 0 )
            {
               AV32Name = cgiGet( edtavName_Internalname);
               AssignAttri(sPrefix, false, "AV32Name", AV32Name);
            }
            cmbavFunctionid.Name = cmbavFunctionid_Internalname;
            cmbavFunctionid.CurrentValue = cgiGet( cmbavFunctionid_Internalname);
            AV16FunctionId = cgiGet( cmbavFunctionid_Internalname);
            AssignAttri(sPrefix, false, "AV16FunctionId", AV16FunctionId);
            AV10Dsc = cgiGet( edtavDsc_Internalname);
            AssignAttri(sPrefix, false, "AV10Dsc", AV10Dsc);
            AV19IsEnable = StringUtil.StrToBool( cgiGet( chkavIsenable_Internalname));
            AssignAttri(sPrefix, false, "AV19IsEnable", AV19IsEnable);
            AV18Impersonate = cgiGet( edtavImpersonate_Internalname);
            AssignAttri(sPrefix, false, "AV18Impersonate", AV18Impersonate);
            AV40SmallImageName = cgiGet( edtavSmallimagename_Internalname);
            AssignAttri(sPrefix, false, "AV40SmallImageName", AV40SmallImageName);
            AV7BigImageName = cgiGet( edtavBigimagename_Internalname);
            AssignAttri(sPrefix, false, "AV7BigImageName", AV7BigImageName);
            AV30LocalSiteURL = cgiGet( edtavLocalsiteurl_Internalname);
            AssignAttri(sPrefix, false, "AV30LocalSiteURL", AV30LocalSiteURL);
            AV37ServiceProviderEntityId = cgiGet( edtavServiceproviderentityid_Internalname);
            AssignAttri(sPrefix, false, "AV37ServiceProviderEntityId", AV37ServiceProviderEntityId);
            AV38IdentityProviderEntityID = cgiGet( edtavIdentityproviderentityid_Internalname);
            AssignAttri(sPrefix, false, "AV38IdentityProviderEntityID", AV38IdentityProviderEntityID);
            AV20IsRedirectBinding = StringUtil.StrToBool( cgiGet( chkavIsredirectbinding_Internalname));
            AssignAttri(sPrefix, false, "AV20IsRedirectBinding", AV20IsRedirectBinding);
            AV36SamlEndpointLocation = cgiGet( edtavSamlendpointlocation_Internalname);
            AssignAttri(sPrefix, false, "AV36SamlEndpointLocation", AV36SamlEndpointLocation);
            cmbavNameidpolicyformat.Name = cmbavNameidpolicyformat_Internalname;
            cmbavNameidpolicyformat.CurrentValue = cgiGet( cmbavNameidpolicyformat_Internalname);
            AV69NameIdPolicyFormat = cgiGet( cmbavNameidpolicyformat_Internalname);
            AssignAttri(sPrefix, false, "AV69NameIdPolicyFormat", AV69NameIdPolicyFormat);
            AV15ForceAuthn = StringUtil.StrToBool( cgiGet( chkavForceauthn_Internalname));
            AssignAttri(sPrefix, false, "AV15ForceAuthn", AV15ForceAuthn);
            AV6AuthnContext = cgiGet( edtavAuthncontext_Internalname);
            AssignAttri(sPrefix, false, "AV6AuthnContext", AV6AuthnContext);
            AV67DisableSingleLogout = StringUtil.StrToBool( cgiGet( chkavDisablesinglelogout_Internalname));
            AssignAttri(sPrefix, false, "AV67DisableSingleLogout", AV67DisableSingleLogout);
            AV39SingleLogoutendpoint = cgiGet( edtavSinglelogoutendpoint_Internalname);
            AssignAttri(sPrefix, false, "AV39SingleLogoutendpoint", AV39SingleLogoutendpoint);
            AV28KeyStPathCredential = cgiGet( edtavKeystpathcredential_Internalname);
            AssignAttri(sPrefix, false, "AV28KeyStPathCredential", AV28KeyStPathCredential);
            AV29KeyStPwdCredential = cgiGet( edtavKeystpwdcredential_Internalname);
            AssignAttri(sPrefix, false, "AV29KeyStPwdCredential", AV29KeyStPwdCredential);
            AV21KeyAliasCredential = cgiGet( edtavKeyaliascredential_Internalname);
            AssignAttri(sPrefix, false, "AV21KeyAliasCredential", AV21KeyAliasCredential);
            AV24KeyStoreCredential = cgiGet( edtavKeystorecredential_Internalname);
            AssignAttri(sPrefix, false, "AV24KeyStoreCredential", AV24KeyStoreCredential);
            AV25KeyStoreFilePathTrustCred = cgiGet( edtavKeystorefilepathtrustcred_Internalname);
            AssignAttri(sPrefix, false, "AV25KeyStoreFilePathTrustCred", AV25KeyStoreFilePathTrustCred);
            AV26KeyStorePwdTrustCred = cgiGet( edtavKeystorepwdtrustcred_Internalname);
            AssignAttri(sPrefix, false, "AV26KeyStorePwdTrustCred", AV26KeyStorePwdTrustCred);
            AV22KeyAliasTrustCred = cgiGet( edtavKeyaliastrustcred_Internalname);
            AssignAttri(sPrefix, false, "AV22KeyAliasTrustCred", AV22KeyAliasTrustCred);
            AV27KeyStoreTrustCred = cgiGet( edtavKeystoretrustcred_Internalname);
            AssignAttri(sPrefix, false, "AV27KeyStoreTrustCred", AV27KeyStoreTrustCred);
            AV44UserInfoResponseUserEmailTag = cgiGet( edtavUserinforesponseuseremailtag_Internalname);
            AssignAttri(sPrefix, false, "AV44UserInfoResponseUserEmailTag", AV44UserInfoResponseUserEmailTag);
            AV46UserInfoResponseUserExternalIdTag = cgiGet( edtavUserinforesponseuserexternalidtag_Internalname);
            AssignAttri(sPrefix, false, "AV46UserInfoResponseUserExternalIdTag", AV46UserInfoResponseUserExternalIdTag);
            AV53UserInfoResponseUserNameTag = cgiGet( edtavUserinforesponseusernametag_Internalname);
            AssignAttri(sPrefix, false, "AV53UserInfoResponseUserNameTag", AV53UserInfoResponseUserNameTag);
            AV47UserInfoResponseUserFirstNameTag = cgiGet( edtavUserinforesponseuserfirstnametag_Internalname);
            AssignAttri(sPrefix, false, "AV47UserInfoResponseUserFirstNameTag", AV47UserInfoResponseUserFirstNameTag);
            AV51UserInfoResponseUserLastNameGenAuto = StringUtil.StrToBool( cgiGet( chkavUserinforesponseuserlastnamegenauto_Internalname));
            AssignAttri(sPrefix, false, "AV51UserInfoResponseUserLastNameGenAuto", AV51UserInfoResponseUserLastNameGenAuto);
            AV52UserInfoResponseUserLastNameTag = cgiGet( edtavUserinforesponseuserlastnametag_Internalname);
            AssignAttri(sPrefix, false, "AV52UserInfoResponseUserLastNameTag", AV52UserInfoResponseUserLastNameTag);
            AV48UserInfoResponseUserGenderTag = cgiGet( edtavUserinforesponseusergendertag_Internalname);
            AssignAttri(sPrefix, false, "AV48UserInfoResponseUserGenderTag", AV48UserInfoResponseUserGenderTag);
            AV49UserInfoResponseUserGenderValues = cgiGet( edtavUserinforesponseusergendervalues_Internalname);
            AssignAttri(sPrefix, false, "AV49UserInfoResponseUserGenderValues", AV49UserInfoResponseUserGenderValues);
            AV43UserInfoResponseUserBirthdayTag = cgiGet( edtavUserinforesponseuserbirthdaytag_Internalname);
            AssignAttri(sPrefix, false, "AV43UserInfoResponseUserBirthdayTag", AV43UserInfoResponseUserBirthdayTag);
            AV55UserInfoResponseUserURLImageTag = cgiGet( edtavUserinforesponseuserurlimagetag_Internalname);
            AssignAttri(sPrefix, false, "AV55UserInfoResponseUserURLImageTag", AV55UserInfoResponseUserURLImageTag);
            AV56UserInfoResponseUserURLProfileTag = cgiGet( edtavUserinforesponseuserurlprofiletag_Internalname);
            AssignAttri(sPrefix, false, "AV56UserInfoResponseUserURLProfileTag", AV56UserInfoResponseUserURLProfileTag);
            AV50UserInfoResponseUserLanguageTag = cgiGet( edtavUserinforesponseuserlanguagetag_Internalname);
            AssignAttri(sPrefix, false, "AV50UserInfoResponseUserLanguageTag", AV50UserInfoResponseUserLanguageTag);
            AV54UserInfoResponseUserTimeZoneTag = cgiGet( edtavUserinforesponseusertimezonetag_Internalname);
            AssignAttri(sPrefix, false, "AV54UserInfoResponseUserTimeZoneTag", AV54UserInfoResponseUserTimeZoneTag);
            AV42UserInfoResponseErrorDescriptionTag = cgiGet( edtavUserinforesponseerrordescriptiontag_Internalname);
            AssignAttri(sPrefix, false, "AV42UserInfoResponseErrorDescriptionTag", AV42UserInfoResponseErrorDescriptionTag);
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
         E131H2 ();
         if (returnInSub) return;
      }

      protected void E131H2( )
      {
         /* Start Routine */
         returnInSub = false;
         /* Execute user subroutine: 'INITAUTHENTICATIONSAML' */
         S112 ();
         if (returnInSub) return;
         AV16FunctionId = "OnlyAuthentication";
         AssignAttri(sPrefix, false, "AV16FunctionId", AV16FunctionId);
         cmbavFunctionid.Enabled = 0;
         AssignProp(sPrefix, false, cmbavFunctionid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavFunctionid.Enabled), 5, 0), true);
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            if ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 )
            {
               bttBtnenter_Visible = 0;
               AssignProp(sPrefix, false, bttBtnenter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnenter_Visible), 5, 0), true);
            }
            bttBtnadd_Visible = 0;
            AssignProp(sPrefix, false, bttBtnadd_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnadd_Visible), 5, 0), true);
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
            edtavImpersonate_Enabled = 0;
            AssignProp(sPrefix, false, edtavImpersonate_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavImpersonate_Enabled), 5, 0), true);
            bttBtnenter_Caption = context.GetMessage( "WWP_GAM_Delete", "");
            AssignProp(sPrefix, false, bttBtnenter_Internalname, "Caption", bttBtnenter_Caption, true);
            edtavServiceproviderentityid_Enabled = 0;
            AssignProp(sPrefix, false, edtavServiceproviderentityid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavServiceproviderentityid_Enabled), 5, 0), true);
            edtavIdentityproviderentityid_Enabled = 0;
            AssignProp(sPrefix, false, edtavIdentityproviderentityid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavIdentityproviderentityid_Enabled), 5, 0), true);
            edtavSamlendpointlocation_Enabled = 0;
            AssignProp(sPrefix, false, edtavSamlendpointlocation_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSamlendpointlocation_Enabled), 5, 0), true);
            edtavSinglelogoutendpoint_Enabled = 0;
            AssignProp(sPrefix, false, edtavSinglelogoutendpoint_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavSinglelogoutendpoint_Enabled), 5, 0), true);
            chkavDisablesinglelogout.Enabled = 0;
            AssignProp(sPrefix, false, chkavDisablesinglelogout_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavDisablesinglelogout.Enabled), 5, 0), true);
            edtavLocalsiteurl_Enabled = 0;
            AssignProp(sPrefix, false, edtavLocalsiteurl_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavLocalsiteurl_Enabled), 5, 0), true);
            edtavKeystpathcredential_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeystpathcredential_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeystpathcredential_Enabled), 5, 0), true);
            edtavKeystpwdcredential_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeystpwdcredential_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeystpwdcredential_Enabled), 5, 0), true);
            edtavKeyaliascredential_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeyaliascredential_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeyaliascredential_Enabled), 5, 0), true);
            edtavKeystorecredential_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeystorecredential_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeystorecredential_Enabled), 5, 0), true);
            edtavKeystorefilepathtrustcred_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeystorefilepathtrustcred_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeystorefilepathtrustcred_Enabled), 5, 0), true);
            edtavKeystorepwdtrustcred_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeystorepwdtrustcred_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeystorepwdtrustcred_Enabled), 5, 0), true);
            edtavKeyaliastrustcred_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeyaliastrustcred_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeyaliastrustcred_Enabled), 5, 0), true);
            edtavKeystoretrustcred_Enabled = 0;
            AssignProp(sPrefix, false, edtavKeystoretrustcred_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavKeystoretrustcred_Enabled), 5, 0), true);
            chkavIsredirectbinding.Enabled = 0;
            AssignProp(sPrefix, false, chkavIsredirectbinding_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavIsredirectbinding.Enabled), 5, 0), true);
            chkavForceauthn.Enabled = 0;
            AssignProp(sPrefix, false, chkavForceauthn_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavForceauthn.Enabled), 5, 0), true);
            edtavAuthncontext_Enabled = 0;
            AssignProp(sPrefix, false, edtavAuthncontext_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavAuthncontext_Enabled), 5, 0), true);
            cmbavNameidpolicyformat.Enabled = 0;
            AssignProp(sPrefix, false, cmbavNameidpolicyformat_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(cmbavNameidpolicyformat.Enabled), 5, 0), true);
            edtavUserinforesponseuserbirthdaytag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuserbirthdaytag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuserbirthdaytag_Enabled), 5, 0), true);
            edtavUserinforesponseuseremailtag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuseremailtag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuseremailtag_Enabled), 5, 0), true);
            edtavUserinforesponseuserexternalidtag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuserexternalidtag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuserexternalidtag_Enabled), 5, 0), true);
            edtavUserinforesponseuserfirstnametag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuserfirstnametag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuserfirstnametag_Enabled), 5, 0), true);
            edtavUserinforesponseusergendertag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseusergendertag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseusergendertag_Enabled), 5, 0), true);
            edtavUserinforesponseusergendervalues_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseusergendervalues_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseusergendervalues_Enabled), 5, 0), true);
            edtavUserinforesponseuserlanguagetag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuserlanguagetag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuserlanguagetag_Enabled), 5, 0), true);
            chkavUserinforesponseuserlastnamegenauto.Enabled = 0;
            AssignProp(sPrefix, false, chkavUserinforesponseuserlastnamegenauto_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkavUserinforesponseuserlastnamegenauto.Enabled), 5, 0), true);
            edtavUserinforesponseuserlastnametag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuserlastnametag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuserlastnametag_Enabled), 5, 0), true);
            edtavUserinforesponseusernametag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseusernametag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseusernametag_Enabled), 5, 0), true);
            edtavUserinforesponseusertimezonetag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseusertimezonetag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseusertimezonetag_Enabled), 5, 0), true);
            edtavUserinforesponseuserurlimagetag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuserurlimagetag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuserurlimagetag_Enabled), 5, 0), true);
            edtavUserinforesponseuserurlprofiletag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseuserurlprofiletag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseuserurlprofiletag_Enabled), 5, 0), true);
            edtavUserinforesponseerrordescriptiontag_Enabled = 0;
            AssignProp(sPrefix, false, edtavUserinforesponseerrordescriptiontag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUserinforesponseerrordescriptiontag_Enabled), 5, 0), true);
         }
         else
         {
            if ( StringUtil.StrCmp(Gx_mode, "INS") == 0 )
            {
               edtavName_Enabled = 1;
               AssignProp(sPrefix, false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
            }
            else
            {
               edtavName_Enabled = 0;
               AssignProp(sPrefix, false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), true);
            }
         }
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp(sPrefix, false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S122 ();
         if (returnInSub) return;
         Grid_empowerer_Gridinternalname = subGrid_Internalname;
         ucGrid_empowerer.SendProperty(context, sPrefix, false, Grid_empowerer_Internalname, "GridInternalName", Grid_empowerer_Gridinternalname);
         subGrid_Rows = 10;
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
      }

      private void E141H2( )
      {
         /* Grid_Load Routine */
         returnInSub = false;
         AV74GXV1 = 1;
         while ( AV74GXV1 <= AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserproperties.Count )
         {
            AV17GAMPropertySimple = ((GeneXus.Programs.genexussecurity.SdtGAMPropertySimple)AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserproperties.Item(AV74GXV1));
            edtavDeleteproperty_gximage = "ActionDelete";
            AV65DeleteProperty = context.GetImagePath( "7695fe89-52c9-4b7e-871e-0e11548f823e", "", context.GetTheme( ));
            AssignAttri(sPrefix, false, edtavDeleteproperty_Internalname, AV65DeleteProperty);
            AV75Deleteproperty_GXI = GXDbFile.PathToUrl( context.GetImagePath( "7695fe89-52c9-4b7e-871e-0e11548f823e", "", context.GetTheme( )), context);
            edtavDeleteproperty_Tooltiptext = "";
            AV11DynamicPropName = AV17GAMPropertySimple.gxTpr_Id;
            AssignAttri(sPrefix, false, edtavDynamicpropname_Internalname, AV11DynamicPropName);
            AV12DynamicPropTag = AV17GAMPropertySimple.gxTpr_Value;
            AssignAttri(sPrefix, false, edtavDynamicproptag_Internalname, AV12DynamicPropTag);
            if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
            {
               edtavDeleteproperty_Visible = 0;
               edtavDynamicpropname_Enabled = 0;
               edtavDynamicproptag_Enabled = 0;
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 235;
            }
            if ( ( subGrid_Islastpage == 1 ) || ( subGrid_Rows == 0 ) || ( ( GRID_nCurrentRecord >= GRID_nFirstRecordOnPage ) && ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_2352( ) ;
            }
            GRID_nEOF = (short)(((GRID_nCurrentRecord<GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
            GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
            subGrid_Recordcount = (int)(GRID_nCurrentRecord);
            if ( isFullAjaxMode( ) && ! bGXsfl_235_Refreshing )
            {
               DoAjaxLoad(235, GridRow);
            }
            AV74GXV1 = (int)(AV74GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E111H2( )
      {
         /* 'DoAdd' Routine */
         returnInSub = false;
         edtavDeleteproperty_gximage = "ActionDelete";
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "gximage", edtavDeleteproperty_gximage, !bGXsfl_235_Refreshing);
         AV65DeleteProperty = context.GetImagePath( "7695fe89-52c9-4b7e-871e-0e11548f823e", "", context.GetTheme( ));
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty)) ? AV75Deleteproperty_GXI : context.convertURL( context.PathToRelativeUrl( AV65DeleteProperty))), !bGXsfl_235_Refreshing);
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "SrcSet", context.GetImageSrcSet( AV65DeleteProperty), true);
         AV75Deleteproperty_GXI = GXDbFile.PathToUrl( context.GetImagePath( "7695fe89-52c9-4b7e-871e-0e11548f823e", "", context.GetTheme( )), context);
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty)) ? AV75Deleteproperty_GXI : context.convertURL( context.PathToRelativeUrl( AV65DeleteProperty))), !bGXsfl_235_Refreshing);
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "SrcSet", context.GetImageSrcSet( AV65DeleteProperty), true);
         edtavDeleteproperty_Visible = 1;
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDeleteproperty_Visible), 5, 0), !bGXsfl_235_Refreshing);
         edtavDynamicpropname_Enabled = 1;
         AssignProp(sPrefix, false, edtavDynamicpropname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDynamicpropname_Enabled), 5, 0), !bGXsfl_235_Refreshing);
         edtavDynamicpropname_Visible = 1;
         AssignProp(sPrefix, false, edtavDynamicpropname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDynamicpropname_Visible), 5, 0), !bGXsfl_235_Refreshing);
         edtavDynamicproptag_Enabled = 1;
         AssignProp(sPrefix, false, edtavDynamicproptag_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDynamicproptag_Enabled), 5, 0), !bGXsfl_235_Refreshing);
         edtavDynamicproptag_Visible = 1;
         AssignProp(sPrefix, false, edtavDynamicproptag_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDynamicproptag_Visible), 5, 0), !bGXsfl_235_Refreshing);
         if ( ( subGrid_Islastpage == 1 ) || ( subGrid_Rows == 0 ) || ( ( GRID_nCurrentRecord >= GRID_nFirstRecordOnPage ) && ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) )
         {
            sendrow_2352( ) ;
         }
         GRID_nEOF = (short)(((GRID_nCurrentRecord<GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( )) ? 1 : 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
         GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
         subGrid_Recordcount = (int)(GRID_nCurrentRecord);
         if ( isFullAjaxMode( ) && ! bGXsfl_235_Refreshing )
         {
            DoAjaxLoad(235, GridRow);
         }
         /*  Sending Event outputs  */
      }

      protected void S132( )
      {
         /* 'CHECKREQUIREDFIELDS' Routine */
         returnInSub = false;
         AV70CheckRequiredFieldsResult = true;
         AssignAttri(sPrefix, false, "AV70CheckRequiredFieldsResult", AV70CheckRequiredFieldsResult);
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV32Name)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_Name", ""), "", "", "", "", "", "", "", ""),  "error",  edtavName_Internalname,  "true",  ""));
            AV70CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV70CheckRequiredFieldsResult", AV70CheckRequiredFieldsResult);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV30LocalSiteURL)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_LocalSiteURL", ""), "", "", "", "", "", "", "", ""),  "error",  edtavLocalsiteurl_Internalname,  "true",  ""));
            AV70CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV70CheckRequiredFieldsResult", AV70CheckRequiredFieldsResult);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV37ServiceProviderEntityId)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_ServiceProviderEntityID", ""), "", "", "", "", "", "", "", ""),  "error",  edtavServiceproviderentityid_Internalname,  "true",  ""));
            AV70CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV70CheckRequiredFieldsResult", AV70CheckRequiredFieldsResult);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV36SamlEndpointLocation)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_SamlEndpointLocation", ""), "", "", "", "", "", "", "", ""),  "error",  edtavSamlendpointlocation_Internalname,  "true",  ""));
            AV70CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV70CheckRequiredFieldsResult", AV70CheckRequiredFieldsResult);
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV39SingleLogoutendpoint)) )
         {
            GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  "",  StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_GAM_SingleLogoutEndpoint", ""), "", "", "", "", "", "", "", ""),  "error",  edtavSinglelogoutendpoint_Internalname,  "true",  ""));
            AV70CheckRequiredFieldsResult = false;
            AssignAttri(sPrefix, false, "AV70CheckRequiredFieldsResult", AV70CheckRequiredFieldsResult);
         }
      }

      protected void S122( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
         if ( ! ( ! AV67DisableSingleLogout ) )
         {
            edtavSinglelogoutendpoint_Visible = 0;
            AssignProp(sPrefix, false, edtavSinglelogoutendpoint_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavSinglelogoutendpoint_Visible), 5, 0), true);
            divSinglelogoutendpoint_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divSinglelogoutendpoint_cell_Internalname, "Class", divSinglelogoutendpoint_cell_Class, true);
         }
         else
         {
            edtavSinglelogoutendpoint_Visible = 1;
            AssignProp(sPrefix, false, edtavSinglelogoutendpoint_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavSinglelogoutendpoint_Visible), 5, 0), true);
            divSinglelogoutendpoint_cell_Class = "col-xs-12 col-sm-6 RequiredDataContentCell DscTop";
            AssignProp(sPrefix, false, divSinglelogoutendpoint_cell_Internalname, "Class", divSinglelogoutendpoint_cell_Class, true);
         }
      }

      protected void E151H2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         edtavDeleteproperty_Enabled = 0;
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDeleteproperty_Enabled), 5, 0), !bGXsfl_235_Refreshing);
         /*  Sending Event outputs  */
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E121H2 ();
         if (returnInSub) return;
      }

      protected void E121H2( )
      {
         /* Enter Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CHECKREQUIREDFIELDS' */
         S132 ();
         if (returnInSub) return;
         if ( AV70CheckRequiredFieldsResult )
         {
            if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) )
            {
               /* Execute user subroutine: 'SAVEAUTHENTICATIONSAML' */
               S142 ();
               if (returnInSub) return;
            }
            else if ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 )
            {
               AV5AuthenticationTypeSaml20.load( AV32Name);
               AV5AuthenticationTypeSaml20.delete();
            }
            if ( AV5AuthenticationTypeSaml20.success() && ( AV14Errors.Count == 0 ) )
            {
               context.CommitDataStores("gamwcauthenticationtypeentrysaml20",pr_default);
               CallWebObject(formatLink("gamwwauthtypes.aspx") );
               context.wjLocDisableFrm = 1;
            }
            else
            {
               AV14Errors = AV5AuthenticationTypeSaml20.geterrors();
               AV76GXV2 = 1;
               while ( AV76GXV2 <= AV14Errors.Count )
               {
                  AV13Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV14Errors.Item(AV76GXV2));
                  GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV13Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV13Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
                  AV76GXV2 = (int)(AV76GXV2+1);
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV5AuthenticationTypeSaml20", AV5AuthenticationTypeSaml20);
      }

      protected void E161H2( )
      {
         /* Deleteproperty_Click Routine */
         returnInSub = false;
         edtavDeleteproperty_gximage = "ActionDelete";
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "gximage", edtavDeleteproperty_gximage, !bGXsfl_235_Refreshing);
         AV65DeleteProperty = context.GetImagePath( "7695fe89-52c9-4b7e-871e-0e11548f823e", "", context.GetTheme( ));
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty)) ? AV75Deleteproperty_GXI : context.convertURL( context.PathToRelativeUrl( AV65DeleteProperty))), !bGXsfl_235_Refreshing);
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "SrcSet", context.GetImageSrcSet( AV65DeleteProperty), true);
         AV75Deleteproperty_GXI = GXDbFile.PathToUrl( context.GetImagePath( "7695fe89-52c9-4b7e-871e-0e11548f823e", "", context.GetTheme( )), context);
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty)) ? AV75Deleteproperty_GXI : context.convertURL( context.PathToRelativeUrl( AV65DeleteProperty))), !bGXsfl_235_Refreshing);
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "SrcSet", context.GetImageSrcSet( AV65DeleteProperty), true);
         edtavDeleteproperty_Visible = 0;
         AssignProp(sPrefix, false, edtavDeleteproperty_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDeleteproperty_Visible), 5, 0), !bGXsfl_235_Refreshing);
         edtavDynamicpropname_Visible = 0;
         AssignProp(sPrefix, false, edtavDynamicpropname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDynamicpropname_Visible), 5, 0), !bGXsfl_235_Refreshing);
         edtavDynamicproptag_Visible = 0;
         AssignProp(sPrefix, false, edtavDynamicproptag_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDynamicproptag_Visible), 5, 0), !bGXsfl_235_Refreshing);
         AV11DynamicPropName = "";
         AssignAttri(sPrefix, false, edtavDynamicpropname_Internalname, AV11DynamicPropName);
         AV12DynamicPropTag = "";
         AssignAttri(sPrefix, false, edtavDynamicproptag_Internalname, AV12DynamicPropTag);
         AV5AuthenticationTypeSaml20.gxTpr_Name = AV32Name;
         AV5AuthenticationTypeSaml20.removeuserinfoproperty( AV11DynamicPropName, out  AV14Errors);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, "AV5AuthenticationTypeSaml20", AV5AuthenticationTypeSaml20);
      }

      protected void S112( )
      {
         /* 'INITAUTHENTICATIONSAML' Routine */
         returnInSub = false;
         AV5AuthenticationTypeSaml20.load( AV32Name);
         AV32Name = AV5AuthenticationTypeSaml20.gxTpr_Name;
         AssignAttri(sPrefix, false, "AV32Name", AV32Name);
         AV19IsEnable = AV5AuthenticationTypeSaml20.gxTpr_Isenable;
         AssignAttri(sPrefix, false, "AV19IsEnable", AV19IsEnable);
         AV10Dsc = AV5AuthenticationTypeSaml20.gxTpr_Description;
         AssignAttri(sPrefix, false, "AV10Dsc", AV10Dsc);
         AV40SmallImageName = AV5AuthenticationTypeSaml20.gxTpr_Smallimagename;
         AssignAttri(sPrefix, false, "AV40SmallImageName", AV40SmallImageName);
         AV7BigImageName = AV5AuthenticationTypeSaml20.gxTpr_Bigimagename;
         AssignAttri(sPrefix, false, "AV7BigImageName", AV7BigImageName);
         AV18Impersonate = AV5AuthenticationTypeSaml20.gxTpr_Impersonate;
         AssignAttri(sPrefix, false, "AV18Impersonate", AV18Impersonate);
         AV37ServiceProviderEntityId = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Serviceproviderentityid;
         AssignAttri(sPrefix, false, "AV37ServiceProviderEntityId", AV37ServiceProviderEntityId);
         AV38IdentityProviderEntityID = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Identityproviderentityid;
         AssignAttri(sPrefix, false, "AV38IdentityProviderEntityID", AV38IdentityProviderEntityID);
         AV6AuthnContext = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Authncontext;
         AssignAttri(sPrefix, false, "AV6AuthnContext", AV6AuthnContext);
         AV69NameIdPolicyFormat = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Nameidpolicyformat;
         AssignAttri(sPrefix, false, "AV69NameIdPolicyFormat", AV69NameIdPolicyFormat);
         AV20IsRedirectBinding = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Isredirectbinding;
         AssignAttri(sPrefix, false, "AV20IsRedirectBinding", AV20IsRedirectBinding);
         AV15ForceAuthn = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Forceauthn;
         AssignAttri(sPrefix, false, "AV15ForceAuthn", AV15ForceAuthn);
         AV21KeyAliasCredential = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keyaliascredential;
         AssignAttri(sPrefix, false, "AV21KeyAliasCredential", AV21KeyAliasCredential);
         AV22KeyAliasTrustCred = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keyaliastrustcred;
         AssignAttri(sPrefix, false, "AV22KeyAliasTrustCred", AV22KeyAliasTrustCred);
         AV24KeyStoreCredential = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystorecredential;
         AssignAttri(sPrefix, false, "AV24KeyStoreCredential", AV24KeyStoreCredential);
         AV25KeyStoreFilePathTrustCred = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystorefilepathtrustcred;
         AssignAttri(sPrefix, false, "AV25KeyStoreFilePathTrustCred", AV25KeyStoreFilePathTrustCred);
         AV26KeyStorePwdTrustCred = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystorepwdtrustcred;
         AssignAttri(sPrefix, false, "AV26KeyStorePwdTrustCred", AV26KeyStorePwdTrustCred);
         AV27KeyStoreTrustCred = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystoretrustcred;
         AssignAttri(sPrefix, false, "AV27KeyStoreTrustCred", AV27KeyStoreTrustCred);
         AV28KeyStPathCredential = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystpathcredential;
         AssignAttri(sPrefix, false, "AV28KeyStPathCredential", AV28KeyStPathCredential);
         AV29KeyStPwdCredential = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystpwdcredential;
         AssignAttri(sPrefix, false, "AV29KeyStPwdCredential", AV29KeyStPwdCredential);
         AV36SamlEndpointLocation = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Samlendpointlocation;
         AssignAttri(sPrefix, false, "AV36SamlEndpointLocation", AV36SamlEndpointLocation);
         AV30LocalSiteURL = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Localsiteurl;
         AssignAttri(sPrefix, false, "AV30LocalSiteURL", AV30LocalSiteURL);
         AV67DisableSingleLogout = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Disablesinglelogout;
         AssignAttri(sPrefix, false, "AV67DisableSingleLogout", AV67DisableSingleLogout);
         AV39SingleLogoutendpoint = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Singlelogoutendpoint;
         AssignAttri(sPrefix, false, "AV39SingleLogoutendpoint", AV39SingleLogoutendpoint);
         AV45UserInfoResponseUserErrorDescriptionTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseerrordescription_name;
         AssignAttri(sPrefix, false, "AV45UserInfoResponseUserErrorDescriptionTag", AV45UserInfoResponseUserErrorDescriptionTag);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG", GetSecureSignedToken( sPrefix, StringUtil.RTrim( context.localUtil.Format( AV45UserInfoResponseUserErrorDescriptionTag, "")), context));
         AV43UserInfoResponseUserBirthdayTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserbirthday_name;
         AssignAttri(sPrefix, false, "AV43UserInfoResponseUserBirthdayTag", AV43UserInfoResponseUserBirthdayTag);
         AV44UserInfoResponseUserEmailTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuseremail_name;
         AssignAttri(sPrefix, false, "AV44UserInfoResponseUserEmailTag", AV44UserInfoResponseUserEmailTag);
         AV46UserInfoResponseUserExternalIdTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserexternalid_name;
         AssignAttri(sPrefix, false, "AV46UserInfoResponseUserExternalIdTag", AV46UserInfoResponseUserExternalIdTag);
         AV47UserInfoResponseUserFirstNameTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserfirstname_name;
         AssignAttri(sPrefix, false, "AV47UserInfoResponseUserFirstNameTag", AV47UserInfoResponseUserFirstNameTag);
         AV48UserInfoResponseUserGenderTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusergender_name;
         AssignAttri(sPrefix, false, "AV48UserInfoResponseUserGenderTag", AV48UserInfoResponseUserGenderTag);
         AV49UserInfoResponseUserGenderValues = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusergender_values;
         AssignAttri(sPrefix, false, "AV49UserInfoResponseUserGenderValues", AV49UserInfoResponseUserGenderValues);
         AV50UserInfoResponseUserLanguageTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserlanguage_name;
         AssignAttri(sPrefix, false, "AV50UserInfoResponseUserLanguageTag", AV50UserInfoResponseUserLanguageTag);
         AV51UserInfoResponseUserLastNameGenAuto = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserlastname_generateautomatic;
         AssignAttri(sPrefix, false, "AV51UserInfoResponseUserLastNameGenAuto", AV51UserInfoResponseUserLastNameGenAuto);
         AV52UserInfoResponseUserLastNameTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserlastname_name;
         AssignAttri(sPrefix, false, "AV52UserInfoResponseUserLastNameTag", AV52UserInfoResponseUserLastNameTag);
         AV53UserInfoResponseUserNameTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusername_name;
         AssignAttri(sPrefix, false, "AV53UserInfoResponseUserNameTag", AV53UserInfoResponseUserNameTag);
         AV54UserInfoResponseUserTimeZoneTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusertimezone_name;
         AssignAttri(sPrefix, false, "AV54UserInfoResponseUserTimeZoneTag", AV54UserInfoResponseUserTimeZoneTag);
         AV55UserInfoResponseUserURLImageTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserurlimage_name;
         AssignAttri(sPrefix, false, "AV55UserInfoResponseUserURLImageTag", AV55UserInfoResponseUserURLImageTag);
         AV56UserInfoResponseUserURLProfileTag = AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserurlprofile_name;
         AssignAttri(sPrefix, false, "AV56UserInfoResponseUserURLProfileTag", AV56UserInfoResponseUserURLProfileTag);
      }

      protected void S142( )
      {
         /* 'SAVEAUTHENTICATIONSAML' Routine */
         returnInSub = false;
         AV5AuthenticationTypeSaml20.load( AV32Name);
         AV5AuthenticationTypeSaml20.gxTpr_Name = AV32Name;
         AV5AuthenticationTypeSaml20.gxTpr_Isenable = AV19IsEnable;
         AV5AuthenticationTypeSaml20.gxTpr_Description = AV10Dsc;
         AV5AuthenticationTypeSaml20.gxTpr_Smallimagename = AV40SmallImageName;
         AV5AuthenticationTypeSaml20.gxTpr_Bigimagename = AV7BigImageName;
         AV5AuthenticationTypeSaml20.gxTpr_Impersonate = AV18Impersonate;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Serviceproviderentityid = AV37ServiceProviderEntityId;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Identityproviderentityid = AV38IdentityProviderEntityID;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Authncontext = AV6AuthnContext;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Nameidpolicyformat = AV69NameIdPolicyFormat;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Isredirectbinding = AV20IsRedirectBinding;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Forceauthn = AV15ForceAuthn;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keyaliascredential = AV21KeyAliasCredential;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keyaliastrustcred = AV22KeyAliasTrustCred;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystorecredential = AV24KeyStoreCredential;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystorefilepathtrustcred = AV25KeyStoreFilePathTrustCred;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystorepwdtrustcred = AV26KeyStorePwdTrustCred;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystoretrustcred = AV27KeyStoreTrustCred;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystpathcredential = AV28KeyStPathCredential;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Keystpwdcredential = AV29KeyStPwdCredential;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Samlendpointlocation = AV36SamlEndpointLocation;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Disablesinglelogout = AV67DisableSingleLogout;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Singlelogoutendpoint = AV39SingleLogoutendpoint;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Localsiteurl = AV30LocalSiteURL;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseerrordescription_name = AV45UserInfoResponseUserErrorDescriptionTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserbirthday_name = AV43UserInfoResponseUserBirthdayTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuseremail_name = AV44UserInfoResponseUserEmailTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserexternalid_name = AV46UserInfoResponseUserExternalIdTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserfirstname_name = AV47UserInfoResponseUserFirstNameTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusergender_name = AV48UserInfoResponseUserGenderTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusergender_values = AV49UserInfoResponseUserGenderValues;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserlanguage_name = AV50UserInfoResponseUserLanguageTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserlastname_generateautomatic = AV51UserInfoResponseUserLastNameGenAuto;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserlastname_name = AV52UserInfoResponseUserLastNameTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusername_name = AV53UserInfoResponseUserNameTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseusertimezone_name = AV54UserInfoResponseUserTimeZoneTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserurlimage_name = AV55UserInfoResponseUserURLImageTag;
         AV5AuthenticationTypeSaml20.gxTpr_Saml20.gxTpr_Userinfo.gxTpr_Responseuserurlprofile_name = AV56UserInfoResponseUserURLProfileTag;
         AV5AuthenticationTypeSaml20.save();
         /* Start For Each Line */
         nRC_GXsfl_235 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_235"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
         nGXsfl_235_fel_idx = 0;
         while ( nGXsfl_235_fel_idx < nRC_GXsfl_235 )
         {
            nGXsfl_235_fel_idx = ((subGrid_Islastpage==1)&&(nGXsfl_235_fel_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_235_fel_idx+1);
            sGXsfl_235_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_235_fel_idx), 4, 0), 4, "0");
            SubsflControlProps_fel_2352( ) ;
            AV11DynamicPropName = cgiGet( edtavDynamicpropname_Internalname);
            AV12DynamicPropTag = cgiGet( edtavDynamicproptag_Internalname);
            AV65DeleteProperty = cgiGet( edtavDeleteproperty_Internalname);
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11DynamicPropName)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV12DynamicPropTag)) )
            {
               AV17GAMPropertySimple = new GeneXus.Programs.genexussecurity.SdtGAMPropertySimple(context);
               AV17GAMPropertySimple.gxTpr_Id = AV11DynamicPropName;
               AV17GAMPropertySimple.gxTpr_Value = AV12DynamicPropTag;
               if ( ! AV5AuthenticationTypeSaml20.setuserinfoproperty(AV17GAMPropertySimple, out  AV14Errors) )
               {
                  AV78GXV3 = 1;
                  while ( AV78GXV3 <= AV14Errors.Count )
                  {
                     AV13Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV14Errors.Item(AV78GXV3));
                     context.StatusMessage( StringUtil.Format( "%1 (GAM%2)", AV13Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV13Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", "") );
                     AV78GXV3 = (int)(AV78GXV3+1);
                  }
               }
            }
            /* End For Each Line */
         }
         if ( nGXsfl_235_fel_idx == 0 )
         {
            nGXsfl_235_idx = 1;
            sGXsfl_235_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_235_idx), 4, 0), 4, "0");
            SubsflControlProps_2352( ) ;
         }
         nGXsfl_235_fel_idx = 1;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         Gx_mode = (string)getParm(obj,0);
         AssignAttri(sPrefix, false, "Gx_mode", Gx_mode);
         AV32Name = (string)getParm(obj,1);
         AssignAttri(sPrefix, false, "AV32Name", AV32Name);
         AV41TypeId = (string)getParm(obj,2);
         AssignAttri(sPrefix, false, "AV41TypeId", AV41TypeId);
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
         PA1H2( ) ;
         WS1H2( ) ;
         WE1H2( ) ;
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
         sCtrlAV32Name = (string)((string)getParm(obj,1));
         sCtrlAV41TypeId = (string)((string)getParm(obj,2));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA1H2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "gamwcauthenticationtypeentrysaml20", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA1H2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            Gx_mode = (string)getParm(obj,2);
            AssignAttri(sPrefix, false, "Gx_mode", Gx_mode);
            AV32Name = (string)getParm(obj,3);
            AssignAttri(sPrefix, false, "AV32Name", AV32Name);
            AV41TypeId = (string)getParm(obj,4);
            AssignAttri(sPrefix, false, "AV41TypeId", AV41TypeId);
         }
         wcpOGx_mode = cgiGet( sPrefix+"wcpOGx_mode");
         wcpOAV32Name = cgiGet( sPrefix+"wcpOAV32Name");
         wcpOAV41TypeId = cgiGet( sPrefix+"wcpOAV41TypeId");
         if ( ! GetJustCreated( ) && ( ( StringUtil.StrCmp(Gx_mode, wcpOGx_mode) != 0 ) || ( StringUtil.StrCmp(AV32Name, wcpOAV32Name) != 0 ) || ( StringUtil.StrCmp(AV41TypeId, wcpOAV41TypeId) != 0 ) ) )
         {
            setjustcreated();
         }
         wcpOGx_mode = Gx_mode;
         wcpOAV32Name = AV32Name;
         wcpOAV41TypeId = AV41TypeId;
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
         sCtrlAV32Name = cgiGet( sPrefix+"AV32Name_CTRL");
         if ( StringUtil.Len( sCtrlAV32Name) > 0 )
         {
            AV32Name = cgiGet( sCtrlAV32Name);
            AssignAttri(sPrefix, false, "AV32Name", AV32Name);
         }
         else
         {
            AV32Name = cgiGet( sPrefix+"AV32Name_PARM");
         }
         sCtrlAV41TypeId = cgiGet( sPrefix+"AV41TypeId_CTRL");
         if ( StringUtil.Len( sCtrlAV41TypeId) > 0 )
         {
            AV41TypeId = cgiGet( sCtrlAV41TypeId);
            AssignAttri(sPrefix, false, "AV41TypeId", AV41TypeId);
         }
         else
         {
            AV41TypeId = cgiGet( sPrefix+"AV41TypeId_PARM");
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
         PA1H2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS1H2( ) ;
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
         WS1H2( ) ;
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
         GxWebStd.gx_hidden_field( context, sPrefix+"AV32Name_PARM", StringUtil.RTrim( AV32Name));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV32Name)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV32Name_CTRL", StringUtil.RTrim( sCtrlAV32Name));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV41TypeId_PARM", StringUtil.RTrim( AV41TypeId));
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV41TypeId)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV41TypeId_CTRL", StringUtil.RTrim( sCtrlAV41TypeId));
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
         WE1H2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202412162336169", true, true);
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
         context.AddJavascriptSource("gamwcauthenticationtypeentrysaml20.js", "?202412162336169", false, true);
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
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_2352( )
      {
         edtavDynamicpropname_Internalname = sPrefix+"vDYNAMICPROPNAME_"+sGXsfl_235_idx;
         edtavDynamicproptag_Internalname = sPrefix+"vDYNAMICPROPTAG_"+sGXsfl_235_idx;
         edtavDeleteproperty_Internalname = sPrefix+"vDELETEPROPERTY_"+sGXsfl_235_idx;
      }

      protected void SubsflControlProps_fel_2352( )
      {
         edtavDynamicpropname_Internalname = sPrefix+"vDYNAMICPROPNAME_"+sGXsfl_235_fel_idx;
         edtavDynamicproptag_Internalname = sPrefix+"vDYNAMICPROPTAG_"+sGXsfl_235_fel_idx;
         edtavDeleteproperty_Internalname = sPrefix+"vDELETEPROPERTY_"+sGXsfl_235_fel_idx;
      }

      protected void sendrow_2352( )
      {
         sGXsfl_235_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_235_idx), 4, 0), 4, "0");
         SubsflControlProps_2352( ) ;
         WB1H0( ) ;
         if ( ( subGrid_Rows * 1 == 0 ) || ( nGXsfl_235_idx <= subGrid_fnc_Recordsperpage( ) * 1 ) )
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
               if ( ((int)((nGXsfl_235_idx) % (2))) == 0 )
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
               context.WriteHtmlText( " class=\""+"GridNoBorder WorkWith"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_235_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavDynamicpropname_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 236,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',235)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDynamicpropname_Internalname,StringUtil.RTrim( AV11DynamicPropName),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,236);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDynamicpropname_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtavDynamicpropname_Visible,(int)edtavDynamicpropname_Enabled,(short)1,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)60,(short)0,(short)0,(short)235,(short)0,(short)-1,(short)0,(bool)true,(string)"GeneXusSecurityCommon\\GAMPropertyId",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavDynamicproptag_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 237,'" + sPrefix + "',false,'" + sGXsfl_235_idx + "',235)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDynamicproptag_Internalname,StringUtil.RTrim( AV12DynamicPropTag),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,237);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDynamicproptag_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtavDynamicproptag_Visible,(int)edtavDynamicproptag_Enabled,(short)1,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)235,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavDeleteproperty_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Active Bitmap Variable */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 238,'" + sPrefix + "',false,'',235)\"";
            ClassString = "ActionBaseColorAttribute" + " " + ((StringUtil.StrCmp(edtavDeleteproperty_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteproperty_gximage+"_Class");
            StyleString = "";
            AV65DeleteProperty_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty))&&String.IsNullOrEmpty(StringUtil.RTrim( AV75Deleteproperty_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty)));
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV65DeleteProperty)) ? AV75Deleteproperty_GXI : context.PathToRelativeUrl( AV65DeleteProperty));
            GridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteproperty_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavDeleteproperty_Visible,(int)edtavDeleteproperty_Enabled,(string)"",(string)edtavDeleteproperty_Tooltiptext,(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteproperty_Jsonclick,"'"+sPrefix+"'"+",false,"+"'"+sPrefix+"EVDELETEPROPERTY.CLICK."+sGXsfl_235_idx+"'",(string)StyleString,(string)ClassString,(string)"WWActionColumn",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV65DeleteProperty_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
            send_integrity_lvl_hashes1H2( ) ;
            GridContainer.AddRow(GridRow);
            nGXsfl_235_idx = ((subGrid_Islastpage==1)&&(nGXsfl_235_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_235_idx+1);
            sGXsfl_235_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_235_idx), 4, 0), 4, "0");
            SubsflControlProps_2352( ) ;
         }
         /* End function sendrow_2352 */
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
         chkavIsenable.Caption = context.GetMessage( "WWP_GAM_Enabled", "");
         AssignProp(sPrefix, false, chkavIsenable_Internalname, "TitleCaption", chkavIsenable.Caption, true);
         chkavIsenable.CheckedValue = "false";
         chkavIsredirectbinding.Name = "vISREDIRECTBINDING";
         chkavIsredirectbinding.WebTags = "";
         chkavIsredirectbinding.Caption = " ";
         AssignProp(sPrefix, false, chkavIsredirectbinding_Internalname, "TitleCaption", chkavIsredirectbinding.Caption, true);
         chkavIsredirectbinding.CheckedValue = "false";
         cmbavNameidpolicyformat.Name = "vNAMEIDPOLICYFORMAT";
         cmbavNameidpolicyformat.WebTags = "";
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified", context.GetMessage( "GAM_UNSPECIFIED", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress", context.GetMessage( "GAM_EMAIL", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:2.0:nameid-format:encrypted", context.GetMessage( "GAM_ENCRYPTED", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:2.0:nameid-format:transient", context.GetMessage( "GAM_TRANSIENT", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:2.0:nameid-format:persistent", context.GetMessage( "GAM_PERSISTENT", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:2.0:nameid-format:entity", context.GetMessage( "GAM_ENTITY", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:2.0:nameid-format:kerberos", context.GetMessage( "GAM_KERBEROS", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName", context.GetMessage( "GAM_WIN_DOMAIN_QUALIFIED", ""), 0);
         cmbavNameidpolicyformat.addItem("urn:oasis:names:tc:SAML:1.1:nameid-format:X509SubjectName", context.GetMessage( "GAM_X509_SUBJECT", ""), 0);
         if ( cmbavNameidpolicyformat.ItemCount > 0 )
         {
         }
         chkavForceauthn.Name = "vFORCEAUTHN";
         chkavForceauthn.WebTags = "";
         chkavForceauthn.Caption = " ";
         AssignProp(sPrefix, false, chkavForceauthn_Internalname, "TitleCaption", chkavForceauthn.Caption, true);
         chkavForceauthn.CheckedValue = "false";
         chkavDisablesinglelogout.Name = "vDISABLESINGLELOGOUT";
         chkavDisablesinglelogout.WebTags = "";
         chkavDisablesinglelogout.Caption = " ";
         AssignProp(sPrefix, false, chkavDisablesinglelogout_Internalname, "TitleCaption", chkavDisablesinglelogout.Caption, true);
         chkavDisablesinglelogout.CheckedValue = "false";
         chkavUserinforesponseuserlastnamegenauto.Name = "vUSERINFORESPONSEUSERLASTNAMEGENAUTO";
         chkavUserinforesponseuserlastnamegenauto.WebTags = "";
         chkavUserinforesponseuserlastnamegenauto.Caption = " ";
         AssignProp(sPrefix, false, chkavUserinforesponseuserlastnamegenauto_Internalname, "TitleCaption", chkavUserinforesponseuserlastnamegenauto.Caption, true);
         chkavUserinforesponseuserlastnamegenauto.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void StartGridControl235( )
      {
         if ( GridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridContainer"+"DivS\" data-gxgridid=\"235\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGrid_Internalname, subGrid_Internalname, "", "GridNoBorder WorkWith", 0, "", "", 1, 2, sStyleString, "", "", 0);
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
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavDynamicpropname_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_AttributeName", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavDynamicproptag_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_AttributeTag", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"ActionBaseColorAttribute"+" "+((StringUtil.StrCmp(edtavDeleteproperty_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteproperty_gximage+"_Class")+"\" "+" style=\""+((edtavDeleteproperty_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridContainer.AddObjectProperty("GridName", "Grid");
         }
         else
         {
            GridContainer.AddObjectProperty("GridName", "Grid");
            GridContainer.AddObjectProperty("Header", subGrid_Header);
            GridContainer.AddObjectProperty("Class", "GridNoBorder WorkWith");
            GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("CmpContext", sPrefix);
            GridContainer.AddObjectProperty("InMasterPage", "false");
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV11DynamicPropName)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDynamicpropname_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDynamicpropname_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV12DynamicPropTag)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDynamicproptag_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDynamicproptag_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", context.convertURL( AV65DeleteProperty));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeleteproperty_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Tooltiptext", StringUtil.RTrim( edtavDeleteproperty_Tooltiptext));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeleteproperty_Visible), 5, 0, ".", "")));
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
         edtavName_Internalname = sPrefix+"vNAME";
         cmbavFunctionid_Internalname = sPrefix+"vFUNCTIONID";
         edtavDsc_Internalname = sPrefix+"vDSC";
         chkavIsenable_Internalname = sPrefix+"vISENABLE";
         edtavImpersonate_Internalname = sPrefix+"vIMPERSONATE";
         edtavSmallimagename_Internalname = sPrefix+"vSMALLIMAGENAME";
         edtavBigimagename_Internalname = sPrefix+"vBIGIMAGENAME";
         divTableattributes_Internalname = sPrefix+"TABLEATTRIBUTES";
         lblGeneral_title_Internalname = sPrefix+"GENERAL_TITLE";
         edtavLocalsiteurl_Internalname = sPrefix+"vLOCALSITEURL";
         edtavServiceproviderentityid_Internalname = sPrefix+"vSERVICEPROVIDERENTITYID";
         edtavIdentityproviderentityid_Internalname = sPrefix+"vIDENTITYPROVIDERENTITYID";
         chkavIsredirectbinding_Internalname = sPrefix+"vISREDIRECTBINDING";
         edtavSamlendpointlocation_Internalname = sPrefix+"vSAMLENDPOINTLOCATION";
         cmbavNameidpolicyformat_Internalname = sPrefix+"vNAMEIDPOLICYFORMAT";
         chkavForceauthn_Internalname = sPrefix+"vFORCEAUTHN";
         edtavAuthncontext_Internalname = sPrefix+"vAUTHNCONTEXT";
         divUnnamedtable7_Internalname = sPrefix+"UNNAMEDTABLE7";
         Dvpanel_unnamedtable7_Internalname = sPrefix+"DVPANEL_UNNAMEDTABLE7";
         chkavDisablesinglelogout_Internalname = sPrefix+"vDISABLESINGLELOGOUT";
         edtavSinglelogoutendpoint_Internalname = sPrefix+"vSINGLELOGOUTENDPOINT";
         divSinglelogoutendpoint_cell_Internalname = sPrefix+"SINGLELOGOUTENDPOINT_CELL";
         divUnnamedtable8_Internalname = sPrefix+"UNNAMEDTABLE8";
         Dvpanel_unnamedtable8_Internalname = sPrefix+"DVPANEL_UNNAMEDTABLE8";
         divUnnamedtable6_Internalname = sPrefix+"UNNAMEDTABLE6";
         lblCredentials_title_Internalname = sPrefix+"CREDENTIALS_TITLE";
         edtavKeystpathcredential_Internalname = sPrefix+"vKEYSTPATHCREDENTIAL";
         edtavKeystpwdcredential_Internalname = sPrefix+"vKEYSTPWDCREDENTIAL";
         edtavKeyaliascredential_Internalname = sPrefix+"vKEYALIASCREDENTIAL";
         edtavKeystorecredential_Internalname = sPrefix+"vKEYSTORECREDENTIAL";
         divUnnamedtable4_Internalname = sPrefix+"UNNAMEDTABLE4";
         Dvpanel_unnamedtable4_Internalname = sPrefix+"DVPANEL_UNNAMEDTABLE4";
         edtavKeystorefilepathtrustcred_Internalname = sPrefix+"vKEYSTOREFILEPATHTRUSTCRED";
         edtavKeystorepwdtrustcred_Internalname = sPrefix+"vKEYSTOREPWDTRUSTCRED";
         edtavKeyaliastrustcred_Internalname = sPrefix+"vKEYALIASTRUSTCRED";
         edtavKeystoretrustcred_Internalname = sPrefix+"vKEYSTORETRUSTCRED";
         divUnnamedtable5_Internalname = sPrefix+"UNNAMEDTABLE5";
         Dvpanel_unnamedtable5_Internalname = sPrefix+"DVPANEL_UNNAMEDTABLE5";
         divUnnamedtable3_Internalname = sPrefix+"UNNAMEDTABLE3";
         lblUserinfo_title_Internalname = sPrefix+"USERINFO_TITLE";
         edtavUserinforesponseuseremailtag_Internalname = sPrefix+"vUSERINFORESPONSEUSEREMAILTAG";
         edtavUserinforesponseuserexternalidtag_Internalname = sPrefix+"vUSERINFORESPONSEUSEREXTERNALIDTAG";
         edtavUserinforesponseusernametag_Internalname = sPrefix+"vUSERINFORESPONSEUSERNAMETAG";
         edtavUserinforesponseuserfirstnametag_Internalname = sPrefix+"vUSERINFORESPONSEUSERFIRSTNAMETAG";
         chkavUserinforesponseuserlastnamegenauto_Internalname = sPrefix+"vUSERINFORESPONSEUSERLASTNAMEGENAUTO";
         edtavUserinforesponseuserlastnametag_Internalname = sPrefix+"vUSERINFORESPONSEUSERLASTNAMETAG";
         edtavUserinforesponseusergendertag_Internalname = sPrefix+"vUSERINFORESPONSEUSERGENDERTAG";
         edtavUserinforesponseusergendervalues_Internalname = sPrefix+"vUSERINFORESPONSEUSERGENDERVALUES";
         edtavUserinforesponseuserbirthdaytag_Internalname = sPrefix+"vUSERINFORESPONSEUSERBIRTHDAYTAG";
         edtavUserinforesponseuserurlimagetag_Internalname = sPrefix+"vUSERINFORESPONSEUSERURLIMAGETAG";
         edtavUserinforesponseuserurlprofiletag_Internalname = sPrefix+"vUSERINFORESPONSEUSERURLPROFILETAG";
         edtavUserinforesponseuserlanguagetag_Internalname = sPrefix+"vUSERINFORESPONSEUSERLANGUAGETAG";
         edtavUserinforesponseusertimezonetag_Internalname = sPrefix+"vUSERINFORESPONSEUSERTIMEZONETAG";
         edtavUserinforesponseerrordescriptiontag_Internalname = sPrefix+"vUSERINFORESPONSEERRORDESCRIPTIONTAG";
         edtavDynamicpropname_Internalname = sPrefix+"vDYNAMICPROPNAME";
         edtavDynamicproptag_Internalname = sPrefix+"vDYNAMICPROPTAG";
         edtavDeleteproperty_Internalname = sPrefix+"vDELETEPROPERTY";
         bttBtnadd_Internalname = sPrefix+"BTNADD";
         divUnnamedtable2_Internalname = sPrefix+"UNNAMEDTABLE2";
         Dvpanel_unnamedtable2_Internalname = sPrefix+"DVPANEL_UNNAMEDTABLE2";
         divUnnamedtable1_Internalname = sPrefix+"UNNAMEDTABLE1";
         Gxuitabspanel_tabs_Internalname = sPrefix+"GXUITABSPANEL_TABS";
         divTablecontent_Internalname = sPrefix+"TABLECONTENT";
         bttBtnenter_Internalname = sPrefix+"BTNENTER";
         bttBtncancel_Internalname = sPrefix+"BTNCANCEL";
         divTablemain_Internalname = sPrefix+"TABLEMAIN";
         Grid_empowerer_Internalname = sPrefix+"GRID_EMPOWERER";
         divHtml_bottomauxiliarcontrols_Internalname = sPrefix+"HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = sPrefix+"LAYOUTMAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGrid_Internalname = sPrefix+"GRID";
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
         subGrid_Allowcollapsing = 0;
         subGrid_Allowselection = 0;
         subGrid_Header = "";
         chkavUserinforesponseuserlastnamegenauto.Caption = " ";
         chkavDisablesinglelogout.Caption = " ";
         chkavForceauthn.Caption = " ";
         chkavIsredirectbinding.Caption = " ";
         chkavIsenable.Caption = context.GetMessage( "WWP_GAM_Enabled", "");
         edtavDeleteproperty_Jsonclick = "";
         edtavDeleteproperty_Tooltiptext = "";
         edtavDynamicproptag_Jsonclick = "";
         edtavDynamicpropname_Jsonclick = "";
         subGrid_Class = "GridNoBorder WorkWith";
         subGrid_Backcolorstyle = 0;
         edtavDeleteproperty_Enabled = 1;
         edtavDynamicproptag_Visible = -1;
         edtavDynamicproptag_Enabled = 1;
         edtavDynamicpropname_Visible = -1;
         edtavDynamicpropname_Enabled = 1;
         edtavDeleteproperty_Visible = -1;
         edtavDeleteproperty_gximage = "";
         bttBtnenter_Caption = context.GetMessage( "GX_BtnEnter", "");
         bttBtnenter_Visible = 1;
         bttBtnadd_Visible = 1;
         edtavUserinforesponseerrordescriptiontag_Jsonclick = "";
         edtavUserinforesponseerrordescriptiontag_Enabled = 1;
         edtavUserinforesponseusertimezonetag_Jsonclick = "";
         edtavUserinforesponseusertimezonetag_Enabled = 1;
         edtavUserinforesponseuserlanguagetag_Jsonclick = "";
         edtavUserinforesponseuserlanguagetag_Enabled = 1;
         edtavUserinforesponseuserurlprofiletag_Jsonclick = "";
         edtavUserinforesponseuserurlprofiletag_Enabled = 1;
         edtavUserinforesponseuserurlimagetag_Jsonclick = "";
         edtavUserinforesponseuserurlimagetag_Enabled = 1;
         edtavUserinforesponseuserbirthdaytag_Jsonclick = "";
         edtavUserinforesponseuserbirthdaytag_Enabled = 1;
         edtavUserinforesponseusergendervalues_Jsonclick = "";
         edtavUserinforesponseusergendervalues_Enabled = 1;
         edtavUserinforesponseusergendertag_Jsonclick = "";
         edtavUserinforesponseusergendertag_Enabled = 1;
         edtavUserinforesponseuserlastnametag_Jsonclick = "";
         edtavUserinforesponseuserlastnametag_Enabled = 1;
         chkavUserinforesponseuserlastnamegenauto.Enabled = 1;
         edtavUserinforesponseuserfirstnametag_Jsonclick = "";
         edtavUserinforesponseuserfirstnametag_Enabled = 1;
         edtavUserinforesponseusernametag_Jsonclick = "";
         edtavUserinforesponseusernametag_Enabled = 1;
         edtavUserinforesponseuserexternalidtag_Jsonclick = "";
         edtavUserinforesponseuserexternalidtag_Enabled = 1;
         edtavUserinforesponseuseremailtag_Jsonclick = "";
         edtavUserinforesponseuseremailtag_Enabled = 1;
         edtavKeystoretrustcred_Jsonclick = "";
         edtavKeystoretrustcred_Enabled = 1;
         edtavKeyaliastrustcred_Jsonclick = "";
         edtavKeyaliastrustcred_Enabled = 1;
         edtavKeystorepwdtrustcred_Jsonclick = "";
         edtavKeystorepwdtrustcred_Enabled = 1;
         edtavKeystorefilepathtrustcred_Jsonclick = "";
         edtavKeystorefilepathtrustcred_Enabled = 1;
         edtavKeystorecredential_Jsonclick = "";
         edtavKeystorecredential_Enabled = 1;
         edtavKeyaliascredential_Jsonclick = "";
         edtavKeyaliascredential_Enabled = 1;
         edtavKeystpwdcredential_Jsonclick = "";
         edtavKeystpwdcredential_Enabled = 1;
         edtavKeystpathcredential_Jsonclick = "";
         edtavKeystpathcredential_Enabled = 1;
         edtavSinglelogoutendpoint_Jsonclick = "";
         edtavSinglelogoutendpoint_Enabled = 1;
         edtavSinglelogoutendpoint_Visible = 1;
         divSinglelogoutendpoint_cell_Class = "col-xs-12 col-sm-6";
         chkavDisablesinglelogout.Enabled = 1;
         edtavAuthncontext_Jsonclick = "";
         edtavAuthncontext_Enabled = 1;
         chkavForceauthn.Enabled = 1;
         cmbavNameidpolicyformat_Jsonclick = "";
         cmbavNameidpolicyformat.Enabled = 1;
         edtavSamlendpointlocation_Jsonclick = "";
         edtavSamlendpointlocation_Enabled = 1;
         chkavIsredirectbinding.Enabled = 1;
         edtavIdentityproviderentityid_Jsonclick = "";
         edtavIdentityproviderentityid_Enabled = 1;
         edtavServiceproviderentityid_Jsonclick = "";
         edtavServiceproviderentityid_Enabled = 1;
         edtavLocalsiteurl_Jsonclick = "";
         edtavLocalsiteurl_Enabled = 1;
         edtavBigimagename_Jsonclick = "";
         edtavBigimagename_Enabled = 1;
         edtavSmallimagename_Jsonclick = "";
         edtavSmallimagename_Enabled = 1;
         edtavImpersonate_Jsonclick = "";
         edtavImpersonate_Enabled = 1;
         chkavIsenable.Enabled = 1;
         edtavDsc_Jsonclick = "";
         edtavDsc_Enabled = 1;
         cmbavFunctionid_Jsonclick = "";
         cmbavFunctionid.Enabled = 1;
         edtavName_Jsonclick = "";
         edtavName_Enabled = 0;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         Gxuitabspanel_tabs_Historymanagement = Convert.ToBoolean( 0);
         Gxuitabspanel_tabs_Class = "Tab";
         Gxuitabspanel_tabs_Pagecount = 3;
         Dvpanel_unnamedtable2_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable2_Iconposition = "Right";
         Dvpanel_unnamedtable2_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable2_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable2_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable2_Title = context.GetMessage( "WWP_GAM_CustomUserAttributes", "");
         Dvpanel_unnamedtable2_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable2_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable2_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable2_Width = "100%";
         Dvpanel_unnamedtable5_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable5_Iconposition = "Right";
         Dvpanel_unnamedtable5_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable5_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable5_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable5_Title = context.GetMessage( "WWP_GAM_Responsecredentials", "");
         Dvpanel_unnamedtable5_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable5_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable5_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable5_Width = "100%";
         Dvpanel_unnamedtable4_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Iconposition = "Right";
         Dvpanel_unnamedtable4_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable4_Title = context.GetMessage( "WWP_GAM_Requestcredentials", "");
         Dvpanel_unnamedtable4_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable4_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable4_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable4_Width = "100%";
         Dvpanel_unnamedtable8_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Iconposition = "Right";
         Dvpanel_unnamedtable8_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable8_Title = context.GetMessage( "WWP_GAM_Logout", "");
         Dvpanel_unnamedtable8_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable8_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable8_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable8_Width = "100%";
         Dvpanel_unnamedtable7_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Iconposition = "Right";
         Dvpanel_unnamedtable7_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable7_Title = context.GetMessage( "WWP_GAM_Login", "");
         Dvpanel_unnamedtable7_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable7_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable7_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable7_Width = "100%";
         subGrid_Rows = 0;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"Gx_mode","fld":"vMODE","pic":"@!"},{"av":"sPrefix"},{"av":"AV19IsEnable","fld":"vISENABLE"},{"av":"AV20IsRedirectBinding","fld":"vISREDIRECTBINDING"},{"av":"AV15ForceAuthn","fld":"vFORCEAUTHN"},{"av":"AV67DisableSingleLogout","fld":"vDISABLESINGLELOGOUT"},{"av":"AV51UserInfoResponseUserLastNameGenAuto","fld":"vUSERINFORESPONSEUSERLASTNAMEGENAUTO"},{"av":"AV45UserInfoResponseUserErrorDescriptionTag","fld":"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"edtavDeleteproperty_Enabled","ctrl":"vDELETEPROPERTY","prop":"Enabled"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E141H2","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!"}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"AV65DeleteProperty","fld":"vDELETEPROPERTY"},{"av":"edtavDeleteproperty_Tooltiptext","ctrl":"vDELETEPROPERTY","prop":"Tooltiptext"},{"av":"AV11DynamicPropName","fld":"vDYNAMICPROPNAME"},{"av":"AV12DynamicPropTag","fld":"vDYNAMICPROPTAG"},{"av":"edtavDeleteproperty_Visible","ctrl":"vDELETEPROPERTY","prop":"Visible"},{"av":"edtavDynamicpropname_Enabled","ctrl":"vDYNAMICPROPNAME","prop":"Enabled"},{"av":"edtavDynamicproptag_Enabled","ctrl":"vDYNAMICPROPTAG","prop":"Enabled"}]}""");
         setEventMetadata("'DOADD'","""{"handler":"E111H2","iparms":[]""");
         setEventMetadata("'DOADD'",""","oparms":[{"av":"AV65DeleteProperty","fld":"vDELETEPROPERTY"},{"av":"edtavDeleteproperty_Visible","ctrl":"vDELETEPROPERTY","prop":"Visible"},{"av":"edtavDynamicpropname_Enabled","ctrl":"vDYNAMICPROPNAME","prop":"Enabled"},{"av":"edtavDynamicpropname_Visible","ctrl":"vDYNAMICPROPNAME","prop":"Visible"},{"av":"edtavDynamicproptag_Enabled","ctrl":"vDYNAMICPROPTAG","prop":"Enabled"},{"av":"edtavDynamicproptag_Visible","ctrl":"vDYNAMICPROPTAG","prop":"Visible"}]}""");
         setEventMetadata("ENTER","""{"handler":"E121H2","iparms":[{"av":"AV70CheckRequiredFieldsResult","fld":"vCHECKREQUIREDFIELDSRESULT"},{"av":"Gx_mode","fld":"vMODE","pic":"@!"},{"av":"AV32Name","fld":"vNAME"},{"av":"AV30LocalSiteURL","fld":"vLOCALSITEURL"},{"av":"AV37ServiceProviderEntityId","fld":"vSERVICEPROVIDERENTITYID"},{"av":"AV36SamlEndpointLocation","fld":"vSAMLENDPOINTLOCATION"},{"av":"AV39SingleLogoutendpoint","fld":"vSINGLELOGOUTENDPOINT"},{"av":"AV19IsEnable","fld":"vISENABLE"},{"av":"AV10Dsc","fld":"vDSC"},{"av":"AV40SmallImageName","fld":"vSMALLIMAGENAME"},{"av":"AV7BigImageName","fld":"vBIGIMAGENAME"},{"av":"AV18Impersonate","fld":"vIMPERSONATE"},{"av":"AV38IdentityProviderEntityID","fld":"vIDENTITYPROVIDERENTITYID"},{"av":"AV6AuthnContext","fld":"vAUTHNCONTEXT"},{"av":"cmbavNameidpolicyformat"},{"av":"AV69NameIdPolicyFormat","fld":"vNAMEIDPOLICYFORMAT"},{"av":"AV20IsRedirectBinding","fld":"vISREDIRECTBINDING"},{"av":"AV15ForceAuthn","fld":"vFORCEAUTHN"},{"av":"AV21KeyAliasCredential","fld":"vKEYALIASCREDENTIAL"},{"av":"AV22KeyAliasTrustCred","fld":"vKEYALIASTRUSTCRED"},{"av":"AV24KeyStoreCredential","fld":"vKEYSTORECREDENTIAL"},{"av":"AV25KeyStoreFilePathTrustCred","fld":"vKEYSTOREFILEPATHTRUSTCRED"},{"av":"AV26KeyStorePwdTrustCred","fld":"vKEYSTOREPWDTRUSTCRED"},{"av":"AV27KeyStoreTrustCred","fld":"vKEYSTORETRUSTCRED"},{"av":"AV28KeyStPathCredential","fld":"vKEYSTPATHCREDENTIAL"},{"av":"AV29KeyStPwdCredential","fld":"vKEYSTPWDCREDENTIAL"},{"av":"AV67DisableSingleLogout","fld":"vDISABLESINGLELOGOUT"},{"av":"AV45UserInfoResponseUserErrorDescriptionTag","fld":"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG","hsh":true},{"av":"AV43UserInfoResponseUserBirthdayTag","fld":"vUSERINFORESPONSEUSERBIRTHDAYTAG"},{"av":"AV44UserInfoResponseUserEmailTag","fld":"vUSERINFORESPONSEUSEREMAILTAG"},{"av":"AV46UserInfoResponseUserExternalIdTag","fld":"vUSERINFORESPONSEUSEREXTERNALIDTAG"},{"av":"AV47UserInfoResponseUserFirstNameTag","fld":"vUSERINFORESPONSEUSERFIRSTNAMETAG"},{"av":"AV48UserInfoResponseUserGenderTag","fld":"vUSERINFORESPONSEUSERGENDERTAG"},{"av":"AV49UserInfoResponseUserGenderValues","fld":"vUSERINFORESPONSEUSERGENDERVALUES"},{"av":"AV50UserInfoResponseUserLanguageTag","fld":"vUSERINFORESPONSEUSERLANGUAGETAG"},{"av":"AV51UserInfoResponseUserLastNameGenAuto","fld":"vUSERINFORESPONSEUSERLASTNAMEGENAUTO"},{"av":"AV52UserInfoResponseUserLastNameTag","fld":"vUSERINFORESPONSEUSERLASTNAMETAG"},{"av":"AV53UserInfoResponseUserNameTag","fld":"vUSERINFORESPONSEUSERNAMETAG"},{"av":"AV54UserInfoResponseUserTimeZoneTag","fld":"vUSERINFORESPONSEUSERTIMEZONETAG"},{"av":"AV55UserInfoResponseUserURLImageTag","fld":"vUSERINFORESPONSEUSERURLIMAGETAG"},{"av":"AV56UserInfoResponseUserURLProfileTag","fld":"vUSERINFORESPONSEUSERURLPROFILETAG"},{"av":"AV11DynamicPropName","fld":"vDYNAMICPROPNAME","grid":235},{"av":"GRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_235","ctrl":"GRID","grid":235,"prop":"GridRC","grid":235},{"av":"AV12DynamicPropTag","fld":"vDYNAMICPROPTAG","grid":235}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV70CheckRequiredFieldsResult","fld":"vCHECKREQUIREDFIELDSRESULT"}]}""");
         setEventMetadata("VDELETEPROPERTY.CLICK","""{"handler":"E161H2","iparms":[{"av":"AV32Name","fld":"vNAME"}]""");
         setEventMetadata("VDELETEPROPERTY.CLICK",""","oparms":[{"av":"AV65DeleteProperty","fld":"vDELETEPROPERTY"},{"av":"edtavDeleteproperty_Visible","ctrl":"vDELETEPROPERTY","prop":"Visible"},{"av":"edtavDynamicpropname_Visible","ctrl":"vDYNAMICPROPNAME","prop":"Visible"},{"av":"edtavDynamicproptag_Visible","ctrl":"vDYNAMICPROPTAG","prop":"Visible"},{"av":"AV11DynamicPropName","fld":"vDYNAMICPROPNAME"},{"av":"AV12DynamicPropTag","fld":"vDYNAMICPROPTAG"}]}""");
         setEventMetadata("GRID_FIRSTPAGE","""{"handler":"subgrid_firstpage","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"Gx_mode","fld":"vMODE","pic":"@!"},{"av":"AV45UserInfoResponseUserErrorDescriptionTag","fld":"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG","hsh":true},{"av":"sPrefix"},{"av":"AV19IsEnable","fld":"vISENABLE"},{"av":"AV20IsRedirectBinding","fld":"vISREDIRECTBINDING"},{"av":"AV15ForceAuthn","fld":"vFORCEAUTHN"},{"av":"AV67DisableSingleLogout","fld":"vDISABLESINGLELOGOUT"},{"av":"AV51UserInfoResponseUserLastNameGenAuto","fld":"vUSERINFORESPONSEUSERLASTNAMEGENAUTO"}]""");
         setEventMetadata("GRID_FIRSTPAGE",""","oparms":[{"av":"edtavDeleteproperty_Enabled","ctrl":"vDELETEPROPERTY","prop":"Enabled"}]}""");
         setEventMetadata("GRID_PREVPAGE","""{"handler":"subgrid_previouspage","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"Gx_mode","fld":"vMODE","pic":"@!"},{"av":"AV45UserInfoResponseUserErrorDescriptionTag","fld":"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG","hsh":true},{"av":"sPrefix"},{"av":"AV19IsEnable","fld":"vISENABLE"},{"av":"AV20IsRedirectBinding","fld":"vISREDIRECTBINDING"},{"av":"AV15ForceAuthn","fld":"vFORCEAUTHN"},{"av":"AV67DisableSingleLogout","fld":"vDISABLESINGLELOGOUT"},{"av":"AV51UserInfoResponseUserLastNameGenAuto","fld":"vUSERINFORESPONSEUSERLASTNAMEGENAUTO"}]""");
         setEventMetadata("GRID_PREVPAGE",""","oparms":[{"av":"edtavDeleteproperty_Enabled","ctrl":"vDELETEPROPERTY","prop":"Enabled"}]}""");
         setEventMetadata("GRID_NEXTPAGE","""{"handler":"subgrid_nextpage","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"Gx_mode","fld":"vMODE","pic":"@!"},{"av":"AV45UserInfoResponseUserErrorDescriptionTag","fld":"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG","hsh":true},{"av":"sPrefix"},{"av":"AV19IsEnable","fld":"vISENABLE"},{"av":"AV20IsRedirectBinding","fld":"vISREDIRECTBINDING"},{"av":"AV15ForceAuthn","fld":"vFORCEAUTHN"},{"av":"AV67DisableSingleLogout","fld":"vDISABLESINGLELOGOUT"},{"av":"AV51UserInfoResponseUserLastNameGenAuto","fld":"vUSERINFORESPONSEUSERLASTNAMEGENAUTO"}]""");
         setEventMetadata("GRID_NEXTPAGE",""","oparms":[{"av":"edtavDeleteproperty_Enabled","ctrl":"vDELETEPROPERTY","prop":"Enabled"}]}""");
         setEventMetadata("GRID_LASTPAGE","""{"handler":"subgrid_lastpage","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"Gx_mode","fld":"vMODE","pic":"@!"},{"av":"AV45UserInfoResponseUserErrorDescriptionTag","fld":"vUSERINFORESPONSEUSERERRORDESCRIPTIONTAG","hsh":true},{"av":"sPrefix"},{"av":"AV19IsEnable","fld":"vISENABLE"},{"av":"AV20IsRedirectBinding","fld":"vISREDIRECTBINDING"},{"av":"AV15ForceAuthn","fld":"vFORCEAUTHN"},{"av":"AV67DisableSingleLogout","fld":"vDISABLESINGLELOGOUT"},{"av":"AV51UserInfoResponseUserLastNameGenAuto","fld":"vUSERINFORESPONSEUSERLASTNAMEGENAUTO"},{"av":"subGrid_Recordcount"}]""");
         setEventMetadata("GRID_LASTPAGE",""","oparms":[{"av":"edtavDeleteproperty_Enabled","ctrl":"vDELETEPROPERTY","prop":"Enabled"}]}""");
         setEventMetadata("VALIDV_FUNCTIONID","""{"handler":"Validv_Functionid","iparms":[]}""");
         setEventMetadata("VALIDV_NAMEIDPOLICYFORMAT","""{"handler":"Validv_Nameidpolicyformat","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Deleteproperty","iparms":[]}""");
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
         wcpOAV32Name = "";
         wcpOAV41TypeId = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV45UserInfoResponseUserErrorDescriptionTag = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         Grid_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         ClassString = "";
         StyleString = "";
         TempTags = "";
         AV16FunctionId = "";
         AV10Dsc = "";
         AV18Impersonate = "";
         AV40SmallImageName = "";
         AV7BigImageName = "";
         ucGxuitabspanel_tabs = new GXUserControl();
         lblGeneral_title_Jsonclick = "";
         AV30LocalSiteURL = "";
         AV37ServiceProviderEntityId = "";
         AV38IdentityProviderEntityID = "";
         ucDvpanel_unnamedtable7 = new GXUserControl();
         AV36SamlEndpointLocation = "";
         AV69NameIdPolicyFormat = "";
         AV6AuthnContext = "";
         ucDvpanel_unnamedtable8 = new GXUserControl();
         AV39SingleLogoutendpoint = "";
         lblCredentials_title_Jsonclick = "";
         ucDvpanel_unnamedtable4 = new GXUserControl();
         AV28KeyStPathCredential = "";
         AV29KeyStPwdCredential = "";
         AV21KeyAliasCredential = "";
         AV24KeyStoreCredential = "";
         ucDvpanel_unnamedtable5 = new GXUserControl();
         AV25KeyStoreFilePathTrustCred = "";
         AV26KeyStorePwdTrustCred = "";
         AV22KeyAliasTrustCred = "";
         AV27KeyStoreTrustCred = "";
         lblUserinfo_title_Jsonclick = "";
         AV44UserInfoResponseUserEmailTag = "";
         AV46UserInfoResponseUserExternalIdTag = "";
         AV53UserInfoResponseUserNameTag = "";
         AV47UserInfoResponseUserFirstNameTag = "";
         AV52UserInfoResponseUserLastNameTag = "";
         AV48UserInfoResponseUserGenderTag = "";
         AV49UserInfoResponseUserGenderValues = "";
         AV43UserInfoResponseUserBirthdayTag = "";
         AV55UserInfoResponseUserURLImageTag = "";
         AV56UserInfoResponseUserURLProfileTag = "";
         AV50UserInfoResponseUserLanguageTag = "";
         AV54UserInfoResponseUserTimeZoneTag = "";
         AV42UserInfoResponseErrorDescriptionTag = "";
         ucDvpanel_unnamedtable2 = new GXUserControl();
         GridContainer = new GXWebGrid( context);
         sStyleString = "";
         bttBtnadd_Jsonclick = "";
         bttBtnenter_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         ucGrid_empowerer = new GXUserControl();
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV11DynamicPropName = "";
         AV12DynamicPropTag = "";
         AV65DeleteProperty = "";
         AV75Deleteproperty_GXI = "";
         GXDecQS = "";
         AV5AuthenticationTypeSaml20 = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSaml20(context);
         AV17GAMPropertySimple = new GeneXus.Programs.genexussecurity.SdtGAMPropertySimple(context);
         GridRow = new GXWebRow();
         AV14Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV13Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlGx_mode = "";
         sCtrlAV32Name = "";
         sCtrlAV41TypeId = "";
         subGrid_Linesclass = "";
         ROClassString = "";
         sImgUrl = "";
         GridColumn = new GXWebColumn();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamwcauthenticationtypeentrysaml20__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamwcauthenticationtypeentrysaml20__default(),
            new Object[][] {
            }
         );
         /* GeneXus formulas. */
      }

      private short GRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short subGrid_Backcolorstyle ;
      private short nGXWrapped ;
      private short subGrid_Backstyle ;
      private short subGrid_Titlebackstyle ;
      private short subGrid_Allowselection ;
      private short subGrid_Allowhovering ;
      private short subGrid_Allowcollapsing ;
      private short subGrid_Collapsed ;
      private int nRC_GXsfl_235 ;
      private int subGrid_Recordcount ;
      private int subGrid_Rows ;
      private int nGXsfl_235_idx=1 ;
      private int Gxuitabspanel_tabs_Pagecount ;
      private int edtavName_Enabled ;
      private int edtavDsc_Enabled ;
      private int edtavImpersonate_Enabled ;
      private int edtavSmallimagename_Enabled ;
      private int edtavBigimagename_Enabled ;
      private int edtavLocalsiteurl_Enabled ;
      private int edtavServiceproviderentityid_Enabled ;
      private int edtavIdentityproviderentityid_Enabled ;
      private int edtavSamlendpointlocation_Enabled ;
      private int edtavAuthncontext_Enabled ;
      private int edtavSinglelogoutendpoint_Visible ;
      private int edtavSinglelogoutendpoint_Enabled ;
      private int edtavKeystpathcredential_Enabled ;
      private int edtavKeystpwdcredential_Enabled ;
      private int edtavKeyaliascredential_Enabled ;
      private int edtavKeystorecredential_Enabled ;
      private int edtavKeystorefilepathtrustcred_Enabled ;
      private int edtavKeystorepwdtrustcred_Enabled ;
      private int edtavKeyaliastrustcred_Enabled ;
      private int edtavKeystoretrustcred_Enabled ;
      private int edtavUserinforesponseuseremailtag_Enabled ;
      private int edtavUserinforesponseuserexternalidtag_Enabled ;
      private int edtavUserinforesponseusernametag_Enabled ;
      private int edtavUserinforesponseuserfirstnametag_Enabled ;
      private int edtavUserinforesponseuserlastnametag_Enabled ;
      private int edtavUserinforesponseusergendertag_Enabled ;
      private int edtavUserinforesponseusergendervalues_Enabled ;
      private int edtavUserinforesponseuserbirthdaytag_Enabled ;
      private int edtavUserinforesponseuserurlimagetag_Enabled ;
      private int edtavUserinforesponseuserurlprofiletag_Enabled ;
      private int edtavUserinforesponseuserlanguagetag_Enabled ;
      private int edtavUserinforesponseusertimezonetag_Enabled ;
      private int edtavUserinforesponseerrordescriptiontag_Enabled ;
      private int bttBtnadd_Visible ;
      private int bttBtnenter_Visible ;
      private int subGrid_Islastpage ;
      private int GRID_nGridOutOfScope ;
      private int AV74GXV1 ;
      private int edtavDeleteproperty_Visible ;
      private int edtavDynamicpropname_Enabled ;
      private int edtavDynamicproptag_Enabled ;
      private int edtavDynamicpropname_Visible ;
      private int edtavDynamicproptag_Visible ;
      private int edtavDeleteproperty_Enabled ;
      private int AV76GXV2 ;
      private int nGXsfl_235_fel_idx=1 ;
      private int AV78GXV3 ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long GRID_nFirstRecordOnPage ;
      private long GRID_nCurrentRecord ;
      private string Gx_mode ;
      private string AV32Name ;
      private string AV41TypeId ;
      private string wcpOGx_mode ;
      private string wcpOAV32Name ;
      private string wcpOAV41TypeId ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_235_idx="0001" ;
      private string AV45UserInfoResponseUserErrorDescriptionTag ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string Dvpanel_unnamedtable7_Width ;
      private string Dvpanel_unnamedtable7_Cls ;
      private string Dvpanel_unnamedtable7_Title ;
      private string Dvpanel_unnamedtable7_Iconposition ;
      private string Dvpanel_unnamedtable8_Width ;
      private string Dvpanel_unnamedtable8_Cls ;
      private string Dvpanel_unnamedtable8_Title ;
      private string Dvpanel_unnamedtable8_Iconposition ;
      private string Dvpanel_unnamedtable4_Width ;
      private string Dvpanel_unnamedtable4_Cls ;
      private string Dvpanel_unnamedtable4_Title ;
      private string Dvpanel_unnamedtable4_Iconposition ;
      private string Dvpanel_unnamedtable5_Width ;
      private string Dvpanel_unnamedtable5_Cls ;
      private string Dvpanel_unnamedtable5_Title ;
      private string Dvpanel_unnamedtable5_Iconposition ;
      private string Dvpanel_unnamedtable2_Width ;
      private string Dvpanel_unnamedtable2_Cls ;
      private string Dvpanel_unnamedtable2_Title ;
      private string Dvpanel_unnamedtable2_Iconposition ;
      private string Gxuitabspanel_tabs_Class ;
      private string Grid_empowerer_Gridinternalname ;
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
      private string AV16FunctionId ;
      private string cmbavFunctionid_Jsonclick ;
      private string edtavDsc_Internalname ;
      private string AV10Dsc ;
      private string edtavDsc_Jsonclick ;
      private string chkavIsenable_Internalname ;
      private string edtavImpersonate_Internalname ;
      private string AV18Impersonate ;
      private string edtavImpersonate_Jsonclick ;
      private string edtavSmallimagename_Internalname ;
      private string AV40SmallImageName ;
      private string edtavSmallimagename_Jsonclick ;
      private string edtavBigimagename_Internalname ;
      private string AV7BigImageName ;
      private string edtavBigimagename_Jsonclick ;
      private string Gxuitabspanel_tabs_Internalname ;
      private string lblGeneral_title_Internalname ;
      private string lblGeneral_title_Jsonclick ;
      private string divUnnamedtable6_Internalname ;
      private string edtavLocalsiteurl_Internalname ;
      private string edtavLocalsiteurl_Jsonclick ;
      private string edtavServiceproviderentityid_Internalname ;
      private string edtavServiceproviderentityid_Jsonclick ;
      private string edtavIdentityproviderentityid_Internalname ;
      private string edtavIdentityproviderentityid_Jsonclick ;
      private string chkavIsredirectbinding_Internalname ;
      private string Dvpanel_unnamedtable7_Internalname ;
      private string divUnnamedtable7_Internalname ;
      private string edtavSamlendpointlocation_Internalname ;
      private string edtavSamlendpointlocation_Jsonclick ;
      private string cmbavNameidpolicyformat_Internalname ;
      private string AV69NameIdPolicyFormat ;
      private string cmbavNameidpolicyformat_Jsonclick ;
      private string chkavForceauthn_Internalname ;
      private string edtavAuthncontext_Internalname ;
      private string AV6AuthnContext ;
      private string edtavAuthncontext_Jsonclick ;
      private string Dvpanel_unnamedtable8_Internalname ;
      private string divUnnamedtable8_Internalname ;
      private string chkavDisablesinglelogout_Internalname ;
      private string divSinglelogoutendpoint_cell_Internalname ;
      private string divSinglelogoutendpoint_cell_Class ;
      private string edtavSinglelogoutendpoint_Internalname ;
      private string edtavSinglelogoutendpoint_Jsonclick ;
      private string lblCredentials_title_Internalname ;
      private string lblCredentials_title_Jsonclick ;
      private string divUnnamedtable3_Internalname ;
      private string Dvpanel_unnamedtable4_Internalname ;
      private string divUnnamedtable4_Internalname ;
      private string edtavKeystpathcredential_Internalname ;
      private string edtavKeystpathcredential_Jsonclick ;
      private string edtavKeystpwdcredential_Internalname ;
      private string AV29KeyStPwdCredential ;
      private string edtavKeystpwdcredential_Jsonclick ;
      private string edtavKeyaliascredential_Internalname ;
      private string AV21KeyAliasCredential ;
      private string edtavKeyaliascredential_Jsonclick ;
      private string edtavKeystorecredential_Internalname ;
      private string AV24KeyStoreCredential ;
      private string edtavKeystorecredential_Jsonclick ;
      private string Dvpanel_unnamedtable5_Internalname ;
      private string divUnnamedtable5_Internalname ;
      private string edtavKeystorefilepathtrustcred_Internalname ;
      private string edtavKeystorefilepathtrustcred_Jsonclick ;
      private string edtavKeystorepwdtrustcred_Internalname ;
      private string AV26KeyStorePwdTrustCred ;
      private string edtavKeystorepwdtrustcred_Jsonclick ;
      private string edtavKeyaliastrustcred_Internalname ;
      private string AV22KeyAliasTrustCred ;
      private string edtavKeyaliastrustcred_Jsonclick ;
      private string edtavKeystoretrustcred_Internalname ;
      private string AV27KeyStoreTrustCred ;
      private string edtavKeystoretrustcred_Jsonclick ;
      private string lblUserinfo_title_Internalname ;
      private string lblUserinfo_title_Jsonclick ;
      private string divUnnamedtable1_Internalname ;
      private string edtavUserinforesponseuseremailtag_Internalname ;
      private string AV44UserInfoResponseUserEmailTag ;
      private string edtavUserinforesponseuseremailtag_Jsonclick ;
      private string edtavUserinforesponseuserexternalidtag_Internalname ;
      private string AV46UserInfoResponseUserExternalIdTag ;
      private string edtavUserinforesponseuserexternalidtag_Jsonclick ;
      private string edtavUserinforesponseusernametag_Internalname ;
      private string AV53UserInfoResponseUserNameTag ;
      private string edtavUserinforesponseusernametag_Jsonclick ;
      private string edtavUserinforesponseuserfirstnametag_Internalname ;
      private string AV47UserInfoResponseUserFirstNameTag ;
      private string edtavUserinforesponseuserfirstnametag_Jsonclick ;
      private string chkavUserinforesponseuserlastnamegenauto_Internalname ;
      private string edtavUserinforesponseuserlastnametag_Internalname ;
      private string AV52UserInfoResponseUserLastNameTag ;
      private string edtavUserinforesponseuserlastnametag_Jsonclick ;
      private string edtavUserinforesponseusergendertag_Internalname ;
      private string AV48UserInfoResponseUserGenderTag ;
      private string edtavUserinforesponseusergendertag_Jsonclick ;
      private string edtavUserinforesponseusergendervalues_Internalname ;
      private string edtavUserinforesponseusergendervalues_Jsonclick ;
      private string edtavUserinforesponseuserbirthdaytag_Internalname ;
      private string AV43UserInfoResponseUserBirthdayTag ;
      private string edtavUserinforesponseuserbirthdaytag_Jsonclick ;
      private string edtavUserinforesponseuserurlimagetag_Internalname ;
      private string AV55UserInfoResponseUserURLImageTag ;
      private string edtavUserinforesponseuserurlimagetag_Jsonclick ;
      private string edtavUserinforesponseuserurlprofiletag_Internalname ;
      private string AV56UserInfoResponseUserURLProfileTag ;
      private string edtavUserinforesponseuserurlprofiletag_Jsonclick ;
      private string edtavUserinforesponseuserlanguagetag_Internalname ;
      private string AV50UserInfoResponseUserLanguageTag ;
      private string edtavUserinforesponseuserlanguagetag_Jsonclick ;
      private string edtavUserinforesponseusertimezonetag_Internalname ;
      private string AV54UserInfoResponseUserTimeZoneTag ;
      private string edtavUserinforesponseusertimezonetag_Jsonclick ;
      private string edtavUserinforesponseerrordescriptiontag_Internalname ;
      private string AV42UserInfoResponseErrorDescriptionTag ;
      private string edtavUserinforesponseerrordescriptiontag_Jsonclick ;
      private string Dvpanel_unnamedtable2_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string sStyleString ;
      private string subGrid_Internalname ;
      private string bttBtnadd_Internalname ;
      private string bttBtnadd_Jsonclick ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Caption ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Jsonclick ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string Grid_empowerer_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavDynamicpropname_Internalname ;
      private string AV11DynamicPropName ;
      private string AV12DynamicPropTag ;
      private string edtavDynamicproptag_Internalname ;
      private string edtavDeleteproperty_Internalname ;
      private string GXDecQS ;
      private string edtavDeleteproperty_gximage ;
      private string edtavDeleteproperty_Tooltiptext ;
      private string sGXsfl_235_fel_idx="0001" ;
      private string sCtrlGx_mode ;
      private string sCtrlAV32Name ;
      private string sCtrlAV41TypeId ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string ROClassString ;
      private string edtavDynamicpropname_Jsonclick ;
      private string edtavDynamicproptag_Jsonclick ;
      private string sImgUrl ;
      private string edtavDeleteproperty_Jsonclick ;
      private string subGrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV19IsEnable ;
      private bool AV20IsRedirectBinding ;
      private bool AV15ForceAuthn ;
      private bool AV67DisableSingleLogout ;
      private bool AV51UserInfoResponseUserLastNameGenAuto ;
      private bool AV70CheckRequiredFieldsResult ;
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
      private bool Dvpanel_unnamedtable4_Autowidth ;
      private bool Dvpanel_unnamedtable4_Autoheight ;
      private bool Dvpanel_unnamedtable4_Collapsible ;
      private bool Dvpanel_unnamedtable4_Collapsed ;
      private bool Dvpanel_unnamedtable4_Showcollapseicon ;
      private bool Dvpanel_unnamedtable4_Autoscroll ;
      private bool Dvpanel_unnamedtable5_Autowidth ;
      private bool Dvpanel_unnamedtable5_Autoheight ;
      private bool Dvpanel_unnamedtable5_Collapsible ;
      private bool Dvpanel_unnamedtable5_Collapsed ;
      private bool Dvpanel_unnamedtable5_Showcollapseicon ;
      private bool Dvpanel_unnamedtable5_Autoscroll ;
      private bool Dvpanel_unnamedtable2_Autowidth ;
      private bool Dvpanel_unnamedtable2_Autoheight ;
      private bool Dvpanel_unnamedtable2_Collapsible ;
      private bool Dvpanel_unnamedtable2_Collapsed ;
      private bool Dvpanel_unnamedtable2_Showcollapseicon ;
      private bool Dvpanel_unnamedtable2_Autoscroll ;
      private bool Gxuitabspanel_tabs_Historymanagement ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_235_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool AV65DeleteProperty_IsBlob ;
      private string AV30LocalSiteURL ;
      private string AV37ServiceProviderEntityId ;
      private string AV38IdentityProviderEntityID ;
      private string AV36SamlEndpointLocation ;
      private string AV39SingleLogoutendpoint ;
      private string AV28KeyStPathCredential ;
      private string AV25KeyStoreFilePathTrustCred ;
      private string AV49UserInfoResponseUserGenderValues ;
      private string AV75Deleteproperty_GXI ;
      private string AV65DeleteProperty ;
      private GXWebGrid GridContainer ;
      private GXWebRow GridRow ;
      private GXWebColumn GridColumn ;
      private GXUserControl ucGxuitabspanel_tabs ;
      private GXUserControl ucDvpanel_unnamedtable7 ;
      private GXUserControl ucDvpanel_unnamedtable8 ;
      private GXUserControl ucDvpanel_unnamedtable4 ;
      private GXUserControl ucDvpanel_unnamedtable5 ;
      private GXUserControl ucDvpanel_unnamedtable2 ;
      private GXUserControl ucGrid_empowerer ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_Gx_mode ;
      private string aP1_Name ;
      private string aP2_TypeId ;
      private GXCombobox cmbavFunctionid ;
      private GXCheckbox chkavIsenable ;
      private GXCheckbox chkavIsredirectbinding ;
      private GXCombobox cmbavNameidpolicyformat ;
      private GXCheckbox chkavForceauthn ;
      private GXCheckbox chkavDisablesinglelogout ;
      private GXCheckbox chkavUserinforesponseuserlastnamegenauto ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSaml20 AV5AuthenticationTypeSaml20 ;
      private GeneXus.Programs.genexussecurity.SdtGAMPropertySimple AV17GAMPropertySimple ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV14Errors ;
      private IDataStoreProvider pr_default ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV13Error ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamwcauthenticationtypeentrysaml20__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamwcauthenticationtypeentrysaml20__default : DataStoreHelperBase, IDataStoreHelper
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
