using Newtonsoft.Json;

namespace LojaSeuManuel.Api.Models;

public class CaixaEmpacotada
{
    public CaixaEmpacotada()
    {
        Produtos = new List<string>();
    }

    [JsonProperty("caixa_id")]
    public string CaixaId { get; set; }

    [JsonProperty("produtos")]
    public List<string> Produtos { get; set; }

    [JsonProperty("observacao")]
    public string Observacao { get; set; }
}