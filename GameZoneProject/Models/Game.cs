using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZoneProject.Models
{
    public class Game
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        [MaxLength(2500)]
        public string Cover { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<GameDevice> Devices { get; set; } = new List<GameDevice>();
    }
}
