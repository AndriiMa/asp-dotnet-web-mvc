using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace csharp_mvc
{
    [Route("/tasks")]
    [ApiController]
    public class GoalController : Controller
    {

        [HttpGet("")]
        public IEnumerable<Goal> GetGoals()
        {
            TodoDto dto = new TodoDto();
            dto.goals = Todo.GetGoals();
            return dto.goals;
        }

        [HttpPost("")]
        public void CreateNewTask([FromBody]Goal goal)
        {
            Todo.AddGoal(goal);
        }

    }

}