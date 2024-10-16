using System.Security.Claims;
using FluentAssertions;
using LojaSeuManuel.Api.Controllers;
using LojaSeuManuel.Api.Models;
using LojaSeuManuel.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LojaSeuManuel.Api.Test
{
    public class EmpacotamentoTest
    {
        private readonly Mock<IEmpacotamentoService> _mockEmpacotamentoService;
        private readonly EmpacotamentoController _controller;

        public EmpacotamentoTest()
        {
            _mockEmpacotamentoService = new Mock<IEmpacotamentoService>();
            _controller = new EmpacotamentoController(_mockEmpacotamentoService.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, "Test User"),
                new(ClaimTypes.Role, "Admin")
            }, "mock"));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public void VerificarEmpacotamentoTest()
        {
            var entrada = new PedidosRequest
            {
                Pedidos = new List<Pedidos>
                {
                    new()
                    {
                        PedidoId = 1,
                        Produtos = new List<Produtos>
                        {
                            new Produtos {
                                ProdutoId = "Produto1",
                                Dimensoes = new Dimensoes{ Altura = 10, Largura = 20, Comprimento = 30}
                            },
                            new Produtos {
                                ProdutoId = "Produto2",
                                Dimensoes =  new Dimensoes { Altura = 5, Largura = 10, Comprimento = 1 }
                            }
                        }
                    }
                }
            };

            var saidaMock = new PedidoResponse
            {
                Pedidos = new List<PedidoEmpacotado>
                {
                    new()
                    {
                        PedidoId = 1,
                        Caixas = new List<CaixaEmpacotada>
                        {
                            new CaixaEmpacotada
                            {
                                CaixaId = "Caixa 1",
                                Produtos = new List<string> { "Produto1", "Produto2" }
                            }
                        }
                    }
                }
            };

            _mockEmpacotamentoService.Setup(service => service.EmpacotarPedidos(It.IsAny<List<Pedidos>>()))
                .Returns(saidaMock);

            var result = _controller.ProcessarPedidos(entrada);

            var okResult = result as OkObjectResult;
            okResult.Value.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var saida = okResult.Value as PedidoResponse;
            saida.Should().NotBeNull();
            saida.Pedidos.Should().HaveCount(1);
            saida.Pedidos[0].Caixas[0].Produtos.Should().HaveCount(2);
        }
    }
}