

IF NOT EXISTS (SELECT NULL FROM sys.procedures p JOIN sys.schemas s ON p.schema_id = s.schema_id WHERE s.name = 'web' AND p.name = 'OrderProduct_Get') BEGIN
	EXEC('Create PROCEDURE [web].[OrderProduct_Get]
 @Filter varchar(50) = NULL
AS
BEGIN
       SELECT cl.[ClientID],cl.[ClientName],c.[CountryID],
	c.[CountryName],o.[OrderID],o.[OrderTime],o.[PlannedDate]
	,st.[StatusID],st.[StatusName],u.[FirstName] + '' '' + u.[LastName] as UserFullName,u.UserName,u.UserID
	,o.OrderNo,(SELECT COUNT(*) FROM [web].[OrderProduct] op WHERE op.[OrderID] = o.[OrderID]) as ProductCount
	FROM [web].[Order] as o 
	INNER JOIN web.Client cl on cl.ClientID = o.[ClientID]
	INNER JOIN web.Country c on c.CountryID = o.[CountryID]
	INNER JOIN [web].[Status] st on st.[StatusID] = o.[StatusID]
	INNER JOIN [authentication].[User] u ON u.UserID = o.UserID
	WHERE o.[OrderTime] >= DATEADD(MONTH,-1,GETDATE()) AND o.[OrderTime] <=GETDATE()
	AND (@Filter IS NULL OR C.CountryName LIKE @Filter + ''%'')END');

	END