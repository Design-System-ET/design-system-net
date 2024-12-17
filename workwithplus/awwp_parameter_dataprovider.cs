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
namespace DesignSystem.Programs.workwithplus {
   public class awwp_parameter_dataprovider : GXProcedure
   {
      public static int Main( string[] args )
      {
         return new workwithplus.awwp_parameter_dataprovider().MainImpl(args); ;
      }

      public int executeCmdLine( string[] args )
      {
         return ExecuteCmdLine(args); ;
      }

      protected override int ExecuteCmdLine( string[] args )
      {
         GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> aP0_Gxm2rootcol = new GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>()  ;
         execute(out aP0_Gxm2rootcol);
         return GX.GXRuntime.ExitCode ;
      }

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

      public awwp_parameter_dataprovider( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public awwp_parameter_dataprovider( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>( context, "WWP_Parameter", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>( context, "WWP_Parameter", "DesignSystem") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_Name";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application name";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "WorkWithPlus";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_Phone";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application phone";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "+1 550 8923";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_Mail";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application mail";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "info@mail.com";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_Website";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application website";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "http://www.web.com";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_Address";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application address";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "French Boulevard 2859";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_Neighbour";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application neighbour";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "Downtown";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_CityAndCountry";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application city and country";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "Paris, France";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         Gxm2rootcol.Add(Gxm1wwp_parameter, 0);
         Gxm1wwp_parameter.gxTpr_Wwpparameterkey = "AD_Application_Footer";
         Gxm1wwp_parameter.gxTpr_Wwpparametercategory = "Application data";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdescription = "Application footer";
         Gxm1wwp_parameter.gxTpr_Wwpparametervalue = "WorkWithPlus for Web - copyright";
         Gxm1wwp_parameter.gxTpr_Wwpparameterdisabledelete = true;
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
         Gxm1wwp_parameter = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         /* GeneXus formulas. */
      }

      private GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> Gxm2rootcol ;
      private DesignSystem.Programs.workwithplus.SdtWWP_Parameter Gxm1wwp_parameter ;
      private GXBCCollection<DesignSystem.Programs.workwithplus.SdtWWP_Parameter> aP0_Gxm2rootcol ;
   }

}
