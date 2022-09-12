using Bismuth.Domain.Entities;
using BismuthAPI.Contracts.Project;
using Mapster;

namespace BismuthAPI.Data.Mappings;

internal sealed class UpdateProjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<UpdateProjectRequest, Project>()
            .IgnoreNullValues(true);
    }
}
