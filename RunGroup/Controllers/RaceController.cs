using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroup.Data;
using RunGroup.Models;

namespace RunGroup.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext context;
        public RaceController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            List<Race> races = this.context.Races.ToList();
            return View(races);
        }
    }
}
