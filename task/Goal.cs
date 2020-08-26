using System;

namespace csharp_mvc
{
    public class Goal
    {
        private String name{set; get;}
        private bool isDone;

        public Goal(string name, bool isDone)
        {
            this.name = name;
            this.isDone = isDone;
        }

        
    }
}