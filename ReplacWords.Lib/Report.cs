using System.IO;
using System.Threading.Tasks;

namespace ReplacWords.Lib
{
    public class Report
    {
        public async Task WriteInfoToFile()
        {
            await using StreamWriter sw = new StreamWriter(Path.GetFullPath("file_info.txt"), true);
            await sw.WriteLineAsync($"Найдено {Search.ArrBannedWordFile.Count} файла(ов) с запрещёнными словами!");
                
            for (int i = 0; i < Search.ArrBannedWordFile.Count; i++)
            {
                await sw.WriteLineAsync($"Размер файла №{i + 1}: {new FileInfo(Search.ArrBannedWordFile[i]).Length}");
                await sw.WriteLineAsync($"Путь файла №{i + 1}: {Path.GetFullPath(Search.ArrBannedWordFile[i])}");
            }

            //TODO
            //await sw.WriteLineAsync($"Общее колличество найденных файлов: {Replace.NumberOfSubstitutions}");
        }
    }
}