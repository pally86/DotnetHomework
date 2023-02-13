using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Utility
{
    public static class TestHelper
    {
        public static bool IsFileLocked(FileInfo file, string path, string dictionary)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (!Directory.Exists(path)) 
            {
                if (sw.ElapsedMilliseconds > 4000)
                    throw new Exception("Problem with creating directory!");
            }
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Stop();
            sw.Reset();
            sw.Start();
            while (!File.Exists(dictionary)) 
            {
                if (sw.ElapsedMilliseconds > 4000)
                    throw new Exception("Problem with creating file!");
            }
            sw.Stop();
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }

        public static void DeleteFilesAfterTest(string path)
        {
            foreach (var item in Directory.GetFiles(path))
            {
                File.Delete(item);
            };
            Directory.Delete(path);
        }
    }
}
