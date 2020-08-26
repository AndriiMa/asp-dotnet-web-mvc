using System;

namespace csharp_mvc
{
    public class Task
    {
        private String name{set; get;}
        private bool isDone;

        public Task(string name, bool isDone)
        {
            this.name = name;
            this.isDone = isDone;
        }

        
    }
}