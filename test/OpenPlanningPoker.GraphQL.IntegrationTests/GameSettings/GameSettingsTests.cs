using OpenPlanningPoker.GraphQL.Service.Features.GameSettings;

namespace OpenPlanningPoker.GraphQL.IntegrationTests.GameSettings;

public class GameSettingsTests
{
    private readonly IGameService _gameService = Substitute.For<IGameService>();
    private readonly IGameSettingsService _gameSettingsService = Substitute.For<IGameSettingsService>();
    private readonly IGameEngineClient _gameEngineClient = Substitute.For<IGameEngineClient>();


    [Fact]
    public async Task GetGameSettings_WithResolvers_Success()
    {
        // Arrange
        const string query = "query { gameSettings(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\") { id gameId votingTime isBreakAllowed game { name description players { items { id name } totalCount } } } }";

        var gameSettingsId = Guid.Parse("df9ff649-1111-41af-9792-5b1cd07a14e9");
        var gameId = Guid.Parse("df9ff649-2222-41af-9792-5b1cd07a14e9");
        const int votingTime = 60;
        const bool isBreakAllowed = true;
        var expectedResponseGameSettings = new GetGameSettingsResponse(gameSettingsId, gameId, votingTime, isBreakAllowed);

        const string gameName = "Name";
        const string gameDescription = "Description";
        var expectedResponseGame = new GetGameResponse(gameId, gameName, gameDescription);

        _gameService.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponseGame);

        _gameSettingsService.GetGameSettings(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponseGameSettings);

        var firstPlayer = new ListPlayersItem(Guid.Parse("d9330d3f-a85b-415a-8136-a454129165a7"), "Name1");
        var secondPlayer = new ListPlayersItem(Guid.Parse("16569daf-a211-43f1-bfe1-a36f6c36e267"), "Name2");

        var expectedResponsePlayers = new ApiCollection<ListPlayersItem>(
            new List<ListPlayersItem>
            {
                firstPlayer,
                secondPlayer
            },
            2);

        _gameService.GetParticipants(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponsePlayers);

        var builder = new ServiceCollection()
            .AddSingleton(_gameService)
            .AddSingleton(_gameSettingsService)
            .AddSingleton(_gameEngineClient)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task CreateGameSettings_Success()
    {
        // Arrange
        const string mutation = "mutation { createGameSettings( gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\", isBreakAllowed: true, votingTime: 60) { id gameId votingTime isBreakAllowed } }";

        var gameSettingsId = Guid.Parse("df9ff649-1111-41af-9792-5b1cd07a14e9");
        var gameId = Guid.Parse("df9ff649-2222-41af-9792-5b1cd07a14e9");
        const int votingTime = 60;
        const bool isBreakAllowed = true;
        var expectedResponseGameSettings = new CreateGameSettingsResponse(gameSettingsId, gameId, votingTime, isBreakAllowed);

        _gameSettingsService.CreateGameSettings(Arg.Any<CreateGameSettingsCommand>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponseGameSettings);

        var builder = new ServiceCollection()
            .AddSingleton(_gameSettingsService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(mutation);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task UpdateGameSettings_Success()
    {
        // Arrange
        const string mutation = "mutation { updateGameSettings( id: \"df9ff649-1111-41af-9792-5b1cd07a14e9\", gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\", isBreakAllowed: true, votingTime: 60) { id gameId votingTime isBreakAllowed } }";

        var gameSettingsId = Guid.Parse("df9ff649-1111-41af-9792-5b1cd07a14e9");
        var gameId = Guid.Parse("df9ff649-2222-41af-9792-5b1cd07a14e9");
        const int votingTime = 60;
        const bool isBreakAllowed = true;
        var expectedResponseGameSettings = new UpdateGameSettingsResponse(gameSettingsId, gameId, votingTime, isBreakAllowed);

        _gameSettingsService.UpdateGameSettings(Arg.Any<UpdateGameSettingsCommand>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponseGameSettings);

        var builder = new ServiceCollection()
            .AddSingleton(_gameSettingsService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(mutation);

        // Assert
        result.ToJson().MatchSnapshot();
    }
}