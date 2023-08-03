using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;
using Todo.Services.Data;
using Todo.ViewModels;
using Todo.Views;

namespace Todo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterTodoModelsAndServices()
            .ConfigureMauiHandlers(handlers =>
            {
                //handlers to fix this issue which keeps crashing anytime we change an android collectionview
                //https://github.com/dotnet/maui/issues/12219
#if ANDROID
                handlers.AddHandler<CollectionView, CustomCollectionViewHandler>();
#endif
            })
            .ConfigureLogging();

		return builder.Build();
	}

	static MauiAppBuilder RegisterTodoModelsAndServices(this MauiAppBuilder builder)
	{
        var s = builder.Services;

        //Add Pages/Views
        s.AddScoped<TodoListPage>();
        s.AddScoped<TodoItemPage>();

        //Add ViewModels
        s.AddScoped<TodoListViewModel>();
        s.AddScoped<TodoItemViewModel>();

        //Add Services
        s.AddSingleton<TodoDatabase>();
        s.AddSingleton<ITodoListRepository, TodoListRepository>();
        s.AddSingleton<ITodoItemRepository, TodoItemRepository>();

        return builder;
    }

	static MauiAppBuilder ConfigureLogging(this MauiAppBuilder builder)
	{
		Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
#if ANDROID
		.WriteTo.AndroidLog()
        .Enrich.WithProperty(Serilog.Core.Constants.SourceContextPropertyName, "todolist")
#endif
#if DEBUG
        .WriteTo.Debug()
#endif
        .CreateLogger();
        
        //The logger currently doesn't work as expected. I don't see output in the Debug output window
        //nor the AndroidLog. The packages are for Xamarin not specifically maui so it might be that
        //normally we'd be logging to AppCenter, SEQ, or something else. So the logging is currently


        builder.Services.AddLogging(builder => {
            builder.AddSerilog(Log.Logger);
        });

        return builder;
	}
}
