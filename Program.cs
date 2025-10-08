using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp
{
    // Возможные жанры книг
    public enum Genre
    {
        Fiction,
        NonFiction,
        Novel,
        Fantasy,
        Detective,
        Romance
    }

    // Класс автора
    public class Author
    {
        public string Name { get; set; }

        // Конструктор
        public Author(string name)
        {
            Name = name;
        }
    }

    // Класс книги
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        // Конструктор
        public Book(int id, string title, Author author, Genre genre, int year, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        // Переопределим вывод книги
        public override string ToString()
        {
            return $"{Id}: {Title} ({Author.Name}, {Year}, {Genre}, {Price} руб.)";
        }
    }

    // Класс библиотеки — хранит список книг и операции с ними
    public class Library
    {
        private List<Book> books = new List<Book>();
        private int nextId = 1;

        // Добавить книгу
        public void AddBook()
        {
            // TODO: Реализовать ввод данных и добавление книги
        }

        // Удалить книгу по ID
        public void RemoveBook()
        {
            // TODO: Реализовать удаление книги по идентификатору
        }

        // Найти книги (по названию, автору, жанру)
        public void FindBooks()
        {
            // TODO: Реализовать поиск книг
        }

        // Отсортировать книги по названию
        public void SortBooksByTitle()
        {
            // TODO: Реализовать сортировку по названию
        }

        // Отсортировать книги по году
        public void SortBooksByYear()
        {
            // TODO: Реализовать сортировку по году
        }

        // Найти самую дорогую книгу
        public void GetMostExpensiveBook()
        {
            // TODO: Реализовать поиск самой дорогой книги
        }

        // Найти самую дешёвую книгу
        public void GetCheapestBook()
        {
            // TODO: Реализовать поиск самой дешёвой книги
        }

        // Сгруппировать книги по авторам
        public void GroupBooksByAuthor()
        {
            // TODO: Реализовать группировку по авторам
        }

        // Вывести все книги
        public void DisplayBooks()
        {
            // TODO: Реализовать вывод всех книг
        }
    }

    // Главный класс программы
    public class Program
    {
        public static void Main(string[] args)
        {
            Library library = new Library();

            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                Console.Write("Введите номер команды: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        library.AddBook();
                        break;
                    case "2":
                        library.RemoveBook();
                        break;
                    case "3":
                        library.FindBooks();
                        break;
                    case "4":
                        library.SortBooksByTitle();
                        break;
                    case "5":
                        library.SortBooksByYear();
                        break;
                    case "6":
                        library.GetMostExpensiveBook();
                        break;
                    case "7":
                        library.GetCheapestBook();
                        break;
                    case "8":
                        library.GroupBooksByAuthor();
                        break;
                    case "9":
                        library.DisplayBooks();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }

        // Выводит меню команд
        static void ShowMenu()
        {
            Console.WriteLine("МЕНЮ БИБЛИОТЕКИ");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Найти книги");
            Console.WriteLine("4. Отсортировать книги по названию");
            Console.WriteLine("5. Отсортировать книги по году");
            Console.WriteLine("6. Самая дорогая книга");
            Console.WriteLine("7. Самая дешёвая книга");
            Console.WriteLine("8. Группировка по авторам");
            Console.WriteLine("9. Показать все книги");
            Console.WriteLine("0. Выход");
        }
    }
}
