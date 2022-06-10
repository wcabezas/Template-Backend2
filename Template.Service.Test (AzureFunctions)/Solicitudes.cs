using System;
using System.Threading.Tasks;
using Template.Common.Models;
using Template.Models;
using Template.Service.Tests.Helpers;
using Xunit;

namespace Template.Service.Tests
{
    /// <summary>
    /// Item fuction test
    /// </summary>
    [Collection(nameof(TestFixtureCollection))]
    public class Solicitudes : BaseTest
    {
        /// <summary>
        /// Receives the fixture to use
        /// </summary>

        public Solicitudes(TestFixture fixture) : base(fixture)
        {
        }



        [Fact]
        public async Task Get_Solicitudes()
        {
            var request = MockHttpRequestData.Create();
            var result = await this.fixture.SolicitudesService.GetSolicitudesAsync(request, 1);
            var content = await result.DeserializeAsync<Result<Solicitud[]>>();
            Assert.True(content?.Success);
        }



        [Fact]
        public async Task Submit_Solicitud()
        {
            var solicitud = new Solicitud
            {
                IdSolicitud = Guid.NewGuid(),
                IdInterfaz = "Interfaz de prueba",
                IdTipoConexionOrigen = "prueba",
                IdTipoConexionDestino = "prueba",
                Descripcion = "Test",
            };

            var request = MockHttpRequestData.Create(solicitud);
            var result = await this.fixture.SolicitudesService.SubmitSolicitudAsync(request);
            var content = await result.DeserializeAsync<Result<Solicitud[]>>();
            Assert.True(content?.Success);
        }


    }
}
