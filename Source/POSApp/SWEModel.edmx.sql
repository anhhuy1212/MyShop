
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/31/2020 13:12:23
-- Generated from EDMX file: C:\Users\hauzi\Downloads\Milestone 02 - Transaction Data\Source\POSApp\SWEModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyStore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Photo_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Photo] DROP CONSTRAINT [FK_Photo_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Purchase_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchase] DROP CONSTRAINT [FK_Purchase_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Purchase_PurchaseStatusEnum]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchase] DROP CONSTRAINT [FK_Purchase_PurchaseStatusEnum];
GO
IF OBJECT_ID(N'[dbo].[FK_PurchaseDetail_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseDetail] DROP CONSTRAINT [FK_PurchaseDetail_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_PurchaseDetail_Purchase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PurchaseDetail] DROP CONSTRAINT [FK_PurchaseDetail_Purchase];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Photo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Photo];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[Purchase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Purchase];
GO
IF OBJECT_ID(N'[dbo].[PurchaseDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseDetail];
GO
IF OBJECT_ID(N'[dbo].[PurchaseStatusEnum]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseStatusEnum];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [username] nvarchar(50)  NOT NULL,
    [rolename] nvarchar(50)  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Category_ID] int IDENTITY(1,1) NOT NULL,
    [Category_Name] nvarchar(max)  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Customer_Name] nvarchar(50)  NULL,
    [Tel] nvarchar(50)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL
);
GO

-- Creating table 'Photos'
CREATE TABLE [dbo].[Photos] (
    [Photo_id] int IDENTITY(1,1) NOT NULL,
    [Data] varbinary(max)  NULL,
    [Product_id] int  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Product_ID] int IDENTITY(1,1) NOT NULL,
    [Catgory_ID] int  NULL,
    [Product_Name] nvarchar(max)  NULL,
    [Quantity] int  NULL,
    [Price] int  NULL
);
GO

-- Creating table 'Purchases'
CREATE TABLE [dbo].[Purchases] (
    [Purchase_ID] int IDENTITY(1,1) NOT NULL,
    [Total] int  NULL,
    [Created_At] datetime  NULL,
    [Status] int  NULL,
    [Customer_Tel] nvarchar(50)  NULL
);
GO

-- Creating table 'PurchaseDetails'
CREATE TABLE [dbo].[PurchaseDetails] (
    [PurchaseDetail_ID] int IDENTITY(1,1) NOT NULL,
    [Purchase_ID] int  NULL,
    [Product_ID] int  NULL,
    [Price] int  NULL,
    [Quantity] int  NULL,
    [Total] int  NULL
);
GO

-- Creating table 'PurchaseStatusEnums'
CREATE TABLE [dbo].[PurchaseStatusEnums] (
    [EnumKey] nvarchar(50)  NOT NULL,
    [Value] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Display_Text] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [username] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([username] ASC);
GO

-- Creating primary key on [Category_ID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Category_ID] ASC);
GO

-- Creating primary key on [Tel] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Tel] ASC);
GO

-- Creating primary key on [Photo_id] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [PK_Photos]
    PRIMARY KEY CLUSTERED ([Photo_id] ASC);
GO

-- Creating primary key on [Product_ID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Product_ID] ASC);
GO

-- Creating primary key on [Purchase_ID] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [PK_Purchases]
    PRIMARY KEY CLUSTERED ([Purchase_ID] ASC);
GO

-- Creating primary key on [PurchaseDetail_ID] in table 'PurchaseDetails'
ALTER TABLE [dbo].[PurchaseDetails]
ADD CONSTRAINT [PK_PurchaseDetails]
    PRIMARY KEY CLUSTERED ([PurchaseDetail_ID] ASC);
GO

-- Creating primary key on [Value] in table 'PurchaseStatusEnums'
ALTER TABLE [dbo].[PurchaseStatusEnums]
ADD CONSTRAINT [PK_PurchaseStatusEnums]
    PRIMARY KEY CLUSTERED ([Value] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Catgory_ID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_Category]
    FOREIGN KEY ([Catgory_ID])
    REFERENCES [dbo].[Categories]
        ([Category_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Category'
CREATE INDEX [IX_FK_Product_Category]
ON [dbo].[Products]
    ([Catgory_ID]);
GO

-- Creating foreign key on [Customer_Tel] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_Purchase_Customer]
    FOREIGN KEY ([Customer_Tel])
    REFERENCES [dbo].[Customers]
        ([Tel])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Purchase_Customer'
CREATE INDEX [IX_FK_Purchase_Customer]
ON [dbo].[Purchases]
    ([Customer_Tel]);
GO

-- Creating foreign key on [Product_id] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [FK_Photo_Product]
    FOREIGN KEY ([Product_id])
    REFERENCES [dbo].[Products]
        ([Product_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Photo_Product'
CREATE INDEX [IX_FK_Photo_Product]
ON [dbo].[Photos]
    ([Product_id]);
GO

-- Creating foreign key on [Product_ID] in table 'PurchaseDetails'
ALTER TABLE [dbo].[PurchaseDetails]
ADD CONSTRAINT [FK_PurchaseDetail_Product]
    FOREIGN KEY ([Product_ID])
    REFERENCES [dbo].[Products]
        ([Product_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseDetail_Product'
CREATE INDEX [IX_FK_PurchaseDetail_Product]
ON [dbo].[PurchaseDetails]
    ([Product_ID]);
GO

-- Creating foreign key on [Status] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_Purchase_PurchaseStatusEnum]
    FOREIGN KEY ([Status])
    REFERENCES [dbo].[PurchaseStatusEnums]
        ([Value])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Purchase_PurchaseStatusEnum'
CREATE INDEX [IX_FK_Purchase_PurchaseStatusEnum]
ON [dbo].[Purchases]
    ([Status]);
GO

-- Creating foreign key on [Purchase_ID] in table 'PurchaseDetails'
ALTER TABLE [dbo].[PurchaseDetails]
ADD CONSTRAINT [FK_PurchaseDetail_Purchase]
    FOREIGN KEY ([Purchase_ID])
    REFERENCES [dbo].[Purchases]
        ([Purchase_ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseDetail_Purchase'
CREATE INDEX [IX_FK_PurchaseDetail_Purchase]
ON [dbo].[PurchaseDetails]
    ([Purchase_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------