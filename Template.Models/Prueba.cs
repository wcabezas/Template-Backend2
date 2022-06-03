
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
        public DateTimeOffset Fecha { get; set; }
        public string Descripcion { get; set; }

        [JsonProperty("_links")]
        public Dictionary<string, string> Links { get; set; }

    }
}
