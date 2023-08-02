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

        /// <summary>
        /// Returns single Todo List by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoList> GetTodoListByIdAsync(int id)
        {
            return await todoDatabase.Catalog.Table<TodoList>()
                                 .FirstOrDefaultAsync(n => n.Id == id);
        }

        /// <summary>
        /// Creates a Todo list with a specific title
        /// </summary>
        /// <param name="todoListTitle"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete a Todo list by its Id
        /// </summary>
        /// <param name="todoListId"></param>
        /// <returns></returns>
        public async Task DeleteTodoListAsync(int todoListId)
        {
            var todoList = await todoDatabase.Catalog.Table<TodoList>().FirstAsync(n => n.Id == todoListId);

            todoList.IsDeleted = true;
            todoList.UpdatedOn = DateTime.Now;
            await todoDatabase.Catalog.UpdateAsync(todoList);
        }

        public async Task EditTodoListAsync(int todoListId, string title)
        {
            var todoList = await todoDatabase.Catalog.Table<TodoList>().FirstAsync(n => n.Id == todoListId);

            todoList.Title = title;
            todoList.UpdatedOn = DateTime.Now;
            await todoDatabase.Catalog.UpdateAsync(todoList);
        }
    }
}
