using OpenPlanningPoker.GameEngine.Api.Models;

namespace OpenPlanningPoker.GraphQL.Service.Features.GameEngine;

public interface IGameService
{
    Task<GetGameResponse> Get(Guid gameId, CancellationToken cancellationToken = default);
    Task<CreateGameResponse> Create(CreateGameCommand command, CancellationToken cancellationToken = default);
    Task<ApiCollection<ListPlayersItem>> GetParticipants(Guid gameId, CancellationToken cancellationToken = default);
    Task<JoinGameResponse> Join(Guid gameId, CancellationToken cancellationToken = default);
    Task<LeaveGameResponse> Leave(Guid gameId, CancellationToken cancellationToken = default);
}

public class GameService : IGameService
{
    private readonly IGameEngineClient _gameEngineClient;

    public GameService(IGameEngineClient gameEngineClient)
    {
        _gameEngineClient = gameEngineClient;
    }
    public async Task<GetGameResponse> Get(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameResource.GetGameDetails(gameId, cancellationToken);
    }

    public async Task<CreateGameResponse> Create(CreateGameCommand command, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameResource.CreateGame(command, cancellationToken);
    }

    public async Task<ApiCollection<ListPlayersItem>> GetParticipants(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameResource.GetParticipants(gameId, cancellationToken);
    }

    public async Task<JoinGameResponse> Join(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameResource.JoinGame(gameId, cancellationToken);
    }

    public async Task<LeaveGameResponse> Leave(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.GameResource.LeaveGame(gameId, cancellationToken);
    }
}