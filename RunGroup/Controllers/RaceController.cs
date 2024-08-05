using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroup.Data;
using RunGroup.Interfaces;
using RunGroup.Models;
using RunGroup.ViewModels;

namespace RunGroup.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRepository<Race> raceRepository;
        private readonly IPhotoService photoService;
        public RaceController(IRepository<Race> raceRepository, IPhotoService photoService)
        {
            this.raceRepository = raceRepository;
            this.photoService = photoService;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await this.photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        State = raceVM.Address.State,
                        City = raceVM.Address.City,
                        Street = raceVM.Address.Street,
                    }
                };
                this.raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceVM);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var race = await this.raceRepository.GetByIdAsync(id);
            if (race == null)
                return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory
            };
            return View(raceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", raceVM);
            }
            var userClub = await this.raceRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await this.photoService.DeletePhotoAsync(userClub.Image);
                }
                catch
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(raceVM);
                }
                var photoResult = await this.photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                };

                this.raceRepository.Update(race);

                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }

        }
    }
}
