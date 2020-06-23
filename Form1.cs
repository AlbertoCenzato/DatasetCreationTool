using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DatasetCreationTool
{
    public partial class Form1 : Form
    {
        private DatasetHandler datasetHandler;
        private string saveTo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Desktop/output");
        private static readonly string ANNOTATIONS_FILE_NAME = "samples.txt";
        private StreamWriter annotationsStream;

        public Form1(DatasetHandler datasetHandler)
        {
            InitializeComponent();
            this.datasetHandler = datasetHandler;
            this.datasetHandler.SelectedImageChanged += OnSelectedImageChanged;
            numericUpDownRectHeight.Maximum = 1000;
            numericUpDownRectWidth.Maximum = 1000;
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                datasetHandler.OpenDirectory(dialog.SelectedPath);
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                datasetHandler.OpenAnnotationsFile(dialog.FileName);
        }

        private void OnSelectedImageChanged(object sender, EventArgs e)
        {
            pictureBoxWorkingImage.Image = datasetHandler.SelectedImage;
            statusStrip1.Items[0].Text = $"Image {datasetHandler.ImageIndex + 1} of {datasetHandler.ImagesFiles.Count}";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var key = e.KeyChar - 32;
            switch (key)
            {
                case (char)Keys.A:
                    datasetHandler.ImageIndex -= 1;
                    e.Handled = true;
                    break;
                case (char)Keys.D:
                    datasetHandler.ImageIndex += 1;
                    e.Handled = true;
                    break;
                case (char)Keys.E:
                    if (datasetHandler.SelectedRegion != Rectangle.Empty)
                    {
                        var savePath = datasetHandler.SaveSelectedPatch(saveTo);
                        annotationsStream.WriteLine(savePath);
                        annotationsStream.Flush();
                        datasetHandler.SelectedRegion = Rectangle.Empty;  // TODO: use a property to refresh each time selectedRegion changes
                        pictureBoxWorkingImage.Refresh();
                    }
                    e.Handled = true;
                    break;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                datasetHandler.SelectedRegion = Rectangle.Empty;
                pictureBoxWorkingImage.Refresh();
                e.Handled = true;
            }
        }

        private void pictureBoxWorkingImage_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBoxWorkingImage.Focus();
            if (pictureBoxWorkingImage.Image == null)
                return;

            var size = new Size((int)numericUpDownRectWidth.Value, (int)numericUpDownRectHeight.Value);
            var rectTopLeftCorner = FormCoordinatesToImageCoordinates(e.Location);
            rectTopLeftCorner.Offset((int)(-size.Width / 2), (int)(-size.Height / 2));

            datasetHandler.SelectedRegion = new Rectangle(rectTopLeftCorner, size);
            
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
            if (pictureBoxWorkingImage.Image == null)
                return;

            (float scale, Point offset) = GetImageTransform();

            Graphics g = e.Graphics;
            g.TranslateTransform(offset.X, offset.Y);
            g.ScaleTransform(scale, scale);
            using var pen = new Pen(Color.Red, 3);
            if (datasetHandler.SelectedRegion != null)
                g.DrawRectangle(pen, datasetHandler.SelectedRegion);
            pen.Color = Color.Green;
            foreach (var rect in datasetHandler.ImageRectangles)
                g.DrawRectangle(pen, rect);
        }

        

        private void buttonSaveTo_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                saveTo = dialog.SelectedPath;
                annotationsStream = File.AppendText(Path.Combine(saveTo, ANNOTATIONS_FILE_NAME));
            }

        }

        private void textBoxClass_Leave(object sender, EventArgs e)
        {
            datasetHandler.CurrentClass = textBoxClass.Text;
            if (!textBoxClass.AutoCompleteCustomSource.Contains(datasetHandler.CurrentClass))
                textBoxClass.AutoCompleteCustomSource.Add(datasetHandler.CurrentClass);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            annotationsStream?.Dispose();
        }
    }
}
