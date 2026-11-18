using CourseWork.AgencyModels;
using CourseWork.EmployerModels;

using System.Windows;

public partial class WorkerDetailWindow : Window
{
    // =================================== GENERAL ===================================
    public string Comment { get; set; }
    public string CandidateInfo { get; }
    public string VacancyInfo { get; }

    public WorkerDisplay _worker;

    public WorkerDetailWindow(WorkerDisplay worker, VacancyDisplay vacancy)
    {
        InitializeComponent();

        CandidateInfo = $"Кандидат: {worker.Worker.Person.Name.LastName}";
        VacancyInfo = $"Вакансия: {vacancy.Vacancy.Job}";

        DataContext = this;

        _worker = worker;
    }

    private void Accept_Click(object sender, RoutedEventArgs e)
    {
        _worker.Worker.SetStatus(Status.Устроенный);
        DialogResult = true;
        Close();
    }

    private void Reject_Click(object sender, RoutedEventArgs e)
    {
        _worker.Worker.SetStatus(Status.Удалённый);
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}


