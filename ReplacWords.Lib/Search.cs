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
        
        public string[] forbiddenWords; //массив запрещенных слов
        public static string[] filesExtensions; //список расширений файлов, которые будут проверяться
        public string docsPath;
        public string controlFolder;
        //for @KirillLagutin
        List<string> ArrBannedWordFile; 
        public Search(string NewDocsPath) 
        {            
            forbiddenWords = new string[size];
            // плюсом не забыть добавить запрещенные слова в массив для поиска по тексту или подставить из общего
            filesExtensions = new string[] { "*.txt", "*.doc", "*.docx", "*.rtf", "*.djvu", "*.pdf", "*.odt", };
            docsPath = NewDocsPath;
            controlFolder = docsPath + "/ForbiddenFiles";
            List<string> ArrBannedWordFile = new List<string>();
        }

        public void StartSearch()
        {
            for(int i = 0; i < filesExtensions.Length; i++)
            {
                SearchOneExt(filesExtensions[i]);
            }
        }

        public void SearchOneExt(string fileExt)
        {
            var Files = Directory.EnumerateFiles(docsPath, fileExt , SearchOption.AllDirectories);
            
            foreach (string currentFile in Files)
            {
                if(SearchingWordsInFile(currentFile))
                {
                    ArrBannedWordFile.Add(currentFile); //полное имя файла(путь) с запрещёнными словами копируется в стринговый массив 
                    
                    CopyToControlFolder(currentFile);
                }
            }           
        }
        public bool SearchingWordsInFile(string filePath)
        {                      
            using (StreamReader sr = new StreamReader(filePath))
            {
                string textFromFile = sr.ReadToEnd();
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

        public void CopyToControlFolder(string dangerFileFullPath)
        {
            string fileName = dangerFileFullPath.Substring(docsPath.Length + 1);
            File.Copy(dangerFileFullPath, Path.Combine(controlFolder, fileName));
        }
        //асинхронная версия метода поиска файлов с запрещёнными словами
        public async Task StartSearchAsync()
        {
            await Task.Run(() => StartSearch());
        }
    }
}
