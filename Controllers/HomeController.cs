using BabyGuide.Models;
using BabyGuide.Models.BD;
using BabyGuide.Models.Listas;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace BabyGuide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
            Expediente expediente = new Expediente();

            var viewModel = new ExpedienteModel
            {
                Alergias = expediente.VerAlergias(),
                Vacunas = expediente.VerVacunas(),
                Medicamentos = expediente.VerMedicamentos(),
            };

            return View(viewModel);
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