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
   public class wwp_getappliedfiltersdescription : GXProcedure
   {
      public wwp_getappliedfiltersdescription( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_getappliedfiltersdescription( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_ListObjectName ,
                           out string aP1_AppliedFiltersDescription )
      {
         this.AV14ListObjectName = aP0_ListObjectName;
         this.AV8AppliedFiltersDescription = "" ;
         initialize();
         ExecuteImpl();
         aP1_AppliedFiltersDescription=this.AV8AppliedFiltersDescription;
      }

      public string executeUdp( string aP0_ListObjectName )
      {
         execute(aP0_ListObjectName, out aP1_AppliedFiltersDescription);
         return AV8AppliedFiltersDescription ;
      }

      public void executeSubmit( string aP0_ListObjectName ,
                                 out string aP1_AppliedFiltersDescription )
      {
         this.AV14ListObjectName = aP0_ListObjectName;
         this.AV8AppliedFiltersDescription = "" ;
         SubmitImpl();
         aP1_AppliedFiltersDescription=this.AV8AppliedFiltersDescription;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV12GridState.FromXml(AV15Session.Get(AV14ListObjectName+"GridState"), null, "", "");
         AV9index = 1;
         while ( AV12GridState.gxTpr_Filtervalues.Count >= AV9index )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV12GridState.gxTpr_Filtervalues.Item(AV9index)).gxTpr_Dsc))) )
            {
               AV12GridState.gxTpr_Filtervalues.RemoveItem(AV9index);
            }
            else
            {
               AV9index = (short)(AV9index+1);
            }
         }
         AV16TotalFilters = (short)(AV12GridState.gxTpr_Filtervalues.Count+AV12GridState.gxTpr_Dynamicfilters.Count);
         AV8AppliedFiltersDescription = "";
         if ( AV16TotalFilters > 0 )
         {
            AV17EmptyDateStr = StringUtil.Trim( context.localUtil.Format( AV18AuxDate, "99/99/99"));
            AV19EmptyTimeStr = context.localUtil.Format( AV20AuxDateTime, "99/99/99 99:99");
            AV19EmptyTimeStr = StringUtil.Trim( StringUtil.StringReplace( AV19EmptyTimeStr, AV17EmptyDateStr, ""));
            AV9index = 1;
            AV21GXV1 = 1;
            while ( AV21GXV1 <= AV12GridState.gxTpr_Filtervalues.Count )
            {
               AV13GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV12GridState.gxTpr_Filtervalues.Item(AV21GXV1));
               AV11FilterDescription = AV13GridStateFilterValue.gxTpr_Dsc;
               AV13GridStateFilterValue.gxTpr_Valuedsc = StringUtil.StringReplace( AV13GridStateFilterValue.gxTpr_Valuedsc, AV19EmptyTimeStr, "");
               AV13GridStateFilterValue.gxTpr_Valuetodsc = StringUtil.StringReplace( AV13GridStateFilterValue.gxTpr_Valuetodsc, AV19EmptyTimeStr, "");
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13GridStateFilterValue.gxTpr_Valuedsc)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV13GridStateFilterValue.gxTpr_Valuetodsc)) )
               {
                  AV11FilterDescription += StringUtil.Format( " (%1 - %2)", AV13GridStateFilterValue.gxTpr_Valuedsc, AV13GridStateFilterValue.gxTpr_Valuetodsc, "", "", "", "", "", "", "");
               }
               else if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13GridStateFilterValue.gxTpr_Valuedsc)) )
               {
                  AV11FilterDescription += StringUtil.Format( " (%1)", AV13GridStateFilterValue.gxTpr_Valuedsc, "", "", "", "", "", "", "", "");
               }
               else if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV13GridStateFilterValue.gxTpr_Valuetodsc)) )
               {
                  AV11FilterDescription += StringUtil.Format( " (%1)", AV13GridStateFilterValue.gxTpr_Valuetodsc, "", "", "", "", "", "", "", "");
               }
               /* Execute user subroutine: 'ADD FILTER TO DESCRIPTION' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV21GXV1 = (int)(AV21GXV1+1);
            }
            AV22GXV2 = 1;
            while ( AV22GXV2 <= AV12GridState.gxTpr_Dynamicfilters.Count )
            {
               AV10DynamicFiltersItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_DynamicFilter)AV12GridState.gxTpr_Dynamicfilters.Item(AV22GXV2));
               AV11FilterDescription = AV10DynamicFiltersItem.gxTpr_Dsc;
               AV10DynamicFiltersItem.gxTpr_Valuedsc = StringUtil.StringReplace( AV10DynamicFiltersItem.gxTpr_Valuedsc, AV19EmptyTimeStr, "");
               AV10DynamicFiltersItem.gxTpr_Valuetodsc = StringUtil.StringReplace( AV10DynamicFiltersItem.gxTpr_Valuetodsc, AV19EmptyTimeStr, "");
               if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10DynamicFiltersItem.gxTpr_Valuedsc)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV10DynamicFiltersItem.gxTpr_Valuetodsc)) )
               {
                  AV11FilterDescription += StringUtil.Format( " (%1 - %2)", AV10DynamicFiltersItem.gxTpr_Valuedsc, AV10DynamicFiltersItem.gxTpr_Valuetodsc, "", "", "", "", "", "", "");
               }
               else if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10DynamicFiltersItem.gxTpr_Valuedsc)) )
               {
                  AV11FilterDescription += StringUtil.Format( " (%1)", AV10DynamicFiltersItem.gxTpr_Valuedsc, "", "", "", "", "", "", "", "");
               }
               else if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV10DynamicFiltersItem.gxTpr_Valuetodsc)) )
               {
                  AV11FilterDescription += StringUtil.Format( " (%1)", AV10DynamicFiltersItem.gxTpr_Valuetodsc, "", "", "", "", "", "", "", "");
               }
               /* Execute user subroutine: 'ADD FILTER TO DESCRIPTION' */
               S111 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
               AV22GXV2 = (int)(AV22GXV2+1);
            }
            AV8AppliedFiltersDescription = StringUtil.Format( context.GetMessage( "WWP_FilteringByCaption", ""), AV8AppliedFiltersDescription, "", "", "", "", "", "", "", "");
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'ADD FILTER TO DESCRIPTION' Routine */
         returnInSub = false;
         if ( AV9index >= 6 )
         {
            if ( AV9index == 6 )
            {
               AV8AppliedFiltersDescription += "...";
            }
         }
         else
         {
            if ( AV9index > 1 )
            {
               AV8AppliedFiltersDescription += ((AV9index==AV16TotalFilters) ? " "+context.GetMessage( "WWP_WordsListLastSeparator", "") : context.GetMessage( "WWP_WordsListSeparator", "")) + " ";
            }
            AV8AppliedFiltersDescription += AV11FilterDescription;
         }
         AV9index = (short)(AV9index+1);
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
         AV8AppliedFiltersDescription = "";
         AV12GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV15Session = context.GetSession();
         AV17EmptyDateStr = "";
         AV18AuxDate = DateTime.MinValue;
         AV19EmptyTimeStr = "";
         AV20AuxDateTime = (DateTime)(DateTime.MinValue);
         AV13GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV11FilterDescription = "";
         AV10DynamicFiltersItem = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_DynamicFilter(context);
         /* GeneXus formulas. */
      }

      private short AV9index ;
      private short AV16TotalFilters ;
      private int AV21GXV1 ;
      private int AV22GXV2 ;
      private DateTime AV20AuxDateTime ;
      private DateTime AV18AuxDate ;
      private bool returnInSub ;
      private string AV14ListObjectName ;
      private string AV8AppliedFiltersDescription ;
      private string AV17EmptyDateStr ;
      private string AV19EmptyTimeStr ;
      private string AV11FilterDescription ;
      private IGxSession AV15Session ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV12GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV13GridStateFilterValue ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_DynamicFilter AV10DynamicFiltersItem ;
      private string aP1_AppliedFiltersDescription ;
   }

}
