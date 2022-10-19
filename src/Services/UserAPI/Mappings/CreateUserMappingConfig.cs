using Bismuth.Domain.Entities;
using GrpcUserApi;
using Mapster;

namespace UserAPI.Mappings;

public sealed class CreateUserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateUserRequest request, string passwordHash), User>()
            .Map(dest => dest, src => src.request)
            .Map(dest => dest.PasswordHash, src => src.passwordHash);
    }
}