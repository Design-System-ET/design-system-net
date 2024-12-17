/*
				   File: type_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem
			Description: Result
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
	[XmlRoot(ElementName="WWP_SearchResults.WWP_SearchResultsItem.ResultItem")]
	[XmlType(TypeName="WWP_SearchResults.WWP_SearchResultsItem.ResultItem" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem : GxUserType
	{
		public SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Displaytype = "";

			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description = "";

			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description2 = "";

			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description3 = "";

			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Url = "";

		}

		public SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem(IGxContext context)
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
			AddObjectProperty("DisplayType", gxTpr_Displaytype, false);


			AddObjectProperty("Description", gxTpr_Description, false);


			AddObjectProperty("Description2", gxTpr_Description2, false);


			AddObjectProperty("Description3", gxTpr_Description3, false);


			AddObjectProperty("Url", gxTpr_Url, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="DisplayType")]
		[XmlElement(ElementName="DisplayType")]
		public string gxTpr_Displaytype
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Displaytype; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Displaytype = value;
				SetDirty("Displaytype");
			}
		}




		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="Description2")]
		[XmlElement(ElementName="Description2")]
		public string gxTpr_Description2
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description2; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description2 = value;
				SetDirty("Description2");
			}
		}




		[SoapElement(ElementName="Description3")]
		[XmlElement(ElementName="Description3")]
		public string gxTpr_Description3
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description3; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description3 = value;
				SetDirty("Description3");
			}
		}




		[SoapElement(ElementName="Url")]
		[XmlElement(ElementName="Url")]
		public string gxTpr_Url
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Url; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Url = value;
				SetDirty("Url");
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
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Displaytype = "";
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description = "";
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description2 = "";
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description3 = "";
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Url = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Displaytype;
		 

		protected string gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description;
		 

		protected string gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description2;
		 

		protected string gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Description3;
		 

		protected string gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_Url;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"WWP_SearchResults.WWP_SearchResultsItem.ResultItem", Namespace="DesignSystem")]
	public class SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_RESTInterface : GxGenericCollectionItem<SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_RESTInterface( ) : base()
		{	
		}

		public SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_RESTInterface( SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="DisplayType", Order=0)]
		public  string gxTpr_Displaytype
		{
			get { 
				return sdt.gxTpr_Displaytype;

			}
			set { 
				 sdt.gxTpr_Displaytype = value;
			}
		}

		[DataMember(Name="Description", Order=1)]
		public  string gxTpr_Description
		{
			get { 
				return sdt.gxTpr_Description;

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[DataMember(Name="Description2", Order=2)]
		public  string gxTpr_Description2
		{
			get { 
				return sdt.gxTpr_Description2;

			}
			set { 
				 sdt.gxTpr_Description2 = value;
			}
		}

		[DataMember(Name="Description3", Order=3)]
		public  string gxTpr_Description3
		{
			get { 
				return sdt.gxTpr_Description3;

			}
			set { 
				 sdt.gxTpr_Description3 = value;
			}
		}

		[DataMember(Name="Url", Order=4)]
		public  string gxTpr_Url
		{
			get { 
				return sdt.gxTpr_Url;

			}
			set { 
				 sdt.gxTpr_Url = value;
			}
		}


		#endregion

		public SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem sdt
		{
			get { 
				return (SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)Sdt;
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
				sdt = new SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem() ;
			}
		}
	}
	#endregion
}