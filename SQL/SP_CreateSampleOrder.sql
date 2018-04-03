/****** Object:  StoredProcedure [dbo].[CreateSampleOrder]    Script Date: 02/04/2018 21:03:06 ******/
DROP PROCEDURE [dbo].[CreateSampleOrder]
GO

/****** Object:  StoredProcedure [dbo].[CreateSampleOrder]    Script Date: 02/04/2018 21:03:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE   PROCEDURE [dbo].[CreateSampleOrder]
(
@OrderRef UNIQUEIDENTIFIER,
@CustomerId varchar(50),
@Price DECIMAL(4,2),
@Currency varchar(50)    
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON    
	insert into dbo.sampleorder( OrderRef, CustomerId, Price, Currency) values (@OrderRef, @CustomerId, @Price, @Currency) 
	select * from sampleorder where orderref = @OrderRef;

END
GO
