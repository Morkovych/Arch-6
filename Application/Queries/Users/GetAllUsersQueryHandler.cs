using Application.Dto;
using AutoMapper;
using Domain.Contracts;
using MediatR;

namespace Application.Queries.Users;

public class GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync();

        return users.Select(mapper.Map<UserDto>).ToList();
    }
}