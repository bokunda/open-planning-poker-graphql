namespace OpenPlanningPoker.GraphQL.Api.AutoMapper;

public class VoteMappingProfile : Profile
{
    public VoteMappingProfile()
    {
        CreateMap<GetVotesItem, Vote>();
        CreateMap<CreateVoteCommand, Vote>();
        CreateMap<CreateVoteResponse, Vote>();
        CreateMap<UpdateVoteCommand, Vote>();
        CreateMap<UpdateVoteResponse, Vote>();
    }
}