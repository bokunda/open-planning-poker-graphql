﻿namespace OpenPlanningPoker.GraphQL.Api.Features.Votes;

public class Vote
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public int Value { get; set; }
}