using NUnit.Framework;
using JobDependencySorter;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class JobDependencySorterTest
    {
        private JobSorter TestObject { get; set; }

        [SetUp]
        public void Setup()
        {
            TestObject = new JobSorter();
        }

        [Test]
        public void ProcessJobs_OnEmptyString_ReturnEmptyString()
        {
            // Arrange
            var inputString = string.Empty;

            // Act
            var output = TestObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBeNullOrEmpty();
        }
    }
}