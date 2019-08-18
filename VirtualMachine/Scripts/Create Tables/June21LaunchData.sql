use Main

Create Table June21LaunchData
(
ID int IDENTITY(1,1) PRIMARY KEY,
SupplyVolts float,
PiezoX float,
PiezoY float,
PiezoZ float,
Seconds int
);
GO

Grant SELECT, insert On dbo.June21LaunchData
TO Walter,DataMember

--Drop Table June21LaunchData