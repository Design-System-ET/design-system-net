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
   public class gamremotelogin : GXDataArea
   {
      public gamremotelogin( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamremotelogin( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref string aP0_IDP_State )
      {
         this.AV17IDP_State = aP0_IDP_State;
         ExecuteImpl();
         aP0_IDP_State=this.AV17IDP_State;
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
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetFirstPar( "IDP_State");
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
               gxfirstwebparm = GetFirstPar( "IDP_State");
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetFirstPar( "IDP_State");
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
         PA0C2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START0C2( ) ;
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
         context.WriteHtmlText( " "+"class=\"form-horizontal FormBackgroundImage\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamremotelogin.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal FormBackgroundImage\" data-gx-class=\"form-horizontal FormBackgroundImage\" novalidate action=\""+formatLink("gamremotelogin.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal FormBackgroundImage", true);
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
         GxWebStd.gx_hidden_field( context, "vUSERREMEMBERME", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV38UserRememberMe), 2, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERREMEMBERME", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV38UserRememberMe), "Z9"), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "vIDP_STATE", StringUtil.RTrim( AV17IDP_State));
         GxWebStd.gx_hidden_field( context, "vLANGUAGE", StringUtil.RTrim( AV24Language));
         GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Language, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERREMEMBERME", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV38UserRememberMe), 2, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERREMEMBERME", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV38UserRememberMe), "Z9"), context));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Enablefixobjectfitcover", StringUtil.BoolToStr( Wwputilities_Enablefixobjectfitcover));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Enableconvertcombotobootstrapselect", StringUtil.BoolToStr( Wwputilities_Enableconvertcombotobootstrapselect));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Allowcolumnresizing", StringUtil.BoolToStr( Wwputilities_Allowcolumnresizing));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Allowcolumnreordering", StringUtil.BoolToStr( Wwputilities_Allowcolumnreordering));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Allowcolumnsrestore", StringUtil.BoolToStr( Wwputilities_Allowcolumnsrestore));
         GxWebStd.gx_hidden_field( context, "WWPUTILITIES_Comboloadtype", StringUtil.RTrim( Wwputilities_Comboloadtype));
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
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal FormBackgroundImage" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE0C2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT0C2( ) ;
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
         GXEncryptionTmp = "gamremotelogin.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
         return formatLink("gamremotelogin.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "GAMRemoteLogin" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Remote Login ", "") ;
      }

      protected void WB0C0( )
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
            GxWebStd.gx_div_start( context, divTablelogin_Internalname, divTablelogin_Visible, 0, "px", 0, "px", "TableLoginTransparency", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Static images/pictures */
            ClassString = "Image" + " " + ((StringUtil.StrCmp(imgLogologin_gximage, "")==0) ? "GX_Image_Logo_Class" : "GX_Image_"+imgLogologin_gximage+"_Class");
            StyleString = "";
            sImgUrl = (string)(context.GetImagePath( "ee49c4f6-1cb0-4b77-b7d6-4299054bdaaf", "", context.GetTheme( )));
            GxWebStd.gx_bitmap( context, imgLogologin_Internalname, sImgUrl, "", "", "", context.GetTheme( ), 1, 1, "", "", 0, 0, 0, "px", 0, "px", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", " "+"data-gx-image"+" ", "", "", 1, false, false, context.GetImageSrcSet( sImgUrl), "HLP_GAMRemoteLogin.htm");
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
            GxWebStd.gx_div_start( context, divLogoappclient_cell_Internalname, 1, 0, "px", 0, "px", divLogoappclient_cell_Class, "Center", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, "", context.GetMessage( "Logo App Client", ""), "col-sm-3 AttributeLabel", 0, true, "");
            /* Static Bitmap Variable */
            ClassString = "Attribute" + " " + ((StringUtil.StrCmp(imgavLogoappclient_gximage, "")==0) ? "" : "GX_Image_"+imgavLogoappclient_gximage+"_Class");
            StyleString = "";
            AV26LogoAppClient_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV26LogoAppClient))&&String.IsNullOrEmpty(StringUtil.RTrim( AV54Logoappclient_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV26LogoAppClient)));
            sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV26LogoAppClient)) ? AV54Logoappclient_GXI : context.PathToRelativeUrl( AV26LogoAppClient));
            GxWebStd.gx_bitmap( context, imgavLogoappclient_Internalname, sImgUrl, "", "", "", context.GetTheme( ), imgavLogoappclient_Visible, 0, "", "", 0, -1, 0, "", 0, "", 0, 0, 0, "", "", StyleString, ClassString, "", "", "", "", "", "", "", 1, AV26LogoAppClient_IsBlob, false, context.GetImageSrcSet( sImgUrl), "HLP_GAMRemoteLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "Center", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbappname_Internalname, lblTbappname_Caption, "", "", lblTbappname_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "TextBlockTitleLogin", 0, "", lblTbappname_Visible, 1, 0, 0, "HLP_GAMRemoteLogin.htm");
            GxWebStd.gx_div_end( context, "Center", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, cmbavLogonto_Internalname, context.GetMessage( "Log on to", ""), "col-sm-3 AttributeLoginImageLeftLabel", 0, true, "");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 29,'',false,'',0)\"";
            /* ComboBox */
            GxWebStd.gx_combobox_ctrl1( context, cmbavLogonto, cmbavLogonto_Internalname, StringUtil.RTrim( AV27LogOnTo), 1, cmbavLogonto_Jsonclick, 5, "'"+""+"'"+",false,"+"'"+"EVLOGONTO.CLICK."+"'", "svchar", "", cmbavLogonto.Visible, cmbavLogonto.Enabled, 0, 0, 0, "em", 0, "", "", "AttributeLoginImageLeft", "", "", TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,29);\"", "", true, 0, "HLP_GAMRemoteLogin.htm");
            cmbavLogonto.CurrentValue = StringUtil.RTrim( AV27LogOnTo);
            AssignProp("", false, cmbavLogonto_Internalname, "Values", (string)(cmbavLogonto.ToJavascriptSource()), true);
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 33,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUsername_Internalname, AV36UserName, StringUtil.RTrim( context.localUtil.Format( AV36UserName, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,33);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", context.GetMessage( "WWP_GAM_Email", ""), edtavUsername_Jsonclick, 0, "AttributeLoginImageLeft", "", "", "", "", 1, edtavUsername_Enabled, 0, "text", "", 80, "chr", 1, "row", 100, 0, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMUserIdentification", "start", true, "", "HLP_GAMRemoteLogin.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 37,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUserpassword_Internalname, StringUtil.RTrim( AV37UserPassword), StringUtil.RTrim( context.localUtil.Format( AV37UserPassword, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,37);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", edtavUserpassword_Invitemessage, edtavUserpassword_Jsonclick, 0, "AttributeLoginImageLeft", "", "", "", "", edtavUserpassword_Visible, edtavUserpassword_Enabled, 0, "text", "", 50, "chr", 1, "row", 50, -1, 0, 0, 0, 0, 0, true, "GeneXusSecurityCommon\\GAMPassword", "start", true, "", "HLP_GAMRemoteLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblTbforgotpwd_Internalname, context.GetMessage( "WWP_GAM_ForgotPassword", ""), "", "", lblTbforgotpwd_Jsonclick, "'"+""+"'"+",false,"+"'"+""+"'", "", "DataDescriptionLogin", 0, "", lblTbforgotpwd_Visible, 1, 0, 1, "HLP_GAMRemoteLogin.htm");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 44,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavKeepmeloggedin_Internalname, StringUtil.BoolToStr( AV23KeepMeLoggedIn), "", context.GetMessage( "Keep Me Logged In", ""), chkavKeepmeloggedin.Visible, chkavKeepmeloggedin.Enabled, "true", context.GetMessage( "WWP_GAM_Keepmeloggedin", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(44, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,44);\"");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 48,'',false,'',0)\"";
            ClassString = "AttributeCheckBox";
            StyleString = "";
            GxWebStd.gx_checkbox_ctrl( context, chkavRememberme_Internalname, StringUtil.BoolToStr( AV29RememberMe), "", context.GetMessage( "Remember Me", ""), chkavRememberme.Visible, chkavRememberme.Enabled, "true", context.GetMessage( "WWP_GAM_RememberMe", ""), StyleString, ClassString, "", "", TempTags+" onclick="+"\"gx.fn.checkboxClick(48, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,48);\"");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 DataContentCellLogin CellPaddingLogin", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 51,'',false,'',0)\"";
            ClassString = "ButtonMaterial ButtonLogin";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "", bttBtnenter_Caption, bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMRemoteLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 58,'',false,'',0)\"";
            GxWebStd.gx_single_line_edit( context, edtavUrl_Internalname, AV35URL, StringUtil.RTrim( context.localUtil.Format( AV35URL, "")), TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,58);\"", "'"+""+"'"+",false,"+"'"+""+"'", "", "", "", "", edtavUrl_Jsonclick, 0, "Attribute", "", "", "", "", edtavUrl_Visible, 1, 0, "text", "", 0, "px", 1, "row", 2048, 0, 0, 0, 0, -1, 0, true, "GeneXusSecurityCommon\\GAMURL", "start", true, "", "HLP_GAMRemoteLogin.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         wbLoad = true;
      }

      protected void START0C2( )
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
         Form.Meta.addItem("description", context.GetMessage( "Remote Login ", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP0C0( ) ;
      }

      protected void WS0C2( )
      {
         START0C2( ) ;
         EVT0C2( ) ;
      }

      protected void EVT0C2( )
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
                              E110C2 ();
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
                                    E120C2 ();
                                 }
                                 dynload_actions( ) ;
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Refresh */
                              E130C2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "VLOGONTO.CLICK") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E140C2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'FORGOTPASSWORD'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'ForgotPassword' */
                              E150C2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Load */
                              E160C2 ();
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

      protected void WE0C2( )
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

      protected void PA0C2( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamremotelogin.aspx")), "gamremotelogin.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamremotelogin.aspx")))) ;
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
                  gxfirstwebparm = GetFirstPar( "IDP_State");
                  toggleJsOutput = isJsOutputEnabled( );
                  if ( context.isSpaRequest( ) )
                  {
                     disableJsOutput();
                  }
                  if ( ! entryPointCalled && ! ( isAjaxCallMode( ) || isFullAjaxMode( ) ) )
                  {
                     AV17IDP_State = gxfirstwebparm;
                     AssignAttri("", false, "AV17IDP_State", AV17IDP_State);
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
            AV27LogOnTo = cmbavLogonto.getValidValue(AV27LogOnTo);
            AssignAttri("", false, "AV27LogOnTo", AV27LogOnTo);
         }
         if ( context.isAjaxRequest( ) )
         {
            cmbavLogonto.CurrentValue = StringUtil.RTrim( AV27LogOnTo);
            AssignProp("", false, cmbavLogonto_Internalname, "Values", cmbavLogonto.ToJavascriptSource(), true);
         }
         AV23KeepMeLoggedIn = StringUtil.StrToBool( StringUtil.BoolToStr( AV23KeepMeLoggedIn));
         AssignAttri("", false, "AV23KeepMeLoggedIn", AV23KeepMeLoggedIn);
         AV29RememberMe = StringUtil.StrToBool( StringUtil.BoolToStr( AV29RememberMe));
         AssignAttri("", false, "AV29RememberMe", AV29RememberMe);
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF0C2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF0C2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         /* Execute user event: Refresh */
         E130C2 ();
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E160C2 ();
            WB0C0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes0C2( )
      {
         GxWebStd.gx_hidden_field( context, "vLANGUAGE", StringUtil.RTrim( AV24Language));
         GxWebStd.gx_hidden_field( context, "gxhash_vLANGUAGE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Language, "")), context));
         GxWebStd.gx_hidden_field( context, "vUSERREMEMBERME", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV38UserRememberMe), 2, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vUSERREMEMBERME", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV38UserRememberMe), "Z9"), context));
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0C0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E110C2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            Wwputilities_Enablefixobjectfitcover = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Enablefixobjectfitcover"));
            Wwputilities_Enableconvertcombotobootstrapselect = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Enableconvertcombotobootstrapselect"));
            Wwputilities_Allowcolumnresizing = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Allowcolumnresizing"));
            Wwputilities_Allowcolumnreordering = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Allowcolumnreordering"));
            Wwputilities_Allowcolumnsrestore = StringUtil.StrToBool( cgiGet( "WWPUTILITIES_Allowcolumnsrestore"));
            Wwputilities_Comboloadtype = cgiGet( "WWPUTILITIES_Comboloadtype");
            /* Read variables values. */
            AV26LogoAppClient = cgiGet( imgavLogoappclient_Internalname);
            cmbavLogonto.CurrentValue = cgiGet( cmbavLogonto_Internalname);
            AV27LogOnTo = cgiGet( cmbavLogonto_Internalname);
            AssignAttri("", false, "AV27LogOnTo", AV27LogOnTo);
            AV36UserName = cgiGet( edtavUsername_Internalname);
            AssignAttri("", false, "AV36UserName", AV36UserName);
            AV37UserPassword = cgiGet( edtavUserpassword_Internalname);
            AssignAttri("", false, "AV37UserPassword", AV37UserPassword);
            AV23KeepMeLoggedIn = StringUtil.StrToBool( cgiGet( chkavKeepmeloggedin_Internalname));
            AssignAttri("", false, "AV23KeepMeLoggedIn", AV23KeepMeLoggedIn);
            AV29RememberMe = StringUtil.StrToBool( cgiGet( chkavRememberme_Internalname));
            AssignAttri("", false, "AV29RememberMe", AV29RememberMe);
            AV35URL = cgiGet( edtavUrl_Internalname);
            AssignAttri("", false, "AV35URL", AV35URL);
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
         E110C2 ();
         if (returnInSub) return;
      }

      protected void E110C2( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = "MainContainerNoBackground";
         AssignProp("", false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
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
               if ( new GeneXus.Programs.genexussecurity.SdtGAM(context).getdefaultrepository(out  AV31RepositoryGUID) )
               {
                  AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryguid(AV31RepositoryGUID, out  AV50Errors);
                  AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
               }
               else
               {
                  AV9ConnectionInfoCollection = new GeneXus.Programs.genexussecurity.SdtGAM(context).getconnections();
                  if ( AV9ConnectionInfoCollection.Count > 0 )
                  {
                     AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnection(((GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo)AV9ConnectionInfoCollection.Item(1)).gxTpr_Name, out  AV50Errors);
                     AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
                  }
               }
            }
         }
         AV11GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getremoteapplication(AV17IDP_State, out  AV50Errors);
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11GAMApplication.gxTpr_Clientimageurl)) )
         {
            imgavLogoappclient_Visible = 1;
            AssignProp("", false, imgavLogoappclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavLogoappclient_Visible), 5, 0), true);
            lblTbappname_Visible = 0;
            AssignProp("", false, lblTbappname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbappname_Visible), 5, 0), true);
            AV26LogoAppClient = AV11GAMApplication.gxTpr_Clientimageurl;
            AssignProp("", false, imgavLogoappclient_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV26LogoAppClient)) ? AV54Logoappclient_GXI : context.convertURL( context.PathToRelativeUrl( AV26LogoAppClient))), true);
            AssignProp("", false, imgavLogoappclient_Internalname, "SrcSet", context.GetImageSrcSet( AV26LogoAppClient), true);
            AV54Logoappclient_GXI = GXDbFile.PathToUrl( AV11GAMApplication.gxTpr_Clientimageurl, context);
            AssignProp("", false, imgavLogoappclient_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV26LogoAppClient)) ? AV54Logoappclient_GXI : context.convertURL( context.PathToRelativeUrl( AV26LogoAppClient))), true);
            AssignProp("", false, imgavLogoappclient_Internalname, "SrcSet", context.GetImageSrcSet( AV26LogoAppClient), true);
         }
         else
         {
            imgavLogoappclient_Visible = 0;
            AssignProp("", false, imgavLogoappclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavLogoappclient_Visible), 5, 0), true);
            lblTbappname_Visible = 1;
            AssignProp("", false, lblTbappname_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbappname_Visible), 5, 0), true);
            lblTbappname_Caption = AV11GAMApplication.gxTpr_Name;
            AssignProp("", false, lblTbappname_Internalname, "Caption", lblTbappname_Caption, true);
         }
         /* Execute user subroutine: 'ATTRIBUTESSECURITYCODE' */
         S122 ();
         if (returnInSub) return;
         edtavUrl_Visible = 0;
         AssignProp("", false, edtavUrl_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUrl_Visible), 5, 0), true);
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E120C2 ();
         if (returnInSub) return;
      }

      protected void E120C2( )
      {
         /* Enter Routine */
         returnInSub = false;
         if ( AV23KeepMeLoggedIn )
         {
            AV5AdditionalParameter.gxTpr_Rememberusertype = 3;
         }
         else if ( AV29RememberMe )
         {
            AV5AdditionalParameter.gxTpr_Rememberusertype = 2;
         }
         else
         {
            AV5AdditionalParameter.gxTpr_Rememberusertype = 1;
         }
         AV15GAMProperties = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getgamremoteinitialproperties(AV17IDP_State);
         AV5AdditionalParameter.gxTpr_Properties = AV15GAMProperties;
         AV5AdditionalParameter.gxTpr_Authenticationtypename = AV27LogOnTo;
         AV5AdditionalParameter.gxTpr_Idpstate = AV17IDP_State;
         AV5AdditionalParameter.gxTpr_Otpstep = 1;
         AV25LoginOK = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).login(AV36UserName, AV37UserPassword, AV5AdditionalParameter, out  AV50Errors);
         if ( AV25LoginOK )
         {
         }
         else
         {
            if ( AV50Errors.Count > 0 )
            {
               if ( ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV50Errors.Item(1)).gxTpr_Code == 24 ) || ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV50Errors.Item(1)).gxTpr_Code == 23 ) )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamchangepassword.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
                  CallWebObject(formatLink("gamchangepassword.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
                  context.wjLocDisableFrm = 1;
               }
               else if ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV50Errors.Item(1)).gxTpr_Code == 161 )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamupdateregisteruser.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
                  CallWebObject(formatLink("gamupdateregisteruser.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
                  context.wjLocDisableFrm = 1;
               }
               else if ( ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV50Errors.Item(1)).gxTpr_Code == 400 ) || ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV50Errors.Item(1)).gxTpr_Code == 410 ) )
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
                  {
                     gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
                  }
                  GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
                  GXEncryptionTmp = "gamotpauthentication.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
                  CallWebObject(formatLink("gamotpauthentication.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
                  context.wjLocDisableFrm = 1;
               }
               else
               {
                  AV37UserPassword = "";
                  AssignAttri("", false, "AV37UserPassword", AV37UserPassword);
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
         if ( ! ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11GAMApplication.gxTpr_Clientimageurl)) ) )
         {
            imgavLogoappclient_Visible = 0;
            AssignProp("", false, imgavLogoappclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavLogoappclient_Visible), 5, 0), true);
            divLogoappclient_cell_Class = "Invisible";
            AssignProp("", false, divLogoappclient_cell_Internalname, "Class", divLogoappclient_cell_Class, true);
         }
         else
         {
            imgavLogoappclient_Visible = 1;
            AssignProp("", false, imgavLogoappclient_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(imgavLogoappclient_Visible), 5, 0), true);
            divLogoappclient_cell_Class = "col-xs-12";
            AssignProp("", false, divLogoappclient_cell_Internalname, "Class", divLogoappclient_cell_Class, true);
         }
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
      }

      protected void E130C2( )
      {
         /* Refresh Routine */
         returnInSub = false;
         AV22isRedirect = false;
         AV51ErrorsLogin = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrors();
         if ( AV51ErrorsLogin.Count > 0 )
         {
            if ( ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV51ErrorsLogin.Item(1)).gxTpr_Code == 1 ) || ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV51ErrorsLogin.Item(1)).gxTpr_Code == 104 ) )
            {
            }
            else if ( ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV51ErrorsLogin.Item(1)).gxTpr_Code == 24 ) || ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV51ErrorsLogin.Item(1)).gxTpr_Code == 23 ) )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "gamchangepassword.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
               CallWebObject(formatLink("gamchangepassword.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
               context.wjLocDisableFrm = 1;
               AV22isRedirect = true;
            }
            else if ( ((GeneXus.Programs.genexussecurity.SdtGAMError)AV51ErrorsLogin.Item(1)).gxTpr_Code == 161 )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
               {
                  gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
               }
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "gamupdateregisteruser.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
               CallWebObject(formatLink("gamupdateregisteruser.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
               context.wjLocDisableFrm = 1;
               AV22isRedirect = true;
            }
            else
            {
               AV37UserPassword = "";
               AssignAttri("", false, "AV37UserPassword", AV37UserPassword);
               AV50Errors = AV51ErrorsLogin;
               /* Execute user subroutine: 'DISPLAYMESSAGES' */
               S132 ();
               if (returnInSub) return;
               divTablelogin_Visible = 0;
               AssignProp("", false, divTablelogin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablelogin_Visible), 5, 0), true);
            }
         }
         if ( ! AV22isRedirect )
         {
            AV33SessionValid = new GeneXus.Programs.genexussecurity.SdtGAMSession(context).isvalid(out  AV32Session, out  AV50Errors);
            if ( AV33SessionValid && ! AV32Session.gxTpr_Isanonymous )
            {
               if ( new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).isremoteauthentication(AV17IDP_State) )
               {
                  new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).redirecttoremoteauthentication(AV17IDP_State) ;
               }
               else
               {
                  AV35URL = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getlasterrorsurl();
                  AssignAttri("", false, "AV35URL", AV35URL);
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV35URL)) )
                  {
                     new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).gohome() ;
                  }
                  else
                  {
                     CallWebObject(formatLink(AV35URL) );
                     context.wjLocDisableFrm = 0;
                  }
               }
            }
            else
            {
               cmbavLogonto.removeAllItems();
               AV7AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV24Language, out  AV50Errors);
               AV55GXV1 = 1;
               while ( AV55GXV1 <= AV7AuthenticationTypes.Count )
               {
                  AV6AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV7AuthenticationTypes.Item(AV55GXV1));
                  if ( AV6AuthenticationType.gxTpr_Needusername )
                  {
                     cmbavLogonto.addItem(AV6AuthenticationType.gxTpr_Name, AV6AuthenticationType.gxTpr_Description, 0);
                  }
                  AV55GXV1 = (int)(AV55GXV1+1);
               }
               if ( cmbavLogonto.ItemCount <= 1 )
               {
                  cmbavLogonto.Visible = 0;
                  AssignProp("", false, cmbavLogonto_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(cmbavLogonto.Visible), 5, 0), true);
               }
               AV21isOK = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getrememberlogin(out  AV27LogOnTo, out  AV36UserName, out  AV38UserRememberMe, out  AV50Errors);
               if ( AV38UserRememberMe == 2 )
               {
                  AV29RememberMe = true;
                  AssignAttri("", false, "AV29RememberMe", AV29RememberMe);
               }
               AV30Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
               if ( cmbavLogonto.ItemCount > 1 )
               {
                  AV27LogOnTo = AV30Repository.gxTpr_Defaultauthenticationtypename;
                  AssignAttri("", false, "AV27LogOnTo", AV27LogOnTo);
               }
               if ( StringUtil.StrCmp(AV30Repository.gxTpr_Userremembermetype, "Login") == 0 )
               {
                  chkavKeepmeloggedin.Visible = 0;
                  AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
                  chkavRememberme.Visible = 1;
                  AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
               }
               else if ( StringUtil.StrCmp(AV30Repository.gxTpr_Userremembermetype, "Auth") == 0 )
               {
                  chkavKeepmeloggedin.Visible = 1;
                  AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
                  chkavRememberme.Visible = 0;
                  AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
               }
               else if ( StringUtil.StrCmp(AV30Repository.gxTpr_Userremembermetype, "Both") == 0 )
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
         }
         /*  Sending Event outputs  */
         cmbavLogonto.CurrentValue = StringUtil.RTrim( AV27LogOnTo);
         AssignProp("", false, cmbavLogonto_Internalname, "Values", cmbavLogonto.ToJavascriptSource(), true);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6AuthenticationType", AV6AuthenticationType);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV30Repository", AV30Repository);
      }

      protected void E140C2( )
      {
         /* Logonto_Click Routine */
         returnInSub = false;
         AV7AuthenticationTypes = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).getenabledauthenticationtypes(AV24Language, out  AV50Errors);
         AV20isModeOTP = false;
         AV56GXV2 = 1;
         while ( AV56GXV2 <= AV7AuthenticationTypes.Count )
         {
            AV6AuthenticationType = ((GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple)AV7AuthenticationTypes.Item(AV56GXV2));
            if ( StringUtil.StrCmp(AV6AuthenticationType.gxTpr_Name, AV27LogOnTo) == 0 )
            {
               /* Execute user subroutine: 'VALIDLOGONTOOTP' */
               S142 ();
               if (returnInSub) return;
               if (true) break;
            }
            AV56GXV2 = (int)(AV56GXV2+1);
         }
         if ( ! AV20isModeOTP )
         {
            edtavUserpassword_Visible = 1;
            AssignProp("", false, edtavUserpassword_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavUserpassword_Visible), 5, 0), true);
            edtavUserpassword_Invitemessage = context.GetMessage( "Password", "");
            AssignProp("", false, edtavUserpassword_Internalname, "Invitemessage", edtavUserpassword_Invitemessage, true);
            bttBtnenter_Caption = context.GetMessage( "WWP_GAM_Login", "");
            AssignProp("", false, bttBtnenter_Internalname, "Caption", bttBtnenter_Caption, true);
            lblTbforgotpwd_Visible = 1;
            AssignProp("", false, lblTbforgotpwd_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbforgotpwd_Visible), 5, 0), true);
            AV30Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
            /* Execute user subroutine: 'DISPLAYCHECKBOX' */
            S152 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV6AuthenticationType", AV6AuthenticationType);
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV30Repository", AV30Repository);
      }

      protected void E150C2( )
      {
         /* 'ForgotPassword' Routine */
         returnInSub = false;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamrecoverpasswordstep1.aspx"+UrlEncode(StringUtil.RTrim(AV17IDP_State));
         CallWebObject(formatLink("gamrecoverpasswordstep1.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey));
         context.wjLocDisableFrm = 1;
         /*  Sending Event outputs  */
      }

      protected void S112( )
      {
         /* 'ISMULTITENANTINSTALLATION' Routine */
         returnInSub = false;
         AV16GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
         if ( ! (0==AV16GAMRepository.gxTpr_Authenticationmasterrepositoryid) )
         {
            AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryid(AV16GAMRepository.gxTpr_Authenticationmasterrepositoryid, out  AV50Errors);
            AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
         }
         if ( ! AV19isConnectionOK )
         {
            if ( new GeneXus.Programs.genexussecurity.SdtGAM(context).getdefaultrepository(out  AV31RepositoryGUID) )
            {
               AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryguid(AV31RepositoryGUID, out  AV50Errors);
               AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
            }
            else
            {
               AV9ConnectionInfoCollection = new GeneXus.Programs.genexussecurity.SdtGAM(context).getconnections();
               if ( AV9ConnectionInfoCollection.Count > 0 )
               {
                  AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnection(((GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo)AV9ConnectionInfoCollection.Item(1)).gxTpr_Name, out  AV50Errors);
                  AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
               }
            }
         }
         if ( AV19isConnectionOK )
         {
            AV16GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
            if ( ! (0==AV16GAMRepository.gxTpr_Authenticationmasterrepositoryid) )
            {
               AV19isConnectionOK = new GeneXus.Programs.genexussecurity.SdtGAM(context).setconnectionbyrepositoryid(AV16GAMRepository.gxTpr_Authenticationmasterrepositoryid, out  AV50Errors);
               AssignAttri("", false, "AV19isConnectionOK", AV19isConnectionOK);
               AV16GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context).get();
            }
         }
      }

      protected void S152( )
      {
         /* 'DISPLAYCHECKBOX' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV30Repository.gxTpr_Userremembermetype, "Login") == 0 )
         {
            chkavKeepmeloggedin.Visible = 0;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            chkavRememberme.Visible = 1;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV30Repository.gxTpr_Userremembermetype, "Auth") == 0 )
         {
            chkavKeepmeloggedin.Visible = 1;
            AssignProp("", false, chkavKeepmeloggedin_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavKeepmeloggedin.Visible), 5, 0), true);
            chkavRememberme.Visible = 0;
            AssignProp("", false, chkavRememberme_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(chkavRememberme.Visible), 5, 0), true);
         }
         else if ( StringUtil.StrCmp(AV30Repository.gxTpr_Userremembermetype, "Both") == 0 )
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

      protected void S142( )
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
            lblTbforgotpwd_Visible = 0;
            AssignProp("", false, lblTbforgotpwd_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblTbforgotpwd_Visible), 5, 0), true);
         }
      }

      protected void S132( )
      {
         /* 'DISPLAYMESSAGES' Routine */
         returnInSub = false;
         AV57GXV3 = 1;
         while ( AV57GXV3 <= AV50Errors.Count )
         {
            AV10Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV50Errors.Item(AV57GXV3));
            if ( AV10Error.gxTpr_Code != 13 )
            {
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV10Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV10Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
            }
            AV57GXV3 = (int)(AV57GXV3+1);
         }
      }

      protected void nextLoad( )
      {
      }

      protected void E160C2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV17IDP_State = (string)getParm(obj,0);
         AssignAttri("", false, "AV17IDP_State", AV17IDP_State);
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
         PA0C2( ) ;
         WS0C2( ) ;
         WE0C2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623491738", true, true);
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
         context.AddJavascriptSource("gamremotelogin.js", "?2024121623491746", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/Mask/jquery.mask.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/BootstrapSelect.js", "", false, true);
         context.AddJavascriptSource("DVelop/WorkWithPlusUtilities/WorkWithPlusUtilitiesRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         cmbavLogonto.Name = "vLOGONTO";
         cmbavLogonto.WebTags = "";
         if ( cmbavLogonto.ItemCount > 0 )
         {
            AV27LogOnTo = cmbavLogonto.getValidValue(AV27LogOnTo);
            AssignAttri("", false, "AV27LogOnTo", AV27LogOnTo);
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
         AV29RememberMe = StringUtil.StrToBool( StringUtil.BoolToStr( AV29RememberMe));
         AssignAttri("", false, "AV29RememberMe", AV29RememberMe);
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         imgLogologin_Internalname = "LOGOLOGIN";
         imgavLogoappclient_Internalname = "vLOGOAPPCLIENT";
         divLogoappclient_cell_Internalname = "LOGOAPPCLIENT_CELL";
         lblTbappname_Internalname = "TBAPPNAME";
         cmbavLogonto_Internalname = "vLOGONTO";
         edtavUsername_Internalname = "vUSERNAME";
         edtavUserpassword_Internalname = "vUSERPASSWORD";
         lblTbforgotpwd_Internalname = "TBFORGOTPWD";
         chkavKeepmeloggedin_Internalname = "vKEEPMELOGGEDIN";
         divKeepmeloggedin_cell_Internalname = "KEEPMELOGGEDIN_CELL";
         chkavRememberme_Internalname = "vREMEMBERME";
         divRememberme_cell_Internalname = "REMEMBERME_CELL";
         bttBtnenter_Internalname = "BTNENTER";
         divUnnamedtable1_Internalname = "UNNAMEDTABLE1";
         divTablelogin_Internalname = "TABLELOGIN";
         divTablelogincontent_Internalname = "TABLELOGINCONTENT";
         Wwputilities_Internalname = "WWPUTILITIES";
         divTablemain_Internalname = "TABLEMAIN";
         edtavUrl_Internalname = "vURL";
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
         chkavRememberme.Caption = context.GetMessage( "Remember Me", "");
         chkavKeepmeloggedin.Caption = context.GetMessage( "Keep Me Logged In", "");
         edtavUrl_Jsonclick = "";
         edtavUrl_Visible = 1;
         bttBtnenter_Caption = context.GetMessage( "WWP_GAM_Login", "");
         chkavRememberme.Enabled = 1;
         chkavRememberme.Visible = 1;
         divRememberme_cell_Class = "col-xs-12";
         chkavKeepmeloggedin.Enabled = 1;
         chkavKeepmeloggedin.Visible = 1;
         divKeepmeloggedin_cell_Class = "col-xs-12";
         lblTbforgotpwd_Visible = 1;
         edtavUserpassword_Jsonclick = "";
         edtavUserpassword_Invitemessage = context.GetMessage( "WWP_GAM_Password", "");
         edtavUserpassword_Enabled = 1;
         edtavUserpassword_Visible = 1;
         edtavUsername_Jsonclick = "";
         edtavUsername_Enabled = 1;
         cmbavLogonto_Jsonclick = "";
         cmbavLogonto.Visible = 1;
         cmbavLogonto.Enabled = 1;
         lblTbappname_Caption = context.GetMessage( "App Name", "");
         lblTbappname_Visible = 1;
         imgavLogoappclient_gximage = "";
         imgavLogoappclient_Visible = 1;
         divLogoappclient_cell_Class = "col-xs-12";
         divTablelogin_Visible = 1;
         divLayoutmaintable_Class = "Table";
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
         Form.Caption = context.GetMessage( "Remote Login ", "");
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV17IDP_State","fld":"vIDP_STATE"},{"av":"AV36UserName","fld":"vUSERNAME"},{"av":"AV23KeepMeLoggedIn","fld":"vKEEPMELOGGEDIN"},{"av":"AV29RememberMe","fld":"vREMEMBERME"},{"av":"AV24Language","fld":"vLANGUAGE","hsh":true},{"av":"AV38UserRememberMe","fld":"vUSERREMEMBERME","pic":"Z9","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV17IDP_State","fld":"vIDP_STATE"},{"av":"AV37UserPassword","fld":"vUSERPASSWORD"},{"av":"divTablelogin_Visible","ctrl":"TABLELOGIN","prop":"Visible"},{"av":"AV35URL","fld":"vURL"},{"av":"cmbavLogonto"},{"av":"AV27LogOnTo","fld":"vLOGONTO"},{"av":"AV29RememberMe","fld":"vREMEMBERME"},{"av":"chkavKeepmeloggedin.Visible","ctrl":"vKEEPMELOGGEDIN","prop":"Visible"},{"av":"chkavRememberme.Visible","ctrl":"vREMEMBERME","prop":"Visible"}]}""");
         setEventMetadata("ENTER","""{"handler":"E120C2","iparms":[{"av":"AV23KeepMeLoggedIn","fld":"vKEEPMELOGGEDIN"},{"av":"AV29RememberMe","fld":"vREMEMBERME"},{"av":"AV17IDP_State","fld":"vIDP_STATE"},{"av":"cmbavLogonto"},{"av":"AV27LogOnTo","fld":"vLOGONTO"},{"av":"AV36UserName","fld":"vUSERNAME"},{"av":"AV37UserPassword","fld":"vUSERPASSWORD"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV17IDP_State","fld":"vIDP_STATE"},{"av":"AV37UserPassword","fld":"vUSERPASSWORD"}]}""");
         setEventMetadata("VLOGONTO.CLICK","""{"handler":"E140C2","iparms":[{"av":"AV24Language","fld":"vLANGUAGE","hsh":true},{"av":"cmbavLogonto"},{"av":"AV27LogOnTo","fld":"vLOGONTO"}]""");
         setEventMetadata("VLOGONTO.CLICK",""","oparms":[{"av":"edtavUserpassword_Visible","ctrl":"vUSERPASSWORD","prop":"Visible"},{"av":"edtavUserpassword_Invitemessage","ctrl":"vUSERPASSWORD","prop":"Invitemessage"},{"ctrl":"BTNENTER","prop":"Caption"},{"av":"lblTbforgotpwd_Visible","ctrl":"TBFORGOTPWD","prop":"Visible"},{"av":"chkavRememberme.Visible","ctrl":"vREMEMBERME","prop":"Visible"},{"av":"chkavKeepmeloggedin.Visible","ctrl":"vKEEPMELOGGEDIN","prop":"Visible"}]}""");
         setEventMetadata("'FORGOTPASSWORD'","""{"handler":"E150C2","iparms":[{"av":"AV17IDP_State","fld":"vIDP_STATE"}]""");
         setEventMetadata("'FORGOTPASSWORD'",""","oparms":[{"av":"AV17IDP_State","fld":"vIDP_STATE"}]}""");
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
         wcpOAV17IDP_State = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV24Language = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ClassString = "";
         imgLogologin_gximage = "";
         StyleString = "";
         sImgUrl = "";
         AV26LogoAppClient = "";
         AV54Logoappclient_GXI = "";
         lblTbappname_Jsonclick = "";
         TempTags = "";
         AV27LogOnTo = "";
         AV36UserName = "";
         AV37UserPassword = "";
         lblTbforgotpwd_Jsonclick = "";
         bttBtnenter_Jsonclick = "";
         ucWwputilities = new GXUserControl();
         AV35URL = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         AV31RepositoryGUID = "";
         AV50Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV9ConnectionInfoCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo>( context, "GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo", "DesignSystem.Programs");
         AV11GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV5AdditionalParameter = new GeneXus.Programs.genexussecurity.SdtGAMLoginAdditionalParameters(context);
         AV15GAMProperties = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMProperty>( context, "GeneXus.Programs.genexussecurity.SdtGAMProperty", "DesignSystem.Programs");
         AV51ErrorsLogin = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV32Session = new GeneXus.Programs.genexussecurity.SdtGAMSession(context);
         AV7AuthenticationTypes = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple>( context, "GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple", "DesignSystem.Programs");
         AV6AuthenticationType = new GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple(context);
         AV30Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV16GAMRepository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         AV10Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short AV38UserRememberMe ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short nGXWrapped ;
      private int divTablelogin_Visible ;
      private int imgavLogoappclient_Visible ;
      private int lblTbappname_Visible ;
      private int edtavUsername_Enabled ;
      private int edtavUserpassword_Visible ;
      private int edtavUserpassword_Enabled ;
      private int lblTbforgotpwd_Visible ;
      private int edtavUrl_Visible ;
      private int AV55GXV1 ;
      private int AV56GXV2 ;
      private int AV57GXV3 ;
      private int idxLst ;
      private string AV17IDP_State ;
      private string wcpOAV17IDP_State ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string AV24Language ;
      private string Wwputilities_Comboloadtype ;
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
      private string divLogoappclient_cell_Internalname ;
      private string divLogoappclient_cell_Class ;
      private string imgavLogoappclient_gximage ;
      private string imgavLogoappclient_Internalname ;
      private string lblTbappname_Internalname ;
      private string lblTbappname_Caption ;
      private string lblTbappname_Jsonclick ;
      private string cmbavLogonto_Internalname ;
      private string TempTags ;
      private string cmbavLogonto_Jsonclick ;
      private string edtavUsername_Internalname ;
      private string edtavUsername_Jsonclick ;
      private string edtavUserpassword_Internalname ;
      private string AV37UserPassword ;
      private string edtavUserpassword_Invitemessage ;
      private string edtavUserpassword_Jsonclick ;
      private string lblTbforgotpwd_Internalname ;
      private string lblTbforgotpwd_Jsonclick ;
      private string divKeepmeloggedin_cell_Internalname ;
      private string divKeepmeloggedin_cell_Class ;
      private string chkavKeepmeloggedin_Internalname ;
      private string divRememberme_cell_Internalname ;
      private string divRememberme_cell_Class ;
      private string chkavRememberme_Internalname ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Caption ;
      private string bttBtnenter_Jsonclick ;
      private string Wwputilities_Internalname ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string edtavUrl_Internalname ;
      private string edtavUrl_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string AV31RepositoryGUID ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Wwputilities_Enablefixobjectfitcover ;
      private bool Wwputilities_Enableconvertcombotobootstrapselect ;
      private bool Wwputilities_Allowcolumnresizing ;
      private bool Wwputilities_Allowcolumnreordering ;
      private bool Wwputilities_Allowcolumnsrestore ;
      private bool wbLoad ;
      private bool AV26LogoAppClient_IsBlob ;
      private bool AV23KeepMeLoggedIn ;
      private bool AV29RememberMe ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV19isConnectionOK ;
      private bool AV25LoginOK ;
      private bool AV22isRedirect ;
      private bool AV33SessionValid ;
      private bool AV21isOK ;
      private bool AV20isModeOTP ;
      private string AV54Logoappclient_GXI ;
      private string AV27LogOnTo ;
      private string AV36UserName ;
      private string AV35URL ;
      private string AV26LogoAppClient ;
      private GXUserControl ucWwputilities ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_IDP_State ;
      private GXCombobox cmbavLogonto ;
      private GXCheckbox chkavKeepmeloggedin ;
      private GXCheckbox chkavRememberme ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV50Errors ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMConnectionInfo> AV9ConnectionInfoCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV11GAMApplication ;
      private GeneXus.Programs.genexussecurity.SdtGAMLoginAdditionalParameters AV5AdditionalParameter ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMProperty> AV15GAMProperties ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV51ErrorsLogin ;
      private GeneXus.Programs.genexussecurity.SdtGAMSession AV32Session ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple> AV7AuthenticationTypes ;
      private GeneXus.Programs.genexussecurity.SdtGAMAuthenticationTypeSimple AV6AuthenticationType ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV30Repository ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV16GAMRepository ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV10Error ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
