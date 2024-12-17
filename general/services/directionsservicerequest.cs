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
   public class directionsservicerequest : GXProcedure
   {
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
            return "directionsservicerequest_Services_Execute" ;
         }

      }

      public directionsservicerequest( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public directionsservicerequest( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_DirectionsServiceProvider ,
                           GeneXus.Core.genexus.common.SdtDirectionsRequestParameters aP1_DirectionsRequestParameters ,
                           out GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute> aP2_Routes ,
                           out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP3_errorMessages )
      {
         this.AV9DirectionsServiceProvider = aP0_DirectionsServiceProvider;
         this.AV8DirectionsRequestParameters = aP1_DirectionsRequestParameters;
         this.AV12Routes = new GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute>( context, "Route", "GeneXus") ;
         this.AV11errorMessages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         initialize();
         ExecuteImpl();
         aP2_Routes=this.AV12Routes;
         aP3_errorMessages=this.AV11errorMessages;
      }

      public GXBaseCollection<GeneXus.Utils.SdtMessages_Message> executeUdp( string aP0_DirectionsServiceProvider ,
                                                                             GeneXus.Core.genexus.common.SdtDirectionsRequestParameters aP1_DirectionsRequestParameters ,
                                                                             out GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute> aP2_Routes )
      {
         execute(aP0_DirectionsServiceProvider, aP1_DirectionsRequestParameters, out aP2_Routes, out aP3_errorMessages);
         return AV11errorMessages ;
      }

      public void executeSubmit( string aP0_DirectionsServiceProvider ,
                                 GeneXus.Core.genexus.common.SdtDirectionsRequestParameters aP1_DirectionsRequestParameters ,
                                 out GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute> aP2_Routes ,
                                 out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP3_errorMessages )
      {
         this.AV9DirectionsServiceProvider = aP0_DirectionsServiceProvider;
         this.AV8DirectionsRequestParameters = aP1_DirectionsRequestParameters;
         this.AV12Routes = new GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute>( context, "Route", "GeneXus") ;
         this.AV11errorMessages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         SubmitImpl();
         aP2_Routes=this.AV12Routes;
         aP3_errorMessages=this.AV11errorMessages;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( StringUtil.StrCmp(AV9DirectionsServiceProvider, "Google") == 0 )
         {
            new GeneXus.Core.genexus.common.googledirectionsservicerequest(context ).execute(  AV8DirectionsRequestParameters, out  AV12Routes, out  AV11errorMessages) ;
         }
         else
         {
            AV10errorMessage.gxTpr_Description = context.GetMessage( "Unknown Error", "");
            AV10errorMessage.gxTpr_Type = 1;
            AV11errorMessages.Add(AV10errorMessage, 0);
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
         ExitApp();
      }

      public override void initialize( )
      {
         AV12Routes = new GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute>( context, "Route", "GeneXus");
         AV11errorMessages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         AV10errorMessage = new GeneXus.Utils.SdtMessages_Message(context);
         /* GeneXus formulas. */
      }

      private string AV9DirectionsServiceProvider ;
      private GeneXus.Core.genexus.common.SdtDirectionsRequestParameters AV8DirectionsRequestParameters ;
      private GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute> AV12Routes ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV11errorMessages ;
      private GeneXus.Utils.SdtMessages_Message AV10errorMessage ;
      private GXBaseCollection<GeneXus.Core.genexus.common.SdtRoute> aP2_Routes ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP3_errorMessages ;
   }

}
