namespace OpenPlanningPoker.GraphQL.Api.Features.GameSettings;

public class GameSettings
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public int VotingTime { get; set; }
    public bool IsBreakAllowed { get; set; }

    public async Task<Game> GetGame(
        [Service] IGameService gameService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken = default)
    {
        var result = await gameService.Get(GameId, cancellationToken);
        return mapper.Map<Game>(result);
    }
}