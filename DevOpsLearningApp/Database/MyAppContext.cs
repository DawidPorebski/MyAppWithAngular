using Microsoft.EntityFrameworkCore;

namespace DevOpsLearningApp.Database;

public class MyAppContext : DbContext
{
    public DbSet<MyEntity> MyTable { get; set; } = default!;

    public MyAppContext(DbContextOptions options) : base(options)
    {
    }
}