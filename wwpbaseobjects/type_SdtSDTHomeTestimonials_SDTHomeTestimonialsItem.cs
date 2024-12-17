/*
				   File: type_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem
			Description: SDTHomeTestimonials
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
	[XmlRoot(ElementName="SDTHomeTestimonialsItem")]
	[XmlType(TypeName="SDTHomeTestimonialsItem" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtSDTHomeTestimonials_SDTHomeTestimonialsItem : GxUserType
	{
		public SdtSDTHomeTestimonials_SDTHomeTestimonialsItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsname = "";

			gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialscompany = "";

			gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsdescription = "";

		}

		public SdtSDTHomeTestimonials_SDTHomeTestimonialsItem(IGxContext context)
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
			AddObjectProperty("SDTHomeTestimonialsName", gxTpr_Sdthometestimonialsname, false);


			AddObjectProperty("SDTHomeTestimonialsCompany", gxTpr_Sdthometestimonialscompany, false);


			AddObjectProperty("SDTHomeTestimonialsDescription", gxTpr_Sdthometestimonialsdescription, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="SDTHomeTestimonialsName")]
		[XmlElement(ElementName="SDTHomeTestimonialsName")]
		public string gxTpr_Sdthometestimonialsname
		{
			get {
				return gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsname; 
			}
			set {
				gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsname = value;
				SetDirty("Sdthometestimonialsname");
			}
		}




		[SoapElement(ElementName="SDTHomeTestimonialsCompany")]
		[XmlElement(ElementName="SDTHomeTestimonialsCompany")]
		public string gxTpr_Sdthometestimonialscompany
		{
			get {
				return gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialscompany; 
			}
			set {
				gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialscompany = value;
				SetDirty("Sdthometestimonialscompany");
			}
		}




		[SoapElement(ElementName="SDTHomeTestimonialsDescription")]
		[XmlElement(ElementName="SDTHomeTestimonialsDescription")]
		public string gxTpr_Sdthometestimonialsdescription
		{
			get {
				return gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsdescription; 
			}
			set {
				gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsdescription = value;
				SetDirty("Sdthometestimonialsdescription");
			}
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
			gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsname = "";
			gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialscompany = "";
			gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsdescription = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsname;
		 

		protected string gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialscompany;
		 

		protected string gxTv_SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_Sdthometestimonialsdescription;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"SDTHomeTestimonialsItem", Namespace="DesignSystem")]
	public class SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_RESTInterface : GxGenericCollectionItem<SdtSDTHomeTestimonials_SDTHomeTestimonialsItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_RESTInterface( ) : base()
		{	
		}

		public SdtSDTHomeTestimonials_SDTHomeTestimonialsItem_RESTInterface( SdtSDTHomeTestimonials_SDTHomeTestimonialsItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="SDTHomeTestimonialsName", Order=0)]
		public  string gxTpr_Sdthometestimonialsname
		{
			get { 
				return sdt.gxTpr_Sdthometestimonialsname;

			}
			set { 
				 sdt.gxTpr_Sdthometestimonialsname = value;
			}
		}

		[DataMember(Name="SDTHomeTestimonialsCompany", Order=1)]
		public  string gxTpr_Sdthometestimonialscompany
		{
			get { 
				return sdt.gxTpr_Sdthometestimonialscompany;

			}
			set { 
				 sdt.gxTpr_Sdthometestimonialscompany = value;
			}
		}

		[DataMember(Name="SDTHomeTestimonialsDescription", Order=2)]
		public  string gxTpr_Sdthometestimonialsdescription
		{
			get { 
				return sdt.gxTpr_Sdthometestimonialsdescription;

			}
			set { 
				 sdt.gxTpr_Sdthometestimonialsdescription = value;
			}
		}


		#endregion

		public SdtSDTHomeTestimonials_SDTHomeTestimonialsItem sdt
		{
			get { 
				return (SdtSDTHomeTestimonials_SDTHomeTestimonialsItem)Sdt;
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
				sdt = new SdtSDTHomeTestimonials_SDTHomeTestimonialsItem() ;
			}
		}
	}
	#endregion
}