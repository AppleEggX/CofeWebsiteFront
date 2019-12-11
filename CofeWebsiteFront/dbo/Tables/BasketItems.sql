CREATE TABLE [dbo].[BasketItems] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [Amount]     INT             NOT NULL,
    [TotalPrice] DECIMAL (18, 2) NOT NULL,
    [Basket_Id]  INT             NULL,
    [Coffee_Id]  INT             NULL,
    CONSTRAINT [PK_dbo.BasketItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.BasketItems_dbo.Baskets_Basket_Id] FOREIGN KEY ([Basket_Id]) REFERENCES [dbo].[Baskets] ([Id]),
    CONSTRAINT [FK_dbo.BasketItems_dbo.Coffees_Coffee_Id] FOREIGN KEY ([Coffee_Id]) REFERENCES [dbo].[Coffees] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Basket_Id]
    ON [dbo].[BasketItems]([Basket_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Coffee_Id]
    ON [dbo].[BasketItems]([Coffee_Id] ASC);

