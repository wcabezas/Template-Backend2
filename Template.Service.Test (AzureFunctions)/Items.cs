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
    public  class Items : BaseTest
    {
        /// <summary>
        /// Receives the fixture to use
        /// </summary>

        public Items(TestFixture fixture) : base(fixture)
        {
        }


        [Fact]
        public async Task Get_Items()
        {
            var request = MockHttpRequestData.Create();
            var result = await this.fixture.ItemsService.GetItemsAsync(request, 1);
            var content = await result.DeserializeAsync<Result<Item[]>>();
            Assert.True(content?.Success);
        }



        [Fact]
        public async Task Submit_Item()
        {
            var item = new Item
            {
                ItemId = Guid.NewGuid(),
                Text = "Test",
            };

            var request = MockHttpRequestData.Create(item);
            var result = await this.fixture.ItemsService.SubmitItemAsync(request);
            var content = await result.DeserializeAsync<Result<Item>>();
            Assert.True(content?.Success);
        }


    }
}
