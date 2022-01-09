using Xunit;
using Moq;
using ProjectX.Api.Repositories;
using System;
using ProjectX.Api.Entities;
using ProjectX.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Api.Dtos;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace ProjectX.Api.UnitTest;

public class TodoItemsControllerTest
{
    private readonly Mock<IInMemTodoItemRepository> repositoryStub = new();

    [Fact]
    public async Task GetTodoItemsAsync_WithUnexcitingItem_ReturnsNotFound()
    {
        // Arrange

        var repositoryStub = new Mock<IInMemTodoItemRepository>();
        repositoryStub.Setup(repo => repo.GetTodoItem(It.IsAny<Guid>())).ReturnsAsync((TodoItem)null);

        var controller = new TodoItemsController(repositoryStub.Object);

        // Act

        var result = await controller.GetTodoItem(Guid.NewGuid());

        // Assert

        Assert.IsType<NotFoundResult>(result.Result);

    }


    [Fact]
    public async Task GetTodoItemsAsync_WithExcitingItem_ReturnsFound()
    {
        // Arrange

        var expectedTodoItems = CreateRandomTodoItem();
        repositoryStub.Setup(repo => repo.GetTodoItem(It.IsAny<Guid>())).ReturnsAsync(expectedTodoItems);

        var controller = new TodoItemsController(repositoryStub.Object);

        // Act

        var result = await controller.GetTodoItem(Guid.NewGuid());

        // Assert

        result.Value.Should().BeEquivalentTo(expectedTodoItems, options => options.ComparingByMembers<TodoItem>());

    }

    [Fact]
    public async Task GetTodoItemsAsync_WithExcitingItems_ReturnsAllItems()
    {
        // Arrange

        var expectedTodoItems = new[] { CreateRandomTodoItem(), CreateRandomTodoItem(), CreateRandomTodoItem() };

        repositoryStub.Setup(repo => repo.GetTodoItemsAsync()).ReturnsAsync(expectedTodoItems);

        var controllers = new TodoItemsController(repositoryStub.Object);
        // Act

        var actualItems = await controllers.GetTodoItemsAsync();

        // Assert

        actualItems.Should().BeEquivalentTo(expectedTodoItems, options => options.ComparingByMembers<TodoItem>());

    }
    
    [Fact]
    public async Task CreateTodoItemsAsync_WithTodoItemToCreate_ReturnsCreatedItem()
    {
        // Arrange

        var todoItemToCreate = new CreateTodoItemDto()
        {
            Name = Guid.NewGuid().ToString(),
            Todo = Guid.NewGuid().ToString()
        };

        var controller = new TodoItemsController(repositoryStub.Object);
        
        // Act

        var result = await controller.CreateTodoItemAsync(todoItemToCreate);

        // Assert

        var createdTodoItem = (result.Result as CreatedAtActionResult).Value as TodoItemDto;
        todoItemToCreate.Should().BeEquivalentTo(createdTodoItem, options => options.ComparingByMembers<TodoItemDto>().ExcludingMissingMembers());
        createdTodoItem.Id.Should().NotBeEmpty();
        createdTodoItem.TodoDateTime.Should().BeCloseTo(DateTimeOffset.Now, 2.Seconds());

    }
    
    [Fact]
    public async Task UpdateExistingTodoItemAsync_WithExistingTodoItem_ReturnsNoContent()
    {
        // Arrange
        TodoItem existingTodoItem = CreateRandomTodoItem();

        repositoryStub.Setup(repo => repo.GetTodoItem(It.IsAny<Guid>())).ReturnsAsync(existingTodoItem);

        var todoItemId = existingTodoItem.Id;
        var todoItemToUpdate = new UpdateTodoItemDto()
            {Name = Guid.NewGuid().ToString(), Todo = Guid.NewGuid().ToString()};
        
        var controller = new TodoItemsController(repositoryStub.Object);
        
        // Act

        var result = await controller.UpdateTodoItemAsync(todoItemId, todoItemToUpdate);

        // Assert

        result.Should().BeOfType<NoContentResult>();

    }

    [Fact]
    public async Task DeleteExistingTodoItemAsync_WithExistingTodoItem_ReturnsNoContent()
    {
        // Arrange
        TodoItem existingTodoItem = CreateRandomTodoItem();

        repositoryStub.Setup(repo => repo.GetTodoItem(It.IsAny<Guid>())).ReturnsAsync(existingTodoItem);

        var controller = new TodoItemsController(repositoryStub.Object);
        
        // Act

        var result = await controller.DeleteTodoItemAsync(existingTodoItem.Id);

        // Assert

        result.Should().BeOfType<NoContentResult>();

    }
    
    private TodoItem CreateRandomTodoItem()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Todo = Guid.NewGuid().ToString(),
            TodoDateTime = DateTimeOffset.UtcNow
        };
    }
}