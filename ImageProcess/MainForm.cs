
//Mark off what items are complete, and put a P if partially complete. For the categories, name the filter and its points. If you crete more than one per category, list their names and their poitns below the required command for that catagory.

//Total available points:  200(250 for CSC 524)

// Done 8pt make a new\open\save image under a File Menu and add Project 1 menu
// Done 2pt display the current state of the image 
// Done 10pt A 3X3 sharpen filter 
// Done 15pt A 5X5 Prewit or Sorbel filter 
// Done 10pt Draw a 100 pixel radius quarter circle
// Done 15pt A horizontal flip (no vertical), a rotation about the bottom right corner, and then a translation (x and y) afterwards
// Done 20pt A gradient screen overlay that blends with the underlying image.  
// NA 30pt OPTIONAL for individuals, and required for teams slide show
// NA 15pt (minimum) GRADUATE proposal and make a procedureal texture
//...

// Done 20pt Gaussian blur - Required Blur\Sharpen\Contrast\Filter
//...

// Done 15pt Channel Filter (all three) - Required Feature Extraction
// Done 10pt blue-green channel swap
//...

// Done 30pt Rotate the upper-left quarter, user allowed to enter rotation parameters (Linear warp a sub selection) - Required Linear warp
//...

// Done 40pt Diamond - Required Non-linear warp
//...

// Done 20 Difference Overlay -  Required Composition

// Total: 215 pt

//The grade you compute is the starting point for course staff, who reserve the right to change the grade if they disagree with your assessment and to deduct points for other issues they may encounter, such as errors in the submission process, naming issues, etc.
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcess
{
    public partial class MainForm : Form
    {
        public enum MODE
        {
            None, Draw, Move, Threshold, WarpBilinear, WarpNearest
        }

        protected MODE mouseMode = MODE.None;
        RasterImage initImage;
        RasterImage postImage;
        
        int offset = 0;
        public MainForm()
        {
            InitializeComponent();
            SetMenuOptionEnable();
            offset = menuStrip1.Height;
            DoubleBuffered = true;
        }

        #region Drawing

        //main Paint function
        protected override void OnPaint(PaintEventArgs e)
        {
            if (initImage == null)
                return;

            Graphics g = e.Graphics;

            //color the background
            Brush b = new SolidBrush(Color.DarkGray);
            g.FillRectangle(b, e.ClipRectangle);

            if (postImage == null)
            {
                //draw only the original
                g.DrawImage(initImage.toBitmap, 0, offset);
            }
            else
            {
                //draw BOTH the original and post processed image

                g.DrawImage(initImage.toBitmap, 0, offset, initImage.Width, initImage.Height);
                g.DrawImage(postImage.toBitmap, initImage.Width + 2, offset, postImage.Width, postImage.Height);

                //
                // Draw a bar between the two images
                //
                Pen black = new Pen(Color.Black);
                black.Width = 3;

                g.DrawLine(black, new PointF(initImage.Width, offset), new PointF(initImage.Width, initImage.Height + offset));
            }
        }

        //redraw on resize
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        #endregion

        #region File menu
        private void newMenu_Click(object sender, EventArgs e)
        {
            initImage = new RasterImage();
            ProcessImage.FillBlack(initImage);
            SetMenuOptionEnable();
            Invalidate();
        }

        private void openTestMenu_Click(object sender, EventArgs e)
        {
            initImage = new RasterImage(new Bitmap(Properties.Resources.Leaf));
            SetMenuOptionEnable();
            Invalidate();
        }

        private void openMenu_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                initImage = new RasterImage(new Bitmap(openFileDialog.FileName));
                SetMenuOptionEnable();
                Invalidate();
            }
        }

        private void SaveMenu_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                OnSave(saveFileDialog.FileName, saveFileDialog.FilterIndex, postImage.toBitmap);
            }
        }

        /// <summary>
        /// function to save a image. Supports, jpeg, gif, png, and bmp
        /// </summary>
        /// <param name="path">location to save</param>
        /// <param name="index">index (format type) from file dialog</param>
        /// <param name="image">image to save</param>
        /// <returns></returns>
        public bool OnSave(string path, int index, Bitmap image)
        {
            try
            {
                switch (index)
                {
                    case 1:
                        image.Save(path, ImageFormat.Jpeg);
                        break;
                    case 2:
                        image.Save(path, ImageFormat.Gif);
                        break;
                    case 3:
                        image.Save(path, ImageFormat.Png);
                        break;
                    case 4:
                        image.Save(path, ImageFormat.Bmp);
                        break;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return false;
            }

            return true;
        }

        private void closeMenu_Click(object sender, EventArgs e)
        {
            if(initImage != null)
                initImage.Close();
            if (postImage != null)
                postImage.Close();
            SetMenuOptionEnable();
            Invalidate();
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            Close();
        }
        

        private void SetMenuOptionEnable()
        {
            if (initImage == null)
            {
                bool on = false;
                fillWhiteMenu.Enabled = on;
                copyMenu.Enabled = on;
                negativeMenu.Enabled = on;
                thresholdMenu.Enabled = on;
                warpNearestMenu.Enabled = on;
                warpBilenearMenu.Enabled = on;
                drawMenu.Enabled = on;
            }
            else {
                bool on = true;
                fillWhiteMenu.Enabled = on;
                copyMenu.Enabled = on;
                negativeMenu.Enabled = on;
                thresholdMenu.Enabled = on;
                warpNearestMenu.Enabled = on;
                warpBilenearMenu.Enabled = on;
                drawMenu.Enabled = on;

            }

           
        }


        #endregion

        #region Generate menu
        private void copyMenu_Click(object sender, EventArgs e)
        {
            postImage = new RasterImage(initImage);
            Invalidate();
        }

        private void thresholdMenu_Click(object sender, EventArgs e)
        {
            mouseMode = MODE.Threshold;
            drawMenu.Checked = false;
        }

        private void warpNearestMenu_Click(object sender, EventArgs e)
        {
            mouseMode = MODE.WarpNearest;
            drawMenu.Checked = false;

            //make post image if needed, and clear
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            ProcessImage.FillWhite(postImage);
            Invalidate();
        }

        private void warpBilenearMenu_Click(object sender, EventArgs e)
        {
            mouseMode = MODE.WarpBilinear;
            drawMenu.Checked = false;

            //make post image if needed, and clear
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            ProcessImage.FillWhite(postImage);
            Invalidate();
        }

        #endregion

        #region Process menu

        private void fillWhiteMenu_Click(object sender, EventArgs e)
        {
            ProcessImage.FillWhite(initImage);
            Invalidate();
        }

        private void negativeMenu_Click(object sender, EventArgs e)
        {
            ProcessImage.OnFilterNegative(initImage);
            Invalidate();
        }

        private void drawMenu_Click(object sender, EventArgs e)
        {
            drawMenu.Checked = !drawMenu.Checked;
            if (drawMenu.Checked)
                mouseMode = MODE.Draw;
            else
                mouseMode = MODE.None;
            SetMenuOptionEnable();
            Invalidate();
        }


        #endregion

        #region Mouse

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e); 

            if (initImage == null)
                return;

            int width = initImage.Width;
            int height = initImage.Height;
            int x = e.X;
            int y = e.Y - offset; //Remove menu strip height from click point

            
            switch(mouseMode)
            {
                case MODE.None:
                    break;
                case MODE.Draw:
                    if (x < width && y < height)
                        initImage[x, y] = Color.Red;
                        mouseMode = MODE.Move;
                    break;
                case MODE.Move:
                    mouseMode = MODE.Draw; //if this happens, there was a glitch, restart
                    break;
                case MODE.Threshold:
                    if (postImage == null)
                        postImage = new RasterImage(initImage.Width, initImage.Height);
                    GenerateImage.FilterThreshold(initImage, postImage, x, y);
                    break;
                case MODE.WarpNearest:
                    GenerateImage.FilterWarp(initImage, postImage, x, y, true);
                    break;
                case MODE.WarpBilinear:
                    GenerateImage.FilterWarp(initImage, postImage, x, y);
                    break;
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            switch (mouseMode)
            {
                case MODE.Move:
                    mouseMode = MODE.Draw; //allow a restart
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (initImage == null)
                return;

            int width = initImage.Width;
            int height = initImage.Height;
            int x = e.X;
            int y = e.Y - offset; //Remove menu strip height from click point

            switch (mouseMode)
            {
                case MODE.None:
                    break;
                case MODE.Move:
                    if (x < width && y < height)
                    { 
                        initImage[x, y] = Color.Red;
                        Invalidate();
                    }
                    break;
                case MODE.Threshold:
                    break;
                case MODE.WarpNearest:
                    break;
                case MODE.WarpBilinear:
                    break;
            }
           
        }



        #endregion


        private DialogResult showDialog(string title, string promptText,ref double value,ref double x,ref double y)
        {
            Form form = new Form();
            Label label = new Label();
            Label labelX = new Label();
            Label labelY = new Label();

            TextBox textBox = new TextBox();
            TextBox textBoxX = new TextBox();
            TextBox textBoxY = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            labelY.Text = "Y:";

            labelX.Text = x == 2.1 && y == -1 ? "Sigma:" : "X:";
            textBox.Text = value.ToString();
            textBoxX.Text = x.ToString();
            textBoxY.Text = y.ToString();

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 58, 13);
            labelX.SetBounds(59, 20, 108, 13);
            labelY.SetBounds(109, 20, 372, 13);
            textBox.SetBounds(9, 36, 124 , 6);
            textBoxX.SetBounds(59, 36, 124, 6);
            textBoxY.SetBounds(109, 36, 124, 6);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            labelX.AutoSize = true;
            labelY.AutoSize = true;

            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            textBoxX.Anchor = textBox.Anchor | AnchorStyles.Right;
            textBoxY.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            if(x == -1 && y == -1)
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            else if(x == 2.1 && y == -1)
                form.Controls.AddRange(new Control[] { label,labelX, textBox, textBoxX, buttonOk, buttonCancel });
            else
                form.Controls.AddRange(new Control[] { label,labelX,labelY, textBox,textBoxX,textBoxY, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = Double.Parse(textBox.Text);
            x = Double.Parse(textBoxX.Text);
            y = Double.Parse(textBoxY.Text);
            return dialogResult;
        }

        
        private void fillGreenOption_Click(object sender, EventArgs e)
        {
            ProcessImage.FillGreen(initImage);
            Invalidate();
        }

        private void dimOption_Click(object sender, EventArgs e)
        {
            ProcessImage.OnFilterDim(initImage);
            Invalidate();
        }

        private void tintOption_Click(object sender, EventArgs e)
        {
            ProcessImage.OnFilterTint(initImage);
            Invalidate();
        }
        private void lowpassOption_Click(object sender, EventArgs e)
        {
            //make post image if needed
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);

            GenerateImage.OnFilterLowpass(initImage, postImage);
            Invalidate();
        }

        private void makeSquareOption_Click(object sender, EventArgs e)
        {
            int size = Math.Max(initImage.Width, initImage.Height);
            postImage = new RasterImage(size * 2, size * 2);

            GenerateImage.FillTarget(initImage, postImage);
            Invalidate();
        }

        private void makeAffineOption_Click(object sender, EventArgs e)
        {
            double angle = 30;
            double x = 0;
            double y = 0;
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            if (showDialog("Enter information", "angle:", ref angle,ref x,ref y) == DialogResult.OK)
            {
                GenerateImage.MakeAffine(initImage, postImage, angle, x, y);
            }
            
            Invalidate();
        }

        private void makeArrowOption_Click(object sender, EventArgs e)
        {
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            
            Invalidate();
        }


        private void drawCircle_Click(object sender, EventArgs e)
        {
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.DrawCircle(initImage, postImage);
            Invalidate();
        }

        private void ChannelFilter_Click(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.ChannelFilter(initImage, postImage, sender.ToString().ToLower());
            Invalidate();
        }

        private void diamond_Click(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.MakeDiamond(initImage, postImage);
            Invalidate();
        }

        private void sharpen_Click(object sender, EventArgs e)
        {
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.MakeSharpen(initImage, postImage);
            Invalidate();

        }

        private void GradientBlended_Click(object sender, EventArgs e)
        {
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.MakeGradientBlend(initImage, postImage);
            Invalidate();
        }

        private void colorChannelSwap_Click(object sender, EventArgs e)
        {

            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.ColorSwap(initImage, postImage);
            Invalidate();
        }

        private void sobelOption_Click(object sender, EventArgs e)
        {
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.MakeSobelFilter(initImage, postImage);
            Invalidate();
        }

 
        private void rotateImage_Click(object sender, EventArgs e)
        {
            double angle = 30;
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);

            double x = -1;
            double  y= -1;
            if (showDialog("Enter information", "angle:", ref angle, ref x, ref y) == DialogResult.OK)
            {
                GenerateImage.rotateImage(initImage, postImage, angle);
            }
            Invalidate();
        }

        private void gaussianBlur_Click(object sender, EventArgs e)
        {
            double width = 3;
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            double x = 2.1;
            double y = -1;
            if (showDialog("Enter information", "Width:", ref width, ref x, ref y) == DialogResult.OK)
            {
                GenerateImage.gaussianBlur(initImage, postImage, (int)width,x);
            }
            Invalidate();
        }

        private void differenceOverlay_Click(object sender, EventArgs e)
        {
            if (postImage == null)
                postImage = new RasterImage(initImage.Width, initImage.Height);
            GenerateImage.differenceOverlay(initImage, postImage);
            Invalidate();
        }
    }
}
