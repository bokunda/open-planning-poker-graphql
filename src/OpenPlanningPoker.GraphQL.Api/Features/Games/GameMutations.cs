namespace OpenPlanningPoker.GraphQL.Api.Features.Games;

[ExtendObjectType(typeof(Mutation))]
public class GameMutations
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;

    public GameMutations(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    public async Task<Game> CreateGame(string name, string description, CancellationToken cancellationToken = default)
    {
        var result = await _gameService.Create(new CreateGameCommand(name, description), cancellationToken);
        return _mapper.Map<Game>(result);
    }

    public async Task<GameUserPair> JoinGame(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await _gameService.Join(gameId, cancellationToken);
        return new(gameId, Guid.Parse("96d0bf00-fdd9-4dd7-944c-927d03bcea55")); // TODO: Use ICurrentUserProvider or update Client
    }

    public async Task<GameUserPair> LeaveGame(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await _gameService.Leave(gameId, cancellationToken);
        return new(gameId, Guid.Parse("96d0bf00-fdd9-4dd7-944c-927d03bcea55")); // TODO: Use ICurrentUserProvider or update Client
    }
}