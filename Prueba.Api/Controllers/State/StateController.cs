using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba.Api.Domains.State;

namespace Prueba.Api.Controllers.State
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController(IStateDomainGetStateList stateDomain) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetState()
        {
            var response = await stateDomain.GetState();
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
