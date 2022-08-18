using System.Text.Json.Serialization;

namespace BismuthAPI.Data.Models;

public sealed record Project(int Id, string Name, string? Description) {
    [JsonIgnore]
    public IList<Issue>? Tasks { get; init; }
}