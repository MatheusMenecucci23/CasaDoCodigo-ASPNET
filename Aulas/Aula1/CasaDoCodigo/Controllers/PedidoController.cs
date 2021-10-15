
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models.ViewModels;

namespace CasaDoCodigo.Controllers
{
    //nessa classe fica os método que serão requisitados
    public class PedidoController : Controller
    {
        //Injeção de dependencias
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;


        public PedidoController(IProdutoRepository produtoRepository, 
            IPedidoRepository pedidoRepository, 
            IItemPedidoRepository itemPedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        public IActionResult Carrossel()
        {
            //produtoRepository.GetProdutos();
            return View(produtoRepository.GetProdutos());
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            List<ItemPedido> itens = pedidoRepository.GetPedido().Itens;
            CarrinhoViewModels carrinhoViewModel = new CarrinhoViewModels(itens);
            return base.View(carrinhoViewModel);
        }

        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            

            return View(pedido.Cadastro);
        }

        //Essa tag impede que alguém chame essa action diretamente pela url
        [HttpPost]
        //validando o token
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Cadastro cadastro)
        {
            //Verificando se os campos foram respondidos corretamente com o ModelState  - estado do nosso modelo - IsValid
            if (ModelState.IsValid)
            {
                return View(pedidoRepository.UpdateCadastro(cadastro));
            }
            //redirecionando o usuário para a página Cadastro
            return RedirectToAction("Cadastro");
        }

        //Pronto para receber uma requisição HttpPost
        [HttpPost]
        //[FromBody] vem do corpo da requisição
        [ValidateAntiForgeryToken]
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            return pedidoRepository.UpdateQuantidade(itemPedido);

        }

    }
}
