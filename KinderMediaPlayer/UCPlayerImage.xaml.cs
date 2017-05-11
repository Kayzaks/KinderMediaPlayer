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
    /// Interaktionslogik für UCPlayerImage.xaml
    /// </summary>
    public partial class UCPlayerImage : UserControl, IMediaPlayer
    {
        public UCPlayerImage()
        {
            InitializeComponent();
        }

        public void playMedia(string inSource)
        {
            try
            {
                imgDisplay.Source = new BitmapImage(new Uri(inSource));
            }
            catch (Exception e)
            {
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void UCPlayerImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
