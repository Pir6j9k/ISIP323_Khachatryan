using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace University
{
    public abstract class Person // Базовый класс для всех людей в университете
    {

    }
    public class Student : Person //Класс студента - наследник Person
    {
        public bool EnrollInCourse(Course course) // Метод для записи студента на курс
        {

        }
        public override void DisplayInfo() // Метод вывода данных о студенте
        {
           
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

    }
}
