using Clean.Domain.Entities;
using Xunit;

namespace Clean.UnitTests.Domain.Entities;

public class TodoTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesTodo()
    {
        // Arrange & Act
        var todo = new Todo("Test Todo", "Test Description");

        // Assert
        Assert.Equal("Test Todo", todo.Title);
        Assert.Equal("Test Description", todo.Description);
        Assert.False(todo.IsDone);
        Assert.NotEqual(default, todo.CreatedAt);
        Assert.Null(todo.DueDate);
    }

    [Fact]
    public void Constructor_WithEmptyTitle_ThrowsArgumentException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Todo("", "Description"));
        Assert.Throws<ArgumentException>(() => new Todo(" ", "Description"));
        Assert.Throws<ArgumentException>(() => new Todo(null!, "Description"));
    }

    [Fact]
    public void MarkDone_SetsIsDoneToTrue()
    {
        // Arrange
        var todo = new Todo("Test Todo");

        // Act
        todo.MarkDone();

        // Assert
        Assert.True(todo.IsDone);
    }

    [Fact]
    public void Update_WithValidData_UpdatesProperties()
    {
        // Arrange
        var todo = new Todo("Old Title", "Old Description");
        var newDueDate = DateTime.UtcNow.AddDays(1);

        // Act
        todo.Update("New Title", "New Description", newDueDate);

        // Assert
        Assert.Equal("New Title", todo.Title);
        Assert.Equal("New Description", todo.Description);
        Assert.Equal(newDueDate, todo.DueDate);
    }

    [Fact]
    public void Update_WithEmptyTitle_KeepsOriginalTitle()
    {
        // Arrange
        var todo = new Todo("Original Title", "Description");

        // Act
        todo.Update("", "New Description", null);

        // Assert
        Assert.Equal("Original Title", todo.Title);
        Assert.Equal("New Description", todo.Description);
    }
}
