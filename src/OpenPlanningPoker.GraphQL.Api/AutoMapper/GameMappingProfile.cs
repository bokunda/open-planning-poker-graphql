namespace OpenPlanningPoker.GraphQL.Api.AutoMapper;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<CreateGameResponse, Game>();
        CreateMap<GetGameResponse, Game>();
        CreateMap<JoinGameResponse, GameUserPair>();
        CreateMap<LeaveGameResponse, GameUserPair>();
        CreateMap<ListPlayersItem, Player>();
    }
}