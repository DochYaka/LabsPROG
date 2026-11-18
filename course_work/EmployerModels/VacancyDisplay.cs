// VacancyDisplay.cs

using CourseWork.JobDetails;
using System;

namespace CourseWork.EmployerModels
{
    public class VacancyDisplay
    {
        public Vacancy Vacancy { get; private set; }
        public string JobDisplay => Vacancy.Job.ToString();
        public string EducationDisplay => Vacancy.Education.ToString();
        public string ExperienceDisplay => Vacancy.Experience.ToString();
        public string WagesDisplay => Vacancy.Wages.ToString();
        public string DistrictDisplay => Vacancy.District.ToString();

        public VacancyDisplay(Vacancy vacancy) 
        {
            if (vacancy == null)
                throw new ArgumentNullException("Вакансия null", nameof(vacancy));

            Vacancy = vacancy; 
        }
    }
}