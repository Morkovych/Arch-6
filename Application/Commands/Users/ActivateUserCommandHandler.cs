using Domain.Contracts;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.Users;

public class ActivateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<ActivateUserCommand, Unit>
{

    public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new UserNotFoundException(request.UserId);

        user.IsActive = true;
        await userRepository.UpdateAsync(user);
        await userRepository.SaveChangesAsync(ct);

        return Unit.Value;
    }
}