// WorkersList.cs
using CourseWork.JobDetails;
using CourseWork.PersonServices;
using CourseWork.WorkerDetails;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;

namespace CourseWork.AgencyModels
{
    public class WorkersList
    {
        private readonly ObservableCollection<Worker> _workers;
       
        private Agency _agency;

        public ReadOnlyObservableCollection<Worker> workers { get; }

        public WorkersList(Agency agency = null)
        {
            _workers = new ObservableCollection<Worker>();
            workers = new ReadOnlyObservableCollection<Worker>(_workers);
            _agency = agency;
        }

        public void AddWorker(Worker worker)
        {
            if (worker == null) { return; }

            if (_workers.Any(w => w.Person.Email.Address == worker.Person.Email.Address))
                throw new InvalidOperationException("Работник с таким email уже существует");

            _workers.Add(worker);
            SaveToAgency();
        }

        public Worker CreateWorkerFromForm(Person person, Gender sex, string address, 
            Experience experience, Education education, Job job, Wages wages, District district)
        {
            return new Worker
            (
                person: person,
                sex: sex,
                address: address,
                experience: experience,
                education: education,
                job: job,
                wages: wages,
                district: district
            );
        }

        public void RemoveWorker(Worker worker)
        {
            if (worker == null) { return; }

            _workers.Remove(worker);
            SaveToAgency();
        }

        private void SaveToAgency() { _agency?.SaveData(); }

        public List<Worker> Sort()
        {
            return workers
                .OrderBy(w => w.Person.Name.LastName)
                .ThenBy(w => w.Person.Name.FirstName)
                .ToList();
        }
    }
}