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
    /// Interaktionslogik für UCMediaElement.xaml
    /// </summary>
    public partial class UCMediaElement : UserControl
    {
        private MediaElement mediaElement;
        private Action<MediaElement> callbackOpen;
        
        public UCMediaElement()
        {
            InitializeComponent();

            mediaElement = null;
        }

        public UCMediaElement(MediaElement inElement, Action<MediaElement> inCallback) : this()
        {            
            setMediaElement(inElement);

            callbackOpen = inCallback;
        }

        public void setMediaElement(MediaElement inElement)
        {
            if (inElement == null)
            {
                return;
            }

            mediaElement = inElement;

            lblName.Content = mediaElement.Name;
            lblDescription.Content = mediaElement.Description;
            

            if (!string.IsNullOrEmpty(mediaElement.Icon))
            {
                try
                {
                    // TODO: Create an Image inside Content and set the URI from that
                    //BitmapImage iconImage = new BitmapImage(new Uri(mediaElement.Icon, UriKind.Relative));
                    //Image iconControl = new Image();
                    //iconControl.Source = iconImage;

                    //StackPanel iconStack = new StackPanel();
                    //iconStack.Orientation = Orientation.Horizontal;
                    //iconStack.Margin = new Thickness(0);
                    //iconStack.Children.Add(iconControl);

                    //btnOpen.Content = iconImage;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not open the Background image. Using white instead.", "Error", MessageBoxButton.OK);
                }
            }          


            switch (mediaElement.Type)
            {
                case MediaElement.MEDIA_TYPE.IMAGE:

                    break;

                case MediaElement.MEDIA_TYPE.SOUND:

                    break;

                case MediaElement.MEDIA_TYPE.VIDEO:

                    break;
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (callbackOpen != null)
            {
                callbackOpen(mediaElement);
            }
        }
    }
}
