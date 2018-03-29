using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartupLanches.BLL;
using StartupLanches.Model;

namespace StartupLanches.Areas.Pedidos.Controllers
{
    [Area("Pedidos")]
    public class PedidoController : Controller
    {
        public ActionResult Index()
        {
            var bllLanche = new StartupLanches.BLL.LancheBLL();
            return View(bllLanche.ListarLanches());
        }

        public PartialViewResult TableMontagemLanche(int? idLanche)
        {
            LanchePedidoMdl lanchePedido;
            if (idLanche.HasValue)
            {
                var bllLanche = new LancheBLL();
                var lanche = bllLanche.Obter(idLanche.Value);
                lanchePedido = new LanchePedidoMdl(lanche);
            }
            else
            {
                lanchePedido = new LanchePedidoMdl();
                lanchePedido.Ingredientes = new List<IngredienteLanchePedidoMdl>();
            }
            return PartialView("pvTableMontagemLanche", lanchePedido);
        }

        public PartialViewResult TrMontagemLanche(int idIngrediente)
        {
            var bllIngrediente = new IngredienteBLL();
            var ingrediente = bllIngrediente.Obter(idIngrediente);
            var ingredientePedido = new IngredienteLanchePedidoMdl(ingrediente, 1);
            return PartialView("pvTrMontagemLanche", ingredientePedido);
        }
    }
}