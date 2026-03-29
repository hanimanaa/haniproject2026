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

namespace WpfHobbies.SubUser
{
    /// <summary>
    /// Interaction logic for SubUserSearch.xaml
    /// </summary>
    
    public partial class SubUserSearch : Page
    {
        Service1Client srv = new Service1Client();
        public SubUserSearch()
        {
            InitializeComponent();
            cityCB.ItemsSource = srv.SelectAllCities();
            cityCB.DisplayMemberPath = "cityName";
            cityCB.SelectedValuePath = "cityNum"; ;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.userEmail = emailBox.Text;           
            user.fName = fNameBox.Text;
            user.lName = lNameBox.Text;
            user.tel = telBox.Text;
            user.city = (City)cityCB.SelectedItem;    

            // حتلنة بيانات الصفحة الرئيسيه من خلال ال Frame
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.myFrame.Content is Users usersPage)
            {
                usersPage.SearchData(user);
            }
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
