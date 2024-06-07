using GameZoneProject.Models;
using GameZoneProject.Reposatories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZoneProject.Controllers
{
    public class GamesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoriesReposatory _categoriesReposatory;
        private readonly IDevicesRepsatory _devicesRepsatory;
        private readonly IGameReposatory _gameReposatory;


        public GamesController(AppDbContext context, ICategoriesReposatory categoriesReposatory, IDevicesRepsatory devicesRepsatory, IGameReposatory gameReposatory)
        {
            _context = context;
            _categoriesReposatory = categoriesReposatory;
            _devicesRepsatory = devicesRepsatory;
            _gameReposatory = gameReposatory;
        }

        public IActionResult Index()
        {
            var games = _gameReposatory.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gameReposatory.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            return View(game);
        }

        public IActionResult Create()
        {
            CreateGameFormViewModel ViewModel = new()
            {
                Categories=_categoriesReposatory.GetSelsectList() ,
                Devices = _devicesRepsatory.GetSelectList()
            };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (ModelState.IsValid==true)
            {
                await _gameReposatory.Create(model);
                return RedirectToAction(nameof(Index));
            }
            model.Categories = _categoriesReposatory.GetSelsectList();
            model.Devices = _devicesRepsatory.GetSelectList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var game = _gameReposatory.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id=id,
                Name=game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId,
                SelectedDevices=game.Devices.Select(d=>d.DeviceId).ToList(),
                Categories=_categoriesReposatory.GetSelsectList(),
                Devices=_devicesRepsatory.GetSelectList()
            };
          
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                var game = await _gameReposatory.Edit(model);

                if (game is null)
                    return BadRequest();

                return RedirectToAction(nameof(Index));
            }
            model.Categories = _categoriesReposatory.GetSelsectList();
            model.Devices = _devicesRepsatory.GetSelectList();
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int  id)
        {
            var isDelted = _gameReposatory.Delete(id);
            
            return isDelted? Ok():BadRequest();
        }
    }
}
