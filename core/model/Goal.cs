using System;

namespace csharp_mvc
{
    public class Goal
    {
        private int id { get; set; }
        private String name { get; set; }
        private bool done { get; set; }

        private int scheduleId { get; set; }

        public Goal(string name, bool done)
        {
            this.name = name;
            this.done = done;
        }

        public Goal(int id, string name, bool done)
        {
            this.id = id;
            this.name = name;
            this.done = done;
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
        public int GetScheduleId()
        {
            return this.scheduleId;
        }

        public void SetScheduleId(int scheduleId)
        {
            this.scheduleId = scheduleId;
        }


    }
}