using System;
using System.Collections.Generic;
using System.Data.Common;
using Npgsql;

namespace csharp_mvc
{

    public class TodoItemRepository
    {

        private TodoItemRepository() { }

        private static TodoItemRepository instance;

        public static TodoItemRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TodoItemRepository();
            }
            return instance;
        }

        public List<TodoItem> GetAll()
        {
            List<TodoItem> todoItems = new List<TodoItem>();
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select * from tasks", connection);

                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todoItems.Add(new TodoItem(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetBoolean(2),
                            reader.GetInt32(3)));
                    }
                }
            }
            return todoItems;
        }

        public TodoItem SaveNew(TodoItem todoItem)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into tasks (task_name, done, schedule_id) values(@task_name, @done, @schedule_id) returning id", connection))
                {
                    cmd.Parameters.AddWithValue("task_name", todoItem.GetName());
                    cmd.Parameters.AddWithValue("done", todoItem.IsDone());
                    cmd.Parameters.AddWithValue("schedule_id", todoItem.IsDone());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            todoItem.SetId(reader.GetInt32(0));
                        }
                    }
                }

            }
            return todoItem;
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

        public TodoItem GetById(int id)
        {
            TodoItem todoItem;
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select id, task_name, done from tasks where id = @id", connection);
                query.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = query.ExecuteReader())
                {
                    reader.Read();
                    todoItem = new TodoItem(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetBoolean(2),
                    reader.GetInt32(3));

                }
            }
            return todoItem;
        }

        public void Update(TodoItem todoItem)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("update tasks set task_name = @task_name, done= @done where id = @id", connection);
                query.Parameters.AddWithValue("id", todoItem.GetId());
                query.Parameters.AddWithValue("task_name", todoItem.GetName());
                query.Parameters.AddWithValue("done", todoItem.IsDone());

                query.ExecuteNonQuery();
            }

        }

        public List<TodoItem> GetByTodoListId(int id)
        {
            List<TodoItem> todoItems = new List<TodoItem>();
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select id, task_name, done from tasks where schedule_id = @schedule_id", connection);
                query.Parameters.AddWithValue("schedule_id", id);
                using (NpgsqlDataReader reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todoItems.Add(new TodoItem(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetBoolean(2),
                        reader.GetInt32(3)));
                    }
                }
            }
            return todoItems;
        }

        public void DeleteByTodoListId(int id)
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