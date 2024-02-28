namespace OpenPlanningPoker.GraphQL.Api.AutoMapper;

public class BaseMappingProfile : Profile
{
    public BaseMappingProfile()
    {
        CreateMap(typeof(GameEngine.Api.Models.ApiCollection<>), typeof(Features.ApiCollection<>));
    }
}