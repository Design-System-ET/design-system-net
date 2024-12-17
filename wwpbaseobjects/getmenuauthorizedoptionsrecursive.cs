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
   public class getmenuauthorizedoptionsrecursive : GXProcedure
   {
      public getmenuauthorizedoptionsrecursive( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public getmenuauthorizedoptionsrecursive( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_ParentItemJson ,
                           out string aP1_ResultJson )
      {
         this.AV13ParentItemJson = aP0_ParentItemJson;
         this.AV16ResultJson = "" ;
         initialize();
         ExecuteImpl();
         aP1_ResultJson=this.AV16ResultJson;
      }

      public string executeUdp( string aP0_ParentItemJson )
      {
         execute(aP0_ParentItemJson, out aP1_ResultJson);
         return AV16ResultJson ;
      }

      public void executeSubmit( string aP0_ParentItemJson ,
                                 out string aP1_ResultJson )
      {
         this.AV13ParentItemJson = aP0_ParentItemJson;
         this.AV16ResultJson = "" ;
         SubmitImpl();
         aP1_ResultJson=this.AV16ResultJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14RemoveIds.Clear();
         AV10DVelop_Menu_Item.FromJSonString(AV13ParentItemJson, null);
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV10DVelop_Menu_Item.gxTpr_Subitems.Count )
         {
            AV9AuxDVelop_Menu_Item = ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV10DVelop_Menu_Item.gxTpr_Subitems.Item(AV17GXV1));
            if ( AV9AuxDVelop_Menu_Item.gxTpr_Subitems.Count > 0 )
            {
               new DesignSystem.Programs.wwpbaseobjects.getmenuauthorizedoptionsrecursive(context ).execute(  AV9AuxDVelop_Menu_Item.ToJSonString(false, true), out  AV15Result2Json) ;
               AV9AuxDVelop_Menu_Item.FromJSonString(AV15Result2Json, null);
               if ( AV9AuxDVelop_Menu_Item.gxTpr_Subitems.Count == 0 )
               {
                  AV14RemoveIds.Add(AV9AuxDVelop_Menu_Item.gxTpr_Id, 0);
               }
            }
            else
            {
               GXt_boolean1 = AV8IsAuthorized;
               new DesignSystem.Programs.wwpbaseobjects.ismenuauthorizedoption(context ).execute(  AV9AuxDVelop_Menu_Item, out  GXt_boolean1) ;
               AV8IsAuthorized = GXt_boolean1;
               if ( ! AV8IsAuthorized )
               {
                  AV14RemoveIds.Add(AV9AuxDVelop_Menu_Item.gxTpr_Id, 0);
               }
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         AV11i = 1;
         while ( AV11i <= AV14RemoveIds.Count )
         {
            AV12j = 0;
            AV18GXV2 = 1;
            while ( AV18GXV2 <= AV10DVelop_Menu_Item.gxTpr_Subitems.Count )
            {
               AV9AuxDVelop_Menu_Item = ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV10DVelop_Menu_Item.gxTpr_Subitems.Item(AV18GXV2));
               AV12j = (short)(AV12j+1);
               if ( StringUtil.StrCmp(StringUtil.Trim( AV9AuxDVelop_Menu_Item.gxTpr_Id), StringUtil.Trim( AV14RemoveIds.GetString(AV11i))) == 0 )
               {
                  AV10DVelop_Menu_Item.gxTpr_Subitems.RemoveItem(AV12j);
                  if (true) break;
               }
               AV18GXV2 = (int)(AV18GXV2+1);
            }
            AV11i = (short)(AV11i+1);
         }
         AV16ResultJson = AV10DVelop_Menu_Item.ToJSonString(false, true);
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
         AV16ResultJson = "";
         AV14RemoveIds = new GxSimpleCollection<string>();
         AV10DVelop_Menu_Item = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         AV9AuxDVelop_Menu_Item = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         AV15Result2Json = "";
         /* GeneXus formulas. */
      }

      private short AV11i ;
      private short AV12j ;
      private int AV17GXV1 ;
      private int AV18GXV2 ;
      private bool AV8IsAuthorized ;
      private bool GXt_boolean1 ;
      private string AV13ParentItemJson ;
      private string AV16ResultJson ;
      private string AV15Result2Json ;
      private GxSimpleCollection<string> AV14RemoveIds ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV10DVelop_Menu_Item ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV9AuxDVelop_Menu_Item ;
      private string aP1_ResultJson ;
   }

}
