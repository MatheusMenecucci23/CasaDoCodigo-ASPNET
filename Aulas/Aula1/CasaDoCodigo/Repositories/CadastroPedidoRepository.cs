using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroPedidoRepository
    {
        Cadastro Update(int cadastroId, Cadastro novoCadastro);
    }
    public class CadastroPedidoRepository : BaseRepository<Cadastro>, ICadastroPedidoRepository
    {
        public CadastroPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Cadastro Update(int cadastroId, Cadastro novoCadastro)
        {
            //SingleOrDefault trazer um objeto ou nenhum(default)
            //Consultando o cadastro no banco de dados
            var cadastroDB = dbSet.Where(c => c.Id == cadastroId)
                .SingleOrDefault();

            if (cadastroDB == null)
            {
                throw new ArgumentNullException("cadastro");

            }
            //atualizando o banco de dados com o novo cadastro
            cadastroDB.Update(novoCadastro);

            //salvando as alterações
            contexto.SaveChanges();

            return cadastroDB;

        }
    }
}
