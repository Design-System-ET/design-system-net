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
   public class dslogin : GXDataArea
   {
      public dslogin( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public dslogin( IGxContext context )
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
         cmbavLogonto = new GXCombobox();
         chkavKeepmeloggedin = new GXCheckbox();
         chkavRememberme = new GXCheckbox();
         cmbavTypeauthtype = new GXCombobox();
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridauthtypes") == 0 )
            {
               gxnrGridauthtypes_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridauthtypes") == 0 )
            {
               gxgrGridauthtypes_refresh_invoke( ) ;
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

      protected void gxnrGridauthtypes_newrow_invoke( )
      {
         nRC_GXsfl_62 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_62"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_62_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_62_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_62_idx = GetPar( "sGXsfl_62_idx");
         cmbavTypeauthtype.Visible = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, cmbavTypeauthtype_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTypeauthtype.Visible), 5, 0), !bGXsfl_62_Refreshing);
         edtavNameauthtype_Visible = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, edtavNameauthtype_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavNameauthtype_Visible), 5, 0), !bGXsfl_62_Refreshing);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridauthtypes_newrow( ) ;
         /* End function gxnrGridauthtypes_newrow_invoke */
      }

      protected void gxgrGridauthtypes_refresh_invoke( )
      {
         AV24Language = GetPar( "Language");
         AV8AuxUserName = GetPar( "AuxUserName");
         AV33UserRememberMe = (short)(Math.Round(NumberUtil.Val( GetPar( "UserRememberMe"), "."), 18, MidpointRounding.ToEven));
         cmbavTypeauthtype.Visible = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, cmbavTypeauthtype_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTypeauthtype.Visible), 5, 0), !bGXsfl_62_Refreshing);
         edtavNameauthtype_Visible = (int)(Math.Round(NumberUtil.Val( GetNextPar( ), "."), 18, MidpointRounding.ToEven));
         AssignProp("", false, edtavNameauthtype_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavNameauthtype_Visible), 5, 0), !bGXsfl_62_Refreshing);
         AV23KeepMeLoggedIn = StringUtil.StrToBool( GetPar( "KeepMeLoggedIn"));
         AV28RememberMe = StringUtil.StrToBool( GetPar( "RememberMe"));
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridauthtypes_refresh( AV24Language, AV8AuxUserName, AV33UserRememberMe, AV23KeepMeLoggedIn, AV28RememberMe) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridauthtypes_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("wwpbaseobjects.workwithplusmasterpageempty", "DesignSystem.Programs.wwpbaseobjects.workwithplusmasterpageempty", new Object[] {context});
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
         PA032( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START032( ) ;
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
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Mask/jquery.mask.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/BootstrapSelect.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/WorkWithPlusUtilitiesRender.js", "", false, true);
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
         context.WriteHtmlText( " "+"class=\"form-horizontal LoginBackgroundImage\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal LoginBackgroundImage\" data-gx-class=\"form-horizontal LoginBackgroundImage\" novalidate action=\""+formatLink("dslogin.aspx") +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal LoginBackgroundImage", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "vLANGUAGE", StringUtil.RTrim( AV24Language));
         GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Language, "")), context));
         GxWebStd.gx_hidden_field( context, "vAUXUSERNAME", AV8AuxUserName);
         GxWebStd.gx_hidden_field( context, "gxhash_vAUXUSERNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8AuxUserName, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERREMEMBERME", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV33UserRememberMe), 2, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERREMEMBERME", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV33UserRememberMe), "Z9"), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_62", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_62), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vIDP_STATE", StringUtil.RTrim( AV18IDP_State));
         GxWebStd.gx_hidden_field( context, "vLANGUAGE", StringUtil.RTrim( AV24Language));
         GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Language, "")), context));
         GxWebStd.gx_hidden_field( context, "vAUXUSERNAME", AV8AuxUserName);
         GxWebStd.gx_hidden_field( context, "gxhash_vAUXUSERNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8AuxUserName, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERREMEMBERME", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV33UserRememberMe), 2, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERREMEMBERME", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV33UserRememberMe), "Z9"), context));
         GxWebStd.gx_hidden_field( context, "subGridauthtypes_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Enablefixobjectfitcover", StringUtil.BoolToStr( Wwputilities_Enablefixobjectfitcover));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Enableconvertcombotobootstrapselect", StringUtil.BoolToStr( Wwputilities_Enableconvertcombotobootstrapselect));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Allowcolumnresizing", StringUtil.BoolToStr( Wwputilities_Allowcolumnresizing));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Allowcolumnreordering", StringUtil.BoolToStr( Wwputilities_Allowcolumnreordering));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Allowcolumnsrestore", StringUtil.BoolToStr( Wwputilities_Allowcolumnsrestore));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Comboloadtype", StringUtil.RTrim( Wwputilities_Comboloadtype));
         GxWebStd.gx_hidden_field( context, "GRIDAUTHTYPES_Class", StringUtil.RTrim( subGridauthtypes_Class));
         GxWebStd.gx_hidden_field( context, "GRIDAUTHTYPES_Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Visible), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "TABLEBUTTONS_Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(divTablebuttons_Visible), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vTYPEAUTHTYPE_Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbavTypeauthtype.Visible), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "vNAMEAUTHTYPE_Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavNameauthtype_Visible), 5, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "TABLEBUTTONS_Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(divTablebuttons_Visible), 5, 0, ".", "")));
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
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal LoginBackgroundImage" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE032( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT032( ) ;
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
         return formatLink("dslogin.aspx")  ;
      }

      public override string GetPgmname( )
      {
         return "DSLogin" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Login", "") ;
      }

      protected void WB030( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginLoginImageLeft", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablelogincontent_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingLeft30", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablelogin_Internalname, 1, 0, "px", 0, "px", "TableLoginTransparency", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "img_login" + " " + ((StringUtil.StrCmp(imgLogologin_gximage, "")==0) ? "GX_Image_Logo_Small_Class" : "GX_Image_"+imgLogologin_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "a03d3f2d-3b6e-40be-a5a2-9d8148598c00", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgLogologin_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divUnnamedtable1_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavLogonto_Internalname, context.GetMessage( "Log on to", ""), "col-sm-3 AttributeLoginImageLeftLabel", 0, true, "");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'',false,'" + sGXsfl_62_idx + "',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavLogonto, cmbavLogonto_Internalname, StringUtil.RTrim( AV26LogOnTo), 1, cmbavLogonto_Jsonclick, 5, "'"+""+"'"+",false,"+"'"+"EVLOGONTO.CLICK."+"'", "svchar", "", cmbavLogonto.Visible, cmbavLogonto.Enabled, 0, 0, 0, "em", 0, "", "", "AttributeLoginImageLeft", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,22);\"", "", true, 0, "HLP_DSLogin.htm");
            cmbavLogonto.CurrentValue = StringUtil.RTrim( AV26LogOnTo);
            AssignProp("", false, cmbavLogonto_Internalname, "Values", (string)(cmbavLogonto.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divCurrentrepositorycell_Internalname, 1, 0, "px", 0, "px", "col-xs-12 CellMarginTop", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblCurrentrepository_Internalname, lblCurrentrepository_Caption, "", "", lblCurrentrepository_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "DataDescriptionLogin", 0, "", lblCurrentrepository_Visible, 1, 0, 0, "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin CellLoginUserName", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUsername_Internalname, context.GetMessage( "User Name", ""), "col-sm-3 AttributeLoginImageLeftLabel", 0, true, "");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'',false,'" + sGXsfl_62_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUsername_Internalname, AV31UserName, StringUtil.RTrim( context.localUtil.Format( AV31UserName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "WWP_GAM_Email", ""), edtavUsername_Jsonclick, 0, "AttributeLoginImageLeft", "", "", "", "", 1, edtavUsername_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin CellLoginPassword", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavUserpassword_Internalname, context.GetMessage( "User Password", ""), "col-sm-3 AttributeLoginImageLeftLabel", 0, true, "");
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'',false,'" + sGXsfl_62_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserpassword_Internalname, StringUtil.RTrim( AV32UserPassword), StringUtil.RTrim( context.localUtil.Format( AV32UserPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,33);\""+" "+"data-gx-password-reveal"+" "+"idenableshowpasswordhint=\"True\""+" ", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", edtavUserpassword_Invitemessage, edtavUserpassword_Jsonclick, 0, "AttributeLoginImageLeft", "", "", "", "", edtavUserpassword_Visible, edtavUserpassword_Enabled, 0, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblForgotpassword_Internalname, context.GetMessage( "WWP_GAM_ForgotPassword", ""), "", "", lblForgotpassword_Jsonclick, "'"+""+"'"+",false,"+"'"+"EFORGOTPASSWORD.CLICK."+"'", "", "DataDescriptionLogin", 5, "", lblForgotpassword_Visible, 1, 0, 1, "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divKeepmeloggedin_cell_Internalname, 1, 0, "px", 0, "px", divKeepmeloggedin_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavKeepmeloggedin_Internalname, context.GetMessage( "Keep Me Logged In", ""), "col-sm-3 AttributeCheckBoxLabel", 0, true, "");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 40,'',false,'" + sGXsfl_62_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavKeepmeloggedin_Internalname, StringUtil.BoolToStr( AV23KeepMeLoggedIn), "", context.GetMessage( "Keep Me Logged In", ""), chkavKeepmeloggedin.Visible, chkavKeepmeloggedin.Enabled, "true", context.GetMessage( "WWP_GAM_Keepmeloggedin", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(40, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,40);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divRememberme_cell_Internalname, 1, 0, "px", 0, "px", divRememberme_cell_Class, "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, chkavRememberme_Internalname, context.GetMessage( "Remember Me", ""), "col-sm-3 AttributeCheckBoxLabel", 0, true, "");
            /* Check box */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'" + sGXsfl_62_idx + "',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRememberme_Internalname, StringUtil.BoolToStr( AV28RememberMe), "", context.GetMessage( "Remember Me", ""), chkavRememberme.Visible, chkavRememberme.Enabled, "true", context.GetMessage( "WWP_GAM_RememberMe", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(44, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,44);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 47,'',false,'',0)\"";
            ClassString = "ButtonMaterial ButtonLogin";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(62), 2, 0)+","+"null"+");", bttBtnenter_Caption, bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin", "start", "top", "", "", "div");
            wb_table1_50_032( true) ;
         }
         else
         {
            wb_table1_50_032( false) ;
         }
         return  ;
      }

      protected void wb_table1_50_032e( bool wbgen )
      {
         if ( wbgen )
         {
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTablebuttons_Internalname, divTablebuttons_Visible, 0, "px", 0, "px", "CellMarginTop", "start", "top", " "+"data-gx-flex"+" ", "flex-wrap:wrap;", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;align-self:center;", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblLoginwith_Internalname, context.GetMessage( "WWP_GAM_OrLoginWith", ""), "", "", lblLoginwith_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "DataDescriptionLogin", 0, "", 1, 1, 0, 1, "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "", "start", "top", "", "flex-grow:1;", "div");
            /*  Grid Control  */
            GridauthtypesContainer.SetIsFreestyle(true);
            GridauthtypesContainer.SetWrapped(nGXWrapped);
            StartGridControl62( ) ;
         }
         if ( wbEnd == 62 )
         {
            wbEnd = 0;
            nRC_GXsfl_62 = (int)(nGXsfl_62_idx-1);
            if ( GridauthtypesContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               if ( subGridauthtypes_Visible != 0 )
               {
                  sStyleString = "";
               }
               else
               {
                  sStyleString = " style=\"display:none;\"";
               }
               context.WriteHtmlText( "<div id=\""+"GridauthtypesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridauthtypes", GridauthtypesContainer, subGridauthtypes_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridauthtypesContainerData", GridauthtypesContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridauthtypesContainerData"+"V", GridauthtypesContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridauthtypesContainerData"+"V"+"\" value='"+GridauthtypesContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
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
            ucWwputilities.SetProperty("EnableFixObjectFitCover", Wwputilities_Enablefixobjectfitcover);
            ucWwputilities.SetProperty("EnableConvertComboToBootstrapSelect", Wwputilities_Enableconvertcombotobootstrapselect);
            ucWwputilities.SetProperty("AllowColumnResizing", Wwputilities_Allowcolumnresizing);
            ucWwputilities.SetProperty("AllowColumnReordering", Wwputilities_Allowcolumnreordering);
            ucWwputilities.SetProperty("AllowColumnsRestore", Wwputilities_Allowcolumnsrestore);
            ucWwputilities.SetProperty("ComboLoadType", Wwputilities_Comboloadtype);
            ucWwputilities.Render(context, "wwp.workwithplusutilities_fal", Wwputilities_Internalname, "WWPUTILITIESContainer");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 84,'',false,'" + sGXsfl_62_idx + "',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUrl_Internalname, AV30URL, StringUtil.RTrim( context.localUtil.Format( AV30URL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,84);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUrl_Jsonclick, 0, "Attribute", "", "", "", "", edtavUrl_Visible, 1, 0, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_DSLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 62 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridauthtypesContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  if ( subGridauthtypes_Visible != 0 )
                  {
                     sStyleString = "";
                  }
                  else
                  {
                     sStyleString = " style=\"display:none;\"";
                  }
                  context.WriteHtmlText( "<div id=\""+"GridauthtypesContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridauthtypes", GridauthtypesContainer, subGridauthtypes_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridauthtypesContainerData", GridauthtypesContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridauthtypesContainerData"+"V", GridauthtypesContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridauthtypesContainerData"+"V"+"\" value='"+GridauthtypesContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START032( )
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
         Form.Meta.addItem("description", context.GetMessage( "Login", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP030( ) ;
      }

      protected void WS032( )
      {
         START032( ) ;
         EVT032( ) ;
      }

      protected void EVT032( )
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
                                    E11032 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VLOGONTO.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E12032 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "FORGOTPASSWORD.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Forgotpassword.Click */
                              E13032 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 18), "GRIDAUTHTYPES.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "VIMAGEAUTHTYPE.CLICK") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 26), "'SELECTAUTHENTICATIONTYPE'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "VIMAGEAUTHTYPE.CLICK") == 0 ) )
                           {
                              nGXsfl_62_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_62_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_62_idx), 4, 0), 4, "0");
                              SubsflControlProps_622( ) ;
                              AV60ImageAuthType = cgiGet( edtavImageauthtype_Internalname);
                              AssignProp("", false, edtavImageauthtype_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV60ImageAuthType)) ? AV75Imageauthtype_GXI : context.convertURL( context.PathToRelativeUrl( AV60ImageAuthType))), !bGXsfl_62_Refreshing);
                              AssignProp("", false, edtavImageauthtype_Internalname, "SrcSet", context.GetImageSrcSet( AV60ImageAuthType), true);
                              cmbavTypeauthtype.Name = cmbavTypeauthtype_Internalname;
                              cmbavTypeauthtype.CurrentValue = cgiGet( cmbavTypeauthtype_Internalname);
                              AV66TypeAuthType = cgiGet( cmbavTypeauthtype_Internalname);
                              AssignAttri("", false, cmbavTypeauthtype_Internalname, AV66TypeAuthType);
                              AV27NameAuthType = cgiGet( edtavNameauthtype_Internalname);
                              AssignAttri("", false, edtavNameauthtype_Internalname, AV27NameAuthType);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E14032 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDAUTHTYPES.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridauthtypes.Load */
                                    E15032 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E16032 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "VIMAGEAUTHTYPE.CLICK") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    E17032 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'SELECTAUTHENTICATIONTYPE'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'SelectAuthenticationType' */
                                    E18032 ();
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

      protected void WE032( )
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

      protected void PA032( )
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
               GX_FocusControl = cmbavLogonto_Internalname;
               AssignAttri("", false, "GX_FocusControl", GX_FocusControl);
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridauthtypes_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_622( ) ;
         while ( nGXsfl_62_idx <= nRC_GXsfl_62 )
         {
            sendrow_622( ) ;
            nGXsfl_62_idx = ((subGridauthtypes_Islastpage==1)&&(nGXsfl_62_idx+1>subGridauthtypes_fnc_Recordsperpage( )) ? 1 : nGXsfl_62_idx+1);
            sGXsfl_62_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_62_idx), 4, 0), 4, "0");
            SubsflControlProps_622( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridauthtypesContainer)) ;
         /* End function gxnrGridauthtypes_newrow */
      }

      protected void gxgrGridauthtypes_refresh( string AV24Language ,
                                                string AV8AuxUserName ,
                                                short AV33UserRememberMe ,
                                                bool AV23KeepMeLoggedIn ,
                                                bool AV28RememberMe )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDAUTHTYPES_nCurrentRecord = 0;
         RF032( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridauthtypes_refresh */
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
         if ( cmbavLogonto.ItemCount > 0 )
         {
            AV26LogOnTo = cmbavLogonto.getValidValue(AV26LogOnTo);
            AssignAttri("", false, "AV26LogOnTo", AV26LogOnTo);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavLogonto.CurrentValue = StringUtil.RTrim( AV26LogOnTo);
            AssignProp("", false, cmbavLogonto_Internalname, "Values", cmbavLogonto.ToJavascriptSource(), true);
         }
         AV23KeepMeLoggedIn = StringUtil.StrToBool( StringUtil.BoolToStr( AV23KeepMeLoggedIn));
         AssignAttri("", false, "AV23KeepMeLoggedIn", AV23KeepMeLoggedIn);
         AV28RememberMe = StringUtil.StrToBool( StringUtil.BoolToStr( AV28RememberMe));
         AssignAttri("", false, "AV28RememberMe", AV28RememberMe);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF032( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF032( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridauthtypesContainer.ClearRows();
         }
         wbStart = 62;
         /* Execute user event: Refresh */
         E16032 ();
         nGXsfl_62_idx = 1;
         sGXsfl_62_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_62_idx), 4, 0), 4, "0");
         SubsflControlProps_622( ) ;
         bGXsfl_62_Refreshing = true;
         GridauthtypesContainer.AddObjectProperty("GridName", "Gridauthtypes");
         GridauthtypesContainer.AddObjectProperty("CmpContext", "");
         GridauthtypesContainer.AddObjectProperty("InMasterPage", "false");
         GridauthtypesContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Visible), 5, 0, ".", "")));
         GridauthtypesContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
         GridauthtypesContainer.AddObjectProperty("Class", "FreeStyleGrid");
         GridauthtypesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridauthtypesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridauthtypesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Backcolorstyle), 1, 0, ".", "")));
         GridauthtypesContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Visible), 5, 0, ".", "")));
         GridauthtypesContainer.PageSize = subGridauthtypes_fnc_Recordsperpage( );
         if ( subGridauthtypes_Islastpage != 0 )
         {
            GRIDAUTHTYPES_nFirstRecordOnPage = (long)(subGridauthtypes_fnc_Recordcount( )-subGridauthtypes_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, "GRIDAUTHTYPES_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDAUTHTYPES_nFirstRecordOnPage), 15, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("GRIDAUTHTYPES_nFirstRecordOnPage", GRIDAUTHTYPES_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_622( ) ;
            /* Execute user event: Gridauthtypes.Load */
            E15032 ();
            wbEnd = 62;
            WB030( ) ;
         }
         bGXsfl_62_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes032( )
      {
         GxWebStd.gx_hidden_field( context, "vLANGUAGE", StringUtil.RTrim( AV24Language));
         GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Language, "")), context));
         GxWebStd.gx_hidden_field( context, "vAUXUSERNAME", AV8AuxUserName);
         GxWebStd.gx_hidden_field( context, "gxhash_vAUXUSERNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV8AuxUserName, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERREMEMBERME", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV33UserRememberMe), 2, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERREMEMBERME", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV33UserRememberMe), "Z9"), context));
      }

      protected int subGridauthtypes_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridauthtypes_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridauthtypes_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridauthtypes_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP030( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E14032 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            nRC_GXsfl_62 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_62"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGridauthtypes_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "subGridauthtypes_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Wwputilities_Enablefixobjectfitcover = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Enablefixobjectfitcover"));
            Wwputilities_Enableconvertcombotobootstrapselect = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Enableconvertcombotobootstrapselect"));
            Wwputilities_Allowcolumnresizing = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Allowcolumnresizing"));
            Wwputilities_Allowcolumnreordering = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Allowcolumnreordering"));
            Wwputilities_Allowcolumnsrestore = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Allowcolumnsrestore"));
            Wwputilities_Comboloadtype = cgiGet( "WWPUTILITIES_Comboloadtype");
            subGridauthtypes_Class = cgiGet( "GRIDAUTHTYPES_Class");
            subGridauthtypes_Visible = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDAUTHTYPES_Visible"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            divTablebuttons_Visible = (int)(Math.Round(context.localUtil.CToN( cgiGet( "TABLEBUTTONS_Visible"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            /* Read variables values. */
            cmbavLogonto.Name = cmbavLogonto_Internalname;
            cmbavLogonto.CurrentValue = cgiGet( cmbavLogonto_Internalname);
            AV26LogOnTo = cgiGet( cmbavLogonto_Internalname);
            AssignAttri("", false, "AV26LogOnTo", AV26LogOnTo);
            AV31UserName = cgiGet( edtavUsername_Internalname);
            AssignAttri("", false, "AV31UserName", AV31UserName);
            AV32UserPassword = cgiGet( edtavUserpassword_Internalname);
            AssignAttri("", false, "AV32UserPassword", AV32UserPassword);
            AV23KeepMeLoggedIn = StringUtil.StrToBool( cgiGet( chkavKeepmeloggedin_Internalname));
            AssignAttri("", false, "AV23KeepMeLoggedIn", AV23KeepMeLoggedIn);
            AV28RememberMe = StringUtil.StrToBool( cgiGet( chkavRememberme_Internalname));
            AssignAttri("", false, "AV28RememberMe", AV28RememberMe);
            AV30URL = cgiGet( edtavUrl_Internalname);
            AssignAttri("", false, "AV30URL", AV30URL);
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
         E14032 ();
         if (returnInSub) return;
      }

      protected void E14032( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = "MainContainerNoBackground";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
         lblRegisteruser_Link = formatLink("dsregister.aspx") ;
         AssignProp("", false, lblRegisteruser_Internalname, "Link", lblRegisteruser_Link, true);
         lblCurrentrepository_Visible = 0;
         AssignProp("", false, lblCurrentrepository_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblCurrentrepository_Visible), 5, 0), true);
         divTablebuttons_Visible = 0;
         AssignProp("", false, divTablebuttons_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablebuttons_Visible), 5, 0), true);
         AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).checkconnection();
         AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
         if ( new GeneXus.Programs.genexussecurity.SdtGAM(context).ismultitenant() )
         {
            /* Execute user subroutine: 'ISMULTITENANTINSTALLATION' */
            S112 ();
            if (returnInSub) return;
         }
         else
         {
            if ( ! AV19isConnectionOK )
            {
               if ( new GeneXus.Programs.genexussecurity.SdtGAM(context).getdefaultrepository(out  AV29RepositoryGUID) )
               {
                  AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryguid(AV29RepositoryGUID, out  AV58Errors);
                  AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
               }
               else
               {
                  AV9ConnectionInfoCollection = new GeneXus.Programs.genexussecurity.SdtGAM(context).getconnections();
                  if ( AV9ConnectionInfoCollection.Count > 0 )
                  {
                     AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnection(((GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo)AV9ConnectionInfoCollection.Item(1)).gxTpr_Name, out  AV58Errors);
                     AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
                  }
               }
            }
         }
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S122 ();
         if (returnInSub) return;
         edtavUrl_Visible = 0;
         AssignProp("", false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), true);
         cmbavTypeauthtype.Visible = 0;
         AssignProp("", false, cmbavTypeauthtype_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavTypeauthtype.Visible), 5, 0), !bGXsfl_62_Refreshing);
         edtavNameauthtype_Visible = 0;
         AssignProp("", false, edtavNameauthtype_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavNameauthtype_Visible), 5, 0), !bGXsfl_62_Refreshing);
      }

      private void E15032( )
      {
         /* Gridauthtypes_Load Routine */
         returnInSub = false;
         AV74GXV1 = 1;
         while ( AV74GXV1 <= AV7AuthenticationTypes.Count )
         {
            AV6AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV7AuthenticationTypes.Item(AV74GXV1));
            if ( ! AV6AuthenticationType.gxTpr_Needusername )
            {
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV6AuthenticationType.gxTpr_Smallimagename)) )
               {
                  AV60ImageAuthType = context.GetImagePath( AV6AuthenticationType.gxTpr_Smallimagename, "", context.GetTheme( ));
                  AssignAttri("", false, edtavImageauthtype_Internalname, AV60ImageAuthType);
                  AV75Imageauthtype_GXI = GXDbFile.PathToUrl( AV6AuthenticationType.gxTpr_Smallimagename, context);
               }
               else
               {
                  edtavImageauthtype_gximage = "GAM_GAMButtonGAMRemoteSmall";
                  AV60ImageAuthType = context.GetImagePath( "6cdd3e18-cc5b-44e0-bd22-3efaf48a6c40", "", context.GetTheme( ));
                  AssignAttri("", false, edtavImageauthtype_Internalname, AV60ImageAuthType);
                  AV75Imageauthtype_GXI = GXDbFile.PathToUrl( context.GetImagePath( "6cdd3e18-cc5b-44e0-bd22-3efaf48a6c40", "", context.GetTheme( )), context);
               }
               AV66TypeAuthType = AV6AuthenticationType.gxTpr_Type;
               AssignAttri("", false, cmbavTypeauthtype_Internalname, AV66TypeAuthType);
               AV27NameAuthType = StringUtil.Trim( AV6AuthenticationType.gxTpr_Name);
               AssignAttri("", false, edtavNameauthtype_Internalname, AV27NameAuthType);
               edtavImageauthtype_Tooltiptext = StringUtil.Format( context.GetMessage( "Sign in with %1", ""), StringUtil.Trim( AV6AuthenticationType.gxTpr_Description), "", "", "", "", "", "", "", "");
               if ( divTablebuttons_Visible == 0 )
               {
                  divTablebuttons_Visible = 1;
                  AssignProp("", false, divTablebuttons_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablebuttons_Visible), 5, 0), true);
               }
               /* Load Method */
               if ( wbStart != -1 )
               {
                  wbStart = 62;
               }
               sendrow_622( ) ;
               if ( isFullAjaxMode( ) && ! bGXsfl_62_Refreshing )
               {
                  DoAjaxLoad(62, GridauthtypesRow);
               }
            }
            AV74GXV1 = (int)(AV74GXV1+1);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6AuthenticationType", AV6AuthenticationType);
         cmbavTypeauthtype.CurrentValue = StringUtil.RTrim( AV66TypeAuthType);
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E11032 ();
         if (returnInSub) return;
      }

      protected void E11032( )
      {
         /* Enter Routine */
         returnInSub = false;
         if ( AV23KeepMeLoggedIn )
         {
            AV5AdditionalParameter.gxTpr_Rememberusertype = 3;
         }
         else if ( AV28RememberMe )
         {
            AV5AdditionalParameter.gxTpr_Rememberusertype = 2;
         }
         else
         {
            AV5AdditionalParameter.gxTpr_Rememberusertype = 1;
         }
         AV5AdditionalParameter.gxTpr_Authenticationtypename = AV26LogOnTo;
         AV5AdditionalParameter.gxTpr_Otpstep = 1;
         AV25LoginOK = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).login(AV31UserName, AV32UserPassword, AV5AdditionalParameter, out  AV58Errors);
         if ( AV25LoginOK )
         {
            AV30URL = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrorsurl();
            AssignAttri("", false, "AV30URL", AV30URL);
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV30URL)) )
            {
               CallWebObject(formatLink("home.aspx") );
               context.wjLocDisableFrm = 1;
            }
            else
            {
               CallWebObject(formatLink(AV30URL) );
               context.wjLocDisableFrm = 0;
            }
         }
         else
         {
            if ( AV58Errors.Count > 0 )
            {
               if ( ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV58Errors.Item(1)).gxTpr_Code == 24 ) || ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV58Errors.Item(1)).gxTpr_Code == 23 ) )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamchangepassword.aspx"+UrlEncode(StringUtil.RTrim(AV18IDP_State));
                  CallWebObject(formatLink("gamchangepassword.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
                  context.wjLocDisableFrm = 1;
               }
               else if ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV58Errors.Item(1)).gxTpr_Code == 161 )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamupdateregisteruser.aspx"+UrlEncode(StringUtil.RTrim(AV18IDP_State));
                  CallWebObject(formatLink("gamupdateregisteruser.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
                  context.wjLocDisableFrm = 1;
               }
               else if ( ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV58Errors.Item(1)).gxTpr_Code == 400 ) || ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV58Errors.Item(1)).gxTpr_Code == 410 ) )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamotpauthentication.aspx"+UrlEncode(StringUtil.RTrim(AV18IDP_State));
                  CallWebObject(formatLink("gamotpauthentication.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
                  context.wjLocDisableFrm = 1;
               }
               else
               {
                  AV32UserPassword = "";
                  AssignAttri("", false, "AV32UserPassword", AV32UserPassword);
                  /* Execute user subroutine: 'DISPLAYMESSAGES' */
                  S132 ();
                  if (returnInSub) return;
               }
            }
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5AdditionalParameter", AV5AdditionalParameter);
      }

      protected void S122( )
      {
         /* 'ATTRIBUTESSECURITYCODE' Routine */
         returnInSub = false;
         if ( ! ( ( 1 == 1 ) ) )
         {
            chkavKeepmeloggedin.Visible = 0;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            divKeepmeloggedin_cell_Class = "Invisible";
            AssignProp("", false, divKeepmeloggedin_cell_Internalname, "Class", divKeepmeloggedin_cell_Class, true);
         }
         else
         {
            chkavKeepmeloggedin.Visible = 1;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            divKeepmeloggedin_cell_Class = "col-xs-12 DataContentCellLogin";
            AssignProp("", false, divKeepmeloggedin_cell_Internalname, "Class", divKeepmeloggedin_cell_Class, true);
         }
         if ( ! ( ( 1 == 1 ) ) )
         {
            chkavRememberme.Visible = 0;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
            divRememberme_cell_Class = "Invisible";
            AssignProp("", false, divRememberme_cell_Internalname, "Class", divRememberme_cell_Class, true);
         }
         else
         {
            chkavRememberme.Visible = 1;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
            divRememberme_cell_Class = "col-xs-12 DataContentCellLogin";
            AssignProp("", false, divRememberme_cell_Internalname, "Class", divRememberme_cell_Class, true);
         }
         divTablebuttons_Visible = ((new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).canauthenticatewith("Facebook")||new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).canauthenticatewith("Twitter")||new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).canauthenticatewith("Google")||new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).canauthenticatewith("GAMRemote")) ? 1 : 0);
         AssignProp("", false, divTablebuttons_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablebuttons_Visible), 5, 0), true);
      }

      protected void E16032( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         AV61isRedirect = false;
         AV59ErrorsLogin = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrors();
         if ( AV59ErrorsLogin.Count > 0 )
         {
            if ( ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV59ErrorsLogin.Item(1)).gxTpr_Code == 1 ) || ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV59ErrorsLogin.Item(1)).gxTpr_Code == 104 ) )
            {
            }
            else
            {
               AV32UserPassword = "";
               AssignAttri("", false, "AV32UserPassword", AV32UserPassword);
               AV58Errors = AV59ErrorsLogin;
               /* Execute user subroutine: 'DISPLAYMESSAGES' */
               S132 ();
               if (returnInSub) return;
            }
         }
         if ( ! AV61isRedirect )
         {
            AV65SessionValid = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).isvalid(out  AV64Session, out  AV58Errors);
            if ( AV65SessionValid && ! AV64Session.gxTpr_Isanonymous )
            {
               AV30URL = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrorsurl();
               AssignAttri("", false, "AV30URL", AV30URL);
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV30URL)) )
               {
                  AV69GAMApplicationCollection = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getapplications(AV70GAMApplicationFilter, out  AV58Errors);
                  AV76GXV2 = 1;
                  while ( AV76GXV2 <= AV69GAMApplicationCollection.Count )
                  {
                     AV71GAMApplication = ((GeneXus.Programs.genexussecurity.SdtGAMApplication)AV69GAMApplicationCollection.Item(AV76GXV2));
                     if ( StringUtil.StrCmp(AV71GAMApplication.gxTpr_Guid, "8d9934db-05db-4d64-adba-5e0466c3appU") != 0 )
                     {
                        if (true) break;
                     }
                     AV76GXV2 = (int)(AV76GXV2+1);
                  }
                  AV71GAMApplication = AV71GAMApplication.get();
                  if ( StringUtil.StrCmp(AV71GAMApplication.gxTpr_Homeobject, "") != 0 )
                  {
                     new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).applicationgohome(AV71GAMApplication.gxTpr_Guid) ;
                  }
                  else
                  {
                     CallWebObject(formatLink("home.aspx") );
                     context.wjLocDisableFrm = 1;
                  }
               }
               else
               {
                  CallWebObject(formatLink(AV30URL) );
                  context.wjLocDisableFrm = 0;
               }
            }
            else
            {
               cmbavLogonto.removeAllItems();
               AV7AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV24Language, out  AV58Errors);
               AV77GXV3 = 1;
               while ( AV77GXV3 <= AV7AuthenticationTypes.Count )
               {
                  AV6AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV7AuthenticationTypes.Item(AV77GXV3));
                  if ( AV6AuthenticationType.gxTpr_Needusername )
                  {
                     cmbavLogonto.addItem(AV6AuthenticationType.gxTpr_Name, AV6AuthenticationType.gxTpr_Description, 0);
                  }
                  else
                  {
                     subGridauthtypes_Visible = 1;
                     AssignProp("", false, "GridauthtypesContainerDiv", "Visible", StringUtil.LTrimStr( (decimal)(subGridauthtypes_Visible), 5, 0), true);
                  }
                  AV77GXV3 = (int)(AV77GXV3+1);
               }
               if ( cmbavLogonto.ItemCount <= 1 )
               {
                  cmbavLogonto.Visible = 0;
                  AssignProp("", false, cmbavLogonto_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavLogonto.Visible), 5, 0), true);
               }
               else
               {
                  AV26LogOnTo = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV7AuthenticationTypes.Item(1)).gxTpr_Name;
                  AssignAttri("", false, "AV26LogOnTo", AV26LogOnTo);
               }
               AV21isOK = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getrememberlogin(out  AV26LogOnTo, out  AV8AuxUserName, out  AV33UserRememberMe, out  AV58Errors);
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV8AuxUserName)) )
               {
                  AV31UserName = AV8AuxUserName;
                  AssignAttri("", false, "AV31UserName", AV31UserName);
               }
               if ( AV33UserRememberMe == 2 )
               {
                  AV28RememberMe = true;
                  AssignAttri("", false, "AV28RememberMe", AV28RememberMe);
               }
               AV63Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
               if ( cmbavLogonto.ItemCount > 1 )
               {
                  AV26LogOnTo = AV63Repository.gxTpr_Defaultauthenticationtypename;
                  AssignAttri("", false, "AV26LogOnTo", AV26LogOnTo);
               }
               /* Execute user subroutine: 'DISPLAYCHECKBOX' */
               S142 ();
               if (returnInSub) return;
               AV78GXV4 = 1;
               while ( AV78GXV4 <= AV7AuthenticationTypes.Count )
               {
                  AV6AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV7AuthenticationTypes.Item(AV78GXV4));
                  if ( StringUtil.StrCmp(AV6AuthenticationType.gxTpr_Name, AV26LogOnTo) == 0 )
                  {
                     /* Execute user subroutine: 'VALIDLOGONTOOTP' */
                     S152 ();
                     if (returnInSub) return;
                     if (true) break;
                  }
                  AV78GXV4 = (int)(AV78GXV4+1);
               }
            }
         }
         /*  Sending Event outputs  */
         cmbavLogonto.CurrentValue = StringUtil.RTrim( AV26LogOnTo);
         AssignProp("", false, cmbavLogonto_Internalname, "Values", cmbavLogonto.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6AuthenticationType", AV6AuthenticationType);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV63Repository", AV63Repository);
      }

      protected void E17032( )
      {
         /* Imageauthtype_Click Routine */
         returnInSub = false;
         AV5AdditionalParameter.gxTpr_Authenticationtypename = AV27NameAuthType;
         AV25LoginOK = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).login(AV31UserName, AV32UserPassword, AV5AdditionalParameter, out  AV58Errors);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5AdditionalParameter", AV5AdditionalParameter);
      }

      protected void E12032( )
      {
         /* Logonto_Click Routine */
         returnInSub = false;
         AV7AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV24Language, out  AV58Errors);
         AV20isModeOTP = false;
         AV79GXV5 = 1;
         while ( AV79GXV5 <= AV7AuthenticationTypes.Count )
         {
            AV6AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV7AuthenticationTypes.Item(AV79GXV5));
            if ( StringUtil.StrCmp(AV6AuthenticationType.gxTpr_Name, AV26LogOnTo) == 0 )
            {
               /* Execute user subroutine: 'VALIDLOGONTOOTP' */
               S152 ();
               if (returnInSub) return;
               if (true) break;
            }
            AV79GXV5 = (int)(AV79GXV5+1);
         }
         if ( ! AV20isModeOTP )
         {
            edtavUserpassword_Visible = 1;
            AssignProp("", false, edtavUserpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUserpassword_Visible), 5, 0), true);
            edtavUserpassword_Invitemessage = context.GetMessage( "WWP_GAM_Password", "");
            AssignProp("", false, edtavUserpassword_Internalname, "Invitemessage", edtavUserpassword_Invitemessage, true);
            bttBtnenter_Caption = context.GetMessage( "WWP_GAM_SignIn", "");
            AssignProp("", false, bttBtnenter_Internalname, "Caption", bttBtnenter_Caption, true);
            lblForgotpassword_Visible = 1;
            AssignProp("", false, lblForgotpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblForgotpassword_Visible), 5, 0), true);
            AV63Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
            /* Execute user subroutine: 'DISPLAYCHECKBOX' */
            S142 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6AuthenticationType", AV6AuthenticationType);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV63Repository", AV63Repository);
      }

      protected void E13032( )
      {
         /* Forgotpassword_Click Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamrecoverpasswordstep1.aspx"+UrlEncode(StringUtil.RTrim(AV18IDP_State));
         CallWebObject(formatLink("gamrecoverpasswordstep1.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void E18032( )
      {
         /* 'SelectAuthenticationType' Routine */
         returnInSub = false;
         AV5AdditionalParameter.gxTpr_Authenticationtypename = AV27NameAuthType;
         AV25LoginOK = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).login(AV31UserName, AV32UserPassword, AV5AdditionalParameter, out  AV58Errors);
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV5AdditionalParameter", AV5AdditionalParameter);
      }

      protected void S112( )
      {
         /* 'ISMULTITENANTINSTALLATION' Routine */
         returnInSub = false;
         AV15GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
         if ( ! (0==AV15GAMRepository.gxTpr_Authenticationmasterrepositoryid) )
         {
            AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryid(AV15GAMRepository.gxTpr_Authenticationmasterrepositoryid, out  AV58Errors);
            AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
         }
         if ( ! AV19isConnectionOK )
         {
            if ( new GeneXus.Programs.genexussecurity.SdtGAM(context).getdefaultrepository(out  AV29RepositoryGUID) )
            {
               AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryguid(AV29RepositoryGUID, out  AV58Errors);
               AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
            }
            else
            {
               AV9ConnectionInfoCollection = new GeneXus.Programs.genexussecurity.SdtGAM(context).getconnections();
               if ( AV9ConnectionInfoCollection.Count > 0 )
               {
                  AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnection(((GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo)AV9ConnectionInfoCollection.Item(1)).gxTpr_Name, out  AV58Errors);
                  AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
               }
            }
         }
         if ( AV19isConnectionOK )
         {
            AV15GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
            if ( ! (0==AV15GAMRepository.gxTpr_Authenticationmasterrepositoryid) )
            {
               AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryid(AV15GAMRepository.gxTpr_Authenticationmasterrepositoryid, out  AV58Errors);
               AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
               AV15GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
            }
            lblCurrentrepository_Caption = context.GetMessage( "Repository: ", "")+AV15GAMRepository.gxTpr_Name;
            AssignProp("", false, lblCurrentrepository_Internalname, "Caption", lblCurrentrepository_Caption, true);
            lblCurrentrepository_Visible = 1;
            AssignProp("", false, lblCurrentrepository_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblCurrentrepository_Visible), 5, 0), true);
         }
      }

      protected void S142( )
      {
         /* 'DISPLAYCHECKBOX' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV63Repository.gxTpr_Userremembermetype, "Login") == 0 )
         {
            chkavKeepmeloggedin.Visible = 0;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            chkavRememberme.Visible = 1;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV63Repository.gxTpr_Userremembermetype, "Auth") == 0 )
         {
            chkavKeepmeloggedin.Visible = 1;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            chkavRememberme.Visible = 0;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV63Repository.gxTpr_Userremembermetype, "Both") == 0 )
         {
            chkavKeepmeloggedin.Visible = 1;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            chkavRememberme.Visible = 1;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
         }
         else
         {
            chkavRememberme.Visible = 0;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
            chkavKeepmeloggedin.Visible = 0;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
         }
      }

      protected void S152( )
      {
         /* 'VALIDLOGONTOOTP' Routine */
         returnInSub = false;
         if ( ! AV6AuthenticationType.gxTpr_Needuserpassword )
         {
            AV20isModeOTP = true;
            edtavUserpassword_Visible = 0;
            AssignProp("", false, edtavUserpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUserpassword_Visible), 5, 0), true);
            bttBtnenter_Caption = context.GetMessage( "WWP_GAM_SendMeCode", "");
            AssignProp("", false, bttBtnenter_Internalname, "Caption", bttBtnenter_Caption, true);
            chkavRememberme.Visible = 0;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
            chkavKeepmeloggedin.Visible = 0;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            lblForgotpassword_Visible = 0;
            AssignProp("", false, lblForgotpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblForgotpassword_Visible), 5, 0), true);
         }
      }

      protected void S132( )
      {
         /* 'DISPLAYMESSAGES' Routine */
         returnInSub = false;
         AV80GXV6 = 1;
         while ( AV80GXV6 <= AV58Errors.Count )
         {
            AV57Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV58Errors.Item(AV80GXV6));
            if ( AV57Error.gxTpr_Code != 13 )
            {
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV57Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV57Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
            }
            AV80GXV6 = (int)(AV80GXV6+1);
         }
      }

      protected void wb_table1_50_032( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblUnnamedtable2_Internalname, tblUnnamedtable2_Internalname, "", "Table", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td>") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblNoaccount_Internalname, context.GetMessage( "WWP_GAM_DontHaveAccount", ""), "", "", lblNoaccount_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlock", 0, "", 1, 1, 0, 0, "HLP_DSLogin.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "<td>") ;
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblRegisteruser_Internalname, context.GetMessage( "WWP_GAM_CreateOneNow", ""), lblRegisteruser_Link, "", lblRegisteruser_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "DataDescriptionLogin", 0, "", 1, 1, 0, 0, "HLP_DSLogin.htm");
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_50_032e( true) ;
         }
         else
         {
            wb_table1_50_032e( false) ;
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
         PA032( ) ;
         WS032( ) ;
         WE032( ) ;
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
         AddStyleSheetFile("DVelop/Bootstrap/Shared/fontawesome_vlatest/css/all.min.css", "");
         AddStyleSheetFile("DVelop/Bootstrap/Shared/DVelopBootstrap.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623463334", true, true);
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
         context.AddJavascriptSource("dslogin.js", "?2024121623463339", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Mask/jquery.mask.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/BootstrapSelect.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/WorkWithPlusUtilitiesRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_622( )
      {
         edtavImageauthtype_Internalname = "vIMAGEAUTHTYPE_"+sGXsfl_62_idx;
         cmbavTypeauthtype_Internalname = "vTYPEAUTHTYPE_"+sGXsfl_62_idx;
         edtavNameauthtype_Internalname = "vNAMEAUTHTYPE_"+sGXsfl_62_idx;
      }

      protected void SubsflControlProps_fel_622( )
      {
         edtavImageauthtype_Internalname = "vIMAGEAUTHTYPE_"+sGXsfl_62_fel_idx;
         cmbavTypeauthtype_Internalname = "vTYPEAUTHTYPE_"+sGXsfl_62_fel_idx;
         edtavNameauthtype_Internalname = "vNAMEAUTHTYPE_"+sGXsfl_62_fel_idx;
      }

      protected void sendrow_622( )
      {
         sGXsfl_62_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_62_idx), 4, 0), 4, "0");
         SubsflControlProps_622( ) ;
         WB030( ) ;
         GridauthtypesRow = GXWebRow.GetNew(context,GridauthtypesContainer);
         if ( subGridauthtypes_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridauthtypes_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridauthtypes_Class, "") != 0 )
            {
               subGridauthtypes_Linesclass = subGridauthtypes_Class+"Odd";
            }
         }
         else if ( subGridauthtypes_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridauthtypes_Backstyle = 0;
            subGridauthtypes_Backcolor = subGridauthtypes_Allbackcolor;
            if ( StringUtil.StrCmp(subGridauthtypes_Class, "") != 0 )
            {
               subGridauthtypes_Linesclass = subGridauthtypes_Class+"Uniform";
            }
         }
         else if ( subGridauthtypes_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridauthtypes_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridauthtypes_Class, "") != 0 )
            {
               subGridauthtypes_Linesclass = subGridauthtypes_Class+"Odd";
            }
            subGridauthtypes_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subGridauthtypes_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridauthtypes_Backstyle = 1;
            subGridauthtypes_Backcolor = (int)(0xFFFFFF);
            if ( StringUtil.StrCmp(subGridauthtypes_Class, "") != 0 )
            {
               subGridauthtypes_Linesclass = subGridauthtypes_Class+"Odd";
            }
         }
         /* Start of Columns property logic. */
         /* Table start */
         GridauthtypesRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtablefsgridauthtypes_Internalname+"_"+sGXsfl_62_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         GridauthtypesRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         GridauthtypesRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Div Control */
         GridauthtypesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Attribute/Variable Label */
         GridauthtypesRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)"",context.GetMessage( "Image Auth Type", ""),(string)"gx-form-item AttributeImage30Label",(short)0,(bool)true,(string)"width: 25%;"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 67,'',false,'',62)\"";
         ClassString = "AttributeImage30" + " " + ((StringUtil.StrCmp(edtavImageauthtype_gximage, "")==0) ? "" : "GX_Image_"+edtavImageauthtype_gximage+"_Class");
         StyleString = "";
         AV60ImageAuthType_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV60ImageAuthType))&&String.IsNullOrEmpty(StringUtil.RTrim( AV75Imageauthtype_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV60ImageAuthType)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV60ImageAuthType)) ? AV75Imageauthtype_GXI : context.PathToRelativeUrl( AV60ImageAuthType));
         GridauthtypesRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavImageauthtype_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(short)1,(short)1,(string)"",(string)edtavImageauthtype_Tooltiptext,(short)0,(short)-1,(short)0,(string)"",(short)0,(string)"",(short)0,(short)0,(short)5,(string)edtavImageauthtype_Jsonclick,"'"+""+"'"+",false,"+"'"+"EVIMAGEAUTHTYPE.CLICK."+sGXsfl_62_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV60ImageAuthType_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl)});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         GridauthtypesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("cell");
         }
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("row");
         }
         GridauthtypesRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         GridauthtypesRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)"Invisible"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Table start */
         GridauthtypesRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblUnnamedtablecontentfsgridauthtypes_Internalname+"_"+sGXsfl_62_idx,(short)1,(string)"Table",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         GridauthtypesRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         GridauthtypesRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Div Control */
         GridauthtypesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Attribute/Variable Label */
         GridauthtypesRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)cmbavTypeauthtype_Internalname,context.GetMessage( "Type Auth Type", ""),(string)"gx-form-item AttributeLabel",(short)0,(bool)true,(string)"width: 25%;"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 74,'',false,'" + sGXsfl_62_idx + "',62)\"";
         if ( ( cmbavTypeauthtype.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "vTYPEAUTHTYPE_" + sGXsfl_62_idx;
            cmbavTypeauthtype.Name = GXCCtl;
            cmbavTypeauthtype.WebTags = "";
            cmbavTypeauthtype.addItem("APIkey", context.GetMessage( "GAM_APIkey", ""), 0);
            cmbavTypeauthtype.addItem("AppleID", context.GetMessage( "GAM_Apple", ""), 0);
            cmbavTypeauthtype.addItem("Custom", context.GetMessage( "GAM_Custom", ""), 0);
            cmbavTypeauthtype.addItem("ExternalWebService", context.GetMessage( "GAM_ExternalWebService", ""), 0);
            cmbavTypeauthtype.addItem("Facebook", context.GetMessage( "GAM_Facebook", ""), 0);
            cmbavTypeauthtype.addItem("GAMLocal", context.GetMessage( "GAM_Local", ""), 0);
            cmbavTypeauthtype.addItem("GAMRemote", context.GetMessage( "GAM_GAMRemote", ""), 0);
            cmbavTypeauthtype.addItem("GAMRemoteRest", context.GetMessage( "GAM_GAMRemoteREST", ""), 0);
            cmbavTypeauthtype.addItem("Google", context.GetMessage( "GAM_Google", ""), 0);
            cmbavTypeauthtype.addItem("Oauth20", context.GetMessage( "GAM_OAuth2.0", ""), 0);
            cmbavTypeauthtype.addItem("OTP", context.GetMessage( "GAM_OneTimePassword", ""), 0);
            cmbavTypeauthtype.addItem("Saml20", context.GetMessage( "GAM_Saml2.0", ""), 0);
            cmbavTypeauthtype.addItem("Twitter", context.GetMessage( "GAM_Twitter", ""), 0);
            cmbavTypeauthtype.addItem("WeChat", context.GetMessage( "GAM_WeChat", ""), 0);
            if ( cmbavTypeauthtype.ItemCount > 0 )
            {
               AV66TypeAuthType = cmbavTypeauthtype.getValidValue(AV66TypeAuthType);
               AssignAttri("", false, cmbavTypeauthtype_Internalname, AV66TypeAuthType);
            }
         }
         /* ComboBox */
         GridauthtypesRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavTypeauthtype,(string)cmbavTypeauthtype_Internalname,StringUtil.RTrim( AV66TypeAuthType),(short)1,(string)cmbavTypeauthtype_Jsonclick,(short)0,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"char",(string)"",cmbavTypeauthtype.Visible,(short)1,(short)0,(short)0,(short)0,(string)"em",(short)0,(string)"",(string)"",(string)"Attribute",(string)"",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,74);\"",(string)"",(bool)true,(short)0});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         cmbavTypeauthtype.CurrentValue = StringUtil.RTrim( AV66TypeAuthType);
         AssignProp("", false, cmbavTypeauthtype_Internalname, "Values", (string)(cmbavTypeauthtype.ToJavascriptSource()), !bGXsfl_62_Refreshing);
         GridauthtypesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("cell");
         }
         GridauthtypesRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Div Control */
         GridauthtypesRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Attribute/Variable Label */
         GridauthtypesRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavNameauthtype_Internalname,context.GetMessage( "Name Auth Type", ""),(string)"gx-form-item AttributeLabel",(short)0,(bool)true,(string)"width: 25%;"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 77,'',false,'" + sGXsfl_62_idx + "',62)\"";
         ROClassString = "Attribute";
         GridauthtypesRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavNameauthtype_Internalname,StringUtil.RTrim( AV27NameAuthType),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,77);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavNameauthtype_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavNameauthtype_Visible,(short)1,(short)0,(string)"text",(string)"",(short)60,(string)"chr",(short)1,(string)"row",(short)60,(short)0,(short)0,(short)62,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusSecurityCommon\\GAMAuthenticationTypeName",(string)"start",(bool)true,(string)""});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         GridauthtypesRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridauthtypesRow.AddRenderProperties(GridauthtypesColumn);
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("cell");
         }
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("row");
         }
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("table");
         }
         /* End of table */
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("cell");
         }
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("row");
         }
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            GridauthtypesContainer.CloseTag("table");
         }
         /* End of table */
         send_integrity_lvl_hashes032( ) ;
         /* End of Columns property logic. */
         GridauthtypesContainer.AddRow(GridauthtypesRow);
         nGXsfl_62_idx = ((subGridauthtypes_Islastpage==1)&&(nGXsfl_62_idx+1>subGridauthtypes_fnc_Recordsperpage( )) ? 1 : nGXsfl_62_idx+1);
         sGXsfl_62_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_62_idx), 4, 0), 4, "0");
         SubsflControlProps_622( ) ;
         /* End function sendrow_622 */
      }

      protected void init_web_controls( )
      {
         cmbavLogonto.Name = "vLOGONTO";
         cmbavLogonto.WebTags = "";
         if ( cmbavLogonto.ItemCount > 0 )
         {
            AV26LogOnTo = cmbavLogonto.getValidValue(AV26LogOnTo);
            AssignAttri("", false, "AV26LogOnTo", AV26LogOnTo);
         }
         chkavKeepmeloggedin.Name = "vKEEPMELOGGEDIN";
         chkavKeepmeloggedin.WebTags = "";
         chkavKeepmeloggedin.Caption = context.GetMessage( "Keep Me Logged In", "");
         AssignProp("", false, chkavKeepmeloggedin_Internalname, "TitleCaption", chkavKeepmeloggedin.Caption, true);
         chkavKeepmeloggedin.CheckedValue = "false";
         AV23KeepMeLoggedIn = StringUtil.StrToBool( StringUtil.BoolToStr( AV23KeepMeLoggedIn));
         AssignAttri("", false, "AV23KeepMeLoggedIn", AV23KeepMeLoggedIn);
         chkavRememberme.Name = "vREMEMBERME";
         chkavRememberme.WebTags = "";
         chkavRememberme.Caption = context.GetMessage( "Remember Me", "");
         AssignProp("", false, chkavRememberme_Internalname, "TitleCaption", chkavRememberme.Caption, true);
         chkavRememberme.CheckedValue = "false";
         AV28RememberMe = StringUtil.StrToBool( StringUtil.BoolToStr( AV28RememberMe));
         AssignAttri("", false, "AV28RememberMe", AV28RememberMe);
         GXCCtl = "vTYPEAUTHTYPE_" + sGXsfl_62_idx;
         cmbavTypeauthtype.Name = GXCCtl;
         cmbavTypeauthtype.WebTags = "";
         cmbavTypeauthtype.addItem("APIkey", context.GetMessage( "GAM_APIkey", ""), 0);
         cmbavTypeauthtype.addItem("AppleID", context.GetMessage( "GAM_Apple", ""), 0);
         cmbavTypeauthtype.addItem("Custom", context.GetMessage( "GAM_Custom", ""), 0);
         cmbavTypeauthtype.addItem("ExternalWebService", context.GetMessage( "GAM_ExternalWebService", ""), 0);
         cmbavTypeauthtype.addItem("Facebook", context.GetMessage( "GAM_Facebook", ""), 0);
         cmbavTypeauthtype.addItem("GAMLocal", context.GetMessage( "GAM_Local", ""), 0);
         cmbavTypeauthtype.addItem("GAMRemote", context.GetMessage( "GAM_GAMRemote", ""), 0);
         cmbavTypeauthtype.addItem("GAMRemoteRest", context.GetMessage( "GAM_GAMRemoteREST", ""), 0);
         cmbavTypeauthtype.addItem("Google", context.GetMessage( "GAM_Google", ""), 0);
         cmbavTypeauthtype.addItem("Oauth20", context.GetMessage( "GAM_OAuth2.0", ""), 0);
         cmbavTypeauthtype.addItem("OTP", context.GetMessage( "GAM_OneTimePassword", ""), 0);
         cmbavTypeauthtype.addItem("Saml20", context.GetMessage( "GAM_Saml2.0", ""), 0);
         cmbavTypeauthtype.addItem("Twitter", context.GetMessage( "GAM_Twitter", ""), 0);
         cmbavTypeauthtype.addItem("WeChat", context.GetMessage( "GAM_WeChat", ""), 0);
         if ( cmbavTypeauthtype.ItemCount > 0 )
         {
            AV66TypeAuthType = cmbavTypeauthtype.getValidValue(AV66TypeAuthType);
            AssignAttri("", false, cmbavTypeauthtype_Internalname, AV66TypeAuthType);
         }
         /* End function init_web_controls */
      }

      protected void StartGridControl62( )
      {
         if ( GridauthtypesContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridauthtypesContainer"+"DivS\" data-gxgridid=\"62\">") ;
            sStyleString = "";
            if ( subGridauthtypes_Visible == 0 )
            {
               sStyleString += "display:none;";
            }
            GxWebStd.gx_table_start( context, subGridauthtypes_Internalname, subGridauthtypes_Internalname, "", "FreeStyleGrid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            GridauthtypesContainer.AddObjectProperty("GridName", "Gridauthtypes");
         }
         else
         {
            GridauthtypesContainer.AddObjectProperty("GridName", "Gridauthtypes");
            GridauthtypesContainer.AddObjectProperty("Header", subGridauthtypes_Header);
            GridauthtypesContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Visible), 5, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleGrid"));
            GridauthtypesContainer.AddObjectProperty("Class", "FreeStyleGrid");
            GridauthtypesContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Backcolorstyle), 1, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Visible), 5, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("CmpContext", "");
            GridauthtypesContainer.AddObjectProperty("InMasterPage", "false");
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesColumn.AddObjectProperty("Value", context.convertURL( AV60ImageAuthType));
            GridauthtypesColumn.AddObjectProperty("Tooltiptext", StringUtil.RTrim( edtavImageauthtype_Tooltiptext));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV66TypeAuthType)));
            GridauthtypesColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbavTypeauthtype.Visible), 5, 0, ".", "")));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV27NameAuthType)));
            GridauthtypesColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavNameauthtype_Visible), 5, 0, ".", "")));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridauthtypesContainer.AddColumnProperties(GridauthtypesColumn);
            GridauthtypesContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Selectedindex), 4, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Allowselection), 1, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Selectioncolor), 9, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Allowhovering), 1, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Hoveringcolor), 9, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Allowcollapsing), 1, 0, ".", "")));
            GridauthtypesContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridauthtypes_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         imgLogologin_Internalname = "LOGOLOGIN";
         cmbavLogonto_Internalname = "vLOGONTO";
         lblCurrentrepository_Internalname = "CURRENTREPOSITORY";
         divCurrentrepositorycell_Internalname = "CURRENTREPOSITORYCELL";
         edtavUsername_Internalname = "vUSERNAME";
         edtavUserpassword_Internalname = "vUSERPASSWORD";
         lblForgotpassword_Internalname = "FORGOTPASSWORD";
         chkavKeepmeloggedin_Internalname = "vKEEPMELOGGEDIN";
         divKeepmeloggedin_cell_Internalname = "KEEPMELOGGEDIN_CELL";
         chkavRememberme_Internalname = "vREMEMBERME";
         divRememberme_cell_Internalname = "REMEMBERME_CELL";
         bttBtnenter_Internalname = "BTNENTER";
         lblNoaccount_Internalname = "NOACCOUNT";
         lblRegisteruser_Internalname = "REGISTERUSER";
         tblUnnamedtable2_Internalname = "UNNAMEDTABLE2";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         lblLoginwith_Internalname = "LOGINWITH";
         edtavImageauthtype_Internalname = "vIMAGEAUTHTYPE";
         cmbavTypeauthtype_Internalname = "vTYPEAUTHTYPE";
         edtavNameauthtype_Internalname = "vNAMEAUTHTYPE";
         tblUnnamedtablecontentfsgridauthtypes_Internalname = "UNNAMEDTABLECONTENTFSGRIDAUTHTYPES";
         tblUnnamedtablefsgridauthtypes_Internalname = "UNNAMEDTABLEFSGRIDAUTHTYPES";
         divTablebuttons_Internalname = "TABLEBUTTONS";
         divTablelogin_Internalname = "TABLELOGIN";
         divTablelogincontent_Internalname = "TABLELOGINCONTENT";
         Wwputilities_Internalname = "WWPUTILITIES";
         divTablemain_Internalname = "TABLEMAIN";
         edtavUrl_Internalname = "vURL";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subGridauthtypes_Internalname = "GRIDAUTHTYPES";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridauthtypes_Allowcollapsing = 0;
         chkavRememberme.Caption = context.GetMessage( "Remember Me", "");
         chkavKeepmeloggedin.Caption = context.GetMessage( "Keep Me Logged In", "");
         edtavNameauthtype_Jsonclick = "";
         cmbavTypeauthtype_Jsonclick = "";
         edtavImageauthtype_Jsonclick = "";
         edtavImageauthtype_gximage = "";
         edtavImageauthtype_Tooltiptext = "";
         lblRegisteruser_Link = "";
         subGridauthtypes_Backcolorstyle = 0;
         edtavUrl_Jsonclick = "";
         edtavUrl_Visible = 1;
         subGridauthtypes_Visible = 1;
         divTablebuttons_Visible = 1;
         bttBtnenter_Caption = context.GetMessage( "WWP_GAM_Login", "");
         chkavRememberme.Enabled = 1;
         chkavRememberme.Visible = 1;
         divRememberme_cell_Class = "col-xs-12";
         chkavKeepmeloggedin.Enabled = 1;
         chkavKeepmeloggedin.Visible = 1;
         divKeepmeloggedin_cell_Class = "col-xs-12";
         lblForgotpassword_Visible = 1;
         edtavUserpassword_Jsonclick = "";
         edtavUserpassword_Invitemessage = context.GetMessage( "WWP_GAM_Password", "");
         edtavUserpassword_Enabled = 1;
         edtavUserpassword_Visible = 1;
         edtavUsername_Jsonclick = "";
         edtavUsername_Enabled = 1;
         lblCurrentrepository_Caption = context.GetMessage( "Repository: <REPOSITORY>", "");
         lblCurrentrepository_Visible = 1;
         cmbavLogonto_Jsonclick = "";
         cmbavLogonto.Visible = 1;
         cmbavLogonto.Enabled = 1;
         divLayoutmaintable_Class = "Table";
         subGridauthtypes_Class = "FreeStyleGrid";
         Wwputilities_Comboloadtype = "InfiniteScrolling";
         Wwputilities_Allowcolumnsrestore = Convert.ToBoolean( -1);
         Wwputilities_Allowcolumnreordering = Convert.ToBoolean( -1);
         Wwputilities_Allowcolumnresizing = Convert.ToBoolean( -1);
         Wwputilities_Enableconvertcombotobootstrapselect = Convert.ToBoolean( -1);
         Wwputilities_Enablefixobjectfitcover = Convert.ToBoolean( -1);
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "Login", "");
         edtavNameauthtype_Visible = 1;
         cmbavTypeauthtype.Visible = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDAUTHTYPES_nFirstRecordOnPage"},{"av":"GRIDAUTHTYPES_nEOF"},{"av":"cmbavTypeauthtype"},{"av":"edtavNameauthtype_Visible","ctrl":"vNAMEAUTHTYPE","prop":"Visible"},{"av":"AV23KeepMeLoggedIn","fld":"vKEEPMELOGGEDIN"},{"av":"AV28RememberMe","fld":"vREMEMBERME"},{"av":"AV24Language","fld":"vLANGUAGE","hsh":true},{"av":"AV8AuxUserName","fld":"vAUXUSERNAME","hsh":true},{"av":"AV33UserRememberMe","fld":"vUSERREMEMBERME","pic":"Z9","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV32UserPassword","fld":"vUSERPASSWORD"},{"av":"AV30URL","fld":"vURL"},{"av":"cmbavLogonto"},{"av":"AV26LogOnTo","fld":"vLOGONTO"},{"av":"subGridauthtypes_Visible","ctrl":"GRIDAUTHTYPES","prop":"Visible"},{"av":"AV31UserName","fld":"vUSERNAME"},{"av":"AV28RememberMe","fld":"vREMEMBERME"},{"av":"chkavKeepmeloggedin.Visible","ctrl":"vKEEPMELOGGEDIN","prop":"Visible"},{"av":"chkavRememberme.Visible","ctrl":"vREMEMBERME","prop":"Visible"},{"av":"edtavUserpassword_Visible","ctrl":"vUSERPASSWORD","prop":"Visible"},{"ctrl":"BTNENTER","prop":"Caption"},{"av":"lblForgotpassword_Visible","ctrl":"FORGOTPASSWORD","prop":"Visible"}]}""");
         setEventMetadata("GRIDAUTHTYPES.LOAD","""{"handler":"E15032","iparms":[{"av":"divTablebuttons_Visible","ctrl":"TABLEBUTTONS","prop":"Visible"}]""");
         setEventMetadata("GRIDAUTHTYPES.LOAD",""","oparms":[{"av":"AV60ImageAuthType","fld":"vIMAGEAUTHTYPE"},{"av":"cmbavTypeauthtype"},{"av":"AV66TypeAuthType","fld":"vTYPEAUTHTYPE"},{"av":"AV27NameAuthType","fld":"vNAMEAUTHTYPE"},{"av":"edtavImageauthtype_Tooltiptext","ctrl":"vIMAGEAUTHTYPE","prop":"Tooltiptext"},{"av":"divTablebuttons_Visible","ctrl":"TABLEBUTTONS","prop":"Visible"}]}""");
         setEventMetadata("ENTER","""{"handler":"E11032","iparms":[{"av":"AV23KeepMeLoggedIn","fld":"vKEEPMELOGGEDIN"},{"av":"AV28RememberMe","fld":"vREMEMBERME"},{"av":"cmbavLogonto"},{"av":"AV26LogOnTo","fld":"vLOGONTO"},{"av":"AV31UserName","fld":"vUSERNAME"},{"av":"AV32UserPassword","fld":"vUSERPASSWORD"},{"av":"AV18IDP_State","fld":"vIDP_STATE"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV30URL","fld":"vURL"},{"av":"AV18IDP_State","fld":"vIDP_STATE"},{"av":"AV32UserPassword","fld":"vUSERPASSWORD"}]}""");
         setEventMetadata("VIMAGEAUTHTYPE.CLICK","""{"handler":"E17032","iparms":[{"av":"AV27NameAuthType","fld":"vNAMEAUTHTYPE"},{"av":"AV31UserName","fld":"vUSERNAME"},{"av":"AV32UserPassword","fld":"vUSERPASSWORD"}]}""");
         setEventMetadata("VLOGONTO.CLICK","""{"handler":"E12032","iparms":[{"av":"AV24Language","fld":"vLANGUAGE","hsh":true},{"av":"cmbavLogonto"},{"av":"AV26LogOnTo","fld":"vLOGONTO"}]""");
         setEventMetadata("VLOGONTO.CLICK",""","oparms":[{"av":"edtavUserpassword_Visible","ctrl":"vUSERPASSWORD","prop":"Visible"},{"av":"edtavUserpassword_Invitemessage","ctrl":"vUSERPASSWORD","prop":"Invitemessage"},{"ctrl":"BTNENTER","prop":"Caption"},{"av":"lblForgotpassword_Visible","ctrl":"FORGOTPASSWORD","prop":"Visible"},{"av":"chkavRememberme.Visible","ctrl":"vREMEMBERME","prop":"Visible"},{"av":"chkavKeepmeloggedin.Visible","ctrl":"vKEEPMELOGGEDIN","prop":"Visible"}]}""");
         setEventMetadata("FORGOTPASSWORD.CLICK","""{"handler":"E13032","iparms":[{"av":"AV18IDP_State","fld":"vIDP_STATE"}]""");
         setEventMetadata("FORGOTPASSWORD.CLICK",""","oparms":[{"av":"AV18IDP_State","fld":"vIDP_STATE"}]}""");
         setEventMetadata("'SELECTAUTHENTICATIONTYPE'","""{"handler":"E18032","iparms":[{"av":"AV27NameAuthType","fld":"vNAMEAUTHTYPE"},{"av":"AV31UserName","fld":"vUSERNAME"},{"av":"AV32UserPassword","fld":"vUSERPASSWORD"}]}""");
         setEventMetadata("VALIDV_TYPEAUTHTYPE","""{"handler":"Validv_Typeauthtype","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Nameauthtype","iparms":[]}""");
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
         AV24Language = "";
         AV8AuxUserName = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV18IDP_State = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         imgLogologin_gximage = "";
         StyleString = "";
         sImgUrl = "";
         TempTags = "";
         AV26LogOnTo = "";
         lblCurrentrepository_Jsonclick = "";
         AV31UserName = "";
         AV32UserPassword = "";
         lblForgotpassword_Jsonclick = "";
         bttBtnenter_Jsonclick = "";
         lblLoginwith_Jsonclick = "";
         GridauthtypesContainer = new GXWebGrid( context);
         sStyleString = "";
         ucWwputilities = new GXUserControl();
         AV30URL = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV60ImageAuthType = "";
         AV75Imageauthtype_GXI = "";
         AV66TypeAuthType = "";
         AV27NameAuthType = "";
         AV29RepositoryGUID = "";
         AV58Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV9ConnectionInfoCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo>( context, "GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo", "DesignSystem.Programs");
         AV7AuthenticationTypes = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple", "DesignSystem.Programs");
         AV6AuthenticationType = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple(context);
         GridauthtypesRow = new GXWebRow();
         AV5AdditionalParameter = new GeneXus.Programs.genexussecurity.SdtGAMLoginAdditionalParameters(context);
         GXEncryptionTmp = "";
         AV59ErrorsLogin = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV64Session = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV69GAMApplicationCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplication>( context, "GeneXus.Programs.genexussecurity.SdtGAMApplication", "DesignSystem.Programs");
         AV70GAMApplicationFilter = new GeneXus.Programs.genexussecurity.SdtGAMApplicationFilter(context);
         AV71GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV63Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV15GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV57Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         lblNoaccount_Jsonclick = "";
         lblRegisteruser_Jsonclick = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridauthtypes_Linesclass = "";
         GridauthtypesColumn = new GXWebColumn();
         GXCCtl = "";
         ROClassString = "";
         subGridauthtypes_Header = "";
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short AV33UserRememberMe ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridauthtypes_Backcolorstyle ;
      private short GRIDAUTHTYPES_nEOF ;
      private short nGXWrapped ;
      private short subGridauthtypes_Backstyle ;
      private short subGridauthtypes_Allowselection ;
      private short subGridauthtypes_Allowhovering ;
      private short subGridauthtypes_Allowcollapsing ;
      private short subGridauthtypes_Collapsed ;
      private int edtavNameauthtype_Visible ;
      private int divTablebuttons_Visible ;
      private int nRC_GXsfl_62 ;
      private int subGridauthtypes_Recordcount ;
      private int nGXsfl_62_idx=1 ;
      private int subGridauthtypes_Visible ;
      private int lblCurrentrepository_Visible ;
      private int edtavUsername_Enabled ;
      private int edtavUserpassword_Visible ;
      private int edtavUserpassword_Enabled ;
      private int lblForgotpassword_Visible ;
      private int edtavUrl_Visible ;
      private int subGridauthtypes_Islastpage ;
      private int AV74GXV1 ;
      private int AV76GXV2 ;
      private int AV77GXV3 ;
      private int AV78GXV4 ;
      private int AV79GXV5 ;
      private int AV80GXV6 ;
      private int idxLst ;
      private int subGridauthtypes_Backcolor ;
      private int subGridauthtypes_Allbackcolor ;
      private int subGridauthtypes_Selectedindex ;
      private int subGridauthtypes_Selectioncolor ;
      private int subGridauthtypes_Hoveringcolor ;
      private long GRIDAUTHTYPES_nCurrentRecord ;
      private long GRIDAUTHTYPES_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_62_idx="0001" ;
      private string cmbavTypeauthtype_Internalname ;
      private string edtavNameauthtype_Internalname ;
      private string AV24Language ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string AV18IDP_State ;
      private string Wwputilities_Comboloadtype ;
      private string subGridauthtypes_Class ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string divTablelogincontent_Internalname ;
      private string divTablelogin_Internalname ;
      private string ClassString ;
      private string imgLogologin_gximage ;
      private string StyleString ;
      private string sImgUrl ;
      private string imgLogologin_Internalname ;
      private string divUnnamedtable1_Internalname ;
      private string cmbavLogonto_Internalname ;
      private string TempTags ;
      private string cmbavLogonto_Jsonclick ;
      private string divCurrentrepositorycell_Internalname ;
      private string lblCurrentrepository_Internalname ;
      private string lblCurrentrepository_Caption ;
      private string lblCurrentrepository_Jsonclick ;
      private string edtavUsername_Internalname ;
      private string edtavUsername_Jsonclick ;
      private string edtavUserpassword_Internalname ;
      private string AV32UserPassword ;
      private string edtavUserpassword_Invitemessage ;
      private string edtavUserpassword_Jsonclick ;
      private string lblForgotpassword_Internalname ;
      private string lblForgotpassword_Jsonclick ;
      private string divKeepmeloggedin_cell_Internalname ;
      private string divKeepmeloggedin_cell_Class ;
      private string chkavKeepmeloggedin_Internalname ;
      private string divRememberme_cell_Internalname ;
      private string divRememberme_cell_Class ;
      private string chkavRememberme_Internalname ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Caption ;
      private string bttBtnenter_Jsonclick ;
      private string divTablebuttons_Internalname ;
      private string lblLoginwith_Internalname ;
      private string lblLoginwith_Jsonclick ;
      private string sStyleString ;
      private string subGridauthtypes_Internalname ;
      private string Wwputilities_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string edtavUrl_Internalname ;
      private string edtavUrl_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string edtavImageauthtype_Internalname ;
      private string AV66TypeAuthType ;
      private string AV27NameAuthType ;
      private string lblRegisteruser_Link ;
      private string lblRegisteruser_Internalname ;
      private string AV29RepositoryGUID ;
      private string edtavImageauthtype_gximage ;
      private string edtavImageauthtype_Tooltiptext ;
      private string GXEncryptionTmp ;
      private string tblUnnamedtable2_Internalname ;
      private string lblNoaccount_Internalname ;
      private string lblNoaccount_Jsonclick ;
      private string lblRegisteruser_Jsonclick ;
      private string sGXsfl_62_fel_idx="0001" ;
      private string subGridauthtypes_Linesclass ;
      private string tblUnnamedtablefsgridauthtypes_Internalname ;
      private string edtavImageauthtype_Jsonclick ;
      private string tblUnnamedtablecontentfsgridauthtypes_Internalname ;
      private string GXCCtl ;
      private string cmbavTypeauthtype_Jsonclick ;
      private string ROClassString ;
      private string edtavNameauthtype_Jsonclick ;
      private string subGridauthtypes_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool bGXsfl_62_Refreshing=false ;
      private bool AV23KeepMeLoggedIn ;
      private bool AV28RememberMe ;
      private bool Wwputilities_Enablefixobjectfitcover ;
      private bool Wwputilities_Enableconvertcombotobootstrapselect ;
      private bool Wwputilities_Allowcolumnresizing ;
      private bool Wwputilities_Allowcolumnreordering ;
      private bool Wwputilities_Allowcolumnsrestore ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV19isConnectionOK ;
      private bool AV25LoginOK ;
      private bool gx_refresh_fired ;
      private bool AV61isRedirect ;
      private bool AV65SessionValid ;
      private bool AV21isOK ;
      private bool AV20isModeOTP ;
      private bool AV60ImageAuthType_IsBlob ;
      private string AV8AuxUserName ;
      private string AV26LogOnTo ;
      private string AV31UserName ;
      private string AV30URL ;
      private string AV75Imageauthtype_GXI ;
      private string AV60ImageAuthType ;
      private GXWebGrid GridauthtypesContainer ;
      private GXWebRow GridauthtypesRow ;
      private GXWebColumn GridauthtypesColumn ;
      private GXUserControl ucWwputilities ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavLogonto ;
      private GXCheckbox chkavKeepmeloggedin ;
      private GXCheckbox chkavRememberme ;
      private GXCombobox cmbavTypeauthtype ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV58Errors ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo> AV9ConnectionInfoCollection ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple> AV7AuthenticationTypes ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple AV6AuthenticationType ;
      private GeneXus.Programs.genexussecurity.SdtGAMLoginAdditionalParameters AV5AdditionalParameter ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV59ErrorsLogin ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV64Session ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplication> AV69GAMApplicationCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationFilter AV70GAMApplicationFilter ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV71GAMApplication ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV63Repository ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV15GAMRepository ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV57Error ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
