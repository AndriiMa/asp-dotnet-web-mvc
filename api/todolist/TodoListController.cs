using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace csharp_mvc
{
    [Route("/api/lists")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private TodoListRepository todoListRepository = TodoListRepository.GetInstance();
        private TodoItemRepository todoItemRepository = TodoItemRepository.GetInstance();
        private TodoListMapper todoListMapper = new TodoListMapper();
        private TodoItemMapper todoItemMapper = new TodoItemMapper();


        [HttpGet("")]
        public List<TodoListDto> GetAllTodoLists()
        {
            List<TodoList> todoLists = todoListRepository.GetAll();
            return todoListMapper.MapToList(todoLists);
        }

        [HttpGet("/{id}")]
        public TodoListDto GetTodoListById(int id)
        {
            TodoList todoList = todoListRepository.GetById(id);
            return todoListMapper.MapToDto(todoList);
        }

        [HttpDelete("/{id}")]
        public void DeleteTodoListById(int id)
        {
            todoItemRepository.DeleteByTodoListId(id);
            todoListRepository.DeleteById(id);
        }

        [HttpPost("")]
        public TodoListDto SaveNewTodoList([FromBody] TodoListDto dto)
        {
            TodoList newTodoList = todoListMapper.MapToObject(dto);
            TodoList savedTodoList = todoListRepository.SaveNew(newTodoList);
            return todoListMapper.MapToDto(savedTodoList);
        }

        [HttpPost("/{id}")]
        public void UpdateTodoListById(int id, [FromBody] TodoListDto dto)
        {
            TodoList todoList = todoListMapper.MapToObject(dto);
            todoListRepository.Update(todoList);
        }

        [HttpGet("/{id}/tasks")]
        public List<TodoItemDto> GetGoalsByTodoListId(int id)
        {
            List<TodoItem> todoItems = todoItemRepository.GetByTodoListId(id);
            return todoItemMapper.MapToList(todoItems);
        }

        [HttpDelete("/{id}/tasks")]
        public void DeleteByTodoListId(int id)
        {
            todoItemRepository.DeleteByTodoListId(id);
        }

        [HttpPost("/{id}/tasks")]
        public TodoItemDto CreateNewTodoItem(int id, NewTodoItemDto dto)
        {
            TodoItem todoItem = todoItemMapper.MapToObject(dto);
            todoItem.SetTodoListId(id);
            TodoItem createdTodoItem = todoItemRepository.SaveNew(todoItem);
            return todoItemMapper.MapToDto(createdTodoItem);
        }

    }

}