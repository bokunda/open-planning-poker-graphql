using OpenPlanningPoker.GraphQL.Service.Features.GameSettings;

namespace OpenPlanningPoker.GraphQL.Api.Features.Games;

public class Game
{
    public Guid Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;

    public async Task<ApiCollection<Player>> GetPlayers(
        [Service] IGameService gameService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var result = await gameService.GetParticipants(Id, cancellationToken);
        return mapper.Map<ApiCollection<Player>>(result);
    }

    public async Task<GameSettings.GameSettings> GetSettings(
        [Service] IGameSettingsService gameSettingsService,
        [Service] IMapper mapper,
        CancellationToken cancellationToken = default)
    {
        var result = await gameSettingsService.GetGameSettings(Id, cancellationToken);
        return mapper.Map<GameSettings.GameSettings>(result);
    }
}