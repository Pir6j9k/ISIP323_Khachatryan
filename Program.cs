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
            public DateTime AnalyzerTime { get; set; }
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
                switch (choice)
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
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
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
                    if (char.IsLetterOrDigit(text[i]) && !inWord)
                    {
                        inWord = true;
                        wordCount++;
                    }
                    else if (char.IsWhiteSpace(text[i]) && char.IsPunctuation(text[i]))
                    {
                        inWord = false;
                    }
                }
                return wordCount;
            }
            static void FindWordExtremes(string text, out string shortest, out string longest)
            {
                shortest = "";
                longest = "";
                string[] words = SplitTextIntoWords(text);
                if (words.Length > 0)
                {
                    shortest = words[0];
                    longest = words[0];
                    for (int i = 1; i < words.Length; i++)
                    {
                        if (words[i].Length < shortest.Length)
                        {
                            shortest = words[i];
                        }
                        if (words[i].Length > longest.Length)
                        {
                            longest = words[i];
                        }
                    }
                }

            }
            static string[] SplitTextIntoWords(string text)
            {
                List<string> words = new List<string>();
                StringBuilder currentWord = new StringBuilder();
                char[] separators = GetWordSeparators();
                for (int i = 0; i < text.Length; i++)
                {
                    if (Array.IndexOf(separators, text[i]) == -1)
                    {
                        currentWord.Append(text[i]);
                    }
                    else
                    {
                        if (currentWord.Length > 0)
                        {
                            words.Add(currentWord.ToString());
                            currentWord.Clear();
                        }
                    }
                }
                if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                }
                return words.ToArray();

            }
            static char[] GetWordSeparators()
            {
                List<char> separators = new List<char>();
                separators.AddRange(new char[] { ' ', '\t', '\n', '\r', '!', ',', '.', '?', ';', ':', '-', '(', ')', '"', '[', ']', '{', '}', '\'' });
                return separators.ToArray();
            }
            static int CountSentences(string text)
            {
                int sentCount = 0;
                bool InSent = false;
                for(int i = 0;i < text.Length;i++)
                {
                    if ((text[i])=='.' ||  (text[i] == '!' ||  text[i] == '?') && InSent)
                    {
                        sentCount++;
                        InSent = false;
                    }
                    else if (char.IsLetter(text[i]) && !InSent){
                        InSent = true;
                    }                                          
                }
                if (InSent)
                {
                    sentCount++;
                }
                return sentCount;
            }
            static void CountGlasAndSogl(string text, out int glas, out int sogl)
            {
                glas = 0;
                sogl = 0;
                char[] russGlas = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я',
                                   'А', 'Е', 'Ё', 'И', 'О', 'У', 'Ы', 'Э', 'Ю', 'Я' };
                char[] engGlas = { 'a', 'e', 'i', 'o', 'u', 'y',
                                   'A', 'E', 'I', 'O', 'U', 'Y' };
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsLetter(text[i]))
                    {
                        bool IsGlas = false;
                        for (int j = 0; j < russGlas.Length; j++)
                        {
                            if (text[i] == russGlas[j])
                            {
                                IsGlas = true;
                                break;
                            }
                        }
                        if (!IsGlas)
                        {
                            for (int j = 0; j < engGlas.Length; j++)
                            {
                                if (text[i] == engGlas[j])
                                {
                                    IsGlas = true;
                                    break;
                                }
                            }
                        }
                        if (IsGlas)
                        {
                            glas++;
                        }
                        else
                        {
                            sogl++;
                        }

                    }
                }

            }
            static Dictionary<char, int> GetLetterFrequency(string text)
            {
                Dictionary<char, int> frequency = new Dictionary<char, int>();
                for (int i = 0; i < text.Length; i++)
                {
                    char currentChar = char.ToLower(text[i]);

                    if (char.IsLetter(currentChar))
                    {
                        if (frequency.ContainsKey(currentChar))
                        {
                            frequency[currentChar]++;
                        }
                        else
                        {
                            frequency[currentChar] = 1;
                        }
                    }
                }
                return frequency;
            }
            static void DisplayCurrentStats(TextStats stats)
            {

            }


        }

    }
}

            
       