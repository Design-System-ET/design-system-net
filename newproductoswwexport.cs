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
   public class newproductoswwexport : GXProcedure
   {
      public newproductoswwexport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newproductoswwexport( IGxContext context )
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
         AV12Filename = GXt_char1 + "NewProductosWWExport-" + StringUtil.Trim( StringUtil.Str( (decimal)(AV16Random), 8, 0)) + ".xlsx";
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
         if ( ! ( (0==AV50TFNewProductosId) && (0==AV51TFNewProductosId_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Id", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = AV50TFNewProductosId;
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = AV51TFNewProductosId_To;
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV36TFNewProductosNombre_Sel)) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Nombre", "")) ;
            AV14CellRow = GXt_int2;
            GXt_char1 = "";
            new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  (String.IsNullOrEmpty(StringUtil.RTrim( AV36TFNewProductosNombre_Sel)) ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV36TFNewProductosNombre_Sel), out  GXt_char1) ;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
         }
         else
         {
            if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV35TFNewProductosNombre)) ) )
            {
               GXt_int2 = (short)(AV14CellRow);
               new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Nombre", "")) ;
               AV14CellRow = GXt_int2;
               GXt_char1 = "";
               new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  AV35TFNewProductosNombre, out  GXt_char1) ;
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
            }
         }
         if ( ! ( (0==AV39TFNewProductosNumeroDescargas) && (0==AV40TFNewProductosNumeroDescargas_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Descargas", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = AV39TFNewProductosNumeroDescargas;
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = AV40TFNewProductosNumeroDescargas_To;
         }
         if ( ! ( (0==AV46TFNewProductosNumeroVentas) && (0==AV47TFNewProductosNumeroVentas_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Ventas", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = AV46TFNewProductosNumeroVentas;
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = AV47TFNewProductosNumeroVentas_To;
         }
         if ( ! ( (0==AV48TFNewProductosVisitas) && (0==AV49TFNewProductosVisitas_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Visitas", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = AV48TFNewProductosVisitas;
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = AV49TFNewProductosVisitas_To;
         }
         AV14CellRow = (int)(AV14CellRow+2);
      }

      protected void S141( )
      {
         /* 'WRITECOLUMNTITLES' Routine */
         returnInSub = false;
         AV32VisibleColumnCount = 0;
         if ( StringUtil.StrCmp(AV20Session.Get("NewProductosWWColumnsSelector"), "") != 0 )
         {
            AV27ColumnsSelectorXML = AV20Session.Get("NewProductosWWColumnsSelector");
            AV24ColumnsSelector.FromXml(AV27ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S151 ();
            if (returnInSub) return;
         }
         AV24ColumnsSelector.gxTpr_Columns.Sort("Order");
         AV61GXV1 = 1;
         while ( AV61GXV1 <= AV24ColumnsSelector.gxTpr_Columns.Count )
         {
            AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV61GXV1));
            if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = context.GetMessage( (String.IsNullOrEmpty(StringUtil.RTrim( AV26ColumnsSelector_Column.gxTpr_Displayname)) ? AV26ColumnsSelector_Column.gxTpr_Columnname : AV26ColumnsSelector_Column.gxTpr_Displayname), "");
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Bold = 1;
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Color = 11;
               AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
            }
            AV61GXV1 = (int)(AV61GXV1+1);
         }
      }

      protected void S161( )
      {
         /* 'WRITEDATA' Routine */
         returnInSub = false;
         AV63Newproductoswwds_1_filterfulltext = AV19FilterFullText;
         AV64Newproductoswwds_2_tfnewproductosid = AV50TFNewProductosId;
         AV65Newproductoswwds_3_tfnewproductosid_to = AV51TFNewProductosId_To;
         AV66Newproductoswwds_4_tfnewproductosnombre = AV35TFNewProductosNombre;
         AV67Newproductoswwds_5_tfnewproductosnombre_sel = AV36TFNewProductosNombre_Sel;
         AV68Newproductoswwds_6_tfnewproductosnumerodescargas = AV39TFNewProductosNumeroDescargas;
         AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV40TFNewProductosNumeroDescargas_To;
         AV70Newproductoswwds_8_tfnewproductosnumeroventas = AV46TFNewProductosNumeroVentas;
         AV71Newproductoswwds_9_tfnewproductosnumeroventas_to = AV47TFNewProductosNumeroVentas_To;
         AV72Newproductoswwds_10_tfnewproductosvisitas = AV48TFNewProductosVisitas;
         AV73Newproductoswwds_11_tfnewproductosvisitas_to = AV49TFNewProductosVisitas_To;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV63Newproductoswwds_1_filterfulltext ,
                                              AV64Newproductoswwds_2_tfnewproductosid ,
                                              AV65Newproductoswwds_3_tfnewproductosid_to ,
                                              AV67Newproductoswwds_5_tfnewproductosnombre_sel ,
                                              AV66Newproductoswwds_4_tfnewproductosnombre ,
                                              AV68Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                              AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                              AV70Newproductoswwds_8_tfnewproductosnumeroventas ,
                                              AV71Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                              AV72Newproductoswwds_10_tfnewproductosvisitas ,
                                              AV73Newproductoswwds_11_tfnewproductosvisitas_to ,
                                              A34NewProductosId ,
                                              A36NewProductosNombre ,
                                              A39NewProductosNumeroDescargas ,
                                              A42NewProductosNumeroVentas ,
                                              A43NewProductosVisitas ,
                                              AV17OrderedBy ,
                                              AV18OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT,
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV63Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV63Newproductoswwds_1_filterfulltext), "%", "");
         lV63Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV63Newproductoswwds_1_filterfulltext), "%", "");
         lV63Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV63Newproductoswwds_1_filterfulltext), "%", "");
         lV63Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV63Newproductoswwds_1_filterfulltext), "%", "");
         lV63Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV63Newproductoswwds_1_filterfulltext), "%", "");
         lV66Newproductoswwds_4_tfnewproductosnombre = StringUtil.Concat( StringUtil.RTrim( AV66Newproductoswwds_4_tfnewproductosnombre), "%", "");
         /* Using cursor P002T2 */
         pr_default.execute(0, new Object[] {lV63Newproductoswwds_1_filterfulltext, lV63Newproductoswwds_1_filterfulltext, lV63Newproductoswwds_1_filterfulltext, lV63Newproductoswwds_1_filterfulltext, lV63Newproductoswwds_1_filterfulltext, AV64Newproductoswwds_2_tfnewproductosid, AV65Newproductoswwds_3_tfnewproductosid_to, lV66Newproductoswwds_4_tfnewproductosnombre, AV67Newproductoswwds_5_tfnewproductosnombre_sel, AV68Newproductoswwds_6_tfnewproductosnumerodescargas, AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to, AV70Newproductoswwds_8_tfnewproductosnumeroventas, AV71Newproductoswwds_9_tfnewproductosnumeroventas_to, AV72Newproductoswwds_10_tfnewproductosvisitas, AV73Newproductoswwds_11_tfnewproductosvisitas_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A43NewProductosVisitas = P002T2_A43NewProductosVisitas[0];
            A42NewProductosNumeroVentas = P002T2_A42NewProductosNumeroVentas[0];
            A39NewProductosNumeroDescargas = P002T2_A39NewProductosNumeroDescargas[0];
            A34NewProductosId = P002T2_A34NewProductosId[0];
            A36NewProductosNombre = P002T2_A36NewProductosNombre[0];
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
            AV74GXV2 = 1;
            while ( AV74GXV2 <= AV24ColumnsSelector.gxTpr_Columns.Count )
            {
               AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV74GXV2));
               if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
               {
                  if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewProductosId") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = A34NewProductosId;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewProductosImagen") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = "";
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewProductosNombre") == 0 )
                  {
                     GXt_char1 = "";
                     new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  A36NewProductosNombre, out  GXt_char1) ;
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = GXt_char1;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewProductosNumeroDescargas") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = A39NewProductosNumeroDescargas;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewProductosNumeroVentas") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = A42NewProductosNumeroVentas;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewProductosVisitas") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = A43NewProductosVisitas;
                  }
                  AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
               }
               AV74GXV2 = (int)(AV74GXV2+1);
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
         AV20Session.Set("WWPExportFileName", "NewProductosWWExport.xlsx");
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
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewProductosId",  "",  "Id",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewProductosImagen",  "",  "Imagen",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewProductosNombre",  "",  "Nombre",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewProductosNumeroDescargas",  "",  "Descargas",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewProductosNumeroVentas",  "",  "Ventas",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewProductosVisitas",  "",  "Visitas",  true,  "") ;
         GXt_char1 = AV28UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "NewProductosWWColumnsSelector", out  GXt_char1) ;
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
         if ( StringUtil.StrCmp(AV20Session.Get("NewProductosWWGridState"), "") == 0 )
         {
            AV22GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "NewProductosWWGridState"), null, "", "");
         }
         else
         {
            AV22GridState.FromXml(AV20Session.Get("NewProductosWWGridState"), null, "", "");
         }
         AV17OrderedBy = AV22GridState.gxTpr_Orderedby;
         AV18OrderedDsc = AV22GridState.gxTpr_Ordereddsc;
         AV75GXV3 = 1;
         while ( AV75GXV3 <= AV22GridState.gxTpr_Filtervalues.Count )
         {
            AV23GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV22GridState.gxTpr_Filtervalues.Item(AV75GXV3));
            if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV19FilterFullText = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSID") == 0 )
            {
               AV50TFNewProductosId = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV51TFNewProductosId_To = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE") == 0 )
            {
               AV35TFNewProductosNombre = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE_SEL") == 0 )
            {
               AV36TFNewProductosNombre_Sel = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMERODESCARGAS") == 0 )
            {
               AV39TFNewProductosNumeroDescargas = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV40TFNewProductosNumeroDescargas_To = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMEROVENTAS") == 0 )
            {
               AV46TFNewProductosNumeroVentas = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV47TFNewProductosNumeroVentas_To = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSVISITAS") == 0 )
            {
               AV48TFNewProductosVisitas = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV49TFNewProductosVisitas_To = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            AV75GXV3 = (int)(AV75GXV3+1);
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
         AV36TFNewProductosNombre_Sel = "";
         AV35TFNewProductosNombre = "";
         AV20Session = context.GetSession();
         AV27ColumnsSelectorXML = "";
         AV24ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV26ColumnsSelector_Column = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         AV63Newproductoswwds_1_filterfulltext = "";
         AV66Newproductoswwds_4_tfnewproductosnombre = "";
         AV67Newproductoswwds_5_tfnewproductosnombre_sel = "";
         lV63Newproductoswwds_1_filterfulltext = "";
         lV66Newproductoswwds_4_tfnewproductosnombre = "";
         A36NewProductosNombre = "";
         P002T2_A43NewProductosVisitas = new short[1] ;
         P002T2_A42NewProductosNumeroVentas = new short[1] ;
         P002T2_A39NewProductosNumeroDescargas = new short[1] ;
         P002T2_A34NewProductosId = new short[1] ;
         P002T2_A36NewProductosNombre = new string[] {""} ;
         AV28UserCustomValue = "";
         GXt_char1 = "";
         AV25ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV22GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV23GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newproductoswwexport__default(),
            new Object[][] {
                new Object[] {
               P002T2_A43NewProductosVisitas, P002T2_A42NewProductosNumeroVentas, P002T2_A39NewProductosNumeroDescargas, P002T2_A34NewProductosId, P002T2_A36NewProductosNombre
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV50TFNewProductosId ;
      private short AV51TFNewProductosId_To ;
      private short AV39TFNewProductosNumeroDescargas ;
      private short AV40TFNewProductosNumeroDescargas_To ;
      private short AV46TFNewProductosNumeroVentas ;
      private short AV47TFNewProductosNumeroVentas_To ;
      private short AV48TFNewProductosVisitas ;
      private short AV49TFNewProductosVisitas_To ;
      private short GXt_int2 ;
      private short AV64Newproductoswwds_2_tfnewproductosid ;
      private short AV65Newproductoswwds_3_tfnewproductosid_to ;
      private short AV68Newproductoswwds_6_tfnewproductosnumerodescargas ;
      private short AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to ;
      private short AV70Newproductoswwds_8_tfnewproductosnumeroventas ;
      private short AV71Newproductoswwds_9_tfnewproductosnumeroventas_to ;
      private short AV72Newproductoswwds_10_tfnewproductosvisitas ;
      private short AV73Newproductoswwds_11_tfnewproductosvisitas_to ;
      private short A34NewProductosId ;
      private short A39NewProductosNumeroDescargas ;
      private short A42NewProductosNumeroVentas ;
      private short A43NewProductosVisitas ;
      private short AV17OrderedBy ;
      private int AV14CellRow ;
      private int AV15FirstColumn ;
      private int AV16Random ;
      private int AV61GXV1 ;
      private int AV74GXV2 ;
      private int AV75GXV3 ;
      private long AV32VisibleColumnCount ;
      private string GXt_char1 ;
      private bool returnInSub ;
      private bool AV18OrderedDsc ;
      private string AV27ColumnsSelectorXML ;
      private string AV28UserCustomValue ;
      private string AV12Filename ;
      private string AV13ErrorMessage ;
      private string AV19FilterFullText ;
      private string AV36TFNewProductosNombre_Sel ;
      private string AV35TFNewProductosNombre ;
      private string AV63Newproductoswwds_1_filterfulltext ;
      private string AV66Newproductoswwds_4_tfnewproductosnombre ;
      private string AV67Newproductoswwds_5_tfnewproductosnombre_sel ;
      private string lV63Newproductoswwds_1_filterfulltext ;
      private string lV66Newproductoswwds_4_tfnewproductosnombre ;
      private string A36NewProductosNombre ;
      private IGxSession AV20Session ;
      private ExcelDocumentI AV11ExcelDocument ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV24ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column AV26ColumnsSelector_Column ;
      private IDataStoreProvider pr_default ;
      private short[] P002T2_A43NewProductosVisitas ;
      private short[] P002T2_A42NewProductosNumeroVentas ;
      private short[] P002T2_A39NewProductosNumeroDescargas ;
      private short[] P002T2_A34NewProductosId ;
      private string[] P002T2_A36NewProductosNombre ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV25ColumnsSelectorAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV22GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV23GridStateFilterValue ;
      private string aP0_Filename ;
      private string aP1_ErrorMessage ;
   }

   public class newproductoswwexport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002T2( IGxContext context ,
                                             string AV63Newproductoswwds_1_filterfulltext ,
                                             short AV64Newproductoswwds_2_tfnewproductosid ,
                                             short AV65Newproductoswwds_3_tfnewproductosid_to ,
                                             string AV67Newproductoswwds_5_tfnewproductosnombre_sel ,
                                             string AV66Newproductoswwds_4_tfnewproductosnombre ,
                                             short AV68Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                             short AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                             short AV70Newproductoswwds_8_tfnewproductosnumeroventas ,
                                             short AV71Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                             short AV72Newproductoswwds_10_tfnewproductosvisitas ,
                                             short AV73Newproductoswwds_11_tfnewproductosvisitas_to ,
                                             short A34NewProductosId ,
                                             string A36NewProductosNombre ,
                                             short A39NewProductosNumeroDescargas ,
                                             short A42NewProductosNumeroVentas ,
                                             short A43NewProductosVisitas ,
                                             short AV17OrderedBy ,
                                             bool AV18OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[15];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `NewProductosVisitas`, `NewProductosNumeroVentas`, `NewProductosNumeroDescargas`, `NewProductosId`, `NewProductosNombre` FROM `NewProductos`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV63Newproductoswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewProductosId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV63Newproductoswwds_1_filterfulltext)) or ( `NewProductosNombre` like CONCAT('%', @lV63Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroDescargas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV63Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroVentas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV63Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV63Newproductoswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
            GXv_int3[4] = 1;
         }
         if ( ! (0==AV64Newproductoswwds_2_tfnewproductosid) )
         {
            AddWhere(sWhereString, "(`NewProductosId` >= @AV64Newproductoswwds_2_tfnewproductosid)");
         }
         else
         {
            GXv_int3[5] = 1;
         }
         if ( ! (0==AV65Newproductoswwds_3_tfnewproductosid_to) )
         {
            AddWhere(sWhereString, "(`NewProductosId` <= @AV65Newproductoswwds_3_tfnewproductosid_to)");
         }
         else
         {
            GXv_int3[6] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV67Newproductoswwds_5_tfnewproductosnombre_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66Newproductoswwds_4_tfnewproductosnombre)) ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` like @lV66Newproductoswwds_4_tfnewproductosnombre)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Newproductoswwds_5_tfnewproductosnombre_sel)) && ! ( StringUtil.StrCmp(AV67Newproductoswwds_5_tfnewproductosnombre_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` = @AV67Newproductoswwds_5_tfnewproductosnombre_sel)");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( StringUtil.StrCmp(AV67Newproductoswwds_5_tfnewproductosnombre_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewProductosNombre`))=0))");
         }
         if ( ! (0==AV68Newproductoswwds_6_tfnewproductosnumerodescargas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` >= @AV68Newproductoswwds_6_tfnewproductosnumerodescargas)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( ! (0==AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` <= @AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV70Newproductoswwds_8_tfnewproductosnumeroventas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` >= @AV70Newproductoswwds_8_tfnewproductosnumeroventas)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( ! (0==AV71Newproductoswwds_9_tfnewproductosnumeroventas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` <= @AV71Newproductoswwds_9_tfnewproductosnumeroventas_to)");
         }
         else
         {
            GXv_int3[12] = 1;
         }
         if ( ! (0==AV72Newproductoswwds_10_tfnewproductosvisitas) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` >= @AV72Newproductoswwds_10_tfnewproductosvisitas)");
         }
         else
         {
            GXv_int3[13] = 1;
         }
         if ( ! (0==AV73Newproductoswwds_11_tfnewproductosvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` <= @AV73Newproductoswwds_11_tfnewproductosvisitas_to)");
         }
         else
         {
            GXv_int3[14] = 1;
         }
         scmdbuf += sWhereString;
         if ( ( AV17OrderedBy == 1 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosNombre`";
         }
         else if ( ( AV17OrderedBy == 1 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosNombre` DESC";
         }
         else if ( ( AV17OrderedBy == 2 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosId`";
         }
         else if ( ( AV17OrderedBy == 2 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosId` DESC";
         }
         else if ( ( AV17OrderedBy == 3 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroDescargas`";
         }
         else if ( ( AV17OrderedBy == 3 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroDescargas` DESC";
         }
         else if ( ( AV17OrderedBy == 4 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroVentas`";
         }
         else if ( ( AV17OrderedBy == 4 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosNumeroVentas` DESC";
         }
         else if ( ( AV17OrderedBy == 5 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewProductosVisitas`";
         }
         else if ( ( AV17OrderedBy == 5 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewProductosVisitas` DESC";
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
                     return conditional_P002T2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (short)dynConstraints[5] , (short)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (short)dynConstraints[13] , (short)dynConstraints[14] , (short)dynConstraints[15] , (short)dynConstraints[16] , (bool)dynConstraints[17] );
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
          Object[] prmP002T2;
          prmP002T2 = new Object[] {
          new ParDef("@lV63Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV63Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV63Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV63Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV63Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV64Newproductoswwds_2_tfnewproductosid",GXType.Int16,4,0) ,
          new ParDef("@AV65Newproductoswwds_3_tfnewproductosid_to",GXType.Int16,4,0) ,
          new ParDef("@lV66Newproductoswwds_4_tfnewproductosnombre",GXType.Char,200,0) ,
          new ParDef("@AV67Newproductoswwds_5_tfnewproductosnombre_sel",GXType.Char,200,0) ,
          new ParDef("@AV68Newproductoswwds_6_tfnewproductosnumerodescargas",GXType.Int16,4,0) ,
          new ParDef("@AV69Newproductoswwds_7_tfnewproductosnumerodescargas_to",GXType.Int16,4,0) ,
          new ParDef("@AV70Newproductoswwds_8_tfnewproductosnumeroventas",GXType.Int16,4,0) ,
          new ParDef("@AV71Newproductoswwds_9_tfnewproductosnumeroventas_to",GXType.Int16,4,0) ,
          new ParDef("@AV72Newproductoswwds_10_tfnewproductosvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV73Newproductoswwds_11_tfnewproductosvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002T2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002T2,100, GxCacheFrequency.OFF ,true,false )
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
                ((short[]) buf[0])[0] = rslt.getShort(1);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                ((short[]) buf[3])[0] = rslt.getShort(4);
                ((string[]) buf[4])[0] = rslt.getVarchar(5);
                return;
       }
    }

 }

}
