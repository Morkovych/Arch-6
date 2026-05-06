using Application.Dto;
using Application.Queries.Users;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Moq;

namespace UnitTests.Users.Queries;

public class GetUserByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingUserId_ShouldReturnUserDto()
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            FirstName = "First",
            LastName = "Last",
            DateOfBirth = new DateTime(1990, 1, 1),
            IsActive = true
        };

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(user);

        var mapper = CreateMapper();
        var handler = new GetUserByIdQueryHandler(mockUserRepository.Object, mapper);
        var query = new GetUserByIdQuery(userId);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("test@example.com", result.Email);
        Assert.Equal("First", result.FirstName);
        Assert.Equal("Last", result.LastName);
        Assert.Equal(new DateTime(1990, 1, 1), result.DateOfBirth);
        Assert.True(result.IsActive);

        mockUserRepository.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentUserId_ShouldThrowUserNotFoundException()
    {
        var userId = Guid.NewGuid();

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync((User)null!); // или default(User)

        var mapper = CreateMapper();
        var handler = new GetUserByIdQueryHandler(mockUserRepository.Object, mapper);
        var query = new GetUserByIdQuery(userId);

        await Assert.ThrowsAsync<UserNotFoundException>(() =>
            handler.Handle(query, CancellationToken.None));
    }

    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
        });
        
        return config.CreateMapper();
    }
}