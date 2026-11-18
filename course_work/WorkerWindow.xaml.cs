// WorkerWindow.xaml.cs
using CourseWork;
using CourseWork.AgencyModels;
using CourseWork.JobDetails;
using CourseWork.PersonServices;
using CourseWork.WorkerDetails;
using System;
using System.Diagnostics;
using System.Windows;

public partial class WorkerWindow : Window
{
    public Agency _agency;

    public WorkerWindow(Agency agency) 
    { 
        InitializeComponent();
        _agency = agency;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadAllData();
    }

    private void Close_Click(object sender, RoutedEventArgs e) { Close(); }

    private void AddWorker_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(tbLastName.Text) || 
                string.IsNullOrWhiteSpace(tbFirstName.Text) ||
                string.IsNullOrWhiteSpace(tbSurName.Text))
            {
                MessageBox.Show("ФИО не введено", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var name = new Name(tbLastName.Text, tbFirstName.Text, tbSurName.Text);

            var email = new Email(tbEmail.Text);

            var address = tbAddress.Text;

            var person = new Person(name, email, address);

            var sex = Gender.None;

            if (rbSexFemale.IsChecked == true) { sex = Gender.Female; }
            else { sex = Gender.Male; }

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

            if (cbDesiredDistrict.SelectedItem == null ||
                !(cbDesiredDistrict.SelectedItem is District district))
            {
                MessageBox.Show("Выберите район", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                cbDesiredDistrict.Focus();
                return;
            }

            var worker = _agency.workers.CreateWorkerFromForm(person, sex, address, experience, education, job, wages, district);

            if (worker == null)
            {
                MessageBox.Show("Не удалось создать вакансию", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _agency.workers.AddWorker(worker);
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


    // =================================== LOAD DATA ===================================
    private void LoadAllData()
    {
        cbEducation.ItemsSource = Enum.GetValues(typeof(Education));
        cbDesiredDistrict.ItemsSource = Enum.GetValues(typeof(District));
        cbJob.ItemsSource = _agency.jobDirectory.GetJobs();
    }
}

