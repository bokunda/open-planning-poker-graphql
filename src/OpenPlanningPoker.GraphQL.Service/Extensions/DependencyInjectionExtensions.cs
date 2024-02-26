using OpenPlanningPoker.GraphQL.Service.Features.GameSettings;
using OpenPlanningPoker.GraphQL.Service.Features.Tickets;

namespace OpenPlanningPoker.GraphQL.Service.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, AppSettings appSettings) =>
        services
            .AddGameEngineClient(appSettings.GameEngineApi)
            .AddTransient<IGameService, GameService>()
            .AddTransient<IGameSettingsService, GameSettingsService>()
            .AddTransient<ITicketService, TicketService>();
}