using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DatasetCreationTool
{
    public partial class Form1 : Form
    {
        private DatasetHandler datasetHandler;
        private List<string> images;
        private Int32 imageIndex = 0;
        private Rectangle selectedRegion = Rectangle.Empty;
        private string saveTo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Desktop/output");
        private Int32 croppedImages = 0;

        public Int32 ImageIndex
        {
            get { return imageIndex; }
            set
            {
                if (value >= images.Count)
                    imageIndex = images.Count - 1;
                else if (value < 0)
                    imageIndex = 0;
                else
                    imageIndex = value;

                if (images.Any())
                {
                    statusStrip1.Items[0].Text = $"Image {imageIndex + 1} of {images.Count}";
                    var oldImage = pictureBoxWorkingImage.Image;
                    selectedRegion = Rectangle.Empty;
                    pictureBoxWorkingImage.Image = Image.FromFile(images[imageIndex]);
                    oldImage?.Dispose();
                }
            }
        }

        public Form1(DatasetHandler datasetHandler)
        {
            this.datasetHandler = datasetHandler;
            this.datasetHandler.InputFilesUpdated += OnInputFilesUpdated;
            InitializeComponent();
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    datasetHandler.OpenDirectory(dialog.SelectedPath);
            }
        }

        private void OnInputFilesUpdated(object sender, InputFilesUpdatedArgs e)
        {
            images = e.Files.ToList();
            ImageIndex = 0;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var key = e.KeyChar - 32;
            switch (key)
            {
                case (char)Keys.A:
                    ImageIndex = ImageIndex - 1;
                    e.Handled = true;
                    break;
                case (char)Keys.D:
                    ImageIndex = ImageIndex + 1;
                    e.Handled = true;
                    break;
                case (char)Keys.E:
                    if (selectedRegion != Rectangle.Empty)
                    {
                        CropRegion(selectedRegion);
                        selectedRegion = Rectangle.Empty;  // TODO: use a property to refresh each time selectedRegion changes
                        pictureBoxWorkingImage.Refresh();
                    }
                    break;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                selectedRegion = Rectangle.Empty;
                pictureBoxWorkingImage.Refresh();
            }
        }

        private void pictureBoxWorkingImage_MouseClick(object sender, MouseEventArgs e)
        {
            var image = pictureBoxWorkingImage.Image;
            if (image == null)
                return;

            var size = new Size((int)numericUpDownRectWidth.Value, (int)numericUpDownRectHeight.Value);
            var rectTopLeftCorner = FormCoordinatesToImageCoordinates(e.Location);
            rectTopLeftCorner.Offset((int)(-size.Width / 2), (int)(-size.Height / 2));

            selectedRegion = new Rectangle(rectTopLeftCorner, size);
            
            pictureBoxWorkingImage.Refresh();
        }

        private Point FormCoordinatesToImageCoordinates(Point point)
        {
            (float scale, Point offset) = GetImageTransform();
            point.Offset(new Point(-offset.X, -offset.Y));
            return new Point((int)(point.X / scale), (int)(point.Y / scale));
        }

        private (float scale, Point offset) GetImageTransform()
        {
            var image = pictureBoxWorkingImage.Image;
            if (image == null)
                return (1, new Point(0,0));

            Size size;
            var imgAspectRatio = image.Height / (float)image.Width;
            var pbAspectRatio = pictureBoxWorkingImage.ClientSize.Height / (float)pictureBoxWorkingImage.ClientSize.Width;
            if (imgAspectRatio > pbAspectRatio)  // image shape is taller than pb
                size = new Size((int)(pictureBoxWorkingImage.ClientSize.Height / imgAspectRatio), pictureBoxWorkingImage.ClientSize.Height);
            else // image shape is wider than pb
                size = new Size(pictureBoxWorkingImage.ClientSize.Width, (int)(pictureBoxWorkingImage.ClientSize.Width * imgAspectRatio));

            var scale = size.Width / pictureBoxWorkingImage.Image.PhysicalDimension.Width;
            var offset = new Point((pictureBoxWorkingImage.ClientSize.Width - size.Width) / 2,
                                   (pictureBoxWorkingImage.ClientSize.Height - size.Height) / 2);
            return (scale, offset);
        }

        private void pictureBoxWorkingImage_Paint(object sender, PaintEventArgs e)
        {
            if (selectedRegion == Rectangle.Empty || pictureBoxWorkingImage.Image == null)
                return;

            (float scale, Point offset) = GetImageTransform();

            Graphics g = e.Graphics;
            g.TranslateTransform(offset.X, offset.Y);
            g.ScaleTransform(scale, scale);
            using var pen = new Pen(Color.Red, 3);
            g.DrawRectangle(pen, selectedRegion);
        }

        private void CropRegion(Rectangle croppingRegion)
        {
            var bmp = new Bitmap(croppingRegion.Width, croppingRegion.Height);
            using (var graphics = Graphics.FromImage(bmp))
                graphics.DrawImage(pictureBoxWorkingImage.Image, new Rectangle(0, 0, bmp.Width, bmp.Height), croppingRegion, GraphicsUnit.Pixel);

            var path = Path.Combine(saveTo, $"{textBoxClass.Text}_{croppedImages++}.jpg");
            bmp.Save(path, ImageFormat.Jpeg);
            bmp.Dispose();
        }

        private void buttonSaveTo_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                saveTo = dialog.SelectedPath;
        }
    }
}
