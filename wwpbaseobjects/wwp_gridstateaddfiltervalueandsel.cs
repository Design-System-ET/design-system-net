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
   public class wwp_gridstateaddfiltervalueandsel : GXProcedure
   {
      public wwp_gridstateaddfiltervalueandsel( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_gridstateaddfiltervalueandsel( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState aP0_GridState ,
                           string aP1_FilterName ,
                           string aP2_FilterDsc ,
                           bool aP3_AddFitler ,
                           short aP4_FilterOperator ,
                           string aP5_FilterValue ,
                           string aP6_FilterValueDsc ,
                           bool aP7_IsRange ,
                           string aP8_FilterValueTo ,
                           string aP9_FilterValueToDsc ,
                           bool aP10_AddFitlerSel ,
                           string aP11_FilterValueSel ,
                           string aP12_FilterValueSelDsc )
      {
         this.AV19GridState = aP0_GridState;
         this.AV11FilterName = aP1_FilterName;
         this.AV10FilterDsc = aP2_FilterDsc;
         this.AV8AddFitler = aP3_AddFitler;
         this.AV12FilterOperator = aP4_FilterOperator;
         this.AV13FilterValue = aP5_FilterValue;
         this.AV14FilterValueDsc = aP6_FilterValueDsc;
         this.AV21IsRange = aP7_IsRange;
         this.AV17FilterValueTo = aP8_FilterValueTo;
         this.AV18FilterValueToDsc = aP9_FilterValueToDsc;
         this.AV9AddFitlerSel = aP10_AddFitlerSel;
         this.AV15FilterValueSel = aP11_FilterValueSel;
         this.AV16FilterValueSelDsc = aP12_FilterValueSelDsc;
         initialize();
         ExecuteImpl();
         aP0_GridState=this.AV19GridState;
      }

      public void executeSubmit( ref DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState aP0_GridState ,
                                 string aP1_FilterName ,
                                 string aP2_FilterDsc ,
                                 bool aP3_AddFitler ,
                                 short aP4_FilterOperator ,
                                 string aP5_FilterValue ,
                                 string aP6_FilterValueDsc ,
                                 bool aP7_IsRange ,
                                 string aP8_FilterValueTo ,
                                 string aP9_FilterValueToDsc ,
                                 bool aP10_AddFitlerSel ,
                                 string aP11_FilterValueSel ,
                                 string aP12_FilterValueSelDsc )
      {
         this.AV19GridState = aP0_GridState;
         this.AV11FilterName = aP1_FilterName;
         this.AV10FilterDsc = aP2_FilterDsc;
         this.AV8AddFitler = aP3_AddFitler;
         this.AV12FilterOperator = aP4_FilterOperator;
         this.AV13FilterValue = aP5_FilterValue;
         this.AV14FilterValueDsc = aP6_FilterValueDsc;
         this.AV21IsRange = aP7_IsRange;
         this.AV17FilterValueTo = aP8_FilterValueTo;
         this.AV18FilterValueToDsc = aP9_FilterValueToDsc;
         this.AV9AddFitlerSel = aP10_AddFitlerSel;
         this.AV15FilterValueSel = aP11_FilterValueSel;
         this.AV16FilterValueSelDsc = aP12_FilterValueSelDsc;
         SubmitImpl();
         aP0_GridState=this.AV19GridState;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( AV8AddFitler )
         {
            AV20GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
            AV20GridStateFilterValue.gxTpr_Name = AV11FilterName;
            AV20GridStateFilterValue.gxTpr_Dsc = (AV9AddFitlerSel ? "" : AV10FilterDsc);
            AV20GridStateFilterValue.gxTpr_Operator = AV12FilterOperator;
            AV20GridStateFilterValue.gxTpr_Value = AV13FilterValue;
            AV20GridStateFilterValue.gxTpr_Valuedsc = AV14FilterValueDsc;
            AV20GridStateFilterValue.gxTpr_Valueto = AV17FilterValueTo;
            AV20GridStateFilterValue.gxTpr_Valuetodsc = AV18FilterValueToDsc;
            if ( AV21IsRange )
            {
               if ( String.IsNullOrEmpty(StringUtil.RTrim( AV18FilterValueToDsc)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV14FilterValueDsc)) )
               {
                  AV20GridStateFilterValue.gxTpr_Valuedsc = StringUtil.Format( context.GetMessage( "from %1", ""), AV14FilterValueDsc, "", "", "", "", "", "", "", "");
               }
               else
               {
                  if ( String.IsNullOrEmpty(StringUtil.RTrim( AV14FilterValueDsc)) && ! String.IsNullOrEmpty(StringUtil.RTrim( AV18FilterValueToDsc)) )
                  {
                     AV20GridStateFilterValue.gxTpr_Valuetodsc = StringUtil.Format( context.GetMessage( "up to %1", ""), AV18FilterValueToDsc, "", "", "", "", "", "", "", "");
                  }
               }
            }
            AV19GridState.gxTpr_Filtervalues.Add(AV20GridStateFilterValue, 0);
         }
         if ( AV9AddFitlerSel )
         {
            AV20GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
            AV20GridStateFilterValue.gxTpr_Name = AV11FilterName+"_SEL";
            AV20GridStateFilterValue.gxTpr_Dsc = AV10FilterDsc;
            AV20GridStateFilterValue.gxTpr_Value = AV15FilterValueSel;
            AV20GridStateFilterValue.gxTpr_Valuedsc = AV16FilterValueSelDsc;
            AV19GridState.gxTpr_Filtervalues.Add(AV20GridStateFilterValue, 0);
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
         AV20GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         /* GeneXus formulas. */
      }

      private short AV12FilterOperator ;
      private bool AV8AddFitler ;
      private bool AV21IsRange ;
      private bool AV9AddFitlerSel ;
      private string AV11FilterName ;
      private string AV10FilterDsc ;
      private string AV13FilterValue ;
      private string AV14FilterValueDsc ;
      private string AV17FilterValueTo ;
      private string AV18FilterValueToDsc ;
      private string AV15FilterValueSel ;
      private string AV16FilterValueSelDsc ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV19GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState aP0_GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV20GridStateFilterValue ;
   }

}
