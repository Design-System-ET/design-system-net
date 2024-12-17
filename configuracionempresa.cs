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
   public class configuracionempresa : GXDataArea
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
            if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "configuracionempresa.aspx")), "configuracionempresa.aspx") == 0 ) )
            {
               SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "configuracionempresa.aspx")))) ;
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
                  AV7ConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( GetPar( "ConfiguracionEmpresaId"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "AV7ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(AV7ConfiguracionEmpresaId), 4, 0));
                  GxWebStd.gx_hidden_field( context, "gxhash_vCONFIGURACIONEMPRESAID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV7ConfiguracionEmpresaId), "ZZZ9"), context));
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
         Form.Meta.addItem("description", context.GetMessage( "Configuracion Empresa", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         if ( ! context.isAjaxRequest( ) )
         {
            GX_FocusControl = edtConfiguracionEmpresaTelefono_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         wbErr = false;
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      public configuracionempresa( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public configuracionempresa( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_Gx_mode ,
                           short aP1_ConfiguracionEmpresaId )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV7ConfiguracionEmpresaId = aP1_ConfiguracionEmpresaId;
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
            return "configuracionempresa_Execute" ;
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-lg-9", "start", "top", "", "", "div");
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaId_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaId_Internalname, context.GetMessage( "Id", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaId_Internalname, StringUtil.LTrim( StringUtil.NToC( (decimal)(A44ConfiguracionEmpresaId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaId_Enabled!=0) ? context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9") : context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9"))), " dir=\"ltr\" inputmode=\"numeric\" pattern=\"[0-9]*\""+TempTags+" onchange=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_integer( this,gx.thousandSeparator);"+";gx.evt.onblur(this,22);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaId_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaId_Enabled, 0, "text", "1", 4, "chr", 1, "row", 4, 0, 0, 0, 0, -1, 0, true, "Id", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaTelefono_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaTelefono_Internalname, context.GetMessage( "Telefono", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         if ( context.isSmartDevice( ) )
         {
            gxphoneLink = "tel:" + StringUtil.RTrim( A45ConfiguracionEmpresaTelefono);
         }
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 26,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaTelefono_Internalname, StringUtil.RTrim( A45ConfiguracionEmpresaTelefono), StringUtil.RTrim( context.localUtil.Format( A45ConfiguracionEmpresaTelefono, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,26);\"", "'"+""+"'"+",false,"+"'"+""+"'", gxphoneLink, "", "", "", edtConfiguracionEmpresaTelefono_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaTelefono_Enabled, 0, "tel", "", 20, "chr", 1, "row", 20, 0, 0, 0, 0, -1, 0, true, "GeneXus\\Phone", "start", true, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCostoPlanB_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCostoPlanB_Internalname, context.GetMessage( "Plan Basico", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 31,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCostoPlanB_Internalname, StringUtil.LTrim( StringUtil.NToC( A46ConfiguracionEmpresaCostoPlanB, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCostoPlanB_Enabled!=0) ? context.localUtil.Format( A46ConfiguracionEmpresaCostoPlanB, "ZZZZZZZZ9.99") : context.localUtil.Format( A46ConfiguracionEmpresaCostoPlanB, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,31);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCostoPlanB_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCostoPlanB_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCuotaPlanB_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCuotaPlanB_Internalname, context.GetMessage( "Plan Basico", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 35,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCuotaPlanB_Internalname, StringUtil.LTrim( StringUtil.NToC( A47ConfiguracionEmpresaCuotaPlanB, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCuotaPlanB_Enabled!=0) ? context.localUtil.Format( A47ConfiguracionEmpresaCuotaPlanB, "ZZZZZZZZ9.99") : context.localUtil.Format( A47ConfiguracionEmpresaCuotaPlanB, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,35);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCuotaPlanB_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCuotaPlanB_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCostoPlanS_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCostoPlanS_Internalname, context.GetMessage( "Plan Superior", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCostoPlanS_Internalname, StringUtil.LTrim( StringUtil.NToC( A48ConfiguracionEmpresaCostoPlanS, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCostoPlanS_Enabled!=0) ? context.localUtil.Format( A48ConfiguracionEmpresaCostoPlanS, "ZZZZZZZZ9.99") : context.localUtil.Format( A48ConfiguracionEmpresaCostoPlanS, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,40);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCostoPlanS_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCostoPlanS_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCuotaPlanS_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCuotaPlanS_Internalname, context.GetMessage( "Plan Superior", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCuotaPlanS_Internalname, StringUtil.LTrim( StringUtil.NToC( A49ConfiguracionEmpresaCuotaPlanS, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCuotaPlanS_Enabled!=0) ? context.localUtil.Format( A49ConfiguracionEmpresaCuotaPlanS, "ZZZZZZZZ9.99") : context.localUtil.Format( A49ConfiguracionEmpresaCuotaPlanS, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,44);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCuotaPlanS_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCuotaPlanS_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCostoPlanN_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCostoPlanN_Internalname, context.GetMessage( "Plan Negocios", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 49,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCostoPlanN_Internalname, StringUtil.LTrim( StringUtil.NToC( A50ConfiguracionEmpresaCostoPlanN, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCostoPlanN_Enabled!=0) ? context.localUtil.Format( A50ConfiguracionEmpresaCostoPlanN, "ZZZZZZZZ9.99") : context.localUtil.Format( A50ConfiguracionEmpresaCostoPlanN, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,49);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCostoPlanN_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCostoPlanN_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCuotaPlanN_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCuotaPlanN_Internalname, context.GetMessage( "Plan Negocios", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 53,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCuotaPlanN_Internalname, StringUtil.LTrim( StringUtil.NToC( A51ConfiguracionEmpresaCuotaPlanN, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCuotaPlanN_Enabled!=0) ? context.localUtil.Format( A51ConfiguracionEmpresaCuotaPlanN, "ZZZZZZZZ9.99") : context.localUtil.Format( A51ConfiguracionEmpresaCuotaPlanN, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,53);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCuotaPlanN_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCuotaPlanN_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCostoLandi_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCostoLandi_Internalname, context.GetMessage( "Landing Page", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 58,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCostoLandi_Internalname, StringUtil.LTrim( StringUtil.NToC( A54ConfiguracionEmpresaCostoLandi, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCostoLandi_Enabled!=0) ? context.localUtil.Format( A54ConfiguracionEmpresaCostoLandi, "ZZZZZZZZ9.99") : context.localUtil.Format( A54ConfiguracionEmpresaCostoLandi, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,58);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCostoLandi_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCostoLandi_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell DscTop", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtConfiguracionEmpresaCuotaLandi_Internalname+"\"", "", "div");
         /* Attribute/Variable Label */
         GxWebStd.gx_label_element( context, edtConfiguracionEmpresaCuotaLandi_Internalname, context.GetMessage( "Landing Page", ""), " AttributeLabel", 1, true, "");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 62,'',false,'',0)\"";
         GxWebStd.gx_single_line_edit( context, edtConfiguracionEmpresaCuotaLandi_Internalname, StringUtil.LTrim( StringUtil.NToC( A55ConfiguracionEmpresaCuotaLandi, 12, 2, context.GetLanguageProperty( "decimal_point"), "")), StringUtil.LTrim( ((edtConfiguracionEmpresaCuotaLandi_Enabled!=0) ? context.localUtil.Format( A55ConfiguracionEmpresaCuotaLandi, "ZZZZZZZZ9.99") : context.localUtil.Format( A55ConfiguracionEmpresaCuotaLandi, "ZZZZZZZZ9.99"))), TempTags+" onchange=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onchange(this, event)\" "+" onblur=\""+"gx.num.valid_decimal( this, gx.thousandSeparator,gx.decimalPoint,'2');"+";gx.evt.onblur(this,62);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtConfiguracionEmpresaCuotaLandi_Jsonclick, 0, "Attribute", "", "", "", "", 1, edtConfiguracionEmpresaCuotaLandi_Enabled, 0, "text", "", 12, "chr", 1, "row", 12, 0, 0, 0, 0, -1, 0, true, "Precio", "end", false, "", "HLP_ConfiguracionEmpresa.htm");
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
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group CellMarginTop10", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 67,'',false,'',0)\"";
         ClassString = "ButtonMaterial";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_enter_Internalname, "", context.GetMessage( "GX_BtnEnter", ""), bttBtntrn_enter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, bttBtntrn_enter_Visible, bttBtntrn_enter_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 69,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_cancel_Internalname, "", context.GetMessage( "GX_BtnCancel", ""), bttBtntrn_cancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, bttBtntrn_cancel_Visible, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_ConfiguracionEmpresa.htm");
         GxWebStd.gx_div_end( context, "start", "top", "div");
         /* Div Control */
         GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 71,'',false,'',0)\"";
         ClassString = "ButtonMaterialDefault";
         StyleString = "";
         GxWebStd.gx_button_ctrl( context, bttBtntrn_delete_Internalname, "", context.GetMessage( "GX_BtnDelete", ""), bttBtntrn_delete_Jsonclick, 5, context.GetMessage( "GX_BtnDelete", ""), "", StyleString, ClassString, bttBtntrn_delete_Visible, bttBtntrn_delete_Enabled, "standard", "'"+""+"'"+",false,"+"'"+"EDELETE."+"'", TempTags, "", context.GetButtonType( ), "HLP_ConfiguracionEmpresa.htm");
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
         E11072 ();
         context.wbGlbDoneStart = 1;
         assign_properties_default( ) ;
         if ( AnyError == 0 )
         {
            if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
            {
               /* Read saved SDTs. */
               /* Read saved values. */
               Z44ConfiguracionEmpresaId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "Z44ConfiguracionEmpresaId"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Z45ConfiguracionEmpresaTelefono = cgiGet( "Z45ConfiguracionEmpresaTelefono");
               Z46ConfiguracionEmpresaCostoPlanB = context.localUtil.CToN( cgiGet( "Z46ConfiguracionEmpresaCostoPlanB"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               Z47ConfiguracionEmpresaCuotaPlanB = context.localUtil.CToN( cgiGet( "Z47ConfiguracionEmpresaCuotaPlanB"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               Z48ConfiguracionEmpresaCostoPlanS = context.localUtil.CToN( cgiGet( "Z48ConfiguracionEmpresaCostoPlanS"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               Z49ConfiguracionEmpresaCuotaPlanS = context.localUtil.CToN( cgiGet( "Z49ConfiguracionEmpresaCuotaPlanS"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               Z50ConfiguracionEmpresaCostoPlanN = context.localUtil.CToN( cgiGet( "Z50ConfiguracionEmpresaCostoPlanN"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               Z51ConfiguracionEmpresaCuotaPlanN = context.localUtil.CToN( cgiGet( "Z51ConfiguracionEmpresaCuotaPlanN"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               Z54ConfiguracionEmpresaCostoLandi = context.localUtil.CToN( cgiGet( "Z54ConfiguracionEmpresaCostoLandi"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               Z55ConfiguracionEmpresaCuotaLandi = context.localUtil.CToN( cgiGet( "Z55ConfiguracionEmpresaCuotaLandi"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
               IsConfirmed = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsConfirmed"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               IsModified = (short)(Math.Round(context.localUtil.CToN( cgiGet( "IsModified"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Gx_mode = cgiGet( "Mode");
               AV7ConfiguracionEmpresaId = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vCONFIGURACIONEMPRESAID"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               Gx_BScreen = (short)(Math.Round(context.localUtil.CToN( cgiGet( "vGXBSCREEN"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
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
               A44ConfiguracionEmpresaId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
               A45ConfiguracionEmpresaTelefono = cgiGet( edtConfiguracionEmpresaTelefono_Internalname);
               AssignAttri("", false, "A45ConfiguracionEmpresaTelefono", A45ConfiguracionEmpresaTelefono);
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACOSTOPLANB");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCostoPlanB_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A46ConfiguracionEmpresaCostoPlanB = 0;
                  AssignAttri("", false, "A46ConfiguracionEmpresaCostoPlanB", StringUtil.LTrimStr( A46ConfiguracionEmpresaCostoPlanB, 12, 2));
               }
               else
               {
                  A46ConfiguracionEmpresaCostoPlanB = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A46ConfiguracionEmpresaCostoPlanB", StringUtil.LTrimStr( A46ConfiguracionEmpresaCostoPlanB, 12, 2));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACUOTAPLANB");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCuotaPlanB_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A47ConfiguracionEmpresaCuotaPlanB = 0;
                  AssignAttri("", false, "A47ConfiguracionEmpresaCuotaPlanB", StringUtil.LTrimStr( A47ConfiguracionEmpresaCuotaPlanB, 12, 2));
               }
               else
               {
                  A47ConfiguracionEmpresaCuotaPlanB = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanB_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A47ConfiguracionEmpresaCuotaPlanB", StringUtil.LTrimStr( A47ConfiguracionEmpresaCuotaPlanB, 12, 2));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACOSTOPLANS");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCostoPlanS_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A48ConfiguracionEmpresaCostoPlanS = 0;
                  AssignAttri("", false, "A48ConfiguracionEmpresaCostoPlanS", StringUtil.LTrimStr( A48ConfiguracionEmpresaCostoPlanS, 12, 2));
               }
               else
               {
                  A48ConfiguracionEmpresaCostoPlanS = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A48ConfiguracionEmpresaCostoPlanS", StringUtil.LTrimStr( A48ConfiguracionEmpresaCostoPlanS, 12, 2));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACUOTAPLANS");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCuotaPlanS_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A49ConfiguracionEmpresaCuotaPlanS = 0;
                  AssignAttri("", false, "A49ConfiguracionEmpresaCuotaPlanS", StringUtil.LTrimStr( A49ConfiguracionEmpresaCuotaPlanS, 12, 2));
               }
               else
               {
                  A49ConfiguracionEmpresaCuotaPlanS = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanS_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A49ConfiguracionEmpresaCuotaPlanS", StringUtil.LTrimStr( A49ConfiguracionEmpresaCuotaPlanS, 12, 2));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACOSTOPLANN");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCostoPlanN_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A50ConfiguracionEmpresaCostoPlanN = 0;
                  AssignAttri("", false, "A50ConfiguracionEmpresaCostoPlanN", StringUtil.LTrimStr( A50ConfiguracionEmpresaCostoPlanN, 12, 2));
               }
               else
               {
                  A50ConfiguracionEmpresaCostoPlanN = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A50ConfiguracionEmpresaCostoPlanN", StringUtil.LTrimStr( A50ConfiguracionEmpresaCostoPlanN, 12, 2));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACUOTAPLANN");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCuotaPlanN_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A51ConfiguracionEmpresaCuotaPlanN = 0;
                  AssignAttri("", false, "A51ConfiguracionEmpresaCuotaPlanN", StringUtil.LTrimStr( A51ConfiguracionEmpresaCuotaPlanN, 12, 2));
               }
               else
               {
                  A51ConfiguracionEmpresaCuotaPlanN = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaPlanN_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A51ConfiguracionEmpresaCuotaPlanN", StringUtil.LTrimStr( A51ConfiguracionEmpresaCuotaPlanN, 12, 2));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACOSTOLANDI");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCostoLandi_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A54ConfiguracionEmpresaCostoLandi = 0;
                  AssignAttri("", false, "A54ConfiguracionEmpresaCostoLandi", StringUtil.LTrimStr( A54ConfiguracionEmpresaCostoLandi, 12, 2));
               }
               else
               {
                  A54ConfiguracionEmpresaCostoLandi = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCostoLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A54ConfiguracionEmpresaCostoLandi", StringUtil.LTrimStr( A54ConfiguracionEmpresaCostoLandi, 12, 2));
               }
               if ( ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) < Convert.ToDecimal( 0 )) ) || ( ( context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")) > 999999999.99m ) ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_badnum", ""), 1, "CONFIGURACIONEMPRESACUOTALANDI");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaCuotaLandi_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  wbErr = true;
                  A55ConfiguracionEmpresaCuotaLandi = 0;
                  AssignAttri("", false, "A55ConfiguracionEmpresaCuotaLandi", StringUtil.LTrimStr( A55ConfiguracionEmpresaCuotaLandi, 12, 2));
               }
               else
               {
                  A55ConfiguracionEmpresaCuotaLandi = context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaCuotaLandi_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep"));
                  AssignAttri("", false, "A55ConfiguracionEmpresaCuotaLandi", StringUtil.LTrimStr( A55ConfiguracionEmpresaCuotaLandi, 12, 2));
               }
               /* Read subfile selected row values. */
               /* Read hidden variables. */
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               forbiddenHiddens = new GXProperties();
               forbiddenHiddens.Add("hshsalt", "hsh"+"ConfiguracionEmpresa");
               A44ConfiguracionEmpresaId = (short)(Math.Round(context.localUtil.CToN( cgiGet( edtConfiguracionEmpresaId_Internalname), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
               forbiddenHiddens.Add("ConfiguracionEmpresaId", context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9"));
               forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
               hsh = cgiGet( "hsh");
               if ( ( ! ( ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId ) ) || ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) ) && ! GXUtil.CheckEncryptedHash( forbiddenHiddens.ToString(), hsh, GXKey) )
               {
                  GXUtil.WriteLogError("configuracionempresa:[ SecurityCheckFailed (403 Forbidden) value for]"+forbiddenHiddens.ToJSonString());
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
                  A44ConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( GetPar( "ConfiguracionEmpresaId"), "."), 18, MidpointRounding.ToEven));
                  AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
                  getEqualNoModal( ) ;
                  if ( ! (0==AV7ConfiguracionEmpresaId) )
                  {
                     A44ConfiguracionEmpresaId = AV7ConfiguracionEmpresaId;
                     AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
                  }
                  else
                  {
                     if ( IsIns( )  && (0==A44ConfiguracionEmpresaId) && ( Gx_BScreen == 0 ) )
                     {
                        A44ConfiguracionEmpresaId = 1;
                        AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
                     }
                  }
                  Gx_mode = "DSP";
                  AssignAttri("", false, "Gx_mode", Gx_mode);
                  disable_std_buttons( ) ;
                  standaloneModal( ) ;
               }
               else
               {
                  if ( IsDsp( ) )
                  {
                     sMode8 = Gx_mode;
                     Gx_mode = "UPD";
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                     if ( ! (0==AV7ConfiguracionEmpresaId) )
                     {
                        A44ConfiguracionEmpresaId = AV7ConfiguracionEmpresaId;
                        AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
                     }
                     else
                     {
                        if ( IsIns( )  && (0==A44ConfiguracionEmpresaId) && ( Gx_BScreen == 0 ) )
                        {
                           A44ConfiguracionEmpresaId = 1;
                           AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
                        }
                     }
                     Gx_mode = sMode8;
                     AssignAttri("", false, "Gx_mode", Gx_mode);
                  }
                  standaloneModal( ) ;
                  if ( ! IsIns( ) )
                  {
                     getByPrimaryKey( ) ;
                     if ( RcdFound8 == 1 )
                     {
                        if ( IsDlt( ) )
                        {
                           /* Confirm record */
                           CONFIRM_070( ) ;
                           if ( AnyError == 0 )
                           {
                              GX_FocusControl = bttBtntrn_enter_Internalname;
                              AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                           }
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_noinsert", ""), 1, "CONFIGURACIONEMPRESAID");
                        AnyError = 1;
                        GX_FocusControl = edtConfiguracionEmpresaId_Internalname;
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
                           E11072 ();
                        }
                        else if ( StringUtil.StrCmp(sEvt, "AFTER TRN") == 0 )
                        {
                           context.wbHandled = 1;
                           dynload_actions( ) ;
                           /* Execute user event: After Trn */
                           E12072 ();
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
            E12072 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               /* Clear variables for new insertion. */
               InitAll078( ) ;
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
            DisableAttributes078( ) ;
         }
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

      protected void CONFIRM_070( )
      {
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls078( ) ;
            }
            else
            {
               CheckExtendedTable078( ) ;
               CloseExtendedTableCursors078( ) ;
            }
         }
         if ( AnyError == 0 )
         {
            IsConfirmed = 1;
            AssignAttri("", false, "IsConfirmed", StringUtil.LTrimStr( (decimal)(IsConfirmed), 4, 0));
         }
      }

      protected void ResetCaption070( )
      {
      }

      protected void E11072( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
      }

      protected void E12072( )
      {
         /* After Trn Routine */
         returnInSub = false;
         if ( ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) && ! AV11TrnContext.gxTpr_Callerondelete )
         {
            CallWebObject(formatLink("configuracionempresaww.aspx") );
            context.wjLocDisableFrm = 1;
         }
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
      }

      protected void ZM078( short GX_JID )
      {
         if ( ( GX_JID == 3 ) || ( GX_JID == 0 ) )
         {
            if ( ! IsIns( ) )
            {
               Z45ConfiguracionEmpresaTelefono = T00073_A45ConfiguracionEmpresaTelefono[0];
               Z46ConfiguracionEmpresaCostoPlanB = T00073_A46ConfiguracionEmpresaCostoPlanB[0];
               Z47ConfiguracionEmpresaCuotaPlanB = T00073_A47ConfiguracionEmpresaCuotaPlanB[0];
               Z48ConfiguracionEmpresaCostoPlanS = T00073_A48ConfiguracionEmpresaCostoPlanS[0];
               Z49ConfiguracionEmpresaCuotaPlanS = T00073_A49ConfiguracionEmpresaCuotaPlanS[0];
               Z50ConfiguracionEmpresaCostoPlanN = T00073_A50ConfiguracionEmpresaCostoPlanN[0];
               Z51ConfiguracionEmpresaCuotaPlanN = T00073_A51ConfiguracionEmpresaCuotaPlanN[0];
               Z54ConfiguracionEmpresaCostoLandi = T00073_A54ConfiguracionEmpresaCostoLandi[0];
               Z55ConfiguracionEmpresaCuotaLandi = T00073_A55ConfiguracionEmpresaCuotaLandi[0];
            }
            else
            {
               Z45ConfiguracionEmpresaTelefono = A45ConfiguracionEmpresaTelefono;
               Z46ConfiguracionEmpresaCostoPlanB = A46ConfiguracionEmpresaCostoPlanB;
               Z47ConfiguracionEmpresaCuotaPlanB = A47ConfiguracionEmpresaCuotaPlanB;
               Z48ConfiguracionEmpresaCostoPlanS = A48ConfiguracionEmpresaCostoPlanS;
               Z49ConfiguracionEmpresaCuotaPlanS = A49ConfiguracionEmpresaCuotaPlanS;
               Z50ConfiguracionEmpresaCostoPlanN = A50ConfiguracionEmpresaCostoPlanN;
               Z51ConfiguracionEmpresaCuotaPlanN = A51ConfiguracionEmpresaCuotaPlanN;
               Z54ConfiguracionEmpresaCostoLandi = A54ConfiguracionEmpresaCostoLandi;
               Z55ConfiguracionEmpresaCuotaLandi = A55ConfiguracionEmpresaCuotaLandi;
            }
         }
         if ( GX_JID == -3 )
         {
            Z44ConfiguracionEmpresaId = A44ConfiguracionEmpresaId;
            Z45ConfiguracionEmpresaTelefono = A45ConfiguracionEmpresaTelefono;
            Z46ConfiguracionEmpresaCostoPlanB = A46ConfiguracionEmpresaCostoPlanB;
            Z47ConfiguracionEmpresaCuotaPlanB = A47ConfiguracionEmpresaCuotaPlanB;
            Z48ConfiguracionEmpresaCostoPlanS = A48ConfiguracionEmpresaCostoPlanS;
            Z49ConfiguracionEmpresaCuotaPlanS = A49ConfiguracionEmpresaCuotaPlanS;
            Z50ConfiguracionEmpresaCostoPlanN = A50ConfiguracionEmpresaCostoPlanN;
            Z51ConfiguracionEmpresaCuotaPlanN = A51ConfiguracionEmpresaCuotaPlanN;
            Z54ConfiguracionEmpresaCostoLandi = A54ConfiguracionEmpresaCostoLandi;
            Z55ConfiguracionEmpresaCuotaLandi = A55ConfiguracionEmpresaCuotaLandi;
         }
      }

      protected void standaloneNotModal( )
      {
         edtConfiguracionEmpresaId_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaId_Enabled), 5, 0), true);
         Gx_BScreen = 0;
         AssignAttri("", false, "Gx_BScreen", StringUtil.Str( (decimal)(Gx_BScreen), 1, 0));
         edtConfiguracionEmpresaId_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaId_Enabled), 5, 0), true);
         bttBtntrn_delete_Enabled = 0;
         AssignProp("", false, bttBtntrn_delete_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(bttBtntrn_delete_Enabled), 5, 0), true);
      }

      protected void standaloneModal( )
      {
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
         if ( ! (0==AV7ConfiguracionEmpresaId) )
         {
            A44ConfiguracionEmpresaId = AV7ConfiguracionEmpresaId;
            AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
         }
         else
         {
            if ( IsIns( )  && (0==A44ConfiguracionEmpresaId) && ( Gx_BScreen == 0 ) )
            {
               A44ConfiguracionEmpresaId = 1;
               AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
            }
         }
      }

      protected void Load078( )
      {
         /* Using cursor T00074 */
         pr_default.execute(2, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(2) != 101) )
         {
            RcdFound8 = 1;
            A45ConfiguracionEmpresaTelefono = T00074_A45ConfiguracionEmpresaTelefono[0];
            AssignAttri("", false, "A45ConfiguracionEmpresaTelefono", A45ConfiguracionEmpresaTelefono);
            A46ConfiguracionEmpresaCostoPlanB = T00074_A46ConfiguracionEmpresaCostoPlanB[0];
            AssignAttri("", false, "A46ConfiguracionEmpresaCostoPlanB", StringUtil.LTrimStr( A46ConfiguracionEmpresaCostoPlanB, 12, 2));
            A47ConfiguracionEmpresaCuotaPlanB = T00074_A47ConfiguracionEmpresaCuotaPlanB[0];
            AssignAttri("", false, "A47ConfiguracionEmpresaCuotaPlanB", StringUtil.LTrimStr( A47ConfiguracionEmpresaCuotaPlanB, 12, 2));
            A48ConfiguracionEmpresaCostoPlanS = T00074_A48ConfiguracionEmpresaCostoPlanS[0];
            AssignAttri("", false, "A48ConfiguracionEmpresaCostoPlanS", StringUtil.LTrimStr( A48ConfiguracionEmpresaCostoPlanS, 12, 2));
            A49ConfiguracionEmpresaCuotaPlanS = T00074_A49ConfiguracionEmpresaCuotaPlanS[0];
            AssignAttri("", false, "A49ConfiguracionEmpresaCuotaPlanS", StringUtil.LTrimStr( A49ConfiguracionEmpresaCuotaPlanS, 12, 2));
            A50ConfiguracionEmpresaCostoPlanN = T00074_A50ConfiguracionEmpresaCostoPlanN[0];
            AssignAttri("", false, "A50ConfiguracionEmpresaCostoPlanN", StringUtil.LTrimStr( A50ConfiguracionEmpresaCostoPlanN, 12, 2));
            A51ConfiguracionEmpresaCuotaPlanN = T00074_A51ConfiguracionEmpresaCuotaPlanN[0];
            AssignAttri("", false, "A51ConfiguracionEmpresaCuotaPlanN", StringUtil.LTrimStr( A51ConfiguracionEmpresaCuotaPlanN, 12, 2));
            A54ConfiguracionEmpresaCostoLandi = T00074_A54ConfiguracionEmpresaCostoLandi[0];
            AssignAttri("", false, "A54ConfiguracionEmpresaCostoLandi", StringUtil.LTrimStr( A54ConfiguracionEmpresaCostoLandi, 12, 2));
            A55ConfiguracionEmpresaCuotaLandi = T00074_A55ConfiguracionEmpresaCuotaLandi[0];
            AssignAttri("", false, "A55ConfiguracionEmpresaCuotaLandi", StringUtil.LTrimStr( A55ConfiguracionEmpresaCuotaLandi, 12, 2));
            ZM078( -3) ;
         }
         pr_default.close(2);
         OnLoadActions078( ) ;
      }

      protected void OnLoadActions078( )
      {
      }

      protected void CheckExtendedTable078( )
      {
         Gx_BScreen = 1;
         AssignAttri("", false, "Gx_BScreen", StringUtil.Str( (decimal)(Gx_BScreen), 1, 0));
         standaloneModal( ) ;
      }

      protected void CloseExtendedTableCursors078( )
      {
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey078( )
      {
         /* Using cursor T00075 */
         pr_default.execute(3, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound8 = 1;
         }
         else
         {
            RcdFound8 = 0;
         }
         pr_default.close(3);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor T00073 */
         pr_default.execute(1, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM078( 3) ;
            RcdFound8 = 1;
            A44ConfiguracionEmpresaId = T00073_A44ConfiguracionEmpresaId[0];
            AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
            A45ConfiguracionEmpresaTelefono = T00073_A45ConfiguracionEmpresaTelefono[0];
            AssignAttri("", false, "A45ConfiguracionEmpresaTelefono", A45ConfiguracionEmpresaTelefono);
            A46ConfiguracionEmpresaCostoPlanB = T00073_A46ConfiguracionEmpresaCostoPlanB[0];
            AssignAttri("", false, "A46ConfiguracionEmpresaCostoPlanB", StringUtil.LTrimStr( A46ConfiguracionEmpresaCostoPlanB, 12, 2));
            A47ConfiguracionEmpresaCuotaPlanB = T00073_A47ConfiguracionEmpresaCuotaPlanB[0];
            AssignAttri("", false, "A47ConfiguracionEmpresaCuotaPlanB", StringUtil.LTrimStr( A47ConfiguracionEmpresaCuotaPlanB, 12, 2));
            A48ConfiguracionEmpresaCostoPlanS = T00073_A48ConfiguracionEmpresaCostoPlanS[0];
            AssignAttri("", false, "A48ConfiguracionEmpresaCostoPlanS", StringUtil.LTrimStr( A48ConfiguracionEmpresaCostoPlanS, 12, 2));
            A49ConfiguracionEmpresaCuotaPlanS = T00073_A49ConfiguracionEmpresaCuotaPlanS[0];
            AssignAttri("", false, "A49ConfiguracionEmpresaCuotaPlanS", StringUtil.LTrimStr( A49ConfiguracionEmpresaCuotaPlanS, 12, 2));
            A50ConfiguracionEmpresaCostoPlanN = T00073_A50ConfiguracionEmpresaCostoPlanN[0];
            AssignAttri("", false, "A50ConfiguracionEmpresaCostoPlanN", StringUtil.LTrimStr( A50ConfiguracionEmpresaCostoPlanN, 12, 2));
            A51ConfiguracionEmpresaCuotaPlanN = T00073_A51ConfiguracionEmpresaCuotaPlanN[0];
            AssignAttri("", false, "A51ConfiguracionEmpresaCuotaPlanN", StringUtil.LTrimStr( A51ConfiguracionEmpresaCuotaPlanN, 12, 2));
            A54ConfiguracionEmpresaCostoLandi = T00073_A54ConfiguracionEmpresaCostoLandi[0];
            AssignAttri("", false, "A54ConfiguracionEmpresaCostoLandi", StringUtil.LTrimStr( A54ConfiguracionEmpresaCostoLandi, 12, 2));
            A55ConfiguracionEmpresaCuotaLandi = T00073_A55ConfiguracionEmpresaCuotaLandi[0];
            AssignAttri("", false, "A55ConfiguracionEmpresaCuotaLandi", StringUtil.LTrimStr( A55ConfiguracionEmpresaCuotaLandi, 12, 2));
            Z44ConfiguracionEmpresaId = A44ConfiguracionEmpresaId;
            sMode8 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            Load078( ) ;
            if ( AnyError == 1 )
            {
               RcdFound8 = 0;
               InitializeNonKey078( ) ;
            }
            Gx_mode = sMode8;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         else
         {
            RcdFound8 = 0;
            InitializeNonKey078( ) ;
            sMode8 = Gx_mode;
            Gx_mode = "DSP";
            AssignAttri("", false, "Gx_mode", Gx_mode);
            standaloneModal( ) ;
            Gx_mode = sMode8;
            AssignAttri("", false, "Gx_mode", Gx_mode);
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey078( ) ;
         if ( RcdFound8 == 0 )
         {
         }
         else
         {
         }
         getByPrimaryKey( ) ;
      }

      protected void move_next( )
      {
         RcdFound8 = 0;
         /* Using cursor T00076 */
         pr_default.execute(4, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(4) != 101) )
         {
            while ( (pr_default.getStatus(4) != 101) && ( ( T00076_A44ConfiguracionEmpresaId[0] < A44ConfiguracionEmpresaId ) ) )
            {
               pr_default.readNext(4);
            }
            if ( (pr_default.getStatus(4) != 101) && ( ( T00076_A44ConfiguracionEmpresaId[0] > A44ConfiguracionEmpresaId ) ) )
            {
               A44ConfiguracionEmpresaId = T00076_A44ConfiguracionEmpresaId[0];
               AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
               RcdFound8 = 1;
            }
         }
         pr_default.close(4);
      }

      protected void move_previous( )
      {
         RcdFound8 = 0;
         /* Using cursor T00077 */
         pr_default.execute(5, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(5) != 101) )
         {
            while ( (pr_default.getStatus(5) != 101) && ( ( T00077_A44ConfiguracionEmpresaId[0] > A44ConfiguracionEmpresaId ) ) )
            {
               pr_default.readNext(5);
            }
            if ( (pr_default.getStatus(5) != 101) && ( ( T00077_A44ConfiguracionEmpresaId[0] < A44ConfiguracionEmpresaId ) ) )
            {
               A44ConfiguracionEmpresaId = T00077_A44ConfiguracionEmpresaId[0];
               AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
               RcdFound8 = 1;
            }
         }
         pr_default.close(5);
      }

      protected void btn_enter( )
      {
         nKeyPressed = 1;
         GetKey078( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            GX_FocusControl = edtConfiguracionEmpresaTelefono_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            Insert078( ) ;
            if ( AnyError == 1 )
            {
               GX_FocusControl = "";
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
         }
         else
         {
            if ( RcdFound8 == 1 )
            {
               if ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId )
               {
                  A44ConfiguracionEmpresaId = Z44ConfiguracionEmpresaId;
                  AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "CONFIGURACIONEMPRESAID");
                  AnyError = 1;
                  GX_FocusControl = edtConfiguracionEmpresaId_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else if ( IsDlt( ) )
               {
                  delete( ) ;
                  AfterTrn( ) ;
                  GX_FocusControl = edtConfiguracionEmpresaTelefono_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
               else
               {
                  /* Update record */
                  Update078( ) ;
                  GX_FocusControl = edtConfiguracionEmpresaTelefono_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
               }
            }
            else
            {
               if ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId )
               {
                  /* Insert record */
                  GX_FocusControl = edtConfiguracionEmpresaTelefono_Internalname;
                  AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  Insert078( ) ;
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
                     GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "CONFIGURACIONEMPRESAID");
                     AnyError = 1;
                     GX_FocusControl = edtConfiguracionEmpresaId_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                  }
                  else
                  {
                     /* Insert record */
                     GX_FocusControl = edtConfiguracionEmpresaTelefono_Internalname;
                     AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
                     Insert078( ) ;
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
         if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
         {
            if ( AnyError == 0 )
            {
               context.nUserReturn = 1;
            }
         }
      }

      protected void btn_delete( )
      {
         if ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId )
         {
            A44ConfiguracionEmpresaId = Z44ConfiguracionEmpresaId;
            AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
            GX_msglist.addItem(context.GetMessage( "GXM_getbeforedlt", ""), 1, "CONFIGURACIONEMPRESAID");
            AnyError = 1;
            GX_FocusControl = edtConfiguracionEmpresaId_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         else
         {
            delete( ) ;
            AfterTrn( ) ;
            GX_FocusControl = edtConfiguracionEmpresaTelefono_Internalname;
            AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
         }
         if ( AnyError != 0 )
         {
         }
      }

      protected void CheckOptimisticConcurrency078( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor T00072 */
            pr_default.execute(0, new Object[] {A44ConfiguracionEmpresaId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"ConfiguracionEmpresa"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z45ConfiguracionEmpresaTelefono, T00072_A45ConfiguracionEmpresaTelefono[0]) != 0 ) || ( Z46ConfiguracionEmpresaCostoPlanB != T00072_A46ConfiguracionEmpresaCostoPlanB[0] ) || ( Z47ConfiguracionEmpresaCuotaPlanB != T00072_A47ConfiguracionEmpresaCuotaPlanB[0] ) || ( Z48ConfiguracionEmpresaCostoPlanS != T00072_A48ConfiguracionEmpresaCostoPlanS[0] ) || ( Z49ConfiguracionEmpresaCuotaPlanS != T00072_A49ConfiguracionEmpresaCuotaPlanS[0] ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z50ConfiguracionEmpresaCostoPlanN != T00072_A50ConfiguracionEmpresaCostoPlanN[0] ) || ( Z51ConfiguracionEmpresaCuotaPlanN != T00072_A51ConfiguracionEmpresaCuotaPlanN[0] ) || ( Z54ConfiguracionEmpresaCostoLandi != T00072_A54ConfiguracionEmpresaCostoLandi[0] ) || ( Z55ConfiguracionEmpresaCuotaLandi != T00072_A55ConfiguracionEmpresaCuotaLandi[0] ) )
            {
               if ( StringUtil.StrCmp(Z45ConfiguracionEmpresaTelefono, T00072_A45ConfiguracionEmpresaTelefono[0]) != 0 )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaTelefono");
                  GXUtil.WriteLogRaw("Old: ",Z45ConfiguracionEmpresaTelefono);
                  GXUtil.WriteLogRaw("Current: ",T00072_A45ConfiguracionEmpresaTelefono[0]);
               }
               if ( Z46ConfiguracionEmpresaCostoPlanB != T00072_A46ConfiguracionEmpresaCostoPlanB[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCostoPlanB");
                  GXUtil.WriteLogRaw("Old: ",Z46ConfiguracionEmpresaCostoPlanB);
                  GXUtil.WriteLogRaw("Current: ",T00072_A46ConfiguracionEmpresaCostoPlanB[0]);
               }
               if ( Z47ConfiguracionEmpresaCuotaPlanB != T00072_A47ConfiguracionEmpresaCuotaPlanB[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCuotaPlanB");
                  GXUtil.WriteLogRaw("Old: ",Z47ConfiguracionEmpresaCuotaPlanB);
                  GXUtil.WriteLogRaw("Current: ",T00072_A47ConfiguracionEmpresaCuotaPlanB[0]);
               }
               if ( Z48ConfiguracionEmpresaCostoPlanS != T00072_A48ConfiguracionEmpresaCostoPlanS[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCostoPlanS");
                  GXUtil.WriteLogRaw("Old: ",Z48ConfiguracionEmpresaCostoPlanS);
                  GXUtil.WriteLogRaw("Current: ",T00072_A48ConfiguracionEmpresaCostoPlanS[0]);
               }
               if ( Z49ConfiguracionEmpresaCuotaPlanS != T00072_A49ConfiguracionEmpresaCuotaPlanS[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCuotaPlanS");
                  GXUtil.WriteLogRaw("Old: ",Z49ConfiguracionEmpresaCuotaPlanS);
                  GXUtil.WriteLogRaw("Current: ",T00072_A49ConfiguracionEmpresaCuotaPlanS[0]);
               }
               if ( Z50ConfiguracionEmpresaCostoPlanN != T00072_A50ConfiguracionEmpresaCostoPlanN[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCostoPlanN");
                  GXUtil.WriteLogRaw("Old: ",Z50ConfiguracionEmpresaCostoPlanN);
                  GXUtil.WriteLogRaw("Current: ",T00072_A50ConfiguracionEmpresaCostoPlanN[0]);
               }
               if ( Z51ConfiguracionEmpresaCuotaPlanN != T00072_A51ConfiguracionEmpresaCuotaPlanN[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCuotaPlanN");
                  GXUtil.WriteLogRaw("Old: ",Z51ConfiguracionEmpresaCuotaPlanN);
                  GXUtil.WriteLogRaw("Current: ",T00072_A51ConfiguracionEmpresaCuotaPlanN[0]);
               }
               if ( Z54ConfiguracionEmpresaCostoLandi != T00072_A54ConfiguracionEmpresaCostoLandi[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCostoLandi");
                  GXUtil.WriteLogRaw("Old: ",Z54ConfiguracionEmpresaCostoLandi);
                  GXUtil.WriteLogRaw("Current: ",T00072_A54ConfiguracionEmpresaCostoLandi[0]);
               }
               if ( Z55ConfiguracionEmpresaCuotaLandi != T00072_A55ConfiguracionEmpresaCuotaLandi[0] )
               {
                  GXUtil.WriteLog("configuracionempresa:[seudo value changed for attri]"+"ConfiguracionEmpresaCuotaLandi");
                  GXUtil.WriteLogRaw("Old: ",Z55ConfiguracionEmpresaCuotaLandi);
                  GXUtil.WriteLogRaw("Current: ",T00072_A55ConfiguracionEmpresaCuotaLandi[0]);
               }
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"ConfiguracionEmpresa"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert078( )
      {
         if ( ! IsAuthorized("configuracionempresa_Insert") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable078( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM078( 0) ;
            CheckOptimisticConcurrency078( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm078( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert078( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T00078 */
                     pr_default.execute(6, new Object[] {A45ConfiguracionEmpresaTelefono, A46ConfiguracionEmpresaCostoPlanB, A47ConfiguracionEmpresaCuotaPlanB, A48ConfiguracionEmpresaCostoPlanS, A49ConfiguracionEmpresaCuotaPlanS, A50ConfiguracionEmpresaCostoPlanN, A51ConfiguracionEmpresaCuotaPlanN, A54ConfiguracionEmpresaCostoLandi, A55ConfiguracionEmpresaCuotaLandi});
                     pr_default.close(6);
                     /* Retrieving last key number assigned */
                     /* Using cursor T00079 */
                     pr_default.execute(7);
                     A44ConfiguracionEmpresaId = T00079_A44ConfiguracionEmpresaId[0];
                     AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
                     pr_default.close(7);
                     pr_default.SmartCacheProvider.SetUpdated("ConfiguracionEmpresa");
                     if ( AnyError == 0 )
                     {
                        /* Start of After( Insert) rules */
                        /* End of After( Insert) rules */
                        if ( AnyError == 0 )
                        {
                           if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
                           {
                              if ( AnyError == 0 )
                              {
                                 context.nUserReturn = 1;
                              }
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
            else
            {
               Load078( ) ;
            }
            EndLevel078( ) ;
         }
         CloseExtendedTableCursors078( ) ;
      }

      protected void Update078( )
      {
         if ( ! IsAuthorized("configuracionempresa_Update") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable078( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency078( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm078( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate078( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor T000710 */
                     pr_default.execute(8, new Object[] {A45ConfiguracionEmpresaTelefono, A46ConfiguracionEmpresaCostoPlanB, A47ConfiguracionEmpresaCuotaPlanB, A48ConfiguracionEmpresaCostoPlanS, A49ConfiguracionEmpresaCuotaPlanS, A50ConfiguracionEmpresaCostoPlanN, A51ConfiguracionEmpresaCuotaPlanN, A54ConfiguracionEmpresaCostoLandi, A55ConfiguracionEmpresaCuotaLandi, A44ConfiguracionEmpresaId});
                     pr_default.close(8);
                     pr_default.SmartCacheProvider.SetUpdated("ConfiguracionEmpresa");
                     if ( (pr_default.getStatus(8) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"ConfiguracionEmpresa"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate078( ) ;
                     if ( AnyError == 0 )
                     {
                        /* Start of After( update) rules */
                        /* End of After( update) rules */
                        if ( AnyError == 0 )
                        {
                           if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
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
            EndLevel078( ) ;
         }
         CloseExtendedTableCursors078( ) ;
      }

      protected void DeferredUpdate078( )
      {
      }

      protected void delete( )
      {
         if ( ! IsAuthorized("configuracionempresa_Delete") )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_notauthorized", ""), 1, "");
            AnyError = 1;
            return  ;
         }
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency078( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls078( ) ;
            AfterConfirm078( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete078( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor T000711 */
                  pr_default.execute(9, new Object[] {A44ConfiguracionEmpresaId});
                  pr_default.close(9);
                  pr_default.SmartCacheProvider.SetUpdated("ConfiguracionEmpresa");
                  if ( AnyError == 0 )
                  {
                     /* Start of After( delete) rules */
                     /* End of After( delete) rules */
                     if ( AnyError == 0 )
                     {
                        if ( IsIns( ) || IsUpd( ) || IsDlt( ) )
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
         sMode8 = Gx_mode;
         Gx_mode = "DLT";
         AssignAttri("", false, "Gx_mode", Gx_mode);
         EndLevel078( ) ;
         Gx_mode = sMode8;
         AssignAttri("", false, "Gx_mode", Gx_mode);
      }

      protected void OnDeleteControls078( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel078( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete078( ) ;
         }
         if ( AnyError == 0 )
         {
            context.CommitDataStores("configuracionempresa",pr_default);
            if ( AnyError == 0 )
            {
               ConfirmValues070( ) ;
            }
            /* After transaction rules */
            /* Execute 'After Trn' event if defined. */
            trnEnded = 1;
         }
         else
         {
            context.RollbackDataStores("configuracionempresa",pr_default);
         }
         IsModified = 0;
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanStart078( )
      {
         /* Scan By routine */
         /* Using cursor T000712 */
         pr_default.execute(10);
         RcdFound8 = 0;
         if ( (pr_default.getStatus(10) != 101) )
         {
            RcdFound8 = 1;
            A44ConfiguracionEmpresaId = T000712_A44ConfiguracionEmpresaId[0];
            AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
         }
         /* Load Subordinate Levels */
      }

      protected void ScanNext078( )
      {
         /* Scan next routine */
         pr_default.readNext(10);
         RcdFound8 = 0;
         if ( (pr_default.getStatus(10) != 101) )
         {
            RcdFound8 = 1;
            A44ConfiguracionEmpresaId = T000712_A44ConfiguracionEmpresaId[0];
            AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
         }
      }

      protected void ScanEnd078( )
      {
         pr_default.close(10);
      }

      protected void AfterConfirm078( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert078( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate078( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete078( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete078( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate078( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes078( )
      {
         edtConfiguracionEmpresaId_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaId_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaId_Enabled), 5, 0), true);
         edtConfiguracionEmpresaTelefono_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaTelefono_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaTelefono_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCostoPlanB_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCostoPlanB_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoPlanB_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCuotaPlanB_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCuotaPlanB_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaPlanB_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCostoPlanS_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCostoPlanS_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoPlanS_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCuotaPlanS_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCuotaPlanS_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaPlanS_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCostoPlanN_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCostoPlanN_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoPlanN_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCuotaPlanN_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCuotaPlanN_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaPlanN_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCostoLandi_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCostoLandi_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCostoLandi_Enabled), 5, 0), true);
         edtConfiguracionEmpresaCuotaLandi_Enabled = 0;
         AssignProp("", false, edtConfiguracionEmpresaCuotaLandi_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtConfiguracionEmpresaCuotaLandi_Enabled), 5, 0), true);
      }

      protected void send_integrity_lvl_hashes078( )
      {
      }

      protected void assign_properties_default( )
      {
      }

      protected void ConfirmValues070( )
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
         GXEncryptionTmp = "configuracionempresa.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV7ConfiguracionEmpresaId,4,0));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("configuracionempresa.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         forbiddenHiddens.Add("hshsalt", "hsh"+"ConfiguracionEmpresa");
         forbiddenHiddens.Add("ConfiguracionEmpresaId", context.localUtil.Format( (decimal)(A44ConfiguracionEmpresaId), "ZZZ9"));
         forbiddenHiddens.Add("Gx_mode", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")));
         GxWebStd.gx_hidden_field( context, "hsh", GetEncryptedHash( forbiddenHiddens.ToString(), GXKey));
         GXUtil.WriteLogInfo("configuracionempresa:[ SendSecurityCheck value for]"+forbiddenHiddens.ToJSonString());
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "Z44ConfiguracionEmpresaId", StringUtil.LTrim( StringUtil.NToC( (decimal)(Z44ConfiguracionEmpresaId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z45ConfiguracionEmpresaTelefono", StringUtil.RTrim( Z45ConfiguracionEmpresaTelefono));
         GxWebStd.gx_hidden_field( context, "Z46ConfiguracionEmpresaCostoPlanB", StringUtil.LTrim( StringUtil.NToC( Z46ConfiguracionEmpresaCostoPlanB, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z47ConfiguracionEmpresaCuotaPlanB", StringUtil.LTrim( StringUtil.NToC( Z47ConfiguracionEmpresaCuotaPlanB, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z48ConfiguracionEmpresaCostoPlanS", StringUtil.LTrim( StringUtil.NToC( Z48ConfiguracionEmpresaCostoPlanS, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z49ConfiguracionEmpresaCuotaPlanS", StringUtil.LTrim( StringUtil.NToC( Z49ConfiguracionEmpresaCuotaPlanS, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z50ConfiguracionEmpresaCostoPlanN", StringUtil.LTrim( StringUtil.NToC( Z50ConfiguracionEmpresaCostoPlanN, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z51ConfiguracionEmpresaCuotaPlanN", StringUtil.LTrim( StringUtil.NToC( Z51ConfiguracionEmpresaCuotaPlanN, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z54ConfiguracionEmpresaCostoLandi", StringUtil.LTrim( StringUtil.NToC( Z54ConfiguracionEmpresaCostoLandi, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Z55ConfiguracionEmpresaCuotaLandi", StringUtil.LTrim( StringUtil.NToC( Z55ConfiguracionEmpresaCuotaLandi, 12, 2, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsConfirmed", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsConfirmed), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "IsModified", StringUtil.LTrim( StringUtil.NToC( (decimal)(IsModified), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "Mode", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_Mode", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
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
         GxWebStd.gx_hidden_field( context, "vCONFIGURACIONEMPRESAID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV7ConfiguracionEmpresaId), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vCONFIGURACIONEMPRESAID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV7ConfiguracionEmpresaId), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vGXBSCREEN", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gx_BScreen), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
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
         GXEncryptionTmp = "configuracionempresa.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.LTrimStr(AV7ConfiguracionEmpresaId,4,0));
         return formatLink("configuracionempresa.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "ConfiguracionEmpresa" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Configuracion Empresa", "") ;
      }

      protected void InitializeNonKey078( )
      {
         A45ConfiguracionEmpresaTelefono = "";
         AssignAttri("", false, "A45ConfiguracionEmpresaTelefono", A45ConfiguracionEmpresaTelefono);
         A46ConfiguracionEmpresaCostoPlanB = 0;
         AssignAttri("", false, "A46ConfiguracionEmpresaCostoPlanB", StringUtil.LTrimStr( A46ConfiguracionEmpresaCostoPlanB, 12, 2));
         A47ConfiguracionEmpresaCuotaPlanB = 0;
         AssignAttri("", false, "A47ConfiguracionEmpresaCuotaPlanB", StringUtil.LTrimStr( A47ConfiguracionEmpresaCuotaPlanB, 12, 2));
         A48ConfiguracionEmpresaCostoPlanS = 0;
         AssignAttri("", false, "A48ConfiguracionEmpresaCostoPlanS", StringUtil.LTrimStr( A48ConfiguracionEmpresaCostoPlanS, 12, 2));
         A49ConfiguracionEmpresaCuotaPlanS = 0;
         AssignAttri("", false, "A49ConfiguracionEmpresaCuotaPlanS", StringUtil.LTrimStr( A49ConfiguracionEmpresaCuotaPlanS, 12, 2));
         A50ConfiguracionEmpresaCostoPlanN = 0;
         AssignAttri("", false, "A50ConfiguracionEmpresaCostoPlanN", StringUtil.LTrimStr( A50ConfiguracionEmpresaCostoPlanN, 12, 2));
         A51ConfiguracionEmpresaCuotaPlanN = 0;
         AssignAttri("", false, "A51ConfiguracionEmpresaCuotaPlanN", StringUtil.LTrimStr( A51ConfiguracionEmpresaCuotaPlanN, 12, 2));
         A54ConfiguracionEmpresaCostoLandi = 0;
         AssignAttri("", false, "A54ConfiguracionEmpresaCostoLandi", StringUtil.LTrimStr( A54ConfiguracionEmpresaCostoLandi, 12, 2));
         A55ConfiguracionEmpresaCuotaLandi = 0;
         AssignAttri("", false, "A55ConfiguracionEmpresaCuotaLandi", StringUtil.LTrimStr( A55ConfiguracionEmpresaCuotaLandi, 12, 2));
         Z45ConfiguracionEmpresaTelefono = "";
         Z46ConfiguracionEmpresaCostoPlanB = 0;
         Z47ConfiguracionEmpresaCuotaPlanB = 0;
         Z48ConfiguracionEmpresaCostoPlanS = 0;
         Z49ConfiguracionEmpresaCuotaPlanS = 0;
         Z50ConfiguracionEmpresaCostoPlanN = 0;
         Z51ConfiguracionEmpresaCuotaPlanN = 0;
         Z54ConfiguracionEmpresaCostoLandi = 0;
         Z55ConfiguracionEmpresaCuotaLandi = 0;
      }

      protected void InitAll078( )
      {
         A44ConfiguracionEmpresaId = 1;
         AssignAttri("", false, "A44ConfiguracionEmpresaId", StringUtil.LTrimStr( (decimal)(A44ConfiguracionEmpresaId), 4, 0));
         InitializeNonKey078( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623402878", true, true);
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
         context.AddJavascriptSource("configuracionempresa.js", "?2024121623402878", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Panel/BootstrapPanelRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_default_properties( )
      {
         edtConfiguracionEmpresaId_Internalname = "CONFIGURACIONEMPRESAID";
         edtConfiguracionEmpresaTelefono_Internalname = "CONFIGURACIONEMPRESATELEFONO";
         edtConfiguracionEmpresaCostoPlanB_Internalname = "CONFIGURACIONEMPRESACOSTOPLANB";
         edtConfiguracionEmpresaCuotaPlanB_Internalname = "CONFIGURACIONEMPRESACUOTAPLANB";
         edtConfiguracionEmpresaCostoPlanS_Internalname = "CONFIGURACIONEMPRESACOSTOPLANS";
         edtConfiguracionEmpresaCuotaPlanS_Internalname = "CONFIGURACIONEMPRESACUOTAPLANS";
         edtConfiguracionEmpresaCostoPlanN_Internalname = "CONFIGURACIONEMPRESACOSTOPLANN";
         edtConfiguracionEmpresaCuotaPlanN_Internalname = "CONFIGURACIONEMPRESACUOTAPLANN";
         edtConfiguracionEmpresaCostoLandi_Internalname = "CONFIGURACIONEMPRESACOSTOLANDI";
         edtConfiguracionEmpresaCuotaLandi_Internalname = "CONFIGURACIONEMPRESACUOTALANDI";
         divTableattributes_Internalname = "TABLEATTRIBUTES";
         Dvpanel_tableattributes_Internalname = "DVPANEL_TABLEATTRIBUTES";
         divTablecontent_Internalname = "TABLECONTENT";
         bttBtntrn_enter_Internalname = "BTNTRN_ENTER";
         bttBtntrn_cancel_Internalname = "BTNTRN_CANCEL";
         bttBtntrn_delete_Internalname = "BTNTRN_DELETE";
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
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Configuracion Empresa", "");
         bttBtntrn_delete_Enabled = 0;
         bttBtntrn_delete_Visible = 1;
         bttBtntrn_cancel_Visible = 1;
         bttBtntrn_enter_Enabled = 1;
         bttBtntrn_enter_Visible = 1;
         edtConfiguracionEmpresaCuotaLandi_Jsonclick = "";
         edtConfiguracionEmpresaCuotaLandi_Enabled = 1;
         edtConfiguracionEmpresaCostoLandi_Jsonclick = "";
         edtConfiguracionEmpresaCostoLandi_Enabled = 1;
         edtConfiguracionEmpresaCuotaPlanN_Jsonclick = "";
         edtConfiguracionEmpresaCuotaPlanN_Enabled = 1;
         edtConfiguracionEmpresaCostoPlanN_Jsonclick = "";
         edtConfiguracionEmpresaCostoPlanN_Enabled = 1;
         edtConfiguracionEmpresaCuotaPlanS_Jsonclick = "";
         edtConfiguracionEmpresaCuotaPlanS_Enabled = 1;
         edtConfiguracionEmpresaCostoPlanS_Jsonclick = "";
         edtConfiguracionEmpresaCostoPlanS_Enabled = 1;
         edtConfiguracionEmpresaCuotaPlanB_Jsonclick = "";
         edtConfiguracionEmpresaCuotaPlanB_Enabled = 1;
         edtConfiguracionEmpresaCostoPlanB_Jsonclick = "";
         edtConfiguracionEmpresaCostoPlanB_Enabled = 1;
         edtConfiguracionEmpresaTelefono_Jsonclick = "";
         edtConfiguracionEmpresaTelefono_Enabled = 1;
         edtConfiguracionEmpresaId_Jsonclick = "";
         edtConfiguracionEmpresaId_Enabled = 0;
         Dvpanel_tableattributes_Autoscroll = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Iconposition = "Right";
         Dvpanel_tableattributes_Showcollapseicon = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Collapsed = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Collapsible = Convert.ToBoolean( 0);
         Dvpanel_tableattributes_Title = context.GetMessage( "WWP_TemplateDataPanelTitle", "");
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
         setEventMetadata("ENTER","""{"handler":"UserMainFullajax","iparms":[{"postForm":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV7ConfiguracionEmpresaId","fld":"vCONFIGURACIONEMPRESAID","pic":"ZZZ9","hsh":true}]}""");
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true},{"av":"AV7ConfiguracionEmpresaId","fld":"vCONFIGURACIONEMPRESAID","pic":"ZZZ9","hsh":true},{"av":"A44ConfiguracionEmpresaId","fld":"CONFIGURACIONEMPRESAID","pic":"ZZZ9"}]}""");
         setEventMetadata("AFTER TRN","""{"handler":"E12072","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV11TrnContext","fld":"vTRNCONTEXT","hsh":true}]}""");
         setEventMetadata("VALID_CONFIGURACIONEMPRESAID","""{"handler":"Valid_Configuracionempresaid","iparms":[]}""");
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
      }

      public override void initialize( )
      {
         sPrefix = "";
         wcpOGx_mode = "";
         Z45ConfiguracionEmpresaTelefono = "";
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
         TempTags = "";
         gxphoneLink = "";
         A45ConfiguracionEmpresaTelefono = "";
         bttBtntrn_enter_Jsonclick = "";
         bttBtntrn_cancel_Jsonclick = "";
         bttBtntrn_delete_Jsonclick = "";
         Dvpanel_tableattributes_Objectcall = "";
         Dvpanel_tableattributes_Class = "";
         Dvpanel_tableattributes_Height = "";
         forbiddenHiddens = new GXProperties();
         hsh = "";
         sMode8 = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         AV8WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV11TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV12WebSession = context.GetSession();
         T00074_A44ConfiguracionEmpresaId = new short[1] ;
         T00074_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         T00074_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         T00074_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         T00074_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         T00074_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         T00074_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         T00074_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         T00074_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         T00074_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         T00075_A44ConfiguracionEmpresaId = new short[1] ;
         T00073_A44ConfiguracionEmpresaId = new short[1] ;
         T00073_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         T00073_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         T00073_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         T00073_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         T00073_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         T00073_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         T00073_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         T00073_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         T00073_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         T00076_A44ConfiguracionEmpresaId = new short[1] ;
         T00077_A44ConfiguracionEmpresaId = new short[1] ;
         T00072_A44ConfiguracionEmpresaId = new short[1] ;
         T00072_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         T00072_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         T00072_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         T00072_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         T00072_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         T00072_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         T00072_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         T00072_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         T00072_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         T00079_A44ConfiguracionEmpresaId = new short[1] ;
         T000712_A44ConfiguracionEmpresaId = new short[1] ;
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXEncryptionTmp = "";
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresa__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresa__default(),
            new Object[][] {
                new Object[] {
               T00072_A44ConfiguracionEmpresaId, T00072_A45ConfiguracionEmpresaTelefono, T00072_A46ConfiguracionEmpresaCostoPlanB, T00072_A47ConfiguracionEmpresaCuotaPlanB, T00072_A48ConfiguracionEmpresaCostoPlanS, T00072_A49ConfiguracionEmpresaCuotaPlanS, T00072_A50ConfiguracionEmpresaCostoPlanN, T00072_A51ConfiguracionEmpresaCuotaPlanN, T00072_A54ConfiguracionEmpresaCostoLandi, T00072_A55ConfiguracionEmpresaCuotaLandi
               }
               , new Object[] {
               T00073_A44ConfiguracionEmpresaId, T00073_A45ConfiguracionEmpresaTelefono, T00073_A46ConfiguracionEmpresaCostoPlanB, T00073_A47ConfiguracionEmpresaCuotaPlanB, T00073_A48ConfiguracionEmpresaCostoPlanS, T00073_A49ConfiguracionEmpresaCuotaPlanS, T00073_A50ConfiguracionEmpresaCostoPlanN, T00073_A51ConfiguracionEmpresaCuotaPlanN, T00073_A54ConfiguracionEmpresaCostoLandi, T00073_A55ConfiguracionEmpresaCuotaLandi
               }
               , new Object[] {
               T00074_A44ConfiguracionEmpresaId, T00074_A45ConfiguracionEmpresaTelefono, T00074_A46ConfiguracionEmpresaCostoPlanB, T00074_A47ConfiguracionEmpresaCuotaPlanB, T00074_A48ConfiguracionEmpresaCostoPlanS, T00074_A49ConfiguracionEmpresaCuotaPlanS, T00074_A50ConfiguracionEmpresaCostoPlanN, T00074_A51ConfiguracionEmpresaCuotaPlanN, T00074_A54ConfiguracionEmpresaCostoLandi, T00074_A55ConfiguracionEmpresaCuotaLandi
               }
               , new Object[] {
               T00075_A44ConfiguracionEmpresaId
               }
               , new Object[] {
               T00076_A44ConfiguracionEmpresaId
               }
               , new Object[] {
               T00077_A44ConfiguracionEmpresaId
               }
               , new Object[] {
               }
               , new Object[] {
               T00079_A44ConfiguracionEmpresaId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               T000712_A44ConfiguracionEmpresaId
               }
            }
         );
         Z44ConfiguracionEmpresaId = 1;
         A44ConfiguracionEmpresaId = 1;
      }

      private short wcpOAV7ConfiguracionEmpresaId ;
      private short Z44ConfiguracionEmpresaId ;
      private short GxWebError ;
      private short AV7ConfiguracionEmpresaId ;
      private short AnyError ;
      private short IsModified ;
      private short IsConfirmed ;
      private short nKeyPressed ;
      private short A44ConfiguracionEmpresaId ;
      private short Gx_BScreen ;
      private short RcdFound8 ;
      private short gxajaxcallmode ;
      private int trnEnded ;
      private int edtConfiguracionEmpresaId_Enabled ;
      private int edtConfiguracionEmpresaTelefono_Enabled ;
      private int edtConfiguracionEmpresaCostoPlanB_Enabled ;
      private int edtConfiguracionEmpresaCuotaPlanB_Enabled ;
      private int edtConfiguracionEmpresaCostoPlanS_Enabled ;
      private int edtConfiguracionEmpresaCuotaPlanS_Enabled ;
      private int edtConfiguracionEmpresaCostoPlanN_Enabled ;
      private int edtConfiguracionEmpresaCuotaPlanN_Enabled ;
      private int edtConfiguracionEmpresaCostoLandi_Enabled ;
      private int edtConfiguracionEmpresaCuotaLandi_Enabled ;
      private int bttBtntrn_enter_Visible ;
      private int bttBtntrn_enter_Enabled ;
      private int bttBtntrn_cancel_Visible ;
      private int bttBtntrn_delete_Visible ;
      private int bttBtntrn_delete_Enabled ;
      private int Dvpanel_tableattributes_Gxcontroltype ;
      private int idxLst ;
      private decimal Z46ConfiguracionEmpresaCostoPlanB ;
      private decimal Z47ConfiguracionEmpresaCuotaPlanB ;
      private decimal Z48ConfiguracionEmpresaCostoPlanS ;
      private decimal Z49ConfiguracionEmpresaCuotaPlanS ;
      private decimal Z50ConfiguracionEmpresaCostoPlanN ;
      private decimal Z51ConfiguracionEmpresaCuotaPlanN ;
      private decimal Z54ConfiguracionEmpresaCostoLandi ;
      private decimal Z55ConfiguracionEmpresaCuotaLandi ;
      private decimal A46ConfiguracionEmpresaCostoPlanB ;
      private decimal A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal A48ConfiguracionEmpresaCostoPlanS ;
      private decimal A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal A50ConfiguracionEmpresaCostoPlanN ;
      private decimal A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal A54ConfiguracionEmpresaCostoLandi ;
      private decimal A55ConfiguracionEmpresaCuotaLandi ;
      private string sPrefix ;
      private string wcpOGx_mode ;
      private string Z45ConfiguracionEmpresaTelefono ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string GXKey ;
      private string GXDecQS ;
      private string Gx_mode ;
      private string PreviousTooltip ;
      private string PreviousCaption ;
      private string GX_FocusControl ;
      private string edtConfiguracionEmpresaTelefono_Internalname ;
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
      private string edtConfiguracionEmpresaId_Internalname ;
      private string TempTags ;
      private string edtConfiguracionEmpresaId_Jsonclick ;
      private string gxphoneLink ;
      private string A45ConfiguracionEmpresaTelefono ;
      private string edtConfiguracionEmpresaTelefono_Jsonclick ;
      private string edtConfiguracionEmpresaCostoPlanB_Internalname ;
      private string edtConfiguracionEmpresaCostoPlanB_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaPlanB_Internalname ;
      private string edtConfiguracionEmpresaCuotaPlanB_Jsonclick ;
      private string edtConfiguracionEmpresaCostoPlanS_Internalname ;
      private string edtConfiguracionEmpresaCostoPlanS_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaPlanS_Internalname ;
      private string edtConfiguracionEmpresaCuotaPlanS_Jsonclick ;
      private string edtConfiguracionEmpresaCostoPlanN_Internalname ;
      private string edtConfiguracionEmpresaCostoPlanN_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaPlanN_Internalname ;
      private string edtConfiguracionEmpresaCuotaPlanN_Jsonclick ;
      private string edtConfiguracionEmpresaCostoLandi_Internalname ;
      private string edtConfiguracionEmpresaCostoLandi_Jsonclick ;
      private string edtConfiguracionEmpresaCuotaLandi_Internalname ;
      private string edtConfiguracionEmpresaCuotaLandi_Jsonclick ;
      private string bttBtntrn_enter_Internalname ;
      private string bttBtntrn_enter_Jsonclick ;
      private string bttBtntrn_cancel_Internalname ;
      private string bttBtntrn_cancel_Jsonclick ;
      private string bttBtntrn_delete_Internalname ;
      private string bttBtntrn_delete_Jsonclick ;
      private string Dvpanel_tableattributes_Objectcall ;
      private string Dvpanel_tableattributes_Class ;
      private string Dvpanel_tableattributes_Height ;
      private string hsh ;
      private string sMode8 ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXEncryptionTmp ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbErr ;
      private bool Dvpanel_tableattributes_Autowidth ;
      private bool Dvpanel_tableattributes_Autoheight ;
      private bool Dvpanel_tableattributes_Collapsible ;
      private bool Dvpanel_tableattributes_Collapsed ;
      private bool Dvpanel_tableattributes_Showcollapseicon ;
      private bool Dvpanel_tableattributes_Autoscroll ;
      private bool Dvpanel_tableattributes_Enabled ;
      private bool Dvpanel_tableattributes_Showheader ;
      private bool Dvpanel_tableattributes_Visible ;
      private bool returnInSub ;
      private bool Gx_longc ;
      private IGxSession AV12WebSession ;
      private GXProperties forbiddenHiddens ;
      private GXUserControl ucDvpanel_tableattributes ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV11TrnContext ;
      private IDataStoreProvider pr_default ;
      private short[] T00074_A44ConfiguracionEmpresaId ;
      private string[] T00074_A45ConfiguracionEmpresaTelefono ;
      private decimal[] T00074_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] T00074_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] T00074_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] T00074_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] T00074_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] T00074_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] T00074_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] T00074_A55ConfiguracionEmpresaCuotaLandi ;
      private short[] T00075_A44ConfiguracionEmpresaId ;
      private short[] T00073_A44ConfiguracionEmpresaId ;
      private string[] T00073_A45ConfiguracionEmpresaTelefono ;
      private decimal[] T00073_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] T00073_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] T00073_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] T00073_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] T00073_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] T00073_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] T00073_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] T00073_A55ConfiguracionEmpresaCuotaLandi ;
      private short[] T00076_A44ConfiguracionEmpresaId ;
      private short[] T00077_A44ConfiguracionEmpresaId ;
      private short[] T00072_A44ConfiguracionEmpresaId ;
      private string[] T00072_A45ConfiguracionEmpresaTelefono ;
      private decimal[] T00072_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] T00072_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] T00072_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] T00072_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] T00072_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] T00072_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] T00072_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] T00072_A55ConfiguracionEmpresaCuotaLandi ;
      private short[] T00079_A44ConfiguracionEmpresaId ;
      private short[] T000712_A44ConfiguracionEmpresaId ;
      private IDataStoreProvider pr_gam ;
   }

   public class configuracionempresa__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class configuracionempresa__default : DataStoreHelperBase, IDataStoreHelper
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
       ,new UpdateCursor(def[6])
       ,new ForEachCursor(def[7])
       ,new UpdateCursor(def[8])
       ,new UpdateCursor(def[9])
       ,new ForEachCursor(def[10])
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        Object[] prmT00072;
        prmT00072 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT00073;
        prmT00073 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT00074;
        prmT00074 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT00075;
        prmT00075 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT00076;
        prmT00076 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT00077;
        prmT00077 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT00078;
        prmT00078 = new Object[] {
        new ParDef("@ConfiguracionEmpresaTelefono",GXType.Char,20,0) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoLandi",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaLandi",GXType.Number,12,2)
        };
        Object[] prmT00079;
        prmT00079 = new Object[] {
        };
        Object[] prmT000710;
        prmT000710 = new Object[] {
        new ParDef("@ConfiguracionEmpresaTelefono",GXType.Char,20,0) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoLandi",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaLandi",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT000711;
        prmT000711 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmT000712;
        prmT000712 = new Object[] {
        };
        def= new CursorDef[] {
            new CursorDef("T00072", "SELECT `ConfiguracionEmpresaId`, `ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId  FOR UPDATE ",true, GxErrorMask.GX_NOMASK, false, this,prmT00072,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00073", "SELECT `ConfiguracionEmpresaId`, `ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00073,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00074", "SELECT TM1.`ConfiguracionEmpresaId`, TM1.`ConfiguracionEmpresaTelefono`, TM1.`ConfiguracionEmpresaCostoPlanB`, TM1.`ConfiguracionEmpresaCuotaPlanB`, TM1.`ConfiguracionEmpresaCostoPlanS`, TM1.`ConfiguracionEmpresaCuotaPlanS`, TM1.`ConfiguracionEmpresaCostoPlanN`, TM1.`ConfiguracionEmpresaCuotaPlanN`, TM1.`ConfiguracionEmpresaCostoLandi`, TM1.`ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` TM1 WHERE TM1.`ConfiguracionEmpresaId` = @ConfiguracionEmpresaId ORDER BY TM1.`ConfiguracionEmpresaId` ",true, GxErrorMask.GX_NOMASK, false, this,prmT00074,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00075", "SELECT `ConfiguracionEmpresaId` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId ",true, GxErrorMask.GX_NOMASK, false, this,prmT00075,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T00076", "SELECT `ConfiguracionEmpresaId` FROM `ConfiguracionEmpresa` WHERE ( `ConfiguracionEmpresaId` > @ConfiguracionEmpresaId) ORDER BY `ConfiguracionEmpresaId`  LIMIT 1",true, GxErrorMask.GX_NOMASK, false, this,prmT00076,1, GxCacheFrequency.OFF ,true,true )
           ,new CursorDef("T00077", "SELECT `ConfiguracionEmpresaId` FROM `ConfiguracionEmpresa` WHERE ( `ConfiguracionEmpresaId` < @ConfiguracionEmpresaId) ORDER BY `ConfiguracionEmpresaId` DESC  LIMIT 1",true, GxErrorMask.GX_NOMASK, false, this,prmT00077,1, GxCacheFrequency.OFF ,true,true )
           ,new CursorDef("T00078", "INSERT INTO `ConfiguracionEmpresa`(`ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaLandi`) VALUES(@ConfiguracionEmpresaTelefono, @ConfiguracionEmpresaCostoPlanB, @ConfiguracionEmpresaCuotaPlanB, @ConfiguracionEmpresaCostoPlanS, @ConfiguracionEmpresaCuotaPlanS, @ConfiguracionEmpresaCostoPlanN, @ConfiguracionEmpresaCuotaPlanN, @ConfiguracionEmpresaCostoLandi, @ConfiguracionEmpresaCuotaLandi)", GxErrorMask.GX_NOMASK,prmT00078)
           ,new CursorDef("T00079", "SELECT LAST_INSERT_ID() ",true, GxErrorMask.GX_NOMASK, false, this,prmT00079,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("T000710", "UPDATE `ConfiguracionEmpresa` SET `ConfiguracionEmpresaTelefono`=@ConfiguracionEmpresaTelefono, `ConfiguracionEmpresaCostoPlanB`=@ConfiguracionEmpresaCostoPlanB, `ConfiguracionEmpresaCuotaPlanB`=@ConfiguracionEmpresaCuotaPlanB, `ConfiguracionEmpresaCostoPlanS`=@ConfiguracionEmpresaCostoPlanS, `ConfiguracionEmpresaCuotaPlanS`=@ConfiguracionEmpresaCuotaPlanS, `ConfiguracionEmpresaCostoPlanN`=@ConfiguracionEmpresaCostoPlanN, `ConfiguracionEmpresaCuotaPlanN`=@ConfiguracionEmpresaCuotaPlanN, `ConfiguracionEmpresaCostoLandi`=@ConfiguracionEmpresaCostoLandi, `ConfiguracionEmpresaCuotaLandi`=@ConfiguracionEmpresaCuotaLandi  WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId", GxErrorMask.GX_NOMASK,prmT000710)
           ,new CursorDef("T000711", "DELETE FROM `ConfiguracionEmpresa`  WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId", GxErrorMask.GX_NOMASK,prmT000711)
           ,new CursorDef("T000712", "SELECT `ConfiguracionEmpresaId` FROM `ConfiguracionEmpresa` ORDER BY `ConfiguracionEmpresaId` ",true, GxErrorMask.GX_NOMASK, false, this,prmT000712,100, GxCacheFrequency.OFF ,true,false )
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
              ((string[]) buf[1])[0] = rslt.getString(2, 20);
              ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
              ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
              ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
              ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
              ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
              ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
              ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
              ((decimal[]) buf[9])[0] = rslt.getDecimal(10);
              return;
           case 1 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getString(2, 20);
              ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
              ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
              ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
              ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
              ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
              ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
              ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
              ((decimal[]) buf[9])[0] = rslt.getDecimal(10);
              return;
           case 2 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getString(2, 20);
              ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
              ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
              ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
              ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
              ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
              ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
              ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
              ((decimal[]) buf[9])[0] = rslt.getDecimal(10);
              return;
           case 3 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 4 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 5 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 7 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 10 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
     }
  }

}

}
