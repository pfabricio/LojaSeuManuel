using Newtonsoft.Json;

namespace LojaSeuManuel.Api.Models;

public class Dimensoes
{
    [JsonProperty("altura")]
    public double Altura { get; set; }

    [JsonProperty("largura")]
    public double Largura { get; set; }

    [JsonProperty("comprimento")]
    public double Comprimento { get; set; }

    public double Volume => Altura * Largura * Comprimento;
}