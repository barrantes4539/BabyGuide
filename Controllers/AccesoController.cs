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
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult DobleVerificacion()
        {
            return View();
        }




        [HttpPost]
        public ActionResult Login(string correoE, string password)
        {
            Usuarios usuarios = new Usuarios();
            string mensaje = usuarios.VerificarCredenciales(correoE, password);

            if (mensaje == "Acceso concedido")
            {
                // Si el login es exitoso, almacenamos el correo electrónico del usuario en una variable de sesión.
                Session["correoUsuario"] = correoE;
                return RedirectToAction("Index", "Home"); // Redireccionar a una página de bienvenida o dashboard después del login exitoso.
            }
            else
            {
                // Mostrar el mensaje de error en el div "errorDiv" en la vista Login.
                ViewBag.ErrorMessage = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Registro(string nombre, string apellido1, string apellido2, string correoE, string password)
        {
            Usuarios usuarios = new Usuarios();
            string mensaje = usuarios.RegistrarUsuario(nombre, apellido1, apellido2, correoE, password);

            if (mensaje == "Usuario registrado exitosamente")
            {
                // Redireccionar a una página de bienvenida o dashboard después del registro exitoso.
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                // Mostrar el mensaje de error en la vista Registro.
                ViewBag.Error = mensaje;
                return View();
            }
        }
    }
}
