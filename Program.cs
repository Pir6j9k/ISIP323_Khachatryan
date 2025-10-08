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
        public Library()
        {
            books.Add(new Book(nextId++, "Война и мир", new Author("Лев Толстой"), Genre.Fiction, 1821, 500));
            books.Add(new Book(nextId++, "Идиот", new Author("Фёдор Достоевский"), Genre.Novel, 1821, 450));
            books.Add(new Book(nextId++, "Десять негретят", new Author("Агата Кристи"), Genre.Detective, 1939, 600));
            books.Add(new Book(nextId++, "Гарри Поттер и философский камень", new Author("Дж. К. Роулинг"), Genre.Fantasy, 1997, 350));
            books.Add(new Book(nextId++, "После", new Author("Анна Тодд"), Genre.Romance, 2021, 379));
        }
        // Добавить книгу
        public void AddBook()
        {
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();

            Console.Write("Введите имя автора: ");
            string authorName = Console.ReadLine();

            // Выводим список жанров для выбора
            Console.WriteLine("Выберите жанр (введите номер):");
            foreach (var genreName in Enum.GetNames(typeof(Genre)))
            {
                int index = Array.IndexOf(Enum.GetNames(typeof(Genre)), genreName);
                Console.WriteLine($"{index + 1}. {genreName}");
            }

            // Проверка правильности ввода жанра
            int genreIndex;
            while (!int.TryParse(Console.ReadLine(), out genreIndex) || genreIndex < 1 || genreIndex > Enum.GetNames(typeof(Genre)).Length)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку:");
            }
            Genre genre = (Genre)(genreIndex - 1); // Преобразуем номер в элемент перечисления

            // Ввод года с проверкой
            Console.Write("Введите год издания: ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year) || year < 0)
            {
                Console.WriteLine("Некорректный год. Повторите ввод:");
            }

            // Ввод цены с проверкой
            Console.Write("Введите цену книги: ");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Некорректная цена. Повторите ввод:");
            }

            // Создаём новую книгу и добавляем в список
            var author = new Author(authorName);
            var book = new Book(nextId++, title, author, genre, year, price);
            books.Add(book);

            Console.WriteLine("Книга успешно добавлена!");
        }        

        // Удалить книгу по ID
        public void RemoveBook()
        {
            Console.Write("Введите ID книги для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = books.FirstOrDefault(b => b.Id == id); // Ищем книгу по ID
                if (book != null)
                {
                    books.Remove(book);
                    Console.WriteLine("Книга удалена.");
                }
                else
                {
                    Console.WriteLine("Книга с таким ID не найдена.");
                }
            }
        }

        // Найти книги (по названию, автору, жанру)
        public void FindBooks()
        {
            Console.Write("Введите строку поиска (название, автор или жанр): ");
            string query = Console.ReadLine().ToLower(); // Приводим к нижнему регистру для нечувствительности к регистру

            // LINQ-запрос: ищем совпадения в трёх полях
            var foundBooks = books.Where(b =>
                b.Title.ToLower().Contains(query) ||
                b.Author.Name.ToLower().Contains(query) ||
                b.Genre.ToString().ToLower().Contains(query)
            );

            DisplayList(foundBooks);
        }

        // Отсортировать книги по названию
        public void SortBooksByTitle()
        {
            var sorted = books.OrderBy(b => b.Title); // LINQ-сортировка
            DisplayList(sorted);
        }

        // Отсортировать книги по году
        public void SortBooksByYear()
        {
            var sorted = books.OrderBy(b => b.Year);
            DisplayList(sorted);
        }

        // Найти самую дорогую книгу
        public void GetMostExpensiveBook()
        {
            var book = books.OrderByDescending(b => b.Price).FirstOrDefault(); // Сортировка по убыванию цены
            if (book != null)
                Console.WriteLine($"Самая дорогая книга: {book}");
            else
                Console.WriteLine("Библиотека пуста.");
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
