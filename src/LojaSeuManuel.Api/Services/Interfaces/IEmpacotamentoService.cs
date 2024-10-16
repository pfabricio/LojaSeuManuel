using LojaSeuManuel.Api.Models;

namespace LojaSeuManuel.Api.Services.Interfaces;

public interface IEmpacotamentoService
{
   PedidoResponse EmpacotarPedidos(List<Pedidos> pedidos);
}