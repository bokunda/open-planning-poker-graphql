namespace OpenPlanningPoker.GraphQL.Api.Features.Votes;

[ExtendObjectType(typeof(Mutation))]
public class VoteMutations
{
    private readonly IVotingService _votingService;
    private readonly IMapper _mapper;

    public VoteMutations(IVotingService votingService, IMapper mapper)
    {
        _votingService = votingService;
        _mapper = mapper;
    }

    public async Task<Vote> CreateVote(Guid ticketId, int value, CancellationToken cancellationToken = default)
    {
        var result = await _votingService.CreateVote(new CreateVoteCommand(ticketId, value), cancellationToken);
        return _mapper.Map<Vote>(result);
    }

    public async Task<Vote> UpdateVote(Guid id, int value, CancellationToken cancellationToken = default)
    {
        var result = await _votingService.UpdateVote(new UpdateVoteCommand(id, value), cancellationToken);
        return _mapper.Map<Vote>(result);
    }
}