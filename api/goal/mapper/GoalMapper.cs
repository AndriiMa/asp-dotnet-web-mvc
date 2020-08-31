using System.Collections.Generic;
using System.Linq;

namespace csharp_mvc
{

    public class GoalMapper
    {

        public GoalDto MapToDto(Goal entity)
        {
            GoalDto dto = new GoalDto();
            dto.id = entity.GetId();
            dto.name = entity.GetName();
            dto.done = entity.IsDone();
            dto.scheduleId = entity.GetScheduleId();

            return dto;
        }

        public Goal MapToObject(GoalDto dto)
        {
            Goal goal = new Goal(
                dto.id,
                dto.name,
                dto.done,
                dto.scheduleId
            );

            return goal;
        }

        public List<GoalDto> MapToList(List<Goal> entityes)
        {
            List<GoalDto> dtos = new List<GoalDto>();
            dtos = entityes.Select(e => MapToDto(e))
            .ToList();

            return dtos;
        }

    }
}