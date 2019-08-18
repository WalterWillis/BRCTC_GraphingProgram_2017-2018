use Main

Create Table TrimmedLaunchData2018
(
ID int IDENTITY(1,1) PRIMARY KEY,
SupplyVolts float,
PiezoXMax float,
PiezoXAvg float, 
PiezoXMin float,
PiezoYMax float,
PiezoYAvg float, 
PiezoYMin float,
PiezoZMax float,
PiezoZAvg float, 
PiezoZMin float,
Seconds int
);
GO

Grant SELECT, insert On dbo.TrimmedLaunchData2018
TO Walter,DataMember

--Drop Table TrimmedLaunchData2018