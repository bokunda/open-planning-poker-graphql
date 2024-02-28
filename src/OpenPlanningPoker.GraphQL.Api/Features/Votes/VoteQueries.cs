namespace OpenPlanningPoker.GraphQL.Api.Features.Votes;

[ExtendObjectType(typeof(Query))]
public class VoteQueries
{
    private readonly IVotingService _votingService;
    private readonly IMapper _mapper;

    public VoteQueries(IVotingService votingService, IMapper mapper)
    {
        _votingService = votingService;
        _mapper = mapper;
    }

    public async Task<ApiCollection<Vote>> GetTicketVotes(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var result = await _votingService.GetTicketVotes(ticketId, cancellationToken);
        return _mapper.Map<ApiCollection<Vote>>(result);
    }
}