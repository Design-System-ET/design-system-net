/*
				   File: type_SdtWWPGridState_DynamicFilter
			Description: DynamicFilters
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
		   Program type: Callable routine
			  Main DBMS: 
*/
using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using DesignSystem.Programs;
namespace DesignSystem.Programs.wwpbaseobjects
{
	[XmlRoot(ElementName="WWPGridState.DynamicFilter")]
	[XmlType(TypeName="WWPGridState.DynamicFilter" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtWWPGridState_DynamicFilter : GxUserType
	{
		public SdtWWPGridState_DynamicFilter( )
		{
			/* Constructor for serialization */
			gxTv_SdtWWPGridState_DynamicFilter_Selected = "";

			gxTv_SdtWWPGridState_DynamicFilter_Dsc = "";

			gxTv_SdtWWPGridState_DynamicFilter_Value = "";

			gxTv_SdtWWPGridState_DynamicFilter_Valuedsc = "";

			gxTv_SdtWWPGridState_DynamicFilter_Valueto = "";

			gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc = "";

		}

		public SdtWWPGridState_DynamicFilter(IGxContext context)
		{
			this.context = context;	
			initialize();
		}

		#region Json
		private static Hashtable mapper;
		public override string JsonMap(string value)
		{
			if (mapper == null)
			{
				mapper = new Hashtable();
				mapper["d"] = "Dsc";

			}
			return (string)mapper[value]; ;
		}

		public override void ToJSON()
		{
			ToJSON(true) ;
			return;
		}

		public override void ToJSON(bool includeState)
		{
			AddObjectProperty("Selected", gxTpr_Selected, false);


			AddObjectProperty("d", gxTpr_Dsc, false);


			AddObjectProperty("Value", gxTpr_Value, false);


			AddObjectProperty("ValueDsc", gxTpr_Valuedsc, false);


			AddObjectProperty("Operator", gxTpr_Operator, false);


			AddObjectProperty("ValueTo", gxTpr_Valueto, false);


			AddObjectProperty("ValueToDsc", gxTpr_Valuetodsc, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Selected")]
		[XmlElement(ElementName="Selected")]
		public string gxTpr_Selected
		{
			get {
				return gxTv_SdtWWPGridState_DynamicFilter_Selected; 
			}
			set {
				gxTv_SdtWWPGridState_DynamicFilter_Selected = value;
				SetDirty("Selected");
			}
		}




		[SoapElement(ElementName="Dsc")]
		[XmlElement(ElementName="Dsc")]
		public string gxTpr_Dsc
		{
			get {
				return gxTv_SdtWWPGridState_DynamicFilter_Dsc; 
			}
			set {
				gxTv_SdtWWPGridState_DynamicFilter_Dsc_N = false;
				gxTv_SdtWWPGridState_DynamicFilter_Dsc = value;
				SetDirty("Dsc");
			}
		}

		public bool ShouldSerializegxTpr_Dsc()

		{
			return !gxTv_SdtWWPGridState_DynamicFilter_Dsc_N;

		}



		[SoapElement(ElementName="Value")]
		[XmlElement(ElementName="Value")]
		public string gxTpr_Value
		{
			get {
				return gxTv_SdtWWPGridState_DynamicFilter_Value; 
			}
			set {
				gxTv_SdtWWPGridState_DynamicFilter_Value = value;
				SetDirty("Value");
			}
		}




		[SoapElement(ElementName="ValueDsc")]
		[XmlElement(ElementName="ValueDsc")]
		public string gxTpr_Valuedsc
		{
			get {
				return gxTv_SdtWWPGridState_DynamicFilter_Valuedsc; 
			}
			set {
				gxTv_SdtWWPGridState_DynamicFilter_Valuedsc_N = false;
				gxTv_SdtWWPGridState_DynamicFilter_Valuedsc = value;
				SetDirty("Valuedsc");
			}
		}

		public bool ShouldSerializegxTpr_Valuedsc()

		{
			return !gxTv_SdtWWPGridState_DynamicFilter_Valuedsc_N;

		}



		[SoapElement(ElementName="Operator")]
		[XmlElement(ElementName="Operator")]
		public short gxTpr_Operator
		{
			get {
				return gxTv_SdtWWPGridState_DynamicFilter_Operator; 
			}
			set {
				gxTv_SdtWWPGridState_DynamicFilter_Operator = value;
				SetDirty("Operator");
			}
		}




		[SoapElement(ElementName="ValueTo")]
		[XmlElement(ElementName="ValueTo")]
		public string gxTpr_Valueto
		{
			get {
				return gxTv_SdtWWPGridState_DynamicFilter_Valueto; 
			}
			set {
				gxTv_SdtWWPGridState_DynamicFilter_Valueto = value;
				SetDirty("Valueto");
			}
		}




		[SoapElement(ElementName="ValueToDsc")]
		[XmlElement(ElementName="ValueToDsc")]
		public string gxTpr_Valuetodsc
		{
			get {
				return gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc; 
			}
			set {
				gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc_N = false;
				gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc = value;
				SetDirty("Valuetodsc");
			}
		}

		public bool ShouldSerializegxTpr_Valuetodsc()

		{
			return !gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc_N;

		}


		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Static Type Properties

		[XmlIgnore]
		private static GXTypeInfo _typeProps;
		protected override GXTypeInfo TypeInfo { get { return _typeProps; } set { _typeProps = value; } }

		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtWWPGridState_DynamicFilter_Selected = "";
			gxTv_SdtWWPGridState_DynamicFilter_Dsc = "";
			gxTv_SdtWWPGridState_DynamicFilter_Dsc_N = true;

			gxTv_SdtWWPGridState_DynamicFilter_Value = "";
			gxTv_SdtWWPGridState_DynamicFilter_Valuedsc = "";
			gxTv_SdtWWPGridState_DynamicFilter_Valuedsc_N = true;


			gxTv_SdtWWPGridState_DynamicFilter_Valueto = "";
			gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc = "";
			gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtWWPGridState_DynamicFilter_Selected;
		 

		protected string gxTv_SdtWWPGridState_DynamicFilter_Dsc;
		protected bool gxTv_SdtWWPGridState_DynamicFilter_Dsc_N;
		 

		protected string gxTv_SdtWWPGridState_DynamicFilter_Value;
		 

		protected string gxTv_SdtWWPGridState_DynamicFilter_Valuedsc;
		protected bool gxTv_SdtWWPGridState_DynamicFilter_Valuedsc_N;
		 

		protected short gxTv_SdtWWPGridState_DynamicFilter_Operator;
		 

		protected string gxTv_SdtWWPGridState_DynamicFilter_Valueto;
		 

		protected string gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc;
		protected bool gxTv_SdtWWPGridState_DynamicFilter_Valuetodsc_N;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"WWPGridState.DynamicFilter", Namespace="DesignSystem")]
	public class SdtWWPGridState_DynamicFilter_RESTInterface : GxGenericCollectionItem<SdtWWPGridState_DynamicFilter>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWWPGridState_DynamicFilter_RESTInterface( ) : base()
		{	
		}

		public SdtWWPGridState_DynamicFilter_RESTInterface( SdtWWPGridState_DynamicFilter psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Selected", Order=0)]
		public  string gxTpr_Selected
		{
			get { 
				return sdt.gxTpr_Selected;

			}
			set { 
				 sdt.gxTpr_Selected = value;
			}
		}

		[DataMember(Name="d", Order=1)]
		public  string gxTpr_Dsc
		{
			get { 
				return sdt.gxTpr_Dsc;

			}
			set { 
				 sdt.gxTpr_Dsc = value;
			}
		}

		[DataMember(Name="Value", Order=2)]
		public  string gxTpr_Value
		{
			get { 
				return sdt.gxTpr_Value;

			}
			set { 
				 sdt.gxTpr_Value = value;
			}
		}

		[DataMember(Name="ValueDsc", Order=3)]
		public  string gxTpr_Valuedsc
		{
			get { 
				return sdt.gxTpr_Valuedsc;

			}
			set { 
				 sdt.gxTpr_Valuedsc = value;
			}
		}

		[DataMember(Name="Operator", Order=4)]
		public short gxTpr_Operator
		{
			get { 
				return sdt.gxTpr_Operator;

			}
			set { 
				sdt.gxTpr_Operator = value;
			}
		}

		[DataMember(Name="ValueTo", Order=5)]
		public  string gxTpr_Valueto
		{
			get { 
				return sdt.gxTpr_Valueto;

			}
			set { 
				 sdt.gxTpr_Valueto = value;
			}
		}

		[DataMember(Name="ValueToDsc", Order=6)]
		public  string gxTpr_Valuetodsc
		{
			get { 
				return sdt.gxTpr_Valuetodsc;

			}
			set { 
				 sdt.gxTpr_Valuetodsc = value;
			}
		}


		#endregion

		public SdtWWPGridState_DynamicFilter sdt
		{
			get { 
				return (SdtWWPGridState_DynamicFilter)Sdt;
			}
			set { 
				Sdt = value;
			}
		}

		[OnDeserializing]
		void checkSdt( StreamingContext ctx )
		{
			if ( sdt == null )
			{
				sdt = new SdtWWPGridState_DynamicFilter() ;
			}
		}
	}
	#endregion
}