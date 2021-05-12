CREATE PROCEDURE [file].[procToggleFilePin]
    @id NVARCHAR(64)
AS
BEGIN

    UPDATE [file].[File]
    SET [PinnedOn] = IIF([PinnedOn] is not null, null, current_timestamp)
    WHERE [Id] = @id;

END;