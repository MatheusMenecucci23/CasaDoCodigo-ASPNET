using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class CarrinhoViewModels
    {
        public CarrinhoViewModels(IList<ItemPedido> itens)
        {
            this.Itens = itens;
            
        }

        public IList<ItemPedido> Itens { get;}
       
        public decimal Total => Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
        public decimal TotalItens => Itens.Sum(i => i.Quantidade);

    }
}
