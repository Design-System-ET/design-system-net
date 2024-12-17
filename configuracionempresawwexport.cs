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
   public class configuracionempresawwexport : GXProcedure
   {
      public configuracionempresawwexport( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public configuracionempresawwexport( IGxContext context )
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
         AV12Filename = GXt_char1 + "ConfiguracionEmpresaWWExport-" + StringUtil.Trim( StringUtil.Str( (decimal)(AV16Random), 8, 0)) + ".xlsx";
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
         if ( ! ( (0==AV35TFConfiguracionEmpresaId) && (0==AV36TFConfiguracionEmpresaId_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Id", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = AV35TFConfiguracionEmpresaId;
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = AV36TFConfiguracionEmpresaId_To;
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV38TFConfiguracionEmpresaTelefono_Sel)) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Telefono", "")) ;
            AV14CellRow = GXt_int2;
            GXt_char1 = "";
            new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  (String.IsNullOrEmpty(StringUtil.RTrim( AV38TFConfiguracionEmpresaTelefono_Sel)) ? context.GetMessage( "WWP_TitleFilter_EmptyOption", "") : AV38TFConfiguracionEmpresaTelefono_Sel), out  GXt_char1) ;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
         }
         else
         {
            if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( AV37TFConfiguracionEmpresaTelefono)) ) )
            {
               GXt_int2 = (short)(AV14CellRow);
               new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Telefono", "")) ;
               AV14CellRow = GXt_int2;
               GXt_char1 = "";
               new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  AV37TFConfiguracionEmpresaTelefono, out  GXt_char1) ;
               AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Text = GXt_char1;
            }
         }
         if ( ! ( (Convert.ToDecimal(0)==AV39TFConfiguracionEmpresaCostoPlanBasico) && (Convert.ToDecimal(0)==AV40TFConfiguracionEmpresaCostoPlanBasico_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Plan Basico", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV39TFConfiguracionEmpresaCostoPlanBasico);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV40TFConfiguracionEmpresaCostoPlanBasico_To);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV41TFConfiguracionEmpresaCuotaPlanBasico) && (Convert.ToDecimal(0)==AV42TFConfiguracionEmpresaCuotaPlanBasico_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Plan Basico", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV41TFConfiguracionEmpresaCuotaPlanBasico);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV42TFConfiguracionEmpresaCuotaPlanBasico_To);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV44TFConfiguracionEmpresaCostoPlanSuperior) && (Convert.ToDecimal(0)==AV45TFConfiguracionEmpresaCostoPlanSuperior_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Plan Superior", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV44TFConfiguracionEmpresaCostoPlanSuperior);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV45TFConfiguracionEmpresaCostoPlanSuperior_To);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV46TFConfiguracionEmpresaCuotaPlanSuperior) && (Convert.ToDecimal(0)==AV47TFConfiguracionEmpresaCuotaPlanSuperior_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Plan Superior", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV46TFConfiguracionEmpresaCuotaPlanSuperior);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV47TFConfiguracionEmpresaCuotaPlanSuperior_To);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV48TFConfiguracionEmpresaCostoPlanNegocios) && (Convert.ToDecimal(0)==AV49TFConfiguracionEmpresaCostoPlanNegocios_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Plan Negocios", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV48TFConfiguracionEmpresaCostoPlanNegocios);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV49TFConfiguracionEmpresaCostoPlanNegocios_To);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV50TFConfiguracionEmpresaCuotaPlanNegocios) && (Convert.ToDecimal(0)==AV51TFConfiguracionEmpresaCuotaPlanNegocios_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Plan Negocios", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV50TFConfiguracionEmpresaCuotaPlanNegocios);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV51TFConfiguracionEmpresaCuotaPlanNegocios_To);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV52TFConfiguracionEmpresaCostoLandingPage) && (Convert.ToDecimal(0)==AV53TFConfiguracionEmpresaCostoLandingPage_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Landing Page", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV52TFConfiguracionEmpresaCostoLandingPage);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV53TFConfiguracionEmpresaCostoLandingPage_To);
         }
         if ( ! ( (Convert.ToDecimal(0)==AV54TFConfiguracionEmpresaCuotaLandingPage) && (Convert.ToDecimal(0)==AV55TFConfiguracionEmpresaCuotaLandingPage_To) ) )
         {
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  true, ref  GXt_int2,  (short)(AV15FirstColumn),  context.GetMessage( "Landing Page", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+1, 1, 1).Number = (double)(AV54TFConfiguracionEmpresaCuotaLandingPage);
            GXt_int2 = (short)(AV14CellRow);
            new DesignSystem.Programs.wwpbaseobjects.wwp_exportwritefilter(context ).execute( ref  AV11ExcelDocument,  false, ref  GXt_int2,  (short)(AV15FirstColumn+2),  context.GetMessage( "WWP_TSTo", "")) ;
            AV14CellRow = GXt_int2;
            AV11ExcelDocument.get_Cells(AV14CellRow, AV15FirstColumn+3, 1, 1).Number = (double)(AV55TFConfiguracionEmpresaCuotaLandingPage_To);
         }
         AV14CellRow = (int)(AV14CellRow+2);
      }

      protected void S141( )
      {
         /* 'WRITECOLUMNTITLES' Routine */
         returnInSub = false;
         AV32VisibleColumnCount = 0;
         if ( StringUtil.StrCmp(AV20Session.Get("ConfiguracionEmpresaWWColumnsSelector"), "") != 0 )
         {
            AV27ColumnsSelectorXML = AV20Session.Get("ConfiguracionEmpresaWWColumnsSelector");
            AV24ColumnsSelector.FromXml(AV27ColumnsSelectorXML, null, "", "");
         }
         else
         {
            /* Execute user subroutine: 'INITIALIZECOLUMNSSELECTOR' */
            S151 ();
            if (returnInSub) return;
         }
         AV24ColumnsSelector.gxTpr_Columns.Sort("Order");
         AV65GXV1 = 1;
         while ( AV65GXV1 <= AV24ColumnsSelector.gxTpr_Columns.Count )
         {
            AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV65GXV1));
            if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
            {
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = context.GetMessage( (String.IsNullOrEmpty(StringUtil.RTrim( AV26ColumnsSelector_Column.gxTpr_Displayname)) ? AV26ColumnsSelector_Column.gxTpr_Columnname : AV26ColumnsSelector_Column.gxTpr_Displayname), "");
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Bold = 1;
               AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Color = 11;
               AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
            }
            AV65GXV1 = (int)(AV65GXV1+1);
         }
      }

      protected void S161( )
      {
         /* 'WRITEDATA' Routine */
         returnInSub = false;
         AV67Configuracionempresawwds_1_filterfulltext = AV19FilterFullText;
         AV68Configuracionempresawwds_2_tfconfiguracionempresaid = AV35TFConfiguracionEmpresaId;
         AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV36TFConfiguracionEmpresaId_To;
         AV70Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV37TFConfiguracionEmpresaTelefono;
         AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV38TFConfiguracionEmpresaTelefono_Sel;
         AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV39TFConfiguracionEmpresaCostoPlanBasico;
         AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV40TFConfiguracionEmpresaCostoPlanBasico_To;
         AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV41TFConfiguracionEmpresaCuotaPlanBasico;
         AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV42TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV44TFConfiguracionEmpresaCostoPlanSuperior;
         AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV45TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV46TFConfiguracionEmpresaCuotaPlanSuperior;
         AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV47TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV48TFConfiguracionEmpresaCostoPlanNegocios;
         AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV49TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV50TFConfiguracionEmpresaCuotaPlanNegocios;
         AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV51TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV52TFConfiguracionEmpresaCostoLandingPage;
         AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV53TFConfiguracionEmpresaCostoLandingPage_To;
         AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV54TFConfiguracionEmpresaCuotaLandingPage;
         AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV55TFConfiguracionEmpresaCuotaLandingPage_To;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV67Configuracionempresawwds_1_filterfulltext ,
                                              AV68Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                              AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                              AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                              AV70Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                              AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                              AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                              AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                              AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                              AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                              AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                              AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                              AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                              AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                              AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                              AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                              AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                              AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                              AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                              AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                              AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
                                              A44ConfiguracionEmpresaId ,
                                              A45ConfiguracionEmpresaTelefono ,
                                              A46ConfiguracionEmpresaCostoPlanB ,
                                              A47ConfiguracionEmpresaCuotaPlanB ,
                                              A48ConfiguracionEmpresaCostoPlanS ,
                                              A49ConfiguracionEmpresaCuotaPlanS ,
                                              A50ConfiguracionEmpresaCostoPlanN ,
                                              A51ConfiguracionEmpresaCuotaPlanN ,
                                              A54ConfiguracionEmpresaCostoLandi ,
                                              A55ConfiguracionEmpresaCuotaLandi ,
                                              AV17OrderedBy ,
                                              AV18OrderedDsc } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.BOOLEAN
                                              }
         });
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV67Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext), "%", "");
         lV70Configuracionempresawwds_4_tfconfiguracionempresatelefono = StringUtil.PadR( StringUtil.RTrim( AV70Configuracionempresawwds_4_tfconfiguracionempresatelefono), 20, "%");
         /* Using cursor P003C2 */
         pr_default.execute(0, new Object[] {lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, lV67Configuracionempresawwds_1_filterfulltext, AV68Configuracionempresawwds_2_tfconfiguracionempresaid, AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to, lV70Configuracionempresawwds_4_tfconfiguracionempresatelefono, AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico, AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to, AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico, AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to, AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior, AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to, AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior, AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to, AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios, AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to, AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios, AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to, AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage, AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to, AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage, AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            A55ConfiguracionEmpresaCuotaLandi = P003C2_A55ConfiguracionEmpresaCuotaLandi[0];
            A54ConfiguracionEmpresaCostoLandi = P003C2_A54ConfiguracionEmpresaCostoLandi[0];
            A51ConfiguracionEmpresaCuotaPlanN = P003C2_A51ConfiguracionEmpresaCuotaPlanN[0];
            A50ConfiguracionEmpresaCostoPlanN = P003C2_A50ConfiguracionEmpresaCostoPlanN[0];
            A49ConfiguracionEmpresaCuotaPlanS = P003C2_A49ConfiguracionEmpresaCuotaPlanS[0];
            A48ConfiguracionEmpresaCostoPlanS = P003C2_A48ConfiguracionEmpresaCostoPlanS[0];
            A47ConfiguracionEmpresaCuotaPlanB = P003C2_A47ConfiguracionEmpresaCuotaPlanB[0];
            A46ConfiguracionEmpresaCostoPlanB = P003C2_A46ConfiguracionEmpresaCostoPlanB[0];
            A44ConfiguracionEmpresaId = P003C2_A44ConfiguracionEmpresaId[0];
            A45ConfiguracionEmpresaTelefono = P003C2_A45ConfiguracionEmpresaTelefono[0];
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
            AV88GXV2 = 1;
            while ( AV88GXV2 <= AV24ColumnsSelector.gxTpr_Columns.Count )
            {
               AV26ColumnsSelector_Column = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column)AV24ColumnsSelector.gxTpr_Columns.Item(AV88GXV2));
               if ( AV26ColumnsSelector_Column.gxTpr_Isvisible )
               {
                  if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaId") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = A44ConfiguracionEmpresaId;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaTelefono") == 0 )
                  {
                     GXt_char1 = "";
                     new DesignSystem.Programs.wwpbaseobjects.wwp_export_securetext(context ).execute(  A45ConfiguracionEmpresaTelefono, out  GXt_char1) ;
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Text = GXt_char1;
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCostoPlanBasico") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A46ConfiguracionEmpresaCostoPlanB);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCuotaPlanBasico") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A47ConfiguracionEmpresaCuotaPlanB);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCostoPlanSuperior") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A48ConfiguracionEmpresaCostoPlanS);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCuotaPlanSuperior") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A49ConfiguracionEmpresaCuotaPlanS);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCostoPlanNegocios") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A50ConfiguracionEmpresaCostoPlanN);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCuotaPlanNegocios") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A51ConfiguracionEmpresaCuotaPlanN);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCostoLandingPage") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A54ConfiguracionEmpresaCostoLandi);
                  }
                  else if ( StringUtil.StrCmp(AV26ColumnsSelector_Column.gxTpr_Columnname, "ConfiguracionEmpresaCuotaLandingPage") == 0 )
                  {
                     AV11ExcelDocument.get_Cells(AV14CellRow, (int)(AV15FirstColumn+AV32VisibleColumnCount), 1, 1).Number = (double)(A55ConfiguracionEmpresaCuotaLandi);
                  }
                  AV32VisibleColumnCount = (long)(AV32VisibleColumnCount+1);
               }
               AV88GXV2 = (int)(AV88GXV2+1);
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
         AV20Session.Set("WWPExportFileName", "ConfiguracionEmpresaWWExport.xlsx");
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
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaId",  "",  "Id",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaTelefono",  "",  "Telefono",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCostoPlanBasico",  "",  "Plan Basico",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCuotaPlanBasico",  "",  "Plan Basico",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCostoPlanSuperior",  "",  "Plan Superior",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCuotaPlanSuperior",  "",  "Plan Superior",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCostoPlanNegocios",  "",  "Plan Negocios",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCuotaPlanNegocios",  "",  "Plan Negocios",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCostoLandingPage",  "",  "Landing Page",  true,  "") ;
         new DesignSystem.Programs.wwpbaseobjects.wwp_columnsselector_add(context ).execute( ref  AV24ColumnsSelector,  "ConfiguracionEmpresaCuotaLandingPage",  "",  "Landing Page",  true,  "") ;
         GXt_char1 = AV28UserCustomValue;
         new DesignSystem.Programs.wwpbaseobjects.loadcolumnsselectorstate(context ).execute(  "ConfiguracionEmpresaWWColumnsSelector", out  GXt_char1) ;
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
         if ( StringUtil.StrCmp(AV20Session.Get("ConfiguracionEmpresaWWGridState"), "") == 0 )
         {
            AV22GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "ConfiguracionEmpresaWWGridState"), null, "", "");
         }
         else
         {
            AV22GridState.FromXml(AV20Session.Get("ConfiguracionEmpresaWWGridState"), null, "", "");
         }
         AV17OrderedBy = AV22GridState.gxTpr_Orderedby;
         AV18OrderedDsc = AV22GridState.gxTpr_Ordereddsc;
         AV89GXV3 = 1;
         while ( AV89GXV3 <= AV22GridState.gxTpr_Filtervalues.Count )
         {
            AV23GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV22GridState.gxTpr_Filtervalues.Item(AV89GXV3));
            if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV19FilterFullText = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESAID") == 0 )
            {
               AV35TFConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV36TFConfiguracionEmpresaId_To = (short)(Math.Round(NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO") == 0 )
            {
               AV37TFConfiguracionEmpresaTelefono = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO_SEL") == 0 )
            {
               AV38TFConfiguracionEmpresaTelefono_Sel = AV23GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANBASICO") == 0 )
            {
               AV39TFConfiguracionEmpresaCostoPlanBasico = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV40TFConfiguracionEmpresaCostoPlanBasico_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANBASICO") == 0 )
            {
               AV41TFConfiguracionEmpresaCuotaPlanBasico = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV42TFConfiguracionEmpresaCuotaPlanBasico_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR") == 0 )
            {
               AV44TFConfiguracionEmpresaCostoPlanSuperior = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV45TFConfiguracionEmpresaCostoPlanSuperior_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR") == 0 )
            {
               AV46TFConfiguracionEmpresaCuotaPlanSuperior = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV47TFConfiguracionEmpresaCuotaPlanSuperior_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS") == 0 )
            {
               AV48TFConfiguracionEmpresaCostoPlanNegocios = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV49TFConfiguracionEmpresaCostoPlanNegocios_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS") == 0 )
            {
               AV50TFConfiguracionEmpresaCuotaPlanNegocios = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV51TFConfiguracionEmpresaCuotaPlanNegocios_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOLANDINGPAGE") == 0 )
            {
               AV52TFConfiguracionEmpresaCostoLandingPage = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV53TFConfiguracionEmpresaCostoLandingPage_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV23GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTALANDINGPAGE") == 0 )
            {
               AV54TFConfiguracionEmpresaCuotaLandingPage = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Value, ".");
               AV55TFConfiguracionEmpresaCuotaLandingPage_To = NumberUtil.Val( AV23GridStateFilterValue.gxTpr_Valueto, ".");
            }
            AV89GXV3 = (int)(AV89GXV3+1);
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
         AV35TFConfiguracionEmpresaId = 0;
         AV36TFConfiguracionEmpresaId_To = 0;
         AV38TFConfiguracionEmpresaTelefono_Sel = "";
         AV37TFConfiguracionEmpresaTelefono = "";
         AV20Session = context.GetSession();
         AV27ColumnsSelectorXML = "";
         AV24ColumnsSelector = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV26ColumnsSelector_Column = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column(context);
         AV67Configuracionempresawwds_1_filterfulltext = "";
         AV70Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = "";
         lV67Configuracionempresawwds_1_filterfulltext = "";
         lV70Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         A45ConfiguracionEmpresaTelefono = "";
         P003C2_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         P003C2_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         P003C2_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         P003C2_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         P003C2_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         P003C2_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         P003C2_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         P003C2_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         P003C2_A44ConfiguracionEmpresaId = new short[1] ;
         P003C2_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         AV28UserCustomValue = "";
         GXt_char1 = "";
         AV25ColumnsSelectorAux = new DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector(context);
         AV22GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV23GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresawwexport__default(),
            new Object[][] {
                new Object[] {
               P003C2_A55ConfiguracionEmpresaCuotaLandi, P003C2_A54ConfiguracionEmpresaCostoLandi, P003C2_A51ConfiguracionEmpresaCuotaPlanN, P003C2_A50ConfiguracionEmpresaCostoPlanN, P003C2_A49ConfiguracionEmpresaCuotaPlanS, P003C2_A48ConfiguracionEmpresaCostoPlanS, P003C2_A47ConfiguracionEmpresaCuotaPlanB, P003C2_A46ConfiguracionEmpresaCostoPlanB, P003C2_A44ConfiguracionEmpresaId, P003C2_A45ConfiguracionEmpresaTelefono
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV35TFConfiguracionEmpresaId ;
      private short AV36TFConfiguracionEmpresaId_To ;
      private short GXt_int2 ;
      private short AV68Configuracionempresawwds_2_tfconfiguracionempresaid ;
      private short AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to ;
      private short A44ConfiguracionEmpresaId ;
      private short AV17OrderedBy ;
      private int AV14CellRow ;
      private int AV15FirstColumn ;
      private int AV16Random ;
      private int AV65GXV1 ;
      private int AV88GXV2 ;
      private int AV89GXV3 ;
      private long AV32VisibleColumnCount ;
      private decimal AV39TFConfiguracionEmpresaCostoPlanBasico ;
      private decimal AV40TFConfiguracionEmpresaCostoPlanBasico_To ;
      private decimal AV41TFConfiguracionEmpresaCuotaPlanBasico ;
      private decimal AV42TFConfiguracionEmpresaCuotaPlanBasico_To ;
      private decimal AV44TFConfiguracionEmpresaCostoPlanSuperior ;
      private decimal AV45TFConfiguracionEmpresaCostoPlanSuperior_To ;
      private decimal AV46TFConfiguracionEmpresaCuotaPlanSuperior ;
      private decimal AV47TFConfiguracionEmpresaCuotaPlanSuperior_To ;
      private decimal AV48TFConfiguracionEmpresaCostoPlanNegocios ;
      private decimal AV49TFConfiguracionEmpresaCostoPlanNegocios_To ;
      private decimal AV50TFConfiguracionEmpresaCuotaPlanNegocios ;
      private decimal AV51TFConfiguracionEmpresaCuotaPlanNegocios_To ;
      private decimal AV52TFConfiguracionEmpresaCostoLandingPage ;
      private decimal AV53TFConfiguracionEmpresaCostoLandingPage_To ;
      private decimal AV54TFConfiguracionEmpresaCuotaLandingPage ;
      private decimal AV55TFConfiguracionEmpresaCuotaLandingPage_To ;
      private decimal AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ;
      private decimal AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ;
      private decimal AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ;
      private decimal AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ;
      private decimal AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ;
      private decimal AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ;
      private decimal AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ;
      private decimal AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ;
      private decimal AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ;
      private decimal AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ;
      private decimal AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ;
      private decimal AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ;
      private decimal AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ;
      private decimal AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ;
      private decimal AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ;
      private decimal AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ;
      private decimal A46ConfiguracionEmpresaCostoPlanB ;
      private decimal A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal A48ConfiguracionEmpresaCostoPlanS ;
      private decimal A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal A50ConfiguracionEmpresaCostoPlanN ;
      private decimal A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal A54ConfiguracionEmpresaCostoLandi ;
      private decimal A55ConfiguracionEmpresaCuotaLandi ;
      private string AV38TFConfiguracionEmpresaTelefono_Sel ;
      private string AV37TFConfiguracionEmpresaTelefono ;
      private string AV70Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ;
      private string lV70Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string A45ConfiguracionEmpresaTelefono ;
      private string GXt_char1 ;
      private bool returnInSub ;
      private bool AV18OrderedDsc ;
      private string AV27ColumnsSelectorXML ;
      private string AV28UserCustomValue ;
      private string AV12Filename ;
      private string AV13ErrorMessage ;
      private string AV19FilterFullText ;
      private string AV67Configuracionempresawwds_1_filterfulltext ;
      private string lV67Configuracionempresawwds_1_filterfulltext ;
      private IGxSession AV20Session ;
      private ExcelDocumentI AV11ExcelDocument ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV24ColumnsSelector ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector_Column AV26ColumnsSelector_Column ;
      private IDataStoreProvider pr_default ;
      private decimal[] P003C2_A55ConfiguracionEmpresaCuotaLandi ;
      private decimal[] P003C2_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] P003C2_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] P003C2_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] P003C2_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] P003C2_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] P003C2_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] P003C2_A46ConfiguracionEmpresaCostoPlanB ;
      private short[] P003C2_A44ConfiguracionEmpresaId ;
      private string[] P003C2_A45ConfiguracionEmpresaTelefono ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPColumnsSelector AV25ColumnsSelectorAux ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV22GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV23GridStateFilterValue ;
      private string aP0_Filename ;
      private string aP1_ErrorMessage ;
   }

   public class configuracionempresawwexport__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P003C2( IGxContext context ,
                                             string AV67Configuracionempresawwds_1_filterfulltext ,
                                             short AV68Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                             short AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                             string AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                             string AV70Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                             decimal AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                             decimal AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                             decimal AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                             decimal AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                             decimal AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                             decimal AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                             decimal AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                             decimal AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                             decimal AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                             decimal AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                             decimal AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                             decimal AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                             decimal AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                             decimal AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                             decimal AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                             decimal AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
                                             short A44ConfiguracionEmpresaId ,
                                             string A45ConfiguracionEmpresaTelefono ,
                                             decimal A46ConfiguracionEmpresaCostoPlanB ,
                                             decimal A47ConfiguracionEmpresaCuotaPlanB ,
                                             decimal A48ConfiguracionEmpresaCostoPlanS ,
                                             decimal A49ConfiguracionEmpresaCuotaPlanS ,
                                             decimal A50ConfiguracionEmpresaCostoPlanN ,
                                             decimal A51ConfiguracionEmpresaCuotaPlanN ,
                                             decimal A54ConfiguracionEmpresaCostoLandi ,
                                             decimal A55ConfiguracionEmpresaCuotaLandi ,
                                             short AV17OrderedBy ,
                                             bool AV18OrderedDsc )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[30];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `ConfiguracionEmpresaCuotaLandi`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaId`, `ConfiguracionEmpresaTelefono` FROM `ConfiguracionEmpresa`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV67Configuracionempresawwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( `ConfiguracionEmpresaTelefono` like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV67Configuracionempresawwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
            GXv_int3[4] = 1;
            GXv_int3[5] = 1;
            GXv_int3[6] = 1;
            GXv_int3[7] = 1;
            GXv_int3[8] = 1;
            GXv_int3[9] = 1;
         }
         if ( ! (0==AV68Configuracionempresawwds_2_tfconfiguracionempresaid) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` >= @AV68Configuracionempresawwds_2_tfconfiguracionempresaid)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` <= @AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV70Configuracionempresawwds_4_tfconfiguracionempresatelefono)) ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` like @lV70Configuracionempresawwds_4_tfconfiguracionempresatelefono)");
         }
         else
         {
            GXv_int3[12] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ! ( StringUtil.StrCmp(AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` = @AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_se)");
         }
         else
         {
            GXv_int3[13] = 1;
         }
         if ( StringUtil.StrCmp(AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`ConfiguracionEmpresaTelefono`))=0))");
         }
         if ( ! (Convert.ToDecimal(0)==AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` >= @AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int3[14] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` <= @AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int3[15] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` >= @AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int3[16] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` <= @AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int3[17] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` >= @AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int3[18] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` <= @AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int3[19] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` >= @AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int3[20] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` <= @AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int3[21] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` >= @AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int3[22] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` <= @AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int3[23] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` >= @AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int3[24] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` <= @AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int3[25] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` >= @AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int3[26] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` <= @AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int3[27] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` >= @AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int3[28] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` <= @AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int3[29] = 1;
         }
         scmdbuf += sWhereString;
         if ( ( AV17OrderedBy == 1 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaTelefono`";
         }
         else if ( ( AV17OrderedBy == 1 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaTelefono` DESC";
         }
         else if ( ( AV17OrderedBy == 2 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaId`";
         }
         else if ( ( AV17OrderedBy == 2 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaId` DESC";
         }
         else if ( ( AV17OrderedBy == 3 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanB`";
         }
         else if ( ( AV17OrderedBy == 3 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanB` DESC";
         }
         else if ( ( AV17OrderedBy == 4 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanB`";
         }
         else if ( ( AV17OrderedBy == 4 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanB` DESC";
         }
         else if ( ( AV17OrderedBy == 5 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanS`";
         }
         else if ( ( AV17OrderedBy == 5 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanS` DESC";
         }
         else if ( ( AV17OrderedBy == 6 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanS`";
         }
         else if ( ( AV17OrderedBy == 6 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanS` DESC";
         }
         else if ( ( AV17OrderedBy == 7 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanN`";
         }
         else if ( ( AV17OrderedBy == 7 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoPlanN` DESC";
         }
         else if ( ( AV17OrderedBy == 8 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanN`";
         }
         else if ( ( AV17OrderedBy == 8 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaPlanN` DESC";
         }
         else if ( ( AV17OrderedBy == 9 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoLandi`";
         }
         else if ( ( AV17OrderedBy == 9 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCostoLandi` DESC";
         }
         else if ( ( AV17OrderedBy == 10 ) && ! AV18OrderedDsc )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaLandi`";
         }
         else if ( ( AV17OrderedBy == 10 ) && ( AV18OrderedDsc ) )
         {
            scmdbuf += " ORDER BY `ConfiguracionEmpresaCuotaLandi` DESC";
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
                     return conditional_P003C2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (decimal)dynConstraints[5] , (decimal)dynConstraints[6] , (decimal)dynConstraints[7] , (decimal)dynConstraints[8] , (decimal)dynConstraints[9] , (decimal)dynConstraints[10] , (decimal)dynConstraints[11] , (decimal)dynConstraints[12] , (decimal)dynConstraints[13] , (decimal)dynConstraints[14] , (decimal)dynConstraints[15] , (decimal)dynConstraints[16] , (decimal)dynConstraints[17] , (decimal)dynConstraints[18] , (decimal)dynConstraints[19] , (decimal)dynConstraints[20] , (short)dynConstraints[21] , (string)dynConstraints[22] , (decimal)dynConstraints[23] , (decimal)dynConstraints[24] , (decimal)dynConstraints[25] , (decimal)dynConstraints[26] , (decimal)dynConstraints[27] , (decimal)dynConstraints[28] , (decimal)dynConstraints[29] , (decimal)dynConstraints[30] , (short)dynConstraints[31] , (bool)dynConstraints[32] );
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
          Object[] prmP003C2;
          prmP003C2 = new Object[] {
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV67Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV68Configuracionempresawwds_2_tfconfiguracionempresaid",GXType.Int16,4,0) ,
          new ParDef("@AV69Configuracionempresawwds_3_tfconfiguracionempresaid_to",GXType.Int16,4,0) ,
          new ParDef("@lV70Configuracionempresawwds_4_tfconfiguracionempresatelefono",GXType.Char,20,0) ,
          new ParDef("@AV71Configuracionempresawwds_5_tfconfiguracionempresatelefono_se",GXType.Char,20,0) ,
          new ParDef("@AV72Configuracionempresawwds_6_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV73Configuracionempresawwds_7_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV74Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV75Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV76Configuracionempresawwds_10_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV77Configuracionempresawwds_11_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV78Configuracionempresawwds_12_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV79Configuracionempresawwds_13_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV80Configuracionempresawwds_14_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV81Configuracionempresawwds_15_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV82Configuracionempresawwds_16_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV83Configuracionempresawwds_17_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV84Configuracionempresawwds_18_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV85Configuracionempresawwds_19_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV86Configuracionempresawwds_20_tfconfiguracionempresacuotalandi",GXType.Number,12,2) ,
          new ParDef("@AV87Configuracionempresawwds_21_tfconfiguracionempresacuotalandi",GXType.Number,12,2)
          };
          def= new CursorDef[] {
              new CursorDef("P003C2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP003C2,100, GxCacheFrequency.OFF ,true,false )
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
                ((decimal[]) buf[0])[0] = rslt.getDecimal(1);
                ((decimal[]) buf[1])[0] = rslt.getDecimal(2);
                ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
                ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
                ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
                ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
                ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
                ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
                ((short[]) buf[8])[0] = rslt.getShort(9);
                ((string[]) buf[9])[0] = rslt.getString(10, 20);
                return;
       }
    }

 }

}
