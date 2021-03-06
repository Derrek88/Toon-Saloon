USE [master]
GO
/****** Object:  Database [ToonSaloon]    Script Date: 12/7/2016 10:21:16 PM ******/
CREATE DATABASE [ToonSaloon]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToonSaloon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\ToonSaloon.mdf' , SIZE = 4500KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ToonSaloon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\ToonSaloon_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ToonSaloon] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToonSaloon].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ToonSaloon] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ToonSaloon] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ToonSaloon] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ToonSaloon] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ToonSaloon] SET ARITHABORT OFF 
GO
ALTER DATABASE [ToonSaloon] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ToonSaloon] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ToonSaloon] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ToonSaloon] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ToonSaloon] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ToonSaloon] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ToonSaloon] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ToonSaloon] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ToonSaloon] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ToonSaloon] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ToonSaloon] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ToonSaloon] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ToonSaloon] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ToonSaloon] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ToonSaloon] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ToonSaloon] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ToonSaloon] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ToonSaloon] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ToonSaloon] SET  MULTI_USER 
GO
ALTER DATABASE [ToonSaloon] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ToonSaloon] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ToonSaloon] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ToonSaloon] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ToonSaloon] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ToonSaloon]
GO
/****** Object:  Table [dbo].[BlogPost]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BlogPost](
	[Body] [varchar](max) NOT NULL,
	[AuthorName] [varchar](max) NOT NULL,
	[Category] [int] NOT NULL,
	[Approved] [int] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[Headline] [varchar](max) NOT NULL,
	[Subtitle] [varchar](max) NOT NULL,
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BlogPost] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CartoonOfTheDay]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CartoonOfTheDay](
	[Author] [varchar](max) NOT NULL,
	[Approved] [int] NOT NULL,
	[ShowName] [varchar](max) NOT NULL,
	[Season] [int] NOT NULL,
	[Episode] [int] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[ImgUrl] [varchar](max) NULL,
	[CotDId] [int] IDENTITY(1,1) NOT NULL,
	[HasNotBeenPosted] [bit] NOT NULL,
	[WhenPosted] [date] NOT NULL,
 CONSTRAINT [PK_CartoonOfTheDay] PRIMARY KEY CLUSTERED 
(
	[CotDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Eighties] [nvarchar](50) NULL,
	[Nineties] [nvarchar](50) NULL,
	[Twothousands] [nvarchar](50) NULL,
	[LateNight] [nvarchar](50) NULL,
	[Childrens] [nvarchar](50) NULL,
	[Strange] [nvarchar](50) NULL,
	[OldSchool] [nvarchar](50) NULL,
	[Anime] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Img]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Img](
	[Title] [varchar](max) NULL,
	[Source] [varchar](max) NOT NULL,
	[Description] [varchar](max) NULL,
	[ImgId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Img] PRIMARY KEY CLUSTERED 
(
	[ImgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Img_BlogBridge]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Img_BlogBridge](
	[BlogId] [int] NOT NULL,
	[ImgId] [int] NOT NULL,
 CONSTRAINT [PK_Img_BlogBridge] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC,
	[ImgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page_TagBridge]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page_TagBridge](
	[StaticId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_Page_TagBridge] PRIMARY KEY CLUSTERED 
(
	[StaticId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StaticPage]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StaticPage](
	[Name] [varchar](max) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[DateCreated] [date] NOT NULL,
	[Approved] [int] NOT NULL,
	[StaticId] [int] IDENTITY(1,1) NOT NULL,
	[Category] [int] NULL,
 CONSTRAINT [PK_StaticPage] PRIMARY KEY CLUSTERED 
(
	[StaticId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tag](
	[Name] [varchar](max) NOT NULL,
	[TagId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tag_BlogBridge]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag_BlogBridge](
	[BlogId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_Tag_BlogBridge] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[AddImage]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddImage]
(
@Title varchar(MAX),
@Source varchar(MAX),
@Description varchar(MAX),
@ImgId int output

)AS

INSERT INTO Img(Title,Source,[Description])
VALUES(@Title,@Source,@Description)
SET @ImgId = SCOPE_IDENTITY();
GO
/****** Object:  StoredProcedure [dbo].[AddPage]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPage]
(
@Body varchar(MAX),
@DateCreated date,
@Approved int,
@Name varchar(MAX),
@Category int,
@StaticId int output

)AS

INSERT INTO StaticPage(Body,DateCreated,Approved,Name,Category)
VALUES (@Body,@DateCreated,@Approved,@Name,@Category)
SET @StaticId = SCOPE_IDENTITY();
GO
/****** Object:  StoredProcedure [dbo].[AddPost]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPost]
(
@Body varchar(MAX),
@AuthorName varchar(MAX),
@Cateogry int,
@Approved int,
@DateCreated date,
@Headline varchar(MAX),
@Subtitle varchar(MAX),
@BlogId int output

)AS

INSERT INTO BlogPost(Body,AuthorName,Category,Approved,DateCreated,Headline,Subtitle)
VALUES(@Body, @AuthorName, @Cateogry, @Approved, @DateCreated, @Headline, @Subtitle)
SET @BlogId = SCOPE_IDENTITY();
GO
/****** Object:  StoredProcedure [dbo].[AddTag]    Script Date: 12/7/2016 10:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTag]
(
@Name varchar(MAX),
@TagId int output

)AS

INSERT INTO Tag(Name)
VALUES(@Name)
SET @TagId = SCOPE_IDENTITY();

GO
USE [master]
GO
ALTER DATABASE [ToonSaloon] SET  READ_WRITE 
GO
