using Bismuth.Domain.Entities;
using BismuthAPI.Contracts.User;
using Mapster;

namespace BismuthAPI.Data.Mappings;

internal sealed class RegisterUserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(RegisterUserRequest request, Guid userId, string passwordHash), User>()
            .Map(dest => dest, src => src.request)
            .Map(dest => dest.Id, src => src.userId)
            .Map(dest => dest.PasswordHash, src => src.passwordHash);
    }
}