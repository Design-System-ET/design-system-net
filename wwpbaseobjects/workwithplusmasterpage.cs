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
   public class workwithplusmasterpage : GXMasterPage
   {
      public workwithplusmasterpage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
      }

      public workwithplusmasterpage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
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
      }

      protected void gxnrFssitemap_newrow_invoke( )
      {
         nRC_GXsfl_71 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_71"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_71_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_71_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_71_idx = GetPar( "sGXsfl_71_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrFssitemap_newrow( ) ;
         /* End function gxnrFssitemap_newrow_invoke */
      }

      protected void gxgrFssitemap_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV23DVelop_Menu);
         AV26Breadcrumb = GetPar( "Breadcrumb");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrFssitemap_refresh( AV23DVelop_Menu, AV26Breadcrumb) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrFssitemap_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            PA2D2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavCaption_Enabled = 0;
               AssignProp("", true, edtavCaption_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCaption_Enabled), 5, 0), !bGXsfl_71_Refreshing);
               WS2D2( ) ;
               if ( ! isAjaxCallMode( ) )
               {
                  WE2D2( ) ;
               }
            }
         }
         cleanup();
      }

      protected void RenderHtmlHeaders( )
      {
         if ( ! isFullAjaxMode( ) )
         {
            GXWebForm.AddResponsiveMetaHeaders((getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Meta);
            getDataAreaObject().RenderHtmlHeaders();
         }
      }

      protected void RenderHtmlOpenForm( )
      {
         if ( ! isFullAjaxMode( ) )
         {
            getDataAreaObject().RenderHtmlOpenForm();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "vBREADCRUMB_MPAGE", AV26Breadcrumb);
         GxWebStd.gx_hidden_field( context, "gxhash_vBREADCRUMB_MPAGE", GetSecureSignedToken( "gxmpage_", StringUtil.RTrim( context.localUtil.Format( AV26Breadcrumb, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_71", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_71), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", true, "vDVELOP_MENU_MPAGE", AV23DVelop_Menu);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDVELOP_MENU_MPAGE", AV23DVelop_Menu);
         }
         GxWebStd.gx_hidden_field( context, "vSEARCHAUX_MPAGE", AV15SearchAux);
         GxWebStd.gx_hidden_field( context, "vBREADCRUMB_MPAGE", AV26Breadcrumb);
         GxWebStd.gx_hidden_field( context, "gxhash_vBREADCRUMB_MPAGE", GetSecureSignedToken( "gxmpage_", StringUtil.RTrim( context.localUtil.Format( AV26Breadcrumb, "")), context));
         GxWebStd.gx_hidden_field( context, "subFssitemap_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "UCMENU_MPAGE_Cls", StringUtil.RTrim( Ucmenu_Cls));
         GxWebStd.gx_hidden_field( context, "UCMENU_MPAGE_Collapsedtitle", StringUtil.RTrim( Ucmenu_Collapsedtitle));
         GxWebStd.gx_hidden_field( context, "UCMENU_MPAGE_Moreoptionenabled", StringUtil.BoolToStr( Ucmenu_Moreoptionenabled));
         GxWebStd.gx_hidden_field( context, "UCMENU_MPAGE_Moreoptioncaption", StringUtil.RTrim( Ucmenu_Moreoptioncaption));
         GxWebStd.gx_hidden_field( context, "UCMENU_MPAGE_Moreoptionicon", StringUtil.RTrim( Ucmenu_Moreoptionicon));
         GxWebStd.gx_hidden_field( context, "DDC_ADMINAG_MPAGE_Icon", StringUtil.RTrim( Ddc_adminag_Icon));
         GxWebStd.gx_hidden_field( context, "DDC_ADMINAG_MPAGE_Caption", StringUtil.RTrim( Ddc_adminag_Caption));
         GxWebStd.gx_hidden_field( context, "DDC_ADMINAG_MPAGE_Cls", StringUtil.RTrim( Ddc_adminag_Cls));
         GxWebStd.gx_hidden_field( context, "DDC_ADMINAG_MPAGE_Componentwidth", StringUtil.LTrim( StringUtil.NToC( (decimal)(Ddc_adminag_Componentwidth), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "UCMESSAGE_MPAGE_Stoponerror", StringUtil.BoolToStr( Ucmessage_Stoponerror));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Enablefixobjectfitcover", StringUtil.BoolToStr( Wwputilities_Enablefixobjectfitcover));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Empowertabs", StringUtil.BoolToStr( Wwputilities_Empowertabs));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Enableupdaterowselectionstatus", StringUtil.BoolToStr( Wwputilities_Enableupdaterowselectionstatus));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Enableconvertcombotobootstrapselect", StringUtil.BoolToStr( Wwputilities_Enableconvertcombotobootstrapselect));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Allowcolumnresizing", StringUtil.BoolToStr( Wwputilities_Allowcolumnresizing));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Allowcolumnreordering", StringUtil.BoolToStr( Wwputilities_Allowcolumnreordering));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Allowcolumndragging", StringUtil.BoolToStr( Wwputilities_Allowcolumndragging));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Allowcolumnsrestore", StringUtil.BoolToStr( Wwputilities_Allowcolumnsrestore));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Pagbarincludegoto", StringUtil.BoolToStr( Wwputilities_Pagbarincludegoto));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_MPAGE_Comboloadtype", StringUtil.RTrim( Wwputilities_Comboloadtype));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Iteminternalname", StringUtil.RTrim( Popover_search_Iteminternalname));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Trigger", StringUtil.RTrim( Popover_search_Trigger));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Triggerelement", StringUtil.RTrim( Popover_search_Triggerelement));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Popoverwidth", StringUtil.LTrim( StringUtil.NToC( (decimal)(Popover_search_Popoverwidth), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Position", StringUtil.RTrim( Popover_search_Position));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Keepopened", StringUtil.BoolToStr( Popover_search_Keepopened));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Reloadonkeychange", StringUtil.BoolToStr( Popover_search_Reloadonkeychange));
         GxWebStd.gx_hidden_field( context, "POPOVER_SEARCH_MPAGE_Minimumcharacters", StringUtil.LTrim( StringUtil.NToC( (decimal)(Popover_search_Minimumcharacters), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "FSSITEMAP_MPAGE_Class", StringUtil.RTrim( subFssitemap_Class));
         GxWebStd.gx_hidden_field( context, "FSSITEMAP_MPAGE_Flexdirection", StringUtil.RTrim( subFssitemap_Flexdirection));
      }

      protected void RenderHtmlCloseForm2D2( )
      {
         SendCloseFormHiddens( ) ;
         SendSecurityToken((string)(sPrefix));
         if ( ! isFullAjaxMode( ) )
         {
            getDataAreaObject().RenderHtmlCloseForm();
         }
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( ! ( WebComp_Wwpaux_wc == null ) )
         {
            WebComp_Wwpaux_wc.componentjscripts();
         }
         context.AddJavascriptSource("calendar.js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("calendar-setup.js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("calendar-"+StringUtil.Substring( context.GetLanguageProperty( "culture"), 1, 2)+".js", "?"+context.GetBuildNumber( 1318140), false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/slimmenu/jquery.slimmenu.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVHorizontalMenu/DVHorizontalMenuRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVMessage/pnotify.custom.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVMessage/DVMessageRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Tooltip/BootstrapTooltipRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Mask/jquery.mask.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/BootstrapSelect.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/WorkWithPlusUtilitiesRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/locales.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/wwp-daterangepicker.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/moment.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/daterangepicker.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DatePicker/DatePickerRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Popover/WWPPopoverRender.js", "", false, true);
         context.AddJavascriptSource("wwpbaseobjects/workwithplusmasterpage.js", "?20241217053970", false, true);
         context.WriteHtmlTextNl( "</body>") ;
         context.WriteHtmlTextNl( "</html>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
      }

      public override string GetPgmname( )
      {
         return "WWPBaseObjects.WorkWithPlusMasterPage" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Master Page", "") ;
      }

      protected void WB2D0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            RenderHtmlHeaders( ) ;
            RenderHtmlOpenForm( ) ;
            if ( ! ShowMPWhenPopUp( ) && context.isPopUpObject( ) )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableOutput();
               }
               if ( context.isSpaRequest( ) )
               {
                  disableJsOutput();
               }
               /* Content placeholder */
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gx-content-placeholder");
               context.WriteHtmlText( ">") ;
               if ( ! isFullAjaxMode( ) )
               {
                  getDataAreaObject().RenderHtmlContent();
               }
               context.WriteHtmlText( "</div>") ;
               if ( context.isSpaRequest( ) )
               {
                  disableOutput();
               }
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
               wbLoad = true;
               return  ;
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 LandingPurusHeaderCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheaderfixwidth_Internalname, 1, 0, "px", 0, "px", "LandingPurusHeader", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheadercell_Internalname, 1, 0, "px", 0, "px", divTableheadercell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableheader_Internalname, 1, 0, "px", 0, "px", "TableHeaderLandingPurus", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "hidden-xs", "start", "top", "", "align-self:flex-start;", "div");
            /* Static images/pictures */
            ClassString = "ImageTop" + " " + ((StringUtil.StrCmp(imgLogoheader_gximage, "")==0) ? "GX_Image_LogoLogin_Class" : "GX_Image_"+imgLogoheader_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "e9edf59f-db45-4e16-b6a6-2c2b6611a4a3", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgLogoheader_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divLogostickymenucell_Internalname, 1, 0, "px", 0, "px", "hidden-xs", "start", "top", "", "flex-grow:1;align-self:flex-start;", "div");
            /* Static images/pictures */
            ClassString = "ImageTop LogoVisibleStickyMenu" + " " + ((StringUtil.StrCmp(imgLogoforstickymenu_gximage, "")==0) ? "GX_Image_LogoLogin_Class" : "GX_Image_"+imgLogoforstickymenu_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "e9edf59f-db45-4e16-b6a6-2c2b6611a4a3", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgLogoforstickymenu_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellHeaderBar", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableuserrole_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "align-items:flex-end;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "HorizontalMenuCell CellPaddingLeft30", "start", "top", "", "flex-grow:1;", "div");
            /* User Defined Control */
            ucUcmenu.SetProperty("Cls", Ucmenu_Cls);
            ucUcmenu.SetProperty("Menu", AV23DVelop_Menu);
            ucUcmenu.SetProperty("CollapsedTitle", Ucmenu_Collapsedtitle);
            ucUcmenu.SetProperty("MoreOptionEnabled", Ucmenu_Moreoptionenabled);
            ucUcmenu.SetProperty("MoreOptionCaption", Ucmenu_Moreoptioncaption);
            ucUcmenu.SetProperty("MoreOptionIcon", Ucmenu_Moreoptionicon);
            ucUcmenu.Render(context, "dvelop.dvhorizontalmenu", Ucmenu_Internalname, "UCMENU_MPAGEContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "hidden-xs", "start", "top", "", "align-self:flex-end;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divToprightfixedelements_Internalname, 1, 0, "px", 0, "px", divToprightfixedelements_Class, "start", "top", " "+"data-gx-flex"+" ", "justify-content:flex-end;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellAttributeSearch", "start", "top", "", "align-self:center;", "div");
            wb_table1_24_2D2( true) ;
         }
         else
         {
            wb_table1_24_2D2( false) ;
         }
         return  ;
      }

      protected void wb_table1_24_2D2e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "MasterTopIconsCell", "start", "top", "", "", "div");
            /* User Defined Control */
            ucDdc_adminag.SetProperty("Caption", Ddc_adminag_Caption);
            ucDdc_adminag.SetProperty("Cls", Ddc_adminag_Cls);
            ucDdc_adminag.SetProperty("ComponentWidth", Ddc_adminag_Componentwidth);
            ucDdc_adminag.Render(context, "dvelop.gxbootstrap.ddcomponent", Ddc_adminag_Internalname, "DDC_ADMINAG_MPAGEContainer");
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
            GxWebStd.gx_div_start( context, divTablecontent_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellTableContentHorizontalMenu", "start", "top", "", "", "div");
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            /* Content placeholder */
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-content-placeholder");
            context.WriteHtmlText( ">") ;
            if ( ! isFullAjaxMode( ) )
            {
               getDataAreaObject().RenderHtmlContent();
            }
            context.WriteHtmlText( "</div>") ;
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divFootercell_Internalname, divFootercell_Visible, 0, "px", 0, "px", "col-xs-12 FooterBlackCell", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divFooter_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-wrap:wrap;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellMarginTop80", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "LandingPurusFooterCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "LandingPurusMoreInfoContact" + " " + ((StringUtil.StrCmp(imgMoreinfocontact_gximage, "")==0) ? "GX_Image_WWPBaseObjects_InfoContact_Class" : "GX_Image_"+imgMoreinfocontact_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "5f4c6265-bfeb-4581-9e8d-2d91397c35b3", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgMoreinfocontact_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellMarginTop80", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "LandingPurusFooterCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblContacttitle_Internalname, context.GetMessage( "Contacto", ""), "", "", lblContacttitle_Jsonclick, "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "LandingPurusFooterTitle", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblAddress1_Internalname, context.GetMessage( "Chuy - Rocha", ""), "", "", lblAddress1_Jsonclick, "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "LandingPurusFooterText", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTelephone_Internalname, "+598 91 388 175", "", "", lblTelephone_Jsonclick, "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "LandingPurusFooterText", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblEmail_Internalname, context.GetMessage( "design-system@gmail.com", ""), "", "", lblEmail_Jsonclick, "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "LandingPurusFooterText", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblFormcontact_Internalname, context.GetMessage( "Formulario Contacto", ""), "", "", lblFormcontact_Jsonclick, "'"+""+"'"+",true,"+"'"+"ECONTACTO_MPAGE."+"'", "", "LandingPurusFooterText", 5, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellMarginTop80", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "LandingPurusFooterCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblSitemaptitle_Internalname, context.GetMessage( "Mapa del sitio", ""), "", "", lblSitemaptitle_Jsonclick, "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "LandingPurusFooterTitle", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            MPFssitemapContainer.SetIsFreestyle(true);
            MPFssitemapContainer.SetWrapped(nGXWrapped);
            StartGridControl71( ) ;
         }
         if ( ( wbEnd == 71 ) && ( ! context.isPopUpObject( ) || ShowMPWhenPopUp( ) ) )
         {
            wbEnd = 0;
            nRC_GXsfl_71 = (int)(nGXsfl_71_idx-1);
            if ( MPFssitemapContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"MPFssitemapContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Fssitemap", MPFssitemapContainer, subFssitemap_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "MPFssitemapContainerData", MPFssitemapContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "MPFssitemapContainerData"+"V", MPFssitemapContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"MPFssitemapContainerData"+"V"+"\" value='"+MPFssitemapContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellMarginTop80", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable4_Internalname, 1, 0, "px", 0, "px", "LandingPurusFooterCellNoBorder", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblFollowustitle_Internalname, context.GetMessage( "¡Síguenos!", ""), "", "", lblFollowustitle_Jsonclick, "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "LandingPurusFooterTitle", 0, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable5_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblUafacebook_Internalname, context.GetMessage( "<i class=\"fab fa-facebook-f LandingPurusFontIconFooter\"></i>", ""), "", "", lblUafacebook_Jsonclick, "'"+""+"'"+",true,"+"'"+"e112d1_client"+"'", "", "TextBlock", 7, "", 1, 1, 0, 1, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblUainstagram_Internalname, context.GetMessage( "<i class=\"fab fa-instagram LandingPurusFontIconFooter\"></i>", ""), "", "", lblUainstagram_Jsonclick, "'"+""+"'"+",true,"+"'"+"e122d1_client"+"'", "", "TextBlock", 7, "", 1, 1, 0, 1, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblUalinkedin_Internalname, context.GetMessage( "<i class=\"fab fa-linkedin LandingPurusFontIconFooter\"></i>", ""), "", "", lblUalinkedin_Jsonclick, "'"+""+"'"+",true,"+"'"+"e132d1_client"+"'", "", "TextBlock", 7, "", 1, 1, 0, 1, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblUseraction1_Internalname, context.GetMessage( "<i class=\"fab fa-github LandingPurusFontIconFooter\"></i>", ""), "", "", lblUseraction1_Jsonclick, "'"+""+"'"+",true,"+"'"+"e142d1_client"+"'", "", "TextBlock", 7, "", 1, 1, 0, 1, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblUawhatsapp_Internalname, context.GetMessage( "<i class=\"fab fa-whatsapp LandingPurusFontIconFooter\"></i>", ""), "", "", lblUawhatsapp_Jsonclick, "'"+""+"'"+",true,"+"'"+"e152d1_client"+"'", "", "TextBlock", 7, "", 1, 1, 0, 1, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop30", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblPoliticaprivacidad_Internalname, context.GetMessage( "Politica de Privacidad", ""), "", "", lblPoliticaprivacidad_Jsonclick, "'"+""+"'"+",true,"+"'"+"e162d1_client"+"'", "", "TextBlock", 7, "", 1, 1, 0, 0, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucUcmessage.SetProperty("StopOnError", Ucmessage_Stoponerror);
            ucUcmessage.Render(context, "dvelop.dvmessage", Ucmessage_Internalname, "UCMESSAGE_MPAGEContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucUctooltip.Render(context, "dvelop.gxbootstrap.tooltip", Uctooltip_Internalname, "UCTOOLTIP_MPAGEContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucWwputilities.SetProperty("EnableFixObjectFitCover", Wwputilities_Enablefixobjectfitcover);
            ucWwputilities.SetProperty("EmpowerTabs", Wwputilities_Empowertabs);
            ucWwputilities.SetProperty("EnableUpdateRowSelectionStatus", Wwputilities_Enableupdaterowselectionstatus);
            ucWwputilities.SetProperty("EnableConvertComboToBootstrapSelect", Wwputilities_Enableconvertcombotobootstrapselect);
            ucWwputilities.SetProperty("AllowColumnResizing", Wwputilities_Allowcolumnresizing);
            ucWwputilities.SetProperty("AllowColumnReordering", Wwputilities_Allowcolumnreordering);
            ucWwputilities.SetProperty("AllowColumnDragging", Wwputilities_Allowcolumndragging);
            ucWwputilities.SetProperty("AllowColumnsRestore", Wwputilities_Allowcolumnsrestore);
            ucWwputilities.SetProperty("PagBarIncludeGoTo", Wwputilities_Pagbarincludegoto);
            ucWwputilities.SetProperty("ComboLoadType", Wwputilities_Comboloadtype);
            ucWwputilities.Render(context, "wwp.workwithplusutilities_fal", Wwputilities_Internalname, "WWPUTILITIES_MPAGEContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucWwpdatepicker.Render(context, "wwp.datepicker", Wwpdatepicker_Internalname, "WWPDATEPICKER_MPAGEContainer");
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
            ucPopover_search.SetProperty("Trigger", Popover_search_Trigger);
            ucPopover_search.SetProperty("TriggerElement", Popover_search_Triggerelement);
            ucPopover_search.SetProperty("PopoverWidth", Popover_search_Popoverwidth);
            ucPopover_search.SetProperty("Position", Popover_search_Position);
            ucPopover_search.SetProperty("KeepOpened", Popover_search_Keepopened);
            ucPopover_search.SetProperty("ReloadOnKeyChange", Popover_search_Reloadonkeychange);
            ucPopover_search.SetProperty("MinimumCharacters", Popover_search_Minimumcharacters);
            ucPopover_search.Render(context, "dvelop.wwppopover", Popover_search_Internalname, "POPOVER_SEARCH_MPAGEContainer");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 114,'',true,'" + sGXsfl_71_idx + "',0)\"";
            context.WriteHtmlText( "<div id=\""+edtavPickerdummyvariable_Internalname+"_dp_container\" class=\"dp_container\" style=\"white-space:nowrap;display:inline;\">") ;
            GxWebStd.gx_single_line_edit( context, edtavPickerdummyvariable_Internalname, context.localUtil.Format(AV33PickerDummyVariable, "99/99/99"), context.localUtil.Format( AV33PickerDummyVariable, "99/99/99"), TempTags+" onchange=\""+"gx.date.valid_date(this, 8,'"+context.GetLanguageProperty( "date_fmt")+"',0,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.date.valid_date(this, 8,'"+context.GetLanguageProperty( "date_fmt")+"',0,"+context.GetLanguageProperty( "time_fmt")+",'"+context.GetLanguageProperty( "code")+"',false,0);"+";gx.evt.onblur(this,114);\"", "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "", "", "", edtavPickerdummyvariable_Jsonclick, 0, "Invisible", "", "", "", "", 1, 1, 0, "text", "", 8, "chr", 1, "row", 8, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_bitmap( context, edtavPickerdummyvariable_Internalname+"_dp_trigger", context.GetImagePath( "61b9b5d3-dff6-4d59-9b00-da61bc2cbe93", "", context.GetTheme( )), "", "", "", "", ((1==0)||(1==0) ? 0 : 1), 0, "Date selector", "Date selector", 0, 1, 0, "", 0, "", 0, 0, 0, "", "", "cursor: pointer;", "", "", "", "", "", "", "", "", 1, false, false, "", "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            context.WriteHtmlTextNl( "</div>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, divDiv_wwpauxwc_Internalname, 1, 0, "px", 0, "px", "Invisible", "start", "top", "", "", "div");
            if ( ! isFullAjaxMode( ) )
            {
               /* WebComponent */
               GxWebStd.gx_hidden_field( context, "MPW0116"+"", StringUtil.RTrim( WebComp_Wwpaux_wc_Component));
               context.WriteHtmlText( "<div") ;
               GxWebStd.ClassAttribute( context, "gxwebcomponent");
               context.WriteHtmlText( " id=\""+"gxHTMLWrpMPW0116"+""+"\""+"") ;
               context.WriteHtmlText( ">") ;
               if ( bGXsfl_71_Refreshing )
               {
                  if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
                  {
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldWwpaux_wc), StringUtil.Lower( WebComp_Wwpaux_wc_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpMPW0116"+"");
                     }
                     WebComp_Wwpaux_wc.componentdraw();
                     if ( StringUtil.StrCmp(StringUtil.Lower( OldWwpaux_wc), StringUtil.Lower( WebComp_Wwpaux_wc_Component)) != 0 )
                     {
                        context.httpAjaxContext.ajax_rspEndCmp();
                     }
                  }
               }
               context.WriteHtmlText( "</div>") ;
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( ( wbEnd == 71 ) && ( ! context.isPopUpObject( ) || ShowMPWhenPopUp( ) ) )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( MPFssitemapContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"MPFssitemapContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Fssitemap", MPFssitemapContainer, subFssitemap_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "MPFssitemapContainerData", MPFssitemapContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "MPFssitemapContainerData"+"V", MPFssitemapContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"MPFssitemapContainerData"+"V"+"\" value='"+MPFssitemapContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2D2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2D0( ) ;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( getDataAreaObject().ExecuteStartEvent() != 0 )
            {
               setAjaxCallMode();
            }
            if ( context.isSpaRequest( ) )
            {
               enableJsOutput();
            }
         }
      }

      protected void WS2D2( )
      {
         START2D2( ) ;
         EVT2D2( ) ;
      }

      protected void EVT2D2( )
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
                  if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                  {
                     sEvtType = StringUtil.Right( sEvt, 1);
                     if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                     {
                        sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                        if ( StringUtil.StrCmp(sEvt, "RFR_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                        }
                        else if ( StringUtil.StrCmp(sEvt, "DDC_ADMINAG_MPAGE.ONLOADCOMPONENT_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Ddc_adminag.Onloadcomponent */
                           E172D2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "POPOVER_SEARCH_MPAGE.ONLOADCOMPONENT_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Popover_search.Onloadcomponent */
                           E182D2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "DOACTIONSEARCH_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'DoActionSearch' */
                           E192D2 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "CONTACTO_MPAGE") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'Contacto' */
                           E202D2 ();
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
                        if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 11), "START_MPAGE") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 26), "FSSITEMAP_MPAGE.LOAD_MPAGE") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "REFRESH_MPAGE") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 11), "ENTER_MPAGE") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 12), "CANCEL_MPAGE") == 0 ) )
                        {
                           nGXsfl_71_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                           sGXsfl_71_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_71_idx), 4, 0), 4, "0");
                           SubsflControlProps_712( ) ;
                           AV12Caption = cgiGet( edtavCaption_Internalname);
                           AssignAttri("", true, edtavCaption_Internalname, AV12Caption);
                           sEvtType = StringUtil.Right( sEvt, 1);
                           if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                           {
                              sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                              if ( StringUtil.StrCmp(sEvt, "START_MPAGE") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Start */
                                 E212D2 ();
                              }
                              else if ( StringUtil.StrCmp(sEvt, "FSSITEMAP_MPAGE.LOAD_MPAGE") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Fssitemap.Load */
                                 E222D2 ();
                              }
                              else if ( StringUtil.StrCmp(sEvt, "REFRESH_MPAGE") == 0 )
                              {
                                 context.wbHandled = 1;
                                 dynload_actions( ) ;
                                 /* Execute user event: Refresh */
                                 E232D2 ();
                              }
                              else if ( StringUtil.StrCmp(sEvt, "ENTER_MPAGE") == 0 )
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
                              }
                           }
                           else
                           {
                           }
                        }
                     }
                  }
                  else if ( StringUtil.StrCmp(sEvtType, "M") == 0 )
                  {
                     sEvtType = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-2));
                     sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-6));
                     nCmpId = (short)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                     if ( nCmpId == 116 )
                     {
                        OldWwpaux_wc = cgiGet( "MPW0116");
                        if ( ( StringUtil.Len( OldWwpaux_wc) == 0 ) || ( StringUtil.StrCmp(OldWwpaux_wc, WebComp_Wwpaux_wc_Component) != 0 ) )
                        {
                           WebComp_Wwpaux_wc = getWebComponent(GetType(), "DesignSystem.Programs", OldWwpaux_wc, new Object[] {context} );
                           WebComp_Wwpaux_wc.ComponentInit();
                           WebComp_Wwpaux_wc.Name = "OldWwpaux_wc";
                           WebComp_Wwpaux_wc_Component = OldWwpaux_wc;
                        }
                        if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
                        {
                           WebComp_Wwpaux_wc.componentprocess("MPW0116", "", sEvt);
                        }
                        WebComp_Wwpaux_wc_Component = OldWwpaux_wc;
                     }
                  }
                  if ( context.wbHandled == 0 )
                  {
                     getDataAreaObject().DispatchEvents();
                  }
                  context.wbHandled = 1;
               }
            }
         }
      }

      protected void WE2D2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm2D2( ) ;
            }
         }
      }

      protected void PA2D2( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
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
               GX_FocusControl = edtavSearch_Internalname;
               AssignAttri("", true, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrFssitemap_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_712( ) ;
         while ( nGXsfl_71_idx <= nRC_GXsfl_71 )
         {
            sendrow_712( ) ;
            nGXsfl_71_idx = ((subFssitemap_Islastpage==1)&&(nGXsfl_71_idx+1>subFssitemap_fnc_Recordsperpage( )) ? 1 : nGXsfl_71_idx+1);
            sGXsfl_71_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_71_idx), 4, 0), 4, "0");
            SubsflControlProps_712( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( MPFssitemapContainer)) ;
         /* End function gxnrFssitemap_newrow */
      }

      protected void gxgrFssitemap_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> AV23DVelop_Menu ,
                                            string AV26Breadcrumb )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         FSSITEMAP_MPAGE_nCurrentRecord = 0;
         RF2D2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrFssitemap_refresh */
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
         RF2D2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCaption_Enabled = 0;
      }

      protected void RF2D2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( ShowMPWhenPopUp( ) || ! context.isPopUpObject( ) )
         {
            if ( isAjaxCallMode( ) )
            {
               MPFssitemapContainer.ClearRows();
            }
            wbStart = 71;
            /* Execute user event: Refresh */
            E232D2 ();
            nGXsfl_71_idx = 1;
            sGXsfl_71_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_71_idx), 4, 0), 4, "0");
            SubsflControlProps_712( ) ;
            bGXsfl_71_Refreshing = true;
            MPFssitemapContainer.AddObjectProperty("GridName", "Fssitemap");
            MPFssitemapContainer.AddObjectProperty("CmpContext", "");
            MPFssitemapContainer.AddObjectProperty("InMasterPage", "true");
            MPFssitemapContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            MPFssitemapContainer.AddObjectProperty("Class", "FreeStyleGrid");
            MPFssitemapContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Backcolorstyle), 1, 0, ".", "")));
            MPFssitemapContainer.PageSize = subFssitemap_fnc_Recordsperpage( );
            if ( subFssitemap_Islastpage != 0 )
            {
               FSSITEMAP_MPAGE_nFirstRecordOnPage = (long)(subFssitemap_fnc_Recordcount( )-subFssitemap_fnc_Recordsperpage( ));
               GxWebStd.gx_hidden_field( context, "FSSITEMAP_MPAGE_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(FSSITEMAP_MPAGE_nFirstRecordOnPage), 15, 0, ".", "")));
               MPFssitemapContainer.AddObjectProperty("FSSITEMAP_MPAGE_nFirstRecordOnPage", FSSITEMAP_MPAGE_nFirstRecordOnPage);
            }
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
            {
               if ( 1 != 0 )
               {
                  if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
                  {
                     WebComp_Wwpaux_wc.componentstart();
                  }
               }
            }
            gxdyncontrolsrefreshing = true;
            fix_multi_value_controls( ) ;
            gxdyncontrolsrefreshing = false;
         }
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_712( ) ;
            /* Execute user event: Fssitemap.Load */
            E222D2 ();
            wbEnd = 71;
            WB2D0( ) ;
            if ( context.isSpaRequest( ) )
            {
               enableOutput();
            }
         }
         bGXsfl_71_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2D2( )
      {
      }

      protected int subFssitemap_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subFssitemap_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subFssitemap_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subFssitemap_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCaption_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2D0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E212D2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "vDVELOP_MENU_MPAGE"), AV23DVelop_Menu);
            /* Read saved values. */
            nRC_GXsfl_71 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_71"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV15SearchAux = cgiGet( "vSEARCHAUX_MPAGE");
            subFssitemap_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "subFssitemap_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Ucmenu_Cls = cgiGet( "UCMENU_MPAGE_Cls");
            Ucmenu_Collapsedtitle = cgiGet( "UCMENU_MPAGE_Collapsedtitle");
            Ucmenu_Moreoptionenabled = StringUtil.StrToBool( cgiGet( "UCMENU_MPAGE_Moreoptionenabled"));
            Ucmenu_Moreoptioncaption = cgiGet( "UCMENU_MPAGE_Moreoptioncaption");
            Ucmenu_Moreoptionicon = cgiGet( "UCMENU_MPAGE_Moreoptionicon");
            Ddc_adminag_Icon = cgiGet( "DDC_ADMINAG_MPAGE_Icon");
            Ddc_adminag_Caption = cgiGet( "DDC_ADMINAG_MPAGE_Caption");
            Ddc_adminag_Cls = cgiGet( "DDC_ADMINAG_MPAGE_Cls");
            Ddc_adminag_Componentwidth = (int)(Math.Round(context.localUtil.CToN( cgiGet( "DDC_ADMINAG_MPAGE_Componentwidth"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Ucmessage_Stoponerror = StringUtil.StrToBool( cgiGet( "UCMESSAGE_MPAGE_Stoponerror"));
            Wwputilities_Enablefixobjectfitcover = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Enablefixobjectfitcover"));
            Wwputilities_Empowertabs = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Empowertabs"));
            Wwputilities_Enableupdaterowselectionstatus = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Enableupdaterowselectionstatus"));
            Wwputilities_Enableconvertcombotobootstrapselect = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Enableconvertcombotobootstrapselect"));
            Wwputilities_Allowcolumnresizing = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Allowcolumnresizing"));
            Wwputilities_Allowcolumnreordering = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Allowcolumnreordering"));
            Wwputilities_Allowcolumndragging = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Allowcolumndragging"));
            Wwputilities_Allowcolumnsrestore = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Allowcolumnsrestore"));
            Wwputilities_Pagbarincludegoto = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_MPAGE_Pagbarincludegoto"));
            Wwputilities_Comboloadtype = cgiGet( "WWPUTILITIES_MPAGE_Comboloadtype");
            Popover_search_Iteminternalname = cgiGet( "POPOVER_SEARCH_MPAGE_Iteminternalname");
            Popover_search_Trigger = cgiGet( "POPOVER_SEARCH_MPAGE_Trigger");
            Popover_search_Triggerelement = cgiGet( "POPOVER_SEARCH_MPAGE_Triggerelement");
            Popover_search_Popoverwidth = (int)(Math.Round(context.localUtil.CToN( cgiGet( "POPOVER_SEARCH_MPAGE_Popoverwidth"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Popover_search_Position = cgiGet( "POPOVER_SEARCH_MPAGE_Position");
            Popover_search_Keepopened = StringUtil.StrToBool( cgiGet( "POPOVER_SEARCH_MPAGE_Keepopened"));
            Popover_search_Reloadonkeychange = StringUtil.StrToBool( cgiGet( "POPOVER_SEARCH_MPAGE_Reloadonkeychange"));
            Popover_search_Minimumcharacters = (int)(Math.Round(context.localUtil.CToN( cgiGet( "POPOVER_SEARCH_MPAGE_Minimumcharacters"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subFssitemap_Class = cgiGet( "FSSITEMAP_MPAGE_Class");
            subFssitemap_Flexdirection = cgiGet( "FSSITEMAP_MPAGE_Flexdirection");
            /* Read variables values. */
            AV19Search = cgiGet( edtavSearch_Internalname);
            AssignAttri("", true, "AV19Search", AV19Search);
            if ( context.localUtil.VCDate( cgiGet( edtavPickerdummyvariable_Internalname), (short)(DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")))) == 0 )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_faildate", new   object[]  {context.GetMessage( "Picker Dummy Variable", "")}), 1, "vPICKERDUMMYVARIABLE_MPAGE");
               GX_FocusControl = edtavPickerdummyvariable_Internalname;
               AssignAttri("", true, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV33PickerDummyVariable = DateTime.MinValue;
               AssignAttri("", true, "AV33PickerDummyVariable", context.localUtil.Format(AV33PickerDummyVariable, "99/99/99"));
            }
            else
            {
               AV33PickerDummyVariable = context.localUtil.CToD( cgiGet( edtavPickerdummyvariable_Internalname), DateTimeUtil.MapDateFormat( context.GetLanguageProperty( "date_fmt")));
               AssignAttri("", true, "AV33PickerDummyVariable", context.localUtil.Format(AV33PickerDummyVariable, "99/99/99"));
            }
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
         E212D2 ();
         if (returnInSub) return;
      }

      protected void E212D2( )
      {
         /* Start Routine */
         returnInSub = false;
         (getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Headerrawhtml = "<link rel=\"shortcut icon\" type=\"image/x-icon\" href=\""+context.convertURL( (string)(context.GetImagePath( "cceab8ff-208f-4395-99fd-7fe799e0d69c", "", context.GetTheme( ))))+"\">";
         divLayoutmaintable_Class = "MainContainerWithFooter";
         AssignProp("", true, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         GXt_objcol_SdtDVelop_Menu_Item1 = AV23DVelop_Menu;
         new DesignSystem.Programs.wwpbaseobjects.menuoptionsdata(context ).execute( out  GXt_objcol_SdtDVelop_Menu_Item1) ;
         AV23DVelop_Menu = GXt_objcol_SdtDVelop_Menu_Item1;
         new DesignSystem.Programs.wwpbaseobjects.getmenuauthorizedoptions(context ).execute( ref  AV23DVelop_Menu) ;
         Popover_search_Iteminternalname = edtavSearch_Internalname;
         ucPopover_search.SendProperty(context, "", true, Popover_search_Internalname, "ItemInternalName", Popover_search_Iteminternalname);
         Ddc_adminag_Icon = context.convertURL( (string)(context.GetImagePath( "3a2bb037-746f-4ca9-85b6-8a9333319398", "", context.GetTheme( ))));
         ucDdc_adminag.SendProperty(context, "", true, Ddc_adminag_Internalname, "Icon", Ddc_adminag_Icon);
         AV9GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context).get();
         AV24UserName = (String.IsNullOrEmpty(StringUtil.RTrim( AV9GAMUser.gxTpr_Firstname)) ? AV9GAMUser.gxTpr_Name : StringUtil.Trim( AV9GAMUser.gxTpr_Firstname)+" "+StringUtil.Trim( AV9GAMUser.gxTpr_Lastname));
         AV5GAMRoleCollection = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).getroles(out  AV7GAMErrorCollection);
         AV37GXV1 = 1;
         while ( AV37GXV1 <= AV5GAMRoleCollection.Count )
         {
            AV6GAMRole = ((GeneXus.Programs.genexussecurity.SdtGAMRole)AV5GAMRoleCollection.Item(AV37GXV1));
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV25RolesDescriptions)) )
            {
               AV25RolesDescriptions += ", ";
               AssignAttri("", true, "AV25RolesDescriptions", AV25RolesDescriptions);
            }
            AV25RolesDescriptions += (String.IsNullOrEmpty(StringUtil.RTrim( AV6GAMRole.gxTpr_Description)) ? AV6GAMRole.gxTpr_Name : AV6GAMRole.gxTpr_Description);
            AssignAttri("", true, "AV25RolesDescriptions", AV25RolesDescriptions);
            AV37GXV1 = (int)(AV37GXV1+1);
         }
         Ddc_adminag_Caption = AV24UserName;
         ucDdc_adminag.SendProperty(context, "", true, Ddc_adminag_Internalname, "Caption", Ddc_adminag_Caption);
         lblActionsearch_Enabled = 0;
         AssignProp("", true, lblActionsearch_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(lblActionsearch_Enabled), 5, 0), true);
         if ( StringUtil.StrCmp(AV30Httprequest.Method, "GET") == 0 )
         {
            GXt_SdtWWP_DesignSystemSettings2 = AV16WWP_DesignSystemSettings;
            new DesignSystem.Programs.wwpbaseobjects.wwp_getdesignsystemsettings(context ).execute( out  GXt_SdtWWP_DesignSystemSettings2) ;
            AV16WWP_DesignSystemSettings = GXt_SdtWWP_DesignSystemSettings2;
            this.executeExternalObjectMethod("", true, "gx.core.ds", "setOption", new Object[] {(string)"base-color",AV16WWP_DesignSystemSettings.gxTpr_Basecolor}, false);
            this.executeExternalObjectMethod("", true, "gx.core.ds", "setOption", new Object[] {(string)"background-color",AV16WWP_DesignSystemSettings.gxTpr_Backgroundstyle}, false);
            this.executeExternalObjectMethod("", true, "WWPActions", "EmpoweredGrids_Refresh", new Object[] {}, false);
         }
      }

      private void E222D2( )
      {
         /* Fssitemap_Load Routine */
         returnInSub = false;
         AV14MaxItemsToDisplay = 15;
         AV13CountItems = 1;
         AV38GXV2 = 1;
         while ( AV38GXV2 <= AV23DVelop_Menu.Count )
         {
            AV10DVelop_Menu_Item = ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV23DVelop_Menu.Item(AV38GXV2));
            if ( AV13CountItems <= AV14MaxItemsToDisplay )
            {
               edtavCaption_Class = "AttributeLandingPurusFooter";
               AV12Caption = AV10DVelop_Menu_Item.gxTpr_Caption;
               AssignAttri("", true, edtavCaption_Internalname, AV12Caption);
               edtavCaption_Link = AV10DVelop_Menu_Item.gxTpr_Link;
               /* Load Method */
               if ( wbStart != -1 )
               {
                  wbStart = 71;
               }
               sendrow_712( ) ;
               if ( isFullAjaxMode( ) && ! bGXsfl_71_Refreshing )
               {
                  DoAjaxLoad(71, FssitemapRow);
               }
               AV13CountItems = (short)(AV13CountItems+1);
               AV39GXV3 = 1;
               while ( AV39GXV3 <= AV10DVelop_Menu_Item.gxTpr_Subitems.Count )
               {
                  AV11DVelop_Menu_SubItem = ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV10DVelop_Menu_Item.gxTpr_Subitems.Item(AV39GXV3));
                  if ( AV13CountItems <= AV14MaxItemsToDisplay )
                  {
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11DVelop_Menu_SubItem.gxTpr_Link)) )
                     {
                        AV12Caption = AV11DVelop_Menu_SubItem.gxTpr_Caption;
                        AssignAttri("", true, edtavCaption_Internalname, AV12Caption);
                        edtavCaption_Link = AV11DVelop_Menu_SubItem.gxTpr_Link;
                        edtavCaption_Class = "AttributeLandingPurusFooterSuboption";
                        /* Load Method */
                        if ( wbStart != -1 )
                        {
                           wbStart = 71;
                        }
                        sendrow_712( ) ;
                        if ( isFullAjaxMode( ) && ! bGXsfl_71_Refreshing )
                        {
                           DoAjaxLoad(71, FssitemapRow);
                        }
                        AV13CountItems = (short)(AV13CountItems+1);
                     }
                  }
                  else
                  {
                     if (true) break;
                  }
                  AV39GXV3 = (int)(AV39GXV3+1);
               }
            }
            else
            {
               if (true) break;
            }
            AV38GXV2 = (int)(AV38GXV2+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E172D2( )
      {
         /* Ddc_adminag_Onloadcomponent Routine */
         returnInSub = false;
         /* Object Property */
         if ( true )
         {
            bDynCreated_Wwpaux_wc = true;
         }
         if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Wwpaux_wc_Component), StringUtil.Lower( "WWPBaseObjects.WWP_MasterPageTopActionsWC")) != 0 )
         {
            WebComp_Wwpaux_wc = getWebComponent(GetType(), "DesignSystem.Programs", "wwpbaseobjects.wwp_masterpagetopactionswc", new Object[] {context} );
            WebComp_Wwpaux_wc.ComponentInit();
            WebComp_Wwpaux_wc.Name = "WWPBaseObjects.WWP_MasterPageTopActionsWC";
            WebComp_Wwpaux_wc_Component = "WWPBaseObjects.WWP_MasterPageTopActionsWC";
         }
         if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
         {
            WebComp_Wwpaux_wc.setjustcreated();
            WebComp_Wwpaux_wc.componentprepare(new Object[] {(string)"MPW0116",(string)""});
            WebComp_Wwpaux_wc.componentbind(new Object[] {});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Wwpaux_wc )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpMPW0116"+"");
            WebComp_Wwpaux_wc.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
         /*  Sending Event outputs  */
      }

      protected void E192D2( )
      {
         /* 'DoActionSearch' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "wwpbaseobjects.wwp_search.aspx"+UrlEncode(StringUtil.RTrim(AV15SearchAux)) + "," + UrlEncode(StringUtil.BoolToStr(false)) + "," + UrlEncode(StringUtil.RTrim(""));
         CallWebObject(formatLink("wwpbaseobjects.wwp_search.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
      }

      protected void E182D2( )
      {
         /* Popover_search_Onloadcomponent Routine */
         returnInSub = false;
         lblActionsearch_Enabled = 1;
         AssignProp("", true, lblActionsearch_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(lblActionsearch_Enabled), 5, 0), true);
         /* Object Property */
         if ( true )
         {
            bDynCreated_Wwpaux_wc = true;
         }
         if ( StringUtil.StrCmp(StringUtil.Lower( WebComp_Wwpaux_wc_Component), StringUtil.Lower( "WWPBaseObjects.WWP_SearchWC")) != 0 )
         {
            WebComp_Wwpaux_wc = getWebComponent(GetType(), "DesignSystem.Programs", "wwpbaseobjects.wwp_searchwc", new Object[] {context} );
            WebComp_Wwpaux_wc.ComponentInit();
            WebComp_Wwpaux_wc.Name = "WWPBaseObjects.WWP_SearchWC";
            WebComp_Wwpaux_wc_Component = "WWPBaseObjects.WWP_SearchWC";
         }
         if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
         {
            WebComp_Wwpaux_wc.setjustcreated();
            WebComp_Wwpaux_wc.componentprepare(new Object[] {(string)"MPW0116",(string)"",(string)AV19Search});
            WebComp_Wwpaux_wc.componentbind(new Object[] {(string)"vSEARCH_MPAGE"});
         }
         if ( isFullAjaxMode( ) || isAjaxCallMode( ) && bDynCreated_Wwpaux_wc )
         {
            context.httpAjaxContext.ajax_rspStartCmp("gxHTMLWrpMPW0116"+"");
            WebComp_Wwpaux_wc.componentdraw();
            context.httpAjaxContext.ajax_rspEndCmp();
         }
         /*  Sending Event outputs  */
      }

      protected void E232D2( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         GXt_boolean3 = false;
         new DesignSystem.Programs.wwpbaseobjects.loadbreadcrumb(context ).execute(  AV23DVelop_Menu,  AV30Httprequest.ScriptName, ref  AV26Breadcrumb, ref  GXt_boolean3) ;
         AssignAttri("", true, "AV26Breadcrumb", AV26Breadcrumb);
         GxWebStd.gx_hidden_field( context, "gxhash_vBREADCRUMB_MPAGE", GetSecureSignedToken( "gxmpage_", StringUtil.RTrim( context.localUtil.Format( AV26Breadcrumb, "")), context));
         if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV26Breadcrumb))) )
         {
            AV26Breadcrumb = AV27WebSession.Get("LastBreadcrumb");
            AssignAttri("", true, "AV26Breadcrumb", AV26Breadcrumb);
            GxWebStd.gx_hidden_field( context, "gxhash_vBREADCRUMB_MPAGE", GetSecureSignedToken( "gxmpage_", StringUtil.RTrim( context.localUtil.Format( AV26Breadcrumb, "")), context));
         }
         else
         {
            AV27WebSession.Set("LastBreadcrumb", AV26Breadcrumb);
         }
         if ( StringUtil.StrCmp(AV27WebSession.Get("IsLandingPage"), "S") == 0 )
         {
            divTableheadercell_Class = "col-xs-12 HorizontalStickyMenuHeaderCell MasterHeaderCellNoBackground CellPaddingLeftRight0XS CellPaddingTop";
            AssignProp("", true, divTableheadercell_Internalname, "Class", divTableheadercell_Class, true);
            divToprightfixedelements_Class = "ElementInStickyMenuHeaderCell TopRightFixedHeader";
            AssignProp("", true, divToprightfixedelements_Internalname, "Class", divToprightfixedelements_Class, true);
            divFootercell_Visible = 1;
            AssignProp("", true, divFootercell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divFootercell_Visible), 5, 0), true);
         }
         else
         {
            divTableheadercell_Class = "col-xs-12 HorizontalStickyMenuHeaderCell FixedHeader LandingPurusFixedHeaderAlignLeft MasterHeaderCellNoBackground CellPaddingLeftRight0XS CellPaddingTop";
            AssignProp("", true, divTableheadercell_Internalname, "Class", divTableheadercell_Class, true);
            divToprightfixedelements_Class = "ElementInStickyMenuHeaderCell FixedHeader TopRightFixedHeader";
            AssignProp("", true, divToprightfixedelements_Internalname, "Class", divToprightfixedelements_Class, true);
            divFootercell_Visible = 0;
            AssignProp("", true, divFootercell_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divFootercell_Visible), 5, 0), true);
         }
         AV27WebSession.Remove("IsLandingPage");
         /*  Sending Event outputs  */
      }

      protected void E202D2( )
      {
         /* 'Contacto' Routine */
         returnInSub = false;
         CallWebObject(formatLink("wwpbaseobjects.formulariocontacto.aspx") );
         context.wjLocDisableFrm = 1;
      }

      protected void wb_table1_24_2D2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblUnnamedtable6_Internalname, tblUnnamedtable6_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavSearch_Internalname, context.GetMessage( "Search", ""), "gx-form-item AttributeSearchLabel", 0, true, "width: 25%;");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 28,'',true,'" + sGXsfl_71_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavSearch_Internalname, AV19Search, StringUtil.RTrim( context.localUtil.Format( AV19Search, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,28);\"", "'"+""+"'"+",true,"+"'"+"E_MPAGE."+"'", "", "", "", context.GetMessage( "WWP_MasterPage_Search", ""), edtavSearch_Jsonclick, 0, "AttributeSearch", "", "", "", "", 1, edtavSearch_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, -1, -1, true, "", "start", true, "", "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblActionsearch_Internalname, context.GetMessage( "<i class=\"fas fa-search ImageSearchIcon\"></i>", ""), "", "", lblActionsearch_Jsonclick, "'"+""+"'"+",true,"+"'"+"EDOACTIONSEARCH_MPAGE."+"'", "", "TextBlock", 5, "", 1, lblActionsearch_Enabled, 1, 1, "HLP_WWPBaseObjects/WorkWithPlusMasterPage.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_24_2D2e( true) ;
         }
         else
         {
            wb_table1_24_2D2e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
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
         PA2D2( ) ;
         WS2D2( ) ;
         WE2D2( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      public override void master_styles( )
      {
         define_styles( ) ;
      }

      protected void define_styles( )
      {
         AddStyleSheetFile("DVelop/DVHorizontalMenu/DVHorizontalMenu.css", "");
         AddStyleSheetFile("DVelop/DVMessage/DVMessage.css", "");
         AddStyleSheetFile("DVelop/Bootstrap/Shared/fontawesome_vlatest/css/all.min.css", "");
         AddStyleSheetFile("DVelop/Bootstrap/Shared/DVelopBootstrap.css", "");
         AddStyleSheetFile("DVelop/Shared/daterangepicker/daterangepicker.css", "");
         AddStyleSheetFile("DVelop/Bootstrap/Shared/DVelopBootstrap.css", "");
         AddStyleSheetFile("calendar-system.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         if ( ! ( WebComp_Wwpaux_wc == null ) )
         {
            if ( StringUtil.Len( WebComp_Wwpaux_wc_Component) != 0 )
            {
               WebComp_Wwpaux_wc.componentthemes();
            }
         }
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= (getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)(getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Jscriptsrc.Item(idxLst))), "?20241217054830", true, true);
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
         context.AddJavascriptSource("wwpbaseobjects/workwithplusmasterpage.js", "?20241217054832", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/slimmenu/jquery.slimmenu.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVHorizontalMenu/DVHorizontalMenuRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVMessage/pnotify.custom.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVMessage/DVMessageRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Tooltip/BootstrapTooltipRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Mask/jquery.mask.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/BootstrapSelect.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/WorkWithPlusUtilitiesRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/locales.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/wwp-daterangepicker.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/moment.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/daterangepicker/daterangepicker.min.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DatePicker/DatePickerRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Popover/WWPPopoverRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_712( )
      {
         edtavCaption_Internalname = "vCAPTION_MPAGE_"+sGXsfl_71_idx;
      }

      protected void SubsflControlProps_fel_712( )
      {
         edtavCaption_Internalname = "vCAPTION_MPAGE_"+sGXsfl_71_fel_idx;
      }

      protected void sendrow_712( )
      {
         sGXsfl_71_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_71_idx), 4, 0), 4, "0");
         SubsflControlProps_712( ) ;
         WB2D0( ) ;
         FssitemapRow = GXWebRow.GetNew(context,MPFssitemapContainer);
         if ( subFssitemap_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subFssitemap_Backstyle = 0;
            if ( StringUtil.StrCmp(subFssitemap_Class, "") != 0 )
            {
               subFssitemap_Linesclass = subFssitemap_Class+"Odd";
            }
         }
         else if ( subFssitemap_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subFssitemap_Backstyle = 0;
            subFssitemap_Backcolor = subFssitemap_Allbackcolor;
            if ( StringUtil.StrCmp(subFssitemap_Class, "") != 0 )
            {
               subFssitemap_Linesclass = subFssitemap_Class+"Uniform";
            }
         }
         else if ( subFssitemap_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subFssitemap_Backstyle = 1;
            if ( StringUtil.StrCmp(subFssitemap_Class, "") != 0 )
            {
               subFssitemap_Linesclass = subFssitemap_Class+"Odd";
            }
            subFssitemap_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subFssitemap_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subFssitemap_Backstyle = 1;
            subFssitemap_Backcolor = (int)(0xFFFFFF);
            if ( StringUtil.StrCmp(subFssitemap_Class, "") != 0 )
            {
               subFssitemap_Linesclass = subFssitemap_Class+"Odd";
            }
         }
         /* Start of Columns property logic. */
         /* Table start */
         FssitemapRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtablefsfssitemap_Internalname+"_"+sGXsfl_71_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FssitemapRow.AddRenderProperties(FssitemapColumn);
         FssitemapRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FssitemapRow.AddRenderProperties(FssitemapColumn);
         FssitemapRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FssitemapRow.AddRenderProperties(FssitemapColumn);
         /* Div Control */
         FssitemapRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FssitemapRow.AddRenderProperties(FssitemapColumn);
         /* Attribute/Variable Label */
         FssitemapRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavCaption_Internalname,context.GetMessage( "Caption", ""),(string)"gx-form-item AttributeLandingPurusFooterLabel",(short)0,(bool)true,(string)"width: 25%;"});
         FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FssitemapRow.AddRenderProperties(FssitemapColumn);
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 76,'',true,'" + sGXsfl_71_idx + "',71)\"";
         ROClassString = edtavCaption_Class;
         FssitemapRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCaption_Internalname,(string)AV12Caption,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,76);\"","'"+""+"'"+",true,"+"'"+"E_MPAGE."+sGXsfl_71_idx+"'",(string)edtavCaption_Link,(string)"",(string)"",(string)"",(string)edtavCaption_Jsonclick,(short)0,(string)edtavCaption_Class,(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavCaption_Enabled,(short)0,(string)"text",(string)"",(short)80,(string)"chr",(short)1,(string)"row",(short)100,(short)0,(short)0,(short)71,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FssitemapRow.AddRenderProperties(FssitemapColumn);
         FssitemapRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FssitemapRow.AddRenderProperties(FssitemapColumn);
         if ( MPFssitemapContainer.GetWrapped() == 1 )
         {
            MPFssitemapContainer.CloseTag("cell");
         }
         if ( MPFssitemapContainer.GetWrapped() == 1 )
         {
            MPFssitemapContainer.CloseTag("row");
         }
         if ( MPFssitemapContainer.GetWrapped() == 1 )
         {
            MPFssitemapContainer.CloseTag("table");
         }
         /* End of table */
         send_integrity_lvl_hashes2D2( ) ;
         GXCCtl = "vDVELOP_MENU_MPAGE_" + sGXsfl_71_idx;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", true, GXCCtl, AV23DVelop_Menu);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(GXCCtl, AV23DVelop_Menu);
         }
         GXCCtl = "vSEARCHAUX_MPAGE_" + sGXsfl_71_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, AV15SearchAux);
         GXCCtl = "vBREADCRUMB_MPAGE_" + sGXsfl_71_idx;
         GxWebStd.gx_hidden_field( context, GXCCtl, AV26Breadcrumb);
         /* End of Columns property logic. */
         MPFssitemapContainer.AddRow(FssitemapRow);
         nGXsfl_71_idx = ((subFssitemap_Islastpage==1)&&(nGXsfl_71_idx+1>subFssitemap_fnc_Recordsperpage( )) ? 1 : nGXsfl_71_idx+1);
         sGXsfl_71_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_71_idx), 4, 0), 4, "0");
         SubsflControlProps_712( ) ;
         /* End function sendrow_712 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl71( )
      {
         if ( MPFssitemapContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"MPFssitemapContainer"+"DivS\" data-gxgridid=\"71\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subFssitemap_Internalname, subFssitemap_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            MPFssitemapContainer.AddObjectProperty("GridName", "Fssitemap");
         }
         else
         {
            MPFssitemapContainer.AddObjectProperty("GridName", "Fssitemap");
            MPFssitemapContainer.AddObjectProperty("Header", subFssitemap_Header);
            MPFssitemapContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            MPFssitemapContainer.AddObjectProperty("Class", "FreeStyleGrid");
            MPFssitemapContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Backcolorstyle), 1, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("CmpContext", "");
            MPFssitemapContainer.AddObjectProperty("InMasterPage", "true");
            FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            MPFssitemapContainer.AddColumnProperties(FssitemapColumn);
            FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            MPFssitemapContainer.AddColumnProperties(FssitemapColumn);
            FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            MPFssitemapContainer.AddColumnProperties(FssitemapColumn);
            FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            MPFssitemapContainer.AddColumnProperties(FssitemapColumn);
            FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            MPFssitemapContainer.AddColumnProperties(FssitemapColumn);
            FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FssitemapColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV12Caption));
            FssitemapColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavCaption_Class));
            FssitemapColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCaption_Enabled), 5, 0, ".", "")));
            FssitemapColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavCaption_Link));
            MPFssitemapContainer.AddColumnProperties(FssitemapColumn);
            FssitemapColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            MPFssitemapContainer.AddColumnProperties(FssitemapColumn);
            MPFssitemapContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Selectedindex), 4, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Allowselection), 1, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Selectioncolor), 9, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Allowhovering), 1, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Hoveringcolor), 9, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Allowcollapsing), 1, 0, ".", "")));
            MPFssitemapContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFssitemap_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         imgLogoheader_Internalname = "LOGOHEADER_MPAGE";
         imgLogoforstickymenu_Internalname = "LOGOFORSTICKYMENU_MPAGE";
         divLogostickymenucell_Internalname = "LOGOSTICKYMENUCELL_MPAGE";
         Ucmenu_Internalname = "UCMENU_MPAGE";
         edtavSearch_Internalname = "vSEARCH_MPAGE";
         lblActionsearch_Internalname = "ACTIONSEARCH_MPAGE";
         tblUnnamedtable6_Internalname = "UNNAMEDTABLE6_MPAGE";
         Ddc_adminag_Internalname = "DDC_ADMINAG_MPAGE";
         divToprightfixedelements_Internalname = "TOPRIGHTFIXEDELEMENTS_MPAGE";
         divTableuserrole_Internalname = "TABLEUSERROLE_MPAGE";
         divTableheader_Internalname = "TABLEHEADER_MPAGE";
         divTableheadercell_Internalname = "TABLEHEADERCELL_MPAGE";
         divTableheaderfixwidth_Internalname = "TABLEHEADERFIXWIDTH_MPAGE";
         divTablecontent_Internalname = "TABLECONTENT_MPAGE";
         imgMoreinfocontact_Internalname = "MOREINFOCONTACT_MPAGE";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1_MPAGE";
         lblContacttitle_Internalname = "CONTACTTITLE_MPAGE";
         lblAddress1_Internalname = "ADDRESS1_MPAGE";
         lblTelephone_Internalname = "TELEPHONE_MPAGE";
         lblEmail_Internalname = "EMAIL_MPAGE";
         lblFormcontact_Internalname = "FORMCONTACT_MPAGE";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2_MPAGE";
         lblSitemaptitle_Internalname = "SITEMAPTITLE_MPAGE";
         edtavCaption_Internalname = "vCAPTION_MPAGE";
         tblUnnamedtablefsfssitemap_Internalname = "UNNAMEDTABLEFSFSSITEMAP_MPAGE";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3_MPAGE";
         lblFollowustitle_Internalname = "FOLLOWUSTITLE_MPAGE";
         lblUafacebook_Internalname = "UAFACEBOOK_MPAGE";
         lblUainstagram_Internalname = "UAINSTAGRAM_MPAGE";
         lblUalinkedin_Internalname = "UALINKEDIN_MPAGE";
         lblUseraction1_Internalname = "USERACTION1_MPAGE";
         lblUawhatsapp_Internalname = "UAWHATSAPP_MPAGE";
         divUnnamedtable5_Internalname = "UNNAMEDTABLE5_MPAGE";
         lblPoliticaprivacidad_Internalname = "POLITICAPRIVACIDAD_MPAGE";
         divUnnamedtable4_Internalname = "UNNAMEDTABLE4_MPAGE";
         divFooter_Internalname = "FOOTER_MPAGE";
         divFootercell_Internalname = "FOOTERCELL_MPAGE";
         Ucmessage_Internalname = "UCMESSAGE_MPAGE";
         Uctooltip_Internalname = "UCTOOLTIP_MPAGE";
         Wwputilities_Internalname = "WWPUTILITIES_MPAGE";
         Wwpdatepicker_Internalname = "WWPDATEPICKER_MPAGE";
         divTablemain_Internalname = "TABLEMAIN_MPAGE";
         Popover_search_Internalname = "POPOVER_SEARCH_MPAGE";
         edtavPickerdummyvariable_Internalname = "vPICKERDUMMYVARIABLE_MPAGE";
         divDiv_wwpauxwc_Internalname = "DIV_WWPAUXWC_MPAGE";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS_MPAGE";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE_MPAGE";
         (getDataAreaObject() == null ? Form : getDataAreaObject().GetForm()).Internalname = "FORM_MPAGE";
         subFssitemap_Internalname = "FSSITEMAP_MPAGE";
      }

      public override void initialize_properties( )
      {
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subFssitemap_Allowcollapsing = 0;
         edtavCaption_Jsonclick = "";
         edtavCaption_Class = "AttributeLandingPurusFooter";
         edtavCaption_Link = "";
         edtavCaption_Enabled = 0;
         lblActionsearch_Enabled = 1;
         edtavSearch_Jsonclick = "";
         edtavSearch_Enabled = 1;
         subFssitemap_Backcolorstyle = 0;
         edtavPickerdummyvariable_Jsonclick = "";
         divFootercell_Visible = 1;
         divToprightfixedelements_Class = "ElementInStickyMenuHeaderCell TopRightFixedHeader";
         divTableheadercell_Class = "col-xs-12 HorizontalStickyMenuHeaderCell MasterHeaderCellNoBackground CellPaddingLeftRight0XS CellPaddingTop";
         divLayoutmaintable_Class = "Table";
         subFssitemap_Flexdirection = "column";
         subFssitemap_Class = "FreeStyleGrid";
         Popover_search_Minimumcharacters = 2;
         Popover_search_Reloadonkeychange = Convert.ToBoolean( -1);
         Popover_search_Keepopened = Convert.ToBoolean( 0);
         Popover_search_Position = "Bottom";
         Popover_search_Popoverwidth = 400;
         Popover_search_Triggerelement = "Value";
         Popover_search_Trigger = "Click";
         Popover_search_Iteminternalname = "";
         Wwputilities_Comboloadtype = "InfiniteScrolling";
         Wwputilities_Pagbarincludegoto = Convert.ToBoolean( -1);
         Wwputilities_Allowcolumnsrestore = Convert.ToBoolean( -1);
         Wwputilities_Allowcolumndragging = Convert.ToBoolean( -1);
         Wwputilities_Allowcolumnreordering = Convert.ToBoolean( -1);
         Wwputilities_Allowcolumnresizing = Convert.ToBoolean( -1);
         Wwputilities_Enableconvertcombotobootstrapselect = Convert.ToBoolean( -1);
         Wwputilities_Enableupdaterowselectionstatus = Convert.ToBoolean( -1);
         Wwputilities_Empowertabs = Convert.ToBoolean( -1);
         Wwputilities_Enablefixobjectfitcover = Convert.ToBoolean( -1);
         Ucmessage_Stoponerror = Convert.ToBoolean( -1);
         Ddc_adminag_Componentwidth = 260;
         Ddc_adminag_Cls = "DropDownOptionsHeader";
         Ddc_adminag_Caption = context.GetMessage( "Administrator", "");
         Ddc_adminag_Icon = "";
         Ucmenu_Moreoptionicon = "fa fa-bars";
         Ucmenu_Moreoptioncaption = "WWP_More";
         Ucmenu_Moreoptionenabled = Convert.ToBoolean( -1);
         Ucmenu_Collapsedtitle = "Design System";
         Ucmenu_Cls = "slimmenu RegularBackgroundColorOption";
         Contentholder.setDataArea(getDataAreaObject());
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
         setEventMetadata("REFRESH_MPAGE","""{"handler":"Refresh","iparms":[{"av":"FSSITEMAP_MPAGE_nFirstRecordOnPage"},{"av":"FSSITEMAP_MPAGE_nEOF"},{"av":"AV23DVelop_Menu","fld":"vDVELOP_MENU_MPAGE"},{"av":"AV26Breadcrumb","fld":"vBREADCRUMB_MPAGE","hsh":true}]""");
         setEventMetadata("REFRESH_MPAGE",""","oparms":[{"av":"AV26Breadcrumb","fld":"vBREADCRUMB_MPAGE","hsh":true},{"av":"divTableheadercell_Class","ctrl":"TABLEHEADERCELL_MPAGE","prop":"Class"},{"av":"divToprightfixedelements_Class","ctrl":"TOPRIGHTFIXEDELEMENTS_MPAGE","prop":"Class"},{"av":"divFootercell_Visible","ctrl":"FOOTERCELL_MPAGE","prop":"Visible"}]}""");
         setEventMetadata("FSSITEMAP_MPAGE.LOAD_MPAGE","""{"handler":"E222D2","iparms":[{"av":"AV23DVelop_Menu","fld":"vDVELOP_MENU_MPAGE"}]""");
         setEventMetadata("FSSITEMAP_MPAGE.LOAD_MPAGE",""","oparms":[{"av":"edtavCaption_Class","ctrl":"vCAPTION_MPAGE","prop":"Class"},{"av":"AV12Caption","fld":"vCAPTION_MPAGE"},{"av":"edtavCaption_Link","ctrl":"vCAPTION_MPAGE","prop":"Link"}]}""");
         setEventMetadata("DOUAFACEBOOK_MPAGE","""{"handler":"E112D1","iparms":[]}""");
         setEventMetadata("DOUAINSTAGRAM_MPAGE","""{"handler":"E122D1","iparms":[]}""");
         setEventMetadata("DOUALINKEDIN_MPAGE","""{"handler":"E132D1","iparms":[]}""");
         setEventMetadata("DOUSERACTION1_MPAGE","""{"handler":"E142D1","iparms":[]}""");
         setEventMetadata("DOUAWHATSAPP_MPAGE","""{"handler":"E152D1","iparms":[]}""");
         setEventMetadata("DDC_ADMINAG_MPAGE.ONLOADCOMPONENT_MPAGE","""{"handler":"E172D2","iparms":[]""");
         setEventMetadata("DDC_ADMINAG_MPAGE.ONLOADCOMPONENT_MPAGE",""","oparms":[{"ctrl":"WWPAUX_WC_MPAGE"}]}""");
         setEventMetadata("DOACTIONSEARCH_MPAGE","""{"handler":"E192D2","iparms":[{"av":"AV15SearchAux","fld":"vSEARCHAUX_MPAGE"}]}""");
         setEventMetadata("POPOVER_SEARCH_MPAGE.ONLOADCOMPONENT_MPAGE","""{"handler":"E182D2","iparms":[{"av":"AV19Search","fld":"vSEARCH_MPAGE"}]""");
         setEventMetadata("POPOVER_SEARCH_MPAGE.ONLOADCOMPONENT_MPAGE",""","oparms":[{"av":"lblActionsearch_Enabled","ctrl":"ACTIONSEARCH_MPAGE","prop":"Enabled"},{"ctrl":"WWPAUX_WC_MPAGE"}]}""");
         setEventMetadata("CONTACTO_MPAGE","""{"handler":"E202D2","iparms":[]}""");
         setEventMetadata("PPRIVACIDAD_MPAGE","""{"handler":"E162D1","iparms":[]}""");
         setEventMetadata("VALIDV_PICKERDUMMYVARIABLE","""{"handler":"Validv_Pickerdummyvariable","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Caption","iparms":[]}""");
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
         Contentholder = new GXDataAreaControl();
         AV23DVelop_Menu = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item>( context, "Item", "DesignSystem");
         AV26Breadcrumb = "";
         GXKey = "";
         AV15SearchAux = "";
         sPrefix = "";
         ClassString = "";
         imgLogoheader_gximage = "";
         StyleString = "";
         sImgUrl = "";
         imgLogoforstickymenu_gximage = "";
         ucUcmenu = new GXUserControl();
         ucDdc_adminag = new GXUserControl();
         imgMoreinfocontact_gximage = "";
         lblContacttitle_Jsonclick = "";
         lblAddress1_Jsonclick = "";
         lblTelephone_Jsonclick = "";
         lblEmail_Jsonclick = "";
         lblFormcontact_Jsonclick = "";
         lblSitemaptitle_Jsonclick = "";
         MPFssitemapContainer = new GXWebGrid( context);
         sStyleString = "";
         lblFollowustitle_Jsonclick = "";
         lblUafacebook_Jsonclick = "";
         lblUainstagram_Jsonclick = "";
         lblUalinkedin_Jsonclick = "";
         lblUseraction1_Jsonclick = "";
         lblUawhatsapp_Jsonclick = "";
         lblPoliticaprivacidad_Jsonclick = "";
         ucUcmessage = new GXUserControl();
         ucUctooltip = new GXUserControl();
         ucWwputilities = new GXUserControl();
         ucWwpdatepicker = new GXUserControl();
         ucPopover_search = new GXUserControl();
         TempTags = "";
         AV33PickerDummyVariable = DateTime.MinValue;
         WebComp_Wwpaux_wc_Component = "";
         OldWwpaux_wc = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV12Caption = "";
         GX_FocusControl = "";
         AV19Search = "";
         GXt_objcol_SdtDVelop_Menu_Item1 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item>( context, "Item", "DesignSystem");
         AV9GAMUser = new GeneXus.Programs.genexussecurity.SdtGAMUser(context);
         AV24UserName = "";
         AV5GAMRoleCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRole>( context, "GeneXus.Programs.genexussecurity.SdtGAMRole", "DesignSystem.Programs");
         AV7GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV6GAMRole = new GeneXus.Programs.genexussecurity.SdtGAMRole(context);
         AV25RolesDescriptions = "";
         AV30Httprequest = new GxHttpRequest( context);
         AV16WWP_DesignSystemSettings = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_DesignSystemSettings(context);
         GXt_SdtWWP_DesignSystemSettings2 = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_DesignSystemSettings(context);
         AV10DVelop_Menu_Item = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         FssitemapRow = new GXWebRow();
         AV11DVelop_Menu_SubItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         GXEncryptionTmp = "";
         AV27WebSession = context.GetSession();
         lblActionsearch_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sDynURL = "";
         Form = new GXWebForm();
         subFssitemap_Linesclass = "";
         FssitemapColumn = new GXWebColumn();
         ROClassString = "";
         GXCCtl = "";
         subFssitemap_Header = "";
         WebComp_Wwpaux_wc = new GeneXus.Http.GXNullWebComponent();
         /* GeneXus formulas. */
         edtavCaption_Enabled = 0;
      }

      private short GxWebError ;
      private short wbEnd ;
      private short wbStart ;
      private short nCmpId ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subFssitemap_Backcolorstyle ;
      private short AV14MaxItemsToDisplay ;
      private short AV13CountItems ;
      private short FSSITEMAP_MPAGE_nEOF ;
      private short nGotPars ;
      private short nGXWrapped ;
      private short subFssitemap_Backstyle ;
      private short subFssitemap_Allowselection ;
      private short subFssitemap_Allowhovering ;
      private short subFssitemap_Allowcollapsing ;
      private short subFssitemap_Collapsed ;
      private int nRC_GXsfl_71 ;
      private int subFssitemap_Recordcount ;
      private int nGXsfl_71_idx=1 ;
      private int edtavCaption_Enabled ;
      private int Ddc_adminag_Componentwidth ;
      private int Popover_search_Popoverwidth ;
      private int Popover_search_Minimumcharacters ;
      private int divFootercell_Visible ;
      private int subFssitemap_Islastpage ;
      private int AV37GXV1 ;
      private int lblActionsearch_Enabled ;
      private int AV38GXV2 ;
      private int AV39GXV3 ;
      private int edtavSearch_Enabled ;
      private int idxLst ;
      private int subFssitemap_Backcolor ;
      private int subFssitemap_Allbackcolor ;
      private int subFssitemap_Selectedindex ;
      private int subFssitemap_Selectioncolor ;
      private int subFssitemap_Hoveringcolor ;
      private long FSSITEMAP_MPAGE_nCurrentRecord ;
      private long FSSITEMAP_MPAGE_nFirstRecordOnPage ;
      private string sGXsfl_71_idx="0001" ;
      private string edtavCaption_Internalname ;
      private string GXKey ;
      private string Ucmenu_Cls ;
      private string Ucmenu_Collapsedtitle ;
      private string Ucmenu_Moreoptioncaption ;
      private string Ucmenu_Moreoptionicon ;
      private string Ddc_adminag_Icon ;
      private string Ddc_adminag_Caption ;
      private string Ddc_adminag_Cls ;
      private string Wwputilities_Comboloadtype ;
      private string Popover_search_Iteminternalname ;
      private string Popover_search_Trigger ;
      private string Popover_search_Triggerelement ;
      private string Popover_search_Position ;
      private string subFssitemap_Class ;
      private string subFssitemap_Flexdirection ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string divTableheaderfixwidth_Internalname ;
      private string divTableheadercell_Internalname ;
      private string divTableheadercell_Class ;
      private string divTableheader_Internalname ;
      private string ClassString ;
      private string imgLogoheader_gximage ;
      private string StyleString ;
      private string sImgUrl ;
      private string imgLogoheader_Internalname ;
      private string divLogostickymenucell_Internalname ;
      private string imgLogoforstickymenu_gximage ;
      private string imgLogoforstickymenu_Internalname ;
      private string divTableuserrole_Internalname ;
      private string Ucmenu_Internalname ;
      private string divToprightfixedelements_Internalname ;
      private string divToprightfixedelements_Class ;
      private string Ddc_adminag_Internalname ;
      private string divTablecontent_Internalname ;
      private string divFootercell_Internalname ;
      private string divFooter_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string imgMoreinfocontact_gximage ;
      private string imgMoreinfocontact_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string lblContacttitle_Internalname ;
      private string lblContacttitle_Jsonclick ;
      private string lblAddress1_Internalname ;
      private string lblAddress1_Jsonclick ;
      private string lblTelephone_Internalname ;
      private string lblTelephone_Jsonclick ;
      private string lblEmail_Internalname ;
      private string lblEmail_Jsonclick ;
      private string lblFormcontact_Internalname ;
      private string lblFormcontact_Jsonclick ;
      private string divUnnamedtable3_Internalname ;
      private string lblSitemaptitle_Internalname ;
      private string lblSitemaptitle_Jsonclick ;
      private string sStyleString ;
      private string subFssitemap_Internalname ;
      private string divUnnamedtable4_Internalname ;
      private string lblFollowustitle_Internalname ;
      private string lblFollowustitle_Jsonclick ;
      private string divUnnamedtable5_Internalname ;
      private string lblUafacebook_Internalname ;
      private string lblUafacebook_Jsonclick ;
      private string lblUainstagram_Internalname ;
      private string lblUainstagram_Jsonclick ;
      private string lblUalinkedin_Internalname ;
      private string lblUalinkedin_Jsonclick ;
      private string lblUseraction1_Internalname ;
      private string lblUseraction1_Jsonclick ;
      private string lblUawhatsapp_Internalname ;
      private string lblUawhatsapp_Jsonclick ;
      private string lblPoliticaprivacidad_Internalname ;
      private string lblPoliticaprivacidad_Jsonclick ;
      private string Ucmessage_Internalname ;
      private string Uctooltip_Internalname ;
      private string Wwputilities_Internalname ;
      private string Wwpdatepicker_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string Popover_search_Internalname ;
      private string TempTags ;
      private string edtavPickerdummyvariable_Internalname ;
      private string edtavPickerdummyvariable_Jsonclick ;
      private string divDiv_wwpauxwc_Internalname ;
      private string WebComp_Wwpaux_wc_Component ;
      private string OldWwpaux_wc ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GX_FocusControl ;
      private string edtavSearch_Internalname ;
      private string lblActionsearch_Internalname ;
      private string edtavCaption_Class ;
      private string edtavCaption_Link ;
      private string GXEncryptionTmp ;
      private string tblUnnamedtable6_Internalname ;
      private string edtavSearch_Jsonclick ;
      private string lblActionsearch_Jsonclick ;
      private string sDynURL ;
      private string sGXsfl_71_fel_idx="0001" ;
      private string subFssitemap_Linesclass ;
      private string tblUnnamedtablefsfssitemap_Internalname ;
      private string ROClassString ;
      private string edtavCaption_Jsonclick ;
      private string GXCCtl ;
      private string subFssitemap_Header ;
      private DateTime AV33PickerDummyVariable ;
      private bool bGXsfl_71_Refreshing=false ;
      private bool Ucmenu_Moreoptionenabled ;
      private bool Ucmessage_Stoponerror ;
      private bool Wwputilities_Enablefixobjectfitcover ;
      private bool Wwputilities_Empowertabs ;
      private bool Wwputilities_Enableupdaterowselectionstatus ;
      private bool Wwputilities_Enableconvertcombotobootstrapselect ;
      private bool Wwputilities_Allowcolumnresizing ;
      private bool Wwputilities_Allowcolumnreordering ;
      private bool Wwputilities_Allowcolumndragging ;
      private bool Wwputilities_Allowcolumnsrestore ;
      private bool Wwputilities_Pagbarincludegoto ;
      private bool Popover_search_Keepopened ;
      private bool Popover_search_Reloadonkeychange ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool toggleJsOutput ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool bDynCreated_Wwpaux_wc ;
      private bool gx_refresh_fired ;
      private bool GXt_boolean3 ;
      private string AV26Breadcrumb ;
      private string AV15SearchAux ;
      private string AV12Caption ;
      private string AV19Search ;
      private string AV24UserName ;
      private string AV25RolesDescriptions ;
      private IGxSession AV27WebSession ;
      private GXWebComponent WebComp_Wwpaux_wc ;
      private GXWebGrid MPFssitemapContainer ;
      private GXWebRow FssitemapRow ;
      private GXWebColumn FssitemapColumn ;
      private GXUserControl ucUcmenu ;
      private GXUserControl ucDdc_adminag ;
      private GXUserControl ucUcmessage ;
      private GXUserControl ucUctooltip ;
      private GXUserControl ucWwputilities ;
      private GXUserControl ucWwpdatepicker ;
      private GXUserControl ucPopover_search ;
      private GxHttpRequest AV30Httprequest ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXDataAreaControl Contentholder ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> AV23DVelop_Menu ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> GXt_objcol_SdtDVelop_Menu_Item1 ;
      private GeneXus.Programs.genexussecurity.SdtGAMUser AV9GAMUser ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMRole> AV5GAMRoleCollection ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV7GAMErrorCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMRole AV6GAMRole ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_DesignSystemSettings AV16WWP_DesignSystemSettings ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_DesignSystemSettings GXt_SdtWWP_DesignSystemSettings2 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV10DVelop_Menu_Item ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV11DVelop_Menu_SubItem ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
