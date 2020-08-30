using System.Collections.Generic;
using Npgsql;

namespace csharp_mvc
{

    public class ScheduleRepository
    {

        private ScheduleRepository() { }

        private static ScheduleRepository instance;

        public static ScheduleRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ScheduleRepository();
            }
            return instance;
        }


        public List<Schedule> GetAll()
        {
            List<Schedule> schedules = new List<Schedule>();
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select * from schedules", connection);

                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        schedules.Add(new Schedule(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2)));
                    }
                }
            }
            return schedules;
        }

        public Schedule SaveNew(Schedule schedule)

        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into schedules (name, description) values(@name, @description) returning id", connection))
                {
                    cmd.Parameters.AddWithValue("name", schedule.GetName());
                    cmd.Parameters.AddWithValue("description", schedule.GetDescription());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            schedule.SetId(reader.GetInt32(0));
                        }
                    }
                }

            }
            return schedule;
        }


        public void DeleteById(int id)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (var writer = new NpgsqlCommand("delete from schedules where id = @id", connection))
                {
                    writer.Parameters.AddWithValue("id", id);
                    writer.ExecuteNonQuery();
                }
            }
        }

        public Schedule GetById(int id)
        {
            Schedule schedule;
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select id, name, description from schedules where id = @id", connection);
                query.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = query.ExecuteReader())
                {
                    reader.Read();
                    schedule = new Schedule(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2));

                }
            }
            return schedule;
        }

        public void Update(Schedule schedule)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("update schedules set name = @name, description= @description where id = @id", connection);
                query.Parameters.AddWithValue("id", schedule.GetId());
                query.Parameters.AddWithValue("task_name", schedule.GetName());
                query.Parameters.AddWithValue("description", schedule.GetDescription());
                query.ExecuteNonQuery();
            }
        }
    }

}