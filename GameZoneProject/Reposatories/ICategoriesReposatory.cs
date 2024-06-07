using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZoneProject.Reposatories
{
    public interface ICategoriesReposatory
    {
        IEnumerable<SelectListItem>GetSelsectList();
    }
}
