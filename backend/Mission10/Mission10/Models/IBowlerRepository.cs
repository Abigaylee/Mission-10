using System.Collections.Generic;
using System.Linq;

namespace Mission10.Models
{
    public interface IBowlerRepository
    {
        // We use IQueryable so we can filter the data (like .Where()) 
        // before it actually hits the database.
        IQueryable<Bowler> Bowlers { get; }
    }
   };
