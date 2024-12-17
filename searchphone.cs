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
   public class searchphone : GXProcedure
   {
      public searchphone( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public searchphone( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out string aP0_Phone )
      {
         this.AV8Phone = "" ;
         initialize();
         ExecuteImpl();
         aP0_Phone=this.AV8Phone;
      }

      public string executeUdp( )
      {
         execute(out aP0_Phone);
         return AV8Phone ;
      }

      public void executeSubmit( out string aP0_Phone )
      {
         this.AV8Phone = "" ;
         SubmitImpl();
         aP0_Phone=this.AV8Phone;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         /* Using cursor P00342 */
         pr_default.execute(0);
         while ( (pr_default.getStatus(0) != 101) )
         {
            A44ConfiguracionEmpresaId = P00342_A44ConfiguracionEmpresaId[0];
            A45ConfiguracionEmpresaTelefono = P00342_A45ConfiguracionEmpresaTelefono[0];
            AV8Phone = A45ConfiguracionEmpresaTelefono;
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
         AV8Phone = "";
         P00342_A44ConfiguracionEmpresaId = new short[1] ;
         P00342_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         A45ConfiguracionEmpresaTelefono = "";
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.searchphone__default(),
            new Object[][] {
                new Object[] {
               P00342_A44ConfiguracionEmpresaId, P00342_A45ConfiguracionEmpresaTelefono
               }
            }
         );
         /* GeneXus formulas. */
      }

      private short A44ConfiguracionEmpresaId ;
      private string AV8Phone ;
      private string A45ConfiguracionEmpresaTelefono ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private IDataStoreProvider pr_default ;
      private short[] P00342_A44ConfiguracionEmpresaId ;
      private string[] P00342_A45ConfiguracionEmpresaTelefono ;
      private string aP0_Phone ;
   }

   public class searchphone__default : DataStoreHelperBase, IDataStoreHelper
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
          Object[] prmP00342;
          prmP00342 = new Object[] {
          };
          def= new CursorDef[] {
              new CursorDef("P00342", "SELECT `ConfiguracionEmpresaId`, `ConfiguracionEmpresaTelefono` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = 1 ORDER BY `ConfiguracionEmpresaId` ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00342,1, GxCacheFrequency.OFF ,false,true )
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
                ((string[]) buf[1])[0] = rslt.getString(2, 20);
                return;
       }
    }

 }

}
