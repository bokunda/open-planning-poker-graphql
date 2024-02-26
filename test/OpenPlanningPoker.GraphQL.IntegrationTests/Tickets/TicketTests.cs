namespace OpenPlanningPoker.GraphQL.IntegrationTests.Tickets;

public class TicketTests
{
    private readonly ITicketService _ticketService = Substitute.For<ITicketService>();
    private readonly IGameService _gameService = Substitute.For<IGameService>();

    [Fact]
    public async Task GetTicket_Success()
    {
        // Arrange
        const string query = "query { ticket(ticketId: \"0f7a0975-6494-4195-aed2-b49121fce1f7\") { id name description game { id name } } }";

        var ticketId = Guid.Parse("c2bc2b96-06af-4e0d-9861-730db9ffbc4d");
        var gameId = Guid.Parse("14303b64-1661-4cf3-800a-2150ab537d5b");
        const string name = "name1";
        const string description = "description1";

        var apiResponse = new GetTicketResponse(ticketId, gameId, name, description);
        _ticketService.GetTicket(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        _gameService.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new GetGameResponse(gameId, "name1", "description1"));

        var builder = new ServiceCollection()
            .AddSingleton(_ticketService)
            .AddSingleton(_gameService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task GetTickets_Success()
    {
        // Arrange
        const string query = "query { tickets(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\") { items { id name description game { id name } } totalCount } }";

        var gameId = Guid.Parse("4a965cf1-f22c-4d50-b46f-862a79eff7db");

        var firstTicketId = Guid.Parse("ec8c3752-4b17-4c1f-bbaf-608dae753904");
        const string firstName = "name1";
        const string firstDescription = "description1";

        var secondTicketId = Guid.Parse("ec8c3752-1111-4c1f-bbaf-608dae753904");
        const string secondName = "name2";
        const string secondDescription = "description2";

        var firstTicket = new GetTicketsItem(firstTicketId, gameId, firstName, firstDescription);
        var secondTicket = new GetTicketsItem(secondTicketId, gameId, secondName, secondDescription);
        var apiResponse = new ApiCollection<GetTicketsItem>(new List<GetTicketsItem>
        {
            firstTicket,
            secondTicket
        }, 2);

        _ticketService.GetTickets(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        _gameService.Get(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new GetGameResponse(gameId, "name1", "description1"));

        var builder = new ServiceCollection()
            .AddSingleton(_ticketService)
            .AddSingleton(_gameService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task CreateTicket_Success()
    {
        // Arrange
        const string mutation = "mutation { createTicket(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\", name: \"name1\", description: \"description1\") { id gameId name description } }";

        var ticketId = Guid.Parse("c2bc2b96-06af-1111-9861-730db9ffbc4d");
        var gameId = Guid.Parse("4a965cf1-f22c-2222-b46f-862a79eff7db");
        const string name = "name1";
        const string description = "description1";

        var apiResponse = new CreateTicketResponse(ticketId, gameId, name, description);
        _ticketService.CreateTicket(Arg.Any<CreateTicketCommand>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        var builder = new ServiceCollection()
            .AddSingleton(_ticketService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(mutation);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task ImportTicket_Success()
    {
        // Arrange
        const string mutation = "mutation { importTickets(gameId: \"df9ff649-d9df-41af-9792-5b1cd07a14e9\", data: { items: [{ name: \"name1\", description: \"description1\" }, { name: \"name2\", description: \"description2\" }, ], totalCount: 2 }) { items { name description } totalCount } }";

        const string firstName = "name1";
        const string firstDescription = "description1";

        const string secondName = "name2";
        const string secondDescription = "description2";

        var firstTicket = new ImportTicketItem(firstName, firstDescription);
        var secondTicket = new ImportTicketItem(secondName, secondDescription);

        var apiResponse = new ApiCollection<ImportTicketItem>(new List<ImportTicketItem>
        {
            firstTicket,
            secondTicket
        }, 2);

        _ticketService.ImportTickets(Arg.Any<ImportTicketsCommand>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        var builder = new ServiceCollection()
            .AddSingleton(_ticketService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(mutation);

        // Assert
        result.ToJson().MatchSnapshot();
    }
}