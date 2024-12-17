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
   public class newproductoswwgetfilterdata : GXProcedure
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
            return "newproductosww_Services_Execute" ;
         }

      }

      public newproductoswwgetfilterdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newproductoswwgetfilterdata( IGxContext context )
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
         this.AV37DDOName = aP0_DDOName;
         this.AV38SearchTxtParms = aP1_SearchTxtParms;
         this.AV39SearchTxtTo = aP2_SearchTxtTo;
         this.AV40OptionsJson = "" ;
         this.AV41OptionsDescJson = "" ;
         this.AV42OptionIndexesJson = "" ;
         initialize();
         ExecuteImpl();
         aP3_OptionsJson=this.AV40OptionsJson;
         aP4_OptionsDescJson=this.AV41OptionsDescJson;
         aP5_OptionIndexesJson=this.AV42OptionIndexesJson;
      }

      public string executeUdp( string aP0_DDOName ,
                                string aP1_SearchTxtParms ,
                                string aP2_SearchTxtTo ,
                                out string aP3_OptionsJson ,
                                out string aP4_OptionsDescJson )
      {
         execute(aP0_DDOName, aP1_SearchTxtParms, aP2_SearchTxtTo, out aP3_OptionsJson, out aP4_OptionsDescJson, out aP5_OptionIndexesJson);
         return AV42OptionIndexesJson ;
      }

      public void executeSubmit( string aP0_DDOName ,
                                 string aP1_SearchTxtParms ,
                                 string aP2_SearchTxtTo ,
                                 out string aP3_OptionsJson ,
                                 out string aP4_OptionsDescJson ,
                                 out string aP5_OptionIndexesJson )
      {
         this.AV37DDOName = aP0_DDOName;
         this.AV38SearchTxtParms = aP1_SearchTxtParms;
         this.AV39SearchTxtTo = aP2_SearchTxtTo;
         this.AV40OptionsJson = "" ;
         this.AV41OptionsDescJson = "" ;
         this.AV42OptionIndexesJson = "" ;
         SubmitImpl();
         aP3_OptionsJson=this.AV40OptionsJson;
         aP4_OptionsDescJson=this.AV41OptionsDescJson;
         aP5_OptionIndexesJson=this.AV42OptionIndexesJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV27Options = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV29OptionsDesc = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV30OptionIndexes = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV24MaxItems = 10;
         AV23PageIndex = (short)((String.IsNullOrEmpty(StringUtil.RTrim( AV38SearchTxtParms)) ? 0 : (long)(Math.Round(NumberUtil.Val( StringUtil.Substring( AV38SearchTxtParms, 1, 2), "."), 18, MidpointRounding.ToEven))));
         AV21SearchTxt = (String.IsNullOrEmpty(StringUtil.RTrim( AV38SearchTxtParms)) ? "" : StringUtil.Substring( AV38SearchTxtParms, 3, -1));
         AV22SkipItems = (short)(AV23PageIndex*AV24MaxItems);
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(StringUtil.Upper( AV37DDOName), "DDO_NEWPRODUCTOSNOMBRE") == 0 )
         {
            /* Execute user subroutine: 'LOADNEWPRODUCTOSNOMBREOPTIONS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV40OptionsJson = AV27Options.ToJSonString(false);
         AV41OptionsDescJson = AV29OptionsDesc.ToJSonString(false);
         AV42OptionIndexesJson = AV30OptionIndexes.ToJSonString(false);
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV32Session.Get("NewProductosWWGridState"), "") == 0 )
         {
            AV34GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "NewProductosWWGridState"), null, "", "");
         }
         else
         {
            AV34GridState.FromXml(AV32Session.Get("NewProductosWWGridState"), null, "", "");
         }
         AV56GXV1 = 1;
         while ( AV56GXV1 <= AV34GridState.gxTpr_Filtervalues.Count )
         {
            AV35GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV34GridState.gxTpr_Filtervalues.Item(AV56GXV1));
            if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV43FilterFullText = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSID") == 0 )
            {
               AV48TFNewProductosId = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV49TFNewProductosId_To = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE") == 0 )
            {
               AV11TFNewProductosNombre = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNOMBRE_SEL") == 0 )
            {
               AV12TFNewProductosNombre_Sel = AV35GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMERODESCARGAS") == 0 )
            {
               AV15TFNewProductosNumeroDescargas = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV16TFNewProductosNumeroDescargas_To = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSNUMEROVENTAS") == 0 )
            {
               AV44TFNewProductosNumeroVentas = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV45TFNewProductosNumeroVentas_To = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV35GridStateFilterValue.gxTpr_Name, "TFNEWPRODUCTOSVISITAS") == 0 )
            {
               AV46TFNewProductosVisitas = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV47TFNewProductosVisitas_To = (short)(Math.Round(NumberUtil.Val( AV35GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            AV56GXV1 = (int)(AV56GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'LOADNEWPRODUCTOSNOMBREOPTIONS' Routine */
         returnInSub = false;
         AV11TFNewProductosNombre = AV21SearchTxt;
         AV12TFNewProductosNombre_Sel = "";
         AV58Newproductoswwds_1_filterfulltext = AV43FilterFullText;
         AV59Newproductoswwds_2_tfnewproductosid = AV48TFNewProductosId;
         AV60Newproductoswwds_3_tfnewproductosid_to = AV49TFNewProductosId_To;
         AV61Newproductoswwds_4_tfnewproductosnombre = AV11TFNewProductosNombre;
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = AV12TFNewProductosNombre_Sel;
         AV63Newproductoswwds_6_tfnewproductosnumerodescargas = AV15TFNewProductosNumeroDescargas;
         AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to = AV16TFNewProductosNumeroDescargas_To;
         AV65Newproductoswwds_8_tfnewproductosnumeroventas = AV44TFNewProductosNumeroVentas;
         AV66Newproductoswwds_9_tfnewproductosnumeroventas_to = AV45TFNewProductosNumeroVentas_To;
         AV67Newproductoswwds_10_tfnewproductosvisitas = AV46TFNewProductosVisitas;
         AV68Newproductoswwds_11_tfnewproductosvisitas_to = AV47TFNewProductosVisitas_To;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV58Newproductoswwds_1_filterfulltext ,
                                              AV59Newproductoswwds_2_tfnewproductosid ,
                                              AV60Newproductoswwds_3_tfnewproductosid_to ,
                                              AV62Newproductoswwds_5_tfnewproductosnombre_sel ,
                                              AV61Newproductoswwds_4_tfnewproductosnombre ,
                                              AV63Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                              AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                              AV65Newproductoswwds_8_tfnewproductosnumeroventas ,
                                              AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                              AV67Newproductoswwds_10_tfnewproductosvisitas ,
                                              AV68Newproductoswwds_11_tfnewproductosvisitas_to ,
                                              A34NewProductosId ,
                                              A36NewProductosNombre ,
                                              A39NewProductosNumeroDescargas ,
                                              A42NewProductosNumeroVentas ,
                                              A43NewProductosVisitas } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT,
                                              TypeConstants.SHORT, TypeConstants.SHORT
                                              }
         });
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV58Newproductoswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext), "%", "");
         lV61Newproductoswwds_4_tfnewproductosnombre = StringUtil.Concat( StringUtil.RTrim( AV61Newproductoswwds_4_tfnewproductosnombre), "%", "");
         /* Using cursor P002S2 */
         pr_default.execute(0, new Object[] {lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, lV58Newproductoswwds_1_filterfulltext, AV59Newproductoswwds_2_tfnewproductosid, AV60Newproductoswwds_3_tfnewproductosid_to, lV61Newproductoswwds_4_tfnewproductosnombre, AV62Newproductoswwds_5_tfnewproductosnombre_sel, AV63Newproductoswwds_6_tfnewproductosnumerodescargas, AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to, AV65Newproductoswwds_8_tfnewproductosnumeroventas, AV66Newproductoswwds_9_tfnewproductosnumeroventas_to, AV67Newproductoswwds_10_tfnewproductosvisitas, AV68Newproductoswwds_11_tfnewproductosvisitas_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            BRK2S2 = false;
            A36NewProductosNombre = P002S2_A36NewProductosNombre[0];
            A43NewProductosVisitas = P002S2_A43NewProductosVisitas[0];
            A42NewProductosNumeroVentas = P002S2_A42NewProductosNumeroVentas[0];
            A39NewProductosNumeroDescargas = P002S2_A39NewProductosNumeroDescargas[0];
            A34NewProductosId = P002S2_A34NewProductosId[0];
            AV31count = 0;
            while ( (pr_default.getStatus(0) != 101) && ( StringUtil.StrCmp(P002S2_A36NewProductosNombre[0], A36NewProductosNombre) == 0 ) )
            {
               BRK2S2 = false;
               A34NewProductosId = P002S2_A34NewProductosId[0];
               AV31count = (long)(AV31count+1);
               BRK2S2 = true;
               pr_default.readNext(0);
            }
            if ( (0==AV22SkipItems) )
            {
               AV26Option = (String.IsNullOrEmpty(StringUtil.RTrim( A36NewProductosNombre)) ? "<#Empty#>" : A36NewProductosNombre);
               AV27Options.Add(AV26Option, 0);
               AV30OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV31count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV27Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV22SkipItems = (short)(AV22SkipItems-1);
            }
            if ( ! BRK2S2 )
            {
               BRK2S2 = true;
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
         AV40OptionsJson = "";
         AV41OptionsDescJson = "";
         AV42OptionIndexesJson = "";
         AV27Options = new GxSimpleCollection<string>();
         AV29OptionsDesc = new GxSimpleCollection<string>();
         AV30OptionIndexes = new GxSimpleCollection<string>();
         AV21SearchTxt = "";
         AV9WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV32Session = context.GetSession();
         AV34GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV35GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV43FilterFullText = "";
         AV11TFNewProductosNombre = "";
         AV12TFNewProductosNombre_Sel = "";
         AV58Newproductoswwds_1_filterfulltext = "";
         AV61Newproductoswwds_4_tfnewproductosnombre = "";
         AV62Newproductoswwds_5_tfnewproductosnombre_sel = "";
         lV58Newproductoswwds_1_filterfulltext = "";
         lV61Newproductoswwds_4_tfnewproductosnombre = "";
         A36NewProductosNombre = "";
         P002S2_A36NewProductosNombre = new string[] {""} ;
         P002S2_A43NewProductosVisitas = new short[1] ;
         P002S2_A42NewProductosNumeroVentas = new short[1] ;
         P002S2_A39NewProductosNumeroDescargas = new short[1] ;
         P002S2_A34NewProductosId = new short[1] ;
         AV26Option = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newproductoswwgetfilterdata__default(),
            new Object[][] {
                new Object[] {
               P002S2_A36NewProductosNombre, P002S2_A43NewProductosVisitas, P002S2_A42NewProductosNumeroVentas, P002S2_A39NewProductosNumeroDescargas, P002S2_A34NewProductosId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV24MaxItems ;
      private short AV23PageIndex ;
      private short AV22SkipItems ;
      private short AV48TFNewProductosId ;
      private short AV49TFNewProductosId_To ;
      private short AV15TFNewProductosNumeroDescargas ;
      private short AV16TFNewProductosNumeroDescargas_To ;
      private short AV44TFNewProductosNumeroVentas ;
      private short AV45TFNewProductosNumeroVentas_To ;
      private short AV46TFNewProductosVisitas ;
      private short AV47TFNewProductosVisitas_To ;
      private short AV59Newproductoswwds_2_tfnewproductosid ;
      private short AV60Newproductoswwds_3_tfnewproductosid_to ;
      private short AV63Newproductoswwds_6_tfnewproductosnumerodescargas ;
      private short AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ;
      private short AV65Newproductoswwds_8_tfnewproductosnumeroventas ;
      private short AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ;
      private short AV67Newproductoswwds_10_tfnewproductosvisitas ;
      private short AV68Newproductoswwds_11_tfnewproductosvisitas_to ;
      private short A34NewProductosId ;
      private short A39NewProductosNumeroDescargas ;
      private short A42NewProductosNumeroVentas ;
      private short A43NewProductosVisitas ;
      private int AV56GXV1 ;
      private long AV31count ;
      private bool returnInSub ;
      private bool BRK2S2 ;
      private string AV40OptionsJson ;
      private string AV41OptionsDescJson ;
      private string AV42OptionIndexesJson ;
      private string AV37DDOName ;
      private string AV38SearchTxtParms ;
      private string AV39SearchTxtTo ;
      private string AV21SearchTxt ;
      private string AV43FilterFullText ;
      private string AV11TFNewProductosNombre ;
      private string AV12TFNewProductosNombre_Sel ;
      private string AV58Newproductoswwds_1_filterfulltext ;
      private string AV61Newproductoswwds_4_tfnewproductosnombre ;
      private string AV62Newproductoswwds_5_tfnewproductosnombre_sel ;
      private string lV58Newproductoswwds_1_filterfulltext ;
      private string lV61Newproductoswwds_4_tfnewproductosnombre ;
      private string A36NewProductosNombre ;
      private string AV26Option ;
      private IGxSession AV32Session ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GxSimpleCollection<string> AV27Options ;
      private GxSimpleCollection<string> AV29OptionsDesc ;
      private GxSimpleCollection<string> AV30OptionIndexes ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV34GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV35GridStateFilterValue ;
      private IDataStoreProvider pr_default ;
      private string[] P002S2_A36NewProductosNombre ;
      private short[] P002S2_A43NewProductosVisitas ;
      private short[] P002S2_A42NewProductosNumeroVentas ;
      private short[] P002S2_A39NewProductosNumeroDescargas ;
      private short[] P002S2_A34NewProductosId ;
      private string aP3_OptionsJson ;
      private string aP4_OptionsDescJson ;
      private string aP5_OptionIndexesJson ;
   }

   public class newproductoswwgetfilterdata__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002S2( IGxContext context ,
                                             string AV58Newproductoswwds_1_filterfulltext ,
                                             short AV59Newproductoswwds_2_tfnewproductosid ,
                                             short AV60Newproductoswwds_3_tfnewproductosid_to ,
                                             string AV62Newproductoswwds_5_tfnewproductosnombre_sel ,
                                             string AV61Newproductoswwds_4_tfnewproductosnombre ,
                                             short AV63Newproductoswwds_6_tfnewproductosnumerodescargas ,
                                             short AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to ,
                                             short AV65Newproductoswwds_8_tfnewproductosnumeroventas ,
                                             short AV66Newproductoswwds_9_tfnewproductosnumeroventas_to ,
                                             short AV67Newproductoswwds_10_tfnewproductosvisitas ,
                                             short AV68Newproductoswwds_11_tfnewproductosvisitas_to ,
                                             short A34NewProductosId ,
                                             string A36NewProductosNombre ,
                                             short A39NewProductosNumeroDescargas ,
                                             short A42NewProductosNumeroVentas ,
                                             short A43NewProductosVisitas )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[15];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT `NewProductosNombre`, `NewProductosVisitas`, `NewProductosNumeroVentas`, `NewProductosNumeroDescargas`, `NewProductosId` FROM `NewProductos`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV58Newproductoswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewProductosId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( `NewProductosNombre` like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroDescargas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosNumeroVentas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewProductosVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV58Newproductoswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int1[0] = 1;
            GXv_int1[1] = 1;
            GXv_int1[2] = 1;
            GXv_int1[3] = 1;
            GXv_int1[4] = 1;
         }
         if ( ! (0==AV59Newproductoswwds_2_tfnewproductosid) )
         {
            AddWhere(sWhereString, "(`NewProductosId` >= @AV59Newproductoswwds_2_tfnewproductosid)");
         }
         else
         {
            GXv_int1[5] = 1;
         }
         if ( ! (0==AV60Newproductoswwds_3_tfnewproductosid_to) )
         {
            AddWhere(sWhereString, "(`NewProductosId` <= @AV60Newproductoswwds_3_tfnewproductosid_to)");
         }
         else
         {
            GXv_int1[6] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Newproductoswwds_5_tfnewproductosnombre_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newproductoswwds_4_tfnewproductosnombre)) ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` like @lV61Newproductoswwds_4_tfnewproductosnombre)");
         }
         else
         {
            GXv_int1[7] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newproductoswwds_5_tfnewproductosnombre_sel)) && ! ( StringUtil.StrCmp(AV62Newproductoswwds_5_tfnewproductosnombre_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewProductosNombre` = @AV62Newproductoswwds_5_tfnewproductosnombre_sel)");
         }
         else
         {
            GXv_int1[8] = 1;
         }
         if ( StringUtil.StrCmp(AV62Newproductoswwds_5_tfnewproductosnombre_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewProductosNombre`))=0))");
         }
         if ( ! (0==AV63Newproductoswwds_6_tfnewproductosnumerodescargas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` >= @AV63Newproductoswwds_6_tfnewproductosnumerodescargas)");
         }
         else
         {
            GXv_int1[9] = 1;
         }
         if ( ! (0==AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroDescargas` <= @AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to)");
         }
         else
         {
            GXv_int1[10] = 1;
         }
         if ( ! (0==AV65Newproductoswwds_8_tfnewproductosnumeroventas) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` >= @AV65Newproductoswwds_8_tfnewproductosnumeroventas)");
         }
         else
         {
            GXv_int1[11] = 1;
         }
         if ( ! (0==AV66Newproductoswwds_9_tfnewproductosnumeroventas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosNumeroVentas` <= @AV66Newproductoswwds_9_tfnewproductosnumeroventas_to)");
         }
         else
         {
            GXv_int1[12] = 1;
         }
         if ( ! (0==AV67Newproductoswwds_10_tfnewproductosvisitas) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` >= @AV67Newproductoswwds_10_tfnewproductosvisitas)");
         }
         else
         {
            GXv_int1[13] = 1;
         }
         if ( ! (0==AV68Newproductoswwds_11_tfnewproductosvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewProductosVisitas` <= @AV68Newproductoswwds_11_tfnewproductosvisitas_to)");
         }
         else
         {
            GXv_int1[14] = 1;
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY `NewProductosNombre`";
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
                     return conditional_P002S2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (short)dynConstraints[5] , (short)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (short)dynConstraints[13] , (short)dynConstraints[14] , (short)dynConstraints[15] );
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
          Object[] prmP002S2;
          prmP002S2 = new Object[] {
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV58Newproductoswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV59Newproductoswwds_2_tfnewproductosid",GXType.Int16,4,0) ,
          new ParDef("@AV60Newproductoswwds_3_tfnewproductosid_to",GXType.Int16,4,0) ,
          new ParDef("@lV61Newproductoswwds_4_tfnewproductosnombre",GXType.Char,200,0) ,
          new ParDef("@AV62Newproductoswwds_5_tfnewproductosnombre_sel",GXType.Char,200,0) ,
          new ParDef("@AV63Newproductoswwds_6_tfnewproductosnumerodescargas",GXType.Int16,4,0) ,
          new ParDef("@AV64Newproductoswwds_7_tfnewproductosnumerodescargas_to",GXType.Int16,4,0) ,
          new ParDef("@AV65Newproductoswwds_8_tfnewproductosnumeroventas",GXType.Int16,4,0) ,
          new ParDef("@AV66Newproductoswwds_9_tfnewproductosnumeroventas_to",GXType.Int16,4,0) ,
          new ParDef("@AV67Newproductoswwds_10_tfnewproductosvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV68Newproductoswwds_11_tfnewproductosvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002S2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002S2,100, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[0])[0] = rslt.getVarchar(1);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                ((short[]) buf[3])[0] = rslt.getShort(4);
                ((short[]) buf[4])[0] = rslt.getShort(5);
                return;
       }
    }

 }

}
