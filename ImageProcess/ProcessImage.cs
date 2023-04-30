using System;
using System.Drawing;


namespace ImageProcess
{
    public class ProcessImage 
    {

       
        //
        //basic processing functions-------------------------------------------
        //

        /// <summary>
        /// This class function fills the base image with white.
        /// </summary>
        /// <param name="image">image to edit</param>
        public static void FillWhite(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                // Looping over the columns of the array
                for (int c = 0; c < width; c++)
                {
                    image[c, r] = Color.White;
                }
            }

        }
        public static void FillGreen(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                // Looping over the columns of the array
                for (int c = 0; c < width; c++)
                {
                    image[c, r] = Color.Green;
                }
            }
        }
        public static void FillLavender(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                // Looping over the columns of the array
                for (int c = 0; c < width; c++)
                {
                    image[c, r] = Color.FromArgb(230,230,250);
                }
            }
        }
        public static void OnFilterDim(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                // Looping over the columns of the array
                for (int c = 0; c < width; c++)
                {
                    Color temp = image[c, r];
                    temp = ColorHelpers.ColorMultiply(0.33, temp);
                    image[c, r] = temp;
                }

            }
        }

        public static void OnFilterTint(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                // Looping over the columns of the array
                for (int c = 0; c < width; c++)
                {
                    Color temp = image[c, r];
                    temp = Color.FromArgb(
                            ColorHelpers.ClampColorElem(0.33 * temp.R),
                            ColorHelpers.ClampColorElem(temp.G),
                            ColorHelpers.ClampColorElem(0.66 * temp.B));
                    image[c, r] = temp;
                }
            }
        }

        //


        /// <summary>
        /// This class function fills the base image with black.
        /// </summary>
        /// <param name="image">image to edit</param>
        public static void FillBlack(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                // Looping over the columns of the array
                for (int c = 0; c < width; c++)
                {
                    image[c, r] = Color.Black;
                }
            }

        }

        /// <summary>
        /// Example filter that computes a negative image.
        /// </summary>
        /// <param name="image"></param>
        public static void OnFilterNegative(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                // Looping over the columns of the array
                for (int c = 0; c < width; c++)
                {
                    Color temp = image[c, r];
                    temp = Color.FromArgb(255 - temp.R,
                                          255 - temp.G,
                                          255 - temp.B);
                    image[c, r] = temp;
                }
            }
        }

        public static void verticalBlueGradient(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    image[c, r] = Color.FromArgb(0, 0, 255 - (byte)((float)r / (float)height * 255));
                }
            }
        }

        public static void diagonalGradient(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    byte ratio = (byte)((float)(c + r) / (float)(width + height) * 255);
                    image[width - c - 1, r] = Color.FromArgb(ratio, 255 - ratio, 0);
                }
            }
        }



        public static void horizontalGradient(RasterImage image)
        {
            int height = image.Height;
            int width = image.Width;

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    image[c, r] = Color.FromArgb((byte)((float)c / (float)width * 255),
                        (byte)((float)c / (float)width * 255), (byte)((float)c / (float)width * 255));
                }
            }
        }

        public static void diagonalLine(RasterImage initImage)
        {

            for (int c = 100; c <= 400; c++)
            {

                float r = ((float)1 / (float)3 * c) + ((float)200 / (float)3);
                if (c < initImage.Width && r < initImage.Height)
                    initImage[c, (int)r] = Color.Red;
            }
        }

        public static void verticalLine(RasterImage initImage)
        {
            int[] c = { 100, 400 };
            int height = initImage.Height;
            int width = initImage.Width;
            for (int r = 0; r < height; r++)
            {
                if (c[0] < width)
                    initImage[c[0], r] = Color.Red;
                if (c[0] + 1 < width)
                    initImage[c[0] + 1, r] = Color.Red;
                if (c[1] < width)
                    initImage[c[1], r] = Color.Red;
                if (c[1] + 1 < width)
                    initImage[c[1] + 1, r] = Color.Red;
            }
        }

        public static void horizontalLine(RasterImage initImage)
        {
            int[] r = { 100, 200 };

            for (int c = 0; c < initImage.Width; c++)
            {
                if (r[0] < initImage.Height)
                    initImage[c, r[0]] = Color.Red;
                if (r[1] < initImage.Height)
                    initImage[c, r[1]] = Color.Red;
            }
        }

        public static void monochrome(RasterImage initImage)
        {
            int div = initImage.Channels;
           for (int r = 0; r < initImage.Height; r++)
           {
                for(int c = 0; c < initImage.Width;c++)
                {
                    int sum = initImage[c, r].R + initImage[c, r].G + initImage[c, r].B;
                    sum = div > 3 ? sum + initImage[c, r].A : sum;
                    int avg = sum/ div;
                    initImage[c, r] = Color.FromArgb(avg, avg, avg);
                }

           }
        }
    }
}
