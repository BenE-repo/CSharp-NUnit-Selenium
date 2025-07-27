namespace NUnit_Selenium.Tests.Base
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public abstract class NUnit_Baseclass
    {
        public PICSTest test;

        [SetUp]
        public void Setup()
        {
            test = new();
        }

        [TearDown]
        public void Teardown()
        {
            test.driver.Quit();
            test.driver.Dispose();
        }
    }
}