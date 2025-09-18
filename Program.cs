using System;
using System.Collections.Generic;
using System.Linq;

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

        }

        static void RemoveProduct()
        {
            
        }

        static void SupplyProduct()
        {
            
        }

        static void SellProduct()
        {

        }

        static void SearchProducts()
        {

        }

        static void SearchByCode()
        {

        }

        static void SearchByName()
        {
        
        }

        static void SearchByCategory()
        {
            
        }

        static void ShowSearchResults()
        {
         
        }

        static void ShowAllProducts()
        {
           
        }
    }
}