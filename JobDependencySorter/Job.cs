using System;
using System.Collections.Generic;
using System.Text;

namespace JobDependencySorter
{
    public class Job
    {
        public string Name { get; set; }
        public string Dependency { get; set; }

        public Job(string job, string dependency)
        {
            Name = job;
            Dependency = dependency;
        }
    }
}
