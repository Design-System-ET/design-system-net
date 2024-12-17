/*
				   File: type_SdtGxMap
			Description: GxMap
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


namespace DesignSystem.Programs
{
	[XmlRoot(ElementName="GxMap")]
	[XmlType(TypeName="GxMap" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtGxMap : GxUserType
	{
		public SdtGxMap( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxMap_Maptype = "";

			gxTv_SdtGxMap_Maplatitude = "";

			gxTv_SdtGxMap_Maplongitude = "";

		}

		public SdtGxMap(IGxContext context)
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
			AddObjectProperty("MapType", gxTpr_Maptype, false);


			AddObjectProperty("MapZoom", gxTpr_Mapzoom, false);


			AddObjectProperty("MapLatitude", gxTpr_Maplatitude, false);


			AddObjectProperty("MapLongitude", gxTpr_Maplongitude, false);

			if (gxTv_SdtGxMap_Circles != null)
			{
				AddObjectProperty("Circles", gxTv_SdtGxMap_Circles, false);
			}
			if (gxTv_SdtGxMap_Points != null)
			{
				AddObjectProperty("Points", gxTv_SdtGxMap_Points, false);
			}
			if (gxTv_SdtGxMap_Polygons != null)
			{
				AddObjectProperty("Polygons", gxTv_SdtGxMap_Polygons, false);
			}
			if (gxTv_SdtGxMap_Lines != null)
			{
				AddObjectProperty("Lines", gxTv_SdtGxMap_Lines, false);
			}
			if (gxTv_SdtGxMap_Routing != null)
			{
				AddObjectProperty("Routing", gxTv_SdtGxMap_Routing, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="MapType")]
		[XmlElement(ElementName="MapType")]
		public string gxTpr_Maptype
		{
			get {
				return gxTv_SdtGxMap_Maptype; 
			}
			set {
				gxTv_SdtGxMap_Maptype = value;
				SetDirty("Maptype");
			}
		}




		[SoapElement(ElementName="MapZoom")]
		[XmlElement(ElementName="MapZoom")]
		public short gxTpr_Mapzoom
		{
			get {
				return gxTv_SdtGxMap_Mapzoom; 
			}
			set {
				gxTv_SdtGxMap_Mapzoom = value;
				SetDirty("Mapzoom");
			}
		}




		[SoapElement(ElementName="MapLatitude")]
		[XmlElement(ElementName="MapLatitude")]
		public string gxTpr_Maplatitude
		{
			get {
				return gxTv_SdtGxMap_Maplatitude; 
			}
			set {
				gxTv_SdtGxMap_Maplatitude = value;
				SetDirty("Maplatitude");
			}
		}




		[SoapElement(ElementName="MapLongitude")]
		[XmlElement(ElementName="MapLongitude")]
		public string gxTpr_Maplongitude
		{
			get {
				return gxTv_SdtGxMap_Maplongitude; 
			}
			set {
				gxTv_SdtGxMap_Maplongitude = value;
				SetDirty("Maplongitude");
			}
		}




		[SoapElement(ElementName="Circles" )]
		[XmlArray(ElementName="Circles"  )]
		[XmlArrayItemAttribute(ElementName="Circle" , IsNullable=false )]
		public GXBaseCollection<SdtGxMap_Circle> gxTpr_Circles
		{
			get {
				if ( gxTv_SdtGxMap_Circles == null )
				{
					gxTv_SdtGxMap_Circles = new GXBaseCollection<SdtGxMap_Circle>( context, "GxMap.Circle", "");
				}
				return gxTv_SdtGxMap_Circles;
			}
			set {
				gxTv_SdtGxMap_Circles_N = false;
				gxTv_SdtGxMap_Circles = value;
				SetDirty("Circles");
			}
		}

		public void gxTv_SdtGxMap_Circles_SetNull()
		{
			gxTv_SdtGxMap_Circles_N = true;
			gxTv_SdtGxMap_Circles = null;
		}

		public bool gxTv_SdtGxMap_Circles_IsNull()
		{
			return gxTv_SdtGxMap_Circles == null;
		}
		public bool ShouldSerializegxTpr_Circles_GxSimpleCollection_Json()
		{
			return gxTv_SdtGxMap_Circles != null && gxTv_SdtGxMap_Circles.Count > 0;

		}



		[SoapElement(ElementName="Points" )]
		[XmlArray(ElementName="Points"  )]
		[XmlArrayItemAttribute(ElementName="Point" , IsNullable=false )]
		public GXBaseCollection<SdtGxMap_Point> gxTpr_Points
		{
			get {
				if ( gxTv_SdtGxMap_Points == null )
				{
					gxTv_SdtGxMap_Points = new GXBaseCollection<SdtGxMap_Point>( context, "GxMap.Point", "");
				}
				return gxTv_SdtGxMap_Points;
			}
			set {
				gxTv_SdtGxMap_Points_N = false;
				gxTv_SdtGxMap_Points = value;
				SetDirty("Points");
			}
		}

		public void gxTv_SdtGxMap_Points_SetNull()
		{
			gxTv_SdtGxMap_Points_N = true;
			gxTv_SdtGxMap_Points = null;
		}

		public bool gxTv_SdtGxMap_Points_IsNull()
		{
			return gxTv_SdtGxMap_Points == null;
		}
		public bool ShouldSerializegxTpr_Points_GxSimpleCollection_Json()
		{
			return gxTv_SdtGxMap_Points != null && gxTv_SdtGxMap_Points.Count > 0;

		}



		[SoapElement(ElementName="Polygons" )]
		[XmlArray(ElementName="Polygons"  )]
		[XmlArrayItemAttribute(ElementName="Polygon" , IsNullable=false )]
		public GXBaseCollection<SdtGxMap_Polygon> gxTpr_Polygons
		{
			get {
				if ( gxTv_SdtGxMap_Polygons == null )
				{
					gxTv_SdtGxMap_Polygons = new GXBaseCollection<SdtGxMap_Polygon>( context, "GxMap.Polygon", "");
				}
				return gxTv_SdtGxMap_Polygons;
			}
			set {
				gxTv_SdtGxMap_Polygons_N = false;
				gxTv_SdtGxMap_Polygons = value;
				SetDirty("Polygons");
			}
		}

		public void gxTv_SdtGxMap_Polygons_SetNull()
		{
			gxTv_SdtGxMap_Polygons_N = true;
			gxTv_SdtGxMap_Polygons = null;
		}

		public bool gxTv_SdtGxMap_Polygons_IsNull()
		{
			return gxTv_SdtGxMap_Polygons == null;
		}
		public bool ShouldSerializegxTpr_Polygons_GxSimpleCollection_Json()
		{
			return gxTv_SdtGxMap_Polygons != null && gxTv_SdtGxMap_Polygons.Count > 0;

		}



		[SoapElement(ElementName="Lines" )]
		[XmlArray(ElementName="Lines"  )]
		[XmlArrayItemAttribute(ElementName="Line" , IsNullable=false )]
		public GXBaseCollection<SdtGxMap_Line> gxTpr_Lines
		{
			get {
				if ( gxTv_SdtGxMap_Lines == null )
				{
					gxTv_SdtGxMap_Lines = new GXBaseCollection<SdtGxMap_Line>( context, "GxMap.Line", "");
				}
				return gxTv_SdtGxMap_Lines;
			}
			set {
				gxTv_SdtGxMap_Lines_N = false;
				gxTv_SdtGxMap_Lines = value;
				SetDirty("Lines");
			}
		}

		public void gxTv_SdtGxMap_Lines_SetNull()
		{
			gxTv_SdtGxMap_Lines_N = true;
			gxTv_SdtGxMap_Lines = null;
		}

		public bool gxTv_SdtGxMap_Lines_IsNull()
		{
			return gxTv_SdtGxMap_Lines == null;
		}
		public bool ShouldSerializegxTpr_Lines_GxSimpleCollection_Json()
		{
			return gxTv_SdtGxMap_Lines != null && gxTv_SdtGxMap_Lines.Count > 0;

		}



		[SoapElement(ElementName="Routing" )]
		[XmlArray(ElementName="Routing"  )]
		[XmlArrayItemAttribute(ElementName="RoutePoint" , IsNullable=false )]
		public GXBaseCollection<SdtGxMap_RoutePoint> gxTpr_Routing
		{
			get {
				if ( gxTv_SdtGxMap_Routing == null )
				{
					gxTv_SdtGxMap_Routing = new GXBaseCollection<SdtGxMap_RoutePoint>( context, "GxMap.RoutePoint", "");
				}
				return gxTv_SdtGxMap_Routing;
			}
			set {
				gxTv_SdtGxMap_Routing_N = false;
				gxTv_SdtGxMap_Routing = value;
				SetDirty("Routing");
			}
		}

		public void gxTv_SdtGxMap_Routing_SetNull()
		{
			gxTv_SdtGxMap_Routing_N = true;
			gxTv_SdtGxMap_Routing = null;
		}

		public bool gxTv_SdtGxMap_Routing_IsNull()
		{
			return gxTv_SdtGxMap_Routing == null;
		}
		public bool ShouldSerializegxTpr_Routing_GxSimpleCollection_Json()
		{
			return gxTv_SdtGxMap_Routing != null && gxTv_SdtGxMap_Routing.Count > 0;

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
			gxTv_SdtGxMap_Maptype = "";

			gxTv_SdtGxMap_Maplatitude = "";
			gxTv_SdtGxMap_Maplongitude = "";

			gxTv_SdtGxMap_Circles_N = true;


			gxTv_SdtGxMap_Points_N = true;


			gxTv_SdtGxMap_Polygons_N = true;


			gxTv_SdtGxMap_Lines_N = true;


			gxTv_SdtGxMap_Routing_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtGxMap_Maptype;
		 

		protected short gxTv_SdtGxMap_Mapzoom;
		 

		protected string gxTv_SdtGxMap_Maplatitude;
		 

		protected string gxTv_SdtGxMap_Maplongitude;
		 
		protected bool gxTv_SdtGxMap_Circles_N;
		protected GXBaseCollection<SdtGxMap_Circle> gxTv_SdtGxMap_Circles = null; 

		protected bool gxTv_SdtGxMap_Points_N;
		protected GXBaseCollection<SdtGxMap_Point> gxTv_SdtGxMap_Points = null; 

		protected bool gxTv_SdtGxMap_Polygons_N;
		protected GXBaseCollection<SdtGxMap_Polygon> gxTv_SdtGxMap_Polygons = null; 

		protected bool gxTv_SdtGxMap_Lines_N;
		protected GXBaseCollection<SdtGxMap_Line> gxTv_SdtGxMap_Lines = null; 

		protected bool gxTv_SdtGxMap_Routing_N;
		protected GXBaseCollection<SdtGxMap_RoutePoint> gxTv_SdtGxMap_Routing = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxMap", Namespace="DesignSystem")]
	public class SdtGxMap_RESTInterface : GxGenericCollectionItem<SdtGxMap>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxMap_RESTInterface( ) : base()
		{	
		}

		public SdtGxMap_RESTInterface( SdtGxMap psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="MapType", Order=0)]
		public  string gxTpr_Maptype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Maptype);

			}
			set { 
				 sdt.gxTpr_Maptype = value;
			}
		}

		[DataMember(Name="MapZoom", Order=1)]
		public short gxTpr_Mapzoom
		{
			get { 
				return sdt.gxTpr_Mapzoom;

			}
			set { 
				sdt.gxTpr_Mapzoom = value;
			}
		}

		[DataMember(Name="MapLatitude", Order=2)]
		public  string gxTpr_Maplatitude
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Maplatitude);

			}
			set { 
				 sdt.gxTpr_Maplatitude = value;
			}
		}

		[DataMember(Name="MapLongitude", Order=3)]
		public  string gxTpr_Maplongitude
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Maplongitude);

			}
			set { 
				 sdt.gxTpr_Maplongitude = value;
			}
		}

		[DataMember(Name="Circles", Order=4, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGxMap_Circle_RESTInterface> gxTpr_Circles
		{
			get {
				if (sdt.ShouldSerializegxTpr_Circles_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGxMap_Circle_RESTInterface>(sdt.gxTpr_Circles);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Circles);
			}
		}

		[DataMember(Name="Points", Order=5, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGxMap_Point_RESTInterface> gxTpr_Points
		{
			get {
				if (sdt.ShouldSerializegxTpr_Points_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGxMap_Point_RESTInterface>(sdt.gxTpr_Points);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Points);
			}
		}

		[DataMember(Name="Polygons", Order=6, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGxMap_Polygon_RESTInterface> gxTpr_Polygons
		{
			get {
				if (sdt.ShouldSerializegxTpr_Polygons_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGxMap_Polygon_RESTInterface>(sdt.gxTpr_Polygons);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Polygons);
			}
		}

		[DataMember(Name="Lines", Order=7, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGxMap_Line_RESTInterface> gxTpr_Lines
		{
			get {
				if (sdt.ShouldSerializegxTpr_Lines_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGxMap_Line_RESTInterface>(sdt.gxTpr_Lines);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Lines);
			}
		}

		[DataMember(Name="Routing", Order=8, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGxMap_RoutePoint_RESTInterface> gxTpr_Routing
		{
			get {
				if (sdt.ShouldSerializegxTpr_Routing_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGxMap_RoutePoint_RESTInterface>(sdt.gxTpr_Routing);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Routing);
			}
		}


		#endregion

		public SdtGxMap sdt
		{
			get { 
				return (SdtGxMap)Sdt;
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
				sdt = new SdtGxMap() ;
			}
		}
	}
	#endregion
}