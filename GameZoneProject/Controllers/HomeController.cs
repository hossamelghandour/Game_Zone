using GameZoneProject.Models;
using GameZoneProject.Reposatories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZoneProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameReposatory _gamesReposatory;

        public HomeController(IGameReposatory gamesReposatory)
        {
            _gamesReposatory = gamesReposatory;
        }

        public IActionResult Index()
        {
            var games = _gamesReposatory.GetAll();
            return View(games);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}