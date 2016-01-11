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

namespace Sunrise.MultipleChoice
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void questionaireBt_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("QuestionaireForm.xaml", UriKind.RelativeOrAbsolute));
        }

        private void questionBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("QuestionForm.xaml", UriKind.RelativeOrAbsolute));
        }

        private void helpBt_Click(object sender, RoutedEventArgs e)
        {
           // NavigationService nav = NavigationService.GetNavigationService(this);
           // nav.Navigate(new Uri("QuestionForm.xaml", UriKind.RelativeOrAbsolute));
        }

   
        private void aboutBt_Click(object sender, RoutedEventArgs e)
        {
           // NavigationService nav = NavigationService.GetNavigationService(this);
           // nav.Navigate(new Uri("QuestionForm.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
