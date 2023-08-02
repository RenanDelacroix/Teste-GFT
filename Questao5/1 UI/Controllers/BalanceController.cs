using Microsoft.AspNetCore.Mvc;
using Questao5._2_Application;
using Questao5._2_Application.Interfaces;
using Questao5._3_Domain.DTO;

namespace Questao5._1_UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class BalanceController : Controller
    {
        private readonly IAccountAppService _accountAppService;
        public BalanceController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [ProducesResponseType(typeof(ResultDTO<BalanceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO<BalanceDTO>), StatusCodes.Status400BadRequest)]
        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetBalance([FromRoute] string accountid)
        {
            var result = await _accountAppService.GetAccountBalance(accountid);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
