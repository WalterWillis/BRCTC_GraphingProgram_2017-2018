use Main

Create Table [First&SecondBurn2018]
(
ID int IDENTITY(1,1) PRIMARY KEY,
SupplyVolts float,
PiezoX float,
PiezoY float,
PiezoZ float,
Seconds int
);
GO

Grant SELECT, insert On dbo.[First&SecondBurn2018]
TO Walter,DataMember

--Drop Table [First&SecondBurn2018]