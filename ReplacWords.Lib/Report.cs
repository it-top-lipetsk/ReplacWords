using System.IO;
using System.Threading.Tasks;


namespace ReplacWords.Lib
{
    public class Report
    {
        //Массив файлов с запрещёнными словами
        string[] bannedWordFile = Directory.GetFiles(Search.controlFolder, "*.txt");
        
        //Запись информации в файл
        public async Task WriteInfoToFile(string[] bannedWordFile)
        {
            using (StreamWriter sw = new StreamWriter(Path.GetFullPath("file_info.txt"), true))
            {
                await sw.WriteLineAsync($"Найдено {bannedWordFile.Length} файла(ов) с запрещёнными словами!");
                
                for (int i = 0; i < bannedWordFile.Length; i++)
                {
                    await sw.WriteLineAsync($"Размер файла №{i + 1}: {new FileInfo(bannedWordFile[i]).Length}");
                    await sw.WriteLineAsync($"Путь файла №{i + 1}: {Path.GetFullPath(bannedWordFile[i])}");
                }

                await sw.WriteLineAsync($"Общее колличество найденных файлов: {Replace.NumberOfSubstitutions}");
            }
        }
    }
}