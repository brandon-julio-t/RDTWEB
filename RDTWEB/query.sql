use master
go
drop database if exists rdt
go
create database rdt
go
use rdt

select u.Id, UserName, r.Id, r.Name from
	AspNetUsers u
		full join AspNetUserRoles ur on u.Id = ur.UserId
		full join AspNetRoles r on r.Id = ur.RoleId

go 

select distinct qs.*
from Answers a
	join Questions q on a.QuestionId = q.Id
	join QuestionSets qs on q.QuestionSetId = qs.Id
select * from Questions
select * from Answers
delete from answers where QuestionId = 10