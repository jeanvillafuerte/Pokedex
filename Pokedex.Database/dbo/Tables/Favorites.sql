CREATE TABLE [dbo].[Favorites] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [UserId]      INT           NOT NULL,
    [PokemonId]   INT           NOT NULL,
    [PokemonName] VARCHAR (100) NOT NULL,
    [Types]       VARCHAR (100) NOT NULL,
    [DateReg]     DATETIME      NOT NULL,
    [DateRem]     DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_favorites_user] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

