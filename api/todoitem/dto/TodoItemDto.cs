using System;

namespace csharp_mvc
{

    public class TodoItemDto
    {
        public int id { get; set; }
        public String name { get; set; }
        public bool done { get; set; }

        public int scheduleId { get; set; }

    }

}