using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.Listas
{
    public class AlertasBebe
    {
        public string idAlerta { get; set; }
        public string Titulo { get; set; }
        public string Hora { get; set; }
        public string idCategoria { get; set; }
        public string Estado { get; set; }
    }
}