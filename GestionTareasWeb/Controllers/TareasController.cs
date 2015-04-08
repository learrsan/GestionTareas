using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionTareasWeb.Models;
using Microsoft.SharePoint.Client;

namespace GestionTareasWeb.Controllers
{
    public class TareasController : Controller
    {
        // GET: Tareas
        public ActionResult Index()
        {
            List<MiTarea> tareas=new List<MiTarea>();
            var url = Session["SPAppUrl"].ToString();

            using (ClientContext ctx = new ClientContext(url))
            {
                
            }


            return View(tareas);
        }
    }
}