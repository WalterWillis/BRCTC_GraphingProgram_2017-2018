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
