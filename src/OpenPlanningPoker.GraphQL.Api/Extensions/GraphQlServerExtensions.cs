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
            .AddTypeExtension<InfoQueries>();

    private static IRequestExecutorBuilder AddMutations(this IRequestExecutorBuilder builder) =>
        builder.AddMutationType<Mutation>();
}