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

namespace KinderMediaPlayer
{
    /// <summary>
    /// Interaktionslogik für WndAdmin.xaml
    /// </summary>
    public partial class WndAdmin : Window
    {
        public WndAdmin()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Check Password
            if (string.IsNullOrEmpty(txtPassword1.Password) == false)
            {
                if (string.IsNullOrEmpty(txtPassword2.Password) || txtPassword1.Password.Equals(txtPassword2.Password) == false)
                {
                    MessageBox.Show("Please make sure the passwords match", "Password", MessageBoxButton.OK);
                    return;
                }

                ServiceSettings.setPassword(txtPassword1.Password);
            }



            ServiceSettings.saveSettings();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
