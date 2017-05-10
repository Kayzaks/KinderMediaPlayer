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

namespace KinderMediaPlayer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class WndMain : Window
    {
        public WndMain()
        {
            // Disable all special keys to prevent the Window from being closed / hidden. 
            // While it won't work 100%, it probably is the only alternative
            // to writing a custom shell
            //
            // NOTE 1: We ignore the on-screen keyboard for now
            // NOTE 2: Does not work with Multiple Screens
            ServiceWinInterop.disableSpecialKeys();

            // Load the Settings from settings.xml
            ServiceSettings.loadSettings();

            InitializeComponent();

            refreshApp();
        }

        #region MEDIA_ELEMENTS
        
        private void populateElements()
        {
            // Mainly called from refreshApp()
            if (ServiceSettings.getMediaElements() == null)
            {
                return;
            }

            pnlElements.Children.Clear();

            foreach (MediaElement currentElement in ServiceSettings.getMediaElements())
            {
                pnlElements.Children.Add(new UCMediaElement(currentElement, openMediaElement));
            }
        }

        private void openMediaElement(MediaElement inElement)
        {
            MessageBox.Show("Test");
        }
        
        #endregion MEDIA_ELEMENTS

        #region ADMINISTRATION

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            dlgPassword cDlg = new dlgPassword();

            // We temporarily allow Keyboard input.
            //
            // NOTE: This WILL enable the Win-Key in the Password
            // Box. At the moment, this is intentional to give
            // a way to close the Player when someone forgot the
            // password. To avoid this, the windows key must be trapped
            // individually from the captureKey() function

            ServiceWinInterop.disableHook();

            if (cDlg.ShowDialog() == true && ServiceSettings.checkPassword(cDlg.resultPassword) == true)
            {
                // A correct password was entered!
                WndAdmin cAdmin = new WndAdmin();
                cAdmin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong Password", "Password", MessageBoxButton.OK);
            }

            ServiceWinInterop.enableHook();

            refreshApp();
        }


        private void refreshApp()
        {
            // Re-Applies any changes made to the App in general
            // Like the background:
            if (!string.IsNullOrEmpty(ServiceSettings.getBackgroundSource()))
            {
                try
                {
                    this.Background = new ImageBrush(new BitmapImage(new Uri(ServiceSettings.getBackgroundSource(), UriKind.Relative)));
                }
                catch(Exception e)
                {
                    MessageBox.Show("Could not open the Background image. Using white instead.", "Error", MessageBoxButton.OK);
                }
            }

            // And all the individual Media Elements:
            populateElements();
        }

        #endregion ADMINISTRATION
    }
}
