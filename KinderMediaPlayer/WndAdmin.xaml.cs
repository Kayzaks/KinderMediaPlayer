using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region VIEWMODEL_INLINE

        class WndAdminViewModel : Notifier
        {
            private MediaElement selectedElement;
            public MediaElement SelectedElement
            {
                get
                {
                    return selectedElement;
                }
                set
                {
                    SetField(ref selectedElement, value, "SelectedElement");
                }
            }

            private ObservableCollection<MediaElement> mediaElements;
            public ObservableCollection<MediaElement> MediaElements
            {
                get
                {
                    return mediaElements;
                }
                set
                {
                    SetField(ref mediaElements, value, "MediaElements");
                }
            }

            string backgroundImage;
            public string BackgroundImage
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(backgroundImage))
                    {
                        return "No File selected";
                    }
                    return backgroundImage;
                }
                set
                {
                    SetField(ref backgroundImage, value, "BackgroundImage");
                }
            }

            public void removeSelected()
            {
                if (SelectedElement != null)
                {
                    MediaElements.Remove(SelectedElement);
                    OnPropertyChanged("MediaElements");
                }
            }

            public void addElement(MediaElement inElement)
            {
                if (inElement != null)
                {
                    MediaElements.Add(inElement);
                    OnPropertyChanged("MediaElements");
                }
            }
        }

        #endregion VIEWMODEL_INLINE
        
        private WndAdminViewModel dataContext;

        public WndAdmin()
        {
            InitializeComponent();

            dataContext = new WndAdminViewModel();
            this.DataContext = dataContext;

            // Background
            dataContext.BackgroundImage = ServiceSettings.getBackgroundSource();

            // Media Elements
            dataContext.MediaElements = ServiceSettings.getCopyMediaElements();
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

            // Copy back Media Elements

            if (dataContext.MediaElements != null)
            {
                ServiceSettings.setMediaElements(new ObservableCollection<MediaElement>(dataContext.MediaElements));
            }

            // And save

            ServiceSettings.saveSettings();

            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnOpenBackground_Click(object sender, RoutedEventArgs e)
        {
            dataContext.BackgroundImage = ServiceWinInterop.openDialogImageFile();
        }

        private void btnAddElement_Click(object sender, RoutedEventArgs e)
        {
            DlgAddMediaElement dlgAddElement = new DlgAddMediaElement();

            if (dlgAddElement.ShowDialog() == true)
            {
                dataContext.addElement(dlgAddElement.resultElement);
            }
        }

        private void btnRemoveElement_Click(object sender, RoutedEventArgs e)
        {
            dataContext.removeSelected();
        }
    }
}
