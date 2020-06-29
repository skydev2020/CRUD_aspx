-- ************************************** [dbo].[Permissions]
CREATE TABLE [dbo].[Permissions]
(
 [PermissionId]  int IDENTITY (1, 1) NOT NULL ,
 [PermissionType] varchar(50) NOT NULL ,


 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([PermissionId] ASC)
);
GO

-- ************************************** [dbo].[OrderStatus]
CREATE TABLE [dbo].[OrderStatus]
(
 [OrderStatusId] int IDENTITY (1, 1) NOT NULL ,
 [StatusType]    nvarchar(50) NOT NULL ,


 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED ([OrderStatusId] ASC)
);
GO

-- ************************************** [dbo].[States]
CREATE TABLE [dbo].[States]
(
 [StateID]    int IDENTITY (1, 1) NOT NULL ,
 [StateAbbrv] varchar(2) NOT NULL ,
 [StateName]  varchar(20) NOT NULL ,


 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([StateID] ASC)
);
GO

-- ************************************** [dbo].[Carrier]
CREATE TABLE [dbo].[Carrier]
(
 [CarrierId]   int IDENTITY (1, 1) NOT NULL ,
 [CarrierName] nvarchar(50) NOT NULL ,


 CONSTRAINT [CLUSTERED] PRIMARY KEY CLUSTERED ([CarrierId] ASC)
);
GO

-- ************************************** [dbo].[StorageCapacity]
CREATE TABLE [dbo].[StorageCapacity]
(
 [StorageCapacityId] int IDENTITY (1, 1) NOT NULL ,
 [StorageCapacity]   int NOT NULL ,


 CONSTRAINT [PK_StorageCapacity] PRIMARY KEY CLUSTERED ([StorageCapacityId] ASC)
);
GO


-- ************************************** [dbo].[PhoneVersion]
CREATE TABLE [dbo].[PhoneVersion]
(
 [VersionId]         int IDENTITY (1, 1) NOT NULL ,
 [Version]           nvarchar(50) NOT NULL ,
 [BaseCost]          money NULL ,
 [ImageName]         nvarchar(50) NULL ,
 [PhoneId]           int NOT NULL ,
 [StorageCapacityId] int NOT NULL ,


 CONSTRAINT [PK_PhoneVersion] PRIMARY KEY CLUSTERED ([VersionId] ASC),
 CONSTRAINT [FK_186] FOREIGN KEY ([PhoneId])  REFERENCES [dbo].[Phones]([PhoneId]),
 CONSTRAINT [FK_189] FOREIGN KEY ([StorageCapacityId])  REFERENCES [dbo].[StorageCapacity]([StorageCapacityId])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_186] ON [dbo].[PhoneVersion] 
 (
  [PhoneId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_189] ON [dbo].[PhoneVersion] 
 (
  [StorageCapacityId] ASC
 )

GO


CREATE NONCLUSTERED INDEX [fkIdx_186] ON [dbo].[PhoneVersion] 
 (
  [PhoneId] ASC
 )
 GO


-- ************************************** [dbo].[User]
CREATE TABLE [dbo].[User]
(
 [UserId]       int IDENTITY (1, 1) NOT NULL ,
 [UserName]     nvarchar(50) NOT NULL ,
 [Password]     nvarchar(50) NOT NULL ,
 [PermissionId] int NOT NULL ,
 [FirstName]    nvarchar(50) NOT NULL ,
 [LastName]     nvarchar(50) NOT NULL ,
 [Email]        nvarchar(50) NOT NULL ,
 [Address]      nvarchar(100) NOT NULL ,
 [Address2]     nvarchar(100) NULL ,
 [City]         nvarchar(50) NOT NULL ,
 [State]        nvarchar(2) NOT NULL ,
 [Zip]          int NOT NULL ,
 [PhoneNumber]  nvarchar(50) NOT NULL ,
 [LastLogin]    datetime NULL ,
 [CreateDate]   datetime NOT NULL ,
 [CreatedBy]    nvarchar(50) NULL ,


 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([UserId] ASC),
 CONSTRAINT [AK1_Customer_CustomerName] UNIQUE NONCLUSTERED ([UserName] ASC),
 CONSTRAINT [FK_178] FOREIGN KEY ([PermissionId])  REFERENCES [dbo].[Permissions]([PermissionId])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_178] ON [dbo].[User] 
 (
  [PermissionId] ASC
 )

GO


EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Basic information 
about Customer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'User';
GO


-- ************************************** [dbo].[Orders]
CREATE TABLE [dbo].[Orders]
(
 [OrderID]       int IDENTITY (1, 1) NOT NULL ,
 [Amount]        money NOT NULL ,
 [UserId]        int NOT NULL ,
 [OrderStatusId] int NOT NULL ,
 [CreateDate]    datetime NOT NULL ,
 [CreateBy]      nvarchar(50) NOT NULL ,


 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC),
 CONSTRAINT [FK_106] FOREIGN KEY ([UserId])  REFERENCES [dbo].[User]([UserId]),
 CONSTRAINT [FK_151] FOREIGN KEY ([OrderStatusId])  REFERENCES [dbo].[OrderStatus]([OrderStatusId])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_106] ON [dbo].[Orders] 
 (
  [UserId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_151] ON [dbo].[Orders] 
 (
  [OrderStatusId] ASC
 )

GO


-- ************************************** [dbo].[Phones]
CREATE TABLE [dbo].[Phones]
(
 [PhoneId] int IDENTITY (1, 1) NOT NULL ,
 [Brand]   nvarchar(50) NOT NULL ,


 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED ([PhoneId] ASC),
 CONSTRAINT [AK1_Supplier_CompanyName] UNIQUE NONCLUSTERED ([Brand] ASC)
);
GO


-- ************************************** [dbo].[UserPhone]
CREATE TABLE [dbo].[UserPhone]
(
 [UserPhoneId] int IDENTITY (1, 1) NOT NULL ,
 [UserId]      int NOT NULL ,
 [PhoneId]     int NOT NULL ,
 [CarrierId]   int NOT NULL ,
 [VersionId]   int NOT NULL ,
 [CreateDate]  datetime NOT NULL ,


 CONSTRAINT [PK_UserPhone] PRIMARY KEY CLUSTERED ([UserPhoneId] ASC),
 CONSTRAINT [FK_171] FOREIGN KEY ([CarrierId])  REFERENCES [dbo].[Carrier]([CarrierId]),
 CONSTRAINT [FK_174] FOREIGN KEY ([VersionId])  REFERENCES [dbo].[PhoneVersion]([VersionId]),
 CONSTRAINT [FK_175] FOREIGN KEY ([UserId])  REFERENCES [dbo].[User]([UserId]),
 CONSTRAINT [FK_190] FOREIGN KEY ([PhoneId])  REFERENCES [dbo].[Phones]([PhoneId])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_171] ON [dbo].[UserPhone] 
 (
  [CarrierId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_174] ON [dbo].[UserPhone] 
 (
  [VersionId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_175] ON [dbo].[UserPhone] 
 (
  [UserId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_190] ON [dbo].[UserPhone] 
 (
  [PhoneId] ASC
 )

GO


-- ************************************** [dbo].[PossibleDefects]
CREATE TABLE [dbo].[PossibleDefects]
(
 [PossibleDefectId] int IDENTITY (1, 1) NOT NULL ,
 [DefectName]       nvarchar(150) NOT NULL ,
 [DefectCost]       money NOT NULL ,
 [VersionId]        int NOT NULL ,


 CONSTRAINT [PK_PossibleDefects] PRIMARY KEY CLUSTERED ([PossibleDefectId] ASC),
 CONSTRAINT [FK_125] FOREIGN KEY ([VersionId])  REFERENCES [dbo].[PhoneVersion]([VersionId])
);
GO

CREATE NONCLUSTERED INDEX [fkIdx_125] ON [dbo].[PossibleDefects] 
 (
  [VersionId] ASC
 )

GO


-- ************************************** [dbo].[UserAnswers]
CREATE TABLE [dbo].[UserAnswers]
(
 [AnswerId]         int IDENTITY (1, 1) NOT NULL ,
 [Answer]           bit NOT NULL ,
 [PossibleDefectId] int NOT NULL ,
 [UserPhoneId]      int NOT NULL ,


 CONSTRAINT [PK_UserAnswers] PRIMARY KEY CLUSTERED ([AnswerId] ASC),
 CONSTRAINT [FK_110] FOREIGN KEY ([PossibleDefectId])  REFERENCES [dbo].[PossibleDefects]([PossibleDefectId]),
 CONSTRAINT [FK_137] FOREIGN KEY ([UserPhoneId])  REFERENCES [dbo].[UserPhone]([UserPhoneId])
);
GO

CREATE NONCLUSTERED INDEX [fkIdx_110] ON [dbo].[UserAnswers] 
 (
  [PossibleDefectId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_137] ON [dbo].[UserAnswers] 
 (
  [UserPhoneId] ASC
 )

GO








