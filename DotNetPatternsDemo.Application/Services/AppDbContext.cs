using Microsoft.EntityFrameworkCore;
using AdvancedDotNetPatternsDemo.Domain.Outbox;

public class AppDbContext : DbContext
{
    /* English comment: 
       This constructor is required so that MediatR/DI can pass 
       the connection string and provider settings from Program.cs 
    */
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    // Add your TodoItems here as well if they are missing
    // public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutboxMessage>().ToTable("OutboxMessages");
    }
}