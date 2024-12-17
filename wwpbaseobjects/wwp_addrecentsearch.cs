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
   public class wwp_addrecentsearch : GXProcedure
   {
      public wwp_addrecentsearch( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_addrecentsearch( IGxContext context )
      {
         this.context = context;
         IsMain = false;
      }

      public void execute( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> aP0_WWP_SearchResults ,
                           string aP1_Url ,
                           short aP2_MaxCategories ,
                           short aP3_MaxItemsPerCategory )
      {
         this.AV20WWP_SearchResults = aP0_WWP_SearchResults;
         this.AV18Url = aP1_Url;
         this.AV11MaxCategories = aP2_MaxCategories;
         this.AV12MaxItemsPerCategory = aP3_MaxItemsPerCategory;
         initialize();
         ExecuteImpl();
      }

      public void executeSubmit( GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> aP0_WWP_SearchResults ,
                                 string aP1_Url ,
                                 short aP2_MaxCategories ,
                                 short aP3_MaxItemsPerCategory )
      {
         this.AV20WWP_SearchResults = aP0_WWP_SearchResults;
         this.AV18Url = aP1_Url;
         this.AV11MaxCategories = aP2_MaxCategories;
         this.AV12MaxItemsPerCategory = aP3_MaxItemsPerCategory;
         SubmitImpl();
      }

      protected override void ExecutePrivate( )
      {
         /* GeneXus formulas */
         /* Output device settings */
         GXt_char1 = AV17RecentSearchResultsJson;
         new DesignSystem.Programs.wwpbaseobjects.loaduserkeyvalue(context ).execute(  "WWPRecentSearch", out  GXt_char1) ;
         AV17RecentSearchResultsJson = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV17RecentSearchResultsJson)) )
         {
            AV14RecentSearches = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         }
         else
         {
            AV14RecentSearches.FromJSonString(AV17RecentSearchResultsJson, null);
         }
         AV10Found = false;
         AV22GXV1 = 1;
         while ( AV22GXV1 <= AV20WWP_SearchResults.Count )
         {
            AV21WWP_SearchResultsItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV20WWP_SearchResults.Item(AV22GXV1));
            AV23GXV2 = 1;
            while ( AV23GXV2 <= AV21WWP_SearchResultsItem.gxTpr_Result.Count )
            {
               AV19WWP_SearchResultItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)AV21WWP_SearchResultsItem.gxTpr_Result.Item(AV23GXV2));
               if ( StringUtil.StrCmp(AV19WWP_SearchResultItem.gxTpr_Url, AV18Url) == 0 )
               {
                  AV8CategoryFound = false;
                  AV24GXV3 = 1;
                  while ( AV24GXV3 <= AV14RecentSearches.Count )
                  {
                     AV16RecentSearchResults = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem)AV14RecentSearches.Item(AV24GXV3));
                     if ( StringUtil.StrCmp(AV16RecentSearchResults.gxTpr_Categoryname, AV21WWP_SearchResultsItem.gxTpr_Categoryname) == 0 )
                     {
                        AV8CategoryFound = true;
                        AV9CurrentRecentSearchResults = AV16RecentSearchResults;
                        if (true) break;
                     }
                     AV24GXV3 = (int)(AV24GXV3+1);
                  }
                  AV13RecentAlreadyAdded = false;
                  if ( ! AV8CategoryFound )
                  {
                     AV9CurrentRecentSearchResults = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem(context);
                     AV9CurrentRecentSearchResults.gxTpr_Categoryname = AV21WWP_SearchResultsItem.gxTpr_Categoryname;
                     AV9CurrentRecentSearchResults.gxTpr_Categoryicon = AV21WWP_SearchResultsItem.gxTpr_Categoryicon;
                     AV9CurrentRecentSearchResults.gxTpr_Orderindex = AV21WWP_SearchResultsItem.gxTpr_Orderindex;
                     AV9CurrentRecentSearchResults.gxTpr_Showingallresults = true;
                     AV14RecentSearches.Add(AV9CurrentRecentSearchResults, 0);
                     if ( AV14RecentSearches.Count > AV11MaxCategories )
                     {
                        AV14RecentSearches.RemoveItem(1);
                     }
                  }
                  else
                  {
                     AV25GXV4 = 1;
                     while ( AV25GXV4 <= AV9CurrentRecentSearchResults.gxTpr_Result.Count )
                     {
                        AV15RecentSearchResultItem = ((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)AV9CurrentRecentSearchResults.gxTpr_Result.Item(AV25GXV4));
                        if ( StringUtil.StrCmp(AV15RecentSearchResultItem.gxTpr_Url, AV18Url) == 0 )
                        {
                           AV13RecentAlreadyAdded = true;
                           if (true) break;
                        }
                        AV25GXV4 = (int)(AV25GXV4+1);
                     }
                  }
                  if ( ! AV13RecentAlreadyAdded )
                  {
                     AV9CurrentRecentSearchResults.gxTpr_Result.Add((DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem)(AV19WWP_SearchResultItem.Clone()), 1);
                     if ( AV9CurrentRecentSearchResults.gxTpr_Result.Count > AV12MaxItemsPerCategory )
                     {
                        AV9CurrentRecentSearchResults.gxTpr_Result.RemoveItem(AV9CurrentRecentSearchResults.gxTpr_Result.Count);
                     }
                     new DesignSystem.Programs.wwpbaseobjects.saveuserkeyvalue(context ).execute(  "WWPRecentSearch",  AV14RecentSearches.ToJSonString(false)) ;
                  }
                  AV10Found = true;
                  if (true) break;
               }
               AV23GXV2 = (int)(AV23GXV2+1);
            }
            if ( AV10Found )
            {
               if (true) break;
            }
            AV22GXV1 = (int)(AV22GXV1+1);
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
         AV17RecentSearchResultsJson = "";
         GXt_char1 = "";
         AV14RecentSearches = new GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem>( context, "WWP_SearchResultsItem", "DesignSystem");
         AV21WWP_SearchResultsItem = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem(context);
         AV19WWP_SearchResultItem = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem(context);
         AV16RecentSearchResults = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem(context);
         AV9CurrentRecentSearchResults = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem(context);
         AV15RecentSearchResultItem = new DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem(context);
         /* GeneXus formulas. */
      }

      private short AV11MaxCategories ;
      private short AV12MaxItemsPerCategory ;
      private int AV22GXV1 ;
      private int AV23GXV2 ;
      private int AV24GXV3 ;
      private int AV25GXV4 ;
      private string GXt_char1 ;
      private bool AV10Found ;
      private bool AV8CategoryFound ;
      private bool AV13RecentAlreadyAdded ;
      private string AV18Url ;
      private string AV17RecentSearchResultsJson ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV20WWP_SearchResults ;
      private GXBaseCollection<DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem> AV14RecentSearches ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem AV21WWP_SearchResultsItem ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem AV19WWP_SearchResultItem ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem AV16RecentSearchResults ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem AV9CurrentRecentSearchResults ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWP_SearchResults_WWP_SearchResultsItem_ResultItem AV15RecentSearchResultItem ;
   }

}
