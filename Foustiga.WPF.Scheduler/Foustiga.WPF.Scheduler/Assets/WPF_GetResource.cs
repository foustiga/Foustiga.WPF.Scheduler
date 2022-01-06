using System;
using System.IO;
using System.Reflection;

namespace Foustiga.WPF.Scheduler.Assets
{
    public static class WPF_GetResource //Helper to get resources in this Module
    {
        public static System.Drawing.Image GetDrawingImageFromResource(string resourceName)
        {
            //Get Resource image and returns it as a System.Drawing.Image (To be converted to ImageSource for use in WPF) 
            //Ensure that the image file is in the Assets folder, with 
            //    generation property set on "Ressource incorporee" 
            //    and copy in folder set on "Do not copy"

            Type t = typeof(WPF_GetResource);
            Assembly asm = Assembly.GetAssembly(t);

            string resourcePath = string.Format("{0}.{1}.{2}" //Resource in thisAssembly.Assets.xxx
                , asm.GetName().Name //Assembly name
                , @"Assets"          //within "Assets" folder
                , resourceName);     //complete image file name, including extension like .png

            Stream strm = asm.GetManifestResourceStream(resourcePath);// new System.IO.MemoryStream();
            //strm = asm.GetManifestResourceStream(resourcePath);

            if (strm is null) { throw new NullReferenceException($"Resource {resourceName} not found."); }
            else
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                return img;
            }
        }
    }
}
