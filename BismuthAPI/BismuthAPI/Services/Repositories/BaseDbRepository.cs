namespace BismuthAPI.Services.Repositories;

public class BaseDbRepository {
    protected readonly DataContext DbContext;

    public BaseDbRepository(DataContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}