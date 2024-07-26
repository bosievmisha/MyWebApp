using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;
using RunGroup.Repository;

namespace RunGroup.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRepository<Race> raceRepository;
        public RaceController(IRepository<Race> raceRepository)
        {
            this.raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await this.raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await this.raceRepository.GetByIdAsync(id);
            return View(race);
        }
    }
}
