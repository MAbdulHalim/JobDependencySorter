using NUnit.Framework;
using JobDependencySorter;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class JobSorterTest
    {
        private JobSorter JobSorterObject { get; set; }

        [SetUp]
        public void Setup()
        {
            JobSorterObject = new JobSorter();
        }

        [Test]
        public void ProcessJobs_OnEmptyString_ReturnEmptyString()
        {
            // Arrange
            var inputString = new[] { string.Empty };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBeNullOrEmpty();
        }

        [Test]
        public void ProcessJobs_OnPassingOneJobWithNoDependency_ReturnStringOfSingleJob()
        {
            // Arrange
            var inputString = new[] { "a =>" };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("a");
        }

        [Test]
        public void ProcessJobs_OnPassingOneJobWithNoDependencyAndEmptyString_ReturnStringOfSingleJob()
        {
            // Arrange
            var inputString = new[] { "a => ", string.Empty };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("a");
        }

        [Test]
        public void ProcessJobs_OnPassingJobsWithNoDependency_ReturnStringOfSortedJobs()
        {
            // Arrange
            var inputString = new[] { "a => ", "b => ", "c => " };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("abc");
        }

        [Test]
        public void ProcessJobs_OnPassingJobsWithOneDependency_ReturnStringOfSortedJobs()
        {
            // Arrange
            var inputString = new[] { "a => ", "b => c", "c => " };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("acb");
        }

        [Test]
        public void ProcessJobs_OnPassingJobsWithMutipleDependency_ReturnStringOfSortedJobs()
        {
            // Arrange
            var inputString = new[] { "a => ", "b => c", "c => f", "d => a", "e => b", "f => " };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldBe("afcbde");
        }

        [Test]
        public void ProcessJobs_OnPassingJobWithSelfDependency_ReturnStringWithError()
        {
            // Arrange
            var inputString = new[] { "a =>", "b =>", "c => c" };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldContain("Job c can not have dependency on itself");
        }

        [Test]
        public void ProcessJobs_OnPassingJobsWithCircularDependency_ReturnErrorString()
        {
            // Arrange
            var inputString = new[] { "a => ", "b => c", "c => f", "d => a", "e => ", "f => b" };

            // Act
            var output = JobSorterObject.ProcessJobs(inputString);

            // Assert
            output.ShouldContain("Jobs can’t have circular dependencies");
        }
    }
}
