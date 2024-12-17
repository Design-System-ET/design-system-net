using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Reorg;
using System.Threading;
using DesignSystem.Programs;
using System.Data;
using GeneXus.Data;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
namespace DesignSystem.Programs {
   public class reorg : GXReorganization
   {
      public reorg( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("WorkWithPlusDS", false);
      }

      public reorg( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsGAM = context.GetDataStore("GAM");
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         initialize();
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         SetCreateDataBase( ) ;
         CreateDataBase( ) ;
         if ( PreviousCheck() )
         {
            ExecuteReorganization( ) ;
         }
      }

      private void CreateDataBase( )
      {
         DS = (GxDataStore)(context.GetDataStore( "Default"));
         ErrCode = DS.Connection.FullConnect();
         DataBaseName = DS.Connection.Database;
         if ( ErrCode != 0 )
         {
            DS.Connection.Database = "";
            ErrCode = DS.Connection.FullConnect();
            if ( ErrCode == 0 )
            {
               try
               {
                  GeneXus.Reorg.GXReorganization.AddMsg( GXResourceManager.GetMessage("GXM_dbcrea")+ " " +DataBaseName , null);
                  cmdBuffer = "CREATE DATABASE " + "`" + DataBaseName + "`";
                  RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                  RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
                  RGZ.ExecuteStmt() ;
                  RGZ.Drop();
                  Count = 1;
               }
               catch ( Exception ex )
               {
                  ErrCode = 1;
                  GeneXus.Reorg.GXReorganization.AddMsg( ex.Message , null);
                  throw;
               }
               ErrCode = DS.Connection.Disconnect();
               DS.Connection.Database = DataBaseName;
               ErrCode = DS.Connection.FullConnect();
               while ( ( ErrCode != 0 ) && ( Count > 0 ) && ( Count < 30 ) )
               {
                  Res = GXUtil.Sleep( 1);
                  ErrCode = DS.Connection.FullConnect();
                  Count = (short)(Count+1);
               }
            }
            if ( ErrCode != 0 )
            {
               ErrMsg = DS.ErrDescription;
               GeneXus.Reorg.GXReorganization.AddMsg( ErrMsg , null);
               ErrCode = 1;
               throw new Exception( ErrMsg) ;
            }
         }
      }

      private void FirstActions( )
      {
         /* Load data into tables. */
      }

      public void CreateConfiguracionEmpresa( )
      {
         string cmdBuffer = "";
         /* Indices for table ConfiguracionEmpresa */
         try
         {
            cmdBuffer=" CREATE TABLE `ConfiguracionEmpresa` (`ConfiguracionEmpresaId` smallint NOT NULL AUTO_INCREMENT, `ConfiguracionEmpresaTelefono` national char(20) NOT NULL , `ConfiguracionEmpresaCostoPlanB` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaPlanB` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCostoPlanS` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaPlanS` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCostoPlanN` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaPlanN` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCostoLandi` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaLandi` NUMERIC(11,2) NOT NULL , PRIMARY KEY(`ConfiguracionEmpresaId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            try
            {
               sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
               DropTableConstraints( "ConfiguracionEmpresa", sSchemaVar) ;
               cmdBuffer=" DROP TABLE `ConfiguracionEmpresa` "
               ;
               RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
               RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
               RGZ.ExecuteStmt() ;
               RGZ.Drop();
            }
            catch
            {
               try
               {
                  sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                  DropTableConstraints( "ConfiguracionEmpresa", sSchemaVar) ;
                  cmdBuffer=" DROP VIEW `ConfiguracionEmpresa` "
                  ;
                  RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                  RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
                  RGZ.ExecuteStmt() ;
                  RGZ.Drop();
               }
               catch
               {
                  try
                  {
                     sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                     DropTableConstraints( "ConfiguracionEmpresa", sSchemaVar) ;
                     cmdBuffer=" DROP FUNCTION `ConfiguracionEmpresa` "
                     ;
                     RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                     RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
                     RGZ.ExecuteStmt() ;
                     RGZ.Drop();
                  }
                  catch
                  {
                  }
               }
            }
            cmdBuffer=" CREATE TABLE `ConfiguracionEmpresa` (`ConfiguracionEmpresaId` smallint NOT NULL AUTO_INCREMENT, `ConfiguracionEmpresaTelefono` national char(20) NOT NULL , `ConfiguracionEmpresaCostoPlanB` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaPlanB` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCostoPlanS` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaPlanS` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCostoPlanN` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaPlanN` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCostoLandi` NUMERIC(11,2) NOT NULL , `ConfiguracionEmpresaCuotaLandi` NUMERIC(11,2) NOT NULL , PRIMARY KEY(`ConfiguracionEmpresaId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
      }

      public void CreateNewProductos( )
      {
         string cmdBuffer = "";
         /* Indices for table NewProductos */
         try
         {
            cmdBuffer=" CREATE TABLE `NewProductos` (`NewProductosId` smallint NOT NULL AUTO_INCREMENT, `NewProductosImagen` LONGBLOB NOT NULL , `NewProductosImagen_GXI` varchar(2048) , `NewProductosNombre` national varchar(200) NOT NULL , `NewProductosDescripcionCorta` national varchar(200) NOT NULL , `NewProductosDescripcion` MEDIUMTEXT CHARACTER SET utf8 NOT NULL , `NewProductosNumeroDescargas` smallint NOT NULL , `NewProductosLinkDescargaDemo` national varchar(1000) NOT NULL , `NewProductosComprar` national varchar(1000) NOT NULL , `NewProductosNumeroVentas` smallint NOT NULL , `CategoriasId` smallint NOT NULL , `NewProductosVisitas` smallint NOT NULL , PRIMARY KEY(`NewProductosId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            try
            {
               sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
               DropTableConstraints( "NewProductos", sSchemaVar) ;
               cmdBuffer=" DROP TABLE `NewProductos` "
               ;
               RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
               RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
               RGZ.ExecuteStmt() ;
               RGZ.Drop();
            }
            catch
            {
               try
               {
                  sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                  DropTableConstraints( "NewProductos", sSchemaVar) ;
                  cmdBuffer=" DROP VIEW `NewProductos` "
                  ;
                  RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                  RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
                  RGZ.ExecuteStmt() ;
                  RGZ.Drop();
               }
               catch
               {
                  try
                  {
                     sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                     DropTableConstraints( "NewProductos", sSchemaVar) ;
                     cmdBuffer=" DROP FUNCTION `NewProductos` "
                     ;
                     RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                     RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
                     RGZ.ExecuteStmt() ;
                     RGZ.Drop();
                  }
                  catch
                  {
                  }
               }
            }
            cmdBuffer=" CREATE TABLE `NewProductos` (`NewProductosId` smallint NOT NULL AUTO_INCREMENT, `NewProductosImagen` LONGBLOB NOT NULL , `NewProductosImagen_GXI` varchar(2048) , `NewProductosNombre` national varchar(200) NOT NULL , `NewProductosDescripcionCorta` national varchar(200) NOT NULL , `NewProductosDescripcion` MEDIUMTEXT CHARACTER SET utf8 NOT NULL , `NewProductosNumeroDescargas` smallint NOT NULL , `NewProductosLinkDescargaDemo` national varchar(1000) NOT NULL , `NewProductosComprar` national varchar(1000) NOT NULL , `NewProductosNumeroVentas` smallint NOT NULL , `CategoriasId` smallint NOT NULL , `NewProductosVisitas` smallint NOT NULL , PRIMARY KEY(`NewProductosId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         try
         {
            cmdBuffer=" CREATE INDEX `INEWPRODUCTOS1` ON `NewProductos` (`CategoriasId` ) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            cmdBuffer=" DROP INDEX `INEWPRODUCTOS1` ON `NewProductos` "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
            cmdBuffer=" CREATE INDEX `INEWPRODUCTOS1` ON `NewProductos` (`CategoriasId` ) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
      }

      public void CreateCategorias( )
      {
         string cmdBuffer = "";
         /* Indices for table Categorias */
         try
         {
            cmdBuffer=" CREATE TABLE `Categorias` (`CategoriasId` smallint NOT NULL AUTO_INCREMENT, `CategoriasCategoria` national char(100) NOT NULL , PRIMARY KEY(`CategoriasId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            try
            {
               sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
               DropTableConstraints( "Categorias", sSchemaVar) ;
               cmdBuffer=" DROP TABLE `Categorias` "
               ;
               RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
               RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
               RGZ.ExecuteStmt() ;
               RGZ.Drop();
            }
            catch
            {
               try
               {
                  sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                  DropTableConstraints( "Categorias", sSchemaVar) ;
                  cmdBuffer=" DROP VIEW `Categorias` "
                  ;
                  RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                  RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
                  RGZ.ExecuteStmt() ;
                  RGZ.Drop();
               }
               catch
               {
                  try
                  {
                     sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                     DropTableConstraints( "Categorias", sSchemaVar) ;
                     cmdBuffer=" DROP FUNCTION `Categorias` "
                     ;
                     RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                     RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
                     RGZ.ExecuteStmt() ;
                     RGZ.Drop();
                  }
                  catch
                  {
                  }
               }
            }
            cmdBuffer=" CREATE TABLE `Categorias` (`CategoriasId` smallint NOT NULL AUTO_INCREMENT, `CategoriasCategoria` national char(100) NOT NULL , PRIMARY KEY(`CategoriasId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
      }

      public void CreateNewBlog( )
      {
         string cmdBuffer = "";
         /* Indices for table NewBlog */
         try
         {
            cmdBuffer=" CREATE TABLE `NewBlog` (`NewBlogId` smallint NOT NULL AUTO_INCREMENT, `NewBlogImagen` LONGBLOB NOT NULL , `NewBlogImagen_GXI` varchar(2048) , `NewBlogTitulo` national varchar(200) NOT NULL , `NewBlogSubTitulo` national varchar(200) NOT NULL , `NewBlogDescripcion` MEDIUMTEXT CHARACTER SET utf8 NOT NULL , `NewBlogVisitas` smallint NOT NULL , `NewBlogDestacado` BOOL NOT NULL , `CategoriasId` smallint NOT NULL , `NewBlogBorrador` BOOL NOT NULL , `NewBlogDescripcionCorta` national varchar(500) NOT NULL , PRIMARY KEY(`NewBlogId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            try
            {
               sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
               DropTableConstraints( "NewBlog", sSchemaVar) ;
               cmdBuffer=" DROP TABLE `NewBlog` "
               ;
               RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
               RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
               RGZ.ExecuteStmt() ;
               RGZ.Drop();
            }
            catch
            {
               try
               {
                  sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                  DropTableConstraints( "NewBlog", sSchemaVar) ;
                  cmdBuffer=" DROP VIEW `NewBlog` "
                  ;
                  RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                  RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
                  RGZ.ExecuteStmt() ;
                  RGZ.Drop();
               }
               catch
               {
                  try
                  {
                     sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                     DropTableConstraints( "NewBlog", sSchemaVar) ;
                     cmdBuffer=" DROP FUNCTION `NewBlog` "
                     ;
                     RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                     RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
                     RGZ.ExecuteStmt() ;
                     RGZ.Drop();
                  }
                  catch
                  {
                  }
               }
            }
            cmdBuffer=" CREATE TABLE `NewBlog` (`NewBlogId` smallint NOT NULL AUTO_INCREMENT, `NewBlogImagen` LONGBLOB NOT NULL , `NewBlogImagen_GXI` varchar(2048) , `NewBlogTitulo` national varchar(200) NOT NULL , `NewBlogSubTitulo` national varchar(200) NOT NULL , `NewBlogDescripcion` MEDIUMTEXT CHARACTER SET utf8 NOT NULL , `NewBlogVisitas` smallint NOT NULL , `NewBlogDestacado` BOOL NOT NULL , `CategoriasId` smallint NOT NULL , `NewBlogBorrador` BOOL NOT NULL , `NewBlogDescripcionCorta` national varchar(500) NOT NULL , PRIMARY KEY(`NewBlogId`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         try
         {
            cmdBuffer=" CREATE INDEX `INEWBLOG1` ON `NewBlog` (`CategoriasId` ) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            cmdBuffer=" DROP INDEX `INEWBLOG1` ON `NewBlog` "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
            cmdBuffer=" CREATE INDEX `INEWBLOG1` ON `NewBlog` (`CategoriasId` ) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
      }

      public void CreateWWP_Parameter( )
      {
         string cmdBuffer = "";
         /* Indices for table WWP_Parameter */
         try
         {
            cmdBuffer=" CREATE TABLE `WWP_Parameter` (`WWPParameterKey` national varchar(200) NOT NULL , `WWPParameterCategory` national varchar(200) NOT NULL , `WWPParameterDescription` national varchar(200) NOT NULL , `WWPParameterValue` MEDIUMTEXT CHARACTER SET utf8 NOT NULL , `WWPParameterDisableDelete` BOOL NOT NULL , PRIMARY KEY(`WWPParameterKey`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            try
            {
               sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
               DropTableConstraints( "WWP_Parameter", sSchemaVar) ;
               cmdBuffer=" DROP TABLE `WWP_Parameter` "
               ;
               RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
               RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
               RGZ.ExecuteStmt() ;
               RGZ.Drop();
            }
            catch
            {
               try
               {
                  sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                  DropTableConstraints( "WWP_Parameter", sSchemaVar) ;
                  cmdBuffer=" DROP VIEW `WWP_Parameter` "
                  ;
                  RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                  RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
                  RGZ.ExecuteStmt() ;
                  RGZ.Drop();
               }
               catch
               {
                  try
                  {
                     sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
                     DropTableConstraints( "WWP_Parameter", sSchemaVar) ;
                     cmdBuffer=" DROP FUNCTION `WWP_Parameter` "
                     ;
                     RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
                     RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
                     RGZ.ExecuteStmt() ;
                     RGZ.Drop();
                  }
                  catch
                  {
                  }
               }
            }
            cmdBuffer=" CREATE TABLE `WWP_Parameter` (`WWPParameterKey` national varchar(200) NOT NULL , `WWPParameterCategory` national varchar(200) NOT NULL , `WWPParameterDescription` national varchar(200) NOT NULL , `WWPParameterValue` MEDIUMTEXT CHARACTER SET utf8 NOT NULL , `WWPParameterDisableDelete` BOOL NOT NULL , PRIMARY KEY(`WWPParameterKey`))  ENGINE=InnoDB "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
      }

      public void RINewBlogCategorias( )
      {
         string cmdBuffer;
         try
         {
            cmdBuffer=" ALTER TABLE `NewBlog` ADD CONSTRAINT `INEWBLOG1` FOREIGN KEY (`CategoriasId`) REFERENCES `Categorias` (`CategoriasId`) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            try
            {
               cmdBuffer=" ALTER TABLE `NewBlog` DROP FOREIGN KEY `INEWBLOG1` "
               ;
               RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
               RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
               RGZ.ExecuteStmt() ;
               RGZ.Drop();
            }
            catch
            {
            }
            cmdBuffer=" ALTER TABLE `NewBlog` ADD CONSTRAINT `INEWBLOG1` FOREIGN KEY (`CategoriasId`) REFERENCES `Categorias` (`CategoriasId`) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
      }

      public void RINewProductosCategorias( )
      {
         string cmdBuffer;
         try
         {
            cmdBuffer=" ALTER TABLE `NewProductos` ADD CONSTRAINT `INEWPRODUCTOS1` FOREIGN KEY (`CategoriasId`) REFERENCES `Categorias` (`CategoriasId`) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
         catch
         {
            try
            {
               cmdBuffer=" ALTER TABLE `NewProductos` DROP FOREIGN KEY `INEWPRODUCTOS1` "
               ;
               RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
               RGZ.ErrorMask = GxErrorMask.GX_MASKNOTFOUND | GxErrorMask.GX_MASKLOOPLOCK;
               RGZ.ExecuteStmt() ;
               RGZ.Drop();
            }
            catch
            {
            }
            cmdBuffer=" ALTER TABLE `NewProductos` ADD CONSTRAINT `INEWPRODUCTOS1` FOREIGN KEY (`CategoriasId`) REFERENCES `Categorias` (`CategoriasId`) "
            ;
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
         }
      }

      private void TablesCount( )
      {
      }

      private bool PreviousCheck( )
      {
         if ( ! IsResumeMode( ) )
         {
            if ( GXUtil.DbmsVersion( context, "DEFAULT") < 5 )
            {
               SetCheckError ( GXResourceManager.GetMessage("GXM_bad_DBMS_version", new   object[]  {"5"}) ) ;
               return false ;
            }
         }
         if ( ! MustRunCheck( ) )
         {
            return true ;
         }
         sSchemaVar = GXUtil.DataBaseName( context, "DEFAULT");
         return true ;
      }

      private void ExecuteOnlyTablesReorganization( )
      {
         ReorgExecute.RegisterBlockForSubmit( 1 ,  "CreateConfiguracionEmpresa" , new Object[]{ });
         ReorgExecute.RegisterBlockForSubmit( 2 ,  "CreateNewProductos" , new Object[]{ });
         ReorgExecute.RegisterBlockForSubmit( 3 ,  "CreateCategorias" , new Object[]{ });
         ReorgExecute.RegisterBlockForSubmit( 4 ,  "CreateNewBlog" , new Object[]{ });
         ReorgExecute.RegisterBlockForSubmit( 5 ,  "CreateWWP_Parameter" , new Object[]{ });
      }

      private void ExecuteOnlyRisReorganization( )
      {
         ReorgExecute.RegisterBlockForSubmit( 6 ,  "RINewBlogCategorias" , new Object[]{ });
         ReorgExecute.RegisterBlockForSubmit( 7 ,  "RINewProductosCategorias" , new Object[]{ });
      }

      private void ExecuteTablesReorganization( )
      {
         ExecuteOnlyTablesReorganization( ) ;
         ExecuteOnlyRisReorganization( ) ;
         ReorgExecute.SubmitAll() ;
      }

      private void SetPrecedence( )
      {
         SetPrecedencetables( ) ;
         SetPrecedenceris( ) ;
      }

      private void SetPrecedencetables( )
      {
         GXReorganization.SetMsg( 1 ,  GXResourceManager.GetMessage("GXM_filecrea", new   object[]  {"ConfiguracionEmpresa", ""}) );
         GXReorganization.SetMsg( 2 ,  GXResourceManager.GetMessage("GXM_filecrea", new   object[]  {"NewProductos", ""}) );
         ReorgExecute.RegisterPrecedence( "CreateNewProductos" ,  "CreateCategorias" );
         GXReorganization.SetMsg( 3 ,  GXResourceManager.GetMessage("GXM_filecrea", new   object[]  {"Categorias", ""}) );
         GXReorganization.SetMsg( 4 ,  GXResourceManager.GetMessage("GXM_filecrea", new   object[]  {"NewBlog", ""}) );
         ReorgExecute.RegisterPrecedence( "CreateNewBlog" ,  "CreateCategorias" );
         GXReorganization.SetMsg( 5 ,  GXResourceManager.GetMessage("GXM_filecrea", new   object[]  {"WWP_Parameter", ""}) );
      }

      private void SetPrecedenceris( )
      {
         GXReorganization.SetMsg( 6 ,  GXResourceManager.GetMessage("GXM_refintcrea", new   object[]  {"`INEWBLOG1`"}) );
         ReorgExecute.RegisterPrecedence( "RINewBlogCategorias" ,  "CreateNewBlog" );
         ReorgExecute.RegisterPrecedence( "RINewBlogCategorias" ,  "CreateCategorias" );
         GXReorganization.SetMsg( 7 ,  GXResourceManager.GetMessage("GXM_refintcrea", new   object[]  {"`INEWPRODUCTOS1`"}) );
         ReorgExecute.RegisterPrecedence( "RINewProductosCategorias" ,  "CreateNewProductos" );
         ReorgExecute.RegisterPrecedence( "RINewProductosCategorias" ,  "CreateCategorias" );
      }

      private void ExecuteReorganization( )
      {
         if ( ErrCode == 0 )
         {
            TablesCount( ) ;
            if ( ! PrintOnlyRecordCount( ) )
            {
               FirstActions( ) ;
               SetPrecedence( ) ;
               ExecuteTablesReorganization( ) ;
            }
         }
      }

      public void DropTableConstraints( string sTableName ,
                                        string sMySchemaName )
      {
         string cmdBuffer;
         /* Using cursor P00012 */
         pr_default.execute(0, new Object[] {sTableName, sMySchemaName});
         while ( (pr_default.getStatus(0) != 101) )
         {
            tablename = P00012_Atablename[0];
            ntablename = P00012_ntablename[0];
            referencedtablename = P00012_Areferencedtablename[0];
            nreferencedtablename = P00012_nreferencedtablename[0];
            constid = P00012_Aconstid[0];
            nconstid = P00012_nconstid[0];
            schemaname = P00012_Aschemaname[0];
            nschemaname = P00012_nschemaname[0];
            cmdBuffer = "ALTER TABLE " + "" + tablename + "" + " DROP FOREIGN KEY " + "" + constid + "";
            RGZ = new GxCommand(dsDefault.Db, cmdBuffer, dsDefault,0,true,false,null);
            RGZ.ErrorMask = GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK;
            RGZ.ExecuteStmt() ;
            RGZ.Drop();
            pr_default.readNext(0);
         }
         pr_default.close(0);
      }

      public void UtilsCleanup( )
      {
         cleanup();
      }

      public override void cleanup( )
      {
         CloseCursors();
      }

      public override void initialize( )
      {
         DS = new GxDataStore();
         ErrMsg = "";
         DataBaseName = "";
         sSchemaVar = "";
         sTableName = "";
         sMySchemaName = "";
         tablename = "";
         ntablename = false;
         referencedtablename = "";
         nreferencedtablename = false;
         constid = "";
         nconstid = false;
         schemaname = "";
         nschemaname = false;
         P00012_Atablename = new string[] {""} ;
         P00012_ntablename = new bool[] {false} ;
         P00012_Areferencedtablename = new string[] {""} ;
         P00012_nreferencedtablename = new bool[] {false} ;
         P00012_Aconstid = new string[] {""} ;
         P00012_nconstid = new bool[] {false} ;
         P00012_Aschemaname = new string[] {""} ;
         P00012_nschemaname = new bool[] {false} ;
         pr_default = new DataStoreProvider(context, new DesignSystem.Programs.reorg__default(),
            new Object[][] {
                new Object[] {
               P00012_Atablename, P00012_Areferencedtablename, P00012_Aconstid, P00012_Aschemaname
               }
            }
         );
         /* GeneXus formulas. */
      }

      protected short ErrCode ;
      protected short Count ;
      protected short Res ;
      protected string ErrMsg ;
      protected string DataBaseName ;
      protected string cmdBuffer ;
      protected string sSchemaVar ;
      protected string sTableName ;
      protected string sMySchemaName ;
      protected bool ntablename ;
      protected bool nreferencedtablename ;
      protected bool nconstid ;
      protected bool nschemaname ;
      protected string tablename ;
      protected string referencedtablename ;
      protected string constid ;
      protected string schemaname ;
      protected GxDataStore DS ;
      protected IGxDataStore dsGAM ;
      protected IGxDataStore dsDefault ;
      protected GxCommand RGZ ;
      protected IDataStoreProvider pr_default ;
      protected string[] P00012_Atablename ;
      protected bool[] P00012_ntablename ;
      protected string[] P00012_Areferencedtablename ;
      protected bool[] P00012_nreferencedtablename ;
      protected string[] P00012_Aconstid ;
      protected bool[] P00012_nconstid ;
      protected string[] P00012_Aschemaname ;
      protected bool[] P00012_nschemaname ;
   }

   public class reorg__default : DataStoreHelperBase, IDataStoreHelper
   {
      public ICursor[] getCursors( )
      {
         cursorDefinitions();
         return new Cursor[] {
          new ForEachCursor(def[0])
       };
    }

    private static CursorDef[] def;
    private void cursorDefinitions( )
    {
       if ( def == null )
       {
          Object[] prmP00012;
          prmP00012 = new Object[] {
          new ParDef("@sTableName",GXType.Char,255,0) ,
          new ParDef("@sMySchemaName",GXType.Char,255,0)
          };
          def= new CursorDef[] {
              new CursorDef("P00012", "SELECT DISTINCT TABLE_NAME, REFERENCED_TABLE_NAME, CONSTRAINT_NAME, TABLE_SCHEMA FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE (REFERENCED_TABLE_NAME = @sTableName) AND (TABLE_SCHEMA = @sMySchemaName) ",false, GxErrorMask.GX_NOMASK | GxErrorMask.GX_MASKLOOPLOCK, false, this,prmP00012,100, GxCacheFrequency.OFF ,true,false )
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
                ((string[]) buf[3])[0] = rslt.getVarchar(4);
                return;
       }
    }

 }

}
