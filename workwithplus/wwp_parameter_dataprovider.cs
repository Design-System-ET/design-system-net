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
namespace DesignSystem.Programs.workwithplus {
   public class wwp_parameter_dataprovider : GXProcedure
   {
      public wwp_parameter_dataprovider( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
      }

      public wwp_parameter_dataprovider( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( out GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> aP0_ReturnValue )
      {
         this.AV2ReturnValue = new GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>( context, "WWP_Parameter", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP0_ReturnValue=this.AV2ReturnValue;
      }

      public GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> executeUdp( )
      {
         execute(out aP0_ReturnValue);
         return AV2ReturnValue ;
      }

      public void executeSubmit( out GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> aP0_ReturnValue )
      {
         this.AV2ReturnValue = new GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>( context, "WWP_Parameter", "DesignSystem") ;
         SubmitImpl();
         aP0_ReturnValue=this.AV2ReturnValue;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         args = new Object[] {(GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>)AV2ReturnValue} ;
         ClassLoader.Execute("workwithplus.awwp_parameter_dataprovider","DesignSystem.Programs","workwithplus.awwp_parameter_dataprovider", new Object[] {context }, "execute", args);
         if ( ( args != null ) && ( args.Length == 1 ) )
         {
            AV2ReturnValue = (GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>)(args[0]) ;
         }
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         AV2ReturnValue = new GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>( context, "WWP_Parameter", "DesignSystem");
         /* GeneXus formulas. */
      }

      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> AV2ReturnValue ;
      private Object[] args ;
      private GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> aP0_ReturnValue ;
   }

}
