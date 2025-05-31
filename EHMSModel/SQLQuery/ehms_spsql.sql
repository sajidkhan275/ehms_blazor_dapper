USE [master]
GO
/****** Object:  Database [EHMS]    Script Date: 1/2/2025 12:19:24 PM ******/
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
/****** Object:  Table [dbo].[Departments]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeHealthInfo]    Script Date: 1/2/2025 12:19:25 PM ******/
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
/****** Object:  Table [dbo].[EmployeePhysicalFitness]    Script Date: 1/2/2025 12:19:25 PM ******/
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
/****** Object:  Table [dbo].[EmployeeRole]    Script Date: 1/2/2025 12:19:25 PM ******/
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
/****** Object:  Table [dbo].[Employees]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode] [varchar](50) NULL,
	[EmployeeName] [varchar](100) NOT NULL,
	[DepartmentId] [int] NULL,
	[JobTitle] [varchar](100) NULL,
	[Email] [varchar](100) NOT NULL,
	[AzureEntraId] [varchar](100) NOT NULL,
 CONSTRAINT [PK__Employee__AF2DBB992589A724] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ErrorLogId] [int] IDENTITY(1,1) NOT NULL,
	[ErrorMessage] [nvarchar](4000) NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorProcedure] [nvarchar](200) NULL,
	[ErrorLine] [int] NULL,
	[ErrorDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestsForHelp]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestsForHelp](
	[RequestForHelpId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[RequestDetails] [varchar](1000) NULL,
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
/****** Object:  Table [dbo].[Roles]    Script Date: 1/2/2025 12:19:25 PM ******/
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
SET IDENTITY_INSERT [dbo].[Departments] ON 
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [CreatedAt], [UpdatedAt]) VALUES (5, N'CS', CAST(N'2024-12-27T15:30:12.033' AS DateTime), CAST(N'2024-12-27T15:31:00.637' AS DateTime))
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [CreatedAt], [UpdatedAt]) VALUES (7, N'IT', CAST(N'2024-12-31T16:29:53.160' AS DateTime), CAST(N'2025-01-01T13:11:03.310' AS DateTime))
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [CreatedAt], [UpdatedAt]) VALUES (10, N'HR', CAST(N'2025-01-01T13:11:16.413' AS DateTime), CAST(N'2025-01-01T13:24:31.127' AS DateTime))
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName], [CreatedAt], [UpdatedAt]) VALUES (11, N''' OR 1=1;', CAST(N'2025-01-01T14:53:17.230' AS DateTime), CAST(N'2025-01-01T14:53:37.107' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeHealthInfo] ON 
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (53, 26, N'o-', 1, N'dummy.pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads\efa58613-8f4e-4560-8e67-aa79105129fadummy.pdf.enc')
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (54, 27, N'b+', 0, N'dummy.pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads\efa58613-8f4e-4560-8e67-aa79105129fadummy.pdf.enc')
GO
INSERT [dbo].[EmployeeHealthInfo] ([EmployeeHealthInfoId], [EmpId], [BloodGroup], [Disability], [MedicalReportFileName], [MedicalReport], [RecentMedicalReportPath]) VALUES (56, 36, N'O+', 1, N'Bench Policy.pdf', NULL, N'C:\Sk\EHMS\EHMSWebApp\wwwroot\uploads\6e1378c7-99c7-438f-8578-7c375b500a5fBench Policy.pdf.enc')
GO
SET IDENTITY_INSERT [dbo].[EmployeeHealthInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeePhysicalFitness] ON 
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (34, 27, 20, 45)
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (40, 26, 20, 45)
GO
INSERT [dbo].[EmployeePhysicalFitness] ([EmployeePhysicalFitnessId], [EmpId], [Weight], [Height]) VALUES (45, 36, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[EmployeePhysicalFitness] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeRole] ON 
GO
INSERT [dbo].[EmployeeRole] ([EmpRoleId], [EmpId], [RoleId]) VALUES (42, 26, 2)
GO
INSERT [dbo].[EmployeeRole] ([EmpRoleId], [EmpId], [RoleId]) VALUES (147, 27, 3)
GO
INSERT [dbo].[EmployeeRole] ([EmpRoleId], [EmpId], [RoleId]) VALUES (196, 36, 1)
GO
SET IDENTITY_INSERT [dbo].[EmployeeRole] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [DepartmentId], [JobTitle], [Email], [AzureEntraId]) VALUES (26, N'101', N'Adele Vance', 10, N'SSE', N'AdeleV@3zdrlc.onmicrosoft.com', N'eae63fe4-e9f7-4e7a-823b-a11a8afc655b')
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [DepartmentId], [JobTitle], [Email], [AzureEntraId]) VALUES (27, N'103', N'Diego Siciliani', 7, N'QA', N'DiegoS@3zdrlc.onmicrosoft.com', N'c4cba155-1512-4ea4-be0f-ccc74862f1f5')
GO
INSERT [dbo].[Employees] ([EmpId], [EmployeeCode], [EmployeeName], [DepartmentId], [JobTitle], [Email], [AzureEntraId]) VALUES (36, N'203', N'Mohammed Sajid', 5, N'sse', N'sajidkhan1@3zdrlc.onmicrosoft.com', N'c064c102-7ef4-4c0f-9c74-327dbb646b9e')
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[RequestsForHelp] ON 
GO
INSERT [dbo].[RequestsForHelp] ([RequestForHelpId], [EmpId], [RequestDetails], [Status], [CreatedAt], [RespondedAt], [RespondedStatus]) VALUES (2, 27, N'aSA', N'Active', CAST(N'2024-12-19T13:07:41.513' AS DateTime), CAST(N'2024-12-20T16:41:13.743' AS DateTime), N'TEST')
GO
INSERT [dbo].[RequestsForHelp] ([RequestForHelpId], [EmpId], [RequestDetails], [Status], [CreatedAt], [RespondedAt], [RespondedStatus]) VALUES (10, 27, N'aaa', N'Pending', CAST(N'2024-12-25T13:50:42.257' AS DateTime), CAST(N'2024-12-27T19:01:38.150' AS DateTime), N'testhghg')
GO
INSERT [dbo].[RequestsForHelp] ([RequestForHelpId], [EmpId], [RequestDetails], [Status], [CreatedAt], [RespondedAt], [RespondedStatus]) VALUES (18, 36, N'health checkup', N'Pending', CAST(N'2025-01-01T14:34:32.700' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[RequestsForHelp] OFF
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
ALTER TABLE [dbo].[Departments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  DEFAULT (getdate()) FOR [ErrorDateTime]
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
/****** Object:  StoredProcedure [dbo].[MangeDepartment]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mohammed Sajid
-- Create date: 24-12-2024
-- Description:	Mange DEPARTMENT INFO
-- =============================================
CREATE PROCEDURE [dbo].[MangeDepartment]
	@Filter varchar(100),
	@DepartmentId int = null,
	@DepartmentName varchar(100) = null
AS
BEGIN
	SET NOCOUNT ON;

     BEGIN TRY
	  BEGIN TRANSACTION; 

	IF(@Filter ='GetAllDepartments')
	BEGIN
		select * from Departments
	END
	ELSE IF(@Filter ='DeleteDepartments')
	BEGIN
		update Employees set DepartmentId = null where DepartmentId = @DepartmentId 
		DELETE FROM Departments WHERE DepartmentId = @DepartmentId
		select 1
	END
	ELSE IF(@Filter ='CreateDepartments')
	BEGIN
		INSERT INTO Departments (DepartmentName, CreatedAt) 
         VALUES (@DepartmentName, GETDATE())
		 SELECT  CAST(SCOPE_IDENTITY() as int)
	END
	ELSE IF(@Filter ='UpdateDepartments')
	BEGIN
		UPDATE Departments  SET DepartmentName = @DepartmentName, UpdatedAt = GETDATE()  WHERE DepartmentId = @DepartmentId
		select 1
	END

	   COMMIT TRANSACTION;

	END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; 

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        

		INSERT INTO ErrorLog (ErrorMessage, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine)
        VALUES (
        @ErrorMessage,
        @ErrorSeverity,
        @ErrorState,
        ERROR_PROCEDURE(),
        ERROR_LINE() );

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[MangeEmployeeData]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mohammed Sajid
-- Create date: 24-12-2024
-- Description:	Mange Employee Info
-- =============================================
CREATE PROCEDURE [dbo].[MangeEmployeeData]
	@Filter varchar(100) =NULL,
	@AzureEntraId varchar(500) = null,
	@EmpId int = null,
	@RoleId int = null,
    @EmployeeCode varchar(50) = null,
	@EmployeeName varchar(100) = null,
	@DepartmentId int = null,
	@JobTitle varchar(100)= null,
	@Email varchar(100) = null
AS
BEGIN
	SET NOCOUNT ON;

	   BEGIN TRY
	   BEGIN TRANSACTION; 
    IF(@Filter ='DeleteEmployee')
	BEGIN
		DELETE FROM EmployeeRole WHERE EmpId = @EmpId
		DELETE FROM EmployeeHealthInfo WHERE EmpId = @EmpId
		DELETE FROM EmployeePhysicalFitness WHERE EmpId = @EmpId
	    DELETE FROM RequestsForHelp WHERE EmpId = @EmpId
		DELETE FROM Employees WHERE EmpId = @EmpId
		select 1
	END
	ELSE IF(@Filter ='CreateEmployee')
	BEGIN
		IF ((SELECT COUNT(1) FROM Employees WHERE AzureEntraId = @AzureEntraId) = 0)
		BEGIN
				INSERT INTO Employees (EmployeeName,  Email,AzureEntraId) 
                 VALUES ( @EmployeeName, @Email, @AzureEntraId)
				 SELECT @EmpId = CAST(SCOPE_IDENTITY() as int)

				 INSERT INTO EmployeeRole (EmpId, RoleId) VALUES (@EmpId, @RoleId)
				 SELECT @EmpId
		END	
		ELSE 
		  SELECT 0
	END
	ELSE IF(@Filter ='UpdateEmployee')
	BEGIN
		UPDATE Employees  SET EmployeeCode = @EmployeeCode, EmployeeName = @EmployeeName, DepartmentId = @DepartmentId, 
		JobTitle = @JobTitle WHERE EmpId = @EmpId
		select 1
	END
	ELSE IF(@Filter ='AddRole')
	BEGIN
		   INSERT INTO EmployeeRole (EmpId, RoleId) VALUES (@EmpId, @RoleId)
		   select 1
	END
	ELSE IF(@Filter ='GetRoleEmpWise')
	BEGIN
	    SELECT * FROM Employees emp WHERE emp.AzureEntraId = @AzureEntraId
		SELECT ER.*, R.[Name] FROM EmployeeRole ER INNER JOIN Roles r ON ER.RoleId = r.RoleId where ER.EmpId=  (SELECT EmpId FROM Employees emp WHERE emp.AzureEntraId = @AzureEntraId)
	END
	ELSE IF(@Filter ='DeletRoleEmpWise')
	BEGIN
	    DELETE FROM EmployeeRole where EmpId = @EmpId 
		select 1
	END
	ELSE IF(@Filter ='GetEmpRole')
	BEGIN
	     SELECT e.EmpId,e.EmployeeName,STRING_AGG(r.Name, ', ') AS [Name] , STRING_AGG(r.RoleId, ', ') AS RolesId
         FROM Employees e LEFT JOIN EmployeeRole er ON e.EmpId = er.EmpId
         LEFT JOIN Roles r ON er.RoleId = r.RoleId GROUP BY e.EmpId, e.EmployeeName;
	END
	ELSE IF(@Filter ='GetAllRole')
	BEGIN
	    select * from  Roles 
    END
	ELSE
	BEGIN
		SELECT e.*, d.DepartmentName FROM Employees e left join Departments d  on e.DepartmentId = d.DepartmentId
	END

	 COMMIT TRANSACTION;

	    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

		INSERT INTO ErrorLog (ErrorMessage, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine)
        VALUES (
        @ErrorMessage,
        @ErrorSeverity,
        @ErrorState,
        ERROR_PROCEDURE(),
        ERROR_LINE() );

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[MangeHealthInfo]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mohammed Sajid
-- Create date: 24-12-2024
-- Description:	Mange Employee Health Info
-- =============================================
CREATE PROCEDURE [dbo].[MangeHealthInfo]
	@Filter varchar(100),
	@EmpId int = null,
	@EmployeeHealthInfoId int = null,
	@BloodGroup varchar(50) = null,
	@Disability bit = null,
    @MedicalReportFileName varchar(255) = null,
	@RecentMedicalReportPath varchar(500) = null
AS
BEGIN
	SET NOCOUNT ON;

	  BEGIN TRY
	  BEGIN TRANSACTION; 

	IF(@Filter ='GetAllEmployeeHealthInfo')
	BEGIN
		SELECT epf.*, emp.EmployeeName FROM EmployeeHealthInfo  epf inner join Employees emp on epf.EmpId = emp.EmpId
	END
	ELSE IF(@Filter ='GetAllEmployeeHealthInfoByEmpId')
	BEGIN
		SELECT epf.*, emp.EmployeeName FROM EmployeeHealthInfo  epf inner join Employees emp on epf.EmpId = emp.EmpId WHERE epf.EmpId = @EmpId
	END
	ELSE IF(@Filter ='DeleteEmployeeHealthInfoAsync')
	BEGIN
		DELETE FROM EmployeeHealthInfo WHERE EmployeeHealthInfoId = @EmployeeHealthInfoId
		select 1
	END
	ELSE IF(@Filter ='CreateEmployeeHealthInfoAsync')
	BEGIN
		INSERT INTO EmployeeHealthInfo (EmpId, BloodGroup, Disability, MedicalReportFileName, RecentMedicalReportPath ) 
         VALUES (@EmpId, @BloodGroup, @Disability, @MedicalReportFileName, @RecentMedicalReportPath )
		 SELECT  CAST(SCOPE_IDENTITY() as int)
	END
	ELSE IF(@Filter ='UpdateEmployeeHealthInfoAsync')
	BEGIN
		UPDATE EmployeeHealthInfo 
                                 SET EmpId = @EmpId, BloodGroup = @BloodGroup, Disability = @Disability,
                                 MedicalReportFileName = @MedicalReportFileName,  RecentMedicalReportPath  =@RecentMedicalReportPath 
                                 WHERE EmployeeHealthInfoId = @EmployeeHealthInfoId
								 select 1
	END

        COMMIT TRANSACTION;

	END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; 

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        

		INSERT INTO ErrorLog (ErrorMessage, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine)
        VALUES (
        @ErrorMessage,
        @ErrorSeverity,
        @ErrorState,
        ERROR_PROCEDURE(),
        ERROR_LINE() );

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[MangePhysicalFitness]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mohammed Sajid
-- Create date: 24-12-2024
-- Description:	Mange Employee Physical Fitness
-- =============================================
CREATE PROCEDURE [dbo].[MangePhysicalFitness]
	@Filter varchar(100),
	@EmpId int = null,
	@EmployeePhysicalFitnessId int = null,
	@Weight decimal(18,2) = null,
	@Height decimal(18,2)  = null
AS
BEGIN
	SET NOCOUNT ON;

     BEGIN TRY
	  BEGIN TRANSACTION; 

	IF(@Filter ='GetAllEmployeePhysicalFitness')
	BEGIN
		SELECT epf.*, emp.EmployeeName FROM EmployeePhysicalFitness epf inner join Employees emp on epf.EmpId = emp.EmpId
	END
	ELSE IF(@Filter ='GetAllEmployeePhysicalFitnessByEmpId')
	BEGIN
		SELECT epf.*, emp.EmployeeName FROM EmployeePhysicalFitness epf inner join Employees emp on epf.EmpId = emp.EmpId WHERE epf.EmpId = @EmpId
	END
	ELSE IF(@Filter ='DeleteEmployeePhysicalFitnessAsync')
	BEGIN
		DELETE FROM EmployeePhysicalFitness WHERE EmployeePhysicalFitnessId = @EmployeePhysicalFitnessId
		select 1
	END
	ELSE IF(@Filter ='CreateEmployeePhysicalFitnessAsync')
	BEGIN
		INSERT INTO EmployeePhysicalFitness (EmpId, [Weight], Height) 
         VALUES (@EmpId, @Weight, @Height)
		 SELECT  CAST(SCOPE_IDENTITY() as int)
	END
	ELSE IF(@Filter ='UpdateEmployeePhysicalFitnessAsync')
	BEGIN
		UPDATE EmployeePhysicalFitness  SET EmpId = @EmpId, Weight = @Weight, Height = @Height
         WHERE EmployeePhysicalFitnessId = @EmployeePhysicalFitnessId
		 select 1
	END

	   COMMIT TRANSACTION;

	END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; 

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        

		INSERT INTO ErrorLog (ErrorMessage, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine)
        VALUES (
        @ErrorMessage,
        @ErrorSeverity,
        @ErrorState,
        ERROR_PROCEDURE(),
        ERROR_LINE() );

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[RequestForHelpManage]    Script Date: 1/2/2025 12:19:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mohammed Sajid
-- Create date: 24-12-2024
-- Description:	Mange Request for help
-- =============================================
CREATE PROCEDURE [dbo].[RequestForHelpManage]
	@Filter varchar(100),
	@EmpId int = null,
	@RequestForHelpId int = null,
	@RequestDetails varchar(1000) = null,
	@Status varchar(50) = null,
    @RespondedStatus varchar(1000) = null,
	@RecentMedicalReportPath varchar(1000) = null,
	@CreatedAt datetime = null,
	@RespondedAt datetime = null
AS
BEGIN
	SET NOCOUNT ON;

    BEGIN TRY
	  BEGIN TRANSACTION; 
	
	IF(@Filter ='GetRequestsByEmployeeIdAsync')
	BEGIN
		SELECT epf.*, emp.EmployeeName FROM RequestsForHelp epf inner join Employees emp on epf.EmpId = emp.EmpId WHERE epf.EmpId = @EmpId
	END
	ELSE IF(@Filter ='GetRequestsByEmployeeAsync')
	BEGIN
		SELECT epf.*, emp.EmployeeName FROM RequestsForHelp epf inner join Employees emp on epf.EmpId = emp.EmpId
	END
	ELSE IF(@Filter ='DeleteRequestForHelpAsync')
	BEGIN
		DELETE FROM RequestsForHelp WHERE RequestForHelpId = @RequestForHelpId
		select 1
	END
	ELSE IF(@Filter ='CreateRequestForHelpAsync')
	BEGIN
		INSERT INTO RequestsForHelp (EmpId, RequestDetails, Status, CreatedAt) 
                                 VALUES (@EmpId, @RequestDetails, @Status, @CreatedAt)
		 SELECT  CAST(SCOPE_IDENTITY() as int)
	END
	ELSE IF(@Filter ='UpdateHRRequestAsync')
	BEGIN
		UPDATE RequestsForHelp SET RespondedStatus = @RespondedStatus, Status = @Status , RespondedAt =@RespondedAt
                             WHERE RequestForHelpId = @RequestForHelpId
							 select 1
	END

	  COMMIT TRANSACTION;

	END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; 

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        

		INSERT INTO ErrorLog (ErrorMessage, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine)
        VALUES (
        @ErrorMessage,
        @ErrorSeverity,
        @ErrorState,
        ERROR_PROCEDURE(),
        ERROR_LINE() );

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [EHMS] SET  READ_WRITE 
GO
