using OpenPlanningPoker.GameEngine.Api.Models.Features.GameSettings;

namespace OpenPlanningPoker.GraphQL.UnitTests.Services;

public class GameSettingsServiceTests
{
    private readonly IGameSettingsService _gameSettingsService;
    private readonly IGameEngineClient _gameEngineClient = Substitute.For<IGameEngineClient>();

    public GameSettingsServiceTests()
    {
        _gameSettingsService = new GameSettingsService(_gameEngineClient);
    }

    [Fact]
    public async Task GetGameSettings_Success()
    {
        // Arrange
        var gameSettingsId = Guid.Parse("df9ff649-1111-41af-9792-5b1cd07a14e9");
        var gameId = Guid.Parse("df9ff649-2222-41af-9792-5b1cd07a14e9");
        const int votingTime = 60;
        const bool isBreakAllowed = true;
        var expectedResponse = new GetGameSettingsResponse(gameSettingsId, gameId, votingTime, isBreakAllowed);

        _gameEngineClient.GameSettingsResource.GetGameSettings(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameSettingsService.GetGameSettings(gameId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateGameSettings_Success()
    {
        // Arrange
        var gameSettingsId = Guid.Parse("df9ff649-1111-41af-9792-5b1cd07a14e9");
        var gameId = Guid.Parse("df9ff649-2222-41af-9792-5b1cd07a14e9");
        const int votingTime = 60;
        const bool isBreakAllowed = true;
        var expectedResponse = new CreateGameSettingsResponse(gameSettingsId, gameId, votingTime, isBreakAllowed);
        var command = new CreateGameSettingsCommand(gameId, votingTime, isBreakAllowed);

        _gameEngineClient.GameSettingsResource.CreateGameSettings(Arg.Any<CreateGameSettingsCommand>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameSettingsService.CreateGameSettings(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task UpdateGameSettings_Success()
    {
        // Arrange
        var gameSettingsId = Guid.Parse("df9ff649-1111-41af-9792-5b1cd07a14e9");
        var gameId = Guid.Parse("df9ff649-2222-41af-9792-5b1cd07a14e9");
        const int votingTime = 60;
        const bool isBreakAllowed = true;
        var expectedResponse = new UpdateGameSettingsResponse(gameSettingsId, gameId, votingTime, isBreakAllowed);
        var command = new UpdateGameSettingsCommand(gameSettingsId, gameId, votingTime, isBreakAllowed);

        _gameEngineClient.GameSettingsResource.UpdateGameSettings(Arg.Any<UpdateGameSettingsCommand>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameSettingsService.UpdateGameSettings(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }
}