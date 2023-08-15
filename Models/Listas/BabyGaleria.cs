using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.Listas
{
    public class ListaBabyGaleria
    {
        public string idBebe { get; set; }
        public string TipoArchivo { get; set; }
        public string Archivo { get; set; }
        public string Titulo { get; set; }
        public string Etapa { get; set; }
        public string Fecha { get; set; }
        public string Album { get; set; }
    }
}