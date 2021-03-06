USE [master]
GO
/****** Object:  Database [Cellable]    Script Date: 12/10/2019 8:58:17 AM ******/
CREATE DATABASE [Cellable]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cellable', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Cellable.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cellable_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Cellable_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Cellable] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cellable].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cellable] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cellable] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cellable] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cellable] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cellable] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cellable] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cellable] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cellable] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cellable] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cellable] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cellable] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cellable] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cellable] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cellable] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cellable] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cellable] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cellable] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cellable] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cellable] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cellable] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cellable] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cellable] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cellable] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Cellable] SET  MULTI_USER 
GO
ALTER DATABASE [Cellable] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cellable] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cellable] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cellable] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cellable] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Cellable', N'ON'
GO
ALTER DATABASE [Cellable] SET QUERY_STORE = OFF
GO
USE [Cellable]
GO
/****** Object:  Table [dbo].[Carrier]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[CarrierId] [int] IDENTITY(1,1) NOT NULL,
	[CarrierName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[UserId] [int] NOT NULL,
	[UserPhoneId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] NOT NULL,
	[PermissionType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[PhoneId] [int] IDENTITY(1,1) NOT NULL,
	[Brand] [nvarchar](50) NOT NULL,
	[StorageCapacityId] [int] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK1_Supplier_CompanyName] UNIQUE NONCLUSTERED 
(
	[Brand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneVersion]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneVersion](
	[Id] [int] NOT NULL,
	[PhoneId] [int] NOT NULL,
	[Version] [nvarchar](50) NOT NULL,
	[BaseCost] [money] NULL,
	[StorageCapacity] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PossibleDefects]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PossibleDefects](
	[PossibleDefectId] [int] NOT NULL,
	[DefectName] [nvarchar](150) NOT NULL,
	[DefectCosst] [money] NOT NULL,
	[PhoneId] [int] NOT NULL,
	[ControlId] [int] NULL,
	[VersionID] [int] NULL,
 CONSTRAINT [PK_PossibleDefects] PRIMARY KEY CLUSTERED 
(
	[PossibleDefectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[States]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[StateID] [int] NOT NULL,
	[StateAbbrv] [varchar](2) NOT NULL,
	[StateName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StorageCapacity]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StorageCapacity](
	[StorageCapacityId] [int] IDENTITY(1,1) NOT NULL,
	[StorageCapacity] [int] NOT NULL,
 CONSTRAINT [PK_StorageCapacity] PRIMARY KEY CLUSTERED 
(
	[StorageCapacityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[StreetNumber] [int] NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[Zip] [int] NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[PermissionId] [int] NOT NULL,
	[LastLogin] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK1_Customer_CustomerName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPhone]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPhone](
	[UserPhoneId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PhoneId] [int] NOT NULL,
 CONSTRAINT [PK_UserPhone] PRIMARY KEY CLUSTERED 
(
	[UserPhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_184]    Script Date: 12/10/2019 8:58:17 AM ******/
CREATE NONCLUSTERED INDEX [fkIdx_184] ON [dbo].[PossibleDefects]
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_178]    Script Date: 12/10/2019 8:58:17 AM ******/
CREATE NONCLUSTERED INDEX [fkIdx_178] ON [dbo].[User]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_175]    Script Date: 12/10/2019 8:58:17 AM ******/
CREATE NONCLUSTERED INDEX [fkIdx_175] ON [dbo].[UserPhone]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_190]    Script Date: 12/10/2019 8:58:17 AM ******/
CREATE NONCLUSTERED INDEX [fkIdx_190] ON [dbo].[UserPhone]
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Orders]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_Phones] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phones] ([PhoneId])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_Phones]
GO
ALTER TABLE [dbo].[PossibleDefects]  WITH CHECK ADD  CONSTRAINT [FK_184] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phones] ([PhoneId])
GO
ALTER TABLE [dbo].[PossibleDefects] CHECK CONSTRAINT [FK_184]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_178] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([PermissionId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_178]
GO
ALTER TABLE [dbo].[UserPhone]  WITH CHECK ADD  CONSTRAINT [FK_175] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserPhone] CHECK CONSTRAINT [FK_175]
GO
ALTER TABLE [dbo].[UserPhone]  WITH CHECK ADD  CONSTRAINT [FK_190] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phones] ([PhoneId])
GO
ALTER TABLE [dbo].[UserPhone] CHECK CONSTRAINT [FK_190]
GO
/****** Object:  StoredProcedure [dbo].[spGetCarriers]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[spGetCarriers]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		CarrierId,
		CarrierName
	FROM dbo.Carrier
	ORDER BY CarrierName
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPhoneDefects]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[spGetPhoneDefects]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [PossibleDefectId] AS ID
		  ,[DefectName]
		  ,CAST([DefectCosst] as DECIMAL(9,2)) AS DefectCosst
		  ,p.Brand
	  FROM [dbo].[PossibleDefects] pv
	  LEFT JOIN [dbo].Phones p
		ON pv.PhoneId = p.PhoneId
	ORDER BY pv.[DefectName], p.Brand
END
GO
/****** Object:  StoredProcedure [dbo].[spGetPhones]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[spGetPhones]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		pv.Id
		,Brand
		,pv.StorageCapacity
		,CAST(pv.BaseCost as DECIMAL(9,2)) AS BaseCost
	FROM Phones p
	LEFT JOIN PhoneVersion pv
	ON p.PhoneId = pv.PhoneId
	ORDER BY Brand, pv.Version
END
GO
/****** Object:  StoredProcedure [dbo].[spGetStorageCapacity]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[spGetStorageCapacity]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		StorageCapacityId
		,StorageCapacity
	FROM StorageCapacity
	ORDER BY StorageCapacity
END
GO
/****** Object:  StoredProcedure [dbo].[spValidateAdminUser]    Script Date: 12/10/2019 8:58:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Britt Moody
-- Create date: 11/28/2019
-- Description:	Validate Admin User
-- =============================================
CREATE   PROCEDURE [dbo].[spValidateAdminUser]
	-- Add the parameters for the stored procedure here
	@UserName varchar(50),
	@UserPassword varchar(50),
	@UserID int OUTPUT,
	@IsAdmin int OUTPUT,
	@LastLogin varchar(50) OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- This is what's returned to the the procedure call
	SELECT @UserID = UserID,
		@IsAdmin = 1 , 
		@LastLogin = CONVERT(VARCHAR(19), LastLogin)
	FROM [dbo].[User] AU
	WHERE UserName = @UserName 
		AND Password = @UserPassword;
	
	-- Update last Login Date & Time
	UPDATE [dbo].[User] SET LastLogin = GETDATE() WHERE UserName = @UserName AND Password = @UserPassword;

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Basic information 
about Customer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
USE [master]
GO
ALTER DATABASE [Cellable] SET  READ_WRITE 
GO
