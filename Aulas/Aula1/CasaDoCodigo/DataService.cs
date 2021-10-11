using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;


        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void IncializaDB()
        {
            //pegando a tabela e tendo certeza que ela foi criada
            contexto.Database.EnsureCreated();


            List<Livro> livros = GetLivros();

            produtoRepository.SaveProdutos(livros);
        }



        private static List<Livro> GetLivros()
        {
            //pegando o conteúdo do arquivo livros.json
            string json = File.ReadAllText("livros.json");

            //Convertendo o conteúdo e desserializando o objeto
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livros;
        }

    }
 
}
