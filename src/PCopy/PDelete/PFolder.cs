using PCopy.LongFilenames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDelete
{
    internal class PFolder
    {
        public string Path { get; set; }

        public PFolder(string path)
        {
            Path = path;
        }

        public void Delete(Parameters parameters)
        {
            if (LongDirectory.Exists(Path))
            {
                string[] paths = LongDirectory.GetFiles(Path);
                foreach (string filePath in paths)
                {
                    if (parameters.Verbose) Console.Out.WriteLine("{0}", filePath);
                    LongFile.Delete(filePath);
                }
                if (parameters.Recursive)
                {
                    string[] subDirs = LongDirectory.GetDirectories(Path);
                    foreach (string subDir in subDirs)
                    {
                        PFolder pFolder = new PFolder(subDir);
                        pFolder.Delete(parameters);
                    }
                }
                if (parameters.Verbose) Console.Out.WriteLine("{0}", Path);
                LongDirectory.Delete(Path);
            }
            else if (LongFile.Exists(Path))
            {
                if (parameters.Verbose) Console.Out.WriteLine("{0}", Path);
                    LongFile.Delete(Path);
            }

            if (Path == parameters.Path)
            {
                if (parameters.RequireEnterToExit)
                {
                    Console.Out.WriteLine("Press enter to exit");
                    Console.In.ReadLine();
                }
            }
        }
    }
}
