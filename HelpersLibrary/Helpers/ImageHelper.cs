using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelpersLibrary.Helpers
{
    public class ImageHelper
    {
        public static byte[] TakeScreenshot(out Size originalResolution, int width = 0, int height = 0, int compressionLevel = 50)
        {
            if (compressionLevel > 100)
                compressionLevel = 100;

            if (compressionLevel < 0)
                compressionLevel = 0;


            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format32bppArgb);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L - compressionLevel);
            myEncoderParameters.Param[0] = myEncoderParameter;


            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmp);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            originalResolution = new Size(bmp.Width, bmp.Height);

            if (width != 0 && height != 0)
                bmp = new Bitmap(bmp, new Size(width, height));

            using (var stream = new MemoryStream())
            {
                bmp.Save(stream, jpgEncoder, myEncoderParameters);
                return stream.ToArray();
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
