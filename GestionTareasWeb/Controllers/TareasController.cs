using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionTareasWeb.Models;
using GestionTareasWeb.Utilidades;
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
                ctx.Credentials = GestionCuentas.GetCredentials();
                var lista = ctx.Web.Lists.GetByTitle("MisTareas");
                ctx.Load(lista);
                var query = new CamlQuery();

                var items = lista.GetItems(query);
                ctx.Load(items);

                ctx.ExecuteQuery();

                foreach (ListItem item in items)
                {
                    var t = new MiTarea()
                    {
                        ID = item.Id,
                        Descripcion = (string) item["Descripcion"],
                        Tarea = (string) item["Tarea"],
                        //Vencimiento = (DateTime) item["Vencimiento"],
                        Finalizada = (bool) item["Finalizada"]
                    };
                    tareas.Add(t);
                }
            }


            return View(tareas);
        }

        public ActionResult Alta()
        {
            return View(new MiTarea());
        }

        [HttpPost]
        public ActionResult Alta(MiTarea tarea)
        {
            var t = Request.Form["Tarea"];
            var d = Request.Form["Descripcion"];
            var f = false;
            try
            {
                f = Boolean.Parse(Request.Form["Finalizada"]);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            var url = Session["SPAppUrl"].ToString();
            
            //if (!ModelState.IsValid)
            //{
            //    return View(tarea);
            //}
            //var url = Session["SPAppUrl"].ToString();

            using (ClientContext ctx = new ClientContext(url))
            {
                ctx.Credentials = GestionCuentas.GetCredentials();
                var lista = ctx.Web.Lists.GetByTitle("MisTareas");
                ctx.Load(lista);
                var itemC = new ListItemCreationInformation();
                var item = lista.AddItem(itemC);
                item["Tarea"] = t;
                item["Descripcion"] = d;
                //item["Vencimiento"] = tarea.Vencimiento;
                item["Finalizada"] = f;

                item.Update();
                ctx.ExecuteQuery();
            }
            return RedirectToAction("Index");

        }
    }

}