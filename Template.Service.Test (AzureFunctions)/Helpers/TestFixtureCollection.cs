using Xunit;

namespace Template.Service.Tests.Helpers
{
    /// <summary>
    /// Allows to use one single fixture por all test 
    /// (needed in order to use just one azure function host process)
    /// </summary>
    [CollectionDefinition(nameof(TestFixtureCollection))]
    public class TestFixtureCollection : ICollectionFixture<TestFixture>
    {

    }
}
