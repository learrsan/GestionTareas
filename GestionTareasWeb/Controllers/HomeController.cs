using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionTareasWeb.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if (Session["SPAppUrl"] == null && Session["SPHostUrl"] == null)
            {
                
                 if (Request.QueryString["SPAppWebUrl"] != null)
                 {
                     Session["SPAppUrl"] = Request.QueryString["SPAppWebUrl"];
                 }
                 if (Request.QueryString["SPHostUrl"] != null)
                 {
                     Session["SPHostUrl"] = Request.QueryString["SPHostUrl"];
                 }
           
            }
            
            return RedirectToAction("Index", "Tareas");

        }

        
    }
}
