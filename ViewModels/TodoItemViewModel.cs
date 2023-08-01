﻿using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public TodoItemViewModel(ITodoListRepository todoRepository, ITodoItemRepository todoItemRepository, ILogger<TodoListViewModel> logger)
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
                await LoadTodoItems(_todoListId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Todo Item");
            }
        }
    }
}