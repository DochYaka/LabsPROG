// Worker.cs
using CourseWork.Interfaces;
using CourseWork.JobDetails;
using CourseWork.PersonServices;

using System;
using System.Xml.Linq;

namespace CourseWork.WorkerDetails
{
    public class Worker
    {
        private const double MinWagesValue = 25000.0;
        private const double MaxWagesValue = 999999.0;

        private const double MinExperienceValue = 0.0;
        private const double MaxExperienceValue = 50.0;

        public Person Person { get; private set; }
        public Gender Sex { get; private set; }
        public string Address { get; private set; }
        public Education Education { get; private set; }
        public Experience Experience { get; private set; } 
        public Job Job { get; private set; }
        public Wages Wages { get; private set; }
        public District District { get; private set; } 
        public Status Status { get; private set; }

        public Worker(Person person, Gender sex, string address, Education education,
         Experience experience, Job job, Wages wages, District district)
        {
            Address = address ?? throw new ArgumentNullException("Адрес не введён!", nameof(address));

            Job = job ?? throw new ArgumentNullException("Должность не выбрана!", nameof(job));

            if (person == null)
                throw new ArgumentNullException("Персона не введена!", nameof(person));

            if (experience.Value < MinExperienceValue || experience.Value > MaxExperienceValue)
            {
                throw new ArgumentException("Опыт введён неверно", nameof(experience));
            }  

            if (wages.Value < MinWagesValue || wages.Value > MaxWagesValue)
            {
                throw new ArgumentException("Зарплата введена неверно", nameof(wages));;
            }

            Person = person;
            Sex = sex;
            Education = education;
            Experience = experience;
            Wages = wages;
            District = district;
        }

        public void SetStatus(Status status)
        {
            Status = status;
        }

        public bool checkStatus()
        {
            if (Status == Status.Устроенный) return true;
            return false;
        }

        public XElement ToXml()
        {
            return new XElement("Worker",
                new XElement("Person",
                    new XElement("Name",
                        new XElement("LastName", Person.Name.LastName),
                        new XElement("FirstName", Person.Name.FirstName),
                        new XElement("SurName", Person.Name.SurName)
                    ),
                    new XElement("Email", Person.Email.Address),
                    new XElement("PhoneNumber", Person.PhoneNumber)
                ),
                new XElement("Sex", Sex.ToString()),
                new XElement("Address", Address),
                new XElement("Education", Education.ToString()),
                new XElement("Experience", Experience.Value),
                new XElement("Job", Job.Name),
                new XElement("Wages", Wages.Value),
                new XElement("District", District.ToString()),
                new XElement("Status", Status.ToString())
            );
        }
    }
}