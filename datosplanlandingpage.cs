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
   public class datosplanlandingpage : GXProcedure
   {
      public datosplanlandingpage( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public datosplanlandingpage( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out decimal aP0_CostoLandingPage ,
                           out decimal aP1_CuotaLandingPage )
      {
         this.AV10CostoLandingPage = 0 ;
         this.AV11CuotaLandingPage = 0 ;
         initialize();
         ExecuteImpl();
         aP0_CostoLandingPage=this.AV10CostoLandingPage;
         aP1_CuotaLandingPage=this.AV11CuotaLandingPage;
      }

      public decimal executeUdp( out decimal aP0_CostoLandingPage )
      {
         execute(out aP0_CostoLandingPage, out aP1_CuotaLandingPage);
         return AV11CuotaLandingPage ;
      }

      public void executeSubmit( out decimal aP0_CostoLandingPage ,
                                 out decimal aP1_CuotaLandingPage )
      {
         this.AV10CostoLandingPage = 0 ;
         this.AV11CuotaLandingPage = 0 ;
         SubmitImpl();
         aP0_CostoLandingPage=this.AV10CostoLandingPage;
         aP1_CuotaLandingPage=this.AV11CuotaLandingPage;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P003E2 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A44ConfiguracionEmpresaId = P003E2_A44ConfiguracionEmpresaId[0];
            A54ConfiguracionEmpresaCostoLandi = P003E2_A54ConfiguracionEmpresaCostoLandi[0];
            A55ConfiguracionEmpresaCuotaLandi = P003E2_A55ConfiguracionEmpresaCuotaLandi[0];
            AV10CostoLandingPage = A54ConfiguracionEmpresaCostoLandi;
            AV11CuotaLandingPage = A55ConfiguracionEmpresaCuotaLandi;
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
         P003E2_A44ConfiguracionEmpresaId = new short[1] ;
         P003E2_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         P003E2_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.datosplanlandingpage__default(),
            new Object[][] {
                new Object[] {
               P003E2_A44ConfiguracionEmpresaId, P003E2_A54ConfiguracionEmpresaCostoLandi, P003E2_A55ConfiguracionEmpresaCuotaLandi
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A44ConfiguracionEmpresaId ;
      private decimal AV10CostoLandingPage ;
      private decimal AV11CuotaLandingPage ;
      private decimal A54ConfiguracionEmpresaCostoLandi ;
      private decimal A55ConfiguracionEmpresaCuotaLandi ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private short[] P003E2_A44ConfiguracionEmpresaId ;
      private decimal[] P003E2_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] P003E2_A55ConfiguracionEmpresaCuotaLandi ;
      private decimal aP0_CostoLandingPage ;
      private decimal aP1_CuotaLandingPage ;
   }

   public class datosplanlandingpage__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP003E2;
          prmP003E2 = new Object[] {
          };
          def= new CursorDef[] {
              new CursorDef("P003E2", "SELECT `ConfiguracionEmpresaId`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = 1 ORDER BY `ConfiguracionEmpresaId` ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP003E2,1, GxCacheFrequency.OFF ,false,true )
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
                return;
       }
    }

 }

}
