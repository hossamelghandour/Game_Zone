using GameZoneProject.Models;
using GameZoneProject.Settings;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GameZoneProject.Reposatories
{
    public class GameReposatory : IGameReposatory
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GameReposatory(AppDbContext context, IWebHostEnvironment webHostEnvironment) 
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagaesPath}";
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g=>g.Category)
                .Include(g=>g.Devices)
                .ThenInclude(d=>d.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
              .Include(g => g.Category)
              .Include(g => g.Devices)
              .ThenInclude(d => d.Device)
              .AsNoTracking()
              .SingleOrDefault(g => g.Id == id);
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var CoverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name=model.Name,
                Description=model.Description,
                CategoryId=model.CategoryId,
                Cover=CoverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice {DeviceId=d }).ToList()
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(g=>g.Devices)
                .SingleOrDefault(g=>g.Id==model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices=model.SelectedDevices.Select(d=>new GameDevice { DeviceId=d}).ToList();

            if (hasNewCover)
            {
                game.Cover= await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);

                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(id);

            if (game is null)
                return isDeleted;

            _context.Games.Remove(game);
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

        private async Task<string>SaveCover(IFormFile Cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imagesPath, CoverName);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            
            return CoverName;
        }

       
    }
}
