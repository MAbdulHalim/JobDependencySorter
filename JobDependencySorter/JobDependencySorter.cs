using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobDependencySorter
{
    /// <summary>
    /// Job Sorter class, sorts a sequence of jobs based on their dependency
    /// </summary>
    public class JobSorter
    {
        /// <summary>
        /// Sorts jobs based on their dependency
        /// </summary>
        /// <param name="jobs">array of strings of jobs to be sorted, ex: "a => " or "a => b"</param>
        /// <returns>string of sorted jobs</returns>
        public string ProcessJobs(string[] jobs)
        {
            var jobList = new List<Job>();
            try
            {
                foreach (var jobString in jobs)
                {
                    jobList.Add(ParseJobString(jobString));
                }
                var sortedJobs = SortJobs(jobList);
                var output = PrintJobs(sortedJobs);
                return output;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Parses a single job string into Job object
        /// </summary>
        /// <param name="job">job string to be parse</param>
        /// <returns>Job object</returns>
        private Job ParseJobString(string job)
        {
            if (string.IsNullOrWhiteSpace(job))
            {
                return new Job(string.Empty, null);
            }
            var splittedJob = job.Split("=>", StringSplitOptions.RemoveEmptyEntries);
            // trim spaces at the beginning / end of each string
            for (int i = 0; i < splittedJob.Length; i++)
            {
                splittedJob[i] = splittedJob[i].Trim();
            }
            if (splittedJob.Length == 1)
            {
                return new Job(splittedJob[0], null);
            }
            if (splittedJob[0] == splittedJob[1])
            {
                throw new Exception($"Job {splittedJob[0]} can not have dependency on it self");
            }
            return new Job(splittedJob[0], splittedJob[1]);
        }

        /// <summary>
        /// Takes a list of unsorted Jobs and sort them based on their dependency, 
        /// more info: https://en.wikipedia.org/wiki/Topological_sorting
        /// </summary>
        /// <param name="jobs">List of Jobs</param>
        /// <returns>List of Jobs sorted</returns>
        private List<Job> SortJobs(List<Job> jobs)
        {
            var sortedJobs = new List<Job>();
            var visitedJobs = new List<Job>();
            foreach (var job in jobs)
            {
                VisitJob(job, sortedJobs, visitedJobs, jobs);
            }
            return sortedJobs;
        }

        /// <summary>
        /// Goes through every job dependency and it to the sorted list and mark this job as visited
        /// </summary>
        /// <param name="job">the job object to get its dependency</param>
        /// <param name="sortedJobs">list of sorted jobs</param>
        /// <param name="visitedJobs">list of visited jobs</param>
        /// <param name="unsortedJobs">list of unsorted jobs(the original ones)</param>
        private void VisitJob(Job job, List<Job> sortedJobs, List<Job> visitedJobs, List<Job> unsortedJobs)
        {
            // job already visited before, return unless there is circular dependencies
            if (visitedJobs.Any(x => x.Name.Equals(job.Name)))
            {
                // if not exist in the sorted list, then circular dependencies found
                if (!sortedJobs.Any(x => x.Name.Equals(job.Name)))
                {
                    throw new Exception("Jobs can’t have circular dependencies");
                }
                return;
            }
            visitedJobs.Add(job);

            // visit the job's dependency (in case it has) and add it to the sorted list first
            if (!string.IsNullOrWhiteSpace(job.Dependency))
            {
                var dependencyJob = unsortedJobs.Where(x => x.Name == job.Dependency).FirstOrDefault();
                if (dependencyJob != null)
                {
                    VisitJob(dependencyJob, sortedJobs, visitedJobs, unsortedJobs);
                }
            }
            // add the current job to the sorted list after looping through its dependencies
            sortedJobs.Add(job);
        }

        /// <summary>
        /// Takes a list of Jobs and prints their names in one string
        /// </summary>
        /// <param name="jobs">job list to be printed</param>
        /// <returns>job names in string</returns>
        private string PrintJobs(List<Job> jobs)
        {
            var output = new StringBuilder();
            foreach (var job in jobs)
            {
                output.Append(job.Name);
            }
            return output.ToString();
        }
    }
}
