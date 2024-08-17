using EcommerceStart.Shared.Models;
using System.Data;

namespace EcommerceStart.Server.Repositories
{
    public class ProdutoRepository
    {
        private readonly IDbConnection _connection;

        public ProdutoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Produto> GetProdutos() 
        { 
            var produtos = new List<Produto>();
            var query = "SELECT * FROM produtos;";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;
                _connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new Produto
                        {
                            Id = (int)reader["id"],
                            Nome = reader["nome"].ToString(),
                            Preco = (decimal)reader["preco"],
                            Quantidade = (int)reader["quantidade"],
                            Imagem = reader["imagem"].ToString()
                        });
                    }
                }
                _connection.Close();
            }
            return produtos;
        }
    }
}
