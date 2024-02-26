using OpenPlanningPoker.GraphQL.Api.Features.Tickets;

namespace OpenPlanningPoker.GraphQL.Api.AutoMapper;

public class TicketMappingProfile : Profile
{
    public TicketMappingProfile()
    {
        CreateMap<GetTicketResponse, Ticket>();
        CreateMap<GetTicketsItem, Ticket>();
        CreateMap<CreateTicketResponse, Ticket>();
        CreateMap<CreateTicketCommand, Ticket>();
        CreateMap<UpdateTicketResponse, Ticket>();
        CreateMap<UpdateTicketCommand, Ticket>();
        CreateMap<ImportTicketsCommand, Ticket>();
    }    
}