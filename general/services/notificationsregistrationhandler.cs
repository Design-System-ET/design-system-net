using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.Procedure;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs.general.services {
   public class notificationsregistrationhandler : GXProcedure
   {
      public notificationsregistrationhandler( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
      }

      public notificationsregistrationhandler( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( short aP0_DeviceType ,
                           string aP1_DeviceId ,
                           string aP2_DeviceToken ,
                           string aP3_DeviceName )
      {
         this.AV2DeviceType = aP0_DeviceType;
         this.AV3DeviceId = aP1_DeviceId;
         this.AV4DeviceToken = aP2_DeviceToken;
         this.AV5DeviceName = aP3_DeviceName;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( short aP0_DeviceType ,
                                 string aP1_DeviceId ,
                                 string aP2_DeviceToken ,
                                 string aP3_DeviceName )
      {
         this.AV2DeviceType = aP0_DeviceType;
         this.AV3DeviceId = aP1_DeviceId;
         this.AV4DeviceToken = aP2_DeviceToken;
         this.AV5DeviceName = aP3_DeviceName;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         args = new Object[] {(short)AV2DeviceType,(string)AV3DeviceId,(string)AV4DeviceToken,(string)AV5DeviceName} ;
         ClassLoader.Execute("general.services.anotificationsregistrationhandler","DesignSystem.Programs","general.services.anotificationsregistrationhandler", new Object[] {context }, "execute", args);
         if ( ( args != null ) && ( args.Length == 4 ) )
         {
         }
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         /* GeneXus formulas. */
      }

      private short AV2DeviceType ;
      private string AV3DeviceId ;
      private string AV4DeviceToken ;
      private string AV5DeviceName ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private Object[] args ;
   }

}
