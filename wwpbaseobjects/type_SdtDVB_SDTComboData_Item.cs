/*
				   File: type_SdtDVB_SDTComboData_Item
			Description: DVB_SDTComboData
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
	[XmlRoot(ElementName="Item")]
	[XmlType(TypeName="Item" , Namespace="" )]
	[Serializable]
	public class SdtDVB_SDTComboData_Item : GxUserType
	{
		public SdtDVB_SDTComboData_Item( )
		{
			/* Constructor for serialization */
			gxTv_SdtDVB_SDTComboData_Item_Id = "";

			gxTv_SdtDVB_SDTComboData_Item_Title = "";

		}

		public SdtDVB_SDTComboData_Item(IGxContext context)
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
				mapper["T"] = "Title";
				mapper["C"] = "Children";

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
			AddObjectProperty("ID", gxTpr_Id, false);


			AddObjectProperty("T", gxTpr_Title, false);

			if (gxTv_SdtDVB_SDTComboData_Item_Children != null)
			{
				AddObjectProperty("C", gxTv_SdtDVB_SDTComboData_Item_Children, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ID")]
		[XmlElement(ElementName="ID")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtDVB_SDTComboData_Item_Id; 
			}
			set {
				gxTv_SdtDVB_SDTComboData_Item_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="Title")]
		[XmlElement(ElementName="Title")]
		public string gxTpr_Title
		{
			get {
				return gxTv_SdtDVB_SDTComboData_Item_Title; 
			}
			set {
				gxTv_SdtDVB_SDTComboData_Item_Title = value;
				SetDirty("Title");
			}
		}




		[SoapElement(ElementName="Children" )]
		[XmlArray(ElementName="Children"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> gxTpr_Children_GXBaseCollection
		{
			get {
				if ( gxTv_SdtDVB_SDTComboData_Item_Children == null )
				{
					gxTv_SdtDVB_SDTComboData_Item_Children = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Data", "");
				}
				return gxTv_SdtDVB_SDTComboData_Item_Children;
			}
			set {
				gxTv_SdtDVB_SDTComboData_Item_Children_N = false;
				gxTv_SdtDVB_SDTComboData_Item_Children = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> gxTpr_Children
		{
			get {
				if ( gxTv_SdtDVB_SDTComboData_Item_Children == null )
				{
					gxTv_SdtDVB_SDTComboData_Item_Children = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item>( context, "Data", "");
				}
				gxTv_SdtDVB_SDTComboData_Item_Children_N = false;
				return gxTv_SdtDVB_SDTComboData_Item_Children ;
			}
			set {
				gxTv_SdtDVB_SDTComboData_Item_Children_N = false;
				gxTv_SdtDVB_SDTComboData_Item_Children = value;
				SetDirty("Children");
			}
		}

		public void gxTv_SdtDVB_SDTComboData_Item_Children_SetNull()
		{
			gxTv_SdtDVB_SDTComboData_Item_Children_N = true;
			gxTv_SdtDVB_SDTComboData_Item_Children = null;
		}

		public bool gxTv_SdtDVB_SDTComboData_Item_Children_IsNull()
		{
			return gxTv_SdtDVB_SDTComboData_Item_Children == null;
		}
		public bool ShouldSerializegxTpr_Children_GXBaseCollection_Json()
		{
			return gxTv_SdtDVB_SDTComboData_Item_Children != null && gxTv_SdtDVB_SDTComboData_Item_Children.Count > 0;

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
			gxTv_SdtDVB_SDTComboData_Item_Id = "";
			gxTv_SdtDVB_SDTComboData_Item_Title = "";

			gxTv_SdtDVB_SDTComboData_Item_Children_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtDVB_SDTComboData_Item_Id;
		 

		protected string gxTv_SdtDVB_SDTComboData_Item_Title;
		 
		protected bool gxTv_SdtDVB_SDTComboData_Item_Children_N;
		protected GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item> gxTv_SdtDVB_SDTComboData_Item_Children = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"Item", Namespace="")]
	public class SdtDVB_SDTComboData_Item_RESTInterface : GxGenericCollectionItem<SdtDVB_SDTComboData_Item>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDVB_SDTComboData_Item_RESTInterface( ) : base()
		{	
		}

		public SdtDVB_SDTComboData_Item_RESTInterface( SdtDVB_SDTComboData_Item psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="ID", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return sdt.gxTpr_Id;

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}

		[DataMember(Name="T", Order=1)]
		public  string gxTpr_Title
		{
			get { 
				return sdt.gxTpr_Title;

			}
			set { 
				 sdt.gxTpr_Title = value;
			}
		}

		[DataMember(Name="C", Order=2, EmitDefaultValue=false)]
		public  GxGenericCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item_RESTInterface> gxTpr_Children
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Children_GXBaseCollection_Json())
					return new GxGenericCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTComboData_Item_RESTInterface>(sdt.gxTpr_Children);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Children);
			}
		}


		#endregion

		public SdtDVB_SDTComboData_Item sdt
		{
			get { 
				return (SdtDVB_SDTComboData_Item)Sdt;
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
				sdt = new SdtDVB_SDTComboData_Item() ;
			}
		}
	}
	#endregion
}