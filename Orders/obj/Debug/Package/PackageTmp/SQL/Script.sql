

IF NOT EXISTS (SELECT NULL FROM sys.procedures p JOIN sys.schemas s ON p.schema_id = s.schema_id WHERE s.name = 'web' AND p.name = 'ProductOrder_Get') BEGIN
	EXEC('Create PROCEDURE web.ProductOrder_Get
	@OrderId INT = NULL
AS
BEGIN
      	SELECT p.[ProductName],op.[CreationDate],o.[OrderNo],u.[FirstName] + '' '' + u.[LastName] as FullName,
		   pt.[ProductTypeName]  FROM [web].[OrderProduct] op
           INNER JOIN [web].[Product] p ON p.ProductID = op.ProductID
		   INNER JOIN [web].[Order] o ON o.OrderID = op.OrderID
		   INNER JOIN [authentication].[User] u ON u.UserID = o.UserID
		   INNER JOIN [web].[ProductType] pt ON pt.ProductTypeID = p.ProductTypeID
		   WHERE @OrderId IS NULL OR @OrderId = o.OrderID END');

	END

	IF NOT EXISTS (SELECT NULL FROM sys.procedures p JOIN sys.schemas s ON p.schema_id = s.schema_id WHERE s.name = 'web' AND p.name = 'Order_Save')
 BEGIN
	EXEC('Create PROCEDURE [web].[Order_Save]
	@OrderId INT = NULL,
	@OrderTime DATETIME,
	@UserID INT,
	@OrderNo INT,
	@StatusID INT,
	@CountryID INT,
	@ClientID INT,
	@PlannedDate DATETIME,
	@IsActive BIT
AS
BEGIN
	IF @OrderId IS NULL
	BEGIN
	INSERT INTO [web].[Order] VALUES(@OrderTime,@UserID,@OrderNo,@StatusID,@CountryID,@ClientID,
	@PlannedDate,@IsActive)
	SELECT IDENT_CURRENT(''web.Order'')
	END
	ELSE
	BEGIN
	UPDATE [web].[Order] SET OrderTime = @OrderTime, UserID = @UserID, OrderNo = @OrderNo,
	StatusID = @StatusID, CountryID = @CountryID, ClientID = @ClientID, PlannedDate = @PlannedDate,	IsActive = @IsActive WHERE OrderID = @OrderId END END');

	END

	
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