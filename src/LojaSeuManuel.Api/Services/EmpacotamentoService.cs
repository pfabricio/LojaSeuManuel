using LojaSeuManuel.Api.Models;

namespace LojaSeuManuel.Api.Services;

public class EmpacotamentoService
{
    private readonly List<Caixas> _caixasDisponiveis = new List<Caixas>
    {
        new Caixas { CaixaId = "Caixa 1", Dimensoes = new Dimensoes { Altura = 30, Largura = 40, Comprimento = 80 } },
        new Caixas { CaixaId = "Caixa 2", Dimensoes = new Dimensoes { Altura = 80, Largura = 50, Comprimento = 40 } },
        new Caixas { CaixaId = "Caixa 3", Dimensoes = new Dimensoes { Altura = 50, Largura = 80, Comprimento = 60 } }
    };

    public List<PedidoEmpacotado> EmpacotarPedidos(List<Pedidos> pedidos)
    {
        var pedidosEmpacotados = new List<PedidoEmpacotado>();

        foreach (var pedido in pedidos)
        {
            var pedidoEmpacotado = new PedidoEmpacotado { PedidoId = pedido.PedidoId };
            var caixasUsadas = new Dictionary<string, Caixas>();

            foreach (var produto in pedido.Produtos)
            {
                bool produtoEmpacotado = false;
                foreach (var caixa in _caixasDisponiveis)
                {
                    if (caixa.CabeNaCaixa(produto.Dimensoes.Volume))
                    {
                        if (!caixasUsadas.ContainsKey(caixa.CaixaId))
                        {
                            caixasUsadas[caixa.CaixaId] = caixa;
                        }

                        caixasUsadas[caixa.CaixaId].Produtos.Add(produto.ProdutoId);
                        caixasUsadas[caixa.CaixaId].EspacoUsado += produto.Dimensoes.Volume;
                        produtoEmpacotado = true;
                        break;
                    }
                }

                if (!produtoEmpacotado)
                {
                    pedidoEmpacotado.Caixas.Add(new CaixaEmpacotada
                    {
                        CaixaId = null,
                        Produtos = new List<string>{ produto.ProdutoId },
                        Observacao = "Produto não cabe em nenhuma caixa disponível."
                    });
                }
            }

            foreach (var caixaUsada in caixasUsadas.Values)
            {
                pedidoEmpacotado.Caixas.Add(new CaixaEmpacotada
                {
                    CaixaId = caixaUsada.CaixaId,
                    Produtos = new List<string>(caixaUsada.Produtos)
                });
            }

            pedidosEmpacotados.Add(pedidoEmpacotado);
        }

        return pedidosEmpacotados;
    }
}