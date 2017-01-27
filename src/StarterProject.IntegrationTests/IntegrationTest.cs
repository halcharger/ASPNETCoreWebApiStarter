using NUnit.Framework;

namespace StarterProject.IntegrationTests
{
    [TestFixture]
    public class IntegrationTest
    {
        protected TestAppServer Server;

        [OneTimeSetUp]
        public virtual void OnTimeSetUp()
        {
            Server = new TestAppServer();
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            Server?.Dispose();
        }
    }
}