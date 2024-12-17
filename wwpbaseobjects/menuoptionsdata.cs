using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs.wwpbaseobjects {
   public class menuoptionsdata : GXProcedure
   {
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

      public menuoptionsdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public menuoptionsdata( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item>( context, "Item", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item>( context, "Item", "DesignSystem") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV5id = 0;
         Gxm1dvelop_menu = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm2rootcol.Add(Gxm1dvelop_menu, 0);
         AV5id = (short)(AV5id+1);
         Gxm1dvelop_menu.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm1dvelop_menu.gxTpr_Tooltip = "";
         Gxm1dvelop_menu.gxTpr_Link = formatLink("home.aspx") ;
         Gxm1dvelop_menu.gxTpr_Linktarget = "";
         Gxm1dvelop_menu.gxTpr_Iconclass = "";
         Gxm1dvelop_menu.gxTpr_Caption = "Home";
         Gxm1dvelop_menu.gxTpr_Authorizationkey = "public";
         Gxm1dvelop_menu = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm2rootcol.Add(Gxm1dvelop_menu, 0);
         AV5id = (short)(AV5id+1);
         Gxm1dvelop_menu.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm1dvelop_menu.gxTpr_Tooltip = "";
         Gxm1dvelop_menu.gxTpr_Link = "";
         Gxm1dvelop_menu.gxTpr_Linktarget = "";
         Gxm1dvelop_menu.gxTpr_Iconclass = "";
         Gxm1dvelop_menu.gxTpr_Caption = "Servicios";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("sitioweb.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Sitios Web";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("landingpages.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "LandingPages";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("appmobile.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Aplicaciones Mobiles";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("apiservice.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "API´s";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = "";
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Aplicaciones Desktop";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm1dvelop_menu = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm2rootcol.Add(Gxm1dvelop_menu, 0);
         AV5id = (short)(AV5id+1);
         Gxm1dvelop_menu.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm1dvelop_menu.gxTpr_Tooltip = "";
         Gxm1dvelop_menu.gxTpr_Link = "";
         Gxm1dvelop_menu.gxTpr_Linktarget = "";
         Gxm1dvelop_menu.gxTpr_Iconclass = "";
         Gxm1dvelop_menu.gxTpr_Caption = "Ecosistema";
         Gxm1dvelop_menu.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("sobrenosotros.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Sobre nosotros";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("blog.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Blog";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("productos.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Soluciones";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = "";
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Documentación";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = "";
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Soporte";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "wwpbaseobjects.formulariocontacto.aspx"+GXUtil.UrlEncode(StringUtil.LTrimStr(0,1,0));
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("wwpbaseobjects.formulariocontacto.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = "Contacto";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "public";
         Gxm1dvelop_menu = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm2rootcol.Add(Gxm1dvelop_menu, 0);
         AV5id = (short)(AV5id+1);
         Gxm1dvelop_menu.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm1dvelop_menu.gxTpr_Tooltip = "";
         Gxm1dvelop_menu.gxTpr_Link = "";
         Gxm1dvelop_menu.gxTpr_Linktarget = "";
         Gxm1dvelop_menu.gxTpr_Iconclass = "menu-icon fa fa-desktop";
         Gxm1dvelop_menu.gxTpr_Caption = "Administración";
         Gxm1dvelop_menu.gxTpr_Submenuimage = formatLink(context.GetMessage( "DeveloperMenuImage", "")) ;
         Gxv4skipcount = 0;
         AV16GXV2 = 1;
         GXt_objcol_SdtProgramNames_ProgramName1 = AV15GXV1;
         new DesignSystem.Programs.wwpbaseobjects.menulista(context ).execute( out  GXt_objcol_SdtProgramNames_ProgramName1) ;
         AV15GXV1 = GXt_objcol_SdtProgramNames_ProgramName1;
         while ( AV16GXV2 <= AV15GXV1.Count )
         {
            AV6ProgramName = ((DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName)AV15GXV1.Item(AV16GXV2));
            Gxv4skipcount = (int)(Gxv4skipcount+1);
            Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
            Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
            AV5id = (short)(AV5id+1);
            Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
            Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
            Gxm3dvelop_menu_subitems.gxTpr_Link = AV6ProgramName.gxTpr_Link;
            Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
            Gxm3dvelop_menu_subitems.gxTpr_Iconclass = "";
            Gxm3dvelop_menu_subitems.gxTpr_Caption = AV6ProgramName.gxTpr_Description;
            if ( ( 20 != 0 ) && ( Gxv4skipcount >= 20 ) )
            {
               /* Exit For each command. Update data (if necessary), close cursors & exit. */
               if (true) break;
            }
            AV16GXV2 = (int)(AV16GXV2+1);
         }
         Gxm1dvelop_menu = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm2rootcol.Add(Gxm1dvelop_menu, 0);
         AV5id = (short)(AV5id+1);
         Gxm1dvelop_menu.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm1dvelop_menu.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_SecurityOfTheApplication", "");
         Gxm1dvelop_menu.gxTpr_Link = "";
         Gxm1dvelop_menu.gxTpr_Linktarget = "";
         Gxm1dvelop_menu.gxTpr_Iconclass = "menu-icon fa fa-key";
         Gxm1dvelop_menu.gxTpr_Caption = context.GetMessage( "WWP_GAM_GAMSecurity", "");
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_Users", "");
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("gamwwusers.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Iconclass = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_Users", "");
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_Roles", "");
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("gamwwroles.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Iconclass = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_Roles", "");
         Gxm3dvelop_menu_subitems.gxTpr_Iconclass = "";
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = "";
         Gxm3dvelop_menu_subitems.gxTpr_Link = "";
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Iconclass = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_Repository", "");
         AV9IsRepoAdministrator = AV7Repository.isgamadministrator(out  AV8Errors);
         if ( AV9IsRepoAdministrator )
         {
            Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
            Gxm3dvelop_menu_subitems.gxTpr_Subitems.Add(Gxm5dvelop_menu_subitems_subitems, 0);
            AV5id = (short)(AV5id+1);
            Gxm5dvelop_menu_subitems_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
            Gxm5dvelop_menu_subitems_subitems.gxTpr_Tooltip = "";
            Gxm5dvelop_menu_subitems_subitems.gxTpr_Link = formatLink("gamwwrepositories.aspx") ;
            Gxm5dvelop_menu_subitems_subitems.gxTpr_Linktarget = "";
            Gxm5dvelop_menu_subitems_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_Repositories", "");
            Gxm5dvelop_menu_subitems_subitems.gxTpr_Authorizationkey = "";
         }
         Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm3dvelop_menu_subitems.gxTpr_Subitems.Add(Gxm5dvelop_menu_subitems_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Tooltip = context.GetMessage( "Repository Configuration", "");
         if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
         {
            gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
         }
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         GXEncryptionTmp = "gamrepositoryconfiguration.aspx"+GXUtil.UrlEncode(StringUtil.LTrimStr(0,1,0));
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Link = formatLink("gamrepositoryconfiguration.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Linktarget = "";
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_Configuration", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Authorizationkey = "";
         Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm3dvelop_menu_subitems.gxTpr_Subitems.Add(Gxm5dvelop_menu_subitems_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Tooltip = context.GetMessage( "Repository Connections", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Link = formatLink("gamwwconnections.aspx") ;
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Linktarget = "";
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_Connections", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Authorizationkey = "";
         Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm3dvelop_menu_subitems.gxTpr_Subitems.Add(Gxm5dvelop_menu_subitems_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Tooltip = context.GetMessage( "Change Working Repository", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Link = formatLink("gamchangerepository.aspx") ;
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Linktarget = "";
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_WorkingRepository", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Authorizationkey = "";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_OtherConfigurations", "");
         Gxm3dvelop_menu_subitems.gxTpr_Link = "";
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Iconclass = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_OtherConfigurations", "");
         Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm3dvelop_menu_subitems.gxTpr_Subitems.Add(Gxm5dvelop_menu_subitems_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_SecurityPolicies", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Link = formatLink("gamwwsecuritypolicy.aspx") ;
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Linktarget = "";
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_SecurityPolicies", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Authorizationkey = "";
         Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm3dvelop_menu_subitems.gxTpr_Subitems.Add(Gxm5dvelop_menu_subitems_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_AuthenticationTypes", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Link = formatLink("gamwwauthtypes.aspx") ;
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Linktarget = "";
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_AuthenticationTypes", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Authorizationkey = "";
         Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm3dvelop_menu_subitems.gxTpr_Subitems.Add(Gxm5dvelop_menu_subitems_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_EventSubscriptions", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Link = formatLink("gamwweventsubscriptions.aspx") ;
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Linktarget = "";
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_EventSubscriptions", "");
         Gxm5dvelop_menu_subitems_subitems.gxTpr_Authorizationkey = "";
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm1dvelop_menu.gxTpr_Subitems.Add(Gxm3dvelop_menu_subitems, 0);
         AV5id = (short)(AV5id+1);
         Gxm3dvelop_menu_subitems.gxTpr_Id = StringUtil.Str( (decimal)(AV5id), 4, 0);
         Gxm3dvelop_menu_subitems.gxTpr_Tooltip = context.GetMessage( "WWP_GAM_Applications", "");
         Gxm3dvelop_menu_subitems.gxTpr_Link = formatLink("gamwwapplications.aspx") ;
         Gxm3dvelop_menu_subitems.gxTpr_Linktarget = "";
         Gxm3dvelop_menu_subitems.gxTpr_Iconclass = "";
         Gxm3dvelop_menu_subitems.gxTpr_Caption = context.GetMessage( "WWP_GAM_Applications", "");
         Gxm3dvelop_menu_subitems.gxTpr_Authorizationkey = "";
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         Gxm1dvelop_menu = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         Gxm3dvelop_menu_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         GXKey = "";
         GXEncryptionTmp = "";
         AV15GXV1 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName>( context, "ProgramName", "DesignSystem");
         GXt_objcol_SdtProgramNames_ProgramName1 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName>( context, "ProgramName", "DesignSystem");
         AV6ProgramName = new DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName(context);
         AV8Errors = new GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError>( context, "GeneXus.Programs.genexussecurity.SdtGAMError", "DesignSystem.Programs");
         AV7Repository = new GeneXus.Programs.genexussecurity.SdtGAMRepository(context);
         Gxm5dvelop_menu_subitems_subitems = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         /* GeneXus formulas. */
      }

      private short AV5id ;
      private short gxcookieaux ;
      private int Gxv4skipcount ;
      private int AV16GXV2 ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private bool AV9IsRepoAdministrator ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> Gxm2rootcol ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item Gxm1dvelop_menu ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item Gxm3dvelop_menu_subitems ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName> AV15GXV1 ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName> GXt_objcol_SdtProgramNames_ProgramName1 ;
      private DesignSystem.Programs.wwpbaseobjects.SdtProgramNames_ProgramName AV6ProgramName ;
      private GXExternalCollection<GeneXus.Programs.genexussecurity.SdtGAMError> AV8Errors ;
      private GeneXus.Programs.genexussecurity.SdtGAMRepository AV7Repository ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item Gxm5dvelop_menu_subitems_subitems ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_Gxm2rootcol ;
   }

}
