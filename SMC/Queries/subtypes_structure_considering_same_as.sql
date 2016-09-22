-- select que traz a estrutura dos subtipos, considerando o uso do campo 'same_as_subtype'
-- Fabricio Kucinskis, 10/05/2012

select 
	a.service_type,
	a.service_name,
	b.service_subtype,
	b.description,
	b.allow_repetition,
	case when b.is_request = 1 then 1 else 0 end as is_request,
	case when b.is_request = 0 then 1 else 0 end as is_report,
	d.read_only,
	isnull(convert(varchar, d.default_value), '(None)') as default_value,
	e.data_field_id,
	e.data_field_name,
	case when e.variable_length = 1 then 'Variable' else CONVERT(varchar, e.number_of_bits) end as number_of_bits
from 
	services a 
	inner join subtypes b on 
				a.service_type = b.service_type 
	inner join subtype_structure c on 
				b.service_type = c.service_type and 
				b.service_subtype = c.service_subtype and
				c.position = (select MIN(x.position) from subtype_structure x where x.service_type = c.service_type and x.service_subtype = c.service_subtype)
	inner join subtype_structure d on
				c.service_type  = d.service_type and
				isnull(c.same_as_subtype, c.service_subtype) = d.service_subtype
	inner join data_fields e on
				d.data_field_id = e.data_field_id
order by
	a.service_type,
	b.service_subtype,
	d.position