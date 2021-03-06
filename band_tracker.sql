USE [master]
GO
/****** Object:  Database [band_tracker]    Script Date: 6/21/17 12:31:26 PM ******/
CREATE DATABASE [band_tracker]
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
/****** Object:  Table [dbo].[bands]    Script Date: 6/21/17 12:31:26 PM ******/
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
/****** Object:  Table [dbo].[bands_venues]    Script Date: 6/21/17 12:31:26 PM ******/
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
/****** Object:  Table [dbo].[shows]    Script Date: 6/21/17 12:31:26 PM ******/
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
/****** Object:  Table [dbo].[venues]    Script Date: 6/21/17 12:31:26 PM ******/
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
INSERT [dbo].[bands] ([id], [band_name], [members], [genre], [information]) VALUES (2, N'Band of Horses', N'Ben Bridwell, Creighton Barrett, Ryan Monroe', N'Indie Rock', N'Band of Horses is an American rock band formed in 2004 in Seattle by Ben Bridwell. The band has released five studio albums, the most successful of which is 2010''s Grammy-nominated Infinite Arms. Band of Horses'' fifth studio album, Why Are You OK, was released in June 2016.')
INSERT [dbo].[bands] ([id], [band_name], [members], [genre], [information]) VALUES (3, N'Eric Church', N'Kenneth Eric Church', N'Country', N'Kenneth Eric Church is an American country music singer and songwriter. Signed to Capitol Nashville since 2005, he has since released a total of five studio albums for that label. His debut album, 2006''s Sinners Like Me," produced three singles on the Billboard country charts including the top 20 hits "How ''Bout You," "Two Pink Lines," and "Guys Like Me".')
INSERT [dbo].[bands] ([id], [band_name], [members], [genre], [information]) VALUES (4, N'Damien Rice', N'Damien Rice', N'Irish singer/songwriter ', N'Damien Rice is an Irish singer-songwriter, musician and record producer. Rice began his musical career as a member of the 1990s rock group Juniper, which were signed to Polygram Records in 1997. The band enjoyed moderate success with a couple of single releases, but a projected album foundered because of record company politics. After leaving the band he worked as a farmer in Tuscany and busked throughout Europe before returning to Ireland in 2001 and beginning a solo musical career, and the rest of the band went on to become Bell X1. In 2002 his debut album O reached No. 8 on the UK albums chart, won the Shortlist Music Prize and generated three top-30 singles in the UK. Rice released his second album 9 in 2006 and his songs have appeared in numerous films and television episodes. After eight years of various collaborations, Rice released his third studio album My Favourite Faded Fantasy on 31 October 2014. Rice''s personal activities include musical contributions to charitable projects such as the Songs for Tibet, Freedom Campaign and the Enough Project.')
INSERT [dbo].[bands] ([id], [band_name], [members], [genre], [information]) VALUES (5, N'Daughter', N'Elena Tonra, Igor Haefeli, Remi Aguilella', N'Indie Folk', N'Daughter are an indie folk band from England. Fronted by North London native Elena Tonra, they were formed in 2010 after the addition of Swiss-born guitarist Igor Haefeli and drummer Remi Aguilella from France. They have released four EPs, three singles and two albums, and are currently signed to Glassnote and 4AD. After playing the local London circuit, they toured supporting Ben Howard around Europe and have since played headlining tours around North America, Europe and Australia.')
INSERT [dbo].[bands] ([id], [band_name], [members], [genre], [information]) VALUES (6, N'Bear''s Den', N'Andrew Davie, Kevin Jones', N'Folk Rock', N'Bear''s Den are a British folk rock band from London, formed in 2012. The band consists of Andrew Davie (lead vocals, electric guitar, acoustic guitar) and Kevin Jones (vocals, drums, bass, guitar). Joey Haynes (vocals, banjo, guitar) left the band in early 2016. For their 2016/2017 Tour in Europe and North America Joey is replaced by Dutch artist Christof van der Ven, not as an official member but as session musician.[1]
Bear''s Den have released two studio albums: Islands (2014), their second studio album Red Earth & Pouring Rain was released on 22 July 2016. Islands peaked at number forty-nine on the UK Albums Chart. The band has also issued two EPs: Agape (2013) and Without/Within (2013).
The band have been nominated for several music awards throughout their career, with "Above the Clouds of Pompeii" earning the band a nomination for the Ivor Novello Award for Best Song Musically and Lyrically in 2015. The band received two nominations at the UK Americana Awards in 2016, Artist of the Year and Song of the Year for "Agape".')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[bands_venues] ON 

INSERT [dbo].[bands_venues] ([id], [bands_id], [venues_id], [shows_id]) VALUES (1, 2, 4, NULL)
INSERT [dbo].[bands_venues] ([id], [bands_id], [venues_id], [shows_id]) VALUES (2, 3, 5, NULL)
INSERT [dbo].[bands_venues] ([id], [bands_id], [venues_id], [shows_id]) VALUES (3, 4, 2, NULL)
INSERT [dbo].[bands_venues] ([id], [bands_id], [venues_id], [shows_id]) VALUES (4, 5, 2, NULL)
SET IDENTITY_INSERT [dbo].[bands_venues] OFF
SET IDENTITY_INSERT [dbo].[shows] ON 

INSERT [dbo].[shows] ([id], [city_state], [date], [bands_id], [venues_id]) VALUES (1, N'Portland, OR', CAST(N'2017-06-20T19:30:00.000' AS DateTime), 4, 1)
INSERT [dbo].[shows] ([id], [city_state], [date], [bands_id], [venues_id]) VALUES (2, N'Portland, OR', CAST(N'2017-06-20T19:45:00.000' AS DateTime), 2, 3)
INSERT [dbo].[shows] ([id], [city_state], [date], [bands_id], [venues_id]) VALUES (3, N'Portland, OR', CAST(N'2018-08-22T19:45:00.000' AS DateTime), 4, 1)
SET IDENTITY_INSERT [dbo].[shows] OFF
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
