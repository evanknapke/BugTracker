using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public class IssueContext : DbContext
    {
        public IssueContext(DbContextOptions<IssueContext> options)
            : base(options)
        {
        }

        public DbSet<IssueItem> IssueItems { get; set; }
    }
}