﻿namespace OpenPlanningPoker.GraphQL.Service.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, AppSettings appSettings) =>
        services
            .AddGameEngineClient(appSettings.GameEngineApi)
            .AddTransient<IGameService, GameService>();
}