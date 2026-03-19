using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission10.Models;

[Route("api/[controller]")]
[ApiController]
public class BowlerController : ControllerBase
{
    private IBowlerRepository _repo;
    public BowlerController(IBowlerRepository temp) => _repo = temp;

    [HttpGet]
    public IEnumerable<Bowler> Get()
    {
        return _repo.Bowlers
            .Include(x => x.Team)
            .Where(x => x.Team != null && (x.Team.TeamName == "Marlins" || x.Team.TeamName == "Sharks"))
            .ToArray();
    }
}