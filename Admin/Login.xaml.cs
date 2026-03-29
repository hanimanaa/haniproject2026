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
using System.Windows.Shapes;
using WpfHobbies.ServiceReference1;

namespace WpfHobbies
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text;
            string pass = PassBox.Password;

            Service1Client srv = new Service1Client();
            
            if (srv.UserExist(email))
            {
                // כניסה לאפלקציה
                MainWindow m = new MainWindow();
                m.Show();
                this.Hide();
            }
            else
            {
                // נתונים לא נכונים
                MessageBox.Show("נתונים לא נכונים");
            }


        }

        private void hint_Click(object sender, RoutedEventArgs e)
        {
            EmailBox.Text = "hani@gmail.com";
            PassBox.Password = "111";
        }
    }
}
