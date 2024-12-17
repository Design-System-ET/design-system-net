using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
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
   public class anotificationsregistrationhandler : GXProcedure
   {
      public static int Main( string[] args )
      {
         return new general.services.anotificationsregistrationhandler().MainImpl(args); ;
      }

      public int executeCmdLine( string[] args )
      {
         return ExecuteCmdLine(args); ;
      }

      protected override int ExecuteCmdLine( string[] args )
      {
          short aP0_DeviceType ;
         string aP1_DeviceId = new string(' ',0)  ;
         string aP2_DeviceToken = new string(' ',0)  ;
         string aP3_DeviceName = new string(' ',0)  ;
         if ( 0 < args.Length )
         {
            aP0_DeviceType=((short)(NumberUtil.Val( (string)(args[0]), ".")));
         }
         else
         {
            aP0_DeviceType=0;
         }
         if ( 1 < args.Length )
         {
            aP1_DeviceId=((string)(args[1]));
         }
         else
         {
            aP1_DeviceId="";
         }
         if ( 2 < args.Length )
         {
            aP2_DeviceToken=((string)(args[2]));
         }
         else
         {
            aP2_DeviceToken="";
         }
         if ( 3 < args.Length )
         {
            aP3_DeviceName=((string)(args[3]));
         }
         else
         {
            aP3_DeviceName="";
         }
         execute(aP0_DeviceType, aP1_DeviceId, aP2_DeviceToken, aP3_DeviceName);
         return GX.GXRuntime.ExitCode ;
      }

      protected override bool IntegratedSecurityEnabled
      {
         get {
            return true ;
         }

      }

      protected override GAMSecurityLevel IntegratedSecurityLevel
      {
         get {
            return GAMSecurityLevel.SecurityHigh ;
         }

      }

      protected override string ExecutePermissionPrefix
      {
         get {
            return "notificationsregistrationhandler_Services_Execute" ;
         }

      }

      public anotificationsregistrationhandler( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public anotificationsregistrationhandler( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( short aP0_DeviceType ,
                           string aP1_DeviceId ,
                           string aP2_DeviceToken ,
                           string aP3_DeviceName )
      {
         this.AV11DeviceType = aP0_DeviceType;
         this.AV8DeviceId = aP1_DeviceId;
         this.AV10DeviceToken = aP2_DeviceToken;
         this.AV9DeviceName = aP3_DeviceName;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( short aP0_DeviceType ,
                                 string aP1_DeviceId ,
                                 string aP2_DeviceToken ,
                                 string aP3_DeviceName )
      {
         this.AV11DeviceType = aP0_DeviceType;
         this.AV8DeviceId = aP1_DeviceId;
         this.AV10DeviceToken = aP2_DeviceToken;
         this.AV9DeviceName = aP3_DeviceName;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         context.StatusMessage( StringUtil.Trim( AV10DeviceToken) );
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
         ExitApp();
      }

      public override void initialize( )
      {
         /* GeneXus formulas. */
      }

      private short AV11DeviceType ;
      private string AV8DeviceId ;
      private string AV10DeviceToken ;
      private string AV9DeviceName ;
   }

}
