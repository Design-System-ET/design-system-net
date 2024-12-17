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
   public class newblogwwgetfilterdata : GXProcedure
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
            return "newblogww_Services_Execute" ;
         }

      }

      public newblogwwgetfilterdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newblogwwgetfilterdata( IGxContext context )
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
         if ( StringUtil.StrCmp(StringUtil.Upper( AV35DDOName), "DDO_NEWBLOGTITULO") == 0 )
         {
            /* Execute user subroutine: 'LOADNEWBLOGTITULOOPTIONS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         else if ( StringUtil.StrCmp(StringUtil.Upper( AV35DDOName), "DDO_NEWBLOGSUBTITULO") == 0 )
         {
            /* Execute user subroutine: 'LOADNEWBLOGSUBTITULOOPTIONS' */
            S131 ();
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
         if ( StringUtil.StrCmp(AV30Session.Get("NewBlogWWGridState"), "") == 0 )
         {
            AV32GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "NewBlogWWGridState"), null, "", "");
         }
         else
         {
            AV32GridState.FromXml(AV30Session.Get("NewBlogWWGridState"), null, "", "");
         }
         AV54GXV1 = 1;
         while ( AV54GXV1 <= AV32GridState.gxTpr_Filtervalues.Count )
         {
            AV33GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV32GridState.gxTpr_Filtervalues.Item(AV54GXV1));
            if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV41FilterFullText = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGID") == 0 )
            {
               AV11TFNewBlogId = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV12TFNewBlogId_To = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO") == 0 )
            {
               AV13TFNewBlogTitulo = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGTITULO_SEL") == 0 )
            {
               AV14TFNewBlogTitulo_Sel = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO") == 0 )
            {
               AV15TFNewBlogSubTitulo = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGSUBTITULO_SEL") == 0 )
            {
               AV16TFNewBlogSubTitulo_Sel = AV33GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGDESTACADO_SEL") == 0 )
            {
               AV44TFNewBlogDestacado_Sel = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGVISITAS") == 0 )
            {
               AV45TFNewBlogVisitas = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
               AV46TFNewBlogVisitas_To = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Valueto, "."), 18, MidpointRounding.ToEven));
            }
            else if ( StringUtil.StrCmp(AV33GridStateFilterValue.gxTpr_Name, "TFNEWBLOGBORRADOR_SEL") == 0 )
            {
               AV47TFNewBlogBorrador_Sel = (short)(Math.Round(NumberUtil.Val( AV33GridStateFilterValue.gxTpr_Value, "."), 18, MidpointRounding.ToEven));
            }
            AV54GXV1 = (int)(AV54GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'LOADNEWBLOGTITULOOPTIONS' Routine */
         returnInSub = false;
         AV13TFNewBlogTitulo = AV19SearchTxt;
         AV14TFNewBlogTitulo_Sel = "";
         AV56Newblogwwds_1_filterfulltext = AV41FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV11TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV12TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV13TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV14TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV15TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV16TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV44TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV45TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV46TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV47TFNewBlogBorrador_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV56Newblogwwds_1_filterfulltext ,
                                              AV57Newblogwwds_2_tfnewblogid ,
                                              AV58Newblogwwds_3_tfnewblogid_to ,
                                              AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                              AV59Newblogwwds_4_tfnewblogtitulo ,
                                              AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                              AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                              AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                              AV64Newblogwwds_9_tfnewblogvisitas ,
                                              AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                              AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                              A12NewBlogId ,
                                              A14NewBlogTitulo ,
                                              A15NewBlogSubTitulo ,
                                              A18NewBlogVisitas ,
                                              A19NewBlogDestacado ,
                                              A25NewBlogBorrador } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.BOOLEAN
                                              }
         });
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV59Newblogwwds_4_tfnewblogtitulo = StringUtil.Concat( StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo), "%", "");
         lV61Newblogwwds_6_tfnewblogsubtitulo = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo), "%", "");
         /* Using cursor P002H2 */
         pr_default.execute(0, new Object[] {lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, AV57Newblogwwds_2_tfnewblogid, AV58Newblogwwds_3_tfnewblogid_to, lV59Newblogwwds_4_tfnewblogtitulo, AV60Newblogwwds_5_tfnewblogtitulo_sel, lV61Newblogwwds_6_tfnewblogsubtitulo, AV62Newblogwwds_7_tfnewblogsubtitulo_sel, AV64Newblogwwds_9_tfnewblogvisitas, AV65Newblogwwds_10_tfnewblogvisitas_to});
         while ( (pr_default.getStatus(0) != 101) )
         {
            BRK2H2 = false;
            A14NewBlogTitulo = P002H2_A14NewBlogTitulo[0];
            A25NewBlogBorrador = P002H2_A25NewBlogBorrador[0];
            A18NewBlogVisitas = P002H2_A18NewBlogVisitas[0];
            A19NewBlogDestacado = P002H2_A19NewBlogDestacado[0];
            A12NewBlogId = P002H2_A12NewBlogId[0];
            A15NewBlogSubTitulo = P002H2_A15NewBlogSubTitulo[0];
            AV29count = 0;
            while ( (pr_default.getStatus(0) != 101) && ( StringUtil.StrCmp(P002H2_A14NewBlogTitulo[0], A14NewBlogTitulo) == 0 ) )
            {
               BRK2H2 = false;
               A12NewBlogId = P002H2_A12NewBlogId[0];
               AV29count = (long)(AV29count+1);
               BRK2H2 = true;
               pr_default.readNext(0);
            }
            if ( (0==AV20SkipItems) )
            {
               AV24Option = (String.IsNullOrEmpty(StringUtil.RTrim( A14NewBlogTitulo)) ? "<#Empty#>" : A14NewBlogTitulo);
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
            if ( ! BRK2H2 )
            {
               BRK2H2 = true;
               pr_default.readNext(0);
            }
         }
         pr_default.close(0);
      }

      protected void S131( )
      {
         /* 'LOADNEWBLOGSUBTITULOOPTIONS' Routine */
         returnInSub = false;
         AV15TFNewBlogSubTitulo = AV19SearchTxt;
         AV16TFNewBlogSubTitulo_Sel = "";
         AV56Newblogwwds_1_filterfulltext = AV41FilterFullText;
         AV57Newblogwwds_2_tfnewblogid = AV11TFNewBlogId;
         AV58Newblogwwds_3_tfnewblogid_to = AV12TFNewBlogId_To;
         AV59Newblogwwds_4_tfnewblogtitulo = AV13TFNewBlogTitulo;
         AV60Newblogwwds_5_tfnewblogtitulo_sel = AV14TFNewBlogTitulo_Sel;
         AV61Newblogwwds_6_tfnewblogsubtitulo = AV15TFNewBlogSubTitulo;
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = AV16TFNewBlogSubTitulo_Sel;
         AV63Newblogwwds_8_tfnewblogdestacado_sel = AV44TFNewBlogDestacado_Sel;
         AV64Newblogwwds_9_tfnewblogvisitas = AV45TFNewBlogVisitas;
         AV65Newblogwwds_10_tfnewblogvisitas_to = AV46TFNewBlogVisitas_To;
         AV66Newblogwwds_11_tfnewblogborrador_sel = AV47TFNewBlogBorrador_Sel;
         pr_default.dynParam(1, new Object[]{ new Object[]{
                                              AV56Newblogwwds_1_filterfulltext ,
                                              AV57Newblogwwds_2_tfnewblogid ,
                                              AV58Newblogwwds_3_tfnewblogid_to ,
                                              AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                              AV59Newblogwwds_4_tfnewblogtitulo ,
                                              AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                              AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                              AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                              AV64Newblogwwds_9_tfnewblogvisitas ,
                                              AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                              AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                              A12NewBlogId ,
                                              A14NewBlogTitulo ,
                                              A15NewBlogSubTitulo ,
                                              A18NewBlogVisitas ,
                                              A19NewBlogDestacado ,
                                              A25NewBlogBorrador } ,
                                              new int[]{
                                              TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.SHORT, TypeConstants.BOOLEAN, TypeConstants.BOOLEAN
                                              }
         });
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV56Newblogwwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext), "%", "");
         lV59Newblogwwds_4_tfnewblogtitulo = StringUtil.Concat( StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo), "%", "");
         lV61Newblogwwds_6_tfnewblogsubtitulo = StringUtil.Concat( StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo), "%", "");
         /* Using cursor P002H3 */
         pr_default.execute(1, new Object[] {lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, lV56Newblogwwds_1_filterfulltext, AV57Newblogwwds_2_tfnewblogid, AV58Newblogwwds_3_tfnewblogid_to, lV59Newblogwwds_4_tfnewblogtitulo, AV60Newblogwwds_5_tfnewblogtitulo_sel, lV61Newblogwwds_6_tfnewblogsubtitulo, AV62Newblogwwds_7_tfnewblogsubtitulo_sel, AV64Newblogwwds_9_tfnewblogvisitas, AV65Newblogwwds_10_tfnewblogvisitas_to});
         while ( (pr_default.getStatus(1) != 101) )
         {
            BRK2H4 = false;
            A15NewBlogSubTitulo = P002H3_A15NewBlogSubTitulo[0];
            A25NewBlogBorrador = P002H3_A25NewBlogBorrador[0];
            A18NewBlogVisitas = P002H3_A18NewBlogVisitas[0];
            A19NewBlogDestacado = P002H3_A19NewBlogDestacado[0];
            A12NewBlogId = P002H3_A12NewBlogId[0];
            A14NewBlogTitulo = P002H3_A14NewBlogTitulo[0];
            AV29count = 0;
            while ( (pr_default.getStatus(1) != 101) && ( StringUtil.StrCmp(P002H3_A15NewBlogSubTitulo[0], A15NewBlogSubTitulo) == 0 ) )
            {
               BRK2H4 = false;
               A12NewBlogId = P002H3_A12NewBlogId[0];
               AV29count = (long)(AV29count+1);
               BRK2H4 = true;
               pr_default.readNext(1);
            }
            if ( (0==AV20SkipItems) )
            {
               AV24Option = (String.IsNullOrEmpty(StringUtil.RTrim( A15NewBlogSubTitulo)) ? "<#Empty#>" : A15NewBlogSubTitulo);
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
            if ( ! BRK2H4 )
            {
               BRK2H4 = true;
               pr_default.readNext(1);
            }
         }
         pr_default.close(1);
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
         AV13TFNewBlogTitulo = "";
         AV14TFNewBlogTitulo_Sel = "";
         AV15TFNewBlogSubTitulo = "";
         AV16TFNewBlogSubTitulo_Sel = "";
         AV56Newblogwwds_1_filterfulltext = "";
         AV59Newblogwwds_4_tfnewblogtitulo = "";
         AV60Newblogwwds_5_tfnewblogtitulo_sel = "";
         AV61Newblogwwds_6_tfnewblogsubtitulo = "";
         AV62Newblogwwds_7_tfnewblogsubtitulo_sel = "";
         lV56Newblogwwds_1_filterfulltext = "";
         lV59Newblogwwds_4_tfnewblogtitulo = "";
         lV61Newblogwwds_6_tfnewblogsubtitulo = "";
         A14NewBlogTitulo = "";
         A15NewBlogSubTitulo = "";
         P002H2_A14NewBlogTitulo = new string[] {""} ;
         P002H2_A25NewBlogBorrador = new bool[] {false} ;
         P002H2_A18NewBlogVisitas = new short[1] ;
         P002H2_A19NewBlogDestacado = new bool[] {false} ;
         P002H2_A12NewBlogId = new short[1] ;
         P002H2_A15NewBlogSubTitulo = new string[] {""} ;
         AV24Option = "";
         P002H3_A15NewBlogSubTitulo = new string[] {""} ;
         P002H3_A25NewBlogBorrador = new bool[] {false} ;
         P002H3_A18NewBlogVisitas = new short[1] ;
         P002H3_A19NewBlogDestacado = new bool[] {false} ;
         P002H3_A12NewBlogId = new short[1] ;
         P002H3_A14NewBlogTitulo = new string[] {""} ;
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newblogwwgetfilterdata__default(),
            new Object[][] {
                new Object[] {
               P002H2_A14NewBlogTitulo, P002H2_A25NewBlogBorrador, P002H2_A18NewBlogVisitas, P002H2_A19NewBlogDestacado, P002H2_A12NewBlogId, P002H2_A15NewBlogSubTitulo
               }
               , new Object[] {
               P002H3_A15NewBlogSubTitulo, P002H3_A25NewBlogBorrador, P002H3_A18NewBlogVisitas, P002H3_A19NewBlogDestacado, P002H3_A12NewBlogId, P002H3_A14NewBlogTitulo
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV22MaxItems ;
      private short AV21PageIndex ;
      private short AV20SkipItems ;
      private short AV11TFNewBlogId ;
      private short AV12TFNewBlogId_To ;
      private short AV44TFNewBlogDestacado_Sel ;
      private short AV45TFNewBlogVisitas ;
      private short AV46TFNewBlogVisitas_To ;
      private short AV47TFNewBlogBorrador_Sel ;
      private short AV57Newblogwwds_2_tfnewblogid ;
      private short AV58Newblogwwds_3_tfnewblogid_to ;
      private short AV63Newblogwwds_8_tfnewblogdestacado_sel ;
      private short AV64Newblogwwds_9_tfnewblogvisitas ;
      private short AV65Newblogwwds_10_tfnewblogvisitas_to ;
      private short AV66Newblogwwds_11_tfnewblogborrador_sel ;
      private short A12NewBlogId ;
      private short A18NewBlogVisitas ;
      private int AV54GXV1 ;
      private long AV29count ;
      private bool returnInSub ;
      private bool A19NewBlogDestacado ;
      private bool A25NewBlogBorrador ;
      private bool BRK2H2 ;
      private bool BRK2H4 ;
      private string AV38OptionsJson ;
      private string AV39OptionsDescJson ;
      private string AV40OptionIndexesJson ;
      private string AV35DDOName ;
      private string AV36SearchTxtParms ;
      private string AV37SearchTxtTo ;
      private string AV19SearchTxt ;
      private string AV41FilterFullText ;
      private string AV13TFNewBlogTitulo ;
      private string AV14TFNewBlogTitulo_Sel ;
      private string AV15TFNewBlogSubTitulo ;
      private string AV16TFNewBlogSubTitulo_Sel ;
      private string AV56Newblogwwds_1_filterfulltext ;
      private string AV59Newblogwwds_4_tfnewblogtitulo ;
      private string AV60Newblogwwds_5_tfnewblogtitulo_sel ;
      private string AV61Newblogwwds_6_tfnewblogsubtitulo ;
      private string AV62Newblogwwds_7_tfnewblogsubtitulo_sel ;
      private string lV56Newblogwwds_1_filterfulltext ;
      private string lV59Newblogwwds_4_tfnewblogtitulo ;
      private string lV61Newblogwwds_6_tfnewblogsubtitulo ;
      private string A14NewBlogTitulo ;
      private string A15NewBlogSubTitulo ;
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
      private string[] P002H2_A14NewBlogTitulo ;
      private bool[] P002H2_A25NewBlogBorrador ;
      private short[] P002H2_A18NewBlogVisitas ;
      private bool[] P002H2_A19NewBlogDestacado ;
      private short[] P002H2_A12NewBlogId ;
      private string[] P002H2_A15NewBlogSubTitulo ;
      private string[] P002H3_A15NewBlogSubTitulo ;
      private bool[] P002H3_A25NewBlogBorrador ;
      private short[] P002H3_A18NewBlogVisitas ;
      private bool[] P002H3_A19NewBlogDestacado ;
      private short[] P002H3_A12NewBlogId ;
      private string[] P002H3_A14NewBlogTitulo ;
      private string aP3_OptionsJson ;
      private string aP4_OptionsDescJson ;
      private string aP5_OptionIndexesJson ;
   }

   public class newblogwwgetfilterdata__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002H2( IGxContext context ,
                                             string AV56Newblogwwds_1_filterfulltext ,
                                             short AV57Newblogwwds_2_tfnewblogid ,
                                             short AV58Newblogwwds_3_tfnewblogid_to ,
                                             string AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                             string AV59Newblogwwds_4_tfnewblogtitulo ,
                                             string AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                             string AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                             short AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                             short AV64Newblogwwds_9_tfnewblogvisitas ,
                                             short AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                             short AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                             short A12NewBlogId ,
                                             string A14NewBlogTitulo ,
                                             string A15NewBlogSubTitulo ,
                                             short A18NewBlogVisitas ,
                                             bool A19NewBlogDestacado ,
                                             bool A25NewBlogBorrador )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[12];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT `NewBlogTitulo`, `NewBlogBorrador`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogId`, `NewBlogSubTitulo` FROM `NewBlog`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewBlogId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogSubTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewBlogVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int1[0] = 1;
            GXv_int1[1] = 1;
            GXv_int1[2] = 1;
            GXv_int1[3] = 1;
         }
         if ( ! (0==AV57Newblogwwds_2_tfnewblogid) )
         {
            AddWhere(sWhereString, "(`NewBlogId` >= @AV57Newblogwwds_2_tfnewblogid)");
         }
         else
         {
            GXv_int1[4] = 1;
         }
         if ( ! (0==AV58Newblogwwds_3_tfnewblogid_to) )
         {
            AddWhere(sWhereString, "(`NewBlogId` <= @AV58Newblogwwds_3_tfnewblogid_to)");
         }
         else
         {
            GXv_int1[5] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` like @lV59Newblogwwds_4_tfnewblogtitulo)");
         }
         else
         {
            GXv_int1[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ! ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` = @AV60Newblogwwds_5_tfnewblogtitulo_sel)");
         }
         else
         {
            GXv_int1[7] = 1;
         }
         if ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogTitulo`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` like @lV61Newblogwwds_6_tfnewblogsubtitulo)");
         }
         else
         {
            GXv_int1[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ! ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` = @AV62Newblogwwds_7_tfnewblogsubtitulo_sel)");
         }
         else
         {
            GXv_int1[9] = 1;
         }
         if ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogSubTitulo`))=0))");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 1)");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 0)");
         }
         if ( ! (0==AV64Newblogwwds_9_tfnewblogvisitas) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` >= @AV64Newblogwwds_9_tfnewblogvisitas)");
         }
         else
         {
            GXv_int1[10] = 1;
         }
         if ( ! (0==AV65Newblogwwds_10_tfnewblogvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` <= @AV65Newblogwwds_10_tfnewblogvisitas_to)");
         }
         else
         {
            GXv_int1[11] = 1;
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 1)");
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 0)");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY `NewBlogTitulo`";
         GXv_Object2[0] = scmdbuf;
         GXv_Object2[1] = GXv_int1;
         return GXv_Object2 ;
      }

      protected Object[] conditional_P002H3( IGxContext context ,
                                             string AV56Newblogwwds_1_filterfulltext ,
                                             short AV57Newblogwwds_2_tfnewblogid ,
                                             short AV58Newblogwwds_3_tfnewblogid_to ,
                                             string AV60Newblogwwds_5_tfnewblogtitulo_sel ,
                                             string AV59Newblogwwds_4_tfnewblogtitulo ,
                                             string AV62Newblogwwds_7_tfnewblogsubtitulo_sel ,
                                             string AV61Newblogwwds_6_tfnewblogsubtitulo ,
                                             short AV63Newblogwwds_8_tfnewblogdestacado_sel ,
                                             short AV64Newblogwwds_9_tfnewblogvisitas ,
                                             short AV65Newblogwwds_10_tfnewblogvisitas_to ,
                                             short AV66Newblogwwds_11_tfnewblogborrador_sel ,
                                             short A12NewBlogId ,
                                             string A14NewBlogTitulo ,
                                             string A15NewBlogSubTitulo ,
                                             short A18NewBlogVisitas ,
                                             bool A19NewBlogDestacado ,
                                             bool A25NewBlogBorrador )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int3 = new short[12];
         Object[] GXv_Object4 = new Object[2];
         scmdbuf = "SELECT `NewBlogSubTitulo`, `NewBlogBorrador`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogId`, `NewBlogTitulo` FROM `NewBlog`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV56Newblogwwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( (LPAD(REPLACE(FORMAT(`NewBlogId`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( `NewBlogSubTitulo` like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)) or ( (LPAD(REPLACE(FORMAT(`NewBlogVisitas`,0), ',', ''), 4, ' ')) like CONCAT('%', @lV56Newblogwwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int3[0] = 1;
            GXv_int3[1] = 1;
            GXv_int3[2] = 1;
            GXv_int3[3] = 1;
         }
         if ( ! (0==AV57Newblogwwds_2_tfnewblogid) )
         {
            AddWhere(sWhereString, "(`NewBlogId` >= @AV57Newblogwwds_2_tfnewblogid)");
         }
         else
         {
            GXv_int3[4] = 1;
         }
         if ( ! (0==AV58Newblogwwds_3_tfnewblogid_to) )
         {
            AddWhere(sWhereString, "(`NewBlogId` <= @AV58Newblogwwds_3_tfnewblogid_to)");
         }
         else
         {
            GXv_int3[5] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV59Newblogwwds_4_tfnewblogtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` like @lV59Newblogwwds_4_tfnewblogtitulo)");
         }
         else
         {
            GXv_int3[6] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV60Newblogwwds_5_tfnewblogtitulo_sel)) && ! ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogTitulo` = @AV60Newblogwwds_5_tfnewblogtitulo_sel)");
         }
         else
         {
            GXv_int3[7] = 1;
         }
         if ( StringUtil.StrCmp(AV60Newblogwwds_5_tfnewblogtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogTitulo`))=0))");
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV61Newblogwwds_6_tfnewblogsubtitulo)) ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` like @lV61Newblogwwds_6_tfnewblogsubtitulo)");
         }
         else
         {
            GXv_int3[8] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV62Newblogwwds_7_tfnewblogsubtitulo_sel)) && ! ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`NewBlogSubTitulo` = @AV62Newblogwwds_7_tfnewblogsubtitulo_sel)");
         }
         else
         {
            GXv_int3[9] = 1;
         }
         if ( StringUtil.StrCmp(AV62Newblogwwds_7_tfnewblogsubtitulo_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`NewBlogSubTitulo`))=0))");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 1)");
         }
         if ( AV63Newblogwwds_8_tfnewblogdestacado_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogDestacado` = 0)");
         }
         if ( ! (0==AV64Newblogwwds_9_tfnewblogvisitas) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` >= @AV64Newblogwwds_9_tfnewblogvisitas)");
         }
         else
         {
            GXv_int3[10] = 1;
         }
         if ( ! (0==AV65Newblogwwds_10_tfnewblogvisitas_to) )
         {
            AddWhere(sWhereString, "(`NewBlogVisitas` <= @AV65Newblogwwds_10_tfnewblogvisitas_to)");
         }
         else
         {
            GXv_int3[11] = 1;
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 1 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 1)");
         }
         if ( AV66Newblogwwds_11_tfnewblogborrador_sel == 2 )
         {
            AddWhere(sWhereString, "(`NewBlogBorrador` = 0)");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY `NewBlogSubTitulo`";
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
                     return conditional_P002H2(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (bool)dynConstraints[16] );
               case 1 :
                     return conditional_P002H3(context, (string)dynConstraints[0] , (short)dynConstraints[1] , (short)dynConstraints[2] , (string)dynConstraints[3] , (string)dynConstraints[4] , (string)dynConstraints[5] , (string)dynConstraints[6] , (short)dynConstraints[7] , (short)dynConstraints[8] , (short)dynConstraints[9] , (short)dynConstraints[10] , (short)dynConstraints[11] , (string)dynConstraints[12] , (string)dynConstraints[13] , (short)dynConstraints[14] , (bool)dynConstraints[15] , (bool)dynConstraints[16] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP002H2;
          prmP002H2 = new Object[] {
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV57Newblogwwds_2_tfnewblogid",GXType.Int16,4,0) ,
          new ParDef("@AV58Newblogwwds_3_tfnewblogid_to",GXType.Int16,4,0) ,
          new ParDef("@lV59Newblogwwds_4_tfnewblogtitulo",GXType.Char,200,0) ,
          new ParDef("@AV60Newblogwwds_5_tfnewblogtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@lV61Newblogwwds_6_tfnewblogsubtitulo",GXType.Char,200,0) ,
          new ParDef("@AV62Newblogwwds_7_tfnewblogsubtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@AV64Newblogwwds_9_tfnewblogvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV65Newblogwwds_10_tfnewblogvisitas_to",GXType.Int16,4,0)
          };
          Object[] prmP002H3;
          prmP002H3 = new Object[] {
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV56Newblogwwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@AV57Newblogwwds_2_tfnewblogid",GXType.Int16,4,0) ,
          new ParDef("@AV58Newblogwwds_3_tfnewblogid_to",GXType.Int16,4,0) ,
          new ParDef("@lV59Newblogwwds_4_tfnewblogtitulo",GXType.Char,200,0) ,
          new ParDef("@AV60Newblogwwds_5_tfnewblogtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@lV61Newblogwwds_6_tfnewblogsubtitulo",GXType.Char,200,0) ,
          new ParDef("@AV62Newblogwwds_7_tfnewblogsubtitulo_sel",GXType.Char,200,0) ,
          new ParDef("@AV64Newblogwwds_9_tfnewblogvisitas",GXType.Int16,4,0) ,
          new ParDef("@AV65Newblogwwds_10_tfnewblogvisitas_to",GXType.Int16,4,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002H2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002H2,100, GxCacheFrequency.OFF ,true,false )
             ,new CursorDef("P002H3", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002H3,100, GxCacheFrequency.OFF ,true,false )
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
                ((bool[]) buf[1])[0] = rslt.getBool(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                ((bool[]) buf[3])[0] = rslt.getBool(4);
                ((short[]) buf[4])[0] = rslt.getShort(5);
                ((string[]) buf[5])[0] = rslt.getVarchar(6);
                return;
             case 1 :
                ((string[]) buf[0])[0] = rslt.getVarchar(1);
                ((bool[]) buf[1])[0] = rslt.getBool(2);
                ((short[]) buf[2])[0] = rslt.getShort(3);
                ((bool[]) buf[3])[0] = rslt.getBool(4);
                ((short[]) buf[4])[0] = rslt.getShort(5);
                ((string[]) buf[5])[0] = rslt.getVarchar(6);
                return;
       }
    }

 }

}
