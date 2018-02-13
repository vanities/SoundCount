using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

/*  This program is a console application that executes the python interpreter
    and runs the python script specified below. Then request a (audio) file 
    location from the user as a character string and delivers this parameter to the script.   */

namespace ExecMeasuringSpeech
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                //variables to pass to python
                //request file path of audio file to process
                Console.WriteLine("Enter path of audio file to process (or drag & drop into terminal window).\n\n\tExample format:\n\tC:\\Users\\User\\Downloads\\audio.wav\n");
                string fileToProcess = Console.ReadLine();


                //some exception handling checking file validity would go here


                //path of program to be executed
                string progToRun = "T:\\SoftEng\\Measuring-Speech\\test.py";
                char[] splitter = { '\r' };     //delimits captured output from python

                //initalize python process
                Process proc = new Process();
                proc.StartInfo.FileName = "python.exe";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;

                // call file.py to concatenate passed parameters
                //access variables in python with sys.argv[n]
                //sys.argv[0] is name of script, n>0 are any arguments passed (just like in C/C++)
                proc.StartInfo.Arguments = string.Concat(progToRun, " ", fileToProcess);
                proc.Start();

                //read redirected stdout from python script
                StreamReader sReader = proc.StandardOutput;
                string[] output = sReader.ReadToEnd().Split(splitter);

                //write captured output to console window
                Console.WriteLine("\nPython responded with:\n");
                foreach (string s in output)
                    Console.Write(s);

                //waits for python process to finish executing (I think, need to check on this)
                proc.WaitForExit();

                Console.WriteLine("\n\n\nPress Enter to continue or Q to quit.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Q);
        }
    }
}
