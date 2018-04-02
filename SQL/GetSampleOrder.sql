/****** Object:  StoredProcedure [dbo].[CreateSampleOrder]    Script Date: 02/04/2018 19:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetSampleOrder]
(
@OrderId varchar(50)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON
	IF(@OrderId IS NULL OR @OrderId = '')
	select * from sampleorder 
	ELSE
	 select * from sampleorder where orderid= @orderId;
END
