﻿using System;
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
    /// Interaction logic for HelpForm.xaml
    /// </summary>
    public partial class HelpForm : Page
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void mainMenuBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("MainMenu.xaml", UriKind.RelativeOrAbsolute));

        }
    }
}
