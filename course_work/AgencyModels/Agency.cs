// Agency.cs
using CourseWork.EmployerModels;
using CourseWork.Interfaces;
using CourseWork.JobDetails;
using CourseWork.Utility;
using CourseWork.WorkerDetails;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.AgencyModels
{
    public class Agency
    {
        public WorkersList workers { get; } = new WorkersList();

        public EmployersList employers { get; } = new EmployersList();

        public JobDirectory jobDirectory { get; } = new JobDirectory();

        private const string DataFilePath = "agency_data.xml";

        public Agency() { LoadData(); }

        public void SaveData()
        {
            try
            {
                XmlSerializerHelper.SaveToXml(DataFilePath, this);
                Console.WriteLine($"Данные сохранены в {DataFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        public void LoadData()
        {
            try
            {
                XmlSerializerHelper.LoadFromXml(DataFilePath, this);
                Console.WriteLine($"Данные загружены из {DataFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
            }
        }

        public bool AutoSelect(Vacancy vacancy)
        {
            Compare compareService = new Compare();
            var res = compareService.CompareVacancyWorkerList(vacancy, workers.workers);

            if (res.Count != 0)
            {
                vacancy.AddToCollectionRequest(res);
                SaveData();
                return true;
            }
            return false;
        }

        public void SendWorkerToEmployer(Worker worker, Vacancy vacancy)
        {
            if (worker == null || vacancy == null)
            {
                MessageBox.Show("Выберите кандидата И вакансию!");
                return;
            }
            else
            {
                CreateReport(worker, vacancy);
            }
        }

        public void CreateReport(Worker worker, Vacancy vacancy)
        {
            worker.SetStatus(Status.На_Вакансии);
            vacancy.AddRequest(worker);
            SaveData();
        }

        public EmployersList LoadDataEmployers() { return employers; }

        public Employer TryLogin(string companyName, string hrFullName)
        {
            if (string.IsNullOrWhiteSpace(companyName) ||
                string.IsNullOrWhiteSpace(hrFullName))
                return null;

            return employers.employers
                .FirstOrDefault(e => IsEmployerMatch(e, companyName, hrFullName));
        }

        private bool IsEmployerMatch(Employer employer, string companyName, string hrFullName)
        {
            if (!employer.NameEmployer.Equals(companyName.Trim(), StringComparison.OrdinalIgnoreCase))
                return false;

            var expectedFullName = $"{employer.Person.Name.FirstName} {employer.Person.Name.LastName}";
            return expectedFullName.Equals(hrFullName.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}

