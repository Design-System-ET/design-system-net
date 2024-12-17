/*
				   File: type_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem
			Description: SDTHomeLandingSampleLogos
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
	[XmlRoot(ElementName="SDTHomeLandingSampleLogosItem")]
	[XmlType(TypeName="SDTHomeLandingSampleLogosItem" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem : GxUserType
	{
		public SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo = "";
			gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo_gxi = "";
		}

		public SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem(IGxContext context)
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
			AddObjectProperty("Logo", gxTpr_Logo, false);
			AddObjectProperty("Logo_GXI", gxTpr_Logo_gxi, false);


			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Logo")]
		[XmlElement(ElementName="Logo")]
		[GxUpload()]

		public string gxTpr_Logo
		{
			get {
				return gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo; 
			}
			set {
				gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo = value;
				SetDirty("Logo");
			}
		}


		[SoapElement(ElementName="Logo_GXI" )]
		[XmlElement(ElementName="Logo_GXI" )]
		public string gxTpr_Logo_gxi
		{
			get {
				return gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo_gxi ;
			}
			set {
				gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo_gxi = value;
				SetDirty("Logo_gxi");
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
			gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo = "";gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo_gxi = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo_gxi;
		protected string gxTv_SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_Logo;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"SDTHomeLandingSampleLogosItem", Namespace="DesignSystem")]
	public class SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_RESTInterface : GxGenericCollectionItem<SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_RESTInterface( ) : base()
		{	
		}

		public SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem_RESTInterface( SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Logo", Order=0)]
		[GxUpload()]
		public  string gxTpr_Logo
		{
			get { 
				return (!String.IsNullOrEmpty(StringUtil.RTrim( sdt.gxTpr_Logo)) ? PathUtil.RelativePath( sdt.gxTpr_Logo) : StringUtil.RTrim( sdt.gxTpr_Logo_gxi));

			}
			set { 
				 sdt.gxTpr_Logo = value;
			}
		}


		#endregion

		public SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem sdt
		{
			get { 
				return (SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem)Sdt;
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
				sdt = new SdtSDTHomeLandingSampleLogos_SDTHomeLandingSampleLogosItem() ;
			}
		}
	}
	#endregion
}