using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReplacWords.Lib
{
    public class Replace
    {

        /// <summary>
        /// с-во строка
        /// </summary>
        public string Line { get; set; }
        /// <summary>
        /// словарь запрещенных слов
        /// </summary>
        private Dictionary<string, string> _word;
        /// <summary>
        /// счетчик измененых слов
        /// </summary>
        public int NumberOfSubstitutions { get; set; }

        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="word">массив запрещенных слов</param>
        public Replace(string[] word)
        {
            FillInDictionary(word);
            NumberOfSubstitutions = 0;
            Line = "";
        }
        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="word">массив запрещенных слов</param>
        /// <param name="line">строка</param>
        public Replace(string[] word, string line)
        {
            FillInDictionary(word);
            NumberOfSubstitutions = 0;
            Line = line;
        }

        /// <summary>
        /// метод заполнения словаря
        /// </summary>
        /// <param name="word">массив запрещенных слов</param>
        private void FillInDictionary(string[] word)
        {

            _word = new Dictionary<string, string>();

            foreach (var w in word)
            {
                string temp = "";
                for (int i = 0; i < w.Length; i++)
                {
                    temp += "*";
                }
                _word.Add(w, temp);
            }
        }

        /// <summary>
        /// метод замены запрещенных слов на звездочки
        /// </summary>
        /// <returns>измененную строку</returns>
        public string ReplacementWord()
        {
            bool ignoreCase = true;
            CultureInfo culture = null;
            string tempLine = Line;

            if (!string.IsNullOrEmpty(Line))
            {
                foreach (var w in _word)
                {
                    Line = Line.Replace(w.Key, w.Value, ignoreCase, culture);
                    if (!tempLine.Equals(Line))
                    {
                        NumberOfSubstitutions++;
                        tempLine = Line;
                    }
                }
            }
            return Line;
        }

        /// <summary>
        /// метод замены запрещенных слов на звездочки с параметром
        /// </summary>
        /// <param name="line">строка</param>
        /// <returns>измененную строку</returns>
        public string ReplacementWord(string line)
        {
            bool ignoreCase = true;
            CultureInfo culture = null;

            if (!string.IsNullOrEmpty(line))
            {
                foreach (var w in _word)
                {
                    CounterReplacedWords(w.Key, line);
                    line = line.Replace(w.Key, w.Value, ignoreCase, culture);
                }
            }
            return line;
        }

        /// <summary>
        /// Метод подщитывает кол-во запрещенных слов в строке
        /// </summary>
        /// <param name="word">запрещенное слово</param>
        /// <param name="line">строка</param>
        private void CounterReplacedWords(string word, string line)
        {
            NumberOfSubstitutions += new Regex(word).Matches(line.ToLower()).Count;
        }

        /*public string ReplacementWord2(string line)
        {
            bool ignoreCase = true;
            CultureInfo culture = null;

            if (!string.IsNullOrEmpty(line))
            {
                string tempLine = "";
                var temp = line.Split(new char[] { ' ' });

                foreach (var w in _word)
                {
                    tempLine = "";

                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].ToLower().Equals(w.Key.ToLower()))
                        {
                            temp[i] = temp[i].Replace(w.Key, w.Value, ignoreCase, culture);
                            NumberOfSubstitutions++;
                        }
                        tempLine += $"{temp[i]} ";

                    }
                }
                return tempLine;
            }
            return line;
        }*/

        /// <summary>
        /// метод замены запрещенных слов на звездочки запускаемый в отдельном потоке
        /// </summary>
        /// <returns>измененную строку</returns>
        public async ValueTask<string> ReplacementWordAsync()
        {
            return await Task.Run(() => ReplacementWord());
        }

        /// <summary>
        /// метод замены запрещенных слов на звездочки с параметром запускаемый в отдельном потоке
        /// </summary>
        /// <param name="line">строка</param>
        /// <returns>измененную строку</returns>
        public async ValueTask<string> ReplacementWordAsync(string line)
        {
            return await Task.Run(() => ReplacementWord(line));
        }


    }
}
