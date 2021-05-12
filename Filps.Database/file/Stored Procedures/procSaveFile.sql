CREATE PROCEDURE [file].[procSaveFile]
    @id        NVARCHAR(64),
    @name      NVARCHAR(256),
    @userEmail NVARCHAR(128)
AS
BEGIN

    IF EXISTS(SELECT * FROM [file].[File] WHERE [Id] = @id)
        BEGIN
            UPDATE [file].[File]
            SET [Name] = @name,
                [AccessedOn] = GETUTCDATE()
            WHERE [Id] = @id
        END
    ELSE
        BEGIN
            INSERT INTO [file].[File] ([Id], [Name], [CreatedBy])
            VALUES (@id, @name, @userEmail)
        END

    SELECT @id;
END;