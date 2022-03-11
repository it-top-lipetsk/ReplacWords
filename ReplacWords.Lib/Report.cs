using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ReplacWords.Lib
{
    public class Report
    {
        //Путь к папке с файлами найденных слов
        static string pathDir = "";
        
        //Путь к файлу для записи информации
        string pathForWrite = @"D:\!Desktop\Step\C#\Projects\ReplacWords\file_report.txt";
        
        //Массив файлов найденных слов в папке
        string[] bannedWordFile = Directory.GetFiles(pathDir, "*.txt");
        
        //Запись информации в файл
        public async Task WriteInfoToFile(string[] bannedWordFile)
        {
            using (StreamWriter sw = new StreamWriter(pathForWrite, true))
            {
                int CountAll = 0;
                
                for (int i = 0; i < bannedWordFile.Length; i++)
                {
                    await sw.WriteLineAsync($"Путь файла №{i + 1}: {Path.GetFullPath(bannedWordFile[i])}");
                    await sw.WriteLineAsync($"Размер файла №{i + 1}: {bannedWordFile[i].Length}");
                }

                await sw.WriteLineAsync($"Общее колличество найденных файлов: {Replace.NumberOfSubstitutions}");
            }
        }
    }
}