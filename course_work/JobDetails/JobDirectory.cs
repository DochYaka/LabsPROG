// JobDirectory.cs
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseWork.JobDetails
{
    public class JobDirectory
    {
        private readonly ObservableCollection<Job> _jobs = new ObservableCollection<Job>();

        public JobDirectory()
        {
            _jobs.Add(new Job("Программист"));
            _jobs.Add(new Job("Врач"));
            _jobs.Add(new Job("Учитель"));
            _jobs.Add(new Job("Инженер"));
            _jobs.Add(new Job("Дизайнер"));
            _jobs.Add(new Job("Архитектор"));
            _jobs.Add(new Job("Повар"));
            _jobs.Add(new Job("Юрист"));
            _jobs.Add(new Job("Маркетолог"));
            _jobs.Add(new Job("Психолог"));
            _jobs.Add(new Job("Электрик"));
            _jobs.Add(new Job("Строитель"));
            _jobs.Add(new Job("Бухгалтер"));
            _jobs.Add(new Job("Журналист"));
            _jobs.Add(new Job("Фотограф"));
            _jobs.Add(new Job("Менеджер"));
            _jobs.Add(new Job("Автомеханик"));
            _jobs.Add(new Job("Логист"));
            _jobs.Add(new Job("Пилот"));
            _jobs.Add(new Job("Водитель"));
        }

        public IReadOnlyList<Job> GetJobs() { return _jobs; }
    }
}