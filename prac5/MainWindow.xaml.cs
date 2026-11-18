using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Практика5
{
    public partial class MainWindow : Window
    {
        //private string xmlPath = "dates.xml";
        private string xmlPath = "C:\\Users\\dmitr\\source\\repos\\Практика5\\Практика5\\dates.xml";

        public MainWindow()
        {
            InitializeComponent();
            LoadXml();
        }

        private void LoadXml()
        {
            if (!System.IO.File.Exists(xmlPath))
                new XDocument(new XElement("Dates")).Save(xmlPath);

            XDocument doc = XDocument.Load(xmlPath);

            var dates = doc.Descendants("Date")
                           .Select(u => new
                           {
                               Day = (int)u.Element("Day"),
                               Month = (string)u.Element("Month"),
                               Year = (int)u.Element("Year")
                           }).ToList();

            DatesList.ItemsSource = dates;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            XDocument doc = XDocument.Load(xmlPath);

            var newDate = new XElement("Date",
                new XElement("Day", DayBox.Text),
                new XElement("Month", MonthBox.Text),
                new XElement("Year", YearBox.Text)
            );

            doc.Root.Add(newDate);
            doc.Save(xmlPath);

            LoadXml();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchBox.Text.ToLower();
            XDocument doc = XDocument.Load(xmlPath);

            var filtered = doc.Descendants("Date")
                              .Where(u => ((string)u.Element("Month"))
                                            .ToLower().Contains(search))
                              .Select(u => new
                              {
                                  Day = (int)u.Element("Day"),
                                  Month = (string)u.Element("Month"),
                                  Year = (int)u.Element("Year")
                              }).ToList();

            DatesList.ItemsSource = filtered;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            LoadXml();
            SearchBox.Text = "";
        }
    }
}
