using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DialogController : ControllerBase
    {
        readonly IDialogService _dialogService;
        public DialogController(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDialog([FromBody] DialogDto dialog) 
        {
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{dialogId:Guid}")]
        public async Task<IActionResult> FinishDialog([FromRoute] Guid dialogId)
        {
            return Ok();
        }
    }
}
