namespace OpenPlanningPoker.GraphQL.Api.Features.Tickets;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public async Task<Game> GetGame(
        [Service] IGameService gameService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken = default)
    {
        var result = await gameService.Get(GameId, cancellationToken);
        return mapper.Map<Game>(result);
    }
}