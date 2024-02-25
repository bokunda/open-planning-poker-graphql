namespace OpenPlanningPoker.GraphQL.Service.Features.GameSettings;

public interface IGameSettingsService
{
    /// <summary>
    /// Returns game settings details - {gameId}
    /// </summary>
    Task<GetGameSettingsResponse> GetGameSettings(Guid gameId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a game settings
    /// </summary>
    Task<CreateGameSettingsResponse> CreateGameSettings(CreateGameSettingsCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a game settings
    /// </summary>
    Task<UpdateGameSettingsResponse> UpdateGameSettings(UpdateGameSettingsCommand command, CancellationToken cancellationToken = default);
}

public class GameSettingsService : IGameSettingsService
{
    private readonly IGameEngineClient _gameEngineClient;

    public GameSettingsService(IGameEngineClient gameEngineClient)
    {
        _gameEngineClient = gameEngineClient;
    }

    public async Task<GetGameSettingsResponse> GetGameSettings(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameSettingsResource.GetGameSettings(gameId, cancellationToken);
    }

    public async Task<CreateGameSettingsResponse> CreateGameSettings(CreateGameSettingsCommand command, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameSettingsResource.CreateGameSettings(command, cancellationToken);
    }

    public async Task<UpdateGameSettingsResponse> UpdateGameSettings(UpdateGameSettingsCommand command, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameSettingsResource.UpdateGameSettings(command, cancellationToken);
    }
}