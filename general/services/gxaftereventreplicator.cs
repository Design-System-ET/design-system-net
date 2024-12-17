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
   public class gxaftereventreplicator : GXProcedure
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
            return "gxaftereventreplicator_Services_Execute" ;
         }

      }

      public gxaftereventreplicator( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public gxaftereventreplicator( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> aP0_EventResults ,
                           GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP1_GxSynchroInfo )
      {
         this.AV8EventResults = aP0_EventResults;
         this.GxSynchroInfo = aP1_GxSynchroInfo;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> aP0_EventResults ,
                                 GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo aP1_GxSynchroInfo )
      {
         this.AV8EventResults = aP0_EventResults;
         this.GxSynchroInfo = aP1_GxSynchroInfo;
         SubmitImpl();
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

      private GXBaseCollection<GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationEventResultList_SynchronizationEventResultListItem> AV8EventResults ;
      private GeneXus.Core.genexus.sd.synchronization.SdtSynchronizationInfo GxSynchroInfo ;
   }

}
