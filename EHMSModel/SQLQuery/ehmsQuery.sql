USE [master]
GO
/****** Object:  Database [EHMS]    Script Date: 12/18/2024 10:46:17 AM ******/
CREATE DATABASE [EHMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EHMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EHMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EHMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EHMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EHMS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EHMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EHMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EHMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EHMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EHMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EHMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [EHMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EHMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EHMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EHMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EHMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EHMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EHMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EHMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EHMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EHMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EHMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EHMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EHMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EHMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EHMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EHMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EHMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EHMS] SET RECOVERY FULL 
GO
ALTER DATABASE [EHMS] SET  MULTI_USER 
GO
ALTER DATABASE [EHMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EHMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EHMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EHMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EHMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EHMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EHMS', N'ON'
GO
ALTER DATABASE [EHMS] SET QUERY_STORE = ON
GO
ALTER DATABASE [EHMS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EHMS]
GO
/****** Object:  Table [dbo].[EmployeeHealthInfo]    Script Date: 12/18/2024 10:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeHealthInfo](
	[EmployeeHealthInfoId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[BloodGroup] [varchar](50) NULL,
	[Disability] [bit] NULL,
	[MedicalReportFileName] [varchar](255) NULL,
	[MedicalReport] [varbinary](max) NULL,
	[RecentMedicalReportPath] [varchar](500) NULL,
 CONSTRAINT [PK__Employee__5D6AC54B99D96712] PRIMARY KEY CLUSTERED 
(
	[EmployeeHealthInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePhysicalFitness]    Script Date: 12/18/2024 10:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePhysicalFitness](
	[EmployeePhysicalFitnessId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeePhysicalFitnessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeRole]    Script Date: 12/18/2024 10:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRole](
	[EmpRoleId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeRole] PRIMARY KEY CLUSTERED 
(
	[EmpRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 12/18/2024 10:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [varchar](50) NOT NULL,
	[EmployeeName] [varchar](100) NOT NULL,
	[Department] [varchar](100) NOT NULL,
	[JobTitle] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[AzureEntraId] [varchar](100) NULL,
 CONSTRAINT [PK__Employee__AF2DBB992589A724] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestsForHelp]    Script Date: 12/18/2024 10:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestsForHelp](
	[RequestForHelpId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[Conversation] [varchar](1000) NULL,
	[Status] [varchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[RespondedAt] [datetime] NULL,
	[RespondedStatus] [varchar](1000) NULL,
 CONSTRAINT [PK__Requests__7C444F31CAA607A9] PRIMARY KEY CLUSTERED 
(
	[RequestForHelpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/18/2024 10:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK__Roles__8AFACE1A55369F30] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EmployeeHealthInfo] ON 
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (11, 8, N'j', 0, N'5.pdf example_2009.pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads')
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (13, 7, N'ghj', 0, N'5.pdf example_2009.pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads')
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (24, 12, N'es', 0, N'dummy.pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads')
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (26, 16, N'dfdf', 0, N'Bench Policy (2).pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads')
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (27, 12, N'c+', 1, N'5.pdf example_2009.pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads')
GO
SET IDENTITY_INSERT [dbo].[EmployeeHealthInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeePhysicalFitness] ON 
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (4, 8, 5631, 22)
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (6, 12, 50, 10)
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (7, 8, 60, 25)
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (10, 7, 20, 30)
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (12, 16, 50, 60)
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (15, 14, 34, 22)
GO
SET IDENTITY_INSERT [dbo].[EmployeePhysicalFitness] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeRole] ON 
GO
INSERT [dbo].[EmployeeRole] ([EmpRoleId], [EmpId], [RoleId]) VALUES (1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[EmployeeRole] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [Department], [JobTitle], [Email], [AzureEntraId]) VALUES (7, N'101', N'srk', N'cse', N'sses', NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [Department], [JobTitle], [Email], [AzureEntraId]) VALUES (8, N'102', N'shwetha', N'CX', N'TL', NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [Department], [JobTitle], [Email], [AzureEntraId]) VALUES (12, N'103', N'MOHD SAJID', N'IT', N'SE', NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [Department], [JobTitle], [Email], [AzureEntraId]) VALUES (13, N'104', N'TEST1', N'TESTDepartment1', N'TESTJob Title1', NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [Department], [JobTitle], [Email], [AzureEntraId]) VALUES (14, N'105', N'Name2', N'Department2', N'Job Title2', NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [Department], [JobTitle], [Email], [AzureEntraId]) VALUES (16, N'104', N'bhumi', N'CX REG', N'SE', NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [Department], [JobTitle], [Email], [AzureEntraId]) VALUES (21, N'106', N'testw', N'dstesd', N'tesj', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([RoleId], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([RoleId], [Name]) VALUES (2, N'HR')
GO
INSERT [dbo].[Roles] ([RoleId], [Name]) VALUES (3, N'Employee')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
ALTER TABLE [dbo].[RequestsForHelp] ADD  CONSTRAINT [DF__RequestsF__Creat__4CA06362]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[EmployeePhysicalFitness]  WITH CHECK ADD  CONSTRAINT [FK__EmployeeP__Emplo__4316F928] FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employees] ([EmpId])
GO
ALTER TABLE [dbo].[EmployeePhysicalFitness] CHECK CONSTRAINT [FK__EmployeeP__Emplo__4316F928]
GO
ALTER TABLE [dbo].[RequestsForHelp]  WITH CHECK ADD  CONSTRAINT [FK__RequestsF__Emplo__4D94879B] FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employees] ([EmpId])
GO
ALTER TABLE [dbo].[RequestsForHelp] CHECK CONSTRAINT [FK__RequestsF__Emplo__4D94879B]
GO
USE [master]
GO
ALTER DATABASE [EHMS] SET  READ_WRITE 
GO
