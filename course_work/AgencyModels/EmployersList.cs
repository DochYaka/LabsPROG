// EmployersList.cs
using CourseWork.EmployerModels;
using CourseWork.JobDetails;
using CourseWork.PersonServices;

using System.Collections.ObjectModel;
using System.Net;

namespace CourseWork.AgencyModels
{
    public class EmployersList
    {
        private readonly ObservableCollection<Employer> _employers = new ObservableCollection<Employer>()
        {
            new Employer
            (
                nameEmployer: "ф",
                address: "Красноярская 35",
                person: new Person(
                    new Name("ф", "ф", "Дмитриевна"),
                    new Email("Yandex.ru"),
                    "88003339639")
            ),
            new Employer
            (
                nameEmployer: "a",
                address: "Красноярская 35",
                person: new Person(
                    new Name("a", "a", "Дмитриевна"),
                    new Email("Yandex.ru"),
                    "88003339639")
            ),
            new Employer
            (
                nameEmployer: "Яндекс",
                address: "Красноярская 35",
                person: new Person(
                    new Name("Американо", "Диана", "Дмитриевна"),
                    new Email("Yandex.ru"),
                    "88003339639")
            ),

            new Employer
            (
                nameEmployer: "Яндекс",
                address: "Красноярская",
                person: new Person(
                    new Name("Алтайский", "Сергей", "Дмитриевич"),
                    new Email("Yandex.ru"),
                    "88004449639")
            ),

            new Employer
            (
                nameEmployer: "Сбербанк",
                address: "Ленина 59",
                person: new Person(
                    new Name("Акано", "Дмитрий", "Дмитриевич"),
                    new Email("Sberbank.ru"),
                    "88005555550")
            ),

            new Employer
            (
                nameEmployer: "Элтех",
                address: "Фрунзе 242",
                person: new Person(
                    new Name("Нока", "Яна", "Сергеевна"),
                    new Email("Eltech.ru"),
                    "88005050040")
            )
        };

        public ReadOnlyObservableCollection<Employer> employers { get; }

        public EmployersList()
        {
            employers = new ReadOnlyObservableCollection<Employer>(_employers);
        }

        public void AddEmployer(Employer employer)
        {
            if (employer == null) return;

            _employers.Add(employer);
        }

        public void RemoveEmployer(Employer employer)
        {
            if (employer == null) return;
            _employers.Remove(employer); 
        }

        public Vacancy CreateVacancyFromForm(Job job, Education education, 
            Experience experience, Wages wages, District district)
        {
            return new Vacancy
            (
                job: job,
                education: education,
                experience: experience,
                wages: wages,
                district: district
            );
        }

        public Employer CreateEmployerFromForm(string nameEmployer, string address, Person person)
        {
            return new Employer
            (
                nameEmployer: nameEmployer,
                address: address,
                person: person
            );
        }
    }
}