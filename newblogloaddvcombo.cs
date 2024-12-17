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
   public class newblogloaddvcombo : GXProcedure
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
            return "newblog_Services_Execute" ;
         }

      }

      public newblogloaddvcombo( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newblogloaddvcombo( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( string aP0_ComboName ,
                           string aP1_TrnMode ,
                           bool aP2_IsDynamicCall ,
                           short aP3_NewBlogId ,
                           string aP4_SearchTxtParms ,
                           out string aP5_SelectedValue ,
                           out string aP6_SelectedText ,
                           out string aP7_Combo_DataJson )
      {
         this.AV13ComboName = aP0_ComboName;
         this.AV14TrnMode = aP1_TrnMode;
         this.AV23IsDynamicCall = aP2_IsDynamicCall;
         this.AV15NewBlogId = aP3_NewBlogId;
         this.AV24SearchTxtParms = aP4_SearchTxtParms;
         this.AV16SelectedValue = "" ;
         this.AV25SelectedText = "" ;
         this.AV26Combo_DataJson = "" ;
         initialize();
         ExecuteImpl();
         aP5_SelectedValue=this.AV16SelectedValue;
         aP6_SelectedText=this.AV25SelectedText;
         aP7_Combo_DataJson=this.AV26Combo_DataJson;
      }

      public string executeUdp( string aP0_ComboName ,
                                string aP1_TrnMode ,
                                bool aP2_IsDynamicCall ,
                                short aP3_NewBlogId ,
                                string aP4_SearchTxtParms ,
                                out string aP5_SelectedValue ,
                                out string aP6_SelectedText )
      {
         execute(aP0_ComboName, aP1_TrnMode, aP2_IsDynamicCall, aP3_NewBlogId, aP4_SearchTxtParms, out aP5_SelectedValue, out aP6_SelectedText, out aP7_Combo_DataJson);
         return AV26Combo_DataJson ;
      }

      public void executeSubmit( string aP0_ComboName ,
                                 string aP1_TrnMode ,
                                 bool aP2_IsDynamicCall ,
                                 short aP3_NewBlogId ,
                                 string aP4_SearchTxtParms ,
                                 out string aP5_SelectedValue ,
                                 out string aP6_SelectedText ,
                                 out string aP7_Combo_DataJson )
      {
         this.AV13ComboName = aP0_ComboName;
         this.AV14TrnMode = aP1_TrnMode;
         this.AV23IsDynamicCall = aP2_IsDynamicCall;
         this.AV15NewBlogId = aP3_NewBlogId;
         this.AV24SearchTxtParms = aP4_SearchTxtParms;
         this.AV16SelectedValue = "" ;
         this.AV25SelectedText = "" ;
         this.AV26Combo_DataJson = "" ;
         SubmitImpl();
         aP5_SelectedValue=this.AV16SelectedValue;
         aP6_SelectedText=this.AV25SelectedText;
         aP7_Combo_DataJson=this.AV26Combo_DataJson;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV9WWPContext) ;
         AV19MaxItems = 10;
         AV21PageIndex = (short)((String.IsNullOrEmpty(StringUtil.RTrim( AV24SearchTxtParms))||StringUtil.StartsWith( AV14TrnMode, "GET") ? 0 : (long)(Math.Round(NumberUtil.Val( StringUtil.Substring( AV24SearchTxtParms, 1, 2), "."), 18, MidpointRounding.ToEven))));
         AV22SearchTxt = (String.IsNullOrEmpty(StringUtil.RTrim( AV24SearchTxtParms))||StringUtil.StartsWith( AV14TrnMode, "GET") ? AV24SearchTxtParms : StringUtil.Substring( AV24SearchTxtParms, 3, -1));
         AV20SkipItems = (short)(AV21PageIndex*AV19MaxItems);
         if ( StringUtil.StrCmp(AV13ComboName, "CategoriasId") == 0 )
         {
            /* Execute user subroutine: 'LOADCOMBOITEMS_CATEGORIASID' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'LOADCOMBOITEMS_CATEGORIASID' Routine */
         returnInSub = false;
         if ( AV23IsDynamicCall )
         {
            GXPagingFrom2 = AV20SkipItems;
            GXPagingTo2 = ((AV19MaxItems>0) ? AV19MaxItems : 100000000);
            pr_default.dynParam(0, new Object[]{ new Object[]{
                                                 AV22SearchTxt ,
                                                 A21CategoriasCategoria } ,
                                                 new int[]{
                                                 }
            });
            lV22SearchTxt = StringUtil.Concat( StringUtil.RTrim( AV22SearchTxt), "%", "");
            /* Using cursor P002N2 */
            pr_default.execute(0, new Object[] {lV22SearchTxt, GXPagingFrom2, GXPagingTo2});
            while ( (pr_default.getStatus(0) != 101) )
            {
               A21CategoriasCategoria = P002N2_A21CategoriasCategoria[0];
               A20CategoriasId = P002N2_A20CategoriasId[0];
               AV12Combo_DataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item(context);
               AV12Combo_DataItem.gxTpr_Id = StringUtil.Trim( StringUtil.Str( (decimal)(A20CategoriasId), 4, 0));
               AV12Combo_DataItem.gxTpr_Title = A21CategoriasCategoria;
               AV11Combo_Data.Add(AV12Combo_DataItem, 0);
               if ( AV11Combo_Data.Count > AV19MaxItems )
               {
                  /* Exit For each command. Update data (if necessary), close cursors & exit. */
                  if (true) break;
               }
               pr_default.readNext(0);
            }
            pr_default.close(0);
            AV26Combo_DataJson = AV11Combo_Data.ToJSonString(false);
         }
         else
         {
            if ( StringUtil.StrCmp(AV14TrnMode, "INS") != 0 )
            {
               if ( StringUtil.StrCmp(AV14TrnMode, "GET") != 0 )
               {
                  /* Using cursor P002N3 */
                  pr_default.execute(1, new Object[] {AV15NewBlogId});
                  while ( (pr_default.getStatus(1) != 101) )
                  {
                     A12NewBlogId = P002N3_A12NewBlogId[0];
                     A20CategoriasId = P002N3_A20CategoriasId[0];
                     A21CategoriasCategoria = P002N3_A21CategoriasCategoria[0];
                     A21CategoriasCategoria = P002N3_A21CategoriasCategoria[0];
                     AV16SelectedValue = ((0==A20CategoriasId) ? "" : StringUtil.Trim( StringUtil.Str( (decimal)(A20CategoriasId), 4, 0)));
                     AV25SelectedText = A21CategoriasCategoria;
                     /* Exiting from a For First loop. */
                     if (true) break;
                  }
                  pr_default.close(1);
               }
               else
               {
                  AV28CategoriasId = (short)(Math.Round(NumberUtil.Val( AV22SearchTxt, "."), 18, MidpointRounding.ToEven));
                  /* Using cursor P002N4 */
                  pr_default.execute(2, new Object[] {AV28CategoriasId});
                  while ( (pr_default.getStatus(2) != 101) )
                  {
                     A20CategoriasId = P002N4_A20CategoriasId[0];
                     A21CategoriasCategoria = P002N4_A21CategoriasCategoria[0];
                     AV25SelectedText = A21CategoriasCategoria;
                     /* Exit For each command. Update data (if necessary), close cursors & exit. */
                     if (true) break;
                     /* Exiting from a For First loop. */
                     if (true) break;
                  }
                  pr_default.close(2);
               }
            }
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
         AV16SelectedValue = "";
         AV25SelectedText = "";
         AV26Combo_DataJson = "";
         AV9WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV22SearchTxt = "";
         lV22SearchTxt = "";
         A21CategoriasCategoria = "";
         P002N2_A21CategoriasCategoria = new string[] {""} ;
         P002N2_A20CategoriasId = new short[1] ;
         AV12Combo_DataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item(context);
         AV11Combo_Data = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Item", "");
         P002N3_A12NewBlogId = new short[1] ;
         P002N3_A20CategoriasId = new short[1] ;
         P002N3_A21CategoriasCategoria = new string[] {""} ;
         P002N4_A20CategoriasId = new short[1] ;
         P002N4_A21CategoriasCategoria = new string[] {""} ;
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newblogloaddvcombo__default(),
            new Object[][] {
                new Object[] {
               P002N2_A21CategoriasCategoria, P002N2_A20CategoriasId
               }
               , new Object[] {
               P002N3_A12NewBlogId, P002N3_A20CategoriasId, P002N3_A21CategoriasCategoria
               }
               , new Object[] {
               P002N4_A20CategoriasId, P002N4_A21CategoriasCategoria
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short AV15NewBlogId ;
      private short AV21PageIndex ;
      private short AV20SkipItems ;
      private short A20CategoriasId ;
      private short A12NewBlogId ;
      private short AV28CategoriasId ;
      private int AV19MaxItems ;
      private int GXPagingFrom2 ;
      private int GXPagingTo2 ;
      private string AV14TrnMode ;
      private string A21CategoriasCategoria ;
      private bool AV23IsDynamicCall ;
      private bool returnInSub ;
      private string AV26Combo_DataJson ;
      private string AV13ComboName ;
      private string AV24SearchTxtParms ;
      private string AV16SelectedValue ;
      private string AV25SelectedText ;
      private string AV22SearchTxt ;
      private string lV22SearchTxt ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV9WWPContext ;
      private IDataStoreProvider pr_default ;
      private string[] P002N2_A21CategoriasCategoria ;
      private short[] P002N2_A20CategoriasId ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item AV12Combo_DataItem ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> AV11Combo_Data ;
      private short[] P002N3_A12NewBlogId ;
      private short[] P002N3_A20CategoriasId ;
      private string[] P002N3_A21CategoriasCategoria ;
      private short[] P002N4_A20CategoriasId ;
      private string[] P002N4_A21CategoriasCategoria ;
      private string aP5_SelectedValue ;
      private string aP6_SelectedText ;
      private string aP7_Combo_DataJson ;
   }

   public class newblogloaddvcombo__default : DataStoreHelperBase, IDataStoreHelper
   {
      protected Object[] conditional_P002N2( IGxContext context ,
                                             string AV22SearchTxt ,
                                             string A21CategoriasCategoria )
      {
         System.Text.StringBuilder sWhereString = new System.Text.StringBuilder();
         string scmdbuf;
         short[] GXv_int1 = new short[3];
         Object[] GXv_Object2 = new Object[2];
         string sSelectString;
         string sFromString;
         string sOrderString;
         sSelectString = " `CategoriasCategoria`, `CategoriasId`";
         sFromString = " FROM `Categorias`";
         sOrderString = "";
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV22SearchTxt)) )
         {
            AddWhere(sWhereString, "(`CategoriasCategoria` like CONCAT('%', @lV22SearchTxt))");
         }
         else
         {
            GXv_int1[0] = 1;
         }
         sOrderString += " ORDER BY `CategoriasCategoria`";
         scmdbuf = "SELECT " + sSelectString + sFromString + sWhereString + sOrderString + "" + " LIMIT " + "@GXPagingFrom2" + ", " + "@GXPagingTo2";
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
                     return conditional_P002N2(context, (string)dynConstraints[0] , (string)dynConstraints[1] );
         }
         return base.getDynamicStatement(cursor, context, dynConstraints);
      }

      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
         ,new ForEachCursor(def[1])
         ,new ForEachCursor(def[2])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP002N3;
          prmP002N3 = new Object[] {
          new ParDef("@AV15NewBlogId",GXType.Int16,4,0)
          };
          Object[] prmP002N4;
          prmP002N4 = new Object[] {
          new ParDef("@AV28CategoriasId",GXType.Int16,4,0)
          };
          Object[] prmP002N2;
          prmP002N2 = new Object[] {
          new ParDef("@lV22SearchTxt",GXType.Char,40,0) ,
          new ParDef("@GXPagingFrom2",GXType.Int32,9,0) ,
          new ParDef("@GXPagingTo2",GXType.Int32,9,0)
          };
          def= new CursorDef[] {
              new CursorDef("P002N2", "scmdbuf",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002N2,100, GxCacheFrequency.OFF ,false,false )
             ,new CursorDef("P002N3", "SELECT T1.`NewBlogId`, T1.`CategoriasId`, T2.`CategoriasCategoria` FROM (`NewBlog` T1 INNER JOIN `Categorias` T2 ON T2.`CategoriasId` = T1.`CategoriasId`) WHERE T1.`NewBlogId` = @AV15NewBlogId ORDER BY T1.`NewBlogId` ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002N3,1, GxCacheFrequency.OFF ,false,true )
             ,new CursorDef("P002N4", "SELECT `CategoriasId`, `CategoriasCategoria` FROM `Categorias` WHERE `CategoriasId` = @AV28CategoriasId ORDER BY `CategoriasId`  LIMIT 1",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP002N4,1, GxCacheFrequency.OFF ,false,true )
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
             case 1 :
                ((short[]) buf[0])[0] = rslt.getShort(1);
                ((short[]) buf[1])[0] = rslt.getShort(2);
                ((string[]) buf[2])[0] = rslt.getString(3, 100);
                return;
             case 2 :
                ((short[]) buf[0])[0] = rslt.getShort(1);
                ((string[]) buf[1])[0] = rslt.getString(2, 100);
                return;
       }
    }

 }

}
