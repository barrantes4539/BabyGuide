using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyGuide.Controllers
{
    public class ConfigController : Controller
    {

        // Los estilos de estas vistas van en la carpeta de ../assets/css/ModuloConfiguracion

        //Frontend: Dashboard: Kevin
        public ActionResult ModuloConfiguracion()
        {
            return View();
        }

        //Frontend: Panel de visualizacion de usuarios: Jasson // Formularios editar y agregar usuarios: Kevin
        public ActionResult Usuarios()
        {
            return View();
        }

        //Frontend: Historial: Jasson
        public ActionResult Historial()
        {
            return View();
        }
        //Frontend: Configuracion: Jasson
        public ActionResult Configuracion()
        {
            return View();
        }

        //Frontend: CrearDietas: Kevin
        public ActionResult CrearDietas()
        {
            return View();
        }
    }
}