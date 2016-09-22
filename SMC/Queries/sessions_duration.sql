-- Esta query traz as sessoes de teste ordenadas pela sua duracao, da maior para a menor

select 
    session_id as [Session ID],
    start_time as [Started At],
    end_time as [Finished At],
    datediff(second, start_time, end_time) as [Duration (in seconds)],
    connection_type as [Type]
from 
    sessions 
where
    connection_type <> 'file'
order by 
    datediff(second, start_time, end_time) desc
