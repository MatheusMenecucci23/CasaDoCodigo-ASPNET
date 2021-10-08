using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroPedidoRepository
    {

    }
    public class CadastroPedidoRepository : BaseRepository<Cadastro>, ICadastroPedidoRepository
    {
        public CadastroPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

    }
}
