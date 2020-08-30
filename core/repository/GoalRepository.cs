using System;
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

                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        goals.Add(new Goal(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetBoolean(2)));
                    }
                }
            }
            return goals;
        }

        public Goal SaveNewGoal(Goal goal)

        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into tasks (task_name, done) values(@task_name, @done) returning id", connection))
                {
                    cmd.Parameters.AddWithValue("task_name", goal.GetName());
                    cmd.Parameters.AddWithValue("done", goal.IsDone());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            goal.SetId(reader.GetInt32(0));
                        }
                    }
                }

            }
            return goal;
        }


        public void DeleteGoalById(int id)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (var writer = new NpgsqlCommand("delete from tasks where id = @id", connection))
                {
                    writer.Parameters.AddWithValue("id", id);
                    writer.ExecuteNonQuery();
                }
            }
        }

        public Goal GetGoalById(int id)
        {
            Goal goal;
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select id, task_name, done from tasks where id = @id", connection);
                query.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = query.ExecuteReader())
                {
                    reader.Read();
                    goal = new Goal(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetBoolean(2));

                }
            }
            return goal;
        }

        public void UpdateGoal(Goal goal)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("update tasks set task_name = @task_name, done= @done where id = @id", connection);
                query.Parameters.AddWithValue("id", goal.GetId());
                query.Parameters.AddWithValue("task_name", goal.GetName());
                query.Parameters.AddWithValue("done", goal.IsDone());

                query.ExecuteNonQuery();
            }

        }
    }

}