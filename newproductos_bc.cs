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
   public class newproductos_bc : GxSilentTrn, IGxSilentTrn
   {
      public newproductos_bc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public newproductos_bc( IGxContext context )
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
         ReadRow067( ) ;
         standaloneNotModal( ) ;
         InitializeNonKey067( ) ;
         standaloneModal( ) ;
         AddRow067( ) ;
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
            E11062 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               Z34NewProductosId = A34NewProductosId;
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

      protected void CONFIRM_060( )
      {
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls067( ) ;
            }
            else
            {
               CheckExtendedTable067( ) ;
               if ( AnyError == 0 )
               {
                  ZM067( 6) ;
               }
               CloseExtendedTableCursors067( ) ;
            }
         }
         if ( AnyError == 0 )
         {
         }
      }

      protected void E12062( )
      {
         /* Start Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
         if ( ( StringUtil.StrCmp(AV11TrnContext.gxTpr_Transactionname, AV28Pgmname) == 0 ) && ( StringUtil.StrCmp(Gx_mode, "INS") == 0 ) )
         {
            AV29GXV1 = 1;
            while ( AV29GXV1 <= AV11TrnContext.gxTpr_Attributes.Count )
            {
               AV15TrnContextAtt = ((DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute)AV11TrnContext.gxTpr_Attributes.Item(AV29GXV1));
               if ( StringUtil.StrCmp(AV15TrnContextAtt.gxTpr_Attributename, "CategoriasId") == 0 )
               {
                  AV14Insert_CategoriasId = (short)(Math.Round(NumberUtil.Val( AV15TrnContextAtt.gxTpr_Attributevalue, "."), 18, MidpointRounding.ToEven));
               }
               AV29GXV1 = (int)(AV29GXV1+1);
            }
         }
      }

      protected void E11062( )
      {
         /* After Trn Routine */
         returnInSub = false;
      }

      protected void E13062( )
      {
         /* 'DoUserAction1' Routine */
         returnInSub = false;
         if ( AV24IsAuthorized_UserAction1 )
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

      protected void ZM067( short GX_JID )
      {
         if ( ( GX_JID == 5 ) || ( GX_JID == 0 ) )
         {
            Z36NewProductosNombre = A36NewProductosNombre;
            Z37NewProductosDescripcionCorta = A37NewProductosDescripcionCorta;
            Z39NewProductosNumeroDescargas = A39NewProductosNumeroDescargas;
            Z40NewProductosLinkDescargaDemo = A40NewProductosLinkDescargaDemo;
            Z41NewProductosComprar = A41NewProductosComprar;
            Z42NewProductosNumeroVentas = A42NewProductosNumeroVentas;
            Z43NewProductosVisitas = A43NewProductosVisitas;
            Z20CategoriasId = A20CategoriasId;
         }
         if ( ( GX_JID == 6 ) || ( GX_JID == 0 ) )
         {
         }
         if ( GX_JID == -5 )
         {
            Z34NewProductosId = A34NewProductosId;
            Z35NewProductosImagen = A35NewProductosImagen;
            Z40000NewProductosImagen_GXI = A40000NewProductosImagen_GXI;
            Z36NewProductosNombre = A36NewProductosNombre;
            Z37NewProductosDescripcionCorta = A37NewProductosDescripcionCorta;
            Z38NewProductosDescripcion = A38NewProductosDescripcion;
            Z39NewProductosNumeroDescargas = A39NewProductosNumeroDescargas;
            Z40NewProductosLinkDescargaDemo = A40NewProductosLinkDescargaDemo;
            Z41NewProductosComprar = A41NewProductosComprar;
            Z42NewProductosNumeroVentas = A42NewProductosNumeroVentas;
            Z43NewProductosVisitas = A43NewProductosVisitas;
            Z20CategoriasId = A20CategoriasId;
         }
      }

      protected void standaloneNotModal( )
      {
         AV28Pgmname = "NewProductos_BC";
      }

      protected void standaloneModal( )
      {
      }

      protected void Load067( )
      {
         /* Using cursor BC00065 */
         pr_default.execute(3, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound7 = 1;
            A40000NewProductosImagen_GXI = BC00065_A40000NewProductosImagen_GXI[0];
            A36NewProductosNombre = BC00065_A36NewProductosNombre[0];
            A37NewProductosDescripcionCorta = BC00065_A37NewProductosDescripcionCorta[0];
            A38NewProductosDescripcion = BC00065_A38NewProductosDescripcion[0];
            A39NewProductosNumeroDescargas = BC00065_A39NewProductosNumeroDescargas[0];
            A40NewProductosLinkDescargaDemo = BC00065_A40NewProductosLinkDescargaDemo[0];
            A41NewProductosComprar = BC00065_A41NewProductosComprar[0];
            A42NewProductosNumeroVentas = BC00065_A42NewProductosNumeroVentas[0];
            A43NewProductosVisitas = BC00065_A43NewProductosVisitas[0];
            A20CategoriasId = BC00065_A20CategoriasId[0];
            A35NewProductosImagen = BC00065_A35NewProductosImagen[0];
            ZM067( -5) ;
         }
         pr_default.close(3);
         OnLoadActions067( ) ;
      }

      protected void OnLoadActions067( )
      {
      }

      protected void CheckExtendedTable067( )
      {
         standaloneModal( ) ;
         if ( ! ( GxRegex.IsMatch(A40NewProductosLinkDescargaDemo,"^((?:[a-zA-Z]+:(//)?)?((?:(?:[a-zA-Z]([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*)(?:\\.(?:([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*))*)|(?:(\\d{1,3}\\.){3}\\d{1,3}))(?::\\d+)?(?:/([a-zA-Z0-9$\\-_@.&+!*\"'(),=;: ]|%[0-9a-fA-F]{2})+)*/?(?:[#?](?:[a-zA-Z0-9$\\-_@.&+!*\"'(),=;: /]|%[0-9a-fA-F]{2})*)?)?\\s*$") ) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXM_DoesNotMatchRegExp", ""), context.GetMessage( "Descarga Demo", ""), "", "", "", "", "", "", "", ""), "OutOfRange", 1, "");
            AnyError = 1;
         }
         if ( ! ( GxRegex.IsMatch(A41NewProductosComprar,"^((?:[a-zA-Z]+:(//)?)?((?:(?:[a-zA-Z]([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*)(?:\\.(?:([a-zA-Z0-9$\\-_@&+!*\"'(),]|%[0-9a-fA-F]{2})*))*)|(?:(\\d{1,3}\\.){3}\\d{1,3}))(?::\\d+)?(?:/([a-zA-Z0-9$\\-_@.&+!*\"'(),=;: ]|%[0-9a-fA-F]{2})+)*/?(?:[#?](?:[a-zA-Z0-9$\\-_@.&+!*\"'(),=;: /]|%[0-9a-fA-F]{2})*)?)?\\s*$") ) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXM_DoesNotMatchRegExp", ""), context.GetMessage( "Comprar", ""), "", "", "", "", "", "", "", ""), "OutOfRange", 1, "");
            AnyError = 1;
         }
         /* Using cursor BC00064 */
         pr_default.execute(2, new Object[] {A20CategoriasId});
         if ( (pr_default.getStatus(2) == 101) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "GXSPC_ForeignKeyNotFound", ""), context.GetMessage( "Categorias", ""), "", "", "", "", "", "", "", ""), "ForeignKeyNotFound", 1, "CATEGORIASID");
            AnyError = 1;
         }
         pr_default.close(2);
      }

      protected void CloseExtendedTableCursors067( )
      {
         pr_default.close(2);
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey067( )
      {
         /* Using cursor BC00066 */
         pr_default.execute(4, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(4) != 101) )
         {
            RcdFound7 = 1;
         }
         else
         {
            RcdFound7 = 0;
         }
         pr_default.close(4);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor BC00063 */
         pr_default.execute(1, new Object[] {A34NewProductosId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM067( 5) ;
            RcdFound7 = 1;
            A34NewProductosId = BC00063_A34NewProductosId[0];
            A40000NewProductosImagen_GXI = BC00063_A40000NewProductosImagen_GXI[0];
            A36NewProductosNombre = BC00063_A36NewProductosNombre[0];
            A37NewProductosDescripcionCorta = BC00063_A37NewProductosDescripcionCorta[0];
            A38NewProductosDescripcion = BC00063_A38NewProductosDescripcion[0];
            A39NewProductosNumeroDescargas = BC00063_A39NewProductosNumeroDescargas[0];
            A40NewProductosLinkDescargaDemo = BC00063_A40NewProductosLinkDescargaDemo[0];
            A41NewProductosComprar = BC00063_A41NewProductosComprar[0];
            A42NewProductosNumeroVentas = BC00063_A42NewProductosNumeroVentas[0];
            A43NewProductosVisitas = BC00063_A43NewProductosVisitas[0];
            A20CategoriasId = BC00063_A20CategoriasId[0];
            A35NewProductosImagen = BC00063_A35NewProductosImagen[0];
            Z34NewProductosId = A34NewProductosId;
            sMode7 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Load067( ) ;
            if ( AnyError == 1 )
            {
               RcdFound7 = 0;
               InitializeNonKey067( ) ;
            }
            Gx_mode = sMode7;
         }
         else
         {
            RcdFound7 = 0;
            InitializeNonKey067( ) ;
            sMode7 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Gx_mode = sMode7;
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey067( ) ;
         if ( RcdFound7 == 0 )
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
         CONFIRM_060( ) ;
      }

      protected void update_Check( )
      {
         insert_Check( ) ;
      }

      protected void delete_Check( )
      {
         insert_Check( ) ;
      }

      protected void CheckOptimisticConcurrency067( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor BC00062 */
            pr_default.execute(0, new Object[] {A34NewProductosId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewProductos"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z36NewProductosNombre, BC00062_A36NewProductosNombre[0]) != 0 ) || ( StringUtil.StrCmp(Z37NewProductosDescripcionCorta, BC00062_A37NewProductosDescripcionCorta[0]) != 0 ) || ( Z39NewProductosNumeroDescargas != BC00062_A39NewProductosNumeroDescargas[0] ) || ( StringUtil.StrCmp(Z40NewProductosLinkDescargaDemo, BC00062_A40NewProductosLinkDescargaDemo[0]) != 0 ) || ( StringUtil.StrCmp(Z41NewProductosComprar, BC00062_A41NewProductosComprar[0]) != 0 ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z42NewProductosNumeroVentas != BC00062_A42NewProductosNumeroVentas[0] ) || ( Z43NewProductosVisitas != BC00062_A43NewProductosVisitas[0] ) || ( Z20CategoriasId != BC00062_A20CategoriasId[0] ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"NewProductos"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert067( )
      {
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable067( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM067( 0) ;
            CheckOptimisticConcurrency067( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm067( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert067( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00067 */
                     pr_default.execute(5, new Object[] {A35NewProductosImagen, A40000NewProductosImagen_GXI, A36NewProductosNombre, A37NewProductosDescripcionCorta, A38NewProductosDescripcion, A39NewProductosNumeroDescargas, A40NewProductosLinkDescargaDemo, A41NewProductosComprar, A42NewProductosNumeroVentas, A43NewProductosVisitas, A20CategoriasId});
                     pr_default.close(5);
                     /* Retrieving last key number assigned */
                     /* Using cursor BC00068 */
                     pr_default.execute(6);
                     A34NewProductosId = BC00068_A34NewProductosId[0];
                     pr_default.close(6);
                     pr_default.SmartCacheProvider.SetUpdated("NewProductos");
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
               Load067( ) ;
            }
            EndLevel067( ) ;
         }
         CloseExtendedTableCursors067( ) ;
      }

      protected void Update067( )
      {
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable067( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency067( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm067( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate067( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00069 */
                     pr_default.execute(7, new Object[] {A36NewProductosNombre, A37NewProductosDescripcionCorta, A38NewProductosDescripcion, A39NewProductosNumeroDescargas, A40NewProductosLinkDescargaDemo, A41NewProductosComprar, A42NewProductosNumeroVentas, A43NewProductosVisitas, A20CategoriasId, A34NewProductosId});
                     pr_default.close(7);
                     pr_default.SmartCacheProvider.SetUpdated("NewProductos");
                     if ( (pr_default.getStatus(7) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"NewProductos"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate067( ) ;
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
            EndLevel067( ) ;
         }
         CloseExtendedTableCursors067( ) ;
      }

      protected void DeferredUpdate067( )
      {
         if ( AnyError == 0 )
         {
            /* Using cursor BC000610 */
            pr_default.execute(8, new Object[] {A35NewProductosImagen, A40000NewProductosImagen_GXI, A34NewProductosId});
            pr_default.close(8);
            pr_default.SmartCacheProvider.SetUpdated("NewProductos");
         }
      }

      protected void delete( )
      {
         Gx_mode = "DLT";
         BeforeValidate067( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency067( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls067( ) ;
            AfterConfirm067( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete067( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor BC000611 */
                  pr_default.execute(9, new Object[] {A34NewProductosId});
                  pr_default.close(9);
                  pr_default.SmartCacheProvider.SetUpdated("NewProductos");
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
         sMode7 = Gx_mode;
         Gx_mode = "DLT";
         EndLevel067( ) ;
         Gx_mode = sMode7;
      }

      protected void OnDeleteControls067( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel067( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete067( ) ;
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

      public void ScanKeyStart067( )
      {
         /* Scan By routine */
         /* Using cursor BC000612 */
         pr_default.execute(10, new Object[] {A34NewProductosId});
         RcdFound7 = 0;
         if ( (pr_default.getStatus(10) != 101) )
         {
            RcdFound7 = 1;
            A34NewProductosId = BC000612_A34NewProductosId[0];
            A40000NewProductosImagen_GXI = BC000612_A40000NewProductosImagen_GXI[0];
            A36NewProductosNombre = BC000612_A36NewProductosNombre[0];
            A37NewProductosDescripcionCorta = BC000612_A37NewProductosDescripcionCorta[0];
            A38NewProductosDescripcion = BC000612_A38NewProductosDescripcion[0];
            A39NewProductosNumeroDescargas = BC000612_A39NewProductosNumeroDescargas[0];
            A40NewProductosLinkDescargaDemo = BC000612_A40NewProductosLinkDescargaDemo[0];
            A41NewProductosComprar = BC000612_A41NewProductosComprar[0];
            A42NewProductosNumeroVentas = BC000612_A42NewProductosNumeroVentas[0];
            A43NewProductosVisitas = BC000612_A43NewProductosVisitas[0];
            A20CategoriasId = BC000612_A20CategoriasId[0];
            A35NewProductosImagen = BC000612_A35NewProductosImagen[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanKeyNext067( )
      {
         /* Scan next routine */
         pr_default.readNext(10);
         RcdFound7 = 0;
         ScanKeyLoad067( ) ;
      }

      protected void ScanKeyLoad067( )
      {
         sMode7 = Gx_mode;
         Gx_mode = "DSP";
         if ( (pr_default.getStatus(10) != 101) )
         {
            RcdFound7 = 1;
            A34NewProductosId = BC000612_A34NewProductosId[0];
            A40000NewProductosImagen_GXI = BC000612_A40000NewProductosImagen_GXI[0];
            A36NewProductosNombre = BC000612_A36NewProductosNombre[0];
            A37NewProductosDescripcionCorta = BC000612_A37NewProductosDescripcionCorta[0];
            A38NewProductosDescripcion = BC000612_A38NewProductosDescripcion[0];
            A39NewProductosNumeroDescargas = BC000612_A39NewProductosNumeroDescargas[0];
            A40NewProductosLinkDescargaDemo = BC000612_A40NewProductosLinkDescargaDemo[0];
            A41NewProductosComprar = BC000612_A41NewProductosComprar[0];
            A42NewProductosNumeroVentas = BC000612_A42NewProductosNumeroVentas[0];
            A43NewProductosVisitas = BC000612_A43NewProductosVisitas[0];
            A20CategoriasId = BC000612_A20CategoriasId[0];
            A35NewProductosImagen = BC000612_A35NewProductosImagen[0];
         }
         Gx_mode = sMode7;
      }

      protected void ScanKeyEnd067( )
      {
         pr_default.close(10);
      }

      protected void AfterConfirm067( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert067( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate067( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete067( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete067( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate067( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes067( )
      {
      }

      protected void send_integrity_lvl_hashes067( )
      {
      }

      protected void AddRow067( )
      {
         VarsToRow7( bcNewProductos) ;
      }

      protected void ReadRow067( )
      {
         RowToVars7( bcNewProductos, 1) ;
      }

      protected void InitializeNonKey067( )
      {
         A35NewProductosImagen = "";
         A40000NewProductosImagen_GXI = "";
         A36NewProductosNombre = "";
         A37NewProductosDescripcionCorta = "";
         A38NewProductosDescripcion = "";
         A39NewProductosNumeroDescargas = 0;
         A40NewProductosLinkDescargaDemo = "";
         A41NewProductosComprar = "";
         A42NewProductosNumeroVentas = 0;
         A43NewProductosVisitas = 0;
         A20CategoriasId = 0;
         Z36NewProductosNombre = "";
         Z37NewProductosDescripcionCorta = "";
         Z39NewProductosNumeroDescargas = 0;
         Z40NewProductosLinkDescargaDemo = "";
         Z41NewProductosComprar = "";
         Z42NewProductosNumeroVentas = 0;
         Z43NewProductosVisitas = 0;
         Z20CategoriasId = 0;
      }

      protected void InitAll067( )
      {
         A34NewProductosId = 0;
         InitializeNonKey067( ) ;
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

      public void VarsToRow7( SdtNewProductos obj7 )
      {
         obj7.gxTpr_Mode = Gx_mode;
         obj7.gxTpr_Newproductosimagen = A35NewProductosImagen;
         obj7.gxTpr_Newproductosimagen_gxi = A40000NewProductosImagen_GXI;
         obj7.gxTpr_Newproductosnombre = A36NewProductosNombre;
         obj7.gxTpr_Newproductosdescripcioncorta = A37NewProductosDescripcionCorta;
         obj7.gxTpr_Newproductosdescripcion = A38NewProductosDescripcion;
         obj7.gxTpr_Newproductosnumerodescargas = A39NewProductosNumeroDescargas;
         obj7.gxTpr_Newproductoslinkdescargademo = A40NewProductosLinkDescargaDemo;
         obj7.gxTpr_Newproductoscomprar = A41NewProductosComprar;
         obj7.gxTpr_Newproductosnumeroventas = A42NewProductosNumeroVentas;
         obj7.gxTpr_Newproductosvisitas = A43NewProductosVisitas;
         obj7.gxTpr_Categoriasid = A20CategoriasId;
         obj7.gxTpr_Newproductosid = A34NewProductosId;
         obj7.gxTpr_Newproductosid_Z = Z34NewProductosId;
         obj7.gxTpr_Newproductosnombre_Z = Z36NewProductosNombre;
         obj7.gxTpr_Newproductosdescripcioncorta_Z = Z37NewProductosDescripcionCorta;
         obj7.gxTpr_Newproductosnumerodescargas_Z = Z39NewProductosNumeroDescargas;
         obj7.gxTpr_Newproductoslinkdescargademo_Z = Z40NewProductosLinkDescargaDemo;
         obj7.gxTpr_Newproductoscomprar_Z = Z41NewProductosComprar;
         obj7.gxTpr_Newproductosnumeroventas_Z = Z42NewProductosNumeroVentas;
         obj7.gxTpr_Newproductosvisitas_Z = Z43NewProductosVisitas;
         obj7.gxTpr_Categoriasid_Z = Z20CategoriasId;
         obj7.gxTpr_Newproductosimagen_gxi_Z = Z40000NewProductosImagen_GXI;
         obj7.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void KeyVarsToRow7( SdtNewProductos obj7 )
      {
         obj7.gxTpr_Newproductosid = A34NewProductosId;
         return  ;
      }

      public void RowToVars7( SdtNewProductos obj7 ,
                              int forceLoad )
      {
         Gx_mode = obj7.gxTpr_Mode;
         A35NewProductosImagen = obj7.gxTpr_Newproductosimagen;
         A40000NewProductosImagen_GXI = obj7.gxTpr_Newproductosimagen_gxi;
         A36NewProductosNombre = obj7.gxTpr_Newproductosnombre;
         A37NewProductosDescripcionCorta = obj7.gxTpr_Newproductosdescripcioncorta;
         A38NewProductosDescripcion = obj7.gxTpr_Newproductosdescripcion;
         A39NewProductosNumeroDescargas = obj7.gxTpr_Newproductosnumerodescargas;
         A40NewProductosLinkDescargaDemo = obj7.gxTpr_Newproductoslinkdescargademo;
         A41NewProductosComprar = obj7.gxTpr_Newproductoscomprar;
         A42NewProductosNumeroVentas = obj7.gxTpr_Newproductosnumeroventas;
         A43NewProductosVisitas = obj7.gxTpr_Newproductosvisitas;
         A20CategoriasId = obj7.gxTpr_Categoriasid;
         A34NewProductosId = obj7.gxTpr_Newproductosid;
         Z34NewProductosId = obj7.gxTpr_Newproductosid_Z;
         Z36NewProductosNombre = obj7.gxTpr_Newproductosnombre_Z;
         Z37NewProductosDescripcionCorta = obj7.gxTpr_Newproductosdescripcioncorta_Z;
         Z39NewProductosNumeroDescargas = obj7.gxTpr_Newproductosnumerodescargas_Z;
         Z40NewProductosLinkDescargaDemo = obj7.gxTpr_Newproductoslinkdescargademo_Z;
         Z41NewProductosComprar = obj7.gxTpr_Newproductoscomprar_Z;
         Z42NewProductosNumeroVentas = obj7.gxTpr_Newproductosnumeroventas_Z;
         Z43NewProductosVisitas = obj7.gxTpr_Newproductosvisitas_Z;
         Z20CategoriasId = obj7.gxTpr_Categoriasid_Z;
         Z40000NewProductosImagen_GXI = obj7.gxTpr_Newproductosimagen_gxi_Z;
         Gx_mode = obj7.gxTpr_Mode;
         return  ;
      }

      public void LoadKey( Object[] obj )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         A34NewProductosId = (short)getParm(obj,0);
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         InitializeNonKey067( ) ;
         ScanKeyStart067( ) ;
         if ( RcdFound7 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z34NewProductosId = A34NewProductosId;
         }
         ZM067( -5) ;
         OnLoadActions067( ) ;
         AddRow067( ) ;
         ScanKeyEnd067( ) ;
         if ( RcdFound7 == 0 )
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
         RowToVars7( bcNewProductos, 0) ;
         ScanKeyStart067( ) ;
         if ( RcdFound7 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z34NewProductosId = A34NewProductosId;
         }
         ZM067( -5) ;
         OnLoadActions067( ) ;
         AddRow067( ) ;
         ScanKeyEnd067( ) ;
         if ( RcdFound7 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      protected void SaveImpl( )
      {
         GetKey067( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            Insert067( ) ;
         }
         else
         {
            if ( RcdFound7 == 1 )
            {
               if ( A34NewProductosId != Z34NewProductosId )
               {
                  A34NewProductosId = Z34NewProductosId;
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
                  Update067( ) ;
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
                  if ( A34NewProductosId != Z34NewProductosId )
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
                        Insert067( ) ;
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
                        Insert067( ) ;
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
         RowToVars7( bcNewProductos, 1) ;
         SaveImpl( ) ;
         VarsToRow7( bcNewProductos) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public bool Insert( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars7( bcNewProductos, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert067( ) ;
         AfterTrn( ) ;
         VarsToRow7( bcNewProductos) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      protected void UpdateImpl( )
      {
         if ( IsUpd( ) )
         {
            SaveImpl( ) ;
            VarsToRow7( bcNewProductos) ;
         }
         else
         {
            SdtNewProductos auxBC = new SdtNewProductos(context);
            IGxSilentTrn auxTrn = auxBC.getTransaction();
            auxBC.Load(A34NewProductosId);
            if ( auxTrn.Errors() == 0 )
            {
               auxBC.UpdateDirties(bcNewProductos);
               auxBC.Save();
               bcNewProductos.Copy((GxSilentTrnSdt)(auxBC));
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
         RowToVars7( bcNewProductos, 1) ;
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
         RowToVars7( bcNewProductos, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert067( ) ;
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
               VarsToRow7( bcNewProductos) ;
            }
         }
         else
         {
            AfterTrn( ) ;
            VarsToRow7( bcNewProductos) ;
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
         RowToVars7( bcNewProductos, 0) ;
         GetKey067( ) ;
         if ( RcdFound7 == 1 )
         {
            if ( IsIns( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( A34NewProductosId != Z34NewProductosId )
            {
               A34NewProductosId = Z34NewProductosId;
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
            if ( A34NewProductosId != Z34NewProductosId )
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
         context.RollbackDataStores("newproductos_bc",pr_default);
         VarsToRow7( bcNewProductos) ;
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
         Gx_mode = bcNewProductos.gxTpr_Mode;
         return Gx_mode ;
      }

      public void SetMode( string lMode )
      {
         Gx_mode = lMode;
         bcNewProductos.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void SetSDT( GxSilentTrnSdt sdt ,
                          short sdtToBc )
      {
         if ( sdt != bcNewProductos )
         {
            bcNewProductos = (SdtNewProductos)(sdt);
            if ( StringUtil.StrCmp(bcNewProductos.gxTpr_Mode, "") == 0 )
            {
               bcNewProductos.gxTpr_Mode = "INS";
            }
            if ( sdtToBc == 1 )
            {
               VarsToRow7( bcNewProductos) ;
            }
            else
            {
               RowToVars7( bcNewProductos, 1) ;
            }
         }
         else
         {
            if ( StringUtil.StrCmp(bcNewProductos.gxTpr_Mode, "") == 0 )
            {
               bcNewProductos.gxTpr_Mode = "INS";
            }
         }
         return  ;
      }

      public void ReloadFromSDT( )
      {
         RowToVars7( bcNewProductos, 1) ;
         return  ;
      }

      public void ForceCommitOnExit( )
      {
         return  ;
      }

      public SdtNewProductos NewProductos_BC
      {
         get {
            return bcNewProductos ;
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
            return "newproductos_Execute" ;
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
         AV28Pgmname = "";
         AV15TrnContextAtt = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute(context);
         GXKey = "";
         GXEncryptionTmp = "";
         Z36NewProductosNombre = "";
         A36NewProductosNombre = "";
         Z37NewProductosDescripcionCorta = "";
         A37NewProductosDescripcionCorta = "";
         Z40NewProductosLinkDescargaDemo = "";
         A40NewProductosLinkDescargaDemo = "";
         Z41NewProductosComprar = "";
         A41NewProductosComprar = "";
         Z35NewProductosImagen = "";
         A35NewProductosImagen = "";
         Z40000NewProductosImagen_GXI = "";
         A40000NewProductosImagen_GXI = "";
         Z38NewProductosDescripcion = "";
         A38NewProductosDescripcion = "";
         BC00065_A34NewProductosId = new short[1] ;
         BC00065_A40000NewProductosImagen_GXI = new string[] {""} ;
         BC00065_A36NewProductosNombre = new string[] {""} ;
         BC00065_A37NewProductosDescripcionCorta = new string[] {""} ;
         BC00065_A38NewProductosDescripcion = new string[] {""} ;
         BC00065_A39NewProductosNumeroDescargas = new short[1] ;
         BC00065_A40NewProductosLinkDescargaDemo = new string[] {""} ;
         BC00065_A41NewProductosComprar = new string[] {""} ;
         BC00065_A42NewProductosNumeroVentas = new short[1] ;
         BC00065_A43NewProductosVisitas = new short[1] ;
         BC00065_A20CategoriasId = new short[1] ;
         BC00065_A35NewProductosImagen = new string[] {""} ;
         BC00064_A20CategoriasId = new short[1] ;
         BC00066_A34NewProductosId = new short[1] ;
         BC00063_A34NewProductosId = new short[1] ;
         BC00063_A40000NewProductosImagen_GXI = new string[] {""} ;
         BC00063_A36NewProductosNombre = new string[] {""} ;
         BC00063_A37NewProductosDescripcionCorta = new string[] {""} ;
         BC00063_A38NewProductosDescripcion = new string[] {""} ;
         BC00063_A39NewProductosNumeroDescargas = new short[1] ;
         BC00063_A40NewProductosLinkDescargaDemo = new string[] {""} ;
         BC00063_A41NewProductosComprar = new string[] {""} ;
         BC00063_A42NewProductosNumeroVentas = new short[1] ;
         BC00063_A43NewProductosVisitas = new short[1] ;
         BC00063_A20CategoriasId = new short[1] ;
         BC00063_A35NewProductosImagen = new string[] {""} ;
         sMode7 = "";
         BC00062_A34NewProductosId = new short[1] ;
         BC00062_A40000NewProductosImagen_GXI = new string[] {""} ;
         BC00062_A36NewProductosNombre = new string[] {""} ;
         BC00062_A37NewProductosDescripcionCorta = new string[] {""} ;
         BC00062_A38NewProductosDescripcion = new string[] {""} ;
         BC00062_A39NewProductosNumeroDescargas = new short[1] ;
         BC00062_A40NewProductosLinkDescargaDemo = new string[] {""} ;
         BC00062_A41NewProductosComprar = new string[] {""} ;
         BC00062_A42NewProductosNumeroVentas = new short[1] ;
         BC00062_A43NewProductosVisitas = new short[1] ;
         BC00062_A20CategoriasId = new short[1] ;
         BC00062_A35NewProductosImagen = new string[] {""} ;
         BC00068_A34NewProductosId = new short[1] ;
         BC000612_A34NewProductosId = new short[1] ;
         BC000612_A40000NewProductosImagen_GXI = new string[] {""} ;
         BC000612_A36NewProductosNombre = new string[] {""} ;
         BC000612_A37NewProductosDescripcionCorta = new string[] {""} ;
         BC000612_A38NewProductosDescripcion = new string[] {""} ;
         BC000612_A39NewProductosNumeroDescargas = new short[1] ;
         BC000612_A40NewProductosLinkDescargaDemo = new string[] {""} ;
         BC000612_A41NewProductosComprar = new string[] {""} ;
         BC000612_A42NewProductosNumeroVentas = new short[1] ;
         BC000612_A43NewProductosVisitas = new short[1] ;
         BC000612_A20CategoriasId = new short[1] ;
         BC000612_A35NewProductosImagen = new string[] {""} ;
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.newproductos_bc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.newproductos_bc__default(),
            new Object[][] {
                new Object[] {
               BC00062_A34NewProductosId, BC00062_A40000NewProductosImagen_GXI, BC00062_A36NewProductosNombre, BC00062_A37NewProductosDescripcionCorta, BC00062_A38NewProductosDescripcion, BC00062_A39NewProductosNumeroDescargas, BC00062_A40NewProductosLinkDescargaDemo, BC00062_A41NewProductosComprar, BC00062_A42NewProductosNumeroVentas, BC00062_A43NewProductosVisitas,
               BC00062_A20CategoriasId, BC00062_A35NewProductosImagen
               }
               , new Object[] {
               BC00063_A34NewProductosId, BC00063_A40000NewProductosImagen_GXI, BC00063_A36NewProductosNombre, BC00063_A37NewProductosDescripcionCorta, BC00063_A38NewProductosDescripcion, BC00063_A39NewProductosNumeroDescargas, BC00063_A40NewProductosLinkDescargaDemo, BC00063_A41NewProductosComprar, BC00063_A42NewProductosNumeroVentas, BC00063_A43NewProductosVisitas,
               BC00063_A20CategoriasId, BC00063_A35NewProductosImagen
               }
               , new Object[] {
               BC00064_A20CategoriasId
               }
               , new Object[] {
               BC00065_A34NewProductosId, BC00065_A40000NewProductosImagen_GXI, BC00065_A36NewProductosNombre, BC00065_A37NewProductosDescripcionCorta, BC00065_A38NewProductosDescripcion, BC00065_A39NewProductosNumeroDescargas, BC00065_A40NewProductosLinkDescargaDemo, BC00065_A41NewProductosComprar, BC00065_A42NewProductosNumeroVentas, BC00065_A43NewProductosVisitas,
               BC00065_A20CategoriasId, BC00065_A35NewProductosImagen
               }
               , new Object[] {
               BC00066_A34NewProductosId
               }
               , new Object[] {
               }
               , new Object[] {
               BC00068_A34NewProductosId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               BC000612_A34NewProductosId, BC000612_A40000NewProductosImagen_GXI, BC000612_A36NewProductosNombre, BC000612_A37NewProductosDescripcionCorta, BC000612_A38NewProductosDescripcion, BC000612_A39NewProductosNumeroDescargas, BC000612_A40NewProductosLinkDescargaDemo, BC000612_A41NewProductosComprar, BC000612_A42NewProductosNumeroVentas, BC000612_A43NewProductosVisitas,
               BC000612_A20CategoriasId, BC000612_A35NewProductosImagen
               }
            }
         );
         AV28Pgmname = "NewProductos_BC";
         INITTRN();
         /* Execute Start event if defined. */
         /* Execute user event: Start */
         E12062 ();
         standaloneNotModal( ) ;
      }

      private short AnyError ;
      private short Z34NewProductosId ;
      private short A34NewProductosId ;
      private short AV14Insert_CategoriasId ;
      private short gxcookieaux ;
      private short Z39NewProductosNumeroDescargas ;
      private short A39NewProductosNumeroDescargas ;
      private short Z42NewProductosNumeroVentas ;
      private short A42NewProductosNumeroVentas ;
      private short Z43NewProductosVisitas ;
      private short A43NewProductosVisitas ;
      private short Z20CategoriasId ;
      private short A20CategoriasId ;
      private short RcdFound7 ;
      private int trnEnded ;
      private int AV29GXV1 ;
      private string Gx_mode ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string AV28Pgmname ;
      private string GXKey ;
      private string GXEncryptionTmp ;
      private string sMode7 ;
      private bool returnInSub ;
      private bool AV24IsAuthorized_UserAction1 ;
      private bool Gx_longc ;
      private string Z38NewProductosDescripcion ;
      private string A38NewProductosDescripcion ;
      private string Z36NewProductosNombre ;
      private string A36NewProductosNombre ;
      private string Z37NewProductosDescripcionCorta ;
      private string A37NewProductosDescripcionCorta ;
      private string Z40NewProductosLinkDescargaDemo ;
      private string A40NewProductosLinkDescargaDemo ;
      private string Z41NewProductosComprar ;
      private string A41NewProductosComprar ;
      private string Z40000NewProductosImagen_GXI ;
      private string A40000NewProductosImagen_GXI ;
      private string Z35NewProductosImagen ;
      private string A35NewProductosImagen ;
      private IGxSession AV12WebSession ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV11TrnContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext_Attribute AV15TrnContextAtt ;
      private IDataStoreProvider pr_default ;
      private short[] BC00065_A34NewProductosId ;
      private string[] BC00065_A40000NewProductosImagen_GXI ;
      private string[] BC00065_A36NewProductosNombre ;
      private string[] BC00065_A37NewProductosDescripcionCorta ;
      private string[] BC00065_A38NewProductosDescripcion ;
      private short[] BC00065_A39NewProductosNumeroDescargas ;
      private string[] BC00065_A40NewProductosLinkDescargaDemo ;
      private string[] BC00065_A41NewProductosComprar ;
      private short[] BC00065_A42NewProductosNumeroVentas ;
      private short[] BC00065_A43NewProductosVisitas ;
      private short[] BC00065_A20CategoriasId ;
      private string[] BC00065_A35NewProductosImagen ;
      private short[] BC00064_A20CategoriasId ;
      private short[] BC00066_A34NewProductosId ;
      private short[] BC00063_A34NewProductosId ;
      private string[] BC00063_A40000NewProductosImagen_GXI ;
      private string[] BC00063_A36NewProductosNombre ;
      private string[] BC00063_A37NewProductosDescripcionCorta ;
      private string[] BC00063_A38NewProductosDescripcion ;
      private short[] BC00063_A39NewProductosNumeroDescargas ;
      private string[] BC00063_A40NewProductosLinkDescargaDemo ;
      private string[] BC00063_A41NewProductosComprar ;
      private short[] BC00063_A42NewProductosNumeroVentas ;
      private short[] BC00063_A43NewProductosVisitas ;
      private short[] BC00063_A20CategoriasId ;
      private string[] BC00063_A35NewProductosImagen ;
      private short[] BC00062_A34NewProductosId ;
      private string[] BC00062_A40000NewProductosImagen_GXI ;
      private string[] BC00062_A36NewProductosNombre ;
      private string[] BC00062_A37NewProductosDescripcionCorta ;
      private string[] BC00062_A38NewProductosDescripcion ;
      private short[] BC00062_A39NewProductosNumeroDescargas ;
      private string[] BC00062_A40NewProductosLinkDescargaDemo ;
      private string[] BC00062_A41NewProductosComprar ;
      private short[] BC00062_A42NewProductosNumeroVentas ;
      private short[] BC00062_A43NewProductosVisitas ;
      private short[] BC00062_A20CategoriasId ;
      private string[] BC00062_A35NewProductosImagen ;
      private short[] BC00068_A34NewProductosId ;
      private short[] BC000612_A34NewProductosId ;
      private string[] BC000612_A40000NewProductosImagen_GXI ;
      private string[] BC000612_A36NewProductosNombre ;
      private string[] BC000612_A37NewProductosDescripcionCorta ;
      private string[] BC000612_A38NewProductosDescripcion ;
      private short[] BC000612_A39NewProductosNumeroDescargas ;
      private string[] BC000612_A40NewProductosLinkDescargaDemo ;
      private string[] BC000612_A41NewProductosComprar ;
      private short[] BC000612_A42NewProductosNumeroVentas ;
      private short[] BC000612_A43NewProductosVisitas ;
      private short[] BC000612_A20CategoriasId ;
      private string[] BC000612_A35NewProductosImagen ;
      private SdtNewProductos bcNewProductos ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class newproductos_bc__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class newproductos_bc__default : DataStoreHelperBase, IDataStoreHelper
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
        Object[] prmBC00062;
        prmBC00062 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmBC00063;
        prmBC00063 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmBC00064;
        prmBC00064 = new Object[] {
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmBC00065;
        prmBC00065 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmBC00066;
        prmBC00066 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmBC00067;
        prmBC00067 = new Object[] {
        new ParDef("@NewProductosImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewProductosImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewProductos", Fld="NewProductosImagen"} ,
        new ParDef("@NewProductosNombre",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcionCorta",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcion",GXType.Char,2097152,0) ,
        new ParDef("@NewProductosNumeroDescargas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosLinkDescargaDemo",GXType.Char,1000,0) ,
        new ParDef("@NewProductosComprar",GXType.Char,1000,0) ,
        new ParDef("@NewProductosNumeroVentas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosVisitas",GXType.Int16,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0)
        };
        Object[] prmBC00068;
        prmBC00068 = new Object[] {
        };
        Object[] prmBC00069;
        prmBC00069 = new Object[] {
        new ParDef("@NewProductosNombre",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcionCorta",GXType.Char,200,0) ,
        new ParDef("@NewProductosDescripcion",GXType.Char,2097152,0) ,
        new ParDef("@NewProductosNumeroDescargas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosLinkDescargaDemo",GXType.Char,1000,0) ,
        new ParDef("@NewProductosComprar",GXType.Char,1000,0) ,
        new ParDef("@NewProductosNumeroVentas",GXType.Int16,4,0) ,
        new ParDef("@NewProductosVisitas",GXType.Int16,4,0) ,
        new ParDef("@CategoriasId",GXType.Int16,4,0) ,
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmBC000610;
        prmBC000610 = new Object[] {
        new ParDef("@NewProductosImagen",GXType.Blob,1024,0){InDB=false} ,
        new ParDef("@NewProductosImagen_GXI",GXType.Char,2048,0){AddAtt=true, ImgIdx=0, Tbl="NewProductos", Fld="NewProductosImagen"} ,
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmBC000611;
        prmBC000611 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        Object[] prmBC000612;
        prmBC000612 = new Object[] {
        new ParDef("@NewProductosId",GXType.Int16,4,0)
        };
        def= new CursorDef[] {
            new CursorDef("BC00062", "SELECT `NewProductosId`, `NewProductosImagen_GXI`, `NewProductosNombre`, `NewProductosDescripcionCorta`, `NewProductosDescripcion`, `NewProductosNumeroDescargas`, `NewProductosLinkDescargaDemo`, `NewProductosComprar`, `NewProductosNumeroVentas`, `NewProductosVisitas`, `CategoriasId`, `NewProductosImagen` FROM `NewProductos` WHERE `NewProductosId` = @NewProductosId  FOR UPDATE ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00062,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00063", "SELECT `NewProductosId`, `NewProductosImagen_GXI`, `NewProductosNombre`, `NewProductosDescripcionCorta`, `NewProductosDescripcion`, `NewProductosNumeroDescargas`, `NewProductosLinkDescargaDemo`, `NewProductosComprar`, `NewProductosNumeroVentas`, `NewProductosVisitas`, `CategoriasId`, `NewProductosImagen` FROM `NewProductos` WHERE `NewProductosId` = @NewProductosId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00063,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00064", "SELECT `CategoriasId` FROM `Categorias` WHERE `CategoriasId` = @CategoriasId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00064,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00065", "SELECT TM1.`NewProductosId`, TM1.`NewProductosImagen_GXI`, TM1.`NewProductosNombre`, TM1.`NewProductosDescripcionCorta`, TM1.`NewProductosDescripcion`, TM1.`NewProductosNumeroDescargas`, TM1.`NewProductosLinkDescargaDemo`, TM1.`NewProductosComprar`, TM1.`NewProductosNumeroVentas`, TM1.`NewProductosVisitas`, TM1.`CategoriasId`, TM1.`NewProductosImagen` FROM `NewProductos` TM1 WHERE TM1.`NewProductosId` = @NewProductosId ORDER BY TM1.`NewProductosId` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00065,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00066", "SELECT `NewProductosId` FROM `NewProductos` WHERE `NewProductosId` = @NewProductosId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00066,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00067", "INSERT INTO `NewProductos`(`NewProductosImagen`, `NewProductosImagen_GXI`, `NewProductosNombre`, `NewProductosDescripcionCorta`, `NewProductosDescripcion`, `NewProductosNumeroDescargas`, `NewProductosLinkDescargaDemo`, `NewProductosComprar`, `NewProductosNumeroVentas`, `NewProductosVisitas`, `CategoriasId`) VALUES(@NewProductosImagen, @NewProductosImagen_GXI, @NewProductosNombre, @NewProductosDescripcionCorta, @NewProductosDescripcion, @NewProductosNumeroDescargas, @NewProductosLinkDescargaDemo, @NewProductosComprar, @NewProductosNumeroVentas, @NewProductosVisitas, @CategoriasId)", GxErrorMask.GX_NOMASK,prmBC00067)
           ,new CursorDef("BC00068", "SELECT LAST_INSERT_ID() ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00068,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00069", "UPDATE `NewProductos` SET `NewProductosNombre`=@NewProductosNombre, `NewProductosDescripcionCorta`=@NewProductosDescripcionCorta, `NewProductosDescripcion`=@NewProductosDescripcion, `NewProductosNumeroDescargas`=@NewProductosNumeroDescargas, `NewProductosLinkDescargaDemo`=@NewProductosLinkDescargaDemo, `NewProductosComprar`=@NewProductosComprar, `NewProductosNumeroVentas`=@NewProductosNumeroVentas, `NewProductosVisitas`=@NewProductosVisitas, `CategoriasId`=@CategoriasId  WHERE `NewProductosId` = @NewProductosId", GxErrorMask.GX_NOMASK,prmBC00069)
           ,new CursorDef("BC000610", "UPDATE `NewProductos` SET `NewProductosImagen`=@NewProductosImagen, `NewProductosImagen_GXI`=@NewProductosImagen_GXI  WHERE `NewProductosId` = @NewProductosId", GxErrorMask.GX_NOMASK,prmBC000610)
           ,new CursorDef("BC000611", "DELETE FROM `NewProductos`  WHERE `NewProductosId` = @NewProductosId", GxErrorMask.GX_NOMASK,prmBC000611)
           ,new CursorDef("BC000612", "SELECT TM1.`NewProductosId`, TM1.`NewProductosImagen_GXI`, TM1.`NewProductosNombre`, TM1.`NewProductosDescripcionCorta`, TM1.`NewProductosDescripcion`, TM1.`NewProductosNumeroDescargas`, TM1.`NewProductosLinkDescargaDemo`, TM1.`NewProductosComprar`, TM1.`NewProductosNumeroVentas`, TM1.`NewProductosVisitas`, TM1.`CategoriasId`, TM1.`NewProductosImagen` FROM `NewProductos` TM1 WHERE TM1.`NewProductosId` = @NewProductosId ORDER BY TM1.`NewProductosId` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000612,100, GxCacheFrequency.OFF ,true,false )
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
              ((short[]) buf[5])[0] = rslt.getShort(6);
              ((string[]) buf[6])[0] = rslt.getVarchar(7);
              ((string[]) buf[7])[0] = rslt.getVarchar(8);
              ((short[]) buf[8])[0] = rslt.getShort(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((short[]) buf[10])[0] = rslt.getShort(11);
              ((string[]) buf[11])[0] = rslt.getMultimediaFile(12, rslt.getVarchar(2));
              return;
           case 1 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getMultimediaUri(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getVarchar(4);
              ((string[]) buf[4])[0] = rslt.getLongVarchar(5);
              ((short[]) buf[5])[0] = rslt.getShort(6);
              ((string[]) buf[6])[0] = rslt.getVarchar(7);
              ((string[]) buf[7])[0] = rslt.getVarchar(8);
              ((short[]) buf[8])[0] = rslt.getShort(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((short[]) buf[10])[0] = rslt.getShort(11);
              ((string[]) buf[11])[0] = rslt.getMultimediaFile(12, rslt.getVarchar(2));
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
              ((short[]) buf[5])[0] = rslt.getShort(6);
              ((string[]) buf[6])[0] = rslt.getVarchar(7);
              ((string[]) buf[7])[0] = rslt.getVarchar(8);
              ((short[]) buf[8])[0] = rslt.getShort(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((short[]) buf[10])[0] = rslt.getShort(11);
              ((string[]) buf[11])[0] = rslt.getMultimediaFile(12, rslt.getVarchar(2));
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
              ((short[]) buf[5])[0] = rslt.getShort(6);
              ((string[]) buf[6])[0] = rslt.getVarchar(7);
              ((string[]) buf[7])[0] = rslt.getVarchar(8);
              ((short[]) buf[8])[0] = rslt.getShort(9);
              ((short[]) buf[9])[0] = rslt.getShort(10);
              ((short[]) buf[10])[0] = rslt.getShort(11);
              ((string[]) buf[11])[0] = rslt.getMultimediaFile(12, rslt.getVarchar(2));
              return;
     }
  }

}

}
