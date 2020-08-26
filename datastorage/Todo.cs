using System.Collections.Generic;

namespace csharp_mvc
{

    public class Todo
    {

        private List<Goal> goals = new List<Goal>();

        public void AddGoal(Goal goal)
        {
            this.goals.Add(goal);
        }

        public List<Goal> GetGoals()
        {
            return this.goals;
        }
    }
}