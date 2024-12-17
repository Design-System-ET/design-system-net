/*
				   File: type_SdtWWPConfFormattingVariable
			Description: WWPConfFormattingVariable
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
namespace DesignSystem.Programs.workwithplus
{
	[XmlRoot(ElementName="WWPConfFormattingVariable")]
	[XmlType(TypeName="WWPConfFormattingVariable" , Namespace="DesignSystem" )]
	[Serializable]
	public class SdtWWPConfFormattingVariable : GxUserType
	{
		public SdtWWPConfFormattingVariable( )
		{
			/* Constructor for serialization */
			gxTv_SdtWWPConfFormattingVariable_Name = "";

			gxTv_SdtWWPConfFormattingVariable_Description = "";

			gxTv_SdtWWPConfFormattingVariable_Defaultvalue = "";

		}

		public SdtWWPConfFormattingVariable(IGxContext context)
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
			AddObjectProperty("Name", gxTpr_Name, false);


			AddObjectProperty("Description", gxTpr_Description, false);


			AddObjectProperty("DefaultValue", gxTpr_Defaultvalue, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Name")]
		[XmlElement(ElementName="Name")]
		public string gxTpr_Name
		{
			get {
				return gxTv_SdtWWPConfFormattingVariable_Name; 
			}
			set {
				gxTv_SdtWWPConfFormattingVariable_Name = value;
				SetDirty("Name");
			}
		}




		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtWWPConfFormattingVariable_Description; 
			}
			set {
				gxTv_SdtWWPConfFormattingVariable_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="DefaultValue")]
		[XmlElement(ElementName="DefaultValue")]
		public string gxTpr_Defaultvalue
		{
			get {
				return gxTv_SdtWWPConfFormattingVariable_Defaultvalue; 
			}
			set {
				gxTv_SdtWWPConfFormattingVariable_Defaultvalue = value;
				SetDirty("Defaultvalue");
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
			gxTv_SdtWWPConfFormattingVariable_Name = "";
			gxTv_SdtWWPConfFormattingVariable_Description = "";
			gxTv_SdtWWPConfFormattingVariable_Defaultvalue = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtWWPConfFormattingVariable_Name;
		 

		protected string gxTv_SdtWWPConfFormattingVariable_Description;
		 

		protected string gxTv_SdtWWPConfFormattingVariable_Defaultvalue;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"WWPConfFormattingVariable", Namespace="DesignSystem")]
	public class SdtWWPConfFormattingVariable_RESTInterface : GxGenericCollectionItem<SdtWWPConfFormattingVariable>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWWPConfFormattingVariable_RESTInterface( ) : base()
		{	
		}

		public SdtWWPConfFormattingVariable_RESTInterface( SdtWWPConfFormattingVariable psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Name", Order=0)]
		public  string gxTpr_Name
		{
			get { 
				return sdt.gxTpr_Name;

			}
			set { 
				 sdt.gxTpr_Name = value;
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

		[DataMember(Name="DefaultValue", Order=2)]
		public  string gxTpr_Defaultvalue
		{
			get { 
				return sdt.gxTpr_Defaultvalue;

			}
			set { 
				 sdt.gxTpr_Defaultvalue = value;
			}
		}


		#endregion

		public SdtWWPConfFormattingVariable sdt
		{
			get { 
				return (SdtWWPConfFormattingVariable)Sdt;
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
				sdt = new SdtWWPConfFormattingVariable() ;
			}
		}
	}
	#endregion
}