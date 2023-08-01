using Todo.Views;

namespace Todo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("TodoItem", typeof(TodoItemPage));
    }
}
