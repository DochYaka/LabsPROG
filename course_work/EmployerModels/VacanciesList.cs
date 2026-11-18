// VacanciesList.cs
using CourseWork.JobDetails;

using System;
using System.Collections.Generic;
using System.Windows;

namespace CourseWork.EmployerModels
{
    public class VacanciesList
    {
        private List<Vacancy> vacancies = new List<Vacancy>()
        {
            new Vacancy
            (
                job: new Job("Программист"),
                education: Education.Высшее,
                experience: new Experience(2.0),
                wages: new Wages(100000.0),
                district: District.Железнодорожный
            ),
        };
    }
}