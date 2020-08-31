using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace csharp_mvc
{

    [Route("/lists")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private ScheduleRepository scheduleRepository = ScheduleRepository.GetInstance();
        private GoalRepository goalRepository = GoalRepository.GetInstance();
        private ScheduleMapper scheduleMapper = new ScheduleMapper();
        private GoalMapper goalMapper = new GoalMapper();

        [HttpGet("/{id}")]
        public ScheduleDto GetScheduleById(int id)
        {
            Schedule schedule = scheduleRepository.GetById(id);
            return scheduleMapper.MapToDto(schedule);
        }

        [HttpDelete("/{id}")]
        public void DeleteScheduleById(int id)
        {
            scheduleRepository.DeleteById(id);
            goalRepository.DeleteByScheduleId(id);
        }

        [HttpPost("")]
        public ScheduleDto SaveNewSchedule([FromBody] ScheduleDto dto)
        {
            Schedule newSchedule = scheduleMapper.MapToObject(dto);
            Schedule savedSchedule = scheduleRepository.SaveNew(newSchedule);
            return scheduleMapper.MapToDto(savedSchedule);
        }

        [HttpPost("/{id}")]
        public void UpdateScheduleById(int id, [FromBody] ScheduleDto dto)
        {
            Schedule schedule = scheduleMapper.MapToObject(dto);
            scheduleRepository.Update(schedule);
        }

        [HttpGet("/{id}/tasks")]
        public List<GoalDto> GetGoalsByScheduleId(int id)
        {
            List<Goal> goals = goalRepository.GetByScheduleId(id);
            return goalMapper.MapToList(goals);
        }

        [HttpDelete("/{id}/tasks")]
        public void DeleteByScheduleId(int id)
        {
            goalRepository.DeleteByScheduleId(id);
        }

        [HttpPost("/{id}/tasks")]
        public void CreateNewGoal(int id, GoalDto dto)
        {
            Goal goal = goalMapper.MapToObject(dto);
            goal.SetScheduleId(id);
            goalRepository.SaveNew(goal);
        }

    }

}