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
            var bllIngrediente = new IngredienteBLL();
            ViewBag.Ingredientes = bllIngrediente.SelecionarTodos();
            var bllLanche = new StartupLanches.BLL.LancheBLL();

            var model = bllLanche.ListarLanches().Select(s => new LanchePedidoMdl(s)).ToList();
            foreach (var lanche in model)
            {
                bllLanche.CalcularValor(lanche);
                bllLanche.CalcularPromocao(lanche);
            }
            return View(model);
        }

        public ActionResult Pedidos()
        {
            var pedidoBLL = new PedidoBLL();
            return View(pedidoBLL.ListarTodos());
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
            return PartialView("pvTbodyTrMontagemLanche", lanchePedido);
        }

        public PartialViewResult CalcularPromocoes(LanchePedidoMdl lanchePedido)
        {
            var lancheBLL = new LancheBLL();
            lancheBLL.CalcularValor(lanchePedido);
            lancheBLL.CalcularPromocao(lanchePedido);
            return PartialView("pvTbodyTrMontagemLanche", lanchePedido);
        }

        public PartialViewResult TrMontagemLanche(int idIngrediente, int quantidade)
        {
            var bllIngrediente = new IngredienteBLL();
            var ingrediente = bllIngrediente.Obter(idIngrediente);
            var ingredientePedido = new IngredienteLanchePedidoMdl(ingrediente, quantidade);
            return PartialView("pvTrMontagemLanche", ingredientePedido);
        }

        public PartialViewResult AdicionarLanchePedido(LanchePedidoMdl lanchePedido)
        {
            var lancheBLL = new LancheBLL();
            lancheBLL.CalcularValor(lanchePedido);
            lancheBLL.CalcularPromocao(lanchePedido);
            return PartialView("pvTrLanchePedido", lanchePedido);
        }

        [HttpPost]
        public JsonResult ConfirmarPedido(PedidoMdl pedido)
        {
            var pedidoBLL = new PedidoBLL();
            return Json(new { sucesso = true, numeroPedido = pedidoBLL.SalvarPedido(pedido) });
        }
    }
}