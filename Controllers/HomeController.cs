using BabyGuide.Models;
using BabyGuide.Models.BD;
using BabyGuide.Models.Listas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;

using System.Net.Mail;
using System.Net;

using System.IO;

using RestSharp;


using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web;

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

        public ActionResult Preguntas()
        {

            string preg = Request.Form["txtpregunta"]?.ToString();

            Preguntas preguntas = new Preguntas();
            if (preg != null)
            {
                DataTable dt = preguntas.BuscarRespuesta(preg);
                if (dt.Rows.Count > 0)
                {
                    DataRow fila = dt.Rows[0];
                    ViewBag.res = "Pregunta más coincidente: " + fila["Respuesta"];
                    ViewBag.preg = fila["Pregunta"];
                }
                else if (dt.Rows.Count <= 0)
                {
                    string _EndPoint = "https://api.openai.com/";
                    string _URI = "v1/chat/completions";
                    string _APIKey = "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";

                    var pSolicitud = preg;

                    var strRespuesta = "";

                    // Consumir la API
                    var oCliente = new RestClient(_EndPoint);
                    var oSolicitud = new RestRequest(_URI, Method.Post);
                    oSolicitud.AddHeader("Content-Type", "application/json");
                    oSolicitud.AddHeader("Authorization", "Bearer " + _APIKey);

                    // Creamos el cuerpo de la solicitud
                    var oCuerpo = new Request()
                    {
                        model = "gpt-3.5-turbo",
                        messages = new List<Message>()
                        {
                        new Message() {
                            role="user",
                            content=pSolicitud
                        }
                        }
                    };

                    var jsonString = JsonConvert.SerializeObject(oCuerpo);

                    oSolicitud.AddJsonBody(jsonString);

                    var oRespuesta = oCliente.Post<Response>(oSolicitud);

                    strRespuesta = oRespuesta.choices[0].message.content;

                    ViewBag.res = "Respondido de ChatGPT: " + strRespuesta;
                }
            }

            List<Preguntas> preguntasl = preguntas.VerPreguntas();

            return View(preguntasl);
        }

        public ActionResult EtapasDesarrollo()
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
            if (Session["idBebe"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Expediente expediente = new Expediente();

            int idexp = expediente.idExpediente(Convert.ToInt32(Session["idBebe"]));

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
                expediente.Modificar(Convert.ToInt32(Session["idBebe"]), idexp, nom, ape1, ape2, nac, gen, gest, Convert.ToDouble(alt), Convert.ToDouble(pes),
                    (List<AlergiasBebe>)Session["AlergiasAgregar"], (List<Alergias>)Session["AlergiasEliminar"],
                    (List<VacunasBebe>)Session["VacunasAgregar"], (List<Vacunas>)Session["VacunasEliminar"],
                    (List<Diagnosticos>)Session["DiagnosticosAgregar"], (List<Diagnosticos>)Session["DiagnosticosEliminar"],
                    (List<Medicamentos>)Session["MedicacionAgregar"], (List<Medicamentos>)Session["MedicacionEliminar"]);
                Session["AlergiasAgregar"] = null;
                Session["AlergiasEliminar"] = null;
                Session["VacunasAgregar"] = null;
                Session["VacunasEliminar"] = null;
                Session["DiagnosticosAgregar"] = null;
                Session["DiagnosticosEliminar"] = null;
                Session["MedicacionAgregar"] = null;
                Session["MedicacionEliminar"] = null;
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


            DataTable dt = expediente.CargarExpediente(Convert.ToInt32(Session["idBebe"]));
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
        [HttpPost]
        public ActionResult AgregarMedicacion(string id, string nombre)
        {
            // Aquí, simplemente creas un nuevo objeto ElementoModel con los valores recibidos
            var elemento = new Medicamentos
            {
                id = id,
                name = nombre,
            };

            if (Session["MedicacionAgregar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<Medicamentos> alergiasLista = new List<Medicamentos>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["MedicacionAgregar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<Medicamentos>)Session["MedicacionAgregar"]).Add(elemento);
            }

            return Json(elemento);
        }
        [HttpPost]
        public ActionResult EliminarMedicacion(string id, string nombre)
        {
            var elemento = new Medicamentos
            {
                id = id,
                name = nombre
            };

            if (Session["MedicacionEliminar"] == null)
            {
                // Si la sesión no tiene una lista de alergias, creamos una nueva lista y la asignamos a la sesión
                List<Medicamentos> alergiasLista = new List<Medicamentos>();
                alergiasLista.Add(elemento); // Agregamos el elemento a la lista
                Session["MedicacionEliminar"] = alergiasLista;
            }
            else
            {
                // Si la sesión ya tiene una lista de alergias, simplemente agregamos el elemento a la lista existente
                ((List<Medicamentos>)Session["MedicacionEliminar"]).Add(elemento);
            }


            // Devolver el resultado.
            return Json(new { success = true });
        }
        #endregion
        public ActionResult Citas()
        {
            if (Session["idBebe"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Citas citas = new Citas();

            int idexp = citas.idExpediente(Convert.ToInt32(Session["idBebe"]));

            string titu = Request.Form["titu"]?.ToString();
            string desc = Request.Form["desc"]?.ToString();
            string fecha = Request.Form["date"]?.ToString();
            string hora = Request.Form["time"]?.ToString();


            if (titu != null && desc != null && fecha != null && hora != null)
            {
                citas.CrearCita(idexp, titu, desc, fecha + " " + hora);
            }

            List<CitasM> lcitas = citas.VerCitasBebe(idexp);

            return View(lcitas);
        }
        [HttpPost]
        public ActionResult EliminarCita(string id)
        {

            Citas citas = new Citas();

            int idexp = citas.idExpediente(Convert.ToInt32(Session["idBebe"]));

            citas.EliminarCita(idexp, Convert.ToInt32(id));

            return Json(new { success = true });
        }

        public ActionResult Perfil()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Perfil perfil = new Perfil();

            string id = Request.Form["id"]?.ToString();
            string nom = Request.Form["nom"]?.ToString();
            string ape1 = Request.Form["ape1"]?.ToString();
            string ape2 = Request.Form["ape2"]?.ToString();
            string gen = Request.Form["gen"]?.ToString();
            string nac = Request.Form["nac"]?.ToString();
            string gest = Request.Form["gest"]?.ToString();

            if (nom != null && ape1 != null && ape2 != null && gen != null && nac != null && gest != null && id != null)
            {

                perfil.AgregarBebe(Convert.ToInt32(id), Convert.ToInt32(Session["idUsuario"]), nom, ape1, ape2, gen, nac, Convert.ToInt32(gest), perfil.GenerarClave());
            }

            DataTable dataTable = perfil.CargarPerfil(Convert.ToInt32(Session["idUsuario"]));
            if (dataTable.Rows.Count > 0)
            {
                DataRow fila = dataTable.Rows[0];
                ViewBag.nom = fila["Nombre"];
                ViewBag.ape = fila["Apellidos"];
                ViewBag.correo = Convert.ToString(Session["correoUsuario"]);
                ViewBag.nombebe = fila["NombreB"];
                ViewBag.apebebe = fila["ApellidosB"];
                ViewBag.rol = fila["Rol"];
                ViewBag.clave = fila["Clave"];
                if (Convert.ToString(fila["idBebe"]) != "")
                {
                    Session["idBebe"] = fila["idBebe"];
                    Session["idRol"] = fila["idRoll"];
                }
            }


            List<BebesP> List = new List<BebesP>();

            foreach (DataRow row in dataTable.Rows)
            {
                BebesP Data = new BebesP
                {
                    name = row["NombreB"].ToString(),
                    id = row["idBebe"].ToString(),
                    apellidos = row["ApellidosB"].ToString(),
                    rol = row["idRoll"].ToString(),
                    clave = row["Clave"].ToString(),
                    roln = row["Rol"].ToString(),
                    // ...
                };
                if (Data.id != "")
                {
                    List.Add(Data);
                }
            }

            return View(List);
        }
        public ActionResult PerfilModificar()
        {
            if (Session["idUsuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            PerfilModificar perfil = new PerfilModificar();
            string nom = Request.Form["nom"]?.ToString();
            string ape1 = Request.Form["ape1"]?.ToString();
            string ape2 = Request.Form["ape2"]?.ToString();
            string email = Request.Form["email"]?.ToString();

            if (nom != null && ape1 != null && ape2 != null && email != null)
            {
                perfil.ModificarPerfil(Convert.ToInt32(Session["idUsuario"]), nom, ape1, ape2, email);
            }

            DataTable dt = perfil.CargarPerfil(Convert.ToInt32(Session["idUsuario"]));

            foreach (DataRow row in dt.Rows)
            {
                ViewBag.nom = row["Nombre"].ToString();
                ViewBag.ape1 = row["Apellido1"].ToString();
                ViewBag.ape2 = row["Apellido2"].ToString();
            }

            ViewBag.email = Convert.ToString(Session["correoUsuario"]);


            return View();
        }
        public ActionResult GestionFamilia()
        {
            if (Session["idBebe"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            GestionarFamilia gestionarFamilia = new GestionarFamilia();

            string fam = Request.Form["fam"]?.ToString();
            string rol = Request.Form["rol"]?.ToString();

            if (fam != null && rol != null)
            {
                gestionarFamilia.ModificarRol(Convert.ToInt32(fam), Convert.ToInt32(rol), Convert.ToInt32(Session["idBebe"]));
            }

            var viewModel = new FamiliaModel
            {
                familia = gestionarFamilia.VerFamilia(Convert.ToInt32(Session["idBebe"])),
                roles = gestionarFamilia.VerRoles(),
            };

            return View(viewModel);
        }
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
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
        public JsonResult ObtenerDescripcionConsejo(string nombre)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            string descripcion = sigNombresConsejos.ObtenerSignificadoPorNombreC(nombre); // Implementa este método
            return Json(new { Descripcion = descripcion }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarNombres()
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            List<string> nombres = sigNombresConsejos.ListarNombres();
            return Json(nombres, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarAlergias()
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            List<string> alergias = sigNombresConsejos.ListarAlergias();
            return Json(alergias, JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public JsonResult ObtenerSignificadoPorNombreC(string nombre)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            string significado = sigNombresConsejos.ObtenerDescripcionPorTitulo(nombre); // Implementa este método
            return Json(new { Significado = significado }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ActualizarDescripcionConsejo(string titulo, string descripcion)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            sigNombresConsejos.ActualizarDescripcion(titulo, descripcion); // Implementa este método
            return Json(new { mensaje = "Descripción actualizada con éxito." });
        }

        [HttpPost]
        public ActionResult AgregarConsejo(string titulo, string descripcion)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            sigNombresConsejos.AgregarConsejo(titulo, descripcion); // Implementa este método
            return Json(new { mensaje = "Consejo agregado exitosamente" });
        }

        [HttpPost]
        public ActionResult EliminarConsejo(string titulo)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            sigNombresConsejos.EliminarConsejo(titulo); // Implementa este método
            return Json(new { mensaje = "Consejo eliminado con éxito" });
        }

        [HttpPost]
        public ActionResult AgregarNombre(string nombre, string significado)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            sigNombresConsejos.AgregarNombre(nombre, significado); // Implementa este método
            return Json(new { mensaje = "Nombre agregado exitosamente" });
        }

        [HttpPost]
        public ActionResult EliminarNombre(string nombre)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            sigNombresConsejos.EliminarNombre(nombre); // Implementa este método
            return Json(new { mensaje = "Nombre eliminado con éxito" });
        }

        [HttpPost]
        public ActionResult ActualizarSignificado(string nombre, string significado)
        {
            SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
            sigNombresConsejos.ActualizarSignificado(nombre, significado); // Implementa este método
            return Json(new { mensaje = "Significado actualizada con éxito." });
        }

        [HttpPost]
        public ActionResult ActualizarExpediente(string nuevaAlergia, string actualAlergia)
        {
            try
            {
                SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
                sigNombresConsejos.ActualizarExpediente(nuevaAlergia, actualAlergia); // Implementa este método
                return Json(new { mensaje = "Expediente actualizado con éxito" });
            }
            catch (Exception ex)
            {
                // Captura y maneja cualquier excepción
                Console.WriteLine("Error al actualizar el expediente: " + ex.Message);
                return Json(new { mensaje = "Error al actualizar el expediente" });
            }
        }

        [HttpPost]
        public ActionResult EliminarAlergiaC(string nombreAlergia)
        {
            try
            {
                SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
                sigNombresConsejos.EliminarAlergia(nombreAlergia); // Implementa este método
                return Json(new { mensaje = "Alergia eliminada con éxito" });
            }
            catch (Exception ex)
            {
                // Captura y maneja cualquier excepción
                Console.WriteLine("Error al eliminar la alergia: " + ex.Message);
                return Json(new { mensaje = "Error al eliminar la alergia" });
            }
        }

        [HttpPost]
        public ActionResult AgregarAlergiaC(string nombreAlergia)
        {
            try
            {
                SigNombres_Consejos sigNombresConsejos = new SigNombres_Consejos();
                sigNombresConsejos.AgregarAlergia(nombreAlergia); // Implementa este método
                return Json(new { mensaje = "Alergia agregada exitosamente" });
            }
            catch (Exception ex)
            {
                // Captura y maneja cualquier excepción
                Console.WriteLine("Error al agregar la alergia: " + ex.Message);
                return Json(new { mensaje = "Error al agregar la alergia" });
            }
        }

        //Indicador Total Usuarios controller
        [HttpGet]
        public int ObtenerTotalUsuarios()
        {
            Usuarios usuarios = new Usuarios();
            int totalUsuarios = usuarios.TotalUsuarios();

            return totalUsuarios;
        }

        [HttpGet]
        //Indicador Total Bebes Controller
        public int ObtenerTotalBebes()
        {
            Usuarios bebes = new Usuarios();
            int totalBebes = bebes.TotalBebes();

            return totalBebes;
        }

        [HttpGet]
        //Indicador Total Dietas Controller
        public int ObtenerTotalDietas()
        {
            Usuarios bebes = new Usuarios();
            int totalDietas = bebes.TotalDietasBebes();

            return totalDietas;
        }


        [HttpGet]
        //Indicador Total Bebes Controller
        public int ObtenerTotalCitas()
        {
            Usuarios bebes = new Usuarios();
            int totalCitas = bebes.TotalCitasRegistradas();

            return totalCitas;
        }

        [HttpPost]
        public ActionResult DetalleBebe(int idBebe)
        {
            //var apiUrl = $"https://tiusr2pl.cuc-carrera-ti.ac.cr/APIRegistroNacional/usuarios.asmx?WSDL/{idBebe}";
            //var response = await httpClient.GetAsync(apiUrl);

            APIRegistroNacional.Usuarios1SoapClient DatosBbebe = new APIRegistroNacional.Usuarios1SoapClient();
            string jsonResult = DatosBbebe.GetUsuarios(idBebe);

            var usuarios = JsonConvert.DeserializeObject<List<Bebes>>(jsonResult);

            foreach (var usuario in usuarios)
            {
                DateTime fechaDateTime = DateTime.Parse(usuario.FechaNacimiento);

                // Formatear la fecha como una cadena con el formato deseado ("dd/MM/yyyy")
                string fechaFormateada = fechaDateTime.ToString("MM/dd/yyyy");

                // Luego, puedes asignar la fecha formateada de vuelta a la propiedad "fecha" en tu objeto "Data"
                usuario.FechaNacimiento = fechaFormateada;
            }


            return Json(usuarios);

        }
        [HttpPost]
        public ActionResult CambioBebe(string valor)
        {
            Session["idBebe"] = valor;
            Perfil perfil = new Perfil();
            DataTable dataTable = perfil.CargarPerfil(Convert.ToInt32(Session["idUsuario"]));
            DataRow fila = dataTable.Rows[0];
            var resultado = new { success = false, nombre = "", apellido = "", rol = "", clave = "", idrol = "" };
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["idBebe"].ToString() == valor)
                {
                    Session["idRol"] = row["idRoll"].ToString();
                    resultado = new
                    {
                        success = true,
                        nombre = row["NombreB"].ToString(),
                        apellido = row["ApellidosB"].ToString(),
                        rol = row["Rol"].ToString(),
                        clave = row["Clave"].ToString(),
                        idrol = row["idRoll"].ToString(),
                    };
                }
            }
            // Devolver el objeto anónimo como respuesta JSON
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Compartirclave(string clave, string correo, string bebe, string ape)
        {
            bool resultado = EnviarCorreo(correo, clave, bebe + " " + ape);
            // Devolver el objeto anónimo como respuesta JSON
            return Json(new { success = resultado });

        }
        [HttpPost]
        public ActionResult Confirmarclave(string clave)
        {
            Perfil perfil = new Perfil();

            perfil.AgregarFamiliar(Convert.ToInt32(Session["idUsuario"]), clave);
            // Devolver el objeto anónimo como respuesta JSON
            DataTable dataTable = perfil.CargarPerfil(Convert.ToInt32(Session["idUsuario"]));

            if (dataTable.Rows.Count > 0)
            {
                DataRow fila = dataTable.Rows[0];
                ViewBag.nom = fila["Nombre"];
                ViewBag.ape = fila["Apellidos"];
                ViewBag.correo = Convert.ToString(Session["correoUsuario"]);
                ViewBag.nombebe = fila["NombreB"];
                ViewBag.apebebe = fila["ApellidosB"];
                ViewBag.rol = fila["Rol"];
                ViewBag.clave = fila["Clave"];
                if (Convert.ToString(fila["idBebe"]) != "")
                {
                    Session["idBebe"] = fila["idBebe"];
                }
            }

            List<BebesP> List = new List<BebesP>();

            foreach (DataRow row in dataTable.Rows)
            {
                BebesP Data = new BebesP
                {
                    name = row["NombreB"].ToString(),
                    id = row["idBebe"].ToString(),
                    apellidos = row["ApellidosB"].ToString(),
                    rol = row["idRoll"].ToString(),
                    clave = row["Clave"].ToString(),
                    roln = row["Rol"].ToString(),
                    // ...
                };
                if (Data.id != "")
                {
                    List.Add(Data);
                }
            }

            return View("Perfil", List);
        }

        #region Alertas
        //Metodo para alertas
        public ActionResult Alertas()
        {
            if (Session["idBebe"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Alertas alertas = new Alertas();
            int idBebe = Convert.ToInt32(Session["idBebe"]);

            string Titulo = Request.Form["iptTitulo"]?.ToString();
            string Hora = Request.Form["iptHora"]?.ToString();
            string idCategoria = Request.Form["slcCategoria"]?.ToString();

            if (Titulo != null && Hora != null && idCategoria != null)
            {
                alertas.IngresarAlerta(Titulo, Hora, Convert.ToInt32(idCategoria), idBebe);
            }

            var viewModel = new AlertasModel
            {
                Alertas = alertas.VerAlertas(idBebe),
                Categorias = alertas.VerCategoriasAlertas()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ActualizarEstado(int idAlerta, bool estado)
        {
            Alertas alertas = new Alertas();
            // Llamamos al método para actualizar el estado en la base de datos
            alertas.ModificarEstado(idAlerta, Convert.ToInt32(estado));

            return Json(new { success = true }); // Enviar una respuesta JSON para indicar el éxito de la operación
        }

        public ActionResult EliminarAlerta(int idAlerta)
        {
            Alertas alertas = new Alertas();
            // Llamamos al método para eliminar la alerta en la base de datos
            alertas.EliminarAlerta(idAlerta);

            return Json(new { success = true }); // Enviar una respuesta JSON para indicar el éxito de la operación
        }
        #endregion

        #region BabyGaleria
        public ActionResult BabyGaleria()
        {
            return View();
        }

        //Redireccionamiento de los botones
        public ActionResult RedirecUltrasonidos()
        {
            return RedirectToAction("Ultrasonidos"); // Redirige a la acción "Ultrasonidos"
        }

        public ActionResult RedirecAlbumAnio()
        {
            return RedirectToAction("AlbumAnio"); // Redirige a la acción "AlbumAnio"
        }

        public ActionResult RedirecEtapasAlbum()
        {
            return RedirectToAction("EtapasAlbum"); // Redirige a la acción "EtapasAlbum"
        }

        public ActionResult RedirecNuevaAventura()
        {
            return RedirectToAction("NuevaAventura"); // Redirige a la acción "EtapasAlbum"
        }

        //Controladores de las vistas de las opciones
        public ActionResult Ultrasonidos()
        {
            int idBebe = 5435454; //Convert.ToInt32(Session["idBebe"])

            BabyGaleria bg = new BabyGaleria();
            List <ListaBabyGaleria>  lbg = bg.VerBabyGaleria(idBebe);

            return View(lbg);
        }

        public ActionResult AlbumAnio()
        {
            return View();
        }

        public ActionResult EtapasAlbum()
        {
            return View();
        }
        public ActionResult NuevaAventura(HttpPostedFileBase file, string slcTipoArchivo, string txtTitulo, string slcEtapa, string slcAlbum)
        {
            int idBebe = 305340319; //Convert.ToInt32(Session["idBebe"])


            Models.BD.BabyGaleria bg = new Models.BD.BabyGaleria();

            if (file != null && file.ContentLength > 0)
            {
                int tamanoArchivo = file.ContentLength;

                // Leer archivo y procesar si es necesario
                byte[] archivoBytes = new byte[tamanoArchivo];
                file.InputStream.Read(archivoBytes, 0, tamanoArchivo);
                bg.IngresarMultimedia(idBebe, slcTipoArchivo, archivoBytes, txtTitulo, slcEtapa, slcAlbum);
            }
            return View();
        }
        #endregion


        #region CorreoE


        public bool EnviarCorreo(string correo, string clave, string bebe)
        {
            string asunto = "Clave Bebé en BabyGuide";
            string mensaje = $@"<!DOCTYPE html>
                                            <html>
                                            <head>
                                                <title>Plantilla de Correo Electrónico</title>
                                                <style>
                                                /* Estilos generales */
                                                body {{
                                                    font-family: Arial, sans-serif;
                                                    background-color: #f5f5f5;
                                                    margin: 0;
                                                    padding: 0;
                                                }}

                                                .container {{
                                                    max-width: 600px;
                                                    margin: 0 auto;
                                                    background-color: #fff;
                                                    padding: 20px;
                                                    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                                }}

                                                /* Encabezado */
                                                .header {{
                                                    text-align: center;
                                                    margin-bottom: 20px;
                                                }}

                                                .logo {{
                                                    max-width: 200px;
                                                    height: auto;
                                                }}

                                                .company-name {{
                                                    font-size: 24px;
                                                    font-weight: bold;
                                                    margin-top: 10px;
                                                }}

                                                /* Contenido */
                                                .content {{
                                                    margin-bottom: 20px;
                                                }}

                                                .content p {{
                                                    margin-bottom: 10px;
                                                }}

                                                /* Pie de página */
                                                .footer {{
                                                    text-align: center;
                                                    font-size: 14px;
                                                    color: #999;
                                                }}
                                                </style>
                                            </head>
                                            <body>
                                                <div class=""container"">
                                                <div class=""header"">
                                                   <img class=""logo"" src=""https://tiusr2pl.cuc-carrera-ti.ac.cr/CSSResponsive/newLogo.png"" alt=""BabyGuide Logo"">
                                                </div>

                                                <div class=""content"">
                                                    <p>Se ha compartido una clave bebé contigo!!</p>
                                                    <p>Si tu no esperabas este código, solo ignoralo. Se ha compartido la clave del bebé: {bebe} para formar parte de su familia y disfrutar de nuestros servicios. Puedes usarlo cuando inicies sesión en nuestra página web https://tiusr2pl.cuc-carrera-ti.ac.cr/BabyGuide/Acceso/Login</p>
                                                    <p>La clave es: <strong>{clave}</strong></p>
                                                </div>

                                                <div class=""footer"">
                                                    <p>&copy; 2023 BabyGuide. Todos los derechos reservados.</p>
                                                </div>
                                                </div>
                                            </body>
                                            </html>";
            bool resultado;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("babyguide3@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("babyguide3@gmail.com", "inqdqakkpulstxrh"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }
        #endregion
    }
}