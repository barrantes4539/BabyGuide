using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.Listas
{
    public class AlertasModel
    {
        public List<AlertasBebe> Alertas { get; set; }
        public List<CategoriasAlertas> Categorias { get; set; }
    }
}