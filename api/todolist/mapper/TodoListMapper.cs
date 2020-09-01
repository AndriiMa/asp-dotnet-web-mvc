using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_mvc
{
    public class TodoListMapper
    {
        public TodoListDto MapToDto(TodoList obj)
        {
            TodoListDto dto = new TodoListDto();
            dto.id = obj.GetId();
            dto.name = obj.GetName();
            dto.description = obj.GetDescription();

            return dto;
        }

        public TodoList MapToObject(TodoListDto dto)
        {
            TodoList obj = new TodoList(
                dto.id,
                dto.name,
                dto.description
            );
            return obj;

        }

        public List<TodoListDto> MapToList(List<TodoList> entityes)
        {
            List<TodoListDto> dtos = new List<TodoListDto>();
            dtos = entityes.Select(e => MapToDto(e))
            .ToList();

            return dtos;
        }
    }
}