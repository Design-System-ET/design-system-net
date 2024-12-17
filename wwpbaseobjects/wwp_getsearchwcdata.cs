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
   public class wwp_getsearchwcdata : GXProcedure
   {
      public wwp_getsearchwcdata( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_getsearchwcdata( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( string aP0_SearchText ,
                           short aP1_MaxCategories ,
                           short aP2_MaxItemsPerCategory ,
                           ref GxSimpleCollection<string> aP3_AdvFilterEntities ,
                           out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> aP4_WWP_SearchResults )
      {
         this.AV11SearchText = aP0_SearchText;
         this.AV9MaxCategories = aP1_MaxCategories;
         this.AV10MaxItemsPerCategory = aP2_MaxItemsPerCategory;
         this.AV8AdvFilterEntities = aP3_AdvFilterEntities;
         this.AV12WWP_SearchResults = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem") ;
         initialize();
         ExecuteImpl();
         aP3_AdvFilterEntities=this.AV8AdvFilterEntities;
         aP4_WWP_SearchResults=this.AV12WWP_SearchResults;
      }

      public GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> executeUdp( string aP0_SearchText ,
                                                                                                                           short aP1_MaxCategories ,
                                                                                                                           short aP2_MaxItemsPerCategory ,
                                                                                                                           ref GxSimpleCollection<string> aP3_AdvFilterEntities )
      {
         execute(aP0_SearchText, aP1_MaxCategories, aP2_MaxItemsPerCategory, ref aP3_AdvFilterEntities, out aP4_WWP_SearchResults);
         return AV12WWP_SearchResults ;
      }

      public void executeSubmit( string aP0_SearchText ,
                                 short aP1_MaxCategories ,
                                 short aP2_MaxItemsPerCategory ,
                                 ref GxSimpleCollection<string> aP3_AdvFilterEntities ,
                                 out GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> aP4_WWP_SearchResults )
      {
         this.AV11SearchText = aP0_SearchText;
         this.AV9MaxCategories = aP1_MaxCategories;
         this.AV10MaxItemsPerCategory = aP2_MaxItemsPerCategory;
         this.AV8AdvFilterEntities = aP3_AdvFilterEntities;
         this.AV12WWP_SearchResults = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem") ;
         SubmitImpl();
         aP3_AdvFilterEntities=this.AV8AdvFilterEntities;
         aP4_WWP_SearchResults=this.AV12WWP_SearchResults;
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV11SearchText)) )
         {
            /* Execute user subroutine: 'SEARCH IN TRANSACTIONS' */
            S111 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            /* Execute user subroutine: 'SEARCH MENU ITEMS' */
            S121 ();
            if ( returnInSub )
            {
               cleanup();
               if (true) return;
            }
            while ( AV12WWP_SearchResults.Count > AV9MaxCategories )
            {
               AV12WWP_SearchResults.RemoveItem(AV12WWP_SearchResults.Count);
            }
         }
         else
         {
            if ( (0==AV10MaxItemsPerCategory) )
            {
               /* Execute user subroutine: 'GET ALL ENTITIES DESCRIPTIONS' */
               S131 ();
               if ( returnInSub )
               {
                  cleanup();
                  if (true) return;
               }
            }
         }
         cleanup();
      }

      protected void S111( )
      {
         /* 'SEARCH IN TRANSACTIONS' Routine */
         returnInSub = false;
         AV12WWP_SearchResults.Sort("[OrderIndex]");
      }

      protected void S121( )
      {
         /* 'SEARCH MENU ITEMS' Routine */
         returnInSub = false;
         if ( ( AV8AdvFilterEntities.Count == 0 ) || ( AV8AdvFilterEntities.IndexOf(context.GetMessage( "WWP_Search_MenuOptions", "")) > 0 ) )
         {
            GXt_objcol_SdtDVelop_Menu_Item1 = AV18MenuData;
            new DesignSystem.Programs.wwpbaseobjects.menuoptionsdata(context ).execute( out  GXt_objcol_SdtDVelop_Menu_Item1) ;
            AV18MenuData = GXt_objcol_SdtDVelop_Menu_Item1;
            new DesignSystem.Programs.wwpbaseobjects.getmenuauthorizedoptions(context ).execute( ref  AV18MenuData) ;
            new DesignSystem.Programs.wwpbaseobjects.wwp_searchmenuoptions(context ).execute(  AV11SearchText,  AV18MenuData,  AV13CurrentMenuOptionPath, ref  AV20MenuOptions, ref  AV15MenuOptionsPaths) ;
            if ( AV20MenuOptions.Count > 0 )
            {
               AV21WWP_SearchResultItem = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem(context);
               AV21WWP_SearchResultItem.gxTpr_Categoryname = context.GetMessage( "WWP_Search_MenuOptions", "");
               AV21WWP_SearchResultItem.gxTpr_Showingallresults = true;
               AV12WWP_SearchResults.Add(AV21WWP_SearchResultItem, 0);
               AV14i = 1;
               AV22GXV1 = 1;
               while ( AV22GXV1 <= AV20MenuOptions.Count )
               {
                  AV19MenuDataItem = ((DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item)AV20MenuOptions.Item(AV22GXV1));
                  if ( AV21WWP_SearchResultItem.gxTpr_Result.Count >= AV10MaxItemsPerCategory )
                  {
                     AV21WWP_SearchResultItem.gxTpr_Showingallresults = false;
                     if (true) break;
                  }
                  else
                  {
                     AV17SearchResult = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem(context);
                     AV17SearchResult.gxTpr_Description = AV19MenuDataItem.gxTpr_Caption;
                     AV13CurrentMenuOptionPath.FromJSonString(((string)AV15MenuOptionsPaths.Item(AV14i)), null);
                     if ( AV13CurrentMenuOptionPath.Count > 0 )
                     {
                        AV23GXV2 = 1;
                        while ( AV23GXV2 <= AV13CurrentMenuOptionPath.Count )
                        {
                           AV16OptionPath = ((string)AV13CurrentMenuOptionPath.Item(AV23GXV2));
                           if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV17SearchResult.gxTpr_Description2)) )
                           {
                              AV17SearchResult.gxTpr_Description2 = AV17SearchResult.gxTpr_Description2+" > ";
                           }
                           AV17SearchResult.gxTpr_Description2 = AV17SearchResult.gxTpr_Description2+AV16OptionPath;
                           AV23GXV2 = (int)(AV23GXV2+1);
                        }
                        AV17SearchResult.gxTpr_Description3 = AV19MenuDataItem.gxTpr_Iconclass;
                        AV17SearchResult.gxTpr_Displaytype = (String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV17SearchResult.gxTpr_Description3))) ? "Title and subtitle" : "Title, subtitle and icon image");
                     }
                     else
                     {
                        AV17SearchResult.gxTpr_Description2 = AV19MenuDataItem.gxTpr_Iconclass;
                        AV17SearchResult.gxTpr_Displaytype = (String.IsNullOrEmpty(StringUtil.RTrim( StringUtil.Trim( AV17SearchResult.gxTpr_Description2))) ? "Title" : "Title and icon image");
                     }
                     AV17SearchResult.gxTpr_Url = AV19MenuDataItem.gxTpr_Link;
                     AV21WWP_SearchResultItem.gxTpr_Result.Add(AV17SearchResult, 0);
                  }
                  AV14i = (short)(AV14i+1);
                  AV22GXV1 = (int)(AV22GXV1+1);
               }
            }
         }
      }

      protected void S131( )
      {
         /* 'GET ALL ENTITIES DESCRIPTIONS' Routine */
         returnInSub = false;
         AV12WWP_SearchResults.Sort("[OrderIndex]");
         AV8AdvFilterEntities = (GxSimpleCollection<string>)(new GxSimpleCollection<string>());
         AV24GXV3 = 1;
         while ( AV24GXV3 <= AV12WWP_SearchResults.Count )
         {
            AV21WWP_SearchResultItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV12WWP_SearchResults.Item(AV24GXV3));
            AV8AdvFilterEntities.Add(AV21WWP_SearchResultItem.gxTpr_Categoryname, 0);
            AV24GXV3 = (int)(AV24GXV3+1);
         }
         AV8AdvFilterEntities.Add(context.GetMessage( "WWP_Search_MenuOptions", ""), 0);
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
         AV12WWP_SearchResults = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         AV18MenuData = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item>( context, "Item", "DesignSystem");
         GXt_objcol_SdtDVelop_Menu_Item1 = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item>( context, "Item", "DesignSystem");
         AV13CurrentMenuOptionPath = new GxSimpleCollection<string>();
         AV20MenuOptions = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item>( context, "Item", "DesignSystem");
         AV15MenuOptionsPaths = new GxSimpleCollection<string>();
         AV21WWP_SearchResultItem = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem(context);
         AV19MenuDataItem = new DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item(context);
         AV17SearchResult = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem(context);
         AV16OptionPath = "";
         /* GeneXus formulas. */
      }

      private short AV9MaxCategories ;
      private short AV10MaxItemsPerCategory ;
      private short AV14i ;
      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private int AV24GXV3 ;
      private bool returnInSub ;
      private string AV11SearchText ;
      private string AV16OptionPath ;
      private GxSimpleCollection<string> AV8AdvFilterEntities ;
      private GxSimpleCollection<string> aP3_AdvFilterEntities ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV12WWP_SearchResults ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> AV18MenuData ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> GXt_objcol_SdtDVelop_Menu_Item1 ;
      private GxSimpleCollection<string> AV13CurrentMenuOptionPath ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item> AV20MenuOptions ;
      private GxSimpleCollection<string> AV15MenuOptionsPaths ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem AV21WWP_SearchResultItem ;
      private DesignSystem.Programs.wwpbaseobjects.SdtDVelop_Menu_Item AV19MenuDataItem ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem AV17SearchResult ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> aP4_WWP_SearchResults ;
   }

}
