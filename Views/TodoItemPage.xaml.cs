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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _todoItemViewModel.LoadTodoItems(TodoListId);
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {

    }

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {

    }
}