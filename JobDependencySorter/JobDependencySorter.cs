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
                    var jobObject = ParseJobString(jobString);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Parses a single job into Job object
        /// </summary>
        /// <param name="job">job string to be parse</param>
        /// <returns>Job object</returns>
        private Job ParseJobString(string job)
        {
            if (string.IsNullOrWhiteSpace(job))
            {
                return new Job(string.Empty, null);
            }
            var splittedJob = job.Split(" => ");
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
    }
}
