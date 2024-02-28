namespace OpenPlanningPoker.GraphQL.Api.Features.Games;

[ExtendObjectType(typeof(Query))]
public class GameQueries
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;

    public GameQueries(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    public async Task<Game> GetGame(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await _gameService.Get(gameId, cancellationToken);
        return _mapper.Map<Game>(result);
    }

    public async Task<ApiCollection<Player>> GetPlayers(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await _gameService.GetParticipants(gameId, cancellationToken);
        return _mapper.Map<ApiCollection<Player>>(result);
    }
}