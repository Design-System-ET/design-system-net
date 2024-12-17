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
using GeneXus.Http.Server;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   public class gamtranslations : GXDataArea
   {
      public gamtranslations( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gamtranslations( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_Gx_mode ,
                           string aP1_Type ,
                           string aP2_Title ,
                           long aP3_PrimaryID ,
                           long aP4_SecondaryID ,
                           long aP5_TertiaryID )
      {
         this.Gx_mode = aP0_Gx_mode;
         this.AV24Type = aP1_Type;
         this.AV23Title = aP2_Title;
         this.AV18PrimaryID = aP3_PrimaryID;
         this.AV20SecondaryID = aP4_SecondaryID;
         this.AV21TertiaryID = aP5_TertiaryID;
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Grid") == 0 )
            {
               gxnrGrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Grid") == 0 )
            {
               gxgrGrid_refresh_invoke( ) ;
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

      protected void gxnrGrid_newrow_invoke( )
      {
         nRC_GXsfl_12 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_12"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_12_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_12_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_12_idx = GetPar( "sGXsfl_12_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGrid_newrow( ) ;
         /* End function gxnrGrid_newrow_invoke */
      }

      protected void gxgrGrid_refresh_invoke( )
      {
         subGrid_Rows = (int)(Math.Round(NumberUtil.Val( GetPar( "subGrid_Rows"), "."), 18, MidpointRounding.ToEven));
         AV49Pgmname = GetPar( "Pgmname");
         AV24Type = GetPar( "Type");
         AV18PrimaryID = (long)(Math.Round(NumberUtil.Val( GetPar( "PrimaryID"), "."), 18, MidpointRounding.ToEven));
         AV20SecondaryID = (long)(Math.Round(NumberUtil.Val( GetPar( "SecondaryID"), "."), 18, MidpointRounding.ToEven));
         AV21TertiaryID = (long)(Math.Round(NumberUtil.Val( GetPar( "TertiaryID"), "."), 18, MidpointRounding.ToEven));
         Gx_mode = GetPar( "Mode");
         AV23Title = GetPar( "Title");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGrid_refresh( subGrid_Rows, AV49Pgmname, AV24Type, AV18PrimaryID, AV20SecondaryID, AV21TertiaryID, Gx_mode, AV23Title) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGrid_refresh_invoke */
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
            return "gamexampleroleselect_Execute" ;
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
         PA162( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START162( ) ;
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
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamtranslations.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.RTrim(AV24Type)) + "," + UrlEncode(StringUtil.RTrim(AV23Title)) + "," + UrlEncode(StringUtil.LTrimStr(AV18PrimaryID,12,0)) + "," + UrlEncode(StringUtil.LTrimStr(AV20SecondaryID,12,0)) + "," + UrlEncode(StringUtil.LTrimStr(AV21TertiaryID,12,0));
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("gamtranslations.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey)+"\">") ;
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
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV49Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV49Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vTYPE", AV24Type);
         GxWebStd.gx_hidden_field( context, "gxhash_vTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Type, "")), context));
         GxWebStd.gx_hidden_field( context, "vPRIMARYID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV18PrimaryID), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vPRIMARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV18PrimaryID), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vSECONDARYID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20SecondaryID), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vSECONDARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV20SecondaryID), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vTERTIARYID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV21TertiaryID), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vTERTIARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV21TertiaryID), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "vTITLE", AV23Title);
         GxWebStd.gx_hidden_field( context, "gxhash_vTITLE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23Title, "")), context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_12", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_12), 8, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDCURRENTPAGE", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV38GridCurrentPage), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDPAGECOUNT", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV39GridPageCount), 10, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "vGRIDAPPLIEDFILTERS", AV40GridAppliedFilters);
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV49Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV49Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "vTYPE", AV24Type);
         GxWebStd.gx_hidden_field( context, "gxhash_vTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Type, "")), context));
         GxWebStd.gx_hidden_field( context, "vPRIMARYID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV18PrimaryID), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vPRIMARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV18PrimaryID), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vSECONDARYID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV20SecondaryID), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vSECONDARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV20SecondaryID), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vTERTIARYID", StringUtil.LTrim( StringUtil.NToC( (decimal)(AV21TertiaryID), 12, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "gxhash_vTERTIARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV21TertiaryID), "ZZZZZZZZZZZ9"), context));
         GxWebStd.gx_hidden_field( context, "vMODE", StringUtil.RTrim( Gx_mode));
         GxWebStd.gx_hidden_field( context, "gxhash_vMODE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( Gx_mode, "@!")), context));
         GxWebStd.gx_hidden_field( context, "vTITLE", AV23Title);
         GxWebStd.gx_hidden_field( context, "gxhash_vTITLE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23Title, "")), context));
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "subGrid_Recordcount", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Recordcount), 5, 0, context.GetLanguageProperty( "decimal_point"), "")));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Class", StringUtil.RTrim( Gridpaginationbar_Class));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Showfirst", StringUtil.BoolToStr( Gridpaginationbar_Showfirst));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Showprevious", StringUtil.BoolToStr( Gridpaginationbar_Showprevious));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Shownext", StringUtil.BoolToStr( Gridpaginationbar_Shownext));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Showlast", StringUtil.BoolToStr( Gridpaginationbar_Showlast));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Pagestoshow", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Pagestoshow), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Pagingbuttonsposition", StringUtil.RTrim( Gridpaginationbar_Pagingbuttonsposition));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Pagingcaptionposition", StringUtil.RTrim( Gridpaginationbar_Pagingcaptionposition));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Emptygridclass", StringUtil.RTrim( Gridpaginationbar_Emptygridclass));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselector", StringUtil.BoolToStr( Gridpaginationbar_Rowsperpageselector));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageoptions", StringUtil.RTrim( Gridpaginationbar_Rowsperpageoptions));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Previous", StringUtil.RTrim( Gridpaginationbar_Previous));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Next", StringUtil.RTrim( Gridpaginationbar_Next));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Caption", StringUtil.RTrim( Gridpaginationbar_Caption));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Emptygridcaption", StringUtil.RTrim( Gridpaginationbar_Emptygridcaption));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpagecaption", StringUtil.RTrim( Gridpaginationbar_Rowsperpagecaption));
         GxWebStd.gx_hidden_field( context, "GRID_EMPOWERER_Gridinternalname", StringUtil.RTrim( Grid_empowerer_Gridinternalname));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Selectedpage", StringUtil.RTrim( Gridpaginationbar_Selectedpage));
         GxWebStd.gx_hidden_field( context, "GRIDPAGINATIONBAR_Rowsperpageselectedvalue", StringUtil.LTrim( StringUtil.NToC( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0, ".", "")));
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
            WE162( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT162( ) ;
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
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamtranslations.aspx"+UrlEncode(StringUtil.RTrim(Gx_mode)) + "," + UrlEncode(StringUtil.RTrim(AV24Type)) + "," + UrlEncode(StringUtil.RTrim(AV23Title)) + "," + UrlEncode(StringUtil.LTrimStr(AV18PrimaryID,12,0)) + "," + UrlEncode(StringUtil.LTrimStr(AV20SecondaryID,12,0)) + "," + UrlEncode(StringUtil.LTrimStr(AV21TertiaryID,12,0));
         return formatLink("gamtranslations.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey) ;
      }

      public override string GetPgmname( )
      {
         return "GAMTranslations" ;
      }

      public override string GetPgmdesc( )
      {
         return context.GetMessage( "GAMExampleTranslations", "") ;
      }

      protected void WB160( )
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
            GxWebStd.gx_div_start( context, divTablemain_Internalname, 1, 0, "px", 0, "px", "TableMainWithShadow", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 SectionGrid GridNoBorderCell CellMarginTop HasGridEmpowerer", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divGridtablewithpaginationbar_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridContainer.SetWrapped(nGXWrapped);
            StartGridControl12( ) ;
         }
         if ( wbEnd == 12 )
         {
            wbEnd = 0;
            nRC_GXsfl_12 = (int)(nGXsfl_12_idx-1);
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid", GridContainer, subGrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridContainerData", GridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridContainerData"+"V", GridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /* User Defined Control */
            ucGridpaginationbar.SetProperty("Class", Gridpaginationbar_Class);
            ucGridpaginationbar.SetProperty("ShowFirst", Gridpaginationbar_Showfirst);
            ucGridpaginationbar.SetProperty("ShowPrevious", Gridpaginationbar_Showprevious);
            ucGridpaginationbar.SetProperty("ShowNext", Gridpaginationbar_Shownext);
            ucGridpaginationbar.SetProperty("ShowLast", Gridpaginationbar_Showlast);
            ucGridpaginationbar.SetProperty("PagesToShow", Gridpaginationbar_Pagestoshow);
            ucGridpaginationbar.SetProperty("PagingButtonsPosition", Gridpaginationbar_Pagingbuttonsposition);
            ucGridpaginationbar.SetProperty("PagingCaptionPosition", Gridpaginationbar_Pagingcaptionposition);
            ucGridpaginationbar.SetProperty("EmptyGridClass", Gridpaginationbar_Emptygridclass);
            ucGridpaginationbar.SetProperty("RowsPerPageSelector", Gridpaginationbar_Rowsperpageselector);
            ucGridpaginationbar.SetProperty("RowsPerPageOptions", Gridpaginationbar_Rowsperpageoptions);
            ucGridpaginationbar.SetProperty("Previous", Gridpaginationbar_Previous);
            ucGridpaginationbar.SetProperty("Next", Gridpaginationbar_Next);
            ucGridpaginationbar.SetProperty("Caption", Gridpaginationbar_Caption);
            ucGridpaginationbar.SetProperty("EmptyGridCaption", Gridpaginationbar_Emptygridcaption);
            ucGridpaginationbar.SetProperty("RowsPerPageCaption", Gridpaginationbar_Rowsperpagecaption);
            ucGridpaginationbar.SetProperty("CurrentPage", AV38GridCurrentPage);
            ucGridpaginationbar.SetProperty("PageCount", AV39GridPageCount);
            ucGridpaginationbar.SetProperty("AppliedFilters", AV40GridAppliedFilters);
            ucGridpaginationbar.Render(context, "dvelop.dvpaginationbar", Gridpaginationbar_Internalname, "GRIDPAGINATIONBARContainer");
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
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 25,'',false,'',0)\"";
            ClassString = "ButtonMaterial";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtnenter_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(12), 2, 0)+","+"null"+");", context.GetMessage( "GX_BtnEnter", ""), bttBtnenter_Jsonclick, 5, context.GetMessage( "GX_BtnEnter", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"EENTER."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMTranslations.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "gx-button", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 27,'',false,'',0)\"";
            ClassString = "ButtonMaterialDefault";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttBtncancel_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(12), 2, 0)+","+"null"+");", context.GetMessage( "GX_BtnCancel", ""), bttBtncancel_Jsonclick, 1, context.GetMessage( "GX_BtnCancel", ""), "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"ECANCEL."+"'", TempTags, "", context.GetButtonType( ), "HLP_GAMTranslations.htm");
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
            /* User Defined Control */
            ucGrid_empowerer.Render(context, "wwp.gridempowerer", Grid_empowerer_Internalname, "GRID_EMPOWERERContainer");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 12 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Grid", GridContainer, subGrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridContainerData", GridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridContainerData"+"V", GridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridContainerData"+"V"+"\" value='"+GridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START162( )
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
         Form.Meta.addItem("description", context.GetMessage( "GAMExampleTranslations", ""), 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP160( ) ;
      }

      protected void WS162( )
      {
         START162( ) ;
         EVT162( ) ;
      }

      protected void EVT162( )
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
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changepage */
                              E11162 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GRIDPAGINATIONBAR.CHANGEROWSPERPAGE") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: Gridpaginationbar.Changerowsperpage */
                              E12162 ();
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
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 7), "REFRESH") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "GRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 9), "'CONFIRM'") == 0 ) )
                           {
                              nGXsfl_12_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
                              SubsflControlProps_122( ) ;
                              AV16LngID = cgiGet( edtavLngid_Internalname);
                              AssignAttri("", false, edtavLngid_Internalname, AV16LngID);
                              GxWebStd.gx_hidden_field( context, "gxhash_vLNGID"+"_"+sGXsfl_12_idx, GetSecureSignedToken( sGXsfl_12_idx, StringUtil.RTrim( context.localUtil.Format( AV16LngID, "")), context));
                              AV15Language = cgiGet( edtavLanguage_Internalname);
                              AssignAttri("", false, edtavLanguage_Internalname, AV15Language);
                              AV17Name = cgiGet( edtavName_Internalname);
                              AssignAttri("", false, edtavName_Internalname, AV17Name);
                              AV22Text = cgiGet( edtavText_Internalname);
                              AssignAttri("", false, edtavText_Internalname, AV22Text);
                              AV6Delete = cgiGet( edtavDelete_Internalname);
                              AssignAttri("", false, edtavDelete_Internalname, AV6Delete);
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E13162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "REFRESH") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Refresh */
                                    E14162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Grid.Load */
                                    E15162 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'CONFIRM'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Confirm' */
                                    E16162 ();
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

      protected void WE162( )
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

      protected void PA162( )
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
               if ( ( StringUtil.StrCmp(StringUtil.Right( GXDecQS, 6), Crypto.CheckSum( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), 6)) == 0 ) && ( StringUtil.StrCmp(StringUtil.Substring( GXDecQS, 1, StringUtil.Len( "gamtranslations.aspx")), "gamtranslations.aspx") == 0 ) )
               {
                  SetQueryString( StringUtil.Right( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)), (short)(StringUtil.Len( StringUtil.Left( GXDecQS, (short)(StringUtil.Len( GXDecQS)-6)))-StringUtil.Len( "gamtranslations.aspx")))) ;
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
                        AV24Type = GetPar( "Type");
                        AssignAttri("", false, "AV24Type", AV24Type);
                        GxWebStd.gx_hidden_field( context, "gxhash_vTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Type, "")), context));
                        AV23Title = GetPar( "Title");
                        AssignAttri("", false, "AV23Title", AV23Title);
                        GxWebStd.gx_hidden_field( context, "gxhash_vTITLE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23Title, "")), context));
                        AV18PrimaryID = (long)(Math.Round(NumberUtil.Val( GetPar( "PrimaryID"), "."), 18, MidpointRounding.ToEven));
                        AssignAttri("", false, "AV18PrimaryID", StringUtil.LTrimStr( (decimal)(AV18PrimaryID), 12, 0));
                        GxWebStd.gx_hidden_field( context, "gxhash_vPRIMARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV18PrimaryID), "ZZZZZZZZZZZ9"), context));
                        AV20SecondaryID = (long)(Math.Round(NumberUtil.Val( GetPar( "SecondaryID"), "."), 18, MidpointRounding.ToEven));
                        AssignAttri("", false, "AV20SecondaryID", StringUtil.LTrimStr( (decimal)(AV20SecondaryID), 12, 0));
                        GxWebStd.gx_hidden_field( context, "gxhash_vSECONDARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV20SecondaryID), "ZZZZZZZZZZZ9"), context));
                        AV21TertiaryID = (long)(Math.Round(NumberUtil.Val( GetPar( "TertiaryID"), "."), 18, MidpointRounding.ToEven));
                        AssignAttri("", false, "AV21TertiaryID", StringUtil.LTrimStr( (decimal)(AV21TertiaryID), 12, 0));
                        GxWebStd.gx_hidden_field( context, "gxhash_vTERTIARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV21TertiaryID), "ZZZZZZZZZZZ9"), context));
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
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_122( ) ;
         while ( nGXsfl_12_idx <= nRC_GXsfl_12 )
         {
            sendrow_122( ) ;
            nGXsfl_12_idx = ((subGrid_Islastpage==1)&&(nGXsfl_12_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_idx+1);
            sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
            SubsflControlProps_122( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridContainer)) ;
         /* End function gxnrGrid_newrow */
      }

      protected void gxgrGrid_refresh( int subGrid_Rows ,
                                       string AV49Pgmname ,
                                       string AV24Type ,
                                       long AV18PrimaryID ,
                                       long AV20SecondaryID ,
                                       long AV21TertiaryID ,
                                       string Gx_mode ,
                                       string AV23Title )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRID_nCurrentRecord = 0;
         RF162( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGrid_refresh */
      }

      protected void send_integrity_hashes( )
      {
         GxWebStd.gx_hidden_field( context, "gxhash_vLNGID", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV16LngID, "")), context));
         GxWebStd.gx_hidden_field( context, "vLNGID", StringUtil.RTrim( AV16LngID));
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
         RF162( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         AV49Pgmname = "GAMTranslations";
         edtavLngid_Enabled = 0;
         edtavLanguage_Enabled = 0;
         edtavName_Enabled = 0;
         edtavText_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      protected void RF162( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridContainer.ClearRows();
         }
         wbStart = 12;
         /* Execute user event: Refresh */
         E14162 ();
         nGXsfl_12_idx = 1;
         sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
         SubsflControlProps_122( ) ;
         bGXsfl_12_Refreshing = true;
         GridContainer.AddObjectProperty("GridName", "Grid");
         GridContainer.AddObjectProperty("CmpContext", "");
         GridContainer.AddObjectProperty("InMasterPage", "false");
         GridContainer.AddObjectProperty("Class", "GridWithPaginationBar GridNoBorder WorkWith");
         GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
         GridContainer.PageSize = subGrid_fnc_Recordsperpage( );
         if ( subGrid_Islastpage != 0 )
         {
            GRID_nFirstRecordOnPage = (long)(subGrid_fnc_Recordcount( )-subGrid_fnc_Recordsperpage( ));
            GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
            GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         }
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_122( ) ;
            /* Execute user event: Grid.Load */
            E15162 ();
            if ( ( subGrid_Islastpage == 0 ) && ( GRID_nCurrentRecord > 0 ) && ( GRID_nGridOutOfScope == 0 ) && ( nGXsfl_12_idx == 1 ) )
            {
               GRID_nCurrentRecord = 0;
               GRID_nGridOutOfScope = 1;
               subgrid_firstpage( ) ;
               /* Execute user event: Grid.Load */
               E15162 ();
            }
            wbEnd = 12;
            WB160( ) ;
         }
         bGXsfl_12_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes162( )
      {
         GxWebStd.gx_hidden_field( context, "vPGMNAME", StringUtil.RTrim( AV49Pgmname));
         GxWebStd.gx_hidden_field( context, "gxhash_vPGMNAME", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV49Pgmname, "")), context));
         GxWebStd.gx_hidden_field( context, "gxhash_vLNGID"+"_"+sGXsfl_12_idx, GetSecureSignedToken( sGXsfl_12_idx, StringUtil.RTrim( context.localUtil.Format( AV16LngID, "")), context));
      }

      protected int subGrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGrid_fnc_Recordcount( )
      {
         return (int)(((subGrid_Recordcount==0) ? GRID_nFirstRecordOnPage+1 : subGrid_Recordcount)) ;
      }

      protected int subGrid_fnc_Recordsperpage( )
      {
         if ( subGrid_Rows > 0 )
         {
            return subGrid_Rows*1 ;
         }
         else
         {
            return (int)(-1) ;
         }
      }

      protected int subGrid_fnc_Currentpage( )
      {
         return (int)(((subGrid_Islastpage==1) ? NumberUtil.Int( (long)(Math.Round(subGrid_fnc_Recordcount( )/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+((((int)((subGrid_fnc_Recordcount( )) % (subGrid_fnc_Recordsperpage( ))))==0) ? 0 : 1) : NumberUtil.Int( (long)(Math.Round(GRID_nFirstRecordOnPage/ (decimal)(subGrid_fnc_Recordsperpage( )), 18, MidpointRounding.ToEven)))+1)) ;
      }

      protected short subgrid_firstpage( )
      {
         GRID_nFirstRecordOnPage = 0;
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV49Pgmname, AV24Type, AV18PrimaryID, AV20SecondaryID, AV21TertiaryID, Gx_mode, AV23Title) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_nextpage( )
      {
         if ( GRID_nEOF == 0 )
         {
            GRID_nFirstRecordOnPage = (long)(GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( ));
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         GridContainer.AddObjectProperty("GRID_nFirstRecordOnPage", GRID_nFirstRecordOnPage);
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV49Pgmname, AV24Type, AV18PrimaryID, AV20SecondaryID, AV21TertiaryID, Gx_mode, AV23Title) ;
         }
         send_integrity_footer_hashes( ) ;
         return (short)(((GRID_nEOF==0) ? 0 : 2)) ;
      }

      protected short subgrid_previouspage( )
      {
         if ( GRID_nFirstRecordOnPage >= subGrid_fnc_Recordsperpage( ) )
         {
            GRID_nFirstRecordOnPage = (long)(GRID_nFirstRecordOnPage-subGrid_fnc_Recordsperpage( ));
         }
         else
         {
            return 2 ;
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV49Pgmname, AV24Type, AV18PrimaryID, AV20SecondaryID, AV21TertiaryID, Gx_mode, AV23Title) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected short subgrid_lastpage( )
      {
         subGrid_Islastpage = 1;
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV49Pgmname, AV24Type, AV18PrimaryID, AV20SecondaryID, AV21TertiaryID, Gx_mode, AV23Title) ;
         }
         send_integrity_footer_hashes( ) ;
         return 0 ;
      }

      protected int subgrid_gotopage( int nPageNo )
      {
         if ( nPageNo > 0 )
         {
            GRID_nFirstRecordOnPage = (long)(subGrid_fnc_Recordsperpage( )*(nPageNo-1));
         }
         else
         {
            GRID_nFirstRecordOnPage = 0;
         }
         GxWebStd.gx_hidden_field( context, "GRID_nFirstRecordOnPage", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nFirstRecordOnPage), 15, 0, ".", "")));
         if ( isFullAjaxMode( ) )
         {
            gxgrGrid_refresh( subGrid_Rows, AV49Pgmname, AV24Type, AV18PrimaryID, AV20SecondaryID, AV21TertiaryID, Gx_mode, AV23Title) ;
         }
         send_integrity_footer_hashes( ) ;
         return (int)(0) ;
      }

      protected void before_start_formulas( )
      {
         AV49Pgmname = "GAMTranslations";
         edtavLngid_Enabled = 0;
         edtavLanguage_Enabled = 0;
         edtavName_Enabled = 0;
         edtavText_Enabled = 0;
         edtavDelete_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP160( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E13162 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            /* Read saved values. */
            nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_12"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV38GridCurrentPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDCURRENTPAGE"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV39GridPageCount = (long)(Math.Round(context.localUtil.CToN( cgiGet( "vGRIDPAGECOUNT"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            AV40GridAppliedFilters = cgiGet( "vGRIDAPPLIEDFILTERS");
            GRID_nFirstRecordOnPage = (long)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_nFirstRecordOnPage"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GRID_nEOF = (short)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_nEOF"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGrid_Recordcount = (int)(Math.Round(context.localUtil.CToN( cgiGet( "subGrid_Recordcount"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Gridpaginationbar_Class = cgiGet( "GRIDPAGINATIONBAR_Class");
            Gridpaginationbar_Showfirst = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Showfirst"));
            Gridpaginationbar_Showprevious = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Showprevious"));
            Gridpaginationbar_Shownext = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Shownext"));
            Gridpaginationbar_Showlast = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Showlast"));
            Gridpaginationbar_Pagestoshow = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDPAGINATIONBAR_Pagestoshow"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Pagingbuttonsposition = cgiGet( "GRIDPAGINATIONBAR_Pagingbuttonsposition");
            Gridpaginationbar_Pagingcaptionposition = cgiGet( "GRIDPAGINATIONBAR_Pagingcaptionposition");
            Gridpaginationbar_Emptygridclass = cgiGet( "GRIDPAGINATIONBAR_Emptygridclass");
            Gridpaginationbar_Rowsperpageselector = StringUtil.StrToBool( cgiGet( "GRIDPAGINATIONBAR_Rowsperpageselector"));
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            Gridpaginationbar_Rowsperpageoptions = cgiGet( "GRIDPAGINATIONBAR_Rowsperpageoptions");
            Gridpaginationbar_Previous = cgiGet( "GRIDPAGINATIONBAR_Previous");
            Gridpaginationbar_Next = cgiGet( "GRIDPAGINATIONBAR_Next");
            Gridpaginationbar_Caption = cgiGet( "GRIDPAGINATIONBAR_Caption");
            Gridpaginationbar_Emptygridcaption = cgiGet( "GRIDPAGINATIONBAR_Emptygridcaption");
            Gridpaginationbar_Rowsperpagecaption = cgiGet( "GRIDPAGINATIONBAR_Rowsperpagecaption");
            Grid_empowerer_Gridinternalname = cgiGet( "GRID_EMPOWERER_Gridinternalname");
            subGrid_Rows = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRID_Rows"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
            Gridpaginationbar_Selectedpage = cgiGet( "GRIDPAGINATIONBAR_Selectedpage");
            Gridpaginationbar_Rowsperpageselectedvalue = (int)(Math.Round(context.localUtil.CToN( cgiGet( "GRIDPAGINATIONBAR_Rowsperpageselectedvalue"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
            /* Read variables values. */
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            /* Check if conditions changed and reset current page numbers */
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E13162 ();
         if (returnInSub) return;
      }

      protected void E13162( )
      {
         /* Start Routine */
         returnInSub = false;
         subGrid_Rows = 10;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         Grid_empowerer_Gridinternalname = subGrid_Internalname;
         ucGrid_empowerer.SendProperty(context, "", false, Grid_empowerer_Internalname, "GridInternalName", Grid_empowerer_Gridinternalname);
         Form.Caption = context.GetMessage( "GAMExampleTranslations", "");
         AssignProp("", false, "FORM", "Caption", Form.Caption, true);
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S112 ();
         if (returnInSub) return;
         Gridpaginationbar_Rowsperpageselectedvalue = subGrid_Rows;
         ucGridpaginationbar.SendProperty(context, "", false, Gridpaginationbar_Internalname, "RowsPerPageSelectedValue", StringUtil.LTrimStr( (decimal)(Gridpaginationbar_Rowsperpageselectedvalue), 9, 0));
         Form.Caption = AV23Title;
         AssignProp("", false, "FORM", "Caption", Form.Caption, true);
      }

      protected void E14162( )
      {
         if ( gx_refresh_fired )
         {
            return  ;
         }
         gx_refresh_fired = true;
         /* Refresh Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV29WWPContext) ;
         /* Execute user subroutine: 'SAVEGRIDSTATE' */
         S122 ();
         if (returnInSub) return;
         AV38GridCurrentPage = subGrid_fnc_Currentpage( );
         AssignAttri("", false, "AV38GridCurrentPage", StringUtil.LTrimStr( (decimal)(AV38GridCurrentPage), 10, 0));
         GXt_char1 = AV40GridAppliedFilters;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getappliedfiltersdescription(context ).execute(  AV49Pgmname, out  GXt_char1) ;
         AV40GridAppliedFilters = GXt_char1;
         AssignAttri("", false, "AV40GridAppliedFilters", AV40GridAppliedFilters);
         /* Execute user subroutine: 'READBYTYPE' */
         S132 ();
         if (returnInSub) return;
         if ( StringUtil.StartsWith( AV24Type, "Application") )
         {
            AV7GAMApplication.load( AV18PrimaryID);
            AV14GAMLanguages = AV7GAMApplication.gxTpr_Languages;
         }
         else
         {
            AV7GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context).get();
            AV14GAMLanguages = AV7GAMApplication.gxTpr_Languages;
         }
         AV39GridPageCount = (long)(AV14GAMLanguages.Count/ (decimal)(subGrid_Rows));
         AssignAttri("", false, "AV39GridPageCount", StringUtil.LTrimStr( (decimal)(AV39GridPageCount), 10, 0));
         /*  Sending Event outputs  */
      }

      protected void E11162( )
      {
         /* Gridpaginationbar_Changepage Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(Gridpaginationbar_Selectedpage, "Previous") == 0 )
         {
            subgrid_previouspage( ) ;
         }
         else if ( StringUtil.StrCmp(Gridpaginationbar_Selectedpage, "Next") == 0 )
         {
            subgrid_nextpage( ) ;
         }
         else
         {
            AV37PageToGo = (int)(Math.Round(NumberUtil.Val( Gridpaginationbar_Selectedpage, "."), 18, MidpointRounding.ToEven));
            subgrid_gotopage( AV37PageToGo) ;
         }
      }

      protected void E12162( )
      {
         /* Gridpaginationbar_Changerowsperpage Routine */
         returnInSub = false;
         subGrid_Rows = Gridpaginationbar_Rowsperpageselectedvalue;
         GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         subgrid_firstpage( ) ;
         /*  Sending Event outputs  */
      }

      private void E15162( )
      {
         /* Grid_Load Routine */
         returnInSub = false;
         AV50GXV1 = 1;
         while ( AV50GXV1 <= AV14GAMLanguages.Count )
         {
            AV13GAMLanguage = ((GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage)AV14GAMLanguages.Item(AV50GXV1));
            AV6Delete = "<i class=\"fa fa-times\"></i>";
            AssignAttri("", false, edtavDelete_Internalname, AV6Delete);
            if ( AV13GAMLanguage.gxTpr_Online )
            {
               AV16LngID = AV13GAMLanguage.gxTpr_Culture;
               AssignAttri("", false, edtavLngid_Internalname, AV16LngID);
               GxWebStd.gx_hidden_field( context, "gxhash_vLNGID"+"_"+sGXsfl_12_idx, GetSecureSignedToken( sGXsfl_12_idx, StringUtil.RTrim( context.localUtil.Format( AV16LngID, "")), context));
               AV15Language = AV13GAMLanguage.gxTpr_Description;
               AssignAttri("", false, edtavLanguage_Internalname, AV15Language);
               AV17Name = "";
               AssignAttri("", false, edtavName_Internalname, AV17Name);
               AV22Text = "";
               AssignAttri("", false, edtavText_Internalname, AV22Text);
               AV51GXV2 = 1;
               while ( AV51GXV2 <= AV11GAMDescriptions.Count )
               {
                  AV10GAMDescription = ((GeneXus.Programs.genexussecurity.SdtGAMDescription)AV11GAMDescriptions.Item(AV51GXV2));
                  if ( StringUtil.StrCmp(StringUtil.Lower( AV16LngID), StringUtil.Lower( AV10GAMDescription.gxTpr_Language)) == 0 )
                  {
                     AV17Name = AV10GAMDescription.gxTpr_Name;
                     AssignAttri("", false, edtavName_Internalname, AV17Name);
                     AV22Text = AV10GAMDescription.gxTpr_Text;
                     AssignAttri("", false, edtavText_Internalname, AV22Text);
                     if (true) break;
                  }
                  AV51GXV2 = (int)(AV51GXV2+1);
               }
               /* Execute user subroutine: 'LOADGRID' */
               S162 ();
               if (returnInSub) return;
               /* Load Method */
               if ( wbStart != -1 )
               {
                  wbStart = 12;
               }
               if ( ( subGrid_Islastpage == 1 ) || ( subGrid_Rows == 0 ) || ( ( GRID_nCurrentRecord >= GRID_nFirstRecordOnPage ) && ( GRID_nCurrentRecord < GRID_nFirstRecordOnPage + subGrid_fnc_Recordsperpage( ) ) ) )
               {
                  sendrow_122( ) ;
               }
               GRID_nEOF = (short)(((GRID_nCurrentRecord<GRID_nFirstRecordOnPage+subGrid_fnc_Recordsperpage( )) ? 1 : 0));
               GxWebStd.gx_hidden_field( context, "GRID_nEOF", StringUtil.LTrim( StringUtil.NToC( (decimal)(GRID_nEOF), 1, 0, ".", "")));
               GRID_nCurrentRecord = (long)(GRID_nCurrentRecord+1);
               subGrid_Recordcount = (int)(GRID_nCurrentRecord);
               if ( isFullAjaxMode( ) && ! bGXsfl_12_Refreshing )
               {
                  DoAjaxLoad(12, GridRow);
               }
            }
            AV50GXV1 = (int)(AV50GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void S112( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV36Session.Get(AV49Pgmname+"GridState"), "") == 0 )
         {
            AV34GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  AV49Pgmname+"GridState"), null, "", "");
         }
         else
         {
            AV34GridState.FromXml(AV36Session.Get(AV49Pgmname+"GridState"), null, "", "");
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV34GridState.gxTpr_Pagesize))) )
         {
            subGrid_Rows = (int)(Math.Round(NumberUtil.Val( AV34GridState.gxTpr_Pagesize, "."), 18, MidpointRounding.ToEven));
            GxWebStd.gx_hidden_field( context, "GRID_Rows", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Rows), 6, 0, ".", "")));
         }
         subgrid_gotopage( AV34GridState.gxTpr_Currentpage) ;
      }

      protected void S122( )
      {
         /* 'SAVEGRIDSTATE' Routine */
         returnInSub = false;
         AV34GridState.FromXml(AV36Session.Get(AV49Pgmname+"GridState"), null, "", "");
         AV34GridState.gxTpr_Pagesize = StringUtil.Str( (decimal)(subGrid_Rows), 10, 0);
         AV34GridState.gxTpr_Currentpage = (short)(subGrid_fnc_Currentpage( ));
         new DesignSystem.Programs.wwpbaseobjects.savegridstate(context ).execute(  AV49Pgmname+"GridState",  AV34GridState.ToXml(false, true, "", "")) ;
      }

      protected void E16162( )
      {
         /* 'Confirm' Routine */
         returnInSub = false;
         /* Execute user subroutine: 'SAVEBYTYPE' */
         S142 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
      }

      protected void S152( )
      {
         /* 'SHOWMESSAGES' Routine */
         returnInSub = false;
         if ( AV5GAMErrorCollection.Count > 0 )
         {
            AV52GXV3 = 1;
            while ( AV52GXV3 <= AV5GAMErrorCollection.Count )
            {
               AV41Error = ((GeneXus.Programs.genexussecurity.SdtGAMError)AV5GAMErrorCollection.Item(AV52GXV3));
               GX_msglist.addItem(StringUtil.Format( "%1 (GAM%2)", AV41Error.gxTpr_Message, StringUtil.LTrimStr( (decimal)(AV41Error.gxTpr_Code), 12, 0), "", "", "", "", "", "", ""));
               AV52GXV3 = (int)(AV52GXV3+1);
            }
         }
      }

      protected void S162( )
      {
         /* 'LOADGRID' Routine */
         returnInSub = false;
         AV6Delete = context.GetMessage( "GAM_Delete", "");
         AssignAttri("", false, edtavDelete_Internalname, AV6Delete);
         if ( ( StringUtil.StrCmp(Gx_mode, "DSP") == 0 ) || ( StringUtil.StrCmp(Gx_mode, "DLT") == 0 ) )
         {
            edtavLanguage_Enabled = 0;
            AssignProp("", false, edtavLanguage_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavLanguage_Enabled), 5, 0), !bGXsfl_12_Refreshing);
            edtavName_Enabled = 0;
            AssignProp("", false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), !bGXsfl_12_Refreshing);
            edtavText_Enabled = 0;
            AssignProp("", false, edtavText_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavText_Enabled), 5, 0), !bGXsfl_12_Refreshing);
            edtavDelete_Visible = 0;
            AssignProp("", false, edtavDelete_Internalname, "Visible", StringUtil.LTrimStr( (decimal)(edtavDelete_Visible), 5, 0), !bGXsfl_12_Refreshing);
         }
         else
         {
            edtavName_Enabled = 1;
            AssignProp("", false, edtavName_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavName_Enabled), 5, 0), !bGXsfl_12_Refreshing);
            edtavText_Enabled = 1;
            AssignProp("", false, edtavText_Internalname, "Enabled", StringUtil.LTrimStr( (decimal)(edtavText_Enabled), 5, 0), !bGXsfl_12_Refreshing);
         }
      }

      protected void S132( )
      {
         /* 'READBYTYPE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV24Type, "Application") == 0 )
         {
            AV7GAMApplication.load( AV18PrimaryID);
            if ( AV7GAMApplication.success() )
            {
               AV11GAMDescriptions = AV7GAMApplication.gxTpr_Descriptions;
            }
            else
            {
               AV12GAMErrorColletion = AV7GAMApplication.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "ApplicationMenu") == 0 )
         {
            AV7GAMApplication.load( AV18PrimaryID);
            if ( AV7GAMApplication.success() )
            {
               AV8GAMApplicationMenu = AV7GAMApplication.getmenu(AV20SecondaryID, out  AV5GAMErrorCollection);
               if ( AV5GAMErrorCollection.Count == 0 )
               {
                  AV11GAMDescriptions = AV8GAMApplicationMenu.gxTpr_Descriptions;
               }
               else
               {
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S152 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               AV12GAMErrorColletion = AV7GAMApplication.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "ApplicationMenuOption") == 0 )
         {
            AV7GAMApplication.load( AV18PrimaryID);
            if ( AV7GAMApplication.success() )
            {
               AV8GAMApplicationMenu = AV7GAMApplication.getmenu(AV20SecondaryID, out  AV5GAMErrorCollection);
               if ( AV5GAMErrorCollection.Count == 0 )
               {
                  AV9GAMApplicationMenuOption = AV8GAMApplicationMenu.getmenuoptionbyid(AV18PrimaryID, AV21TertiaryID, out  AV5GAMErrorCollection);
                  if ( AV5GAMErrorCollection.Count == 0 )
                  {
                     AV11GAMDescriptions = AV9GAMApplicationMenuOption.gxTpr_Descriptions;
                  }
                  else
                  {
                     /* Execute user subroutine: 'SHOWMESSAGES' */
                     S152 ();
                     if (returnInSub) return;
                  }
               }
               else
               {
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S152 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               AV12GAMErrorColletion = AV7GAMApplication.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "Role") == 0 )
         {
            AV25GAMRole.load( AV18PrimaryID);
            if ( AV25GAMRole.success() )
            {
               AV11GAMDescriptions = AV25GAMRole.gxTpr_Descriptions;
            }
            else
            {
               AV12GAMErrorColletion = AV25GAMRole.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "SecurityPolicy") == 0 )
         {
            AV26GAMSecurityPolicy.load( (int)(AV18PrimaryID));
            if ( AV26GAMSecurityPolicy.success() )
            {
               AV11GAMDescriptions = AV26GAMSecurityPolicy.gxTpr_Descriptions;
            }
            else
            {
               AV12GAMErrorColletion = AV26GAMSecurityPolicy.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
      }

      protected void S142( )
      {
         /* 'SAVEBYTYPE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV24Type, "Application") == 0 )
         {
            AV7GAMApplication.load( AV18PrimaryID);
            if ( AV7GAMApplication.success() )
            {
               AV7GAMApplication.gxTpr_Descriptions.Clear();
               /* Start For Each Line */
               nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_12"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               nGXsfl_12_fel_idx = 0;
               while ( nGXsfl_12_fel_idx < nRC_GXsfl_12 )
               {
                  nGXsfl_12_fel_idx = ((subGrid_Islastpage==1)&&(nGXsfl_12_fel_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_fel_idx+1);
                  sGXsfl_12_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_fel_idx), 4, 0), 4, "0");
                  SubsflControlProps_fel_122( ) ;
                  AV16LngID = cgiGet( edtavLngid_Internalname);
                  AV15Language = cgiGet( edtavLanguage_Internalname);
                  AV17Name = cgiGet( edtavName_Internalname);
                  AV22Text = cgiGet( edtavText_Internalname);
                  AV6Delete = cgiGet( edtavDelete_Internalname);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17Name)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV22Text)) )
                  {
                     AV10GAMDescription = new GeneXus.Programs.genexussecurity.SdtGAMDescription(context);
                     AV10GAMDescription.gxTpr_Language = AV16LngID;
                     AV10GAMDescription.gxTpr_Name = AV17Name;
                     AV10GAMDescription.gxTpr_Text = AV22Text;
                     AV7GAMApplication.gxTpr_Descriptions.Add(AV10GAMDescription, 0);
                  }
                  /* End For Each Line */
               }
               if ( nGXsfl_12_fel_idx == 0 )
               {
                  nGXsfl_12_idx = 1;
                  sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
                  SubsflControlProps_122( ) ;
               }
               nGXsfl_12_fel_idx = 1;
               AV7GAMApplication.save();
               if ( AV7GAMApplication.success() )
               {
                  context.CommitDataStores("gamtranslations",pr_default);
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
               else
               {
                  AV12GAMErrorColletion = AV7GAMApplication.geterrors();
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S152 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               AV12GAMErrorColletion = AV7GAMApplication.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "ApplicationMenu") == 0 )
         {
            AV7GAMApplication.load( AV18PrimaryID);
            if ( AV7GAMApplication.success() )
            {
               AV8GAMApplicationMenu = AV7GAMApplication.getmenu(AV20SecondaryID, out  AV5GAMErrorCollection);
               if ( AV5GAMErrorCollection.Count == 0 )
               {
                  AV8GAMApplicationMenu.gxTpr_Descriptions.Clear();
                  /* Start For Each Line */
                  nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_12"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                  nGXsfl_12_fel_idx = 0;
                  while ( nGXsfl_12_fel_idx < nRC_GXsfl_12 )
                  {
                     nGXsfl_12_fel_idx = ((subGrid_Islastpage==1)&&(nGXsfl_12_fel_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_fel_idx+1);
                     sGXsfl_12_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_fel_idx), 4, 0), 4, "0");
                     SubsflControlProps_fel_122( ) ;
                     AV16LngID = cgiGet( edtavLngid_Internalname);
                     AV15Language = cgiGet( edtavLanguage_Internalname);
                     AV17Name = cgiGet( edtavName_Internalname);
                     AV22Text = cgiGet( edtavText_Internalname);
                     AV6Delete = cgiGet( edtavDelete_Internalname);
                     if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17Name)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV22Text)) )
                     {
                        AV10GAMDescription = new GeneXus.Programs.genexussecurity.SdtGAMDescription(context);
                        AV10GAMDescription.gxTpr_Language = AV16LngID;
                        AV10GAMDescription.gxTpr_Name = AV17Name;
                        AV10GAMDescription.gxTpr_Text = AV22Text;
                        AV8GAMApplicationMenu.gxTpr_Descriptions.Add(AV10GAMDescription, 0);
                     }
                     /* End For Each Line */
                  }
                  if ( nGXsfl_12_fel_idx == 0 )
                  {
                     nGXsfl_12_idx = 1;
                     sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
                     SubsflControlProps_122( ) ;
                  }
                  nGXsfl_12_fel_idx = 1;
                  if ( AV7GAMApplication.updatemenu(AV8GAMApplicationMenu, out  AV12GAMErrorColletion) )
                  {
                     context.CommitDataStores("gamtranslations",pr_default);
                     context.setWebReturnParms(new Object[] {});
                     context.setWebReturnParmsMetadata(new Object[] {});
                     context.wjLocDisableFrm = 1;
                     context.nUserReturn = 1;
                     returnInSub = true;
                     if (true) return;
                  }
                  else
                  {
                     /* Execute user subroutine: 'SHOWMESSAGES' */
                     S152 ();
                     if (returnInSub) return;
                  }
               }
               else
               {
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S152 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               AV12GAMErrorColletion = AV7GAMApplication.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "ApplicationMenuOption") == 0 )
         {
            AV7GAMApplication.load( AV18PrimaryID);
            if ( AV7GAMApplication.success() )
            {
               AV8GAMApplicationMenu = AV7GAMApplication.getmenu(AV20SecondaryID, out  AV5GAMErrorCollection);
               if ( AV5GAMErrorCollection.Count == 0 )
               {
                  AV9GAMApplicationMenuOption = AV7GAMApplication.getmenuoption(AV20SecondaryID, AV21TertiaryID, out  AV5GAMErrorCollection);
                  if ( AV5GAMErrorCollection.Count == 0 )
                  {
                     AV9GAMApplicationMenuOption.gxTpr_Descriptions.Clear();
                     /* Start For Each Line */
                     nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_12"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
                     nGXsfl_12_fel_idx = 0;
                     while ( nGXsfl_12_fel_idx < nRC_GXsfl_12 )
                     {
                        nGXsfl_12_fel_idx = ((subGrid_Islastpage==1)&&(nGXsfl_12_fel_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_fel_idx+1);
                        sGXsfl_12_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_fel_idx), 4, 0), 4, "0");
                        SubsflControlProps_fel_122( ) ;
                        AV16LngID = cgiGet( edtavLngid_Internalname);
                        AV15Language = cgiGet( edtavLanguage_Internalname);
                        AV17Name = cgiGet( edtavName_Internalname);
                        AV22Text = cgiGet( edtavText_Internalname);
                        AV6Delete = cgiGet( edtavDelete_Internalname);
                        if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17Name)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV22Text)) )
                        {
                           AV10GAMDescription = new GeneXus.Programs.genexussecurity.SdtGAMDescription(context);
                           AV10GAMDescription.gxTpr_Language = AV16LngID;
                           AV10GAMDescription.gxTpr_Name = AV17Name;
                           AV10GAMDescription.gxTpr_Text = AV22Text;
                           AV9GAMApplicationMenuOption.gxTpr_Descriptions.Add(AV10GAMDescription, 0);
                        }
                        /* End For Each Line */
                     }
                     if ( nGXsfl_12_fel_idx == 0 )
                     {
                        nGXsfl_12_idx = 1;
                        sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
                        SubsflControlProps_122( ) ;
                     }
                     nGXsfl_12_fel_idx = 1;
                     if ( AV7GAMApplication.updatemenuoption(AV20SecondaryID, AV9GAMApplicationMenuOption, out  AV5GAMErrorCollection) )
                     {
                        context.CommitDataStores("gamtranslations",pr_default);
                        context.setWebReturnParms(new Object[] {});
                        context.setWebReturnParmsMetadata(new Object[] {});
                        context.wjLocDisableFrm = 1;
                        context.nUserReturn = 1;
                        returnInSub = true;
                        if (true) return;
                     }
                     else
                     {
                        /* Execute user subroutine: 'SHOWMESSAGES' */
                        S152 ();
                        if (returnInSub) return;
                     }
                  }
                  else
                  {
                     /* Execute user subroutine: 'SHOWMESSAGES' */
                     S152 ();
                     if (returnInSub) return;
                  }
               }
               else
               {
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S152 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               AV12GAMErrorColletion = AV7GAMApplication.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "Role") == 0 )
         {
            AV25GAMRole.load( AV18PrimaryID);
            if ( AV25GAMRole.success() )
            {
               AV25GAMRole.gxTpr_Descriptions.Clear();
               /* Start For Each Line */
               nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_12"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               nGXsfl_12_fel_idx = 0;
               while ( nGXsfl_12_fel_idx < nRC_GXsfl_12 )
               {
                  nGXsfl_12_fel_idx = ((subGrid_Islastpage==1)&&(nGXsfl_12_fel_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_fel_idx+1);
                  sGXsfl_12_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_fel_idx), 4, 0), 4, "0");
                  SubsflControlProps_fel_122( ) ;
                  AV16LngID = cgiGet( edtavLngid_Internalname);
                  AV15Language = cgiGet( edtavLanguage_Internalname);
                  AV17Name = cgiGet( edtavName_Internalname);
                  AV22Text = cgiGet( edtavText_Internalname);
                  AV6Delete = cgiGet( edtavDelete_Internalname);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17Name)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV22Text)) )
                  {
                     AV10GAMDescription = new GeneXus.Programs.genexussecurity.SdtGAMDescription(context);
                     AV10GAMDescription.gxTpr_Language = AV16LngID;
                     AV10GAMDescription.gxTpr_Name = AV17Name;
                     AV10GAMDescription.gxTpr_Text = AV22Text;
                     AV25GAMRole.gxTpr_Descriptions.Add(AV10GAMDescription, 0);
                  }
                  /* End For Each Line */
               }
               if ( nGXsfl_12_fel_idx == 0 )
               {
                  nGXsfl_12_idx = 1;
                  sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
                  SubsflControlProps_122( ) ;
               }
               nGXsfl_12_fel_idx = 1;
               AV25GAMRole.save();
               if ( AV25GAMRole.success() )
               {
                  context.CommitDataStores("gamtranslations",pr_default);
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
               else
               {
                  AV12GAMErrorColletion = AV25GAMRole.geterrors();
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S152 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               AV12GAMErrorColletion = AV25GAMRole.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
         else if ( StringUtil.StrCmp(AV24Type, "SecurityPolicy") == 0 )
         {
            AV26GAMSecurityPolicy.load( (int)(AV18PrimaryID));
            if ( AV26GAMSecurityPolicy.success() )
            {
               AV26GAMSecurityPolicy.gxTpr_Descriptions.Clear();
               /* Start For Each Line */
               nRC_GXsfl_12 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_12"), context.GetLanguageProperty( "decimal_point"), context.GetLanguageProperty( "thousand_sep")), 18, MidpointRounding.ToEven));
               nGXsfl_12_fel_idx = 0;
               while ( nGXsfl_12_fel_idx < nRC_GXsfl_12 )
               {
                  nGXsfl_12_fel_idx = ((subGrid_Islastpage==1)&&(nGXsfl_12_fel_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_fel_idx+1);
                  sGXsfl_12_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_fel_idx), 4, 0), 4, "0");
                  SubsflControlProps_fel_122( ) ;
                  AV16LngID = cgiGet( edtavLngid_Internalname);
                  AV15Language = cgiGet( edtavLanguage_Internalname);
                  AV17Name = cgiGet( edtavName_Internalname);
                  AV22Text = cgiGet( edtavText_Internalname);
                  AV6Delete = cgiGet( edtavDelete_Internalname);
                  if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17Name)) || ! String.IsNullOrEmpty(StringUtil.RTrim( AV22Text)) )
                  {
                     AV10GAMDescription = new GeneXus.Programs.genexussecurity.SdtGAMDescription(context);
                     AV10GAMDescription.gxTpr_Language = AV16LngID;
                     AV10GAMDescription.gxTpr_Name = AV17Name;
                     AV10GAMDescription.gxTpr_Text = AV22Text;
                     AV26GAMSecurityPolicy.gxTpr_Descriptions.Add(AV10GAMDescription, 0);
                  }
                  /* End For Each Line */
               }
               if ( nGXsfl_12_fel_idx == 0 )
               {
                  nGXsfl_12_idx = 1;
                  sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
                  SubsflControlProps_122( ) ;
               }
               nGXsfl_12_fel_idx = 1;
               AV26GAMSecurityPolicy.save();
               if ( AV26GAMSecurityPolicy.success() )
               {
                  context.CommitDataStores("gamtranslations",pr_default);
                  context.setWebReturnParms(new Object[] {});
                  context.setWebReturnParmsMetadata(new Object[] {});
                  context.wjLocDisableFrm = 1;
                  context.nUserReturn = 1;
                  returnInSub = true;
                  if (true) return;
               }
               else
               {
                  AV12GAMErrorColletion = AV26GAMSecurityPolicy.geterrors();
                  /* Execute user subroutine: 'SHOWMESSAGES' */
                  S152 ();
                  if (returnInSub) return;
               }
            }
            else
            {
               AV12GAMErrorColletion = AV26GAMSecurityPolicy.geterrors();
               /* Execute user subroutine: 'SHOWMESSAGES' */
               S152 ();
               if (returnInSub) return;
            }
         }
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
         Gx_mode = (string)getParm(obj,0);
         AssignAttri("", false, "Gx_mode", Gx_mode);
         AV24Type = (string)getParm(obj,1);
         AssignAttri("", false, "AV24Type", AV24Type);
         GxWebStd.gx_hidden_field( context, "gxhash_vTYPE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV24Type, "")), context));
         AV23Title = (string)getParm(obj,2);
         AssignAttri("", false, "AV23Title", AV23Title);
         GxWebStd.gx_hidden_field( context, "gxhash_vTITLE", GetSecureSignedToken( "", StringUtil.RTrim( context.localUtil.Format( AV23Title, "")), context));
         AV18PrimaryID = Convert.ToInt64(getParm(obj,3));
         AssignAttri("", false, "AV18PrimaryID", StringUtil.LTrimStr( (decimal)(AV18PrimaryID), 12, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vPRIMARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV18PrimaryID), "ZZZZZZZZZZZ9"), context));
         AV20SecondaryID = Convert.ToInt64(getParm(obj,4));
         AssignAttri("", false, "AV20SecondaryID", StringUtil.LTrimStr( (decimal)(AV20SecondaryID), 12, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vSECONDARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV20SecondaryID), "ZZZZZZZZZZZ9"), context));
         AV21TertiaryID = Convert.ToInt64(getParm(obj,5));
         AssignAttri("", false, "AV21TertiaryID", StringUtil.LTrimStr( (decimal)(AV21TertiaryID), 12, 0));
         GxWebStd.gx_hidden_field( context, "gxhash_vTERTIARYID", GetSecureSignedToken( "", context.localUtil.Format( (decimal)(AV21TertiaryID), "ZZZZZZZZZZZ9"), context));
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
         PA162( ) ;
         WS162( ) ;
         WE162( ) ;
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
         AddStyleSheetFile("DVelop/DVPaginationBar/DVPaginationBar.css", "");
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?2024121623504767", true, true);
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
         context.AddJavascriptSource("gamtranslations.js", "?2024121623504769", false, true);
         context.AddJavascriptSource("DVelop/Bootstrap/Shared/DVelopBootstrap.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/DVPaginationBar/DVPaginationBarRender.js", "", false, true);
         context.AddJavascriptSource("DVelop/Shared/WorkWithPlusCommon.js", "", false, true);
         context.AddJavascriptSource("DVelop/GridEmpowerer/GridEmpowererRender.js", "", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_122( )
      {
         edtavLngid_Internalname = "vLNGID_"+sGXsfl_12_idx;
         edtavLanguage_Internalname = "vLANGUAGE_"+sGXsfl_12_idx;
         edtavName_Internalname = "vNAME_"+sGXsfl_12_idx;
         edtavText_Internalname = "vTEXT_"+sGXsfl_12_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_12_idx;
      }

      protected void SubsflControlProps_fel_122( )
      {
         edtavLngid_Internalname = "vLNGID_"+sGXsfl_12_fel_idx;
         edtavLanguage_Internalname = "vLANGUAGE_"+sGXsfl_12_fel_idx;
         edtavName_Internalname = "vNAME_"+sGXsfl_12_fel_idx;
         edtavText_Internalname = "vTEXT_"+sGXsfl_12_fel_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_12_fel_idx;
      }

      protected void sendrow_122( )
      {
         sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
         SubsflControlProps_122( ) ;
         WB160( ) ;
         if ( ( subGrid_Rows * 1 == 0 ) || ( nGXsfl_12_idx <= subGrid_fnc_Recordsperpage( ) * 1 ) )
         {
            GridRow = GXWebRow.GetNew(context,GridContainer);
            if ( subGrid_Backcolorstyle == 0 )
            {
               /* None style subfile background logic. */
               subGrid_Backstyle = 0;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Odd";
               }
            }
            else if ( subGrid_Backcolorstyle == 1 )
            {
               /* Uniform style subfile background logic. */
               subGrid_Backstyle = 0;
               subGrid_Backcolor = subGrid_Allbackcolor;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Uniform";
               }
            }
            else if ( subGrid_Backcolorstyle == 2 )
            {
               /* Header style subfile background logic. */
               subGrid_Backstyle = 1;
               if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Odd";
               }
               subGrid_Backcolor = (int)(0x0);
            }
            else if ( subGrid_Backcolorstyle == 3 )
            {
               /* Report style subfile background logic. */
               subGrid_Backstyle = 1;
               if ( ((int)((nGXsfl_12_idx) % (2))) == 0 )
               {
                  subGrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Even";
                  }
               }
               else
               {
                  subGrid_Backcolor = (int)(0x0);
                  if ( StringUtil.StrCmp(subGrid_Class, "") != 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Odd";
                  }
               }
            }
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<tr ") ;
               context.WriteHtmlText( " class=\""+"GridWithPaginationBar GridNoBorder WorkWith"+"\" style=\""+""+"\"") ;
               context.WriteHtmlText( " gxrow=\""+sGXsfl_12_idx+"\">") ;
            }
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+"display:none;"+"\">") ;
            }
            /* Single line edit */
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavLngid_Internalname,StringUtil.RTrim( AV16LngID),(string)"",""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavLngid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)0,(int)edtavLngid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)15,(short)0,(short)0,(short)12,(short)0,(short)-1,(short)0,(bool)true,(string)"GeneXusSecurityCommon\\GAMLanguageCulture",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'',false,'" + sGXsfl_12_idx + "',12)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavLanguage_Internalname,(string)AV15Language,(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,14);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavLanguage_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)-1,(int)edtavLanguage_Enabled,(short)1,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)40,(short)0,(short)0,(short)12,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'" + sGXsfl_12_idx + "',12)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavName_Internalname,StringUtil.RTrim( AV17Name),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,15);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavName_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)-1,(int)edtavName_Enabled,(short)1,(string)"text",(string)"",(short)570,(string)"px",(short)17,(string)"px",(short)254,(short)0,(short)0,(short)12,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusSecurityCommon\\GAMDescriptionLong",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'" + sGXsfl_12_idx + "',12)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavText_Internalname,StringUtil.RTrim( AV22Text),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,16);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavText_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWColumn",(string)"",(short)-1,(int)edtavText_Enabled,(short)1,(string)"text",(string)"",(short)570,(string)"px",(short)17,(string)"px",(short)254,(short)0,(short)0,(short)12,(short)0,(short)-1,(short)-1,(bool)true,(string)"GeneXusSecurityCommon\\GAMDescriptionLong",(string)"start",(bool)true,(string)""});
            /* Subfile cell */
            if ( GridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavDelete_Visible==0) ? "display:none;" : "")+"\">") ;
            }
            /* Single line edit */
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'" + sGXsfl_12_idx + "',12)\"";
            ROClassString = "Attribute";
            GridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDelete_Internalname,StringUtil.RTrim( AV6Delete),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"",(string)"'"+""+"'"+",false,"+"'"+"e17162_client"+"'",(string)"",(string)"",context.GetMessage( "GX_BtnDelete", ""),(string)"",(string)edtavDelete_Jsonclick,(short)7,(string)"Attribute",(string)"",(string)ROClassString,(string)"WWIconActionColumn",(string)"",(int)edtavDelete_Visible,(int)edtavDelete_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)1,(short)12,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
            send_integrity_lvl_hashes162( ) ;
            GridContainer.AddRow(GridRow);
            nGXsfl_12_idx = ((subGrid_Islastpage==1)&&(nGXsfl_12_idx+1>subGrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_12_idx+1);
            sGXsfl_12_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_12_idx), 4, 0), 4, "0");
            SubsflControlProps_122( ) ;
         }
         /* End function sendrow_122 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl12( )
      {
         if ( GridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridContainer"+"DivS\" data-gxgridid=\"12\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGrid_Internalname, subGrid_Internalname, "", "GridWithPaginationBar GridNoBorder WorkWith", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGrid_Backcolorstyle == 0 )
            {
               subGrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subGrid_Class) > 0 )
               {
                  subGrid_Linesclass = subGrid_Class+"Title";
               }
            }
            else
            {
               subGrid_Titlebackstyle = 1;
               if ( subGrid_Backcolorstyle == 1 )
               {
                  subGrid_Titlebackcolor = subGrid_Allbackcolor;
                  if ( StringUtil.Len( subGrid_Class) > 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGrid_Class) > 0 )
                  {
                     subGrid_Linesclass = subGrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Language", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" width="+StringUtil.LTrimStr( (decimal)(570), 4, 0)+"px"+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Name", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" width="+StringUtil.LTrimStr( (decimal)(570), 4, 0)+"px"+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( context.GetMessage( "WWP_GAM_Description", "")) ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavDelete_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridContainer.AddObjectProperty("GridName", "Grid");
         }
         else
         {
            GridContainer.AddObjectProperty("GridName", "Grid");
            GridContainer.AddObjectProperty("Header", subGrid_Header);
            GridContainer.AddObjectProperty("Class", "GridWithPaginationBar GridNoBorder WorkWith");
            GridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Backcolorstyle), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("CmpContext", "");
            GridContainer.AddObjectProperty("InMasterPage", "false");
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV16LngID)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavLngid_Enabled), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( AV15Language));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavLanguage_Enabled), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV17Name)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavName_Enabled), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV22Text)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavText_Enabled), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV6Delete)));
            GridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDelete_Enabled), 5, 0, ".", "")));
            GridColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDelete_Visible), 5, 0, ".", "")));
            GridContainer.AddColumnProperties(GridColumn);
            GridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Selectedindex), 4, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowselection), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Selectioncolor), 9, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowhovering), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Hoveringcolor), 9, 0, ".", "")));
            GridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Allowcollapsing), 1, 0, ".", "")));
            GridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavLngid_Internalname = "vLNGID";
         edtavLanguage_Internalname = "vLANGUAGE";
         edtavName_Internalname = "vNAME";
         edtavText_Internalname = "vTEXT";
         edtavDelete_Internalname = "vDELETE";
         Gridpaginationbar_Internalname = "GRIDPAGINATIONBAR";
         divGridtablewithpaginationbar_Internalname = "GRIDTABLEWITHPAGINATIONBAR";
         bttBtnenter_Internalname = "BTNENTER";
         bttBtncancel_Internalname = "BTNCANCEL";
         divTablemain_Internalname = "TABLEMAIN";
         Grid_empowerer_Internalname = "GRID_EMPOWERER";
         divHtml_bottomauxiliarcontrols_Internalname = "HTML_BOTTOMAUXILIARCONTROLS";
         divLayoutmaintable_Internalname = "LAYOUTMAINTABLE";
         Form.Internalname = "FORM";
         subGrid_Internalname = "GRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("WorkWithPlusDS", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGrid_Allowcollapsing = 0;
         subGrid_Allowselection = 0;
         subGrid_Header = "";
         edtavDelete_Jsonclick = "";
         edtavDelete_Enabled = 1;
         edtavText_Jsonclick = "";
         edtavName_Jsonclick = "";
         edtavLanguage_Jsonclick = "";
         edtavLngid_Jsonclick = "";
         edtavLngid_Enabled = 1;
         subGrid_Class = "GridWithPaginationBar GridNoBorder WorkWith";
         subGrid_Backcolorstyle = 0;
         edtavDelete_Visible = -1;
         edtavText_Enabled = 1;
         edtavName_Enabled = 1;
         edtavLanguage_Enabled = 1;
         Gridpaginationbar_Rowsperpagecaption = "WWP_PagingRowsPerPage";
         Gridpaginationbar_Emptygridcaption = "WWP_PagingEmptyGridCaption";
         Gridpaginationbar_Caption = context.GetMessage( "WWP_PagingCaption", "");
         Gridpaginationbar_Next = "WWP_PagingNextCaption";
         Gridpaginationbar_Previous = "WWP_PagingPreviousCaption";
         Gridpaginationbar_Rowsperpageoptions = "5:WWP_Rows5,10:WWP_Rows10,20:WWP_Rows20,50:WWP_Rows50";
         Gridpaginationbar_Rowsperpageselectedvalue = 10;
         Gridpaginationbar_Rowsperpageselector = Convert.ToBoolean( -1);
         Gridpaginationbar_Emptygridclass = "PaginationBarEmptyGrid";
         Gridpaginationbar_Pagingcaptionposition = "Left";
         Gridpaginationbar_Pagingbuttonsposition = "Right";
         Gridpaginationbar_Pagestoshow = 5;
         Gridpaginationbar_Showlast = Convert.ToBoolean( 0);
         Gridpaginationbar_Shownext = Convert.ToBoolean( -1);
         Gridpaginationbar_Showprevious = Convert.ToBoolean( -1);
         Gridpaginationbar_Showfirst = Convert.ToBoolean( 0);
         Gridpaginationbar_Class = "PaginationBar";
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = context.GetMessage( "GAMExampleTranslations", "");
         subGrid_Rows = 0;
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV49Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV24Type","fld":"vTYPE","hsh":true},{"av":"AV18PrimaryID","fld":"vPRIMARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV20SecondaryID","fld":"vSECONDARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV21TertiaryID","fld":"vTERTIARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV23Title","fld":"vTITLE","hsh":true}]""");
         setEventMetadata("REFRESH",""","oparms":[{"av":"AV38GridCurrentPage","fld":"vGRIDCURRENTPAGE","pic":"ZZZZZZZZZ9"},{"av":"AV40GridAppliedFilters","fld":"vGRIDAPPLIEDFILTERS"},{"av":"AV39GridPageCount","fld":"vGRIDPAGECOUNT","pic":"ZZZZZZZZZ9"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEPAGE","""{"handler":"E11162","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV49Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV24Type","fld":"vTYPE","hsh":true},{"av":"AV18PrimaryID","fld":"vPRIMARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV20SecondaryID","fld":"vSECONDARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV21TertiaryID","fld":"vTERTIARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV23Title","fld":"vTITLE","hsh":true},{"av":"Gridpaginationbar_Selectedpage","ctrl":"GRIDPAGINATIONBAR","prop":"SelectedPage"}]}""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE","""{"handler":"E12162","iparms":[{"av":"GRID_nFirstRecordOnPage"},{"av":"GRID_nEOF"},{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"},{"av":"AV49Pgmname","fld":"vPGMNAME","hsh":true},{"av":"AV24Type","fld":"vTYPE","hsh":true},{"av":"AV18PrimaryID","fld":"vPRIMARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV20SecondaryID","fld":"vSECONDARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV21TertiaryID","fld":"vTERTIARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true},{"av":"AV23Title","fld":"vTITLE","hsh":true},{"av":"Gridpaginationbar_Rowsperpageselectedvalue","ctrl":"GRIDPAGINATIONBAR","prop":"RowsPerPageSelectedValue"}]""");
         setEventMetadata("GRIDPAGINATIONBAR.CHANGEROWSPERPAGE",""","oparms":[{"av":"subGrid_Rows","ctrl":"GRID","prop":"Rows"}]}""");
         setEventMetadata("GRID.LOAD","""{"handler":"E15162","iparms":[{"av":"Gx_mode","fld":"vMODE","pic":"@!","hsh":true}]""");
         setEventMetadata("GRID.LOAD",""","oparms":[{"av":"AV6Delete","fld":"vDELETE"},{"av":"AV16LngID","fld":"vLNGID","hsh":true},{"av":"AV15Language","fld":"vLANGUAGE"},{"av":"AV17Name","fld":"vNAME"},{"av":"AV22Text","fld":"vTEXT"},{"av":"edtavLanguage_Enabled","ctrl":"vLANGUAGE","prop":"Enabled"},{"av":"edtavDelete_Visible","ctrl":"vDELETE","prop":"Visible"},{"av":"edtavName_Enabled","ctrl":"vNAME","prop":"Enabled"},{"av":"edtavText_Enabled","ctrl":"vTEXT","prop":"Enabled"}]}""");
         setEventMetadata("'CONFIRM'","""{"handler":"E16162","iparms":[{"av":"AV24Type","fld":"vTYPE","hsh":true},{"av":"AV18PrimaryID","fld":"vPRIMARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV17Name","fld":"vNAME","grid":12},{"av":"GRID_nFirstRecordOnPage"},{"av":"nRC_GXsfl_12","ctrl":"GRID","grid":12,"prop":"GridRC","grid":12},{"av":"AV22Text","fld":"vTEXT","grid":12},{"av":"AV16LngID","fld":"vLNGID","grid":12,"hsh":true},{"av":"AV20SecondaryID","fld":"vSECONDARYID","pic":"ZZZZZZZZZZZ9","hsh":true},{"av":"AV21TertiaryID","fld":"vTERTIARYID","pic":"ZZZZZZZZZZZ9","hsh":true}]}""");
         setEventMetadata("VDELETE.CLICK","""{"handler":"E17162","iparms":[]""");
         setEventMetadata("VDELETE.CLICK",""","oparms":[{"av":"AV17Name","fld":"vNAME"},{"av":"AV22Text","fld":"vTEXT"}]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Delete","iparms":[]}""");
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
         wcpOGx_mode = "";
         wcpOAV24Type = "";
         wcpOAV23Title = "";
         Gridpaginationbar_Selectedpage = "";
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV49Pgmname = "";
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         GXEncryptionTmp = "";
         AV40GridAppliedFilters = "";
         Grid_empowerer_Gridinternalname = "";
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         GridContainer = new GXWebGrid( context);
         sStyleString = "";
         ucGridpaginationbar = new GXUserControl();
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttBtnenter_Jsonclick = "";
         bttBtncancel_Jsonclick = "";
         ucGrid_empowerer = new GXUserControl();
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV16LngID = "";
         AV15Language = "";
         AV17Name = "";
         AV22Text = "";
         AV6Delete = "";
         GXDecQS = "";
         AV29WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         GXt_char1 = "";
         AV7GAMApplication = new GeneXus.Programs.genexussecurity.SdtGAMApplication(context);
         AV14GAMLanguages = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage>( context, "GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage", "DesignSystem.Programs");
         AV13GAMLanguage = new GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage(context);
         AV11GAMDescriptions = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMDescription>( context, "GeneXus.Programs.genexussecurity.SdtGAMDescription", "DesignSystem.Programs");
         AV10GAMDescription = new GeneXus.Programs.genexussecurity.SdtGAMDescription(context);
         GridRow = new GXWebRow();
         AV36Session = context.GetSession();
         AV34GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV5GAMErrorCollection = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV41Error = new GeneXus.Programs.genexussecurity.SdtGAMError(context);
         AV12GAMErrorColletion = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV8GAMApplicationMenu = new GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu(context);
         AV9GAMApplicationMenuOption = new GeneXus.Programs.genexussecurity.SdtGAMApplicationMenuOption(context);
         AV25GAMRole = new GeneXus.Programs.genexussecurity.SdtGAMRole(context);
         AV26GAMSecurityPolicy = new GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGrid_Linesclass = "";
         ROClassString = "";
         GridColumn = new GXWebColumn();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.gamtranslations__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.gamtranslations__default(),
            new Object[][] {
            }
         );
         AV49Pgmname = "GAMTranslations";
         /* GeneXus formulas. */
         AV49Pgmname = "GAMTranslations";
         edtavLngid_Enabled = 0;
         edtavLanguage_Enabled = 0;
         edtavName_Enabled = 0;
         edtavText_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      private short GRID_nEOF ;
      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short subGrid_Backcolorstyle ;
      private short nGXWrapped ;
      private short subGrid_Backstyle ;
      private short subGrid_Titlebackstyle ;
      private short subGrid_Allowselection ;
      private short subGrid_Allowhovering ;
      private short subGrid_Allowcollapsing ;
      private short subGrid_Collapsed ;
      private int subGrid_Rows ;
      private int Gridpaginationbar_Rowsperpageselectedvalue ;
      private int nRC_GXsfl_12 ;
      private int subGrid_Recordcount ;
      private int nGXsfl_12_idx=1 ;
      private int Gridpaginationbar_Pagestoshow ;
      private int subGrid_Islastpage ;
      private int edtavLngid_Enabled ;
      private int edtavLanguage_Enabled ;
      private int edtavName_Enabled ;
      private int edtavText_Enabled ;
      private int edtavDelete_Enabled ;
      private int GRID_nGridOutOfScope ;
      private int AV37PageToGo ;
      private int AV50GXV1 ;
      private int AV51GXV2 ;
      private int AV52GXV3 ;
      private int edtavDelete_Visible ;
      private int nGXsfl_12_fel_idx=1 ;
      private int idxLst ;
      private int subGrid_Backcolor ;
      private int subGrid_Allbackcolor ;
      private int subGrid_Titlebackcolor ;
      private int subGrid_Selectedindex ;
      private int subGrid_Selectioncolor ;
      private int subGrid_Hoveringcolor ;
      private long AV18PrimaryID ;
      private long AV20SecondaryID ;
      private long AV21TertiaryID ;
      private long wcpOAV18PrimaryID ;
      private long wcpOAV20SecondaryID ;
      private long wcpOAV21TertiaryID ;
      private long GRID_nFirstRecordOnPage ;
      private long AV38GridCurrentPage ;
      private long AV39GridPageCount ;
      private long GRID_nCurrentRecord ;
      private string Gx_mode ;
      private string wcpOGx_mode ;
      private string Gridpaginationbar_Selectedpage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_12_idx="0001" ;
      private string AV49Pgmname ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string Gridpaginationbar_Class ;
      private string Gridpaginationbar_Pagingbuttonsposition ;
      private string Gridpaginationbar_Pagingcaptionposition ;
      private string Gridpaginationbar_Emptygridclass ;
      private string Gridpaginationbar_Rowsperpageoptions ;
      private string Gridpaginationbar_Previous ;
      private string Gridpaginationbar_Next ;
      private string Gridpaginationbar_Caption ;
      private string Gridpaginationbar_Emptygridcaption ;
      private string Gridpaginationbar_Rowsperpagecaption ;
      private string Grid_empowerer_Gridinternalname ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divLayoutmaintable_Internalname ;
      private string divTablemain_Internalname ;
      private string divGridtablewithpaginationbar_Internalname ;
      private string sStyleString ;
      private string subGrid_Internalname ;
      private string Gridpaginationbar_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttBtnenter_Internalname ;
      private string bttBtnenter_Jsonclick ;
      private string bttBtncancel_Internalname ;
      private string bttBtncancel_Jsonclick ;
      private string divHtml_bottomauxiliarcontrols_Internalname ;
      private string Grid_empowerer_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV16LngID ;
      private string edtavLngid_Internalname ;
      private string edtavLanguage_Internalname ;
      private string AV17Name ;
      private string edtavName_Internalname ;
      private string AV22Text ;
      private string edtavText_Internalname ;
      private string AV6Delete ;
      private string edtavDelete_Internalname ;
      private string GXDecQS ;
      private string GXt_char1 ;
      private string sGXsfl_12_fel_idx="0001" ;
      private string subGrid_Class ;
      private string subGrid_Linesclass ;
      private string ROClassString ;
      private string edtavLngid_Jsonclick ;
      private string edtavLanguage_Jsonclick ;
      private string edtavName_Jsonclick ;
      private string edtavText_Jsonclick ;
      private string edtavDelete_Jsonclick ;
      private string subGrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool Gridpaginationbar_Showfirst ;
      private bool Gridpaginationbar_Showprevious ;
      private bool Gridpaginationbar_Shownext ;
      private bool Gridpaginationbar_Showlast ;
      private bool Gridpaginationbar_Rowsperpageselector ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_12_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_refresh_fired ;
      private string AV24Type ;
      private string AV23Title ;
      private string wcpOAV24Type ;
      private string wcpOAV23Title ;
      private string AV40GridAppliedFilters ;
      private string AV15Language ;
      private IGxSession AV36Session ;
      private GXWebGrid GridContainer ;
      private GXWebRow GridRow ;
      private GXWebColumn GridColumn ;
      private GXUserControl ucGridpaginationbar ;
      private GXUserControl ucGrid_empowerer ;
      private GXWebForm Form ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV29WWPContext ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplication AV7GAMApplication ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage> AV14GAMLanguages ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationLanguage AV13GAMLanguage ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMDescription> AV11GAMDescriptions ;
      private GeneXus.Programs.genexussecurity.SdtGAMDescription AV10GAMDescription ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV34GridState ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV5GAMErrorCollection ;
      private GeneXus.Programs.genexussecurity.SdtGAMError AV41Error ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV12GAMErrorColletion ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationMenu AV8GAMApplicationMenu ;
      private GeneXus.Programs.genexussecurity.SdtGAMApplicationMenuOption AV9GAMApplicationMenuOption ;
      private GeneXus.Programs.genexussecurity.SdtGAMRole AV25GAMRole ;
      private GeneXus.Programs.genexussecurity.SdtGAMSecurityPolicy AV26GAMSecurityPolicy ;
      private IDataStoreProvider pr_default ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class gamtranslations__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class gamtranslations__default : DataStoreHelperBase, IDataStoreHelper
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

}

}
