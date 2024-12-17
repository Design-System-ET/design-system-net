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
   public class gxbeforeeventreplicator : GXProcedure
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
            return "gxbeforeeventreplicator_Services_Execute" ;
         }

      }

      public gxbeforeeventreplicator( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gxbeforeeventreplicator( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( ref GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem> aP0_GxPendingEvents ,
                           GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP1_GxSyncroInfo ,
                           ref GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> aP2_EventResults )
      {
         this.GxPendingEvents = aP0_GxPendingEvents;
         this.GxSyncroInfo = aP1_GxSyncroInfo;
         this.AV8EventResults = aP2_EventResults;
         initialize();
         ExecuteImpl();
         aP0_GxPendingEvents=this.GxPendingEvents;
         aP2_EventResults=this.AV8EventResults;
      }

      public GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> executeUdp( ref GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem> aP0_GxPendingEvents ,
                                                                                                                                                        GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP1_GxSyncroInfo )
      {
         execute(ref aP0_GxPendingEvents, aP1_GxSyncroInfo, ref aP2_EventResults);
         return AV8EventResults ;
      }

      public void executeSubmit( ref GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem> aP0_GxPendingEvents ,
                                 GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP1_GxSyncroInfo ,
                                 ref GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> aP2_EventResults )
      {
         this.GxPendingEvents = aP0_GxPendingEvents;
         this.GxSyncroInfo = aP1_GxSyncroInfo;
         this.AV8EventResults = aP2_EventResults;
         SubmitImpl();
         aP0_GxPendingEvents=this.GxPendingEvents;
         aP2_EventResults=this.AV8EventResults;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
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

      private GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem> GxPendingEvents ;
      private GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem> aP0_GxPendingEvents ;
      private GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo GxSyncroInfo ;
      private GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> AV8EventResults ;
      private GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> aP2_EventResults ;
   }

}
