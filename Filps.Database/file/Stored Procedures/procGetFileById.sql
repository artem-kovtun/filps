CREATE PROCEDURE [file].[procGetFileById]
    @id NVARCHAR(64)
AS
BEGIN

    UPDATE [file].[File] SET [AccessedOn] = GETUTCDATE() WHERE [Id] = @id;

    SELECT
        [Id],
        [Name],
        IIF([PinnedOn] IS NOT NULL, 1, 0) [IsPinned],
        [AccessedOn]
    FROM [file].[File]
    WHERE [Id] = @id
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;

END;