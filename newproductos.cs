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
   public class newproductos : GXDataArea
   {
      protected void INITENV( )
      {
         if ( GxWebError != 0 )
         {
            return  ;
         }
      }

      protected void INITTRN( )
      {
         initialize_properties( ) ;
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
         else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxExecAct_"+"gxLoad_11") == 0 )
         {
            A20CategoriasId = (short)(Math.Round(NumberUtil.Val( GetPar( "CategoriasId"), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
            setAjaxCallMode();
            if ( ! IsValidAjaxCall( true) )
            {
               GxWebError = 1;
               return  ;
            }
            gxLoad_11( A20CategoriasId) ;
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
            if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "newproductos.aspx")), "newproductos.aspx") == 0 ) )
            {
               SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "newproductos.aspx")))) ;
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
                  AV13NewProductosId = (short)(Math.Round(NumberUtil.Val( GetPar( "NewProductosId"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "AV13NewProductosId", StringUtil.LTrimStr( (decimal)(AV13NewProductosId), 4, 0));
                  GxWebStd.gx_hidden_field( context, "gxhash_vNEWPRODUCTOSID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV13NewProductosId), "ZZZ9"), context));
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
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_10-184260", 0) ;
            }
         }
         Form.Meta.addItem("description", context.GetMessage( "New Productos", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         if ( ! context.isAjaxRequest( ) )
         {
            GX_FocusControl = edtNewProductosNombre_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         wbErr = false;
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      public newproductos( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newproductos( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_Gx_mode ,
                           short aP1_NewProductosId )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV13NewProductosId = aP1_NewProductosId;
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
            return "newproductos_Execute" ;
         }

      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITENV( ) ;
         INITTRN( ) ;
         if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
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

      protected void fix_multi_value_controls( )
      {
      }

      protected void Draw( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! GxWebStd.gx_redirect( context) )
         {
            disable_std_buttons( ) ;
            enableDisable( ) ;
            set_caption( ) ;
            /* Form start */
            DrawControls( ) ;
            fix_multi_value_controls( ) ;
         }
         /* Execute Exit event if defined. */
      }

      protected void DrawControls( )
      {
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
         GxWebStd.gx_div_start( context, divTablecontent_Internalname, 1, 0, "px", 0, "px", "CellMarginTop10", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 table-responsive", "start", "top", "", "", "div");
         /* User Defined Control */
         ucDvpanel_tableattributes.SetProperty("Width", Dvpanel_tableattributes_Width);
         ucDvpanel_tableattributes.SetProperty("AutoWidth", Dvpanel_tableattributes_Autowidth);
         ucDvpanel_tableattributes.SetProperty("AutoHeight", Dvpanel_tableattributes_Autoheight);
         ucDvpanel_tableattributes.SetProperty("Cls", Dvpanel_tableattributes_Cls);
         ucDvpanel_tableattributes.SetProperty("Title", Dvpanel_tableattributes_Title);
         ucDvpanel_tableattributes.SetProperty("Collapsible", Dvpanel_tableattributes_Collapsible);
         ucDvpanel_tableattributes.SetProperty("Collapsed", Dvpanel_tableattributes_Collapsed);
         ucDvpanel_tableattributes.SetProperty("ShowCollapseIcon", Dvpanel_tableattributes_Showcollapseicon);
         ucDvpanel_tableattributes.SetProperty("IconPosition", Dvpanel_tableattributes_Iconposition);
         ucDvpanel_tableattributes.SetProperty("AutoScroll", Dvpanel_tableattributes_Autoscroll);
         ucDvpanel_tableattributes.Render(context, "dvelop.gxbootstrap.panel_al", Dvpanel_tableattributes_Internalname, "DVPANEL_TABLEATTRIBUTESContainer");
         context.WriteHtmlText( "<div class=\"gx_usercontrol_child\" id=\""+"DVPANEL_TABLEATTRIBUTESContainer"+"TableAttributes"+"\" style=\"display:none;\">") ;
         /* Div Control */
         GxWebStd.gx_div_start( context, divTableattributes_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-8", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "DataContentCell", "start", "top", "", "flex-grow:1;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* User Defined Control */
         ucNewproductosdescripcion.SetProperty("Width", Newproductosdescripcion_Width);
         ucNewproductosdescripcion.SetProperty("Height", Newproductosdescripcion_Height);
         ucNewproductosdescripcion.SetProperty("Attribute", NewProductosDescripcion);
         ucNewproductosdescripcion.SetProperty("Skin", Newproductosdescripcion_Skin);
         ucNewproductosdescripcion.SetProperty("Toolbar", Newproductosdescripcion_Toolbar);
         ucNewproductosdescripcion.SetProperty("ToolbarCanCollapse", Newproductosdescripcion_Toolbarcancollapse);
         ucNewproductosdescripcion.SetProperty("ToolbarExpanded", Newproductosdescripcion_Toolbarexpanded);
         ucNewproductosdescripcion.SetProperty("Color", Newproductosdescripcion_Color);
         ucNewproductosdescripcion.SetProperty("CaptionClass", Newproductosdescripcion_Captionclass);
         ucNewproductosdescripcion.SetProperty("CaptionStyle", Newproductosdescripcion_Captionstyle);
         ucNewproductosdescripcion.SetProperty("CaptionPosition", Newproductosdescripcion_Captionposition);
         ucNewproductosdescripcion.Render(context, "fckeditor", Newproductosdescripcion_Internalname, "NEWPRODUCTOSDESCRIPCIONContainer");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divUnnamedtable2_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "flex-direction:column;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divUnnamedtable3_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtNewProductosNombre_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtNewProductosNombre_Internalname, context.GetMessage( "Nombre Producto", ""), " RowBlog_1Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'',false,'',0)\"";
         ClassString = "RowBlog_1";
         StyleString = "";
         ClassString = "RowBlog_1";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtNewProductosNombre_Internalname, A36NewProductosNombre, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"", 0, 1, edtNewProductosNombre_Enabled, 0, 80, "chr", 3, "row", 0, StyleString, ClassString, "", "", "200", -1, 0, "", "", -1, true, "GeneXusUnanimo\\Description", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtNewProductosDescripcionCorta_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtNewProductosDescripcionCorta_Internalname, context.GetMessage( "Descripcion Corta", ""), " RowBlog_2Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'',0)\"";
         ClassString = "RowBlog_2";
         StyleString = "";
         ClassString = "RowBlog_2";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtNewProductosDescripcionCorta_Internalname, A37NewProductosDescripcionCorta, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"", 0, 1, edtNewProductosDescripcionCorta_Enabled, 0, 80, "chr", 3, "row", 0, StyleString, ClassString, "", "", "200", -1, 0, "", "", -1, true, "GeneXusUnanimo\\Description", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+imgNewProductosImagen_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, "", context.GetMessage( "Imagen", ""), " RowBlog_1Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Static Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'',false,'',0)\"";
         ClassString = "RowBlog_1";
         StyleString = "";
         A35NewProductosImagen_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen))&&String.IsNullOrEmpty(StringUtil.RTrim( A40000NewProductosImagen_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.PathToRelativeUrl( A35NewProductosImagen));
         GxWebStd.gx_bitmap( context, imgNewProductosImagen_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, imgNewProductosImagen_Enabled, "", "", 0, -1, 0, "", 0, "", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,42);\"", "", "", "", 0, A35NewProductosImagen_IsBlob, true, context.GetImageSrcSet( sImgUrl), "HLP_NewProductos.htm");
         AssignProp("", false, imgNewProductosImagen_Internalname, "URL", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.PathToRelativeUrl( A35NewProductosImagen)), true);
         AssignProp("", false, imgNewProductosImagen_Internalname, "IsBlob", StringUtil.BoolToStr( A35NewProductosImagen_IsBlob), true);
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divUnnamedtable6_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divUnnamedtable7_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtNewProductosLinkDescargaDemo_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtNewProductosLinkDescargaDemo_Internalname, context.GetMessage( "Link Descarga Demo", ""), " RowProductos_1Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 52,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtNewProductosLinkDescargaDemo_Internalname, A40NewProductosLinkDescargaDemo, StringUtil.RTrim( context.localUtil.Format( A40NewProductosLinkDescargaDemo, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,52);\"", "'"+""+"'"+",false,"+"'"+""+"'", A40NewProductosLinkDescargaDemo, "_blank", "", "", edtNewProductosLinkDescargaDemo_Jsonclick, 0, "RowProductos_1", "", "", "", "", 1, edtNewProductosLinkDescargaDemo_Enabled, 0, "url", "", 80, "chr", 1, "row", 1000, 0, 0, 0, 0, -1, 0, true, "GeneXus\\Url", "start", true, "", "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtNewProductosComprar_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtNewProductosComprar_Internalname, context.GetMessage( "Link Comprar", ""), " RowProductos_1Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtNewProductosComprar_Internalname, A41NewProductosComprar, StringUtil.RTrim( context.localUtil.Format( A41NewProductosComprar, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,56);\"", "'"+""+"'"+",false,"+"'"+""+"'", A41NewProductosComprar, "_blank", "", "", edtNewProductosComprar_Jsonclick, 0, "RowProductos_1", "", "", "", "", 1, edtNewProductosComprar_Enabled, 0, "url", "", 80, "chr", 1, "row", 1000, 0, 0, 0, 0, -1, 0, true, "GeneXus\\Url", "start", true, "", "HLP_NewProductos.htm");
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divUnnamedtable4_Internalname, 1, 0, "px", 0, "px", "Flex", "start", "top", " "+"data-gx-flex"+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divUnnamedtable5_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 DscTop ExtendedComboCell", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divTablesplittedcategoriasid_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 MergeLabelCell", "start", "top", "", "", "div");
         /* Text block */
         GxWebStd.gx_label_ctrl( context, lblTextblockcategoriasid_Internalname, context.GetMessage( "Categoria", ""), "", "", lblTextblockcategoriasid_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* User Defined Control */
         ucCombo_categoriasid.SetProperty("Caption", Combo_categoriasid_Caption);
         ucCombo_categoriasid.SetProperty("Cls", Combo_categoriasid_Cls);
         ucCombo_categoriasid.SetProperty("DataListProc", Combo_categoriasid_Datalistproc);
         ucCombo_categoriasid.SetProperty("DataListProcParametersPrefix", Combo_categoriasid_Datalistprocparametersprefix);
         ucCombo_categoriasid.SetProperty("DropDownOptionsTitleSettingsIcons", AV17DDO_TitleSettingsIcons);
         ucCombo_categoriasid.SetProperty("DropDownOptionsData", AV27CategoriasId_Data);
         ucCombo_categoriasid.Render(context, "dvelop.gxbootstrap.ddoextendedcombo", Combo_categoriasid_Internalname, "COMBO_CATEGORIASIDContainer");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 Invisible", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtCategoriasId_Internalname, context.GetMessage( "Id", ""), "col-sm-3 AttributeLabel", 0, true, "");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 71,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtCategoriasId_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A20CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(A20CategoriasId), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,71);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtCategoriasId_Jsonclick, 0, "Attribute", "", "", "", "", edtCategoriasId_Visible, edtCategoriasId_Enabled, 1, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "Id", "end", false, "", "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 CellPaddingTopButton", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 73,'',false,'',0)\"";
         ClassString = "ButtonMaterialGAM";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtnuseraction1_Internalname, "", "+", bttBtnuseraction1_Jsonclick, 5, "+", "", StyleString, ClassString, bttBtnuseraction1_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSERACTION1\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
         /* Text block */
         GxWebStd.gx_label_ctrl( context, lblTextblock1_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock1_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;align-self:flex-end;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 79,'',false,'',0)\"";
         ClassString = "ButtonMaterial";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_enter_Internalname, "", context.GetMessage( "GX_BtnEnter", ""), bttBtntrn_enter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtntrn_enter_Visible, bttBtntrn_enter_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 81,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_cancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtntrn_cancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, bttBtntrn_cancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 83,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_delete_Internalname, "", context.GetMessage( "GX_BtnDelete", ""), bttBtntrn_delete_Jsonclick, 5, context.GetMessage( "GX_BtnDelete", ""), "", StyleString, ClassString, bttBtntrn_delete_Visible, bttBtntrn_delete_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EDELETE."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
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
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divHtml_bottomauxiliarcontrols_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, divSectionattribute_categoriasid_Internalname, 1, 0, "px", 0, "px", "Section", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 88,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtavCombocategoriasid_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV21ComboCategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavCombocategoriasid_Enabled!=0) ? context.localUtil.Format( (decimal)(AV21ComboCategoriasId), "ZZZ9") : context.localUtil.Format( (decimal)(AV21ComboCategoriasId), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,88);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCombocategoriasid_Jsonclick, 0, "Attribute", "", "", "", "", edtavCombocategoriasid_Visible, edtavCombocategoriasid_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 89,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtNewProductosId_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A34NewProductosId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtNewProductosId_Enabled!=0) ? context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9") : context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,89);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtNewProductosId_Jsonclick, 0, "Attribute", "", "", "", "", edtNewProductosId_Visible, edtNewProductosId_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "Id", "end", false, "", "HLP_NewProductos.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
      }

      protected void UserMain( )
      {
         standaloneStartup( ) ;
      }

      protected void UserMainFullajax( )
      {
         INITENV( ) ;
         INITTRN( ) ;
         UserMain( ) ;
         Draw( ) ;
         SendCloseFormHiddens( ) ;
      }

      protected void standaloneStartup( )
      {
         standaloneStartupServer( ) ;
         disable_std_buttons( ) ;
         enableDisable( ) ;
         Process( ) ;
      }

      protected void standaloneStartupServer( )
      {
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E11062 ();
         context.wbGlbDoneStart = 1;
         assign_properties_default( ) ;
         if ( AnyError == 0 )
         {
            if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
            {
               /* Read saved SDTs. */
               ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV17DDO_TitleSettingsIcons);
               ajax_req_read_hidden_sdt(cgiGet( "vCATEGORIASID_DATA"), AV27CategoriasId_Data);
               /* Read saved values. */
               Z34NewProductosId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z34NewProductosId"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z36NewProductosNombre = cgiGet( "Z36NewProductosNombre");
               Z37NewProductosDescripcionCorta = cgiGet( "Z37NewProductosDescripcionCorta");
               Z39NewProductosNumeroDescargas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z39NewProductosNumeroDescargas"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z40NewProductosLinkDescargaDemo = cgiGet( "Z40NewProductosLinkDescargaDemo");
               Z41NewProductosComprar = cgiGet( "Z41NewProductosComprar");
               Z42NewProductosNumeroVentas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z42NewProductosNumeroVentas"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z43NewProductosVisitas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z43NewProductosVisitas"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z20CategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z20CategoriasId"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A39NewProductosNumeroDescargas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z39NewProductosNumeroDescargas"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A42NewProductosNumeroVentas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z42NewProductosNumeroVentas"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A43NewProductosVisitas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z43NewProductosVisitas"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               IsConfirmed = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsConfirmed"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               IsModified = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsModified"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Gx_mode = cgiGet( "Mode");
               N20CategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "N20CategoriasId"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AV13NewProductosId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vNEWPRODUCTOSID"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AV14Insert_CategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vINSERT_CATEGORIASID"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A40000NewProductosImagen_GXI = cgiGet( "NEWPRODUCTOSIMAGEN_GXI");
               A38NewProductosDescripcion = cgiGet( "NEWPRODUCTOSDESCRIPCION");
               A39NewProductosNumeroDescargas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "NEWPRODUCTOSNUMERODESCARGAS"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A42NewProductosNumeroVentas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "NEWPRODUCTOSNUMEROVENTAS"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A43NewProductosVisitas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "NEWPRODUCTOSVISITAS"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AV28Pgmname = cgiGet( "vPGMNAME");
               Newproductosdescripcion_Objectcall = cgiGet( "NEWPRODUCTOSDESCRIPCION_Objectcall");
               Newproductosdescripcion_Class = cgiGet( "NEWPRODUCTOSDESCRIPCION_Class");
               Newproductosdescripcion_Enabled = StringUtil.StrToBool( cgiGet( "NEWPRODUCTOSDESCRIPCION_Enabled"));
               Newproductosdescripcion_Width = cgiGet( "NEWPRODUCTOSDESCRIPCION_Width");
               Newproductosdescripcion_Height = cgiGet( "NEWPRODUCTOSDESCRIPCION_Height");
               Newproductosdescripcion_Skin = cgiGet( "NEWPRODUCTOSDESCRIPCION_Skin");
               Newproductosdescripcion_Toolbar = cgiGet( "NEWPRODUCTOSDESCRIPCION_Toolbar");
               Newproductosdescripcion_Customtoolbar = cgiGet( "NEWPRODUCTOSDESCRIPCION_Customtoolbar");
               Newproductosdescripcion_Customconfiguration = cgiGet( "NEWPRODUCTOSDESCRIPCION_Customconfiguration");
               Newproductosdescripcion_Toolbarcancollapse = StringUtil.StrToBool( cgiGet( "NEWPRODUCTOSDESCRIPCION_Toolbarcancollapse"));
               Newproductosdescripcion_Toolbarexpanded = StringUtil.StrToBool( cgiGet( "NEWPRODUCTOSDESCRIPCION_Toolbarexpanded"));
               Newproductosdescripcion_Color = (int)(Math.Round(context.localUtil.CToN( cgiGet( "NEWPRODUCTOSDESCRIPCION_Color"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Newproductosdescripcion_Buttonpressedid = cgiGet( "NEWPRODUCTOSDESCRIPCION_Buttonpressedid");
               Newproductosdescripcion_Captionvalue = cgiGet( "NEWPRODUCTOSDESCRIPCION_Captionvalue");
               Newproductosdescripcion_Captionclass = cgiGet( "NEWPRODUCTOSDESCRIPCION_Captionclass");
               Newproductosdescripcion_Captionstyle = cgiGet( "NEWPRODUCTOSDESCRIPCION_Captionstyle");
               Newproductosdescripcion_Captionposition = cgiGet( "NEWPRODUCTOSDESCRIPCION_Captionposition");
               Newproductosdescripcion_Isabstractlayoutcontrol = StringUtil.StrToBool( cgiGet( "NEWPRODUCTOSDESCRIPCION_Isabstractlayoutcontrol"));
               Newproductosdescripcion_Coltitle = cgiGet( "NEWPRODUCTOSDESCRIPCION_Coltitle");
               Newproductosdescripcion_Coltitlefont = cgiGet( "NEWPRODUCTOSDESCRIPCION_Coltitlefont");
               Newproductosdescripcion_Coltitlecolor = (int)(Math.Round(context.localUtil.CToN( cgiGet( "NEWPRODUCTOSDESCRIPCION_Coltitlecolor"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Newproductosdescripcion_Usercontroliscolumn = StringUtil.StrToBool( cgiGet( "NEWPRODUCTOSDESCRIPCION_Usercontroliscolumn"));
               Newproductosdescripcion_Visible = StringUtil.StrToBool( cgiGet( "NEWPRODUCTOSDESCRIPCION_Visible"));
               Combo_categoriasid_Objectcall = cgiGet( "COMBO_CATEGORIASID_Objectcall");
               Combo_categoriasid_Class = cgiGet( "COMBO_CATEGORIASID_Class");
               Combo_categoriasid_Icontype = cgiGet( "COMBO_CATEGORIASID_Icontype");
               Combo_categoriasid_Icon = cgiGet( "COMBO_CATEGORIASID_Icon");
               Combo_categoriasid_Caption = cgiGet( "COMBO_CATEGORIASID_Caption");
               Combo_categoriasid_Tooltip = cgiGet( "COMBO_CATEGORIASID_Tooltip");
               Combo_categoriasid_Cls = cgiGet( "COMBO_CATEGORIASID_Cls");
               Combo_categoriasid_Selectedvalue_set = cgiGet( "COMBO_CATEGORIASID_Selectedvalue_set");
               Combo_categoriasid_Selectedvalue_get = cgiGet( "COMBO_CATEGORIASID_Selectedvalue_get");
               Combo_categoriasid_Selectedtext_set = cgiGet( "COMBO_CATEGORIASID_Selectedtext_set");
               Combo_categoriasid_Selectedtext_get = cgiGet( "COMBO_CATEGORIASID_Selectedtext_get");
               Combo_categoriasid_Gamoauthtoken = cgiGet( "COMBO_CATEGORIASID_Gamoauthtoken");
               Combo_categoriasid_Ddointernalname = cgiGet( "COMBO_CATEGORIASID_Ddointernalname");
               Combo_categoriasid_Titlecontrolalign = cgiGet( "COMBO_CATEGORIASID_Titlecontrolalign");
               Combo_categoriasid_Dropdownoptionstype = cgiGet( "COMBO_CATEGORIASID_Dropdownoptionstype");
               Combo_categoriasid_Enabled = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Enabled"));
               Combo_categoriasid_Visible = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Visible"));
               Combo_categoriasid_Titlecontrolidtoreplace = cgiGet( "COMBO_CATEGORIASID_Titlecontrolidtoreplace");
               Combo_categoriasid_Datalisttype = cgiGet( "COMBO_CATEGORIASID_Datalisttype");
               Combo_categoriasid_Allowmultipleselection = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Allowmultipleselection"));
               Combo_categoriasid_Datalistfixedvalues = cgiGet( "COMBO_CATEGORIASID_Datalistfixedvalues");
               Combo_categoriasid_Isgriditem = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Isgriditem"));
               Combo_categoriasid_Hasdescription = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Hasdescription"));
               Combo_categoriasid_Datalistproc = cgiGet( "COMBO_CATEGORIASID_Datalistproc");
               Combo_categoriasid_Datalistprocparametersprefix = cgiGet( "COMBO_CATEGORIASID_Datalistprocparametersprefix");
               Combo_categoriasid_Remoteservicesparameters = cgiGet( "COMBO_CATEGORIASID_Remoteservicesparameters");
               Combo_categoriasid_Datalistupdateminimumcharacters = (int)(Math.Round(context.localUtil.CToN( cgiGet( "COMBO_CATEGORIASID_Datalistupdateminimumcharacters"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Combo_categoriasid_Includeonlyselectedoption = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Includeonlyselectedoption"));
               Combo_categoriasid_Includeselectalloption = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Includeselectalloption"));
               Combo_categoriasid_Emptyitem = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Emptyitem"));
               Combo_categoriasid_Includeaddnewoption = StringUtil.StrToBool( cgiGet( "COMBO_CATEGORIASID_Includeaddnewoption"));
               Combo_categoriasid_Htmltemplate = cgiGet( "COMBO_CATEGORIASID_Htmltemplate");
               Combo_categoriasid_Multiplevaluestype = cgiGet( "COMBO_CATEGORIASID_Multiplevaluestype");
               Combo_categoriasid_Loadingdata = cgiGet( "COMBO_CATEGORIASID_Loadingdata");
               Combo_categoriasid_Noresultsfound = cgiGet( "COMBO_CATEGORIASID_Noresultsfound");
               Combo_categoriasid_Emptyitemtext = cgiGet( "COMBO_CATEGORIASID_Emptyitemtext");
               Combo_categoriasid_Onlyselectedvalues = cgiGet( "COMBO_CATEGORIASID_Onlyselectedvalues");
               Combo_categoriasid_Selectalltext = cgiGet( "COMBO_CATEGORIASID_Selectalltext");
               Combo_categoriasid_Multiplevaluesseparator = cgiGet( "COMBO_CATEGORIASID_Multiplevaluesseparator");
               Combo_categoriasid_Addnewoptiontext = cgiGet( "COMBO_CATEGORIASID_Addnewoptiontext");
               Dvpanel_tableattributes_Objectcall = cgiGet( "DVPANEL_TABLEATTRIBUTES_Objectcall");
               Dvpanel_tableattributes_Class = cgiGet( "DVPANEL_TABLEATTRIBUTES_Class");
               Dvpanel_tableattributes_Enabled = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Enabled"));
               Dvpanel_tableattributes_Width = cgiGet( "DVPANEL_TABLEATTRIBUTES_Width");
               Dvpanel_tableattributes_Height = cgiGet( "DVPANEL_TABLEATTRIBUTES_Height");
               Dvpanel_tableattributes_Autowidth = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Autowidth"));
               Dvpanel_tableattributes_Autoheight = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Autoheight"));
               Dvpanel_tableattributes_Cls = cgiGet( "DVPANEL_TABLEATTRIBUTES_Cls");
               Dvpanel_tableattributes_Showheader = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Showheader"));
               Dvpanel_tableattributes_Title = cgiGet( "DVPANEL_TABLEATTRIBUTES_Title");
               Dvpanel_tableattributes_Collapsible = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Collapsible"));
               Dvpanel_tableattributes_Collapsed = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Collapsed"));
               Dvpanel_tableattributes_Showcollapseicon = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Showcollapseicon"));
               Dvpanel_tableattributes_Iconposition = cgiGet( "DVPANEL_TABLEATTRIBUTES_Iconposition");
               Dvpanel_tableattributes_Autoscroll = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Autoscroll"));
               Dvpanel_tableattributes_Visible = StringUtil.StrToBool( cgiGet( "DVPANEL_TABLEATTRIBUTES_Visible"));
               Dvpanel_tableattributes_Gxcontroltype = (int)(Math.Round(context.localUtil.CToN( cgiGet( "DVPANEL_TABLEATTRIBUTES_Gxcontroltype"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               /* Read variables values. */
               A36NewProductosNombre = cgiGet( edtNewProductosNombre_Internalname);
               AssignAttri("", false, "A36NewProductosNombre", A36NewProductosNombre);
               A37NewProductosDescripcionCorta = cgiGet( edtNewProductosDescripcionCorta_Internalname);
               AssignAttri("", false, "A37NewProductosDescripcionCorta", A37NewProductosDescripcionCorta);
               A35NewProductosImagen = cgiGet( imgNewProductosImagen_Internalname);
               AssignAttri("", false, "A35NewProductosImagen", A35NewProductosImagen);
               A40NewProductosLinkDescargaDemo = cgiGet( edtNewProductosLinkDescargaDemo_Internalname);
               AssignAttri("", false, "A40NewProductosLinkDescargaDemo", A40NewProductosLinkDescargaDemo);
               A41NewProductosComprar = cgiGet( edtNewProductosComprar_Internalname);
               AssignAttri("", false, "A41NewProductosComprar", A41NewProductosComprar);
               if ( ( ( context.localUtil.CToN( cgiGet( edtCategoriasId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtCategoriasId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CATEGORIASID");
                  AnyError = 1;
                  GX_FocusControl = edtCategoriasId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A20CategoriasId = 0;
                  AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
               }
               else
               {
                  A20CategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtCategoriasId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
               }
               AV21ComboCategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCombocategoriasid_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV21ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV21ComboCategoriasId), 4, 0));
               A34NewProductosId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewProductosId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
               /* Read subfile selected row values. */
               /* Read hidden variables. */
               getMultimediaValue(imgNewProductosImagen_Internalname, ref  A35NewProductosImagen, ref  A40000NewProductosImagen_GXI);
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               forbiddenHiddens = new GXProperties();
               forbiddenHiddens.Add("hshsalt", "hsh"+"NewProductos");
               A34NewProductosId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewProductosId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
               forbiddenHiddens.Add("NewProductosId", context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9"));
               forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
               forbiddenHiddens.Add("NewProductosNumeroDescargas", context.localUtil.Format( (decimal)(A39NewProductosNumeroDescargas), "ZZZ9"));
               forbiddenHiddens.Add("NewProductosNumeroVentas", context.localUtil.Format( (decimal)(A42NewProductosNumeroVentas), "ZZZ9"));
               forbiddenHiddens.Add("NewProductosVisitas", context.localUtil.Format( (decimal)(A43NewProductosVisitas), "ZZZ9"));
               hsh = cgiGet( "hsh");
               if ( ( ! ( ( A34NewProductosId != Z34NewProductosId ) ) || ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) ) && ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
               {
                  GXUtil.WriteLogError("newproductos:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
                  GxWebError = 1;
                  context.HttpContext.Response.StatusCode = 403;
                  context.WriteHtmlText( "<title>403 Forbidden</title>") ;
                  context.WriteHtmlText( "<h1>403 Forbidden</h1>") ;
                  context.WriteHtmlText( "<p /><hr />") ;
                  GXUtil.WriteLog("send_http_error_code " + 403.ToString());
                  AnyError = 1;
                  return  ;
               }
               standaloneNotModal( ) ;
            }
            else
            {
               standaloneNotModal( ) ;
               if ( StringUtil.StrCmp(gxfirstwebparm, "viewer") == 0 )
               {
                  Gx_mode = "DSP";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  A34NewProductosId = (short)(Math.Round(NumberUtil.Val( GetPar( "NewProductosId"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
                  getEqualNoModal( ) ;
                  Gx_mode = "DSP";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  disable_std_buttons( ) ;
                  standaloneModal( ) ;
               }
               else
               {
                  if ( IsDsp( ) )
                  {
                     sMode7 = Gx_mode;
                     Gx_mode = "UPD";
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                     Gx_mode = sMode7;
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                  }
                  standaloneModal( ) ;
                  if ( ! IsIns( ) )
                  {
                     getByPrimaryKey( ) ;
                     if ( RcdFound7 == 1 )
                     {
                        if ( IsDlt( ) )
                        {
                           /* Confirm record */
                           CONFIRM_060( ) ;
                           if ( AnyError == 0 )
                           {
                              GX_FocusControl = bttBtntrn_enter_Internalname;
                              AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                           }
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_noinsert", ""), 1, "NEWPRODUCTOSID");
                        AnyError = 1;
                        GX_FocusControl = edtNewProductosId_Internalname;
                        AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     }
                  }
               }
            }
         }
      }

      protected void Process( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read Transaction buttons. */
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
                        if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: Start */
                           E11062 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "AFTER TRN") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: After Trn */
                           E12062 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'DOUSERACTION1'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'DoUserAction1' */
                           E13062 ();
                           nKeyPressed = 3;
                        }
                        else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                        {
                           context.wbHandled = 1;
                           if ( ! IsDsp( ) )
                           {
                              btn_enter( ) ;
                           }
                           /* No code required for Cancel button. It is implemented as the Reset button. */
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

      protected void AfterTrn( )
      {
         if ( trnEnded == 1 )
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( endTrnMsgTxt)) )
            {
               GX_msglist.addItem(endTrnMsgTxt, endTrnMsgCod, 0, "", true);
            }
            /* Execute user event: After Trn */
            E12062 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               /* Clear variables for new insertion. */
               InitAll067( ) ;
               standaloneNotModal( ) ;
               standaloneModal( ) ;
            }
         }
         endTrnMsgTxt = "";
      }

      public override string ToString( )
      {
         return "" ;
      }

      public GxContentInfo GetContentInfo( )
      {
         return (GxContentInfo)(null) ;
      }

      protected void disable_std_buttons( )
      {
         bttBtntrn_delete_Visible = 0;
         AssignProp("", false, bttBtntrn_delete_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Visible), 5, 0), true);
         if ( IsDsp( ) || IsDlt( ) )
         {
            bttBtntrn_delete_Visible = 0;
            AssignProp("", false, bttBtntrn_delete_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Visible), 5, 0), true);
            if ( IsDsp( ) )
            {
               bttBtntrn_enter_Visible = 0;
               AssignProp("", false, bttBtntrn_enter_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtntrn_enter_Visible), 5, 0), true);
            }
            DisableAttributes067( ) ;
         }
         AssignProp("", false, edtavCombocategoriasid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCombocategoriasid_Enabled), 5, 0), true);
      }

      protected void set_caption( )
      {
         if ( ( IsConfirmed == 1 ) && ( AnyError == 0 ) )
         {
            if ( IsDlt( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_confdelete", ""), 0, "", true);
            }
            else
            {
               GX_msglist.addItem(context.GetMessage( "GXM_mustconfirm", ""), 0, "", true);
            }
         }
      }

      protected void CONFIRM_060( )
      {
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls067( ) ;
            }
            else
            {
               CheckExtendedTable067( ) ;
               CloseExtendedTableCursors067( ) ;
            }
         }
         if ( AnyError == 0 )
         {
            IsConfirmed = 1;
            AssignAttri("", false, "IsConfirmed", StringUtil.LTrimStr( (decimal)(IsConfirmed), 4, 0));
         }
      }

      protected void ResetCaption060( )
      {
      }

      protected void E11062( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = AV17DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1) ;
         AV17DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1;
         AV22GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV23GAMErrors);
         Combo_categoriasid_Gamoauthtoken = AV22GAMSession.gxTpr_Token;
         ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "GAMOAuthToken", Combo_categoriasid_Gamoauthtoken);
         edtCategoriasId_Visible = 0;
         AssignProp("", false, edtCategoriasId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtCategoriasId_Visible), 5, 0), true);
         AV21ComboCategoriasId = 0;
         AssignAttri("", false, "AV21ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV21ComboCategoriasId), 4, 0));
         edtavCombocategoriasid_Visible = 0;
         AssignProp("", false, edtavCombocategoriasid_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavCombocategoriasid_Visible), 5, 0), true);
         /* Execute user subroutine: 'LOADCOMBOCATEGORIASID' */
         S112 ();
         if ( returnInSub )
         {
            returnInSub = true;
            if (true) return;
         }
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
         if ( ( StringUtil.StrCmp(AV11TrnContext.gxTpr_Transactionname, AV28Pgmname) == 0 ) && ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) )
         {
            AV29GXV1 = 1;
            AssignAttri("", false, "AV29GXV1", StringUtil.LTrimStr( (decimal)(AV29GXV1), 8, 0));
            while ( AV29GXV1 <= AV11TrnContext.gxTpr_Attributes.Count )
            {
               AV15TrnContextAtt = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute)AV11TrnContext.gxTpr_Attributes.Item(AV29GXV1));
               if ( StringUtil.StrCmp(AV15TrnContextAtt.gxTpr_Attributename, "CategoriasId") == 0 )
               {
                  AV14Insert_CategoriasId = (short)(Math.Round(NumberUtil.Val( AV15TrnContextAtt.gxTpr_Attributevalue, "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "AV14Insert_CategoriasId", StringUtil.LTrimStr( (decimal)(AV14Insert_CategoriasId), 4, 0));
                  if ( ! (0==AV14Insert_CategoriasId) )
                  {
                     AV21ComboCategoriasId = AV14Insert_CategoriasId;
                     AssignAttri("", false, "AV21ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV21ComboCategoriasId), 4, 0));
                     Combo_categoriasid_Selectedvalue_set = StringUtil.Trim( StringUtil.Str( (decimal)(AV21ComboCategoriasId), 4, 0));
                     ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedValue_set", Combo_categoriasid_Selectedvalue_set);
                     GXt_char2 = AV20Combo_DataJson;
                     new newproductosloaddvcombo(context ).execute(  "CategoriasId",  "GET",  false,  AV13NewProductosId,  AV15TrnContextAtt.gxTpr_Attributevalue, out  AV18ComboSelectedValue, out  AV19ComboSelectedText, out  GXt_char2) ;
                     AssignAttri("", false, "AV18ComboSelectedValue", AV18ComboSelectedValue);
                     AssignAttri("", false, "AV19ComboSelectedText", AV19ComboSelectedText);
                     AV20Combo_DataJson = GXt_char2;
                     AssignAttri("", false, "AV20Combo_DataJson", AV20Combo_DataJson);
                     Combo_categoriasid_Selectedtext_set = AV19ComboSelectedText;
                     ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedText_set", Combo_categoriasid_Selectedtext_set);
                     Combo_categoriasid_Enabled = false;
                     ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_categoriasid_Enabled));
                  }
               }
               AV29GXV1 = (int)(AV29GXV1+1);
               AssignAttri("", false, "AV29GXV1", StringUtil.LTrimStr( (decimal)(AV29GXV1), 8, 0));
            }
         }
         edtNewProductosId_Visible = 0;
         AssignProp("", false, edtNewProductosId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewProductosId_Visible), 5, 0), true);
         GXt_boolean3 = AV24IsAuthorized_UserAction1;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "categoriasww_Execute", out  GXt_boolean3) ;
         AV24IsAuthorized_UserAction1 = GXt_boolean3;
         AssignAttri("", false, "AV24IsAuthorized_UserAction1", AV24IsAuthorized_UserAction1);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_USERACTION1", GetSecureSignedToken( "", AV24IsAuthorized_UserAction1, context));
         if ( ! ( AV24IsAuthorized_UserAction1 ) )
         {
            bttBtnuseraction1_Visible = 0;
            AssignProp("", false, bttBtnuseraction1_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnuseraction1_Visible), 5, 0), true);
         }
      }

      protected void E12062( )
      {
         /* After Trn Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) && ! AV11TrnContext.gxTpr_Callerondelete )
         {
            CallWebObject(formatLink("newproductosww.aspx") );
            context.wjLocDisableFrm = 1;
         }
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void E13062( )
      {
         /* 'DoUserAction1' Routine */
         returnInSub = false;
         if ( AV24IsAuthorized_UserAction1 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "categoriasww.aspx"+UrlEncode(StringUtil.RTrim("INS"));
            context.PopUp(formatLink("categoriasww.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
         }
      }

      protected void S112( )
      {
         /* 'LOADCOMBOCATEGORIASID' Routine */
         returnInSub = false;
         GXt_char2 = AV20Combo_DataJson;
         new newproductosloaddvcombo(context ).execute(  "CategoriasId",  Gx_mode,  false,  AV13NewProductosId,  "", out  AV18ComboSelectedValue, out  AV19ComboSelectedText, out  GXt_char2) ;
         AssignAttri("", false, "AV18ComboSelectedValue", AV18ComboSelectedValue);
         AssignAttri("", false, "AV19ComboSelectedText", AV19ComboSelectedText);
         AV20Combo_DataJson = GXt_char2;
         AssignAttri("", false, "AV20Combo_DataJson", AV20Combo_DataJson);
         Combo_categoriasid_Selectedvalue_set = AV18ComboSelectedValue;
         ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedValue_set", Combo_categoriasid_Selectedvalue_set);
         Combo_categoriasid_Selectedtext_set = AV19ComboSelectedText;
         ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedText_set", Combo_categoriasid_Selectedtext_set);
         AV21ComboCategoriasId = (short)(Math.Round(NumberUtil.Val( AV18ComboSelectedValue, "."), 18, MidpointRounding.ToEven));
         AssignAttri("", false, "AV21ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV21ComboCategoriasId), 4, 0));
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            Combo_categoriasid_Enabled = false;
            ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_categoriasid_Enabled));
         }
      }

      protected void ZM067( short GX_JID )
      {
         if ( ( GX_JID == 10 ) || ( GX_JID == 0 ) )
         {
            if ( ! IsIns( ) )
            {
               Z36NewProductosNombre = T00063_A36NewProductosNombre[0];
               Z37NewProductosDescripcionCorta = T00063_A37NewProductosDescripcionCorta[0];
               Z39NewProductosNumeroDescargas = T00063_A39NewProductosNumeroDescargas[0];
               Z40NewProductosLinkDescargaDemo = T00063_A40NewProductosLinkDescargaDemo[0];
               Z41NewProductosComprar = T00063_A41NewProductosComprar[0];
               Z42NewProductosNumeroVentas = T00063_A42NewProductosNumeroVentas[0];
               Z43NewProductosVisitas = T00063_A43NewProductosVisitas[0];
               Z20CategoriasId = T00063_A20CategoriasId[0];
            }
            else
            {
               Z36NewProductosNombre = A36NewProductosNombre;
               Z37NewProductosDescripcionCorta = A37NewProductosDescripcionCorta;
               Z39NewProductosNumeroDescargas = A39NewProductosNumeroDescargas;
               Z40NewProductosLinkDescargaDemo = A40NewProductosLinkDescargaDemo;
               Z41NewProductosComprar = A41NewProductosComprar;
               Z42NewProductosNumeroVentas = A42NewProductosNumeroVentas;
               Z43NewProductosVisitas = A43NewProductosVisitas;
               Z20CategoriasId = A20CategoriasId;
            }
         }
         if ( GX_JID == -10 )
         {
            Z34NewProductosId = A34NewProductosId;
            Z35NewProductosImagen = A35NewProductosImagen;
            Z40000NewProductosImagen_GXI = A40000NewProductosImagen_GXI;
            Z36NewProductosNombre = A36NewProductosNombre;
            Z37NewProductosDescripcionCorta = A37NewProductosDescripcionCorta;
            Z38NewProductosDescripcion = A38NewProductosDescripcion;
            Z39NewProductosNumeroDescargas = A39NewProductosNumeroDescargas;
            Z40NewProductosLinkDescargaDemo = A40NewProductosLinkDescargaDemo;
            Z41NewProductosComprar = A41NewProductosComprar;
            Z42NewProductosNumeroVentas = A42NewProductosNumeroVentas;
            Z43NewProductosVisitas = A43NewProductosVisitas;
            Z20CategoriasId = A20CategoriasId;
         }
      }

      protected void standaloneNotModal( )
      {
         edtNewProductosId_Enabled = 0;
         AssignProp("", false, edtNewProductosId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewProductosId_Enabled), 5, 0), true);
         AV28Pgmname = "NewProductos";
         AssignAttri("", false, "AV28Pgmname", AV28Pgmname);
         edtNewProductosId_Enabled = 0;
         AssignProp("", false, edtNewProductosId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewProductosId_Enabled), 5, 0), true);
         bttBtntrn_delete_Enabled = 0;
         AssignProp("", false, bttBtntrn_delete_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Enabled), 5, 0), true);
         if ( ! (0==AV13NewProductosId) )
         {
            A34NewProductosId = AV13NewProductosId;
            AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (0==AV14Insert_CategoriasId) )
         {
            edtCategoriasId_Enabled = 0;
            AssignProp("", false, edtCategoriasId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtCategoriasId_Enabled), 5, 0), true);
         }
         else
         {
            edtCategoriasId_Enabled = 1;
            AssignProp("", false, edtCategoriasId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtCategoriasId_Enabled), 5, 0), true);
         }
      }

      protected void standaloneModal( )
      {
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (0==AV14Insert_CategoriasId) )
         {
            A20CategoriasId = AV14Insert_CategoriasId;
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
         }
         else
         {
            A20CategoriasId = AV21ComboCategoriasId;
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
         }
         if ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 )
         {
            bttBtntrn_enter_Enabled = 0;
            AssignProp("", false, bttBtntrn_enter_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_enter_Enabled), 5, 0), true);
         }
         else
         {
            bttBtntrn_enter_Enabled = 1;
            AssignProp("", false, bttBtntrn_enter_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_enter_Enabled), 5, 0), true);
         }
      }

      protected void Load067( )
      {
         /* Using cursor T00065 */
         pr_default.execute(3, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound7 = 1;
            A40000NewProductosImagen_GXI = T00065_A40000NewProductosImagen_GXI[0];
            AssignProp("", false, imgNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), true);
            AssignProp("", false, imgNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
            A36NewProductosNombre = T00065_A36NewProductosNombre[0];
            AssignAttri("", false, "A36NewProductosNombre", A36NewProductosNombre);
            A37NewProductosDescripcionCorta = T00065_A37NewProductosDescripcionCorta[0];
            AssignAttri("", false, "A37NewProductosDescripcionCorta", A37NewProductosDescripcionCorta);
            A38NewProductosDescripcion = T00065_A38NewProductosDescripcion[0];
            A39NewProductosNumeroDescargas = T00065_A39NewProductosNumeroDescargas[0];
            A40NewProductosLinkDescargaDemo = T00065_A40NewProductosLinkDescargaDemo[0];
            AssignAttri("", false, "A40NewProductosLinkDescargaDemo", A40NewProductosLinkDescargaDemo);
            A41NewProductosComprar = T00065_A41NewProductosComprar[0];
            AssignAttri("", false, "A41NewProductosComprar", A41NewProductosComprar);
            A42NewProductosNumeroVentas = T00065_A42NewProductosNumeroVentas[0];
            A43NewProductosVisitas = T00065_A43NewProductosVisitas[0];
            A20CategoriasId = T00065_A20CategoriasId[0];
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
            A35NewProductosImagen = T00065_A35NewProductosImagen[0];
            AssignAttri("", false, "A35NewProductosImagen", A35NewProductosImagen);
            AssignProp("", false, imgNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), true);
            AssignProp("", false, imgNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
            ZM067( -10) ;
         }
         pr_default.close(3);
         OnLoadActions067( ) ;
      }

      protected void OnLoadActions067( )
      {
      }

      protected void CheckExtendedTable067( )
      {
         Gx_BScreen = 1;
         standaloneModal( ) ;
         if ( ! ( GxRegex.IsMatch(A40NewProductosLinkDescargaDemo,"^((?:[a-zA-Z]+:(//)?)?((?:(?:[a-zA-Z]([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*)(?:\\.(?:([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*))*)|(?:(\\d{1,3}\\.){3}\\d{1,3}))(?::\\d+)?(?:/([a-zA-Z0-9$\\-_@.&+!*\"'(),=;: ]|%[0-9a-fA-F]{2})+)*/?(?:[#?](?:[a-zA-Z0-9$\\-_@.&+!*\"'(),=;: /]|%[0-9a-fA-F]{2})*)?)?\\s*$") ) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXM_DoesNotMatchRegExp", ""), context.GetMessage( "Descarga Demo", ""), "", "", "", "", "", "", "", ""), "OutOfRange", 1, "NEWPRODUCTOSLINKDESCARGADEMO");
            AnyError = 1;
            GX_FocusControl = edtNewProductosLinkDescargaDemo_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         if ( ! ( GxRegex.IsMatch(A41NewProductosComprar,"^((?:[a-zA-Z]+:(//)?)?((?:(?:[a-zA-Z]([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*)(?:\\.(?:([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*))*)|(?:(\\d{1,3}\\.){3}\\d{1,3}))(?::\\d+)?(?:/([a-zA-Z0-9$\\-_@.&+!*\"'(),=;: ]|%[0-9a-fA-F]{2})+)*/?(?:[#?](?:[a-zA-Z0-9$\\-_@.&+!*\"'(),=;: /]|%[0-9a-fA-F]{2})*)?)?\\s*$") ) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXM_DoesNotMatchRegExp", ""), context.GetMessage( "Comprar", ""), "", "", "", "", "", "", "", ""), "OutOfRange", 1, "NEWPRODUCTOSCOMPRAR");
            AnyError = 1;
            GX_FocusControl = edtNewProductosComprar_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         /* Using cursor T00064 */
         pr_default.execute(2, new Object[] {A20CategoriasId});
         if ( (pr_default.getStatus(2) == 101) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Categorias", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "CATEGORIASID");
            AnyError = 1;
            GX_FocusControl = edtCategoriasId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         pr_default.close(2);
      }

      protected void CloseExtendedTableCursors067( )
      {
         pr_default.close(2);
      }

      protected void enableDisable( )
      {
      }

      protected void gxLoad_11( short A20CategoriasId )
      {
         /* Using cursor T00066 */
         pr_default.execute(4, new Object[] {A20CategoriasId});
         if ( (pr_default.getStatus(4) == 101) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Categorias", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "CATEGORIASID");
            AnyError = 1;
            GX_FocusControl = edtCategoriasId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         GxWebStd.set_html_headers( context, 0, "", "");
         AddString( "[[") ;
         AddString( "]") ;
         if ( (pr_default.getStatus(4) == 101) )
         {
            AddString( ",") ;
            AddString( "101") ;
         }
         AddString( "]") ;
         pr_default.close(4);
      }

      protected void GetKey067( )
      {
         /* Using cursor T00067 */
         pr_default.execute(5, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(5) != 101) )
         {
            RcdFound7 = 1;
         }
         else
         {
            RcdFound7 = 0;
         }
         pr_default.close(5);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor T00063 */
         pr_default.execute(1, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM067( 10) ;
            RcdFound7 = 1;
            A34NewProductosId = T00063_A34NewProductosId[0];
            AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
            A40000NewProductosImagen_GXI = T00063_A40000NewProductosImagen_GXI[0];
            AssignProp("", false, imgNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), true);
            AssignProp("", false, imgNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
            A36NewProductosNombre = T00063_A36NewProductosNombre[0];
            AssignAttri("", false, "A36NewProductosNombre", A36NewProductosNombre);
            A37NewProductosDescripcionCorta = T00063_A37NewProductosDescripcionCorta[0];
            AssignAttri("", false, "A37NewProductosDescripcionCorta", A37NewProductosDescripcionCorta);
            A38NewProductosDescripcion = T00063_A38NewProductosDescripcion[0];
            A39NewProductosNumeroDescargas = T00063_A39NewProductosNumeroDescargas[0];
            A40NewProductosLinkDescargaDemo = T00063_A40NewProductosLinkDescargaDemo[0];
            AssignAttri("", false, "A40NewProductosLinkDescargaDemo", A40NewProductosLinkDescargaDemo);
            A41NewProductosComprar = T00063_A41NewProductosComprar[0];
            AssignAttri("", false, "A41NewProductosComprar", A41NewProductosComprar);
            A42NewProductosNumeroVentas = T00063_A42NewProductosNumeroVentas[0];
            A43NewProductosVisitas = T00063_A43NewProductosVisitas[0];
            A20CategoriasId = T00063_A20CategoriasId[0];
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
            A35NewProductosImagen = T00063_A35NewProductosImagen[0];
            AssignAttri("", false, "A35NewProductosImagen", A35NewProductosImagen);
            AssignProp("", false, imgNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), true);
            AssignProp("", false, imgNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
            Z34NewProductosId = A34NewProductosId;
            sMode7 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            Load067( ) ;
            if ( AnyError == 1 )
            {
               RcdFound7 = 0;
               InitializeNonKey067( ) ;
            }
            Gx_mode = sMode7;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         else
         {
            RcdFound7 = 0;
            InitializeNonKey067( ) ;
            sMode7 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            standaloneModal( ) ;
            Gx_mode = sMode7;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey067( ) ;
         if ( RcdFound7 == 0 )
         {
         }
         else
         {
         }
         getByPrimaryKey( ) ;
      }

      protected void move_next( )
      {
         RcdFound7 = 0;
         /* Using cursor T00068 */
         pr_default.execute(6, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(6) != 101) )
         {
            while ( (pr_default.getStatus(6) != 101) && ( ( T00068_A34NewProductosId[0] < A34NewProductosId ) ) )
            {
               pr_default.readNext(6);
            }
            if ( (pr_default.getStatus(6) != 101) && ( ( T00068_A34NewProductosId[0] > A34NewProductosId ) ) )
            {
               A34NewProductosId = T00068_A34NewProductosId[0];
               AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
               RcdFound7 = 1;
            }
         }
         pr_default.close(6);
      }

      protected void move_previous( )
      {
         RcdFound7 = 0;
         /* Using cursor T00069 */
         pr_default.execute(7, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(7) != 101) )
         {
            while ( (pr_default.getStatus(7) != 101) && ( ( T00069_A34NewProductosId[0] > A34NewProductosId ) ) )
            {
               pr_default.readNext(7);
            }
            if ( (pr_default.getStatus(7) != 101) && ( ( T00069_A34NewProductosId[0] < A34NewProductosId ) ) )
            {
               A34NewProductosId = T00069_A34NewProductosId[0];
               AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
               RcdFound7 = 1;
            }
         }
         pr_default.close(7);
      }

      protected void btn_enter( )
      {
         nKeyPressed = 1;
         GetKey067( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            GX_FocusControl = edtNewProductosNombre_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            Insert067( ) ;
            if ( AnyError == 1 )
            {
               GX_FocusControl = "";
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         else
         {
            if ( RcdFound7 == 1 )
            {
               if ( A34NewProductosId != Z34NewProductosId )
               {
                  A34NewProductosId = Z34NewProductosId;
                  AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "NEWPRODUCTOSID");
                  AnyError = 1;
                  GX_FocusControl = edtNewProductosId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else if ( IsDlt( ) )
               {
                  delete( ) ;
                  AfterTrn( ) ;
                  GX_FocusControl = edtNewProductosNombre_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else
               {
                  /* Update record */
                  Update067( ) ;
                  GX_FocusControl = edtNewProductosNombre_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
            }
            else
            {
               if ( A34NewProductosId != Z34NewProductosId )
               {
                  /* Insert record */
                  GX_FocusControl = edtNewProductosNombre_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  Insert067( ) ;
                  if ( AnyError == 1 )
                  {
                     GX_FocusControl = "";
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
               }
               else
               {
                  if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "NEWPRODUCTOSID");
                     AnyError = 1;
                     GX_FocusControl = edtNewProductosId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
                  else
                  {
                     /* Insert record */
                     GX_FocusControl = edtNewProductosNombre_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     Insert067( ) ;
                     if ( AnyError == 1 )
                     {
                        GX_FocusControl = "";
                        AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     }
                  }
               }
            }
         }
         AfterTrn( ) ;
         if ( IsUpd( ) || IsDlt( ) )
         {
            if ( AnyError == 0 )
            {
               context.nUserReturn = 1;
            }
         }
      }

      protected void btn_delete( )
      {
         if ( A34NewProductosId != Z34NewProductosId )
         {
            A34NewProductosId = Z34NewProductosId;
            AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
            GX_msglist.addItem(context.GetMessage( "GXM_getbeforedlt", ""), 1, "NEWPRODUCTOSID");
            AnyError = 1;
            GX_FocusControl = edtNewProductosId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         else
         {
            delete( ) ;
            AfterTrn( ) ;
            GX_FocusControl = edtNewProductosNombre_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         if ( AnyError != 0 )
         {
         }
      }

      protected void CheckOptimisticConcurrency067( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor T00062 */
            pr_default.execute(0, new Object[] {A34NewProductosId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewProductos"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z36NewProductosNombre, T00062_A36NewProductosNombre[0]) != 0 ) || ( StringUtil.StrCmp(Z37NewProductosDescripcionCorta, T00062_A37NewProductosDescripcionCorta[0]) != 0 ) || ( Z39NewProductosNumeroDescargas != T00062_A39NewProductosNumeroDescargas[0] ) || ( StringUtil.StrCmp(Z40NewProductosLinkDescargaDemo, T00062_A40NewProductosLinkDescargaDemo[0]) != 0 ) || ( StringUtil.StrCmp(Z41NewProductosComprar, T00062_A41NewProductosComprar[0]) != 0 ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z42NewProductosNumeroVentas != T00062_A42NewProductosNumeroVentas[0] ) || ( Z43NewProductosVisitas != T00062_A43NewProductosVisitas[0] ) || ( Z20CategoriasId != T00062_A20CategoriasId[0] ) )
            {
               if ( StringUtil.StrCmp(Z36NewProductosNombre, T00062_A36NewProductosNombre[0]) != 0 )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"NewProductosNombre");
                  GXUtil.WriteLogRaw("Old: ",Z36NewProductosNombre);
                  GXUtil.WriteLogRaw("Current: ",T00062_A36NewProductosNombre[0]);
               }
               if ( StringUtil.StrCmp(Z37NewProductosDescripcionCorta, T00062_A37NewProductosDescripcionCorta[0]) != 0 )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"NewProductosDescripcionCorta");
                  GXUtil.WriteLogRaw("Old: ",Z37NewProductosDescripcionCorta);
                  GXUtil.WriteLogRaw("Current: ",T00062_A37NewProductosDescripcionCorta[0]);
               }
               if ( Z39NewProductosNumeroDescargas != T00062_A39NewProductosNumeroDescargas[0] )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"NewProductosNumeroDescargas");
                  GXUtil.WriteLogRaw("Old: ",Z39NewProductosNumeroDescargas);
                  GXUtil.WriteLogRaw("Current: ",T00062_A39NewProductosNumeroDescargas[0]);
               }
               if ( StringUtil.StrCmp(Z40NewProductosLinkDescargaDemo, T00062_A40NewProductosLinkDescargaDemo[0]) != 0 )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"NewProductosLinkDescargaDemo");
                  GXUtil.WriteLogRaw("Old: ",Z40NewProductosLinkDescargaDemo);
                  GXUtil.WriteLogRaw("Current: ",T00062_A40NewProductosLinkDescargaDemo[0]);
               }
               if ( StringUtil.StrCmp(Z41NewProductosComprar, T00062_A41NewProductosComprar[0]) != 0 )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"NewProductosComprar");
                  GXUtil.WriteLogRaw("Old: ",Z41NewProductosComprar);
                  GXUtil.WriteLogRaw("Current: ",T00062_A41NewProductosComprar[0]);
               }
               if ( Z42NewProductosNumeroVentas != T00062_A42NewProductosNumeroVentas[0] )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"NewProductosNumeroVentas");
                  GXUtil.WriteLogRaw("Old: ",Z42NewProductosNumeroVentas);
                  GXUtil.WriteLogRaw("Current: ",T00062_A42NewProductosNumeroVentas[0]);
               }
               if ( Z43NewProductosVisitas != T00062_A43NewProductosVisitas[0] )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"NewProductosVisitas");
                  GXUtil.WriteLogRaw("Old: ",Z43NewProductosVisitas);
                  GXUtil.WriteLogRaw("Current: ",T00062_A43NewProductosVisitas[0]);
               }
               if ( Z20CategoriasId != T00062_A20CategoriasId[0] )
               {
                  GXUtil.WriteLog("newproductos:[seudo value changed for attri]"+"CategoriasId");
                  GXUtil.WriteLogRaw("Old: ",Z20CategoriasId);
                  GXUtil.WriteLogRaw("Current: ",T00062_A20CategoriasId[0]);
               }
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"NewProductos"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert067( )
      {
         if ( ! IsAuthorized("newproductos_Insert") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable067( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM067( 0) ;
            CheckOptimisticConcurrency067( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm067( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert067( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T000610 */
                     pr_default.execute(8, new Object[] {A35NewProductosImagen, A40000NewProductosImagen_GXI, A36NewProductosNombre, A37NewProductosDescripcionCorta, A38NewProductosDescripcion, A39NewProductosNumeroDescargas, A40NewProductosLinkDescargaDemo, A41NewProductosComprar, A42NewProductosNumeroVentas, A43NewProductosVisitas, A20CategoriasId});
                     pr_default.close(8);
                     /* Retrieving last key number assigned */
                     /* Using cursor T000611 */
                     pr_default.execute(9);
                     A34NewProductosId = T000611_A34NewProductosId[0];
                     AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
                     pr_default.close(9);
                     pr_default.SmartCacheProvider.SetUpdated("NewProductos");
                     if ( AnyError == 0 )
                     {
                        /* Start of After( Insert) rules */
                        /* End of After( Insert) rules */
                        if ( AnyError == 0 )
                        {
                           /* Save values for previous() function. */
                           endTrnMsgTxt = context.GetMessage( "GXM_sucadded", "");
                           endTrnMsgCod = "SuccessfullyAdded";
                           ResetCaption060( ) ;
                        }
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                     AnyError = 1;
                  }
               }
            }
            else
            {
               Load067( ) ;
            }
            EndLevel067( ) ;
         }
         CloseExtendedTableCursors067( ) ;
      }

      protected void Update067( )
      {
         if ( ! IsAuthorized("newproductos_Update") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable067( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency067( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm067( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate067( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T000612 */
                     pr_default.execute(10, new Object[] {A36NewProductosNombre, A37NewProductosDescripcionCorta, A38NewProductosDescripcion, A39NewProductosNumeroDescargas, A40NewProductosLinkDescargaDemo, A41NewProductosComprar, A42NewProductosNumeroVentas, A43NewProductosVisitas, A20CategoriasId, A34NewProductosId});
                     pr_default.close(10);
                     pr_default.SmartCacheProvider.SetUpdated("NewProductos");
                     if ( (pr_default.getStatus(10) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewProductos"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate067( ) ;
                     if ( AnyError == 0 )
                     {
                        /* Start of After( update) rules */
                        /* End of After( update) rules */
                        if ( AnyError == 0 )
                        {
                           if ( IsUpd( ) || IsDlt( ) )
                           {
                              if ( AnyError == 0 )
                              {
                                 context.nUserReturn = 1;
                              }
                           }
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                        AnyError = 1;
                     }
                  }
               }
            }
            EndLevel067( ) ;
         }
         CloseExtendedTableCursors067( ) ;
      }

      protected void DeferredUpdate067( )
      {
         if ( AnyError == 0 )
         {
            /* Using cursor T000613 */
            pr_default.execute(11, new Object[] {A35NewProductosImagen, A40000NewProductosImagen_GXI, A34NewProductosId});
            pr_default.close(11);
            pr_default.SmartCacheProvider.SetUpdated("NewProductos");
         }
      }

      protected void delete( )
      {
         if ( ! IsAuthorized("newproductos_Delete") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency067( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls067( ) ;
            AfterConfirm067( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete067( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor T000614 */
                  pr_default.execute(12, new Object[] {A34NewProductosId});
                  pr_default.close(12);
                  pr_default.SmartCacheProvider.SetUpdated("NewProductos");
                  if ( AnyError == 0 )
                  {
                     /* Start of After( delete) rules */
                     /* End of After( delete) rules */
                     if ( AnyError == 0 )
                     {
                        if ( IsUpd( ) || IsDlt( ) )
                        {
                           if ( AnyError == 0 )
                           {
                              context.nUserReturn = 1;
                           }
                        }
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                     AnyError = 1;
                  }
               }
            }
         }
         sMode7 = Gx_mode;
         Gx_mode = "DLT";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         EndLevel067( ) ;
         Gx_mode = sMode7;
         AssignAttri("", false, "Gx_mode", Gx_mode);
      }

      protected void OnDeleteControls067( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel067( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete067( ) ;
         }
         if ( AnyError == 0 )
         {
            context.CommitDataStores("newproductos",pr_default);
            if ( AnyError == 0 )
            {
               ConfirmValues060( ) ;
            }
            /* After transaction rules */
            /* Execute 'After Trn' event if defined. */
            trnEnded = 1;
         }
         else
         {
            context.RollbackDataStores("newproductos",pr_default);
         }
         IsModified = 0;
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanStart067( )
      {
         /* Scan By routine */
         /* Using cursor T000615 */
         pr_default.execute(13);
         RcdFound7 = 0;
         if ( (pr_default.getStatus(13) != 101) )
         {
            RcdFound7 = 1;
            A34NewProductosId = T000615_A34NewProductosId[0];
            AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
         }
         /* Load Subordinate Levels */
      }

      protected void ScanNext067( )
      {
         /* Scan next routine */
         pr_default.readNext(13);
         RcdFound7 = 0;
         if ( (pr_default.getStatus(13) != 101) )
         {
            RcdFound7 = 1;
            A34NewProductosId = T000615_A34NewProductosId[0];
            AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
         }
      }

      protected void ScanEnd067( )
      {
         pr_default.close(13);
      }

      protected void AfterConfirm067( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert067( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate067( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete067( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete067( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate067( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes067( )
      {
         edtNewProductosNombre_Enabled = 0;
         AssignProp("", false, edtNewProductosNombre_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewProductosNombre_Enabled), 5, 0), true);
         edtNewProductosDescripcionCorta_Enabled = 0;
         AssignProp("", false, edtNewProductosDescripcionCorta_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewProductosDescripcionCorta_Enabled), 5, 0), true);
         imgNewProductosImagen_Enabled = 0;
         AssignProp("", false, imgNewProductosImagen_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(imgNewProductosImagen_Enabled), 5, 0), true);
         edtNewProductosLinkDescargaDemo_Enabled = 0;
         AssignProp("", false, edtNewProductosLinkDescargaDemo_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewProductosLinkDescargaDemo_Enabled), 5, 0), true);
         edtNewProductosComprar_Enabled = 0;
         AssignProp("", false, edtNewProductosComprar_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewProductosComprar_Enabled), 5, 0), true);
         edtCategoriasId_Enabled = 0;
         AssignProp("", false, edtCategoriasId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtCategoriasId_Enabled), 5, 0), true);
         edtavCombocategoriasid_Enabled = 0;
         AssignProp("", false, edtavCombocategoriasid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCombocategoriasid_Enabled), 5, 0), true);
         edtNewProductosId_Enabled = 0;
         AssignProp("", false, edtNewProductosId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewProductosId_Enabled), 5, 0), true);
         Newproductosdescripcion_Enabled = Convert.ToBoolean( 0);
         AssignProp("", false, Newproductosdescripcion_Internalname, "Enabled", StringUtil.BoolToStr( Newproductosdescripcion_Enabled), true);
      }

      protected void send_integrity_lvl_hashes067( )
      {
      }

      protected void assign_properties_default( )
      {
      }

      protected void ConfirmValues060( )
      {
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
         MasterPageObj.master_styles();
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
         context.AddJavascriptSource("CKEditor/ckeditor/ckeditor.js", "", false, true);
         context.AddJavascriptSource("CKEditor/CKEditorRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
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
         bodyStyle += "-moz-opacity:0;opacity:0;";
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "newproductos.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV13NewProductosId,4,0));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("newproductos.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         forbiddenHiddens = new GXProperties();
         forbiddenHiddens.Add("hshsalt", "hsh"+"NewProductos");
         forbiddenHiddens.Add("NewProductosId", context.localUtil.Format( (decimal)(A34NewProductosId), "ZZZ9"));
         forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
         forbiddenHiddens.Add("NewProductosNumeroDescargas", context.localUtil.Format( (decimal)(A39NewProductosNumeroDescargas), "ZZZ9"));
         forbiddenHiddens.Add("NewProductosNumeroVentas", context.localUtil.Format( (decimal)(A42NewProductosNumeroVentas), "ZZZ9"));
         forbiddenHiddens.Add("NewProductosVisitas", context.localUtil.Format( (decimal)(A43NewProductosVisitas), "ZZZ9"));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("newproductos:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "Z34NewProductosId", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z34NewProductosId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z36NewProductosNombre", Z36NewProductosNombre);
         GxWebStd.gx_hidden_field( context, "Z37NewProductosDescripcionCorta", Z37NewProductosDescripcionCorta);
         GxWebStd.gx_hidden_field( context, "Z39NewProductosNumeroDescargas", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z39NewProductosNumeroDescargas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z40NewProductosLinkDescargaDemo", Z40NewProductosLinkDescargaDemo);
         GxWebStd.gx_hidden_field( context, "Z41NewProductosComprar", Z41NewProductosComprar);
         GxWebStd.gx_hidden_field( context, "Z42NewProductosNumeroVentas", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z42NewProductosNumeroVentas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z43NewProductosVisitas", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z43NewProductosVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z20CategoriasId", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z20CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsConfirmed", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsConfirmed), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsModified", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsModified), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Mode", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_Mode", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "N20CategoriasId", StringUtil.LTrim( StringUtil.NToC( (decimal)(A20CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV17DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV17DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCATEGORIASID_DATA", AV27CategoriasId_Data);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCATEGORIASID_DATA", AV27CategoriasId_Data);
         }
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vTRNCONTEXT", AV11TrnContext);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vTRNCONTEXT", AV11TrnContext);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vTRNCONTEXT", GetSecureSignedToken( "", AV11TrnContext, context));
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_USERACTION1", AV24IsAuthorized_UserAction1);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_USERACTION1", GetSecureSignedToken( "", AV24IsAuthorized_UserAction1, context));
         GxWebStd.gx_hidden_field( context, "vNEWPRODUCTOSID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13NewProductosId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vNEWPRODUCTOSID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV13NewProductosId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vINSERT_CATEGORIASID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV14Insert_CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSIMAGEN_GXI", A40000NewProductosImagen_GXI);
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION", A38NewProductosDescripcion);
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSNUMERODESCARGAS", StringUtil.LTrim( StringUtil.NToC( (decimal)(A39NewProductosNumeroDescargas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSNUMEROVENTAS", StringUtil.LTrim( StringUtil.NToC( (decimal)(A42NewProductosNumeroVentas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSVISITAS", StringUtil.LTrim( StringUtil.NToC( (decimal)(A43NewProductosVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV28Pgmname));
         GXCCtlgxBlob = "NEWPRODUCTOSIMAGEN" + "_gxBlob";
         GxWebStd.gx_hidden_field( context, GXCCtlgxBlob, A35NewProductosImagen);
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Objectcall", StringUtil.RTrim( Newproductosdescripcion_Objectcall));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Enabled", StringUtil.BoolToStr( Newproductosdescripcion_Enabled));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Width", StringUtil.RTrim( Newproductosdescripcion_Width));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Height", StringUtil.RTrim( Newproductosdescripcion_Height));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Skin", StringUtil.RTrim( Newproductosdescripcion_Skin));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Toolbar", StringUtil.RTrim( Newproductosdescripcion_Toolbar));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Toolbarcancollapse", StringUtil.BoolToStr( Newproductosdescripcion_Toolbarcancollapse));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Toolbarexpanded", StringUtil.BoolToStr( Newproductosdescripcion_Toolbarexpanded));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Color", StringUtil.LTrim( StringUtil.NToC( (decimal)(Newproductosdescripcion_Color), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Captionclass", StringUtil.RTrim( Newproductosdescripcion_Captionclass));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Captionstyle", StringUtil.RTrim( Newproductosdescripcion_Captionstyle));
         GxWebStd.gx_hidden_field( context, "NEWPRODUCTOSDESCRIPCION_Captionposition", StringUtil.RTrim( Newproductosdescripcion_Captionposition));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Objectcall", StringUtil.RTrim( Combo_categoriasid_Objectcall));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Cls", StringUtil.RTrim( Combo_categoriasid_Cls));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Selectedvalue_set", StringUtil.RTrim( Combo_categoriasid_Selectedvalue_set));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Selectedtext_set", StringUtil.RTrim( Combo_categoriasid_Selectedtext_set));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Gamoauthtoken", StringUtil.RTrim( Combo_categoriasid_Gamoauthtoken));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Enabled", StringUtil.BoolToStr( Combo_categoriasid_Enabled));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Datalistproc", StringUtil.RTrim( Combo_categoriasid_Datalistproc));
         GxWebStd.gx_hidden_field( context, "COMBO_CATEGORIASID_Datalistprocparametersprefix", StringUtil.RTrim( Combo_categoriasid_Datalistprocparametersprefix));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Objectcall", StringUtil.RTrim( Dvpanel_tableattributes_Objectcall));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Enabled", StringUtil.BoolToStr( Dvpanel_tableattributes_Enabled));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Width", StringUtil.RTrim( Dvpanel_tableattributes_Width));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Autowidth", StringUtil.BoolToStr( Dvpanel_tableattributes_Autowidth));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Autoheight", StringUtil.BoolToStr( Dvpanel_tableattributes_Autoheight));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Cls", StringUtil.RTrim( Dvpanel_tableattributes_Cls));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Title", StringUtil.RTrim( Dvpanel_tableattributes_Title));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Collapsible", StringUtil.BoolToStr( Dvpanel_tableattributes_Collapsible));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Collapsed", StringUtil.BoolToStr( Dvpanel_tableattributes_Collapsed));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Showcollapseicon", StringUtil.BoolToStr( Dvpanel_tableattributes_Showcollapseicon));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Iconposition", StringUtil.RTrim( Dvpanel_tableattributes_Iconposition));
         GxWebStd.gx_hidden_field( context, "DVPANEL_TABLEATTRIBUTES_Autoscroll", StringUtil.BoolToStr( Dvpanel_tableattributes_Autoscroll));
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken(sPrefix);
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

      public override short ExecuteStartEvent( )
      {
         standaloneStartup( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         return gxajaxcallmode ;
      }

      public override void RenderHtmlContent( )
      {
         context.WriteHtmlText( "<div") ;
         GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
         context.WriteHtmlText( ">") ;
         Draw( ) ;
         context.WriteHtmlText( "</div>") ;
      }

      public override void DispatchEvents( )
      {
         Process( ) ;
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
         GXEncryptionTmp = "newproductos.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV13NewProductosId,4,0));
         return formatLink("newproductos.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "NewProductos" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "New Productos", "") ;
      }

      protected void InitializeNonKey067( )
      {
         A20CategoriasId = 0;
         AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
         A35NewProductosImagen = "";
         AssignAttri("", false, "A35NewProductosImagen", A35NewProductosImagen);
         AssignProp("", false, imgNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), true);
         AssignProp("", false, imgNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
         A40000NewProductosImagen_GXI = "";
         AssignProp("", false, imgNewProductosImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A35NewProductosImagen)) ? A40000NewProductosImagen_GXI : context.convertURL( context.PathToRelativeUrl( A35NewProductosImagen))), true);
         AssignProp("", false, imgNewProductosImagen_Internalname, "SrcSet", context.GetImageSrcSet( A35NewProductosImagen), true);
         A36NewProductosNombre = "";
         AssignAttri("", false, "A36NewProductosNombre", A36NewProductosNombre);
         A37NewProductosDescripcionCorta = "";
         AssignAttri("", false, "A37NewProductosDescripcionCorta", A37NewProductosDescripcionCorta);
         A38NewProductosDescripcion = "";
         AssignAttri("", false, "A38NewProductosDescripcion", A38NewProductosDescripcion);
         A39NewProductosNumeroDescargas = 0;
         AssignAttri("", false, "A39NewProductosNumeroDescargas", StringUtil.LTrimStr( (decimal)(A39NewProductosNumeroDescargas), 4, 0));
         A40NewProductosLinkDescargaDemo = "";
         AssignAttri("", false, "A40NewProductosLinkDescargaDemo", A40NewProductosLinkDescargaDemo);
         A41NewProductosComprar = "";
         AssignAttri("", false, "A41NewProductosComprar", A41NewProductosComprar);
         A42NewProductosNumeroVentas = 0;
         AssignAttri("", false, "A42NewProductosNumeroVentas", StringUtil.LTrimStr( (decimal)(A42NewProductosNumeroVentas), 4, 0));
         A43NewProductosVisitas = 0;
         AssignAttri("", false, "A43NewProductosVisitas", StringUtil.LTrimStr( (decimal)(A43NewProductosVisitas), 4, 0));
         Z36NewProductosNombre = "";
         Z37NewProductosDescripcionCorta = "";
         Z39NewProductosNumeroDescargas = 0;
         Z40NewProductosLinkDescargaDemo = "";
         Z41NewProductosComprar = "";
         Z42NewProductosNumeroVentas = 0;
         Z43NewProductosVisitas = 0;
         Z20CategoriasId = 0;
      }

      protected void InitAll067( )
      {
         A34NewProductosId = 0;
         AssignAttri("", false, "A34NewProductosId", StringUtil.LTrimStr( (decimal)(A34NewProductosId), 4, 0));
         InitializeNonKey067( ) ;
      }

      protected void StandaloneModalInsert( )
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623404758", true, true);
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
         context.AddJavascriptSource("newproductos.js", "?2024121623404762", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         context.AddJavascriptSource("CKEditor/ckeditor/ckeditor.js", "", false, true);
         context.AddJavascriptSource("CKEditor/CKEditorRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/DropDownOptions/BootstrapDropDownOptionsRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_default_properties( )
      {
         Newproductosdescripcion_Internalname = "NEWPRODUCTOSDESCRIPCION";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         edtNewProductosNombre_Internalname = "NEWPRODUCTOSNOMBRE";
         edtNewProductosDescripcionCorta_Internalname = "NEWPRODUCTOSDESCRIPCIONCORTA";
         imgNewProductosImagen_Internalname = "NEWPRODUCTOSIMAGEN";
         edtNewProductosLinkDescargaDemo_Internalname = "NEWPRODUCTOSLINKDESCARGADEMO";
         edtNewProductosComprar_Internalname = "NEWPRODUCTOSCOMPRAR";
         divUnnamedtable7_Internalname = "UNNAMEDTABLE7";
         divUnnamedtable6_Internalname = "UNNAMEDTABLE6";
         divUnnamedtable3_Internalname = "UNNAMEDTABLE3";
         lblTextblockcategoriasid_Internalname = "TEXTBLOCKCATEGORIASID";
         Combo_categoriasid_Internalname = "COMBO_CATEGORIASID";
         edtCategoriasId_Internalname = "CATEGORIASID";
         divTablesplittedcategoriasid_Internalname = "TABLESPLITTEDCATEGORIASID";
         bttBtnuseraction1_Internalname = "BTNUSERACTION1";
         divUnnamedtable5_Internalname = "UNNAMEDTABLE5";
         divUnnamedtable4_Internalname = "UNNAMEDTABLE4";
         lblTextblock1_Internalname = "TEXTBLOCK1";
         bttBtntrn_enter_Internalname = "BTNTRN_ENTER";
         bttBtntrn_cancel_Internalname = "BTNTRN_CANCEL";
         bttBtntrn_delete_Internalname = "BTNTRN_DELETE";
         divUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         divTableattributes_Internalname = "TABLEATTRIBUTES";
         Dvpanel_tableattributes_Internalname = "DVPANEL_TABLEATTRIBUTES";
         divTablecontent_Internalname = "TABLECONTENT";
         divTablemain_Internalname = "TABLEMAIN";
         edtavCombocategoriasid_Internalname = "vCOMBOCATEGORIASID";
         divSectionattribute_categoriasid_Internalname = "SECTIONATTRIBUTE_CATEGORIASID";
         edtNewProductosId_Internalname = "NEWPRODUCTOSID";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
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
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "New Productos", "");
         edtNewProductosId_Jsonclick = "";
         edtNewProductosId_Enabled = 0;
         edtNewProductosId_Visible = 1;
         edtavCombocategoriasid_Jsonclick = "";
         edtavCombocategoriasid_Enabled = 0;
         edtavCombocategoriasid_Visible = 1;
         bttBtntrn_delete_Enabled = 0;
         bttBtntrn_delete_Visible = 1;
         bttBtntrn_cancel_Visible = 1;
         bttBtntrn_enter_Enabled = 1;
         bttBtntrn_enter_Visible = 1;
         bttBtnuseraction1_Visible = 1;
         edtCategoriasId_Jsonclick = "";
         edtCategoriasId_Enabled = 1;
         edtCategoriasId_Visible = 1;
         Combo_categoriasid_Datalistprocparametersprefix = " \"ComboName\": \"CategoriasId\", \"TrnMode\": \"INS\", \"IsDynamicCall\": true, \"NewProductosId\": 0";
         Combo_categoriasid_Datalistproc = "NewProductosLoadDVCombo";
         Combo_categoriasid_Cls = "ExtendedCombo Attribute";
         Combo_categoriasid_Caption = "";
         Combo_categoriasid_Enabled = Convert.ToBoolean( -1);
         edtNewProductosComprar_Jsonclick = "";
         edtNewProductosComprar_Enabled = 1;
         edtNewProductosLinkDescargaDemo_Jsonclick = "";
         edtNewProductosLinkDescargaDemo_Enabled = 1;
         imgNewProductosImagen_Enabled = 1;
         edtNewProductosDescripcionCorta_Enabled = 1;
         edtNewProductosNombre_Enabled = 1;
         Newproductosdescripcion_Captionposition = "None";
         Newproductosdescripcion_Captionstyle = "width: 25%;";
         Newproductosdescripcion_Captionclass = "gx-form-item AttributeLabel";
         Newproductosdescripcion_Color = (int)(0xD3D3D3);
         Newproductosdescripcion_Toolbarexpanded = Convert.ToBoolean( -1);
         Newproductosdescripcion_Toolbarcancollapse = Convert.ToBoolean( 0);
         Newproductosdescripcion_Toolbar = "Default";
         Newproductosdescripcion_Skin = "silver";
         Newproductosdescripcion_Height = "430";
         Newproductosdescripcion_Width = "100%";
         Newproductosdescripcion_Enabled = Convert.ToBoolean( 1);
         Dvpanel_tableattributes_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Iconposition = "Right";
         Dvpanel_tableattributes_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Collapsible = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Title = context.GetMessage( "Nuevo Producto", "");
         Dvpanel_tableattributes_Cls = "DVBootstrapResponsivePanel";
         Dvpanel_tableattributes_Autoheight = Convert.ToBoolean( -1);
         Dvpanel_tableattributes_Autowidth = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Width = "100%";
         divLayoutmaintable_Class = "Table";
         context.GX_msglist.DisplayMode = 1;
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected bool IsIns( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "INS")==0) ? true : false) ;
      }

      protected bool IsDlt( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "DLT")==0) ? true : false) ;
      }

      protected bool IsUpd( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "UPD")==0) ? true : false) ;
      }

      protected bool IsDsp( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "DSP")==0) ? true : false) ;
      }

      public void Valid_Categoriasid( )
      {
         /* Using cursor T000616 */
         pr_default.execute(14, new Object[] {A20CategoriasId});
         if ( (pr_default.getStatus(14) == 101) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Categorias", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "CATEGORIASID");
            AnyError = 1;
            GX_FocusControl = edtCategoriasId_Internalname;
         }
         pr_default.close(14);
         dynload_actions( ) ;
         /*  Sending validation outputs */
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
         setEventMetadata("ENTER","""{"handler":"UserMainFullajax","iparms":[{"postForm":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV13NewProductosId","fld":"vNEWPRODUCTOSID","pic":"ZZZ9","hsh":true}]}""");
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true},{"av":"AV24IsAuthorized_UserAction1","fld":"vISAUTHORIZED_USERACTION1","hsh":true},{"av":"AV13NewProductosId","fld":"vNEWPRODUCTOSID","pic":"ZZZ9","hsh":true},{"av":"A34NewProductosId","fld":"NEWPRODUCTOSID","pic":"ZZZ9"},{"av":"A39NewProductosNumeroDescargas","fld":"NEWPRODUCTOSNUMERODESCARGAS","pic":"ZZZ9"},{"av":"A42NewProductosNumeroVentas","fld":"NEWPRODUCTOSNUMEROVENTAS","pic":"ZZZ9"},{"av":"A43NewProductosVisitas","fld":"NEWPRODUCTOSVISITAS","pic":"ZZZ9"}]}""");
         setEventMetadata("AFTER TRN","""{"handler":"E12062","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true}]}""");
         setEventMetadata("'DOUSERACTION1'","""{"handler":"E13062","iparms":[{"av":"AV24IsAuthorized_UserAction1","fld":"vISAUTHORIZED_USERACTION1","hsh":true}]}""");
         setEventMetadata("VALID_NEWPRODUCTOSLINKDESCARGADEMO","""{"handler":"Valid_Newproductoslinkdescargademo","iparms":[]}""");
         setEventMetadata("VALID_NEWPRODUCTOSCOMPRAR","""{"handler":"Valid_Newproductoscomprar","iparms":[]}""");
         setEventMetadata("VALID_CATEGORIASID","""{"handler":"Valid_Categoriasid","iparms":[{"av":"A20CategoriasId","fld":"CATEGORIASID","pic":"ZZZ9"}]}""");
         setEventMetadata("VALIDV_COMBOCATEGORIASID","""{"handler":"Validv_Combocategoriasid","iparms":[]}""");
         setEventMetadata("VALID_NEWPRODUCTOSID","""{"handler":"Valid_Newproductosid","iparms":[]}""");
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

      protected override void CloseCursors( )
      {
         pr_default.close(1);
         pr_default.close(14);
      }

      public override void initialize( )
      {
         sPrefix = "";
         wcpOGx_mode = "";
         Z36NewProductosNombre = "";
         Z37NewProductosDescripcionCorta = "";
         Z40NewProductosLinkDescargaDemo = "";
         Z41NewProductosComprar = "";
         Combo_categoriasid_Selectedvalue_get = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         GXKey = "";
         GXDecQS = "";
         PreviousTooltip = "";
         PreviousCaption = "";
         Form = new GXWebForm();
         GX_FocusControl = "";
         ClassString = "";
         StyleString = "";
         ucDvpanel_tableattributes = new GXUserControl();
         ucNewproductosdescripcion = new GXUserControl();
         NewProductosDescripcion = "";
         TempTags = "";
         A36NewProductosNombre = "";
         A37NewProductosDescripcionCorta = "";
         A35NewProductosImagen = "";
         A40000NewProductosImagen_GXI = "";
         sImgUrl = "";
         A40NewProductosLinkDescargaDemo = "";
         A41NewProductosComprar = "";
         lblTextblockcategoriasid_Jsonclick = "";
         ucCombo_categoriasid = new GXUserControl();
         AV17DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV27CategoriasId_Data = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Item", "");
         bttBtnuseraction1_Jsonclick = "";
         lblTextblock1_Jsonclick = "";
         bttBtntrn_enter_Jsonclick = "";
         bttBtntrn_cancel_Jsonclick = "";
         bttBtntrn_delete_Jsonclick = "";
         A38NewProductosDescripcion = "";
         AV28Pgmname = "";
         Newproductosdescripcion_Objectcall = "";
         Newproductosdescripcion_Class = "";
         Newproductosdescripcion_Customtoolbar = "";
         Newproductosdescripcion_Customconfiguration = "";
         Newproductosdescripcion_Buttonpressedid = "";
         Newproductosdescripcion_Captionvalue = "";
         Newproductosdescripcion_Coltitle = "";
         Newproductosdescripcion_Coltitlefont = "";
         Combo_categoriasid_Objectcall = "";
         Combo_categoriasid_Class = "";
         Combo_categoriasid_Icontype = "";
         Combo_categoriasid_Icon = "";
         Combo_categoriasid_Tooltip = "";
         Combo_categoriasid_Selectedvalue_set = "";
         Combo_categoriasid_Selectedtext_set = "";
         Combo_categoriasid_Selectedtext_get = "";
         Combo_categoriasid_Gamoauthtoken = "";
         Combo_categoriasid_Ddointernalname = "";
         Combo_categoriasid_Titlecontrolalign = "";
         Combo_categoriasid_Dropdownoptionstype = "";
         Combo_categoriasid_Titlecontrolidtoreplace = "";
         Combo_categoriasid_Datalisttype = "";
         Combo_categoriasid_Datalistfixedvalues = "";
         Combo_categoriasid_Remoteservicesparameters = "";
         Combo_categoriasid_Htmltemplate = "";
         Combo_categoriasid_Multiplevaluestype = "";
         Combo_categoriasid_Loadingdata = "";
         Combo_categoriasid_Noresultsfound = "";
         Combo_categoriasid_Emptyitemtext = "";
         Combo_categoriasid_Onlyselectedvalues = "";
         Combo_categoriasid_Selectalltext = "";
         Combo_categoriasid_Multiplevaluesseparator = "";
         Combo_categoriasid_Addnewoptiontext = "";
         Dvpanel_tableattributes_Objectcall = "";
         Dvpanel_tableattributes_Class = "";
         Dvpanel_tableattributes_Height = "";
         forbiddenHiddens = new GXProperties();
         hsh = "";
         sMode7 = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         AV8WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV22GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV23GAMErrors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV11TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV12WebSession = context.GetSession();
         AV15TrnContextAtt = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute(context);
         AV20Combo_DataJson = "";
         AV18ComboSelectedValue = "";
         AV19ComboSelectedText = "";
         GXEncryptionTmp = "";
         GXt_char2 = "";
         Z35NewProductosImagen = "";
         Z40000NewProductosImagen_GXI = "";
         Z38NewProductosDescripcion = "";
         T00065_A34NewProductosId = new short[1] ;
         T00065_A40000NewProductosImagen_GXI = new string[] {""} ;
         T00065_A36NewProductosNombre = new string[] {""} ;
         T00065_A37NewProductosDescripcionCorta = new string[] {""} ;
         T00065_A38NewProductosDescripcion = new string[] {""} ;
         T00065_A39NewProductosNumeroDescargas = new short[1] ;
         T00065_A40NewProductosLinkDescargaDemo = new string[] {""} ;
         T00065_A41NewProductosComprar = new string[] {""} ;
         T00065_A42NewProductosNumeroVentas = new short[1] ;
         T00065_A43NewProductosVisitas = new short[1] ;
         T00065_A20CategoriasId = new short[1] ;
         T00065_A35NewProductosImagen = new string[] {""} ;
         T00064_A20CategoriasId = new short[1] ;
         T00066_A20CategoriasId = new short[1] ;
         T00067_A34NewProductosId = new short[1] ;
         T00063_A34NewProductosId = new short[1] ;
         T00063_A40000NewProductosImagen_GXI = new string[] {""} ;
         T00063_A36NewProductosNombre = new string[] {""} ;
         T00063_A37NewProductosDescripcionCorta = new string[] {""} ;
         T00063_A38NewProductosDescripcion = new string[] {""} ;
         T00063_A39NewProductosNumeroDescargas = new short[1] ;
         T00063_A40NewProductosLinkDescargaDemo = new string[] {""} ;
         T00063_A41NewProductosComprar = new string[] {""} ;
         T00063_A42NewProductosNumeroVentas = new short[1] ;
         T00063_A43NewProductosVisitas = new short[1] ;
         T00063_A20CategoriasId = new short[1] ;
         T00063_A35NewProductosImagen = new string[] {""} ;
         T00068_A34NewProductosId = new short[1] ;
         T00069_A34NewProductosId = new short[1] ;
         T00062_A34NewProductosId = new short[1] ;
         T00062_A40000NewProductosImagen_GXI = new string[] {""} ;
         T00062_A36NewProductosNombre = new string[] {""} ;
         T00062_A37NewProductosDescripcionCorta = new string[] {""} ;
         T00062_A38NewProductosDescripcion = new string[] {""} ;
         T00062_A39NewProductosNumeroDescargas = new short[1] ;
         T00062_A40NewProductosLinkDescargaDemo = new string[] {""} ;
         T00062_A41NewProductosComprar = new string[] {""} ;
         T00062_A42NewProductosNumeroVentas = new short[1] ;
         T00062_A43NewProductosVisitas = new short[1] ;
         T00062_A20CategoriasId = new short[1] ;
         T00062_A35NewProductosImagen = new string[] {""} ;
         T000611_A34NewProductosId = new short[1] ;
         T000615_A34NewProductosId = new short[1] ;
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXCCtlgxBlob = "";
         T000616_A20CategoriasId = new short[1] ;
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.newproductos__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newproductos__default(),
            new Object[][] {
                new Object[] {
               T00062_A34NewProductosId, T00062_A40000NewProductosImagen_GXI, T00062_A36NewProductosNombre, T00062_A37NewProductosDescripcionCorta, T00062_A38NewProductosDescripcion, T00062_A39NewProductosNumeroDescargas, T00062_A40NewProductosLinkDescargaDemo, T00062_A41NewProductosComprar, T00062_A42NewProductosNumeroVentas, T00062_A43NewProductosVisitas,
               T00062_A20CategoriasId, T00062_A35NewProductosImagen
               }
               , new Object[] {
               T00063_A34NewProductosId, T00063_A40000NewProductosImagen_GXI, T00063_A36NewProductosNombre, T00063_A37NewProductosDescripcionCorta, T00063_A38NewProductosDescripcion, T00063_A39NewProductosNumeroDescargas, T00063_A40NewProductosLinkDescargaDemo, T00063_A41NewProductosComprar, T00063_A42NewProductosNumeroVentas, T00063_A43NewProductosVisitas,
               T00063_A20CategoriasId, T00063_A35NewProductosImagen
               }
               , new Object[] {
               T00064_A20CategoriasId
               }
               , new Object[] {
               T00065_A34NewProductosId, T00065_A40000NewProductosImagen_GXI, T00065_A36NewProductosNombre, T00065_A37NewProductosDescripcionCorta, T00065_A38NewProductosDescripcion, T00065_A39NewProductosNumeroDescargas, T00065_A40NewProductosLinkDescargaDemo, T00065_A41NewProductosComprar, T00065_A42NewProductosNumeroVentas, T00065_A43NewProductosVisitas,
               T00065_A20CategoriasId, T00065_A35NewProductosImagen
               }
               , new Object[] {
               T00066_A20CategoriasId
               }
               , new Object[] {
               T00067_A34NewProductosId
               }
               , new Object[] {
               T00068_A34NewProductosId
               }
               , new Object[] {
               T00069_A34NewProductosId
               }
               , new Object[] {
               }
               , new Object[] {
               T000611_A34NewProductosId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               T000615_A34NewProductosId
               }
               , new Object[] {
               T000616_A20CategoriasId
               }
            }
         );
         AV28Pgmname = "NewProductos";
      }

      private short wcpOAV13NewProductosId ;
      private short Z34NewProductosId ;
      private short Z39NewProductosNumeroDescargas ;
      private short Z42NewProductosNumeroVentas ;
      private short Z43NewProductosVisitas ;
      private short Z20CategoriasId ;
      private short N20CategoriasId ;
      private short GxWebError ;
      private short A20CategoriasId ;
      private short AV13NewProductosId ;
      private short AnyError ;
      private short IsModified ;
      private short IsConfirmed ;
      private short nKeyPressed ;
      private short AV21ComboCategoriasId ;
      private short A34NewProductosId ;
      private short A39NewProductosNumeroDescargas ;
      private short A42NewProductosNumeroVentas ;
      private short A43NewProductosVisitas ;
      private short AV14Insert_CategoriasId ;
      private short RcdFound7 ;
      private short gxcookieaux ;
      private short Gx_BScreen ;
      private short gxajaxcallmode ;
      private int trnEnded ;
      private int Newproductosdescripcion_Color ;
      private int edtNewProductosNombre_Enabled ;
      private int edtNewProductosDescripcionCorta_Enabled ;
      private int imgNewProductosImagen_Enabled ;
      private int edtNewProductosLinkDescargaDemo_Enabled ;
      private int edtNewProductosComprar_Enabled ;
      private int edtCategoriasId_Visible ;
      private int edtCategoriasId_Enabled ;
      private int bttBtnuseraction1_Visible ;
      private int bttBtntrn_enter_Visible ;
      private int bttBtntrn_enter_Enabled ;
      private int bttBtntrn_cancel_Visible ;
      private int bttBtntrn_delete_Visible ;
      private int bttBtntrn_delete_Enabled ;
      private int edtavCombocategoriasid_Enabled ;
      private int edtavCombocategoriasid_Visible ;
      private int edtNewProductosId_Enabled ;
      private int edtNewProductosId_Visible ;
      private int Newproductosdescripcion_Coltitlecolor ;
      private int Combo_categoriasid_Datalistupdateminimumcharacters ;
      private int Dvpanel_tableattributes_Gxcontroltype ;
      private int AV29GXV1 ;
      private int idxLst ;
      private string sPrefix ;
      private string wcpOGx_mode ;
      private string Combo_categoriasid_Selectedvalue_get ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string GXKey ;
      private string GXDecQS ;
      private string Gx_mode ;
      private string PreviousTooltip ;
      private string PreviousCaption ;
      private string GX_FocusControl ;
      private string edtNewProductosNombre_Internalname ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string divTablecontent_Internalname ;
      private string Dvpanel_tableattributes_Width ;
      private string Dvpanel_tableattributes_Cls ;
      private string Dvpanel_tableattributes_Title ;
      private string Dvpanel_tableattributes_Iconposition ;
      private string Dvpanel_tableattributes_Internalname ;
      private string divTableattributes_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string Newproductosdescripcion_Width ;
      private string Newproductosdescripcion_Height ;
      private string Newproductosdescripcion_Skin ;
      private string Newproductosdescripcion_Toolbar ;
      private string Newproductosdescripcion_Captionclass ;
      private string Newproductosdescripcion_Captionstyle ;
      private string Newproductosdescripcion_Captionposition ;
      private string Newproductosdescripcion_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string divUnnamedtable3_Internalname ;
      private string TempTags ;
      private string edtNewProductosDescripcionCorta_Internalname ;
      private string imgNewProductosImagen_Internalname ;
      private string sImgUrl ;
      private string divUnnamedtable6_Internalname ;
      private string divUnnamedtable7_Internalname ;
      private string edtNewProductosLinkDescargaDemo_Internalname ;
      private string edtNewProductosLinkDescargaDemo_Jsonclick ;
      private string edtNewProductosComprar_Internalname ;
      private string edtNewProductosComprar_Jsonclick ;
      private string divUnnamedtable4_Internalname ;
      private string divUnnamedtable5_Internalname ;
      private string divTablesplittedcategoriasid_Internalname ;
      private string lblTextblockcategoriasid_Internalname ;
      private string lblTextblockcategoriasid_Jsonclick ;
      private string Combo_categoriasid_Caption ;
      private string Combo_categoriasid_Cls ;
      private string Combo_categoriasid_Datalistproc ;
      private string Combo_categoriasid_Datalistprocparametersprefix ;
      private string Combo_categoriasid_Internalname ;
      private string edtCategoriasId_Internalname ;
      private string edtCategoriasId_Jsonclick ;
      private string bttBtnuseraction1_Internalname ;
      private string bttBtnuseraction1_Jsonclick ;
      private string lblTextblock1_Internalname ;
      private string lblTextblock1_Jsonclick ;
      private string bttBtntrn_enter_Internalname ;
      private string bttBtntrn_enter_Jsonclick ;
      private string bttBtntrn_cancel_Internalname ;
      private string bttBtntrn_cancel_Jsonclick ;
      private string bttBtntrn_delete_Internalname ;
      private string bttBtntrn_delete_Jsonclick ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string divSectionattribute_categoriasid_Internalname ;
      private string edtavCombocategoriasid_Internalname ;
      private string edtavCombocategoriasid_Jsonclick ;
      private string edtNewProductosId_Internalname ;
      private string edtNewProductosId_Jsonclick ;
      private string AV28Pgmname ;
      private string Newproductosdescripcion_Objectcall ;
      private string Newproductosdescripcion_Class ;
      private string Newproductosdescripcion_Customtoolbar ;
      private string Newproductosdescripcion_Customconfiguration ;
      private string Newproductosdescripcion_Buttonpressedid ;
      private string Newproductosdescripcion_Captionvalue ;
      private string Newproductosdescripcion_Coltitle ;
      private string Newproductosdescripcion_Coltitlefont ;
      private string Combo_categoriasid_Objectcall ;
      private string Combo_categoriasid_Class ;
      private string Combo_categoriasid_Icontype ;
      private string Combo_categoriasid_Icon ;
      private string Combo_categoriasid_Tooltip ;
      private string Combo_categoriasid_Selectedvalue_set ;
      private string Combo_categoriasid_Selectedtext_set ;
      private string Combo_categoriasid_Selectedtext_get ;
      private string Combo_categoriasid_Gamoauthtoken ;
      private string Combo_categoriasid_Ddointernalname ;
      private string Combo_categoriasid_Titlecontrolalign ;
      private string Combo_categoriasid_Dropdownoptionstype ;
      private string Combo_categoriasid_Titlecontrolidtoreplace ;
      private string Combo_categoriasid_Datalisttype ;
      private string Combo_categoriasid_Datalistfixedvalues ;
      private string Combo_categoriasid_Remoteservicesparameters ;
      private string Combo_categoriasid_Htmltemplate ;
      private string Combo_categoriasid_Multiplevaluestype ;
      private string Combo_categoriasid_Loadingdata ;
      private string Combo_categoriasid_Noresultsfound ;
      private string Combo_categoriasid_Emptyitemtext ;
      private string Combo_categoriasid_Onlyselectedvalues ;
      private string Combo_categoriasid_Selectalltext ;
      private string Combo_categoriasid_Multiplevaluesseparator ;
      private string Combo_categoriasid_Addnewoptiontext ;
      private string Dvpanel_tableattributes_Objectcall ;
      private string Dvpanel_tableattributes_Class ;
      private string Dvpanel_tableattributes_Height ;
      private string hsh ;
      private string sMode7 ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string GXEncryptionTmp ;
      private string GXt_char2 ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXCCtlgxBlob ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbErr ;
      private bool Dvpanel_tableattributes_Autowidth ;
      private bool Dvpanel_tableattributes_Autoheight ;
      private bool Dvpanel_tableattributes_Collapsible ;
      private bool Dvpanel_tableattributes_Collapsed ;
      private bool Dvpanel_tableattributes_Showcollapseicon ;
      private bool Dvpanel_tableattributes_Autoscroll ;
      private bool Newproductosdescripcion_Toolbarcancollapse ;
      private bool Newproductosdescripcion_Toolbarexpanded ;
      private bool A35NewProductosImagen_IsBlob ;
      private bool Newproductosdescripcion_Enabled ;
      private bool Newproductosdescripcion_Isabstractlayoutcontrol ;
      private bool Newproductosdescripcion_Usercontroliscolumn ;
      private bool Newproductosdescripcion_Visible ;
      private bool Combo_categoriasid_Enabled ;
      private bool Combo_categoriasid_Visible ;
      private bool Combo_categoriasid_Allowmultipleselection ;
      private bool Combo_categoriasid_Isgriditem ;
      private bool Combo_categoriasid_Hasdescription ;
      private bool Combo_categoriasid_Includeonlyselectedoption ;
      private bool Combo_categoriasid_Includeselectalloption ;
      private bool Combo_categoriasid_Emptyitem ;
      private bool Combo_categoriasid_Includeaddnewoption ;
      private bool Dvpanel_tableattributes_Enabled ;
      private bool Dvpanel_tableattributes_Showheader ;
      private bool Dvpanel_tableattributes_Visible ;
      private bool returnInSub ;
      private bool AV24IsAuthorized_UserAction1 ;
      private bool GXt_boolean3 ;
      private bool Gx_longc ;
      private string NewProductosDescripcion ;
      private string A38NewProductosDescripcion ;
      private string AV20Combo_DataJson ;
      private string Z38NewProductosDescripcion ;
      private string Z36NewProductosNombre ;
      private string Z37NewProductosDescripcionCorta ;
      private string Z40NewProductosLinkDescargaDemo ;
      private string Z41NewProductosComprar ;
      private string A36NewProductosNombre ;
      private string A37NewProductosDescripcionCorta ;
      private string A40000NewProductosImagen_GXI ;
      private string A40NewProductosLinkDescargaDemo ;
      private string A41NewProductosComprar ;
      private string AV18ComboSelectedValue ;
      private string AV19ComboSelectedText ;
      private string Z40000NewProductosImagen_GXI ;
      private string A35NewProductosImagen ;
      private string Z35NewProductosImagen ;
      private IGxSession AV12WebSession ;
      private GXProperties forbiddenHiddens ;
      private GXUserControl ucDvpanel_tableattributes ;
      private GXUserControl ucNewproductosdescripcion ;
      private GXUserControl ucCombo_categoriasid ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV17DDO_TitleSettingsIcons ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> AV27CategoriasId_Data ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV22GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV23GAMErrors ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV11TrnContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute AV15TrnContextAtt ;
      private IDataStoreProvider pr_default ;
      private short[] T00065_A34NewProductosId ;
      private string[] T00065_A40000NewProductosImagen_GXI ;
      private string[] T00065_A36NewProductosNombre ;
      private string[] T00065_A37NewProductosDescripcionCorta ;
      private string[] T00065_A38NewProductosDescripcion ;
      private short[] T00065_A39NewProductosNumeroDescargas ;
      private string[] T00065_A40NewProductosLinkDescargaDemo ;
      private string[] T00065_A41NewProductosComprar ;
      private short[] T00065_A42NewProductosNumeroVentas ;
      private short[] T00065_A43NewProductosVisitas ;
      private short[] T00065_A20CategoriasId ;
      private string[] T00065_A35NewProductosImagen ;
      private short[] T00064_A20CategoriasId ;
      private short[] T00066_A20CategoriasId ;
      private short[] T00067_A34NewProductosId ;
      private short[] T00063_A34NewProductosId ;
      private string[] T00063_A40000NewProductosImagen_GXI ;
      private string[] T00063_A36NewProductosNombre ;
      private string[] T00063_A37NewProductosDescripcionCorta ;
      private string[] T00063_A38NewProductosDescripcion ;
      private short[] T00063_A39NewProductosNumeroDescargas ;
      private string[] T00063_A40NewProductosLinkDescargaDemo ;
      private string[] T00063_A41NewProductosComprar ;
      private short[] T00063_A42NewProductosNumeroVentas ;
      private short[] T00063_A43NewProductosVisitas ;
      private short[] T00063_A20CategoriasId ;
      private string[] T00063_A35NewProductosImagen ;
      private short[] T00068_A34NewProductosId ;
      private short[] T00069_A34NewProductosId ;
      private short[] T00062_A34NewProductosId ;
      private string[] T00062_A40000NewProductosImagen_GXI ;
      private string[] T00062_A36NewProductosNombre ;
      private string[] T00062_A37NewProductosDescripcionCorta ;
      private string[] T00062_A38NewProductosDescripcion ;
      private short[] T00062_A39NewProductosNumeroDescargas ;
      private string[] T00062_A40NewProductosLinkDescargaDemo ;
      private string[] T00062_A41NewProductosComprar ;
      private short[] T00062_A42NewProductosNumeroVentas ;
      private short[] T00062_A43NewProductosVisitas ;
      private short[] T00062_A20CategoriasId ;
      private string[] T00062_A35NewProductosImagen ;
      private short[] T000611_A34NewProductosId ;
      private short[] T000615_A34NewProductosId ;
      private short[] T000616_A20CategoriasId ;
      private IDataStoreProvider pr_gam ;
   }

   public class newproductos__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class newproductos__default : DataStoreHelperBase, IDataStoreHelper
 {
    public ICursor[] getCursors( )
    {
       cursorDefinitions();
       return new Cursor[] {
        new ForEachCursor(def[0])
       ,new ForEachCursor(def[1])
       ,new ForEachCursor(def[2])
       ,new ForEachCursor(def[3])
       ,new ForEachCursor(def[4])
       ,new ForEachCursor(def[5])
       ,new ForEachCursor(def[6])
       ,new ForEachCursor(def[7])
       ,new UpdateCursor(def[8])
       ,new ForEachCursor(def[9])
       ,new UpdateCursor(def[10])
       ,new UpdateCursor(def[11])
       ,new UpdateCursor(def[12])
       ,new ForEachCursor(def[13])
       ,new ForEachCursor(def[14])
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        Object[] prmT00062;
        prmT00062 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT00063;
        prmT00063 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT00064;
        prmT00064 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmT00065;
        prmT00065 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT00066;
        prmT00066 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmT00067;
        prmT00067 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT00068;
        prmT00068 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT00069;
        prmT00069 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT000610;
        prmT000610 = new Object[] {
        new ParDef("@NewProductosImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewProductosImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewProductos", Fld="NewProductosImagen"} ,
        new ParDef("@NewProductosNombre",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcionCorta",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcion",GXType.Char,2097152,0) ,
        new ParDef("@NewProductosNumeroDescargas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosLinkDescargaDemo",GXType.Char,1000,0) ,
        new ParDef("@NewProductosComprar",GXType.Char,1000,0) ,
        new ParDef("@NewProductosNumeroVentas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosVisitas",GXType.Int16,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmT000611;
        prmT000611 = new Object[] {
        };
        Object[] prmT000612;
        prmT000612 = new Object[] {
        new ParDef("@NewProductosNombre",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcionCorta",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcion",GXType.Char,2097152,0) ,
        new ParDef("@NewProductosNumeroDescargas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosLinkDescargaDemo",GXType.Char,1000,0) ,
        new ParDef("@NewProductosComprar",GXType.Char,1000,0) ,
        new ParDef("@NewProductosNumeroVentas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosVisitas",GXType.Int16,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0) ,
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT000613;
        prmT000613 = new Object[] {
        new ParDef("@NewProductosImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewProductosImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewProductos", Fld="NewProductosImagen"} ,
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT000614;
        prmT000614 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmT000615;
        prmT000615 = new Object[] {
        };
        Object[] prmT000616;
        prmT000616 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        def= new CursorDef[] {
            new CursorDef("T00062", "SELECT `NewProductosId`, `NewProductosImagen_GXI`, `NewProductosNombre`, `NewProductosDescripcionCorta`, `NewProductosDescripcion`, `NewProductosNumeroDescargas`, `NewProductosLinkDescargaDemo`, `NewProductosComprar`, `NewProductosNumeroVentas`, `NewProductosVisitas`, `CategoriasId`, `NewProductosImagen` FROM `NewProductos` WHERE `NewProductosId` = @NewProductosId  FOR UPDATE ",true, GxErrorMask.GX_NOMASK, false, this,prmT00062,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00063", "SELECT `NewProductosId`, `NewProductosImagen_GXI`, `NewProductosNombre`, `NewProductosDescripcionCorta`, `NewProductosDescripcion`, `NewProductosNumeroDescargas`, `NewProductosLinkDescargaDemo`, `NewProductosComprar`, `NewProductosNumeroVentas`, `NewProductosVisitas`, `CategoriasId`, `NewProductosImagen` FROM `NewProductos` WHERE `NewProductosId` = @NewProductosId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00063,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00064", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00064,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00065", "SELECT TM1.`NewProductosId`, TM1.`NewProductosImagen_GXI`, TM1.`NewProductosNombre`, TM1.`NewProductosDescripcionCorta`, TM1.`NewProductosDescripcion`, TM1.`NewProductosNumeroDescargas`, TM1.`NewProductosLinkDescargaDemo`, TM1.`NewProductosComprar`, TM1.`NewProductosNumeroVentas`, TM1.`NewProductosVisitas`, TM1.`CategoriasId`, TM1.`NewProductosImagen` FROM `NewProductos` TM1 WHERE TM1.`NewProductosId` = @NewProductosId ORDER BY TM1.`NewProductosId` ",true, GxErrorMask.GX_NOMASK, false, this,prmT00065,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00066", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00066,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00067", "SELECT `NewProductosId` FROM `NewProductos` WHERE `NewProductosId` = @NewProductosId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00067,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00068", "SELECT `NewProductosId` FROM `NewProductos` WHERE ( `NewProductosId` > @NewProductosId) ORDER BY `NewProductosId`  LIMIT 1",true, GxErrorMask.GX_NOMASK, false, this,prmT00068,1, GxCacheFrequency.OFF ,true,true )
           ,new CursorDef("T00069", "SELECT `NewProductosId` FROM `NewProductos` WHERE ( `NewProductosId` < @NewProductosId) ORDER BY `NewProductosId` DESC  LIMIT 1",true, GxErrorMask.GX_NOMASK, false, this,prmT00069,1, GxCacheFrequency.OFF ,true,true )
           ,new CursorDef("T000610", "INSERT INTO `NewProductos`(`NewProductosImagen`, `NewProductosImagen_GXI`, `NewProductosNombre`, `NewProductosDescripcionCorta`, `NewProductosDescripcion`, `NewProductosNumeroDescargas`, `NewProductosLinkDescargaDemo`, `NewProductosComprar`, `NewProductosNumeroVentas`, `NewProductosVisitas`, `CategoriasId`) VALUES(@NewProductosImagen, @NewProductosImagen_GXI, @NewProductosNombre, @NewProductosDescripcionCorta, @NewProductosDescripcion, @NewProductosNumeroDescargas, @NewProductosLinkDescargaDemo, @NewProductosComprar, @NewProductosNumeroVentas, @NewProductosVisitas, @CategoriasId)", GxErrorMask.GX_NOMASK,prmT000610)
           ,new CursorDef("T000611", "SELECT LAST_INSERT_ID() ",true, GxErrorMask.GX_NOMASK, false, this,prmT000611,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T000612", "UPDATE `NewProductos` SET `NewProductosNombre`=@NewProductosNombre, `NewProductosDescripcionCorta`=@NewProductosDescripcionCorta, `NewProductosDescripcion`=@NewProductosDescripcion, `NewProductosNumeroDescargas`=@NewProductosNumeroDescargas, `NewProductosLinkDescargaDemo`=@NewProductosLinkDescargaDemo, `NewProductosComprar`=@NewProductosComprar, `NewProductosNumeroVentas`=@NewProductosNumeroVentas, `NewProductosVisitas`=@NewProductosVisitas, `CategoriasId`=@CategoriasId  WHERE `NewProductosId` = @NewProductosId", GxErrorMask.GX_NOMASK,prmT000612)
           ,new CursorDef("T000613", "UPDATE `NewProductos` SET `NewProductosImagen`=@NewProductosImagen, `NewProductosImagen_GXI`=@NewProductosImagen_GXI  WHERE `NewProductosId` = @NewProductosId", GxErrorMask.GX_NOMASK,prmT000613)
           ,new CursorDef("T000614", "DELETE FROM `NewProductos`  WHERE `NewProductosId` = @NewProductosId", GxErrorMask.GX_NOMASK,prmT000614)
           ,new CursorDef("T000615", "SELECT `NewProductosId` FROM `NewProductos` ORDER BY `NewProductosId` ",true, GxErrorMask.GX_NOMASK, false, this,prmT000615,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T000616", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmT000616,1, GxCacheFrequency.OFF ,true,false )
        };
     }
  }

  public void getResults( int cursor ,
                          IFieldGetter rslt ,
                          Object[] buf )
  {
     switch ( cursor )
     {
           case 0 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((short[]) buf[5])[0] = rslt.getShort(6);
              ((string[]) buf[6])[0] = rslt.getVarchar(7);
              ((string[]) buf[7])[0] = rslt.getVarchar(8);
              ((short[]) buf[8])[0] = rslt.getShort(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((short[]) buf[10])[0] = rslt.getShort(11);
              ((string[]) buf[11])[0] = rslt.getMultimediaFile(12, rslt.getVarchar(2));
              return;
           case 1 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((short[]) buf[5])[0] = rslt.getShort(6);
              ((string[]) buf[6])[0] = rslt.getVarchar(7);
              ((string[]) buf[7])[0] = rslt.getVarchar(8);
              ((short[]) buf[8])[0] = rslt.getShort(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((short[]) buf[10])[0] = rslt.getShort(11);
              ((string[]) buf[11])[0] = rslt.getMultimediaFile(12, rslt.getVarchar(2));
              return;
           case 2 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 3 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((short[]) buf[5])[0] = rslt.getShort(6);
              ((string[]) buf[6])[0] = rslt.getVarchar(7);
              ((string[]) buf[7])[0] = rslt.getVarchar(8);
              ((short[]) buf[8])[0] = rslt.getShort(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((short[]) buf[10])[0] = rslt.getShort(11);
              ((string[]) buf[11])[0] = rslt.getMultimediaFile(12, rslt.getVarchar(2));
              return;
           case 4 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 5 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 6 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 7 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 9 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 13 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 14 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
     }
  }

}

}
