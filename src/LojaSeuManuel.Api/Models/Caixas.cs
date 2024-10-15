namespace LojaSeuManuel.Api.Models;

public class Caixas
{
    public string CaixaId { get; set; }
    public Dimensoes Dimensoes { get; set; }
    public double EspacoUsado { get; set; }
    public List<string> Produtos { get; set; } = new List<string>();

    public double EspacoDisponivel => Dimensoes.Volume - EspacoUsado;

    public bool CabeNaCaixa(double volumeProduto)
    {
        return EspacoDisponivel >= volumeProduto;
    }
}