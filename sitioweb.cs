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
   public class sitioweb : GXDataArea
   {
      public sitioweb( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public sitioweb( IGxContext context )
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
         PA3M2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START3M2( ) ;
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("sitioweb.aspx") +"\">") ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vCUOTAPLANBASICO", StringUtil.LTrim( StringUtil.NToC( AV12CuotaPlanBasico, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vCUOTAPLANSUPERIOR", StringUtil.LTrim( StringUtil.NToC( AV14CuotaPlanSuperior, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vCUOTAPLANNEGOCIOS", StringUtil.LTrim( StringUtil.NToC( AV16CuotaPlanNegocios, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
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
            WE3M2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT3M2( ) ;
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
         return formatLink("sitioweb.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "SitioWeb" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Sitio Web", "") ;
      }

      protected void WB3M0( )
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SitioWebTable", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop30", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock1_Internalname, context.GetMessage( "Creamos tu sitio web profesional ", ""), "", "", lblTextblock1_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "WebTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom40", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock2_Internalname, context.GetMessage( "para diferentes propositos", ""), "", "", lblTextblock2_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "WebTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock3_Internalname, context.GetMessage( "Diseñamos tu perfecta presencia en línea, profesional, funcional y a medida.", ""), "", "", lblTextblock3_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebSubTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom60", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock4_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock4_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock5_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock5_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginBottom60", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable30_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 30,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseraction1_Internalname, "", context.GetMessage( "Contratar AHORA!", ""), bttBtnuseraction1_Jsonclick, 5, context.GetMessage( "Contratar AHORA!", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSERACTION1\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseraction2_Internalname, "", context.GetMessage( "Solicita mas Información", ""), bttBtnuseraction2_Jsonclick, 5, context.GetMessage( "Solicita mas Información", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSERACTION2\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock8_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock8_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock41_Internalname, context.GetMessage( "<hr>", ""), "", "", lblTextblock41_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-10", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable28_Internalname, 1, 0, "px", 0, "px", "SitioWebTableMargin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3 SectionAbout", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "SitioWebImagen" + " " + ((StringUtil.StrCmp(imgEcommerce_gximage, "")==0) ? "GX_Image_Ecommerce_Class" : "GX_Image_"+imgEcommerce_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "fe4991b6-b0d0-4671-9e57-c22d9154b61e", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgEcommerce_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 SectionAbout", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable29_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock6_Internalname, context.GetMessage( "E-commerce", ""), "", "", lblTextblock6_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock9_Internalname, context.GetMessage( "Lanza tu tienda en línea con una experiencia de compra fluida y segura. Desde catálogos de productos hasta sistemas de pago integrados, optimizamos cada detalle para maximizar tus ventas.", ""), "", "", lblTextblock9_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock12_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock12_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-10", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable26_Internalname, 1, 0, "px", 0, "px", "SitioWebTableMargin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3 SectionAbout", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "SitioWebImagen" + " " + ((StringUtil.StrCmp(imgBlog_gximage, "")==0) ? "GX_Image_WWPBaseObjects_ImgBlog_Class" : "GX_Image_"+imgBlog_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "e516efa9-e888-4f8d-9b2c-1aa985ea909b", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgBlog_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 SectionAbout", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable27_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock10_Internalname, context.GetMessage( "Blogs", ""), "", "", lblTextblock10_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock11_Internalname, context.GetMessage( "Comparte tus ideas y conocimientos con el mundo a través de un diseño atractivo y funcional. Personalizamos tu blog para que sea fácil de usar y destacar tu contenido de manera profesional.", ""), "", "", lblTextblock11_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock14_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock14_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-10", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable4_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable24_Internalname, 1, 0, "px", 0, "px", "SitioWebTableMargin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3 SectionAbout", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "SitioWebImagen" + " " + ((StringUtil.StrCmp(imgNoticias_gximage, "")==0) ? "GX_Image_WWPBaseObjects_Noticias_Class" : "GX_Image_"+imgNoticias_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "edd8544d-2aa1-4f07-a35d-54bc70884a31", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgNoticias_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 SectionAbout", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable25_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock7_Internalname, context.GetMessage( "Noticias", ""), "", "", lblTextblock7_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock13_Internalname, context.GetMessage( "Mantén a tus lectores informados con un sitio de noticias dinámico y actualizado. Con opciones de gestión de contenido simplificadas, te ayudamos a ofrecer noticias frescas de manera efectiva.", ""), "", "", lblTextblock13_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock15_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock15_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-10", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable5_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable22_Internalname, 1, 0, "px", 0, "px", "SitioWebTableMargin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3 SectionAbout", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "SitioWebImagen" + " " + ((StringUtil.StrCmp(imgCoporativas_gximage, "")==0) ? "GX_Image_WWPBaseObjects_Coporativas_Class" : "GX_Image_"+imgCoporativas_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "6678ab2f-14e5-413b-aa72-a6bc211dde55", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgCoporativas_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 SectionAbout", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable23_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock16_Internalname, context.GetMessage( "Páginas Corporativas", ""), "", "", lblTextblock16_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock17_Internalname, context.GetMessage( "Refleja la esencia de tu empresa con una página web corporativa que impresione a tus clientes. Ofrecemos un diseño elegante y una estructura intuitiva para destacar tu marca y servicios.", ""), "", "", lblTextblock17_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock18_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock18_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-10 CellMarginBottom40", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable6_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable20_Internalname, 1, 0, "px", 0, "px", "SitioWebTableMargin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3 SectionAbout", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "SitioWebImagen" + " " + ((StringUtil.StrCmp(imgPortafolio_gximage, "")==0) ? "GX_Image_WWPBaseObjects_Portafolio_Class" : "GX_Image_"+imgPortafolio_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "23e2cba6-45c2-4d1b-9c12-a581ff697e98", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgPortafolio_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 SectionAbout", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable21_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock19_Internalname, context.GetMessage( "Portafolios", ""), "", "", lblTextblock19_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock20_Internalname, context.GetMessage( "Muestra tus proyectos y habilidades con un portafolio visualmente impactante. Ideal para creativos, profesionales y empresas que desean resaltar su trabajo con estilo.", ""), "", "", lblTextblock20_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock21_Internalname, context.GetMessage( "<hr>", ""), "", "", lblTextblock21_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SitioWebTableInfo_1", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable7_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable19_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock24_Internalname, context.GetMessage( "Multiplataformas y Responsivas.", ""), "", "", lblTextblock24_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitleGreen", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock25_Internalname, context.GetMessage( "Hosting y Dominio", ""), "", "", lblTextblock25_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitleWhite", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock29_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock29_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock28_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock28_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock26_Internalname, context.GetMessage( "Asegura una experiencia de usuario óptima en cualquier dispositivo con nuestras páginas web multiplataformas y completamente responsivas. Tu sitio se verá perfecto en móviles, tablets y ordenadores.", ""), "", "", lblTextblock26_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SubtitleWhite", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock27_Internalname, context.GetMessage( "Optimiza tu presencia en línea con nuestro paquete que incluye hosting confiable y un dominio único. Todo lo que necesitas para destacar en la web, en una sola oferta.", ""), "", "", lblTextblock27_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SubtitleWhite", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
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
            GxWebStd.gx_label_ctrl( context, lblTextblock42_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock42_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop80", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock22_Internalname, context.GetMessage( "Elige el plan perfecto para ti", ""), "", "", lblTextblock22_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "AboutTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SitioWebTableInfo_2", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableplanos_Internalname, 1, 0, "px", 0, "px", "SitioTableMargin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop30", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable10_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 col-lg-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable11_Internalname, 1, 0, "px", 0, "px", "CardShadowBlue", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock30_Internalname, context.GetMessage( "Basico", ""), "", "", lblTextblock30_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock34_Internalname, context.GetMessage( "El paquete ideal para un sito personal", ""), "", "", lblTextblock34_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SubtitleCardBlack", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable18_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock43_Internalname, context.GetMessage( "USD", ""), "", "", lblTextblock43_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebAttributeColor", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-9", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCostoplanbasico_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 190,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCostoplanbasico_Internalname, StringUtil.LTrim( StringUtil.NToC( AV11CostoPlanBasico, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavCostoplanbasico_Enabled!=0) ? context.localUtil.Format( AV11CostoPlanBasico, "ZZZZZZZZ9.99") : context.localUtil.Format( AV11CostoPlanBasico, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,190);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCostoplanbasico_Jsonclick, 0, "SitioWebAttributeColor", "", "", "", "", 1, edtavCostoplanbasico_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_SitioWeb.htm");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock44_Internalname, context.GetMessage( "*Inversion desarrollo", ""), "", "", lblTextblock44_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "AttributeSizeSmall", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock50_Internalname, context.GetMessage( "*+ Cuota mensual", ""), "", "", lblTextblock50_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "AttributeSizeSmall", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock46_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock46_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock35_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  4 Paginas", ""), "", "", lblTextblock35_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock38_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Responsivo", ""), "", "", lblTextblock38_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock36_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Hosting", ""), "", "", lblTextblock36_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock37_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Dominio", ""), "", "", lblTextblock37_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock40_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Formulario Contacto", ""), "", "", lblTextblock40_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock47_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock47_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 220,'',false,'',0)\"";
            ClassString = "ButtonColor";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseraction3_Internalname, "", context.GetMessage( "Elegir Plan", ""), bttBtnuseraction3_Jsonclick, 7, context.GetMessage( "Elegir Plan", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e113m1_client"+"'", TempTags, "", 2, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock51_Internalname, context.GetMessage( "Ver Detalles", ""), "", "", lblTextblock51_Jsonclick, "'"+""+"'"+",false,"+"'"+"E\\'BASICO\\'."+"'", "", "AttributeSizeSmall", 5, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock48_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock48_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 col-lg-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable12_Internalname, 1, 0, "px", 0, "px", "CardShadowBlueAnimacion", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock31_Internalname, context.GetMessage( "Superior", ""), "", "", lblTextblock31_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock52_Internalname, context.GetMessage( "Ideal para compartir contenido informativo", ""), "", "", lblTextblock52_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SubtitleCardBlack", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable17_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock45_Internalname, context.GetMessage( "USD", ""), "", "", lblTextblock45_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebAttributeColor", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-9", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCostoplansuperior_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 244,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCostoplansuperior_Internalname, StringUtil.LTrim( StringUtil.NToC( AV13CostoPlanSuperior, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavCostoplansuperior_Enabled!=0) ? context.localUtil.Format( AV13CostoPlanSuperior, "ZZZZZZZZ9.99") : context.localUtil.Format( AV13CostoPlanSuperior, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,244);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCostoplansuperior_Jsonclick, 0, "SitioWebAttributeColor", "", "", "", "", 1, edtavCostoplansuperior_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_SitioWeb.htm");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock55_Internalname, context.GetMessage( "*Inversion desarrollo", ""), "", "", lblTextblock55_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "AttributeSizeSmall", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock56_Internalname, context.GetMessage( "*+ Cuota mensual", ""), "", "", lblTextblock56_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "AttributeSizeSmall", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock57_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock57_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock58_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  Incluye Plan Basico", ""), "", "", lblTextblock58_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock59_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  7 Paginas", ""), "", "", lblTextblock59_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock60_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  Panel Edición Contenido", ""), "", "", lblTextblock60_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock61_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  Base de Datos 10GB", ""), "", "", lblTextblock61_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock62_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> WhattsApp ", ""), "", "", lblTextblock62_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock63_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock63_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 274,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseraction4_Internalname, "", context.GetMessage( "Elegir Plan", ""), bttBtnuseraction4_Jsonclick, 7, context.GetMessage( "Elegir Plan", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e123m1_client"+"'", TempTags, "", 2, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock64_Internalname, context.GetMessage( "Ver Detalles", ""), "", "", lblTextblock64_Jsonclick, "'"+""+"'"+",false,"+"'"+"E\\'SUPERIOR\\'."+"'", "", "AttributeSizeSmall", 5, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock65_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock65_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 col-lg-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable13_Internalname, 1, 0, "px", 0, "px", "CardShadowBlue", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock32_Internalname, context.GetMessage( "Negocios", ""), "", "", lblTextblock32_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock39_Internalname, context.GetMessage( "Ideal para concretar ventas en linea ", ""), "", "", lblTextblock39_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SubtitleCardBlack", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable16_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-3", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock53_Internalname, context.GetMessage( "USD", ""), "", "", lblTextblock53_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebAttributeColor", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-9 2", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavCostoplannegocios_Internalname+"\"", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-sm-9 gx-attribute", "start", "top", "", "", "div");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 298,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavCostoplannegocios_Internalname, StringUtil.LTrim( StringUtil.NToC( AV15CostoPlanNegocios, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavCostoplannegocios_Enabled!=0) ? context.localUtil.Format( AV15CostoPlanNegocios, "ZZZZZZZZ9.99") : context.localUtil.Format( AV15CostoPlanNegocios, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,298);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCostoplannegocios_Jsonclick, 0, "SitioWebAttributeColor", "", "", "", "", 1, edtavCostoplannegocios_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_SitioWeb.htm");
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
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock68_Internalname, context.GetMessage( "*Inversion desarrollo", ""), "", "", lblTextblock68_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "AttributeSizeSmall", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock69_Internalname, context.GetMessage( "*+ Cuota mensual", ""), "", "", lblTextblock69_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "AttributeSizeSmall", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock70_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock70_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock71_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  Incluye Plan Superior", ""), "", "", lblTextblock71_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock72_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Ventas en Linea", ""), "", "", lblTextblock72_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock73_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Punto de venta local", ""), "", "", lblTextblock73_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock74_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Panel Administracion", ""), "", "", lblTextblock74_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock75_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Registro de Clientes", ""), "", "", lblTextblock75_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock76_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock76_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 328,'',false,'',0)\"";
            ClassString = "ButtonColor";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseraction5_Internalname, "", context.GetMessage( "Elegir Plan", ""), bttBtnuseraction5_Jsonclick, 7, context.GetMessage( "Elegir Plan", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e133m1_client"+"'", TempTags, "", 2, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock77_Internalname, context.GetMessage( "Ver Detalles", ""), "", "", lblTextblock77_Jsonclick, "'"+""+"'"+",false,"+"'"+"E\\'NEGOCIOS\\'."+"'", "", "AttributeSizeSmall", 5, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock78_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock78_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 col-lg-3", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable14_Internalname, 1, 0, "px", 0, "px", "CardShadowBlue", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock33_Internalname, context.GetMessage( "Empresa", ""), "", "", lblTextblock33_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebTerciaryTitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock79_Internalname, context.GetMessage( "Solución Completa para tu Empresa", ""), "", "", lblTextblock79_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SubtitleCardBlack", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable15_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock92_Internalname, context.GetMessage( "Kit Completo", ""), "", "", lblTextblock92_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "SitioWebAttributeColor", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
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
            GxWebStd.gx_label_ctrl( context, lblTextblock54_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock54_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop20", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock83_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  Incluye Plan Negocios", ""), "", "", lblTextblock83_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock84_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  App Movil de ventas", ""), "", "", lblTextblock84_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock85_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Envio Notificaciones y Email", ""), "", "", lblTextblock85_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock86_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>  Sistema de estadisticas", ""), "", "", lblTextblock86_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock87_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Respaldo Base de Datos", ""), "", "", lblTextblock87_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock81_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Usuarios y Roles", ""), "", "", lblTextblock81_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock82_Internalname, context.GetMessage( "<i class=\"fa fa-check\" aria-hidden=\"true\"></i> App Administración y Stock", ""), "", "", lblTextblock82_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock88_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock88_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 378,'',false,'',0)\"";
            ClassString = "ButtonExcel";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnuseraction6_Internalname, "", context.GetMessage( "Consultar", ""), bttBtnuseraction6_Jsonclick, 5, context.GetMessage( "Consultar", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSERACTION6\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop10", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock89_Internalname, context.GetMessage( "Ver Detalles", ""), "", "", lblTextblock89_Jsonclick, "'"+""+"'"+",false,"+"'"+"E\\'EMPRESA\\'."+"'", "", "AttributeSizeSmall", 5, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock23_Internalname, context.GetMessage( "Comienza por el que mas te convenga, puedes actualizar en cualquier momento.", ""), "", "", lblTextblock23_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "LandingPurusSubtitle", 0, "", 1, 1, 0, 0, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTextblock49_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock49_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_SitioWeb.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable8_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable9_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
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
         }
         wbLoad = true;
      }

      protected void START3M2( )
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
         Form.Meta.addItem("description", context.GetMessage( "Sitio Web", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP3M0( ) ;
      }

      protected void WS3M2( )
      {
         START3M2( ) ;
         EVT3M2( ) ;
      }

      protected void EVT3M2( )
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
                              E143M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOUSERACTION6'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoUserAction6' */
                              E153M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOUSERACTION1'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoUserAction1' */
                              E163M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'DOUSERACTION2'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'DoUserAction2' */
                              E173M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'BASICO'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Basico' */
                              E183M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'SUPERIOR'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Superior' */
                              E193M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'NEGOCIOS'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Negocios' */
                              E203M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'EMPRESA'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Empresa' */
                              E213M2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E223M2 ();
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
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE3M2( )
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

      protected void PA3M2( )
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
               GX_FocusControl = edtavCostoplanbasico_Internalname;
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
         RF3M2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCostoplanbasico_Enabled = 0;
         AssignProp("", false, edtavCostoplanbasico_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCostoplanbasico_Enabled), 5, 0), true);
         edtavCostoplansuperior_Enabled = 0;
         AssignProp("", false, edtavCostoplansuperior_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCostoplansuperior_Enabled), 5, 0), true);
         edtavCostoplannegocios_Enabled = 0;
         AssignProp("", false, edtavCostoplannegocios_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCostoplannegocios_Enabled), 5, 0), true);
      }

      protected void RF3M2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E223M2 ();
            WB3M0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes3M2( )
      {
      }

      protected void before_start_formulas( )
      {
         edtavCostoplanbasico_Enabled = 0;
         AssignProp("", false, edtavCostoplanbasico_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCostoplanbasico_Enabled), 5, 0), true);
         edtavCostoplansuperior_Enabled = 0;
         AssignProp("", false, edtavCostoplansuperior_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCostoplansuperior_Enabled), 5, 0), true);
         edtavCostoplannegocios_Enabled = 0;
         AssignProp("", false, edtavCostoplannegocios_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCostoplannegocios_Enabled), 5, 0), true);
         fix_multi_value_controls( ) ;
      }

      protected void STRUP3M0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E143M2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            /* Read variables values. */
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCostoplanbasico_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCostoplanbasico_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vCOSTOPLANBASICO");
               GX_FocusControl = edtavCostoplanbasico_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV11CostoPlanBasico = 0;
               AssignAttri("", false, "AV11CostoPlanBasico", StringUtil.LTrimStr( AV11CostoPlanBasico, 12, 2));
            }
            else
            {
               AV11CostoPlanBasico = context.localUtil.CToN( cgiGet( edtavCostoplanbasico_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               AssignAttri("", false, "AV11CostoPlanBasico", StringUtil.LTrimStr( AV11CostoPlanBasico, 12, 2));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCostoplansuperior_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCostoplansuperior_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vCOSTOPLANSUPERIOR");
               GX_FocusControl = edtavCostoplansuperior_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV13CostoPlanSuperior = 0;
               AssignAttri("", false, "AV13CostoPlanSuperior", StringUtil.LTrimStr( AV13CostoPlanSuperior, 12, 2));
            }
            else
            {
               AV13CostoPlanSuperior = context.localUtil.CToN( cgiGet( edtavCostoplansuperior_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               AssignAttri("", false, "AV13CostoPlanSuperior", StringUtil.LTrimStr( AV13CostoPlanSuperior, 12, 2));
            }
            if ( ( ( context.localUtil.CToN( cgiGet( edtavCostoplannegocios_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtavCostoplannegocios_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "vCOSTOPLANNEGOCIOS");
               GX_FocusControl = edtavCostoplannegocios_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               wbErr = true;
               AV15CostoPlanNegocios = 0;
               AssignAttri("", false, "AV15CostoPlanNegocios", StringUtil.LTrimStr( AV15CostoPlanNegocios, 12, 2));
            }
            else
            {
               AV15CostoPlanNegocios = context.localUtil.CToN( cgiGet( edtavCostoplannegocios_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               AssignAttri("", false, "AV15CostoPlanNegocios", StringUtil.LTrimStr( AV15CostoPlanNegocios, 12, 2));
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
         E143M2 ();
         if (returnInSub) return;
      }

      protected void E143M2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV7WebSession.Set("IsLandingPage", "S");
         new datosplanesweb(context ).execute( out  AV11CostoPlanBasico, out  AV12CuotaPlanBasico, out  AV13CostoPlanSuperior, out  AV14CuotaPlanSuperior, out  AV15CostoPlanNegocios, out  AV16CuotaPlanNegocios) ;
         AssignAttri("", false, "AV11CostoPlanBasico", StringUtil.LTrimStr( AV11CostoPlanBasico, 12, 2));
         AssignAttri("", false, "AV12CuotaPlanBasico", StringUtil.LTrimStr( AV12CuotaPlanBasico, 12, 2));
         AssignAttri("", false, "AV13CostoPlanSuperior", StringUtil.LTrimStr( AV13CostoPlanSuperior, 12, 2));
         AssignAttri("", false, "AV14CuotaPlanSuperior", StringUtil.LTrimStr( AV14CuotaPlanSuperior, 12, 2));
         AssignAttri("", false, "AV15CostoPlanNegocios", StringUtil.LTrimStr( AV15CostoPlanNegocios, 12, 2));
         AssignAttri("", false, "AV16CuotaPlanNegocios", StringUtil.LTrimStr( AV16CuotaPlanNegocios, 12, 2));
      }

      protected void E153M2( )
      {
         /* 'DoUserAction6' Routine */
         returnInSub = false;
         GXt_char1 = AV10Phone;
         new searchphone(context ).execute( out  GXt_char1) ;
         AV10Phone = GXt_char1;
         AV8Window.Url = context.GetMessage( "https://api.whatsapp.com/send?phone=", "")+AV10Phone+context.GetMessage( "&text=Buenas%20Design%20System!!%20%F0%9F%98%8A%0AMe%20gustar%C3%ADa%20consultar%20sobre%20Plan%20Empresa%20%F0%9F%8C%8F%20para%20mi%2Fnegocio.", "");
         context.NewWindow(AV8Window);
         /*  Sending Event outputs  */
      }

      protected void E163M2( )
      {
         /* 'DoUserAction1' Routine */
         returnInSub = false;
         GXt_char1 = AV10Phone;
         new searchphone(context ).execute( out  GXt_char1) ;
         AV10Phone = GXt_char1;
         AV8Window.Url = context.GetMessage( "https://api.whatsapp.com/send?phone=", "")+AV10Phone+context.GetMessage( "&text=Buenas%20Design%20System!!%20%F0%9F%98%8A%0AMe%20gustar%C3%ADa%20consultar%20sobre%20desarrollo%20web%20%F0%9F%8C%8F%20para%20mi%2Fnegocio.", "");
         context.NewWindow(AV8Window);
         /*  Sending Event outputs  */
      }

      protected void E173M2( )
      {
         /* 'DoUserAction2' Routine */
         returnInSub = false;
         AV17mensaje = context.GetMessage( "Consulto sobre la posibilidad de contratar un desarrollo web", "");
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "appformulario.aspx"+UrlEncode(StringUtil.LTrimStr(0,1,0)) + "," + UrlEncode(StringUtil.RTrim(AV17mensaje));
         context.PopUp(formatLink("appformulario.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
      }

      protected void E183M2( )
      {
         /* 'Basico' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "webbasico.aspx"+UrlEncode(StringUtil.LTrimStr(AV11CostoPlanBasico,12,2)) + "," + UrlEncode(StringUtil.LTrimStr(AV12CuotaPlanBasico,12,2));
         context.PopUp(formatLink("webbasico.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {"AV11CostoPlanBasico","AV12CuotaPlanBasico"});
         /*  Sending Event outputs  */
      }

      protected void E193M2( )
      {
         /* 'Superior' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "websuperior.aspx"+UrlEncode(StringUtil.LTrimStr(AV13CostoPlanSuperior,12,2)) + "," + UrlEncode(StringUtil.LTrimStr(AV14CuotaPlanSuperior,12,2));
         context.PopUp(formatLink("websuperior.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {"AV13CostoPlanSuperior","AV14CuotaPlanSuperior"});
         /*  Sending Event outputs  */
      }

      protected void E203M2( )
      {
         /* 'Negocios' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "webnegocios.aspx"+UrlEncode(StringUtil.LTrimStr(AV15CostoPlanNegocios,12,2)) + "," + UrlEncode(StringUtil.LTrimStr(AV16CuotaPlanNegocios,12,2));
         context.PopUp(formatLink("webnegocios.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {"AV15CostoPlanNegocios","AV16CuotaPlanNegocios"});
         /*  Sending Event outputs  */
      }

      protected void E213M2( )
      {
         /* 'Empresa' Routine */
         returnInSub = false;
         context.PopUp(formatLink("webempresa.aspx") , new Object[] {});
      }

      protected void nextLoad( )
      {
      }

      protected void E223M2( )
      {
         /* Load Routine */
         returnInSub = false;
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
         PA3M2( ) ;
         WS3M2( ) ;
         WE3M2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?20241217075424", true, true);
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
         context.AddJavascriptSource("sitioweb.js", "?20241217075424", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         lblTextblock1_Internalname = "TEXTBLOCK1";
         lblTextblock2_Internalname = "TEXTBLOCK2";
         lblTextblock3_Internalname = "TEXTBLOCK3";
         lblTextblock4_Internalname = "TEXTBLOCK4";
         lblTextblock5_Internalname = "TEXTBLOCK5";
         bttBtnuseraction1_Internalname = "BTNUSERACTION1";
         bttBtnuseraction2_Internalname = "BTNUSERACTION2";
         divUnnamedtable30_Internalname = "UNNAMEDTABLE30";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         lblTextblock8_Internalname = "TEXTBLOCK8";
         lblTextblock41_Internalname = "TEXTBLOCK41";
         imgEcommerce_Internalname = "ECOMMERCE";
         lblTextblock6_Internalname = "TEXTBLOCK6";
         lblTextblock9_Internalname = "TEXTBLOCK9";
         divUnnamedtable29_Internalname = "UNNAMEDTABLE29";
         divUnnamedtable28_Internalname = "UNNAMEDTABLE28";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         lblTextblock12_Internalname = "TEXTBLOCK12";
         imgBlog_Internalname = "BLOG";
         lblTextblock10_Internalname = "TEXTBLOCK10";
         lblTextblock11_Internalname = "TEXTBLOCK11";
         divUnnamedtable27_Internalname = "UNNAMEDTABLE27";
         divUnnamedtable26_Internalname = "UNNAMEDTABLE26";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         lblTextblock14_Internalname = "TEXTBLOCK14";
         imgNoticias_Internalname = "NOTICIAS";
         lblTextblock7_Internalname = "TEXTBLOCK7";
         lblTextblock13_Internalname = "TEXTBLOCK13";
         divUnnamedtable25_Internalname = "UNNAMEDTABLE25";
         divUnnamedtable24_Internalname = "UNNAMEDTABLE24";
         divUnnamedtable4_Internalname = "UNNAMEDTABLE4";
         lblTextblock15_Internalname = "TEXTBLOCK15";
         imgCoporativas_Internalname = "COPORATIVAS";
         lblTextblock16_Internalname = "TEXTBLOCK16";
         lblTextblock17_Internalname = "TEXTBLOCK17";
         divUnnamedtable23_Internalname = "UNNAMEDTABLE23";
         divUnnamedtable22_Internalname = "UNNAMEDTABLE22";
         divUnnamedtable5_Internalname = "UNNAMEDTABLE5";
         lblTextblock18_Internalname = "TEXTBLOCK18";
         imgPortafolio_Internalname = "PORTAFOLIO";
         lblTextblock19_Internalname = "TEXTBLOCK19";
         lblTextblock20_Internalname = "TEXTBLOCK20";
         divUnnamedtable21_Internalname = "UNNAMEDTABLE21";
         divUnnamedtable20_Internalname = "UNNAMEDTABLE20";
         divUnnamedtable6_Internalname = "UNNAMEDTABLE6";
         lblTextblock21_Internalname = "TEXTBLOCK21";
         lblTextblock24_Internalname = "TEXTBLOCK24";
         lblTextblock25_Internalname = "TEXTBLOCK25";
         lblTextblock29_Internalname = "TEXTBLOCK29";
         lblTextblock28_Internalname = "TEXTBLOCK28";
         lblTextblock26_Internalname = "TEXTBLOCK26";
         lblTextblock27_Internalname = "TEXTBLOCK27";
         divUnnamedtable19_Internalname = "UNNAMEDTABLE19";
         lblTextblock42_Internalname = "TEXTBLOCK42";
         divUnnamedtable7_Internalname = "UNNAMEDTABLE7";
         lblTextblock22_Internalname = "TEXTBLOCK22";
         lblTextblock30_Internalname = "TEXTBLOCK30";
         lblTextblock34_Internalname = "TEXTBLOCK34";
         lblTextblock43_Internalname = "TEXTBLOCK43";
         edtavCostoplanbasico_Internalname = "vCOSTOPLANBASICO";
         divUnnamedtable18_Internalname = "UNNAMEDTABLE18";
         lblTextblock44_Internalname = "TEXTBLOCK44";
         lblTextblock50_Internalname = "TEXTBLOCK50";
         lblTextblock46_Internalname = "TEXTBLOCK46";
         lblTextblock35_Internalname = "TEXTBLOCK35";
         lblTextblock38_Internalname = "TEXTBLOCK38";
         lblTextblock36_Internalname = "TEXTBLOCK36";
         lblTextblock37_Internalname = "TEXTBLOCK37";
         lblTextblock40_Internalname = "TEXTBLOCK40";
         lblTextblock47_Internalname = "TEXTBLOCK47";
         bttBtnuseraction3_Internalname = "BTNUSERACTION3";
         lblTextblock51_Internalname = "TEXTBLOCK51";
         lblTextblock48_Internalname = "TEXTBLOCK48";
         divUnnamedtable11_Internalname = "UNNAMEDTABLE11";
         lblTextblock31_Internalname = "TEXTBLOCK31";
         lblTextblock52_Internalname = "TEXTBLOCK52";
         lblTextblock45_Internalname = "TEXTBLOCK45";
         edtavCostoplansuperior_Internalname = "vCOSTOPLANSUPERIOR";
         divUnnamedtable17_Internalname = "UNNAMEDTABLE17";
         lblTextblock55_Internalname = "TEXTBLOCK55";
         lblTextblock56_Internalname = "TEXTBLOCK56";
         lblTextblock57_Internalname = "TEXTBLOCK57";
         lblTextblock58_Internalname = "TEXTBLOCK58";
         lblTextblock59_Internalname = "TEXTBLOCK59";
         lblTextblock60_Internalname = "TEXTBLOCK60";
         lblTextblock61_Internalname = "TEXTBLOCK61";
         lblTextblock62_Internalname = "TEXTBLOCK62";
         lblTextblock63_Internalname = "TEXTBLOCK63";
         bttBtnuseraction4_Internalname = "BTNUSERACTION4";
         lblTextblock64_Internalname = "TEXTBLOCK64";
         lblTextblock65_Internalname = "TEXTBLOCK65";
         divUnnamedtable12_Internalname = "UNNAMEDTABLE12";
         lblTextblock32_Internalname = "TEXTBLOCK32";
         lblTextblock39_Internalname = "TEXTBLOCK39";
         lblTextblock53_Internalname = "TEXTBLOCK53";
         edtavCostoplannegocios_Internalname = "vCOSTOPLANNEGOCIOS";
         divUnnamedtable16_Internalname = "UNNAMEDTABLE16";
         lblTextblock68_Internalname = "TEXTBLOCK68";
         lblTextblock69_Internalname = "TEXTBLOCK69";
         lblTextblock70_Internalname = "TEXTBLOCK70";
         lblTextblock71_Internalname = "TEXTBLOCK71";
         lblTextblock72_Internalname = "TEXTBLOCK72";
         lblTextblock73_Internalname = "TEXTBLOCK73";
         lblTextblock74_Internalname = "TEXTBLOCK74";
         lblTextblock75_Internalname = "TEXTBLOCK75";
         lblTextblock76_Internalname = "TEXTBLOCK76";
         bttBtnuseraction5_Internalname = "BTNUSERACTION5";
         lblTextblock77_Internalname = "TEXTBLOCK77";
         lblTextblock78_Internalname = "TEXTBLOCK78";
         divUnnamedtable13_Internalname = "UNNAMEDTABLE13";
         lblTextblock33_Internalname = "TEXTBLOCK33";
         lblTextblock79_Internalname = "TEXTBLOCK79";
         lblTextblock92_Internalname = "TEXTBLOCK92";
         divUnnamedtable15_Internalname = "UNNAMEDTABLE15";
         lblTextblock54_Internalname = "TEXTBLOCK54";
         lblTextblock83_Internalname = "TEXTBLOCK83";
         lblTextblock84_Internalname = "TEXTBLOCK84";
         lblTextblock85_Internalname = "TEXTBLOCK85";
         lblTextblock86_Internalname = "TEXTBLOCK86";
         lblTextblock87_Internalname = "TEXTBLOCK87";
         lblTextblock81_Internalname = "TEXTBLOCK81";
         lblTextblock82_Internalname = "TEXTBLOCK82";
         lblTextblock88_Internalname = "TEXTBLOCK88";
         bttBtnuseraction6_Internalname = "BTNUSERACTION6";
         lblTextblock89_Internalname = "TEXTBLOCK89";
         divUnnamedtable14_Internalname = "UNNAMEDTABLE14";
         divUnnamedtable10_Internalname = "UNNAMEDTABLE10";
         divTableplanos_Internalname = "TABLEPLANOS";
         lblTextblock23_Internalname = "TEXTBLOCK23";
         lblTextblock49_Internalname = "TEXTBLOCK49";
         divUnnamedtable9_Internalname = "UNNAMEDTABLE9";
         divUnnamedtable8_Internalname = "UNNAMEDTABLE8";
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
         edtavCostoplannegocios_Jsonclick = "";
         edtavCostoplannegocios_Enabled = 1;
         edtavCostoplansuperior_Jsonclick = "";
         edtavCostoplansuperior_Enabled = 1;
         edtavCostoplanbasico_Jsonclick = "";
         edtavCostoplanbasico_Enabled = 1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Sitio Web", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[]}""");
         setEventMetadata("'DOUSERACTION6'","""{"handler":"E153M2","iparms":[]}""");
         setEventMetadata("'DOUSERACTION5'","""{"handler":"E133M1","iparms":[]}""");
         setEventMetadata("'DOUSERACTION4'","""{"handler":"E123M1","iparms":[]}""");
         setEventMetadata("'DOUSERACTION3'","""{"handler":"E113M1","iparms":[]}""");
         setEventMetadata("'DOUSERACTION1'","""{"handler":"E163M2","iparms":[]}""");
         setEventMetadata("'DOUSERACTION2'","""{"handler":"E173M2","iparms":[]}""");
         setEventMetadata("'BASICO'","""{"handler":"E183M2","iparms":[{"av":"AV11CostoPlanBasico","fld":"vCOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV12CuotaPlanBasico","fld":"vCUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"}]""");
         setEventMetadata("'BASICO'",""","oparms":[{"av":"AV12CuotaPlanBasico","fld":"vCUOTAPLANBASICO","pic":"ZZZZZZZZ9.99"},{"av":"AV11CostoPlanBasico","fld":"vCOSTOPLANBASICO","pic":"ZZZZZZZZ9.99"}]}""");
         setEventMetadata("'SUPERIOR'","""{"handler":"E193M2","iparms":[{"av":"AV13CostoPlanSuperior","fld":"vCOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV14CuotaPlanSuperior","fld":"vCUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"}]""");
         setEventMetadata("'SUPERIOR'",""","oparms":[{"av":"AV14CuotaPlanSuperior","fld":"vCUOTAPLANSUPERIOR","pic":"ZZZZZZZZ9.99"},{"av":"AV13CostoPlanSuperior","fld":"vCOSTOPLANSUPERIOR","pic":"ZZZZZZZZ9.99"}]}""");
         setEventMetadata("'NEGOCIOS'","""{"handler":"E203M2","iparms":[{"av":"AV15CostoPlanNegocios","fld":"vCOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV16CuotaPlanNegocios","fld":"vCUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"}]""");
         setEventMetadata("'NEGOCIOS'",""","oparms":[{"av":"AV16CuotaPlanNegocios","fld":"vCUOTAPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"},{"av":"AV15CostoPlanNegocios","fld":"vCOSTOPLANNEGOCIOS","pic":"ZZZZZZZZ9.99"}]}""");
         setEventMetadata("'EMPRESA'","""{"handler":"E213M2","iparms":[]}""");
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
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         lblTextblock1_Jsonclick = "";
         lblTextblock2_Jsonclick = "";
         lblTextblock3_Jsonclick = "";
         lblTextblock4_Jsonclick = "";
         lblTextblock5_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttBtnuseraction1_Jsonclick = "";
         bttBtnuseraction2_Jsonclick = "";
         lblTextblock8_Jsonclick = "";
         lblTextblock41_Jsonclick = "";
         imgEcommerce_gximage = "";
         sImgUrl = "";
         lblTextblock6_Jsonclick = "";
         lblTextblock9_Jsonclick = "";
         lblTextblock12_Jsonclick = "";
         imgBlog_gximage = "";
         lblTextblock10_Jsonclick = "";
         lblTextblock11_Jsonclick = "";
         lblTextblock14_Jsonclick = "";
         imgNoticias_gximage = "";
         lblTextblock7_Jsonclick = "";
         lblTextblock13_Jsonclick = "";
         lblTextblock15_Jsonclick = "";
         imgCoporativas_gximage = "";
         lblTextblock16_Jsonclick = "";
         lblTextblock17_Jsonclick = "";
         lblTextblock18_Jsonclick = "";
         imgPortafolio_gximage = "";
         lblTextblock19_Jsonclick = "";
         lblTextblock20_Jsonclick = "";
         lblTextblock21_Jsonclick = "";
         lblTextblock24_Jsonclick = "";
         lblTextblock25_Jsonclick = "";
         lblTextblock29_Jsonclick = "";
         lblTextblock28_Jsonclick = "";
         lblTextblock26_Jsonclick = "";
         lblTextblock27_Jsonclick = "";
         lblTextblock42_Jsonclick = "";
         lblTextblock22_Jsonclick = "";
         lblTextblock30_Jsonclick = "";
         lblTextblock34_Jsonclick = "";
         lblTextblock43_Jsonclick = "";
         lblTextblock44_Jsonclick = "";
         lblTextblock50_Jsonclick = "";
         lblTextblock46_Jsonclick = "";
         lblTextblock35_Jsonclick = "";
         lblTextblock38_Jsonclick = "";
         lblTextblock36_Jsonclick = "";
         lblTextblock37_Jsonclick = "";
         lblTextblock40_Jsonclick = "";
         lblTextblock47_Jsonclick = "";
         bttBtnuseraction3_Jsonclick = "";
         lblTextblock51_Jsonclick = "";
         lblTextblock48_Jsonclick = "";
         lblTextblock31_Jsonclick = "";
         lblTextblock52_Jsonclick = "";
         lblTextblock45_Jsonclick = "";
         lblTextblock55_Jsonclick = "";
         lblTextblock56_Jsonclick = "";
         lblTextblock57_Jsonclick = "";
         lblTextblock58_Jsonclick = "";
         lblTextblock59_Jsonclick = "";
         lblTextblock60_Jsonclick = "";
         lblTextblock61_Jsonclick = "";
         lblTextblock62_Jsonclick = "";
         lblTextblock63_Jsonclick = "";
         bttBtnuseraction4_Jsonclick = "";
         lblTextblock64_Jsonclick = "";
         lblTextblock65_Jsonclick = "";
         lblTextblock32_Jsonclick = "";
         lblTextblock39_Jsonclick = "";
         lblTextblock53_Jsonclick = "";
         lblTextblock68_Jsonclick = "";
         lblTextblock69_Jsonclick = "";
         lblTextblock70_Jsonclick = "";
         lblTextblock71_Jsonclick = "";
         lblTextblock72_Jsonclick = "";
         lblTextblock73_Jsonclick = "";
         lblTextblock74_Jsonclick = "";
         lblTextblock75_Jsonclick = "";
         lblTextblock76_Jsonclick = "";
         bttBtnuseraction5_Jsonclick = "";
         lblTextblock77_Jsonclick = "";
         lblTextblock78_Jsonclick = "";
         lblTextblock33_Jsonclick = "";
         lblTextblock79_Jsonclick = "";
         lblTextblock92_Jsonclick = "";
         lblTextblock54_Jsonclick = "";
         lblTextblock83_Jsonclick = "";
         lblTextblock84_Jsonclick = "";
         lblTextblock85_Jsonclick = "";
         lblTextblock86_Jsonclick = "";
         lblTextblock87_Jsonclick = "";
         lblTextblock81_Jsonclick = "";
         lblTextblock82_Jsonclick = "";
         lblTextblock88_Jsonclick = "";
         bttBtnuseraction6_Jsonclick = "";
         lblTextblock89_Jsonclick = "";
         lblTextblock23_Jsonclick = "";
         lblTextblock49_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV7WebSession = context.GetSession();
         AV10Phone = "";
         AV8Window = new GXWindow();
         GXt_char1 = "";
         AV17mensaje = "";
         GXEncryptionTmp = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
         edtavCostoplanbasico_Enabled = 0;
         edtavCostoplansuperior_Enabled = 0;
         edtavCostoplannegocios_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int edtavCostoplanbasico_Enabled ;
      private int edtavCostoplansuperior_Enabled ;
      private int edtavCostoplannegocios_Enabled ;
      private int idxLst ;
      private decimal AV12CuotaPlanBasico ;
      private decimal AV14CuotaPlanSuperior ;
      private decimal AV16CuotaPlanNegocios ;
      private decimal AV11CostoPlanBasico ;
      private decimal AV13CostoPlanSuperior ;
      private decimal AV15CostoPlanNegocios ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divTablemain_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string lblTextblock1_Internalname ;
      private string lblTextblock1_Jsonclick ;
      private string lblTextblock2_Internalname ;
      private string lblTextblock2_Jsonclick ;
      private string lblTextblock3_Internalname ;
      private string lblTextblock3_Jsonclick ;
      private string lblTextblock4_Internalname ;
      private string lblTextblock4_Jsonclick ;
      private string lblTextblock5_Internalname ;
      private string lblTextblock5_Jsonclick ;
      private string divUnnamedtable30_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtnuseraction1_Internalname ;
      private string bttBtnuseraction1_Jsonclick ;
      private string bttBtnuseraction2_Internalname ;
      private string bttBtnuseraction2_Jsonclick ;
      private string lblTextblock8_Internalname ;
      private string lblTextblock8_Jsonclick ;
      private string lblTextblock41_Internalname ;
      private string lblTextblock41_Jsonclick ;
      private string divUnnamedtable2_Internalname ;
      private string divUnnamedtable28_Internalname ;
      private string imgEcommerce_gximage ;
      private string sImgUrl ;
      private string imgEcommerce_Internalname ;
      private string divUnnamedtable29_Internalname ;
      private string lblTextblock6_Internalname ;
      private string lblTextblock6_Jsonclick ;
      private string lblTextblock9_Internalname ;
      private string lblTextblock9_Jsonclick ;
      private string lblTextblock12_Internalname ;
      private string lblTextblock12_Jsonclick ;
      private string divUnnamedtable3_Internalname ;
      private string divUnnamedtable26_Internalname ;
      private string imgBlog_gximage ;
      private string imgBlog_Internalname ;
      private string divUnnamedtable27_Internalname ;
      private string lblTextblock10_Internalname ;
      private string lblTextblock10_Jsonclick ;
      private string lblTextblock11_Internalname ;
      private string lblTextblock11_Jsonclick ;
      private string lblTextblock14_Internalname ;
      private string lblTextblock14_Jsonclick ;
      private string divUnnamedtable4_Internalname ;
      private string divUnnamedtable24_Internalname ;
      private string imgNoticias_gximage ;
      private string imgNoticias_Internalname ;
      private string divUnnamedtable25_Internalname ;
      private string lblTextblock7_Internalname ;
      private string lblTextblock7_Jsonclick ;
      private string lblTextblock13_Internalname ;
      private string lblTextblock13_Jsonclick ;
      private string lblTextblock15_Internalname ;
      private string lblTextblock15_Jsonclick ;
      private string divUnnamedtable5_Internalname ;
      private string divUnnamedtable22_Internalname ;
      private string imgCoporativas_gximage ;
      private string imgCoporativas_Internalname ;
      private string divUnnamedtable23_Internalname ;
      private string lblTextblock16_Internalname ;
      private string lblTextblock16_Jsonclick ;
      private string lblTextblock17_Internalname ;
      private string lblTextblock17_Jsonclick ;
      private string lblTextblock18_Internalname ;
      private string lblTextblock18_Jsonclick ;
      private string divUnnamedtable6_Internalname ;
      private string divUnnamedtable20_Internalname ;
      private string imgPortafolio_gximage ;
      private string imgPortafolio_Internalname ;
      private string divUnnamedtable21_Internalname ;
      private string lblTextblock19_Internalname ;
      private string lblTextblock19_Jsonclick ;
      private string lblTextblock20_Internalname ;
      private string lblTextblock20_Jsonclick ;
      private string lblTextblock21_Internalname ;
      private string lblTextblock21_Jsonclick ;
      private string divUnnamedtable7_Internalname ;
      private string divUnnamedtable19_Internalname ;
      private string lblTextblock24_Internalname ;
      private string lblTextblock24_Jsonclick ;
      private string lblTextblock25_Internalname ;
      private string lblTextblock25_Jsonclick ;
      private string lblTextblock29_Internalname ;
      private string lblTextblock29_Jsonclick ;
      private string lblTextblock28_Internalname ;
      private string lblTextblock28_Jsonclick ;
      private string lblTextblock26_Internalname ;
      private string lblTextblock26_Jsonclick ;
      private string lblTextblock27_Internalname ;
      private string lblTextblock27_Jsonclick ;
      private string lblTextblock42_Internalname ;
      private string lblTextblock42_Jsonclick ;
      private string lblTextblock22_Internalname ;
      private string lblTextblock22_Jsonclick ;
      private string divTableplanos_Internalname ;
      private string divUnnamedtable10_Internalname ;
      private string divUnnamedtable11_Internalname ;
      private string lblTextblock30_Internalname ;
      private string lblTextblock30_Jsonclick ;
      private string lblTextblock34_Internalname ;
      private string lblTextblock34_Jsonclick ;
      private string divUnnamedtable18_Internalname ;
      private string lblTextblock43_Internalname ;
      private string lblTextblock43_Jsonclick ;
      private string edtavCostoplanbasico_Internalname ;
      private string edtavCostoplanbasico_Jsonclick ;
      private string lblTextblock44_Internalname ;
      private string lblTextblock44_Jsonclick ;
      private string lblTextblock50_Internalname ;
      private string lblTextblock50_Jsonclick ;
      private string lblTextblock46_Internalname ;
      private string lblTextblock46_Jsonclick ;
      private string lblTextblock35_Internalname ;
      private string lblTextblock35_Jsonclick ;
      private string lblTextblock38_Internalname ;
      private string lblTextblock38_Jsonclick ;
      private string lblTextblock36_Internalname ;
      private string lblTextblock36_Jsonclick ;
      private string lblTextblock37_Internalname ;
      private string lblTextblock37_Jsonclick ;
      private string lblTextblock40_Internalname ;
      private string lblTextblock40_Jsonclick ;
      private string lblTextblock47_Internalname ;
      private string lblTextblock47_Jsonclick ;
      private string bttBtnuseraction3_Internalname ;
      private string bttBtnuseraction3_Jsonclick ;
      private string lblTextblock51_Internalname ;
      private string lblTextblock51_Jsonclick ;
      private string lblTextblock48_Internalname ;
      private string lblTextblock48_Jsonclick ;
      private string divUnnamedtable12_Internalname ;
      private string lblTextblock31_Internalname ;
      private string lblTextblock31_Jsonclick ;
      private string lblTextblock52_Internalname ;
      private string lblTextblock52_Jsonclick ;
      private string divUnnamedtable17_Internalname ;
      private string lblTextblock45_Internalname ;
      private string lblTextblock45_Jsonclick ;
      private string edtavCostoplansuperior_Internalname ;
      private string edtavCostoplansuperior_Jsonclick ;
      private string lblTextblock55_Internalname ;
      private string lblTextblock55_Jsonclick ;
      private string lblTextblock56_Internalname ;
      private string lblTextblock56_Jsonclick ;
      private string lblTextblock57_Internalname ;
      private string lblTextblock57_Jsonclick ;
      private string lblTextblock58_Internalname ;
      private string lblTextblock58_Jsonclick ;
      private string lblTextblock59_Internalname ;
      private string lblTextblock59_Jsonclick ;
      private string lblTextblock60_Internalname ;
      private string lblTextblock60_Jsonclick ;
      private string lblTextblock61_Internalname ;
      private string lblTextblock61_Jsonclick ;
      private string lblTextblock62_Internalname ;
      private string lblTextblock62_Jsonclick ;
      private string lblTextblock63_Internalname ;
      private string lblTextblock63_Jsonclick ;
      private string bttBtnuseraction4_Internalname ;
      private string bttBtnuseraction4_Jsonclick ;
      private string lblTextblock64_Internalname ;
      private string lblTextblock64_Jsonclick ;
      private string lblTextblock65_Internalname ;
      private string lblTextblock65_Jsonclick ;
      private string divUnnamedtable13_Internalname ;
      private string lblTextblock32_Internalname ;
      private string lblTextblock32_Jsonclick ;
      private string lblTextblock39_Internalname ;
      private string lblTextblock39_Jsonclick ;
      private string divUnnamedtable16_Internalname ;
      private string lblTextblock53_Internalname ;
      private string lblTextblock53_Jsonclick ;
      private string edtavCostoplannegocios_Internalname ;
      private string edtavCostoplannegocios_Jsonclick ;
      private string lblTextblock68_Internalname ;
      private string lblTextblock68_Jsonclick ;
      private string lblTextblock69_Internalname ;
      private string lblTextblock69_Jsonclick ;
      private string lblTextblock70_Internalname ;
      private string lblTextblock70_Jsonclick ;
      private string lblTextblock71_Internalname ;
      private string lblTextblock71_Jsonclick ;
      private string lblTextblock72_Internalname ;
      private string lblTextblock72_Jsonclick ;
      private string lblTextblock73_Internalname ;
      private string lblTextblock73_Jsonclick ;
      private string lblTextblock74_Internalname ;
      private string lblTextblock74_Jsonclick ;
      private string lblTextblock75_Internalname ;
      private string lblTextblock75_Jsonclick ;
      private string lblTextblock76_Internalname ;
      private string lblTextblock76_Jsonclick ;
      private string bttBtnuseraction5_Internalname ;
      private string bttBtnuseraction5_Jsonclick ;
      private string lblTextblock77_Internalname ;
      private string lblTextblock77_Jsonclick ;
      private string lblTextblock78_Internalname ;
      private string lblTextblock78_Jsonclick ;
      private string divUnnamedtable14_Internalname ;
      private string lblTextblock33_Internalname ;
      private string lblTextblock33_Jsonclick ;
      private string lblTextblock79_Internalname ;
      private string lblTextblock79_Jsonclick ;
      private string divUnnamedtable15_Internalname ;
      private string lblTextblock92_Internalname ;
      private string lblTextblock92_Jsonclick ;
      private string lblTextblock54_Internalname ;
      private string lblTextblock54_Jsonclick ;
      private string lblTextblock83_Internalname ;
      private string lblTextblock83_Jsonclick ;
      private string lblTextblock84_Internalname ;
      private string lblTextblock84_Jsonclick ;
      private string lblTextblock85_Internalname ;
      private string lblTextblock85_Jsonclick ;
      private string lblTextblock86_Internalname ;
      private string lblTextblock86_Jsonclick ;
      private string lblTextblock87_Internalname ;
      private string lblTextblock87_Jsonclick ;
      private string lblTextblock81_Internalname ;
      private string lblTextblock81_Jsonclick ;
      private string lblTextblock82_Internalname ;
      private string lblTextblock82_Jsonclick ;
      private string lblTextblock88_Internalname ;
      private string lblTextblock88_Jsonclick ;
      private string bttBtnuseraction6_Internalname ;
      private string bttBtnuseraction6_Jsonclick ;
      private string lblTextblock89_Internalname ;
      private string lblTextblock89_Jsonclick ;
      private string lblTextblock23_Internalname ;
      private string lblTextblock23_Jsonclick ;
      private string lblTextblock49_Internalname ;
      private string lblTextblock49_Jsonclick ;
      private string divUnnamedtable8_Internalname ;
      private string divUnnamedtable9_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV10Phone ;
      private string GXt_char1 ;
      private string AV17mensaje ;
      private string GXEncryptionTmp ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private IGxSession AV7WebSession ;
      private GXWebForm Form ;
      private GXWindow AV8Window ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
