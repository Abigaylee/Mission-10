using Microsoft.EntityFrameworkCore;

namespace Mission10.Models
{
    public class BowlingLeagueContext : DbContext
    {
        // Constructor that passes the configuration settings (like the connection string) 
        // to the base DbContext class.
        public BowlingLeagueContext(DbContextOptions<BowlingLeagueContext> options)
            : base(options)
        {
        }

        // These DbSets represent the tables in your database.
        // The names here (Bowlers, Teams) should match the table names in SQLite.
        public DbSet<Bowler> Bowlers { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
