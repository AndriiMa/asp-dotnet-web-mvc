using System.Collections.Generic;
using System.Linq;

namespace csharp_mvc
{

    public class GoalMapper
    {

        public GoalDto MapToDto(Goal entity)
        {
            GoalDto dto = new GoalDto();
            dto.SetId(entity.GetId());
            dto.SetName(entity.GetName());
            dto.SetDone(entity.IsDone());

            return dto;
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