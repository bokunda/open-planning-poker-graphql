namespace OpenPlanningPoker.GraphQL.IntegrationTests.Schema;

public class SchemaTests
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        // Arrange
        var schema = await new ServiceCollection()
            .AddGraphQlWithSchema()
            .BuildSchemaAsync();

        // Act
        var schemaAsString = schema.ToString();

        // Assert
        schemaAsString.MatchSnapshot();
    }
}