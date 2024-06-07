using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZoneProject.Reposatories
{
    public interface IDevicesRepsatory
    {
        IEnumerable<SelectListItem> GetSelectList(); 
    }
}
