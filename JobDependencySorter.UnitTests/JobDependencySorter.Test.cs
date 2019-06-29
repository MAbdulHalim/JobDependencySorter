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
            var inputString = new[] { string.Empty };

            // Act
            var output = TestObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBeNullOrEmpty();
        }

        [Test]
        public void ProcessJobs_OnPassingOneJobWithSelfDependency_ReturnStringWithError()
        {
            // Arrange
            var inputString = new[] { "a => a" };

            // Act
            var output = TestObject.ProcessJobs(inputString);

            // Assert
            output.ShouldContain("Job a can not have dependency on it self");
        }

        [Test]
        public void ProcessJobs_OnPassingOneJobWithNoDependency_ReturnStringOfSingleJob()
        {
            // Arrange
            var inputString = new[] { "a =>" };

            // Act
            var output = TestObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("a");
        }

        [Test]
        public void ProcessJobs_OnPassingOneJobWithNoDependencyAndEmptyString_ReturnStringOfSingleJob()
        {
            // Arrange
            var inputString = new[] { "a => ", string.Empty };

            // Act
            var output = TestObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("a");
        }

        [Test]
        public void ProcessJobs_OnPassingJobsWithNoDependency_ReturnStringJobs()
        {
            // Arrange
            var inputString = new[] { "a => ", "b => ", "c => " };

            // Act
            var output = TestObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("abc");
        }
    }
}