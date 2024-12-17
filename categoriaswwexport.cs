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
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Office;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   public class categoriaswwexport : GXProcedure
   {
      public categoriaswwexport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public categoriaswwexport( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out string aP0_Filename ,
                           out string aP1_ErrorMessage )
      {
         this.AV12Filename = "" ;
         this.AV13ErrorMessage = "" ;
         initialize();
         ExecuteImpl();
         aP0_Filename=this.AV12Filename;
         aP1_ErrorMessage=this.AV13ErrorMessage;
      }

      public string executeUdp( out string aP0_Filename )
      {
         execute(out aP0_Filename, out aP1_ErrorMessage);
         return AV13ErrorMessage ;
      }

      public void executeSubmit( out string aP0_Filename ,
                                 out string aP1_ErrorMessage )
      {
         this.AV12Filename = "" ;
         this.AV13ErrorMessage = "" ;
         SubmitImpl();
         aP0_Filename=this.AV12Filename;
         aP1_ErrorMessage=this.AV13ErrorMessage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'OPENDOCUMENT' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         AV14CellRow = 1;
         AV15FirstColumn = 1;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S201 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'WRITEFILTERS' */
         S131 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'WRITECOLUMNTITLES' */
         S141 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'WRITEDATA' */
         S161 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         /* Execute user subroutine: 'CLOSEDOCUMENT' */
         S191 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'OPENDOCUMENT' Routine */
         returnInSub = false;
         AV16Random = (int)(NumberUtil.Random( )*10000);
         GXt_char1 = AV12Filename;
         new DesignSystem.Programs.wwpbaseobjects.wwp_getdefaultexportpath(context ).execute( out  GXt_char1) ;
         AV12Filename = GXt_char1 + "CategoriasWWExport-" + StringUtil.Trim( StringUtil.Str( (decimal)(AV16Random), 8, 0)) + ".xlsx";
         AV11ExcelDocument.Open(AV12Filename);
         /* Execute user subroutine: 'CHECKSTATUS' */
         S121 ();
         if (returnInSub) return;
         AV11ExcelDocument.Clear();
      }

      protected void S131( )
      {
         /* 'WRITEFILTERS' Routine */
         returnInSub = false;
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV19FilterFullText)) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "WWP_FullTextFilterDescription", "")) ;
            AV14CellRow = GXt_int2;
            GXt_char1 = "";
            new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  AV19FilterFullText, out  GXt_char1) ;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV38TFCategoriasCategoria_Sel)) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Categoria", "")) ;
            AV14CellRow = GXt_int2;
            GXt_char1 = "";
            new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  (String.IsNullOrEmpty(StringUtil.RTrim( AV38TFCategoriasCategoria_Sel)) ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV38TFCategoriasCategoria_Sel), out  GXt_char1) ;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
         }
         else
         {
            if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV37TFCategoriasCategoria)) ) )
            {
               GXt_int2 = (short)(AV14CellRow);
               new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Categoria", "")) ;
               AV14CellRow = GXt_int2;
               GXt_char1 = "";
               new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  AV37TFCategoriasCategoria, out  GXt_char1) ;
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
            }
         }
         AV14CellRow = (int)(AV14CellRow+2);
      }

      protected void S141( )
      {
         /* 'WRITECOLUMNTITLES' Routine */
         returnInSub = false;
         AV32VisibleColumnCount = 0;
         if ( StringUtil.StrCmp(AV20Session.Get("CategoriasWWColumnsSelector"), "") != 0 )
         {
            AV27ColumnsSelectorXML = AV20Session.Get("CategoriasWWColumnsSelector");
            AV24ColumnsSelector.FromXml(AV27ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S151 ();
            if (returnInSub) return;
         }
         AV24ColumnsSelector.gxTpr_Columns.Sort("Order");
         AV48GXV1 = 1;
         while ( AV48GXV1 <= AV24ColumnsSelector.gxTpr_Columns.Count )
         {
            AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV48GXV1));
            if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = context.GetMessage( (String.IsNullOrEmpty(StringUtil.RTrim( AV26ColumnsSelector_Column.gxTpr_Displayname)) ? AV26ColumnsSelector_Column.gxTpr_Columnname : AV26ColumnsSelector_Column.gxTpr_Displayname), "");
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Bold = 1;
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Color = 11;
               AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
            }
            AV48GXV1 = (int)(AV48GXV1+1);
         }
      }

      protected void S161( )
      {
         /* 'WRITEDATA' Routine */
         returnInSub = false;
         AV50Categoriaswwds_1_filterfulltext = AV19FilterFullText;
         AV51Categoriaswwds_2_tfcategoriascategoria = AV37TFCategoriasCategoria;
         AV52Categoriaswwds_3_tfcategoriascategoria_sel = AV38TFCategoriasCategoria_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV50Categoriaswwds_1_filterfulltext ,
                                              AV52Categoriaswwds_3_tfcategoriascategoria_sel ,
                                              AV51Categoriaswwds_2_tfcategoriascategoria ,
                                              A21CategoriasCategoria ,
                                              AV18OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.BOOLEAN
                                              }
         });
         lV50Categoriaswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV50Categoriaswwds_1_filterfulltext), "%", "");
         lV51Categoriaswwds_2_tfcategoriascategoria = StringUtil.PadR( StringUtil.RTrim( AV51Categoriaswwds_2_tfcategoriascategoria), 100, "%");
         /* Using cursor P002L2 */
         pr_default.execute(0, new Object[] {lV50Categoriaswwds_1_filterfulltext, lV51Categoriaswwds_2_tfcategoriascategoria, AV52Categoriaswwds_3_tfcategoriascategoria_sel});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A21CategoriasCategoria = P002L2_A21CategoriasCategoria[0];
            A20CategoriasId = P002L2_A20CategoriasId[0];
            AV14CellRow = (int)(AV14CellRow+1);
            /* Execute user subroutine: 'BEFOREWRITELINE' */
            S172 ();
            if ( returnInSub )
            {
               pr_default.close(0);
               returnInSub = true;
               if (true) return;
            }
            AV32VisibleColumnCount = 0;
            AV53GXV2 = 1;
            while ( AV53GXV2 <= AV24ColumnsSelector.gxTpr_Columns.Count )
            {
               AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV53GXV2));
               if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
               {
                  if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "CategoriasCategoria") == 0 )
                  {
                     GXt_char1 = "";
                     new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  A21CategoriasCategoria, out  GXt_char1) ;
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = GXt_char1;
                  }
                  AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
               }
               AV53GXV2 = (int)(AV53GXV2+1);
            }
            /* Execute user subroutine: 'AFTERWRITELINE' */
            S182 ();
            if ( returnInSub )
            {
               pr_default.close(0);
               returnInSub = true;
               if (true) return;
            }
            pr_default.readNext(0);
         }
         pr_default.close(0);
      }

      protected void S191( )
      {
         /* 'CLOSEDOCUMENT' Routine */
         returnInSub = false;
         AV11ExcelDocument.Save();
         /* Execute user subroutine: 'CHECKSTATUS' */
         S121 ();
         if (returnInSub) return;
         AV11ExcelDocument.Close();
         AV20Session.Set("WWPExportFilePath", AV12Filename);
         AV20Session.Set("WWPExportFileName", "CategoriasWWExport.xlsx");
         AV12Filename = formatLink("wwpbaseobjects.wwp_downloadreport.aspx") ;
      }

      protected void S121( )
      {
         /* 'CHECKSTATUS' Routine */
         returnInSub = false;
         if ( AV11ExcelDocument.ErrCode != 0 )
         {
            AV12Filename = "";
            AV13ErrorMessage = AV11ExcelDocument.ErrDescription;
            AV11ExcelDocument.Close();
            returnInSub = true;
            if (true) return;
         }
      }

      protected void S151( )
      {
         /* 'INITIALIZECOLUMNSSELECTOR' Routine */
         returnInSub = false;
         AV24ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "CategoriasCategoria",  "",  "Categoria",  true,  "") ;
         GXt_char1 = AV28UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "CategoriasWWColumnsSelector", out  GXt_char1) ;
         AV28UserCustomValue = GXt_char1;
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV28UserCustomValue)) ) )
         {
            AV25ColumnsSelectorAux.FromXml(AV28UserCustomValue, null, "", "");
            new DesignSystem.Programs.wwpbaseobjects.wwp_columnselector_updatecolumns(context ).execute( ref  AV25ColumnsSelectorAux, ref  AV24ColumnsSelector) ;
         }
      }

      protected void S201( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV20Session.Get("CategoriasWWGridState"), "") == 0 )
         {
            AV22GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "CategoriasWWGridState"), null, "", "");
         }
         else
         {
            AV22GridState.FromXml(AV20Session.Get("CategoriasWWGridState"), null, "", "");
         }
         AV18OrderedDsc = AV22GridState.gxTpr_Ordereddsc;
         AV54GXV3 = 1;
         while ( AV54GXV3 <= AV22GridState.gxTpr_Filtervalues.Count )
         {
            AV23GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV22GridState.gxTpr_Filtervalues.Item(AV54GXV3));
            if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV19FilterFullText = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCATEGORIASCATEGORIA") == 0 )
            {
               AV37TFCategoriasCategoria = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCATEGORIASCATEGORIA_SEL") == 0 )
            {
               AV38TFCategoriasCategoria_Sel = AV23GridStateFilterValue.gxTpr_Value;
            }
            AV54GXV3 = (int)(AV54GXV3+1);
         }
      }

      protected void S172( )
      {
         /* 'BEFOREWRITELINE' Routine */
         returnInSub = false;
      }

      protected void S182( )
      {
         /* 'AFTERWRITELINE' Routine */
         returnInSub = false;
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
         AV12Filename = "";
         AV13ErrorMessage = "";
         AV9WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV11ExcelDocument = new ExcelDocumentI();
         AV19FilterFullText = "";
         AV38TFCategoriasCategoria_Sel = "";
         AV37TFCategoriasCategoria = "";
         AV20Session = context.GetSession();
         AV27ColumnsSelectorXML = "";
         AV24ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV26ColumnsSelector_Column = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         AV50Categoriaswwds_1_filterfulltext = "";
         AV51Categoriaswwds_2_tfcategoriascategoria = "";
         AV52Categoriaswwds_3_tfcategoriascategoria_sel = "";
         lV50Categoriaswwds_1_filterfulltext = "";
         lV51Categoriaswwds_2_tfcategoriascategoria = "";
         A21CategoriasCategoria = "";
         P002L2_A21CategoriasCategoria = new string[] {""} ;
         P002L2_A20CategoriasId = new short[1] ;
         AV28UserCustomValue = "";
         GXt_char1 = "";
         AV25ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV22GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV23GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.categoriaswwexport__default(),
            new Object[][] {
                new Object[] {
               P002L2_A21CategoriasCategoria, P002L2_A20CategoriasId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short GXt_int2 ;
      private short A20CategoriasId ;
      private int AV14CellRow ;
      private int AV15FirstColumn ;
      private int AV16Random ;
      private int AV48GXV1 ;
      private int AV53GXV2 ;
      private int AV54GXV3 ;
      private long AV32VisibleColumnCount ;
      private string AV38TFCategoriasCategoria_Sel ;
      private string AV37TFCategoriasCategoria ;
      private string AV51Categoriaswwds_2_tfcategoriascategoria ;
      private string AV52Categoriaswwds_3_tfcategoriascategoria_sel ;
      private string lV51Categoriaswwds_2_tfcategoriascategoria ;
      private string A21CategoriasCategoria ;
      private string GXt_char1 ;
      private bool returnInSub ;
      private bool AV18OrderedDsc ;
      private string AV27ColumnsSelectorXML ;
      private string AV28UserCustomValue ;
      private string AV12Filename ;
      private string AV13ErrorMessage ;
      private string AV19FilterFullText ;
      private string AV50Categoriaswwds_1_filterfulltext ;
      private string lV50Categoriaswwds_1_filterfulltext ;
      private IGxSession AV20Session ;
      private ExcelDocumentI AV11ExcelDocument ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV24ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column AV26ColumnsSelector_Column ;
      private IDataStoreProvider pr_default ;
      private string[] P002L2_A21CategoriasCategoria ;
      private short[] P002L2_A20CategoriasId ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV25ColumnsSelectorAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV22GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV23GridStateFilterValue ;
      private string aP0_Filename ;
      private string aP1_ErrorMessage ;
   }

   public class categoriaswwexport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002L2( IGxContext context ,
                                             string AV50Categoriaswwds_1_filterfulltext ,
                                             string AV52Categoriaswwds_3_tfcategoriascategoria_sel ,
                                             string AV51Categoriaswwds_2_tfcategoriascategoria ,
                                             string A21CategoriasCategoria ,
                                             bool AV18OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[3];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `CategoriasCategoria`, `CategoriasId` FROM `Categorias`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV50Categoriaswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( `CategoriasCategoria` like CONCAT('%', @lV50Categoriaswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV52Categoriaswwds_3_tfcategoriascategoria_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV51Categoriaswwds_2_tfcategoriascategoria)) ) )
         {
            AddWhere(sWhereString, "(`CategoriasCategoria` like @lV51Categoriaswwds_2_tfcategoriascategoria)");
         }
         else
         {
            GXv_int3[1] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV52Categoriaswwds_3_tfcategoriascategoria_sel)) && ! ( StringUtil.StrCmp(AV52Categoriaswwds_3_tfcategoriascategoria_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`CategoriasCategoria` = @AV52Categoriaswwds_3_tfcategoriascategoria_sel)");
         }
         else
         {
            GXv_int3[2] = 1;
         }
         if ( StringUtil.StrCmp(AV52Categoriaswwds_3_tfcategoriascategoria_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`CategoriasCategoria`))=0))");
         }
         scmdbuf += sWhereString;
         if ( ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `CategoriasCategoria`";
         }
         else if ( AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `CategoriasCategoria` DESC";
         }
         GXv_Object4[0] = scmdbuf;
         GXv_Object4[1] = GXv_int3;
         return GXv_Object4 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_P002L2(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] , (bool)dynConstraints[4] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP002L2;
          prmP002L2 = new Object[] {
          new ParDef("@lV50Categoriaswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV51Categoriaswwds_2_tfcategoriascategoria",GXType.Char,100,0) ,
          new ParDef("@AV52Categoriaswwds_3_tfcategoriascategoria_sel",GXType.Char,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002L2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002L2,100, GxCacheFrequency.OFF ,true,false )
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
       switch ( cursor )
       {
             case 0 :
                ((string[]) buf[0])[0] = rslt.getString(1, 100);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                return;
       }
    }

 }

}
