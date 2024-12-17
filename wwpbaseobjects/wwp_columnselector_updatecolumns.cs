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
   public class wwp_columnselector_updatecolumns : GXProcedure
   {
      public wwp_columnselector_updatecolumns( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_columnselector_updatecolumns( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP0_OldColumnsSelector ,
                           ref DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP1_ColumnsSelector )
      {
         this.AV15OldColumnsSelector = aP0_OldColumnsSelector;
         this.AV11ColumnsSelector = aP1_ColumnsSelector;
         initialize();
         ExecuteImpl();
         aP0_OldColumnsSelector=this.AV15OldColumnsSelector;
         aP1_ColumnsSelector=this.AV11ColumnsSelector;
      }

      public DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector executeUdp( ref DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP0_OldColumnsSelector )
      {
         execute(ref aP0_OldColumnsSelector, ref aP1_ColumnsSelector);
         return AV11ColumnsSelector ;
      }

      public void executeSubmit( ref DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP0_OldColumnsSelector ,
                                 ref DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP1_ColumnsSelector )
      {
         this.AV15OldColumnsSelector = aP0_OldColumnsSelector;
         this.AV11ColumnsSelector = aP1_ColumnsSelector;
         SubmitImpl();
         aP0_OldColumnsSelector=this.AV15OldColumnsSelector;
         aP1_ColumnsSelector=this.AV11ColumnsSelector;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV17GXV1 = 1;
         while ( AV17GXV1 <= AV11ColumnsSelector.gxTpr_Columns.Count )
         {
            AV8Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV11ColumnsSelector.gxTpr_Columns.Item(AV17GXV1));
            /* Execute user subroutine: 'ISCOLUMNVISIBLE' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            if ( AV13Found )
            {
               AV8Column.gxTpr_Isvisible = AV14IsColumnVisible;
               AV8Column.gxTpr_Fixed = AV9Fixed;
               AV8Column.gxTpr_Order = AV16ColumnOrder;
            }
            AV17GXV1 = (int)(AV17GXV1+1);
         }
         AV12ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV12ColumnsSelectorAux.FromJSonString(AV11ColumnsSelector.ToJSonString(false, true), null);
         AV12ColumnsSelectorAux.gxTpr_Columns.Sort("Order");
         AV16ColumnOrder = 0;
         AV18GXV2 = 1;
         while ( AV18GXV2 <= AV12ColumnsSelectorAux.gxTpr_Columns.Count )
         {
            AV10ColumnAux = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV12ColumnsSelectorAux.gxTpr_Columns.Item(AV18GXV2));
            AV19GXV3 = 1;
            while ( AV19GXV3 <= AV11ColumnsSelector.gxTpr_Columns.Count )
            {
               AV8Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV11ColumnsSelector.gxTpr_Columns.Item(AV19GXV3));
               if ( StringUtil.StrCmp(AV8Column.gxTpr_Columnname, AV10ColumnAux.gxTpr_Columnname) == 0 )
               {
                  AV8Column.gxTpr_Order = AV16ColumnOrder;
                  if (true) break;
               }
               AV19GXV3 = (int)(AV19GXV3+1);
            }
            AV16ColumnOrder = (short)(AV16ColumnOrder+1);
            AV18GXV2 = (int)(AV18GXV2+1);
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'ISCOLUMNVISIBLE' Routine */
         returnInSub = false;
         AV13Found = false;
         AV20GXV4 = 1;
         while ( AV20GXV4 <= AV15OldColumnsSelector.gxTpr_Columns.Count )
         {
            AV10ColumnAux = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV15OldColumnsSelector.gxTpr_Columns.Item(AV20GXV4));
            if ( StringUtil.StrCmp(AV8Column.gxTpr_Columnname, AV10ColumnAux.gxTpr_Columnname) == 0 )
            {
               AV14IsColumnVisible = AV10ColumnAux.gxTpr_Isvisible;
               AV9Fixed = AV10ColumnAux.gxTpr_Fixed;
               AV16ColumnOrder = AV10ColumnAux.gxTpr_Order;
               AV13Found = true;
               if (true) break;
            }
            AV20GXV4 = (int)(AV20GXV4+1);
         }
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
         AV8Column = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         AV9Fixed = "";
         AV12ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV10ColumnAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         /* GeneXus formulas. */
      }

      private short AV16ColumnOrder ;
      private int AV17GXV1 ;
      private int AV18GXV2 ;
      private int AV19GXV3 ;
      private int AV20GXV4 ;
      private bool returnInSub ;
      private bool AV13Found ;
      private bool AV14IsColumnVisible ;
      private string AV9Fixed ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV15OldColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP0_OldColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV11ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP1_ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column AV8Column ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV12ColumnsSelectorAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column AV10ColumnAux ;
   }

}
