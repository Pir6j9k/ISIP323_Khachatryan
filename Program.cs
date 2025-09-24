using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalyzer
{
    class Program
    {
        //Класс для хранения статистики по тексту
        class TextStats
        {
            public int WordCount { get; set; }
            public string ShortestWord { get; set; }
            public int SentenceCount { get; set; }
            public int GlasCount { get; set; }
            public int SoglCount { get; set; }
            public string LongestWord { get; set; }
            public Dictionary<char, int> LetterFrequency { get; set; }
            public DateTime AnalyzerTime {  get; set; }
        }
    }
}