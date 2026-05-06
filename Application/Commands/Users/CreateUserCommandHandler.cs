using Application.Dto;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users;

public class CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var user = mapper.Map<User>(request);

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync(ct);

        return mapper.Map<UserDto>(user);
    }
}