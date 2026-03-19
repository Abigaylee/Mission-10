using System.ComponentModel.DataAnnotations;

namespace Mission10.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; } = null!;
    }
}
