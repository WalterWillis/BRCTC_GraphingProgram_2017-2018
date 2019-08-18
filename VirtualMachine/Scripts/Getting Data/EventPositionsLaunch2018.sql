select * from June21LaunchData where ID > 4167000 and ID < 5130000

select Seconds from June21LaunchData where ID > 1980000 and ID < 2200000 -- Amount of samples, by the estimated time SPS
select count(PiezoZ) from June21LaunchData where ID > 1980000 and ID < 2200000 -- Amount of samples, by the estimated time SPS
select Seconds from June21LaunchData where ID > 4167000 and ID < 5130000 -- Amount of samples, by the estimated time SPS
select count(PiezoZ) from June21LaunchData where PiezoZ > .7

select * from June21LaunchData where ID > 717516 and Seconds < 90 order by ID
select * from June21LaunchData where ID = 1431089
select * from June21LaunchData where ID < 717516

--insert into [First&SecondBurn2018]
--select SupplyVolts, PiezoX,PiezoY,PiezoZ,Seconds 
--from June21LaunchData where ID > 1980000 and ID < 2297300