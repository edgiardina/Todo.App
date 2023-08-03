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
                                             .Where(n => n.TodoListId == listId &&
                                                    n.IsDeleted == false)
                                             //sort by Completed so incomplete items are biased towards the top
                                             .OrderBy(n => n.IsCompleted)   
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

        public async Task MarkTodoItemCompleteAsync(int todoId)
        {
            var todoItem = await todoDatabase.Catalog.Table<TodoItem>().FirstAsync(n => n.Id == todoId);

            todoItem.IsCompleted = true;
            todoItem.UpdatedOn = DateTime.Now;
            await todoDatabase.Catalog.UpdateAsync(todoItem);
        }

        public async Task DeleteTodoItemAsync(int todoId)
        {
            var todoItem = await todoDatabase.Catalog.Table<TodoItem>().FirstAsync(n => n.Id == todoId);

            todoItem.IsDeleted = true;
            todoItem.UpdatedOn = DateTime.Now;
            await todoDatabase.Catalog.UpdateAsync(todoItem);
        }

        public async Task EditTodoItemAsync(int todoId, string title)
        {
            var todoList = await todoDatabase.Catalog.Table<TodoItem>().FirstAsync(n => n.Id == todoId);

            todoList.Title = title;
            todoList.UpdatedOn = DateTime.Now;
            await todoDatabase.Catalog.UpdateAsync(todoList);
        }

        public async Task MarkTodoItemIncompleteAsync(int todoId)
        {
            var todoItem = await todoDatabase.Catalog.Table<TodoItem>().FirstAsync(n => n.Id == todoId);

            todoItem.IsCompleted = false;
            todoItem.UpdatedOn = DateTime.Now;
            await todoDatabase.Catalog.UpdateAsync(todoItem);
        }
    }
}
