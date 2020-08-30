using System;

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
    }
}