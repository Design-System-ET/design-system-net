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
   public class dvmessagegetbasicnotificationmsg : GXProcedure
   {
      public dvmessagegetbasicnotificationmsg( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public dvmessagegetbasicnotificationmsg( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_Title ,
                           string aP1_Text ,
                           string aP2_Type ,
                           string aP3_ControlSelector ,
                           string aP4_Hide ,
                           string aP5_ClickRedirectURL ,
                           out string aP6_Parms )
      {
         this.AV14Title = aP0_Title;
         this.AV13Text = aP1_Text;
         this.AV15Type = aP2_Type;
         this.AV10ControlSelector = aP3_ControlSelector;
         this.AV11Hide = aP4_Hide;
         this.AV9ClickRedirectURL = aP5_ClickRedirectURL;
         this.AV12Parms = "" ;
         initialize();
         ExecuteImpl();
         aP6_Parms=this.AV12Parms;
      }

      public string executeUdp( string aP0_Title ,
                                string aP1_Text ,
                                string aP2_Type ,
                                string aP3_ControlSelector ,
                                string aP4_Hide ,
                                string aP5_ClickRedirectURL )
      {
         execute(aP0_Title, aP1_Text, aP2_Type, aP3_ControlSelector, aP4_Hide, aP5_ClickRedirectURL, out aP6_Parms);
         return AV12Parms ;
      }

      public void executeSubmit( string aP0_Title ,
                                 string aP1_Text ,
                                 string aP2_Type ,
                                 string aP3_ControlSelector ,
                                 string aP4_Hide ,
                                 string aP5_ClickRedirectURL ,
                                 out string aP6_Parms )
      {
         this.AV14Title = aP0_Title;
         this.AV13Text = aP1_Text;
         this.AV15Type = aP2_Type;
         this.AV10ControlSelector = aP3_ControlSelector;
         this.AV11Hide = aP4_Hide;
         this.AV9ClickRedirectURL = aP5_ClickRedirectURL;
         this.AV12Parms = "" ;
         SubmitImpl();
         aP6_Parms=this.AV12Parms;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV12Parms;
         new DesignSystem.Programs.wwpbaseobjects.dvmessagegetadvancednotificationmsg(context ).execute(  AV14Title,  AV13Text,  AV15Type,  AV10ControlSelector,  AV11Hide,  "false",  "",  AV9ClickRedirectURL, out  GXt_char1) ;
         AV12Parms = GXt_char1;
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
         AV12Parms = "";
         GXt_char1 = "";
         /* GeneXus formulas. */
      }

      private string AV14Title ;
      private string AV13Text ;
      private string AV15Type ;
      private string AV10ControlSelector ;
      private string AV11Hide ;
      private string GXt_char1 ;
      private string AV9ClickRedirectURL ;
      private string AV12Parms ;
      private string aP6_Parms ;
   }

}
