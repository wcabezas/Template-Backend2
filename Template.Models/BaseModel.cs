
using System;

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
    }
}
