using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.SqlDataAccess.Entities
{
    /// <summary>
    /// Base entity class
    /// </summary>
    public class BaseEntity
    {
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Updated { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string CreatedBy { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string UpdatedBy { get; set; }

        public bool Deleted { get; set; }

    }
}
