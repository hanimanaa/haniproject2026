using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfHobbies.ServiceReference1;

using System.Text.RegularExpressions;

namespace WpfHobbies.SubUser
{
    /// <summary>
    /// Interaction logic for SubUserSearch.xaml
    /// </summary>
    
    public partial class SubUserSearch : Page
    {
        Service1Client srv = new Service1Client();
        User user = new User();
        public SubUserSearch()
        {
            InitializeComponent();
            // הוספת אפשרות בחר הכל
            CityList cities = srv.SelectAllCities();
            City city = new City();
            city.cityNum = -1;
            city.cityName = "-- בחר עיר --";
            cities.Insert(0, city);

            cityCB.ItemsSource = cities;
            cityCB.DisplayMemberPath = "cityName";
            cityCB.SelectedValuePath = "cityNum";
            cityCB.SelectedIndex = 0;
            
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            user.userEmail = emailBox.Text;        
            user.fName = fNameBox.Text;
            user.lName = lNameBox.Text;
            user.tel = telBox.Text;
            user.city = (City)cityCB.SelectedItem;

            SearchData(user);
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        // הקלדת מספרים בלבד !!
        private void OnlyNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // בודק אם התו המוקלד הוא ספרה 0-9
            Regex regex = new Regex("[^0-9]+");

            // אם התו הוא לא מספר, אנחנו מסמנים שהאירוע טופל (Handled) והתו לא יופיע
            e.Handled = regex.IsMatch(e.Text);
        }
        private void RefreshData()
        {
            // حتلنة بيانات الصفحة الرئيسيه من خلال ال Frame
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.myFrame.Content is Users usersPage)
            {
                usersPage.RefreshData();
            }
        }
        private void SearchData(User user)
        {
            // حتلنة بيانات الصفحة الرئيسيه من خلال ال Frame
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.myFrame.Content is Users usersPage)
            {
                usersPage.SearchData(user);
            }
        }
    }
}
