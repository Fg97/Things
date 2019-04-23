
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/30/2017 10:57:29
-- Generated from EDMX file: C:\Users\Victor-PC\Documents\Visual Studio 2015\Projects\DI\DI\DIC.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DI];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[communication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[communication];
GO
IF OBJECT_ID(N'[dbo].[companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[companies];
GO
IF OBJECT_ID(N'[dbo].[fields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[fields];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'communication'
CREATE TABLE [dbo].[communication] (
    [id] int IDENTITY(1,1) NOT NULL,
    [field_id] int  NOT NULL,
    [value] varchar(50)  NOT NULL,
    [company_id] int  NOT NULL
);
GO

-- Creating table 'companies'
CREATE TABLE [dbo].[companies] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(max)  NOT NULL,
    [create_date] int  NULL
);
GO

-- Creating table 'fields'
CREATE TABLE [dbo].[fields] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NULL,
    [description] varchar(50)  NULL,
    [measure] varchar(50)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'communication'
ALTER TABLE [dbo].[communication]
ADD CONSTRAINT [PK_communication]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'companies'
ALTER TABLE [dbo].[companies]
ADD CONSTRAINT [PK_companies]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'fields'
ALTER TABLE [dbo].[fields]
ADD CONSTRAINT [PK_fields]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------