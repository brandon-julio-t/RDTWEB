select u.Id, UserName, r.Id, r.Name from
	AspNetUsers u
		full join AspNetUserRoles ur on u.Id = ur.UserId
		full join AspNetRoles r on r.Id = ur.RoleId

go 

select * from AspNetUsers