/*
				   File: type_SdtWWP_SearchResults_WWP_SearchResultsItem
			Description: WWP_SearchResults
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
	[XmlRoot(ElementName="WWP_SearchResultsItem")]
	[XmlType(TypeName="WWP_SearchResultsItem" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtWWP_SearchResults_WWP_SearchResultsItem : GxUserType
	{
		public SdtWWP_SearchResults_WWP_SearchResultsItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryname = "";

			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryicon = "";

		}

		public SdtWWP_SearchResults_WWP_SearchResultsItem(IGxContext context)
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
			AddObjectProperty("CategoryName", gxTpr_Categoryname, false);


			AddObjectProperty("CategoryIcon", gxTpr_Categoryicon, false);


			AddObjectProperty("ShowingAllResults", gxTpr_Showingallresults, false);


			AddObjectProperty("OrderIndex", gxTpr_Orderindex, false);

			if (gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result != null)
			{
				AddObjectProperty("Result", gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="CategoryName")]
		[XmlElement(ElementName="CategoryName")]
		public string gxTpr_Categoryname
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryname; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryname = value;
				SetDirty("Categoryname");
			}
		}




		[SoapElement(ElementName="CategoryIcon")]
		[XmlElement(ElementName="CategoryIcon")]
		public string gxTpr_Categoryicon
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryicon; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryicon = value;
				SetDirty("Categoryicon");
			}
		}




		[SoapElement(ElementName="ShowingAllResults")]
		[XmlElement(ElementName="ShowingAllResults")]
		public bool gxTpr_Showingallresults
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Showingallresults; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Showingallresults = value;
				SetDirty("Showingallresults");
			}
		}




		[SoapElement(ElementName="OrderIndex")]
		[XmlElement(ElementName="OrderIndex")]
		public short gxTpr_Orderindex
		{
			get {
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Orderindex; 
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Orderindex = value;
				SetDirty("Orderindex");
			}
		}




		[SoapElement(ElementName="Result" )]
		[XmlArray(ElementName="Result"  )]
		[XmlArrayItemAttribute(ElementName="ResultItem" , IsNullable=false )]
		public GXBaseCollection<SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem> gxTpr_Result
		{
			get {
				if ( gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result == null )
				{
					gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result = new GXBaseCollection<SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem>( context, "WWP_SearchResults.WWP_SearchResultsItem.ResultItem", "");
				}
				return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result;
			}
			set {
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result_N = false;
				gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result = value;
				SetDirty("Result");
			}
		}

		public void gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result_SetNull()
		{
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result_N = true;
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result = null;
		}

		public bool gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result_IsNull()
		{
			return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result == null;
		}
		public bool ShouldSerializegxTpr_Result_GxSimpleCollection_Json()
		{
			return gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result != null && gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result.Count > 0;

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
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryname = "";
			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryicon = "";



			gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryname;
		 

		protected string gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Categoryicon;
		 

		protected bool gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Showingallresults;
		 

		protected short gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Orderindex;
		 
		protected bool gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result_N;
		protected GXBaseCollection<SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem> gxTv_SdtWWP_SearchResults_WWP_SearchResultsItem_Result = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"WWP_SearchResultsItem", Namespace="DesignSystem")]
	public class SdtWWP_SearchResults_WWP_SearchResultsItem_RESTInterface : GxGenericCollectionItem<SdtWWP_SearchResults_WWP_SearchResultsItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWWP_SearchResults_WWP_SearchResultsItem_RESTInterface( ) : base()
		{	
		}

		public SdtWWP_SearchResults_WWP_SearchResultsItem_RESTInterface( SdtWWP_SearchResults_WWP_SearchResultsItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="CategoryName", Order=0)]
		public  string gxTpr_Categoryname
		{
			get { 
				return sdt.gxTpr_Categoryname;

			}
			set { 
				 sdt.gxTpr_Categoryname = value;
			}
		}

		[DataMember(Name="CategoryIcon", Order=1)]
		public  string gxTpr_Categoryicon
		{
			get { 
				return sdt.gxTpr_Categoryicon;

			}
			set { 
				 sdt.gxTpr_Categoryicon = value;
			}
		}

		[DataMember(Name="ShowingAllResults", Order=2)]
		public bool gxTpr_Showingallresults
		{
			get { 
				return sdt.gxTpr_Showingallresults;

			}
			set { 
				sdt.gxTpr_Showingallresults = value;
			}
		}

		[DataMember(Name="OrderIndex", Order=3)]
		public short gxTpr_Orderindex
		{
			get { 
				return sdt.gxTpr_Orderindex;

			}
			set { 
				sdt.gxTpr_Orderindex = value;
			}
		}

		[DataMember(Name="Result", Order=4, EmitDefaultValue=false)]
		public GxGenericCollection<SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_RESTInterface> gxTpr_Result
		{
			get {
				if (sdt.ShouldSerializegxTpr_Result_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem_RESTInterface>(sdt.gxTpr_Result);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Result);
			}
		}


		#endregion

		public SdtWWP_SearchResults_WWP_SearchResultsItem sdt
		{
			get { 
				return (SdtWWP_SearchResults_WWP_SearchResultsItem)Sdt;
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
				sdt = new SdtWWP_SearchResults_WWP_SearchResultsItem() ;
			}
		}
	}
	#endregion
}