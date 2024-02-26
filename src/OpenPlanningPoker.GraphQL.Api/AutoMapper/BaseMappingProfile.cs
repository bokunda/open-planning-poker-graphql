using OpenPlanningPoker.GameEngine.Api.Models;

namespace OpenPlanningPoker.GraphQL.Api.AutoMapper;

public class BaseMappingProfile : Profile
{
    public BaseMappingProfile()
    {
        CreateMap(typeof(ApiCollection<>), typeof(Features.Tickets.ApiCollection<>));
    }
}