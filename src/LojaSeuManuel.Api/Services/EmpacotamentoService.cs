using LojaSeuManuel.Api.Models;
using LojaSeuManuel.Api.Services.Interfaces;

namespace LojaSeuManuel.Api.Services;

public class EmpacotamentoService: IEmpacotamentoService
{
    private readonly List<Caixas> _caixasDisponiveis = new List<Caixas>
    {
        new Caixas { CaixaId = "Caixa 1", Dimensoes = new Dimensoes { Altura = 30, Largura = 40, Comprimento = 80 } },
        new Caixas { CaixaId = "Caixa 2", Dimensoes = new Dimensoes { Altura = 80, Largura = 50, Comprimento = 40 } },
        new Caixas { CaixaId = "Caixa 3", Dimensoes = new Dimensoes { Altura = 50, Largura = 80, Comprimento = 60 } }
    };

    public PedidoResponse EmpacotarPedidos(List<Pedidos> pedidos)
    {
        var pedidosEmpacotados = new PedidoResponse();

        foreach (var pedido in pedidos)
        {
            var pedidoEmpacotado = new PedidoEmpacotado { PedidoId = pedido.PedidoId };
            var produtosRestantes = new List<Produtos>(pedido.Produtos);

            produtosRestantes.Sort((a, b) => a.Dimensoes.Volume.CompareTo(b.Dimensoes.Volume));

            while (produtosRestantes.Any())
            {
                var caixaUsada = new CaixaEmpacotada { Produtos = new List<string>() };
                bool caixaSelecionada = false;

                foreach (var produto in produtosRestantes.ToList())
                {
                    Caixas melhorCaixa = EncontrarMelhorCaixa(produto);

                    if (melhorCaixa != null)
                    {
                        caixaUsada.CaixaId = melhorCaixa.CaixaId;
                        caixaUsada.Produtos.Add(produto.ProdutoId);
                        melhorCaixa.EspacoUsado += produto.Dimensoes.Volume;

                        produtosRestantes.Remove(produto);
                        caixaSelecionada = true;
                    }
                }

                if (!caixaSelecionada)
                {
                    foreach (var produto in produtosRestantes)
                    {
                        pedidoEmpacotado.Caixas.Add(new CaixaEmpacotada
                        {
                            CaixaId = null,
                            Produtos = new List<string> { produto.ProdutoId },
                            Observacao = "Produto não cabe em nenhuma caixa disponível."
                        });
                    }
                    break;
                }

                pedidoEmpacotado.Caixas.Add(caixaUsada);
            }

            pedidosEmpacotados.Pedidos.Add(pedidoEmpacotado);
        }

        return pedidosEmpacotados;
    }

    private Caixas EncontrarMelhorCaixa(Produtos produto)
    {
        foreach (var caixa in _caixasDisponiveis.OrderBy(c => c.Dimensoes.Volume))
        {
            if (caixa.CabeNaCaixa(produto.Dimensoes.Volume))
            {
                return caixa;
            }
        }

        return null;
    }
}