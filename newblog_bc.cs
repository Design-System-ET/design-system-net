using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace DesignSystem.Programs {
   public class newblog_bc : GxSilentTrn, IGxSilentTrn
   {
      public newblog_bc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newblog_bc( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      protected void INITTRN( )
      {
      }

      public void GetInsDefault( )
      {
         ReadRow033( ) ;
         standaloneNotModal( ) ;
         InitializeNonKey033( ) ;
         standaloneModal( ) ;
         AddRow033( ) ;
         Gx_mode = "INS";
         return  ;
      }

      protected void AfterTrn( )
      {
         if ( trnEnded == 1 )
         {
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( endTrnMsgTxt)) )
            {
               GX_msglist.addItem(endTrnMsgTxt, endTrnMsgCod, 0, "", true);
            }
            /* Execute user event: After Trn */
            E11032 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               Z12NewBlogId = A12NewBlogId;
               SetMode( "UPD") ;
            }
         }
         endTrnMsgTxt = "";
      }

      public override string ToString( )
      {
         return "" ;
      }

      public GxContentInfo GetContentInfo( )
      {
         return (GxContentInfo)(null) ;
      }

      public bool Reindex( )
      {
         return true ;
      }

      protected void CONFIRM_030( )
      {
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls033( ) ;
            }
            else
            {
               CheckExtendedTable033( ) ;
               if ( AnyError == 0 )
               {
                  ZM033( 4) ;
               }
               CloseExtendedTableCursors033( ) ;
            }
         }
         if ( AnyError == 0 )
         {
         }
      }

      protected void E12032( )
      {
         /* Start Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
         if ( ( StringUtil.StrCmp(AV11TrnContext.gxTpr_Transactionname, AV27Pgmname) == 0 ) && ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) )
         {
            AV28GXV1 = 1;
            while ( AV28GXV1 <= AV11TrnContext.gxTpr_Attributes.Count )
            {
               AV14TrnContextAtt = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute)AV11TrnContext.gxTpr_Attributes.Item(AV28GXV1));
               if ( StringUtil.StrCmp(AV14TrnContextAtt.gxTpr_Attributename, "CategoriasId") == 0 )
               {
                  AV13Insert_CategoriasId = (short)(Math.Round(NumberUtil.Val( AV14TrnContextAtt.gxTpr_Attributevalue, "."), 18, MidpointRounding.ToEven));
               }
               AV28GXV1 = (int)(AV28GXV1+1);
            }
         }
      }

      protected void E11032( )
      {
         /* After Trn Routine */
         returnInSub = false;
      }

      protected void E13032( )
      {
         /* 'DoUserAction1' Routine */
         returnInSub = false;
         if ( AV18IsAuthorized_UserAction1 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            GXEncryptionTmp = "categoriasww.aspx"+UrlEncode(StringUtil.RTrim("INS"));
            context.PopUp(formatLink("categoriasww.aspx") + "?" + UriEncrypt64( GXEncryptionTmp+Crypto.CheckSum( GXEncryptionTmp, 6), GXKey), new Object[] {});
         }
         else
         {
            GX_msglist.addItem(context.GetMessage( "WWP_ActionNoLongerAvailable", ""));
         }
      }

      protected void ZM033( short GX_JID )
      {
         if ( ( GX_JID == 3 ) || ( GX_JID == 0 ) )
         {
            Z14NewBlogTitulo = A14NewBlogTitulo;
            Z15NewBlogSubTitulo = A15NewBlogSubTitulo;
            Z17NewBlogDescripcionCorta = A17NewBlogDescripcionCorta;
            Z18NewBlogVisitas = A18NewBlogVisitas;
            Z19NewBlogDestacado = A19NewBlogDestacado;
            Z25NewBlogBorrador = A25NewBlogBorrador;
            Z20CategoriasId = A20CategoriasId;
         }
         if ( ( GX_JID == 4 ) || ( GX_JID == 0 ) )
         {
         }
         if ( GX_JID == -3 )
         {
            Z12NewBlogId = A12NewBlogId;
            Z13NewBlogImagen = A13NewBlogImagen;
            Z40000NewBlogImagen_GXI = A40000NewBlogImagen_GXI;
            Z14NewBlogTitulo = A14NewBlogTitulo;
            Z15NewBlogSubTitulo = A15NewBlogSubTitulo;
            Z16NewBlogDescripcion = A16NewBlogDescripcion;
            Z17NewBlogDescripcionCorta = A17NewBlogDescripcionCorta;
            Z18NewBlogVisitas = A18NewBlogVisitas;
            Z19NewBlogDestacado = A19NewBlogDestacado;
            Z25NewBlogBorrador = A25NewBlogBorrador;
            Z20CategoriasId = A20CategoriasId;
         }
      }

      protected void standaloneNotModal( )
      {
         AV27Pgmname = "NewBlog_BC";
      }

      protected void standaloneModal( )
      {
      }

      protected void Load033( )
      {
         /* Using cursor BC00035 */
         pr_default.execute(3, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound3 = 1;
            A40000NewBlogImagen_GXI = BC00035_A40000NewBlogImagen_GXI[0];
            A14NewBlogTitulo = BC00035_A14NewBlogTitulo[0];
            A15NewBlogSubTitulo = BC00035_A15NewBlogSubTitulo[0];
            A16NewBlogDescripcion = BC00035_A16NewBlogDescripcion[0];
            A17NewBlogDescripcionCorta = BC00035_A17NewBlogDescripcionCorta[0];
            A18NewBlogVisitas = BC00035_A18NewBlogVisitas[0];
            A19NewBlogDestacado = BC00035_A19NewBlogDestacado[0];
            A25NewBlogBorrador = BC00035_A25NewBlogBorrador[0];
            A20CategoriasId = BC00035_A20CategoriasId[0];
            A13NewBlogImagen = BC00035_A13NewBlogImagen[0];
            ZM033( -3) ;
         }
         pr_default.close(3);
         OnLoadActions033( ) ;
      }

      protected void OnLoadActions033( )
      {
      }

      protected void CheckExtendedTable033( )
      {
         standaloneModal( ) ;
         /* Using cursor BC00034 */
         pr_default.execute(2, new Object[] {A20CategoriasId});
         if ( (pr_default.getStatus(2) == 101) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Categorias", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "CATEGORIASID");
            AnyError = 1;
         }
         pr_default.close(2);
      }

      protected void CloseExtendedTableCursors033( )
      {
         pr_default.close(2);
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey033( )
      {
         /* Using cursor BC00036 */
         pr_default.execute(4, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(4) != 101) )
         {
            RcdFound3 = 1;
         }
         else
         {
            RcdFound3 = 0;
         }
         pr_default.close(4);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor BC00033 */
         pr_default.execute(1, new Object[] {A12NewBlogId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM033( 3) ;
            RcdFound3 = 1;
            A12NewBlogId = BC00033_A12NewBlogId[0];
            A40000NewBlogImagen_GXI = BC00033_A40000NewBlogImagen_GXI[0];
            A14NewBlogTitulo = BC00033_A14NewBlogTitulo[0];
            A15NewBlogSubTitulo = BC00033_A15NewBlogSubTitulo[0];
            A16NewBlogDescripcion = BC00033_A16NewBlogDescripcion[0];
            A17NewBlogDescripcionCorta = BC00033_A17NewBlogDescripcionCorta[0];
            A18NewBlogVisitas = BC00033_A18NewBlogVisitas[0];
            A19NewBlogDestacado = BC00033_A19NewBlogDestacado[0];
            A25NewBlogBorrador = BC00033_A25NewBlogBorrador[0];
            A20CategoriasId = BC00033_A20CategoriasId[0];
            A13NewBlogImagen = BC00033_A13NewBlogImagen[0];
            Z12NewBlogId = A12NewBlogId;
            sMode3 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Load033( ) ;
            if ( AnyError == 1 )
            {
               RcdFound3 = 0;
               InitializeNonKey033( ) ;
            }
            Gx_mode = sMode3;
         }
         else
         {
            RcdFound3 = 0;
            InitializeNonKey033( ) ;
            sMode3 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Gx_mode = sMode3;
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey033( ) ;
         if ( RcdFound3 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
         }
         getByPrimaryKey( ) ;
      }

      protected void insert_Check( )
      {
         CONFIRM_030( ) ;
      }

      protected void update_Check( )
      {
         insert_Check( ) ;
      }

      protected void delete_Check( )
      {
         insert_Check( ) ;
      }

      protected void CheckOptimisticConcurrency033( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor BC00032 */
            pr_default.execute(0, new Object[] {A12NewBlogId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewBlog"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z14NewBlogTitulo, BC00032_A14NewBlogTitulo[0]) != 0 ) || ( StringUtil.StrCmp(Z15NewBlogSubTitulo, BC00032_A15NewBlogSubTitulo[0]) != 0 ) || ( StringUtil.StrCmp(Z17NewBlogDescripcionCorta, BC00032_A17NewBlogDescripcionCorta[0]) != 0 ) || ( Z18NewBlogVisitas != BC00032_A18NewBlogVisitas[0] ) || ( Z19NewBlogDestacado != BC00032_A19NewBlogDestacado[0] ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z25NewBlogBorrador != BC00032_A25NewBlogBorrador[0] ) || ( Z20CategoriasId != BC00032_A20CategoriasId[0] ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"NewBlog"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert033( )
      {
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable033( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM033( 0) ;
            CheckOptimisticConcurrency033( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm033( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert033( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00037 */
                     pr_default.execute(5, new Object[] {A13NewBlogImagen, A40000NewBlogImagen_GXI, A14NewBlogTitulo, A15NewBlogSubTitulo, A16NewBlogDescripcion, A17NewBlogDescripcionCorta, A18NewBlogVisitas, A19NewBlogDestacado, A25NewBlogBorrador, A20CategoriasId});
                     pr_default.close(5);
                     /* Retrieving last key number assigned */
                     /* Using cursor BC00038 */
                     pr_default.execute(6);
                     A12NewBlogId = BC00038_A12NewBlogId[0];
                     pr_default.close(6);
                     pr_default.SmartCacheProvider.SetUpdated("NewBlog");
                     if ( AnyError == 0 )
                     {
                        /* Start of After( Insert) rules */
                        /* End of After( Insert) rules */
                        if ( AnyError == 0 )
                        {
                           /* Save values for previous() function. */
                           endTrnMsgTxt = context.GetMessage( "GXM_sucadded", "");
                           endTrnMsgCod = "SuccessfullyAdded";
                        }
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                     AnyError = 1;
                  }
               }
            }
            else
            {
               Load033( ) ;
            }
            EndLevel033( ) ;
         }
         CloseExtendedTableCursors033( ) ;
      }

      protected void Update033( )
      {
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable033( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency033( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm033( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate033( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00039 */
                     pr_default.execute(7, new Object[] {A14NewBlogTitulo, A15NewBlogSubTitulo, A16NewBlogDescripcion, A17NewBlogDescripcionCorta, A18NewBlogVisitas, A19NewBlogDestacado, A25NewBlogBorrador, A20CategoriasId, A12NewBlogId});
                     pr_default.close(7);
                     pr_default.SmartCacheProvider.SetUpdated("NewBlog");
                     if ( (pr_default.getStatus(7) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewBlog"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate033( ) ;
                     if ( AnyError == 0 )
                     {
                        /* Start of After( update) rules */
                        /* End of After( update) rules */
                        if ( AnyError == 0 )
                        {
                           getByPrimaryKey( ) ;
                           endTrnMsgTxt = context.GetMessage( "GXM_sucupdated", "");
                           endTrnMsgCod = "SuccessfullyUpdated";
                        }
                     }
                     else
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                        AnyError = 1;
                     }
                  }
               }
            }
            EndLevel033( ) ;
         }
         CloseExtendedTableCursors033( ) ;
      }

      protected void DeferredUpdate033( )
      {
         if ( AnyError == 0 )
         {
            /* Using cursor BC000310 */
            pr_default.execute(8, new Object[] {A13NewBlogImagen, A40000NewBlogImagen_GXI, A12NewBlogId});
            pr_default.close(8);
            pr_default.SmartCacheProvider.SetUpdated("NewBlog");
         }
      }

      protected void delete( )
      {
         Gx_mode = "DLT";
         BeforeValidate033( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency033( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls033( ) ;
            AfterConfirm033( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete033( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor BC000311 */
                  pr_default.execute(9, new Object[] {A12NewBlogId});
                  pr_default.close(9);
                  pr_default.SmartCacheProvider.SetUpdated("NewBlog");
                  if ( AnyError == 0 )
                  {
                     /* Start of After( delete) rules */
                     /* End of After( delete) rules */
                     if ( AnyError == 0 )
                     {
                        endTrnMsgTxt = context.GetMessage( "GXM_sucdeleted", "");
                        endTrnMsgCod = "SuccessfullyDeleted";
                     }
                  }
                  else
                  {
                     GX_msglist.addItem(context.GetMessage( "GXM_unexp", ""), 1, "");
                     AnyError = 1;
                  }
               }
            }
         }
         sMode3 = Gx_mode;
         Gx_mode = "DLT";
         EndLevel033( ) ;
         Gx_mode = sMode3;
      }

      protected void OnDeleteControls033( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel033( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete033( ) ;
         }
         if ( AnyError == 0 )
         {
            /* After transaction rules */
            /* Execute 'After Trn' event if defined. */
            trnEnded = 1;
         }
         else
         {
         }
         if ( AnyError != 0 )
         {
            context.wjLoc = "";
            context.nUserReturn = 0;
         }
      }

      public void ScanKeyStart033( )
      {
         /* Scan By routine */
         /* Using cursor BC000312 */
         pr_default.execute(10, new Object[] {A12NewBlogId});
         RcdFound3 = 0;
         if ( (pr_default.getStatus(10) != 101) )
         {
            RcdFound3 = 1;
            A12NewBlogId = BC000312_A12NewBlogId[0];
            A40000NewBlogImagen_GXI = BC000312_A40000NewBlogImagen_GXI[0];
            A14NewBlogTitulo = BC000312_A14NewBlogTitulo[0];
            A15NewBlogSubTitulo = BC000312_A15NewBlogSubTitulo[0];
            A16NewBlogDescripcion = BC000312_A16NewBlogDescripcion[0];
            A17NewBlogDescripcionCorta = BC000312_A17NewBlogDescripcionCorta[0];
            A18NewBlogVisitas = BC000312_A18NewBlogVisitas[0];
            A19NewBlogDestacado = BC000312_A19NewBlogDestacado[0];
            A25NewBlogBorrador = BC000312_A25NewBlogBorrador[0];
            A20CategoriasId = BC000312_A20CategoriasId[0];
            A13NewBlogImagen = BC000312_A13NewBlogImagen[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanKeyNext033( )
      {
         /* Scan next routine */
         pr_default.readNext(10);
         RcdFound3 = 0;
         ScanKeyLoad033( ) ;
      }

      protected void ScanKeyLoad033( )
      {
         sMode3 = Gx_mode;
         Gx_mode = "DSP";
         if ( (pr_default.getStatus(10) != 101) )
         {
            RcdFound3 = 1;
            A12NewBlogId = BC000312_A12NewBlogId[0];
            A40000NewBlogImagen_GXI = BC000312_A40000NewBlogImagen_GXI[0];
            A14NewBlogTitulo = BC000312_A14NewBlogTitulo[0];
            A15NewBlogSubTitulo = BC000312_A15NewBlogSubTitulo[0];
            A16NewBlogDescripcion = BC000312_A16NewBlogDescripcion[0];
            A17NewBlogDescripcionCorta = BC000312_A17NewBlogDescripcionCorta[0];
            A18NewBlogVisitas = BC000312_A18NewBlogVisitas[0];
            A19NewBlogDestacado = BC000312_A19NewBlogDestacado[0];
            A25NewBlogBorrador = BC000312_A25NewBlogBorrador[0];
            A20CategoriasId = BC000312_A20CategoriasId[0];
            A13NewBlogImagen = BC000312_A13NewBlogImagen[0];
         }
         Gx_mode = sMode3;
      }

      protected void ScanKeyEnd033( )
      {
         pr_default.close(10);
      }

      protected void AfterConfirm033( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert033( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate033( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete033( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete033( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate033( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes033( )
      {
      }

      protected void send_integrity_lvl_hashes033( )
      {
      }

      protected void AddRow033( )
      {
         VarsToRow3( bcNewBlog) ;
      }

      protected void ReadRow033( )
      {
         RowToVars3( bcNewBlog, 1) ;
      }

      protected void InitializeNonKey033( )
      {
         A13NewBlogImagen = "";
         A40000NewBlogImagen_GXI = "";
         A14NewBlogTitulo = "";
         A15NewBlogSubTitulo = "";
         A16NewBlogDescripcion = "";
         A17NewBlogDescripcionCorta = "";
         A18NewBlogVisitas = 0;
         A19NewBlogDestacado = false;
         A25NewBlogBorrador = false;
         A20CategoriasId = 0;
         Z14NewBlogTitulo = "";
         Z15NewBlogSubTitulo = "";
         Z17NewBlogDescripcionCorta = "";
         Z18NewBlogVisitas = 0;
         Z19NewBlogDestacado = false;
         Z25NewBlogBorrador = false;
         Z20CategoriasId = 0;
      }

      protected void InitAll033( )
      {
         A12NewBlogId = 0;
         InitializeNonKey033( ) ;
      }

      protected void StandaloneModalInsert( )
      {
      }

      protected bool IsIns( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "INS")==0) ? true : false) ;
      }

      protected bool IsDlt( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "DLT")==0) ? true : false) ;
      }

      protected bool IsUpd( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "UPD")==0) ? true : false) ;
      }

      protected bool IsDsp( )
      {
         return ((StringUtil.StrCmp(Gx_mode, "DSP")==0) ? true : false) ;
      }

      public void VarsToRow3( SdtNewBlog obj3 )
      {
         obj3.gxTpr_Mode = Gx_mode;
         obj3.gxTpr_Newblogimagen = A13NewBlogImagen;
         obj3.gxTpr_Newblogimagen_gxi = A40000NewBlogImagen_GXI;
         obj3.gxTpr_Newblogtitulo = A14NewBlogTitulo;
         obj3.gxTpr_Newblogsubtitulo = A15NewBlogSubTitulo;
         obj3.gxTpr_Newblogdescripcion = A16NewBlogDescripcion;
         obj3.gxTpr_Newblogdescripcioncorta = A17NewBlogDescripcionCorta;
         obj3.gxTpr_Newblogvisitas = A18NewBlogVisitas;
         obj3.gxTpr_Newblogdestacado = A19NewBlogDestacado;
         obj3.gxTpr_Newblogborrador = A25NewBlogBorrador;
         obj3.gxTpr_Categoriasid = A20CategoriasId;
         obj3.gxTpr_Newblogid = A12NewBlogId;
         obj3.gxTpr_Newblogid_Z = Z12NewBlogId;
         obj3.gxTpr_Newblogtitulo_Z = Z14NewBlogTitulo;
         obj3.gxTpr_Newblogsubtitulo_Z = Z15NewBlogSubTitulo;
         obj3.gxTpr_Newblogdescripcioncorta_Z = Z17NewBlogDescripcionCorta;
         obj3.gxTpr_Newblogvisitas_Z = Z18NewBlogVisitas;
         obj3.gxTpr_Newblogdestacado_Z = Z19NewBlogDestacado;
         obj3.gxTpr_Newblogborrador_Z = Z25NewBlogBorrador;
         obj3.gxTpr_Categoriasid_Z = Z20CategoriasId;
         obj3.gxTpr_Newblogimagen_gxi_Z = Z40000NewBlogImagen_GXI;
         obj3.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void KeyVarsToRow3( SdtNewBlog obj3 )
      {
         obj3.gxTpr_Newblogid = A12NewBlogId;
         return  ;
      }

      public void RowToVars3( SdtNewBlog obj3 ,
                              int forceLoad )
      {
         Gx_mode = obj3.gxTpr_Mode;
         A13NewBlogImagen = obj3.gxTpr_Newblogimagen;
         A40000NewBlogImagen_GXI = obj3.gxTpr_Newblogimagen_gxi;
         A14NewBlogTitulo = obj3.gxTpr_Newblogtitulo;
         A15NewBlogSubTitulo = obj3.gxTpr_Newblogsubtitulo;
         A16NewBlogDescripcion = obj3.gxTpr_Newblogdescripcion;
         A17NewBlogDescripcionCorta = obj3.gxTpr_Newblogdescripcioncorta;
         A18NewBlogVisitas = obj3.gxTpr_Newblogvisitas;
         A19NewBlogDestacado = obj3.gxTpr_Newblogdestacado;
         A25NewBlogBorrador = obj3.gxTpr_Newblogborrador;
         A20CategoriasId = obj3.gxTpr_Categoriasid;
         A12NewBlogId = obj3.gxTpr_Newblogid;
         Z12NewBlogId = obj3.gxTpr_Newblogid_Z;
         Z14NewBlogTitulo = obj3.gxTpr_Newblogtitulo_Z;
         Z15NewBlogSubTitulo = obj3.gxTpr_Newblogsubtitulo_Z;
         Z17NewBlogDescripcionCorta = obj3.gxTpr_Newblogdescripcioncorta_Z;
         Z18NewBlogVisitas = obj3.gxTpr_Newblogvisitas_Z;
         Z19NewBlogDestacado = obj3.gxTpr_Newblogdestacado_Z;
         Z25NewBlogBorrador = obj3.gxTpr_Newblogborrador_Z;
         Z20CategoriasId = obj3.gxTpr_Categoriasid_Z;
         Z40000NewBlogImagen_GXI = obj3.gxTpr_Newblogimagen_gxi_Z;
         Gx_mode = obj3.gxTpr_Mode;
         return  ;
      }

      public void LoadKey( Object[] obj )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         A12NewBlogId = (short)getParm(obj,0);
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         InitializeNonKey033( ) ;
         ScanKeyStart033( ) ;
         if ( RcdFound3 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z12NewBlogId = A12NewBlogId;
         }
         ZM033( -3) ;
         OnLoadActions033( ) ;
         AddRow033( ) ;
         ScanKeyEnd033( ) ;
         if ( RcdFound3 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      public void Load( )
      {
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         RowToVars3( bcNewBlog, 0) ;
         ScanKeyStart033( ) ;
         if ( RcdFound3 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z12NewBlogId = A12NewBlogId;
         }
         ZM033( -3) ;
         OnLoadActions033( ) ;
         AddRow033( ) ;
         ScanKeyEnd033( ) ;
         if ( RcdFound3 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      protected void SaveImpl( )
      {
         GetKey033( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            Insert033( ) ;
         }
         else
         {
            if ( RcdFound3 == 1 )
            {
               if ( A12NewBlogId != Z12NewBlogId )
               {
                  A12NewBlogId = Z12NewBlogId;
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "");
                  AnyError = 1;
               }
               else if ( IsDlt( ) )
               {
                  delete( ) ;
                  AfterTrn( ) ;
               }
               else
               {
                  Gx_mode = "UPD";
                  /* Update record */
                  Update033( ) ;
               }
            }
            else
            {
               if ( IsDlt( ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "CandidateKeyNotFound", 1, "");
                  AnyError = 1;
               }
               else
               {
                  if ( A12NewBlogId != Z12NewBlogId )
                  {
                     if ( IsUpd( ) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "DuplicatePrimaryKey", 1, "");
                        AnyError = 1;
                     }
                     else
                     {
                        Gx_mode = "INS";
                        /* Insert record */
                        Insert033( ) ;
                     }
                  }
                  else
                  {
                     if ( StringUtil.StrCmp(Gx_mode, "UPD") == 0 )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "");
                        AnyError = 1;
                     }
                     else
                     {
                        Gx_mode = "INS";
                        /* Insert record */
                        Insert033( ) ;
                     }
                  }
               }
            }
         }
         AfterTrn( ) ;
      }

      public void Save( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars3( bcNewBlog, 1) ;
         SaveImpl( ) ;
         VarsToRow3( bcNewBlog) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public bool Insert( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars3( bcNewBlog, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert033( ) ;
         AfterTrn( ) ;
         VarsToRow3( bcNewBlog) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      protected void UpdateImpl( )
      {
         if ( IsUpd( ) )
         {
            SaveImpl( ) ;
            VarsToRow3( bcNewBlog) ;
         }
         else
         {
            SdtNewBlog auxBC = new SdtNewBlog(context);
            IGxSilentTrn auxTrn = auxBC.getTransaction();
            auxBC.Load(A12NewBlogId);
            if ( auxTrn.Errors() == 0 )
            {
               auxBC.UpdateDirties(bcNewBlog);
               auxBC.Save();
               bcNewBlog.Copy((GxSilentTrnSdt)(auxBC));
            }
            LclMsgLst = (msglist)(auxTrn.GetMessages());
            AnyError = (short)(auxTrn.Errors());
            context.GX_msglist = LclMsgLst;
            if ( auxTrn.Errors() == 0 )
            {
               Gx_mode = auxTrn.GetMode();
               AfterTrn( ) ;
            }
         }
      }

      public bool Update( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars3( bcNewBlog, 1) ;
         UpdateImpl( ) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      public bool InsertOrUpdate( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars3( bcNewBlog, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert033( ) ;
         if ( AnyError == 1 )
         {
            if ( StringUtil.StrCmp(context.GX_msglist.getItemValue(1), "DuplicatePrimaryKey") == 0 )
            {
               AnyError = 0;
               context.GX_msglist.removeAllItems();
               UpdateImpl( ) ;
            }
            else
            {
               VarsToRow3( bcNewBlog) ;
            }
         }
         else
         {
            AfterTrn( ) ;
            VarsToRow3( bcNewBlog) ;
         }
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      public void Check( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars3( bcNewBlog, 0) ;
         GetKey033( ) ;
         if ( RcdFound3 == 1 )
         {
            if ( IsIns( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( A12NewBlogId != Z12NewBlogId )
            {
               A12NewBlogId = Z12NewBlogId;
               GX_msglist.addItem(context.GetMessage( "GXM_getbeforeupd", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( IsDlt( ) )
            {
               delete_Check( ) ;
            }
            else
            {
               Gx_mode = "UPD";
               update_Check( ) ;
            }
         }
         else
         {
            if ( A12NewBlogId != Z12NewBlogId )
            {
               Gx_mode = "INS";
               insert_Check( ) ;
            }
            else
            {
               if ( IsUpd( ) )
               {
                  GX_msglist.addItem(context.GetMessage( "GXM_recdeleted", ""), 1, "");
                  AnyError = 1;
               }
               else
               {
                  Gx_mode = "INS";
                  insert_Check( ) ;
               }
            }
         }
         context.RollbackDataStores("newblog_bc",pr_default);
         VarsToRow3( bcNewBlog) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public int Errors( )
      {
         if ( AnyError == 0 )
         {
            return (int)(0) ;
         }
         return (int)(1) ;
      }

      public msglist GetMessages( )
      {
         return LclMsgLst ;
      }

      public string GetMode( )
      {
         Gx_mode = bcNewBlog.gxTpr_Mode;
         return Gx_mode ;
      }

      public void SetMode( string lMode )
      {
         Gx_mode = lMode;
         bcNewBlog.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void SetSDT( GxSilentTrnSdt sdt ,
                          short sdtToBc )
      {
         if ( sdt != bcNewBlog )
         {
            bcNewBlog = (SdtNewBlog)(sdt);
            if ( StringUtil.StrCmp(bcNewBlog.gxTpr_Mode, "") == 0 )
            {
               bcNewBlog.gxTpr_Mode = "INS";
            }
            if ( sdtToBc == 1 )
            {
               VarsToRow3( bcNewBlog) ;
            }
            else
            {
               RowToVars3( bcNewBlog, 1) ;
            }
         }
         else
         {
            if ( StringUtil.StrCmp(bcNewBlog.gxTpr_Mode, "") == 0 )
            {
               bcNewBlog.gxTpr_Mode = "INS";
            }
         }
         return  ;
      }

      public void ReloadFromSDT( )
      {
         RowToVars3( bcNewBlog, 1) ;
         return  ;
      }

      public void ForceCommitOnExit( )
      {
         return  ;
      }

      public SdtNewBlog NewBlog_BC
      {
         get {
            return bcNewBlog ;
         }

      }

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
            return "newblog_Execute" ;
         }

      }

      public void webExecute( )
      {
         createObjects();
         initialize();
      }

      public bool isMasterPage( )
      {
         return false;
      }

      protected void createObjects( )
      {
      }

      protected void Process( )
      {
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      protected override void CloseCursors( )
      {
         pr_default.close(1);
      }

      public override void initialize( )
      {
         Gx_mode = "";
         endTrnMsgTxt = "";
         endTrnMsgCod = "";
         AV8WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV11TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV12WebSession = context.GetSession();
         AV27Pgmname = "";
         AV14TrnContextAtt = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute(context);
         GXKey = "";
         GXEncryptionTmp = "";
         Z14NewBlogTitulo = "";
         A14NewBlogTitulo = "";
         Z15NewBlogSubTitulo = "";
         A15NewBlogSubTitulo = "";
         Z17NewBlogDescripcionCorta = "";
         A17NewBlogDescripcionCorta = "";
         Z13NewBlogImagen = "";
         A13NewBlogImagen = "";
         Z40000NewBlogImagen_GXI = "";
         A40000NewBlogImagen_GXI = "";
         Z16NewBlogDescripcion = "";
         A16NewBlogDescripcion = "";
         BC00035_A12NewBlogId = new short[1] ;
         BC00035_A40000NewBlogImagen_GXI = new string[] {""} ;
         BC00035_A14NewBlogTitulo = new string[] {""} ;
         BC00035_A15NewBlogSubTitulo = new string[] {""} ;
         BC00035_A16NewBlogDescripcion = new string[] {""} ;
         BC00035_A17NewBlogDescripcionCorta = new string[] {""} ;
         BC00035_A18NewBlogVisitas = new short[1] ;
         BC00035_A19NewBlogDestacado = new bool[] {false} ;
         BC00035_A25NewBlogBorrador = new bool[] {false} ;
         BC00035_A20CategoriasId = new short[1] ;
         BC00035_A13NewBlogImagen = new string[] {""} ;
         BC00034_A20CategoriasId = new short[1] ;
         BC00036_A12NewBlogId = new short[1] ;
         BC00033_A12NewBlogId = new short[1] ;
         BC00033_A40000NewBlogImagen_GXI = new string[] {""} ;
         BC00033_A14NewBlogTitulo = new string[] {""} ;
         BC00033_A15NewBlogSubTitulo = new string[] {""} ;
         BC00033_A16NewBlogDescripcion = new string[] {""} ;
         BC00033_A17NewBlogDescripcionCorta = new string[] {""} ;
         BC00033_A18NewBlogVisitas = new short[1] ;
         BC00033_A19NewBlogDestacado = new bool[] {false} ;
         BC00033_A25NewBlogBorrador = new bool[] {false} ;
         BC00033_A20CategoriasId = new short[1] ;
         BC00033_A13NewBlogImagen = new string[] {""} ;
         sMode3 = "";
         BC00032_A12NewBlogId = new short[1] ;
         BC00032_A40000NewBlogImagen_GXI = new string[] {""} ;
         BC00032_A14NewBlogTitulo = new string[] {""} ;
         BC00032_A15NewBlogSubTitulo = new string[] {""} ;
         BC00032_A16NewBlogDescripcion = new string[] {""} ;
         BC00032_A17NewBlogDescripcionCorta = new string[] {""} ;
         BC00032_A18NewBlogVisitas = new short[1] ;
         BC00032_A19NewBlogDestacado = new bool[] {false} ;
         BC00032_A25NewBlogBorrador = new bool[] {false} ;
         BC00032_A20CategoriasId = new short[1] ;
         BC00032_A13NewBlogImagen = new string[] {""} ;
         BC00038_A12NewBlogId = new short[1] ;
         BC000312_A12NewBlogId = new short[1] ;
         BC000312_A40000NewBlogImagen_GXI = new string[] {""} ;
         BC000312_A14NewBlogTitulo = new string[] {""} ;
         BC000312_A15NewBlogSubTitulo = new string[] {""} ;
         BC000312_A16NewBlogDescripcion = new string[] {""} ;
         BC000312_A17NewBlogDescripcionCorta = new string[] {""} ;
         BC000312_A18NewBlogVisitas = new short[1] ;
         BC000312_A19NewBlogDestacado = new bool[] {false} ;
         BC000312_A25NewBlogBorrador = new bool[] {false} ;
         BC000312_A20CategoriasId = new short[1] ;
         BC000312_A13NewBlogImagen = new string[] {""} ;
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.newblog_bc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newblog_bc__default(),
            new Object[][] {
                new Object[] {
               BC00032_A12NewBlogId, BC00032_A40000NewBlogImagen_GXI, BC00032_A14NewBlogTitulo, BC00032_A15NewBlogSubTitulo, BC00032_A16NewBlogDescripcion, BC00032_A17NewBlogDescripcionCorta, BC00032_A18NewBlogVisitas, BC00032_A19NewBlogDestacado, BC00032_A25NewBlogBorrador, BC00032_A20CategoriasId,
               BC00032_A13NewBlogImagen
               }
               , new Object[] {
               BC00033_A12NewBlogId, BC00033_A40000NewBlogImagen_GXI, BC00033_A14NewBlogTitulo, BC00033_A15NewBlogSubTitulo, BC00033_A16NewBlogDescripcion, BC00033_A17NewBlogDescripcionCorta, BC00033_A18NewBlogVisitas, BC00033_A19NewBlogDestacado, BC00033_A25NewBlogBorrador, BC00033_A20CategoriasId,
               BC00033_A13NewBlogImagen
               }
               , new Object[] {
               BC00034_A20CategoriasId
               }
               , new Object[] {
               BC00035_A12NewBlogId, BC00035_A40000NewBlogImagen_GXI, BC00035_A14NewBlogTitulo, BC00035_A15NewBlogSubTitulo, BC00035_A16NewBlogDescripcion, BC00035_A17NewBlogDescripcionCorta, BC00035_A18NewBlogVisitas, BC00035_A19NewBlogDestacado, BC00035_A25NewBlogBorrador, BC00035_A20CategoriasId,
               BC00035_A13NewBlogImagen
               }
               , new Object[] {
               BC00036_A12NewBlogId
               }
               , new Object[] {
               }
               , new Object[] {
               BC00038_A12NewBlogId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               BC000312_A12NewBlogId, BC000312_A40000NewBlogImagen_GXI, BC000312_A14NewBlogTitulo, BC000312_A15NewBlogSubTitulo, BC000312_A16NewBlogDescripcion, BC000312_A17NewBlogDescripcionCorta, BC000312_A18NewBlogVisitas, BC000312_A19NewBlogDestacado, BC000312_A25NewBlogBorrador, BC000312_A20CategoriasId,
               BC000312_A13NewBlogImagen
               }
            }
         );
         AV27Pgmname = "NewBlog_BC";
         INITTRN();
         /* Execute Start event if defined. */
         /* Execute user event: Start */
         E12032 ();
         standaloneNotModal( ) ;
      }

      private short AnyError ;
      private short Z12NewBlogId ;
      private short A12NewBlogId ;
      private short AV13Insert_CategoriasId ;
      private short gxcookieaux ;
      private short Z18NewBlogVisitas ;
      private short A18NewBlogVisitas ;
      private short Z20CategoriasId ;
      private short A20CategoriasId ;
      private short RcdFound3 ;
      private int trnEnded ;
      private int AV28GXV1 ;
      private string Gx_mode ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string AV27Pgmname ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string sMode3 ;
      private bool returnInSub ;
      private bool AV18IsAuthorized_UserAction1 ;
      private bool Z19NewBlogDestacado ;
      private bool A19NewBlogDestacado ;
      private bool Z25NewBlogBorrador ;
      private bool A25NewBlogBorrador ;
      private bool Gx_longc ;
      private string Z16NewBlogDescripcion ;
      private string A16NewBlogDescripcion ;
      private string Z14NewBlogTitulo ;
      private string A14NewBlogTitulo ;
      private string Z15NewBlogSubTitulo ;
      private string A15NewBlogSubTitulo ;
      private string Z17NewBlogDescripcionCorta ;
      private string A17NewBlogDescripcionCorta ;
      private string Z40000NewBlogImagen_GXI ;
      private string A40000NewBlogImagen_GXI ;
      private string Z13NewBlogImagen ;
      private string A13NewBlogImagen ;
      private IGxSession AV12WebSession ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV11TrnContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute AV14TrnContextAtt ;
      private IDataStoreProvider pr_default ;
      private short[] BC00035_A12NewBlogId ;
      private string[] BC00035_A40000NewBlogImagen_GXI ;
      private string[] BC00035_A14NewBlogTitulo ;
      private string[] BC00035_A15NewBlogSubTitulo ;
      private string[] BC00035_A16NewBlogDescripcion ;
      private string[] BC00035_A17NewBlogDescripcionCorta ;
      private short[] BC00035_A18NewBlogVisitas ;
      private bool[] BC00035_A19NewBlogDestacado ;
      private bool[] BC00035_A25NewBlogBorrador ;
      private short[] BC00035_A20CategoriasId ;
      private string[] BC00035_A13NewBlogImagen ;
      private short[] BC00034_A20CategoriasId ;
      private short[] BC00036_A12NewBlogId ;
      private short[] BC00033_A12NewBlogId ;
      private string[] BC00033_A40000NewBlogImagen_GXI ;
      private string[] BC00033_A14NewBlogTitulo ;
      private string[] BC00033_A15NewBlogSubTitulo ;
      private string[] BC00033_A16NewBlogDescripcion ;
      private string[] BC00033_A17NewBlogDescripcionCorta ;
      private short[] BC00033_A18NewBlogVisitas ;
      private bool[] BC00033_A19NewBlogDestacado ;
      private bool[] BC00033_A25NewBlogBorrador ;
      private short[] BC00033_A20CategoriasId ;
      private string[] BC00033_A13NewBlogImagen ;
      private short[] BC00032_A12NewBlogId ;
      private string[] BC00032_A40000NewBlogImagen_GXI ;
      private string[] BC00032_A14NewBlogTitulo ;
      private string[] BC00032_A15NewBlogSubTitulo ;
      private string[] BC00032_A16NewBlogDescripcion ;
      private string[] BC00032_A17NewBlogDescripcionCorta ;
      private short[] BC00032_A18NewBlogVisitas ;
      private bool[] BC00032_A19NewBlogDestacado ;
      private bool[] BC00032_A25NewBlogBorrador ;
      private short[] BC00032_A20CategoriasId ;
      private string[] BC00032_A13NewBlogImagen ;
      private short[] BC00038_A12NewBlogId ;
      private short[] BC000312_A12NewBlogId ;
      private string[] BC000312_A40000NewBlogImagen_GXI ;
      private string[] BC000312_A14NewBlogTitulo ;
      private string[] BC000312_A15NewBlogSubTitulo ;
      private string[] BC000312_A16NewBlogDescripcion ;
      private string[] BC000312_A17NewBlogDescripcionCorta ;
      private short[] BC000312_A18NewBlogVisitas ;
      private bool[] BC000312_A19NewBlogDestacado ;
      private bool[] BC000312_A25NewBlogBorrador ;
      private short[] BC000312_A20CategoriasId ;
      private string[] BC000312_A13NewBlogImagen ;
      private SdtNewBlog bcNewBlog ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class newblog_bc__gam : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          def= new CursorDef[] {
          };
       }
    }

    public void getResults( int cursor ,
                            IFieldGetter rslt ,
                            Object[] buf )
    {
    }

    public override string getDataStoreName( )
    {
       return "GAM";
    }

 }

 public class newblog_bc__default : DataStoreHelperBase, IDataStoreHelper
 {
    public ICursor[] getCursors( )
    {
       cursorDefinitions();
       return new Cursor[] {
        new ForEachCursor(def[0])
       ,new ForEachCursor(def[1])
       ,new ForEachCursor(def[2])
       ,new ForEachCursor(def[3])
       ,new ForEachCursor(def[4])
       ,new UpdateCursor(def[5])
       ,new ForEachCursor(def[6])
       ,new UpdateCursor(def[7])
       ,new UpdateCursor(def[8])
       ,new UpdateCursor(def[9])
       ,new ForEachCursor(def[10])
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        Object[] prmBC00032;
        prmBC00032 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmBC00033;
        prmBC00033 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmBC00034;
        prmBC00034 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmBC00035;
        prmBC00035 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmBC00036;
        prmBC00036 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmBC00037;
        prmBC00037 = new Object[] {
        new ParDef("@NewBlogImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewBlogImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewBlog", Fld="NewBlogImagen"} ,
        new ParDef("@NewBlogTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogSubTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogDescripcion",GXType.Char,102400,0) ,
        new ParDef("@NewBlogDescripcionCorta",GXType.Char,500,0) ,
        new ParDef("@NewBlogVisitas",GXType.Int16,4,0) ,
        new ParDef("@NewBlogDestacado",GXType.Byte,4,0) ,
        new ParDef("@NewBlogBorrador",GXType.Byte,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmBC00038;
        prmBC00038 = new Object[] {
        };
        Object[] prmBC00039;
        prmBC00039 = new Object[] {
        new ParDef("@NewBlogTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogSubTitulo",GXType.Char,200,0) ,
        new ParDef("@NewBlogDescripcion",GXType.Char,102400,0) ,
        new ParDef("@NewBlogDescripcionCorta",GXType.Char,500,0) ,
        new ParDef("@NewBlogVisitas",GXType.Int16,4,0) ,
        new ParDef("@NewBlogDestacado",GXType.Byte,4,0) ,
        new ParDef("@NewBlogBorrador",GXType.Byte,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0) ,
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmBC000310;
        prmBC000310 = new Object[] {
        new ParDef("@NewBlogImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewBlogImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewBlog", Fld="NewBlogImagen"} ,
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmBC000311;
        prmBC000311 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        Object[] prmBC000312;
        prmBC000312 = new Object[] {
        new ParDef("@NewBlogId",GXType.Int16,4,0)
        };
        def= new CursorDef[] {
            new CursorDef("BC00032", "SELECT `NewBlogId`, `NewBlogImagen_GXI`, `NewBlogTitulo`, `NewBlogSubTitulo`, `NewBlogDescripcion`, `NewBlogDescripcionCorta`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogBorrador`, `CategoriasId`, `NewBlogImagen` FROM `NewBlog` WHERE `NewBlogId` = @NewBlogId  FOR UPDATE ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00032,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00033", "SELECT `NewBlogId`, `NewBlogImagen_GXI`, `NewBlogTitulo`, `NewBlogSubTitulo`, `NewBlogDescripcion`, `NewBlogDescripcionCorta`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogBorrador`, `CategoriasId`, `NewBlogImagen` FROM `NewBlog` WHERE `NewBlogId` = @NewBlogId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00033,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00034", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00034,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00035", "SELECT TM1.`NewBlogId`, TM1.`NewBlogImagen_GXI`, TM1.`NewBlogTitulo`, TM1.`NewBlogSubTitulo`, TM1.`NewBlogDescripcion`, TM1.`NewBlogDescripcionCorta`, TM1.`NewBlogVisitas`, TM1.`NewBlogDestacado`, TM1.`NewBlogBorrador`, TM1.`CategoriasId`, TM1.`NewBlogImagen` FROM `NewBlog` TM1 WHERE TM1.`NewBlogId` = @NewBlogId ORDER BY TM1.`NewBlogId` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00035,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00036", "SELECT `NewBlogId` FROM `NewBlog` WHERE `NewBlogId` = @NewBlogId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00036,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00037", "INSERT INTO `NewBlog`(`NewBlogImagen`, `NewBlogImagen_GXI`, `NewBlogTitulo`, `NewBlogSubTitulo`, `NewBlogDescripcion`, `NewBlogDescripcionCorta`, `NewBlogVisitas`, `NewBlogDestacado`, `NewBlogBorrador`, `CategoriasId`) VALUES(@NewBlogImagen, @NewBlogImagen_GXI, @NewBlogTitulo, @NewBlogSubTitulo, @NewBlogDescripcion, @NewBlogDescripcionCorta, @NewBlogVisitas, @NewBlogDestacado, @NewBlogBorrador, @CategoriasId)", GxErrorMask.GX_NOMASK,prmBC00037)
           ,new CursorDef("BC00038", "SELECT LAST_INSERT_ID() ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00038,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00039", "UPDATE `NewBlog` SET `NewBlogTitulo`=@NewBlogTitulo, `NewBlogSubTitulo`=@NewBlogSubTitulo, `NewBlogDescripcion`=@NewBlogDescripcion, `NewBlogDescripcionCorta`=@NewBlogDescripcionCorta, `NewBlogVisitas`=@NewBlogVisitas, `NewBlogDestacado`=@NewBlogDestacado, `NewBlogBorrador`=@NewBlogBorrador, `CategoriasId`=@CategoriasId  WHERE `NewBlogId` = @NewBlogId", GxErrorMask.GX_NOMASK,prmBC00039)
           ,new CursorDef("BC000310", "UPDATE `NewBlog` SET `NewBlogImagen`=@NewBlogImagen, `NewBlogImagen_GXI`=@NewBlogImagen_GXI  WHERE `NewBlogId` = @NewBlogId", GxErrorMask.GX_NOMASK,prmBC000310)
           ,new CursorDef("BC000311", "DELETE FROM `NewBlog`  WHERE `NewBlogId` = @NewBlogId", GxErrorMask.GX_NOMASK,prmBC000311)
           ,new CursorDef("BC000312", "SELECT TM1.`NewBlogId`, TM1.`NewBlogImagen_GXI`, TM1.`NewBlogTitulo`, TM1.`NewBlogSubTitulo`, TM1.`NewBlogDescripcion`, TM1.`NewBlogDescripcionCorta`, TM1.`NewBlogVisitas`, TM1.`NewBlogDestacado`, TM1.`NewBlogBorrador`, TM1.`CategoriasId`, TM1.`NewBlogImagen` FROM `NewBlog` TM1 WHERE TM1.`NewBlogId` = @NewBlogId ORDER BY TM1.`NewBlogId` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000312,100, GxCacheFrequency.OFF ,true,false )
        };
     }
  }

  public void getResults( int cursor ,
                          IFieldGetter rslt ,
                          Object[] buf )
  {
     switch ( cursor )
     {
           case 0 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((string[]) buf[5])[0] = rslt.getVarchar(6);
              ((short[]) buf[6])[0] = rslt.getShort(7);
              ((bool[]) buf[7])[0] = rslt.getBool(8);
              ((bool[]) buf[8])[0] = rslt.getBool(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((string[]) buf[10])[0] = rslt.getMultimediaFile(11, rslt.getVarchar(2));
              return;
           case 1 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((string[]) buf[5])[0] = rslt.getVarchar(6);
              ((short[]) buf[6])[0] = rslt.getShort(7);
              ((bool[]) buf[7])[0] = rslt.getBool(8);
              ((bool[]) buf[8])[0] = rslt.getBool(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((string[]) buf[10])[0] = rslt.getMultimediaFile(11, rslt.getVarchar(2));
              return;
           case 2 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 3 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((string[]) buf[5])[0] = rslt.getVarchar(6);
              ((short[]) buf[6])[0] = rslt.getShort(7);
              ((bool[]) buf[7])[0] = rslt.getBool(8);
              ((bool[]) buf[8])[0] = rslt.getBool(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((string[]) buf[10])[0] = rslt.getMultimediaFile(11, rslt.getVarchar(2));
              return;
           case 4 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 6 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 10 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((string[]) buf[5])[0] = rslt.getVarchar(6);
              ((short[]) buf[6])[0] = rslt.getShort(7);
              ((bool[]) buf[7])[0] = rslt.getBool(8);
              ((bool[]) buf[8])[0] = rslt.getBool(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((string[]) buf[10])[0] = rslt.getMultimediaFile(11, rslt.getVarchar(2));
              return;
     }
  }

}

}
