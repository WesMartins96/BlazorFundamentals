using EcommerceStart.Server.Repositories;
using EcommerceStart.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EcommerceStart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly ProdutoRepository _produtoRepository;

        public ProdutosController(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("listar")]
        public IActionResult Listar(string? nome)
        {
            var produtos = _produtoRepository.GetProdutos();
            return Ok(produtos);
        }

        [HttpPost("incluir")]
        public ActionResult Incluir(Produto produto)
        {
            if (produto == null)
                return BadRequest("Produto não foi enviado por parâmetro");

            Produto? produtoAnterior = Banco.Produtos.OrderByDescending(e => e.Id).FirstOrDefault();
            if (produtoAnterior != null)
            {
                produto.Id = produtoAnterior.Id + 1;
            }
            else
            {
                produto.Id = 1;
            }

            Banco.Produtos.Add(produto);
            return Ok();
        }

        [HttpPost("incluir-ou-alterar")]
        public ActionResult IncluirOuAlterar(Produto produto)
        {
            if (produto == null)
                return BadRequest("Produto não foi enviado por parâmetro");

            Produto? produtoExistente = Banco.Produtos.Where(p => p.Id.Equals(produto.Id)).FirstOrDefault();
            if (produtoExistente is null) 
            {
                //aqui o produto é novo
                Produto? produtoAnterior = Banco.Produtos.OrderByDescending(e => e.Id).FirstOrDefault();
                if (produtoAnterior != null)
                {
                    produto.Id = produtoAnterior.Id + 1;
                }
                else
                {
                    produto.Id = 1;
                }
                Banco.Produtos.Add(produto);
            }
            else
            {
                //aqui o produto já existe
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Preco = produto.Preco;
                produtoExistente.Quantidade = produto.Quantidade;
                produtoExistente.Imagem = produto.Imagem;
            }
                      
            return Ok();
        }


        

        [HttpGet("consultar/{id:int}")]
        public IActionResult Consultar(int id)
        {
            Produto? p = Banco.Produtos.Where(e => e.Id == id).FirstOrDefault();
            if (p == null)
                return BadRequest("Produto não existe mais na base de dados, deve ter sido removido");

            return Ok(p);
        }

        [HttpPut("alterar")]
        public IActionResult Alterar(Produto produto)
        {
            if (produto == null)
                return BadRequest("Produto não foi enviado por parâmetro");

            Produto? p = Banco.Produtos.Where(e => e.Id == produto.Id).FirstOrDefault();
            if (p == null)
                return BadRequest("Produto não existe mais na base de dados, deve ter sido removido");

            p.Nome = produto.Nome;
            p.Preco = produto.Preco;
            p.Quantidade = produto.Quantidade;
            p.Imagem = produto.Imagem;

            return Ok();
        }

        [HttpDelete("excluir/{id:int}")]
        public IActionResult Excluir(int id)
        {
            Produto? p = Banco.Produtos.Where(e => e.Id == id).FirstOrDefault();
            if (p == null)
                return BadRequest("Produto não existe mais na base de dados");

            Banco.Produtos.Remove(p);
            return Ok();
        }
    }
}
