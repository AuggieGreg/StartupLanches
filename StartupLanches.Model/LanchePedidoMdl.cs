using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StartupLanches.Model
{
    public class LanchePedidoMdl
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<IngredienteLanchePedidoMdl> Ingredientes { get; set; }
        public decimal Valor { get; set; }

        public List<PromocaoLancheMdl> Promocoes { get; set; }

        public decimal ValorIngredientes
        {
            get
            {
                return Ingredientes != null ? Ingredientes.Sum(s => s.Quantidade * s.Valor) : 0;
            }
        }
        public decimal ValorFinal
        {
            get
            {
                return ValorIngredientes + (Promocoes != null ? Promocoes.Sum(s => s.Valor) : 0);
            }
        }
    }
}
