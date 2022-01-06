using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MNY.WPF.Image
{
    //Mettre la fonction en Convertisseur à utiliser depuis un View en xaml.
    /*
        xmlns:WPF_Image="clr-namespace:Global.WPF.Images;assembly=Global.WPF.Images"
    
        <UserControl.Resources>
            <WPF_Image:StreamToImageSourceConverter x:Key="StreamToImageSource"/>
        </UserControl.Resources>


        <Image Source="{Binding ViewIcon, Converter={StaticResource StreamToImageSource}}"/>

     */

    public class StreamToImageSourceConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value != null)
                {
                    //if (!(value.GetType() == typeof(System.IO.Stream)))
                    //{
                    //    return null;
                    //    //throw new ArgumentException("Value must be an Enumeration type");
                    //}
                    MemoryStream mStream = new MemoryStream();
                    (value as System.IO.Stream).CopyTo(mStream);

                    using (MemoryStream memoryStream = mStream)
                    {
                        var imageSource = new BitmapImage();
                        imageSource.BeginInit();
                        imageSource.StreamSource = memoryStream;
                        imageSource.EndInit();

                        // Assign the Source property of your image
                        return imageSource;
                    }
                }
                else
                {
                    return null;
                }

            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    
}