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
   public class newblogwwexport : GXProcedure
   {
      public newblogwwexport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newblogwwexport( IGxContext context )
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
         AV12Filename = GXt_char1 + "NewBlogWWExport-" + StringUtil.Trim( StringUtil.Str( (decimal)(AV16Random), 8, 0)) + ".xlsx";
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
         if ( ! ( (0==AV35TFNewBlogId) && (0==AV36TFNewBlogId_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Id", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = AV35TFNewBlogId;
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = AV36TFNewBlogId_To;
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV38TFNewBlogTitulo_Sel)) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Titulo", "")) ;
            AV14CellRow = GXt_int2;
            GXt_char1 = "";
            new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  (String.IsNullOrEmpty(StringUtil.RTrim( AV38TFNewBlogTitulo_Sel)) ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV38TFNewBlogTitulo_Sel), out  GXt_char1) ;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
         }
         else
         {
            if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV37TFNewBlogTitulo)) ) )
            {
               GXt_int2 = (short)(AV14CellRow);
               new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Titulo", "")) ;
               AV14CellRow = GXt_int2;
               GXt_char1 = "";
               new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  AV37TFNewBlogTitulo, out  GXt_char1) ;
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
            }
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV40TFNewBlogSubTitulo_Sel)) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "SubTitulo", "")) ;
            AV14CellRow = GXt_int2;
            GXt_char1 = "";
            new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  (String.IsNullOrEmpty(StringUtil.RTrim( AV40TFNewBlogSubTitulo_Sel)) ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV40TFNewBlogSubTitulo_Sel), out  GXt_char1) ;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
         }
         else
         {
            if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV39TFNewBlogSubTitulo)) ) )
            {
               GXt_int2 = (short)(AV14CellRow);
               new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "SubTitulo", "")) ;
               AV14CellRow = GXt_int2;
               GXt_char1 = "";
               new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  AV39TFNewBlogSubTitulo, out  GXt_char1) ;
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
            }
         }
         if ( ! ( (0==AV46TFNewBlogDestacado_Sel) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Destacado", "")) ;
            AV14CellRow = GXt_int2;
            if ( AV46TFNewBlogDestacado_Sel == 1 )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = context.GetMessage( "WWP_TSChecked", "");
            }
            else if ( AV46TFNewBlogDestacado_Sel == 2 )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = context.GetMessage( "WWP_TSUnChecked", "");
            }
         }
         if ( ! ( (0==AV47TFNewBlogVisitas) && (0==AV48TFNewBlogVisitas_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Visitas", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = AV47TFNewBlogVisitas;
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = AV48TFNewBlogVisitas_To;
         }
         if ( ! ( (0==AV49TFNewBlogBorrador_Sel) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Borrador", "")) ;
            AV14CellRow = GXt_int2;
            if ( AV49TFNewBlogBorrador_Sel == 1 )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = context.GetMessage( "WWP_TSChecked", "");
            }
            else if ( AV49TFNewBlogBorrador_Sel == 2 )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = context.GetMessage( "WWP_TSUnChecked", "");
            }
         }
         AV14CellRow = (int)(AV14CellRow+2);
      }

      protected void S141( )
      {
         /* 'WRITECOLUMNTITLES' Routine */
         returnInSub = false;
         AV32VisibleColumnCount = 0;
         if ( StringUtil.StrCmp(AV20Session.Get("NewBlogWWColumnsSelector"), "") != 0 )
         {
            AV27ColumnsSelectorXML = AV20Session.Get("NewBlogWWColumnsSelector");
            AV24ColumnsSelector.FromXml(AV27ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S151 ();
            if (returnInSub) return;
         }
         AV24ColumnsSelector.gxTpr_Columns.Sort("Order");
         AV59GXV1 = 1;
         while ( AV59GXV1 <= AV24ColumnsSelector.gxTpr_Columns.Count )
         {
            AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV59GXV1));
            if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = context.GetMessage( (String.IsNullOrEmpty(StringUtil.RTrim( AV26ColumnsSelector_Column.gxTpr_Displayname)) ? AV26ColumnsSelector_Column.gxTpr_Columnname : AV26ColumnsSelector_Column.gxTpr_Displayname), "");
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Bold = 1;
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Color = 11;
               AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
            }
            AV59GXV1 = (int)(AV59GXV1+1);
         }
      }

      protected void S161( )
      {
         /* 'WRITEDATA' Routine */
         returnInSub = false;
         AV61Newblogwwds_1_filterfulltext = AV19FilterFullText;
         AV62Newblogwwds_2_tfnewblogid = AV35TFNewBlogId;
         AV63Newblogwwds_3_tfnewblogid_to = AV36TFNewBlogId_To;
         AV64Newblogwwds_4_tfnewblogtitulo = AV37TFNewBlogTitulo;
         AV65Newblogwwds_5_tfnewblogtitulo_sel = AV38TFNewBlogTitulo_Sel;
         AV66Newblogwwds_6_tfnewblogsubtitulo = AV39TFNewBlogSubTitulo;
         AV67Newblogwwds_7_tfnewblogsubtitulo_sel = AV40TFNewBlogSubTitulo_Sel;
         AV68Newblogwwds_8_tfnewblogdestacado_sel = AV46TFNewBlogDestacado_Sel;
         AV69Newblogwwds_9_tfnewblogvisitas = AV47TFNewBlogVisitas;
         AV70Newblogwwds_10_tfnewblogvisitas_to = AV48TFNewBlogVisitas_To;
         AV71Newblogwwds_11_tfnewblogborrador_sel = AV49TFNewBlogBorrador_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV61Newblogwwds_1_filterfulltext ,
                                              AV62Newblogwwds_2_tfnewblogid ,
                                              AV63Newblogwwds_3_tfnewblogid_to ,
                                              AV65Newblogwwds_5_tfnewblogtitulo_sel ,
                                              AV64Newblogwwds_4_tfnewblogtitulo ,
                                              AV67Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                              AV66Newblogwwds_6_tfnewblogsubtitulo ,
                                              AV68Newblogwwds_8_tfnewblogdestacado_sel ,
                                              AV69Newblogwwds_9_tfnewblogvisitas ,
                                              AV70Newblogwwds_10_tfnewblogvisitas_to ,
                                              AV71Newblogwwds_11_tfnewblogborrador_sel ,
                                              A12NewBlogId ,
                                              A14NewBlogTitulo ,
                                              A15NewBlogSubTitulo ,
                                              A18NewBlogVisitas ,
                                              A19NewBlogDestacado ,
                                              A25NewBlogBorrador ,
                                              AV17OrderedBy ,
                                              AV18OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.BOOLEAN,
                                              TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV61Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_1_filterfulltext), "%", "");
         lV61Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_1_filterfulltext), "%", "");
         lV61Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_1_filterfulltext), "%", "");
         lV61Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_1_filterfulltext), "%", "");
         lV64Newblogwwds_4_tfnewblogtitulo = StringUtil.Concat( StringUtil.RTrim( AV64Newblogwwds_4_tfnewblogtitulo), "%", "");
         lV66Newblogwwds_6_tfnewblogsubtitulo = StringUtil.Concat( StringUtil.RTrim( AV66Newblogwwds_6_tfnewblogsubtitulo), "%", "");
         /* Using cursor P002I2 */
         pr_default.execute(0, new Object[] {lV61Newblogwwds_1_filterfulltext, lV61Newblogwwds_1_filterfulltext, lV61Newblogwwds_1_filterfulltext, lV61Newblogwwds_1_filterfulltext, AV62Newblogwwds_2_tfnewblogid, AV63Newblogwwds_3_tfnewblogid_to, lV64Newblogwwds_4_tfnewblogtitulo, AV65Newblogwwds_5_tfnewblogtitulo_sel, lV66Newblogwwds_6_tfnewblogsubtitulo, AV67Newblogwwds_7_tfnewblogsubtitulo_sel, AV69Newblogwwds_9_tfnewblogvisitas, AV70Newblogwwds_10_tfnewblogvisitas_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A25NewBlogBorrador = P002I2_A25NewBlogBorrador[0];
            A18NewBlogVisitas = P002I2_A18NewBlogVisitas[0];
            A19NewBlogDestacado = P002I2_A19NewBlogDestacado[0];
            A12NewBlogId = P002I2_A12NewBlogId[0];
            A15NewBlogSubTitulo = P002I2_A15NewBlogSubTitulo[0];
            A14NewBlogTitulo = P002I2_A14NewBlogTitulo[0];
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
            AV72GXV2 = 1;
            while ( AV72GXV2 <= AV24ColumnsSelector.gxTpr_Columns.Count )
            {
               AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV72GXV2));
               if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
               {
                  if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewBlogId") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = A12NewBlogId;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewBlogImagen") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = "";
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewBlogTitulo") == 0 )
                  {
                     GXt_char1 = "";
                     new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  A14NewBlogTitulo, out  GXt_char1) ;
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = GXt_char1;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewBlogSubTitulo") == 0 )
                  {
                     GXt_char1 = "";
                     new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  A15NewBlogSubTitulo, out  GXt_char1) ;
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = GXt_char1;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewBlogDestacado") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = StringUtil.BoolToStr( A19NewBlogDestacado);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewBlogVisitas") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = A18NewBlogVisitas;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "NewBlogBorrador") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = StringUtil.BoolToStr( A25NewBlogBorrador);
                  }
                  AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
               }
               AV72GXV2 = (int)(AV72GXV2+1);
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
         AV20Session.Set("WWPExportFileName", "NewBlogWWExport.xlsx");
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
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewBlogId",  "",  "Id",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewBlogImagen",  "",  "Imagen",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewBlogTitulo",  "",  "Titulo",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewBlogSubTitulo",  "",  "SubTitulo",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewBlogDestacado",  "",  "Destacado",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewBlogVisitas",  "",  "Visitas",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "NewBlogBorrador",  "",  "Borrador",  true,  "") ;
         GXt_char1 = AV28UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "NewBlogWWColumnsSelector", out  GXt_char1) ;
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
         if ( StringUtil.StrCmp(AV20Session.Get("NewBlogWWGridState"), "") == 0 )
         {
            AV22GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "NewBlogWWGridState"), null, "", "");
         }
         else
         {
            AV22GridState.FromXml(AV20Session.Get("NewBlogWWGridState"), null, "", "");
         }
         AV17OrderedBy = AV22GridState.gxTpr_Orderedby;
         AV18OrderedDsc = AV22GridState.gxTpr_Ordereddsc;
         AV73GXV3 = 1;
         while ( AV73GXV3 <= AV22GridState.gxTpr_Filtervalues.Count )
         {
            AV23GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV22GridState.gxTpr_Filtervalues.Item(AV73GXV3));
            if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV19FilterFullText = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGID") == 0 )
            {
               AV35TFNewBlogId = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV36TFNewBlogId_To = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO") == 0 )
            {
               AV37TFNewBlogTitulo = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO_SEL") == 0 )
            {
               AV38TFNewBlogTitulo_Sel = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO") == 0 )
            {
               AV39TFNewBlogSubTitulo = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO_SEL") == 0 )
            {
               AV40TFNewBlogSubTitulo_Sel = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGDESTACADO_SEL") == 0 )
            {
               AV46TFNewBlogDestacado_Sel = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGVISITAS") == 0 )
            {
               AV47TFNewBlogVisitas = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV48TFNewBlogVisitas_To = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFNEWBLOGBORRADOR_SEL") == 0 )
            {
               AV49TFNewBlogBorrador_Sel = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
            }
            AV73GXV3 = (int)(AV73GXV3+1);
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
         AV38TFNewBlogTitulo_Sel = "";
         AV37TFNewBlogTitulo = "";
         AV40TFNewBlogSubTitulo_Sel = "";
         AV39TFNewBlogSubTitulo = "";
         AV20Session = context.GetSession();
         AV27ColumnsSelectorXML = "";
         AV24ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV26ColumnsSelector_Column = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         AV61Newblogwwds_1_filterfulltext = "";
         AV64Newblogwwds_4_tfnewblogtitulo = "";
         AV65Newblogwwds_5_tfnewblogtitulo_sel = "";
         AV66Newblogwwds_6_tfnewblogsubtitulo = "";
         AV67Newblogwwds_7_tfnewblogsubtitulo_sel = "";
         lV61Newblogwwds_1_filterfulltext = "";
         lV64Newblogwwds_4_tfnewblogtitulo = "";
         lV66Newblogwwds_6_tfnewblogsubtitulo = "";
         A14NewBlogTitulo = "";
         A15NewBlogSubTitulo = "";
         P002I2_A25NewBlogBorrador = new bool[] {false} ;
         P002I2_A18NewBlogVisitas = new short[1] ;
         P002I2_A19NewBlogDestacado = new bool[] {false} ;
         P002I2_A12NewBlogId = new short[1] ;
         P002I2_A15NewBlogSubTitulo = new string[] {""} ;
         P002I2_A14NewBlogTitulo = new string[] {""} ;
         AV28UserCustomValue = "";
         GXt_char1 = "";
         AV25ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV22GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV23GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newblogwwexport__default(),
            new Object[][] {
                new Object[] {
               P002I2_A25NewBlogBorrador, P002I2_A18NewBlogVisitas, P002I2_A19NewBlogDestacado, P002I2_A12NewBlogId, P002I2_A15NewBlogSubTitulo, P002I2_A14NewBlogTitulo
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV35TFNewBlogId ;
      private short AV36TFNewBlogId_To ;
      private short AV46TFNewBlogDestacado_Sel ;
      private short AV47TFNewBlogVisitas ;
      private short AV48TFNewBlogVisitas_To ;
      private short AV49TFNewBlogBorrador_Sel ;
      private short GXt_int2 ;
      private short AV62Newblogwwds_2_tfnewblogid ;
      private short AV63Newblogwwds_3_tfnewblogid_to ;
      private short AV68Newblogwwds_8_tfnewblogdestacado_sel ;
      private short AV69Newblogwwds_9_tfnewblogvisitas ;
      private short AV70Newblogwwds_10_tfnewblogvisitas_to ;
      private short AV71Newblogwwds_11_tfnewblogborrador_sel ;
      private short A12NewBlogId ;
      private short A18NewBlogVisitas ;
      private short AV17OrderedBy ;
      private int AV14CellRow ;
      private int AV15FirstColumn ;
      private int AV16Random ;
      private int AV59GXV1 ;
      private int AV72GXV2 ;
      private int AV73GXV3 ;
      private long AV32VisibleColumnCount ;
      private string GXt_char1 ;
      private bool returnInSub ;
      private bool A19NewBlogDestacado ;
      private bool A25NewBlogBorrador ;
      private bool AV18OrderedDsc ;
      private string AV27ColumnsSelectorXML ;
      private string AV28UserCustomValue ;
      private string AV12Filename ;
      private string AV13ErrorMessage ;
      private string AV19FilterFullText ;
      private string AV38TFNewBlogTitulo_Sel ;
      private string AV37TFNewBlogTitulo ;
      private string AV40TFNewBlogSubTitulo_Sel ;
      private string AV39TFNewBlogSubTitulo ;
      private string AV61Newblogwwds_1_filterfulltext ;
      private string AV64Newblogwwds_4_tfnewblogtitulo ;
      private string AV65Newblogwwds_5_tfnewblogtitulo_sel ;
      private string AV66Newblogwwds_6_tfnewblogsubtitulo ;
      private string AV67Newblogwwds_7_tfnewblogsubtitulo_sel ;
      private string lV61Newblogwwds_1_filterfulltext ;
      private string lV64Newblogwwds_4_tfnewblogtitulo ;
      private string lV66Newblogwwds_6_tfnewblogsubtitulo ;
      private string A14NewBlogTitulo ;
      private string A15NewBlogSubTitulo ;
      private IGxSession AV20Session ;
      private ExcelDocumentI AV11ExcelDocument ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV24ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column AV26ColumnsSelector_Column ;
      private IDataStoreProvider pr_default ;
      private bool[] P002I2_A25NewBlogBorrador ;
      private short[] P002I2_A18NewBlogVisitas ;
      private bool[] P002I2_A19NewBlogDestacado ;
      private short[] P002I2_A12NewBlogId ;
      private string[] P002I2_A15NewBlogSubTitulo ;
      private string[] P002I2_A14NewBlogTitulo ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV25ColumnsSelectorAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV22GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV23GridStateFilterValue ;
      private string aP0_Filename ;
      private string aP1_ErrorMessage ;
   }

   public class newblogwwexport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002I2( IGxContext context ,
                                             string AV61Newblogwwds_1_filterfulltext ,
                                             short AV62Newblogwwds_2_tfnewblogid ,
                                             short AV63Newblogwwds_3_tfnewblogid_to ,
                                             string AV65Newblogwwds_5_tfnewblogtitulo_sel ,
                                             string AV64Newblogwwds_4_tfnewblogtitulo ,
                                             string AV67Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                             string AV66Newblogwwds_6_tfnewblogsubtitulo ,
                                             short AV68Newblogwwds_8_tfnewblogdestacado_sel ,
                                             short AV69Newblogwwds_9_tfnewblogvisitas ,
                                             short AV70Newblogwwds_10_tfnewblogvisitas_to ,
                                             short AV71Newblogwwds_11_tfnewblogborrador_sel ,
                                             short A12NewBlogId ,
                                             string A14NewBlogTitulo ,
                                             string A15NewBlogSubTitulo ,
                                             short A18NewBlogVisitas ,
                                             bool A19NewBlogDestacado ,
                                             bool A25NewBlogBorrador ,
                                             short AV17OrderedBy ,
                                             bool AV18OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[12];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `NewBlogBorrador`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogId`, `NewBlogSubTitulo`, `NewBlogTitulo` FROM `NewBlog`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newblogwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewBlogId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV61Newblogwwds_1_filterfulltext)) or ( `NewBlogTitulo` like CONCAT('%', @lV61Newblogwwds_1_filterfulltext)) or ( `NewBlogSubTitulo` like CONCAT('%', @lV61Newblogwwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewBlogVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV61Newblogwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
         }
         if ( ! (0==AV62Newblogwwds_2_tfnewblogid) )
         {
            AddWhere(sWhereString, "(`NewBlogId` >= @AV62Newblogwwds_2_tfnewblogid)");
         }
         else
         {
            GXv_int3[4] = 1;
         }
         if ( ! (0==AV63Newblogwwds_3_tfnewblogid_to) )
         {
            AddWhere(sWhereString, "(`NewBlogId` <= @AV63Newblogwwds_3_tfnewblogid_to)");
         }
         else
         {
            GXv_int3[5] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV65Newblogwwds_5_tfnewblogtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV64Newblogwwds_4_tfnewblogtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` like @lV64Newblogwwds_4_tfnewblogtitulo)");
         }
         else
         {
            GXv_int3[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65Newblogwwds_5_tfnewblogtitulo_sel)) && ! ( StringUtil.StrCmp(AV65Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` = @AV65Newblogwwds_5_tfnewblogtitulo_sel)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( StringUtil.StrCmp(AV65Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogTitulo`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV67Newblogwwds_7_tfnewblogsubtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66Newblogwwds_6_tfnewblogsubtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` like @lV66Newblogwwds_6_tfnewblogsubtitulo)");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Newblogwwds_7_tfnewblogsubtitulo_sel)) && ! ( StringUtil.StrCmp(AV67Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` = @AV67Newblogwwds_7_tfnewblogsubtitulo_sel)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( StringUtil.StrCmp(AV67Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogSubTitulo`))=0))");
         }
         if ( AV68Newblogwwds_8_tfnewblogdestacado_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 1)");
         }
         if ( AV68Newblogwwds_8_tfnewblogdestacado_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 0)");
         }
         if ( ! (0==AV69Newblogwwds_9_tfnewblogvisitas) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` >= @AV69Newblogwwds_9_tfnewblogvisitas)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV70Newblogwwds_10_tfnewblogvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` <= @AV70Newblogwwds_10_tfnewblogvisitas_to)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( AV71Newblogwwds_11_tfnewblogborrador_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 1)");
         }
         if ( AV71Newblogwwds_11_tfnewblogborrador_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 0)");
         }
         scmdbuf += sWhereString;
         if ( ( AV17OrderedBy == 1 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogTitulo`";
         }
         else if ( ( AV17OrderedBy == 1 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogTitulo` DESC";
         }
         else if ( ( AV17OrderedBy == 2 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogId`";
         }
         else if ( ( AV17OrderedBy == 2 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogId` DESC";
         }
         else if ( ( AV17OrderedBy == 3 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogSubTitulo`";
         }
         else if ( ( AV17OrderedBy == 3 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogSubTitulo` DESC";
         }
         else if ( ( AV17OrderedBy == 4 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogDestacado`";
         }
         else if ( ( AV17OrderedBy == 4 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogDestacado` DESC";
         }
         else if ( ( AV17OrderedBy == 5 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogVisitas`";
         }
         else if ( ( AV17OrderedBy == 5 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogVisitas` DESC";
         }
         else if ( ( AV17OrderedBy == 6 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `NewBlogBorrador`";
         }
         else if ( ( AV17OrderedBy == 6 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `NewBlogBorrador` DESC";
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
                     return conditional_P002I2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (bool)dynConstraints[16] , (short)dynConstraints[17] , (bool)dynConstraints[18] );
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
          Object[] prmP002I2;
          prmP002I2 = new Object[] {
          new ParDef("@lV61Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV61Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV61Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV61Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV62Newblogwwds_2_tfnewblogid",GXType.Int16,4,0) ,
          new ParDef("@AV63Newblogwwds_3_tfnewblogid_to",GXType.Int16,4,0) ,
          new ParDef("@lV64Newblogwwds_4_tfnewblogtitulo",GXType.Char,200,0) ,
          new ParDef("@AV65Newblogwwds_5_tfnewblogtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@lV66Newblogwwds_6_tfnewblogsubtitulo",GXType.Char,200,0) ,
          new ParDef("@AV67Newblogwwds_7_tfnewblogsubtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@AV69Newblogwwds_9_tfnewblogvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV70Newblogwwds_10_tfnewblogvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002I2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002I2,100, GxCacheFrequency.OFF ,true,false )
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
                ((bool[]) buf[0])[0] = rslt.getBool(1);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                ((bool[]) buf[2])[0] = rslt.getBool(3);
                ((short[]) buf[3])[0] = rslt.getShort(4);
                ((string[]) buf[4])[0] = rslt.getVarchar(5);
                ((string[]) buf[5])[0] = rslt.getVarchar(6);
                return;
       }
    }

 }

}
