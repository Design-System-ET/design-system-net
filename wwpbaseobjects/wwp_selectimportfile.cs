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
   public class wwp_selectimportfile : GXWebComponent
   {
      public wwp_selectimportfile( )
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

      public wwp_selectimportfile( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( ref string aP0_TransactionName ,
                           ref string aP1_ImportType ,
                           ref string aP2_ExtraParmsJson )
      {
         this.AV14TransactionName = aP0_TransactionName;
         this.AV9ImportType = aP1_ImportType;
         this.AV6ExtraParmsJson = aP2_ExtraParmsJson;
         ExecuteImpl();
         aP0_TransactionName=this.AV14TransactionName;
         aP1_ImportType=this.AV9ImportType;
         aP2_ExtraParmsJson=this.AV6ExtraParmsJson;
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
               gxfirstwebparm = GetFirstPar( "TransactionName");
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
                  AV14TransactionName = GetPar( "TransactionName");
                  AssignAttri(sPrefix, false, "AV14TransactionName", AV14TransactionName);
                  AV9ImportType = GetPar( "ImportType");
                  AssignAttri(sPrefix, false, "AV9ImportType", AV9ImportType);
                  AV6ExtraParmsJson = GetPar( "ExtraParmsJson");
                  AssignAttri(sPrefix, false, "AV6ExtraParmsJson", AV6ExtraParmsJson);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(string)AV14TransactionName,(string)AV9ImportType,(string)AV6ExtraParmsJson});
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
                  gxfirstwebparm = GetFirstPar( "TransactionName");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "TransactionName");
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
            PA0K2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               WS0K2( ) ;
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
            context.SendWebValue( context.GetMessage( "Select file to import", "")) ;
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
         if ( StringUtil.Len( sPrefix) == 0 )
         {
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
            bodyStyle = "";
            if ( nGXWrapped == 0 )
            {
               bodyStyle += "-moz-opacity:0;opacity:0;";
            }
            context.WriteHtmlText( " "+"class=\"form-horizontal FormNoBackgroundColor\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "wwpbaseobjects.wwp_selectimportfile.aspx"+UrlEncode(StringUtil.RTrim(AV14TransactionName)) + "," + UrlEncode(StringUtil.RTrim(AV9ImportType)) + "," + UrlEncode(StringUtil.RTrim(AV6ExtraParmsJson));
            context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal FormNoBackgroundColor\" data-gx-class=\"form-horizontal FormNoBackgroundColor\" novalidate action=\""+formatLink("wwpbaseobjects.wwp_selectimportfile.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
            GxWebStd.gx_hidden_field( context, "_EventName", "");
            GxWebStd.gx_hidden_field( context, "_EventGridId", "");
            GxWebStd.gx_hidden_field( context, "_EventRowId", "");
            context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
            AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal FormNoBackgroundColor", true);
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
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal FormNoBackgroundColor" : Form.Class)+"-fx");
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vERRORMSGS", AV5ErrorMsgs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vERRORMSGS", AV5ErrorMsgs);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vERRORMSGS", GetSecureSignedToken( sPrefix, AV5ErrorMsgs, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV14TransactionName", wcpOAV14TransactionName);
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV9ImportType", wcpOAV9ImportType);
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV6ExtraParmsJson", wcpOAV6ExtraParmsJson);
         GxWebStd.gx_hidden_field( context, sPrefix+"vIMPORTTYPE", AV9ImportType);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vERRORMSGS", AV5ErrorMsgs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vERRORMSGS", AV5ErrorMsgs);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vERRORMSGS", GetSecureSignedToken( sPrefix, AV5ErrorMsgs, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vEXTRAPARMSJSON", AV6ExtraParmsJson);
         GxWebStd.gx_hidden_field( context, sPrefix+"vTRANSACTIONNAME", AV14TransactionName);
         GXCCtlgxBlob = "vFILTERTOUPLOAD" + "_gxBlob";
         GxWebStd.gx_hidden_field( context, sPrefix+GXCCtlgxBlob, AV7FilterToUpload);
         GxWebStd.gx_hidden_field( context, sPrefix+"vFILTERTOUPLOAD_Filename", StringUtil.RTrim( edtavFiltertoupload_Filename));
         GxWebStd.gx_hidden_field( context, sPrefix+"vFILTERTOUPLOAD_Filename", StringUtil.RTrim( edtavFiltertoupload_Filename));
      }

      protected void RenderHtmlCloseForm0K2( )
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
         return "WWPBaseObjects.WWP_SelectImportFile" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Select file to import", "") ;
      }

      protected void WB0K0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wwpbaseobjects.wwp_selectimportfile.aspx");
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMainTransactionPopUp", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            ClassString = "ErrorViewer";
            StyleString = "";
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, StyleString, ClassString, sPrefix, "false");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divTableattributes_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6 DataContentCell AttributeImportFileCell DscTop", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "form-group gx-form-group", "start", "top", ""+" data-gx-for=\""+edtavFiltertoupload_Internalname+"\"", "", "div");
            /* Attribute/Variable Label */
            GxWebStd.gx_label_element( context, edtavFiltertoupload_Internalname, context.GetMessage( "File", ""), " AttributeLabel", 1, true, "");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", " gx-attribute", "start", "top", "", "", "div");
            ClassString = "Attribute";
            StyleString = "";
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'" + sPrefix + "',false,'',0)\"";
            edtavFiltertoupload_Filetype = "tmp";
            AssignProp(sPrefix, false, edtavFiltertoupload_Internalname, "Filetype", edtavFiltertoupload_Filetype, true);
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7FilterToUpload)) )
            {
               gxblobfileaux.Source = AV7FilterToUpload;
               if ( ! gxblobfileaux.HasExtension() || ( StringUtil.StrCmp(edtavFiltertoupload_Filetype, "tmp") != 0 ) )
               {
                  gxblobfileaux.SetExtension(StringUtil.Trim( edtavFiltertoupload_Filetype));
               }
               if ( gxblobfileaux.ErrCode == 0 )
               {
                  AV7FilterToUpload = gxblobfileaux.GetURI();
                  AssignProp(sPrefix, false, edtavFiltertoupload_Internalname, "URL", context.PathToRelativeUrl( AV7FilterToUpload), true);
                  edtavFiltertoupload_Filetype = gxblobfileaux.GetExtension();
                  AssignProp(sPrefix, false, edtavFiltertoupload_Internalname, "Filetype", edtavFiltertoupload_Filetype, true);
               }
               AssignProp(sPrefix, false, edtavFiltertoupload_Internalname, "URL", context.PathToRelativeUrl( AV7FilterToUpload), true);
            }
            GxWebStd.gx_blob_field( context, edtavFiltertoupload_Internalname, StringUtil.RTrim( AV7FilterToUpload), context.PathToRelativeUrl( AV7FilterToUpload), (String.IsNullOrEmpty(StringUtil.RTrim( edtavFiltertoupload_Contenttype)) ? context.GetContentType( (String.IsNullOrEmpty(StringUtil.RTrim( edtavFiltertoupload_Filetype)) ? AV7FilterToUpload : edtavFiltertoupload_Filetype)) : edtavFiltertoupload_Contenttype), false, "", edtavFiltertoupload_Parameters, 0, edtavFiltertoupload_Enabled, 1, "", "", 0, -1, 250, "px", 60, "px", 0, 0, 0, edtavFiltertoupload_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", StyleString, ClassString, "", "", ""+TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"", "", "", "HLP_WWPBaseObjects/WWP_SelectImportFile.htm");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-action-group ActionGroupRight", "start", "top", " "+"data-gx-actiongroup-type=\"toolbar\""+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 22,'" + sPrefix + "',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "", context.GetMessage( "Import", ""), bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+sPrefix+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_WWPBaseObjects/WWP_SelectImportFile.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 24,'" + sPrefix + "',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnucancel_Internalname, "", context.GetMessage( "Cancel", ""), bttBtnucancel_Jsonclick, 7, context.GetMessage( "Cancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+sPrefix+"'"+",false,"+"'"+"e110k1_client"+"'", TempTags, "", 2, "HLP_WWPBaseObjects/WWP_SelectImportFile.htm");
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

      protected void START0K2( )
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
            Form.Meta.addItem("description", context.GetMessage( "Select file to import", ""), 0) ;
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
               STRUP0K0( ) ;
            }
         }
      }

      protected void WS0K2( )
      {
         START0K2( ) ;
         EVT0K2( ) ;
      }

      protected void EVT0K2( )
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
                                 STRUP0K0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0K0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E120K2 ();
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0K0( ) ;
                              }
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
                                          /* Execute user event: Enter */
                                          E130K2 ();
                                       }
                                       dynload_actions( ) ;
                                    }
                                 }
                              }
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LOAD") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0K0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    /* Execute user event: Load */
                                    E140K2 ();
                                 }
                              }
                              /* No code required for Cancel button. It is implemented as the Reset button. */
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0K0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                    GX_FocusControl = edtavFiltertoupload_Internalname;
                                    AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
                                 }
                              }
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

      protected void WE0K2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm0K2( ) ;
            }
         }
      }

      protected void PA0K2( )
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
                  if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "wwpbaseobjects.wwp_selectimportfile.aspx")), "wwpbaseobjects.wwp_selectimportfile.aspx") == 0 ) )
                  {
                     SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "wwpbaseobjects.wwp_selectimportfile.aspx")))) ;
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
                     gxfirstwebparm = GetFirstPar( "TransactionName");
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
               GX_FocusControl = edtavFiltertoupload_Internalname;
               AssignAttri(sPrefix, false, "GX_FocusControl", GX_FocusControl);
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
         RF0K2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
      }

      protected void RF0K2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            /* Execute user event: Load */
            E140K2 ();
            WB0K0( ) ;
         }
      }

      protected void send_integrity_lvl_hashes0K2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vERRORMSGS", AV5ErrorMsgs);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vERRORMSGS", AV5ErrorMsgs);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vERRORMSGS", GetSecureSignedToken( sPrefix, AV5ErrorMsgs, context));
      }

      protected void before_start_formulas( )
      {
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0K0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E120K2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            wcpOAV14TransactionName = cgiGet( sPrefix+"wcpOAV14TransactionName");
            wcpOAV9ImportType = cgiGet( sPrefix+"wcpOAV9ImportType");
            wcpOAV6ExtraParmsJson = cgiGet( sPrefix+"wcpOAV6ExtraParmsJson");
            edtavFiltertoupload_Filename = cgiGet( sPrefix+"vFILTERTOUPLOAD_Filename");
            /* Read variables values. */
            AV7FilterToUpload = cgiGet( edtavFiltertoupload_Internalname);
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7FilterToUpload)) )
            {
               GXCCtlgxBlob = "vFILTERTOUPLOAD" + "_gxBlob";
               AV7FilterToUpload = cgiGet( sPrefix+GXCCtlgxBlob);
            }
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
         E120K2 ();
         if (returnInSub) return;
      }

      protected void E120K2( )
      {
         /* Start Routine */
         returnInSub = false;
         divLayoutmaintable_Class = divLayoutmaintable_Class+" "+"EditForm";
         AssignProp(sPrefix, false, divLayoutmaintable_Internalname, "Class", divLayoutmaintable_Class, true);
      }

      public void GXEnter( )
      {
         /* Execute user event: Enter */
         E130K2 ();
         if (returnInSub) return;
      }

      protected void E130K2( )
      {
         /* Enter Routine */
         returnInSub = false;
         AV8FilterToUploadExt = edtavFiltertoupload_Filename;
         AV8FilterToUploadExt = ((StringUtil.StringSearch( AV8FilterToUploadExt, ".", 1)>0) ? StringUtil.Substring( AV8FilterToUploadExt, StringUtil.StringSearchRev( AV8FilterToUploadExt, ".", -1)+1, StringUtil.Len( AV8FilterToUploadExt)-StringUtil.StringSearchRev( AV8FilterToUploadExt, ".", -1)) : "");
         AV15BlobId = AV7FilterToUpload;
         AV17BlobRef = StringUtil.StringReplace( AV15BlobId, "gxupload:", "");
         AV18Cache = CacheAPI.GetCache( "FL");
         AV19BlobData.FromJSonString(AV18Cache.Get(AV17BlobRef), null);
         AV20BlobPath = AV19BlobData.gxTpr_Path;
         GX_msglist.addItem("<#CLEAR#>");
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7FilterToUpload)) )
         {
            if ( ( ( StringUtil.StrCmp(StringUtil.Upper( AV8FilterToUploadExt), "CSV") == 0 ) && ( StringUtil.StrCmp(AV9ImportType, "CSV") == 0 ) ) || ( ( StringUtil.StrCmp(StringUtil.Upper( AV8FilterToUploadExt), "XLSX") == 0 ) && ( StringUtil.StrCmp(AV9ImportType, "Excel") == 0 ) ) )
            {
               AV12ResultMsg = "";
               if ( new DesignSystem.Programs.wwpbaseobjects.wwp_importdata(context).executeUdp(  AV14TransactionName,  AV9ImportType,  AV20BlobPath,  AV6ExtraParmsJson, out  AV5ErrorMsgs) )
               {
                  AV10LastErrorType = 2;
                  AV23GXV1 = 1;
                  while ( AV23GXV1 <= AV5ErrorMsgs.Count )
                  {
                     AV11Message = ((GeneXus.Utils.SdtMessages_Message)AV5ErrorMsgs.Item(AV23GXV1));
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12ResultMsg)) )
                     {
                        AV12ResultMsg += StringUtil.NewLine( );
                        if ( ( AV10LastErrorType == 0 ) && ( AV11Message.gxTpr_Type == 2 ) )
                        {
                           AV12ResultMsg += StringUtil.NewLine( );
                        }
                     }
                     AV10LastErrorType = AV11Message.gxTpr_Type;
                     AV12ResultMsg += AV11Message.gxTpr_Description;
                     AV23GXV1 = (int)(AV23GXV1+1);
                  }
                  GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  context.GetMessage( "WWP_ImportFile_ImportSuccessTitle", ""),  AV12ResultMsg,  "success",  "",  "na",  ""));
                  this.executeExternalObjectMethod(sPrefix, false, "WWPActions", "WCPopup_Close", new Object[] {(string)AV12ResultMsg}, false);
               }
               else
               {
                  AV7FilterToUpload = "";
                  AssignProp(sPrefix, false, edtavFiltertoupload_Internalname, "URL", context.PathToRelativeUrl( AV7FilterToUpload), true);
                  AV24GXV2 = 1;
                  while ( AV24GXV2 <= AV5ErrorMsgs.Count )
                  {
                     AV11Message = ((GeneXus.Utils.SdtMessages_Message)AV5ErrorMsgs.Item(AV24GXV2));
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV12ResultMsg)) )
                     {
                        AV12ResultMsg += StringUtil.NewLine( );
                        if ( StringUtil.StrCmp(AV11Message.gxTpr_Id, "WWP_LineId") == 0 )
                        {
                           AV12ResultMsg += StringUtil.NewLine( );
                        }
                     }
                     AV12ResultMsg += AV11Message.gxTpr_Description;
                     AV24GXV2 = (int)(AV24GXV2+1);
                  }
                  GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  context.GetMessage( "WWP_ImportFile_ErrorImportingTitle", ""),  AV12ResultMsg,  "error",  "",  "false",  ""));
               }
            }
            else
            {
               AV7FilterToUpload = "";
               AssignProp(sPrefix, false, edtavFiltertoupload_Internalname, "URL", context.PathToRelativeUrl( AV7FilterToUpload), true);
               AV12ResultMsg = StringUtil.Format( context.GetMessage( "WWP_ImportFile_ExpectedFileType", ""), ((StringUtil.StrCmp(AV9ImportType, "CSV")==0) ? "csv" : "xlsx"), "", "", "", "", "", "", "", "");
               GX_msglist.addItem(new DesignSystem.Programs.wwpbaseobjects.dvmessagegetbasicnotificationmsg(context).executeUdp(  context.GetMessage( "WWP_ImportFile_ErrorImportingTitle", ""),  AV12ResultMsg,  "error",  "",  "na",  ""));
            }
         }
         else
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "File", ""), "", "", "", "", "", "", "", ""));
         }
         /*  Sending Event outputs  */
      }

      protected void nextLoad( )
      {
      }

      protected void E140K2( )
      {
         /* Load Routine */
         returnInSub = false;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV14TransactionName = (string)getParm(obj,0);
         AssignAttri(sPrefix, false, "AV14TransactionName", AV14TransactionName);
         AV9ImportType = (string)getParm(obj,1);
         AssignAttri(sPrefix, false, "AV9ImportType", AV9ImportType);
         AV6ExtraParmsJson = (string)getParm(obj,2);
         AssignAttri(sPrefix, false, "AV6ExtraParmsJson", AV6ExtraParmsJson);
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
         PA0K2( ) ;
         WS0K2( ) ;
         WE0K2( ) ;
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
         sCtrlAV14TransactionName = (string)((string)getParm(obj,0));
         sCtrlAV9ImportType = (string)((string)getParm(obj,1));
         sCtrlAV6ExtraParmsJson = (string)((string)getParm(obj,2));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA0K2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wwpbaseobjects\\wwp_selectimportfile", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA0K2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV14TransactionName = (string)getParm(obj,2);
            AssignAttri(sPrefix, false, "AV14TransactionName", AV14TransactionName);
            AV9ImportType = (string)getParm(obj,3);
            AssignAttri(sPrefix, false, "AV9ImportType", AV9ImportType);
            AV6ExtraParmsJson = (string)getParm(obj,4);
            AssignAttri(sPrefix, false, "AV6ExtraParmsJson", AV6ExtraParmsJson);
         }
         wcpOAV14TransactionName = cgiGet( sPrefix+"wcpOAV14TransactionName");
         wcpOAV9ImportType = cgiGet( sPrefix+"wcpOAV9ImportType");
         wcpOAV6ExtraParmsJson = cgiGet( sPrefix+"wcpOAV6ExtraParmsJson");
         if ( ! GetJustCreated( ) && ( ( StringUtil.StrCmp(AV14TransactionName, wcpOAV14TransactionName) != 0 ) || ( StringUtil.StrCmp(AV9ImportType, wcpOAV9ImportType) != 0 ) || ( StringUtil.StrCmp(AV6ExtraParmsJson, wcpOAV6ExtraParmsJson) != 0 ) ) )
         {
            setjustcreated();
         }
         wcpOAV14TransactionName = AV14TransactionName;
         wcpOAV9ImportType = AV9ImportType;
         wcpOAV6ExtraParmsJson = AV6ExtraParmsJson;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV14TransactionName = cgiGet( sPrefix+"AV14TransactionName_CTRL");
         if ( StringUtil.Len( sCtrlAV14TransactionName) > 0 )
         {
            AV14TransactionName = cgiGet( sCtrlAV14TransactionName);
            AssignAttri(sPrefix, false, "AV14TransactionName", AV14TransactionName);
         }
         else
         {
            AV14TransactionName = cgiGet( sPrefix+"AV14TransactionName_PARM");
         }
         sCtrlAV9ImportType = cgiGet( sPrefix+"AV9ImportType_CTRL");
         if ( StringUtil.Len( sCtrlAV9ImportType) > 0 )
         {
            AV9ImportType = cgiGet( sCtrlAV9ImportType);
            AssignAttri(sPrefix, false, "AV9ImportType", AV9ImportType);
         }
         else
         {
            AV9ImportType = cgiGet( sPrefix+"AV9ImportType_PARM");
         }
         sCtrlAV6ExtraParmsJson = cgiGet( sPrefix+"AV6ExtraParmsJson_CTRL");
         if ( StringUtil.Len( sCtrlAV6ExtraParmsJson) > 0 )
         {
            AV6ExtraParmsJson = cgiGet( sCtrlAV6ExtraParmsJson);
            AssignAttri(sPrefix, false, "AV6ExtraParmsJson", AV6ExtraParmsJson);
         }
         else
         {
            AV6ExtraParmsJson = cgiGet( sPrefix+"AV6ExtraParmsJson_PARM");
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
         PA0K2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS0K2( ) ;
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
         WS0K2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"AV14TransactionName_PARM", AV14TransactionName);
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV14TransactionName)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV14TransactionName_CTRL", StringUtil.RTrim( sCtrlAV14TransactionName));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV9ImportType_PARM", AV9ImportType);
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV9ImportType)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV9ImportType_CTRL", StringUtil.RTrim( sCtrlAV9ImportType));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV6ExtraParmsJson_PARM", AV6ExtraParmsJson);
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV6ExtraParmsJson)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV6ExtraParmsJson_CTRL", StringUtil.RTrim( sCtrlAV6ExtraParmsJson));
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
         WE0K2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623361374", true, true);
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
         context.AddJavascriptSource("wwpbaseobjects/wwp_selectimportfile.js", "?2024121623361374", false, true);
         /* End function include_jscripts */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void init_default_properties( )
      {
         edtavFiltertoupload_Internalname = sPrefix+"vFILTERTOUPLOAD";
         divTableattributes_Internalname = sPrefix+"TABLEATTRIBUTES";
         bttBtnenter_Internalname = sPrefix+"BTNENTER";
         bttBtnucancel_Internalname = sPrefix+"BTNUCANCEL";
         divTablemain_Internalname = sPrefix+"TABLEMAIN";
         divLayoutmaintable_Internalname = sPrefix+"LAYOUTMAINTABLE";
         Form.Internalname = sPrefix+"FORM";
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
         edtavFiltertoupload_Jsonclick = "";
         edtavFiltertoupload_Parameters = "";
         edtavFiltertoupload_Contenttype = "";
         edtavFiltertoupload_Filetype = "";
         edtavFiltertoupload_Enabled = 1;
         divLayoutmaintable_Class = "Table TableTransactionTemplate";
         edtavFiltertoupload_Filename = "";
         context.GX_msglist.DisplayMode = 1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"AV5ErrorMsgs","fld":"vERRORMSGS","hsh":true}]}""");
         setEventMetadata("'DOUCANCEL'","""{"handler":"E110K1","iparms":[]}""");
         setEventMetadata("ENTER","""{"handler":"E130K2","iparms":[{"av":"edtavFiltertoupload_Filename","ctrl":"vFILTERTOUPLOAD","prop":"Filename"},{"av":"AV7FilterToUpload","fld":"vFILTERTOUPLOAD"},{"av":"AV9ImportType","fld":"vIMPORTTYPE"},{"av":"AV5ErrorMsgs","fld":"vERRORMSGS","hsh":true},{"av":"AV6ExtraParmsJson","fld":"vEXTRAPARMSJSON"},{"av":"AV14TransactionName","fld":"vTRANSACTIONNAME"}]""");
         setEventMetadata("ENTER",""","oparms":[{"av":"AV7FilterToUpload","fld":"vFILTERTOUPLOAD"}]}""");
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
         wcpOAV14TransactionName = "";
         wcpOAV9ImportType = "";
         wcpOAV6ExtraParmsJson = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV5ErrorMsgs = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         GXCCtlgxBlob = "";
         AV7FilterToUpload = "";
         GX_FocusControl = "";
         ClassString = "";
         StyleString = "";
         TempTags = "";
         gxblobfileaux = new GxFile(context.GetPhysicalPath());
         bttBtnenter_Jsonclick = "";
         bttBtnucancel_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         AV8FilterToUploadExt = "";
         AV15BlobId = "";
         AV17BlobRef = "";
         AV18Cache = new CacheAPI();
         AV19BlobData = new DesignSystem.Programs.wwpbaseobjects.SdtBlobData(context);
         AV20BlobPath = "";
         AV12ResultMsg = "";
         AV11Message = new GeneXus.Utils.SdtMessages_Message(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV14TransactionName = "";
         sCtrlAV9ImportType = "";
         sCtrlAV6ExtraParmsJson = "";
         /* GeneXus formulas. */
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short AV10LastErrorType ;
      private short nGXWrapped ;
      private int edtavFiltertoupload_Enabled ;
      private int AV23GXV1 ;
      private int AV24GXV2 ;
      private int idxLst ;
      private string edtavFiltertoupload_Filename ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string GXCCtlgxBlob ;
      private string GX_FocusControl ;
      private string divLayoutmaintable_Internalname ;
      private string divLayoutmaintable_Class ;
      private string divTablemain_Internalname ;
      private string ClassString ;
      private string StyleString ;
      private string divTableattributes_Internalname ;
      private string edtavFiltertoupload_Internalname ;
      private string TempTags ;
      private string edtavFiltertoupload_Filetype ;
      private string edtavFiltertoupload_Contenttype ;
      private string edtavFiltertoupload_Parameters ;
      private string edtavFiltertoupload_Jsonclick ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtnucancel_Internalname ;
      private string bttBtnucancel_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string AV15BlobId ;
      private string AV17BlobRef ;
      private string AV20BlobPath ;
      private string sCtrlAV14TransactionName ;
      private string sCtrlAV9ImportType ;
      private string sCtrlAV6ExtraParmsJson ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private string AV14TransactionName ;
      private string AV9ImportType ;
      private string AV6ExtraParmsJson ;
      private string wcpOAV14TransactionName ;
      private string wcpOAV9ImportType ;
      private string wcpOAV6ExtraParmsJson ;
      private string AV8FilterToUploadExt ;
      private string AV12ResultMsg ;
      private string AV7FilterToUpload ;
      private GxFile gxblobfileaux ;
      private GXWebForm Form ;
      private CacheAPI AV18Cache ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private string aP0_TransactionName ;
      private string aP1_ImportType ;
      private string aP2_ExtraParmsJson ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV5ErrorMsgs ;
      private DesignSystem.Programs.wwpbaseobjects.SdtBlobData AV19BlobData ;
      private GeneXus.Utils.SdtMessages_Message AV11Message ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
