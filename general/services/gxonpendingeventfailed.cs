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
   public class gxonpendingeventfailed : GXProcedure
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
            return "gxonpendingeventfailed_Services_Execute" ;
         }

      }

      public gxonpendingeventfailed( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gxonpendingeventfailed( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem aP0_PendingEvent ,
                           string aP1_BCName ,
                           string aP2_BCJson ,
                           GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem aP3_EventResult ,
                           GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP4_GxSyncroInfo ,
                           out bool aP5_Continue )
      {
         this.AV8PendingEvent = aP0_PendingEvent;
         this.AV9BCName = aP1_BCName;
         this.AV10BCJson = aP2_BCJson;
         this.AV12EventResult = aP3_EventResult;
         this.GxSyncroInfo = aP4_GxSyncroInfo;
         this.AV11Continue = false ;
         initialize();
         ExecuteImpl();
         aP5_Continue=this.AV11Continue;
      }

      public bool executeUdp( GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem aP0_PendingEvent ,
                              string aP1_BCName ,
                              string aP2_BCJson ,
                              GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem aP3_EventResult ,
                              GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP4_GxSyncroInfo )
      {
         execute(aP0_PendingEvent, aP1_BCName, aP2_BCJson, aP3_EventResult, aP4_GxSyncroInfo, out aP5_Continue);
         return AV11Continue ;
      }

      public void executeSubmit( GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem aP0_PendingEvent ,
                                 string aP1_BCName ,
                                 string aP2_BCJson ,
                                 GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem aP3_EventResult ,
                                 GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP4_GxSyncroInfo ,
                                 out bool aP5_Continue )
      {
         this.AV8PendingEvent = aP0_PendingEvent;
         this.AV9BCName = aP1_BCName;
         this.AV10BCJson = aP2_BCJson;
         this.AV12EventResult = aP3_EventResult;
         this.GxSyncroInfo = aP4_GxSyncroInfo;
         this.AV11Continue = false ;
         SubmitImpl();
         aP5_Continue=this.AV11Continue;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV11Continue = true;
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

      private bool AV11Continue ;
      private string AV10BCJson ;
      private string AV9BCName ;
      private GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventList_SynchronizationEventListItem AV8PendingEvent ;
      private GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem AV12EventResult ;
      private GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo GxSyncroInfo ;
      private bool aP5_Continue ;
   }

}
