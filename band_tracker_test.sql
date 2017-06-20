USE [master]
GO
/****** Object:  Database [band_tracker_test]    Script Date: 6/19/17 10:17:38 PM ******/
CREATE DATABASE [band_tracker_test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'band_tracker', FILENAME = N'C:\Users\alyssamoody\band_tracker_test.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'band_tracker_log', FILENAME = N'C:\Users\alyssamoody\band_tracker_test_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [band_tracker_test] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [band_tracker_test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [band_tracker_test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [band_tracker_test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [band_tracker_test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [band_tracker_test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [band_tracker_test] SET ARITHABORT OFF 
GO
ALTER DATABASE [band_tracker_test] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [band_tracker_test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [band_tracker_test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [band_tracker_test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [band_tracker_test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [band_tracker_test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [band_tracker_test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [band_tracker_test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [band_tracker_test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [band_tracker_test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [band_tracker_test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [band_tracker_test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [band_tracker_test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [band_tracker_test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [band_tracker_test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [band_tracker_test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [band_tracker_test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [band_tracker_test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [band_tracker_test] SET  MULTI_USER 
GO
ALTER DATABASE [band_tracker_test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [band_tracker_test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [band_tracker_test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [band_tracker_test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [band_tracker_test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [band_tracker_test] SET QUERY_STORE = OFF
GO
USE [band_tracker_test]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [band_tracker_test]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 6/19/17 10:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[band_name] [varchar](100) NULL,
	[members] [varchar](255) NULL,
	[genre] [varchar](50) NULL,
	[information] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bands_venues]    Script Date: 6/19/17 10:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bands_id] [int] NULL,
	[venues_id] [int] NULL,
	[shows_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shows]    Script Date: 6/19/17 10:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shows](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[city_state] [varchar](100) NULL,
	[date] [datetime] NULL,
	[bands_id] [int] NULL,
	[venues_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venues]    Script Date: 6/19/17 10:17:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[venue_name] [varchar](100) NULL,
	[location] [varchar](100) NULL,
	[details] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [band_tracker_test] SET  READ_WRITE 
GO
