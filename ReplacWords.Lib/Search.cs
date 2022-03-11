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
        public const int size = 10;
        public string[] forbiddenWords;
        public string docsPath;
        public string controlFolder;
        //static string docsPath =
        //     Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //internal static string controlFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ForbiddenFiles";
                
        public Search(string NewDocsPath) 
        {            
            forbiddenWords = new string[size];
            // плюсом не забыть добавить запрещенные слова в массив для поиска по тексту 
            docsPath = NewDocsPath;
            controlFolder = docsPath + "/ForbiddenFiles";
        }

        public void StartSearch()
        {
            var Files = Directory.EnumerateFiles(docsPath, "*.txt", SearchOption.AllDirectories);

            foreach (string currentFile in Files)
            {
                if(SearchingWordsInFile(currentFile))
                {
                    string fileName = currentFile.Substring(docsPath.Length + 1);
                    File.Copy(currentFile, Path.Combine(controlFolder, fileName));
                }
            }           
        }
        public bool SearchingWordsInFile(string filePath)
        {
            using (FileStream fstream = File.OpenRead(filePath))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                fstream.Read(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);
                //Console.WriteLine($"Текст из файла: {textFromFile}");

                for (int i = 0; i < size; i++)
                {
                    if (textFromFile.Contains(forbiddenWords[i]))
                    {
                        return true;
                    }     
                }
                return false;
            }
        }   
    }
}
