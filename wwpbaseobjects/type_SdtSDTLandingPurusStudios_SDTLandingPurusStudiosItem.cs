/*
				   File: type_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem
			Description: SDTLandingPurusStudios
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
	[XmlRoot(ElementName="SDTLandingPurusStudiosItem")]
	[XmlType(TypeName="SDTLandingPurusStudiosItem" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem : GxUserType
	{
		public SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage = "";
			gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage_gxi = "";
			gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiotitle = "";

			gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiodescription = "";

		}

		public SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem(IGxContext context)
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
			AddObjectProperty("StudioImage", gxTpr_Studioimage, false);
			AddObjectProperty("StudioImage_GXI", gxTpr_Studioimage_gxi, false);



			AddObjectProperty("StudioTitle", gxTpr_Studiotitle, false);


			AddObjectProperty("StudioDescription", gxTpr_Studiodescription, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="StudioImage")]
		[XmlElement(ElementName="StudioImage")]
		[GxUpload()]

		public string gxTpr_Studioimage
		{
			get {
				return gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage; 
			}
			set {
				gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage = value;
				SetDirty("Studioimage");
			}
		}


		[SoapElement(ElementName="StudioImage_GXI" )]
		[XmlElement(ElementName="StudioImage_GXI" )]
		public string gxTpr_Studioimage_gxi
		{
			get {
				return gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage_gxi ;
			}
			set {
				gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage_gxi = value;
				SetDirty("Studioimage_gxi");
			}
		}

		[SoapElement(ElementName="StudioTitle")]
		[XmlElement(ElementName="StudioTitle")]
		public string gxTpr_Studiotitle
		{
			get {
				return gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiotitle; 
			}
			set {
				gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiotitle = value;
				SetDirty("Studiotitle");
			}
		}




		[SoapElement(ElementName="StudioDescription")]
		[XmlElement(ElementName="StudioDescription")]
		public string gxTpr_Studiodescription
		{
			get {
				return gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiodescription; 
			}
			set {
				gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiodescription = value;
				SetDirty("Studiodescription");
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
			gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage = "";gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage_gxi = "";
			gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiotitle = "";
			gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiodescription = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage_gxi;
		protected string gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studioimage;
		 

		protected string gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiotitle;
		 

		protected string gxTv_SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_Studiodescription;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"SDTLandingPurusStudiosItem", Namespace="DesignSystem")]
	public class SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_RESTInterface : GxGenericCollectionItem<SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_RESTInterface( ) : base()
		{	
		}

		public SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem_RESTInterface( SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="StudioImage", Order=0)]
		[GxUpload()]
		public  string gxTpr_Studioimage
		{
			get { 
				return (!String.IsNullOrEmpty(StringUtil.RTrim( sdt.gxTpr_Studioimage)) ? PathUtil.RelativePath( sdt.gxTpr_Studioimage) : StringUtil.RTrim( sdt.gxTpr_Studioimage_gxi));

			}
			set { 
				 sdt.gxTpr_Studioimage = value;
			}
		}

		[DataMember(Name="StudioTitle", Order=1)]
		public  string gxTpr_Studiotitle
		{
			get { 
				return sdt.gxTpr_Studiotitle;

			}
			set { 
				 sdt.gxTpr_Studiotitle = value;
			}
		}

		[DataMember(Name="StudioDescription", Order=2)]
		public  string gxTpr_Studiodescription
		{
			get { 
				return sdt.gxTpr_Studiodescription;

			}
			set { 
				 sdt.gxTpr_Studiodescription = value;
			}
		}


		#endregion

		public SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem sdt
		{
			get { 
				return (SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem)Sdt;
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
				sdt = new SdtSDTLandingPurusStudios_SDTLandingPurusStudiosItem() ;
			}
		}
	}
	#endregion
}