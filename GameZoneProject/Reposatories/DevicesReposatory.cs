using GameZoneProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZoneProject.Reposatories
{
    public class DevicesReposatory : IDevicesRepsatory
    {
        private readonly AppDbContext _context;

        public DevicesReposatory(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Devices
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .ToList();
        }
    }
}
