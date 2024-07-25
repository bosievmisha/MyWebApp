using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroup.Data;
using RunGroup.Models;

namespace RunGroup.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext context;
        public ClubController(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public IActionResult Index()
        {   
            List<Club> clubs = this.context.Clubs.ToList();
            return View(clubs);
        }

        public IActionResult Detail(int id)
        {
            Club club = this.context.Clubs.FirstOrDefault(c => c.Id == id);
            return View(club);
        }
    }
}
