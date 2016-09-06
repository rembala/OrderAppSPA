

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
		   WHERE @OrderId IS NULL OR @OrderId = o.OrderID');

	END