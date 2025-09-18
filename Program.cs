using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace ShopInventory
{
    public enum Category
    {
        Electronics,
        Cloths,
        Food,
        Books,
        Sports
    }

    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool InStock => Quantity > 0;    
        public Category Category { get; set; }
        public override string ToString()
        {
            return $"Код: {Code}\n Название: {Name}\nЦена:{Price}\nКоличество: {Quantity}\nВ наличии: {(InStock ? "Да" : "Нет")}\nКатегория: {Category}";
        }
    }

    class Program
    {
        private static List<Product> products = new List<Product>();
        private static int productCounter = 1;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("_____Учет товаров в магазине_____");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Заказать поставку товара");
                Console.WriteLine("4. Продать товар");
                Console.WriteLine("5. Поиск товара по индексу");
                Console.WriteLine("6. Вывести список товаров");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddProduct(); break;
                    case "2": RemoveProduct(); break;
                    case "3": SupplyProduct(); break;
                    case "4": SellProduct(); break;
                    case "5": SearchProducts(); break;
                    case "6": ShowAllProducts(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор"); break;
                }

            }
        }

        static void AddProduct()
        {
            Console.WriteLine("Добавление товара");

            Console.Write("Название: ");
            var name = Console.ReadLine();
            Console.Write("Цена: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Ошибка ввода цены");
                return;
            }

            Console.Write("Количество: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Ошибка ввода количества");
                return;
            }

            Console.Write("Категория:");
            foreach (var cat in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)cat}. {cat}");
            }
            Console.Write("Выберите категорию: ");
            if (!Enum.TryParse(Console.ReadLine(), out Category category))
            {
                Console.WriteLine("Ошибка ввода категории");
                return;
            }
            products.Add(new Product
            {
                Code = $"{productCounter++}",
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = category,
            });
            Console.WriteLine("Товар успешно добавлен");
        }

        static void RemoveProduct()
        {
            Console.WriteLine("Введите код товара для удаления: ");
            var code = Console.ReadLine();

            var product = products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }
            products.Remove(product);
            Console.WriteLine("Товар успешно удалён");

        }

        static void SupplyProduct()
        {
            Console.WriteLine("Введите код товара для поставки: "); 
            var code = Console.ReadLine();
            var product = products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }
            Console.WriteLine("Введите количество для поставки: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Ошибка ввода количества");
                return;
            }
            product.Quantity += quantity;
            Console.WriteLine("Поставка успешно обработана");


        }

        static void SellProduct()
        {
            Console.Write("\nВведите код товара для продажи: ");
            var code = Console.ReadLine();
            var product = products.FirstOrDefault(p => p.Code == code);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }
            Console.Write("Введите количество для продажи: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Ошибка ввода количества!");
                return;
            }
            if (product.Quantity < quantity)
            {
                Console.WriteLine("Недостаточно товара на складе!");
                return;
            }
            product.Quantity -= quantity;
            Console.WriteLine("Продажа успешно обработана!");


        }

        static void SearchProducts()
        {
            Console.WriteLine("Поиск товаров");
            Console.WriteLine("1. По коду");
            Console.WriteLine("2. По названию");
            Console.WriteLine("3. По категории");
            Console.Write("Выберите тип поиска: ");
            var choice  = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите код: ");
                    SearchByCode(Console.ReadLine());
                    break;
                case "2":
                    Console.WriteLine("Введите название: ");
                    SearchByName(Console.ReadLine());
                    break; 
                case "3":
                    Console.WriteLine("Доступные категории:");
                    foreach (var cat in Enum.GetValues(typeof(Category)))
                    {
                        Console.WriteLine($"{(int)cat}. {cat}");
                    }
                    Console.Write("Выберите категорию: ");
                    if (Enum.TryParse(Console.ReadLine(), out Category category))
                    {
                        SearchByCategory(category);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка выбора категории!");
                    }
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }

        static void SearchByCode(string code)
        {
            var results = products.Where(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            ShowSearchResults(results);
        }

        static void SearchByName(string name)
        {
            var results = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            ShowSearchResults(results);
        }

        static void SearchByCategory(Category category)
        {
            var results = products.Where(p => p.Category == category);
            ShowSearchResults(results);
        }

        static void ShowSearchResults(IEnumerable<Product> results)
        {
            if (!results.Any())
            {
                Console.WriteLine("Товары не найдены!");
                return;
            }

            foreach (var product in results)
            {
                Console.WriteLine(product);
            }
        }

        static void ShowAllProducts()
        {
            Console.WriteLine("Все товары");

        }
    }
}