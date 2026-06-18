
using Gym.DAL.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MyApp.Namespace
{
    public class PlanController : Controller
    {
       private readonly IPlanReositories planReositories;

        public PlanController(IPlanReositories _planReositories)
        {
            planReositories = _planReositories;


        }
        public async Task<IActionResult> Index()
        {
            var plans = await planReositories.GetAll();
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

    