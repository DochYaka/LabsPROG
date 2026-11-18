// Vacancy.cs
using CourseWork.WorkerDetails;

using System;
using System.Collections.ObjectModel;

namespace CourseWork.EmployerModels
{
    using CourseWork.JobDetails;
    using CourseWork.Interfaces;

    public class Vacancy : IVacancy
    {
        private const double MinWagesValue = 30000.0;
        private const double MaxWagesValue = 999999.0;

        private const double MinExperienceValue = 0.0;
        private const double MaxExperienceValue = 50.0;

        public Job Job { get; private set; }
        public Education Education { get; private set; }
        public Experience Experience { get; private set; }
        public Wages Wages { get; private set; }
        public District District { get; private set; }

        private ObservableCollection<Worker> Requests { get; }
        = new ObservableCollection<Worker>();

        public Vacancy(Job job, Education education, Experience experience, Wages wages, District district)
        {
            Job = job ?? throw new ArgumentNullException(nameof(job), "Должность не выбрана!");

            //if (education == null) 
            //    throw new ArgumentNullException(nameof(education), "Образование не выбрано!");

            if (experience.Value < MinExperienceValue || experience.Value > MaxExperienceValue)
                throw new ArgumentException("Опыт должен быть больше 0", nameof(experience));

            if (wages.Value < MinWagesValue || wages.Value > MaxWagesValue)
                throw new ArgumentException("Зарплата должена быть больше 0", nameof(wages));

            //if (district == null) 
            //    throw new ArgumentNullException(nameof(district), "Район не выбран");

            Education = education;
            Experience = experience;
            Wages = wages;
            District = district;
        }

        public void AddRequest(Worker worker)
        {
            if (worker == null)
                throw new ArgumentNullException(nameof(worker));

            Requests.Add(worker);
        }

        public void RemoveRequest(Worker worker)
        {
            if (worker == null) { throw new ArgumentNullException(nameof(worker)); }

            Requests.Remove(worker);
        }

        public ObservableCollection<Worker> GetRequests() => Requests;

        public void AddToCollectionRequest(ObservableCollection<Worker> workers)
        {
            if (workers == null)
            {
                throw new ArgumentNullException(nameof(workers));
            }

            foreach (Worker worker in workers)
            {
                AddRequest(worker);
            }
        }

    }
}