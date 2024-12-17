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
namespace DesignSystem.Programs.wwpbaseobjects {
   public class wwp_search : GXDataArea
   {
      public wwp_search( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_search( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_DeafultSearchText ,
                           bool aP1_DefaultIsAdvancedSearch ,
                           string aP2_DefaultAdvFilterEntitiesJson )
      {
         this.AV10DeafultSearchText = aP0_DeafultSearchText;
         this.AV12DefaultIsAdvancedSearch = aP1_DefaultIsAdvancedSearch;
         this.AV11DefaultAdvFilterEntitiesJson = aP2_DefaultAdvFilterEntitiesJson;
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
            gxfirstwebparm = GetFirstPar( "DeafultSearchText");
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
               gxfirstwebparm = GetFirstPar( "DeafultSearchText");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "DeafultSearchText");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Fsresults") == 0 )
            {
               gxnrFsresults_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Fsresults") == 0 )
            {
               gxgrFsresults_refresh_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Fsresultcategories") == 0 )
            {
               gxnrFsresultcategories_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Fsresultcategories") == 0 )
            {
               gxgrFsresultcategories_refresh_invoke( ) ;
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

      protected void gxnrFsresults_newrow_invoke( )
      {
         nRC_GXsfl_61 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_61"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_61_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_61_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_61_idx = GetPar( "sGXsfl_61_idx");
         edtavUrl_Visible = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), !bGXsfl_61_Refreshing);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrFsresults_newrow( ) ;
         /* End function gxnrFsresults_newrow_invoke */
      }

      protected void gxgrFsresults_refresh_invoke( )
      {
         edtavUrl_Visible = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), !bGXsfl_61_Refreshing);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV28WWP_SearchResults);
         AV10DeafultSearchText = GetPar( "DeafultSearchText");
         AV12DefaultIsAdvancedSearch = StringUtil.StrToBool( GetPar( "DefaultIsAdvancedSearch"));
         AV11DefaultAdvFilterEntitiesJson = GetPar( "DefaultAdvFilterEntitiesJson");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrFsresults_refresh( AV28WWP_SearchResults, AV10DeafultSearchText, AV12DefaultIsAdvancedSearch, AV11DefaultAdvFilterEntitiesJson) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrFsresults_refresh_invoke */
      }

      protected void gxnrFsresultcategories_newrow_invoke( )
      {
         nRC_GXsfl_53 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_53"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_53_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_53_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_53_idx = GetPar( "sGXsfl_53_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrFsresultcategories_newrow( ) ;
         /* End function gxnrFsresultcategories_newrow_invoke */
      }

      protected void gxgrFsresultcategories_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV28WWP_SearchResults);
         AV10DeafultSearchText = GetPar( "DeafultSearchText");
         AV12DefaultIsAdvancedSearch = StringUtil.StrToBool( GetPar( "DefaultIsAdvancedSearch"));
         AV11DefaultAdvFilterEntitiesJson = GetPar( "DefaultAdvFilterEntitiesJson");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrFsresultcategories_refresh( AV28WWP_SearchResults, AV10DeafultSearchText, AV12DefaultIsAdvancedSearch, AV11DefaultAdvFilterEntitiesJson) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrFsresultcategories_refresh_invoke */
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
            return "wwp_search_Execute" ;
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
         PA102( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START102( ) ;
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
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
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
         GXEncryptionTmp = "wwpbaseobjects.wwp_search.aspx"+UrlEncode(StringUtil.RTrim(AV10DeafultSearchText)) + "," + UrlEncode(StringUtil.BoolToStr(AV12DefaultIsAdvancedSearch)) + "," + UrlEncode(StringUtil.RTrim(AV11DefaultAdvFilterEntitiesJson));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wwpbaseobjects.wwp_search.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vDEAFULTSEARCHTEXT", AV10DeafultSearchText);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEAFULTSEARCHTEXT", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV10DeafultSearchText, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vDEFAULTISADVANCEDSEARCH", AV12DefaultIsAdvancedSearch);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTISADVANCEDSEARCH", GetSecureSignedToken( "", AV12DefaultIsAdvancedSearch, context));
         GxWebStd.gx_hidden_field( context, "vDEFAULTADVFILTERENTITIESJSON", AV11DefaultAdvFilterEntitiesJson);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTADVFILTERENTITIESJSON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV11DefaultAdvFilterEntitiesJson, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Wwp_searchresults", AV28WWP_SearchResults);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Wwp_searchresults", AV28WWP_SearchResults);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_53", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_53), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV9DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV9DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vADVFILTERENTITIES_DATA", AV6AdvFilterEntities_Data);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vADVFILTERENTITIES_DATA", AV6AdvFilterEntities_Data);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWWP_SEARCHRESULTS", AV28WWP_SearchResults);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWWP_SEARCHRESULTS", AV28WWP_SearchResults);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vISADVANCEDSEARCH", AV23IsAdvancedSearch);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vADVFILTERENTITIES", AV5AdvFilterEntities);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vADVFILTERENTITIES", AV5AdvFilterEntities);
         }
         GxWebStd.gx_hidden_field( context, "vDEAFULTSEARCHTEXT", AV10DeafultSearchText);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEAFULTSEARCHTEXT", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV10DeafultSearchText, "")), context));
         GxWebStd.gx_boolean_hidden_field( context, "vDEFAULTISADVANCEDSEARCH", AV12DefaultIsAdvancedSearch);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTISADVANCEDSEARCH", GetSecureSignedToken( "", AV12DefaultIsAdvancedSearch, context));
         GxWebStd.gx_hidden_field( context, "vDEFAULTADVFILTERENTITIESJSON", AV11DefaultAdvFilterEntitiesJson);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTADVFILTERENTITIESJSON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV11DefaultAdvFilterEntitiesJson, "")), context));
         GxWebStd.gx_hidden_field( context, "COMBO_ADVFILTERENTITIES_Selectedvalue_get", StringUtil.RTrim( Combo_advfilterentities_Selectedvalue_get));
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
            WE102( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT102( ) ;
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
         GXEncryptionTmp = "wwpbaseobjects.wwp_search.aspx"+UrlEncode(StringUtil.RTrim(AV10DeafultSearchText)) + "," + UrlEncode(StringUtil.BoolToStr(AV12DefaultIsAdvancedSearch)) + "," + UrlEncode(StringUtil.RTrim(AV11DefaultAdvFilterEntitiesJson));
         return formatLink("wwpbaseobjects.wwp_search.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "WWPBaseObjects.WWP_Search" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Search results", "") ;
      }

      protected void WB100( )
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMain", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingBottom80", "start", "top", "", "", "div");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 WWFiltersCell WWFiltersCell SearchFiltersCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheader_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-direction:column;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesimplesearchcell_Internalname, divTablesimplesearchcell_Visible, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable8_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-9", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable10_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-9 col-md-8 col-lg-6 DataContentCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavSearchtext_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSearchtext_Internalname, context.GetMessage( "WWP_Search_SearchDescription", ""), "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'',false,'" + sGXsfl_53_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSearchtext_Internalname, AV25SearchText, StringUtil.RTrim( context.localUtil.Format( AV25SearchText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,24);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavSearchtext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavSearchtext_Enabled, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_WWPBaseObjects/WWP_Search.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(53), 2, 0)+","+"null"+");", context.GetMessage( "GX_BtnSearch", ""), bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_WWPBaseObjects/WWP_Search.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3", "end", "top", "", "", "div");
            /* User Defined Control */
            ucBtnadvancedsearch.SetProperty("TooltipText", Btnadvancedsearch_Tooltiptext);
            ucBtnadvancedsearch.SetProperty("BeforeIconClass", Btnadvancedsearch_Beforeiconclass);
            ucBtnadvancedsearch.SetProperty("Caption", Btnadvancedsearch_Caption);
            ucBtnadvancedsearch.SetProperty("Class", Btnadvancedsearch_Class);
            ucBtnadvancedsearch.Render(context, "wwp_iconbutton", Btnadvancedsearch_Internalname, "BTNADVANCEDSEARCHContainer");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableadvancedsearchcell_Internalname, divTableadvancedsearchcell_Visible, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableadvancedsearch_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-9", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable9_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-9 col-md-8 col-lg-6 DataContentCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAdvfiltertext_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAdvfiltertext_Internalname, context.GetMessage( "WWP_Search_SearchDescription", ""), "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 38,'',false,'" + sGXsfl_53_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAdvfiltertext_Internalname, AV8AdvFilterText, StringUtil.RTrim( context.localUtil.Format( AV8AdvFilterText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,38);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAdvfiltertext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAdvfiltertext_Enabled, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_WWPBaseObjects/WWP_Search.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-9 col-md-8 col-lg-6 DataContentCell ExtendedComboCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedadvfilterentities_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockcombo_advfilterentities_Internalname, context.GetMessage( "WWP_Search_SearchInDescription", ""), "", "", lblTextblockcombo_advfilterentities_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WWP_Search.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-9", "start", "top", "", "", "div");
            /* User Defined Control */
            ucCombo_advfilterentities.SetProperty("Caption", Combo_advfilterentities_Caption);
            ucCombo_advfilterentities.SetProperty("Cls", Combo_advfilterentities_Cls);
            ucCombo_advfilterentities.SetProperty("AllowMultipleSelection", Combo_advfilterentities_Allowmultipleselection);
            ucCombo_advfilterentities.SetProperty("IncludeOnlySelectedOption", Combo_advfilterentities_Includeonlyselectedoption);
            ucCombo_advfilterentities.SetProperty("EmptyItem", Combo_advfilterentities_Emptyitem);
            ucCombo_advfilterentities.SetProperty("MultipleValuesType", Combo_advfilterentities_Multiplevaluestype);
            ucCombo_advfilterentities.SetProperty("DropDownOptionsTitleSettingsIcons", AV9DDO_TitleSettingsIcons);
            ucCombo_advfilterentities.SetProperty("DropDownOptionsData", AV6AdvFilterEntities_Data);
            ucCombo_advfilterentities.Render(context, "dvelop.gxbootstrap.ddoextendedcombo", Combo_advfilterentities_Internalname, "COMBO_ADVFILTERENTITIESContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 48,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnbtnadvsearch_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(53), 2, 0)+","+"null"+");", context.GetMessage( "GX_BtnSearch", ""), bttBtnbtnadvsearch_Jsonclick, 5, context.GetMessage( "GX_BtnSearch", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOBTNADVSEARCH\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_WWPBaseObjects/WWP_Search.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3", "end", "top", "", "", "div");
            /* User Defined Control */
            ucBtnbasicsearch.SetProperty("TooltipText", Btnbasicsearch_Tooltiptext);
            ucBtnbasicsearch.SetProperty("BeforeIconClass", Btnbasicsearch_Beforeiconclass);
            ucBtnbasicsearch.SetProperty("Caption", Btnbasicsearch_Caption);
            ucBtnbasicsearch.SetProperty("Class", Btnbasicsearch_Class);
            ucBtnbasicsearch.Render(context, "wwp_iconbutton", Btnbasicsearch_Internalname, "BTNBASICSEARCHContainer");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divFsresultcategoriescell_Internalname, divFsresultcategoriescell_Visible, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            FsresultcategoriesContainer.SetIsFreestyle(true);
            FsresultcategoriesContainer.SetWrapped(nGXWrapped);
            StartGridControl53( ) ;
         }
         if ( wbEnd == 53 )
         {
            wbEnd = 0;
            nRC_GXsfl_53 = (int)(nGXsfl_53_idx-1);
            if ( FsresultcategoriesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV36GXV1 = nGXsfl_53_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"FsresultcategoriesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Fsresultcategories", FsresultcategoriesContainer, subFsresultcategories_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "FsresultcategoriesContainerData", FsresultcategoriesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "FsresultcategoriesContainerData"+"V", FsresultcategoriesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"FsresultcategoriesContainerData"+"V"+"\" value='"+FsresultcategoriesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTxtnoresultscell_Internalname, divTxtnoresultscell_Visible, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTxtnoresults_Internalname, lblTxtnoresults_Caption, "", "", lblTxtnoresults_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlockShowingOnly", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WWP_Search.htm");
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
         if ( wbEnd == 53 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( FsresultcategoriesContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV36GXV1 = nGXsfl_53_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"FsresultcategoriesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Fsresultcategories", FsresultcategoriesContainer, subFsresultcategories_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "FsresultcategoriesContainerData", FsresultcategoriesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "FsresultcategoriesContainerData"+"V", FsresultcategoriesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"FsresultcategoriesContainerData"+"V"+"\" value='"+FsresultcategoriesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         if ( wbEnd == 61 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( FsresultsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV38GXV3 = nGXsfl_61_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"FsresultsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Fsresults", FsresultsContainer, subFsresults_Internalname);
                  if ( ! isAjaxCallMode( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "FsresultsContainerData", FsresultsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "FsresultsContainerData"+"V", FsresultsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"FsresultsContainerData"+"V"+"\" value='"+FsresultsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START102( )
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
         Form.Meta.addItem("description", context.GetMessage( "Search results", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP100( ) ;
      }

      protected void WS102( )
      {
         START102( ) ;
         EVT102( ) ;
      }

      protected void EVT102( )
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
                           else if ( StringUtil.StrCmp(sEvt, "'DOBTNADVSEARCH'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoBtnAdvSearch' */
                              E11102 ();
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
                                    E12102 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "TABLEFSCARD.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Tablefscard.Click */
                              E13102 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "FSRESULTCATEGORIES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "FSRESULTS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "TABLEFSCARD.CLICK") == 0 ) )
                           {
                              nGXsfl_53_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
                              SubsflControlProps_532( ) ;
                              AV36GXV1 = nGXsfl_53_idx;
                              if ( ( AV28WWP_SearchResults.Count >= AV36GXV1 ) && ( AV36GXV1 > 0 ) )
                              {
                                 AV28WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1));
                              }
                              GXCCtl = "subFsresults_Recordcount_" + sGXsfl_53_idx;
                              subFsresults_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( GXCCtl), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E14102 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "FSRESULTCATEGORIES.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Fsresultcategories.Load */
                                    E15102 ();
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
                                 sEvtType = StringUtil.Right( sEvt, 4);
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                                 if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "FSRESULTS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 17), "TABLEFSCARD.CLICK") == 0 ) )
                                 {
                                    nGXsfl_61_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                                    sGXsfl_61_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_61_idx), 4, 0), 4, "0") + sGXsfl_53_idx;
                                    SubsflControlProps_613( ) ;
                                    AV26Url = cgiGet( edtavUrl_Internalname);
                                    AssignAttri("", false, edtavUrl_Internalname, AV26Url);
                                    AV13DisplayType1_Title = cgiGet( edtavDisplaytype1_title_Internalname);
                                    AssignAttri("", false, edtavDisplaytype1_title_Internalname, AV13DisplayType1_Title);
                                    AV14DisplayType2_Title = cgiGet( edtavDisplaytype2_title_Internalname);
                                    AssignAttri("", false, edtavDisplaytype2_title_Internalname, AV14DisplayType2_Title);
                                    AV16DisplayType3_Title = cgiGet( edtavDisplaytype3_title_Internalname);
                                    AssignAttri("", false, edtavDisplaytype3_title_Internalname, AV16DisplayType3_Title);
                                    AV15DisplayType3_Subtitle = cgiGet( edtavDisplaytype3_subtitle_Internalname);
                                    AssignAttri("", false, edtavDisplaytype3_subtitle_Internalname, AV15DisplayType3_Subtitle);
                                    AV17DisplayType4_Image = cgiGet( edtavDisplaytype4_image_Internalname);
                                    AssignProp("", false, edtavDisplaytype4_image_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV17DisplayType4_Image)) ? AV39Displaytype4_image_GXI : context.convertURL( context.PathToRelativeUrl( AV17DisplayType4_Image))), !bGXsfl_61_Refreshing);
                                    AssignProp("", false, edtavDisplaytype4_image_Internalname, "SrcSet", context.GetImageSrcSet( AV17DisplayType4_Image), true);
                                    AV18DisplayType4_Title = cgiGet( edtavDisplaytype4_title_Internalname);
                                    AssignAttri("", false, edtavDisplaytype4_title_Internalname, AV18DisplayType4_Title);
                                    AV20DisplayType5_Title = cgiGet( edtavDisplaytype5_title_Internalname);
                                    AssignAttri("", false, edtavDisplaytype5_title_Internalname, AV20DisplayType5_Title);
                                    AV19DisplayType5_Subtitle = cgiGet( edtavDisplaytype5_subtitle_Internalname);
                                    AssignAttri("", false, edtavDisplaytype5_subtitle_Internalname, AV19DisplayType5_Subtitle);
                                    sEvtType = StringUtil.Right( sEvt, 1);
                                    if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                                    {
                                       sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                       if ( StringUtil.StrCmp(sEvt, "FSRESULTS.LOAD") == 0 )
                                       {
                                          context.wbHandled = 1;
                                          dynload_actions( ) ;
                                          /* Execute user event: Fsresults.Load */
                                          E16103 ();
                                       }
                                       else if ( StringUtil.StrCmp(sEvt, "TABLEFSCARD.CLICK") == 0 )
                                       {
                                          context.wbHandled = 1;
                                          dynload_actions( ) ;
                                          /* Execute user event: Tablefscard.Click */
                                          E13102 ();
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
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE102( )
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

      protected void PA102( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "wwpbaseobjects.wwp_search.aspx")), "wwpbaseobjects.wwp_search.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "wwpbaseobjects.wwp_search.aspx")))) ;
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
                  gxfirstwebparm = GetFirstPar( "DeafultSearchText");
                  toggleJsOutput = isJsOutputEnabled( );
                  if ( context.isSpaRequest( ) )
                  {
                     disableJsOutput();
                  }
                  if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
                  {
                     AV10DeafultSearchText = gxfirstwebparm;
                     AssignAttri("", false, "AV10DeafultSearchText", AV10DeafultSearchText);
                     GxWebStd.gx_hidden_field( context, "gxhash_vDEAFULTSEARCHTEXT", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV10DeafultSearchText, "")), context));
                     if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") != 0 )
                     {
                        AV12DefaultIsAdvancedSearch = StringUtil.StrToBool( GetPar( "DefaultIsAdvancedSearch"));
                        AssignAttri("", false, "AV12DefaultIsAdvancedSearch", AV12DefaultIsAdvancedSearch);
                        GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTISADVANCEDSEARCH", GetSecureSignedToken( "", AV12DefaultIsAdvancedSearch, context));
                        AV11DefaultAdvFilterEntitiesJson = GetPar( "DefaultAdvFilterEntitiesJson");
                        AssignAttri("", false, "AV11DefaultAdvFilterEntitiesJson", AV11DefaultAdvFilterEntitiesJson);
                        GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTADVFILTERENTITIESJSON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV11DefaultAdvFilterEntitiesJson, "")), context));
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
               GX_FocusControl = edtavSearchtext_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrFsresultcategories_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_532( ) ;
         while ( nGXsfl_53_idx <= nRC_GXsfl_53 )
         {
            sendrow_532( ) ;
            nGXsfl_53_idx = ((subFsresultcategories_Islastpage==1)&&(nGXsfl_53_idx+1>subFsresultcategories_fnc_Recordsperpage( )) ? 1 : nGXsfl_53_idx+1);
            sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
            SubsflControlProps_532( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( FsresultcategoriesContainer)) ;
         /* End function gxnrFsresultcategories_newrow */
      }

      protected void gxnrFsresults_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_613( ) ;
         while ( nGXsfl_61_idx <= nRC_GXsfl_61 )
         {
            sendrow_613( ) ;
            nGXsfl_61_idx = ((subFsresults_Islastpage==1)&&(nGXsfl_61_idx+1>subFsresults_fnc_Recordsperpage( )) ? 1 : nGXsfl_61_idx+1);
            sGXsfl_61_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_61_idx), 4, 0), 4, "0") + sGXsfl_53_idx;
            SubsflControlProps_613( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( FsresultsContainer)) ;
         /* End function gxnrFsresults_newrow */
      }

      protected void gxgrFsresults_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV28WWP_SearchResults ,
                                            string AV10DeafultSearchText ,
                                            bool AV12DefaultIsAdvancedSearch ,
                                            string AV11DefaultAdvFilterEntitiesJson )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         FSRESULTS_nCurrentRecord = 0;
         RF103( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrFsresults_refresh */
      }

      protected void gxgrFsresultcategories_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV28WWP_SearchResults ,
                                                     string AV10DeafultSearchText ,
                                                     bool AV12DefaultIsAdvancedSearch ,
                                                     string AV11DefaultAdvFilterEntitiesJson )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         FSRESULTCATEGORIES_nCurrentRecord = 0;
         RF102( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrFsresultcategories_refresh */
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
         RF102( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavWwp_searchresults__categoryname_Enabled = 0;
         edtavDisplaytype1_title_Enabled = 0;
         edtavDisplaytype2_title_Enabled = 0;
         edtavDisplaytype3_title_Enabled = 0;
         edtavDisplaytype3_subtitle_Enabled = 0;
         edtavDisplaytype4_title_Enabled = 0;
         edtavDisplaytype5_title_Enabled = 0;
         edtavDisplaytype5_subtitle_Enabled = 0;
      }

      protected void RF102( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            FsresultcategoriesContainer.ClearRows();
         }
         wbStart = 53;
         nGXsfl_53_idx = 1;
         sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
         SubsflControlProps_532( ) ;
         bGXsfl_53_Refreshing = true;
         FsresultcategoriesContainer.AddObjectProperty("GridName", "Fsresultcategories");
         FsresultcategoriesContainer.AddObjectProperty("CmpContext", "");
         FsresultcategoriesContainer.AddObjectProperty("InMasterPage", "false");
         FsresultcategoriesContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         FsresultcategoriesContainer.AddObjectProperty("Class", "FreeStyleGrid");
         FsresultcategoriesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         FsresultcategoriesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         FsresultcategoriesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Backcolorstyle), 1, 0, ".", "")));
         FsresultcategoriesContainer.PageSize = subFsresultcategories_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_532( ) ;
            /* Execute user event: Fsresultcategories.Load */
            E15102 ();
            wbEnd = 53;
            WB100( ) ;
         }
         bGXsfl_53_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes102( )
      {
      }

      protected void RF103( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            FsresultsContainer.ClearRows();
         }
         wbStart = 61;
         nGXsfl_61_idx = 1;
         sGXsfl_61_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_61_idx), 4, 0), 4, "0") + sGXsfl_53_idx;
         SubsflControlProps_613( ) ;
         bGXsfl_61_Refreshing = true;
         FsresultsContainer.AddObjectProperty("GridName", "Fsresults");
         FsresultsContainer.AddObjectProperty("CmpContext", "");
         FsresultsContainer.AddObjectProperty("InMasterPage", "false");
         FsresultsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         FsresultsContainer.AddObjectProperty("Class", "FreeStyleGrid");
         FsresultsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         FsresultsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         FsresultsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Backcolorstyle), 1, 0, ".", "")));
         FsresultsContainer.PageSize = subFsresults_fnc_Recordsperpage( );
         GXCCtl = "FSRESULTS_nFirstRecordOnPage_" + sGXsfl_53_idx;
         if ( subFsresults_Islastpage != 0 )
         {
            FSRESULTS_nFirstRecordOnPage = (long)(subFsresults_fnc_Recordcount( )-subFsresults_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(FSRESULTS_nFirstRecordOnPage), 15, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("FSRESULTS_nFirstRecordOnPage", FSRESULTS_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_613( ) ;
            /* Execute user event: Fsresults.Load */
            E16103 ();
            wbEnd = 61;
            WB100( ) ;
         }
         bGXsfl_61_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes103( )
      {
      }

      protected int subFsresultcategories_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subFsresultcategories_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subFsresultcategories_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subFsresultcategories_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected int subFsresults_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subFsresults_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subFsresults_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subFsresults_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavWwp_searchresults__categoryname_Enabled = 0;
         edtavDisplaytype1_title_Enabled = 0;
         edtavDisplaytype2_title_Enabled = 0;
         edtavDisplaytype3_title_Enabled = 0;
         edtavDisplaytype3_subtitle_Enabled = 0;
         edtavDisplaytype4_title_Enabled = 0;
         edtavDisplaytype5_title_Enabled = 0;
         edtavDisplaytype5_subtitle_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP100( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E14102 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Wwp_searchresults"), AV28WWP_SearchResults);
            ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV9DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( "vADVFILTERENTITIES_DATA"), AV6AdvFilterEntities_Data);
            ajax_req_read_hidden_sdt(cgiGet( "vWWP_SEARCHRESULTS"), AV28WWP_SearchResults);
            ajax_req_read_hidden_sdt(cgiGet( "vADVFILTERENTITIES"), AV5AdvFilterEntities);
            /* Read saved values. */
            nRC_GXsfl_53 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_53"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV23IsAdvancedSearch = StringUtil.StrToBool( cgiGet( "vISADVANCEDSEARCH"));
            nRC_GXsfl_53 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_53"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nGXsfl_53_fel_idx = 0;
            while ( nGXsfl_53_fel_idx < nRC_GXsfl_53 )
            {
               nGXsfl_53_fel_idx = ((subFsresultcategories_Islastpage==1)&&(nGXsfl_53_fel_idx+1>subFsresultcategories_fnc_Recordsperpage( )) ? 1 : nGXsfl_53_fel_idx+1);
               sGXsfl_53_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_532( ) ;
               AV36GXV1 = nGXsfl_53_fel_idx;
               if ( ( AV28WWP_SearchResults.Count >= AV36GXV1 ) && ( AV36GXV1 > 0 ) )
               {
                  AV28WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1));
               }
               GXCCtl = "subFsresults_Recordcount_" + sGXsfl_53_fel_idx;
               subFsresults_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( GXCCtl), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            }
            if ( nGXsfl_53_fel_idx == 0 )
            {
               nGXsfl_53_idx = 1;
               sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
               SubsflControlProps_532( ) ;
            }
            nGXsfl_53_fel_idx = 1;
            /* Read variables values. */
            AV25SearchText = cgiGet( edtavSearchtext_Internalname);
            AssignAttri("", false, "AV25SearchText", AV25SearchText);
            AV8AdvFilterText = cgiGet( edtavAdvfiltertext_Internalname);
            AssignAttri("", false, "AV8AdvFilterText", AV8AdvFilterText);
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
         E14102 ();
         if (returnInSub) return;
      }

      protected void E14102( )
      {
         /* Start Routine */
         returnInSub = false;
         AV25SearchText = AV10DeafultSearchText;
         AssignAttri("", false, "AV25SearchText", AV25SearchText);
         AV23IsAdvancedSearch = AV12DefaultIsAdvancedSearch;
         AssignAttri("", false, "AV23IsAdvancedSearch", AV23IsAdvancedSearch);
         AV8AdvFilterText = AV25SearchText;
         AssignAttri("", false, "AV8AdvFilterText", AV8AdvFilterText);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11DefaultAdvFilterEntitiesJson)) )
         {
            AV5AdvFilterEntities.FromJSonString(AV11DefaultAdvFilterEntitiesJson, null);
         }
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = AV9DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1) ;
         AV9DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1;
         /* Execute user subroutine: 'LOADCOMBOADVFILTERENTITIES' */
         S112 ();
         if (returnInSub) return;
         edtavUrl_Visible = 0;
         AssignProp("", false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), !bGXsfl_61_Refreshing);
         /* Execute user subroutine: 'EXECUTE SEARCH' */
         S122 ();
         if (returnInSub) return;
         AV32WebSession.Set("IsLandingPage", "S");
      }

      private void E15102( )
      {
         /* Fsresultcategories_Load Routine */
         returnInSub = false;
         AV36GXV1 = 1;
         while ( AV36GXV1 <= AV28WWP_SearchResults.Count )
         {
            AV28WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1));
            if ( ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Showingallresults )
            {
               lblTxtshowingonly_Caption = "";
            }
            else
            {
               lblTxtshowingonly_Caption = StringUtil.Format( context.GetMessage( "WWP_Search_ShowingTopResults", ""), StringUtil.LTrimStr( (decimal)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.Count), 9, 0), "", "", "", "", "", "", "", "");
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 53;
            }
            sendrow_532( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_53_Refreshing )
            {
               DoAjaxLoad(53, FsresultcategoriesRow);
            }
            AV36GXV1 = (int)(AV36GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E11102( )
      {
         AV36GXV1 = nGXsfl_53_idx;
         if ( ( AV36GXV1 > 0 ) && ( AV28WWP_SearchResults.Count >= AV36GXV1 ) )
         {
            AV28WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1));
         }
         /* 'DoBtnAdvSearch' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'EXECUTE SEARCH' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5AdvFilterEntities", AV5AdvFilterEntities);
         if ( gx_BV53 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV28WWP_SearchResults", AV28WWP_SearchResults);
            nGXsfl_53_bak_idx = nGXsfl_53_idx;
            gxgrFsresultcategories_refresh( AV28WWP_SearchResults, AV10DeafultSearchText, AV12DefaultIsAdvancedSearch, AV11DefaultAdvFilterEntitiesJson) ;
            nGXsfl_53_idx = nGXsfl_53_bak_idx;
            sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
            SubsflControlProps_532( ) ;
         }
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E12102 ();
         if (returnInSub) return;
      }

      protected void E12102( )
      {
         AV36GXV1 = nGXsfl_53_idx;
         if ( ( AV36GXV1 > 0 ) && ( AV28WWP_SearchResults.Count >= AV36GXV1 ) )
         {
            AV28WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1));
         }
         /* Enter Routine */
         returnInSub = false;
         /* Execute user subroutine: 'EXECUTE SEARCH' */
         S122 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5AdvFilterEntities", AV5AdvFilterEntities);
         if ( gx_BV53 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV28WWP_SearchResults", AV28WWP_SearchResults);
            nGXsfl_53_bak_idx = nGXsfl_53_idx;
            gxgrFsresultcategories_refresh( AV28WWP_SearchResults, AV10DeafultSearchText, AV12DefaultIsAdvancedSearch, AV11DefaultAdvFilterEntitiesJson) ;
            nGXsfl_53_idx = nGXsfl_53_bak_idx;
            sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
            SubsflControlProps_532( ) ;
         }
      }

      protected void S112( )
      {
         /* 'LOADCOMBOADVFILTERENTITIES' Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getsearchwcdata(context ).execute(  "",  999,  0, ref  AV22EntityDescriptions, out  AV27WWP_SearchResultAux) ;
         AV6AdvFilterEntities_Data = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Item", "");
         AV40GXV4 = 1;
         while ( AV40GXV4 <= AV22EntityDescriptions.Count )
         {
            AV21EntityDescription = ((string)AV22EntityDescriptions.Item(AV40GXV4));
            AV7AdvFilterEntities_DataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item(context);
            AV7AdvFilterEntities_DataItem.gxTpr_Id = AV21EntityDescription;
            AV7AdvFilterEntities_DataItem.gxTpr_Title = AV21EntityDescription;
            AV6AdvFilterEntities_Data.Add(AV7AdvFilterEntities_DataItem, 0);
            AV40GXV4 = (int)(AV40GXV4+1);
         }
         Combo_advfilterentities_Selectedvalue_set = AV5AdvFilterEntities.ToJSonString(false);
         ucCombo_advfilterentities.SendProperty(context, "", false, Combo_advfilterentities_Internalname, "SelectedValue_set", Combo_advfilterentities_Selectedvalue_set);
      }

      protected void E13102( )
      {
         AV36GXV1 = nGXsfl_53_idx;
         if ( ( AV36GXV1 > 0 ) && ( AV28WWP_SearchResults.Count >= AV36GXV1 ) )
         {
            AV28WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1));
         }
         /* Tablefscard_Click Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.wwp_addrecentsearch(context ).execute(  AV28WWP_SearchResults,  AV26Url,  4,  3) ;
         CallWebObject(formatLink(AV26Url) );
         context.wjLocDisableFrm = 0;
      }

      protected void S122( )
      {
         /* 'EXECUTE SEARCH' Routine */
         returnInSub = false;
         divTableadvancedsearchcell_Visible = (AV23IsAdvancedSearch ? 1 : 0);
         AssignProp("", false, divTableadvancedsearchcell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTableadvancedsearchcell_Visible), 5, 0), true);
         divTablesimplesearchcell_Visible = ((!AV23IsAdvancedSearch) ? 1 : 0);
         AssignProp("", false, divTablesimplesearchcell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablesimplesearchcell_Visible), 5, 0), true);
         AV24MinimumCharsToSearch = 1;
         AV25SearchText = (AV23IsAdvancedSearch ? AV8AdvFilterText : AV25SearchText);
         AssignAttri("", false, "AV25SearchText", AV25SearchText);
         if ( StringUtil.Len( StringUtil.Trim( AV25SearchText)) >= AV24MinimumCharsToSearch )
         {
            if ( ! AV23IsAdvancedSearch )
            {
               AV5AdvFilterEntities = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
               Combo_advfilterentities_Selectedvalue_set = AV5AdvFilterEntities.ToJSonString(false);
               ucCombo_advfilterentities.SendProperty(context, "", false, Combo_advfilterentities_Internalname, "SelectedValue_set", Combo_advfilterentities_Selectedvalue_set);
            }
            GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2 = AV28WWP_SearchResults;
            new DesignSystem.Programs.wwpbaseobjects.wwp_getsearchwcdata(context ).execute(  AV25SearchText,  10,  12, ref  AV5AdvFilterEntities, out  GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2) ;
            AV28WWP_SearchResults = GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2;
            gx_BV53 = true;
         }
         else
         {
            AV28WWP_SearchResults = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
            gx_BV53 = true;
         }
         if ( AV28WWP_SearchResults.Count == 0 )
         {
            if ( StringUtil.Len( StringUtil.Trim( AV25SearchText)) < AV24MinimumCharsToSearch )
            {
               lblTxtnoresults_Caption = context.GetMessage( "WWP_Search_TypeToSearch", "");
               AssignProp("", false, lblTxtnoresults_Internalname, "Caption", lblTxtnoresults_Caption, true);
            }
            else
            {
               lblTxtnoresults_Caption = StringUtil.Format( context.GetMessage( "WWP_Search_NoResults", ""), AV25SearchText, "", "", "", "", "", "", "", "");
               AssignProp("", false, lblTxtnoresults_Internalname, "Caption", lblTxtnoresults_Caption, true);
            }
         }
         divFsresultcategoriescell_Visible = (((AV28WWP_SearchResults.Count>0)) ? 1 : 0);
         AssignProp("", false, divFsresultcategoriescell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divFsresultcategoriescell_Visible), 5, 0), true);
         divTxtnoresultscell_Visible = (((AV28WWP_SearchResults.Count==0)) ? 1 : 0);
         AssignProp("", false, divTxtnoresultscell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTxtnoresultscell_Visible), 5, 0), true);
      }

      private void E16103( )
      {
         AV36GXV1 = nGXsfl_53_idx;
         if ( AV28WWP_SearchResults.Count < AV36GXV1 )
         {
            return  ;
         }
         /* Fsresults_Load Routine */
         returnInSub = false;
         AV38GXV3 = 1;
         while ( AV38GXV3 <= ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1)).gxTpr_Result.Count )
         {
            ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1)).gxTpr_Result.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1)).gxTpr_Result.Item(AV38GXV3));
            divDisplaytype1_cell_Visible = 0;
            AssignProp("", false, divDisplaytype1_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype1_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
            divDisplaytype2_cell_Visible = 0;
            AssignProp("", false, divDisplaytype2_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype2_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
            divDisplaytype3_cell_Visible = 0;
            AssignProp("", false, divDisplaytype3_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype3_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
            divDisplaytype4_cell_Visible = 0;
            AssignProp("", false, divDisplaytype4_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype4_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
            divDisplaytype5_cell_Visible = 0;
            AssignProp("", false, divDisplaytype5_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype5_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
            if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title") == 0 )
            {
               divDisplaytype1_cell_Visible = 1;
               AssignProp("", false, divDisplaytype1_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype1_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
               AV13DisplayType1_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri("", false, edtavDisplaytype1_title_Internalname, AV13DisplayType1_Title);
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title and icon image") == 0 )
            {
               divDisplaytype2_cell_Visible = 1;
               AssignProp("", false, divDisplaytype2_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype2_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
               AV14DisplayType2_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri("", false, edtavDisplaytype2_title_Internalname, AV14DisplayType2_Title);
               lblDisplaytype2_icon_Caption = StringUtil.Format( "<i class='SearchResultIcon %1'></i>", ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2, "", "", "", "", "", "", "", "");
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title and subtitle") == 0 )
            {
               divDisplaytype3_cell_Visible = 1;
               AssignProp("", false, divDisplaytype3_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype3_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
               AV16DisplayType3_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri("", false, edtavDisplaytype3_title_Internalname, AV16DisplayType3_Title);
               AV15DisplayType3_Subtitle = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2;
               AssignAttri("", false, edtavDisplaytype3_subtitle_Internalname, AV15DisplayType3_Subtitle);
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title and image") == 0 )
            {
               divDisplaytype4_cell_Visible = 1;
               AssignProp("", false, divDisplaytype4_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype4_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
               AV18DisplayType4_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri("", false, edtavDisplaytype4_title_Internalname, AV18DisplayType4_Title);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2)) )
               {
                  AV17DisplayType4_Image = "";
                  AssignAttri("", false, edtavDisplaytype4_image_Internalname, AV17DisplayType4_Image);
                  AV39Displaytype4_image_GXI = "";
               }
               else
               {
                  AV17DisplayType4_Image = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2;
                  AssignAttri("", false, edtavDisplaytype4_image_Internalname, AV17DisplayType4_Image);
                  AV39Displaytype4_image_GXI = GXDbFile.PathToUrl( ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2, context);
               }
               edtavDisplaytype4_image_Visible = ((!String.IsNullOrEmpty(StringUtil.RTrim( ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2))) ? 1 : 0);
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title, subtitle and icon image") == 0 )
            {
               divDisplaytype5_cell_Visible = 1;
               AssignProp("", false, divDisplaytype5_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype5_cell_Visible), 5, 0), !bGXsfl_61_Refreshing);
               AV20DisplayType5_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri("", false, edtavDisplaytype5_title_Internalname, AV20DisplayType5_Title);
               AV19DisplayType5_Subtitle = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2;
               AssignAttri("", false, edtavDisplaytype5_subtitle_Internalname, AV19DisplayType5_Subtitle);
               lblDisplaytype5_icon_Caption = StringUtil.Format( "<i class='SearchResultIconWS %1'></i>", ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description3, "", "", "", "", "", "", "", "");
            }
            AV26Url = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV28WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Url;
            AssignAttri("", false, edtavUrl_Internalname, AV26Url);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 61;
            }
            sendrow_613( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_61_Refreshing )
            {
               DoAjaxLoad(61, FsresultsRow);
            }
            AV38GXV3 = (int)(AV38GXV3+1);
         }
         /*  Sending Event outputs  */
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV10DeafultSearchText = (string)getParm(obj,0);
         AssignAttri("", false, "AV10DeafultSearchText", AV10DeafultSearchText);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEAFULTSEARCHTEXT", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV10DeafultSearchText, "")), context));
         AV12DefaultIsAdvancedSearch = (bool)getParm(obj,1);
         AssignAttri("", false, "AV12DefaultIsAdvancedSearch", AV12DefaultIsAdvancedSearch);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTISADVANCEDSEARCH", GetSecureSignedToken( "", AV12DefaultIsAdvancedSearch, context));
         AV11DefaultAdvFilterEntitiesJson = (string)getParm(obj,2);
         AssignAttri("", false, "AV11DefaultAdvFilterEntitiesJson", AV11DefaultAdvFilterEntitiesJson);
         GxWebStd.gx_hidden_field( context, "gxhash_vDEFAULTADVFILTERENTITIESJSON", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV11DefaultAdvFilterEntitiesJson, "")), context));
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
         PA102( ) ;
         WS102( ) ;
         WE102( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623491057", true, true);
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
         context.AddJavascriptSource("wwpbaseobjects/wwp_search.js", "?2024121623491057", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_613( )
      {
         edtavUrl_Internalname = "vURL_"+sGXsfl_61_idx;
         edtavDisplaytype1_title_Internalname = "vDISPLAYTYPE1_TITLE_"+sGXsfl_61_idx;
         lblDisplaytype2_icon_Internalname = "DISPLAYTYPE2_ICON_"+sGXsfl_61_idx;
         edtavDisplaytype2_title_Internalname = "vDISPLAYTYPE2_TITLE_"+sGXsfl_61_idx;
         edtavDisplaytype3_title_Internalname = "vDISPLAYTYPE3_TITLE_"+sGXsfl_61_idx;
         edtavDisplaytype3_subtitle_Internalname = "vDISPLAYTYPE3_SUBTITLE_"+sGXsfl_61_idx;
         edtavDisplaytype4_image_Internalname = "vDISPLAYTYPE4_IMAGE_"+sGXsfl_61_idx;
         edtavDisplaytype4_title_Internalname = "vDISPLAYTYPE4_TITLE_"+sGXsfl_61_idx;
         lblDisplaytype5_icon_Internalname = "DISPLAYTYPE5_ICON_"+sGXsfl_61_idx;
         edtavDisplaytype5_title_Internalname = "vDISPLAYTYPE5_TITLE_"+sGXsfl_61_idx;
         edtavDisplaytype5_subtitle_Internalname = "vDISPLAYTYPE5_SUBTITLE_"+sGXsfl_61_idx;
      }

      protected void SubsflControlProps_fel_613( )
      {
         edtavUrl_Internalname = "vURL_"+sGXsfl_61_fel_idx;
         edtavDisplaytype1_title_Internalname = "vDISPLAYTYPE1_TITLE_"+sGXsfl_61_fel_idx;
         lblDisplaytype2_icon_Internalname = "DISPLAYTYPE2_ICON_"+sGXsfl_61_fel_idx;
         edtavDisplaytype2_title_Internalname = "vDISPLAYTYPE2_TITLE_"+sGXsfl_61_fel_idx;
         edtavDisplaytype3_title_Internalname = "vDISPLAYTYPE3_TITLE_"+sGXsfl_61_fel_idx;
         edtavDisplaytype3_subtitle_Internalname = "vDISPLAYTYPE3_SUBTITLE_"+sGXsfl_61_fel_idx;
         edtavDisplaytype4_image_Internalname = "vDISPLAYTYPE4_IMAGE_"+sGXsfl_61_fel_idx;
         edtavDisplaytype4_title_Internalname = "vDISPLAYTYPE4_TITLE_"+sGXsfl_61_fel_idx;
         lblDisplaytype5_icon_Internalname = "DISPLAYTYPE5_ICON_"+sGXsfl_61_fel_idx;
         edtavDisplaytype5_title_Internalname = "vDISPLAYTYPE5_TITLE_"+sGXsfl_61_fel_idx;
         edtavDisplaytype5_subtitle_Internalname = "vDISPLAYTYPE5_SUBTITLE_"+sGXsfl_61_fel_idx;
      }

      protected void sendrow_613( )
      {
         sGXsfl_61_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_61_idx), 4, 0), 4, "0") + sGXsfl_53_idx;
         SubsflControlProps_613( ) ;
         WB100( ) ;
         FsresultsRow = GXWebRow.GetNew(context,FsresultsContainer);
         if ( subFsresults_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subFsresults_Backstyle = 0;
            if ( StringUtil.StrCmp(subFsresults_Class, "") != 0 )
            {
               subFsresults_Linesclass = subFsresults_Class+"Odd";
            }
         }
         else if ( subFsresults_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subFsresults_Backstyle = 0;
            subFsresults_Backcolor = subFsresults_Allbackcolor;
            if ( StringUtil.StrCmp(subFsresults_Class, "") != 0 )
            {
               subFsresults_Linesclass = subFsresults_Class+"Uniform";
            }
         }
         else if ( subFsresults_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subFsresults_Backstyle = 1;
            if ( StringUtil.StrCmp(subFsresults_Class, "") != 0 )
            {
               subFsresults_Linesclass = subFsresults_Class+"Odd";
            }
            subFsresults_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subFsresults_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subFsresults_Backstyle = 1;
            if ( ((int)((nGXsfl_61_idx) % (2))) == 0 )
            {
               subFsresults_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subFsresults_Class, "") != 0 )
               {
                  subFsresults_Linesclass = subFsresults_Class+"Even";
               }
            }
            else
            {
               subFsresults_Backcolor = (int)(0xFFFFFF);
               if ( StringUtil.StrCmp(subFsresults_Class, "") != 0 )
               {
                  subFsresults_Linesclass = subFsresults_Class+"Odd";
               }
            }
         }
         /* Start of Columns property logic. */
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr"+" class=\""+subFsresults_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_61_idx+"\">") ;
         }
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtablefsfsresults_Internalname+"_"+sGXsfl_61_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Flex",(string)"start",(string)"top",(string)" "+"data-gx-flex"+" ",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Invisible",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Table start */
         FsresultsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtablecontentfsfsresults_Internalname+"_"+sGXsfl_61_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         FsresultsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavUrl_Internalname,context.GetMessage( "Url", ""),(string)"gx-form-item AttributeLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Single line edit */
         ROClassString = "Attribute";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavUrl_Internalname,(string)AV26Url,(string)"",(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavUrl_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavUrl_Visible,(short)0,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("cell");
         }
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("row");
         }
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("table");
         }
         /* End of table */
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"CellMarginTop SearchResultCardCell",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divTablefscard_Internalname+"_"+sGXsfl_61_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"SimpleCardTable",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype1_cell_Internalname+"_"+sGXsfl_61_idx,(int)divDisplaytype1_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable2_Internalname+"_"+sGXsfl_61_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype1_title_Internalname,context.GetMessage( "Display Type1_Title", ""),(string)"col-sm-3 AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 77,'',false,'" + sGXsfl_61_idx + "',61)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype1_title_Internalname,(string)AV13DisplayType1_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,77);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype1_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype1_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype2_cell_Internalname+"_"+sGXsfl_61_idx,(int)divDisplaytype2_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Table start */
         FsresultsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtable3_Internalname+"_"+sGXsfl_61_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         FsresultsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Text block */
         FsresultsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblDisplaytype2_icon_Internalname,(string)lblDisplaytype2_icon_Caption,(string)"",(string)"",(string)lblDisplaytype2_icon_Jsonclick,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlock",(short)0,(string)"",(short)1,(short)1,(short)0,(short)2});
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("cell");
         }
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype2_title_Internalname,context.GetMessage( "Display Type2_Title", ""),(string)"gx-form-item AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 86,'',false,'" + sGXsfl_61_idx + "',61)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype2_title_Internalname,(string)AV14DisplayType2_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,86);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype2_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype2_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("cell");
         }
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("row");
         }
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("table");
         }
         /* End of table */
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype3_cell_Internalname+"_"+sGXsfl_61_idx,(int)divDisplaytype3_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable4_Internalname+"_"+sGXsfl_61_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         sendrow_61330( ) ;
      }

      protected void sendrow_61330( )
      {
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype3_title_Internalname,context.GetMessage( "Display Type3_Title", ""),(string)"col-sm-3 AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 93,'',false,'" + sGXsfl_61_idx + "',61)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype3_title_Internalname,(string)AV16DisplayType3_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,93);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype3_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype3_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype3_subtitle_Internalname,context.GetMessage( "Display Type3_Subtitle", ""),(string)"col-sm-3 AttributeSearchResultSubtitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 97,'',false,'" + sGXsfl_61_idx + "',61)\"";
         ROClassString = "AttributeSearchResultSubtitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype3_subtitle_Internalname,(string)AV15DisplayType3_Subtitle,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,97);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype3_subtitle_Jsonclick,(short)0,(string)"AttributeSearchResultSubtitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype3_subtitle_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype4_cell_Internalname+"_"+sGXsfl_61_idx,(int)divDisplaytype4_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable5_Internalname+"_"+sGXsfl_61_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Flex",(string)"start",(string)"top",(string)" "+"data-gx-flex"+" ",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"AttributeSearchResultImageCell",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)"",context.GetMessage( "Display Type4_Image", ""),(string)"gx-form-item AttributeSearchResultImageLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Static Bitmap Variable */
         ClassString = "AttributeSearchResultImage" + " " + ((StringUtil.StrCmp(edtavDisplaytype4_image_gximage, "")==0) ? "" : "GX_Image_"+edtavDisplaytype4_image_gximage+"_Class");
         StyleString = "";
         AV17DisplayType4_Image_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV17DisplayType4_Image))&&String.IsNullOrEmpty(StringUtil.RTrim( AV39Displaytype4_image_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV17DisplayType4_Image)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV17DisplayType4_Image)) ? AV39Displaytype4_image_GXI : context.PathToRelativeUrl( AV17DisplayType4_Image));
         FsresultsRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype4_image_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavDisplaytype4_image_Visible,(short)0,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"",(short)0,(string)"",(short)0,(short)0,(short)0,(string)"",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(bool)AV17DisplayType4_Image_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;align-self:center;",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype4_title_Internalname,context.GetMessage( "Display Type4_Title", ""),(string)"gx-form-item AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 106,'',false,'" + sGXsfl_61_idx + "',61)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype4_title_Internalname,(string)AV18DisplayType4_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,106);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype4_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype4_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype5_cell_Internalname+"_"+sGXsfl_61_idx,(int)divDisplaytype5_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Table start */
         FsresultsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtable6_Internalname+"_"+sGXsfl_61_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         FsresultsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Text block */
         FsresultsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblDisplaytype5_icon_Internalname,(string)lblDisplaytype5_icon_Caption,(string)"",(string)"",(string)lblDisplaytype5_icon_Jsonclick,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlock",(short)0,(string)"",(short)1,(short)1,(short)0,(short)2});
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("cell");
         }
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable7_Internalname+"_"+sGXsfl_61_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype5_title_Internalname,context.GetMessage( "Display Type5_Title", ""),(string)"col-sm-3 AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 118,'',false,'" + sGXsfl_61_idx + "',61)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype5_title_Internalname,(string)AV20DisplayType5_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,118);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype5_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype5_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype5_subtitle_Internalname,context.GetMessage( "Display Type5_Subtitle", ""),(string)"col-sm-3 AttributeSearchResultSubtitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 122,'',false,'" + sGXsfl_61_idx + "',61)\"";
         ROClassString = "AttributeSearchResultSubtitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype5_subtitle_Internalname,(string)AV19DisplayType5_Subtitle,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,122);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype5_subtitle_Jsonclick,(short)0,(string)"AttributeSearchResultSubtitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype5_subtitle_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)61,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("cell");
         }
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("row");
         }
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("table");
         }
         /* End of table */
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         send_integrity_lvl_hashes103( ) ;
         /* End of Columns property logic. */
         FsresultsContainer.AddRow(FsresultsRow);
         nGXsfl_61_idx = ((subFsresults_Islastpage==1)&&(nGXsfl_61_idx+1>subFsresults_fnc_Recordsperpage( )) ? 1 : nGXsfl_61_idx+1);
         sGXsfl_61_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_61_idx), 4, 0), 4, "0") + sGXsfl_53_idx;
         SubsflControlProps_613( ) ;
         /* End function sendrow_613 */
      }

      protected void SubsflControlProps_532( )
      {
         edtavWwp_searchresults__categoryname_Internalname = "WWP_SEARCHRESULTS__CATEGORYNAME_"+sGXsfl_53_idx;
         lblTxtshowingonly_Internalname = "TXTSHOWINGONLY_"+sGXsfl_53_idx;
         subFsresults_Internalname = "FSRESULTS_"+sGXsfl_53_idx;
      }

      protected void SubsflControlProps_fel_532( )
      {
         edtavWwp_searchresults__categoryname_Internalname = "WWP_SEARCHRESULTS__CATEGORYNAME_"+sGXsfl_53_fel_idx;
         lblTxtshowingonly_Internalname = "TXTSHOWINGONLY_"+sGXsfl_53_fel_idx;
         subFsresults_Internalname = "FSRESULTS_"+sGXsfl_53_fel_idx;
      }

      protected void sendrow_532( )
      {
         sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
         SubsflControlProps_532( ) ;
         WB100( ) ;
         FsresultcategoriesRow = GXWebRow.GetNew(context,FsresultcategoriesContainer);
         if ( subFsresultcategories_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subFsresultcategories_Backstyle = 0;
            if ( StringUtil.StrCmp(subFsresultcategories_Class, "") != 0 )
            {
               subFsresultcategories_Linesclass = subFsresultcategories_Class+"Odd";
            }
         }
         else if ( subFsresultcategories_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subFsresultcategories_Backstyle = 0;
            subFsresultcategories_Backcolor = subFsresultcategories_Allbackcolor;
            if ( StringUtil.StrCmp(subFsresultcategories_Class, "") != 0 )
            {
               subFsresultcategories_Linesclass = subFsresultcategories_Class+"Uniform";
            }
         }
         else if ( subFsresultcategories_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subFsresultcategories_Backstyle = 1;
            if ( StringUtil.StrCmp(subFsresultcategories_Class, "") != 0 )
            {
               subFsresultcategories_Linesclass = subFsresultcategories_Class+"Odd";
            }
            subFsresultcategories_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subFsresultcategories_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subFsresultcategories_Backstyle = 1;
            if ( ((int)((nGXsfl_53_idx) % (2))) == 0 )
            {
               subFsresultcategories_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subFsresultcategories_Class, "") != 0 )
               {
                  subFsresultcategories_Linesclass = subFsresultcategories_Class+"Even";
               }
            }
            else
            {
               subFsresultcategories_Backcolor = (int)(0xFFFFFF);
               if ( StringUtil.StrCmp(subFsresultcategories_Class, "") != 0 )
               {
                  subFsresultcategories_Linesclass = subFsresultcategories_Class+"Odd";
               }
            }
         }
         /* Start of Columns property logic. */
         if ( FsresultcategoriesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr"+" class=\""+subFsresultcategories_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_53_idx+"\">") ;
         }
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtablefsfsresultcategories_Internalname+"_"+sGXsfl_53_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 SearchResultCategoryCell",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultcategoriesRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavWwp_searchresults__categoryname_Internalname,context.GetMessage( "Category Name", ""),(string)"col-sm-3 AttributeLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 58,'',false,'" + sGXsfl_53_idx + "',53)\"";
         ROClassString = "Attribute";
         FsresultcategoriesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavWwp_searchresults__categoryname_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV28WWP_SearchResults.Item(AV36GXV1)).gxTpr_Categoryname,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,58);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavWwp_searchresults__categoryname_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavWwp_searchresults__categoryname_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)53,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /*  Child Grid Control  */
         FsresultcategoriesRow.AddColumnProperties("subfile", -1, isAjaxCallMode( ), new Object[] {(string)"FsresultsContainer"});
         if ( isAjaxCallMode( ) )
         {
            FsresultsContainer = new GXWebGrid( context);
         }
         else
         {
            FsresultsContainer.Clear();
         }
         FsresultsContainer.SetIsFreestyle(true);
         FsresultsContainer.SetWrapped(nGXWrapped);
         StartGridControl61( ) ;
         RF103( ) ;
         nRC_GXsfl_61 = (int)(nGXsfl_61_idx-1);
         GXCCtl = "nRC_GXsfl_61_" + sGXsfl_53_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_61), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "</table>") ;
         }
         else
         {
            if ( ! isAjaxCallMode( ) )
            {
               GxWebStd.gx_hidden_field( context, "FsresultsContainerData"+"_"+sGXsfl_53_idx, FsresultsContainer.ToJavascriptSource());
            }
            if ( isAjaxCallMode( ) )
            {
               FsresultcategoriesRow.AddGrid("Fsresults", FsresultsContainer);
            }
            if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
            {
               GxWebStd.gx_hidden_field( context, "FsresultsContainerData"+"V_"+sGXsfl_53_idx, FsresultsContainer.GridValuesHidden());
            }
            else
            {
               context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"FsresultsContainerData"+"V_"+sGXsfl_53_idx+"\" value='"+FsresultsContainer.GridValuesHidden()+"'/>") ;
            }
         }
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"end",(string)"top",(string)"",(string)"",(string)"div"});
         /* Text block */
         FsresultcategoriesRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblTxtshowingonly_Internalname,(string)lblTxtshowingonly_Caption,(string)"",(string)"",(string)lblTxtshowingonly_Jsonclick,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlockShowingOnly",(short)0,(string)"",(short)1,(short)1,(short)0,(short)0});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"end",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         send_integrity_lvl_hashes102( ) ;
         GXCCtl = "subFsresults_Recordcount_" + sGXsfl_53_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         /* End of Columns property logic. */
         FsresultcategoriesContainer.AddRow(FsresultcategoriesRow);
         nGXsfl_53_idx = ((subFsresultcategories_Islastpage==1)&&(nGXsfl_53_idx+1>subFsresultcategories_fnc_Recordsperpage( )) ? 1 : nGXsfl_53_idx+1);
         sGXsfl_53_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_53_idx), 4, 0), 4, "0");
         SubsflControlProps_532( ) ;
         /* End function sendrow_532 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl53( )
      {
         if ( FsresultcategoriesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"FsresultcategoriesContainer"+"DivS\" data-gxgridid=\"53\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subFsresultcategories_Internalname, subFsresultcategories_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            FsresultcategoriesContainer.AddObjectProperty("GridName", "Fsresultcategories");
         }
         else
         {
            FsresultcategoriesContainer.AddObjectProperty("GridName", "Fsresultcategories");
            FsresultcategoriesContainer.AddObjectProperty("Header", subFsresultcategories_Header);
            FsresultcategoriesContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            FsresultcategoriesContainer.AddObjectProperty("Class", "FreeStyleGrid");
            FsresultcategoriesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Backcolorstyle), 1, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("CmpContext", "");
            FsresultcategoriesContainer.AddObjectProperty("InMasterPage", "false");
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavWwp_searchresults__categoryname_Enabled), 5, 0, ".", "")));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesColumn.AddObjectProperty("Value", lblTxtshowingonly_Caption);
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultcategoriesContainer.AddColumnProperties(FsresultcategoriesColumn);
            FsresultcategoriesContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Selectedindex), 4, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Allowselection), 1, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Selectioncolor), 9, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Allowhovering), 1, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Hoveringcolor), 9, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Allowcollapsing), 1, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void StartGridControl61( )
      {
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"FsresultsContainer"+"DivS\" data-gxgridid=\"61\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subFsresults_Internalname, subFsresults_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            FsresultsContainer.AddObjectProperty("GridName", "Fsresults");
         }
         else
         {
            FsresultsContainer.AddObjectProperty("GridName", "Fsresults");
            FsresultsContainer.AddObjectProperty("Header", subFsresults_Header);
            FsresultsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            FsresultsContainer.AddObjectProperty("Class", "FreeStyleGrid");
            FsresultsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Backcolorstyle), 1, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("CmpContext", "");
            FsresultsContainer.AddObjectProperty("InMasterPage", "false");
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV26Url));
            FsresultsColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavUrl_Visible), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV13DisplayType1_Title));
            FsresultsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype1_title_Enabled), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", lblDisplaytype2_icon_Caption);
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV14DisplayType2_Title));
            FsresultsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype2_title_Enabled), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV16DisplayType3_Title));
            FsresultsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype3_title_Enabled), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV15DisplayType3_Subtitle));
            FsresultsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype3_subtitle_Enabled), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", context.convertURL( AV17DisplayType4_Image));
            FsresultsColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype4_image_Visible), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV18DisplayType4_Title));
            FsresultsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype4_title_Enabled), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", lblDisplaytype2_icon_Caption);
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV20DisplayType5_Title));
            FsresultsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype5_title_Enabled), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV19DisplayType5_Subtitle));
            FsresultsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDisplaytype5_subtitle_Enabled), 5, 0, ".", "")));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Selectedindex), 4, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Allowselection), 1, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Selectioncolor), 9, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Allowhovering), 1, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Hoveringcolor), 9, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Allowcollapsing), 1, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavSearchtext_Internalname = "vSEARCHTEXT";
         bttBtnenter_Internalname = "BTNENTER";
         divUnnamedtable10_Internalname = "UNNAMEDTABLE10";
         Btnadvancedsearch_Internalname = "BTNADVANCEDSEARCH";
         divUnnamedtable8_Internalname = "UNNAMEDTABLE8";
         divTablesimplesearchcell_Internalname = "TABLESIMPLESEARCHCELL";
         edtavAdvfiltertext_Internalname = "vADVFILTERTEXT";
         lblTextblockcombo_advfilterentities_Internalname = "TEXTBLOCKCOMBO_ADVFILTERENTITIES";
         Combo_advfilterentities_Internalname = "COMBO_ADVFILTERENTITIES";
         divTablesplittedadvfilterentities_Internalname = "TABLESPLITTEDADVFILTERENTITIES";
         bttBtnbtnadvsearch_Internalname = "BTNBTNADVSEARCH";
         divUnnamedtable9_Internalname = "UNNAMEDTABLE9";
         Btnbasicsearch_Internalname = "BTNBASICSEARCH";
         divTableadvancedsearch_Internalname = "TABLEADVANCEDSEARCH";
         divTableadvancedsearchcell_Internalname = "TABLEADVANCEDSEARCHCELL";
         divTableheader_Internalname = "TABLEHEADER";
         edtavWwp_searchresults__categoryname_Internalname = "WWP_SEARCHRESULTS__CATEGORYNAME";
         edtavUrl_Internalname = "vURL";
         tblUnnamedtablecontentfsfsresults_Internalname = "UNNAMEDTABLECONTENTFSFSRESULTS";
         edtavDisplaytype1_title_Internalname = "vDISPLAYTYPE1_TITLE";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         divDisplaytype1_cell_Internalname = "DISPLAYTYPE1_CELL";
         lblDisplaytype2_icon_Internalname = "DISPLAYTYPE2_ICON";
         edtavDisplaytype2_title_Internalname = "vDISPLAYTYPE2_TITLE";
         tblUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         divDisplaytype2_cell_Internalname = "DISPLAYTYPE2_CELL";
         edtavDisplaytype3_title_Internalname = "vDISPLAYTYPE3_TITLE";
         edtavDisplaytype3_subtitle_Internalname = "vDISPLAYTYPE3_SUBTITLE";
         divUnnamedtable4_Internalname = "UNNAMEDTABLE4";
         divDisplaytype3_cell_Internalname = "DISPLAYTYPE3_CELL";
         edtavDisplaytype4_image_Internalname = "vDISPLAYTYPE4_IMAGE";
         edtavDisplaytype4_title_Internalname = "vDISPLAYTYPE4_TITLE";
         divUnnamedtable5_Internalname = "UNNAMEDTABLE5";
         divDisplaytype4_cell_Internalname = "DISPLAYTYPE4_CELL";
         lblDisplaytype5_icon_Internalname = "DISPLAYTYPE5_ICON";
         edtavDisplaytype5_title_Internalname = "vDISPLAYTYPE5_TITLE";
         edtavDisplaytype5_subtitle_Internalname = "vDISPLAYTYPE5_SUBTITLE";
         divUnnamedtable7_Internalname = "UNNAMEDTABLE7";
         tblUnnamedtable6_Internalname = "UNNAMEDTABLE6";
         divDisplaytype5_cell_Internalname = "DISPLAYTYPE5_CELL";
         divTablefscard_Internalname = "TABLEFSCARD";
         divUnnamedtablefsfsresults_Internalname = "UNNAMEDTABLEFSFSRESULTS";
         lblTxtshowingonly_Internalname = "TXTSHOWINGONLY";
         divUnnamedtablefsfsresultcategories_Internalname = "UNNAMEDTABLEFSFSRESULTCATEGORIES";
         divFsresultcategoriescell_Internalname = "FSRESULTCATEGORIESCELL";
         lblTxtnoresults_Internalname = "TXTNORESULTS";
         divTxtnoresultscell_Internalname = "TXTNORESULTSCELL";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         Dvpanel_unnamedtable1_Internalname = "DVPANEL_UNNAMEDTABLE1";
         divTablemain_Internalname = "TABLEMAIN";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subFsresults_Internalname = "FSRESULTS";
         subFsresultcategories_Internalname = "FSRESULTCATEGORIES";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subFsresults_Allowcollapsing = 0;
         lblDisplaytype2_icon_Caption = context.GetMessage( "<i class='fa fa-home'></i>", "");
         subFsresultcategories_Allowcollapsing = 0;
         lblTxtshowingonly_Caption = context.GetMessage( "Showing only the first 3 results...", "");
         lblTxtshowingonly_Caption = context.GetMessage( "Showing only the first 3 results...", "");
         edtavWwp_searchresults__categoryname_Jsonclick = "";
         edtavWwp_searchresults__categoryname_Enabled = 0;
         subFsresultcategories_Class = "FreeStyleGrid";
         edtavDisplaytype5_subtitle_Jsonclick = "";
         edtavDisplaytype5_subtitle_Enabled = 0;
         edtavDisplaytype5_title_Jsonclick = "";
         edtavDisplaytype5_title_Enabled = 0;
         lblDisplaytype5_icon_Caption = context.GetMessage( "<i class='fa fa-home'></i>", "");
         divDisplaytype5_cell_Visible = 1;
         edtavDisplaytype4_title_Jsonclick = "";
         edtavDisplaytype4_title_Enabled = 0;
         edtavDisplaytype4_image_gximage = "";
         edtavDisplaytype4_image_Visible = 1;
         divDisplaytype4_cell_Visible = 1;
         edtavDisplaytype3_subtitle_Jsonclick = "";
         edtavDisplaytype3_subtitle_Enabled = 0;
         edtavDisplaytype3_title_Jsonclick = "";
         edtavDisplaytype3_title_Enabled = 0;
         divDisplaytype3_cell_Visible = 1;
         edtavDisplaytype2_title_Jsonclick = "";
         edtavDisplaytype2_title_Enabled = 0;
         lblDisplaytype2_icon_Caption = context.GetMessage( "<i class='fa fa-home'></i>", "");
         divDisplaytype2_cell_Visible = 1;
         edtavDisplaytype1_title_Jsonclick = "";
         edtavDisplaytype1_title_Enabled = 0;
         divDisplaytype1_cell_Visible = 1;
         edtavUrl_Jsonclick = "";
         subFsresults_Class = "FreeStyleGrid";
         subFsresults_Backcolorstyle = 0;
         subFsresultcategories_Backcolorstyle = 0;
         edtavWwp_searchresults__categoryname_Enabled = -1;
         lblTxtnoresults_Caption = context.GetMessage( "No results found for '%1'.", "");
         divTxtnoresultscell_Visible = 1;
         divFsresultcategoriescell_Visible = 1;
         Btnbasicsearch_Class = "IconButtonLink";
         Btnbasicsearch_Caption = context.GetMessage( "WWP_Search_BasicSearch", "");
         Btnbasicsearch_Beforeiconclass = "fas fa-filter";
         Btnbasicsearch_Tooltiptext = "";
         Combo_advfilterentities_Multiplevaluestype = "Tags";
         Combo_advfilterentities_Emptyitem = Convert.ToBoolean( 0);
         Combo_advfilterentities_Includeonlyselectedoption = Convert.ToBoolean( -1);
         Combo_advfilterentities_Allowmultipleselection = Convert.ToBoolean( -1);
         Combo_advfilterentities_Cls = "ExtendedCombo Attribute";
         Combo_advfilterentities_Caption = "";
         edtavAdvfiltertext_Jsonclick = "";
         edtavAdvfiltertext_Enabled = 1;
         divTableadvancedsearchcell_Visible = 1;
         Btnadvancedsearch_Class = "IconButtonLink";
         Btnadvancedsearch_Caption = context.GetMessage( "WWP_Search_AdvancedSearch", "");
         Btnadvancedsearch_Beforeiconclass = "fas fa-filter";
         Btnadvancedsearch_Tooltiptext = "";
         edtavSearchtext_Jsonclick = "";
         edtavSearchtext_Enabled = 1;
         divTablesimplesearchcell_Visible = 1;
         Dvpanel_unnamedtable1_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Iconposition = "Right";
         Dvpanel_unnamedtable1_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Collapsible = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Title = context.GetMessage( "Buscar en el Sitio", "");
         Dvpanel_unnamedtable1_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_unnamedtable1_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_unnamedtable1_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_unnamedtable1_Width = "100%";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Search results", "");
         edtavUrl_Visible = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"FSRESULTCATEGORIES_nEOF"},{"av":"FSRESULTS_nFirstRecordOnPage"},{"av":"FSRESULTS_nEOF"},{"av":"edtavUrl_Visible","ctrl":"vURL","prop":"Visible"},{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53},{"av":"AV10DeafultSearchText","fld":"vDEAFULTSEARCHTEXT","hsh":true},{"av":"AV12DefaultIsAdvancedSearch","fld":"vDEFAULTISADVANCEDSEARCH","hsh":true},{"av":"AV11DefaultAdvFilterEntitiesJson","fld":"vDEFAULTADVFILTERENTITIESJSON","hsh":true}]}""");
         setEventMetadata("FSRESULTCATEGORIES.LOAD","""{"handler":"E15102","iparms":[{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53}]""");
         setEventMetadata("FSRESULTCATEGORIES.LOAD",""","oparms":[{"av":"lblTxtshowingonly_Caption","ctrl":"TXTSHOWINGONLY","prop":"Caption"}]}""");
         setEventMetadata("FSRESULTS.LOAD","""{"handler":"E16103","iparms":[{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53}]""");
         setEventMetadata("FSRESULTS.LOAD",""","oparms":[{"av":"divDisplaytype1_cell_Visible","ctrl":"DISPLAYTYPE1_CELL","prop":"Visible"},{"av":"divDisplaytype2_cell_Visible","ctrl":"DISPLAYTYPE2_CELL","prop":"Visible"},{"av":"divDisplaytype3_cell_Visible","ctrl":"DISPLAYTYPE3_CELL","prop":"Visible"},{"av":"divDisplaytype4_cell_Visible","ctrl":"DISPLAYTYPE4_CELL","prop":"Visible"},{"av":"divDisplaytype5_cell_Visible","ctrl":"DISPLAYTYPE5_CELL","prop":"Visible"},{"av":"AV13DisplayType1_Title","fld":"vDISPLAYTYPE1_TITLE"},{"av":"AV14DisplayType2_Title","fld":"vDISPLAYTYPE2_TITLE"},{"av":"lblDisplaytype2_icon_Caption","ctrl":"DISPLAYTYPE2_ICON","prop":"Caption"},{"av":"AV16DisplayType3_Title","fld":"vDISPLAYTYPE3_TITLE"},{"av":"AV15DisplayType3_Subtitle","fld":"vDISPLAYTYPE3_SUBTITLE"},{"av":"AV18DisplayType4_Title","fld":"vDISPLAYTYPE4_TITLE"},{"av":"AV17DisplayType4_Image","fld":"vDISPLAYTYPE4_IMAGE"},{"av":"edtavDisplaytype4_image_Visible","ctrl":"vDISPLAYTYPE4_IMAGE","prop":"Visible"},{"av":"AV20DisplayType5_Title","fld":"vDISPLAYTYPE5_TITLE"},{"av":"AV19DisplayType5_Subtitle","fld":"vDISPLAYTYPE5_SUBTITLE"},{"av":"lblDisplaytype5_icon_Caption","ctrl":"DISPLAYTYPE5_ICON","prop":"Caption"},{"av":"AV26Url","fld":"vURL"}]}""");
         setEventMetadata("'DOBTNADVSEARCH'","""{"handler":"E11102","iparms":[{"av":"AV23IsAdvancedSearch","fld":"vISADVANCEDSEARCH"},{"av":"AV8AdvFilterText","fld":"vADVFILTERTEXT"},{"av":"AV25SearchText","fld":"vSEARCHTEXT"},{"av":"AV5AdvFilterEntities","fld":"vADVFILTERENTITIES"},{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53},{"av":"FSRESULTCATEGORIES_nEOF"},{"av":"FSRESULTS_nFirstRecordOnPage"},{"av":"FSRESULTS_nEOF"},{"av":"edtavUrl_Visible","ctrl":"vURL","prop":"Visible"},{"av":"AV10DeafultSearchText","fld":"vDEAFULTSEARCHTEXT","hsh":true},{"av":"AV12DefaultIsAdvancedSearch","fld":"vDEFAULTISADVANCEDSEARCH","hsh":true},{"av":"AV11DefaultAdvFilterEntitiesJson","fld":"vDEFAULTADVFILTERENTITIESJSON","hsh":true}]""");
         setEventMetadata("'DOBTNADVSEARCH'",""","oparms":[{"av":"divTableadvancedsearchcell_Visible","ctrl":"TABLEADVANCEDSEARCHCELL","prop":"Visible"},{"av":"divTablesimplesearchcell_Visible","ctrl":"TABLESIMPLESEARCHCELL","prop":"Visible"},{"av":"AV25SearchText","fld":"vSEARCHTEXT"},{"av":"AV5AdvFilterEntities","fld":"vADVFILTERENTITIES"},{"av":"Combo_advfilterentities_Selectedvalue_set","ctrl":"COMBO_ADVFILTERENTITIES","prop":"SelectedValue_set"},{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53},{"av":"lblTxtnoresults_Caption","ctrl":"TXTNORESULTS","prop":"Caption"},{"av":"divFsresultcategoriescell_Visible","ctrl":"FSRESULTCATEGORIESCELL","prop":"Visible"},{"av":"divTxtnoresultscell_Visible","ctrl":"TXTNORESULTSCELL","prop":"Visible"}]}""");
         setEventMetadata("ENTER","""{"handler":"E12102","iparms":[{"av":"AV23IsAdvancedSearch","fld":"vISADVANCEDSEARCH"},{"av":"AV8AdvFilterText","fld":"vADVFILTERTEXT"},{"av":"AV25SearchText","fld":"vSEARCHTEXT"},{"av":"AV5AdvFilterEntities","fld":"vADVFILTERENTITIES"},{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53},{"av":"FSRESULTCATEGORIES_nEOF"},{"av":"FSRESULTS_nFirstRecordOnPage"},{"av":"FSRESULTS_nEOF"},{"av":"edtavUrl_Visible","ctrl":"vURL","prop":"Visible"},{"av":"AV10DeafultSearchText","fld":"vDEAFULTSEARCHTEXT","hsh":true},{"av":"AV12DefaultIsAdvancedSearch","fld":"vDEFAULTISADVANCEDSEARCH","hsh":true},{"av":"AV11DefaultAdvFilterEntitiesJson","fld":"vDEFAULTADVFILTERENTITIESJSON","hsh":true}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"divTableadvancedsearchcell_Visible","ctrl":"TABLEADVANCEDSEARCHCELL","prop":"Visible"},{"av":"divTablesimplesearchcell_Visible","ctrl":"TABLESIMPLESEARCHCELL","prop":"Visible"},{"av":"AV25SearchText","fld":"vSEARCHTEXT"},{"av":"AV5AdvFilterEntities","fld":"vADVFILTERENTITIES"},{"av":"Combo_advfilterentities_Selectedvalue_set","ctrl":"COMBO_ADVFILTERENTITIES","prop":"SelectedValue_set"},{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53},{"av":"lblTxtnoresults_Caption","ctrl":"TXTNORESULTS","prop":"Caption"},{"av":"divFsresultcategoriescell_Visible","ctrl":"FSRESULTCATEGORIESCELL","prop":"Visible"},{"av":"divTxtnoresultscell_Visible","ctrl":"TXTNORESULTSCELL","prop":"Visible"}]}""");
         setEventMetadata("TABLEFSCARD.CLICK","""{"handler":"E13102","iparms":[{"av":"AV28WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":53},{"av":"nGXsfl_53_idx","ctrl":"GRID","prop":"GridCurrRow","grid":53},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_53","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":53},{"av":"AV26Url","fld":"vURL"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Displaytype5_subtitle","iparms":[]}""");
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
         wcpOAV10DeafultSearchText = "";
         wcpOAV11DefaultAdvFilterEntitiesJson = "";
         Combo_advfilterentities_Selectedvalue_get = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV28WWP_SearchResults = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV9DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV6AdvFilterEntities_Data = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Item", "");
         AV5AdvFilterEntities = new GxSimpleCollection<string>();
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ucDvpanel_unnamedtable1 = new GXUserControl();
         TempTags = "";
         AV25SearchText = "";
         ClassString = "";
         StyleString = "";
         bttBtnenter_Jsonclick = "";
         ucBtnadvancedsearch = new GXUserControl();
         AV8AdvFilterText = "";
         lblTextblockcombo_advfilterentities_Jsonclick = "";
         ucCombo_advfilterentities = new GXUserControl();
         bttBtnbtnadvsearch_Jsonclick = "";
         ucBtnbasicsearch = new GXUserControl();
         FsresultcategoriesContainer = new GXWebGrid( context);
         sStyleString = "";
         lblTxtnoresults_Jsonclick = "";
         FsresultsContainer = new GXWebGrid( context);
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXCCtl = "";
         AV26Url = "";
         AV13DisplayType1_Title = "";
         AV14DisplayType2_Title = "";
         AV16DisplayType3_Title = "";
         AV15DisplayType3_Subtitle = "";
         AV17DisplayType4_Image = "";
         AV39Displaytype4_image_GXI = "";
         AV18DisplayType4_Title = "";
         AV20DisplayType5_Title = "";
         AV19DisplayType5_Subtitle = "";
         GXDecQS = "";
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV32WebSession = context.GetSession();
         FsresultcategoriesRow = new GXWebRow();
         AV22EntityDescriptions = new GxSimpleCollection<string>();
         AV27WWP_SearchResultAux = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         AV21EntityDescription = "";
         AV7AdvFilterEntities_DataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item(context);
         Combo_advfilterentities_Selectedvalue_set = "";
         GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         FsresultsRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subFsresults_Linesclass = "";
         ROClassString = "";
         lblDisplaytype2_icon_Jsonclick = "";
         sImgUrl = "";
         lblDisplaytype5_icon_Jsonclick = "";
         subFsresultcategories_Linesclass = "";
         lblTxtshowingonly_Jsonclick = "";
         subFsresultcategories_Header = "";
         FsresultcategoriesColumn = new GXWebColumn();
         subFsresults_Header = "";
         FsresultsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavWwp_searchresults__categoryname_Enabled = 0;
         edtavDisplaytype1_title_Enabled = 0;
         edtavDisplaytype2_title_Enabled = 0;
         edtavDisplaytype3_title_Enabled = 0;
         edtavDisplaytype3_subtitle_Enabled = 0;
         edtavDisplaytype4_title_Enabled = 0;
         edtavDisplaytype5_title_Enabled = 0;
         edtavDisplaytype5_subtitle_Enabled = 0;
      }

      private short nRcdExists_3 ;
      private short nIsMod_3 ;
      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short subFsresultcategories_Backcolorstyle ;
      private short subFsresults_Backcolorstyle ;
      private short FSRESULTCATEGORIES_nEOF ;
      private short FSRESULTS_nEOF ;
      private short AV24MinimumCharsToSearch ;
      private short nGXWrapped ;
      private short subFsresults_Backstyle ;
      private short subFsresultcategories_Backstyle ;
      private short subFsresultcategories_Allowselection ;
      private short subFsresultcategories_Allowhovering ;
      private short subFsresultcategories_Allowcollapsing ;
      private short subFsresultcategories_Collapsed ;
      private short subFsresults_Allowselection ;
      private short subFsresults_Allowhovering ;
      private short subFsresults_Allowcollapsing ;
      private short subFsresults_Collapsed ;
      private int nRC_GXsfl_61 ;
      private int nGXsfl_61_idx=1 ;
      private int nRC_GXsfl_53 ;
      private int edtavUrl_Visible ;
      private int nGXsfl_53_idx=1 ;
      private int divTablesimplesearchcell_Visible ;
      private int edtavSearchtext_Enabled ;
      private int divTableadvancedsearchcell_Visible ;
      private int edtavAdvfiltertext_Enabled ;
      private int divFsresultcategoriescell_Visible ;
      private int AV36GXV1 ;
      private int divTxtnoresultscell_Visible ;
      private int AV38GXV3 ;
      private int subFsresults_Recordcount ;
      private int subFsresultcategories_Islastpage ;
      private int subFsresults_Islastpage ;
      private int edtavWwp_searchresults__categoryname_Enabled ;
      private int edtavDisplaytype1_title_Enabled ;
      private int edtavDisplaytype2_title_Enabled ;
      private int edtavDisplaytype3_title_Enabled ;
      private int edtavDisplaytype3_subtitle_Enabled ;
      private int edtavDisplaytype4_title_Enabled ;
      private int edtavDisplaytype5_title_Enabled ;
      private int edtavDisplaytype5_subtitle_Enabled ;
      private int nGXsfl_53_fel_idx=1 ;
      private int nGXsfl_53_bak_idx=1 ;
      private int AV40GXV4 ;
      private int divDisplaytype1_cell_Visible ;
      private int divDisplaytype2_cell_Visible ;
      private int divDisplaytype3_cell_Visible ;
      private int divDisplaytype4_cell_Visible ;
      private int divDisplaytype5_cell_Visible ;
      private int edtavDisplaytype4_image_Visible ;
      private int idxLst ;
      private int subFsresults_Backcolor ;
      private int subFsresults_Allbackcolor ;
      private int subFsresultcategories_Backcolor ;
      private int subFsresultcategories_Allbackcolor ;
      private int subFsresultcategories_Selectedindex ;
      private int subFsresultcategories_Selectioncolor ;
      private int subFsresultcategories_Hoveringcolor ;
      private int subFsresults_Selectedindex ;
      private int subFsresults_Selectioncolor ;
      private int subFsresults_Hoveringcolor ;
      private long FSRESULTS_nCurrentRecord ;
      private long FSRESULTCATEGORIES_nCurrentRecord ;
      private long FSRESULTS_nFirstRecordOnPage ;
      private long FSRESULTCATEGORIES_nFirstRecordOnPage ;
      private string Combo_advfilterentities_Selectedvalue_get ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_61_idx="0001" ;
      private string edtavUrl_Internalname ;
      private string sGXsfl_53_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divTablemain_Internalname ;
      private string Dvpanel_unnamedtable1_Width ;
      private string Dvpanel_unnamedtable1_Cls ;
      private string Dvpanel_unnamedtable1_Title ;
      private string Dvpanel_unnamedtable1_Iconposition ;
      private string Dvpanel_unnamedtable1_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string divTableheader_Internalname ;
      private string divTablesimplesearchcell_Internalname ;
      private string divUnnamedtable8_Internalname ;
      private string divUnnamedtable10_Internalname ;
      private string edtavSearchtext_Internalname ;
      private string TempTags ;
      private string edtavSearchtext_Jsonclick ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Jsonclick ;
      private string Btnadvancedsearch_Tooltiptext ;
      private string Btnadvancedsearch_Beforeiconclass ;
      private string Btnadvancedsearch_Caption ;
      private string Btnadvancedsearch_Class ;
      private string Btnadvancedsearch_Internalname ;
      private string divTableadvancedsearchcell_Internalname ;
      private string divTableadvancedsearch_Internalname ;
      private string divUnnamedtable9_Internalname ;
      private string edtavAdvfiltertext_Internalname ;
      private string edtavAdvfiltertext_Jsonclick ;
      private string divTablesplittedadvfilterentities_Internalname ;
      private string lblTextblockcombo_advfilterentities_Internalname ;
      private string lblTextblockcombo_advfilterentities_Jsonclick ;
      private string Combo_advfilterentities_Caption ;
      private string Combo_advfilterentities_Cls ;
      private string Combo_advfilterentities_Multiplevaluestype ;
      private string Combo_advfilterentities_Internalname ;
      private string bttBtnbtnadvsearch_Internalname ;
      private string bttBtnbtnadvsearch_Jsonclick ;
      private string Btnbasicsearch_Tooltiptext ;
      private string Btnbasicsearch_Beforeiconclass ;
      private string Btnbasicsearch_Caption ;
      private string Btnbasicsearch_Class ;
      private string Btnbasicsearch_Internalname ;
      private string divFsresultcategoriescell_Internalname ;
      private string sStyleString ;
      private string subFsresultcategories_Internalname ;
      private string divTxtnoresultscell_Internalname ;
      private string lblTxtnoresults_Internalname ;
      private string lblTxtnoresults_Caption ;
      private string lblTxtnoresults_Jsonclick ;
      private string subFsresults_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXCCtl ;
      private string edtavDisplaytype1_title_Internalname ;
      private string edtavDisplaytype2_title_Internalname ;
      private string edtavDisplaytype3_title_Internalname ;
      private string edtavDisplaytype3_subtitle_Internalname ;
      private string edtavDisplaytype4_image_Internalname ;
      private string edtavDisplaytype4_title_Internalname ;
      private string edtavDisplaytype5_title_Internalname ;
      private string edtavDisplaytype5_subtitle_Internalname ;
      private string GXDecQS ;
      private string sGXsfl_53_fel_idx="0001" ;
      private string lblTxtshowingonly_Caption ;
      private string Combo_advfilterentities_Selectedvalue_set ;
      private string divDisplaytype1_cell_Internalname ;
      private string divDisplaytype2_cell_Internalname ;
      private string divDisplaytype3_cell_Internalname ;
      private string divDisplaytype4_cell_Internalname ;
      private string divDisplaytype5_cell_Internalname ;
      private string lblDisplaytype2_icon_Caption ;
      private string lblDisplaytype5_icon_Caption ;
      private string lblDisplaytype2_icon_Internalname ;
      private string lblDisplaytype5_icon_Internalname ;
      private string sGXsfl_61_fel_idx="0001" ;
      private string subFsresults_Class ;
      private string subFsresults_Linesclass ;
      private string divUnnamedtablefsfsresults_Internalname ;
      private string tblUnnamedtablecontentfsfsresults_Internalname ;
      private string ROClassString ;
      private string edtavUrl_Jsonclick ;
      private string divTablefscard_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string edtavDisplaytype1_title_Jsonclick ;
      private string tblUnnamedtable3_Internalname ;
      private string lblDisplaytype2_icon_Jsonclick ;
      private string edtavDisplaytype2_title_Jsonclick ;
      private string divUnnamedtable4_Internalname ;
      private string edtavDisplaytype3_title_Jsonclick ;
      private string edtavDisplaytype3_subtitle_Jsonclick ;
      private string divUnnamedtable5_Internalname ;
      private string edtavDisplaytype4_image_gximage ;
      private string sImgUrl ;
      private string edtavDisplaytype4_title_Jsonclick ;
      private string tblUnnamedtable6_Internalname ;
      private string lblDisplaytype5_icon_Jsonclick ;
      private string divUnnamedtable7_Internalname ;
      private string edtavDisplaytype5_title_Jsonclick ;
      private string edtavDisplaytype5_subtitle_Jsonclick ;
      private string edtavWwp_searchresults__categoryname_Internalname ;
      private string lblTxtshowingonly_Internalname ;
      private string subFsresultcategories_Class ;
      private string subFsresultcategories_Linesclass ;
      private string divUnnamedtablefsfsresultcategories_Internalname ;
      private string edtavWwp_searchresults__categoryname_Jsonclick ;
      private string lblTxtshowingonly_Jsonclick ;
      private string subFsresultcategories_Header ;
      private string subFsresults_Header ;
      private bool AV12DefaultIsAdvancedSearch ;
      private bool wcpOAV12DefaultIsAdvancedSearch ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_61_Refreshing=false ;
      private bool AV23IsAdvancedSearch ;
      private bool wbLoad ;
      private bool Dvpanel_unnamedtable1_Autowidth ;
      private bool Dvpanel_unnamedtable1_Autoheight ;
      private bool Dvpanel_unnamedtable1_Collapsible ;
      private bool Dvpanel_unnamedtable1_Collapsed ;
      private bool Dvpanel_unnamedtable1_Showcollapseicon ;
      private bool Dvpanel_unnamedtable1_Autoscroll ;
      private bool Combo_advfilterentities_Allowmultipleselection ;
      private bool Combo_advfilterentities_Includeonlyselectedoption ;
      private bool Combo_advfilterentities_Emptyitem ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_53_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV53 ;
      private bool AV17DisplayType4_Image_IsBlob ;
      private string AV10DeafultSearchText ;
      private string AV11DefaultAdvFilterEntitiesJson ;
      private string wcpOAV10DeafultSearchText ;
      private string wcpOAV11DefaultAdvFilterEntitiesJson ;
      private string AV25SearchText ;
      private string AV8AdvFilterText ;
      private string AV26Url ;
      private string AV13DisplayType1_Title ;
      private string AV14DisplayType2_Title ;
      private string AV16DisplayType3_Title ;
      private string AV15DisplayType3_Subtitle ;
      private string AV39Displaytype4_image_GXI ;
      private string AV18DisplayType4_Title ;
      private string AV20DisplayType5_Title ;
      private string AV19DisplayType5_Subtitle ;
      private string AV21EntityDescription ;
      private string AV17DisplayType4_Image ;
      private GXWebGrid FsresultcategoriesContainer ;
      private GXWebGrid FsresultsContainer ;
      private GXWebRow FsresultcategoriesRow ;
      private GXWebRow FsresultsRow ;
      private GXWebColumn FsresultcategoriesColumn ;
      private GXWebColumn FsresultsColumn ;
      private GXUserControl ucDvpanel_unnamedtable1 ;
      private GXUserControl ucBtnadvancedsearch ;
      private GXUserControl ucCombo_advfilterentities ;
      private GXUserControl ucBtnbasicsearch ;
      private IGxSession AV32WebSession ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV28WWP_SearchResults ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV9DDO_TitleSettingsIcons ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> AV6AdvFilterEntities_Data ;
      private GxSimpleCollection<string> AV5AdvFilterEntities ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 ;
      private GxSimpleCollection<string> AV22EntityDescriptions ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV27WWP_SearchResultAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item AV7AdvFilterEntities_DataItem ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
