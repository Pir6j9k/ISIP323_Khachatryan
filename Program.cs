using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace University
{
    public abstract class Person // Базовый класс для всех людей в университете
    {
        private static int nextId = 1;  // Статическая переменная для генерации уникальных ID

        // Свойства с защитой через инкапсуляцию (доступны только для чтения извне)
        public int Id { get; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Email { get; private set; }
        protected Person(string name, int age, string email) // Конструктор базового класса с валидацией данных
        {
            // Валидация входных данных
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: Имя не может быть пустым");
                return;
            }

            if (age <= 0 || age > 120)
            {
                Console.WriteLine("Ошибка: Возраст должен быть от 1 до 120 лет");
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Ошибка: Некорректный email");
                return;
            }
            // Инициализация свойств
            Id = nextId++;
            Name = name;
            Age = age;
            Email = email;
        }
        public abstract void DisplayInfo(); // Абстрактный метод, демонстрирующий принцип полиморфизма (должен быть реализован в наследниках)
    }
    public class Student : Person //Класс студента - наследник Person
    {
        private List<Course> courses;
        public string StudentId { get; }
        public IReadOnlyList<Course> Courses => courses.AsReadOnly(); // Свойство только для чтения - защита внутренней коллекции
        public Student(string name, int age, string email) // Конструктор вызывает конструктор базового класса
            : base(name, age, email) 
        {
            // Генерация уникального ID студента
            StudentId = $"{Id:00}";
            courses = new List<Course>();
        }
        public bool EnrollInCourse(Course course) // Метод для записи студента на курс
        {
            if (course == null)
            {
                Console.WriteLine("Ошибка: Курс не может быть null");
                return false;
            }
            if (courses.Contains(course))
            {
                Console.WriteLine("Ошибка: Студент уже записан на этот курс");
                return false;
            }
            courses.Add(course);
            course.AddStudent(this);
            return true;
        }
        public override void DisplayInfo() // Метод вывода данных о студенте
        {
            Console.WriteLine($"Студент: {Name}");
            Console.WriteLine($"  ID: {StudentId}");
            Console.WriteLine($"  Возраст: {Age}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Количество курсов: {courses.Count}");
        }

        public void DisplayCourses() // Метод для отображения списка курсов студента
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Студент не записан ни на один курс");
                return;
            }

            Console.WriteLine($"Курсы студента {Name}:");
            foreach (var course in courses)
            {
                Console.WriteLine($"  - {course.Name}");
            }
        }
    }
    public class Teacher : Person //Класс преподаватель - наследник Person
    {
        public string TeacherId { get; }
        public List<Course> CoursesTeaching { get; }
        public Teacher(string name, int age, string email) 
            : base(name, age, email)
        {
            TeacherId = $"{Id:00}";
            CoursesTeaching = new List<Course>();
        }
        public bool AssignToCourse(Course course) // Метод для назначения преподавателя на курс
        {
            if (course == null)
            {
                Console.WriteLine("Ошибка: Курс не может быть null");
                return false;
            }
            course.AssignTeacher(this);
            CoursesTeaching.Add(course);
            return true;
        }
        public override void DisplayInfo() // Метод вывода информации о преподавателях
        {
            Console.WriteLine($"Преподаватель: {Name}");
            Console.WriteLine($"  ID: {TeacherId}");
            Console.WriteLine($"  Возраст: {Age}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Количество ведомых курсов: {CoursesTeaching.Count}");
        }
    }
    public class Course //Класс курсов
    {
        private static int nextId = 1;  
        private List<Student> students;  // Список студентов на курсе
        // Свойства курса
        public int CourseId { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Teacher Teacher { get; private set; }
        public IReadOnlyList<Student> Students => students.AsReadOnly();  // свойство только для чтения - защита внутренней коллекции
        public Course(string name, string description)
        {
            // Валидация названия курса
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: Название курса не может быть пустым");
                return;
            }

            CourseId = nextId++;
            Name = name;
            Description = description ?? "Описание отсутствует";
            students = new List<Student>();
            Teacher = null;  // Изначально преподаватель не назначен
        }
        public bool AssignTeacher(Teacher teacher) // Метод для назначения преподавателя на курс
        {
            if (teacher == null)
            {
                Console.WriteLine("Ошибка: Преподаватель не может быть null");
                return false;
            }
            Teacher = teacher;
            if (!teacher.CoursesTeaching.Contains(this))
            {
                teacher.CoursesTeaching.Add(this);
            }
            return true;
        }

        public bool AddStudent(Student student) // Метод для добавления студента на курс
        {
            if (student == null)
            {
                Console.WriteLine("Ошибка: Студент не может быть null");
                return false;
            }

            if (students.Contains(student))
            {
                Console.WriteLine("Ошибка: Студент уже записан на этот курс");
                return false;
            }

            students.Add(student);
            return true;
        }

        public void DisplayInfo() // Метод для отображения информации о курсе
        {
            Console.WriteLine($"Курс: {Name}");
            Console.WriteLine($"  ID: {CourseId}");
            Console.WriteLine($"  Описание: {Description}");
            Console.WriteLine($"  Преподаватель: {Teacher?.Name ?? "Не назначен"}");
            Console.WriteLine($"  Количество студентов: {students.Count}");
        }

        public void DisplayStudents() // Метод для отображения списка студентов на курсе
        {
            if (students.Count == 0)
            {
                Console.WriteLine("На курс не записан ни один студент");
                return;
            }

            Console.WriteLine($"Студенты на курсе '{Name}':");
            foreach (var student in students)
            {
                Console.WriteLine($"  - {student.Name} ({student.StudentId})");
            }
        }
    }
    public class UniversityManager //Основной класс для управления университетской системой
    {
        // Коллекции для хранения данных - инкапсуляция
        private List<Student> students;
        private List<Teacher> teachers;
        private List<Course> courses;
        
        public UniversityManager() // Конструктор инициализирует пустые коллекции
        {
            students = new List<Student>();
            teachers = new List<Teacher>();
            courses = new List<Course>();
        }
        public bool AddStudent(Student student) // Метод добавления студента
        {
            if (student == null)
            {
                Console.WriteLine("Ошибка: Студент не может быть null");
                return false;
            }

            students.Add(student);
            return true;
        }
        public bool AddTeacher(Teacher teacher) // Метод добавления преподавателя
        {
            if (teacher == null)
            {
                Console.WriteLine("Ошибка: Преподаватель не может быть null");
                return false;
            }

            teachers.Add(teacher);
            return true;
        }
        public bool AddCourse(Course course) // Метод добавления курса
        {
            if (course == null)
            {
                Console.WriteLine("Ошибка: Курс не может быть null");
                return false;
            }

            courses.Add(course);
            return true;
        }
        public Student FindStudentById(string studentId) // Метод поиска студентов по ID
        {
            return students.FirstOrDefault(s => s.StudentId == studentId);
        }
        public Teacher FindTeacherById(string teacherId) // Метод поиска преподавателей по ID
        {
            return teachers.FirstOrDefault(t => t.TeacherId == teacherId);
        }
        public Course FindCourseById(int courseId) // Метод поиска курсов по ID
        {
            return courses.FirstOrDefault(c => c.CourseId == courseId);
        }
        public void DisplayAllStudents() // Метод для отображения всех студентов
        {
            if (students.Count == 0)
            {
                Console.WriteLine("В системе нет студентов :(");
            }
            Console.WriteLine("Все студенты: ");
            foreach (var student in students)
            {
                student.DisplayInfo();
                Console.WriteLine();
            }
        }
        public void DisplayAllTeachers() // Метод для отображения всех преподвателей
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("В системе нет преподавателей :(");
            }
            Console.WriteLine("Все преподаватели: ");
            foreach(var teacher in teachers)
            {
                teacher.DisplayInfo();
                Console.WriteLine();
            }
        }
        public void DisplayAllCourses() // Метод для отображения всех курсов
        {
            if ( courses.Count == 0)
            {
                Console.WriteLine(" В системе нет курсов :(");
            }
            Console.WriteLine("Все курсы: ");
            foreach( var course in courses)
            {
                course.DisplayInfo();
                Console.WriteLine();
            }
        }
    }
    public class Program // Главный класс программы
    {
        private static UniversityManager universityManager = new UniversityManager();
        static void Main(string[] args)
        {
            Console.WriteLine("");
            InitializeTestData(); // Инициализация тестовыми данными для демонстрации
            // Основной цикл программы
            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": AddTeacher(); break;
                    case "3": AddCourse(); break;
                    case "4": EnrollStudentInCourse(); break;
                    case "5": AssignTeacherToCourse(); break;
                    case "6": universityManager.DisplayAllStudents(); break;
                    case "7": universityManager.DisplayAllTeachers(); break;
                    case "8": universityManager.DisplayAllCourses(); break;
                    case "9": DisplayStudentCourses(); break;
                    case "10": DisplayCourseStudents(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Неверный выбор :("); break;
                }
            }
        }
        static void DisplayMenu() // Метод для отображения главного меню
        {
            Console.WriteLine("\nГлавное меню");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Добавить преподавателя");
            Console.WriteLine("3. Добавить курс");
            Console.WriteLine("4. Записать студента на курс");
            Console.WriteLine("5. Назначить преподавателя на курс");
            Console.WriteLine("6. Показать всех студентов");
            Console.WriteLine("7. Показать всех преподавателей");
            Console.WriteLine("8. Показать все курсы");
            Console.WriteLine("9. Показать курсы студента");
            Console.WriteLine("10. Показать студентов курса");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите опцию: ");
        }
        static void InitializeTestData() // Метод для инициализации тестовых данных
        {
            // Создание тестовых преподавателей
            var teacher1 = new Teacher("Иван Петров", 45, "i.petrov@university.ru");
            var teacher2 = new Teacher("Мария Сидорова", 38, "m.sidorova@university.ru");
            // Создание тестовых студентов
            var student1 = new Student("Алексей Иванов", 20, "a.ivanov@student.ru");
            var student2 = new Student("Елена Смирнова", 19, "e.smirnova@student.ru");
            // Создание тестовых курсов
            var course1 = new Course("Программирование на C#", "Основы программирования на языке C#");
            var course2 = new Course("Высшая математика", "Математический анализ и линейная алгебра");

            // Добавление сущностей в систему
            universityManager.AddTeacher(teacher1);
            universityManager.AddTeacher(teacher2);
            universityManager.AddStudent(student1);
            universityManager.AddStudent(student2);
            universityManager.AddCourse(course1);
            universityManager.AddCourse(course2);

            // Установление связей между сущностями
            teacher1.AssignToCourse(course1);
            teacher2.AssignToCourse(course2);
            student1.EnrollInCourse(course1);
            student1.EnrollInCourse(course2);
            student2.EnrollInCourse(course1);

            Console.WriteLine("Тестовые данные загружены успешно");
        }
        static void AddStudent() // Метод для добавления нового студента
        {
            Console.Write("Введите имя студента: ");
            var name = Console.ReadLine();

            Console.Write("Введите возраст: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Некорректный возраст");
                return;
            }

            Console.Write("Введите email: ");
            var email = Console.ReadLine();

            // Создание и добавление студента
            var student = new Student(name, age, email);
            if (universityManager.AddStudent(student))
            {
                Console.WriteLine($"Студент {name} добавлен успешно. ID: {student.StudentId}");
            }
        }
        static void AddTeacher() // Метод для добавления нового преподавателя
        {
            Console.Write("Введите имя преподавателя: ");
            var name = Console.ReadLine();

            Console.Write("Введите возраст: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Некорректный возраст");
                return;
            }

            Console.Write("Введите email: ");
            var email = Console.ReadLine();

            var teacher = new Teacher(name, age, email);
            if (universityManager.AddTeacher(teacher))
            {
                Console.WriteLine($"Преподаватель {name} добавлен успешно. ID: {teacher.TeacherId}");
            }
        }
        static void AddCourse() // Метод для добавления нового курса
        {
            Console.Write("Введите название курса: ");
            var name = Console.ReadLine();

            Console.Write("Введите описание курса: ");
            var description = Console.ReadLine();

            var course = new Course(name, description);
            if (universityManager.AddCourse(course))
            {
                Console.WriteLine($"Курс '{name}' добавлен успешно. ID: {course.CourseId}");
            }
        }
        static void EnrollStudentInCourse() // Метод для записи студента на курс
        {
            Console.Write("Введите ID студента: ");
            var studentId = Console.ReadLine();

            Console.Write("Введите ID курса: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Некорректный ID курса");
                return;
            }

            // Поиск студента и курса
            var student = universityManager.FindStudentById(studentId);
            var course = universityManager.FindCourseById(courseId);

            if (student == null)
            {
                Console.WriteLine("Студент не найден");
                return;
            }

            if (course == null)
            {
                Console.WriteLine("Курс не найден");
                return;
            }

            // Запись студента на курс
            if (student.EnrollInCourse(course))
            {
                Console.WriteLine($"Студент {student.Name} успешно записан на курс '{course.Name}'");
            }
        }
        static void AssignTeacherToCourse() // Метод для назначения преподавателя на курс
        {
            Console.Write("Введите ID преподавателя: ");
            var teacherId = Console.ReadLine();

            Console.Write("Введите ID курса: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Некорректный ID курса");
                return;
            }

            // Поиск преподавателя и курса
            var teacher = universityManager.FindTeacherById(teacherId);
            var course = universityManager.FindCourseById(courseId);

            if (teacher == null)
            {
                Console.WriteLine("Преподаватель не найден");
                return;
            }

            if (course == null)
            {
                Console.WriteLine("Курс не найден");
                return;
            }

            // Назначение преподавателя на курс
            if (teacher.AssignToCourse(course))
            {
                Console.WriteLine($"Преподаватель {teacher.Name} назначен на курс '{course.Name}'");
            }
        }
        static void DisplayStudentCourses() // Метод для отображения курсов конкретного студента
        {
            Console.Write("Введите ID студента: ");
            var studentId = Console.ReadLine();

            var student = universityManager.FindStudentById(studentId);
            if (student == null)
            {
                Console.WriteLine("Студент не найден");
                return;
            }

            student.DisplayCourses();
        }
        static void DisplayCourseStudents() // Метод для отображения студентов конкретного курса
        {
            Console.Write("Введите ID курса: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Некорректный ID курса");
                return;
            }

            var course = universityManager.FindCourseById(courseId);
            if (course == null)
            {
                Console.WriteLine("Курс не найден");
                return;
            }

            course.DisplayStudents();
        }
    }
}
