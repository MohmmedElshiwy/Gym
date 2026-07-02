
using Gym.BLL.Services.Classes;
using Gym.BLL.Services.Interfaces;
using Gym.BLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;


namespace MyApp.Namespace
{
    public class PlanController : Controller
    {
       private readonly IPlanServices planServices;

        public PlanController(IPlanServices _planServices)
        {
            planServices = _planServices;


        }
        public async Task<IActionResult> Index(  CancellationToken ct)
        {
            var plans = await planServices.GetAllPlansAsync(ct);
            return View(plans); 
        }
        public async Task<IActionResult> Details(int id)
        {
            var plan = await planServices.GetPlanDetailsAsync(id, CancellationToken.None);
            if(plan == null )RedirectToAction(nameof(Index));
            return View(plan);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var plan = await planServices.GetPlanToUpdateAsync(id, ct);
            return View(plan);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id , UpdatePlanViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid) return  View(nameof(Edit), model);
            var result = await planServices.UpdatedPlanAsync(id, model, ct);
            if (result) TempData["Success"] = "Member Updated Successfuly";
            else TempData["Failed"] = "Member Update Failed";
            return RedirectToAction(nameof(Index));


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await planServices.IsDeletedAsync(id, ct);
            if (result) TempData["Success"] = "Plan Deleted Successfuly";
            else TempData["Failed"] = "Plan Deletion Failed";
            return RedirectToAction(nameof(Index));
        }
    } 
}

    