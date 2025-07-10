using Domain.Options.Shared;

namespace Domain.Options;

public class MongoDbSettings : SettingsDbBase
{
    public string Database { get; set; } = string.Empty;
}