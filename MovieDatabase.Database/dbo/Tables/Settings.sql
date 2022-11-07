CREATE TABLE [dbo].[Settings] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [CreatedOn]  DATETIME2 (7)  NOT NULL,
    [DeletedOn]  DATETIME2 (7)  NULL,
    [IsDeleted]  BIT            NOT NULL,
    [ModifiedOn] DATETIME2 (7)  NULL,
    [Name]       NVARCHAR (MAX) NULL,
    [Value]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Settings_IsDeleted]
    ON [dbo].[Settings]([IsDeleted] ASC);

