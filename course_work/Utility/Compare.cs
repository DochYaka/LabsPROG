// Compare.cs
using CourseWork.AgencyModels;
using CourseWork.Interfaces;
using CourseWork.WorkerDetails;

using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

namespace CourseWork.Utility
{
    public class Compare
    {
        public ObservableCollection<Worker> CompareVacancyWorkerList(IVacancy vacancy, ReadOnlyObservableCollection<Worker> workers)
        {
            var res = new ObservableCollection<Worker>();

            int points = 0;

            foreach (var worker in workers)
            {
                // Должность 0 - 1
                if (worker.Job.Name != vacancy.Job.Name) { 
                    continue; }

                if (worker.Job.Name == vacancy.Job.Name) {  points++; }

                // Образование 0 - 2
                if (worker.Education == Education.Высшее) { points += 2; }
                else
                {
                    if (worker.Education > vacancy.Education || worker.Education == vacancy.Education) { points += 2; }
                    else { points += 0; }
                }

                // Опыт работы
                if (worker.Experience.Value >= vacancy.Experience.Value) { points += 2; }
                else
                {
                    if (worker.Experience.Value == vacancy.Experience.Value) { points += 1; }
                    else { points += 0; }
                }

                // Район
                if (worker.District == vacancy.District) { points++; }
                else
                {
                    if (worker.District == District.Любой) { points++; }
                    else { points += 0; }
                }

                if (points >= 5) { res.Add(worker); points = 0; }
                else
                {
                    points = 0;
                    continue;
                }
            }
            return res;
        }
    }
}