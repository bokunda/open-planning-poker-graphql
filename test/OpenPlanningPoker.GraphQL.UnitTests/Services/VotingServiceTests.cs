using OpenPlanningPoker.GameEngine.Api.Models.Features.Votes;

namespace OpenPlanningPoker.GraphQL.UnitTests.Services;

public class VotingServiceTests
{
    private readonly IGameEngineClient _gameEngineClient = Substitute.For<IGameEngineClient>();
    private readonly IVotingService _votingService;

    public VotingServiceTests()
    {
        _votingService = new VotingService(_gameEngineClient);
    }

    [Fact]
    public async Task GetVotes_Success()
    {
        // Arrange 
        var ticketId = Guid.Parse("c2bc2b96-06af-4e0d-9861-730db9ffbc4d");
        var firstVote = new GetVotesItem(Guid.Parse("c2bc2b96-1111-4e0d-9861-730db9ffbc4d"), Guid.Parse("c2bc2b96-3333-4e0d-9861-730db9ffbc4d"), 2);
        var secondVote = new GetVotesItem(Guid.Parse("c2bc2b96-2222-4e0d-9861-730db9ffbc4d"), Guid.Parse("c2bc2b96-4444-4e0d-9861-730db9ffbc4d"), 2);

        var votesApiResponse = new ApiCollection<GetVotesItem>(new List<GetVotesItem> { firstVote, secondVote }, 2);

        _gameEngineClient.VoteResource.GetTicketVotes(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(votesApiResponse);

        // Act
        var result = await _votingService.GetTicketVotes(ticketId, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(votesApiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task CreateVote_Success()
    {
        // Arrange 
        var ticketId = Guid.Parse("c2bc2b96-06af-4e0d-9861-730db9ffbc4d");
        const int voteValue = 2;
        var voteApiResponse = new CreateVoteResponse(Guid.Parse("c2bc2b96-5555-4e0d-9861-730db9ffbc4d"), Guid.Parse("c2bc2b96-6666-4e0d-9861-730db9ffbc4d"), ticketId, voteValue);
        var command = new CreateVoteCommand(ticketId, voteValue);

        _gameEngineClient.VoteResource.CreateVote(Arg.Any<CreateVoteCommand>(), Arg.Any<CancellationToken>())
            .Returns(voteApiResponse);

        // Act
        var result = await _votingService.CreateVote(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(voteApiResponse, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task UpdateVote_Success()
    {
        // Arrange 
        var voteId = Guid.Parse("c2bc2b96-5555-4e0d-9861-730db9ffbc4d");
        var ticketId = Guid.Parse("c2bc2b96-06af-4e0d-9861-730db9ffbc4d");
        const int voteValue = 2;
        var voteApiResponse = new UpdateVoteResponse(voteId, Guid.Parse("c2bc2b96-6666-4e0d-9861-730db9ffbc4d"), ticketId, voteValue);
        var command = new UpdateVoteCommand(voteId, voteValue);

        _gameEngineClient.VoteResource.UpdateVote(Arg.Any<UpdateVoteCommand>(), Arg.Any<CancellationToken>())
            .Returns(voteApiResponse);

        // Act
        var result = await _votingService.UpdateVote(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(voteApiResponse, opt => opt.ExcludingMissingMembers());
    }
}