namespace OpenPlanningPoker.GraphQL.Service.Features;

public interface IGameService
{
    Task<GetGameResponse> Get(Guid gameId, CancellationToken cancellationToken = default);
    Task<CreateGameResponse> Create(CreateGameCommand command, CancellationToken cancellationToken = default);
    Task<ListPlayersResponse> GetParticipants(Guid gameId, CancellationToken cancellationToken = default);
    Task<JoinGameResponse> Join(Guid gameId, CancellationToken cancellationToken = default);
    Task<LeaveGameResponse> Leave(Guid gameId, CancellationToken cancellationToken = default);
}

public class GameService : IGameService
{
    private readonly GameEngineClient _gameEngineClient;

    public GameService(GameEngineClient gameEngineClient)
    {
        _gameEngineClient = gameEngineClient;
    }
    public async Task<GetGameResponse> Get(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.Games.GetGameDetails(gameId, cancellationToken);
    }

    public async Task<CreateGameResponse> Create(CreateGameCommand command, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.Games.CreateGame(command, cancellationToken);
    }

    public async Task<ListPlayersResponse> GetParticipants(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.Games.GetParticipants(gameId, cancellationToken);
    }

    public async Task<JoinGameResponse> Join(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.Games.JoinGame(gameId, cancellationToken);
    }

    public async Task<LeaveGameResponse> Leave(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.Games.LeaveGame(gameId, cancellationToken);
    }
}