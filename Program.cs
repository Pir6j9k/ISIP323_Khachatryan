using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace University
{
    public abstract class Person // Базовый класс для всех людей в университете
    {
        private static int _nextId = 1;  // Статическая переменная для генерации уникальных ID

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
            Id = _nextId++;
            Name = name;
            Age = age;
            Email = email;
        }
        public abstract void DisplayInfo(); // Абстрактный метод, демонстрирующий принцип полиморфизма (должен быть реализован в наследниках)
    }
    public class Student : Person //Класс студента - наследник Person
    {
        private List<Course> _courses;
        public string StudentId { get; }
        public IReadOnlyList<Course> Courses => _courses.AsReadOnly(); // Свойство только для чтения - защита внутренней коллекции
        public Student(string name, int age, string email) // Конструктор вызывает конструктор базового класса
            : base(name, age, email) 
        {
            // Генерация уникального ID студента
            StudentId = $"{Id:00}";
            _courses = new List<Course>();
        }
        public bool EnrollInCourse(Course course) // Метод для записи студента на курс
        {
            if (course == null)
            {
                Console.WriteLine("Ошибка: Курс не может быть null");
                return false;
            }
            if (_courses.Contains(course))
            {
                Console.WriteLine("Ошибка: Студент уже записан на этот курс");
                return false;
            }
            _courses.Add(course);
            course.AddStudent(this);
            return true;
        }
        public override void DisplayInfo() // Метод вывода данных о студенте
        {
            Console.WriteLine($"Студент: {Name}");
            Console.WriteLine($"  ID: {StudentId}");
            Console.WriteLine($"  Возраст: {Age}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Количество курсов: {_courses.Count}");
        }

        public void DisplayCourses() // Метод для отображения списка курсов студента
        {
            
        }
    }
    public class Teacher : Person //Класс преподаватель - наследник Person
    {
        public bool AssignToCourse(Course course) // Метод для назначения преподавателя на курс
        {
            
        }
        public override void DisplayInfo() // Метод вывода информации о преподавателях
        {
            
        }
    }
    public class Course //Класс курсов
    {
        public bool AssignTeacher(Teacher teacher) // Метод для назначения преподавателя на курс
        {

        }

        public bool AddStudent(Student student) // Метод для добавления студента на курс
        {

        }

        public void DisplayInfo() // Метод для отображения информации о курсе
        {

        }

        public void DisplayStudents() // Метод для отображения списка студентов на курсе
        {

        }
    }
    public class UniversityManager //Основной класс для управления университетской системой
    {
        public bool AddStudent(Student student) // Метод добавления студента
        {
            
        }
        public bool AddTeacher(Teacher teacher) // Метод добавления преподавателя
        {
            
        }
        public bool AddCourse(Course course) // Метод добавления курса
        {
            
        }
        public Student FindStudentById(string studentId) // Метод поиска студентов по ID
        {

        }
        public Teacher FindTeacherById(string teacherId) // Метод поиска преподавателей по ID
        {
        }
        public Course FindCourseById(int courseId) // Метод поиска курсов по ID
        {

        }
        public void DisplayAllStudents() // Метод для отображения всех студентов
        {
            
        }
        public void DisplayAllTeachers() // Метод для отображения всех преподвателей
        {
            
        }
        public void DisplayAllCourses() // Метод для отображения всех курсов
        {
            
        }
    }
    public class Program // Главный класс программы
    {
        static void DisplayMenu() // Метод для отображения главного меню
        {
            
        }
        static void InitializeTestData() // Метод для инициализации тестовых данных
        {
            
        }
        static void AddStudent() // Метод для добавления нового студента
        {
            
        }
        static void AddTeacher() // Метод для добавления нового преподавателя
        {
            
        }
        static void AddCourse() // Метод для добавления нового курса
        {
            
        }
        static void EnrollStudentInCourse() // Метод для записи студента на курс
        {
            
        }
        static void AssignTeacherToCourse() // Метод для назначения преподавателя на курс
        {
            
        }
        static void DisplayStudentCourses() // Метод для отображения курсов конкретного студента
        {
            
        }
        static void DisplayCourseStudents() // Метод для отображения студентов конкретного курса
        {
            
        }
    }
}
