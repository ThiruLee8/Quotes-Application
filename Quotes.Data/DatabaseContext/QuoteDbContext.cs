using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quotes.Data.EntityModals;

namespace Quotes.Data.DatabaseContext
{
    public class QuoteDbContext : DbContext
    {
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options):base(options) { }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuoteStage> QuoteStages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuoteStage>().HasData(
            new QuoteStage { QuoteStageId = 1, QuoteStageName = "Created", IsActive = true },
            new QuoteStage { QuoteStageId = 2, QuoteStageName = "Approved Validation", IsActive = true },
            new QuoteStage { QuoteStageId = 3, QuoteStageName = "Rejected Validation", IsActive = true },
            new QuoteStage { QuoteStageId = 4, QuoteStageName = "Approved", IsActive = true },
            new QuoteStage { QuoteStageId = 5, QuoteStageName = "Rejected", IsActive = true }
        );
        }
    }
}
