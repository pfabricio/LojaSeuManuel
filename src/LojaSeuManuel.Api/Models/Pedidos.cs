using Newtonsoft.Json;

namespace LojaSeuManuel.Api.Models;

public class Pedidos
{
    [JsonProperty("pedido_id")]
    public int PedidoId { get; set; }

    [JsonProperty("produtos")]
    public List<Produtos> Produtos { get; set; }
}