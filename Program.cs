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
        static List<TextStats> allstats = new List<TextStats>();
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Выберите дейстиве: ");
                Console.WriteLine("1. Анализ нового текста");
                Console.WriteLine("2. Просмотр статистики по прошлым текстам");
                Console.WriteLine("3. Выход");
                string choice = Console.ReadLine();
                switch(choice) 
                {
                    case "1":
                        AnalyseNewText();
                        break;
                    case "2":
                        ShowPreviousStats();
                        break;
                    case "3":
                        return;
                    default: 
                        Console.WriteLine("Неверный выбор");
                        break;
                }
                static void AnalyseNewText()
                {
                    Console.WriteLine("Введите текст (минимум 100 символов): ");
                    string text = GetTextFromUser();
                    if (text.Length < 100)
                    {
                        Console.WriteLine("Недостаточно символов");
                        return;
                    }
                    TextStats stats = new TextStats();
                    stats.AnalyzerTime = DateTime.Now;
                    stats.WordCount = CountWords(text);
                    FindWordExtremes(text, out string shortest, out string longest);
                    stats.ShortestWord = shortest;
                    stats.LongestWord = longest;
                    stats.SentenceCount = CountSentences(text);
                    CountGlasAndSogl(text, out int glas, out int sogl);
                    stats.GlasCount = glas;
                    stats.SoglCount = sogl;
                    stats.LetterFrequency = GetLetterFrequency(text);
                    allstats.Add(stats);
                    DisplayCurrentStats(stats);
                }
                static string GetTextFromUser()
                {
                    StringBuilder textSb = new StringBuilder();
                    string line;
                    Console.WriteLine("Введите текст (для завершения ввода введите пустую строку):");
                    while(!string.IsNullOrEmpty(line=Console.ReadLine()))
                    {
                        textSb.AppendLine(line);
                    }
                    return textSb.ToString().Trim();
                }
                static int CountWords(string text)
                {
                    int wordCount = 0;
                    bool inWord = false;
                    for (int i = 0; i < text.Length; i++)
                    {
                        if(char.IsLetterOrDigit(text[i]) && !inWord)
                        {
                            inWord = true; 
                            wordCount++;
                        }
                    }
                }
            }

        }
    }
}