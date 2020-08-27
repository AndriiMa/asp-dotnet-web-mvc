using System;

namespace csharp_mvc
{

    public class GoalDto
    {

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

        public void setDone(bool done)
        {
            this.done = done;
        }
    }

}