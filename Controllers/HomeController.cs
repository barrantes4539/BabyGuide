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
using System.Xml.Linq;

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

            int idexp = expediente.idExpediente(2);

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
                expediente.Modificar(2, idexp, nom, ape1, ape2, nac, gen, gest, Convert.ToDouble(alt), Convert.ToDouble(pes),
                    (List<AlergiasBebe>)Session["AlergiasAgregar"], (List<Alergias>)Session["AlergiasEliminar"],
                    (List<VacunasBebe>)Session["VacunasAgregar"], (List<Vacunas>)Session["VacunasEliminar"],
                    (List<Diagnosticos>)Session["DiagnosticosAgregar"], (List<Diagnosticos>)Session["DiagnosticosEliminar"]);
                Session["AlergiasAgregar"] = null;
                Session["AlergiasEliminar"] = null;
                Session["VacunasAgregar"] = null;
                Session["VacunasEliminar"] = null;
                Session["DiagnosticosAgregar"] = null;
                Session["DiagnosticosEliminar"] = null;
            }

            var viewModel = new ExpedienteModel
            {
                Alergias = expediente.VerAlergias(),
                AlergiasBebe = expediente.VerAlergiasBebe(idexp),
                Vacunas = expediente.VerVacunas(),
                VacunasBebe = expediente.VerVacunasBebe(idexp),
                Diagnosticos = expediente.VerDiagnosticosBebe(idexp),
                Medicamentos = expediente.VerMedicamentos(),
                MedicamentosBebe = expediente.VerMedicamentosBebe(idexp),
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
        #region Expediente
        [HttpPost]
        public ActionResult AgregarAlergia(string id, string nombre, string fecha)
        {
            // Aquí, simplemente creas un nuevo objeto ElementoModel con los valores recibidos
            var elemento = new AlergiasBebe
            {
                id = id,
                name = nombre,
                fecha = fecha
            };

            if (Session["AlergiasAgregar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<AlergiasBebe> alergiasLista = new List<AlergiasBebe>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["AlergiasAgregar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<AlergiasBebe>)Session["AlergiasAgregar"]).Add(elemento);
            }

            return Json(elemento);
        }
        [HttpPost]
        public ActionResult EliminarAlergia(string id, string nombre)
        {
            var elemento = new Alergias
            {
                id = id,
                name = nombre
            };

            if (Session["AlergiasEliminar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<Alergias> alergiasLista = new List<Alergias>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["AlergiasEliminar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<Alergias>)Session["AlergiasEliminar"]).Add(elemento);
            }


            // Devolver el resultado.
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult AgregarVacuna(string id, string nombre, string fecha)
        {
            // Aquí, simplemente creas un nuevo objeto ElementoModel con los valores recibidos
            var elemento = new VacunasBebe
            {
                id = id,
                name = nombre,
                fecha = fecha
            };

            if (Session["VacunasAgregar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<VacunasBebe> alergiasLista = new List<VacunasBebe>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["VacunasAgregar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<VacunasBebe>)Session["VacunasAgregar"]).Add(elemento);
            }

            return Json(elemento);
        }
        [HttpPost]
        public ActionResult EliminarVacuna(string id, string nombre)
        {
            var elemento = new Vacunas
            {
                id = id,
                name = nombre
            };

            if (Session["VacunasEliminar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<Vacunas> alergiasLista = new List<Vacunas>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["VacunasEliminar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<Vacunas>)Session["VacunasEliminar"]).Add(elemento);
            }


            // Devolver el resultado.
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult AgregarDiagnostico(string nombre, string fecha)
        {
            // Aquí, simplemente creas un nuevo objeto ElementoModel con los valores recibidos
            var elemento = new Diagnosticos
            {
                name = nombre,
                fecha = fecha
            };

            if (Session["DiagnosticosAgregar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<Diagnosticos> alergiasLista = new List<Diagnosticos>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["DiagnosticosAgregar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<Diagnosticos>)Session["DiagnosticosAgregar"]).Add(elemento);
            }

            return Json(elemento);
        }
        [HttpPost]
        public ActionResult EliminarDiagnostico(string nombre)
        {
            var elemento = new Diagnosticos
            {
                fecha = "",
                name = nombre
            };

            if (Session["DiagnosticosEliminar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<Diagnosticos> alergiasLista = new List<Diagnosticos>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["DiagnosticosEliminar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<Diagnosticos>)Session["DiagnosticosEliminar"]).Add(elemento);
            }


            // Devolver el resultado.
            return Json(new { success = true });
        }
        #endregion
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