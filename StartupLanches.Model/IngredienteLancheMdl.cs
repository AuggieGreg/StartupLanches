using System;

namespace StartupLanches.Model
{
    public class IngredienteLancheMdl
    {
        public IngredienteLancheMdl(IngredienteMdl Ingrediente)
        {
            this.Ingrediente = Ingrediente;
            this.Nome = Ingrediente.Nome;
            this.Valor = Ingrediente.Valor;
            this.IdIngrediente = Ingrediente.Id;
        }
        public int IdIngrediente { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public IngredienteMdl Ingrediente { get; set; }
    }
}