USE [master]
GO

CREATE DATABASE [DbPokemon];
GO

If not Exists (select loginname from master.dbo.syslogins where name = 'USPOKEMON')
BEGIN
	create LOGIN USPOKEMON with password = '@1PoKeTest1%';
END

use  [DbPokemon];

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PokemonId] [int] NOT NULL,
	[PokemonName] [varchar](100) NOT NULL,
	[Types] [varchar](100) NOT NULL,
	[Thumbnail] [varbinary](max) NOT NULL,
	[DateReg] [datetime] NOT NULL,
	[DateRem] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[DateReg] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [UserName], [Password], [DateReg]) VALUES (1, N'Ash', N'Ketchum', N'Ash', N'nsyVJdNSC2YaW38Eu/KGQG/bW6Tr/U2qdU2EHJJZgZPi8bfaUahQvRUTk3SMDnvhoFNElDR2HhgZi9Aqc+XhNTGw5FgT', CAST(N'2020-09-06T14:00:51.040' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [fk_favorites_user] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [fk_favorites_user]
GO
USE [master]
GO
ALTER DATABASE [DbPokemon] SET  READ_WRITE 
GO

use [DbPokemon];
GO

CREATE USER [PokemonUser] FOR LOGIN [USPOKEMON] WITH DEFAULT_SCHEMA=[dbo]
GO
grant connect to PokemonUser;

Grant select, insert, update, delete on Users to PokemonUser;
Grant select, insert, update, delete on Favorites to PokemonUser;