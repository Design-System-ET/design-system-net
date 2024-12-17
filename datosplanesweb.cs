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
   public class datosplanesweb : GXProcedure
   {
      public datosplanesweb( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public datosplanesweb( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out decimal aP0_CostoPlanBasico ,
                           out decimal aP1_CuotaPlanBasico ,
                           out decimal aP2_CostoPlanSuperior ,
                           out decimal aP3_CuotaPlanSuperior ,
                           out decimal aP4_CostoPlanNegocios ,
                           out decimal aP5_CuotaPlanNegocios )
      {
         this.AV8CostoPlanBasico = 0 ;
         this.AV9CuotaPlanBasico = 0 ;
         this.AV15CostoPlanSuperior = 0 ;
         this.AV14CuotaPlanSuperior = 0 ;
         this.AV13CostoPlanNegocios = 0 ;
         this.AV12CuotaPlanNegocios = 0 ;
         initialize();
         ExecuteImpl();
         aP0_CostoPlanBasico=this.AV8CostoPlanBasico;
         aP1_CuotaPlanBasico=this.AV9CuotaPlanBasico;
         aP2_CostoPlanSuperior=this.AV15CostoPlanSuperior;
         aP3_CuotaPlanSuperior=this.AV14CuotaPlanSuperior;
         aP4_CostoPlanNegocios=this.AV13CostoPlanNegocios;
         aP5_CuotaPlanNegocios=this.AV12CuotaPlanNegocios;
      }

      public decimal executeUdp( out decimal aP0_CostoPlanBasico ,
                                 out decimal aP1_CuotaPlanBasico ,
                                 out decimal aP2_CostoPlanSuperior ,
                                 out decimal aP3_CuotaPlanSuperior ,
                                 out decimal aP4_CostoPlanNegocios )
      {
         execute(out aP0_CostoPlanBasico, out aP1_CuotaPlanBasico, out aP2_CostoPlanSuperior, out aP3_CuotaPlanSuperior, out aP4_CostoPlanNegocios, out aP5_CuotaPlanNegocios);
         return AV12CuotaPlanNegocios ;
      }

      public void executeSubmit( out decimal aP0_CostoPlanBasico ,
                                 out decimal aP1_CuotaPlanBasico ,
                                 out decimal aP2_CostoPlanSuperior ,
                                 out decimal aP3_CuotaPlanSuperior ,
                                 out decimal aP4_CostoPlanNegocios ,
                                 out decimal aP5_CuotaPlanNegocios )
      {
         this.AV8CostoPlanBasico = 0 ;
         this.AV9CuotaPlanBasico = 0 ;
         this.AV15CostoPlanSuperior = 0 ;
         this.AV14CuotaPlanSuperior = 0 ;
         this.AV13CostoPlanNegocios = 0 ;
         this.AV12CuotaPlanNegocios = 0 ;
         SubmitImpl();
         aP0_CostoPlanBasico=this.AV8CostoPlanBasico;
         aP1_CuotaPlanBasico=this.AV9CuotaPlanBasico;
         aP2_CostoPlanSuperior=this.AV15CostoPlanSuperior;
         aP3_CuotaPlanSuperior=this.AV14CuotaPlanSuperior;
         aP4_CostoPlanNegocios=this.AV13CostoPlanNegocios;
         aP5_CuotaPlanNegocios=this.AV12CuotaPlanNegocios;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P003A2 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A44ConfiguracionEmpresaId = P003A2_A44ConfiguracionEmpresaId[0];
            A46ConfiguracionEmpresaCostoPlanB = P003A2_A46ConfiguracionEmpresaCostoPlanB[0];
            A47ConfiguracionEmpresaCuotaPlanB = P003A2_A47ConfiguracionEmpresaCuotaPlanB[0];
            A48ConfiguracionEmpresaCostoPlanS = P003A2_A48ConfiguracionEmpresaCostoPlanS[0];
            A49ConfiguracionEmpresaCuotaPlanS = P003A2_A49ConfiguracionEmpresaCuotaPlanS[0];
            A50ConfiguracionEmpresaCostoPlanN = P003A2_A50ConfiguracionEmpresaCostoPlanN[0];
            A51ConfiguracionEmpresaCuotaPlanN = P003A2_A51ConfiguracionEmpresaCuotaPlanN[0];
            AV8CostoPlanBasico = A46ConfiguracionEmpresaCostoPlanB;
            AV9CuotaPlanBasico = A47ConfiguracionEmpresaCuotaPlanB;
            AV15CostoPlanSuperior = A48ConfiguracionEmpresaCostoPlanS;
            AV14CuotaPlanSuperior = A49ConfiguracionEmpresaCuotaPlanS;
            AV13CostoPlanNegocios = A50ConfiguracionEmpresaCostoPlanN;
            AV12CuotaPlanNegocios = A51ConfiguracionEmpresaCuotaPlanN;
            /* Exiting from a For First loop. */
            if (true) break;
         }
         pr_default.close(0);
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
         P003A2_A44ConfiguracionEmpresaId = new short[1] ;
         P003A2_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         P003A2_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         P003A2_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         P003A2_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         P003A2_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         P003A2_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.datosplanesweb__default(),
            new Object[][] {
                new Object[] {
               P003A2_A44ConfiguracionEmpresaId, P003A2_A46ConfiguracionEmpresaCostoPlanB, P003A2_A47ConfiguracionEmpresaCuotaPlanB, P003A2_A48ConfiguracionEmpresaCostoPlanS, P003A2_A49ConfiguracionEmpresaCuotaPlanS, P003A2_A50ConfiguracionEmpresaCostoPlanN, P003A2_A51ConfiguracionEmpresaCuotaPlanN
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A44ConfiguracionEmpresaId ;
      private decimal AV8CostoPlanBasico ;
      private decimal AV9CuotaPlanBasico ;
      private decimal AV15CostoPlanSuperior ;
      private decimal AV14CuotaPlanSuperior ;
      private decimal AV13CostoPlanNegocios ;
      private decimal AV12CuotaPlanNegocios ;
      private decimal A46ConfiguracionEmpresaCostoPlanB ;
      private decimal A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal A48ConfiguracionEmpresaCostoPlanS ;
      private decimal A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal A50ConfiguracionEmpresaCostoPlanN ;
      private decimal A51ConfiguracionEmpresaCuotaPlanN ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private short[] P003A2_A44ConfiguracionEmpresaId ;
      private decimal[] P003A2_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] P003A2_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] P003A2_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] P003A2_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] P003A2_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] P003A2_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal aP0_CostoPlanBasico ;
      private decimal aP1_CuotaPlanBasico ;
      private decimal aP2_CostoPlanSuperior ;
      private decimal aP3_CuotaPlanSuperior ;
      private decimal aP4_CostoPlanNegocios ;
      private decimal aP5_CuotaPlanNegocios ;
   }

   public class datosplanesweb__default : DataStoreHelperBase, IDataStoreHelper
   {
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
          Object[] prmP003A2;
          prmP003A2 = new Object[] {
          };
          def= new CursorDef[] {
              new CursorDef("P003A2", "SELECT `ConfiguracionEmpresaId`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanN` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = 1 ORDER BY `ConfiguracionEmpresaId` ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP003A2,1, GxCacheFrequency.OFF ,false,true )
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
                ((decimal[]) buf[1])[0] = rslt.getDecimal(2);
                ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
                ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
                ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
                ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
                ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
                return;
       }
    }

 }

}
