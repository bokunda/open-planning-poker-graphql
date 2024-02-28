using OpenPlanningPoker.GraphQL.Api.Features.Tickets;

namespace OpenPlanningPoker.GraphQL.Api.Extensions;

public static class GraphQlServerExtensions
{
    public static IRequestExecutorBuilder AddGraphQlWithSchema(this IServiceCollection services) =>
        services
            .AddGraphQLServer()
            .AddQueries()
            .AddMutations();

    private static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder) =>
        builder
            .AddQueryType<Query>()
            .AddTypeExtension<InfoQueries>()
            .AddTypeExtension<GameQueries>()
            .AddTypeExtension<GameSettingsQueries>()
            .AddTypeExtension<TicketQueries>()
            .AddTypeExtension<VoteQueries>();

    private static IRequestExecutorBuilder AddMutations(this IRequestExecutorBuilder builder) =>
        builder
            .AddMutationType<Mutation>()
            .AddTypeExtension<GameMutations>()
            .AddTypeExtension<GameSettingsMutations>()
            .AddTypeExtension<TicketMutations>()
            .AddTypeExtension<VoteMutations>();
}