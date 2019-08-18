--/****** Script for SelectTopNRows command from SSMS  ******/
--declare @avgFactor int = 7775; --7775 is the calculate amount of data per second
--declare @currentPos int = @avgFactor;
--declare @Xaverage float = 0;
--declare @Yaverage float = 0;
--declare @Zaverage float = 0;
--declare @Xaddition float = 0;
--declare @Yaddition float = 0;
--declare @Zaddition float = 0;
--declare @Supply float = 0;
--declare @Seconds int = 0;
--declare @count int = 1;

--declare @endPos int = (select max(ID) FROM [Main].[dbo].[June21LaunchData] ) 

--while @currentPos <= @endPos
--BEGIN
--while @count < @currentPos

--BEGIN
--set @Supply = @Supply + (SELECT
--      SupplyVolts
--  FROM [Main].[dbo].[June21LaunchData] 
--  where id = @count
--  )
--set @Xaddition = @Xaddition + (SELECT
--      [PiezoX]
--  FROM [Main].[dbo].[June21LaunchData] 
--  where id = @count
--  )
--  set @Yaddition = @Yaddition + (SELECT
--      [PiezoY]
--  FROM [Main].[dbo].[June21LaunchData] 
--  where id = @count
--  )
--set @Zaddition = @Zaddition + (SELECT
--      [PiezoZ]
--  FROM [Main].[dbo].[June21LaunchData] 
--  where id = @count
--  )
--  set @Seconds =(SELECT
--      Seconds
--  FROM [Main].[dbo].[June21LaunchData] 
--  where id = @count
--  )

--  set @count = @count + 1
--END
--set @Xaverage = @Xaddition / @avgFactor
--set @Yaverage = @Yaddition / @avgFactor
--set @Zaverage = @Zaddition / @avgFactor
--set @Supply = @Supply / @avgFactor

--  Insert into dbo.TrimmedLaunchData2018 (SupplyVolts,PiezoX,PiezoY, PiezoZ,Seconds)
--  Values(@Supply,@Xaverage,@Yaverage,@Zaverage,@Seconds)

--set @count = @currentPos;
--set @currentPos = @currentPos + @avgFactor;
--set @Xaverage = 0;
--set @Yaverage = 0;
--set @Zaverage = 0;
--set @Xaddition = 0;
--set @Yaddition = 0;
--set @Zaddition = 0;
--set @Supply = 0;
--set @Seconds = 0;

--END

declare @dataPerSecond int = 7775; --7775 is the calculate amount of data per second
declare @currentPos int = @dataPerSecond;
declare @endPos int = (select max(ID) FROM [Main].[dbo].[June21LaunchData] );
declare @count int = 1;

while @currentPos < @endPos
begin
Insert into dbo.TrimmedLaunchData2018 (SupplyVolts, PiezoXMax, PiezoXAvg, PiezoXMin, PiezoYMax, PiezoYAvg, PiezoYMin, PiezoZMax, PiezoZAvg, PiezoZMin, Seconds)
  select avg(SupplyVolts) as SupplyVolts, max(PiezoX) as PiezoXMax, avg(PiezoX) as PiezoXAvg, Min(PiezoX) as PiezoXMin,
   max(PiezoY) as PiezoYMax, avg(PiezoY) as PiezoYAvg, Min(PiezoY) as PiezoYMin,
    max(PiezoZ) as PiezoZMax, avg(PiezoZ) as PiezoZAvg,Min(PiezoZ) as PiezoZMin, max(Seconds) as Seconds
  from [Main].[dbo].[June21LaunchData] 
  where ID > @count and ID <= @currentPos

  set @count = @currentPos
  set @currentPos = @currentPos + @dataPerSecond
end
