using Bismuth.Domain.Entities;
using MapsterMapper;
using MediatR;
using UserAPI.Abstractions;

namespace UserAPI.Application.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(command);
        await _userRepository.AddUserAsync(user, cancellationToken);

        return user;
    }
}