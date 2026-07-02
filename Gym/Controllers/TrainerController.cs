using Gym.BLL.Services.Interfaces;
using Gym.BLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITranierServices trainerServices;
        public TrainerController(ITranierServices trainerServices)
        {
            this.trainerServices = trainerServices;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var trainer = await trainerServices.GetAllTrainerAsync(ct);
            return View(trainer);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainer(CreateTrainerViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(nameof(Create), model);
            var result = await trainerServices.CreateTrainerAsync(model, ct);
            if (result) TempData["Success"] = "Trainer Created Successfuly";
            else TempData["Failed"] = "Trainer Creation Failed";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var trainer = await trainerServices.GetTrainerDetailsAsync(id, ct);
            if (trainer is null) { TempData["ErrorMsg"] = "Trainer not found"; }
            return View(trainer);

        }


        [HttpGet]

        public async Task<IActionResult> Edit(int id ,CancellationToken ct)
        {
            var trainer = await trainerServices.GetTrainerToUpdateAsync(id, ct);
            if (trainer is null) { TempData["ErrorMsg"] = "Trainer not found"; }
            return View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (TrainerToUpdateViewModel model , int id , CancellationToken ct)
        {
            if(!ModelState.IsValid) return  View(nameof(Edit), model);
            var result = await trainerServices.UpdateTrainerDetailsAsync(id, model, ct);
            if (result) TempData["Success"] = "Trainer Updated Successfuly";
            else TempData["Failed"] = "Trainer Update Failed";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id )
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id ,CancellationToken ct)
        {
            var result = await trainerServices.DeleteTrainerDetailsAsync(id, ct);
            if (result) TempData["Success"] = "Trainer Deleted Successfuly";
            else TempData["Failed"] = "Trainer Deleted Failed";
            return RedirectToAction(nameof(Index));
        }
    }
}