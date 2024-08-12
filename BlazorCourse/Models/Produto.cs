namespace BlazorCourse.Models
{
    public class Produto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public decimal ValorEstoqueTotal()
        {
            return Preco * Quantidade;
        }
    }
}
