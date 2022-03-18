using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


namespace ReplacWords.Lib
{
    public class Search
    {
        public const int size = 10;
        
        public string[] forbiddenWords; //массив запрещенных слов
        public static string[] filesExtensions; //список расширений файлов, которые будут проверяться
        public string docsPath;
        public string controlFolder;
        //for @KirillLagutin
        public static List<string> ArrBannedWordFile = new List<string>();
        public Search(string newDocsPath) 
        {            
            forbiddenWords = new string[size];
            // плюсом не забыть добавить запрещенные слова в массив для поиска по тексту или подставить из общего
            filesExtensions = new string[] { "*.txt", "*.doc", "*.docx", "*.rtf", "*.djvu", "*.pdf", "*.odt", };
            docsPath = newDocsPath;
            controlFolder = docsPath + @"\ForbiddenFiles";
           
        }

        public void StartSearch(IProgress<int> progress, CancellationToken token)
        {
            for(int i = 0; i < filesExtensions.Length; i++)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                progress.Report(i);
                //SearchOneExt(filesExtensions[i]);
                Thread.Sleep(2000);
            }
        }

        public void SearchOneExt(string fileExt)
        {
            var files = Directory.EnumerateFiles(docsPath, fileExt , SearchOption.AllDirectories);
            
            foreach (string currentFile in files)
            {
                if (!SearchingWordsInFile(currentFile)) continue;
                
                ArrBannedWordFile.Add(currentFile); //полное имя файла(путь) с запрещёнными словами копируется в стринговый массив 
                CopyToControlFolder(currentFile);
            }
        }
        public bool SearchingWordsInFile(string filePath)
        {
            using StreamReader sr = new StreamReader(filePath);
            var textFromFile = sr.ReadToEnd();
            for (int i = 0; i < size; i++)
            {
                if (textFromFile.Contains(forbiddenWords[i]))
                {
                    return true;
                }     
            }
            return false;
        }

        public void CopyToControlFolder(string dangerFileFullPath)
        {
            var fileName = dangerFileFullPath[(docsPath.Length + 1)..];
            File.Copy(dangerFileFullPath, Path.Combine(controlFolder, fileName));
        }
        //асинхронная версия метода поиска файлов с запрещёнными словами
        public async Task StartSearchAsync(IProgress<int> progress, CancellationToken token)
        {
            await Task.Run(() => StartSearch(progress, token), token);
        }
    }
}
