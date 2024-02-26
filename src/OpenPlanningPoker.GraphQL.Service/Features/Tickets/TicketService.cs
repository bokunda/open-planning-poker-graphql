namespace OpenPlanningPoker.GraphQL.Service.Features.Tickets;

public interface ITicketService
{
    /// <summary>
    /// Returns Ticket details - {id}
    /// </summary>
    /// <returns></returns>
    Task<GetTicketResponse> GetTicket(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns Tickets for selected game - game/{gameId}
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<GetTicketsItem>> GetTickets(Guid gameId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a Ticket
    /// </summary>
    /// <returns></returns>
    Task<CreateTicketResponse> CreateTicket(CreateTicketCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a Ticket
    /// </summary>
    /// <returns></returns>
    Task<UpdateTicketResponse> UpdateTicket(UpdateTicketCommand data, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Import a list of Tickets using web API - import
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<ImportTicketItem>> ImportTickets(ImportTicketsCommand data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Import a list of Tickets using a CSV file - import/csv/{gameId}
    /// </summary>
    /// <returns></returns>
    Task<ApiCollection<ImportTicketItem>> ImportTicketsCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a Ticket - {id}
    /// </summary>
    /// <returns></returns>
    Task<DeleteTicketResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default);
}

public class TicketService : ITicketService
{
    private readonly IGameEngineClient _gameEngineClient;

    public TicketService(IGameEngineClient gameEngineClient)
    {
        _gameEngineClient = gameEngineClient;
    }

    public async Task<GetTicketResponse> GetTicket(Guid id, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.TicketResource.GetTicket(id, cancellationToken);
    }

    public async Task<ApiCollection<GetTicketsItem>> GetTickets(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.TicketResource.GetTickets(gameId, cancellationToken);
    }

    public async Task<CreateTicketResponse> CreateTicket(CreateTicketCommand data, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.TicketResource.CreateTicket(data, cancellationToken);
    }

    public async Task<UpdateTicketResponse> UpdateTicket(UpdateTicketCommand data, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.TicketResource.UpdateTicket(data, cancellationToken);
    }

    public async Task<ApiCollection<ImportTicketItem>> ImportTickets(ImportTicketsCommand data, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.TicketResource.ImportTicket(data, cancellationToken);
    }

    public async Task<ApiCollection<ImportTicketItem>> ImportTicketsCsv(Guid gameId, IFormFile file, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.TicketResource.ImportTicketsCsv(gameId, file, cancellationToken);
    }

    public async Task<DeleteTicketResponse> DeleteTicket(Guid id, CancellationToken cancellationToken = default)
    {
        return await _gameEngineClient.TicketResource.DeleteTicket(id, cancellationToken);
    }
}