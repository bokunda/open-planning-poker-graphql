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

    public async Task<Players> GetPlayers(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await _gameService.GetParticipants(gameId, cancellationToken);
        var mappedResult = _mapper.Map<Players>(result);
        mappedResult.PlayerList = _mapper.Map<ICollection<Player>>(result.Players);
        return mappedResult;
    }
}