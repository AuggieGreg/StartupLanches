using System;

namespace StartupLanches.Model
{
    public class IngredienteLanchePedidoMdl
    {
        public IngredienteLanchePedidoMdl(IngredienteMdl Ingrediente, int quantidade)
        {
            this.Ingrediente = Ingrediente;
            this.Nome = Ingrediente.Nome;
            this.Valor = Ingrediente.Valor;
            this.IdIngrediente = Ingrediente.Id;
            this.Quantidade = quantidade;
        }
        public int Id { get; set; }
        public int IdIngrediente { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public IngredienteMdl Ingrediente { get; set; }
    }
}