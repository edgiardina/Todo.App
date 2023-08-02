using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Data
{
    public interface ITodoItemRepository
    {
        Task CreateTodoItemAsync(int todoListId, string todoItemTitle);
        Task DeleteTodoItemAsync(int todoId);
        Task<List<TodoItem>> GetTodoItemsByListIdAsync(int listId);
        Task MarkTodoItemCompleteAsync(int todoId);
    }
}
