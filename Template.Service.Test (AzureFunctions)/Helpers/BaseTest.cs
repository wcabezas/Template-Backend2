using Xunit;


namespace Template.Service.Tests.Helpers
{
    /// <summary>
    /// Base class for all test
    /// </summary>
    [Collection(nameof(TestFixtureCollection))]
    public class BaseTest
    {
        protected TestFixture fixture;

        /// <summary>
        /// Gets the fixture to use
        /// </summary>
        public BaseTest(TestFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
