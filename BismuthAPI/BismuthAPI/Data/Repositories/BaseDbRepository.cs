namespace BismuthAPI.Data.Repositories;

public class BaseDbRepository
{
    protected readonly DataContext _dbContext;

    public BaseDbRepository(DataContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}