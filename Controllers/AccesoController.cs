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
                string mensajeCorreo = @"<!DOCTYPE html>
                                            <html>
                                            <head>
                                              <title>Plantilla de Correo Electrónico</title>
                                              <style>
                                                /* Estilos generales */
                                                body {
                                                  font-family: Arial, sans-serif;
                                                  background-color: #f5f5f5;
                                                  margin: 0;
                                                  padding: 0;
                                                }
    
                                                .container {
                                                  max-width: 600px;
                                                  margin: 0 auto;
                                                  background-color: #fff;
                                                  padding: 20px;
                                                  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                                }
    
                                                /* Encabezado */
                                                .header {
                                                  text-align: center;
                                                  margin-bottom: 20px;
                                                }
    
                                                .logo {
                                                  max-width: 200px;
                                                  height: auto;
                                                }
    
                                                .company-name {
                                                  font-size: 24px;
                                                  font-weight: bold;
                                                  margin-top: 10px;
                                                }
    
                                                /* Contenido */
                                                .content {
                                                  margin-bottom: 20px;
                                                }
    
                                                .content p {
                                                  margin-bottom: 10px;
                                                }
    
                                                /* Pie de página */
                                                .footer {
                                                  text-align: center;
                                                  font-size: 14px;
                                                  color: #999;
                                                }
                                              </style>
                                            </head>
                                            <body>
                                              <div class=""container"">
                                                <div class=""header"">
                                                  <img class=""logo"" alt=""Logo"">
                                                  <h1 class=""company-name"">BabyGuide</h1>
                                                </div>
    
                                                <div class=""content"">
                                                  <p>¡Gracias por elegir nuestros servicios! Estamos encantados de atender sus necesidades.</p>
                                                  <p>Su código para acceder es: " + datosUsuario.CodigoVerificacion + @"</p>
                                                </div>
    
                                                <div class=""footer"">
                                                  <p>&copy; 2023 BabyGuide. Todos los derechos reservados.</p>
                                                </div>
                                              </div>
                                            </body>
                                            </html>";
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
