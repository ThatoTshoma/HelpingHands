IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] int NOT NULL IDENTITY,
        [UserRole] nvarchar(max) NULL,
        [RegisteredDate] datetime2 NOT NULL,
        [Status] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Chronics] (
        [ChronicID] int NOT NULL IDENTITY,
        [ChronicName] nvarchar(50) NOT NULL,
        [Description] nvarchar(250) NOT NULL,
        CONSTRAINT [PK_Chronics] PRIMARY KEY ([ChronicID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Cities] (
        [CityID] int NOT NULL IDENTITY,
        [CityName] nvarchar(50) NOT NULL,
        [CityAbbreviation] nvarchar(10) NOT NULL,
        CONSTRAINT [PK_Cities] PRIMARY KEY ([CityID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] int NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Admins] (
        [AdminID] int NOT NULL IDENTITY,
        [ApplicationUserId] int NOT NULL,
        [FirstName] nvarchar(50) NULL,
        [Surname] nvarchar(50) NULL,
        [EmailAddress] nvarchar(50) NULL,
        CONSTRAINT [PK_Admins] PRIMARY KEY ([AdminID]),
        CONSTRAINT [FK_Admins_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] int NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Nurses] (
        [NurseID] int NOT NULL IDENTITY,
        [ApplicationUserId] int NOT NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [Surname] nvarchar(50) NOT NULL,
        [FullName] nvarchar(50) NULL,
        [IDNumber] nvarchar(13) NOT NULL,
        [EmailAddress] nvarchar(50) NOT NULL,
        [ContactNumber] nvarchar(10) NOT NULL,
        [Gender] nvarchar(max) NULL,
        CONSTRAINT [PK_Nurses] PRIMARY KEY ([NurseID]),
        CONSTRAINT [FK_Nurses_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [OfficeManagers] (
        [Id] int NOT NULL IDENTITY,
        [ApplicationUserId] int NOT NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [Surname] nvarchar(50) NOT NULL,
        [IDNumber] nvarchar(13) NOT NULL,
        [EmailAddress] nvarchar(50) NOT NULL,
        [ContactNumber] nvarchar(10) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_OfficeManagers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OfficeManagers_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Patients] (
        [PatientID] int NOT NULL IDENTITY,
        [ApplicationUserId] int NOT NULL,
        [FirstName] nvarchar(50) NULL,
        [Surname] nvarchar(50) NULL,
        [FullName] nvarchar(50) NULL,
        [IDNumber] nvarchar(13) NULL,
        [EmailAddress] nvarchar(50) NULL,
        [ContactNumber] nvarchar(10) NULL,
        [EmergencyNumber] nvarchar(10) NULL,
        [EmergencyContatctPerson] nvarchar(50) NULL,
        [Gender] nvarchar(max) NULL,
        [DateOfBirth] datetime2 NOT NULL,
        CONSTRAINT [PK_Patients] PRIMARY KEY ([PatientID]),
        CONSTRAINT [FK_Patients_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Suburbs] (
        [SuburbID] int NOT NULL IDENTITY,
        [SuburbName] nvarchar(50) NOT NULL,
        [PostalCode] nvarchar(5) NOT NULL,
        [CityID] int NOT NULL,
        CONSTRAINT [PK_Suburbs] PRIMARY KEY ([SuburbID]),
        CONSTRAINT [FK_Suburbs_Cities_CityID] FOREIGN KEY ([CityID]) REFERENCES [Cities] ([CityID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [PatientChronics] (
        [PatientChronicID] int NOT NULL IDENTITY,
        [PatientID] int NOT NULL,
        [ChronicID] int NOT NULL,
        CONSTRAINT [PK_PatientChronics] PRIMARY KEY ([PatientChronicID]),
        CONSTRAINT [FK_PatientChronics_Chronics_ChronicID] FOREIGN KEY ([ChronicID]) REFERENCES [Chronics] ([ChronicID]) ON DELETE CASCADE,
        CONSTRAINT [FK_PatientChronics_Patients_PatientID] FOREIGN KEY ([PatientID]) REFERENCES [Patients] ([PatientID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Businesses] (
        [OrganizationID] int NOT NULL IDENTITY,
        [OrganizationName] nvarchar(50) NOT NULL,
        [NPONumber] nvarchar(10) NOT NULL,
        [AddressLine1] nvarchar(50) NOT NULL,
        [AddressLine2] nvarchar(50) NULL,
        [ConatactNumber] nvarchar(10) NOT NULL,
        [Email] nvarchar(20) NOT NULL,
        [OperatingHours] nvarchar(10) NOT NULL,
        [SuburbID] int NOT NULL,
        CONSTRAINT [PK_Businesses] PRIMARY KEY ([OrganizationID]),
        CONSTRAINT [FK_Businesses_Suburbs_SuburbID] FOREIGN KEY ([SuburbID]) REFERENCES [Suburbs] ([SuburbID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Contracts] (
        [ContractID] int NOT NULL IDENTITY,
        [ContractDate] datetime2 NOT NULL,
        [AddressLine1] nvarchar(50) NOT NULL,
        [AddressLine2] nvarchar(50) NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [WoundDesc] nvarchar(250) NOT NULL,
        [Status] nvarchar(10) NULL,
        [NurseID] int NULL,
        [PatientID] int NOT NULL,
        [SuburbID] int NOT NULL,
        CONSTRAINT [PK_Contracts] PRIMARY KEY ([ContractID]),
        CONSTRAINT [FK_Contracts_Nurses_NurseID] FOREIGN KEY ([NurseID]) REFERENCES [Nurses] ([NurseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Contracts_Patients_PatientID] FOREIGN KEY ([PatientID]) REFERENCES [Patients] ([PatientID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Contracts_Suburbs_SuburbID] FOREIGN KEY ([SuburbID]) REFERENCES [Suburbs] ([SuburbID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [PreferedSuburbs] (
        [PreferedSuburbID] int NOT NULL IDENTITY,
        [NurseID] int NOT NULL,
        [SuburbID] int NOT NULL,
        CONSTRAINT [PK_PreferedSuburbs] PRIMARY KEY ([PreferedSuburbID]),
        CONSTRAINT [FK_PreferedSuburbs_Nurses_NurseID] FOREIGN KEY ([NurseID]) REFERENCES [Nurses] ([NurseID]) ON DELETE CASCADE,
        CONSTRAINT [FK_PreferedSuburbs_Suburbs_SuburbID] FOREIGN KEY ([SuburbID]) REFERENCES [Suburbs] ([SuburbID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Schedules] (
        [ScheduleID] int NOT NULL IDENTITY,
        [ScheduleDate] datetime2 NOT NULL,
        [ApproxArriTime] time NOT NULL,
        [DepartTime] time NOT NULL,
        [ArriveTime] time NOT NULL,
        [WoundCondition] nvarchar(max) NULL,
        [Note] nvarchar(max) NULL,
        [NurseID] int NOT NULL,
        [ContractID] int NOT NULL,
        CONSTRAINT [PK_Schedules] PRIMARY KEY ([ScheduleID]),
        CONSTRAINT [FK_Schedules_Contracts_ContractID] FOREIGN KEY ([ContractID]) REFERENCES [Contracts] ([ContractID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Schedules_Nurses_NurseID] FOREIGN KEY ([NurseID]) REFERENCES [Nurses] ([NurseID]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE TABLE [Visits] (
        [VisitID] int NOT NULL IDENTITY,
        [VistDate] datetime2 NOT NULL,
        [ApproxArriTime] time NOT NULL,
        [DepartTime] time NOT NULL,
        [ArriveTime] time NOT NULL,
        [WoundCondition] nvarchar(max) NULL,
        [Note] nvarchar(max) NULL,
        [NurseID] int NOT NULL,
        [ContractID] int NOT NULL,
        CONSTRAINT [PK_Visits] PRIMARY KEY ([VisitID]),
        CONSTRAINT [FK_Visits_Contracts_ContractID] FOREIGN KEY ([ContractID]) REFERENCES [Contracts] ([ContractID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Visits_Nurses_NurseID] FOREIGN KEY ([NurseID]) REFERENCES [Nurses] ([NurseID]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Admins_ApplicationUserId] ON [Admins] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Businesses_SuburbID] ON [Businesses] ([SuburbID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Contracts_NurseID] ON [Contracts] ([NurseID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Contracts_PatientID] ON [Contracts] ([PatientID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Contracts_SuburbID] ON [Contracts] ([SuburbID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Nurses_ApplicationUserId] ON [Nurses] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_OfficeManagers_ApplicationUserId] ON [OfficeManagers] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_PatientChronics_ChronicID] ON [PatientChronics] ([ChronicID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_PatientChronics_PatientID] ON [PatientChronics] ([PatientID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Patients_ApplicationUserId] ON [Patients] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_PreferedSuburbs_NurseID] ON [PreferedSuburbs] ([NurseID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_PreferedSuburbs_SuburbID] ON [PreferedSuburbs] ([SuburbID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Schedules_ContractID] ON [Schedules] ([ContractID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Schedules_NurseID] ON [Schedules] ([NurseID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Suburbs_CityID] ON [Suburbs] ([CityID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Visits_ContractID] ON [Visits] ([ContractID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    CREATE INDEX [IX_Visits_NurseID] ON [Visits] ([NurseID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104081637_add1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231104081637_add1', N'5.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104084944_Add2')
BEGIN
    ALTER TABLE [OfficeManagers] ADD [Status] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104084944_Add2')
BEGIN
    ALTER TABLE [Nurses] ADD [Status] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104084944_Add2')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Businesses]') AND [c].[name] = N'Email');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Businesses] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Businesses] ALTER COLUMN [Email] nvarchar(50) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104084944_Add2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231104084944_Add2', N'5.0.17');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231104101231_add3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231104101231_add3', N'5.0.17');
END;
GO

COMMIT;
GO

