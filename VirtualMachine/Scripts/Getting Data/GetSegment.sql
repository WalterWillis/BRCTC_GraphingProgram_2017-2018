Use Main
Go

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSegment')
DROP PROCEDURE GetSegment
GO

Create Procedure GetSegment
@tableName nvarchar(max),
@targetColumnName nvarchar(max),
@whereColumnModifier nvarchar(max),
@startIndex nvarchar(max),
@endIndex nvarchar(max),
@orderBy nvarchar(max)
AS

SET NOCOUNT ON;

Declare @select nvarchar(max)
Declare @where nvarchar(max)
Declare @sqlCommand nvarchar(max)

set @select = 'select ' + @targetColumnName + ' from ' + @tableName;

set @where = ' where ' + @whereColumnModifier + ' between ' + @startIndex + ' and ' + @endIndex;

set @orderBy = 'order by ' + @orderBy;

set @sqlCommand = @select + @where + @orderBy

exec (@sqlCommand)

GO

Grant EXECUTE on GetSegment
TO Walter, DataMember
GO

--exec GetSegment 'FirstImport','SupplyVolts,ID','ID','500', '600'



