using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DatasetCreationTool
{
    public class InputFilesUpdatedArgs
    {
        public InputFilesUpdatedArgs(IEnumerable<string> files) { Files = files; }
        public IEnumerable<string> Files { get; }
    }

    public class DatasetHandler
    {
        public delegate void InputFilesUpdatedHandler(object sender, InputFilesUpdatedArgs e);

        public event InputFilesUpdatedHandler InputFilesUpdated;

        public IEnumerable<string> InputFiles { get; private set; }

        public bool OpenDirectory(string path)
        {
            if (!Directory.Exists(path))
                return false;

            InputFiles = Directory.EnumerateFiles(path, "*")
                                  .Where(f => IsSupportedFormat(new FileInfo(f).Extension));
            bool result = InputFiles.Any();
            InputFilesUpdated?.Invoke(this, new InputFilesUpdatedArgs(InputFiles));
            return result;
        }

        public static bool IsSupportedFormat(string extension)
        {
            return extension == ".jpg" ||
                   extension == ".bmp" ||
                   extension == ".png" ;
        }

    }
}
