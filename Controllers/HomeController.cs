using BabyGuide.Models;
using BabyGuide.Models.BD;
using System.Web.Mvc;

namespace BabyGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            expediente expediente = new expediente();
            expediente.VerAlergias();
            
            return View();
        }

        public ActionResult Dietas()
        {
            return View();
        }
        public ActionResult BabyGaleria()
        {
            return View();
        }
        public ActionResult Preguntas()
        {
            return View();
        }
        public ActionResult Expediente()
        {
            return View();
        }
        public ActionResult Citas()
        {
            return View();
        }
        public ActionResult Alertas()
        {
            return View();
        }
        public ActionResult NuevaAventura()
        {
            return View();
        }
        public ActionResult Perfil()
        {
            return View();
        }
        public ActionResult PerfilModificar()
        {
            return View();
        }
        public ActionResult GestionFamilia()
        {
            return View();
        }
    }
}