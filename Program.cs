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
            if (_courses.Count == 0)
            {
                Console.WriteLine("Студент не записан ни на один курс");
                return;
            }

            Console.WriteLine($"Курсы студента {Name}:");
            foreach (var course in _courses)
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
        private static int _nextId = 1;  
        private List<Student> _students;  // Список студентов на курсе
        // Свойства курса
        public int CourseId { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Teacher Teacher { get; private set; }
        public IReadOnlyList<Student> Students => _students.AsReadOnly();  // свойство только для чтения - защита внутренней коллекции
        public Course(string name, string description)
        {
            // Валидация названия курса
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ошибка: Название курса не может быть пустым");
                return;
            }

            CourseId = _nextId++;
            Name = name;
            Description = description ?? "Описание отсутствует";
            _students = new List<Student>();
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

            if (_students.Contains(student))
            {
                Console.WriteLine("Ошибка: Студент уже записан на этот курс");
                return false;
            }

            _students.Add(student);
            return true;
        }

        public void DisplayInfo() // Метод для отображения информации о курсе
        {
            Console.WriteLine($"Курс: {Name}");
            Console.WriteLine($"  ID: {CourseId}");
            Console.WriteLine($"  Описание: {Description}");
            Console.WriteLine($"  Преподаватель: {Teacher?.Name ?? "Не назначен"}");
            Console.WriteLine($"  Количество студентов: {_students.Count}");
        }

        public void DisplayStudents() // Метод для отображения списка студентов на курсе
        {
            if (_students.Count == 0)
            {
                Console.WriteLine("На курс не записан ни один студент");
                return;
            }

            Console.WriteLine($"Студенты на курсе '{Name}':");
            foreach (var student in _students)
            {
                Console.WriteLine($"  - {student.Name} ({student.StudentId})");
            }
        }
    }
    public class UniversityManager //Основной класс для управления университетской системой
    {
        // Коллекции для хранения данных - инкапсуляция
        private List<Student> _students;
        private List<Teacher> _teachers;
        private List<Course> _courses;
        public UniversityManager() // Конструктор инициализирует пустые коллекции
        {
            _students = new List<Student>();
            _teachers = new List<Teacher>();
            _courses = new List<Course>();
        }
        public bool AddStudent(Student student) // Метод добавления студента
        {
            if (student == null)
            {
                Console.WriteLine("Ошибка: Студент не может быть null");
                return false;
            }

            _students.Add(student);
            return true;
        }
        public bool AddTeacher(Teacher teacher) // Метод добавления преподавателя
        {
            if (teacher == null)
            {
                Console.WriteLine("Ошибка: Преподаватель не может быть null");
                return false;
            }

            _teachers.Add(teacher);
            return true;
        }
        public bool AddCourse(Course course) // Метод добавления курса
        {
            if (course == null)
            {
                Console.WriteLine("Ошибка: Курс не может быть null");
                return false;
            }

            _courses.Add(course);
            return true;
        }
        public Student FindStudentById(string studentId) // Метод поиска студентов по ID
        {
            return _students.FirstOrDefault(s => s.StudentId == studentId);
        }
        public Teacher FindTeacherById(string teacherId) // Метод поиска преподавателей по ID
        {
            return _teachers.FirstOrDefault(t => t.TeacherId == teacherId);
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
