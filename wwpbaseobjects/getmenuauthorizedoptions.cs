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
   public class getmenuauthorizedoptions : GXProcedure
   {
      public getmenuauthorizedoptions( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public getmenuauthorizedoptions( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_DVelop_Menu )
      {
         this.AV9DVelop_Menu = aP0_DVelop_Menu;
         initialize();
         ExecuteImpl();
         aP0_DVelop_Menu=this.AV9DVelop_Menu;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> executeUdp( )
      {
         execute(ref aP0_DVelop_Menu);
         return AV9DVelop_Menu ;
      }

      public void executeSubmit( ref GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_DVelop_Menu )
      {
         this.AV9DVelop_Menu = aP0_DVelop_Menu;
         SubmitImpl();
         aP0_DVelop_Menu=this.AV9DVelop_Menu;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV16GXV1 = 1;
         while ( AV16GXV1 <= AV9DVelop_Menu.Count )
         {
            AV10DVelop_Menu_Item = ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV9DVelop_Menu.Item(AV16GXV1));
            if ( AV10DVelop_Menu_Item.gxTpr_Subitems.Count > 0 )
            {
               new DesignSystem.Programs.wwpbaseobjects.getmenuauthorizedoptionsrecursive(context ).execute(  AV10DVelop_Menu_Item.ToJSonString(false, true), out  AV15ResultJson) ;
               AV8Aux_DVelop_Menu_Item.FromJSonString(AV15ResultJson, null);
               if ( AV8Aux_DVelop_Menu_Item.gxTpr_Subitems.Count == 0 )
               {
                  AV14RemoveIds.Add(AV10DVelop_Menu_Item.gxTpr_Id, 0);
               }
               else
               {
                  AV10DVelop_Menu_Item.FromJSonString(AV15ResultJson, null);
               }
            }
            else
            {
               GXt_boolean1 = AV12IsAuthorized;
               new DesignSystem.Programs.wwpbaseobjects.ismenuauthorizedoption(context ).execute(  AV10DVelop_Menu_Item, out  GXt_boolean1) ;
               AV12IsAuthorized = GXt_boolean1;
               if ( ! AV12IsAuthorized )
               {
                  AV14RemoveIds.Add(AV10DVelop_Menu_Item.gxTpr_Id, 0);
               }
            }
            AV16GXV1 = (int)(AV16GXV1+1);
         }
         AV11i = 1;
         while ( AV11i <= AV14RemoveIds.Count )
         {
            AV13j = 1;
            while ( AV13j <= AV9DVelop_Menu.Count )
            {
               if ( StringUtil.StrCmp(StringUtil.Trim( ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV9DVelop_Menu.Item(AV13j)).gxTpr_Id), StringUtil.Trim( AV14RemoveIds.GetString(AV11i))) == 0 )
               {
                  AV9DVelop_Menu.RemoveItem(AV13j);
                  if (true) break;
               }
               AV13j = (short)(AV13j+1);
            }
            AV11i = (short)(AV11i+1);
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
         AV15ResultJson = "";
         AV8Aux_DVelop_Menu_Item = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         AV14RemoveIds = new GxSimpleCollection<string>();
         /* GeneXus formulas. */
      }

      private short AV11i ;
      private short AV13j ;
      private int AV16GXV1 ;
      private bool AV12IsAuthorized ;
      private bool GXt_boolean1 ;
      private string AV15ResultJson ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> AV9DVelop_Menu ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> aP0_DVelop_Menu ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV10DVelop_Menu_Item ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV8Aux_DVelop_Menu_Item ;
      private GxSimpleCollection<string> AV14RemoveIds ;
   }

}
