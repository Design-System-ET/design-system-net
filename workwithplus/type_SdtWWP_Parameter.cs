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
namespace DesignSystem.Programs.workwithplus {
   [XmlRoot(ElementName = "WWP_Parameter" )]
   [XmlType(TypeName =  "WWP_Parameter" , Namespace = "DesignSystem" )]
   [Serializable]
   public class SdtWWP_Parameter : GxSilentTrnSdt
   {
      public SdtWWP_Parameter( )
      {
      }

      public SdtWWP_Parameter( IGxContext context )
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

      public void Load( string AV1WWPParameterKey )
      {
         IGxSilentTrn obj;
         obj = getTransaction();
         obj.LoadKey(new Object[] {(string)AV1WWPParameterKey});
         return  ;
      }

      public override Object[][] GetBCKey( )
      {
         return (Object[][])(new Object[][]{new Object[]{"WWPParameterKey", typeof(string)}}) ;
      }

      public override GXProperties GetMetadata( )
      {
         GXProperties metadata = new GXProperties();
         metadata.Set("Name", "WorkWithPlus\\WWP_Parameter");
         metadata.Set("BT", "WWP_Parameter");
         metadata.Set("PK", "[ \"WWPParameterKey\" ]");
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
         state.Add("gxTpr_Wwpparameterkey_Z");
         state.Add("gxTpr_Wwpparametercategory_Z");
         state.Add("gxTpr_Wwpparameterdescription_Z");
         state.Add("gxTpr_Wwpparametervaluetrimmed_Z");
         state.Add("gxTpr_Wwpparameterdisabledelete_Z");
         return state ;
      }

      public override void Copy( GxUserType source )
      {
         DesignSystem.Programs.workwithplus.SdtWWP_Parameter sdt;
         sdt = (DesignSystem.Programs.workwithplus.SdtWWP_Parameter)(source);
         gxTv_SdtWWP_Parameter_Wwpparameterkey = sdt.gxTv_SdtWWP_Parameter_Wwpparameterkey ;
         gxTv_SdtWWP_Parameter_Wwpparametercategory = sdt.gxTv_SdtWWP_Parameter_Wwpparametercategory ;
         gxTv_SdtWWP_Parameter_Wwpparameterdescription = sdt.gxTv_SdtWWP_Parameter_Wwpparameterdescription ;
         gxTv_SdtWWP_Parameter_Wwpparametervalue = sdt.gxTv_SdtWWP_Parameter_Wwpparametervalue ;
         gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed = sdt.gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed ;
         gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete = sdt.gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete ;
         gxTv_SdtWWP_Parameter_Mode = sdt.gxTv_SdtWWP_Parameter_Mode ;
         gxTv_SdtWWP_Parameter_Initialized = sdt.gxTv_SdtWWP_Parameter_Initialized ;
         gxTv_SdtWWP_Parameter_Wwpparameterkey_Z = sdt.gxTv_SdtWWP_Parameter_Wwpparameterkey_Z ;
         gxTv_SdtWWP_Parameter_Wwpparametercategory_Z = sdt.gxTv_SdtWWP_Parameter_Wwpparametercategory_Z ;
         gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z = sdt.gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z ;
         gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z = sdt.gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z ;
         gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z = sdt.gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z ;
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
         AddObjectProperty("WWPParameterKey", gxTv_SdtWWP_Parameter_Wwpparameterkey, false, includeNonInitialized);
         AddObjectProperty("WWPParameterCategory", gxTv_SdtWWP_Parameter_Wwpparametercategory, false, includeNonInitialized);
         AddObjectProperty("WWPParameterDescription", gxTv_SdtWWP_Parameter_Wwpparameterdescription, false, includeNonInitialized);
         AddObjectProperty("WWPParameterValue", gxTv_SdtWWP_Parameter_Wwpparametervalue, false, includeNonInitialized);
         AddObjectProperty("WWPParameterValueTrimmed", gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed, false, includeNonInitialized);
         AddObjectProperty("WWPParameterDisableDelete", gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete, false, includeNonInitialized);
         if ( includeState )
         {
            AddObjectProperty("Mode", gxTv_SdtWWP_Parameter_Mode, false, includeNonInitialized);
            AddObjectProperty("Initialized", gxTv_SdtWWP_Parameter_Initialized, false, includeNonInitialized);
            AddObjectProperty("WWPParameterKey_Z", gxTv_SdtWWP_Parameter_Wwpparameterkey_Z, false, includeNonInitialized);
            AddObjectProperty("WWPParameterCategory_Z", gxTv_SdtWWP_Parameter_Wwpparametercategory_Z, false, includeNonInitialized);
            AddObjectProperty("WWPParameterDescription_Z", gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z, false, includeNonInitialized);
            AddObjectProperty("WWPParameterValueTrimmed_Z", gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z, false, includeNonInitialized);
            AddObjectProperty("WWPParameterDisableDelete_Z", gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z, false, includeNonInitialized);
         }
         return  ;
      }

      public void UpdateDirties( DesignSystem.Programs.workwithplus.SdtWWP_Parameter sdt )
      {
         if ( sdt.IsDirty("WWPParameterKey") )
         {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterkey = sdt.gxTv_SdtWWP_Parameter_Wwpparameterkey ;
         }
         if ( sdt.IsDirty("WWPParameterCategory") )
         {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametercategory = sdt.gxTv_SdtWWP_Parameter_Wwpparametercategory ;
         }
         if ( sdt.IsDirty("WWPParameterDescription") )
         {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterdescription = sdt.gxTv_SdtWWP_Parameter_Wwpparameterdescription ;
         }
         if ( sdt.IsDirty("WWPParameterValue") )
         {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametervalue = sdt.gxTv_SdtWWP_Parameter_Wwpparametervalue ;
         }
         if ( sdt.IsDirty("WWPParameterValueTrimmed") )
         {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed = sdt.gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed ;
         }
         if ( sdt.IsDirty("WWPParameterDisableDelete") )
         {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete = sdt.gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete ;
         }
         return  ;
      }

      [  SoapElement( ElementName = "WWPParameterKey" )]
      [  XmlElement( ElementName = "WWPParameterKey"   )]
      public string gxTpr_Wwpparameterkey
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparameterkey ;
         }

         set {
            sdtIsNull = 0;
            if ( StringUtil.StrCmp(gxTv_SdtWWP_Parameter_Wwpparameterkey, value) != 0 )
            {
               gxTv_SdtWWP_Parameter_Mode = "INS";
               this.gxTv_SdtWWP_Parameter_Wwpparameterkey_Z_SetNull( );
               this.gxTv_SdtWWP_Parameter_Wwpparametercategory_Z_SetNull( );
               this.gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z_SetNull( );
               this.gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z_SetNull( );
               this.gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z_SetNull( );
            }
            gxTv_SdtWWP_Parameter_Wwpparameterkey = value;
            SetDirty("Wwpparameterkey");
         }

      }

      [  SoapElement( ElementName = "WWPParameterCategory" )]
      [  XmlElement( ElementName = "WWPParameterCategory"   )]
      public string gxTpr_Wwpparametercategory
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparametercategory ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametercategory = value;
            SetDirty("Wwpparametercategory");
         }

      }

      [  SoapElement( ElementName = "WWPParameterDescription" )]
      [  XmlElement( ElementName = "WWPParameterDescription"   )]
      public string gxTpr_Wwpparameterdescription
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparameterdescription ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterdescription = value;
            SetDirty("Wwpparameterdescription");
         }

      }

      [  SoapElement( ElementName = "WWPParameterValue" )]
      [  XmlElement( ElementName = "WWPParameterValue"   )]
      public string gxTpr_Wwpparametervalue
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparametervalue ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametervalue = value;
            SetDirty("Wwpparametervalue");
         }

      }

      [  SoapElement( ElementName = "WWPParameterValueTrimmed" )]
      [  XmlElement( ElementName = "WWPParameterValueTrimmed"   )]
      public string gxTpr_Wwpparametervaluetrimmed
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed = value;
            SetDirty("Wwpparametervaluetrimmed");
         }

      }

      public void gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed = "";
         SetDirty("Wwpparametervaluetrimmed");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "WWPParameterDisableDelete" )]
      [  XmlElement( ElementName = "WWPParameterDisableDelete"   )]
      public bool gxTpr_Wwpparameterdisabledelete
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete = value;
            SetDirty("Wwpparameterdisabledelete");
         }

      }

      [  SoapElement( ElementName = "Mode" )]
      [  XmlElement( ElementName = "Mode"   )]
      public string gxTpr_Mode
      {
         get {
            return gxTv_SdtWWP_Parameter_Mode ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Mode = value;
            SetDirty("Mode");
         }

      }

      public void gxTv_SdtWWP_Parameter_Mode_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Mode = "";
         SetDirty("Mode");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Mode_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "Initialized" )]
      [  XmlElement( ElementName = "Initialized"   )]
      public short gxTpr_Initialized
      {
         get {
            return gxTv_SdtWWP_Parameter_Initialized ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Initialized = value;
            SetDirty("Initialized");
         }

      }

      public void gxTv_SdtWWP_Parameter_Initialized_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Initialized = 0;
         SetDirty("Initialized");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Initialized_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "WWPParameterKey_Z" )]
      [  XmlElement( ElementName = "WWPParameterKey_Z"   )]
      public string gxTpr_Wwpparameterkey_Z
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparameterkey_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterkey_Z = value;
            SetDirty("Wwpparameterkey_Z");
         }

      }

      public void gxTv_SdtWWP_Parameter_Wwpparameterkey_Z_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Wwpparameterkey_Z = "";
         SetDirty("Wwpparameterkey_Z");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Wwpparameterkey_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "WWPParameterCategory_Z" )]
      [  XmlElement( ElementName = "WWPParameterCategory_Z"   )]
      public string gxTpr_Wwpparametercategory_Z
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparametercategory_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametercategory_Z = value;
            SetDirty("Wwpparametercategory_Z");
         }

      }

      public void gxTv_SdtWWP_Parameter_Wwpparametercategory_Z_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Wwpparametercategory_Z = "";
         SetDirty("Wwpparametercategory_Z");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Wwpparametercategory_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "WWPParameterDescription_Z" )]
      [  XmlElement( ElementName = "WWPParameterDescription_Z"   )]
      public string gxTpr_Wwpparameterdescription_Z
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z = value;
            SetDirty("Wwpparameterdescription_Z");
         }

      }

      public void gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z = "";
         SetDirty("Wwpparameterdescription_Z");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "WWPParameterValueTrimmed_Z" )]
      [  XmlElement( ElementName = "WWPParameterValueTrimmed_Z"   )]
      public string gxTpr_Wwpparametervaluetrimmed_Z
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z = value;
            SetDirty("Wwpparametervaluetrimmed_Z");
         }

      }

      public void gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z = "";
         SetDirty("Wwpparametervaluetrimmed_Z");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z_IsNull( )
      {
         return false ;
      }

      [  SoapElement( ElementName = "WWPParameterDisableDelete_Z" )]
      [  XmlElement( ElementName = "WWPParameterDisableDelete_Z"   )]
      public bool gxTpr_Wwpparameterdisabledelete_Z
      {
         get {
            return gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z ;
         }

         set {
            sdtIsNull = 0;
            gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z = value;
            SetDirty("Wwpparameterdisabledelete_Z");
         }

      }

      public void gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z_SetNull( )
      {
         gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z = false;
         SetDirty("Wwpparameterdisabledelete_Z");
         return  ;
      }

      public bool gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z_IsNull( )
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
         gxTv_SdtWWP_Parameter_Wwpparameterkey = "";
         sdtIsNull = 1;
         gxTv_SdtWWP_Parameter_Wwpparametercategory = "";
         gxTv_SdtWWP_Parameter_Wwpparameterdescription = "";
         gxTv_SdtWWP_Parameter_Wwpparametervalue = "";
         gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed = "";
         gxTv_SdtWWP_Parameter_Mode = "";
         gxTv_SdtWWP_Parameter_Wwpparameterkey_Z = "";
         gxTv_SdtWWP_Parameter_Wwpparametercategory_Z = "";
         gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z = "";
         gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z = "";
         IGxSilentTrn obj;
         obj = (IGxSilentTrn)ClassLoader.FindInstance( "workwithplus.wwp_parameter", "DesignSystem.Programs.workwithplus.wwp_parameter_bc", new Object[] {context}, constructorCallingAssembly);;
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

      private short sdtIsNull ;
      private short gxTv_SdtWWP_Parameter_Initialized ;
      private string gxTv_SdtWWP_Parameter_Mode ;
      private bool gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete ;
      private bool gxTv_SdtWWP_Parameter_Wwpparameterdisabledelete_Z ;
      private string gxTv_SdtWWP_Parameter_Wwpparametervalue ;
      private string gxTv_SdtWWP_Parameter_Wwpparameterkey ;
      private string gxTv_SdtWWP_Parameter_Wwpparametercategory ;
      private string gxTv_SdtWWP_Parameter_Wwpparameterdescription ;
      private string gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed ;
      private string gxTv_SdtWWP_Parameter_Wwpparameterkey_Z ;
      private string gxTv_SdtWWP_Parameter_Wwpparametercategory_Z ;
      private string gxTv_SdtWWP_Parameter_Wwpparameterdescription_Z ;
      private string gxTv_SdtWWP_Parameter_Wwpparametervaluetrimmed_Z ;
   }

   [DataContract(Name = @"WorkWithPlus\WWP_Parameter", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtWWP_Parameter_RESTInterface : GxGenericCollectionItem<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>
   {
      public SdtWWP_Parameter_RESTInterface( ) : base()
      {
      }

      public SdtWWP_Parameter_RESTInterface( DesignSystem.Programs.workwithplus.SdtWWP_Parameter psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "WWPParameterKey" , Order = 0 )]
      [GxSeudo()]
      public string gxTpr_Wwpparameterkey
      {
         get {
            return sdt.gxTpr_Wwpparameterkey ;
         }

         set {
            sdt.gxTpr_Wwpparameterkey = value;
         }

      }

      [DataMember( Name = "WWPParameterCategory" , Order = 1 )]
      [GxSeudo()]
      public string gxTpr_Wwpparametercategory
      {
         get {
            return sdt.gxTpr_Wwpparametercategory ;
         }

         set {
            sdt.gxTpr_Wwpparametercategory = value;
         }

      }

      [DataMember( Name = "WWPParameterDescription" , Order = 2 )]
      [GxSeudo()]
      public string gxTpr_Wwpparameterdescription
      {
         get {
            return sdt.gxTpr_Wwpparameterdescription ;
         }

         set {
            sdt.gxTpr_Wwpparameterdescription = value;
         }

      }

      [DataMember( Name = "WWPParameterValue" , Order = 3 )]
      public string gxTpr_Wwpparametervalue
      {
         get {
            return sdt.gxTpr_Wwpparametervalue ;
         }

         set {
            sdt.gxTpr_Wwpparametervalue = value;
         }

      }

      [DataMember( Name = "WWPParameterValueTrimmed" , Order = 4 )]
      [GxSeudo()]
      public string gxTpr_Wwpparametervaluetrimmed
      {
         get {
            return sdt.gxTpr_Wwpparametervaluetrimmed ;
         }

         set {
            sdt.gxTpr_Wwpparametervaluetrimmed = value;
         }

      }

      [DataMember( Name = "WWPParameterDisableDelete" , Order = 5 )]
      [GxSeudo()]
      public bool gxTpr_Wwpparameterdisabledelete
      {
         get {
            return sdt.gxTpr_Wwpparameterdisabledelete ;
         }

         set {
            sdt.gxTpr_Wwpparameterdisabledelete = value;
         }

      }

      public DesignSystem.Programs.workwithplus.SdtWWP_Parameter sdt
      {
         get {
            return (DesignSystem.Programs.workwithplus.SdtWWP_Parameter)Sdt ;
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
            sdt = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter() ;
         }
      }

      [DataMember( Name = "gx_md5_hash", Order = 6 )]
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

   [DataContract(Name = @"WorkWithPlus\WWP_Parameter", Namespace = "DesignSystem")]
   [GxJsonSerialization("default")]
   public class SdtWWP_Parameter_RESTLInterface : GxGenericCollectionItem<DesignSystem.Programs.workwithplus.SdtWWP_Parameter>
   {
      public SdtWWP_Parameter_RESTLInterface( ) : base()
      {
      }

      public SdtWWP_Parameter_RESTLInterface( DesignSystem.Programs.workwithplus.SdtWWP_Parameter psdt ) : base(psdt)
      {
      }

      [DataMember( Name = "WWPParameterCategory" , Order = 0 )]
      [GxSeudo()]
      public string gxTpr_Wwpparametercategory
      {
         get {
            return sdt.gxTpr_Wwpparametercategory ;
         }

         set {
            sdt.gxTpr_Wwpparametercategory = value;
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

      public DesignSystem.Programs.workwithplus.SdtWWP_Parameter sdt
      {
         get {
            return (DesignSystem.Programs.workwithplus.SdtWWP_Parameter)Sdt ;
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
            sdt = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter() ;
         }
      }

   }

}
