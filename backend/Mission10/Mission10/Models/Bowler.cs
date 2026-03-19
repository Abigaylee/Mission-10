using System.ComponentModel.DataAnnotations;

namespace Mission10.Models
{
    public class Bowler
    {
            [Key]
            public int BowlerID { get; set; }
            public string? BowlerLastName { get; set; }
            public string? BowlerFirstName { get; set; }
            public string? BowlerMiddleInit { get; set; }
            public string? BowlerAddress { get; set; }
            public string? BowlerCity { get; set; }
            public string? BowlerState { get; set; }
            public string? BowlerZip { get; set; }
            public string? BowlerPhoneNumber { get; set; }

            // Foreign Key
            public int TeamID { get; set; }
            public Team? Team { get; set; }
    }
}
