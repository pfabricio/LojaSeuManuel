using Newtonsoft.Json;

namespace LojaSeuManuel.Api.Models;

public class Produtos
{
    [JsonProperty("produto_id")]
    public string ProdutoId { get; set; }

    [JsonProperty("dimensoes")]
    public Dimensoes Dimensoes { get; set; }
}