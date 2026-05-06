using MediatR;

namespace Application.Commands.Users;

public record ActivateUserCommand(Guid UserId) : IRequest<Unit>;