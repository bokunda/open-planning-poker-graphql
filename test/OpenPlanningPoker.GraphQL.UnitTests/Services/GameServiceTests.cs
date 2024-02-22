using OpenPlanningPoker.GameEngine.Api.Models;
using OpenPlanningPoker.GraphQL.Service.Features.GameEngine;

namespace OpenPlanningPoker.GraphQL.UnitTests.Services;

public class GameServiceTests
{
    private readonly IGameService _gameService;
    private readonly IGameEngineClient _gameEngineClient = Substitute.For<IGameEngineClient>();

    public GameServiceTests()
    {
        _gameService = new GameService(_gameEngineClient);
    }

    [Fact]
    public async Task GetGame_Success()
    {
        // Arrange
        var gameId = Guid.Parse("df9ff649-d9df-41af-9792-5b1cd07a14e9");
        const string gameName = "Name";
        const string gameDescription = "Description";
        var expectedResponse = new GetGameResponse(gameId, gameName, gameDescription);

        _gameEngineClient.GameResource.GetGameDetails(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameService.Get(gameId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateGame_Success()
    {
        // Arrange
        var gameId = Guid.Parse("df9ff649-d9df-41af-9792-5b1cd07a14e9");
        const string gameName = "Name";
        const string gameDescription = "Description";
        var expectedResponse = new CreateGameResponse(gameId, gameName, gameDescription);

        _gameEngineClient.GameResource.CreateGame(Arg.Any<CreateGameCommand>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameService.Create(new CreateGameCommand(gameName, gameDescription), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetParticipants_Success()
    {
        // Arrange
        var gameId = Guid.Parse("df9ff649-d9df-41af-9792-5b1cd07a14e9");
        var firstPlayer = new ListPlayersItem(Guid.Parse("d9330d3f-a85b-415a-8136-a454129165a7"), "Name1");
        var secondPlayer = new ListPlayersItem(Guid.Parse("16569daf-a211-43f1-bfe1-a36f6c36e267"), "Name2");

        var expectedResponse = new ApiCollection<ListPlayersItem>(
            new List<ListPlayersItem>
            {
                firstPlayer,
                secondPlayer
            },
            2);

        _gameEngineClient.GameResource.GetParticipants(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameService.GetParticipants(gameId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task JoinGame_Success()
    {
        // Arrange
        var gameId = Guid.Parse("df9ff649-d9df-41af-9792-5b1cd07a14e9");
        var expectedResponse = new JoinGameResponse();

        _gameEngineClient.GameResource.JoinGame(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameService.Join(gameId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task LeaveGame_Success()
    {
        // Arrange
        var gameId = Guid.Parse("df9ff649-d9df-41af-9792-5b1cd07a14e9");
        var expectedResponse = new LeaveGameResponse();

        _gameEngineClient.GameResource.LeaveGame(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _gameService.Leave(gameId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }
}