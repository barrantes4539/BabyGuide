using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.Listas
{
    public class UsuariosModel
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string CorreoE { get; set; }
        public string Clave { get; set; }
        public string CodigoVerificacion { get; set; }

    }
}