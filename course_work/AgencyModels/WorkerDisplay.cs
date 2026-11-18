// WorkerDisplay.cs
using CourseWork.EmployerModels;
using CourseWork.WorkerDetails;
using System;

namespace CourseWork.AgencyModels
{
    public class WorkerDisplay
    {
        public int Id { get; private set; }
        public Worker Worker { get; private set; }

        public string LastName => Worker.Person.Name.LastName;
        public string FirstName => Worker.Person.Name.FirstName;
        public string SurName => Worker.Person.Name.SurName;
        public string FullName => $"{LastName} {FirstName} {SurName}".Trim();

        public string PhoneNumber => Worker.Person.PhoneNumber;
        public string Email => Worker.Person.Email.Address;

        public string GenderDisplay => Worker.Sex == Gender.Male ? "М" : "Ж";
        public string Address => Worker.Address;
        public string DesiredJob => Worker.Job.Name;
        public string ExperienceDisplay => Worker.Experience.ToString();
        public string EducationDisplay => Worker.Education.ToString();
        public string WagesDisplay => Worker.Wages.ToString();
        public string DistrictDisplay => Worker.District.ToString();
        public string StatusDisplay => Worker.Status.ToString();

        public WorkerDisplay(int id, Worker worker)
        {
            if (id < 0)
                throw new ArgumentException("айди не может быть меньше нуля", nameof(id));

            if (worker == null)
                throw new ArgumentNullException(nameof(worker), "Вакансия null");

            Id = id;
            Worker = worker;
        }
    }
}
