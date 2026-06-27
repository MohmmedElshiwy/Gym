using Gym.BLL.Services.Interfaces;
using Gym.BLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberServices memberServices;

        public MemberController(IMemberServices memberReositories)
        {
            this.memberServices = memberReositories;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await memberServices.GetAllMemberAsync(ct);
            return View(members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> CreateMember(CreateMemberViewModel model,CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(nameof(Create), model);
            await memberServices.CreateMemberAsync(model, ct);

            return Redirect(nameof(Index));

        }
    }
}
