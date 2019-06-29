using System;
using System.Collections.Generic;
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
            foreach (var jobString in jobs)
            {
                try
                {
                    jobList.Add(ParseJobString(jobString));
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            var sortedJobs = SortJobs(jobList);
            var output = PrintJobs(sortedJobs);
            return output;
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
            if(splittedJob[0] == splittedJob[1])
            {
                throw new Exception($"Job {splittedJob[0]} can not have dependency on it self");
            }
            return new Job(splittedJob[0], splittedJob[1]);
        }

        /// <summary>
        /// Takes a list of unsorted Jobs and sort them based on their dependency
        /// </summary>
        /// <param name="jobs">List of Jobs</param>
        /// <returns>List of Jobs sorted</returns>
        private List<Job> SortJobs(List<Job> jobs)
        {
            var sortedJobs = new List<Job>();
            foreach (var job in jobs)
            {
                if (string.IsNullOrWhiteSpace(job.Dependency))
                {
                    sortedJobs.Add(job);
                }
            }
            return sortedJobs;
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
