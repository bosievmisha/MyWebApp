using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;
using RunGroup.Repository;

namespace RunGroup.Controllers
{
    public class ClubController : Controller
    {
        private readonly IRepository<Club> clubRepository;
        public ClubController(IRepository<Club> clubRepository) 
        {
            this.clubRepository = clubRepository;
        }
        public async Task<IActionResult> Index()
        {   
            IEnumerable<Club> clubs = await this.clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club club = await this.clubRepository.GetByIdAsync(id);
            return View(club);
        }
    }
}
