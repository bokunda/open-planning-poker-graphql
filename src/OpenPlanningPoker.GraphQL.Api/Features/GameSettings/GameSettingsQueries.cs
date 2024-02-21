namespace OpenPlanningPoker.GraphQL.Api.Features.GameSettings;

[ExtendObjectType(typeof(Query))]
public class GameSettingsQueries
{
    private readonly IGameSettingsService _gameSettingsService;
    private readonly IMapper _mapper;

    public GameSettingsQueries(IGameSettingsService gameSettingsService, IMapper mapper)
    {
        _gameSettingsService = gameSettingsService;
        _mapper = mapper;
    }

    public async Task<GameSettings> GetGameSettings(Guid gameId)
    {
        var result = await _gameSettingsService.GetGameSettings(gameId);
        return _mapper.Map<GameSettings>(result);
    }
}