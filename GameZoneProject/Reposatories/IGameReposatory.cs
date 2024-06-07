using GameZoneProject.Models;

namespace GameZoneProject.Reposatories
{
    public interface IGameReposatory
    {
        IEnumerable<Game>GetAll();
        Game? GetById(int id);
        Task Create(CreateGameFormViewModel model);
        Task<Game?> Edit(EditGameFormViewModel model);
        bool Delete(int id);
    }
}
 