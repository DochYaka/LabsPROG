// RegistrEmployerWindow.xaml.cs
using CourseWork.AgencyModels;
using CourseWork.EmployerModels;
using CourseWork.PersonServices;

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class RegistrEmployerWindow : Window
{
    public Agency _agency;

    public RegistrEmployerWindow(Agency agency)
    {
        InitializeComponent();
        _agency = agency;
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

        btnRegistr.Background = allFilled
            ? Brushes.LightGreen
            : Brushes.Red;
    }

    private void btnRegistr_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(tbNameEmployer.Text))
            {
                MessageBox.Show("Введите название компании", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                tbNameEmployer.Focus();
                return;
            }

            string nameEmployer = tbNameEmployer.Text;

            if (string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                MessageBox.Show("Введите адресс", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                tbAddress.Focus();
                return;
            }

            string address = tbAddress.Text;

            if (string.IsNullOrWhiteSpace(tbFullNameHR.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                tbFullNameHR.Focus();
                return;
            }

            var parts = tbFullNameHR.Text
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string lastName = parts.Length > 0 ? parts[0] : "";
            string firstName = parts.Length > 1 ? parts[1] : "";
            string surName = parts.Length > 2 ? parts[2] : "";

            var name = new Name(lastName, firstName, surName);

            string phoneNumber = tbPhoneNumber.Text.Trim();

            var email = new Email(tbEmail.Text.Trim());

            var person = new Person(name, email, phoneNumber);

            var emp = _agency.employers.CreateEmployerFromForm(nameEmployer, address, person);

            if (!string.IsNullOrWhiteSpace(tbNameEmployer.Text) && !string.IsNullOrWhiteSpace(tbAddress.Text) &&
                !string.IsNullOrWhiteSpace(tbFullNameHR.Text) && !string.IsNullOrWhiteSpace(tbPhoneNumber.Text) &&
                !string.IsNullOrWhiteSpace(tbEmail.Text)) 
            {
                _agency.employers.AddEmployer(emp);

                this.Close(); 
            }
            else { MessageBox.Show("Заполните все поля!"); }
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show($"Ошибка в данных: {ex.Message}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка при добавлении работадателя: {ex}");

            MessageBox.Show($"Произошла ошибка: {ex.Message}\nПроверьте все поля", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
