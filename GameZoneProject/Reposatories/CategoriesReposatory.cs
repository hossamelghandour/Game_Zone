using GameZoneProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZoneProject.Reposatories
{
    public class CategoriesReposatory : ICategoriesReposatory
    {
        private readonly AppDbContext _context;

        public CategoriesReposatory(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelsectList()
        {
            return _context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .ToList();
        }
    }
}
