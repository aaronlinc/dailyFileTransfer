//Aaron Lincoln

//Transfer files not older than 24 hours from source
//folder to the destination folder.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dailyFileTransfer
{
    class fileTransfer
    {
        static void Main(string[] args)
        {

            fileTransfer fileTrans = new fileTransfer();
            //Transfer files not older than 24 hours from source
            //folder to the destination folder.
            string oldFilePath = "OldFiles";
            string destination = "Destination";


            int numTrans = fileTrans.moveFiles(oldFilePath, destination);
            Console.WriteLine("{0} Files Successfully Transfered.", numTrans);

            //Wait for user input to exit.
            Console.WriteLine("Press Enter to Exit...");
            Console.Read();
        }

        // Returns true if the file passed in has been
        // created or modified in the last 24 hours
        // otherwise returns false.
        private bool isNewFile(string path)
        {
            var timeModified = File.GetLastWriteTime(path);
            var threshold = DateTime.Now.AddHours(-24);
            if (timeModified >= threshold)
                return true;                
            else
                return false; 
        }

        // Moves file from source path to destination
        // path. If it is less than 24 hours old
        // Returns Number of Files Transfered. 
        public int moveFiles(string source, string destination)
        {
            int counter = 0;
            source = Path.GetFullPath(source);
            destination = Path.GetFullPath(destination);
            if (Directory.Exists(source))
            {
                string[] files = Directory.GetFiles(source);
                foreach(string s in files)
                {
                    
                    string fileName = (s);
                    FileInfo filePath = new FileInfo(fileName);
                    if (isNewFile(fileName))
                    {
                        string destinationFile = Path.Combine(destination, (Path.GetFileName(fileName)));
                        if (File.Exists(destinationFile))
                        {
                            File.Delete(destinationFile);
                            destinationFile = Path.Combine(destination, (Path.GetFileName(fileName)));
                        }
                        
                        filePath.MoveTo(destinationFile);
                        counter++;
                    }
                }
            }
            return counter;
        }

        
    }
}
