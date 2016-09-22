-- Esta query traz uma lista de todos os subtipos presentes em uma determinada
-- sessao de testes, e o numero de ocorrencias dela na sessao de testes

select distinct
	a.service_type as [Service Type],
	c.service_name as [Service],
	a.service_subtype as [Subtype],
	b.description as [Description],
	case when b.is_request = 1 then
		'Request'
	else
		'Report'
	end as [Packet Type],
	count(a.service_type) as [Number of Occurrences]
from 
	packets_log a
	inner join subtypes b on
		a.service_type = b.service_type and
		a.service_subtype = b.service_subtype
	inner join services c on
		a.service_type = c.service_type
where 
	a.session_id = 1
group by
	a.service_type,
	c.service_name,
	a.service_subtype,
	b.is_request,
	b.description
order by
	a.service_type,
	a.service_subtype
