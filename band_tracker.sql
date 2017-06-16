USE [master]
GO
/****** Object:  Database [band_tracker]    Script Date: 6/16/17 3:24:28 PM ******/
CREATE DATABASE [band_tracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'band_tracker', FILENAME = N'C:\Users\alyssamoody\band_tracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'band_tracker_log', FILENAME = N'C:\Users\alyssamoody\band_tracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [band_tracker] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [band_tracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [band_tracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [band_tracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [band_tracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [band_tracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [band_tracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [band_tracker] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [band_tracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [band_tracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [band_tracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [band_tracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [band_tracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [band_tracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [band_tracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [band_tracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [band_tracker] SET  ENABLE_BROKER 
GO
ALTER DATABASE [band_tracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [band_tracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [band_tracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [band_tracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [band_tracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [band_tracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [band_tracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [band_tracker] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [band_tracker] SET  MULTI_USER 
GO
ALTER DATABASE [band_tracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [band_tracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [band_tracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [band_tracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [band_tracker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [band_tracker] SET QUERY_STORE = OFF
GO
USE [band_tracker]
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
USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 6/16/17 3:24:28 PM ******/
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
/****** Object:  Table [dbo].[bands_venues]    Script Date: 6/16/17 3:24:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bands_id] [int] NULL,
	[venues_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venues]    Script Date: 6/16/17 3:24:28 PM ******/
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
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [band_name], [members], [genre], [information]) VALUES (1, N'Bon Iver', N'Justin Vernon', N'Indie Folk', N'Bon Iver is an American indie folk band founded in 2007 by singer-songwriter Justin Vernon. Vernon released Bon Iver''s debut album, For Emma, Forever Ago, independently in July 2007. The majority of that album was recorded while Vernon spent three months isolated in a cabin in northwestern Wisconsin.')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [venue_name], [location], [details]) VALUES (1, N'Crystal Ballroom', N'1332 W. Burnside, Portland, OR 97209', N'The historic Crystal Ballroom -- now over a century old -- is one of those rare concert halls that can point to a proud, diverse history while also laying claim to an ongoing musical legacy. Every time you enter this majestic ballroom, let your imagination sense the tremors resonating from a century''s worth of gatherings, and realize that you are joining a thriving, generations-long procession of show-goers. Welcome!')
INSERT [dbo].[venues] ([id], [venue_name], [location], [details]) VALUES (2, N'The Showbox', N'1426 1st Ave, Seattle, WA 98101', N'On July 24, 1939 the doors opened, and fans flocked to, Seattle’s newest entertainment hot spot – a beautiful art-deco gem named The Show Box. 2014 marked the club''s 75th anniversary, one of Seattle’s few extant entertainment venues that can lay claim to having provided local music fans with such an astonishing breadth of entertainment over the decades. From the Jazz Age to the Grunge Era to the current wave of Seattle exports – neo-folk and hip hop – the storied ballroom has featured shows by touring icons such as Duke Ellington, Muddy Waters and the Ramones — as well as those by homegrown talents ranging from burlesque queen Gypsy Rose Lee, to grunge gods Pearl Jam, to the current dynamic duo on a course for world domination Macklemore & Ryan Lewis. More recent shows have included the likes of Prince, Foo Fighters, and The Roots - to name a few.')
INSERT [dbo].[venues] ([id], [venue_name], [location], [details]) VALUES (3, N'Mississippi Studios', N'3939 N Mississippi Ave, Portland, OR 97227', N'Mississippi Studios offers concertgoers incredible acoustics (with all-custom, non-parallel venue walls + state-of-the art analog perfection in the booth) and ‘feels like you’re in the front row’ sight lines. Bar Bar is the venue’s hopping burger slinging sidekick which boasts one of Portland’s favorite burgers + a large covered patio, a great selection of beers, and an ever-changing seasonal cocktail menu – seven days a week, 11:00 am until 2:00 am. Happy hour 4pm -6pm. Minors allowed until 8:00 pm with parent or guardian.')
INSERT [dbo].[venues] ([id], [venue_name], [location], [details]) VALUES (4, N'Doug Fir Lounge', N'830 E Burnside St, Portland, OR 97214', N'Since 2004, Doug Fir Lounge has stood above the rest as Portland’s best sounding venue that hosts over 25 shows a month.   The Lounge was named to Rolling Stone Magazine’s list of top club venues in America in 2013.
Whether it’s the hottest new act on the scene, or a band doing an “underplay” (playing a show in a much smaller venue than they could otherwise), Doug Fir Lounge is the place to see the world’s best performers in the most intimate of settings for a truly memorable experience.
Some acts that have graced our stage include:  Sleater-Kinney, MGMT, Vampire Weekend, Alabama Shakes, M83, Cold War Kids, Cake, The Shins, and many, many more!')
INSERT [dbo].[venues] ([id], [venue_name], [location], [details]) VALUES (5, N'Humphreys Concerts by the Bay', N'2241 Shelter Island Dr., San Diego, CA 92106', N'Humphreys Concerts by the Bay is located between Humphreys Restaurant and Humphreys Half Moon Inn. The 1,400 seat outdoor theatre, ideally situated on San Diego Bay, has been presenting a wide variety of major attractions since 1982. Generally running from April through October, Humphreys Concerts covers the total spectrum of entertainment from rock and jazz to comedy, blues, folk and international music.')
SET IDENTITY_INSERT [dbo].[venues] OFF
USE [master]
GO
ALTER DATABASE [band_tracker] SET  READ_WRITE 
GO
