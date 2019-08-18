use Main

Create Table FirstImport
(
ID int IDENTITY(1,1) PRIMARY KEY,
SupplyVolts float,
PiezoX float,
PiezoY float,
PiezoZ float,
Seconds int
);
GO

Grant SELECT On dbo.FirstImport
TO Walter

--Drop Table FirstImport