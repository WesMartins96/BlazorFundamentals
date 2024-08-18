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
            IEnumerable<Produto> produtos;

            if (string.IsNullOrEmpty(nome))
            {
                produtos = _produtoRepository.GetProdutos();
            }
            else
            {
                produtos = _produtoRepository.GetProdutosByName(nome);
            }

            return Ok(produtos);
        }

        [HttpPost("incluir")]
        public ActionResult Incluir(Produto produto)
        {
            _produtoRepository.AddProduto(produto);
            return StatusCode(StatusCodes.Status201Created);
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
            var produto = _produtoRepository.GetProdutoById(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPut("alterar/{id}")]
        public IActionResult Alterar(int id, [FromBody]Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest("ID do produto não corresponde ao ID na solicitação.");
            }

            _produtoRepository.UpdateProduto(produto);
            return NoContent();
        }

        [HttpDelete("excluir/{id:int}")]
        public IActionResult Excluir(int id)
        {
            _produtoRepository.DeleteProduto(id);
            return NoContent();
        }
    }
}
