using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunat.WebApi.Models
{
    public class Contribuyente
    {
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Condicion { get; set; }
        public string NombreComercial { get; set; }
        public string Tipo { get; set; }
        public string Inscripcion { get; set; }
        public string Estado { get; set; }
        public string Direccion { get; set; }
        public string SistemaEmision { get; set; }
        public string ActividadExterior { get; set; }
        public string SistemaContabilidad { get; set; }
        public string EmisionElectronica { get; set; }
        public string PLE { get; set; }
        public object RepresentantesLegales { get; set; }
        public object CantidadTrabajadores { get; set; }
    }
}
