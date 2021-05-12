CREATE TABLE [file].[File] (
    [Id]         VARCHAR (64)   NOT NULL,
    [Name]       VARCHAR (256)  NOT NULL,
    [AccessedOn] DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]  NVARCHAR (128) NOT NULL,
    [DeletedOn]  DATETIME       NULL,
    [PinnedOn]   DATETIME       NULL,
    CONSTRAINT [File_pk] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

