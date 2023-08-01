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

    private void EditItem_Invoked(object sender, EventArgs e)
    {

    }
    private void DeleteItem_Invoked(object sender, EventArgs e)
    {

    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        
    }
}