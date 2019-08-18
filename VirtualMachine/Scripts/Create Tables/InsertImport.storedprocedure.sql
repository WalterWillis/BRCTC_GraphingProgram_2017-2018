Use Main
Go
Create Procedure InsertImport
(
	@Data dbo.InsertImport readonly
) as
begin
insert into FirstImport (SupplyVolts, PiezoX, PiezoY, PiezoZ, Seconds)
select * from @Data
end

--drop procedure InsertImport