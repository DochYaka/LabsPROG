using CourseWork.JobDetails;

namespace CourseWork.Interfaces
{
    public interface IVacancy
    {
        Job Job { get; }
        Education Education { get; }
        Experience Experience { get; }
        Wages Wages { get; }
        District District { get; }
    }
}