using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReplacWords.Lib
{
    class Search
    {
        const int size = 10;
        string[] forbiddenWords = new string[size];
        static string docsPath =
             Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string controlFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ForbiddenFiles";

        // конструктор по умолчанию
        public Search() { }

        public Search(string anotherPath) { docsPath = anotherPath; }

        public void StartSearch()
        {

            var Files = Directory.EnumerateFiles(docsPath, "*.txt", SearchOption.AllDirectories);


            foreach (string currentFile in Files)
            {

                string line;
                StreamReader f = new StreamReader(currentFile);
                while ((line = f.ReadLine()) != null)
                {
                    for(int i = 0; i < size; i++)
                    {
                        if(line.Contains(forbiddenWords[i]))
                        {
                            string fileName = currentFile.Substring(docsPath.Length + 1);
                            File.Copy(currentFile, Path.Combine(controlFolder, fileName));
                        }
                    }
                }
                f.Close();
               
            }          
        }
    }
}
