using StartupLanches.BLL.Exception;
using StartupLanches.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupLanches.BLL
{
    public class PedidoBLL : BaseBLL
    {
        public PedidoBLL() : base()
        {

        }

        public int SalvarPedido(PedidoMdl Pedido)
        {
            if (string.IsNullOrEmpty(Pedido.NomeCliente))
            {
                throw new BusinessException("O nome do cliente é obrigatório!");
            }

            if (Pedido.Lanches == null || !Pedido.Lanches.Any())
            {
                throw new BusinessException("É obrigatório que um pedido tenha ao menos um lanche.");
            }

            var lancheBLL = new LancheBLL();
            foreach (var lanche in Pedido.Lanches)
            {
                lancheBLL.CalcularValor(lanche);
                lancheBLL.CalcularPromocao(lanche);
            }

            Pedido.Numero = dataBase.DbPedido.Count + 1;
            Pedido.ValorTotal = Pedido.Lanches.Sum(s => s.ValorFinal);
            dataBase.DbPedido.Add(Pedido);
            return Pedido.Numero;
        }

        public List<PedidoMdl> ListarTodos()
        {
            return dataBase.DbPedido.ToList();
        }
    }
}
