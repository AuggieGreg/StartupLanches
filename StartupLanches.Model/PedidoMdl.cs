using System;
using System.Collections.Generic;
using System.Text;

namespace StartupLanches.Model
{
    public class PedidoMdl
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public List<LanchePedidoMdl> Lanches { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
