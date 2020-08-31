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

        public List<Goal> GetAll()
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
                            reader.GetBoolean(2),
                            reader.GetInt32(3)));
                    }
                }
            }
            return goals;
        }

        public Goal SaveNew(Goal goal)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into tasks (task_name, done, schedule_id) values(@task_name, @done, @schedule_id) returning id", connection))
                {
                    cmd.Parameters.AddWithValue("task_name", goal.GetName());
                    cmd.Parameters.AddWithValue("done", goal.IsDone());
                    cmd.Parameters.AddWithValue("schedule_id", goal.IsDone());
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




        public void DeleteById(int id)
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

        public Goal GetById(int id)
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
                    reader.GetBoolean(2),
                    reader.GetInt32(3));

                }
            }
            return goal;
        }

        public void Update(Goal goal)
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

        public List<Goal> GetByScheduleId(int id)
        {
            List<Goal> goals = new List<Goal>();
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select id, task_name, done from tasks where schedule_id = @schedule_id", connection);
                query.Parameters.AddWithValue("schedule_id", id);
                using (NpgsqlDataReader reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        goals.Add(new Goal(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetBoolean(2),
                        reader.GetInt32(3)));
                    }
                }
            }
            return goals;
        }

        public void DeleteByScheduleId(int id)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (var writer = new NpgsqlCommand("delete from tasks where schedule_id = @schedule_id", connection))
                {
                    writer.Parameters.AddWithValue("schedule_id", id);
                    writer.ExecuteNonQuery();
                }
            }
        }
    }

}