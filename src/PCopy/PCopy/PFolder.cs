using PCopy.LongFilenames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCopy
{
    internal class PFolder
    {
        public string SourcePath { get; set; }

        public string DestPath { get; set; }

        public PFolder(string sourcePath, string destPath)
        {
            SourcePath = sourcePath;
            DestPath = destPath;
        }

        public void Copy(Parameters parameters)
        {
            if (!LongDirectory.Exists(DestPath))
            {
                LongDirectory.CreateDirectory(DestPath);
            }

            if (parameters.Verbose) Console.Out.WriteLine("{0}", SourcePath);
            string[] sourceFilePaths = LongDirectory.GetFiles(SourcePath);
            string[] destFilePaths = LongDirectory.GetFiles(DestPath);

            foreach (string sourceFilePath in sourceFilePaths)
            {
                string copyFileComment = string.Empty;
                bool copyFile = false;
                string sourceFilename = LongFile.GetName(sourceFilePath);
                string destFilePath = LongFile.Combine(DestPath, sourceFilename);
                if (destFilePaths.FirstOrDefault(f => f.Equals(destFilePath, StringComparison.CurrentCultureIgnoreCase)) == null)
                {
                    copyFile = true;
                    copyFileComment = "new   ";
                }
                else
                {
                    DateTime sourceModified = LongFile.GetLastWriteTime(sourceFilePath);
                    DateTime destModified = LongFile.GetLastWriteTime(destFilePath);
                    int modComp = sourceModified.CompareTo(destModified);
                    if (modComp > 0)
                    {
                        copyFile = true;
                        copyFileComment = "newer ";
                    }
                    else if (modComp < 0)
                    {
                        copyFile = true;
                        copyFileComment = "older ";
                    }
                    else
                    {
                        copyFileComment = "same  ";
                    }

                }

                if (copyFile)
                {
                    if (parameters.Verbose) Console.Out.WriteLine("     {0} : {1} ", copyFileComment, sourceFilename);
                    try
                    {
                        LongFile.Copy(sourceFilePath, destFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine("{0}", ex.Message);
                    }
                }
            }

            if (parameters.Recursive)
            {
                string[] sourceDirs = LongDirectory.GetDirectories(SourcePath);
                foreach (string sourceDir in sourceDirs)
                {
                    string sourceName = LongFile.GetName(sourceDir);
                    string destDir = LongFile.Combine(DestPath, sourceName);
                    if (!LongDirectory.Exists(destDir))
                    {
                        LongDirectory.CreateDirectory(destDir);
                    }

                    PFolder pFolder = new PFolder(sourceDir, destDir);
                    pFolder.Copy(parameters);
                }
            }

            if (SourcePath == parameters.SourceDirectory)
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
