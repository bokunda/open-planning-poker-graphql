namespace OpenPlanningPoker.GraphQL.Api.Features.Info;

[ExtendObjectType(typeof(Query))]
public class InfoQueries
{
    public async Task<string> GetInfo() => await Task.FromResult("v0.0.1");
}