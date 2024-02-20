namespace OpenPlanningPoker.GraphQL.Api.Features.Games;

public class Game
{
    public Guid Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;

    public async Task<Players> GetPlayers(
        [Service] IGameService gameService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var result = await gameService.GetParticipants(Id, cancellationToken);
        var mappedResult = mapper.Map<Players>(result);
        mappedResult.PlayerList = mapper.Map<ICollection<Player>>(result.Players);
        return mappedResult;
    }
}