using System.Xml.Linq;

public class Item
{
        
    public Item(string name, string type, int attackBonus = 0, int defenseBonus = 0) // Конструктор класса Item для инициализации свойств
    {
        
    }
    public virtual void Use(Player player) // Виртуальный метод использования предмета 
    {

    }    
    public virtual void ShowStats() // Виртуальный метод отображения статистики предмета
    {
        
    }
}
public class Weapon: Item
{
    // Конструктор класса Weapon, вызывает базовый конструктор с типом "Оружие"
    public Weapon(string name, int attackBonus) 
        : base(name, "Оружие", attackBonus, 0) 
    {

    } 
    public override void ShowStats() // Переопределенный метод отображения статистики для оружия
    {
        
    }
}
public class Armor: Item
{
    // Конструктор класса Armor, вызывает базовый конструктор с типом "Доспехи"
    public Armor(string name, int defenseBonus)
        : base(name, "Доспехи", 0, defenseBonus) { } 
    public override void ShowStats() // Переопределенный метод отображения статистики для доспехов
    {
        
    }
}
public class Potion: Item
{
    // Конструктор класса Potion, вызывает базовый конструктор с типом "Зелье"
    public Potion(string name)
        : base(name, "Зелье") { } 
    public override void Use(Player player) // Переопределенный метод использования зелья
    {
        
    }
    public override void ShowStats() // Переопределенный метод отображения статистики для зелья
    {

    }
}
public class Player
{
    public void Attack(Enemy enemy) // Метод атаки врага
    {

    }
    public void Defend() // Метод защиты от атаки
    {
    
    }
    public void TakeDamage(int damage) // Метод получения урона
    {
        
    }
    public void Heal(int amount) // Метод лечения игрока
    {

    }
    public void EquipWeapon(Weapon weapon) // Метод экипировки оружия
    {

    }
    public void EquipArmor(Armor armor) // Метод экипировки доспехов
    {

    }
    public int GetTotalAttack() // Метод расчета общей атаки игрока
    {

    }   
    public int GetTotalDefense() // Метод расчета общей защиты игрока
    {

    }
    public void ShowStatus() // Метод отображения статуса игрока
    {

    }
    public void ShowCurrentEquipmentStats()  // Метод отображения статистики текущей экипировки
    {

    }
}
public class Enemy
{

}
public class Goblin: Enemy
{

}
public class Skeleton: Enemy
{

}
public class Mage: Enemy
{

}
public class Boss: Enemy
{

}
public class VVG : Boss
{

}
public class Kovalsky: Boss
{

}
public class ArchimageCPP: Boss
{

}
public class PestovCMM: Boss
{

}
public class Chest
{

}
public class Game
{

}
class Program
{

}