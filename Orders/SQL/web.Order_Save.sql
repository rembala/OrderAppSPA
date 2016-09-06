

IF NOT EXISTS (SELECT NULL FROM sys.procedures p JOIN sys.schemas s ON p.schema_id = s.schema_id WHERE s.name = 'web' AND p.name = 'Order_Save') BEGIN
	EXEC('CREATE PROCEDURE web.Order_Save
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
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @OrderId IS NULL
	BEGIN
	INSERT INTO [web].[Order] VALUES(@OrderTime,@UserID,@OrderNo,@StatusID,@CountryID,@ClientID,
	@PlannedDate,@IsActive)
	SELECT IDENT_CURRENT(''web.Order'')''
	END
	ELSE
	BEGIN
	UPDATE [web].[Order] SET OrderTime = @OrderTime, UserID = @UserID, OrderNo = @OrderNo,
			StatusID = @StatusID, CountryID = @CountryID, ClientID = @ClientID, PlannedDate = @PlannedDate,
			IsActive = @IsActive WHERE OrderID = @OrderId
	END
END
GO');

	END