CREATE TABLE [dbo].[Favorites] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [UserId]      INT             NOT NULL,
    [PokemonId]   INT             NOT NULL,
    [PokemonName] VARCHAR (100)   NOT NULL,
    [Types]       VARCHAR (100)   NOT NULL,
    [Thumbnail]   VARBINARY (MAX) NOT NULL,
    [DateReg]     DATETIME        NOT NULL,
    [DateRem]     DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_favorites_user] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Favorites] TO [PokemonUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[Favorites] TO [PokemonUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[Favorites] TO [PokemonUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[Favorites] TO [PokemonUser]
    AS [dbo];

