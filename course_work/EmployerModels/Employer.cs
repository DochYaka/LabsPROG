// Employer.cs
using CourseWork.JobDetails;
using CourseWork.PersonServices;
using CourseWork.WorkerDetails;

using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace CourseWork.EmployerModels
{
    public class Employer
    {
        public string NameEmployer { get; private set; }
        public string Address { get; private set; }
        public Person Person { get; private set; }
        public string NameHRPlusEmployer => $"{NameEmployer} HR: {Person.Name.FirstName} {Person.Name.SurName}";

        private ObservableCollection<Vacancy> Vacancies { get; }
        = new ObservableCollection<Vacancy>();

        public ObservableCollection<Worker> Employees { get; }
        = new ObservableCollection<Worker>();

        public Employer(string nameEmployer, string address, Person person)
        {
            if (string.IsNullOrWhiteSpace(nameEmployer))
                throw new ArgumentException("Название компании не введёно", nameof(nameEmployer));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Адрес не введён", nameof(address));

            if (person == null)
                throw new ArgumentNullException(nameof(person), "Персона не введена!");

            NameEmployer = nameEmployer;
            Address = address;
            Person = person;
        }

        public void AddVacancy(Vacancy vacancy)
        {
            if (vacancy == null) throw new ArgumentNullException(nameof(vacancy));

            Vacancies.Add(vacancy);
        }

        public void RemoveVacancy(Vacancy vacancy)
        {
            if (vacancy == null) throw new ArgumentNullException(nameof(vacancy));

            Vacancies.Remove(vacancy);
        }

        public ObservableCollection<Vacancy> GetVacancies() => Vacancies;
    }
}