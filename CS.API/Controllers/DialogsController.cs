using CS.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CS.DOM.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        private readonly IDialogService _dialogService;

        public DialogsController(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("{ticketId:Guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // todo finish here

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var roleName = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            var dialogs = await _dialogService.GetAllUserDialogs(userId, roleName);

            return Ok(dialogs);
        }
    }
}