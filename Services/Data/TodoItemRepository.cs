using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Data
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoDatabase todoDatabase;

        public TodoItemRepository(TodoDatabase todoDatabase)
        {
            this.todoDatabase = todoDatabase;
        }

        /// <summary>
        /// Get Todo items from a list
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<List<TodoItem>> GetTodoItemsByListIdAsync(int listId)
        {
            return await todoDatabase.Catalog.Table<TodoItem>()
                                             .Where(n => 
                                                    n.TodoListId == listId && 
                                                    n.IsDeleted == false && 
                                                    n.IsCompleted == false)
                                             .ToListAsync();
        }

        public async Task CreateTodoItemAsync(int todoListId, string todoItemTitle)
        {
            var todoItem = new TodoItem
            {
                Title = todoItemTitle,
                TodoListId = todoListId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                IsDeleted = false,
                IsCompleted = false
            };

            await todoDatabase.Catalog.InsertAsync(todoItem);
        }
    }
}
