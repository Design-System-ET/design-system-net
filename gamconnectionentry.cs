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
   public class gamconnectionentry : GXDataArea
   {
      public gamconnectionentry( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamconnectionentry( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref string aP0_Gx_mode ,
                           ref string aP1_pConnectionName )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV19pConnectionName = aP1_pConnectionName;
         ExecuteImpl();
         aP0_Gx_mode=this.Gx_mode;
         aP1_pConnectionName=this.AV19pConnectionName;
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridwwsysconns") == 0 )
            {
               gxnrGridwwsysconns_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridwwsysconns") == 0 )
            {
               gxgrGridwwsysconns_refresh_invoke( ) ;
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

      protected void gxnrGridwwsysconns_newrow_invoke( )
      {
         nRC_GXsfl_85 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_85"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_85_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_85_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_85_idx = GetPar( "sGXsfl_85_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridwwsysconns_newrow( ) ;
         /* End function gxnrGridwwsysconns_newrow_invoke */
      }

      protected void gxgrGridwwsysconns_refresh_invoke( )
      {
         subGridwwsysconns_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGridwwsysconns_Rows"), "."), 18, MidpointRounding.ToEven));
         Gx_mode = GetPar( "Mode");
         AV23CurrentConnectionKey = GetPar( "CurrentConnectionKey");
         AV12FileXML = GetPar( "FileXML");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridwwsysconns_refresh( subGridwwsysconns_Rows, Gx_mode, AV23CurrentConnectionKey, AV12FileXML) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridwwsysconns_refresh_invoke */
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
            return "gamconnectionentry_Execute" ;
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
         PA1V2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START1V2( ) ;
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
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
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
         GXEncryptionTmp = "gamconnectionentry.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.RTrim(AV19pConnectionName));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamconnectionentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vCURRENTCONNECTIONKEY", StringUtil.RTrim( AV23CurrentConnectionKey));
         GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTCONNECTIONKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23CurrentConnectionKey, "")), context));
         GxWebStd.gx_hidden_field( context, "vFILEXML", StringUtil.RTrim( AV12FileXML));
         GxWebStd.gx_hidden_field( context, "gxhash_vFILEXML", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV12FileXML, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_85", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_85), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "vCURRENTCONNECTIONKEY", StringUtil.RTrim( AV23CurrentConnectionKey));
         GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTCONNECTIONKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23CurrentConnectionKey, "")), context));
         GxWebStd.gx_hidden_field( context, "vFILEXML", StringUtil.RTrim( AV12FileXML));
         GxWebStd.gx_hidden_field( context, "gxhash_vFILEXML", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV12FileXML, "")), context));
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "subGridwwsysconns_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Width", StringUtil.RTrim( Dvpanel_unnamedtable3_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Autowidth", StringUtil.BoolToStr( Dvpanel_unnamedtable3_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Autoheight", StringUtil.BoolToStr( Dvpanel_unnamedtable3_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Cls", StringUtil.RTrim( Dvpanel_unnamedtable3_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Title", StringUtil.RTrim( Dvpanel_unnamedtable3_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Collapsible", StringUtil.BoolToStr( Dvpanel_unnamedtable3_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Collapsed", StringUtil.BoolToStr( Dvpanel_unnamedtable3_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_unnamedtable3_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Iconposition", StringUtil.RTrim( Dvpanel_unnamedtable3_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_UNNAMEDTABLE3_Autoscroll", StringUtil.BoolToStr( Dvpanel_unnamedtable3_Autoscroll));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Width", StringUtil.RTrim( Dvpanel_tbladdconnkey_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Autowidth", StringUtil.BoolToStr( Dvpanel_tbladdconnkey_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Autoheight", StringUtil.BoolToStr( Dvpanel_tbladdconnkey_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Cls", StringUtil.RTrim( Dvpanel_tbladdconnkey_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Title", StringUtil.RTrim( Dvpanel_tbladdconnkey_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Collapsible", StringUtil.BoolToStr( Dvpanel_tbladdconnkey_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Collapsed", StringUtil.BoolToStr( Dvpanel_tbladdconnkey_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_tbladdconnkey_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Iconposition", StringUtil.RTrim( Dvpanel_tbladdconnkey_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TBLADDCONNKEY_Autoscroll", StringUtil.BoolToStr( Dvpanel_tbladdconnkey_Autoscroll));
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
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_EMPOWERER_Gridinternalname", StringUtil.RTrim( Gridwwsysconns_empowerer_Gridinternalname));
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
            WE1V2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT1V2( ) ;
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
         GXEncryptionTmp = "gamconnectionentry.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.RTrim(AV19pConnectionName));
         return formatLink("gamconnectionentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "GAMConnectionEntry" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Connection", "") ;
      }

      protected void WB1V0( )
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMainTransaction", "start", "top", "", "", "div");
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
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblmaindata_Internalname, divTblmaindata_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-9", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_unnamedtable3.SetProperty("Width", Dvpanel_unnamedtable3_Width);
            ucDvpanel_unnamedtable3.SetProperty("AutoWidth", Dvpanel_unnamedtable3_Autowidth);
            ucDvpanel_unnamedtable3.SetProperty("AutoHeight", Dvpanel_unnamedtable3_Autoheight);
            ucDvpanel_unnamedtable3.SetProperty("Cls", Dvpanel_unnamedtable3_Cls);
            ucDvpanel_unnamedtable3.SetProperty("Title", Dvpanel_unnamedtable3_Title);
            ucDvpanel_unnamedtable3.SetProperty("Collapsible", Dvpanel_unnamedtable3_Collapsible);
            ucDvpanel_unnamedtable3.SetProperty("Collapsed", Dvpanel_unnamedtable3_Collapsed);
            ucDvpanel_unnamedtable3.SetProperty("ShowCollapseIcon", Dvpanel_unnamedtable3_Showcollapseicon);
            ucDvpanel_unnamedtable3.SetProperty("IconPosition", Dvpanel_unnamedtable3_Iconposition);
            ucDvpanel_unnamedtable3.SetProperty("AutoScroll", Dvpanel_unnamedtable3_Autoscroll);
            ucDvpanel_unnamedtable3.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_unnamedtable3_Internalname, "DVPANEL_UNNAMEDTABLE3Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_UNNAMEDTABLE3Container"+"UnnamedTable3"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavConnectionname_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavConnectionname_Internalname, context.GetMessage( "WWP_GAM_ConnectionName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'" + sGXsfl_85_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavConnectionname_Internalname, StringUtil.RTrim( AV8ConnectionName), StringUtil.RTrim( context.localUtil.Format( AV8ConnectionName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,25);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavConnectionname_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavConnectionname_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUsername_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUsername_Internalname, context.GetMessage( "WWP_GAM_UserName", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'',false,'" + sGXsfl_85_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUsername_Internalname, StringUtil.RTrim( AV20UserName), StringUtil.RTrim( context.localUtil.Format( AV20UserName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,30);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUsername_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavUsername_Enabled, 1, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUserpassword_cell_Internalname, 1, 0, "px", 0, "px", divUserpassword_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavUserpassword_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavUserpassword_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserpassword_Internalname, context.GetMessage( "WWP_GAM_UserPassword", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'" + sGXsfl_85_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserpassword_Internalname, StringUtil.RTrim( AV21UserPassword), StringUtil.RTrim( context.localUtil.Format( AV21UserPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,35);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUserpassword_Jsonclick, 0, "Attribute", "", "", "", "", edtavUserpassword_Visible, edtavUserpassword_Enabled, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedencryptionkey_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTextblockencryptionkey_cell_Internalname, 1, 0, "px", 0, "px", divTextblockencryptionkey_cell_Class, "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockencryptionkey_Internalname, context.GetMessage( "WWP_GAM_EncriptionKey", ""), "", "", lblTextblockencryptionkey_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            wb_table1_43_1V2( true) ;
         }
         else
         {
            wb_table1_43_1V2( false) ;
         }
         return  ;
      }

      protected void wb_table1_43_1V2e( bool wbgen )
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
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-9", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTblconnkeys_Internalname, divTblconnkeys_Visible, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDvpanel_tbladdconnkey.SetProperty("Width", Dvpanel_tbladdconnkey_Width);
            ucDvpanel_tbladdconnkey.SetProperty("AutoWidth", Dvpanel_tbladdconnkey_Autowidth);
            ucDvpanel_tbladdconnkey.SetProperty("AutoHeight", Dvpanel_tbladdconnkey_Autoheight);
            ucDvpanel_tbladdconnkey.SetProperty("Cls", Dvpanel_tbladdconnkey_Cls);
            ucDvpanel_tbladdconnkey.SetProperty("Title", Dvpanel_tbladdconnkey_Title);
            ucDvpanel_tbladdconnkey.SetProperty("Collapsible", Dvpanel_tbladdconnkey_Collapsible);
            ucDvpanel_tbladdconnkey.SetProperty("Collapsed", Dvpanel_tbladdconnkey_Collapsed);
            ucDvpanel_tbladdconnkey.SetProperty("ShowCollapseIcon", Dvpanel_tbladdconnkey_Showcollapseicon);
            ucDvpanel_tbladdconnkey.SetProperty("IconPosition", Dvpanel_tbladdconnkey_Iconposition);
            ucDvpanel_tbladdconnkey.SetProperty("AutoScroll", Dvpanel_tbladdconnkey_Autoscroll);
            ucDvpanel_tbladdconnkey.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_tbladdconnkey_Internalname, "DVPANEL_TBLADDCONNKEYContainer");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_TBLADDCONNKEYContainer"+"TblAddConnKey"+"\" style=\"display:none;\">") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divTbladdconnkey_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "DataContentCellFL DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtablenewconnectionkey_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblocknewconnectionkey_Internalname, context.GetMessage( "WWP_GAM_AddConnectionKey", ""), "", "", lblTextblocknewconnectionkey_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavNewconnectionkey_Internalname, context.GetMessage( "New Connection Key", ""), "col-sm-3 AttributeLabel", 0, true, "");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 68,'',false,'" + sGXsfl_85_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavNewconnectionkey_Internalname, StringUtil.RTrim( AV18NewConnectionKey), StringUtil.RTrim( context.localUtil.Format( AV18NewConnectionKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,68);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavNewconnectionkey_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavNewconnectionkey_Enabled, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 70,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnsavekey_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(85), 2, 0)+","+"null"+");", context.GetMessage( "WWP_GAM_SaveKey", ""), bttBtnsavekey_Jsonclick, 5, context.GetMessage( "WWP_GAM_SaveKey", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOSAVEKEY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroupGrouped", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 75,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseautomatickey_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(85), 2, 0)+","+"null"+");", context.GetMessage( "WWP_GAM_UseAutomaticKey", ""), bttBtnuseautomatickey_Jsonclick, 5, context.GetMessage( "WWP_GAM_UseAutomaticKey", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSEAUTOMATICKEY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 77,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnusecurrentkey_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(85), 2, 0)+","+"null"+");", context.GetMessage( "WWP_GAM_UseCurrentKey", ""), bttBtnusecurrentkey_Jsonclick, 5, context.GetMessage( "WWP_GAM_UseCurrentKey", ""), "", StyleString, ClassString, bttBtnusecurrentkey_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSECURRENTKEY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMConnectionEntry.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop HasGridEmpowerer", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridwwsysconnsContainer.SetWrapped(nGXWrapped);
            StartGridControl85( ) ;
         }
         if ( wbEnd == 85 )
         {
            wbEnd = 0;
            nRC_GXsfl_85 = (int)(nGXsfl_85_idx-1);
            if ( GridwwsysconnsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               GridwwsysconnsContainer.AddObjectProperty("GRIDWWSYSCONNS_nEOF", GRIDWWSYSCONNS_nEOF);
               GridwwsysconnsContainer.AddObjectProperty("GRIDWWSYSCONNS_nFirstRecordOnPage", GRIDWWSYSCONNS_nFirstRecordOnPage);
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridwwsysconnsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridwwsysconns", GridwwsysconnsContainer, subGridwwsysconns_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridwwsysconnsContainerData", GridwwsysconnsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridwwsysconnsContainerData"+"V", GridwwsysconnsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridwwsysconnsContainerData"+"V"+"\" value='"+GridwwsysconnsContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellFL DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", edtavConnectionfilexml_Visible, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavConnectionfilexml_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavConnectionfilexml_Internalname, context.GetMessage( "WWP_GAM_ConnectionFileXML", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Multiple line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 94,'',false,'" + sGXsfl_85_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_html_textarea( context, edtavConnectionfilexml_Internalname, StringUtil.RTrim( AV6ConnectionFileXML), "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,94);\"", 0, edtavConnectionfilexml_Visible, edtavConnectionfilexml_Enabled, 0, 80, "chr", 10, "row", 0, StyleString, ClassString, "", "", "2000", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_GAMConnectionEntry.htm");
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
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 99,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(85), 2, 0)+","+"null"+");", bttBtnenter_Caption, bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtnenter_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 101,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuacancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(85), 2, 0)+","+"null"+");", context.GetMessage( "GX_BtnCancel", ""), bttBtnuacancel_Jsonclick, 5, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUACANCEL\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMConnectionEntry.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 105,'',false,'" + sGXsfl_85_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavFileconnectionkey_Internalname, StringUtil.RTrim( AV25FileConnectionKey), StringUtil.RTrim( context.localUtil.Format( AV25FileConnectionKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,105);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavFileconnectionkey_Jsonclick, 0, "Attribute", "", "", "", "", edtavFileconnectionkey_Visible, 1, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMGUID", "start", true, "", "HLP_GAMConnectionEntry.htm");
            /* Single line edit */
            GxWebStd.gx_single_line_edit( context, edtavPconnectionname_Internalname, StringUtil.RTrim( AV19pConnectionName), StringUtil.RTrim( context.localUtil.Format( AV19pConnectionName, "")), "", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavPconnectionname_Jsonclick, 0, "Attribute", "", "", "", "", edtavPconnectionname_Visible, 0, 0, "text", "", 0, "px", 1, "row", 254, 0, 0, 0, 0, -1, -1, true, "GeneXusSecurityCommon\\GAMDescriptionLong", "start", true, "", "HLP_GAMConnectionEntry.htm");
            /* User Defined Control */
            ucGridwwsysconns_empowerer.Render(context, "wwp.gridempowerer", Gridwwsysconns_empowerer_Internalname, "GRIDWWSYSCONNS_EMPOWERERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 85 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridwwsysconnsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  GridwwsysconnsContainer.AddObjectProperty("GRIDWWSYSCONNS_nEOF", GRIDWWSYSCONNS_nEOF);
                  GridwwsysconnsContainer.AddObjectProperty("GRIDWWSYSCONNS_nFirstRecordOnPage", GRIDWWSYSCONNS_nFirstRecordOnPage);
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridwwsysconnsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridwwsysconns", GridwwsysconnsContainer, subGridwwsysconns_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridwwsysconnsContainerData", GridwwsysconnsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridwwsysconnsContainerData"+"V", GridwwsysconnsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridwwsysconnsContainerData"+"V"+"\" value='"+GridwwsysconnsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START1V2( )
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
         Form.Meta.addItem("description", context.GetMessage( "Connection", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP1V0( ) ;
      }

      protected void WS1V2( )
      {
         START1V2( ) ;
         EVT1V2( ) ;
      }

      protected void EVT1V2( )
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
                           else if ( StringUtil.StrCmp(sEvt, "'DOUACANCEL'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoUACancel' */
                              E111V2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOUSEAUTOMATICKEY'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoUseAutomaticKey' */
                              E121V2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOUSECURRENTKEY'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoUseCurrentKey' */
                              E131V2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOSAVEKEY'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoSaveKey' */
                              E141V2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOGENKEY'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoGenKey' */
                              E151V2 ();
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
                                    E161V2 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDWWSYSCONNSPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              sEvt = cgiGet( "GRIDWWSYSCONNSPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subgridwwsysconns_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subgridwwsysconns_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subgridwwsysconns_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subgridwwsysconns_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "GRIDWWSYSCONNS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "'WWCONNKEYS'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "VDELETECONNECTION.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 21), "VCONNECTIONFILE.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 21), "VCONNECTIONFILE.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "VDELETECONNECTION.CLICK") == 0 ) )
                           {
                              nGXsfl_85_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_85_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_85_idx), 4, 0), 4, "0");
                              SubsflControlProps_852( ) ;
                              AV7ConnectionKey = cgiGet( edtavConnectionkey_Internalname);
                              AssignAttri("", false, edtavConnectionkey_Internalname, AV7ConnectionKey);
                              GxWebStd.gx_hidden_field( context, "gxhash_vCONNECTIONKEY"+"_"+sGXsfl_85_idx, GetSecureSignedToken( sGXsfl_85_idx, StringUtil.RTrim( context.localUtil.Format( AV7ConnectionKey, "")), context));
                              AV24IsCurrentKey = cgiGet( edtavIscurrentkey_Internalname);
                              AssignAttri("", false, edtavIscurrentkey_Internalname, AV24IsCurrentKey);
                              AV32ConnectionFile = cgiGet( edtavConnectionfile_Internalname);
                              AssignAttri("", false, edtavConnectionfile_Internalname, AV32ConnectionFile);
                              AV33DeleteConnection = cgiGet( edtavDeleteconnection_Internalname);
                              AssignAttri("", false, edtavDeleteconnection_Internalname, AV33DeleteConnection);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E171V2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E181V2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDWWSYSCONNS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridwwsysconns.Load */
                                    E191V2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'WWCONNKEYS'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'WWConnKeys' */
                                    E201V2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VDELETECONNECTION.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E211V2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VCONNECTIONFILE.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E221V2 ();
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

      protected void WE1V2( )
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

      protected void PA1V2( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamconnectionentry.aspx")), "gamconnectionentry.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamconnectionentry.aspx")))) ;
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
                        AV19pConnectionName = GetPar( "pConnectionName");
                        AssignAttri("", false, "AV19pConnectionName", AV19pConnectionName);
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
               GX_FocusControl = edtavConnectionname_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridwwsysconns_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_852( ) ;
         while ( nGXsfl_85_idx <= nRC_GXsfl_85 )
         {
            sendrow_852( ) ;
            nGXsfl_85_idx = ((subGridwwsysconns_Islastpage==1)&&(nGXsfl_85_idx+1>subGridwwsysconns_fnc_Recordsperpage( )) ? 1 : nGXsfl_85_idx+1);
            sGXsfl_85_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_85_idx), 4, 0), 4, "0");
            SubsflControlProps_852( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridwwsysconnsContainer)) ;
         /* End function gxnrGridwwsysconns_newrow */
      }

      protected void gxgrGridwwsysconns_refresh( int subGridwwsysconns_Rows ,
                                                 string Gx_mode ,
                                                 string AV23CurrentConnectionKey ,
                                                 string AV12FileXML )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDWWSYSCONNS_nCurrentRecord = 0;
         RF1V2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridwwsysconns_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_vCONNECTIONKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV7ConnectionKey, "")), context));
         GxWebStd.gx_hidden_field( context, "vCONNECTIONKEY", StringUtil.RTrim( AV7ConnectionKey));
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
         RF1V2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavConnectionkey_Enabled = 0;
         edtavIscurrentkey_Enabled = 0;
         edtavConnectionfile_Enabled = 0;
         edtavDeleteconnection_Enabled = 0;
      }

      protected void RF1V2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridwwsysconnsContainer.ClearRows();
         }
         wbStart = 85;
         /* Execute user event: Refresh */
         E181V2 ();
         nGXsfl_85_idx = 1;
         sGXsfl_85_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_85_idx), 4, 0), 4, "0");
         SubsflControlProps_852( ) ;
         bGXsfl_85_Refreshing = true;
         GridwwsysconnsContainer.AddObjectProperty("GridName", "Gridwwsysconns");
         GridwwsysconnsContainer.AddObjectProperty("CmpContext", "");
         GridwwsysconnsContainer.AddObjectProperty("InMasterPage", "false");
         GridwwsysconnsContainer.AddObjectProperty("Class", "GridNoBorder WorkWith");
         GridwwsysconnsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridwwsysconnsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridwwsysconnsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Backcolorstyle), 1, 0, ".", "")));
         GridwwsysconnsContainer.PageSize = subGridwwsysconns_fnc_Recordsperpage( );
         if ( subGridwwsysconns_Islastpage != 0 )
         {
            GRIDWWSYSCONNS_nFirstRecordOnPage = (long)(subGridwwsysconns_fnc_Recordcount( )-subGridwwsysconns_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nFirstRecordOnPage), 15, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("GRIDWWSYSCONNS_nFirstRecordOnPage", GRIDWWSYSCONNS_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_852( ) ;
            /* Execute user event: Gridwwsysconns.Load */
            E191V2 ();
            if ( ( subGridwwsysconns_Islastpage == 0 ) && ( GRIDWWSYSCONNS_nCurrentRecord > 0 ) && ( GRIDWWSYSCONNS_nGridOutOfScope == 0 ) && ( nGXsfl_85_idx == 1 ) )
            {
               GRIDWWSYSCONNS_nCurrentRecord = 0;
               GRIDWWSYSCONNS_nGridOutOfScope = 1;
               subgridwwsysconns_firstpage( ) ;
               /* Execute user event: Gridwwsysconns.Load */
               E191V2 ();
            }
            wbEnd = 85;
            WB1V0( ) ;
         }
         bGXsfl_85_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes1V2( )
      {
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "vCURRENTCONNECTIONKEY", StringUtil.RTrim( AV23CurrentConnectionKey));
         GxWebStd.gx_hidden_field( context, "gxhash_vCURRENTCONNECTIONKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23CurrentConnectionKey, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vCONNECTIONKEY"+"_"+sGXsfl_85_idx, GetSecureSignedToken( sGXsfl_85_idx, StringUtil.RTrim( context.localUtil.Format( AV7ConnectionKey, "")), context));
         GxWebStd.gx_hidden_field( context, "vFILEXML", StringUtil.RTrim( AV12FileXML));
         GxWebStd.gx_hidden_field( context, "gxhash_vFILEXML", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV12FileXML, "")), context));
      }

      protected int subGridwwsysconns_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridwwsysconns_fnc_Recordcount( )
      {
         return (int)(((subGridwwsysconns_Recordcount==0) ? GRIDWWSYSCONNS_nFirstRecordOnPage+1 : subGridwwsysconns_Recordcount)) ;
      }

      protected int subGridwwsysconns_fnc_Recordsperpage( )
      {
         if ( subGridwwsysconns_Rows > 0 )
         {
            return subGridwwsysconns_Rows*1 ;
         }
         else
         {
            return (int)(-1) ;
         }
      }

      protected int subGridwwsysconns_fnc_Currentpage( )
      {
         return (int)(((subGridwwsysconns_Islastpage==1) ? NumberUtil.Int( (long)(Math.Round(subGridwwsysconns_fnc_Recordcount( )/ (decimal)(subGridwwsysconns_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+((((int)((subGridwwsysconns_fnc_Recordcount( )) % (subGridwwsysconns_fnc_Recordsperpage( ))))==0) ? 0 : 1) : NumberUtil.Int( (long)(Math.Round(GRIDWWSYSCONNS_nFirstRecordOnPage/ (decimal)(subGridwwsysconns_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1)) ;
      }

      protected short subgridwwsysconns_firstpage( )
      {
         GRIDWWSYSCONNS_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridwwsysconns_refresh( subGridwwsysconns_Rows, Gx_mode, AV23CurrentConnectionKey, AV12FileXML) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridwwsysconns_nextpage( )
      {
         if ( GRIDWWSYSCONNS_nEOF == 0 )
         {
            GRIDWWSYSCONNS_nFirstRecordOnPage = (long)(GRIDWWSYSCONNS_nFirstRecordOnPage+subGridwwsysconns_fnc_Recordsperpage( ));
         }
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nFirstRecordOnPage), 15, 0, ".", "")));
         GridwwsysconnsContainer.AddObjectProperty("GRIDWWSYSCONNS_nFirstRecordOnPage", GRIDWWSYSCONNS_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGridwwsysconns_refresh( subGridwwsysconns_Rows, Gx_mode, AV23CurrentConnectionKey, AV12FileXML) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRIDWWSYSCONNS_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgridwwsysconns_previouspage( )
      {
         if ( GRIDWWSYSCONNS_nFirstRecordOnPage >= subGridwwsysconns_fnc_Recordsperpage( ) )
         {
            GRIDWWSYSCONNS_nFirstRecordOnPage = (long)(GRIDWWSYSCONNS_nFirstRecordOnPage-subGridwwsysconns_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridwwsysconns_refresh( subGridwwsysconns_Rows, Gx_mode, AV23CurrentConnectionKey, AV12FileXML) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridwwsysconns_lastpage( )
      {
         subGridwwsysconns_Islastpage = 1;
         if ( isFullAjaxMode( ) )
         {
            gxgrGridwwsysconns_refresh( subGridwwsysconns_Rows, Gx_mode, AV23CurrentConnectionKey, AV12FileXML) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgridwwsysconns_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRIDWWSYSCONNS_nFirstRecordOnPage = (long)(subGridwwsysconns_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRIDWWSYSCONNS_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridwwsysconns_refresh( subGridwwsysconns_Rows, Gx_mode, AV23CurrentConnectionKey, AV12FileXML) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavConnectionkey_Enabled = 0;
         edtavIscurrentkey_Enabled = 0;
         edtavConnectionfile_Enabled = 0;
         edtavDeleteconnection_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1V0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E171V2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            nRC_GXsfl_85 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_85"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRIDWWSYSCONNS_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDWWSYSCONNS_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRIDWWSYSCONNS_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDWWSYSCONNS_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGridwwsysconns_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "subGridwwsysconns_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGridwwsysconns_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDWWSYSCONNS_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Rows), 6, 0, ".", "")));
            Dvpanel_unnamedtable3_Width = cgiGet( "DVPANEL_UNNAMEDTABLE3_Width");
            Dvpanel_unnamedtable3_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE3_Autowidth"));
            Dvpanel_unnamedtable3_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE3_Autoheight"));
            Dvpanel_unnamedtable3_Cls = cgiGet( "DVPANEL_UNNAMEDTABLE3_Cls");
            Dvpanel_unnamedtable3_Title = cgiGet( "DVPANEL_UNNAMEDTABLE3_Title");
            Dvpanel_unnamedtable3_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE3_Collapsible"));
            Dvpanel_unnamedtable3_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE3_Collapsed"));
            Dvpanel_unnamedtable3_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE3_Showcollapseicon"));
            Dvpanel_unnamedtable3_Iconposition = cgiGet( "DVPANEL_UNNAMEDTABLE3_Iconposition");
            Dvpanel_unnamedtable3_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_UNNAMEDTABLE3_Autoscroll"));
            Dvpanel_tbladdconnkey_Width = cgiGet( "DVPANEL_TBLADDCONNKEY_Width");
            Dvpanel_tbladdconnkey_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_TBLADDCONNKEY_Autowidth"));
            Dvpanel_tbladdconnkey_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_TBLADDCONNKEY_Autoheight"));
            Dvpanel_tbladdconnkey_Cls = cgiGet( "DVPANEL_TBLADDCONNKEY_Cls");
            Dvpanel_tbladdconnkey_Title = cgiGet( "DVPANEL_TBLADDCONNKEY_Title");
            Dvpanel_tbladdconnkey_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_TBLADDCONNKEY_Collapsible"));
            Dvpanel_tbladdconnkey_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_TBLADDCONNKEY_Collapsed"));
            Dvpanel_tbladdconnkey_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_TBLADDCONNKEY_Showcollapseicon"));
            Dvpanel_tbladdconnkey_Iconposition = cgiGet( "DVPANEL_TBLADDCONNKEY_Iconposition");
            Dvpanel_tbladdconnkey_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_TBLADDCONNKEY_Autoscroll"));
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
            Gridwwsysconns_empowerer_Gridinternalname = cgiGet( "GRIDWWSYSCONNS_EMPOWERER_Gridinternalname");
            /* Read variables values. */
            AV8ConnectionName = cgiGet( edtavConnectionname_Internalname);
            AssignAttri("", false, "AV8ConnectionName", AV8ConnectionName);
            AV20UserName = cgiGet( edtavUsername_Internalname);
            AssignAttri("", false, "AV20UserName", AV20UserName);
            AV21UserPassword = cgiGet( edtavUserpassword_Internalname);
            AssignAttri("", false, "AV21UserPassword", AV21UserPassword);
            AV9EncryptionKey = cgiGet( edtavEncryptionkey_Internalname);
            AssignAttri("", false, "AV9EncryptionKey", AV9EncryptionKey);
            AV18NewConnectionKey = cgiGet( edtavNewconnectionkey_Internalname);
            AssignAttri("", false, "AV18NewConnectionKey", AV18NewConnectionKey);
            AV6ConnectionFileXML = cgiGet( edtavConnectionfilexml_Internalname);
            AssignAttri("", false, "AV6ConnectionFileXML", AV6ConnectionFileXML);
            AV25FileConnectionKey = cgiGet( edtavFileconnectionkey_Internalname);
            AssignAttri("", false, "AV25FileConnectionKey", AV25FileConnectionKey);
            AV19pConnectionName = cgiGet( edtavPconnectionname_Internalname);
            AssignAttri("", false, "AV19pConnectionName", AV19pConnectionName);
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
         E171V2 ();
         if (returnInSub) return;
      }

      protected void E171V2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV8ConnectionName = AV19pConnectionName;
         AssignAttri("", false, "AV8ConnectionName", AV8ConnectionName);
         GX_FocusControl = edtavConnectionname_Internalname;
         AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         context.DoAjaxSetFocus(GX_FocusControl);
         edtavConnectionname_Enabled = 0;
         AssignProp("", false, edtavConnectionname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavConnectionname_Enabled), 5, 0), true);
         edtavUsername_Enabled = 0;
         AssignProp("", false, edtavUsername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUsername_Enabled), 5, 0), true);
         divTblconnkeys_Visible = 0;
         AssignProp("", false, divTblconnkeys_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblconnkeys_Visible), 5, 0), true);
         if ( StringUtil.StrCmp(Gx_mode, "INS") == 0 )
         {
            edtavConnectionname_Enabled = 1;
            AssignProp("", false, edtavConnectionname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavConnectionname_Enabled), 5, 0), true);
            edtavUsername_Enabled = 1;
            AssignProp("", false, edtavUsername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUsername_Enabled), 5, 0), true);
         }
         else
         {
            AV13GAMRepositoryConnection.load( AV8ConnectionName);
            AV20UserName = AV13GAMRepositoryConnection.gxTpr_Username;
            AssignAttri("", false, "AV20UserName", AV20UserName);
            AV21UserPassword = AV13GAMRepositoryConnection.gxTpr_Userpassword;
            AssignAttri("", false, "AV21UserPassword", AV21UserPassword);
            AV9EncryptionKey = AV13GAMRepositoryConnection.gxTpr_Key;
            AssignAttri("", false, "AV9EncryptionKey", AV9EncryptionKey);
         }
         if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
         {
            edtavUsername_Enabled = 1;
            AssignProp("", false, edtavUsername_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavUsername_Enabled), 5, 0), true);
         }
         if ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 )
         {
            bttBtnenter_Caption = "Delete";
            AssignProp("", false, bttBtnenter_Internalname, "Caption", bttBtnenter_Caption, true);
         }
         if ( StringUtil.StrCmp(Gx_mode, "KEY") == 0 )
         {
            Form.Caption = StringUtil.Format( context.GetMessage( "WWP_GAM_ConnectionKeyList", ""), StringUtil.Upper( AV8ConnectionName), "", "", "", "", "", "", "", "");
            AssignProp("", false, "FORM", "Caption", Form.Caption, true);
            divTblmaindata_Visible = 0;
            AssignProp("", false, divTblmaindata_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblmaindata_Visible), 5, 0), true);
            divTblconnkeys_Visible = 1;
            AssignProp("", false, divTblconnkeys_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblconnkeys_Visible), 5, 0), true);
            edtavConnectionfilexml_Visible = 0;
            AssignProp("", false, edtavConnectionfilexml_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavConnectionfilexml_Visible), 5, 0), true);
            edtavFileconnectionkey_Visible = 0;
            AssignProp("", false, edtavFileconnectionkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavFileconnectionkey_Visible), 5, 0), true);
         }
         else
         {
            Form.Caption = context.GetMessage( "WWP_GAM_Connections", "");
            AssignProp("", false, "FORM", "Caption", Form.Caption, true);
         }
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S112 ();
         if (returnInSub) return;
         edtavFileconnectionkey_Visible = 0;
         AssignProp("", false, edtavFileconnectionkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavFileconnectionkey_Visible), 5, 0), true);
         edtavPconnectionname_Visible = 0;
         AssignProp("", false, edtavPconnectionname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavPconnectionname_Visible), 5, 0), true);
         Gridwwsysconns_empowerer_Gridinternalname = subGridwwsysconns_Internalname;
         ucGridwwsysconns_empowerer.SendProperty(context, "", false, Gridwwsysconns_empowerer_Internalname, "GridInternalName", Gridwwsysconns_empowerer_Gridinternalname);
         subGridwwsysconns_Rows = 10;
         GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Rows), 6, 0, ".", "")));
      }

      protected void E181V2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         /* Execute user subroutine: 'CHECKSECURITYFORACTIONS' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      private void E191V2( )
      {
         /* Gridwwsysconns_Load Routine */
         returnInSub = false;
         AV16isOk = new GeneXus.Programs.genexussecurity.SdtGAM(context).getconnectionkey(out  AV23CurrentConnectionKey);
         AV27hasCurrentKey = false;
         AV38GXV2 = 1;
         AV37GXV1 = AV13GAMRepositoryConnection.getkeys(out  AV11Errors);
         while ( AV38GXV2 <= AV37GXV1.Count )
         {
            AV14GAMSystemConnection = ((GeneXus.Programs.genexussecurity.SdtGAMSystemConnection)AV37GXV1.Item(AV38GXV2));
            AV7ConnectionKey = AV14GAMSystemConnection.gxTpr_Key;
            AssignAttri("", false, edtavConnectionkey_Internalname, AV7ConnectionKey);
            GxWebStd.gx_hidden_field( context, "gxhash_vCONNECTIONKEY"+"_"+sGXsfl_85_idx, GetSecureSignedToken( sGXsfl_85_idx, StringUtil.RTrim( context.localUtil.Format( AV7ConnectionKey, "")), context));
            if ( StringUtil.StrCmp(StringUtil.Trim( AV14GAMSystemConnection.gxTpr_Key), StringUtil.Trim( AV23CurrentConnectionKey)) == 0 )
            {
               AV27hasCurrentKey = true;
               AV24IsCurrentKey = context.GetMessage( "WWP_GAM_Current", "");
               AssignAttri("", false, edtavIscurrentkey_Internalname, AV24IsCurrentKey);
            }
            else
            {
               AV24IsCurrentKey = "";
               AssignAttri("", false, edtavIscurrentkey_Internalname, AV24IsCurrentKey);
            }
            AV32ConnectionFile = "<i class=\"fa fa-file\"></i>";
            AssignAttri("", false, edtavConnectionfile_Internalname, AV32ConnectionFile);
            AV33DeleteConnection = "<i class=\"fa fa-times\"></i>";
            AssignAttri("", false, edtavDeleteconnection_Internalname, AV33DeleteConnection);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 85;
            }
            if ( ( subGridwwsysconns_Islastpage == 1 ) || ( subGridwwsysconns_Rows == 0 ) || ( ( GRIDWWSYSCONNS_nCurrentRecord >= GRIDWWSYSCONNS_nFirstRecordOnPage ) && ( GRIDWWSYSCONNS_nCurrentRecord < GRIDWWSYSCONNS_nFirstRecordOnPage + subGridwwsysconns_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_852( ) ;
            }
            GRIDWWSYSCONNS_nEOF = (short)(((GRIDWWSYSCONNS_nCurrentRecord<GRIDWWSYSCONNS_nFirstRecordOnPage+subGridwwsysconns_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRIDWWSYSCONNS_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWWSYSCONNS_nEOF), 1, 0, ".", "")));
            GRIDWWSYSCONNS_nCurrentRecord = (long)(GRIDWWSYSCONNS_nCurrentRecord+1);
            subGridwwsysconns_Recordcount = (int)(GRIDWWSYSCONNS_nCurrentRecord);
            if ( isFullAjaxMode( ) && ! bGXsfl_85_Refreshing )
            {
               DoAjaxLoad(85, GridwwsysconnsRow);
            }
            AV38GXV2 = (int)(AV38GXV2+1);
         }
         if ( AV27hasCurrentKey )
         {
            bttBtnusecurrentkey_Visible = 0;
            AssignProp("", false, bttBtnusecurrentkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnusecurrentkey_Visible), 5, 0), true);
         }
         /*  Sending Event outputs  */
      }

      protected void E111V2( )
      {
         /* 'DoUACancel' Routine */
         returnInSub = false;
         CallWebObject(formatLink("gamwwconnections.aspx") );
         context.wjLocDisableFrm = 1;
      }

      protected void E121V2( )
      {
         /* 'DoUseAutomaticKey' Routine */
         returnInSub = false;
         AV15GXGUID = Guid.NewGuid( );
         AV18NewConnectionKey = StringUtil.Trim( AV15GXGUID.ToString());
         AssignAttri("", false, "AV18NewConnectionKey", AV18NewConnectionKey);
         /*  Sending Event outputs  */
      }

      protected void E131V2( )
      {
         /* 'DoUseCurrentKey' Routine */
         returnInSub = false;
         AV16isOk = new GeneXus.Programs.genexussecurity.SdtGAM(context).getconnectionkey(out  AV23CurrentConnectionKey);
         AV18NewConnectionKey = StringUtil.Trim( AV23CurrentConnectionKey);
         AssignAttri("", false, "AV18NewConnectionKey", AV18NewConnectionKey);
         /*  Sending Event outputs  */
      }

      protected void E141V2( )
      {
         /* 'DoSaveKey' Routine */
         returnInSub = false;
         if ( new GeneXus.Programs.genexussecurity.SdtGAM(context).addconnectiontofilekey(AV18NewConnectionKey, AV8ConnectionName, out  AV11Errors) )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamconnectionentry.aspx"+UrlEncode(StringUtil.RTrim("KEY")) + "," + UrlEncode(StringUtil.RTrim(AV19pConnectionName));
            CallWebObject(formatLink("gamconnectionentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            /* Execute user subroutine: 'DISPLAYMESSAGES' */
            S132 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
      }

      protected void E151V2( )
      {
         /* 'DoGenKey' Routine */
         returnInSub = false;
         AV9EncryptionKey = Crypto.GetEncryptionKey( );
         AssignAttri("", false, "AV9EncryptionKey", AV9EncryptionKey);
         /*  Sending Event outputs  */
      }

      protected void S122( )
      {
         /* 'CHECKSECURITYFORACTIONS' Routine */
         returnInSub = false;
         if ( ! ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) ) )
         {
            bttBtnenter_Visible = 0;
            AssignProp("", false, bttBtnenter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnenter_Visible), 5, 0), true);
         }
         if ( ! ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) ) )
         {
            bttBtngenkey_Visible = 0;
            AssignProp("", false, bttBtngenkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtngenkey_Visible), 5, 0), true);
         }
      }

      protected void S112( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
         if ( ! ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) ) )
         {
            edtavUserpassword_Visible = 0;
            AssignProp("", false, edtavUserpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUserpassword_Visible), 5, 0), true);
            divUserpassword_cell_Class = "Invisible";
            AssignProp("", false, divUserpassword_cell_Internalname, "Class", divUserpassword_cell_Class, true);
         }
         else
         {
            edtavUserpassword_Visible = 1;
            AssignProp("", false, edtavUserpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUserpassword_Visible), 5, 0), true);
            divUserpassword_cell_Class = "col-xs-12 DataContentCell DscTop";
            AssignProp("", false, divUserpassword_cell_Internalname, "Class", divUserpassword_cell_Class, true);
         }
         if ( ! ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) ) )
         {
            edtavEncryptionkey_Visible = 0;
            AssignProp("", false, edtavEncryptionkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEncryptionkey_Visible), 5, 0), true);
            cellEncryptionkey_cell_Class = "Invisible";
            AssignProp("", false, cellEncryptionkey_cell_Internalname, "Class", cellEncryptionkey_cell_Class, true);
            divTextblockencryptionkey_cell_Class = "Invisible";
            AssignProp("", false, divTextblockencryptionkey_cell_Internalname, "Class", divTextblockencryptionkey_cell_Class, true);
         }
         else
         {
            edtavEncryptionkey_Visible = 1;
            AssignProp("", false, edtavEncryptionkey_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavEncryptionkey_Visible), 5, 0), true);
            cellEncryptionkey_cell_Class = "MergeDataCell";
            AssignProp("", false, cellEncryptionkey_cell_Internalname, "Class", cellEncryptionkey_cell_Class, true);
            divTextblockencryptionkey_cell_Class = "col-sm-12 MergeLabelCell";
            AssignProp("", false, divTextblockencryptionkey_cell_Internalname, "Class", divTextblockencryptionkey_cell_Class, true);
         }
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E161V2 ();
         if (returnInSub) return;
      }

      protected void E161V2( )
      {
         /* Enter Routine */
         returnInSub = false;
         AV13GAMRepositoryConnection.load( AV8ConnectionName);
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 ) )
         {
            AV13GAMRepositoryConnection.gxTpr_Username = AV20UserName;
            AV13GAMRepositoryConnection.gxTpr_Userpassword = AV21UserPassword;
            AV13GAMRepositoryConnection.gxTpr_Key = AV9EncryptionKey;
            AV13GAMRepositoryConnection.save();
            if ( AV13GAMRepositoryConnection.success() )
            {
               context.CommitDataStores("gamconnectionentry",pr_default);
               context.setWebReturnParms(new Object[] {(string)Gx_mode,(string)AV19pConnectionName});
               context.setWebReturnParmsMetadata(new Object[] {"Gx_mode","AV19pConnectionName"});
               context.wjLocDisableFrm = 1;
               context.nUserReturn = 1;
               returnInSub = true;
               if (true) return;
            }
            else
            {
               AV11Errors = AV13GAMRepositoryConnection.geterrors();
               /* Execute user subroutine: 'DISPLAYMESSAGES' */
               S132 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 )
         {
            AV13GAMRepositoryConnection.delete();
            if ( AV13GAMRepositoryConnection.success() )
            {
               context.CommitDataStores("gamconnectionentry",pr_default);
               CallWebObject(formatLink("gamwwconnections.aspx") );
               context.wjLocDisableFrm = 1;
            }
            else
            {
               AV11Errors = AV13GAMRepositoryConnection.geterrors();
               /* Execute user subroutine: 'DISPLAYMESSAGES' */
               S132 ();
               if (returnInSub) return;
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV13GAMRepositoryConnection", AV13GAMRepositoryConnection);
      }

      protected void E201V2( )
      {
         /* 'WWConnKeys' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamconnectionentry.aspx"+UrlEncode(StringUtil.RTrim("KEY")) + "," + UrlEncode(StringUtil.RTrim(AV19pConnectionName));
         CallWebObject(formatLink("gamconnectionentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void E211V2( )
      {
         /* Deleteconnection_Click Routine */
         returnInSub = false;
         if ( new GeneXus.Programs.genexussecurity.SdtGAM(context).deleteconnectionfromfilekey(AV7ConnectionKey, AV8ConnectionName, out  AV11Errors) )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "gamconnectionentry.aspx"+UrlEncode(StringUtil.RTrim("KEY")) + "," + UrlEncode(StringUtil.RTrim(AV19pConnectionName));
            CallWebObject(formatLink("gamconnectionentry.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
            context.wjLocDisableFrm = 1;
         }
         else
         {
            /* Execute user subroutine: 'DISPLAYMESSAGES' */
            S132 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
      }

      protected void E221V2( )
      {
         /* Connectionfile_Click Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV25FileConnectionKey, AV7ConnectionKey) == 0 )
         {
            AV25FileConnectionKey = "";
            AssignAttri("", false, "AV25FileConnectionKey", AV25FileConnectionKey);
            edtavConnectionfilexml_Visible = 0;
            AssignProp("", false, edtavConnectionfilexml_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavConnectionfilexml_Visible), 5, 0), true);
         }
         else
         {
            AV25FileConnectionKey = AV7ConnectionKey;
            AssignAttri("", false, "AV25FileConnectionKey", AV25FileConnectionKey);
            edtavConnectionfilexml_Visible = 1;
            AssignProp("", false, edtavConnectionfilexml_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavConnectionfilexml_Visible), 5, 0), true);
            AV6ConnectionFileXML = "";
            AssignAttri("", false, "AV6ConnectionFileXML", AV6ConnectionFileXML);
            new GeneXus.Programs.genexussecurity.SdtGAM(context).getconnectionxmlfromfilekey(AV8ConnectionName, AV7ConnectionKey, out  AV12FileXML, out  AV11Errors) ;
            if ( AV11Errors.Count == 0 )
            {
               AV6ConnectionFileXML = AV12FileXML;
               AssignAttri("", false, "AV6ConnectionFileXML", AV6ConnectionFileXML);
            }
            else
            {
               /* Execute user subroutine: 'DISPLAYMESSAGES' */
               S132 ();
               if (returnInSub) return;
            }
         }
         /*  Sending Event outputs  */
      }

      protected void S132( )
      {
         /* 'DISPLAYMESSAGES' Routine */
         returnInSub = false;
         AV39GXV3 = 1;
         while ( AV39GXV3 <= AV11Errors.Count )
         {
            AV10Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV11Errors.Item(AV39GXV3));
            if ( AV10Error.gxTpr_Code != 13 )
            {
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV10Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV10Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
            }
            AV39GXV3 = (int)(AV39GXV3+1);
         }
      }

      protected void wb_table1_43_1V2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablemergedencryptionkey_Internalname, tblTablemergedencryptionkey_Internalname, "", "TableMerged", 0, "", "", 0, 0, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td id=\""+cellEncryptionkey_cell_Internalname+"\"  class='"+cellEncryptionkey_cell_Class+"'>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavEncryptionkey_Internalname, context.GetMessage( "Encryption Key", ""), "gx-form-item AttributeLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 47,'',false,'" + sGXsfl_85_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavEncryptionkey_Internalname, StringUtil.RTrim( AV9EncryptionKey), StringUtil.RTrim( context.localUtil.Format( AV9EncryptionKey, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,47);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavEncryptionkey_Jsonclick, 0, "Attribute", "", "", "", "", edtavEncryptionkey_Visible, edtavEncryptionkey_Enabled, 0, "text", "", 32, "chr", 1, "row", 32, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMEncryptionKey", "start", true, "", "HLP_GAMConnectionEntry.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtngenkey_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(85), 2, 0)+","+"null"+");", context.GetMessage( "WWP_GAM_GenerateKey", ""), bttBtngenkey_Jsonclick, 5, context.GetMessage( "WWP_GAM_GenerateKey", ""), "", StyleString, ClassString, bttBtngenkey_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOGENKEY\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMConnectionEntry.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_43_1V2e( true) ;
         }
         else
         {
            wb_table1_43_1V2e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         Gx_mode = (string)getParm(obj,0);
         AssignAttri("", false, "Gx_mode", Gx_mode);
         AV19pConnectionName = (string)getParm(obj,1);
         AssignAttri("", false, "AV19pConnectionName", AV19pConnectionName);
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
         PA1V2( ) ;
         WS1V2( ) ;
         WE1V2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623592492", true, true);
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
         context.AddJavascriptSource("gamconnectionentry.js", "?2024121623592496", false, true);
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

      protected void SubsflControlProps_852( )
      {
         edtavConnectionkey_Internalname = "vCONNECTIONKEY_"+sGXsfl_85_idx;
         edtavIscurrentkey_Internalname = "vISCURRENTKEY_"+sGXsfl_85_idx;
         edtavConnectionfile_Internalname = "vCONNECTIONFILE_"+sGXsfl_85_idx;
         edtavDeleteconnection_Internalname = "vDELETECONNECTION_"+sGXsfl_85_idx;
      }

      protected void SubsflControlProps_fel_852( )
      {
         edtavConnectionkey_Internalname = "vCONNECTIONKEY_"+sGXsfl_85_fel_idx;
         edtavIscurrentkey_Internalname = "vISCURRENTKEY_"+sGXsfl_85_fel_idx;
         edtavConnectionfile_Internalname = "vCONNECTIONFILE_"+sGXsfl_85_fel_idx;
         edtavDeleteconnection_Internalname = "vDELETECONNECTION_"+sGXsfl_85_fel_idx;
      }

      protected void sendrow_852( )
      {
         sGXsfl_85_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_85_idx), 4, 0), 4, "0");
         SubsflControlProps_852( ) ;
         WB1V0( ) ;
         if ( ( subGridwwsysconns_Rows * 1 == 0 ) || ( nGXsfl_85_idx <= subGridwwsysconns_fnc_Recordsperpage( ) * 1 ) )
         {
            GridwwsysconnsRow = GXWebRow.GetNew(context,GridwwsysconnsContainer);
            if ( subGridwwsysconns_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGridwwsysconns_Backstyle = 0;
               if ( StringUtil.StrCmp(subGridwwsysconns_Class, "") != 0 )
               {
                  subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"Odd";
               }
            }
            else if ( subGridwwsysconns_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGridwwsysconns_Backstyle = 0;
               subGridwwsysconns_Backcolor = subGridwwsysconns_Allbackcolor;
               if ( StringUtil.StrCmp(subGridwwsysconns_Class, "") != 0 )
               {
                  subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"Uniform";
               }
            }
            else if ( subGridwwsysconns_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGridwwsysconns_Backstyle = 1;
               if ( StringUtil.StrCmp(subGridwwsysconns_Class, "") != 0 )
               {
                  subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"Odd";
               }
               subGridwwsysconns_Backcolor = (int)(0x0);
            }
            else if ( subGridwwsysconns_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGridwwsysconns_Backstyle = 1;
               if ( ((int)((nGXsfl_85_idx) % (2))) == 0 )
               {
                  subGridwwsysconns_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridwwsysconns_Class, "") != 0 )
                  {
                     subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"Even";
                  }
               }
               else
               {
                  subGridwwsysconns_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridwwsysconns_Class, "") != 0 )
                  {
                     subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"Odd";
                  }
               }
            }
            if ( GridwwsysconnsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"GridNoBorder WorkWith"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_85_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridwwsysconnsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 86,'',false,'" + sGXsfl_85_idx + "',85)\"";
            ROClassString = "Attribute";
            GridwwsysconnsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavConnectionkey_Internalname,StringUtil.RTrim( AV7ConnectionKey),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,86);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavConnectionkey_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)-1,(int)edtavConnectionkey_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)85,(short)0,(short)-1,(short)0,(bool)true,(string)"GeneXusSecurityCommon\\GAMGUID",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridwwsysconnsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 87,'',false,'" + sGXsfl_85_idx + "',85)\"";
            ROClassString = "Attribute";
            GridwwsysconnsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavIscurrentkey_Internalname,(string)AV24IsCurrentKey,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,87);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavIscurrentkey_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)-1,(int)edtavIscurrentkey_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)85,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridwwsysconnsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 88,'',false,'" + sGXsfl_85_idx + "',85)\"";
            ROClassString = "Attribute";
            GridwwsysconnsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavConnectionfile_Internalname,StringUtil.RTrim( AV32ConnectionFile),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,88);\"","'"+""+"'"+",false,"+"'"+"EVCONNECTIONFILE.CLICK."+sGXsfl_85_idx+"'",(string)"",(string)"",context.GetMessage( "WWP_GAM_File", ""),(string)"",(string)edtavConnectionfile_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(short)-1,(int)edtavConnectionfile_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)85,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridwwsysconnsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 89,'',false,'" + sGXsfl_85_idx + "',85)\"";
            ROClassString = "Attribute";
            GridwwsysconnsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteconnection_Internalname,StringUtil.RTrim( AV33DeleteConnection),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,89);\"","'"+""+"'"+",false,"+"'"+"EVDELETECONNECTION.CLICK."+sGXsfl_85_idx+"'",(string)"",(string)"",context.GetMessage( "WWP_GAM_Delete", ""),(string)"",(string)edtavDeleteconnection_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(short)-1,(int)edtavDeleteconnection_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)85,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes1V2( ) ;
            GridwwsysconnsContainer.AddRow(GridwwsysconnsRow);
            nGXsfl_85_idx = ((subGridwwsysconns_Islastpage==1)&&(nGXsfl_85_idx+1>subGridwwsysconns_fnc_Recordsperpage( )) ? 1 : nGXsfl_85_idx+1);
            sGXsfl_85_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_85_idx), 4, 0), 4, "0");
            SubsflControlProps_852( ) ;
         }
         /* End function sendrow_852 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl85( )
      {
         if ( GridwwsysconnsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridwwsysconnsContainer"+"DivS\" data-gxgridid=\"85\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridwwsysconns_Internalname, subGridwwsysconns_Internalname, "", "GridNoBorder WorkWith", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridwwsysconns_Backcolorstyle == 0 )
            {
               subGridwwsysconns_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridwwsysconns_Class) > 0 )
               {
                  subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"Title";
               }
            }
            else
            {
               subGridwwsysconns_Titlebackstyle = 1;
               if ( subGridwwsysconns_Backcolorstyle == 1 )
               {
                  subGridwwsysconns_Titlebackcolor = subGridwwsysconns_Allbackcolor;
                  if ( StringUtil.Len( subGridwwsysconns_Class) > 0 )
                  {
                     subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridwwsysconns_Class) > 0 )
                  {
                     subGridwwsysconns_Linesclass = subGridwwsysconns_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_ConnectionKey", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridwwsysconnsContainer.AddObjectProperty("GridName", "Gridwwsysconns");
         }
         else
         {
            GridwwsysconnsContainer.AddObjectProperty("GridName", "Gridwwsysconns");
            GridwwsysconnsContainer.AddObjectProperty("Header", subGridwwsysconns_Header);
            GridwwsysconnsContainer.AddObjectProperty("Class", "GridNoBorder WorkWith");
            GridwwsysconnsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Backcolorstyle), 1, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("CmpContext", "");
            GridwwsysconnsContainer.AddObjectProperty("InMasterPage", "false");
            GridwwsysconnsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwwsysconnsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV7ConnectionKey)));
            GridwwsysconnsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavConnectionkey_Enabled), 5, 0, ".", "")));
            GridwwsysconnsContainer.AddColumnProperties(GridwwsysconnsColumn);
            GridwwsysconnsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwwsysconnsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV24IsCurrentKey));
            GridwwsysconnsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavIscurrentkey_Enabled), 5, 0, ".", "")));
            GridwwsysconnsContainer.AddColumnProperties(GridwwsysconnsColumn);
            GridwwsysconnsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwwsysconnsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV32ConnectionFile)));
            GridwwsysconnsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavConnectionfile_Enabled), 5, 0, ".", "")));
            GridwwsysconnsContainer.AddColumnProperties(GridwwsysconnsColumn);
            GridwwsysconnsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwwsysconnsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV33DeleteConnection)));
            GridwwsysconnsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeleteconnection_Enabled), 5, 0, ".", "")));
            GridwwsysconnsContainer.AddColumnProperties(GridwwsysconnsColumn);
            GridwwsysconnsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Selectedindex), 4, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Allowselection), 1, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Selectioncolor), 9, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Allowhovering), 1, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Hoveringcolor), 9, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Allowcollapsing), 1, 0, ".", "")));
            GridwwsysconnsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwwsysconns_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavConnectionname_Internalname = "vCONNECTIONNAME";
         edtavUsername_Internalname = "vUSERNAME";
         edtavUserpassword_Internalname = "vUSERPASSWORD";
         divUserpassword_cell_Internalname = "USERPASSWORD_CELL";
         lblTextblockencryptionkey_Internalname = "TEXTBLOCKENCRYPTIONKEY";
         divTextblockencryptionkey_cell_Internalname = "TEXTBLOCKENCRYPTIONKEY_CELL";
         edtavEncryptionkey_Internalname = "vENCRYPTIONKEY";
         cellEncryptionkey_cell_Internalname = "ENCRYPTIONKEY_CELL";
         bttBtngenkey_Internalname = "BTNGENKEY";
         tblTablemergedencryptionkey_Internalname = "TABLEMERGEDENCRYPTIONKEY";
         divTablesplittedencryptionkey_Internalname = "TABLESPLITTEDENCRYPTIONKEY";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         Dvpanel_unnamedtable3_Internalname = "DVPANEL_UNNAMEDTABLE3";
         divTblmaindata_Internalname = "TBLMAINDATA";
         lblTextblocknewconnectionkey_Internalname = "TEXTBLOCKNEWCONNECTIONKEY";
         edtavNewconnectionkey_Internalname = "vNEWCONNECTIONKEY";
         divUnnamedtablenewconnectionkey_Internalname = "UNNAMEDTABLENEWCONNECTIONKEY";
         bttBtnsavekey_Internalname = "BTNSAVEKEY";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         bttBtnuseautomatickey_Internalname = "BTNUSEAUTOMATICKEY";
         bttBtnusecurrentkey_Internalname = "BTNUSECURRENTKEY";
         divTbladdconnkey_Internalname = "TBLADDCONNKEY";
         Dvpanel_tbladdconnkey_Internalname = "DVPANEL_TBLADDCONNKEY";
         edtavConnectionkey_Internalname = "vCONNECTIONKEY";
         edtavIscurrentkey_Internalname = "vISCURRENTKEY";
         edtavConnectionfile_Internalname = "vCONNECTIONFILE";
         edtavDeleteconnection_Internalname = "vDELETECONNECTION";
         edtavConnectionfilexml_Internalname = "vCONNECTIONFILEXML";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         Dvpanel_unnamedtable1_Internalname = "DVPANEL_UNNAMEDTABLE1";
         divTblconnkeys_Internalname = "TBLCONNKEYS";
         divTablecontent_Internalname = "TABLECONTENT";
         bttBtnenter_Internalname = "BTNENTER";
         bttBtnuacancel_Internalname = "BTNUACANCEL";
         divTablemain_Internalname = "TABLEMAIN";
         edtavFileconnectionkey_Internalname = "vFILECONNECTIONKEY";
         edtavPconnectionname_Internalname = "vPCONNECTIONNAME";
         Gridwwsysconns_empowerer_Internalname = "GRIDWWSYSCONNS_EMPOWERER";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subGridwwsysconns_Internalname = "GRIDWWSYSCONNS";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridwwsysconns_Allowcollapsing = 0;
         subGridwwsysconns_Allowselection = 0;
         subGridwwsysconns_Header = "";
         edtavDeleteconnection_Jsonclick = "";
         edtavDeleteconnection_Enabled = 1;
         edtavConnectionfile_Jsonclick = "";
         edtavConnectionfile_Enabled = 1;
         edtavIscurrentkey_Jsonclick = "";
         edtavIscurrentkey_Enabled = 1;
         edtavConnectionkey_Jsonclick = "";
         edtavConnectionkey_Enabled = 1;
         subGridwwsysconns_Class = "GridNoBorder WorkWith";
         subGridwwsysconns_Backcolorstyle = 0;
         bttBtngenkey_Visible = 1;
         edtavEncryptionkey_Jsonclick = "";
         edtavEncryptionkey_Enabled = 1;
         cellEncryptionkey_cell_Class = "";
         edtavEncryptionkey_Visible = 1;
         edtavPconnectionname_Jsonclick = "";
         edtavPconnectionname_Visible = 1;
         edtavFileconnectionkey_Jsonclick = "";
         edtavFileconnectionkey_Visible = 1;
         bttBtnenter_Caption = context.GetMessage( "GX_BtnEnter", "");
         bttBtnenter_Visible = 1;
         edtavConnectionfilexml_Enabled = 1;
         edtavConnectionfilexml_Visible = 1;
         bttBtnusecurrentkey_Visible = 1;
         edtavNewconnectionkey_Jsonclick = "";
         edtavNewconnectionkey_Enabled = 1;
         divTblconnkeys_Visible = 1;
         divTextblockencryptionkey_cell_Class = "col-xs-12";
         edtavUserpassword_Jsonclick = "";
         edtavUserpassword_Enabled = 1;
         edtavUserpassword_Visible = 1;
         divUserpassword_cell_Class = "col-xs-12";
         edtavUsername_Jsonclick = "";
         edtavUsername_Enabled = 1;
         edtavConnectionname_Jsonclick = "";
         edtavConnectionname_Enabled = 1;
         divTblmaindata_Visible = 1;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         Dvpanel_unnamedtable1_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Iconposition = "Right";
         Dvpanel_unnamedtable1_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Title = "";
         Dvpanel_unnamedtable1_Cls = "PanelNoHeader";
         Dvpanel_unnamedtable1_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Width = "100%";
         Dvpanel_tbladdconnkey_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_tbladdconnkey_Iconposition = "Right";
         Dvpanel_tbladdconnkey_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_tbladdconnkey_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_tbladdconnkey_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_tbladdconnkey_Title = context.GetMessage( "WWP_GAM_ConnectionKey", "");
         Dvpanel_tbladdconnkey_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_tbladdconnkey_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_tbladdconnkey_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_tbladdconnkey_Width = "100%";
         Dvpanel_unnamedtable3_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable3_Iconposition = "Right";
         Dvpanel_unnamedtable3_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable3_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable3_Collapsible = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable3_Title = context.GetMessage( "WWP_GAM_Connection", "");
         Dvpanel_unnamedtable3_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable3_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable3_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable3_Width = "100%";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Connection", "");
         subGridwwsysconns_Rows = 0;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDWWSYSCONNS_nFirstRecordOnPage"},{"av":"GRIDWWSYSCONNS_nEOF"},{"av":"subGridwwsysconns_Rows","ctrl":"GRIDWWSYSCONNS","prop":"Rows"},{"av":"AV23CurrentConnectionKey","fld":"vCURRENTCONNECTIONKEY","hsh":true},{"av":"AV12FileXML","fld":"vFILEXML","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"ctrl":"BTNENTER","prop":"Visible"},{"ctrl":"BTNGENKEY","prop":"Visible"}]}""");
         setEventMetadata("GRIDWWSYSCONNS.LOAD","""{"handler":"E191V2","iparms":[{"av":"AV23CurrentConnectionKey","fld":"vCURRENTCONNECTIONKEY","hsh":true}]""");
         setEventMetadata("GRIDWWSYSCONNS.LOAD",""","oparms":[{"av":"AV7ConnectionKey","fld":"vCONNECTIONKEY","hsh":true},{"av":"AV24IsCurrentKey","fld":"vISCURRENTKEY"},{"av":"AV32ConnectionFile","fld":"vCONNECTIONFILE"},{"av":"AV33DeleteConnection","fld":"vDELETECONNECTION"},{"ctrl":"BTNUSECURRENTKEY","prop":"Visible"}]}""");
         setEventMetadata("'DOUACANCEL'","""{"handler":"E111V2","iparms":[]}""");
         setEventMetadata("'DOUSEAUTOMATICKEY'","""{"handler":"E121V2","iparms":[]""");
         setEventMetadata("'DOUSEAUTOMATICKEY'",""","oparms":[{"av":"AV18NewConnectionKey","fld":"vNEWCONNECTIONKEY"}]}""");
         setEventMetadata("'DOUSECURRENTKEY'","""{"handler":"E131V2","iparms":[{"av":"AV23CurrentConnectionKey","fld":"vCURRENTCONNECTIONKEY","hsh":true}]""");
         setEventMetadata("'DOUSECURRENTKEY'",""","oparms":[{"av":"AV18NewConnectionKey","fld":"vNEWCONNECTIONKEY"}]}""");
         setEventMetadata("'DOSAVEKEY'","""{"handler":"E141V2","iparms":[{"av":"AV18NewConnectionKey","fld":"vNEWCONNECTIONKEY"},{"av":"AV8ConnectionName","fld":"vCONNECTIONNAME"},{"av":"AV19pConnectionName","fld":"vPCONNECTIONNAME"}]""");
         setEventMetadata("'DOSAVEKEY'",""","oparms":[{"av":"AV19pConnectionName","fld":"vPCONNECTIONNAME"}]}""");
         setEventMetadata("'DOGENKEY'","""{"handler":"E151V2","iparms":[]""");
         setEventMetadata("'DOGENKEY'",""","oparms":[{"av":"AV9EncryptionKey","fld":"vENCRYPTIONKEY"}]}""");
         setEventMetadata("ENTER","""{"handler":"E161V2","iparms":[{"av":"AV8ConnectionName","fld":"vCONNECTIONNAME"},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV20UserName","fld":"vUSERNAME"},{"av":"AV21UserPassword","fld":"vUSERPASSWORD"},{"av":"AV9EncryptionKey","fld":"vENCRYPTIONKEY"},{"av":"AV19pConnectionName","fld":"vPCONNECTIONNAME"}]}""");
         setEventMetadata("'WWCONNKEYS'","""{"handler":"E201V2","iparms":[{"av":"AV19pConnectionName","fld":"vPCONNECTIONNAME"}]""");
         setEventMetadata("'WWCONNKEYS'",""","oparms":[{"av":"AV19pConnectionName","fld":"vPCONNECTIONNAME"}]}""");
         setEventMetadata("VDELETECONNECTION.CLICK","""{"handler":"E211V2","iparms":[{"av":"AV7ConnectionKey","fld":"vCONNECTIONKEY","hsh":true},{"av":"AV8ConnectionName","fld":"vCONNECTIONNAME"},{"av":"AV19pConnectionName","fld":"vPCONNECTIONNAME"}]""");
         setEventMetadata("VDELETECONNECTION.CLICK",""","oparms":[{"av":"AV19pConnectionName","fld":"vPCONNECTIONNAME"}]}""");
         setEventMetadata("VCONNECTIONFILE.CLICK","""{"handler":"E221V2","iparms":[{"av":"AV25FileConnectionKey","fld":"vFILECONNECTIONKEY"},{"av":"AV7ConnectionKey","fld":"vCONNECTIONKEY","hsh":true},{"av":"AV8ConnectionName","fld":"vCONNECTIONNAME"},{"av":"AV12FileXML","fld":"vFILEXML","hsh":true}]""");
         setEventMetadata("VCONNECTIONFILE.CLICK",""","oparms":[{"av":"AV6ConnectionFileXML","fld":"vCONNECTIONFILEXML"},{"av":"AV25FileConnectionKey","fld":"vFILECONNECTIONKEY"},{"av":"edtavConnectionfilexml_Visible","ctrl":"vCONNECTIONFILEXML","prop":"Visible"}]}""");
         setEventMetadata("GRIDWWSYSCONNS_FIRSTPAGE","""{"handler":"subgridwwsysconns_firstpage","iparms":[{"av":"GRIDWWSYSCONNS_nFirstRecordOnPage"},{"av":"GRIDWWSYSCONNS_nEOF"},{"av":"subGridwwsysconns_Rows","ctrl":"GRIDWWSYSCONNS","prop":"Rows"},{"av":"AV23CurrentConnectionKey","fld":"vCURRENTCONNECTIONKEY","hsh":true},{"av":"AV12FileXML","fld":"vFILEXML","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true}]""");
         setEventMetadata("GRIDWWSYSCONNS_FIRSTPAGE",""","oparms":[{"ctrl":"BTNENTER","prop":"Visible"},{"ctrl":"BTNGENKEY","prop":"Visible"}]}""");
         setEventMetadata("GRIDWWSYSCONNS_PREVPAGE","""{"handler":"subgridwwsysconns_previouspage","iparms":[{"av":"GRIDWWSYSCONNS_nFirstRecordOnPage"},{"av":"GRIDWWSYSCONNS_nEOF"},{"av":"subGridwwsysconns_Rows","ctrl":"GRIDWWSYSCONNS","prop":"Rows"},{"av":"AV23CurrentConnectionKey","fld":"vCURRENTCONNECTIONKEY","hsh":true},{"av":"AV12FileXML","fld":"vFILEXML","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true}]""");
         setEventMetadata("GRIDWWSYSCONNS_PREVPAGE",""","oparms":[{"ctrl":"BTNENTER","prop":"Visible"},{"ctrl":"BTNGENKEY","prop":"Visible"}]}""");
         setEventMetadata("GRIDWWSYSCONNS_NEXTPAGE","""{"handler":"subgridwwsysconns_nextpage","iparms":[{"av":"GRIDWWSYSCONNS_nFirstRecordOnPage"},{"av":"GRIDWWSYSCONNS_nEOF"},{"av":"subGridwwsysconns_Rows","ctrl":"GRIDWWSYSCONNS","prop":"Rows"},{"av":"AV23CurrentConnectionKey","fld":"vCURRENTCONNECTIONKEY","hsh":true},{"av":"AV12FileXML","fld":"vFILEXML","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true}]""");
         setEventMetadata("GRIDWWSYSCONNS_NEXTPAGE",""","oparms":[{"ctrl":"BTNENTER","prop":"Visible"},{"ctrl":"BTNGENKEY","prop":"Visible"}]}""");
         setEventMetadata("GRIDWWSYSCONNS_LASTPAGE","""{"handler":"subgridwwsysconns_lastpage","iparms":[{"av":"GRIDWWSYSCONNS_nFirstRecordOnPage"},{"av":"GRIDWWSYSCONNS_nEOF"},{"av":"subGridwwsysconns_Rows","ctrl":"GRIDWWSYSCONNS","prop":"Rows"},{"av":"AV23CurrentConnectionKey","fld":"vCURRENTCONNECTIONKEY","hsh":true},{"av":"AV12FileXML","fld":"vFILEXML","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"subGridwwsysconns_Recordcount"}]""");
         setEventMetadata("GRIDWWSYSCONNS_LASTPAGE",""","oparms":[{"ctrl":"BTNENTER","prop":"Visible"},{"ctrl":"BTNGENKEY","prop":"Visible"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Deleteconnection","iparms":[]}""");
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
         wcpOAV19pConnectionName = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV23CurrentConnectionKey = "";
         AV12FileXML = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         Gridwwsysconns_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         StyleString = "";
         ucDvpanel_unnamedtable3 = new GXUserControl();
         TempTags = "";
         AV8ConnectionName = "";
         AV20UserName = "";
         AV21UserPassword = "";
         lblTextblockencryptionkey_Jsonclick = "";
         ucDvpanel_tbladdconnkey = new GXUserControl();
         lblTextblocknewconnectionkey_Jsonclick = "";
         AV18NewConnectionKey = "";
         bttBtnsavekey_Jsonclick = "";
         bttBtnuseautomatickey_Jsonclick = "";
         bttBtnusecurrentkey_Jsonclick = "";
         ucDvpanel_unnamedtable1 = new GXUserControl();
         GridwwsysconnsContainer = new GXWebGrid( context);
         sStyleString = "";
         AV6ConnectionFileXML = "";
         bttBtnenter_Jsonclick = "";
         bttBtnuacancel_Jsonclick = "";
         AV25FileConnectionKey = "";
         ucGridwwsysconns_empowerer = new GXUserControl();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7ConnectionKey = "";
         AV24IsCurrentKey = "";
         AV32ConnectionFile = "";
         AV33DeleteConnection = "";
         GXDecQS = "";
         AV9EncryptionKey = "";
         AV13GAMRepositoryConnection = new GeneXus.Programs.genexussecurity.SdtGAMRepositoryConnection(context);
         AV37GXV1 = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMSystemConnection>( context, "GeneXus.Programs.genexussecurity.SdtGAMSystemConnection", "DesignSystem.Programs");
         AV11Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV14GAMSystemConnection = new GeneXus.Programs.genexussecurity.SdtGAMSystemConnection(context);
         GridwwsysconnsRow = new GXWebRow();
         AV15GXGUID = Guid.Empty;
         AV10Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         bttBtngenkey_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridwwsysconns_Linesclass = "";
         ROClassString = "";
         GridwwsysconnsColumn = new GXWebColumn();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamconnectionentry__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamconnectionentry__default(),
            new Object[][] {
            }
         );
         /* GeneXus formulas. */
         edtavConnectionkey_Enabled = 0;
         edtavIscurrentkey_Enabled = 0;
         edtavConnectionfile_Enabled = 0;
         edtavDeleteconnection_Enabled = 0;
      }

      private short GRIDWWSYSCONNS_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short subGridwwsysconns_Backcolorstyle ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private short subGridwwsysconns_Backstyle ;
      private short subGridwwsysconns_Titlebackstyle ;
      private short subGridwwsysconns_Allowselection ;
      private short subGridwwsysconns_Allowhovering ;
      private short subGridwwsysconns_Allowcollapsing ;
      private short subGridwwsysconns_Collapsed ;
      private int nRC_GXsfl_85 ;
      private int subGridwwsysconns_Recordcount ;
      private int subGridwwsysconns_Rows ;
      private int nGXsfl_85_idx=1 ;
      private int divTblmaindata_Visible ;
      private int edtavConnectionname_Enabled ;
      private int edtavUsername_Enabled ;
      private int edtavUserpassword_Visible ;
      private int edtavUserpassword_Enabled ;
      private int divTblconnkeys_Visible ;
      private int edtavNewconnectionkey_Enabled ;
      private int bttBtnusecurrentkey_Visible ;
      private int edtavConnectionfilexml_Visible ;
      private int edtavConnectionfilexml_Enabled ;
      private int bttBtnenter_Visible ;
      private int edtavFileconnectionkey_Visible ;
      private int edtavPconnectionname_Visible ;
      private int subGridwwsysconns_Islastpage ;
      private int edtavConnectionkey_Enabled ;
      private int edtavIscurrentkey_Enabled ;
      private int edtavConnectionfile_Enabled ;
      private int edtavDeleteconnection_Enabled ;
      private int GRIDWWSYSCONNS_nGridOutOfScope ;
      private int AV38GXV2 ;
      private int bttBtngenkey_Visible ;
      private int edtavEncryptionkey_Visible ;
      private int AV39GXV3 ;
      private int edtavEncryptionkey_Enabled ;
      private int idxLst ;
      private int subGridwwsysconns_Backcolor ;
      private int subGridwwsysconns_Allbackcolor ;
      private int subGridwwsysconns_Titlebackcolor ;
      private int subGridwwsysconns_Selectedindex ;
      private int subGridwwsysconns_Selectioncolor ;
      private int subGridwwsysconns_Hoveringcolor ;
      private long GRIDWWSYSCONNS_nFirstRecordOnPage ;
      private long GRIDWWSYSCONNS_nCurrentRecord ;
      private string Gx_mode ;
      private string AV19pConnectionName ;
      private string wcpOGx_mode ;
      private string wcpOAV19pConnectionName ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_85_idx="0001" ;
      private string AV23CurrentConnectionKey ;
      private string AV12FileXML ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string Dvpanel_unnamedtable3_Width ;
      private string Dvpanel_unnamedtable3_Cls ;
      private string Dvpanel_unnamedtable3_Title ;
      private string Dvpanel_unnamedtable3_Iconposition ;
      private string Dvpanel_tbladdconnkey_Width ;
      private string Dvpanel_tbladdconnkey_Cls ;
      private string Dvpanel_tbladdconnkey_Title ;
      private string Dvpanel_tbladdconnkey_Iconposition ;
      private string Dvpanel_unnamedtable1_Width ;
      private string Dvpanel_unnamedtable1_Cls ;
      private string Dvpanel_unnamedtable1_Title ;
      private string Dvpanel_unnamedtable1_Iconposition ;
      private string Gridwwsysconns_empowerer_Gridinternalname ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string divTablecontent_Internalname ;
      private string divTblmaindata_Internalname ;
      private string Dvpanel_unnamedtable3_Internalname ;
      private string divUnnamedtable3_Internalname ;
      private string edtavConnectionname_Internalname ;
      private string TempTags ;
      private string AV8ConnectionName ;
      private string edtavConnectionname_Jsonclick ;
      private string edtavUsername_Internalname ;
      private string AV20UserName ;
      private string edtavUsername_Jsonclick ;
      private string divUserpassword_cell_Internalname ;
      private string divUserpassword_cell_Class ;
      private string edtavUserpassword_Internalname ;
      private string AV21UserPassword ;
      private string edtavUserpassword_Jsonclick ;
      private string divTablesplittedencryptionkey_Internalname ;
      private string divTextblockencryptionkey_cell_Internalname ;
      private string divTextblockencryptionkey_cell_Class ;
      private string lblTextblockencryptionkey_Internalname ;
      private string lblTextblockencryptionkey_Jsonclick ;
      private string divTblconnkeys_Internalname ;
      private string Dvpanel_tbladdconnkey_Internalname ;
      private string divTbladdconnkey_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string divUnnamedtablenewconnectionkey_Internalname ;
      private string lblTextblocknewconnectionkey_Internalname ;
      private string lblTextblocknewconnectionkey_Jsonclick ;
      private string edtavNewconnectionkey_Internalname ;
      private string AV18NewConnectionKey ;
      private string edtavNewconnectionkey_Jsonclick ;
      private string bttBtnsavekey_Internalname ;
      private string bttBtnsavekey_Jsonclick ;
      private string bttBtnuseautomatickey_Internalname ;
      private string bttBtnuseautomatickey_Jsonclick ;
      private string bttBtnusecurrentkey_Internalname ;
      private string bttBtnusecurrentkey_Jsonclick ;
      private string Dvpanel_unnamedtable1_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string sStyleString ;
      private string subGridwwsysconns_Internalname ;
      private string edtavConnectionfilexml_Internalname ;
      private string AV6ConnectionFileXML ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Caption ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtnuacancel_Internalname ;
      private string bttBtnuacancel_Jsonclick ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string edtavFileconnectionkey_Internalname ;
      private string AV25FileConnectionKey ;
      private string edtavFileconnectionkey_Jsonclick ;
      private string edtavPconnectionname_Internalname ;
      private string edtavPconnectionname_Jsonclick ;
      private string Gridwwsysconns_empowerer_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV7ConnectionKey ;
      private string edtavConnectionkey_Internalname ;
      private string edtavIscurrentkey_Internalname ;
      private string AV32ConnectionFile ;
      private string edtavConnectionfile_Internalname ;
      private string AV33DeleteConnection ;
      private string edtavDeleteconnection_Internalname ;
      private string GXDecQS ;
      private string AV9EncryptionKey ;
      private string edtavEncryptionkey_Internalname ;
      private string bttBtngenkey_Internalname ;
      private string cellEncryptionkey_cell_Class ;
      private string cellEncryptionkey_cell_Internalname ;
      private string tblTablemergedencryptionkey_Internalname ;
      private string edtavEncryptionkey_Jsonclick ;
      private string bttBtngenkey_Jsonclick ;
      private string sGXsfl_85_fel_idx="0001" ;
      private string subGridwwsysconns_Class ;
      private string subGridwwsysconns_Linesclass ;
      private string ROClassString ;
      private string edtavConnectionkey_Jsonclick ;
      private string edtavIscurrentkey_Jsonclick ;
      private string edtavConnectionfile_Jsonclick ;
      private string edtavDeleteconnection_Jsonclick ;
      private string subGridwwsysconns_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Dvpanel_unnamedtable3_Autowidth ;
      private bool Dvpanel_unnamedtable3_Autoheight ;
      private bool Dvpanel_unnamedtable3_Collapsible ;
      private bool Dvpanel_unnamedtable3_Collapsed ;
      private bool Dvpanel_unnamedtable3_Showcollapseicon ;
      private bool Dvpanel_unnamedtable3_Autoscroll ;
      private bool Dvpanel_tbladdconnkey_Autowidth ;
      private bool Dvpanel_tbladdconnkey_Autoheight ;
      private bool Dvpanel_tbladdconnkey_Collapsible ;
      private bool Dvpanel_tbladdconnkey_Collapsed ;
      private bool Dvpanel_tbladdconnkey_Showcollapseicon ;
      private bool Dvpanel_tbladdconnkey_Autoscroll ;
      private bool Dvpanel_unnamedtable1_Autowidth ;
      private bool Dvpanel_unnamedtable1_Autoheight ;
      private bool Dvpanel_unnamedtable1_Collapsible ;
      private bool Dvpanel_unnamedtable1_Collapsed ;
      private bool Dvpanel_unnamedtable1_Showcollapseicon ;
      private bool Dvpanel_unnamedtable1_Autoscroll ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_85_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private bool AV16isOk ;
      private bool AV27hasCurrentKey ;
      private string AV24IsCurrentKey ;
      private Guid AV15GXGUID ;
      private GXWebGrid GridwwsysconnsContainer ;
      private GXWebRow GridwwsysconnsRow ;
      private GXWebColumn GridwwsysconnsColumn ;
      private GXUserControl ucDvpanel_unnamedtable3 ;
      private GXUserControl ucDvpanel_tbladdconnkey ;
      private GXUserControl ucDvpanel_unnamedtable1 ;
      private GXUserControl ucGridwwsysconns_empowerer ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_Gx_mode ;
      private string aP1_pConnectionName ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepositoryConnection AV13GAMRepositoryConnection ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMSystemConnection> AV37GXV1 ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV11Errors ;
      private GeneXus.Programs.genexussecurity.SdtGAMSystemConnection AV14GAMSystemConnection ;
      private IDataStoreProvider pr_default ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV10Error ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamconnectionentry__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamconnectionentry__default : DataStoreHelperBase, IDataStoreHelper
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
