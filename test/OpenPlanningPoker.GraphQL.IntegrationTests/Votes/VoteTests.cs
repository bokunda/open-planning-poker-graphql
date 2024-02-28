namespace OpenPlanningPoker.GraphQL.IntegrationTests.Votes;

public class VoteTests
{
    private readonly IVotingService _voteService = Substitute.For<IVotingService>();

    [Fact]
    public async Task GetVotes_Success()
    {
        // Arrange
        const string query = "query { ticketVotes(ticketId: \"c2bc2b96-3333-4e0d-9861-730db9ffbc4d\") { items { id playerId value } totalCount } }";

        var firstVote = new GetVotesItem(Guid.Parse("c2bc2b96-1111-4e0d-9861-730db9ffbc4d"), Guid.Parse("c2bc2b96-3333-4e0d-9861-730db9ffbc4d"), 2);
        var secondVote = new GetVotesItem(Guid.Parse("c2bc2b96-2222-4e0d-9861-730db9ffbc4d"), Guid.Parse("c2bc2b96-4444-4e0d-9861-730db9ffbc4d"), 2);

        var votesApiResponse = new ApiCollection<GetVotesItem>(new List<GetVotesItem> { firstVote, secondVote }, 2);

        _voteService.GetTicketVotes(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(votesApiResponse);

        var builder = new ServiceCollection()
            .AddSingleton(_voteService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task CreateVote_Success()
    {
        // Arrange
        const string command = "mutation { createVote(ticketId: \"c2bc2b96-3333-4e0d-9861-730db9ffbc4d\", value: 2) { id playerId value } }";

        var ticketId = Guid.Parse("c2bc2b96-06af-4e0d-9861-730db9ffbc4d");
        const int voteValue = 2;
        var voteApiResponse = new CreateVoteResponse(Guid.Parse("c2bc2b96-5555-4e0d-9861-730db9ffbc4d"), Guid.Parse("c2bc2b96-6666-4e0d-9861-730db9ffbc4d"), ticketId, voteValue);

        _voteService.CreateVote(Arg.Any<CreateVoteCommand>(), Arg.Any<CancellationToken>())
            .Returns(voteApiResponse);

        var builder = new ServiceCollection()
            .AddSingleton(_voteService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(command);

        // Assert
        result.ToJson().MatchSnapshot();
    }

    [Fact]
    public async Task UpdateVote_Success()
    {
        // Arrange
        const string command = "mutation { updateVote(id: \"c2bc2b96-3333-4e0d-9861-730db9ffbc4d\", value: 2) { id playerId value } }";

        var ticketId = Guid.Parse("c2bc2b96-06af-4e0d-9861-730db9ffbc4d");
        const int voteValue = 2;
        var voteApiResponse = new UpdateVoteResponse(Guid.Parse("c2bc2b96-5555-4e0d-9861-730db9ffbc4d"), Guid.Parse("c2bc2b96-6666-4e0d-9861-730db9ffbc4d"), ticketId, voteValue);

        _voteService.UpdateVote(Arg.Any<UpdateVoteCommand>(), Arg.Any<CancellationToken>())
            .Returns(voteApiResponse);

        var builder = new ServiceCollection()
            .AddSingleton(_voteService)
            .AddAutoMapper(typeof(GameMappingProfile).Assembly)
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(command);

        // Assert
        result.ToJson().MatchSnapshot();
    }
}