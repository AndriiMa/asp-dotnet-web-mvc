using System;

namespace csharp_mvc
{
    public class Goal
    {
        private String name;
        private bool done;

        public Goal(string name, bool done)
        {
            this.name = name;
            this.done = done;
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