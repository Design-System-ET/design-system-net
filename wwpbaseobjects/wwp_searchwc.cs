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
   public class wwp_searchwc : GXWebComponent
   {
      public wwp_searchwc( )
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

      public wwp_searchwc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_SearchText )
      {
         this.AV22SearchText = aP0_SearchText;
         ExecuteImpl();
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
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( StringUtil.Len( (string)(sPrefix)) == 0 )
         {
            if ( nGotPars == 0 )
            {
               entryPointCalled = false;
               gxfirstwebparm = GetFirstPar( "SearchText");
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
                  AV22SearchText = GetPar( "SearchText");
                  AssignAttri(sPrefix, false, "AV22SearchText", AV22SearchText);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(string)AV22SearchText});
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
                  gxfirstwebparm = GetFirstPar( "SearchText");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "SearchText");
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrFsresults_newrow_invoke( )
      {
         nRC_GXsfl_29 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_29"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_29_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_29_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_29_idx = GetPar( "sGXsfl_29_idx");
         sPrefix = GetPar( "sPrefix");
         edtavUrl_Visible = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp(sPrefix, false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), !bGXsfl_29_Refreshing);
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
         AssignProp(sPrefix, false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), !bGXsfl_29_Refreshing);
         ajax_req_read_hidden_sdt(GetNextPar( ), AV26WWP_SearchResults);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrFsresults_refresh( AV26WWP_SearchResults, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrFsresults_refresh_invoke */
      }

      protected void gxnrFsresultcategories_newrow_invoke( )
      {
         nRC_GXsfl_21 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_21"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_21_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_21_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_21_idx = GetPar( "sGXsfl_21_idx");
         sPrefix = GetPar( "sPrefix");
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
         ajax_req_read_hidden_sdt(GetNextPar( ), AV26WWP_SearchResults);
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrFsresultcategories_refresh( AV26WWP_SearchResults, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrFsresultcategories_refresh_invoke */
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
            PA0Z2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavWwp_searchresults__categoryname_Enabled = 0;
               AssignProp(sPrefix, false, edtavWwp_searchresults__categoryname_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWwp_searchresults__categoryname_Enabled), 5, 0), !bGXsfl_21_Refreshing);
               edtavDisplaytype1_title_Enabled = 0;
               AssignProp(sPrefix, false, edtavDisplaytype1_title_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDisplaytype1_title_Enabled), 5, 0), !bGXsfl_29_Refreshing);
               edtavDisplaytype2_title_Enabled = 0;
               AssignProp(sPrefix, false, edtavDisplaytype2_title_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDisplaytype2_title_Enabled), 5, 0), !bGXsfl_29_Refreshing);
               edtavDisplaytype3_title_Enabled = 0;
               AssignProp(sPrefix, false, edtavDisplaytype3_title_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDisplaytype3_title_Enabled), 5, 0), !bGXsfl_29_Refreshing);
               edtavDisplaytype3_subtitle_Enabled = 0;
               AssignProp(sPrefix, false, edtavDisplaytype3_subtitle_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDisplaytype3_subtitle_Enabled), 5, 0), !bGXsfl_29_Refreshing);
               edtavDisplaytype4_title_Enabled = 0;
               AssignProp(sPrefix, false, edtavDisplaytype4_title_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDisplaytype4_title_Enabled), 5, 0), !bGXsfl_29_Refreshing);
               edtavDisplaytype5_title_Enabled = 0;
               AssignProp(sPrefix, false, edtavDisplaytype5_title_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDisplaytype5_title_Enabled), 5, 0), !bGXsfl_29_Refreshing);
               edtavDisplaytype5_subtitle_Enabled = 0;
               AssignProp(sPrefix, false, edtavDisplaytype5_subtitle_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavDisplaytype5_subtitle_Enabled), 5, 0), !bGXsfl_29_Refreshing);
               WS0Z2( ) ;
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
            context.SendWebValue( context.GetMessage( "WWP_Search WC", "")) ;
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
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "wwpbaseobjects.wwp_searchwc.aspx"+UrlEncode(StringUtil.RTrim(AV22SearchText));
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wwpbaseobjects.wwp_searchwc.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWWP_SEARCHRESULTS", AV26WWP_SearchResults);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWWP_SEARCHRESULTS", AV26WWP_SearchResults);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWP_SEARCHRESULTS", GetSecureSignedToken( sPrefix, AV26WWP_SearchResults, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Wwp_searchresults", AV26WWP_SearchResults);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Wwp_searchresults", AV26WWP_SearchResults);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_Wwp_searchresults", GetSecureSignedToken( sPrefix, AV26WWP_SearchResults, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_21", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_21), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vDDO_TITLESETTINGSICONS", AV9DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vDDO_TITLESETTINGSICONS", AV9DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vADVFILTERENTITIES_DATA", AV6AdvFilterEntities_Data);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vADVFILTERENTITIES_DATA", AV6AdvFilterEntities_Data);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV22SearchText", wcpOAV22SearchText);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWWP_SEARCHRESULTS", AV26WWP_SearchResults);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWWP_SEARCHRESULTS", AV26WWP_SearchResults);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWP_SEARCHRESULTS", GetSecureSignedToken( sPrefix, AV26WWP_SearchResults, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSEARCHTEXT", AV22SearchText);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vADVFILTERENTITIES", AV5AdvFilterEntities);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vADVFILTERENTITIES", AV5AdvFilterEntities);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"TABLEADVANCEDSEARCHCELL_Class", StringUtil.RTrim( divTableadvancedsearchcell_Class));
         GxWebStd.gx_hidden_field( context, sPrefix+"TABLESIMPLESEARCHCELL_Class", StringUtil.RTrim( divTablesimplesearchcell_Class));
         GxWebStd.gx_hidden_field( context, sPrefix+"COMBO_ADVFILTERENTITIES_Selectedvalue_get", StringUtil.RTrim( Combo_advfilterentities_Selectedvalue_get));
      }

      protected void RenderHtmlCloseForm0Z2( )
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
         return "WWPBaseObjects.WWP_SearchWC" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "WWP_Search WC", "") ;
      }

      protected void WB0Z0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wwpbaseobjects.wwp_searchwc.aspx");
               context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
               context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
               context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
               context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CloseSearchIconCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblClosesearch_Internalname, context.GetMessage( "<i class=\"fa fa-times\"></i>", ""), "", "", lblClosesearch_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOCLOSESEARCH\\'."+"'", "", "TextBlock", 5, "", 1, 1, 0, 1, "HLP_WWPBaseObjects/WWP_SearchWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divShowall_cell_Internalname, 1, 0, "px", 0, "px", divShowall_cell_Class, "start", "top", "", "", "div");
            /* User Defined Control */
            ucBtnshowall.SetProperty("TooltipText", Btnshowall_Tooltiptext);
            ucBtnshowall.SetProperty("BeforeIconClass", Btnshowall_Beforeiconclass);
            ucBtnshowall.SetProperty("Caption", Btnshowall_Caption);
            ucBtnshowall.SetProperty("Class", Btnshowall_Class);
            ucBtnshowall.Render(context, "wwp_iconbutton", Btnshowall_Internalname, sPrefix+"BTNSHOWALLContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTxtnoresultscell_Internalname, 1, 0, "px", 0, "px", divTxtnoresultscell_Class, "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTxtnoresults_Internalname, lblTxtnoresults_Caption, "", "", lblTxtnoresults_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "TextBlockShowingOnly", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WWP_SearchWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesimplesearchcell_Internalname, 1, 0, "px", 0, "px", divTablesimplesearchcell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesimplesearch_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divFsresultcategories_cell_Internalname, 1, 0, "px", 0, "px", divFsresultcategories_cell_Class, "start", "top", "", "", "div");
            /*  Grid Control  */
            FsresultcategoriesContainer.SetIsFreestyle(true);
            FsresultcategoriesContainer.SetWrapped(nGXWrapped);
            StartGridControl21( ) ;
         }
         if ( wbEnd == 21 )
         {
            wbEnd = 0;
            nRC_GXsfl_21 = (int)(nGXsfl_21_idx-1);
            if ( FsresultcategoriesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV30GXV1 = nGXsfl_21_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"FsresultcategoriesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Fsresultcategories", FsresultcategoriesContainer, subFsresultcategories_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"FsresultcategoriesContainerData", FsresultcategoriesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"FsresultcategoriesContainerData"+"V", FsresultcategoriesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"FsresultcategoriesContainerData"+"V"+"\" value='"+FsresultcategoriesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableadvancedsearchcell_Internalname, 1, 0, "px", 0, "px", divTableadvancedsearchcell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableadvancedsearch_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavAdvfiltertext_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavAdvfiltertext_Internalname, context.GetMessage( "WWP_Search", ""), "col-sm-3 AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 94,'" + sPrefix + "',false,'" + sGXsfl_21_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavAdvfiltertext_Internalname, AV8AdvFilterText, StringUtil.RTrim( context.localUtil.Format( AV8AdvFilterText, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,94);\"", "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavAdvfiltertext_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtavAdvfiltertext_Enabled, 0, "text", "", 40, "chr", 1, "row", 40, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_WWPBaseObjects/WWP_SearchWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCell ExtendedComboCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablesplittedadvfilterentities_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-3 MergeLabelCell", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblockcombo_advfilterentities_Internalname, context.GetMessage( "WWP_Search_SearchInDescription", ""), "", "", lblTextblockcombo_advfilterentities_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WWP_SearchWC.htm");
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
            ucCombo_advfilterentities.Render(context, "dvelop.gxbootstrap.ddoextendedcombo", Combo_advfilterentities_Internalname, sPrefix+"COMBO_ADVFILTERENTITIESContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 105,'" + sPrefix + "',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnbtnadvsearch_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(21), 2, 0)+","+"null"+");", context.GetMessage( "GX_BtnSearch", ""), bttBtnbtnadvsearch_Jsonclick, 5, context.GetMessage( "GX_BtnSearch", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"E\\'DOBTNADVSEARCH\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_WWPBaseObjects/WWP_SearchWC.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 BtnAdvancedSearchCell", "start", "top", "", "", "div");
            /* User Defined Control */
            ucBtnadvancedsearch.SetProperty("TooltipText", Btnadvancedsearch_Tooltiptext);
            ucBtnadvancedsearch.SetProperty("BeforeIconClass", Btnadvancedsearch_Beforeiconclass);
            ucBtnadvancedsearch.SetProperty("Caption", Btnadvancedsearch_Caption);
            ucBtnadvancedsearch.SetProperty("Class", Btnadvancedsearch_Class);
            ucBtnadvancedsearch.Render(context, "wwp_iconbutton", Btnadvancedsearch_Internalname, sPrefix+"BTNADVANCEDSEARCHContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 21 )
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
                  AV30GXV1 = nGXsfl_21_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"FsresultcategoriesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Fsresultcategories", FsresultcategoriesContainer, subFsresultcategories_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"FsresultcategoriesContainerData", FsresultcategoriesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"FsresultcategoriesContainerData"+"V", FsresultcategoriesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"FsresultcategoriesContainerData"+"V"+"\" value='"+FsresultcategoriesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         if ( wbEnd == 29 )
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
                  AV32GXV3 = nGXsfl_29_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"FsresultsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Fsresults", FsresultsContainer, subFsresults_Internalname);
                  if ( ! isAjaxCallMode( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"FsresultsContainerData", FsresultsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"FsresultsContainerData"+"V", FsresultsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"FsresultsContainerData"+"V"+"\" value='"+FsresultsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START0Z2( )
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
            Form.Meta.addItem("description", context.GetMessage( "WWP_Search WC", ""), 0) ;
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
               STRUP0Z0( ) ;
            }
         }
      }

      protected void WS0Z2( )
      {
         START0Z2( ) ;
         EVT0Z2( ) ;
      }

      protected void EVT0Z2( )
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
                                 STRUP0Z0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "'DOCLOSESEARCH'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0Z0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoCloseSearch' */
                                    E110Z2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOSHOWALL'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0Z0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoShowAll' */
                                    E120Z2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOBTNADVSEARCH'") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0Z0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: 'DoBtnAdvSearch' */
                                    E130Z2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "UNNAMEDTABLEFSFSRESULTS.CLICK") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0Z0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Unnamedtablefsfsresults.Click */
                                    E140Z2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0Z0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavAdvfiltertext_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 23), "FSRESULTCATEGORIES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "FSRESULTS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 29), "UNNAMEDTABLEFSFSRESULTS.CLICK") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0Z0( ) ;
                              }
                              nGXsfl_21_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_21_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_21_idx), 4, 0), 4, "0");
                              SubsflControlProps_212( ) ;
                              AV30GXV1 = nGXsfl_21_idx;
                              if ( ( AV26WWP_SearchResults.Count >= AV30GXV1 ) && ( AV30GXV1 > 0 ) )
                              {
                                 AV26WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1));
                              }
                              GXCCtl = "subFsresults_Recordcount_" + sGXsfl_21_idx;
                              subFsresults_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+GXCCtl), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
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
                                          GX_FocusControl = edtavAdvfiltertext_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Start */
                                          E150Z2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "FSRESULTCATEGORIES.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavAdvfiltertext_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                          /* Execute user event: Fsresultcategories.Load */
                                          E160Z2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
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
                                       STRUP0Z0( ) ;
                                    }
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          GX_FocusControl = edtavAdvfiltertext_Internalname;
                                          AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                       }
                                    }
                                 }
                              }
                              else
                              {
                                 sEvtType = StringUtil.Right( sEvt, 4);
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                                 if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "FSRESULTS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 29), "UNNAMEDTABLEFSFSRESULTS.CLICK") == 0 ) )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP0Z0( ) ;
                                    }
                                    nGXsfl_29_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                                    sGXsfl_29_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_29_idx), 4, 0), 4, "0") + sGXsfl_21_idx;
                                    SubsflControlProps_293( ) ;
                                    AV24Url = cgiGet( edtavUrl_Internalname);
                                    AssignAttri(sPrefix, false, edtavUrl_Internalname, AV24Url);
                                    AV10DisplayType1_Title = cgiGet( edtavDisplaytype1_title_Internalname);
                                    AssignAttri(sPrefix, false, edtavDisplaytype1_title_Internalname, AV10DisplayType1_Title);
                                    AV11DisplayType2_Title = cgiGet( edtavDisplaytype2_title_Internalname);
                                    AssignAttri(sPrefix, false, edtavDisplaytype2_title_Internalname, AV11DisplayType2_Title);
                                    AV13DisplayType3_Title = cgiGet( edtavDisplaytype3_title_Internalname);
                                    AssignAttri(sPrefix, false, edtavDisplaytype3_title_Internalname, AV13DisplayType3_Title);
                                    AV12DisplayType3_Subtitle = cgiGet( edtavDisplaytype3_subtitle_Internalname);
                                    AssignAttri(sPrefix, false, edtavDisplaytype3_subtitle_Internalname, AV12DisplayType3_Subtitle);
                                    AV14DisplayType4_Image = cgiGet( edtavDisplaytype4_image_Internalname);
                                    AssignProp(sPrefix, false, edtavDisplaytype4_image_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV14DisplayType4_Image)) ? AV33Displaytype4_image_GXI : context.convertURL( context.PathToRelativeUrl( AV14DisplayType4_Image))), !bGXsfl_29_Refreshing);
                                    AssignProp(sPrefix, false, edtavDisplaytype4_image_Internalname, "SrcSet", context.GetImageSrcSet( AV14DisplayType4_Image), true);
                                    AV15DisplayType4_Title = cgiGet( edtavDisplaytype4_title_Internalname);
                                    AssignAttri(sPrefix, false, edtavDisplaytype4_title_Internalname, AV15DisplayType4_Title);
                                    AV17DisplayType5_Title = cgiGet( edtavDisplaytype5_title_Internalname);
                                    AssignAttri(sPrefix, false, edtavDisplaytype5_title_Internalname, AV17DisplayType5_Title);
                                    AV16DisplayType5_Subtitle = cgiGet( edtavDisplaytype5_subtitle_Internalname);
                                    AssignAttri(sPrefix, false, edtavDisplaytype5_subtitle_Internalname, AV16DisplayType5_Subtitle);
                                    sEvtType = StringUtil.Right( sEvt, 1);
                                    if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                                    {
                                       sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                       if ( StringUtil.StrCmp(sEvt, "FSRESULTS.LOAD") == 0 )
                                       {
                                          if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                          {
                                             context.wbHandled = 1;
                                             if ( ! wbErr )
                                             {
                                                dynload_actions( ) ;
                                                GX_FocusControl = edtavAdvfiltertext_Internalname;
                                                AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                                /* Execute user event: Fsresults.Load */
                                                E170Z3 ();
                                             }
                                          }
                                       }
                                       else if ( StringUtil.StrCmp(sEvt, "UNNAMEDTABLEFSFSRESULTS.CLICK") == 0 )
                                       {
                                          if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                          {
                                             context.wbHandled = 1;
                                             if ( ! wbErr )
                                             {
                                                dynload_actions( ) ;
                                                GX_FocusControl = edtavAdvfiltertext_Internalname;
                                                AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                                /* Execute user event: Unnamedtablefsfsresults.Click */
                                                E140Z2 ();
                                             }
                                          }
                                       }
                                       else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                       {
                                          if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                          {
                                             STRUP0Z0( ) ;
                                          }
                                          if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                          {
                                             context.wbHandled = 1;
                                             if ( ! wbErr )
                                             {
                                                dynload_actions( ) ;
                                                GX_FocusControl = edtavAdvfiltertext_Internalname;
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
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE0Z2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm0Z2( ) ;
            }
         }
      }

      protected void PA0Z2( )
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
                  if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "wwpbaseobjects.wwp_searchwc.aspx")), "wwpbaseobjects.wwp_searchwc.aspx") == 0 ) )
                  {
                     SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "wwpbaseobjects.wwp_searchwc.aspx")))) ;
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
                     gxfirstwebparm = GetFirstPar( "SearchText");
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
               GX_FocusControl = edtavAdvfiltertext_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
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
         SubsflControlProps_212( ) ;
         while ( nGXsfl_21_idx <= nRC_GXsfl_21 )
         {
            sendrow_212( ) ;
            nGXsfl_21_idx = ((subFsresultcategories_Islastpage==1)&&(nGXsfl_21_idx+1>subFsresultcategories_fnc_Recordsperpage( )) ? 1 : nGXsfl_21_idx+1);
            sGXsfl_21_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_21_idx), 4, 0), 4, "0");
            SubsflControlProps_212( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( FsresultcategoriesContainer)) ;
         /* End function gxnrFsresultcategories_newrow */
      }

      protected void gxnrFsresults_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_293( ) ;
         while ( nGXsfl_29_idx <= nRC_GXsfl_29 )
         {
            sendrow_293( ) ;
            nGXsfl_29_idx = ((subFsresults_Islastpage==1)&&(nGXsfl_29_idx+1>subFsresults_fnc_Recordsperpage( )) ? 1 : nGXsfl_29_idx+1);
            sGXsfl_29_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_29_idx), 4, 0), 4, "0") + sGXsfl_21_idx;
            SubsflControlProps_293( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( FsresultsContainer)) ;
         /* End function gxnrFsresults_newrow */
      }

      protected void gxgrFsresults_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV26WWP_SearchResults ,
                                            string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         FSRESULTS_nCurrentRecord = 0;
         RF0Z3( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrFsresults_refresh */
      }

      protected void gxgrFsresultcategories_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV26WWP_SearchResults ,
                                                     string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         FSRESULTCATEGORIES_nCurrentRecord = 0;
         RF0Z2( ) ;
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
         RF0Z2( ) ;
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

      protected void RF0Z2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            FsresultcategoriesContainer.ClearRows();
         }
         wbStart = 21;
         nGXsfl_21_idx = 1;
         sGXsfl_21_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_21_idx), 4, 0), 4, "0");
         SubsflControlProps_212( ) ;
         bGXsfl_21_Refreshing = true;
         FsresultcategoriesContainer.AddObjectProperty("GridName", "Fsresultcategories");
         FsresultcategoriesContainer.AddObjectProperty("CmpContext", sPrefix);
         FsresultcategoriesContainer.AddObjectProperty("InMasterPage", "false");
         FsresultcategoriesContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGridMPSearch"));
         FsresultcategoriesContainer.AddObjectProperty("Class", "FreeStyleGridMPSearch");
         FsresultcategoriesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         FsresultcategoriesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         FsresultcategoriesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Backcolorstyle), 1, 0, ".", "")));
         FsresultcategoriesContainer.PageSize = subFsresultcategories_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_212( ) ;
            /* Execute user event: Fsresultcategories.Load */
            E160Z2 ();
            wbEnd = 21;
            WB0Z0( ) ;
         }
         bGXsfl_21_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes0Z2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWWP_SEARCHRESULTS", AV26WWP_SearchResults);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWWP_SEARCHRESULTS", AV26WWP_SearchResults);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWWP_SEARCHRESULTS", GetSecureSignedToken( sPrefix, AV26WWP_SearchResults, context));
      }

      protected void RF0Z3( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            FsresultsContainer.ClearRows();
         }
         wbStart = 29;
         nGXsfl_29_idx = 1;
         sGXsfl_29_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_29_idx), 4, 0), 4, "0") + sGXsfl_21_idx;
         SubsflControlProps_293( ) ;
         bGXsfl_29_Refreshing = true;
         FsresultsContainer.AddObjectProperty("GridName", "Fsresults");
         FsresultsContainer.AddObjectProperty("CmpContext", sPrefix);
         FsresultsContainer.AddObjectProperty("InMasterPage", "false");
         FsresultsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         FsresultsContainer.AddObjectProperty("Class", "FreeStyleGrid");
         FsresultsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         FsresultsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         FsresultsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Backcolorstyle), 1, 0, ".", "")));
         FsresultsContainer.PageSize = subFsresults_fnc_Recordsperpage( );
         GXCCtl = "FSRESULTS_nFirstRecordOnPage_" + sGXsfl_21_idx;
         if ( subFsresults_Islastpage != 0 )
         {
            FSRESULTS_nFirstRecordOnPage = (long)(subFsresults_fnc_Recordcount( )-subFsresults_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, sPrefix+GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(FSRESULTS_nFirstRecordOnPage), 15, 0, ".", "")));
            FsresultsContainer.AddObjectProperty("FSRESULTS_nFirstRecordOnPage", FSRESULTS_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_293( ) ;
            /* Execute user event: Fsresults.Load */
            E170Z3 ();
            wbEnd = 29;
            WB0Z0( ) ;
         }
         bGXsfl_29_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes0Z3( )
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

      protected void STRUP0Z0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E150Z2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Wwp_searchresults"), AV26WWP_SearchResults);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vDDO_TITLESETTINGSICONS"), AV9DDO_TitleSettingsIcons);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vADVFILTERENTITIES_DATA"), AV6AdvFilterEntities_Data);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vWWP_SEARCHRESULTS"), AV26WWP_SearchResults);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vADVFILTERENTITIES"), AV5AdvFilterEntities);
            /* Read saved values. */
            nRC_GXsfl_21 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_21"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            wcpOAV22SearchText = cgiGet( sPrefix+"wcpOAV22SearchText");
            nRC_GXsfl_21 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_21"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nGXsfl_21_fel_idx = 0;
            while ( nGXsfl_21_fel_idx < nRC_GXsfl_21 )
            {
               nGXsfl_21_fel_idx = ((subFsresultcategories_Islastpage==1)&&(nGXsfl_21_fel_idx+1>subFsresultcategories_fnc_Recordsperpage( )) ? 1 : nGXsfl_21_fel_idx+1);
               sGXsfl_21_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_21_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_212( ) ;
               AV30GXV1 = nGXsfl_21_fel_idx;
               if ( ( AV26WWP_SearchResults.Count >= AV30GXV1 ) && ( AV30GXV1 > 0 ) )
               {
                  AV26WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1));
               }
               GXCCtl = "subFsresults_Recordcount_" + sGXsfl_21_fel_idx;
               subFsresults_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+GXCCtl), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            }
            if ( nGXsfl_21_fel_idx == 0 )
            {
               nGXsfl_21_idx = 1;
               sGXsfl_21_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_21_idx), 4, 0), 4, "0");
               SubsflControlProps_212( ) ;
            }
            nGXsfl_21_fel_idx = 1;
            /* Read variables values. */
            AV8AdvFilterText = cgiGet( edtavAdvfiltertext_Internalname);
            AssignAttri(sPrefix, false, "AV8AdvFilterText", AV8AdvFilterText);
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
         E150Z2 ();
         if (returnInSub) return;
      }

      protected void E150Z2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV20MinimumCharsToSearch = 2;
         AV23ShowingRecents = false;
         if ( StringUtil.Len( StringUtil.Trim( AV22SearchText)) < AV20MinimumCharsToSearch )
         {
            GXt_char1 = AV21RecentSearchResultsJson;
            new DesignSystem.Programs.wwpbaseobjects.loaduserkeyvalue(context ).execute(  "WWPRecentSearch", out  GXt_char1) ;
            AV21RecentSearchResultsJson = GXt_char1;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV21RecentSearchResultsJson)) )
            {
               AV26WWP_SearchResults.FromJSonString(AV21RecentSearchResultsJson, null);
               gx_BV21 = true;
               AV23ShowingRecents = (bool)(((AV26WWP_SearchResults.Count>0)));
            }
         }
         else
         {
            GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2 = AV26WWP_SearchResults;
            new DesignSystem.Programs.wwpbaseobjects.wwp_getsearchwcdata(context ).execute(  AV22SearchText,  4,  3, ref  AV5AdvFilterEntities, out  GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2) ;
            AV26WWP_SearchResults = GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2;
            gx_BV21 = true;
         }
         if ( AV26WWP_SearchResults.Count == 0 )
         {
            divShowall_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divShowall_cell_Internalname, "Class", divShowall_cell_Class, true);
            divFsresultcategories_cell_Class = "Invisible";
            AssignProp(sPrefix, false, divFsresultcategories_cell_Internalname, "Class", divFsresultcategories_cell_Class, true);
            if ( StringUtil.Len( StringUtil.Trim( AV22SearchText)) < AV20MinimumCharsToSearch )
            {
               lblTxtnoresults_Caption = context.GetMessage( "WWP_Search_TypeToSearch", "");
               AssignProp(sPrefix, false, lblTxtnoresults_Internalname, "Caption", lblTxtnoresults_Caption, true);
            }
            else
            {
               lblTxtnoresults_Caption = StringUtil.Format( context.GetMessage( "WWP_Search_NoResults", ""), AV22SearchText, "", "", "", "", "", "", "", "");
               AssignProp(sPrefix, false, lblTxtnoresults_Internalname, "Caption", lblTxtnoresults_Caption, true);
            }
         }
         else
         {
            if ( AV23ShowingRecents )
            {
               divShowall_cell_Class = "Invisible";
               AssignProp(sPrefix, false, divShowall_cell_Internalname, "Class", divShowall_cell_Class, true);
               lblTxtnoresults_Caption = context.GetMessage( "WWP_Search_ShowingRecentSearchResults", "");
               AssignProp(sPrefix, false, lblTxtnoresults_Internalname, "Caption", lblTxtnoresults_Caption, true);
            }
            else
            {
               divTxtnoresultscell_Class = "Invisible";
               AssignProp(sPrefix, false, divTxtnoresultscell_Internalname, "Class", divTxtnoresultscell_Class, true);
            }
         }
         Btnshowall_Caption = StringUtil.Format( context.GetMessage( "WWP_Search_ShowAll", ""), AV22SearchText, "", "", "", "", "", "", "", "");
         ucBtnshowall.SendProperty(context, sPrefix, false, Btnshowall_Internalname, "Caption", Btnshowall_Caption);
         divTableadvancedsearchcell_Class = "Invisible";
         AssignProp(sPrefix, false, divTableadvancedsearchcell_Internalname, "Class", divTableadvancedsearchcell_Class, true);
         AV8AdvFilterText = AV22SearchText;
         AssignAttri(sPrefix, false, "AV8AdvFilterText", AV8AdvFilterText);
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons3 = AV9DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons3) ;
         AV9DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons3;
         /* Execute user subroutine: 'LOADCOMBOADVFILTERENTITIES' */
         S112 ();
         if (returnInSub) return;
         edtavUrl_Visible = 0;
         AssignProp(sPrefix, false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), !bGXsfl_29_Refreshing);
      }

      private void E160Z2( )
      {
         /* Fsresultcategories_Load Routine */
         returnInSub = false;
         AV30GXV1 = 1;
         while ( AV30GXV1 <= AV26WWP_SearchResults.Count )
         {
            AV26WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1));
            divShowingonlycell_Visible = ((!((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Showingallresults) ? 1 : 0);
            AssignProp(sPrefix, false, divShowingonlycell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divShowingonlycell_Visible), 5, 0), !bGXsfl_21_Refreshing);
            lblTxtshowingonly_Caption = StringUtil.Format( context.GetMessage( "WWP_Search_ShowingTopResults", ""), StringUtil.LTrimStr( (decimal)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.Count), 9, 0), "", "", "", "", "", "", "", "");
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 21;
            }
            sendrow_212( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_21_Refreshing )
            {
               DoAjaxLoad(21, FsresultcategoriesRow);
            }
            AV30GXV1 = (int)(AV30GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E110Z2( )
      {
         /* 'DoCloseSearch' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod(sPrefix, false, "WWPActions", "Popover_Close", new Object[] {(string)divTablemain_Internalname,(bool)false}, false);
      }

      protected void E120Z2( )
      {
         /* 'DoShowAll' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod(sPrefix, false, "WWPActions", "Popover_Close", new Object[] {(string)divTablemain_Internalname,(bool)false}, false);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "wwpbaseobjects.wwp_search.aspx"+UrlEncode(StringUtil.RTrim(AV22SearchText)) + "," + UrlEncode(StringUtil.BoolToStr(false)) + "," + UrlEncode(StringUtil.RTrim(""));
         CallWebObject(formatLink("wwpbaseobjects.wwp_search.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
      }

      protected void E130Z2( )
      {
         /* 'DoBtnAdvSearch' Routine */
         returnInSub = false;
         this.executeExternalObjectMethod(sPrefix, false, "WWPActions", "Popover_Close", new Object[] {(string)divTablemain_Internalname,(bool)false}, false);
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "wwpbaseobjects.wwp_search.aspx"+UrlEncode(StringUtil.RTrim(AV8AdvFilterText)) + "," + UrlEncode(StringUtil.BoolToStr(true)) + "," + UrlEncode(StringUtil.RTrim(AV5AdvFilterEntities.ToJSonString(false)));
         CallWebObject(formatLink("wwpbaseobjects.wwp_search.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
      }

      protected void S112( )
      {
         /* 'LOADCOMBOADVFILTERENTITIES' Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getsearchwcdata(context ).execute(  "",  999,  0, ref  AV19EntityDescriptions, out  AV25WWP_SearchResultAux) ;
         AV6AdvFilterEntities_Data = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Item", "");
         AV34GXV4 = 1;
         while ( AV34GXV4 <= AV19EntityDescriptions.Count )
         {
            AV18EntityDescription = ((string)AV19EntityDescriptions.Item(AV34GXV4));
            AV7AdvFilterEntities_DataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item(context);
            AV7AdvFilterEntities_DataItem.gxTpr_Id = AV18EntityDescription;
            AV7AdvFilterEntities_DataItem.gxTpr_Title = AV18EntityDescription;
            AV6AdvFilterEntities_Data.Add(AV7AdvFilterEntities_DataItem, 0);
            AV34GXV4 = (int)(AV34GXV4+1);
         }
         Combo_advfilterentities_Selectedvalue_set = AV5AdvFilterEntities.ToJSonString(false);
         ucCombo_advfilterentities.SendProperty(context, sPrefix, false, Combo_advfilterentities_Internalname, "SelectedValue_set", Combo_advfilterentities_Selectedvalue_set);
      }

      protected void E140Z2( )
      {
         AV30GXV1 = nGXsfl_21_idx;
         if ( ( AV30GXV1 > 0 ) && ( AV26WWP_SearchResults.Count >= AV30GXV1 ) )
         {
            AV26WWP_SearchResults.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1));
         }
         /* Unnamedtablefsfsresults_Click Routine */
         returnInSub = false;
         this.executeExternalObjectMethod(sPrefix, false, "WWPActions", "Popover_Close", new Object[] {(string)divTablemain_Internalname,(bool)false}, false);
         new DesignSystem.Programs.wwpbaseobjects.wwp_addrecentsearch(context ).execute(  AV26WWP_SearchResults,  AV24Url,  4,  3) ;
         CallWebObject(formatLink(AV24Url) );
         context.wjLocDisableFrm = 0;
      }

      private void E170Z3( )
      {
         AV30GXV1 = nGXsfl_21_idx;
         if ( AV26WWP_SearchResults.Count < AV30GXV1 )
         {
            return  ;
         }
         /* Fsresults_Load Routine */
         returnInSub = false;
         AV32GXV3 = 1;
         while ( AV32GXV3 <= ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1)).gxTpr_Result.Count )
         {
            ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1)).gxTpr_Result.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1)).gxTpr_Result.Item(AV32GXV3));
            divDisplaytype1_cell_Visible = 0;
            AssignProp(sPrefix, false, divDisplaytype1_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype1_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
            divDisplaytype2_cell_Visible = 0;
            AssignProp(sPrefix, false, divDisplaytype2_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype2_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
            divDisplaytype3_cell_Visible = 0;
            AssignProp(sPrefix, false, divDisplaytype3_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype3_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
            divDisplaytype4_cell_Visible = 0;
            AssignProp(sPrefix, false, divDisplaytype4_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype4_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
            divDisplaytype5_cell_Visible = 0;
            AssignProp(sPrefix, false, divDisplaytype5_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype5_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
            if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title") == 0 )
            {
               divDisplaytype1_cell_Visible = 1;
               AssignProp(sPrefix, false, divDisplaytype1_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype1_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
               AV10DisplayType1_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri(sPrefix, false, edtavDisplaytype1_title_Internalname, AV10DisplayType1_Title);
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title and icon image") == 0 )
            {
               divDisplaytype2_cell_Visible = 1;
               AssignProp(sPrefix, false, divDisplaytype2_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype2_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
               AV11DisplayType2_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri(sPrefix, false, edtavDisplaytype2_title_Internalname, AV11DisplayType2_Title);
               lblDisplaytype2_icon_Caption = StringUtil.Format( "<i class='SearchResultIcon %1'></i>", ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2, "", "", "", "", "", "", "", "");
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title and subtitle") == 0 )
            {
               divDisplaytype3_cell_Visible = 1;
               AssignProp(sPrefix, false, divDisplaytype3_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype3_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
               AV13DisplayType3_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri(sPrefix, false, edtavDisplaytype3_title_Internalname, AV13DisplayType3_Title);
               AV12DisplayType3_Subtitle = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2;
               AssignAttri(sPrefix, false, edtavDisplaytype3_subtitle_Internalname, AV12DisplayType3_Subtitle);
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title and image") == 0 )
            {
               divDisplaytype4_cell_Visible = 1;
               AssignProp(sPrefix, false, divDisplaytype4_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype4_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
               AV15DisplayType4_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri(sPrefix, false, edtavDisplaytype4_title_Internalname, AV15DisplayType4_Title);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2)) )
               {
                  AV14DisplayType4_Image = "";
                  AssignAttri(sPrefix, false, edtavDisplaytype4_image_Internalname, AV14DisplayType4_Image);
                  AV33Displaytype4_image_GXI = "";
               }
               else
               {
                  AV14DisplayType4_Image = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2;
                  AssignAttri(sPrefix, false, edtavDisplaytype4_image_Internalname, AV14DisplayType4_Image);
                  AV33Displaytype4_image_GXI = GXDbFile.PathToUrl( ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2, context);
               }
               edtavDisplaytype4_image_Visible = ((!String.IsNullOrEmpty(StringUtil.RTrim( ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2))) ? 1 : 0);
            }
            else if ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Displaytype, "Title, subtitle and icon image") == 0 )
            {
               divDisplaytype5_cell_Visible = 1;
               AssignProp(sPrefix, false, divDisplaytype5_cell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divDisplaytype5_cell_Visible), 5, 0), !bGXsfl_29_Refreshing);
               AV17DisplayType5_Title = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description;
               AssignAttri(sPrefix, false, edtavDisplaytype5_title_Internalname, AV17DisplayType5_Title);
               AV16DisplayType5_Subtitle = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description2;
               AssignAttri(sPrefix, false, edtavDisplaytype5_subtitle_Internalname, AV16DisplayType5_Subtitle);
               lblDisplaytype5_icon_Caption = StringUtil.Format( "<i class='SearchResultIconWS %1'></i>", ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Description3, "", "", "", "", "", "", "", "");
            }
            AV24Url = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)(AV26WWP_SearchResults.CurrentItem)).gxTpr_Result.CurrentItem)).gxTpr_Url;
            AssignAttri(sPrefix, false, edtavUrl_Internalname, AV24Url);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 29;
            }
            sendrow_293( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_29_Refreshing )
            {
               DoAjaxLoad(29, FsresultsRow);
            }
            AV32GXV3 = (int)(AV32GXV3+1);
         }
         /*  Sending Event outputs  */
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV22SearchText = (string)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV22SearchText", AV22SearchText);
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
         PA0Z2( ) ;
         WS0Z2( ) ;
         WE0Z2( ) ;
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
         sCtrlAV22SearchText = (string)((string)getParm(obj,0));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA0Z2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wwpbaseobjects\\wwp_searchwc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA0Z2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV22SearchText = (string)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV22SearchText", AV22SearchText);
         }
         wcpOAV22SearchText = cgiGet( sPrefix+"wcpOAV22SearchText");
         if ( ! GetJustCreated( ) && ( ( StringUtil.StrCmp(AV22SearchText, wcpOAV22SearchText) != 0 ) ) )
         {
            setjustcreated();
         }
         wcpOAV22SearchText = AV22SearchText;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV22SearchText = cgiGet( sPrefix+"AV22SearchText_CTRL");
         if ( StringUtil.Len( sCtrlAV22SearchText) > 0 )
         {
            AV22SearchText = cgiGet( sCtrlAV22SearchText);
            AssignAttri(sPrefix, false, "AV22SearchText", AV22SearchText);
         }
         else
         {
            AV22SearchText = cgiGet( sPrefix+"AV22SearchText_PARM");
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
         PA0Z2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS0Z2( ) ;
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
         WS0Z2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV22SearchText_PARM", AV22SearchText);
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV22SearchText)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV22SearchText_CTRL", StringUtil.RTrim( sCtrlAV22SearchText));
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
         WE0Z2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202412162336273", true, true);
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
         context.AddJavascriptSource("wwpbaseobjects/wwp_searchwc.js", "?202412162336275", false, true);
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("UserControls/WWP_IconButtonRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_293( )
      {
         edtavUrl_Internalname = sPrefix+"vURL_"+sGXsfl_29_idx;
         edtavDisplaytype1_title_Internalname = sPrefix+"vDISPLAYTYPE1_TITLE_"+sGXsfl_29_idx;
         lblDisplaytype2_icon_Internalname = sPrefix+"DISPLAYTYPE2_ICON_"+sGXsfl_29_idx;
         edtavDisplaytype2_title_Internalname = sPrefix+"vDISPLAYTYPE2_TITLE_"+sGXsfl_29_idx;
         edtavDisplaytype3_title_Internalname = sPrefix+"vDISPLAYTYPE3_TITLE_"+sGXsfl_29_idx;
         edtavDisplaytype3_subtitle_Internalname = sPrefix+"vDISPLAYTYPE3_SUBTITLE_"+sGXsfl_29_idx;
         edtavDisplaytype4_image_Internalname = sPrefix+"vDISPLAYTYPE4_IMAGE_"+sGXsfl_29_idx;
         edtavDisplaytype4_title_Internalname = sPrefix+"vDISPLAYTYPE4_TITLE_"+sGXsfl_29_idx;
         lblDisplaytype5_icon_Internalname = sPrefix+"DISPLAYTYPE5_ICON_"+sGXsfl_29_idx;
         edtavDisplaytype5_title_Internalname = sPrefix+"vDISPLAYTYPE5_TITLE_"+sGXsfl_29_idx;
         edtavDisplaytype5_subtitle_Internalname = sPrefix+"vDISPLAYTYPE5_SUBTITLE_"+sGXsfl_29_idx;
      }

      protected void SubsflControlProps_fel_293( )
      {
         edtavUrl_Internalname = sPrefix+"vURL_"+sGXsfl_29_fel_idx;
         edtavDisplaytype1_title_Internalname = sPrefix+"vDISPLAYTYPE1_TITLE_"+sGXsfl_29_fel_idx;
         lblDisplaytype2_icon_Internalname = sPrefix+"DISPLAYTYPE2_ICON_"+sGXsfl_29_fel_idx;
         edtavDisplaytype2_title_Internalname = sPrefix+"vDISPLAYTYPE2_TITLE_"+sGXsfl_29_fel_idx;
         edtavDisplaytype3_title_Internalname = sPrefix+"vDISPLAYTYPE3_TITLE_"+sGXsfl_29_fel_idx;
         edtavDisplaytype3_subtitle_Internalname = sPrefix+"vDISPLAYTYPE3_SUBTITLE_"+sGXsfl_29_fel_idx;
         edtavDisplaytype4_image_Internalname = sPrefix+"vDISPLAYTYPE4_IMAGE_"+sGXsfl_29_fel_idx;
         edtavDisplaytype4_title_Internalname = sPrefix+"vDISPLAYTYPE4_TITLE_"+sGXsfl_29_fel_idx;
         lblDisplaytype5_icon_Internalname = sPrefix+"DISPLAYTYPE5_ICON_"+sGXsfl_29_fel_idx;
         edtavDisplaytype5_title_Internalname = sPrefix+"vDISPLAYTYPE5_TITLE_"+sGXsfl_29_fel_idx;
         edtavDisplaytype5_subtitle_Internalname = sPrefix+"vDISPLAYTYPE5_SUBTITLE_"+sGXsfl_29_fel_idx;
      }

      protected void sendrow_293( )
      {
         sGXsfl_29_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_29_idx), 4, 0), 4, "0") + sGXsfl_21_idx;
         SubsflControlProps_293( ) ;
         WB0Z0( ) ;
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
            if ( ((int)((nGXsfl_29_idx) % (2))) == 0 )
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
            context.WriteHtmlText( "<tr"+" class=\""+subFsresults_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_29_idx+"\">") ;
         }
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtablefsfsresults_Internalname+"_"+sGXsfl_29_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Flex",(string)"start",(string)"top",(string)" "+"data-gx-flex"+" ",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Invisible",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Table start */
         FsresultsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtablecontentfsfsresults_Internalname+"_"+sGXsfl_29_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         FsresultsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavUrl_Internalname,context.GetMessage( "Url", ""),(string)"gx-form-item AttributeLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Single line edit */
         ROClassString = "Attribute";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavUrl_Internalname,(string)AV24Url,(string)"",(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavUrl_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavUrl_Visible,(short)0,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
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
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype1_cell_Internalname+"_"+sGXsfl_29_idx,(int)divDisplaytype1_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable1_Internalname+"_"+sGXsfl_29_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype1_title_Internalname,context.GetMessage( "Display Type1_Title", ""),(string)"col-sm-3 AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'" + sPrefix + "',false,'" + sGXsfl_29_idx + "',29)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype1_title_Internalname,(string)AV10DisplayType1_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,42);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype1_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype1_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype2_cell_Internalname+"_"+sGXsfl_29_idx,(int)divDisplaytype2_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Table start */
         FsresultsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtable2_Internalname+"_"+sGXsfl_29_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         FsresultsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Text block */
         FsresultsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblDisplaytype2_icon_Internalname,(string)lblDisplaytype2_icon_Caption,(string)"",(string)"",(string)lblDisplaytype2_icon_Jsonclick,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlock",(short)0,(string)"",(short)1,(short)1,(short)0,(short)2});
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
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 50,'" + sPrefix + "',false,'" + sGXsfl_29_idx + "',29)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype2_title_Internalname,(string)AV11DisplayType2_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,50);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype2_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype2_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
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
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype3_cell_Internalname+"_"+sGXsfl_29_idx,(int)divDisplaytype3_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable3_Internalname+"_"+sGXsfl_29_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype3_title_Internalname,context.GetMessage( "Display Type3_Title", ""),(string)"col-sm-3 AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'" + sPrefix + "',false,'" + sGXsfl_29_idx + "',29)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype3_title_Internalname,(string)AV13DisplayType3_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,56);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype3_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype3_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
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
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'" + sPrefix + "',false,'" + sGXsfl_29_idx + "',29)\"";
         ROClassString = "AttributeSearchResultSubtitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype3_subtitle_Internalname,(string)AV12DisplayType3_Subtitle,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,60);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype3_subtitle_Jsonclick,(short)0,(string)"AttributeSearchResultSubtitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype3_subtitle_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         sendrow_29330( ) ;
      }

      protected void sendrow_29330( )
      {
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype4_cell_Internalname+"_"+sGXsfl_29_idx,(int)divDisplaytype4_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable4_Internalname+"_"+sGXsfl_29_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Flex",(string)"start",(string)"top",(string)" "+"data-gx-flex"+" ",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"AttributeSearchResultImageCell",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)"",context.GetMessage( "Display Type4_Image", ""),(string)"gx-form-item AttributeSearchResultImageLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Static Bitmap Variable */
         ClassString = "AttributeSearchResultImage" + " " + ((StringUtil.StrCmp(edtavDisplaytype4_image_gximage, "")==0) ? "" : "GX_Image_"+edtavDisplaytype4_image_gximage+"_Class");
         StyleString = "";
         AV14DisplayType4_Image_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV14DisplayType4_Image))&&String.IsNullOrEmpty(StringUtil.RTrim( AV33Displaytype4_image_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV14DisplayType4_Image)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV14DisplayType4_Image)) ? AV33Displaytype4_image_GXI : context.PathToRelativeUrl( AV14DisplayType4_Image));
         FsresultsRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype4_image_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavDisplaytype4_image_Visible,(short)0,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"",(short)0,(string)"",(short)0,(short)0,(short)0,(string)"",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(bool)AV14DisplayType4_Image_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;align-self:center;",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype4_title_Internalname,context.GetMessage( "Display Type4_Title", ""),(string)"gx-form-item AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)"width: 25%;"});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 68,'" + sPrefix + "',false,'" + sGXsfl_29_idx + "',29)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype4_title_Internalname,(string)AV15DisplayType4_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,68);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype4_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype4_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divDisplaytype5_cell_Internalname+"_"+sGXsfl_29_idx,(int)divDisplaytype5_cell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         /* Table start */
         FsresultsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtable5_Internalname+"_"+sGXsfl_29_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         FsresultsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Text block */
         FsresultsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblDisplaytype5_icon_Internalname,(string)lblDisplaytype5_icon_Caption,(string)"",(string)"",(string)lblDisplaytype5_icon_Jsonclick,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlock",(short)0,(string)"",(short)1,(short)1,(short)0,(short)2});
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            FsresultsContainer.CloseTag("cell");
         }
         FsresultsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable6_Internalname+"_"+sGXsfl_29_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype5_title_Internalname,context.GetMessage( "Display Type5_Title", ""),(string)"col-sm-3 AttributeSearchResultTitleLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 79,'" + sPrefix + "',false,'" + sGXsfl_29_idx + "',29)\"";
         ROClassString = "AttributeSearchResultTitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype5_title_Internalname,(string)AV17DisplayType5_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,79);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype5_title_Jsonclick,(short)0,(string)"AttributeSearchResultTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype5_title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
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
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 83,'" + sPrefix + "',false,'" + sGXsfl_29_idx + "',29)\"";
         ROClassString = "AttributeSearchResultSubtitle";
         FsresultsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDisplaytype5_subtitle_Internalname,(string)AV16DisplayType5_Subtitle,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,83);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDisplaytype5_subtitle_Jsonclick,(short)0,(string)"AttributeSearchResultSubtitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavDisplaytype5_subtitle_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)29,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
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
         send_integrity_lvl_hashes0Z3( ) ;
         /* End of Columns property logic. */
         FsresultsContainer.AddRow(FsresultsRow);
         nGXsfl_29_idx = ((subFsresults_Islastpage==1)&&(nGXsfl_29_idx+1>subFsresults_fnc_Recordsperpage( )) ? 1 : nGXsfl_29_idx+1);
         sGXsfl_29_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_29_idx), 4, 0), 4, "0") + sGXsfl_21_idx;
         SubsflControlProps_293( ) ;
         /* End function sendrow_293 */
      }

      protected void SubsflControlProps_212( )
      {
         edtavWwp_searchresults__categoryname_Internalname = sPrefix+"WWP_SEARCHRESULTS__CATEGORYNAME_"+sGXsfl_21_idx;
         lblTxtshowingonly_Internalname = sPrefix+"TXTSHOWINGONLY_"+sGXsfl_21_idx;
         subFsresults_Internalname = sPrefix+"FSRESULTS_"+sGXsfl_21_idx;
      }

      protected void SubsflControlProps_fel_212( )
      {
         edtavWwp_searchresults__categoryname_Internalname = sPrefix+"WWP_SEARCHRESULTS__CATEGORYNAME_"+sGXsfl_21_fel_idx;
         lblTxtshowingonly_Internalname = sPrefix+"TXTSHOWINGONLY_"+sGXsfl_21_fel_idx;
         subFsresults_Internalname = sPrefix+"FSRESULTS_"+sGXsfl_21_fel_idx;
      }

      protected void sendrow_212( )
      {
         sGXsfl_21_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_21_idx), 4, 0), 4, "0");
         SubsflControlProps_212( ) ;
         WB0Z0( ) ;
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
            if ( ((int)((nGXsfl_21_idx) % (2))) == 0 )
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
            context.WriteHtmlText( "<tr"+" class=\""+subFsresultcategories_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_21_idx+"\">") ;
         }
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtablefsfsresultcategories_Internalname+"_"+sGXsfl_21_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 SearchResultMPCategoryCell",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Attribute/Variable Label */
         FsresultcategoriesRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavWwp_searchresults__categoryname_Internalname,context.GetMessage( "Category Name", ""),(string)"col-sm-3 AttributeLabel",(short)0,(bool)true,(string)""});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'" + sPrefix + "',false,'" + sGXsfl_21_idx + "',21)\"";
         ROClassString = "Attribute";
         FsresultcategoriesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavWwp_searchresults__categoryname_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV26WWP_SearchResults.Item(AV30GXV1)).gxTpr_Categoryname,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,26);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavWwp_searchresults__categoryname_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavWwp_searchresults__categoryname_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)21,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
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
         StartGridControl29( ) ;
         RF0Z3( ) ;
         nRC_GXsfl_29 = (int)(nGXsfl_29_idx-1);
         GXCCtl = "nRC_GXsfl_29_" + sGXsfl_21_idx;
         GxWebStd.gx_hidden_field( context, sPrefix+GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_29), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "</table>") ;
         }
         else
         {
            if ( ! isAjaxCallMode( ) )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"FsresultsContainerData"+"_"+sGXsfl_21_idx, FsresultsContainer.ToJavascriptSource());
            }
            if ( isAjaxCallMode( ) )
            {
               FsresultcategoriesRow.AddGrid("Fsresults", FsresultsContainer);
            }
            if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
            {
               GxWebStd.gx_hidden_field( context, sPrefix+"FsresultsContainerData"+"V_"+sGXsfl_21_idx, FsresultsContainer.GridValuesHidden());
            }
            else
            {
               context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"FsresultsContainerData"+"V_"+sGXsfl_21_idx+"\" value='"+FsresultsContainer.GridValuesHidden()+"'/>") ;
            }
         }
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         FsresultcategoriesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divShowingonlycell_Internalname+"_"+sGXsfl_21_idx,(int)divShowingonlycell_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Text block */
         FsresultcategoriesRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblTxtshowingonly_Internalname,(string)lblTxtshowingonly_Caption,(string)"",(string)"",(string)lblTxtshowingonly_Jsonclick,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlockShowingOnly",(short)0,(string)"",(short)1,(short)1,(short)0,(short)0});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FsresultcategoriesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         send_integrity_lvl_hashes0Z2( ) ;
         GXCCtl = "subFsresults_Recordcount_" + sGXsfl_21_idx;
         GxWebStd.gx_hidden_field( context, sPrefix+GXCCtl, StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresults_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         /* End of Columns property logic. */
         FsresultcategoriesContainer.AddRow(FsresultcategoriesRow);
         nGXsfl_21_idx = ((subFsresultcategories_Islastpage==1)&&(nGXsfl_21_idx+1>subFsresultcategories_fnc_Recordsperpage( )) ? 1 : nGXsfl_21_idx+1);
         sGXsfl_21_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_21_idx), 4, 0), 4, "0");
         SubsflControlProps_212( ) ;
         /* End function sendrow_212 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl21( )
      {
         if ( FsresultcategoriesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"FsresultcategoriesContainer"+"DivS\" data-gxgridid=\"21\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subFsresultcategories_Internalname, subFsresultcategories_Internalname, "", "FreeStyleGridMPSearch", 0, "", "", 1, 2, sStyleString, "", "", 0);
            FsresultcategoriesContainer.AddObjectProperty("GridName", "Fsresultcategories");
         }
         else
         {
            FsresultcategoriesContainer.AddObjectProperty("GridName", "Fsresultcategories");
            FsresultcategoriesContainer.AddObjectProperty("Header", subFsresultcategories_Header);
            FsresultcategoriesContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGridMPSearch"));
            FsresultcategoriesContainer.AddObjectProperty("Class", "FreeStyleGridMPSearch");
            FsresultcategoriesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFsresultcategories_Backcolorstyle), 1, 0, ".", "")));
            FsresultcategoriesContainer.AddObjectProperty("CmpContext", sPrefix);
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

      protected void StartGridControl29( )
      {
         if ( FsresultsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"FsresultsContainer"+"DivS\" data-gxgridid=\"29\">") ;
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
            FsresultsContainer.AddObjectProperty("CmpContext", sPrefix);
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
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV24Url));
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
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV10DisplayType1_Title));
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
            FsresultsColumn.AddObjectProperty("Value", lblDisplaytype2_icon_Caption);
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsContainer.AddColumnProperties(FsresultsColumn);
            FsresultsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV11DisplayType2_Title));
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
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV13DisplayType3_Title));
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
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV12DisplayType3_Subtitle));
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
            FsresultsColumn.AddObjectProperty("Value", context.convertURL( AV14DisplayType4_Image));
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
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV15DisplayType4_Title));
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
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV17DisplayType5_Title));
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
            FsresultsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV16DisplayType5_Subtitle));
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
         lblClosesearch_Internalname = sPrefix+"CLOSESEARCH";
         Btnshowall_Internalname = sPrefix+"BTNSHOWALL";
         divShowall_cell_Internalname = sPrefix+"SHOWALL_CELL";
         lblTxtnoresults_Internalname = sPrefix+"TXTNORESULTS";
         divTxtnoresultscell_Internalname = sPrefix+"TXTNORESULTSCELL";
         edtavWwp_searchresults__categoryname_Internalname = sPrefix+"WWP_SEARCHRESULTS__CATEGORYNAME";
         edtavUrl_Internalname = sPrefix+"vURL";
         tblUnnamedtablecontentfsfsresults_Internalname = sPrefix+"UNNAMEDTABLECONTENTFSFSRESULTS";
         edtavDisplaytype1_title_Internalname = sPrefix+"vDISPLAYTYPE1_TITLE";
         divUnnamedtable1_Internalname = sPrefix+"UNNAMEDTABLE1";
         divDisplaytype1_cell_Internalname = sPrefix+"DISPLAYTYPE1_CELL";
         lblDisplaytype2_icon_Internalname = sPrefix+"DISPLAYTYPE2_ICON";
         edtavDisplaytype2_title_Internalname = sPrefix+"vDISPLAYTYPE2_TITLE";
         tblUnnamedtable2_Internalname = sPrefix+"UNNAMEDTABLE2";
         divDisplaytype2_cell_Internalname = sPrefix+"DISPLAYTYPE2_CELL";
         edtavDisplaytype3_title_Internalname = sPrefix+"vDISPLAYTYPE3_TITLE";
         edtavDisplaytype3_subtitle_Internalname = sPrefix+"vDISPLAYTYPE3_SUBTITLE";
         divUnnamedtable3_Internalname = sPrefix+"UNNAMEDTABLE3";
         divDisplaytype3_cell_Internalname = sPrefix+"DISPLAYTYPE3_CELL";
         edtavDisplaytype4_image_Internalname = sPrefix+"vDISPLAYTYPE4_IMAGE";
         edtavDisplaytype4_title_Internalname = sPrefix+"vDISPLAYTYPE4_TITLE";
         divUnnamedtable4_Internalname = sPrefix+"UNNAMEDTABLE4";
         divDisplaytype4_cell_Internalname = sPrefix+"DISPLAYTYPE4_CELL";
         lblDisplaytype5_icon_Internalname = sPrefix+"DISPLAYTYPE5_ICON";
         edtavDisplaytype5_title_Internalname = sPrefix+"vDISPLAYTYPE5_TITLE";
         edtavDisplaytype5_subtitle_Internalname = sPrefix+"vDISPLAYTYPE5_SUBTITLE";
         divUnnamedtable6_Internalname = sPrefix+"UNNAMEDTABLE6";
         tblUnnamedtable5_Internalname = sPrefix+"UNNAMEDTABLE5";
         divDisplaytype5_cell_Internalname = sPrefix+"DISPLAYTYPE5_CELL";
         divUnnamedtablefsfsresults_Internalname = sPrefix+"UNNAMEDTABLEFSFSRESULTS";
         lblTxtshowingonly_Internalname = sPrefix+"TXTSHOWINGONLY";
         divShowingonlycell_Internalname = sPrefix+"SHOWINGONLYCELL";
         divUnnamedtablefsfsresultcategories_Internalname = sPrefix+"UNNAMEDTABLEFSFSRESULTCATEGORIES";
         divFsresultcategories_cell_Internalname = sPrefix+"FSRESULTCATEGORIES_CELL";
         divTablesimplesearch_Internalname = sPrefix+"TABLESIMPLESEARCH";
         divTablesimplesearchcell_Internalname = sPrefix+"TABLESIMPLESEARCHCELL";
         edtavAdvfiltertext_Internalname = sPrefix+"vADVFILTERTEXT";
         lblTextblockcombo_advfilterentities_Internalname = sPrefix+"TEXTBLOCKCOMBO_ADVFILTERENTITIES";
         Combo_advfilterentities_Internalname = sPrefix+"COMBO_ADVFILTERENTITIES";
         divTablesplittedadvfilterentities_Internalname = sPrefix+"TABLESPLITTEDADVFILTERENTITIES";
         bttBtnbtnadvsearch_Internalname = sPrefix+"BTNBTNADVSEARCH";
         divTableadvancedsearch_Internalname = sPrefix+"TABLEADVANCEDSEARCH";
         divTableadvancedsearchcell_Internalname = sPrefix+"TABLEADVANCEDSEARCHCELL";
         Btnadvancedsearch_Internalname = sPrefix+"BTNADVANCEDSEARCH";
         divTablemain_Internalname = sPrefix+"TABLEMAIN";
         divLayoutmaintable_Internalname = sPrefix+"LAYOUTMAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subFsresults_Internalname = sPrefix+"FSRESULTS";
         subFsresultcategories_Internalname = sPrefix+"FSRESULTCATEGORIES";
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
         subFsresults_Allowcollapsing = 0;
         lblDisplaytype2_icon_Caption = context.GetMessage( "<i class='fa fa-home'></i>", "");
         subFsresultcategories_Allowcollapsing = 0;
         lblTxtshowingonly_Caption = context.GetMessage( "WWP_Search_ShowingTopResults", "");
         lblTxtshowingonly_Caption = context.GetMessage( "WWP_Search_ShowingTopResults", "");
         divShowingonlycell_Visible = 1;
         edtavWwp_searchresults__categoryname_Jsonclick = "";
         edtavWwp_searchresults__categoryname_Enabled = 0;
         subFsresultcategories_Class = "FreeStyleGridMPSearch";
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
         Btnadvancedsearch_Class = "IconButtonLink";
         Btnadvancedsearch_Caption = context.GetMessage( "WWP_Search_AdvancedSearch", "");
         Btnadvancedsearch_Beforeiconclass = "fas fa-filter";
         Btnadvancedsearch_Tooltiptext = "";
         Combo_advfilterentities_Multiplevaluestype = "Tags";
         Combo_advfilterentities_Emptyitem = Convert.ToBoolean( 0);
         Combo_advfilterentities_Includeonlyselectedoption = Convert.ToBoolean( -1);
         Combo_advfilterentities_Allowmultipleselection = Convert.ToBoolean( -1);
         Combo_advfilterentities_Cls = "ExtendedCombo Attribute";
         Combo_advfilterentities_Caption = "";
         edtavAdvfiltertext_Jsonclick = "";
         edtavAdvfiltertext_Enabled = 1;
         divFsresultcategories_cell_Class = "col-xs-12 FSResultCategoriesCell";
         lblTxtnoresults_Caption = context.GetMessage( "WWP_Search_NoResults", "");
         divTxtnoresultscell_Class = "col-xs-12";
         Btnshowall_Class = "IconButtonLink IconButtonSearchAll";
         Btnshowall_Caption = context.GetMessage( "WWP_Search_ShowAll", "");
         Btnshowall_Beforeiconclass = "fas fa-search";
         Btnshowall_Tooltiptext = "";
         divShowall_cell_Class = "col-xs-12";
         divTablesimplesearchcell_Class = "col-xs-12";
         divTableadvancedsearchcell_Class = "col-xs-12 TableAdvancedSearchCell";
         edtavWwp_searchresults__categoryname_Enabled = -1;
         edtavUrl_Visible = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"FSRESULTCATEGORIES_nEOF"},{"av":"FSRESULTS_nFirstRecordOnPage"},{"av":"FSRESULTS_nEOF"},{"av":"edtavUrl_Visible","ctrl":"vURL","prop":"Visible"},{"av":"sPrefix"},{"av":"AV26WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":21,"hsh":true},{"av":"nGXsfl_21_idx","ctrl":"GRID","prop":"GridCurrRow","grid":21},{"av":"nRC_GXsfl_21","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":21}]}""");
         setEventMetadata("FSRESULTCATEGORIES.LOAD","""{"handler":"E160Z2","iparms":[{"av":"AV26WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":21,"hsh":true},{"av":"nGXsfl_21_idx","ctrl":"GRID","prop":"GridCurrRow","grid":21},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_21","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":21}]""");
         setEventMetadata("FSRESULTCATEGORIES.LOAD",""","oparms":[{"av":"divShowingonlycell_Visible","ctrl":"SHOWINGONLYCELL","prop":"Visible"},{"av":"lblTxtshowingonly_Caption","ctrl":"TXTSHOWINGONLY","prop":"Caption"}]}""");
         setEventMetadata("FSRESULTS.LOAD","""{"handler":"E170Z3","iparms":[{"av":"AV26WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":21,"hsh":true},{"av":"nGXsfl_21_idx","ctrl":"GRID","prop":"GridCurrRow","grid":21},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_21","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":21}]""");
         setEventMetadata("FSRESULTS.LOAD",""","oparms":[{"av":"divDisplaytype1_cell_Visible","ctrl":"DISPLAYTYPE1_CELL","prop":"Visible"},{"av":"divDisplaytype2_cell_Visible","ctrl":"DISPLAYTYPE2_CELL","prop":"Visible"},{"av":"divDisplaytype3_cell_Visible","ctrl":"DISPLAYTYPE3_CELL","prop":"Visible"},{"av":"divDisplaytype4_cell_Visible","ctrl":"DISPLAYTYPE4_CELL","prop":"Visible"},{"av":"divDisplaytype5_cell_Visible","ctrl":"DISPLAYTYPE5_CELL","prop":"Visible"},{"av":"AV10DisplayType1_Title","fld":"vDISPLAYTYPE1_TITLE"},{"av":"AV11DisplayType2_Title","fld":"vDISPLAYTYPE2_TITLE"},{"av":"lblDisplaytype2_icon_Caption","ctrl":"DISPLAYTYPE2_ICON","prop":"Caption"},{"av":"AV13DisplayType3_Title","fld":"vDISPLAYTYPE3_TITLE"},{"av":"AV12DisplayType3_Subtitle","fld":"vDISPLAYTYPE3_SUBTITLE"},{"av":"AV15DisplayType4_Title","fld":"vDISPLAYTYPE4_TITLE"},{"av":"AV14DisplayType4_Image","fld":"vDISPLAYTYPE4_IMAGE"},{"av":"edtavDisplaytype4_image_Visible","ctrl":"vDISPLAYTYPE4_IMAGE","prop":"Visible"},{"av":"AV17DisplayType5_Title","fld":"vDISPLAYTYPE5_TITLE"},{"av":"AV16DisplayType5_Subtitle","fld":"vDISPLAYTYPE5_SUBTITLE"},{"av":"lblDisplaytype5_icon_Caption","ctrl":"DISPLAYTYPE5_ICON","prop":"Caption"},{"av":"AV24Url","fld":"vURL"}]}""");
         setEventMetadata("'DOCLOSESEARCH'","""{"handler":"E110Z2","iparms":[]}""");
         setEventMetadata("'DOSHOWALL'","""{"handler":"E120Z2","iparms":[{"av":"AV22SearchText","fld":"vSEARCHTEXT"}]}""");
         setEventMetadata("'DOBTNADVSEARCH'","""{"handler":"E130Z2","iparms":[{"av":"AV8AdvFilterText","fld":"vADVFILTERTEXT"},{"av":"AV5AdvFilterEntities","fld":"vADVFILTERENTITIES"}]}""");
         setEventMetadata("UNNAMEDTABLEFSFSRESULTS.CLICK","""{"handler":"E140Z2","iparms":[{"av":"AV26WWP_SearchResults","fld":"vWWP_SEARCHRESULTS","grid":21,"hsh":true},{"av":"nGXsfl_21_idx","ctrl":"GRID","prop":"GridCurrRow","grid":21},{"av":"FSRESULTCATEGORIES_nFirstRecordOnPage"},{"av":"nRC_GXsfl_21","ctrl":"FSRESULTCATEGORIES","prop":"GridRC","grid":21},{"av":"AV24Url","fld":"vURL"}]}""");
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
         wcpOAV22SearchText = "";
         Combo_advfilterentities_Selectedvalue_get = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV26WWP_SearchResults = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV9DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV6AdvFilterEntities_Data = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Item", "");
         AV5AdvFilterEntities = new GxSimpleCollection<string>();
         GX_FocusControl = "";
         lblClosesearch_Jsonclick = "";
         ucBtnshowall = new GXUserControl();
         lblTxtnoresults_Jsonclick = "";
         FsresultcategoriesContainer = new GXWebGrid( context);
         sStyleString = "";
         TempTags = "";
         AV8AdvFilterText = "";
         lblTextblockcombo_advfilterentities_Jsonclick = "";
         ucCombo_advfilterentities = new GXUserControl();
         ClassString = "";
         StyleString = "";
         bttBtnbtnadvsearch_Jsonclick = "";
         ucBtnadvancedsearch = new GXUserControl();
         FsresultsContainer = new GXWebGrid( context);
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXCCtl = "";
         AV24Url = "";
         AV10DisplayType1_Title = "";
         AV11DisplayType2_Title = "";
         AV13DisplayType3_Title = "";
         AV12DisplayType3_Subtitle = "";
         AV14DisplayType4_Image = "";
         AV33Displaytype4_image_GXI = "";
         AV15DisplayType4_Title = "";
         AV17DisplayType5_Title = "";
         AV16DisplayType5_Subtitle = "";
         GXDecQS = "";
         AV21RecentSearchResultsJson = "";
         GXt_char1 = "";
         GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons3 = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         FsresultcategoriesRow = new GXWebRow();
         AV19EntityDescriptions = new GxSimpleCollection<string>();
         AV25WWP_SearchResultAux = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         AV18EntityDescription = "";
         AV7AdvFilterEntities_DataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item(context);
         Combo_advfilterentities_Selectedvalue_set = "";
         FsresultsRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV22SearchText = "";
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
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short subFsresultcategories_Backcolorstyle ;
      private short subFsresults_Backcolorstyle ;
      private short AV20MinimumCharsToSearch ;
      private short gxcookieaux ;
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
      private short FSRESULTCATEGORIES_nEOF ;
      private short FSRESULTS_nEOF ;
      private int nRC_GXsfl_29 ;
      private int nGXsfl_29_idx=1 ;
      private int nRC_GXsfl_21 ;
      private int edtavUrl_Visible ;
      private int nGXsfl_21_idx=1 ;
      private int edtavWwp_searchresults__categoryname_Enabled ;
      private int edtavDisplaytype1_title_Enabled ;
      private int edtavDisplaytype2_title_Enabled ;
      private int edtavDisplaytype3_title_Enabled ;
      private int edtavDisplaytype3_subtitle_Enabled ;
      private int edtavDisplaytype4_title_Enabled ;
      private int edtavDisplaytype5_title_Enabled ;
      private int edtavDisplaytype5_subtitle_Enabled ;
      private int AV30GXV1 ;
      private int edtavAdvfiltertext_Enabled ;
      private int AV32GXV3 ;
      private int subFsresults_Recordcount ;
      private int subFsresultcategories_Islastpage ;
      private int subFsresults_Islastpage ;
      private int nGXsfl_21_fel_idx=1 ;
      private int divShowingonlycell_Visible ;
      private int AV34GXV4 ;
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
      private string divTableadvancedsearchcell_Class ;
      private string divTablesimplesearchcell_Class ;
      private string Combo_advfilterentities_Selectedvalue_get ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_29_idx="0001" ;
      private string edtavUrl_Internalname ;
      private string sGXsfl_21_idx="0001" ;
      private string edtavWwp_searchresults__categoryname_Internalname ;
      private string edtavDisplaytype1_title_Internalname ;
      private string edtavDisplaytype2_title_Internalname ;
      private string edtavDisplaytype3_title_Internalname ;
      private string edtavDisplaytype3_subtitle_Internalname ;
      private string edtavDisplaytype4_title_Internalname ;
      private string edtavDisplaytype5_title_Internalname ;
      private string edtavDisplaytype5_subtitle_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string GX_FocusControl ;
      private string divLayoutmaintable_Internalname ;
      private string divTablemain_Internalname ;
      private string lblClosesearch_Internalname ;
      private string lblClosesearch_Jsonclick ;
      private string divShowall_cell_Internalname ;
      private string divShowall_cell_Class ;
      private string Btnshowall_Tooltiptext ;
      private string Btnshowall_Beforeiconclass ;
      private string Btnshowall_Caption ;
      private string Btnshowall_Class ;
      private string Btnshowall_Internalname ;
      private string divTxtnoresultscell_Internalname ;
      private string divTxtnoresultscell_Class ;
      private string lblTxtnoresults_Internalname ;
      private string lblTxtnoresults_Caption ;
      private string lblTxtnoresults_Jsonclick ;
      private string divTablesimplesearchcell_Internalname ;
      private string divTablesimplesearch_Internalname ;
      private string divFsresultcategories_cell_Internalname ;
      private string divFsresultcategories_cell_Class ;
      private string sStyleString ;
      private string subFsresultcategories_Internalname ;
      private string divTableadvancedsearchcell_Internalname ;
      private string divTableadvancedsearch_Internalname ;
      private string edtavAdvfiltertext_Internalname ;
      private string TempTags ;
      private string edtavAdvfiltertext_Jsonclick ;
      private string divTablesplittedadvfilterentities_Internalname ;
      private string lblTextblockcombo_advfilterentities_Internalname ;
      private string lblTextblockcombo_advfilterentities_Jsonclick ;
      private string Combo_advfilterentities_Caption ;
      private string Combo_advfilterentities_Cls ;
      private string Combo_advfilterentities_Multiplevaluestype ;
      private string Combo_advfilterentities_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtnbtnadvsearch_Internalname ;
      private string bttBtnbtnadvsearch_Jsonclick ;
      private string Btnadvancedsearch_Tooltiptext ;
      private string Btnadvancedsearch_Beforeiconclass ;
      private string Btnadvancedsearch_Caption ;
      private string Btnadvancedsearch_Class ;
      private string Btnadvancedsearch_Internalname ;
      private string subFsresults_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXCCtl ;
      private string edtavDisplaytype4_image_Internalname ;
      private string GXDecQS ;
      private string sGXsfl_21_fel_idx="0001" ;
      private string GXt_char1 ;
      private string divShowingonlycell_Internalname ;
      private string lblTxtshowingonly_Caption ;
      private string Combo_advfilterentities_Selectedvalue_set ;
      private string divDisplaytype1_cell_Internalname ;
      private string divDisplaytype2_cell_Internalname ;
      private string divDisplaytype3_cell_Internalname ;
      private string divDisplaytype4_cell_Internalname ;
      private string divDisplaytype5_cell_Internalname ;
      private string lblDisplaytype2_icon_Caption ;
      private string lblDisplaytype5_icon_Caption ;
      private string sCtrlAV22SearchText ;
      private string lblDisplaytype2_icon_Internalname ;
      private string lblDisplaytype5_icon_Internalname ;
      private string sGXsfl_29_fel_idx="0001" ;
      private string subFsresults_Class ;
      private string subFsresults_Linesclass ;
      private string divUnnamedtablefsfsresults_Internalname ;
      private string tblUnnamedtablecontentfsfsresults_Internalname ;
      private string ROClassString ;
      private string edtavUrl_Jsonclick ;
      private string divUnnamedtable1_Internalname ;
      private string edtavDisplaytype1_title_Jsonclick ;
      private string tblUnnamedtable2_Internalname ;
      private string lblDisplaytype2_icon_Jsonclick ;
      private string edtavDisplaytype2_title_Jsonclick ;
      private string divUnnamedtable3_Internalname ;
      private string edtavDisplaytype3_title_Jsonclick ;
      private string edtavDisplaytype3_subtitle_Jsonclick ;
      private string divUnnamedtable4_Internalname ;
      private string edtavDisplaytype4_image_gximage ;
      private string sImgUrl ;
      private string edtavDisplaytype4_title_Jsonclick ;
      private string tblUnnamedtable5_Internalname ;
      private string lblDisplaytype5_icon_Jsonclick ;
      private string divUnnamedtable6_Internalname ;
      private string edtavDisplaytype5_title_Jsonclick ;
      private string edtavDisplaytype5_subtitle_Jsonclick ;
      private string lblTxtshowingonly_Internalname ;
      private string subFsresultcategories_Class ;
      private string subFsresultcategories_Linesclass ;
      private string divUnnamedtablefsfsresultcategories_Internalname ;
      private string edtavWwp_searchresults__categoryname_Jsonclick ;
      private string lblTxtshowingonly_Jsonclick ;
      private string subFsresultcategories_Header ;
      private string subFsresults_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_29_Refreshing=false ;
      private bool bGXsfl_21_Refreshing=false ;
      private bool wbLoad ;
      private bool Combo_advfilterentities_Allowmultipleselection ;
      private bool Combo_advfilterentities_Includeonlyselectedoption ;
      private bool Combo_advfilterentities_Emptyitem ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV23ShowingRecents ;
      private bool gx_BV21 ;
      private bool AV14DisplayType4_Image_IsBlob ;
      private string AV22SearchText ;
      private string wcpOAV22SearchText ;
      private string AV8AdvFilterText ;
      private string AV24Url ;
      private string AV10DisplayType1_Title ;
      private string AV11DisplayType2_Title ;
      private string AV13DisplayType3_Title ;
      private string AV12DisplayType3_Subtitle ;
      private string AV33Displaytype4_image_GXI ;
      private string AV15DisplayType4_Title ;
      private string AV17DisplayType5_Title ;
      private string AV16DisplayType5_Subtitle ;
      private string AV21RecentSearchResultsJson ;
      private string AV18EntityDescription ;
      private string AV14DisplayType4_Image ;
      private GXWebGrid FsresultcategoriesContainer ;
      private GXWebGrid FsresultsContainer ;
      private GXWebRow FsresultcategoriesRow ;
      private GXWebRow FsresultsRow ;
      private GXWebColumn FsresultcategoriesColumn ;
      private GXWebColumn FsresultsColumn ;
      private GXUserControl ucBtnshowall ;
      private GXUserControl ucCombo_advfilterentities ;
      private GXUserControl ucBtnadvancedsearch ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV26WWP_SearchResults ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV9DDO_TitleSettingsIcons ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> AV6AdvFilterEntities_Data ;
      private GxSimpleCollection<string> AV5AdvFilterEntities ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> GXt_objcol_SdtWWP_SearchResults_WWP_SearchResultsItem2 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons3 ;
      private GxSimpleCollection<string> AV19EntityDescriptions ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV25WWP_SearchResultAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item AV7AdvFilterEntities_DataItem ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
