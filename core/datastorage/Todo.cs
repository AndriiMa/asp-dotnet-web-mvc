using System.Collections.Generic;

namespace csharp_mvc
{

    public class Todo
    {

        private static List<Goal> goals = new List<Goal>();

        public static void AddGoal(Goal goal)
        {
            goals.Add(goal);
        }

        public static List<Goal> GetGoals()
        {
            return goals;
        }
    }
}