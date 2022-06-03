namespace Template.SqlDataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Template.Models;
    

    /// <summary>
    /// Sample item 
    /// </summary>
    public class PruebaEntity 
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PruebaId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Descripcion { get; set; }

        public DateTimeOffset Fecha { get; set; }

        public bool Deleted { get; set; }

        /// <summary>
        /// Returns the model using this entity info
        /// </summary>        
        public Prueba ToModel()
        {
            return new Prueba
            {
                PruebaId = this.PruebaId,
                Descripcion = this.Descripcion,
                Fecha = this.Fecha,
            };
        }


        /// <summary>
        /// Cretea a new entity based on a item
        /// </summary>        
        public static PruebaEntity FromModel(Prueba prueba)
        {
            return new PruebaEntity 
            {
                PruebaId = prueba.PruebaId,
                Descripcion = prueba.Descripcion,
                Fecha = prueba.Fecha,
            };
        }

    }
}
