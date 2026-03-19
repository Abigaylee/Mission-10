using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mission10.Models
{
    public class EFBowlerRepository : IBowlerRepository
    {
        private BowlingLeagueContext _context;

        // Constructor that receives the database context via Dependency Injection
        public EFBowlerRepository(BowlingLeagueContext temp)
        {
            _context = temp;
        }

        // Implementation of the interface property
        // This returns the Bowlers table from the database
        public IQueryable<Bowler> Bowlers => _context.Bowlers;
    }
};