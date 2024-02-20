namespace OpenPlanningPoker.GraphQL.Api.Features.Games;

public class Players
{
    public Guid GameId { get; set; }
    public ICollection<Player> PlayerList { get; set; } = new List<Player>();
    public int TotalCount { get; set; }
}

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}