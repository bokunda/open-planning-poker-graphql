namespace OpenPlanningPoker.GraphQL.IntegrationTests.Info;

public class InfoTests
{
    [Fact]
    public async Task GetInfo_Success()
    {
        // Arrange
        const string query = "query { info() }";

        var builder = new ServiceCollection()
            .AddGraphQlWithSchema();

        // Act
        var result = await builder.ExecuteRequestAsync(query);

        // Assert
        result.ToJson().MatchSnapshot();
    }
}