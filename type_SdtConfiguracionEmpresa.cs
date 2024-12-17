using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   [XmlRoot(ElementName = "ConfiguracionEmpresa" )]
   [XmlType(TypeName =  "ConfiguracionEmpresa" , Namespace = "DesignSystem" )]
   [Serializable]
   public class SdtConfiguracionEmpresa : GxSilentTrnSdt
   {
      public SdtConfiguracionEmpresa( )
      {
      }

      public SdtConfiguracionEmpresa( IGxContext context )
      {
         this.context = context;
         constructorCallingAssembly = Assembly.GetEntryAssembly();
         initialize();
      }

      private static Hashtable mapper;
      public override string JsonMap( string value )
      {
         if ( mapper == null )
         {
            mapper = new Hashtable();
         }
         return (string)mapper[value]; ;
      }

      public void Load( short AV44ConfiguracionEmpresaId )
      {
         IGxSilentTrn obj;
         obj = getTransaction();
         obj.LoadKey(new Object[] {(short)AV44ConfiguracionEmpresaId});
         return  ;
      }

      public override Object[][] GetBCKey( )
      {
         return (Object[][])(new Object[][]{new Object[]{"ConfiguracionEmpresaId", typeof(short)}}) ;
      }

      public override GXProperties GetMetadata( )
      {
         GXProperties metadata = new GXProperties();
         metadata.Set("Name", "ConfiguracionEmpresa");
         metadata.Set("BT", "ConfiguracionEmpresa");
         metadata.Set("PK", "[ \"ConfiguracionEmpresaId\" ]");
         metadata.Set("PKAssigned", "[ \"ConfiguracionEmpresaId\" ]");
         metadata.Set("AllowInsert", "True");
         metadata.Set("AllowUpdate", "True");
         metadata.Set("AllowDelete", "True");
         return metadata ;
      }

      public override GeneXus.Utils.GxStringCollection StateAttributes( )
      {
         GeneXus.Utils.GxStringCollection state = new GeneXus.Utils.GxStringCollection();
         state.Add("gxTpr_Mode");
         state.Add("gxTpr_Initialized");
         state.Add("gxTpr_Configuracionempresaid_Z");
         state.Add("gxTpr_Configuracionempresatelefono_Z");
         state.Add("gxTpr_Configuracionempresacostoplanbasico_Z");
         state.Add("gxTpr_Configuracionempresacuotaplanbasico_Z");
         state.Add("gxTpr_Configuracionempresacostoplansuperior_Z");
         state.Add("gxTpr_Configuracionempresacuotaplansuperior_Z");
         state.Add("gxTpr_Configuracionempresacostoplannegocios_Z");
         state.Add("gxTpr_Configuracionempresacuotaplannegocios_Z");
         state.Add("gxTpr_Configuracionempresacostolandingpage_Z");
         state.Add("gxTpr_Configuracionempresacuotalandingpage_Z");
         return state ;
      }

      public override void Copy( GxUserType source )
      {
         SdtConfiguracionEmpresa sdt;
         sdt = (SdtConfiguracionEmpresa)(source);
         gxTv_SdtConfiguracionEmpresa_Configuracionempresaid = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresaid ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage ;
         gxTv_SdtConfiguracionEmpresa_Mode = sdt.gxTv_SdtConfiguracionEmpresa_Mode ;
         gxTv_SdtConfiguracionEmpresa_Initialized = sdt.gxTv_SdtConfiguracionEmpresa_Initialized ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z ;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z ;
         return  ;
      }

      public override void ToJSON( )
      {
         ToJSON( true) ;
         return  ;
      }

      public override void ToJSON( bool includeState )
      {
         ToJSON( includeState, true) ;
         return  ;
      }

      public override void ToJSON( bool includeState ,
                                   bool includeNonInitialized )
      {
         AddObjectProperty("ConfiguracionEmpresaId", gxTv_SdtConfiguracionEmpresa_Configuracionempresaid, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaTelefono", gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCostoPlanBasico", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCuotaPlanBasico", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCostoPlanSuperior", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCuotaPlanSuperior", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCostoPlanNegocios", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCuotaPlanNegocios", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCostoLandingPage", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage, false, includeNonInitialized);
         AddObjectProperty("ConfiguracionEmpresaCuotaLandingPage", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage, false, includeNonInitialized);
         if ( includeState )
         {
            AddObjectProperty("Mode", gxTv_SdtConfiguracionEmpresa_Mode, false, includeNonInitialized);
            AddObjectProperty("Initialized", gxTv_SdtConfiguracionEmpresa_Initialized, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaId_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaTelefono_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCostoPlanBasico_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCuotaPlanBasico_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCostoPlanSuperior_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCuotaPlanSuperior_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCostoPlanNegocios_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCuotaPlanNegocios_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCostoLandingPage_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z, false, includeNonInitialized);
            AddObjectProperty("ConfiguracionEmpresaCuotaLandingPage_Z", gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z, false, includeNonInitialized);
         }
         return  ;
      }

      public void UpdateDirties( SdtConfiguracionEmpresa sdt )
      {
         if ( sdt.IsDirty("ConfiguracionEmpresaId") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresaid = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresaid ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaTelefono") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCostoPlanBasico") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCuotaPlanBasico") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCostoPlanSuperior") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCuotaPlanSuperior") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCostoPlanNegocios") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCuotaPlanNegocios") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCostoLandingPage") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage ;
         }
         if ( sdt.IsDirty("ConfiguracionEmpresaCuotaLandingPage") )
         {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage = sdt.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage ;
         }
         return  ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaId" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaId"   )]
      public short gxTpr_Configuracionempresaid
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresaid ;
         }

         set {
            sdtIsNull = 0;
            if ( gxTv_SdtConfiguracionEmpresa_Configuracionempresaid != value )
            {
               gxTv_SdtConfiguracionEmpresa_Mode = "INS";
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z_SetNull( );
               this.gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z_SetNull( );
            }
            gxTv_SdtConfiguracionEmpresa_Configuracionempresaid = value;
            SetDirty("Configuracionempresaid");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaTelefono" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaTelefono"   )]
      public string gxTpr_Configuracionempresatelefono
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono = value;
            SetDirty("Configuracionempresatelefono");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoPlanBasico" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoPlanBasico"   )]
      public decimal gxTpr_Configuracionempresacostoplanbasico
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico = value;
            SetDirty("Configuracionempresacostoplanbasico");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaPlanBasico" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaPlanBasico"   )]
      public decimal gxTpr_Configuracionempresacuotaplanbasico
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico = value;
            SetDirty("Configuracionempresacuotaplanbasico");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoPlanSuperior" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoPlanSuperior"   )]
      public decimal gxTpr_Configuracionempresacostoplansuperior
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior = value;
            SetDirty("Configuracionempresacostoplansuperior");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaPlanSuperior" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaPlanSuperior"   )]
      public decimal gxTpr_Configuracionempresacuotaplansuperior
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior = value;
            SetDirty("Configuracionempresacuotaplansuperior");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoPlanNegocios" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoPlanNegocios"   )]
      public decimal gxTpr_Configuracionempresacostoplannegocios
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios = value;
            SetDirty("Configuracionempresacostoplannegocios");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaPlanNegocios" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaPlanNegocios"   )]
      public decimal gxTpr_Configuracionempresacuotaplannegocios
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios = value;
            SetDirty("Configuracionempresacuotaplannegocios");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoLandingPage" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoLandingPage"   )]
      public decimal gxTpr_Configuracionempresacostolandingpage
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage = value;
            SetDirty("Configuracionempresacostolandingpage");
         }

      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaLandingPage" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaLandingPage"   )]
      public decimal gxTpr_Configuracionempresacuotalandingpage
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage = value;
            SetDirty("Configuracionempresacuotalandingpage");
         }

      }

      [  SoapElement( ElementName = "Mode" )]
      [  XmlElement( ElementName = "Mode"   )]
      public string gxTpr_Mode
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Mode ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Mode = value;
            SetDirty("Mode");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Mode_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Mode = "";
         SetDirty("Mode");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Mode_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "Initialized" )]
      [  XmlElement( ElementName = "Initialized"   )]
      public short gxTpr_Initialized
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Initialized ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Initialized = value;
            SetDirty("Initialized");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Initialized_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Initialized = 0;
         SetDirty("Initialized");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Initialized_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaId_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaId_Z"   )]
      public short gxTpr_Configuracionempresaid_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z = value;
            SetDirty("Configuracionempresaid_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z = 0;
         SetDirty("Configuracionempresaid_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaTelefono_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaTelefono_Z"   )]
      public string gxTpr_Configuracionempresatelefono_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z = value;
            SetDirty("Configuracionempresatelefono_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z = "";
         SetDirty("Configuracionempresatelefono_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoPlanBasico_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoPlanBasico_Z"   )]
      public decimal gxTpr_Configuracionempresacostoplanbasico_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z = value;
            SetDirty("Configuracionempresacostoplanbasico_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z = 0;
         SetDirty("Configuracionempresacostoplanbasico_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaPlanBasico_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaPlanBasico_Z"   )]
      public decimal gxTpr_Configuracionempresacuotaplanbasico_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z = value;
            SetDirty("Configuracionempresacuotaplanbasico_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z = 0;
         SetDirty("Configuracionempresacuotaplanbasico_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoPlanSuperior_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoPlanSuperior_Z"   )]
      public decimal gxTpr_Configuracionempresacostoplansuperior_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z = value;
            SetDirty("Configuracionempresacostoplansuperior_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z = 0;
         SetDirty("Configuracionempresacostoplansuperior_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaPlanSuperior_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaPlanSuperior_Z"   )]
      public decimal gxTpr_Configuracionempresacuotaplansuperior_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z = value;
            SetDirty("Configuracionempresacuotaplansuperior_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z = 0;
         SetDirty("Configuracionempresacuotaplansuperior_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoPlanNegocios_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoPlanNegocios_Z"   )]
      public decimal gxTpr_Configuracionempresacostoplannegocios_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z = value;
            SetDirty("Configuracionempresacostoplannegocios_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z = 0;
         SetDirty("Configuracionempresacostoplannegocios_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaPlanNegocios_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaPlanNegocios_Z"   )]
      public decimal gxTpr_Configuracionempresacuotaplannegocios_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z = value;
            SetDirty("Configuracionempresacuotaplannegocios_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z = 0;
         SetDirty("Configuracionempresacuotaplannegocios_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCostoLandingPage_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCostoLandingPage_Z"   )]
      public decimal gxTpr_Configuracionempresacostolandingpage_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z = value;
            SetDirty("Configuracionempresacostolandingpage_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z = 0;
         SetDirty("Configuracionempresacostolandingpage_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "ConfiguracionEmpresaCuotaLandingPage_Z" )]
      [  XmlElement( ElementName = "ConfiguracionEmpresaCuotaLandingPage_Z"   )]
      public decimal gxTpr_Configuracionempresacuotalandingpage_Z
      {
         get {
            return gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z = value;
            SetDirty("Configuracionempresacuotalandingpage_Z");
         }

      }

      public void gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z_SetNull( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z = 0;
         SetDirty("Configuracionempresacuotalandingpage_Z");
         return  ;
      }

      public bool gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z_IsNull( )
      {
         return false ;
      }

      [XmlIgnore]
      private static GXTypeInfo _typeProps;
      protected override GXTypeInfo TypeInfo
      {
         get {
            return _typeProps ;
         }

         set {
            _typeProps = value ;
         }

      }

      public void initialize( )
      {
         gxTv_SdtConfiguracionEmpresa_Configuracionempresaid = 1;
         sdtIsNull = 1;
         gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono = "";
         gxTv_SdtConfiguracionEmpresa_Mode = "";
         gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z = "";
         IGxSilentTrn obj;
         obj = (IGxSilentTrn)ClassLoader.FindInstance( "configuracionempresa", "DesignSystem.Programs.configuracionempresa_bc", new Object[] {context}, constructorCallingAssembly);;
         obj.initialize();
         obj.SetSDT(this, 1);
         setTransaction( obj) ;
         obj.SetMode("INS");
         return  ;
      }

      public short isNull( )
      {
         return sdtIsNull ;
      }

      private short gxTv_SdtConfiguracionEmpresa_Configuracionempresaid ;
      private short sdtIsNull ;
      private short gxTv_SdtConfiguracionEmpresa_Initialized ;
      private short gxTv_SdtConfiguracionEmpresa_Configuracionempresaid_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplanbasico_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplanbasico_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplansuperior_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplansuperior_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostoplannegocios_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotaplannegocios_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacostolandingpage_Z ;
      private decimal gxTv_SdtConfiguracionEmpresa_Configuracionempresacuotalandingpage_Z ;
      private string gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono ;
      private string gxTv_SdtConfiguracionEmpresa_Mode ;
      private string gxTv_SdtConfiguracionEmpresa_Configuracionempresatelefono_Z ;
   }

   [DataContract(Name = @"ConfiguracionEmpresa", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtConfiguracionEmpresa_RESTInterface : GxGenericCollectionItem<SdtConfiguracionEmpresa>
   {
      public SdtConfiguracionEmpresa_RESTInterface( ) : base()
      {
      }

      public SdtConfiguracionEmpresa_RESTInterface( SdtConfiguracionEmpresa psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "ConfiguracionEmpresaId" , Order = 0 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Configuracionempresaid
      {
         get {
            return sdt.gxTpr_Configuracionempresaid ;
         }

         set {
            sdt.gxTpr_Configuracionempresaid = (short)(value.HasValue ? value.Value : 0);
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaTelefono" , Order = 1 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresatelefono
      {
         get {
            return StringUtil.RTrim( sdt.gxTpr_Configuracionempresatelefono) ;
         }

         set {
            sdt.gxTpr_Configuracionempresatelefono = value;
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCostoPlanBasico" , Order = 2 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacostoplanbasico
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacostoplanbasico, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacostoplanbasico = NumberUtil.Val( value, ".");
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCuotaPlanBasico" , Order = 3 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacuotaplanbasico
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacuotaplanbasico, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacuotaplanbasico = NumberUtil.Val( value, ".");
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCostoPlanSuperior" , Order = 4 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacostoplansuperior
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacostoplansuperior, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacostoplansuperior = NumberUtil.Val( value, ".");
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCuotaPlanSuperior" , Order = 5 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacuotaplansuperior
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacuotaplansuperior, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacuotaplansuperior = NumberUtil.Val( value, ".");
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCostoPlanNegocios" , Order = 6 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacostoplannegocios
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacostoplannegocios, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacostoplannegocios = NumberUtil.Val( value, ".");
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCuotaPlanNegocios" , Order = 7 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacuotaplannegocios
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacuotaplannegocios, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacuotaplannegocios = NumberUtil.Val( value, ".");
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCostoLandingPage" , Order = 8 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacostolandingpage
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacostolandingpage, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacostolandingpage = NumberUtil.Val( value, ".");
         }

      }

      [DataMember( Name = "ConfiguracionEmpresaCuotaLandingPage" , Order = 9 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresacuotalandingpage
      {
         get {
            return StringUtil.LTrim( StringUtil.Str( sdt.gxTpr_Configuracionempresacuotalandingpage, 12, 2)) ;
         }

         set {
            sdt.gxTpr_Configuracionempresacuotalandingpage = NumberUtil.Val( value, ".");
         }

      }

      public SdtConfiguracionEmpresa sdt
      {
         get {
            return (SdtConfiguracionEmpresa)Sdt ;
         }

         set {
            Sdt = value ;
         }

      }

      [OnDeserializing]
      void checkSdt( StreamingContext ctx )
      {
         if ( sdt == null )
         {
            sdt = new SdtConfiguracionEmpresa() ;
         }
      }

      [DataMember( Name = "gx_md5_hash", Order = 10 )]
      public string Hash
      {
         get {
            if ( StringUtil.StrCmp(md5Hash, null) == 0 )
            {
               md5Hash = (string)(getHash());
            }
            return md5Hash ;
         }

         set {
            md5Hash = value ;
         }

      }

      private string md5Hash ;
   }

   [DataContract(Name = @"ConfiguracionEmpresa", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtConfiguracionEmpresa_RESTLInterface : GxGenericCollectionItem<SdtConfiguracionEmpresa>
   {
      public SdtConfiguracionEmpresa_RESTLInterface( ) : base()
      {
      }

      public SdtConfiguracionEmpresa_RESTLInterface( SdtConfiguracionEmpresa psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "ConfiguracionEmpresaTelefono" , Order = 0 )]
      [GxSeudo()]
      public string gxTpr_Configuracionempresatelefono
      {
         get {
            return StringUtil.RTrim( sdt.gxTpr_Configuracionempresatelefono) ;
         }

         set {
            sdt.gxTpr_Configuracionempresatelefono = value;
         }

      }

      [DataMember( Name = "uri", Order = 1 )]
      public string Uri
      {
         get {
            return "" ;
         }

         set {
         }

      }

      public SdtConfiguracionEmpresa sdt
      {
         get {
            return (SdtConfiguracionEmpresa)Sdt ;
         }

         set {
            Sdt = value ;
         }

      }

      [OnDeserializing]
      void checkSdt( StreamingContext ctx )
      {
         if ( sdt == null )
         {
            sdt = new SdtConfiguracionEmpresa() ;
         }
      }

   }

}
