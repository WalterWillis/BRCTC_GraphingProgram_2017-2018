use Main

Create Table May18PCBTest
(
ID int IDENTITY(1,1) PRIMARY KEY,
SupplyVolts float,
PiezoX float,
PiezoY float,
PiezoZ float,
Seconds int
);
GO

Grant SELECT, insert On dbo.May18PCBTest
TO Walter,DataMember

--Drop Table May18PCBTest