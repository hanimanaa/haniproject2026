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

namespace WpfHobbies
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        Service1Client srv = new Service1Client();
        public Users()
        {
            InitializeComponent();
            UsersDG.ItemsSource = srv.SelectAllUsers();

        }

        private void UsersDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = new User();
            user = (User)UsersDG.SelectedItem;
            this.SubFrame.Navigate(new SubUser.SubUserUpdateDelete(user));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
           this.SubFrame.Navigate(new SubUser.SubUserAdd());
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            this.SubFrame.Navigate(new SubUser.SubUserSearch());
        }

        private void aBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.SubFrame.Navigate(new SubUser.SubUser3());
        }

        // دالة تحدث بيانات الصفحة
        public void RefreshData()
        {
            UsersDG.ItemsSource = srv.SelectAllUsers();
        }
        // دالة تحدث بيانات الصفحة
        public void SearchData(User user)
        {
            UsersDG.ItemsSource = srv.SearchUsers(user);
        }
    }
}
