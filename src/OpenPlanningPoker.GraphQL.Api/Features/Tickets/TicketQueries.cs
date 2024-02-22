namespace OpenPlanningPoker.GraphQL.Api.Features.Tickets;

[ExtendObjectType(typeof(Query))]
public class TicketQueries
{
    private readonly ITicketService _ticketService;
    private readonly IMapper _mapper;

    public TicketQueries(ITicketService ticketService, IMapper mapper)
    {
        _ticketService = ticketService;
        _mapper = mapper;
    }

    public async Task<Ticket> GetTicket(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var result = await _ticketService.GetTicket(ticketId, cancellationToken);
        return _mapper.Map<Ticket>(result);
    }

    public async Task<ApiCollection<Ticket>> GetTickets(Guid gameId, CancellationToken cancellationToken = default)
    {
        var result = await _ticketService.GetTickets(gameId, cancellationToken);
        return _mapper.Map<ApiCollection<Ticket>>(result);
    }
}