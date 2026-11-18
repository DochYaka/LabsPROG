// Job.cs
using CourseWork.Interfaces;

namespace CourseWork.JobDetails
{
    public class Job
    {
        public string Name { get; }

        public Job(string name) { Name = name; }

        public override string ToString() {  return Name; }
    }
}