using System.Collections.Generic;
using System.Linq;

namespace csharp_mvc
{

    public class TodoItemMapper
    {

        public TodoItemDto MapToDto(TodoItem entity)
        {
            TodoItemDto dto = new TodoItemDto();
            dto.id = entity.GetId();
            dto.name = entity.GetName();
            dto.done = entity.IsDone();
            dto.scheduleId = entity.GetTodoListId();

            return dto;
        }

        public TodoItem MapToObject(TodoItemDto dto)
        {
            TodoItem goal = new TodoItem(
                dto.id,
                dto.name,
                dto.done,
                dto.scheduleId
            );

            return goal;
        }

        public TodoItem MapToObject(NewTodoItemDto dto)
        {
            TodoItem goal = new TodoItem(
                dto.name,
                dto.done,
                0
            );

            return goal;
        }

        public List<TodoItemDto> MapToList(List<TodoItem> entityes)
        {
            List<TodoItemDto> dtos = new List<TodoItemDto>();
            dtos = entityes.Select(e => MapToDto(e))
            .ToList();

            return dtos;
        }

    }
}