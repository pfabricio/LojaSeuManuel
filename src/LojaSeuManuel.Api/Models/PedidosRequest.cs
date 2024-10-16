using Newtonsoft.Json;

namespace LojaSeuManuel.Api.Models;

public class PedidosRequest
{
    [JsonProperty("pedidos")]
    public List<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
}