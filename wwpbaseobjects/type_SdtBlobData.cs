/*
				   File: type_SdtBlobData
			Description: BlobData
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
	[XmlRoot(ElementName="BlobData")]
	[XmlType(TypeName="BlobData" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtBlobData : GxUserType
	{
		public SdtBlobData( )
		{
			/* Constructor for serialization */
			gxTv_SdtBlobData_Path = "";

			gxTv_SdtBlobData_Filename = "";

			gxTv_SdtBlobData_Fileextension = "";

		}

		public SdtBlobData(IGxContext context)
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
			AddObjectProperty("path", gxTpr_Path, false);


			AddObjectProperty("filename", gxTpr_Filename, false);


			AddObjectProperty("fileextension", gxTpr_Fileextension, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="path")]
		[XmlElement(ElementName="path")]
		public string gxTpr_Path
		{
			get {
				return gxTv_SdtBlobData_Path; 
			}
			set {
				gxTv_SdtBlobData_Path = value;
				SetDirty("Path");
			}
		}




		[SoapElement(ElementName="filename")]
		[XmlElement(ElementName="filename")]
		public string gxTpr_Filename
		{
			get {
				return gxTv_SdtBlobData_Filename; 
			}
			set {
				gxTv_SdtBlobData_Filename = value;
				SetDirty("Filename");
			}
		}




		[SoapElement(ElementName="fileextension")]
		[XmlElement(ElementName="fileextension")]
		public string gxTpr_Fileextension
		{
			get {
				return gxTv_SdtBlobData_Fileextension; 
			}
			set {
				gxTv_SdtBlobData_Fileextension = value;
				SetDirty("Fileextension");
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
			gxTv_SdtBlobData_Path = "";
			gxTv_SdtBlobData_Filename = "";
			gxTv_SdtBlobData_Fileextension = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtBlobData_Path;
		 

		protected string gxTv_SdtBlobData_Filename;
		 

		protected string gxTv_SdtBlobData_Fileextension;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"BlobData", Namespace="DesignSystem")]
	public class SdtBlobData_RESTInterface : GxGenericCollectionItem<SdtBlobData>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtBlobData_RESTInterface( ) : base()
		{	
		}

		public SdtBlobData_RESTInterface( SdtBlobData psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="path", Order=0)]
		public  string gxTpr_Path
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Path);

			}
			set { 
				 sdt.gxTpr_Path = value;
			}
		}

		[DataMember(Name="filename", Order=1)]
		public  string gxTpr_Filename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Filename);

			}
			set { 
				 sdt.gxTpr_Filename = value;
			}
		}

		[DataMember(Name="fileextension", Order=2)]
		public  string gxTpr_Fileextension
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Fileextension);

			}
			set { 
				 sdt.gxTpr_Fileextension = value;
			}
		}


		#endregion

		public SdtBlobData sdt
		{
			get { 
				return (SdtBlobData)Sdt;
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
				sdt = new SdtBlobData() ;
			}
		}
	}
	#endregion
}