// XmlSerializerHelper.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CourseWork.AgencyModels;
using CourseWork.EmployerModels;
using CourseWork.JobDetails;
using CourseWork.PersonServices;
using CourseWork.WorkerDetails;

namespace CourseWork.Utility
{
    public static class XmlSerializerHelper
    {
        public static void SaveToXml(string filePath, Agency agency)
        {
            var root = new XElement("AgencyData");

            var workersElement = new XElement("Workers");
            foreach (var worker in agency.workers.workers)
            {
                workersElement.Add(SerializeWorker(worker));
            }
            root.Add(workersElement);

            var employersElement = new XElement("Employers");
            foreach (var employer in agency.employers.employers)
            {
                employersElement.Add(SerializeEmployer(employer));
            }
            root.Add(employersElement);

            var vacanciesElement = new XElement("AllVacancies");
            foreach (var employer in agency.employers.employers)
            {
                foreach (var vacancy in employer.GetVacancies())
                {
                    vacanciesElement.Add(SerializeVacancy(vacancy, employer.NameEmployer));
                }
            }
            root.Add(vacanciesElement);

            root.Save(filePath);
        }

        public static void LoadFromXml(string filePath, Agency agency)
        {
            if (!System.IO.File.Exists(filePath))
                return;

            var doc = XDocument.Load(filePath);
            var root = doc.Root;

            ClearCollections(agency);

            var workersElement = root.Element("Workers");
            if (workersElement != null)
            {
                foreach (var workerElement in workersElement.Elements("Worker"))
                {
                    var worker = DeserializeWorker(workerElement);
                    if (worker != null)
                        agency.workers.AddWorker(worker);
                }
            }

            var employersElement = root.Element("Employers");
            if (employersElement != null)
            {
                foreach (var employerElement in employersElement.Elements("Employer"))
                {
                    var employer = DeserializeEmployer(employerElement);
                    if (employer != null)
                        agency.employers.AddEmployer(employer);
                }
            }

            var vacanciesElement = root.Element("AllVacancies");
            if (vacanciesElement != null)
            {
                foreach (var vacancyElement in vacanciesElement.Elements("Vacancy"))
                {
                    var vacancy = DeserializeVacancy(vacancyElement, agency);
                    if (vacancy != null)
                    {
                        var employerName = vacancyElement.Element("EmployerName")?.Value;
                        var employer = agency.employers.employers.FirstOrDefault(e => e.NameEmployer == employerName);
                        employer?.AddVacancy(vacancy);
                    }
                }
            }
        }

        private static void ClearCollections(Agency agency)
        {
            var workersToRemove = agency.workers.workers.ToList();
            foreach (var worker in workersToRemove)
            {
                agency.workers.RemoveWorker(worker);
            }

            var employersToRemove = agency.employers.employers.ToList();
            foreach (var employer in employersToRemove)
            {
                agency.employers.RemoveEmployer(employer);
            }
        }

        private static XElement SerializeWorker(Worker worker)
        {
            return new XElement("Worker",
                new XElement("LastName", worker.Person.Name.LastName),
                new XElement("FirstName", worker.Person.Name.FirstName),
                new XElement("SurName", worker.Person.Name.SurName),
                new XElement("Email", worker.Person.Email.Address),
                new XElement("PhoneNumber", worker.Person.PhoneNumber),
                new XElement("Gender", worker.Sex.ToString()),
                new XElement("Address", worker.Address),
                new XElement("Experience", worker.Experience.Value),
                new XElement("Education", worker.Education.ToString()),
                new XElement("Job", worker.Job.Name),
                new XElement("Wages", worker.Wages.Value),
                new XElement("District", worker.District.ToString()),
                new XElement("Status", worker.Status.ToString())
            );
        }

        private static Worker DeserializeWorker(XElement element)
        {
            try
            {
                var name = new Name(
                    element.Element("LastName")?.Value ?? "",
                    element.Element("FirstName")?.Value ?? "",
                    element.Element("SurName")?.Value ?? ""
                );

                var email = new Email(element.Element("Email")?.Value ?? "");

                var person = new Person(
                    name,
                    email,
                    element.Element("PhoneNumber")?.Value ?? ""
                );

                Enum.TryParse<Gender>(element.Element("Gender")?.Value, out var gender);
                Enum.TryParse<Education>(element.Element("Education")?.Value, out var education);
                Enum.TryParse<District>(element.Element("District")?.Value, out var district);
                Enum.TryParse<Status>(element.Element("Status")?.Value, out var status);

                double.TryParse(element.Element("Experience")?.Value, out var experience);
                double.TryParse(element.Element("Wages")?.Value, out var wages);

                var worker = new Worker(
                    person,
                    gender,
                    element.Element("Address")?.Value ?? "",
                    education,
                    new Experience(experience),
                    new Job(element.Element("Job")?.Value ?? ""),
                    new Wages(wages),
                    district
                );

                worker.SetStatus(status);
                return worker;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static XElement SerializeEmployer(Employer employer)
        {
            return new XElement("Employer",
                new XElement("Name", employer.NameEmployer),
                new XElement("Address", employer.Address),
                new XElement("HR_LastName", employer.Person.Name.LastName),
                new XElement("HR_FirstName", employer.Person.Name.FirstName),
                new XElement("HR_SurName", employer.Person.Name.SurName),
                new XElement("HR_Email", employer.Person.Email.Address),
                new XElement("HR_Phone", employer.Person.PhoneNumber)
            );
        }

        private static Employer DeserializeEmployer(XElement element)
        {
            try
            {
                var hrName = new Name(
                    element.Element("HR_LastName")?.Value ?? "",
                    element.Element("HR_FirstName")?.Value ?? "",
                    element.Element("HR_SurName")?.Value ?? ""
                );

                var hrEmail = new Email(element.Element("HR_Email")?.Value ?? "");

                var hrPerson = new Person(
                    hrName,
                    hrEmail,
                    element.Element("HR_Phone")?.Value ?? ""
                );

                return new Employer(
                    element.Element("Name")?.Value ?? "",
                    element.Element("Address")?.Value ?? "",
                    hrPerson
                );
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static XElement SerializeVacancy(Vacancy vacancy, string employerName)
        {
            return new XElement("Vacancy",
                new XElement("EmployerName", employerName),
                new XElement("Job", vacancy.Job.Name),
                new XElement("Education", vacancy.Education.ToString()),
                new XElement("Experience", vacancy.Experience.Value),
                new XElement("Wages", vacancy.Wages.Value),
                new XElement("District", vacancy.District.ToString())
            );
        }

        private static Vacancy DeserializeVacancy(XElement element, Agency agency)
        {
            try
            {
                var job = new Job(element.Element("Job")?.Value ?? "");
                Enum.TryParse<Education>(element.Element("Education")?.Value, out var education);
                double.TryParse(element.Element("Experience")?.Value, out var experience);
                double.TryParse(element.Element("Wages")?.Value, out var wages);
                Enum.TryParse<District>(element.Element("District")?.Value, out var district);

                return new Vacancy(
                    job,
                    education,
                    new Experience(experience),
                    new Wages(wages),
                    district
                );
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}