using StartupLanches.Model;
using System;

namespace StartupLanches.BLL
{
    public class PedidoBLL : BaseBLL
    {
        public PedidoBLL() : base()
        {

        }

        public void SalvarPedido(PedidoMdl Pedido)
        {
            dataBase.DbPedido.Add(Pedido);
        }
    }
}
