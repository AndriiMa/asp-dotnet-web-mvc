using System.Collections.Generic;
using Npgsql;

namespace csharp_mvc
{

    public class TodoListRepository
    {

        private TodoListRepository() { }

        private static TodoListRepository instance;

        public static TodoListRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TodoListRepository();
            }
            return instance;
        }


        public List<TodoList> GetAll()
        {
            List<TodoList> todoLists = new List<TodoList>();
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select * from schedules", connection);

                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todoLists.Add(new TodoList(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2)));
                    }
                }
            }
            return todoLists;
        }

        public TodoList SaveNew(TodoList todolist)

        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    "insert into schedules (name, description) values(@name, @description) returning id", connection))
                {
                    cmd.Parameters.AddWithValue("name", todolist.GetName());
                    cmd.Parameters.AddWithValue("description", todolist.GetDescription());
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            todolist.SetId(reader.GetInt32(0));
                        }
                    }
                }

            }
            return todolist;
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

        public TodoList GetById(int id)
        {
            TodoList todoList;
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("select id, name, description from schedules where id = @id", connection);
                query.Parameters.AddWithValue("id", id);
                using (NpgsqlDataReader reader = query.ExecuteReader())
                {
                    reader.Read();
                    todoList = new TodoList(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2));

                }
            }
            return todoList;
        }

        public void Update(TodoList todoList)
        {
            using (NpgsqlConnection connection = DatabaseService.CreateConnection())
            {
                connection.Open();
                NpgsqlCommand query = new NpgsqlCommand("update schedules set name = @name, description= @description where id = @id", connection);
                query.Parameters.AddWithValue("id", todoList.GetId());
                query.Parameters.AddWithValue("task_name", todoList.GetName());
                query.Parameters.AddWithValue("description", todoList.GetDescription());
                query.ExecuteNonQuery();
            }
        }
    }

}