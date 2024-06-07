using GameZoneProject.Attributes;
using GameZoneProject.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZoneProject.Models
{
    public class EditGameFormViewModel
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [MaxLength(2500)]
        public string Description { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
