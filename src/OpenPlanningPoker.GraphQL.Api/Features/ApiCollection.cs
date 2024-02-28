namespace OpenPlanningPoker.GraphQL.Api.Features;

public class ApiCollection<T>
{
    public ApiCollection(ICollection<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public ICollection<T> Items { get; set; }
    public int TotalCount { get; set; }
}