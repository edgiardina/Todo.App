﻿using Todo.Models;
using Todo.ViewModels;

namespace Todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        protected TodoListViewModel _todoListViewModel;

        public TodoListPage(TodoListViewModel todoListViewModel)
        {
            InitializeComponent();
            BindingContext = _todoListViewModel = todoListViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _todoListViewModel.LoadTodoLists();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayPromptAsync("Title", "Create a title for your Todo list");

            if (!string.IsNullOrWhiteSpace(response))
            {
                await _todoListViewModel.CreateTodoList(response.Trim());
            }
        }

        private async void DeleteItem_Invoked(object sender, EventArgs e)
        {
            var foundListItemId = int.TryParse(((SwipeItem)sender).CommandParameter.ToString(), out int swipedToDoListItemId);

            if (foundListItemId)
            {
                await _todoListViewModel.DeleteTodoList(swipedToDoListItemId);
            }
        }

        private async void TodoLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (TodoList)e.CurrentSelection.FirstOrDefault();

            if (selectedItem != null)
            {
                ((CollectionView)sender).SelectedItem = null;
                await Shell.Current.GoToAsync($"TodoItem?todoListId={selectedItem.Id}");
            }
        }

        private async void EditItem_Invoked(object sender, EventArgs e)
        {
            var foundListItemId = int.TryParse(((SwipeItem)sender).CommandParameter.ToString(), out int swipedToDoListItemId);

            if(foundListItemId)
            {
                var todoList = _todoListViewModel.TodoLists.Single(n => n.Id == swipedToDoListItemId);
                var newTitle = await DisplayPromptAsync("Enter a new title", string.Empty, placeholder: todoList.Title);
                if (string.IsNullOrWhiteSpace(newTitle))
                {
                    await _todoListViewModel.EditTodoListItem(swipedToDoListItemId, newTitle);
                }
            }
        }
    }
}