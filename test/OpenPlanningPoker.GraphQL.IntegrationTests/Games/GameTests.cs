using OpenPlanningPoker.GameEngine.Api.Models;
using OpenPlanningPoker.GraphQL.Service.Features.GameEngine;

namespace OpenPlanningPoker.GraphQL.IntegrationTests.Games;

public class GameTests
{
    private readonly IGameService _gameService = Substitute.For<IGameService>();
    private readonly IGameSettingsService _gameSettingsService = Substitute.For<IGameSettingsService>();

    [Fact]
    public async Task GetGame_WithoutResolvers_Success()
    {
        // Arrange
        const string query = "query { game(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\") { id name description } }";

        var gameId = Guid.Parse("f3ac3173-837d-4d2d-84f3-037ba450f503");
        const string gameName = "name";
        const string gameDescription = "description";

        _gameService.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new GetGameResponse(gameId, gameName, gameDescription));

        var builder = new ServiceCollection()
            .AddSingleton(_gameService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task GetGame_WithPlayersResolver_Success()
    {
        // Arrange
        const string query = "query { game(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\") { id name description players { items { id name } totalCount } settings { id votingTime game { players { items { name } } } } } }";

        var gameId = Guid.Parse("f3ac3173-837d-4d2d-84f3-037ba450f503");
        const string gameName = "name";
        const string gameDescription = "description";

        var gameSettingsId = Guid.Parse("df9ff649-1111-41af-9792-5b1cd07a14e9");
        const int votingTime = 60;
        const bool isBreakAllowed = true;
        var expectedResponseGameSettings = new GetGameSettingsResponse(gameSettingsId, gameId, votingTime, isBreakAllowed);

        _gameService.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new GetGameResponse(gameId, gameName, gameDescription));

        _gameService.GetParticipants(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new ApiCollection<ListPlayersItem>(
                new List<ListPlayersItem>
                {
                    new (Guid.Parse("eca9f3f0-1777-4507-9880-845ccfe241d5"), "Name1"),
                    new (Guid.Parse("513e1a3a-5719-4179-bdfb-b80f194e8282"), "Name2")
                },
                2));

        _gameSettingsService.GetGameSettings(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(expectedResponseGameSettings);

        var builder = new ServiceCollection()
            .AddSingleton(_gameService)
            .AddSingleton(_gameSettingsService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task GetGameParticipants_Success()
    {
        // Arrange
        const string query = "query { players(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\") { items { id name } totalCount } }";

        var gameId = Guid.Parse("f3ac3173-837d-4d2d-84f3-037ba450f503");

        _gameService.GetParticipants(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new ApiCollection<ListPlayersItem>(
                new List<ListPlayersItem>
                {
                    new (Guid.Parse("eca9f3f0-1777-4507-9880-845ccfe241d5"), "Name1"),
                    new (Guid.Parse("513e1a3a-5719-4179-bdfb-b80f194e8282"), "Name2")
                },
                2));

        var builder = new ServiceCollection()
            .AddSingleton(_gameService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task CreateGame_Success()
    {
        // Arrange
        const string command = "mutation { createGame(name: \"name\", description: \"description\") { id name description } }";

        var gameId = Guid.Parse("f3ac3173-837d-4d2d-84f3-037ba450f503");
        const string gameName = "name";
        const string gameDescription = "description";

        _gameService.Create(Arg.Any<CreateGameCommand>(), Arg.Any<CancellationToken>())
            .Returns(new CreateGameResponse(gameId, gameName, gameDescription));

        var builder = new ServiceCollection()
            .AddSingleton(_gameService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(command);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task JoinGame_Success()
    {
        // Arrange
        const string command = "mutation { joinGame(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\") { gameId userId } }";

        _gameService.Join(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new JoinGameResponse());

        var builder = new ServiceCollection()
            .AddSingleton(_gameService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(command);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task LeaveGame_Success()
    {
        // Arrange
        const string command = "mutation { leaveGame(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\") { gameId userId } }";

        _gameService.Leave(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new LeaveGameResponse());

        var builder = new ServiceCollection()
            .AddSingleton(_gameService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(command);

        // Assert
        result.ToJson().MatchSnapshot();
    }
}