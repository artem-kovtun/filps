CREATE PROCEDURE [file].[procDeleteFile]
    @id        NVARCHAR(64)
AS
BEGIN
    DELETE FROM [file].[File] WHERE [Id] = @id;
END;