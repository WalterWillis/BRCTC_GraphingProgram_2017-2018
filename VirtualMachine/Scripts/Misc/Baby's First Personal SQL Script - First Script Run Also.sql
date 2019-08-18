create table #temp(
dataA int,
dataB varchar(max)
)
declare @val int = 0;
while @val < 300
BEGIN
INSERT #temp (dataA,dataB)
values(@val, CONVERT(varchar(max), @val))
set @val = @val + 1
END

select * from #temp

drop table #temp