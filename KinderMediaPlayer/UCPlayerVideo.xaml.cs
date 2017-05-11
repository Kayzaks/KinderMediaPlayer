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
using System.Windows.Threading;

namespace KinderMediaPlayer
{
    /// <summary>
    /// Interaktionslogik für UCPlayerVideo.xaml
    /// </summary>
    public partial class UCPlayerVideo : UserControl, IMediaPlayer
    {
        DispatcherTimer videoTimer;

        public UCPlayerVideo()
        {
            InitializeComponent();

            videoTimer = new DispatcherTimer();
            videoTimer.Interval = TimeSpan.FromSeconds(1);
            videoTimer.Tick += videoTimerTick;
        }

        private void videoTimerTick(object sender, EventArgs e)
        {
            if (vidDisplay.Source != null)
            {
                if (vidDisplay.NaturalDuration.HasTimeSpan)
                {
                    if (vidDisplay.Position >= vidDisplay.NaturalDuration.TimeSpan)
                    {
                        closePlayer();
                    }
                }
            }
        }

        public void playMedia(string inSource)
        {
            try
            {
                vidDisplay.Source = new Uri(inSource);
                vidDisplay.Play();
                videoTimer.Start();
            }
            catch (Exception e)
            {
                closePlayer();
            }
        }

        private void UCPlayerVideo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            closePlayer();
        }

        private void closePlayer()
        {
            vidDisplay.Stop();
            videoTimer.Stop();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
