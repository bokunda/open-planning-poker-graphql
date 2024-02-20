namespace OpenPlanningPoker.GraphQL.Api.Features;

public class Query
{
    public async Task<string> Ping() => await Task.FromResult("Pong.");
}