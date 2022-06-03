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
    public  class Pruebas : BaseTest
    {
        /// <summary>
        /// Receives the fixture to use
        /// </summary>

        public Pruebas(TestFixture fixture) : base(fixture)
        {
        }

        

        [Fact]
        public async Task Get_Pruebas()
        {
            var request = MockHttpRequestData.Create();
            var result = await this.fixture.PruebasService.GetPruebasAsync(request, 1);
            var content = await result.DeserializeAsync<Result<Prueba[]>>();
            Assert.True(content?.Success);
        }



        [Fact]
        public async Task Submit_Prueba()
        {
            var prueba = new Prueba
            {
                PruebaId = Guid.NewGuid(),
                Descripcion = "Test",
            };

            var request = MockHttpRequestData.Create(prueba);
            var result = await this.fixture.PruebasService.SubmitPruebaAsync(request);
            var content = await result.DeserializeAsync<Result<Prueba[]>>();
            Assert.True(content?.Success);
        }


    }
}
