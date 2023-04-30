using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcess
{
    public class RasterImage
    {

        private Bitmap myBitmap;
        private bool dirty = false;

        public static int R = 0;
        public static int G = 1;
        public static int B = 2;
        public static int A = 3;

        public Bitmap toBitmap
        {
            get
            {
                if (dirty)
                {
                    //new bitmap to fill
                    if (channels == 4)
                        myBitmap = new Bitmap(Width, Height);
                    else
                        myBitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

                    //lock bytes for entire image
                    var BoundsRect = new Rectangle(0, 0, Width, Height);
                    BitmapData bmpData = myBitmap.LockBits(BoundsRect,
                                                    ImageLockMode.WriteOnly,
                                                    myBitmap.PixelFormat);

                    IntPtr ptr = bmpData.Scan0; //location of bytes in memory
                    int bytes = bmpData.Stride * myBitmap.Height; //num of bytes in image
                    Marshal.Copy(bytedata, 0, ptr, bytes); //copy bytes back to bitmap
                    myBitmap.UnlockBits(bmpData); //release the bitmap's bytes so it can be rendered
                }
                dirty = false;
                return myBitmap;
            }
        }
        public int Height { get => height; }
        public int Width { get => width; }
        public int Channels { get => channels; }
        byte[] bytedata = null;
        int channels = 0;
        int width = 0;
        int height = 0;

        public Color this[int x, int y]
        {
            get
            {
                if (channels == 4)
                    return Color.FromArgb(bytedata[width * y * channels + x * channels + 3],
                        bytedata[width * y * channels + x * channels + 2],
                        bytedata[width * y * channels + x * channels + 1],
                        bytedata[width * y * channels + x * channels]);
                else
                    return Color.FromArgb(bytedata[width * y * channels + x * channels + 2],
                        bytedata[width * y * channels + x * channels + 1],
                        bytedata[width * y * channels + x * channels]);
            }
            set
            {
                if (channels == 4)
                {
                    bytedata[width * y * channels + x * channels] = value.B;
                    bytedata[width * y * channels + x * channels + 1] = value.G;
                    bytedata[width * y * channels + x * channels + 2] = value.R;
                    bytedata[width * y * channels + x * channels + 3] = value.A;
                }
                else
                {
                    bytedata[width * y * channels + x * channels] = value.B;
                    bytedata[width * y * channels + x * channels + 1] = value.G;
                    bytedata[width * y * channels + x * channels + 2] = value.R;
                }

                dirty = true;
            }
        }

        public byte this[int x, int y, int p]
        {

            get
            {
                //correct for BGR and make RGB
                if (p == 2)
                    p = 0;
                else if (p == 0)
                    p = 2;
                return bytedata[width * y * channels + x * channels + p];
            }
            set
            {
                //correct for BGR and make RGB
                if (p == 2)
                    p = 0;
                else if (p == 0)
                    p = 2;

                if (channels == 4)
                {
                    bytedata[width * y * channels + x * channels + p] = value;
                }
                else
                {
                    bytedata[width * y * channels + x * channels + p] = value;
                }

                dirty = true;
            }
        }

        public byte this[int index]
        {

            get
            {
                return bytedata[index];
            }
            set
            {
                bytedata[index] = value;
                dirty = true;
            }
        }

        /// <summary>
        /// yOffset should be the menu bar height
        /// </summary>
        /// <param name="yOffest"></param>
        public RasterImage(String path) : this(new Bitmap(path))
        { }

        /// <summary>
        /// yOffset should be the menu bar height
        /// </summary>
        /// <param name="yOffest"></param>
        public RasterImage() : this(100, 100)
        { }

        public RasterImage(int width, int height)
        {
            init(width, height);
        }

        private void init(int width, int height)
        {
            this.width = width;
            this.height = height;
            channels = 4;

            int numbytes = 4 * height * width; //make rgba
            bytedata = new byte[numbytes];
            fill(Color.White);

            dirty = true;
        }

        public RasterImage(Bitmap bitmap)
        {
            convertFromBitmap(bitmap);
        }

        public RasterImage(RasterImage img)
        {
            width = img.Width;
            height = img.Height;
            channels = img.Channels;
            bytedata = new byte[img.bytedata.Length];
            img.bytedata.CopyTo(bytedata, 0);
            dirty = true;
        }

        public void fill(Color color)
        {
            for (int c = 0; c < Width; c++)
                for (int r = 0; r < Height; r++)
                {
                    this[c, r] = color;
                }
        }


        private void convertFromBitmap(Bitmap bitmap)
        {
            BitmapData bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);

            switch (bitmap.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Canonical:
                    channels = 4;
                    break;
                case PixelFormat.Format24bppRgb:
                    channels = 3;
                    break;
                default:
                    MessageBox.Show("Unsupported format", "Error");
                    return;
            }


            width = bmpdata.Width;
            height = bmpdata.Height;

            int numbytes = bmpdata.Stride * height;
            bytedata = new byte[numbytes]; //array to fill.
            IntPtr ptr = bmpdata.Scan0; //location of bytes in memory

            Marshal.Copy(ptr, bytedata, 0, numbytes); //copy bytes to a usable byte array
            bitmap.UnlockBits(bmpdata);

            dirty = true;
        }

        /// <summary>
        /// reset to base image
        /// </summary>
        public void Reset()
        {
            init(100, 100);
        }



        public void Close()
        {
            myBitmap = null;

        }

        /// <summary>
        /// Simple parallel filter to edit pixels, based on a single, current pixel color
        /// </summary>
        /// <param name="functionToPass"> A function that takes in the current pixel color, 
        /// and returns the filtered version</param>
        public void parallelFilter(Func<Color, Color> functionToPass)
        {
            Parallel.For(0, width, c =>
            {
                Parallel.For(0, height, r =>
                {
                    this[c, r] = functionToPass(this[c, r]);
                });
            });
        }


        /// <summary>
        /// Parallel filter to edit pixels, using the original image area
        /// <param name="newImg">the resulting image</param>
        /// <param name="functionToPass"> A function that takes in the column, row of the pixel to edit 
        /// and original image. Return the new color for the pixel location</param>
        public void parallelFilter(RasterImage newImg, Func<int, int, RasterImage, Color> functionToPass)
        {
            Parallel.For(0, width, c =>
            {
                Parallel.For(0, height, r =>
                {
                    newImg[c, r] = functionToPass(c, r, this);
                });
            });
        }
    }
}

