using Newtonsoft.Json;

namespace LojaSeuManuel.Api.Models;

public class PedidoResponse
{
    [JsonProperty("pedidos")] public List<PedidoEmpacotado> Pedidos { get; set; } = new List<PedidoEmpacotado>();
}