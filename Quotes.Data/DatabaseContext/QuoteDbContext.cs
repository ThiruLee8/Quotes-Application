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
    }
}
