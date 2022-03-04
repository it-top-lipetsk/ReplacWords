using ReplacWords.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplacWords.Lib
{
    public class Replace
    {
        //с-во, присваивается слово которое изменяем 
        public string Word { get; set; }

        //конструктор по умолчанию
        public Replace() { }

        //конструктор с параметрами
        public Replace(string word) => Word = word;

        //метод изменяет слово на ****
        public void ReplacementWord()
        {
            if (Word is not null && Word != "")
            {
                int size = Word.Length;
                Word = "";

                for (int i = 0; i < size; i++)
                {
                    Word += "*";
                }
            }

        }

        //асинхронный метод изменения слова
        public async Task ReplacementWordAsync()
        {
            await Task.Run(() => ReplacementWord());
        }

        //так для разнообразия
        public override string ToString() => $"{Word}";
    }
}
