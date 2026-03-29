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

using System.Windows.Navigation;


namespace WpfHobbies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;   //hide the Frame Navigation

        }

   

        private void Users_Selected(object sender, RoutedEventArgs e)
        {
            this.myFrame.Navigate(new Users());
        }

        private void Home_Selected(object sender, RoutedEventArgs e)
        {
            this.myFrame.Navigate(new Home());
        }

        private void Category_Selected(object sender, RoutedEventArgs e)
        {
            this.myFrame.Navigate(new Category());

        }

        private void Exit_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("؟ هل انت متاكد من اغلاق البرنامج","خروج من البرنامج", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            Application.Current.Shutdown();
        }

        private void Order_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
