using Application.Dto;
using MediatR;

namespace Application.Queries.Users;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;