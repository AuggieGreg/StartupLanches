using System;
using System.Collections.Generic;
using System.Text;

namespace StartupLanches.Model
{
    public class LanchePedidoMdl
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<IngredienteLanchePedidoMdl> Ingredientes { get; set; }
        public decimal Valor { get; set; }
    }
}
