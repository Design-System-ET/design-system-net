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
   public class wizardstepsbulletwc : GXWebComponent
   {
      public wizardstepsbulletwc( )
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

      public wizardstepsbulletwc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> aP0_WizardSteps ,
                           string aP1_CurrentStep )
      {
         this.AV17WizardSteps = aP0_WizardSteps;
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
                  ajax_req_read_hidden_sdt(GetNextPar( ), AV17WizardSteps);
                  AV6CurrentStep = GetPar( "CurrentStep");
                  AssignAttri(sPrefix, false, "AV6CurrentStep", AV6CurrentStep);
                  setjustcreated();
                  componentprepare(new Object[] {(string)sCompPrefix,(string)sSFPrefix,(GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)AV17WizardSteps,(string)AV6CurrentStep});
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
         nRC_GXsfl_5 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_5"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_5_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_5_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_5_idx = GetPar( "sGXsfl_5_idx");
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
         AV15StepRealNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "StepRealNumber"), "."), 18, MidpointRounding.ToEven));
         AV12SelectedStepNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "SelectedStepNumber"), "."), 18, MidpointRounding.ToEven));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV18WizardStepsAux);
         AV14StepNumber = (short)(Math.Round(NumberUtil.Val( GetPar( "StepNumber"), "."), 18, MidpointRounding.ToEven));
         AV20WizardStepsCount = (short)(Math.Round(NumberUtil.Val( GetPar( "WizardStepsCount"), "."), 18, MidpointRounding.ToEven));
         AV7FirstIsDummy = StringUtil.StrToBool( GetPar( "FirstIsDummy"));
         AV8LastIsDummy = StringUtil.StrToBool( GetPar( "LastIsDummy"));
         ajax_req_read_hidden_sdt(GetNextPar( ), AV17WizardSteps);
         AV10PenultimateIsDummy = StringUtil.StrToBool( GetPar( "PenultimateIsDummy"));
         sPrefix = GetPar( "sPrefix");
         init_default_properties( ) ;
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridwizardsteps_refresh( AV15StepRealNumber, AV12SelectedStepNumber, AV18WizardStepsAux, AV14StepNumber, AV20WizardStepsCount, AV7FirstIsDummy, AV8LastIsDummy, AV17WizardSteps, AV10PenultimateIsDummy, sPrefix) ;
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
            PA0M2( ) ;
            if ( ( GxWebError == 0 ) && ! isAjaxCallMode( ) )
            {
               /* GeneXus formulas. */
               edtavWizardstepsaux__title_Enabled = 0;
               AssignProp(sPrefix, false, edtavWizardstepsaux__title_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavWizardstepsaux__title_Enabled), 5, 0), !bGXsfl_5_Refreshing);
               WS0M2( ) ;
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
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, false);
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
            context.SendWebValue( context.GetMessage( "Wizard Steps Bullet WC.", "")) ;
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
            context.WriteHtmlText( " "+"class=\"Form\""+" "+ "style='"+bodyStyle+"'") ;
            context.WriteHtmlText( FormProcess+">") ;
            context.skipLines(1);
            if ( nGXWrapped != 1 )
            {
               GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
               GXEncryptionTmp = "wwpbaseobjects.wizardstepsbulletwc.aspx"+UrlEncode(StringUtil.RTrim(AV6CurrentStep));
               context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"Form\" data-gx-class=\"Form\" novalidate action=\""+formatLink("wwpbaseobjects.wizardstepsbulletwc.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
               GxWebStd.gx_hidden_field( context, "_EventName", "");
               GxWebStd.gx_hidden_field( context, "_EventGridId", "");
               GxWebStd.gx_hidden_field( context, "_EventRowId", "");
               context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
               AssignProp(sPrefix, false, "FORM", "Class", "Form", true);
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
            GxWebStd.ClassAttribute( context, "gxwebcomponent-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "Form" : Form.Class)+"-fx");
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
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPREALNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15StepRealNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepRealNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSELECTEDSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV12SelectedStepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV12SelectedStepNumber), "ZZZ9"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPSAUX", AV18WizardStepsAux);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPSAUX", AV18WizardStepsAux);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSAUX", GetSecureSignedToken( sPrefix, AV18WizardStepsAux, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV14StepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV14StepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWIZARDSTEPSCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20WizardStepsCount), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFIRSTISDUMMY", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vLASTISDUMMY", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vPENULTIMATEISDUMMY", AV10PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV10PenultimateIsDummy, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"Wizardstepsaux", AV18WizardStepsAux);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"Wizardstepsaux", AV18WizardStepsAux);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_Wizardstepsaux", GetSecureSignedToken( sPrefix, AV18WizardStepsAux, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"nRC_GXsfl_5", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_5), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"wcpOAV6CurrentStep", wcpOAV6CurrentStep);
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPREALNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15StepRealNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepRealNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSELECTEDSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV12SelectedStepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV12SelectedStepNumber), "ZZZ9"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPSAUX", AV18WizardStepsAux);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPSAUX", AV18WizardStepsAux);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSAUX", GetSecureSignedToken( sPrefix, AV18WizardStepsAux, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV14StepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV14StepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWIZARDSTEPSCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20WizardStepsCount), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFIRSTISDUMMY", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vLASTISDUMMY", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPS", AV17WizardSteps);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPS", AV17WizardSteps);
         }
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vPENULTIMATEISDUMMY", AV10PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV10PenultimateIsDummy, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vCURRENTSTEP", AV6CurrentStep);
      }

      protected void RenderHtmlCloseForm0M2( )
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
         return "WWPBaseObjects.WizardStepsBulletWC" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "Wizard Steps Bullet WC.", "") ;
      }

      protected void WB0M0( )
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
               GxWebStd.gx_hidden_field( context, sPrefix+"_CMPPGM", "wwpbaseobjects.wizardstepsbulletwc.aspx");
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", sPrefix, "false");
            wb_table1_2_0M2( true) ;
         }
         else
         {
            wb_table1_2_0M2( false) ;
         }
         return  ;
      }

      protected void wb_table1_2_0M2e( bool wbgen )
      {
         if ( wbgen )
         {
         }
         if ( wbEnd == 5 )
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
                  AV24GXV1 = nGXsfl_5_idx;
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

      protected void START0M2( )
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
            Form.Meta.addItem("description", context.GetMessage( "Wizard Steps Bullet WC.", ""), 0) ;
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
               STRUP0M0( ) ;
            }
         }
      }

      protected void WS0M2( )
      {
         START0M2( ) ;
         EVT0M2( ) ;
      }

      protected void EVT0M2( )
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
                                 STRUP0M0( ) ;
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
                                 STRUP0M0( ) ;
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
                                 STRUP0M0( ) ;
                              }
                              nGXsfl_5_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_5_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_5_idx), 4, 0), 4, "0");
                              SubsflControlProps_52( ) ;
                              AV24GXV1 = nGXsfl_5_idx;
                              if ( ( AV18WizardStepsAux.Count >= AV24GXV1 ) && ( AV24GXV1 > 0 ) )
                              {
                                 AV18WizardStepsAux.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV18WizardStepsAux.Item(AV24GXV1));
                              }
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
                                          E110M2 ();
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
                                          E120M2 ();
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
                                       STRUP0M0( ) ;
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

      protected void WE0M2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               RenderHtmlCloseForm0M2( ) ;
            }
         }
      }

      protected void PA0M2( )
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
                  if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "wwpbaseobjects.wizardstepsbulletwc.aspx")), "wwpbaseobjects.wizardstepsbulletwc.aspx") == 0 ) )
                  {
                     SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "wwpbaseobjects.wizardstepsbulletwc.aspx")))) ;
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
         SubsflControlProps_52( ) ;
         while ( nGXsfl_5_idx <= nRC_GXsfl_5 )
         {
            sendrow_52( ) ;
            nGXsfl_5_idx = ((subGridwizardsteps_Islastpage==1)&&(nGXsfl_5_idx+1>subGridwizardsteps_fnc_Recordsperpage( )) ? 1 : nGXsfl_5_idx+1);
            sGXsfl_5_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_5_idx), 4, 0), 4, "0");
            SubsflControlProps_52( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridwizardstepsContainer)) ;
         /* End function gxnrGridwizardsteps_newrow */
      }

      protected void gxgrGridwizardsteps_refresh( short AV15StepRealNumber ,
                                                  short AV12SelectedStepNumber ,
                                                  GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV18WizardStepsAux ,
                                                  short AV14StepNumber ,
                                                  short AV20WizardStepsCount ,
                                                  bool AV7FirstIsDummy ,
                                                  bool AV8LastIsDummy ,
                                                  GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV17WizardSteps ,
                                                  bool AV10PenultimateIsDummy ,
                                                  string sPrefix )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDWIZARDSTEPS_nCurrentRecord = 0;
         RF0M2( ) ;
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
         RF0M2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavWizardstepsaux__title_Enabled = 0;
      }

      protected void RF0M2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridwizardstepsContainer.ClearRows();
         }
         wbStart = 5;
         nGXsfl_5_idx = 1;
         sGXsfl_5_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_5_idx), 4, 0), 4, "0");
         SubsflControlProps_52( ) ;
         bGXsfl_5_Refreshing = true;
         GridwizardstepsContainer.AddObjectProperty("GridName", "Gridwizardsteps");
         GridwizardstepsContainer.AddObjectProperty("CmpContext", sPrefix);
         GridwizardstepsContainer.AddObjectProperty("InMasterPage", "false");
         GridwizardstepsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleSteps"));
         GridwizardstepsContainer.AddObjectProperty("Class", "FreeStyleSteps");
         GridwizardstepsContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridwizardstepsContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridwizardstepsContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridwizardsteps_Backcolorstyle), 1, 0, ".", "")));
         GridwizardstepsContainer.PageSize = subGridwizardsteps_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_52( ) ;
            /* Execute user event: Gridwizardsteps.Load */
            E120M2 ();
            wbEnd = 5;
            WB0M0( ) ;
         }
         bGXsfl_5_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes0M2( )
      {
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPREALNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV15StepRealNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepRealNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSELECTEDSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV12SelectedStepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV12SelectedStepNumber), "ZZZ9"), context));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"vWIZARDSTEPSAUX", AV18WizardStepsAux);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"vWIZARDSTEPSAUX", AV18WizardStepsAux);
         }
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSAUX", GetSecureSignedToken( sPrefix, AV18WizardStepsAux, context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vSTEPNUMBER", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV14StepNumber), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV14StepNumber), "ZZZ9"), context));
         GxWebStd.gx_hidden_field( context, sPrefix+"vWIZARDSTEPSCOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20WizardStepsCount), 4, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vFIRSTISDUMMY", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vLASTISDUMMY", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         GxWebStd.gx_boolean_hidden_field( context, sPrefix+"vPENULTIMATEISDUMMY", AV10PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV10PenultimateIsDummy, context));
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
         edtavWizardstepsaux__title_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP0M0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E110M2 ();
         context.wbGlbDoneStart = 1;
         nDoneStart = 1;
         /* After Start, stand alone formulas. */
         sXEvt = cgiGet( "_EventName");
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"Wizardstepsaux"), AV18WizardStepsAux);
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"vWIZARDSTEPSAUX"), AV18WizardStepsAux);
            /* Read saved values. */
            nRC_GXsfl_5 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_5"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            wcpOAV6CurrentStep = cgiGet( sPrefix+"wcpOAV6CurrentStep");
            nRC_GXsfl_5 = (int)(Math.Round(context.localUtil.CToN( cgiGet( sPrefix+"nRC_GXsfl_5"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            nGXsfl_5_fel_idx = 0;
            while ( nGXsfl_5_fel_idx < nRC_GXsfl_5 )
            {
               nGXsfl_5_fel_idx = ((subGridwizardsteps_Islastpage==1)&&(nGXsfl_5_fel_idx+1>subGridwizardsteps_fnc_Recordsperpage( )) ? 1 : nGXsfl_5_fel_idx+1);
               sGXsfl_5_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_5_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_52( ) ;
               AV24GXV1 = nGXsfl_5_fel_idx;
               if ( ( AV18WizardStepsAux.Count >= AV24GXV1 ) && ( AV24GXV1 > 0 ) )
               {
                  AV18WizardStepsAux.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV18WizardStepsAux.Item(AV24GXV1));
               }
            }
            if ( nGXsfl_5_fel_idx == 0 )
            {
               nGXsfl_5_idx = 1;
               sGXsfl_5_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_5_idx), 4, 0), 4, "0");
               SubsflControlProps_52( ) ;
            }
            nGXsfl_5_fel_idx = 1;
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
         E110M2 ();
         if (returnInSub) return;
      }

      protected void E110M2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV9MaxStepsToShow = 11;
         AV21MaxStepsToShowInXS = 5;
         AV12SelectedStepNumber = 1;
         AssignAttri(sPrefix, false, "AV12SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV12SelectedStepNumber), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV12SelectedStepNumber), "ZZZ9"), context));
         AV26GXV3 = 1;
         while ( AV26GXV3 <= AV17WizardSteps.Count )
         {
            AV5WizardStep = ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV17WizardSteps.Item(AV26GXV3));
            if ( StringUtil.StrCmp(AV5WizardStep.gxTpr_Code, AV6CurrentStep) == 0 )
            {
               if (true) break;
            }
            else
            {
               AV12SelectedStepNumber = (short)(AV12SelectedStepNumber+1);
               AssignAttri(sPrefix, false, "AV12SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV12SelectedStepNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV12SelectedStepNumber), "ZZZ9"), context));
            }
            AV26GXV3 = (int)(AV26GXV3+1);
         }
         AV15StepRealNumber = 1;
         AssignAttri(sPrefix, false, "AV15StepRealNumber", StringUtil.LTrimStr( (decimal)(AV15StepRealNumber), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepRealNumber), "ZZZ9"), context));
         AV18WizardStepsAux = (GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)(AV17WizardSteps.Clone());
         gx_BV5 = true;
         AV7FirstIsDummy = false;
         AssignAttri(sPrefix, false, "AV7FirstIsDummy", AV7FirstIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
         AV11SecondIsDummy = false;
         AV10PenultimateIsDummy = false;
         AssignAttri(sPrefix, false, "AV10PenultimateIsDummy", AV10PenultimateIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV10PenultimateIsDummy, context));
         AV8LastIsDummy = false;
         AssignAttri(sPrefix, false, "AV8LastIsDummy", AV8LastIsDummy);
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
         if ( AV18WizardStepsAux.Count > AV9MaxStepsToShow )
         {
            if ( AV12SelectedStepNumber > AV18WizardStepsAux.Count )
            {
               AV12SelectedStepNumber = 1;
               AssignAttri(sPrefix, false, "AV12SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV12SelectedStepNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV12SelectedStepNumber), "ZZZ9"), context));
            }
            AV13StartIndex = 1;
            if ( ( AV12SelectedStepNumber + 3 - AV9MaxStepsToShow /  ( decimal )( 2 ) > Convert.ToDecimal( 0 )) )
            {
               AV13StartIndex = (short)(AV12SelectedStepNumber+3-AV9MaxStepsToShow/ (decimal)(2));
               if ( AV13StartIndex + ( AV9MaxStepsToShow - 2 ) > AV18WizardStepsAux.Count + 1 )
               {
                  AV13StartIndex = (short)(AV18WizardStepsAux.Count-(AV9MaxStepsToShow-2)+1);
               }
            }
            if ( AV13StartIndex > 3 )
            {
               AV15StepRealNumber = AV13StartIndex;
               AssignAttri(sPrefix, false, "AV15StepRealNumber", StringUtil.LTrimStr( (decimal)(AV15StepRealNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepRealNumber), "ZZZ9"), context));
               AV7FirstIsDummy = true;
               AssignAttri(sPrefix, false, "AV7FirstIsDummy", AV7FirstIsDummy);
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vFIRSTISDUMMY", GetSecureSignedToken( sPrefix, AV7FirstIsDummy, context));
               AV11SecondIsDummy = true;
               ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV18WizardStepsAux.Item(2)).gxTpr_Title = "...";
               while ( AV13StartIndex > 3 )
               {
                  AV18WizardStepsAux.RemoveItem(3);
                  gx_BV5 = true;
                  AV13StartIndex = (short)(AV13StartIndex-1);
                  AV12SelectedStepNumber = (short)(AV12SelectedStepNumber-1);
                  AssignAttri(sPrefix, false, "AV12SelectedStepNumber", StringUtil.LTrimStr( (decimal)(AV12SelectedStepNumber), 4, 0));
                  GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSELECTEDSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV12SelectedStepNumber), "ZZZ9"), context));
               }
            }
            if ( AV18WizardStepsAux.Count > AV9MaxStepsToShow )
            {
               AV8LastIsDummy = true;
               AssignAttri(sPrefix, false, "AV8LastIsDummy", AV8LastIsDummy);
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vLASTISDUMMY", GetSecureSignedToken( sPrefix, AV8LastIsDummy, context));
               AV10PenultimateIsDummy = true;
               AssignAttri(sPrefix, false, "AV10PenultimateIsDummy", AV10PenultimateIsDummy);
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vPENULTIMATEISDUMMY", GetSecureSignedToken( sPrefix, AV10PenultimateIsDummy, context));
               ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV18WizardStepsAux.Item(AV18WizardStepsAux.Count-1)).gxTpr_Title = "";
               while ( AV18WizardStepsAux.Count > AV9MaxStepsToShow )
               {
                  AV18WizardStepsAux.RemoveItem(AV18WizardStepsAux.Count-2);
                  gx_BV5 = true;
               }
            }
         }
         AV20WizardStepsCount = (short)(AV18WizardStepsAux.Count);
         AssignAttri(sPrefix, false, "AV20WizardStepsCount", StringUtil.LTrimStr( (decimal)(AV20WizardStepsCount), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vWIZARDSTEPSCOUNT", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV20WizardStepsCount), "ZZZ9"), context));
         if ( AV18WizardStepsAux.Count > AV21MaxStepsToShowInXS )
         {
            AV19WizardStepsItem = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
            AV19WizardStepsItem.gxTpr_Title = "DummiesXS_Test";
            AV19WizardStepsItem.gxTpr_Code = "FirstDummyXS";
            AV18WizardStepsAux.Add(AV19WizardStepsItem, 2);
            gx_BV5 = true;
            AV19WizardStepsItem = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
            AV19WizardStepsItem.gxTpr_Title = "DummiesXS_Test";
            AV19WizardStepsItem.gxTpr_Code = "LastDummyXS";
            AV18WizardStepsAux.Add(AV19WizardStepsItem, AV18WizardStepsAux.Count-1);
            gx_BV5 = true;
         }
         AV14StepNumber = 1;
         AssignAttri(sPrefix, false, "AV14StepNumber", StringUtil.LTrimStr( (decimal)(AV14StepNumber), 4, 0));
         GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV14StepNumber), "ZZZ9"), context));
      }

      private void E120M2( )
      {
         /* Gridwizardsteps_Load Routine */
         returnInSub = false;
         AV24GXV1 = 1;
         while ( AV24GXV1 <= AV18WizardStepsAux.Count )
         {
            AV18WizardStepsAux.CurrentItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV18WizardStepsAux.Item(AV24GXV1));
            lblStepnumber_Visible = 0;
            lblStepnumber_Caption = context.localUtil.Format( (decimal)(AV15StepRealNumber), "ZZZ9");
            tblTablestepbulletlineleft_Class = "TableStepBulletLine";
            AssignProp(sPrefix, false, tblTablestepbulletlineleft_Internalname, "Class", tblTablestepbulletlineleft_Class, !bGXsfl_5_Refreshing);
            tblTablestepbulletlineright_Class = "TableStepBulletLine";
            AssignProp(sPrefix, false, tblTablestepbulletlineright_Internalname, "Class", tblTablestepbulletlineright_Class, !bGXsfl_5_Refreshing);
            tblTblcontainerstep_Class = "TableContainerStepBullet";
            AssignProp(sPrefix, false, tblTblcontainerstep_Internalname, "Class", tblTblcontainerstep_Class, !bGXsfl_5_Refreshing);
            if ( ( AV12SelectedStepNumber != AV14StepNumber ) && ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)(AV18WizardStepsAux.CurrentItem)).gxTpr_Code, "FirstDummyXS") != 0 ) && ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)(AV18WizardStepsAux.CurrentItem)).gxTpr_Code, "LastDummyXS") != 0 ) && ( AV14StepNumber > 1 ) && ( AV14StepNumber < AV20WizardStepsCount ) )
            {
               if ( ( AV12SelectedStepNumber <= 3 ) && ( AV14StepNumber > 3 ) )
               {
                  tblTblcontainerstep_Class = tblTblcontainerstep_Class+" hidden-xs";
                  AssignProp(sPrefix, false, tblTblcontainerstep_Internalname, "Class", tblTblcontainerstep_Class, !bGXsfl_5_Refreshing);
               }
               if ( ( AV12SelectedStepNumber > 3 ) && ( AV14StepNumber > 1 ) && ( AV12SelectedStepNumber < AV20WizardStepsCount - 2 ) )
               {
                  tblTblcontainerstep_Class = tblTblcontainerstep_Class+" hidden-xs";
                  AssignProp(sPrefix, false, tblTblcontainerstep_Internalname, "Class", tblTblcontainerstep_Class, !bGXsfl_5_Refreshing);
               }
               if ( ( AV12SelectedStepNumber >= AV20WizardStepsCount - 2 ) && ( AV14StepNumber < AV20WizardStepsCount - 2 ) )
               {
                  tblTblcontainerstep_Class = tblTblcontainerstep_Class+" hidden-xs";
                  AssignProp(sPrefix, false, tblTblcontainerstep_Internalname, "Class", tblTblcontainerstep_Class, !bGXsfl_5_Refreshing);
               }
            }
            if ( AV14StepNumber < AV12SelectedStepNumber )
            {
               divTablestepitem_Class = "TableStepBulletChecked";
               AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_5_Refreshing);
               tblTablestepbulletlineleft_Class = "TableStepBulletLineChecked";
               AssignProp(sPrefix, false, tblTablestepbulletlineleft_Internalname, "Class", tblTablestepbulletlineleft_Class, !bGXsfl_5_Refreshing);
               tblTablestepbulletlineright_Class = "TableStepBulletLineChecked";
               AssignProp(sPrefix, false, tblTablestepbulletlineright_Internalname, "Class", tblTablestepbulletlineright_Class, !bGXsfl_5_Refreshing);
               edtavWizardstepsaux__title_Class = "AttributeStepBullet";
            }
            else if ( AV14StepNumber == AV12SelectedStepNumber )
            {
               lblStepnumber_Visible = 1;
               lblStepnumber_Class = "StepNumberBulletSelected";
               divTablestepitem_Class = "TableStepBulletSelected";
               AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_5_Refreshing);
               tblTablestepbulletlineleft_Class = "TableStepBulletLineChecked";
               AssignProp(sPrefix, false, tblTablestepbulletlineleft_Internalname, "Class", tblTablestepbulletlineleft_Class, !bGXsfl_5_Refreshing);
               edtavWizardstepsaux__title_Class = "AttributeStepBulletSelected";
            }
            else if ( AV14StepNumber > AV12SelectedStepNumber )
            {
               lblStepnumber_Class = "StepNumberBullet";
               lblStepnumber_Visible = 1;
               divTablestepitem_Class = "TableStepBullet";
               AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_5_Refreshing);
               edtavWizardstepsaux__title_Class = "AttributeStepBulletUnSelected";
            }
            tblTblcontainerstep_Visible = 1;
            AssignProp(sPrefix, false, tblTblcontainerstep_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(tblTblcontainerstep_Visible), 5, 0), !bGXsfl_5_Refreshing);
            if ( ! StringUtil.Contains( ((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)(AV18WizardStepsAux.CurrentItem)).gxTpr_Code, "DummyXS") )
            {
               if ( ( AV14StepNumber == 1 ) && AV7FirstIsDummy )
               {
                  lblStepnumber_Caption = StringUtil.Trim( StringUtil.Str( (decimal)(AV14StepNumber), 4, 0));
               }
               else if ( ( AV14StepNumber == AV20WizardStepsCount ) && AV8LastIsDummy )
               {
                  lblStepnumber_Caption = StringUtil.Trim( StringUtil.Str( (decimal)(AV17WizardSteps.Count), 9, 0));
               }
               else if ( ( AV14StepNumber == AV20WizardStepsCount - 1 ) && AV10PenultimateIsDummy )
               {
                  lblStepnumber_Caption = "...";
               }
               else if ( ( AV14StepNumber == 2 ) && AV7FirstIsDummy )
               {
               }
               else
               {
                  AV15StepRealNumber = (short)(AV15StepRealNumber+1);
                  AssignAttri(sPrefix, false, "AV15StepRealNumber", StringUtil.LTrimStr( (decimal)(AV15StepRealNumber), 4, 0));
                  GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPREALNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV15StepRealNumber), "ZZZ9"), context));
               }
               AV14StepNumber = (short)(AV14StepNumber+1);
               AssignAttri(sPrefix, false, "AV14StepNumber", StringUtil.LTrimStr( (decimal)(AV14StepNumber), 4, 0));
               GxWebStd.gx_hidden_field( context, sPrefix+"gxhash_vSTEPNUMBER", GetSecureSignedToken( sPrefix, context.localUtil.Format( (decimal)(AV14StepNumber), "ZZZ9"), context));
            }
            else
            {
               lblStepnumber_Caption = "...";
               tblTblcontainerstep_Class = "TableContainerStepBullet hidden-sm hidden-lg hidden-md";
               AssignProp(sPrefix, false, tblTblcontainerstep_Internalname, "Class", tblTblcontainerstep_Class, !bGXsfl_5_Refreshing);
               if ( ( ( AV12SelectedStepNumber <= 3 ) && ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)(AV18WizardStepsAux.CurrentItem)).gxTpr_Code, "FirstDummyXS") == 0 ) ) || ( ( AV12SelectedStepNumber >= AV20WizardStepsCount - 2 ) && ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)(AV18WizardStepsAux.CurrentItem)).gxTpr_Code, "LastDummyXS") == 0 ) ) )
               {
                  tblTblcontainerstep_Visible = 0;
                  AssignProp(sPrefix, false, tblTblcontainerstep_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(tblTblcontainerstep_Visible), 5, 0), !bGXsfl_5_Refreshing);
               }
               if ( ( AV12SelectedStepNumber > 3 ) && ( StringUtil.StrCmp(((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)(AV18WizardStepsAux.CurrentItem)).gxTpr_Code, "FirstDummyXS") == 0 ) )
               {
                  divTablestepitem_Class = "TableStepExtraBulletChecked";
                  AssignProp(sPrefix, false, divTablestepitem_Internalname, "Class", divTablestepitem_Class, !bGXsfl_5_Refreshing);
                  lblStepnumber_Visible = 1;
               }
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 5;
            }
            sendrow_52( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_5_Refreshing )
            {
               DoAjaxLoad(5, GridwizardstepsRow);
            }
            AV24GXV1 = (int)(AV24GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void wb_table1_2_0M2( bool wbgen )
      {
         if ( wbgen )
         {
            /* Table start */
            sStyleString = "";
            GxWebStd.gx_table_start( context, tblTablemain_Internalname, tblTablemain_Internalname, "", "TableWizardSteps TableAlignedCentered", 0, "", "", 1, 2, sStyleString, "", "", 0);
            context.WriteHtmlText( "<tbody>") ;
            context.WriteHtmlText( "<tr>") ;
            context.WriteHtmlText( "<td class='WizardStepsContainerCell'>") ;
            /*  Grid Control  */
            GridwizardstepsContainer.SetIsFreestyle(true);
            GridwizardstepsContainer.SetWrapped(nGXWrapped);
            StartGridControl5( ) ;
         }
         if ( wbEnd == 5 )
         {
            wbEnd = 0;
            nRC_GXsfl_5 = (int)(nGXsfl_5_idx-1);
            if ( GridwizardstepsContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV24GXV1 = nGXsfl_5_idx;
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
            context.WriteHtmlText( "</td>") ;
            context.WriteHtmlText( "</tr>") ;
            context.WriteHtmlText( "</tbody>") ;
            /* End of table */
            context.WriteHtmlText( "</table>") ;
            wb_table1_2_0M2e( true) ;
         }
         else
         {
            wb_table1_2_0M2e( false) ;
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         AV17WizardSteps = (GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)getParm(obj,0);
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
         PA0M2( ) ;
         WS0M2( ) ;
         WE0M2( ) ;
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
         sCtrlAV17WizardSteps = (string)((string)getParm(obj,0));
         sCtrlAV6CurrentStep = (string)((string)getParm(obj,1));
      }

      public override void componentrestorestate( string sPPrefix ,
                                                  string sPSFPrefix )
      {
         sPrefix = sPPrefix + sPSFPrefix;
         PA0M2( ) ;
         WCParametersGet( ) ;
      }

      public override void componentprepare( Object[] obj )
      {
         wbLoad = false;
         sCompPrefix = (string)getParm(obj,0);
         sSFPrefix = (string)getParm(obj,1);
         sPrefix = sCompPrefix + sSFPrefix;
         AddComponentObject(sPrefix, "wwpbaseobjects\\wizardstepsbulletwc", GetJustCreated( ));
         if ( ( nDoneStart == 0 ) && ( nDynComponent == 0 ) )
         {
            INITWEB( ) ;
         }
         else
         {
            init_default_properties( ) ;
            init_web_controls( ) ;
         }
         PA0M2( ) ;
         if ( ! GetJustCreated( ) && ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 ) && ( context.wbGlbDoneStart == 0 ) )
         {
            WCParametersGet( ) ;
         }
         else
         {
            AV17WizardSteps = (GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>)getParm(obj,2);
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
         sCtrlAV17WizardSteps = cgiGet( sPrefix+"AV17WizardSteps_CTRL");
         if ( StringUtil.Len( sCtrlAV17WizardSteps) > 0 )
         {
            AV17WizardSteps = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>();
         }
         else
         {
            ajax_req_read_hidden_sdt(cgiGet( sPrefix+"AV17WizardSteps_PARM"), AV17WizardSteps);
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
         PA0M2( ) ;
         sEvt = sCompEvt;
         WCParametersGet( ) ;
         WS0M2( ) ;
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
         WS0M2( ) ;
         SaveComponentMsgList(sPrefix);
         context.GX_msglist = BackMsgLst;
      }

      protected void WCParametersSet( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri(sPrefix, false, sPrefix+"AV17WizardSteps_PARM", AV17WizardSteps);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt(sPrefix+"AV17WizardSteps_PARM", AV17WizardSteps);
         }
         if ( StringUtil.Len( StringUtil.RTrim( sCtrlAV17WizardSteps)) > 0 )
         {
            GxWebStd.gx_hidden_field( context, sPrefix+"AV17WizardSteps_CTRL", StringUtil.RTrim( sCtrlAV17WizardSteps));
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
         WE0M2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623361137", true, true);
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
            context.AddJavascriptSource("wwpbaseobjects/wizardstepsbulletwc.js", "?2024121623361137", false, true);
         }
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_52( )
      {
         lblDummybulletlineleft_Internalname = sPrefix+"DUMMYBULLETLINELEFT_"+sGXsfl_5_idx;
         lblStepnumber_Internalname = sPrefix+"STEPNUMBER_"+sGXsfl_5_idx;
         lblDummybulletlineright_Internalname = sPrefix+"DUMMYBULLETLINERIGHT_"+sGXsfl_5_idx;
         edtavWizardstepsaux__title_Internalname = sPrefix+"WIZARDSTEPSAUX__TITLE_"+sGXsfl_5_idx;
      }

      protected void SubsflControlProps_fel_52( )
      {
         lblDummybulletlineleft_Internalname = sPrefix+"DUMMYBULLETLINELEFT_"+sGXsfl_5_fel_idx;
         lblStepnumber_Internalname = sPrefix+"STEPNUMBER_"+sGXsfl_5_fel_idx;
         lblDummybulletlineright_Internalname = sPrefix+"DUMMYBULLETLINERIGHT_"+sGXsfl_5_fel_idx;
         edtavWizardstepsaux__title_Internalname = sPrefix+"WIZARDSTEPSAUX__TITLE_"+sGXsfl_5_fel_idx;
      }

      protected void sendrow_52( )
      {
         sGXsfl_5_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_5_idx), 4, 0), 4, "0");
         SubsflControlProps_52( ) ;
         WB0M0( ) ;
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
         GridwizardstepsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)subGridwizardsteps_Linesclass,(string)""});
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Table start */
         GridwizardstepsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblTblcontainerstep_Internalname+"_"+sGXsfl_5_idx,(int)tblTblcontainerstep_Visible,(string)tblTblcontainerstep_Class,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)0,(short)0,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridwizardstepsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Table start */
         GridwizardstepsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblTablestepbulletline_Internalname+"_"+sGXsfl_5_idx,(short)1,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)1,(short)2,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridwizardstepsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Table start */
         GridwizardstepsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblTablestepbulletlineleft_Internalname+"_"+sGXsfl_5_idx,(short)1,(string)tblTablestepbulletlineleft_Class,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)0,(short)0,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridwizardstepsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Text block */
         GridwizardstepsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblDummybulletlineleft_Internalname,(string)" ",(string)"",(string)"",(string)lblDummybulletlineleft_Jsonclick,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlock",(short)0,(string)"",(short)1,(short)1,(short)0,(short)0});
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("row");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("table");
         }
         /* End of table */
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divLayout_tablestepitem_Internalname+"_"+sGXsfl_5_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Section",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"Section",(string)"start",(string)"top",(string)" "+"data-gx-base-lib=\"bootstrapv3\""+" "+"data-abstract-form"+" ",(string)"",(string)"div"});
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)divTablestepitem_Internalname+"_"+sGXsfl_5_idx,(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)divTablestepitem_Class,(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"row",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Div Control */
         GridwizardstepsRow.AddColumnProperties("div_start", -1, isAjaxCallMode( ), new Object[] {(string)"",(short)1,(short)0,(string)"px",(short)0,(string)"px",(string)"col-xs-12 StepNumberBulletCell",(string)"start",(string)"top",(string)"",(string)"",(string)"div"});
         /* Text block */
         GridwizardstepsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblStepnumber_Internalname,(string)lblStepnumber_Caption,(string)"",(string)"",(string)lblStepnumber_Jsonclick,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)lblStepnumber_Class,(short)0,(string)"",(int)lblStepnumber_Visible,(short)1,(short)0,(short)0});
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         GridwizardstepsRow.AddColumnProperties("div_end", -1, isAjaxCallMode( ), new Object[] {(string)"start",(string)"top",(string)"div"});
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Table start */
         GridwizardstepsRow.AddColumnProperties("table", -1, isAjaxCallMode( ), new Object[] {(string)tblTablestepbulletlineright_Internalname+"_"+sGXsfl_5_idx,(short)1,(string)tblTablestepbulletlineright_Class,(string)"",(string)"",(string)"",(string)"",(string)"",(string)"",(short)0,(short)0,(string)"",(string)"",(string)"",(string)"px",(string)"px",(string)""});
         GridwizardstepsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         /* Text block */
         GridwizardstepsRow.AddColumnProperties("label", 1, isAjaxCallMode( ), new Object[] {(string)lblDummybulletlineright_Internalname,(string)" ",(string)"",(string)"",(string)lblDummybulletlineright_Jsonclick,(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"TextBlock",(short)0,(string)"",(short)1,(short)1,(short)0,(short)0});
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("row");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("table");
         }
         /* End of table */
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("row");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("table");
         }
         /* End of table */
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("row");
         }
         sendrow_5230( ) ;
      }

      protected void sendrow_5230( )
      {
         GridwizardstepsRow.AddColumnProperties("row", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)""});
         GridwizardstepsRow.AddColumnProperties("cell", -1, isAjaxCallMode( ), new Object[] {(string)"",(string)"",(string)"AttributeStepBulletCell"});
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 32,'" + sPrefix + "',false,'" + sGXsfl_5_idx + "',5)\"";
         ROClassString = edtavWizardstepsaux__title_Class;
         GridwizardstepsRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavWizardstepsaux__title_Internalname,((DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem)AV18WizardStepsAux.Item(AV24GXV1)).gxTpr_Title,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,32);\"",(string)"'"+sPrefix+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavWizardstepsaux__title_Jsonclick,(short)0,(string)edtavWizardstepsaux__title_Class,(string)"",(string)ROClassString,(string)"",(string)"",(short)1,(int)edtavWizardstepsaux__title_Enabled,(short)0,(string)"text",(string)"",(short)40,(string)"chr",(short)1,(string)"row",(short)40,(short)0,(short)0,(short)5,(short)0,(short)-1,(short)-1,(bool)false,(string)"",(string)"start",(bool)true,(string)""});
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("row");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("table");
         }
         /* End of table */
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("cell");
         }
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            GridwizardstepsContainer.CloseTag("row");
         }
         send_integrity_lvl_hashes0M2( ) ;
         /* End of Columns property logic. */
         GridwizardstepsContainer.AddRow(GridwizardstepsRow);
         nGXsfl_5_idx = ((subGridwizardsteps_Islastpage==1)&&(nGXsfl_5_idx+1>subGridwizardsteps_fnc_Recordsperpage( )) ? 1 : nGXsfl_5_idx+1);
         sGXsfl_5_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_5_idx), 4, 0), 4, "0");
         SubsflControlProps_52( ) ;
         /* End function sendrow_52 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl5( )
      {
         if ( GridwizardstepsContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+sPrefix+"GridwizardstepsContainer"+"DivS\" data-gxgridid=\"5\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridwizardsteps_Internalname, subGridwizardsteps_Internalname, "", "FreeStyleSteps", 0, "", "", 1, 2, sStyleString, "", "", 0);
            GridwizardstepsContainer.AddObjectProperty("GridName", "Gridwizardsteps");
         }
         else
         {
            GridwizardstepsContainer.AddObjectProperty("GridName", "Gridwizardsteps");
            GridwizardstepsContainer.AddObjectProperty("Header", subGridwizardsteps_Header);
            GridwizardstepsContainer.AddObjectProperty("Class", StringUtil.RTrim( "FreeStyleSteps"));
            GridwizardstepsContainer.AddObjectProperty("Class", "FreeStyleSteps");
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
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsColumn.AddObjectProperty("Value", lblDummybulletlineleft_Caption);
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
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsColumn.AddObjectProperty("Value", lblDummybulletlineleft_Caption);
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsContainer.AddColumnProperties(GridwizardstepsColumn);
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridwizardstepsColumn.AddObjectProperty("Class", StringUtil.RTrim( edtavWizardstepsaux__title_Class));
            GridwizardstepsColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavWizardstepsaux__title_Enabled), 5, 0, ".", "")));
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
         lblDummybulletlineleft_Internalname = sPrefix+"DUMMYBULLETLINELEFT";
         tblTablestepbulletlineleft_Internalname = sPrefix+"TABLESTEPBULLETLINELEFT";
         lblStepnumber_Internalname = sPrefix+"STEPNUMBER";
         divTablestepitem_Internalname = sPrefix+"TABLESTEPITEM";
         divLayout_tablestepitem_Internalname = sPrefix+"LAYOUT_TABLESTEPITEM";
         lblDummybulletlineright_Internalname = sPrefix+"DUMMYBULLETLINERIGHT";
         tblTablestepbulletlineright_Internalname = sPrefix+"TABLESTEPBULLETLINERIGHT";
         tblTablestepbulletline_Internalname = sPrefix+"TABLESTEPBULLETLINE";
         edtavWizardstepsaux__title_Internalname = sPrefix+"WIZARDSTEPSAUX__TITLE";
         tblTblcontainerstep_Internalname = sPrefix+"TBLCONTAINERSTEP";
         tblTablemain_Internalname = sPrefix+"TABLEMAIN";
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
         lblDummybulletlineleft_Caption = " ";
         edtavWizardstepsaux__title_Jsonclick = "";
         edtavWizardstepsaux__title_Class = "AttributeStepBulletUnSelected";
         edtavWizardstepsaux__title_Enabled = 0;
         lblStepnumber_Class = "StepNumberBullet";
         lblStepnumber_Caption = "1";
         lblStepnumber_Visible = 1;
         subGridwizardsteps_Class = "FreeStyleSteps";
         tblTblcontainerstep_Visible = 1;
         divTablestepitem_Class = "TableStepBullet";
         tblTblcontainerstep_Class = "TableContainerStepBullet";
         tblTablestepbulletlineright_Class = "TableStepBulletLine";
         tblTablestepbulletlineleft_Class = "TableStepBulletLine";
         subGridwizardsteps_Backcolorstyle = 0;
         edtavWizardstepsaux__title_Enabled = -1;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDWIZARDSTEPS_nFirstRecordOnPage"},{"av":"GRIDWIZARDSTEPS_nEOF"},{"av":"AV17WizardSteps","fld":"vWIZARDSTEPS"},{"av":"sPrefix"},{"av":"AV18WizardStepsAux","fld":"vWIZARDSTEPSAUX","grid":5,"hsh":true},{"av":"nGXsfl_5_idx","ctrl":"GRID","prop":"GridCurrRow","grid":5},{"av":"nRC_GXsfl_5","ctrl":"GRIDWIZARDSTEPS","prop":"GridRC","grid":5},{"av":"AV15StepRealNumber","fld":"vSTEPREALNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV12SelectedStepNumber","fld":"vSELECTEDSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV14StepNumber","fld":"vSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV20WizardStepsCount","fld":"vWIZARDSTEPSCOUNT","pic":"ZZZ9","hsh":true},{"av":"AV7FirstIsDummy","fld":"vFIRSTISDUMMY","hsh":true},{"av":"AV8LastIsDummy","fld":"vLASTISDUMMY","hsh":true},{"av":"AV10PenultimateIsDummy","fld":"vPENULTIMATEISDUMMY","hsh":true}]}""");
         setEventMetadata("GRIDWIZARDSTEPS.LOAD","""{"handler":"E120M2","iparms":[{"av":"AV15StepRealNumber","fld":"vSTEPREALNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV12SelectedStepNumber","fld":"vSELECTEDSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV18WizardStepsAux","fld":"vWIZARDSTEPSAUX","grid":5,"hsh":true},{"av":"nGXsfl_5_idx","ctrl":"GRID","prop":"GridCurrRow","grid":5},{"av":"GRIDWIZARDSTEPS_nFirstRecordOnPage"},{"av":"nRC_GXsfl_5","ctrl":"GRIDWIZARDSTEPS","prop":"GridRC","grid":5},{"av":"AV14StepNumber","fld":"vSTEPNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV20WizardStepsCount","fld":"vWIZARDSTEPSCOUNT","pic":"ZZZ9","hsh":true},{"av":"AV7FirstIsDummy","fld":"vFIRSTISDUMMY","hsh":true},{"av":"AV8LastIsDummy","fld":"vLASTISDUMMY","hsh":true},{"av":"AV17WizardSteps","fld":"vWIZARDSTEPS"},{"av":"AV10PenultimateIsDummy","fld":"vPENULTIMATEISDUMMY","hsh":true}]""");
         setEventMetadata("GRIDWIZARDSTEPS.LOAD",""","oparms":[{"av":"lblStepnumber_Visible","ctrl":"STEPNUMBER","prop":"Visible"},{"av":"lblStepnumber_Caption","ctrl":"STEPNUMBER","prop":"Caption"},{"av":"tblTablestepbulletlineleft_Class","ctrl":"TABLESTEPBULLETLINELEFT","prop":"Class"},{"av":"tblTablestepbulletlineright_Class","ctrl":"TABLESTEPBULLETLINERIGHT","prop":"Class"},{"av":"tblTblcontainerstep_Class","ctrl":"TBLCONTAINERSTEP","prop":"Class"},{"av":"divTablestepitem_Class","ctrl":"TABLESTEPITEM","prop":"Class"},{"ctrl":"WIZARDSTEPSAUX__TITLE","prop":"Class"},{"av":"lblStepnumber_Class","ctrl":"STEPNUMBER","prop":"Class"},{"av":"tblTblcontainerstep_Visible","ctrl":"TBLCONTAINERSTEP","prop":"Visible"},{"av":"AV15StepRealNumber","fld":"vSTEPREALNUMBER","pic":"ZZZ9","hsh":true},{"av":"AV14StepNumber","fld":"vSTEPNUMBER","pic":"ZZZ9","hsh":true}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Gxv2","iparms":[]}""");
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
         AV17WizardSteps = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>( context, "WizardStepsItem", "DesignSystem");
         wcpOAV6CurrentStep = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         sPrefix = "";
         AV18WizardStepsAux = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem>( context, "WizardStepsItem", "DesignSystem");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         GX_FocusControl = "";
         GridwizardstepsContainer = new GXWebGrid( context);
         sStyleString = "";
         Form = new GXWebForm();
         sXEvt = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         GXDecQS = "";
         AV5WizardStep = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
         AV19WizardStepsItem = new DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem(context);
         GridwizardstepsRow = new GXWebRow();
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         sCtrlAV17WizardSteps = "";
         sCtrlAV6CurrentStep = "";
         subGridwizardsteps_Linesclass = "";
         lblDummybulletlineleft_Jsonclick = "";
         lblStepnumber_Jsonclick = "";
         lblDummybulletlineright_Jsonclick = "";
         TempTags = "";
         ROClassString = "";
         subGridwizardsteps_Header = "";
         GridwizardstepsColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavWizardstepsaux__title_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short nDynComponent ;
      private short AV15StepRealNumber ;
      private short AV12SelectedStepNumber ;
      private short AV14StepNumber ;
      private short AV20WizardStepsCount ;
      private short nGXWrapped ;
      private short wbEnd ;
      private short wbStart ;
      private short nDraw ;
      private short nDoneStart ;
      private short nDonePA ;
      private short subGridwizardsteps_Backcolorstyle ;
      private short AV9MaxStepsToShow ;
      private short AV21MaxStepsToShowInXS ;
      private short AV13StartIndex ;
      private short subGridwizardsteps_Backstyle ;
      private short subGridwizardsteps_Allowselection ;
      private short subGridwizardsteps_Allowhovering ;
      private short subGridwizardsteps_Allowcollapsing ;
      private short subGridwizardsteps_Collapsed ;
      private short GRIDWIZARDSTEPS_nEOF ;
      private int nRC_GXsfl_5 ;
      private int nGXsfl_5_idx=1 ;
      private int edtavWizardstepsaux__title_Enabled ;
      private int AV24GXV1 ;
      private int subGridwizardsteps_Islastpage ;
      private int nGXsfl_5_fel_idx=1 ;
      private int AV26GXV3 ;
      private int lblStepnumber_Visible ;
      private int tblTblcontainerstep_Visible ;
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
      private string sGXsfl_5_idx="0001" ;
      private string edtavWizardstepsaux__title_Internalname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string GX_FocusControl ;
      private string sStyleString ;
      private string subGridwizardsteps_Internalname ;
      private string sXEvt ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string GXDecQS ;
      private string sGXsfl_5_fel_idx="0001" ;
      private string lblStepnumber_Caption ;
      private string tblTablestepbulletlineleft_Class ;
      private string tblTablestepbulletlineleft_Internalname ;
      private string tblTablestepbulletlineright_Class ;
      private string tblTablestepbulletlineright_Internalname ;
      private string tblTblcontainerstep_Class ;
      private string tblTblcontainerstep_Internalname ;
      private string divTablestepitem_Class ;
      private string divTablestepitem_Internalname ;
      private string edtavWizardstepsaux__title_Class ;
      private string lblStepnumber_Class ;
      private string tblTablemain_Internalname ;
      private string sCtrlAV17WizardSteps ;
      private string sCtrlAV6CurrentStep ;
      private string lblDummybulletlineleft_Internalname ;
      private string lblStepnumber_Internalname ;
      private string lblDummybulletlineright_Internalname ;
      private string subGridwizardsteps_Class ;
      private string subGridwizardsteps_Linesclass ;
      private string tblTablestepbulletline_Internalname ;
      private string lblDummybulletlineleft_Jsonclick ;
      private string divLayout_tablestepitem_Internalname ;
      private string lblStepnumber_Jsonclick ;
      private string lblDummybulletlineright_Jsonclick ;
      private string TempTags ;
      private string ROClassString ;
      private string edtavWizardstepsaux__title_Jsonclick ;
      private string subGridwizardsteps_Header ;
      private string lblDummybulletlineleft_Caption ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV7FirstIsDummy ;
      private bool AV8LastIsDummy ;
      private bool AV10PenultimateIsDummy ;
      private bool bGXsfl_5_Refreshing=false ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV5 ;
      private bool AV11SecondIsDummy ;
      private string AV6CurrentStep ;
      private string wcpOAV6CurrentStep ;
      private GXWebGrid GridwizardstepsContainer ;
      private GXWebRow GridwizardstepsRow ;
      private GXWebColumn GridwizardstepsColumn ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV17WizardSteps ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem> AV18WizardStepsAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem AV5WizardStep ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWizardSteps_WizardStepsItem AV19WizardStepsItem ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
