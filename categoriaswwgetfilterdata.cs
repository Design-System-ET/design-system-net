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
   public class categoriaswwgetfilterdata : GXProcedure
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
            return "categoriasww_Services_Execute" ;
         }

      }

      public categoriaswwgetfilterdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public categoriaswwgetfilterdata( IGxContext context )
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
         this.AV31DDOName = aP0_DDOName;
         this.AV32SearchTxtParms = aP1_SearchTxtParms;
         this.AV33SearchTxtTo = aP2_SearchTxtTo;
         this.AV34OptionsJson = "" ;
         this.AV35OptionsDescJson = "" ;
         this.AV36OptionIndexesJson = "" ;
         initialize();
         ExecuteImpl();
         aP3_OptionsJson=this.AV34OptionsJson;
         aP4_OptionsDescJson=this.AV35OptionsDescJson;
         aP5_OptionIndexesJson=this.AV36OptionIndexesJson;
      }

      public string executeUdp( string aP0_DDOName ,
                                string aP1_SearchTxtParms ,
                                string aP2_SearchTxtTo ,
                                out string aP3_OptionsJson ,
                                out string aP4_OptionsDescJson )
      {
         execute(aP0_DDOName, aP1_SearchTxtParms, aP2_SearchTxtTo, out aP3_OptionsJson, out aP4_OptionsDescJson, out aP5_OptionIndexesJson);
         return AV36OptionIndexesJson ;
      }

      public void executeSubmit( string aP0_DDOName ,
                                 string aP1_SearchTxtParms ,
                                 string aP2_SearchTxtTo ,
                                 out string aP3_OptionsJson ,
                                 out string aP4_OptionsDescJson ,
                                 out string aP5_OptionIndexesJson )
      {
         this.AV31DDOName = aP0_DDOName;
         this.AV32SearchTxtParms = aP1_SearchTxtParms;
         this.AV33SearchTxtTo = aP2_SearchTxtTo;
         this.AV34OptionsJson = "" ;
         this.AV35OptionsDescJson = "" ;
         this.AV36OptionIndexesJson = "" ;
         SubmitImpl();
         aP3_OptionsJson=this.AV34OptionsJson;
         aP4_OptionsDescJson=this.AV35OptionsDescJson;
         aP5_OptionIndexesJson=this.AV36OptionIndexesJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV21Options = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV23OptionsDesc = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV24OptionIndexes = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV18MaxItems = 10;
         AV17PageIndex = (short)((String.IsNullOrEmpty(StringUtil.RTrim( AV32SearchTxtParms)) ? 0 : (long)(Math.Round(NumberUtil.Val( StringUtil.Substring( AV32SearchTxtParms, 1, 2), "."), 18, MidpointRounding.ToEven))));
         AV15SearchTxt = (String.IsNullOrEmpty(StringUtil.RTrim( AV32SearchTxtParms)) ? "" : StringUtil.Substring( AV32SearchTxtParms, 3, -1));
         AV16SkipItems = (short)(AV17PageIndex*AV18MaxItems);
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         /* Execute user subroutine: 'LOADGRIDSTATE' */
         S111 ();
         if ( returnInSub )
         {
            cleanup();
            if (true) return;
         }
         if ( StringUtil.StrCmp(StringUtil.Upper( AV31DDOName), "DDO_CATEGORIASCATEGORIA") == 0 )
         {
            /* Execute user subroutine: 'LOADCATEGORIASCATEGORIAOPTIONS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         AV34OptionsJson = AV21Options.ToJSonString(false);
         AV35OptionsDescJson = AV23OptionsDesc.ToJSonString(false);
         AV36OptionIndexesJson = AV24OptionIndexes.ToJSonString(false);
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOADGRIDSTATE' Routine */
         returnInSub = false;
         if ( StringUtil.StrCmp(AV26Session.Get("CategoriasWWGridState"), "") == 0 )
         {
            AV28GridState.FromXml(new DesignSystem.Programs.wwpbaseobjects.loadgridstate(context).executeUdp(  "CategoriasWWGridState"), null, "", "");
         }
         else
         {
            AV28GridState.FromXml(AV26Session.Get("CategoriasWWGridState"), null, "", "");
         }
         AV44GXV1 = 1;
         while ( AV44GXV1 <= AV28GridState.gxTpr_Filtervalues.Count )
         {
            AV29GridStateFilterValue = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue)AV28GridState.gxTpr_Filtervalues.Item(AV44GXV1));
            if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "FILTERFULLTEXT") == 0 )
            {
               AV37FilterFullText = AV29GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFCATEGORIASCATEGORIA") == 0 )
            {
               AV13TFCategoriasCategoria = AV29GridStateFilterValue.gxTpr_Value;
            }
            else if ( StringUtil.StrCmp(AV29GridStateFilterValue.gxTpr_Name, "TFCATEGORIASCATEGORIA_SEL") == 0 )
            {
               AV14TFCategoriasCategoria_Sel = AV29GridStateFilterValue.gxTpr_Value;
            }
            AV44GXV1 = (int)(AV44GXV1+1);
         }
      }

      protected void S121( )
      {
         /* 'LOADCATEGORIASCATEGORIAOPTIONS' Routine */
         returnInSub = false;
         AV13TFCategoriasCategoria = AV15SearchTxt;
         AV14TFCategoriasCategoria_Sel = "";
         AV46Categoriaswwds_1_filterfulltext = AV37FilterFullText;
         AV47Categoriaswwds_2_tfcategoriascategoria = AV13TFCategoriasCategoria;
         AV48Categoriaswwds_3_tfcategoriascategoria_sel = AV14TFCategoriasCategoria_Sel;
         pr_default.dynParam(0, new Object[]{ new Object[]{
                                              AV46Categoriaswwds_1_filterfulltext ,
                                              AV48Categoriaswwds_3_tfcategoriascategoria_sel ,
                                              AV47Categoriaswwds_2_tfcategoriascategoria ,
                                              A21CategoriasCategoria } ,
                                              new int[]{
                                              }
         });
         lV46Categoriaswwds_1_filterfulltext = StringUtil.Concat( StringUtil.RTrim( AV46Categoriaswwds_1_filterfulltext), "%", "");
         lV47Categoriaswwds_2_tfcategoriascategoria = StringUtil.PadR( StringUtil.RTrim( AV47Categoriaswwds_2_tfcategoriascategoria), 100, "%");
         /* Using cursor P002K2 */
         pr_default.execute(0, new Object[] {lV46Categoriaswwds_1_filterfulltext, lV47Categoriaswwds_2_tfcategoriascategoria, AV48Categoriaswwds_3_tfcategoriascategoria_sel});
         while ( (pr_default.getStatus(0) != 101) )
         {
            BRK2K2 = false;
            A21CategoriasCategoria = P002K2_A21CategoriasCategoria[0];
            A20CategoriasId = P002K2_A20CategoriasId[0];
            AV25count = 0;
            while ( (pr_default.getStatus(0) != 101) && ( StringUtil.StrCmp(P002K2_A21CategoriasCategoria[0], A21CategoriasCategoria) == 0 ) )
            {
               BRK2K2 = false;
               A20CategoriasId = P002K2_A20CategoriasId[0];
               AV25count = (long)(AV25count+1);
               BRK2K2 = true;
               pr_default.readNext(0);
            }
            if ( (0==AV16SkipItems) )
            {
               AV20Option = (String.IsNullOrEmpty(StringUtil.RTrim( A21CategoriasCategoria)) ? "<#Empty#>" : A21CategoriasCategoria);
               AV21Options.Add(AV20Option, 0);
               AV24OptionIndexes.Add(StringUtil.Trim( context.localUtil.Format( (decimal)(AV25count), "Z,ZZZ,ZZZ,ZZ9")), 0);
               if ( AV21Options.Count == 10 )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
            }
            else
            {
               AV16SkipItems = (short)(AV16SkipItems-1);
            }
            if ( ! BRK2K2 )
            {
               BRK2K2 = true;
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
         AV34OptionsJson = "";
         AV35OptionsDescJson = "";
         AV36OptionIndexesJson = "";
         AV21Options = new GxSimpleCollection<string>();
         AV23OptionsDesc = new GxSimpleCollection<string>();
         AV24OptionIndexes = new GxSimpleCollection<string>();
         AV15SearchTxt = "";
         AV9WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV26Session = context.GetSession();
         AV28GridState = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState(context);
         AV29GridStateFilterValue = new DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue(context);
         AV37FilterFullText = "";
         AV13TFCategoriasCategoria = "";
         AV14TFCategoriasCategoria_Sel = "";
         AV46Categoriaswwds_1_filterfulltext = "";
         AV47Categoriaswwds_2_tfcategoriascategoria = "";
         AV48Categoriaswwds_3_tfcategoriascategoria_sel = "";
         lV46Categoriaswwds_1_filterfulltext = "";
         lV47Categoriaswwds_2_tfcategoriascategoria = "";
         A21CategoriasCategoria = "";
         P002K2_A21CategoriasCategoria = new string[] {""} ;
         P002K2_A20CategoriasId = new short[1] ;
         AV20Option = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.categoriaswwgetfilterdata__default(),
            new Object[][] {
                new Object[] {
               P002K2_A21CategoriasCategoria, P002K2_A20CategoriasId
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV18MaxItems ;
      private short AV17PageIndex ;
      private short AV16SkipItems ;
      private short A20CategoriasId ;
      private int AV44GXV1 ;
      private long AV25count ;
      private string AV13TFCategoriasCategoria ;
      private string AV14TFCategoriasCategoria_Sel ;
      private string AV47Categoriaswwds_2_tfcategoriascategoria ;
      private string AV48Categoriaswwds_3_tfcategoriascategoria_sel ;
      private string lV47Categoriaswwds_2_tfcategoriascategoria ;
      private string A21CategoriasCategoria ;
      private bool returnInSub ;
      private bool BRK2K2 ;
      private string AV34OptionsJson ;
      private string AV35OptionsDescJson ;
      private string AV36OptionIndexesJson ;
      private string AV31DDOName ;
      private string AV32SearchTxtParms ;
      private string AV33SearchTxtTo ;
      private string AV15SearchTxt ;
      private string AV37FilterFullText ;
      private string AV46Categoriaswwds_1_filterfulltext ;
      private string lV46Categoriaswwds_1_filterfulltext ;
      private string AV20Option ;
      private IGxSession AV26Session ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GxSimpleCollection<string> AV21Options ;
      private GxSimpleCollection<string> AV23OptionsDesc ;
      private GxSimpleCollection<string> AV24OptionIndexes ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState AV28GridState ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPGridState_FilterValue AV29GridStateFilterValue ;
      private IDataStoreProvider pr_default ;
      private string[] P002K2_A21CategoriasCategoria ;
      private short[] P002K2_A20CategoriasId ;
      private string aP3_OptionsJson ;
      private string aP4_OptionsDescJson ;
      private string aP5_OptionIndexesJson ;
   }

   public class categoriaswwgetfilterdata__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002K2( IGxContext context ,
                                             string AV46Categoriaswwds_1_filterfulltext ,
                                             string AV48Categoriaswwds_3_tfcategoriascategoria_sel ,
                                             string AV47Categoriaswwds_2_tfcategoriascategoria ,
                                             string A21CategoriasCategoria )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[3];
         Object[] GXv_Object2 = new Object[2];
         scmdbuf = "SELECT `CategoriasCategoria`, `CategoriasId` FROM `Categorias`";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV46Categoriaswwds_1_filterfulltext)) )
         {
            AddWhere(sWhereString, "(( `CategoriasCategoria` like CONCAT('%', @lV46Categoriaswwds_1_filterfulltext)))");
         }
         else
         {
            GXv_int1[0] = 1;
         }
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV48Categoriaswwds_3_tfcategoriascategoria_sel)) && ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV47Categoriaswwds_2_tfcategoriascategoria)) ) )
         {
            AddWhere(sWhereString, "(`CategoriasCategoria` like @lV47Categoriaswwds_2_tfcategoriascategoria)");
         }
         else
         {
            GXv_int1[1] = 1;
         }
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV48Categoriaswwds_3_tfcategoriascategoria_sel)) && ! ( StringUtil.StrCmp(AV48Categoriaswwds_3_tfcategoriascategoria_sel, context.GetMessage( "<#Empty#>", "")) == 0 ) )
         {
            AddWhere(sWhereString, "(`CategoriasCategoria` = @AV48Categoriaswwds_3_tfcategoriascategoria_sel)");
         }
         else
         {
            GXv_int1[2] = 1;
         }
         if ( StringUtil.StrCmp(AV48Categoriaswwds_3_tfcategoriascategoria_sel, context.GetMessage( "<#Empty#>", "")) == 0 )
         {
            AddWhere(sWhereString, "((LENGTH(TRIM(`CategoriasCategoria`))=0))");
         }
         scmdbuf += sWhereString;
         scmdbuf += " ORDER BY `CategoriasCategoria`";
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
                     return conditional_P002K2(context, (string)dynConstraints[0] , (string)dynConstraints[1] , (string)dynConstraints[2] , (string)dynConstraints[3] );
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
          Object[] prmP002K2;
          prmP002K2 = new Object[] {
          new ParDef("@lV46Categoriaswwds_1_filterfulltext",GXType.Char,100,0) ,
          new ParDef("@lV47Categoriaswwds_2_tfcategoriascategoria",GXType.Char,100,0) ,
          new ParDef("@AV48Categoriaswwds_3_tfcategoriascategoria_sel",GXType.Char,100,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002K2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002K2,100, GxCacheFrequency.OFF ,true,false )
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
