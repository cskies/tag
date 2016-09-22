/** 
  * Esta query lista em detalhes as sessoes de testes, relacionando os acks aos pacotes de origem. 
  * Antes de rodar, troque o valor de session_id na clausula Where das ultimas linhas.
  **/

select                    
    convert(varchar, b.log_time, 103) + ' ' + convert(varchar, b.log_time, 108) as [Log Time],
    case b.is_request when 1 then 'Request' else 'Report' end as [Packet Type],
    '[' + dbo.f_zero(c.apid, 4) + '] ' + isnull(c.application_name, 'invalid') as APID,
    b.ssc as SSC,
    '[' + dbo.f_zero(b.service_type, 4) + '] ' + isnull(d.service_name, 'invalid') as [Service Type],                    
    '[' + dbo.f_zero(b.service_subtype, 2) + '] ' + isnull(e.description, 'invalid') as [Service Subtype],
    case b.is_request when 1 then '[N/A]' else upper(master.dbo.fn_varbintohexstr(b.time_tag)) end as [Time Tag],
    case b.crc_error when 0 then 'OK' else 'Error' end as CRC,
    case b.is_request 
        when 0 then 
            '[N/A]' 
        else 
            case (convert(int, substring(master.dbo.fn_varbintohexstr(b.raw_packet), 16, 1)) & 1) 
                when 0 then 
                    'Not Asked' 
                else 
                    isnull((select top 1 
                                case service_subtype 
                                    when 1 then 
                                        'Success Ack' 
                                    else 
                                        'Failure Ack: 0x' + substring(master.dbo.fn_varbintohexstr(raw_packet), 42, 1) 
                                    end 
                            from 
                                packets_log 
                            where 
                                session_id = b.session_id and 
                                is_request = 0 and 
                                service_type = 1 and 
                                service_subtype in (1, 2) and 
                                substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 37, 2) =                                                                                                                                 substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.apid))), 3, 2) and 
                                /** obs: a clausula abaixo so funciona para sscs <= 255!!! **/
                                substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 41, 2) =                                                                                                                                 substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.ssc))), 3, 2)),
                            'Not Received')
                end 
            end as 'Reception Ack',
    case b.is_request 
        when 0 then 
            '[N/A]' 
        else
            case (convert(int, substring(master.dbo.fn_varbintohexstr(raw_packet), 16, 1)) & 8) when 0 then
                'Not Asked'
            else
                isnull((select top 1
                            case service_subtype
                                when 7 then
                                    'Success Ack'
                                else
                                     'Failure Ack: 0x' + substring(master.dbo.fn_varbintohexstr(raw_packet), 42, 1)
                                end
                        from
                            packets_log
                        where
                            session_id = b.session_id and
                            is_request = 0 and
                            service_type = 1 and
                            service_subtype in (7, 8) and
                            substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 37, 2) =                                                                     substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.apid))), 3, 2) and
                            /** obs: a clausula abaixo so funciona para sscs <= 255!!! **/         
                            substring(upper(master.dbo.fn_varbintohexstr(raw_packet)), 41, 2) =                                                                                        substring(upper(master.dbo.fn_varbintohexstr(convert(binary(1), b.ssc))), 3, 2)),
                        'Not Received')
            end 
        end as 'Execution Ack'
from                     
    sessions a
    inner join packets_log b on
                    a.session_id = b.session_id
    left join apids c on
                    b.apid = c.apid
    left join services d on
                    b.service_type = d.service_type
    left join subtypes e on
                    b.service_type = e.service_type and
                    b.service_subtype = e.service_subtype
where 
    a.session_id = 1 /** troque o session_id aqui !!! **/
order by 
    b.unique_log_id