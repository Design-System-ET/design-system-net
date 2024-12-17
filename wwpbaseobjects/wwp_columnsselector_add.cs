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
   public class wwp_columnsselector_add : GXProcedure
   {
      public wwp_columnsselector_add( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_columnsselector_add( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP0_ColumnsSelector ,
                           string aP1_ColumnName ,
                           string aP2_Category ,
                           string aP3_DisplayName ,
                           bool aP4_IsVisible ,
                           string aP5_Fixed )
      {
         this.AV8ColumnsSelector = aP0_ColumnsSelector;
         this.AV9ColumnName = aP1_ColumnName;
         this.AV10Category = aP2_Category;
         this.AV11DisplayName = aP3_DisplayName;
         this.AV13IsVisible = aP4_IsVisible;
         this.AV12Fixed = aP5_Fixed;
         initialize();
         ExecuteImpl();
         aP0_ColumnsSelector=this.AV8ColumnsSelector;
      }

      public void executeSubmit( ref DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP0_ColumnsSelector ,
                                 string aP1_ColumnName ,
                                 string aP2_Category ,
                                 string aP3_DisplayName ,
                                 bool aP4_IsVisible ,
                                 string aP5_Fixed )
      {
         this.AV8ColumnsSelector = aP0_ColumnsSelector;
         this.AV9ColumnName = aP1_ColumnName;
         this.AV10Category = aP2_Category;
         this.AV11DisplayName = aP3_DisplayName;
         this.AV13IsVisible = aP4_IsVisible;
         this.AV12Fixed = aP5_Fixed;
         SubmitImpl();
         aP0_ColumnsSelector=this.AV8ColumnsSelector;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV14Column = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         AV14Column.gxTpr_Columnname = AV9ColumnName;
         AV14Column.gxTpr_Displayname = AV11DisplayName;
         AV14Column.gxTpr_Isvisible = AV13IsVisible;
         AV14Column.gxTpr_Order = (short)(AV8ColumnsSelector.gxTpr_Columns.Count+1);
         AV14Column.gxTpr_Category = AV10Category;
         AV8ColumnsSelector.gxTpr_Columns.Add(AV14Column, 0);
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
         AV14Column = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         /* GeneXus formulas. */
      }

      private bool AV13IsVisible ;
      private string AV9ColumnName ;
      private string AV10Category ;
      private string AV11DisplayName ;
      private string AV12Fixed ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV8ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector aP0_ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column AV14Column ;
   }

}
