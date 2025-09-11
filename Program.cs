using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        int count;
        do
        {
            Console.Write("Введите количество операций (2-40): ");
        } 
        while (!int.TryParse(Console.ReadLine(), out count) || count < 2 || count > 40);

        string[] names = new string[count];
        decimal[] amounts = new decimal[count];

        for (int i = 0; i < count; i++)
        {
            Console.Write($"Введите операцию {i + 1} в формате (Название; Сумма): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(';');

            if (parts.Length != 2)
            {
                Console.WriteLine("Ошибка формата. Повторите ввод.");
                i--;
                continue;
            }

            names[i] = parts[0].Trim();
            if (!decimal.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out amounts[i]))
            {
                Console.WriteLine("Ошибка формата суммы. Повторите ввод.");
                i--;
            }
        }

        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Вывод данных");
            Console.WriteLine("2. Статистика");
            Console.WriteLine("3. Сортировка по цене");
            Console.WriteLine("4. Конвертация валюты");
            Console.WriteLine("5. Поиск по названию");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PrintData(names, amounts);
                    break;
                case "2":
                    ShowStatistics(amounts);
                    break;
                case "3":
                    BubbleSort(names, amounts);
                    Console.WriteLine("Данные отсортированы.");
                    break;
                case "4":
                    ConvertCurrency(amounts);
                    break;
                case "5":
                    SearchByName(names, amounts);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        }
    }

    static void PrintData(string[] names, decimal[] amounts)
    {
        Console.WriteLine("Список операций:");
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"{names[i]}; {amounts[i]} руб.");
        }
    }

    static void ShowStatistics(decimal[] amounts)
    {
        if (amounts.Length == 0) return;

        decimal sum = 0;
        decimal min = amounts[0];
        decimal max = amounts[0];

        foreach (decimal amount in amounts)
        {
            sum += amount;
            if (amount < min) min = amount;
            if (amount > max) max = amount;
        }

        Console.WriteLine("Статистика:");
        Console.WriteLine($"Сумма: {sum} руб.");
        Console.WriteLine($"Среднее: {sum / amounts.Length} руб.");
        Console.WriteLine($"Минимальная: {min} руб.");
        Console.WriteLine($"Максимальная: {max} руб.");
    }

    static void BubbleSort(string[] names, decimal[] amounts)
    {
        for (int i = 0; i < amounts.Length - 1; i++)
        {
            for (int j = 0; j < amounts.Length - i - 1; j++)
            {
                if (amounts[j] > amounts[j + 1])
                {
                    (amounts[j], amounts[j + 1]) = (amounts[j + 1], amounts[j]);
                    (names[j], names[j + 1]) = (names[j + 1], names[j]);
                }
            }
        }
    }

    static void ConvertCurrency(decimal[] amounts)
    {
        Console.WriteLine("Доступные валюты:");
        Console.WriteLine("1. Доллар (USD)");
        Console.WriteLine("2. Евро (EUR)");
        Console.WriteLine("3. Ввести свой курс");

        Console.Write("Выберите вариант: ");
        string choice = Console.ReadLine();

        decimal rate;
        string currencySymbol;

        switch (choice)
        {
            case "1":
                rate = 0.011m; 
                currencySymbol = "USD";
                break;
            case "2":
                rate = 0.010m; 
                currencySymbol = "EUR";
                break;
            case "3":
                Console.Write("Введите курс конвертации (рублей за единицу валюты): ");
                while (!decimal.TryParse(Console.ReadLine(), out rate) || rate <= 0)
                {
                    Console.Write("Ошибка! Введите положительное число: ");
                }
                Console.Write("Введите обозначение валюты: ");
                currencySymbol = Console.ReadLine();
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                return;
        }

        Console.WriteLine("Конвертированные суммы:");
        for (int i = 0; i < amounts.Length; i++)
        {
            Console.WriteLine($"{amounts[i] * rate} {currencySymbol}");
        }
    }

    static void SearchByName(string[] names, decimal[] amounts)
    {
        Console.Write("Введите поисковый запрос: ");
        string query = Console.ReadLine().ToLower();

        Console.WriteLine("Результаты поиска:");
        bool found = false;

        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].ToLower().Contains(query))
            {
                Console.WriteLine($"{names[i]}; {amounts[i]} руб.");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Совпадений не найдено.");
        }
    }
}