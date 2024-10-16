using LojaSeuManuel.Api.Models;
using LojaSeuManuel.Api.Services.Interfaces;
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
        private readonly IEmpacotamentoService _empacotamentoService;
        public EmpacotamentoController(IEmpacotamentoService empacotamentoService)
        {
            _empacotamentoService = empacotamentoService;
        }

        [HttpPost("processarpedidos")]
        public IActionResult ProcessarPedidos([FromBody] PedidosRequest? pedidosRequest)
        {
            try
            {
                var resultado = _empacotamentoService.EmpacotarPedidos(pedidosRequest.Pedidos);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
