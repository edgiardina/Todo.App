using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services.Data
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly TodoDatabase todoDatabase;

        public TodoListRepository(TodoDatabase todoDatabase)
        {
            this.todoDatabase = todoDatabase;
        }

        /// <summary>
        /// Returns non-deleted To-do lists
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoList>> GetTodoListsAsync()
        {
            return await todoDatabase.Catalog.Table<TodoList>()
                                             .Where(n => n.IsDeleted == false)
                                             .ToListAsync();
        }

        public async Task CreateTodoListAsync(string todoListTitle)
        {
            var todoList = new TodoList
            {
                Title = todoListTitle,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                IsDeleted = false
            };

            await todoDatabase.Catalog.InsertAsync(todoList);
        }

        public async Task DeleteTodoListAsync(int todoListId)
        {
            var todoList = await todoDatabase.Catalog.Table<TodoList>().FirstAsync(n => n.Id == todoListId);

            todoList.IsDeleted = true;
            await todoDatabase.Catalog.UpdateAsync(todoList);        }
    }
}
