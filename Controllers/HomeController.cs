using BabyGuide.Models;
using BabyGuide.Models.BD;
using BabyGuide.Models.Listas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            var viewModel = new ExpedienteModel
            {
                Alergias = expediente.VerAlergias(),
                AlergiasSelec = null,
                Vacunas = expediente.VerVacunas(),
                Medicamentos = expediente.VerMedicamentos(),
            };

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

            // Agregar el elemento a la lista de elementos seleccionados.
            // Suponiendo que 'ListaSeleccionados' es una lista en tu modelo.
            

            // Devolver el elemento agregado como una respuesta JSON.
            return Json(elemento);
        }
        [HttpPost]
        public ActionResult EliminarElemento(int id)
        {
            // Eliminar el elemento de la lista seleccionada.
            // Suponiendo que 'ListaSeleccionados' es una lista en tu modelo.
            

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