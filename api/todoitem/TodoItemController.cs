using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace csharp_mvc
{
    [Route("/api/tasks")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private TodoItemRepository todoItemRepository = TodoItemRepository.GetInstance();
        private TodoItemMapper todoItemMapper = new TodoItemMapper();

        [HttpGet("")]
        public List<TodoItemDto> GetAllTodoItems()
        {
            List<TodoItem> entyties = todoItemRepository.GetAll();

            return todoItemMapper.MapToList(entyties);
        }

        [HttpPost("")]
        public TodoItemDto CreateTodoItem([FromBody] TodoItemDto dto)
        {
            TodoItem todoItem = new TodoItem(dto.name, dto.done, dto.scheduleId);
            TodoItem savedTodoItem = todoItemRepository.SaveNew(todoItem);
            return todoItemMapper.MapToDto(savedTodoItem);
        }

        [HttpPost("/{id}")]
        public void UpdateTodoItem(int id, [FromBody] TodoItemDto dto){
            TodoItem todoItem = new TodoItem(id, dto.name, dto.done, dto.scheduleId);
            todoItemRepository.Update(todoItem);
        }

        [HttpDelete("/{id}")]
        public void DeleteTodoItemById(int id)
        {
            todoItemRepository.DeleteById(id);
        }

        [HttpGet("/{id}")]
        public TodoItemDto GetTodoItemById(int id)
        {
            TodoItem todoItem = todoItemRepository.GetById(id);
            return todoItemMapper.MapToDto(todoItem);
        }
    }

}