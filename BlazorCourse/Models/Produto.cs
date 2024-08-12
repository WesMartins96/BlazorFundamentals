using System.ComponentModel.DataAnnotations;

namespace BlazorCourse.Models
{
    public class Produto
    {
        [Required(ErrorMessage = "O Nome é Obrigatório!")]
        [StringLength(20, ErrorMessage = "Nome deve conter menos que 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Idade é Obrigatório!")]
        [Range(1, 80, ErrorMessage = "A Idade deve ser entre 1 a 80 anos")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O Preço é Obrigatório!")]
        [Range(0.1, 99999999, ErrorMessage = "Informe um preço adequado!")]
        public decimal Preco { get; set; }

        public decimal ValorEstoqueTotal()
        {
            return Preco * Quantidade;
        }
    }
}
