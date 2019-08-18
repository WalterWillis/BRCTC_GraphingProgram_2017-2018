
  use Main
  go


  select stdev(PiezoX) as PiezoXDeviation, stdev(PiezoY) as PiezoYDeviation, stdev(PiezoZ) as PiezoZDeviation
  from May18PCBTest

  select max(PiezoX) as PiezoXMax, avg(PiezoX) as PiezoXAvg, Min(PiezoX) as PiezoXMin,
   max(PiezoY) as PiezoYMax, avg(PiezoY) as PiezoYAvg, Min(PiezoY) as PiezoYMin,
    max(PiezoZ) as PiezoZMax, avg(PiezoZ) as PiezoZAvg,Min(PiezoZ) as PiezoZMin
  from May18PCBTest