using Application.Commands.Users;
using Application.Dto;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Moq;

namespace UnitTests.Users.Commands;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCreateUserCommand_ShouldCreateUserAndReturnDto()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var mapper = CreateMapper();

        var handler = new CreateUserCommandHandler(mockUserRepository.Object, mapper);

        var command = new CreateUserCommand(
            Email: "test@example.com",
            FirstName: "First",
            LastName: "Last",
            DateOfBirth: new DateTime(1999, 1, 1)
        );

        User? addedUser = null;
        mockUserRepository
            .Setup(repo => repo.AddAsync(It.IsAny<User>()))
            .Callback<User>(u => addedUser = u)
            .Returns(Task.CompletedTask);

        mockUserRepository
            .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("test@example.com", result.Email);
        Assert.Equal("First", result.FirstName);
        Assert.Equal("Last", result.LastName);

        mockUserRepository.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        mockUserRepository.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotNull(addedUser);
        Assert.Equal(command.Email, addedUser.Email);
        Assert.Equal(command.FirstName, addedUser.FirstName);
        Assert.Equal(command.LastName, addedUser.LastName);
        Assert.Equal(command.DateOfBirth, addedUser.DateOfBirth);
    }

    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateUserCommand, User>();
            cfg.CreateMap<User, UserDto>();
        });

        return config.CreateMapper();
    }
}