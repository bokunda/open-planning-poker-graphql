namespace OpenPlanningPoker.GraphQL.Service.Features.Votes;

public interface IVotingService
{
    /// <summary>
    /// Returns votes for a ticket - {ticketId}
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<GetVotesItem>> GetTicketVotes(Guid ticketId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a vote
    /// </summary>
    /// <returns></returns>
    Task<CreateVoteResponse> CreateVote(CreateVoteCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a vote
    /// </summary>
    /// <returns></returns>
    Task<UpdateVoteResponse> UpdateVote(UpdateVoteCommand data, CancellationToken cancellationToken = default);
}

public class VotingService : IVotingService
{
    private readonly IGameEngineClient _gameEngineClient;

    public VotingService(IGameEngineClient gameEngineClient)
    {
        _gameEngineClient = gameEngineClient;
    }

    public async Task<ApiCollection<GetVotesItem>> GetTicketVotes(Guid ticketId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.VoteResource.GetTicketVotes(ticketId, cancellationToken);
    }

    public async Task<CreateVoteResponse> CreateVote(CreateVoteCommand data, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.VoteResource.CreateVote(data, cancellationToken);
    }

    public async Task<UpdateVoteResponse> UpdateVote(UpdateVoteCommand data, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.VoteResource.UpdateVote(data, cancellationToken);
    }
}