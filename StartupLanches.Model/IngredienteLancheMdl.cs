using System;

namespace StartupLanches.Model
{
    public class IngredienteLancheMdl
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public IngredienteMdl Ingrediente { get; set; }
    }
}