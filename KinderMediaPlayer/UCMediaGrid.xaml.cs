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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinderMediaPlayer
{
    /// <summary>
    /// Interaction logic for UCMediaGrid.xaml
    /// </summary>
    public partial class UCMediaGrid : UserControl
    {
        public UCMediaGrid()
        {
            InitializeComponent();
        }

        #region EXTERNAL_BINDING

        // This Event will be called when one of the MediaElements is clicked
        public event EventHandler OnStartMediaElement;

        // Binding to a List of all the MediaElement's
        public static readonly DependencyProperty MediaListProperty =
                    DependencyProperty.Register(
                    "MediaList", typeof(ObservableCollection<MediaElement>), typeof(UCMediaGrid),
                    new PropertyMetadata(new ObservableCollection<MediaElement>(), new PropertyChangedCallback(OnMediaListChanged))
                    );

        public ObservableCollection<MediaElement> MediaList
        {
            get
            {
                return (ObservableCollection<MediaElement>)GetValue(MediaListProperty);
            }
            set
            {
                SetValue(MediaListProperty, value);
            }
        }

        public static void OnMediaListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null && d is UCMediaGrid)
            {
                ((UCMediaGrid)d).populateElements();
            }
        }

        #endregion EXTERNAL_BINDING

        #region MEDIA_ELEMENTS

        private void populateElements()
        {
            // Called when the MediaList has changed
            if (MediaList == null)
            {
                return;
            }

            pnlElements.Children.Clear();
            pnlElements.RowDefinitions.Clear();
            pnlElements.ColumnDefinitions.Clear();

            if (MediaList.Count == 0)
            {
                return;
            }

            // We order the Elements in a perfectly square grid
            int sqrtCount = (int) Math.Ceiling(Math.Sqrt((double)MediaList.Count));

            // but reduce the number of rows, depending on the amount of
            // elements actually present
            int numRows = MediaList.Count / sqrtCount;

            if (MediaList.Count % sqrtCount > 0)
            {
                numRows = numRows + 1;
            }

            for (int i = 0; i < numRows; ++i)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(1, GridUnitType.Star);
                pnlElements.RowDefinitions.Add(gridRow);
            }
            for (int i = 0; i < sqrtCount; ++i)
            {
                ColumnDefinition gridColumn = new ColumnDefinition();
                gridColumn.Width = new GridLength(1, GridUnitType.Star);
                pnlElements.ColumnDefinitions.Add(gridColumn);
            }

            // And now we populate each Row as a Stackpanel and add it in
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < sqrtCount; ++j)
                {
                    int currentIndex = i * sqrtCount + j;
                    if (currentIndex < MediaList.Count)
                    {
                        UCMediaElement currentElement = new UCMediaElement(MediaList[currentIndex], openMediaElement);
                        Grid.SetRow(currentElement, i);
                        Grid.SetColumn(currentElement, j);
                        pnlElements.Children.Add(currentElement);
                    }
                }
            }
        }

        private void openMediaElement(MediaElement inElement)
        {
            if (OnStartMediaElement != null)
            {
                OnStartMediaElement(inElement, EventArgs.Empty);
            }
        }

        #endregion MEDIA_ELEMENTS
    }
}
