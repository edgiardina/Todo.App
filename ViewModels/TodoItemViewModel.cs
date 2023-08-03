using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Todo.Models;
using Todo.Services.Data;

namespace Todo.ViewModels
{
    public class TodoItemViewModel : BaseViewModel
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly ILogger _logger;

        private int _todoListId;

        public ObservableCollection<TodoItem> TodoItems { get; set; } = new ObservableCollection<TodoItem>();
        public string ListTitle { get; set; }

        public TodoItemViewModel(ITodoListRepository todoRepository, ITodoItemRepository todoItemRepository, ILogger<TodoItemViewModel> logger)
        {
            _todoListRepository = todoRepository;
            _todoItemRepository = todoItemRepository;
            _logger = logger;
        }

        public async Task LoadTodoItems(int todoListId)
        {
            if(_todoListId == default)
            {
                _todoListId = todoListId;
            }

            try
            {
                var todoList = await _todoListRepository.GetTodoListByIdAsync(todoListId);
                ListTitle = todoList.Title;
                OnPropertyChanged(nameof(ListTitle));
                
                var todoItems = await _todoItemRepository.GetTodoItemsByListIdAsync(todoListId);

                TodoItems.Clear();
                foreach (var todoItem in todoItems)
                {
                    TodoItems.Add(todoItem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Todo Items");
            }
        }

        public async Task CreateTodoItem(int todoListId, string title)
        {
            try
            {
                await _todoItemRepository.CreateTodoItemAsync(todoListId, title);
                await LoadTodoItems(todoListId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Todo Item");
            }
        }

        public async Task MarkCompleteTodoItem(int todoItemId)
        {
            try
            {
                await _todoItemRepository.MarkTodoItemCompleteAsync(todoItemId);
                await LoadTodoItems(_todoListId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing Todo Item");
            }
        }

        public async Task DeleteTodoItem(int todoItemId)
        {
            try
            {
                await _todoItemRepository.DeleteTodoItemAsync(todoItemId);
                //This is rather lazy, to be honest. reloading the items instead of just finding and
                //modifying the observable collection / item. But, it wasn't clear how much time I should
                //spend on this, (I did ask) so I put this here to get the entire spec done, and then add the magical
                //TODO: do not reload the entire list
                await LoadTodoItems(_todoListId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Todo Item");
            }
        }

        public async Task EditTodoItem(int toDoItemId, string title)
        {
            try
            {
                await _todoItemRepository.EditTodoItemAsync(toDoItemId, title);
                await this.LoadTodoItems(_todoListId);
                _logger.LogDebug("User Edited TodoList item", toDoItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing todolist");
            }
        }

        public async Task MarkIncompleteTodoItem(int todoItemId)
        {
            try
            {
                await _todoItemRepository.MarkTodoItemIncompleteAsync(todoItemId);
                await LoadTodoItems(_todoListId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking incomplete Todo Item");
            }
        }
    }
}
