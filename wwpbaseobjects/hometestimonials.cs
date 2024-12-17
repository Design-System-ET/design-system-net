using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
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
namespace DesignSystem.Programs.wwpbaseobjects {
   public class hometestimonials : GXProcedure
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

      public hometestimonials( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public hometestimonials( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem>( context, "SDTHomeTestimonialsItem", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem>( context, "SDTHomeTestimonialsItem", "DesignSystem") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         Gxm1sdthometestimonials = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(context);
         Gxm2rootcol.Add(Gxm1sdthometestimonials, 0);
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsname = context.GetMessage( "Daniel Pachón", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialscompany = context.GetMessage( "Distribuiton Channels Manager - Skandia", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsdescription = context.GetMessage( "\"Since 2014 we have found with DVelop the way to work under agile methodologies, as well as the correct balance in the proposal of initiatives that make our business grow.\"", "");
         Gxm1sdthometestimonials = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(context);
         Gxm2rootcol.Add(Gxm1sdthometestimonials, 0);
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsname = context.GetMessage( "Tomás Galdamez", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialscompany = context.GetMessage( "Applications Developer - Ricoh", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsdescription = context.GetMessage( "“Having DVelop helping us in such an important part of our business has been key to the success of our mobile workforce strategy. DVelop constantly demonstrates its high level of professionalism and commitment and is the main reason why today we continue to use the Genexus platform in several of our business solutions\".", "");
         Gxm1sdthometestimonials = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(context);
         Gxm2rootcol.Add(Gxm1sdthometestimonials, 0);
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsname = context.GetMessage( "Ignacio Galliazzi", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialscompany = context.GetMessage( "COO - AccesOne", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsdescription = context.GetMessage( "\"I have had the opportunity to have DVelop as a technology partner through different companies in the last 5 years. Its people have always proven to be professionals with a remarkable technical capacity, committed to their company and its customers.\"", "");
         Gxm1sdthometestimonials = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(context);
         Gxm2rootcol.Add(Gxm1sdthometestimonials, 0);
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsname = context.GetMessage( "Daniel Pachón", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialscompany = context.GetMessage( "Distribuiton Channels Manager - Skandia", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsdescription = context.GetMessage( "\"Since 2014 we have found with DVelop the way to work under agile methodologies, as well as the correct balance in the proposal of initiatives that make our business grow.\"", "");
         Gxm1sdthometestimonials = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(context);
         Gxm2rootcol.Add(Gxm1sdthometestimonials, 0);
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsname = context.GetMessage( "Tomás Galdamez", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialscompany = context.GetMessage( "Applications Developer - Ricoh", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsdescription = context.GetMessage( "“Having DVelop helping us in such an important part of our business has been key to the success of our mobile workforce strategy. DVelop constantly demonstrates its high level of professionalism and commitment and is the main reason why today we continue to use the Genexus platform in several of our business solutions\".", "");
         Gxm1sdthometestimonials = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(context);
         Gxm2rootcol.Add(Gxm1sdthometestimonials, 0);
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsname = context.GetMessage( "Ignacio Galliazzi", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialscompany = context.GetMessage( "COO - AccesOne", "");
         Gxm1sdthometestimonials.gxTpr_Sdthometestimonialsdescription = context.GetMessage( "\"I have had the opportunity to have DVelop as a technology partner through different companies in the last 5 years. Its people have always proven to be professionals with a remarkable technical capacity, committed to their company and its customers.\"", "");
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
         Gxm1sdthometestimonials = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(context);
         /* GeneXus formulas. */
      }

      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem> Gxm2rootcol ;
      private DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem Gxm1sdthometestimonials ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeTestimonials_SDTHomeTestimonialsItem> aP0_Gxm2rootcol ;
   }

}
