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
   public class landingpurusstudiosamples : GXProcedure
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

      public landingpurusstudiosamples( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public landingpurusstudiosamples( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem>( context, "SDTLandingPurusStudiosItem", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem> executeUdp( )
      {
         execute(out aP0_Gxm2rootcol);
         return Gxm2rootcol ;
      }

      public void executeSubmit( out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem> aP0_Gxm2rootcol )
      {
         this.Gxm2rootcol = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem>( context, "SDTLandingPurusStudiosItem", "DesignSystem") ;
         SubmitImpl();
         aP0_Gxm2rootcol=this.Gxm2rootcol;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "4f34a868-a429-4e5a-8793-1f91da6c6370", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "Genexus", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Creación de aplicaciones empresariales mucho mas rapido.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "67df3af3-7c84-4989-8244-3bf237d5dd48", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "WWP Web", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Potente herramienta para diseño web.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "f2ccbd80-cdd3-4f66-affc-c7f86c0089ac", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "WWP SD", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Potente herramienta para desarrollo Smart Device", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "f0f959b8-73e6-4b2f-8ea3-55362954b3ee", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "Java", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Utilizado en aplicaciones web, móviles y de servidor.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "44961ca1-8343-4dea-95b4-e89a7eae7425", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "Spring Boot", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Configuración automática y una estructura de proyecto lista para usar.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "7d077d7e-41e1-44a7-8f44-bcd95516cf49", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "Python", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Usado en desarrollo web, análisis de datos, inteligencia artificial y automatización.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "5e3f8de8-82fa-46d5-80d7-5157b6c3e541", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "HTML", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Lenguaje de marcado que estructura el contenido de las páginas web.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "8e2de0d7-6e70-4199-8b78-a195cd0a609e", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "CSS", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Permite el diseño y la maquetación de páginas web con precisión.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "babb0aea-316a-40be-9eea-c1285a39bbc8", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = "JavaScript";
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Permite la creación de paginas web dinámicas e interactivas.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "9d014ab9-b628-4d86-93fc-9cebc74e3ea1", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "C#", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Ideal para aplicaciones en Windows, juegos y sistemas empresariales robustos.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "cad694b4-df53-46af-a6e4-3d6e881462bf", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "Android", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Extiende tu presencia y productividad con desarrollo mobil.", "");
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         Gxm2rootcol.Add(Gxm1sdtlandingpurusstudios, 0);
         Gxm1sdtlandingpurusstudios.gxTpr_Studioimage = context.convertURL( (string)(context.GetImagePath( "3430e38a-6dd5-4122-9b38-aee965d9bf6f", "", context.GetTheme( ))));
         Gxm1sdtlandingpurusstudios.gxTpr_Studiotitle = context.GetMessage( "Bases de Datos", "");
         Gxm1sdtlandingpurusstudios.gxTpr_Studiodescription = context.GetMessage( "Diferentes Bases de Datos como MySQL, SQL, MongoDB y otras", "");
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
         Gxm1sdtlandingpurusstudios = new DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(context);
         /* GeneXus formulas. */
      }

      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem> Gxm2rootcol ;
      private DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem Gxm1sdtlandingpurusstudios ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem> aP0_Gxm2rootcol ;
   }

}
