namespace OpenPlanningPoker.GraphQL.Api.AutoMapper;

public class GameSettingsMappingProfile : Profile
{
    public GameSettingsMappingProfile()
    {
        CreateMap<GetGameSettingsResponse, GameSettings>();
        CreateMap<CreateGameSettingsCommand, GameSettings>();
        CreateMap<CreateGameSettingsResponse, GameSettings>();
        CreateMap<UpdateGameSettingsCommand, GameSettings>();
        CreateMap<UpdateGameSettingsResponse, GameSettings>();
    }
}