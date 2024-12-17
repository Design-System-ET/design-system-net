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
   public class homecopy1 : GXDataArea
   {
      public homecopy1( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public homecopy1( IGxContext context )
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
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetNextPar( );
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
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Studiosgrid") == 0 )
            {
               gxnrStudiosgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Studiosgrid") == 0 )
            {
               gxgrStudiosgrid_refresh_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Fstestimonials") == 0 )
            {
               gxnrFstestimonials_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Fstestimonials") == 0 )
            {
               gxgrFstestimonials_refresh_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Customersgrid") == 0 )
            {
               gxnrCustomersgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Customersgrid") == 0 )
            {
               gxgrCustomersgrid_refresh_invoke( ) ;
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

      protected void gxnrStudiosgrid_newrow_invoke( )
      {
         nRC_GXsfl_111 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_111"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_111_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_111_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_111_idx = GetPar( "sGXsfl_111_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrStudiosgrid_newrow( ) ;
         /* End function gxnrStudiosgrid_newrow_invoke */
      }

      protected void gxgrStudiosgrid_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18SDTHomeLandingSampleLogos);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrStudiosgrid_refresh( AV18SDTHomeLandingSampleLogos) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrStudiosgrid_refresh_invoke */
      }

      protected void gxnrFstestimonials_newrow_invoke( )
      {
         nRC_GXsfl_133 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_133"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_133_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_133_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_133_idx = GetPar( "sGXsfl_133_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrFstestimonials_newrow( ) ;
         /* End function gxnrFstestimonials_newrow_invoke */
      }

      protected void gxgrFstestimonials_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18SDTHomeLandingSampleLogos);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrFstestimonials_refresh( AV18SDTHomeLandingSampleLogos) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrFstestimonials_refresh_invoke */
      }

      protected void gxnrCustomersgrid_newrow_invoke( )
      {
         nRC_GXsfl_161 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_161"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_161_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_161_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_161_idx = GetPar( "sGXsfl_161_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrCustomersgrid_newrow( ) ;
         /* End function gxnrCustomersgrid_newrow_invoke */
      }

      protected void gxgrCustomersgrid_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18SDTHomeLandingSampleLogos);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrCustomersgrid_refresh( AV18SDTHomeLandingSampleLogos) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrCustomersgrid_refresh_invoke */
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
         PA2G2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START2G2( ) ;
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
         context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
         context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
         context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
         context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = ((nGXWrapped==0) ? " data-HasEnter=\"false\" data-Skiponenter=\"false\"" : "");
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
         if ( nGXWrapped != 1 )
         {
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("homecopy1.aspx") +"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         }
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSDTHOMELANDINGSAMPLELOGOS", AV18SDTHomeLandingSampleLogos);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSDTHOMELANDINGSAMPLELOGOS", AV18SDTHomeLandingSampleLogos);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vSDTHOMELANDINGSAMPLELOGOS", GetSecureSignedToken( "", AV18SDTHomeLandingSampleLogos, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Sdtlandingpurusstudiosample", AV12SDTLandingPurusStudioSample);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Sdtlandingpurusstudiosample", AV12SDTLandingPurusStudioSample);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Sdthometestimonials", AV16SDTHomeTestimonials);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Sdthometestimonials", AV16SDTHomeTestimonials);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_111", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_111), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_133", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_133), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_161", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_161), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSDTHOMELANDINGSAMPLELOGOS", AV18SDTHomeLandingSampleLogos);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSDTHOMELANDINGSAMPLELOGOS", AV18SDTHomeLandingSampleLogos);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vSDTHOMELANDINGSAMPLELOGOS", GetSecureSignedToken( "", AV18SDTHomeLandingSampleLogos, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSDTLANDINGPURUSSTUDIOSAMPLE", AV12SDTLandingPurusStudioSample);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSDTLANDINGPURUSSTUDIOSAMPLE", AV12SDTLandingPurusStudioSample);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSDTHOMETESTIMONIALS", AV16SDTHomeTestimonials);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSDTHOMETESTIMONIALS", AV16SDTHomeTestimonials);
         }
         GxWebStd.gx_hidden_field( context, "subCustomersgrid_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "STUDIOSGRID_Class", StringUtil.RTrim( subStudiosgrid_Class));
         GxWebStd.gx_hidden_field( context, "STUDIOSGRID_Flexwrap", StringUtil.RTrim( subStudiosgrid_Flexwrap));
         GxWebStd.gx_hidden_field( context, "STUDIOSGRID_Justifycontent", StringUtil.RTrim( subStudiosgrid_Justifycontent));
         GxWebStd.gx_hidden_field( context, "STUDIOSGRID_Aligncontent", StringUtil.RTrim( subStudiosgrid_Aligncontent));
         GxWebStd.gx_hidden_field( context, "FSTESTIMONIALS_Class", StringUtil.RTrim( subFstestimonials_Class));
         GxWebStd.gx_hidden_field( context, "CUSTOMERSGRID_Class", StringUtil.RTrim( subCustomersgrid_Class));
         GxWebStd.gx_hidden_field( context, "CUSTOMERSGRID_Showpagecontroller", StringUtil.RTrim( StringUtil.BoolToStr( subCustomersgrid_Showpagecontroller)));
         GxWebStd.gx_hidden_field( context, "CUSTOMERSGRID_Infinite", StringUtil.RTrim( StringUtil.BoolToStr( subCustomersgrid_Infinite)));
         GxWebStd.gx_hidden_field( context, "CUSTOMERSGRID_Autoplay", StringUtil.RTrim( StringUtil.BoolToStr( subCustomersgrid_Autoplay)));
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
         if ( nGXWrapped != 1 )
         {
            context.WriteHtmlTextNl( "</form>") ;
         }
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
            WE2G2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT2G2( ) ;
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
         return formatLink("homecopy1.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "HomeCopy1" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Home Copy1", "") ;
      }

      protected void WB2G0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SectionSloganHeaderLandingPurus", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divSlogansection_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "Image" + " " + ((StringUtil.StrCmp(imgSloganheader_gximage, "")==0) ? "GX_Image_slogan_head_Class" : "GX_Image_"+imgSloganheader_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "4fed01cc-1844-4a02-92c0-d10cfc9c783f", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgSloganheader_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock1_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock1_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblSlogansubtitle_Internalname, context.GetMessage( "Desarrollamos Más que Software, Creamos Posibilidades. Soluciones Inteligentes para un Mundo Digital.", ""), "", "", lblSlogansubtitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSloganSubtitle", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
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
            GxWebStd.gx_div_start( context, divMainnumberssection_Internalname, 1, 0, "px", 0, "px", "TableContentLandingPurus", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop80", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblMainnumberstitle_Internalname, context.GetMessage( "Por qué Design System?", ""), "", "", lblMainnumberstitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusTitleWithUnderline", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblMainnumberssubtitle_Internalname, context.GetMessage( "En Design System, entendemos que en un entorno empresarial en constante evolución, la capacidad de innovar y adaptarse es clave para el éxito. Nuestra misión es ayudar a las empresas a no solo mantenerse competitivas, sino a liderar el mercado mediante una combinación estratégica de productividad, experiencia y visión tecnológica a largo plazo.", ""), "", "", lblMainnumberssubtitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock6_Internalname, context.GetMessage( "En Design System no solo brindamos soluciones tecnológicas, sino que nos asociamos contigo para construir un futuro más innovador, eficiente y competitivo para tu empresa. Nuestra experiencia y enfoque integral aseguran que cada proyecto no solo cumpla con tus expectativas, sino que las supere.", ""), "", "", lblTextblock6_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable4_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-wrap:wrap;justify-content:space-around;align-items:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellPaddingTop50", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable5_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "Image" + " " + ((StringUtil.StrCmp(imgMainnumbers_icon1_gximage, "")==0) ? "GX_Image_MainNumbers_Icon1_Class" : "GX_Image_"+imgMainnumbers_icon1_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "6d684090-2eee-49ab-95a3-82ac72d0a9e0", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgMainnumbers_icon1_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-md-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable10_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "justify-content:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock3_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock3_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMainnumbers_description1_Internalname, context.GetMessage( "Main Numbers_Description1", ""), "col-sm-3 AttributeLandingPurusMainNumbersSubtitleLabel", 0, true, "");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 46,'',false,'" + sGXsfl_111_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMainnumbers_description1_Internalname, AV10MainNumbers_Description1, StringUtil.RTrim( context.localUtil.Format( AV10MainNumbers_Description1, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,46);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMainnumbers_description1_Jsonclick, 0, "AttributeLandingPurusMainNumbersSubtitle", "", "", "", "", 1, edtavMainnumbers_description1_Enabled, 0, "text", "", 80, "chr", 1, "row", 150, 0, 0, 0, 0, -1, -1, false, "", "start", true, "", "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellPaddingTop50", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable6_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "Image" + " " + ((StringUtil.StrCmp(imgMainnumbers_icon2_gximage, "")==0) ? "GX_Image_MainNumbers_Icon2_Class" : "GX_Image_"+imgMainnumbers_icon2_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "458a60e9-8cd3-40dc-a78d-9fd963b05e17", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgMainnumbers_icon2_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-md-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable9_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "justify-content:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock4_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock4_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMainnumbers_description2_Internalname, context.GetMessage( "Main Numbers_Description2", ""), "col-sm-3 AttributeLandingPurusMainNumbersSubtitleLabel", 0, true, "");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 59,'',false,'" + sGXsfl_111_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMainnumbers_description2_Internalname, AV9MainNumbers_Description2, StringUtil.RTrim( context.localUtil.Format( AV9MainNumbers_Description2, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,59);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMainnumbers_description2_Jsonclick, 0, "AttributeLandingPurusMainNumbersSubtitle", "", "", "", "", 1, edtavMainnumbers_description2_Enabled, 0, "text", "", 80, "chr", 1, "row", 150, 0, 0, 0, 0, -1, -1, false, "", "start", true, "", "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "CellPaddingTop50", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable7_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "Image" + " " + ((StringUtil.StrCmp(imgMainnumbers_icon3_gximage, "")==0) ? "GX_Image_MainNumbers_Icon3_Class" : "GX_Image_"+imgMainnumbers_icon3_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "cffcd64c-205a-4f65-b8e1-72fd2d713bc4", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgMainnumbers_icon3_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-md-6", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable8_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "justify-content:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock5_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock5_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavMainnumbers_description3_Internalname, context.GetMessage( "Main Numbers_Description3", ""), "col-sm-3 AttributeLandingPurusMainNumbersSubtitleLabel", 0, true, "");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 72,'',false,'" + sGXsfl_111_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavMainnumbers_description3_Internalname, AV8MainNumbers_Description3, StringUtil.RTrim( context.localUtil.Format( AV8MainNumbers_Description3, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,72);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavMainnumbers_description3_Jsonclick, 0, "AttributeLandingPurusMainNumbersSubtitle", "", "", "", "", 1, edtavMainnumbers_description3_Enabled, 0, "text", "", 80, "chr", 1, "row", 150, 0, 0, 0, 0, -1, -1, false, "", "start", true, "", "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock2_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock2_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 LandingPurusFeaturesCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divFeaturessection_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop80", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblFeaturestitle_Internalname, context.GetMessage( "Nuestro proceso de desarrollo", ""), "", "", lblFeaturestitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusTitleWhiteWithUnderline", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblFeaturessubtitle1_Internalname, context.GetMessage( "Con un enfoque DevOps como metodología de trabajo, aceleramos la entrega de aplicaciones y servicios con mayor calidad, mediante la automatización de tareas para los equipos de desarrollo y operaciones. Esto optimiza tanto la integración continua como la entrega continua.", ""), "", "", lblFeaturessubtitle1_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitleWhite", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblFeaturessubtitle2_Internalname, context.GetMessage( "Adoptando el enfoque DevOps, conseguimos ofrecerle productos y servicios de manera más rápida y eficiente. Este enfoque nos permite implementar nuevas funcionalidades y mejoras con mayor agilidad, asegurando que siempre tenga acceso a las últimas innovaciones. Además, con pruebas y monitoreo constantes, garantizamos una mayor estabilidad y calidad en lo que ofrecemos.", ""), "", "", lblFeaturessubtitle2_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitleWhite", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock7_Internalname, context.GetMessage( "Nuestro método también nos ayuda a adaptarnos rápidamente a sus necesidades y comentarios, mejorando continuamente en función de su experiencia. Esto se traduce en una mayor disponibilidad y un rendimiento más confiable, con menos interrupciones. En esencia, DevOps nos permite ofrecerle un servicio más eficaz, transparente y alineado con sus expectativas.", ""), "", "", lblTextblock7_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitleWhite", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50 CellPaddingBottom80", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "justify-content:center;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "LandingPurusImageServices" + " " + ((StringUtil.StrCmp(imgLandindpurusfeaturesleft_gximage, "")==0) ? "GX_Image_LandindPurusFeaturesLeft_Class" : "GX_Image_"+imgLandindpurusfeaturesleft_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "81a08ff3-87e1-4ded-9a0f-6d17017f0237", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgLandindpurusfeaturesleft_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "LandingPurusImageServices" + " " + ((StringUtil.StrCmp(imgLandingpurusfeaturescenter_gximage, "")==0) ? "GX_Image_LandingPurusFeaturesCenter_Class" : "GX_Image_"+imgLandingpurusfeaturescenter_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "d850f88f-0242-4a3c-9598-5ca79d3a44ac", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgLandingpurusfeaturescenter_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "LandingPurusImageServices" + " " + ((StringUtil.StrCmp(imgLandingpurusfeaturesright_gximage, "")==0) ? "GX_Image_LandingPurusFeaturesRight_Class" : "GX_Image_"+imgLandingpurusfeaturesright_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "b331a689-7018-4c3c-aec4-3eac144f2e99", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgLandingpurusfeaturesright_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_HomeCopy1.htm");
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
            GxWebStd.gx_div_start( context, divStudiossection_Internalname, 1, 0, "px", 0, "px", "TableContentLandingPurus", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop80", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblStudiostitle_Internalname, context.GetMessage( "Tecnologias que utilizamos", ""), "", "", lblStudiostitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusTitleWithUnderline", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblStudiossubtitle_Internalname, context.GetMessage( "Transformamos y potenciamos tu idea de negocio, escalando y compitiendo de manera efectiva con una visión tecnológica a largo plazo. Contamos con una amplio arsenal de tecnologías para satisfacer todas las necesidades en los mas variados proyectos, estas son algunas:", ""), "", "", lblStudiossubtitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingBottom80", "start", "top", "", "", "div");
            /*  Grid Control  */
            StudiosgridContainer.SetIsFreestyle(true);
            StudiosgridContainer.SetWrapped(nGXWrapped);
            StartGridControl111( ) ;
         }
         if ( wbEnd == 111 )
         {
            wbEnd = 0;
            nRC_GXsfl_111 = (int)(nGXsfl_111_idx-1);
            if ( StudiosgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV21GXV1 = nGXsfl_111_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"StudiosgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Studiosgrid", StudiosgridContainer, subStudiosgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "StudiosgridContainerData", StudiosgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "StudiosgridContainerData"+"V", StudiosgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"StudiosgridContainerData"+"V"+"\" value='"+StudiosgridContainer.GridValuesHidden()+"'/>") ;
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 LandingPurusTestimonialsCell", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTestimonialssection_Internalname, 1, 0, "px", 0, "px", "TableContentLandingPurusTestimonials", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTestimonialstitle_Internalname, context.GetMessage( "Testimonials", ""), "", "", lblTestimonialstitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusTitleWhiteWithUnderline", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50", "start", "top", "", "", "div");
            /*  Grid Control  */
            FstestimonialsContainer.SetIsFreestyle(true);
            FstestimonialsContainer.SetWrapped(nGXWrapped);
            StartGridControl133( ) ;
         }
         if ( wbEnd == 133 )
         {
            wbEnd = 0;
            nRC_GXsfl_133 = (int)(nGXsfl_133_idx-1);
            if ( FstestimonialsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV25GXV5 = nGXsfl_133_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"FstestimonialsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Fstestimonials", FstestimonialsContainer, subFstestimonials_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "FstestimonialsContainerData", FstestimonialsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "FstestimonialsContainerData"+"V", FstestimonialsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"FstestimonialsContainerData"+"V"+"\" value='"+FstestimonialsContainer.GridValuesHidden()+"'/>") ;
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingBottom80", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divClientssection_Internalname, 1, 0, "px", 0, "px", "TableContentLandingPurusHorizontalGrid", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop80", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblClientstitle_Internalname, context.GetMessage( "Customers", ""), "", "", lblClientstitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusTitleWithUnderline", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingTop50 CellPaddingBottom50", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblCustomerssubtitle_Internalname, context.GetMessage( "Some of the companies that have relied on us.", ""), "", "", lblCustomerssubtitle_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_HomeCopy1.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 LandingPurusCustomersHGridCell", "start", "top", "", "", "div");
            /*  Grid Control  */
            CustomersgridContainer.SetIsFreestyle(true);
            CustomersgridContainer.SetWrapped(nGXWrapped);
            StartGridControl161( ) ;
         }
         if ( wbEnd == 161 )
         {
            wbEnd = 0;
            nRC_GXsfl_161 = (int)(nGXsfl_161_idx-1);
            if ( CustomersgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV29GXV9 = nGXsfl_161_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"CustomersgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Customersgrid", CustomersgridContainer, subCustomersgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "CustomersgridContainerData", CustomersgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "CustomersgridContainerData"+"V", CustomersgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"CustomersgridContainerData"+"V"+"\" value='"+CustomersgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
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
         }
         if ( wbEnd == 111 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( StudiosgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV21GXV1 = nGXsfl_111_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"StudiosgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Studiosgrid", StudiosgridContainer, subStudiosgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "StudiosgridContainerData", StudiosgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "StudiosgridContainerData"+"V", StudiosgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"StudiosgridContainerData"+"V"+"\" value='"+StudiosgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         if ( wbEnd == 133 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( FstestimonialsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV25GXV5 = nGXsfl_133_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"FstestimonialsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Fstestimonials", FstestimonialsContainer, subFstestimonials_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "FstestimonialsContainerData", FstestimonialsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "FstestimonialsContainerData"+"V", FstestimonialsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"FstestimonialsContainerData"+"V"+"\" value='"+FstestimonialsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         if ( wbEnd == 161 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( CustomersgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV29GXV9 = nGXsfl_161_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"CustomersgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Customersgrid", CustomersgridContainer, subCustomersgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "CustomersgridContainerData", CustomersgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "CustomersgridContainerData"+"V", CustomersgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"CustomersgridContainerData"+"V"+"\" value='"+CustomersgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START2G2( )
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
         Form.Meta.addItem("description", context.GetMessage( "Home Copy1", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP2G0( ) ;
      }

      protected void WS2G2( )
      {
         START2G2( ) ;
         EVT2G2( ) ;
      }

      protected void EVT2G2( )
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "STUDIOSGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              nGXsfl_111_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_111_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_111_idx), 4, 0), 4, "0");
                              SubsflControlProps_1112( ) ;
                              AV21GXV1 = nGXsfl_111_idx;
                              if ( ( AV12SDTLandingPurusStudioSample.Count >= AV21GXV1 ) && ( AV21GXV1 > 0 ) )
                              {
                                 AV12SDTLandingPurusStudioSample.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1));
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
                                    E112G2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "STUDIOSGRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Studiosgrid.Load */
                                    E122G2 ();
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
                                 }
                              }
                              else
                              {
                              }
                           }
                           else if ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "FSTESTIMONIALS.LOAD") == 0 )
                           {
                              nGXsfl_133_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_133_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_133_idx), 4, 0), 4, "0");
                              SubsflControlProps_1334( ) ;
                              AV25GXV5 = nGXsfl_133_idx;
                              if ( ( AV16SDTHomeTestimonials.Count >= AV25GXV5 ) && ( AV25GXV5 > 0 ) )
                              {
                                 AV16SDTHomeTestimonials.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem)AV16SDTHomeTestimonials.Item(AV25GXV5));
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "FSTESTIMONIALS.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Fstestimonials.Load */
                                    E132G4 ();
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
                           else if ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "CUSTOMERSGRID.LOAD") == 0 )
                           {
                              nGXsfl_161_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_161_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_161_idx), 4, 0), 4, "0");
                              SubsflControlProps_1613( ) ;
                              AV20Logo = cgiGet( edtavLogo_Internalname);
                              AssignProp("", false, edtavLogo_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV20Logo)) ? AV30Logo_GXI : context.convertURL( context.PathToRelativeUrl( AV20Logo))), !bGXsfl_161_Refreshing);
                              AssignProp("", false, edtavLogo_Internalname, "SrcSet", context.GetImageSrcSet( AV20Logo), true);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "CUSTOMERSGRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Customersgrid.Load */
                                    E142G3 ();
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

      protected void WE2G2( )
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

      protected void PA2G2( )
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
               GX_FocusControl = edtavMainnumbers_description1_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrStudiosgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_1112( ) ;
         while ( nGXsfl_111_idx <= nRC_GXsfl_111 )
         {
            sendrow_1112( ) ;
            nGXsfl_111_idx = ((subStudiosgrid_Islastpage==1)&&(nGXsfl_111_idx+1>subStudiosgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_111_idx+1);
            sGXsfl_111_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_111_idx), 4, 0), 4, "0");
            SubsflControlProps_1112( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( StudiosgridContainer)) ;
         /* End function gxnrStudiosgrid_newrow */
      }

      protected void gxnrCustomersgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_1613( ) ;
         while ( nGXsfl_161_idx <= nRC_GXsfl_161 )
         {
            sendrow_1613( ) ;
            nGXsfl_161_idx = ((subCustomersgrid_Islastpage==1)&&(nGXsfl_161_idx+1>subCustomersgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_161_idx+1);
            sGXsfl_161_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_161_idx), 4, 0), 4, "0");
            SubsflControlProps_1613( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( CustomersgridContainer)) ;
         /* End function gxnrCustomersgrid_newrow */
      }

      protected void gxnrFstestimonials_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_1334( ) ;
         while ( nGXsfl_133_idx <= nRC_GXsfl_133 )
         {
            sendrow_1334( ) ;
            nGXsfl_133_idx = ((subFstestimonials_Islastpage==1)&&(nGXsfl_133_idx+1>subFstestimonials_fnc_Recordsperpage( )) ? 1 : nGXsfl_133_idx+1);
            sGXsfl_133_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_133_idx), 4, 0), 4, "0");
            SubsflControlProps_1334( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( FstestimonialsContainer)) ;
         /* End function gxnrFstestimonials_newrow */
      }

      protected void gxgrStudiosgrid_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> AV18SDTHomeLandingSampleLogos )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         STUDIOSGRID_nCurrentRecord = 0;
         RF2G2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrStudiosgrid_refresh */
      }

      protected void gxgrFstestimonials_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> AV18SDTHomeLandingSampleLogos )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         FSTESTIMONIALS_nCurrentRecord = 0;
         RF2G4( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrFstestimonials_refresh */
      }

      protected void gxgrCustomersgrid_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> AV18SDTHomeLandingSampleLogos )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         CUSTOMERSGRID_nCurrentRecord = 0;
         RF2G3( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrCustomersgrid_refresh */
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
         RF2G2( ) ;
         RF2G4( ) ;
         RF2G3( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavMainnumbers_description1_Enabled = 0;
         AssignProp("", false, edtavMainnumbers_description1_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMainnumbers_description1_Enabled), 5, 0), true);
         edtavMainnumbers_description2_Enabled = 0;
         AssignProp("", false, edtavMainnumbers_description2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMainnumbers_description2_Enabled), 5, 0), true);
         edtavMainnumbers_description3_Enabled = 0;
         AssignProp("", false, edtavMainnumbers_description3_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMainnumbers_description3_Enabled), 5, 0), true);
         edtavSdtlandingpurusstudiosample__studiotitle_Enabled = 0;
         edtavSdtlandingpurusstudiosample__studiodescription_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsdescription_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsname_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialscompany_Enabled = 0;
      }

      protected void RF2G2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            StudiosgridContainer.ClearRows();
         }
         wbStart = 111;
         nGXsfl_111_idx = 1;
         sGXsfl_111_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_111_idx), 4, 0), 4, "0");
         SubsflControlProps_1112( ) ;
         bGXsfl_111_Refreshing = true;
         StudiosgridContainer.AddObjectProperty("GridName", "Studiosgrid");
         StudiosgridContainer.AddObjectProperty("CmpContext", "");
         StudiosgridContainer.AddObjectProperty("InMasterPage", "false");
         StudiosgridContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         StudiosgridContainer.AddObjectProperty("Class", "FreeStyleGrid");
         StudiosgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         StudiosgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         StudiosgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Backcolorstyle), 1, 0, ".", "")));
         StudiosgridContainer.PageSize = subStudiosgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_1112( ) ;
            /* Execute user event: Studiosgrid.Load */
            E122G2 ();
            wbEnd = 111;
            WB2G0( ) ;
         }
         bGXsfl_111_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2G2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vSDTHOMELANDINGSAMPLELOGOS", AV18SDTHomeLandingSampleLogos);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vSDTHOMELANDINGSAMPLELOGOS", AV18SDTHomeLandingSampleLogos);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vSDTHOMELANDINGSAMPLELOGOS", GetSecureSignedToken( "", AV18SDTHomeLandingSampleLogos, context));
      }

      protected void RF2G3( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            CustomersgridContainer.ClearRows();
         }
         wbStart = 161;
         nGXsfl_161_idx = 1;
         sGXsfl_161_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_161_idx), 4, 0), 4, "0");
         SubsflControlProps_1613( ) ;
         bGXsfl_161_Refreshing = true;
         CustomersgridContainer.AddObjectProperty("GridName", "Customersgrid");
         CustomersgridContainer.AddObjectProperty("CmpContext", "");
         CustomersgridContainer.AddObjectProperty("InMasterPage", "false");
         CustomersgridContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         CustomersgridContainer.AddObjectProperty("Class", "FreeStyleGrid");
         CustomersgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         CustomersgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         CustomersgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Backcolorstyle), 1, 0, ".", "")));
         CustomersgridContainer.PageSize = subCustomersgrid_fnc_Recordsperpage( );
         if ( subStudiosgrid_Islastpage != 0 )
         {
            STUDIOSGRID_nFirstRecordOnPage = (long)(subStudiosgrid_fnc_Recordcount( )-subStudiosgrid_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, "STUDIOSGRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(STUDIOSGRID_nFirstRecordOnPage), 15, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("STUDIOSGRID_nFirstRecordOnPage", STUDIOSGRID_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_1613( ) ;
            /* Execute user event: Customersgrid.Load */
            E142G3 ();
            wbEnd = 161;
            WB2G0( ) ;
         }
         bGXsfl_161_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2G3( )
      {
      }

      protected void RF2G4( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            FstestimonialsContainer.ClearRows();
         }
         wbStart = 133;
         nGXsfl_133_idx = 1;
         sGXsfl_133_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_133_idx), 4, 0), 4, "0");
         SubsflControlProps_1334( ) ;
         bGXsfl_133_Refreshing = true;
         FstestimonialsContainer.AddObjectProperty("GridName", "Fstestimonials");
         FstestimonialsContainer.AddObjectProperty("CmpContext", "");
         FstestimonialsContainer.AddObjectProperty("InMasterPage", "false");
         FstestimonialsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         FstestimonialsContainer.AddObjectProperty("Class", "FreeStyleGrid");
         FstestimonialsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         FstestimonialsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         FstestimonialsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Backcolorstyle), 1, 0, ".", "")));
         FstestimonialsContainer.PageSize = subFstestimonials_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_1334( ) ;
            /* Execute user event: Fstestimonials.Load */
            E132G4 ();
            wbEnd = 133;
            WB2G0( ) ;
         }
         bGXsfl_133_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes2G4( )
      {
      }

      protected int subStudiosgrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subStudiosgrid_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subStudiosgrid_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subStudiosgrid_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected int subCustomersgrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subCustomersgrid_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subCustomersgrid_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subCustomersgrid_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected int subFstestimonials_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subFstestimonials_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subFstestimonials_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subFstestimonials_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavMainnumbers_description1_Enabled = 0;
         AssignProp("", false, edtavMainnumbers_description1_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMainnumbers_description1_Enabled), 5, 0), true);
         edtavMainnumbers_description2_Enabled = 0;
         AssignProp("", false, edtavMainnumbers_description2_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMainnumbers_description2_Enabled), 5, 0), true);
         edtavMainnumbers_description3_Enabled = 0;
         AssignProp("", false, edtavMainnumbers_description3_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavMainnumbers_description3_Enabled), 5, 0), true);
         edtavSdtlandingpurusstudiosample__studiotitle_Enabled = 0;
         edtavSdtlandingpurusstudiosample__studiodescription_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsdescription_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsname_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialscompany_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP2G0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E112G2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Sdtlandingpurusstudiosample"), AV12SDTLandingPurusStudioSample);
            ajax_req_read_hidden_sdt(cgiGet( "Sdthometestimonials"), AV16SDTHomeTestimonials);
            ajax_req_read_hidden_sdt(cgiGet( "vSDTLANDINGPURUSSTUDIOSAMPLE"), AV12SDTLandingPurusStudioSample);
            ajax_req_read_hidden_sdt(cgiGet( "vSDTHOMETESTIMONIALS"), AV16SDTHomeTestimonials);
            /* Read saved values. */
            nRC_GXsfl_111 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_111"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nRC_GXsfl_133 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_133"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nRC_GXsfl_161 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_161"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subCustomersgrid_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "subCustomersgrid_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subStudiosgrid_Class = cgiGet( "STUDIOSGRID_Class");
            subStudiosgrid_Flexwrap = cgiGet( "STUDIOSGRID_Flexwrap");
            subStudiosgrid_Justifycontent = cgiGet( "STUDIOSGRID_Justifycontent");
            subStudiosgrid_Aligncontent = cgiGet( "STUDIOSGRID_Aligncontent");
            subFstestimonials_Class = cgiGet( "FSTESTIMONIALS_Class");
            subCustomersgrid_Class = cgiGet( "CUSTOMERSGRID_Class");
            subCustomersgrid_Showpagecontroller = StringUtil.StrToBool( cgiGet( "CUSTOMERSGRID_Showpagecontroller"));
            subCustomersgrid_Infinite = StringUtil.StrToBool( cgiGet( "CUSTOMERSGRID_Infinite"));
            subCustomersgrid_Autoplay = StringUtil.StrToBool( cgiGet( "CUSTOMERSGRID_Autoplay"));
            nRC_GXsfl_111 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_111"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nGXsfl_111_fel_idx = 0;
            while ( nGXsfl_111_fel_idx < nRC_GXsfl_111 )
            {
               nGXsfl_111_fel_idx = ((subStudiosgrid_Islastpage==1)&&(nGXsfl_111_fel_idx+1>subStudiosgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_111_fel_idx+1);
               sGXsfl_111_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_111_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_1112( ) ;
               AV21GXV1 = nGXsfl_111_fel_idx;
               if ( ( AV12SDTLandingPurusStudioSample.Count >= AV21GXV1 ) && ( AV21GXV1 > 0 ) )
               {
                  AV12SDTLandingPurusStudioSample.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1));
               }
            }
            if ( nGXsfl_111_fel_idx == 0 )
            {
               nGXsfl_111_idx = 1;
               sGXsfl_111_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_111_idx), 4, 0), 4, "0");
               SubsflControlProps_1112( ) ;
            }
            nGXsfl_111_fel_idx = 1;
            nRC_GXsfl_133 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_133"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nGXsfl_133_fel_idx = 0;
            while ( nGXsfl_133_fel_idx < nRC_GXsfl_133 )
            {
               nGXsfl_133_fel_idx = ((subFstestimonials_Islastpage==1)&&(nGXsfl_133_fel_idx+1>subFstestimonials_fnc_Recordsperpage( )) ? 1 : nGXsfl_133_fel_idx+1);
               sGXsfl_133_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_133_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_1334( ) ;
               AV25GXV5 = nGXsfl_133_fel_idx;
               if ( ( AV16SDTHomeTestimonials.Count >= AV25GXV5 ) && ( AV25GXV5 > 0 ) )
               {
                  AV16SDTHomeTestimonials.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem)AV16SDTHomeTestimonials.Item(AV25GXV5));
               }
            }
            if ( nGXsfl_133_fel_idx == 0 )
            {
               nGXsfl_133_idx = 1;
               sGXsfl_133_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_133_idx), 4, 0), 4, "0");
               SubsflControlProps_1334( ) ;
            }
            nGXsfl_133_fel_idx = 1;
            /* Read variables values. */
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
         E112G2 ();
         if (returnInSub) return;
      }

      protected void E112G2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV10MainNumbers_Description1 = context.GetMessage( "Años de Experiencia", "");
         AssignAttri("", false, "AV10MainNumbers_Description1", AV10MainNumbers_Description1);
         AV9MainNumbers_Description2 = context.GetMessage( "Diversos Proyetos", "");
         AssignAttri("", false, "AV9MainNumbers_Description2", AV9MainNumbers_Description2);
         AV8MainNumbers_Description3 = context.GetMessage( "Sin Fronteras", "");
         AssignAttri("", false, "AV8MainNumbers_Description3", AV8MainNumbers_Description3);
         GXt_objcol_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem1 = AV12SDTLandingPurusStudioSample;
         new DesignSystem.Programs.wwpbaseobjects.landingpurusstudiosamples(context ).execute( out  GXt_objcol_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem1) ;
         AV12SDTLandingPurusStudioSample = GXt_objcol_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem1;
         gx_BV111 = true;
         GXt_objcol_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem2 = AV16SDTHomeTestimonials;
         new DesignSystem.Programs.wwpbaseobjects.hometestimonials(context ).execute( out  GXt_objcol_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem2) ;
         AV16SDTHomeTestimonials = GXt_objcol_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem2;
         gx_BV133 = true;
         GXt_objcol_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem3 = AV18SDTHomeLandingSampleLogos;
         new DesignSystem.Programs.wwpbaseobjects.homelandingcustomerlogos(context ).execute( out  GXt_objcol_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem3) ;
         AV18SDTHomeLandingSampleLogos = GXt_objcol_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem3;
         subCustomersgrid_Autoplay = true;
         AssignProp("", false, "CustomersgridContainerDiv", "AutoPlay", StringUtil.BoolToStr( subCustomersgrid_Autoplay), true);
         subCustomersgrid_Showpagecontroller = false;
         AssignProp("", false, "CustomersgridContainerDiv", "ShowPageController", StringUtil.BoolToStr( subCustomersgrid_Showpagecontroller), true);
         subCustomersgrid_Infinite = true;
         AssignProp("", false, "CustomersgridContainerDiv", "Infinite", StringUtil.BoolToStr( subCustomersgrid_Infinite), true);
         AV19WebSession.Set("IsLandingPage", "S");
         /* Execute user subroutine: 'REFRESH' */
         S112 ();
         if (returnInSub) return;
      }

      private void E122G2( )
      {
         /* Studiosgrid_Load Routine */
         returnInSub = false;
         AV21GXV1 = 1;
         while ( AV21GXV1 <= AV12SDTLandingPurusStudioSample.Count )
         {
            AV12SDTLandingPurusStudioSample.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 111;
            }
            sendrow_1112( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_111_Refreshing )
            {
               DoAjaxLoad(111, StudiosgridRow);
            }
            AV21GXV1 = (int)(AV21GXV1+1);
         }
      }

      protected void S112( )
      {
         /* 'REFRESH' Routine */
         returnInSub = false;
         context.DoAjaxRefresh();
      }

      private void E142G3( )
      {
         /* Customersgrid_Load Routine */
         returnInSub = false;
         AV29GXV9 = 1;
         while ( AV29GXV9 <= AV18SDTHomeLandingSampleLogos.Count )
         {
            AV18SDTHomeLandingSampleLogos.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem)AV18SDTHomeLandingSampleLogos.Item(AV29GXV9));
            AV20Logo = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem)(AV18SDTHomeLandingSampleLogos.CurrentItem)).gxTpr_Logo;
            AssignAttri("", false, edtavLogo_Internalname, AV20Logo);
            AV30Logo_GXI = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem)(AV18SDTHomeLandingSampleLogos.CurrentItem)).gxTpr_Logo_gxi;
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 161;
            }
            sendrow_1613( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_161_Refreshing )
            {
               DoAjaxLoad(161, CustomersgridRow);
            }
            AV29GXV9 = (int)(AV29GXV9+1);
         }
         /*  Sending Event outputs  */
      }

      private void E132G4( )
      {
         /* Fstestimonials_Load Routine */
         returnInSub = false;
         AV25GXV5 = 1;
         while ( AV25GXV5 <= AV16SDTHomeTestimonials.Count )
         {
            AV16SDTHomeTestimonials.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem)AV16SDTHomeTestimonials.Item(AV25GXV5));
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 133;
            }
            sendrow_1334( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_133_Refreshing )
            {
               DoAjaxLoad(133, FstestimonialsRow);
            }
            AV25GXV5 = (int)(AV25GXV5+1);
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
         PA2G2( ) ;
         WS2G2( ) ;
         WE2G2( ) ;
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
         AddStyleSheetFile("HorizontalGrid/horizontalgrid.min.css", "");
         AddStyleSheetFile("HorizontalGrid/horizontalgrid.min.css", "");
         AddStyleSheetFile("HorizontalGrid/horizontalgrid.min.css", "");
         AddStyleSheetFile("HorizontalGrid/horizontalgrid.min.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217052252", true, true);
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
         if ( nGXWrapped != 1 )
         {
            context.AddJavascriptSource("messages."+StringUtil.Lower( context.GetLanguageProperty( "code"))+".js", "?"+GetCacheInvalidationToken( ), false, true);
            context.AddJavascriptSource("homecopy1.js", "?20241217052252", false, true);
            context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
            context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
            context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
            context.AddJavascriptSource("HorizontalGrid/horizontalgrid.min.js", "", false, true);
         }
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_1112( )
      {
         edtavSdtlandingpurusstudiosample__studioimage_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIOIMAGE_"+sGXsfl_111_idx;
         edtavSdtlandingpurusstudiosample__studiotitle_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIOTITLE_"+sGXsfl_111_idx;
         edtavSdtlandingpurusstudiosample__studiodescription_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIODESCRIPTION_"+sGXsfl_111_idx;
      }

      protected void SubsflControlProps_fel_1112( )
      {
         edtavSdtlandingpurusstudiosample__studioimage_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIOIMAGE_"+sGXsfl_111_fel_idx;
         edtavSdtlandingpurusstudiosample__studiotitle_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIOTITLE_"+sGXsfl_111_fel_idx;
         edtavSdtlandingpurusstudiosample__studiodescription_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIODESCRIPTION_"+sGXsfl_111_fel_idx;
      }

      protected void sendrow_1112( )
      {
         sGXsfl_111_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_111_idx), 4, 0), 4, "0");
         SubsflControlProps_1112( ) ;
         WB2G0( ) ;
         StudiosgridRow = GXWebRow.GetNew(context,StudiosgridContainer);
         if ( subStudiosgrid_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subStudiosgrid_Backstyle = 0;
            if ( StringUtil.StrCmp(subStudiosgrid_Class, "") != 0 )
            {
               subStudiosgrid_Linesclass = subStudiosgrid_Class+"Odd";
            }
         }
         else if ( subStudiosgrid_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subStudiosgrid_Backstyle = 0;
            subStudiosgrid_Backcolor = subStudiosgrid_Allbackcolor;
            if ( StringUtil.StrCmp(subStudiosgrid_Class, "") != 0 )
            {
               subStudiosgrid_Linesclass = subStudiosgrid_Class+"Uniform";
            }
         }
         else if ( subStudiosgrid_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subStudiosgrid_Backstyle = 1;
            if ( StringUtil.StrCmp(subStudiosgrid_Class, "") != 0 )
            {
               subStudiosgrid_Linesclass = subStudiosgrid_Class+"Odd";
            }
            subStudiosgrid_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subStudiosgrid_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subStudiosgrid_Backstyle = 1;
            subStudiosgrid_Backcolor = (int)(0xFFFFFF);
            if ( StringUtil.StrCmp(subStudiosgrid_Class, "") != 0 )
            {
               subStudiosgrid_Linesclass = subStudiosgrid_Class+"Odd";
            }
         }
         /* Start of Columns property logic. */
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtablefsstudiosgrid_Internalname+"_"+sGXsfl_111_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable2_Internalname+"_"+sGXsfl_111_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Flex",(string)"start",(string)"top",(string)" "+"data-gx-flex"+" ",(string)"flex-direction:column;align-items:center;",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Attribute/Variable Label */
         StudiosgridRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)"",context.GetMessage( "Studio Image", ""),(string)"gx-form-item AttributeLandingPurusStudioImageLabel",(short)0,(bool)true,(string)"width: 25%;"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Static Bitmap Variable */
         ClassString = "AttributeLandingPurusStudioImage" + " " + ((StringUtil.StrCmp(edtavSdtlandingpurusstudiosample__studioimage_gximage, "")==0) ? "" : "GX_Image_"+edtavSdtlandingpurusstudiosample__studioimage_gximage+"_Class");
         StyleString = "";
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( ((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1)).gxTpr_Studioimage)) ? ((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1)).gxTpr_Studioimage_gxi : context.PathToRelativeUrl( ((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1)).gxTpr_Studioimage));
         StudiosgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavSdtlandingpurusstudiosample__studioimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)1,(short)0,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"",(short)0,(string)"",(short)0,(short)0,(short)0,(string)"",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(bool)false,(bool)false,context.GetImageSrcSet( sImgUrl)});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Attribute/Variable Label */
         StudiosgridRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavSdtlandingpurusstudiosample__studiotitle_Internalname,context.GetMessage( "Studio Title", ""),(string)"gx-form-item AttributeLandingPurusSimpleTitleLabel",(short)0,(bool)true,(string)"width: 25%;"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 121,'',false,'" + sGXsfl_111_idx + "',111)\"";
         ROClassString = "AttributeLandingPurusSimpleTitle";
         StudiosgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavSdtlandingpurusstudiosample__studiotitle_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1)).gxTpr_Studiotitle,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,121);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavSdtlandingpurusstudiosample__studiotitle_Jsonclick,(short)0,(string)"AttributeLandingPurusSimpleTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavSdtlandingpurusstudiosample__studiotitle_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)111,(short)0,(short)-1,(short)-1,(bool)false,(string)"",(string)"start",(bool)true,(string)""});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"CellPaddingTop10",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Div Control */
         StudiosgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Attribute/Variable Label */
         StudiosgridRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavSdtlandingpurusstudiosample__studiodescription_Internalname,context.GetMessage( "Studio Description", ""),(string)"gx-form-item AttributeLandingPurusSubtitleLabel",(short)0,(bool)true,(string)"width: 25%;"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 124,'',false,'" + sGXsfl_111_idx + "',111)\"";
         ClassString = "AttributeLandingPurusSubtitle";
         StyleString = "";
         ClassString = "AttributeLandingPurusSubtitle";
         StyleString = "";
         StudiosgridRow.AddColumnProperties("html_textarea", 1, isAjaxCallMode( ), new Object[] {(string)edtavSdtlandingpurusstudiosample__studiodescription_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)AV12SDTLandingPurusStudioSample.Item(AV21GXV1)).gxTpr_Studiodescription,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,124);\"",(short)0,(short)1,(int)edtavSdtlandingpurusstudiosample__studiodescription_Enabled,(short)0,(short)80,(string)"chr",(short)5,(string)"row",(short)0,(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"400",(short)-1,(short)0,(string)"",(string)"",(short)-1,(bool)false,(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(short)0,(string)""});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         StudiosgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         StudiosgridRow.AddRenderProperties(StudiosgridColumn);
         send_integrity_lvl_hashes2G2( ) ;
         /* End of Columns property logic. */
         StudiosgridContainer.AddRow(StudiosgridRow);
         nGXsfl_111_idx = ((subStudiosgrid_Islastpage==1)&&(nGXsfl_111_idx+1>subStudiosgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_111_idx+1);
         sGXsfl_111_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_111_idx), 4, 0), 4, "0");
         SubsflControlProps_1112( ) ;
         /* End function sendrow_1112 */
      }

      protected void SubsflControlProps_1334( )
      {
         edtavSdthometestimonials__sdthometestimonialsdescription_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSDESCRIPTION_"+sGXsfl_133_idx;
         edtavSdthometestimonials__sdthometestimonialsname_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSNAME_"+sGXsfl_133_idx;
         edtavSdthometestimonials__sdthometestimonialscompany_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSCOMPANY_"+sGXsfl_133_idx;
      }

      protected void SubsflControlProps_fel_1334( )
      {
         edtavSdthometestimonials__sdthometestimonialsdescription_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSDESCRIPTION_"+sGXsfl_133_fel_idx;
         edtavSdthometestimonials__sdthometestimonialsname_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSNAME_"+sGXsfl_133_fel_idx;
         edtavSdthometestimonials__sdthometestimonialscompany_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSCOMPANY_"+sGXsfl_133_fel_idx;
      }

      protected void sendrow_1334( )
      {
         sGXsfl_133_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_133_idx), 4, 0), 4, "0");
         SubsflControlProps_1334( ) ;
         WB2G0( ) ;
         FstestimonialsRow = GXWebRow.GetNew(context,FstestimonialsContainer);
         if ( subFstestimonials_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subFstestimonials_Backstyle = 0;
            if ( StringUtil.StrCmp(subFstestimonials_Class, "") != 0 )
            {
               subFstestimonials_Linesclass = subFstestimonials_Class+"Odd";
            }
         }
         else if ( subFstestimonials_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subFstestimonials_Backstyle = 0;
            subFstestimonials_Backcolor = subFstestimonials_Allbackcolor;
            if ( StringUtil.StrCmp(subFstestimonials_Class, "") != 0 )
            {
               subFstestimonials_Linesclass = subFstestimonials_Class+"Uniform";
            }
         }
         else if ( subFstestimonials_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subFstestimonials_Backstyle = 1;
            if ( StringUtil.StrCmp(subFstestimonials_Class, "") != 0 )
            {
               subFstestimonials_Linesclass = subFstestimonials_Class+"Odd";
            }
            subFstestimonials_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subFstestimonials_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subFstestimonials_Backstyle = 1;
            if ( ((int)((nGXsfl_133_idx) % (2))) == 0 )
            {
               subFstestimonials_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subFstestimonials_Class, "") != 0 )
               {
                  subFstestimonials_Linesclass = subFstestimonials_Class+"Even";
               }
            }
            else
            {
               subFstestimonials_Backcolor = (int)(0xFFFFFF);
               if ( StringUtil.StrCmp(subFstestimonials_Class, "") != 0 )
               {
                  subFstestimonials_Linesclass = subFstestimonials_Class+"Odd";
               }
            }
         }
         /* Start of Columns property logic. */
         if ( FstestimonialsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr"+" class=\""+subFstestimonials_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_133_idx+"\">") ;
         }
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtablefsfstestimonials_Internalname+"_"+sGXsfl_133_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 TableLandingPurusTestimonials",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Attribute/Variable Label */
         FstestimonialsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavSdthometestimonials__sdthometestimonialsdescription_Internalname,context.GetMessage( "SDTHome Testimonials Description", ""),(string)"col-sm-3 AttributeLandingPurusTestimonialDescriptionLabel",(short)0,(bool)true,(string)""});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 138,'',false,'" + sGXsfl_133_idx + "',133)\"";
         ClassString = "AttributeLandingPurusTestimonialDescription";
         StyleString = "";
         ClassString = "AttributeLandingPurusTestimonialDescription";
         StyleString = "";
         FstestimonialsRow.AddColumnProperties("html_textarea", 1, isAjaxCallMode( ), new Object[] {(string)edtavSdthometestimonials__sdthometestimonialsdescription_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem)AV16SDTHomeTestimonials.Item(AV25GXV5)).gxTpr_Sdthometestimonialsdescription,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,138);\"",(short)0,(short)1,(int)edtavSdthometestimonials__sdthometestimonialsdescription_Enabled,(short)0,(short)80,(string)"chr",(short)5,(string)"row",(short)0,(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"400",(short)-1,(short)0,(string)"",(string)"",(short)-1,(bool)false,(string)"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(short)0,(string)""});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 LandingPurusTestimonialTitleCell",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtable1_Internalname+"_"+sGXsfl_133_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Table",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Attribute/Variable Label */
         FstestimonialsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavSdthometestimonials__sdthometestimonialsname_Internalname,context.GetMessage( "SDTHome Testimonials Name", ""),(string)"col-sm-3 AttributeLandingPurusTestimonialTitleLabel",(short)0,(bool)true,(string)""});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 145,'',false,'" + sGXsfl_133_idx + "',133)\"";
         ROClassString = "AttributeLandingPurusTestimonialTitle";
         FstestimonialsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavSdthometestimonials__sdthometestimonialsname_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem)AV16SDTHomeTestimonials.Item(AV25GXV5)).gxTpr_Sdthometestimonialsname,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,145);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavSdthometestimonials__sdthometestimonialsname_Jsonclick,(short)0,(string)"AttributeLandingPurusTestimonialTitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavSdthometestimonials__sdthometestimonialsname_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)133,(short)0,(short)-1,(short)-1,(bool)false,(string)"",(string)"start",(bool)true,(string)""});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Div Control */
         FstestimonialsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Attribute/Variable Label */
         FstestimonialsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavSdthometestimonials__sdthometestimonialscompany_Internalname,context.GetMessage( "SDTHome Testimonials Company", ""),(string)"col-sm-3 AttributeLandingPurusTestimonialSubtitleLabel",(short)0,(bool)true,(string)""});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 149,'',false,'" + sGXsfl_133_idx + "',133)\"";
         ROClassString = "AttributeLandingPurusTestimonialSubtitle";
         FstestimonialsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavSdthometestimonials__sdthometestimonialscompany_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem)AV16SDTHomeTestimonials.Item(AV25GXV5)).gxTpr_Sdthometestimonialscompany,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,149);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavSdthometestimonials__sdthometestimonialscompany_Jsonclick,(short)0,(string)"AttributeLandingPurusTestimonialSubtitle",(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavSdthometestimonials__sdthometestimonialscompany_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)133,(short)0,(short)-1,(short)-1,(bool)false,(string)"",(string)"start",(bool)true,(string)""});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         FstestimonialsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         FstestimonialsRow.AddRenderProperties(FstestimonialsColumn);
         send_integrity_lvl_hashes2G4( ) ;
         /* End of Columns property logic. */
         FstestimonialsContainer.AddRow(FstestimonialsRow);
         nGXsfl_133_idx = ((subFstestimonials_Islastpage==1)&&(nGXsfl_133_idx+1>subFstestimonials_fnc_Recordsperpage( )) ? 1 : nGXsfl_133_idx+1);
         sGXsfl_133_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_133_idx), 4, 0), 4, "0");
         SubsflControlProps_1334( ) ;
         /* End function sendrow_1334 */
      }

      protected void SubsflControlProps_1613( )
      {
         edtavLogo_Internalname = "vLOGO_"+sGXsfl_161_idx;
      }

      protected void SubsflControlProps_fel_1613( )
      {
         edtavLogo_Internalname = "vLOGO_"+sGXsfl_161_fel_idx;
      }

      protected void sendrow_1613( )
      {
         sGXsfl_161_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_161_idx), 4, 0), 4, "0");
         SubsflControlProps_1613( ) ;
         WB2G0( ) ;
         CustomersgridRow = GXWebRow.GetNew(context,CustomersgridContainer);
         if ( subCustomersgrid_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subCustomersgrid_Backstyle = 0;
            if ( StringUtil.StrCmp(subCustomersgrid_Class, "") != 0 )
            {
               subCustomersgrid_Linesclass = subCustomersgrid_Class+"Odd";
            }
         }
         else if ( subCustomersgrid_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subCustomersgrid_Backstyle = 0;
            subCustomersgrid_Backcolor = subCustomersgrid_Allbackcolor;
            if ( StringUtil.StrCmp(subCustomersgrid_Class, "") != 0 )
            {
               subCustomersgrid_Linesclass = subCustomersgrid_Class+"Uniform";
            }
         }
         else if ( subCustomersgrid_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subCustomersgrid_Backstyle = 1;
            if ( StringUtil.StrCmp(subCustomersgrid_Class, "") != 0 )
            {
               subCustomersgrid_Linesclass = subCustomersgrid_Class+"Odd";
            }
            subCustomersgrid_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subCustomersgrid_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subCustomersgrid_Backstyle = 1;
            if ( ((int)((nGXsfl_161_idx) % (2))) == 0 )
            {
               subCustomersgrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subCustomersgrid_Class, "") != 0 )
               {
                  subCustomersgrid_Linesclass = subCustomersgrid_Class+"Even";
               }
            }
            else
            {
               subCustomersgrid_Backcolor = (int)(0xFFFFFF);
               if ( StringUtil.StrCmp(subCustomersgrid_Class, "") != 0 )
               {
                  subCustomersgrid_Linesclass = subCustomersgrid_Class+"Odd";
               }
            }
         }
         /* Start of Columns property logic. */
         if ( CustomersgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr"+" class=\""+subCustomersgrid_Linesclass+"\" style=\""+""+"\""+" data-gxrow=\""+sGXsfl_161_idx+"\">") ;
         }
         /* Table start */
         CustomersgridRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtablefscustomersgrid_Internalname+"_"+sGXsfl_161_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         CustomersgridRow.AddRenderProperties(CustomersgridColumn);
         CustomersgridRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         CustomersgridRow.AddRenderProperties(CustomersgridColumn);
         CustomersgridRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         CustomersgridRow.AddRenderProperties(CustomersgridColumn);
         /* Div Control */
         CustomersgridRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         CustomersgridRow.AddRenderProperties(CustomersgridColumn);
         /* Attribute/Variable Label */
         CustomersgridRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)"",context.GetMessage( "Logo", ""),(string)"gx-form-item AttributeLandingPurusCustomersImageLabel",(short)0,(bool)true,(string)"width: 25%;"});
         CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         CustomersgridRow.AddRenderProperties(CustomersgridColumn);
         /* Static Bitmap Variable */
         ClassString = "AttributeLandingPurusCustomersImage" + " " + ((StringUtil.StrCmp(edtavLogo_gximage, "")==0) ? "" : "GX_Image_"+edtavLogo_gximage+"_Class");
         StyleString = "";
         AV20Logo_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV20Logo))&&String.IsNullOrEmpty(StringUtil.RTrim( AV30Logo_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV20Logo)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV20Logo)) ? AV30Logo_GXI : context.PathToRelativeUrl( AV20Logo));
         CustomersgridRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavLogo_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)1,(int)edtavLogo_Enabled,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"",(short)0,(string)"",(short)0,(short)0,(short)0,(string)"",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(bool)AV20Logo_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         CustomersgridRow.AddRenderProperties(CustomersgridColumn);
         CustomersgridRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         CustomersgridRow.AddRenderProperties(CustomersgridColumn);
         if ( CustomersgridContainer.GetWrapped() == 1 )
         {
            CustomersgridContainer.CloseTag("cell");
         }
         if ( CustomersgridContainer.GetWrapped() == 1 )
         {
            CustomersgridContainer.CloseTag("row");
         }
         if ( CustomersgridContainer.GetWrapped() == 1 )
         {
            CustomersgridContainer.CloseTag("table");
         }
         /* End of table */
         send_integrity_lvl_hashes2G3( ) ;
         /* End of Columns property logic. */
         CustomersgridContainer.AddRow(CustomersgridRow);
         nGXsfl_161_idx = ((subCustomersgrid_Islastpage==1)&&(nGXsfl_161_idx+1>subCustomersgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_161_idx+1);
         sGXsfl_161_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_161_idx), 4, 0), 4, "0");
         SubsflControlProps_1613( ) ;
         /* End function sendrow_1613 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl111( )
      {
         if ( StudiosgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"StudiosgridContainer"+"DivS\" data-gxgridid=\"111\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subStudiosgrid_Internalname, subStudiosgrid_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            StudiosgridContainer.AddObjectProperty("GridName", "Studiosgrid");
         }
         else
         {
            StudiosgridContainer.AddObjectProperty("GridName", "Studiosgrid");
            StudiosgridContainer.AddObjectProperty("Header", subStudiosgrid_Header);
            StudiosgridContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            StudiosgridContainer.AddObjectProperty("Class", "FreeStyleGrid");
            StudiosgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Backcolorstyle), 1, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("CmpContext", "");
            StudiosgridContainer.AddObjectProperty("InMasterPage", "false");
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSdtlandingpurusstudiosample__studiotitle_Enabled), 5, 0, ".", "")));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSdtlandingpurusstudiosample__studiodescription_Enabled), 5, 0, ".", "")));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            StudiosgridContainer.AddColumnProperties(StudiosgridColumn);
            StudiosgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Selectedindex), 4, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Allowselection), 1, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Selectioncolor), 9, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Allowhovering), 1, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Hoveringcolor), 9, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Allowcollapsing), 1, 0, ".", "")));
            StudiosgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subStudiosgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void StartGridControl133( )
      {
         if ( FstestimonialsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"FstestimonialsContainer"+"DivS\" data-gxgridid=\"133\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subFstestimonials_Internalname, subFstestimonials_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            FstestimonialsContainer.AddObjectProperty("GridName", "Fstestimonials");
         }
         else
         {
            FstestimonialsContainer.AddObjectProperty("GridName", "Fstestimonials");
            FstestimonialsContainer.AddObjectProperty("Header", subFstestimonials_Header);
            FstestimonialsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            FstestimonialsContainer.AddObjectProperty("Class", "FreeStyleGrid");
            FstestimonialsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Backcolorstyle), 1, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("CmpContext", "");
            FstestimonialsContainer.AddObjectProperty("InMasterPage", "false");
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSdthometestimonials__sdthometestimonialsdescription_Enabled), 5, 0, ".", "")));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSdthometestimonials__sdthometestimonialsname_Enabled), 5, 0, ".", "")));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSdthometestimonials__sdthometestimonialscompany_Enabled), 5, 0, ".", "")));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            FstestimonialsContainer.AddColumnProperties(FstestimonialsColumn);
            FstestimonialsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Selectedindex), 4, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Allowselection), 1, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Selectioncolor), 9, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Allowhovering), 1, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Hoveringcolor), 9, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Allowcollapsing), 1, 0, ".", "")));
            FstestimonialsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subFstestimonials_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void StartGridControl161( )
      {
         if ( CustomersgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"CustomersgridContainer"+"DivS\" data-gxgridid=\"161\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subCustomersgrid_Internalname, subCustomersgrid_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            CustomersgridContainer.AddObjectProperty("GridName", "Customersgrid");
         }
         else
         {
            CustomersgridContainer.AddObjectProperty("GridName", "Customersgrid");
            CustomersgridContainer.AddObjectProperty("Header", subCustomersgrid_Header);
            CustomersgridContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            CustomersgridContainer.AddObjectProperty("Class", "FreeStyleGrid");
            CustomersgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Backcolorstyle), 1, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("CmpContext", "");
            CustomersgridContainer.AddObjectProperty("InMasterPage", "false");
            CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CustomersgridContainer.AddColumnProperties(CustomersgridColumn);
            CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CustomersgridContainer.AddColumnProperties(CustomersgridColumn);
            CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CustomersgridContainer.AddColumnProperties(CustomersgridColumn);
            CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CustomersgridContainer.AddColumnProperties(CustomersgridColumn);
            CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CustomersgridContainer.AddColumnProperties(CustomersgridColumn);
            CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CustomersgridColumn.AddObjectProperty("Value", context.convertURL( AV20Logo));
            CustomersgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavLogo_Enabled), 5, 0, ".", "")));
            CustomersgridContainer.AddColumnProperties(CustomersgridColumn);
            CustomersgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            CustomersgridContainer.AddColumnProperties(CustomersgridColumn);
            CustomersgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Selectedindex), 4, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Allowselection), 1, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Selectioncolor), 9, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Allowhovering), 1, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Hoveringcolor), 9, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Allowcollapsing), 1, 0, ".", "")));
            CustomersgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subCustomersgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         imgSloganheader_Internalname = "SLOGANHEADER";
         lblTextblock1_Internalname = "TEXTBLOCK1";
         lblSlogansubtitle_Internalname = "SLOGANSUBTITLE";
         divSlogansection_Internalname = "SLOGANSECTION";
         lblMainnumberstitle_Internalname = "MAINNUMBERSTITLE";
         lblMainnumberssubtitle_Internalname = "MAINNUMBERSSUBTITLE";
         lblTextblock6_Internalname = "TEXTBLOCK6";
         imgMainnumbers_icon1_Internalname = "MAINNUMBERS_ICON1";
         lblTextblock3_Internalname = "TEXTBLOCK3";
         divUnnamedtable10_Internalname = "UNNAMEDTABLE10";
         edtavMainnumbers_description1_Internalname = "vMAINNUMBERS_DESCRIPTION1";
         divUnnamedtable5_Internalname = "UNNAMEDTABLE5";
         imgMainnumbers_icon2_Internalname = "MAINNUMBERS_ICON2";
         lblTextblock4_Internalname = "TEXTBLOCK4";
         divUnnamedtable9_Internalname = "UNNAMEDTABLE9";
         edtavMainnumbers_description2_Internalname = "vMAINNUMBERS_DESCRIPTION2";
         divUnnamedtable6_Internalname = "UNNAMEDTABLE6";
         imgMainnumbers_icon3_Internalname = "MAINNUMBERS_ICON3";
         lblTextblock5_Internalname = "TEXTBLOCK5";
         divUnnamedtable8_Internalname = "UNNAMEDTABLE8";
         edtavMainnumbers_description3_Internalname = "vMAINNUMBERS_DESCRIPTION3";
         divUnnamedtable7_Internalname = "UNNAMEDTABLE7";
         divUnnamedtable4_Internalname = "UNNAMEDTABLE4";
         lblTextblock2_Internalname = "TEXTBLOCK2";
         divMainnumberssection_Internalname = "MAINNUMBERSSECTION";
         lblFeaturestitle_Internalname = "FEATURESTITLE";
         lblFeaturessubtitle1_Internalname = "FEATURESSUBTITLE1";
         lblFeaturessubtitle2_Internalname = "FEATURESSUBTITLE2";
         lblTextblock7_Internalname = "TEXTBLOCK7";
         imgLandindpurusfeaturesleft_Internalname = "LANDINDPURUSFEATURESLEFT";
         imgLandingpurusfeaturescenter_Internalname = "LANDINGPURUSFEATURESCENTER";
         imgLandingpurusfeaturesright_Internalname = "LANDINGPURUSFEATURESRIGHT";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         divFeaturessection_Internalname = "FEATURESSECTION";
         lblStudiostitle_Internalname = "STUDIOSTITLE";
         lblStudiossubtitle_Internalname = "STUDIOSSUBTITLE";
         edtavSdtlandingpurusstudiosample__studioimage_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIOIMAGE";
         edtavSdtlandingpurusstudiosample__studiotitle_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIOTITLE";
         edtavSdtlandingpurusstudiosample__studiodescription_Internalname = "SDTLANDINGPURUSSTUDIOSAMPLE__STUDIODESCRIPTION";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         divUnnamedtablefsstudiosgrid_Internalname = "UNNAMEDTABLEFSSTUDIOSGRID";
         divStudiossection_Internalname = "STUDIOSSECTION";
         lblTestimonialstitle_Internalname = "TESTIMONIALSTITLE";
         edtavSdthometestimonials__sdthometestimonialsdescription_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSDESCRIPTION";
         edtavSdthometestimonials__sdthometestimonialsname_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSNAME";
         edtavSdthometestimonials__sdthometestimonialscompany_Internalname = "SDTHOMETESTIMONIALS__SDTHOMETESTIMONIALSCOMPANY";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         divUnnamedtablefsfstestimonials_Internalname = "UNNAMEDTABLEFSFSTESTIMONIALS";
         divTestimonialssection_Internalname = "TESTIMONIALSSECTION";
         lblClientstitle_Internalname = "CLIENTSTITLE";
         lblCustomerssubtitle_Internalname = "CUSTOMERSSUBTITLE";
         edtavLogo_Internalname = "vLOGO";
         tblUnnamedtablefscustomersgrid_Internalname = "UNNAMEDTABLEFSCUSTOMERSGRID";
         divClientssection_Internalname = "CLIENTSSECTION";
         divTablemain_Internalname = "TABLEMAIN";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subStudiosgrid_Internalname = "STUDIOSGRID";
         subFstestimonials_Internalname = "FSTESTIMONIALS";
         subCustomersgrid_Internalname = "CUSTOMERSGRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subCustomersgrid_Allowcollapsing = 0;
         subFstestimonials_Allowcollapsing = 0;
         subStudiosgrid_Allowcollapsing = 0;
         edtavLogo_gximage = "";
         edtavLogo_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialscompany_Jsonclick = "";
         edtavSdthometestimonials__sdthometestimonialscompany_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsname_Jsonclick = "";
         edtavSdthometestimonials__sdthometestimonialsname_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsdescription_Enabled = 0;
         edtavSdtlandingpurusstudiosample__studiodescription_Enabled = 0;
         edtavSdtlandingpurusstudiosample__studiotitle_Jsonclick = "";
         edtavSdtlandingpurusstudiosample__studiotitle_Enabled = 0;
         edtavSdtlandingpurusstudiosample__studioimage_gximage = "";
         subFstestimonials_Backcolorstyle = 0;
         subCustomersgrid_Backcolorstyle = 0;
         subStudiosgrid_Backcolorstyle = 0;
         edtavSdthometestimonials__sdthometestimonialscompany_Enabled = -1;
         edtavSdthometestimonials__sdthometestimonialsname_Enabled = -1;
         edtavSdthometestimonials__sdthometestimonialsdescription_Enabled = -1;
         edtavSdtlandingpurusstudiosample__studiodescription_Enabled = -1;
         edtavSdtlandingpurusstudiosample__studiotitle_Enabled = -1;
         edtavMainnumbers_description3_Jsonclick = "";
         edtavMainnumbers_description3_Enabled = 1;
         edtavMainnumbers_description2_Jsonclick = "";
         edtavMainnumbers_description2_Enabled = 1;
         edtavMainnumbers_description1_Jsonclick = "";
         edtavMainnumbers_description1_Enabled = 1;
         subCustomersgrid_Autoplay = Convert.ToBoolean( 0);
         subCustomersgrid_Infinite = Convert.ToBoolean( 0);
         subCustomersgrid_Showpagecontroller = Convert.ToBoolean( -1);
         subCustomersgrid_Class = "FreeStyleGrid";
         subFstestimonials_Class = "FreeStyleGrid";
         subStudiosgrid_Aligncontent = "space-around";
         subStudiosgrid_Justifycontent = "space-around";
         subStudiosgrid_Flexwrap = "wrap";
         subStudiosgrid_Class = "FreeStyleGrid";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Home Copy1", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"STUDIOSGRID_nFirstRecordOnPage"},{"av":"STUDIOSGRID_nEOF"},{"av":"AV12SDTLandingPurusStudioSample","fld":"vSDTLANDINGPURUSSTUDIOSAMPLE","grid":111},{"av":"nGXsfl_111_idx","ctrl":"GRID","prop":"GridCurrRow","grid":111},{"av":"nRC_GXsfl_111","ctrl":"STUDIOSGRID","prop":"GridRC","grid":111},{"av":"CUSTOMERSGRID_nFirstRecordOnPage"},{"av":"CUSTOMERSGRID_nEOF"},{"av":"FSTESTIMONIALS_nFirstRecordOnPage"},{"av":"FSTESTIMONIALS_nEOF"},{"av":"AV16SDTHomeTestimonials","fld":"vSDTHOMETESTIMONIALS","grid":133},{"av":"nGXsfl_133_idx","ctrl":"GRID","prop":"GridCurrRow","grid":133},{"av":"nRC_GXsfl_133","ctrl":"FSTESTIMONIALS","prop":"GridRC","grid":133},{"av":"AV18SDTHomeLandingSampleLogos","fld":"vSDTHOMELANDINGSAMPLELOGOS","hsh":true}]}""");
         setEventMetadata("CUSTOMERSGRID.LOAD","""{"handler":"E142G3","iparms":[{"av":"AV18SDTHomeLandingSampleLogos","fld":"vSDTHOMELANDINGSAMPLELOGOS","hsh":true}]""");
         setEventMetadata("CUSTOMERSGRID.LOAD",""","oparms":[{"av":"AV20Logo","fld":"vLOGO"}]}""");
         setEventMetadata("FSTESTIMONIALS.LOAD","""{"handler":"E132G4","iparms":[]}""");
         setEventMetadata("STUDIOSGRID.LOAD","""{"handler":"E122G2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv4","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv8","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Logo","iparms":[]}""");
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
         AV18SDTHomeLandingSampleLogos = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem>( context, "SDTHomeLandingSampleLogosItem", "DesignSystem");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV12SDTLandingPurusStudioSample = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem>( context, "SDTLandingPurusStudiosItem", "DesignSystem");
         AV16SDTHomeTestimonials = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem>( context, "SDTHomeTestimonialsItem", "DesignSystem");
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         imgSloganheader_gximage = "";
         StyleString = "";
         sImgUrl = "";
         lblTextblock1_Jsonclick = "";
         lblSlogansubtitle_Jsonclick = "";
         lblMainnumberstitle_Jsonclick = "";
         lblMainnumberssubtitle_Jsonclick = "";
         lblTextblock6_Jsonclick = "";
         imgMainnumbers_icon1_gximage = "";
         lblTextblock3_Jsonclick = "";
         TempTags = "";
         AV10MainNumbers_Description1 = "";
         imgMainnumbers_icon2_gximage = "";
         lblTextblock4_Jsonclick = "";
         AV9MainNumbers_Description2 = "";
         imgMainnumbers_icon3_gximage = "";
         lblTextblock5_Jsonclick = "";
         AV8MainNumbers_Description3 = "";
         lblTextblock2_Jsonclick = "";
         lblFeaturestitle_Jsonclick = "";
         lblFeaturessubtitle1_Jsonclick = "";
         lblFeaturessubtitle2_Jsonclick = "";
         lblTextblock7_Jsonclick = "";
         imgLandindpurusfeaturesleft_gximage = "";
         imgLandingpurusfeaturescenter_gximage = "";
         imgLandingpurusfeaturesright_gximage = "";
         lblStudiostitle_Jsonclick = "";
         lblStudiossubtitle_Jsonclick = "";
         StudiosgridContainer = new GXWebGrid( context);
         sStyleString = "";
         lblTestimonialstitle_Jsonclick = "";
         FstestimonialsContainer = new GXWebGrid( context);
         lblClientstitle_Jsonclick = "";
         lblCustomerssubtitle_Jsonclick = "";
         CustomersgridContainer = new GXWebGrid( context);
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV20Logo = "";
         AV30Logo_GXI = "";
         GXt_objcol_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem1 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem>( context, "SDTLandingPurusStudiosItem", "DesignSystem");
         GXt_objcol_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem2 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem>( context, "SDTHomeTestimonialsItem", "DesignSystem");
         GXt_objcol_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem3 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem>( context, "SDTHomeLandingSampleLogosItem", "DesignSystem");
         AV19WebSession = context.GetSession();
         StudiosgridRow = new GXWebRow();
         CustomersgridRow = new GXWebRow();
         FstestimonialsRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subStudiosgrid_Linesclass = "";
         StudiosgridColumn = new GXWebColumn();
         ROClassString = "";
         subFstestimonials_Linesclass = "";
         FstestimonialsColumn = new GXWebColumn();
         subCustomersgrid_Linesclass = "";
         CustomersgridColumn = new GXWebColumn();
         subStudiosgrid_Header = "";
         subFstestimonials_Header = "";
         subCustomersgrid_Header = "";
         /* GeneXus formulas. */
         edtavMainnumbers_description1_Enabled = 0;
         edtavMainnumbers_description2_Enabled = 0;
         edtavMainnumbers_description3_Enabled = 0;
         edtavSdtlandingpurusstudiosample__studiotitle_Enabled = 0;
         edtavSdtlandingpurusstudiosample__studiodescription_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsdescription_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialsname_Enabled = 0;
         edtavSdthometestimonials__sdthometestimonialscompany_Enabled = 0;
      }

      private short nRcdExists_4 ;
      private short nIsMod_4 ;
      private short nRcdExists_3 ;
      private short nIsMod_3 ;
      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short nGXWrapped ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subStudiosgrid_Backcolorstyle ;
      private short subCustomersgrid_Backcolorstyle ;
      private short subFstestimonials_Backcolorstyle ;
      private short subStudiosgrid_Backstyle ;
      private short subFstestimonials_Backstyle ;
      private short subCustomersgrid_Backstyle ;
      private short subStudiosgrid_Allowselection ;
      private short subStudiosgrid_Allowhovering ;
      private short subStudiosgrid_Allowcollapsing ;
      private short subStudiosgrid_Collapsed ;
      private short subFstestimonials_Allowselection ;
      private short subFstestimonials_Allowhovering ;
      private short subFstestimonials_Allowcollapsing ;
      private short subFstestimonials_Collapsed ;
      private short subCustomersgrid_Allowselection ;
      private short subCustomersgrid_Allowhovering ;
      private short subCustomersgrid_Allowcollapsing ;
      private short subCustomersgrid_Collapsed ;
      private short STUDIOSGRID_nEOF ;
      private short CUSTOMERSGRID_nEOF ;
      private short FSTESTIMONIALS_nEOF ;
      private int nRC_GXsfl_111 ;
      private int nRC_GXsfl_133 ;
      private int nRC_GXsfl_161 ;
      private int subCustomersgrid_Recordcount ;
      private int nGXsfl_111_idx=1 ;
      private int nGXsfl_133_idx=1 ;
      private int nGXsfl_161_idx=1 ;
      private int edtavMainnumbers_description1_Enabled ;
      private int edtavMainnumbers_description2_Enabled ;
      private int edtavMainnumbers_description3_Enabled ;
      private int AV21GXV1 ;
      private int AV25GXV5 ;
      private int AV29GXV9 ;
      private int subStudiosgrid_Islastpage ;
      private int subCustomersgrid_Islastpage ;
      private int subFstestimonials_Islastpage ;
      private int edtavSdtlandingpurusstudiosample__studiotitle_Enabled ;
      private int edtavSdtlandingpurusstudiosample__studiodescription_Enabled ;
      private int edtavSdthometestimonials__sdthometestimonialsdescription_Enabled ;
      private int edtavSdthometestimonials__sdthometestimonialsname_Enabled ;
      private int edtavSdthometestimonials__sdthometestimonialscompany_Enabled ;
      private int nGXsfl_111_fel_idx=1 ;
      private int nGXsfl_133_fel_idx=1 ;
      private int idxLst ;
      private int subStudiosgrid_Backcolor ;
      private int subStudiosgrid_Allbackcolor ;
      private int subFstestimonials_Backcolor ;
      private int subFstestimonials_Allbackcolor ;
      private int subCustomersgrid_Backcolor ;
      private int subCustomersgrid_Allbackcolor ;
      private int edtavLogo_Enabled ;
      private int subStudiosgrid_Selectedindex ;
      private int subStudiosgrid_Selectioncolor ;
      private int subStudiosgrid_Hoveringcolor ;
      private int subFstestimonials_Selectedindex ;
      private int subFstestimonials_Selectioncolor ;
      private int subFstestimonials_Hoveringcolor ;
      private int subCustomersgrid_Selectedindex ;
      private int subCustomersgrid_Selectioncolor ;
      private int subCustomersgrid_Hoveringcolor ;
      private long STUDIOSGRID_nCurrentRecord ;
      private long FSTESTIMONIALS_nCurrentRecord ;
      private long CUSTOMERSGRID_nCurrentRecord ;
      private long STUDIOSGRID_nFirstRecordOnPage ;
      private long CUSTOMERSGRID_nFirstRecordOnPage ;
      private long FSTESTIMONIALS_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_111_idx="0001" ;
      private string sGXsfl_133_idx="0001" ;
      private string sGXsfl_161_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string subStudiosgrid_Class ;
      private string subStudiosgrid_Flexwrap ;
      private string subStudiosgrid_Justifycontent ;
      private string subStudiosgrid_Aligncontent ;
      private string subFstestimonials_Class ;
      private string subCustomersgrid_Class ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divTablemain_Internalname ;
      private string divSlogansection_Internalname ;
      private string ClassString ;
      private string imgSloganheader_gximage ;
      private string StyleString ;
      private string sImgUrl ;
      private string imgSloganheader_Internalname ;
      private string lblTextblock1_Internalname ;
      private string lblTextblock1_Jsonclick ;
      private string lblSlogansubtitle_Internalname ;
      private string lblSlogansubtitle_Jsonclick ;
      private string divMainnumberssection_Internalname ;
      private string lblMainnumberstitle_Internalname ;
      private string lblMainnumberstitle_Jsonclick ;
      private string lblMainnumberssubtitle_Internalname ;
      private string lblMainnumberssubtitle_Jsonclick ;
      private string lblTextblock6_Internalname ;
      private string lblTextblock6_Jsonclick ;
      private string divUnnamedtable4_Internalname ;
      private string divUnnamedtable5_Internalname ;
      private string imgMainnumbers_icon1_gximage ;
      private string imgMainnumbers_icon1_Internalname ;
      private string divUnnamedtable10_Internalname ;
      private string lblTextblock3_Internalname ;
      private string lblTextblock3_Jsonclick ;
      private string edtavMainnumbers_description1_Internalname ;
      private string TempTags ;
      private string edtavMainnumbers_description1_Jsonclick ;
      private string divUnnamedtable6_Internalname ;
      private string imgMainnumbers_icon2_gximage ;
      private string imgMainnumbers_icon2_Internalname ;
      private string divUnnamedtable9_Internalname ;
      private string lblTextblock4_Internalname ;
      private string lblTextblock4_Jsonclick ;
      private string edtavMainnumbers_description2_Internalname ;
      private string edtavMainnumbers_description2_Jsonclick ;
      private string divUnnamedtable7_Internalname ;
      private string imgMainnumbers_icon3_gximage ;
      private string imgMainnumbers_icon3_Internalname ;
      private string divUnnamedtable8_Internalname ;
      private string lblTextblock5_Internalname ;
      private string lblTextblock5_Jsonclick ;
      private string edtavMainnumbers_description3_Internalname ;
      private string edtavMainnumbers_description3_Jsonclick ;
      private string lblTextblock2_Internalname ;
      private string lblTextblock2_Jsonclick ;
      private string divFeaturessection_Internalname ;
      private string lblFeaturestitle_Internalname ;
      private string lblFeaturestitle_Jsonclick ;
      private string lblFeaturessubtitle1_Internalname ;
      private string lblFeaturessubtitle1_Jsonclick ;
      private string lblFeaturessubtitle2_Internalname ;
      private string lblFeaturessubtitle2_Jsonclick ;
      private string lblTextblock7_Internalname ;
      private string lblTextblock7_Jsonclick ;
      private string divUnnamedtable3_Internalname ;
      private string imgLandindpurusfeaturesleft_gximage ;
      private string imgLandindpurusfeaturesleft_Internalname ;
      private string imgLandingpurusfeaturescenter_gximage ;
      private string imgLandingpurusfeaturescenter_Internalname ;
      private string imgLandingpurusfeaturesright_gximage ;
      private string imgLandingpurusfeaturesright_Internalname ;
      private string divStudiossection_Internalname ;
      private string lblStudiostitle_Internalname ;
      private string lblStudiostitle_Jsonclick ;
      private string lblStudiossubtitle_Internalname ;
      private string lblStudiossubtitle_Jsonclick ;
      private string sStyleString ;
      private string subStudiosgrid_Internalname ;
      private string divTestimonialssection_Internalname ;
      private string lblTestimonialstitle_Internalname ;
      private string lblTestimonialstitle_Jsonclick ;
      private string subFstestimonials_Internalname ;
      private string divClientssection_Internalname ;
      private string lblClientstitle_Internalname ;
      private string lblClientstitle_Jsonclick ;
      private string lblCustomerssubtitle_Internalname ;
      private string lblCustomerssubtitle_Jsonclick ;
      private string subCustomersgrid_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavLogo_Internalname ;
      private string sGXsfl_111_fel_idx="0001" ;
      private string sGXsfl_133_fel_idx="0001" ;
      private string edtavSdtlandingpurusstudiosample__studioimage_Internalname ;
      private string edtavSdtlandingpurusstudiosample__studiotitle_Internalname ;
      private string edtavSdtlandingpurusstudiosample__studiodescription_Internalname ;
      private string subStudiosgrid_Linesclass ;
      private string divUnnamedtablefsstudiosgrid_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string edtavSdtlandingpurusstudiosample__studioimage_gximage ;
      private string ROClassString ;
      private string edtavSdtlandingpurusstudiosample__studiotitle_Jsonclick ;
      private string edtavSdthometestimonials__sdthometestimonialsdescription_Internalname ;
      private string edtavSdthometestimonials__sdthometestimonialsname_Internalname ;
      private string edtavSdthometestimonials__sdthometestimonialscompany_Internalname ;
      private string subFstestimonials_Linesclass ;
      private string divUnnamedtablefsfstestimonials_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string edtavSdthometestimonials__sdthometestimonialsname_Jsonclick ;
      private string edtavSdthometestimonials__sdthometestimonialscompany_Jsonclick ;
      private string sGXsfl_161_fel_idx="0001" ;
      private string subCustomersgrid_Linesclass ;
      private string tblUnnamedtablefscustomersgrid_Internalname ;
      private string edtavLogo_gximage ;
      private string subStudiosgrid_Header ;
      private string subFstestimonials_Header ;
      private string subCustomersgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool subCustomersgrid_Showpagecontroller ;
      private bool subCustomersgrid_Infinite ;
      private bool subCustomersgrid_Autoplay ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_161_Refreshing=false ;
      private bool bGXsfl_111_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool bGXsfl_133_Refreshing=false ;
      private bool returnInSub ;
      private bool gx_BV111 ;
      private bool gx_BV133 ;
      private bool AV20Logo_IsBlob ;
      private string AV10MainNumbers_Description1 ;
      private string AV9MainNumbers_Description2 ;
      private string AV8MainNumbers_Description3 ;
      private string AV30Logo_GXI ;
      private string AV20Logo ;
      private GXWebGrid StudiosgridContainer ;
      private GXWebGrid FstestimonialsContainer ;
      private GXWebGrid CustomersgridContainer ;
      private GXWebRow StudiosgridRow ;
      private GXWebRow CustomersgridRow ;
      private GXWebRow FstestimonialsRow ;
      private GXWebColumn StudiosgridColumn ;
      private GXWebColumn FstestimonialsColumn ;
      private GXWebColumn CustomersgridColumn ;
      private IGxSession AV19WebSession ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> AV18SDTHomeLandingSampleLogos ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem> AV12SDTLandingPurusStudioSample ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem> AV16SDTHomeTestimonials ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem> GXt_objcol_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem1 ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem> GXt_objcol_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem2 ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> GXt_objcol_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem3 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
