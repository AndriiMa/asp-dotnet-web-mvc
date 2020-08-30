using System;

namespace csharp_mvc
{

    public class Schedule
    {

        private int id;
        private String name;
        private String description;

        public Schedule(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return this.id;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public String GetName()
        {
            return this.name;
        }

        public String GetDescription()
        {
            return this.description;
        }

        public void SetDescription(String description)
        {
            this.description = description;
        }

    }

}