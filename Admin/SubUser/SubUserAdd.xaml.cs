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
    /// Interaction logic for SubUserAdd.xaml
    /// </summary>
    public partial class SubUserAdd : Page
    {
        Service1Client srv = new Service1Client();

        public SubUserAdd()
        {
            InitializeComponent();
            cityCB.ItemsSource = srv.SelectAllCities();
            cityCB.DisplayMemberPath = "cityName";
            cityCB.SelectedValuePath = "cityNum"; ;
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.userEmail = emailBox.Text;
            user.userPassword = passBox.Password;
            user.fName = fNameBox.Text;
            user.lName = lNameBox.Text;
            user.birthday = birthdayDP.SelectedDate.Value;

            if (genderMaleRB.IsChecked == true)
                user.gender = "זכר";
            else
                user.gender = "נקבה";

            user.tel = telBox.Text;
            user.city = (City)cityCB.SelectedItem;

            if (messageCB.IsChecked == true)
                user.message = true;
            else
                user.message = false;

            if (srv.AddUser(user) > 0)
                MessageBox.Show("The User : " + user.userEmail + " is Add !!");
            else
                MessageBox.Show("Not ADD !! ");
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            // حتلنة بيانات الصفحة الرئيسيه من خلال ال Frame
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow.myFrame.Content is Users usersPage)
            {
                usersPage.RefreshData();
            }
        }
    }
}
