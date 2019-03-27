using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDelete
{
    class Parameters
    {
        /// <summary>
        /// The path to delete
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Whether subdirectories should also be copied
        /// </summary>
        public bool Recursive { get; set; }

        /// <summary>
        /// Whether the progress should be shown
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
            Recursive = false;
            Verbose = false;
            RequireEnterToExit = false;
            Path = string.Empty;

            foreach (string arg in args)
            {
                if (arg == "-r")
                {
                    Recursive = true;
                }
                else if (arg == "-e")
                {
                    RequireEnterToExit = true;
                }
                else if (arg == "-v")
                {
                    Verbose = true;
                }
                else if (string.IsNullOrEmpty(Path))
                {
                    Path = arg;
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
                return !string.IsNullOrWhiteSpace(Path);
            }
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
                sb.AppendLine("  pdelete <dir> [-r] [-e]");
                sb.AppendLine("");
                sb.AppendLine("<dir>   Specifies the directory to delete");
                sb.AppendLine("-r      If specified, recursively deletes all sub directories aswell.");
                sb.AppendLine("-e      If specified, the user must press enter once ");
                sb.AppendLine("        the delete is finished to exit the application.");
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
