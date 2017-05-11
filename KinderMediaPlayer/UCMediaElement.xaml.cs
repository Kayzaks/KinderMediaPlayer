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
        #region VIEWMODEL_INLINE

        class UCMediaElementViewModel : Notifier
        {
            private MediaElement referenceElement;

            public MediaElement ReferenceElement
            {
                get
                {
                    return referenceElement;
                }
                set
                {
                    SetField(ref referenceElement, value);
                }
            }

            public Action<MediaElement> CallbackOpen
            {
                get; set;
            }
        }

        #endregion VIEWMODEL_INLINE
        

        private UCMediaElementViewModel dataContext;
        
        public UCMediaElement()
        {
            InitializeComponent();

            dataContext = new UCMediaElementViewModel();
            this.DataContext = dataContext;
        }

        public UCMediaElement(MediaElement inElement, Action<MediaElement> inCallback) : this()
        {            
            dataContext.CallbackOpen = inCallback;
            dataContext.ReferenceElement = inElement;
        }


        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            dataContext.CallbackOpen?.Invoke(dataContext.ReferenceElement);
        }
    }
}
