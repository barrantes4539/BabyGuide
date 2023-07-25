using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.Listas
{
    public class ExpedienteModel
    {
        public List<Alergias> Alergias { get; set; }
        public List<Vacunas> Vacunas { get; set; }
        public List<Medicamentos> Medicamentos { get; set; }
    }
}