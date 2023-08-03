using Microsoft.Extensions.Logging;
using Moq;
using Todo.Services.Data;
using Todo.ViewModels;

namespace Todo.UnitTests
{
    public class TodoListViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TodoListIsNotNullAfterViewModelInitialization()
        {
            // Arrange
            var mockToDoListId = 1;
            var mockListTitle = "Title";
            var todoListRepo = new Mock<ITodoListRepository>();
            var loggerMock = new Mock<ILogger<TodoListViewModel>>();
            var viewModel = new TodoListViewModel(todoListRepo.Object, loggerMock.Object);

            todoListRepo.Setup(f => f.GetTodoListsAsync()).ReturnsAsync(new List<Models.TodoList>()
            {
                new Models.TodoList 
                {
                    Id = mockToDoListId,
                    Title = mockListTitle 
                }
            });

            // Act
            await viewModel.LoadTodoLists();

            // Assert
            Assert.That(viewModel.TodoLists, Is.Not.Empty);
        }

        [Test]
        public async Task TodoListsAddCallsRepositoryMethod()
        {
            // Arrange
            var mockListTitle = "Title";
            var todoListRepo = new Mock<ITodoListRepository>();
            var loggerMock = new Mock<ILogger<TodoListViewModel>>();
            var viewModel = new TodoListViewModel(todoListRepo.Object, loggerMock.Object);

            // Act
            await viewModel.CreateTodoList(mockListTitle);

            // Assert
            todoListRepo.Verify(n => n.CreateTodoListAsync(mockListTitle), Times.Once);
        }
    }
}