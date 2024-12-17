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
   public class getwwptitlesettingsicons : GXProcedure
   {
      public getwwptitlesettingsicons( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public getwwptitlesettingsicons( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( out DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons aP0_TitleSettingsIcons )
      {
         this.AV8TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context) ;
         initialize();
         ExecuteImpl();
         aP0_TitleSettingsIcons=this.AV8TitleSettingsIcons;
      }

      public DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons executeUdp( )
      {
         execute(out aP0_TitleSettingsIcons);
         return AV8TitleSettingsIcons ;
      }

      public void executeSubmit( out DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons aP0_TitleSettingsIcons )
      {
         this.AV8TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context) ;
         SubmitImpl();
         aP0_TitleSettingsIcons=this.AV8TitleSettingsIcons;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         AV8TitleSettingsIcons.gxTpr_Default_fi = "fas fa-caret-down CSTitleIcon";
         AV8TitleSettingsIcons.gxTpr_Filtered_fi = "fas fa-filter CSTitleIcon";
         AV8TitleSettingsIcons.gxTpr_Sortedasc_fi = "fas fa-long-arrow-alt-up CSTitleIcon";
         AV8TitleSettingsIcons.gxTpr_Sorteddsc_fi = "fas fa-long-arrow-alt-down CSTitleIcon";
         AV8TitleSettingsIcons.gxTpr_Filteredsortedasc_fi = "fas fa-long-arrow-alt-up CSTitleIconDanger";
         AV8TitleSettingsIcons.gxTpr_Filteredsorteddsc_fi = "fas fa-long-arrow-alt-down CSTitleIconDanger";
         AV8TitleSettingsIcons.gxTpr_Optionsortasc_fi = "fas fa-sort-amount-up CSDropDownFI";
         AV8TitleSettingsIcons.gxTpr_Optionsortdsc_fi = "fas fa-sort-amount-down CSDropDownFI";
         AV8TitleSettingsIcons.gxTpr_Optionapplyfilter_fi = "fas fa-search";
         AV8TitleSettingsIcons.gxTpr_Optionfilteringdata_fi = "fa fa-spinner fa-pulse fa-fw CSDropDownFI";
         AV8TitleSettingsIcons.gxTpr_Optioncleanfilters_fi = "fas fa-times CSDropDownFI";
         AV8TitleSettingsIcons.gxTpr_Selectedoption_fi = "fas fa-filter CSDropDownFilter";
         AV8TitleSettingsIcons.gxTpr_Multiseloption_fi = "far fa-square CSDropDownFilter";
         AV8TitleSettingsIcons.gxTpr_Multiselseloption_fi = "far fa-check-square CSDropDownFilter";
         AV8TitleSettingsIcons.gxTpr_Treeviewcollapse_fi = "fas fa-angle-down";
         AV8TitleSettingsIcons.gxTpr_Treeviewexpand_fi = "fas fa-angle-right";
         AV8TitleSettingsIcons.gxTpr_Fixleft_fi = "fa fa-rotate-270 fa-table CSDropDownFI";
         AV8TitleSettingsIcons.gxTpr_Fixright_fi = "fa fa-rotate-90 fa-table CSDropDownFI";
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
         AV8TitleSettingsIcons = new DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons(context);
         /* GeneXus formulas. */
      }

      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons AV8TitleSettingsIcons ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVB_SDTDropDownOptionsTitleSettingsIcons aP0_TitleSettingsIcons ;
   }

}
