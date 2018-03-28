using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StartupLanches.Areas.Pedidos.Controllers
{
    [Area("Pedidos")]
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            var bllLanche = new StartupLanches.BLL.LancheBLL();
            return View(bllLanche.ListarLanches());
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pedido/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}