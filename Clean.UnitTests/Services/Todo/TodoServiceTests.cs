using Clean.Application.Abstractions;
using Clean.Application.Dtos.Todo;
using Clean.Domain.Entities;
using Clean.Application.Services.Todo;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Clean.UnitTests.Services.Todo;

public class TodoServiceTests
{
    private Mock<IAppDbContext> _mockContext;
    private TodoService _todoService;
    private readonly List<Clean.Domain.Entities.Todo> _todos;

 

    [Fact]
    public void Get_WhenCalled_ReturnsAllTodos()
    {
        // Arrange
       var todos =  new List<Clean.Domain.Entities.Todo>(new[]
        {
            new Clean.Domain.Entities.Todo("Test 1", "Description 1"),
            new Clean.Domain.Entities.Todo("Test 2", "Description 2")
        }).AsQueryable();
        
        var mockSet = new Mock<DbSet<Clean.Domain.Entities.Todo>>();
        mockSet.As<IQueryable<Clean.Domain.Entities.Todo>>().Setup(m => m.Provider).Returns(todos.Provider);
        mockSet.As<IQueryable<Clean.Domain.Entities.Todo>>().Setup(m => m.Expression).Returns(todos.Expression);
        mockSet.As<IQueryable<Clean.Domain.Entities.Todo>>().Setup(m => m.ElementType).Returns(todos.ElementType);
        mockSet.As<IQueryable<Clean.Domain.Entities.Todo>>().Setup(m => m.GetEnumerator()).Returns(todos.GetEnumerator());

        
        var mockContext = new Mock<IAppDbContext>();
        mockContext.Setup(c => c.Todos).Returns(mockSet.Object);
        

        // Act
        var result = _todoService.Get();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Test 1", result[0].Title);
        Assert.Equal("Test 2", result[1].Title);
    }

    [Fact]
    public async Task AddTodo_WithValidData_AddsNewTodo()
    {
        // Arrange
        var createDto = new CreateTodoDto 
        { 
            Title = "New Task",
            Description = "New Description"
        };
        
        var mockSet = new Mock<DbSet<Clean.Domain.Entities.Todo>>();
        _mockContext = new Mock<IAppDbContext>();
        _mockContext.Setup(c => c.Todos).Returns(mockSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        var emailService = new Mock<IEmailService>();
        emailService.Setup(e => e.Send(It.IsAny<string>())).ReturnsAsync(true);
        
        _todoService = new TodoService(_mockContext.Object, emailService.Object);
        
        
        // Act
        var result = await _todoService.AddTodo(createDto);

        // Assert
        Assert.Equal(createDto.Title, result.Title);
        Assert.Equal(createDto.Description, result.Description);
        Assert.False(result.IsDone);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task Update_WithValidData_UpdatesTodo()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var existingTodo = new Clean.Domain.Entities.Todo("Old Title", "Old Description")
        {
            Id = todoId
        };

        var updateDto = new CreateTodoDto
        {
            Id = todoId,
            Title = "Updated Title",
            Description = "Updated Description",
            DueDate = DateTime.UtcNow.AddDays(7)
        };

        var mockSet = new Mock<DbSet<Clean.Domain.Entities.Todo>>();
        _mockContext = new Mock<IAppDbContext>();
        _mockContext.Setup(c => c.Todos.FindAsync(todoId)).ReturnsAsync(existingTodo);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        
        var emailService = new Mock<IEmailService>();
        emailService.Setup(e => e.Send(It.IsAny<string>())).ReturnsAsync(true);
        
        _todoService = new TodoService(_mockContext.Object, emailService.Object);

        // Act
        var result = await _todoService.Update(updateDto);

        // Assert
        Assert.Equal(updateDto.Title, result.Title);
        Assert.Equal(updateDto.Description, result.Description);
        Assert.Equal(updateDto.DueDate, result.DueDate);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Update_WithNonExistentId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        var updateDto = new CreateTodoDto { Id = nonExistentId, Title = "Test" };

        _mockContext = new Mock<IAppDbContext>();
        _mockContext.Setup(c => c.Todos.FindAsync(nonExistentId)).ReturnsAsync((Clean.Domain.Entities.Todo)null);
       // _todoService = new TodoService(_mockContext.Object);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _todoService.Update(updateDto));
    }

    [Fact]
    public async Task Delete_WithExistingId_ReturnsTrue()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var existingTodo = new Clean.Domain.Entities.Todo("Test", "Test Description") { Id = todoId };
        
        var mockSet = new Mock<DbSet<Clean.Domain.Entities.Todo>>();
        _mockContext = new Mock<IAppDbContext>();
        _mockContext.Setup(c => c.Todos.FindAsync(todoId)).ReturnsAsync(existingTodo);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        
        var emailService = new Mock<IEmailService>();
        emailService.Setup(e => e.Send(It.IsAny<string>())).ReturnsAsync(true);
        
        _todoService = new TodoService(_mockContext.Object,emailService.Object);

        // Act
        var result = await _todoService.Delete(todoId);

        // Assert
        Assert.True(result);
        _mockContext.Verify(c => c.Todos.Remove(existingTodo), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_WithNonExistentId_ReturnsFalse()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        
        _mockContext = new Mock<IAppDbContext>();
        _mockContext.Setup(c => c.Todos.FindAsync(nonExistentId)).ReturnsAsync((Clean.Domain.Entities.Todo)null);
        
        var emailService = new Mock<IEmailService>();
        emailService.Setup(e => e.Send(It.IsAny<string>())).ReturnsAsync(true);
        
        _todoService = new TodoService(_mockContext.Object,emailService.Object);

        // Act
        var result = await _todoService.Delete(nonExistentId);

        // Assert
        Assert.False(result);
        _mockContext.Verify(c => c.Todos.Remove(It.IsAny<Clean.Domain.Entities.Todo>()), Times.Never);
        _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
