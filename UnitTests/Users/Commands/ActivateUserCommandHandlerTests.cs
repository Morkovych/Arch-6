using Application.Commands.Users;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using Moq;

namespace UnitTests.Users.Commands;

public class ActivateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidUserId_ShouldActivateUserAndSave()
    {
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, IsActive = false };

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(user);

        var handler = new ActivateUserCommandHandler(mockUserRepository.Object);
        var command = new ActivateUserCommand(userId);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(Unit.Value, result);
        Assert.True(user.IsActive);

        mockUserRepository.Verify(repo => repo.UpdateAsync(user), Times.Once);
        mockUserRepository.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentUserId_ShouldThrowUserNotFoundException()
    {
        var userId = Guid.NewGuid();

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync((User)null!);

        var handler = new ActivateUserCommandHandler(mockUserRepository.Object);
        var command = new ActivateUserCommand(userId);

        await Assert.ThrowsAsync<UserNotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}