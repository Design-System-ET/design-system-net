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
   [XmlRoot(ElementName = "NewBlog" )]
   [XmlType(TypeName =  "NewBlog" , Namespace = "DesignSystem" )]
   [Serializable]
   public class SdtNewBlog : GxSilentTrnSdt
   {
      public SdtNewBlog( )
      {
      }

      public SdtNewBlog( IGxContext context )
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

      public void Load( short AV12NewBlogId )
      {
         IGxSilentTrn obj;
         obj = getTransaction();
         obj.LoadKey(new Object[] {(short)AV12NewBlogId});
         return  ;
      }

      public override Object[][] GetBCKey( )
      {
         return (Object[][])(new Object[][]{new Object[]{"NewBlogId", typeof(short)}}) ;
      }

      public override GXProperties GetMetadata( )
      {
         GXProperties metadata = new GXProperties();
         metadata.Set("Name", "NewBlog");
         metadata.Set("BT", "NewBlog");
         metadata.Set("PK", "[ \"NewBlogId\" ]");
         metadata.Set("PKAssigned", "[ \"NewBlogId\" ]");
         metadata.Set("FKList", "[ { \"FK\":[ \"CategoriasId\" ],\"FKMap\":[  ] } ]");
         metadata.Set("AllowInsert", "True");
         metadata.Set("AllowUpdate", "True");
         metadata.Set("AllowDelete", "True");
         return metadata ;
      }

      public override GeneXus.Utils.GxStringCollection StateAttributes( )
      {
         GeneXus.Utils.GxStringCollection state = new GeneXus.Utils.GxStringCollection();
         state.Add("gxTpr_Newblogimagen_gxi");
         state.Add("gxTpr_Mode");
         state.Add("gxTpr_Initialized");
         state.Add("gxTpr_Newblogid_Z");
         state.Add("gxTpr_Newblogtitulo_Z");
         state.Add("gxTpr_Newblogsubtitulo_Z");
         state.Add("gxTpr_Newblogdescripcioncorta_Z");
         state.Add("gxTpr_Newblogvisitas_Z");
         state.Add("gxTpr_Newblogdestacado_Z");
         state.Add("gxTpr_Newblogborrador_Z");
         state.Add("gxTpr_Categoriasid_Z");
         state.Add("gxTpr_Newblogimagen_gxi_Z");
         return state ;
      }

      public override void Copy( GxUserType source )
      {
         SdtNewBlog sdt;
         sdt = (SdtNewBlog)(source);
         gxTv_SdtNewBlog_Newblogid = sdt.gxTv_SdtNewBlog_Newblogid ;
         gxTv_SdtNewBlog_Newblogimagen = sdt.gxTv_SdtNewBlog_Newblogimagen ;
         gxTv_SdtNewBlog_Newblogimagen_gxi = sdt.gxTv_SdtNewBlog_Newblogimagen_gxi ;
         gxTv_SdtNewBlog_Newblogtitulo = sdt.gxTv_SdtNewBlog_Newblogtitulo ;
         gxTv_SdtNewBlog_Newblogsubtitulo = sdt.gxTv_SdtNewBlog_Newblogsubtitulo ;
         gxTv_SdtNewBlog_Newblogdescripcion = sdt.gxTv_SdtNewBlog_Newblogdescripcion ;
         gxTv_SdtNewBlog_Newblogdescripcioncorta = sdt.gxTv_SdtNewBlog_Newblogdescripcioncorta ;
         gxTv_SdtNewBlog_Newblogvisitas = sdt.gxTv_SdtNewBlog_Newblogvisitas ;
         gxTv_SdtNewBlog_Newblogdestacado = sdt.gxTv_SdtNewBlog_Newblogdestacado ;
         gxTv_SdtNewBlog_Newblogborrador = sdt.gxTv_SdtNewBlog_Newblogborrador ;
         gxTv_SdtNewBlog_Categoriasid = sdt.gxTv_SdtNewBlog_Categoriasid ;
         gxTv_SdtNewBlog_Mode = sdt.gxTv_SdtNewBlog_Mode ;
         gxTv_SdtNewBlog_Initialized = sdt.gxTv_SdtNewBlog_Initialized ;
         gxTv_SdtNewBlog_Newblogid_Z = sdt.gxTv_SdtNewBlog_Newblogid_Z ;
         gxTv_SdtNewBlog_Newblogtitulo_Z = sdt.gxTv_SdtNewBlog_Newblogtitulo_Z ;
         gxTv_SdtNewBlog_Newblogsubtitulo_Z = sdt.gxTv_SdtNewBlog_Newblogsubtitulo_Z ;
         gxTv_SdtNewBlog_Newblogdescripcioncorta_Z = sdt.gxTv_SdtNewBlog_Newblogdescripcioncorta_Z ;
         gxTv_SdtNewBlog_Newblogvisitas_Z = sdt.gxTv_SdtNewBlog_Newblogvisitas_Z ;
         gxTv_SdtNewBlog_Newblogdestacado_Z = sdt.gxTv_SdtNewBlog_Newblogdestacado_Z ;
         gxTv_SdtNewBlog_Newblogborrador_Z = sdt.gxTv_SdtNewBlog_Newblogborrador_Z ;
         gxTv_SdtNewBlog_Categoriasid_Z = sdt.gxTv_SdtNewBlog_Categoriasid_Z ;
         gxTv_SdtNewBlog_Newblogimagen_gxi_Z = sdt.gxTv_SdtNewBlog_Newblogimagen_gxi_Z ;
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
         AddObjectProperty("NewBlogId", gxTv_SdtNewBlog_Newblogid, false, includeNonInitialized);
         AddObjectProperty("NewBlogImagen", gxTv_SdtNewBlog_Newblogimagen, false, includeNonInitialized);
         AddObjectProperty("NewBlogTitulo", gxTv_SdtNewBlog_Newblogtitulo, false, includeNonInitialized);
         AddObjectProperty("NewBlogSubTitulo", gxTv_SdtNewBlog_Newblogsubtitulo, false, includeNonInitialized);
         AddObjectProperty("NewBlogDescripcion", gxTv_SdtNewBlog_Newblogdescripcion, false, includeNonInitialized);
         AddObjectProperty("NewBlogDescripcionCorta", gxTv_SdtNewBlog_Newblogdescripcioncorta, false, includeNonInitialized);
         AddObjectProperty("NewBlogVisitas", gxTv_SdtNewBlog_Newblogvisitas, false, includeNonInitialized);
         AddObjectProperty("NewBlogDestacado", gxTv_SdtNewBlog_Newblogdestacado, false, includeNonInitialized);
         AddObjectProperty("NewBlogBorrador", gxTv_SdtNewBlog_Newblogborrador, false, includeNonInitialized);
         AddObjectProperty("CategoriasId", gxTv_SdtNewBlog_Categoriasid, false, includeNonInitialized);
         if ( includeState )
         {
            AddObjectProperty("NewBlogImagen_GXI", gxTv_SdtNewBlog_Newblogimagen_gxi, false, includeNonInitialized);
            AddObjectProperty("Mode", gxTv_SdtNewBlog_Mode, false, includeNonInitialized);
            AddObjectProperty("Initialized", gxTv_SdtNewBlog_Initialized, false, includeNonInitialized);
            AddObjectProperty("NewBlogId_Z", gxTv_SdtNewBlog_Newblogid_Z, false, includeNonInitialized);
            AddObjectProperty("NewBlogTitulo_Z", gxTv_SdtNewBlog_Newblogtitulo_Z, false, includeNonInitialized);
            AddObjectProperty("NewBlogSubTitulo_Z", gxTv_SdtNewBlog_Newblogsubtitulo_Z, false, includeNonInitialized);
            AddObjectProperty("NewBlogDescripcionCorta_Z", gxTv_SdtNewBlog_Newblogdescripcioncorta_Z, false, includeNonInitialized);
            AddObjectProperty("NewBlogVisitas_Z", gxTv_SdtNewBlog_Newblogvisitas_Z, false, includeNonInitialized);
            AddObjectProperty("NewBlogDestacado_Z", gxTv_SdtNewBlog_Newblogdestacado_Z, false, includeNonInitialized);
            AddObjectProperty("NewBlogBorrador_Z", gxTv_SdtNewBlog_Newblogborrador_Z, false, includeNonInitialized);
            AddObjectProperty("CategoriasId_Z", gxTv_SdtNewBlog_Categoriasid_Z, false, includeNonInitialized);
            AddObjectProperty("NewBlogImagen_GXI_Z", gxTv_SdtNewBlog_Newblogimagen_gxi_Z, false, includeNonInitialized);
         }
         return  ;
      }

      public void UpdateDirties( SdtNewBlog sdt )
      {
         if ( sdt.IsDirty("NewBlogId") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogid = sdt.gxTv_SdtNewBlog_Newblogid ;
         }
         if ( sdt.IsDirty("NewBlogImagen") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogimagen = sdt.gxTv_SdtNewBlog_Newblogimagen ;
         }
         if ( sdt.IsDirty("NewBlogImagen") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogimagen_gxi = sdt.gxTv_SdtNewBlog_Newblogimagen_gxi ;
         }
         if ( sdt.IsDirty("NewBlogTitulo") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogtitulo = sdt.gxTv_SdtNewBlog_Newblogtitulo ;
         }
         if ( sdt.IsDirty("NewBlogSubTitulo") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogsubtitulo = sdt.gxTv_SdtNewBlog_Newblogsubtitulo ;
         }
         if ( sdt.IsDirty("NewBlogDescripcion") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdescripcion = sdt.gxTv_SdtNewBlog_Newblogdescripcion ;
         }
         if ( sdt.IsDirty("NewBlogDescripcionCorta") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdescripcioncorta = sdt.gxTv_SdtNewBlog_Newblogdescripcioncorta ;
         }
         if ( sdt.IsDirty("NewBlogVisitas") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogvisitas = sdt.gxTv_SdtNewBlog_Newblogvisitas ;
         }
         if ( sdt.IsDirty("NewBlogDestacado") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdestacado = sdt.gxTv_SdtNewBlog_Newblogdestacado ;
         }
         if ( sdt.IsDirty("NewBlogBorrador") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogborrador = sdt.gxTv_SdtNewBlog_Newblogborrador ;
         }
         if ( sdt.IsDirty("CategoriasId") )
         {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Categoriasid = sdt.gxTv_SdtNewBlog_Categoriasid ;
         }
         return  ;
      }

      [  SoapElement( ElementName = "NewBlogId" )]
      [  XmlElement( ElementName = "NewBlogId"   )]
      public short gxTpr_Newblogid
      {
         get {
            return gxTv_SdtNewBlog_Newblogid ;
         }

         set {
            sdtIsNull = 0;
            if ( gxTv_SdtNewBlog_Newblogid != value )
            {
               gxTv_SdtNewBlog_Mode = "INS";
               this.gxTv_SdtNewBlog_Newblogid_Z_SetNull( );
               this.gxTv_SdtNewBlog_Newblogtitulo_Z_SetNull( );
               this.gxTv_SdtNewBlog_Newblogsubtitulo_Z_SetNull( );
               this.gxTv_SdtNewBlog_Newblogdescripcioncorta_Z_SetNull( );
               this.gxTv_SdtNewBlog_Newblogvisitas_Z_SetNull( );
               this.gxTv_SdtNewBlog_Newblogdestacado_Z_SetNull( );
               this.gxTv_SdtNewBlog_Newblogborrador_Z_SetNull( );
               this.gxTv_SdtNewBlog_Categoriasid_Z_SetNull( );
               this.gxTv_SdtNewBlog_Newblogimagen_gxi_Z_SetNull( );
            }
            gxTv_SdtNewBlog_Newblogid = value;
            SetDirty("Newblogid");
         }

      }

      [  SoapElement( ElementName = "NewBlogImagen" )]
      [  XmlElement( ElementName = "NewBlogImagen"   )]
      [GxUpload()]
      public string gxTpr_Newblogimagen
      {
         get {
            return gxTv_SdtNewBlog_Newblogimagen ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogimagen = value;
            SetDirty("Newblogimagen");
         }

      }

      [  SoapElement( ElementName = "NewBlogImagen_GXI" )]
      [  XmlElement( ElementName = "NewBlogImagen_GXI"   )]
      public string gxTpr_Newblogimagen_gxi
      {
         get {
            return gxTv_SdtNewBlog_Newblogimagen_gxi ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogimagen_gxi = value;
            SetDirty("Newblogimagen_gxi");
         }

      }

      [  SoapElement( ElementName = "NewBlogTitulo" )]
      [  XmlElement( ElementName = "NewBlogTitulo"   )]
      public string gxTpr_Newblogtitulo
      {
         get {
            return gxTv_SdtNewBlog_Newblogtitulo ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogtitulo = value;
            SetDirty("Newblogtitulo");
         }

      }

      [  SoapElement( ElementName = "NewBlogSubTitulo" )]
      [  XmlElement( ElementName = "NewBlogSubTitulo"   )]
      public string gxTpr_Newblogsubtitulo
      {
         get {
            return gxTv_SdtNewBlog_Newblogsubtitulo ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogsubtitulo = value;
            SetDirty("Newblogsubtitulo");
         }

      }

      [  SoapElement( ElementName = "NewBlogDescripcion" )]
      [  XmlElement( ElementName = "NewBlogDescripcion"   )]
      public string gxTpr_Newblogdescripcion
      {
         get {
            return gxTv_SdtNewBlog_Newblogdescripcion ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdescripcion = value;
            SetDirty("Newblogdescripcion");
         }

      }

      [  SoapElement( ElementName = "NewBlogDescripcionCorta" )]
      [  XmlElement( ElementName = "NewBlogDescripcionCorta"   )]
      public string gxTpr_Newblogdescripcioncorta
      {
         get {
            return gxTv_SdtNewBlog_Newblogdescripcioncorta ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdescripcioncorta = value;
            SetDirty("Newblogdescripcioncorta");
         }

      }

      [  SoapElement( ElementName = "NewBlogVisitas" )]
      [  XmlElement( ElementName = "NewBlogVisitas"   )]
      public short gxTpr_Newblogvisitas
      {
         get {
            return gxTv_SdtNewBlog_Newblogvisitas ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogvisitas = value;
            SetDirty("Newblogvisitas");
         }

      }

      [  SoapElement( ElementName = "NewBlogDestacado" )]
      [  XmlElement( ElementName = "NewBlogDestacado"   )]
      public bool gxTpr_Newblogdestacado
      {
         get {
            return gxTv_SdtNewBlog_Newblogdestacado ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdestacado = value;
            SetDirty("Newblogdestacado");
         }

      }

      [  SoapElement( ElementName = "NewBlogBorrador" )]
      [  XmlElement( ElementName = "NewBlogBorrador"   )]
      public bool gxTpr_Newblogborrador
      {
         get {
            return gxTv_SdtNewBlog_Newblogborrador ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogborrador = value;
            SetDirty("Newblogborrador");
         }

      }

      [  SoapElement( ElementName = "CategoriasId" )]
      [  XmlElement( ElementName = "CategoriasId"   )]
      public short gxTpr_Categoriasid
      {
         get {
            return gxTv_SdtNewBlog_Categoriasid ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Categoriasid = value;
            SetDirty("Categoriasid");
         }

      }

      [  SoapElement( ElementName = "Mode" )]
      [  XmlElement( ElementName = "Mode"   )]
      public string gxTpr_Mode
      {
         get {
            return gxTv_SdtNewBlog_Mode ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Mode = value;
            SetDirty("Mode");
         }

      }

      public void gxTv_SdtNewBlog_Mode_SetNull( )
      {
         gxTv_SdtNewBlog_Mode = "";
         SetDirty("Mode");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Mode_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "Initialized" )]
      [  XmlElement( ElementName = "Initialized"   )]
      public short gxTpr_Initialized
      {
         get {
            return gxTv_SdtNewBlog_Initialized ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Initialized = value;
            SetDirty("Initialized");
         }

      }

      public void gxTv_SdtNewBlog_Initialized_SetNull( )
      {
         gxTv_SdtNewBlog_Initialized = 0;
         SetDirty("Initialized");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Initialized_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogId_Z" )]
      [  XmlElement( ElementName = "NewBlogId_Z"   )]
      public short gxTpr_Newblogid_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogid_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogid_Z = value;
            SetDirty("Newblogid_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogid_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogid_Z = 0;
         SetDirty("Newblogid_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogid_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogTitulo_Z" )]
      [  XmlElement( ElementName = "NewBlogTitulo_Z"   )]
      public string gxTpr_Newblogtitulo_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogtitulo_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogtitulo_Z = value;
            SetDirty("Newblogtitulo_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogtitulo_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogtitulo_Z = "";
         SetDirty("Newblogtitulo_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogtitulo_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogSubTitulo_Z" )]
      [  XmlElement( ElementName = "NewBlogSubTitulo_Z"   )]
      public string gxTpr_Newblogsubtitulo_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogsubtitulo_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogsubtitulo_Z = value;
            SetDirty("Newblogsubtitulo_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogsubtitulo_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogsubtitulo_Z = "";
         SetDirty("Newblogsubtitulo_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogsubtitulo_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogDescripcionCorta_Z" )]
      [  XmlElement( ElementName = "NewBlogDescripcionCorta_Z"   )]
      public string gxTpr_Newblogdescripcioncorta_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogdescripcioncorta_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdescripcioncorta_Z = value;
            SetDirty("Newblogdescripcioncorta_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogdescripcioncorta_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogdescripcioncorta_Z = "";
         SetDirty("Newblogdescripcioncorta_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogdescripcioncorta_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogVisitas_Z" )]
      [  XmlElement( ElementName = "NewBlogVisitas_Z"   )]
      public short gxTpr_Newblogvisitas_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogvisitas_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogvisitas_Z = value;
            SetDirty("Newblogvisitas_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogvisitas_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogvisitas_Z = 0;
         SetDirty("Newblogvisitas_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogvisitas_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogDestacado_Z" )]
      [  XmlElement( ElementName = "NewBlogDestacado_Z"   )]
      public bool gxTpr_Newblogdestacado_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogdestacado_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogdestacado_Z = value;
            SetDirty("Newblogdestacado_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogdestacado_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogdestacado_Z = false;
         SetDirty("Newblogdestacado_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogdestacado_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogBorrador_Z" )]
      [  XmlElement( ElementName = "NewBlogBorrador_Z"   )]
      public bool gxTpr_Newblogborrador_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogborrador_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogborrador_Z = value;
            SetDirty("Newblogborrador_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogborrador_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogborrador_Z = false;
         SetDirty("Newblogborrador_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogborrador_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "CategoriasId_Z" )]
      [  XmlElement( ElementName = "CategoriasId_Z"   )]
      public short gxTpr_Categoriasid_Z
      {
         get {
            return gxTv_SdtNewBlog_Categoriasid_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Categoriasid_Z = value;
            SetDirty("Categoriasid_Z");
         }

      }

      public void gxTv_SdtNewBlog_Categoriasid_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Categoriasid_Z = 0;
         SetDirty("Categoriasid_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Categoriasid_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "NewBlogImagen_GXI_Z" )]
      [  XmlElement( ElementName = "NewBlogImagen_GXI_Z"   )]
      public string gxTpr_Newblogimagen_gxi_Z
      {
         get {
            return gxTv_SdtNewBlog_Newblogimagen_gxi_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtNewBlog_Newblogimagen_gxi_Z = value;
            SetDirty("Newblogimagen_gxi_Z");
         }

      }

      public void gxTv_SdtNewBlog_Newblogimagen_gxi_Z_SetNull( )
      {
         gxTv_SdtNewBlog_Newblogimagen_gxi_Z = "";
         SetDirty("Newblogimagen_gxi_Z");
         return  ;
      }

      public bool gxTv_SdtNewBlog_Newblogimagen_gxi_Z_IsNull( )
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
         gxTv_SdtNewBlog_Newblogimagen = "";
         gxTv_SdtNewBlog_Newblogimagen_gxi = "";
         gxTv_SdtNewBlog_Newblogtitulo = "";
         gxTv_SdtNewBlog_Newblogsubtitulo = "";
         gxTv_SdtNewBlog_Newblogdescripcion = "";
         gxTv_SdtNewBlog_Newblogdescripcioncorta = "";
         gxTv_SdtNewBlog_Mode = "";
         gxTv_SdtNewBlog_Newblogtitulo_Z = "";
         gxTv_SdtNewBlog_Newblogsubtitulo_Z = "";
         gxTv_SdtNewBlog_Newblogdescripcioncorta_Z = "";
         gxTv_SdtNewBlog_Newblogimagen_gxi_Z = "";
         IGxSilentTrn obj;
         obj = (IGxSilentTrn)ClassLoader.FindInstance( "newblog", "DesignSystem.Programs.newblog_bc", new Object[] {context}, constructorCallingAssembly);;
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

      private short gxTv_SdtNewBlog_Newblogid ;
      private short sdtIsNull ;
      private short gxTv_SdtNewBlog_Newblogvisitas ;
      private short gxTv_SdtNewBlog_Categoriasid ;
      private short gxTv_SdtNewBlog_Initialized ;
      private short gxTv_SdtNewBlog_Newblogid_Z ;
      private short gxTv_SdtNewBlog_Newblogvisitas_Z ;
      private short gxTv_SdtNewBlog_Categoriasid_Z ;
      private string gxTv_SdtNewBlog_Mode ;
      private bool gxTv_SdtNewBlog_Newblogdestacado ;
      private bool gxTv_SdtNewBlog_Newblogborrador ;
      private bool gxTv_SdtNewBlog_Newblogdestacado_Z ;
      private bool gxTv_SdtNewBlog_Newblogborrador_Z ;
      private string gxTv_SdtNewBlog_Newblogdescripcion ;
      private string gxTv_SdtNewBlog_Newblogimagen_gxi ;
      private string gxTv_SdtNewBlog_Newblogtitulo ;
      private string gxTv_SdtNewBlog_Newblogsubtitulo ;
      private string gxTv_SdtNewBlog_Newblogdescripcioncorta ;
      private string gxTv_SdtNewBlog_Newblogtitulo_Z ;
      private string gxTv_SdtNewBlog_Newblogsubtitulo_Z ;
      private string gxTv_SdtNewBlog_Newblogdescripcioncorta_Z ;
      private string gxTv_SdtNewBlog_Newblogimagen_gxi_Z ;
      private string gxTv_SdtNewBlog_Newblogimagen ;
   }

   [DataContract(Name = @"NewBlog", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtNewBlog_RESTInterface : GxGenericCollectionItem<SdtNewBlog>
   {
      public SdtNewBlog_RESTInterface( ) : base()
      {
      }

      public SdtNewBlog_RESTInterface( SdtNewBlog psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "NewBlogId" , Order = 0 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Newblogid
      {
         get {
            return sdt.gxTpr_Newblogid ;
         }

         set {
            sdt.gxTpr_Newblogid = (short)(value.HasValue ? value.Value : 0);
         }

      }

      [DataMember( Name = "NewBlogImagen" , Order = 1 )]
      [GxUpload()]
      public string gxTpr_Newblogimagen
      {
         get {
            return (!String.IsNullOrEmpty(StringUtil.RTrim( sdt.gxTpr_Newblogimagen)) ? PathUtil.RelativeURL( sdt.gxTpr_Newblogimagen) : StringUtil.RTrim( sdt.gxTpr_Newblogimagen_gxi)) ;
         }

         set {
            sdt.gxTpr_Newblogimagen = value;
         }

      }

      [DataMember( Name = "NewBlogTitulo" , Order = 2 )]
      [GxSeudo()]
      public string gxTpr_Newblogtitulo
      {
         get {
            return sdt.gxTpr_Newblogtitulo ;
         }

         set {
            sdt.gxTpr_Newblogtitulo = value;
         }

      }

      [DataMember( Name = "NewBlogSubTitulo" , Order = 3 )]
      [GxSeudo()]
      public string gxTpr_Newblogsubtitulo
      {
         get {
            return sdt.gxTpr_Newblogsubtitulo ;
         }

         set {
            sdt.gxTpr_Newblogsubtitulo = value;
         }

      }

      [DataMember( Name = "NewBlogDescripcion" , Order = 4 )]
      public string gxTpr_Newblogdescripcion
      {
         get {
            return sdt.gxTpr_Newblogdescripcion ;
         }

         set {
            sdt.gxTpr_Newblogdescripcion = value;
         }

      }

      [DataMember( Name = "NewBlogDescripcionCorta" , Order = 5 )]
      [GxSeudo()]
      public string gxTpr_Newblogdescripcioncorta
      {
         get {
            return sdt.gxTpr_Newblogdescripcioncorta ;
         }

         set {
            sdt.gxTpr_Newblogdescripcioncorta = value;
         }

      }

      [DataMember( Name = "NewBlogVisitas" , Order = 6 )]
      [GxSeudo()]
      public Nullable<short> gxTpr_Newblogvisitas
      {
         get {
            return sdt.gxTpr_Newblogvisitas ;
         }

         set {
            sdt.gxTpr_Newblogvisitas = (short)(value.HasValue ? value.Value : 0);
         }

      }

      [DataMember( Name = "NewBlogDestacado" , Order = 7 )]
      [GxSeudo()]
      public bool gxTpr_Newblogdestacado
      {
         get {
            return sdt.gxTpr_Newblogdestacado ;
         }

         set {
            sdt.gxTpr_Newblogdestacado = value;
         }

      }

      [DataMember( Name = "NewBlogBorrador" , Order = 8 )]
      [GxSeudo()]
      public bool gxTpr_Newblogborrador
      {
         get {
            return sdt.gxTpr_Newblogborrador ;
         }

         set {
            sdt.gxTpr_Newblogborrador = value;
         }

      }

      [DataMember( Name = "CategoriasId" , Order = 9 )]
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

      public SdtNewBlog sdt
      {
         get {
            return (SdtNewBlog)Sdt ;
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
            sdt = new SdtNewBlog() ;
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

   [DataContract(Name = @"NewBlog", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtNewBlog_RESTLInterface : GxGenericCollectionItem<SdtNewBlog>
   {
      public SdtNewBlog_RESTLInterface( ) : base()
      {
      }

      public SdtNewBlog_RESTLInterface( SdtNewBlog psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "NewBlogTitulo" , Order = 0 )]
      [GxSeudo()]
      public string gxTpr_Newblogtitulo
      {
         get {
            return sdt.gxTpr_Newblogtitulo ;
         }

         set {
            sdt.gxTpr_Newblogtitulo = value;
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

      public SdtNewBlog sdt
      {
         get {
            return (SdtNewBlog)Sdt ;
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
            sdt = new SdtNewBlog() ;
         }
      }

   }

}
