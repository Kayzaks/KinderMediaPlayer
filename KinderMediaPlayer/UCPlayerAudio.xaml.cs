using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaktionslogik für UCPlayerAudio.xaml
    /// </summary>
    public partial class UCPlayerAudio : UserControl, IMediaPlayer
    {
        MediaPlayer soundPlayer;
        DispatcherTimer soundTimer;

        public UCPlayerAudio()
        {
            InitializeComponent();

            soundTimer = new DispatcherTimer();
            soundTimer.Interval = TimeSpan.FromSeconds(1);
            soundTimer.Tick += soundTimerTicks;
            soundPlayer = new MediaPlayer();
        }

        private void soundTimerTicks(object sender, EventArgs e)
        {
            if (soundPlayer.Source != null)
            {
                if (soundPlayer.NaturalDuration.HasTimeSpan)
                {
                    if (soundPlayer.Position >= soundPlayer.NaturalDuration.TimeSpan)
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
                soundPlayer.Open(new Uri(inSource));
                soundPlayer.Play();
                soundTimer.Start();
            }
            catch (Exception e)
            {
                closePlayer();
            }
        }

        private void UCPlayerAudio_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            closePlayer();
        }

        private void closePlayer()
        {
            soundPlayer.Stop();
            soundTimer.Stop();
            this.Visibility = Visibility.Collapsed;
        }

    }
}
