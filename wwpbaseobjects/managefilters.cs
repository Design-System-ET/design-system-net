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
using GeneXus.Http.Server;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs.wwpbaseobjects {
   public class managefilters : GXDataArea
   {
      public managefilters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public managefilters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_UserKey )
      {
         this.AV14UserKey = aP0_UserKey;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         chkavCollectionisempty = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "UserKey");
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
               gxfirstwebparm = GetFirstPar( "UserKey");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "UserKey");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridgridstatecollections") == 0 )
            {
               gxnrGridgridstatecollections_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridgridstatecollections") == 0 )
            {
               gxgrGridgridstatecollections_refresh_invoke( ) ;
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

      protected void gxnrGridgridstatecollections_newrow_invoke( )
      {
         nRC_GXsfl_15 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_15"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_15_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_15_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_15_idx = GetPar( "sGXsfl_15_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridgridstatecollections_newrow( ) ;
         /* End function gxnrGridgridstatecollections_newrow_invoke */
      }

      protected void gxgrGridgridstatecollections_refresh_invoke( )
      {
         subGridgridstatecollections_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGridgridstatecollections_Rows"), "."), 18, MidpointRounding.ToEven));
         AV14UserKey = GetPar( "UserKey");
         AV5CollectionIsEmpty = StringUtil.StrToBool( GetPar( "CollectionIsEmpty"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridgridstatecollections_refresh_invoke */
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
            return "managefilters_Execute" ;
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
         PA0O2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START0O2( ) ;
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
         context.AddJavascriptSource("shared/jquery/jquery1.9.1.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/bootstrap.js", "", false, true);
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
         context.WriteHtmlText( " "+"class=\"form-horizontal FormNoBackgroundColor\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "wwpbaseobjects.managefilters.aspx"+UrlEncode(StringUtil.RTrim(AV14UserKey));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal FormNoBackgroundColor\" data-gx-class=\"form-horizontal FormNoBackgroundColor\" novalidate action=\""+formatLink("wwpbaseobjects.managefilters.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vUSERKEY", AV14UserKey);
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14UserKey, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Gridstatecollection", AV6GridStateCollection);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Gridstatecollection", AV6GridStateCollection);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_15", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_15), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vUSERKEY", AV14UserKey);
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14UserKey, "")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGRIDSTATECOLLECTION", AV6GridStateCollection);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGRIDSTATECOLLECTION", AV6GridStateCollection);
         }
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_EMPOWERER_Gridinternalname", StringUtil.RTrim( Gridgridstatecollections_empowerer_Gridinternalname));
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
            WE0O2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT0O2( ) ;
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
         GXEncryptionTmp = "wwpbaseobjects.managefilters.aspx"+UrlEncode(StringUtil.RTrim(AV14UserKey));
         return formatLink("wwpbaseobjects.managefilters.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "WWPBaseObjects.ManageFilters" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "WWP_ManageFiltersDescription", "") ;
      }

      protected void WB0O0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 PopupContentCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablecontent_Internalname, 1, 0, "px", 0, "px", "TableContent", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 EditableGridCell_LinedAtts HasGridEmpowerer", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridgridstatecollectionsContainer.SetWrapped(nGXWrapped);
            StartGridControl15( ) ;
         }
         if ( wbEnd == 15 )
         {
            wbEnd = 0;
            nRC_GXsfl_15 = (int)(nGXsfl_15_idx-1);
            if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               GridgridstatecollectionsContainer.AddObjectProperty("GRIDGRIDSTATECOLLECTIONS_nEOF", GRIDGRIDSTATECOLLECTIONS_nEOF);
               GridgridstatecollectionsContainer.AddObjectProperty("GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
               AV17GXV1 = nGXsfl_15_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridgridstatecollectionsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridgridstatecollections", GridgridstatecollectionsContainer, subGridgridstatecollections_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridgridstatecollectionsContainerData", GridgridstatecollectionsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridgridstatecollectionsContainerData"+"V", GridgridstatecollectionsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridgridstatecollectionsContainerData"+"V"+"\" value='"+GridgridstatecollectionsContainer.GridValuesHidden()+"'/>") ;
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroupRight", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(15), 2, 0)+","+"null"+");", context.GetMessage( "WWP_SaveButtonCaption", ""), bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_WWPBaseObjects/ManageFilters.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 27,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(15), 2, 0)+","+"null"+");", context.GetMessage( "GX_BtnCancel", ""), bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_WWPBaseObjects/ManageFilters.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divHtml_usertable_utpaneldummy_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
            wb_table1_31_0O2( true) ;
         }
         else
         {
            wb_table1_31_0O2( false) ;
         }
         return  ;
      }

      protected void wb_table1_31_0O2e( bool wbgen )
      {
         if ( wbgen )
         {
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
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 39,'',false,'" + sGXsfl_15_idx + "',0)\"";
            ClassString = "Attribute";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavCollectionisempty_Internalname, StringUtil.BoolToStr( AV5CollectionIsEmpty), "", "", chkavCollectionisempty.Visible, 1, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(39, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,39);\"");
            /* User Defined Control */
            ucGridgridstatecollections_empowerer.Render(context, "wwp.gridempowerer", Gridgridstatecollections_empowerer_Internalname, "GRIDGRIDSTATECOLLECTIONS_EMPOWERERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 15 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  GridgridstatecollectionsContainer.AddObjectProperty("GRIDGRIDSTATECOLLECTIONS_nEOF", GRIDGRIDSTATECOLLECTIONS_nEOF);
                  GridgridstatecollectionsContainer.AddObjectProperty("GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
                  AV17GXV1 = nGXsfl_15_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridgridstatecollectionsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridgridstatecollections", GridgridstatecollectionsContainer, subGridgridstatecollections_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridgridstatecollectionsContainerData", GridgridstatecollectionsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridgridstatecollectionsContainerData"+"V", GridgridstatecollectionsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridgridstatecollectionsContainerData"+"V"+"\" value='"+GridgridstatecollectionsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START0O2( )
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
         Form.Meta.addItem("description", context.GetMessage( "WWP_ManageFiltersDescription", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP0O0( ) ;
      }

      protected void WS0O2( )
      {
         START0O2( ) ;
         EVT0O2( ) ;
      }

      protected void EVT0O2( )
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
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              context.wbHandled = 1;
                              if ( ! wbErr )
                              {
                                 Rfr0gs = false;
                                 if ( ! Rfr0gs )
                                 {
                                    /* Execute user event: Enter */
                                    E110O2 ();
                                 }
                                 dynload_actions( ) ;
                              }
                              /* No code required for Cancel button. It is implemented as the Reset button. */
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDGRIDSTATECOLLECTIONSPAGING") == 0 )
                           {
                              context.wbHandled = 1;
                              sEvt = cgiGet( "GRIDGRIDSTATECOLLECTIONSPAGING");
                              if ( StringUtil.StrCmp(sEvt, "FIRST") == 0 )
                              {
                                 subgridgridstatecollections_firstpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "PREV") == 0 )
                              {
                                 subgridgridstatecollections_previouspage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "NEXT") == 0 )
                              {
                                 subgridgridstatecollections_nextpage( ) ;
                              }
                              else if ( StringUtil.StrCmp(sEvt, "LAST") == 0 )
                              {
                                 subgridgridstatecollections_lastpage( ) ;
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 29), "GRIDGRIDSTATECOLLECTIONS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "VMOVEUP.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "VMOVEDOWN.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "VUDELETE.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "VMOVEUP.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 15), "VMOVEDOWN.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "VUDELETE.CLICK") == 0 ) )
                           {
                              nGXsfl_15_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
                              SubsflControlProps_152( ) ;
                              AV17GXV1 = (int)(nGXsfl_15_idx+GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
                              if ( ( AV6GridStateCollection.Count >= AV17GXV1 ) && ( AV17GXV1 > 0 ) )
                              {
                                 AV6GridStateCollection.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1));
                                 AV12MoveUp = cgiGet( edtavMoveup_Internalname);
                                 AssignAttri("", false, edtavMoveup_Internalname, AV12MoveUp);
                                 AV11MoveDown = cgiGet( edtavMovedown_Internalname);
                                 AssignAttri("", false, edtavMovedown_Internalname, AV11MoveDown);
                                 AV13UDelete = cgiGet( edtavUdelete_Internalname);
                                 AssignAttri("", false, edtavUdelete_Internalname, AV13UDelete);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E120O2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDGRIDSTATECOLLECTIONS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridgridstatecollections.Load */
                                    E130O2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E140O2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VMOVEUP.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E150O2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VMOVEDOWN.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E160O2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VUDELETE.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E170O2 ();
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

      protected void WE0O2( )
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

      protected void PA0O2( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "wwpbaseobjects.managefilters.aspx")), "wwpbaseobjects.managefilters.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "wwpbaseobjects.managefilters.aspx")))) ;
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
                  gxfirstwebparm = GetFirstPar( "UserKey");
                  toggleJsOutput = isJsOutputEnabled( );
                  if ( context.isSpaRequest( ) )
                  {
                     disableJsOutput();
                  }
                  if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
                  {
                     AV14UserKey = gxfirstwebparm;
                     AssignAttri("", false, "AV14UserKey", AV14UserKey);
                     GxWebStd.gx_hidden_field( context, "gxhash_vUSERKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14UserKey, "")), context));
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
               GX_FocusControl = chkavCollectionisempty_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridgridstatecollections_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_152( ) ;
         while ( nGXsfl_15_idx <= nRC_GXsfl_15 )
         {
            sendrow_152( ) ;
            nGXsfl_15_idx = ((subGridgridstatecollections_Islastpage==1)&&(nGXsfl_15_idx+1>subGridgridstatecollections_fnc_Recordsperpage( )) ? 1 : nGXsfl_15_idx+1);
            sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
            SubsflControlProps_152( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridgridstatecollectionsContainer)) ;
         /* End function gxnrGridgridstatecollections_newrow */
      }

      protected void gxgrGridgridstatecollections_refresh( int subGridgridstatecollections_Rows ,
                                                           string AV14UserKey ,
                                                           bool AV5CollectionIsEmpty )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDGRIDSTATECOLLECTIONS_nCurrentRecord = 0;
         RF0O2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridgridstatecollections_refresh */
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
         AV5CollectionIsEmpty = StringUtil.StrToBool( StringUtil.BoolToStr( AV5CollectionIsEmpty));
         AssignAttri("", false, "AV5CollectionIsEmpty", AV5CollectionIsEmpty);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF0O2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavMoveup_Enabled = 0;
         edtavMovedown_Enabled = 0;
         edtavUdelete_Enabled = 0;
      }

      protected void RF0O2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridgridstatecollectionsContainer.ClearRows();
         }
         wbStart = 15;
         /* Execute user event: Refresh */
         E140O2 ();
         nGXsfl_15_idx = 1;
         sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
         SubsflControlProps_152( ) ;
         bGXsfl_15_Refreshing = true;
         GridgridstatecollectionsContainer.AddObjectProperty("GridName", "Gridgridstatecollections");
         GridgridstatecollectionsContainer.AddObjectProperty("CmpContext", "");
         GridgridstatecollectionsContainer.AddObjectProperty("InMasterPage", "false");
         GridgridstatecollectionsContainer.AddObjectProperty("Class", "GridNoBorder WorkWith");
         GridgridstatecollectionsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridgridstatecollectionsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridgridstatecollectionsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Backcolorstyle), 1, 0, ".", "")));
         GridgridstatecollectionsContainer.PageSize = subGridgridstatecollections_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_152( ) ;
            /* Execute user event: Gridgridstatecollections.Load */
            E130O2 ();
            if ( ( subGridgridstatecollections_Islastpage == 0 ) && ( GRIDGRIDSTATECOLLECTIONS_nCurrentRecord > 0 ) && ( GRIDGRIDSTATECOLLECTIONS_nGridOutOfScope == 0 ) && ( nGXsfl_15_idx == 1 ) )
            {
               GRIDGRIDSTATECOLLECTIONS_nCurrentRecord = 0;
               GRIDGRIDSTATECOLLECTIONS_nGridOutOfScope = 1;
               subgridgridstatecollections_firstpage( ) ;
               /* Execute user event: Gridgridstatecollections.Load */
               E130O2 ();
            }
            wbEnd = 15;
            WB0O0( ) ;
         }
         bGXsfl_15_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes0O2( )
      {
      }

      protected int subGridgridstatecollections_fnc_Pagecount( )
      {
         GRIDGRIDSTATECOLLECTIONS_nRecordCount = subGridgridstatecollections_fnc_Recordcount( );
         if ( ((int)((GRIDGRIDSTATECOLLECTIONS_nRecordCount) % (subGridgridstatecollections_fnc_Recordsperpage( )))) == 0 )
         {
            return (int)(NumberUtil.Int( (long)(Math.Round(GRIDGRIDSTATECOLLECTIONS_nRecordCount/ (decimal)(subGridgridstatecollections_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))) ;
         }
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDGRIDSTATECOLLECTIONS_nRecordCount/ (decimal)(subGridgridstatecollections_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected int subGridgridstatecollections_fnc_Recordcount( )
      {
         return AV6GridStateCollection.Count ;
      }

      protected int subGridgridstatecollections_fnc_Recordsperpage( )
      {
         if ( subGridgridstatecollections_Rows > 0 )
         {
            return subGridgridstatecollections_Rows*1 ;
         }
         else
         {
            return (int)(-1) ;
         }
      }

      protected int subGridgridstatecollections_fnc_Currentpage( )
      {
         return (int)(NumberUtil.Int( (long)(Math.Round(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage/ (decimal)(subGridgridstatecollections_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1) ;
      }

      protected short subgridgridstatecollections_firstpage( )
      {
         GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridgridstatecollections_nextpage( )
      {
         GRIDGRIDSTATECOLLECTIONS_nRecordCount = subGridgridstatecollections_fnc_Recordcount( );
         if ( ( GRIDGRIDSTATECOLLECTIONS_nRecordCount >= subGridgridstatecollections_fnc_Recordsperpage( ) ) && ( GRIDGRIDSTATECOLLECTIONS_nEOF == 0 ) )
         {
            GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = (long)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage+subGridgridstatecollections_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage), 15, 0, ".", "")));
         GridgridstatecollectionsContainer.AddObjectProperty("GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRIDGRIDSTATECOLLECTIONS_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgridgridstatecollections_previouspage( )
      {
         if ( GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage >= subGridgridstatecollections_fnc_Recordsperpage( ) )
         {
            GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = (long)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage-subGridgridstatecollections_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgridgridstatecollections_lastpage( )
      {
         GRIDGRIDSTATECOLLECTIONS_nRecordCount = subGridgridstatecollections_fnc_Recordcount( );
         if ( GRIDGRIDSTATECOLLECTIONS_nRecordCount > subGridgridstatecollections_fnc_Recordsperpage( ) )
         {
            if ( ((int)((GRIDGRIDSTATECOLLECTIONS_nRecordCount) % (subGridgridstatecollections_fnc_Recordsperpage( )))) == 0 )
            {
               GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = (long)(GRIDGRIDSTATECOLLECTIONS_nRecordCount-subGridgridstatecollections_fnc_Recordsperpage( ));
            }
            else
            {
               GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = (long)(GRIDGRIDSTATECOLLECTIONS_nRecordCount-((int)((GRIDGRIDSTATECOLLECTIONS_nRecordCount) % (subGridgridstatecollections_fnc_Recordsperpage( )))));
            }
         }
         else
         {
            GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgridgridstatecollections_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = (long)(subGridgridstatecollections_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         edtavMoveup_Enabled = 0;
         edtavMovedown_Enabled = 0;
         edtavUdelete_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0O0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E120O2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Gridstatecollection"), AV6GridStateCollection);
            ajax_req_read_hidden_sdt(cgiGet( "vGRIDSTATECOLLECTION"), AV6GridStateCollection);
            /* Read saved values. */
            nRC_GXsfl_15 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_15"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRIDGRIDSTATECOLLECTIONS_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDGRIDSTATECOLLECTIONS_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGridgridstatecollections_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDGRIDSTATECOLLECTIONS_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Rows), 6, 0, ".", "")));
            Gridgridstatecollections_empowerer_Gridinternalname = cgiGet( "GRIDGRIDSTATECOLLECTIONS_EMPOWERER_Gridinternalname");
            nRC_GXsfl_15 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_15"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nGXsfl_15_fel_idx = 0;
            while ( nGXsfl_15_fel_idx < nRC_GXsfl_15 )
            {
               nGXsfl_15_fel_idx = ((subGridgridstatecollections_Islastpage==1)&&(nGXsfl_15_fel_idx+1>subGridgridstatecollections_fnc_Recordsperpage( )) ? 1 : nGXsfl_15_fel_idx+1);
               sGXsfl_15_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_152( ) ;
               AV17GXV1 = (int)(nGXsfl_15_fel_idx+GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
               if ( ( AV6GridStateCollection.Count >= AV17GXV1 ) && ( AV17GXV1 > 0 ) )
               {
                  AV6GridStateCollection.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1));
                  AV12MoveUp = cgiGet( edtavMoveup_Internalname);
                  AV11MoveDown = cgiGet( edtavMovedown_Internalname);
                  AV13UDelete = cgiGet( edtavUdelete_Internalname);
               }
            }
            if ( nGXsfl_15_fel_idx == 0 )
            {
               nGXsfl_15_idx = 1;
               sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
               SubsflControlProps_152( ) ;
            }
            nGXsfl_15_fel_idx = 1;
            /* Read variables values. */
            AV5CollectionIsEmpty = StringUtil.StrToBool( cgiGet( chkavCollectionisempty_Internalname));
            AssignAttri("", false, "AV5CollectionIsEmpty", AV5CollectionIsEmpty);
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
         E120O2 ();
         if (returnInSub) return;
      }

      protected void E120O2( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         chkavCollectionisempty.Visible = 0;
         AssignProp("", false, chkavCollectionisempty_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavCollectionisempty.Visible), 5, 0), true);
         Gridgridstatecollections_empowerer_Gridinternalname = subGridgridstatecollections_Internalname;
         ucGridgridstatecollections_empowerer.SendProperty(context, "", false, Gridgridstatecollections_empowerer_Internalname, "GridInternalName", Gridgridstatecollections_empowerer_Gridinternalname);
         subGridgridstatecollections_Rows = 0;
         GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Rows), 6, 0, ".", "")));
         if ( StringUtil.StrCmp(AV8HTTPRequest.Method, "GET") == 0 )
         {
            AV6GridStateCollection.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadmanagefiltersstate(context).executeUdp(  AV14UserKey), null, "Items", "");
            gx_BV15 = true;
         }
      }

      private void E130O2( )
      {
         /* Gridgridstatecollections_Load Routine */
         returnInSub = false;
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV6GridStateCollection.Count )
         {
            AV6GridStateCollection.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1));
            AV12MoveUp = "<i class=\"fas fa-arrow-up\"></i>";
            AssignAttri("", false, edtavMoveup_Internalname, AV12MoveUp);
            AV11MoveDown = "<i class=\"fas fa-arrow-down\"></i>";
            AssignAttri("", false, edtavMovedown_Internalname, AV11MoveDown);
            AV13UDelete = "<i class=\"fas fa-times\"></i>";
            AssignAttri("", false, edtavUdelete_Internalname, AV13UDelete);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 15;
            }
            if ( ( subGridgridstatecollections_Islastpage == 1 ) || ( subGridgridstatecollections_Rows == 0 ) || ( ( GRIDGRIDSTATECOLLECTIONS_nCurrentRecord >= GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage ) && ( GRIDGRIDSTATECOLLECTIONS_nCurrentRecord < GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage + subGridgridstatecollections_fnc_Recordsperpage( ) ) ) )
            {
               sendrow_152( ) ;
            }
            GRIDGRIDSTATECOLLECTIONS_nEOF = (short)(((GRIDGRIDSTATECOLLECTIONS_nCurrentRecord<GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage+subGridgridstatecollections_fnc_Recordsperpage( )) ? 1 : 0));
            GxWebStd.gx_hidden_field( context, "GRIDGRIDSTATECOLLECTIONS_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDGRIDSTATECOLLECTIONS_nEOF), 1, 0, ".", "")));
            GRIDGRIDSTATECOLLECTIONS_nCurrentRecord = (long)(GRIDGRIDSTATECOLLECTIONS_nCurrentRecord+1);
            if ( isFullAjaxMode( ) && ! bGXsfl_15_Refreshing )
            {
               DoAjaxLoad(15, GridgridstatecollectionsRow);
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E140O2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(StringUtil.Lower( AV14UserKey), "appbookmarks") == 0 )
         {
            edtavGridstatecollection__title_Title = context.GetMessage( "Bookmark", "");
            AssignProp("", false, edtavGridstatecollection__title_Internalname, "Title", edtavGridstatecollection__title_Title, !bGXsfl_15_Refreshing);
            Form.Caption = context.GetMessage( "Bookmark Manager", "");
            AssignProp("", false, "FORM", "Caption", Form.Caption, true);
         }
         else
         {
            edtavGridstatecollection__gridstatexml_Visible = 0;
            AssignProp("", false, edtavGridstatecollection__gridstatexml_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavGridstatecollection__gridstatexml_Visible), 5, 0), !bGXsfl_15_Refreshing);
            edtavGridstatecollection__title_Class = "AttributeRealWidth";
            AssignProp("", false, edtavGridstatecollection__title_Internalname, "Class", edtavGridstatecollection__title_Class, !bGXsfl_15_Refreshing);
         }
         /*  Sending Event outputs  */
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E110O2 ();
         if (returnInSub) return;
      }

      protected void E110O2( )
      {
         AV17GXV1 = (int)(nGXsfl_15_idx+GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
         if ( ( AV17GXV1 > 0 ) && ( AV6GridStateCollection.Count >= AV17GXV1 ) )
         {
            AV6GridStateCollection.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1));
         }
         /* Enter Routine */
         returnInSub = false;
         AV10IsOK = true;
         AV20GXV4 = 1;
         while ( AV20GXV4 <= AV6GridStateCollection.Count )
         {
            AV7GridStateCollectionItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV20GXV4));
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7GridStateCollectionItem.gxTpr_Title)) )
            {
               GX_msglist.addItem(context.GetMessage( "Empty", ""));
               AV10IsOK = false;
               if (true) break;
            }
            AV20GXV4 = (int)(AV20GXV4+1);
         }
         if ( AV10IsOK )
         {
            if ( AV5CollectionIsEmpty )
            {
               AV6GridStateCollection = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item>( context, "Item", "");
               gx_BV15 = true;
            }
            new DesignSystem.Programs.wwpbaseobjects.savemanagefiltersstate(context ).execute(  AV14UserKey,  AV6GridStateCollection.ToXml(false, true, "Items", "")) ;
            if ( StringUtil.StrCmp(StringUtil.Lower( AV14UserKey), "appbookmarks") == 0 )
            {
            }
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6GridStateCollection", AV6GridStateCollection);
         nGXsfl_15_bak_idx = nGXsfl_15_idx;
         gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         nGXsfl_15_idx = nGXsfl_15_bak_idx;
         sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
         SubsflControlProps_152( ) ;
      }

      protected void E150O2( )
      {
         AV17GXV1 = (int)(nGXsfl_15_idx+GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
         if ( ( AV17GXV1 > 0 ) && ( AV6GridStateCollection.Count >= AV17GXV1 ) )
         {
            AV6GridStateCollection.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1));
         }
         /* Moveup_Click Routine */
         returnInSub = false;
         AV9i = (short)(AV6GridStateCollection.IndexOf(((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)(AV6GridStateCollection.CurrentItem))));
         if ( AV9i > 1 )
         {
            AV7GridStateCollectionItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV9i));
            AV6GridStateCollection.RemoveItem(AV9i);
            gx_BV15 = true;
            AV6GridStateCollection.Add(AV7GridStateCollectionItem, AV9i-1);
            gx_BV15 = true;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6GridStateCollection", AV6GridStateCollection);
         nGXsfl_15_bak_idx = nGXsfl_15_idx;
         gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         nGXsfl_15_idx = nGXsfl_15_bak_idx;
         sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
         SubsflControlProps_152( ) ;
      }

      protected void E160O2( )
      {
         AV17GXV1 = (int)(nGXsfl_15_idx+GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
         if ( ( AV17GXV1 > 0 ) && ( AV6GridStateCollection.Count >= AV17GXV1 ) )
         {
            AV6GridStateCollection.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1));
         }
         /* Movedown_Click Routine */
         returnInSub = false;
         AV9i = (short)(AV6GridStateCollection.IndexOf(((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)(AV6GridStateCollection.CurrentItem))));
         if ( AV9i < AV6GridStateCollection.Count )
         {
            AV7GridStateCollectionItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV9i));
            AV6GridStateCollection.RemoveItem(AV9i);
            gx_BV15 = true;
            AV6GridStateCollection.Add(AV7GridStateCollectionItem, AV9i+1);
            gx_BV15 = true;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6GridStateCollection", AV6GridStateCollection);
         nGXsfl_15_bak_idx = nGXsfl_15_idx;
         gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         nGXsfl_15_idx = nGXsfl_15_bak_idx;
         sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
         SubsflControlProps_152( ) ;
      }

      protected void E170O2( )
      {
         AV17GXV1 = (int)(nGXsfl_15_idx+GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage);
         if ( ( AV17GXV1 > 0 ) && ( AV6GridStateCollection.Count >= AV17GXV1 ) )
         {
            AV6GridStateCollection.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1));
         }
         /* Udelete_Click Routine */
         returnInSub = false;
         AV9i = (short)(AV6GridStateCollection.IndexOf(((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)(AV6GridStateCollection.CurrentItem))));
         AV6GridStateCollection.RemoveItem(AV9i);
         gx_BV15 = true;
         if ( AV6GridStateCollection.Count == 0 )
         {
            AV5CollectionIsEmpty = true;
            AssignAttri("", false, "AV5CollectionIsEmpty", AV5CollectionIsEmpty);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6GridStateCollection", AV6GridStateCollection);
         nGXsfl_15_bak_idx = nGXsfl_15_idx;
         gxgrGridgridstatecollections_refresh( subGridgridstatecollections_Rows, AV14UserKey, AV5CollectionIsEmpty) ;
         nGXsfl_15_idx = nGXsfl_15_bak_idx;
         sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
         SubsflControlProps_152( ) ;
      }

      protected void wb_table1_31_0O2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblUtpaneldummy_Internalname, tblUtpaneldummy_Internalname, "", "Invisible", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tbody>") ;
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* User Defined Control */
            ucDvelop_bootstrap_panel1.SetProperty("Title", Dvelop_bootstrap_panel1_Title);
            ucDvelop_bootstrap_panel1.Render(context, "dvelop.bootstrap.panel", Dvelop_bootstrap_panel1_Internalname, "DVELOP_BOOTSTRAP_PANEL1Container");
            context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVELOP_BOOTSTRAP_PANEL1Container"+"Body"+"\" style=\"display:none;\">") ;
            context.WriteHtmlText( "</div>") ;
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            context.WriteHtmlText( "</tbody>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_31_0O2e( true) ;
         }
         else
         {
            wb_table1_31_0O2e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV14UserKey = (string)getParm(obj,0);
         AssignAttri("", false, "AV14UserKey", AV14UserKey);
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERKEY", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV14UserKey, "")), context));
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
         PA0O2( ) ;
         WS0O2( ) ;
         WE0O2( ) ;
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
         AddStyleSheetFile("DVelop/Bootstrap/Shared/bootstrap.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202412162348953", true, true);
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
         context.AddJavascriptSource("wwpbaseobjects/managefilters.js", "?202412162348954", false, true);
         context.AddJavascriptSource("shared/jquery/jquery1.9.1.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/bootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_152( )
      {
         edtavMoveup_Internalname = "vMOVEUP_"+sGXsfl_15_idx;
         edtavMovedown_Internalname = "vMOVEDOWN_"+sGXsfl_15_idx;
         edtavGridstatecollection__title_Internalname = "GRIDSTATECOLLECTION__TITLE_"+sGXsfl_15_idx;
         edtavGridstatecollection__gridstatexml_Internalname = "GRIDSTATECOLLECTION__GRIDSTATEXML_"+sGXsfl_15_idx;
         edtavUdelete_Internalname = "vUDELETE_"+sGXsfl_15_idx;
      }

      protected void SubsflControlProps_fel_152( )
      {
         edtavMoveup_Internalname = "vMOVEUP_"+sGXsfl_15_fel_idx;
         edtavMovedown_Internalname = "vMOVEDOWN_"+sGXsfl_15_fel_idx;
         edtavGridstatecollection__title_Internalname = "GRIDSTATECOLLECTION__TITLE_"+sGXsfl_15_fel_idx;
         edtavGridstatecollection__gridstatexml_Internalname = "GRIDSTATECOLLECTION__GRIDSTATEXML_"+sGXsfl_15_fel_idx;
         edtavUdelete_Internalname = "vUDELETE_"+sGXsfl_15_fel_idx;
      }

      protected void sendrow_152( )
      {
         sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
         SubsflControlProps_152( ) ;
         WB0O0( ) ;
         if ( ( subGridgridstatecollections_Rows * 1 == 0 ) || ( nGXsfl_15_idx <= subGridgridstatecollections_fnc_Recordsperpage( ) * 1 ) )
         {
            GridgridstatecollectionsRow = GXWebRow.GetNew(context,GridgridstatecollectionsContainer);
            if ( subGridgridstatecollections_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGridgridstatecollections_Backstyle = 0;
               if ( StringUtil.StrCmp(subGridgridstatecollections_Class, "") != 0 )
               {
                  subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"Odd";
               }
            }
            else if ( subGridgridstatecollections_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGridgridstatecollections_Backstyle = 0;
               subGridgridstatecollections_Backcolor = subGridgridstatecollections_Allbackcolor;
               if ( StringUtil.StrCmp(subGridgridstatecollections_Class, "") != 0 )
               {
                  subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"Uniform";
               }
            }
            else if ( subGridgridstatecollections_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGridgridstatecollections_Backstyle = 1;
               if ( StringUtil.StrCmp(subGridgridstatecollections_Class, "") != 0 )
               {
                  subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"Odd";
               }
               subGridgridstatecollections_Backcolor = (int)(0x0);
            }
            else if ( subGridgridstatecollections_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGridgridstatecollections_Backstyle = 1;
               if ( ((int)((nGXsfl_15_idx) % (2))) == 0 )
               {
                  subGridgridstatecollections_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridgridstatecollections_Class, "") != 0 )
                  {
                     subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"Even";
                  }
               }
               else
               {
                  subGridgridstatecollections_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGridgridstatecollections_Class, "") != 0 )
                  {
                     subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"Odd";
                  }
               }
            }
            if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"GridNoBorder WorkWith"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_15_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'" + sGXsfl_15_idx + "',15)\"";
            ROClassString = "Attribute";
            GridgridstatecollectionsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavMoveup_Internalname,StringUtil.RTrim( AV12MoveUp),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\"","'"+""+"'"+",false,"+"'"+"EVMOVEUP.CLICK."+sGXsfl_15_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavMoveup_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(short)-1,(int)edtavMoveup_Enabled,(short)0,(string)"text",(string)"",(short)30,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)15,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'" + sGXsfl_15_idx + "',15)\"";
            ROClassString = "Attribute";
            GridgridstatecollectionsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavMovedown_Internalname,StringUtil.RTrim( AV11MoveDown),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"","'"+""+"'"+",false,"+"'"+"EVMOVEDOWN.CLICK."+sGXsfl_15_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavMovedown_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(short)-1,(int)edtavMovedown_Enabled,(short)0,(string)"text",(string)"",(short)30,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)15,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'" + sGXsfl_15_idx + "',15)\"";
            ROClassString = edtavGridstatecollection__title_Class;
            GridgridstatecollectionsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridstatecollection__title_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1)).gxTpr_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavGridstatecollection__title_Jsonclick,(short)0,(string)edtavGridstatecollection__title_Class,(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)-1,(short)1,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)100,(short)0,(short)0,(short)15,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavGridstatecollection__gridstatexml_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'" + sGXsfl_15_idx + "',15)\"";
            ROClassString = "AttributeRealWidth";
            GridgridstatecollectionsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavGridstatecollection__gridstatexml_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1)).gxTpr_Gridstatexml,((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV6GridStateCollection.Item(AV17GXV1)).gxTpr_Gridstatexml,TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavGridstatecollection__gridstatexml_Jsonclick,(short)0,(string)"AttributeRealWidth",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(int)edtavGridstatecollection__gridstatexml_Visible,(short)1,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(int)2097152,(short)0,(short)0,(short)15,(short)0,(short)0,(short)-1,(bool)true,(string)"",(string)"start",(bool)false,(string)""});
            /* Subfile cell */
            if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'',false,'" + sGXsfl_15_idx + "',15)\"";
            ROClassString = "Attribute";
            GridgridstatecollectionsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavUdelete_Internalname,StringUtil.RTrim( AV13UDelete),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,20);\"","'"+""+"'"+",false,"+"'"+"EVUDELETE.CLICK."+sGXsfl_15_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavUdelete_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(short)-1,(int)edtavUdelete_Enabled,(short)0,(string)"text",(string)"",(short)30,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)15,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes0O2( ) ;
            GridgridstatecollectionsContainer.AddRow(GridgridstatecollectionsRow);
            nGXsfl_15_idx = ((subGridgridstatecollections_Islastpage==1)&&(nGXsfl_15_idx+1>subGridgridstatecollections_fnc_Recordsperpage( )) ? 1 : nGXsfl_15_idx+1);
            sGXsfl_15_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_15_idx), 4, 0), 4, "0");
            SubsflControlProps_152( ) ;
         }
         /* End function sendrow_152 */
      }

      protected void init_web_controls( )
      {
         chkavCollectionisempty.Name = "vCOLLECTIONISEMPTY";
         chkavCollectionisempty.WebTags = "";
         chkavCollectionisempty.Caption = "";
         AssignProp("", false, chkavCollectionisempty_Internalname, "TitleCaption", chkavCollectionisempty.Caption, true);
         chkavCollectionisempty.CheckedValue = "false";
         AV5CollectionIsEmpty = StringUtil.StrToBool( StringUtil.BoolToStr( AV5CollectionIsEmpty));
         AssignAttri("", false, "AV5CollectionIsEmpty", AV5CollectionIsEmpty);
         /* End function init_web_controls */
      }

      protected void StartGridControl15( )
      {
         if ( GridgridstatecollectionsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridgridstatecollectionsContainer"+"DivS\" data-gxgridid=\"15\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridgridstatecollections_Internalname, subGridgridstatecollections_Internalname, "", "GridNoBorder WorkWith", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridgridstatecollections_Backcolorstyle == 0 )
            {
               subGridgridstatecollections_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridgridstatecollections_Class) > 0 )
               {
                  subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"Title";
               }
            }
            else
            {
               subGridgridstatecollections_Titlebackstyle = 1;
               if ( subGridgridstatecollections_Backcolorstyle == 1 )
               {
                  subGridgridstatecollections_Titlebackcolor = subGridgridstatecollections_Allbackcolor;
                  if ( StringUtil.Len( subGridgridstatecollections_Class) > 0 )
                  {
                     subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridgridstatecollections_Class) > 0 )
                  {
                     subGridgridstatecollections_Linesclass = subGridgridstatecollections_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" width="+StringUtil.LTrimStr( (decimal)(30), 4, 0)+"px"+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" width="+StringUtil.LTrimStr( (decimal)(30), 4, 0)+"px"+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+edtavGridstatecollection__title_Class+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( edtavGridstatecollection__title_Title) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"AttributeRealWidth"+"\" "+" style=\""+((edtavGridstatecollection__gridstatexml_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "URL", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" width="+StringUtil.LTrimStr( (decimal)(30), 4, 0)+"px"+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridgridstatecollectionsContainer.AddObjectProperty("GridName", "Gridgridstatecollections");
         }
         else
         {
            GridgridstatecollectionsContainer.AddObjectProperty("GridName", "Gridgridstatecollections");
            GridgridstatecollectionsContainer.AddObjectProperty("Header", subGridgridstatecollections_Header);
            GridgridstatecollectionsContainer.AddObjectProperty("Class", "GridNoBorder WorkWith");
            GridgridstatecollectionsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Backcolorstyle), 1, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("CmpContext", "");
            GridgridstatecollectionsContainer.AddObjectProperty("InMasterPage", "false");
            GridgridstatecollectionsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgridstatecollectionsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV12MoveUp)));
            GridgridstatecollectionsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavMoveup_Enabled), 5, 0, ".", "")));
            GridgridstatecollectionsContainer.AddColumnProperties(GridgridstatecollectionsColumn);
            GridgridstatecollectionsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgridstatecollectionsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV11MoveDown)));
            GridgridstatecollectionsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavMovedown_Enabled), 5, 0, ".", "")));
            GridgridstatecollectionsContainer.AddColumnProperties(GridgridstatecollectionsColumn);
            GridgridstatecollectionsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgridstatecollectionsColumn.AddObjectProperty("Title", StringUtil.RTrim( edtavGridstatecollection__title_Title));
            GridgridstatecollectionsColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavGridstatecollection__title_Class));
            GridgridstatecollectionsContainer.AddColumnProperties(GridgridstatecollectionsColumn);
            GridgridstatecollectionsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgridstatecollectionsColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavGridstatecollection__gridstatexml_Visible), 5, 0, ".", "")));
            GridgridstatecollectionsContainer.AddColumnProperties(GridgridstatecollectionsColumn);
            GridgridstatecollectionsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgridstatecollectionsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV13UDelete)));
            GridgridstatecollectionsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavUdelete_Enabled), 5, 0, ".", "")));
            GridgridstatecollectionsContainer.AddColumnProperties(GridgridstatecollectionsColumn);
            GridgridstatecollectionsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Selectedindex), 4, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Allowselection), 1, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Selectioncolor), 9, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Allowhovering), 1, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Hoveringcolor), 9, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Allowcollapsing), 1, 0, ".", "")));
            GridgridstatecollectionsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgridstatecollections_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavMoveup_Internalname = "vMOVEUP";
         edtavMovedown_Internalname = "vMOVEDOWN";
         edtavGridstatecollection__title_Internalname = "GRIDSTATECOLLECTION__TITLE";
         edtavGridstatecollection__gridstatexml_Internalname = "GRIDSTATECOLLECTION__GRIDSTATEXML";
         edtavUdelete_Internalname = "vUDELETE";
         divTablecontent_Internalname = "TABLECONTENT";
         bttBtnenter_Internalname = "BTNENTER";
         bttBtncancel_Internalname = "BTNCANCEL";
         Dvelop_bootstrap_panel1_Internalname = "DVELOP_BOOTSTRAP_PANEL1";
         tblUtpaneldummy_Internalname = "UTPANELDUMMY";
         divHtml_usertable_utpaneldummy_Internalname = "HTML_USERTABLE_UTPANELDUMMY";
         divTablemain_Internalname = "TABLEMAIN";
         chkavCollectionisempty_Internalname = "vCOLLECTIONISEMPTY";
         Gridgridstatecollections_empowerer_Internalname = "GRIDGRIDSTATECOLLECTIONS_EMPOWERER";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subGridgridstatecollections_Internalname = "GRIDGRIDSTATECOLLECTIONS";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridgridstatecollections_Allowcollapsing = 0;
         subGridgridstatecollections_Allowselection = 0;
         subGridgridstatecollections_Header = "";
         edtavGridstatecollection__title_Title = context.GetMessage( "WWP_FiltersColumnName", "");
         chkavCollectionisempty.Caption = "";
         edtavUdelete_Jsonclick = "";
         edtavUdelete_Enabled = 1;
         edtavGridstatecollection__gridstatexml_Jsonclick = "";
         edtavGridstatecollection__gridstatexml_Visible = -1;
         edtavGridstatecollection__title_Jsonclick = "";
         edtavGridstatecollection__title_Class = "Attribute";
         edtavMovedown_Jsonclick = "";
         edtavMovedown_Enabled = 1;
         edtavMoveup_Jsonclick = "";
         edtavMoveup_Enabled = 1;
         subGridgridstatecollections_Class = "GridNoBorder WorkWith";
         subGridgridstatecollections_Backcolorstyle = 0;
         Dvelop_bootstrap_panel1_Title = "";
         edtavGridstatecollection__title_Class = "Attribute";
         edtavGridstatecollection__gridstatexml_Visible = -1;
         edtavGridstatecollection__title_Title = context.GetMessage( "WWP_FiltersColumnName", "");
         chkavCollectionisempty.Visible = 1;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "WWP_ManageFiltersDescription", "");
         subGridgridstatecollections_Rows = 0;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Title"},{"ctrl":"FORM","prop":"Caption"},{"ctrl":"GRIDSTATECOLLECTION__GRIDSTATEXML","prop":"Visible"},{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Class"}]}""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS.LOAD","""{"handler":"E130O2","iparms":[]""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS.LOAD",""","oparms":[{"av":"AV12MoveUp","fld":"vMOVEUP"},{"av":"AV11MoveDown","fld":"vMOVEDOWN"},{"av":"AV13UDelete","fld":"vUDELETE"}]}""");
         setEventMetadata("ENTER","""{"handler":"E110O2","iparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15}]}""");
         setEventMetadata("VMOVEUP.CLICK","""{"handler":"E150O2","iparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true}]""");
         setEventMetadata("VMOVEUP.CLICK",""","oparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15}]}""");
         setEventMetadata("VMOVEDOWN.CLICK","""{"handler":"E160O2","iparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true}]""");
         setEventMetadata("VMOVEDOWN.CLICK",""","oparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15}]}""");
         setEventMetadata("VUDELETE.CLICK","""{"handler":"E170O2","iparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true}]""");
         setEventMetadata("VUDELETE.CLICK",""","oparms":[{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"}]}""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_FIRSTPAGE","""{"handler":"subgridgridstatecollections_firstpage","iparms":[{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"}]""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_FIRSTPAGE",""","oparms":[{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Title"},{"ctrl":"FORM","prop":"Caption"},{"ctrl":"GRIDSTATECOLLECTION__GRIDSTATEXML","prop":"Visible"},{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Class"}]}""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_PREVPAGE","""{"handler":"subgridgridstatecollections_previouspage","iparms":[{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"}]""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_PREVPAGE",""","oparms":[{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Title"},{"ctrl":"FORM","prop":"Caption"},{"ctrl":"GRIDSTATECOLLECTION__GRIDSTATEXML","prop":"Visible"},{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Class"}]}""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_NEXTPAGE","""{"handler":"subgridgridstatecollections_nextpage","iparms":[{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"}]""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_NEXTPAGE",""","oparms":[{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Title"},{"ctrl":"FORM","prop":"Caption"},{"ctrl":"GRIDSTATECOLLECTION__GRIDSTATEXML","prop":"Visible"},{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Class"}]}""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_LASTPAGE","""{"handler":"subgridgridstatecollections_lastpage","iparms":[{"av":"GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage"},{"av":"GRIDGRIDSTATECOLLECTIONS_nEOF"},{"av":"AV6GridStateCollection","fld":"vGRIDSTATECOLLECTION","grid":15},{"av":"nGXsfl_15_idx","ctrl":"GRID","prop":"GridCurrRow","grid":15},{"av":"nRC_GXsfl_15","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"GridRC","grid":15},{"av":"subGridgridstatecollections_Rows","ctrl":"GRIDGRIDSTATECOLLECTIONS","prop":"Rows"},{"av":"AV14UserKey","fld":"vUSERKEY","hsh":true},{"av":"AV5CollectionIsEmpty","fld":"vCOLLECTIONISEMPTY"}]""");
         setEventMetadata("GRIDGRIDSTATECOLLECTIONS_LASTPAGE",""","oparms":[{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Title"},{"ctrl":"FORM","prop":"Caption"},{"ctrl":"GRIDSTATECOLLECTION__GRIDSTATEXML","prop":"Visible"},{"ctrl":"GRIDSTATECOLLECTION__TITLE","prop":"Class"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Udelete","iparms":[]}""");
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
         wcpOAV14UserKey = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV6GridStateCollection = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item>( context, "Item", "");
         Gridgridstatecollections_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         StyleString = "";
         GridgridstatecollectionsContainer = new GXWebGrid( context);
         sStyleString = "";
         TempTags = "";
         bttBtnenter_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         ucGridgridstatecollections_empowerer = new GXUserControl();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV12MoveUp = "";
         AV11MoveDown = "";
         AV13UDelete = "";
         GXDecQS = "";
         AV8HTTPRequest = new GxHttpRequest( context);
         GridgridstatecollectionsRow = new GXWebRow();
         AV7GridStateCollectionItem = new DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item(context);
         ucDvelop_bootstrap_panel1 = new GXUserControl();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridgridstatecollections_Linesclass = "";
         ROClassString = "";
         GridgridstatecollectionsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavMoveup_Enabled = 0;
         edtavMovedown_Enabled = 0;
         edtavUdelete_Enabled = 0;
      }

      private short GRIDGRIDSTATECOLLECTIONS_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short subGridgridstatecollections_Backcolorstyle ;
      private short AV9i ;
      private short nGXWrapped ;
      private short subGridgridstatecollections_Backstyle ;
      private short subGridgridstatecollections_Titlebackstyle ;
      private short subGridgridstatecollections_Allowselection ;
      private short subGridgridstatecollections_Allowhovering ;
      private short subGridgridstatecollections_Allowcollapsing ;
      private short subGridgridstatecollections_Collapsed ;
      private int nRC_GXsfl_15 ;
      private int subGridgridstatecollections_Rows ;
      private int nGXsfl_15_idx=1 ;
      private int AV17GXV1 ;
      private int subGridgridstatecollections_Islastpage ;
      private int edtavMoveup_Enabled ;
      private int edtavMovedown_Enabled ;
      private int edtavUdelete_Enabled ;
      private int GRIDGRIDSTATECOLLECTIONS_nGridOutOfScope ;
      private int nGXsfl_15_fel_idx=1 ;
      private int edtavGridstatecollection__gridstatexml_Visible ;
      private int AV20GXV4 ;
      private int nGXsfl_15_bak_idx=1 ;
      private int idxLst ;
      private int subGridgridstatecollections_Backcolor ;
      private int subGridgridstatecollections_Allbackcolor ;
      private int subGridgridstatecollections_Titlebackcolor ;
      private int subGridgridstatecollections_Selectedindex ;
      private int subGridgridstatecollections_Selectioncolor ;
      private int subGridgridstatecollections_Hoveringcolor ;
      private long GRIDGRIDSTATECOLLECTIONS_nFirstRecordOnPage ;
      private long GRIDGRIDSTATECOLLECTIONS_nCurrentRecord ;
      private long GRIDGRIDSTATECOLLECTIONS_nRecordCount ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_15_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string Gridgridstatecollections_empowerer_Gridinternalname ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string divTablecontent_Internalname ;
      private string sStyleString ;
      private string subGridgridstatecollections_Internalname ;
      private string TempTags ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Jsonclick ;
      private string divHtml_usertable_utpaneldummy_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string chkavCollectionisempty_Internalname ;
      private string Gridgridstatecollections_empowerer_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV12MoveUp ;
      private string edtavMoveup_Internalname ;
      private string AV11MoveDown ;
      private string edtavMovedown_Internalname ;
      private string AV13UDelete ;
      private string edtavUdelete_Internalname ;
      private string GXDecQS ;
      private string sGXsfl_15_fel_idx="0001" ;
      private string edtavGridstatecollection__title_Title ;
      private string edtavGridstatecollection__title_Internalname ;
      private string edtavGridstatecollection__gridstatexml_Internalname ;
      private string edtavGridstatecollection__title_Class ;
      private string tblUtpaneldummy_Internalname ;
      private string Dvelop_bootstrap_panel1_Title ;
      private string Dvelop_bootstrap_panel1_Internalname ;
      private string subGridgridstatecollections_Class ;
      private string subGridgridstatecollections_Linesclass ;
      private string ROClassString ;
      private string edtavMoveup_Jsonclick ;
      private string edtavMovedown_Jsonclick ;
      private string edtavGridstatecollection__title_Jsonclick ;
      private string edtavGridstatecollection__gridstatexml_Jsonclick ;
      private string edtavUdelete_Jsonclick ;
      private string subGridgridstatecollections_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV5CollectionIsEmpty ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_15_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV15 ;
      private bool gx_refresh_fired ;
      private bool AV10IsOK ;
      private string AV14UserKey ;
      private string wcpOAV14UserKey ;
      private GXWebGrid GridgridstatecollectionsContainer ;
      private GXWebRow GridgridstatecollectionsRow ;
      private GXWebColumn GridgridstatecollectionsColumn ;
      private GXUserControl ucGridgridstatecollections_empowerer ;
      private GXUserControl ucDvelop_bootstrap_panel1 ;
      private GxHttpRequest AV8HTTPRequest ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkavCollectionisempty ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item> AV6GridStateCollection ;
      private DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item AV7GridStateCollectionItem ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
