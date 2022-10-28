namespace SessionAPI.Configurations;

public sealed class MongoDbConnectionSettings
{
    public const string SectionName = "MongoDbConnection";
    public string DatabaseName { get; init; } = null!;
    public string UsersCollectionName { get; init; } = null!;
}