using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.SqlDataAccess.Migrations
{
    public partial class SeagregatablaSolicitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    IdSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdInterfaz = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IdTipoConexionOrigen = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IdTipoConexionDestino = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    NumeroRITM = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    FechaCreacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Desactivado = table.Column<bool>(type: "bit", nullable: false),
                    FechaModificacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UM = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UA = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.IdSolicitud);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitudes");
        }
    }
}
