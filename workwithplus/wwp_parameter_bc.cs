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
namespace DesignSystem.Programs.workwithplus {
   public class wwp_parameter_bc : GxSilentTrn, IGxSilentTrn
   {
      public wwp_parameter_bc( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", true);
      }

      public wwp_parameter_bc( IGxContext context )
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
         ReadRow011( ) ;
         standaloneNotModal( ) ;
         InitializeNonKey011( ) ;
         standaloneModal( ) ;
         AddRow011( ) ;
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
            E11012 ();
            trnEnded = 0;
            standaloneNotModal( ) ;
            standaloneModal( ) ;
            if ( IsIns( )  )
            {
               Z1WWPParameterKey = A1WWPParameterKey;
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

      protected void CONFIRM_010( )
      {
         BeforeValidate011( ) ;
         if ( AnyError == 0 )
         {
            if ( IsDlt( ) )
            {
               OnDeleteControls011( ) ;
            }
            else
            {
               CheckExtendedTable011( ) ;
               if ( AnyError == 0 )
               {
               }
               CloseExtendedTableCursors011( ) ;
            }
         }
         if ( AnyError == 0 )
         {
         }
      }

      protected void E12012( )
      {
         /* Start Routine */
         returnInSub = false;
         new DesignSystem.Programs.wwpbaseobjects.loadwwpcontext(context ).execute( out  AV8WWPContext) ;
         AV9TrnContext.FromXml(AV10WebSession.Get("TrnContext"), null, "", "");
      }

      protected void E11012( )
      {
         /* After Trn Routine */
         returnInSub = false;
      }

      protected void ZM011( short GX_JID )
      {
         if ( ( GX_JID == 3 ) || ( GX_JID == 0 ) )
         {
            Z3WWPParameterCategory = A3WWPParameterCategory;
            Z4WWPParameterDescription = A4WWPParameterDescription;
            Z5WWPParameterDisableDelete = A5WWPParameterDisableDelete;
            Z6WWPParameterValueTrimmed = A6WWPParameterValueTrimmed;
         }
         if ( GX_JID == -3 )
         {
            Z1WWPParameterKey = A1WWPParameterKey;
            Z3WWPParameterCategory = A3WWPParameterCategory;
            Z4WWPParameterDescription = A4WWPParameterDescription;
            Z2WWPParameterValue = A2WWPParameterValue;
            Z5WWPParameterDisableDelete = A5WWPParameterDisableDelete;
         }
      }

      protected void standaloneNotModal( )
      {
      }

      protected void standaloneModal( )
      {
      }

      protected void Load011( )
      {
         /* Using cursor BC00014 */
         pr_default.execute(2, new Object[] {A1WWPParameterKey});
         if ( (pr_default.getStatus(2) != 101) )
         {
            RcdFound1 = 1;
            A3WWPParameterCategory = BC00014_A3WWPParameterCategory[0];
            A4WWPParameterDescription = BC00014_A4WWPParameterDescription[0];
            A2WWPParameterValue = BC00014_A2WWPParameterValue[0];
            A5WWPParameterDisableDelete = BC00014_A5WWPParameterDisableDelete[0];
            ZM011( -3) ;
         }
         pr_default.close(2);
         OnLoadActions011( ) ;
      }

      protected void OnLoadActions011( )
      {
         if ( StringUtil.Len( A2WWPParameterValue) <= 30 )
         {
            A6WWPParameterValueTrimmed = A2WWPParameterValue;
         }
         else
         {
            A6WWPParameterValueTrimmed = StringUtil.Trim( StringUtil.Substring( A2WWPParameterValue, 1, 27)) + "...";
         }
      }

      protected void CheckExtendedTable011( )
      {
         standaloneModal( ) ;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( A1WWPParameterKey)) )
         {
            GX_msglist.addItem(StringUtil.Format( context.GetMessage( "WWP_RequiredAttribute", ""), context.GetMessage( "WWP_ParameterKey_Attribute_Description", ""), "", "", "", "", "", "", "", ""), 1, "");
            AnyError = 1;
         }
         if ( StringUtil.Len( A2WWPParameterValue) <= 30 )
         {
            A6WWPParameterValueTrimmed = A2WWPParameterValue;
         }
         else
         {
            A6WWPParameterValueTrimmed = StringUtil.Trim( StringUtil.Substring( A2WWPParameterValue, 1, 27)) + "...";
         }
      }

      protected void CloseExtendedTableCursors011( )
      {
      }

      protected void enableDisable( )
      {
      }

      protected void GetKey011( )
      {
         /* Using cursor BC00015 */
         pr_default.execute(3, new Object[] {A1WWPParameterKey});
         if ( (pr_default.getStatus(3) != 101) )
         {
            RcdFound1 = 1;
         }
         else
         {
            RcdFound1 = 0;
         }
         pr_default.close(3);
      }

      protected void getByPrimaryKey( )
      {
         /* Using cursor BC00013 */
         pr_default.execute(1, new Object[] {A1WWPParameterKey});
         if ( (pr_default.getStatus(1) != 101) )
         {
            ZM011( 3) ;
            RcdFound1 = 1;
            A1WWPParameterKey = BC00013_A1WWPParameterKey[0];
            A3WWPParameterCategory = BC00013_A3WWPParameterCategory[0];
            A4WWPParameterDescription = BC00013_A4WWPParameterDescription[0];
            A2WWPParameterValue = BC00013_A2WWPParameterValue[0];
            A5WWPParameterDisableDelete = BC00013_A5WWPParameterDisableDelete[0];
            Z1WWPParameterKey = A1WWPParameterKey;
            sMode1 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Load011( ) ;
            if ( AnyError == 1 )
            {
               RcdFound1 = 0;
               InitializeNonKey011( ) ;
            }
            Gx_mode = sMode1;
         }
         else
         {
            RcdFound1 = 0;
            InitializeNonKey011( ) ;
            sMode1 = Gx_mode;
            Gx_mode = "DSP";
            standaloneModal( ) ;
            Gx_mode = sMode1;
         }
         pr_default.close(1);
      }

      protected void getEqualNoModal( )
      {
         GetKey011( ) ;
         if ( RcdFound1 == 0 )
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
         CONFIRM_010( ) ;
      }

      protected void update_Check( )
      {
         insert_Check( ) ;
      }

      protected void delete_Check( )
      {
         insert_Check( ) ;
      }

      protected void CheckOptimisticConcurrency011( )
      {
         if ( ! IsIns( ) )
         {
            /* Using cursor BC00012 */
            pr_default.execute(0, new Object[] {A1WWPParameterKey});
            if ( (pr_default.getStatus(0) == 103) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"WWP_Parameter"}), "RecordIsLocked", 1, "");
               AnyError = 1;
               return  ;
            }
            if ( (pr_default.getStatus(0) == 101) || ( StringUtil.StrCmp(Z3WWPParameterCategory, BC00012_A3WWPParameterCategory[0]) != 0 ) || ( StringUtil.StrCmp(Z4WWPParameterDescription, BC00012_A4WWPParameterDescription[0]) != 0 ) || ( Z5WWPParameterDisableDelete != BC00012_A5WWPParameterDisableDelete[0] ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_waschg", new   object[]  {"WWP_Parameter"}), "RecordWasChanged", 1, "");
               AnyError = 1;
               return  ;
            }
         }
      }

      protected void Insert011( )
      {
         BeforeValidate011( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable011( ) ;
         }
         if ( AnyError == 0 )
         {
            ZM011( 0) ;
            CheckOptimisticConcurrency011( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm011( ) ;
               if ( AnyError == 0 )
               {
                  BeforeInsert011( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00016 */
                     pr_default.execute(4, new Object[] {A1WWPParameterKey, A3WWPParameterCategory, A4WWPParameterDescription, A2WWPParameterValue, A5WWPParameterDisableDelete});
                     pr_default.close(4);
                     pr_default.SmartCacheProvider.SetUpdated("WWP_Parameter");
                     if ( (pr_default.getStatus(4) == 1) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
                        AnyError = 1;
                     }
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
               Load011( ) ;
            }
            EndLevel011( ) ;
         }
         CloseExtendedTableCursors011( ) ;
      }

      protected void Update011( )
      {
         BeforeValidate011( ) ;
         if ( AnyError == 0 )
         {
            CheckExtendedTable011( ) ;
         }
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency011( ) ;
            if ( AnyError == 0 )
            {
               AfterConfirm011( ) ;
               if ( AnyError == 0 )
               {
                  BeforeUpdate011( ) ;
                  if ( AnyError == 0 )
                  {
                     /* Using cursor BC00017 */
                     pr_default.execute(5, new Object[] {A3WWPParameterCategory, A4WWPParameterDescription, A2WWPParameterValue, A5WWPParameterDisableDelete, A1WWPParameterKey});
                     pr_default.close(5);
                     pr_default.SmartCacheProvider.SetUpdated("WWP_Parameter");
                     if ( (pr_default.getStatus(5) == 103) )
                     {
                        GX_msglist.addItem(context.GetMessage( "GXM_lock", new   object[]  {"WWP_Parameter"}), "RecordIsLocked", 1, "");
                        AnyError = 1;
                     }
                     DeferredUpdate011( ) ;
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
            EndLevel011( ) ;
         }
         CloseExtendedTableCursors011( ) ;
      }

      protected void DeferredUpdate011( )
      {
      }

      protected void delete( )
      {
         Gx_mode = "DLT";
         BeforeValidate011( ) ;
         if ( AnyError == 0 )
         {
            CheckOptimisticConcurrency011( ) ;
         }
         if ( AnyError == 0 )
         {
            OnDeleteControls011( ) ;
            AfterConfirm011( ) ;
            if ( AnyError == 0 )
            {
               BeforeDelete011( ) ;
               if ( AnyError == 0 )
               {
                  /* No cascading delete specified. */
                  /* Using cursor BC00018 */
                  pr_default.execute(6, new Object[] {A1WWPParameterKey});
                  pr_default.close(6);
                  pr_default.SmartCacheProvider.SetUpdated("WWP_Parameter");
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
         sMode1 = Gx_mode;
         Gx_mode = "DLT";
         EndLevel011( ) ;
         Gx_mode = sMode1;
      }

      protected void OnDeleteControls011( )
      {
         standaloneModal( ) ;
         if ( AnyError == 0 )
         {
            /* Delete mode formulas */
            if ( StringUtil.Len( A2WWPParameterValue) <= 30 )
            {
               A6WWPParameterValueTrimmed = A2WWPParameterValue;
            }
            else
            {
               A6WWPParameterValueTrimmed = StringUtil.Trim( StringUtil.Substring( A2WWPParameterValue, 1, 27)) + "...";
            }
         }
      }

      protected void EndLevel011( )
      {
         if ( ! IsIns( ) )
         {
            pr_default.close(0);
         }
         if ( AnyError == 0 )
         {
            BeforeComplete011( ) ;
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

      public void ScanKeyStart011( )
      {
         /* Scan By routine */
         /* Using cursor BC00019 */
         pr_default.execute(7, new Object[] {A1WWPParameterKey});
         RcdFound1 = 0;
         if ( (pr_default.getStatus(7) != 101) )
         {
            RcdFound1 = 1;
            A1WWPParameterKey = BC00019_A1WWPParameterKey[0];
            A3WWPParameterCategory = BC00019_A3WWPParameterCategory[0];
            A4WWPParameterDescription = BC00019_A4WWPParameterDescription[0];
            A2WWPParameterValue = BC00019_A2WWPParameterValue[0];
            A5WWPParameterDisableDelete = BC00019_A5WWPParameterDisableDelete[0];
         }
         /* Load Subordinate Levels */
      }

      protected void ScanKeyNext011( )
      {
         /* Scan next routine */
         pr_default.readNext(7);
         RcdFound1 = 0;
         ScanKeyLoad011( ) ;
      }

      protected void ScanKeyLoad011( )
      {
         sMode1 = Gx_mode;
         Gx_mode = "DSP";
         if ( (pr_default.getStatus(7) != 101) )
         {
            RcdFound1 = 1;
            A1WWPParameterKey = BC00019_A1WWPParameterKey[0];
            A3WWPParameterCategory = BC00019_A3WWPParameterCategory[0];
            A4WWPParameterDescription = BC00019_A4WWPParameterDescription[0];
            A2WWPParameterValue = BC00019_A2WWPParameterValue[0];
            A5WWPParameterDisableDelete = BC00019_A5WWPParameterDisableDelete[0];
         }
         Gx_mode = sMode1;
      }

      protected void ScanKeyEnd011( )
      {
         pr_default.close(7);
      }

      protected void AfterConfirm011( )
      {
         /* After Confirm Rules */
      }

      protected void BeforeInsert011( )
      {
         /* Before Insert Rules */
      }

      protected void BeforeUpdate011( )
      {
         /* Before Update Rules */
      }

      protected void BeforeDelete011( )
      {
         /* Before Delete Rules */
      }

      protected void BeforeComplete011( )
      {
         /* Before Complete Rules */
      }

      protected void BeforeValidate011( )
      {
         /* Before Validate Rules */
      }

      protected void DisableAttributes011( )
      {
      }

      protected void send_integrity_lvl_hashes011( )
      {
      }

      protected void AddRow011( )
      {
         VarsToRow1( bcworkwithplus_WWP_Parameter) ;
      }

      protected void ReadRow011( )
      {
         RowToVars1( bcworkwithplus_WWP_Parameter, 1) ;
      }

      protected void InitializeNonKey011( )
      {
         A6WWPParameterValueTrimmed = "";
         A3WWPParameterCategory = "";
         A4WWPParameterDescription = "";
         A2WWPParameterValue = "";
         A5WWPParameterDisableDelete = false;
         Z3WWPParameterCategory = "";
         Z4WWPParameterDescription = "";
         Z5WWPParameterDisableDelete = false;
      }

      protected void InitAll011( )
      {
         A1WWPParameterKey = "";
         InitializeNonKey011( ) ;
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

      public void VarsToRow1( DesignSystem.Programs.workwithplus.SdtWWP_Parameter obj1 )
      {
         obj1.gxTpr_Mode = Gx_mode;
         obj1.gxTpr_Wwpparametervaluetrimmed = A6WWPParameterValueTrimmed;
         obj1.gxTpr_Wwpparametercategory = A3WWPParameterCategory;
         obj1.gxTpr_Wwpparameterdescription = A4WWPParameterDescription;
         obj1.gxTpr_Wwpparametervalue = A2WWPParameterValue;
         obj1.gxTpr_Wwpparameterdisabledelete = A5WWPParameterDisableDelete;
         obj1.gxTpr_Wwpparameterkey = A1WWPParameterKey;
         obj1.gxTpr_Wwpparameterkey_Z = Z1WWPParameterKey;
         obj1.gxTpr_Wwpparametercategory_Z = Z3WWPParameterCategory;
         obj1.gxTpr_Wwpparameterdescription_Z = Z4WWPParameterDescription;
         obj1.gxTpr_Wwpparametervaluetrimmed_Z = Z6WWPParameterValueTrimmed;
         obj1.gxTpr_Wwpparameterdisabledelete_Z = Z5WWPParameterDisableDelete;
         obj1.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void KeyVarsToRow1( DesignSystem.Programs.workwithplus.SdtWWP_Parameter obj1 )
      {
         obj1.gxTpr_Wwpparameterkey = A1WWPParameterKey;
         return  ;
      }

      public void RowToVars1( DesignSystem.Programs.workwithplus.SdtWWP_Parameter obj1 ,
                              int forceLoad )
      {
         Gx_mode = obj1.gxTpr_Mode;
         A6WWPParameterValueTrimmed = obj1.gxTpr_Wwpparametervaluetrimmed;
         A3WWPParameterCategory = obj1.gxTpr_Wwpparametercategory;
         A4WWPParameterDescription = obj1.gxTpr_Wwpparameterdescription;
         A2WWPParameterValue = obj1.gxTpr_Wwpparametervalue;
         A5WWPParameterDisableDelete = obj1.gxTpr_Wwpparameterdisabledelete;
         A1WWPParameterKey = obj1.gxTpr_Wwpparameterkey;
         Z1WWPParameterKey = obj1.gxTpr_Wwpparameterkey_Z;
         Z3WWPParameterCategory = obj1.gxTpr_Wwpparametercategory_Z;
         Z4WWPParameterDescription = obj1.gxTpr_Wwpparameterdescription_Z;
         Z6WWPParameterValueTrimmed = obj1.gxTpr_Wwpparametervaluetrimmed_Z;
         Z5WWPParameterDisableDelete = obj1.gxTpr_Wwpparameterdisabledelete_Z;
         Gx_mode = obj1.gxTpr_Mode;
         return  ;
      }

      public void LoadKey( Object[] obj )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         A1WWPParameterKey = (string)getParm(obj,0);
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         InitializeNonKey011( ) ;
         ScanKeyStart011( ) ;
         if ( RcdFound1 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z1WWPParameterKey = A1WWPParameterKey;
         }
         ZM011( -3) ;
         OnLoadActions011( ) ;
         AddRow011( ) ;
         ScanKeyEnd011( ) ;
         if ( RcdFound1 == 0 )
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
         RowToVars1( bcworkwithplus_WWP_Parameter, 0) ;
         ScanKeyStart011( ) ;
         if ( RcdFound1 == 0 )
         {
            Gx_mode = "INS";
         }
         else
         {
            Gx_mode = "UPD";
            Z1WWPParameterKey = A1WWPParameterKey;
         }
         ZM011( -3) ;
         OnLoadActions011( ) ;
         AddRow011( ) ;
         ScanKeyEnd011( ) ;
         if ( RcdFound1 == 0 )
         {
            GX_msglist.addItem(context.GetMessage( "GXM_keynfound", ""), "PrimaryKeyNotFound", 1, "");
            AnyError = 1;
         }
         context.GX_msglist = BackMsgLst;
      }

      protected void SaveImpl( )
      {
         GetKey011( ) ;
         if ( IsIns( ) )
         {
            /* Insert record */
            Insert011( ) ;
         }
         else
         {
            if ( RcdFound1 == 1 )
            {
               if ( StringUtil.StrCmp(A1WWPParameterKey, Z1WWPParameterKey) != 0 )
               {
                  A1WWPParameterKey = Z1WWPParameterKey;
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
                  Update011( ) ;
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
                  if ( StringUtil.StrCmp(A1WWPParameterKey, Z1WWPParameterKey) != 0 )
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
                        Insert011( ) ;
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
                        Insert011( ) ;
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
         RowToVars1( bcworkwithplus_WWP_Parameter, 1) ;
         SaveImpl( ) ;
         VarsToRow1( bcworkwithplus_WWP_Parameter) ;
         context.GX_msglist = BackMsgLst;
         return  ;
      }

      public bool Insert( )
      {
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         AnyError = 0;
         context.GX_msglist.removeAllItems();
         RowToVars1( bcworkwithplus_WWP_Parameter, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert011( ) ;
         AfterTrn( ) ;
         VarsToRow1( bcworkwithplus_WWP_Parameter) ;
         context.GX_msglist = BackMsgLst;
         return (AnyError==0) ;
      }

      protected void UpdateImpl( )
      {
         if ( IsUpd( ) )
         {
            SaveImpl( ) ;
            VarsToRow1( bcworkwithplus_WWP_Parameter) ;
         }
         else
         {
            DesignSystem.Programs.workwithplus.SdtWWP_Parameter auxBC = new DesignSystem.Programs.workwithplus.SdtWWP_Parameter(context);
            IGxSilentTrn auxTrn = auxBC.getTransaction();
            auxBC.Load(A1WWPParameterKey);
            if ( auxTrn.Errors() == 0 )
            {
               auxBC.UpdateDirties(bcworkwithplus_WWP_Parameter);
               auxBC.Save();
               bcworkwithplus_WWP_Parameter.Copy((GxSilentTrnSdt)(auxBC));
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
         RowToVars1( bcworkwithplus_WWP_Parameter, 1) ;
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
         RowToVars1( bcworkwithplus_WWP_Parameter, 1) ;
         Gx_mode = "INS";
         /* Insert record */
         Insert011( ) ;
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
               VarsToRow1( bcworkwithplus_WWP_Parameter) ;
            }
         }
         else
         {
            AfterTrn( ) ;
            VarsToRow1( bcworkwithplus_WWP_Parameter) ;
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
         RowToVars1( bcworkwithplus_WWP_Parameter, 0) ;
         GetKey011( ) ;
         if ( RcdFound1 == 1 )
         {
            if ( IsIns( ) )
            {
               GX_msglist.addItem(context.GetMessage( "GXM_noupdate", ""), "DuplicatePrimaryKey", 1, "");
               AnyError = 1;
            }
            else if ( StringUtil.StrCmp(A1WWPParameterKey, Z1WWPParameterKey) != 0 )
            {
               A1WWPParameterKey = Z1WWPParameterKey;
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
            if ( StringUtil.StrCmp(A1WWPParameterKey, Z1WWPParameterKey) != 0 )
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
         context.RollbackDataStores("workwithplus.wwp_parameter_bc",pr_default);
         VarsToRow1( bcworkwithplus_WWP_Parameter) ;
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
         Gx_mode = bcworkwithplus_WWP_Parameter.gxTpr_Mode;
         return Gx_mode ;
      }

      public void SetMode( string lMode )
      {
         Gx_mode = lMode;
         bcworkwithplus_WWP_Parameter.gxTpr_Mode = Gx_mode;
         return  ;
      }

      public void SetSDT( GxSilentTrnSdt sdt ,
                          short sdtToBc )
      {
         if ( sdt != bcworkwithplus_WWP_Parameter )
         {
            bcworkwithplus_WWP_Parameter = (DesignSystem.Programs.workwithplus.SdtWWP_Parameter)(sdt);
            if ( StringUtil.StrCmp(bcworkwithplus_WWP_Parameter.gxTpr_Mode, "") == 0 )
            {
               bcworkwithplus_WWP_Parameter.gxTpr_Mode = "INS";
            }
            if ( sdtToBc == 1 )
            {
               VarsToRow1( bcworkwithplus_WWP_Parameter) ;
            }
            else
            {
               RowToVars1( bcworkwithplus_WWP_Parameter, 1) ;
            }
         }
         else
         {
            if ( StringUtil.StrCmp(bcworkwithplus_WWP_Parameter.gxTpr_Mode, "") == 0 )
            {
               bcworkwithplus_WWP_Parameter.gxTpr_Mode = "INS";
            }
         }
         return  ;
      }

      public void ReloadFromSDT( )
      {
         RowToVars1( bcworkwithplus_WWP_Parameter, 1) ;
         return  ;
      }

      public void ForceCommitOnExit( )
      {
         return  ;
      }

      public SdtWWP_Parameter WWP_Parameter_BC
      {
         get {
            return bcworkwithplus_WWP_Parameter ;
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
            return "wwp_parameter_Execute" ;
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
         Z1WWPParameterKey = "";
         A1WWPParameterKey = "";
         AV8WWPContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPContext(context);
         AV9TrnContext = new DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext(context);
         AV10WebSession = context.GetSession();
         Z3WWPParameterCategory = "";
         A3WWPParameterCategory = "";
         Z4WWPParameterDescription = "";
         A4WWPParameterDescription = "";
         Z6WWPParameterValueTrimmed = "";
         A6WWPParameterValueTrimmed = "";
         Z2WWPParameterValue = "";
         A2WWPParameterValue = "";
         BC00014_A1WWPParameterKey = new string[] {""} ;
         BC00014_A3WWPParameterCategory = new string[] {""} ;
         BC00014_A4WWPParameterDescription = new string[] {""} ;
         BC00014_A2WWPParameterValue = new string[] {""} ;
         BC00014_A5WWPParameterDisableDelete = new bool[] {false} ;
         BC00015_A1WWPParameterKey = new string[] {""} ;
         BC00013_A1WWPParameterKey = new string[] {""} ;
         BC00013_A3WWPParameterCategory = new string[] {""} ;
         BC00013_A4WWPParameterDescription = new string[] {""} ;
         BC00013_A2WWPParameterValue = new string[] {""} ;
         BC00013_A5WWPParameterDisableDelete = new bool[] {false} ;
         sMode1 = "";
         BC00012_A1WWPParameterKey = new string[] {""} ;
         BC00012_A3WWPParameterCategory = new string[] {""} ;
         BC00012_A4WWPParameterDescription = new string[] {""} ;
         BC00012_A2WWPParameterValue = new string[] {""} ;
         BC00012_A5WWPParameterDisableDelete = new bool[] {false} ;
         BC00019_A1WWPParameterKey = new string[] {""} ;
         BC00019_A3WWPParameterCategory = new string[] {""} ;
         BC00019_A4WWPParameterDescription = new string[] {""} ;
         BC00019_A2WWPParameterValue = new string[] {""} ;
         BC00019_A5WWPParameterDisableDelete = new bool[] {false} ;
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         pr_gam = new DataStoreProvider(context, new DesignSystem.Programs.workwithplus.wwp_parameter_bc__gam(),
            new Object[][] {
            }
         );
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.workwithplus.wwp_parameter_bc__default(),
            new Object[][] {
                new Object[] {
               BC00012_A1WWPParameterKey, BC00012_A3WWPParameterCategory, BC00012_A4WWPParameterDescription, BC00012_A2WWPParameterValue, BC00012_A5WWPParameterDisableDelete
               }
               , new Object[] {
               BC00013_A1WWPParameterKey, BC00013_A3WWPParameterCategory, BC00013_A4WWPParameterDescription, BC00013_A2WWPParameterValue, BC00013_A5WWPParameterDisableDelete
               }
               , new Object[] {
               BC00014_A1WWPParameterKey, BC00014_A3WWPParameterCategory, BC00014_A4WWPParameterDescription, BC00014_A2WWPParameterValue, BC00014_A5WWPParameterDisableDelete
               }
               , new Object[] {
               BC00015_A1WWPParameterKey
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               }
               , new Object[] {
               BC00019_A1WWPParameterKey, BC00019_A3WWPParameterCategory, BC00019_A4WWPParameterDescription, BC00019_A2WWPParameterValue, BC00019_A5WWPParameterDisableDelete
               }
            }
         );
         INITTRN();
         /* Execute Start event if defined. */
         /* Execute user event: Start */
         E12012 ();
         standaloneNotModal( ) ;
      }

      private short AnyError ;
      private short RcdFound1 ;
      private int trnEnded ;
      private string Gx_mode ;
      private string endTrnMsgTxt ;
      private string endTrnMsgCod ;
      private string sMode1 ;
      private bool returnInSub ;
      private bool Z5WWPParameterDisableDelete ;
      private bool A5WWPParameterDisableDelete ;
      private string Z2WWPParameterValue ;
      private string A2WWPParameterValue ;
      private string Z1WWPParameterKey ;
      private string A1WWPParameterKey ;
      private string Z3WWPParameterCategory ;
      private string A3WWPParameterCategory ;
      private string Z4WWPParameterDescription ;
      private string A4WWPParameterDescription ;
      private string Z6WWPParameterValueTrimmed ;
      private string A6WWPParameterValueTrimmed ;
      private IGxSession AV10WebSession ;
      private IGxDataStore dsGAM ;
      private IGxDataStore dsDefault ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPContext AV8WWPContext ;
      private DesignSystem.Programs.wwpbaseobjects.SdtWWPTransactionContext AV9TrnContext ;
      private IDataStoreProvider pr_default ;
      private string[] BC00014_A1WWPParameterKey ;
      private string[] BC00014_A3WWPParameterCategory ;
      private string[] BC00014_A4WWPParameterDescription ;
      private string[] BC00014_A2WWPParameterValue ;
      private bool[] BC00014_A5WWPParameterDisableDelete ;
      private string[] BC00015_A1WWPParameterKey ;
      private string[] BC00013_A1WWPParameterKey ;
      private string[] BC00013_A3WWPParameterCategory ;
      private string[] BC00013_A4WWPParameterDescription ;
      private string[] BC00013_A2WWPParameterValue ;
      private bool[] BC00013_A5WWPParameterDisableDelete ;
      private string[] BC00012_A1WWPParameterKey ;
      private string[] BC00012_A3WWPParameterCategory ;
      private string[] BC00012_A4WWPParameterDescription ;
      private string[] BC00012_A2WWPParameterValue ;
      private bool[] BC00012_A5WWPParameterDisableDelete ;
      private string[] BC00019_A1WWPParameterKey ;
      private string[] BC00019_A3WWPParameterCategory ;
      private string[] BC00019_A4WWPParameterDescription ;
      private string[] BC00019_A2WWPParameterValue ;
      private bool[] BC00019_A5WWPParameterDisableDelete ;
      private DesignSystem.Programs.workwithplus.SdtWWP_Parameter bcworkwithplus_WWP_Parameter ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
      private IDataStoreProvider pr_gam ;
   }

   public class wwp_parameter_bc__gam : DataStoreHelperBase, IDataStoreHelper
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

 public class wwp_parameter_bc__default : DataStoreHelperBase, IDataStoreHelper
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
       ,new UpdateCursor(def[5])
       ,new UpdateCursor(def[6])
       ,new ForEachCursor(def[7])
     };
  }

  private static CursorDef[] def;
  private void cursorDefinitions( )
  {
     if ( def == null )
     {
        Object[] prmBC00012;
        prmBC00012 = new Object[] {
        new ParDef("@WWPParameterKey",GXType.Char,200,0)
        };
        Object[] prmBC00013;
        prmBC00013 = new Object[] {
        new ParDef("@WWPParameterKey",GXType.Char,200,0)
        };
        Object[] prmBC00014;
        prmBC00014 = new Object[] {
        new ParDef("@WWPParameterKey",GXType.Char,200,0)
        };
        Object[] prmBC00015;
        prmBC00015 = new Object[] {
        new ParDef("@WWPParameterKey",GXType.Char,200,0)
        };
        Object[] prmBC00016;
        prmBC00016 = new Object[] {
        new ParDef("@WWPParameterKey",GXType.Char,200,0) ,
        new ParDef("@WWPParameterCategory",GXType.Char,200,0) ,
        new ParDef("@WWPParameterDescription",GXType.Char,200,0) ,
        new ParDef("@WWPParameterValue",GXType.Char,2097152,0) ,
        new ParDef("@WWPParameterDisableDelete",GXType.Byte,4,0)
        };
        Object[] prmBC00017;
        prmBC00017 = new Object[] {
        new ParDef("@WWPParameterCategory",GXType.Char,200,0) ,
        new ParDef("@WWPParameterDescription",GXType.Char,200,0) ,
        new ParDef("@WWPParameterValue",GXType.Char,2097152,0) ,
        new ParDef("@WWPParameterDisableDelete",GXType.Byte,4,0) ,
        new ParDef("@WWPParameterKey",GXType.Char,200,0)
        };
        Object[] prmBC00018;
        prmBC00018 = new Object[] {
        new ParDef("@WWPParameterKey",GXType.Char,200,0)
        };
        Object[] prmBC00019;
        prmBC00019 = new Object[] {
        new ParDef("@WWPParameterKey",GXType.Char,200,0)
        };
        def= new CursorDef[] {
            new CursorDef("BC00012", "SELECT `WWPParameterKey`, `WWPParameterCategory`, `WWPParameterDescription`, `WWPParameterValue`, `WWPParameterDisableDelete` FROM `WWP_Parameter` WHERE `WWPParameterKey` = @WWPParameterKey  FOR UPDATE ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00012,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00013", "SELECT `WWPParameterKey`, `WWPParameterCategory`, `WWPParameterDescription`, `WWPParameterValue`, `WWPParameterDisableDelete` FROM `WWP_Parameter` WHERE `WWPParameterKey` = @WWPParameterKey ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00013,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00014", "SELECT TM1.`WWPParameterKey`, TM1.`WWPParameterCategory`, TM1.`WWPParameterDescription`, TM1.`WWPParameterValue`, TM1.`WWPParameterDisableDelete` FROM `WWP_Parameter` TM1 WHERE TM1.`WWPParameterKey` = @WWPParameterKey ORDER BY TM1.`WWPParameterKey` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00014,100, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00015", "SELECT `WWPParameterKey` FROM `WWP_Parameter` WHERE `WWPParameterKey` = @WWPParameterKey ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00015,1, GxCacheFrequency.OFF ,true,false )
           ,new CursorDef("BC00016", "INSERT INTO `WWP_Parameter`(`WWPParameterKey`, `WWPParameterCategory`, `WWPParameterDescription`, `WWPParameterValue`, `WWPParameterDisableDelete`) VALUES(@WWPParameterKey, @WWPParameterCategory, @WWPParameterDescription, @WWPParameterValue, @WWPParameterDisableDelete)", GxErrorMask.GX_NOMASK,prmBC00016)
           ,new CursorDef("BC00017", "UPDATE `WWP_Parameter` SET `WWPParameterCategory`=@WWPParameterCategory, `WWPParameterDescription`=@WWPParameterDescription, `WWPParameterValue`=@WWPParameterValue, `WWPParameterDisableDelete`=@WWPParameterDisableDelete  WHERE `WWPParameterKey` = @WWPParameterKey", GxErrorMask.GX_NOMASK,prmBC00017)
           ,new CursorDef("BC00018", "DELETE FROM `WWP_Parameter`  WHERE `WWPParameterKey` = @WWPParameterKey", GxErrorMask.GX_NOMASK,prmBC00018)
           ,new CursorDef("BC00019", "SELECT TM1.`WWPParameterKey`, TM1.`WWPParameterCategory`, TM1.`WWPParameterDescription`, TM1.`WWPParameterValue`, TM1.`WWPParameterDisableDelete` FROM `WWP_Parameter` TM1 WHERE TM1.`WWPParameterKey` = @WWPParameterKey ORDER BY TM1.`WWPParameterKey` ",true, GxErrorMask.GX_NOMASK, false, this,prmBC00019,100, GxCacheFrequency.OFF ,true,false )
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
              ((string[]) buf[0])[0] = rslt.getVarchar(1);
              ((string[]) buf[1])[0] = rslt.getVarchar(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
              ((bool[]) buf[4])[0] = rslt.getBool(5);
              return;
           case 1 :
              ((string[]) buf[0])[0] = rslt.getVarchar(1);
              ((string[]) buf[1])[0] = rslt.getVarchar(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
              ((bool[]) buf[4])[0] = rslt.getBool(5);
              return;
           case 2 :
              ((string[]) buf[0])[0] = rslt.getVarchar(1);
              ((string[]) buf[1])[0] = rslt.getVarchar(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
              ((bool[]) buf[4])[0] = rslt.getBool(5);
              return;
           case 3 :
              ((string[]) buf[0])[0] = rslt.getVarchar(1);
              return;
           case 7 :
              ((string[]) buf[0])[0] = rslt.getVarchar(1);
              ((string[]) buf[1])[0] = rslt.getVarchar(2);
              ((string[]) buf[2])[0] = rslt.getVarchar(3);
              ((string[]) buf[3])[0] = rslt.getLongVarchar(4);
              ((bool[]) buf[4])[0] = rslt.getBool(5);
              return;
     }
  }

}

}
