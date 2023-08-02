using Microsoft.Extensions.Logging;
using Moq;
using Todo.Services.Data;
using Todo.ViewModels;

namespace Todo.UnitTests
{
    public class TodoItemViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TodoItemsIsNotNullAfterViewModelInitialization()
        {
            // Arrange
            var mockToDoListId = 1;
            var mockToDoItemId = 2;
            var mockListTitle = "Title";
            var mockItemTitle = "OtherTitle";
            var todoItemRepo = new Mock<ITodoItemRepository>();
            var todoListRepo = new Mock<ITodoListRepository>();
            var loggerMock = new Mock<ILogger<TodoItemViewModel>>();
            var viewModel = new TodoItemViewModel(todoListRepo.Object, todoItemRepo.Object, loggerMock.Object);

            todoListRepo.Setup(f => f.GetTodoListByIdAsync(It.IsAny<int>())).ReturnsAsync(new Models.TodoList
            {
                Id = mockToDoListId,
                Title = mockListTitle
            });

            todoItemRepo.Setup(f => f.GetTodoItemsByListIdAsync(It.IsAny<int>())).ReturnsAsync(new List<Models.TodoItem>
            {
                new Models.TodoItem { Id = mockToDoItemId, Title = mockItemTitle }
            });

            // Act
            await viewModel.LoadTodoItems(mockToDoListId);

            // Assert
            Assert.That(mockListTitle, Is.EqualTo(viewModel.ListTitle));
            Assert.That(viewModel.TodoItems, Is.Not.Empty);
        }
    }
}