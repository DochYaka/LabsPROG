// EmployerWindow.xaml.cs
using CourseWork.AgencyModels;
using CourseWork.EmployerModels;
using CourseWork.Interfaces;
using CourseWork.JobDetails;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

public partial class EmployerWindow : Window
{
    public Agency Agency { get; }

    public EmployersList EmployersList { get; }

    public Employer CurrentEmployer { get; set; }

    public Vacancy CurrentVacancy { get; set; }

    private VacancyDisplay selectedVacancy;

    public EmployerWindow(EmployersList employersLists, Agency agency)
    {
        InitializeComponent();
        EmployersList = employersLists;
        Agency = agency;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadAllData();
    }

    private void TextBox_Changed(object sender, RoutedEventArgs e)
    {
        var tb = sender as TextBox;
        if (tb == null) return;

        var hintName = tb.Tag as string;
        if (hintName == null) return;

        var hint = FindName(hintName) as TextBlock;
        if (hint == null) return;

        hint.Visibility =
            string.IsNullOrEmpty(tb.Text) && !tb.IsFocused
                ? Visibility.Visible
                : Visibility.Hidden;

        UpdateRegisterButtonState();
    }

    private void UpdateRegisterButtonState()
    {
        bool allFilled = true;

        foreach (var child in MainDockPanel.Children)
        {
            var panel = child as Panel;
            if (panel == null) continue;

            foreach (var element in panel.Children)
            {
                var tb = element as TextBox;
                if (tb == null) continue;

                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    allFilled = false;
                    break;
                }
            }
        }

        LoginButton.Background = allFilled
            ? Brushes.LightGreen
            : Brushes.Red;
    }

    private void AddVacancy_Click(object sender, RoutedEventArgs e)
        {
        try
        {
            if (CurrentEmployer == null)
            {
                MessageBox.Show("Сначала войдите как работодатель", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbJob.SelectedItem == null || 
                !(cbJob.SelectedItem is Job job))
            {
                MessageBox.Show("Выберите должность", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                cbJob.Focus();
                return;
            }

            if (cbEducation.SelectedItem == null ||
                !(cbEducation.SelectedItem is Education education))
            {
                MessageBox.Show("Выберите образование", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                cbEducation.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbExperience.Text))
            {
                MessageBox.Show("Введите опыт работы", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                tbExperience.Focus();
                return;
            }

            if (!double.TryParse(tbExperience.Text, out double experienceValue) ||
                experienceValue < 0)
            {
                MessageBox.Show("Опыт должен быть положительным числом", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                tbExperience.SelectAll();
                tbExperience.Focus();
                return;
            }

            var experience = new Experience(experienceValue);

            if (string.IsNullOrWhiteSpace(tbWages.Text))
            {
                MessageBox.Show("Введите зарплату", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                tbWages.Focus();
                return;
            }

            if (!double.TryParse(tbWages.Text, out double wagesValue) ||
                wagesValue <= 0)
            {
                MessageBox.Show("Зарплата должна быть положительным числом", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                tbWages.SelectAll();
                tbWages.Focus();
                return;
            }

            var wages = new Wages(wagesValue);

            if (cbDistrict.SelectedItem == null ||
                !(cbDistrict.SelectedItem is District district))
            {
                MessageBox.Show("Выберите район", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                cbDistrict.Focus();
                return;
            }

            var vacancy = Agency.employers.CreateVacancyFromForm(job, education, experience, wages, district);

            if (vacancy == null)
            {
                MessageBox.Show("Не удалось создать вакансию", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CurrentEmployer.AddVacancy(vacancy);

            UpdateListViewVacancies();

            ClearVacancyForm();
        }
        catch (FormatException)
        {
            MessageBox.Show("Некорректный формат числа в поле 'Опыт' или 'Зарплата'", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show($"Ошибка в данных: {ex.Message}", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка при добавлении вакансии: {ex}");

            MessageBox.Show($"Произошла ошибка: {ex.Message}\nПроверьте все поля", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ClearVacancyForm()
    {
        cbJob.SelectedItem = null;
        cbEducation.SelectedItem = null;
        cbDistrict.SelectedItem = null;
        tbExperience.Clear();
        tbWages.Clear();
    }

    private void lvVacancy_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedVacancy = lvVacancy.SelectedItem as VacancyDisplay;

        if (selectedVacancy != null)
        {
            CurrentVacancy = selectedVacancy.Vacancy;
            lvLoadRequests();
            UpdateListViewRequests();
        }
        else { CurrentVacancy = null; }
    }

    private void RemoveVacancy_Click(object sender, RoutedEventArgs e)
    {
        if (selectedVacancy == null) return;

        CurrentEmployer.RemoveVacancy(selectedVacancy.Vacancy);

        UpdateListViewVacancies();
    }

    private void lvLoadRequests()
    {
        if (selectedVacancy != null)
        {
            lvRequest.Visibility = Visibility.Visible;

            lvRequest.ItemsSource = CurrentVacancy.GetRequests();
        }
        else return;
    }

    private void lvRequests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var selectedRequest = lvRequest.SelectedItem as WorkerDisplay;

        var window = new WorkerDetailWindow(selectedRequest, selectedVacancy) { Owner = this };
        window.ShowDialog();

        if (window._worker.Worker.checkStatus())
        {
            CurrentEmployer.Employees.Add(selectedRequest.Worker);
            UpdateListViewRequests();
        }
        else if (window._worker.Worker.checkStatus())
        {
            CurrentVacancy.RemoveRequest(selectedRequest.Worker);
            UpdateListViewRequests();
        }
    }

    private void Login_Click(object sender, RoutedEventArgs e) => Login();

    private void Login()
    {
        var employer = Agency.TryLogin(tbNameEmployer.Text, tbFullNameHR.Text);

        if (employer != null)
        {
            CurrentEmployer = employer;
            MainDockPanel.Visibility = Visibility.Collapsed;
            lvDockPanel.Visibility = Visibility.Visible;
            UpdateListViewVacancies();

            tbProfile.Text = $"Профиль: {CurrentEmployer.NameEmployer}, {CurrentEmployer.Person.Name.FirstName} {CurrentEmployer.Person.Name.LastName}";
        }
        else MessageBox.Show("Неверные данные для входа");
    }


    private void RegistrWindow_Click(object sender, RoutedEventArgs e)
    {
        var w = new RegistrEmployerWindow(Agency) { Owner = this };
        w.ShowDialog();
    }

    // =================================== UPDATE ===================================
    private void UpdateListViewVacancies()
    {
        var vacs = CurrentEmployer.GetVacancies();

        var displayList = vacs
            .Select((vacancy) => new VacancyDisplay(vacancy))
            .ToList();

        lvVacancy.ItemsSource = displayList;
    }

    private void UpdateListViewRequests()
    {
        var reqs = CurrentVacancy.GetRequests();

        var displayList = reqs
            .Select((worker, index) => new WorkerDisplay(index + 1, worker))
            .ToList();

        lvRequest.ItemsSource = displayList;
    }


    // =================================== LOAD DATA ===================================
    private void LoadAllData()
    {
        cbEducation.ItemsSource = Enum.GetValues(typeof(Education));
        cbDistrict.ItemsSource = Enum.GetValues(typeof(District));
        cbJob.ItemsSource = Agency.jobDirectory.GetJobs();
    }
}

