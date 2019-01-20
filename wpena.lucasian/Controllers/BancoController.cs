using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wpena.lucasian.Services;

namespace wpena.lucasian.Controllers
{
    public class BancoController : Controller
    {
        // GET: Banco
        public ActionResult Index()
        {
            return View(BancoRepositorio.Bancos.Select(x => x.banco));
        }

        // GET: Banco/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = BancoRepositorio.Bancos.Find(x => x.banco != null && x.banco.id == id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}
