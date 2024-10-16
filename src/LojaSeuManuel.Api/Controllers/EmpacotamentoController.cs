using LojaSeuManuel.Api.Models;
using LojaSeuManuel.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LojaSeuManuel.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpacotamentoController : ControllerBase
    {
        private readonly EmpacotamentoService _empacotamentoService;
        public EmpacotamentoController(EmpacotamentoService empacotamentoService)
        {
            _empacotamentoService = empacotamentoService;
        }

        [HttpPost("processarpedidos")]
        public IActionResult ProcessarPedidos([FromBody] PedidosRequest? pedidosRequest)
        {
            try
            {
                var resultado = _empacotamentoService.EmpacotarPedidos(pedidosRequest.Pedidos);
                var jsonResultado = JsonConvert.SerializeObject(resultado, Newtonsoft.Json.Formatting.Indented);
                return Ok(jsonResultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
