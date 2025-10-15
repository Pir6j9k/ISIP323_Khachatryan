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

    }
    public class Program // Главный класс программы
    {

    }
}
