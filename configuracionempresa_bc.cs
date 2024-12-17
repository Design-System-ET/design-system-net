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
   public class configuracionempresa_bc : GxSilentTrn, IGxSilentTrn
   {
      public configuracionempresa_bc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public configuracionempresa_bc( IGxContext context )
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
         ReadRow078( ) ;
         standaloneNotModal( ) ;
         InitializeNonKey078( ) ;
         standaloneModal( ) ;
         AddRow078( ) ;
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
            E11072 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               Z44ConfiguracionEmpresaId = A44ConfiguracionEmpresaId;
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

      protected void CONFIRM_070( )
      {
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls078( ) ;
            }
            else
            {
               CheckExtendedTable078( ) ;
               if ( AnyError == 0 )
               {
               }
               CloseExtendedTableCursors078( ) ;
            }
         }
         if ( AnyError == 0 )
         {
         }
      }

      protected void E12072( )
      {
         /* Start Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV11TrnContext.FromXml(AV12WebSession.Get("TrnContext"), null, "", "");
      }

      protected void E11072( )
      {
         /* After Trn Routine */
         returnInSub = false;
      }

      protected void ZM078( short GX_JID )
      {
         if ( ( GX_JID == 2 ) || ( GX_JID == 0 ) )
         {
            Z45ConfiguracionEmpresaTelefono = A45ConfiguracionEmpresaTelefono;
            Z46ConfiguracionEmpresaCostoPlanB = A46ConfiguracionEmpresaCostoPlanB;
            Z47ConfiguracionEmpresaCuotaPlanB = A47ConfiguracionEmpresaCuotaPlanB;
            Z48ConfiguracionEmpresaCostoPlanS = A48ConfiguracionEmpresaCostoPlanS;
            Z49ConfiguracionEmpresaCuotaPlanS = A49ConfiguracionEmpresaCuotaPlanS;
            Z50ConfiguracionEmpresaCostoPlanN = A50ConfiguracionEmpresaCostoPlanN;
            Z51ConfiguracionEmpresaCuotaPlanN = A51ConfiguracionEmpresaCuotaPlanN;
            Z54ConfiguracionEmpresaCostoLandi = A54ConfiguracionEmpresaCostoLandi;
            Z55ConfiguracionEmpresaCuotaLandi = A55ConfiguracionEmpresaCuotaLandi;
         }
         if ( GX_JID == -2 )
         {
            Z44ConfiguracionEmpresaId = A44ConfiguracionEmpresaId;
            Z45ConfiguracionEmpresaTelefono = A45ConfiguracionEmpresaTelefono;
            Z46ConfiguracionEmpresaCostoPlanB = A46ConfiguracionEmpresaCostoPlanB;
            Z47ConfiguracionEmpresaCuotaPlanB = A47ConfiguracionEmpresaCuotaPlanB;
            Z48ConfiguracionEmpresaCostoPlanS = A48ConfiguracionEmpresaCostoPlanS;
            Z49ConfiguracionEmpresaCuotaPlanS = A49ConfiguracionEmpresaCuotaPlanS;
            Z50ConfiguracionEmpresaCostoPlanN = A50ConfiguracionEmpresaCostoPlanN;
            Z51ConfiguracionEmpresaCuotaPlanN = A51ConfiguracionEmpresaCuotaPlanN;
            Z54ConfiguracionEmpresaCostoLandi = A54ConfiguracionEmpresaCostoLandi;
            Z55ConfiguracionEmpresaCuotaLandi = A55ConfiguracionEmpresaCuotaLandi;
         }
      }

      protected void standaloneNotModal( )
      {
         Gx_BScreen = 0;
      }

      protected void standaloneModal( )
      {
         if ( IsIns( )  && (0==A44ConfiguracionEmpresaId) && ( Gx_BScreen == 0 ) )
         {
            A44ConfiguracionEmpresaId = 1;
         }
      }

      protected void Load078( )
      {
         /* Using cursor BC00074 */
         pr_default.execute(2, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(2) != 101) )
         {
            RcdFound8 = 1;
            A45ConfiguracionEmpresaTelefono = BC00074_A45ConfiguracionEmpresaTelefono[0];
            A46ConfiguracionEmpresaCostoPlanB = BC00074_A46ConfiguracionEmpresaCostoPlanB[0];
            A47ConfiguracionEmpresaCuotaPlanB = BC00074_A47ConfiguracionEmpresaCuotaPlanB[0];
            A48ConfiguracionEmpresaCostoPlanS = BC00074_A48ConfiguracionEmpresaCostoPlanS[0];
            A49ConfiguracionEmpresaCuotaPlanS = BC00074_A49ConfiguracionEmpresaCuotaPlanS[0];
            A50ConfiguracionEmpresaCostoPlanN = BC00074_A50ConfiguracionEmpresaCostoPlanN[0];
            A51ConfiguracionEmpresaCuotaPlanN = BC00074_A51ConfiguracionEmpresaCuotaPlanN[0];
            A54ConfiguracionEmpresaCostoLandi = BC00074_A54ConfiguracionEmpresaCostoLandi[0];
            A55ConfiguracionEmpresaCuotaLandi = BC00074_A55ConfiguracionEmpresaCuotaLandi[0];
            ZM078( -2) ;
         }
         pr_default.close(2);
         OnLoadActions078( ) ;
      }

      protected void OnLoadActions078( )
      {
      }

      protected void CheckExtendedTable078( )
      {
         standaloneModal( ) ;
      }

      protected void CloseExtendedTableCursors078( )
      {
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey078( )
      {
         /* Using cursor BC00075 */
         pr_default.execute(3, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound8 = 1;
         }
         else
         {
            RcdFound8 = 0;
         }
         pr_default.close(3);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor BC00073 */
         pr_default.execute(1, new Object[] {A44ConfiguracionEmpresaId});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM078( 2) ;
            RcdFound8 = 1;
            A44ConfiguracionEmpresaId = BC00073_A44ConfiguracionEmpresaId[0];
            A45ConfiguracionEmpresaTelefono = BC00073_A45ConfiguracionEmpresaTelefono[0];
            A46ConfiguracionEmpresaCostoPlanB = BC00073_A46ConfiguracionEmpresaCostoPlanB[0];
            A47ConfiguracionEmpresaCuotaPlanB = BC00073_A47ConfiguracionEmpresaCuotaPlanB[0];
            A48ConfiguracionEmpresaCostoPlanS = BC00073_A48ConfiguracionEmpresaCostoPlanS[0];
            A49ConfiguracionEmpresaCuotaPlanS = BC00073_A49ConfiguracionEmpresaCuotaPlanS[0];
            A50ConfiguracionEmpresaCostoPlanN = BC00073_A50ConfiguracionEmpresaCostoPlanN[0];
            A51ConfiguracionEmpresaCuotaPlanN = BC00073_A51ConfiguracionEmpresaCuotaPlanN[0];
            A54ConfiguracionEmpresaCostoLandi = BC00073_A54ConfiguracionEmpresaCostoLandi[0];
            A55ConfiguracionEmpresaCuotaLandi = BC00073_A55ConfiguracionEmpresaCuotaLandi[0];
            Z44ConfiguracionEmpresaId = A44ConfiguracionEmpresaId;
            sMode8 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Load078( ) ;
            if ( AnyError == 1 )
            {
               RcdFound8 = 0;
               InitializeNonKey078( ) ;
            }
            Gx_mode = sMode8;
         }
         else
         {
            RcdFound8 = 0;
            InitializeNonKey078( ) ;
            sMode8 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Gx_mode = sMode8;
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey078( ) ;
         if ( RcdFound8 == 0 )
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
         CONFIRM_070( ) ;
      }

      protected void update_Check( )
      {
         insert_Check( ) ;
      }

      protected void delete_Check( )
      {
         insert_Check( ) ;
      }

      protected void CheckOptimisticConcurrency078( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor BC00072 */
            pr_default.execute(0, new Object[] {A44ConfiguracionEmpresaId});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"ConfiguracionEmpresa"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            Gx_longc = false;
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z45ConfiguracionEmpresaTelefono, BC00072_A45ConfiguracionEmpresaTelefono[0]) != 0 ) || ( Z46ConfiguracionEmpresaCostoPlanB != BC00072_A46ConfiguracionEmpresaCostoPlanB[0] ) || ( Z47ConfiguracionEmpresaCuotaPlanB != BC00072_A47ConfiguracionEmpresaCuotaPlanB[0] ) || ( Z48ConfiguracionEmpresaCostoPlanS != BC00072_A48ConfiguracionEmpresaCostoPlanS[0] ) || ( Z49ConfiguracionEmpresaCuotaPlanS != BC00072_A49ConfiguracionEmpresaCuotaPlanS[0] ) )
            {
               Gx_longc = true;
            }
            if ( Gx_longc || ( Z50ConfiguracionEmpresaCostoPlanN != BC00072_A50ConfiguracionEmpresaCostoPlanN[0] ) || ( Z51ConfiguracionEmpresaCuotaPlanN != BC00072_A51ConfiguracionEmpresaCuotaPlanN[0] ) || ( Z54ConfiguracionEmpresaCostoLandi != BC00072_A54ConfiguracionEmpresaCostoLandi[0] ) || ( Z55ConfiguracionEmpresaCuotaLandi != BC00072_A55ConfiguracionEmpresaCuotaLandi[0] ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"ConfiguracionEmpresa"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert078( )
      {
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable078( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM078( 0) ;
            CheckOptimisticConcurrency078( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm078( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert078( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00076 */
                     pr_default.execute(4, new Object[] {A45ConfiguracionEmpresaTelefono, A46ConfiguracionEmpresaCostoPlanB, A47ConfiguracionEmpresaCuotaPlanB, A48ConfiguracionEmpresaCostoPlanS, A49ConfiguracionEmpresaCuotaPlanS, A50ConfiguracionEmpresaCostoPlanN, A51ConfiguracionEmpresaCuotaPlanN, A54ConfiguracionEmpresaCostoLandi, A55ConfiguracionEmpresaCuotaLandi});
                     pr_default.close(4);
                     /* Retrieving last key number assigned */
                     /* Using cursor BC00077 */
                     pr_default.execute(5);
                     A44ConfiguracionEmpresaId = BC00077_A44ConfiguracionEmpresaId[0];
                     pr_default.close(5);
                     pr_default.SmartCacheProvider.SetUpdated("ConfiguracionEmpresa");
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
               Load078( ) ;
            }
            EndLevel078( ) ;
         }
         CloseExtendedTableCursors078( ) ;
      }

      protected void Update078( )
      {
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable078( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency078( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm078( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate078( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00078 */
                     pr_default.execute(6, new Object[] {A45ConfiguracionEmpresaTelefono, A46ConfiguracionEmpresaCostoPlanB, A47ConfiguracionEmpresaCuotaPlanB, A48ConfiguracionEmpresaCostoPlanS, A49ConfiguracionEmpresaCuotaPlanS, A50ConfiguracionEmpresaCostoPlanN, A51ConfiguracionEmpresaCuotaPlanN, A54ConfiguracionEmpresaCostoLandi, A55ConfiguracionEmpresaCuotaLandi, A44ConfiguracionEmpresaId});
                     pr_default.close(6);
                     pr_default.SmartCacheProvider.SetUpdated("ConfiguracionEmpresa");
                     if ( (pr_default.getStatus(6) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"ConfiguracionEmpresa"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate078( ) ;
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
            EndLevel078( ) ;
         }
         CloseExtendedTableCursors078( ) ;
      }

      protected void DeferredUpdate078( )
      {
      }

      protected void delete( )
      {
         Gx_mode = "DLT";
         BeforeValidate078( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency078( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls078( ) ;
            AfterConfirm078( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete078( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor BC00079 */
                  pr_default.execute(7, new Object[] {A44ConfiguracionEmpresaId});
                  pr_default.close(7);
                  pr_default.SmartCacheProvider.SetUpdated("ConfiguracionEmpresa");
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
         sMode8 = Gx_mode;
         Gx_mode = "DLT";
         EndLevel078( ) ;
         Gx_mode = sMode8;
      }

      protected void OnDeleteControls078( )
      {
         standaloneModal( ) ;
         /* No delete mode formulas found. */
      }

      protected void EndLevel078( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete078( ) ;
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

      public void ScanKeyStart078( )
      {
         /* Scan By routine */
         /* Using cursor BC000710 */
         pr_default.execute(8, new Object[] {A44ConfiguracionEmpresaId});
         RcdFound8 = 0;
         if ( (pr_default.getStatus(8) != 101) )
         {
            RcdFound8 = 1;
            A44ConfiguracionEmpresaId = BC000710_A44ConfiguracionEmpresaId[0];
            A45ConfiguracionEmpresaTelefono = BC000710_A45ConfiguracionEmpresaTelefono[0];
            A46ConfiguracionEmpresaCostoPlanB = BC000710_A46ConfiguracionEmpresaCostoPlanB[0];
            A47ConfiguracionEmpresaCuotaPlanB = BC000710_A47ConfiguracionEmpresaCuotaPlanB[0];
            A48ConfiguracionEmpresaCostoPlanS = BC000710_A48ConfiguracionEmpresaCostoPlanS[0];
            A49ConfiguracionEmpresaCuotaPlanS = BC000710_A49ConfiguracionEmpresaCuotaPlanS[0];
            A50ConfiguracionEmpresaCostoPlanN = BC000710_A50ConfiguracionEmpresaCostoPlanN[0];
            A51ConfiguracionEmpresaCuotaPlanN = BC000710_A51ConfiguracionEmpresaCuotaPlanN[0];
            A54ConfiguracionEmpresaCostoLandi = BC000710_A54ConfiguracionEmpresaCostoLandi[0];
            A55ConfiguracionEmpresaCuotaLandi = BC000710_A55ConfiguracionEmpresaCuotaLandi[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanKeyNext078( )
      {
         /* Scan next routine */
         pr_default.readNext(8);
         RcdFound8 = 0;
         ScanKeyLoad078( ) ;
      }

      protected void ScanKeyLoad078( )
      {
         sMode8 = Gx_mode;
         Gx_mode = "DSP";
         if ( (pr_default.getStatus(8) != 101) )
         {
            RcdFound8 = 1;
            A44ConfiguracionEmpresaId = BC000710_A44ConfiguracionEmpresaId[0];
            A45ConfiguracionEmpresaTelefono = BC000710_A45ConfiguracionEmpresaTelefono[0];
            A46ConfiguracionEmpresaCostoPlanB = BC000710_A46ConfiguracionEmpresaCostoPlanB[0];
            A47ConfiguracionEmpresaCuotaPlanB = BC000710_A47ConfiguracionEmpresaCuotaPlanB[0];
            A48ConfiguracionEmpresaCostoPlanS = BC000710_A48ConfiguracionEmpresaCostoPlanS[0];
            A49ConfiguracionEmpresaCuotaPlanS = BC000710_A49ConfiguracionEmpresaCuotaPlanS[0];
            A50ConfiguracionEmpresaCostoPlanN = BC000710_A50ConfiguracionEmpresaCostoPlanN[0];
            A51ConfiguracionEmpresaCuotaPlanN = BC000710_A51ConfiguracionEmpresaCuotaPlanN[0];
            A54ConfiguracionEmpresaCostoLandi = BC000710_A54ConfiguracionEmpresaCostoLandi[0];
            A55ConfiguracionEmpresaCuotaLandi = BC000710_A55ConfiguracionEmpresaCuotaLandi[0];
         }
         Gx_mode = sMode8;
      }

      protected void ScanKeyEnd078( )
      {
         pr_default.close(8);
      }

      protected void AfterConfirm078( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert078( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate078( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete078( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete078( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate078( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes078( )
      {
      }

      protected void send_integrity_lvl_hashes078( )
      {
      }

      protected void AddRow078( )
      {
         VarsToRow8( bcConfiguracionEmpresa) ;
      }

      protected void ReadRow078( )
      {
         RowToVars8( bcConfiguracionEmpresa, 1) ;
      }

      protected void InitializeNonKey078( )
      {
         A45ConfiguracionEmpresaTelefono = "";
         A46ConfiguracionEmpresaCostoPlanB = 0;
         A47ConfiguracionEmpresaCuotaPlanB = 0;
         A48ConfiguracionEmpresaCostoPlanS = 0;
         A49ConfiguracionEmpresaCuotaPlanS = 0;
         A50ConfiguracionEmpresaCostoPlanN = 0;
         A51ConfiguracionEmpresaCuotaPlanN = 0;
         A54ConfiguracionEmpresaCostoLandi = 0;
         A55ConfiguracionEmpresaCuotaLandi = 0;
         Z45ConfiguracionEmpresaTelefono = "";
         Z46ConfiguracionEmpresaCostoPlanB = 0;
         Z47ConfiguracionEmpresaCuotaPlanB = 0;
         Z48ConfiguracionEmpresaCostoPlanS = 0;
         Z49ConfiguracionEmpresaCuotaPlanS = 0;
         Z50ConfiguracionEmpresaCostoPlanN = 0;
         Z51ConfiguracionEmpresaCuotaPlanN = 0;
         Z54ConfiguracionEmpresaCostoLandi = 0;
         Z55ConfiguracionEmpresaCuotaLandi = 0;
      }

      protected void InitAll078( )
      {
         A44ConfiguracionEmpresaId = 1;
         InitializeNonKey078( ) ;
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

      public void VarsToRow8( SdtConfiguracionEmpresa obj8 )
      {
         obj8.gxTpr_Mode = Gx_mode;
         obj8.gxTpr_Configuracionempresatelefono = A45ConfiguracionEmpresaTelefono;
         obj8.gxTpr_Configuracionempresacostoplanbasico = A46ConfiguracionEmpresaCostoPlanB;
         obj8.gxTpr_Configuracionempresacuotaplanbasico = A47ConfiguracionEmpresaCuotaPlanB;
         obj8.gxTpr_Configuracionempresacostoplansuperior = A48ConfiguracionEmpresaCostoPlanS;
         obj8.gxTpr_Configuracionempresacuotaplansuperior = A49ConfiguracionEmpresaCuotaPlanS;
         obj8.gxTpr_Configuracionempresacostoplannegocios = A50ConfiguracionEmpresaCostoPlanN;
         obj8.gxTpr_Configuracionempresacuotaplannegocios = A51ConfiguracionEmpresaCuotaPlanN;
         obj8.gxTpr_Configuracionempresacostolandingpage = A54ConfiguracionEmpresaCostoLandi;
         obj8.gxTpr_Configuracionempresacuotalandingpage = A55ConfiguracionEmpresaCuotaLandi;
         obj8.gxTpr_Configuracionempresaid = A44ConfiguracionEmpresaId;
         obj8.gxTpr_Configuracionempresaid_Z = Z44ConfiguracionEmpresaId;
         obj8.gxTpr_Configuracionempresatelefono_Z = Z45ConfiguracionEmpresaTelefono;
         obj8.gxTpr_Configuracionempresacostoplanbasico_Z = Z46ConfiguracionEmpresaCostoPlanB;
         obj8.gxTpr_Configuracionempresacuotaplanbasico_Z = Z47ConfiguracionEmpresaCuotaPlanB;
         obj8.gxTpr_Configuracionempresacostoplansuperior_Z = Z48ConfiguracionEmpresaCostoPlanS;
         obj8.gxTpr_Configuracionempresacuotaplansuperior_Z = Z49ConfiguracionEmpresaCuotaPlanS;
         obj8.gxTpr_Configuracionempresacostoplannegocios_Z = Z50ConfiguracionEmpresaCostoPlanN;
         obj8.gxTpr_Configuracionempresacuotaplannegocios_Z = Z51ConfiguracionEmpresaCuotaPlanN;
         obj8.gxTpr_Configuracionempresacostolandingpage_Z = Z54ConfiguracionEmpresaCostoLandi;
         obj8.gxTpr_Configuracionempresacuotalandingpage_Z = Z55ConfiguracionEmpresaCuotaLandi;
         obj8.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void KeyVarsToRow8( SdtConfiguracionEmpresa obj8 )
      {
         obj8.gxTpr_Configuracionempresaid = A44ConfiguracionEmpresaId;
         return  ;
      }

      public void RowToVars8( SdtConfiguracionEmpresa obj8 ,
                              int forceLoad )
      {
         Gx_mode = obj8.gxTpr_Mode;
         A45ConfiguracionEmpresaTelefono = obj8.gxTpr_Configuracionempresatelefono;
         A46ConfiguracionEmpresaCostoPlanB = obj8.gxTpr_Configuracionempresacostoplanbasico;
         A47ConfiguracionEmpresaCuotaPlanB = obj8.gxTpr_Configuracionempresacuotaplanbasico;
         A48ConfiguracionEmpresaCostoPlanS = obj8.gxTpr_Configuracionempresacostoplansuperior;
         A49ConfiguracionEmpresaCuotaPlanS = obj8.gxTpr_Configuracionempresacuotaplansuperior;
         A50ConfiguracionEmpresaCostoPlanN = obj8.gxTpr_Configuracionempresacostoplannegocios;
         A51ConfiguracionEmpresaCuotaPlanN = obj8.gxTpr_Configuracionempresacuotaplannegocios;
         A54ConfiguracionEmpresaCostoLandi = obj8.gxTpr_Configuracionempresacostolandingpage;
         A55ConfiguracionEmpresaCuotaLandi = obj8.gxTpr_Configuracionempresacuotalandingpage;
         A44ConfiguracionEmpresaId = obj8.gxTpr_Configuracionempresaid;
         Z44ConfiguracionEmpresaId = obj8.gxTpr_Configuracionempresaid_Z;
         Z45ConfiguracionEmpresaTelefono = obj8.gxTpr_Configuracionempresatelefono_Z;
         Z46ConfiguracionEmpresaCostoPlanB = obj8.gxTpr_Configuracionempresacostoplanbasico_Z;
         Z47ConfiguracionEmpresaCuotaPlanB = obj8.gxTpr_Configuracionempresacuotaplanbasico_Z;
         Z48ConfiguracionEmpresaCostoPlanS = obj8.gxTpr_Configuracionempresacostoplansuperior_Z;
         Z49ConfiguracionEmpresaCuotaPlanS = obj8.gxTpr_Configuracionempresacuotaplansuperior_Z;
         Z50ConfiguracionEmpresaCostoPlanN = obj8.gxTpr_Configuracionempresacostoplannegocios_Z;
         Z51ConfiguracionEmpresaCuotaPlanN = obj8.gxTpr_Configuracionempresacuotaplannegocios_Z;
         Z54ConfiguracionEmpresaCostoLandi = obj8.gxTpr_Configuracionempresacostolandingpage_Z;
         Z55ConfiguracionEmpresaCuotaLandi = obj8.gxTpr_Configuracionempresacuotalandingpage_Z;
         Gx_mode = obj8.gxTpr_Mode;
         return  ;
      }

      public void LoadKey( Object[] obj )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         A44ConfiguracionEmpresaId = (short)getParm(obj,0);
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         InitializeNonKey078( ) ;
         ScanKeyStart078( ) ;
         if ( RcdFound8 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z44ConfiguracionEmpresaId = A44ConfiguracionEmpresaId;
         }
         ZM078( -2) ;
         OnLoadActions078( ) ;
         AddRow078( ) ;
         ScanKeyEnd078( ) ;
         if ( RcdFound8 == 0 )
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
         RowToVars8( bcConfiguracionEmpresa, 0) ;
         ScanKeyStart078( ) ;
         if ( RcdFound8 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z44ConfiguracionEmpresaId = A44ConfiguracionEmpresaId;
         }
         ZM078( -2) ;
         OnLoadActions078( ) ;
         AddRow078( ) ;
         ScanKeyEnd078( ) ;
         if ( RcdFound8 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      protected void SaveImpl( )
      {
         GetKey078( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            Insert078( ) ;
         }
         else
         {
            if ( RcdFound8 == 1 )
            {
               if ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId )
               {
                  A44ConfiguracionEmpresaId = Z44ConfiguracionEmpresaId;
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
                  Update078( ) ;
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
                  if ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId )
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
                        Insert078( ) ;
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
                        Insert078( ) ;
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
         RowToVars8( bcConfiguracionEmpresa, 1) ;
         SaveImpl( ) ;
         VarsToRow8( bcConfiguracionEmpresa) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public bool Insert( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars8( bcConfiguracionEmpresa, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert078( ) ;
         AfterTrn( ) ;
         VarsToRow8( bcConfiguracionEmpresa) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      protected void UpdateImpl( )
      {
         if ( IsUpd( ) )
         {
            SaveImpl( ) ;
            VarsToRow8( bcConfiguracionEmpresa) ;
         }
         else
         {
            SdtConfiguracionEmpresa auxBC = new SdtConfiguracionEmpresa(context);
            IGxSilentTrn auxTrn = auxBC.getTransaction();
            auxBC.Load(A44ConfiguracionEmpresaId);
            if ( auxTrn.Errors() == 0 )
            {
               auxBC.UpdateDirties(bcConfiguracionEmpresa);
               auxBC.Save();
               bcConfiguracionEmpresa.Copy((GxSilentTrnSdt)(auxBC));
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
         RowToVars8( bcConfiguracionEmpresa, 1) ;
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
         RowToVars8( bcConfiguracionEmpresa, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert078( ) ;
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
               VarsToRow8( bcConfiguracionEmpresa) ;
            }
         }
         else
         {
            AfterTrn( ) ;
            VarsToRow8( bcConfiguracionEmpresa) ;
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
         RowToVars8( bcConfiguracionEmpresa, 0) ;
         GetKey078( ) ;
         if ( RcdFound8 == 1 )
         {
            if ( IsIns( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId )
            {
               A44ConfiguracionEmpresaId = Z44ConfiguracionEmpresaId;
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
            if ( A44ConfiguracionEmpresaId != Z44ConfiguracionEmpresaId )
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
         context.RollbackDataStores("configuracionempresa_bc",pr_default);
         VarsToRow8( bcConfiguracionEmpresa) ;
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
         Gx_mode = bcConfiguracionEmpresa.gxTpr_Mode;
         return Gx_mode ;
      }

      public void SetMode( string lMode )
      {
         Gx_mode = lMode;
         bcConfiguracionEmpresa.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void SetSDT( GxSilentTrnSdt sdt ,
                          short sdtToBc )
      {
         if ( sdt != bcConfiguracionEmpresa )
         {
            bcConfiguracionEmpresa = (SdtConfiguracionEmpresa)(sdt);
            if ( StringUtil.StrCmp(bcConfiguracionEmpresa.gxTpr_Mode, "") == 0 )
            {
               bcConfiguracionEmpresa.gxTpr_Mode = "INS";
            }
            if ( sdtToBc == 1 )
            {
               VarsToRow8( bcConfiguracionEmpresa) ;
            }
            else
            {
               RowToVars8( bcConfiguracionEmpresa, 1) ;
            }
         }
         else
         {
            if ( StringUtil.StrCmp(bcConfiguracionEmpresa.gxTpr_Mode, "") == 0 )
            {
               bcConfiguracionEmpresa.gxTpr_Mode = "INS";
            }
         }
         return  ;
      }

      public void ReloadFromSDT( )
      {
         RowToVars8( bcConfiguracionEmpresa, 1) ;
         return  ;
      }

      public void ForceCommitOnExit( )
      {
         return  ;
      }

      public SdtConfiguracionEmpresa ConfiguracionEmpresa_BC
      {
         get {
            return bcConfiguracionEmpresa ;
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
            return "configuracionempresa_Execute" ;
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
         Z45ConfiguracionEmpresaTelefono = "";
         A45ConfiguracionEmpresaTelefono = "";
         BC00074_A44ConfiguracionEmpresaId = new short[1] ;
         BC00074_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         BC00074_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         BC00074_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         BC00074_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         BC00074_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         BC00074_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         BC00074_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         BC00074_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         BC00074_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         BC00075_A44ConfiguracionEmpresaId = new short[1] ;
         BC00073_A44ConfiguracionEmpresaId = new short[1] ;
         BC00073_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         BC00073_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         BC00073_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         BC00073_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         BC00073_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         BC00073_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         BC00073_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         BC00073_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         BC00073_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         sMode8 = "";
         BC00072_A44ConfiguracionEmpresaId = new short[1] ;
         BC00072_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         BC00072_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         BC00072_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         BC00072_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         BC00072_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         BC00072_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         BC00072_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         BC00072_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         BC00072_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         BC00077_A44ConfiguracionEmpresaId = new short[1] ;
         BC000710_A44ConfiguracionEmpresaId = new short[1] ;
         BC000710_A45ConfiguracionEmpresaTelefono = new string[] {""} ;
         BC000710_A46ConfiguracionEmpresaCostoPlanB = new decimal[1] ;
         BC000710_A47ConfiguracionEmpresaCuotaPlanB = new decimal[1] ;
         BC000710_A48ConfiguracionEmpresaCostoPlanS = new decimal[1] ;
         BC000710_A49ConfiguracionEmpresaCuotaPlanS = new decimal[1] ;
         BC000710_A50ConfiguracionEmpresaCostoPlanN = new decimal[1] ;
         BC000710_A51ConfiguracionEmpresaCuotaPlanN = new decimal[1] ;
         BC000710_A54ConfiguracionEmpresaCostoLandi = new decimal[1] ;
         BC000710_A55ConfiguracionEmpresaCuotaLandi = new decimal[1] ;
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresa_bc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.configuracionempresa_bc__default(),
            new Object[][] {
                new Object[] {
               BC00072_A44ConfiguracionEmpresaId, BC00072_A45ConfiguracionEmpresaTelefono, BC00072_A46ConfiguracionEmpresaCostoPlanB, BC00072_A47ConfiguracionEmpresaCuotaPlanB, BC00072_A48ConfiguracionEmpresaCostoPlanS, BC00072_A49ConfiguracionEmpresaCuotaPlanS, BC00072_A50ConfiguracionEmpresaCostoPlanN, BC00072_A51ConfiguracionEmpresaCuotaPlanN, BC00072_A54ConfiguracionEmpresaCostoLandi, BC00072_A55ConfiguracionEmpresaCuotaLandi
               }
               , new Object[] {
               BC00073_A44ConfiguracionEmpresaId, BC00073_A45ConfiguracionEmpresaTelefono, BC00073_A46ConfiguracionEmpresaCostoPlanB, BC00073_A47ConfiguracionEmpresaCuotaPlanB, BC00073_A48ConfiguracionEmpresaCostoPlanS, BC00073_A49ConfiguracionEmpresaCuotaPlanS, BC00073_A50ConfiguracionEmpresaCostoPlanN, BC00073_A51ConfiguracionEmpresaCuotaPlanN, BC00073_A54ConfiguracionEmpresaCostoLandi, BC00073_A55ConfiguracionEmpresaCuotaLandi
               }
               , new Object[] {
               BC00074_A44ConfiguracionEmpresaId, BC00074_A45ConfiguracionEmpresaTelefono, BC00074_A46ConfiguracionEmpresaCostoPlanB, BC00074_A47ConfiguracionEmpresaCuotaPlanB, BC00074_A48ConfiguracionEmpresaCostoPlanS, BC00074_A49ConfiguracionEmpresaCuotaPlanS, BC00074_A50ConfiguracionEmpresaCostoPlanN, BC00074_A51ConfiguracionEmpresaCuotaPlanN, BC00074_A54ConfiguracionEmpresaCostoLandi, BC00074_A55ConfiguracionEmpresaCuotaLandi
               }
               , new Object[] {
               BC00075_A44ConfiguracionEmpresaId
               }
               , new Object[] {
               }
               , new Object[] {
               BC00077_A44ConfiguracionEmpresaId
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               BC000710_A44ConfiguracionEmpresaId, BC000710_A45ConfiguracionEmpresaTelefono, BC000710_A46ConfiguracionEmpresaCostoPlanB, BC000710_A47ConfiguracionEmpresaCuotaPlanB, BC000710_A48ConfiguracionEmpresaCostoPlanS, BC000710_A49ConfiguracionEmpresaCuotaPlanS, BC000710_A50ConfiguracionEmpresaCostoPlanN, BC000710_A51ConfiguracionEmpresaCuotaPlanN, BC000710_A54ConfiguracionEmpresaCostoLandi, BC000710_A55ConfiguracionEmpresaCuotaLandi
               }
            }
         );
         Z44ConfiguracionEmpresaId = 1;
         A44ConfiguracionEmpresaId = 1;
         INITTRN();
         /* Execute Start event if defined. */
         /* Execute user event: Start */
         E12072 ();
         standaloneNotModal( ) ;
      }

      private short AnyError ;
      private short Z44ConfiguracionEmpresaId ;
      private short A44ConfiguracionEmpresaId ;
      private short Gx_BScreen ;
      private short RcdFound8 ;
      private int trnEnded ;
      private decimal Z46ConfiguracionEmpresaCostoPlanB ;
      private decimal A46ConfiguracionEmpresaCostoPlanB ;
      private decimal Z47ConfiguracionEmpresaCuotaPlanB ;
      private decimal A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal Z48ConfiguracionEmpresaCostoPlanS ;
      private decimal A48ConfiguracionEmpresaCostoPlanS ;
      private decimal Z49ConfiguracionEmpresaCuotaPlanS ;
      private decimal A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal Z50ConfiguracionEmpresaCostoPlanN ;
      private decimal A50ConfiguracionEmpresaCostoPlanN ;
      private decimal Z51ConfiguracionEmpresaCuotaPlanN ;
      private decimal A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal Z54ConfiguracionEmpresaCostoLandi ;
      private decimal A54ConfiguracionEmpresaCostoLandi ;
      private decimal Z55ConfiguracionEmpresaCuotaLandi ;
      private decimal A55ConfiguracionEmpresaCuotaLandi ;
      private string Gx_mode ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string Z45ConfiguracionEmpresaTelefono ;
      private string A45ConfiguracionEmpresaTelefono ;
      private string sMode8 ;
      private bool returnInSub ;
      private bool Gx_longc ;
      private IGxSession AV12WebSession ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV11TrnContext ;
      private IDataStoreProvider pr_default ;
      private short[] BC00074_A44ConfiguracionEmpresaId ;
      private string[] BC00074_A45ConfiguracionEmpresaTelefono ;
      private decimal[] BC00074_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] BC00074_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] BC00074_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] BC00074_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] BC00074_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] BC00074_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] BC00074_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] BC00074_A55ConfiguracionEmpresaCuotaLandi ;
      private short[] BC00075_A44ConfiguracionEmpresaId ;
      private short[] BC00073_A44ConfiguracionEmpresaId ;
      private string[] BC00073_A45ConfiguracionEmpresaTelefono ;
      private decimal[] BC00073_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] BC00073_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] BC00073_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] BC00073_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] BC00073_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] BC00073_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] BC00073_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] BC00073_A55ConfiguracionEmpresaCuotaLandi ;
      private short[] BC00072_A44ConfiguracionEmpresaId ;
      private string[] BC00072_A45ConfiguracionEmpresaTelefono ;
      private decimal[] BC00072_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] BC00072_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] BC00072_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] BC00072_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] BC00072_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] BC00072_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] BC00072_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] BC00072_A55ConfiguracionEmpresaCuotaLandi ;
      private short[] BC00077_A44ConfiguracionEmpresaId ;
      private short[] BC000710_A44ConfiguracionEmpresaId ;
      private string[] BC000710_A45ConfiguracionEmpresaTelefono ;
      private decimal[] BC000710_A46ConfiguracionEmpresaCostoPlanB ;
      private decimal[] BC000710_A47ConfiguracionEmpresaCuotaPlanB ;
      private decimal[] BC000710_A48ConfiguracionEmpresaCostoPlanS ;
      private decimal[] BC000710_A49ConfiguracionEmpresaCuotaPlanS ;
      private decimal[] BC000710_A50ConfiguracionEmpresaCostoPlanN ;
      private decimal[] BC000710_A51ConfiguracionEmpresaCuotaPlanN ;
      private decimal[] BC000710_A54ConfiguracionEmpresaCostoLandi ;
      private decimal[] BC000710_A55ConfiguracionEmpresaCuotaLandi ;
      private SdtConfiguracionEmpresa bcConfiguracionEmpresa ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class configuracionempresa_bc__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class configuracionempresa_bc__default : DataStoreHelperBase, IDataStoreHelper
 {
    public ICursor[] getCursors( )
    {
       cursorDefinitions();
       return new Cursor[] {
        new ForEachCursor(def[0])
       ,new ForEachCursor(def[1])
       ,new ForEachCursor(def[2])
       ,new ForEachCursor(def[3])
       ,new UpdateCursor(def[4])
       ,new ForEachCursor(def[5])
       ,new UpdateCursor(def[6])
       ,new UpdateCursor(def[7])
       ,new ForEachCursor(def[8])
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        Object[] prmBC00072;
        prmBC00072 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmBC00073;
        prmBC00073 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmBC00074;
        prmBC00074 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmBC00075;
        prmBC00075 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmBC00076;
        prmBC00076 = new Object[] {
        new ParDef("@ConfiguracionEmpresaTelefono",GXType.Char,20,0) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoLandi",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaLandi",GXType.Number,12,2)
        };
        Object[] prmBC00077;
        prmBC00077 = new Object[] {
        };
        Object[] prmBC00078;
        prmBC00078 = new Object[] {
        new ParDef("@ConfiguracionEmpresaTelefono",GXType.Char,20,0) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanB",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanS",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaPlanN",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCostoLandi",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaCuotaLandi",GXType.Number,12,2) ,
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmBC00079;
        prmBC00079 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        Object[] prmBC000710;
        prmBC000710 = new Object[] {
        new ParDef("@ConfiguracionEmpresaId",GXType.Int16,4,0)
        };
        def= new CursorDef[] {
            new CursorDef("BC00072", "SELECT `ConfiguracionEmpresaId`, `ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId  FOR UPDATE ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00072,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00073", "SELECT `ConfiguracionEmpresaId`, `ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00073,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00074", "SELECT TM1.`ConfiguracionEmpresaId`, TM1.`ConfiguracionEmpresaTelefono`, TM1.`ConfiguracionEmpresaCostoPlanB`, TM1.`ConfiguracionEmpresaCuotaPlanB`, TM1.`ConfiguracionEmpresaCostoPlanS`, TM1.`ConfiguracionEmpresaCuotaPlanS`, TM1.`ConfiguracionEmpresaCostoPlanN`, TM1.`ConfiguracionEmpresaCuotaPlanN`, TM1.`ConfiguracionEmpresaCostoLandi`, TM1.`ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` TM1 WHERE TM1.`ConfiguracionEmpresaId` = @ConfiguracionEmpresaId ORDER BY TM1.`ConfiguracionEmpresaId` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00074,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00075", "SELECT `ConfiguracionEmpresaId` FROM `ConfiguracionEmpresa` WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00075,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00076", "INSERT INTO `ConfiguracionEmpresa`(`ConfiguracionEmpresaTelefono`, `ConfiguracionEmpresaCostoPlanB`, `ConfiguracionEmpresaCuotaPlanB`, `ConfiguracionEmpresaCostoPlanS`, `ConfiguracionEmpresaCuotaPlanS`, `ConfiguracionEmpresaCostoPlanN`, `ConfiguracionEmpresaCuotaPlanN`, `ConfiguracionEmpresaCostoLandi`, `ConfiguracionEmpresaCuotaLandi`) VALUES(@ConfiguracionEmpresaTelefono, @ConfiguracionEmpresaCostoPlanB, @ConfiguracionEmpresaCuotaPlanB, @ConfiguracionEmpresaCostoPlanS, @ConfiguracionEmpresaCuotaPlanS, @ConfiguracionEmpresaCostoPlanN, @ConfiguracionEmpresaCuotaPlanN, @ConfiguracionEmpresaCostoLandi, @ConfiguracionEmpresaCuotaLandi)", GxErrorMask.GX_NOMASK,prmBC00076)
           ,new CursorDef("BC00077", "SELECT LAST_INSERT_ID() ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00077,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00078", "UPDATE `ConfiguracionEmpresa` SET `ConfiguracionEmpresaTelefono`=@ConfiguracionEmpresaTelefono, `ConfiguracionEmpresaCostoPlanB`=@ConfiguracionEmpresaCostoPlanB, `ConfiguracionEmpresaCuotaPlanB`=@ConfiguracionEmpresaCuotaPlanB, `ConfiguracionEmpresaCostoPlanS`=@ConfiguracionEmpresaCostoPlanS, `ConfiguracionEmpresaCuotaPlanS`=@ConfiguracionEmpresaCuotaPlanS, `ConfiguracionEmpresaCostoPlanN`=@ConfiguracionEmpresaCostoPlanN, `ConfiguracionEmpresaCuotaPlanN`=@ConfiguracionEmpresaCuotaPlanN, `ConfiguracionEmpresaCostoLandi`=@ConfiguracionEmpresaCostoLandi, `ConfiguracionEmpresaCuotaLandi`=@ConfiguracionEmpresaCuotaLandi  WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId", GxErrorMask.GX_NOMASK,prmBC00078)
           ,new CursorDef("BC00079", "DELETE FROM `ConfiguracionEmpresa`  WHERE `ConfiguracionEmpresaId` = @ConfiguracionEmpresaId", GxErrorMask.GX_NOMASK,prmBC00079)
           ,new CursorDef("BC000710", "SELECT TM1.`ConfiguracionEmpresaId`, TM1.`ConfiguracionEmpresaTelefono`, TM1.`ConfiguracionEmpresaCostoPlanB`, TM1.`ConfiguracionEmpresaCuotaPlanB`, TM1.`ConfiguracionEmpresaCostoPlanS`, TM1.`ConfiguracionEmpresaCuotaPlanS`, TM1.`ConfiguracionEmpresaCostoPlanN`, TM1.`ConfiguracionEmpresaCuotaPlanN`, TM1.`ConfiguracionEmpresaCostoLandi`, TM1.`ConfiguracionEmpresaCuotaLandi` FROM `ConfiguracionEmpresa` TM1 WHERE TM1.`ConfiguracionEmpresaId` = @ConfiguracionEmpresaId ORDER BY TM1.`ConfiguracionEmpresaId` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC000710,100, GxCacheFrequency.OFF ,true,false )
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
              ((string[]) buf[1])[0] = rslt.getString(2, 20);
              ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
              ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
              ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
              ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
              ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
              ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
              ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
              ((decimal[]) buf[9])[0] = rslt.getDecimal(10);
              return;
           case 1 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getString(2, 20);
              ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
              ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
              ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
              ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
              ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
              ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
              ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
              ((decimal[]) buf[9])[0] = rslt.getDecimal(10);
              return;
           case 2 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getString(2, 20);
              ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
              ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
              ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
              ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
              ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
              ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
              ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
              ((decimal[]) buf[9])[0] = rslt.getDecimal(10);
              return;
           case 3 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 5 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              return;
           case 8 :
              ((short[]) buf[0])[0] = rslt.getShort(1);
              ((string[]) buf[1])[0] = rslt.getString(2, 20);
              ((decimal[]) buf[2])[0] = rslt.getDecimal(3);
              ((decimal[]) buf[3])[0] = rslt.getDecimal(4);
              ((decimal[]) buf[4])[0] = rslt.getDecimal(5);
              ((decimal[]) buf[5])[0] = rslt.getDecimal(6);
              ((decimal[]) buf[6])[0] = rslt.getDecimal(7);
              ((decimal[]) buf[7])[0] = rslt.getDecimal(8);
              ((decimal[]) buf[8])[0] = rslt.getDecimal(9);
              ((decimal[]) buf[9])[0] = rslt.getDecimal(10);
              return;
     }
  }

}

}
