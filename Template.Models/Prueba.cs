
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Template.Models
{
    /// <summary>
    /// Common properties for all models
    /// </summary>
    public class Prueba
    {
        public Guid PruebaId { get; set; }
        public DateTimeOffset FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public int Numero { get; set; }

        

    }
}
