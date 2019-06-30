using System;
using System.Collections.Generic;
using System.Text;

namespace JobDependencySorter.Model
{
    /// <summary>
    /// Job class stores information about job name and its dependency
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Job name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// name of the dependency job which the current Job object depends on it
        /// </summary>
        public string Dependency { get; set; }

        public Job(string job, string dependency)
        {
            Name = job;
            Dependency = dependency;
        }
    }
}
