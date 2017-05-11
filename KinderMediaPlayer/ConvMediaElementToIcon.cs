using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace KinderMediaPlayer
{
    public class ConvMediaElementToIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(MediaElement))
            {
                return "/Icons/unknown.png";
            }
            
            MediaElement referenceElement = value as MediaElement;
                        
            // In case we don't have an Icon path set, use one of the pre-defined Icons
            if (string.IsNullOrEmpty(referenceElement.Icon) == true)
            {
                switch (referenceElement.Type)
                {
                    case MediaElement.MEDIA_TYPE.IMAGE:

                        return "/Icons/image.png";

                    case MediaElement.MEDIA_TYPE.SOUND:

                        return "/Icons/audio.png";

                    case MediaElement.MEDIA_TYPE.VIDEO:

                        return "/Icons/video.png";
                }
            }

            return referenceElement.Icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
