CREATE TABLE [dbo].[Review] (
	[SystemId] INT IDENTITY (1, 1) NOT NULL ,
	[ReviewId] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	[RestaurantId] UNIQUEIDENTIFIER NOT NULL,
	[UserName] NVARCHAR(100) NOT NULL,
	[Rating] INT NOT NULL,
	[Details] NVARCHAR(4000) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Review_ReviewId] PRIMARY KEY NONCLUSTERED ([ReviewId] ASC));

GO;

CREATE UNIQUE CLUSTERED INDEX [CIX_Review_SystemId] ON [dbo].[Review](SystemId);

GO;