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
   public class newblog : GXDataArea
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
         else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxExecAct_"+"gxLoad_9") == 0 )
         {
            A20CategoriasId = (short)(Math.Round(NumberUtil.Val( GetPar( "CategoriasId"), "."), 18, MidpointRounding.ToEven));
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
            setAjaxCallMode();
            if ( ! IsValidAjaxCall( true) )
            {
               GxWebError = 1;
               return  ;
            }
            gxLoad_9( A20CategoriasId) ;
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
            if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "newblog.aspx")), "newblog.aspx") == 0 ) )
            {
               SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "newblog.aspx")))) ;
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
                  AV7NewBlogId = (short)(Math.Round(NumberUtil.Val( GetPar( "NewBlogId"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "AV7NewBlogId", StringUtil.LTrimStr( (decimal)(AV7NewBlogId), 4, 0));
                  GxWebStd.gx_hidden_field( context, "gxhash_vNEWBLOGID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV7NewBlogId), "ZZZ9"), context));
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
         Form.Meta.addItem("description", context.GetMessage( "New Blog", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         if ( ! context.isAjaxRequest( ) )
         {
            GX_FocusControl = edtNewBlogTitulo_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         wbErr = false;
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      public newblog( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newblog( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_Gx_mode ,
                           short aP1_NewBlogId )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV7NewBlogId = aP1_NewBlogId;
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         chkNewBlogDestacado = new GXCheckbox();
         chkNewBlogBorrador = new GXCheckbox();
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
            return "newblog_Execute" ;
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
         A19NewBlogDestacado = StringUtil.StrToBool( StringUtil.BoolToStr( A19NewBlogDestacado));
         AssignAttri("", false, "A19NewBlogDestacado", A19NewBlogDestacado);
         A25NewBlogBorrador = StringUtil.StrToBool( StringUtil.BoolToStr( A25NewBlogBorrador));
         AssignAttri("", false, "A25NewBlogBorrador", A25NewBlogBorrador);
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* User Defined Control */
         ucNewblogdescripcion.SetProperty("Width", Newblogdescripcion_Width);
         ucNewblogdescripcion.SetProperty("Height", Newblogdescripcion_Height);
         ucNewblogdescripcion.SetProperty("Attribute", NewBlogDescripcion);
         ucNewblogdescripcion.SetProperty("Toolbar", Newblogdescripcion_Toolbar);
         ucNewblogdescripcion.SetProperty("ToolbarCanCollapse", Newblogdescripcion_Toolbarcancollapse);
         ucNewblogdescripcion.SetProperty("ToolbarExpanded", Newblogdescripcion_Toolbarexpanded);
         ucNewblogdescripcion.SetProperty("Color", Newblogdescripcion_Color);
         ucNewblogdescripcion.SetProperty("CaptionClass", Newblogdescripcion_Captionclass);
         ucNewblogdescripcion.SetProperty("CaptionStyle", Newblogdescripcion_Captionstyle);
         ucNewblogdescripcion.SetProperty("CaptionPosition", Newblogdescripcion_Captionposition);
         ucNewblogdescripcion.Render(context, "fckeditor", Newblogdescripcion_Internalname, "NEWBLOGDESCRIPCIONContainer");
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtNewBlogTitulo_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtNewBlogTitulo_Internalname, context.GetMessage( "Titulo", ""), " RowBlog_1Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'',false,'',0)\"";
         ClassString = "RowBlog_1";
         StyleString = "";
         ClassString = "RowBlog_1";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtNewBlogTitulo_Internalname, A14NewBlogTitulo, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"", 0, 1, edtNewBlogTitulo_Enabled, 0, 80, "chr", 3, "row", 0, StyleString, ClassString, "", "", "200", -1, 0, "", "", -1, true, "GeneXusUnanimo\\Description", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtNewBlogSubTitulo_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtNewBlogSubTitulo_Internalname, context.GetMessage( "Sub Titulo", ""), " RowBlog_1Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'',0)\"";
         ClassString = "RowBlog_1";
         StyleString = "";
         ClassString = "RowBlog_1";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtNewBlogSubTitulo_Internalname, A15NewBlogSubTitulo, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"", 0, 1, edtNewBlogSubTitulo_Enabled, 0, 80, "chr", 3, "row", 0, StyleString, ClassString, "", "", "200", -1, 0, "", "", -1, true, "GeneXusUnanimo\\Description", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtNewBlogDescripcionCorta_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtNewBlogDescripcionCorta_Internalname, context.GetMessage( "Descripción Corta", ""), " RowBlog_2Label", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 42,'',false,'',0)\"";
         ClassString = "RowBlog_2";
         StyleString = "";
         ClassString = "RowBlog_2";
         StyleString = "";
         GxWebStd.gx_html_textarea( context, edtNewBlogDescripcionCorta_Internalname, A17NewBlogDescripcionCorta, "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,42);\"", 0, 1, edtNewBlogDescripcionCorta_Enabled, 0, 80, "chr", 7, "row", 0, StyleString, ClassString, "", "", "500", -1, 0, "", "", -1, true, "", "'"+""+"'"+",false,"+"'"+""+"'", 0, "", "HLP_NewBlog.htm");
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+imgNewBlogImagen_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, "", context.GetMessage( "Imagen", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Static Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 52,'',false,'',0)\"";
         ClassString = "Attribute";
         StyleString = "";
         A13NewBlogImagen_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen))&&String.IsNullOrEmpty(StringUtil.RTrim( A40000NewBlogImagen_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.PathToRelativeUrl( A13NewBlogImagen));
         GxWebStd.gx_bitmap( context, imgNewBlogImagen_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, imgNewBlogImagen_Enabled, "", "", 0, -1, 0, "", 0, "", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,52);\"", "", "", "", 0, A13NewBlogImagen_IsBlob, true, context.GetImageSrcSet( sImgUrl), "HLP_NewBlog.htm");
         AssignProp("", false, imgNewBlogImagen_Internalname, "URL", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.PathToRelativeUrl( A13NewBlogImagen)), true);
         AssignProp("", false, imgNewBlogImagen_Internalname, "IsBlob", StringUtil.BoolToStr( A13NewBlogImagen_IsBlob), true);
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 DscTop", "end", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkNewBlogDestacado_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, chkNewBlogDestacado_Internalname, context.GetMessage( "Destacado", ""), " AttributeCheckBoxLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Check box */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 56,'',false,'',0)\"";
         ClassString = "AttributeCheckBox";
         StyleString = "";
         GxWebStd.gx_checkbox_ctrl( context, chkNewBlogDestacado_Internalname, StringUtil.BoolToStr( A19NewBlogDestacado), "", context.GetMessage( "Destacado", ""), 1, chkNewBlogDestacado.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(56, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,56);\"");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "end", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-4 DscTop", "end", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+chkNewBlogBorrador_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, chkNewBlogBorrador_Internalname, context.GetMessage( "Borrador", ""), " AttributeCheckBoxLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Check box */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 60,'',false,'',0)\"";
         ClassString = "AttributeCheckBox";
         StyleString = "";
         GxWebStd.gx_checkbox_ctrl( context, chkNewBlogBorrador_Internalname, StringUtil.BoolToStr( A25NewBlogBorrador), "", context.GetMessage( "Borrador", ""), 1, chkNewBlogBorrador.Enabled, "true", "", StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(60, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,60);\"");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "end", "top", "div");
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
         GxWebStd.gx_label_ctrl( context, lblTextblockcategoriasid_Internalname, context.GetMessage( "Categoria", ""), "", "", lblTextblockcategoriasid_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "Label", 0, "", 1, 1, 0, 0, "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* User Defined Control */
         ucCombo_categoriasid.SetProperty("Caption", Combo_categoriasid_Caption);
         ucCombo_categoriasid.SetProperty("Cls", Combo_categoriasid_Cls);
         ucCombo_categoriasid.SetProperty("DataListProc", Combo_categoriasid_Datalistproc);
         ucCombo_categoriasid.SetProperty("DataListProcParametersPrefix", Combo_categoriasid_Datalistprocparametersprefix);
         ucCombo_categoriasid.SetProperty("DropDownOptionsTitleSettingsIcons", AV19DDO_TitleSettingsIcons);
         ucCombo_categoriasid.SetProperty("DropDownOptionsData", AV26CategoriasId_Data);
         ucCombo_categoriasid.Render(context, "dvelop.gxbootstrap.ddoextendedcombo", Combo_categoriasid_Internalname, "COMBO_CATEGORIASIDContainer");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 Invisible", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtCategoriasId_Internalname, context.GetMessage( "Id", ""), "col-sm-3 AttributeLabel", 0, true, "");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 75,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtCategoriasId_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A20CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( context.localUtil.Format( (decimal)(A20CategoriasId), "ZZZ9")), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,75);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtCategoriasId_Jsonclick, 0, "Attribute", "", "", "", "", edtCategoriasId_Visible, edtCategoriasId_Enabled, 1, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "Id", "end", false, "", "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-6 CellPaddingTopButton", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 77,'',false,'',0)\"";
         ClassString = "ButtonMaterialGAM";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtnuseraction1_Internalname, "", "+", bttBtnuseraction1_Jsonclick, 5, "+", "", StyleString, ClassString, bttBtnuseraction1_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'DOUSERACTION1\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
         /* Text block */
         GxWebStd.gx_label_ctrl( context, lblTextblock1_Internalname, context.GetMessage( "<br>", ""), "", "", lblTextblock1_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 1, "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;align-self:flex-end;", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroup", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 83,'',false,'',0)\"";
         ClassString = "ButtonMaterial";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_enter_Internalname, "", context.GetMessage( "GX_BtnEnter", ""), bttBtntrn_enter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtntrn_enter_Visible, bttBtntrn_enter_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 85,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_cancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtntrn_cancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, bttBtntrn_cancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 87,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_delete_Internalname, "", context.GetMessage( "GX_BtnDelete", ""), bttBtntrn_delete_Jsonclick, 5, context.GetMessage( "GX_BtnDelete", ""), "", StyleString, ClassString, bttBtntrn_delete_Visible, bttBtntrn_delete_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EDELETE."+"'", TempTags, "", context.GetButtonType( ), "HLP_NewBlog.htm");
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
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 92,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtavCombocategoriasid_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(AV17ComboCategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtavCombocategoriasid_Enabled!=0) ? context.localUtil.Format( (decimal)(AV17ComboCategoriasId), "ZZZ9") : context.localUtil.Format( (decimal)(AV17ComboCategoriasId), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,92);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavCombocategoriasid_Jsonclick, 0, "Attribute", "", "", "", "", edtavCombocategoriasid_Visible, edtavCombocategoriasid_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "", "end", false, "", "HLP_NewBlog.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 93,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtNewBlogVisitas_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A18NewBlogVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtNewBlogVisitas_Enabled!=0) ? context.localUtil.Format( (decimal)(A18NewBlogVisitas), "ZZZ9") : context.localUtil.Format( (decimal)(A18NewBlogVisitas), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,93);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtNewBlogVisitas_Jsonclick, 0, "Attribute", "", "", "", "", edtNewBlogVisitas_Visible, edtNewBlogVisitas_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "Contador", "end", false, "", "HLP_NewBlog.htm");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 94,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtNewBlogId_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A12NewBlogId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtNewBlogId_Enabled!=0) ? context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9") : context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,94);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtNewBlogId_Jsonclick, 0, "Attribute", "", "", "", "", edtNewBlogId_Visible, edtNewBlogId_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "Id", "end", false, "", "HLP_NewBlog.htm");
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
         E11032 ();
         context.wbGlbDoneStart = 1;
         assign_properties_default( ) ;
         if ( AnyError == 0 )
         {
            if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
            {
               /* Read saved SDTs. */
               ajax_req_read_hidden_sdt(cgiGet( "vDDO_TITLESETTINGSICONS"), AV19DDO_TitleSettingsIcons);
               ajax_req_read_hidden_sdt(cgiGet( "vCATEGORIASID_DATA"), AV26CategoriasId_Data);
               /* Read saved values. */
               Z12NewBlogId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z12NewBlogId"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z14NewBlogTitulo = cgiGet( "Z14NewBlogTitulo");
               Z15NewBlogSubTitulo = cgiGet( "Z15NewBlogSubTitulo");
               Z17NewBlogDescripcionCorta = cgiGet( "Z17NewBlogDescripcionCorta");
               Z18NewBlogVisitas = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z18NewBlogVisitas"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z19NewBlogDestacado = StringUtil.StrToBool( cgiGet( "Z19NewBlogDestacado"));
               Z25NewBlogBorrador = StringUtil.StrToBool( cgiGet( "Z25NewBlogBorrador"));
               Z20CategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z20CategoriasId"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               IsConfirmed = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsConfirmed"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               IsModified = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsModified"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Gx_mode = cgiGet( "Mode");
               N20CategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "N20CategoriasId"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AV7NewBlogId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vNEWBLOGID"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AV13Insert_CategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vINSERT_CATEGORIASID"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               A40000NewBlogImagen_GXI = cgiGet( "NEWBLOGIMAGEN_GXI");
               A16NewBlogDescripcion = cgiGet( "NEWBLOGDESCRIPCION");
               AV27Pgmname = cgiGet( "vPGMNAME");
               Newblogdescripcion_Objectcall = cgiGet( "NEWBLOGDESCRIPCION_Objectcall");
               Newblogdescripcion_Class = cgiGet( "NEWBLOGDESCRIPCION_Class");
               Newblogdescripcion_Enabled = StringUtil.StrToBool( cgiGet( "NEWBLOGDESCRIPCION_Enabled"));
               Newblogdescripcion_Width = cgiGet( "NEWBLOGDESCRIPCION_Width");
               Newblogdescripcion_Height = cgiGet( "NEWBLOGDESCRIPCION_Height");
               Newblogdescripcion_Skin = cgiGet( "NEWBLOGDESCRIPCION_Skin");
               Newblogdescripcion_Toolbar = cgiGet( "NEWBLOGDESCRIPCION_Toolbar");
               Newblogdescripcion_Customtoolbar = cgiGet( "NEWBLOGDESCRIPCION_Customtoolbar");
               Newblogdescripcion_Customconfiguration = cgiGet( "NEWBLOGDESCRIPCION_Customconfiguration");
               Newblogdescripcion_Toolbarcancollapse = StringUtil.StrToBool( cgiGet( "NEWBLOGDESCRIPCION_Toolbarcancollapse"));
               Newblogdescripcion_Toolbarexpanded = StringUtil.StrToBool( cgiGet( "NEWBLOGDESCRIPCION_Toolbarexpanded"));
               Newblogdescripcion_Color = (int)(Math.Round(context.localUtil.CToN( cgiGet( "NEWBLOGDESCRIPCION_Color"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Newblogdescripcion_Buttonpressedid = cgiGet( "NEWBLOGDESCRIPCION_Buttonpressedid");
               Newblogdescripcion_Captionvalue = cgiGet( "NEWBLOGDESCRIPCION_Captionvalue");
               Newblogdescripcion_Captionclass = cgiGet( "NEWBLOGDESCRIPCION_Captionclass");
               Newblogdescripcion_Captionstyle = cgiGet( "NEWBLOGDESCRIPCION_Captionstyle");
               Newblogdescripcion_Captionposition = cgiGet( "NEWBLOGDESCRIPCION_Captionposition");
               Newblogdescripcion_Isabstractlayoutcontrol = StringUtil.StrToBool( cgiGet( "NEWBLOGDESCRIPCION_Isabstractlayoutcontrol"));
               Newblogdescripcion_Coltitle = cgiGet( "NEWBLOGDESCRIPCION_Coltitle");
               Newblogdescripcion_Coltitlefont = cgiGet( "NEWBLOGDESCRIPCION_Coltitlefont");
               Newblogdescripcion_Coltitlecolor = (int)(Math.Round(context.localUtil.CToN( cgiGet( "NEWBLOGDESCRIPCION_Coltitlecolor"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Newblogdescripcion_Usercontroliscolumn = StringUtil.StrToBool( cgiGet( "NEWBLOGDESCRIPCION_Usercontroliscolumn"));
               Newblogdescripcion_Visible = StringUtil.StrToBool( cgiGet( "NEWBLOGDESCRIPCION_Visible"));
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
               A14NewBlogTitulo = cgiGet( edtNewBlogTitulo_Internalname);
               AssignAttri("", false, "A14NewBlogTitulo", A14NewBlogTitulo);
               A15NewBlogSubTitulo = cgiGet( edtNewBlogSubTitulo_Internalname);
               AssignAttri("", false, "A15NewBlogSubTitulo", A15NewBlogSubTitulo);
               A17NewBlogDescripcionCorta = cgiGet( edtNewBlogDescripcionCorta_Internalname);
               AssignAttri("", false, "A17NewBlogDescripcionCorta", A17NewBlogDescripcionCorta);
               A13NewBlogImagen = cgiGet( imgNewBlogImagen_Internalname);
               AssignAttri("", false, "A13NewBlogImagen", A13NewBlogImagen);
               A19NewBlogDestacado = StringUtil.StrToBool( cgiGet( chkNewBlogDestacado_Internalname));
               AssignAttri("", false, "A19NewBlogDestacado", A19NewBlogDestacado);
               A25NewBlogBorrador = StringUtil.StrToBool( cgiGet( chkNewBlogBorrador_Internalname));
               AssignAttri("", false, "A25NewBlogBorrador", A25NewBlogBorrador);
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
               AV17ComboCategoriasId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtavCombocategoriasid_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "AV17ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV17ComboCategoriasId), 4, 0));
               if ( ( ( context.localUtil.CToN( cgiGet( edtNewBlogVisitas_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtNewBlogVisitas_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > Convert.ToDecimal( 9999 )) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "NEWBLOGVISITAS");
                  AnyError = 1;
                  GX_FocusControl = edtNewBlogVisitas_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A18NewBlogVisitas = 0;
                  AssignAttri("", false, "A18NewBlogVisitas", StringUtil.LTrimStr( (decimal)(A18NewBlogVisitas), 4, 0));
               }
               else
               {
                  A18NewBlogVisitas = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewBlogVisitas_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A18NewBlogVisitas", StringUtil.LTrimStr( (decimal)(A18NewBlogVisitas), 4, 0));
               }
               A12NewBlogId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewBlogId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
               /* Read subfile selected row values. */
               /* Read hidden variables. */
               getMultimediaValue(imgNewBlogImagen_Internalname, ref  A13NewBlogImagen, ref  A40000NewBlogImagen_GXI);
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               forbiddenHiddens = new GXProperties();
               forbiddenHiddens.Add("hshsalt", "hsh"+"NewBlog");
               A12NewBlogId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtNewBlogId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
               forbiddenHiddens.Add("NewBlogId", context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9"));
               forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
               hsh = cgiGet( "hsh");
               if ( ( ! ( ( A12NewBlogId != Z12NewBlogId ) ) || ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) ) && ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
               {
                  GXUtil.WriteLogError("newblog:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
                  A12NewBlogId = (short)(Math.Round(NumberUtil.Val( GetPar( "NewBlogId"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
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
                     sMode3 = Gx_mode;
                     Gx_mode = "UPD";
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                     Gx_mode = sMode3;
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                  }
                  standaloneModal( ) ;
                  if ( ! IsIns( ) )
                  {
                     getByPrimaryKey( ) ;
                     if ( RcdFound3 == 1 )
                     {
                        if ( IsDlt( ) )
                        {
                           /* Confirm record */
                           CONFIRM_030( ) ;
                           if ( AnyError == 0 )
                           {
                              GX_FocusControl = bttBtntrn_enter_Internalname;
                              AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                           }
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_noinsert", ""), 1, "NEWBLOGID");
                        AnyError = 1;
                        GX_FocusControl = edtNewBlogId_Internalname;
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
                           E11032 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "AFTER TRN") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: After Trn */
                           E12032 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "'DOUSERACTION1'") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: 'DoUserAction1' */
                           E13032 ();
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
            E12032 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               /* Clear variables for new insertion. */
               InitAll033( ) ;
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
            DisableAttributes033( ) ;
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

      protected void CONFIRM_030( )
      {
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls033( ) ;
            }
            else
            {
               CheckExtendedTable033( ) ;
               CloseExtendedTableCursors033( ) ;
            }
         }
         if ( AnyError == 0 )
         {
            IsConfirmed = 1;
            AssignAttri("", false, "IsConfirmed", StringUtil.LTrimStr( (decimal)(IsConfirmed), 4, 0));
         }
      }

      protected void ResetCaption030( )
      {
      }

      protected void E11032( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 = AV19DDO_TitleSettingsIcons;
         new DesignSystem.Programs.wwpbaseobjects.getwwptitlesettingsicons(context ).execute( out  GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1) ;
         AV19DDO_TitleSettingsIcons = GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1;
         AV22GAMSession = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).get(out  AV23GAMErrors);
         Combo_categoriasid_Gamoauthtoken = AV22GAMSession.gxTpr_Token;
         ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "GAMOAuthToken", Combo_categoriasid_Gamoauthtoken);
         edtCategoriasId_Visible = 0;
         AssignProp("", false, edtCategoriasId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtCategoriasId_Visible), 5, 0), true);
         AV17ComboCategoriasId = 0;
         AssignAttri("", false, "AV17ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV17ComboCategoriasId), 4, 0));
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
         if ( ( StringUtil.StrCmp(AV11TrnContext.gxTpr_Transactionname, AV27Pgmname) == 0 ) && ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) )
         {
            AV28GXV1 = 1;
            AssignAttri("", false, "AV28GXV1", StringUtil.LTrimStr( (decimal)(AV28GXV1), 8, 0));
            while ( AV28GXV1 <= AV11TrnContext.gxTpr_Attributes.Count )
            {
               AV14TrnContextAtt = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute)AV11TrnContext.gxTpr_Attributes.Item(AV28GXV1));
               if ( StringUtil.StrCmp(AV14TrnContextAtt.gxTpr_Attributename, "CategoriasId") == 0 )
               {
                  AV13Insert_CategoriasId = (short)(Math.Round(NumberUtil.Val( AV14TrnContextAtt.gxTpr_Attributevalue, "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "AV13Insert_CategoriasId", StringUtil.LTrimStr( (decimal)(AV13Insert_CategoriasId), 4, 0));
                  if ( ! (0==AV13Insert_CategoriasId) )
                  {
                     AV17ComboCategoriasId = AV13Insert_CategoriasId;
                     AssignAttri("", false, "AV17ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV17ComboCategoriasId), 4, 0));
                     Combo_categoriasid_Selectedvalue_set = StringUtil.Trim( StringUtil.Str( (decimal)(AV17ComboCategoriasId), 4, 0));
                     ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedValue_set", Combo_categoriasid_Selectedvalue_set);
                     GXt_char2 = AV21Combo_DataJson;
                     new newblogloaddvcombo(context ).execute(  "CategoriasId",  "GET",  false,  AV7NewBlogId,  AV14TrnContextAtt.gxTpr_Attributevalue, out  AV16ComboSelectedValue, out  AV20ComboSelectedText, out  GXt_char2) ;
                     AssignAttri("", false, "AV16ComboSelectedValue", AV16ComboSelectedValue);
                     AssignAttri("", false, "AV20ComboSelectedText", AV20ComboSelectedText);
                     AV21Combo_DataJson = GXt_char2;
                     AssignAttri("", false, "AV21Combo_DataJson", AV21Combo_DataJson);
                     Combo_categoriasid_Selectedtext_set = AV20ComboSelectedText;
                     ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedText_set", Combo_categoriasid_Selectedtext_set);
                     Combo_categoriasid_Enabled = false;
                     ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_categoriasid_Enabled));
                  }
               }
               AV28GXV1 = (int)(AV28GXV1+1);
               AssignAttri("", false, "AV28GXV1", StringUtil.LTrimStr( (decimal)(AV28GXV1), 8, 0));
            }
         }
         edtNewBlogVisitas_Visible = 0;
         AssignProp("", false, edtNewBlogVisitas_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewBlogVisitas_Visible), 5, 0), true);
         edtNewBlogId_Visible = 0;
         AssignProp("", false, edtNewBlogId_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtNewBlogId_Visible), 5, 0), true);
         GXt_boolean3 = AV18IsAuthorized_UserAction1;
         new DesignSystem.Programs.wwpbaseobjects.secgamisauthbyfunctionalitykey(context ).execute(  "categoriasww_Execute", out  GXt_boolean3) ;
         AV18IsAuthorized_UserAction1 = GXt_boolean3;
         AssignAttri("", false, "AV18IsAuthorized_UserAction1", AV18IsAuthorized_UserAction1);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_USERACTION1", GetSecureSignedToken( "", AV18IsAuthorized_UserAction1, context));
         if ( ! ( AV18IsAuthorized_UserAction1 ) )
         {
            bttBtnuseraction1_Visible = 0;
            AssignProp("", false, bttBtnuseraction1_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(bttBtnuseraction1_Visible), 5, 0), true);
         }
      }

      protected void E12032( )
      {
         /* After Trn Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) && ! AV11TrnContext.gxTpr_Callerondelete )
         {
            CallWebObject(formatLink("newblogww.aspx") );
            context.wjLocDisableFrm = 1;
         }
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void E13032( )
      {
         /* 'DoUserAction1' Routine */
         returnInSub = false;
         if ( AV18IsAuthorized_UserAction1 )
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
         GXt_char2 = AV21Combo_DataJson;
         new newblogloaddvcombo(context ).execute(  "CategoriasId",  Gx_mode,  false,  AV7NewBlogId,  "", out  AV16ComboSelectedValue, out  AV20ComboSelectedText, out  GXt_char2) ;
         AssignAttri("", false, "AV16ComboSelectedValue", AV16ComboSelectedValue);
         AssignAttri("", false, "AV20ComboSelectedText", AV20ComboSelectedText);
         AV21Combo_DataJson = GXt_char2;
         AssignAttri("", false, "AV21Combo_DataJson", AV21Combo_DataJson);
         Combo_categoriasid_Selectedvalue_set = AV16ComboSelectedValue;
         ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedValue_set", Combo_categoriasid_Selectedvalue_set);
         Combo_categoriasid_Selectedtext_set = AV20ComboSelectedText;
         ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "SelectedText_set", Combo_categoriasid_Selectedtext_set);
         AV17ComboCategoriasId = (short)(Math.Round(NumberUtil.Val( AV16ComboSelectedValue, "."), 18, MidpointRounding.ToEven));
         AssignAttri("", false, "AV17ComboCategoriasId", StringUtil.LTrimStr( (decimal)(AV17ComboCategoriasId), 4, 0));
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            Combo_categoriasid_Enabled = false;
            ucCombo_categoriasid.SendProperty(context, "", false, Combo_categoriasid_Internalname, "Enabled", StringUtil.BoolToStr( Combo_categoriasid_Enabled));
         }
      }

      protected void ZM033( short GX_JID )
      {
         if ( ( GX_JID == 8 ) || ( GX_JID == 0 ) )
         {
            if ( ! IsIns( ) )
            {
               Z14NewBlogTitulo = T00033_A14NewBlogTitulo[0];
               Z15NewBlogSubTitulo = T00033_A15NewBlogSubTitulo[0];
               Z17NewBlogDescripcionCorta = T00033_A17NewBlogDescripcionCorta[0];
               Z18NewBlogVisitas = T00033_A18NewBlogVisitas[0];
               Z19NewBlogDestacado = T00033_A19NewBlogDestacado[0];
               Z25NewBlogBorrador = T00033_A25NewBlogBorrador[0];
               Z20CategoriasId = T00033_A20CategoriasId[0];
            }
            else
            {
               Z14NewBlogTitulo = A14NewBlogTitulo;
               Z15NewBlogSubTitulo = A15NewBlogSubTitulo;
               Z17NewBlogDescripcionCorta = A17NewBlogDescripcionCorta;
               Z18NewBlogVisitas = A18NewBlogVisitas;
               Z19NewBlogDestacado = A19NewBlogDestacado;
               Z25NewBlogBorrador = A25NewBlogBorrador;
               Z20CategoriasId = A20CategoriasId;
            }
         }
         if ( GX_JID == -8 )
         {
            Z12NewBlogId = A12NewBlogId;
            Z13NewBlogImagen = A13NewBlogImagen;
            Z40000NewBlogImagen_GXI = A40000NewBlogImagen_GXI;
            Z14NewBlogTitulo = A14NewBlogTitulo;
            Z15NewBlogSubTitulo = A15NewBlogSubTitulo;
            Z16NewBlogDescripcion = A16NewBlogDescripcion;
            Z17NewBlogDescripcionCorta = A17NewBlogDescripcionCorta;
            Z18NewBlogVisitas = A18NewBlogVisitas;
            Z19NewBlogDestacado = A19NewBlogDestacado;
            Z25NewBlogBorrador = A25NewBlogBorrador;
            Z20CategoriasId = A20CategoriasId;
         }
      }

      protected void standaloneNotModal( )
      {
         edtNewBlogId_Enabled = 0;
         AssignProp("", false, edtNewBlogId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewBlogId_Enabled), 5, 0), true);
         AV27Pgmname = "NewBlog";
         AssignAttri("", false, "AV27Pgmname", AV27Pgmname);
         edtNewBlogId_Enabled = 0;
         AssignProp("", false, edtNewBlogId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewBlogId_Enabled), 5, 0), true);
         bttBtntrn_delete_Enabled = 0;
         AssignProp("", false, bttBtntrn_delete_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Enabled), 5, 0), true);
         if ( ! (0==AV7NewBlogId) )
         {
            A12NewBlogId = AV7NewBlogId;
            AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
         }
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (0==AV13Insert_CategoriasId) )
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
         if ( ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) && ! (0==AV13Insert_CategoriasId) )
         {
            A20CategoriasId = AV13Insert_CategoriasId;
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
         }
         else
         {
            A20CategoriasId = AV17ComboCategoriasId;
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

      protected void Load033( )
      {
         /* Using cursor T00035 */
         pr_default.execute(3, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound3 = 1;
            A40000NewBlogImagen_GXI = T00035_A40000NewBlogImagen_GXI[0];
            AssignProp("", false, imgNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), true);
            AssignProp("", false, imgNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
            A14NewBlogTitulo = T00035_A14NewBlogTitulo[0];
            AssignAttri("", false, "A14NewBlogTitulo", A14NewBlogTitulo);
            A15NewBlogSubTitulo = T00035_A15NewBlogSubTitulo[0];
            AssignAttri("", false, "A15NewBlogSubTitulo", A15NewBlogSubTitulo);
            A16NewBlogDescripcion = T00035_A16NewBlogDescripcion[0];
            A17NewBlogDescripcionCorta = T00035_A17NewBlogDescripcionCorta[0];
            AssignAttri("", false, "A17NewBlogDescripcionCorta", A17NewBlogDescripcionCorta);
            A18NewBlogVisitas = T00035_A18NewBlogVisitas[0];
            AssignAttri("", false, "A18NewBlogVisitas", StringUtil.LTrimStr( (decimal)(A18NewBlogVisitas), 4, 0));
            A19NewBlogDestacado = T00035_A19NewBlogDestacado[0];
            AssignAttri("", false, "A19NewBlogDestacado", A19NewBlogDestacado);
            A25NewBlogBorrador = T00035_A25NewBlogBorrador[0];
            AssignAttri("", false, "A25NewBlogBorrador", A25NewBlogBorrador);
            A20CategoriasId = T00035_A20CategoriasId[0];
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
            A13NewBlogImagen = T00035_A13NewBlogImagen[0];
            AssignAttri("", false, "A13NewBlogImagen", A13NewBlogImagen);
            AssignProp("", false, imgNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), true);
            AssignProp("", false, imgNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
            ZM033( -8) ;
         }
         pr_default.close(3);
         OnLoadActions033( ) ;
      }

      protected void OnLoadActions033( )
      {
      }

      protected void CheckExtendedTable033( )
      {
         Gx_BScreen = 1;
         standaloneModal( ) ;
         /* Using cursor T00034 */
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

      protected void CloseExtendedTableCursors033( )
      {
         pr_default.close(2);
      }

      protected void enableDisable( )
      {
      }

      protected void gxLoad_9( short A20CategoriasId )
      {
         /* Using cursor T00036 */
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

      protected void GetKey033( )
      {
         /* Using cursor T00037 */
         pr_default.execute(5, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(5) != 101) )
         {
            RcdFound3 = 1;
         }
         else
         {
            RcdFound3 = 0;
         }
         pr_default.close(5);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor T00033 */
         pr_default.execute(1, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM033( 8) ;
            RcdFound3 = 1;
            A12NewBlogId = T00033_A12NewBlogId[0];
            AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
            A40000NewBlogImagen_GXI = T00033_A40000NewBlogImagen_GXI[0];
            AssignProp("", false, imgNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), true);
            AssignProp("", false, imgNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
            A14NewBlogTitulo = T00033_A14NewBlogTitulo[0];
            AssignAttri("", false, "A14NewBlogTitulo", A14NewBlogTitulo);
            A15NewBlogSubTitulo = T00033_A15NewBlogSubTitulo[0];
            AssignAttri("", false, "A15NewBlogSubTitulo", A15NewBlogSubTitulo);
            A16NewBlogDescripcion = T00033_A16NewBlogDescripcion[0];
            A17NewBlogDescripcionCorta = T00033_A17NewBlogDescripcionCorta[0];
            AssignAttri("", false, "A17NewBlogDescripcionCorta", A17NewBlogDescripcionCorta);
            A18NewBlogVisitas = T00033_A18NewBlogVisitas[0];
            AssignAttri("", false, "A18NewBlogVisitas", StringUtil.LTrimStr( (decimal)(A18NewBlogVisitas), 4, 0));
            A19NewBlogDestacado = T00033_A19NewBlogDestacado[0];
            AssignAttri("", false, "A19NewBlogDestacado", A19NewBlogDestacado);
            A25NewBlogBorrador = T00033_A25NewBlogBorrador[0];
            AssignAttri("", false, "A25NewBlogBorrador", A25NewBlogBorrador);
            A20CategoriasId = T00033_A20CategoriasId[0];
            AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
            A13NewBlogImagen = T00033_A13NewBlogImagen[0];
            AssignAttri("", false, "A13NewBlogImagen", A13NewBlogImagen);
            AssignProp("", false, imgNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), true);
            AssignProp("", false, imgNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
            Z12NewBlogId = A12NewBlogId;
            sMode3 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            Load033( ) ;
            if ( AnyError == 1 )
            {
               RcdFound3 = 0;
               InitializeNonKey033( ) ;
            }
            Gx_mode = sMode3;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         else
         {
            RcdFound3 = 0;
            InitializeNonKey033( ) ;
            sMode3 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            standaloneModal( ) ;
            Gx_mode = sMode3;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey033( ) ;
         if ( RcdFound3 == 0 )
         {
         }
         else
         {
         }
         getByPrimaryKey( ) ;
      }

      protected void move_next( )
      {
         RcdFound3 = 0;
         /* Using cursor T00038 */
         pr_default.execute(6, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(6) != 101) )
         {
            while ( (pr_default.getStatus(6) != 101) && ( ( T00038_A12NewBlogId[0] < A12NewBlogId ) ) )
            {
               pr_default.readNext(6);
            }
            if ( (pr_default.getStatus(6) != 101) && ( ( T00038_A12NewBlogId[0] > A12NewBlogId ) ) )
            {
               A12NewBlogId = T00038_A12NewBlogId[0];
               AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
               RcdFound3 = 1;
            }
         }
         pr_default.close(6);
      }

      protected void move_previous( )
      {
         RcdFound3 = 0;
         /* Using cursor T00039 */
         pr_default.execute(7, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(7) != 101) )
         {
            while ( (pr_default.getStatus(7) != 101) && ( ( T00039_A12NewBlogId[0] > A12NewBlogId ) ) )
            {
               pr_default.readNext(7);
            }
            if ( (pr_default.getStatus(7) != 101) && ( ( T00039_A12NewBlogId[0] < A12NewBlogId ) ) )
            {
               A12NewBlogId = T00039_A12NewBlogId[0];
               AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
               RcdFound3 = 1;
            }
         }
         pr_default.close(7);
      }

      protected void btn_enter( )
      {
         nKeyPressed = 1;
         GetKey033( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            GX_FocusControl = edtNewBlogTitulo_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            Insert033( ) ;
            if ( AnyError == 1 )
            {
               GX_FocusControl = "";
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         else
         {
            if ( RcdFound3 == 1 )
            {
               if ( A12NewBlogId != Z12NewBlogId )
               {
                  A12NewBlogId = Z12NewBlogId;
                  AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "NEWBLOGID");
                  AnyError = 1;
                  GX_FocusControl = edtNewBlogId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else if ( IsDlt( ) )
               {
                  delete( ) ;
                  AfterTrn( ) ;
                  GX_FocusControl = edtNewBlogTitulo_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else
               {
                  /* Update record */
                  Update033( ) ;
                  GX_FocusControl = edtNewBlogTitulo_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
            }
            else
            {
               if ( A12NewBlogId != Z12NewBlogId )
               {
                  /* Insert record */
                  GX_FocusControl = edtNewBlogTitulo_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  Insert033( ) ;
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
                     GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "NEWBLOGID");
                     AnyError = 1;
                     GX_FocusControl = edtNewBlogId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
                  else
                  {
                     /* Insert record */
                     GX_FocusControl = edtNewBlogTitulo_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     Insert033( ) ;
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
         if ( A12NewBlogId != Z12NewBlogId )
         {
            A12NewBlogId = Z12NewBlogId;
            AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
            GX_msglist.addItem(context.GetMessage( "GXM_getbeforedlt", ""), 1, "NEWBLOGID");
            AnyError = 1;
            GX_FocusControl = edtNewBlogId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         else
         {
            delete( ) ;
            AfterTrn( ) ;
            GX_FocusControl = edtNewBlogTitulo_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         if ( AnyError != 0 )
         {
         }
      }

      protected void CheckOptimisticConcurrency033( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor T00032 */
            pr_default.execute(0, new Object[] {A12NewBlogId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewBlog"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z14NewBlogTitulo, T00032_A14NewBlogTitulo[0]) != 0 ) || ( StringUtil.StrCmp(Z15NewBlogSubTitulo, T00032_A15NewBlogSubTitulo[0]) != 0 ) || ( StringUtil.StrCmp(Z17NewBlogDescripcionCorta, T00032_A17NewBlogDescripcionCorta[0]) != 0 ) || ( Z18NewBlogVisitas != T00032_A18NewBlogVisitas[0] ) || ( Z19NewBlogDestacado != T00032_A19NewBlogDestacado[0] ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z25NewBlogBorrador != T00032_A25NewBlogBorrador[0] ) || ( Z20CategoriasId != T00032_A20CategoriasId[0] ) )
            {
               if ( StringUtil.StrCmp(Z14NewBlogTitulo, T00032_A14NewBlogTitulo[0]) != 0 )
               {
                  GXUtil.WriteLog("newblog:[seudo value changed for attri]"+"NewBlogTitulo");
                  GXUtil.WriteLogRaw("Old: ",Z14NewBlogTitulo);
                  GXUtil.WriteLogRaw("Current: ",T00032_A14NewBlogTitulo[0]);
               }
               if ( StringUtil.StrCmp(Z15NewBlogSubTitulo, T00032_A15NewBlogSubTitulo[0]) != 0 )
               {
                  GXUtil.WriteLog("newblog:[seudo value changed for attri]"+"NewBlogSubTitulo");
                  GXUtil.WriteLogRaw("Old: ",Z15NewBlogSubTitulo);
                  GXUtil.WriteLogRaw("Current: ",T00032_A15NewBlogSubTitulo[0]);
               }
               if ( StringUtil.StrCmp(Z17NewBlogDescripcionCorta, T00032_A17NewBlogDescripcionCorta[0]) != 0 )
               {
                  GXUtil.WriteLog("newblog:[seudo value changed for attri]"+"NewBlogDescripcionCorta");
                  GXUtil.WriteLogRaw("Old: ",Z17NewBlogDescripcionCorta);
                  GXUtil.WriteLogRaw("Current: ",T00032_A17NewBlogDescripcionCorta[0]);
               }
               if ( Z18NewBlogVisitas != T00032_A18NewBlogVisitas[0] )
               {
                  GXUtil.WriteLog("newblog:[seudo value changed for attri]"+"NewBlogVisitas");
                  GXUtil.WriteLogRaw("Old: ",Z18NewBlogVisitas);
                  GXUtil.WriteLogRaw("Current: ",T00032_A18NewBlogVisitas[0]);
               }
               if ( Z19NewBlogDestacado != T00032_A19NewBlogDestacado[0] )
               {
                  GXUtil.WriteLog("newblog:[seudo value changed for attri]"+"NewBlogDestacado");
                  GXUtil.WriteLogRaw("Old: ",Z19NewBlogDestacado);
                  GXUtil.WriteLogRaw("Current: ",T00032_A19NewBlogDestacado[0]);
               }
               if ( Z25NewBlogBorrador != T00032_A25NewBlogBorrador[0] )
               {
                  GXUtil.WriteLog("newblog:[seudo value changed for attri]"+"NewBlogBorrador");
                  GXUtil.WriteLogRaw("Old: ",Z25NewBlogBorrador);
                  GXUtil.WriteLogRaw("Current: ",T00032_A25NewBlogBorrador[0]);
               }
               if ( Z20CategoriasId != T00032_A20CategoriasId[0] )
               {
                  GXUtil.WriteLog("newblog:[seudo value changed for attri]"+"CategoriasId");
                  GXUtil.WriteLogRaw("Old: ",Z20CategoriasId);
                  GXUtil.WriteLogRaw("Current: ",T00032_A20CategoriasId[0]);
               }
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"NewBlog"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert033( )
      {
         if ( ! IsAuthorized("newblog_Insert") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable033( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM033( 0) ;
            CheckOptimisticConcurrency033( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm033( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert033( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T000310 */
                     pr_default.execute(8, new Object[] {A13NewBlogImagen, A40000NewBlogImagen_GXI, A14NewBlogTitulo, A15NewBlogSubTitulo, A16NewBlogDescripcion, A17NewBlogDescripcionCorta, A18NewBlogVisitas, A19NewBlogDestacado, A25NewBlogBorrador, A20CategoriasId});
                     pr_default.close(8);
                     /* Retrieving last key number assigned */
                     /* Using cursor T000311 */
                     pr_default.execute(9);
                     A12NewBlogId = T000311_A12NewBlogId[0];
                     AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
                     pr_default.close(9);
                     pr_default.SmartCacheProvider.SetUpdated("NewBlog");
                     if ( AnyError == 0 )
                     {
                        /* Start of After( Insert) rules */
                        /* End of After( Insert) rules */
                        if ( AnyError == 0 )
                        {
                           /* Save values for previous() function. */
                           endTrnMsgTxt = context.GetMessage( "GXM_sucadded", "");
                           endTrnMsgCod = "SuccessfullyAdded";
                           ResetCaption030( ) ;
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
               Load033( ) ;
            }
            EndLevel033( ) ;
         }
         CloseExtendedTableCursors033( ) ;
      }

      protected void Update033( )
      {
         if ( ! IsAuthorized("newblog_Update") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable033( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency033( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm033( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate033( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T000312 */
                     pr_default.execute(10, new Object[] {A14NewBlogTitulo, A15NewBlogSubTitulo, A16NewBlogDescripcion, A17NewBlogDescripcionCorta, A18NewBlogVisitas, A19NewBlogDestacado, A25NewBlogBorrador, A20CategoriasId, A12NewBlogId});
                     pr_default.close(10);
                     pr_default.SmartCacheProvider.SetUpdated("NewBlog");
                     if ( (pr_default.getStatus(10) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewBlog"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate033( ) ;
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
            EndLevel033( ) ;
         }
         CloseExtendedTableCursors033( ) ;
      }

      protected void DeferredUpdate033( )
      {
         if ( AnyError == 0 )
         {
            /* Using cursor T000313 */
            pr_default.execute(11, new Object[] {A13NewBlogImagen, A40000NewBlogImagen_GXI, A12NewBlogId});
            pr_default.close(11);
            pr_default.SmartCacheProvider.SetUpdated("NewBlog");
         }
      }

      protected void delete( )
      {
         if ( ! IsAuthorized("newblog_Delete") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency033( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls033( ) ;
            AfterConfirm033( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete033( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor T000314 */
                  pr_default.execute(12, new Object[] {A12NewBlogId});
                  pr_default.close(12);
                  pr_default.SmartCacheProvider.SetUpdated("NewBlog");
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
         sMode3 = Gx_mode;
         Gx_mode = "DLT";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         EndLevel033( ) ;
         Gx_mode = sMode3;
         AssignAttri("", false, "Gx_mode", Gx_mode);
      }

      protected void OnDeleteControls033( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel033( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete033( ) ;
         }
         if ( AnyError == 0 )
         {
            context.CommitDataStores("newblog",pr_default);
            if ( AnyError == 0 )
            {
               ConfirmValues030( ) ;
            }
            /* After transaction rules */
            /* Execute 'After Trn' event if defined. */
            trnEnded = 1;
         }
         else
         {
            context.RollbackDataStores("newblog",pr_default);
         }
         IsModified = 0;
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanStart033( )
      {
         /* Scan By routine */
         /* Using cursor T000315 */
         pr_default.execute(13);
         RcdFound3 = 0;
         if ( (pr_default.getStatus(13) != 101) )
         {
            RcdFound3 = 1;
            A12NewBlogId = T000315_A12NewBlogId[0];
            AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
         }
         /* Load Subordinate Levels */
      }

      protected void ScanNext033( )
      {
         /* Scan next routine */
         pr_default.readNext(13);
         RcdFound3 = 0;
         if ( (pr_default.getStatus(13) != 101) )
         {
            RcdFound3 = 1;
            A12NewBlogId = T000315_A12NewBlogId[0];
            AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
         }
      }

      protected void ScanEnd033( )
      {
         pr_default.close(13);
      }

      protected void AfterConfirm033( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert033( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate033( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete033( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete033( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate033( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes033( )
      {
         edtNewBlogTitulo_Enabled = 0;
         AssignProp("", false, edtNewBlogTitulo_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewBlogTitulo_Enabled), 5, 0), true);
         edtNewBlogSubTitulo_Enabled = 0;
         AssignProp("", false, edtNewBlogSubTitulo_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewBlogSubTitulo_Enabled), 5, 0), true);
         edtNewBlogDescripcionCorta_Enabled = 0;
         AssignProp("", false, edtNewBlogDescripcionCorta_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewBlogDescripcionCorta_Enabled), 5, 0), true);
         imgNewBlogImagen_Enabled = 0;
         AssignProp("", false, imgNewBlogImagen_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(imgNewBlogImagen_Enabled), 5, 0), true);
         chkNewBlogDestacado.Enabled = 0;
         AssignProp("", false, chkNewBlogDestacado_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkNewBlogDestacado.Enabled), 5, 0), true);
         chkNewBlogBorrador.Enabled = 0;
         AssignProp("", false, chkNewBlogBorrador_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(chkNewBlogBorrador.Enabled), 5, 0), true);
         edtCategoriasId_Enabled = 0;
         AssignProp("", false, edtCategoriasId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtCategoriasId_Enabled), 5, 0), true);
         edtavCombocategoriasid_Enabled = 0;
         AssignProp("", false, edtavCombocategoriasid_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavCombocategoriasid_Enabled), 5, 0), true);
         edtNewBlogVisitas_Enabled = 0;
         AssignProp("", false, edtNewBlogVisitas_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewBlogVisitas_Enabled), 5, 0), true);
         edtNewBlogId_Enabled = 0;
         AssignProp("", false, edtNewBlogId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtNewBlogId_Enabled), 5, 0), true);
         Newblogdescripcion_Enabled = Convert.ToBoolean( 0);
         AssignProp("", false, Newblogdescripcion_Internalname, "Enabled", StringUtil.BoolToStr( Newblogdescripcion_Enabled), true);
      }

      protected void send_integrity_lvl_hashes033( )
      {
      }

      protected void assign_properties_default( )
      {
      }

      protected void ConfirmValues030( )
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
         GXEncryptionTmp = "newblog.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV7NewBlogId,4,0));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("newblog.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         forbiddenHiddens.Add("hshsalt", "hsh"+"NewBlog");
         forbiddenHiddens.Add("NewBlogId", context.localUtil.Format( (decimal)(A12NewBlogId), "ZZZ9"));
         forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("newblog:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "Z12NewBlogId", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z12NewBlogId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z14NewBlogTitulo", Z14NewBlogTitulo);
         GxWebStd.gx_hidden_field( context, "Z15NewBlogSubTitulo", Z15NewBlogSubTitulo);
         GxWebStd.gx_hidden_field( context, "Z17NewBlogDescripcionCorta", Z17NewBlogDescripcionCorta);
         GxWebStd.gx_hidden_field( context, "Z18NewBlogVisitas", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z18NewBlogVisitas), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_boolean_hidden_field( context, "Z19NewBlogDestacado", Z19NewBlogDestacado);
         GxWebStd.gx_boolean_hidden_field( context, "Z25NewBlogBorrador", Z25NewBlogBorrador);
         GxWebStd.gx_hidden_field( context, "Z20CategoriasId", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z20CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsConfirmed", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsConfirmed), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsModified", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsModified), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Mode", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_Mode", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "N20CategoriasId", StringUtil.LTrim( StringUtil.NToC( (decimal)(A20CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vDDO_TITLESETTINGSICONS", AV19DDO_TitleSettingsIcons);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vDDO_TITLESETTINGSICONS", AV19DDO_TitleSettingsIcons);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vCATEGORIASID_DATA", AV26CategoriasId_Data);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vCATEGORIASID_DATA", AV26CategoriasId_Data);
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
         GxWebStd.gx_boolean_hidden_field( context, "vISAUTHORIZED_USERACTION1", AV18IsAuthorized_UserAction1);
         GxWebStd.gx_hidden_field( context, "gxhash_vISAUTHORIZED_USERACTION1", GetSecureSignedToken( "", AV18IsAuthorized_UserAction1, context));
         GxWebStd.gx_hidden_field( context, "vNEWBLOGID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV7NewBlogId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vNEWBLOGID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV7NewBlogId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vINSERT_CATEGORIASID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13Insert_CategoriasId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "NEWBLOGIMAGEN_GXI", A40000NewBlogImagen_GXI);
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION", A16NewBlogDescripcion);
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV27Pgmname));
         GXCCtlgxBlob = "NEWBLOGIMAGEN" + "_gxBlob";
         GxWebStd.gx_hidden_field( context, GXCCtlgxBlob, A13NewBlogImagen);
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Objectcall", StringUtil.RTrim( Newblogdescripcion_Objectcall));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Enabled", StringUtil.BoolToStr( Newblogdescripcion_Enabled));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Width", StringUtil.RTrim( Newblogdescripcion_Width));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Height", StringUtil.RTrim( Newblogdescripcion_Height));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Toolbar", StringUtil.RTrim( Newblogdescripcion_Toolbar));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Toolbarcancollapse", StringUtil.BoolToStr( Newblogdescripcion_Toolbarcancollapse));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Toolbarexpanded", StringUtil.BoolToStr( Newblogdescripcion_Toolbarexpanded));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Color", StringUtil.LTrim( StringUtil.NToC( (decimal)(Newblogdescripcion_Color), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Captionclass", StringUtil.RTrim( Newblogdescripcion_Captionclass));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Captionstyle", StringUtil.RTrim( Newblogdescripcion_Captionstyle));
         GxWebStd.gx_hidden_field( context, "NEWBLOGDESCRIPCION_Captionposition", StringUtil.RTrim( Newblogdescripcion_Captionposition));
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
         GXEncryptionTmp = "newblog.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV7NewBlogId,4,0));
         return formatLink("newblog.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "NewBlog" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "New Blog", "") ;
      }

      protected void InitializeNonKey033( )
      {
         A20CategoriasId = 0;
         AssignAttri("", false, "A20CategoriasId", StringUtil.LTrimStr( (decimal)(A20CategoriasId), 4, 0));
         A13NewBlogImagen = "";
         AssignAttri("", false, "A13NewBlogImagen", A13NewBlogImagen);
         AssignProp("", false, imgNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), true);
         AssignProp("", false, imgNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
         A40000NewBlogImagen_GXI = "";
         AssignProp("", false, imgNewBlogImagen_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( A13NewBlogImagen)) ? A40000NewBlogImagen_GXI : context.convertURL( context.PathToRelativeUrl( A13NewBlogImagen))), true);
         AssignProp("", false, imgNewBlogImagen_Internalname, "SrcSet", context.GetImageSrcSet( A13NewBlogImagen), true);
         A14NewBlogTitulo = "";
         AssignAttri("", false, "A14NewBlogTitulo", A14NewBlogTitulo);
         A15NewBlogSubTitulo = "";
         AssignAttri("", false, "A15NewBlogSubTitulo", A15NewBlogSubTitulo);
         A16NewBlogDescripcion = "";
         AssignAttri("", false, "A16NewBlogDescripcion", A16NewBlogDescripcion);
         A17NewBlogDescripcionCorta = "";
         AssignAttri("", false, "A17NewBlogDescripcionCorta", A17NewBlogDescripcionCorta);
         A18NewBlogVisitas = 0;
         AssignAttri("", false, "A18NewBlogVisitas", StringUtil.LTrimStr( (decimal)(A18NewBlogVisitas), 4, 0));
         A19NewBlogDestacado = false;
         AssignAttri("", false, "A19NewBlogDestacado", A19NewBlogDestacado);
         A25NewBlogBorrador = false;
         AssignAttri("", false, "A25NewBlogBorrador", A25NewBlogBorrador);
         Z14NewBlogTitulo = "";
         Z15NewBlogSubTitulo = "";
         Z17NewBlogDescripcionCorta = "";
         Z18NewBlogVisitas = 0;
         Z19NewBlogDestacado = false;
         Z25NewBlogBorrador = false;
         Z20CategoriasId = 0;
      }

      protected void InitAll033( )
      {
         A12NewBlogId = 0;
         AssignAttri("", false, "A12NewBlogId", StringUtil.LTrimStr( (decimal)(A12NewBlogId), 4, 0));
         InitializeNonKey033( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623411193", true, true);
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
         context.AddJavascriptSource("newblog.js", "?2024121623411197", false, true);
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
         Newblogdescripcion_Internalname = "NEWBLOGDESCRIPCION";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         edtNewBlogTitulo_Internalname = "NEWBLOGTITULO";
         edtNewBlogSubTitulo_Internalname = "NEWBLOGSUBTITULO";
         edtNewBlogDescripcionCorta_Internalname = "NEWBLOGDESCRIPCIONCORTA";
         imgNewBlogImagen_Internalname = "NEWBLOGIMAGEN";
         chkNewBlogDestacado_Internalname = "NEWBLOGDESTACADO";
         chkNewBlogBorrador_Internalname = "NEWBLOGBORRADOR";
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
         edtNewBlogVisitas_Internalname = "NEWBLOGVISITAS";
         edtNewBlogId_Internalname = "NEWBLOGID";
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
         Form.Caption = context.GetMessage( "New Blog", "");
         edtNewBlogId_Jsonclick = "";
         edtNewBlogId_Enabled = 0;
         edtNewBlogId_Visible = 1;
         edtNewBlogVisitas_Jsonclick = "";
         edtNewBlogVisitas_Enabled = 1;
         edtNewBlogVisitas_Visible = 1;
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
         Combo_categoriasid_Datalistprocparametersprefix = " \"ComboName\": \"CategoriasId\", \"TrnMode\": \"INS\", \"IsDynamicCall\": true, \"NewBlogId\": 0";
         Combo_categoriasid_Datalistproc = "NewBlogLoadDVCombo";
         Combo_categoriasid_Cls = "ExtendedCombo Attribute";
         Combo_categoriasid_Caption = "";
         Combo_categoriasid_Enabled = Convert.ToBoolean( -1);
         chkNewBlogBorrador.Enabled = 1;
         chkNewBlogDestacado.Enabled = 1;
         imgNewBlogImagen_Enabled = 1;
         edtNewBlogDescripcionCorta_Enabled = 1;
         edtNewBlogSubTitulo_Enabled = 1;
         edtNewBlogTitulo_Enabled = 1;
         Newblogdescripcion_Captionposition = "None";
         Newblogdescripcion_Captionstyle = "width: 25%;";
         Newblogdescripcion_Captionclass = "gx-form-item AttributeLabel";
         Newblogdescripcion_Color = (int)(0xD3D3D3);
         Newblogdescripcion_Toolbarexpanded = Convert.ToBoolean( -1);
         Newblogdescripcion_Toolbarcancollapse = Convert.ToBoolean( 0);
         Newblogdescripcion_Toolbar = "Default";
         Newblogdescripcion_Height = "430";
         Newblogdescripcion_Width = "100%";
         Newblogdescripcion_Enabled = Convert.ToBoolean( 1);
         Dvpanel_tableattributes_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Iconposition = "Right";
         Dvpanel_tableattributes_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Collapsible = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Title = context.GetMessage( "Nueva Entrada en el Blog", "");
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
         chkNewBlogDestacado.Name = "NEWBLOGDESTACADO";
         chkNewBlogDestacado.WebTags = "";
         chkNewBlogDestacado.Caption = context.GetMessage( "Destacado", "");
         AssignProp("", false, chkNewBlogDestacado_Internalname, "TitleCaption", chkNewBlogDestacado.Caption, true);
         chkNewBlogDestacado.CheckedValue = "false";
         A19NewBlogDestacado = StringUtil.StrToBool( StringUtil.BoolToStr( A19NewBlogDestacado));
         AssignAttri("", false, "A19NewBlogDestacado", A19NewBlogDestacado);
         chkNewBlogBorrador.Name = "NEWBLOGBORRADOR";
         chkNewBlogBorrador.WebTags = "";
         chkNewBlogBorrador.Caption = context.GetMessage( "Borrador", "");
         AssignProp("", false, chkNewBlogBorrador_Internalname, "TitleCaption", chkNewBlogBorrador.Caption, true);
         chkNewBlogBorrador.CheckedValue = "false";
         A25NewBlogBorrador = StringUtil.StrToBool( StringUtil.BoolToStr( A25NewBlogBorrador));
         AssignAttri("", false, "A25NewBlogBorrador", A25NewBlogBorrador);
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
         /* Using cursor T000316 */
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
         setEventMetadata("ENTER","""{"handler":"UserMainFullajax","iparms":[{"postForm":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV7NewBlogId","fld":"vNEWBLOGID","pic":"ZZZ9","hsh":true},{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]}""");
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true},{"av":"AV18IsAuthorized_UserAction1","fld":"vISAUTHORIZED_USERACTION1","hsh":true},{"av":"AV7NewBlogId","fld":"vNEWBLOGID","pic":"ZZZ9","hsh":true},{"av":"A12NewBlogId","fld":"NEWBLOGID","pic":"ZZZ9"},{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]}""");
         setEventMetadata("AFTER TRN","""{"handler":"E12032","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true},{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]""");
         setEventMetadata("AFTER TRN",""","oparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]}""");
         setEventMetadata("'DOUSERACTION1'","""{"handler":"E13032","iparms":[{"av":"AV18IsAuthorized_UserAction1","fld":"vISAUTHORIZED_USERACTION1","hsh":true},{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]""");
         setEventMetadata("'DOUSERACTION1'",""","oparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]}""");
         setEventMetadata("VALID_CATEGORIASID","""{"handler":"Valid_Categoriasid","iparms":[{"av":"A20CategoriasId","fld":"CATEGORIASID","pic":"ZZZ9"},{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]""");
         setEventMetadata("VALID_CATEGORIASID",""","oparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]}""");
         setEventMetadata("VALIDV_COMBOCATEGORIASID","""{"handler":"Validv_Combocategoriasid","iparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]""");
         setEventMetadata("VALIDV_COMBOCATEGORIASID",""","oparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]}""");
         setEventMetadata("VALID_NEWBLOGID","""{"handler":"Valid_Newblogid","iparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]""");
         setEventMetadata("VALID_NEWBLOGID",""","oparms":[{"av":"A19NewBlogDestacado","fld":"NEWBLOGDESTACADO"},{"av":"A25NewBlogBorrador","fld":"NEWBLOGBORRADOR"}]}""");
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
         Z14NewBlogTitulo = "";
         Z15NewBlogSubTitulo = "";
         Z17NewBlogDescripcionCorta = "";
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
         ucNewblogdescripcion = new GXUserControl();
         NewBlogDescripcion = "";
         TempTags = "";
         A14NewBlogTitulo = "";
         A15NewBlogSubTitulo = "";
         A17NewBlogDescripcionCorta = "";
         A13NewBlogImagen = "";
         A40000NewBlogImagen_GXI = "";
         sImgUrl = "";
         lblTextblockcategoriasid_Jsonclick = "";
         ucCombo_categoriasid = new GXUserControl();
         AV19DDO_TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         AV26CategoriasId_Data = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Item", "");
         bttBtnuseraction1_Jsonclick = "";
         lblTextblock1_Jsonclick = "";
         bttBtntrn_enter_Jsonclick = "";
         bttBtntrn_cancel_Jsonclick = "";
         bttBtntrn_delete_Jsonclick = "";
         A16NewBlogDescripcion = "";
         AV27Pgmname = "";
         Newblogdescripcion_Objectcall = "";
         Newblogdescripcion_Class = "";
         Newblogdescripcion_Skin = "";
         Newblogdescripcion_Customtoolbar = "";
         Newblogdescripcion_Customconfiguration = "";
         Newblogdescripcion_Buttonpressedid = "";
         Newblogdescripcion_Captionvalue = "";
         Newblogdescripcion_Coltitle = "";
         Newblogdescripcion_Coltitlefont = "";
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
         sMode3 = "";
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
         AV14TrnContextAtt = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute(context);
         AV21Combo_DataJson = "";
         AV16ComboSelectedValue = "";
         AV20ComboSelectedText = "";
         GXEncryptionTmp = "";
         GXt_char2 = "";
         Z13NewBlogImagen = "";
         Z40000NewBlogImagen_GXI = "";
         Z16NewBlogDescripcion = "";
         T00035_A12NewBlogId = new short[1] ;
         T00035_A40000NewBlogImagen_GXI = new string[] {""} ;
         T00035_A14NewBlogTitulo = new string[] {""} ;
         T00035_A15NewBlogSubTitulo = new string[] {""} ;
         T00035_A16NewBlogDescripcion = new string[] {""} ;
         T00035_A17NewBlogDescripcionCorta = new string[] {""} ;
         T00035_A18NewBlogVisitas = new short[1] ;
         T00035_A19NewBlogDestacado = new bool[] {false} ;
         T00035_A25NewBlogBorrador = new bool[] {false} ;
         T00035_A20CategoriasId = new short[1] ;
         T00035_A13NewBlogImagen = new string[] {""} ;
         T00034_A20CategoriasId = new short[1] ;
         T00036_A20CategoriasId = new short[1] ;
         T00037_A12NewBlogId = new short[1] ;
         T00033_A12NewBlogId = new short[1] ;
         T00033_A40000NewBlogImagen_GXI = new string[] {""} ;
         T00033_A14NewBlogTitulo = new string[] {""} ;
         T00033_A15NewBlogSubTitulo = new string[] {""} ;
         T00033_A16NewBlogDescripcion = new string[] {""} ;
         T00033_A17NewBlogDescripcionCorta = new string[] {""} ;
         T00033_A18NewBlogVisitas = new short[1] ;
         T00033_A19NewBlogDestacado = new bool[] {false} ;
         T00033_A25NewBlogBorrador = new bool[] {false} ;
         T00033_A20CategoriasId = new short[1] ;
         T00033_A13NewBlogImagen = new string[] {""} ;
         T00038_A12NewBlogId = new short[1] ;
         T00039_A12NewBlogId = new short[1] ;
         T00032_A12NewBlogId = new short[1] ;
         T00032_A40000NewBlogImagen_GXI = new string[] {""} ;
         T00032_A14NewBlogTitulo = new string[] {""} ;
         T00032_A15NewBlogSubTitulo = new string[] {""} ;
         T00032_A16NewBlogDescripcion = new string[] {""} ;
         T00032_A17NewBlogDescripcionCorta = new string[] {""} ;
         T00032_A18NewBlogVisitas = new short[1] ;
         T00032_A19NewBlogDestacado = new bool[] {false} ;
         T00032_A25NewBlogBorrador = new bool[] {false} ;
         T00032_A20CategoriasId = new short[1] ;
         T00032_A13NewBlogImagen = new string[] {""} ;
         T000311_A12NewBlogId = new short[1] ;
         T000315_A12NewBlogId = new short[1] ;
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXCCtlgxBlob = "";
         T000316_A20CategoriasId = new short[1] ;
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.newblog__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newblog__default(),
            new Object[][] {
                new Object[] {
               T00032_A12NewBlogId, T00032_A40000NewBlogImagen_GXI, T00032_A14NewBlogTitulo, T00032_A15NewBlogSubTitulo, T00032_A16NewBlogDescripcion, T00032_A17NewBlogDescripcionCorta, T00032_A18NewBlogVisitas, T00032_A19NewBlogDestacado, T00032_A25NewBlogBorrador, T00032_A20CategoriasId,
               T00032_A13NewBlogImagen
               }
               , new Object[] {
               T00033_A12NewBlogId, T00033_A40000NewBlogImagen_GXI, T00033_A14NewBlogTitulo, T00033_A15NewBlogSubTitulo, T00033_A16NewBlogDescripcion, T00033_A17NewBlogDescripcionCorta, T00033_A18NewBlogVisitas, T00033_A19NewBlogDestacado, T00033_A25NewBlogBorrador, T00033_A20CategoriasId,
               T00033_A13NewBlogImagen
               }
               , new Object[] {
               T00034_A20CategoriasId
               }
               , new Object[] {
               T00035_A12NewBlogId, T00035_A40000NewBlogImagen_GXI, T00035_A14NewBlogTitulo, T00035_A15NewBlogSubTitulo, T00035_A16NewBlogDescripcion, T00035_A17NewBlogDescripcionCorta, T00035_A18NewBlogVisitas, T00035_A19NewBlogDestacado, T00035_A25NewBlogBorrador, T00035_A20CategoriasId,
               T00035_A13NewBlogImagen
               }
               , new Object[] {
               T00036_A20CategoriasId
               }
               , new Object[] {
               T00037_A12NewBlogId
               }
               , new Object[] {
               T00038_A12NewBlogId
               }
               , new Object[] {
               T00039_A12NewBlogId
               }
               , new Object[] {
               }
               , new Object[] {
               T000311_A12NewBlogId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               T000315_A12NewBlogId
               }
               , new Object[] {
               T000316_A20CategoriasId
               }
            }
         );
         AV27Pgmname = "NewBlog";
      }

      private short wcpOAV7NewBlogId ;
      private short Z12NewBlogId ;
      private short Z18NewBlogVisitas ;
      private short Z20CategoriasId ;
      private short N20CategoriasId ;
      private short GxWebError ;
      private short A20CategoriasId ;
      private short AV7NewBlogId ;
      private short AnyError ;
      private short IsModified ;
      private short IsConfirmed ;
      private short nKeyPressed ;
      private short AV17ComboCategoriasId ;
      private short A18NewBlogVisitas ;
      private short A12NewBlogId ;
      private short AV13Insert_CategoriasId ;
      private short RcdFound3 ;
      private short gxcookieaux ;
      private short Gx_BScreen ;
      private short gxajaxcallmode ;
      private int trnEnded ;
      private int Newblogdescripcion_Color ;
      private int edtNewBlogTitulo_Enabled ;
      private int edtNewBlogSubTitulo_Enabled ;
      private int edtNewBlogDescripcionCorta_Enabled ;
      private int imgNewBlogImagen_Enabled ;
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
      private int edtNewBlogVisitas_Enabled ;
      private int edtNewBlogVisitas_Visible ;
      private int edtNewBlogId_Enabled ;
      private int edtNewBlogId_Visible ;
      private int Newblogdescripcion_Coltitlecolor ;
      private int Combo_categoriasid_Datalistupdateminimumcharacters ;
      private int Dvpanel_tableattributes_Gxcontroltype ;
      private int AV28GXV1 ;
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
      private string edtNewBlogTitulo_Internalname ;
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
      private string Newblogdescripcion_Width ;
      private string Newblogdescripcion_Height ;
      private string Newblogdescripcion_Toolbar ;
      private string Newblogdescripcion_Captionclass ;
      private string Newblogdescripcion_Captionstyle ;
      private string Newblogdescripcion_Captionposition ;
      private string Newblogdescripcion_Internalname ;
      private string divUnnamedtable2_Internalname ;
      private string divUnnamedtable3_Internalname ;
      private string TempTags ;
      private string edtNewBlogSubTitulo_Internalname ;
      private string edtNewBlogDescripcionCorta_Internalname ;
      private string divUnnamedtable6_Internalname ;
      private string divUnnamedtable7_Internalname ;
      private string imgNewBlogImagen_Internalname ;
      private string sImgUrl ;
      private string chkNewBlogDestacado_Internalname ;
      private string chkNewBlogBorrador_Internalname ;
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
      private string edtNewBlogVisitas_Internalname ;
      private string edtNewBlogVisitas_Jsonclick ;
      private string edtNewBlogId_Internalname ;
      private string edtNewBlogId_Jsonclick ;
      private string AV27Pgmname ;
      private string Newblogdescripcion_Objectcall ;
      private string Newblogdescripcion_Class ;
      private string Newblogdescripcion_Skin ;
      private string Newblogdescripcion_Customtoolbar ;
      private string Newblogdescripcion_Customconfiguration ;
      private string Newblogdescripcion_Buttonpressedid ;
      private string Newblogdescripcion_Captionvalue ;
      private string Newblogdescripcion_Coltitle ;
      private string Newblogdescripcion_Coltitlefont ;
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
      private string sMode3 ;
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
      private bool Z19NewBlogDestacado ;
      private bool Z25NewBlogBorrador ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbErr ;
      private bool A19NewBlogDestacado ;
      private bool A25NewBlogBorrador ;
      private bool Dvpanel_tableattributes_Autowidth ;
      private bool Dvpanel_tableattributes_Autoheight ;
      private bool Dvpanel_tableattributes_Collapsible ;
      private bool Dvpanel_tableattributes_Collapsed ;
      private bool Dvpanel_tableattributes_Showcollapseicon ;
      private bool Dvpanel_tableattributes_Autoscroll ;
      private bool Newblogdescripcion_Toolbarcancollapse ;
      private bool Newblogdescripcion_Toolbarexpanded ;
      private bool A13NewBlogImagen_IsBlob ;
      private bool Newblogdescripcion_Enabled ;
      private bool Newblogdescripcion_Isabstractlayoutcontrol ;
      private bool Newblogdescripcion_Usercontroliscolumn ;
      private bool Newblogdescripcion_Visible ;
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
      private bool AV18IsAuthorized_UserAction1 ;
      private bool GXt_boolean3 ;
      private bool Gx_longc ;
      private string NewBlogDescripcion ;
      private string A16NewBlogDescripcion ;
      private string AV21Combo_DataJson ;
      private string Z16NewBlogDescripcion ;
      private string Z14NewBlogTitulo ;
      private string Z15NewBlogSubTitulo ;
      private string Z17NewBlogDescripcionCorta ;
      private string A14NewBlogTitulo ;
      private string A15NewBlogSubTitulo ;
      private string A17NewBlogDescripcionCorta ;
      private string A40000NewBlogImagen_GXI ;
      private string AV16ComboSelectedValue ;
      private string AV20ComboSelectedText ;
      private string Z40000NewBlogImagen_GXI ;
      private string A13NewBlogImagen ;
      private string Z13NewBlogImagen ;
      private IGxSession AV12WebSession ;
      private GXProperties forbiddenHiddens ;
      private GXUserControl ucDvpanel_tableattributes ;
      private GXUserControl ucNewblogdescripcion ;
      private GXUserControl ucCombo_categoriasid ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCheckbox chkNewBlogDestacado ;
      private GXCheckbox chkNewBlogBorrador ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV19DDO_TitleSettingsIcons ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> AV26CategoriasId_Data ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons GXt_SdtDVB_SDTDropDownOptionsTitleSettingsIcons1 ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV22GAMSession ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV23GAMErrors ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV11TrnContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute AV14TrnContextAtt ;
      private IDataStoreProvider pr_default ;
      private short[] T00035_A12NewBlogId ;
      private string[] T00035_A40000NewBlogImagen_GXI ;
      private string[] T00035_A14NewBlogTitulo ;
      private string[] T00035_A15NewBlogSubTitulo ;
      private string[] T00035_A16NewBlogDescripcion ;
      private string[] T00035_A17NewBlogDescripcionCorta ;
      private short[] T00035_A18NewBlogVisitas ;
      private bool[] T00035_A19NewBlogDestacado ;
      private bool[] T00035_A25NewBlogBorrador ;
      private short[] T00035_A20CategoriasId ;
      private string[] T00035_A13NewBlogImagen ;
      private short[] T00034_A20CategoriasId ;
      private short[] T00036_A20CategoriasId ;
      private short[] T00037_A12NewBlogId ;
      private short[] T00033_A12NewBlogId ;
      private string[] T00033_A40000NewBlogImagen_GXI ;
      private string[] T00033_A14NewBlogTitulo ;
      private string[] T00033_A15NewBlogSubTitulo ;
      private string[] T00033_A16NewBlogDescripcion ;
      private string[] T00033_A17NewBlogDescripcionCorta ;
      private short[] T00033_A18NewBlogVisitas ;
      private bool[] T00033_A19NewBlogDestacado ;
      private bool[] T00033_A25NewBlogBorrador ;
      private short[] T00033_A20CategoriasId ;
      private string[] T00033_A13NewBlogImagen ;
      private short[] T00038_A12NewBlogId ;
      private short[] T00039_A12NewBlogId ;
      private short[] T00032_A12NewBlogId ;
      private string[] T00032_A40000NewBlogImagen_GXI ;
      private string[] T00032_A14NewBlogTitulo ;
      private string[] T00032_A15NewBlogSubTitulo ;
      private string[] T00032_A16NewBlogDescripcion ;
      private string[] T00032_A17NewBlogDescripcionCorta ;
      private short[] T00032_A18NewBlogVisitas ;
      private bool[] T00032_A19NewBlogDestacado ;
      private bool[] T00032_A25NewBlogBorrador ;
      private short[] T00032_A20CategoriasId ;
      private string[] T00032_A13NewBlogImagen ;
      private short[] T000311_A12NewBlogId ;
      private short[] T000315_A12NewBlogId ;
      private short[] T000316_A20CategoriasId ;
      private IDataStoreProvider pr_gam ;
   }

   public class newblog__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class newblog__default : DataStoreHelperBase, IDataStoreHelper
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
        Object[] prmT00032;
        prmT00032 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT00033;
        prmT00033 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT00034;
        prmT00034 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmT00035;
        prmT00035 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT00036;
        prmT00036 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmT00037;
        prmT00037 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT00038;
        prmT00038 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT00039;
        prmT00039 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT000310;
        prmT000310 = new Object[] {
        new ParDef("@NewBlogImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewBlogImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewBlog", Fld="NewBlogImagen"} ,
        new ParDef("@NewBlogTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogSubTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogDescripcion",GXType.Char,102400,0) ,
        new ParDef("@NewBlogDescripcionCorta",GXType.Char,500,0) ,
        new ParDef("@NewBlogVisitas",GXType.Int16,4,0) ,
        new ParDef("@NewBlogDestacado",GXType.Byte,4,0) ,
        new ParDef("@NewBlogBorrador",GXType.Byte,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmT000311;
        prmT000311 = new Object[] {
        };
        Object[] prmT000312;
        prmT000312 = new Object[] {
        new ParDef("@NewBlogTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogSubTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogDescripcion",GXType.Char,102400,0) ,
        new ParDef("@NewBlogDescripcionCorta",GXType.Char,500,0) ,
        new ParDef("@NewBlogVisitas",GXType.Int16,4,0) ,
        new ParDef("@NewBlogDestacado",GXType.Byte,4,0) ,
        new ParDef("@NewBlogBorrador",GXType.Byte,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0) ,
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT000313;
        prmT000313 = new Object[] {
        new ParDef("@NewBlogImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewBlogImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewBlog", Fld="NewBlogImagen"} ,
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT000314;
        prmT000314 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmT000315;
        prmT000315 = new Object[] {
        };
        Object[] prmT000316;
        prmT000316 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        def= new CursorDef[] {
            new CursorDef("T00032", "SELECT `NewBlogId`, `NewBlogImagen_GXI`, `NewBlogTitulo`, `NewBlogSubTitulo`, `NewBlogDescripcion`, `NewBlogDescripcionCorta`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogBorrador`, `CategoriasId`, `NewBlogImagen` FROM `NewBlog` WHERE `NewBlogId` = @NewBlogId  FOR UPDATE ",true, GxErrorMask.GX_NOMASK, false, this,prmT00032,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00033", "SELECT `NewBlogId`, `NewBlogImagen_GXI`, `NewBlogTitulo`, `NewBlogSubTitulo`, `NewBlogDescripcion`, `NewBlogDescripcionCorta`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogBorrador`, `CategoriasId`, `NewBlogImagen` FROM `NewBlog` WHERE `NewBlogId` = @NewBlogId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00033,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00034", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00034,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00035", "SELECT TM1.`NewBlogId`, TM1.`NewBlogImagen_GXI`, TM1.`NewBlogTitulo`, TM1.`NewBlogSubTitulo`, TM1.`NewBlogDescripcion`, TM1.`NewBlogDescripcionCorta`, TM1.`NewBlogVisitas`, TM1.`NewBlogDestacado`, TM1.`NewBlogBorrador`, TM1.`CategoriasId`, TM1.`NewBlogImagen` FROM `NewBlog` TM1 WHERE TM1.`NewBlogId` = @NewBlogId ORDER BY TM1.`NewBlogId` ",true, GxErrorMask.GX_NOMASK, false, this,prmT00035,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00036", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00036,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00037", "SELECT `NewBlogId` FROM `NewBlog` WHERE `NewBlogId` = @NewBlogId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00037,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00038", "SELECT `NewBlogId` FROM `NewBlog` WHERE ( `NewBlogId` > @NewBlogId) ORDER BY `NewBlogId`  LIMIT 1",true, GxErrorMask.GX_NOMASK, false, this,prmT00038,1, GxCacheFrequency.OFF ,true,true )
           ,new CursorDef("T00039", "SELECT `NewBlogId` FROM `NewBlog` WHERE ( `NewBlogId` < @NewBlogId) ORDER BY `NewBlogId` DESC  LIMIT 1",true, GxErrorMask.GX_NOMASK, false, this,prmT00039,1, GxCacheFrequency.OFF ,true,true )
           ,new CursorDef("T000310", "INSERT INTO `NewBlog`(`NewBlogImagen`, `NewBlogImagen_GXI`, `NewBlogTitulo`, `NewBlogSubTitulo`, `NewBlogDescripcion`, `NewBlogDescripcionCorta`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogBorrador`, `CategoriasId`) VALUES(@NewBlogImagen, @NewBlogImagen_GXI, @NewBlogTitulo, @NewBlogSubTitulo, @NewBlogDescripcion, @NewBlogDescripcionCorta, @NewBlogVisitas, @NewBlogDestacado, @NewBlogBorrador, @CategoriasId)", GxErrorMask.GX_NOMASK,prmT000310)
           ,new CursorDef("T000311", "SELECT LAST_INSERT_ID() ",true, GxErrorMask.GX_NOMASK, false, this,prmT000311,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T000312", "UPDATE `NewBlog` SET `NewBlogTitulo`=@NewBlogTitulo, `NewBlogSubTitulo`=@NewBlogSubTitulo, `NewBlogDescripcion`=@NewBlogDescripcion, `NewBlogDescripcionCorta`=@NewBlogDescripcionCorta, `NewBlogVisitas`=@NewBlogVisitas, `NewBlogDestacado`=@NewBlogDestacado, `NewBlogBorrador`=@NewBlogBorrador, `CategoriasId`=@CategoriasId  WHERE `NewBlogId` = @NewBlogId", GxErrorMask.GX_NOMASK,prmT000312)
           ,new CursorDef("T000313", "UPDATE `NewBlog` SET `NewBlogImagen`=@NewBlogImagen, `NewBlogImagen_GXI`=@NewBlogImagen_GXI  WHERE `NewBlogId` = @NewBlogId", GxErrorMask.GX_NOMASK,prmT000313)
           ,new CursorDef("T000314", "DELETE FROM `NewBlog`  WHERE `NewBlogId` = @NewBlogId", GxErrorMask.GX_NOMASK,prmT000314)
           ,new CursorDef("T000315", "SELECT `NewBlogId` FROM `NewBlog` ORDER BY `NewBlogId` ",true, GxErrorMask.GX_NOMASK, false, this,prmT000315,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T000316", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmT000316,1, GxCacheFrequency.OFF ,true,false )
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
              ((string[]) buf[5])[0] = rslt.getVarchar(6);
              ((short[]) buf[6])[0] = rslt.getShort(7);
              ((bool[]) buf[7])[0] = rslt.getBool(8);
              ((bool[]) buf[8])[0] = rslt.getBool(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((string[]) buf[10])[0] = rslt.getMultimediaFile(11, rslt.getVarchar(2));
              return;
           case 1 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((string[]) buf[5])[0] = rslt.getVarchar(6);
              ((short[]) buf[6])[0] = rslt.getShort(7);
              ((bool[]) buf[7])[0] = rslt.getBool(8);
              ((bool[]) buf[8])[0] = rslt.getBool(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((string[]) buf[10])[0] = rslt.getMultimediaFile(11, rslt.getVarchar(2));
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
              ((string[]) buf[5])[0] = rslt.getVarchar(6);
              ((short[]) buf[6])[0] = rslt.getShort(7);
              ((bool[]) buf[7])[0] = rslt.getBool(8);
              ((bool[]) buf[8])[0] = rslt.getBool(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((string[]) buf[10])[0] = rslt.getMultimediaFile(11, rslt.getVarchar(2));
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
