
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/16/2017 14:10:06
-- Generated from EDMX file: C:\Users\Victor-PC\Documents\Visual Studio 2015\Projects\KuznetsovVV_Counter\Server\Counter.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CountersDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CounterCounterIndication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CounterIndicationSet] DROP CONSTRAINT [FK_CounterCounterIndication];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountCounterIndication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CounterIndicationSet] DROP CONSTRAINT [FK_AccountCounterIndication];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CounterIndicationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CounterIndicationSet];
GO
IF OBJECT_ID(N'[dbo].[CounterSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CounterSet];
GO
IF OBJECT_ID(N'[dbo].[AccountSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CounterIndicationSet'
CREATE TABLE [dbo].[CounterIndicationSet] (
    [IndicationId] int IDENTITY(1,1) NOT NULL,
    [AccountKey] int  NOT NULL,
    [CounterKey] int  NOT NULL,
    [Measure] nvarchar(max)  NOT NULL,
    [Value] int  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'CounterSet'
CREATE TABLE [dbo].[CounterSet] (
    [CounterId] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AccountSet'
CREATE TABLE [dbo].[AccountSet] (
    [AccountId] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IndicationId] in table 'CounterIndicationSet'
ALTER TABLE [dbo].[CounterIndicationSet]
ADD CONSTRAINT [PK_CounterIndicationSet]
    PRIMARY KEY CLUSTERED ([IndicationId] ASC);
GO

-- Creating primary key on [CounterId] in table 'CounterSet'
ALTER TABLE [dbo].[CounterSet]
ADD CONSTRAINT [PK_CounterSet]
    PRIMARY KEY CLUSTERED ([CounterId] ASC);
GO

-- Creating primary key on [AccountId] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [PK_AccountSet]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AccountKey] in table 'CounterIndicationSet'
ALTER TABLE [dbo].[CounterIndicationSet]
ADD CONSTRAINT [FK_AccountCounterIndication]
    FOREIGN KEY ([AccountKey])
    REFERENCES [dbo].[AccountSet]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountCounterIndication'
CREATE INDEX [IX_FK_AccountCounterIndication]
ON [dbo].[CounterIndicationSet]
    ([AccountKey]);
GO

-- Creating foreign key on [CounterKey] in table 'CounterIndicationSet'
ALTER TABLE [dbo].[CounterIndicationSet]
ADD CONSTRAINT [FK_CounterCounterIndication]
    FOREIGN KEY ([CounterKey])
    REFERENCES [dbo].[CounterSet]
        ([CounterId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CounterCounterIndication'
CREATE INDEX [IX_FK_CounterCounterIndication]
ON [dbo].[CounterIndicationSet]
    ([CounterKey]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------