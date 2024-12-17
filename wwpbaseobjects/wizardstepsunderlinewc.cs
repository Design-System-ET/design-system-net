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
   public class wizardstepsunderlinewc : GXWebComponent
   {
      public wizardstepsunderlinewc( )
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

      public wizardstepsunderlinewc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> aP0_WizardSteps ,
                           string aP1_CurrentStep )
      {
         this.AV18WizardSteps = aP0_WizardSteps;
         this.AV6CurrentStep = aP1_CurrentStep;
         ExecuteImpl();
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
               gxfirstwebparm = GetFirstPar( "WizardSteps");
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
                  ajax_req_read_hidden_sdt(GetNextPar( ), AV18WizardSteps);
                  AV6CurrentStep = GetPar( "CurrentStep");
                  AssignAttri(sPrefix, false, "AV6CurrentStep", AV6CurrentStep);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)AV18WizardSteps,(string)AV6CurrentStep});
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
                  gxfirstwebparm = GetFirstPar( "WizardSteps");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
               {
                  if ( ! IsValidAjaxCall( true) )
                  {
                     GxWebError = 1;
                     return  ;
                  }
                  gxfirstwebparm = GetFirstPar( "WizardSteps");
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridwizardsteps") == 0 )
               {
                  gxnrGridwizardsteps_newrow_invoke( ) ;
                  return  ;
               }
               else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridwizardsteps") == 0 )
               {
                  gxgrGridwizardsteps_refresh_invoke( ) ;
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
         }
         if ( StringUtil.Len( sPrefix) == 0 )
         {
            if ( ! context.IsLocalStorageSupported( ) )
            {
               context.PushCurrentUrl();
            }
         }
      }

      protected void gxnrGridwizardsteps_newrow_invoke( )
      {
         nRC_GXsfl_9 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_9"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_9_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_9_idx = GetPar( "sGXsfl_9_idx");
         sPrefix = GetPar( "sPrefix");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridwizardsteps_newrow( ) ;
         /* End function gxnrGridwizardsteps_newrow_invoke */
      }

      protected void gxgrGridwizardsteps_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV19WizardStepsAux);
         AV16StepRealNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "StepRealNumber"), "."), 18, MidpointRounding.ToEven));
         AV15StepNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "StepNumber"), "."), 18, MidpointRounding.ToEven));
         AV13SelectedStepNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "SelectedStepNumber"), "."), 18, MidpointRounding.ToEven));
         AV20WizardStepsCount = (short)(Math.Round(NumberUtil.Val( GetPar( "WizardStepsCount"), "."), 18, MidpointRounding.ToEven));
         AV7FirstIsDummy = StringUtil.StrToBool( GetPar( "FirstIsDummy"));
         AV8LastIsDummy = StringUtil.StrToBool( GetPar( "LastIsDummy"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18WizardSteps);
         AV11PenultimateIsDummy = StringUtil.StrToBool( GetPar( "PenultimateIsDummy"));
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridwizardsteps_refresh( AV19WizardStepsAux, AV16StepRealNumber, AV15StepNumber, AV13SelectedStepNumber, AV20WizardStepsCount, AV7FirstIsDummy, AV8LastIsDummy, AV18WizardSteps, AV11PenultimateIsDummy, sPrefix) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridwizardsteps_refresh_invoke */
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
            PA0U2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavWizardsteptitle_Enabled = 0;
               AssignProp(sPrefix, false, edtavWizardsteptitle_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWizardsteptitle_Enabled), 5, 0), !bGXsfl_9_Refreshing);
               WS0U2( ) ;
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
            context.SendWebValue( context.GetMessage( "Wizard Steps Underline WC", "")) ;
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
            FormProcess = ((nGXWrapped==0) ? " data-HasEnter=\"false\" data-Skiponenter=\"false\"" : "");
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
            context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            if ( nGXWrapped != 1 )
            {
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "wwpbaseobjects.wizardstepsunderlinewc.aspx"+UrlEncode(StringUtil.RTrim(AV6CurrentStep));
               context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wwpbaseobjects.wizardstepsunderlinewc.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
               GxWebStd.gx_hidden_field( context, "_EventName", "");
               GxWebStd.gx_hidden_field( context, "_EventGridId", "");
               GxWebStd.gx_hidden_field( context, "_EventRowId", "");
               context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
               AssignProp(sPrefix, false, "FORM", "Class", "form-horizontal Form", true);
            }
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
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
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
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPSAUX", AV19WizardStepsAux);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPSAUX", AV19WizardStepsAux);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSAUX", GetSecureSignedToken( sPrefix, AV19WizardStepsAux, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPREALNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV16StepRealNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV16StepRealNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15StepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSELECTEDSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13SelectedStepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV13SelectedStepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWIZARDSTEPSCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20WizardStepsCount), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFIRSTISDUMMY", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vLASTISDUMMY", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vPENULTIMATEISDUMMY", AV11PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV11PenultimateIsDummy, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_9", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_9), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV6CurrentStep", wcpOAV6CurrentStep);
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPSAUX", AV19WizardStepsAux);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPSAUX", AV19WizardStepsAux);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSAUX", GetSecureSignedToken( sPrefix, AV19WizardStepsAux, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPREALNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV16StepRealNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV16StepRealNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15StepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSELECTEDSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13SelectedStepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV13SelectedStepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWIZARDSTEPSCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20WizardStepsCount), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFIRSTISDUMMY", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vLASTISDUMMY", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPS", AV18WizardSteps);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPS", AV18WizardSteps);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vPENULTIMATEISDUMMY", AV11PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV11PenultimateIsDummy, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vCURRENTSTEP", AV6CurrentStep);
         GxWebStd.gx_hidden_field( context, sPrefix+"subGridwizardsteps_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"GRIDWIZARDSTEPS_Class", StringUtil.RTrim( subGridwizardsteps_Class));
      }

      protected void RenderHtmlCloseForm0U2( )
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
            if ( nGXWrapped != 1 )
            {
               context.WriteHtmlTextNl( "</form>") ;
            }
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
         return "WWPBaseObjects.WizardStepsUnderlineWC" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Wizard Steps Underline WC", "") ;
      }

      protected void WB0U0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wwpbaseobjects.wizardstepsunderlinewc.aspx");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 WizardStepsUnderlineContainerCell", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridwizardstepsContainer.SetIsFreestyle(true);
            GridwizardstepsContainer.SetWrapped(nGXWrapped);
            StartGridControl9( ) ;
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            nRC_GXsfl_9 = (int)(nGXsfl_9_idx-1);
            if ( GridwizardstepsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+sPrefix+"GridwizardstepsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridwizardsteps", GridwizardstepsContainer, subGridwizardsteps_Internalname);
               if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridwizardstepsContainerData", GridwizardstepsContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, sPrefix+"GridwizardstepsContainerData"+"V", GridwizardstepsContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridwizardstepsContainerData"+"V"+"\" value='"+GridwizardstepsContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 CellPaddingBottom", "start", "top", "", "", "div");
            /* Text block */
            GxWebStd.gx_label_ctrl( context, lblWizardstepdescription_Internalname, lblWizardstepdescription_Caption, "", "", lblWizardstepdescription_Jsonclick, "'"+sPrefix+"'"+",false,"+"'"+""+"'", "", "WizardStepDescriptionUnderline", 0, "", lblWizardstepdescription_Visible, 1, 0, 0, "HLP_WWPBaseObjects/WizardStepsUnderlineWC.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 9 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridwizardstepsContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+sPrefix+"GridwizardstepsContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid(sPrefix+"_"+"Gridwizardsteps", GridwizardstepsContainer, subGridwizardsteps_Internalname);
                  if ( ! isAjaxCallMode( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridwizardstepsContainerData", GridwizardstepsContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, sPrefix+"GridwizardstepsContainerData"+"V", GridwizardstepsContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+sPrefix+"GridwizardstepsContainerData"+"V"+"\" value='"+GridwizardstepsContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START0U2( )
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
            Form.Meta.addItem("description", context.GetMessage( "Wizard Steps Underline WC", ""), 0) ;
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
               STRUP0U0( ) ;
            }
         }
      }

      protected void WS0U2( )
      {
         START0U2( ) ;
         EVT0U2( ) ;
      }

      protected void EVT0U2( )
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
                                 STRUP0U0( ) ;
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
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0U0( ) ;
                              }
                              if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                              {
                                 context.wbHandled = 1;
                                 if ( ! wbErr )
                                 {
                                    dynload_actions( ) ;
                                 }
                              }
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "GRIDWIZARDSTEPS.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) )
                           {
                              if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                              {
                                 STRUP0U0( ) ;
                              }
                              nGXsfl_9_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
                              SubsflControlProps_92( ) ;
                              AV22WizardStepTitle = cgiGet( edtavWizardsteptitle_Internalname);
                              AssignAttri(sPrefix, false, edtavWizardsteptitle_Internalname, AV22WizardStepTitle);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          /* Execute user event: Start */
                                          E110U2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDWIZARDSTEPS.LOAD") == 0 )
                                 {
                                    if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
                                    {
                                       context.wbHandled = 1;
                                       if ( ! wbErr )
                                       {
                                          dynload_actions( ) ;
                                          /* Execute user event: Gridwizardsteps.Load */
                                          E120U2 ();
                                       }
                                    }
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
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
                                             }
                                             dynload_actions( ) ;
                                          }
                                       }
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    if ( ( StringUtil.Len( sPrefix) != 0 ) && ( nDoneStart == 0 ) )
                                    {
                                       STRUP0U0( ) ;
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

      protected void WE0U2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm0U2( ) ;
            }
         }
      }

      protected void PA0U2( )
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
                  if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "wwpbaseobjects.wizardstepsunderlinewc.aspx")), "wwpbaseobjects.wizardstepsunderlinewc.aspx") == 0 ) )
                  {
                     SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "wwpbaseobjects.wizardstepsunderlinewc.aspx")))) ;
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
                     gxfirstwebparm = GetFirstPar( "WizardSteps");
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
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridwizardsteps_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_92( ) ;
         while ( nGXsfl_9_idx <= nRC_GXsfl_9 )
         {
            sendrow_92( ) ;
            nGXsfl_9_idx = ((subGridwizardsteps_Islastpage==1)&&(nGXsfl_9_idx+1>subGridwizardsteps_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
            sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
            SubsflControlProps_92( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridwizardstepsContainer)) ;
         /* End function gxnrGridwizardsteps_newrow */
      }

      protected void gxgrGridwizardsteps_refresh( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV19WizardStepsAux ,
                                                  short AV16StepRealNumber ,
                                                  short AV15StepNumber ,
                                                  short AV13SelectedStepNumber ,
                                                  short AV20WizardStepsCount ,
                                                  bool AV7FirstIsDummy ,
                                                  bool AV8LastIsDummy ,
                                                  GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV18WizardSteps ,
                                                  bool AV11PenultimateIsDummy ,
                                                  string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDWIZARDSTEPS_nCurrentRecord = 0;
         RF0U2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridwizardsteps_refresh */
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
         RF0U2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavWizardsteptitle_Enabled = 0;
      }

      protected void RF0U2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridwizardstepsContainer.ClearRows();
         }
         wbStart = 9;
         nGXsfl_9_idx = 1;
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         bGXsfl_9_Refreshing = true;
         GridwizardstepsContainer.AddObjectProperty("GridName", "Gridwizardsteps");
         GridwizardstepsContainer.AddObjectProperty("CmpContext", sPrefix);
         GridwizardstepsContainer.AddObjectProperty("InMasterPage", "false");
         GridwizardstepsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleStepsUnderline"));
         GridwizardstepsContainer.AddObjectProperty("Class", "FreeStyleStepsUnderline");
         GridwizardstepsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridwizardstepsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridwizardstepsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Backcolorstyle), 1, 0, ".", "")));
         GridwizardstepsContainer.PageSize = subGridwizardsteps_fnc_Recordsperpage( );
         if ( subGridwizardsteps_Islastpage != 0 )
         {
            GRIDWIZARDSTEPS_nFirstRecordOnPage = (long)(subGridwizardsteps_fnc_Recordcount( )-subGridwizardsteps_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, sPrefix+"GRIDWIZARDSTEPS_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRIDWIZARDSTEPS_nFirstRecordOnPage), 15, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("GRIDWIZARDSTEPS_nFirstRecordOnPage", GRIDWIZARDSTEPS_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_92( ) ;
            /* Execute user event: Gridwizardsteps.Load */
            E120U2 ();
            wbEnd = 9;
            WB0U0( ) ;
         }
         bGXsfl_9_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes0U2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPSAUX", AV19WizardStepsAux);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPSAUX", AV19WizardStepsAux);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSAUX", GetSecureSignedToken( sPrefix, AV19WizardStepsAux, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPREALNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV16StepRealNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV16StepRealNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15StepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSELECTEDSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV13SelectedStepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV13SelectedStepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWIZARDSTEPSCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20WizardStepsCount), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFIRSTISDUMMY", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vLASTISDUMMY", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vPENULTIMATEISDUMMY", AV11PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV11PenultimateIsDummy, context));
      }

      protected int subGridwizardsteps_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridwizardsteps_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridwizardsteps_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridwizardsteps_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavWizardsteptitle_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0U0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E110U2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            nRC_GXsfl_9 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_9"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            wcpOAV6CurrentStep = cgiGet( sPrefix+"wcpOAV6CurrentStep");
            subGridwizardsteps_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"subGridwizardsteps_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGridwizardsteps_Class = cgiGet( sPrefix+"GRIDWIZARDSTEPS_Class");
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
         E110U2 ();
         if (returnInSub) return;
      }

      protected void E110U2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV9MaxStepsToShow = 8;
         AV10MaxStepsToShowInXS = 5;
         AV13SelectedStepNumber = 1;
         AssignAttri(sPrefix, false, "AV13SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV13SelectedStepNumber), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV13SelectedStepNumber), "ZZZ9"), context));
         AV25GXV1 = 1;
         while ( AV25GXV1 <= AV18WizardSteps.Count )
         {
            AV5WizardStep = ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV18WizardSteps.Item(AV25GXV1));
            if ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Code, AV6CurrentStep) == 0 )
            {
               if ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Description, "") != 0 )
               {
                  lblWizardstepdescription_Caption = AV5WizardStep.gxTpr_Description;
                  AssignProp(sPrefix, false, lblWizardstepdescription_Internalname, "Caption", lblWizardstepdescription_Caption, true);
               }
               else
               {
                  lblWizardstepdescription_Visible = 0;
                  AssignProp(sPrefix, false, lblWizardstepdescription_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(lblWizardstepdescription_Visible), 5, 0), true);
               }
               if (true) break;
            }
            else
            {
               AV13SelectedStepNumber = (short)(AV13SelectedStepNumber+1);
               AssignAttri(sPrefix, false, "AV13SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV13SelectedStepNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV13SelectedStepNumber), "ZZZ9"), context));
            }
            AV25GXV1 = (int)(AV25GXV1+1);
         }
         AV16StepRealNumber = 1;
         AssignAttri(sPrefix, false, "AV16StepRealNumber", StringUtil.LTrimStr( (decimal)(AV16StepRealNumber), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV16StepRealNumber), "ZZZ9"), context));
         AV19WizardStepsAux = (GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)(AV18WizardSteps.Clone());
         AV7FirstIsDummy = false;
         AssignAttri(sPrefix, false, "AV7FirstIsDummy", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         AV12SecondIsDummy = false;
         AV11PenultimateIsDummy = false;
         AssignAttri(sPrefix, false, "AV11PenultimateIsDummy", AV11PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV11PenultimateIsDummy, context));
         AV8LastIsDummy = false;
         AssignAttri(sPrefix, false, "AV8LastIsDummy", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         if ( AV19WizardStepsAux.Count > AV9MaxStepsToShow )
         {
            if ( AV13SelectedStepNumber > AV19WizardStepsAux.Count )
            {
               AV13SelectedStepNumber = 1;
               AssignAttri(sPrefix, false, "AV13SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV13SelectedStepNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV13SelectedStepNumber), "ZZZ9"), context));
            }
            AV14StartIndex = 1;
            if ( ( AV13SelectedStepNumber + 3 - AV9MaxStepsToShow /  ( decimal )( 2 ) > Convert.ToDecimal( 0 )) )
            {
               AV14StartIndex = (short)(AV13SelectedStepNumber+3-AV9MaxStepsToShow/ (decimal)(2));
               if ( AV14StartIndex + ( AV9MaxStepsToShow - 2 ) > AV19WizardStepsAux.Count + 1 )
               {
                  AV14StartIndex = (short)(AV19WizardStepsAux.Count-(AV9MaxStepsToShow-2)+1);
               }
            }
            if ( AV14StartIndex > 3 )
            {
               AV16StepRealNumber = AV14StartIndex;
               AssignAttri(sPrefix, false, "AV16StepRealNumber", StringUtil.LTrimStr( (decimal)(AV16StepRealNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV16StepRealNumber), "ZZZ9"), context));
               AV7FirstIsDummy = true;
               AssignAttri(sPrefix, false, "AV7FirstIsDummy", AV7FirstIsDummy);
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
               AV12SecondIsDummy = true;
               ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV19WizardStepsAux.Item(2)).gxTpr_Title = "...";
               while ( AV14StartIndex > 3 )
               {
                  AV19WizardStepsAux.RemoveItem(3);
                  AV14StartIndex = (short)(AV14StartIndex-1);
                  AV13SelectedStepNumber = (short)(AV13SelectedStepNumber-1);
                  AssignAttri(sPrefix, false, "AV13SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV13SelectedStepNumber), 4, 0));
                  GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV13SelectedStepNumber), "ZZZ9"), context));
               }
            }
            if ( AV19WizardStepsAux.Count > AV9MaxStepsToShow )
            {
               AV8LastIsDummy = true;
               AssignAttri(sPrefix, false, "AV8LastIsDummy", AV8LastIsDummy);
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
               AV11PenultimateIsDummy = true;
               AssignAttri(sPrefix, false, "AV11PenultimateIsDummy", AV11PenultimateIsDummy);
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV11PenultimateIsDummy, context));
               ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV19WizardStepsAux.Item(AV19WizardStepsAux.Count-1)).gxTpr_Title = "";
               while ( AV19WizardStepsAux.Count > AV9MaxStepsToShow )
               {
                  AV19WizardStepsAux.RemoveItem(AV19WizardStepsAux.Count-2);
               }
            }
         }
         AV20WizardStepsCount = (short)(AV19WizardStepsAux.Count);
         AssignAttri(sPrefix, false, "AV20WizardStepsCount", StringUtil.LTrimStr( (decimal)(AV20WizardStepsCount), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         if ( AV19WizardStepsAux.Count > AV10MaxStepsToShowInXS )
         {
            AV21WizardStepsItem = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
            AV21WizardStepsItem.gxTpr_Title = "DummiesXS_Test";
            AV21WizardStepsItem.gxTpr_Code = "FirstDummyXS";
            AV19WizardStepsAux.Add(AV21WizardStepsItem, 2);
            AV21WizardStepsItem = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
            AV21WizardStepsItem.gxTpr_Title = "DummiesXS_Test";
            AV21WizardStepsItem.gxTpr_Code = "LastDummyXS";
            AV19WizardStepsAux.Add(AV21WizardStepsItem, AV19WizardStepsAux.Count-1);
         }
         AV15StepNumber = 1;
         AssignAttri(sPrefix, false, "AV15StepNumber", StringUtil.LTrimStr( (decimal)(AV15StepNumber), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepNumber), "ZZZ9"), context));
      }

      private void E120U2( )
      {
         /* Gridwizardsteps_Load Routine */
         returnInSub = false;
         AV26GXV2 = 1;
         while ( AV26GXV2 <= AV19WizardStepsAux.Count )
         {
            AV5WizardStep = ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV19WizardStepsAux.Item(AV26GXV2));
            AV22WizardStepTitle = AV5WizardStep.gxTpr_Title;
            AssignAttri(sPrefix, false, edtavWizardsteptitle_Internalname, AV22WizardStepTitle);
            lblStepnumber_Caption = context.localUtil.Format( (decimal)(AV16StepRealNumber), "ZZZ9");
            divTblcontainerstep_Class = "TableContainerStepUnderline";
            AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Class", divTblcontainerstep_Class, !bGXsfl_9_Refreshing);
            divTablestepitem_Class = "TableStepUnderline";
            AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_9_Refreshing);
            divTablestepitem_Visible = 1;
            AssignProp(sPrefix, false, divTablestepitem_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestepitem_Visible), 5, 0), !bGXsfl_9_Refreshing);
            if ( AV15StepNumber < AV13SelectedStepNumber )
            {
               divUnnamedtablefsgridwizardsteps_Class = "Table CompletedStep";
               AssignProp(sPrefix, false, divUnnamedtablefsgridwizardsteps_Internalname, "Class", divUnnamedtablefsgridwizardsteps_Class, !bGXsfl_9_Refreshing);
            }
            else if ( AV15StepNumber == AV13SelectedStepNumber )
            {
               divUnnamedtablefsgridwizardsteps_Class = "Table CurrentStep";
               AssignProp(sPrefix, false, divUnnamedtablefsgridwizardsteps_Internalname, "Class", divUnnamedtablefsgridwizardsteps_Class, !bGXsfl_9_Refreshing);
            }
            else
            {
               divUnnamedtablefsgridwizardsteps_Class = "Table RemainingStep";
               AssignProp(sPrefix, false, divUnnamedtablefsgridwizardsteps_Internalname, "Class", divUnnamedtablefsgridwizardsteps_Class, !bGXsfl_9_Refreshing);
            }
            if ( ( AV13SelectedStepNumber != AV15StepNumber ) && ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Code, "FirstDummyXS") != 0 ) && ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Code, "LastDummyXS") != 0 ) && ( AV15StepNumber > 1 ) && ( AV15StepNumber < AV20WizardStepsCount ) )
            {
               if ( ( AV13SelectedStepNumber <= 3 ) && ( AV15StepNumber > 3 ) )
               {
                  divTblcontainerstep_Class = divTblcontainerstep_Class+" hidden-xs";
                  AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Class", divTblcontainerstep_Class, !bGXsfl_9_Refreshing);
               }
               if ( ( AV13SelectedStepNumber > 3 ) && ( AV15StepNumber > 1 ) && ( AV13SelectedStepNumber < AV20WizardStepsCount - 2 ) )
               {
                  divTblcontainerstep_Class = divTblcontainerstep_Class+" hidden-xs";
                  AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Class", divTblcontainerstep_Class, !bGXsfl_9_Refreshing);
               }
               if ( ( AV13SelectedStepNumber >= AV20WizardStepsCount - 2 ) && ( AV15StepNumber < AV20WizardStepsCount - 2 ) )
               {
                  divTblcontainerstep_Class = divTblcontainerstep_Class+" hidden-xs";
                  AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Class", divTblcontainerstep_Class, !bGXsfl_9_Refreshing);
               }
            }
            divTblcontainerstep_Visible = 1;
            AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblcontainerstep_Visible), 5, 0), !bGXsfl_9_Refreshing);
            if ( ! StringUtil.Contains( AV5WizardStep.gxTpr_Code, "DummyXS") )
            {
               if ( ( AV15StepNumber == 1 ) && AV7FirstIsDummy )
               {
                  lblStepnumber_Caption = StringUtil.Trim( StringUtil.Str( (decimal)(AV15StepNumber), 4, 0));
               }
               else if ( ( AV15StepNumber == AV20WizardStepsCount ) && AV8LastIsDummy )
               {
                  lblStepnumber_Caption = StringUtil.Trim( StringUtil.Str( (decimal)(AV18WizardSteps.Count), 9, 0));
               }
               else if ( ( AV15StepNumber == AV20WizardStepsCount - 1 ) && AV11PenultimateIsDummy )
               {
                  lblStepnumber_Caption = "...";
                  divTablestepitem_Class = divTablestepitem_Class+" TableStepUnderlineExtraBullet";
                  AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_9_Refreshing);
               }
               else if ( ( AV15StepNumber == 2 ) && AV7FirstIsDummy )
               {
                  divTablestepitem_Visible = 0;
                  AssignProp(sPrefix, false, divTablestepitem_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTablestepitem_Visible), 5, 0), !bGXsfl_9_Refreshing);
                  divTblcontainerstep_Class = divTblcontainerstep_Class+context.GetMessage( " TableContainerUnderlineExtraBullet", "");
                  AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Class", divTblcontainerstep_Class, !bGXsfl_9_Refreshing);
               }
               else
               {
                  AV16StepRealNumber = (short)(AV16StepRealNumber+1);
                  AssignAttri(sPrefix, false, "AV16StepRealNumber", StringUtil.LTrimStr( (decimal)(AV16StepRealNumber), 4, 0));
                  GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV16StepRealNumber), "ZZZ9"), context));
               }
               AV15StepNumber = (short)(AV15StepNumber+1);
               AssignAttri(sPrefix, false, "AV15StepNumber", StringUtil.LTrimStr( (decimal)(AV15StepNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepNumber), "ZZZ9"), context));
            }
            else
            {
               lblStepnumber_Caption = "...";
               divTblcontainerstep_Class = divTblcontainerstep_Class+context.GetMessage( " TableContainerUnderlineExtraBullet hidden-sm hidden-lg hidden-md", "");
               AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Class", divTblcontainerstep_Class, !bGXsfl_9_Refreshing);
               if ( ( ( AV13SelectedStepNumber <= 3 ) && ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Code, "FirstDummyXS") == 0 ) ) || ( ( AV13SelectedStepNumber >= AV20WizardStepsCount - 2 ) && ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Code, "LastDummyXS") == 0 ) ) )
               {
                  divTblcontainerstep_Visible = 0;
                  AssignProp(sPrefix, false, divTblcontainerstep_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(divTblcontainerstep_Visible), 5, 0), !bGXsfl_9_Refreshing);
                  divTablestepitem_Class = "TableStepUnderlineExtra";
                  AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_9_Refreshing);
               }
               if ( ( AV13SelectedStepNumber > 3 ) && ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Code, "FirstDummyXS") == 0 ) )
               {
                  divTablestepitem_Class = "TableStepExtraUnderlineChecked";
                  AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_9_Refreshing);
                  lblStepnumber_Visible = 1;
               }
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 9;
            }
            sendrow_92( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_9_Refreshing )
            {
               DoAjaxLoad(9, GridwizardstepsRow);
            }
            AV26GXV2 = (int)(AV26GXV2+1);
         }
         /*  Sending Event outputs  */
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV18WizardSteps = (GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)getParm(obj,0);
         AV6CurrentStep = (string)getParm(obj,1);
         AssignAttri(sPrefix, false, "AV6CurrentStep", AV6CurrentStep);
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
         PA0U2( ) ;
         WS0U2( ) ;
         WE0U2( ) ;
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
         sCtrlAV18WizardSteps = (string)((string)getParm(obj,0));
         sCtrlAV6CurrentStep = (string)((string)getParm(obj,1));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA0U2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wwpbaseobjects\\wizardstepsunderlinewc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA0U2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV18WizardSteps = (GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)getParm(obj,2);
            AV6CurrentStep = (string)getParm(obj,3);
            AssignAttri(sPrefix, false, "AV6CurrentStep", AV6CurrentStep);
         }
         wcpOAV6CurrentStep = cgiGet( sPrefix+"wcpOAV6CurrentStep");
         if ( ! GetJustCreated( ) && ( ( StringUtil.StrCmp(AV6CurrentStep, wcpOAV6CurrentStep) != 0 ) ) )
         {
            setjustcreated();
         }
         wcpOAV6CurrentStep = AV6CurrentStep;
      }

      protected void WCParametersGet( )
      {
         /* Read Component Parameters. */
         sCtrlAV18WizardSteps = cgiGet( sPrefix+"AV18WizardSteps_CTRL");
         if ( StringUtil.Len( sCtrlAV18WizardSteps) > 0 )
         {
            AV18WizardSteps = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>();
         }
         else
         {
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"AV18WizardSteps_PARM"), AV18WizardSteps);
         }
         sCtrlAV6CurrentStep = cgiGet( sPrefix+"AV6CurrentStep_CTRL");
         if ( StringUtil.Len( sCtrlAV6CurrentStep) > 0 )
         {
            AV6CurrentStep = cgiGet( sCtrlAV6CurrentStep);
            AssignAttri(sPrefix, false, "AV6CurrentStep", AV6CurrentStep);
         }
         else
         {
            AV6CurrentStep = cgiGet( sPrefix+"AV6CurrentStep_PARM");
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
         PA0U2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS0U2( ) ;
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
         WS0U2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"AV18WizardSteps_PARM", AV18WizardSteps);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"AV18WizardSteps_PARM", AV18WizardSteps);
         }
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV18WizardSteps)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV18WizardSteps_CTRL", StringUtil.RTrim( sCtrlAV18WizardSteps));
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"AV6CurrentStep_PARM", AV6CurrentStep);
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV6CurrentStep)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV6CurrentStep_CTRL", StringUtil.RTrim( sCtrlAV6CurrentStep));
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
         WE0U2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202412162336846", true, true);
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
         if ( nGXWrapped != 1 )
         {
            context.AddJavascriptSource("wwpbaseobjects/wizardstepsunderlinewc.js", "?202412162336847", false, true);
         }
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_92( )
      {
         lblStepnumber_Internalname = sPrefix+"STEPNUMBER_"+sGXsfl_9_idx;
         edtavWizardsteptitle_Internalname = sPrefix+"vWIZARDSTEPTITLE_"+sGXsfl_9_idx;
      }

      protected void SubsflControlProps_fel_92( )
      {
         lblStepnumber_Internalname = sPrefix+"STEPNUMBER_"+sGXsfl_9_fel_idx;
         edtavWizardsteptitle_Internalname = sPrefix+"vWIZARDSTEPTITLE_"+sGXsfl_9_fel_idx;
      }

      protected void sendrow_92( )
      {
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         WB0U0( ) ;
         GridwizardstepsRow = GXWebRow.GetNew(context,GridwizardstepsContainer);
         if ( subGridwizardsteps_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridwizardsteps_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridwizardsteps_Class, "") != 0 )
            {
               subGridwizardsteps_Linesclass = subGridwizardsteps_Class+"Odd";
            }
         }
         else if ( subGridwizardsteps_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridwizardsteps_Backstyle = 0;
            subGridwizardsteps_Backcolor = subGridwizardsteps_Allbackcolor;
            if ( StringUtil.StrCmp(subGridwizardsteps_Class, "") != 0 )
            {
               subGridwizardsteps_Linesclass = subGridwizardsteps_Class+"Uniform";
            }
         }
         else if ( subGridwizardsteps_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridwizardsteps_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridwizardsteps_Class, "") != 0 )
            {
               subGridwizardsteps_Linesclass = subGridwizardsteps_Class+"Odd";
            }
            subGridwizardsteps_Backcolor = (int)(0xFFFFFF);
         }
         else if ( subGridwizardsteps_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridwizardsteps_Backstyle = 1;
            subGridwizardsteps_Backcolor = (int)(0xFFFFFF);
            if ( StringUtil.StrCmp(subGridwizardsteps_Class, "") != 0 )
            {
               subGridwizardsteps_Linesclass = subGridwizardsteps_Class+"Odd";
            }
         }
         /* Start of Columns property logic. */
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divUnnamedtablefsgridwizardsteps_Internalname+"_"+sGXsfl_9_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)divUnnamedtablefsgridwizardsteps_Class,(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divTblcontainerstep_Internalname+"_"+sGXsfl_9_idx,(int)divTblcontainerstep_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)divTblcontainerstep_Class,(string)"start",(string)"top",(string)" "+"data-gx-flex"+" ",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divTablestepitem_Internalname+"_"+sGXsfl_9_idx,(int)divTablestepitem_Visible,(short)0,(string)"px",(short)0,(string)"px",(string)divTablestepitem_Class,(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 StepNumberUnderlineCell",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Text block */
         GridwizardstepsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblStepnumber_Internalname,(string)lblStepnumber_Caption,(string)"",(string)"",(string)lblStepnumber_Jsonclick,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"StepNumberUnderline",(short)0,(string)"",(int)lblStepnumber_Visible,(short)1,(short)0,(short)0});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"AttributeStepBulletCell hidden-xs",(string)"start",(string)"top",(string)"",(string)"flex-grow:1;",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)" gx-attribute",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Attribute/Variable Label */
         GridwizardstepsRow.AddColumnProperties("html_label", -1, isAjaxCallMode( ), new Object[] {(string)edtavWizardsteptitle_Internalname,context.GetMessage( "Wizard Step Title", ""),(string)"gx-form-item AttributeStepUnderlineLabel",(short)0,(bool)true,(string)"width: 25%;"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         /* Multiple line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 21,'" + sPrefix + "',false,'" + sGXsfl_9_idx + "',9)\"";
         ClassString = "AttributeStepUnderline";
         StyleString = "";
         ClassString = "AttributeStepUnderline";
         StyleString = "";
         GridwizardstepsRow.AddColumnProperties("html_textarea", 1, isAjaxCallMode( ), new Object[] {(string)edtavWizardsteptitle_Internalname,(string)AV22WizardStepTitle,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,21);\"",(short)0,(short)1,(int)edtavWizardsteptitle_Enabled,(short)0,(short)80,(string)"chr",(short)3,(string)"row",(short)0,(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"200",(short)-1,(short)0,(string)"",(string)"",(short)-1,(bool)false,(string)"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(short)0,(string)""});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
         GridwizardstepsRow.AddRenderProperties(GridwizardstepsColumn);
         send_integrity_lvl_hashes0U2( ) ;
         /* End of Columns property logic. */
         GridwizardstepsContainer.AddRow(GridwizardstepsRow);
         nGXsfl_9_idx = ((subGridwizardsteps_Islastpage==1)&&(nGXsfl_9_idx+1>subGridwizardsteps_fnc_Recordsperpage( )) ? 1 : nGXsfl_9_idx+1);
         sGXsfl_9_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_9_idx), 4, 0), 4, "0");
         SubsflControlProps_92( ) ;
         /* End function sendrow_92 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl9( )
      {
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridwizardstepsContainer"+"DivS\" data-gxgridid=\"9\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridwizardsteps_Internalname, subGridwizardsteps_Internalname, "", "FreeStyleStepsUnderline", 0, "", "", 1, 2, sStyleString, "", "", 0);
            GridwizardstepsContainer.AddObjectProperty("GridName", "Gridwizardsteps");
         }
         else
         {
            GridwizardstepsContainer.AddObjectProperty("GridName", "Gridwizardsteps");
            GridwizardstepsContainer.AddObjectProperty("Header", subGridwizardsteps_Header);
            GridwizardstepsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleStepsUnderline"));
            GridwizardstepsContainer.AddObjectProperty("Class", "FreeStyleStepsUnderline");
            GridwizardstepsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Backcolorstyle), 1, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("CmpContext", sPrefix);
            GridwizardstepsContainer.AddObjectProperty("InMasterPage", "false");
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsColumn.AddObjectProperty("Value", lblStepnumber_Caption);
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV22WizardStepTitle));
            GridwizardstepsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavWizardsteptitle_Enabled), 5, 0, ".", "")));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Selectedindex), 4, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Allowselection), 1, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Selectioncolor), 9, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Allowhovering), 1, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Hoveringcolor), 9, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Allowcollapsing), 1, 0, ".", "")));
            GridwizardstepsContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         lblStepnumber_Internalname = sPrefix+"STEPNUMBER";
         divTablestepitem_Internalname = sPrefix+"TABLESTEPITEM";
         edtavWizardsteptitle_Internalname = sPrefix+"vWIZARDSTEPTITLE";
         divTblcontainerstep_Internalname = sPrefix+"TBLCONTAINERSTEP";
         divUnnamedtablefsgridwizardsteps_Internalname = sPrefix+"UNNAMEDTABLEFSGRIDWIZARDSTEPS";
         lblWizardstepdescription_Internalname = sPrefix+"WIZARDSTEPDESCRIPTION";
         divTablemain_Internalname = sPrefix+"TABLEMAIN";
         divLayoutmaintable_Internalname = sPrefix+"LAYOUTMAINTABLE";
         Form.Internalname = sPrefix+"FORM";
         subGridwizardsteps_Internalname = sPrefix+"GRIDWIZARDSTEPS";
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
         subGridwizardsteps_Allowcollapsing = 0;
         lblStepnumber_Caption = "1";
         edtavWizardsteptitle_Enabled = 0;
         lblStepnumber_Caption = "1";
         lblStepnumber_Visible = 1;
         divTablestepitem_Visible = 1;
         divTblcontainerstep_Visible = 1;
         divUnnamedtablefsgridwizardsteps_Class = "Table";
         divTablestepitem_Class = "TableStepUnderline";
         divTblcontainerstep_Class = "TableContainerStepUnderline";
         subGridwizardsteps_Backcolorstyle = 0;
         lblWizardstepdescription_Caption = context.GetMessage( "Step Description", "");
         lblWizardstepdescription_Visible = 1;
         subGridwizardsteps_Class = "FreeStyleStepsUnderline";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDWIZARDSTEPS_nFirstRecordOnPage"},{"av":"GRIDWIZARDSTEPS_nEOF"},{"av":"AV18WizardSteps","fld":"vWIZARDSTEPS"},{"av":"sPrefix"},{"av":"AV19WizardStepsAux","fld":"vWIZARDSTEPSAUX","hsh":true},{"av":"AV16StepRealNumber","fld":"vSTEPREALNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV15StepNumber","fld":"vSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV13SelectedStepNumber","fld":"vSELECTEDSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV20WizardStepsCount","fld":"vWIZARDSTEPSCOUNT","pic":"ZZZ9","hsh":true},{"av":"AV7FirstIsDummy","fld":"vFIRSTISDUMMY","hsh":true},{"av":"AV8LastIsDummy","fld":"vLASTISDUMMY","hsh":true},{"av":"AV11PenultimateIsDummy","fld":"vPENULTIMATEISDUMMY","hsh":true}]}""");
         setEventMetadata("GRIDWIZARDSTEPS.LOAD","""{"handler":"E120U2","iparms":[{"av":"AV19WizardStepsAux","fld":"vWIZARDSTEPSAUX","hsh":true},{"av":"AV16StepRealNumber","fld":"vSTEPREALNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV15StepNumber","fld":"vSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV13SelectedStepNumber","fld":"vSELECTEDSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV20WizardStepsCount","fld":"vWIZARDSTEPSCOUNT","pic":"ZZZ9","hsh":true},{"av":"AV7FirstIsDummy","fld":"vFIRSTISDUMMY","hsh":true},{"av":"AV8LastIsDummy","fld":"vLASTISDUMMY","hsh":true},{"av":"AV18WizardSteps","fld":"vWIZARDSTEPS"},{"av":"AV11PenultimateIsDummy","fld":"vPENULTIMATEISDUMMY","hsh":true}]""");
         setEventMetadata("GRIDWIZARDSTEPS.LOAD",""","oparms":[{"av":"AV22WizardStepTitle","fld":"vWIZARDSTEPTITLE"},{"av":"lblStepnumber_Caption","ctrl":"STEPNUMBER","prop":"Caption"},{"av":"divTblcontainerstep_Class","ctrl":"TBLCONTAINERSTEP","prop":"Class"},{"av":"divTablestepitem_Class","ctrl":"TABLESTEPITEM","prop":"Class"},{"av":"divTablestepitem_Visible","ctrl":"TABLESTEPITEM","prop":"Visible"},{"av":"divUnnamedtablefsgridwizardsteps_Class","ctrl":"UNNAMEDTABLEFSGRIDWIZARDSTEPS","prop":"Class"},{"av":"divTblcontainerstep_Visible","ctrl":"TBLCONTAINERSTEP","prop":"Visible"},{"av":"AV16StepRealNumber","fld":"vSTEPREALNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV15StepNumber","fld":"vSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"lblStepnumber_Visible","ctrl":"STEPNUMBER","prop":"Visible"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Wizardsteptitle","iparms":[]}""");
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
         AV18WizardSteps = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>( context, "WizardStepsItem", "DesignSystem");
         wcpOAV6CurrentStep = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV19WizardStepsAux = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>( context, "WizardStepsItem", "DesignSystem");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         GX_FocusControl = "";
         GridwizardstepsContainer = new GXWebGrid( context);
         sStyleString = "";
         lblWizardstepdescription_Jsonclick = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV22WizardStepTitle = "";
         GXDecQS = "";
         AV5WizardStep = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
         AV21WizardStepsItem = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
         GridwizardstepsRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV18WizardSteps = "";
         sCtrlAV6CurrentStep = "";
         subGridwizardsteps_Linesclass = "";
         GridwizardstepsColumn = new GXWebColumn();
         lblStepnumber_Jsonclick = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         subGridwizardsteps_Header = "";
         /* GeneXus formulas. */
         edtavWizardsteptitle_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short AV16StepRealNumber ;
      private short AV15StepNumber ;
      private short AV13SelectedStepNumber ;
      private short AV20WizardStepsCount ;
      private short nGXWrapped ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short subGridwizardsteps_Backcolorstyle ;
      private short AV9MaxStepsToShow ;
      private short AV10MaxStepsToShowInXS ;
      private short AV14StartIndex ;
      private short subGridwizardsteps_Backstyle ;
      private short subGridwizardsteps_Allowselection ;
      private short subGridwizardsteps_Allowhovering ;
      private short subGridwizardsteps_Allowcollapsing ;
      private short subGridwizardsteps_Collapsed ;
      private short GRIDWIZARDSTEPS_nEOF ;
      private int nRC_GXsfl_9 ;
      private int subGridwizardsteps_Recordcount ;
      private int nGXsfl_9_idx=1 ;
      private int edtavWizardsteptitle_Enabled ;
      private int lblWizardstepdescription_Visible ;
      private int subGridwizardsteps_Islastpage ;
      private int AV25GXV1 ;
      private int AV26GXV2 ;
      private int divTablestepitem_Visible ;
      private int divTblcontainerstep_Visible ;
      private int lblStepnumber_Visible ;
      private int idxLst ;
      private int subGridwizardsteps_Backcolor ;
      private int subGridwizardsteps_Allbackcolor ;
      private int subGridwizardsteps_Selectedindex ;
      private int subGridwizardsteps_Selectioncolor ;
      private int subGridwizardsteps_Hoveringcolor ;
      private long GRIDWIZARDSTEPS_nCurrentRecord ;
      private long GRIDWIZARDSTEPS_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sPrefix ;
      private string sCompPrefix ;
      private string sSFPrefix ;
      private string sGXsfl_9_idx="0001" ;
      private string edtavWizardsteptitle_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string subGridwizardsteps_Class ;
      private string GX_FocusControl ;
      private string divLayoutmaintable_Internalname ;
      private string divTablemain_Internalname ;
      private string sStyleString ;
      private string subGridwizardsteps_Internalname ;
      private string lblWizardstepdescription_Internalname ;
      private string lblWizardstepdescription_Caption ;
      private string lblWizardstepdescription_Jsonclick ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string lblStepnumber_Caption ;
      private string divTblcontainerstep_Class ;
      private string divTblcontainerstep_Internalname ;
      private string divTablestepitem_Class ;
      private string divTablestepitem_Internalname ;
      private string divUnnamedtablefsgridwizardsteps_Class ;
      private string divUnnamedtablefsgridwizardsteps_Internalname ;
      private string sCtrlAV18WizardSteps ;
      private string sCtrlAV6CurrentStep ;
      private string lblStepnumber_Internalname ;
      private string sGXsfl_9_fel_idx="0001" ;
      private string subGridwizardsteps_Linesclass ;
      private string lblStepnumber_Jsonclick ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string subGridwizardsteps_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV7FirstIsDummy ;
      private bool AV8LastIsDummy ;
      private bool AV11PenultimateIsDummy ;
      private bool bGXsfl_9_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool AV12SecondIsDummy ;
      private string AV6CurrentStep ;
      private string wcpOAV6CurrentStep ;
      private string AV22WizardStepTitle ;
      private GXWebGrid GridwizardstepsContainer ;
      private GXWebRow GridwizardstepsRow ;
      private GXWebColumn GridwizardstepsColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV18WizardSteps ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV19WizardStepsAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem AV5WizardStep ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem AV21WizardStepsItem ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
