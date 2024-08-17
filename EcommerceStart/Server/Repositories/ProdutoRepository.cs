using EcommerceStart.Shared.Models;
using Npgsql;
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

        public void AddProduto(Produto produto) 
        {
            var query = @"INSERT INTO produtos (nome, preco, quantidade, imagem)
                            VALUES (@Nome, @Preco, @Quantidade, @Imagem)";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new NpgsqlParameter("@Nome", produto.Nome));
                command.Parameters.Add(new NpgsqlParameter("@Preco", produto.Preco));
                command.Parameters.Add(new NpgsqlParameter("@Quantidade", produto.Quantidade));
                command.Parameters.Add(new NpgsqlParameter("@Imagem", produto.Imagem));

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }       
        }
    }
}
