using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDelete
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Parameters parameters = new Parameters(args);
                if (!parameters.Valid)
                {
                    Console.Out.WriteLine(Parameters.Syntax);
                    return 1;
                }
                else
                {
                    PFolder folder = new PFolder(parameters.Path);
                    folder.Delete(parameters);
                }
            }
            catch (InvalidParameterException ex)
            {
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(Parameters.Syntax);
                return 1;
            }
            return 0;
        }
    }
}
