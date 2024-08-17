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

        public Produto GetProdutoById(int id) 
        { 
            Produto produto = null;
            var query = @"SELECT * FROM produtos 
                            WHERE id=@Id";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new NpgsqlParameter("@Id", id));

                _connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        produto = new Produto
                        {
                            Id = (int)reader["id"],
                            Nome = reader["nome"].ToString(),
                            Preco = (decimal)reader["preco"],
                            Quantidade = (int)reader["quantidade"],
                            Imagem = reader["imagem"].ToString()
                        };
                    }
                }
                _connection.Close();
            }
            return produto;
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

        public void UpdateProduto(Produto produto) 
        {
            var query = @"UPDATE produtos 
                            SET nome=@Nome, preco=@Preco, quantidade=@Quantidade, imagem=@Imagem
                            WHERE id=@Id";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new NpgsqlParameter("@Nome", produto.Nome));
                command.Parameters.Add(new NpgsqlParameter("@Preco", produto.Preco));
                command.Parameters.Add(new NpgsqlParameter("@Quantidade", produto.Quantidade));
                command.Parameters.Add(new NpgsqlParameter("@Imagem", produto.Imagem));
                command.Parameters.Add(new NpgsqlParameter("@Id", produto.Id));

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeleteProduto(int id)
        {
            var query = @"DELETE FROM produtos 
                            WHERE id=@Id";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new NpgsqlParameter("@Id", id));

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
