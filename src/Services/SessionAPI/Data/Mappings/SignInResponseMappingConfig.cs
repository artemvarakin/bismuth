using GrpcSessionApi;
using Mapster;

namespace SessionAPI.Data.Mappings;

public class SignInResponseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(string idToken, string refreshToken), SignInResponse>()
            .Map(dest => dest.IdToken, src => src.idToken)
            .Map(dest => dest.RefreshToken, src => src.refreshToken);
    }
}