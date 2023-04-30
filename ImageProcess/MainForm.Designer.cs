namespace ImageProcess
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openTestMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillWhiteMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.drawMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fillGreenOption = new System.Windows.Forms.ToolStripMenuItem();
            this.dimOption = new System.Windows.Forms.ToolStripMenuItem();
            this.tintOption = new System.Windows.Forms.ToolStripMenuItem();
            this.processMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.thresholdMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.warpNearestMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.warpBilenearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lowpassOption = new System.Windows.Forms.ToolStripMenuItem();
            this.makeSquareOption = new System.Windows.Forms.ToolStripMenuItem();
            this.coreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpen = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelOption = new System.Windows.Forms.ToolStripMenuItem();
            this.drawCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientBlended = new System.Windows.Forms.ToolStripMenuItem();
            this.basicProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianBlur = new System.Windows.Forms.ToolStripMenuItem();
            this.featureExtractionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.red = new System.Windows.Forms.ToolStripMenuItem();
            this.blue = new System.Windows.Forms.ToolStripMenuItem();
            this.green = new System.Windows.Forms.ToolStripMenuItem();
            this.colorChannelSwap = new System.Windows.Forms.ToolStripMenuItem();
            this.nonLinearWarpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diamond = new System.Windows.Forms.ToolStripMenuItem();
            this.linearWarpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateImage = new System.Windows.Forms.ToolStripMenuItem();
            this.overlaysAndBlendsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.differenceOverlay = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.processMenu,
            this.coreToolStripMenuItem,
            this.basicProcessingToolStripMenuItem,
            this.featureExtractionToolStripMenuItem,
            this.nonLinearWarpsToolStripMenuItem,
            this.linearWarpsToolStripMenuItem,
            this.overlaysAndBlendsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenu,
            this.openTestMenu,
            this.openMenu,
            this.saveMenu,
            this.closeMenu,
            this.exitMenu});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newMenu
            // 
            this.newMenu.Name = "newMenu";
            this.newMenu.Size = new System.Drawing.Size(193, 34);
            this.newMenu.Text = "&New";
            this.newMenu.Click += new System.EventHandler(this.newMenu_Click);
            // 
            // openTestMenu
            // 
            this.openTestMenu.Name = "openTestMenu";
            this.openTestMenu.Size = new System.Drawing.Size(193, 34);
            this.openTestMenu.Text = "Open &Test";
            this.openTestMenu.Click += new System.EventHandler(this.openTestMenu_Click);
            // 
            // openMenu
            // 
            this.openMenu.Name = "openMenu";
            this.openMenu.Size = new System.Drawing.Size(193, 34);
            this.openMenu.Text = "&Open";
            this.openMenu.Click += new System.EventHandler(this.openMenu_Click);
            // 
            // saveMenu
            // 
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.Size = new System.Drawing.Size(193, 34);
            this.saveMenu.Text = "&Save";
            this.saveMenu.Click += new System.EventHandler(this.SaveMenu_Click);
            // 
            // closeMenu
            // 
            this.closeMenu.Name = "closeMenu";
            this.closeMenu.Size = new System.Drawing.Size(193, 34);
            this.closeMenu.Text = "&Close";
            this.closeMenu.Click += new System.EventHandler(this.closeMenu_Click);
            // 
            // exitMenu
            // 
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.Size = new System.Drawing.Size(193, 34);
            this.exitMenu.Text = "&Exit";
            this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillWhiteMenu,
            this.negativeMenu,
            this.toolStripSeparator1,
            this.drawMenu,
            this.fillGreenOption,
            this.dimOption,
            this.tintOption});
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(88, 29);
            this.generateToolStripMenuItem.Text = "&Process";
            // 
            // fillWhiteMenu
            // 
            this.fillWhiteMenu.Name = "fillWhiteMenu";
            this.fillWhiteMenu.Size = new System.Drawing.Size(186, 34);
            this.fillWhiteMenu.Text = "&Fill White";
            this.fillWhiteMenu.Click += new System.EventHandler(this.fillWhiteMenu_Click);
            // 
            // negativeMenu
            // 
            this.negativeMenu.Name = "negativeMenu";
            this.negativeMenu.Size = new System.Drawing.Size(186, 34);
            this.negativeMenu.Text = "&Negative";
            this.negativeMenu.Click += new System.EventHandler(this.negativeMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // drawMenu
            // 
            this.drawMenu.Name = "drawMenu";
            this.drawMenu.Size = new System.Drawing.Size(186, 34);
            this.drawMenu.Text = "&Draw";
            this.drawMenu.Click += new System.EventHandler(this.drawMenu_Click);
            // 
            // fillGreenOption
            // 
            this.fillGreenOption.Name = "fillGreenOption";
            this.fillGreenOption.Size = new System.Drawing.Size(186, 34);
            this.fillGreenOption.Text = "Fill Green";
            this.fillGreenOption.Click += new System.EventHandler(this.fillGreenOption_Click);
            // 
            // dimOption
            // 
            this.dimOption.Name = "dimOption";
            this.dimOption.Size = new System.Drawing.Size(186, 34);
            this.dimOption.Text = "Dim";
            this.dimOption.Click += new System.EventHandler(this.dimOption_Click);
            // 
            // tintOption
            // 
            this.tintOption.Name = "tintOption";
            this.tintOption.Size = new System.Drawing.Size(186, 34);
            this.tintOption.Text = "Tint";
            this.tintOption.Click += new System.EventHandler(this.tintOption_Click);
            // 
            // processMenu
            // 
            this.processMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenu,
            this.toolStripSeparator2,
            this.thresholdMenu,
            this.warpNearestMenu,
            this.warpBilenearMenu,
            this.lowpassOption,
            this.makeSquareOption});
            this.processMenu.Name = "processMenu";
            this.processMenu.Size = new System.Drawing.Size(98, 29);
            this.processMenu.Text = "&Generate";
            // 
            // copyMenu
            // 
            this.copyMenu.Name = "copyMenu";
            this.copyMenu.Size = new System.Drawing.Size(222, 34);
            this.copyMenu.Text = "&Copy";
            this.copyMenu.Click += new System.EventHandler(this.copyMenu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // thresholdMenu
            // 
            this.thresholdMenu.Name = "thresholdMenu";
            this.thresholdMenu.Size = new System.Drawing.Size(222, 34);
            this.thresholdMenu.Text = "&Threshold";
            this.thresholdMenu.Click += new System.EventHandler(this.thresholdMenu_Click);
            // 
            // warpNearestMenu
            // 
            this.warpNearestMenu.Name = "warpNearestMenu";
            this.warpNearestMenu.Size = new System.Drawing.Size(222, 34);
            this.warpNearestMenu.Text = "&Warp Nearest";
            this.warpNearestMenu.Click += new System.EventHandler(this.warpNearestMenu_Click);
            // 
            // warpBilenearMenu
            // 
            this.warpBilenearMenu.Name = "warpBilenearMenu";
            this.warpBilenearMenu.Size = new System.Drawing.Size(222, 34);
            this.warpBilenearMenu.Text = "Warp &Bilenear";
            this.warpBilenearMenu.Click += new System.EventHandler(this.warpBilenearMenu_Click);
            // 
            // lowpassOption
            // 
            this.lowpassOption.Name = "lowpassOption";
            this.lowpassOption.Size = new System.Drawing.Size(222, 34);
            this.lowpassOption.Text = "Lowpass";
            this.lowpassOption.Click += new System.EventHandler(this.lowpassOption_Click);
            // 
            // makeSquareOption
            // 
            this.makeSquareOption.Name = "makeSquareOption";
            this.makeSquareOption.Size = new System.Drawing.Size(222, 34);
            this.makeSquareOption.Text = "Make Square";
            this.makeSquareOption.Click += new System.EventHandler(this.makeSquareOption_Click);
            // 
            // coreToolStripMenuItem
            // 
            this.coreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sharpen,
            this.sobelOption,
            this.drawCircle,
            this.horizontalFlipToolStripMenuItem,
            this.gradientBlended});
            this.coreToolStripMenuItem.Name = "coreToolStripMenuItem";
            this.coreToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.coreToolStripMenuItem.Text = "Core";
            // 
            // sharpen
            // 
            this.sharpen.Name = "sharpen";
            this.sharpen.Size = new System.Drawing.Size(249, 34);
            this.sharpen.Text = "Sharpen";
            this.sharpen.Click += new System.EventHandler(this.sharpen_Click);
            // 
            // sobelOption
            // 
            this.sobelOption.Name = "sobelOption";
            this.sobelOption.Size = new System.Drawing.Size(249, 34);
            this.sobelOption.Text = "Sobel";
            this.sobelOption.Click += new System.EventHandler(this.sobelOption_Click);
            // 
            // drawCircle
            // 
            this.drawCircle.Name = "drawCircle";
            this.drawCircle.Size = new System.Drawing.Size(249, 34);
            this.drawCircle.Text = "Circle";
            this.drawCircle.Click += new System.EventHandler(this.drawCircle_Click);
            // 
            // horizontalFlipToolStripMenuItem
            // 
            this.horizontalFlipToolStripMenuItem.Name = "horizontalFlipToolStripMenuItem";
            this.horizontalFlipToolStripMenuItem.Size = new System.Drawing.Size(249, 34);
            this.horizontalFlipToolStripMenuItem.Text = "Affine";
            this.horizontalFlipToolStripMenuItem.Click += new System.EventHandler(this.makeAffineOption_Click);
            // 
            // gradientBlended
            // 
            this.gradientBlended.Name = "gradientBlended";
            this.gradientBlended.Size = new System.Drawing.Size(249, 34);
            this.gradientBlended.Text = "Blended gradient";
            this.gradientBlended.Click += new System.EventHandler(this.GradientBlended_Click);
            // 
            // basicProcessingToolStripMenuItem
            // 
            this.basicProcessingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gaussianBlur});
            this.basicProcessingToolStripMenuItem.Name = "basicProcessingToolStripMenuItem";
            this.basicProcessingToolStripMenuItem.Size = new System.Drawing.Size(67, 29);
            this.basicProcessingToolStripMenuItem.Text = "Basic";
            // 
            // gaussianBlur
            // 
            this.gaussianBlur.Name = "gaussianBlur";
            this.gaussianBlur.Size = new System.Drawing.Size(219, 34);
            this.gaussianBlur.Text = "Gaussian Blur";
            this.gaussianBlur.Click += new System.EventHandler(this.gaussianBlur_Click);
            // 
            // featureExtractionToolStripMenuItem
            // 
            this.featureExtractionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelFilter,
            this.colorChannelSwap});
            this.featureExtractionToolStripMenuItem.Name = "featureExtractionToolStripMenuItem";
            this.featureExtractionToolStripMenuItem.Size = new System.Drawing.Size(168, 29);
            this.featureExtractionToolStripMenuItem.Text = "Feature Extraction";
            // 
            // channelFilter
            // 
            this.channelFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.red,
            this.blue,
            this.green});
            this.channelFilter.Name = "channelFilter";
            this.channelFilter.Size = new System.Drawing.Size(252, 34);
            this.channelFilter.Text = "Channel Filter";
            // 
            // red
            // 
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(160, 34);
            this.red.Text = "Red";
            this.red.Click += new System.EventHandler(this.ChannelFilter_Click);
            // 
            // blue
            // 
            this.blue.Name = "blue";
            this.blue.Size = new System.Drawing.Size(160, 34);
            this.blue.Text = "Blue";
            this.blue.Click += new System.EventHandler(this.ChannelFilter_Click);
            // 
            // green
            // 
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(160, 34);
            this.green.Text = "Green";
            this.green.Click += new System.EventHandler(this.ChannelFilter_Click);
            // 
            // colorChannelSwap
            // 
            this.colorChannelSwap.Name = "colorChannelSwap";
            this.colorChannelSwap.Size = new System.Drawing.Size(252, 34);
            this.colorChannelSwap.Text = "BG Channel Swap";
            this.colorChannelSwap.Click += new System.EventHandler(this.colorChannelSwap_Click);
            // 
            // nonLinearWarpsToolStripMenuItem
            // 
            this.nonLinearWarpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diamond});
            this.nonLinearWarpsToolStripMenuItem.Name = "nonLinearWarpsToolStripMenuItem";
            this.nonLinearWarpsToolStripMenuItem.Size = new System.Drawing.Size(168, 29);
            this.nonLinearWarpsToolStripMenuItem.Text = "Non Linear Warps";
            // 
            // diamond
            // 
            this.diamond.Name = "diamond";
            this.diamond.Size = new System.Drawing.Size(188, 34);
            this.diamond.Text = "Diamond";
            this.diamond.Click += new System.EventHandler(this.diamond_Click);
            // 
            // linearWarpsToolStripMenuItem
            // 
            this.linearWarpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateImage});
            this.linearWarpsToolStripMenuItem.Name = "linearWarpsToolStripMenuItem";
            this.linearWarpsToolStripMenuItem.Size = new System.Drawing.Size(129, 29);
            this.linearWarpsToolStripMenuItem.Text = "Linear Warps";
            // 
            // rotateImage
            // 
            this.rotateImage.Name = "rotateImage";
            this.rotateImage.Size = new System.Drawing.Size(304, 34);
            this.rotateImage.Text = "Rotate upper-left corner";
            this.rotateImage.Click += new System.EventHandler(this.rotateImage_Click);
            // 
            // overlaysAndBlendsToolStripMenuItem
            // 
            this.overlaysAndBlendsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.differenceOverlay});
            this.overlaysAndBlendsToolStripMenuItem.Name = "overlaysAndBlendsToolStripMenuItem";
            this.overlaysAndBlendsToolStripMenuItem.Size = new System.Drawing.Size(188, 29);
            this.overlaysAndBlendsToolStripMenuItem.Text = "Overlays and Blends";
            this.overlaysAndBlendsToolStripMenuItem.Click += new System.EventHandler(this.differenceOverlay_Click);
            // 
            // differenceOverlay
            // 
            this.differenceOverlay.Name = "differenceOverlay";
            this.differenceOverlay.Size = new System.Drawing.Size(259, 34);
            this.differenceOverlay.Text = "Difference Overlay";
            this.differenceOverlay.Click += new System.EventHandler(this.differenceOverlay_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png|BMP File" +
    "s (*.bmp)|*p.bmp|All files (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png|BMP File" +
    "s (*.bmp)|*.bmp";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Project 1: Patnekar Prarthavi";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMenu;
        private System.Windows.Forms.ToolStripMenuItem openTestMenu;
        private System.Windows.Forms.ToolStripMenuItem openMenu;
        private System.Windows.Forms.ToolStripMenuItem saveMenu;
        private System.Windows.Forms.ToolStripMenuItem closeMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.ToolStripMenuItem processMenu;
        private System.Windows.Forms.ToolStripMenuItem thresholdMenu;
        private System.Windows.Forms.ToolStripMenuItem warpNearestMenu;
        private System.Windows.Forms.ToolStripMenuItem warpBilenearMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem copyMenu;
        private System.Windows.Forms.ToolStripMenuItem lowpassOption;
        private System.Windows.Forms.ToolStripMenuItem makeSquareOption;
        private System.Windows.Forms.ToolStripMenuItem coreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpen;
        private System.Windows.Forms.ToolStripMenuItem sobelOption;
        private System.Windows.Forms.ToolStripMenuItem drawCircle;
        private System.Windows.Forms.ToolStripMenuItem gradientBlended;
        private System.Windows.Forms.ToolStripMenuItem horizontalFlipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem featureExtractionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelFilter;
        private System.Windows.Forms.ToolStripMenuItem red;
        private System.Windows.Forms.ToolStripMenuItem blue;
        private System.Windows.Forms.ToolStripMenuItem green;
        private System.Windows.Forms.ToolStripMenuItem nonLinearWarpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diamond;
        private System.Windows.Forms.ToolStripMenuItem colorChannelSwap;
        private System.Windows.Forms.ToolStripMenuItem basicProcessingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearWarpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overlaysAndBlendsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillWhiteMenu;
        private System.Windows.Forms.ToolStripMenuItem negativeMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem drawMenu;
        private System.Windows.Forms.ToolStripMenuItem fillGreenOption;
        private System.Windows.Forms.ToolStripMenuItem dimOption;
        private System.Windows.Forms.ToolStripMenuItem tintOption;
        private System.Windows.Forms.ToolStripMenuItem differenceOverlay;
        private System.Windows.Forms.ToolStripMenuItem rotateImage;
        private System.Windows.Forms.ToolStripMenuItem gaussianBlur;
    }
}

