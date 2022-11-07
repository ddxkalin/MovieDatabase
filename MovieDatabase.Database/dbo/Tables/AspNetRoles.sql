CREATE TABLE [dbo].[AspNetRoles] (
    [Id]               NVARCHAR (450) NOT NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [CreatedOn]        DATETIME2 (7)  NOT NULL,
    [DeletedOn]        DATETIME2 (7)  NULL,
    [IsDeleted]        BIT            NOT NULL,
    [ModifiedOn]       DATETIME2 (7)  NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoles_IsDeleted]
    ON [dbo].[AspNetRoles]([IsDeleted] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);

