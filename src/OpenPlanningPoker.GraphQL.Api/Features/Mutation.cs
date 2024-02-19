namespace OpenPlanningPoker.GraphQL.Api.Features;

public class Mutation
{
    public async Task<string> Ping() => await Task.FromResult("Pong.");
}