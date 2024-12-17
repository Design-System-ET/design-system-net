/*
				   File: type_SdtWWPColumnsSelector
			Description: WWPColumnsSelector
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
	[XmlRoot(ElementName="WWPColumnsSelector")]
	[XmlType(TypeName="WWPColumnsSelector" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtWWPColumnsSelector : GxUserType
	{
		public SdtWWPColumnsSelector( )
		{
			/* Constructor for serialization */
		}

		public SdtWWPColumnsSelector(IGxContext context)
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
			if (gxTv_SdtWWPColumnsSelector_Columns != null)
			{
				AddObjectProperty("Columns", gxTv_SdtWWPColumnsSelector_Columns, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Columns" )]
		[XmlArray(ElementName="Columns"  )]
		[XmlArrayItemAttribute(ElementName="Column" , IsNullable=false )]
		public GXBaseCollection<SdtWWPColumnsSelector_Column> gxTpr_Columns
		{
			get {
				if ( gxTv_SdtWWPColumnsSelector_Columns == null )
				{
					gxTv_SdtWWPColumnsSelector_Columns = new GXBaseCollection<SdtWWPColumnsSelector_Column>( context, "WWPColumnsSelector.Column", "");
				}
				return gxTv_SdtWWPColumnsSelector_Columns;
			}
			set {
				gxTv_SdtWWPColumnsSelector_Columns_N = false;
				gxTv_SdtWWPColumnsSelector_Columns = value;
				SetDirty("Columns");
			}
		}

		public void gxTv_SdtWWPColumnsSelector_Columns_SetNull()
		{
			gxTv_SdtWWPColumnsSelector_Columns_N = true;
			gxTv_SdtWWPColumnsSelector_Columns = null;
		}

		public bool gxTv_SdtWWPColumnsSelector_Columns_IsNull()
		{
			return gxTv_SdtWWPColumnsSelector_Columns == null;
		}
		public bool ShouldSerializegxTpr_Columns_GxSimpleCollection_Json()
		{
			return gxTv_SdtWWPColumnsSelector_Columns != null && gxTv_SdtWWPColumnsSelector_Columns.Count > 0;

		}


		public override bool ShouldSerializeSdtJson()
		{
			return (
				ShouldSerializegxTpr_Columns_GxSimpleCollection_Json() || 
				false);
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
			gxTv_SdtWWPColumnsSelector_Columns_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtWWPColumnsSelector_Columns_N;
		protected GXBaseCollection<SdtWWPColumnsSelector_Column> gxTv_SdtWWPColumnsSelector_Columns = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"WWPColumnsSelector", Namespace="DesignSystem")]
	public class SdtWWPColumnsSelector_RESTInterface : GxGenericCollectionItem<SdtWWPColumnsSelector>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWWPColumnsSelector_RESTInterface( ) : base()
		{	
		}

		public SdtWWPColumnsSelector_RESTInterface( SdtWWPColumnsSelector psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Columns", Order=0, EmitDefaultValue=false)]
		public GxGenericCollection<SdtWWPColumnsSelector_Column_RESTInterface> gxTpr_Columns
		{
			get {
				if (sdt.ShouldSerializegxTpr_Columns_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtWWPColumnsSelector_Column_RESTInterface>(sdt.gxTpr_Columns);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Columns);
			}
		}


		#endregion

		public SdtWWPColumnsSelector sdt
		{
			get { 
				return (SdtWWPColumnsSelector)Sdt;
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
				sdt = new SdtWWPColumnsSelector() ;
			}
		}
	}
	#endregion
}