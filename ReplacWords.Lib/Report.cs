using System;
using System.IO;
using System.Threading.Tasks;

namespace ReplacWords.Lib
{
    public class Report
    {
        Report(){ }
        
        //Путь к папке с файлами найденных слов
        static string pathDir = "";
        
        //Массив файлов найденных слов в папке
        string[] bannedWordFile = Directory.GetFiles(pathDir, "*.txt");
        
        //Запись информации в файл
        public async Task WriteInfoToFile(string filePath)
        {
            //Путь к файлу для записи информации
            string pathForWrite = @"D:\!Desktop\Step\C#\Projects\ReplacWords\file_report.txt";

            for (int i = 0; i < bannedWordFile.Length; i++)
            {
                using (StreamWriter sw = new StreamWriter(pathForWrite, true))
                {
                    await sw.WriteLineAsync($"Путь файла: {Path.GetFullPath(filePath)}");
                    await sw.WriteLineAsync($"Размер файла: {filePath.Length}");
                    await sw.WriteLineAsync("Количество замен: "+ CountForbiddenWords(filePath));
                }
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