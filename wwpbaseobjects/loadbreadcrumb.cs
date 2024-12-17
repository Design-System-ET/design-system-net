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
   public class loadbreadcrumb : GXProcedure
   {
      public loadbreadcrumb( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public loadbreadcrumb( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_DVelop_Menu ,
                           string aP1_MenuOptionToFind ,
                           ref string aP2_Breadcrumb ,
                           ref bool aP3_MenuOptionFounded )
      {
         this.AV9DVelop_Menu = aP0_DVelop_Menu;
         this.AV12MenuOptionToFind = aP1_MenuOptionToFind;
         this.AV8Breadcrumb = aP2_Breadcrumb;
         this.AV11MenuOptionFounded = aP3_MenuOptionFounded;
         initialize();
         ExecuteImpl();
         aP2_Breadcrumb=this.AV8Breadcrumb;
         aP3_MenuOptionFounded=this.AV11MenuOptionFounded;
      }

      public bool executeUdp( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_DVelop_Menu ,
                              string aP1_MenuOptionToFind ,
                              ref string aP2_Breadcrumb )
      {
         execute(aP0_DVelop_Menu, aP1_MenuOptionToFind, ref aP2_Breadcrumb, ref aP3_MenuOptionFounded);
         return AV11MenuOptionFounded ;
      }

      public void executeSubmit( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_DVelop_Menu ,
                                 string aP1_MenuOptionToFind ,
                                 ref string aP2_Breadcrumb ,
                                 ref bool aP3_MenuOptionFounded )
      {
         this.AV9DVelop_Menu = aP0_DVelop_Menu;
         this.AV12MenuOptionToFind = aP1_MenuOptionToFind;
         this.AV8Breadcrumb = aP2_Breadcrumb;
         this.AV11MenuOptionFounded = aP3_MenuOptionFounded;
         SubmitImpl();
         aP2_Breadcrumb=this.AV8Breadcrumb;
         aP3_MenuOptionFounded=this.AV11MenuOptionFounded;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11MenuOptionFounded = false;
         AV13GXV1 = 1;
         while ( AV13GXV1 <= AV9DVelop_Menu.Count )
         {
            AV10DVelop_Menu_Item = ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV9DVelop_Menu.Item(AV13GXV1));
            if ( StringUtil.StrCmp(StringUtil.Trim( StringUtil.Lower( AV10DVelop_Menu_Item.gxTpr_Link)), StringUtil.Trim( StringUtil.Lower( AV12MenuOptionToFind))) == 0 )
            {
               AV8Breadcrumb = StringUtil.Format( "<span class=\"BreadCrumb\">%1</span>", AV10DVelop_Menu_Item.gxTpr_Caption, "", "", "", "", "", "", "", "");
               AV11MenuOptionFounded = true;
               if (true) break;
            }
            else
            {
               new DesignSystem.Programs.wwpbaseobjects.loadbreadcrumb(context ).execute(  AV10DVelop_Menu_Item.gxTpr_Subitems,  AV12MenuOptionToFind, ref  AV8Breadcrumb, ref  AV11MenuOptionFounded) ;
               if ( AV11MenuOptionFounded )
               {
                  AV8Breadcrumb = StringUtil.Format( "<span class=\"%3\">%1</span><i class=\"fa fa-angle-right %4\"></i>%2", AV10DVelop_Menu_Item.gxTpr_Caption, AV8Breadcrumb, "BreadCrumb", "BreadCrumbIcon", "", "", "", "", "");
                  if (true) break;
               }
            }
            AV13GXV1 = (int)(AV13GXV1+1);
         }
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
         AV10DVelop_Menu_Item = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         /* GeneXus formulas. */
      }

      private int AV13GXV1 ;
      private bool AV11MenuOptionFounded ;
      private string AV12MenuOptionToFind ;
      private string AV8Breadcrumb ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> AV9DVelop_Menu ;
      private string aP2_Breadcrumb ;
      private bool aP3_MenuOptionFounded ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV10DVelop_Menu_Item ;
   }

}
