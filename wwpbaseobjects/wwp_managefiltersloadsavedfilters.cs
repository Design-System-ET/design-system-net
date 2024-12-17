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
   public class wwp_managefiltersloadsavedfilters : GXProcedure
   {
      public wwp_managefiltersloadsavedfilters( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_managefiltersloadsavedfilters( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_Key ,
                           string aP1_CleanJSFormat ,
                           string aP2_TableInternalName ,
                           bool aP3_HasAdvancedFilters ,
                           out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> aP4_ManageFiltersData )
      {
         this.AV8Key = aP0_Key;
         this.AV14CleanJSFormat = aP1_CleanJSFormat;
         this.AV15TableInternalName = aP2_TableInternalName;
         this.AV9HasAdvancedFilters = aP3_HasAdvancedFilters;
         this.AV10ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "") ;
         initialize();
         ExecuteImpl();
         aP4_ManageFiltersData=this.AV10ManageFiltersData;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> executeUdp( string aP0_Key ,
                                                                                                                   string aP1_CleanJSFormat ,
                                                                                                                   string aP2_TableInternalName ,
                                                                                                                   bool aP3_HasAdvancedFilters )
      {
         execute(aP0_Key, aP1_CleanJSFormat, aP2_TableInternalName, aP3_HasAdvancedFilters, out aP4_ManageFiltersData);
         return AV10ManageFiltersData ;
      }

      public void executeSubmit( string aP0_Key ,
                                 string aP1_CleanJSFormat ,
                                 string aP2_TableInternalName ,
                                 bool aP3_HasAdvancedFilters ,
                                 out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> aP4_ManageFiltersData )
      {
         this.AV8Key = aP0_Key;
         this.AV14CleanJSFormat = aP1_CleanJSFormat;
         this.AV15TableInternalName = aP2_TableInternalName;
         this.AV9HasAdvancedFilters = aP3_HasAdvancedFilters;
         this.AV10ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "") ;
         SubmitImpl();
         aP4_ManageFiltersData=this.AV10ManageFiltersData;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV10ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV11ManageFiltersDataItem.gxTpr_Title = context.GetMessage( "WWP_CleanFiltersCaption", "");
         AV11ManageFiltersDataItem.gxTpr_Eventkey = "<#Clean#>";
         AV11ManageFiltersDataItem.gxTpr_Isdivider = false;
         AV11ManageFiltersDataItem.gxTpr_Fonticon = "fa fa-times-circle";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14CleanJSFormat)) )
         {
            AV11ManageFiltersDataItem.gxTpr_Jsonclickevent = StringUtil.Format( AV14CleanJSFormat, AV15TableInternalName, "", "", "", "", "", "", "", "");
         }
         AV10ManageFiltersData.Add(AV11ManageFiltersDataItem, 0);
         AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV11ManageFiltersDataItem.gxTpr_Title = context.GetMessage( "WWP_SaveFilterAsOption", "");
         AV11ManageFiltersDataItem.gxTpr_Eventkey = "<#Save#>";
         AV11ManageFiltersDataItem.gxTpr_Isdivider = false;
         AV11ManageFiltersDataItem.gxTpr_Fonticon = "fa fa-save";
         AV10ManageFiltersData.Add(AV11ManageFiltersDataItem, 0);
         if ( AV9HasAdvancedFilters )
         {
            AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
            AV11ManageFiltersDataItem.gxTpr_Title = context.GetMessage( "WWP_ShowAdvancedFilters", "")+"|"+context.GetMessage( "WWP_HideAdvancedFilters", "");
            AV11ManageFiltersDataItem.gxTpr_Eventkey = "<#ADV#>";
            AV11ManageFiltersDataItem.gxTpr_Isdivider = false;
            AV11ManageFiltersDataItem.gxTpr_Fonticon = "fas fa-filter";
            AV10ManageFiltersData.Add(AV11ManageFiltersDataItem, 0);
         }
         AV13ManageFiltersItems.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadmanagefiltersstate(context).executeUdp(  AV8Key), null, "Items", "");
         if ( AV13ManageFiltersItems.Count > 0 )
         {
            AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
            AV11ManageFiltersDataItem.gxTpr_Isdivider = true;
            AV10ManageFiltersData.Add(AV11ManageFiltersDataItem, 0);
            AV16GXV1 = 1;
            while ( AV16GXV1 <= AV13ManageFiltersItems.Count )
            {
               AV12ManageFiltersItem = ((DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item)AV13ManageFiltersItems.Item(AV16GXV1));
               AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
               AV11ManageFiltersDataItem.gxTpr_Title = AV12ManageFiltersItem.gxTpr_Title;
               AV11ManageFiltersDataItem.gxTpr_Eventkey = AV12ManageFiltersItem.gxTpr_Title;
               AV11ManageFiltersDataItem.gxTpr_Isdivider = false;
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV14CleanJSFormat)) )
               {
                  AV11ManageFiltersDataItem.gxTpr_Jsonclickevent = StringUtil.Format( AV14CleanJSFormat, AV15TableInternalName, "", "", "", "", "", "", "", "");
               }
               AV10ManageFiltersData.Add(AV11ManageFiltersDataItem, 0);
               if ( AV10ManageFiltersData.Count == 13 )
               {
                  if (true) break;
               }
               AV16GXV1 = (int)(AV16GXV1+1);
            }
            AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
            AV11ManageFiltersDataItem.gxTpr_Isdivider = true;
            AV10ManageFiltersData.Add(AV11ManageFiltersDataItem, 0);
            AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
            AV11ManageFiltersDataItem.gxTpr_Title = context.GetMessage( "WWP_ManageFiltersOption", "");
            AV11ManageFiltersDataItem.gxTpr_Eventkey = "<#Manage#>";
            AV11ManageFiltersDataItem.gxTpr_Isdivider = false;
            AV11ManageFiltersDataItem.gxTpr_Fonticon = "fa fa-cog";
            AV11ManageFiltersDataItem.gxTpr_Jsonclickevent = "";
            AV10ManageFiltersData.Add(AV11ManageFiltersDataItem, 0);
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
         AV10ManageFiltersData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item>( context, "Item", "");
         AV11ManageFiltersDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item(context);
         AV13ManageFiltersItems = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item>( context, "Item", "");
         AV12ManageFiltersItem = new DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item(context);
         /* GeneXus formulas. */
      }

      private int AV16GXV1 ;
      private bool AV9HasAdvancedFilters ;
      private string AV8Key ;
      private string AV14CleanJSFormat ;
      private string AV15TableInternalName ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> AV10ManageFiltersData ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item AV11ManageFiltersDataItem ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item> AV13ManageFiltersItems ;
      private DesignSystem.Programs.wwpbaseobjects.SdtGridStateCollection_Item AV12ManageFiltersItem ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsData_Item> aP4_ManageFiltersData ;
   }

}
