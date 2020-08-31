using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_mvc
{
    public class ScheduleMapper
    {
        public ScheduleDto MapToDto(Schedule obj)
        {
            ScheduleDto dto = new ScheduleDto();
            dto.id = obj.GetId();
            dto.name = obj.GetName();
            dto.description = obj.GetDescription();

            return dto;
        }

        public Schedule MapToObject(ScheduleDto dto)
        {
            Schedule obj = new Schedule(
                dto.id,
                dto.name,
                dto.description
            );
            return obj;

        }

        public List<ScheduleDto> MapToList(List<Schedule> entityes)
        {
            List<ScheduleDto> dtos = new List<ScheduleDto>();
            dtos = entityes.Select(e => MapToDto(e))
            .ToList();

            return dtos;
        }
    }
}