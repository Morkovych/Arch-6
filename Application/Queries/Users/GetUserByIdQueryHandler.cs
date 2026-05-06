using Application.Dto;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.Users;

public class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        return user == null
            ? throw new UserNotFoundException(request.UserId)
            : mapper.Map<UserDto>(user);
    }
}