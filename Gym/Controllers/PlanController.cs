
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MyApp.Namespace
{
    public class PlanController : Controller
    {
       private readonly IGenaricRepo<Plan> planReositories;

        public PlanController(IGenaricRepo<Plan> _planReositories)
        {
            planReositories = _planReositories;


        }
        public async Task<IActionResult> Index(bool isTraked , CancellationToken ct)
        {
            var plans = await planReositories.GetAll(false,ct);
            return View(plans); 
        }
        public async Task<IActionResult> Details(int id)
        {
            var plan = await planReositories.GetById(id);
            if(plan == null )RedirectToAction(nameof(Index));
            return View(plan);
        }
    } 
}

    