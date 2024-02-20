namespace OpenPlanningPoker.GraphQL.Service;

public class AppSettings
{
    public string GameEngineApi { get; set; } = "https://host.docker.internal:6992/api/"; // TODO: Move this to appSettings.json
}