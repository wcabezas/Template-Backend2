using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Models
{
    public class Solicitud
    {
        public Guid IdSolicitud { get; set; }
        public string IdInterfaz { get; set; }
        public string IdTipoConexionOrigen { get; set; }
        public string IdTipoConexionDestino { get; set; }
        public int NumeroRITM { get; set; }
        public string Descripcion { get; set; }
        public DateTimeOffset FechaCreacion { get; set; }
        public bool Desactivado { get; set; }
        public DateTimeOffset FechaModificacion { get; set; }
        public string UM { get; set; }
        public string UA { get; set; }

    }
}
