using Application.Dto;
using MediatR;

namespace Application.Queries.Users;

public record GetAllUsersQuery : IRequest<List<UserDto>>;