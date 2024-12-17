using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
namespace DesignSystem.Programs {
   public class GxFullTextSearchReindexer
   {
      public static int Reindex( IGxContext context )
      {
         GxSilentTrnSdt obj;
         IGxSilentTrn trn;
         bool result;
         obj = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
         trn = obj.getTransaction();
         result = trn.Reindex();
         obj = new SdtConfiguracionEmpresa(context);
         trn = obj.getTransaction();
         result = trn.Reindex();
         obj = new SdtNewProductos(context);
         trn = obj.getTransaction();
         result = trn.Reindex();
         obj = new SdtNewBlog(context);
         trn = obj.getTransaction();
         result = trn.Reindex();
         return 1 ;
      }

   }

}
