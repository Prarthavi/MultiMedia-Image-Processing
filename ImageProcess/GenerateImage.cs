using System;
using System.Collections.Generic;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ImageProcess
{
    public class GenerateImage
    {
        private static Vector delta;
        private static Vector gamma;
        private static int clickCnt = 0;

        //
        //Filter Functions-----------------------------------------------------------------
        //


        /// <summary>
        /// This is an example filter that looks at a specific location
        //  and determines a gray scale value we can use as a threshold.
        //  We'll then use that threshold to convert the image into a 
        ///  bi-level image (black and white only).
        /// </summary>
        /// <param name="image"></param>
        /// <param name="postImage"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void FilterThreshold(RasterImage image, RasterImage postImage, int x, int y)
        {
            int height = image.Height;
            int width = image.Width;

            if (x > width || y > height)
                return;

            // What is the monochrome value at location x,y in the
            // input image, not the filtered image!
            Color temp = image[x, y];
            int threshgray = (int)((temp.R + temp.G + temp.B) / 3);

            // Now do the image threshold
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    // What is the gray value at this location?
                    temp = image[c, r];
                    int pixelgray = (int)((temp.R + temp.G + temp.B) / 3);

                    if (pixelgray >= threshgray)
                    {

                        // Set the pixel to white
                        postImage[c, r] = Color.White;
                    }
                    else
                    {
                        // Set the pixel to black
                        postImage[c, r] = Color.Black;
                    }

                }

            }

        }

        public static void OnFilterLowpass(RasterImage image, RasterImage postImage)
        {
            int height = image.Height;
            int width = image.Width;

            // reset the filtered image and make the same size as the input image
            int range = 1;

            //loop over pixels
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    Color pixel = Color.Black;
                    int tallyR = 0;
                    int tallyG = 0;
                    int tallyB = 0;

                    //loop over square around this pixel, watching boundaries
                    for (int i = -range; i <= range; i++)
                    {
                        if ((r + i) >= 0 && (r + i) < height)
                        {

                            for (int j = -range; j <= range; j++)
                            {
                                if ((c + j) >= 0 && (c + j) < width)
                                {
                                    //tally channels
                                    pixel = image[c + j, r + i];
                                    tallyR += pixel.R;
                                    tallyG += pixel.G;
                                    tallyB += pixel.B;
                                }
                            }
                        }
                    }

                    //average values, and set
                    int square = 2 * range + 1;
                    square *= square;
                    pixel = Color.FromArgb(tallyR / square, tallyG / square, tallyB / square);
                    postImage[c, r] = pixel;
                }
            }
        }

        /// <summary>
        /// This function implements an image warping example that moves
        ///   an default image onto two specified points on the image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="postImage"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nearest">use the nearest pixel or bilinear</param>
        public static void FilterWarp(RasterImage image, RasterImage postImage, int x, int y, bool nearest = false)
        {
            int height = image.Height;
            int width = image.Width;



            //
            // Step 0:  Handle the alternate mouse click feature.
            //

            // This handles the alternate mouse click feature
            // The first mouse click is just indicated on the
            // image, the second initiates the warping.
            clickCnt++;
            if ((clickCnt & 1) == 1)
            {
                ProcessImage.FillWhite(postImage);

                delta.X = x;
                delta.Y = y;

                // Draw a dot on the image
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        if (x > 0 && y > 0 && width > x && height > y)
                            postImage[(int)(delta.X + i), (int)(delta.Y + j)] = Color.Red;
                return;
            }

            gamma.X = x;
            gamma.Y = y;

            // Draw a dot on the image
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if (x > 0 && y > 0 && width > x && height > y)
                        postImage[(int)(gamma.X + i), (int)(gamma.Y + j)] = Color.Cyan;

            int w = image.Width;
            int h = image.Height;

            // 
            // Now we are actually going to do the warping work.
            //

            // These are the alignment points in the warp image
            // The destination points are delta and gamma
            Vector alpha = new Vector(w / 2, h);
            Vector beta = new Vector(w / 2, 2 * h);

            // These are the corners of the warp image
            System.Windows.Point tl = new System.Windows.Point(0, h);
            System.Windows.Point tr = new System.Windows.Point(w, h);
            System.Windows.Point bl = new System.Windows.Point(0, 0);
            System.Windows.Point br = new System.Windows.Point(w, 0);

            //
            // Step 1:  Build a matrix that translates points in the source 
            // image to points in the destination image.  
            //

            // Translate the point we are going to rotate around to the origin
            Matrix m1 = new Matrix
            {
                OffsetX = -alpha.X,
                OffsetY = -alpha.Y
            };

            // Rotate -90 degrees
            Matrix r1 = Matrix.Identity;
            r1.M11 = 0;
            r1.M21 = -1;
            r1.M12 = 1;
            r1.M22 = 0;
            //OR 
            //r1.Rotate(90);

            // Rotate onto the delta to gamma vector.
            Vector v = gamma - delta;
            v.Normalize();
            Matrix r2 = new Matrix
            {
                M11 = v.X,
                M21 = -v.Y,
                M12 = v.Y,
                M22 = v.X
            };

            // Scale image to the size of the output location.
            Matrix s = new Matrix();
            double scale = (gamma - delta).Length / (beta - alpha).Length;
            s.Scale(scale, scale);

            // Translate to the destination
            Matrix m2 = new Matrix
            {
                OffsetX = delta.X,
                OffsetY = delta.Y
            };

            // Multiply all of these matrices together.
            Matrix src_to_dest = m1 * r1 * r2 * s * m2;

            //
            // Step 2:  Create an inverse version of that matrix.
            //
            Matrix dest_to_src = src_to_dest;
            dest_to_src.Invert();

            // 
            // Step 3:  Determine the bounding box in the destination image for
            // the stickman image.  
            //
            System.Windows.Point itl = src_to_dest.Transform(tl);
            System.Windows.Point itr = src_to_dest.Transform(tr);
            System.Windows.Point ibl = src_to_dest.Transform(bl);
            System.Windows.Point ibr = src_to_dest.Transform(br);
            double minx = ColorHelpers.Min4(itl.X, itr.X, ibl.X, ibr.X);
            double maxx = ColorHelpers.Max4(itl.X, itr.X, ibl.X, ibr.X);
            double miny = ColorHelpers.Min4(itl.Y, itr.Y, ibl.Y, ibr.Y);
            double maxy = ColorHelpers.Max4(itl.Y, itr.Y, ibl.Y, ibr.Y);

            // Make sure the bounding box is in the image
            if (minx < 0)
                minx = 0;

            if (maxx >= postImage.Width)
                maxx = postImage.Width - 1;

            if (miny < 0)
                miny = 0;

            if (maxy >= postImage.Height)
                maxy = postImage.Height - 1;

            // 
            // Step 4:  Loop over the pixels in the bounding box, determining 
            // their color in the source image.  We use op to indicate pixel 
            // locations in the output image and ip to indicate locations in the
            // input image.
            //

            for (int y2 = (int)(miny + .5); y2 <= (int)(maxy + .5); y2++)
            {
                for (int x2 = (int)(minx + .5); x2 <= (int)(maxx + .5); x2++)
                {
                    // This is the equivalent point in the source image (stickman)
                    // This is the equivalent point in the source image 
                    double x1 = dest_to_src.M11 * x2 + dest_to_src.M21 * y2 + dest_to_src.OffsetX;
                    double y1 = dest_to_src.M12 * x2 + dest_to_src.M22 * y2 + dest_to_src.OffsetY;

                    int ix = (int)(x1);
                    double ixf = x1 - ix;      // Fractional part
                    int iy = (int)(y1);
                    double iyf = y1 - iy;

                    // Notice that I only use pixels that are going to be 
                    // within the stickman image and have pixels all of the
                    // way around.  This is so our bilinear interpolation
                    // will have neighbors on all sides!  That's why we
                    // have w-1 and h-1 here:
                    if (ix >= 0 && ix < w - 1 &&
                        iy >= 0 && iy < h - 1)
                    {
                        // This is a test for the color white, which is what I'll 
                        // consider to be transparent.  We only use colors that are
                        // not the pure white color.
                        if (!(image[ix, iy].A < 5 || ColorHelpers.ColorEqual(image[ix, iy], Color.White)))
                        {
                            if (nearest)
                            {
                                postImage[x2, y2] = image[ix, iy];
                            }
                            else
                            {
                                // Bilinear interpolation version
                                Color upLeft = image[ix, iy];
                                Color lowLeft = image[ix, iy + 1];
                                Color upRight = image[ix + 1, iy];
                                Color lowRight = image[ix + 1, iy + 1];
                                Color temp = ColorHelpers.ColorMultiply((1.0 - ixf) * (1.0 - iyf), upLeft);
                                temp = ColorHelpers.ColorAdd(temp, ColorHelpers.ColorMultiply((1.0 - ixf) * iyf, lowLeft));
                                temp = ColorHelpers.ColorAdd(temp, ColorHelpers.ColorMultiply(ixf * (1.0 - iyf), upRight));
                                temp = ColorHelpers.ColorAdd(temp, ColorHelpers.ColorMultiply(ixf * iyf, lowRight));

                                postImage[x2, y2] = temp;
                            }
                        }
                    }
                }
            }

        }
        public static Color BilinearInterpolate(double x, double y, RasterImage source)
        {
            int ix = (int)(x);
            double ixf = x - ix;      // Fractional part
            int iy = (int)(y);
            double iyf = y - iy;

            //check bounds
            if (ix >= 0 && ix < source.Width - 1 &&
                       iy >= 0 && iy < source.Height - 1)
            {
                //grab neighboring pixels
                Color upLeft = source[ix, iy];
                Color lowLeft = source[ix, iy + 1];
                Color upRight = source[ix + 1, iy];
                Color lowRight = source[ix + 1, iy + 1];

                //interpolate colors based on fractional part
                Color temp = ColorHelpers.ColorMultiply((1.0 - ixf) * (1.0 - iyf), upLeft);
                temp = ColorHelpers.ColorAdd(temp, ColorHelpers.ColorMultiply((1.0 - ixf) * iyf, lowLeft));
                temp = ColorHelpers.ColorAdd(temp, ColorHelpers.ColorMultiply(ixf * (1.0 - iyf), upRight));
                temp = ColorHelpers.ColorAdd(temp, ColorHelpers.ColorMultiply(ixf * iyf, lowRight));

                return temp;
            }
            else
            {
                //out of bounds
                return Color.White;
            }
        }

        public static void MakeAffine(RasterImage image, RasterImage postImage, double angle, double x, double y)
        {
            int postHeight = postImage.Height;
            int postWidth = postImage.Width;
            Matrix r1 = Matrix.Identity;

            //loop over target pixels
            for (int r = 0; r < postHeight; r++)
            {
                for (int c = 0; c < postWidth; c++)
                {
                    postImage[c, r] = image[postWidth - 1 - c, r];
                }
            }
            r1.TranslatePrepend(postWidth, postHeight);
            r1.RotatePrepend(angle);
            r1.TranslatePrepend(-postWidth, -postHeight);
            
            r1.Invert();
            r1.Translate(x, y);
            for (int r = 0; r < postHeight; r++)
            {
                for (int c = 0; c < postWidth; c++)
                {
                    System.Windows.Point v = new System.Windows.Point(c, r);
                    System.Windows.Point v2 = r1.Transform(v);

                   postImage[c, r] = BilinearInterpolate(v2.X, v2.Y, postImage);
                }
            }
        }

        internal static void median(RasterImage initImage, RasterImage postImage)
        {
            List<byte> redPixelList = new List<byte>();
            List<byte> greenPixelList = new List<byte>();
            List<byte> bluePixelList = new List<byte>();
            int width = initImage.Width;
            int height = initImage.Height;

            //applying Median Filtering 
            for (int c = 1; c <= width - 2; c++)
                for (int r = 1; r <= height - 2; r++)
                {
                    for (int x = c - 1; x <= c + 1; x++)
                        for (int y = r - 1; y <= r + 1; y++)
                        {
                            redPixelList.Add(initImage[x, y].R);
                            greenPixelList.Add(initImage[x, y].G);
                            bluePixelList.Add(initImage[x, y].B);
                        }
                    byte[] redPixels = redPixelList.ToArray();
                    byte[] bluePixels = bluePixelList.ToArray();
                    byte[] greenPixels = greenPixelList.ToArray();
                    redPixelList.Clear();
                    greenPixelList.Clear();
                    bluePixelList.Clear();

                    Array.Sort<byte>(redPixels);
                    Array.Reverse(redPixels);
                    Array.Sort<byte>(bluePixels);
                    Array.Sort<byte>(greenPixels);
                    Array.Reverse(bluePixels);
                    Array.Reverse(greenPixels);
                   
                    postImage[c, r] = Color.FromArgb(redPixels[4],greenPixels[4], bluePixels[4]);
                }
        }

        public static void FillTarget(RasterImage image, RasterImage postImage)
        {
            int height = image.Height;
            int width = image.Width;
            int postHeight = postImage.Height;
            int postWidth = postImage.Width;

            double scaleX = (double)postWidth / width;
            double scaleY = (double)postHeight / height;

            //loop over source pixels
            for (int r = 0; r < postHeight; r++)
            {
                for (int c = 0; c < postWidth; c++)
                {
                    double targetX = c / scaleX;
                    double targetY = r / scaleY;

                    postImage[c, r] = BilinearInterpolate(targetX, targetY, image);

                }
            }
        }
        public static void MakeSharpen(RasterImage image, RasterImage tempImage)
        {
            int height = image.Height;
            int width = image.Width;
          

            double[,] kernel = {
                { 0, -1,  0 } ,
                { -1,  5, -1 },
                { 0, - 1,  0 } 
            };
            int center = 1;
            int initWidth = image.Width;
            int initHeight = image.Height;
            for (int r = 0; r < initHeight; r++)
            {

                for (int c = 0; c < initWidth; c++)
                {

                    int red = 0;
                    int blue = 0;
                    int green = 0;


                    for (int i = -center; i <= center; i++)
                    {
                        for (int j = -center; j <= center; j++)
                        {
                            if (c + j >= 0 && c + j < initWidth && r + i >= 0 && r + i < initHeight)
                            {
                                Color temp = image[c + j, r + i];
                                double num = kernel[j + center, i + center];
                                red += (int)(num * temp.R);
                                blue += (int)(num * temp.B);
                                green += (int)(num * temp.G);
                            }
                        }
                    }
                    Color color = Color.FromArgb(red > 255 ? 255 : red < 0 ? 0 : red, green > 255 ? 255 : green < 0 ? 0 : green, blue > 255 ? 255 : blue < 0 ? 0 : blue);

                    tempImage[c, r] = color;
                }
            }
            

        }
        public static void MakeDiamond(RasterImage initImage, RasterImage postImage)
        {
            float initHeight = initImage.Height;
            float initWidth = initImage.Width;
            //loop over target pixels
            for (int y = 0; y < (initHeight / 2); y++)
            {

                for (int x = 0; x < (initImage.Width) / 2; x++)
                {
                    float xShiftPercent = (float)(((float)initHeight / 2) - y) / ((float)initHeight / 2);
                    float xShift = xShiftPercent * 1 * (initWidth / 2);
                    int temp = (int)(x - xShift);

                    if (temp >= 0 && temp < initImage.Width)
                        postImage[x, y] = initImage[temp, y];
                }
                for (int x = (initImage.Width) / 2; x < initWidth; x++)
                {
                    float xShiftPercent = (float)(((float)initHeight / 2) - y) / ((float)initHeight / 2);
                    float xShift = xShiftPercent * -1 * (initWidth / 2);
                    int temp = (int)(x - xShift);

                    if (temp >= 0 && temp < initImage.Width)
                        postImage[x, y] = initImage[temp, y];
                }
            }
            for (int y = (int)(initHeight / 2); y < initHeight; y++)
            {

                for (int x = 0; x < (initImage.Width) / 2; x++)
                {
                    float xShiftPercent = (float)(((float)initHeight / 2) - y) / ((float)initHeight / 2);
                    float xShift = xShiftPercent * -1 * (initWidth / 2);
                    int temp = (int)(x - xShift);

                    if (temp < initImage.Width && temp >= 0)
                        postImage[x, y] = initImage[temp, y];
                }
                for (int x = (initImage.Width) / 2; x < initWidth; x++)
                {
                    float xShiftPercent = (float)(((float)initHeight / 2) - y) / ((float)initHeight / 2);
                    float xShift = xShiftPercent * 1 * (initWidth / 2);
                    int temp = (int)(x - xShift);

                    if (temp >= 0 && temp < initImage.Width)
                        postImage[x, y] = initImage[temp, y];
                }
            }
        }



        public static void MakeGradientBlend(RasterImage initImage, RasterImage postImage)
        {
            float height = initImage.Height;
            float width = initImage.Width;

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    Color initColor = initImage[c, r];
                    byte ratio = (byte)((float)c / width * 255);
                    postImage[c,r] = Color.FromArgb((initColor.R + ratio)/2, initColor.G , initColor.B);
                }
            }
        }
        public static void ColorSwap(RasterImage initImage, RasterImage postImage)
        {
            float height = initImage.Height;
            float width = initImage.Width;

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    Color initColor = initImage[c, r];
                   
                    postImage[c, r] = Color.FromArgb(initColor.R , initColor.B, initColor.G);
                }
            }
        }

        public static void ChannelFilter(RasterImage initImage, RasterImage postImage,String color)
        {
            float height = initImage.Height;
            float width = initImage.Width;
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    Color initColor = initImage[c, r];
                    switch (color)
                    {
                        case "blue":
                                postImage[c, r] = Color.FromArgb(0, 0, initColor.B);
                            break;
                        case "green":
                            postImage[c, r] = Color.FromArgb(0, initColor.G, 0);
                            break;
                        case "red":
                            postImage[c, r] = Color.FromArgb(initColor.R,0, 0);
                            break;

                    }
                    
                }
            }
        }
        public static void MakeSobelFilter(RasterImage initImage, RasterImage postImage)
        {
            float initHeight = initImage.Height;
            float initWidth = initImage.Width;


            int[,] sorbelHMatrix =
            {
                {2,2,4,2,2},
                {1,1,2,1,1},
                {0,0,0,0,0},
                {-1,-1,-2,-1,-1},
                {-2,-2,-4,-2,-2}
             };
            int[,] sorbelVMatrix =
            {
              {2,1,0,-1,-2 },
              {2,1,0,-1,-2 },
              {4,2,0,-2,-4},
              {2,1,0,-1,-2 },
              {2,1,0,-1,-2 }
            };

            for (int r = 0; r < initHeight;r++)
            {
                
                for(int c = 0; c < initWidth;c++)
                {

                    int red = 0;
                    int blue = 0;
                    int green = 0;
                    int redY = 0;
                    int blueY = 0;
                    int greenY = 0;

                    for (int i = -2; i < 3;i++)
                   {
                        for (int j = -2; j < 3; j++)
                        {
                            if(c + j >= 0 && c + j < initWidth && r + i >= 0 && r + i < initHeight)
                            {
                                Color temp = initImage[c + j, r + i];
                                int num = sorbelHMatrix[j + 2, i + 2];
                                red += (num * temp.R);
                                blue += (num * temp.B);
                                green += (num * temp.G);
                                num = sorbelVMatrix[j + 2, i + 2];
                                redY += (num * temp.R);
                                blueY += (num * temp.B);
                                greenY += (num * temp.G);
                            }
                        }
                   }
                    Color GX = Color.FromArgb(red > 255 ? 255 : red < 0 ? 0: red, green > 255 ? 255 : green < 0 ? 0 : green, blue > 255 ? 255 : blue < 0 ? 0 : blue);
                    Color GY = Color.FromArgb(redY > 255 ? 255 : redY < 0 ? 0: redY, greenY > 255 ? 255 : greenY < 0 ? 0 : greenY, blueY > 255 ? 255 : blueY < 0 ? 0 : blueY);

                    red = (int)Math.Sqrt((GX.R * GX.R) + (GY.R * GY.R));
                    green = (int)Math.Sqrt((GX.G * GX.G) + (GY.G * GY.G));
                    blue = (int)Math.Sqrt((GX.B * GX.B) + (GY.B * GY.B));
                    
                    postImage[c,r] = Color.FromArgb(red < 190 ? 0 : red > 255 ? 255: 1, green < 190 ? 0 : green > 255 ? 255 :1, blue < 190 ? 0 : blue > 255 ? 255 : 1);
                }
            }

        }
        public static void DrawCircle(RasterImage initImage, RasterImage postImage)
        {
            float initHeight = initImage.Height;
            
            for(int r = 0; r < initHeight; r++)
            {
                for(int c = 0; c < initImage.Width; c++)
                {
                        postImage[c, r] = initImage[c, r];
                }
            }
            for(int r = 0; r < 100; r++)
            {

                 int x = (int)Math.Sqrt(10000 - (r * r));
                for(int c = 0; c <= x; c++)
                {
                    postImage[c, r] = Color.Black;
                }

            }

        }
        public static void differenceOverlay(RasterImage initImage, RasterImage postImage)
        {
            float initHeight = initImage.Height;
            RasterImage mask = new RasterImage(new Bitmap(Properties.Resources.Mask));
            for (int r = 0; r < initHeight; r++)
            {
                for (int c = 0; c < initImage.Width; c++)
                {
                    if(r < mask.Height && c < mask.Width)
                    postImage[c, r] = ColorHelpers.ColorSub(initImage[c,r], mask[c,r]);
                }
            }
        }



        public static void gaussianBlur(RasterImage initImage, RasterImage postImage, int length, double sigma)
        {
           
            double[,] gaussianMatrix = new double[length, length];
            double total = 0;
            int initHeight = initImage.Height;
            int initWidth = initImage.Width;
            int center = length / 2;
            double sigmaSquare = sigma * sigma;
            Console.WriteLine("width: " + length + "center " + center);

            double baseX = 1.0 / (2.0 * Math.PI * sigmaSquare);

            for (int r = -center; r <= center; r++)
            {
                for (int c = -center;c <= center; c++)
                {
                    double distance = ((c * c) + (r * r)) / (2 * (sigmaSquare));
                   double sum  = baseX * Math.Exp(-distance);
                    gaussianMatrix[c + center, r + center] = sum;
                    total += sum;
                }
            }


            for (int r = 0; r < length; r++)
            {
                for (int c = 0; c < length; c++)
                {
                    gaussianMatrix[c, r] = ((double)gaussianMatrix[c, r] / (double)total);
                }
            }


            for (int r = 0; r < initHeight; r++)
            {

                for (int c = 0; c < initWidth; c++)
                {

                    int red = 0;
                    int blue = 0;
                    int green = 0;
                    

                    for (int i = -center; i <= center; i++)
                    {
                        for (int j = -center; j <= center; j++)
                        {
                            if (c + j >= 0 && c + j < initWidth && r + i >= 0 && r + i < initHeight)
                            {
                                Color temp = initImage[c + j, r + i];
                                double num = gaussianMatrix[j + center, i + center];
                                red += (int)(num * temp.R);
                                blue += (int)(num * temp.B);
                                green += (int)(num * temp.G);
                            }
                        }
                    }
                    Color color = Color.FromArgb(red > 255 ? 255 : red < 0 ? 0 : red,  green > 255 ? 255 : green < 0 ? 0 : green, blue > 255 ? 255 : blue < 0 ? 0 : blue);

                    postImage[c, r] = color;
                }
            }

        }

        internal static void rotateImage(RasterImage initImage, RasterImage postImage, double angle)
        {
            Matrix r1 = Matrix.Identity;
            int postHeight = initImage.Height;
            int postWidth = initImage.Width;

            r1.TranslatePrepend(postHeight / 2, postWidth / 2);
            r1.RotatePrepend(angle);
            r1.TranslatePrepend(-postHeight / 2, -postWidth / 2);
            r1.Invert();
            for (int r = 0; r < postHeight / 2; r++)
            {
                for (int c = 0; c < postWidth / 2; c++)
                {
                    System.Windows.Point v = new System.Windows.Point(c, r);

                    System.Windows.Point v2 = r1.Transform(v);
                    postImage[c, r] = BilinearInterpolate(v2.X, v2.Y, initImage);
                }
                for (int c = postWidth / 2; c < postWidth; c++)
                {
                    postImage[c, r] = initImage[c, r];
                }
            }

            for (int r = postHeight / 2; r < postHeight; r++)
            {
                for (int c = 0; c < postWidth; c++)
                {
                    postImage[c, r] = initImage[c, r];
                }
            }
        }
    }

    
}
