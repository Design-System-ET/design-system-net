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
namespace DesignSystem.Programs.wwpbaseobjects {
   public class loadwwpcontext : GXProcedure
   {
      public loadwwpcontext( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public loadwwpcontext( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out DesignSystem.Programs.wwpbaseobjects.SdtWWPContext aP0_Context )
      {
         this.AV8Context = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context) ;
         initialize();
         ExecuteImpl();
         aP0_Context=this.AV8Context;
      }

      public DesignSystem.Programs.wwpbaseobjects.SdtWWPContext executeUdp( )
      {
         execute(out aP0_Context);
         return AV8Context ;
      }

      public void executeSubmit( out DesignSystem.Programs.wwpbaseobjects.SdtWWPContext aP0_Context )
      {
         this.AV8Context = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context) ;
         SubmitImpl();
         aP0_Context=this.AV8Context;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8Context.FromXml(AV9Session.Get("wwpcontext"), null, "", "");
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
         AV8Context = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV9Session = context.GetSession();
         /* GeneXus formulas. */
      }

      private IGxSession AV9Session ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8Context ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext aP0_Context ;
   }

}
