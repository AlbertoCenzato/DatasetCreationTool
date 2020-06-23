using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace DatasetCreationTool
{
    public class InputFilesUpdatedArgs
    {
        public InputFilesUpdatedArgs(List<string> files) { Files = files; }
        public List<string> Files { get; }
    }

    public class DatasetHandler
    {
        private Int32 imageIndex;
        public Rectangle SelectedRegion = Rectangle.Empty;
        public Image SelectedImage;

        private string currentClass;

        public string CurrentClass
        {
            get { return currentClass; }
            set
            {
                currentClass = value;
                if (!CroppedImagesPerClass.ContainsKey(currentClass))
                    CroppedImagesPerClass[currentClass] = 0;
            }
        }

        public Dictionary<string, Int64> CroppedImagesPerClass;

        public delegate void InputFilesUpdatedHandler(object sender, InputFilesUpdatedArgs e);
        public delegate void SelectedImageChangedHandler(object sender, EventArgs e);

        public event InputFilesUpdatedHandler InputFilesUpdated;
        public event SelectedImageChangedHandler SelectedImageChanged;

        public DatasetHandler()
        {
            CroppedImagesPerClass = new Dictionary<string, Int64>();
        }


        public Int32 ImageIndex
        {
            get { return imageIndex; }
            set
            {
                if (value >= ImagesFiles.Count)
                    imageIndex = ImagesFiles.Count - 1;
                else if (value < 0)
                    imageIndex = 0;
                else
                    imageIndex = value;

                if (ImagesFiles.Any())
                {
                    var oldImage = SelectedImage;
                    SelectedRegion = Rectangle.Empty;
                    SelectedImage = Image.FromFile(ImagesFiles[imageIndex]);
                    oldImage?.Dispose();
                    SelectedImageChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public List<string> ImagesFiles { get; private set; }

        public bool OpenDirectory(string path)
        {
            if (!Directory.Exists(path))
                return false;

            ImagesFiles = Directory.EnumerateFiles(path, "*")
                                  .Where(f => IsSupportedFormat(new FileInfo(f).Extension))
                                  .ToList();
            bool result = ImagesFiles.Any();
            if (result)
                ImageIndex = 0;
            return result;
        }

        public async Task<bool> OpenAnnotationsFile(string path)
        {
            if (!File.Exists(path))
                return false;

            static (string, List<Rectangle>) ParseLine(string text)
            {
                var index = Regex.Match(text, @"\.[a-z]{3} [0-9]").Index;
                var path = text.Substring(0, index + 4);

                var annotationData = text.Substring(index + 4)
                                         .Split(" ")
                                         .Where(str => str.Length != 0)
                                         .Select(str => Int32.Parse(str))
                                         .ToArray();

                if ((annotationData.Length - 1) / 4 != annotationData[0])
                    throw new Exception("Invalid format!");

                var rectangles = new List<Rectangle>();
                for (int i = 0; i < annotationData[0]; i++)
                {
                    int x      = annotationData[i * 4 + 1];
                    int y      = annotationData[i * 4 + 2];
                    int width  = annotationData[i * 4 + 3];
                    int height = annotationData[i * 4 + 4];
                    var rect = new Rectangle(x, y, width, height);
                    rectangles.Add(rect);
                }

                return (path, rectangles);
            }

            var lines = await File.ReadAllLinesAsync(path);
            var tmp = lines.Select(ParseLine)
                           .Where(tuple => IsSupportedFormat(new FileInfo(tuple.Item1).Extension));
            ImagesFiles = tmp.Select(tuple => tuple.Item1)
                             .ToList();
            bool result = ImagesFiles.Any();
            if (result)
                ImageIndex = 0;
            return result;
        }

        public static bool IsSupportedFormat(string extension)
        {
            return extension == ".jpg" ||
                   extension == ".bmp" ||
                   extension == ".png" ;
        }

        public string SaveSelectedPatch(string saveDir)
        {
            using var bmp = new Bitmap(SelectedRegion.Width, SelectedRegion.Height);
            using (var graphics = Graphics.FromImage(bmp))
                graphics.DrawImage(SelectedImage, new Rectangle(0, 0, bmp.Width, bmp.Height), SelectedRegion, GraphicsUnit.Pixel);

            var path = Path.Combine(saveDir, $"{CurrentClass}_{CroppedImagesPerClass[CurrentClass]++}.jpg");
            bmp.Save(path, ImageFormat.Jpeg);
            return path;
        }

    }
}
