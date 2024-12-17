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
   [XmlRoot(ElementName = "NewProductos" )]
   [XmlType(TypeName =  "NewProductos" , Namespace = "DesignSystem" )]
   [Serializable]
   public class SdtNewProductos : GxSilentTrnSdt
   {
      public SdtNewProductos( )
      {
      }

      public SdtNewProductos( IGxContext context )
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

      public void Load( short AV34NewProductosId )
      {
         IGxSilentTrn obj;
         obj = getTransaction();
         obj.LoadKey(new Object[] {(short)AV34NewProductosId});
         return  ;
      }

      public override Object[][] GetBCKey( )
      {
         return (Object[][])(new Object[][]{new Object[]{"NewProductosId", typeof(short)}}) ;
      }

      public override GXProperties GetMetadata( )
      {
         GXProperties metadata = new GXProperties();
         metadata.Set("Name", "NewProductos");
         metadata.Set("BT", "NewProductos");
         metadata.Set("PK", "[ \"NewProductosId\" ]");
         metadata.Set("PKAssigned", "[ \"NewProductosId\" ]");
         metadata.Set("FKList", "[ { \"FK\":[ \"CategoriasId\" ],\"FKMap\":[  ] } ]");
         metadata.Set("AllowInsert", "True");
         metadata.Set("AllowUpdate", "True");
         metadata.Set("AllowDelete", "True");
         return metadata ;
      }

      public override GeneXus.Utils.GxStringCollection StateAttributes( )
      {
         GeneXus.Utils.GxStringCollection state = new GeneXus.Utils.GxStringCollection();
         state.Add("gxTpr_Newproductosimagen_gxi");
         state.Add("gxTpr_Mode");
         state.Add("gxTpr_Initialized");
         state.Add("gxTpr_Newproductosid_Z");
         state.Add("gxTpr_Newproductosnombre_Z");
         state.Add("gxTpr_Newproductosdescripcioncorta_Z");
         state.Add("gxTpr_Newproductosnumerodescargas_Z");
         state.Add("gxTpr_Newproductoslinkdescargademo_Z");
         state.Add("gxTpr_Newproductoscomprar_Z");
         state.Add("gxTpr_Newproductosnumeroventas_Z");
         state.Add("gxTpr_Newproductosvisitas_Z");
         state.Add("gxTpr_Categoriasid_Z");
         state.Add("gxTpr_Newproductosimagen_gxi_Z");
         return state ;
      }

      public override void Copy( GxUserType source )
      {
         SdtNewProductos sdt;
         sdt = (SdtNewProductos)(source);
         gxTv_SdtNewProductos_Newproductosid = sdt.gxTv_SdtNewProductos_Newproductosid ;
         gxTv_SdtNewProductos_Newproductosimagen = sdt.gxTv_SdtNewProductos_Newproductosimagen ;
         gxTv_SdtNewProductos_Newproductosimagen_gxi = sdt.gxTv_SdtNewProductos_Newproductosimagen_gxi ;
         gxTv_SdtNewProductos_Newproductosnombre = sdt.gxTv_SdtNewProductos_Newproductosnombre ;
         gxTv_SdtNewProductos_Newproductosdescripcioncorta = sdt.gxTv_SdtNewProductos_Newproductosdescripcioncorta ;
         gxTv_SdtNewProductos_Newproductosdescripcion = sdt.gxTv_SdtNewProductos_Newproductosdescripcion ;
         gxTv_SdtNewProductos_Newproductosnumerodescargas = sdt.gxTv_SdtNewProductos_Newproductosnumerodescargas ;
         gxTv_SdtNewProductos_Newproductoslinkdescargademo = sdt.gxTv_SdtNewProductos_Newproductoslinkdescargademo ;
         gxTv_SdtNewProductos_Newproductoscomprar = sdt.gxTv_SdtNewProductos_Newproductoscomprar ;
         gxTv_SdtNewProductos_Newproductosnumeroventas = sdt.gxTv_SdtNewProductos_Newproductosnumeroventas ;
         gxTv_SdtNewProductos_Newproductosvisitas = sdt.gxTv_SdtNewProductos_Newproductosvisitas ;
         gxTv_SdtNewProductos_Categoriasid = sdt.gxTv_SdtNewProductos_Categoriasid ;
         gxTv_SdtNewProductos_Mode = sdt.gxTv_SdtNewProductos_Mode ;
         gxTv_SdtNewProductos_Initialized = sdt.gxTv_SdtNewProductos_Initialized ;
         gxTv_SdtNewProductos_Newproductosid_Z = sdt.gxTv_SdtNewProductos_Newproductosid_Z ;
         gxTv_SdtNewProductos_Newproductosnombre_Z = sdt.gxTv_SdtNewProductos_Newproductosnombre_Z ;
         gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z = sdt.gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z ;
         gxTv_SdtNewProductos_Newproductosnumerodescargas_Z = sdt.gxTv_SdtNewProductos_Newproductosnumerodescargas_Z ;
         gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z = sdt.gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z ;
         gxTv_SdtNewProductos_Newproductoscomprar_Z = sdt.gxTv_SdtNewProductos_Newproductoscomprar_Z ;
         gxTv_SdtNewProductos_Newproductosnumeroventas_Z = sdt.gxTv_SdtNewProductos_Newproductosnumeroventas_Z ;
         gxTv_SdtNewProductos_Newproductosvisitas_Z = sdt.gxTv_SdtNewProductos_Newproductosvisitas_Z ;
         gxTv_SdtNewProductos_Categoriasid_Z = sdt.gxTv_SdtNewProductos_Categoriasid_Z ;
         gxTv_SdtNewProductos_Newproductosimagen_gxi_Z = sdt.gxTv_SdtNewProductos_Newproductosimagen_gxi_Z ;
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
         AddObjectProperty("NewProductosId", gxTv_SdtNewProductos_Newproductosid, false, includeNonInitialized);
         AddObjectProperty("NewProductosImagen", gxTv_SdtNewProductos_Newproductosimagen, false, includeNonInitialized);
         AddObjectProperty("NewProductosNombre", gxTv_SdtNewProductos_Newproductosnombre, false, includeNonInitialized);
         AddObjectProperty("NewProductosDescripcionCorta", gxTv_SdtNewProductos_Newproductosdescripcioncorta, false, includeNonInitialized);
         AddObjectProperty("NewProductosDescripcion", gxTv_SdtNewProductos_Newproductosdescripcion, false, includeNonInitialized);
         AddObjectProperty("NewProductosNumeroDescargas", gxTv_SdtNewProductos_Newproductosnumerodescargas, false, includeNonInitialized);
         AddObjectProperty("NewProductosLinkDescargaDemo", gxTv_SdtNewProductos_Newproductoslinkdescargademo, false, includeNonInitialized);
         AddObjectProperty("NewProductosComprar", gxTv_SdtNewProductos_Newproductoscomprar, false, includeNonInitialized);
         AddObjectProperty("NewProductosNumeroVentas", gxTv_SdtNewProductos_Newproductosnumeroventas, false, includeNonInitialized);
         AddObjectProperty("NewProductosVisitas", gxTv_SdtNewProductos_Newproductosvisitas, false, includeNonInitialized);
         AddObjectProperty("CategoriasId", gxTv_SdtNewProductos_Categoriasid, false, includeNonInitialized);
         if ( includeState )
         {
            AddObjectProperty("NewProductosImagen_GXI", gxTv_SdtNewProductos_Newproductosimagen_gxi, false, includeNonInitialized);
            AddObjectProperty("Mode", gxTv_SdtNewProductos_Mode, false, includeNonInitialized);
            AddObjectProperty("Initialized", gxTv_SdtNewProductos_Initialized, false, includeNonInitialized);
            AddObjectProperty("NewProductosId_Z", gxTv_SdtNewProductos_Newproductosid_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosNombre_Z", gxTv_SdtNewProductos_Newproductosnombre_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosDescripcionCorta_Z", gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosNumeroDescargas_Z", gxTv_SdtNewProductos_Newproductosnumerodescargas_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosLinkDescargaDemo_Z", gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosComprar_Z", gxTv_SdtNewProductos_Newproductoscomprar_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosNumeroVentas_Z", gxTv_SdtNewProductos_Newproductosnumeroventas_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosVisitas_Z", gxTv_SdtNewProductos_Newproductosvisitas_Z, false, includeNonInitialized);
            AddObjectProperty("CategoriasId_Z", gxTv_SdtNewProductos_Categoriasid_Z, false, includeNonInitialized);
            AddObjectProperty("NewProductosImagen_GXI_Z", gxTv_SdtNewProductos_Newproductosimagen_gxi_Z, false, includeNonInitialized);
         }
         return  ;
      }

      public void UpdateDirties( SdtNewProductos sdt )
      {
         if ( sdt.IsDirty("NewProductosId") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosid = sdt.gxTv_SdtNewProductos_Newproductosid ;
         }
         if ( sdt.IsDirty("NewProductosImagen") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosimagen = sdt.gxTv_SdtNewProductos_Newproductosimagen ;
         }
         if ( sdt.IsDirty("NewProductosImagen") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosimagen_gxi = sdt.gxTv_SdtNewProductos_Newproductosimagen_gxi ;
         }
         if ( sdt.IsDirty("NewProductosNombre") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnombre = sdt.gxTv_SdtNewProductos_Newproductosnombre ;
         }
         if ( sdt.IsDirty("NewProductosDescripcionCorta") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosdescripcioncorta = sdt.gxTv_SdtNewProductos_Newproductosdescripcioncorta ;
         }
         if ( sdt.IsDirty("NewProductosDescripcion") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosdescripcion = sdt.gxTv_SdtNewProductos_Newproductosdescripcion ;
         }
         if ( sdt.IsDirty("NewProductosNumeroDescargas") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnumerodescargas = sdt.gxTv_SdtNewProductos_Newproductosnumerodescargas ;
         }
         if ( sdt.IsDirty("NewProductosLinkDescargaDemo") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductoslinkdescargademo = sdt.gxTv_SdtNewProductos_Newproductoslinkdescargademo ;
         }
         if ( sdt.IsDirty("NewProductosComprar") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductoscomprar = sdt.gxTv_SdtNewProductos_Newproductoscomprar ;
         }
         if ( sdt.IsDirty("NewProductosNumeroVentas") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnumeroventas = sdt.gxTv_SdtNewProductos_Newproductosnumeroventas ;
         }
         if ( sdt.IsDirty("NewProductosVisitas") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosvisitas = sdt.gxTv_SdtNewProductos_Newproductosvisitas ;
         }
         if ( sdt.IsDirty("CategoriasId") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Categoriasid = sdt.gxTv_SdtNewProductos_Categoriasid ;
         }
         return  ;
      }

      [  SoapElement( ElementName = "NewProductosId" )]
      [  XmlElement( ElementName = "NewProductosId"   )]
      public short gxTpr_Newproductosid
      {
         get {
            return gxTv_SdtNewProductos_Newproductosid ;
         }

         set {
            sdtIsNull = 0;
            if ( gxTv_SdtNewProductos_Newproductosid != value )
            {
               gxTv_SdtNewProductos_Mode = "INS";
               this.gxTv_SdtNewProductos_Newproductosid_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductosnombre_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductosnumerodescargas_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductoscomprar_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductosnumeroventas_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductosvisitas_Z_SetNull( );
               this.gxTv_SdtNewProductos_Categoriasid_Z_SetNull( );
               this.gxTv_SdtNewProductos_Newproductosimagen_gxi_Z_SetNull( );
            }
            gxTv_SdtNewProductos_Newproductosid = value;
            SetDirty("Newproductosid");
         }

      }

      [  SoapElement( ElementName = "NewProductosImagen" )]
      [  XmlElement( ElementName = "NewProductosImagen"   )]
      [GxUpload()]
      public string gxTpr_Newproductosimagen
      {
         get {
            return gxTv_SdtNewProductos_Newproductosimagen ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosimagen = value;
            SetDirty("Newproductosimagen");
         }

      }

      [  SoapElement( ElementName = "NewProductosImagen_GXI" )]
      [  XmlElement( ElementName = "NewProductosImagen_GXI"   )]
      public string gxTpr_Newproductosimagen_gxi
      {
         get {
            return gxTv_SdtNewProductos_Newproductosimagen_gxi ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosimagen_gxi = value;
            SetDirty("Newproductosimagen_gxi");
         }

      }

      [  SoapElement( ElementName = "NewProductosNombre" )]
      [  XmlElement( ElementName = "NewProductosNombre"   )]
      public string gxTpr_Newproductosnombre
      {
         get {
            return gxTv_SdtNewProductos_Newproductosnombre ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnombre = value;
            SetDirty("Newproductosnombre");
         }

      }

      [  SoapElement( ElementName = "NewProductosDescripcionCorta" )]
      [  XmlElement( ElementName = "NewProductosDescripcionCorta"   )]
      public string gxTpr_Newproductosdescripcioncorta
      {
         get {
            return gxTv_SdtNewProductos_Newproductosdescripcioncorta ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosdescripcioncorta = value;
            SetDirty("Newproductosdescripcioncorta");
         }

      }

      [  SoapElement( ElementName = "NewProductosDescripcion" )]
      [  XmlElement( ElementName = "NewProductosDescripcion"   )]
      public string gxTpr_Newproductosdescripcion
      {
         get {
            return gxTv_SdtNewProductos_Newproductosdescripcion ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosdescripcion = value;
            SetDirty("Newproductosdescripcion");
         }

      }

      [  SoapElement( ElementName = "NewProductosNumeroDescargas" )]
      [  XmlElement( ElementName = "NewProductosNumeroDescargas"   )]
      public short gxTpr_Newproductosnumerodescargas
      {
         get {
            return gxTv_SdtNewProductos_Newproductosnumerodescargas ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnumerodescargas = value;
            SetDirty("Newproductosnumerodescargas");
         }

      }

      [  SoapElement( ElementName = "NewProductosLinkDescargaDemo" )]
      [  XmlElement( ElementName = "NewProductosLinkDescargaDemo"   )]
      public string gxTpr_Newproductoslinkdescargademo
      {
         get {
            return gxTv_SdtNewProductos_Newproductoslinkdescargademo ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductoslinkdescargademo = value;
            SetDirty("Newproductoslinkdescargademo");
         }

      }

      [  SoapElement( ElementName = "NewProductosComprar" )]
      [  XmlElement( ElementName = "NewProductosComprar"   )]
      public string gxTpr_Newproductoscomprar
      {
         get {
            return gxTv_SdtNewProductos_Newproductoscomprar ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductoscomprar = value;
            SetDirty("Newproductoscomprar");
         }

      }

      [  SoapElement( ElementName = "NewProductosNumeroVentas" )]
      [  XmlElement( ElementName = "NewProductosNumeroVentas"   )]
      public short gxTpr_Newproductosnumeroventas
      {
         get {
            return gxTv_SdtNewProductos_Newproductosnumeroventas ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnumeroventas = value;
            SetDirty("Newproductosnumeroventas");
         }

      }

      [  SoapElement( ElementName = "NewProductosVisitas" )]
      [  XmlElement( ElementName = "NewProductosVisitas"   )]
      public short gxTpr_Newproductosvisitas
      {
         get {
            return gxTv_SdtNewProductos_Newproductosvisitas ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosvisitas = value;
            SetDirty("Newproductosvisitas");
         }

      }

      [  SoapElement( ElementName = "CategoriasId" )]
      [  XmlElement( ElementName = "CategoriasId"   )]
      public short gxTpr_Categoriasid
      {
         get {
            return gxTv_SdtNewProductos_Categoriasid ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Categoriasid = value;
            SetDirty("Categoriasid");
         }

      }

      [  SoapElement( ElementName = "Mode" )]
      [  XmlElement( ElementName = "Mode"   )]
      public string gxTpr_Mode
      {
         get {
            return gxTv_SdtNewProductos_Mode ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Mode = value;
            SetDirty("Mode");
         }

      }

      public void gxTv_SdtNewProductos_Mode_SetNull( )
      {
         gxTv_SdtNewProductos_Mode = "";
         SetDirty("Mode");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Mode_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "Initialized" )]
      [  XmlElement( ElementName = "Initialized"   )]
      public short gxTpr_Initialized
      {
         get {
            return gxTv_SdtNewProductos_Initialized ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Initialized = value;
            SetDirty("Initialized");
         }

      }

      public void gxTv_SdtNewProductos_Initialized_SetNull( )
      {
         gxTv_SdtNewProductos_Initialized = 0;
         SetDirty("Initialized");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Initialized_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosId_Z" )]
      [  XmlElement( ElementName = "NewProductosId_Z"   )]
      public short gxTpr_Newproductosid_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductosid_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosid_Z = value;
            SetDirty("Newproductosid_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductosid_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductosid_Z = 0;
         SetDirty("Newproductosid_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductosid_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosNombre_Z" )]
      [  XmlElement( ElementName = "NewProductosNombre_Z"   )]
      public string gxTpr_Newproductosnombre_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductosnombre_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnombre_Z = value;
            SetDirty("Newproductosnombre_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductosnombre_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductosnombre_Z = "";
         SetDirty("Newproductosnombre_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductosnombre_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosDescripcionCorta_Z" )]
      [  XmlElement( ElementName = "NewProductosDescripcionCorta_Z"   )]
      public string gxTpr_Newproductosdescripcioncorta_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z = value;
            SetDirty("Newproductosdescripcioncorta_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z = "";
         SetDirty("Newproductosdescripcioncorta_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosNumeroDescargas_Z" )]
      [  XmlElement( ElementName = "NewProductosNumeroDescargas_Z"   )]
      public short gxTpr_Newproductosnumerodescargas_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductosnumerodescargas_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnumerodescargas_Z = value;
            SetDirty("Newproductosnumerodescargas_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductosnumerodescargas_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductosnumerodescargas_Z = 0;
         SetDirty("Newproductosnumerodescargas_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductosnumerodescargas_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosLinkDescargaDemo_Z" )]
      [  XmlElement( ElementName = "NewProductosLinkDescargaDemo_Z"   )]
      public string gxTpr_Newproductoslinkdescargademo_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z = value;
            SetDirty("Newproductoslinkdescargademo_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z = "";
         SetDirty("Newproductoslinkdescargademo_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosComprar_Z" )]
      [  XmlElement( ElementName = "NewProductosComprar_Z"   )]
      public string gxTpr_Newproductoscomprar_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductoscomprar_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductoscomprar_Z = value;
            SetDirty("Newproductoscomprar_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductoscomprar_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductoscomprar_Z = "";
         SetDirty("Newproductoscomprar_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductoscomprar_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosNumeroVentas_Z" )]
      [  XmlElement( ElementName = "NewProductosNumeroVentas_Z"   )]
      public short gxTpr_Newproductosnumeroventas_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductosnumeroventas_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosnumeroventas_Z = value;
            SetDirty("Newproductosnumeroventas_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductosnumeroventas_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductosnumeroventas_Z = 0;
         SetDirty("Newproductosnumeroventas_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductosnumeroventas_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosVisitas_Z" )]
      [  XmlElement( ElementName = "NewProductosVisitas_Z"   )]
      public short gxTpr_Newproductosvisitas_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductosvisitas_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosvisitas_Z = value;
            SetDirty("Newproductosvisitas_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductosvisitas_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductosvisitas_Z = 0;
         SetDirty("Newproductosvisitas_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductosvisitas_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "CategoriasId_Z" )]
      [  XmlElement( ElementName = "CategoriasId_Z"   )]
      public short gxTpr_Categoriasid_Z
      {
         get {
            return gxTv_SdtNewProductos_Categoriasid_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Categoriasid_Z = value;
            SetDirty("Categoriasid_Z");
         }

      }

      public void gxTv_SdtNewProductos_Categoriasid_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Categoriasid_Z = 0;
         SetDirty("Categoriasid_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Categoriasid_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewProductosImagen_GXI_Z" )]
      [  XmlElement( ElementName = "NewProductosImagen_GXI_Z"   )]
      public string gxTpr_Newproductosimagen_gxi_Z
      {
         get {
            return gxTv_SdtNewProductos_Newproductosimagen_gxi_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewProductos_Newproductosimagen_gxi_Z = value;
            SetDirty("Newproductosimagen_gxi_Z");
         }

      }

      public void gxTv_SdtNewProductos_Newproductosimagen_gxi_Z_SetNull( )
      {
         gxTv_SdtNewProductos_Newproductosimagen_gxi_Z = "";
         SetDirty("Newproductosimagen_gxi_Z");
         return  ;
      }

      public bool gxTv_SdtNewProductos_Newproductosimagen_gxi_Z_IsNull( )
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
         sdtIsNull = 1;
         gxTv_SdtNewProductos_Newproductosimagen = "";
         gxTv_SdtNewProductos_Newproductosimagen_gxi = "";
         gxTv_SdtNewProductos_Newproductosnombre = "";
         gxTv_SdtNewProductos_Newproductosdescripcioncorta = "";
         gxTv_SdtNewProductos_Newproductosdescripcion = "";
         gxTv_SdtNewProductos_Newproductoslinkdescargademo = "";
         gxTv_SdtNewProductos_Newproductoscomprar = "";
         gxTv_SdtNewProductos_Mode = "";
         gxTv_SdtNewProductos_Newproductosnombre_Z = "";
         gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z = "";
         gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z = "";
         gxTv_SdtNewProductos_Newproductoscomprar_Z = "";
         gxTv_SdtNewProductos_Newproductosimagen_gxi_Z = "";
         IGxSilentTrn obj;
         obj = (IGxSilentTrn)ClassLoader.FindInstance( "newproductos", "DesignSystem.Programs.newproductos_bc", new Object[] {context}, constructorCallingAssembly);;
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

      private short gxTv_SdtNewProductos_Newproductosid ;
      private short sdtIsNull ;
      private short gxTv_SdtNewProductos_Newproductosnumerodescargas ;
      private short gxTv_SdtNewProductos_Newproductosnumeroventas ;
      private short gxTv_SdtNewProductos_Newproductosvisitas ;
      private short gxTv_SdtNewProductos_Categoriasid ;
      private short gxTv_SdtNewProductos_Initialized ;
      private short gxTv_SdtNewProductos_Newproductosid_Z ;
      private short gxTv_SdtNewProductos_Newproductosnumerodescargas_Z ;
      private short gxTv_SdtNewProductos_Newproductosnumeroventas_Z ;
      private short gxTv_SdtNewProductos_Newproductosvisitas_Z ;
      private short gxTv_SdtNewProductos_Categoriasid_Z ;
      private string gxTv_SdtNewProductos_Mode ;
      private string gxTv_SdtNewProductos_Newproductosdescripcion ;
      private string gxTv_SdtNewProductos_Newproductosimagen_gxi ;
      private string gxTv_SdtNewProductos_Newproductosnombre ;
      private string gxTv_SdtNewProductos_Newproductosdescripcioncorta ;
      private string gxTv_SdtNewProductos_Newproductoslinkdescargademo ;
      private string gxTv_SdtNewProductos_Newproductoscomprar ;
      private string gxTv_SdtNewProductos_Newproductosnombre_Z ;
      private string gxTv_SdtNewProductos_Newproductosdescripcioncorta_Z ;
      private string gxTv_SdtNewProductos_Newproductoslinkdescargademo_Z ;
      private string gxTv_SdtNewProductos_Newproductoscomprar_Z ;
      private string gxTv_SdtNewProductos_Newproductosimagen_gxi_Z ;
      private string gxTv_SdtNewProductos_Newproductosimagen ;
   }

   [DataContract(Name = @"NewProductos", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtNewProductos_RESTInterface : GxGenericCollectionItem<SdtNewProductos>
   {
      public SdtNewProductos_RESTInterface( ) : base()
      {
      }

      public SdtNewProductos_RESTInterface( SdtNewProductos psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "NewProductosId" , Order = 0 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Newproductosid
      {
         get {
            return sdt.gxTpr_Newproductosid ;
         }

         set {
            sdt.gxTpr_Newproductosid = (short)(value.HasValue ? value.Value : 0);
         }

      }

      [DataMember( Name = "NewProductosImagen" , Order = 1 )]
      [GxUpload()]
      public string gxTpr_Newproductosimagen
      {
         get {
            return (!String.IsNullOrEmpty(StringUtil.RTrim( sdt.gxTpr_Newproductosimagen)) ? PathUtil.RelativeURL( sdt.gxTpr_Newproductosimagen) : StringUtil.RTrim( sdt.gxTpr_Newproductosimagen_gxi)) ;
         }

         set {
            sdt.gxTpr_Newproductosimagen = value;
         }

      }

      [DataMember( Name = "NewProductosNombre" , Order = 2 )]
      [GxSeudo()]
      public string gxTpr_Newproductosnombre
      {
         get {
            return sdt.gxTpr_Newproductosnombre ;
         }

         set {
            sdt.gxTpr_Newproductosnombre = value;
         }

      }

      [DataMember( Name = "NewProductosDescripcionCorta" , Order = 3 )]
      [GxSeudo()]
      public string gxTpr_Newproductosdescripcioncorta
      {
         get {
            return sdt.gxTpr_Newproductosdescripcioncorta ;
         }

         set {
            sdt.gxTpr_Newproductosdescripcioncorta = value;
         }

      }

      [DataMember( Name = "NewProductosDescripcion" , Order = 4 )]
      public string gxTpr_Newproductosdescripcion
      {
         get {
            return sdt.gxTpr_Newproductosdescripcion ;
         }

         set {
            sdt.gxTpr_Newproductosdescripcion = value;
         }

      }

      [DataMember( Name = "NewProductosNumeroDescargas" , Order = 5 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Newproductosnumerodescargas
      {
         get {
            return sdt.gxTpr_Newproductosnumerodescargas ;
         }

         set {
            sdt.gxTpr_Newproductosnumerodescargas = (short)(value.HasValue ? value.Value : 0);
         }

      }

      [DataMember( Name = "NewProductosLinkDescargaDemo" , Order = 6 )]
      [GxSeudo()]
      public string gxTpr_Newproductoslinkdescargademo
      {
         get {
            return sdt.gxTpr_Newproductoslinkdescargademo ;
         }

         set {
            sdt.gxTpr_Newproductoslinkdescargademo = value;
         }

      }

      [DataMember( Name = "NewProductosComprar" , Order = 7 )]
      [GxSeudo()]
      public string gxTpr_Newproductoscomprar
      {
         get {
            return sdt.gxTpr_Newproductoscomprar ;
         }

         set {
            sdt.gxTpr_Newproductoscomprar = value;
         }

      }

      [DataMember( Name = "NewProductosNumeroVentas" , Order = 8 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Newproductosnumeroventas
      {
         get {
            return sdt.gxTpr_Newproductosnumeroventas ;
         }

         set {
            sdt.gxTpr_Newproductosnumeroventas = (short)(value.HasValue ? value.Value : 0);
         }

      }

      [DataMember( Name = "NewProductosVisitas" , Order = 9 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Newproductosvisitas
      {
         get {
            return sdt.gxTpr_Newproductosvisitas ;
         }

         set {
            sdt.gxTpr_Newproductosvisitas = (short)(value.HasValue ? value.Value : 0);
         }

      }

      [DataMember( Name = "CategoriasId" , Order = 10 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Categoriasid
      {
         get {
            return sdt.gxTpr_Categoriasid ;
         }

         set {
            sdt.gxTpr_Categoriasid = (short)(value.HasValue ? value.Value : 0);
         }

      }

      public SdtNewProductos sdt
      {
         get {
            return (SdtNewProductos)Sdt ;
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
            sdt = new SdtNewProductos() ;
         }
      }

      [DataMember( Name = "gx_md5_hash", Order = 11 )]
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

   [DataContract(Name = @"NewProductos", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtNewProductos_RESTLInterface : GxGenericCollectionItem<SdtNewProductos>
   {
      public SdtNewProductos_RESTLInterface( ) : base()
      {
      }

      public SdtNewProductos_RESTLInterface( SdtNewProductos psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "NewProductosNombre" , Order = 0 )]
      [GxSeudo()]
      public string gxTpr_Newproductosnombre
      {
         get {
            return sdt.gxTpr_Newproductosnombre ;
         }

         set {
            sdt.gxTpr_Newproductosnombre = value;
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

      public SdtNewProductos sdt
      {
         get {
            return (SdtNewProductos)Sdt ;
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
            sdt = new SdtNewProductos() ;
         }
      }

   }

}
