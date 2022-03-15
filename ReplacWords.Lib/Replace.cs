using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReplacWords.Lib
{
    public class Replace
    {
        //с-во, присваивается слово которое изменяем 
        public string Line { get; set; }

        private Dictionary<string, string> _word;
        public static int NumberOfSubstitutions { get; set; }

        //конструктор с параметрами
        public Replace(string[] line)
        {
            _word = new Dictionary<string, string>();
            NumberOfSubstitutions = 0;

            foreach (var l in line)
            {
                string temp = "";
                for (int i = 0; i < l.Length; i++)
                {
                    temp += "*";
                }
                _word.Add(l, temp);
            }
        }

        public Replace(string[] word, string line)
        {
            _word = new Dictionary<string, string>();
            NumberOfSubstitutions = 0;
            Line = line;

            foreach (var l in word)
            {
                string temp = "";
                for (int i = 0; i < l.Length; i++)
                {
                    temp += "*";
                }
                _word.Add(l, temp);
            }
        }

        //метод изменяет слово на **** без параметров
        public string ReplacementWord()
        {
            string tempLine = Line;
            if (!string.IsNullOrEmpty(Line))
            {
                foreach (var w in _word)
                {
                    Line = Line.Replace(w.Key, w.Value);
                    if (string.Equals(tempLine, Line))
                    {
                        NumberOfSubstitutions++;
                        tempLine = Line;
                    }
                }
            }
            return Line;
        }

        //метод изменяет слово на **** с параметрами
        public string ReplacementWord(string line)
        {
            string tempLine = line;
            if (!string.IsNullOrEmpty(line))
            {
                foreach (var w in _word)
                {
                    line = line.Replace(w.Key, w.Value);
                    if (string.Equals(tempLine, line))
                    {
                        NumberOfSubstitutions++;
                        tempLine = line;
                    }
                }
            }
            return line;
        }

        //асинхронный метод изменения слова без параметра
        public async ValueTask<string> ReplacementWordAsync()
        {
            return await Task.Run(() => ReplacementWord());
        }

        //асинхронный метод изменения слова c параметрами
        public async ValueTask<string> ReplacementWordAsync(string line)
        {
            return await Task.Run(() => ReplacementWord(line));
        }


    }
}
