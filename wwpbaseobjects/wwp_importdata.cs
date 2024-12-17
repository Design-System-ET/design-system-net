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
   public class wwp_importdata : GXProcedure
   {
      public wwp_importdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_importdata( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_SelctionName ,
                           string aP1_ImportType ,
                           string aP2_FilePath ,
                           string aP3_ExtraParmsJson ,
                           out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP4_Messages ,
                           out bool aP5_IsOk )
      {
         this.AV12SelctionName = aP0_SelctionName;
         this.AV9ImportType = aP1_ImportType;
         this.AV8FilePath = aP2_FilePath;
         this.AV14ExtraParmsJson = aP3_ExtraParmsJson;
         this.AV11Messages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         this.AV10IsOk = false ;
         initialize();
         ExecuteImpl();
         aP4_Messages=this.AV11Messages;
         aP5_IsOk=this.AV10IsOk;
      }

      public bool executeUdp( string aP0_SelctionName ,
                              string aP1_ImportType ,
                              string aP2_FilePath ,
                              string aP3_ExtraParmsJson ,
                              out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP4_Messages )
      {
         execute(aP0_SelctionName, aP1_ImportType, aP2_FilePath, aP3_ExtraParmsJson, out aP4_Messages, out aP5_IsOk);
         return AV10IsOk ;
      }

      public void executeSubmit( string aP0_SelctionName ,
                                 string aP1_ImportType ,
                                 string aP2_FilePath ,
                                 string aP3_ExtraParmsJson ,
                                 out GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP4_Messages ,
                                 out bool aP5_IsOk )
      {
         this.AV12SelctionName = aP0_SelctionName;
         this.AV9ImportType = aP1_ImportType;
         this.AV8FilePath = aP2_FilePath;
         this.AV14ExtraParmsJson = aP3_ExtraParmsJson;
         this.AV11Messages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus") ;
         this.AV10IsOk = false ;
         SubmitImpl();
         aP4_Messages=this.AV11Messages;
         aP5_IsOk=this.AV10IsOk;
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
         AV11Messages = new GXBaseCollection<GeneXus.Utils.SdtMessages_Message>( context, "Message", "GeneXus");
         /* GeneXus formulas. */
      }

      private bool AV10IsOk ;
      private string AV12SelctionName ;
      private string AV9ImportType ;
      private string AV8FilePath ;
      private string AV14ExtraParmsJson ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> AV11Messages ;
      private GXBaseCollection<GeneXus.Utils.SdtMessages_Message> aP4_Messages ;
      private bool aP5_IsOk ;
   }

}
