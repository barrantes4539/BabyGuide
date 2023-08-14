using BabyGuide.Models;
using BabyGuide.Models.BD;
using BabyGuide.Models.Listas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Web.UI.WebControls;

namespace BabyGuide.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso

        UsuariosModel datosUsuario = new UsuariosModel();
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
        public ActionResult Login(string correo, string password)
        {
            Usuarios usuarios = new Usuarios();
            string mensaje = usuarios.VerificarCredenciales(correo, password);

            if (mensaje == "Acceso concedido")
            {
                // Si el login es exitoso, almacenamos el correo electrónico del usuario en una variable de sesión.
                Session["correoUsuarioTemporal"] = correo;
                Console.WriteLine(usuarios.IdUsuarioLogueado(correo));
                Session["idUsuario"] = usuarios.IdUsuarioLogueado(correo);


                bool respuesta = EnviarVerificacion(Session["correoUsuarioTemporal"].ToString(), out mensaje);

                return RedirectToAction("DobleVerificacion", "Acceso"); // Redireccionar a una página de bienvenida o dashboard después del login exitoso.
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


        //Envío por correo
        public bool EnviarCorreo(string correo, string asunto, string mensaje)
        {


            // Acceder al valor de la variable de sesión "correoUsuario".
            string correoUsuario = Session["correoUsuarioTemporal"] as string;
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correoUsuario);
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

        public bool EnviarVerificacion(string correoUsuario, out string mensaje)
        {
            Usuarios negocioUsuarios = new Usuarios();
            UsuariosModel datosUsuario = new UsuariosModel();
            // Acceder al valor de la variable de sesión "correoUsuario".
            correoUsuario = Session["correoUsuarioTemporal"] as string;
            datosUsuario.CodigoVerificacion = negocioUsuarios.CodigoVerificacionLogin();


            mensaje = string.Empty;


            bool codigoInsertado = negocioUsuarios.InsertarCodigoVerificacion(correoUsuario, datosUsuario.CodigoVerificacion, out mensaje);

            //Verificacion codigo insertado en la base de datos
            if (codigoInsertado)
            {
                string asunto = "Verificación de cuenta";
                string mensajeCorreo = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        .bodyb {{
            max-width: 600px;
            width: 600px;
            margin: 0 auto;
            font-family: ""Montserrat"", sans-serif;
        }}

        .div-logo {{
            background: #C7E3E9;
            padding: 15px;
            justify-content: center;
            text-align: center;
        }}

        .div-logo img {{
            width: 200px;
            height: 100px;
        }}


        .div-img img {{
            display: flex;
            width: 600px;
            height: 150px;
        }}

        .div-info {{
            background: #E8E8E8;
            padding-top: 50px;
            padding-bottom: 100px;
            border-top: 10px solid #00DDBD;
            margin-bottom: 100px;
            position: relative;
        }}

        .borde-titulo {{
            border-top: 4px solid black;
            border-bottom: 4px solid black;
            width: 80%;
            text-align: center;
            margin-left: 60px;
        }}

        .sonaja {{
            position: absolute;
            width: 40px;
            height: 40px;
            right: 10px;
            margin-left: 30px;
        }}

        .oso {{
            position: absolute;
            width: 100px;
            height: 100px;
            bottom: 30px;
            left: 40px;
            margin-left: 70px;
        }}

        h1 {{
            color: #426E77;
            font-family: ""Book Antiqua"";
        }}

        h2 {{
            color: #00BEA3;
            margin-left: 150px;
        }}

        h3 {{
            color: #426E77;
            margin-left: 250px;
        }}

        .footer {{
            text-align: center;
            font-size: 14px;
            color: #999;
        }}
    </style>
</head>
<body>
  <div class=""bodyb"">
    <div class=""div-logo""><img src=""https://tiusr2pl.cuc-carrera-ti.ac.cr/CSSResponsive/newLogo.png""></div>
    <div class=""div-img""><img src=""https://tiusr2pl.cuc-carrera-ti.ac.cr/CSSResponsive/BebeCaminando.jpg""></div>
    <div class=""div-info"">
        <div class=""borde-titulo"">
            <h1>Bienvenido/a de nuevo!! <img name=""sonaja"" class=""sonaja"" src=""https://tiusr2pl.cuc-carrera-ti.ac.cr/CSSResponsive/sonajero.png""></h1>
        </div>
        <h2>Tu código para acceder es: </h2>
        <h3>{datosUsuario.CodigoVerificacion}</h3>
        
        <img name=""oso"" class=""oso"" src=""https://tiusr2pl.cuc-carrera-ti.ac.cr/CSSResponsive/oso-de-peluche.png"">
    </div>
    <div class=""footer"">
        <p>&copy; 2023 BabyGuide. Todos los derechos reservados.</p>
    </div>
  </div>
</body>
</html>
";

                bool respuesta = EnviarCorreo(correoUsuario, asunto, mensajeCorreo);

                //Verificacion envio de correo
                if (respuesta)
                {
                    return true;
                }
                else
                {
                    mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                mensaje = "No se pudo enviar el código de verificación";
                return false;
            }

        }

        [HttpPost]
        public ActionResult DobleVerificacion(string codigoVerificacion)
        {
            string correoUsuario = Session["correoUsuarioTemporal"] as string;
            Usuarios negocios = new Usuarios();


            string mensaje = string.Empty;
            string codigoGenerado = negocios.CodigoVerificacion(correoUsuario);

            if (codigoGenerado != null && codigoGenerado.Equals(codigoVerificacion))
            {

                negocios.EliminarCodigo(correoUsuario);
                Session["correoUsuario"] = Session["correoUsuarioTemporal"];
                Session["idBebe"] = negocios.idBebe(Convert.ToInt32(Session["idUsuario"]));
                if (Session["idBebe"] != null)
                {
                    Session["idRol"] = negocios.idRol(Convert.ToInt32(Session["idBebe"]));
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Código de verificación incorrecto";
                return View();
            }
        }
    }
}
