using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Foustiga.WPF.Scheduler.Foundation.Image
{
    //Mettre la fonction en Convertisseur à utiliser depuis un View en xaml.
    /*
       xmlns:WPF_Image="clr-namespace:Global.WPF.Images;assembly=Global.WPF.Images"

        <UserControl.Resources>
            <WPF_Image:DrawingImageToImageSourceConverter x:Key="DrawingImageToImageSource"/>
        </UserControl.Resources>


        <Image Source="{Binding ViewIcon, Converter={StaticResource DrawingImageToImageSource}}"/>
     
     */

    public class DrawingImageToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Convert System.Drawing.Image to System.Windows.Media.Imaging.ImageSource
            if (value != null)
            {
                BitmapImage bitmap = new BitmapImage();

                using (MemoryStream stream = new MemoryStream())
                {
                    // Save to the stream
                    (value as System.Drawing.Image).Save(stream, ImageFormat.Png);

                    // Rewind the stream
                    stream.Seek(0, SeekOrigin.Begin);

                    // Tell the WPF BitmapImage to use this stream
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                }

                return bitmap;
            }
            else
            {
                return null;
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Convert back from System.Windows.Media.Imaging.ImageSource to System.Drawing.Image
            if (value != null)
            {
                //Convert System.Windows.Media.Imaging.ImageSource to System.Drawing.Image
                MemoryStream ms = new MemoryStream();
                var encoder = new System.Windows.Media.Imaging.BmpBitmapEncoder();
                encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(value as System.Windows.Media.Imaging.BitmapSource));
                encoder.Save(ms);
                ms.Flush();
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                return img;
            }
            else
            {
                return null;
            }
        }
    }

}