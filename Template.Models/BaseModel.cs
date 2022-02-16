
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Template.Models
{
    /// <summary>
    /// Common properties for all models
    /// </summary>
    public class BaseModel
    {
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }


        [JsonProperty("_links")]
        public Dictionary<string, string> Links{ get; set; }
    }
}
