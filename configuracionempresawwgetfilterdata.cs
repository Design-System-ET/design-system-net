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
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   public class configuracionempresawwgetfilterdata : GXProcedure
   {
      protected override bool IntegratedSecurityEnabled
      {
         get {
            return true ;
         }

      }

      protected override GAMSecurityLevel IntegratedSecurityLevel
      {
         get {
            return GAMSecurityLevel.SecurityHigh ;
         }

      }

      protected override string ExecutePermissionPrefix
      {
         get {
            return "configuracionempresaww_Services_Execute" ;
         }

      }

      public configuracionempresawwgetfilterdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public configuracionempresawwgetfilterdata( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_DDOName ,
                           string aP1_SearchTxtParms ,
                           string aP2_SearchTxtTo ,
                           out string aP3_OptionsJson ,
                           out string aP4_OptionsDescJson ,
                           out string aP5_OptionIndexesJson )
      {
         this.AV35DDOName = aP0_DDOName;
         this.AV36SearchTxtParms = aP1_SearchTxtParms;
         this.AV37SearchTxtTo = aP2_SearchTxtTo;
         this.AV38OptionsJson = "" ;
         this.AV39OptionsDescJson = "" ;
         this.AV40OptionIndexesJson = "" ;
         initialize();
         ExecuteImpl();
         aP3_OptionsJson=this.AV38OptionsJson;
         aP4_OptionsDescJson=this.AV39OptionsDescJson;
         aP5_OptionIndexesJson=this.AV40OptionIndexesJson;
      }

      public string executeUdp( string aP0_DDOName ,
                                string aP1_SearchTxtParms ,
                                string aP2_SearchTxtTo ,
                                out string aP3_OptionsJson ,
                                out string aP4_OptionsDescJson )
      {
         execute(aP0_DDOName, aP1_SearchTxtParms, aP2_SearchTxtTo, out aP3_OptionsJson, out aP4_OptionsDescJson, out aP5_OptionIndexesJson);
         return AV40OptionIndexesJson ;
      }

      public void executeSubmit( string aP0_DDOName ,
                                 string aP1_SearchTxtParms ,
                                 string aP2_SearchTxtTo ,
                                 out string aP3_OptionsJson ,
                                 out string aP4_OptionsDescJson ,
                                 out string aP5_OptionIndexesJson )
      {
         this.AV35DDOName = aP0_DDOName;
         this.AV36SearchTxtParms = aP1_SearchTxtParms;
         this.AV37SearchTxtTo = aP2_SearchTxtTo;
         this.AV38OptionsJson = "" ;
         this.AV39OptionsDescJson = "" ;
         this.AV40OptionIndexesJson = "" ;
         SubmitImpl();
         aP3_OptionsJson=this.AV38OptionsJson;
         aP4_OptionsDescJson=this.AV39OptionsDescJson;
         aP5_OptionIndexesJson=this.AV40OptionIndexesJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV25Options = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV27OptionsDesc = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV28OptionIndexes = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV22MaxItems = 10;
         AV21PageIndex = (short)((String.IsNullOrEmpty(StringUtil.RTrim( AV36SearchTxtParms)) ? 0 : (long)(Math.Round(NumberUtil.Val( StringUtil.Substring( AV36SearchTxtParms, 1, 2), "."), 18, MidpointRounding.ToEven))));
         AV19SearchTxt = (String.IsNullOrEmpty(StringUtil.RTrim( AV36SearchTxtParms)) ? "" : StringUtil.Substring( AV36SearchTxtParms, 3, -1));
         AV20SkipItems = (short)(AV21PageIndex*AV22MaxItems);
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(StringUtil.Upper( AV35DDOName), "DDO_CONFIGURACIONEMPRESATELEFONO") == 0 )
         {
            /* Execute user subroutine: 'LOADCONFIGURACIONEMPRESATELEFONOOPTIONS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV38OptionsJson = AV25Options.ToJSonString(false);
         AV39OptionsDescJson = AV27OptionsDesc.ToJSonString(false);
         AV40OptionIndexesJson = AV28OptionIndexes.ToJSonString(false);
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV30Session.Get("ConfiguracionEmpresaWWGridState"), "") == 0 )
         {
            AV32GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "ConfiguracionEmpresaWWGridState"), null, "", "");
         }
         else
         {
            AV32GridState.FromXml(AV30Session.Get("ConfiguracionEmpresaWWGridState"), null, "", "");
         }
         AV60GXV1 = 1;
         while ( AV60GXV1 <= AV32GridState.gxTpr_Filtervalues.Count )
         {
            AV33GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV32GridState.gxTpr_Filtervalues.Item(AV60GXV1));
            if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV41FilterFullText = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESAID") == 0 )
            {
               AV11TFConfiguracionEmpresaId = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV12TFConfiguracionEmpresaId_To = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO") == 0 )
            {
               AV13TFConfiguracionEmpresaTelefono = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESATELEFONO_SEL") == 0 )
            {
               AV14TFConfiguracionEmpresaTelefono_Sel = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANBASICO") == 0 )
            {
               AV15TFConfiguracionEmpresaCostoPlanBasico = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV16TFConfiguracionEmpresaCostoPlanBasico_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANBASICO") == 0 )
            {
               AV17TFConfiguracionEmpresaCuotaPlanBasico = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV18TFConfiguracionEmpresaCuotaPlanBasico_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANSUPERIOR") == 0 )
            {
               AV42TFConfiguracionEmpresaCostoPlanSuperior = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV43TFConfiguracionEmpresaCostoPlanSuperior_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANSUPERIOR") == 0 )
            {
               AV44TFConfiguracionEmpresaCuotaPlanSuperior = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV45TFConfiguracionEmpresaCuotaPlanSuperior_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOPLANNEGOCIOS") == 0 )
            {
               AV46TFConfiguracionEmpresaCostoPlanNegocios = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV47TFConfiguracionEmpresaCostoPlanNegocios_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTAPLANNEGOCIOS") == 0 )
            {
               AV48TFConfiguracionEmpresaCuotaPlanNegocios = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV49TFConfiguracionEmpresaCuotaPlanNegocios_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACOSTOLANDINGPAGE") == 0 )
            {
               AV50TFConfiguracionEmpresaCostoLandingPage = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV51TFConfiguracionEmpresaCostoLandingPage_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFCONFIGURACIONEMPRESACUOTALANDINGPAGE") == 0 )
            {
               AV52TFConfiguracionEmpresaCuotaLandingPage = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, ".");
               AV53TFConfiguracionEmpresaCuotaLandingPage_To = NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, ".");
            }
            AV60GXV1 = (int)(AV60GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'LOADCONFIGURACIONEMPRESATELEFONOOPTIONS' Routine */
         returnInSub = false;
         AV13TFConfiguracionEmpresaTelefono = AV19SearchTxt;
         AV14TFConfiguracionEmpresaTelefono_Sel = "";
         AV62Configuracionempresawwds_1_filterfulltext = AV41FilterFullText;
         AV63Configuracionempresawwds_2_tfconfiguracionempresaid = AV11TFConfiguracionEmpresaId;
         AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to = AV12TFConfiguracionEmpresaId_To;
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = AV13TFConfiguracionEmpresaTelefono;
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = AV14TFConfiguracionEmpresaTelefono_Sel;
         AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico = AV15TFConfiguracionEmpresaCostoPlanBasico;
         AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to = AV16TFConfiguracionEmpresaCostoPlanBasico_To;
         AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico = AV17TFConfiguracionEmpresaCuotaPlanBasico;
         AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to = AV18TFConfiguracionEmpresaCuotaPlanBasico_To;
         AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior = AV42TFConfiguracionEmpresaCostoPlanSuperior;
         AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to = AV43TFConfiguracionEmpresaCostoPlanSuperior_To;
         AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior = AV44TFConfiguracionEmpresaCuotaPlanSuperior;
         AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to = AV45TFConfiguracionEmpresaCuotaPlanSuperior_To;
         AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios = AV46TFConfiguracionEmpresaCostoPlanNegocios;
         AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to = AV47TFConfiguracionEmpresaCostoPlanNegocios_To;
         AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios = AV48TFConfiguracionEmpresaCuotaPlanNegocios;
         AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to = AV49TFConfiguracionEmpresaCuotaPlanNegocios_To;
         AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage = AV50TFConfiguracionEmpresaCostoLandingPage;
         AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to = AV51TFConfiguracionEmpresaCostoLandingPage_To;
         AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage = AV52TFConfiguracionEmpresaCuotaLandingPage;
         AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to = AV53TFConfiguracionEmpresaCuotaLandingPage_To;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV62Configuracionempresawwds_1_filterfulltext ,
                                              AV63Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                              AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                              AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                              AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                              AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                              AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                              AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                              AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                              AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                              AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                              AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                              AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                              AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                              AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                              AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                              AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                              AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                              AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                              AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                              AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
                                              A44ConfiguracionEmpresaId ,
                                              A45ConfiguracionEmpresaTelefono ,
                                              A46ConfiguracionEmpresaCostoPlanB ,
                                              A47ConfiguracionEmpresaCuotaPlanB ,
                                              A48ConfiguracionEmpresaCostoPlanS ,
                                              A49ConfiguracionEmpresaCuotaPlanS ,
                                              A50ConfiguracionEmpresaCostoPlanN ,
                                              A51ConfiguracionEmpresaCuotaPlanN ,
                                              A54ConfiguracionEmpresaCostoLandi ,
                                              A55ConfiguracionEmpresaCuotaLandi } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.SHORT, TypeConstants.DECIMAL,
                                              TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL, TypeConstants.DECIMAL
                                              }
         });
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV62Configuracionempresawwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext), "%", "");
         lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = StringUtil.PadR( StringUtil.RTrim( AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono), 20, "%");
         /* Using cursor P003B2 */
         pr_default.execute(0, new Object[] {lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, lV62Configuracionempresawwds_1_filterfulltext, AV63Configuracionempresawwds_2_tfconfiguracionempresaid, AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to, lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono, AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico, AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to, AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico, AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to, AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior, AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to, AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior, AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to, AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios, AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to, AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios, AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to, AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage, AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to, AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage, AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            BRK3B2 = false;
            A45ConfiguracionEmpresaTelefono = P003B2_A45ConfiguracionEmpresaTelefono[0];
            A55ConfiguracionEmpresaCuotaLandi = P003B2_A55ConfiguracionEmpresaCuotaLandi[0];
            A54ConfiguracionEmpresaCostoLandi = P003B2_A54ConfiguracionEmpresaCostoLandi[0];
            A51ConfiguracionEmpresaCuotaPlanN = P003B2_A51ConfiguracionEmpresaCuotaPlanN[0];
            A50ConfiguracionEmpresaCostoPlanN = P003B2_A50ConfiguracionEmpresaCostoPlanN[0];
            A49ConfiguracionEmpresaCuotaPlanS = P003B2_A49ConfiguracionEmpresaCuotaPlanS[0];
            A48ConfiguracionEmpresaCostoPlanS = P003B2_A48ConfiguracionEmpresaCostoPlanS[0];
            A47ConfiguracionEmpresaCuotaPlanB = P003B2_A47ConfiguracionEmpresaCuotaPlanB[0];
            A46ConfiguracionEmpresaCostoPlanB = P003B2_A46ConfiguracionEmpresaCostoPlanB[0];
            A44ConfiguracionEmpresaId = P003B2_A44ConfiguracionEmpresaId[0];
            AV29count = 0;
            while ( (pr_default.getStatus(0) != 101) && ( StringUtil.StrCmp(P003B2_A45ConfiguracionEmpresaTelefono[0], A45ConfiguracionEmpresaTelefono) == 0 ) )
            {
               BRK3B2 = false;
               A44ConfiguracionEmpresaId = P003B2_A44ConfiguracionEmpresaId[0];
               AV29count = (long)(AV29count+1);
               BRK3B2 = true;
               pr_default.readNext(0);
            }
            if ( (0==AV20SkipItems) )
            {
               AV24Option = (String.IsNullOrEmpty(StringUtil.RTrim( A45ConfiguracionEmpresaTelefono)) ? "<#Empty#>" : A45ConfiguracionEmpresaTelefono);
               AV25Options.Add(AV24Option, 0);
               AV28OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV29count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV25Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV20SkipItems = (short)(AV20SkipItems-1);
            }
            if ( ! BRK3B2 )
            {
               BRK3B2 = true;
               pr_default.readNext(0);
            }
         }
         pr_default.close(0);
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
         AV38OptionsJson = "";
         AV39OptionsDescJson = "";
         AV40OptionIndexesJson = "";
         AV25Options = new GxSimpleCollection<string>();
         AV27OptionsDesc = new GxSimpleCollection<string>();
         AV28OptionIndexes = new GxSimpleCollection<string>();
         AV19SearchTxt = "";
         AV9WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV30Session = context.GetSession();
         AV32GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV33GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV41FilterFullText = "";
         AV11TFConfiguracionEmpresaId = 0;
         AV12TFConfiguracionEmpresaId_To = 0;
         AV13TFConfiguracionEmpresaTelefono = "";
         AV14TFConfiguracionEmpresaTelefono_Sel = "";
         AV62Configuracionempresawwds_1_filterfulltext = "";
         AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel = "";
         lV62Configuracionempresawwds_1_filterfulltext = "";
         lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono = "";
         A45ConfiguracionEmpresaTelefono = "";
         P003B2_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         P003B2_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         P003B2_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         P003B2_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         P003B2_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         P003B2_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         P003B2_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         P003B2_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         P003B2_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         P003B2_A44ConfiguracionEmpresaId = new short[1] ;
         AV24Option = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresawwgetfilterdata__default(),
            new Object[][] {
                new Object[] {
               P003B2_A45ConfiguracionEmpresaTelefono, P003B2_A55ConfiguracionEmpresaCuotaLandi, P003B2_A54ConfiguracionEmpresaCostoLandi, P003B2_A51ConfiguracionEmpresaCuotaPlanN, P003B2_A50ConfiguracionEmpresaCostoPlanN, P003B2_A49ConfiguracionEmpresaCuotaPlanS, P003B2_A48ConfiguracionEmpresaCostoPlanS, P003B2_A47ConfiguracionEmpresaCuotaPlanB, P003B2_A46ConfiguracionEmpresaCostoPlanB, P003B2_A44ConfiguracionEmpresaId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV22MaxItems ;
      private short AV21PageIndex ;
      private short AV20SkipItems ;
      private short AV11TFConfiguracionEmpresaId ;
      private short AV12TFConfiguracionEmpresaId_To ;
      private short AV63Configuracionempresawwds_2_tfconfiguracionempresaid ;
      private short AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ;
      private short A44ConfiguracionEmpresaId ;
      private int AV60GXV1 ;
      private long AV29count ;
      private decimal AV15TFConfiguracionEmpresaCostoPlanBasico ;
      private decimal AV16TFConfiguracionEmpresaCostoPlanBasico_To ;
      private decimal AV17TFConfiguracionEmpresaCuotaPlanBasico ;
      private decimal AV18TFConfiguracionEmpresaCuotaPlanBasico_To ;
      private decimal AV42TFConfiguracionEmpresaCostoPlanSuperior ;
      private decimal AV43TFConfiguracionEmpresaCostoPlanSuperior_To ;
      private decimal AV44TFConfiguracionEmpresaCuotaPlanSuperior ;
      private decimal AV45TFConfiguracionEmpresaCuotaPlanSuperior_To ;
      private decimal AV46TFConfiguracionEmpresaCostoPlanNegocios ;
      private decimal AV47TFConfiguracionEmpresaCostoPlanNegocios_To ;
      private decimal AV48TFConfiguracionEmpresaCuotaPlanNegocios ;
      private decimal AV49TFConfiguracionEmpresaCuotaPlanNegocios_To ;
      private decimal AV50TFConfiguracionEmpresaCostoLandingPage ;
      private decimal AV51TFConfiguracionEmpresaCostoLandingPage_To ;
      private decimal AV52TFConfiguracionEmpresaCuotaLandingPage ;
      private decimal AV53TFConfiguracionEmpresaCuotaLandingPage_To ;
      private decimal AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ;
      private decimal AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ;
      private decimal AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ;
      private decimal AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ;
      private decimal AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ;
      private decimal AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ;
      private decimal AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ;
      private decimal AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ;
      private decimal AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ;
      private decimal AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ;
      private decimal AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ;
      private decimal AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ;
      private decimal AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ;
      private decimal AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ;
      private decimal AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ;
      private decimal AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ;
      private decimal A46ConfiguracionEmpresaCostoPlanB ;
      private decimal A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal A48ConfiguracionEmpresaCostoPlanS ;
      private decimal A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal A50ConfiguracionEmpresaCostoPlanN ;
      private decimal A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal A54ConfiguracionEmpresaCostoLandi ;
      private decimal A55ConfiguracionEmpresaCuotaLandi ;
      private string AV13TFConfiguracionEmpresaTelefono ;
      private string AV14TFConfiguracionEmpresaTelefono_Sel ;
      private string AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ;
      private string lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ;
      private string A45ConfiguracionEmpresaTelefono ;
      private bool returnInSub ;
      private bool BRK3B2 ;
      private string AV38OptionsJson ;
      private string AV39OptionsDescJson ;
      private string AV40OptionIndexesJson ;
      private string AV35DDOName ;
      private string AV36SearchTxtParms ;
      private string AV37SearchTxtTo ;
      private string AV19SearchTxt ;
      private string AV41FilterFullText ;
      private string AV62Configuracionempresawwds_1_filterfulltext ;
      private string lV62Configuracionempresawwds_1_filterfulltext ;
      private string AV24Option ;
      private IGxSession AV30Session ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GxSimpleCollection<string> AV25Options ;
      private GxSimpleCollection<string> AV27OptionsDesc ;
      private GxSimpleCollection<string> AV28OptionIndexes ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV32GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV33GridStateFilterValue ;
      private IDataStoreProvider pr_default ;
      private string[] P003B2_A45ConfiguracionEmpresaTelefono ;
      private decimal[] P003B2_A55ConfiguracionEmpresaCuotaLandi ;
      private decimal[] P003B2_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] P003B2_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] P003B2_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] P003B2_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] P003B2_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] P003B2_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] P003B2_A46ConfiguracionEmpresaCostoPlanB ;
      private short[] P003B2_A44ConfiguracionEmpresaId ;
      private string aP3_OptionsJson ;
      private string aP4_OptionsDescJson ;
      private string aP5_OptionIndexesJson ;
   }

   public class configuracionempresawwgetfilterdata__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P003B2( IGxContext context ,
                                             string AV62Configuracionempresawwds_1_filterfulltext ,
                                             short AV63Configuracionempresawwds_2_tfconfiguracionempresaid ,
                                             short AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to ,
                                             string AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel ,
                                             string AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono ,
                                             decimal AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico ,
                                             decimal AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to ,
                                             decimal AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico ,
                                             decimal AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to ,
                                             decimal AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior ,
                                             decimal AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to ,
                                             decimal AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior ,
                                             decimal AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to ,
                                             decimal AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios ,
                                             decimal AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to ,
                                             decimal AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios ,
                                             decimal AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to ,
                                             decimal AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage ,
                                             decimal AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to ,
                                             decimal AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage ,
                                             decimal AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to ,
                                             short A44ConfiguracionEmpresaId ,
                                             string A45ConfiguracionEmpresaTelefono ,
                                             decimal A46ConfiguracionEmpresaCostoPlanB ,
                                             decimal A47ConfiguracionEmpresaCuotaPlanB ,
                                             decimal A48ConfiguracionEmpresaCostoPlanS ,
                                             decimal A49ConfiguracionEmpresaCuotaPlanS ,
                                             decimal A50ConfiguracionEmpresaCostoPlanN ,
                                             decimal A51ConfiguracionEmpresaCuotaPlanN ,
                                             decimal A54ConfiguracionEmpresaCostoLandi ,
                                             decimal A55ConfiguracionEmpresaCuotaLandi )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[30];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT `ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaCuotaLandi`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaId` FROM `ConfiguracionEmpresa`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Configuracionempresawwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( `ConfiguracionEmpresaTelefono` like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanB`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanS`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaPlanN`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCostoLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`ConfiguracionEmpresaCuotaLandi`,2), ',', ''), 12, ' ')) like CONCAT('%', @lV62Configuracionempresawwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int1[0] = 1;
            GXv_int1[1] = 1;
            GXv_int1[2] = 1;
            GXv_int1[3] = 1;
            GXv_int1[4] = 1;
            GXv_int1[5] = 1;
            GXv_int1[6] = 1;
            GXv_int1[7] = 1;
            GXv_int1[8] = 1;
            GXv_int1[9] = 1;
         }
         if ( ! (0==AV63Configuracionempresawwds_2_tfconfiguracionempresaid) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` >= @AV63Configuracionempresawwds_2_tfconfiguracionempresaid)");
         }
         else
         {
            GXv_int1[10] = 1;
         }
         if ( ! (0==AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaId` <= @AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to)");
         }
         else
         {
            GXv_int1[11] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV65Configuracionempresawwds_4_tfconfiguracionempresatelefono)) ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` like @lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono)");
         }
         else
         {
            GXv_int1[12] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel)) && ! ( StringUtil.StrCmp(AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaTelefono` = @AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_se)");
         }
         else
         {
            GXv_int1[13] = 1;
         }
         if ( StringUtil.StrCmp(AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`ConfiguracionEmpresaTelefono`))=0))");
         }
         if ( ! (Convert.ToDecimal(0)==AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` >= @AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int1[14] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanB` <= @AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanba)");
         }
         else
         {
            GXv_int1[15] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanbasico) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` >= @AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int1[16] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanbasico_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanB` <= @AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba)");
         }
         else
         {
            GXv_int1[17] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` >= @AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int1[18] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanS` <= @AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplans)");
         }
         else
         {
            GXv_int1[19] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplansuperior) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` >= @AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int1[20] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplansuperior_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanS` <= @AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplans)");
         }
         else
         {
            GXv_int1[21] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` >= @AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int1[22] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoPlanN` <= @AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplann)");
         }
         else
         {
            GXv_int1[23] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplannegocios) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` >= @AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int1[24] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplannegocios_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaPlanN` <= @AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplann)");
         }
         else
         {
            GXv_int1[25] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` >= @AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int1[26] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCostoLandi` <= @AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandi)");
         }
         else
         {
            GXv_int1[27] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandingpage) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` >= @AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int1[28] = 1;
         }
         if ( ! (Convert.ToDecimal(0)==AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandingpage_to) )
         {
            AddWhere(sWhereString, "(`ConfiguracionEmpresaCuotaLandi` <= @AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandi)");
         }
         else
         {
            GXv_int1[29] = 1;
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY `ConfiguracionEmpresaTelefono`";
         GXv_Object2[0] = scmdbuf;
         GXv_Object2[1] = GXv_int1;
         return GXv_Object2 ;
      }

      public override Object [] getDynamicStatement( int cursor ,
                                                     IGxContext context ,
                                                     Object [] dynConstraints )
      {
         switch ( cursor )
         {
               case 0 :
                     return conditional_P003B2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (decimal)dynConstraints[5] , (decimal)dynConstraints[6] , (decimal)dynConstraints[7] , (decimal)dynConstraints[8] , (decimal)dynConstraints[9] , (decimal)dynConstraints[10] , (decimal)dynConstraints[11] , (decimal)dynConstraints[12] , (decimal)dynConstraints[13] , (decimal)dynConstraints[14] , (decimal)dynConstraints[15] , (decimal)dynConstraints[16] , (decimal)dynConstraints[17] , (decimal)dynConstraints[18] , (decimal)dynConstraints[19] , (decimal)dynConstraints[20] , (short)dynConstraints[21] , (string)dynConstraints[22] , (decimal)dynConstraints[23] , (decimal)dynConstraints[24] , (decimal)dynConstraints[25] , (decimal)dynConstraints[26] , (decimal)dynConstraints[27] , (decimal)dynConstraints[28] , (decimal)dynConstraints[29] , (decimal)dynConstraints[30] );
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
          Object[] prmP003B2;
          prmP003B2 = new Object[] {
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV62Configuracionempresawwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV63Configuracionempresawwds_2_tfconfiguracionempresaid",GXType.Int16,4,0) ,
          new ParDef("@AV64Configuracionempresawwds_3_tfconfiguracionempresaid_to",GXType.Int16,4,0) ,
          new ParDef("@lV65Configuracionempresawwds_4_tfconfiguracionempresatelefono",GXType.Char,20,0) ,
          new ParDef("@AV66Configuracionempresawwds_5_tfconfiguracionempresatelefono_se",GXType.Char,20,0) ,
          new ParDef("@AV67Configuracionempresawwds_6_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV68Configuracionempresawwds_7_tfconfiguracionempresacostoplanba",GXType.Number,12,2) ,
          new ParDef("@AV69Configuracionempresawwds_8_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV70Configuracionempresawwds_9_tfconfiguracionempresacuotaplanba",GXType.Number,12,2) ,
          new ParDef("@AV71Configuracionempresawwds_10_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV72Configuracionempresawwds_11_tfconfiguracionempresacostoplans",GXType.Number,12,2) ,
          new ParDef("@AV73Configuracionempresawwds_12_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV74Configuracionempresawwds_13_tfconfiguracionempresacuotaplans",GXType.Number,12,2) ,
          new ParDef("@AV75Configuracionempresawwds_14_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV76Configuracionempresawwds_15_tfconfiguracionempresacostoplann",GXType.Number,12,2) ,
          new ParDef("@AV77Configuracionempresawwds_16_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV78Configuracionempresawwds_17_tfconfiguracionempresacuotaplann",GXType.Number,12,2) ,
          new ParDef("@AV79Configuracionempresawwds_18_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV80Configuracionempresawwds_19_tfconfiguracionempresacostolandi",GXType.Number,12,2) ,
          new ParDef("@AV81Configuracionempresawwds_20_tfconfiguracionempresacuotalandi",GXType.Number,12,2) ,
          new ParDef("@AV82Configuracionempresawwds_21_tfconfiguracionempresacuotalandi",GXType.Number,12,2)
          };
          def= new CursorDef[] {
              new CursorDef("P003B2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP003B2,100, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[0])[0] = rslt.getString(1, 20);
                ((decimal[]) buf[1])[0] = rslt.getDecimal(2);
                ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
                ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
                ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
                ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
                ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
                ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
                ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
                ((short[]) buf[9])[0] = rslt.getShort(10);
                return;
       }
    }

 }

}
