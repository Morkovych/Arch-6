using Application.Dto;
using Application.Queries.Users;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Moq;

namespace UnitTests.Users.Queries;

public class GetAllUsersQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedUserDtos()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var mapper = CreateMapper();

        var handler = new GetAllUsersQueryHandler(mockUserRepository.Object, mapper);

        var users = new List<User>
        {
            new()
            {
                Id = Guid.NewGuid(), Email = "user1@example.com", FirstName = "John", LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1), IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(), Email = "user2@example.com", FirstName = "Jane", LastName = "Smith",
                DateOfBirth = new DateTime(1985, 5, 15), IsActive = false
            }
        };

        mockUserRepository
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(users);

        var query = new GetAllUsersQuery();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        for (int i = 0; i < users.Count; i++)
        {
            Assert.Equal(users[i].Id, result[i].Id);
            Assert.Equal(users[i].Email, result[i].Email);
            Assert.Equal(users[i].FirstName, result[i].FirstName);
            Assert.Equal(users[i].LastName, result[i].LastName);
            Assert.Equal(users[i].DateOfBirth, result[i].DateOfBirth);
            Assert.Equal(users[i].IsActive, result[i].IsActive);
        }

        mockUserRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
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