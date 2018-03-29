using System;

namespace StartupLanches.Model
{
    public class IngredienteLanchePedidoMdl
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public IngredienteMdl Ingrediente { get; set; }
    }
}