using Microsoft.AspNetCore.Mvc;
using Questao5._2_Application.Commands.Requests;
using Questao5._2_Application.Interfaces;
using Questao5._3_Domain.DTO;

namespace Questao5._1_UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TransactionController : Controller
    {
        private readonly IAccountAppService _accountAppService;
        public TransactionController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        
        [ProducesResponseType(typeof(ResultDTO<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultDTO<dynamic>), StatusCodes.Status400BadRequest)]
        [HttpPost("")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCommand command, [FromHeader] Guid idempotency_key, [FromQuery] TransactionTypeCommand type)
        {
            if (idempotency_key == Guid.Empty)
                return BadRequest("Idempotency Key is missing");

            var result = await _accountAppService.Transaction(command.AccountId, idempotency_key, type.TransactionType, command.Amount);
            if(result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
