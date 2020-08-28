using System.Collections.Generic;
using System.Data.Common;
using Npgsql;

namespace csharp_mvc
{

    public class GoalRepository
    {

        private GoalRepository() { }

        private static GoalRepository instance;

        public static GoalRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new GoalRepository();
            }
            return instance;
        }

        public List<Goal> GetAllGoals()
        {
            List<Goal> goals = new List<Goal>();
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select * from tasks", connection);

                using (var readed = query.ExecuteReader())
                {
                    while (readed.Read())
                    {
                        goals.Add(new Goal()
                        {
                            name = readed.GetString(1),
                            done = readed.GetBoolean(2)
                        });
                    }
                }
            }
            return goals;
        }

        public void SaveGoal(Goal goal)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (var writer = new NpgsqlCommand("inserto into tasks (name, done) values(@name, @done)"))
                {
                    writer.Parameters.AddWithValue("name", goal.GetName());
                    writer.Parameters.AddWithValue("done", goal.isDone());
                    writer.ExecuteNonQuery();
                }
            }
        }

    }

}