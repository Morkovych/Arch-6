using Application.Dto;
using MediatR;

namespace Application.Commands.Users;

public record CreateUserCommand(
    string Email,
    string FirstName,
    string LastName,
    DateTime DateOfBirth
) : IRequest<UserDto>;