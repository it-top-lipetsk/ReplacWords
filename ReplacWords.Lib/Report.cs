using System;
using System.IO;
using System.Threading.Tasks;

namespace ReplacWords.Lib
{
    public class Report
    {
        //Путь к папке с файлами найденных слов
        static string pathDir = "";
        
        Report(string _path)
        {
            pathDir = _path;
        }
        
        //Массив файлов найденных слов в папке
        string[] bannedWordFile = Directory.GetFiles(pathDir, "*.txt");
        
        //Запись информации в файл
        public async Task WriteInfoToFile(string[] bannedWordFile)
        {
            //Путь к файлу для записи информации
            string pathForWrite = @"D:\!Desktop\Step\C#\Projects\ReplacWords\file_report.txt";

            using (StreamWriter sw = new StreamWriter(pathForWrite, true))
            {
                int CountAll = 0;
                
                for (int i = 0; i < bannedWordFile.Length; i++)
                {
                    await sw.WriteLineAsync($"Путь файла №{i + 1}: {Path.GetFullPath(bannedWordFile[i])}");
                    await sw.WriteLineAsync($"Размер файла №{i + 1}: {bannedWordFile[i].Length}");
                    await sw.WriteLineAsync("Количество замен в файле №" + i + 1 + ":" + CountForbiddenWords(bannedWordFile[i]));
                    CountAll += CountForbiddenWords(bannedWordFile[i]);
                }

                await sw.WriteLineAsync($"Общее колличество найденных файлов: {CountAll}");
            }
        }
        
        // Подсчёт найденных запрещённых слов в файле
        public int CountForbiddenWords(string path)
        {
            int wordsCounter = 0;

            StreamReader sr = new StreamReader(path);
            
            string word = "";
            while (!sr.EndOfStream)
            {
                word += sr.ReadLine();
            }

            string[] wordArray = word.Split(new char[] {' ',','});
            foreach (string str in wordArray)
            {
                wordsCounter++;
            }

            return wordsCounter;
        }
        
        //Вывод информации на экран
        public async Task Show(string filePath)
        {
            using(StreamReader sr = new StreamReader(filePath))
            {
                string textInfo = await sr.ReadToEndAsync();
                Console.WriteLine(textInfo);
            }
        }
    }
}