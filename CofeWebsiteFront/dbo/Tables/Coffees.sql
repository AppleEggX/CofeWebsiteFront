CREATE TABLE [dbo].[Coffees] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX)  NULL,
    [Origin]      NVARCHAR (MAX)  NULL,
    [Type]        NVARCHAR (MAX)  NULL,
    [Rating]      FLOAT (53)      NOT NULL,
    [TastingNote] NVARCHAR (MAX)  NULL,
    [Story]       NVARCHAR (MAX)  NULL,
    [Storage]     INT             NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_dbo.Coffees] PRIMARY KEY CLUSTERED ([Id] ASC)
);

