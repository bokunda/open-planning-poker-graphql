using OpenPlanningPoker.GraphQL.Service.Features.GameSettings;

namespace OpenPlanningPoker.GraphQL.Api.Features.GameSettings;

[ExtendObjectType(typeof(Mutation))]
public class GameSettingsMutations
{
    private readonly IGameSettingsService _gameSettingsService;
    private readonly IMapper _mapper;

    public GameSettingsMutations(IGameSettingsService gameSettingsService, IMapper mapper)
    {
        _gameSettingsService = gameSettingsService;
        _mapper = mapper;
    }

    public async Task<GameSettings> CreateGameSettings(Guid gameId, int votingTime, bool isBreakAllowed)
    {
        var result = await _gameSettingsService.CreateGameSettings(new CreateGameSettingsCommand(gameId, votingTime, isBreakAllowed));
        return _mapper.Map<GameSettings>(result);
    }

    public async Task<GameSettings> UpdateGameSettings(Guid id, Guid gameId, int votingTime, bool isBreakAllowed)
    {
        var result = await _gameSettingsService.UpdateGameSettings(new UpdateGameSettingsCommand(id, gameId, votingTime, isBreakAllowed));
        return _mapper.Map<GameSettings>(result);
    }
}