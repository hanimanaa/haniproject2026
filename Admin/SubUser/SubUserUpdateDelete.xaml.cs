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
    /// Interaction logic for SubUserUpdateDelete.xaml
    /// </summary>
    public partial class SubUserUpdateDelete : Page
    {
        Service1Client srv = new Service1Client();
        public SubUserUpdateDelete() { }
        public SubUserUpdateDelete(User user)
        {
            InitializeComponent();
            cityCB.ItemsSource = srv.SelectAllCities();
            cityCB.DisplayMemberPath = "cityName";
            cityCB.SelectedValuePath = "cityNum"; ;

            emailBox.Text = user.userEmail;
            passBox.Password = user.userPassword;
            fNameBox.Text = user.fName;
            lNameBox.Text = user.lName;
            birthdayDP.SelectedDate = user.birthday;
            if (user.gender == "זכר")
            {
                genderMaleRB.IsChecked = true;
                genderFMaleRB.IsChecked = false;
            }               
            else
            {
                genderMaleRB.IsChecked = false;
                genderFMaleRB.IsChecked = true;
            }
            telBox.Text = user.tel;
            cityCB.SelectedValue = user.city.cityNum;
          
            if (user.message == true)           
                messageCB.IsChecked = true;           
            else
                messageCB.IsChecked = false;

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(emailBox.Text) || String.IsNullOrWhiteSpace(passBox.Password) || String.IsNullOrWhiteSpace(fNameBox.Text) || String.IsNullOrWhiteSpace(lNameBox.Text) || String.IsNullOrWhiteSpace(telBox.Text))
            {
                MessageBox.Show("נא למלא את כל השדות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Validation.IsValidEmail(emailBox.Text))
            {
                MessageBox.Show("דואר אלקטרוני לא תקין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Validation.IsValidPhone(telBox.Text))
            {
                MessageBox.Show("מספר טלפון לא תקין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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

            if (srv.UpdateUser(user) > 0)
            {
                MessageBox.Show("The User : " + user.userEmail + " is Update !!");
                RefreshData();
            }          
            else
                MessageBox.Show("Not update !! ");
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string userEmail = emailBox.Text;
            // فحص اذا كان المستخدم موجود
            if (!srv.UserExist(userEmail))
            {
                //مستخدم غير موجودة
                MessageBox.Show("משתמש לא קיים !");
            }
            else
            {
                if (srv.DeleteUserByEmail(userEmail) > 0)
                {
                    // تمت الحذف بنجاح
                    MessageBox.Show("המשתמש נמחק בהצלחה !");
                }
                else
                {
                    // هنالك مشكلة
                    MessageBox.Show("תקלה !!");
                }
            }
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
    }
}
