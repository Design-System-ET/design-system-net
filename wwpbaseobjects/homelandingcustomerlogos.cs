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
   public class homelandingcustomerlogos : GXProcedure
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

      public homelandingcustomerlogos( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public homelandingcustomerlogos( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem>( context, "SDTHomeLandingSampleLogosItem", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem>( context, "SDTHomeLandingSampleLogosItem", "DesignSystem") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "1e9ce4cc-c2b1-409d-a4ae-f043cabb7386", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "e73f4800-a7cc-4737-b8fd-3b69a2ba5e49", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "a2e531e5-0116-4d22-8745-52dfb0163e34", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "593d2cdf-6319-44df-b510-93f7cd545c56", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "40120363-2972-45d9-9e27-968b97f881b1", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "32c3b586-0f02-4246-b673-54ce0a72b7d0", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "f1be7bfe-5af9-4a9f-8931-079ac08d6fcc", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "5231b8e2-8573-41cd-80a0-a3f8bf6e16d4", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "9e1e5682-b5d8-4032-80d3-92448e28eff4", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "10a9035f-850f-401a-8c94-c724c8aa525c", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "e7af7def-c650-4146-a688-a6f865083e66", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "84afd878-0348-4d35-b5fe-dd475143e66d", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "10a9035f-850f-401a-8c94-c724c8aa525c", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "a8626400-abe5-4188-85d7-ae2f79bf3969", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "f1be7bfe-5af9-4a9f-8931-079ac08d6fcc", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "065ef6be-d168-469d-8628-6db04fdb47cf", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "eef6214d-c9e0-4886-bee4-a80f44c659c0", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "5231b8e2-8573-41cd-80a0-a3f8bf6e16d4", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "e7af7def-c650-4146-a688-a6f865083e66", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "55402a3b-70c1-4d17-8846-19b37dff7e2b", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "84afd878-0348-4d35-b5fe-dd475143e66d", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "8665dbfa-aa61-4660-a101-2a451b96ea9b", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "9e1e5682-b5d8-4032-80d3-92448e28eff4", "", context.GetTheme( ))));
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         Gxm2rootcol.Add(Gxm1sdthomelandingsamplelogos, 0);
         Gxm1sdthomelandingsamplelogos.gxTpr_Logo = context.convertURL( (string)(context.GetImagePath( "90c36231-f6db-45f2-a916-04cb469820a5", "", context.GetTheme( ))));
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
         Gxm1sdthomelandingsamplelogos = new DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(context);
         /* GeneXus formulas. */
      }

      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> Gxm2rootcol ;
      private DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem Gxm1sdthomelandingsamplelogos ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem> aP0_Gxm2rootcol ;
   }

}
