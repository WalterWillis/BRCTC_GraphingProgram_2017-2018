use Main

Create Table June15PreIntegrationTest
(
ID int IDENTITY(1,1) PRIMARY KEY,
SupplyVolts float,
PiezoX float,
PiezoY float,
PiezoZ float,
Seconds int
);
GO

Grant SELECT, insert On dbo.June15PreIntegrationTest
TO Walter,DataMember

--Drop Table June15PreIntegrationTest