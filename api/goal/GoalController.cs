using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace csharp_mvc
{
    [Route("/tasks")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private GoalRepository goalRepository = GoalRepository.GetInstance();
        private GoalMapper goalMapper = new GoalMapper();

        [HttpGet("")]
        public List<GoalDto> GetGoals()
        {
            List<Goal> entyties = goalRepository.GetAllGoals();

            return goalMapper.MapToList(entyties);
        }

        [HttpPost("")]
        public GoalDto CreateNewGoal([FromBody] GoalDto dto)
        {
            Goal goal = new Goal(dto.name, dto.done);
            Goal savedGoal = goalRepository.SaveNewGoal(goal);
            return goalMapper.MapToDto(savedGoal);
        }

        [HttpPost("/{id}")]
        public void UpdateGoal(int id, [FromBody] GoalDto dto){
            Goal goal = new Goal(id, dto.name, dto.done);
            goalRepository.UpdateGoal(goal);
        }

        [HttpDelete("/{id}")]
        public void DeleteGoalById(int id)
        {
            goalRepository.DeleteGoalById(id);
        }

        [HttpGet("/{id}")]
        public GoalDto GetGoalById(int id)
        {
            Goal goal = goalRepository.GetGoalById(id);
            return goalMapper.MapToDto(goal);
        }
    }

}