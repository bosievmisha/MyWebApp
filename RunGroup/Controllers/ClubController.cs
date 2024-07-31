using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;
using RunGroup.ViewModels;

namespace RunGroup.Controllers
{
    public class ClubController : Controller
    {
        private readonly IRepository<Club> clubRepository;
        private readonly IPhotoService photoService;
        public ClubController(IRepository<Club> clubRepository, IPhotoService photoService) 
        {
            this.clubRepository = clubRepository;
            this.photoService = photoService;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM) 
        {
            if (ModelState.IsValid)
            {
                var result = await this.photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        State = clubVM.Address.State,
                        City = clubVM.Address.City,
                        Street = clubVM.Address.Street,
                    }
                };
                this.clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else 
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
            
        }
    }
}
