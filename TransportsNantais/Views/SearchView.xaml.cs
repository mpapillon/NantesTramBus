using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using TransportsNantais.ViewModels;
using TransportsNantais.Bases;

namespace TransportsNantais.Views
{
    public partial class SearchView : ApplicationPageBase
    {
        // Constructor
        public SearchView()
        {
            InitializeComponent();
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            this.SearchBox.Focus();
        }

        private void SearchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Focus(); // dismiss the keyboard
                
                var vm = (SearchViewModel)this.DataContext;
                var tbx = (PhoneTextBox)sender;

                vm.SearchCommand.Execute(tbx.Text);
            }
        }

        private void SearchBox_ActionIconTapped(object sender, EventArgs e)
        {
            this.Focus(); // dismiss the keyboard

            var vm = (SearchViewModel)this.DataContext;
            var tbx = (PhoneTextBox)sender;

            vm.SearchCommand.Execute(tbx.Text);
        }
    }
}