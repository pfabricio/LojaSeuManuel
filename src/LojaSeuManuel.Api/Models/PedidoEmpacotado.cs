namespace LojaSeuManuel.Api.Models;

public class PedidoEmpacotado
{
    public int PedidoId { get; set; }
    public List<CaixaEmpacotada> Caixas { get; set; } = new List<CaixaEmpacotada>();
}