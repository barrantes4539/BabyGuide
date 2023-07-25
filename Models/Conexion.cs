using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BabyGuide.Models
{
    public class Conexion
    {
        public static string cn = "Data Source=tiusr2pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019;Initial Catalog=tiusr2pl_BabyGuide;User Id=BabyGuide;Password=BabyGuideAdmin1347;";

        //public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}