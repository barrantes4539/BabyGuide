using BabyGuide.Models;
using BabyGuide.Models.BD;
using BabyGuide.Models.Listas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

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
        public ActionResult NuevaAventura()
        {
            return View();
        }
        public ActionResult Preguntas()
        {

            return View();
        }


        //[HttpPost]
        //public async Task<ActionResult> Resp(string inputText)
        //{
        //    var api = new OpenAI_API.OpenAIAPI("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");
        //    var chat = api.Chat.CreateConversation();
        //    chat.AppendUserInput("How to make a hamburger?");
        //    string response = "";
        //    try
        //    {
        //        // Obtener la respuesta directamente del método asincrónico
        //        response = await chat.GetResponseFromChatbotAsync();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return Content(response);
        //}
        public ActionResult Expediente()
        {
            Expediente expediente = new Expediente();

            string nom = Request.Form["nom"]?.ToString();
            string ape1 = Request.Form["ape1"]?.ToString();
            string ape2 = Request.Form["ape2"]?.ToString();
            string gen = Request.Form["gen"]?.ToString();
            string nac = Request.Form["nac"]?.ToString();
            int gest = Convert.ToInt32(Request.Form["gest"]?.ToString());
            string alt = Request.Form["alt"]?.ToString();
            string pes = Request.Form["pes"]?.ToString();

            if (nom != null && ape1 != null && ape2 != null && nac != null && alt != null && pes != null)
            {
                expediente.Modificar(2, nom, ape1, ape2, nac, gen, gest, Convert.ToDouble(alt), Convert.ToDouble(pes), (List<Alergias>)Session["Alergias"]);
            }

            List<Alergias> alergias = expediente.VerAlergias();
            List<Vacunas> vacunas = expediente.VerVacunas();
            List<Medicamentos> medicamentos = expediente.VerMedicamentos();
            Session["Alergias"] = alergias;
            var viewModel = new ExpedienteModel
            {
                Alergias = alergias,
                AlergiasSelec = null,
                Vacunas = vacunas,
                Medicamentos = medicamentos,
            };
            

            DataTable dt = expediente.CargarExpediente();
            DataRow fila = dt.Rows[0];
            ViewBag.nom = fila["Nombre"];
            ViewBag.ape1 = fila["Apellido1"];
            ViewBag.ape2 = fila["Apellido2"];
            ViewBag.gen = fila["Genero"];
            ViewBag.nac = fila["FechaNacimiento"];
            ViewBag.gest = fila["FechaGestacion"];
            ViewBag.alt = fila["Altura"];
            ViewBag.peso = fila["Peso"];

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AgregarElemento(string id, string nombre)
        {
            // Aquí, simplemente creas un nuevo objeto ElementoModel con los valores recibidos
            var elemento = new Alergias
            {
                id = id,
                name = nombre
            };

            ((List<Alergias>)Session["Alergias"]).Add(elemento);

            return Json(elemento);
        }
        [HttpPost]
        public ActionResult EliminarElemento(string id)
        {
            // Eliminar el elemento de la lista seleccionada.
            // Suponiendo que 'ListaSeleccionados' es una lista en tu modelo.

            ((List<Alergias>)Session["Alergias"]).RemoveAll(alergia => alergia.id == id);


            // Devolver el resultado.
            return Json(new { success = true });
        }
        public ActionResult Citas()
        {
            return View();
        }
        public ActionResult Alertas()
        {
            return View();
        }
        public ActionResult Perfil()
        {
            string nom = Request.Form["nom"]?.ToString();
            string ape1 = Request.Form["ape1"]?.ToString();
            string ape2 = Request.Form["ape2"]?.ToString();
            string gen = Request.Form["gen"]?.ToString();
            string nac = Request.Form["nac"]?.ToString();
            string gest = Request.Form["gest"]?.ToString();

            if (nom != null && ape1 != null && ape2 != null && gen != null && nac != null && gest != null)
            {
                Perfil perfil = new Perfil();
                perfil.AgregarBebe(nom, ape1, ape2, Convert.ToInt32(gen), nac, Convert.ToInt32(gest));
            }

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


        [HttpPost]
        public ActionResult ObtenerPasosIngredienteYEtapa(string ingrediente, int etapa)
        {
            Dietas dietas = new Dietas();

            string pasos = dietas.ObtenerPasosPorIngredienteYEtapa(ingrediente, etapa);

            var data = new
            {
                etapa = "Etapa" + etapa,
                pasos = pasos
            };

            return Json(data);
        }


        [HttpGet]
        public JsonResult ObtenerListaIngredientes()
        {
            Dietas dietas = new Dietas();
            List<string> listaIngredientes = dietas.ObtenerListaDeIngredientes();

            // Devuelve la lista de ingredientes como JSON
            return Json(listaIngredientes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ObtenerSignificadoPorNombre(string nombre)
        {
            SigNombres_Consejos significadosController = new SigNombres_Consejos();
            string significado = significadosController.ObtenerSignificadoPorNombre(nombre);

            // Devuelve el resultado como una respuesta JSON
            return Json(new { significado });
        }

        [HttpGet]
        public JsonResult ListarConsejos()
        {
            // Crear una instancia de la clase SigNombres_Consejos
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();

            // Obtener la lista de consejos utilizando el método ListarConsejos
            List<Consejos> consejos = sigNombresConsejos.ListarConsejos();

            // Devolver la lista de consejos como resultado JSON
            return Json(consejos, JsonRequestBehavior.AllowGet);
        }



    }
}