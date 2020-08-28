using System;

namespace csharp_mvc
{

    public class GoalDto
    {
        public int id { get; set; }
        public String name { get; set; }
        public bool done { get; set; }

        public GoalDto()
        {
        }

        public String GetName()
        {
            return this.name;
        }

        public bool isDone()
        {
            return this.done;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public void SetDone(bool done)
        {
            this.done = done;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public int GetId(int id)
        {
            return this.id;
        }
    }

}