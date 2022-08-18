namespace BismuthAPI.Data.Models;

public sealed record Issue(
    int Id,
    string Title,
    string Description,
    int ProjectId
);