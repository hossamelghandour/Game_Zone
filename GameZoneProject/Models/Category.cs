using System.ComponentModel.DataAnnotations;

namespace GameZoneProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
