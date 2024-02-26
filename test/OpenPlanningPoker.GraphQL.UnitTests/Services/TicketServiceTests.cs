using OpenPlanningPoker.GameEngine.Api.Models.Features.Tickets;

namespace OpenPlanningPoker.GraphQL.UnitTests.Services;

public class TicketServiceTests
{
    private readonly IGameEngineClient _gameEngineClient = Substitute.For<IGameEngineClient>();
    private readonly ITicketService _ticketService;

    public TicketServiceTests()
    {
        _ticketService = new TicketService(_gameEngineClient);
    }

    [Fact]
    public async Task GetTicket_Success()
    {
        // Arrange
        var ticketId = Guid.Parse("c2bc2b96-06af-4e0d-9861-730db9ffbc4d");
        var gameId = Guid.Parse("14303b64-1661-4cf3-800a-2150ab537d5b");
        const string name = "name1";
        const string description = "description1";

        var apiResponse = new GetTicketResponse(ticketId, gameId, name, description);
        _gameEngineClient.TicketResource.GetTicket(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        // Act
        var result = await _ticketService.GetTicket(ticketId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(apiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetTickets_Success()
    {
        // Arrange
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

        _gameEngineClient.TicketResource.GetTickets(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        // Act
        var result = await _ticketService.GetTickets(gameId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(apiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task CreateTicket_Success()
    {
        // Arrange
        var ticketId = Guid.Parse("c2bc2b96-06af-1111-9861-730db9ffbc4d");
        var gameId = Guid.Parse("4a965cf1-f22c-2222-b46f-862a79eff7db");
        const string name = "name1";
        const string description = "description1";

        var command = new CreateTicketCommand(gameId, name, description);
        
        var apiResponse = new CreateTicketResponse(ticketId, gameId, name, description);
        _gameEngineClient.TicketResource.CreateTicket(Arg.Any<CreateTicketCommand>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        // Act
        var result = await _ticketService.CreateTicket(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(apiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task UpdateTicket_Success()
    {
        // Arrange
        var ticketId = Guid.Parse("c2bc2b96-06af-4444-9861-730db9ffbc4d");
        var gameId = Guid.Parse("4a965cf1-f22c-5555-b46f-862a79eff7db");
        const string name = "name1";
        const string description = "description1";

        var command = new UpdateTicketCommand(ticketId, name, description);

        var apiResponse = new UpdateTicketResponse(ticketId, gameId, name, description);
        _gameEngineClient.TicketResource.UpdateTicket(Arg.Any<UpdateTicketCommand>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        // Act
        var result = await _ticketService.UpdateTicket(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(apiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ImportTickets_Success()
    {
        // TODO: Missing ticketId in response!
        var gameId = Guid.Parse("1a965cf1-1111-4d50-b46f-862a79eff7db");

        var firstTicketId = Guid.Parse("2c8c3752-2222-4c1f-bbaf-608dae753904");
        const string firstName = "name1";
        const string firstDescription = "description1";

        var secondTicketId = Guid.Parse("1c8c3752-3333-4c1f-bbaf-608dae753904");
        const string secondName = "name2";
        const string secondDescription = "description2";

        var firstTicket = new ImportTicketItem(firstName, firstDescription);
        var secondTicket = new ImportTicketItem(secondName, secondDescription);

        var apiResponse = new ApiCollection<ImportTicketItem>(new List<ImportTicketItem>
        {
            firstTicket,
            secondTicket
        }, 2);

        _gameEngineClient.TicketResource.ImportTicket(Arg.Any<ImportTicketsCommand>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        var command = new ImportTicketsCommand(gameId, apiResponse.Items);

        // Act
        var result = await _ticketService.ImportTickets(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(apiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task ImportTicketsCsv_Success()
    {
        // TODO: Missing ticketId in response!
        var gameId = Guid.Parse("1a965cf1-1111-4d50-2222-862a79eff7db");

        var firstTicketId = Guid.Parse("2c8c3752-2222-4c1f-3333-608dae753904");
        const string firstName = "name1";
        const string firstDescription = "description1";

        var secondTicketId = Guid.Parse("1c8c3752-3333-4c1f-4444-608dae753904");
        const string secondName = "name2";
        const string secondDescription = "description2";

        var firstTicket = new ImportTicketItem(firstName, firstDescription);
        var secondTicket = new ImportTicketItem(secondName, secondDescription);

        var apiResponse = new ApiCollection<ImportTicketItem>(new List<ImportTicketItem>
        {
            firstTicket,
            secondTicket
        }, 2);

        _gameEngineClient.TicketResource.ImportTicketsCsv(Arg.Any<Guid>(), Arg.Any<IFormFile>(), Arg.Any<CancellationToken>())
            .Returns(apiResponse);

        using var stream = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(apiResponse));
        var formFile = new FormFile(stream, 0, stream.Length, "stream", "csv");

        // Act
        var result = await _ticketService.ImportTicketsCsv(gameId, formFile, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(apiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task DeleteTicket_Success()
    {
        // Arrange
        var ticketId = Guid.Parse("2c8c3752-2222-4c1f-3333-608dae753904");
        _gameEngineClient.TicketResource.DeleteTicket(ticketId)
            .Returns(new DeleteTicketResponse());

        // Act
        var result = await _ticketService.DeleteTicket(ticketId, CancellationToken.None);

        // Assert
        result.Should().BeOfType(typeof(DeleteTicketResponse));
    }
}