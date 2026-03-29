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
    /// Interaction logic for SubUserHome.xaml
    /// </summary>
    public partial class SubUserHome : Page
    {
        Service1Client srv = new Service1Client();

        public SubUserHome()
        {
            InitializeComponent();

          
        } 
    }
}
