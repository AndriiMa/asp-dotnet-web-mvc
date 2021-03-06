using System;

namespace csharp_mvc
{
    public class TodoItem
    {
        private int id { get; set; }
        private String name { get; set; }
        private bool done { get; set; }

        private int scheduleId { get; set; }

        public TodoItem(string name, bool done, int scheduleId)
        {
            this.name = name;
            this.done = done;
            this.scheduleId = scheduleId;
        }

        public TodoItem(int id, string name, bool done, int scheduleId)
        {
            this.id = id;
            this.name = name;
            this.done = done;
            this.scheduleId = scheduleId;
        }

        public String GetName()
        {
            return this.name;
        }

        public bool IsDone()
        {
            return this.done;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public void setDone(bool done)
        {
            this.done = done;
        }

        public int GetId()
        {
            return this.id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }
        public int GetTodoListId()
        {
            return this.scheduleId;
        }

        public void SetTodoListId(int scheduleId)
        {
            this.scheduleId = scheduleId;
        }
    }
}