using System.ComponentModel.DataAnnotations;

namespace GameZoneProject.Models
{
    public class Device
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Icon { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual List<GameDevice> GameDevices { get; set; }
    }
}
