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
    /// Interaktionslogik für DlgPassword.xaml
    /// </summary>
    public partial class DlgPassword : Window
    {
        public string resultPassword = null;

        public DlgPassword()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            resultPassword = txtPassword.Password;
            DialogResult = true;
        }

        private void txtPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOk_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
