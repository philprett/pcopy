using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCopy
{
    class Parameters
    {
        /// <summary>
        /// The source directory 
        /// </summary>
        public string SourceDirectory { get; set; }

        /// <summary>
        /// The destination directory
        /// </summary>
        public string DestinationDirectory { get; set; }

        /// <summary>
        /// Whether subdirectories should also be copied
        /// </summary>
        public bool Recursive { get; set; }

        /// <summary>
        /// Whether files in destination directory that are not in source are deleted.
        /// </summary>
        public bool Purge { get; set; }

        /// <summary>
        /// A list of strings which specifies which files and directories are to be excluded
        /// </summary>
        public List<string> Excludes { get; set; }

        /// <summary>
        /// Determines whether the equal files are listed
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Whether the user must press enter to exit the application.
        /// </summary>
        public bool RequireEnterToExit { get; set; }

        /// <summary>
        /// Constructor.
        /// Pass it the array of strings that Program.main receives.
        /// </summary>
        /// <param name="args"></param>
        public Parameters(string[] args)
        {
            Verbose = false;
            Purge = false;
            RequireEnterToExit = false;
            Excludes = new List<string>();
            SourceDirectory = string.Empty;
            DestinationDirectory = string.Empty;

            foreach (string arg in args)
            {
                if (arg.StartsWith("-x:"))
                {
                    Excludes.Add(arg.Substring(3));
                }
                else if (arg == "-r")
                {
                    Recursive = true;
                }
                else if (arg == "-p")
                {
                    Purge = true;
                }
                else if (arg == "-v")
                {
                    Verbose = true;
                }
                else if (arg == "-e")
                {
                    RequireEnterToExit = true;
                }
                else if (string.IsNullOrEmpty(SourceDirectory))
                {
                    SourceDirectory = arg;
                }
                else if (string.IsNullOrEmpty(DestinationDirectory))
                {
                    DestinationDirectory = arg;
                }
                else
                {
                    throw new InvalidParameterException(string.Format("The parameter \"{0}\" is unknown", arg));
                }
            }
        }

        /// <summary>
        /// Returns whether the parameters are valid to perform a copy
        /// </summary>
        public bool Valid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SourceDirectory) && !string.IsNullOrWhiteSpace(DestinationDirectory);
            }
        }

        /// <summary>
        /// Check if a filename/directoryname is excluded or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsExcluded(string path)
        {
            string lowerPath = path.ToLower();
            foreach (string exclude in Excludes)
            {
                if (lowerPath.Contains(exclude.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return a description of the parameters.
        /// </summary>
        public static string Syntax
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Syntax:");
                sb.AppendLine("");
                sb.AppendLine("  pcopy <source dir> <destination dir> [-x:<exclude path>] [-v] [-p]");
                sb.AppendLine("");
                sb.AppendLine("<source dir>   Specifies the source directory");
                sb.AppendLine("<dest dir>     Specified the destination directory");
                sb.AppendLine("<exclude path> Specifies the path to exclude");
                sb.AppendLine("-r             If specified, recursively copies all directories aswell.");
                sb.AppendLine("-v             Verbose output. Shows all equal files.");
                sb.AppendLine("-p             Purge. Delete files in destination directory that are not in source.");
                sb.AppendLine("-e             If specified, the user must press enter once ");
                sb.AppendLine("               the copy is finished to exit the application.");
                sb.AppendLine("");

                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// Exception class for when a parameter is unknown.
    /// </summary>
    class InvalidParameterException : Exception { public InvalidParameterException(string message) : base(message) { } }
}
