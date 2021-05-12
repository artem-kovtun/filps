CREATE PROCEDURE [file].[procGetUserFiles]
    @userEmail NVARCHAR(128),
    @page INT = 1,
    @take INT = 10,
    @search NVARCHAR(MAX)
AS
BEGIN

    SELECT
        [Id],
        [Name] ,
        IIF([PinnedOn] IS NOT NULL, 1, 0) AS [IsPinned],
        [AccessedOn],
        COUNT(*) OVER() [Total]
    INTO #UserFiles
    FROM [file].[File]
    WHERE [CreatedBy] = @userEmail AND [Name] LIKE '%' + @search + '%'
    GROUP BY [Id], [Name],[PinnedOn],[AccessedOn]
    ORDER BY IIF([PinnedOn] IS NOT NULL, 1, 0) DESC, [PinnedOn] DESC, [AccessedOn] DESC
    OFFSET (@page - 1) * @take ROWS FETCH NEXT @take ROWS ONLY;

    SELECT
        (SELECT TOP 1 [Total] FROM #UserFiles) as Total,
        (SELECT [Id],
                [Name],
                [IsPinned],
                [AccessedOn]
        FROM #UserFiles FOR JSON PATH) Items
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;

    DROP TABLE #UserFiles;

END;