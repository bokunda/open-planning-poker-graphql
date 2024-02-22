namespace OpenPlanningPoker.GraphQL.Api.Features.Tickets;

[ExtendObjectType(typeof(Mutation))]
public class TicketMutations
{
    private readonly ITicketService _ticketService;
    private readonly IMapper _mapper;

    public TicketMutations(ITicketService ticketService, IMapper mapper)
    {
        _ticketService = ticketService;
        _mapper = mapper;
    }

    public async Task<CreateTicketResponse> CreateTicket(Guid gameId, string name, string description, CancellationToken cancellationToken = default)
    {
        var result = await _ticketService.CreateTicket(new CreateTicketCommand(gameId, name, description), cancellationToken);
        return _mapper.Map<CreateTicketResponse>(result);
    }

    public async Task<bool> DeleteTicket(Guid ticketId, CancellationToken cancellationToken = default)
    {
        var result = await _ticketService.DeleteTicket(ticketId, cancellationToken);
        return true;
    }

    public async Task<ApiCollection<ImportTicketItem>> ImportTickets(Guid gameId, ApiCollection<ImportTicketItem> data, CancellationToken cancellationToken = default)
    {
        var result = await _ticketService.ImportTicket(new ImportTicketsCommand(gameId, data.Items), cancellationToken);
        return _mapper.Map<ApiCollection<ImportTicketItem>>(result);
    }

    //public async Task<ApiCollection<ImportTicketItem>> ImportTicketsCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default)
    //{
    //    var result = await _ticketService.ImportTicketsCsv(gameId, file, cancellationToken);
    //    return _mapper.Map<ApiCollection<ImportTicketItem>>(result);
    //}
}