using Gym.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ISessionsServices sessionsServices;

        public SessionsController(ISessionsServices sessionsServices)
        {
            this.sessionsServices = sessionsServices;
        }
        // GET: SessionsController1
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await sessionsServices.GetAllSessionsAsync(ct);
            return View(sessions);
        }

      
        
    }
}