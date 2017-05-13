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
    /// Interaction logic for DlgAddMediaElement.xaml
    /// </summary>
    public partial class DlgAddMediaElement : Window
    {
        public MediaElement resultElement;

        private string sourceFile = null;
        private string iconFile = null;

        public DlgAddMediaElement()
        {
            InitializeComponent();

            resultElement = new MediaElement();

            // Fill in the Combobox will all the available types
            cmbType.ItemsSource = MediaElement.MEDIA_TYPE_STRING;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // First, we check the valid input
            if (MediaElement.isMediaType(cmbType.SelectedIndex) == false)
            {
                MessageBox.Show("Please select a Media Type.", "Warning", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(sourceFile))
            {
                MessageBox.Show("Please select a Source file for the Media you wish to add.", "Warning", MessageBoxButton.OK);
                return;
            }

            // And then we return the value
            resultElement.Name = txtName.Text;
            resultElement.Description = txtDescription.Text;
            resultElement.Icon = iconFile;
            resultElement.Source = sourceFile;
            resultElement.Type = (MediaElement.MEDIA_TYPE)cmbType.SelectedIndex;

            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOpenIcon_Click(object sender, RoutedEventArgs e)
        {
            iconFile = ServiceWinInterop.openDialogImageFile();

            if (string.IsNullOrWhiteSpace(iconFile))
            {
                lblIconFile.Content = "No File Selected";
                iconFile = null;
            }
            else
            {
                lblIconFile.Content = iconFile;
            }
        }

        private void btnOpenSource_Click(object sender, RoutedEventArgs e)
        {
            sourceFile = ServiceWinInterop.openDialogSourceFile();

            if (string.IsNullOrWhiteSpace(sourceFile))
            {
                lblSourceFile.Content = "No File Selected";
                sourceFile = null;
            }
            else
            {
                lblSourceFile.Content = sourceFile;
            }
        }
    }
}
