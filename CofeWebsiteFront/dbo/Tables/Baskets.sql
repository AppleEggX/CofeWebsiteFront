CREATE TABLE [dbo].[Baskets] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [SumPrice] DECIMAL (18, 2) NOT NULL,
    [User_Id]  INT             NULL,
    [Paied]    BIT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Baskets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Baskets_dbo.Users_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[Baskets]([User_Id] ASC);

