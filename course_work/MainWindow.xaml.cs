// MainWindow.xaml.cs
using CourseWork.AgencyModels;
using CourseWork.EmployerModels;

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CourseWork
{
    public partial class MainWindow : Window
    {
        public Agency agency = new Agency();

        public ObservableCollection<WorkerDisplay> workersDisplay = new ObservableCollection<WorkerDisplay>();

        private GridViewColumnHeader lastHeader;
        private ListSortDirection dir = ListSortDirection.Ascending;

        private Employer CurrentEmployer;

        private Vacancy CurrentVacancy;

        public MainWindow() { InitializeComponent(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateListViewWorkers();
            LoadDataEmployers();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                agency.SaveData();
                MessageBox.Show("Данные успешно сохранены!", "Сохранение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            agency.SaveData();
            MessageBox.Show("Данные сохранены!", "Сохранение",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            agency.LoadData();
            UpdateListViewWorkers();
            LoadDataEmployers();
            MessageBox.Show("Данные загружены!", "Загрузка",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AutoSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedVacancy = lvVacancy.SelectedItem as VacancyDisplay;

                if (selectedVacancy == null)
                {
                    MessageBox.Show("Вакансия не выбрана");
                    return;
                }

                CurrentVacancy = selectedVacancy.Vacancy;

                if (agency.AutoSelect(CurrentVacancy) == false)
                {
                    MessageBox.Show("На вакансию нет кандидатов");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void SendWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedWorker = lvWorkers.SelectedItem as WorkerDisplay;

                var selectedVacancy = lvVacancy.SelectedItem as VacancyDisplay;

                if (selectedWorker == null || selectedVacancy == null)
                {
                    MessageBox.Show("Выберите кандидата И вакансию!");
                    return;
                }

                CurrentVacancy = selectedVacancy.Vacancy;

                agency.CreateReport(selectedWorker.Worker, selectedVacancy.Vacancy);

                UpdateListViewWorkers();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
     
        private void cbEmployers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbVacancy.Visibility = Visibility.Visible;
            lvVacancy.Visibility = Visibility.Visible;

            CurrentEmployer = cbEmployers.SelectedItem as Employer;
            if (CurrentEmployer == null) return;

            lvVacancy.ItemsSource = CurrentEmployer.GetVacancies();

            UpdateListViewVacancies();
        }


        // =================================== UTILITY ===================================
        private void Header_Click(object sender, RoutedEventArgs e)
        {
            var header = e.OriginalSource as GridViewColumnHeader;
            if (header == null || header.Tag == null) return;

            if (lastHeader == header)
                dir = dir == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            else
                dir = ListSortDirection.Ascending;

            var view = CollectionViewSource.GetDefaultView(lvWorkers.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(header.Tag.ToString(), dir));

            UpdateArrow(header);
            lastHeader = header;
        }

        private void UpdateArrow(GridViewColumnHeader header)
        {
            HideAllArrows();

            var panel = header.Content as StackPanel;
            var arrow = panel.Children.OfType<Path>().FirstOrDefault();
            if (arrow == null) return;

            arrow.Visibility = Visibility.Visible;
            arrow.RenderTransform = new RotateTransform(
                dir == ListSortDirection.Ascending ? 0 : 180,
                4, 2
            );
        }

        private void HideAllArrows()
        {
            foreach (var path in FindVisualChildren<Path>(lvWorkers))
                path.Visibility = Visibility.Collapsed;
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t)
                    yield return t;

                foreach (var sub in FindVisualChildren<T>(child))
                    yield return sub;
            }
        }


        // =================================== CREATE WINDOWS ===================================
        private void OpenEmployerWindow_Click(object sender, RoutedEventArgs e)
        {
            var w = new EmployerWindow(agency.employers, agency) {  Owner = this };
            w.Show();
        }

        private void OpenWorkerWindow_Click(object sender, RoutedEventArgs e)
        {
            var w = new WorkerWindow(agency) { Owner = this };
            w.ShowDialog();
        }

        private void LoadDataEmployers()
        {
            cbEmployers.ItemsSource = agency.LoadDataEmployers().employers;
            cbEmployers.DisplayMemberPath = "NameHRPlusEmployer";
        }


        // =================================== UPDATE ===================================
        private void ResetSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var view = CollectionViewSource.GetDefaultView(lvWorkers.ItemsSource);
                view.SortDescriptions.Clear();

                lastHeader = null;
                dir = ListSortDirection.Ascending;

                HideAllArrows();

                UpdateListViewWorkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        public void UpdateListViewWorkers()
        {
            var workersList = agency.workers.workers;

            var displayList = workersList
                .Select((worker, index) => new WorkerDisplay(index + 1, worker))
                .ToList();

            lvWorkers.ItemsSource = displayList;

            tbStatus.Text = "Обновились работники";
        }

        private void UpdateListViewVacancies()
        { 
            var vacs = CurrentEmployer.GetVacancies();

            foreach (Vacancy vac in vacs)
            {
                var displayList = vacs
                    .Select((vacancy) => new VacancyDisplay(vacancy))
                    .ToList();

                lvVacancy.ItemsSource = displayList;
            }
        }

    }
}