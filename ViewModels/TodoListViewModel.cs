using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Todo.Models;
using Todo.Services.Data;

namespace Todo.ViewModels
{
    public class TodoListViewModel
    {
        public ObservableCollection<TodoList> TodoLists { get; set; } = new ObservableCollection<TodoList>();

        private readonly ITodoListRepository _todoRepository;
        private readonly ILogger _logger;

        public TodoListViewModel(ITodoListRepository todoRepository, ILogger<TodoListViewModel> logger)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }

        public async Task LoadTodoLists()
        {
            try
            {
                var todoLists = await _todoRepository.GetTodoListsAsync();
                TodoLists.Clear();
                foreach (var todoList in todoLists)
                {
                    TodoLists.Add(todoList);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading TodoLists");
            }
        }

        public async Task CreateTodoList(string titleOfTodoList)
        {
            try
            {
                await _todoRepository.CreateTodoListAsync(titleOfTodoList);
                await this.LoadTodoLists();
                _logger.LogDebug("User Added TodoList item", titleOfTodoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding TodoList");
            }
        }

        public async Task DeleteTodoList(int todoListId)
        {
            try
            {
                await _todoRepository.DeleteTodoListAsync(todoListId);
                await this.LoadTodoLists();
                _logger.LogDebug("User Deleted TodoList item", todoListId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error swiping to delete item");
            }
        }

    }
}
