using System;

namespace csharp_mvc
{
    public class Goal
    {
        public String name{get; set;}
        public bool done{get;set;}

        
        public Goal(string name, bool done)
        {
            this.name = name;
            this.done = done;
        }

        public Goal()
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