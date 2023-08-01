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
            .ConfigureLogging();

		return builder.Build();
	}

	static MauiAppBuilder RegisterTodoModelsAndServices(this MauiAppBuilder builder)
	{
        var s = builder.Services;

        //Add Pages/Views
        s.AddSingleton<TodoListPage>();

        //Add ViewModels
        s.AddSingleton<TodoListViewModel>();

        //Add Services
        s.AddSingleton<TodoDatabase>();
        s.AddSingleton<ITodoListRepository, TodoListRepository>();

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


        //builder.Logging.AddSerilog(Log.Logger);

        builder.Services.AddLogging(builder => {
            builder.AddSerilog(Log.Logger);
        });

        return builder;
	}
}
