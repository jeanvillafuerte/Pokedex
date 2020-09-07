CREATE TABLE [dbo].[Users] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (100) NOT NULL,
    [LastName]  VARCHAR (100) NOT NULL,
    [UserName]  VARCHAR (100) NOT NULL,
    [Password]  VARCHAR (100) NOT NULL,
    [DateReg]   DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

