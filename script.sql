USE [master]
GO
/****** Object:  Database [ToonSaloon]    Script Date: 11/30/2016 9:36:10 AM ******/
CREATE DATABASE [ToonSaloon]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToonSaloon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQL2014\MSSQL\DATA\ToonSaloon.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
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
/****** Object:  Table [dbo].[BlogPost]    Script Date: 11/30/2016 9:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BlogPost](
	[Id] [uniqueidentifier] NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[Tags] [varchar](max) NOT NULL,
	[AuthorName] [varchar](max) NOT NULL,
	[Cateogry] [varchar](max) NOT NULL,
	[isApproved] [int] NOT NULL,
	[Youtube] [varchar](max) NOT NULL,
	[DateCreated] [date] NOT NULL,
	[Imgs] [varchar](max) NOT NULL,
	[Headline] [varchar](max) NOT NULL,
	[Subtitle] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CartoonOfTheDay]    Script Date: 11/30/2016 9:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CartoonOfTheDay](
	[Id] [uniqueidentifier] NOT NULL,
	[Author] [varchar](max) NOT NULL,
	[isApproved] [int] NOT NULL,
	[ShowName] [varchar](max) NOT NULL,
	[Season] [int] NOT NULL,
	[Episode] [int] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[ImgUrl] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Img]    Script Date: 11/30/2016 9:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Img](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [varchar](max) NOT NULL,
	[Source] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StaticPage]    Script Date: 11/30/2016 9:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StaticPage](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[DateCreated] [date] NOT NULL,
	[isApproved] [int] NOT NULL,
	[Tag] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 11/30/2016 9:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Youtube]    Script Date: 11/30/2016 9:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Youtube](
	[TubeId] [varchar](11) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [ToonSaloon] SET  READ_WRITE 
GO
