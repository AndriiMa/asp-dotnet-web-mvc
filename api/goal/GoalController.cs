using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace csharp_mvc
{
    [Route("/tasks")]
    [ApiController]
    public class GoalController : Controller
    {
        private GoalRepository goalRepository = GoalRepository.GetInstance();

        [HttpGet("")]
        public TodoDto GetGoals()
        {
            TodoDto dto = new TodoDto();
            dto.goals = goalRepository.GetAllGoals();
            return dto;
        }

        [HttpPost("")]
        public void CreateNewTask([FromBody] GoalDto dto)
        {
            Goal goal = new Goal(dto.name, dto.done);
            goalRepository.SaveGoal(goal);
        }
    }

}