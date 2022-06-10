namespace Template.SqlDataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Template.Models;


    /// <summary>
    /// Sample item 
    /// </summary>
    public class SolicitudEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid IdSolicitud { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string IdInterfaz { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string IdTipoConexionOrigen { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string IdTipoConexionDestino { get; set; }

        public int NumeroRITM { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Descripcion { get; set; }
        public DateTimeOffset FechaCreacion { get; set; }
        public bool Desactivado { get; set; }
        public DateTimeOffset FechaModificacion { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string UM { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string UA { get; set; }

        /// <summary>
        /// Returns the model using this entity info
        /// </summary>        
        public Solicitud ToModel()
        {
            return new Solicitud
            {
                IdSolicitud = this.IdSolicitud,
                IdInterfaz = this.IdInterfaz,
                IdTipoConexionOrigen = this.IdTipoConexionOrigen,
                IdTipoConexionDestino = this.IdTipoConexionDestino,
                NumeroRITM = this.NumeroRITM,
                Descripcion = this.Descripcion,
                FechaCreacion = this.FechaCreacion,
                Desactivado = this.Desactivado,
                FechaModificacion = this.FechaModificacion,
                UM = this.UM,
                UA = this.UA,
            };
        }


        /// <summary>
        /// Cretea a new entity based on a item
        /// </summary>        
        public static SolicitudEntity FromModel(Solicitud solicitud)
        {
            return new SolicitudEntity
            {
                IdSolicitud = solicitud.IdSolicitud,
                IdInterfaz = solicitud.IdInterfaz,
                IdTipoConexionOrigen = solicitud.IdTipoConexionOrigen,
                IdTipoConexionDestino = solicitud.IdTipoConexionDestino,
                NumeroRITM = solicitud.NumeroRITM,
                Descripcion = solicitud.Descripcion,
                FechaCreacion = solicitud.FechaCreacion,
                Desactivado = solicitud.Desactivado,
                FechaModificacion = solicitud.FechaModificacion,
                UM = solicitud.UM,
                UA = solicitud.UA,
            };
        }

    }
}
