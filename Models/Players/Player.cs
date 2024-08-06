
using System.ComponentModel.DataAnnotations;

namespace RPG.Models.Player
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
