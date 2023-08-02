using Todo.ViewModels;

namespace Todo.Views;

[QueryProperty("TodoListId", "todoListId")]
public partial class TodoItemPage : ContentPage
{
    public int TodoListId { get; set; }
    protected TodoItemViewModel _todoItemViewModel;

    public TodoItemPage(TodoItemViewModel todoItemViewModel)
	{
		InitializeComponent();
		BindingContext = _todoItemViewModel = todoItemViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _todoItemViewModel.LoadTodoItems(TodoListId);
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync("Title", "Create a title for your Todo Item");

        if (!string.IsNullOrWhiteSpace(response))
        {
            await _todoItemViewModel.CreateTodoItem(TodoListId, response.Trim());
        }
    }

    private async void EditItem_Invoked(object sender, EventArgs e)
    {
        var foundTodoItemId = int.TryParse(((SwipeItem)sender).CommandParameter.ToString(), out int swipedToDoItemId);

        if (foundTodoItemId)
        {
            var todoItem = _todoItemViewModel.TodoItems.Single(n => n.Id == swipedToDoItemId);
            var newTitle = await DisplayPromptAsync("Enter a new title", string.Empty, placeholder: todoItem.Title);
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                await _todoItemViewModel.EditTodoItem(swipedToDoItemId, newTitle);
            }
        }
    }
    private async void DeleteItem_Invoked(object sender, EventArgs e)
    {
        var foundTodoItemId = int.TryParse(((SwipeItem)sender).CommandParameter.ToString(), out int swipedToDoItemId);

        if (foundTodoItemId)
        {
            await _todoItemViewModel.DeleteTodoItem(swipedToDoItemId);
        }
    }

    private async void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var foundTodoItemId = int.TryParse(((RadioButton)sender).Value.ToString(), out int swipedToDoItemId);

        if (foundTodoItemId)
        {
            await _todoItemViewModel.CompleteTodoItem(swipedToDoItemId);
        }
    }
}