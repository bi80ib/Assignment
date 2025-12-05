using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment
{
    public class Logger
    {
        private static Logger instance;
        private static string logFile = "log.txt";
        private Logger() { }
        public static Logger GetInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }
        public void Log(string message)
        {
            string entry = ($"Log: {message},{DateTime.Now.ToString("HH,mm,ss")}");

            File.AppendAllText(logFile, entry + " \n");

        }

    }

}
